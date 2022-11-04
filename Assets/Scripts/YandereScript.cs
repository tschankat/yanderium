using HighlightingSystem;
using System.Collections;
using UnityEngine;
using Pathfinding;

using XInputDotNetPure;

public enum SanityType
{
	High,
	Medium,
	Low
}

public enum YandereInteractionType
{
	Idle = 0,
	Apologizing = 1,
	Compliment = 2,
	Gossip = 3,
	Bye = 4,
	FollowMe = 6,
	GoAway = 7,
	DistractThem = 8,
	ClubInfo = 10,
	ClubJoin = 11,
	ClubQuit = 12,
	ClubBye = 13,
	ClubActivity = 14,
	NamingCrush = 17,
	ChangingAppearance = 18,
	Court = 19,
	Confess = 20,
	Feed = 21,
	TaskInquiry = 22,
	GivingSnack = 23,
	AskingForHelp = 24,
	SendingToLocker = 25
}

public enum MovementType
{
	Idle,
	Walking,
	Running
}

public enum PKDirType
{
	None,
	Up,
	Down,
	Right,
	Left
}

public enum YanderePersonaType
{
	Default = 0,
	Chill = 1,
	Confident = 2,
	Elegant = 3,
	Girly = 4,
	Graceful = 5,
	Haughty = 6,
	Lively = 7,
	Scholarly = 8,
	Shy = 9,
	Tough = 10,
	Aggressive = 11,
	Grunt = 12
}

public class YandereScript : MonoBehaviour
{
	public Quaternion targetRotation;
	private Vector3 targetDirection;
	private GameObject NewTrail;
	public int AccessoryID = 0;
	private int ID = 0;

	public FootprintSpawnerScript RightFootprintSpawner;
	public FootprintSpawnerScript LeftFootprintSpawner;

	public ColorCorrectionCurves YandereColorCorrection;
	public ColorCorrectionCurves ColorCorrection;
	public SelectiveGrayscale SelectGrayscale;
	public HighlightingRenderer HighlightingR;
	public HighlightingBlitter HighlightingB;
	public AmbientObscurance Obscurance;
	public DepthOfField34 DepthOfField;
	public Vignetting Vignette;
	public Blur Blur;

	public NotificationManagerScript NotificationManager;
	public ObstacleDetectorScript ObstacleDetector;
	public RiggedAccessoryAttacher GloveAttacher;
	public RiggedAccessoryAttacher PantyAttacher;
	public AccessoryGroupScript AccessoryGroup;
	public DumpsterHandleScript DumpsterHandle;
	public PhonePromptBarScript PhonePromptBar;
	public ShoulderCameraScript ShoulderCamera;
	public StudentManagerScript StudentManager;
	public AttackManagerScript AttackManager;
	public CameraEffectsScript CameraEffects;
	public WeaponManagerScript WeaponManager;
	public YandereShowerScript YandereShower;
	public PromptParentScript PromptParent;
	public SplashCameraScript SplashCamera;
	public SWP_HeartRateMonitor HeartRate;
	public GenericBentoScript TargetBento;
	public LoveManagerScript LoveManager;
	public StruggleBarScript StruggleBar;
	public RummageSpotScript RummageSpot;
	public IncineratorScript Incinerator;
	public InputDeviceScript InputDevice;
	public MusicCreditScript MusicCredit;
	public PauseScreenScript PauseScreen;
	public SmartphoneScript PhoneToCrush;
	public WoodChipperScript WoodChipper;
	public RagdollScript CurrentRagdoll;
	public StudentScript TargetStudent;
	public WeaponMenuScript WeaponMenu;
	public PromptScript NearestPrompt;
	public ContainerScript Container;
	public InventoryScript Inventory;
	public TallLockerScript MyLocker;
	public PromptBarScript PromptBar;
	public TranqCaseScript TranqCase;
	public LocationScript Location;
	public SubtitleScript Subtitle;
	public UITexture SanitySmudges;
	public StudentScript Follower;
	public DemonScript EmptyDemon;
	public UIPanel DetectionPanel;
	public JukeboxScript Jukebox;
	public OutlineScript Outline;
	public StudentScript Pursuer;
	public ShutterScript Shutter;
	public Collider HipCollider;
	public UISprite ProgressBar;
	public RPG_Camera RPGCamera;
	public BucketScript Bucket;
	public LookAtTarget LookAt;
	public PickUpScript PickUp;
	public PoliceScript Police;
	public UILabel SanityLabel;
	public GloveScript Gloves;
    public ClassScript Class;
    public Camera UICamera;
	public UILabel PowerUp;
	public MaskScript Mask;
	public MopScript Mop;
	public UIPanel HUD;

	public CharacterController MyController;

	public Transform LeftItemParent;
	public Transform DismemberSpot;
	public Transform CameraTarget;
	public Transform InvertSphere;
	public Transform RightArmRoll;
	public Transform LeftArmRoll;
	public Transform CameraFocus;
	public Transform RightBreast;
	public Transform HidingSpot;
	public Transform ItemParent;
	public Transform LeftBreast;
	public Transform LimbParent;
	public Transform PelvisRoot;
	public Transform PoisonSpot;
	public Transform CameraPOV;
	public Transform RightHand;
	public Transform RightKnee;
	public Transform RightFoot;
	public Transform ExitSpot;
	public Transform LeftHand;
	public Transform Backpack;
	public Transform DropSpot;
	public Transform Homeroom;
	public Transform DigSpot;
	public Transform Senpai;
	public Transform Target;
	public Transform Stool;
	public Transform Eyes;
	public Transform Head;
	public Transform Hips;

	public AudioSource HeartBeat;
	public AudioSource MyAudio;

	public GameObject[] Accessories;
	public GameObject[] Hairstyles;
	public GameObject[] Poisons;
	public GameObject[] Shoes;

	public float[] DropTimer;

	public GameObject CinematicCamera;
	public GameObject FloatingShovel;
	public GameObject EasterEggMenu;
	public GameObject StolenObject;
	public GameObject SelfieGuide;
	public GameObject MemeGlasses;
	public GameObject GiggleDisc;
	public GameObject KONGlasses;
	public GameObject Microphone;
	public GameObject SpiderLegs;
	public GameObject AlarmDisc;
	public GameObject Character;
	public GameObject DebugMenu;
	public GameObject EyepatchL;
	public GameObject EyepatchR;
	public GameObject EmptyHusk;
	public GameObject Handcuffs;
	public GameObject ShoePair;
	public GameObject Barcode;
	public GameObject Headset;
	public GameObject Ragdoll;
	public GameObject Hearts;
	public GameObject Teeth;
	public GameObject Phone;
	public GameObject Trail;
	public GameObject Match;
	public GameObject Arc;

	public SkinnedMeshRenderer MyRenderer;
	public Animation CharacterAnimation;
	public ParticleSystem GiggleLines;
	public ParticleSystem InsaneLines;
	public SpringJoint RagdollDragger;
	public SpringJoint RagdollPK;
	public Projector MyProjector;
	public Camera HeartCamera;
	public Camera MainCamera;
	public Camera Smartphone;
	public Camera HandCamera;

	public Renderer SmartphoneRenderer;
	public Renderer LongHairRenderer;
	public Renderer PonytailRenderer;
	public Renderer PigtailR;
	public Renderer PigtailL;
	public Renderer Drills;

	public float MurderousActionTimer = 0.0f;
	public float CinematicTimer = 0.0f;
	public float ClothingTimer = 0.0f;
	public float BreakUpTimer = 0.0f;
	public float CanMoveTimer = 0.0f;
	public float RummageTimer = 0.0f;
	public float YandereTimer = 0.0f;
	public float AttackTimer = 0.0f;
	public float CaughtTimer = 0.0f;
	public float GiggleTimer = 0.0f;
	public float SenpaiTimer = 0.0f;
	public float WeaponTimer = 0.0f;
	public float CrawlTimer = 0.0f;
	public float GloveTimer = 0.0f;
	public float LaughTimer = 0.0f;
	public float SprayTimer = 0.0f;
	public float TheftTimer = 0.0f;
	public float BeatTimer = 0.0f;
	public float BoneTimer = 0.0f;
	public float DumpTimer = 0.0f;
	public float ExitTimer = 0.0f;
	public float TalkTimer = 0.0f;

	[SerializeField] float bloodiness;

	public float PreviousSanity = 100.0f;
	[SerializeField] float sanity;

	public float TwitchTimer = 0.0f;
	public float NextTwitch = 0.0f;

	public float LaughIntensity = 0.0f;
	public float TimeSkipHeight = 0.0f;
    public float PourDistance = 0.0f;
	public float TargetHeight = 0.0f;
    public float PermitLaugh = 0.0f;
    public float ReachWeight = 0.0f;
	public float BreastSize = 0.0f;
	public float Numbness = 0.0f;
	public float PourTime = 0.0f;
	public float RunSpeed = 0.0f;
	public float Height = 0.0f;
	public float Slouch = 0.0f;
	public float Bend = 0.0f;

	public float CrouchWalkSpeed = 0.0f;
	public float CrouchRunSpeed = 0.0f;
	public float ShoveSpeed = 2.0f;
	public float CrawlSpeed = 0.0f;
	public float FlapSpeed = 0.0f;
	public float WalkSpeed = 0.0f;

	public float YandereFade = 0.0f;
	public float YandereTint = 0.0f;
	public float SenpaiFade = 0.0f;
	public float SenpaiTint = 0.0f;
	public float GreyTarget = 0.0f;

	public int CurrentUniformOrigin = 1;
	public int PreviousSchoolwear = 0;
	public int NearestCorpseID = 0;
	public int StrugglePhase = 0;
	public int PhysicalGrade = 0;
	public int CarryAnimID = 0;
	public int AttackPhase = 0;
	public int Creepiness = 1;
	public int GloveBlood = 0;
	public int NearBodies = 0;
	public int PoisonType = 0;
	public int Schoolwear = 0;
	public int SpeedBonus = 0;
	public int SprayPhase = 0;
	public int DragState = 0;
	public int EggBypass = 0;
	public int EyewearID = 0;
	public int Followers = 0;
	public int Hairstyle = 0;
    public int PersonaID = 0;
    public int DigPhase = 0;
	public int Equipped = 0;
	public int Chasers = 0;
	public int Costume = 0;
	public int Alerts = 0;
	public int Health = 5;

	public YandereInteractionType Interaction = YandereInteractionType.Idle;
	public YanderePersonaType Persona = YanderePersonaType.Default;
    public ClubType Club;

	public bool EavesdropWarning = false;
	public bool ClothingWarning = false;
	public bool BloodyWarning = false;
	public bool CorpseWarning = false;
	public bool SanityWarning = false;
	public bool WeaponWarning = false;

	public bool DelinquentFighting = false;
	public bool DumpsterGrabbing = false;
	public bool BucketDropping = false;
	public bool CleaningWeapon = false;
	public bool SubtleStabbing = false;
	public bool TranquilHiding = false;
	public bool CrushingPhone = false;
	public bool Eavesdropping = false;
	public bool Pickpocketing = false;
	public bool Dismembering = false;
	public bool ShootingBeam = false;
	public bool StoppingTime = false;
	public bool TimeSkipping = false;
	public bool Cauterizing = false;
	public bool HeavyWeight = false;
	public bool Trespassing = false;
	public bool WritingName = false;
	public bool Struggling = false;
	public bool Attacking = false;
	public bool Degloving = false;
	public bool Poisoning = false;
	public bool Rummaging = false;
	public bool Stripping = false;
	public bool Blasting = false;
	public bool Carrying = false;
	public bool Chipping = false;
	public bool Dragging = false;
	public bool Dropping = false;
	public bool Flicking = false;
	public bool Freezing = false;
	public bool Laughing = false;
	public bool Punching = false;
	public bool Throwing = false;
	public bool Tripping = false;
	public bool Bathing = false;
	public bool Burying = false;
	public bool Cooking = false;
	public bool Digging = false;
	public bool Dipping = false;
	public bool Dumping = false;
	public bool Exiting = false;
	public bool Lifting = false;
	public bool Mopping = false;
	public bool Pouring = false;
	public bool Resting = false;
	public bool Running = false;
	public bool Talking = false;
	public bool Testing = false;
	public bool Aiming = false;
	public bool Eating = false;
	public bool Hiding = false;
	public bool Riding = false;

	public Stance Stance = new Stance(StanceType.Standing);

	public bool PreparedForStruggle = false;
	public bool CrouchButtonDown = false;
	public bool FightHasBrokenUp = false;
	public bool UsingController = false;
	public bool SeenByAuthority = false;
	public bool CameFromCrouch = false; // @todo: Replace this with YandereScript.Stance.Previous.
	public bool CannotRecover = false;
	public bool NoStainGloves = false;
	public bool YandereVision = false;
	public bool ClubActivity = false;
	public bool FlameDemonic = false;
	public bool SanityBased = false;
	public bool SummonBones = false;
	public bool ClubAttire = false;
	public bool FollowHips = false;
	public bool NearSenpai = false;
	public bool RivalPhone = false;
	public bool SpiderGrow = false;
	public bool Possessed = false;
	public bool ToggleRun = false;
	public bool WitchMode = false;
	public bool Attacked = false;
	public bool CanTranq = false;
	public bool Collapse = false;
	public bool Unmasked = false;
	public bool RedPaint = false;
	public bool RoofPush = false;
	public bool Demonic = false;
	public bool FlapOut = false;
	public bool NoDebug = false;
	public bool Noticed = false;
	public bool InClass = false;
	public bool Slender = false;
	public bool Sprayed = false;
	public bool Caught = false;
	public bool CanMove = true;
	public bool Chased = false;
	public bool Gloved = false;
	public bool Selfie = false;
	public bool Shoved = false;
	public bool Drown = false;
	public bool Xtan = false;
	public bool Lewd = false;
	public bool Lost = false;
	public bool Sans = false;
	public bool Egg = false;
	public bool Won = false;
	public bool AR = false;
	public bool DK = false;
	public bool PK = false;

	public Texture[] UniformTextures;
	public Texture[] CasualTextures;
	public Texture[] FlowerTextures;
	public Texture[] BloodTextures;

	public AudioClip[] CreepyGiggles;
	public AudioClip[] Stabs;

	public WeaponScript[] Weapon;
	public GameObject[] ZipTie;

	public string[] ArmedAnims;
	public string[] CarryAnims;

	public Transform[] Spine;
	public Transform[] Foot;
	public Transform[] Hand;
	public Transform[] Arm;
	public Transform[] Leg;

	public Mesh[] Uniforms;

	public Renderer RightYandereEye;
	public Renderer LeftYandereEye;
	public Vector3 RightEyeOrigin;
	public Vector3 LeftEyeOrigin;
	public Renderer RightRedEye;
	public Renderer LeftRedEye;
	public Transform RightEye;
	public Transform LeftEye;
	public float EyeShrink = 0.0f;

	public Vector3 Twitch;

	private AudioClip LaughClip;
	public string PourHeight = string.Empty;
	public string DrownAnim = string.Empty;
	public string LaughAnim = string.Empty;
	public string HideAnim = string.Empty;
	public string IdleAnim = string.Empty;
	public string TalkAnim = string.Empty;
	public string WalkAnim = string.Empty;
	public string RunAnim = string.Empty;

	public string CrouchIdleAnim = string.Empty;
	public string CrouchWalkAnim = string.Empty;
	public string CrouchRunAnim = string.Empty;

	public string CrawlIdleAnim = string.Empty;
	public string CrawlWalkAnim = string.Empty;

	public string HeavyIdleAnim = string.Empty;
	public string HeavyWalkAnim = string.Empty;

	public string CarryIdleAnim = string.Empty;
	public string CarryWalkAnim = string.Empty;
	public string CarryRunAnim = string.Empty;

	public string[] CreepyIdles;
	public string[] CreepyWalks;

	public AudioClip DramaticWriting;
	public AudioClip ChargeUp;
	public AudioClip Laugh0;
	public AudioClip Laugh1;
	public AudioClip Laugh2;
	public AudioClip Laugh3;
	public AudioClip Laugh4;
	public AudioClip Thud;
	public AudioClip Dig;

	public Vector3 PreviousPosition;

	public string OriginalIdleAnim = string.Empty;
	public string OriginalWalkAnim = string.Empty;
	public string OriginalRunAnim = string.Empty;

	public Texture YanderePhoneTexture;
	public Texture BloodyGloveTexture;
	public Texture RivalPhoneTexture;
	public Texture BlondePony;

	public float VibrationIntensity = 0.0f;
	public float VibrationTimer = 0.0f;
	public bool VibrationCheck = false;

	void Start()
	{
        Application.runInBackground = false;

        this.PhysicalGrade = ClassGlobals.PhysicalGrade;
		this.SpeedBonus = PlayerGlobals.SpeedBonus;
        this.Club = ClubGlobals.Club;

        //Debug.Log("ClubGlobals.Club is: " + ClubGlobals.Club);

		this.SanitySmudges.color = new Color(1, 1, 1, 0);

		this.SpiderLegs.SetActive(GameGlobals.EmptyDemon);

		this.MyRenderer.materials[2].SetFloat("_BlendAmount1", 0);

		this.SetAnimationLayers();

		this.UpdateNumbness();

		this.RightEyeOrigin = this.RightEye.localPosition;
		this.LeftEyeOrigin = this.LeftEye.localPosition;

		this.CharacterAnimation[AnimNames.FemaleYanderePose].weight = 0.0f;
		this.CharacterAnimation[AnimNames.FemaleCameraPose].weight = 0.0f;
		this.CharacterAnimation["f02_selfie_00"].weight = 0.0f;

		this.CharacterAnimation["f02_shipGirlSnap_00"].speed = 2;
		this.CharacterAnimation["f02_gazerSnap_00"].speed = 2;

		this.CharacterAnimation["f02_performing_00"].speed = .9f;

		this.CharacterAnimation["f02_sithAttack_00"].speed = 1.5f;
		this.CharacterAnimation["f02_sithAttack_01"].speed = 1.5f;
		this.CharacterAnimation["f02_sithAttack_02"].speed = 1.5f;

		this.CharacterAnimation["f02_sithAttackHard_00"].speed = 1.5f;
		this.CharacterAnimation["f02_sithAttackHard_01"].speed = 1.5f;
		this.CharacterAnimation["f02_sithAttackHard_02"].speed = 1.5f;

		this.CharacterAnimation["f02_nierRun_00"].speed = 1.5f;

		ColorCorrectionCurves[] ColorCorrections = Camera.main.GetComponents<ColorCorrectionCurves>();
		Vignetting[] Vignettes = Camera.main.GetComponents<Vignetting>();

		this.YandereColorCorrection = ColorCorrections[1];
		this.Vignette = Vignettes[1];

		this.ResetYandereEffects();
		this.ResetSenpaiEffects();

		// [af] Initialize sanity and bloodiness accessors to defaults.
		this.Sanity = 100.0f;
		this.Bloodiness = 0.0f;

		this.SetUniform();

		this.EasterEggMenu.transform.localPosition = new Vector3(
			this.EasterEggMenu.transform.localPosition.x,
			0.0f,
			this.EasterEggMenu.transform.localPosition.z);

		// [af] Added "gameObject" for C# compatibility.
		this.ProgressBar.transform.parent.gameObject.SetActive(false);
		this.Smartphone.transform.parent.gameObject.SetActive(false);

		this.ObstacleDetector.gameObject.SetActive(false);
		this.SithBeam[1].gameObject.SetActive(false);
		this.SithBeam[2].gameObject.SetActive(false);
		this.PunishedAccessories.SetActive(false);
		this.SukebanAccessories.SetActive(false);
		this.FalconShoulderpad.SetActive(false);
		this.CensorSteam[0].SetActive(false);
		this.CensorSteam[1].SetActive(false);
		this.CensorSteam[2].SetActive(false);
		this.CensorSteam[3].SetActive(false);
		this.FloatingShovel.SetActive(false);
		this.BlackEyePatch.SetActive(false);
		this.EasterEggMenu.SetActive(false);
		this.FalconKneepad1.SetActive(false);
		this.FalconKneepad2.SetActive(false);
		this.PunishedScarf.SetActive(false);
		this.FalconBuckle.SetActive(false);
		this.FalconHelmet.SetActive(false);
		this.TornadoDress.SetActive(false);
		this.Stand.Stand.SetActive(false);
		this.TornadoHair.SetActive(false);
		this.MemeGlasses.SetActive(false);
		this.CirnoWings.SetActive(false);
		this.KONGlasses.SetActive(false);
		this.EbolaWings.SetActive(false);
		this.Microphone.SetActive(false);
		this.Poisons[1].SetActive(false);
		this.Poisons[2].SetActive(false);
		this.Poisons[3].SetActive(false);
		this.BladeHair.SetActive(false);
		this.CirnoHair.SetActive(false);
		this.EbolaHair.SetActive(false);
		//this.FalconGun.SetActive(false);
		this.EyepatchL.SetActive(false);
		this.EyepatchR.SetActive(false);
		this.Handcuffs.SetActive(false);
		this.ZipTie[0].SetActive(false);
		this.ZipTie[1].SetActive(false);
		this.Shoes[0].SetActive(false);
		this.Shoes[1].SetActive(false);
		this.Phone.SetActive(false);
		this.Cape.SetActive(false);

		this.HeavySwordParent.gameObject.SetActive(false);
		this.LightSwordParent.gameObject.SetActive(false);
		this.Pod.SetActive(false);

		this.OriginalIdleAnim = this.IdleAnim;
		this.OriginalWalkAnim = this.WalkAnim;
		this.OriginalRunAnim = this.RunAnim;

		foreach (GameObject armor in this.Armor)
		{
			armor.SetActive(false);
		}

		for (this.ID = 1; this.ID < this.Accessories.Length; this.ID++)
		{
			this.Accessories[this.ID].SetActive(false);
		}

		foreach (GameObject arm in this.PunishedArm)
		{
			arm.SetActive(false);
		}

		foreach (GameObject accessory in this.GaloAccessories)
		{
			accessory.SetActive(false);
		}

		foreach (GameObject vector in this.Vectors)
		{
			vector.SetActive(false);
		}

		for (this.ID = 1; this.ID < this.CyborgParts.Length; this.ID++)
		{
			this.CyborgParts[this.ID].SetActive(false);
		}

		for (this.ID = 0; this.ID < this.KLKParts.Length; this.ID++)
		{
			this.KLKParts[this.ID].SetActive(false);
		}

		for (this.ID = 0; this.ID < this.BanchoAccessories.Length; this.ID++)
		{
			this.BanchoAccessories[this.ID].SetActive(false);
		}

		if (PlayerGlobals.PantiesEquipped == 5)
		{
			this.RunSpeed++;
		}

		if (PlayerGlobals.Headset)
		{
			this.Inventory.Headset = true;
		}

		this.UpdateHair();
		this.ClubAccessory();

		if (MissionModeGlobals.MissionMode || GameGlobals.LoveSick)
		{
			this.NoDebug = true;
		}

		if (GameGlobals.BlondeHair)
		{
			this.PonytailRenderer.material.mainTexture = this.BlondePony;
		}

#if UNITY_EDITOR
		this.NoDebug = false;
#endif

		#if !UNITY_EDITOR

		if (this.StudentManager.Students[11] != null)
		{
			Destroy(this.gameObject);
		}

		#endif

		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 1.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 1.0f);

        //Must be the final line of code in the Start() function
        this.CharacterAnimation.Sample();
    }
	
	public float Sanity
	{
		get
		{
			return this.sanity;
		}
		set
		{
			this.sanity = Mathf.Clamp(value, 0.0f, 100.0f);

			if (this.sanity > 66.66666f)
			{
				this.HeartRate.SetHeartRateColour(this.HeartRate.NormalColour);
				this.SanityWarning = false;
			}
			else if (this.sanity > 33.33333f)
			{
				this.HeartRate.SetHeartRateColour(this.HeartRate.MediumColour);
				this.SanityWarning = false;

				if (this.PreviousSanity < 33.33333f)
				{
					this.StudentManager.UpdateStudents();
				}
			}
			else
			{
				this.HeartRate.SetHeartRateColour(this.HeartRate.BadColour);

				if (!this.SanityWarning)
				{
					this.NotificationManager.DisplayNotification(NotificationType.Insane);

					this.StudentManager.TutorialWindow.ShowSanityMessage = true;
					this.SanityWarning = true;
				}
			}

			this.HeartRate.BeatsPerMinute = (int)(240.0f - (this.sanity * 1.80f));

			if (!this.Laughing)
			{
				this.Teeth.SetActive(this.SanityWarning);
			}

			if (this.MyRenderer.sharedMesh != this.NudeMesh)
			{
				if (!Slender)
				{
					this.MyRenderer.materials[2].SetFloat("_BlendAmount", 1.0f - (this.sanity / 100.0f));
				}
				else
				{
					this.MyRenderer.materials[2].SetFloat("_BlendAmount", 0.0f);
				}
			}
			else
			{
				this.MyRenderer.materials[2].SetFloat("_BlendAmount", 0.0f);
			}

			this.PreviousSanity = this.sanity;

			this.Hairstyles[2].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, this.Sanity);
		}
	}

	public float Bloodiness
	{
		get
		{
			return this.bloodiness;
		}
		set
		{
			this.bloodiness = Mathf.Clamp(value, 0.0f, 100.0f);

			if (Bloodiness > 0)
			{
				this.StudentManager.TutorialWindow.ShowBloodMessage = true;
			}

			if (!this.BloodyWarning && (this.Bloodiness > 0.0f))
			{
				this.NotificationManager.DisplayNotification(NotificationType.Bloody);

				this.BloodyWarning = true;

				if (this.Schoolwear > 0 && this.ClubAttire)
				{
					this.Police.BloodyClothing++;

					if (this.CurrentUniformOrigin == 1)
					{
						this.StudentManager.OriginalUniforms--;

						Debug.Log("One of the original uniforms has become bloody. There are now " + this.StudentManager.OriginalUniforms + " clean original uniforms in the school.");
					}
					else
					{
						this.StudentManager.NewUniforms--;

						Debug.Log("One of the new uniforms has become bloody. There are now " + this.StudentManager.NewUniforms + " clean original uniforms in the school.");
					}
				}
			}

			this.MyProjector.enabled = true;
			this.RedPaint = false;

			if (!GameGlobals.CensorBlood)
			{
				this.MyProjector.material.SetColor("_TintColor", new Color(.25f, .25f, .25f, .5f));

				if (this.Bloodiness == 100.0f){this.MyProjector.material.mainTexture = this.BloodTextures[5];}
				else if (this.Bloodiness >= 80.0f){this.MyProjector.material.mainTexture = this.BloodTextures[4];}
				else if (this.Bloodiness >= 60.0f){this.MyProjector.material.mainTexture = this.BloodTextures[3];}
				else if (this.Bloodiness >= 40.0f){this.MyProjector.material.mainTexture = this.BloodTextures[2];}
				else if (this.Bloodiness >= 20.0f){this.MyProjector.material.mainTexture = this.BloodTextures[1];}
				else
				{
					this.MyProjector.enabled = false;
					this.BloodyWarning = false;
				}
			}
			else
			{
				this.MyProjector.material.SetColor("_TintColor", new Color(.5f, .5f, .5f, .5f));

				if (this.Bloodiness == 100.0f){this.MyProjector.material.mainTexture = this.FlowerTextures[5];}
				else if (this.Bloodiness >= 80.0f){this.MyProjector.material.mainTexture = this.FlowerTextures[4];}
				else if (this.Bloodiness >= 60.0f){this.MyProjector.material.mainTexture = this.FlowerTextures[3];}
				else if (this.Bloodiness >= 40.0f){this.MyProjector.material.mainTexture = this.FlowerTextures[2];}
				else if (this.Bloodiness >= 20.0f){this.MyProjector.material.mainTexture = this.FlowerTextures[1];}
				else
				{
					this.MyProjector.enabled = false;
					this.BloodyWarning = false;
				}
			}

			this.StudentManager.UpdateBooths();
			this.MyLocker.UpdateButtons();

			this.Outline.h.ReinitMaterials();
		}
	}

	public WeaponScript EquippedWeapon
	{
		get { return this.Weapon[this.Equipped]; }
		set { this.Weapon[this.Equipped] = value; }
	}

	public bool Armed
	{
		get { return this.EquippedWeapon != null; }
	}

	public SanityType SanityType
	{
		get
		{
			if ((this.Sanity / 100.0f) > (2.0f / 3.0f))
			{
				return SanityType.High;
			}
			else if ((this.Sanity / 100.0f) > (1.0f / 3.0f))
			{
				return SanityType.Medium;
			}
			else
			{
				return SanityType.Low;
			}
		}
	}

	// [af] This function is necessary for some sanity animations.
	public string GetSanityString(SanityType sanityType)
	{
		if (sanityType == SanityType.High)
		{
			return "High";
		}
		else if (sanityType == SanityType.Medium)
		{
			return "Med";
		}
		else
		{
			return "Low";
		}
	}

	// [af] Used with student vision. Yandere-chan's head isn't used specifically because
	// her head might go outside her cylinder collider (i.e., when crouching or crawling), 
	// and the ray cast checks would fail then.
	public Vector3 HeadPosition
	{
		get
		{
			return new Vector3(
				this.transform.position.x,
				this.Hips.position.y + .2f,
				this.transform.position.z);
		}
	}

	public void SetAnimationLayers()
	{
		this.CharacterAnimation[AnimNames.FemaleYanderePose].layer = 1;
		this.CharacterAnimation.Play(AnimNames.FemaleYanderePose);
		this.CharacterAnimation[AnimNames.FemaleYanderePose].weight = 0.0f;

		this.CharacterAnimation[AnimNames.FemaleShy].layer = 2;
		this.CharacterAnimation.Play(AnimNames.FemaleShy);
		this.CharacterAnimation[AnimNames.FemaleShy].weight = 0.0f;

		this.CharacterAnimation[AnimNames.FemaleSingleSaw].layer = 3;
		this.CharacterAnimation.Play(AnimNames.FemaleSingleSaw);
		this.CharacterAnimation[AnimNames.FemaleSingleSaw].weight = 0.0f;

		this.CharacterAnimation[AnimNames.FemaleFist].layer = 4;
		this.CharacterAnimation.Play(AnimNames.FemaleFist);
		this.CharacterAnimation[AnimNames.FemaleFist].weight = 0.0f;

		this.CharacterAnimation[AnimNames.FemaleMopping].layer = 5;
		this.CharacterAnimation[AnimNames.FemaleMopping].speed = 2.0f;
		this.CharacterAnimation.Play(AnimNames.FemaleMopping);
		this.CharacterAnimation[AnimNames.FemaleMopping].weight = 0.0f;

		this.CharacterAnimation[AnimNames.FemaleCarry].layer = 6;
		this.CharacterAnimation.Play(AnimNames.FemaleCarry);
		this.CharacterAnimation[AnimNames.FemaleCarry].weight = 0.0f;

		this.CharacterAnimation[AnimNames.FemaleMopCarry].layer = 7;
		this.CharacterAnimation.Play(AnimNames.FemaleMopCarry);
		this.CharacterAnimation[AnimNames.FemaleMopCarry].weight = 0.0f;

		this.CharacterAnimation[AnimNames.FemaleBucketCarry].layer = 8;
		this.CharacterAnimation.Play(AnimNames.FemaleBucketCarry);
		this.CharacterAnimation[AnimNames.FemaleBucketCarry].weight = 0.0f;

		this.CharacterAnimation[AnimNames.FemaleCameraPose].layer = 9;
		this.CharacterAnimation.Play(AnimNames.FemaleCameraPose);
		this.CharacterAnimation[AnimNames.FemaleCameraPose].weight = 0.0f;

		this.CharacterAnimation[AnimNames.FemaleGrip].layer = 10;
		this.CharacterAnimation.Play(AnimNames.FemaleGrip);
		this.CharacterAnimation[AnimNames.FemaleGrip].weight = 0.0f;

		this.CharacterAnimation[AnimNames.FemaleHoldHead].layer = 11;
		this.CharacterAnimation.Play(AnimNames.FemaleHoldHead);
		this.CharacterAnimation[AnimNames.FemaleHoldHead].weight = 0.0f;

		this.CharacterAnimation[AnimNames.FemaleHoldTorso].layer = 12;
		this.CharacterAnimation.Play(AnimNames.FemaleHoldTorso);
		this.CharacterAnimation[AnimNames.FemaleHoldTorso].weight = 0.0f;

		this.CharacterAnimation[AnimNames.FemaleCarryCan].layer = 13;
		this.CharacterAnimation.Play(AnimNames.FemaleCarryCan);
		this.CharacterAnimation[AnimNames.FemaleCarryCan].weight = 0.0f;

		this.CharacterAnimation[AnimNames.FemaleLeftGrip].layer = 14;
		this.CharacterAnimation.Play(AnimNames.FemaleLeftGrip);
		this.CharacterAnimation[AnimNames.FemaleLeftGrip].weight = 0.0f;

		this.CharacterAnimation[AnimNames.FemaleCarryShoulder].layer = 15;
		this.CharacterAnimation.Play(AnimNames.FemaleCarryShoulder);
		this.CharacterAnimation[AnimNames.FemaleCarryShoulder].weight = 0.0f;

		this.CharacterAnimation["f02_carryFlashlight_00"].layer = 16;
		this.CharacterAnimation.Play("f02_carryFlashlight_00");
		this.CharacterAnimation["f02_carryFlashlight_00"].weight = 0.0f;

		this.CharacterAnimation["f02_carryBox_00"].layer = 17;
		this.CharacterAnimation.Play("f02_carryBox_00");
		this.CharacterAnimation["f02_carryBox_00"].weight = 0.0f;

		this.CharacterAnimation["f02_holdBook_00"].layer = 18;
		this.CharacterAnimation.Play("f02_holdBook_00");
		this.CharacterAnimation["f02_holdBook_00"].weight = 0.0f;

		this.CharacterAnimation["f02_holdBook_00"].speed = 0.5f;

		this.CharacterAnimation[CreepyIdles[1]].layer = 19; this.CharacterAnimation.Play(CreepyIdles[1]); this.CharacterAnimation[CreepyIdles[1]].weight = 0.0f;
		this.CharacterAnimation[CreepyIdles[2]].layer = 20; this.CharacterAnimation.Play(CreepyIdles[2]); this.CharacterAnimation[CreepyIdles[2]].weight = 0.0f;
		this.CharacterAnimation[CreepyIdles[3]].layer = 21; this.CharacterAnimation.Play(CreepyIdles[3]); this.CharacterAnimation[CreepyIdles[3]].weight = 0.0f;
		this.CharacterAnimation[CreepyIdles[4]].layer = 22; this.CharacterAnimation.Play(CreepyIdles[4]); this.CharacterAnimation[CreepyIdles[4]].weight = 0.0f;
		this.CharacterAnimation[CreepyIdles[5]].layer = 23; this.CharacterAnimation.Play(CreepyIdles[5]); this.CharacterAnimation[CreepyIdles[5]].weight = 0.0f;
	
		this.CharacterAnimation[CreepyWalks[1]].layer = 24; this.CharacterAnimation.Play(CreepyWalks[1]); this.CharacterAnimation[CreepyWalks[1]].weight = 0.0f;
		this.CharacterAnimation[CreepyWalks[2]].layer = 25; this.CharacterAnimation.Play(CreepyWalks[2]); this.CharacterAnimation[CreepyWalks[2]].weight = 0.0f;
		this.CharacterAnimation[CreepyWalks[3]].layer = 26; this.CharacterAnimation.Play(CreepyWalks[3]); this.CharacterAnimation[CreepyWalks[3]].weight = 0.0f;
		this.CharacterAnimation[CreepyWalks[4]].layer = 27; this.CharacterAnimation.Play(CreepyWalks[4]); this.CharacterAnimation[CreepyWalks[4]].weight = 0.0f;
		this.CharacterAnimation[CreepyWalks[5]].layer = 28; this.CharacterAnimation.Play(CreepyWalks[5]); this.CharacterAnimation[CreepyWalks[5]].weight = 0.0f;

		this.CharacterAnimation[AnimNames.FemaleCarryDramatic].layer = 29;
		this.CharacterAnimation.Play(AnimNames.FemaleCarryDramatic);
		this.CharacterAnimation[AnimNames.FemaleCarryDramatic].weight = 0.0f;

		this.CharacterAnimation["f02_selfie_00"].layer = 30;
		this.CharacterAnimation.Play("f02_selfie_00");
		this.CharacterAnimation["f02_selfie_00"].weight = 0.0f;

		this.CharacterAnimation["f02_dramaticWriting_00"].layer = 31;
		this.CharacterAnimation.Play("f02_dramaticWriting_00");
		this.CharacterAnimation["f02_dramaticWriting_00"].weight = 0.0f;

		this.CharacterAnimation["f02_reachForWeapon_00"].layer = 32;
		this.CharacterAnimation.Play("f02_reachForWeapon_00");
		this.CharacterAnimation["f02_reachForWeapon_00"].weight = 0.0f;
		this.CharacterAnimation["f02_reachForWeapon_00"].speed = 2;

		this.CharacterAnimation["f02_gutsEye_00"].layer = 33;
		this.CharacterAnimation.Play("f02_gutsEye_00");
		this.CharacterAnimation["f02_gutsEye_00"].weight = 0.0f;

		this.CharacterAnimation["f02_fingerSnap_00"].layer = 34;
		this.CharacterAnimation.Play("f02_fingerSnap_00");
		this.CharacterAnimation["f02_fingerSnap_00"].weight = 0.0f;

		this.CharacterAnimation["f02_sadEyebrows_00"].layer = 35;
		this.CharacterAnimation.Play("f02_sadEyebrows_00");
		this.CharacterAnimation["f02_sadEyebrows_00"].weight = 0.0f;

		this.CharacterAnimation[AnimNames.FemaleDipping].speed = 2.0f;
		this.CharacterAnimation[AnimNames.FemaleStripping].speed = 1.50f;

		this.CharacterAnimation[AnimNames.FemaleFalconIdle].speed = 2.0f;

		this.CharacterAnimation[AnimNames.FemaleCarryIdleA].speed = 1.75f;

		this.CharacterAnimation[AnimNames.CyborgNinjaRunArmed].speed = 2.0f;
		this.CharacterAnimation[AnimNames.CyborgNinjaRunUnarmed].speed = 2.0f;
	}

	public float v = 0.0f;
	public float h = 0.0f;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftAlt))
		{
			this.CinematicCamera.SetActive(false);
		}

		if (!this.PauseScreen.Show)
		{
			////////////////////
			///// MOVEMENT /////
			////////////////////

			this.UpdateMovement();

			this.UpdatePoisoning();

			if (!this.Laughing)
			{
				MyAudio.volume -= Time.deltaTime * 2.0f;
			}
			else
			{
				if (this.PickUp != null)
				{
					if (!this.PickUp.Clothing)
					{
						this.CharacterAnimation[this.CarryAnims[1]].weight = Mathf.Lerp(
						this.CharacterAnimation[this.CarryAnims[1]].weight, 1.0f, Time.deltaTime * 10.0f);
					}
				}
			}

			if (!this.Mopping)
			{
				this.CharacterAnimation[AnimNames.FemaleMopping].weight = Mathf.Lerp(
					this.CharacterAnimation[AnimNames.FemaleMopping].weight, 0.0f, Time.deltaTime * 10.0f);
			}
			else
			{
				this.CharacterAnimation[AnimNames.FemaleMopping].weight = Mathf.Lerp(
					this.CharacterAnimation[AnimNames.FemaleMopping].weight, 1.0f, Time.deltaTime * 10.0f);

				if (Input.GetButtonUp(InputNames.Xbox_A) || Input.GetKeyDown(KeyCode.Escape))
				{
					this.Mopping = false;
				}
			}

			if (this.LaughIntensity == 0.0f)
			{
				for (this.ID = 0; this.ID < this.CarryAnims.Length; this.ID++)
				{
					string carryAnim = this.CarryAnims[this.ID];

					if ((this.PickUp != null) && (this.CarryAnimID == this.ID) &&
						!this.Mopping && !this.Dipping && !this.Pouring &&
						!this.BucketDropping && !this.Digging && !this.Burying &&
						!this.WritingName)
					{
						//Debug.Log("Performing a carry animation.");

						this.CharacterAnimation[carryAnim].weight = Mathf.Lerp(
							this.CharacterAnimation[carryAnim].weight, 1.0f, Time.deltaTime * 10.0f);
					}
					else
					{
						//Debug.Log("NOT performing a carry animation.");

						this.CharacterAnimation[carryAnim].weight = Mathf.Lerp(
							this.CharacterAnimation[carryAnim].weight, 0.0f, Time.deltaTime * 10.0f);
					}
				}
			}
			else
			{
				if (this.Armed)
				{
					this.CharacterAnimation[AnimNames.FemaleMopCarry].weight = Mathf.Lerp(
						this.CharacterAnimation[AnimNames.FemaleMopCarry].weight, 1.0f, Time.deltaTime * 10.0f);
				}
			}

			if (this.Noticed)
			{
				if (!this.Attacking && !this.Struggling)
				{
					if (!this.Collapse)
					{
						if (this.ShoulderCamera.NoticedTimer < 1)
						{
							this.CharacterAnimation.CrossFade(AnimNames.FemaleScaredIdle);
						}

						this.targetRotation = Quaternion.LookRotation(
							this.Senpai.position - this.transform.position);
						
						this.transform.rotation = Quaternion.Slerp(
							this.transform.rotation, this.targetRotation, Time.deltaTime * 10.0f);

						this.transform.localEulerAngles = new Vector3(
							0.0f,
							this.transform.localEulerAngles.y,
							this.transform.localEulerAngles.z);

						if (Vector3.Distance(this.transform.position, this.Senpai.position) < 1.25f)
						{				
							this.MyController.Move(this.transform.forward * (Time.deltaTime * -2));
						}
					}
				}
			}

			///////////////////
			///// EFFECTS /////
			///////////////////

			this.UpdateEffects();

			///////////////////
			///// TALKING /////
			///////////////////

			this.UpdateTalking();

			/////////////////////
			///// ATTACKING /////
			/////////////////////

			this.UpdateAttacking();

			//////////////////
			///// SANITY /////
			//////////////////

			this.UpdateSlouch();

			if (!this.Noticed)
			{
				this.RightYandereEye.material.color = new Color(
					this.RightYandereEye.material.color.r,
					this.RightYandereEye.material.color.g,
					this.RightYandereEye.material.color.b,
					1.0f - (this.Sanity / 100.0f));

				this.LeftYandereEye.material.color = new Color(
					this.LeftYandereEye.material.color.r,
					this.LeftYandereEye.material.color.g,
					this.LeftYandereEye.material.color.b,
					1.0f - (this.Sanity / 100.0f));

				this.EyeShrink = Mathf.Lerp(
					this.EyeShrink, 0.50f * (1.0f - (this.Sanity / 100.0f)), Time.deltaTime * 10.0f);
			}

			this.UpdateTwitch();

			////////////////////
			///// WARNINGS /////
			////////////////////

			this.UpdateWarnings();

			///////////////////////////////
			///// DEBUG FUNCTIONALITY /////
			///////////////////////////////

			this.UpdateDebugFunctionality();

			// [af] Keep Yandere-chan from falling through the ground (this doesn't appear
			// to be necessary for the school grounds anymore).
			if (this.transform.position.y < 0.0f)
			{
				this.transform.position = new Vector3(
					this.transform.position.x,
					0.0f,
					this.transform.position.z);
			}

			// [af] Keep Yandere-chan from going on the street.
			if (this.transform.position.z < -99.50f)
			{
				this.transform.position = new Vector3(
					this.transform.position.x,
					this.transform.position.y,
					-99.50f);
			}

			// [af] Keep Yandere-chan from rotating on the X or Z axes.
			this.transform.eulerAngles = new Vector3(
				0.0f,
				this.transform.eulerAngles.y,
				0.0f);
		}
		else
		{
			MyAudio.volume -= 1.0f / 3.0f;
		}

		// [af] Commented in JS code.
		/*
		if (Input.GetKeyDown(KeyCode.Keypad5))
		{
			this.CharacterAnimation.CrossFade("f02_bucketTrip_00");
			this.Tripping = true;
			this.CanMove = false;
		}
		*/

#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			//this.CharacterAnimation.CrossFade("f02_batHighSanityA_00");
			//this.CanMove = false;
			//this.Testing = true;

			this.MyRenderer.sharedMesh = TestMesh;

			//SkinUpdater.UpdateSkin();

			/*
			this.MyRenderer.sharedMesh = this.RivalChanMesh;
			*/

			/*
			this.MyRenderer.materials[0].mainTexture = this.FaceTexture;
			this.MyRenderer.materials[1].mainTexture = this.FaceTexture;
			this.MyRenderer.materials[2].mainTexture = this.FaceTexture;
			this.MyRenderer.materials[3].mainTexture = this.FaceTexture;
			
			this.MyRenderer.materials[0].shader = Shader.Find("Diffuse");
			this.MyRenderer.materials[1].shader = Shader.Find("Diffuse");
			this.MyRenderer.materials[2].shader = Shader.Find("Diffuse");
			this.MyRenderer.materials[3].shader = Shader.Find("Diffuse");
			
			//Instantiate(Fireball, transform.position + Vector3.up + (transform.forward * .5), transform.rotation);
			*/
		}
#endif

		// [af] Commented in JS code.
		/*
		if (Input.GetKeyDown(KeyCode.Space))
		{
			this.LiftOffParticles.SetActive(true);
			this.LiftOff = true;
		}
		
		if (this.LiftOff)
		{
			this.LiftOffSpeed += Time.deltaTime * .01;
			this.Character.transform.localPosition.y += this.LiftOffSpeed;
		}
		*/
	}

	int DebugInt;

	void GoToPKDir(PKDirType pkDir, string sansAnim, Vector3 ragdollLocalPos)
	{
		this.CharacterAnimation.CrossFade(sansAnim);
		this.RagdollPK.transform.localPosition = ragdollLocalPos;

		if (this.PKDir != pkDir)
		{
			AudioSource.PlayClipAtPoint(this.Slam, this.transform.position + Vector3.up);
		}

		this.PKDir = pkDir;
	}

	void UpdateMovement()
	{
		if (this.CanMove)
		{
			if (!this.ToggleRun)
			{
				this.Running = false;

				if (Input.GetButton(InputNames.Xbox_LB))
				{
					this.Running = true;
				}
			}
			else
			{
				if (Input.GetButtonDown(InputNames.Xbox_LB))
				{
					this.Running = !this.Running;
				}
			}

			this.MyController.Move(Physics.gravity * Time.deltaTime);

			this.v = Input.GetAxis("Vertical");
			this.h = Input.GetAxis("Horizontal");
			this.FlapSpeed = Mathf.Abs(this.v) + Mathf.Abs(this.h);

			if (this.Selfie)
			{
				this.v = -1 * this.v;
				this.h = -1 * this.h;
			}

			if (!this.Aiming)
			{
				Vector3 forward = this.MainCamera.transform.TransformDirection(Vector3.forward);
				forward.y = 0.0f;
				forward = forward.normalized;

				Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);

				this.targetDirection = (this.h * right) + (this.v * forward);

				if (this.targetDirection != Vector3.zero)
				{
					this.targetRotation = Quaternion.LookRotation(this.targetDirection);
					this.transform.rotation = Quaternion.Lerp(
						this.transform.rotation, this.targetRotation, Time.deltaTime * 10.0f);
				}
				else
				{
					this.targetRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
				}

				// If we are getting directional input...
				if ((this.v != 0.0f) || (this.h != 0.0f))
				{
					// [af] Commented in JS code.
					//DragState++;

					// If the Run button is held down...
					if (this.Running && Vector3.Distance(this.transform.position, this.Senpai.position) > 1.0f)
					{
						if (this.Stance.Current == StanceType.Crouching)
						{
							// Crouch-Run animation.
							//this.CharacterAnimation[this.CrouchWalkAnim].speed = 2.0f;
							this.CharacterAnimation.CrossFade(this.CrouchRunAnim);
							this.MyController.Move(this.transform.forward * (CrouchRunSpeed +
								((PhysicalGrade + SpeedBonus) * 0.25f)) *
								Time.deltaTime);
						}
						else if (!this.Dragging && !this.Mopping)
						{
							// Run animation.
							this.CharacterAnimation.CrossFade(this.RunAnim);
							this.MyController.Move(this.transform.forward * (this.RunSpeed +
								((PhysicalGrade + SpeedBonus) * 0.25f)) *
								Time.deltaTime);
						}
						else
						{
							if (this.Mopping)
							{
								// Walk animation.
								this.CharacterAnimation.CrossFade(this.WalkAnim);
								this.MyController.Move(this.transform.forward * (this.WalkSpeed * Time.deltaTime));
							}
						}

						if (this.Stance.Current == StanceType.Crouching)
						{
							// [af] Commented in JS code.
							//Crouching = false;
						}

						if (this.Stance.Current == StanceType.Crawling)
						{
							this.Stance.Current = StanceType.Crouching;
							this.Crouch();
						}
					}
					// If the Run button is not held down...
					else
					{
						if (!this.Dragging)
						{
							if (this.Stance.Current == StanceType.Crawling)
							{
								// Crawl animation.
								this.CharacterAnimation.CrossFade(this.CrawlWalkAnim);
								this.MyController.Move(this.transform.forward * (this.CrawlSpeed * Time.deltaTime));
							}
							else if (this.Stance.Current == StanceType.Crouching)
							{
								// Crouch-walk animation.
								this.CharacterAnimation[this.CrouchWalkAnim].speed = 1.0f;
								this.CharacterAnimation.CrossFade(this.CrouchWalkAnim);
								this.MyController.Move(this.transform.forward * (this.CrouchWalkSpeed * Time.deltaTime));
							}
							else
							{
								// Walk animation.
								this.CharacterAnimation.CrossFade(this.WalkAnim);

								if (this.NearSenpai)
								{
									for (int i = 1; i < 6; i++)
									{
										if (i != this.Creepiness)
										{
											this.CharacterAnimation[this.CreepyIdles[i]].weight = Mathf.MoveTowards(this.CharacterAnimation[this.CreepyIdles[i]].weight, 0, Time.deltaTime);
											this.CharacterAnimation[this.CreepyWalks[i]].weight = Mathf.MoveTowards(this.CharacterAnimation[this.CreepyWalks[i]].weight, 0, Time.deltaTime);
										}
									}

									this.CharacterAnimation[CreepyIdles[this.Creepiness]].weight = Mathf.MoveTowards(this.CharacterAnimation[CreepyIdles[this.Creepiness]].weight, 0, Time.deltaTime);
									this.CharacterAnimation[CreepyWalks[this.Creepiness]].weight = Mathf.MoveTowards(this.CharacterAnimation[CreepyWalks[this.Creepiness]].weight, 1, Time.deltaTime);
								}

								this.MyController.Move(this.transform.forward * (this.WalkSpeed * Time.deltaTime));
							}
						}
						else
						{
							// Drag animation.
							this.CharacterAnimation.CrossFade(AnimNames.FemaleDragWalk01);
							this.MyController.Move(this.transform.forward * (this.WalkSpeed * Time.deltaTime));
						}
					}
				}
				// If we are not getting directional input...
				else
				{
					if (!this.Dragging)
					{
						if (this.Stance.Current == StanceType.Crawling)
						{
							this.CharacterAnimation.CrossFade(this.CrawlIdleAnim);
						}
						else if (this.Stance.Current == StanceType.Crouching)
						{
							this.CharacterAnimation.CrossFade(this.CrouchIdleAnim);
						}
						else
						{
							this.CharacterAnimation.CrossFade(this.IdleAnim);

							if (this.NearSenpai)
							{
								for (int i = 1; i < 6; i++)
								{
									if (i != this.Creepiness)
									{
										this.CharacterAnimation[this.CreepyIdles[i]].weight = Mathf.MoveTowards(this.CharacterAnimation[this.CreepyIdles[i]].weight, 0, Time.deltaTime);
										this.CharacterAnimation[this.CreepyWalks[i]].weight = Mathf.MoveTowards(this.CharacterAnimation[this.CreepyWalks[i]].weight, 0, Time.deltaTime);
									}
								}

								this.CharacterAnimation[CreepyIdles[this.Creepiness]].weight = Mathf.MoveTowards(this.CharacterAnimation[CreepyIdles[this.Creepiness]].weight, 1, Time.deltaTime);
								this.CharacterAnimation[CreepyWalks[this.Creepiness]].weight = Mathf.MoveTowards(this.CharacterAnimation[CreepyWalks[this.Creepiness]].weight, 0, Time.deltaTime);
							}
						}
					}
					else
					{
						// [af] Commented in JS code.
						//if (DragState == 0)
						//{
						//	CharacterAnimation.CrossFade("f02_dragIdle_01");
						//}
						//else
						//{
						this.CharacterAnimation.CrossFade(AnimNames.FemaleDragIdle02);
						//}
					}
				}
			}
			// If the player is aiming the camera...
			else
			{
				// If we are getting directional input...
				if ((this.v != 0.0f) || (this.h != 0.0f))
				{
					if (this.Stance.Current == StanceType.Crawling)
					{
						// Crawl animation.
						this.CharacterAnimation.CrossFade(this.CrawlWalkAnim);
						this.MyController.Move(this.transform.forward * (this.CrawlSpeed * Time.deltaTime * this.v));
						this.MyController.Move(this.transform.right * (this.CrawlSpeed * Time.deltaTime * this.h));
					}
					else if (this.Stance.Current == StanceType.Crouching)
					{
						// Crouch-walk animation.
						this.CharacterAnimation.CrossFade(this.CrouchWalkAnim);
						this.MyController.Move(this.transform.forward * (this.CrouchWalkSpeed * Time.deltaTime * this.v));
						this.MyController.Move(this.transform.right * (this.CrouchWalkSpeed * Time.deltaTime * this.h));
					}
					else
					{
						// Walk animation.
						this.CharacterAnimation.CrossFade(this.WalkAnim);
						this.MyController.Move(this.transform.forward * (this.WalkSpeed * Time.deltaTime * this.v));
						this.MyController.Move(this.transform.right * (this.WalkSpeed * Time.deltaTime * this.h));
					}
				}
				else
				{
					if (this.Stance.Current == StanceType.Crawling)
					{
						this.CharacterAnimation.CrossFade(this.CrawlIdleAnim);
					}
					else if (this.Stance.Current == StanceType.Crouching)
					{
						this.CharacterAnimation.CrossFade(this.CrouchIdleAnim);
					}
					else
					{
						this.CharacterAnimation.CrossFade(this.IdleAnim);
					}
				}

				if (!RPGCamera.invertAxis)
            	{
					this.Bend += Input.GetAxis("Mouse Y") * 8.0f;
				}
				else
				{
					this.Bend -= Input.GetAxis("Mouse Y") * 8.0f;
				}

				/*
				if (this.Selfie)
				{
					this.Bend = -1 * this.Bend;
				}
				*/

				if (this.Stance.Current == StanceType.Crawling)
				{
					if (this.Bend < 0.0f)
					{
						this.Bend = 0.0f;
					}
				}
				else if (this.Stance.Current == StanceType.Crouching)
				{
					if (this.Bend < -45.0f)
					{
						this.Bend = -45.0f;
					}
				}
				else
				{
					if (this.Bend < -85.0f)
					{
						this.Bend = -85.0f;
					}
				}

				if (this.Bend > 85.0f)
				{
					this.Bend = 85.0f;
				}

				this.transform.localEulerAngles = new Vector3(
					this.transform.localEulerAngles.x,
					this.transform.localEulerAngles.y + (Input.GetAxis("Mouse X") * 8.0f),
					this.transform.localEulerAngles.z);
			}

			if (!this.NearSenpai)
			{
				if (!Input.GetButton(InputNames.Xbox_A) && !Input.GetButton(InputNames.Xbox_B) && 
					!Input.GetButton(InputNames.Xbox_X) && !Input.GetButton(InputNames.Xbox_Y) &&
					!this.StudentManager.Clock.UpdateBloom)
				{
					if ((Input.GetAxis(InputNames.Xbox_LT) > 0.50f) || 
						Input.GetMouseButton(InputNames.Mouse_RMB))
					{
						if (this.Inventory.RivalPhone)
						{
							if (Input.GetButtonDown(InputNames.Xbox_LB))
							{
								this.CharacterAnimation[AnimNames.FemaleCameraPose].weight = 0.0f;
								this.CharacterAnimation["f02_selfie_00"].weight = 0.0f;

								if (!this.RivalPhone)
								{
									this.SmartphoneRenderer.material.mainTexture = this.RivalPhoneTexture;
									this.PhonePromptBar.Label.text = "SWITCH TO YOUR PHONE";
									this.RivalPhone = true;
								}
								else
								{
									this.SmartphoneRenderer.material.mainTexture = this.YanderePhoneTexture;
									this.PhonePromptBar.Label.text = "SWITCH TO STOLEN PHONE";
									this.RivalPhone = false;
								}
							}
						}
						else
						{
							if (!this.Selfie)
							{
								if (Input.GetButtonDown(InputNames.Xbox_LB))
								{
									if (!this.AR)
									{
										this.Smartphone.cullingMask |= (1 << LayerMask.NameToLayer("Miyuki"));
										this.AR = true;
									}
									else
									{
										this.Smartphone.cullingMask &= ~(1 << LayerMask.NameToLayer("Miyuki"));
										this.AR = false;
									}
								}
							}
						}

						if (Input.GetAxis(InputNames.Xbox_LT) > 0.50f)
						{
							this.UsingController = true;
						}

						if (!this.Aiming)
						{
							if (this.CameraEffects.OneCamera)
							{
								this.MainCamera.clearFlags = CameraClearFlags.SolidColor;
								this.MainCamera.farClipPlane = 0.020f;

								this.HandCamera.clearFlags = CameraClearFlags.SolidColor;
							}
							else
							{
								this.MainCamera.clearFlags = CameraClearFlags.Skybox;
								this.MainCamera.farClipPlane = OptionGlobals.DrawDistance;

								this.HandCamera.clearFlags = CameraClearFlags.Depth;
							}

							this.transform.eulerAngles = new Vector3(
								this.transform.eulerAngles.x,
								this.MainCamera.transform.eulerAngles.y,
								this.transform.eulerAngles.z);

							this.CharacterAnimation.Play(this.IdleAnim);

							// [af] Added "gameObject" for C# compatibility.
							this.Smartphone.transform.parent.gameObject.SetActive(true);

							if (!this.CinematicCamera.activeInHierarchy)
							{
								this.DisableHairAndAccessories();
							}

							this.HandCamera.gameObject.SetActive(true);
							this.ShoulderCamera.AimingCamera = true;
							this.Obscurance.enabled = false;
							this.YandereVision = false;
							this.Blur.enabled = true;
							this.Mopping = false;
							this.Selfie = false;
							this.Aiming = true;
							this.EmptyHands();

							this.PhonePromptBar.Panel.enabled = true;
							this.PhonePromptBar.Show = true;

							if (this.Inventory.RivalPhone)
							{
								if (!this.RivalPhone)
								{
									this.PhonePromptBar.Label.text = "SWITCH TO STOLEN PHONE";
								}
								else
								{
									this.PhonePromptBar.Label.text = "SWITCH TO YOUR PHONE";
								}
							}
							else
							{
								this.PhonePromptBar.Label.text = "AR GAME ON/OFF";
							}

							Time.timeScale = 1.0f;

							this.UpdateSelfieStatus();
                            this.StudentManager.UpdatePanties(true);
                        }
					}
				}

                this.PermitLaugh += Time.deltaTime;

				if (!this.Aiming && !this.Accessories[9].activeInHierarchy && !this.Accessories[16].activeInHierarchy && !this.Pod.activeInHierarchy && this.PermitLaugh > 1)
				{
					if (Input.GetButton(InputNames.Xbox_RB))
					{
						if (this.MagicalGirl)
						{
							if (this.Armed)
							{
								if (this.EquippedWeapon.WeaponID == 14)
								{
									if (Input.GetButtonDown(InputNames.Xbox_RB))
									{
										if (!this.ShootingBeam)
										{
											AudioSource.PlayClipAtPoint(this.LoveLoveBeamVoice, this.transform.position);
											this.CharacterAnimation["f02_LoveLoveBeam_00"].time = 0;
											this.CharacterAnimation.CrossFade("f02_LoveLoveBeam_00");
											this.ShootingBeam = true;
											this.CanMove = false;
										}
									}
								}
							}
						}
						else if (this.BlackRobe.activeInHierarchy)
						{
							if (Input.GetButtonDown(InputNames.Xbox_RB))
							{
								AudioSource.PlayClipAtPoint(this.SithOn, this.transform.position);
							}

							this.SithTrailEnd1.localPosition = new Vector3(-1, 0, 0);
							this.SithTrailEnd2.localPosition = new Vector3(1, 0, 0);

							this.Beam[0].Play();
							this.Beam[1].Play();
							this.Beam[2].Play();
							this.Beam[3].Play();

							if (Input.GetButtonDown(InputNames.Xbox_X))
							{
								this.CharacterAnimation["f02_sithAttack_00"].time = 0;
								this.CharacterAnimation.Play("f02_sithAttack_00");
								this.SithBeam[1].Damage = 10;
								this.SithBeam[2].Damage = 10;
								this.SithAttacking = true;
								this.CanMove = false;

								this.SithPrefix = "";
								this.AttackPrefix = "sith";
							}

							if (Input.GetButtonDown(InputNames.Xbox_Y))
							{
								this.CharacterAnimation["f02_sithAttackHard_00"].time = 0;
								this.CharacterAnimation.Play("f02_sithAttackHard_00");
								this.SithBeam[1].Damage = 20;
								this.SithBeam[2].Damage = 20;
								this.SithAttacking = true;
								this.CanMove = false;

								this.SithPrefix = "Hard";
								this.AttackPrefix = "sith";
							}
						}
						else
						{
							if (Input.GetButtonDown(InputNames.Xbox_RB))
							{
								if (this.SpiderLegs.activeInHierarchy)
								{
									this.SpiderGrow = !SpiderGrow;

									if (this.SpiderGrow)
									{
										AudioSource.PlayClipAtPoint(this.EmptyDemon.MouthOpen, this.transform.position);
									}
									else
									{
										AudioSource.PlayClipAtPoint(this.EmptyDemon.MouthClose, this.transform.position);
									}

									this.StudentManager.UpdateStudents();
								}
							}
						}

						this.YandereTimer += Time.deltaTime;

						if (this.YandereTimer > 0.50f)
						{
							if (!this.Sans && !this.BlackRobe.activeInHierarchy)
							{
								this.YandereVision = true;
							}
							else
							{
								if (this.Sans)
								{
									this.SansEyes[0].SetActive(true);
									this.SansEyes[1].SetActive(true);
									this.GlowEffect.Play();
									this.SummonBones = true;
									this.YandereTimer = 0.0f;
									this.CanMove = false;
								}
							}
						}
					}
					else
					{
						if (this.BlackRobe.activeInHierarchy)
						{
							this.SithTrailEnd1.localPosition = new Vector3(0, 0, 0);
							this.SithTrailEnd2.localPosition = new Vector3(0, 0, 0);
							
							if (Input.GetButtonUp(InputNames.Xbox_RB))
							{
								AudioSource.PlayClipAtPoint(this.SithOff, this.transform.position);
							}

							this.Beam[0].Stop();
							this.Beam[1].Stop();
							this.Beam[2].Stop();
							this.Beam[3].Stop();
						}

						if (this.YandereVision)
						{
							this.Obscurance.enabled = false;
							this.YandereVision = false;
						}
					}

					if (Input.GetButtonUp(InputNames.Xbox_RB))
					{
						if (this.Stance.Current != StanceType.Crouching && this.Stance.Current != StanceType.Crawling)
						{
							if (this.YandereTimer < 0.50f)
							{
								if (!this.Dragging && !this.Carrying && !this.Pod.activeInHierarchy)
								{
									if (!this.Laughing)
									{
										if (this.Sans)
										{
											this.BlasterStage++;

											if (this.BlasterStage > 5)
											{
												this.BlasterStage = 1;
											}

											GameObject NewBlasterSet = Instantiate(
												this.BlasterSet[this.BlasterStage], this.transform.position, Quaternion.identity);

											NewBlasterSet.transform.position = this.transform.position;
											NewBlasterSet.transform.rotation = this.transform.rotation;

											AudioSource.PlayClipAtPoint(this.BlasterClip, this.transform.position + Vector3.up);
											this.CharacterAnimation[AnimNames.FemaleSansBlaster].time = 0.0f;
											this.CharacterAnimation.Play(AnimNames.FemaleSansBlaster);

											this.SansEyes[0].SetActive(true);
											this.SansEyes[1].SetActive(true);
											this.GlowEffect.Play();

											this.Blasting = true;
											this.CanMove = false;
										}
										else if (this.BlackRobe.activeInHierarchy)
										{
											//Nothing
										}
										else if (this.Kagune[0].activeInHierarchy)
										{
											if (!this.SwingKagune)
											{
												AudioSource.PlayClipAtPoint(this.KaguneSwoosh, this.transform.position + Vector3.up);
												this.SwingKagune = true;
											}
										}
										else if (this.Gazing || this.Shipgirl)
										{
											if (this.Gazing)
											{
												this.CharacterAnimation["f02_gazerSnap_00"].time = 0;
												this.CharacterAnimation.CrossFade("f02_gazerSnap_00");
											}
											else
											{
												this.CharacterAnimation["f02_shipGirlSnap_00"].time = 0;
												this.CharacterAnimation.CrossFade("f02_shipGirlSnap_00");
											}

											this.Snapping = true;
											this.CanMove = false;
										}
										else if (this.WitchMode)
										{
											if (!this.StoppingTime)
											{
												this.CharacterAnimation["f02_summonStand_00"].time = 0;

												if (this.Freezing)
												{
													AudioSource.PlayClipAtPoint(this.StartShout, this.transform.position);
												}
												else
												{
													AudioSource.PlayClipAtPoint(this.StopShout, this.transform.position);
												}

												this.Freezing = !this.InvertSphere.gameObject.activeInHierarchy;
												this.StoppingTime = true;
												this.CanMove = false;
												this.MyAudio.Stop();
												this.Egg = true;
											}
										}
										else if (this.PickUp != null && this.PickUp.CarryAnimID == 10)
										{
											this.StudentManager.NoteWindow.gameObject.SetActive(true);
											this.StudentManager.NoteWindow.Show = true;

											this.PromptBar.Show = true;
											this.Blur.enabled = true;
											this.CanMove = false;

											Time.timeScale = 0.0001f;
											this.HUD.alpha = 0.0f;

											this.PromptBar.Label[0].text = "Confirm";
											this.PromptBar.Label[1].text = "Cancel";
											this.PromptBar.Label[4].text = "Select";
											this.PromptBar.UpdateButtons();
										}
										else
										{
											if (!this.FalconHelmet.activeInHierarchy && !this.Cape.activeInHierarchy && !this.MagicalGirl)
											{
												//If we're not currently using the X-tan easter egg...
												if (!this.Xtan)
												{
													if (!this.CirnoHair.activeInHierarchy && !this.TornadoHair.activeInHierarchy &&
														!this.BladeHair.activeInHierarchy)
													{
														this.LaughAnim = AnimNames.FemaleLaugh01;
														this.LaughClip = this.Laugh1;

														this.LaughIntensity += 1.0f;

														MyAudio.clip = this.LaughClip;
														MyAudio.time = 0.0f;
														MyAudio.Play();
													}

													GiggleLines.Play();
													Instantiate(this.GiggleDisc,
														this.transform.position + Vector3.up, Quaternion.identity);

													MyAudio.volume = 1.0f;
													this.LaughTimer = 0.50f;
													this.Laughing = true;
                                                    this.Mopping = false;
													this.CanMove = false;
													this.Teeth.SetActive(false);
												}
												//If we're currently using the X-tan easter egg...
												else
												{
													if (this.LongHair[0].gameObject.activeInHierarchy)
													{
														this.LongHair[0].gameObject.SetActive(false);
														this.BlackEyePatch.SetActive(false);

														this.SlenderHair[0].transform.parent.gameObject.SetActive(true);
														this.SlenderHair[0].SetActive(true);
														this.SlenderHair[1].SetActive(true);
													}
													else
													{
														this.LongHair[0].gameObject.SetActive(true);
														this.BlackEyePatch.SetActive(true);

														this.SlenderHair[0].transform.parent.gameObject.SetActive(true);
														this.SlenderHair[0].SetActive(false);
														this.SlenderHair[1].SetActive(false);
													}
												}
											}
											else
											{
												if (!this.Punching)
												{
													if (this.FalconHelmet.activeInHierarchy)
													{
														GameObject WindUp = Instantiate(this.FalconWindUp);
														WindUp.transform.parent = this.ItemParent;
														WindUp.transform.localPosition = Vector3.zero;
														
														AudioClipPlayer.PlayAttached(this.FalconPunchVoice,
															this.MainCamera.transform, 5.0f, 10.0f);

														this.CharacterAnimation[AnimNames.FemaleFalconPunch].time = 0.0f;
														this.CharacterAnimation.Play(AnimNames.FemaleFalconPunch);
														this.FalconSpeed = 0.0f;
													}
													else
													{
														GameObject WindUp = Instantiate(this.FalconWindUp);
														WindUp.transform.parent = this.ItemParent;
														WindUp.transform.localPosition = Vector3.zero;

														AudioSource.PlayClipAtPoint(this.OnePunchVoices[Random.Range(0, OnePunchVoices.Length)], this.transform.position + Vector3.up);
														this.CharacterAnimation[AnimNames.FemaleOnePunch].time = 0.0f;
														this.CharacterAnimation.CrossFade(AnimNames.FemaleOnePunch, 0.15f);
													}

													this.Punching = true;
													this.CanMove = false;
												}
											}
										}
									}
								}
							}
						}

						this.YandereTimer = 0.0f;
					}
				}

				//if (!this.Running)
				//{
					if ((this.Stance.Current != StanceType.Crouching) &&
						(this.Stance.Current != StanceType.Crawling))
					{
						if (Input.GetButtonDown(InputNames.Xbox_RS))
						{
							this.Obscurance.enabled = false;
							this.CrouchButtonDown = true;
							this.YandereVision = false;
							this.Stance.Current = StanceType.Crouching;
							//this.Running = false;
							this.Crouch();

							this.EmptyHands();
						}
					}
					else
					{
						if (this.Stance.Current == StanceType.Crouching)
						{
							if (Input.GetButton(InputNames.Xbox_RS))
							{
								if (!this.CameFromCrouch)
								{
									this.CrawlTimer += Time.deltaTime;
								}
							}

							if (this.CrawlTimer > 0.50f)
							{
								if (!this.Selfie)
								{
									this.EmptyHands();

									this.Obscurance.enabled = false;
									this.YandereVision = false;
									this.Stance.Current = StanceType.Crawling;
									this.CrawlTimer = 0.0f;
									this.Crawl();
								}
							}
							else
							{
								if (Input.GetButtonUp(InputNames.Xbox_RS))
								{
									if (!this.CrouchButtonDown)
									{
										if (!this.CameFromCrouch)
										{
											this.Stance.Current = StanceType.Standing;
											this.CrawlTimer = 0.0f;
											this.Uncrouch();
										}
									}
								}
							}
						}
						else
						{
							if (Input.GetButtonDown(InputNames.Xbox_RS))
							{
								this.CameFromCrouch = true;
								this.Stance.Current = StanceType.Crouching;
								this.Crouch();
							}
						}

						if (Input.GetButtonUp(InputNames.Xbox_RS))
						{
							this.CrouchButtonDown = false;
							this.CameFromCrouch = false;
							this.CrawlTimer = 0.0f;
						}
					}
				//}
			}

			if (this.Aiming)
			{
				if (!this.RivalPhone && Input.GetButtonDown(InputNames.Xbox_A))
				{
					Selfie = !Selfie;

					this.UpdateSelfieStatus();
				}

				if (!Selfie)
				{
					this.CharacterAnimation[AnimNames.FemaleCameraPose].weight = Mathf.Lerp(
						this.CharacterAnimation[AnimNames.FemaleCameraPose].weight, 1.0f, Time.deltaTime * 10.0f);

					this.CharacterAnimation["f02_selfie_00"].weight = Mathf.Lerp(
						this.CharacterAnimation["f02_selfie_00"].weight, 0, Time.deltaTime * 10.0f);
				}
				else
				{
					this.CharacterAnimation[AnimNames.FemaleCameraPose].weight = Mathf.Lerp(
						this.CharacterAnimation[AnimNames.FemaleCameraPose].weight, 0, Time.deltaTime * 10.0f);

					this.CharacterAnimation["f02_selfie_00"].weight = Mathf.Lerp(
						this.CharacterAnimation["f02_selfie_00"].weight, 1.0f, Time.deltaTime * 10.0f);

					if (Input.GetButtonDown(InputNames.Xbox_B))
					{
						if (!SelfieGuide.activeInHierarchy)
						{
							SelfieGuide.SetActive(true);
						}
						else
						{
							SelfieGuide.SetActive(false);
						}
					}
				}

				if (this.ClubAccessories[7].activeInHierarchy)
				{
					if ((Input.GetAxis("DpadY") != 0.0f) || (Input.GetAxis("Mouse ScrollWheel") != 0.0f) ||
						Input.GetKey(KeyCode.Tab) || Input.GetKey(KeyCode.LeftShift))
					{
						if (Input.GetKey(KeyCode.Tab))
						{
							this.Smartphone.fieldOfView -= Time.deltaTime * 100;
						}

						if (Input.GetKey(KeyCode.LeftShift))
						{
							this.Smartphone.fieldOfView += Time.deltaTime * 100;
						}

						this.Smartphone.fieldOfView -= Input.GetAxis("DpadY");

						this.Smartphone.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * 10.0f;

						if (this.Smartphone.fieldOfView > 60.0f)
						{
							this.Smartphone.fieldOfView = 60.0f;
						}

						if (this.Smartphone.fieldOfView < 30.0f)
						{
							this.Smartphone.fieldOfView = 30.0f;
						}
					}
				}

				if (Input.GetAxis(InputNames.Xbox_RT) == 1 || 
					Input.GetMouseButtonDown(InputNames.Mouse_LMB) ||
					Input.GetButtonDown(InputNames.Xbox_RB))
				{
					//Debug.Log(Input.GetAxis(InputNames.Xbox_RT));

					this.FixCamera();

					this.PauseScreen.CorrectingTime = false;
					Time.timeScale = 0.0001f;
					this.CanMove = false;
					this.Shutter.Snap();
				}

				if (Time.timeScale > 0.0001f)
				{
					if (this.UsingController && (Input.GetAxis(InputNames.Xbox_LT) < 0.50f) ||
						!this.UsingController && !Input.GetMouseButton(InputNames.Mouse_RMB))
					{
						this.StopAiming();
					}
				}

				if (Input.GetKey(KeyCode.LeftAlt))
				{
					if (!this.CinematicCamera.activeInHierarchy)
					{
						if (this.CinematicTimer > 0.0f)
						{
							this.CinematicCamera.transform.eulerAngles = this.Smartphone.transform.eulerAngles;
							this.CinematicCamera.transform.position = this.Smartphone.transform.position;
							this.CinematicCamera.SetActive(true);
							this.CinematicTimer = 0.0f;

							this.UpdateHair();

							this.StopAiming();
						}

						this.CinematicTimer++;
					}
				}
				else
				{
					this.CinematicTimer = 0.0f;
				}
			}

			if (this.Gloved)
			{
				if (!this.Chased && this.Chasers == 0)
				{
					if (this.InputDevice.Type == InputDeviceType.Gamepad)
					{
						if (Input.GetAxis("DpadY") < -0.50f)
						{
							this.GloveTimer += Time.deltaTime;

							if (this.GloveTimer > 0.50f)
							{
								this.CharacterAnimation.CrossFade(AnimNames.FemaleRemoveGloves);
								this.Degloving = true;
								this.CanMove = false;
							}
						}
						else
						{
							this.GloveTimer = 0.0f;
						}
					}
					else
					{
						if (Input.GetKey(KeyCode.Alpha1))
						{
							this.GloveTimer += Time.deltaTime;

							if (this.GloveTimer > 0.10f)
							{
								this.CharacterAnimation.CrossFade(AnimNames.FemaleRemoveGloves);
								this.Degloving = true;
								this.CanMove = false;
							}
						}
						else
						{
							this.GloveTimer = 0.0f;
						}
					}
				}
				else
				{
					this.GloveTimer = 0.0f;
				}
			}

			if (this.Weapon[1] != null)
			{
				if (this.DropTimer[2] == 0.0f)
				{
					if (this.InputDevice.Type == InputDeviceType.Gamepad)
					{
						if (Input.GetAxis("DpadX") < -0.50f)
						{
							this.DropWeapon(1);
						}
						else
						{
							this.DropTimer[1] = 0.0f;
						}
					}
					else
					{
						if (Input.GetKey(KeyCode.Alpha2))
						{
							this.DropWeapon(1);
						}
						else
						{
							this.DropTimer[1] = 0.0f;
						}
					}
				}
			}

			if (this.Weapon[2] != null)
			{
				if (this.DropTimer[1] == 0.0f)
				{
					if (this.InputDevice.Type == InputDeviceType.Gamepad)
					{
						if (Input.GetAxis("DpadX") > 0.50f)
						{
							this.DropWeapon(2);
						}
						else
						{
							this.DropTimer[2] = 0.0f;
						}
					}
					else
					{
						if (Input.GetKey(KeyCode.Alpha3))
						{
							this.DropWeapon(2);
						}
						else
						{
							this.DropTimer[2] = 0.0f;
						}
					}
				}
			}

			if (Input.GetButtonDown(InputNames.Xbox_LS) || Input.GetKeyDown(KeyCode.T))
			{
				if (this.NewTrail != null)
				{
					Destroy(this.NewTrail);
				}

				this.NewTrail = Instantiate(this.Trail,
					this.transform.position + (this.transform.forward * 0.50f) + (Vector3.up * 0.10f),
					Quaternion.identity);

				if (SchemeGlobals.CurrentScheme == 0)
				{
					this.NewTrail.GetComponent<AIPath>().target = this.Homeroom;
				}
				else
				{
					if (this.PauseScreen.Schemes.SchemeDestinations[SchemeGlobals.GetSchemeStage(SchemeGlobals.CurrentScheme)] != null)
					{
						this.NewTrail.GetComponent<AIPath>().target =
							this.PauseScreen.Schemes.SchemeDestinations[SchemeGlobals.GetSchemeStage(SchemeGlobals.CurrentScheme)];
					}
					else
					{
						Destroy(this.NewTrail);
					}
				}
			}

			if (this.Armed)
			{
				// [af] Converted while loop to for loop.
				for (this.ID = 0; this.ID < this.ArmedAnims.Length; this.ID++)
				{
					string armedAnim = this.ArmedAnims[this.ID];

					// [af] Replaced if/else statement with ternary expression.
					this.CharacterAnimation[armedAnim].weight = Mathf.Lerp(
						this.CharacterAnimation[armedAnim].weight,
						(this.EquippedWeapon.AnimID == this.ID) ? 1.0f : 0.0f,
						Time.deltaTime * 10.0f);
				}
			}
			else
			{
				StopArmedAnim();
			}

			if (this.TheftTimer > 0)
			{
				this.TheftTimer = Mathf.MoveTowards(this.TheftTimer, 0, Time.deltaTime);
			}

			if (this.WeaponTimer > 0)
			{
				this.WeaponTimer = Mathf.MoveTowards(this.WeaponTimer, 0, Time.deltaTime);
			}

			if (this.MurderousActionTimer > 0)
			{
				this.MurderousActionTimer = Mathf.MoveTowards(this.MurderousActionTimer, 0, Time.deltaTime);

				if (this.MurderousActionTimer == 0)
				{
					this.TargetStudent = null;
				}
			}

			if (this.Chased)
			{
				this.PreparedForStruggle = true;
				this.CanMove = false;
			}

			if (this.Egg)
			{
				if (this.Eating)
				{
					this.FollowHips = false;
					this.Attacking = false;
					this.CanMove = true;
					this.Eating = false;
					this.EatPhase = 0;
				}

				if (this.Pod.activeInHierarchy)
				{
					if (!this.SithAttacking)
					{
						if (this.LightSword.transform.parent != this.LightSwordParent)
						{
							this.LightSword.transform.parent = this.LightSwordParent;
							this.LightSword.transform.localPosition = new Vector3(0, 0, 0);
							this.LightSword.transform.localEulerAngles = new Vector3(0, 0, 0);

							this.LightSwordParticles.Play();
						}

						if (this.HeavySword.transform.parent != this.HeavySwordParent)
						{
							this.HeavySword.transform.parent = this.HeavySwordParent;
							this.HeavySword.transform.localPosition = new Vector3(0, 0, 0);
							this.HeavySword.transform.localEulerAngles = new Vector3(0, 0, 0);

							this.HeavySwordParticles.Play();
						}
					}

					if (Input.GetButtonDown(InputNames.Xbox_X))
					{
						this.LightSword.transform.parent = this.LeftItemParent;
						this.LightSword.transform.localPosition = new Vector3(-.015f, 0, 0);
						this.LightSword.transform.localEulerAngles = new Vector3(0, 0, -90);

						this.LightSword.GetComponent<WeaponTrail>().enabled = true;
						this.LightSword.GetComponent<WeaponTrail>().Start();

						this.CharacterAnimation["f02_nierAttack_00"].time = 0;
						this.CharacterAnimation.Play("f02_nierAttack_00");
						this.SithAttacking = true;
						this.CanMove = false;

						this.SithBeam[1].Damage = 10;
						this.NierDamage = 10;

						this.SithPrefix = "";
						this.AttackPrefix = "nier";
					}

					if (Input.GetButtonDown(InputNames.Xbox_Y))
					{
						this.HeavySword.transform.parent = this.ItemParent;
						this.HeavySword.transform.localPosition = new Vector3(-.015f, 0, 0);
						this.HeavySword.transform.localEulerAngles = new Vector3(0, 0, -90);

						this.HeavySword.GetComponent<WeaponTrail>().enabled = true;
						this.HeavySword.GetComponent<WeaponTrail>().Start();

						this.CharacterAnimation["f02_nierAttackHard_00"].time = 0;
						this.CharacterAnimation.Play("f02_nierAttackHard_00");
						this.SithAttacking = true;
						this.CanMove = false;

						this.SithBeam[1].Damage = 20;
						this.NierDamage = 20;

						this.SithPrefix = "Hard";
						this.AttackPrefix = "nier";
					}
				}

				if (this.WitchMode)
				{
					if (Input.GetButtonDown(InputNames.Xbox_X))
					{
						if (this.InvertSphere.gameObject.activeInHierarchy)
						{
							this.CharacterAnimation["f02_fingerSnap_00"].time = 0;
							this.CharacterAnimation.Play("f02_fingerSnap_00");
							this.CharacterAnimation.CrossFade(this.IdleAnim);
							this.Snapping = true;
							this.CanMove = false;
						}
					}
				}

				if (this.Armor[20].activeInHierarchy)
				{
					if (this.Armor[20].transform.parent == ItemParent)
					{
						if (Input.GetButtonDown(InputNames.Xbox_X) || Input.GetButtonDown(InputNames.Xbox_Y))
						{
							this.CharacterAnimation["f02_nierAttackHard_00"].time = 0;
							this.CharacterAnimation.Play("f02_nierAttackHard_00");
							this.SithAttacking = true;
							this.CanMove = false;

							this.SithBeam[1].Damage = 20;
							this.NierDamage = 20;

							this.SithPrefix = "Hard";
							this.AttackPrefix = "nier";
						}
					}
				}
			}
		}
		//If CanMove is set to false...
		else
		{
			if (this.Chased && !this.Sprayed && !this.Attacking && !this.Dumping &&
				!this.StudentManager.PinningDown && !this.DelinquentFighting)
			{
				if (!this.ShoulderCamera.HeartbrokenCamera.activeInHierarchy)
				{
                    if (this.Pursuer != null)
                    {
					    this.targetRotation = Quaternion.LookRotation(
						    this.Pursuer.transform.position - this.transform.position);
					    this.transform.rotation = Quaternion.Slerp(
						    this.transform.rotation, this.targetRotation, Time.deltaTime * 10.0f);

					    this.CharacterAnimation.CrossFade(AnimNames.FemaleReadyToFight);

					    if (this.Dragging || this.Carrying)
					    {
						    this.EmptyHands();
					    }
                    }
                    else
                    {
                        this.PreparedForStruggle = false;
                        this.CanMove = true;
                        this.Chased = false;
                    }
                }
			}

			StopArmedAnim();

			if (this.Dumping)
			{
				this.targetRotation = Quaternion.LookRotation(
					this.Incinerator.transform.position - this.transform.position);
				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.targetRotation, Time.deltaTime * 10.0f);

				this.MoveTowardsTarget(this.Incinerator.transform.position + (Vector3.right * -2.0f));

				if (this.DumpTimer == 0.0f)
				{
					if (this.Carrying)
					{
						this.CharacterAnimation[AnimNames.FemaleCarryDisposeA].time = 2.50f;
					}
				}

				this.DumpTimer += Time.deltaTime;

				if (this.DumpTimer > 1.0f)
				{
					if (this.Ragdoll != null)
					{
						if (!this.Ragdoll.GetComponent<RagdollScript>().Dumped)
						{
							this.DumpRagdoll(RagdollDumpType.Incinerator);
						}
					}

					this.CharacterAnimation.CrossFade(AnimNames.FemaleCarryDisposeA);

                    if (this.CharacterAnimation[AnimNames.FemaleCarryDisposeA].time >=
						this.CharacterAnimation[AnimNames.FemaleCarryDisposeA].length)
					{
						this.Incinerator.Prompt.enabled = true;
						this.Incinerator.Ready = true;
						this.Incinerator.Open = false;

						this.Dragging = false;
						this.Dumping = false;
						this.CanMove = true;
						this.Ragdoll = null;

						this.StopCarrying();

						this.DumpTimer = 0.0f;
					}
				}
			}

			if (this.Chipping)
			{
				this.targetRotation = Quaternion.LookRotation(
					this.WoodChipper.gameObject.transform.position - this.transform.position);
				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.targetRotation, Time.deltaTime * 10.0f);

				this.MoveTowardsTarget(this.WoodChipper.DumpPoint.position);

				if (this.DumpTimer == 0.0f)
				{
					if (this.Carrying)
					{
						this.CharacterAnimation[AnimNames.FemaleCarryDisposeA].time = 2.50f;
					}
				}

				this.DumpTimer += Time.deltaTime;

				if (this.DumpTimer > 1.0f)
				{
					if (!this.Ragdoll.GetComponent<RagdollScript>().Dumped)
					{
						this.DumpRagdoll(RagdollDumpType.WoodChipper);
					}

					this.CharacterAnimation.CrossFade(AnimNames.FemaleCarryDisposeA);

					if (this.CharacterAnimation[AnimNames.FemaleCarryDisposeA].time >=
						this.CharacterAnimation[AnimNames.FemaleCarryDisposeA].length)
					{
						this.WoodChipper.Prompt.HideButton[0] = false;
						this.WoodChipper.Prompt.HideButton[3] = true;
						this.WoodChipper.Occupied = true;
						this.WoodChipper.Open = false;

						this.Dragging = false;
						this.Chipping = false;
						this.CanMove = true;
						this.Ragdoll = null;

						this.StopCarrying();

						this.DumpTimer = 0.0f;
					}
				}
			}

			if (this.TranquilHiding)
			{
				this.targetRotation = Quaternion.LookRotation(
					this.TranqCase.transform.position - this.transform.position);
				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.targetRotation, Time.deltaTime * 10.0f);

				this.MoveTowardsTarget(this.TranqCase.transform.position + (Vector3.right * 1.40f));

				if (this.DumpTimer == 0.0f)
				{
					if (this.Carrying)
					{
						this.CharacterAnimation[AnimNames.FemaleCarryDisposeA].time = 2.50f;
					}
				}

				this.DumpTimer += Time.deltaTime;

				if (this.DumpTimer > 1.0f)
				{
					if (!this.Ragdoll.GetComponent<RagdollScript>().Dumped)
					{
						this.DumpRagdoll(RagdollDumpType.TranqCase);
					}

					this.CharacterAnimation.CrossFade(AnimNames.FemaleCarryDisposeA);

					if (this.CharacterAnimation[AnimNames.FemaleCarryDisposeA].time >=
						this.CharacterAnimation[AnimNames.FemaleCarryDisposeA].length)
					{
						this.TranquilHiding = false;
						this.Dragging = false;
						this.Dumping = false;
						this.CanMove = true;
						this.Ragdoll = null;

						this.StopCarrying();

						this.DumpTimer = 0.0f;
					}
				}
			}

			if (this.Dipping)
			{
				if (this.Bucket != null)
				{
					this.targetRotation = Quaternion.LookRotation(new Vector3(
						this.Bucket.transform.position.x,
						this.transform.position.y,
						this.Bucket.transform.position.z) - this.transform.position);
					this.transform.rotation = Quaternion.Slerp(
						this.transform.rotation, this.targetRotation, Time.deltaTime * 10.0f);
				}

				this.CharacterAnimation.CrossFade(AnimNames.FemaleDipping);

				if (this.CharacterAnimation[AnimNames.FemaleDipping].time >=
					(this.CharacterAnimation[AnimNames.FemaleDipping].length * 0.50f))
				{
					this.Mop.Bleached = true;
					this.Mop.Sparkles.Play();

					if (this.Mop.Bloodiness > 0.0f)
					{
						if (this.Bucket != null)
						{
							this.Bucket.Bloodiness += this.Mop.Bloodiness / 2.0f;
							this.Bucket.UpdateAppearance = true;
						}
							
						this.Mop.Bloodiness = 0.0f;
						this.Mop.UpdateBlood();
					}
				}

				if (this.CharacterAnimation[AnimNames.FemaleDipping].time >=
					this.CharacterAnimation[AnimNames.FemaleDipping].length)
				{
					this.CharacterAnimation[AnimNames.FemaleDipping].time = 0.0f;
					this.Mop.Prompt.enabled = true;

					this.Dipping = false;
					this.CanMove = true;
				}
			}

			if (this.Pouring)
			{
				this.MoveTowardsTarget(this.Stool.position);
				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.Stool.rotation, 10.0f * Time.deltaTime);

				string dumpAnimString = "f02_bucketDump" + this.PourHeight + "_00";
				AnimationState dumpAnimState = this.CharacterAnimation[dumpAnimString];

				this.CharacterAnimation.CrossFade(dumpAnimString, 0.0f);

				if (dumpAnimState.time >= this.PourTime)
				{
					if (!this.PickUp.Bucket.Poured)
					{
						if (this.PickUp.Bucket.Gasoline)
						{
							// [af] This is the (unintuitive) Unity 5.3 way of changing particle color.
							ParticleSystem.MainModule pourEffectMain = this.PickUp.Bucket.PourEffect.main;
							pourEffectMain.startColor = new Color(1.0f, 1.0f, 0.0f, 0.50f);

							Instantiate(this.PickUp.Bucket.GasCollider,
								this.PickUp.Bucket.PourEffect.transform.position +
								(this.PourDistance * this.transform.forward),
								Quaternion.identity);
						}
						else
						{
							if (this.PickUp.Bucket.Bloodiness < 50.0f)
							{
								// [af] This is the (unintuitive) Unity 5.3 way of changing particle color.
								ParticleSystem.MainModule pourEffectMain = this.PickUp.Bucket.PourEffect.main;
								pourEffectMain.startColor = new Color(0.0f, 1.0f, 1.0f, 0.50f);

								Instantiate(this.PickUp.Bucket.WaterCollider,
									this.PickUp.Bucket.PourEffect.transform.position +
									(this.PourDistance * this.transform.forward),
									Quaternion.identity);
							}
							else
							{
								// [af] This is the (unintuitive) Unity 5.3 way of changing particle color.
								ParticleSystem.MainModule pourEffectMain = this.PickUp.Bucket.PourEffect.main;
								pourEffectMain.startColor = new Color(0.50f, 0.0f, 0.0f, 0.50f);

								Instantiate(this.PickUp.Bucket.BloodCollider,
									this.PickUp.Bucket.PourEffect.transform.position +
									(this.PourDistance * this.transform.forward),
									Quaternion.identity);
							}
						}

						this.PickUp.Bucket.PourEffect.Play();
						this.PickUp.Bucket.Poured = true;
						this.PickUp.Bucket.Empty();
					}
				}

				if (dumpAnimState.time >= dumpAnimState.length)
				{
					dumpAnimState.time = 0.0f;
					this.PickUp.Bucket.Poured = false;
					this.Pouring = false;
					this.CanMove = true;
				}
			}

			if (this.Laughing)
			{
				if (this.Hairstyles[14].activeInHierarchy)
				{
					this.LaughAnim = AnimNames.MaleStorePower20;
					this.LaughClip = this.ChargeUp;
				}

				if (this.Stand.Stand.activeInHierarchy)
				{
					this.LaughAnim = AnimNames.FemaleJojoAttack;
					this.LaughClip = this.YanYan;
				}
				else if (this.FlameDemonic)
				{
					float FlameV = Input.GetAxis("Vertical");
					float FlameH = Input.GetAxis("Horizontal");

					Vector3 FlameF = this.MainCamera.transform.TransformDirection(Vector3.forward);
					FlameF.y = 0.0f;
					FlameF = FlameF.normalized;

					Vector3 FlameRight = new Vector3(FlameF.z, 0.0f, -FlameF.x);
					Vector3 FlameDirection = (FlameH * FlameRight) + (FlameV * FlameF);

					if (FlameDirection != Vector3.zero)
					{
						this.targetRotation = Quaternion.LookRotation(FlameDirection);
						this.transform.rotation = Quaternion.Lerp(
							this.transform.rotation, this.targetRotation, Time.deltaTime * 10.0f);
					}

					this.LaughAnim = AnimNames.FemaleDemonAttack;

					this.CirnoTimer -= Time.deltaTime;

					if (this.CirnoTimer < 0.0f)
					{
						GameObject NewFireball1 = Instantiate(this.Fireball,
							this.RightHand.position, this.transform.rotation);
						NewFireball1.transform.localEulerAngles += new Vector3(
							Random.Range(0, 22.50f),
							Random.Range(-22.50f, 22.50f),
							Random.Range(-22.50f, 22.50f));

						GameObject NewFireball2 = Instantiate(this.Fireball,
							this.LeftHand.position, this.transform.rotation);
						NewFireball2.transform.localEulerAngles += new Vector3(
							Random.Range(0, 22.50f),
							Random.Range(-22.50f, 22.50f),
							Random.Range(-22.50f, 22.50f));

						this.CirnoTimer = 0.10f;
					}
				}
				else if (this.CirnoHair.activeInHierarchy)
				{
					float CirnoV = Input.GetAxis("Vertical");
					float CirnoH = Input.GetAxis("Horizontal");

					Vector3 CirnoF = this.MainCamera.transform.TransformDirection(Vector3.forward);
					CirnoF.y = 0.0f;
					CirnoF = CirnoF.normalized;

					Vector3 CirnoRight = new Vector3(CirnoF.z, 0.0f, -CirnoF.x);
					Vector3 CirnoDirection = (CirnoH * CirnoRight) + (CirnoV * CirnoF);

					if (CirnoDirection != Vector3.zero)
					{
						this.targetRotation = Quaternion.LookRotation(CirnoDirection);
						this.transform.rotation = Quaternion.Lerp(
							this.transform.rotation, this.targetRotation, Time.deltaTime * 10.0f);
					}

					this.LaughAnim = AnimNames.FemaleCirnoAttack;

					this.CirnoTimer -= Time.deltaTime;

					if (this.CirnoTimer < 0.0f)
					{
						GameObject NewIceAttack = Instantiate(this.CirnoIceAttack,
							this.transform.position + (this.transform.up * 1.40f),
							this.transform.rotation);
						NewIceAttack.transform.localEulerAngles += new Vector3(
							Random.Range(-5.0f, 5.0f),
							Random.Range(-5.0f, 5.0f),
							Random.Range(-5.0f, 5.0f));
						MyAudio.PlayOneShot(this.CirnoIceClip);

						this.CirnoTimer = 0.10f;
					}
				}
				else if (this.TornadoHair.activeInHierarchy)
				{
					this.LaughAnim = AnimNames.FemaleTornadoAttack;

					this.CirnoTimer -= Time.deltaTime;

					if (this.CirnoTimer < 0.0f)
					{
						GameObject NewTornado = Instantiate(this.TornadoAttack, (this.transform.forward * 5) + new Vector3(
							this.transform.position.x + Random.Range(-5.0f, 5.0f),
							this.transform.position.y,
							this.transform.position.z + Random.Range(-5.0f, 5.0f)),
							this.transform.rotation);

						while (Vector3.Distance(this.transform.position, NewTornado.transform.position) < 1.0f)
						{
							NewTornado.transform.position = (this.transform.forward * 5) + new Vector3(
								this.transform.position.x + Random.Range(-5.0f, 5.0f),
								this.transform.position.y,
								this.transform.position.z + Random.Range(-5.0f, 5.0f));
						}

						this.CirnoTimer = 0.10f;
					}
				}
				else if (this.BladeHair.activeInHierarchy)
				{
					this.LaughAnim = AnimNames.FemaleSpin;

					this.transform.localEulerAngles = new Vector3(
						this.transform.localEulerAngles.x,
						this.transform.localEulerAngles.y + (Time.deltaTime * 360.0f * 2.0f),
						this.transform.localEulerAngles.z);

					this.BladeHairCollider1.enabled = true;
					this.BladeHairCollider2.enabled = true;
				}
				else if (this.BanchoActive)
				{
					this.BanchoFlurry.MyCollider.enabled = true;
					this.LaughAnim = "f02_banchoFlurry_00";
				}
				else
				{
					if (MyAudio.clip != this.LaughClip)
					{
						MyAudio.clip = this.LaughClip;
						MyAudio.time = 0.0f;

						MyAudio.Play();
					}
				}

				this.CharacterAnimation.CrossFade(this.LaughAnim);

				if (Input.GetButtonDown(InputNames.Xbox_RB))
				{
					this.LaughIntensity += 1.0f;

					if (this.LaughIntensity <= 5.0f)
					{
						this.LaughAnim = AnimNames.FemaleLaugh01;
						this.LaughClip = this.Laugh1;
						this.LaughTimer = 0.50f;
					}
					else if (this.LaughIntensity <= 10.0f)
					{
						this.LaughAnim = AnimNames.FemaleLaugh02;
						this.LaughClip = this.Laugh2;
						this.LaughTimer = 1.0f;
					}
					else if (this.LaughIntensity <= 15.0f)
					{
						this.LaughAnim = AnimNames.FemaleLaugh03;
						this.LaughClip = this.Laugh3;
						this.LaughTimer = 1.50f;
					}
					else if (this.LaughIntensity <= 20.0f)
					{
						GameObject Disc = Instantiate(this.AlarmDisc,
							this.transform.position + Vector3.up, Quaternion.identity);
						Disc.GetComponent<AlarmDiscScript>().NoScream = true;

						this.LaughAnim = AnimNames.FemaleLaugh04;
						this.LaughClip = this.Laugh4;
						this.LaughTimer = 2.0f;
					}
					else
					{
						GameObject Disc = Instantiate(this.AlarmDisc,
							this.transform.position + Vector3.up, Quaternion.identity);
						Disc.GetComponent<AlarmDiscScript>().NoScream = true;

						this.LaughAnim = AnimNames.FemaleLaugh04;
						this.LaughIntensity = 20.0f;
						this.LaughTimer = 2.0f;
					}
				}

				if (this.LaughIntensity > 15.0f)
				{
					this.Sanity += Time.deltaTime * 10.0f;
				}

				this.LaughTimer -= Time.deltaTime;

				if (this.LaughTimer <= 0.0f)
				{
					this.StopLaughing();
				}
			}

			if (this.TimeSkipping)
			{
				this.transform.position = new Vector3(
					this.transform.position.x,
					this.TimeSkipHeight,
					this.transform.position.z);

				this.CharacterAnimation.CrossFade(AnimNames.FemaleTimeSkip);
				this.MyController.Move(this.transform.up * 0.00010f);
				this.Sanity += Time.deltaTime * 0.17f;
			}

			if (this.DumpsterGrabbing)
			{
				if ((Input.GetAxis("Horizontal") > 0.50f) ||
					(Input.GetAxis("DpadX") > 0.50f) ||
					Input.GetKey("right"))
				{
					// [af] Replaced if/else statement with ternary expression.
					this.CharacterAnimation.CrossFade((this.DumpsterHandle.Direction == -1.0f) ?
						AnimNames.FemaleDumpsterPull : AnimNames.FemaleDumpsterPush);
				}
				else if ((Input.GetAxis("Horizontal") < -0.50f) ||
						(Input.GetAxis("DpadX") < -0.50f) ||
						Input.GetKey("left"))
				{
					// [af] Replaced if/else statement with ternary expression.
					this.CharacterAnimation.CrossFade((this.DumpsterHandle.Direction == -1.0f) ?
						AnimNames.FemaleDumpsterPush : AnimNames.FemaleDumpsterPull);
				}
				else
				{
					this.CharacterAnimation.CrossFade(AnimNames.FemaleDumpsterGrab);
				}
			}

			if (this.Stripping)
			{
				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.StudentManager.YandereStripSpot.rotation, 10.0f * Time.deltaTime);

				if (this.CharacterAnimation[AnimNames.FemaleStripping].time >=
					this.CharacterAnimation[AnimNames.FemaleStripping].length)
				{
					this.Stripping = false;
					this.CanMove = true;

					this.MyLocker.UpdateSchoolwear();
				}
			}

			if (this.Bathing)
			{
				this.MoveTowardsTarget(this.YandereShower.BatheSpot.position);

				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.YandereShower.BatheSpot.rotation, 10.0f * Time.deltaTime);

				this.CharacterAnimation.CrossFade(this.IdleAnim);

				if (this.YandereShower.Timer < 1)
				{
					this.Bloodiness = 0.0f;
					this.Bathing = false;
					this.CanMove = true;
				}
			}

			if (this.Degloving)
			{
				this.CharacterAnimation.CrossFade(AnimNames.FemaleRemoveGloves);

				if (this.CharacterAnimation[AnimNames.FemaleRemoveGloves].time >=
					this.CharacterAnimation[AnimNames.FemaleRemoveGloves].length)
				{
					this.Gloves.GetComponent<Rigidbody>().isKinematic = false;
					this.Gloves.transform.parent = null;

					this.GloveAttacher.newRenderer.enabled = false;

					// [af] Added "gameObject" for C# compatibility.
					this.Gloves.gameObject.SetActive(true);

					this.Degloving = false;
					this.CanMove = true;
					this.Gloved = false;
					this.Gloves = null;
					this.SetUniform();

					this.GloveBlood = 0;

					Debug.Log("Gloves removed.");
				}
				else
				{
					if (this.Chased || this.Chasers > 0 || this.Noticed)
					{
						this.Degloving = false;
						this.GloveTimer = 0.0f;

						if (!this.Noticed)
						{
							this.CanMove = true;
						}
					}
					else
					{
						if (this.InputDevice.Type == InputDeviceType.Gamepad)
						{
							if (Input.GetAxis("DpadY") > -0.50f)
							{
								this.Degloving = false;
								this.CanMove = true;
								this.GloveTimer = 0.0f;
							}
						}
						else
						{
							if (Input.GetKeyUp(KeyCode.Alpha1))
							{
								this.Degloving = false;
								this.CanMove = true;
								this.GloveTimer = 0.0f;
							}
						}
					}
				}
			}

			if (this.Struggling)
			{
				if (!this.Won && !this.Lost)
				{
					// [af] Replaced if/else statement with ternary expression.
					this.CharacterAnimation.CrossFade(this.TargetStudent.Teacher ?
						AnimNames.FemaleTeacherStruggleA : AnimNames.FemaleStruggleA);

					this.targetRotation = Quaternion.LookRotation(
						this.TargetStudent.transform.position - this.transform.position);
					this.transform.rotation = Quaternion.Slerp(
						this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);
				}
				else
				{
					//if (this.ShoulderCamera.Timer == 0)
					//{
						if (this.Won)
						{
							if (!this.TargetStudent.Teacher)
							{
								this.CharacterAnimation.CrossFade(AnimNames.FemaleStruggleWinA);

								if (this.CharacterAnimation[AnimNames.FemaleStruggleWinA].time >
									(this.CharacterAnimation[AnimNames.FemaleStruggleWinA].length - 1.0f))
								{
									this.EquippedWeapon.transform.localEulerAngles = Vector3.Lerp(
										this.EquippedWeapon.transform.localEulerAngles,
										Vector3.zero,
										Time.deltaTime * 3.33333f);
								}
							}
							else
							{
                                Debug.Log(this.TargetStudent.Name + " is being instructed to perform their ''losing struggle'' animation.");

								this.CharacterAnimation.CrossFade(AnimNames.FemaleTeacherStruggleWinA);

                                this.TargetStudent.CharacterAnimation.CrossFade(this.TargetStudent.StruggleWonAnim);

								this.EquippedWeapon.transform.localEulerAngles = Vector3.Lerp(
									this.EquippedWeapon.transform.localEulerAngles,
									Vector3.zero,
									Time.deltaTime);
							}

							if (this.StrugglePhase == 0)
							{
								if (!this.TargetStudent.Teacher &&
									(this.CharacterAnimation[AnimNames.FemaleStruggleWinA].time > 1.30f) ||
									this.TargetStudent.Teacher &&
									(this.CharacterAnimation[AnimNames.FemaleTeacherStruggleWinA].time > 0.80f))
								{
									Debug.Log("Yandere-chan just killed " + this.TargetStudent.Name + " as a result of winning a struggling against them.");

									this.TargetStudent.DeathCause = this.EquippedWeapon.WeaponID;

									// [af] Replaced if/else statement with ternary expression.
									Instantiate(this.TargetStudent.StabBloodEffect,
										this.TargetStudent.Teacher ? this.EquippedWeapon.transform.position : this.TargetStudent.Head.position,
										Quaternion.identity);

									this.Bloodiness += 20.0f;
									this.Sanity -= 20.0f * this.Numbness;

									this.StainWeapon();

									this.StrugglePhase++;
								}
							}
							else if (this.StrugglePhase == 1)
							{
								if (this.TargetStudent.Teacher &&
									(this.CharacterAnimation[AnimNames.FemaleTeacherStruggleWinA].time > 1.30f))
								{
									Instantiate(this.TargetStudent.StabBloodEffect,
										this.EquippedWeapon.transform.position, Quaternion.identity);
									this.StrugglePhase++;
								}
							}
							else if (this.StrugglePhase == 2)
							{
								if (this.TargetStudent.Teacher &&
									(this.CharacterAnimation[AnimNames.FemaleTeacherStruggleWinA].time > 2.10f))
								{
									Instantiate(this.TargetStudent.StabBloodEffect,
										this.EquippedWeapon.transform.position, Quaternion.identity);
									this.StrugglePhase++;
								}
							}

							if (!this.TargetStudent.Teacher &&
								(this.CharacterAnimation[AnimNames.FemaleStruggleWinA].time > this.CharacterAnimation[AnimNames.FemaleStruggleWinA].length) ||
								this.TargetStudent.Teacher &&
								(this.CharacterAnimation[AnimNames.FemaleTeacherStruggleWinA].time > this.CharacterAnimation[AnimNames.FemaleTeacherStruggleWinA].length))
							{
								this.MyController.radius = 0.20f;

								this.CharacterAnimation.CrossFade(this.IdleAnim);

								this.ShoulderCamera.Struggle = false;
								this.Struggling = false;
								this.StrugglePhase = 0;

								if (this.TargetStudent == this.Pursuer)
								{
									this.Pursuer = null;
									this.Chased = false;
								}

								this.TargetStudent.BecomeRagdoll();
								this.TargetStudent.DeathType = DeathType.Weapon;

								this.SeenByAuthority = false;
							}
						}
						else if (this.Lost)
						{
							// [af] Replaced if/else statement with ternary expression.
							this.CharacterAnimation.CrossFade(this.TargetStudent.Teacher ?
								AnimNames.FemaleTeacherStruggleLoseA : AnimNames.FemaleStruggleLoseA);
						}
					//}
				}
			}

			if (this.ClubActivity)
			{
				if (this.Club == ClubType.Drama)
				{
					this.CharacterAnimation.Play("f02_performing_00");
				}
				else if (this.Club == ClubType.Art)
				{
					this.CharacterAnimation.Play("f02_painting_00");
				}
				else if (this.Club == ClubType.MartialArts)
				{
					this.CharacterAnimation.Play(AnimNames.FemaleKick23);

					if (this.CharacterAnimation[AnimNames.FemaleKick23].time >=
						this.CharacterAnimation[AnimNames.FemaleKick23].length)
					{
						this.CharacterAnimation[AnimNames.FemaleKick23].time = 0.0f;
					}
				}
				else if (this.Club == ClubType.Photography)
				{
					this.CharacterAnimation.Play("f02_sit_00");
				}
				else if (this.Club == ClubType.Gaming)
				{
					this.CharacterAnimation.Play("f02_playingGames_00");
				}
			}

			if (this.Possessed)
			{
				this.CharacterAnimation.CrossFade(AnimNames.FemalePossessionPose);
			}

			if (this.Lifting)
			{
				if (!this.HeavyWeight)
				{
					if (this.CharacterAnimation[AnimNames.FemaleCarryLiftA].time >=
						this.CharacterAnimation[AnimNames.FemaleCarryLiftA].length)
					{
						this.IdleAnim = this.CarryIdleAnim;
						this.WalkAnim = this.CarryWalkAnim;
						this.RunAnim = this.CarryRunAnim;

						this.CanMove = true;
						this.Carrying = true;
						this.Lifting = false;
					}
				}
				else
				{
					if (this.CharacterAnimation[AnimNames.FemaleHeavyWeightLift].time >=
						this.CharacterAnimation[AnimNames.FemaleHeavyWeightLift].length)
					{
						this.CharacterAnimation[this.CarryAnims[0]].weight = 1.0f;

						this.IdleAnim = this.HeavyIdleAnim;
						this.WalkAnim = this.HeavyWalkAnim;
						this.RunAnim = this.CarryRunAnim;

						this.CanMove = true;
						this.Lifting = false;
					}
				}
			}

			if (this.Dropping)
			{
				this.targetRotation = Quaternion.LookRotation(
					(this.DropSpot.position + this.DropSpot.forward) - this.transform.position);
				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.targetRotation, Time.deltaTime * 10.0f);

				this.MoveTowardsTarget(this.DropSpot.position);

				if (this.Ragdoll != null && this.CurrentRagdoll == null)
				{
					CurrentRagdoll = this.Ragdoll.GetComponent<RagdollScript>();
				}

				if (this.DumpTimer == 0.0f)
				{
					if (this.Carrying)
					{
						CurrentRagdoll.CharacterAnimation[CurrentRagdoll.DumpedAnim].time = 2.50f;
						this.CharacterAnimation[AnimNames.FemaleCarryDisposeA].time = 2.50f;
					}
				}

				this.DumpTimer += Time.deltaTime;

				if (this.DumpTimer > 1.0f)
				{
                    this.FollowHips = true;

					if (this.Ragdoll != null)
					{
						CurrentRagdoll.PelvisRoot.localEulerAngles = new Vector3(
							CurrentRagdoll.PelvisRoot.localEulerAngles.x,
							0.0f,
							CurrentRagdoll.PelvisRoot.localEulerAngles.z);

						CurrentRagdoll.PelvisRoot.localPosition = new Vector3(
							CurrentRagdoll.PelvisRoot.localPosition.x,
							CurrentRagdoll.PelvisRoot.localPosition.y,
							0.0f);
					}

					this.CameraTarget.position = Vector3.MoveTowards(this.CameraTarget.position,
						new Vector3(this.Hips.position.x, this.transform.position.y + 1.0f, this.Hips.position.z),
						Time.deltaTime * 10.0f);

					if (this.CharacterAnimation[AnimNames.FemaleCarryDisposeA].time >= 4.50f)
					{
						this.StopCarrying();
					}
					else
					{
						if (CurrentRagdoll.StopAnimation)
						{
							CurrentRagdoll.StopAnimation = false;

							// [af] Converted while loop to for loop.
							for (this.ID = 0; this.ID < CurrentRagdoll.AllRigidbodies.Length; this.ID++)
							{
								CurrentRagdoll.AllRigidbodies[this.ID].isKinematic = true;
							}
						}

						this.CharacterAnimation.CrossFade(AnimNames.FemaleCarryDisposeA);
						CurrentRagdoll.CharacterAnimation.CrossFade(CurrentRagdoll.DumpedAnim);

						this.Ragdoll.transform.position = this.transform.position;
						this.Ragdoll.transform.eulerAngles = this.transform.eulerAngles;
					}

					if (this.CharacterAnimation[AnimNames.FemaleCarryDisposeA].time >=
						this.CharacterAnimation[AnimNames.FemaleCarryDisposeA].length)
					{
						this.CameraTarget.localPosition = new Vector3(0.0f, 1.0f, 0.0f);
                        this.FollowHips = false;
                        this.Dropping = false;
						this.CanMove = true;

						this.DumpTimer = 0.0f;
					}
				}
			}

			if (this.Dismembering)
			{
				if (this.CharacterAnimation[AnimNames.FemaleDismember].time >=
					this.CharacterAnimation[AnimNames.FemaleDismember].length)
				{
					this.Ragdoll.GetComponent<RagdollScript>().Dismember();
					this.RPGCamera.enabled = true;
					this.TargetStudent = null;
					this.Dismembering = false;
					this.CanMove = true;
					this.Ragdoll = null;
				}
			}

			if (this.Shoved)
			{
				if (this.CharacterAnimation[AnimNames.FemaleShoveA].time >=
					this.CharacterAnimation[AnimNames.FemaleShoveA].length)
				{
					this.CharacterAnimation.CrossFade(this.IdleAnim);
					this.Shoved = false;

					if (!this.CannotRecover)
					{
						this.CanMove = true;
					}
				}
				else
				{
					if (this.CharacterAnimation[AnimNames.FemaleShoveA].time < .66666f)
					{
						this.MyController.Move(this.transform.forward * -1 * this.ShoveSpeed * Time.deltaTime);
						this.MyController.Move(Physics.gravity * 0.10f);

						if (this.ShoveSpeed > 0)
						{
							this.ShoveSpeed = Mathf.MoveTowards(this.ShoveSpeed, 0, Time.deltaTime * 3);
						}
					}
				}
			}

			if (this.Attacked)
			{
				if (this.CharacterAnimation[AnimNames.FemaleSwingB].time >=
					this.CharacterAnimation[AnimNames.FemaleSwingB].length)
				{
					this.ShoulderCamera.HeartbrokenCamera.SetActive(true);
					this.enabled = false;
				}
			}

			if (this.Hiding)
			{
				if (!this.Exiting)
				{
					this.transform.rotation = Quaternion.Slerp(
						this.transform.rotation, this.HidingSpot.rotation, Time.deltaTime * 10.0f);
					this.MoveTowardsTarget(this.HidingSpot.position);
					this.CharacterAnimation.CrossFade(this.HideAnim);

					if (Input.GetButtonDown(InputNames.Xbox_B))
					{
						this.PromptBar.ClearButtons();
						this.PromptBar.Show = false;
						this.Exiting = true;
					}
				}
				else
				{
					this.MoveTowardsTarget(this.ExitSpot.position);
					this.CharacterAnimation.CrossFade(this.IdleAnim);

					this.ExitTimer += Time.deltaTime;

					if ((this.ExitTimer > 1.0f) ||
						(Vector3.Distance(this.transform.position, this.ExitSpot.position) < 0.10f))
					{
						this.MyController.center = new Vector3(
							this.MyController.center.x,
							0.875f,
							this.MyController.center.z);

						this.MyController.radius = 0.20f;
						this.MyController.height = 1.55f;
						this.ExitTimer = 0.0f;

						this.Exiting = false;
						this.CanMove = true;
						this.Hiding = false;
					}
				}
			}

			/*
			if (this.Tripping)
			{
				if (this.CharacterAnimation[AnimNames.FemaleBucketTrip].time >=
					this.CharacterAnimation[AnimNames.FemaleBucketTrip].length)
				{
					this.CharacterAnimation[AnimNames.FemaleBucketTrip].speed = 1.0f;
					this.Tripping = false;
					this.CanMove = true;
				}
				else if (this.CharacterAnimation[AnimNames.FemaleBucketTrip].time < 0.50f)
				{
					this.MyController.Move(this.transform.forward * (Time.deltaTime * 2.0f));

					if (this.CharacterAnimation[AnimNames.FemaleBucketTrip].time > (1.0f / 3.0f))
					{
						if (this.CharacterAnimation[AnimNames.FemaleBucketTrip].speed == 1.0f)
						{
							this.CharacterAnimation[AnimNames.FemaleBucketTrip].speed += 0.000001f;

							AudioSource.PlayClipAtPoint(this.Thud, this.transform.position);
						}
					}
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.CharacterAnimation[AnimNames.FemaleBucketTrip].speed += 0.10f;
					}
				}
			}
			*/

			if (this.BucketDropping)
			{
				this.targetRotation = Quaternion.LookRotation(
					(this.DropSpot.position + this.DropSpot.forward) - this.transform.position);
				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.targetRotation, Time.deltaTime * 10.0f);

				this.MoveTowardsTarget(this.DropSpot.position);

				if (this.CharacterAnimation[AnimNames.FemaleBucketDrop].time >=
					this.CharacterAnimation[AnimNames.FemaleBucketDrop].length)
				{
					this.MyController.radius = 0.20f;
					this.BucketDropping = false;
					this.CanMove = true;
				}
				else if (this.CharacterAnimation[AnimNames.FemaleBucketDrop].time >= 1.0f)
				{
					if (this.PickUp != null)
					{
						GameObjectUtils.SetLayerRecursively(this.PickUp.Bucket.gameObject, 0);

						this.PickUp.Bucket.UpdateAppearance = true;
						this.PickUp.Bucket.Dropped = true;
						this.EmptyHands();
					}
				}
			}

			if (this.Flicking)
			{
				if (this.CharacterAnimation[AnimNames.FemaleFlickingMatch].time >=
					this.CharacterAnimation[AnimNames.FemaleFlickingMatch].length)
				{
					this.PickUp.GetComponent<MatchboxScript>().Prompt.enabled = true;

					this.Arc.SetActive(true);
					this.Flicking = false;
					this.CanMove = true;
				}
				else if (this.CharacterAnimation[AnimNames.FemaleFlickingMatch].time > 1.0f)
				{
					if (this.Match != null)
					{
						Rigidbody matchRigidBody = this.Match.GetComponent<Rigidbody>();
						matchRigidBody.isKinematic = false;
						matchRigidBody.useGravity = true;

						matchRigidBody.AddRelativeForce(Vector3.right * 250.0f);

						// [af] Commented in JS code.
						//Match.rigidbody.AddRelativeForce(Vector3.up * 100);

						this.Match.transform.parent = null;
						this.Match = null;
					}
				}
			}

			if (this.Rummaging)
			{
				this.MoveTowardsTarget(this.RummageSpot.Target.position);
				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.RummageSpot.Target.rotation, Time.deltaTime * 10.0f);

				this.RummageTimer += Time.deltaTime;

				this.ProgressBar.transform.localScale = new Vector3(
					this.RummageTimer / 10.0f,
					this.ProgressBar.transform.localScale.y,
					this.ProgressBar.transform.localScale.z);

				if (this.RummageTimer > 10.0f)
				{
					this.RummageSpot.GetReward();

					// [af] Added "gameObject" for C# compatibility.
					this.ProgressBar.transform.parent.gameObject.SetActive(false);

					this.RummageSpot = null;
					this.Rummaging = false;
					this.RummageTimer = 0.0f;
					this.CanMove = true;
				}
			}

			if (this.Digging)
			{
				if (this.DigPhase == 1)
				{
					if (this.CharacterAnimation[AnimNames.FemaleShovelDig].time >= (50.0f / 30.0f))
					{
						MyAudio.volume = 1.0f;
						MyAudio.clip = this.Dig;
						MyAudio.Play();
						this.DigPhase++;
					}
				}
				else if (this.DigPhase == 2)
				{
					if (this.CharacterAnimation[AnimNames.FemaleShovelDig].time >= (105.0f / 30.0f))
					{
						MyAudio.volume = 1.0f;
						MyAudio.Play();
						this.DigPhase++;
					}
				}
				else if (this.DigPhase == 3)
				{
					if (this.CharacterAnimation[AnimNames.FemaleShovelDig].time >= (170.0f / 30.0f))
					{
						MyAudio.volume = 1.0f;
						MyAudio.Play();
						this.DigPhase++;
					}
				}
				else if (this.DigPhase == 4)
				{
					if (this.CharacterAnimation[AnimNames.FemaleShovelDig].time >=
						this.CharacterAnimation[AnimNames.FemaleShovelDig].length)
					{
						this.EquippedWeapon.gameObject.SetActive(true);
						this.FloatingShovel.SetActive(false);
						this.RPGCamera.enabled = true;
						this.Digging = false;
						this.CanMove = true;
					}
				}
			}

			if (this.Burying)
			{
				if (this.DigPhase == 1)
				{
					if (this.CharacterAnimation[AnimNames.FemaleShovelBury].time >= (65.0f / 30.0f))
					{
						MyAudio.volume = 1.0f;
						MyAudio.clip = this.Dig;
						MyAudio.Play();
						this.DigPhase++;
					}
				}
				else if (this.DigPhase == 2)
				{
					if (this.CharacterAnimation[AnimNames.FemaleShovelBury].time >= (140.0f / 30.0f))
					{
						MyAudio.volume = 1.0f;
						MyAudio.Play();
						this.DigPhase++;
					}
				}
				else if (this.CharacterAnimation[AnimNames.FemaleShovelBury].time >=
					this.CharacterAnimation[AnimNames.FemaleShovelBury].length)
				{
					this.EquippedWeapon.gameObject.SetActive(true);
					this.FloatingShovel.SetActive(false);
					this.RPGCamera.enabled = true;
					this.Burying = false;
					this.CanMove = true;
				}
			}

			if (this.Pickpocketing)
			{
				if (!this.Noticed)
				{
					if (this.Caught)
					{
						this.CaughtTimer += Time.deltaTime;

						if (this.CaughtTimer > 1)
						{
							if (!this.CannotRecover)
							{
								this.CanMove = true;
							}

							this.Pickpocketing = false;
							this.CaughtTimer = 0;
							this.Caught = false;
						}
					}
				}
			}

			if (this.Sprayed)
			{
				if (this.SprayPhase == 0)
				{
					if (this.CharacterAnimation["f02_sprayed_00"].time > .66666)
					{
						Blur.enabled = true;
						Blur.blurSize += Time.deltaTime;

						if (Blur.blurSize > Blur.blurIterations)
						{
							Blur.blurIterations++;
						}
					}

					if (CharacterAnimation["f02_sprayed_00"].time > 5)
					{
						this.Police.Darkness.enabled = true;

						this.Police.Darkness.color = new Color(0, 0, 0,
							Mathf.MoveTowards(this.Police.Darkness.color.a, 1, Time.deltaTime));

						if (this.Police.Darkness.color.a == 1)
						{
							this.SprayTimer += Time.deltaTime;

							if (this.SprayTimer > 1)
							{
								this.CharacterAnimation.Play("f02_tied_00");
								this.RPGCamera.enabled = false;
								this.ZipTie[0].SetActive(true);
								this.ZipTie[1].SetActive(true);
								this.Blur.enabled = false;
								this.SprayTimer = 0;
								this.SprayPhase++;
							}
						}
					}
				}
				else if (this.SprayPhase == 1)
				{
					this.Police.Darkness.color = new Color(0, 0, 0,
						Mathf.MoveTowards(this.Police.Darkness.color.a, 0, Time.deltaTime));

					if (this.Police.Darkness.color.a == 0)
					{
						this.SprayTimer += Time.deltaTime;

						if (this.SprayTimer > 1)
						{
							this.ShoulderCamera.HeartbrokenCamera.SetActive(true);
							this.HeartCamera.gameObject.SetActive(false);
							this.HUD.alpha = 0;
							this.SprayPhase++;
						}
					}
				}
			}

			if (this.CleaningWeapon)
			{
				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.Target.rotation, Time.deltaTime * 10.0f);

				this.MoveTowardsTarget(this.Target.position);

				if (this.CharacterAnimation["f02_cleaningWeapon_00"].time >= this.CharacterAnimation["f02_cleaningWeapon_00"].length )
				{
					this.EquippedWeapon.Blood.enabled = false;
					this.EquippedWeapon.Bloody = false;
                    this.EquippedWeapon.SuspicionCheck();

                    if (this.Gloved)
					{
						this.EquippedWeapon.FingerprintID = 0;
					}

					this.CleaningWeapon = false;
                    this.Police.MurderWeapons--;
					this.CanMove = true;
				}
			}

			if (this.CrushingPhone)
			{
				this.CharacterAnimation.CrossFade("f02_phoneCrush_00");

				this.targetRotation = Quaternion.LookRotation(new Vector3(
					this.PhoneToCrush.transform.position.x,
					this.transform.position.y,
					this.PhoneToCrush.transform.position.z) - this.transform.position);

				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.targetRotation, Time.deltaTime * 10.0f);

				this.MoveTowardsTarget(this.PhoneToCrush.PhoneCrushingSpot.position);

				if (this.CharacterAnimation["f02_phoneCrush_00"].time >= .5f)
				{
					if (PhoneToCrush.enabled)
					{
						PhoneToCrush.transform.localEulerAngles = new Vector3(
							PhoneToCrush.transform.localEulerAngles.x,
							PhoneToCrush.transform.localEulerAngles.y,
							0);

						Instantiate(PhoneToCrush.PhoneSmash, PhoneToCrush.transform.position, Quaternion.identity);

						Police.PhotoEvidence--;

						PhoneToCrush.MyRenderer.material.mainTexture = PhoneToCrush.SmashedTexture;
						PhoneToCrush.MyMesh.mesh = PhoneToCrush.SmashedMesh;

						PhoneToCrush.Prompt.Hide();
						PhoneToCrush.Prompt.enabled = false;
						PhoneToCrush.enabled = false;
					}
				}

				if (this.CharacterAnimation["f02_phoneCrush_00"].time >= this.CharacterAnimation["f02_phoneCrush_00"].length)
				{
					this.CrushingPhone = false;
					this.CanMove = true;
				}
			}

			if (this.SubtleStabbing)
			{
				if (this.CharacterAnimation["f02_subtleStab_00"].time < .5f)
				{
					this.CharacterAnimation.CrossFade("f02_subtleStab_00");

					this.targetRotation = Quaternion.LookRotation(new Vector3(
						this.TargetStudent.transform.position.x,
						this.transform.position.y,
						this.TargetStudent.transform.position.z) - this.transform.position);

					this.transform.rotation = Quaternion.Slerp(
						this.transform.rotation, this.targetRotation, Time.deltaTime * 10.0f);

					this.MoveTowardsTarget(this.TargetStudent.transform.position + (this.TargetStudent.transform.forward * -1));
				}
				else
				{
					if (this.TargetStudent.Strength > 0)
					{
						this.TargetStudent.Strength = 0;
						this.TargetStudent.Hunter.MurderSuicidePhase = 0;
						this.TargetStudent.Hunter.AttackWillFail = false;
						this.TargetStudent.Hunter.Pathfinding.canMove = true;

						this.TargetStudent.CharacterAnimation["f02_murderSuicide_01"].time = 1.5f;
						this.TargetStudent.Hunter.CharacterAnimation["f02_murderSuicide_00"].time = 1.5f;

						Debug.Log("Making the hunter's attack a success!");
					}
				}

				if (this.CharacterAnimation["f02_subtleStab_00"].time >= this.CharacterAnimation["f02_subtleStab_00"].length)
				{
					this.SubtleStabbing = false;
					this.CanMove = true;
				}
			}

			if (this.CanMoveTimer > 0)
			{
				this.CanMoveTimer = Mathf.MoveTowards(this.CanMoveTimer, 0, Time.deltaTime);

				if (this.CanMoveTimer == 0)
				{
					this.CanMove = true;
				}
			}

			if (this.Egg)
			{
				if (this.Punching)
				{
					if (this.FalconHelmet.activeInHierarchy)
					{
						if ((this.CharacterAnimation[AnimNames.FemaleFalconPunch].time >= 1.0f) &&
							(this.CharacterAnimation[AnimNames.FemaleFalconPunch].time <= 1.25f))
						{
							this.FalconSpeed = Mathf.MoveTowards(
								this.FalconSpeed, 2.50f, Time.deltaTime * 2.50f);
						}
						else if ((this.CharacterAnimation[AnimNames.FemaleFalconPunch].time >= 1.25f) &&
							(this.CharacterAnimation[AnimNames.FemaleFalconPunch].time <= 1.50f))
						{
							this.FalconSpeed = Mathf.MoveTowards(
								this.FalconSpeed, 0.0f, Time.deltaTime * 2.50f);
						}

						if ((this.CharacterAnimation[AnimNames.FemaleFalconPunch].time >= 1.0f) &&
							(this.CharacterAnimation[AnimNames.FemaleFalconPunch].time <= 1.50f))
						{
							if (this.NewFalconPunch == null)
							{
								this.NewFalconPunch = Instantiate(this.FalconPunch);
								this.NewFalconPunch.transform.parent = this.ItemParent;
								this.NewFalconPunch.transform.localPosition = Vector3.zero;
							}

							this.MyController.Move(this.transform.forward * this.FalconSpeed);
						}

						if (this.CharacterAnimation[AnimNames.FemaleFalconPunch].time >=
							this.CharacterAnimation[AnimNames.FemaleFalconPunch].length)
						{
							this.NewFalconPunch = null;
							this.Punching = false;
							this.CanMove = true;
						}
					}
					else
					{
						if ((this.CharacterAnimation[AnimNames.FemaleOnePunch].time >= 0.833333f) &&
							(this.CharacterAnimation[AnimNames.FemaleOnePunch].time <= 1.0f))
						{
							if (this.NewOnePunch == null)
							{
								this.NewOnePunch = Instantiate(this.OnePunch);
								this.NewOnePunch.transform.parent = this.ItemParent;
								this.NewOnePunch.transform.localPosition = Vector3.zero;
							}
						}

						if (this.CharacterAnimation[AnimNames.FemaleOnePunch].time >= 2.0f)
						{
							this.NewOnePunch = null;
							this.Punching = false;
							this.CanMove = true;
						}
					}
				}

				if (this.PK)
				{
					if (Input.GetAxis("Vertical") > 0.50f)
					{
						this.GoToPKDir(PKDirType.Up, AnimNames.FemaleSansUp, 
							new Vector3(0.0f, 3.0f, 2.0f));
					}
					else if (Input.GetAxis("Vertical") < -0.50f)
					{
						this.GoToPKDir(PKDirType.Down, AnimNames.FemaleSansDown, 
							new Vector3(0.0f, 0.0f, 2.0f));
					}
					else if (Input.GetAxis("Horizontal") > 0.50f)
					{
						this.GoToPKDir(PKDirType.Right, AnimNames.FemaleSansRight, 
							new Vector3(1.50f, 1.50f, 2.0f));
					}
					else if (Input.GetAxis("Horizontal") < -0.50f)
					{
						this.GoToPKDir(PKDirType.Left, AnimNames.FemaleSansLeft, 
							new Vector3(-1.50f, 1.50f, 2.0f));
					}
					else
					{
						this.CharacterAnimation.CrossFade(AnimNames.FemaleSansHold);
						this.RagdollPK.transform.localPosition = new Vector3(0.0f, 1.50f, 2.0f);

						this.PKDir = PKDirType.None;
					}

					if (Input.GetButtonDown(InputNames.Xbox_B))
					{
						this.PromptBar.ClearButtons();
						this.PromptBar.UpdateButtons();
						this.PromptBar.Show = false;

						this.Ragdoll.GetComponent<RagdollScript>().StopDragging();
						this.SansEyes[0].SetActive(false);
						this.SansEyes[1].SetActive(false);
						this.GlowEffect.Stop();
						this.CanMove = true;
						this.PK = false;
					}
				}

				if (this.SummonBones)
				{
					this.CharacterAnimation.CrossFade(AnimNames.FemaleSansBones);

					if (this.BoneTimer == 0.0f)
					{
						Instantiate(this.Bone,
							this.transform.position +
							(this.transform.right * Random.Range(-2.50f, 2.50f)) +
							(this.transform.up * -2.0f) +
							(this.transform.forward * Random.Range(1.0f, 6.0f)),
							Quaternion.identity);
					}

					this.BoneTimer += Time.deltaTime;

					if (this.BoneTimer > 0.10f)
					{
						this.BoneTimer = 0.0f;
					}

					if (Input.GetButtonUp(InputNames.Xbox_RB))
					{
						this.SansEyes[0].SetActive(false);
						this.SansEyes[1].SetActive(false);
						this.GlowEffect.Stop();
						this.SummonBones = false;
						this.CanMove = true;
					}

					if (this.PK)
					{
						this.PromptBar.ClearButtons();
						this.PromptBar.UpdateButtons();
						this.PromptBar.Show = false;

						this.Ragdoll.GetComponent<RagdollScript>().StopDragging();
						this.SansEyes[0].SetActive(false);
						this.SansEyes[1].SetActive(false);
						this.GlowEffect.Stop();
						this.CanMove = true;
						this.PK = false;
					}
				}

				if (this.Blasting)
				{
					if (this.CharacterAnimation[AnimNames.FemaleSansBlaster].time >=
						(this.CharacterAnimation[AnimNames.FemaleSansBlaster].length - 0.25f))
					{
						this.SansEyes[0].SetActive(false);
						this.SansEyes[1].SetActive(false);
						this.GlowEffect.Stop();
						this.Blasting = false;
						this.CanMove = true;
					}

					if (this.PK)
					{
						this.PromptBar.ClearButtons();
						this.PromptBar.UpdateButtons();
						this.PromptBar.Show = false;

						this.Ragdoll.GetComponent<RagdollScript>().StopDragging();
						this.SansEyes[0].SetActive(false);
						this.SansEyes[1].SetActive(false);
						this.GlowEffect.Stop();
						this.CanMove = true;
						this.PK = false;
					}
				}

				if (this.SithAttacking)
				{
					if (!this.SithRecovering)
					{
						if (this.SithBeam[1].Damage == 10 || this.NierDamage == 10)
						{
							if (this.SithAttacks == 0)
							{
								if (this.CharacterAnimation["f02_" + AttackPrefix + "Attack" + SithPrefix + "_0" + this.SithCombo].time >= this.SithSpawnTime[SithCombo])
								{
									Instantiate(this.SithHitbox, transform.position + (transform.forward * 1) + transform.up, transform.rotation);
									this.SithAttacks++;
								}
							}
						}
						else
						{
							if (this.Pod.activeInHierarchy || this.Armor[20].activeInHierarchy)
							{
								if (this.CharacterAnimation["f02_" + AttackPrefix + "Attack" + SithPrefix + "_0" + this.SithCombo].time >= this.SithHardSpawnTime1[SithCombo])
								{
									if (this.SithAttacks == 0)
									{
										GameObject NewSithHitbox = Instantiate(this.SithHitbox, transform.position + (transform.forward * 1.5f) + transform.up, transform.rotation);
										NewSithHitbox.GetComponent<SithBeamScript>().Damage = 20;
										this.SithAttacks++;

										if (SithCombo < 2)
										{
											GameObject NewImpact = Instantiate(this.GroundImpact, transform.position + (transform.forward * 1.5f), transform.rotation);
											NewImpact.transform.localScale = new Vector3(2, 2, 2);
										}
									}
								}
							}
							else
							{
								if (this.SithAttacks == 0)
								{
									if (this.CharacterAnimation["f02_" + AttackPrefix + "Attack" + SithPrefix + "_0" + this.SithCombo].time >= this.SithHardSpawnTime1[SithCombo])
									{
										Instantiate(this.SithHardHitbox, transform.position + (transform.forward * 1) + transform.up, transform.rotation);
										this.SithAttacks++;
									}
								}
								else if (this.SithAttacks == 1)
								{
									if (this.CharacterAnimation["f02_" + AttackPrefix + "Attack" + SithPrefix + "_0" + this.SithCombo].time >= this.SithHardSpawnTime2[SithCombo])
									{
										Instantiate(this.SithHardHitbox, transform.position + (transform.forward * 1) + transform.up, transform.rotation);
										this.SithAttacks++;
									}
								}
								else if (this.SithAttacks == 2)
								{
									if (this.SithCombo == 1)
									{
										if (this.CharacterAnimation["f02_" + AttackPrefix + "Attack" + SithPrefix + "_0" + this.SithCombo].time >= 28.0f / 30.0f)
										{
											Instantiate(this.SithHardHitbox, transform.position + (transform.forward * 1) + transform.up, transform.rotation);
											this.SithAttacks++;
										}
									}
								}
							}
						}

						this.SithSoundCheck();

						if (this.CharacterAnimation["f02_" + AttackPrefix + "Attack" + SithPrefix + "_0" + this.SithCombo].time >= this.CharacterAnimation["f02_" + AttackPrefix + "Attack" + SithPrefix + "_0" + this.SithCombo].length)
						{
							if (this.SithCombo < this.SithComboLength)
							{
								this.SithCombo++;
								this.SithSounds = 0;
								this.SithAttacks = 0;
								this.CharacterAnimation.Play("f02_" + AttackPrefix + "Attack" + SithPrefix + "_0" + this.SithCombo);
							}
							else
							{
								this.CharacterAnimation.Play("f02_" + AttackPrefix + "Recover" + SithPrefix + "_0" + this.SithCombo);

								if (!this.Pod.activeInHierarchy)
								{
									this.CharacterAnimation["f02_" + AttackPrefix + "Recover" + SithPrefix + "_0" + this.SithCombo].speed = 2;
								}
								else
								{
									this.CharacterAnimation["f02_" + AttackPrefix + "Recover" + SithPrefix + "_0" + this.SithCombo].speed = .5f;
								}

								this.SithRecovering = true;
							}
						}
						else
						{
							if (Input.GetButtonDown(InputNames.Xbox_X))
							{
								if (this.SithComboLength < this.SithCombo + 1)
								{
									if (this.SithComboLength < 2)
									{
										this.SithComboLength++;
									}
								}
							}

							if (Input.GetButtonDown(InputNames.Xbox_Y))
							{
								if (this.SithComboLength < this.SithCombo + 1)
								{
									if (this.SithComboLength < 2)
									{
										this.SithComboLength++;
									}
								}
							}
						}
					}
					else
					{
						if (this.CharacterAnimation["f02_" + AttackPrefix + "Recover" + SithPrefix + "_0" + this.SithCombo].time >= this.CharacterAnimation["f02_" + AttackPrefix + "Recover" + SithPrefix + "_0" + this.SithCombo].length || this.h + this.v != 0)
						{
							//this.CharacterAnimation["f02_" + AttackPrefix + "Recover" + SithPrefix + "_0" + this.SithCombo].speed = 1;

							if (this.SithPrefix == "")
							{
								this.LightSwordParticles.Play();
							}
							else
							{
								this.HeavySwordParticles.Play();
							}

							this.HeavySword.GetComponent<WeaponTrail>().enabled = false;
							this.LightSword.GetComponent<WeaponTrail>().enabled = false;

							this.SithRecovering = false;
							this.SithAttacking = false;
							this.SithComboLength = 0;
							this.SithAttacks = 0;
							this.SithSounds = 0;
							this.SithCombo = 0;

							this.CanMove = true;

							/*
							this.CharacterAnimation["f02_nierAttack_00"].speed = .5f;
							this.CharacterAnimation["f02_nierAttack_01"].speed = .5f;
							this.CharacterAnimation["f02_nierAttack_02"].speed = .5f;

							this.CharacterAnimation["f02_nierAttackHard_00"].speed = .5f;
							this.CharacterAnimation["f02_nierAttackHard_01"].speed = .5f;
							this.CharacterAnimation["f02_nierAttackHard_02"].speed = .5f;
							*/
						}
					}
				}

				if (this.Eating)
				{
					//this.MoveTowardsTarget(this.SixTarget.position);

					if (Vector3.Distance(this.transform.position, this.TargetStudent.transform.position) > .5f)
					{
						this.transform.Translate(Vector3.forward * Time.deltaTime);
					}

					this.targetRotation = Quaternion.LookRotation(new Vector3(
						this.TargetStudent.transform.position.x,
						this.transform.position.y,
						this.TargetStudent.transform.position.z) - this.transform.position);
					
					this.transform.rotation = Quaternion.Slerp(
						this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);

					if (this.CharacterAnimation[AnimNames.SixEat].time > this.BloodTimes[EatPhase])
					{
						GameObject Blood = Instantiate(this.TargetStudent.StabBloodEffect, this.Mouth.position, Quaternion.identity);
						Blood.GetComponent<RandomStabScript>().Biting = true;
						this.Bloodiness += 20;
						this.EatPhase++;
					}

					if (this.CharacterAnimation[AnimNames.SixEat].time >= this.CharacterAnimation[AnimNames.SixEat].length)
					{
						if (!this.Kagune[0].activeInHierarchy)
						{
							if (this.Hunger < 5)
							{
								this.CharacterAnimation[AnimNames.SixRun].speed += .1f;
								this.RunSpeed++;
								this.Hunger++;

								if (this.Hunger == 5)
								{
									this.RisingSmoke.SetActive(true);
									this.RunAnim = AnimNames.SixFastRun;
								}
							}
						}

						Debug.Log("Finished eating.");

						this.FollowHips = false;
						this.Attacking = false;
						this.CanMove = true;
						this.Eating = false;
						this.EatPhase = 0;
					}
				}

				if (this.Snapping)
				{
					if (this.SnapPhase == 0)
					{
						if (this.Gazing)
						{
							if (this.CharacterAnimation["f02_gazerSnap_00"].time >= 0.8f)
							{
								AudioSource.PlayClipAtPoint(this.FingerSnap, this.transform.position + Vector3.up);
								this.GazerEyes.ChangeEffect();

								this.SnapPhase++;
							}
						}
						else if (this.WitchMode)
						{
							if (this.CharacterAnimation["f02_fingerSnap_00"].time >= 1f)
							{
								AudioSource.PlayClipAtPoint(this.FingerSnap, this.transform.position + Vector3.up);
								GameObject NewKnifeArray = Instantiate(this.KnifeArray, this.transform.position, this.transform.rotation);
								NewKnifeArray.GetComponent<KnifeArrayScript>().GlobalKnifeArray = GlobalKnifeArray;

								this.SnapPhase++;
							}
						}
						else
						{
							if (this.ShotsFired < 1)
							{
								if (this.CharacterAnimation["f02_shipGirlSnap_00"].time >= 1.0f)
								{
									Instantiate(Shell, this.Guns[1].position, transform.rotation);
									this.ShotsFired++;
								}
							}
							else if (this.ShotsFired < 2)
							{
								if (this.CharacterAnimation["f02_shipGirlSnap_00"].time >= 1.2f)
								{
									Instantiate(Shell, this.Guns[2].position, transform.rotation);
									this.ShotsFired++;
								}
							}
							else if (this.ShotsFired < 3)
							{
								if (this.CharacterAnimation["f02_shipGirlSnap_00"].time >= 1.4f)
								{
									Instantiate(Shell, this.Guns[3].position, transform.rotation);
									this.ShotsFired++;
								}
							}
							else if (this.ShotsFired < 4)
							{
								if (this.CharacterAnimation["f02_shipGirlSnap_00"].time >= 1.6f)
								{
									Instantiate(Shell, this.Guns[4].position, transform.rotation);
									this.ShotsFired++;

									this.SnapPhase++;
								}
							}
						}
					}
					else
					{
						if (this.Gazing)
						{
							if (this.CharacterAnimation["f02_gazerSnap_00"].time >= this.CharacterAnimation["f02_gazerSnap_00"].length)
							{
								this.Snapping = false;
								this.CanMove = true;
								this.SnapPhase = 0;
							}
						}
						else if (this.WitchMode)
						{
							if (this.CharacterAnimation["f02_fingerSnap_00"].time >= this.CharacterAnimation["f02_fingerSnap_00"].length)
							{
								this.CharacterAnimation.Stop("f02_fingerSnap_00");
								this.Snapping = false;
								this.CanMove = true;
								this.SnapPhase = 0;
							}
						}
						else
						{
							if (this.CharacterAnimation["f02_shipGirlSnap_00"].time >= this.CharacterAnimation["f02_shipGirlSnap_00"].length)
							{
								this.Snapping = false;
								this.CanMove = true;
								this.ShotsFired = 0;
								this.SnapPhase = 0;
							}
						}
					}
				}

				if (this.GazeAttacking)
				{
					if (this.TargetStudent != null)
					{
						this.targetRotation = Quaternion.LookRotation(new Vector3(
							this.TargetStudent.transform.position.x,
							this.transform.position.y,
							this.TargetStudent.transform.position.z) - this.transform.position);
						this.transform.rotation = Quaternion.Slerp(
							this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);
					}

					if (this.SnapPhase == 0)
					{
						if (this.CharacterAnimation["f02_gazerPoint_00"].time >= 1)
						{
							AudioSource.PlayClipAtPoint(this.Zap, this.transform.position + Vector3.up);

							this.GazerEyes.Attack();

							this.SnapPhase++;
						}
					}
					else
					{
						if (this.CharacterAnimation["f02_gazerPoint_00"].time >= this.CharacterAnimation["f02_gazerPoint_00"].length)
						{
							this.GazerEyes.Attacking = false;
							this.GazeAttacking = false;
							this.CanMove = true;
							this.SnapPhase = 0;
						}
					}
				}

				if (this.Finisher)
				{
					if (this.CharacterAnimation["f02_banchoFinisher_00"].time >= this.CharacterAnimation["f02_banchoFinisher_00"].length)
					{
						this.CharacterAnimation.CrossFade(this.IdleAnim);
						this.Finisher = false;
						this.CanMove = true;
					}
					else if (this.CharacterAnimation["f02_banchoFinisher_00"].time >= 50.0f / 30.0f)
					{
						this.BanchoFinisher.MyCollider.enabled = false;
					}
					else if (this.CharacterAnimation["f02_banchoFinisher_00"].time >= 25.0f / 30.0f)
					{
						this.BanchoFinisher.MyCollider.enabled = true;
					}
				}

				if (this.ShootingBeam)
				{
					this.CharacterAnimation.CrossFade("f02_LoveLoveBeam_00");

					if (this.CharacterAnimation["f02_LoveLoveBeam_00"].time >= 1.5f)
					{
						if (this.BeamPhase == 0)
						{
							Instantiate(LoveLoveBeam, transform.position, transform.rotation);
							this.BeamPhase++;
						}
					}

					if (this.CharacterAnimation["f02_LoveLoveBeam_00"].time >= this.CharacterAnimation["f02_LoveLoveBeam_00"].length - 1)
					{
						this.ShootingBeam = false;
						this.YandereTimer = 0;
						this.CanMove = true;
						this.BeamPhase = 0;
					}
				}

				if (this.WritingName)
				{
					this.CharacterAnimation.CrossFade("f02_dramaticWriting_00");

					if (this.CharacterAnimation["f02_dramaticWriting_00"].time == 0)
					{
						AudioSource.PlayClipAtPoint(this.DramaticWriting, this.transform.position);
					}

					if (this.CharacterAnimation["f02_dramaticWriting_00"].time >= 5)
					{
						if (this.StudentManager.NoteWindow.TargetStudent > 0)
						{
							this.StudentManager.Students[this.StudentManager.NoteWindow.TargetStudent].Fate =
								this.StudentManager.NoteWindow.MeetID;
							this.StudentManager.Students[this.StudentManager.NoteWindow.TargetStudent].TimeOfDeath =
								this.StudentManager.NoteWindow.TimeID;

							this.StudentManager.NoteWindow.TargetStudent = 0;
						}
					}

					if (this.CharacterAnimation["f02_dramaticWriting_00"].time >= this.CharacterAnimation["f02_dramaticWriting_00"].length)
					{
						this.CharacterAnimation[this.CarryAnims[10]].weight = 1.0f;

						this.CharacterAnimation["f02_dramaticWriting_00"].time = 0;
						this.CharacterAnimation.Stop("f02_dramaticWriting_00");
						this.WritingName = false;
						this.CanMove = true;
					}
				}

				if (this.StoppingTime)
				{
					this.CharacterAnimation.CrossFade("f02_summonStand_00");

					if (this.CharacterAnimation["f02_summonStand_00"].time >= 1)
					{
						if (this.Freezing)
						{
							if (!this.InvertSphere.gameObject.activeInHierarchy)
							{
								if (this.MyAudio.clip != this.ClockStop)
								{
									this.MyAudio.clip = this.ClockStop;
									this.MyAudio.volume = 1;
									this.MyAudio.Play();
								}

								this.InvertSphere.gameObject.SetActive(true);
								this.PlayerOnlyCamera.SetActive(true);

								this.StudentManager.TimeFreeze();
							}

							this.InvertSphere.transform.localScale = Vector3.MoveTowards(
								this.InvertSphere.transform.localScale,
								new Vector3(0.2375f, 0.2375f, 0),
								Time.deltaTime);

							this.MyAudio.volume = 1;

							this.Jukebox.Ebola.pitch = Mathf.MoveTowards(this.Jukebox.Ebola.pitch, .2f, Time.deltaTime);
						}
						else
						{
							if (this.MyAudio.clip != this.ClockStart)
							{
								this.MyAudio.clip = this.ClockStart;
								this.MyAudio.volume = 1;
								this.MyAudio.Play();

								this.StudentManager.TimeUnfreeze();
							}

							this.InvertSphere.transform.localScale = Vector3.MoveTowards(
								this.InvertSphere.transform.localScale,
								new Vector3(0, 0, 0),
								Time.deltaTime);

							this.MyAudio.volume = 1;

							this.Jukebox.Ebola.pitch = Mathf.MoveTowards(this.Jukebox.Ebola.pitch, 1, Time.deltaTime);

							this.GlobalKnifeArray.ActivateKnives();
						}
					}

					if (this.CharacterAnimation["f02_summonStand_00"].time >= this.CharacterAnimation["f02_summonStand_00"].length)
					{
						//this.CharacterAnimation["f02_summonStand_00"].time = 0;
						//this.CharacterAnimation.Stop("f02_summonStand_00");
						this.StoppingTime = false;
						this.CanMove = true;

						this.InvertSphere.gameObject.SetActive(this.Freezing);
						this.PlayerOnlyCamera.SetActive(this.Freezing);
					}
				}
			}
		}
	}

	void UpdatePoisoning()
	{
		if (this.Poisoning)
		{
			if (this.PoisonSpot != null)
			{
				this.MoveTowardsTarget(this.PoisonSpot.position);
				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.PoisonSpot.rotation, Time.deltaTime * 10.0f);
			}
			else
			{
				this.targetRotation = Quaternion.LookRotation(new Vector3(
					this.TargetBento.transform.position.x,
					this.transform.position.y,
					this.TargetBento.transform.position.z) - this.transform.position);

				this.transform.rotation = Quaternion.Slerp(this.transform.rotation, this.targetRotation, Time.deltaTime * 10.0f);

				this.MoveTowardsTarget(this.TargetBento.PoisonSpot.position);
			}

			if (this.CharacterAnimation[AnimNames.FemalePoisoning].time >=
				this.CharacterAnimation[AnimNames.FemalePoisoning].length)
			{
				this.CharacterAnimation[AnimNames.FemalePoisoning].speed = 1;

				this.PoisonSpot = null;
				this.Poisoning = false;
				this.CanMove = true;
			}
			else if (this.CharacterAnimation[AnimNames.FemalePoisoning].time >= 5.25f)
			{
				this.Poisons[this.PoisonType].SetActive(false);
			}
			else if (this.CharacterAnimation[AnimNames.FemalePoisoning].time >= 0.75)
			{
				this.Poisons[this.PoisonType].SetActive(true);
			}
		}
	}

	void UpdateEffects()
	{
		if (!this.Attacking && !this.DelinquentFighting && !this.Lost && this.CanMove)
		{
			if (Vector3.Distance(this.transform.position, this.Senpai.position) < 1.0f)
			{
				if (!this.Talking)
				{
					if (!this.NearSenpai)
					{
						if (this.StudentManager.Students[1].Pathfinding.speed < 7.5f)
						{
							this.StudentManager.TutorialWindow.ShowSenpaiMessage = true;
							this.DepthOfField.focalSize = 150.0f;
							this.NearSenpai = true;
						}
					}

					if (this.Laughing)
					{
                        Debug.Log("Yandere-chan was laughing, and is being told to stop laughing because UpdateEffects() was called.");

						this.StopLaughing();

                        if (this.Pursuer != null)
                        {
                            this.CanMove = false;
                        }
					}

					this.Stance.Current = StanceType.Standing;
					this.Obscurance.enabled = false;
					this.YandereVision = false;
					this.Mopping = false;
					this.Uncrouch();

					this.YandereTimer = 0.0f;

					this.EmptyHands();

					if (this.Aiming)
					{
						this.StopAiming();
					}
				}
			}
			else
			{
				this.NearSenpai = false;
			}
		}

		if (this.NearSenpai && !this.Noticed)
		{
			this.DepthOfField.enabled = true;

			this.DepthOfField.focalSize = Mathf.Lerp(
				this.DepthOfField.focalSize, 0.0f, Time.deltaTime * 10.0f);
			this.DepthOfField.focalZStartCurve = Mathf.Lerp(
				this.DepthOfField.focalZStartCurve, 20.0f, Time.deltaTime * 10.0f);
			this.DepthOfField.focalZEndCurve = Mathf.Lerp(
				this.DepthOfField.focalZEndCurve, 20.0f, Time.deltaTime * 10.0f);

			this.DepthOfField.objectFocus = this.Senpai.transform;

			this.ColorCorrection.enabled = true;

			this.SenpaiFade = Mathf.Lerp(this.SenpaiFade, 0.0f, Time.deltaTime * 10.0f);

			this.SenpaiTint = 1.0f - (this.SenpaiFade / 100.0f);

			this.ColorCorrection.redChannel.MoveKey(1, new Keyframe(0.50f, 0.50f + (this.SenpaiTint * 0.50f)));
			this.ColorCorrection.greenChannel.MoveKey(1, new Keyframe(0.50f, 1.0f - (this.SenpaiTint * 0.50f)));
			this.ColorCorrection.blueChannel.MoveKey(1, new Keyframe(0.50f, 0.50f + (this.SenpaiTint * 0.50f)));

			this.ColorCorrection.redChannel.SmoothTangents(1, 0);
			this.ColorCorrection.greenChannel.SmoothTangents(1, 0);
			this.ColorCorrection.blueChannel.SmoothTangents(1, 0);

			this.ColorCorrection.UpdateTextures();

			if (!this.Attacking)
			{
				//this.CharacterAnimation[AnimNames.FemaleShy].weight = this.SenpaiTint;
			}

			this.SelectGrayscale.desaturation = Mathf.Lerp(this.SelectGrayscale.desaturation, 0, Time.deltaTime * 10.0f);

			this.HeartBeat.volume = this.SenpaiTint;

			this.Sanity += Time.deltaTime * 10.0f;

			this.SenpaiTimer += Time.deltaTime;
			this.BeatTimer += Time.deltaTime;

			if (this.BeatTimer > 60.0f / this.HeartRate.BeatsPerMinute)
			{
				GamePad.SetVibration(0, 1, 1);
				this.VibrationCheck = true;
				this.VibrationTimer = .1f;
				this.BeatTimer = 0;
			}

			if (this.SenpaiTimer > 10)
			{
				if (this.Creepiness < 5)
				{
					this.SenpaiTimer = 0;
					this.Creepiness++;
				}
			}
		}
		else
		{
			if (this.SenpaiFade < 99.0f)
			{
				this.DepthOfField.focalSize = Mathf.Lerp(
					this.DepthOfField.focalSize, 150.0f, Time.deltaTime * 10.0f);
				this.DepthOfField.focalZStartCurve = Mathf.Lerp(
					this.DepthOfField.focalZStartCurve, 0.0f, Time.deltaTime * 10.0f);
				this.DepthOfField.focalZEndCurve = Mathf.Lerp(
					this.DepthOfField.focalZEndCurve, 0.0f, Time.deltaTime * 10.0f);

				this.SenpaiFade = Mathf.Lerp(this.SenpaiFade, 100.0f, Time.deltaTime * 10.0f);

				this.SenpaiTint = this.SenpaiFade / 100.0f;

				this.ColorCorrection.redChannel.MoveKey(1, new Keyframe(0.50f, 1.0f - (this.SenpaiTint * 0.50f)));
				this.ColorCorrection.greenChannel.MoveKey(1, new Keyframe(0.50f, this.SenpaiTint * 0.50f));
				this.ColorCorrection.blueChannel.MoveKey(1, new Keyframe(0.50f, 1.0f - (this.SenpaiTint * 0.50f)));

				this.ColorCorrection.redChannel.SmoothTangents(1, 0);
				this.ColorCorrection.greenChannel.SmoothTangents(1, 0);
				this.ColorCorrection.blueChannel.SmoothTangents(1, 0);

				this.ColorCorrection.UpdateTextures();

				this.SelectGrayscale.desaturation = Mathf.Lerp(this.SelectGrayscale.desaturation, this.GreyTarget, Time.deltaTime * 10.0f);
				this.CharacterAnimation[AnimNames.FemaleShy].weight = 1.0f - this.SenpaiTint;

				for (int i = 1; i < 6; i++)
				{
					this.CharacterAnimation[this.CreepyIdles[i]].weight = Mathf.MoveTowards(this.CharacterAnimation[this.CreepyIdles[i]].weight, 0, Time.deltaTime * 10);
					this.CharacterAnimation[this.CreepyWalks[i]].weight = Mathf.MoveTowards(this.CharacterAnimation[this.CreepyWalks[i]].weight, 0, Time.deltaTime * 10);
				}

				this.HeartBeat.volume = 1.0f - this.SenpaiTint;
			}
			else if (this.SenpaiFade < 100.0f)
			{
				this.ResetSenpaiEffects();
			}
		}

		if (this.YandereVision)
		{
			if (!this.HighlightingR.enabled)
			{
				this.YandereColorCorrection.enabled = true;
				this.HighlightingR.enabled = true;
				this.HighlightingB.enabled = true;
				this.Obscurance.enabled = true;
				this.Vignette.enabled = true;
			}

			Time.timeScale = Mathf.Lerp(Time.timeScale, 0.50f, Time.unscaledDeltaTime * 10.0f);

			this.YandereFade = Mathf.Lerp(this.YandereFade, 0.0f, Time.deltaTime * 10.0f);

			this.YandereTint = 1.0f - (this.YandereFade / 100.0f);

			this.YandereColorCorrection.redChannel.MoveKey(1, new Keyframe(0.50f, 0.50f - (this.YandereTint * 0.25f)));
			this.YandereColorCorrection.greenChannel.MoveKey(1, new Keyframe(0.50f, 0.50f - (this.YandereTint * 0.25f)));
			this.YandereColorCorrection.blueChannel.MoveKey(1, new Keyframe(0.50f, 0.50f + (this.YandereTint * 0.25f)));

			this.YandereColorCorrection.redChannel.SmoothTangents(1, 0);
			this.YandereColorCorrection.greenChannel.SmoothTangents(1, 0);
			this.YandereColorCorrection.blueChannel.SmoothTangents(1, 0);

			this.YandereColorCorrection.UpdateTextures();

			this.Vignette.intensity = Mathf.Lerp(
				this.Vignette.intensity, this.YandereTint * 5.0f, Time.deltaTime * 10.0f);
			this.Vignette.blur = Mathf.Lerp(
				this.Vignette.blur, this.YandereTint, Time.deltaTime * 10.0f);
			this.Vignette.chromaticAberration = Mathf.Lerp(
				this.Vignette.chromaticAberration, this.YandereTint * 5.0f, Time.deltaTime * 10.0f);

			if (this.StudentManager.Tag.Target != null)
			{
				this.StudentManager.Tag.Sprite.color = new Color(1, 0, 0, Mathf.Lerp(this.StudentManager.Tag.Sprite.color.a, 1, Time.unscaledDeltaTime * 10.0f));
			}

			if (this.StudentManager.Students[this.StudentManager.RivalID] != null)
			{
				this.StudentManager.RedString.gameObject.SetActive(true);
			}
		}
		else
		{
			if (this.HighlightingR.enabled)
			{
				this.HighlightingR.enabled = false;
				this.HighlightingB.enabled = false;
				this.Obscurance.enabled = false;
			}

			if (this.YandereFade < 99.0f)
			{
				if (!this.Aiming)
				{
					Time.timeScale = Mathf.Lerp(Time.timeScale, 1.0f, Time.unscaledDeltaTime * 10.0f);
				}

				this.YandereFade = Mathf.Lerp(this.YandereFade, 100.0f, Time.deltaTime * 10.0f);

				this.YandereTint = this.YandereFade / 100.0f;

				this.YandereColorCorrection.redChannel.MoveKey(1, new Keyframe(0.50f, this.YandereTint * 0.50f));
				this.YandereColorCorrection.greenChannel.MoveKey(1, new Keyframe(0.50f, this.YandereTint * 0.50f));
				this.YandereColorCorrection.blueChannel.MoveKey(1, new Keyframe(0.50f, 1.0f - (this.YandereTint * 0.50f)));

				this.YandereColorCorrection.redChannel.SmoothTangents(1, 0);
				this.YandereColorCorrection.greenChannel.SmoothTangents(1, 0);
				this.YandereColorCorrection.blueChannel.SmoothTangents(1, 0);

				this.YandereColorCorrection.UpdateTextures();

				this.Vignette.intensity = Mathf.Lerp(
					this.Vignette.intensity, 0.0f, Time.deltaTime * 10.0f);
				this.Vignette.blur = Mathf.Lerp(
					this.Vignette.blur, 0.0f, Time.deltaTime * 10.0f);
				this.Vignette.chromaticAberration = Mathf.Lerp(
					this.Vignette.chromaticAberration, 0.0f, Time.deltaTime * 10.0f);

				this.StudentManager.Tag.Sprite.color = new Color(1, 0, 0, Mathf.Lerp(this.StudentManager.Tag.Sprite.color.a, 0, Time.unscaledDeltaTime * 10.0f));

				this.StudentManager.RedString.gameObject.SetActive(false);
			}
			else if (this.YandereFade < 100.0f)
			{
				this.ResetYandereEffects();
			}
		}

		this.RightRedEye.material.color = new Color(
			this.RightRedEye.material.color.r,
			this.RightRedEye.material.color.g,
			this.RightRedEye.material.color.b,
			1.0f - (this.YandereFade / 100.0f));

		this.LeftRedEye.material.color = new Color(
			this.LeftRedEye.material.color.r,
			this.LeftRedEye.material.color.g,
			this.LeftRedEye.material.color.b,
			1.0f - (this.YandereFade / 100.0f));

		this.RightYandereEye.material.color = new Color(
			this.RightYandereEye.material.color.r,
			this.YandereFade / 100.0f,
			this.YandereFade / 100.0f,
			this.RightYandereEye.material.color.a);

		this.LeftYandereEye.material.color = new Color(
			this.LeftYandereEye.material.color.r,
			this.YandereFade / 100.0f,
			this.YandereFade / 100.0f,
			this.LeftYandereEye.material.color.a);
	}

	void UpdateTalking()
	{
		if (this.Talking)
		{
			if (this.TargetStudent != null)
			{
				this.targetRotation = Quaternion.LookRotation(new Vector3(
					this.TargetStudent.transform.position.x,
					this.transform.position.y,
					this.TargetStudent.transform.position.z) - this.transform.position);

				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.targetRotation, Time.deltaTime * 10.0f);

				if (Vector3.Distance(this.transform.position, this.TargetStudent.transform.position) < .75f)
				{
					this.MyController.Move(transform.forward * Time.deltaTime * -1);
				}
			}

			if (this.Interaction == YandereInteractionType.Idle)
			{
				if (this.TargetStudent != null)
				{
					if (!this.TargetStudent.Counselor)
					{
						this.CharacterAnimation.CrossFade(this.IdleAnim);
					}
				}
			}

			///////////////////////
			///// APOLOGIZING /////
			///////////////////////

			else if (this.Interaction == YandereInteractionType.Apologizing)
			{
				if (this.TalkTimer == 3.0f)
				{
					this.CharacterAnimation.CrossFade(AnimNames.FemaleGreet);

					if ((this.TargetStudent.Witnessed == StudentWitnessType.Insanity) ||
						(this.TargetStudent.Witnessed == StudentWitnessType.WeaponAndBloodAndInsanity) ||
						(this.TargetStudent.Witnessed == StudentWitnessType.WeaponAndInsanity) ||
						(this.TargetStudent.Witnessed == StudentWitnessType.BloodAndInsanity))
					{
						this.Subtitle.UpdateLabel(SubtitleType.InsanityApology, 0, 3.0f);
					}
					else if (this.TargetStudent.Witnessed == StudentWitnessType.WeaponAndBlood)
					{
						this.Subtitle.UpdateLabel(SubtitleType.WeaponAndBloodApology, 0, 3.0f);
					}
					else if (this.TargetStudent.Witnessed == StudentWitnessType.Weapon)
					{
						this.Subtitle.UpdateLabel(SubtitleType.WeaponApology, 0, 3.0f);
					}
					else if (this.TargetStudent.Witnessed == StudentWitnessType.Blood)
					{
						this.Subtitle.UpdateLabel(SubtitleType.BloodApology, 0, 3.0f);
					}
					else if (this.TargetStudent.Witnessed == StudentWitnessType.Lewd)
					{
						this.Subtitle.UpdateLabel(SubtitleType.LewdApology, 0, 3.0f);
					}
					else if (this.TargetStudent.Witnessed == StudentWitnessType.Accident)
					{
						this.Subtitle.UpdateLabel(SubtitleType.AccidentApology, 0, 3.0f);
					}
					else if (this.TargetStudent.Witnessed == StudentWitnessType.Suspicious)
					{
						this.Subtitle.UpdateLabel(SubtitleType.SuspiciousApology, 0, 3.0f);
					}
					else if (this.TargetStudent.Witnessed == StudentWitnessType.Eavesdropping)
					{
						this.Subtitle.UpdateLabel(SubtitleType.EavesdropApology, 0, 3.0f);
					}
					else if (this.TargetStudent.Witnessed == StudentWitnessType.Theft)
					{
						this.Subtitle.UpdateLabel(SubtitleType.TheftApology, 0, 3.0f);
					}
					else if (this.TargetStudent.Witnessed == StudentWitnessType.Violence)
					{
						this.Subtitle.UpdateLabel(SubtitleType.ViolenceApology, 0, 3.0f);
					}
					else if (this.TargetStudent.Witnessed == StudentWitnessType.Pickpocketing)
					{
						this.Subtitle.UpdateLabel(SubtitleType.PickpocketApology, 0, 3.0f);
					}
					else if (this.TargetStudent.Witnessed == StudentWitnessType.CleaningItem)
					{
						this.Subtitle.UpdateLabel(SubtitleType.CleaningApology, 0, 3.0f);
					}
					else if (this.TargetStudent.Witnessed == StudentWitnessType.Poisoning)
					{
						this.Subtitle.UpdateLabel(SubtitleType.PoisonApology, 0, 3.0f);
					}
					else if (this.TargetStudent.Witnessed == StudentWitnessType.HoldingBloodyClothing)
					{
						this.Subtitle.UpdateLabel(SubtitleType.HoldingBloodyClothingApology, 0, 3.0f);
					}
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.TalkTimer = 0.0f;
					}

					if (this.CharacterAnimation[AnimNames.FemaleGreet].time >=
						this.CharacterAnimation[AnimNames.FemaleGreet].length)
					{
						this.CharacterAnimation.CrossFade(this.IdleAnim);
					}

					if (this.TalkTimer <= 0.0f)
					{
						this.TargetStudent.Interaction = StudentInteractionType.Forgiving;
						this.TargetStudent.TalkTimer = 3.0f;
						this.Interaction = YandereInteractionType.Idle;
					}
				}

				this.TalkTimer -= Time.deltaTime;
			}

			//////////////////////
			///// COMPLIMENT /////
			//////////////////////

			else if (this.Interaction == YandereInteractionType.Compliment)
			{
				if (this.TalkTimer == 3.0f)
				{
					this.CharacterAnimation.CrossFade(AnimNames.FemaleGreet01);

					if (!this.TargetStudent.Male)
					{
						this.Subtitle.UpdateLabel(SubtitleType.PlayerCompliment, 0, 3.0f);
					}
					else
					{
						this.Subtitle.UpdateLabel(SubtitleType.PlayerCompliment, 1, 3.0f);
					}
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.TalkTimer = 0.0f;
					}

					if (this.CharacterAnimation[AnimNames.FemaleGreet01].time >=
						this.CharacterAnimation[AnimNames.FemaleGreet01].length)
					{
						this.CharacterAnimation.CrossFade(this.IdleAnim);
					}

					if (this.TalkTimer <= 0.0f)
					{
						this.TargetStudent.Interaction = StudentInteractionType.ReceivingCompliment;
						this.TargetStudent.TalkTimer = 3.0f;
						this.Interaction = YandereInteractionType.Idle;
					}
				}

				this.TalkTimer -= Time.deltaTime;
			}

			//////////////////
			///// GOSSIP /////
			//////////////////

			else if (this.Interaction == YandereInteractionType.Gossip)
			{
				if (this.TalkTimer == 3.0f)
				{
					this.CharacterAnimation.CrossFade(AnimNames.FemaleLookDown);

					this.Subtitle.UpdateLabel(SubtitleType.PlayerGossip, 0, 3.0f);
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.TalkTimer = 0.0f;
					}

					if (this.CharacterAnimation[AnimNames.FemaleLookDown].time >=
						this.CharacterAnimation[AnimNames.FemaleLookDown].length)
					{
						this.CharacterAnimation.CrossFade(this.IdleAnim);
					}

					if (this.TalkTimer <= 0.0f)
					{
						this.TargetStudent.Interaction = StudentInteractionType.Gossiping;
						this.TargetStudent.TalkTimer = 3.0f;
						this.Interaction = YandereInteractionType.Idle;
					}
				}

				this.TalkTimer -= Time.deltaTime;
			}

			///////////////
			///// BYE /////
			///////////////

			else if (this.Interaction == YandereInteractionType.Bye)
			{
				if (this.TalkTimer == 2.0f)
				{
					this.CharacterAnimation.CrossFade(AnimNames.FemaleGreet);

					this.Subtitle.UpdateLabel(SubtitleType.PlayerFarewell, 0, 2.0f);
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.TalkTimer = 0.0f;
					}

					if (this.CharacterAnimation[AnimNames.FemaleGreet].time >=
						this.CharacterAnimation[AnimNames.FemaleGreet].length)
					{
						this.CharacterAnimation.CrossFade(this.IdleAnim);
					}

					if (this.TalkTimer <= 0.0f)
					{
						this.TargetStudent.Interaction = StudentInteractionType.Bye;
						this.TargetStudent.TalkTimer = 2.0f;
						this.Interaction = YandereInteractionType.Idle;
					}
				}

				this.TalkTimer -= Time.deltaTime;
			}

			/////////////////////
			///// FOLLOW ME /////
			/////////////////////

			else if (this.Interaction == YandereInteractionType.FollowMe)
			{
				int TempID = 0;if (this.Club == ClubType.Delinquent){TempID++;}

				if (this.TalkTimer == 3.0f)
				{
					if (this.Club == ClubType.Delinquent){this.TalkAnim = "f02_delinquentGesture_01";}
					else {this.TalkAnim = AnimNames.FemaleGreet01;}

					this.CharacterAnimation.CrossFade(this.TalkAnim);

					this.Subtitle.UpdateLabel(SubtitleType.PlayerFollow, TempID, 3.0f);
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.TalkTimer = 0.0f;
					}

					if (this.CharacterAnimation[this.TalkAnim].time >=
						this.CharacterAnimation[this.TalkAnim].length)
					{
						this.CharacterAnimation.CrossFade(this.IdleAnim);
					}

					if (this.TalkTimer <= 0.0f)
					{
						this.TargetStudent.Interaction = StudentInteractionType.FollowingPlayer;
						this.TargetStudent.TalkTimer = 2.0f;
						this.Interaction = YandereInteractionType.Idle;
					}
				}

				this.TalkTimer -= Time.deltaTime;
			}

			///////////////////
			///// GO AWAY /////
			///////////////////

			else if (this.Interaction == YandereInteractionType.GoAway)
			{
				int TempID = 0;if (this.Club == ClubType.Delinquent){TempID++;}

				if (this.TalkTimer == 3.0f)
				{
					if (this.Club == ClubType.Delinquent){this.TalkAnim = "f02_delinquentGesture_01";}
					else {this.TalkAnim = AnimNames.FemaleLookDown;}

					this.CharacterAnimation.CrossFade(this.TalkAnim);

					this.Subtitle.UpdateLabel(SubtitleType.PlayerLeave, TempID, 3.0f);
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.TalkTimer = 0.0f;
					}

					if (this.CharacterAnimation[this.TalkAnim].time >=
						this.CharacterAnimation[this.TalkAnim].length)
					{
						this.CharacterAnimation.CrossFade(this.IdleAnim);
					}

					if (this.TalkTimer <= 0.0f)
					{
						this.TargetStudent.Interaction = StudentInteractionType.GoingAway;
						this.TargetStudent.TalkTimer = 3.0f;
						this.Interaction = YandereInteractionType.Idle;
					}
				}

				this.TalkTimer -= Time.deltaTime;
			}

			/////////////////////////
			///// DISTRACT THEM /////
			/////////////////////////

			else if (this.Interaction == YandereInteractionType.DistractThem)
			{
				int TempID = 0;if (this.Club == ClubType.Delinquent){TempID++;}

				if (this.TalkTimer == 3.0f)
				{
					if (this.Club == ClubType.Delinquent){this.TalkAnim = "f02_delinquentGesture_01";}
					else {this.TalkAnim = AnimNames.FemaleLookDown;}

					this.CharacterAnimation.CrossFade(this.TalkAnim);

					this.Subtitle.UpdateLabel(SubtitleType.PlayerDistract, TempID, 3.0f);
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.TalkTimer = 0.0f;
					}

					if (this.CharacterAnimation[this.TalkAnim].time >=
						this.CharacterAnimation[this.TalkAnim].length)
					{
						this.CharacterAnimation.CrossFade(this.IdleAnim);
					}

					if (this.TalkTimer <= 0.0f)
					{
						this.TargetStudent.Interaction = StudentInteractionType.DistractingTarget;
						this.TargetStudent.TalkTimer = 3.0f;
						this.Interaction = YandereInteractionType.Idle;
					}
				}

				this.TalkTimer -= Time.deltaTime;
			}

			////////////////////////
			///// NAMING CRUSH /////
			////////////////////////

			else if (this.Interaction == YandereInteractionType.NamingCrush)
			{
				if (this.TalkTimer == 3.0f)
				{
					this.CharacterAnimation.CrossFade(AnimNames.FemaleGreet01);
					this.Subtitle.UpdateLabel(SubtitleType.PlayerLove, 0, 3.0f);
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.TalkTimer = 0.0f;
					}

					if (this.CharacterAnimation[AnimNames.FemaleGreet01].time >=
						this.CharacterAnimation[AnimNames.FemaleGreet01].length)
					{
						this.CharacterAnimation.CrossFade(this.IdleAnim);
					}

					if (this.TalkTimer <= 0.0f)
					{
						this.TargetStudent.Interaction = StudentInteractionType.NamingCrush;
						this.TargetStudent.TalkTimer = 3.0f;
						this.Interaction = YandereInteractionType.Idle;
					}
				}

				this.TalkTimer -= Time.deltaTime;
			}

			///////////////////////////////
			///// CHANGING APPEARANCE /////
			///////////////////////////////

			else if (this.Interaction == YandereInteractionType.ChangingAppearance)
			{
				if (this.TalkTimer == 3.0f)
				{
					this.CharacterAnimation.CrossFade(AnimNames.FemaleGreet01);
					this.Subtitle.UpdateLabel(SubtitleType.PlayerLove, 2, 3.0f);
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.TalkTimer = 0.0f;
					}

					if (this.CharacterAnimation[AnimNames.FemaleGreet01].time >=
						this.CharacterAnimation[AnimNames.FemaleGreet01].length)
					{
						this.CharacterAnimation.CrossFade(this.IdleAnim);
					}

					if (this.TalkTimer <= 0.0f)
					{
						this.TargetStudent.Interaction = StudentInteractionType.ChangingAppearance;
						this.TargetStudent.TalkTimer = 3.0f;
						this.Interaction = YandereInteractionType.Idle;
					}
				}

				this.TalkTimer -= Time.deltaTime;
			}

			/////////////////////
			///// COURTSHIP /////
			/////////////////////

			else if (this.Interaction == YandereInteractionType.Court)
			{
				if (this.TalkTimer == 5.0f)
				{
					this.CharacterAnimation.CrossFade(AnimNames.FemaleGreet01);

					if (!this.TargetStudent.Male)
					{
						this.Subtitle.UpdateLabel(SubtitleType.PlayerLove, 3, 5.0f);
					}
					else
					{
						this.Subtitle.UpdateLabel(SubtitleType.PlayerLove, 4, 5.0f);
					}
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.TalkTimer = 0.0f;
					}

					if (this.CharacterAnimation[AnimNames.FemaleGreet01].time >=
						this.CharacterAnimation[AnimNames.FemaleGreet01].length)
					{
						this.CharacterAnimation.CrossFade(this.IdleAnim);
					}

					if (this.TalkTimer <= 0.0f)
					{
						this.TargetStudent.Interaction = StudentInteractionType.Court;
						this.TargetStudent.TalkTimer = 3.0f;
						this.Interaction = YandereInteractionType.Idle;
					}
				}

				this.TalkTimer -= Time.deltaTime;
			}

			///////////////////
			///// CONFESS /////
			///////////////////

			else if (this.Interaction == YandereInteractionType.Confess)
			{
				if (this.TalkTimer == 5.0f)
				{
					this.CharacterAnimation.CrossFade(AnimNames.FemaleGreet01);
					this.Subtitle.UpdateLabel(SubtitleType.PlayerLove, 5, 5.0f);
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.TalkTimer = 0.0f;
					}

					if (this.CharacterAnimation[AnimNames.FemaleGreet01].time >=
						this.CharacterAnimation[AnimNames.FemaleGreet01].length)
					{
						this.CharacterAnimation.CrossFade(this.IdleAnim);
					}

					if (this.TalkTimer <= 0.0f)
					{
						this.TargetStudent.Interaction = StudentInteractionType.Gift;
						this.TargetStudent.TalkTimer = 5.0f;
						this.Interaction = YandereInteractionType.Idle;
					}
				}

				this.TalkTimer -= Time.deltaTime;
			}

			////////////////////////
			///// TASK INQUIRY /////
			////////////////////////

			else if (this.Interaction == YandereInteractionType.TaskInquiry)
			{
				if (this.TalkTimer == 3.0f)
				{
					this.CharacterAnimation.CrossFade(AnimNames.FemaleGreet01);
					this.Subtitle.UpdateLabel(SubtitleType.TaskInquiry, 0, 5.0f);
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.TalkTimer = 0.0f;
					}

					if (this.CharacterAnimation[AnimNames.FemaleGreet01].time >=
						this.CharacterAnimation[AnimNames.FemaleGreet01].length)
					{
						this.CharacterAnimation.CrossFade(this.IdleAnim);
					}

					if (this.TalkTimer <= 0.0f)
					{
						this.TargetStudent.Interaction = StudentInteractionType.TaskInquiry;
						this.TargetStudent.TalkTimer = 10.0f;
						this.Interaction = YandereInteractionType.Idle;
					}
				}

				this.TalkTimer -= Time.deltaTime;
			}

			////////////////////////
			///// GIVING SNACK /////
			////////////////////////

			else if (this.Interaction == YandereInteractionType.GivingSnack)
			{
				if (this.TalkTimer == 3.0f)
				{
					this.CharacterAnimation.CrossFade(AnimNames.FemaleGreet01);
					this.Subtitle.UpdateLabel(SubtitleType.OfferSnack, 0, 3.0f);
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.TalkTimer = 0.0f;
					}

					if (this.CharacterAnimation[AnimNames.FemaleGreet01].time >=
						this.CharacterAnimation[AnimNames.FemaleGreet01].length)
					{
						this.CharacterAnimation.CrossFade(this.IdleAnim);
					}

					if (this.TalkTimer <= 0.0f)
					{
						this.TargetStudent.Interaction = StudentInteractionType.TakingSnack;
						this.TargetStudent.TalkTimer = 5.0f;
						this.Interaction = YandereInteractionType.Idle;
					}
				}

				this.TalkTimer -= Time.deltaTime;
			}

			///////////////////////////
			///// ASKING FOR HELP /////
			///////////////////////////

			else if (this.Interaction == YandereInteractionType.AskingForHelp)
			{
				if (this.TalkTimer == 5.0f)
				{
					this.CharacterAnimation.CrossFade(this.IdleAnim);
					this.Subtitle.UpdateLabel(SubtitleType.AskForHelp, 0, 5.0f);
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.TalkTimer = 0.0f;
					}

					if (this.TalkTimer <= 0.0f)
					{
						this.TargetStudent.Interaction = StudentInteractionType.GivingHelp;
						this.TargetStudent.TalkTimer = 4.0f;
						this.Interaction = YandereInteractionType.Idle;
					}
				}

				this.TalkTimer -= Time.deltaTime;
			}

			/////////////////////////////
			///// SENDING TO LOCKER /////
			/////////////////////////////

			else if (this.Interaction == YandereInteractionType.SendingToLocker)
			{
				if (this.TalkTimer == 5.0f)
				{
					this.CharacterAnimation.CrossFade(AnimNames.FemaleGreet01);
					this.Subtitle.UpdateLabel(SubtitleType.SendToLocker, 0, 5.0f);
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.TalkTimer = 0.0f;
					}

					if (this.TalkTimer <= 0.0f)
					{
						this.TargetStudent.Interaction = StudentInteractionType.SentToLocker;
						this.TargetStudent.TalkTimer = 5.0f;
						this.Interaction = YandereInteractionType.Idle;
					}
				}

				this.TalkTimer -= Time.deltaTime;
			}
		}
	}

	void UpdateAttacking()
	{
		if (this.Attacking)
		{
			if (this.TargetStudent != null)
			{
				this.targetRotation = Quaternion.LookRotation(new Vector3(
					this.TargetStudent.transform.position.x,
					this.transform.position.y,
					this.TargetStudent.transform.position.z) - this.transform.position);
				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.targetRotation, Time.deltaTime * 10.0f);
			}

			if (this.Drown)
			{
				this.MoveTowardsTarget(this.TargetStudent.transform.position +
					(this.TargetStudent.transform.forward * -0.0001f));

				this.CharacterAnimation.CrossFade(this.DrownAnim);

				if (this.CharacterAnimation[this.DrownAnim].time >
					this.CharacterAnimation[this.DrownAnim].length)
				{
					this.TargetStudent.DeathType = DeathType.Drowning;
					this.Attacking = false;
					this.CanMove = true;
					this.Drown = false;

					// [af] Replaced if/else statement with ternary expression.
					this.Sanity -= ((PlayerGlobals.PantiesEquipped == 10) ? 10.0f : 20.0f) * this.Numbness;
				}
			}
			else if (this.RoofPush)
			{
				this.CameraTarget.position = Vector3.MoveTowards(
					this.CameraTarget.position,
					new Vector3(this.Hips.position.x, this.transform.position.y + 1.0f, this.Hips.position.z),
					Time.deltaTime * 10.0f);

				this.MoveTowardsTarget(this.TargetStudent.transform.position - this.TargetStudent.transform.forward);

				this.CharacterAnimation.CrossFade(AnimNames.FemaleRoofPushA);

				if (this.CharacterAnimation[AnimNames.FemaleRoofPushA].time > 4.33333333333f)
				{
					if (this.Shoes[0].activeInHierarchy)
					{
						GameObject NewShoePair = Instantiate(this.ShoePair,
							this.transform.position + new Vector3(0, 0.045f, 0) + (transform.forward * 1.6f), Quaternion.identity);

						NewShoePair.transform.eulerAngles = transform.eulerAngles;

						this.Shoes[0].SetActive(false);
						this.Shoes[1].SetActive(false);
					}
				}
				else if (this.CharacterAnimation[AnimNames.FemaleRoofPushA].time > 2.16666666667f)
				{
					if (this.TargetStudent.Schoolwear == 1 && !this.TargetStudent.ClubAttire)
					{
						if (!this.Shoes[0].activeInHierarchy)
						{
							this.TargetStudent.RemoveShoes();
							this.Shoes[0].SetActive(true);
							this.Shoes[1].SetActive(true);
						}
					}
				}

				float EndTime = 0.0f;

				if (this.TargetStudent.Schoolwear == 1 && !this.TargetStudent.ClubAttire)
				{
					EndTime = this.CharacterAnimation[AnimNames.FemaleRoofPushA].length;
				}
				else
				{
					EndTime = 3.5f;
				}

				if (this.CharacterAnimation[AnimNames.FemaleRoofPushA].time >
					EndTime)
				{
					this.CameraTarget.localPosition = new Vector3(0.0f, 1.0f, 0.0f);

					this.TargetStudent.DeathType = DeathType.Falling;
					this.SplashCamera.transform.parent = null;
					this.Attacking = false;
					this.RoofPush = false;
					this.CanMove = true;
					this.Sanity -= 20.0f * this.Numbness;
				}

				if (Input.GetButtonDown(InputNames.Xbox_B))
				{
					this.SplashCamera.transform.parent = this.transform;

					this.SplashCamera.transform.localPosition = new Vector3(2, -10.65f, 4.775f);
					this.SplashCamera.transform.localEulerAngles = new Vector3(0.0f, -135.0f, 0.0f);

					this.SplashCamera.Show = true;
					this.SplashCamera.MyCamera.enabled = true;
//					this.SplashCamera.transform.position = new Vector3(-33.0f, 1.35f, 30.0f);
//					this.SplashCamera.transform.eulerAngles = new Vector3(0.0f, 135.0f, 0.0f);
				}
			}
			else
			{
				if (this.TargetStudent.Teacher)
				{
					//this.CharacterAnimation.CrossFade(AnimNames.FemaleCounterA);
					this.CharacterAnimation.CrossFade("f02_teacherCounterA_00");

					if (this.EquippedWeapon != null)
					{
						this.EquippedWeapon.transform.localEulerAngles = new Vector3(0, 180, 0);
					}

					this.Character.transform.position = new Vector3(
						this.Character.transform.position.x,
						this.TargetStudent.transform.position.y,
						this.Character.transform.position.z);
				}
				else
				{
					if (!this.SanityBased)
					{
                        // The garrote.
                        if (this.EquippedWeapon.WeaponID == 27)
                        {
                            Debug.Log("Well, uh, we're killing with a garrote...");
                        }
                        // The energy sword.
                        else if (this.EquippedWeapon.WeaponID == 11)
						{
							this.CharacterAnimation.CrossFade("CyborgNinja_Slash");

							if (this.CharacterAnimation["CyborgNinja_Slash"].time == 0.0f)
							{
								this.TargetStudent.CharacterAnimation[this.TargetStudent.PhoneAnim].weight = 0.0f;
								this.EquippedWeapon.gameObject.GetComponent<AudioSource>().Play();
							}

							if (this.CharacterAnimation["CyborgNinja_Slash"].time >=
								this.CharacterAnimation["CyborgNinja_Slash"].length)
							{
								this.Bloodiness += 20.0f;
								this.StainWeapon();

								this.CharacterAnimation["CyborgNinja_Slash"].time = 0.0f;
								this.CharacterAnimation.Stop("CyborgNinja_Slash");
								this.CharacterAnimation.CrossFade(this.IdleAnim);
								this.Attacking = false;

								if (!this.Noticed)
								{
									this.CanMove = true;
								}
								else
								{
									this.EquippedWeapon.Drop();
								}
							}
						}
						// The circular saw.
						else if (this.EquippedWeapon.WeaponID == 7)
						{
							this.CharacterAnimation.CrossFade(AnimNames.FemaleBuzzSawKillA);

							if (this.CharacterAnimation[AnimNames.FemaleBuzzSawKillA].time == 0.0f)
							{
								this.TargetStudent.CharacterAnimation[this.TargetStudent.PhoneAnim].weight = 0.0f;
								this.EquippedWeapon.gameObject.GetComponent<AudioSource>().Play();
							}

							if (this.AttackPhase == 1)
							{
								if (this.CharacterAnimation[AnimNames.FemaleBuzzSawKillA].time > (1.0f / 3.0f))
								{
									this.TargetStudent.LiquidProjector.enabled = true;
									this.EquippedWeapon.Effect();
									this.StainWeapon();

									this.TargetStudent.LiquidProjector.material.mainTexture = this.BloodTextures[1];
									this.Bloodiness += 20.0f;
									this.AttackPhase++;
								}
							}
							else if (this.AttackPhase < 6)
							{
								if (this.CharacterAnimation[AnimNames.FemaleBuzzSawKillA].time >
									((1.0f / 3.0f) * this.AttackPhase))
								{
									this.TargetStudent.LiquidProjector.material.mainTexture = this.BloodTextures[this.AttackPhase];
									this.Bloodiness += 20.0f;
									this.AttackPhase++;
								}
							}

							if (this.CharacterAnimation[AnimNames.FemaleBuzzSawKillA].time >
								this.CharacterAnimation[AnimNames.FemaleBuzzSawKillA].length)
							{
								if (this.TargetStudent == this.StudentManager.Reporter)
								{
									this.StudentManager.Reporter = null;
								}

								this.CharacterAnimation[AnimNames.FemaleBuzzSawKillA].time = 0.0f;
								this.CharacterAnimation.Stop(AnimNames.FemaleBuzzSawKillA);
								this.CharacterAnimation.CrossFade(this.IdleAnim);
								this.MyController.radius = 0.20f;
								this.Attacking = false;
								this.AttackPhase = 1;

								this.Sanity -= 20.0f * this.Numbness;
								this.TargetStudent.DeathType = DeathType.Weapon;
								this.TargetStudent.BecomeRagdoll();

								if (!this.Noticed)
								{
									this.CanMove = true;
								}
								else
								{
									this.EquippedWeapon.Drop();
								}
							}
						}
						// Any other weapon.
						else
						{
							// A long weapon.
							if (!this.EquippedWeapon.Concealable)
							{
								if (this.AttackPhase == 1)
								{
									this.CharacterAnimation.CrossFade(AnimNames.FemaleSwingA);

									if (this.CharacterAnimation[AnimNames.FemaleSwingA].time >
										(this.CharacterAnimation[AnimNames.FemaleSwingA].length * 0.30f))
									{
										if (this.TargetStudent == this.StudentManager.Reporter)
										{
											this.StudentManager.Reporter = null;
										}

										Destroy(this.TargetStudent.DeathScream);
										this.EquippedWeapon.Effect();
										this.AttackPhase = 2;

										this.Bloodiness += 20.0f;
										this.StainWeapon();

										this.Sanity -= 20.0f * this.Numbness;
									}
								}
								else
								{
									if (this.CharacterAnimation[AnimNames.FemaleSwingA].time >=
										(this.CharacterAnimation[AnimNames.FemaleSwingA].length * 0.90f))
									{
										this.CharacterAnimation.CrossFade(this.IdleAnim);

										this.TargetStudent.DeathType = DeathType.Weapon;
										this.TargetStudent.BecomeRagdoll();

										this.MyController.radius = 0.20f;
										this.Attacking = false;
										this.AttackPhase = 1;
										this.AttackTimer = 0.0f;

										if (!this.Noticed)
										{
											this.CanMove = true;
										}
										else
										{
											this.EquippedWeapon.Drop();
										}
									}
								}
							}
							// A short weapon.
							else
							{
								if (this.AttackPhase == 1)
								{
									this.CharacterAnimation.CrossFade(AnimNames.FemaleStab);

									if (this.CharacterAnimation[AnimNames.FemaleStab].time >
										(this.CharacterAnimation[AnimNames.FemaleStab].length * 0.35f))
									{
										this.CharacterAnimation.CrossFade(this.IdleAnim);

										if (this.EquippedWeapon.Flaming)
										{
											this.Egg = true;
											this.TargetStudent.Combust();
										}
										else if (this.CanTranq && !this.TargetStudent.Male && this.TargetStudent.Club != ClubType.Council)
										{
											this.TargetStudent.Tranquil = true;
											this.CanTranq = false;
                                            this.Follower = null;
											this.Followers--;
										}
										else
										{
											this.TargetStudent.BloodSpray.SetActive(true);
											this.TargetStudent.DeathType = DeathType.Weapon;
											this.Bloodiness += 20.0f;
										}

										if (this.TargetStudent == this.StudentManager.Reporter)
										{
											this.StudentManager.Reporter = null;
										}

										AudioSource.PlayClipAtPoint(this.Stabs[Random.Range(0, this.Stabs.Length)],
											this.transform.position + Vector3.up);
										Destroy(this.TargetStudent.DeathScream);
										this.AttackPhase = 2;
										this.Sanity -= 20.0f * this.Numbness;

										if (this.EquippedWeapon.WeaponID == 8)
										{
											this.TargetStudent.Ragdoll.Sacrifice = true;

											if (GameGlobals.Paranormal)
											{
												this.EquippedWeapon.Effect();
											}
										}
									}
								}
								else
								{
									this.AttackTimer += Time.deltaTime;

									if (this.AttackTimer > 0.30f)
									{
										if (!this.CanTranq)
										{
											this.StainWeapon();
										}

										this.MyController.radius = 0.20f;
										this.SanityBased = true;
										this.Attacking = false;
										this.AttackPhase = 1;
										this.AttackTimer = 0.0f;

										if (!this.Noticed)
										{
											this.CanMove = true;
										}
										else
										{
											this.EquippedWeapon.Drop();
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}

	public void UpdateSlouch()
	{
		if (this.CanMove && !this.Attacking && !this.Dragging && (this.PickUp == null) &&
			!this.Aiming && (this.Stance.Current != StanceType.Crawling) && !this.Possessed &&
			!this.Carrying && !this.CirnoWings.activeInHierarchy && (this.LaughIntensity < 16.0f))
		{
			this.CharacterAnimation[AnimNames.FemaleYanderePose].weight = Mathf.Lerp(
				this.CharacterAnimation[AnimNames.FemaleYanderePose].weight,
				1.0f - (this.Sanity / 100.0f),
				Time.deltaTime * 10.0f);

			if ((this.Hairstyle == 2) && (this.Stance.Current == StanceType.Crouching))
			{
				this.Slouch = Mathf.Lerp(this.Slouch, 0.0f, Time.deltaTime * 20.0f);
			}
			else
			{
				this.Slouch = Mathf.Lerp(
					this.Slouch, 5.0f * (1.0f - (this.Sanity / 100.0f)), Time.deltaTime * 10.0f);
			}
		}
		else
		{
			this.CharacterAnimation[AnimNames.FemaleYanderePose].weight = Mathf.Lerp(
				this.CharacterAnimation[AnimNames.FemaleYanderePose].weight, 0.0f, Time.deltaTime * 10.0f);

			this.Slouch = Mathf.Lerp(this.Slouch, 0.0f, Time.deltaTime * 10.0f);
		}
	}

	void UpdateTwitch()
	{
		if (this.Sanity < 100.0f)
		{
			this.TwitchTimer += Time.deltaTime;

			if (this.TwitchTimer > this.NextTwitch)
			{
				this.Twitch = new Vector3(
					(1.0f - (this.Sanity / 100.0f)) * Random.Range(-10.0f, 10.0f),
					(1.0f - (this.Sanity / 100.0f)) * Random.Range(-10.0f, 10.0f),
					(1.0f - (this.Sanity / 100.0f)) * Random.Range(-10.0f, 10.0f));

				this.NextTwitch = Random.Range(0.0f, 1.0f);

				this.TwitchTimer = 0.0f;
			}

			this.Twitch = Vector3.Lerp(this.Twitch, Vector3.zero, Time.deltaTime * 10.0f);
		}
	}

	void UpdateWarnings()
	{
		if (this.NearBodies > 0)
		{
			if (!this.CorpseWarning)
			{
				this.NotificationManager.DisplayNotification(NotificationType.Body);

				this.StudentManager.UpdateStudents();

				this.CorpseWarning = true;
			}
		}
		else
		{
			if (this.CorpseWarning)
			{
				this.StudentManager.UpdateStudents();

				this.CorpseWarning = false;
			}
		}

		if (this.Eavesdropping)
		{
			if (!this.EavesdropWarning)
			{
				this.NotificationManager.DisplayNotification(NotificationType.Eavesdropping);

				this.EavesdropWarning = true;
			}
		}
		else
		{
			if (this.EavesdropWarning)
			{
				this.EavesdropWarning = false;
			}
		}

		if (this.ClothingWarning)
		{
			this.ClothingTimer += Time.deltaTime;

			if (this.ClothingTimer > 1)
			{
				this.ClothingWarning = false;
				this.ClothingTimer = 0;
			}
		}
	}

	void UpdateDebugFunctionality()
	{
		if (!this.EasterEggMenu.activeInHierarchy && !this.DebugMenu.activeInHierarchy)
		{
			if (!this.Aiming && this.CanMove && (Time.timeScale > 0.0f))
			{
				if (Input.GetKeyDown(KeyCode.Escape))
				{
					this.PauseScreen.JumpToQuit();
				}
			}

			if (Input.GetKeyDown(KeyCode.P))
			{
				this.CyborgParts[1].SetActive(false);
				this.MemeGlasses.SetActive(false);
				this.KONGlasses.SetActive(false);
				this.EyepatchR.SetActive(false);
				this.EyepatchL.SetActive(false);

				this.EyewearID++;

				if (this.EyewearID == 1)
				{
					this.EyepatchR.SetActive(true);
				}
				else if (this.EyewearID == 2)
				{
					this.EyepatchL.SetActive(true);
				}
				else if (this.EyewearID == 3)
				{
					this.EyepatchR.SetActive(true);
					this.EyepatchL.SetActive(true);
				}
				else if (this.EyewearID == 4)
				{
					this.KONGlasses.SetActive(true);
				}
				else if (this.EyewearID == 5)
				{
					this.MemeGlasses.SetActive(true);
				}
				else if (this.EyewearID == 6)
				{
					if (this.CyborgParts[2].activeInHierarchy)
					{
						this.CyborgParts[1].SetActive(true);
					}
					else
					{
						this.EyewearID = 0;
					}
				}
				else
				{
					this.EyewearID = 0;
				}
			}

			if (Input.GetKeyDown(KeyCode.H))
			{
				if (Input.GetButton(InputNames.Xbox_LB))
				{
					this.Hairstyle += 10;
				}
				else
				{
					this.Hairstyle++;
				}

				this.UpdateHair();
			}

			if (Input.GetKey(KeyCode.H))
			{
				if (Input.GetKeyDown(KeyCode.LeftArrow))
				{
					this.Hairstyle--;
					this.UpdateHair();
				}
			}

			if (Input.GetKeyDown(KeyCode.O))
			{
				if (!this.EasterEggMenu.activeInHierarchy)
				{
					if (this.AccessoryID > 0)
					{
						this.Accessories[this.AccessoryID].SetActive(false);
					}

					if (Input.GetButton(InputNames.Xbox_LB))
					{
						this.AccessoryID += 10;
					}
					else
					{
						this.AccessoryID++;
					}

					this.UpdateAccessory();
				}
			}

			if (Input.GetKey(KeyCode.O))
			{
				if (Input.GetKeyDown(KeyCode.LeftArrow))
				{
					if (this.AccessoryID > 0)
					{
						this.Accessories[this.AccessoryID].SetActive(false);
					}

					this.AccessoryID--;
					this.UpdateAccessory();
				}
			}

            #if UNITY_EDITOR
            if (Input.GetKeyDown("-"))
            {
                if (Time.timeScale < 6.0f)
                {
                    Time.timeScale = 1.0f;
                }
                else
                {
                    Time.timeScale -= 5.0f;
                }
            }

            if (Input.GetKeyDown("="))
            {
                if (Time.timeScale < 5.0f)
                {
                    Time.timeScale = 5.0f;
                }
                else
                {
                    Time.timeScale += 5.0f;

                    if (Time.timeScale > 25.0f)
                    {
                        Time.timeScale = 25.0f;
                    }
                }
            }
            #endif

            if (!this.NoDebug)
			{
				if (!this.DebugMenu.activeInHierarchy

				#if UNITY_EDITOR

				&& !this.CensorSteam[0].activeInHierarchy

				#endif

				)

				{
					if (this.CanMove && !this.DebugMenu.activeInHierarchy)
					{
                        #if !UNITY_EDITOR
                        if (Input.GetKeyDown("-"))
						{
							if (Time.timeScale < 6.0f)
							{
								Time.timeScale = 1.0f;
							}
							else
							{
								Time.timeScale -= 5.0f;
							}
						}

						if (Input.GetKeyDown("="))
						{
							if (Time.timeScale < 5.0f)
							{
								Time.timeScale = 5.0f;
							}
							else
							{
								Time.timeScale += 5.0f;

								if (Time.timeScale > 25.0f)
								{
									Time.timeScale = 25.0f;
								}
							}
						}
                        #endif
                    }
                }
			}

			if (Input.GetKey(KeyCode.Period))
			{
				this.BreastSize += Time.deltaTime;

				if (this.BreastSize > 2.0f)
				{
					this.BreastSize = 2.0f;
				}

				this.RightBreast.localScale = new Vector3(this.BreastSize, this.BreastSize, this.BreastSize);
				this.LeftBreast.localScale = new Vector3(this.BreastSize, this.BreastSize, this.BreastSize);
			}

			if (Input.GetKey(KeyCode.Comma))
			{
				this.BreastSize -= Time.deltaTime;

				if (this.BreastSize < 0.50f)
				{
					this.BreastSize = 0.50f;
				}

				this.RightBreast.localScale = new Vector3(this.BreastSize, this.BreastSize, this.BreastSize);
				this.LeftBreast.localScale = new Vector3(this.BreastSize, this.BreastSize, this.BreastSize);
			}
		}

		if (!this.NoDebug)
		{
			if (this.CanMove)
			{
				if (!this.Egg || this.EggBypass > 8)
				{
					if (this.transform.position.y < 1000.0f)
					{
						if (Input.GetKeyDown(KeyCode.Slash))
						{
							this.DebugMenu.SetActive(false);

							// [af] Replaced if/else statement with boolean expression.
							this.EasterEggMenu.SetActive(!this.EasterEggMenu.activeInHierarchy);
						}

						if (this.EasterEggMenu.activeInHierarchy)
						{
							if (Input.GetKeyDown(KeyCode.P))
							{
								this.Punish();
							}
							else if (Input.GetKeyDown(KeyCode.Z))
							{
								this.Slend();
							}
							else if (Input.GetKeyDown(KeyCode.B))
							{
								this.Bancho();
							}
							else if (Input.GetKeyDown(KeyCode.C))
							{
								this.Cirno();
							}
							else if (Input.GetKeyDown(KeyCode.H))
							{
								this.EmptyHands();
								this.Hate();
							}
							else if (Input.GetKeyDown(KeyCode.T))
							{
								this.StudentManager.AttackOnTitan();
								this.AttackOnTitan();
							}
							else if (Input.GetKeyDown(KeyCode.G))
							{
								this.GaloSengen();
							}
							else if (Input.GetKeyDown(KeyCode.J))
							{
#if UNITY_EDITOR
								this.Jojo();
#endif
							}
							else if (Input.GetKeyDown(KeyCode.K))
							{
								this.EasterEggMenu.SetActive(false);
								this.StudentManager.Kong();
								this.DK = true;
							}
							else if (Input.GetKeyDown(KeyCode.L))
							{
								this.Agent();
							}
							else if (Input.GetKeyDown(KeyCode.N))
							{
#if UNITY_EDITOR
                                this.Nude();
#endif
                            }
                            else if (Input.GetKeyDown(KeyCode.S))
							{
								this.EasterEggMenu.SetActive(false);
								this.Egg = true;
								this.StudentManager.Spook();
							}
							else if (Input.GetKeyDown(KeyCode.F))
							{
								this.EasterEggMenu.SetActive(false);
								this.Falcon();
							}
							else if (Input.GetKeyDown(KeyCode.X))
							{
								this.EasterEggMenu.SetActive(false);
								this.X();
							}
							else if (Input.GetKeyDown(KeyCode.O))
							{
								this.EasterEggMenu.SetActive(false);
								this.Punch();
							}
							else if (Input.GetKeyDown(KeyCode.U))
							{
								this.EasterEggMenu.SetActive(false);
								this.BadTime();
							}
							else if (Input.GetKeyDown(KeyCode.Y))
							{
								this.EasterEggMenu.SetActive(false);
								this.CyborgNinja();
							}
							else if (Input.GetKeyDown(KeyCode.E))
							{
								this.EasterEggMenu.SetActive(false);
								this.Ebola();
							}
							else if (Input.GetKeyDown(KeyCode.Q))
							{
								this.EasterEggMenu.SetActive(false);
								this.Samus();
							}
							else if (Input.GetKeyDown(KeyCode.W))
							{
								this.EasterEggMenu.SetActive(false);
								this.Witch();
							}
							else if (Input.GetKeyDown(KeyCode.R))
							{
								this.EasterEggMenu.SetActive(false);
								this.Pose();
							}
							else if (Input.GetKeyDown(KeyCode.V))
							{
								this.EasterEggMenu.SetActive(false);
								this.Vaporwave();
							}
							else if (Input.GetKeyDown(KeyCode.Alpha2))
							{
								this.EasterEggMenu.SetActive(false);
								this.HairBlades();
							}
							else if (Input.GetKeyDown(KeyCode.Alpha7))
							{
								this.EasterEggMenu.SetActive(false);
								this.Tornado();
							}
							else if (Input.GetKeyDown(KeyCode.Alpha8))
							{
								this.EasterEggMenu.SetActive(false);
								this.GenderSwap();
							}
							else if (Input.GetKeyDown("[5]")) // @todo: Is this used?
							{
								this.EasterEggMenu.SetActive(false);
								this.SwapMesh();
							}
							else if (Input.GetKeyDown(KeyCode.A))
							{
								/*
								this.StudentManager.ChangeOka();

								#if UNITY_EDITOR
								this.StudentManager.ChangeRibaru();
								#endif
								*/

								this.EasterEggMenu.SetActive(false);
							}
							else if (Input.GetKeyDown(KeyCode.I))
							{
								this.StudentManager.NoGravity = true;
								this.EasterEggMenu.SetActive(false);
							}
							else if (Input.GetKeyDown(KeyCode.D))
							{
								this.EasterEggMenu.SetActive(false);
								this.Sith();
							}
							else if (Input.GetKeyDown(KeyCode.M))
							{
								this.EasterEggMenu.SetActive(false);
								this.Snake();
							}
							else if (Input.GetKeyDown(KeyCode.Alpha1))
							{
								this.EasterEggMenu.SetActive(false);
								this.Gazer();
							}
							else if (Input.GetKeyDown(KeyCode.Alpha3))
							{
								this.StudentManager.SecurityCameras();
								this.EasterEggMenu.SetActive(false);
							}
							else if (Input.GetKeyDown(KeyCode.Alpha4))
							{
								this.KLK();
								this.EasterEggMenu.SetActive(false);
							}
							else if (Input.GetKeyDown(KeyCode.Alpha6))
							{
								this.EasterEggMenu.SetActive(false);
								this.Six();
							}
							else if (Input.GetKeyDown(KeyCode.F1))
							{
								this.Weather();
								this.EasterEggMenu.SetActive(false);
							}
							else if (Input.GetKeyDown(KeyCode.F2))
							{
								this.Horror();
								this.EasterEggMenu.SetActive(false);
							}
							else if (Input.GetKeyDown(KeyCode.F3))
							{
								this.LifeNote();
								this.EasterEggMenu.SetActive(false);
							}
							else if (Input.GetKeyDown(KeyCode.F4))
							{
								this.Mandere();
								this.EasterEggMenu.SetActive(false);
							}
							else if (Input.GetKeyDown(KeyCode.F5))
							{
								this.BlackHoleChan();
								this.EasterEggMenu.SetActive(false);
							}
							else if (Input.GetKeyDown(KeyCode.F6))
							{
								this.ElfenLied();
								this.EasterEggMenu.SetActive(false);
							}
							else if (Input.GetKeyDown(KeyCode.F7))
							{
								this.Berserk();
								this.EasterEggMenu.SetActive(false);
							}
							else if (Input.GetKeyDown(KeyCode.F8))
							{
								this.Nier();
								this.EasterEggMenu.SetActive(false);
							}
							else if (Input.GetKeyDown(KeyCode.F9))
							{
								this.Ghoul();
								this.EasterEggMenu.SetActive(false);
							}
							else if (Input.GetKeyDown(KeyCode.F10))
							{
								this.CinematicCameraFilters.enabled = true;
								this.CameraFilters.enabled = true;
								this.EasterEggMenu.SetActive(false);
							}
							else if (Input.GetKeyDown(KeyCode.F11))
							{
#if UNITY_EDITOR
                                this.Blacklight();
								this.EasterEggMenu.SetActive(false);
#endif
                            }
							else if (Input.GetKeyDown(KeyCode.Space))
							{
								#if UNITY_EDITOR
								//this.Miyuki();
								#endif

								#if UNITY_EDITOR
								//this.KizunaAI();
								#endif

								this.EasterEggMenu.SetActive(false);
							}
						}
					}
				}
				else
				{
					if (Input.GetKeyDown(KeyCode.Slash))
					{
						this.EggBypass++;
					}
				}
			}
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.Z))
			{
				this.DebugMenu.transform.parent.GetComponent<DebugMenuScript>().Censor();
			}
		}
	}

	public GameObject CreepyArms;

	void LateUpdate()
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

		this.LeftEye.localPosition = new Vector3(
			this.LeftEye.localPosition.x,
			this.LeftEye.localPosition.y,// - (this.EyeShrink * 0.005f),
			this.LeftEyeOrigin.z - (this.EyeShrink * 0.020f));

		this.RightEye.localPosition = new Vector3(
			this.RightEye.localPosition.x,
			this.RightEye.localPosition.y,// - (this.EyeShrink * 0.005f),
			this.RightEyeOrigin.z + (this.EyeShrink * 0.020f));

		this.LeftEye.localScale = new Vector3(
			1.0f - (this.EyeShrink),// * 0.50f),
			1.0f - (this.EyeShrink),// * 0.50f),
			this.LeftEye.localScale.z);

		this.RightEye.localScale = new Vector3(
			1.0f - (this.EyeShrink),// * 0.50f),
			1.0f - (this.EyeShrink),// * 0.50f),
			this.RightEye.localScale.z);

		// [af] Converted while loop to for loop.
		for (this.ID = 0; this.ID < this.Spine.Length; this.ID++)
		{
			Transform spineTransform = this.Spine[this.ID].transform;
			spineTransform.localEulerAngles = new Vector3(
				spineTransform.localEulerAngles.x + this.Slouch,
				spineTransform.localEulerAngles.y,
				spineTransform.localEulerAngles.z);
		}

		if (this.Aiming)
		{
			float Inversion = 1;

			if (this.Selfie)
			{
				Inversion = -1;
			}

			Transform spine3Transform = this.Spine[3].transform;
			spine3Transform.localEulerAngles = new Vector3(
				spine3Transform.localEulerAngles.x - (this.Bend * Inversion),
				spine3Transform.localEulerAngles.y,
				spine3Transform.localEulerAngles.z);
		}

		float SlouchBonus = 1;

		if (this.Stance.Current == StanceType.Crouching)
		{
			SlouchBonus = 3.66666f;
		}

		Transform arm0Transform = this.Arm[0].transform;
		arm0Transform.localEulerAngles = new Vector3(
			arm0Transform.localEulerAngles.x,
			arm0Transform.localEulerAngles.y,
			arm0Transform.localEulerAngles.z - (this.Slouch * (3 + SlouchBonus)));

		Transform arm1Transform = this.Arm[1].transform;
		arm1Transform.localEulerAngles = new Vector3(
			arm1Transform.localEulerAngles.x,
			arm1Transform.localEulerAngles.y,
			arm1Transform.localEulerAngles.z + (this.Slouch * (3 + SlouchBonus)));

		// [af] Commented in JS code.
		//RightBreast.localScale = Vector3(BreastSize, BreastSize, BreastSize);
		//LeftBreast.localScale = Vector3(BreastSize, BreastSize, BreastSize);

		if (!this.Aiming)
		{
			this.Head.localEulerAngles += this.Twitch;
		}

		if (this.Aiming)
		{
			if (this.Stance.Current == StanceType.Crawling)
			{
				this.TargetHeight = -1.40f;
			}
			else if (this.Stance.Current == StanceType.Crouching)
			{
				this.TargetHeight = -0.60f;
			}
			else
			{
				this.TargetHeight = 0.0f;
			}

			this.Height = Mathf.Lerp(this.Height, this.TargetHeight, Time.deltaTime * 10.0f);

			this.PelvisRoot.transform.localPosition = new Vector3(
				this.PelvisRoot.transform.localPosition.x,
				this.Height,
				this.PelvisRoot.transform.localPosition.z);
		}

		if (this.Egg)
		{
			if (this.Slender)
			{
				Transform leg0 = this.Leg[0];
				leg0.localScale = new Vector3(
					leg0.localScale.x,
					2.0f,
					leg0.localScale.z);

				Transform foot0 = this.Foot[0];
				foot0.localScale = new Vector3(
					foot0.localScale.x,
					0.50f,
					foot0.localScale.z);

				Transform leg1 = this.Leg[1];
				leg1.localScale = new Vector3(
					leg1.localScale.x,
					2.0f,
					leg1.localScale.z);

				Transform foot1 = this.Foot[1];
				foot1.localScale = new Vector3(
					foot1.localScale.x,
					0.50f,
					foot1.localScale.z);

				Transform arm0 = this.Arm[0];
				arm0.localScale = new Vector3(
					2.0f,
					arm0.localScale.y,
					arm0.localScale.z);

				Transform arm1 = this.Arm[1];
				arm1.localScale = new Vector3(
					2.0f,
					arm1.localScale.y,
					arm1.localScale.z);
			}

			if (this.DK)
			{
				this.Arm[0].localScale = new Vector3(2.0f, 2.0f, 2.0f);
				this.Arm[1].localScale = new Vector3(2.0f, 2.0f, 2.0f);
				this.Head.localScale = new Vector3(2.0f, 2.0f, 2.0f);
			}

			if (this.CirnoWings.activeInHierarchy)
			{
				if (this.Running)
				{
					this.FlapSpeed = 5.0f;
				}
				else
				{
					if (this.FlapSpeed == 0.0f)
					{
						this.FlapSpeed = 1.0f;
					}
					else
					{
						this.FlapSpeed = 3.0f;
					}
				}

				Transform cirnoWing0 = this.CirnoWing[0];
				Transform cirnoWing1 = this.CirnoWing[1];

				if (!this.FlapOut)
				{
					this.CirnoRotation += Time.deltaTime * 100.0f * this.FlapSpeed;

					cirnoWing0.localEulerAngles = new Vector3(
						cirnoWing0.localEulerAngles.x,
						this.CirnoRotation,
						cirnoWing0.localEulerAngles.z);

					cirnoWing1.localEulerAngles = new Vector3(
						cirnoWing1.localEulerAngles.x,
						-this.CirnoRotation,
						cirnoWing1.localEulerAngles.z);

					if (this.CirnoRotation > 15.0f)
					{
						this.FlapOut = true;
					}
				}
				else
				{
					this.CirnoRotation -= Time.deltaTime * 100.0f * this.FlapSpeed;

					cirnoWing0.localEulerAngles = new Vector3(
						cirnoWing0.localEulerAngles.x,
						this.CirnoRotation,
						cirnoWing0.localEulerAngles.z);

					cirnoWing1.localEulerAngles = new Vector3(
						cirnoWing1.localEulerAngles.x,
						-this.CirnoRotation,
						cirnoWing1.localEulerAngles.z);

					if (this.CirnoRotation < -15.0f)
					{
						this.FlapOut = false;
					}
				}
			}

			if (this.SpiderLegs.activeInHierarchy)
			{
				if (this.SpiderGrow)
				{
					if (this.SpiderLegs.transform.localScale.x < .49f)
					{
						this.SpiderLegs.transform.localScale = Vector3.Lerp(this.SpiderLegs.transform.localScale, new Vector3(.5f, .5f, .5f), Time.deltaTime * 5);
						SchoolGlobals.SchoolAtmosphere = 1 - this.SpiderLegs.transform.localScale.x;
						this.StudentManager.SetAtmosphere();
					}
				}
				else
				{
					if (this.SpiderLegs.transform.localScale.x > .001f)
					{
						this.SpiderLegs.transform.localScale = Vector3.Lerp(this.SpiderLegs.transform.localScale, new Vector3(0, 0, 0), Time.deltaTime * 5);
						SchoolGlobals.SchoolAtmosphere = 1 - this.SpiderLegs.transform.localScale.x;
						this.StudentManager.SetAtmosphere();
					}
				}
			}

			if (this.BlackHole)
			{
				this.RightLeg.transform.Rotate(-60, 0, 0, Space.Self);
				this.LeftLeg.transform.Rotate(-60, 0, 0, Space.Self);
			}

			if (this.SwingKagune)
			{
				if (!this.ReturnKagune)
				{
					int tempID = 0;

					while (tempID < Kagune.Length)
					{
						if (tempID < 2)
						{
							KaguneRotation[tempID] = new Vector3 (
								Mathf.MoveTowards(KaguneRotation[tempID].x, 15, Time.deltaTime * 1000),
								Mathf.MoveTowards(KaguneRotation[tempID].y, 180, Time.deltaTime * 1000),
								Mathf.MoveTowards(KaguneRotation[tempID].z, 500, Time.deltaTime * 1000));

							this.Kagune[tempID].transform.localEulerAngles = KaguneRotation[tempID];
						}
						else
						{
							KaguneRotation[tempID] = new Vector3 (
								Mathf.MoveTowards(KaguneRotation[tempID].x, 15, Time.deltaTime * 1000),
								Mathf.MoveTowards(KaguneRotation[tempID].y, -180, Time.deltaTime * 1000),
								Mathf.MoveTowards(KaguneRotation[tempID].z, -500, Time.deltaTime * 1000));

							this.Kagune[tempID].transform.localEulerAngles = KaguneRotation[tempID];
						}

						tempID++;
					}

					if (KagunePhase == 0)
					{
						if (KaguneRotation[0].y == 180)
						{
							Instantiate(DemonSlash, transform.position + transform.up + transform.forward, Quaternion.identity);
							KagunePhase = 1;
						}
					}

					if (KaguneRotation[0] == new Vector3(15, 180, 500))
					{
						this.ReturnKagune = true;
					}
				}
				else
				{
					int tempID = 0;

					while (tempID < Kagune.Length)
					{
						if (tempID == 0)
						{
							KaguneRotation[tempID] = new Vector3 (
								Mathf.Lerp(KaguneRotation[tempID].x, 22.5f, Time.deltaTime * 10),
								Mathf.Lerp(KaguneRotation[tempID].y, 22.5f, Time.deltaTime * 10),
								Mathf.Lerp(KaguneRotation[tempID].z, 0, Time.deltaTime * 10));
						}
						else if (tempID == 1)
						{
							KaguneRotation[tempID] = new Vector3 (
								Mathf.Lerp(KaguneRotation[tempID].x, -22.5f, Time.deltaTime * 10),
								Mathf.Lerp(KaguneRotation[tempID].y, 22.5f, Time.deltaTime * 10),
								Mathf.Lerp(KaguneRotation[tempID].z, 0, Time.deltaTime * 10));
						}
						else if (tempID == 2)
						{
							KaguneRotation[tempID] = new Vector3 (
								Mathf.Lerp(KaguneRotation[tempID].x, 22.5f, Time.deltaTime * 10),
								Mathf.Lerp(KaguneRotation[tempID].y, -22.5f, Time.deltaTime * 10),
								Mathf.Lerp(KaguneRotation[tempID].z, 0, Time.deltaTime * 10));
						}
						else if (tempID == 3)
						{
							KaguneRotation[tempID] = new Vector3 (
								Mathf.Lerp(KaguneRotation[tempID].x, -22.5f, Time.deltaTime * 10),
								Mathf.Lerp(KaguneRotation[tempID].y, -22.5f, Time.deltaTime * 10),
								Mathf.Lerp(KaguneRotation[tempID].z, 0, Time.deltaTime * 10));
						}
							
						this.Kagune[tempID].transform.localEulerAngles = KaguneRotation[tempID];

						tempID++;
					}

					if (Vector3.Distance(KaguneRotation[0], new Vector3(22.5f, 22.5f, 0)) < 10f)
					{
						this.SwingKagune = false;
						this.ReturnKagune = false;
						this.KagunePhase = 0;
					}
				}
			}
		}

		if (this.PickUp != null)
		{
			if (this.PickUp.Flashlight)
			{
				this.RightHand.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
			}
		}

		if (this.ReachWeight > 0)
		{
			this.CharacterAnimation["f02_reachForWeapon_00"].weight = ReachWeight;

			this.ReachWeight = Mathf.MoveTowards(this.ReachWeight, 0, Time.deltaTime * 2);

			if (this.ReachWeight < .01f)
			{
				this.ReachWeight = 0;
				this.CharacterAnimation["f02_reachForWeapon_00"].weight = 0;
			}
		}

		//Debug.Log("SanitySmudges.a is " + this.SanitySmudges.color.a);
		//Debug.Log("1 - (this.sanity / 100.0f)" + " is " + (1 - (this.sanity / 100.0f)));

		if (this.SanitySmudges.color.a > (1 - ((this.sanity / 100.0f))) + .0001f ||
			this.SanitySmudges.color.a < (1 - ((this.sanity / 100.0f))) - .0001f  )
		{
			//Debug.Log("Sanity is: " + this.sanity);

			float alpha = this.SanitySmudges.color.a;

			alpha = Mathf.MoveTowards(alpha, 1 - (this.sanity / 100.0f), Time.deltaTime);

			this.SanitySmudges.color = new Color(1, 1, 1, alpha);

			this.StudentManager.SelectiveGreyscale.desaturation = (1.0f - this.StudentManager.Atmosphere) + alpha;

			//Debug.Log("Yandere-chan's Sanity is setting the desaturation to: " + this.StudentManager.SelectiveGreyscale.desaturation);

			if (alpha > .66666f)
			{
				float darkness = 1 - ((1 - alpha) / .33333f);

				//Debug.Log("Alpha is " + alpha + " and Darkness is " + darkness);

				this.StudentManager.SetFaces(darkness);
			}
			else
			{
				//Debug.Log("Alpha is " + alpha + " and Darkness is 0.");

				this.StudentManager.SetFaces(0);
			}

			float sanityNumber = 100 - (alpha * 100);

			SanityLabel.text = sanityNumber.ToString("0") + "%";
		}

		if (this.CanMove)
		{
			if (this.sanity < 33.333f)
			{
				if (!this.NearSenpai)
				{
					this.GiggleTimer += Time.deltaTime * (1 - (sanity / 33.333f));

					if (this.GiggleTimer > 10)
					{
						Instantiate(this.GiggleDisc, this.transform.position + Vector3.up, Quaternion.identity);
						AudioSource.PlayClipAtPoint(this.CreepyGiggles[Random.Range(0, this.CreepyGiggles.Length)], transform.position);
						InsaneLines.Play();

						this.GiggleTimer = 0;
					}
				}
			}
		}

		if (this.FightHasBrokenUp)
		{
			this.BreakUpTimer = Mathf.MoveTowards(this.BreakUpTimer, 0, Time.deltaTime);

			if (this.BreakUpTimer == 0)
			{
				this.FightHasBrokenUp = false;
				this.SeenByAuthority = false;
			}
		}
	}

	public void StainWeapon()
	{
		if (this.EquippedWeapon != null)
		{
			if (this.TargetStudent != null)
			{
				if (this.TargetStudent.StudentID < this.EquippedWeapon.Victims.Length)
				{
					this.EquippedWeapon.Victims[this.TargetStudent.StudentID] = true;
				}
			}

			this.EquippedWeapon.Blood.enabled = true;
			this.EquippedWeapon.MurderWeapon = true;

			if (!this.NoStainGloves)
			{
				if (this.Gloved)
				{
					if (!this.Gloves.Blood.enabled)
					{
						this.GloveAttacher.newRenderer.material.mainTexture = this.BloodyGloveTexture;
						this.Gloves.PickUp.Evidence = true;
						this.Gloves.Blood.enabled = true;
						this.GloveBlood = 1;

						this.Police.BloodyClothing++;
					}
				}
			}

			this.NoStainGloves = false;

			if (this.Mask != null)
			{
				if (!this.Mask.Blood.enabled)
				{
					this.Mask.PickUp.Evidence = true;
					this.Mask.Blood.enabled = true;
					this.Police.BloodyClothing++;
				}
			}

			if (!this.EquippedWeapon.Evidence)
			{
				this.EquippedWeapon.Evidence = true;
				this.Police.MurderWeapons++;
			}
		}
	}

	public void MoveTowardsTarget(Vector3 target)
	{
		Vector3 offset = target - this.transform.position;
		this.MyController.Move(offset * (Time.deltaTime * 10.0f));
	}

	public void StopAiming()
	{
		this.UpdateAccessory();
		this.UpdateHair();

		this.CharacterAnimation[AnimNames.FemaleCameraPose].weight = 0.0f;
		this.CharacterAnimation["f02_selfie_00"].weight = 0.0f;

		this.PelvisRoot.transform.localPosition = new Vector3(
			this.PelvisRoot.transform.localPosition.x,
			0.0f,
			this.PelvisRoot.transform.localPosition.z);

		this.ShoulderCamera.AimingCamera = false;

		if (!Input.GetButtonDown(InputNames.Xbox_Start) && !Input.GetKeyDown(KeyCode.Escape))
		{
			this.FixCamera();
		}

		if (this.ShoulderCamera.Timer == 0.0f)
		{
			this.RPGCamera.enabled = true;
		}

		if (!OptionGlobals.Fog)
		{
			this.MainCamera.clearFlags = CameraClearFlags.Skybox;
		}
		else
		{
			this.MainCamera.clearFlags = CameraClearFlags.SolidColor;
		}

		this.MainCamera.farClipPlane = OptionGlobals.DrawDistance;

		// [af] Added "gameObject" for C# compatibility.
		this.Smartphone.transform.parent.gameObject.SetActive(false);

		this.Smartphone.targetTexture = this.Shutter.SmartphoneScreen;

		this.Smartphone.transform.localEulerAngles = new Vector3(0, 0, 0);
		this.Smartphone.fieldOfView = 60.0f;
		this.Shutter.TargetStudent = 0;
		this.Height = 0.0f;
		this.Bend = 0.0f;

		this.HandCamera.gameObject.SetActive(false);
		this.SelfieGuide.SetActive(false);
		this.PhonePromptBar.Show = false;
		this.MainCamera.enabled = true;
		this.UsingController = false;
		this.Aiming = false;
		this.Selfie = false;
		this.Lewd = false;

        this.StudentManager.UpdatePanties(false);
    }

	public void FixCamera()
	{
        //Debug.Log("fix it");

		this.RPGCamera.enabled = true;

		this.RPGCamera.UpdateRotation();
		this.RPGCamera.mouseSmoothingFactor = 0.0f;
		this.RPGCamera.GetInput();
		this.RPGCamera.GetDesiredPosition();
		this.RPGCamera.PositionUpdate();
		this.RPGCamera.mouseSmoothingFactor = 0.080f;

		this.Blur.enabled = false;
	}

	void ResetSenpaiEffects()
	{
		this.DepthOfField.focalSize = 150.0f;
		this.DepthOfField.focalZStartCurve = 0.0f;
		this.DepthOfField.focalZEndCurve = 0.0f;
		this.DepthOfField.enabled = false;

		this.ColorCorrection.redChannel.MoveKey(1, new Keyframe(0.50f, 0.50f));
		this.ColorCorrection.greenChannel.MoveKey(1, new Keyframe(0.50f, 0.50f));
		this.ColorCorrection.blueChannel.MoveKey(1, new Keyframe(0.50f, 0.50f));

		this.ColorCorrection.redChannel.SmoothTangents(1, 0);
		this.ColorCorrection.greenChannel.SmoothTangents(1, 0);
		this.ColorCorrection.blueChannel.SmoothTangents(1, 0);

		this.ColorCorrection.UpdateTextures();
		this.ColorCorrection.enabled = false;

		for (int i = 1; i < 6; i++)
		{
			this.CharacterAnimation[this.CreepyIdles[i]].weight = 0;
			this.CharacterAnimation[this.CreepyWalks[i]].weight = 0;
		}

		this.CharacterAnimation[AnimNames.FemaleShy].weight = 0.0f;
		this.HeartBeat.volume = 0.0f;

		this.SelectGrayscale.desaturation = this.GreyTarget;

		//Debug.Log("Resetting ''Senpai Effects''. That means that desaturation is being set to: " + this.GreyTarget);

		this.SenpaiFade = 100.0f;
		this.SenpaiTint = 0;
	}

	public void ResetYandereEffects()
	{
		this.Obscurance.enabled = false;

		this.Vignette.intensity = 0.0f;
		this.Vignette.blur = 0.0f;
		this.Vignette.chromaticAberration = 0.0f;
		this.Vignette.enabled = false;

		this.YandereColorCorrection.redChannel.MoveKey(1, new Keyframe(0.50f, 0.50f));
		this.YandereColorCorrection.greenChannel.MoveKey(1, new Keyframe(0.50f, 0.50f));
		this.YandereColorCorrection.blueChannel.MoveKey(1, new Keyframe(0.50f, 0.50f));

		this.YandereColorCorrection.redChannel.SmoothTangents(1, 0);
		this.YandereColorCorrection.greenChannel.SmoothTangents(1, 0);
		this.YandereColorCorrection.blueChannel.SmoothTangents(1, 0);

		this.YandereColorCorrection.UpdateTextures();
		this.YandereColorCorrection.enabled = false;

		Time.timeScale = 1.0f;

		this.YandereFade = 100.0f;

		this.StudentManager.Tag.Sprite.color = new Color(1, 0, 0, 0);

		this.StudentManager.RedString.gameObject.SetActive(false);
	}

	void DumpRagdoll(RagdollDumpType Type)
	{
		this.Ragdoll.transform.position = this.transform.position;

		if (Type == RagdollDumpType.Incinerator)
		{
			this.Ragdoll.transform.LookAt(this.Incinerator.transform.position);
			this.Ragdoll.transform.eulerAngles = new Vector3(
				this.Ragdoll.transform.eulerAngles.x,
				this.Ragdoll.transform.eulerAngles.y + 180.0f,
				this.Ragdoll.transform.eulerAngles.z);
		}
		else if (Type == RagdollDumpType.TranqCase)
		{
			this.Ragdoll.transform.LookAt(this.TranqCase.transform.position);
		}
		else if (Type == RagdollDumpType.WoodChipper)
		{
			this.Ragdoll.transform.LookAt(this.WoodChipper.transform.position);
		}

		RagdollScript ragdoll = this.Ragdoll.GetComponent<RagdollScript>();
		ragdoll.DumpType = Type;
		ragdoll.Dump();
	}

    public void Unequip()
	{
		//Debug.Log("Yandere-chan has been told to de-equip her weapon.");

		if (this.CanMove || this.Noticed)
		{
			//Debug.Log("Yandere-chan has now de-equipped her weapon.");

			if (this.Equipped < 3)
			{
				this.CharacterAnimation["f02_reachForWeapon_00"].time = 0;
				this.ReachWeight = 1;

				if (this.EquippedWeapon != null)
				{
					// [af] Added "gameObject" for C# compatibility.
					this.EquippedWeapon.gameObject.SetActive(false);
				}
			}
			else
			{
                if (this.Weapon[3] != null)
                {
				    this.Weapon[3].Drop();
                }
            }

			this.Equipped = 0;

			// @todo: Refactor Yandere-chan weapon code so index 0 can be non-null.
			// "Empty hands" should not need its own weapon index.
			Debug.Assert(this.Weapon[this.Equipped] == null);

			this.Mopping = false;

			this.StudentManager.UpdateStudents();
			this.WeaponManager.UpdateLabels();
			this.WeaponMenu.UpdateSprites();

			this.WeaponWarning = false;
		}
    }

	public void DropWeapon(int ID)
	{
		this.DropTimer[ID] += Time.deltaTime;

		if (this.DropTimer[ID] > 0.50f)
		{
			this.Weapon[ID].Drop();
			this.Weapon[ID] = null;
			this.Unequip();
			this.DropTimer[ID] = 0.0f;
		}
	}

	public void EmptyHands()
	{
		//Debug.Log("Yandere-chan has been told to drop what she is carrying.");

		if (this.Carrying || this.HeavyWeight)
		{
			this.StopCarrying();
		}

		if (this.Armed)
		{
			this.Unequip();
		}

		if (this.PickUp != null)
		{
			this.PickUp.Drop();
		}

		if (this.Dragging)
		{
			this.Ragdoll.GetComponent<RagdollScript>().StopDragging();
		}

		for (this.ID = 1; this.ID < this.Poisons.Length; this.ID++)
		{
			this.Poisons[this.ID].SetActive(false);
		}

		this.Mopping = false;
	}

	public void UpdateNumbness()
	{
		this.Numbness = 1.0f - (0.10f * 
			(this.Class.Numbness + this.Class.NumbnessBonus));
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "BloodPool(Clone)")
		{
			if (other.transform.localScale.x > 0.30f)
			{
				if (PlayerGlobals.PantiesEquipped == 8)
				{
					this.RightFootprintSpawner.Bloodiness = 5;
					this.LeftFootprintSpawner.Bloodiness = 5;
				}
				else
				{
					this.RightFootprintSpawner.Bloodiness = 10;
					this.LeftFootprintSpawner.Bloodiness = 10;
				}
			}
		}
	}

	public void UpdateHair()
	{
		if (this.Hairstyle > (this.Hairstyles.Length - 1))
		{
			this.Hairstyle = 0;
		}

		if (this.Hairstyle < 0)
		{
			this.Hairstyle = this.Hairstyles.Length - 1;
		}

		// [af] Converted while loop to for loop.
		for (this.ID = 1; this.ID < this.Hairstyles.Length; this.ID++)
		{
			this.Hairstyles[this.ID].SetActive(false);
		}

		if (this.Hairstyle > 0)
		{
			this.Hairstyles[this.Hairstyle].SetActive(true);
		}
	}

	public void StopLaughing()
	{
        Debug.Log("Yandere-chan has been instructed to stop laughing.");

		this.BladeHairCollider1.enabled = false;
		this.BladeHairCollider2.enabled = false;

		if (this.Sanity < 33.33333f)
		{
			this.Teeth.SetActive(true);
		}

		this.LaughIntensity = 0.0f;
		this.Laughing = false;
		this.LaughClip = null;

		this.Twitch = Vector3.zero;

		if (!this.Stand.Stand.activeInHierarchy)
		{
			this.CanMove = true;
		}

		if (this.BanchoActive)
		{
			AudioSource.PlayClipAtPoint(this.BanchoFinalYan, transform.position);
			this.CharacterAnimation.CrossFade("f02_banchoFinisher_00");
			this.BanchoFlurry.MyCollider.enabled = false;
			this.Finisher = true;
			this.CanMove = false;
		}
	}

	void SetUniform()
	{
		if (StudentGlobals.FemaleUniform == 0)
		{
			StudentGlobals.FemaleUniform = 1;
		}

		this.MyRenderer.sharedMesh = this.Uniforms[StudentGlobals.FemaleUniform];

		if (this.Casual)
		{
			this.TextureToUse = this.UniformTextures[StudentGlobals.FemaleUniform];
		}
		else
		{
			this.TextureToUse = this.CasualTextures[StudentGlobals.FemaleUniform];
		}

		this.MyRenderer.materials[0].mainTexture = this.TextureToUse;
		this.MyRenderer.materials[1].mainTexture = this.TextureToUse;
		this.MyRenderer.materials[2].mainTexture = this.FaceTexture;

		this.StartCoroutine(this.ApplyCustomCostume());
	}

	IEnumerator ApplyCustomCostume()
	{
		if (StudentGlobals.FemaleUniform == 1)
		{
			WWW CustomUniform = new WWW("file:///" +
				Application.streamingAssetsPath + "/CustomUniform.png");

			yield return CustomUniform;

			if (CustomUniform.error == null)
			{
				this.MyRenderer.materials[0].mainTexture = CustomUniform.texture;
				this.MyRenderer.materials[1].mainTexture = CustomUniform.texture;
			}
		}
		else if (StudentGlobals.FemaleUniform == 2)
		{
			WWW CustomLong = new WWW("file:///" +
				Application.streamingAssetsPath + "/CustomLong.png");

			yield return CustomLong;

			if (CustomLong.error == null)
			{
				this.MyRenderer.materials[0].mainTexture = CustomLong.texture;
				this.MyRenderer.materials[1].mainTexture = CustomLong.texture;
			}
		}
		else if (StudentGlobals.FemaleUniform == 3)
		{
			WWW CustomSweater = new WWW("file:///" +
				Application.streamingAssetsPath + "/CustomSweater.png");

			yield return CustomSweater;

			if (CustomSweater.error == null)
			{
				this.MyRenderer.materials[0].mainTexture = CustomSweater.texture;
				this.MyRenderer.materials[1].mainTexture = CustomSweater.texture;
			}
		}
		else if ((StudentGlobals.FemaleUniform == 4) || (StudentGlobals.FemaleUniform == 5))
		{
			WWW CustomBlazer = new WWW("file:///" +
				Application.streamingAssetsPath + "/CustomBlazer.png");

			yield return CustomBlazer;

			if (CustomBlazer.error == null)
			{
				this.MyRenderer.materials[0].mainTexture = CustomBlazer.texture;
				this.MyRenderer.materials[1].mainTexture = CustomBlazer.texture;
			}
		}

		WWW CustomFace = new WWW("file:///" +
			Application.streamingAssetsPath + "/CustomFace.png");

		yield return CustomFace;

		if (CustomFace.error == null)
		{
			this.MyRenderer.materials[2].mainTexture = CustomFace.texture;
			this.FaceTexture = CustomFace.texture;
		}

		WWW CustomHair = new WWW("file:///" +
			Application.streamingAssetsPath + "/CustomHair.png");

		yield return CustomHair;

		if (CustomHair.error == null)
		{
			this.PonytailRenderer.material.mainTexture = CustomHair.texture;
			this.PigtailR.material.mainTexture = CustomHair.texture;
			this.PigtailL.material.mainTexture = CustomHair.texture;
		}

		WWW CustomDrills = new WWW("file:///" +
			Application.streamingAssetsPath + "/CustomDrills.png");

		yield return CustomDrills;

		if (CustomDrills.error == null)
		{
			this.Drills.materials[0].mainTexture = CustomDrills.texture;
			this.Drills.material.mainTexture = CustomDrills.texture;
		}
		//this.Drills.materials[2].mainTexture = CustomDrills.texture;

		WWW CustomSwimsuit = new WWW("file:///" +
			Application.streamingAssetsPath + "/CustomSwimsuit.png");

		yield return CustomSwimsuit;

		if (CustomSwimsuit.error == null)
		{
			this.SwimsuitTexture = CustomSwimsuit.texture;
		}

		WWW CustomGym = new WWW("file:///" +
			Application.streamingAssetsPath + "/CustomGym.png");

		yield return CustomGym;

		if (CustomGym.error == null)
		{
			this.GymTexture = CustomGym.texture;
		}

		WWW CustomNude = new WWW("file:///" +
			Application.streamingAssetsPath + "/CustomNude.png");

		yield return CustomNude;

		if (CustomNude.error == null)
		{
			this.NudeTexture = CustomNude.texture;
		}

		WWW CustomLongHairA = new WWW("file:///" +
			Application.streamingAssetsPath + "/CustomLongHairA.png");

		yield return CustomDrills;

		WWW CustomLongHairB = new WWW("file:///" +
			Application.streamingAssetsPath + "/CustomLongHairB.png");

		yield return CustomDrills;

		WWW CustomLongHairC = new WWW("file:///" +
			Application.streamingAssetsPath + "/CustomLongHairC.png");

		yield return CustomDrills;

		if ((CustomLongHairA.error == null) && (CustomLongHairB.error == null) &&
			(CustomLongHairC.error == null))
		{
			this.LongHairRenderer.materials[0].mainTexture = CustomLongHairA.texture;
			this.LongHairRenderer.materials[1].mainTexture = CustomLongHairB.texture;
			this.LongHairRenderer.materials[2].mainTexture = CustomLongHairC.texture;
		}
	}

	public Texture[] GloveTextures;

	public void WearGloves()
	{
		if (this.Bloodiness > 0.0f)
		{
			if (!this.Gloves.Blood.enabled)
			{
				this.Gloves.PickUp.Evidence = true;
				this.Gloves.Blood.enabled = true;
				this.Police.BloodyClothing++;
			}
		}

		if (this.Gloves.Blood.enabled)
		{
			this.GloveBlood = 1;
		}

		this.Gloved = true;

		this.GloveAttacher.newRenderer.enabled = true;

		/*
		if (StudentGlobals.FemaleUniform == 1)
		{
			this.MyRenderer.materials[1].mainTexture = this.GloveTextures[StudentGlobals.FemaleUniform];
		}
		else
		{
			this.MyRenderer.materials[0].mainTexture = this.GloveTextures[StudentGlobals.FemaleUniform];
		}
		*/
	}

	public Texture TitanTexture;

	void AttackOnTitan()
	{
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		this.MusicCredit.SongLabel.text = "Now Playing: This Is My Choice";
		this.MusicCredit.BandLabel.text = "By: The Kira Justice";
		this.MusicCredit.Panel.enabled = true;
		this.MusicCredit.Slide = true;

		this.EasterEggMenu.SetActive(false);
		this.Egg = true;

		this.MyRenderer.sharedMesh = this.Uniforms[1];
		this.MyRenderer.materials[0].mainTexture = this.TitanTexture;
		this.MyRenderer.materials[1].mainTexture = this.TitanTexture;
		this.MyRenderer.materials[2].mainTexture = this.FaceTexture;

		this.Outline.h.ReinitMaterials();
	}

	public Texture KONTexture;

	void KON()
	{
		this.MyRenderer.sharedMesh = this.Uniforms[4];
		this.MyRenderer.materials[0].mainTexture = this.KONTexture;
		this.MyRenderer.materials[1].mainTexture = this.KONTexture;
		this.MyRenderer.materials[2].mainTexture = this.FaceTexture;

		this.Outline.h.ReinitMaterials();
	}

	public GameObject PunishedAccessories;
	public GameObject PunishedScarf;
	public GameObject[] PunishedArm;
	public Texture[] PunishedTextures;
	public Shader PunishedShader;
	public Mesh PunishedMesh;

	void Punish()
	{
		this.PunishedShader = Shader.Find("Toon/Cutoff");

		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		this.EasterEggMenu.SetActive(false);
		this.Egg = true;

		this.PunishedAccessories.SetActive(true);
		this.PunishedScarf.SetActive(true);

		this.EyepatchL.SetActive(false);
		this.EyepatchR.SetActive(false);

		// [af] Converted while loop to for loop.
		for (this.ID = 0; this.ID < this.PunishedArm.Length; this.ID++)
		{
			this.PunishedArm[this.ID].SetActive(true);
		}

		this.MyRenderer.sharedMesh = this.PunishedMesh;

		this.MyRenderer.materials[0].mainTexture = this.PunishedTextures[1];
		this.MyRenderer.materials[1].mainTexture = this.PunishedTextures[1];
		this.MyRenderer.materials[2].mainTexture = this.PunishedTextures[0];

		this.MyRenderer.materials[1].shader = this.PunishedShader;
		this.MyRenderer.materials[1].SetFloat("_Shininess", 1.0f);
		this.MyRenderer.materials[1].SetFloat("_ShadowThreshold", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_Cutoff", 0.9f);
		this.MyRenderer.materials[1].color = new Color(1, 1, 1, 1);

		this.Outline.h.ReinitMaterials();
	}

	public Material HatefulSkybox;
	public Texture HatefulUniform;

	void Hate()
	{
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		this.MyRenderer.sharedMesh = this.Uniforms[1];
		this.MyRenderer.materials[0].mainTexture = this.HatefulUniform;
		this.MyRenderer.materials[1].mainTexture = this.HatefulUniform;
		this.MyRenderer.materials[2].mainTexture = this.FaceTexture;

		RenderSettings.skybox = this.HatefulSkybox;
		this.SelectGrayscale.desaturation = 1.0f;

		// [af] Added "gameObject" for C# compatibility.
		this.HeartRate.gameObject.SetActive(false);

		this.Sanity = 0.0f;

		this.Hairstyle = 15;
		this.UpdateHair();

		this.EasterEggMenu.SetActive(false);
		this.Egg = true;
	}

	public GameObject SukebanAccessories;
	public Texture SukebanBandages;
	public Texture SukebanUniform;

	void Sukeban()
	{
		this.IdleAnim = AnimNames.FemaleIdle;
		this.SukebanAccessories.SetActive(true);

		this.MyRenderer.sharedMesh = this.Uniforms[1];
		this.MyRenderer.materials[1].mainTexture = this.SukebanBandages;
		this.MyRenderer.materials[0].mainTexture = this.SukebanUniform;
		this.MyRenderer.materials[2].mainTexture = this.FaceTexture;

		this.EasterEggMenu.SetActive(false);
		this.Egg = true;
	}

	public FalconPunchScript BanchoFinisher;
	public StandPunchScript BanchoFlurry;

	public GameObject BanchoPants;
	public Mesh BanchoMesh;

	public Texture BanchoBody;
	public Texture BanchoFace;

	public GameObject[] BanchoAccessories;

	public bool BanchoActive;
	public bool Finisher;

	public AudioClip BanchoYanYan;
	public AudioClip BanchoFinalYan;

	public AmplifyMotionObject MotionObject;
	public AmplifyMotionEffect MotionBlur;
	public GameObject BanchoCamera;

	void Bancho()
	{
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		this.BanchoCamera.SetActive(true);
		this.MotionObject.enabled = true;
		this.MotionBlur.enabled = true;

		this.BanchoAccessories[0].SetActive(true);
		this.BanchoAccessories[1].SetActive(true);
		this.BanchoAccessories[2].SetActive(true);
		this.BanchoAccessories[3].SetActive(true);
		this.BanchoAccessories[4].SetActive(true);
		this.BanchoAccessories[5].SetActive(true);
		this.BanchoAccessories[6].SetActive(true);
		this.BanchoAccessories[7].SetActive(true);
		this.BanchoAccessories[8].SetActive(true);

		this.Laugh1 = this.BanchoYanYan;
		this.Laugh2 = this.BanchoYanYan;
		this.Laugh3 = this.BanchoYanYan;
		this.Laugh4 = this.BanchoYanYan;

		this.IdleAnim = "f02_banchoIdle_00";
		this.WalkAnim = "f02_banchoWalk_00";
		this.RunAnim = "f02_banchoSprint_00";

		this.OriginalIdleAnim = this.IdleAnim;
		this.OriginalWalkAnim = this.WalkAnim;
		this.OriginalRunAnim = this.RunAnim;

		this.RunSpeed *= 2;

		this.BanchoPants.SetActive(true);

		this.MyRenderer.sharedMesh = BanchoMesh;
		this.MyRenderer.materials[0].mainTexture = this.BanchoFace;
		this.MyRenderer.materials[1].mainTexture = this.BanchoBody;
		this.MyRenderer.materials[2].mainTexture = this.BanchoBody;

		this.BanchoActive = true;
		this.TheDebugMenuScript.UpdateCensor();

		this.Character.transform.localPosition = new Vector3(0, 0.04f, 0);

		this.Hairstyle = 0;
		this.UpdateHair();

		this.EasterEggMenu.SetActive(false);
		this.Egg = true;
	}
		
	public GameObject[] SlenderHair;
	public Texture SlenderUniform;
	public Material SlenderSkybox;
	public Texture SlenderSkin;

	void Slend()
	{
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		RenderSettings.skybox = this.SlenderSkybox;
		this.SelectGrayscale.desaturation = 0.50f;
		this.SelectGrayscale.enabled = true;

		this.EasterEggMenu.SetActive(false);
		this.Slender = true;
		this.Egg = true;

		this.Hairstyle = 0;
		this.UpdateHair();

		this.SlenderHair[0].transform.parent.gameObject.SetActive(true);
		this.SlenderHair[0].SetActive(true);
		this.SlenderHair[1].SetActive(true);

		this.RightYandereEye.gameObject.SetActive(false);
		this.LeftYandereEye.gameObject.SetActive(false);

		this.Character.transform.localPosition = new Vector3(
			this.Character.transform.localPosition.x,
			0.822f,
			this.Character.transform.localPosition.z);

		this.MyRenderer.sharedMesh = this.Uniforms[1];
		this.MyRenderer.materials[0].mainTexture = this.SlenderUniform;
		this.MyRenderer.materials[1].mainTexture = this.SlenderUniform;
		this.MyRenderer.materials[2].mainTexture = this.SlenderSkin;

		this.Sanity = 0.0f;
	}

	public Transform[] LongHair;
	public GameObject BlackEyePatch;
	public GameObject XSclera;
	public GameObject XEye;
	public Texture XBody;
	public Texture XFace;

	void X()
	{
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		this.Xtan = true;
		this.Egg = true;

		this.Hairstyle = 9;
		this.UpdateHair();

		this.BlackEyePatch.SetActive(true);
		this.XSclera.SetActive(true);
		this.XEye.SetActive(true);

		this.Schoolwear = 2;
		this.ChangeSchoolwear();
		this.CanMove = true;

		this.MyRenderer.materials[0].mainTexture = this.XBody;
		this.MyRenderer.materials[1].mainTexture = this.XBody;
		this.MyRenderer.materials[2].mainTexture = this.XFace;
	}

	public GameObject[] GaloAccessories;
	public Texture GaloArms;
	public Texture GaloFace;

	void GaloSengen()
	{
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		this.IdleAnim = AnimNames.FemaleGruntIdle;

		this.EasterEggMenu.SetActive(false);
		this.Egg = true;

		// [af] Converted while loop to for loop.
		for (this.ID = 0; this.ID < this.GaloAccessories.Length; this.ID++)
		{
			this.GaloAccessories[this.ID].SetActive(true);
		}

		this.MyRenderer.sharedMesh = this.Uniforms[1];
		this.MyRenderer.materials[0].mainTexture = this.UniformTextures[1];
		this.MyRenderer.materials[1].mainTexture = this.GaloArms;
		this.MyRenderer.materials[2].mainTexture = this.GaloFace;

		this.Hairstyle = 14;
		this.UpdateHair();
	}

	public AudioClip SummonStand;
	public StandScript Stand;
	public AudioClip YanYan;

	public void Jojo()
	{
		this.ShoulderCamera.LastPosition = this.ShoulderCamera.transform.position;
		this.ShoulderCamera.Summoning = true;
		this.RPGCamera.enabled = false;

		AudioSource.PlayClipAtPoint(this.SummonStand, this.transform.position);
		//this.StudentManager.HideStudents();
		this.IdleAnim = AnimNames.FemaleJojoPose;
		this.WalkAnim = AnimNames.FemaleJojoWalk;
		this.EasterEggMenu.SetActive(false);
		this.CanMove = false;
		this.Egg = true;

		this.CharacterAnimation.CrossFade(AnimNames.FemaleSummonStand);

		this.Laugh1 = this.YanYan;
		this.Laugh2 = this.YanYan;
		this.Laugh3 = this.YanYan;
		this.Laugh4 = this.YanYan;

		//this.Stand.Spawn();
	}

	public Texture AgentFace;
	public Texture AgentSuit;

	void Agent()
	{
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		this.MyRenderer.sharedMesh = this.Uniforms[4];
		this.MyRenderer.materials[0].mainTexture = this.AgentSuit;
		this.MyRenderer.materials[1].mainTexture = this.AgentSuit;
		this.MyRenderer.materials[2].mainTexture = this.AgentFace;

		this.EasterEggMenu.SetActive(false);
		this.Egg = true;

		this.Hairstyle = 0;
		this.UpdateHair();
	}

	public GameObject CirnoIceAttack;
	public AudioClip CirnoIceClip;
	public GameObject CirnoWings;
	public GameObject CirnoHair;
	public Texture CirnoUniform;
	public Texture CirnoFace;

	public Transform[] CirnoWing;

	public float CirnoRotation = 0.0f;
	public float CirnoTimer = 0.0f;

	void Cirno()
	{
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		this.MyRenderer.sharedMesh = this.Uniforms[3];
		this.MyRenderer.materials[0].mainTexture = this.CirnoUniform;
		this.MyRenderer.materials[1].mainTexture = this.CirnoUniform;
		this.MyRenderer.materials[2].mainTexture = this.CirnoFace;
		
		this.CirnoWings.SetActive(true);
		this.CirnoHair.SetActive(true);

		this.IdleAnim = AnimNames.FemaleCirnoIdle;
		this.WalkAnim = AnimNames.FemaleCirnoWalk;
		this.RunAnim = AnimNames.FemaleCirnoRun;

		this.EasterEggMenu.SetActive(false);
		this.Stance.Current = StanceType.Standing;
		this.Uncrouch();
		this.Egg = true;

		this.Hairstyle = 0;
		this.UpdateHair();
	}

	public AudioClip FalconPunchVoice;
	public Texture FalconBody;
	public Texture FalconFace;
	public float FalconSpeed = 0.0f;

	public GameObject NewFalconPunch;
	public GameObject FalconWindUp;
	public GameObject FalconPunch;

	public GameObject FalconShoulderpad;
	public GameObject FalconKneepad1;
	public GameObject FalconKneepad2;
	public GameObject FalconBuckle;
	public GameObject FalconHelmet;
	//public GameObject FalconGun;

	void Falcon()
	{
		this.MyRenderer.sharedMesh = this.NudeMesh;
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		this.MyRenderer.materials[0].mainTexture = this.FalconFace;
		this.MyRenderer.materials[1].mainTexture = this.FalconBody;
		this.MyRenderer.materials[2].mainTexture = this.FalconBody;

		this.FalconShoulderpad.SetActive(true);
		this.FalconKneepad1.SetActive(true);
		this.FalconKneepad2.SetActive(true);
		this.FalconBuckle.SetActive(true);
		this.FalconHelmet.SetActive(true);
		//this.FalconGun.SetActive(true);

		this.CharacterAnimation[this.RunAnim].speed = 5.0f;
		this.IdleAnim = AnimNames.FemaleFalconIdle;
		this.RunSpeed *= 5.0f;
		this.Egg = true;

		this.Hairstyle = 3;
		this.UpdateHair();

		this.DebugMenu.transform.parent.GetComponent<DebugMenuScript>().UpdateCensor();
	}

	public AudioClip[] OnePunchVoices;
	public GameObject NewOnePunch;
	public GameObject OnePunch;
	public Texture SaitamaSuit;
	public GameObject Cape;

	void Punch()
	{
		this.MusicCredit.SongLabel.text = "Now Playing: Unknown Hero";
		this.MusicCredit.BandLabel.text = "By: The Kira Justice";
		this.MusicCredit.Panel.enabled = true;
		this.MusicCredit.Slide = true;

		this.MyRenderer.sharedMesh = this.SchoolSwimsuit;
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		this.MyRenderer.materials[0].mainTexture = this.SaitamaSuit;
		this.MyRenderer.materials[1].mainTexture = this.SaitamaSuit;
		this.MyRenderer.materials[2].mainTexture = this.FaceTexture;

		this.EasterEggMenu.SetActive(false);
		this.Barcode.SetActive(false);
		this.Cape.SetActive(true);
		this.Egg = true;

		this.Hairstyle = 0;
		this.UpdateHair();

		this.DebugMenu.transform.parent.GetComponent<DebugMenuScript>().UpdateCensor();
	}

	public ParticleSystem GlowEffect;
	public GameObject[] BlasterSet;
	public GameObject[] SansEyes;
	public AudioClip BlasterClip;
	public Texture SansTexture;
	public Texture SansFace;
	public GameObject Bone;
	public AudioClip Slam;
	public Mesh Jersey;

	public int BlasterStage = 0;
	public PKDirType PKDir = PKDirType.None;

	void BadTime()
	{
		this.MyRenderer.sharedMesh = this.Jersey;
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		this.MyRenderer.materials[0].mainTexture = this.SansFace;
		this.MyRenderer.materials[1].mainTexture = this.SansTexture;
		this.MyRenderer.materials[2].mainTexture = this.SansTexture;

		this.EasterEggMenu.SetActive(false);

		this.IdleAnim = AnimNames.FemaleSansIdle;
		this.WalkAnim = AnimNames.FemaleSansWalk;
		this.RunAnim = AnimNames.FemaleSansRun;

		this.StudentManager.BadTime();
		this.Barcode.SetActive(false);
		this.Sans = true;
		this.Egg = true;

		this.Hairstyle = 0;
		this.UpdateHair();
		this.DebugMenu.transform.parent.GetComponent<DebugMenuScript>().EasterEggCheck();
	}

	public Texture CyborgBody;
	public Texture CyborgFace;

	public GameObject[] CyborgParts;
	public GameObject EnergySword;

	public bool Ninja;

	void CyborgNinja()
	{
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		this.EnergySword.SetActive(true);

		this.IdleAnim = AnimNames.CyborgNinjaIdleUnarmed;
		this.RunAnim = AnimNames.CyborgNinjaRunUnarmed;

		this.MyRenderer.sharedMesh = this.NudeMesh;
		this.MyRenderer.materials[0].mainTexture = this.CyborgFace;
		this.MyRenderer.materials[1].mainTexture = this.CyborgBody;
		this.MyRenderer.materials[2].mainTexture = this.CyborgBody;
		this.Schoolwear = 0;

		// [af] Commented in JS code.
		//MyRenderer.materials[3].color.a = 0;

		// [af] Converted while loop to for loop.
		for (this.ID = 1; this.ID < this.CyborgParts.Length; this.ID++)
		{
			this.CyborgParts[this.ID].SetActive(true);
		}

		// [af] Converted while loop to for loop.
		for (this.ID = 1; this.ID < this.StudentManager.Students.Length; this.ID++)
		{
			StudentScript student = this.StudentManager.Students[this.ID];

			if (student != null)
			{
				student.Teacher = false;
			}
		}

		this.RunSpeed *= 2.0f;

		this.EyewearID = 6;

		this.Hairstyle = 45;
		this.UpdateHair();

		this.Ninja = true;
		this.Egg = true;

		this.DebugMenu.transform.parent.GetComponent<DebugMenuScript>().UpdateCensor();
	}

	public GameObject EbolaEffect;
	public GameObject EbolaWings;
	public GameObject EbolaHair;

	public Texture EbolaFace;
	public Texture EbolaUniform;

	void Ebola()
	{
		this.StudentManager.Ebola = true;

		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		this.IdleAnim = AnimNames.FemaleEbolaIdle;

		this.MyRenderer.sharedMesh = this.Uniforms[1];
		this.MyRenderer.materials[0].mainTexture = this.EbolaUniform;
		this.MyRenderer.materials[1].mainTexture = this.EbolaUniform;
		this.MyRenderer.materials[2].mainTexture = this.EbolaFace;

		this.Hairstyle = 0;
		this.UpdateHair();

		this.EbolaWings.SetActive(true);
		this.EbolaHair.SetActive(true);
		this.Egg = true;
	}

	public Mesh LongUniform;

	void Long()
	{
		this.MyRenderer.sharedMesh = this.LongUniform;
	}

	public Texture NewFace;
	public Mesh NewMesh;

	void SwapMesh()
	{
		this.MyRenderer.sharedMesh = this.NewMesh;

		this.MyRenderer.materials[0].mainTexture = this.TextureToUse;
		this.MyRenderer.materials[1].mainTexture = this.NewFace;
		this.MyRenderer.materials[2].mainTexture = this.TextureToUse;

		this.RightYandereEye.gameObject.SetActive(false);
		this.LeftYandereEye.gameObject.SetActive(false);
	}

	public GameObject[] CensorSteam;
	public Texture NudePanties;
	public Texture NudeTexture;
	public Mesh NudeMesh;

	void Nude()
	{
		Debug.Log("Making Yandere-chan nude.");

		this.MyRenderer.sharedMesh = this.NudeMesh;
		this.MyRenderer.materials[0].mainTexture = this.FaceTexture;
		this.MyRenderer.materials[1].mainTexture = this.NudeTexture;

		// [af] Converted while loop to for loop.
		for (this.ID = 0; this.ID < this.CensorSteam.Length; this.ID++)
		{
			//this.CensorSteam[this.ID].SetActive(true);
		}

		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		this.MyRenderer.materials[0].SetFloat("_BlendAmount1", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount1", 0.0f);

		this.EasterEggMenu.SetActive(false);
		this.ClubAttire = false;
		this.Schoolwear = 0;

		this.ClubAccessory();
	}

	public Texture SamusBody;
	public Texture SamusFace;

	void Samus()
	{
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		this.MyRenderer.sharedMesh = this.NudeMesh;
		this.MyRenderer.materials[0].mainTexture = this.SamusFace;
		this.MyRenderer.materials[1].mainTexture = this.SamusBody;
		this.PantyAttacher.newRenderer.enabled = false;
		this.Schoolwear = 0;

		// [af] Commented in JS code.
		//MyRenderer.materials[3].mainTexture = null;

		this.PonytailRenderer.material.mainTexture = this.SamusFace;

		this.Egg = true;

		this.DebugMenu.transform.parent.GetComponent<DebugMenuScript>().UpdateCensor();
	}

	public GlobalKnifeArrayScript GlobalKnifeArray;
	public GameObject PlayerOnlyCamera;
	public GameObject KnifeArray;

	public AudioClip ClockStart;
	public AudioClip ClockStop;
	public AudioClip ClockTick;

	public AudioClip StartShout;
	public AudioClip StopShout;

	public Texture WitchBody;
	public Texture WitchFace;

	void Witch()
	{
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		this.MyRenderer.sharedMesh = this.NudeMesh;
		this.MyRenderer.materials[0].mainTexture = this.WitchFace;
		this.MyRenderer.materials[1].mainTexture = this.WitchBody;
		this.PantyAttacher.newRenderer.enabled = false;
		this.Schoolwear = 0;

		this.IdleAnim = "f02_idleElegant_00";
		this.WalkAnim = AnimNames.FemaleJojoWalk;

		this.WitchMode = true;
		this.Egg = true;

		this.Hairstyle = 100;
		this.UpdateHair();

		this.DebugMenu.transform.parent.GetComponent<DebugMenuScript>().UpdateCensor();
	}

	void Pose()
	{
		if (!this.StudentManager.Pose)
		{
			this.StudentManager.Pose = true;
		}
		else
		{
			this.StudentManager.Pose = false;
		}

		this.StudentManager.UpdateStudents();
	}

	public Collider BladeHairCollider1;
	public Collider BladeHairCollider2;
	public GameObject BladeHair;

	void HairBlades()
	{
		this.Hairstyle = 0;
		this.UpdateHair();

		this.BladeHair.SetActive(true);
		this.Egg = true;
	}

	public DebugMenuScript TheDebugMenuScript;

	public GameObject RiggedAccessory;
	public GameObject TornadoAttack;
	public GameObject TornadoDress;
	public GameObject TornadoHair;

	public Renderer TornadoRenderer;
	public Mesh NoTorsoMesh;

	void Tornado()
	{
		this.Hairstyle = 0;
		this.UpdateHair();

		this.IdleAnim = AnimNames.FemaleTornadoIdle;
		this.WalkAnim = AnimNames.FemaleTornadoWalk;
		this.RunAnim = AnimNames.FemaleTornadoRun;

		this.TornadoHair.SetActive(true);
		this.TornadoDress.SetActive(true);
		this.RiggedAccessory.SetActive(true);
		this.MyRenderer.sharedMesh = this.NoTorsoMesh;
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		this.Sanity = 100.0f;

		this.MyRenderer.materials[0].mainTexture = this.FaceTexture;
		this.MyRenderer.materials[1].mainTexture = this.NudePanties;
		this.MyRenderer.materials[2].mainTexture = this.NudePanties;

		this.TheDebugMenuScript.UpdateCensor();

		this.Stance.Current = StanceType.Standing;
		this.Egg = true;
	}

	public GameObject KunHair;
	public GameObject Kun;

	void GenderSwap()
	{
		this.Kun.SetActive(true);
		this.KunHair.SetActive(true);
		this.MyRenderer.enabled = false;
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		this.IdleAnim = AnimNames.MaleIdleShort;
		this.WalkAnim = AnimNames.MaleWalk;
		this.RunAnim = AnimNames.MaleNewSprint;

		this.OriginalIdleAnim = this.IdleAnim;
		this.OriginalWalkAnim = this.WalkAnim;
		this.OriginalRunAnim = this.RunAnim;

		this.Hairstyle = 0;
		this.UpdateHair();
	}

	public GameObject Man;

	void Mandere()
	{
		this.Man.SetActive(true);
		this.Barcode.SetActive(false);
		this.MyRenderer.enabled = false;
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		this.RightYandereEye.gameObject.SetActive(false);
		this.LeftYandereEye.gameObject.SetActive(false);

		this.IdleAnim = AnimNames.MaleIdleShort;
		this.WalkAnim = AnimNames.MaleWalk;
		this.RunAnim = AnimNames.MaleNewSprint;

		this.OriginalIdleAnim = this.IdleAnim;
		this.OriginalWalkAnim = this.WalkAnim;
		this.OriginalRunAnim = this.RunAnim;

		this.Hairstyle = 0;
		this.UpdateHair();
	}

	public GameObject BlackHoleEffects;
	public Texture BlackHoleFace;
	public Texture Black;
	public bool BlackHole;

	public Transform RightLeg;
	public Transform LeftLeg;

	void BlackHoleChan()
	{
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		this.MyRenderer.sharedMesh = this.NudeMesh;
		this.MyRenderer.materials[0].mainTexture = this.BlackHoleFace;
		this.MyRenderer.materials[1].mainTexture = this.Black;
		this.MyRenderer.materials[2].mainTexture = this.Black;
		this.PantyAttacher.newRenderer.enabled = false;
		this.Schoolwear = 0;

		this.IdleAnim = AnimNames.FemaleGazerIdle;
		this.WalkAnim = AnimNames.FemaleGazerWalk;
		this.RunAnim = AnimNames.FemaleGazerRun;

		this.OriginalIdleAnim = this.IdleAnim;
		this.OriginalWalkAnim = this.WalkAnim;
		this.OriginalRunAnim = this.RunAnim;

		this.Hairstyle = 182;
		this.UpdateHair();

		this.BlackHoleEffects.SetActive(true);
		this.BlackHole = true;
		this.Egg = true;

		this.DebugMenu.transform.parent.GetComponent<DebugMenuScript>().UpdateCensor();
	}

	public GameObject Bandages;
	public GameObject LucyHelmet;
	public GameObject[] Vectors;

	void ElfenLied()
	{
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		this.MyRenderer.sharedMesh = this.NudeMesh;
		this.MyRenderer.materials[0].mainTexture = this.FaceTexture;
		this.MyRenderer.materials[1].mainTexture = this.NudeTexture;
		this.MyRenderer.materials[2].mainTexture = this.NudeTexture;

		foreach (GameObject vector in this.Vectors)
		{
			vector.SetActive(true);
		}

		this.IdleAnim = AnimNames.SixIdle;
		this.WalkAnim = AnimNames.SixWalk;
		this.RunAnim = AnimNames.SixRun;

		this.OriginalIdleAnim = this.IdleAnim;
		this.OriginalWalkAnim = this.WalkAnim;
		this.OriginalRunAnim = this.RunAnim;

		this.LucyHelmet.SetActive(true);
		this.Bandages.SetActive(true);
		this.Egg = true;

		this.WalkSpeed = 0.75f;
		this.RunSpeed = 2f;

		this.Hairstyle = 0;
		this.UpdateHair();

		this.DebugMenu.transform.parent.GetComponent<DebugMenuScript>().UpdateCensor();
	}
		
	public GameObject[] Kagune;
	public Texture GhoulFace;
	public Texture GhoulBody;

	public bool ReturnKagune;
	public bool SwingKagune;

	public Vector3[] KaguneRotation;

	public AudioClip KaguneSwoosh;

	public GameObject DemonSlash;

	public int KagunePhase = 0;

	void Ghoul()
	{
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		this.MyRenderer.sharedMesh = this.NudeMesh;
		this.MyRenderer.materials[0].mainTexture = this.GhoulFace;
		this.MyRenderer.materials[1].mainTexture = this.GhoulBody;
		this.MyRenderer.materials[2].mainTexture = this.GhoulBody;

		foreach (GameObject kagune in this.Kagune)
		{
			kagune.SetActive(true);
		}

		this.IdleAnim = AnimNames.SixIdle;
		this.WalkAnim = AnimNames.SixWalk;
		this.RunAnim = "f02_psychoRun_00";

		this.OriginalIdleAnim = this.IdleAnim;
		this.OriginalWalkAnim = this.WalkAnim;
		this.OriginalRunAnim = this.RunAnim;

		this.StudentManager.Six = true;
		this.StudentManager.UpdateStudents();

		this.Egg = true;

		this.WalkSpeed = 0.75f;
		this.RunSpeed = 10f;

		this.Hairstyle = 15;
		this.UpdateHair();

		this.DebugMenu.transform.parent.GetComponent<DebugMenuScript>().UpdateCensor();
	}

	public GameObject[] Armor;
	public Texture Chainmail;
	public Texture Scarface;
	public Material Metal;
	public Material Trans;

	void Berserk()
	{
		SchoolGlobals.SchoolAtmosphere = 0.5f;
		this.StudentManager.SetAtmosphere();

		foreach (GameObject armor in this.Armor)
		{
			armor.SetActive(true);
		}

		foreach (Renderer tree in this.StudentManager.Trees)
		{
			tree.materials[1] = Trans;
		}

		SithSpawnTime = NierSpawnTime;
		SithHardSpawnTime1 = NierHardSpawnTime;
		SithHardSpawnTime2 = NierHardSpawnTime;
		SithAudio.clip = NierSwoosh;

		this.MyRenderer.sharedMesh = this.NudeMesh;
		this.MyRenderer.materials[0].mainTexture = this.Scarface;
		this.MyRenderer.materials[1].mainTexture = this.Chainmail;
		this.MyRenderer.materials[2].mainTexture = this.Chainmail;

		this.PantyAttacher.newRenderer.enabled = false;
		this.Schoolwear = 0;

		this.TheDebugMenuScript.UpdateCensor();

		this.IdleAnim = AnimNames.FemaleHeroicIdle;
		this.WalkAnim = AnimNames.FemaleConfidentWalk;
		this.RunAnim = "f02_nierRun_00";

		this.CharacterAnimation["f02_nierRun_00"].speed = 1;
		this.CharacterAnimation["f02_gutsEye_00"].weight = 1;
		this.RunSpeed = 7.5f;

		this.OriginalIdleAnim = this.IdleAnim;
		this.OriginalWalkAnim = this.WalkAnim;
		this.OriginalRunAnim = this.RunAnim;

		this.Hairstyle = 188;
		this.UpdateHair();

		this.Egg = true;
	}

	public GameObject BlackRobe;

	public Mesh NoUpperBodyMesh;

	public ParticleSystem[] Beam;
	public SithBeamScript[] SithBeam;

	public bool SithRecovering;
	public bool SithAttacking;
	public bool SithLord;

	public string SithPrefix;

	public int SithComboLength;
	public int SithAttacks;
	public int SithSounds;
	public int SithCombo;

	public GameObject SithHardHitbox;
	public GameObject SithHitbox;

	public GameObject SithTrail1;
	public GameObject SithTrail2;

	public Transform SithTrailEnd1;
	public Transform SithTrailEnd2;

	public ZoomScript Zoom;

	public AudioClip SithOn;
	public AudioClip SithOff;
	public AudioClip SithSwing;
	public AudioClip SithStrike;
	public AudioSource SithAudio;

	public float[] SithHardSpawnTime1;
	public float[] SithHardSpawnTime2;
	public float[] SithSpawnTime;

	void Sith()
	{
		this.Hairstyle = 67;
		this.UpdateHair();

		this.SithTrail1.SetActive(true);
		this.SithTrail2.SetActive(true);

		this.IdleAnim = AnimNames.FemaleSithIdle;
		this.WalkAnim = AnimNames.FemaleSithWalk;
		this.RunAnim = AnimNames.FemaleSithRun;

		this.BlackRobe.SetActive(true);
		this.MyRenderer.sharedMesh = this.NoUpperBodyMesh;

		this.MyRenderer.materials[0].mainTexture = this.NudePanties;
		this.MyRenderer.materials[1].mainTexture = this.FaceTexture;
		this.MyRenderer.materials[2].mainTexture = this.NudePanties;

		this.Stance.Current = StanceType.Standing;
		this.FollowHips = true;
		this.SithLord = true;
		this.Egg = true;

		this.TheDebugMenuScript.UpdateCensor();
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		//this.WalkSpeed = 0.5f;
		this.RunSpeed *= 2.0f;
		this.Zoom.TargetZoom = 0.4f;
	}

	public Texture SnakeFace;
	public Texture SnakeBody;
	public Texture Stone;

	public AudioClip Petrify;
	public GameObject Pebbles;
	public bool Medusa;

	void Snake()
	{
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		this.MyRenderer.sharedMesh = this.Uniforms[1];
		this.MyRenderer.materials[0].mainTexture = this.SnakeBody;
		this.MyRenderer.materials[1].mainTexture = this.SnakeBody;
		this.MyRenderer.materials[2].mainTexture = this.SnakeFace;

		this.Hairstyle = 161;
		this.UpdateHair();

		this.Medusa = true;
		this.Egg = true;
	}

	public Texture GazerFace;
	public Texture GazerBody;

	public GazerEyesScript GazerEyes;

	public AudioClip FingerSnap;
	public AudioClip Zap;

	public bool GazeAttacking;
	public bool Snapping;
	public bool Gazing;

	public int SnapPhase;

	void Gazer()
	{
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		this.GazerEyes.gameObject.SetActive(true);

		this.MyRenderer.sharedMesh = this.NudeMesh;
		this.MyRenderer.materials[0].mainTexture = this.GazerFace;
		this.MyRenderer.materials[1].mainTexture = this.GazerBody;
		this.MyRenderer.materials[2].mainTexture = this.GazerBody;
		this.PantyAttacher.newRenderer.enabled = false;
		this.Schoolwear = 0;

		this.IdleAnim = AnimNames.FemaleGazerIdle;
		this.WalkAnim = AnimNames.FemaleGazerWalk;
		this.RunAnim = AnimNames.FemaleGazerRun;

		this.OriginalIdleAnim = this.IdleAnim;
		this.OriginalWalkAnim = this.WalkAnim;
		this.OriginalRunAnim = this.RunAnim;

		this.Hairstyle = 158;
		this.UpdateHair();

		this.StudentManager.Gaze = true;
		this.StudentManager.UpdateStudents();

		this.Gazing = true;
		this.Egg = true;

		this.DebugMenu.transform.parent.GetComponent<DebugMenuScript>().UpdateCensor();
	}
		
	public GameObject SixRaincoat;
	public GameObject RisingSmoke;
	public GameObject DarkHelix;

	public Texture SixFaceTexture;
	public AudioClip SixTakedown;
	public Transform SixTarget;
	public Mesh SixBodyMesh;
	public Transform Mouth;

	public int EatPhase;
	public bool Hungry;
	public int Hunger;

	public float[] BloodTimes;
	public AudioClip[] Snarls;

	void Six()
	{
		RenderSettings.skybox = this.HatefulSkybox;

		this.Hairstyle = 0;
		this.UpdateHair();

		this.IdleAnim = AnimNames.SixIdle;
		this.WalkAnim = AnimNames.SixWalk;
		this.RunAnim = AnimNames.SixRun;

		this.OriginalIdleAnim = this.IdleAnim;
		this.OriginalWalkAnim = this.WalkAnim;
		this.OriginalRunAnim = this.RunAnim;

		this.SixRaincoat.SetActive(true);
		this.MyRenderer.sharedMesh = this.SixBodyMesh;
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		this.MyRenderer.materials[0].mainTexture = this.SixFaceTexture;
		this.MyRenderer.materials[1].mainTexture = this.NudeTexture;
		this.MyRenderer.materials[2].mainTexture = this.NudeTexture;

		this.TheDebugMenuScript.UpdateCensor();

		SchoolGlobals.SchoolAtmosphere = 0.0f;
		this.StudentManager.SetAtmosphere();
		//this.StudentManager.StopHidingPrompts();

		this.StudentManager.Six = true;
		this.StudentManager.UpdateStudents();

		this.WalkSpeed = 0.75f;
		this.RunSpeed = 2f;

		this.Hungry = true;
		this.Egg = true;
	}

	public Texture KLKBody;
	public Texture KLKFace;

	public GameObject[] KLKParts;
	public GameObject KLKSword;

	void KLK()
	{
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		this.KLKSword.SetActive(true);

		this.IdleAnim = AnimNames.FemaleHeroicIdle;
		this.WalkAnim = AnimNames.FemaleConfidentWalk;

		this.MyRenderer.sharedMesh = this.NudeMesh;
		this.MyRenderer.materials[0].mainTexture = this.KLKFace;
		this.MyRenderer.materials[1].mainTexture = this.KLKBody;
		this.MyRenderer.materials[2].mainTexture = this.KLKBody;
		this.PantyAttacher.newRenderer.enabled = false;
		this.Schoolwear = 0;

		// [af] Commented in JS code.
		//MyRenderer.materials[3].color.a = 0;

		for (this.ID = 0; this.ID < this.KLKParts.Length; this.ID++)
		{
			this.KLKParts[this.ID].SetActive(true);
		}
			
		for (this.ID = 1; this.ID < this.StudentManager.Students.Length; this.ID++)
		{
			StudentScript student = this.StudentManager.Students[this.ID];

			if (student != null)
			{
				student.Teacher = false;
			}
		}

		this.Egg = true;

		this.DebugMenu.transform.parent.GetComponent<DebugMenuScript>().UpdateCensor();
	}

	public AudioClip LoveLoveBeamVoice;

	public GameObject MiyukiCostume;
	public GameObject LoveLoveBeam;
	public GameObject MiyukiWings;

	public Texture MiyukiSkin;
	public Texture MiyukiFace;

	public bool MagicalGirl;
	public int BeamPhase;

	public void Miyuki()
	{
		this.MiyukiCostume.SetActive(true);
		this.MiyukiWings.SetActive(true);

		this.IdleAnim = AnimNames.FemaleGirlyIdle;
		this.WalkAnim = AnimNames.FemaleGirlyWalk;

		this.MyRenderer.sharedMesh = this.NudeMesh;
		this.MyRenderer.materials[0].mainTexture = this.MiyukiFace;
		this.MyRenderer.materials[1].mainTexture = this.MiyukiSkin;
		this.MyRenderer.materials[2].mainTexture = this.MiyukiSkin;

		/*
		this.IdleAnim = AnimNames.FemaleGazerIdle;
		this.WalkAnim = AnimNames.FemaleGazerWalk;
		this.RunAnim = AnimNames.FemaleGazerRun;
		*/

		this.OriginalIdleAnim = this.IdleAnim;
		this.OriginalWalkAnim = this.WalkAnim;
		this.OriginalRunAnim = this.RunAnim;

		this.TheDebugMenuScript.UpdateCensor();
		this.Jukebox.MiyukiMusic();

		this.Hairstyle = 171;
		this.UpdateHair();

		this.MagicalGirl = true;
		this.Egg = true;
	}

	public GameObject AzurGuns;
	public GameObject AzurWater;
	public GameObject AzurMist;
	public GameObject Shell;

	public Transform[] Guns;

	public int ShotsFired;
	public bool Shipgirl;

	public void AzurLane()
	{
		this.Schoolwear = 2;
		this.ChangeSchoolwear();
		this.PantyAttacher.newRenderer.enabled = false;

		this.IdleAnim = AnimNames.FemaleGazerIdle;
		this.WalkAnim = AnimNames.FemaleGazerWalk;
		this.RunAnim = AnimNames.FemaleGazerRun;

		this.OriginalIdleAnim = this.IdleAnim;
		this.OriginalWalkAnim = this.WalkAnim;
		this.OriginalRunAnim = this.RunAnim;

		this.AzurGuns.SetActive(true);
		this.AzurWater.SetActive(true);
		this.AzurMist.SetActive(true);

		//this.SithLord = true;
		this.Shipgirl = true;
		this.CanMove = true;
		this.Egg = true;

		this.Jukebox.Shipgirl();

		//this.DebugMenu.transform.parent.GetComponent<DebugMenuScript>().UpdateCensor();
	}

    public BlacklightEffect BlacklightShader;
    public GameObject BlacklightOutfit;
    public Mesh BlacklightBodyMesh;

    void Blacklight()
    {
        this.BlacklightShader.enabled = true;

        this.Hairstyle = 196;
        this.UpdateHair();

        this.IdleAnim = "f02_idleElegant_00";
        this.WalkAnim = AnimNames.FemaleJojoWalk;

        this.OriginalIdleAnim = this.IdleAnim;
        this.OriginalWalkAnim = this.WalkAnim;

        this.BlacklightOutfit.SetActive(true);
        this.MyRenderer.sharedMesh = this.BlacklightBodyMesh;
        this.PantyAttacher.newRenderer.enabled = false;
        this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
        this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

        this.MyRenderer.materials[0].mainTexture = this.FaceTexture;
        this.MyRenderer.materials[1].mainTexture = this.NudeTexture;
        this.MyRenderer.materials[2].mainTexture = this.NudeTexture;

        //this.TheDebugMenuScript.UpdateCensor();

        this.Egg = true;
    }

    public GameObject Raincoat;
	public GameObject Rain;

	public void Weather()
	{
		if (!this.Rain.activeInHierarchy)
		{
			this.StudentManager.Clock.BloomEffect.bloomIntensity = 10f;
			this.StudentManager.Clock.BloomEffect.bloomThreshhold = 0.0f;
			this.StudentManager.Clock.UpdateBloom = true;

			SchoolGlobals.SchoolAtmosphere = 0;
			this.StudentManager.SetAtmosphere();
			this.Rain.SetActive(true);

			this.Jukebox.Start();
		}
		else
		{
			this.Hairstyle = 67;
			this.UpdateHair();

			this.Raincoat.SetActive(true);
			this.MyRenderer.sharedMesh = this.SixBodyMesh;
			this.PantyAttacher.newRenderer.enabled = false;
			this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
			this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

			this.MyRenderer.materials[0].mainTexture = this.FaceTexture;
			this.MyRenderer.materials[1].mainTexture = this.NudeTexture;
			this.MyRenderer.materials[2].mainTexture = this.NudeTexture;

			this.TheDebugMenuScript.UpdateCensor();
		}
	}

	public Material HorrorSkybox;

	void Horror()
	{
		this.Rain.SetActive(false);

		RenderSettings.ambientLight = new Color(.1f, .1f, .1f);
		RenderSettings.skybox = this.HorrorSkybox;

		SchoolGlobals.SchoolAtmosphere = 0;
		this.StudentManager.SetAtmosphere();

		this.RPGCamera.desiredDistance = .33333f;

		this.Zoom.OverShoulder = true;
		this.Zoom.TargetZoom = .2f;

		this.PauseScreen.MissionMode.FPS.transform.localPosition = new Vector3(1020, -465, 0);
		this.PauseScreen.MissionMode.Watermark.gameObject.SetActive(false);
		this.PauseScreen.MissionMode.Nemesis.SetActive(true);

		this.StudentManager.Clock.MainLight.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
		this.StudentManager.Clock.gameObject.SetActive(false);
		this.StudentManager.Clock.SunFlare.SetActive(false);
		this.StudentManager.Clock.Horror = true;

		this.StudentManager.Students[1].transform.position = new Vector3(0, 0, 0);

		this.StudentManager.Headmaster.gameObject.SetActive(false);

		this.StudentManager.Reputation.gameObject.SetActive(false);

		this.StudentManager.Flashlight.gameObject.SetActive(true);
		this.StudentManager.Flashlight.BePickedUp();

		this.StudentManager.DelinquentRadio.SetActive(false);

		this.StudentManager.CounselorDoor[0].enabled = false;
		this.StudentManager.CounselorDoor[1].enabled = false;
		this.StudentManager.CounselorDoor[0].Prompt.enabled = false;
		this.StudentManager.CounselorDoor[1].Prompt.enabled = false;

		this.StudentManager.Portal.SetActive(false);

		RenderSettings.ambientLight = new Color(.1f, .1f, .1f);

		ID = 1;

		while (ID < 101)
		{
			if (StudentManager.Students[ID] != null)
			{
				if (StudentManager.Students[ID].gameObject.activeInHierarchy)
				{
					StudentManager.DisableStudent(ID);
				}
			}

			ID++;
		}

		this.Egg = true;
	}

	public Texture YamikoFaceTexture;
	public Texture YamikoSkinTexture;
	public Texture YamikoAccessoryTexture;

	public GameObject LifeNotebook;
	public GameObject LifeNotePen;

	public Mesh YamikoMesh;

	void LifeNote()
	{
		for (this.ID = 1; this.ID < 101; this.ID++)
		{
			StudentGlobals.SetStudentPhotographed(this.ID, true);
		}

		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		this.LifeNotebook.transform.position = this.transform.position + this.transform.forward + new Vector3(0, 2.5f, 0);
		this.LifeNotebook.GetComponent<Rigidbody>().useGravity = true;
		this.LifeNotebook.GetComponent<Rigidbody>().isKinematic = false;

		this.LifeNotebook.gameObject.SetActive(true);

		this.MyRenderer.sharedMesh = this.YamikoMesh;
		this.MyRenderer.materials[0].mainTexture = this.YamikoSkinTexture;
		this.MyRenderer.materials[1].mainTexture = this.YamikoAccessoryTexture;
		this.MyRenderer.materials[2].mainTexture = this.YamikoFaceTexture;
		this.PantyAttacher.newRenderer.enabled = false;
		this.Schoolwear = 0;

		this.Hairstyle = 180;
		this.UpdateHair();

		this.StudentManager.NoteWindow.BecomeLifeNote();

		this.Egg = true;

		this.DebugMenu.transform.parent.GetComponent<DebugMenuScript>().UpdateCensor();
	}

	public GameObject GroundImpact;
	public GameObject NierCostume;
	public GameObject HeavySword;
	public GameObject LightSword;
	public GameObject Pod;

	public Transform LightSwordParent;
	public Transform HeavySwordParent;

	public ParticleSystem LightSwordParticles;
	public ParticleSystem HeavySwordParticles;

	public string AttackPrefix;
	public float NierDamage;

	public float[] NierSpawnTime;
	public float[] NierHardSpawnTime;

	public AudioClip NierSwoosh;

	void Nier()
	{
		this.NierCostume.SetActive(true);

		this.HeavySwordParent.gameObject.SetActive(true);
		this.LightSwordParent.gameObject.SetActive(true);

		this.HeavySword.GetComponent<WeaponTrail>().Start();
		this.LightSword.GetComponent<WeaponTrail>().Start();

		this.HeavySword.GetComponent<WeaponTrail>().enabled = false;
		this.LightSword.GetComponent<WeaponTrail>().enabled = false;

		this.Pod.SetActive(true);

		SithSpawnTime = NierSpawnTime;
		SithHardSpawnTime1 = NierHardSpawnTime;
		SithHardSpawnTime2 = NierHardSpawnTime;
		SithAudio.clip = NierSwoosh;

		this.Pod.transform.parent = null;

		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		this.MyRenderer.sharedMesh = null;

		this.PantyAttacher.newRenderer.enabled = false;
		this.Schoolwear = 0;

		this.Hairstyle = 181;
		this.UpdateHair();

		this.Egg = true;

		this.IdleAnim = AnimNames.FemaleHeroicIdle;
		this.WalkAnim = "f02_walkGraceful_00";
		this.RunAnim = "f02_nierRun_00";
		this.RunSpeed = 10;

		this.DebugMenu.transform.parent.GetComponent<DebugMenuScript>().UpdateCensor();
	}

	public GameObject ChinaDress;

	public void WearChinaDress()
	{
		this.EbolaHair.SetActive(false);

		this.EbolaWings.GetComponent<Renderer>().material.color = new Color(0, 0, 0);
		this.EbolaWings.GetComponent<Renderer>().material.SetColor("_OutlineColor", new Color(0, 0, 0));

		this.Hairstyle = 1;
		this.UpdateHair();

		this.ChinaDress.SetActive(true);
		this.Nude();

		this.PantyAttacher.newRenderer.enabled = true;
	}

	public NormalBufferView VaporwaveVisuals;
	public Material VaporwaveSkybox;
	public GameObject PalmTrees;
	public GameObject[] Trees;

	void Vaporwave()
	{
		VaporwaveVisuals.ApplyNormalView();
		RenderSettings.skybox = this.VaporwaveSkybox;

		PauseScreen.Settings.QualityManager.Obscurance.enabled = false;

		PalmTrees.SetActive(true);

		int TempID = 1;

		while (TempID < Trees.Length)
		{
			Trees[TempID].SetActive(false);
			TempID++;
		}
	}

	public Mesh SchoolSwimsuit;
	public Mesh GymUniform;
	public Mesh Towel;

	public Texture FaceTexture;
	public Texture SwimsuitTexture;
	public Texture GymTexture;
	public Texture TextureToUse;
	public Texture TowelTexture;

	public bool Casual = true;

	public void ChangeSchoolwear()
	{
		this.PantyAttacher.newRenderer.enabled = false;

		this.RightFootprintSpawner.Bloodiness = 0;
		this.LeftFootprintSpawner.Bloodiness = 0;

		if (this.ClubAttire && (this.Bloodiness == 0.0f))
		{
			this.Schoolwear = this.PreviousSchoolwear;
		}

		this.LabcoatAttacher.RemoveAccessory();

		this.Paint = false;

		// [af] Converted while loop to for loop.
		for (this.ID = 0; this.ID < this.CensorSteam.Length; this.ID++)
		{
			this.CensorSteam[this.ID].SetActive(false);
		}

		if (this.Casual)
		{
			this.TextureToUse = this.UniformTextures[StudentGlobals.FemaleUniform];
		}
		else
		{
			this.TextureToUse = this.CasualTextures[StudentGlobals.FemaleUniform];
		}

		if (this.ClubAttire && (this.Bloodiness > 0.0f) || (this.Schoolwear == 0))
		{
			this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
			this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

			this.MyRenderer.materials[0].SetFloat("_BlendAmount1", 0.0f);
			this.MyRenderer.materials[1].SetFloat("_BlendAmount1", 0.0f);

			this.MyRenderer.sharedMesh = this.Towel;

			this.MyRenderer.materials[0].mainTexture = this.TowelTexture;
			this.MyRenderer.materials[1].mainTexture = this.TowelTexture;
			this.MyRenderer.materials[2].mainTexture = this.FaceTexture;

			this.ClubAttire = false;
			this.Schoolwear = 0;
		}
		else if (this.Schoolwear == 1)
		{
			this.PantyAttacher.newRenderer.enabled = true;

			this.MyRenderer.sharedMesh = this.Uniforms[StudentGlobals.FemaleUniform];

			this.MyRenderer.materials[0].SetFloat("_BlendAmount", 1.0f);
			this.MyRenderer.materials[1].SetFloat("_BlendAmount", 1.0f);

			if (this.StudentManager.Censor)
			{
				Debug.Log("Activating shadows on Yandere-chan.");

				this.MyRenderer.materials[0].SetFloat("_BlendAmount1", 1.0f);
				this.MyRenderer.materials[1].SetFloat("_BlendAmount1", 1.0f);
				this.PantyAttacher.newRenderer.enabled = false;
			}

			this.MyRenderer.materials[0].mainTexture = this.TextureToUse;
			this.MyRenderer.materials[1].mainTexture = this.TextureToUse;
			this.MyRenderer.materials[2].mainTexture = this.FaceTexture;

			this.StartCoroutine(this.ApplyCustomCostume());
		}
		else if (this.Schoolwear == 2)
		{
			this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
			this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

			this.MyRenderer.materials[0].SetFloat("_BlendAmount1", 0.0f);
			this.MyRenderer.materials[1].SetFloat("_BlendAmount1", 0.0f);

			this.MyRenderer.sharedMesh = this.SchoolSwimsuit;

			this.MyRenderer.materials[0].mainTexture = this.SwimsuitTexture;
			this.MyRenderer.materials[1].mainTexture = this.SwimsuitTexture;
			this.MyRenderer.materials[2].mainTexture = this.FaceTexture;
		}
		else if (this.Schoolwear == 3)
		{
			this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
			this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

			this.MyRenderer.materials[0].SetFloat("_BlendAmount1", 0.0f);
			this.MyRenderer.materials[1].SetFloat("_BlendAmount1", 0.0f);

			this.MyRenderer.sharedMesh = this.GymUniform;

			this.MyRenderer.materials[0].mainTexture = this.GymTexture;
			this.MyRenderer.materials[1].mainTexture = this.GymTexture;
			this.MyRenderer.materials[2].mainTexture = this.FaceTexture;
		}

		this.CanMove = false;

		this.Outline.h.ReinitMaterials();
		this.ClubAccessory();
	}

	public Mesh JudoGiMesh;
	public Texture JudoGiTexture;

	public Mesh ApronMesh;
	public Texture ApronTexture;

	public Mesh LabCoatMesh;
	public Mesh HeadAndHands;
	public Texture LabCoatTexture;
	public RiggedAccessoryAttacher LabcoatAttacher;

	public bool Paint = false;

	public void ChangeClubwear()
	{
		this.PantyAttacher.newRenderer.enabled = false;

		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		this.MyRenderer.materials[0].SetFloat("_BlendAmount1", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount1", 0.0f);

		this.Paint = false;

		if (!this.ClubAttire)
		{
			this.ClubAttire = true;

			if (this.Club == ClubType.Art)
			{
				this.MyRenderer.sharedMesh = this.ApronMesh;

				this.MyRenderer.materials[0].mainTexture = this.ApronTexture;
				this.MyRenderer.materials[1].mainTexture = this.ApronTexture;
				this.MyRenderer.materials[2].mainTexture = this.FaceTexture;

				this.Schoolwear = 4;
				this.Paint = true;
			}
			else if (this.Club == ClubType.MartialArts)
			{
				this.MyRenderer.sharedMesh = this.JudoGiMesh;

				this.MyRenderer.materials[0].mainTexture = this.JudoGiTexture;
				this.MyRenderer.materials[1].mainTexture = this.JudoGiTexture;
				this.MyRenderer.materials[2].mainTexture = this.FaceTexture;

				this.Schoolwear = 5;
			}
			else if (this.Club == ClubType.Science)
			{
				this.LabcoatAttacher.enabled = true;
				this.MyRenderer.sharedMesh = HeadAndHands;

				if (this.LabcoatAttacher.Initialized)
				{
					this.LabcoatAttacher.AttachAccessory();
				}

				this.MyRenderer.materials[0].mainTexture = this.FaceTexture;
				this.MyRenderer.materials[1].mainTexture = this.NudeTexture;
				this.MyRenderer.materials[2].mainTexture = this.NudeTexture;

				this.Schoolwear = 6;
			}
		}
		else
		{
			this.ChangeSchoolwear();
			this.ClubAttire = false;
		}

		this.MyLocker.UpdateButtons();
	}

	public GameObject[] ClubAccessories;

	public void ClubAccessory()
	{
		// [af] Converted while loop to for loop.
		for (this.ID = 0; this.ID < this.ClubAccessories.Length; this.ID++)
		{
			GameObject accessory = this.ClubAccessories[this.ID];

			if (accessory != null)
			{
				accessory.SetActive(false);
			}
		}

		if (!this.CensorSteam[0].activeInHierarchy)
		{
			if (this.Club > ClubType.None)
			{
				if (this.ClubAccessories[(int)this.Club] != null)
				{
					this.ClubAccessories[(int)this.Club].SetActive(true);
				}
			}
		}
	}

	public void StopCarrying()
	{
        this.CurrentRagdoll = null;

        if (this.Ragdoll != null)
		{
			this.Ragdoll.GetComponent<RagdollScript>().Fall();
		}

		this.HeavyWeight = false;
		this.Carrying = false;

		this.IdleAnim = this.OriginalIdleAnim;
		this.WalkAnim = this.OriginalWalkAnim;
		this.RunAnim = this.OriginalRunAnim;
	}

	void Crouch()
	{
		this.MyController.center = new Vector3(
			this.MyController.center.x,
			0.55f,
			this.MyController.center.z);

		this.MyController.height = 0.90f;
	}

	void Crawl()
	{
		this.MyController.center = new Vector3(
			this.MyController.center.x,
			0.25f,
			this.MyController.center.z);

		this.MyController.height = 0.1f;
	}

	public void Uncrouch()
	{
		this.MyController.center = new Vector3(
			this.MyController.center.x,
			0.875f,
			this.MyController.center.z);

		this.MyController.height = 1.55f;
	}

	public GameObject Fireball;

	public bool LiftOff = false;
	public GameObject LiftOffParticles;
	public float LiftOffSpeed = 0.0f;

	public SkinnedMeshUpdater SkinUpdater;

	public Mesh RivalChanMesh;
	public Mesh TestMesh;

	void StopArmedAnim()
	{
		// [af] Converted while loop to for loop.
		for (this.ID = 0; this.ID < this.ArmedAnims.Length; this.ID++)
		{
			string armedAnim = this.ArmedAnims[this.ID];

			this.CharacterAnimation[armedAnim].weight = Mathf.Lerp(
				this.CharacterAnimation[armedAnim].weight, 0.0f, Time.deltaTime * 10.0f);
		}
	}

	public void UpdateAccessory()
	{
		if (this.AccessoryGroup != null)
		{
			this.AccessoryGroup.SetPartsActive(false);
		}
			
		if (this.AccessoryID > (this.Accessories.Length - 1))
		{
			this.AccessoryID = 0;
		}

		if (this.AccessoryID < 0)
		{
			this.AccessoryID = this.Accessories.Length - 1;
		}

		if (this.AccessoryID > 0)
		{
			this.Accessories[this.AccessoryID].SetActive(true);
			this.AccessoryGroup = this.Accessories[this.AccessoryID].GetComponent<AccessoryGroupScript>();

			if (this.AccessoryGroup != null)
			{
				this.AccessoryGroup.SetPartsActive(true);
			}
		}
	}

	void DisableHairAndAccessories()
	{
		for (this.ID = 1; this.ID < this.Accessories.Length; this.ID++)
		{
			this.Accessories[this.ID].SetActive(false);
		}

		for (this.ID = 1; this.ID < this.Hairstyles.Length; this.ID++)
		{
			this.Hairstyles[this.ID].SetActive(false);
		}
	}

	public bool BullyPhoto;

	public void BullyPhotoCheck()
	{
		Debug.Log("We are now going to perform a bully photo check.");

		for (int ID = 1; ID < 26; ID++)
		{
			if (PlayerGlobals.GetBullyPhoto(ID) > 0)
			{
				Debug.Log("Yandere-chan has a bully photo in her photo gallery!");

				this.BullyPhoto = true;
			}
		}
	}

	public void UpdatePersona(int NewPersona)
	{
		switch (NewPersona)
		{
			case 0:
				this.Persona = YanderePersonaType.Default;
				break;

			case 1:
				this.Persona = YanderePersonaType.Chill;
				break;

			case 2:
				this.Persona = YanderePersonaType.Confident;
				break;

			case 3:
				this.Persona = YanderePersonaType.Elegant;
				break;

			case 4:
				this.Persona = YanderePersonaType.Girly;
				break;

			case 5:
				this.Persona = YanderePersonaType.Graceful;
				break;

			case 6:
				this.Persona = YanderePersonaType.Haughty;
				break;

			case 7:
				this.Persona = YanderePersonaType.Lively;
				break;

			case 8:
				this.Persona = YanderePersonaType.Scholarly;
				break;

			case 9:
				this.Persona = YanderePersonaType.Shy;
				break;

			case 10:
				this.Persona = YanderePersonaType.Tough;
				break;

			case 11:
				this.Persona = YanderePersonaType.Aggressive;
				break;

			case 12:
				this.Persona = YanderePersonaType.Grunt;
				break;
		}
	}

	void SithSoundCheck()
	{
		if (this.SithBeam[1].Damage == 10 || this.NierDamage == 10)
		{
			if (this.SithSounds == 0)
			{
				if (this.CharacterAnimation["f02_" + AttackPrefix + "Attack" + SithPrefix + "_0" + this.SithCombo].time >= this.SithSpawnTime[SithCombo] - .1f)
				{
					this.SithAudio.pitch = Random.Range(.9f, 1.1f);
					this.SithAudio.Play();
					this.SithSounds++;
				}
			}
		}
		else
		{
			if (this.SithSounds == 0)
			{
				if (this.CharacterAnimation["f02_" + AttackPrefix + "Attack" + SithPrefix + "_0" + this.SithCombo].time >= this.SithHardSpawnTime1[SithCombo] - .1f)
				{
					this.SithAudio.pitch = Random.Range(.9f, 1.1f);
					this.SithAudio.Play();
					this.SithSounds++;
				}
			}
			else if (this.SithSounds == 1)
			{
				if (this.CharacterAnimation["f02_" + AttackPrefix + "Attack" + SithPrefix + "_0" + this.SithCombo].time >= this.SithHardSpawnTime2[SithCombo] - .1f)
				{
					this.SithAudio.pitch = Random.Range(.9f, 1.1f);
					this.SithAudio.Play();
					this.SithSounds++;
				}
			}
			else if (this.SithSounds == 2)
			{
				if (this.SithCombo == 1)
				{
					if (this.CharacterAnimation["f02_" + AttackPrefix + "Attack" + SithPrefix + "_0" + this.SithCombo].time >= (28.0f / 30.0f)  - .1f)
					{
						this.SithAudio.pitch = Random.Range(.9f, 1.1f);
						this.SithAudio.Play();
						this.SithSounds++;
					}
				}
			}
		}
	}

	public void UpdateSelfieStatus()
	{
		if (!Selfie)
		{
			this.Smartphone.transform.localEulerAngles = new Vector3(0, 0, 0);

			this.Smartphone.targetTexture = this.Shutter.SmartphoneScreen;
			this.HandCamera.gameObject.SetActive(true);
			this.SelfieGuide.SetActive(false);
			this.MainCamera.enabled = true;
			this.Blur.enabled = true;
		}
		else
		{
			if (this.Stance.Current == StanceType.Crawling)
			{
				this.Stance.Current = StanceType.Crouching;
			}

			this.Smartphone.transform.localEulerAngles = new Vector3(0, 180, 0);
			this.UpdateAccessory();
			this.UpdateHair();

			this.HandCamera.gameObject.SetActive(false);
			this.Smartphone.targetTexture = null;
			this.MainCamera.enabled = false;

			this.Smartphone.cullingMask &= ~(1 << LayerMask.NameToLayer("Miyuki"));
			this.AR = false;
		}
	}

	public CameraFilterScript CinematicCameraFilters;
	public CameraFilterScript CameraFilters;

	#if !UNITY_EDITOR
	void OnApplicationFocus(bool hasFocus)
	{
	    Cursor.lockState = CursorLockMode.Locked;
	}

	void OnApplicationPause(bool pauseStatus)
	{
    	Cursor.lockState = CursorLockMode.None;
	}
	#endif
}