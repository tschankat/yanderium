using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenuScript : MonoBehaviour
{
	public ColorCorrectionCurves ColorCorrection;
	public InputManagerScript InputManager;
	public TitleSaveFilesScript SaveFiles;
	public SelectiveGrayscale Grayscale;
	public TitleSponsorScript Sponsors;
	public TitleExtrasScript Extras;
	public PromptBarScript PromptBar;
	public SSAOEffect SSAO;
	public JsonScript JSON;

	public UISprite[] MediumSprites;
	public UISprite[] LightSprites;
	public UISprite[] DarkSprites;

	public UILabel TitleLabel;
	public UILabel SimulatorLabel;
	public UILabel[] ColoredLabels;

	public Color MediumColor;
	public Color LightColor;
	public Color DarkColor;

	public Transform VictimHead;
	public Transform RightHand;
	public Transform TwintailL;
	public Transform TwintailR;

	public Animation LoveSickYandere;

	public GameObject BloodProjector;
	public GameObject LoveSickLogo;
	public GameObject BloodCamera;
	public GameObject Yandere;
	public GameObject Knife;
	public GameObject Logo;
	public GameObject Sun;

	public AudioSource LoveSickMusic;
	public AudioSource CuteMusic;
	public AudioSource DarkMusic;

	public Renderer[] YandereEye;

	public Material CuteSkybox;
	public Material DarkSkybox;

	public Transform Highlight;
	public Transform[] Spine;
	public Transform[] Arm;

	public UISprite Darkness;

	public Vector3 PermaPositionL;
	public Vector3 PermaPositionR;

	public bool NeverChange = false;
	public bool LoveSick = false;
	public bool FadeOut = false;
	public bool Turning = false;
	public bool Fading = true;

	public int SelectionCount = 8;
	public int Selected = 0;

	public float InputTimer = 0.0f;
	public float FadeSpeed = 1.0f;
	public float LateTimer = 0.0f;
	public float RotationY = 0.0f;
	public float RotationZ = 0.0f;
	public float Volume = 0.0f;
	public float Timer = 0.0f;

	void Awake()
	{
		// [af] Moved from class scope for compatibility with C#.
		Animation yandereAnim = this.Yandere.GetComponent<Animation>();
		yandereAnim[AnimNames.FemaleYanderePose].layer = 1;
		yandereAnim.Blend(AnimNames.FemaleYanderePose);

		yandereAnim[AnimNames.FemaleFist].layer = 2;
		yandereAnim.Blend(AnimNames.FemaleFist);
	}

	void Start()
	{
        Cursor.visible = false;

		if (GameGlobals.LoveSick)
		{
			LoveSick = true;
		}

		this.PromptBar.Label[0].text = "Confirm";
		this.PromptBar.Label[1].text = string.Empty;
		this.PromptBar.UpdateButtons();
		
		this.MediumColor = this.MediumSprites[0].color;
		this.LightColor = this.LightSprites[0].color;
		this.DarkColor = this.DarkSprites[0].color;

		if (!LoveSick)
		{
			transform.position = new Vector3(transform.position.x, 1.2f, transform.position.z);

			this.LoveSickLogo.SetActive(false);
			this.LoveSickMusic.volume = 0.0f;
			this.Grayscale.enabled = false;
			this.SSAO.enabled = false;
			this.Sun.SetActive(true);

			this.Darkness.color = new Color(
				this.Darkness.color.r,
				this.Darkness.color.g,
				this.Darkness.color.b,
				1.0f);

			this.TurnCute();

			RenderSettings.ambientLight = new Color(0.75f, 0.75f, 0.75f, 1.0f);
			RenderSettings.skybox.SetColor("_Tint", new Color(0.50f, 0.50f, 0.50f));
		}
		else
		{
			this.transform.position = new Vector3(transform.position.x, 101.2f, transform.position.z);
			//this.Grayscale.enabled = true;
			this.Sun.SetActive(false);
			this.SSAO.enabled = true;
			this.FadeSpeed = .2f;

			this.Darkness.color = new Color(
				0,
				0,
				0,
				1.0f);

			this.TurnLoveSick();
		}

		Time.timeScale = 1.0f;
	}

	void Update()
	{
        Cursor.visible = false;

        if (LoveSick)
		{
			this.Timer += Time.deltaTime * .001f;

			if (this.transform.position.z > -18.0f)
			{
				this.LateTimer = Mathf.Lerp(this.LateTimer, this.Timer, Time.deltaTime);

				this.RotationY = Mathf.Lerp(this.RotationY, -22.5f, Time.deltaTime * LateTimer);
			}
				
			this.RotationZ = Mathf.Lerp(this.RotationZ, 22.5f, Time.deltaTime * Timer);

			this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(0.33333f, 101.45f, -16.5f), Time.deltaTime * Timer);
			this.transform.eulerAngles = new Vector3(0, this.RotationY, this.RotationZ);

			if (!this.Turning)
			{
				if (this.transform.position.z > -17.0f)
				{
					this.LoveSickYandere.CrossFade("f02_edgyTurn_00");
					this.VictimHead.parent = this.RightHand;
					this.Turning = true;
				}
			}
			else
			{
				if (this.LoveSickYandere["f02_edgyTurn_00"].time >= this.LoveSickYandere["f02_edgyTurn_00"].length)
				{
					this.LoveSickYandere.CrossFade("f02_edgyOverShoulder_00");
				}
			}
		}

		if (!this.Sponsors.Show && !this.SaveFiles.Show && !this.Extras.Show)
		{
			this.InputTimer += Time.deltaTime;

			if (this.InputTimer > 1)
			{
				if (this.InputManager.TappedDown)
				{
					this.Selected = (this.Selected < (this.SelectionCount - 1)) ?
						(this.Selected + 1) : 0;
				}

				if (this.InputManager.TappedUp)
				{
					this.Selected = (this.Selected > 0) ?
						(this.Selected - 1) : (this.SelectionCount - 1);
				}

				bool selectionChanged = this.InputManager.TappedUp ||
					this.InputManager.TappedDown;

				if (selectionChanged)
				{
					this.Highlight.localPosition = new Vector3(
						this.Highlight.localPosition.x,
						225.0f - (75.0f * this.Selected),
						this.Highlight.localPosition.z);
				}

				if (Input.GetButtonDown(InputNames.Xbox_A))
				{
				        //Debug Quick Start   //Mission Mode
					if (this.Selected == 0 || this.Selected == 2 ||
						//Credits             //Exit
						this.Selected == 5 || this.Selected == 7)
					{
						this.Darkness.color = new Color(0.0f, 0.0f, 0.0f, this.Darkness.color.a);
						this.InputTimer = -10;
						this.FadeOut = true;
						this.Fading = true;
					}
					//Save Files
					else if (this.Selected == 1)
					{
						this.PromptBar.Label[0].text = "Select";
						this.PromptBar.Label[1].text = "Back";
						this.PromptBar.Label[2].text = "Delete";
						this.PromptBar.UpdateButtons();

						this.SaveFiles.Show = true;
					}
					//Sponsors
					else if (this.Selected == 3)
					{
						this.PromptBar.Label[0].text = "Visit";
						this.PromptBar.Label[1].text = "Back";
						this.PromptBar.UpdateButtons();

						this.Sponsors.Show = true;
					}
					//Extras
					else if (this.Selected == 6)
					{
						this.PromptBar.Label[1].text = "Back";
						this.PromptBar.UpdateButtons();

						this.Extras.Show = true;
					}
					
					if (!LoveSick)
					{
						this.TurnCute();
					}
				}
					
				if (Input.GetKeyDown ("l"))
				{
					GameGlobals.LoveSick = !GameGlobals.LoveSick;
					SceneManager.LoadScene(SceneNames.TitleScene);
				}

				if (Input.GetKeyDown ("m"))
				{
					//TitleLabel.text = "MANDERE";
				}

				if (!LoveSick)
				{
					if (NeverChange)
					{
						Timer = 0f;
					}

					if (Input.GetKeyDown(KeyCode.Space))
					{
						this.Timer = 10.0f;
					}

					this.Timer += Time.deltaTime;

					if (this.Timer > 10.0f)
					{
						this.TurnDark();
					}

					if (this.Timer > 11.0f)
					{
						this.TurnCute();
					}
				}
			}
		}
		else
		{
			if (this.Sponsors.Show)
			{
				int sponsorIndex = this.Sponsors.GetSponsorIndex();

				if (this.Sponsors.SponsorHasWebsite(sponsorIndex))
				{
					this.PromptBar.Label[0].text = "Visit";
					this.PromptBar.UpdateButtons();
				}
				else
				{
					this.PromptBar.Label[0].text = string.Empty;
					this.PromptBar.UpdateButtons();
				}
			}
			else if (this.SaveFiles.Show)
			{
				if (this.SaveFiles.SaveDatas[this.SaveFiles.ID].EmptyFile.activeInHierarchy)
				{
					this.PromptBar.Label[0].text = "Create New";
					this.PromptBar.Label[2].text = "";
					this.PromptBar.UpdateButtons();
				}
				else
				{
					this.PromptBar.Label[0].text = "Load";
					this.PromptBar.Label[2].text = "Delete";
					this.PromptBar.UpdateButtons();
				}
			}

			if (Input.GetButtonDown(InputNames.Xbox_B))
			{
				if (!this.SaveFiles.ConfirmationWindow.activeInHierarchy)
				{
					this.SaveFiles.Show = false;
					this.Sponsors.Show = false;
					this.Extras.Show = false;

					this.PromptBar.Label[0].text = "Confirm";
					this.PromptBar.Label[1].text = string.Empty;
					this.PromptBar.Label[2].text = string.Empty;
					this.PromptBar.UpdateButtons();
				}
			}
		}

		if (this.Fading)
		{
			if (!this.FadeOut)
			{
				if (this.Darkness.color.a > 0.0f)
				{
					this.Darkness.color = new Color(
						this.Darkness.color.r,
						this.Darkness.color.g,
						this.Darkness.color.b,
						this.Darkness.color.a - Time.deltaTime * this.FadeSpeed);

					if (this.Darkness.color.a <= 0.0f)
					{
						this.Darkness.color = new Color(
							this.Darkness.color.r,
							this.Darkness.color.g,
							this.Darkness.color.b,
							0.0f);

						this.Fading = false;
					}
				}
			}
			//When the screen is fading out, what screen do we go to?
			else
			{
				if (this.Darkness.color.a < 1.0f)
				{
					MissionModeGlobals.MissionMode = false;

					this.Darkness.color = new Color(
						this.Darkness.color.r,
						this.Darkness.color.g,
						this.Darkness.color.b,
						this.Darkness.color.a + Time.deltaTime);

					if (this.Darkness.color.a >= 1.0f)
					{
						//Debug Quick Start
						if (this.Selected == 0)
						{
							GameGlobals.Profile = 1;
							SceneManager.LoadScene(SceneNames.CalendarScene);
						}
						//Save Files
						else if (this.Selected == 1)
						{
							if (this.LoveSick)
							{
								GameGlobals.LoveSick = true;
							}

							if (PlayerPrefs.GetInt("ProfileCreated_" + GameGlobals.Profile) == 0)
							{
								PlayerPrefs.SetInt("ProfileCreated_" + GameGlobals.Profile, 1);

								//Debug.Log("ProfileCreated_" + GameGlobals.Profile + " is: " + PlayerPrefs.GetInt("ProfileCreated_" + GameGlobals.Profile));

								PlayerGlobals.Money = 10;

								DateGlobals.Weekday = DayOfWeek.Sunday;
								SceneManager.LoadScene(SceneNames.SenpaiScene);
							}
							else
							{
								//We should probably load into the scene where the player last saved.
								SceneManager.LoadScene(SceneNames.CalendarScene);
							}
						}
						//Mission Mode
						else if (this.Selected == 2)
						{
							SceneManager.LoadScene(SceneNames.MissionModeScene);
						}
						//Credits
						else if (this.Selected == 5)
						{
							SceneManager.LoadScene(SceneNames.CreditsScene);
						}
						else if (this.Selected == 7)
						{
							Application.Quit();
						}
					}

					this.LoveSickMusic.volume -= Time.deltaTime;
					this.CuteMusic.volume -= Time.deltaTime;
				}
			}
		}

		if (this.Timer < 10.0f)
		{
			Animation yandereAnim = this.Yandere.GetComponent<Animation>();
			yandereAnim[AnimNames.FemaleYanderePose].weight = 0.0f;
			yandereAnim[AnimNames.FemaleFist].weight = 0.0f;
		}

		if (Input.GetKeyDown(KeyCode.Minus))
		{
			Time.timeScale -= 1.0f;
		}

		if (Input.GetKeyDown(KeyCode.Equals))
		{
			Time.timeScale += 1.0f;
		}
	}

	void LateUpdate()
	{
        if (this.Knife.activeInHierarchy)
		{
			// [af] Converted while loop to foreach loop.
			foreach (Transform transform in this.Spine)
			{
				transform.transform.localEulerAngles = new Vector3(
					transform.transform.localEulerAngles.x + 5.0f,
					transform.transform.localEulerAngles.y,
					transform.transform.localEulerAngles.z);
			}

			Transform arm0 = this.Arm[0];
			arm0.transform.localEulerAngles = new Vector3(
				arm0.transform.localEulerAngles.x,
				arm0.transform.localEulerAngles.y,
				arm0.transform.localEulerAngles.z - 15.0f);

			Transform arm1 = this.Arm[1];
			arm1.transform.localEulerAngles = new Vector3(
				arm1.transform.localEulerAngles.x,
				arm1.transform.localEulerAngles.y,
				arm1.transform.localEulerAngles.z + 15.0f);
		}
	}

	void TurnDark()
	{
		GameObjectUtils.SetLayerRecursively(this.Yandere.transform.parent.gameObject, 14);

		Animation yandereAnim = this.Yandere.GetComponent<Animation>();
		yandereAnim[AnimNames.FemaleYanderePose].weight = 1.0f;
		yandereAnim[AnimNames.FemaleFist].weight = 1.0f;
		yandereAnim.Play(AnimNames.FemaleFist);

		Renderer yandereEye0 = this.YandereEye[0];
		yandereEye0.material.color = new Color(
			yandereEye0.material.color.r,
			yandereEye0.material.color.g,
			yandereEye0.material.color.b,
			1.0f);

		Renderer yandereEye1 = this.YandereEye[1];
		yandereEye1.material.color = new Color(
			yandereEye1.material.color.r,
			yandereEye1.material.color.g,
			yandereEye1.material.color.b,
			1.0f);

		this.ColorCorrection.enabled = true;
		this.BloodProjector.SetActive(true);
		this.BloodCamera.SetActive(true);
		this.Knife.SetActive(true);
		this.CuteMusic.volume = 0.0f;
		this.DarkMusic.volume = 1.0f;

		RenderSettings.ambientLight = new Color(0.50f, 0.50f, 0.50f, 1.0f);
		RenderSettings.skybox = this.DarkSkybox;
		RenderSettings.fog = true;

		// [af] Converted while loop to foreach loop.
		foreach (UISprite sprite in this.MediumSprites)
		{
			sprite.color = new Color(1.0f, 0.0f, 0.0f, sprite.color.a);
		}

		// [af] Converted while loop to foreach loop.
		foreach (UISprite sprite in this.LightSprites)
		{
			sprite.color = new Color(0.0f, 0.0f, 0.0f, sprite.color.a);
		}

		// [af] Converted while loop to foreach loop.
		foreach (UISprite sprite in this.DarkSprites)
		{
			sprite.color = new Color(0.0f, 0.0f, 0.0f, sprite.color.a);
		}

		// [af] Converted while loop to foreach loop.
		foreach (UILabel label in this.ColoredLabels)
		{
			label.color = new Color(0.0f, 0.0f, 0.0f, label.color.a);
		}

		this.SimulatorLabel.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
	}

	void TurnCute()
	{
		GameObjectUtils.SetLayerRecursively(this.Yandere.transform.parent.gameObject, 9);

		Animation yandereAnim = this.Yandere.GetComponent<Animation>();
		yandereAnim[AnimNames.FemaleYanderePose].weight = 0.0f;
		yandereAnim[AnimNames.FemaleFist].weight = 0.0f;
		yandereAnim.Stop(AnimNames.FemaleFist);

		Renderer yandereEye0 = this.YandereEye[0];
		yandereEye0.material.color = new Color(
			yandereEye0.material.color.r,
			yandereEye0.material.color.g,
			yandereEye0.material.color.b,
			0.0f);

		Renderer yandereEye1 = this.YandereEye[1];
		yandereEye1.material.color = new Color(
			yandereEye1.material.color.r,
			yandereEye1.material.color.g,
			yandereEye1.material.color.b,
			0.0f);

		this.ColorCorrection.enabled = false;
		this.BloodProjector.SetActive(false);
		this.BloodCamera.SetActive(false);
		this.Knife.SetActive(false);

		this.CuteMusic.volume = 1.0f;
		this.DarkMusic.volume = 0.0f;
		this.Timer = 0.0f;

		RenderSettings.ambientLight = new Color(0.75f, 0.75f, 0.75f, 1.0f);
		RenderSettings.skybox = this.CuteSkybox;
		RenderSettings.fog = false;

		// [af] Converted while loop to foreach loop.
		foreach (UISprite sprite in this.MediumSprites)
		{
			sprite.color = new Color(
				this.MediumColor.r,
				this.MediumColor.g,
				this.MediumColor.b,
				sprite.color.a);
		}

		// [af] Converted while loop to foreach loop.
		foreach (UISprite sprite in this.LightSprites)
		{
			sprite.color = new Color(
				this.LightColor.r,
				this.LightColor.g,
				this.LightColor.b,
				sprite.color.a);
		}

		// [af] Converted while loop to foreach loop.
		foreach (UISprite sprite in this.DarkSprites)
		{
			sprite.color = new Color(
				this.DarkColor.r,
				this.DarkColor.g,
				this.DarkColor.b,
				sprite.color.a);
		}

		// [af] Converted while loop to foreach loop.
		foreach (UILabel label in this.ColoredLabels)
		{
			label.color = new Color(1.0f, 1.0f, 1.0f, label.color.a);
		}

		this.SimulatorLabel.color = this.MediumColor;
	}

	void TurnLoveSick()
	{
		RenderSettings.ambientLight = new Color(0.25f, 0.25f, 0.25f, 1.0f);

		this.CuteMusic.volume = 0.0f;
		this.DarkMusic.volume = 0.0f;
		this.LoveSickMusic.volume = 1.0f;

		// [af] Converted while loop to foreach loop.
		foreach (UISprite sprite in this.MediumSprites)
		{
			sprite.color = new Color(0.0f, 0.0f, 0.0f, sprite.color.a);
		}

		// [af] Converted while loop to foreach loop.
		foreach (UISprite sprite in this.LightSprites)
		{
			//sprite.color = new Color(189.0f / 255.0f, 7.0f / 255.0f, 88.0f / 255.0f, sprite.color.a);
			sprite.color = new Color(1.0f, 0.0f, 0.0f, sprite.color.a);
		}

		// [af] Converted while loop to foreach loop.
		foreach (UISprite sprite in this.DarkSprites)
		{
			//sprite.color = new Color(189.0f / 255.0f, 7.0f / 255.0f, 88.0f / 255.0f, sprite.color.a);
			sprite.color = new Color(1.0f, 0.0f, 0.0f, sprite.color.a);
		}

		// [af] Converted while loop to foreach loop.
		foreach (UILabel label in this.ColoredLabels)
		{
			//label.color = new Color(189.0f / 255.0f, 7.0f / 255.0f, 88.0f / 255.0f, label.color.a);
			label.color = new Color(1.0f, 0.0f, 0.0f, label.color.a);
		}
			
		this.LoveSickLogo.SetActive(true);
		this.Logo.SetActive(false);
	}
}
