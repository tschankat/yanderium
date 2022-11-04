using UnityEngine;

public class StudentManagerScript : MonoBehaviour
{
	private PortraitChanScript NewPortraitChan;
	private GameObject NewStudent;

	public StudentScript[] Students;

	public SelectiveGrayscale SmartphoneSelectiveGreyscale;
    public PickpocketMinigameScript PickpocketMinigame;
	public PopulationManagerScript PopulationManager;
	public SelectiveGrayscale HandSelectiveGreyscale;
	public SkinnedMeshRenderer FemaleShowerCurtain;
    public CleaningManagerScript CleaningManager;
	public StolenPhoneSpotScript StolenPhoneSpot;
	public SelectiveGrayscale SelectiveGreyscale;
    public InterestManagerScript InterestManager;
    public CombatMinigameScript CombatMinigame;
	public DatingMinigameScript DatingMinigame;
	public SnappedYandereScript SnappedYandere;
	public TextureManagerScript TextureManager;
	public TutorialWindowScript TutorialWindow;
	public QualityManagerScript QualityManager;
	public ComputerGamesScript ComputerGames;
	public EmergencyExitScript EmergencyExit;
	public MemorialSceneScript MemorialScene;
	public TranqDetectorScript TranqDetector;
    public WitnessCameraScript WitnessCamera;
	public ConvoManagerScript ConvoManager;
	public TallLockerScript CommunalLocker;
    public BloodParentScript BloodParent;
    public CabinetDoorScript CabinetDoor;
    public ClubManagerScript ClubManager;
    public LightSwitchScript LightSwitch;
	public LoveManagerScript LoveManager;
	public MiyukiEnemyScript MiyukiEnemy;
	public TaskManagerScript TaskManager;
	public Collider MaleLockerRoomArea;
	public StudentScript BloodReporter;
	public HeadmasterScript Headmaster;
	public NoteWindowScript NoteWindow;
	public ReputationScript Reputation;
	public WeaponScript FragileWeapon;
	public AudioSource PracticeVocals;
	public AudioSource PracticeMusic;
	public ContainerScript Container;
	public RedStringScript RedString;
	public RingEventScript RingEvent;
	public RivalPoseScript RivalPose;
	public GazerEyesScript Shinigami;
    public HologramScript Holograms;
    public RobotArmScript RobotArms;
    public AlphabetScript Alphabet;
    public PickUpScript Flashlight;
	public FountainScript Fountain;
	public PoseModeScript PoseMode;
	public TrashCanScript TrashCan;
	public Collider LockerRoomArea;
	public StudentScript Reporter;
	public DoorScript GamingDoor;
	public GhostScript GhostChan;
	public YandereScript Yandere;
	public ListScript MeetSpots;
    public MirrorScript Mirror;
    public PoliceScript Police;
	public DoorScript ShedDoor;
	public UILabel ErrorLabel;
	public RestScript Rest;
	public TagScript Tag;

	public Collider EastBathroomArea;
	public Collider WestBathroomArea;
	public Collider IncineratorArea;
	public Collider HeadmasterArea;

    public Collider GardenArea;
    public Collider PoolStairs;
    public Collider TreeArea;
    public Collider NEStairs;
	public Collider NWStairs;
	public Collider SEStairs;
	public Collider SWStairs;

	public DoorScript AltFemaleVomitDoor;
	public DoorScript FemaleVomitDoor;

	public CounselorDoorScript[] CounselorDoor;

	public ParticleSystem AltFemaleDrownSplashes;
	public ParticleSystem FemaleDrownSplashes;

	public OfferHelpScript FragileOfferHelp;
    public OfferHelpScript OsanaOfferHelp;
    public OfferHelpScript OfferHelp;

	public Transform AltFemaleVomitSpot;

	public ListScript SearchPatrols;
	public ListScript CleaningSpots;
	public ListScript Patrols;
	public ClockScript Clock;
	public JsonScript JSON;
	public GateScript Gate;

	public ListScript EntranceVectors;
	public ListScript ShowerLockers;
	public ListScript GoAwaySpots;
	public ListScript HidingSpots;
	public ListScript LunchSpots;
	public ListScript Hangouts;
	public ListScript Lockers;
	public ListScript Podiums;
	public ListScript Clubs;

    public BodyHidingLockerScript[] BodyHidingLockers;
    public ChangingBoothScript[] ChangingBooths;
	public GradingPaperScript[] FacultyDesks;
	public GameObject[] ShrineCollectibles;
	public StudentScript[] WitnessList;
	public StudentScript[] Teachers;
	public GameObject[] Graffiti;
	public GameObject[] Canvas;
	public ListScript[] Seats;
	public Collider[] Blood;
	public Collider[] Limbs;

	public Transform[] TeacherGuardLocation;
	public Transform[] CorpseGuardLocation;
	public Transform[] BloodGuardLocation;
	public Transform[] SleuthDestinations;
	public Transform[] StrippingPositions;
	public Transform[] GardeningPatrols;
	public Transform[] MartialArtsSpots;
	public Transform[] LockerPositions;
	public Transform[] BackstageSpots;
	public Transform[] SpawnPositions;
	public Transform[] GraffitiSpots;
	public Transform[] PracticeSpots;
	public Transform[] SunbatheSpots;
	public Transform[] MeetingSpots;
	public Transform[] PinDownSpots;
	public Transform[] ShockedSpots;
	public Transform[] FridaySpots;
	public Transform[] MiyukiSpots;
	public Transform[] SocialSeats;
	public Transform[] SocialSpots;
	public Transform[] SupplySpots;
	public Transform[] BullySpots;
	public Transform[] DramaSpots;
	public Transform[] MournSpots;
	public Transform[] ClubZones;
	public Transform[] SulkSpots;
	public Transform[] FleeSpots;
	public Transform[] Uniforms;
	public Transform[] Plates;

	public Transform[] FemaleVomitSpots;
	public Transform[] MaleVomitSpots;

	public Transform[] FemaleWashSpots;
	public Transform[] MaleWashSpots;

	public DoorScript[] FemaleToiletDoors;
	public DoorScript[] MaleToiletDoors;

	public DrinkingFountainScript[] DrinkingFountains;

	public Renderer[] FridayPaintings;

	public bool[] SeatsTaken11;
	public bool[] SeatsTaken12;
	public bool[] SeatsTaken21;
	public bool[] SeatsTaken22;
	public bool[] SeatsTaken31;
	public bool[] SeatsTaken32;
	public bool[] NoBully;

	public Quaternion[] OriginalClubRotations;
	public Vector3[] OriginalClubPositions;

	public Collider RivalDeskCollider;

	public Transform FollowerLookAtTarget;
	public Transform SuitorConfessionSpot;
	public Transform RivalConfessionSpot;
	public Transform OriginalLyricsSpot;
	public Transform FragileSlaveSpot;
	public Transform FemaleCoupleSpot;
	public Transform YandereStripSpot;
	public Transform FemaleBatheSpot;
	public Transform FemaleStalkSpot;
	public Transform FemaleStripSpot;
	public Transform FemaleVomitSpot;
	public Transform MedicineCabinet;
	public Transform ConfessionSpot;
	public Transform CorpseLocation;
	public Transform FemaleRestSpot;
	public Transform FemaleWashSpot;
	public Transform MaleCoupleSpot;
	public Transform AirGuitarSpot;
	public Transform BloodLocation;
	public Transform FastBatheSpot;
	public Transform InfirmarySeat;
	public Transform MaleBatheSpot;
	public Transform MaleStalkSpot;
	public Transform MaleStripSpot;
	public Transform MaleVomitSpot;
	public Transform SacrificeSpot;
	public Transform WeaponBoxSpot;
	public Transform FountainSpot;
	public Transform MaleWashSpot;
	public Transform SenpaiLocker;
	public Transform SuitorLocker;
	public Transform MaleRestSpot;
	public Transform RomanceSpot;
	public Transform BrokenSpot;
	public Transform BullyGroup;
	public Transform EdgeOfGrid;
	public Transform GoAwaySpot;
	public Transform LyricsSpot;
	public Transform MainCamera;
	public Transform SuitorSpot;
	public Transform ToolTarget;
	public Transform MiyukiCat;
	public Transform ShameSpot;
	public Transform SlaveSpot;
	public Transform Papers;
	public Transform Exit;

	public GameObject LovestruckCamera;
	public GameObject DelinquentRadio;
	public GameObject GardenBlockade;
	public GameObject PortraitChan;
	public GameObject RandomPatrol;
	public GameObject ChaseCamera;
	public GameObject EmptyObject;
	public GameObject PortraitKun;
	public GameObject StudentChan;
	public GameObject StudentKun;
	public GameObject RivalChan;
	public GameObject Canvases;
	public GameObject Medicine;
	public GameObject DrumSet;
	public GameObject Flowers;
	public GameObject Portal;
	public GameObject Gift;

	public float[] SpawnTimes;

	public int LowDetailThreshold = 0;
	public int FarAnimThreshold = 0;
	public int MartialArtsPhase = 0;
	public int OriginalUniforms = 2;
	public int StudentsSpawned = 0;
	public int SedatedStudents = 0;
	public int StudentsTotal = 13;
	public int TeachersTotal = 6;
	public int GirlsSpawned = 0;
	public int NewUniforms = 0;
	public int NPCsSpawned = 0;
	public int SleuthPhase = 1;
	public int DramaPhase = 1;
	public int NPCsTotal = 0;
	public int Witnesses = 0;
	public int PinPhase = 0;
	public int Bullies = 0;
	public int Speaker = 21;
	public int Frame = 0;

	public int GymTeacherID = 100;
	public int ObstacleID = 6;
	public int CurrentID = 0;
	public int SuitorID = 13;
	public int VictimID = 0;
	public int NurseID = 93;
	public int RivalID = 7;
	public int SpawnID = 0;
	public int ID = 0;

	public bool ReactedToGameLeader = false;
	public bool MurderTakingPlace = false;
	public bool ControllerShrink = false;
	public bool DisableFarAnims = false;
    public bool GameOverIminent = false;
    public bool RivalEliminated = false;
	public bool TakingPortraits = false;
	public bool TeachersSpawned = false;
	public bool MetalDetectors = false;
	public bool YandereVisible = false;
	public bool NoClubMeeting = false;
	public bool UpdatedBlood = false;
	public bool YandereDying = false;
    public bool FirstUpdate = false;
	public bool MissionMode = false;
	public bool OpenCurtain = false;
	public bool PinningDown = false;
	public bool RoofFenceUp = false;
	public bool YandereLate = false;
	public bool ForceSpawn = false;
	public bool NoGravity = false;
	public bool Randomize = false;
	public bool LoveSick = false;
	public bool NoSpeech = false;
	public bool Meeting = false;
	public bool Censor = false;
	public bool Spooky = false;
	public bool Bully = false;
	public bool Ebola = false;
	public bool Gaze = false;
	public bool Pose = false;
	public bool Sans = false;
	public bool Stop = false;
	public bool Egg = false;
	public bool Six = false;
	public bool AoT = false;
	public bool DK = false;

	public float Atmosphere = 0.0f;
	public float OpenValue = 100.0f;
	public float YandereHeight = 999;

	public float MeetingTimer = 0.0f;
	public float PinDownTimer = 0.0f;
	public float ChangeTimer = 0.0f;
	public float SleuthTimer = 0.0f;
	public float DramaTimer = 0.0f;
	public float LowestRep = 0.0f;
	public float PinTimer = 0.0f;
	public float Timer = 0.0f;

	public string[] ColorNames;

	public string[] MaleNames;
	public string[] FirstNames;
	public string[] LastNames;

	public AudioSource[] FountainAudio;

	public AudioClip YanderePinDown;
	public AudioClip PinDownSFX;
	
	[SerializeField] int ProblemID = -1; // [af] -1 means no problem.

	public GameObject Cardigan;
	public SkinnedMeshRenderer CardiganRenderer;

	public Mesh OpenChipBag;

	public Vignetting[] Vignettes;

	public Renderer[] Trees;

	public DoorScript[] AllDoors;

	public OcclusionPortal PlazaOccluder;

	void Start()
	{
		LoveSick = GameGlobals.LoveSick;

		MetalDetectors = SchoolGlobals.HighSecurity;

		RoofFenceUp = SchoolGlobals.RoofFence;

		SchemeGlobals.DeleteAll();

		if (ClubGlobals.GetClubClosed(ClubType.LightMusic))
		{
			this.SpawnPositions[51].position = new Vector3(3, 0, -95);
		}

		if (HomeGlobals.LateForSchool)
		{
			HomeGlobals.LateForSchool = false;
			this.YandereLate = true;

			Debug.Log("Yandere-chan is late for school!");
		}

        if (GameGlobals.Profile == 0)
        {
            GameGlobals.Profile = 1;
            PlayerGlobals.Money = 10;
        }

        if (PlayerPrefs.GetInt("LoadingSave") == 1)
        {
            int Profile = GameGlobals.Profile;
            int Slot = PlayerPrefs.GetInt("SaveSlot");

            StudentGlobals.MemorialStudents = PlayerPrefs.GetInt("Profile_" + Profile + "_Slot_" + Slot + "_MemorialStudents");
        }

        if (!this.YandereLate)
		{
			if (StudentGlobals.MemorialStudents > 0)
			{
				this.Yandere.HUD.alpha = 0;
				this.Yandere.HeartCamera.enabled = false;
			}
		}

		if (!GameGlobals.ReputationsInitialized)
		{
			GameGlobals.ReputationsInitialized = true;
			InitializeReputations();
		}

		this.ID = 76;

		while (this.ID < 81)
		{
			if (StudentGlobals.GetStudentReputation(this.ID) > -67)
			{
				StudentGlobals.SetStudentReputation(this.ID, -67);
			}

			this.ID++;
		}

		if (ClubGlobals.GetClubClosed(ClubType.Gardening))
		{
			this.GardenBlockade.SetActive(true);
			this.Flowers.SetActive(false);
		}

		this.ID = 0;

		// [af] Converted while loop to for loop.
		for (this.ID = 1; this.ID < this.JSON.Students.Length; this.ID++)
		{
			if (!this.JSON.Students[this.ID].Success)
			{
				this.ProblemID = this.ID;
				break;
			}
		}

		if (this.FridayPaintings.Length > 0)
		{
			for (this.ID = 1; this.ID < this.FridayPaintings.Length; this.ID++)
			{
				Renderer painting = this.FridayPaintings[this.ID];

				painting.material.color = new Color(1, 1, 1, 0);
			}
		}

		if (DateGlobals.Weekday != System.DayOfWeek.Friday)
		{
			if (this.Canvases != null)
			{
				this.Canvases.SetActive(false);
			}
		}
		else
		{
			if (ClubGlobals.GetClubClosed(ClubType.Art))
			{
				this.Canvases.SetActive(false);
			}

			/*
			int tempID = 1;

			while (tempID < 6)
			{
				if (StudentGlobals.GetStudentDead(40 + tempID) ||
					StudentGlobals.GetStudentKidnapped(40 + tempID) ||
					StudentGlobals.GetStudentArrested(40 + tempID) ||
					StudentGlobals.GetStudentExpelled(40 + tempID))
				{
					Canvas[tempID].SetActive(false);
				}

				tempID++;
			}
			*/
		}

		bool problem = this.ProblemID != -1;

		if (problem)
		{
			if (this.ErrorLabel != null)
			{
				this.ErrorLabel.text = string.Empty;
				this.ErrorLabel.enabled = false;
			}

			if (MissionModeGlobals.MissionMode)
			{
				StudentGlobals.FemaleUniform = 5;
				StudentGlobals.MaleUniform = 5;

				this.RedString.gameObject.SetActive(false);
			}
			//else
			//{
				this.SetAtmosphere();
			//}

			GameGlobals.Paranormal = false;

			if (StudentGlobals.StudentSlave > 0)
			{
				if (!StudentGlobals.GetStudentDead(StudentGlobals.StudentSlave))
				{
					int slave = StudentGlobals.StudentSlave;

					this.ForceSpawn = true;
					this.SpawnPositions[slave] = this.SlaveSpot;
					this.SpawnID = slave;
					StudentGlobals.SetStudentDead(slave, false);
					this.SpawnStudent(this.SpawnID);
					this.Students[slave].Slave = true;

					this.SpawnID = 0;
				}
			}

			if (StudentGlobals.FragileSlave > 0)
			{
				if (!StudentGlobals.GetStudentDead(StudentGlobals.FragileSlave))
				{
					int slave = StudentGlobals.FragileSlave;

					this.ForceSpawn = true;
					this.SpawnPositions[slave] = this.FragileSlaveSpot;
					this.SpawnID = slave;
					StudentGlobals.SetStudentDead(slave, false);
					this.SpawnStudent(this.SpawnID);
					this.Students[slave].FragileSlave = true;
					this.Students[slave].Slave = true;

					this.SpawnID = 0;
				}
			}

			this.NPCsTotal = this.StudentsTotal + this.TeachersTotal;
			//SpawnID = StudentsTotal + 1;
			this.SpawnID = 1;

			if (StudentGlobals.MaleUniform == 0)
			{
				StudentGlobals.MaleUniform = 1;
			}

			// [af] Converted while loop to for loop.
			for (this.ID = 1; this.ID < (this.NPCsTotal + 1); this.ID++)
			{
				if (!StudentGlobals.GetStudentDead(this.ID))
				{
					StudentGlobals.SetStudentDying(this.ID, false);
				}
			}

			if (!this.TakingPortraits)
			{
				// [af] Converted while loop to for loop.
				for (this.ID = 1; this.ID < this.Lockers.List.Length; this.ID++)
				{
					this.LockerPositions[this.ID].transform.position =
						this.Lockers.List[this.ID].position +
						(this.Lockers.List[this.ID].forward * 0.50f);
					
					this.LockerPositions[this.ID].LookAt(this.Lockers.List[this.ID].position);
				}

				for (this.ID = 1; this.ID < this.ShowerLockers.List.Length; this.ID++)
				{
					Transform LockerPosition = Instantiate(this.EmptyObject,
						this.ShowerLockers.List[this.ID].position +
						(this.ShowerLockers.List[this.ID].forward * 0.50f),
						this.ShowerLockers.List[this.ID].rotation).transform;
					
					LockerPosition.parent = this.ShowerLockers.transform;

					LockerPosition.transform.eulerAngles = new Vector3(
						LockerPosition.transform.eulerAngles.x,
						LockerPosition.transform.eulerAngles.y + 180.0f,
						LockerPosition.transform.eulerAngles.z);

					this.StrippingPositions[this.ID] = LockerPosition;
				}

				for (this.ID = 1; this.ID < this.HidingSpots.List.Length; this.ID++)
				{
					if (this.HidingSpots.List[this.ID] == null)
					{
						GameObject newHidingSpot = Instantiate(this.EmptyObject,
							new Vector3(Random.Range(-17.0f, 17.0f), 0.0f, Random.Range(-17.0f, 17.0f)),
							Quaternion.identity);

						while ((newHidingSpot.transform.position.x < 2.50f) && 
							(newHidingSpot.transform.position.x > -2.50f) && 
							(newHidingSpot.transform.position.z > -2.50f) && 
							(newHidingSpot.transform.position.z < 2.50f))
						{
							newHidingSpot.transform.position = new Vector3(
								Random.Range(-17.0f, 17.0f), 
								0.0f, 
								Random.Range(-17.0f, 17.0f));
						}

						newHidingSpot.transform.parent = this.HidingSpots.transform;
						this.HidingSpots.List[this.ID] = newHidingSpot.transform;
					}
				}

			}

			if (this.YandereLate)
			{
				this.Clock.PresentTime = 8.0f * 60.0f;
				this.Clock.HourTime = 8.0f;
				this.Clock.UpdateClock();

				this.SkipTo8();
			}

			if (GameGlobals.AlphabetMode)
			{
                Debug.Log("Entering Alphabet Killer Mode. Repositioning Yandere-chan and others.");

                this.Yandere.transform.position = this.Portal.transform.position + new Vector3(1, 0, 0);
                this.Clock.StopTime = true;
				this.SkipTo730();
			}

			if (!this.TakingPortraits)
			{
				while (this.SpawnID < (this.NPCsTotal + 1))
				{
					this.SpawnStudent(this.SpawnID);
					this.SpawnID++;
				}

				this.Graffiti[1].SetActive(false);
				this.Graffiti[2].SetActive(false);
				this.Graffiti[3].SetActive(false);
				this.Graffiti[4].SetActive(false);
				this.Graffiti[5].SetActive(false);

				#if UNITY_EDITOR

				//Obstacle
				if (this.Students[10] != null)
				{
					this.RivalChan.SetActive(false);
				}

				#endif
			}
		}
		else
		{
			string ProblemSource = string.Empty;

			if (this.ProblemID > 1)
			{
				ProblemSource = "The problem may be caused by Student " + this.ProblemID.ToString() + ".";
			}

			if (this.ErrorLabel != null)
			{
				this.ErrorLabel.text = "The game cannot compile Students.JSON! There is a typo somewhere in the JSON file. The problem might be a missing quotation mark, a missing colon, a missing comma, or something else like that. Please find your typo and fix it, or revert to a backup of the JSON file. " + ProblemSource;
				this.ErrorLabel.enabled = true;
			}
		}

		if (!this.TakingPortraits)
		{
			this.NEStairs = GameObject.Find("NEStairs").GetComponent<Collider>();
			this.NWStairs = GameObject.Find("NWStairs").GetComponent<Collider>();
			this.SEStairs = GameObject.Find("SEStairs").GetComponent<Collider>();
			this.SWStairs = GameObject.Find("SWStairs").GetComponent<Collider>();

			//this.AllDoors = FindSceneObjectsOfType(typeof(DoorScript)) as DoorScript[];
		}
	}

	public void SetAtmosphere()
	{
		//Debug.Log("Setting school atmosphere now.");

		if (GameGlobals.LoveSick)
		{
			SchoolGlobals.SchoolAtmosphereSet = true;
			SchoolGlobals.SchoolAtmosphere = 0.0f;
		}

		if (!MissionModeGlobals.MissionMode)
		{
			if (!SchoolGlobals.SchoolAtmosphereSet)
			{
				SchoolGlobals.SchoolAtmosphereSet = true;
				SchoolGlobals.SchoolAtmosphere = 1.0f;
			}

			Atmosphere = SchoolGlobals.SchoolAtmosphere;
			//Debug.Log("StudentManager set the atmosphere.");
		}

		//Debug.Log("StudentManager's Atmosphere is " + Atmosphere);

		this.Vignettes = Camera.main.GetComponents<Vignetting>();
		float EffectStrength = 1.0f - Atmosphere;

		//Debug.Log("StudentManager's EffectStrength is " + EffectStrength);

		if (!this.TakingPortraits)
		{
			//Debug.Log("StudentManager is now applying effects to the camera.");

			this.SelectiveGreyscale.desaturation = EffectStrength;

			//Debug.Log("Selective Greyscale's desaturation is now: " + this.SelectiveGreyscale.desaturation);

			if (HandSelectiveGreyscale != null)
			{
				HandSelectiveGreyscale.desaturation = EffectStrength;
				SmartphoneSelectiveGreyscale.desaturation = EffectStrength;
			}

			Vignettes[2].intensity = EffectStrength * 5.0f;
			Vignettes[2].blur = EffectStrength;
			Vignettes[2].chromaticAberration = EffectStrength * 5.0f;

			float FogColor = 1.0f - EffectStrength;

			RenderSettings.fogColor = new Color(FogColor, FogColor, FogColor, 1.0f);
			Camera.main.backgroundColor = new Color(FogColor, FogColor, FogColor, 1.0f);

			RenderSettings.fogDensity = EffectStrength * 0.10f;
		}

		if (this.Yandere != null)
		{
			this.Yandere.GreyTarget = EffectStrength;
		}
	}

	void Update()
	{
		//If we are NOT taking portraits...
		if (!this.TakingPortraits)
		{
			if (!this.Yandere.ShoulderCamera.Counselor.Interrogating)
			{
                #if !UNITY_EDITOR
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
                #endif
			}

			this.Frame++;

			if (!this.FirstUpdate)
			{
				this.QualityManager.UpdateOutlines();
				this.FirstUpdate = true;
				this.AssignTeachers();
			}

			if (this.Frame == 3)
			{
				this.LoveManager.CoupleCheck();

				if (this.Bullies > 0)
				{
					this.DetermineVictim();
				}

				this.UpdateStudents();

				if (!OptionGlobals.RimLight)
				{
					this.QualityManager.RimLight();
				}

				ID = 26;

				while (ID < 31)
				{
					if (this.Students[ID] != null)
					{
						this.OriginalClubPositions[ID - 25] = Clubs.List[ID].position;
						this.OriginalClubRotations[ID - 25] = Clubs.List[ID].rotation;
					}

					ID++;
				}

				if (!this.TakingPortraits)
				{
					this.TaskManager.UpdateTaskStatus();
				}

				this.Yandere.GloveAttacher.newRenderer.enabled = false;

				this.UpdateAprons();

				if (PlayerPrefs.GetInt("LoadingSave") == 1)
				{
                    PlayerPrefs.SetInt("LoadingSave", 0);
                    this.Load();
				}

                if (!YandereLate)
				{
					if (StudentGlobals.MemorialStudents > 0)
					{
						this.Yandere.HUD.alpha = 0;

						this.Yandere.RPGCamera.transform.position = new Vector3(38, 4.125f, 68.825f);
						this.Yandere.RPGCamera.transform.eulerAngles = new Vector3(22.5f, 67.5f, 0);
						this.Yandere.RPGCamera.transform.Translate(Vector3.forward, Space.Self);
						this.Yandere.RPGCamera.enabled = false;

						this.Yandere.HeartCamera.enabled = false;
						this.Yandere.CanMove = false;

						this.Clock.StopTime = true;
						this.StopMoving();

						this.MemorialScene.gameObject.SetActive(true);
						this.MemorialScene.enabled = true;
					}
				}

				ID = 1;

				while (ID < 90)
				{
					if (this.Students[ID] != null)
					{
						this.Students[ID].ShoeRemoval.Start();
					}

					ID++;
				}
			}

			if (Clock.HourTime > 16.9)
			{
				this.CheckMusic();
			}
		}
		//If we ARE taking portraits...
		else
		{
			if (this.NPCsSpawned < this.StudentsTotal + this.TeachersTotal)
			{
				this.Frame++;

				if (this.Frame == 1)
				{
					if (this.NewStudent != null)
					{
						Destroy(this.NewStudent);
					}

					if (this.Randomize)
					{
						int Gender = Random.Range(0, 2);

						// [af] Replaced if/else statement with ternary expression.
						this.NewStudent = Instantiate(
							(Gender == 0) ? this.PortraitChan : this.PortraitKun,
							Vector3.zero,
							Quaternion.identity);
					}
					else
					{
						// [af] Replaced if/else statement with ternary expression.
						this.NewStudent = Instantiate(
							(this.JSON.Students[this.NPCsSpawned + 1].Gender == 0) ? this.PortraitChan : this.PortraitKun,
							Vector3.zero,
							Quaternion.identity);
					}

					CosmeticScript NewStudentCosmetic = this.NewStudent.GetComponent<CosmeticScript>();

					NewStudentCosmetic.StudentID = this.NPCsSpawned + 1;
					NewStudentCosmetic.StudentManager = this;
					NewStudentCosmetic.TakingPortrait = true;
					NewStudentCosmetic.Randomize = this.Randomize;
					NewStudentCosmetic.JSON = this.JSON;

					/*
					this.NewPortraitChan = this.NewStudent.GetComponent<PortraitChanScript>();
					this.NewPortraitChan.StudentID = this.NPCsSpawned + 1;
					this.NewPortraitChan.StudentManager = this;
					this.NewPortraitChan.JSON = this.JSON;
					*/

					if (!this.Randomize)
					{
						this.NPCsSpawned++;
					}
				}

				if (this.Frame == 2)
				{
					ScreenCapture.CaptureScreenshot(Application.streamingAssetsPath +
						"/Portraits/" + "Student_" + this.NPCsSpawned.ToString() + ".png");
					this.Frame = 0;
				}
			}
			else
			{
				ScreenCapture.CaptureScreenshot(Application.streamingAssetsPath +
					"/Portraits/" + "Student_" + this.NPCsSpawned.ToString() + ".png");

				// [af] Added "gameObject" for C# compatibility.
				this.gameObject.SetActive(false);
			}
		}

		if (this.Witnesses > 0)
		{
			// [af] Converted while loop to for loop.
			for (this.ID = 1; this.ID < this.WitnessList.Length; this.ID++)
			{
				StudentScript witness = this.WitnessList[this.ID];

				if (witness != null)
				{
					if (!witness.Alive || witness.Attacked || witness.Dying || witness.Routine ||
						 witness.Fleeing && !witness.PinningDown)
					{
						witness.PinDownWitness = false;
						witness = null;

						if (this.ID != (this.WitnessList.Length - 1))
						{
							this.Shuffle(this.ID);
						}

						this.Witnesses--;
					}
				}
			}

			if (this.PinningDown)
			{
				if (this.Witnesses < 4)
				{
					Debug.Log("Students were going to pin Yandere-chan down, " + 
					"but now there are less than 4 witnesses, so it's not going to happen.");

					if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
					{
						this.Yandere.CanMove = true;
					}

					this.PinningDown = false;
					this.PinDownTimer = 0;
					this.PinPhase = 0;
				}
			}
		}

		if (this.PinningDown)
		{
			if (!this.Yandere.Attacking)
			{
				if (this.Yandere.CanMove)
				{
					this.Yandere.CharacterAnimation.CrossFade("f02_pinDownPanic_00");
					this.Yandere.EmptyHands();
					this.Yandere.CanMove = false;

					//this.Yandere.Chased = true;
				}
			}

			/*
			if (!this.Yandere.CanMove)
			{
				if (this.YandereHeight == 999)
				{
					this.YandereHeight = this.Yandere.transform.position.y;
				}

				Debug.Log ("Growing/Shrinking controller.");

				if (!this.ControllerShrink)
				{
					this.Yandere.MyController.radius += Time.deltaTime;

					if (this.Yandere.MyController.radius > 1)
					{
						ControllerShrink = true;
					}
				}
				else
				{
					this.Yandere.MyController.radius = Mathf.MoveTowards (this.Yandere.MyController.radius, 0, Time.deltaTime);
				}

				this.Yandere.MyController.Move(this.transform.up * 0.00010f);
				this.Yandere.transform.position = new Vector3 (
					this.Yandere.transform.position.x,
					this.YandereHeight,
					this.Yandere.transform.position.z);
			}
			*/

			if (this.PinPhase == 1)
			{
				if (!this.Yandere.Attacking && !this.Yandere.Struggling)
				{
					this.PinTimer += Time.deltaTime;
				}

				if (this.PinTimer > 1.0f)
				{
					// [af] Converted while loop to for loop.
					for (this.ID = 1; this.ID < 5; this.ID++)
					{
						StudentScript witness = this.WitnessList[this.ID];

						if (witness != null)
						{
							witness.transform.position = new Vector3(
								witness.transform.position.x,
								witness.transform.position.y + 0.10f,
								witness.transform.position.z);

							witness.CurrentDestination = this.PinDownSpots[this.ID];
							witness.Pathfinding.target = this.PinDownSpots[this.ID];
							witness.SprintAnim = witness.OriginalSprintAnim;
							witness.DistanceToDestination = 100.0f;
							witness.Pathfinding.speed = 5.0f;
							witness.MyController.radius = 0;
							witness.PinningDown = true;
							witness.Alarmed = false;
							witness.Routine = false;
							witness.Fleeing = true;
							witness.AlarmTimer = 0.0f;

							witness.SmartPhone.SetActive(false);

							witness.Safe = true;
							witness.Prompt.Hide();
							witness.Prompt.enabled = false;

							Debug.Log(witness + "'s current destination is " + witness.CurrentDestination);
						}
					}

					this.PinPhase++;
				}
			}
			else
			{
				if (this.WitnessList[1].PinPhase == 0)
				{
					if (!this.Yandere.ShoulderCamera.Noticed &&
						!this.Yandere.ShoulderCamera.HeartbrokenCamera.activeInHierarchy)
					{
						this.PinDownTimer += Time.deltaTime;

						if (this.PinDownTimer > 10 ||
							this.WitnessList[1].DistanceToDestination < 1.0f &&
							this.WitnessList[2].DistanceToDestination < 1.0f &&
							this.WitnessList[3].DistanceToDestination < 1.0f &&
							this.WitnessList[4].DistanceToDestination < 1.0f)
						{
							this.Clock.StopTime = true;
							this.Yandere.HUD.enabled = false;

							if (this.Yandere.Aiming)
							{
								this.Yandere.StopAiming();
								this.Yandere.enabled = false;
							}

							this.Yandere.Mopping = false;
							this.Yandere.EmptyHands();

							AudioSource audioSource = this.GetComponent<AudioSource>();
							audioSource.PlayOneShot(this.PinDownSFX);
							audioSource.PlayOneShot(this.YanderePinDown);

							this.Yandere.CharacterAnimation.CrossFade(AnimNames.FemalePinDown);
							this.Yandere.CanMove = false;

							this.Yandere.ShoulderCamera.LookDown = true;
							this.Yandere.RPGCamera.enabled = false;
							
							this.StopMoving();
							
							this.Yandere.ShoulderCamera.HeartbrokenCamera.GetComponent<Camera>().cullingMask |= (1 << 9);

							// [af] Converted while loop to for loop.
							for (this.ID = 1; this.ID < 5; this.ID++)
							{
								StudentScript witness = this.WitnessList[this.ID];

								if (witness.MyWeapon != null)
								{
									GameObjectUtils.SetLayerRecursively(witness.MyWeapon.gameObject, 13);
								}

								//GameObjectUtils.SetLayerRecursively(witness.gameObject, 13);

								// [af] Replaced if/else statement with ternary expression.
								witness.CharacterAnimation.CrossFade(
									((witness.Male ? "pinDown_0" : "f02_pinDown_0") + this.ID).ToString());

								witness.PinPhase++;
							}
						}
					}
				}
				else
				{
					bool Continue = false;

					if (!this.WitnessList[1].Male)
					{
						if (this.WitnessList[1].CharacterAnimation[AnimNames.FemalePinDown01].time >=
							this.WitnessList[1].CharacterAnimation[AnimNames.FemalePinDown01].length)
						{
							Continue = true;
						}
					}
					else
					{
						if (this.WitnessList[1].CharacterAnimation[AnimNames.MalePinDown01].time >=
							this.WitnessList[1].CharacterAnimation[AnimNames.MalePinDown01].length)
						{
							Continue = true;
						}
					}

					if (Continue)
					{
						this.Yandere.CharacterAnimation.CrossFade(AnimNames.FemalePinDownLoop);

						// [af] Converted while loop to for loop.
						for (this.ID = 1; this.ID < 5; this.ID++)
						{
							StudentScript witness = this.WitnessList[this.ID];

							// [af] Replaced if/else statement with ternary expression.
							witness.CharacterAnimation.CrossFade(
								((witness.Male ? "pinDownLoop_0" : "f02_pinDownLoop_0") + this.ID).ToString());
						}

						this.PinningDown = false;
					}
				}
			}
		}

		if (Meeting)
		{
			UpdateMeeting();
		}

		if (Input.GetKeyDown ("space"))
		{
			DetermineVictim();
		}

		if (this.Police != null)
		{
			if (this.Police.BloodParent.childCount > 0 ||
				this.Police.LimbParent.childCount > 0 ||
				this.Yandere.WeaponManager.MisplacedWeapons > 0)
			{
				CurrentID++;

				if (CurrentID > 97)
				{
					this.UpdateBlood();

					CurrentID = 1;
				}

				if (Students[CurrentID] == null)
				{
					CurrentID++;
				}
				else
				{
					if (!Students[CurrentID].gameObject.activeInHierarchy)
					{
						CurrentID++;
					}
				}
			}
		}

		if (this.OpenCurtain)
		{
			this.OpenValue = Mathf.Lerp(this.OpenValue, 100, Time.deltaTime * 10);

			if (OpenValue > 99)
			{
				this.OpenCurtain = false;
			}

			this.FemaleShowerCurtain.SetBlendShapeWeight(0, OpenValue);
		}

		if (this.AoT)
		{
			for (this.ID = 1; this.ID < this.Students.Length; this.ID++)
			{
				StudentScript student = this.Students[this.ID];

				if (student != null)
				{
					if (student.transform.localScale.x < 9.99f)
					{
						student.transform.localScale = Vector3.Lerp(
							student.transform.localScale, new Vector3(10.0f, 10.0f, 10.0f), Time.deltaTime);
					}
				}
			}
		}

		if (this.Pose)
		{
			for (this.ID = 1; this.ID < this.Students.Length; this.ID++)
			{
				StudentScript student = this.Students[this.ID];

				if (student != null)
				{
					if (student.Prompt.Label[0] != null)
					{
						student.Prompt.Label[0].text = "     " + "Pose";
					}
				}
			}
		}

		if (this.Yandere.Egg)
		{
			if (this.Sans)
			{
				for (this.ID = 1; this.ID < this.Students.Length; this.ID++)
				{
					StudentScript student = this.Students[this.ID];

					if (student != null)
					{
						if (student.Prompt.Label[0] != null)
						{
							student.Prompt.Label[0].text = "     " + "Psychokinesis";
						}
					}
				}
			}

			if (this.Ebola)
			{
				for (this.ID = 2; this.ID < this.Students.Length; this.ID++)
				{
					StudentScript student = this.Students[this.ID];

					if (student != null && student.isActiveAndEnabled)
					{
						if (student.DistanceToPlayer < 1)
						{
							Instantiate(this.Yandere.EbolaEffect,
								student.transform.position + Vector3.up, Quaternion.identity);
							student.SpawnAlarmDisc();
							student.BecomeRagdoll();
							student.DeathType = DeathType.EasterEgg;
						}
					}
				}
			}

			if (this.Yandere.Hunger >= 5)
			{
				for (this.ID = 2; this.ID < this.Students.Length; this.ID++)
				{
					StudentScript student = this.Students[this.ID];

					if (student != null && student.isActiveAndEnabled)
					{
						if (student.DistanceToPlayer < 5)
						{
							Instantiate(this.Yandere.DarkHelix,
								student.transform.position + Vector3.up, Quaternion.identity);
							student.SpawnAlarmDisc();
							student.BecomeRagdoll();
							student.DeathType = DeathType.EasterEgg;
						}
					}
				}
			}
		}

		if (this.Yandere.transform.position.z < -50)
		{
			PlazaOccluder.open = false;
		}
		else
		{
			PlazaOccluder.open = true;
		}

#if UNITY_EDITOR
		if (Input.GetKeyDown("space"))
		{
			//TutorialWindow.enabled = true;
			//TutorialGlobals.DeleteAll();
		}
#endif

		this.YandereVisible = false;
	}

	public void SpawnStudent(int spawnID)
	{
		bool DoNotSpawn = false;

		if (this.JSON.Students[spawnID].Club != ClubType.Delinquent &&
			StudentGlobals.GetStudentReputation(spawnID) < -100)
		{
			DoNotSpawn = true;
		}

		if (spawnID > 9 && spawnID < 21)
		{
			DoNotSpawn = true;
		}

#if UNITY_EDITOR
		if (spawnID == 10 ||spawnID == 11)
		{
			DoNotSpawn = false;
		}
#endif

#if UNITY_EDITOR
		//DoNotSpawn = false;
#endif

		if (!DoNotSpawn &&
			this.Students[spawnID] == null &&
			!StudentGlobals.GetStudentDead(spawnID) &&
			!StudentGlobals.GetStudentKidnapped(spawnID) &&
			!StudentGlobals.GetStudentArrested(spawnID) &&
			!StudentGlobals.GetStudentExpelled(spawnID))
		{
			int gender = 0;

			if (this.JSON.Students[spawnID].Name == "Random")
			{
				GameObject newHidingSpot = Instantiate(this.EmptyObject,
					new Vector3(Random.Range(-17.0f, 17.0f), 0.0f, Random.Range(-17.0f, 17.0f)),
					Quaternion.identity);

				while ((newHidingSpot.transform.position.x < 2.50f) && 
					(newHidingSpot.transform.position.x > -2.50f) && 
					(newHidingSpot.transform.position.z > -2.50f) && 
					(newHidingSpot.transform.position.z < 2.50f))
				{
					newHidingSpot.transform.position = new Vector3(
						Random.Range(-17.0f, 17.0f), 
						0.0f, 
						Random.Range(-17.0f, 17.0f));
				}

				newHidingSpot.transform.parent = this.HidingSpots.transform;
				this.HidingSpots.List[spawnID] = newHidingSpot.transform;

				GameObject newRandomPatrol = Instantiate(this.RandomPatrol, Vector3.zero, Quaternion.identity);
				newRandomPatrol.transform.parent = this.Patrols.transform;
				this.Patrols.List[spawnID] = newRandomPatrol.transform;

				GameObject newRandomClean = Instantiate(this.RandomPatrol, Vector3.zero, Quaternion.identity);
				newRandomClean.transform.parent = this.CleaningSpots.transform;
				this.CleaningSpots.List[spawnID] = newRandomClean.transform;
				
				gender = (MissionModeGlobals.MissionMode &&
					(MissionModeGlobals.MissionTarget == spawnID)) ? 0 : Random.Range(0, 2);

				this.FindUnoccupiedSeat();
			}
			else
			{
				gender = this.JSON.Students[spawnID].Gender;
			}

			// [af] Replaced if/else statement with ternary expression.
			this.NewStudent = Instantiate(
				(gender == 0) ? this.StudentChan : this.StudentKun,
				this.SpawnPositions[spawnID].position,
				Quaternion.identity);

			CosmeticScript NewStudentCosmetic = this.NewStudent.GetComponent<CosmeticScript>();

			NewStudentCosmetic.LoveManager = this.LoveManager;
			NewStudentCosmetic.StudentManager = this;
			NewStudentCosmetic.Randomize = this.Randomize;
			NewStudentCosmetic.StudentID = spawnID;
			NewStudentCosmetic.JSON = this.JSON;

			if (this.JSON.Students[spawnID].Name == "Random")
			{
				this.NewStudent.GetComponent<StudentScript>().CleaningSpot = this.CleaningSpots.List[spawnID];
				this.NewStudent.GetComponent<StudentScript>().CleaningRole = 3;
			}

			if (this.JSON.Students[spawnID].Club == ClubType.Bully)
			{
				this.Bullies++;
			}

			this.Students[spawnID] = this.NewStudent.GetComponent<StudentScript>();

			StudentScript spawnedStudent = this.Students[spawnID];

			spawnedStudent.ChaseSelectiveGrayscale.desaturation = 1.0f - SchoolGlobals.SchoolAtmosphere; 
			spawnedStudent.Cosmetic.TextureManager = this.TextureManager;
            spawnedStudent.WitnessCamera = this.WitnessCamera;
			spawnedStudent.StudentManager = this;
			spawnedStudent.StudentID = spawnID;
			spawnedStudent.JSON = this.JSON;

            spawnedStudent.BloodSpawnerIdentifier.ObjectID = "Student_" + spawnID + "_BloodSpawner";
            spawnedStudent.HipsIdentifier.ObjectID = "Student_" + spawnID + "_Hips";
            spawnedStudent.YanSave.ObjectID = "Student_" + spawnID;

            if (spawnedStudent.Miyuki != null)
			{
				spawnedStudent.Miyuki.Enemy = MiyukiCat;
			}

			if (this.AoT)
			{
				spawnedStudent.AoT = true;
			}

			if (this.DK)
			{
				spawnedStudent.DK = true;
			}

			if (this.Spooky)
			{
				spawnedStudent.Spooky = true;
			}

			if (this.Sans)
			{
				spawnedStudent.BadTime = true;
			}
				
			if (spawnID == this.RivalID)
			{
				spawnedStudent.Rival = true;
				RedString.transform.parent = spawnedStudent.LeftPinky;
				RedString.transform.localPosition = new Vector3(0, 0, 0);
			}

			if (spawnID == 1)
			{
				RedString.Target = spawnedStudent.LeftPinky;
			}

#if !UNITY_EDITOR
			if (this.JSON.Students[spawnID].Persona == PersonaType.Protective ||
				this.JSON.Students[spawnID].Hairstyle == "20" ||
				this.JSON.Students[spawnID].Hairstyle == "21")
			{
				Destroy(spawnedStudent);
			}
#endif

			if (gender == 0)
			{
				this.GirlsSpawned++;
				spawnedStudent.GirlID = this.GirlsSpawned;
			}

			this.OccupySeat();
		}

		this.NPCsSpawned++;

		this.ForceSpawn = false;

#if !UNITY_EDITOR
		if (Students[10] != null || Students[11] != null)
		{
			Destroy(Students[10].gameObject);
			Destroy(Students[11].gameObject);
			Destroy(gameObject);
		}
#endif
	}

	public void UpdateStudents(int SpecificStudent = 0)
	{
		//Debug.Log("Updating students.");

		this.ID = 2;

		while (this.ID < this.Students.Length)
		{
			bool EndAfterOne = false;

			if (SpecificStudent != 0)
			{
				ID = SpecificStudent;
				EndAfterOne = true;
			}

			StudentScript student = this.Students[this.ID];

			if (student != null)
			{
				// [af] Added "gameObject" for C# compatibility.
				if (student.gameObject.activeInHierarchy || student.Hurry)
				{
					if (!student.Safe)
					{
						if (!student.Slave)
						{
							if (student.Pushable)
							{
								student.Prompt.Label[0].text = "     " + "Push";
							}
							else if (Yandere.SpiderGrow)
							{
								if (!student.Cosmetic.Empty)
								{
									student.Prompt.Label[0].text = "     " + "Send Husk";
								}
								else
								{
									student.Prompt.Label[0].text = "     " + "Talk";
								}
							}
							else if (!student.Following)
							{
								student.Prompt.Label[0].text = "     " + "Talk";
							}
							else
							{
								student.Prompt.Label[0].text = "     " + "Stop";
							}

							student.Prompt.HideButton[0] = false;
							student.Prompt.HideButton[2] = false;
							student.Prompt.Attack = false;

							if (this.Yandere.Mask != null || student.Ragdoll.Zs.activeInHierarchy)
							{
								student.Prompt.HideButton[0] = true;
							}

							if (this.Yandere.Dragging || (this.Yandere.PickUp != null) || this.Yandere.Chased)
							{
								student.Prompt.HideButton[0] = true;
								student.Prompt.HideButton[2] = true;

								if (this.Yandere.PickUp != null)
								{
									if (!student.Following)
									{
										if (this.Yandere.PickUp.Food > 0)
										{
											student.Prompt.Label[0].text = "     " + "Feed";
											student.Prompt.HideButton[0] = false;
											student.Prompt.HideButton[2] = true;
										}
										else if (this.Yandere.PickUp.Salty)
										{
											student.Prompt.Label[0].text = "     " + "Give Snack";
											student.Prompt.HideButton[0] = false;
											student.Prompt.HideButton[2] = true;
										}
										else if (this.Yandere.PickUp.StuckBoxCutter != null)
										{
											student.Prompt.Label[0].text = "     " + "Ask For Help";
											student.Prompt.HideButton[0] = false;
											student.Prompt.HideButton[2] = true;
										}
										else if (this.Yandere.PickUp.PuzzleCube)
										{
											student.Prompt.Label[0].text = "     " + "Give Puzzle";
											student.Prompt.HideButton[0] = false;
											student.Prompt.HideButton[2] = true;
										}
									}
								}
							}

							if (this.Yandere.Armed)
							{
								student.Prompt.HideButton[0] = true;
								student.Prompt.Attack = true;

								student.Prompt.MinimumDistanceSqr = 1;
								student.Prompt.MinimumDistance = 1;
							}
							else
							{
								student.Prompt.HideButton[2] = true;

								student.Prompt.MinimumDistanceSqr = 2;
								student.Prompt.MinimumDistance = 2;

								if (student.WitnessedMurder || student.WitnessedCorpse || student.Private)
								{
									student.Prompt.HideButton[0] = true;
								}
							}

							if ((this.Yandere.NearBodies > 0) || (this.Yandere.Sanity < 33.33333f))
							{
								student.Prompt.HideButton[0] = true;
							}

							if (student.Teacher)
							{
								student.Prompt.HideButton[0] = true;
							}
						}
						else
						{
							if (!student.FragileSlave)
							{
								if (this.Yandere.Armed)
								{
									if (this.Yandere.EquippedWeapon.Concealable)
									{
										student.Prompt.HideButton[0] = false;
										student.Prompt.Label[0].text = "     " + "Give Weapon";
									}
									else
									{
										student.Prompt.HideButton[0] = true;
										student.Prompt.Label[0].text = string.Empty;
									}
								}
								else
								{
									student.Prompt.HideButton[0] = true;
									student.Prompt.Label[0].text = string.Empty;
								}
							}
						}
					}

					if (student.FightingSlave)
					{
						if (this.Yandere.Armed)
						{
							Debug.Log("Fighting with a slave!");

							student.Prompt.Label[0].text = "     " + "Stab";
							student.Prompt.HideButton[0] = false;
							student.Prompt.HideButton[2] = true;
							student.Prompt.enabled = true;
						}
					}

					if (this.NoSpeech)
					{
						if (!student.Armband.activeInHierarchy)
						{
							student.Prompt.HideButton[0] = true;
						}
					}
				}

				if (student.Prompt.Label[0] != null)
				{
					if (this.Sans)
					{
						student.Prompt.HideButton[0] = false;
						student.Prompt.Label[0].text = "     " + "Psychokinesis";
					}

					if (this.Pose)
					{
						student.Prompt.HideButton[0] = false;
						student.Prompt.Label[0].text = "     " + "Pose";

						student.Prompt.BloodMask = 1 << 0;
						student.Prompt.BloodMask |= 1 << 1;
						student.Prompt.BloodMask |= 1 << 9;
						student.Prompt.BloodMask |= 1 << 13;
						student.Prompt.BloodMask |= 1 << 14;
						student.Prompt.BloodMask |= 1 << 16;
						student.Prompt.BloodMask |= 1 << 21;
						student.Prompt.BloodMask = ~student.Prompt.BloodMask;
					}

					if (!student.Teacher)
					{
						if (this.Six)
						{
							student.Prompt.MinimumDistance = .75f;
							student.Prompt.HideButton[0] = false;
							student.Prompt.Label[0].text = "     " + "Eat";
						}
					}

					if (this.Gaze)
					{
						student.Prompt.MinimumDistance = 5;
						student.Prompt.HideButton[0] = false;
						student.Prompt.Label[0].text = "     " + "Gaze";
					}
				}

				if (GameGlobals.EmptyDemon)
				{
					student.Prompt.HideButton[0] = false;
				}
			}

			this.ID++;

			if (EndAfterOne)
			{
				this.ID = this.Students.Length;
			}
		}

		this.Container.UpdatePrompts();
		this.TrashCan.UpdatePrompt();
	}

	public void UpdateMe(int ID)
	{
		if (ID > 1)
		{
			StudentScript student = this.Students[ID];

			if (!student.Safe)
			{
				student.Prompt.Label[0].text = "     " + "Talk";
				student.Prompt.HideButton[0] = false;
				student.Prompt.HideButton[2] = false;
				student.Prompt.Attack = false;

				if (student.FightingSlave)
				{
					if (this.Yandere.Armed)
					{
						Debug.Log("Fighting with a slave!");

						student.Prompt.Label[0].text = "     " + "Stab";
						student.Prompt.HideButton[0] = false;
						student.Prompt.HideButton[2] = true;
						student.Prompt.enabled = true;
					}
				}
				else
				{
					if (this.Yandere.Armed && this.OriginalUniforms + this.NewUniforms > 0)
					{
						student.Prompt.HideButton[0] = true;
						student.Prompt.MinimumDistance = 1;
						student.Prompt.Attack = true;
					}
					else
					{
						student.Prompt.HideButton[2] = true;
						student.Prompt.MinimumDistance = 2;

						if (student.WitnessedMurder || student.WitnessedCorpse || student.Private)
						{
							student.Prompt.HideButton[0] = true;
						}
					}

					if (this.Yandere.Dragging || (this.Yandere.PickUp != null) || this.Yandere.Chased || this.Yandere.Chasers > 0)
					{
						student.Prompt.HideButton[0] = true;
						student.Prompt.HideButton[2] = true;
					}

					if ((this.Yandere.NearBodies > 0) || (this.Yandere.Sanity < 33.33333f))
					{
						student.Prompt.HideButton[0] = true;
					}

					if (student.Teacher)
					{
						student.Prompt.HideButton[0] = true;
					}
				}
			}

			if (this.Sans)
			{
				student.Prompt.HideButton[0] = false;
				student.Prompt.Label[0].text = "     " + "Psychokinesis";
			}

			if (this.Pose)
			{
				student.Prompt.HideButton[0] = false;
				student.Prompt.Label[0].text = "     " + "Pose";
			}

			if (this.NoSpeech || student.Ragdoll.Zs.activeInHierarchy)
			{
				student.Prompt.HideButton[0] = true;
			}
		}
	}

	public void AttendClass()
	{
		//Debug.Log("All students are now being told to attend class.");

		this.ConvoManager.Confirmed = false;
		this.SleuthPhase = 3;

		if (this.RingEvent.EventActive)
		{
			this.RingEvent.ReturnRing();
		}

		while (this.NPCsSpawned < this.NPCsTotal)
		{
			this.SpawnStudent(this.SpawnID);
			this.SpawnID++;
		}

		if (this.Clock.LateStudent)
		{
			this.Clock.ActivateLateStudent();
		}

		// [af] Converted while loop to for loop.
		for (this.ID = 1; this.ID < this.Students.Length; this.ID++)
		{
			StudentScript student = this.Students[this.ID];

			if (student != null)
			{
				//Debug.Log("Student " + ID + " is Alive: " + student.Alive + ", Slave: " + student.Slave + ", Tranquil: " + student.Tranquil + ", Fleeing: " + student.Fleeing + ", Enabled: " + student.enabled + ", Active: " + student.gameObject.activeInHierarchy);

				if (student.WitnessedBloodPool && !student.WitnessedMurder && !student.WitnessedCorpse)
				{
					student.Fleeing = false;
					student.Alarmed = false;
					student.AlarmTimer = 0.0f;
					student.ReportPhase = 0;
					student.WitnessedBloodPool = false;
				}

				if (student.HoldingHands)
				{
					student.HoldingHands = false;
					student.Paired = false;
					student.enabled = true;
				}

				if (student.Alive && !student.Slave && !student.Tranquil && !student.Fleeing && student.enabled && student.gameObject.activeInHierarchy)
				{
					if (!student.Started)
					{
						student.Start();
					}

					if (!student.Teacher)
					{
						//Debug.Log("Right now, " + student.Name + " is SUPPOSED to be teleporting to their seat...");

						if (!student.Indoors)
						{
							if (student.ShoeRemoval.Locker == null)
							{
								student.ShoeRemoval.Start();
							}

							student.ShoeRemoval.PutOnShoes();
						}

						//Debug.Log(student.Name + "'s position is: " + student.transform.position);

						student.transform.position = student.Seat.position + (Vector3.up * 0.010f);
						student.transform.rotation = student.Seat.rotation;

						//Debug.Log(student.Name + "'s position is: " + student.transform.position);

						student.CharacterAnimation.Play(student.SitAnim);

						student.Pathfinding.canSearch = false;
						student.Pathfinding.canMove = false;

						//student.OccultBook.SetActive(false);
						//student.SmartPhone.SetActive(false);

						student.Pathfinding.speed = 0.0f;
						student.ClubActivityPhase = 0;
						student.ClubTimer = 0;
						student.Pestered = 0;

						student.Distracting = false;
						student.Distracted = false;
						student.Tripping = false;
						student.Ignoring = false;
						student.Pushable = false;
						student.Vomiting = false;
						student.Private = false;
						student.Sedated = false;
						student.Emetic = false;
						student.Hurry = false;
						student.Safe = false;

						student.CanTalk = true;
						student.Routine = true;

						if (student.Wet)
						{
                            student.CharacterAnimation[student.WetAnim].weight = 0.0f;

                            CommunalLocker.Student = null;

							student.Schoolwear = 3;
							student.ChangeSchoolwear();
							student.LiquidProjector.enabled = false;
							student.Splashed = false;
							student.Bloody = false;
							student.BathePhase = 1;
							student.Wet = false;
							student.UnWet();

							if (student.Rival)
							{
								if (CommunalLocker.RivalPhone.Stolen)
								{
									student.RealizePhoneIsMissing();
								}
							}
						}

						if (student.ClubAttire)
						{
							student.ChangeSchoolwear();
							student.ClubAttire = false;
						}

						if (student.Schoolwear != 1)
						{
							if (!student.BeenSplashed)
							{
								student.Schoolwear = 1;
								student.ChangeSchoolwear();
							}
						}

						if (student.Meeting)
						{
							if (this.Clock.HourTime > student.MeetTime)
							{
								student.Meeting = false;
							}
						}

						if (student.Club == ClubType.Sports)
						{
							student.SetSplashes(false);
							student.WalkAnim = student.OriginalWalkAnim;
							student.Character.transform.localPosition = new Vector3(0, 0, 0);
							student.Cosmetic.Goggles[student.StudentID].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 0);

							if (!student.Cosmetic.Empty)
							{
								student.Cosmetic.MaleHair[student.Cosmetic.Hairstyle].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 0);
							}
						}

						if (student.MyPlate != null)
						{
							if (student.MyPlate.transform.parent == student.RightHand)
							{
								student.MyPlate.transform.parent = null;
								student.MyPlate.transform.position = student.OriginalPlatePosition;
								student.MyPlate.transform.rotation = student.OriginalPlateRotation;

								student.IdleAnim = student.OriginalIdleAnim;
								student.WalkAnim = student.OriginalWalkAnim;
							}
						}

						if (student.ReturningMisplacedWeapon)
						{
							student.ReturnMisplacedWeapon();
						}

						//student.EmptyHands();
					}
					//If this character is a teacher...
					else
					{
						if (this.ID != this.GymTeacherID && this.ID != this.NurseID)
						{
							student.transform.position =
								this.Podiums.List[student.Class].position + (Vector3.up * 0.010f);
							student.transform.rotation = this.Podiums.List[student.Class].rotation;
						}
						else
						{
							student.transform.position = student.Seat.position + (Vector3.up * 0.010f);
							student.transform.rotation = student.Seat.rotation;
						}
					}
				}
			}
		}

		UpdateStudents();
		Physics.SyncTransforms();

		if (GameGlobals.SenpaiMourning)
		{
			Students[1].gameObject.SetActive(false);
		}

		int ShrineID = 1;

		while (ShrineID < 10)
		{
			if (ShrineCollectibles[ShrineID] != null)
			{
				ShrineCollectibles[ShrineID].SetActive(true);
			}

			ShrineID++;
		}

		//Debug.Log("Osana's ''HoldingHands'' variable is: " + Students[11].HoldingHands);

		Gift.SetActive(false);
	}

	public void SkipTo8()
	{
		while (this.NPCsSpawned < this.NPCsTotal)
		{
			this.SpawnStudent(this.SpawnID);
			this.SpawnID++;
		}

		int Iterations = 0;
		int Column = 0;

		// [af] Converted while loop to for loop.
		for (this.ID = 1; this.ID < this.Students.Length; this.ID++)
		{
			StudentScript student = this.Students[this.ID];

			if (student != null)
			{
				if (student.Alive && !student.Slave && !student.Tranquil)
				{
					if (!student.Started)
					{
						student.Start();
					}

					bool TurnBackIntoTeacher = false;

					if (this.MemorialScene.enabled)
					{
						if (student.Teacher)
						{
							TurnBackIntoTeacher = true;
							student.Teacher = false;
						}
					}

					if (!student.Teacher)
					{
						if (!student.Indoors)
						{
							if (student.ShoeRemoval.Locker == null)
							{
								student.ShoeRemoval.Start();
							}

							student.ShoeRemoval.PutOnShoes();
						}

						student.transform.position = student.Seat.position + (Vector3.up * 0.010f);
						student.transform.rotation = student.Seat.rotation;
						student.Pathfinding.canSearch = true;
						student.Pathfinding.canMove = true;
						//student.SmartPhone.SetActive(false);
						//student.OccultBook.SetActive(false);
						student.Pathfinding.speed = 1.0f;
						student.ClubActivityPhase = 0;
						student.Distracted = false;
						student.Spawned = true;
						student.Routine = true;
						student.Safe = false;

						student.SprintAnim = student.OriginalSprintAnim;

						if (student.ClubAttire)
						{
							student.ChangeSchoolwear();
							student.ClubAttire = true;
						}

						student.TeleportToDestination();
						student.TeleportToDestination();
					}
					else
					{
						student.TeleportToDestination();
						student.TeleportToDestination();
					}

					if (this.MemorialScene.enabled)
					{
						if (TurnBackIntoTeacher)
						{
							student.Teacher = true;
						}

						if (student.Persona == PersonaType.PhoneAddict)
						{
							student.SmartPhone.SetActive(true);
						}

						if (student.Actions[student.Phase] == StudentActionType.Graffiti)
						{
							if (!Bully)
							{
								ScheduleBlock newBlock2 = student.ScheduleBlocks[2];
								newBlock2.destination = "Patrol";
								newBlock2.action = "Patrol";

								student.GetDestinations();
							}
						}

						student.SpeechLines.Stop();

						student.transform.position = new Vector3(20 + (Iterations * 1.1f), 0, 82 - (Column * 5));

						Column++;

						if (Column > 4)
						{
							Iterations++;
							Column = 0;
						}
					}
				}
			}
		}
	}

	public void SkipTo730()
	{
		while (this.NPCsSpawned < this.NPCsTotal)
		{
			this.SpawnStudent(this.SpawnID);
			this.SpawnID++;
		}

		int Iterations = 0;
		int Column = 0;

		// [af] Converted while loop to for loop.
		for (this.ID = 1; this.ID < this.Students.Length; this.ID++)
		{
			StudentScript student = this.Students[this.ID];

			if (student != null)
			{
				if (student.Alive && !student.Slave && !student.Tranquil)
				{
					if (!student.Started)
					{
						student.Start();
					}

					if (!student.Teacher)
					{
						if (!student.Indoors)
						{
							if (student.ShoeRemoval.Locker == null)
							{
								student.ShoeRemoval.Start();
							}

							student.ShoeRemoval.PutOnShoes();
						}

                        student.transform.position = student.Seat.position + (Vector3.up * 0.010f);
                        student.transform.rotation = student.Seat.rotation;
                        student.Pathfinding.canSearch = true;
						student.Pathfinding.canMove = true;
						student.Pathfinding.speed = 1.0f;
						student.ClubActivityPhase = 0;
						student.Distracted = false;
						student.Spawned = true;
						student.Routine = true;
						student.Safe = false;

						student.SprintAnim = student.OriginalSprintAnim;

						if (student.ClubAttire)
						{
							student.ChangeSchoolwear();
							student.ClubAttire = true;
						}

						student.AltTeleportToDestination();
						student.AltTeleportToDestination();
					}
					else
					{
						student.AltTeleportToDestination();
						student.AltTeleportToDestination();
					}
				}
			}
		}

		Physics.SyncTransforms();
	}

	public void ResumeMovement()
	{
		// [af] Converted while loop to for loop.
		for (this.ID = 1; this.ID < this.Students.Length; this.ID++)
		{
			StudentScript student = this.Students[this.ID];

			if (student != null)
			{
				if (!student.Fleeing)
				{
					student.Pathfinding.canSearch = true;
					student.Pathfinding.canMove = true;
					student.Pathfinding.speed = 1.0f;
					student.Routine = true;
				}
			}
		}
	}

	public void StopMoving()
	{
		this.CombatMinigame.enabled = false;
		this.Stop = true;

        if (this.GameOverIminent)
        {
            this.Portal.GetComponent<PortalScript>().EndEvents();
            this.Portal.GetComponent<PortalScript>().EndLaterEvents();
        }

        // [af] Converted while loop to for loop.
        for (this.ID = 1; this.ID < this.Students.Length; this.ID++)
		{
			StudentScript student = this.Students[this.ID];

			if (student != null)
			{
				if (!student.Dying && !student.PinningDown &&
					!student.Spraying && !student.Struggling)
				{
					if (YandereDying && student.Club != ClubType.Council)
					{
						student.IdleAnim = student.ScaredAnim;
					}	

					if (this.Yandere.Attacking)
					{
						if (student.MurderReaction == 0)
						{
							student.Character.GetComponent<Animation>().CrossFade(student.ScaredAnim);
						}
					}
					else
					{
						if (this.ID > 1)
						{
							if (student.CharacterAnimation != null)
							{
								student.CharacterAnimation.CrossFade(student.IdleAnim);
							}
						}
					}

					student.Pathfinding.canSearch = false;
					student.Pathfinding.canMove = false;
					student.Pathfinding.speed = 0.0f;
					student.Stop = true;

					if (student.EventManager != null)
					{
						student.EventManager.EndEvent();
					}
				}

				if (student.Alive)
				{
					if (student.SawMask)
					{
						this.Police.MaskReported = true;
					}
				}

				if (student.Slave && this.Police.DayOver)
				{
					Debug.Log("A mind-broken slave committed suicide.");

					student.Broken.Subtitle.text = string.Empty;
					student.Broken.Done = true;
					Destroy(student.Broken);

					//student.Ragdoll.enabled = true;
					student.BecomeRagdoll();

					student.Slave = false;
					student.Suicide = true;
					student.DeathType = DeathType.Mystery;
					StudentGlobals.StudentSlave = student.StudentID;
				}
			}
		}
	}

	public void TimeFreeze()
	{
		for (this.ID = 1; this.ID < this.Students.Length; this.ID++)
		{
			StudentScript student = this.Students[this.ID];

			if (student != null)
			{
				if (student.Alive)
				{
					student.enabled = false;
					student.CharacterAnimation.Stop();

					student.Pathfinding.canSearch = false;
					student.Pathfinding.canMove = false;

					student.Prompt.Hide();
					student.Prompt.enabled = false;
				}
			}
		}
	}

	public void TimeUnfreeze()
	{
		for (this.ID = 1; this.ID < this.Students.Length; this.ID++)
		{
			StudentScript student = this.Students[this.ID];

			if (student != null)
			{
				if (student.Alive)
				{
					student.enabled = true;
					student.Prompt.enabled = true;

					student.Pathfinding.canSearch = true;
					student.Pathfinding.canMove = true;
				}
			}
		}
	}

	public void ComeBack()
	{
		this.Stop = false;

		for (this.ID = 1; this.ID < this.Students.Length; this.ID++)
		{
			StudentScript student = this.Students[this.ID];

			if (student != null)
			{
                Debug.Log(this.Students[this.ID].Name + "'s expelled status is set to: " + StudentGlobals.GetStudentExpelled(this.ID));

                if (!student.Dying && !student.Replaced && student.Spawned &&
					!StudentGlobals.GetStudentExpelled(this.ID) &&
					!student.Ragdoll.Disposed)
				{
                    student.gameObject.SetActive(true);
					student.Pathfinding.canSearch = true;
					student.Pathfinding.canMove = true;
					student.Pathfinding.speed = 1.0f;
					student.Stop = false;
				}

				if (student.Teacher)
				{
					student.CurrentDestination = student.Destinations[student.Phase];
					student.Pathfinding.target = student.Destinations[student.Phase];

					student.Alarmed = false;
					student.Reacted = false;
					student.Witness = false;
					student.Routine = true;

					student.AlarmTimer = 0;
					student.Concern = 0;
				}

				if (student.Club == ClubType.Council)
				{
					student.Teacher = false;
				}

				if (student.Slave)
				{
					student.Stop = false;
				}
			}
		}

		this.UpdateAllAnimLayers();

		if (Police.EndOfDay.RivalEliminationMethod == RivalEliminationType.Expelled)
		{
			Students[RivalID].gameObject.SetActive(false);
		}

		if (GameGlobals.SenpaiMourning)
		{
			Students[1].gameObject.SetActive(false);
		}
	}

	public void StopFleeing()
	{
		// [af] Converted while loop to for loop.
		for (this.ID = 1; this.ID < this.Students.Length; this.ID++)
		{
			StudentScript student = this.Students[this.ID];

			if (student != null)
			{
				if (!student.Teacher)
				{
					student.Pathfinding.target = student.Destinations[student.Phase];
					student.Pathfinding.speed = 1.0f;

					student.WitnessedCorpse = false;
					student.WitnessedMurder = false;
					student.Alarmed = false;
					student.Fleeing = false;
					student.Reacted = false;
					student.Witness = false;
					student.Routine = true;
				}
			}
		}
	}

	public void EnablePrompts()
	{
		// [af] Converted while loop to for loop.
		for (this.ID = 2; this.ID < this.Students.Length; this.ID++)
		{
			StudentScript student = this.Students[this.ID];

			if (student != null)
			{
				student.Prompt.enabled = true;
			}
		}
	}

	public void DisablePrompts()
	{
		// [af] Converted while loop to for loop.
		for (this.ID = 2; this.ID < this.Students.Length; this.ID++)
		{
			StudentScript student = this.Students[this.ID];

			if (student != null)
			{
				student.Prompt.Hide();
				student.Prompt.enabled = false;
			}
		}
	}

	public void WipePendingRep()
	{
		// [af] Converted while loop to for loop.
		for (this.ID = 2; this.ID < this.Students.Length; this.ID++)
		{
			StudentScript student = this.Students[this.ID];

			if (student != null)
			{
				student.PendingRep = 0.0f;
			}
		}
	}

	public void AttackOnTitan()
	{
		this.AoT = true;

		// [af] Converted while loop to for loop.
		for (this.ID = 2; this.ID < this.Students.Length; this.ID++)
		{
			StudentScript student = this.Students[this.ID];

			if (student != null)
			{
				if (!student.Teacher)
				{
					student.AttackOnTitan();
				}
			}
		}
	}

	public void Kong()
	{
		this.DK = true;

		// [af] Converted while loop to for loop.
		for (this.ID = 1; this.ID < this.Students.Length; this.ID++)
		{
			StudentScript student = this.Students[this.ID];

			if (student != null)
			{
				student.DK = true;
			}
		}
	}

	public void Spook()
	{
		this.Spooky = true;

		// [af] Converted while loop to for loop.
		for (this.ID = 2; this.ID < this.Students.Length; this.ID++)
		{
			StudentScript student = this.Students[this.ID];

			if (student != null)
			{
				if (!student.Male)
				{
					student.Spook();
				}
			}
		}
	}

	public void BadTime()
	{
		this.Sans = true;

		// [af] Converted while loop to for loop.
		for (this.ID = 2; this.ID < this.Students.Length; this.ID++)
		{
			StudentScript student = this.Students[this.ID];

			if (student != null)
			{
				student.Prompt.HideButton[0] = false;
				student.BadTime = true;
			}
		}
	}

	public void UpdateBooths()
	{
		// [af] Converted while loop to for loop. I'd love to use a foreach loop, but
		// I cannot guarantee correctness because "this.ID" is not local.
		for (this.ID = 0; this.ID < this.ChangingBooths.Length; this.ID++)
		{
			ChangingBoothScript booth = this.ChangingBooths[this.ID];

			if (booth != null)
			{
				booth.CheckYandereClub();
			}
		}
	}

	public void UpdatePerception()
	{
		// [af] Converted while loop to for loop.
		for (this.ID = 0; this.ID < this.Students.Length; this.ID++)
		{
			StudentScript student = this.Students[this.ID];

			if (student != null)
			{
				student.UpdatePerception();
			}
		}
	}

	public void StopHesitating()
	{
		// [af] Converted while loop to foreach loop.
		for (this.ID = 0; this.ID < this.Students.Length; this.ID++)
		{
			StudentScript student = this.Students[this.ID];

			if (student != null)
			{
				if (student.AlarmTimer > 0.0f)
				{
					student.AlarmTimer = 1.0f;
				}

				student.Hesitation = 0.0f;
			}
		}
	}

	public void Unstop()
	{
		// [af] Converted while loop to foreach loop.
		for (this.ID = 0; this.ID < this.Students.Length; this.ID++)
		{
			StudentScript student = this.Students[this.ID];

			if (student != null)
			{
				student.Stop = false;
			}
		}
	}

	public void LowerCorpsePosition()
	{
		int Height = 0;

		//Debug.Log ("Corpse's Y position is: " + this.CorpseLocation.position.y);

		     if (this.CorpseLocation.position.y < 2.0f) {Height = 0;}
		else if (this.CorpseLocation.position.y < 4.0f) {Height = 2;}
		else if (this.CorpseLocation.position.y < 6.0f) {Height = 4;}
		else if (this.CorpseLocation.position.y < 8.0f) {Height = 6;}
		else if (this.CorpseLocation.position.y < 10.0f){Height = 8;}
		else if (this.CorpseLocation.position.y < 12.0f){Height = 10;}
		else                                            {Height = 12;}

		this.CorpseLocation.position = new Vector3(
			this.CorpseLocation.position.x,
			Height,
			this.CorpseLocation.position.z);

		//Debug.Log("The corpse's height is: " + Height);

		/*
		if(this.CorpseLocation.position.y < 12.0f) {
			Height = 2 * Math.Floor((this.CorpseLocation.position.y) / 2);
		} else {
			Height = 12;
		}
		*/
	}

	public void LowerBloodPosition()
	{
		//Maybe we shouldn't do this.
		//Maybe we SHOULD do this?

		int Height = 0;

		if (this.BloodLocation.position.y < 2.0f) {Height = 0;}
		else if (this.BloodLocation.position.y < 4.0f) {Height = 2;}
		else if (this.BloodLocation.position.y < 6.0f) {Height = 4;}
		else if (this.BloodLocation.position.y < 8.0f) {Height = 6;}
		else if (this.BloodLocation.position.y < 10.0f){Height = 8;}
		else if (this.BloodLocation.position.y < 12.0f){Height = 10;}
		else                                            {Height = 12;}

		this.BloodLocation.position = new Vector3(
			this.BloodLocation.position.x,
			Height,
			this.BloodLocation.position.z);
	}

	public void CensorStudents()
	{
		// [af] Converted while loop to foreach loop.
		for (this.ID = 0; this.ID < this.Students.Length; this.ID++)
		{
			StudentScript student = this.Students[this.ID];

			if (student != null)
			{
				if (!student.Male &&
					student.Club != ClubType.Teacher &&
					student.Club != ClubType.GymTeacher &&
					student.Club != ClubType.Nurse)
				{
					if (this.Censor)
					{
						student.Cosmetic.CensorPanties();
					}
					else
					{
						student.Cosmetic.RemoveCensor();
					}
				}
			}
		}
	}

	void OccupySeat()
	{
		// [af] Commented in JS code.
		//Debug.Log("SpawnID is: " + SpawnID);
		//Debug.Log("Class is: " + JSON.StudentClasses[SpawnID]);
		//Debug.Log("Seat is: " + JSON.StudentSeats[SpawnID]);

		int spawnedStudentClass = this.JSON.Students[this.SpawnID].Class;
		int spawnedStudentSeat = this.JSON.Students[this.SpawnID].Seat;

		if (spawnedStudentClass == 11)
		{
			this.SeatsTaken11[spawnedStudentSeat] = true;
		}
		else if (spawnedStudentClass == 12)
		{
			this.SeatsTaken12[spawnedStudentSeat] = true;
		}
		else if (spawnedStudentClass == 21)
		{
			this.SeatsTaken21[spawnedStudentSeat] = true;
		}
		else if (spawnedStudentClass == 22)
		{
			this.SeatsTaken22[spawnedStudentSeat] = true;
		}
		else if (spawnedStudentClass == 31)
		{
			this.SeatsTaken31[spawnedStudentSeat] = true;
		}
		else if (spawnedStudentClass == 32)
		{
			this.SeatsTaken32[spawnedStudentSeat] = true;
		}
	}

	public bool SeatOccupied = false;
	public int Class = 1;

	void FindUnoccupiedSeat()
	{
		this.SeatOccupied = false;

		if (this.Class == 1)
		{
			this.JSON.Students[this.SpawnID].Class = 11;

			// [af] Converted while loop to for loop.
			for (this.ID = 1; (this.ID < this.SeatsTaken11.Length) && !this.SeatOccupied;)
			{
				if (!this.SeatsTaken11[this.ID])
				{
					this.JSON.Students[this.SpawnID].Seat = this.ID;
					this.SeatsTaken11[this.ID] = true;
					this.SeatOccupied = true;
				}

				this.ID++;

				if (this.ID > 15)
				{
					this.Class++;
				}
			}
		}
		else if (this.Class == 2)
		{
			this.JSON.Students[this.SpawnID].Class = 12;

			// [af] Converted while loop to for loop.
			for (this.ID = 1; (this.ID < this.SeatsTaken12.Length) && !this.SeatOccupied;)
			{
				if (!this.SeatsTaken12[this.ID])
				{
					this.JSON.Students[this.SpawnID].Seat = this.ID;
					this.SeatsTaken12[this.ID] = true;
					this.SeatOccupied = true;
				}

				this.ID++;

				if (this.ID > 15)
				{
					this.Class++;
				}
			}
		}
		else if (this.Class == 3)
		{
			this.JSON.Students[this.SpawnID].Class = 21;

			// [af] Converted while loop to for loop.
			for (this.ID = 1; (this.ID < this.SeatsTaken21.Length) && !this.SeatOccupied;)
			{
				if (!this.SeatsTaken21[this.ID])
				{
					this.JSON.Students[this.SpawnID].Seat = this.ID;
					this.SeatsTaken21[this.ID] = true;
					this.SeatOccupied = true;
				}

				this.ID++;

				if (this.ID > 15)
				{
					this.Class++;
				}
			}
		}
		else if (this.Class == 4)
		{
			this.JSON.Students[this.SpawnID].Class = 22;

			// [af] Converted while loop to for loop.
			for (this.ID = 1; (this.ID < this.SeatsTaken22.Length) && !this.SeatOccupied;)
			{
				if (!this.SeatsTaken22[this.ID])
				{
					this.JSON.Students[this.SpawnID].Seat = this.ID;
					this.SeatsTaken22[this.ID] = true;
					this.SeatOccupied = true;
				}

				this.ID++;

				if (this.ID > 15)
				{
					this.Class++;
				}
			}
		}
		else if (this.Class == 5)
		{
			this.JSON.Students[this.SpawnID].Class = 31;

			// [af] Converted while loop to for loop.
			for (this.ID = 1; (this.ID < this.SeatsTaken31.Length) && !this.SeatOccupied;)
			{
				if (!this.SeatsTaken31[this.ID])
				{
					this.JSON.Students[this.SpawnID].Seat = this.ID;
					this.SeatsTaken31[this.ID] = true;
					this.SeatOccupied = true;
				}

				this.ID++;

				if (this.ID > 15)
				{
					this.Class++;
				}
			}
		}
		else if (this.Class == 6)
		{
			this.JSON.Students[this.SpawnID].Class = 32;

			// [af] Converted while loop to for loop.
			for (this.ID = 1; (this.ID < this.SeatsTaken32.Length) && !this.SeatOccupied;)
			{
				if (!this.SeatsTaken32[this.ID])
				{
					this.JSON.Students[this.SpawnID].Seat = this.ID;
					this.SeatsTaken32[this.ID] = true;
					this.SeatOccupied = true;
				}

				this.ID++;

				if (this.ID > 15)
				{
					this.Class++;
				}
			}
		}

		if (!this.SeatOccupied)
		{
			this.FindUnoccupiedSeat();
		}
	}

	public void PinDownCheck()
	{
		if (!this.PinningDown)
		{
			if (this.Witnesses > 3)
			{
				// [af] Converted while loop to for loop.
				for (this.ID = 1; this.ID < this.WitnessList.Length; this.ID++)
				{
					StudentScript witness = this.WitnessList[this.ID];

					if (witness != null)
					{
						if (!witness.Alive || witness.Attacked ||
                            witness.Fleeing || witness.Dying ||
                            witness.Routine)
						{
							witness = null;

							if (this.ID != (this.WitnessList.Length - 1))
							{
								this.Shuffle(this.ID);
							}

							this.Witnesses--;
						}
					}
				}

				if (this.Witnesses > 3)
				{
					this.PinningDown = true;
					this.PinPhase = 1;
				}
			}
		}
	}

	void Shuffle(int Start)
	{
		// [af] Converted while loop to for loop.
		for (int ShuffleID = Start; ShuffleID < (this.WitnessList.Length - 1); ShuffleID++)
		{
			this.WitnessList[ShuffleID] = this.WitnessList[ShuffleID + 1];
		}
	}

	/*
	public void ChangeOka()
	{
		StudentScript student26 = this.Students[26];

		if (student26 != null)
		{
			student26.AttachRiggedAccessory();
		}
	}
	*/

#if UNITY_EDITOR
	public void ChangeRibaru()
	{
		StudentScript student6 = this.Students[6];

		if (student6 != null)
		{
			student6.AttachRiggedAccessory();
		}
	}
#endif

	public void RemovePapersFromDesks()
	{
		// [af] Converted while loop to for loop.
		for (this.ID = 1; this.ID < this.Students.Length; this.ID++)
		{
			StudentScript student = this.Students[this.ID];

			if ((student != null) && (student.MyPaper != null))
			{
				student.MyPaper.SetActive(false);
			}
		}
	}

	public void SetStudentsActive(bool active)
	{
		for (this.ID = 1; this.ID < this.Students.Length; this.ID++)
		{
			StudentScript student = this.Students[this.ID];

			if (student != null)
			{
				student.gameObject.SetActive(active);
			}
		}
	}

	public void AssignTeachers()
	{
		for (this.ID = 1; this.ID < this.Students.Length; this.ID++)
		{
			StudentScript student = this.Students[this.ID];

			if (student != null)
			{
				student.MyTeacher = 
					this.Teachers[this.JSON.Students[student.StudentID].Class];
			}
		}
	}

	public void ToggleBookBags()
	{
		for (this.ID = 1; this.ID < this.Students.Length; this.ID++)
		{
			StudentScript student = this.Students[this.ID];

			if (student != null)
			{
				student.BookBag.SetActive(!student.BookBag.activeInHierarchy);
			}
		}
	}

	public void DetermineVictim()
	{
		this.Bully = false;

		for (this.ID = 2; this.ID < this.Students.Length; this.ID++)
		{
			StudentScript student = this.Students[this.ID];

			if (student != null)
			{
				if (StudentGlobals.GetStudentReputation(ID) < 33.33333f)
				{
					if (ID == 36 && TaskGlobals.GetTaskStatus(36) == 3)
					{
						//This is Handsome Gema, don't bully him.
					}
					else if (!student.Teacher && !student.Slave
						&& student.Club != ClubType.Bully
						&& student.Club != ClubType.Council
						&& student.Club != ClubType.Photography
						&& student.Club != ClubType.Delinquent)
					{
						if (StudentGlobals.GetStudentReputation(ID) < this.LowestRep)
						{
							this.LowestRep = StudentGlobals.GetStudentReputation(ID);
							this.VictimID = ID;
							this.Bully = true;
						}
					}
				}
			}
		}

		if (this.Bully)
		{
			Debug.Log ("A student has been chosen to be bullied. It's Student #" + this.VictimID + ".");

			if (this.Students[this.VictimID].Seat.position.x > 0)
			{
				this.BullyGroup.position = this.Students[this.VictimID].Seat.position + new Vector3(0.33333f, 0, 0);
			}
			else
			{
				this.BullyGroup.position = this.Students[this.VictimID].Seat.position - new Vector3(0.33333f, 0, 0);

				this.BullyGroup.eulerAngles = new Vector3(0, 90, 0);
			}

			StudentScript victim = this.Students[this.VictimID];

			//victim.WalkAnim = victim.ShameWalkAnim;
			//victim.IdleAnim = victim.ShameIdleAnim;

			ScheduleBlock block2 = victim.ScheduleBlocks[2];
			block2.destination = "ShameSpot";
			block2.action = "Shamed";
			block2.time = 8;

			ScheduleBlock block4 = victim.ScheduleBlocks[4];
			block4.destination = "Seat";
			block4.action = "Sit";

			if (victim.Male)
			{
				victim.ChemistScanner.MyRenderer.materials[1].mainTexture = victim.ChemistScanner.SadEyes;
				victim.ChemistScanner.enabled = false;
			}

			victim.IdleAnim = victim.BulliedIdleAnim;
			victim.WalkAnim = victim.BulliedWalkAnim;
			victim.Bullied = true;

			victim.GetDestinations();

			victim.CameraAnims = victim.CowardAnims;
			victim.BusyAtLunch = true;
			victim.Shy = false;
		}

		//Debug.Log("As of now, the ''Bully'' boolean is set to " + this.Bully);
	}

	public void SecurityCameras()
	{
		this.Egg = true;

		for (this.ID = 1; this.ID < this.Students.Length; this.ID++)
		{
			StudentScript student = this.Students[this.ID];

			if (student != null)
			{
				if (student.SecurityCamera != null)
				{
					if (student.Alive)
					{
						Debug.Log("Enabling security camera on this character's head.");
						student.SecurityCamera.SetActive(true);
					}
				}
			}
		}
	}

	public void DisableEveryone()
	{
		for (this.ID = 1; this.ID < this.Students.Length; this.ID++)
		{
			StudentScript student = this.Students[this.ID];

			if (student != null)
			{
				if (!student.Ragdoll.enabled)
				{
					student.gameObject.SetActive(false);
				}
			}
		}
	}

	public void DisableStudent(int DisableID)
	{
		StudentScript student = this.Students[DisableID];

		if (student != null)
		{
			if (student.gameObject.activeInHierarchy)
			{
				student.gameObject.SetActive(false);
			}
			else
			{
				student.gameObject.SetActive(true);
				this.UpdateOneAnimLayer(DisableID);
				this.Students[DisableID].ReadPhase = 0;
			}
		}
	}

	public void UpdateOneAnimLayer(int DisableID)
	{
		this.Students[DisableID].UpdateAnimLayers();
		this.Students[DisableID].ReadPhase = 0;
	}

	public void UpdateAllAnimLayers()
	{
		for (this.ID = 1; this.ID < this.Students.Length; this.ID++)
		{
			StudentScript student = this.Students[this.ID];

			if (student != null)
			{
				student.UpdateAnimLayers();
				student.ReadPhase = 0;
			}
		}
	}

	public void UpdateGrafitti()
	{
		ID = 1;

		while (ID < 6)
		{
			if (!NoBully[ID])
			{
				Graffiti[ID].SetActive(true);
			}

			ID++;
		}
	}

	public void UpdateAllBentos()
	{
		for (this.ID = 1; this.ID < this.Students.Length; this.ID++)
		{
			StudentScript student = this.Students[this.ID];

			if (student != null)
			{
				student.Bento.GetComponent<GenericBentoScript>().Prompt.Yandere = Yandere;
				student.Bento.GetComponent<GenericBentoScript>().UpdatePrompts();
			}
		}
	}

	public void UpdateSleuths()
	{
		//Debug.Log("The sleuths have been updated!");

		this.SleuthPhase++;

		ID = 56;

		while (ID < 61)
		{
			if (this.Students[ID] != null)
			{
				if (!this.Students[ID].Slave && !this.Students[ID].Following)
				{
					if (this.SleuthPhase < 3)
					{
						this.Students[ID].SleuthTarget = this.SleuthDestinations[ID - 55];
						this.Students[ID].Pathfinding.target = this.Students[ID].SleuthTarget;
						this.Students[ID].CurrentDestination = this.Students[ID].SleuthTarget;
					}
					else if (this.SleuthPhase == 3)
					{
						this.Students[ID].GetSleuthTarget();
					}
					else if (this.SleuthPhase == 4)
					{
						this.Students[ID].SleuthTarget = this.Clubs.List[ID];
						this.Students[ID].Pathfinding.target = this.Students[ID].SleuthTarget;
						this.Students[ID].CurrentDestination = this.Students[ID].SleuthTarget;
					}

					this.Students[ID].SmartPhone.SetActive(true);
					this.Students[ID].SpeechLines.Stop();
				}
			}

			ID++;
		}
	}

	public void UpdateDrama()
	{
		if (!this.MemorialScene.gameObject.activeInHierarchy)
		{
			this.DramaPhase++;

			//Debug.Log("DramaPhase is: " + DramaPhase);

			ID = 26;

			while (ID < 31)
			{
				if (this.Students[ID] != null)
				{
					if (this.DramaPhase == 1)
					{
						this.Clubs.List[ID].position = this.OriginalClubPositions[ID - 25];
						this.Clubs.List[ID].rotation = this.OriginalClubRotations[ID - 25];

						this.Students[ID].ClubAnim = this.Students[ID].OriginalClubAnim;
					}
					else if (this.DramaPhase == 2)
					{
						this.Clubs.List[ID].position = this.DramaSpots[ID - 25].position;
						this.Clubs.List[ID].rotation = this.DramaSpots[ID - 25].rotation;

						     if (ID == 26){this.Students[ID].ClubAnim = this.Students[ID].ActAnim;}
						else if (ID == 27){this.Students[ID].ClubAnim = this.Students[ID].ThinkAnim;}
						else if (ID == 28){this.Students[ID].ClubAnim = this.Students[ID].ThinkAnim;}
						else if (ID == 29){this.Students[ID].ClubAnim = this.Students[ID].ActAnim;}
						else if (ID == 30){this.Students[ID].ClubAnim = this.Students[ID].ThinkAnim;}
					}
					else if (this.DramaPhase == 3)
					{
						this.Clubs.List[ID].position = this.BackstageSpots[ID - 25].position;
						this.Clubs.List[ID].rotation = this.BackstageSpots[ID - 25].rotation;
					}
					else if (this.DramaPhase == 4)
					{
						this.DramaPhase = 1;
						UpdateDrama();
					}

					this.Students[ID].DistanceToDestination = 100;
					this.Students[ID].SmartPhone.SetActive(false);
					this.Students[ID].SpeechLines.Stop();
				}

				ID++;
			}
		}
	}

	public void UpdateMartialArts()
	{
		this.ConvoManager.Confirmed = false;

		this.MartialArtsPhase++;

		ID = 46;

		while (ID < 51)
		{
			if (this.Students[ID] != null)
			{
				if (this.MartialArtsPhase == 1)
				{
					this.Clubs.List[ID].position = this.MartialArtsSpots[ID - 45].position;
					this.Clubs.List[ID].rotation = this.MartialArtsSpots[ID - 45].rotation;
				}
				else if (this.MartialArtsPhase == 2)
				{
					this.Clubs.List[ID].position = this.MartialArtsSpots[ID - 40].position;
					this.Clubs.List[ID].rotation = this.MartialArtsSpots[ID - 40].rotation;
				}
				else if (this.MartialArtsPhase == 3)
				{
					this.Clubs.List[ID].position = this.MartialArtsSpots[ID - 35].position;
					this.Clubs.List[ID].rotation = this.MartialArtsSpots[ID - 35].rotation;
				}
				else if (this.MartialArtsPhase == 4)
				{
					this.MartialArtsPhase = 0;
					UpdateMartialArts();
				}

				this.Students[ID].DistanceToDestination = 100;
				this.Students[ID].SmartPhone.SetActive(false);
				this.Students[ID].SpeechLines.Stop();
			}

			ID++;
		}
	}

	public void UpdateMeeting()
	{
		MeetingTimer += Time.deltaTime;

		if (MeetingTimer > 5)
		{
			Speaker += 5;

			if (Speaker == 91)
			{
				Speaker = 21;
			}
			else if (Speaker == 76)
			{
				Speaker = 86;
			}
			else if (Speaker == 36)
			{
				Speaker = 41;
			}

			MeetingTimer = 0;
		}
	}

	public void CheckMusic()
	{
		int Ready = 0;

		ID = 51;

		while (ID < 56)
		{
			if (Students[ID] != null)
			{
				if (Students[ID].Routine && Students[ID].DistanceToDestination < .1f)
				{
					Ready++;
				}
			}

			ID++;
		}

		if (Ready == 5)
		{
			PracticeVocals.pitch = Time.timeScale;
			PracticeMusic.pitch = Time.timeScale;

			if (!PracticeMusic.isPlaying)
			{
				PracticeVocals.Play();
				PracticeMusic.Play();
			}
		}
		else
		{
			PracticeVocals.Stop();
			PracticeMusic.Stop();
		}
	}

	public void UpdateAprons()
	{
		ID = 21;

		while (ID < 26)
		{
			if (this.Students[ID] != null)
			{
				if (Students[ID].ClubMemberID > 0)
				{
					if (this.Students[ID].ApronAttacher != null)
					{
						if (this.Students[ID].ApronAttacher.newRenderer != null)
						{
							this.Students[ID].ApronAttacher.newRenderer.material.mainTexture = this.Students[ID].Cosmetic.ApronTextures[this.Students[ID].ClubMemberID];
						}
					}
				}
			}

			ID++;
		}
	}

	public void PreventAlarm()
	{
		ID = 1;

		while (ID < 101)
		{
			if (this.Students[ID] != null)
			{
				this.Students[ID].Alarm = 0;
			}

			ID++;
		}
	}

	public void VolumeDown()
	{
		ID = 51;

		while (ID < 56)
		{
			if (this.Students[ID] != null)
			{
				if (this.Students[ID].Instruments[this.Students[ID].ClubMemberID] != null)
				{
					this.Students[ID].Instruments[this.Students[ID].ClubMemberID].GetComponent<AudioSource>().volume = .2f;
				}
			}

			ID++;
		}
	}

	public void VolumeUp()
	{
		ID = 51;

		while (ID < 56)
		{
			if (this.Students[ID] != null)
			{
				if (this.Students[ID].Instruments[this.Students[ID].ClubMemberID] != null)
				{
					this.Students[ID].Instruments[this.Students[ID].ClubMemberID].GetComponent<AudioSource>().volume = 1;
				}
			}

			ID++;
		}
	}

	public void GetMaleVomitSpot(StudentScript VomitStudent)
	{
		this.MaleVomitSpot = this.MaleVomitSpots[1];
		VomitStudent.VomitDoor = this.MaleToiletDoors[1];
		ID = 2;

		while (ID < 7)
		{
			if (Vector3.Distance(VomitStudent.transform.position, this.MaleVomitSpots[ID].position) <
				Vector3.Distance(VomitStudent.transform.position, this.MaleVomitSpot.position))
			{
				this.MaleVomitSpot = this.MaleVomitSpots[ID];
				VomitStudent.VomitDoor = this.MaleToiletDoors[ID];
			}

			ID++;
		}
	}

	public void GetFemaleVomitSpot(StudentScript VomitStudent)
	{
		this.FemaleVomitSpot = this.FemaleVomitSpots[1];
		VomitStudent.VomitDoor = this.FemaleToiletDoors[1];
		ID = 2;

		while (ID < 7)
		{
			if (Vector3.Distance(VomitStudent.transform.position, this.FemaleVomitSpots[ID].position) <
				Vector3.Distance(VomitStudent.transform.position, this.FemaleVomitSpot.position))
			{
				this.FemaleVomitSpot = this.FemaleVomitSpots[ID];
				VomitStudent.VomitDoor = this.FemaleToiletDoors[ID];
			}

			ID++;
		}
	}

	public void GetMaleWashSpot(StudentScript VomitStudent)
	{
		Transform NearestWashSpot = MaleWashSpots[1];
		ID = 2;

		while (ID < 7)
		{
			if (Vector3.Distance(VomitStudent.transform.position, MaleWashSpots[ID].position) <
				Vector3.Distance(VomitStudent.transform.position, NearestWashSpot.position))
			{
				NearestWashSpot = MaleWashSpots[ID];
			}

			ID++;
		}

		this.MaleWashSpot = NearestWashSpot;
	}

	public void GetFemaleWashSpot(StudentScript VomitStudent)
	{
		Transform NearestWashSpot = FemaleWashSpots[1];
		ID = 2;

		while (ID < 7)
		{
			if (Vector3.Distance(VomitStudent.transform.position, FemaleWashSpots[ID].position) <
				Vector3.Distance(VomitStudent.transform.position, NearestWashSpot.position))
			{
				NearestWashSpot = FemaleWashSpots[ID];
			}

			ID++;
		}

		this.FemaleWashSpot = NearestWashSpot;
	}

	public void GetNearestFountain (StudentScript Student)
	{
		DrinkingFountainScript NearestFountain = DrinkingFountains[1];
		bool ForgetIt = false;

		ID = 1;

		while (NearestFountain.Occupied)
		{
			NearestFountain = DrinkingFountains[1 + ID];
			ID++;

			if (1 + ID == DrinkingFountains.Length)
			{
				ForgetIt = true;
				break;
			}
		}

		if (ForgetIt)
		{
			Student.EquipCleaningItems();

			Student.EatingSnack = false;
			Student.Private = false;
			Student.Routine = true;
			Student.StudentManager.UpdateMe(Student.StudentID);

			Student.CurrentDestination = Student.Destinations[Student.Phase];
			Student.Pathfinding.target = Student.Destinations[Student.Phase];
		}
		else
		{
			ID = 2;

			while (ID < 8)
			{
				if (Vector3.Distance(Student.transform.position, DrinkingFountains[ID].transform.position) <
					Vector3.Distance(Student.transform.position, NearestFountain.transform.position))
				{
					if (!DrinkingFountains[ID].Occupied)
					{
						//Debug.Log("Drinking Fountain #" + ID + " + is NOT occupied!");

						NearestFountain = DrinkingFountains[ID];
					}
				}

				ID++;
			}

			Student.DrinkingFountain = NearestFountain;
			Student.DrinkingFountain.Occupied = true;
		}
	}

	public int Thins;
	public int Seriouses;
	public int Rounds;
	public int Sads;
	public int Means;
	public int Smugs;
	public int Gentles;
	public int Rival1s;

	public DoorScript[] Doors;
	public int DoorID;

	public void Save()
	{
        int Profile = GameGlobals.Profile;
        int Slot = PlayerPrefs.GetInt("SaveSlot");

        Debug.Log("At the moment of saving, Himari's Phase was: " + Students[72].Phase + " and her PatrolID was: " + Students[72].PatrolID);
        //Debug.Log("At the moment of saving, ClubGlobals.Club is: " + ClubGlobals.Club);

        BloodParent.RecordAllBlood();
        YanSave.SaveData("Profile_" + Profile + "_Slot_" + Slot);
        PlayerPrefs.SetInt("Profile_" + Profile + "_Slot_" + Slot + "_MemorialStudents", StudentGlobals.MemorialStudents);

        //Debug.Log("WHILE SAVING PlayerPrefs.GetInt(''Profile_1_StudentDead_4'') is: " + PlayerPrefs.GetInt("Profile_1_StudentDead_4"));
        //Debug.Log("WHILE SAVING StudentGlobals.GetStudentDead(4) is: " + StudentGlobals.GetStudentDead(4));
    }

	public void Load()
	{
        Debug.Log("Now loading save data.");

        int Profile = GameGlobals.Profile;
        int Slot = PlayerPrefs.GetInt("SaveSlot");

        Debug.Log("Before loading data, Himari's Phase was: " + Students[72].Phase + " and her PatrolID was: " + Students[72].PatrolID);

        YanSave.LoadData("Profile_" + Profile + "_Slot_" + Slot);

        Debug.Log("After loading data, Himari's Phase was: " + Students[72].Phase + " and her PatrolID was: " + Students[72].PatrolID);

        Physics.SyncTransforms();
        //this.Yandere.FixCamera();

        //this.Police.Corpses = 0;
        //this.Police.Corpses += this.Yandere.Incinerator.Corpses;

        ID = 1;

		while (ID < 101)
		{
			if (this.Students[ID] != null)
			{
                if (!this.Students[ID].Alive)
                {
                    //Debug.Log(this.Students[ID].Name + " is confirmed to be dead.");

                    Vector3 HipsPosition = this.Students[ID].Hips.localPosition;
                    Quaternion HipsRotation = this.Students[ID].Hips.localRotation;

                    this.Students[ID].BecomeRagdoll();
                    this.Students[ID].Ragdoll.UpdateNextFrame = true;
                    this.Students[ID].Ragdoll.NextPosition = HipsPosition;
                    this.Students[ID].Ragdoll.NextRotation = HipsRotation;

                    //Debug.Log("Adding " + this.Students[ID].Name + " to the Police CorpseList.");

                    this.Police.CorpseList[this.Police.Corpses] = this.Students[ID].Ragdoll;
                    this.Police.Corpses++;

                    if (this.Students[ID].Removed)
                    {
                        //Debug.Log("Removing " + this.Students[ID].Name + " from the Police CorpseList.");

                        this.Students[ID].Ragdoll.Remove();
                        this.Police.Corpses--;
                    }
                }
                //If this student is alive...
                else
                {
                    this.Students[ID].ReturningFromSave = true;
                    this.Students[ID].PhaseFromSave = this.Students[ID].Phase;

                    if (this.Students[ID].ChangingShoes)
                    {
                        this.Students[ID].ShoeRemoval.enabled = true;
                    }

                    if (this.Students[ID].Schoolwear != 1)
                    {
                        this.Students[ID].ChangeSchoolwear();
                    }

                    if (this.Students[ID].ClubAttire)
                    {
                        int OriginalPhase = this.Students[ID].ClubActivityPhase;

                        this.Students[ID].ClubAttire = false;

                        //Swimming club special case
                        if (this.Students[ID].ClubActivityPhase > 14)
                        {
                            if (this.Students[ID].ClubActivityPhase == 18 ||
                                this.Students[ID].ClubActivityPhase == 19)
                            {
                                this.Students[ID].Destinations[this.Students[ID].Phase] =
                                    this.Clubs.List[ID].GetChild(this.Students[ID].ClubActivityPhase - 2);

                                this.Students[ID].Destinations[this.Students[ID].Phase + 1] =
                                    this.Clubs.List[ID].GetChild(this.Students[ID].ClubActivityPhase - 2);

                                this.Students[ID].CurrentDestination = this.Clubs.List[ID].GetChild(this.Students[ID].ClubActivityPhase - 2);
                                this.Students[ID].Pathfinding.target = this.Clubs.List[ID].GetChild(this.Students[ID].ClubActivityPhase - 2);

                                this.Students[ID].Character.transform.localPosition = new Vector3(0, -.25f, 0);
                                this.Students[ID].CurrentAction = StudentActionType.ClubAction;
                                this.Students[ID].WalkAnim = "poolSwim_00";
                                this.Students[ID].ClubAnim = "poolSwim_00";
                                this.Students[ID].SetSplashes(true);
                                this.Students[ID].Phase++;
                            }

                            this.Clock.Period = 3;
                        }

                        this.Students[ID].ChangeClubwear();

                        if (this.Students[ID].ClubActivityPhase > 14)
                        {
                            this.Students[ID].ClubActivityPhase = OriginalPhase;
                        }
                    }

                    if (this.Students[ID].Defeats > 0)
                    {
                        this.Students[ID].IdleAnim = AnimNames.MaleInjuredIdle;
                        this.Students[ID].WalkAnim = AnimNames.MaleInjuredWalk;

                        this.Students[ID].OriginalIdleAnim = this.Students[ID].IdleAnim;
                        this.Students[ID].OriginalWalkAnim = this.Students[ID].WalkAnim;

                        this.Students[ID].LeanAnim = this.Students[ID].IdleAnim;

                        this.Students[ID].CharacterAnimation.CrossFade(this.Students[ID].IdleAnim);

                        this.Students[ID].Injured = true;
                        this.Students[ID].Strength = 0;

                        ScheduleBlock newBlock2 = Students[ID].ScheduleBlocks[2];
                        newBlock2.destination = "Sulk";
                        newBlock2.action = "Sulk";

                        ScheduleBlock newBlock4 = Students[ID].ScheduleBlocks[4];
                        newBlock4.destination = "Sulk";
                        newBlock4.action = "Sulk";

                        ScheduleBlock newBlock6 = Students[ID].ScheduleBlocks[6];
                        newBlock6.destination = "Sulk";
                        newBlock6.action = "Sulk";

                        ScheduleBlock newBlock7 = Students[ID].ScheduleBlocks[7];
                        newBlock7.destination = "Sulk";
                        newBlock7.action = "Sulk";

                        Students[ID].GetDestinations();
                    }

                    if (this.Students[ID].Actions[this.Students[ID].Phase] == StudentActionType.ClubAction &&
                        this.Students[ID].Club == ClubType.Cooking && this.Students[ID].ClubActivityPhase > 0)
                    {
                        this.Students[ID].MyPlate.parent = this.Students[ID].RightHand;
                        this.Students[ID].MyPlate.localPosition = new Vector3(0.02f, -.02f, -0.15f);
                        this.Students[ID].MyPlate.localEulerAngles = new Vector3(-5.0f, -90.0f, 172.5f);

                        this.Students[ID].IdleAnim = this.Students[ID].PlateIdleAnim;
                        this.Students[ID].WalkAnim = this.Students[ID].PlateWalkAnim;
                        this.Students[ID].LeanAnim = this.Students[ID].PlateIdleAnim;

                        this.Students[ID].GetFoodTarget();
                        this.Students[ID].ClubTimer = 0;
                    }
                    else
                    {
                        if (this.Students[ID].Phase > 0)
                        {
                            this.Students[ID].Phase--;
                        }
                    }
                }
            }

			ID++;
		}

        Clock.UpdateClock();
        Alphabet.UpdateText();

        ClubManager.ActivateClubBenefit();

        Yandere.CanMove = true;
        Yandere.ClubAccessory();
        Yandere.Inventory.UpdateMoney();
        Yandere.WeaponManager.EquipWeaponsFromSave();
        Yandere.WeaponManager.RestoreWeaponToStudent();
        Yandere.WeaponManager.UpdateDelinquentWeapons();

        Mirror.UpdatePersona();

        if (Yandere.ClubAttire)
        {
            Yandere.ClubAttire = false;
            Yandere.ChangeClubwear();
        }

		foreach (DoorScript Door in Doors)
		{
			if (Door != null)
			{
                if (Door.Open)
                {
					Door.OpenDoor();
				}
			}
		}

        foreach (BodyHidingLockerScript BodyHidingLocker in BodyHidingLockers)
        {
            if (BodyHidingLocker != null)
            {
                if (BodyHidingLocker.StudentID > 0)
                {
                    BodyHidingLocker.UpdateCorpse();
                }
            }
        }

        BloodParent.RestoreAllBlood();

        Debug.Log("After performing the entire loading process, Himari's Phase is: " + Students[72].Phase + " and her PatrolID is: " + Students[72].PatrolID);
    }

	public void UpdateBlood()
	{
		if (this.Police.BloodParent.childCount > 0)
		{
			ID = 0;

			foreach (Transform child in Police.BloodParent)
			{
				if (ID < 100)
				{
					Blood[ID] = child.gameObject.GetComponent<Collider>();
					ID++;
				}
			}
		}

		if (this.Police.BloodParent.childCount > 0 || this.Police.LimbParent.childCount > 0)
		{
			ID = 0;

			foreach (Transform child in Police.LimbParent)
			{
				if (ID < 100)
				{
					Limbs[ID] = child.gameObject.GetComponent<Collider>();
					ID++;
				}
			}
		}
	}

	public void CanAnyoneSeeYandere()
	{
		this.YandereVisible = false;

		foreach (StudentScript student in Students)
		{
			if (student != null)
			{
				if (student.CanSeeObject(student.Yandere.gameObject, student.Yandere.HeadPosition))
				{
					this.YandereVisible = true;
					break;
				}
			}
		}
	}

	public void SetFaces(float alpha)
	{
		foreach (StudentScript student in Students)
		{
			if (student != null)
			{
				if (student.StudentID > 1)
				{
					/*
					if (student.Male)
					{
						student.MyRenderer.materials[student.Cosmetic.FaceID].color = new Color(1 - alpha, 1 - alpha, 1 - alpha, 1);
					}
					else
					{
						student.MyRenderer.materials[2].color = new Color(1 - alpha, 1 - alpha, 1 - alpha, 1);
					}
					*/

        student.MyRenderer.materials[0].color = new Color(1 - alpha, 1 - alpha, 1 - alpha, 1);
					student.MyRenderer.materials[1].color = new Color(1 - alpha, 1 - alpha, 1 - alpha, 1);
					student.MyRenderer.materials[2].color = new Color(1 - alpha, 1 - alpha, 1 - alpha, 1);
					student.Cosmetic.LeftEyeRenderer.material.color = new Color(1 - alpha, 1 - alpha, 1 - alpha, 1);
					student.Cosmetic.RightEyeRenderer.material.color = new Color(1 - alpha, 1 - alpha, 1 - alpha, 1);
					student.Cosmetic.HairRenderer.material.color = new Color(1 - alpha, 1 - alpha, 1 - alpha, 1);
				}
			}
		}
	}

	public void DisableChaseCameras()
	{
		foreach (StudentScript student in Students)
		{
			if (student != null)
			{
				student.ChaseCamera.SetActive(false);
			}
		}
	}

	public void InitializeReputations()
	{
		//Liked, Respected, Feared

		////////////////////
		///// CLUBLESS /////
		////////////////////
		
		//Taro Yamada (Senpai)
		StudentGlobals.SetReputationTriangle(1, new Vector3(0, 0, 0));
		//Sakyu Basu
		StudentGlobals.SetReputationTriangle(2, new Vector3(70, -10, 10));
		//Inkyu Basu
		StudentGlobals.SetReputationTriangle(3, new Vector3(50, -10, 30));
		//Kuu Dere
		StudentGlobals.SetReputationTriangle(4, new Vector3(0, 10, 0));
		//Horuda Puresu
		StudentGlobals.SetReputationTriangle(5, new Vector3(-50, -30, 10));
		//Kyuji Konagawa (Lobster Boy)
		StudentGlobals.SetReputationTriangle(6, new Vector3(30, 0, 0));
		//Otohiko Meichi (Pink-Haired Clumsy Boy)
		StudentGlobals.SetReputationTriangle(7, new Vector3(-10, -10, -10));
		//Hazu Kashibuchi (Hentai Protagonist)
		StudentGlobals.SetReputationTriangle(8, new Vector3(0, 10, -30));
		//Toga Tabara (Discount Senpai)
		StudentGlobals.SetReputationTriangle(9, new Vector3(0, 0, 0));
		//Raibaru Fumetsu
		StudentGlobals.SetReputationTriangle(10, new Vector3(100, 100, 100));

		//////////////////
		///// RIVALS /////
		//////////////////

		//Osana Najimi
		StudentGlobals.SetReputationTriangle(11, new Vector3(100, 100, 0));
		//Amai Odayaka
		StudentGlobals.SetReputationTriangle(12, new Vector3(100, 100, -10));
		//Kizana Sunobu
		StudentGlobals.SetReputationTriangle(13, new Vector3(-10, 100, 100));
		//Oka Ruto
		StudentGlobals.SetReputationTriangle(14, new Vector3(0, 100, -10));
		//Asu Rito
		StudentGlobals.SetReputationTriangle(15, new Vector3(100, 100, 0));
		//Muja Kina
		StudentGlobals.SetReputationTriangle(16, new Vector3(0, -10, 0));
		//Mida Rana
		StudentGlobals.SetReputationTriangle(17, new Vector3(-10, -10, 50));
		//Osoro Shidesu
		StudentGlobals.SetReputationTriangle(18, new Vector3(-100, -100, 100));
		//Hanako Yamada
		StudentGlobals.SetReputationTriangle(19, new Vector3(10, 0, 0));
		//Megami Saikou
		StudentGlobals.SetReputationTriangle(20, new Vector3(100, 100, 100));

		/////////////////
		///// CLUBS /////
		/////////////////

		//Cooking Club
		StudentGlobals.SetReputationTriangle(21, new Vector3(50, 100, 0));
		StudentGlobals.SetReputationTriangle(22, new Vector3(30, 50, 0));
		StudentGlobals.SetReputationTriangle(23, new Vector3(50, 50, 0));
		StudentGlobals.SetReputationTriangle(24, new Vector3(30, 50, 10));
		StudentGlobals.SetReputationTriangle(25, new Vector3(70, 50, -30));

		//Drama Club
		StudentGlobals.SetReputationTriangle(26, new Vector3(-10, 100, 0));
		StudentGlobals.SetReputationTriangle(27, new Vector3(0, 70, 0));
		StudentGlobals.SetReputationTriangle(28, new Vector3(0, 50, 0));
		StudentGlobals.SetReputationTriangle(29, new Vector3(-10, 50, 0));
		StudentGlobals.SetReputationTriangle(30, new Vector3(30, 50, 0));

		//Occult Club
		StudentGlobals.SetReputationTriangle(31, new Vector3(-70, 100, 10));
		StudentGlobals.SetReputationTriangle(32, new Vector3(-70, -10, 10));
		StudentGlobals.SetReputationTriangle(33, new Vector3(-70, -10, 10));
		StudentGlobals.SetReputationTriangle(34, new Vector3(-70, -10, 10));
		StudentGlobals.SetReputationTriangle(35, new Vector3(-70, -10, 10));

		//Gaming Club
		StudentGlobals.SetReputationTriangle(36, new Vector3(-70, 100, 0));
		StudentGlobals.SetReputationTriangle(37, new Vector3(0, -10, 0));
		StudentGlobals.SetReputationTriangle(38, new Vector3(50, 0, 0));
		StudentGlobals.SetReputationTriangle(39, new Vector3(-50, -10, 0));
		StudentGlobals.SetReputationTriangle(40, new Vector3(70, -30, 10));

		//Art Club
		StudentGlobals.SetReputationTriangle(41, new Vector3(0, 100, 0));
		StudentGlobals.SetReputationTriangle(42, new Vector3(-50, -30, 30));
		StudentGlobals.SetReputationTriangle(43, new Vector3(-10, -10, 0));
		StudentGlobals.SetReputationTriangle(44, new Vector3(-10, 0, 0));
		StudentGlobals.SetReputationTriangle(45, new Vector3(0, -10, 0));

		//Martial Arts Club
		StudentGlobals.SetReputationTriangle(46, new Vector3(100, 100, 100));
		StudentGlobals.SetReputationTriangle(47, new Vector3(10, 30, 10));
		StudentGlobals.SetReputationTriangle(48, new Vector3(30, 10, 10));
		StudentGlobals.SetReputationTriangle(49, new Vector3(30, 30, 10));
		StudentGlobals.SetReputationTriangle(50, new Vector3(30, 10, 10));

		//Light Music Club
		StudentGlobals.SetReputationTriangle(51, new Vector3(10, 100, 0));
		StudentGlobals.SetReputationTriangle(52, new Vector3(30, 70, 0));
		StudentGlobals.SetReputationTriangle(53, new Vector3(50, 10, 0));
		StudentGlobals.SetReputationTriangle(54, new Vector3(50, 50, -10));
		StudentGlobals.SetReputationTriangle(55, new Vector3(30, 30, 0));

		//Photography Club
		StudentGlobals.SetReputationTriangle(56, new Vector3(70, 100, 0));
		StudentGlobals.SetReputationTriangle(57, new Vector3(70, -30, 0));
		StudentGlobals.SetReputationTriangle(58, new Vector3(70, -30, 0));
		StudentGlobals.SetReputationTriangle(59, new Vector3(50, -10, 0));
		StudentGlobals.SetReputationTriangle(60, new Vector3(-10, -50, 0));

		//Science Club
		StudentGlobals.SetReputationTriangle(61, new Vector3(-50, 100, 100));
		StudentGlobals.SetReputationTriangle(62, new Vector3(0, 70, 10));
		StudentGlobals.SetReputationTriangle(63, new Vector3(0, 30, 50));
		StudentGlobals.SetReputationTriangle(64, new Vector3(-10, 30, 50));
		StudentGlobals.SetReputationTriangle(65, new Vector3(-10, 30, 50));

		//Sports Club
		StudentGlobals.SetReputationTriangle(66, new Vector3(-50, 100, 50));
		StudentGlobals.SetReputationTriangle(67, new Vector3(30, 70, 0));
		StudentGlobals.SetReputationTriangle(68, new Vector3(0, 0, 50));
		StudentGlobals.SetReputationTriangle(69, new Vector3(30, 50, 0));
		StudentGlobals.SetReputationTriangle(70, new Vector3(50, 30, 0));

		//Gardening Club
		StudentGlobals.SetReputationTriangle(71, new Vector3(100, 100, -100));
		StudentGlobals.SetReputationTriangle(72, new Vector3(50, 30, 0));
		StudentGlobals.SetReputationTriangle(73, new Vector3(100, 100, -100));
		StudentGlobals.SetReputationTriangle(74, new Vector3(70, 50, -50));
		StudentGlobals.SetReputationTriangle(75, new Vector3(10, 50, 0));

		//Delinquents
		StudentGlobals.SetReputationTriangle(76, new Vector3(-100, -100, 100));
		StudentGlobals.SetReputationTriangle(77, new Vector3(-100, -100, 100));
		StudentGlobals.SetReputationTriangle(78, new Vector3(-100, -100, 100));
		StudentGlobals.SetReputationTriangle(79, new Vector3(-100, -100, 100));
		StudentGlobals.SetReputationTriangle(80, new Vector3(-100, -100, 100));

		//Bullies
		StudentGlobals.SetReputationTriangle(81, new Vector3(50, -10, 50));
		StudentGlobals.SetReputationTriangle(82, new Vector3(50, -10, 50));
		StudentGlobals.SetReputationTriangle(83, new Vector3(50, -10, 50));
		StudentGlobals.SetReputationTriangle(84, new Vector3(50, -10, 50));
		StudentGlobals.SetReputationTriangle(85, new Vector3(50, -10, 50));

		//Student Council
		StudentGlobals.SetReputationTriangle(86, new Vector3(30, 100, 70));
		StudentGlobals.SetReputationTriangle(87, new Vector3(30, -10, 100));
		StudentGlobals.SetReputationTriangle(88, new Vector3(100, 30, 50));
		StudentGlobals.SetReputationTriangle(89, new Vector3(-10, 30, 100));

		//Faculty
		StudentGlobals.SetReputationTriangle(90, new Vector3(10, 100, 10));
		StudentGlobals.SetReputationTriangle(91, new Vector3(0, 50, 100));
		StudentGlobals.SetReputationTriangle(92, new Vector3(0, 70, 50));
		StudentGlobals.SetReputationTriangle(93, new Vector3(0, 100, 50));
		StudentGlobals.SetReputationTriangle(94, new Vector3(0, 70, 100));
		StudentGlobals.SetReputationTriangle(95, new Vector3(0, 50, 70));
		StudentGlobals.SetReputationTriangle(96, new Vector3(0, 100, 50));
		StudentGlobals.SetReputationTriangle(97, new Vector3(50, 100, 30));
		StudentGlobals.SetReputationTriangle(98, new Vector3(0, 100, 100));
		StudentGlobals.SetReputationTriangle(99, new Vector3(-50, 50, 100));
		
		//Info-chan
		StudentGlobals.SetReputationTriangle(99, new Vector3(-100, -100, 100));

		ID = 2;

		while (ID < 101)
		{
			Vector3 Triangle = StudentGlobals.GetReputationTriangle(ID);
			Triangle.x = Triangle.x * .33333f;
			Triangle.y = Triangle.y * .33333f;
			Triangle.z = Triangle.z * .33333f;

			StudentGlobals.SetStudentReputation(ID, Mathf.RoundToInt(Triangle.x + Triangle.y + Triangle.z));
			ID++;
		}
	}

	public void GracePeriod(float Length)
	{
		for (this.ID = 1; this.ID < this.Students.Length; this.ID++)
		{
			StudentScript student = this.Students[this.ID];

			if (student != null)
			{
				student.IgnoreTimer = Length;
			}
		}
	}

	int OpenedDoors = 0;

	public void OpenSomeDoors()
	{
		int startID = OpenedDoors;

		while (OpenedDoors < startID + 11)
		{
			if (OpenedDoors < Doors.Length)
			{
				if (Doors[OpenedDoors] != null && Doors[OpenedDoors].enabled)
				{
					Doors[OpenedDoors].Open = true;
					Doors[OpenedDoors].OpenDoor();
				}
			}

			OpenedDoors++;
		}
	}
		
	int SnappedStudents = 1;

	public void SnapSomeStudents()
	{
		int startID = SnappedStudents;

		while (SnappedStudents < startID + 10)
		{
			if (SnappedStudents < Students.Length)
			{
				StudentScript student = Students[SnappedStudents];

				if (student != null)
				{
					if (student.gameObject.activeInHierarchy && student.Alive)
					{
						student.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
						student.CharacterAnimation[student.SocialSitAnim].weight = 0.0f;
						student.SnapStudent.Yandere = SnappedYandere;
						student.SnapStudent.enabled = true;
						student.SpeechLines.Stop();
						student.enabled = false;
						student.EmptyHands();

						if (student.Shy)
						{
							student.CharacterAnimation[student.ShyAnim].weight = 0.0f;
						}

						if (student.Club == ClubType.LightMusic)
						{
							student.StopMusic();
						}
					}
				}
			}

			SnappedStudents++;
		}
	}

	public Texture PureWhite;

	public void DarkenAllStudents()
	{
		foreach (StudentScript student in Students)
		{
			if (student != null)
			{
				if (student.StudentID > 1)
				{
					student.MyRenderer.materials[0].mainTexture = this.PureWhite;
					student.MyRenderer.materials[1].mainTexture = this.PureWhite;
					student.MyRenderer.materials[2].mainTexture = this.PureWhite;

					student.MyRenderer.materials[0].color = new Color(1, 1, 1, 1);
					student.MyRenderer.materials[1].color = new Color(1, 1, 1, 1);
					student.MyRenderer.materials[2].color = new Color(1, 1, 1, 1);

					student.Cosmetic.LeftEyeRenderer.material.mainTexture = this.PureWhite;
					student.Cosmetic.RightEyeRenderer.material.mainTexture = this.PureWhite;
					student.Cosmetic.HairRenderer.material.mainTexture = this.PureWhite;

					student.Cosmetic.LeftEyeRenderer.material.color = new Color(1, 1, 1, 1);
					student.Cosmetic.RightEyeRenderer.material.color = new Color(1, 1, 1, 1);
					student.Cosmetic.HairRenderer.material.color = new Color(1, 1, 1, 1);
				}
			}
		}
	}

	public Transform[] BullySnapPosition;

	public void LockDownOccultClub()
	{
		int OccultClubID = 31;

		while (OccultClubID < 36)
		{
			Patrols.List[OccultClubID].GetChild(1).position = Patrols.List[OccultClubID].GetChild(0).position;
			Patrols.List[OccultClubID].GetChild(2).position = Patrols.List[OccultClubID].GetChild(0).position;
			Patrols.List[OccultClubID].GetChild(3).position = Patrols.List[OccultClubID].GetChild(0).position;
			Patrols.List[OccultClubID].GetChild(4).position = Patrols.List[OccultClubID].GetChild(0).position;
			Patrols.List[OccultClubID].GetChild(5).position = Patrols.List[OccultClubID].GetChild(0).position;

			OccultClubID++;
		}

		int BullyID = 81;

		while (BullyID < 86)
		{
			Patrols.List[BullyID].GetChild(0).position = BullySnapPosition[BullyID].position;
			Patrols.List[BullyID].GetChild(1).position = BullySnapPosition[BullyID].position;
			Patrols.List[BullyID].GetChild(2).position = BullySnapPosition[BullyID].position;
			Patrols.List[BullyID].GetChild(3).position = BullySnapPosition[BullyID].position;

			BullyID++;
		}
	}

    public OcclusionPortal WindowOccluder;
    public bool OpaqueWindows;
    public Renderer Window;

    public void SetWindowsOpaque()
    {
        WindowOccluder.open = !WindowOccluder.open;

        if (WindowOccluder.open)
        {
            Window.sharedMaterial.color = new Color(1, 1, 1, .5f);
            Window.sharedMaterial.shader = Shader.Find("Transparent/Diffuse");
        }
        else
        {
            Window.sharedMaterial.color = new Color(1, 1, 1, 1);
            Window.sharedMaterial.shader = Shader.Find("Diffuse");
        }
    }

    public void LateUpdate()
    {
        if (this.OpaqueWindows)
        {
            if (this.Yandere.transform.position.y > .1f && this.Yandere.transform.position.y < 11)
            {
                if (this.WindowOccluder.open)
                {
                   this.SetWindowsOpaque();
                }
            }
            else
            {
                if (!this.WindowOccluder.open)
                {
                    this.SetWindowsOpaque();
                }
            }
        }
    }

    public void UpdateSkirts(bool Status)
    {
        foreach (StudentScript student in Students)
        {
            if (student != null)
            {
                if (!student.Male && !student.Teacher && student.Schoolwear == 1)
                {
                    student.SkirtCollider.gameObject.SetActive(Status);
                }

                student.RightHandCollider.enabled = Status;
                student.LeftHandCollider.enabled = Status;
            }
        }
    }

    public void UpdatePanties(bool Status)
    {
        foreach (StudentScript student in Students)
        {
            if (student != null)
            {
                if (!student.Male && !student.Teacher)
                {
                    if (student.Schoolwear == 1)
                    {
                        student.PantyCollider.gameObject.SetActive(Status);
                    }
                }

                student.NotFaceCollider.enabled = Status;
                student.FaceCollider.enabled = Status;
            }
        }
    }
}