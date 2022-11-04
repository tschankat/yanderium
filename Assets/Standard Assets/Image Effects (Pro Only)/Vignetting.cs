using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Camera/Vignette and Chromatic Aberration")]
public class Vignetting /* And Chromatic Aberration */ : PostEffectsBase
{
	public enum AberrationMode
	{
		Simple = 0,
		Advanced = 1,
	}

	public AberrationMode mode = AberrationMode.Simple;

	public float intensity = 0.375f; // intensity == 0 disables pre pass (optimization)
	public float chromaticAberration = 0.2f;
	public float axialAberration = 0.5f;

	public float blur = 0.0f; // blur == 0 disables blur pass (optimization)
	public float blurSpread = 0.75f;

	public float luminanceDependency = 0.25f;

	public float blurDistance = 2.5f;

	public Shader vignetteShader;
	private Material vignetteMaterial;

	public Shader separableBlurShader;
	private Material separableBlurMaterial;

	public Shader chromAberrationShader;
	private Material chromAberrationMaterial;

	protected override bool CheckResources()
	{
		this.CheckSupport(false);

		this.vignetteMaterial = this.CheckShaderAndCreateMaterial(
			this.vignetteShader, this.vignetteMaterial);
		this.separableBlurMaterial = this.CheckShaderAndCreateMaterial(
			this.separableBlurShader, this.separableBlurMaterial);
		this.chromAberrationMaterial = this.CheckShaderAndCreateMaterial(
			this.chromAberrationShader, this.chromAberrationMaterial);

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

		int rtW = source.width;
		int rtH = source.height;

		bool doPrepass = (Mathf.Abs(this.blur) > 0.0f) || (Mathf.Abs(this.intensity) > 0.0f);

		float widthOverHeight = (float)rtW / rtH;
		float oneOverBaseSize = 1.0f / 512.0f;

		RenderTexture color = null;
		RenderTexture color2a = null;
		RenderTexture color2b = null;

		if (doPrepass)
		{
			color = RenderTexture.GetTemporary(rtW, rtH, 0, source.format);

			// Blur corners
			if (Mathf.Abs(this.blur) > 0.0f)
			{
				color2a = RenderTexture.GetTemporary(rtW / 2, rtH / 2, 0, source.format);

				Graphics.Blit(source, color2a, this.chromAberrationMaterial, 0);

				for (int i = 0; i < 2; i++) // maybe make iteration count tweakable
				{
					this.separableBlurMaterial.SetVector("offsets",
						new Vector4(0.0f, this.blurSpread * oneOverBaseSize, 0.0f, 0.0f));
					color2b = RenderTexture.GetTemporary(rtW / 2, rtH / 2, 0, source.format);
					Graphics.Blit(color2a, color2b, this.separableBlurMaterial);
					RenderTexture.ReleaseTemporary(color2a);

					this.separableBlurMaterial.SetVector("offsets",
						new Vector4(this.blurSpread * oneOverBaseSize / widthOverHeight, 0.0f, 0.0f, 0.0f));
					color2a = RenderTexture.GetTemporary(rtW / 2, rtH / 2, 0, source.format);
					Graphics.Blit(color2b, color2a, this.separableBlurMaterial);
					RenderTexture.ReleaseTemporary(color2b);
				}
			}

			this.vignetteMaterial.SetFloat("_Intensity", this.intensity);         // intensity for vignette
			this.vignetteMaterial.SetFloat("_Blur", this.blur);                   // blur intensity
			this.vignetteMaterial.SetTexture("_VignetteTex", color2a);   // blurred texture

			Graphics.Blit(source, color, this.vignetteMaterial, 0);      // prepass blit: darken & blur corners
		}

		this.chromAberrationMaterial.SetFloat("_ChromaticAberration", this.chromaticAberration);
		this.chromAberrationMaterial.SetFloat("_AxialAberration", this.axialAberration);
		this.chromAberrationMaterial.SetVector("_BlurDistance", new Vector2(-this.blurDistance, this.blurDistance));
		this.chromAberrationMaterial.SetFloat("_Luminance", 1.0f / Mathf.Max(Mathf.Epsilon, this.luminanceDependency));

		if (doPrepass)
		{
			color.wrapMode = TextureWrapMode.Clamp;
		}
		else
		{
			source.wrapMode = TextureWrapMode.Clamp;
		}

		Graphics.Blit(doPrepass ? color : source, destination, this.chromAberrationMaterial,
			(this.mode == AberrationMode.Advanced) ? 2 : 1);

		RenderTexture.ReleaseTemporary(color);
		RenderTexture.ReleaseTemporary(color2a);
	}
}
