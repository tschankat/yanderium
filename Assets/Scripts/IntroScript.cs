using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;

using XInputDotNetPure;

public class IntroScript : MonoBehaviour
{
	public PostProcessingBehaviour PostProcessing;
	public PostProcessingBehaviour GUIPP;
	public PostProcessingProfile Profile;

	public GameObject[] AttackPair;

	public GameObject ConfessionScene;
	public GameObject ParentAndChild;
	public GameObject DeathCorridor;
	public GameObject BloodParent;
	public GameObject Particles;
	public GameObject Stalking;
	public GameObject School;
	public GameObject Osana;
	public GameObject Room;
	public GameObject Quad;

	public Texture[] Textures;

	public Transform RightForeArm;
	public Transform LeftForeArm;
	public Transform RightWrist;
	public Transform LeftWrist;

	public Transform Corridors;
	public Transform RightHand;
	public Transform Senpai;
	public Transform Head;

	public Animation BloodyHandsAnim;
	public Animation HoleInChestAnim;
	public Animation YoungFatherAnim;
	public Animation YoungRyobaAnim;
	public Animation StalkerAnim;
	public Animation YandereAnim;
	public Animation SenpaiAnim;

	public Animation MotherAnim;
	public Animation ChildAnim;

	public Animation[] AttackAnim;

	public Renderer[] TreeRenderer;

	public Renderer YoungFatherHairRenderer;
	public Renderer YoungFatherRenderer;
	public Renderer Montage;
	public Renderer Mound;
	public Renderer Sky;

	public SkinnedMeshRenderer Yandere;

	public ParticleSystem Blossoms;
	public ParticleSystem Bubbles;
	public ParticleSystem Stars;

	public UISprite FadeOutDarkness;
	public UITexture LoveSickLogo;
	public UIPanel SkipPanel;
	public UISprite Darkness;
	public UISprite Circle;
	public UITexture Logo;
	public UILabel Label;

	public AudioSource Narration;
	public AudioSource BGM;

	public string[] Lines;
	public float[] Cue;

	public bool Narrating = false;
	public bool Musicing = false;
	public bool FadeOut = false;
	public bool New = false;

	public float CameraRotationX = 0.0f;
	public float CameraRotationY = 0.0f;

	public float ThirdSpeed = 0.0f;
	public float SecondSpeed = 0.0f;
	public float Speed = 0.0f;

	public float Brightness = 0.0f;
	public float StartTimer = 0.0f;
	public float SkipTimer = 0.0f;
	public float EyeTimer = 0.0f;
	public float Alpha = 0.0f;
	public float Timer = 0.0f;

	public float AnimTimer = 0.0f;

	public int TextureID = 0;
	public int ID = 0;

	public float VibrationIntensity = 0.0f;
	public float VibrationTimer = 0.0f;
	public bool VibrationCheck = false;

	void Start()
	{
		Application.targetFrameRate = 60;

		this.LoveSickCheck();

		this.Circle.fillAmount = 0.0f;

		if (!this.New)
		{
			this.Darkness.color = new Color(
				0, 0,
				0, 1.0f);
		}

		this.Label.text = string.Empty;
		this.SkipTimer = 15.0f;

		if (this.New)
		{
			RenderSettings.ambientLight = new Color(1f, 1f, 1f);

			BloodyHandsAnim["f02_clenchFists_00"].speed = .166666f;
			HoleInChestAnim["f02_holeInChest_00"].speed = 0;
			YoungRyobaAnim["f02_introHoldHands_00"].speed = 0;
			YoungFatherAnim["introHoldHands_00"].speed = 0;

			this.BrightenEnvironment();

			this.transform.position = new Vector3(0, 1.255f, 0.2f);
			this.transform.eulerAngles = new Vector3(45, 0, 0);

			this.HoleInChestAnim.gameObject.SetActive(false);
			this.BloodyHandsAnim.gameObject.SetActive(true);
			this.Montage.gameObject.SetActive(false);
			this.ConfessionScene.SetActive(false);
			this.ParentAndChild.SetActive(false);
			this.DeathCorridor.SetActive(false);
			this.Stalking.SetActive(false);
			this.School.SetActive(false);
			this.Room.SetActive(false);

			this.SetToDefault();

			DepthOfFieldModel.Settings DepthSettings = this.Profile.depthOfField.settings;
			DepthSettings.focusDistance = .66666f;
			DepthSettings.aperture = 32;
			this.Profile.depthOfField.settings = DepthSettings;

			Renderer[] renderers = Corridors.gameObject.GetComponentsInChildren<Renderer>();

			foreach (Renderer renderer in renderers)
	        {
				renderer.material.color = new Color(.75f, .75f, .75f, 1);
	        }

			AttackAnim[1]["f02_katanaHighSanityA_00"].speed = 2.5f;
			AttackAnim[2]["f02_katanaHighSanityB_00"].speed = 2.5f;

			AttackAnim[3]["f02_batLowSanityA_00"].speed = 2.5f;
			AttackAnim[4]["f02_batLowSanityB_00"].speed = 2.5f;

			AttackAnim[5]["f02_katanaLowSanityA_00"].speed = 2.5f;
			AttackAnim[6]["f02_katanaLowSanityB_00"].speed = 2.5f;

			MotherAnim["f02_parentTalking_00"].speed = .75f;
			ChildAnim["f02_childListening_00"].speed = .75f;

			int TempID = 4;

			while (TempID < Cue.Length)
			{
				if (TempID < 21)
				{
					Cue[TempID] += 3.898f;
				}
				else if (TempID > 32)
				{
					Cue[TempID] += 4;
				}
				else
				{
					Cue[TempID] += 2f;
				}

				if (TempID > 40)
				{
					Cue[TempID] += 3f;
				}

				TempID++;
			}
		}
	}

	void Update()
	{
		if (this.VibrationCheck)
		{
			this.VibrationTimer = Mathf.MoveTowards(this.VibrationTimer, 0, Time.deltaTime);

			if (this.VibrationTimer == 0)
			{
				GamePad.SetVibration(0, 0, 0);
				this.VibrationCheck = false;
			}
		}

		if (Input.GetButton(InputNames.Xbox_X))
		{
			this.Circle.fillAmount = Mathf.MoveTowards(
				this.Circle.fillAmount, 1.0f, Time.deltaTime);

			this.SkipTimer = 15.0f;

			if (this.Circle.fillAmount == 1.0f)
			{
				this.FadeOut = true;
			}

			this.SkipPanel.alpha = 1.0f;
		}
		else
		{
			this.Circle.fillAmount = Mathf.MoveTowards(
				this.Circle.fillAmount, 0.0f, Time.deltaTime);

			this.SkipTimer -= Time.deltaTime * 2;
			this.SkipPanel.alpha = this.SkipTimer / 10.0f;
		}

		this.StartTimer += Time.deltaTime;

		if (this.StartTimer > 1.0f)
		{
			if (!this.Narrating)
			{
				this.Narration.Play();
				this.Narrating = true;

				if (this.BGM != null)
				{
					this.BGM.Play();
				}
			}
		}

		this.Timer = this.Narration.time;

		if (this.ID < this.Cue.Length)
		{
			if (this.Narration.time > this.Cue[this.ID])
			{
				this.Label.text = this.Lines[this.ID];
				this.ID++;
			}
		}

		if (this.FadeOut)
		{
			this.FadeOutDarkness.color = new Color(
				this.FadeOutDarkness.color.r,
				this.FadeOutDarkness.color.g,
				this.FadeOutDarkness.color.b,
				Mathf.MoveTowards(this.FadeOutDarkness.color.a, 1.0f, Time.deltaTime));

			this.Circle.fillAmount = 1.0f;
			this.Narration.volume = this.FadeOutDarkness.color.a;

			if (this.FadeOutDarkness.color.a == 1.0f)
			{
				SceneManager.LoadScene(SceneNames.PhoneScene);
			}
		}

		if (!this.New)
		{
			if ((this.Narration.time > 39.75f) && (this.Narration.time < 73.0f))
			{
				this.Darkness.color = new Color(
					this.Darkness.color.r,
					this.Darkness.color.g,
					this.Darkness.color.b,
					Mathf.MoveTowards(this.Darkness.color.a, 0.0f, Time.deltaTime * 0.50f));
			}

			if ((this.Narration.time > 73.0f) && (this.Narration.time < 105.50f))
			{
				this.Darkness.color = new Color(
					this.Darkness.color.r,
					this.Darkness.color.g,
					this.Darkness.color.b,
					Mathf.MoveTowards(this.Darkness.color.a, 1.0f, Time.deltaTime));
			}

			if ((this.Narration.time > 105.50f) && (this.Narration.time < 134.0f))
			{
				this.Darkness.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
			}

			if ((this.Narration.time > 134.0f) && (this.Narration.time < 147.0f))
			{
				this.Darkness.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
			}

			if ((this.Narration.time > 147.0f) && (this.Narration.time < 152.0f))
			{
				this.Logo.transform.localScale = new Vector3(
					this.Logo.transform.localScale.x + (Time.deltaTime * 0.10f),
					this.Logo.transform.localScale.y + (Time.deltaTime * 0.10f),
					this.Logo.transform.localScale.z + (Time.deltaTime * 0.10f));

				this.LoveSickLogo.transform.localScale = new Vector3(
					this.LoveSickLogo.transform.localScale.x + (Time.deltaTime * 0.05f),
					this.LoveSickLogo.transform.localScale.y + (Time.deltaTime * 0.05f),
					this.LoveSickLogo.transform.localScale.z + (Time.deltaTime * 0.05f));

				this.Logo.color = new Color(
					this.Logo.color.r, this.Logo.color.g, this.Logo.color.b, 1.0f);

				this.LoveSickLogo.color = new Color(
					this.LoveSickLogo.color.r, this.LoveSickLogo.color.g, this.LoveSickLogo.color.b, 1.0f);
			}

			if (this.Narration.time > 3.898f + 152.0f)
			{
				this.Logo.color = new Color(
					this.Logo.color.r, this.Logo.color.g, this.Logo.color.b, 0.0f);

				this.LoveSickLogo.color = new Color(
					this.LoveSickLogo.color.r, this.LoveSickLogo.color.g, this.LoveSickLogo.color.b, 0.0f);
			}

			if (this.Narration.time > 3.898f + 156.0f)
			{
				SceneManager.LoadScene(SceneNames.PhoneScene);
			}
		}

		#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (this.Narration.time < Cue[4] - 1)
			{
				this.Narration.time = Cue[4] - 1;
			}
			else if (this.Narration.time < Cue[9] - 1)
			{
				this.Narration.time = Cue[9] - 1;
			}
			else if (this.Narration.time < Cue[13] - 1)
			{
				this.Narration.time = Cue[13] - 1;
			}
			else if (this.Narration.time < Cue[15] - 1)
			{
				this.Narration.time = Cue[15] - 1;
			}
			else if (this.Narration.time < Cue[19] - 1)
			{
				this.Narration.time = Cue[19] - 1;
			}
			else if (this.Narration.time < Cue[31] - 1)
			{
				this.Narration.time = Cue[31] - 1;
			}
			else if (this.Narration.time < Cue[33] - 1)
			{
				this.Narration.time = Cue[33] - 1;
			}
			else if (this.Narration.time < Cue[39])
			{
				this.Narration.time = Cue[39];
			}
			else if (this.Narration.time < Cue[41] - 1)
			{
				this.Narration.time = Cue[41] - 1;
			}
			else if (this.Narration.time < Cue[50])
			{
				this.Narration.time = Cue[50];
			}

			this.BGM.time = this.Narration.time;

			//SceneManager.LoadScene(SceneNames.PhoneScene);
			//this.Narration.time += 5.0f;
		}

		if (Input.GetKeyDown ("l"))
		{
			GameGlobals.LoveSick = !GameGlobals.LoveSick;
			SceneManager.LoadScene(SceneNames.NewIntroScene);
		}
		#endif

		if (this.New)
		{
			if (this.ID > 19)
			{
				AnimTimer += Time.deltaTime;

				//Debug.Log("Animation has been playing for: " + AnimTimer);
			}

			////////////////////////
			///// LOGO ZOOM-IN /////
			////////////////////////

			if (this.ID > 3 + 49)
			{
				if (!this.Narration.isPlaying)
				{
					if (this.Montage.gameObject.activeInHierarchy)
					{
						this.Darkness.color = new Color(0, 0, 0, 0);
						this.Montage.gameObject.SetActive(false);
						this.PostProcessing.enabled = true;
						this.Label.enabled = false;
						this.GUIPP.enabled = true;
						this.Speed = 0;

						if (GameGlobals.LoveSick)
						{
							this.LoveSickLogo.gameObject.SetActive(true);
						}
						else
						{
							this.Logo.gameObject.SetActive(true);
						}

						DepthOfFieldModel.Settings DepthSettings = this.Profile.depthOfField.settings;
						DepthSettings.focusDistance = 10;
						this.Profile.depthOfField.settings = DepthSettings;

						BloomModel.Settings BloomSettings = this.Profile.bloom.settings;
						BloomSettings.bloom.intensity = 1;
						this.Profile.bloom.settings = BloomSettings;

						GamePad.SetVibration(0, 1, 1);
						this.VibrationCheck = true;
						this.VibrationTimer = .1f;
					}

					this.Logo.transform.localScale = new Vector3(
						this.Logo.transform.localScale.x + (Time.deltaTime * 0.10f),
						this.Logo.transform.localScale.y + (Time.deltaTime * 0.10f),
						this.Logo.transform.localScale.z + (Time.deltaTime * 0.10f));

					this.LoveSickLogo.transform.localScale = new Vector3(
						this.LoveSickLogo.transform.localScale.x + (Time.deltaTime * 0.05f),
						this.LoveSickLogo.transform.localScale.y + (Time.deltaTime * 0.05f),
						this.LoveSickLogo.transform.localScale.z + (Time.deltaTime * 0.05f));

					this.Logo.color = new Color(
						this.Logo.color.r, this.Logo.color.g, this.Logo.color.b, 1.0f);

					this.LoveSickLogo.color = new Color(
						this.LoveSickLogo.color.r, this.LoveSickLogo.color.g, this.LoveSickLogo.color.b, 1.0f);

					this.Speed += Time.deltaTime;

					if (this.Speed > 11)
					{
						SceneManager.LoadScene(SceneNames.PhoneScene);
					}
					else if (this.Speed > 5)
					{
						if (this.Logo.gameObject.activeInHierarchy || this.LoveSickLogo.gameObject.activeInHierarchy)
						{
							this.LoveSickLogo.gameObject.SetActive(false);
							this.Logo.gameObject.SetActive(false);

							GamePad.SetVibration(0, 1, 1);
							this.VibrationCheck = true;
							this.VibrationTimer = .1f;
						}
					}
				}
			}

			///////////////////
			///// MONTAGE /////
			///////////////////

			else if (this.ID > 3 + 48)
			{
				if (!this.Montage.gameObject.activeInHierarchy)
				{
					this.Darkness.color = new Color(0, 0, 0, 0);
					this.Montage.gameObject.SetActive(true);
					this.DeathCorridor.SetActive(false);
					this.PostProcessing.enabled = false;
					this.BloodParent.SetActive(false);
					this.Stalking.SetActive(false);
					this.BGM.volume = 1;
					this.Speed = 0;

					this.SetToDefault();

					DepthOfFieldModel.Settings DepthSettings = this.Profile.depthOfField.settings;
					DepthSettings.focusDistance = 10;
					this.Profile.depthOfField.settings = DepthSettings;

					BloomModel.Settings BloomSettings = this.Profile.bloom.settings;
					BloomSettings.bloom.intensity = .5f;
					this.Profile.bloom.settings = BloomSettings;

					this.VibrationIntensity = 0;
				}

				this.Speed++;

				if (this.Speed > 2)
				{
					this.Speed = 0;

					this.TextureID++;

					if (this.TextureID == 31)
					{
						this.TextureID = 1;
					}

					if (this.TextureID > 10 && this.TextureID < 21)
					{
						this.PostProcessing.enabled = true;
					}
					else
					{
						this.PostProcessing.enabled = false;
					}

					this.Montage.material.mainTexture = Textures[this.TextureID];
				}

				if (this.Timer > 4 + 221)
				{
					this.Darkness.color = new Color(0, 0, 0, 1);
				}
				else
				{
					this.VibrationIntensity += Time.deltaTime * .2f;

					GamePad.SetVibration(0, this.VibrationIntensity, this.VibrationIntensity);
					this.VibrationCheck = true;
					this.VibrationTimer = .1f;
				}
			}

			//////////////////////////
			///// DEATH CORRIDOR /////
			//////////////////////////

			else if (this.ID > 3 + 38)
			{
				if (this.transform.position.z < 0)
				{
					RenderSettings.ambientLight = new Color(.2f, .2f, .2f);

					this.AttackPair[3].SetActive(false);
					this.DeathCorridor.SetActive(true);
					this.Stalking.SetActive(false);
					this.Quad.SetActive(false);

					this.transform.position = new Vector3(0, 1, 0);
					this.transform.eulerAngles = new Vector3(0, 0, -15);

					ColorGradingModel.Settings ColorSettings = this.Profile.colorGrading.settings;

					ColorSettings.basic.saturation = 1;
					ColorSettings.channelMixer.red = new Vector3(1, 0, 0);
					ColorSettings.channelMixer.green = new Vector3(0, 1, 0);
					ColorSettings.channelMixer.blue = new Vector3(0, 0, 1);

					this.Profile.colorGrading.settings = ColorSettings;

					this.Rotation = -15;
					this.Speed = 0;

					this.BGM.volume = .5f;
				}

				this.Speed += Time.deltaTime * .015f;

				this.transform.position = Vector3.Lerp(
						this.transform.position,
						new Vector3(0, 1, 34),
						Time.deltaTime * Speed);

				this.Rotation = Mathf.Lerp(this.Rotation, 15.0f, Time.deltaTime * Speed);

				this.transform.eulerAngles = new Vector3(0, 0, Rotation);
				
				if (this.ID < 51)
				{
					this.Alpha = Mathf.MoveTowards(this.Alpha, 0, Time.deltaTime * .2f);
				}
				else
				{
					this.Alpha = 1;
					//this.BGM.volume = 1;
				}

				this.Darkness.color = new Color(0, 0, 0, Alpha);
			}

			//////////////////////////////////////
			///// HOLE IN CHEST STALKS OSANA /////
			//////////////////////////////////////

			else if (this.ID > 2 + 31)
			{
				if (this.School.activeInHierarchy)
				{
					this.School.SetActive(false);
					this.Stalking.SetActive(true);

					this.transform.position = new Vector3(-.02f, 1.12f, 1);
					this.transform.eulerAngles = new Vector3(0, 0, 0);

					this.SetToDefault();

					this.Speed = 0;
				}

				if (this.ID < 40)
				{
					this.VibrationIntensity += Time.deltaTime * .05f;

					GamePad.SetVibration(0, this.VibrationIntensity, this.VibrationIntensity);
					this.VibrationCheck = true;
					this.VibrationTimer = .1f;
				}

				if (this.ID < 2 + 35)
				{
					this.Speed += Time.deltaTime * .1f;

					this.transform.position = Vector3.Lerp(
						this.transform.position,
						new Vector3(-.02f, 1.12f, -.25f),
						Time.deltaTime * Speed);

					this.Alpha = Mathf.MoveTowards(this.Alpha, 0, Time.deltaTime * .2f);
				}
				else
				{
					if (this.Speed > 0)
					{
						this.CameraRotationY = 0;
						this.Speed = 0;
					}

					this.Speed -= Time.deltaTime * .1f;

					this.transform.position = Vector3.Lerp(
						this.transform.position,
						new Vector3(.3f, .8f, -.25f),
						Time.deltaTime * Speed * -1);

					this.CameraRotationY = Mathf.Lerp(
						this.CameraRotationY,
						-15,
						Time.deltaTime * Speed * -1);

					this.transform.eulerAngles = new Vector3(0, CameraRotationY, 0);

					if (this.Timer > 4 + 175f)
					{
						this.StalkerAnim.Play("f02_clenchFist_00");
					}

					//Even if it means killing her.
					if (this.ID == 40)
					{
						this.Alpha = 1;
					}

					if (this.ID > 39)
					{
						this.BGM.volume += Time.deltaTime;
					}

					if (this.Timer > 186f)
					{
						this.DeathCorridor.SetActive(true);
						this.Alpha = 1;
					}
					else if (this.Timer > 5 + 180.6f)
					{
						this.AttackPair[2].SetActive(false);
						this.AttackPair[3].SetActive(true);
						GamePad.SetVibration(0, 1, 1);
						this.VibrationCheck = true;
						this.VibrationTimer = .2f;

						this.Alpha = 0;
					}
					else if (this.Timer > 5 + 180.2f)
					{
						this.Alpha = 1;
					}
					else if (this.Timer > 5 + 179.8f)
					{
						this.AttackPair[1].SetActive(false);
						this.AttackPair[2].SetActive(true);
						GamePad.SetVibration(0, 1, 1);
						this.VibrationCheck = true;
						this.VibrationTimer = .2f;

						this.Alpha = 0;
					}
					else if (this.Timer > 5 + 179.4f)
					{
						this.Alpha = 1;
					}
					else if (this.Timer > 184)
					{
						ColorGradingModel.Settings ColorSettings = this.Profile.colorGrading.settings;

						ColorSettings.channelMixer.red = new Vector3(.1f, 0, 0);
						ColorSettings.channelMixer.green = Vector3.zero;
						ColorSettings.channelMixer.blue = Vector3.zero;

						this.Profile.colorGrading.settings = ColorSettings;

						this.Alpha = 0;

						this.Stalking.SetActive(false);
						this.Quad.SetActive(true);

						this.AttackPair[1].SetActive(true);
						GamePad.SetVibration(0, 1, 1);
						this.VibrationCheck = true;
						this.VibrationTimer = .2f;
					}
				}

				this.Darkness.color = new Color(0, 0, 0, Alpha);
			}

			///////////////////////////////////
			///// SEEING OSANA FIRST TIME /////
			///////////////////////////////////

			else if (this.ID > 31)
			{
				if (!this.Osana.activeInHierarchy)
				{
					this.transform.position = new Vector3(.5f, 1.366666f, .25f);
					this.transform.eulerAngles = new Vector3(15, -67.5f, 0);

					this.SenpaiAnim.transform.parent.localPosition = new Vector3(0.533333f, 0, -6.9f);
					this.SenpaiAnim.transform.parent.localEulerAngles = new Vector3(0, 90, 0);
					this.SenpaiAnim.Play("Monday_1");

					this.Osana.SetActive(true);

					DepthOfFieldModel.Settings DepthSettings = this.Profile.depthOfField.settings;
					DepthSettings.focusDistance = 1.5f;
					this.Profile.depthOfField.settings = DepthSettings;

					this.YandereAnim["f02_Intro_00"].speed = .33333f;

					this.Darkness.color = new Color(0, 0, 0, 0);
					this.Alpha = 0;
				}

				this.transform.Translate(Vector3.forward * .01f * Time.deltaTime, Space.Self);

				this.TurnRed();

				if (this.Narration.time > 156.2f)
				{
					this.Darkness.color = new Color(0, 0, 0, 1);
					this.Alpha = 1;
				}
			}

			/////////////////////////////
			///// SENPAI WALKS AWAY /////
			/////////////////////////////

			else if (this.ID > 27)
			{
				if (this.transform.position.x > 0)
				{
					this.transform.position = new Vector3(-1.5f, 1, -1.5f);
					this.transform.eulerAngles = new Vector3(0, 45, 0);

					this.YandereAnim["f02_Intro_00"].time += 0;
					this.SenpaiAnim["Intro_00"].time += 0;

					this.YandereAnim["f02_Intro_00"].speed = 1.33333f;
					this.SenpaiAnim["Intro_00"].speed = 1.33333f;

					this.Speed = 0;

					DepthOfFieldModel.Settings DepthSettings = this.Profile.depthOfField.settings;
					DepthSettings.focusDistance = 1.5f;
					DepthSettings.aperture = 11.2f;
					this.Profile.depthOfField.settings = DepthSettings;
				}

				if (this.ID > 28)
				{
					this.Speed += Time.deltaTime * .1f;

					this.transform.position = Vector3.Lerp(
						this.transform.position,
						new Vector3(-1.1f, 1.33333f, 1),
						Time.deltaTime * Speed);

					this.transform.eulerAngles = Vector3.Lerp(
						this.transform.eulerAngles,
						new Vector3(0, 135, 0),
						Time.deltaTime * Speed);
				}

				if (this.ID > 2 + 27)
				{
					this.Stars.Stop();
					this.Bubbles.Stop();
				}

				if (this.ID > 2 + 28)
				{
					this.TurnNeutral();
				}
			}

			//////////////////////////////////
			///// YANDERE-CHAN STANDS UP /////
			//////////////////////////////////

			else if (this.ID > 23)
			{
				if (this.EyeTimer == 0)
				{
					this.transform.position = new Vector3(0, .9f, 0);
					this.transform.eulerAngles = new Vector3(15f, -45f, 0);

					this.YandereAnim["f02_Intro_00"].speed = .2f;
					this.SenpaiAnim["Intro_00"].speed = .2f;

					this.Yandere.materials[2].SetFloat("_BlendAmount", 1.0f);

					DepthOfFieldModel.Settings DepthSettings = this.Profile.depthOfField.settings;
					DepthSettings.focusDistance = 1.5f;
					DepthSettings.aperture = 11.2f;
					this.Profile.depthOfField.settings = DepthSettings;
				}

				this.EyeTimer += Time.deltaTime;

				if (this.EyeTimer > .1f)
				{
					this.Yandere.materials[2].SetTextureOffset("_OverlayTex", new Vector2(Random.Range(-.01f, .01f), Random.Range(-.01f, .01f)));
					this.EyeTimer -= .1f;
				}

				this.transform.Translate(Vector3.forward * -.1f * Time.deltaTime, Space.Self);
			}

			////////////////////////////////////
			///// MEETING SENPAI AT SCHOOL /////
			////////////////////////////////////

			else if (this.ID > 19)
			{
				if (this.Room.gameObject.activeInHierarchy)
				{
					this.Room.gameObject.SetActive(false);
					this.School.SetActive(true);

					this.transform.localPosition = new Vector3(-3f, 1, 1.5f);

					this.Darkness.color = new Color(0, 0, 0, 1);
					this.Alpha = 1;
					this.Speed = 0;

					ColorGradingModel.Settings ColorSettings = this.Profile.colorGrading.settings;
					ColorSettings.basic.saturation = 0;
					this.Profile.colorGrading.settings = ColorSettings;

					DepthOfFieldModel.Settings DepthSettings = this.Profile.depthOfField.settings;
					DepthSettings.focusDistance = 10;
					DepthSettings.aperture = 5.6f;
					this.Profile.depthOfField.settings = DepthSettings;

					this.Yandere.materials[2].SetFloat("_BlendAmount", 0.0f);
				}

				if (this.Narration.time < 3.898f + 91)
				{
					this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(0, 1, -2), Time.deltaTime * 1.09f);
				}
				else
				{
					DepthOfFieldModel.Settings DepthSettings = this.Profile.depthOfField.settings;
					DepthSettings.focusDistance = 1.2f;
					DepthSettings.aperture = 11.2f;
					this.Profile.depthOfField.settings = DepthSettings;

					if (this.Narration.time < 101.5f)
					{
						this.transform.position = new Vector3(-.25f, .75f, -.25f);
						this.transform.eulerAngles = new Vector3(22.5f, -45, 0);

						this.Senpai.transform.position = new Vector3(0, -.2f, 0);
					}
					else
					{
						this.Speed += Time.deltaTime * .5f;

						this.Senpai.transform.position = Vector3.Lerp(
							this.Senpai.transform.position,
							new Vector3(0, 0, 0),
							Time.deltaTime * Speed * 2);

						this.transform.position = Vector3.Lerp(
							this.transform.position,
							new Vector3(-.25f, .66666f, -1.25f),
							Time.deltaTime * Speed * .5f);

						this.CameraRotationX = Mathf.Lerp(this.CameraRotationX, 0, Time.deltaTime * Speed);
						this.CameraRotationY = Mathf.Lerp(this.CameraRotationY, 0, Time.deltaTime * Speed);

						this.transform.eulerAngles = new Vector3(CameraRotationX, CameraRotationY, 0);
					}
				}

				if (this.ID > 2 + 19)
				{
					this.YandereAnim["f02_Intro_00"].speed = Mathf.MoveTowards(this.YandereAnim["f02_Intro_00"].speed, .1f, Time.deltaTime);
					this.SenpaiAnim["Intro_00"].speed = Mathf.MoveTowards(this.SenpaiAnim["Intro_00"].speed, .1f, Time.deltaTime);

					if (this.Narration.time > 2 + 104.5f)
					{
						ColorGradingModel.Settings ColorSettings = this.Profile.colorGrading.settings;
						ColorSettings.basic.saturation = Mathf.MoveTowards(ColorSettings.basic.saturation, 2, Time.deltaTime);

						ColorSettings.channelMixer.red = Vector3.MoveTowards(
							ColorSettings.channelMixer.red, 
							new Vector3(2, 0, 0),
							Time.deltaTime);

						ColorSettings.channelMixer.blue = Vector3.MoveTowards(
							ColorSettings.channelMixer.blue, 
							new Vector3(0, 0, 2),
							Time.deltaTime);

						this.Profile.colorGrading.settings = ColorSettings;

						this.Particles.SetActive(true);
					}
				}
				else
				{
					if (this.Narration.time > 98)
					{
						this.YandereAnim["f02_Intro_00"].speed = 1;
						this.SenpaiAnim["Intro_00"].speed = 1;
					}
					else if (this.Narration.time > 97)
					{
						this.YandereAnim["f02_Intro_00"].speed = 3;
						this.SenpaiAnim["Intro_00"].speed = 3;
					}
				}

				this.Alpha = Mathf.MoveTowards(this.Alpha, 0, Time.deltaTime * .2f);

				this.Darkness.color = new Color(0, 0, 0, Alpha);
			}

			/////////////////////////////
			///// IN ROOM AT WINDOW /////
			/////////////////////////////

			else if (this.ID > 2 + 13)
			{
				if (this.ParentAndChild.gameObject.activeInHierarchy)
				{
					this.ParentAndChild.gameObject.SetActive(false);
					this.Room.SetActive(true);

					this.transform.position = new Vector3(0, 1, 0);

					this.Darkness.color = new Color(0, 0, 0, 1);
					this.Alpha = 1;
					this.Speed = .1f;

					DepthOfFieldModel.Settings DepthSettings = this.Profile.depthOfField.settings;
					DepthSettings.focusDistance = 10;
					this.Profile.depthOfField.settings = DepthSettings;
				}

				this.transform.position -= new Vector3(0, 0, Time.deltaTime * Speed * .75f);

				if (this.Narration.time > 3.898f + 85)
				{
					this.Alpha = Mathf.MoveTowards(this.Alpha, 1, Time.deltaTime * .66666f); 
				}
				else
				{
					this.Alpha = Mathf.MoveTowards(this.Alpha, 0, Time.deltaTime * .2f);
				}

				this.Darkness.color = new Color(0, 0, 0, Alpha);
			}

			///////////////////////////////
			///// MOTHER AND DAUGHTER /////
			///////////////////////////////

			else if (this.ID > 2 + 11)
			{
				if (this.ConfessionScene.gameObject.activeInHierarchy)
				{
					this.ConfessionScene.gameObject.SetActive(false);
					this.ParentAndChild.SetActive(true);

					this.X = 220; this.Y = -90;
					this.X2 = 150; this.Y2 = -90;

					this.transform.position = new Vector3(0, .5f, -1);

					this.Darkness.color = new Color(0, 0, 0, 1);
					this.Alpha = 1;
					this.Speed = .1f;

					ColorGradingModel.Settings ColorSettings = this.Profile.colorGrading.settings;
					ColorSettings.basic.saturation = 1;
					this.Profile.colorGrading.settings = ColorSettings;

					BloomModel.Settings BloomSettings = this.Profile.bloom.settings;
					BloomSettings.bloom.intensity = 1;
					this.Profile.bloom.settings = BloomSettings;

					DepthOfFieldModel.Settings DepthSettings = this.Profile.depthOfField.settings;
					DepthSettings.focusDistance = 10;
					DepthSettings.aperture = 11.2f;
					this.Profile.depthOfField.settings = DepthSettings;
				}

				this.transform.position -= new Vector3(0, 0, Time.deltaTime * Speed);

				if (this.Narration.time > 3.898f + 66)
				{
					this.Alpha = Mathf.MoveTowards(this.Alpha, 1, Time.deltaTime * .5f); 
				}
				else
				{
					this.Alpha = Mathf.MoveTowards(this.Alpha, 0, Time.deltaTime * .2f);
				}

				this.Darkness.color = new Color(0, 0, 0, Alpha);
			}

			///////////////////////////
			///// TREE CONFESSION /////
			///////////////////////////

			else if (this.ID > 2 + 7)
			{
				if (this.HoleInChestAnim.gameObject.activeInHierarchy)
				{
					this.HoleInChestAnim.gameObject.SetActive(false);
					this.ConfessionScene.SetActive(true);

					this.transform.position = new Vector3(0, 1, -1);

					this.Darkness.color = new Color(0, 0, 0, 1);
					this.Alpha = 1;
					this.Speed = .1f;

					DepthOfFieldModel.Settings DepthSettings = this.Profile.depthOfField.settings;
					DepthSettings.focusDistance = 1;
					this.Profile.depthOfField.settings = DepthSettings;

					ColorGradingModel.Settings ColorSettings = this.Profile.colorGrading.settings;
					ColorSettings.basic.saturation = 0;
					this.Profile.colorGrading.settings = ColorSettings;
				}

				this.transform.position -= new Vector3(0, 0, Time.deltaTime * Speed);

				if (this.ID > 2 + 8)
				{
					this.YoungRyobaAnim["f02_introHoldHands_00"].speed = .5f;
					this.YoungRyobaAnim.Play();

					this.YoungFatherAnim["introHoldHands_00"].speed = .5f;
					this.YoungFatherAnim.Play();

					Brightness = Mathf.MoveTowards(this.Brightness, 1, Time.deltaTime * .25f);
					BrightenEnvironment();

					Blossoms.Play();
				}

				if (this.ID > 2 + 9)
				{
					ColorGradingModel.Settings ColorSettings = this.Profile.colorGrading.settings;
					ColorSettings.basic.saturation += Time.deltaTime * .25f;

					BloomModel.Settings BloomSettings = this.Profile.bloom.settings;
					BloomSettings.bloom.intensity = 1 + (ColorSettings.basic.saturation);
					this.Profile.bloom.settings = BloomSettings;

					this.Profile.colorGrading.settings = ColorSettings;
				}

				if (this.Narration.time > 3.898f + 53)
				{
					this.Alpha = Mathf.MoveTowards(this.Alpha, 1, Time.deltaTime * .5f); 
				}
				else
				{
					this.Alpha = Mathf.MoveTowards(this.Alpha, 0, Time.deltaTime * .2f);
				}

				this.Darkness.color = new Color(0, 0, 0, Alpha);
			}

			/////////////////////////
			///// HOLE IN CHEST /////
			/////////////////////////

			else if (this.ID > 2 + 2)
			{
				if (this.BloodyHandsAnim.gameObject.activeInHierarchy)
				{
					this.transform.position = new Vector3(0.012f, 1.13f, 0.029f);
					this.transform.eulerAngles = Vector3.zero;

					this.BloodyHandsAnim.gameObject.SetActive(false);
					this.HoleInChestAnim.gameObject.SetActive(true);

					this.Alpha = 0;
					this.Darkness.color = new Color(0, 0, 0, Alpha);

					this.SetToDefault();
				}

				//this.Speed = .1f;
				this.Speed = Mathf.MoveTowards(this.Speed, .1f, Time.deltaTime * .01f);

				this.transform.position -= new Vector3(0, 0, Time.deltaTime * Speed);

				if (this.transform.position.z < -.05f)
				{
					this.SecondSpeed = Mathf.MoveTowards(this.SecondSpeed, .1f, Time.deltaTime * .1f);
					
					this.transform.position = Vector3.Lerp(
						this.transform.position,
						new Vector3(this.transform.position.x, 1, this.transform.position.z),
						Time.deltaTime * SecondSpeed);
				}

				if (this.transform.position.z < -.5f)
				{
					this.HoleInChestAnim["f02_holeInChest_00"].speed = .5f;
					this.HoleInChestAnim.Play();
				}

				if (this.ID > 2 + 6)
				{
					this.Alpha = Mathf.MoveTowards(this.Alpha, 1, Time.deltaTime * .2f);

					this.Darkness.color = new Color(0, 0, 0, Alpha);
				}
			}

			////////////////////////
			///// BLOODY HANDS /////
			////////////////////////

			else if (this.ID > 0)
			{
				if (this.Timer < 2)
				{
					this.BloodyHandsAnim["f02_clenchFists_00"].time = .6f;
					this.BloodyHandsAnim["f02_clenchFists_00"].speed = 0;
				}
				else
				{
					this.BloodyHandsAnim["f02_clenchFists_00"].speed = .07f;
				}

				//Debug.Log(this.BloodyHandsAnim["f02_clenchFists_00"].time);

				if (this.ID > 3)
				{
					this.Alpha = 1;
					this.Darkness.color = new Color(0, 0, 0, Alpha);

					this.BGM.volume = Mathf.MoveTowards(this.BGM.volume, .5f, Time.deltaTime * .266666f);
				}
			}
		}
	}

	public Transform RightHair;
	public Transform LeftHair;
	public Transform Ponytail;

	public Transform RightHair2;
	public Transform LeftHair2;
	public Transform Ponytail2;

	public Transform BookRight;
	public Transform BookLeft;

	public Transform LeftArm;
	public Transform Neck;

	public float Rotation = 0;
	public float Weight = 0;

	public float X;public float Y;public float Z;
	public float X2;public float Y2;public float Z2;

	void LateUpdate()
	{
		if (this.New)
		{
			if (this.ID < 2 + 39)
			{
				if (this.Narration.time > 103f)
				{
					//this.ThirdSpeed += Time.deltaTime;

					//Rotation = Mathf.Lerp(Rotation, 0, Time.deltaTime * 3.6f * ThirdSpeed);

					if (this.ID < 2 + 22)
					{
						this.LeftArm.localEulerAngles = new Vector3(0, 15, 0);
						this.BookRight.localEulerAngles = new Vector3(0, 180, -90);
						this.BookLeft.localEulerAngles = new Vector3(0, 180, 0);

						this.BookRight.parent.parent.localPosition = new Vector3(-.12f, -.04f, 0);
						this.BookRight.parent.parent.localEulerAngles = new Vector3(0, -15, -135);

						this.BookRight.parent.parent.parent.localEulerAngles = new Vector3(45, 45, 0);
					}
					else
					{
						this.BookRight.parent.parent.localPosition = new Vector3(-.075f, -.085f, 0);
						this.BookRight.parent.parent.localEulerAngles = new Vector3(0, -45, -90);

						this.BookRight.localEulerAngles = new Vector3(0, 180, -45);
						this.BookLeft.localEulerAngles = new Vector3(0, 180, 45);
					}
				}
				else if (this.Narration.time > 3.898f + 91)
				{
					Rotation = 22.5f;
				}
			}

			if (this.Narration.time > 104f && this.Narration.time < 190)
			{
				this.ThirdSpeed += Time.deltaTime;

				Rotation = Mathf.Lerp(Rotation, 0, Time.deltaTime * 3.6f * ThirdSpeed);
			}

			Neck.localEulerAngles += new Vector3(Rotation, 0, 0);

			//Yandere-chan opens her eyes.
			if (this.Narration.time > 99f)
			{
				Weight = Mathf.MoveTowards(Weight, 0, Time.deltaTime * 20);
			}
			//Yandere-chan's eyes are half-closed.
			else if (this.Narration.time > 3.898f + 92.75f)
			{
				Weight = Mathf.MoveTowards(Weight, 50, Time.deltaTime * 100);
			}

			Yandere.SetBlendShapeWeight(5, Weight);

			this.Ponytail.transform.eulerAngles = new Vector3(X, Y, Z);
			this.RightHair.transform.eulerAngles = new Vector3(X2, Y2, Z2);
			this.LeftHair.transform.eulerAngles = new Vector3(X2, Y2, Z2);

			this.Ponytail2.transform.eulerAngles = new Vector3(X, Y, Z);
			this.RightHair2.transform.eulerAngles = new Vector3(X2, Y2, Z2);
			this.LeftHair2.transform.eulerAngles = new Vector3(X2, Y2, Z2);

			this.RightHand.localEulerAngles += new Vector3(
				Random.Range(1.0f, -1.0f),
				Random.Range(1.0f, -1.0f),
				Random.Range(1.0f, -1.0f));

			/*
			this.RightForeArm.transform.localEulerAngles += new Vector3(45, 0, 0);
			this.LeftForeArm.transform.localEulerAngles += new Vector3(45, 0, 0);

			this.RightWrist.transform.localEulerAngles += new Vector3(45, 0, 0);
			this.LeftWrist.transform.localEulerAngles += new Vector3(45, 0, 0);
			*/
		}
	}

	/////////////////////////////
	/////////////////////////////
	/////////////////////////////
	/////////////////////////////
	/////////////////////////////
	/////////////////////////////
	/////////////////////////////
	/////////////////////////////
	/////////////////////////////
	/////////////////////////////
	/// POST-PROCESSING STUFF ///
	/////////////////////////////
	/////////////////////////////
	/////////////////////////////
	/////////////////////////////
	/////////////////////////////
	/////////////////////////////
	/////////////////////////////
	/////////////////////////////
	/////////////////////////////
	/////////////////////////////

	void LoveSickCheck()
	{
		if (GameGlobals.LoveSick)
		{
			Camera.main.backgroundColor = new Color (0, 0, 0, 1);

			GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

			foreach(GameObject go in allObjects)
			{
				UISprite sprite = go.GetComponent<UISprite> ();

				if (sprite != null)
				{
					sprite.color = new Color(1.0f, 0.0f, 0.0f, sprite.color.a);
				}

				UITexture texture = go.GetComponent<UITexture> ();

				if (texture != null)
				{
					texture.color = new Color(1.0f, 0.0f, 0.0f, texture.color.a);
				}

				UILabel label = go.GetComponent<UILabel> ();

				if (label != null)
				{
					if (label.color != Color.black)
					{
						label.color = new Color(1.0f, 0.0f, 0.0f, label.color.a);
					}
				}
			}

			FadeOutDarkness.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
			LoveSickLogo.enabled = true;
			Logo.enabled = false;
		}
		else
		{
			LoveSickLogo.enabled = false;
		}
	}

	void BrightenEnvironment()
	{
		//Debug.Log("Brightness is: " + Brightness);

		this.TreeRenderer[0].materials[0].color = new Color(Brightness, Brightness, Brightness, 1);
		this.TreeRenderer[0].materials[1].color = new Color(Brightness, Brightness, Brightness, 1);

		this.TreeRenderer[1].materials[0].color = new Color(Brightness, Brightness, Brightness, 1);
		this.TreeRenderer[1].materials[1].color = new Color(Brightness, Brightness, Brightness, 1);

		this.TreeRenderer[2].materials[0].color = new Color(Brightness, Brightness, Brightness, 1);
		this.TreeRenderer[2].materials[1].color = new Color(Brightness, Brightness, Brightness, 1);

		this.Mound.material.color = new Color(Brightness, Brightness, Brightness, 1);
		this.Sky.material.color = new Color(Brightness, Brightness, Brightness, 1);

		this.YoungFatherRenderer.materials[0].color = new Color(Brightness, Brightness, Brightness, 1);
		this.YoungFatherRenderer.materials[1].color = new Color(Brightness, Brightness, Brightness, 1);
		this.YoungFatherRenderer.materials[2].color = new Color(Brightness, Brightness, Brightness, 1);

		this.YoungFatherHairRenderer.material.color = new Color(Brightness * .5f, Brightness * .5f, Brightness * .5f, 1);
	}

	void TurnNeutral()
	{
		ColorGradingModel.Settings ColorSettings = this.Profile.colorGrading.settings;

		ColorSettings.channelMixer.red = Vector3.MoveTowards(
			ColorSettings.channelMixer.red, 
			new Vector3(1, 0, 0),
			Time.deltaTime * .66666f);

		ColorSettings.channelMixer.green = Vector3.MoveTowards(
			ColorSettings.channelMixer.green, 
			new Vector3(0, 1, 0),
			Time.deltaTime * .66666f);

		ColorSettings.channelMixer.blue = Vector3.MoveTowards(
			ColorSettings.channelMixer.blue, 
			new Vector3(0, 0, 1),
			Time.deltaTime * .66666f);

		this.Profile.colorGrading.settings = ColorSettings;
	}

	void TurnRed()
	{
		ColorGradingModel.Settings ColorSettings = this.Profile.colorGrading.settings;

		ColorSettings.basic.saturation = Mathf.MoveTowards(
			ColorSettings.basic.saturation,
			1,
			Time.deltaTime * .1f);

		ColorSettings.channelMixer.red = Vector3.MoveTowards(
			ColorSettings.channelMixer.red, 
			new Vector3(1, 0, 0),
			Time.deltaTime * .1f);

		ColorSettings.channelMixer.green = Vector3.MoveTowards(
			ColorSettings.channelMixer.green, 
			new Vector3(0, .5f, 0),
			Time.deltaTime * .1f);

		ColorSettings.channelMixer.blue = Vector3.MoveTowards(
			ColorSettings.channelMixer.blue, 
			new Vector3(0, 0, .5f),
			Time.deltaTime * .1f);

		this.Profile.colorGrading.settings = ColorSettings;
	}

	void SetToDefault()
	{
		ColorGradingModel.Settings ColorSettings = this.Profile.colorGrading.settings;
		ColorSettings.basic.saturation = 1;
		ColorSettings.channelMixer.red = new Vector3(1, 0, 0);
		ColorSettings.channelMixer.green = new Vector3(0, 1, 0);
		ColorSettings.channelMixer.blue = new Vector3(0, 0, 1);
		this.Profile.colorGrading.settings = ColorSettings;

		DepthOfFieldModel.Settings DepthSettings = this.Profile.depthOfField.settings;
		DepthSettings.focusDistance = 10;
		DepthSettings.aperture = 11.2f;
		this.Profile.depthOfField.settings = DepthSettings;

		BloomModel.Settings BloomSettings = this.Profile.bloom.settings;
		BloomSettings.bloom.intensity = 1;
		this.Profile.bloom.settings = BloomSettings;
	}
}