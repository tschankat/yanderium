using UnityEngine;

// [af] Maybe I didn't need to convert this file after all.

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Camera/Camera Motion Blur")]
public class CameraMotionBlur : PostEffectsBase
{
	// Make sure to match this to MAX_RADIUS in shader ('k' in paper)
	const float MAX_RADIUS = 10.0f;

	public enum MotionBlurFilter
	{
		CameraMotion = 0,           // global screen blur based on cam motion
		LocalBlur = 1,              // cheap blur, no dilation or scattering
		Reconstruction = 2,         // advanced filter (simulates scattering) as in plausible motion blur paper
		ReconstructionDX11 = 3,     // advanced filter (simulates scattering) as in plausible motion blur paper
		ReconstructionDisc = 4,     // advanced filter using scaled poisson disc sampling
	}

	// settings
	public MotionBlurFilter filterType = MotionBlurFilter.Reconstruction;
	public bool preview = false;                // show how blur would look like in action ...
	public Vector3 previewScale = Vector3.one;  // ... given this movement vector

	// params
	public float movementScale = 0.0f;
	public float rotationScale = 1.0f;
	public float maxVelocity = 8.0f;    // maximum velocity in pixels
	public float minVelocity = 0.1f;    // minimum velocity in pixels
	public float velocityScale = 0.375f;    // global velocity scale
	public float softZDistance = 0.005f;    // for z overlap check softness (reconstruction filter only)
	public int velocityDownsample = 1;  // low resolution velocity buffer? (optimization)
	public LayerMask excludeLayers = 0;
	//public var dynamicLayers : LayerMask = 0;
	private GameObject tmpCam = null;

	// resources
	public Shader shader;
	public Shader dx11MotionBlurShader;
	public Shader replacementClear;
	//public var replacementDynamics : Shader;
	private Material motionBlurMaterial = null;
	private Material dx11MotionBlurMaterial = null;

	public Texture2D noiseTexture = null;
	public float jitter = 0.05f;

	// (internal) debug
	public bool showVelocity = false;
	public float showVelocityScale = 1.0f;

	// camera transforms
	private Matrix4x4 currentViewProjMat;
	private Matrix4x4 prevViewProjMat;
	private int prevFrameCount;
	private bool wasActive;
	// shortcuts to calculate global blur direction when using 'CameraMotion'
	private Vector3 prevFrameForward = Vector3.forward;
	private Vector3 prevFrameRight = Vector3.right;
	private Vector3 prevFrameUp = Vector3.up;
	private Vector3 prevFramePos = Vector3.zero;

	void CalculateViewProjection()
	{
		Camera camera = this.GetComponent<Camera>();
		Matrix4x4 viewMat = camera.worldToCameraMatrix;
		Matrix4x4 projMat = GL.GetGPUProjectionMatrix(camera.projectionMatrix, true);
		this.currentViewProjMat = projMat * viewMat;
	}

	protected override void Start()
	{
		this.CheckResources();

		this.wasActive = this.gameObject.activeInHierarchy;
		this.CalculateViewProjection();
		this.Remember();
		this.wasActive = false; // hack to fake position/rotation update and prevent bad blurs.
	}

	void OnEnable()
	{
		this.GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;
	}

	void OnDisable()
	{
		if (null != this.motionBlurMaterial)
		{
			DestroyImmediate(this.motionBlurMaterial);
			this.motionBlurMaterial = null;
		}
		if (null != this.dx11MotionBlurMaterial)
		{
			DestroyImmediate(this.dx11MotionBlurMaterial);
			this.dx11MotionBlurMaterial = null;
		}
		if (null != this.tmpCam)
		{
			DestroyImmediate(this.tmpCam);
			this.tmpCam = null;
		}
	}

	protected override bool CheckResources()
	{
		this.CheckSupport(true, true); // depth & hdr needed
		this.motionBlurMaterial = this.CheckShaderAndCreateMaterial(this.shader, this.motionBlurMaterial);

		if (this.supportDX11 && (this.filterType == MotionBlurFilter.ReconstructionDX11))
		{
			this.dx11MotionBlurMaterial = this.CheckShaderAndCreateMaterial(
				this.dx11MotionBlurShader, this.dx11MotionBlurMaterial);
		}

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

		if (this.filterType == MotionBlurFilter.CameraMotion)
		{
			this.StartFrame();
		}

		// use if possible new RG format ... fallback to half otherwise
		RenderTextureFormat rtFormat = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.RGHalf) ?
			RenderTextureFormat.RGHalf : RenderTextureFormat.ARGBHalf;

		// get temp textures
		RenderTexture velBuffer = RenderTexture.GetTemporary(
			divRoundUp(source.width, velocityDownsample),
			divRoundUp(source.height, velocityDownsample),
			0,
			rtFormat);

		int tileWidth = 1;
		int tileHeight = 1;
		this.maxVelocity = Mathf.Max(2.0f, this.maxVelocity);

		float _maxVelocity = this.maxVelocity; // calculate 'k'
											   // note: 's' is hardcoded in shaders except for DX11 path

		// auto DX11 fallback!
		bool fallbackFromDX11 = false;
		if ((this.filterType == MotionBlurFilter.ReconstructionDX11) &&
			(this.dx11MotionBlurMaterial == null))
		{
			fallbackFromDX11 = true;
		}

		if ((this.filterType == MotionBlurFilter.Reconstruction) ||
			fallbackFromDX11 ||
			(this.filterType == MotionBlurFilter.ReconstructionDisc))
		{
			this.maxVelocity = Mathf.Min(this.maxVelocity, MAX_RADIUS);
			tileWidth = this.divRoundUp(velBuffer.width, (int)this.maxVelocity);
			tileHeight = this.divRoundUp(velBuffer.height, (int)this.maxVelocity);
			_maxVelocity = velBuffer.width / tileWidth;
		}
		else
		{
			tileWidth = this.divRoundUp(velBuffer.width, (int)this.maxVelocity);
			tileHeight = this.divRoundUp(velBuffer.height, (int)this.maxVelocity);
			_maxVelocity = velBuffer.width / tileWidth;
		}

		RenderTexture tileMax = RenderTexture.GetTemporary(tileWidth, tileHeight, 0, rtFormat);
		RenderTexture neighbourMax = RenderTexture.GetTemporary(tileWidth, tileHeight, 0, rtFormat);
		velBuffer.filterMode = FilterMode.Point;
		tileMax.filterMode = FilterMode.Point;
		neighbourMax.filterMode = FilterMode.Point;

		if (this.noiseTexture)
		{
			this.noiseTexture.filterMode = FilterMode.Point;
		}

		source.wrapMode = TextureWrapMode.Clamp;
		velBuffer.wrapMode = TextureWrapMode.Clamp;
		neighbourMax.wrapMode = TextureWrapMode.Clamp;
		tileMax.wrapMode = TextureWrapMode.Clamp;

		// calc correct viewprj matrix
		this.CalculateViewProjection();

		// just started up?		
		if (this.gameObject.activeInHierarchy && !this.wasActive)
		{
			this.Remember();
		}

		this.wasActive = this.gameObject.activeInHierarchy;

		// matrices
		Matrix4x4 invViewPrj = Matrix4x4.Inverse(this.currentViewProjMat);
		this.motionBlurMaterial.SetMatrix("_InvViewProj", invViewPrj);
		this.motionBlurMaterial.SetMatrix("_PrevViewProj", this.prevViewProjMat);
		this.motionBlurMaterial.SetMatrix("_ToPrevViewProjCombined", this.prevViewProjMat * invViewPrj);

		this.motionBlurMaterial.SetFloat("_MaxVelocity", _maxVelocity);
		this.motionBlurMaterial.SetFloat("_MaxRadiusOrKInPaper", _maxVelocity);
		this.motionBlurMaterial.SetFloat("_MinVelocity", this.minVelocity);
		this.motionBlurMaterial.SetFloat("_VelocityScale", this.velocityScale);
		this.motionBlurMaterial.SetFloat("_Jitter", this.jitter);

		// texture samplers
		this.motionBlurMaterial.SetTexture("_NoiseTex", this.noiseTexture);
		this.motionBlurMaterial.SetTexture("_VelTex", velBuffer);
		this.motionBlurMaterial.SetTexture("_NeighbourMaxTex", neighbourMax);
		this.motionBlurMaterial.SetTexture("_TileTexDebug", tileMax);

		Camera camera = this.GetComponent<Camera>();

		if (this.preview)
		{
			// generate an artifical 'previous' matrix to simulate blur look
			Matrix4x4 viewMat = camera.worldToCameraMatrix;
			Matrix4x4 offset = Matrix4x4.identity;
			offset.SetTRS(previewScale * (1.0f / 3.0f), Quaternion.identity, Vector3.one); // using only translation
			Matrix4x4 projMat = GL.GetGPUProjectionMatrix(camera.projectionMatrix, true);
			this.prevViewProjMat = projMat * offset * viewMat;
			this.motionBlurMaterial.SetMatrix("_PrevViewProj", this.prevViewProjMat);
			this.motionBlurMaterial.SetMatrix("_ToPrevViewProjCombined", this.prevViewProjMat * invViewPrj);
		}

		if (this.filterType == MotionBlurFilter.CameraMotion)
		{
			// build blur vector to be used in shader to create a global blur direction
			Vector4 blurVector = Vector4.zero;

			float lookUpDown = Vector3.Dot(this.transform.up, Vector3.up);
			Vector3 distanceVector = this.prevFramePos - this.transform.position;

			float distMag = distanceVector.magnitude;

			float farHeur = 1.0f;

			// pitch (vertical)
			farHeur = (Vector3.Angle(this.transform.up, this.prevFrameUp) / camera.fieldOfView) *
				(source.width * 0.75f);
			blurVector.x = this.rotationScale * farHeur;//Mathf.Clamp01((1.0f-Vector3.Dot(transform.up, prevFrameUp)));

			// yaw #1 (horizontal, faded by pitch)
			farHeur = (Vector3.Angle(this.transform.forward, this.prevFrameForward) / camera.fieldOfView) *
				(source.width * 0.75f);
			blurVector.y = this.rotationScale * lookUpDown * farHeur;//Mathf.Clamp01((1.0f-Vector3.Dot(transform.forward, prevFrameForward)));

			// yaw #2 (when looking down, faded by 1-pitch)
			farHeur = (Vector3.Angle(this.transform.forward, this.prevFrameForward) / camera.fieldOfView) *
				(source.width * 0.75f);
			blurVector.z = this.rotationScale * (1.0f - lookUpDown) * farHeur;//Mathf.Clamp01((1.0f-Vector3.Dot(transform.forward, prevFrameForward)));

			if ((distMag > Mathf.Epsilon) && (this.movementScale > Mathf.Epsilon))
			{
				// forward (probably most important)
				blurVector.w = this.movementScale * (Vector3.Dot(this.transform.forward, distanceVector)) *
					(source.width * 0.5f);
				// jump (maybe scale down further)
				blurVector.x += this.movementScale * (Vector3.Dot(this.transform.up, distanceVector)) *
					(source.width * 0.5f);
				// strafe (maybe scale down further)
				blurVector.y += this.movementScale * (Vector3.Dot(this.transform.right, distanceVector)) *
					(source.width * 0.5f);
			}

			if (this.preview) // crude approximation
			{
				motionBlurMaterial.SetVector("_BlurDirectionPacked",
					new Vector4(previewScale.y, previewScale.x, 0.0f, previewScale.z) * (0.50f * camera.fieldOfView));
			}
			else
			{
				motionBlurMaterial.SetVector("_BlurDirectionPacked", blurVector);
			}
		}
		else
		{
			// generate velocity buffer	
			Graphics.Blit(source, velBuffer, motionBlurMaterial, 0);

			// patch up velocity buffer:

			// exclude certain layers (e.g. skinned objects as we cant really support that atm)

			Camera cam = null;
			if (this.excludeLayers.value != 0)// || dynamicLayers.value)
			{
				cam = this.GetTmpCam();
			}

			if (cam && (this.excludeLayers.value != 0) &&
				this.replacementClear && this.replacementClear.isSupported)
			{
				cam.targetTexture = velBuffer;
				cam.cullingMask = this.excludeLayers;
				cam.RenderWithShader(this.replacementClear, string.Empty);
			}

			// dynamic layers (e.g. rigid bodies)
			// no worky in 4.0, but let's fix for 4.x
			/*
			if (cam && dynamicLayers.value != 0 && replacementDynamics && replacementDynamics.isSupported) {

				Shader.SetGlobalFloat ("_MaxVelocity", maxVelocity);
				Shader.SetGlobalFloat ("_VelocityScale", velocityScale);
				Shader.SetGlobalVector ("_VelBufferSize", Vector4 (velBuffer.width, velBuffer.height, 0, 0));
				Shader.SetGlobalMatrix ("_PrevViewProj", prevViewProjMat);
				Shader.SetGlobalMatrix ("_ViewProj", currentViewProjMat);

				cam.targetTexture = velBuffer;				
				cam.cullingMask = dynamicLayers;
				cam.RenderWithShader (replacementDynamics, "");
			}
			*/

		}

		if (!this.preview && (Time.frameCount != this.prevFrameCount))
		{
			// remember current transformation data for next frame
			this.prevFrameCount = Time.frameCount;
			this.Remember();
		}

		source.filterMode = FilterMode.Bilinear;

		// debug vel buffer:
		if (this.showVelocity)
		{
			// generate tile max and neighbour max		
			//Graphics.Blit (velBuffer, tileMax, motionBlurMaterial, 2);
			//Graphics.Blit (tileMax, neighbourMax, motionBlurMaterial, 3);
			this.motionBlurMaterial.SetFloat("_DisplayVelocityScale", this.showVelocityScale);
			Graphics.Blit(velBuffer, destination, this.motionBlurMaterial, 1);
		}
		else
		{
			if ((this.filterType == MotionBlurFilter.ReconstructionDX11) && !fallbackFromDX11)
			{
				// need to reset some parameters for dx11 shader
				this.dx11MotionBlurMaterial.SetFloat("_MinVelocity", this.minVelocity);
				this.dx11MotionBlurMaterial.SetFloat("_VelocityScale", this.velocityScale);
				this.dx11MotionBlurMaterial.SetFloat("_Jitter", this.jitter);

				// texture samplers
				this.dx11MotionBlurMaterial.SetTexture("_NoiseTex", this.noiseTexture);
				this.dx11MotionBlurMaterial.SetTexture("_VelTex", velBuffer);
				this.dx11MotionBlurMaterial.SetTexture("_NeighbourMaxTex", neighbourMax);

				this.dx11MotionBlurMaterial.SetFloat("_SoftZDistance", Mathf.Max(0.00025f, this.softZDistance));
				this.dx11MotionBlurMaterial.SetFloat("_MaxRadiusOrKInPaper", _maxVelocity);

				// generate tile max and neighbour max		
				Graphics.Blit(velBuffer, tileMax, this.dx11MotionBlurMaterial, 0);
				Graphics.Blit(tileMax, neighbourMax, this.dx11MotionBlurMaterial, 1);

				// final blur
				Graphics.Blit(source, destination, this.dx11MotionBlurMaterial, 2);
			}
			else if ((this.filterType == MotionBlurFilter.Reconstruction) || fallbackFromDX11)
			{
				// 'reconstructing' properly integrated color
				this.motionBlurMaterial.SetFloat("_SoftZDistance", Mathf.Max(0.00025f, this.softZDistance));

				// generate tile max and neighbour max		
				Graphics.Blit(velBuffer, tileMax, this.motionBlurMaterial, 2);
				Graphics.Blit(tileMax, neighbourMax, this.motionBlurMaterial, 3);

				// final blur
				Graphics.Blit(source, destination, this.motionBlurMaterial, 4);
			}
			else if (this.filterType == MotionBlurFilter.CameraMotion)
			{
				// orange box style motion blur
				Graphics.Blit(source, destination, this.motionBlurMaterial, 6);
			}
			else if (this.filterType == MotionBlurFilter.ReconstructionDisc)
			{
				// dof style motion blur defocuing and ellipse around the princical blur direction
				// 'reconstructing' properly integrated color
				this.motionBlurMaterial.SetFloat("_SoftZDistance", Mathf.Max(0.00025f, this.softZDistance));

				// generate tile max and neighbour max		
				Graphics.Blit(velBuffer, tileMax, this.motionBlurMaterial, 2);
				Graphics.Blit(tileMax, neighbourMax, this.motionBlurMaterial, 3);

				Graphics.Blit(source, destination, this.motionBlurMaterial, 7);
			}
			else
			{
				// simple & fast blur (low quality): just blurring along velocity
				Graphics.Blit(source, destination, this.motionBlurMaterial, 5);
			}
		}

		// cleanup
		RenderTexture.ReleaseTemporary(velBuffer);
		RenderTexture.ReleaseTemporary(tileMax);
		RenderTexture.ReleaseTemporary(neighbourMax);
	}

	void Remember()
	{
		this.prevViewProjMat = this.currentViewProjMat;
		this.prevFrameForward = this.transform.forward;
		this.prevFrameRight = this.transform.right;
		this.prevFrameUp = this.transform.up;
		this.prevFramePos = this.transform.position;
	}

	Camera GetTmpCam()
	{
		Camera camera = this.GetComponent<Camera>();

		if (this.tmpCam == null)
		{
			string name = "_" + camera.name + "_MotionBlurTmpCam";
			GameObject go = GameObject.Find(name);
			if (null == go) // couldn't find, recreate
			{
				this.tmpCam = new GameObject(name, typeof(Camera));
			}
			else
			{
				this.tmpCam = go;
			}
		}

		this.tmpCam.hideFlags = HideFlags.DontSave;
		this.tmpCam.transform.position = camera.transform.position;
		this.tmpCam.transform.rotation = camera.transform.rotation;
		this.tmpCam.transform.localScale = camera.transform.localScale;
		this.tmpCam.GetComponent<Camera>().CopyFrom(camera);

		this.tmpCam.GetComponent<Camera>().enabled = false;
		this.tmpCam.GetComponent<Camera>().depthTextureMode = DepthTextureMode.None;
		this.tmpCam.GetComponent<Camera>().clearFlags = CameraClearFlags.Nothing;

		return tmpCam.GetComponent<Camera>();
	}

	void StartFrame()
	{
		// take only x% of positional changes into account (camera motion)
		// TODO: possibly do the same for rotational part
		this.prevFramePos = Vector3.Slerp(this.prevFramePos, this.transform.position, 0.75f);
	}

	int divRoundUp(int x, int d)
	{
		return (x + d - 1) / d;
	}
}
