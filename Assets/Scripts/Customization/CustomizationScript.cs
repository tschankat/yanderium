using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomizationScript : MonoBehaviour
{
	// [af] Various data members for keeping track of customization state.
	class CustomizationData
	{
		public RangeInt skinColor;
		public RangeInt hairstyle;
		public RangeInt hairColor;
		public RangeInt eyeColor;
		public RangeInt eyewear;
		public RangeInt facialHair;
		public RangeInt maleUniform;
		public RangeInt femaleUniform;
	}

	[SerializeField] CustomizationData Data;

	[SerializeField] InputManagerScript InputManager;

	[SerializeField] Renderer FacialHairRenderer;
	[SerializeField] SkinnedMeshRenderer YandereRenderer; // [af] Replaced Renderer with SkinnedMeshRenderer.
	[SerializeField] SkinnedMeshRenderer SenpaiRenderer; // [af] Replaced Renderer with SkinnedMeshRenderer.
	[SerializeField] Renderer HairRenderer;
	[SerializeField] AudioSource MyAudio;
	[SerializeField] Renderer EyeR;
	[SerializeField] Renderer EyeL;

	[SerializeField] Transform UniformHighlight;
	[SerializeField] Transform ApologyWindow;
	[SerializeField] Transform YandereHead;
	[SerializeField] Transform YandereNeck;
	[SerializeField] Transform SenpaiHead;
	[SerializeField] Transform Highlight;
	[SerializeField] Transform Yandere;
	[SerializeField] Transform Senpai;
	[SerializeField] Transform[] Corridor;

	[SerializeField] UIPanel CustomizePanel;
	[SerializeField] UIPanel UniformPanel;
	[SerializeField] UIPanel FinishPanel;
	[SerializeField] UIPanel GenderPanel;
	[SerializeField] UIPanel WhitePanel;

	[SerializeField] UILabel FacialHairStyleLabel;
	[SerializeField] UILabel FemaleUniformLabel;
	[SerializeField] UILabel MaleUniformLabel;
	[SerializeField] UILabel SkinColorLabel;
	[SerializeField] UILabel HairStyleLabel;
	[SerializeField] UILabel HairColorLabel;
	[SerializeField] UILabel EyeColorLabel;
	[SerializeField] UILabel EyeWearLabel;

	[SerializeField] GameObject LoveSickCamera;
	[SerializeField] GameObject CensorCloud;
	[SerializeField] GameObject BigCloud;
	[SerializeField] GameObject Hearts;
	[SerializeField] GameObject Cloud;

	[SerializeField] UISprite Black;
	[SerializeField] UISprite White;

	[SerializeField] bool Apologize = false;
	[SerializeField] bool LoveSick = false;
	[SerializeField] bool FadeOut = false;

	[SerializeField] float ScrollSpeed = 0.0f;
	[SerializeField] float Timer = 0.0f;

	[SerializeField] int Selected = 1;
	[SerializeField] int Phase = 1;

	[SerializeField] Texture[] FemaleUniformTextures;
	[SerializeField] Texture[] MaleUniformTextures;
	[SerializeField] Texture[] FaceTextures;
	[SerializeField] Texture[] SkinTextures;

	[SerializeField] GameObject[] FacialHairstyles;
	[SerializeField] GameObject[] Hairstyles;
	[SerializeField] GameObject[] Eyewears;

	[SerializeField] Mesh[] FemaleUniforms;
	[SerializeField] Mesh[] MaleUniforms;

	[SerializeField] Texture FemaleFace;

	[SerializeField] string HairColorName = string.Empty;
	[SerializeField] string EyeColorName = string.Empty;

	[SerializeField] AudioClip LoveSickIntro;
	[SerializeField] AudioClip LoveSickLoop;

	public float AbsoluteRotation = 0.0f;
	public float Adjustment = 0.0f;
	public float Rotation = 0.0f;

	void Awake()
	{
		this.Data = new CustomizationData();
		this.Data.skinColor = new RangeInt(3, this.MinSkinColor, this.MaxSkinColor);
		this.Data.hairstyle = new RangeInt(1, this.MinHairstyle, this.MaxHairstyle);
		this.Data.hairColor = new RangeInt(1, this.MinHairColor, this.MaxHairColor);
		this.Data.eyeColor = new RangeInt(1, this.MinEyeColor, this.MaxEyeColor);
		this.Data.eyewear = new RangeInt(0, this.MinEyewear, this.MaxEyewear);
		this.Data.facialHair = new RangeInt(0, this.MinFacialHair, this.MaxFacialHair);
		this.Data.maleUniform = new RangeInt(1, this.MinMaleUniform, this.MaxMaleUniform);
		this.Data.femaleUniform = new RangeInt(1, this.MinFemaleUniform, this.MaxFemaleUniform);
	}

	void Start()
	{
        Cursor.visible = false;

		Time.timeScale = 1;

		this.LoveSick = GameGlobals.LoveSick;

		this.ApologyWindow.localPosition = new Vector3(
			1360.0f,
			this.ApologyWindow.localPosition.y,
			this.ApologyWindow.localPosition.z);

		this.CustomizePanel.alpha = 0.0f;
		this.UniformPanel.alpha = 0.0f;
		this.FinishPanel.alpha = 0.0f;
		this.GenderPanel.alpha = 0.0f;
		this.WhitePanel.alpha = 1.0f;

		this.UpdateFacialHair(this.Data.facialHair.Value);
		this.UpdateHairStyle(this.Data.hairstyle.Value);
		this.UpdateEyes(this.Data.eyeColor.Value);
		this.UpdateSkin(this.Data.skinColor.Value);

		if (this.LoveSick)
		{
			this.LoveSickColorSwap();

			this.WhitePanel.alpha = 0.0f;
			this.Data.femaleUniform.Value = 5;
			this.Data.maleUniform.Value = 5;

			//Camera.main.backgroundColor = new Color(0, 0, 0, 1);
			RenderSettings.fogColor = new Color(0, 0, 0, 1);
			this.LoveSickCamera.SetActive(true);
			this.Black.color = Color.black;

			this.MyAudio.loop = false;
			this.MyAudio.clip = this.LoveSickIntro;
			this.MyAudio.Play();
		}
		else
		{
			this.Data.femaleUniform.Value = this.MinFemaleUniform;
			this.Data.maleUniform.Value = this.MinMaleUniform;

			//Camera.main.backgroundColor = new Color(1, .5f, 1, 1);
			RenderSettings.fogColor = new Color(1, .5f, 1, 1);
			this.Black.color = new Color(0, 0, 0, 0);
			this.LoveSickCamera.SetActive(false);
		}

		this.UpdateMaleUniform(this.Data.maleUniform.Value, this.Data.skinColor.Value);
		this.UpdateFemaleUniform(this.Data.femaleUniform.Value);

		this.Senpai.position = new Vector3(0.0f, -1.0f, 2.0f);
		this.Senpai.gameObject.SetActive(true);
		this.Senpai.GetComponent<Animation>().Play(AnimNames.MaleNewWalk);

		this.Yandere.position = new Vector3(1.0f, -1.0f, 4.50f);
		this.Yandere.gameObject.SetActive(true);
		this.Yandere.GetComponent<Animation>().Play(AnimNames.FemaleNewWalk);

		this.CensorCloud.SetActive(false);
		this.Hearts.SetActive(false);
	}

	int MinSkinColor { get { return 1; } }
	int MaxSkinColor { get { return 5; } }
	int MinHairstyle { get { return 0; } }
	int MaxHairstyle { get { return this.Hairstyles.Length - 1; } }
	int MinHairColor { get { return 1; } }
	int MaxHairColor { get { return ColorPairs.Length - 1; } }
	int MinEyeColor { get { return 1; } }
	int MaxEyeColor { get { return ColorPairs.Length - 1; } }
	int MinEyewear { get { return 0; } }
	int MaxEyewear { get { return 5; } }
	int MinFacialHair { get { return 0; } }
	int MaxFacialHair { get { return this.FacialHairstyles.Length - 1; } }
	int MinMaleUniform { get { return 1; } }
	int MaxMaleUniform { get { return this.MaleUniforms.Length - 1; } }
	int MinFemaleUniform { get { return 1; } }
	int MaxFemaleUniform { get { return this.FemaleUniforms.Length - 1; } }

	// [af] Camera speed when moving between Senpai angles.
	float CameraSpeed { get { return Time.deltaTime * 10.0f; } }

	void Update()
	{
		if (!this.MyAudio.loop && !this.MyAudio.isPlaying)
		{
			this.MyAudio.loop = true;
			this.MyAudio.clip = this.LoveSickLoop;
			this.MyAudio.Play();
		}

		for (int ID = 1; ID < 3; ID++)
		{
			Transform corridorTransform = this.Corridor[ID];
			corridorTransform.position = new Vector3(
				corridorTransform.position.x,
				corridorTransform.position.y,
				corridorTransform.position.z + (Time.deltaTime * this.ScrollSpeed));

			if (corridorTransform.position.z > 36.0f)
			{
				corridorTransform.position = new Vector3(
					corridorTransform.position.x,
					corridorTransform.position.y,
					corridorTransform.position.z - 72.0f);
			}
		}

#if UNITY_EDITOR

		if (Input.GetKeyDown(KeyCode.Equals))
		{
			Time.timeScale += 10.0f;
		}

		if (Input.GetKeyDown(KeyCode.Minus))
		{
			Time.timeScale -= 10.0f;
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			SceneManager.LoadScene(SceneNames.NewIntroScene);
		}

#endif

		if (this.Phase == 1)
		{
			if (this.WhitePanel.alpha == 0.0f)
			{
				this.GenderPanel.alpha = Mathf.MoveTowards(
					this.GenderPanel.alpha, 1.0f, Time.deltaTime * 2.0f);

				if (this.GenderPanel.alpha == 1.0f)
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						//this.Cloud.SetActive (true);

						this.Phase++;
					}

					if (Input.GetButtonDown(InputNames.Xbox_B))
					{
						this.Apologize = true;
					}

					if (Input.GetButtonDown(InputNames.Xbox_X))
					{
						this.White.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
						this.FadeOut = true;
						this.Phase = 0;
					}
				}
			}
		}
		else if (this.Phase == 2)
		{
			this.GenderPanel.alpha = Mathf.MoveTowards(
				this.GenderPanel.alpha, 0.0f, Time.deltaTime * 2.0f);

			this.Black.color = new Color(0, 0, 0, Mathf.MoveTowards(
				this.Black.color.a, 0.0f, Time.deltaTime * 2.0f));

			if (this.GenderPanel.alpha == 0.0f)
			{
				this.Senpai.gameObject.SetActive(true);

				this.Phase++;
			}
		}

		//////////////////////////////
		///// CUSTOMIZING SENPAI /////
		//////////////////////////////

		else if (this.Phase == 3)
		{
			this.Adjustment += Input.GetAxis("Mouse X") * Time.deltaTime * 10;

			if (this.Adjustment > 3)
			{
				this.Adjustment = 3;
			}
			else if (this.Adjustment < 0)
			{
				this.Adjustment = 0;
			}

			this.CustomizePanel.alpha = Mathf.MoveTowards(
				this.CustomizePanel.alpha, 1.0f, Time.deltaTime * 2.0f);

			if (this.CustomizePanel.alpha == 1.0f)
			{
				if (Input.GetButtonDown(InputNames.Xbox_A))
				{
					this.Senpai.localEulerAngles = new Vector3(
						this.Senpai.localEulerAngles.x,
						180.0f,
						this.Senpai.localEulerAngles.z);

					this.Phase++;
				}

				bool tappedDown = this.InputManager.TappedDown;
				bool tappedUp = this.InputManager.TappedUp;

				if (tappedDown || tappedUp)
				{
					const int minSelected = 1;
					const int maxSelected = 6;

					if (tappedDown)
					{
						this.Selected = (this.Selected >= maxSelected) ?
							minSelected : (this.Selected + 1);
					}
					else if (tappedUp)
					{
						this.Selected = (this.Selected <= minSelected) ?
							maxSelected : (this.Selected - 1);
					}

					this.Highlight.localPosition = new Vector3(
						this.Highlight.localPosition.x,
						650.0f - (this.Selected * 150.0f),
						this.Highlight.localPosition.z);
				}

				if (this.InputManager.TappedRight)
				{
					if (this.Selected == 1)
					{
						this.Data.skinColor.Value = this.Data.skinColor.Next;
						this.UpdateSkin(this.Data.skinColor.Value);
					}
					else if (this.Selected == 2)
					{
						this.Data.hairstyle.Value = this.Data.hairstyle.Next;
						this.UpdateHairStyle(this.Data.hairstyle.Value);
					}
					else if (this.Selected == 3)
					{
						this.Data.hairColor.Value = this.Data.hairColor.Next;
						this.UpdateColor(this.Data.hairColor.Value);
					}
					else if (this.Selected == 4)
					{
						this.Data.eyeColor.Value = this.Data.eyeColor.Next;
						this.UpdateEyes(this.Data.eyeColor.Value);
					}
					else if (this.Selected == 5)
					{
						this.Data.eyewear.Value = this.Data.eyewear.Next;
						this.UpdateEyewear(this.Data.eyewear.Value);
					}
					else if (this.Selected == 6)
					{
						this.Data.facialHair.Value = this.Data.facialHair.Next;
						this.UpdateFacialHair(this.Data.facialHair.Value);
					}
				}

				if (this.InputManager.TappedLeft)
				{
					if (this.Selected == 1)
					{
						this.Data.skinColor.Value = this.Data.skinColor.Previous;
						this.UpdateSkin(this.Data.skinColor.Value);
					}
					else if (this.Selected == 2)
					{
						this.Data.hairstyle.Value = this.Data.hairstyle.Previous;
						this.UpdateHairStyle(this.Data.hairstyle.Value);
					}
					else if (this.Selected == 3)
					{
						this.Data.hairColor.Value = this.Data.hairColor.Previous;
						this.UpdateColor(this.Data.hairColor.Value);
					}
					else if (this.Selected == 4)
					{
						this.Data.eyeColor.Value = this.Data.eyeColor.Previous;
						this.UpdateEyes(this.Data.eyeColor.Value);
					}
					else if (this.Selected == 5)
					{
						this.Data.eyewear.Value = this.Data.eyewear.Previous;
						this.UpdateEyewear(this.Data.eyewear.Value);
					}
					else if (this.Selected == 6)
					{
						this.Data.facialHair.Value = this.Data.facialHair.Previous;
						this.UpdateFacialHair(this.Data.facialHair.Value);
					}
				}
			}

			this.Rotation = Mathf.Lerp(this.Rotation, 45.0f - (this.Adjustment * 30.0f), Time.deltaTime * 10);

			this.AbsoluteRotation = 45 - Mathf.Abs(Rotation);

			if (this.Selected == 1)
			{
				this.LoveSickCamera.transform.position = new Vector3(
					Mathf.Lerp(this.LoveSickCamera.transform.position.x, -1.50f + this.Adjustment, this.CameraSpeed),
					Mathf.Lerp(this.LoveSickCamera.transform.position.y, 0.0f, this.CameraSpeed),
					Mathf.Lerp(this.LoveSickCamera.transform.position.z, 0.50f - (AbsoluteRotation *.015f), this.CameraSpeed));
			}
			else
			{
				this.LoveSickCamera.transform.position = new Vector3(
					Mathf.Lerp(this.LoveSickCamera.transform.position.x, -0.50f + (this.Adjustment * .33333f), this.CameraSpeed),
					Mathf.Lerp(this.LoveSickCamera.transform.position.y, 0.50f, this.CameraSpeed),
					Mathf.Lerp(this.LoveSickCamera.transform.position.z, 1.50f - (AbsoluteRotation *.015f * .33333f), this.CameraSpeed));
			}

			this.LoveSickCamera.transform.eulerAngles = new Vector3(0, this.Rotation, 0);

			this.transform.eulerAngles = this.LoveSickCamera.transform.eulerAngles;
			this.transform.position = this.LoveSickCamera.transform.position;
		}
		else if (this.Phase == 4)
		{
			this.Adjustment = Mathf.Lerp(this.Adjustment, 0, Time.deltaTime * 10);

			this.Rotation = Mathf.Lerp(this.Rotation, 45.0f, Time.deltaTime * 10);

			this.AbsoluteRotation = 45 - Mathf.Abs(Rotation);

			this.LoveSickCamera.transform.position = new Vector3(
					Mathf.Lerp(this.LoveSickCamera.transform.position.x, -1.50f + this.Adjustment, this.CameraSpeed),
					Mathf.Lerp(this.LoveSickCamera.transform.position.y, 0.0f, this.CameraSpeed),
					Mathf.Lerp(this.LoveSickCamera.transform.position.z, 0.50f - (AbsoluteRotation *.015f), this.CameraSpeed));

			this.LoveSickCamera.transform.eulerAngles = new Vector3(0, this.Rotation, 0);

			this.transform.eulerAngles = this.LoveSickCamera.transform.eulerAngles;
			this.transform.position = this.LoveSickCamera.transform.position;

			this.CustomizePanel.alpha = Mathf.MoveTowards(
				this.CustomizePanel.alpha, 0.0f, Time.deltaTime * 2.0f);

			if (this.CustomizePanel.alpha == 0.0f)
			{
				this.Phase++;
			}
		}
		else if (this.Phase == 5)
		{
			this.FinishPanel.alpha = Mathf.MoveTowards(
				this.FinishPanel.alpha, 1.0f, Time.deltaTime * 2.0f);

			if (this.FinishPanel.alpha == 1.0f)
			{
				if (Input.GetButtonDown(InputNames.Xbox_A))
				{
					this.Phase++;
				}

				if (Input.GetButtonDown(InputNames.Xbox_B))
				{
					const int minSelected = 1;

					this.Selected = minSelected;
					this.Highlight.localPosition = new Vector3(
						this.Highlight.localPosition.x,
						650.0f - (this.Selected * 150.0f),
						this.Highlight.localPosition.z);

					this.Phase = 100;
				}
			}
		}
		else if (this.Phase == 6)
		{
			this.FinishPanel.alpha = Mathf.MoveTowards(
				this.FinishPanel.alpha, 0.0f, Time.deltaTime * 2.0f);

			if (this.FinishPanel.alpha == 0.0f)
			{
				this.UpdateFemaleUniform(this.Data.femaleUniform.Value);
				this.UpdateMaleUniform(this.Data.maleUniform.Value, this.Data.skinColor.Value);

				this.CensorCloud.SetActive(false);

				// [af] Added "gameObject" for C# compatibility.
				this.Yandere.gameObject.SetActive(true);

				const int minSelected = 1;

				this.Selected = minSelected;
				this.Phase++;
			}
		}

		//////////////////////////////
		///// CUSTOMIZING SENPAI /////
		//////////////////////////////

		else if (this.Phase == 7)
		{
			this.UniformPanel.alpha = Mathf.MoveTowards(
				this.UniformPanel.alpha, 1.0f, Time.deltaTime * 2.0f);

			if (this.UniformPanel.alpha == 1.0f)
			{
				if (Input.GetButtonDown(InputNames.Xbox_A))
				{
					this.Yandere.localEulerAngles = new Vector3(
						this.Yandere.localEulerAngles.x,
						180.0f,
						this.Yandere.localEulerAngles.z);

					this.Senpai.localEulerAngles = new Vector3(
						this.Senpai.localEulerAngles.x,
						180.0f,
						this.Senpai.localEulerAngles.z);

					this.Phase++;
				}

				const int minSelected = 1;
				const int maxSelected = 2;

				if (this.InputManager.TappedDown || this.InputManager.TappedUp)
				{
					this.Selected = (this.Selected == minSelected) ? maxSelected : minSelected;

					this.UniformHighlight.localPosition = new Vector3(
						this.UniformHighlight.localPosition.x,
						650.0f - (this.Selected * 150.0f),
						this.UniformHighlight.localPosition.z);
				}

				if (this.InputManager.TappedRight)
				{
					if (this.Selected == minSelected)
					{
						this.Data.maleUniform.Value = this.Data.maleUniform.Next;
						this.UpdateMaleUniform(this.Data.maleUniform.Value, this.Data.skinColor.Value);
					}
					else if (this.Selected == maxSelected)
					{
						this.Data.femaleUniform.Value = this.Data.femaleUniform.Next;
						this.UpdateFemaleUniform(this.Data.femaleUniform.Value);
					}
				}

				if (this.InputManager.TappedLeft)
				{
					if (this.Selected == minSelected)
					{
						this.Data.maleUniform.Value = this.Data.maleUniform.Previous;
						this.UpdateMaleUniform(this.Data.maleUniform.Value, this.Data.skinColor.Value);
					}
					else if (this.Selected == maxSelected)
					{
						this.Data.femaleUniform.Value = this.Data.femaleUniform.Previous;
						this.UpdateFemaleUniform(this.Data.femaleUniform.Value);
					}
				}
			}
		}
		else if (this.Phase == 8)
		{
			this.UniformPanel.alpha = Mathf.MoveTowards(
				this.UniformPanel.alpha, 0.0f, Time.deltaTime * 2.0f);

			if (this.UniformPanel.alpha == 0.0f)
			{
				this.Phase++;
			}
		}
		else if (this.Phase == 9)
		{
			this.FinishPanel.alpha = Mathf.MoveTowards(
				this.FinishPanel.alpha, 1.0f, Time.deltaTime * 2.0f);

			if (this.FinishPanel.alpha == 1.0f)
			{
				if (Input.GetButtonDown(InputNames.Xbox_A))
				{
					this.Phase++;
				}

				if (Input.GetButtonDown(InputNames.Xbox_B))
				{
					const int minSelected = 1;

					this.Selected = minSelected;
					this.UniformHighlight.localPosition = new Vector3(
						this.UniformHighlight.localPosition.x,
						650.0f - (this.Selected * 150.0f),
						this.UniformHighlight.localPosition.z);

					this.Phase = 99;
				}
			}
		}
		else if (this.Phase == 10)
		{
			this.FinishPanel.alpha = Mathf.MoveTowards(
				this.FinishPanel.alpha, 0.0f, Time.deltaTime * 2.0f);

			if (this.FinishPanel.alpha == 0.0f)
			{
				this.White.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
				this.FadeOut = true;
				this.Phase = 0;
			}
		}
		else if (this.Phase == 99)
		{
			this.FinishPanel.alpha = Mathf.MoveTowards(
				this.FinishPanel.alpha, 0.0f, Time.deltaTime * 2.0f);

			if (this.FinishPanel.alpha == 0.0f)
			{
				this.Phase = 7;
			}
		}
		else if (this.Phase == 100)
		{
			this.FinishPanel.alpha = Mathf.MoveTowards(
				this.FinishPanel.alpha, 0.0f, Time.deltaTime * 2.0f);

			if (this.FinishPanel.alpha == 0.0f)
			{
				this.Phase = 3;
			}
		}

		if ((this.Phase > 3) && (this.Phase < 100))
		{
			if (this.Phase < 6)
			{
				this.LoveSickCamera.transform.position = new Vector3(
					Mathf.Lerp(this.LoveSickCamera.transform.position.x, -1.50f, this.CameraSpeed),
					Mathf.Lerp(this.LoveSickCamera.transform.position.y, 0.0f, this.CameraSpeed),
					Mathf.Lerp(this.LoveSickCamera.transform.position.z, 0.5f, this.CameraSpeed));

				this.transform.position = this.LoveSickCamera.transform.position;
			}
			else
			{
				this.LoveSickCamera.transform.position = new Vector3(
					Mathf.Lerp(this.LoveSickCamera.transform.position.x, 0.0f, this.CameraSpeed),
					Mathf.Lerp(this.LoveSickCamera.transform.position.y, 0.50f, this.CameraSpeed),
					Mathf.Lerp(this.LoveSickCamera.transform.position.z, 0.0f, this.CameraSpeed));

				this.LoveSickCamera.transform.eulerAngles = new Vector3(
					Mathf.Lerp(this.LoveSickCamera.transform.eulerAngles.x, 15.0f, this.CameraSpeed),
					Mathf.Lerp(this.LoveSickCamera.transform.eulerAngles.y, 0.0f, this.CameraSpeed),
					Mathf.Lerp(this.LoveSickCamera.transform.eulerAngles.z, 0.0f, this.CameraSpeed));

				this.transform.eulerAngles = this.LoveSickCamera.transform.eulerAngles;
				this.transform.position = this.LoveSickCamera.transform.position;

				this.Yandere.position = new Vector3(
					Mathf.Lerp(this.Yandere.position.x, 1.0f, Time.deltaTime * 10.0f),
					Mathf.Lerp(this.Yandere.position.y, -1.0f, Time.deltaTime * 10.0f),
					Mathf.Lerp(this.Yandere.position.z, 3.50f, Time.deltaTime * 10.0f));
			}
		}

		if (this.FadeOut)
		{
			this.WhitePanel.alpha = Mathf.MoveTowards(
				this.WhitePanel.alpha, 1.0f, Time.deltaTime);
			this.GetComponent<AudioSource>().volume = 1.0f - this.WhitePanel.alpha;

			if (this.WhitePanel.alpha == 1.0f)
			{
				SenpaiGlobals.CustomSenpai = true;
				SenpaiGlobals.SenpaiSkinColor = this.Data.skinColor.Value;
				SenpaiGlobals.SenpaiHairStyle = this.Data.hairstyle.Value;
				SenpaiGlobals.SenpaiHairColor = this.HairColorName;
				SenpaiGlobals.SenpaiEyeColor = this.EyeColorName;
				SenpaiGlobals.SenpaiEyeWear = this.Data.eyewear.Value;
				SenpaiGlobals.SenpaiFacialHair = this.Data.facialHair.Value;
				StudentGlobals.MaleUniform = this.Data.maleUniform.Value;
				StudentGlobals.FemaleUniform = this.Data.femaleUniform.Value;

				SceneManager.LoadScene(SceneNames.NewIntroScene);
			}
		}
		else
		{
			this.WhitePanel.alpha = Mathf.MoveTowards(
				this.WhitePanel.alpha, 0.0f, Time.deltaTime);
		}

		if (this.Apologize)
		{
			this.Timer += Time.deltaTime;

			if (this.Timer < 1.0f)
			{
				this.ApologyWindow.localPosition = new Vector3(
					Mathf.Lerp(this.ApologyWindow.localPosition.x, 0.0f, Time.deltaTime * 10.0f),
					this.ApologyWindow.localPosition.y,
					this.ApologyWindow.localPosition.z);
			}
			else
			{
				this.ApologyWindow.localPosition = new Vector3(
					Mathf.Abs((this.ApologyWindow.localPosition.x - Time.deltaTime) * 0.010f) * (Time.deltaTime * 1000.0f),
					this.ApologyWindow.localPosition.y,
					this.ApologyWindow.localPosition.z);

				if (this.ApologyWindow.localPosition.x < -1360.0f)
				{
					this.ApologyWindow.localPosition = new Vector3(
						1360.0f,
						this.ApologyWindow.localPosition.y,
						this.ApologyWindow.localPosition.z);

					this.Apologize = false;
					this.Timer = 0.0f;
				}
			}
		}

#if UNITY_EDITOR
		if (Input.GetKeyDown("l"))
		{
			GameGlobals.LoveSick = !GameGlobals.LoveSick;
			SceneManager.LoadScene(SceneNames.SenpaiScene);
		}
#endif
	}

	void LateUpdate()
	{
		this.YandereHead.LookAt(this.SenpaiHead.position);
	}

	void UpdateSkin(int skinColor)
	{
		this.UpdateMaleUniform(this.Data.maleUniform.Value, skinColor);
		this.SkinColorLabel.text = "Skin Color " + skinColor.ToString();
	}

	void UpdateHairStyle(int hairstyle)
	{
		for (int ID = 1; ID < this.Hairstyles.Length; ID++)
		{
			this.Hairstyles[ID].SetActive(false);
		}

		if (hairstyle > 0)
		{
			this.HairRenderer = this.Hairstyles[hairstyle].GetComponent<Renderer>();
			this.Hairstyles[hairstyle].SetActive(true);
		}

		this.HairStyleLabel.text = "Hair Style " + hairstyle.ToString();

		this.UpdateColor(this.Data.hairColor.Value);
	}

	void UpdateFacialHair(int facialHair)
	{
		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < this.FacialHairstyles.Length; ID++)
		{
			this.FacialHairstyles[ID].SetActive(false);
		}

		if (facialHair > 0)
		{
			this.FacialHairRenderer = this.FacialHairstyles[facialHair].GetComponent<Renderer>();
			this.FacialHairstyles[facialHair].SetActive(true);
		}

		this.FacialHairStyleLabel.text = "Facial Hair " + facialHair.ToString();

		this.UpdateColor(this.Data.hairColor.Value);
	}

	// [af] Color definitions for hair and eyes.
	static readonly KeyValuePair<Color, string>[] ColorPairs = new KeyValuePair<Color, string>[]
	{
		new KeyValuePair<Color, string>(new Color(), string.Empty), // Unused (index 0).
		new KeyValuePair<Color, string>(new Color(0.50f, 0.50f, 0.50f), "Black"),
		new KeyValuePair<Color, string>(new Color(1.0f, 0.0f, 0.0f), "Red"),
		new KeyValuePair<Color, string>(new Color(1.0f, 1.0f, 0.0f), "Yellow"),
		new KeyValuePair<Color, string>(new Color(0.0f, 1.0f, 0.0f), "Green"),
		new KeyValuePair<Color, string>(new Color(0.0f, 1.0f, 1.0f), "Cyan"),
		new KeyValuePair<Color, string>(new Color(0.0f, 0.0f, 1.0f), "Blue"),
		new KeyValuePair<Color, string>(new Color(1.0f, 0.0f, 1.0f), "Purple"),
		new KeyValuePair<Color, string>(new Color(1.0f, 0.50f, 0.0f), "Orange"),
		new KeyValuePair<Color, string>(new Color(0.50f, 0.25f, 0.0f), "Brown"),
		new KeyValuePair<Color, string>(new Color(1.0f, 1.0f, 1.0f), "White")
	};

	void UpdateColor(int hairColor)
	{
		// [af] Get the color pair for the selected hair.
		var colorPair = ColorPairs[hairColor];
		Color hairColorValue = colorPair.Key;
		this.HairColorName = colorPair.Value;

		if (this.Data.hairstyle.Value > 0)
		{
			this.HairRenderer = this.Hairstyles[this.Data.hairstyle.Value].GetComponent<Renderer>();
			this.HairRenderer.material.color = hairColorValue;
		}

		if (this.Data.facialHair.Value > 0)
		{
			this.FacialHairRenderer = this.FacialHairstyles[this.Data.facialHair.Value].GetComponent<Renderer>();
			this.FacialHairRenderer.material.color = hairColorValue;

			if (this.FacialHairRenderer.materials.Length > 1)
			{
				this.FacialHairRenderer.materials[1].color = hairColorValue;
			}
		}

		this.HairColorLabel.text = "Hair Color " + hairColor.ToString();
	}

	void UpdateEyes(int eyeColor)
	{
		// [af] Get the color pair for the selected eye color.
		var colorPair = ColorPairs[eyeColor];
		Color newColor = colorPair.Key;
		this.EyeColorName = colorPair.Value;

		this.EyeR.material.color = newColor;
		this.EyeL.material.color = newColor;

		this.EyeColorLabel.text = "Eye Color " + eyeColor.ToString();
	}

	void UpdateEyewear(int eyewear)
	{
		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < this.Eyewears.Length; ID++)
		{
			this.Eyewears[ID].SetActive(false);
		}

		if (eyewear > 0)
		{
			this.Eyewears[eyewear].SetActive(true);
		}

		this.EyeWearLabel.text = "Eye Wear " + eyewear.ToString();
	}

	void UpdateMaleUniform(int maleUniform, int skinColor)
	{
		this.SenpaiRenderer.sharedMesh = this.MaleUniforms[maleUniform];

		if (maleUniform == 1)
		{
			this.SenpaiRenderer.materials[0].mainTexture = this.SkinTextures[skinColor];
			this.SenpaiRenderer.materials[1].mainTexture = this.MaleUniformTextures[maleUniform];
			this.SenpaiRenderer.materials[2].mainTexture = this.FaceTextures[skinColor];
		}
		else if (maleUniform == 2)
		{
			this.SenpaiRenderer.materials[0].mainTexture = this.MaleUniformTextures[maleUniform];
			this.SenpaiRenderer.materials[1].mainTexture = this.FaceTextures[skinColor];
			this.SenpaiRenderer.materials[2].mainTexture = this.SkinTextures[skinColor];
		}
		else if (maleUniform == 3)
		{
			this.SenpaiRenderer.materials[0].mainTexture = this.MaleUniformTextures[maleUniform];
			this.SenpaiRenderer.materials[1].mainTexture = this.FaceTextures[skinColor];
			this.SenpaiRenderer.materials[2].mainTexture = this.SkinTextures[skinColor];
		}
		else if (maleUniform == 4)
		{
			this.SenpaiRenderer.materials[0].mainTexture = this.FaceTextures[skinColor];
			this.SenpaiRenderer.materials[1].mainTexture = this.SkinTextures[skinColor];
			this.SenpaiRenderer.materials[2].mainTexture = this.MaleUniformTextures[maleUniform];
		}
		else if (maleUniform == 5)
		{
			this.SenpaiRenderer.materials[0].mainTexture = this.FaceTextures[skinColor];
			this.SenpaiRenderer.materials[1].mainTexture = this.SkinTextures[skinColor];
			this.SenpaiRenderer.materials[2].mainTexture = this.MaleUniformTextures[maleUniform];
		}
		else if (maleUniform == 6)
		{
			this.SenpaiRenderer.materials[0].mainTexture = this.FaceTextures[skinColor];
			this.SenpaiRenderer.materials[1].mainTexture = this.SkinTextures[skinColor];
			this.SenpaiRenderer.materials[2].mainTexture = this.MaleUniformTextures[maleUniform];
		}

		this.MaleUniformLabel.text = "Male Uniform " + maleUniform.ToString();
	}

	void UpdateFemaleUniform(int femaleUniform)
	{
		this.YandereRenderer.sharedMesh = this.FemaleUniforms[femaleUniform];

		this.YandereRenderer.materials[0].mainTexture = this.FemaleUniformTextures[femaleUniform];
		this.YandereRenderer.materials[1].mainTexture = this.FemaleUniformTextures[femaleUniform];
		this.YandereRenderer.materials[2].mainTexture = this.FemaleFace;
		this.YandereRenderer.materials[3].mainTexture = this.FemaleFace;

		this.FemaleUniformLabel.text = "Female Uniform " + femaleUniform.ToString();
	}

	void LoveSickColorSwap()
	{
		GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

		foreach (GameObject go in allObjects)
		{
			UISprite sprite = go.GetComponent<UISprite>();

			if (sprite != null)
			{
				if (sprite.color != Color.black && sprite.transform.parent != Highlight && sprite.transform.parent != UniformHighlight)
				{
					sprite.color = new Color(1.0f, 0.0f, 0.0f, sprite.color.a);
				}
			}

			UITexture texture = go.GetComponent<UITexture>();

			if (texture != null)
			{
				texture.color = new Color(1.0f, 0.0f, 0.0f, texture.color.a);
			}

			UILabel label = go.GetComponent<UILabel>();

			if (label != null)
			{
				if (label.color != Color.black)
				{
					label.color = new Color(1.0f, 0.0f, 0.0f, label.color.a);
				}
			}
		}
	}
}
