using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Bloom and Glow/Bloom")]
public class Bloom : PostEffectsBase
{
	public enum LensFlareStyle
	{
		Ghosting = 0,
		Anamorphic = 1,
		Combined = 2,
	}

	public enum TweakMode
	{
		Basic = 0,
		Complex = 1,
	}

	public enum HDRBloomMode
	{
		Auto = 0,
		On = 1,
		Off = 2,
	}

	public enum BloomScreenBlendMode
	{
		Screen = 0,
		Add = 1,
	}

	public enum BloomQuality
	{
		Cheap = 0,
		High = 1,
	}

	public TweakMode tweakMode = TweakMode.Basic;
	public BloomScreenBlendMode screenBlendMode = BloomScreenBlendMode.Add;

	public HDRBloomMode hdr = HDRBloomMode.Auto;
	private bool doHdr = false;
	public float sepBlurSpread = 2.50f;

	public BloomQuality quality = BloomQuality.High;

	public float bloomIntensity = 0.50f;
	public float bloomThreshhold = 0.50f;
	public Color bloomThreshholdColor = Color.white;
	public int bloomBlurIterations = 2;

	public int hollywoodFlareBlurIterations = 2;
	public float flareRotation = 0.0f;
	public LensFlareStyle lensflareMode = LensFlareStyle.Anamorphic;
	public float hollyStretchWidth = 2.50f;
	public float lensflareIntensity = 0.0f;
	public float lensflareThreshhold = 0.30f;
	public float lensFlareSaturation = 0.75f;
	public Color flareColorA = new Color(0.40f, 0.40f, 0.80f, 0.75f);
	public Color flareColorB = new Color(0.40f, 0.80f, 0.80f, 0.75f);
	public Color flareColorC = new Color(0.80f, 0.40f, 0.80f, 0.75f);
	public Color flareColorD = new Color(0.80f, 0.40f, 0.0f, 0.75f);
	public float blurWidth = 1.0f;
	public Texture2D lensFlareVignetteMask;

	public Shader lensFlareShader;
	private Material lensFlareMaterial;

	public Shader screenBlendShader;
	private Material screenBlend;

	public Shader blurAndFlaresShader;
	private Material blurAndFlaresMaterial;

	public Shader brightPassFilterShader;
	private Material brightPassFilterMaterial;

	protected override bool CheckResources()
	{
		this.CheckSupport(false);

		this.screenBlend = this.CheckShaderAndCreateMaterial(
			this.screenBlendShader, this.screenBlend);
		this.lensFlareMaterial = this.CheckShaderAndCreateMaterial(
			this.lensFlareShader, this.lensFlareMaterial);
		this.blurAndFlaresMaterial = this.CheckShaderAndCreateMaterial(
			this.blurAndFlaresShader, this.blurAndFlaresMaterial);
		this.brightPassFilterMaterial = this.CheckShaderAndCreateMaterial(
			this.brightPassFilterShader, this.brightPassFilterMaterial);

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

		// Screen blend is not supported when HDR is enabled (will cap values).
		this.doHdr = false;
		if (this.hdr == HDRBloomMode.Auto)
		{
			this.doHdr = (source.format == RenderTextureFormat.ARGBHalf) &&
				this.GetComponent<Camera>().allowHDR;
		}
		else
		{
			this.doHdr = this.hdr == HDRBloomMode.On;
		}

		this.doHdr = this.doHdr && this.supportHDRTextures;

		BloomScreenBlendMode realBlendMode = this.doHdr ?
			BloomScreenBlendMode.Add : this.screenBlendMode;

		RenderTextureFormat rtFormat = this.doHdr ? RenderTextureFormat.ARGBHalf : RenderTextureFormat.Default;
		int rtW2 = source.width / 2;
		int rtH2 = source.height / 2;
		int rtW4 = source.width / 4;
		int rtH4 = source.height / 4;

		float widthOverHeight = (float)source.width / source.height;
		float oneOverBaseSize = 1.0f / 512.0f;

		// Downsample.
		RenderTexture quarterRezColor = RenderTexture.GetTemporary(rtW4, rtH4, 0, rtFormat);
		RenderTexture halfRezColorDown = RenderTexture.GetTemporary(rtW2, rtH2, 0, rtFormat);

		if (this.quality > BloomQuality.Cheap)
		{
			Graphics.Blit(source, halfRezColorDown, this.screenBlend, 2);
			RenderTexture rtDown4 = RenderTexture.GetTemporary(rtW4, rtH4, 0, rtFormat);
			Graphics.Blit(halfRezColorDown, rtDown4, this.screenBlend, 2);
			Graphics.Blit(rtDown4, quarterRezColor, this.screenBlend, 6);
			RenderTexture.ReleaseTemporary(rtDown4);
		}
		else
		{
			Graphics.Blit(source, halfRezColorDown);
			Graphics.Blit(halfRezColorDown, quarterRezColor, this.screenBlend, 6);
		}
		RenderTexture.ReleaseTemporary(halfRezColorDown);

		// Cut colors (thresholding).
		RenderTexture secondQuarterRezColor = RenderTexture.GetTemporary(rtW4, rtH4, 0, rtFormat);
		this.BrightFilter(this.bloomThreshhold * this.bloomThreshholdColor,
			quarterRezColor, secondQuarterRezColor);

		// Blurring.
		if (this.bloomBlurIterations < 1)
		{
			this.bloomBlurIterations = 1;
		}
		else if (this.bloomBlurIterations > 10)
		{
			this.bloomBlurIterations = 10;
		}

		for (int iter = 0; iter < this.bloomBlurIterations; iter++)
		{
			float spreadForPass = (1.0f + (iter * 0.25f)) * this.sepBlurSpread;

			// Vertical blur.
			RenderTexture blur4 = RenderTexture.GetTemporary(rtW4, rtH4, 0, rtFormat);
			this.blurAndFlaresMaterial.SetVector("_Offsets",
				new Vector4(0.0f, spreadForPass * oneOverBaseSize, 0.0f, 0.0f));
			Graphics.Blit(secondQuarterRezColor, blur4, this.blurAndFlaresMaterial, 4);
			RenderTexture.ReleaseTemporary(secondQuarterRezColor);
			secondQuarterRezColor = blur4;

			// Horizontal blur.
			blur4 = RenderTexture.GetTemporary(rtW4, rtH4, 0, rtFormat);
			blurAndFlaresMaterial.SetVector("_Offsets",
				new Vector4((spreadForPass / widthOverHeight) * oneOverBaseSize, 0.0f, 0.0f, 0.0f));
			Graphics.Blit(secondQuarterRezColor, blur4, this.blurAndFlaresMaterial, 4);
			RenderTexture.ReleaseTemporary(secondQuarterRezColor);
			secondQuarterRezColor = blur4;

			if (this.quality > BloomQuality.Cheap)
			{
				if (iter == 0)
				{
					Graphics.SetRenderTarget(quarterRezColor);
					GL.Clear(false, true, Color.black); // Clear to avoid RT restore.
					Graphics.Blit(secondQuarterRezColor, quarterRezColor);
				}
				else
				{
					quarterRezColor.MarkRestoreExpected(); // Using max blending, RT restore expected.
					Graphics.Blit(secondQuarterRezColor, quarterRezColor, screenBlend, 10);
				}
			}
		}

		if (this.quality > BloomQuality.Cheap)
		{
			Graphics.SetRenderTarget(secondQuarterRezColor);
			GL.Clear(false, true, Color.black); // Clear to avoid RT restore.
			Graphics.Blit(quarterRezColor, secondQuarterRezColor, this.screenBlend, 6);
		}

		// Lens flares: ghosting, anamorphic or both (ghosted anamorphic flares).
		if (this.lensflareIntensity > Mathf.Epsilon)
		{
			RenderTexture rtFlares4 = RenderTexture.GetTemporary(rtW4, rtH4, 0, rtFormat);

			if (this.lensflareMode == 0)
			{
				// Ghosting only.
				this.BrightFilter(this.lensflareThreshhold, secondQuarterRezColor, rtFlares4);

				if (this.quality > BloomQuality.Cheap)
				{
					// Smooth a little.
					this.blurAndFlaresMaterial.SetVector("_Offsets",
						new Vector4(0.0f, 1.50f / quarterRezColor.height, 0.0f, 0.0f));
					Graphics.SetRenderTarget(quarterRezColor);
					GL.Clear(false, true, Color.black); // Clear to avoid RT restore.
					Graphics.Blit(rtFlares4, quarterRezColor, this.blurAndFlaresMaterial, 4);

					blurAndFlaresMaterial.SetVector("_Offsets",
						new Vector4(1.50f / quarterRezColor.width, 0.0f, 0.0f, 0.0f));
					Graphics.SetRenderTarget(rtFlares4);
					GL.Clear(false, true, Color.black); // Clear to avoid RT restore.
					Graphics.Blit(quarterRezColor, rtFlares4, this.blurAndFlaresMaterial, 4);
				}

				// No ugly edges!
				this.Vignette(0.975f, rtFlares4, rtFlares4);
				this.BlendFlares(rtFlares4, secondQuarterRezColor);
			}
			else
			{
				//Vignette (0.975f, rtFlares4, rtFlares4);	
				//DrawBorder(rtFlares4, screenBlend, 8);

				float flareXRot = Mathf.Cos(this.flareRotation);
				float flareyRot = Mathf.Sin(this.flareRotation);

				float stretchWidth = (this.hollyStretchWidth * 1.0f / widthOverHeight) * oneOverBaseSize;
				float stretchWidthY = this.hollyStretchWidth * oneOverBaseSize;

				this.blurAndFlaresMaterial.SetVector("_Offsets",
					new Vector4(flareXRot, flareyRot, 0.0f, 0.0f));
				this.blurAndFlaresMaterial.SetVector("_Threshhold",
					new Vector4(this.lensflareThreshhold, 1.0f, 0.0f, 0.0f));
				this.blurAndFlaresMaterial.SetVector("_TintColor",
					new Vector4(this.flareColorA.r, this.flareColorA.g, this.flareColorA.b, this.flareColorA.a) *
					(this.flareColorA.a * this.lensflareIntensity));
				this.blurAndFlaresMaterial.SetFloat("_Saturation", this.lensFlareSaturation);

				// "pre and cut"
				quarterRezColor.DiscardContents();
				Graphics.Blit(rtFlares4, quarterRezColor, this.blurAndFlaresMaterial, 2);
				// "post"
				rtFlares4.DiscardContents();
				Graphics.Blit(quarterRezColor, rtFlares4, this.blurAndFlaresMaterial, 3);

				this.blurAndFlaresMaterial.SetVector("_Offsets",
					new Vector4(flareXRot * stretchWidth, flareyRot * stretchWidth, 0.0f, 0.0f));
				// stretch 1st
				this.blurAndFlaresMaterial.SetFloat("_StretchWidth", this.hollyStretchWidth);
				quarterRezColor.DiscardContents();
				Graphics.Blit(rtFlares4, quarterRezColor, this.blurAndFlaresMaterial, 1);
				// stretch 2nd
				this.blurAndFlaresMaterial.SetFloat("_StretchWidth", this.hollyStretchWidth * 2.0f);
				rtFlares4.DiscardContents();
				Graphics.Blit(quarterRezColor, rtFlares4, this.blurAndFlaresMaterial, 1);
				// stretch 3rd
				this.blurAndFlaresMaterial.SetFloat("_StretchWidth", this.hollyStretchWidth * 4.0f);
				quarterRezColor.DiscardContents();
				Graphics.Blit(rtFlares4, quarterRezColor, this.blurAndFlaresMaterial, 1);

				// Additional blur passes.
				for (int iter = 0; iter < hollywoodFlareBlurIterations; iter++)
				{
					stretchWidth = (this.hollyStretchWidth * 2.0f / widthOverHeight) * oneOverBaseSize;

					blurAndFlaresMaterial.SetVector("_Offsets",
						new Vector4(stretchWidth * flareXRot, stretchWidth * flareyRot, 0.0f, 0.0f));
					rtFlares4.DiscardContents();
					Graphics.Blit(quarterRezColor, rtFlares4, this.blurAndFlaresMaterial, 4);

					blurAndFlaresMaterial.SetVector("_Offsets",
						new Vector4(stretchWidth * flareXRot, stretchWidth * flareyRot, 0.0f, 0.0f));
					quarterRezColor.DiscardContents();
					Graphics.Blit(rtFlares4, quarterRezColor, this.blurAndFlaresMaterial, 4);
				}

				if (lensflareMode == LensFlareStyle.Anamorphic)
					// anamorphic lens flares															
					this.AddTo(1.0f, quarterRezColor, secondQuarterRezColor);
				else
				{
					// "combined" lens flares													
					this.Vignette(1.0f, quarterRezColor, rtFlares4);
					this.BlendFlares(rtFlares4, quarterRezColor);
					this.AddTo(1.0f, quarterRezColor, secondQuarterRezColor);
				}
			}

			RenderTexture.ReleaseTemporary(rtFlares4);
		}

		int blendPass = (int)realBlendMode;

		//if(Mathf.Abs(chromaticBloom) < Mathf.Epsilon) 
		//	blendPass += 4;

		this.screenBlend.SetFloat("_Intensity", this.bloomIntensity);
		this.screenBlend.SetTexture("_ColorBuffer", source);

		if (this.quality > BloomQuality.Cheap)
		{
			RenderTexture halfRezColorUp = RenderTexture.GetTemporary(rtW2, rtH2, 0, rtFormat);
			Graphics.Blit(secondQuarterRezColor, halfRezColorUp);
			Graphics.Blit(halfRezColorUp, destination, this.screenBlend, blendPass);
			RenderTexture.ReleaseTemporary(halfRezColorUp);
		}
		else
		{
			Graphics.Blit(secondQuarterRezColor, destination, this.screenBlend, blendPass);
		}

		RenderTexture.ReleaseTemporary(quarterRezColor);
		RenderTexture.ReleaseTemporary(secondQuarterRezColor);
	}

	void AddTo(float intensity_, RenderTexture from, RenderTexture to)
	{
		this.screenBlend.SetFloat("_Intensity", intensity_);
		to.MarkRestoreExpected(); // Additive blending, RT restore expected.
		Graphics.Blit(from, to, this.screenBlend, 9);
	}

	void BlendFlares(RenderTexture from, RenderTexture to)
	{
		this.lensFlareMaterial.SetVector("colorA", new Vector4(
			this.flareColorA.r, this.flareColorA.g, this.flareColorA.b, this.flareColorA.a) *
			this.lensflareIntensity);
		this.lensFlareMaterial.SetVector("colorB", new Vector4(
			this.flareColorB.r, this.flareColorB.g, this.flareColorB.b, this.flareColorB.a) *
			this.lensflareIntensity);
		this.lensFlareMaterial.SetVector("colorC", new Vector4(
			this.flareColorC.r, this.flareColorC.g, this.flareColorC.b, this.flareColorC.a) *
			this.lensflareIntensity);
		this.lensFlareMaterial.SetVector("colorD", new Vector4(
			this.flareColorD.r, this.flareColorD.g, this.flareColorD.b, this.flareColorD.a) *
			this.lensflareIntensity);

		to.MarkRestoreExpected(); // Additive blending, RT restore expected.
		Graphics.Blit(from, to, this.lensFlareMaterial);
	}

	void BrightFilter(float thresh, RenderTexture from, RenderTexture to)
	{
		this.brightPassFilterMaterial.SetVector("_Threshhold", new Vector4(thresh, thresh, thresh, thresh));
		Graphics.Blit(from, to, this.brightPassFilterMaterial, 0);
	}

	void BrightFilter(Color threshColor, RenderTexture from, RenderTexture to)
	{
		this.brightPassFilterMaterial.SetVector("_Threshhold", threshColor);
		Graphics.Blit(from, to, this.brightPassFilterMaterial, 1);
	}

	void Vignette(float amount, RenderTexture from, RenderTexture to)
	{
		if (this.lensFlareVignetteMask)
		{
			this.screenBlend.SetTexture("_ColorBuffer", this.lensFlareVignetteMask);
			to.MarkRestoreExpected(); // Using blending, RT restore expected.
			Graphics.Blit((from == to) ? null : from, to, this.screenBlend, (from == to) ? 7 : 3);
		}
		else if (from != to)
		{
			Graphics.SetRenderTarget(to);
			GL.Clear(false, true, Color.black); // Clear destination to avoid RT restore.
			Graphics.Blit(from, to);
		}
	}
}
