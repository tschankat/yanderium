using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Bloom and Glow/BloomAndFlares (3.50f, Deprecated)")]
public class BloomAndLensFlares : PostEffectsBase
{
	public enum LensflareStyle34
	{
		Ghosting = 0,
		Anamorphic = 1,
		Combined = 2,
	}

	public enum TweakMode34
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

	public TweakMode34 tweakMode = TweakMode34.Basic;
	public BloomScreenBlendMode screenBlendMode = BloomScreenBlendMode.Add;

	public HDRBloomMode hdr = HDRBloomMode.Auto;
	private bool doHdr = false;
	public float sepBlurSpread = 1.50f;
	public float useSrcAlphaAsMask = 0.50f;

	public float bloomIntensity = 1.0f;
	public float bloomThreshhold = 0.50f;
	public int bloomBlurIterations = 2;

	public bool lensflares = false;
	public int hollywoodFlareBlurIterations = 2;
	public LensflareStyle34 lensflareMode = LensflareStyle34.Anamorphic;
	public float hollyStretchWidth = 3.50f;
	public float lensflareIntensity = 1.0f;
	public float lensflareThreshhold = 0.30f;
	public Color flareColorA = new Color(0.40f, 0.40f, 0.80f, 0.75f);
	public Color flareColorB = new Color(0.40f, 0.80f, 0.80f, 0.75f);
	public Color flareColorC = new Color(0.80f, 0.40f, 0.80f, 0.75f);
	public Color flareColorD = new Color(0.80f, 0.40f, 0.0f, 0.75f);
	public float blurWidth = 1.0f;
	public Texture2D lensFlareVignetteMask;

	public Shader lensFlareShader;
	private Material lensFlareMaterial;

	public Shader vignetteShader;
	private Material vignetteMaterial;

	public Shader separableBlurShader;
	private Material separableBlurMaterial;

	public Shader addBrightStuffOneOneShader;
	private Material addBrightStuffBlendOneOneMaterial;

	public Shader screenBlendShader;
	private Material screenBlend;

	public Shader hollywoodFlaresShader;
	private Material hollywoodFlaresMaterial;

	public Shader brightPassFilterShader;
	private Material brightPassFilterMaterial;

	protected override bool CheckResources()
	{
		this.CheckSupport(false);

		this.screenBlend = this.CheckShaderAndCreateMaterial(
			this.screenBlendShader, this.screenBlend);
		this.lensFlareMaterial = this.CheckShaderAndCreateMaterial(
			this.lensFlareShader, this.lensFlareMaterial);
		this.vignetteMaterial = this.CheckShaderAndCreateMaterial(
			this.vignetteShader, this.vignetteMaterial);
		this.separableBlurMaterial = this.CheckShaderAndCreateMaterial(
			this.separableBlurShader, this.separableBlurMaterial);
		this.addBrightStuffBlendOneOneMaterial = this.CheckShaderAndCreateMaterial(
			this.addBrightStuffOneOneShader, this.addBrightStuffBlendOneOneMaterial);
		this.hollywoodFlaresMaterial = this.CheckShaderAndCreateMaterial(
			this.hollywoodFlaresShader, this.hollywoodFlaresMaterial);
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
		RenderTexture halfRezColor = RenderTexture.GetTemporary(
			source.width / 2, source.height / 2, 0, rtFormat);
		RenderTexture quarterRezColor = RenderTexture.GetTemporary(
			source.width / 4, source.height / 4, 0, rtFormat);
		RenderTexture secondQuarterRezColor = RenderTexture.GetTemporary(
			source.width / 4, source.height / 4, 0, rtFormat);
		RenderTexture thirdQuarterRezColor = RenderTexture.GetTemporary(
			source.width / 4, source.height / 4, 0, rtFormat);

		float widthOverHeight = (float)source.width / source.height;
		float oneOverBaseSize = 1.0f / 512.0f;

		// Downsample.
		Graphics.Blit(source, halfRezColor, this.screenBlend, 2); // <- 2 is stable downsample
		Graphics.Blit(halfRezColor, quarterRezColor, this.screenBlend, 2); // <- 2 is stable downsample

		RenderTexture.ReleaseTemporary(halfRezColor);

		// Cut colors (thresholding).
		this.BrightFilter(this.bloomThreshhold, this.useSrcAlphaAsMask,
			quarterRezColor, secondQuarterRezColor);
		quarterRezColor.DiscardContents();

		// Blurring.
		if (this.bloomBlurIterations < 1)
		{
			this.bloomBlurIterations = 1;
		}

		for (int iter = 0; iter < this.bloomBlurIterations; iter++)
		{
			float spreadForPass = (1.0f + (iter * 0.5f)) * this.sepBlurSpread;
			this.separableBlurMaterial.SetVector("offsets",
				new Vector4(0.0f, spreadForPass * oneOverBaseSize, 0.0f, 0.0f));

			RenderTexture src = (iter == 0) ? secondQuarterRezColor : quarterRezColor;
			Graphics.Blit(src, thirdQuarterRezColor, this.separableBlurMaterial);
			src.DiscardContents();

			this.separableBlurMaterial.SetVector("offsets",
				new Vector4((spreadForPass / widthOverHeight) * oneOverBaseSize, 0.0f, 0.0f, 0.0f));
			Graphics.Blit(thirdQuarterRezColor, quarterRezColor, this.separableBlurMaterial);
			thirdQuarterRezColor.DiscardContents();
		}

		// Lens flares: ghosting, anamorphic or a combination.
		if (this.lensflares)
		{
			if (this.lensflareMode == LensflareStyle34.Ghosting)
			{
				this.BrightFilter(this.lensflareThreshhold, 0.0f, quarterRezColor, thirdQuarterRezColor);
				quarterRezColor.DiscardContents();

				// smooth a little, this needs to be resolution dependent
				/*
				separableBlurMaterial.SetVector ("offsets", Vector4 (0.0f, (2.0f) / (1.0f * quarterRezColor.height), 0.0f, 0.0f));
				Graphics.Blit (thirdQuarterRezColor, secondQuarterRezColor, separableBlurMaterial);
				separableBlurMaterial.SetVector ("offsets", Vector4 ((2.0f) / (1.0f * quarterRezColor.width), 0.0f, 0.0f, 0.0f));
				Graphics.Blit (secondQuarterRezColor, thirdQuarterRezColor, separableBlurMaterial);
				*/
				// no ugly edges!

				this.Vignette(0.975f, thirdQuarterRezColor, secondQuarterRezColor);
				thirdQuarterRezColor.DiscardContents();

				this.BlendFlares(secondQuarterRezColor, quarterRezColor);
				secondQuarterRezColor.DiscardContents();
			}

			// (b) hollywood/anamorphic flares?

			else
			{
				// thirdQuarter has the brightcut unblurred colors
				// quarterRezColor is the blurred, brightcut buffer that will end up as bloom

				this.hollywoodFlaresMaterial.SetVector("_Threshhold",
					new Vector4(this.lensflareThreshhold, 1.0f / (1.0f - this.lensflareThreshhold), 0.0f, 0.0f));
				this.hollywoodFlaresMaterial.SetVector("tintColor", new Vector4(
					this.flareColorA.r, this.flareColorA.g, this.flareColorA.b, this.flareColorA.a) *
					(this.flareColorA.a * this.lensflareIntensity));
				Graphics.Blit(thirdQuarterRezColor, secondQuarterRezColor, this.hollywoodFlaresMaterial, 2);
				thirdQuarterRezColor.DiscardContents();

				Graphics.Blit(secondQuarterRezColor, thirdQuarterRezColor, this.hollywoodFlaresMaterial, 3);
				secondQuarterRezColor.DiscardContents();

				this.hollywoodFlaresMaterial.SetVector("offsets",
					new Vector4((sepBlurSpread * 1.0f / widthOverHeight) * oneOverBaseSize, 0.0f, 0.0f, 0.0f));
				this.hollywoodFlaresMaterial.SetFloat("stretchWidth", this.hollyStretchWidth);
				Graphics.Blit(thirdQuarterRezColor, secondQuarterRezColor, this.hollywoodFlaresMaterial, 1);
				thirdQuarterRezColor.DiscardContents();

				this.hollywoodFlaresMaterial.SetFloat("stretchWidth", this.hollyStretchWidth * 2.0f);
				Graphics.Blit(secondQuarterRezColor, thirdQuarterRezColor, this.hollywoodFlaresMaterial, 1);
				secondQuarterRezColor.DiscardContents();

				this.hollywoodFlaresMaterial.SetFloat("stretchWidth", this.hollyStretchWidth * 4.0f);
				Graphics.Blit(thirdQuarterRezColor, secondQuarterRezColor, this.hollywoodFlaresMaterial, 1);
				thirdQuarterRezColor.DiscardContents();

				if (lensflareMode == LensflareStyle34.Anamorphic)
				{
					for (int itera = 0; itera < this.hollywoodFlareBlurIterations; itera++)
					{
						this.separableBlurMaterial.SetVector("offsets",
							new Vector4((this.hollyStretchWidth * 2.0f / widthOverHeight) * oneOverBaseSize,
							0.0f, 0.0f, 0.0f));
						Graphics.Blit(secondQuarterRezColor, thirdQuarterRezColor, this.separableBlurMaterial);
						secondQuarterRezColor.DiscardContents();

						this.separableBlurMaterial.SetVector("offsets",
							new Vector4((this.hollyStretchWidth * 2.0f / widthOverHeight) * oneOverBaseSize,
							0.0f, 0.0f, 0.0f));
						Graphics.Blit(thirdQuarterRezColor, secondQuarterRezColor, this.separableBlurMaterial);
						thirdQuarterRezColor.DiscardContents();
					}

					this.AddTo(1.0f, secondQuarterRezColor, quarterRezColor);
					secondQuarterRezColor.DiscardContents();
				}
				else
				{
					// (c) combined
					for (int ix = 0; ix < this.hollywoodFlareBlurIterations; ix++)
					{
						this.separableBlurMaterial.SetVector("offsets",
							new Vector4((this.hollyStretchWidth * 2.0f / widthOverHeight) * oneOverBaseSize,
							0.0f, 0.0f, 0.0f));
						Graphics.Blit(secondQuarterRezColor, thirdQuarterRezColor, this.separableBlurMaterial);
						secondQuarterRezColor.DiscardContents();

						this.separableBlurMaterial.SetVector("offsets",
							new Vector4((this.hollyStretchWidth * 2.0f / widthOverHeight) * oneOverBaseSize,
							0.0f, 0.0f, 0.0f));
						Graphics.Blit(thirdQuarterRezColor, secondQuarterRezColor, this.separableBlurMaterial);
						thirdQuarterRezColor.DiscardContents();
					}

					this.Vignette(1.0f, secondQuarterRezColor, thirdQuarterRezColor);
					secondQuarterRezColor.DiscardContents();

					this.BlendFlares(thirdQuarterRezColor, secondQuarterRezColor);
					thirdQuarterRezColor.DiscardContents();

					this.AddTo(1.0f, secondQuarterRezColor, quarterRezColor);
					secondQuarterRezColor.DiscardContents();
				}
			}
		}

		// Screen blend bloom results to color buffer.
		this.screenBlend.SetFloat("_Intensity", this.bloomIntensity);
		this.screenBlend.SetTexture("_ColorBuffer", source);
		Graphics.Blit(quarterRezColor, destination, this.screenBlend, (int)realBlendMode);

		RenderTexture.ReleaseTemporary(quarterRezColor);
		RenderTexture.ReleaseTemporary(secondQuarterRezColor);
		RenderTexture.ReleaseTemporary(thirdQuarterRezColor);
	}

	void AddTo(float intensity_, RenderTexture from, RenderTexture to)
	{
		this.addBrightStuffBlendOneOneMaterial.SetFloat("_Intensity", intensity_);
		Graphics.Blit(from, to, this.addBrightStuffBlendOneOneMaterial);
	}

	void BlendFlares(RenderTexture from, RenderTexture to)
	{
		this.lensFlareMaterial.SetVector("colorA", new Vector4(
			this.flareColorA.r, this.flareColorA.g, this.flareColorA.b, this.flareColorA.a) * this.lensflareIntensity);
		this.lensFlareMaterial.SetVector("colorB", new Vector4(
			this.flareColorB.r, this.flareColorB.g, this.flareColorB.b, this.flareColorB.a) * this.lensflareIntensity);
		this.lensFlareMaterial.SetVector("colorC", new Vector4(
			this.flareColorC.r, this.flareColorC.g, this.flareColorC.b, this.flareColorC.a) * this.lensflareIntensity);
		this.lensFlareMaterial.SetVector("colorD", new Vector4(
			this.flareColorD.r, this.flareColorD.g, this.flareColorD.b, this.flareColorD.a) * this.lensflareIntensity);
		Graphics.Blit(from, to, this.lensFlareMaterial);
	}

	void BrightFilter(float thresh, float useAlphaAsMask, RenderTexture from, RenderTexture to)
	{
		if (this.doHdr)
		{
			this.brightPassFilterMaterial.SetVector("threshhold",
				new Vector4(thresh, 1.0f, 0.0f, 0.0f));
		}
		else
		{
			this.brightPassFilterMaterial.SetVector("threshhold",
				new Vector4(thresh, 1.0f / (1.0f - thresh), 0.0f, 0.0f));
		}

		this.brightPassFilterMaterial.SetFloat("useSrcAlphaAsMask", useAlphaAsMask);
		Graphics.Blit(from, to, this.brightPassFilterMaterial);
	}

	void Vignette(float amount, RenderTexture from, RenderTexture to)
	{
		if (this.lensFlareVignetteMask)
		{
			this.screenBlend.SetTexture("_ColorBuffer", this.lensFlareVignetteMask);
			Graphics.Blit(from, to, this.screenBlend, 3);
		}
		else
		{
			this.vignetteMaterial.SetFloat("vignetteIntensity", amount);
			Graphics.Blit(from, to, this.vignetteMaterial);
		}
	}
}
