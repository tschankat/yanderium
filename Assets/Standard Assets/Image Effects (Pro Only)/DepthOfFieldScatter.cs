using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Camera/Depth of Field (Lens Blur, Scatter, DX11)")]
public class DepthOfFieldScatter : PostEffectsBase
{
	public bool visualizeFocus = false;
	public float focalLength = 10.0f;
	public float focalSize = 0.05f;
	public float aperture = 11.5f;
	public Transform focalTransform = null;
	public float maxBlurSize = 2.0f;
	public bool highResolution = false;

	public enum BlurType
	{
		DiscBlur = 0,
		DX11 = 1,
	}

	public enum BlurSampleCount
	{
		Low = 0,
		Medium = 1,
		High = 2,
	}

	public BlurType blurType = BlurType.DiscBlur;
	public BlurSampleCount blurSampleCount = BlurSampleCount.High;

	public bool nearBlur = false;
	public float foregroundOverlap = 1.0f;

	public Shader dofHdrShader;
	private Material dofHdrMaterial = null;

	public Shader dx11BokehShader;
	private Material dx11bokehMaterial;

	public float dx11BokehThreshhold = 0.5f;
	public float dx11SpawnHeuristic = 0.0875f;
	public Texture2D dx11BokehTexture = null;
	public float dx11BokehScale = 1.20f;
	public float dx11BokehIntensity = 2.5f;

	private float focalDistance01 = 10.0f;
	private ComputeBuffer cbDrawArgs;
	private ComputeBuffer cbPoints;
	private float internalBlurWidth = 1.0f;

	protected override bool CheckResources()
	{
		this.CheckSupport(true); // only requires depth, not HDR.

		this.dofHdrMaterial = this.CheckShaderAndCreateMaterial(this.dofHdrShader, this.dofHdrMaterial);
		if (this.supportDX11 && (this.blurType == BlurType.DX11))
		{
			this.dx11bokehMaterial = this.CheckShaderAndCreateMaterial(this.dx11BokehShader, this.dx11bokehMaterial);
			this.CreateComputeResources();
		}

		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}

		return this.isSupported;
	}

	void OnEnable()
	{
		this.GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;
	}

	void OnDisable()
	{
		this.ReleaseComputeResources();

		if (this.dofHdrMaterial)
		{
			DestroyImmediate(this.dofHdrMaterial);
		}

		this.dofHdrMaterial = null;

		if (this.dx11bokehMaterial)
		{
			DestroyImmediate(this.dx11bokehMaterial);
		}

		this.dx11bokehMaterial = null;
	}

	void ReleaseComputeResources()
	{
		if (this.cbDrawArgs != null)
		{
			this.cbDrawArgs.Release();
		}

		this.cbDrawArgs = null;

		if (this.cbPoints != null)
		{
			cbPoints.Release();
		}

		this.cbPoints = null;
	}

	void CreateComputeResources()
	{
		if (this.cbDrawArgs == null)
		{
			this.cbDrawArgs = new ComputeBuffer(1, 16, ComputeBufferType.IndirectArguments);
			int[] args = new int[4];
			args[0] = 0;
			args[1] = 1;
			args[2] = 0;
			args[3] = 0;
			this.cbDrawArgs.SetData(args);
		}

		if (this.cbPoints == null)
		{
			this.cbPoints = new ComputeBuffer(90000, 12 + 16, ComputeBufferType.Append);
		}
	}

	float FocalDistance01(float worldDist)
	{
		Camera camera = this.GetComponent<Camera>();
		return camera.WorldToViewportPoint((worldDist - camera.nearClipPlane) * camera.transform.forward +
			camera.transform.position).z / (camera.farClipPlane - camera.nearClipPlane);
	}

	void WriteCoc(RenderTexture fromTo, bool fgDilate)
	{
		this.dofHdrMaterial.SetTexture("_FgOverlap", null);

		if (this.nearBlur && fgDilate)
		{
			int rtW = fromTo.width / 2;
			int rtH = fromTo.height / 2;

			// capture fg coc
			RenderTexture temp2 = RenderTexture.GetTemporary(rtW, rtH, 0, fromTo.format);
			Graphics.Blit(fromTo, temp2, this.dofHdrMaterial, 4);

			// special blur
			float fgAdjustment = this.internalBlurWidth * this.foregroundOverlap;

			this.dofHdrMaterial.SetVector("_Offsets", new Vector4(0.0f, fgAdjustment, 0.0f, fgAdjustment));
			RenderTexture temp1 = RenderTexture.GetTemporary(rtW, rtH, 0, fromTo.format);
			Graphics.Blit(temp2, temp1, this.dofHdrMaterial, 2);
			RenderTexture.ReleaseTemporary(temp2);

			this.dofHdrMaterial.SetVector("_Offsets", new Vector4(fgAdjustment, 0.0f, 0.0f, fgAdjustment));
			temp2 = RenderTexture.GetTemporary(rtW, rtH, 0, fromTo.format);
			Graphics.Blit(temp1, temp2, this.dofHdrMaterial, 2);
			RenderTexture.ReleaseTemporary(temp1);

			// "merge up" with background COC
			this.dofHdrMaterial.SetTexture("_FgOverlap", temp2);
			fromTo.MarkRestoreExpected(); // only touching alpha channel, RT restore expected
			Graphics.Blit(fromTo, fromTo, this.dofHdrMaterial, 13);
			RenderTexture.ReleaseTemporary(temp2);
		}
		else
		{
			// capture full coc in alpha channel (fromTo is not read, but bound to detect screen flip)
			Graphics.Blit(fromTo, fromTo, this.dofHdrMaterial, 0);
		}
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
			return;
		}

		// clamp & prepare values so they make sense
		if (this.aperture < 0.0f)
		{
			this.aperture = 0.0f;
		}

		if (this.maxBlurSize < 0.1f)
		{
			this.maxBlurSize = 0.1f;
		}

		this.focalSize = Mathf.Clamp(this.focalSize, 0.0f, 2.0f);
		this.internalBlurWidth = Mathf.Max(this.maxBlurSize, 0.0f);

		// focal & coc calculations
		Camera camera = this.GetComponent<Camera>();
		this.focalDistance01 = this.focalTransform ?
			((camera.WorldToViewportPoint(this.focalTransform.position)).z / camera.farClipPlane) :
			this.FocalDistance01(this.focalLength);
		this.dofHdrMaterial.SetVector("_CurveParams",
			new Vector4(1.0f, this.focalSize, this.aperture / 10.0f, focalDistance01));

		// possible render texture helpers

		RenderTexture rtLow = null;
		RenderTexture rtLow2 = null;
		RenderTexture rtSuperLow1 = null;
		RenderTexture rtSuperLow2 = null;
		float fgBlurDist = this.internalBlurWidth * this.foregroundOverlap;

		if (this.visualizeFocus)
		{
			//
			// 2.
			// visualize coc
			//
			//

			this.WriteCoc(source, true);
			Graphics.Blit(source, destination, this.dofHdrMaterial, 16);
		}
		else if ((this.blurType == BlurType.DX11) && this.dx11bokehMaterial)
		{
			//
			// 1.
			// optimized dx11 bokeh scatter
			//
			//

			if (this.highResolution)
			{
				this.internalBlurWidth = this.internalBlurWidth < 0.1f ? 0.1f : this.internalBlurWidth;
				fgBlurDist = this.internalBlurWidth * this.foregroundOverlap;

				rtLow = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);

				RenderTexture dest2 = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);

				// capture COC
				this.WriteCoc(source, false);

				// blur a bit so we can do a frequency check
				rtSuperLow1 = RenderTexture.GetTemporary(source.width >> 1, source.height >> 1, 0, source.format);
				rtSuperLow2 = RenderTexture.GetTemporary(source.width >> 1, source.height >> 1, 0, source.format);

				Graphics.Blit(source, rtSuperLow1, this.dofHdrMaterial, 15);
				this.dofHdrMaterial.SetVector("_Offsets", new Vector4(0.0f, 1.5f, 0.0f, 1.5f));
				Graphics.Blit(rtSuperLow1, rtSuperLow2, this.dofHdrMaterial, 19);
				this.dofHdrMaterial.SetVector("_Offsets", new Vector4(1.5f, 0.0f, 0.0f, 1.5f));
				Graphics.Blit(rtSuperLow2, rtSuperLow1, this.dofHdrMaterial, 19);

				// capture fg coc
				if (this.nearBlur)
				{
					Graphics.Blit(source, rtSuperLow2, this.dofHdrMaterial, 4);
				}

				this.dx11bokehMaterial.SetTexture("_BlurredColor", rtSuperLow1);
				this.dx11bokehMaterial.SetFloat("_SpawnHeuristic", this.dx11SpawnHeuristic);
				this.dx11bokehMaterial.SetVector("_BokehParams", new Vector4(
					this.dx11BokehScale, this.dx11BokehIntensity,
					Mathf.Clamp(this.dx11BokehThreshhold, 0.005f, 4.0f), this.internalBlurWidth));
				this.dx11bokehMaterial.SetTexture("_FgCocMask", this.nearBlur ? rtSuperLow2 : null);

				// collect bokeh candidates and replace with a darker pixel
				Graphics.SetRandomWriteTarget(1, this.cbPoints);
				Graphics.Blit(source, rtLow, this.dx11bokehMaterial, 0);
				Graphics.ClearRandomWriteTargets();

				// fg coc blur happens here (after collect!)
				if (this.nearBlur)
				{
					this.dofHdrMaterial.SetVector("_Offsets", new Vector4(0.0f, fgBlurDist, 0.0f, fgBlurDist));
					Graphics.Blit(rtSuperLow2, rtSuperLow1, this.dofHdrMaterial, 2);
					this.dofHdrMaterial.SetVector("_Offsets", new Vector4(fgBlurDist, 0.0f, 0.0f, fgBlurDist));
					Graphics.Blit(rtSuperLow1, rtSuperLow2, this.dofHdrMaterial, 2);

					// merge fg coc with bg coc
					Graphics.Blit(rtSuperLow2, rtLow, this.dofHdrMaterial, 3);
				}

				// NEW: LAY OUT ALPHA on destination target so we get nicer outlines for the high rez version
				Graphics.Blit(rtLow, dest2, this.dofHdrMaterial, 20);

				// box blur (easier to merge with bokeh buffer)
				this.dofHdrMaterial.SetVector("_Offsets",
					new Vector4(this.internalBlurWidth, 0.0f, 0.0f, this.internalBlurWidth));
				Graphics.Blit(rtLow, source, this.dofHdrMaterial, 5);
				this.dofHdrMaterial.SetVector("_Offsets",
					new Vector4(0.0f, this.internalBlurWidth, 0.0f, this.internalBlurWidth));
				Graphics.Blit(source, dest2, this.dofHdrMaterial, 21);

				// apply bokeh candidates		
				Graphics.SetRenderTarget(dest2);
				ComputeBuffer.CopyCount(this.cbPoints, this.cbDrawArgs, 0);
				this.dx11bokehMaterial.SetBuffer("pointBuffer", this.cbPoints);
				this.dx11bokehMaterial.SetTexture("_MainTex", this.dx11BokehTexture);
				this.dx11bokehMaterial.SetVector("_Screen", new Vector3(
					1.0f / source.width, 1.0f / source.height, this.internalBlurWidth));
				this.dx11bokehMaterial.SetPass(2);

				Graphics.DrawProceduralIndirectNow(MeshTopology.Points, this.cbDrawArgs, 0);

				Graphics.Blit(dest2, destination);  // hackaround for DX11 high resolution flipfun (OPTIMIZEME)

				RenderTexture.ReleaseTemporary(dest2);
				RenderTexture.ReleaseTemporary(rtSuperLow1);
				RenderTexture.ReleaseTemporary(rtSuperLow2);
			}
			else
			{
				rtLow = RenderTexture.GetTemporary(source.width >> 1, source.height >> 1, 0, source.format);
				rtLow2 = RenderTexture.GetTemporary(source.width >> 1, source.height >> 1, 0, source.format);

				fgBlurDist = this.internalBlurWidth * this.foregroundOverlap;

				// capture COC & color in low resolution
				this.WriteCoc(source, false);
				source.filterMode = FilterMode.Bilinear;
				Graphics.Blit(source, rtLow, this.dofHdrMaterial, 6);

				// blur a bit so we can do a frequency check
				rtSuperLow1 = RenderTexture.GetTemporary(rtLow.width >> 1, rtLow.height >> 1, 0, rtLow.format);
				rtSuperLow2 = RenderTexture.GetTemporary(rtLow.width >> 1, rtLow.height >> 1, 0, rtLow.format);

				Graphics.Blit(rtLow, rtSuperLow1, this.dofHdrMaterial, 15);
				this.dofHdrMaterial.SetVector("_Offsets", new Vector4(0.0f, 1.5f, 0.0f, 1.5f));
				Graphics.Blit(rtSuperLow1, rtSuperLow2, this.dofHdrMaterial, 19);
				this.dofHdrMaterial.SetVector("_Offsets", new Vector4(1.5f, 0.0f, 0.0f, 1.5f));
				Graphics.Blit(rtSuperLow2, rtSuperLow1, this.dofHdrMaterial, 19);

				RenderTexture rtLow3 = null;

				if (this.nearBlur)
				{
					// capture fg coc
					rtLow3 = RenderTexture.GetTemporary(source.width >> 1, source.height >> 1, 0, source.format);
					Graphics.Blit(source, rtLow3, this.dofHdrMaterial, 4);
				}

				this.dx11bokehMaterial.SetTexture("_BlurredColor", rtSuperLow1);
				this.dx11bokehMaterial.SetFloat("_SpawnHeuristic", this.dx11SpawnHeuristic);
				this.dx11bokehMaterial.SetVector("_BokehParams", new Vector4(
					this.dx11BokehScale, this.dx11BokehIntensity,
					Mathf.Clamp(this.dx11BokehThreshhold, 0.005f, 4.0f), this.internalBlurWidth));
				this.dx11bokehMaterial.SetTexture("_FgCocMask", rtLow3);

				// collect bokeh candidates and replace with a darker pixel
				Graphics.SetRandomWriteTarget(1, this.cbPoints);
				Graphics.Blit(rtLow, rtLow2, this.dx11bokehMaterial, 0);
				Graphics.ClearRandomWriteTargets();

				RenderTexture.ReleaseTemporary(rtSuperLow1);
				RenderTexture.ReleaseTemporary(rtSuperLow2);

				// fg coc blur happens here (after collect!)
				if (this.nearBlur)
				{
					this.dofHdrMaterial.SetVector("_Offsets", new Vector4(0.0f, fgBlurDist, 0.0f, fgBlurDist));
					Graphics.Blit(rtLow3, rtLow, this.dofHdrMaterial, 2);
					this.dofHdrMaterial.SetVector("_Offsets", new Vector4(fgBlurDist, 0.0f, 0.0f, fgBlurDist));
					Graphics.Blit(rtLow, rtLow3, this.dofHdrMaterial, 2);

					// merge fg coc with bg coc
					Graphics.Blit(rtLow3, rtLow2, this.dofHdrMaterial, 3);
				}

				// box blur (easier to merge with bokeh buffer)
				this.dofHdrMaterial.SetVector("_Offsets",
					new Vector4(this.internalBlurWidth, 0.0f, 0.0f, this.internalBlurWidth));
				Graphics.Blit(rtLow2, rtLow, this.dofHdrMaterial, 5);
				dofHdrMaterial.SetVector("_Offsets",
					new Vector4(0.0f, this.internalBlurWidth, 0.0f, this.internalBlurWidth));
				Graphics.Blit(rtLow, rtLow2, this.dofHdrMaterial, 5);

				// apply bokeh candidates
				Graphics.SetRenderTarget(rtLow2);
				ComputeBuffer.CopyCount(this.cbPoints, this.cbDrawArgs, 0);
				this.dx11bokehMaterial.SetBuffer("pointBuffer", this.cbPoints);
				this.dx11bokehMaterial.SetTexture("_MainTex", this.dx11BokehTexture);
				this.dx11bokehMaterial.SetVector("_Screen",
					new Vector3(1.0f / rtLow2.width, 1.0f / rtLow2.height, this.internalBlurWidth));
				this.dx11bokehMaterial.SetPass(1);
				Graphics.DrawProceduralIndirectNow(MeshTopology.Points, this.cbDrawArgs, 0);

				// upsample & combine
				this.dofHdrMaterial.SetTexture("_LowRez", rtLow2);
				this.dofHdrMaterial.SetTexture("_FgOverlap", rtLow3);
				this.dofHdrMaterial.SetVector("_Offsets",
					((1.0f * source.width) / (1.0f * rtLow2.width)) * this.internalBlurWidth * Vector4.one);
				Graphics.Blit(source, destination, this.dofHdrMaterial, 9);

				if (rtLow3)
				{
					RenderTexture.ReleaseTemporary(rtLow3);
				}
			}
		}
		else
		{
			//
			// 2.
			// poisson disc style blur in low resolution
			//
			//

			source.filterMode = FilterMode.Bilinear;

			if (this.highResolution)
			{
				this.internalBlurWidth *= 2.0f;
			}

			this.WriteCoc(source, true);

			rtLow = RenderTexture.GetTemporary(source.width >> 1, source.height >> 1, 0, source.format);
			rtLow2 = RenderTexture.GetTemporary(source.width >> 1, source.height >> 1, 0, source.format);

			int blurPass = ((this.blurSampleCount == BlurSampleCount.High) ||
				(this.blurSampleCount == BlurSampleCount.Medium)) ? 17 : 11;

			if (this.highResolution)
			{
				this.dofHdrMaterial.SetVector("_Offsets",
					new Vector4(0.0f, this.internalBlurWidth, 0.025f, this.internalBlurWidth));
				Graphics.Blit(source, destination, this.dofHdrMaterial, blurPass);
			}
			else
			{
				this.dofHdrMaterial.SetVector("_Offsets",
					new Vector4(0.0f, this.internalBlurWidth, 0.10f, this.internalBlurWidth));

				// blur
				Graphics.Blit(source, rtLow, this.dofHdrMaterial, 6);
				Graphics.Blit(rtLow, rtLow2, this.dofHdrMaterial, blurPass);

				// cheaper blur in high resolution, upsample and combine
				this.dofHdrMaterial.SetTexture("_LowRez", rtLow2);
				this.dofHdrMaterial.SetTexture("_FgOverlap", null);
				this.dofHdrMaterial.SetVector("_Offsets",
					Vector4.one * ((1.0f * source.width) / (1.0f * rtLow2.width)) * this.internalBlurWidth);
				Graphics.Blit(source, destination, this.dofHdrMaterial,
					(blurSampleCount == BlurSampleCount.High) ? 18 : 12);
			}
		}

		if (rtLow)
		{
			RenderTexture.ReleaseTemporary(rtLow);
		}

		if (rtLow2)
		{
			RenderTexture.ReleaseTemporary(rtLow2);
		}
	}
}
