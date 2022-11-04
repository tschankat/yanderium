using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Blur/Blur (Optimized)")]
public class Blur : PostEffectsBase
{
	public enum BlurType
	{
		StandardGauss = 0,
		SgxGauss = 1,
	}

	[Range(0, 2)]
	public int downsample = 1;

	[Range(0.0f, 10.0f)]
	public float blurSize = 3.0f;

	[Range(1, 4)]
	public int blurIterations = 2;

	public BlurType blurType = BlurType.StandardGauss;

	public Shader blurShader;
	private Material blurMaterial = null;

	protected override bool CheckResources()
	{
		this.CheckSupport(false);

		this.blurMaterial = this.CheckShaderAndCreateMaterial(this.blurShader, this.blurMaterial);

		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}

		return this.isSupported;
	}

	void OnDisable()
	{
		if (this.blurMaterial)
		{
			DestroyImmediate(this.blurMaterial);
		}
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
			return;
		}

		float widthMod = 1.0f / (1 << downsample);

		this.blurMaterial.SetVector("_Parameter",
			new Vector4(this.blurSize * widthMod, -this.blurSize * widthMod, 0.0f, 0.0f));
		source.filterMode = FilterMode.Bilinear;

		int rtW = source.width >> downsample;
		int rtH = source.height >> downsample;

		// Downsample.
		RenderTexture rt = RenderTexture.GetTemporary(rtW, rtH, 0, source.format);

		rt.filterMode = FilterMode.Bilinear;
		Graphics.Blit(source, rt, this.blurMaterial, 0);

		int passOffs = (blurType == BlurType.StandardGauss) ? 0 : 2;

		for (int i = 0; i < this.blurIterations; i++)
		{
			float iterationOffs = (float)i;
			blurMaterial.SetVector("_Parameter", new Vector4(
				(this.blurSize * widthMod) + iterationOffs, (-this.blurSize * widthMod) - iterationOffs, 0.0f, 0.0f));

			// Vertical blur.
			RenderTexture rt2 = RenderTexture.GetTemporary(rtW, rtH, 0, source.format);
			rt2.filterMode = FilterMode.Bilinear;
			Graphics.Blit(rt, rt2, this.blurMaterial, 1 + passOffs);
			RenderTexture.ReleaseTemporary(rt);
			rt = rt2;

			// Horizontal blur.
			rt2 = RenderTexture.GetTemporary(rtW, rtH, 0, source.format);
			rt2.filterMode = FilterMode.Bilinear;
			Graphics.Blit(rt, rt2, this.blurMaterial, 2 + passOffs);
			RenderTexture.ReleaseTemporary(rt);
			rt = rt2;
		}

		Graphics.Blit(rt, destination);

		RenderTexture.ReleaseTemporary(rt);
	}
}
