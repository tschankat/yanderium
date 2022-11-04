using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Noise/Noise And Grain (Filmic)")]
public class NoiseAndGrain : PostEffectsBase
{
	public float intensityMultiplier = 0.25f;

	public float generalIntensity = 0.50f;
	public float blackIntensity = 1.0f;
	public float whiteIntensity = 1.0f;
	public float midGrey = 0.20f;

	public bool dx11Grain = false;
	public float softness = 0.0f;
	public bool monochrome = false;

	public Vector3 intensities = new Vector3(1.0f, 1.0f, 1.0f);
	public Vector3 tiling = new Vector3(64.0f, 64.0f, 64.0f);
	public float monochromeTiling = 64.0f;

	public FilterMode filterMode = FilterMode.Bilinear;

	public Texture2D noiseTexture;

	public Shader noiseShader;
	private Material noiseMaterial = null;

	public Shader dx11NoiseShader;
	private Material dx11NoiseMaterial = null;

	const float TILE_AMOUNT = 64.0f;

	protected override bool CheckResources()
	{
		this.CheckSupport(false);

		this.noiseMaterial = this.CheckShaderAndCreateMaterial(this.noiseShader, this.noiseMaterial);

		if (this.dx11Grain && this.supportDX11)
		{
#if UNITY_EDITOR
			this.dx11NoiseShader = Shader.Find("Hidden/NoiseAndGrainDX11");
#endif
			this.dx11NoiseMaterial = this.CheckShaderAndCreateMaterial(this.dx11NoiseShader, this.dx11NoiseMaterial);
		}

		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}

		return this.isSupported;
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources() || (null == this.noiseTexture))
		{
			Graphics.Blit(source, destination);

			if (null == this.noiseTexture)
			{
				Debug.LogWarning("Noise & Grain effect failing as noise texture is not assigned. Please assign.", this.transform);
			}

			return;
		}

		this.softness = Mathf.Clamp(this.softness, 0.0f, 0.99f);

		if (this.dx11Grain && this.supportDX11)
		{
			// We have a fancy, procedural noise pattern in this version, so no texture needed

			this.dx11NoiseMaterial.SetFloat("_DX11NoiseTime", Time.frameCount);
			this.dx11NoiseMaterial.SetTexture("_NoiseTex", this.noiseTexture);
			this.dx11NoiseMaterial.SetVector("_NoisePerChannel", this.monochrome ? Vector3.one : this.intensities);
			this.dx11NoiseMaterial.SetVector("_MidGrey",
				new Vector3(this.midGrey, 1.0f / (1.0f - this.midGrey), -1.0f / this.midGrey));
			this.dx11NoiseMaterial.SetVector("_NoiseAmount",
				new Vector3(this.generalIntensity, this.blackIntensity, this.whiteIntensity) * this.intensityMultiplier);

			if (this.softness > Mathf.Epsilon)
			{
				RenderTexture rt = RenderTexture.GetTemporary(
					(int)(source.width * (1.0f - this.softness)),
					(int)(source.height * (1.0f - this.softness)));
				DrawNoiseQuadGrid(source, rt, this.dx11NoiseMaterial, this.noiseTexture, this.monochrome ? 3 : 2);
				this.dx11NoiseMaterial.SetTexture("_NoiseTex", rt);
				Graphics.Blit(source, destination, this.dx11NoiseMaterial, 4);
				RenderTexture.ReleaseTemporary(rt);
			}
			else
			{
				DrawNoiseQuadGrid(source, destination, this.dx11NoiseMaterial,
					this.noiseTexture, this.monochrome ? 1 : 0);
			}
		}
		else
		{
			// normal noise (DX9 style)

			if (this.noiseTexture)
			{
				this.noiseTexture.wrapMode = TextureWrapMode.Repeat;
				this.noiseTexture.filterMode = filterMode;
			}

			this.noiseMaterial.SetTexture("_NoiseTex", this.noiseTexture);
			this.noiseMaterial.SetVector("_NoisePerChannel", this.monochrome ? Vector3.one : intensities);
			this.noiseMaterial.SetVector("_NoiseTilingPerChannel",
				this.monochrome ? (Vector3.one * this.monochromeTiling) : this.tiling);
			this.noiseMaterial.SetVector("_MidGrey",
				new Vector3(this.midGrey, 1.0f / (1.0f - this.midGrey), -1.0f / this.midGrey));
			this.noiseMaterial.SetVector("_NoiseAmount",
				new Vector3(this.generalIntensity, this.blackIntensity, this.whiteIntensity) * this.intensityMultiplier);

			if (this.softness > Mathf.Epsilon)
			{
				RenderTexture rt2 = RenderTexture.GetTemporary(
					(int)(source.width * (1.0f - this.softness)),
					(int)(source.height * (1.0f - this.softness)));
				DrawNoiseQuadGrid(source, rt2, this.noiseMaterial, this.noiseTexture, 2);
				this.noiseMaterial.SetTexture("_NoiseTex", rt2);
				Graphics.Blit(source, destination, this.noiseMaterial, 1);
				RenderTexture.ReleaseTemporary(rt2);
			}
			else
			{
				DrawNoiseQuadGrid(source, destination, this.noiseMaterial, this.noiseTexture, 0);
			}
		}
	}

	static void DrawNoiseQuadGrid(RenderTexture source, RenderTexture dest, Material fxMaterial,
		Texture2D noise, int passNr)
	{
		RenderTexture.active = dest;

		float noiseSize = noise.width;
		float subDs = source.width / TILE_AMOUNT;

		fxMaterial.SetTexture("_MainTex", source);

		GL.PushMatrix();
		GL.LoadOrtho();

		float aspectCorrection = (float)source.width / source.height;
		float stepSizeX = 1.0f / subDs;
		float stepSizeY = stepSizeX * aspectCorrection;
		float texTile = noiseSize / noise.width;

		fxMaterial.SetPass(passNr);

		GL.Begin(GL.QUADS);

		for (float x1 = 0.0f; x1 < 1.0f; x1 += stepSizeX)
		{
			for (float y1 = 0.0f; y1 < 1.0f; y1 += stepSizeY)
			{
				float tcXStart = Random.Range(0.0f, 1.0f);
				float tcYStart = Random.Range(0.0f, 1.0f);

				//var v3 : Vector3 = Random.insideUnitSphere; 
				//var c : Color = new Color(v3.x, v3.y, v3.z);

				tcXStart = Mathf.Floor(tcXStart * noiseSize) / noiseSize;
				tcYStart = Mathf.Floor(tcYStart * noiseSize) / noiseSize;

				float texTileMod = 1.0f / noiseSize;

				GL.MultiTexCoord2(0, tcXStart, tcYStart);
				GL.MultiTexCoord2(1, 0.0f, 0.0f);
				//GL.Color( c );
				GL.Vertex3(x1, y1, 0.10f);
				GL.MultiTexCoord2(0, tcXStart + texTile * texTileMod, tcYStart);
				GL.MultiTexCoord2(1, 1.0f, 0.0f);
				//GL.Color( c );
				GL.Vertex3(x1 + stepSizeX, y1, 0.10f);
				GL.MultiTexCoord2(0, tcXStart + texTile * texTileMod, tcYStart + texTile * texTileMod);
				GL.MultiTexCoord2(1, 1.0f, 1.0f);
				//GL.Color( c );
				GL.Vertex3(x1 + stepSizeX, y1 + stepSizeY, 0.10f);
				GL.MultiTexCoord2(0, tcXStart, tcYStart + texTile * texTileMod);
				GL.MultiTexCoord2(1, 0.0f, 1.0f);
				//GL.Color( c );
				GL.Vertex3(x1, y1 + stepSizeY, 0.10f);
			}
		}

		GL.End();
		GL.PopMatrix();
	}
}
