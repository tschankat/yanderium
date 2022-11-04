using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Rendering/Sun Shafts")]
public class SunShafts : PostEffectsBase
{
	public enum SunShaftsResolution
	{
		Low = 0,
		Normal = 1,
		High = 2,
	}

	public enum ShaftsScreenBlendMode
	{
		Screen = 0,
		Add = 1,
	}

	public SunShaftsResolution resolution = SunShaftsResolution.Normal;
	public ShaftsScreenBlendMode screenBlendMode = ShaftsScreenBlendMode.Screen;

	public Transform sunTransform;
	public int radialBlurIterations = 2;
	public Color sunColor = Color.white;
	public float sunShaftBlurRadius = 2.5f;
	public float sunShaftIntensity = 1.15f;
	public float useSkyBoxAlpha = 0.75f;

	public float maxRadius = 0.75f;

	public bool useDepthTexture = true;

	public Shader sunShaftsShader;
	private Material sunShaftsMaterial;

	public Shader simpleClearShader;
	private Material simpleClearMaterial;

	protected override bool CheckResources()
	{
		this.CheckSupport(this.useDepthTexture);

		this.sunShaftsMaterial = this.CheckShaderAndCreateMaterial(
			this.sunShaftsShader, this.sunShaftsMaterial);
		this.simpleClearMaterial = this.CheckShaderAndCreateMaterial(
			this.simpleClearShader, this.simpleClearMaterial);

		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}

		return this.isSupported;
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
			return;
		}

		Camera camera = this.GetComponent<Camera>();

		// we actually need to check this every frame
		if (this.useDepthTexture)
		{
			camera.depthTextureMode |= DepthTextureMode.Depth;
		}

		int divider = 4;

		if (this.resolution == SunShaftsResolution.Normal)
		{
			divider = 2;
		}
		else if (resolution == SunShaftsResolution.High)
		{
			divider = 1;
		}

		Vector3 v = Vector3.one * 0.50f;

		if (this.sunTransform)
		{
			v = camera.WorldToViewportPoint(this.sunTransform.position);
		}
		else
		{
			v = new Vector3(0.50f, 0.50f, 0.0f);
		}

		int rtW = source.width / divider;
		int rtH = source.height / divider;

		RenderTexture lrColorB;
		RenderTexture lrDepthBuffer = RenderTexture.GetTemporary(rtW, rtH, 0);

		// mask out everything except the skybox
		// we have 2 methods, one of which requires depth buffer support, the other one is just comparing images

		this.sunShaftsMaterial.SetVector("_BlurRadius4",
			new Vector4(1.0f, 1.0f, 0.0f, 0.0f) * this.sunShaftBlurRadius);
		this.sunShaftsMaterial.SetVector("_SunPosition", new Vector4(v.x, v.y, v.z, this.maxRadius));
		this.sunShaftsMaterial.SetFloat("_NoSkyBoxMask", 1.0f - this.useSkyBoxAlpha);

		if (!this.useDepthTexture)
		{
			RenderTexture tmpBuffer = RenderTexture.GetTemporary(source.width, source.height, 0);
			RenderTexture.active = tmpBuffer;
			GL.ClearWithSkybox(false, camera);

			this.sunShaftsMaterial.SetTexture("_Skybox", tmpBuffer);
			Graphics.Blit(source, lrDepthBuffer, this.sunShaftsMaterial, 3);
			RenderTexture.ReleaseTemporary(tmpBuffer);
		}
		else
		{
			Graphics.Blit(source, lrDepthBuffer, this.sunShaftsMaterial, 2);
		}

		// paint a small black small border to get rid of clamping problems
		this.DrawBorder(lrDepthBuffer, this.simpleClearMaterial);

		// radial blur:

		this.radialBlurIterations = Mathf.Clamp(this.radialBlurIterations, 1, 4);

		float ofs = this.sunShaftBlurRadius * (1.0f / 768.0f);

		this.sunShaftsMaterial.SetVector("_BlurRadius4", new Vector4(ofs, ofs, 0.0f, 0.0f));
		this.sunShaftsMaterial.SetVector("_SunPosition", new Vector4(v.x, v.y, v.z, maxRadius));

		for (int it2 = 0; it2 < this.radialBlurIterations; it2++)
		{
			// each iteration takes 2 * 6 samples
			// we update _BlurRadius each time to cheaply get a very smooth look

			lrColorB = RenderTexture.GetTemporary(rtW, rtH, 0);
			Graphics.Blit(lrDepthBuffer, lrColorB, this.sunShaftsMaterial, 1);
			RenderTexture.ReleaseTemporary(lrDepthBuffer);
			ofs = this.sunShaftBlurRadius * (((it2 * 2.0f + 1.0f) * 6.0f)) / 768.0f;
			this.sunShaftsMaterial.SetVector("_BlurRadius4", new Vector4(ofs, ofs, 0.0f, 0.0f));

			lrDepthBuffer = RenderTexture.GetTemporary(rtW, rtH, 0);
			Graphics.Blit(lrColorB, lrDepthBuffer, this.sunShaftsMaterial, 1);
			RenderTexture.ReleaseTemporary(lrColorB);
			ofs = this.sunShaftBlurRadius * (((it2 * 2.0f + 2.0f) * 6.0f)) / 768.0f;
			this.sunShaftsMaterial.SetVector("_BlurRadius4", new Vector4(ofs, ofs, 0.0f, 0.0f));
		}

		// put together:

		if (v.z >= 0.0f)
		{
			this.sunShaftsMaterial.SetVector("_SunColor",
				new Vector4(sunColor.r, sunColor.g, sunColor.b, sunColor.a) * this.sunShaftIntensity);
		}
		else
		{
			this.sunShaftsMaterial.SetVector("_SunColor", Vector4.zero); // no backprojection !
		}

		this.sunShaftsMaterial.SetTexture("_ColorBuffer", lrDepthBuffer);
		Graphics.Blit(source, destination, this.sunShaftsMaterial,
			(this.screenBlendMode == ShaftsScreenBlendMode.Screen) ? 0 : 4);

		RenderTexture.ReleaseTemporary(lrDepthBuffer);
	}
}
