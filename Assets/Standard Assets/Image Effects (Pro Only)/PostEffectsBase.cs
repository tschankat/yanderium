using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class PostEffectsBase : MonoBehaviour
{
	protected bool supportHDRTextures = true;
	protected bool supportDX11 = false;
	protected bool isSupported = true;

	protected Material CheckShaderAndCreateMaterial(Shader s, Material m2Create)
	{
		if (!s)
		{
			Debug.Log("Missing shader in " + this.ToString());
			this.enabled = false;
			return null;
		}

		if (s.isSupported && m2Create && (m2Create.shader == s))
		{
			return m2Create;
		}

		if (!s.isSupported)
		{
			this.NotSupported();
			Debug.Log("The shader " + s.ToString() + " on effect " + this.ToString() +
				" is not supported on this platform!");
			return null;
		}
		else
		{
			m2Create = new Material(s);
			m2Create.hideFlags = HideFlags.DontSave;
			return m2Create ? m2Create : null;
		}
	}

	protected Material CreateMaterial(Shader s, Material m2Create)
	{
		if (!s)
		{
			Debug.Log("Missing shader in " + this.ToString());
			return null;
		}

		if (m2Create && (m2Create.shader == s) && s.isSupported)
		{
			return m2Create;
		}

		if (!s.isSupported)
		{
			return null;
		}
		else
		{
			m2Create = new Material(s);
			m2Create.hideFlags = HideFlags.DontSave;
			return m2Create ? m2Create : null;
		}
	}

	void OnEnable()
	{
		this.isSupported = true;
	}

	bool CheckSupport()
	{
		return this.CheckSupport(false);
	}

	protected virtual bool CheckResources()
	{
		Debug.LogWarning("CheckResources() for " + this.ToString() + " should be overwritten.");
		return this.isSupported;
	}

	protected virtual void Start()
	{
		this.CheckResources();
	}

	protected bool CheckSupport(bool needDepth)
	{
		this.isSupported = true;
		this.supportHDRTextures = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf);
		this.supportDX11 = (SystemInfo.graphicsShaderLevel >= 50) && SystemInfo.supportsComputeShaders;

		// [af] SystemInfo.supportsRenderTextures always returns true in Unity 5.
		if (!SystemInfo.supportsImageEffects /*|| !SystemInfo.supportsRenderTextures*/)
		{
			this.NotSupported();
			return false;
		}

		if (needDepth && !SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
		{
			this.NotSupported();
			return false;
		}

		if (needDepth)
		{
			this.GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;
		}

		return true;
	}

	protected bool CheckSupport(bool needDepth, bool needHdr)
	{
		if (!this.CheckSupport(needDepth))
		{
			return false;
		}

		if (needHdr && !this.supportHDRTextures)
		{
			this.NotSupported();
			return false;
		}

		return true;
	}

	public bool Dx11Support()
	{
		return this.supportDX11;
	}

	protected void ReportAutoDisable()
	{
		Debug.LogWarning("The image effect " + this.ToString() +
			" has been disabled as it's not supported on the current platform.");
	}

	// Deprecated but needed for old effects to survive upgrading.
	bool CheckShader(Shader s)
	{
		Debug.Log("The shader " + s.ToString() + " on effect " + this.ToString() +
			" is not part of the Unity 3.2+ effects suite anymore." + " " +
			"For best performance and quality, please ensure you are using the latest" + " " +
			"Standard Assets Image Effects (Pro only) package.");

		if (!s.isSupported)
		{
			this.NotSupported();
			return false;
		}
		else
		{
			return false;
		}
	}

	protected void NotSupported()
	{
		this.enabled = false;
		this.isSupported = false;
	}

	protected void DrawBorder(RenderTexture dest, Material material)
	{
		float x1;
		float x2;
		float y1;
		float y2;

		RenderTexture.active = dest;
		bool invertY = true; // source.texelSize.y < 0.0f;

		// Set up the simple Matrix.
		GL.PushMatrix();
		GL.LoadOrtho();

		for (int i = 0; i < material.passCount; i++)
		{
			material.SetPass(i);

			float y1_;
			float y2_;

			if (invertY)
			{
				y1_ = 1.0f;
				y2_ = 0.0f;
			}
			else
			{
				y1_ = 0.0f;
				y2_ = 1.0f;
			}

			// Left.
			x1 = 0.0f;
			x2 = 0.0f + (1.0f / (dest.width * 1.0f));
			y1 = 0.0f;
			y2 = 1.0f;
			GL.Begin(GL.QUADS);

			GL.TexCoord2(0.0f, y1_);
			GL.Vertex3(x1, y1, 0.10f);
			GL.TexCoord2(1.0f, y1_);
			GL.Vertex3(x2, y1, 0.10f);
			GL.TexCoord2(1.0f, y2_);
			GL.Vertex3(x2, y2, 0.10f);
			GL.TexCoord2(0.0f, y2_);
			GL.Vertex3(x1, y2, 0.10f);

			// Right.
			x1 = 1.0f - (1.0f / (dest.width * 1.0f));
			x2 = 1.0f;
			y1 = 0.0f;
			y2 = 1.0f;

			GL.TexCoord2(0.0f, y1_);
			GL.Vertex3(x1, y1, 0.10f);
			GL.TexCoord2(1.0f, y1_);
			GL.Vertex3(x2, y1, 0.10f);
			GL.TexCoord2(1.0f, y2_);
			GL.Vertex3(x2, y2, 0.10f);
			GL.TexCoord2(0.0f, y2_);
			GL.Vertex3(x1, y2, 0.10f);

			// top
			x1 = 0.0f;
			x2 = 1.0f;
			y1 = 0.0f;
			y2 = 0.0f + (1.0f / (dest.height * 1.0f));

			GL.TexCoord2(0.0f, y1_);
			GL.Vertex3(x1, y1, 0.10f);
			GL.TexCoord2(1.0f, y1_);
			GL.Vertex3(x2, y1, 0.10f);
			GL.TexCoord2(1.0f, y2_);
			GL.Vertex3(x2, y2, 0.10f);
			GL.TexCoord2(0.0f, y2_);
			GL.Vertex3(x1, y2, 0.10f);

			// Bottom.
			x1 = 0.0f;
			x2 = 1.0f;
			y1 = 1.0f - (1.0f / (dest.height * 1.0f));
			y2 = 1.0f;

			GL.TexCoord2(0.0f, y1_);
			GL.Vertex3(x1, y1, 0.10f);
			GL.TexCoord2(1.0f, y1_);
			GL.Vertex3(x2, y1, 0.10f);
			GL.TexCoord2(1.0f, y2_);
			GL.Vertex3(x2, y2, 0.10f);
			GL.TexCoord2(0.0f, y2_);
			GL.Vertex3(x1, y2, 0.10f);

			GL.End();
		}

		GL.PopMatrix();
	}
}
