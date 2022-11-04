using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Color Adjustments/Color Correction (Curves, Saturation)")]
public class ColorCorrectionCurves : PostEffectsBase
{
	public enum ColorCorrectionMode
	{
		Simple = 0,
		Advanced = 1
	}

	public AnimationCurve redChannel;
	public AnimationCurve greenChannel;
	public AnimationCurve blueChannel;

	public bool useDepthCorrection = false;

	public AnimationCurve zCurve;
	public AnimationCurve depthRedChannel;
	public AnimationCurve depthGreenChannel;
	public AnimationCurve depthBlueChannel;

	private Material ccMaterial;
	private Material ccDepthMaterial;
	private Material selectiveCcMaterial;

	private Texture2D rgbChannelTex;
	private Texture2D rgbDepthChannelTex;
	private Texture2D zCurveTex;

	public float saturation = 1.0f;

	public bool selectiveCc = false;

	public Color selectiveFromColor = Color.white;
	public Color selectiveToColor = Color.white;

	public ColorCorrectionMode mode;

	public bool updateTextures = true;

	public Shader colorCorrectionCurvesShader = null;
	public Shader simpleColorCorrectionCurvesShader = null;
	public Shader colorCorrectionSelectiveShader = null;

	private bool updateTexturesOnStartup = true;

	protected override void Start()
	{
		base.Start();
		this.updateTexturesOnStartup = true;
	}

	void Awake()
	{
		// Do nothing.
	}

	protected override bool CheckResources()
	{
		this.CheckSupport(this.mode == ColorCorrectionMode.Advanced);

		this.ccMaterial = this.CheckShaderAndCreateMaterial(
			this.simpleColorCorrectionCurvesShader, this.ccMaterial);
		this.ccDepthMaterial = this.CheckShaderAndCreateMaterial(
			this.colorCorrectionCurvesShader, this.ccDepthMaterial);
		this.selectiveCcMaterial = this.CheckShaderAndCreateMaterial(
			this.colorCorrectionSelectiveShader, this.selectiveCcMaterial);

		if (!this.rgbChannelTex)
		{
			this.rgbChannelTex = new Texture2D(256, 4, TextureFormat.ARGB32, false, true);
		}

		if (!this.rgbDepthChannelTex)
		{
			this.rgbDepthChannelTex = new Texture2D(256, 4, TextureFormat.ARGB32, false, true);
		}

		if (!this.zCurveTex)
		{
			this.zCurveTex = new Texture2D(256, 1, TextureFormat.ARGB32, false, true);
		}

		this.rgbChannelTex.hideFlags = HideFlags.DontSave;
		this.rgbDepthChannelTex.hideFlags = HideFlags.DontSave;
		this.zCurveTex.hideFlags = HideFlags.DontSave;

		this.rgbChannelTex.wrapMode = TextureWrapMode.Clamp;
		this.rgbDepthChannelTex.wrapMode = TextureWrapMode.Clamp;
		this.zCurveTex.wrapMode = TextureWrapMode.Clamp;

		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}

		return this.isSupported;
	}

	public void UpdateParameters()
	{
		this.CheckResources(); // textures might not be created if we're tweaking UI while disabled

		if ((this.redChannel != null) && (this.greenChannel != null) && (this.blueChannel != null))
		{
			for (float i = 0.0f; i <= 1.0f; i += 1.0f / 255.0f)
			{
				float rCh = Mathf.Clamp(this.redChannel.Evaluate(i), 0.0f, 1.0f);
				float gCh = Mathf.Clamp(this.greenChannel.Evaluate(i), 0.0f, 1.0f);
				float bCh = Mathf.Clamp(this.blueChannel.Evaluate(i), 0.0f, 1.0f);

				this.rgbChannelTex.SetPixel((int)Mathf.Floor(i * 255.0f), 0, new Color(rCh, rCh, rCh));
				this.rgbChannelTex.SetPixel((int)Mathf.Floor(i * 255.0f), 1, new Color(gCh, gCh, gCh));
				this.rgbChannelTex.SetPixel((int)Mathf.Floor(i * 255.0f), 2, new Color(bCh, bCh, bCh));

				float zC = Mathf.Clamp(this.zCurve.Evaluate(i), 0.0f, 1.0f);

				this.zCurveTex.SetPixel((int)Mathf.Floor(i * 255.0f), 0, new Color(zC, zC, zC));

				rCh = Mathf.Clamp(this.depthRedChannel.Evaluate(i), 0.0f, 1.0f);
				gCh = Mathf.Clamp(this.depthGreenChannel.Evaluate(i), 0.0f, 1.0f);
				bCh = Mathf.Clamp(this.depthBlueChannel.Evaluate(i), 0.0f, 1.0f);

				this.rgbDepthChannelTex.SetPixel((int)Mathf.Floor(i * 255.0f), 0, new Color(rCh, rCh, rCh));
				this.rgbDepthChannelTex.SetPixel((int)Mathf.Floor(i * 255.0f), 1, new Color(gCh, gCh, gCh));
				this.rgbDepthChannelTex.SetPixel((int)Mathf.Floor(i * 255.0f), 2, new Color(bCh, bCh, bCh));
			}

			this.rgbChannelTex.Apply();
			this.rgbDepthChannelTex.Apply();
			this.zCurveTex.Apply();
		}
	}

	public void UpdateTextures()
	{
		this.UpdateParameters();
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
			return;
		}

		if (this.updateTexturesOnStartup)
		{
			this.UpdateParameters();
			this.updateTexturesOnStartup = false;
		}

		if (this.useDepthCorrection)
			this.GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;

		RenderTexture renderTarget2Use = destination;

		if (this.selectiveCc)
		{
			renderTarget2Use = RenderTexture.GetTemporary(source.width, source.height);
		}

		if (this.useDepthCorrection)
		{
			this.ccDepthMaterial.SetTexture("_RgbTex", this.rgbChannelTex);
			this.ccDepthMaterial.SetTexture("_ZCurve", this.zCurveTex);
			this.ccDepthMaterial.SetTexture("_RgbDepthTex", this.rgbDepthChannelTex);
			this.ccDepthMaterial.SetFloat("_Saturation", this.saturation);

			Graphics.Blit(source, renderTarget2Use, this.ccDepthMaterial);
		}
		else
		{
			this.ccMaterial.SetTexture("_RgbTex", this.rgbChannelTex);
			this.ccMaterial.SetFloat("_Saturation", this.saturation);

			Graphics.Blit(source, renderTarget2Use, this.ccMaterial);
		}

		if (this.selectiveCc)
		{
			this.selectiveCcMaterial.SetColor("selColor", this.selectiveFromColor);
			this.selectiveCcMaterial.SetColor("targetColor", this.selectiveToColor);
			Graphics.Blit(renderTarget2Use, destination, this.selectiveCcMaterial);

			RenderTexture.ReleaseTemporary(renderTarget2Use);
		}
	}
}
