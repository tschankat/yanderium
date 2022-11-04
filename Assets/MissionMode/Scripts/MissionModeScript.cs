using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionModeScript : MonoBehaviour
{
	public NotificationManagerScript NotificationManager;
	public NewMissionWindowScript NewMissionWindow;
	public MissionModeMenuScript MissionModeMenu;
	public StudentManagerScript StudentManager;
	public WeaponManagerScript WeaponManager;
	public PromptScript ExitPortalPrompt;
	public IncineratorScript Incinerator;
	public WoodChipperScript WoodChipper;
	public AlphabetScript AlphabetArrow;
	public ReputationScript Reputation;
	public GrayscaleEffect Grayscale;
	public PromptBarScript PromptBar;
	public BoundaryScript Boundary;
	public JukeboxScript Jukebox;
	public YandereScript Yandere;
	public PoliceScript Police;
	public ClockScript Clock;

	public UILabel EventSubtitleLabel;
	public UILabel ReputationLabel;
	public UILabel GameOverHeader;
	public UILabel GameOverReason;
	public UILabel SubtitleLabel;
	public UILabel LoadingLabel;
	public UILabel SpottedLabel;
	public UILabel SanityLabel;
	public UILabel MoneyLabel;
	public UILabel TimeLabel;

	public UISprite ReputationFace1;
	public UISprite ReputationFace2;
	public UISprite ReputationBG;
	public UISprite CautionSign;
	public UISprite MusicIcon;
	public UISprite Darkness;

	public UILabel FPS;

	public GardenHoleScript[] GardenHoles;
	public GameObject[] ReputationIcons;
	public string[] GameOverReasons;
	public AudioClip[] StealthMusic;
	public Transform[] SpawnPoints;
	public UISprite[] PoliceIcon;
	public UILabel[] PoliceLabel;
	public int[] Conditions;

	public GameObject SecurityCameraGroup;
	public GameObject MetalDetectorGroup;
	public GameObject HeartbrokenCamera;
	public GameObject DetectionCamera;
	public GameObject HeartbeatCamera;
	public GameObject MissionModeHUD;
	public GameObject SpottedWindow;
	public GameObject TranqDetector;
	public GameObject WitnessCamera;
	public GameObject GameOverText;
	public GameObject VoidGoddess;
	public GameObject Headmaster;
	public GameObject ExitPortal;
	public GameObject MurderKit;
	public GameObject Subtitle;
	public GameObject Nemesis;
	public GameObject Safe;

	public Transform LastKnownPosition;

	public int RequiredClothingID = 0;
	public int RequiredDisposalID = 0;
	public int RequiredWeaponID = 0;

	public int NemesisDifficulty = 0;
	public int DisposalMethod = 0;
	public int MurderWeaponID = 0;
	public int GameOverPhase = 0;
	public int Destination = 0;
	public int Difficulty = 0;
	public int GameOverID = 0;
	public int TargetID = 0;
	public int MusicID = 1;
	public int Phase = 1;
	public int ID = 0;

	public int[] Target;
	public int[] Method;

	public bool SecurityCameras = false;
	public bool MetalDetectors = false;
	public bool StealDocuments = false;
	public bool NoCollateral = false;
	public bool NoSuspicion = false;
	public bool NoWitnesses = false;
	public bool NoCorpses = false;
	public bool NoSpeech = false;
	public bool NoWeapon = false;
	public bool NoBlood = false;
	public bool TimeLimit = false;

	public bool CorrectClothingConfirmed = false;
	public bool DocumentsStolen = false;
	public bool CorpseDisposed = false;
	public bool WeaponDisposed = false;
	public bool CheckForBlood = false;
	public bool BloodCleaned = false;
	public bool MultiMission = false;
	public bool InfoRemark = false;
	public bool TargetDead = false;
	public bool Chastise = false;
	public bool FadeOut = false;

	public bool Enabled = false;

	public bool[] Checking;

	public string CauseOfFailure = string.Empty;

	public float TimeRemaining = 300.0f;
	public float TargetHeight = 0.0f;
	public float BloodTimer = 0.0f;
	public float Speed = 0.0f;
	public float Timer = 0.0f;

    public AudioClip InfoAccomplished;
	public AudioClip InfoExfiltrate;
	public AudioClip InfoObjective;
	public AudioClip InfoFailure;

	public AudioClip GameOverSound;

    public AudioSource MyAudio;

    public ColorCorrectionCurves[] ColorCorrections;

	public Camera MainCamera;

	public UILabel Watermark;
	public Font Arial;

	void Start()
	{
        if (!SchoolGlobals.HighSecurity)
		{
			this.SecurityCameraGroup.SetActive(false);
			this.MetalDetectorGroup.SetActive(false);
		}

		this.NewMissionWindow.gameObject.SetActive(false);
		this.MissionModeHUD.SetActive(false);
		this.SpottedWindow.SetActive(false);
		this.ExitPortal.SetActive(false);
		this.Safe.SetActive(false);

		this.MainCamera = Camera.main;

		if (GameGlobals.LoveSick)
		{
			this.MurderKit.SetActive(false);

			this.Yandere.HeartRate.MediumColour = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			this.Yandere.HeartRate.NormalColour = new Color(1.0f, 1.0f, 1.0f, 1.0f);

			this.Clock.PeriodLabel.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
			this.Clock.TimeLabel.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
			//this.Clock.DayLabel.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
			this.Clock.DayLabel.enabled = false;

			this.MoneyLabel.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);

			this.Reputation.PendingRepMarker.GetComponent<UISprite>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
			this.Reputation.CurrentRepMarker.gameObject.SetActive(false);
			this.Reputation.PendingRepLabel.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);

			this.ReputationFace1.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
			this.ReputationFace2.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
			this.ReputationBG.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);

			this.ReputationLabel.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);

			this.Watermark.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);

			this.EventSubtitleLabel.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
			this.SubtitleLabel.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
			this.CautionSign.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
			this.FPS.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);

			this.SanityLabel.color  = new Color(1.0f, 0.0f, 0.0f, 1.0f);

			for (this.ID = 1; this.ID < (this.PoliceLabel.Length); this.ID++)
			{
				this.PoliceLabel[ID].color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
			}

			for (this.ID = 1; this.ID < (this.PoliceIcon.Length); this.ID++)
			{
				this.PoliceIcon[ID].color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
			}
		}

		if (MissionModeGlobals.MissionMode)
		{
			this.AlphabetArrow.gameObject.SetActive(true);
			this.AlphabetArrow.gameObject.GetComponent<Renderer>().material.shader = this.StudentManager.QualityManager.ToonOutline;
			this.AlphabetArrow.gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0);

			this.Headmaster.SetActive(false);

			this.Yandere.HeartRate.MediumColour = new Color(1.0f, 0.50f, 0.50f, 1.0f);
			this.Yandere.HeartRate.NormalColour = new Color(1.0f, 1.0f, 1.0f, 1.0f);

			this.Clock.PeriodLabel.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			this.Clock.TimeLabel.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			this.Clock.DayLabel.enabled = false;

			this.MoneyLabel.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			this.MoneyLabel.fontStyle = FontStyle.Bold;
			this.MoneyLabel.trueTypeFont = this.Arial;

			this.Reputation.PendingRepMarker.GetComponent<UISprite>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			this.Reputation.CurrentRepMarker.gameObject.SetActive(false);
			this.Reputation.PendingRepLabel.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

			this.ReputationLabel.fontStyle = FontStyle.Bold;
			this.ReputationLabel.trueTypeFont = this.Arial;
			this.ReputationLabel.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			this.ReputationLabel.text = "AWARENESS";

			this.ReputationIcons[0].SetActive(true);
			this.ReputationIcons[1].SetActive(false);
			this.ReputationIcons[2].SetActive(false);
			this.ReputationIcons[3].SetActive(false);
			this.ReputationIcons[4].SetActive(false);
			this.ReputationIcons[5].SetActive(false);

			this.Clock.TimeLabel.fontStyle = FontStyle.Bold;
			this.Clock.TimeLabel.trueTypeFont = this.Arial;

			this.Clock.PeriodLabel.fontStyle = FontStyle.Bold;
			this.Clock.PeriodLabel.trueTypeFont = this.Arial;

			this.Watermark.fontStyle = FontStyle.Bold;
			this.Watermark.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			this.Watermark.trueTypeFont = this.Arial;

			this.SubtitleLabel.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			this.CautionSign.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			this.FPS.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

			this.SanityLabel.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

			this.ColorCorrections = MainCamera.GetComponents<ColorCorrectionCurves>();
			this.StudentManager.MissionMode = true;

			this.NemesisDifficulty = MissionModeGlobals.NemesisDifficulty;
			this.Difficulty = MissionModeGlobals.MissionDifficulty;
			this.TargetID = MissionModeGlobals.MissionTarget;

			ClassGlobals.BiologyGrade = 1;
			ClassGlobals.ChemistryGrade = 1;
			ClassGlobals.LanguageGrade = 1;
			ClassGlobals.PhysicalGrade = 1;
			ClassGlobals.PsychologyGrade = 1;

			//this.Yandere.StudentManager.TutorialWindow.gameObject.SetActive(false);
			OptionGlobals.TutorialsOff = true;

			SchoolGlobals.SchoolAtmosphereSet = true;
			SchoolGlobals.SchoolAtmosphere = 1.0f - (this.Difficulty * .1f);

			PlayerGlobals.Money = 20;

			StudentManager.Atmosphere = 1.0f - (this.Difficulty * .1f);
			StudentManager.SetAtmosphere();

			for (this.ID = 1; this.ID < (this.PoliceLabel.Length); this.ID++)
			{
				this.PoliceLabel[ID].fontStyle = FontStyle.Bold;
				this.PoliceLabel[ID].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
				this.PoliceLabel[ID].trueTypeFont = this.Arial;
			}

			for (this.ID = 1; this.ID < (this.PoliceIcon.Length); this.ID++)
			{
				this.PoliceIcon[ID].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			}

			if (this.Difficulty > 1)
			{
				// [af] Converted while loop to for loop.
				for (this.ID = 2; this.ID < (this.Difficulty + 1); this.ID++)
				{
					int condition = MissionModeGlobals.GetMissionCondition(this.ID);

					if (condition == 1)
					{
						this.RequiredWeaponID = MissionModeGlobals.MissionRequiredWeapon;
					}
					else if (condition == 2)
					{
						this.RequiredClothingID = MissionModeGlobals.MissionRequiredClothing;
					}
					else if (condition == 3)
					{
						this.RequiredDisposalID = MissionModeGlobals.MissionRequiredDisposal;
					}
					else if (condition == 4)
					{
						this.NoCollateral = true;
					}
					else if (condition == 5)
					{
						this.NoWitnesses = true;
					}
					else if (condition == 6)
					{
						this.NoCorpses = true;
					}
					else if (condition == 7)
					{
						this.NoWeapon = true;
					}
					else if (condition == 8)
					{
						this.NoBlood = true;
					}
					else if (condition == 9)
					{
						this.TimeLimit = true;
					}
					else if (condition == 10)
					{
						this.NoSuspicion = true;
					}
					else if (condition == 11)
					{
						this.SecurityCameras = true;
					}
					else if (condition == 12)
					{
						this.MetalDetectors = true;
					}
					else if (condition == 13)
					{
						this.NoSpeech = true;
					}
					else if (condition == 14)
					{
						this.StealDocuments = true;
					}

					this.Conditions[this.ID] = condition;
				}
			}

			if (!this.StealDocuments)
			{
				this.DocumentsStolen = true;
			}
			else
			{
				this.Safe.SetActive(true);
			}

			if (this.SecurityCameras)
			{
				this.SecurityCameraGroup.SetActive(true);
			}

			if (this.MetalDetectors)
			{
				this.MetalDetectorGroup.SetActive(true);
			}

			if (this.TimeLimit)
			{
				this.TimeLabel.gameObject.SetActive(true);
			}

			if (this.NoSpeech)
			{
				this.StudentManager.NoSpeech = true;
			}

			if (this.RequiredDisposalID == 0)
			{
				this.CorpseDisposed = true;
			}

			if (this.NemesisDifficulty > 0)
			{
				this.Nemesis.SetActive(true);
			}

			if (!this.NoWeapon)
			{
				this.WeaponDisposed = true;
			}

			if (!this.NoBlood)
			{
				this.BloodCleaned = true;
			}

			this.Jukebox.Egg = true;
			this.Jukebox.KillVolume();
			this.Jukebox.MissionMode.enabled = true;
			this.Jukebox.MissionMode.volume = 0.0f;

			this.Yandere.FixCamera();

			MainCamera.transform.position = new Vector3(
				MainCamera.transform.position.x,
				6.51505f,
				-76.9222f);

			MainCamera.transform.eulerAngles = new Vector3(
				15.0f,
				MainCamera.transform.eulerAngles.y,
				MainCamera.transform.eulerAngles.z);

			this.Yandere.RPGCamera.enabled = false;
			this.Yandere.SanityBased = true;
			this.Yandere.CanMove = false;

			this.VoidGoddess.GetComponent<VoidGoddessScript>().Window.gameObject.SetActive(false);
			this.HeartbeatCamera.SetActive(false);
			this.TranqDetector.SetActive(false);
			this.VoidGoddess.SetActive(false);
			this.MurderKit.SetActive(false);

			this.TargetHeight = 1.51505f;
			this.Yandere.HUD.alpha = 0.0f;

			this.MusicIcon.color = new Color(
				this.MusicIcon.color.r,
				this.MusicIcon.color.g,
				this.MusicIcon.color.b,
				1.0f);

			this.Darkness.color = new Color(
				this.Darkness.color.r,
				this.Darkness.color.g,
				this.Darkness.color.b,
				1.0f);

			this.MissionModeMenu.UpdateGraphics();
			this.MissionModeMenu.gameObject.SetActive(true);

			if (MissionModeGlobals.MultiMission)
			{
				this.NewMissionWindow.gameObject.SetActive(true);
				this.MissionModeMenu.gameObject.SetActive(false);
				this.NewMissionWindow.FillOutInfo();
				this.NewMissionWindow.HideButtons();
				this.MultiMission = true;

				int TempID = 1;

				while (TempID < 11)
				{
					this.Target[TempID] = PlayerPrefs.GetInt("MissionModeTarget" + TempID);
					this.Method[TempID] = PlayerPrefs.GetInt("MissionModeMethod" + TempID);

					TempID++;
				}
			}

			this.Enabled = true;
		}
		else
		{
			this.MissionModeMenu.gameObject.SetActive(false);
			this.TimeLabel.gameObject.SetActive(false);
			this.enabled = false;
		}
	}

	public int Frame = 0;

	void Update()
	{
		// [af] Commented in JS code.
		/*
		if (this.Frame == 1)
		{
			this.ChangeAllText();
		}
		
		this.Frame++;
		*/

		if (this.Phase == 1)
		{
			this.Darkness.color = new Color(
				this.Darkness.color.r,
				this.Darkness.color.g,
				this.Darkness.color.b,
				Mathf.MoveTowards(this.Darkness.color.a, 0.0f, Time.deltaTime / 3.0f));

			if (this.Darkness.color.a == 0.0f)
			{
				this.Speed += Time.deltaTime / 3.0f;

				MainCamera.transform.position = new Vector3(
					MainCamera.transform.position.x,
					Mathf.Lerp(MainCamera.transform.position.y, this.TargetHeight, Time.deltaTime * this.Speed),
					MainCamera.transform.position.z);

				if (MainCamera.transform.position.y < (this.TargetHeight + 0.10f))
				{
					this.Yandere.HUD.alpha = Mathf.MoveTowards(
						this.Yandere.HUD.alpha, 1.0f, Time.deltaTime / 3.0f);

					if (this.Yandere.HUD.alpha == 1.0f)
					{
						Debug.Log("Incrementing phase.");

						this.Yandere.RPGCamera.enabled = true;
						this.HeartbeatCamera.SetActive(true);
						this.Yandere.CanMove = true;
						this.Phase++;
					}
				}
			}

			if (Input.GetButtonDown(InputNames.Xbox_A))
			{
				MainCamera.transform.position = new Vector3(
					MainCamera.transform.position.x,
					this.TargetHeight,
					MainCamera.transform.position.z);

				this.Yandere.RPGCamera.enabled = true;
				this.HeartbeatCamera.SetActive(true);
				this.Yandere.CanMove = true;
				this.Yandere.HUD.alpha = 1.0f;

				this.Darkness.color = new Color(
					this.Darkness.color.r,
					this.Darkness.color.g,
					this.Darkness.color.b,
					0.0f);

				this.Phase++;
			}
		}
		else if (this.Phase == 2)
		{
			if (this.MultiMission)
			{
				int TempID = 1;

				while (TempID < this.Target.Length)
				{
					if (this.Target[TempID] == 0)
					{
						this.Checking[TempID] = false;
					}
					else
					{
						if (this.Checking[TempID])
						{
							if (this.StudentManager.Students[this.Target[TempID]].transform.position.y < -11.0f)
							{
								this.GameOverID = 1;
								this.GameOver();
								this.Phase = 4;
							}
							else
							{
								if (!this.StudentManager.Students[this.Target[TempID]].Alive)
								{
									if (this.Method[TempID] == 0)
									{
										if (this.StudentManager.Students[this.Target[TempID]].DeathType == DeathType.Weapon)
										{
											this.NewMissionWindow.DeathSkulls[TempID].SetActive(true);
											this.Checking[TempID] = false;
											this.CheckForCompletion();
										}
										else
										{
											this.GameOverID = 18;
											this.GameOver();
											this.Phase = 4;
										}
									}
									else if (this.Method[TempID] == 1)
									{
										if (this.StudentManager.Students[this.Target[TempID]].DeathType == DeathType.Drowning)
										{
											this.NewMissionWindow.DeathSkulls[TempID].SetActive(true);
											this.Checking[TempID] = false;
											this.CheckForCompletion();
										}
										else
										{
											this.GameOverID = 18;
											this.GameOver();
											this.Phase = 4;
										}
									}
									else if (this.Method[TempID] == 2)
									{
										if (this.StudentManager.Students[this.Target[TempID]].DeathType == DeathType.Poison)
										{
											this.NewMissionWindow.DeathSkulls[TempID].SetActive(true);
											this.Checking[TempID] = false;
											this.CheckForCompletion();
										}
										else
										{
											this.GameOverID = 18;
											this.GameOver();
											this.Phase = 4;
										}
									}
									else if (this.Method[TempID] == 3)
									{
										if (this.StudentManager.Students[this.Target[TempID]].DeathType == DeathType.Electrocution)
										{
											this.NewMissionWindow.DeathSkulls[TempID].SetActive(true);
											this.Checking[TempID] = false;
											this.CheckForCompletion();
										}
										else
										{
											this.GameOverID = 18;
											this.GameOver();
											this.Phase = 4;
										}
									}
									else if (this.Method[TempID] == 4)
									{
										if (this.StudentManager.Students[this.Target[TempID]].DeathType == DeathType.Burning)
										{
											this.NewMissionWindow.DeathSkulls[TempID].SetActive(true);
											this.Checking[TempID] = false;
											this.CheckForCompletion();
										}
										else
										{
											this.GameOverID = 18;
											this.GameOver();
											this.Phase = 4;
										}
									}
									else if (this.Method[TempID] == 5)
									{
										if (this.StudentManager.Students[this.Target[TempID]].DeathType == DeathType.Falling)
										{
											this.NewMissionWindow.DeathSkulls[TempID].SetActive(true);
											this.Checking[TempID] = false;
											this.CheckForCompletion();
										}
										else
										{
											this.GameOverID = 18;
											this.GameOver();
											this.Phase = 4;
										}
									}
									else if (this.Method[TempID] == 6)
									{
										if (this.StudentManager.Students[this.Target[TempID]].DeathType == DeathType.Weight)
										{
											this.NewMissionWindow.DeathSkulls[TempID].SetActive(true);
											this.Checking[TempID] = false;
											this.CheckForCompletion();
										}
										else
										{
											this.GameOverID = 18;
											this.GameOver();
											this.Phase = 4;
										}
									}
								}
							}
						}
					}

					TempID++;
				}
			}

			if (!this.TargetDead)
			{
				if (this.StudentManager.Students[this.TargetID] != null)
				{
					this.AlphabetArrow.LocalArrow.LookAt(StudentManager.Students[this.TargetID].transform.position);
					this.AlphabetArrow.transform.eulerAngles = this.AlphabetArrow.LocalArrow.eulerAngles - new Vector3(0, StudentManager.MainCamera.transform.eulerAngles.y, 0);
					this.AlphabetArrow.gameObject.SetActive(true);

					///////////////////////////////////
					///// CHECK IF TARGET IS DEAD /////
					///////////////////////////////////

					if (!this.StudentManager.Students[this.TargetID].Alive)
					{
						if (this.Yandere.Equipped > 0)
						{
							if (this.StudentManager.Students[this.TargetID].DeathType == DeathType.Weapon)
							{
								this.MurderWeaponID = this.Yandere.EquippedWeapon.WeaponID;
							}
							else
							{
								this.WeaponDisposed = true;
							}
						}
						else
						{
							this.WeaponDisposed = true;
						}

						this.AlphabetArrow.gameObject.SetActive(false);
						this.TargetDead = true;
					}

					///////////////////////////////////////
					///// CHECK IF TARGET HAS ESCAPED /////
					///////////////////////////////////////

					if (this.StudentManager.Students[this.TargetID].transform.position.y < -11.0f)
					{
						this.GameOverID = 1;
						this.GameOver();
						this.Phase = 4;
					}
				}
			}

			/////////////////////////////////////////////////////
			///// CHECK IF PLAYER HAS USED INCORRECT WEAPON /////
			/////////////////////////////////////////////////////

			if (this.RequiredWeaponID > 0)
			{
				if (this.StudentManager.Students[this.TargetID] != null)
				{
					if (!this.StudentManager.Students[this.TargetID].Alive)
					{
						if (this.StudentManager.Students[this.TargetID].DeathCause != this.RequiredWeaponID)
						{
							this.Chastise = true;

							this.GameOverID = 2;
							this.GameOver();
							this.Phase = 4;
						}
					}
				}
			}

			///////////////////////////////////////////////////
			///// CHECK IF PLAYER WORE INCORRECT CLOTHING /////
			///////////////////////////////////////////////////

			if (!this.CorrectClothingConfirmed)
			{
				if (this.RequiredClothingID > 0)
				{
					if (this.StudentManager.Students[this.TargetID] != null)
					{
						if (!this.StudentManager.Students[this.TargetID].Alive)
						{
							if (this.Yandere.Schoolwear != this.RequiredClothingID)
							{
								this.Chastise = true;

								this.GameOverID = 3;
								this.GameOver();
								this.Phase = 4;
							}
							else
							{
								this.CorrectClothingConfirmed = true;
							}
						}
					}
				}
			}

			//////////////////////////////////////////////////////////
			///// CHECK IF PLAYER DISPOSED OF CORPSE INCORRECTLY /////
			//////////////////////////////////////////////////////////

			if (this.RequiredDisposalID > 0)
			{
				if (this.DisposalMethod == 0)
				{
					if (this.TargetDead)
					{
						for (this.ID = 1; this.ID < (this.Incinerator.Victims + 1); this.ID++)
						{
							if (this.Incinerator.VictimList[this.ID] == this.TargetID)
							{
								if (this.Incinerator.Smoke.isPlaying)
								{
									this.DisposalMethod = 1;
								}
							}
						}
							
						int TargetLimbs = 0;

						for (this.ID = 1; this.ID < (this.Incinerator.Limbs + 1); this.ID++)
						{
							if (this.Incinerator.LimbList[this.ID] == this.TargetID)
							{
								TargetLimbs++;
							}

							if (TargetLimbs == 6)
							{
								if (this.Incinerator.Smoke.isPlaying)
								{
									this.DisposalMethod = 1;
								}
							}
						}
							
						for (this.ID = 1; this.ID < (this.WoodChipper.Victims + 1); this.ID++)
						{
							if (this.WoodChipper.VictimList[this.ID] == this.TargetID)
							{
								if (this.WoodChipper.Shredding)
								{
									this.DisposalMethod = 2;
								}
							}
						}
							
						for (this.ID = 1; this.ID < this.GardenHoles.Length; this.ID++)
						{
							if (this.GardenHoles[this.ID].VictimID == this.TargetID)
							{
								if (!this.GardenHoles[this.ID].enabled)
								{
									this.DisposalMethod = 3;
								}
							}
						}

						if (this.DisposalMethod > 0)
						{
							if (this.DisposalMethod != this.RequiredDisposalID)
							{
								this.Chastise = true;

								this.GameOverID = 4;
								this.GameOver();
								this.Phase = 4;
							}
							else
							{
								this.CorpseDisposed = true;
							}
						}
					}
				}
			}

			//////////////////////////////////////////////////
			///// CHECK IF PLAYER HAS KILLED NON-TARGETS /////
			//////////////////////////////////////////////////

			if (this.NoCollateral)
			{
				if (this.Police.Corpses == 1)
				{
					if (this.Police.CorpseList[0].StudentID != this.TargetID)
					{
						this.Chastise = true;

						this.GameOverID = 5;
						this.GameOver();
						this.Phase = 4;
					}
				}
				else if (this.Police.Corpses > 1)
				{
					this.GameOverID = 5;
					this.GameOver();
					this.Phase = 4;
				}
			}

			////////////////////////////////////////////////
			///// CHECK IF A MURDER HAS BEEN WITNESSED /////
			////////////////////////////////////////////////

			if (this.NoWitnesses)
			{
				// [af] Converted while loop to for loop.
				for (this.ID = 1; this.ID < this.StudentManager.Students.Length; this.ID++)
				{
					if (this.StudentManager.Students[this.ID] != null)
					{
						if (this.StudentManager.Students[this.ID].WitnessedMurder && !this.Yandere.DelinquentFighting)
						{
							this.SpottedLabel.text = this.StudentManager.Students[this.ID].Name;
							this.SpottedWindow.SetActive(true);

							this.Chastise = true;

							this.GameOverID = 6;

							if (this.Yandere.Mopping)
							{
								this.GameOverID = 20;
							}

							this.GameOver();
							this.Phase = 4;
						}
					}
				}
			}

			/////////////////////////////////////////////
			///// CHECK IF A CORPSE BEEN DISCOVERED /////
			/////////////////////////////////////////////

			if (this.NoCorpses)
			{
				// [af] Converted while loop to for loop.
				for (this.ID = 1; this.ID < this.StudentManager.Students.Length; this.ID++)
				{
					if (this.StudentManager.Students[this.ID] != null)
					{
						if (this.StudentManager.Students[this.ID].WitnessedCorpse || this.StudentManager.Students[this.ID].WitnessedMurder)
						{
							this.SpottedLabel.text = this.StudentManager.Students[this.ID].Name;
							this.SpottedWindow.SetActive(true);

							this.Chastise = true;

							if (this.Yandere.DelinquentFighting)
							{
								this.GameOverID = 19;
							}
							else
							{
								this.GameOverID = 7;
							}

							this.GameOver();
							this.Phase = 4;
						}
					}
				}
			}

			////////////////////////////////////////////////
			///// CHECK IF PLAYER HAS CLEANED UP BLOOD /////
			////////////////////////////////////////////////

			if (this.NoBlood)
			{
				if (this.Police.Deaths > 0)
				{
					this.CheckForBlood = true;
				}

				if (this.CheckForBlood)
				{
					if (this.Police.BloodParent.childCount == 0)
					{
						this.BloodTimer += Time.deltaTime;

						if (BloodTimer > 2)
						{
							this.BloodCleaned = true;
						}
					}
					else
					{
						this.BloodTimer = 0;
					}
				}
			}

			//////////////////////////////////////////////////
			///// CHECK IF PLAYER HAS DISPOSED OF WEAPON /////
			//////////////////////////////////////////////////

			if (this.NoWeapon)
			{
				if (!this.WeaponDisposed)
				{
					if (this.Incinerator.Timer > 0.0f)
					{
						// [af] Converted while loop to for loop.
						for (this.ID = 1; this.ID < (this.Incinerator.DestroyedEvidence + 1); this.ID++)
						{
							if (this.Incinerator.EvidenceList[this.ID] == this.MurderWeaponID)
							{
								this.WeaponDisposed = true;
							}
						}
					}
				}
			}

			/////////////////////////////////////
			///// CHECK IF TIME HAS RUN OUT /////
			/////////////////////////////////////

			if (this.TimeLimit)
			{
				if (!this.Yandere.PauseScreen.Show)
				{
					this.TimeRemaining = Mathf.MoveTowards(this.TimeRemaining, 0.0f, Time.deltaTime);
				}

				int RoundedTime = Mathf.CeilToInt(this.TimeRemaining);
				int Minutes = RoundedTime / 60;
				int Seconds = RoundedTime % 60;

				this.TimeLabel.text = string.Format("{0:00}:{1:00}", Minutes, Seconds);

				if (this.TimeRemaining == 0.0f)
				{
					this.Chastise = true;

					this.GameOverID = 10;
					this.GameOver();
					this.Phase = 4;
				}
			}

			//////////////////////////////////////////
			///// CHECK IF AWARENESS IS TOO HIGH /////
			//////////////////////////////////////////

			if ((this.Reputation.Reputation + this.Reputation.PendingRep) <= -100.0f)
			{
				this.GameOverID = 14;
				this.GameOver();
				this.Phase = 4;
			}

			//////////////////////////////////////////
			///// CHECK IF AWARENESS IS TOO HIGH /////
			//////////////////////////////////////////

			if (this.NoSuspicion)
			{
				if ((this.Reputation.Reputation + this.Reputation.PendingRep) < 0.0f)
				{
					this.GameOverID = 14;
					this.GameOver();
					this.Phase = 4;
				}
			}

			////////////////////////////////////////////////
			///// CHECK IF PLAYER HAS BEEN APPREHENDED /////
			////////////////////////////////////////////////

			if (this.HeartbrokenCamera.activeInHierarchy)
			{
				this.HeartbrokenCamera.SetActive(false);
				this.GameOverID = 0;
				this.GameOver();
				this.Phase = 4;
			}

			//////////////////////////////////////
			///// CHECK IF THE DAY HAS ENDED /////
			//////////////////////////////////////

			if (this.Clock.PresentTime > 1080.0f)
			{
				this.GameOverID = 11;
				this.GameOver();
				this.Phase = 4;
			}

			////////////////////////////////////////
			///// CHECK IF POLICE HAVE ARRIVED /////
			////////////////////////////////////////

			else if (this.Police.FadeOut)
			{
				this.GameOverID = 12;
				this.GameOver();
				this.Phase = 4;
			}

			/////////////////////////////////////////////////////////
			///// CHECK IF PLAYER HAS BEEN NOTICED BY A TEACHER /////
			/////////////////////////////////////////////////////////

			if (this.Yandere.ShoulderCamera.Noticed)
			{
				if (this.Yandere.Senpai.GetComponent<StudentScript>().Club == ClubType.Council)
				{
					this.GameOverID = 21;
				}
				else
				{
					this.GameOverID = 17;
				}

				this.GameOver();
				this.Phase = 4;
			}

			///////////////////////////////////////////
			///// CHECK IF PLAYER IS EXFILTRATING /////
			///////////////////////////////////////////

			if (this.ExitPortal.activeInHierarchy)
			{
				if (this.Yandere.Chased || this.Yandere.Chasers > 0)
				{
					this.ExitPortalPrompt.Label[0].text = "     " + "Cannot Exfiltrate!";
					this.ExitPortalPrompt.Circle[0].fillAmount = 1.0f;
				}
				else
				{
					this.ExitPortalPrompt.Label[0].text = "     " + "Exfiltrate";

					if (this.ExitPortalPrompt.Circle[0].fillAmount == 0.0f)
					{
						StudentManager.DisableChaseCameras();

						MainCamera.transform.position = new Vector3(0.50f, 2.25f, -100.50f);
						MainCamera.transform.eulerAngles = Vector3.zero;

						this.Yandere.transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
						this.Yandere.transform.position = new Vector3(0.0f, 0.0f, -94.50f);

						this.Yandere.Character.GetComponent<Animation>().Play(this.Yandere.WalkAnim);
						this.Yandere.RPGCamera.enabled = false;

						// [af] Added "gameObject" for C# compatibility.
						this.Yandere.HUD.gameObject.SetActive(false);

						this.Yandere.CanMove = false;

                        this.Jukebox.MissionMode.clip = this.StealthMusic[10];
                        this.Jukebox.MissionMode.loop = false;
                        this.Jukebox.MissionMode.Play();

						this.MyAudio.PlayOneShot(this.InfoAccomplished);

						this.HeartbeatCamera.SetActive(false);
						this.Boundary.enabled = false;
						this.Phase++;
					}
				}
			}

			/////////////////////////////////////////////////////////////////////
			///// CHECK IF ALL CONDITIONS FOR MISSION SUCCESS HAVE BEEN MET /////
			/////////////////////////////////////////////////////////////////////

			if (this.TargetDead && this.CorpseDisposed && this.BloodCleaned &&
				this.WeaponDisposed && this.DocumentsStolen)
			{
				if (this.GameOverID == 0)
				{
					if (!this.ExitPortal.activeInHierarchy)
					{
						this.NotificationManager.DisplayNotification(NotificationType.Complete);
						this.NotificationManager.DisplayNotification(NotificationType.Exfiltrate);
						this.MyAudio.PlayOneShot(this.InfoExfiltrate);
						this.AlphabetArrow.gameObject.SetActive(true);
						this.ExitPortal.SetActive(true);
					}
				}
			}

			/////////////////////////////////////////////////////////////////
			///// CHECK IF CONDITIONS FOR MISSION SUCCESS HAVE REVERTED /////
			/////////////////////////////////////////////////////////////////

			if (this.NoBlood && this.BloodCleaned)
			{
				if (this.Police.BloodParent.childCount > 0)
				{
					this.ExitPortal.SetActive(false);
					this.BloodCleaned = false;
					this.BloodTimer = 0;
				}
			}

			///////////////////////////////////////////////////////////////////
			///// CHECK IF CONDITIONS ARE MET FOR A REMARK FROM INFO-CHAN /////
			///////////////////////////////////////////////////////////////////

			if (!this.InfoRemark)
			{
				if (this.GameOverID == 0)
				{
					if (this.TargetDead)
					{
						if (!this.CorpseDisposed || !this.BloodCleaned || !this.WeaponDisposed)
						{
							this.MyAudio.PlayOneShot(this.InfoObjective);
							this.InfoRemark = true;
						}
					}
				}
			}

			if (this.ExitPortal.activeInHierarchy)
			{
				this.AlphabetArrow.LocalArrow.LookAt(new Vector3(0, 0, this.ExitPortal.transform.position.z));
				this.AlphabetArrow.transform.eulerAngles = this.AlphabetArrow.LocalArrow.transform.eulerAngles - new Vector3(0, StudentManager.MainCamera.transform.eulerAngles.y, 0);
			}
		}
		else if (this.Phase == 3)
		{
			this.Timer += Time.deltaTime;

			MainCamera.transform.position = new Vector3(
				MainCamera.transform.position.x,
				MainCamera.transform.position.y - (Time.deltaTime * 0.20f),
				MainCamera.transform.position.z);

			this.Yandere.transform.position = new Vector3(
				this.Yandere.transform.position.x,
				this.Yandere.transform.position.y,
				this.Yandere.transform.position.z - Time.deltaTime);

			if (this.Timer > 5.0f)
			{
				this.Success();
				this.Timer = 0.0f;
				this.Phase++;
			}
		}
		else if (this.Phase == 4)
		{
			this.Timer += 1.0f / 60.0f;

			if (this.Timer > 1.0f)
			{
				if (!this.FadeOut)
				{
					if (!this.PromptBar.Show)
					{
						this.PromptBar.Show = true;
					}
					else
					{
						if (Input.GetButtonDown(InputNames.Xbox_A))
						{
							this.PromptBar.Show = false;
							this.Destination = 1;
							this.FadeOut = true;
						}
						else if (Input.GetButtonDown(InputNames.Xbox_B))
						{
							this.PromptBar.Show = false;
							this.Destination = 2;
							this.FadeOut = true;
						}
						else if (Input.GetButtonDown(InputNames.Xbox_X))
						{
							this.PromptBar.Show = false;
							this.Destination = 3;
							this.FadeOut = true;
						}
                        /*
						else if (Input.GetButtonDown(InputNames.Xbox_Y))
						{
							this.PromptBar.Show = false;
							this.Destination = 4;
							this.FadeOut = true;
						}
                        */
					}
				}
				else
				{
					this.Darkness.color = new Color(
						this.Darkness.color.r,
						this.Darkness.color.g,
						this.Darkness.color.b,
						Mathf.MoveTowards(this.Darkness.color.a, 1.0f, 1.0f / 60.0f));

					this.Jukebox.Dip = Mathf.MoveTowards(this.Jukebox.Dip, 0.0f, 1.0f / 60.0f);

					if (this.Darkness.color.a > (254.0f / 256.0f))
					{
						if (this.Destination == 1)
						{
							this.LoadingLabel.enabled = true;

							this.ResetGlobals();
							SceneManager.LoadScene(SceneManager.GetActiveScene().name);
						}
						else if (this.Destination == 2)
						{
							Globals.DeleteAll();
							SceneManager.LoadScene(SceneNames.MissionModeScene);
						}
						else if (this.Destination == 3)
						{
							Globals.DeleteAll();
							SceneManager.LoadScene(SceneNames.TitleScene);
						}
						else if (this.Destination == 4)
						{
							Globals.DeleteAll();
							SceneManager.LoadScene(SceneNames.DiscordScene);
						}
					}
				}
			}

			if (this.GameOverPhase == 1)
			{
				if (this.Timer > 2.50f)
				{
					if (this.Chastise)
					{
						this.MyAudio.PlayOneShot(this.InfoFailure);
						this.GameOverPhase++;
					}
					else
					{
						this.GameOverPhase++;
						this.Timer += 5;
					}
				}
			}
			else if (this.GameOverPhase == 2)
			{
				if (this.Timer > 7.50f)
				{
					this.Jukebox.MissionMode.clip = this.StealthMusic[0];
					this.Jukebox.MissionMode.Play();
					this.Jukebox.Volume = 0.50f;
					this.GameOverPhase++;
				}
			}

			#if UNITY_EDITOR
			if (Input.GetKeyDown("space"))
			{
				Valid = false;
				Success();
			}
			#endif
		}
	}

	public void GameOver()
	{
		if (this.Yandere.Aiming)
		{
			this.Yandere.StopAiming();
		}

		if (this.Yandere.YandereVision)
		{
			this.Yandere.YandereVision = false;
			this.Yandere.ResetYandereEffects();
		}

		this.Yandere.enabled = false;

		this.GameOverReason.text = this.GameOverReasons[this.GameOverID];

		if (this.ColorCorrections.Length > 0)
		{
			this.ColorCorrections[2].enabled = true;
		}

		this.MyAudio.PlayOneShot(this.GameOverSound);
		this.DetectionCamera.SetActive(false);
		this.HeartbeatCamera.SetActive(false);
		this.WitnessCamera.SetActive(false);
		this.GameOverText.SetActive(true);

		// [af] Added "gameObject" for C# compatibility.
		this.Yandere.HUD.gameObject.SetActive(false);

		this.Subtitle.SetActive(false);
		Time.timeScale = 0.0001f;
		this.GameOverPhase = 1;

		this.Jukebox.MissionMode.Stop();

		// [af] Commented in JS code.
		/*
		this.PromptBar.Show = true;
		
		this.PromptBar.ClearButtons();
		this.PromptBar.Label[0].text = "Retry";
		this.PromptBar.Label[1].text = "Mission Menu";
		this.PromptBar.Label[2].text = "Main Menu";
		this.PromptBar.UpdateButtons();
		*/
	}

	public UILabel DiscordCodeLabel;
	public float RandomNumber;
	public bool Valid;

	void Success()
	{
		while (!Valid)
		{
			RandomNumber = Random.Range(1000000, 10000000);

			float Division = RandomNumber / 9.0f;

			if((Division%5) == 0)
			{
				Valid = true;
			}
		}

		this.DiscordCodeLabel.text = "" + RandomNumber;
		this.DiscordCodeLabel.transform.parent.gameObject.SetActive(true);

		this.GameOverHeader.transform.localPosition = new Vector3(
			this.GameOverHeader.transform.localPosition.x,
			0.0f,
			this.GameOverHeader.transform.localPosition.z);

		this.GameOverHeader.text = "MISSION ACCOMPLISHED";
		this.GameOverReason.gameObject.SetActive(false);
		this.ColorCorrections[2].enabled = true;
		this.DetectionCamera.SetActive(false);
		this.WitnessCamera.SetActive(false);
		this.GameOverText.SetActive(true);
		this.GameOverReason.text = string.Empty;
		this.Subtitle.SetActive(false);
		this.Jukebox.Volume = 1.0f;
		Time.timeScale = 0.0001f;

		// [af] Commented in JS code.
		/*
		this.PromptBar.Show = true;
		
		this.PromptBar.ClearButtons();
		this.PromptBar.Label[0].text = "Replay";
		this.PromptBar.Label[1].text = "Mission Menu";
		this.PromptBar.Label[2].text = "Main Menu";
		this.PromptBar.UpdateButtons();
		*/
	}

	public void ChangeMusic()
	{
		this.MusicID++;

		if (this.MusicID > 9)
		{
			this.MusicID = 1;
		}

		this.Jukebox.MissionMode.clip = this.StealthMusic[this.MusicID];
		this.Jukebox.MissionMode.Play();
	}

	void ResetGlobals()
	{
		Debug.Log("Mission Difficulty was: " + MissionModeGlobals.MissionDifficulty);

		int DisableAnimations = OptionGlobals.DisableFarAnimations;
		bool DisablePostAliasing = OptionGlobals.DisablePostAliasing;
		bool DisableOutlines = OptionGlobals.DisableOutlines;
		int Detail = OptionGlobals.LowDetailStudents;
		int Particles = OptionGlobals.ParticleCount;
		bool EnableShadows = OptionGlobals.EnableShadows;
		int Distance = OptionGlobals.DrawDistance;
		int DistanceLimit = OptionGlobals.DrawDistanceLimit;
		bool DisableBloom = OptionGlobals.DisableBloom;
		bool Fog = OptionGlobals.Fog;
		bool AggressiveNemesis = MissionModeGlobals.NemesisAggression;

		string TargetName = MissionModeGlobals.MissionTargetName;
		bool HighPopulation = OptionGlobals.HighPopulation;

		//////////////////////////
		///// DELETE GLOBALS /////
		//////////////////////////

		Globals.DeleteAll();

		OptionGlobals.TutorialsOff = true;

		int TempID = 1;

		while (TempID < 11)
		{
			PlayerPrefs.SetInt("MissionModeTarget" + TempID, this.Target[TempID]);
			PlayerPrefs.SetInt("MissionModeMethod" + TempID, this.Method[TempID]);

			TempID++;
		}

		SchoolGlobals.SchoolAtmosphere = 1.0f - (this.Difficulty * .1f);
		MissionModeGlobals.MissionTargetName = TargetName;
		MissionModeGlobals.MissionDifficulty = this.Difficulty;
		OptionGlobals.HighPopulation = HighPopulation;
		MissionModeGlobals.MissionTarget = this.TargetID;
		SchoolGlobals.SchoolAtmosphereSet = true;
		MissionModeGlobals.MissionMode = true;
		MissionModeGlobals.MultiMission = MultiMission;

		MissionModeGlobals.MissionRequiredWeapon = this.RequiredWeaponID;
		MissionModeGlobals.MissionRequiredClothing = this.RequiredClothingID;
		MissionModeGlobals.MissionRequiredDisposal = this.RequiredDisposalID;

		ClassGlobals.BiologyGrade = 1;
		ClassGlobals.ChemistryGrade = 1;
		ClassGlobals.LanguageGrade = 1;
		ClassGlobals.PhysicalGrade = 1;
		ClassGlobals.PsychologyGrade = 1;

		// [af] Converted while loop to for loop.
		for (this.ID = 2; this.ID < 11; this.ID++)
		{
			MissionModeGlobals.SetMissionCondition(this.ID, this.Conditions[this.ID]);
		}

		MissionModeGlobals.NemesisDifficulty = this.NemesisDifficulty;
		MissionModeGlobals.NemesisAggression = AggressiveNemesis;
		OptionGlobals.DisableFarAnimations = DisableAnimations;
		OptionGlobals.DisablePostAliasing = DisablePostAliasing;
		OptionGlobals.DisableOutlines = DisableOutlines;
		OptionGlobals.LowDetailStudents = Detail;
		OptionGlobals.ParticleCount = Particles;
		OptionGlobals.EnableShadows = EnableShadows;
		OptionGlobals.DrawDistance = Distance;
		OptionGlobals.DrawDistanceLimit = DistanceLimit;
		OptionGlobals.DisableBloom = DisableBloom;
		OptionGlobals.Fog = Fog;

		Debug.Log("Mission Difficulty is now: " + MissionModeGlobals.MissionDifficulty);
	}

	void ChangeAllText()
	{
		UILabel[] Labels = FindObjectsOfType<UILabel>();

		foreach (UILabel Text in Labels)
		{
			float TextAlpha = Text.color.a;
			Text.color = new Color(1.0f, 1.0f, 1.0f, TextAlpha);
			Text.trueTypeFont = this.Arial;
		}

		UISprite[] Sprites = FindObjectsOfType<UISprite>();

		foreach (UISprite Sprite in Sprites)
		{
			float SpriteAlpha = Sprite.color.a;

			if (Sprite.color != new Color(0.0f, 0.0f, 0.0f, SpriteAlpha))
			{
				Sprite.color = new Color(1.0f, 1.0f, 1.0f, SpriteAlpha);
			}
		}
	}

	void CheckForCompletion()
	{
		if (!this.Checking[1] && !this.Checking[2] && !this.Checking[3] &&
			!this.Checking[4] && !this.Checking[5] && !this.Checking[6] &&
			!this.Checking[7] && !this.Checking[8] && !this.Checking[9] &&
			!this.Checking[10])
		{
			this.TargetDead = true;
		}
	}
}