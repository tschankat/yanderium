using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Rendering/Screen Space Ambient Obscurance")]
public class AmbientObscurance : PostEffectsBase
{
	[Range(0.0f, 3.0f)]
	public float intensity = 0.50f;
	[Range(0.10f, 3.0f)]
	public float radius = 0.20f;
	[Range(0.0f, 3.0f)]
	public int blurIterations = 1;
	[Range(0.0f, 5.0f)]
	public float blurFilterDistance = 1.25f;
	[Range(0, 1)]
	public int downsample = 0;

	public Texture2D rand;
	public Shader aoShader;

	private Material aoMaterial = null;

	protected override bool CheckResources()
	{
		this.CheckSupport(true);

		this.aoMaterial = this.CheckShaderAndCreateMaterial(this.aoShader, this.aoMaterial);

		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}

		return this.isSupported;
	}

	void OnDisable()
	{
		if (this.aoMaterial)
		{
			DestroyImmediate(this.aoMaterial);
		}

		this.aoMaterial = null;
	}

	[ImageEffectOpaque]
	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
			return;
		}

		Matrix4x4 P = this.GetComponent<Camera>().projectionMatrix;
		Matrix4x4 invP = P.inverse;
		Vector4 projInfo = new Vector4
			((-2.0f / (Screen.width * P[0])),
			 (-2.0f / (Screen.height * P[5])),
			 ((1.0f - P[2]) / P[0]),
			 ((1.0f + P[6]) / P[5]));

		this.aoMaterial.SetVector("_ProjInfo", projInfo); // used for unprojection
		this.aoMaterial.SetMatrix("_ProjectionInv", invP); // only used for reference
		this.aoMaterial.SetTexture("_Rand", this.rand); // not needed for DX11 :)
		this.aoMaterial.SetFloat("_Radius", this.radius);
		this.aoMaterial.SetFloat("_Radius2", this.radius * this.radius);
		this.aoMaterial.SetFloat("_Intensity", this.intensity);
		this.aoMaterial.SetFloat("_BlurFilterDistance", this.blurFilterDistance);

		int rtW = source.width;
		int rtH = source.height;

		RenderTexture tmpRt = RenderTexture.GetTemporary(rtW >> this.downsample, rtH >> this.downsample);
		RenderTexture tmpRt2;

		Graphics.Blit(source, tmpRt, this.aoMaterial, 0);

		if (this.downsample > 0)
		{
			tmpRt2 = RenderTexture.GetTemporary(rtW, rtH);
			Graphics.Blit(tmpRt, tmpRt2, this.aoMaterial, 4);
			RenderTexture.ReleaseTemporary(tmpRt);
			tmpRt = tmpRt2;

			// @NOTE: it's probably worth a shot to blur in low resolution 
			// instead with a blit-upsample afterwards...
		}

		for (int i = 0; i < blurIterations; i++)
		{
			this.aoMaterial.SetVector("_Axis", new Vector2(1.0f, 0.0f));
			tmpRt2 = RenderTexture.GetTemporary(rtW, rtH);
			Graphics.Blit(tmpRt, tmpRt2, this.aoMaterial, 1);
			RenderTexture.ReleaseTemporary(tmpRt);

			this.aoMaterial.SetVector("_Axis", new Vector2(0.0f, 1.0f));
			tmpRt = RenderTexture.GetTemporary(rtW, rtH);
			Graphics.Blit(tmpRt2, tmpRt, this.aoMaterial, 1);
			RenderTexture.ReleaseTemporary(tmpRt2);
		}

		this.aoMaterial.SetTexture("_AOTex", tmpRt);
		Graphics.Blit(source, destination, this.aoMaterial, 2);

		RenderTexture.ReleaseTemporary(tmpRt);
	}
}
