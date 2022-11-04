using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
//using System;

public enum StudentActionType
{
	AtLocker = 0,
	Socializing = 1,
	Gaming = 2,
	Shamed = 3,
	Slave = 4,
	Relax = 5,
	SitAndTakeNotes = 6,
	Peek = 7,
	ClubAction = 8,
	SitAndSocialize = 9,
	SitAndEatBento = 10,
	ChangeShoes = 11,
	GradePapers = 12,
	Patrol = 13,
	Read = 14,
	Texting = 15,
	Mourn = 16,
	Cuddle = 17,
	Teaching = 18,
	SearchPatrol = 19,
	Wait = 20,
	Clean = 21,
	Gossip = 22,
	Graffiti = 23,
	Bully = 24,
	Follow = 25,
	Sulk = 26,
	Sleuth = 27,
	Stalk = 28,
	Sketch = 29,
	Sunbathe = 30,
	Shock = 31,
	Miyuki = 32,
	Meeting = 33,
	Lyrics = 34,
	Practice = 35,
	Sew = 36,
	Paint = 37
}

public enum StudentInteractionType
{
	Idle = 0,
	Forgiving = 1,
	ReceivingCompliment = 2,
	Gossiping = 3,
	Bye = 4,
	GivingTask = 5,
	FollowingPlayer = 6,
	GoingAway = 7,
	DistractingTarget = 8,
	PersonalGrudge = 9,
	ClubInfo = 10,
	ClubJoin = 11,
	ClubQuit = 12,
	ClubBye = 13,
	ClubActivity = 14,
	ClubUnwelcome = 15,
	ClubKick = 16,
	ClubGrudge = 17,
	ClubPractice = 18,
	NamingCrush = 19,
	ChangingAppearance = 20,
	Court = 21,
	Gift = 22,
	Feeding = 23,
	TaskInquiry = 24,
	TakingSnack = 25,
	GivingHelp = 26,
	SentToLocker = 27
}

// [af] Perhaps in the future some of the "tuples" can be split up into individual 
// elements in a list, and then the code would check if the list contains certain
// witness types.
public enum StudentWitnessType
{
	None,
	Accident,
	Blood,
	BloodAndInsanity,
	Corpse,
	Eavesdropping,
	Insanity,
	Interruption,
	Lewd,
	Murder,
	Pickpocketing,
	CleaningItem,
	Suspicious,
	Stalking,
	Theft,
	Trespassing,
	Violence,
	Poisoning,
	Weapon,
	WeaponAndBlood,
	WeaponAndBloodAndInsanity,
	WeaponAndInsanity,
	BloodPool,
	SeveredLimb,
	BloodyWeapon,
	DroppedWeapon,
	CoverUp,
	HoldingBloodyClothing
}

public enum GameOverType
{
	None,
	Blood,
	Insanity,
	Lewd,
	Murder,
	Stalking,
	Violence,
	Weapon,
}

public class StudentScript : MonoBehaviour
{
	public Quaternion targetRotation;
	public Quaternion OriginalRotation;
	public Quaternion OriginalPlateRotation;

    public SelectiveGrayscale ChaseSelectiveGrayscale;
    public YanSaveIdentifier BloodSpawnerIdentifier;
    public DrinkingFountainScript DrinkingFountain;
	public DetectionMarkerScript DetectionMarker;
	public ChemistScannerScript ChemistScanner;
	public StudentManagerScript StudentManager;
	public CameraEffectsScript CameraEffects;
	public ChangingBoothScript ChangingBooth;
	public DialogueWheelScript DialogueWheel;
	public WitnessCameraScript WitnessCamera;
    public YanSaveIdentifier HipsIdentifier;
    public StudentScript DistractionTarget;
	public CookingEventScript CookingEvent;
	public EventManagerScript EventManager;
	public GradingPaperScript GradingPaper;
	public CountdownScript FollowCountdown;
	public ClubManagerScript ClubManager;
	public LightSwitchScript LightSwitch;
	public MovingEventScript MovingEvent;
	public ShoeRemovalScript ShoeRemoval;
	public SnapStudentScript SnapStudent;
	public StruggleBarScript StruggleBar;
	public ToiletEventScript ToiletEvent;
	public WeaponScript WeaponToTakeAway;
	public DynamicGridObstacle Obstacle;
	public PhoneEventScript PhoneEvent;
	public PickpocketScript PickPocket;
	public ReputationScript Reputation;
	public StudentScript TargetStudent;
	public GenericBentoScript MyBento;
	public StudentScript FollowTarget;
	public CountdownScript Countdown;
	public Renderer SmartPhoneScreen;
    public YanSaveIdentifier YanSave;
    public StudentScript Distractor;
	public StudentScript HuntTarget;
	public StudentScript MyReporter;
	public StudentScript MyTeacher;
	public BoneSetsScript BoneSets;
	public CosmeticScript Cosmetic;
	public PickUpScript PuzzleCube;
	public SaveLoadScript SaveLoad;
	public SubtitleScript Subtitle;
	public StudentScript Follower;
	public DynamicBone OsanaHairL;
	public DynamicBone OsanaHairR;
	public ARMiyukiScript Miyuki;
	public WeaponScript MyWeapon;
	public StudentScript Partner;
	public RagdollScript Ragdoll;
	public YandereScript Yandere;
	public Camera DramaticCamera;
	public RagdollScript Corpse;
	public StudentScript Hunter;
	public DoorScript VomitDoor;
	public BrokenScript Broken;
	public PoliceScript Police;
	public PromptScript Prompt;
	public AIPath Pathfinding;
	public TalkingScript Talk;
	public CheerScript Cheer;
	public ClockScript Clock;
	public RadioScript Radio;
	public Renderer Painting;
	public JsonScript JSON;
	public SuckScript Suck;
	public Renderer Tears;

	public Rigidbody MyRigidbody;

	public Collider HorudaCollider;
	public Collider MyCollider;

	public CharacterController MyController;
	public Animation CharacterAnimation;
	public Projector LiquidProjector;

	public float VisionFOV;
	public float VisionDistance;

	public ParticleSystem DelinquentSpeechLines;
	public ParticleSystem PepperSprayEffect;
	public ParticleSystem DrowningSplashes;
	public ParticleSystem BloodFountain;
	public ParticleSystem VomitEmitter;
	public ParticleSystem SpeechLines;
	public ParticleSystem BullyDust;
	public ParticleSystem ChalkDust;
	public ParticleSystem Hearts;

	public Texture KokonaPhoneTexture;
	public Texture MidoriPhoneTexture;
	public Texture OsanaPhoneTexture;
	public Texture RedBookTexture;
	public Texture BloodTexture;
	public Texture WaterTexture;
	public Texture GasTexture;

	public SkinnedMeshRenderer MyRenderer;
	public Renderer BookRenderer;

	public Transform LastSuspiciousObject2;
	public Transform LastSuspiciousObject;
	public Transform CurrentDestination;
	//public Transform TeacherTalkPoint;
	public Transform LeftMiddleFinger;
	public Transform WeaponBagParent;
	public Transform LeftItemParent;
	public Transform PetDestination;
	public Transform SketchPosition;
	public Transform CleaningSpot;
	public Transform SleuthTarget;
	public Transform Distraction;
	public Transform StalkTarget;
	public Transform ItemParent;
	public Transform WitnessPOV;
	public Transform RightDrill;
	public Transform BloodPool;
	public Transform LeftDrill;
	public Transform LeftPinky;
	public Transform MapMarker;
	public Transform RightHand;
	public Transform LeftHand;
	public Transform MeetSpot;
	public Transform MyLocker;
	public Transform MyPlate;
	public Transform Spine;
	public Transform Eyes;
	public Transform Head;
	public Transform Hips;
	public Transform Neck;
	public Transform Seat;
    public Transform LipL;
    public Transform LipR;
    public Transform Jaw;

    public ParticleSystem[] LiquidEmitters;
	public ParticleSystem[] SplashEmitters;
	public ParticleSystem[] FireEmitters;

	// [af] A student's destinations and actions for each "time block" of their schedule.
	public ScheduleBlock[] ScheduleBlocks; 

	public Transform[] Destinations;
	public Transform[] LongHair;
	public Transform[] Skirt;
	public Transform[] Arm;

	public DynamicBone[] BlackHoleEffect;
	public OutlineScript[] Outlines;

	public GameObject[] InstrumentBag;
	public GameObject[] ScienceProps;
	public GameObject[] Instruments;
	public GameObject[] Chopsticks;
	public GameObject[] Drumsticks;
	public GameObject[] Fingerfood;
	public GameObject[] Bones;

	public string[] DelinquentAnims;
	public string[] AnimationNames;
	public string[] GossipAnims;
	public string[] SleuthAnims;
	public string[] CheerAnims;

	[SerializeField] List<int> VisibleCorpses = new List<int>();
	[SerializeField] int[] CorpseLayers = new int[] { 11, 14 };
    [SerializeField] LayerMask YandereCheckMask;
    [SerializeField] LayerMask Mask;

	public StudentActionType CurrentAction;
	public StudentActionType[] Actions;
	public StudentActionType[] OriginalActions;

	public AudioClip MurderSuicideKiller;
	public AudioClip MurderSuicideVictim;
	public AudioClip MurderSuicideSounds;
	public AudioClip PoisonDeathClip;
	public AudioClip PepperSpraySFX;
	public AudioClip BurningClip;

	public AudioSource AirGuitar;

	public AudioClip[] FemaleAttacks;
	public AudioClip[] BullyGiggles;
	public AudioClip[] BullyLaughs;
	public AudioClip[] MaleAttacks;

	public SphereCollider HipCollider;
	public Collider RightHandCollider;
	public Collider LeftHandCollider;
	public Collider NotFaceCollider;
	public Collider PantyCollider;
	public Collider SkirtCollider;
	public Collider FaceCollider;

	public Collider NEStairs;
	public Collider NWStairs;
	public Collider SEStairs;
	public Collider SWStairs;

	public GameObject BloodSprayCollider;
	public GameObject BullyPhotoCollider;
	public GameObject SquishyBloodEffect;
	public GameObject WhiteQuestionMark;
	public GameObject MiyukiGameScreen;
	public GameObject EmptyGameObject;
	public GameObject StabBloodEffect;
	public GameObject BigWaterSplash;
	public GameObject SecurityCamera;
	public GameObject RightEmptyEye;
	public GameObject Handkerchief;
	public GameObject LeftEmptyEye;
	public GameObject AnimatedBook;
	public GameObject BloodyScream;
	public GameObject BloodEffect;
	public GameObject CameraFlash;
	public GameObject ChaseCamera;
	public GameObject DeathScream;
	public GameObject PepperSpray;
	public GameObject PinkSeifuku;
	public GameObject WateringCan;
	public GameObject BagOfChips;
	public GameObject BloodSpray;
	public GameObject Sketchbook;
	public GameObject SmartPhone;
	public GameObject OccultBook;
	public GameObject Paintbrush;
	public GameObject AlarmDisc;
	public GameObject Character;
	public GameObject Cigarette;
	public GameObject EventBook;
	public GameObject Handcuffs;
	public GameObject HealthBar;
	public GameObject OsanaHair;
	public GameObject WeaponBag;
	public GameObject CandyBar;
	public GameObject Earpiece;
	public GameObject Scrubber;
	public GameObject Armband;
	public GameObject BookBag;
	public GameObject Lighter;
	public GameObject MyPaper;
	public GameObject Octodog;
	public GameObject Palette;
	public GameObject Eraser;
	public GameObject Giggle;
	public GameObject Marker;
	public GameObject Pencil;
	public GameObject Weapon;
	public GameObject Bento;
	public GameObject Paper;
	public GameObject Note;
	public GameObject Pen;
	public GameObject Lid;

	public bool InvestigatingPossibleDeath = false;
	public bool InvestigatingPossibleLimb = false;
	public bool SpecialRivalDeathReaction = false;
	public bool WitnessedMindBrokenMurder = false;
	public bool ReturningMisplacedWeapon = false;
	public bool SenpaiWitnessingRivalDie = false;
	public bool TargetedForDistraction = false;
	public bool SchoolwearUnavailable = false;
	public bool WitnessedBloodyWeapon = false;
	public bool IgnoringPettyActions = false;
	public bool ReturnToRoutineAfter = false;
	public bool MustChangeClothing = false;
	public bool SawCorpseThisFrame = false;
	public bool WitnessedBloodPool = false;
	public bool WitnessedSomething = false;
	public bool FoundFriendCorpse = false;
	public bool OriginallyTeacher = false;
    public bool ReturningFromSave = false;
    public bool DramaticReaction = false;
	public bool EventInterrupted = false;
	public bool FoundEnemyCorpse = false;
	public bool LostTeacherTrust = false;
	public bool WitnessedCoverUp = false;
	public bool WitnessedCorpse = false;
	public bool WitnessedMurder = false;
	public bool WitnessedWeapon = false;
	public bool VerballyReacted = false;
	public bool YandereInnocent = false;
	public bool GetNewAnimation = true;
	public bool AttackWillFail = false;
	public bool CanStillNotice = false;
	public bool FocusOnYandere = false;
	public bool ManualRotation = false;
	public bool PinDownWitness = false;
	public bool RepeatReaction = false;
	public bool StalkerFleeing = false;
	public bool YandereVisible = false;
	public bool CrimeReported = false;
	public bool FleeWhenClean = false;
	public bool MurderSuicide = false;
	public bool PhotoEvidence = false;
	public bool RespectEarned = false;
	public bool WitnessedLimb = false;
	public bool BeenSplashed = false;
	public bool BoobsResized = false;
	public bool CheckingNote = false;
	public bool ClubActivity = false;
	public bool Complimented = false;
	public bool Electrocuted = false;
	public bool FragileSlave = false;
	public bool HoldingHands = false;
	public bool PlayingAudio = false;
	public bool StopRotating = false;
	public bool SawFriendDie = false;
	public bool SentToLocker = false;
	public bool TurnOffRadio = false;
	public bool BusyAtLunch = false;
    public bool Electrified = false;
	public bool HeardScream = false;
	public bool IgnoreBlood = false;
	public bool MusumeRight = false;
    public bool NeckSnapped = false;
    public bool UpdateSkirt = false;
	public bool ClubAttire = false;
	public bool ClubLeader = false;
	public bool Confessing = false;
	public bool Distracted = false;
	public bool KilledMood = false;
	public bool LewdPhotos = false;
	public bool InDarkness = false;
	public bool SwitchBack = false;
	public bool Threatened = false;
	public bool BatheFast = false;
	public bool Counselor = false;
	public bool Depressed = false;
	public bool DiscCheck = false;
	public bool DressCode = false;
	public bool Drownable = false;
	public bool EndSearch = false;
	public bool GasWarned = false;
	public bool KnifeDown = false;
	public bool LongSkirt = false;
	public bool NoBreakUp = false;
	public bool Phoneless = false;
    public bool RingReact = false;
    public bool TrueAlone = false;
	public bool WillChase = false;
    public bool Attacked = false;
	public bool Headache = false;
	public bool Gossiped = false;
	public bool Pushable = false;
	public bool Replaced = false;
	public bool Restless = false;
	public bool SentHome = false;
	public bool Splashed = false;
	public bool Tranquil = false;
	public bool WalkBack = false;
    public bool Alarmed = false;
	public bool BadTime = false;
	public bool Bullied = false;
	public bool Drowned = false;
	public bool Forgave = false;
	public bool Indoors = false;
	public bool InEvent = false;
	public bool Injured = false;
	public bool Nemesis = false;
	public bool Private = false;
	public bool Reacted = false;
    public bool Removed = false;
	public bool SawMask = false;
	public bool Sedated = false;
	public bool SlideIn = false;
	public bool Spawned = false;
	public bool Started = false;
	public bool Suicide = false;
	public bool Teacher = false;
	public bool Tripped = false;
	public bool Witness = false;
	public bool Bloody = false;
	public bool CanTalk = true;
	public bool Emetic = false;
	public bool Lethal = false;
	public bool Routine = true;
	public bool GoAway = false;
	public bool Grudge = false;
	public bool Hungry = false;
	public bool Hunted = false;
	public bool NoTalk = false;
	public bool Paired = false;
	public bool Pushed = false;
	public bool Urgent = false;
	public bool Warned = false;
	public bool Alone = false;
	public bool Blind = false;
	public bool Eaten = false;
	public bool Hurry = false;
	public bool Rival = false;
	public bool Slave = false;
	public bool Alive { get { return this.DeathType == DeathType.None; } }
	public bool Calm = false;
	public bool Halt = false;
	public bool Lost = false;
	public bool Male = false;
	public bool Rose = false;
	public bool Safe = false;
	public bool Stop = false;
	public bool AoT = false;
	public bool Fed = false;
	public bool Gas = false;
	public bool Shy = false;
	public bool Wet = false;
	public bool Won = false;
	public bool DK = false;

	public bool NotAlarmedByYandereChan = false;
	public bool InvestigatingBloodPool = false;
	public bool RetreivingMedicine = false;
	public bool ResumeDistracting = false;
	public bool BreakingUpFight = false;
	public bool SeekingMedicine = false;
	public bool ReportingMurder = false;
	public bool CameraReacting = false;
	public bool UsingRigidbody = false;
	public bool ReportingBlood = false;
	public bool FightingSlave = false;
	public bool Investigating = false;
	public bool SolvingPuzzle = false;
    public bool ChangingShoes = false;
    public bool Distracting = false;
	public bool EatingSnack = false;
	public bool HitReacting = false;
	public bool PinningDown = false;
	public bool Struggling = false;
	public bool Following = false;
	public bool Sleuthing = false;
	public bool Stripping = false;
	public bool Fighting = false;
	public bool Guarding = false;
	public bool Ignoring = false;
	public bool Spraying = false;
	public bool Tripping = false;
	public bool Vomiting = false;
	public bool Burning = false;
	public bool Chasing = false;
	public bool Curious = false;
	public bool Fleeing = false;
	public bool Hunting = false;
	public bool Leaving = false;
	public bool Meeting = false;
	public bool Shoving = false;
	public bool Talking = false;
	public bool Waiting = false;
	public bool Dodging = false;
	public bool Posing = false;
	public bool Dying = false;

	public float DistanceToDestination = 0.0f;
	public float FollowTargetDistance = 0.0f;
	public float DistanceToPlayer = 0.0f;
	public float TargetDistance = 0.0f;
	public float ThreatDistance = 0.0f;

	public float WitnessCooldownTimer = 0.0f;
	public float InvestigationTimer = 0.0f;
	public float CameraPoseTimer = 0.0f;
	public float RivalDeathTimer = 0.0f;
	public float CuriosityTimer = 0.0f;
	public float DistractTimer = 0.0f;
	public float DramaticTimer = 0.0f;
	public float MedicineTimer = 0.0f;
	public float ReactionTimer = 0.0f;
	public float WalkBackTimer = 0.0f;
	public float AmnesiaTimer = 0.0f;
	public float ElectroTimer = 0.0f;
	public float PuzzleTimer = 0.0f;
	public float GiggleTimer = 0.0f;
	public float GoAwayTimer = 0.0f;
	public float IgnoreTimer = 0.0f;
	public float LyricsTimer = 0.0f;
	public float MiyukiTimer = 0.0f;
	public float MusumeTimer = 0.0f;
	public float PatrolTimer = 0.0f;
	public float ReportTimer = 0.0f;
	public float SplashTimer = 0.0f;
	public float ThreatTimer = 0.0f;
	public float UpdateTimer = 0.0f;
	public float AlarmTimer = 0.0f;
	public float BatheTimer = 0.0f;
	public float ChaseTimer = 0.0f;
	public float CheerTimer = 0.0f;
	public float CleanTimer = 0.0f;
	public float LaughTimer = 0.0f;
	public float RadioTimer = 0.0f;
	public float SnackTimer = 0.0f;
	public float SprayTimer = 0.0f;
	public float StuckTimer = 0.0f;
	public float ClubTimer = 0.0f;
	public float MeetTimer = 0.0f;
	public float SulkTimer = 0.0f;
	public float TalkTimer = 0.0f;
	public float WaitTimer = 0.0f;
	public float SewTimer = 0.0f;

	public float OriginalYPosition = 0.0f;
	public float PreviousEyeShrink = 0.0f;
	public float PhotoPatience = 0.0f;
	public float PreviousAlarm = 0.0f;
	public float ClubThreshold = 6.0f;
	public float RepDeduction = 0.0f;
	public float RepRecovery = 0.0f;
	public float BreastSize = 0.0f;
	public float DodgeSpeed = 2.0f;
	public float Hesitation = 0.0f;
	public float PendingRep = 0.0f;
	public float Perception = 1.0f;
	public float EyeShrink = 0.0f;
	public float MeetTime = 0.0f;
	public float Paranoia = 0.0f;
	public float RepLoss = 0.0f;
	public float Health = 100.0f;
	public float Alarm = 0.0f;

	public int ReturningMisplacedWeaponPhase = 0;
	public int RetrieveMedicinePhase = 0;
	public int WitnessRivalDiePhase = 0;
	public int ChangeClothingPhase = 0;
	public int InvestigationPhase = 0;
	public int MurderSuicidePhase = 0;
	public int ClubActivityPhase = 0;
	public int SeekMedicinePhase = 0;
	public int CameraReactPhase = 0;
	public int CuriosityPhase = 0;
	public int DramaticPhase = 0;
	public int GraffitiPhase = 0;
	public int SentHomePhase = 0;
	public int SunbathePhase = 0;
	public int ConfessPhase = 1;
	public int SciencePhase = 0;
	public int LyricsPhase = 0;
	public int ReportPhase = 0;
	public int SplashPhase = 0;
	public int ThreatPhase = 1;
	public int BathePhase = 0;
	public int BullyPhase = 0;
	public int RadioPhase = 1;
	public int SnackPhase = 0;
	public int VomitPhase = 0;
	public int ClubPhase = 0;
	public int SulkPhase = 0;
	public int TaskPhase = 0;
	public int ReadPhase = 0;
	public int PinPhase = 0;
	public int Phase = 0;

	public PersonaType OriginalPersona = PersonaType.None;

	public StudentInteractionType Interaction = StudentInteractionType.Idle;

    public int BloodPoolsSpawned = 0;
    public int LovestruckTarget = 0;
	public int MurdersWitnessed = 0;
	public int WeaponWitnessed = 0;
	public int MurderReaction = 0;
    public int PhaseFromSave = 0;
    public int CleaningRole = 0;
	public int StruggleWait = 0;
	public int TimesAnnoyed = 0;
	public int GossipBonus = 0;
	public int DeathCause = 0;
	public int Schoolwear = 0;
	public int SkinColor = 3;
	public int Attempts = 0;
	public int Patience = 5;
	public int Pestered = 0;
	public int RepBonus = 0;
	public int Strength = 0;
	public int Concern = 0;
	public int Defeats = 0;
	public int Crush = 0;

	public StudentWitnessType PreviouslyWitnessed = StudentWitnessType.None;
	public StudentWitnessType Witnessed = StudentWitnessType.None;
	public GameOverType GameOverCause = GameOverType.None;
	public DeathType DeathType = DeathType.None;

	public string CurrentAnim = string.Empty;
	public string RivalPrefix = string.Empty;
	public string RandomAnim = string.Empty;
	public string Accessory = string.Empty;
	public string Hairstyle = string.Empty;
	public string Suffix = string.Empty;
	public string Name = string.Empty;

	public string OriginalOriginalWalkAnim = string.Empty;
	public string OriginalIdleAnim = string.Empty;
	public string OriginalWalkAnim = string.Empty;
	public string OriginalSprintAnim = string.Empty;
	public string OriginalLeanAnim = string.Empty;
	public string WalkAnim = string.Empty;
	public string RunAnim = string.Empty;
	public string SprintAnim = string.Empty;
	public string IdleAnim = string.Empty;
	public string Nod1Anim = string.Empty;
	public string Nod2Anim = string.Empty;
	public string DefendAnim = string.Empty;
	public string DeathAnim = string.Empty;
	public string ScaredAnim = string.Empty;
	public string EvilWitnessAnim = string.Empty;
	public string LookDownAnim = string.Empty;
	public string PhoneAnim = string.Empty;
	public string AngryFaceAnim = string.Empty;
	public string ToughFaceAnim = string.Empty;
	public string InspectAnim = string.Empty;
	public string GuardAnim = string.Empty;
	public string CallAnim = string.Empty;
	public string CounterAnim = string.Empty;
	public string PushedAnim = string.Empty;
	public string GameAnim = string.Empty;
	public string BentoAnim = string.Empty;
	public string EatAnim = string.Empty;
	public string DrownAnim = string.Empty;
	public string WetAnim = string.Empty;
	public string SplashedAnim = string.Empty;
	public string StripAnim = string.Empty;
	public string ParanoidAnim = string.Empty;
	public string GossipAnim = string.Empty;
	public string SadSitAnim = string.Empty;
	public string BrokenAnim = string.Empty;
	public string BrokenSitAnim = string.Empty;
	public string BrokenWalkAnim = string.Empty;
	public string FistAnim = string.Empty;
	public string AttackAnim = string.Empty;
	public string SuicideAnim = string.Empty;
	public string RelaxAnim = string.Empty;
	public string SitAnim = string.Empty;
	public string ShyAnim = string.Empty;
	public string PeekAnim = string.Empty;
	public string ClubAnim = string.Empty;
	public string StruggleAnim = string.Empty;
	public string StruggleWonAnim = string.Empty;
	public string StruggleLostAnim = string.Empty;
	public string SocialSitAnim = string.Empty;
	public string CarryAnim = string.Empty;
	public string ActivityAnim = string.Empty;
	public string GrudgeAnim = string.Empty;
	public string SadFaceAnim = string.Empty;
	public string CowardAnim = string.Empty;
	public string EvilAnim = string.Empty;
	public string SocialReportAnim = string.Empty;
	public string SocialFearAnim = string.Empty;
	public string SocialTerrorAnim = string.Empty;
	public string BuzzSawDeathAnim = string.Empty;
	public string SwingDeathAnim = string.Empty;
	public string CyborgDeathAnim = string.Empty;
	public string WalkBackAnim = string.Empty;
	public string PatrolAnim = string.Empty;
	public string RadioAnim = string.Empty;
	public string BookSitAnim = string.Empty;
	public string BookReadAnim = string.Empty;
	public string LovedOneAnim = string.Empty;
	public string CuddleAnim = string.Empty;
	public string VomitAnim = string.Empty;
	public string WashFaceAnim = string.Empty;
	public string EmeticAnim = string.Empty;
	public string BurningAnim = string.Empty;
	public string JojoReactAnim = string.Empty;
	public string TeachAnim = string.Empty;
	public string LeanAnim = string.Empty;
	public string DeskTextAnim = string.Empty;
	public string CarryShoulderAnim = string.Empty;
	public string ReadyToFightAnim = string.Empty;
	public string SearchPatrolAnim = string.Empty;
	public string DiscoverPhoneAnim = string.Empty;
	public string WaitAnim = string.Empty;
	public string ShoveAnim = string.Empty;
	public string SprayAnim = string.Empty;
	public string SithReactAnim = string.Empty;
	public string EatVictimAnim = string.Empty;
	public string RandomGossipAnim = string.Empty;
	public string CuteAnim = string.Empty;
	public string BulliedIdleAnim = string.Empty;
	public string BulliedWalkAnim = string.Empty;
	public string BullyVictimAnim = string.Empty;
	public string SadDeskSitAnim = string.Empty;
	public string ConfusedSitAnim = string.Empty;
	public string SentHomeAnim = string.Empty;
	public string RandomCheerAnim = string.Empty;
	public string ParanoidWalkAnim = string.Empty;
	public string SleuthIdleAnim = string.Empty;
	public string SleuthWalkAnim = string.Empty;
	public string SleuthCalmAnim = string.Empty;
	public string SleuthScanAnim = string.Empty;
	public string SleuthReactAnim = string.Empty;
	public string SleuthSprintAnim = string.Empty;
	public string SleuthReportAnim = string.Empty;
	public string RandomSleuthAnim = string.Empty;
	public string BreakUpAnim = string.Empty;
	public string PaintAnim = string.Empty;
	public string SketchAnim = string.Empty;
	public string RummageAnim = string.Empty;
	public string ThinkAnim = string.Empty;
	public string ActAnim = string.Empty;
	public string OriginalClubAnim = string.Empty;
	public string MiyukiAnim = string.Empty;
	public string VictoryAnim = string.Empty;
	public string PlateIdleAnim = string.Empty;
	public string PlateWalkAnim = string.Empty;
	public string PlateEatAnim = string.Empty;
	public string PrepareFoodAnim = string.Empty;
	public string PoisonDeathAnim = string.Empty;
	public string HeadacheAnim = string.Empty;
	public string HeadacheSitAnim = string.Empty;
	public string ElectroAnim = string.Empty;
	public string EatChipsAnim = string.Empty;
	public string DrinkFountainAnim = string.Empty;
	public string PullBoxCutterAnim = string.Empty;
	public string TossNoteAnim = string.Empty;
	public string KeepNoteAnim = string.Empty;
	public string BathingAnim = string.Empty;
	public string DodgeAnim = string.Empty;
	public string InspectBloodAnim = string.Empty;
	public string PickUpAnim = string.Empty;
	public string PuzzleAnim = string.Empty;

	public string[] CleanAnims;

	public string[] CameraAnims;
	public string[] SocialAnims;
	public string[] CowardAnims;
	public string[] EvilAnims;
	public string[] HeroAnims;
	public string[] TaskAnims;
	public string[] PhoneAnims;

	public int ClubMemberID = 0;
	public int StudentID = 0;
	public int PatrolID = 0;
	public int SleuthID = 0;
	public int BullyID = 0;
	public int CleanID = 0;
	public int GirlID = 0;
	public int Class = 0;
	public int ID = 0;

	public PersonaType Persona = PersonaType.None;
	public ClubType OriginalClub = ClubType.None;
	public ClubType Club = ClubType.None;

	public Vector3 OriginalPlatePosition;
	public Vector3 OriginalPosition;
	public Vector3 LastKnownCorpse;
	public Vector3 DistractionSpot;
	public Vector3 LastKnownBlood;
	public Vector3 RightEyeOrigin;
	public Vector3 LeftEyeOrigin;
	public Vector3 PreviousSkirt;
	public Vector3 LastPosition;
	public Vector3 BurnTarget;

	public Transform RightBreast;
	public Transform LeftBreast;
	public Transform RightEye;
	public Transform LeftEye;

	public int Frame = 0;

	private float MaxSpeed = 10.0f;

	const string RIVAL_PREFIX = "Rival ";

	public Vector3[] SkirtPositions;
	public Vector3[] SkirtRotations;
	public Vector3[] SkirtOrigins;

	public void Start()
	{
		this.CounterAnim = "f02_teacherCounterB_00";

		if (!this.Started)
		{
			this.CharacterAnimation = this.Character.GetComponent<Animation>();
			this.MyBento = this.Bento.GetComponent<GenericBentoScript>();

			this.Pathfinding.repathRate += (this.StudentID * 0.01f);

			this.OriginalIdleAnim = this.IdleAnim;
			this.OriginalLeanAnim = this.LeanAnim;

			if (!StudentManager.LoveSick)
			{
				if (SchoolAtmosphere.Type == SchoolAtmosphereType.Low)
				{
					if (Club <= ClubType.Gaming)
					{
						this.IdleAnim = this.ParanoidAnim;
					}
				}
			}

			if (ClubGlobals.Club == ClubType.Occult)
			{
				this.Perception = 0.50f;
			}

			// [af] This is the (unintuitive) Unity 5.3 way of changing emission.
			ParticleSystem.EmissionModule heartsEmission = this.Hearts.emission;
			heartsEmission.enabled = false;

			this.Prompt.OwnerType = PromptOwnerType.Person;

			this.Paranoia = 2.0f - SchoolGlobals.SchoolAtmosphere;
			
			this.VisionDistance = 
				((PlayerGlobals.PantiesEquipped == 4) ? 5.0f : 10.0f) * this.Paranoia;

			if (GameObject.Find("DetectionCamera") != null)
			{
				this.SpawnDetectionMarker();
			}

			// [af] Commented in JS code.
			//Debug.Log(JSON);			
			//Debug.Log(JSON.StudentTimes[StudentID]);

			StudentJson studentJson = this.JSON.Students[this.StudentID];
			this.ScheduleBlocks = studentJson.ScheduleBlocks;
			this.Persona = studentJson.Persona;
			this.Class = studentJson.Class;
			this.Crush = studentJson.Crush;
			this.Club = studentJson.Club;
			this.BreastSize = studentJson.BreastSize;
			this.Strength = studentJson.Strength;
			this.Hairstyle = studentJson.Hairstyle;
			this.Accessory = studentJson.Accessory;
			this.Name = studentJson.Name;
			this.OriginalClub = this.Club;

			if (StudentGlobals.GetStudentBroken(this.StudentID))
			{
				this.Cosmetic.RightEyeRenderer.gameObject.SetActive(false);
				this.Cosmetic.LeftEyeRenderer.gameObject.SetActive(false);
				this.Cosmetic.RightIrisLight.SetActive(false);
				this.Cosmetic.LeftIrisLight.SetActive(false);
				this.RightEmptyEye.SetActive(true);
				this.LeftEmptyEye.SetActive(true);
				this.Shy = true;

				this.Persona = PersonaType.Coward;
			}

			if (this.Name == "Random")
			{
				this.Persona = (PersonaType)Random.Range(1, 8);

				if (this.Persona == (PersonaType)7)
				{
					this.Persona = (PersonaType)10;
				}

				studentJson.Persona = this.Persona;

				if (this.Persona == PersonaType.Heroic)
				{
					this.Strength = Random.Range(1, 5);
					studentJson.Strength = this.Strength;
				}
			}

			this.Seat = this.StudentManager.Seats[this.Class].List[studentJson.Seat];

			this.gameObject.name = "Student_" + this.StudentID.ToString() + " (" + this.Name + ")";

			this.OriginalPersona = this.Persona;

			if (this.StudentID == 81)
			{
				if (StudentGlobals.GetStudentBroken(81))
				{
					this.Persona = PersonaType.Coward;
				}
			}

			// Loner or Coward.
			if (this.Persona == PersonaType.Loner || this.Persona == PersonaType.Coward || this.Persona == PersonaType.Fragile)
			{
				this.CameraAnims = this.CowardAnims;
			}
			// Teacher's Pet or Hero or Teacher.
			else if ((this.Persona == PersonaType.TeachersPet) || 
				(this.Persona == PersonaType.Heroic) || (this.Persona == PersonaType.Strict))
			{
				//this.ScaredAnim = this.ReadyToFightAnim;
				//this.ParanoidAnim = this.GuardAnim;
				this.CameraAnims = this.HeroAnims;
			}
			// Evil.
			else if (this.Persona == PersonaType.Evil || this.Persona == PersonaType.Spiteful || this.Persona == PersonaType.Violent)
			{
				this.CameraAnims = this.EvilAnims;
			}
			// Social.
			else if (this.Persona == PersonaType.SocialButterfly || 
					 this.Persona == PersonaType.Lovestruck ||
					 this.Persona == PersonaType.PhoneAddict ||
					 this.Persona == PersonaType.Sleuth)
			{
				this.CameraAnims = this.SocialAnims;
			}

			if (ClubGlobals.GetClubClosed(this.Club))
			{
				this.Club = ClubType.None;
			}

			this.DialogueWheel = GameObject.Find("DialogueWheel").GetComponent<DialogueWheelScript>();
			this.ClubManager = GameObject.Find("ClubManager").GetComponent<ClubManagerScript>();
			this.Reputation = GameObject.Find("Reputation").GetComponent<ReputationScript>();
			this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();
			this.Police = GameObject.Find("Police").GetComponent<PoliceScript>();
			this.Clock = GameObject.Find("Clock").GetComponent<ClockScript>();
			this.Subtitle = this.Yandere.Subtitle;

			this.CameraEffects = this.Yandere.MainCamera.GetComponent<CameraEffectsScript>();

			this.RightEyeOrigin = this.RightEye.localPosition;
			this.LeftEyeOrigin = this.LeftEye.localPosition;

			this.Bento.GetComponent<GenericBentoScript>().StudentID = this.StudentID;

			this.HealthBar.transform.parent.gameObject.SetActive(false);
			this.FollowCountdown.gameObject.SetActive(false);
			this.VomitEmitter.gameObject.SetActive(false);
			this.ChaseCamera.gameObject.SetActive(false);
			this.Countdown.gameObject.SetActive(false);
			this.MiyukiGameScreen.SetActive(false);
			this.Chopsticks[0].SetActive(false);
			this.Chopsticks[1].SetActive(false);
			this.Sketchbook.SetActive(false);
			this.SmartPhone.SetActive(false);
			this.OccultBook.SetActive(false);
			this.Paintbrush.SetActive(false);
			this.EventBook.SetActive(false);
			this.Handcuffs.SetActive(false);
			this.Scrubber.SetActive(false);
			this.Octodog.SetActive(false);
			this.Palette.SetActive(false);
			this.Eraser.SetActive(false);
			this.Pencil.SetActive(false);
			this.Bento.SetActive(false);
			this.Pen.SetActive(false);
			this.SpeechLines.Stop();

			foreach (GameObject prop in this.ScienceProps)
			{
				if (prop != null)
				{
					prop.SetActive(false);
				}
			}

			foreach (GameObject prop in this.Fingerfood)
			{
				if (prop != null)
				{
					prop.SetActive(false);
				}
			}

			OriginalOriginalWalkAnim = this.WalkAnim;

			this.OriginalSprintAnim = this.SprintAnim;
			this.OriginalWalkAnim = this.WalkAnim;

			if (this.Persona == PersonaType.Evil)
			{
				this.ScaredAnim = this.EvilWitnessAnim;
			}

			if (this.Persona == PersonaType.PhoneAddict)
			{
				this.SmartPhone.transform.localPosition = new Vector3(0.01f, 0.005f, 0.01f);
				this.SmartPhone.transform.localEulerAngles = new Vector3(0, -160, 165);

				this.Countdown.Speed = 0.1f;

				this.SprintAnim = this.PhoneAnims[2];
				this.PatrolAnim = this.PhoneAnims[3];
			}

			if (this.Club == ClubType.Bully)
			{
                this.SkirtCollider.transform.localPosition = new Vector3(0, 0.055f, 0);

				if (!StudentGlobals.GetStudentBroken(this.StudentID))
				{
					this.IdleAnim = this.PhoneAnims[0];
					this.BullyID = this.StudentID - 80;
				}

				if (TaskGlobals.GetTaskStatus(36) == 3)
				{
					if (!SchoolGlobals.ReactedToGameLeader)
					{
						this.StudentManager.ReactedToGameLeader = true;

						ScheduleBlock block4 = this.ScheduleBlocks[4];
						block4.destination = "Shock";
						block4.action = "Shock";

						//this.GetDestinations();
					}
				}
			}

			if (!this.Male)
			{
				this.SkirtOrigins[0] = this.Skirt[0].transform.localPosition;
				this.SkirtOrigins[1] = this.Skirt[1].transform.localPosition;
				this.SkirtOrigins[2] = this.Skirt[2].transform.localPosition;
				this.SkirtOrigins[3] = this.Skirt[3].transform.localPosition;

				this.InstrumentBag[1].SetActive(false);
				this.InstrumentBag[2].SetActive(false);
				this.InstrumentBag[3].SetActive(false);
				this.InstrumentBag[4].SetActive(false);
				this.InstrumentBag[5].SetActive(false);

				this.PickRandomGossipAnim();

				this.DramaticCamera.gameObject.SetActive(false);
				this.AnimatedBook.SetActive(false);
				this.Handkerchief.SetActive(false);
				this.PepperSpray.SetActive(false);
				this.WateringCan.SetActive(false);
				this.Sketchbook.SetActive(false);
				this.Cigarette.SetActive(false);
				this.CandyBar.SetActive(false);
				this.Lighter.SetActive(false);

				foreach (GameObject instrument in this.Instruments)
				{
					if (instrument != null)
					{
						instrument.SetActive(false);
					}
				}

				this.Drumsticks[0].SetActive(false);
				this.Drumsticks[1].SetActive(false);

				if (this.Club >= ClubType.Teacher)
				{
					this.BecomeTeacher();
				}

				if (this.StudentManager.Censor)
				{
					if (!this.Teacher)
					{
						this.Cosmetic.CensorPanties();
					}
				}

				this.DisableEffects();
			}
			else
			{
				this.RandomCheerAnim = this.CheerAnims[Random.Range(0, this.CheerAnims.Length)];

				this.MapMarker.gameObject.SetActive(false);

				this.DelinquentSpeechLines.Stop();

				this.PinkSeifuku.SetActive(false);
				this.WeaponBag.SetActive(false);
				this.Earpiece.SetActive(false);

				this.SetSplashes(false);

				// [af] Converted while loop to foreach loop.
				foreach (ParticleSystem liquidEmitter in this.LiquidEmitters)
				{
					liquidEmitter.gameObject.SetActive(false);
				}
			}

			/////////////////////////
			///// Special Cases /////
			/////////////////////////

			//Senpai
			if (this.StudentID == 1)
			{
				//this.SmartPhone.GetComponent<Renderer>().material.mainTexture = this.OsanaPhoneTexture;
				this.MapMarker.gameObject.SetActive(true);

				//Perception = 5;
			}
			//Sakyu Basu
			else if (this.StudentID == 2)
			{
				if (StudentGlobals.GetStudentDead(3) || StudentGlobals.GetStudentKidnapped(3) ||
					this.StudentManager.Students[3].Slave)
				{
					ScheduleBlock block2 = this.ScheduleBlocks[2];
					block2.destination = "Mourn";
					block2.action = "Mourn";

                    ScheduleBlock block7 = this.ScheduleBlocks[7];
                    block7.destination = "Mourn";
                    block7.action = "Mourn";

                    this.IdleAnim = this.BulliedIdleAnim;
					this.WalkAnim = this.BulliedWalkAnim;
                }

                this.PatrolAnim = this.SearchPatrolAnim;
            }
			//Inkyu Basu
			else if (this.StudentID == 3)
			{
				if (StudentGlobals.GetStudentDead(2) || StudentGlobals.GetStudentKidnapped(2) || this.StudentManager.Students[2] == null ||
					this.StudentManager.Students[2] != null && this.StudentManager.Students[2].Slave)
				{
					ScheduleBlock block2 = this.ScheduleBlocks[2];
					block2.destination = "Mourn";
					block2.action = "Mourn";

                    ScheduleBlock block7 = this.ScheduleBlocks[7];
                    block7.destination = "Mourn";
                    block7.action = "Mourn";

                    this.IdleAnim = this.BulliedIdleAnim;
					this.WalkAnim = this.BulliedWalkAnim;
				}

                this.PatrolAnim = this.SearchPatrolAnim;
            }
			//Kuu Dere
			else if (this.StudentID == 4)
			{
				this.IdleAnim = "f02_idleShort_00";
				this.WalkAnim = "f02_newWalk_00";
			}
			//Fragile Girl
			else if (this.StudentID == 5)
			{
				this.LongSkirt = true;
				this.Shy = true;
			}
			//Suitor Boy
			else if (this.StudentID == 6)
			{
				this.RelaxAnim = "crossarms_00";
				this.CameraAnims = this.HeroAnims;

				//Anti-Osana Code
				#if UNITY_EDITOR
				this.Curious = true;
				this.Crush = 11;

				if (this.StudentManager.Students[11] == null)
				{
					this.Curious = false;
				}
				#endif
			}
			//Feminine Boy
			else if (this.StudentID == 7)
			{
				this.RunAnim = "runFeminine_00";
				this.SprintAnim = "runFeminine_00";
				this.RelaxAnim = "infirmaryRest_00";
				this.OriginalSprintAnim = this.SprintAnim;

				this.Cosmetic.Start();

				if (!GameGlobals.AlphabetMode)
				{
					this.gameObject.SetActive(false);
				}
			}
			//Shy Boy
			else if (this.StudentID == 8)
			{
				this.IdleAnim = this.BulliedIdleAnim;
				this.WalkAnim = this.BulliedWalkAnim;
			}
			//Spikey Boy
			else if (this.StudentID == 9)
			{
				this.IdleAnim = "idleScholarly_00";
				this.WalkAnim = "walkScholarly_00";
				this.CameraAnims = this.HeroAnims;
			}
			//Raibaru
			else if (this.StudentID == 10)
			{
				//Anti-Osana Code
				#if UNITY_EDITOR

				this.LovestruckTarget = 11;

				//If Osana is alive, not dating anyone, and Raibaru has a good reputation...
				if (this.StudentManager.Students[11] != null && DatingGlobals.SuitorProgress < 2 && 
					StudentGlobals.GetStudentReputation(10) > -33.33333f)
				{
					this.StudentManager.Patrols.List[this.StudentID].parent = this.StudentManager.Students[11].transform;
					this.StudentManager.Patrols.List[this.StudentID].localEulerAngles = new Vector3(0, 0, 0);
					this.StudentManager.Patrols.List[this.StudentID].localPosition = new Vector3(0, 0, 0);

					this.VisionDistance = 12;
					this.VomitPhase = -1;

					this.Indoors = true;
					this.Spawned = true;
					this.Calm = true;

					if (this.ShoeRemoval.Locker == null)
					{
						this.ShoeRemoval.Start();
					}

					this.ShoeRemoval.PutOnShoes();

					this.FollowTarget = this.StudentManager.Students[11];
					this.FollowTarget.Follower = this;

					this.IdleAnim = AnimNames.FemaleGirlyIdle;
					this.WalkAnim = AnimNames.FemaleGirlyWalk;
					this.PatrolAnim = AnimNames.FemaleStretch;
					this.OriginalIdleAnim = this.IdleAnim;

					//this.CharacterAnimation[this.PatrolAnim].speed = .5f;
				}
				//If Osana is either dead, dating, or Raibaru has a bad reputation...
				else
				{
					Debug.Log("Due to the circumstances, we're changing Raibaru's destinations.");

					//If Osana died...
					if (this.StudentManager.Students[11] == null)
					{
						ScheduleBlock newBlock2 = this.ScheduleBlocks[2];
						newBlock2.destination = "Mourn";
						newBlock2.action = "Mourn";

						ScheduleBlock newBlock4 = this.ScheduleBlocks[4];
						newBlock4.destination = "Mourn";
						newBlock4.action = "Mourn";

						this.Persona = PersonaType.Heroic;

						this.IdleAnim = this.BulliedIdleAnim;
						this.WalkAnim = this.BulliedWalkAnim;
						this.OriginalIdleAnim = this.IdleAnim;
					}
					//If Raibaru's reputation is low...
					else if (StudentGlobals.GetStudentReputation(10) <= -33.33333f)
					{
						ScheduleBlock newBlock2 = this.ScheduleBlocks[2];
						newBlock2.destination = "Seat";
						newBlock2.action = "Sit";
						newBlock2.time = 8;

						ScheduleBlock newBlock4 = this.ScheduleBlocks[4];
						newBlock4.destination = "Seat";
						newBlock4.action = "Sit";

						this.IdleAnim = this.BulliedIdleAnim;
						this.WalkAnim = this.BulliedWalkAnim;
						this.OriginalIdleAnim = this.IdleAnim;
					}
					else if (DatingGlobals.SuitorProgress == 2)
					{
						ScheduleBlock newBlock2 = this.ScheduleBlocks[2];
						newBlock2.destination = "Peek";
						newBlock2.action = "Peek";
						newBlock2.time = 8;

						ScheduleBlock newBlock4 = this.ScheduleBlocks[4];
						newBlock4.destination = "LunchSpot";
						newBlock4.action = "Eat";
					}

					ScheduleBlock newBlock3 = this.ScheduleBlocks[3];
					newBlock3.destination = "Seat";
					newBlock3.action = "Sit";

					ScheduleBlock newBlock5 = this.ScheduleBlocks[5];
					newBlock5.destination = "Seat";
					newBlock5.action = "Sit";

					ScheduleBlock newBlock6 = this.ScheduleBlocks[6];
					newBlock6.destination = "Locker";
					newBlock6.action = "Shoes";

					ScheduleBlock newBlock7 = this.ScheduleBlocks[7];
					newBlock7.destination = "Exit";
					newBlock7.action = "Exit";

					ScheduleBlock newBlock8 = this.ScheduleBlocks[8];
					newBlock8.destination = "Exit";
					newBlock8.action = "Exit";

					ScheduleBlock newBlock9 = this.ScheduleBlocks[9];
					newBlock9.destination = "Exit";
					newBlock9.action = "Exit";

					this.TargetDistance = .5f;
				}

				this.PhotoPatience = 0;
				this.OriginalWalkAnim = this.WalkAnim;
				#endif

				#if !UNITY_EDITOR
				Destroy(this.gameObject);
				#endif
			}
			//Osana
			else if (this.StudentID == 11)
			{
				//Anti-Osana Code
				#if UNITY_EDITOR

				this.SmartPhone.transform.localPosition = new Vector3(-0.0075f, -0.0025f, -0.0075f);
				this.SmartPhone.transform.localEulerAngles = new Vector3(5.0f, -150.0f, 170.0f);
				this.SmartPhone.GetComponent<Renderer>().material.mainTexture = this.OsanaPhoneTexture;

				this.IdleAnim = AnimNames.FemaleTsunIdle;
				this.WalkAnim = AnimNames.FemaleTsunWalk;

				this.TaskAnims[0] = AnimNames.FemaleTask33Line0;
				this.TaskAnims[1] = AnimNames.FemaleTask33Line1;
				this.TaskAnims[2] = AnimNames.FemaleTask33Line2;
				this.TaskAnims[3] = AnimNames.FemaleTask33Line3;
				this.TaskAnims[4] = AnimNames.FemaleTask33Line4;
				this.TaskAnims[5] = AnimNames.FemaleTask33Line5;

				this.LovestruckTarget = 1;
				this.PhotoPatience = 0;

				if (this.StudentManager.Students[10] == null)
				{
					Debug.Log("Raibaru has been killed/arrested/vanished, so Osana's schedule has changed.");

					ScheduleBlock newBlock2 = this.ScheduleBlocks[2];
					newBlock2.destination = "Mourn";
					newBlock2.action = "Mourn";

					ScheduleBlock newBlock7 = this.ScheduleBlocks[7];
					newBlock7.destination = "Mourn";
					newBlock7.action = "Mourn";

					this.IdleAnim = this.BulliedIdleAnim;
					this.WalkAnim = this.BulliedWalkAnim;
				}
				else if (PlayerGlobals.RaibaruLoner)
				{
					Debug.Log("Raibaru has become a loner, so Osana's schedule has changed.");

					ScheduleBlock newBlock2 = this.ScheduleBlocks[2];
					newBlock2.destination = "Patrol";
					newBlock2.action = "Patrol";

					ScheduleBlock newBlock7 = this.ScheduleBlocks[7];
					newBlock7.destination = "Patrol";
					newBlock7.action = "Patrol";

					this.PatrolAnim = "f02_pondering_00";
				}

				this.OriginalWalkAnim = this.WalkAnim;
				#endif

				#if !UNITY_EDITOR
				Destroy(this.gameObject);
				#endif
			}
			//Cooking Girls
			else if (this.StudentID == 24 && this.StudentID == 25)
			{
				this.IdleAnim = AnimNames.FemaleGirlyIdle;
				this.WalkAnim = AnimNames.FemaleGirlyWalk;
			}
			//Tsuruzo Yamazaki (Drama Club Substitute Leader)
			else if (this.StudentID == 26)
			{
				this.IdleAnim = "idleHaughty_00";
				this.WalkAnim = "walkHaughty_00";
			}
			//Kokona and Riku
			else if ((this.StudentID == 28) || (this.StudentID == 30))
			{
				if (this.StudentID == 30)
				{
					this.SmartPhone.GetComponent<Renderer>().material.mainTexture = this.KokonaPhoneTexture;
				}
			}
			//Shin Higaku
			else if (this.StudentID == 31)
			{
				this.IdleAnim = this.BulliedIdleAnim;
				this.WalkAnim = this.BulliedWalkAnim;
			}
			//Supana Churu and Kokouma Jutsu
			else if (this.StudentID == 34 || this.StudentID == 35)
			{
				this.IdleAnim = "f02_idleShort_00";
				this.WalkAnim = "f02_newWalk_00";

				if (this.Paranoia > 1.66666f)
				{
					this.IdleAnim = this.ParanoidAnim;
					this.WalkAnim = this.ParanoidWalkAnim;
				}
			}
			//Gema Taku
			else if (this.StudentID == 36)
			{
				if (TaskGlobals.GetTaskStatus(36) < 3)
				{
					this.IdleAnim = "slouchIdle_00";
					this.WalkAnim = "slouchWalk_00";
					this.ClubAnim = "slouchGaming_00";
				}
				else
				{
					this.IdleAnim = "properIdle_00";
					this.WalkAnim = "properWalk_00";
					this.ClubAnim = "properGaming_00";
				}

				if (this.Paranoia > 1.66666f)
				{
					this.IdleAnim = this.ParanoidAnim;
					this.WalkAnim = this.ParanoidWalkAnim;
				}
			}
			//Midori
			else if (this.StudentID == 39)
			{
				this.SmartPhone.GetComponent<Renderer>().material.mainTexture = this.MidoriPhoneTexture;	
				this.PatrolAnim = AnimNames.FemaleMidoriTexting;
			}
			//Miyuji Shan
			else if (this.StudentID == 51)
			{
				this.IdleAnim = "f02_idleConfident_01";
				this.WalkAnim = "f02_walkConfident_01";

				if (ClubGlobals.GetClubClosed(ClubType.LightMusic))
				{
					this.IdleAnim = this.BulliedIdleAnim;
					this.WalkAnim = this.BulliedWalkAnim;
					this.CameraAnims = this.CowardAnims;
					this.Persona = PersonaType.Loner;

					if (!SchoolGlobals.RoofFence)
					{
						ScheduleBlock block2 = this.ScheduleBlocks[2];
						block2.destination = "Sulk";
						block2.action = "Sulk";

						ScheduleBlock block4 = this.ScheduleBlocks[4];
						block4.destination = "Sulk";
						block4.action = "Sulk";

						ScheduleBlock block7 = this.ScheduleBlocks[7];
						block7.destination = "Sulk";
						block7.action = "Sulk";

						ScheduleBlock block8 = this.ScheduleBlocks[8];
						block8.destination = "Sulk";
						block8.action = "Sulk";
					}
					else
					{
						ScheduleBlock block2 = this.ScheduleBlocks[2];
						block2.destination = "Seat";
						block2.action = "Sit";

						ScheduleBlock block4 = this.ScheduleBlocks[4];
						block4.destination = "Seat";
						block4.action = "Sit";

						ScheduleBlock block7 = this.ScheduleBlocks[7];
						block7.destination = "Seat";
						block7.action = "Sit";

						ScheduleBlock block8 = this.ScheduleBlocks[8];
						block8.destination = "Seat";
						block8.action = "Sit";
					}
				}
			}
			//Sleuths
			else if (this.StudentID == 56)
			{
				this.IdleAnim = "idleConfident_00";
				this.WalkAnim = "walkConfident_00";

				this.SleuthID = 0;
			}
			//Rojasu
			else if (this.StudentID == 57)
			{
				this.IdleAnim = "idleChill_01";
				this.WalkAnim = "walkChill_01";

				this.SleuthID = 20;
			}
			//Sukubi
			else if (this.StudentID == 58)
			{
				this.IdleAnim = "idleChill_00";
				this.WalkAnim = "walkChill_00";

				this.SleuthID = 40;
			}
			//Dafuni
			else if (this.StudentID == 59)
			{
				this.IdleAnim = "f02_idleGraceful_00";
				this.WalkAnim = "f02_walkGraceful_00";

				this.SleuthID = 60;
			}
			//Beruma
			else if (this.StudentID == 60)
			{
				this.IdleAnim = "f02_idleScholarly_00";
				this.WalkAnim = "f02_walkScholarly_00";

				this.CameraAnims = this.HeroAnims;

				this.SleuthID = 80;
			}
			//Kaga Kusha
			else if (this.StudentID == 61)
			{
				this.IdleAnim = "scienceIdle_00";
				this.WalkAnim = "scienceWalk_00";
				this.OriginalWalkAnim = "scienceWalk_00";
			}
			//Horo Guramu
			else if (this.StudentID == 62)
			{
				this.IdleAnim = "idleFrown_00";
				this.WalkAnim = "walkFrown_00";

				if (this.Paranoia > 1.66666f)
				{
					this.IdleAnim = this.ParanoidAnim;
					this.WalkAnim = this.ParanoidWalkAnim;
				}
			}
			//Homu Kurusu and Meka Nikaru
			else if (this.StudentID == 64 || this.StudentID == 65)
			{
				this.IdleAnim = "f02_idleShort_00";
				this.WalkAnim = "f02_newWalk_00";

				if (this.Paranoia > 1.66666f)
				{
					this.IdleAnim = this.ParanoidAnim;
					this.WalkAnim = this.ParanoidWalkAnim;
				}
			}
			//Sports Club Leader
			else if (this.StudentID == 66)
			{
				this.IdleAnim = AnimNames.MalePose03;

				this.OriginalWalkAnim = "walkConfident_00";
				this.WalkAnim = "walkConfident_00";

				this.ClubThreshold = 100.0f;
			}
			//Iruka Dorufino
			else if (this.StudentID == 69)
			{
				this.IdleAnim = "idleFrown_00";
				this.WalkAnim = "walkFrown_00";

				if (this.Paranoia > 1.66666f)
				{
					this.IdleAnim = this.ParanoidAnim;
					this.WalkAnim = this.ParanoidWalkAnim;
				}
			}
			//Uekiya
			else if (this.StudentID == 71)
			{
				this.IdleAnim = AnimNames.FemaleGirlyIdle;
				this.WalkAnim = AnimNames.FemaleGirlyWalk;
			}
				
			//Anti-Osana Code
			#if UNITY_EDITOR
			if (this.StudentID == 6 || this.StudentID == 11)
			{
				if (DatingGlobals.SuitorProgress == 2)
				{
					// [af] Replaced if/else statement with ternary expression.
					this.Partner = (this.StudentID == 11) ?
						this.StudentManager.Students[6] : this.StudentManager.Students[11];

					ScheduleBlock block2 = this.ScheduleBlocks[2];
					block2.destination = "Cuddle";
					block2.action = "Cuddle";

					ScheduleBlock block4 = this.ScheduleBlocks[4];
					block4.destination = "Cuddle";
					block4.action = "Cuddle";

					ScheduleBlock newBlock6 = this.ScheduleBlocks[6];
					newBlock6.destination = "Locker";
					newBlock6.action = "Shoes";

					ScheduleBlock newBlock7 = this.ScheduleBlocks[7];
					newBlock7.destination = "Exit";
					newBlock7.action = "Exit";
				}
			}
			#endif

			this.OriginalWalkAnim = this.WalkAnim;

			if (StudentGlobals.GetStudentGrudge(this.StudentID))
			{
				if (this.Persona != PersonaType.Coward && this.Persona != PersonaType.Evil && this.Club != ClubType.Delinquent)
				{
					this.CameraAnims = this.EvilAnims;

					this.Reputation.PendingRep -= 10.0f;
					this.PendingRep -= 10.0f;

					// [af] Converted while loop to for loop.
					for (this.ID = 0; this.ID < this.Outlines.Length; this.ID++)
					{
						this.Outlines[this.ID].color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
					}
				}

				this.Grudge = true;

				this.CameraAnims = this.EvilAnims;
			}

			if (this.Persona == PersonaType.Sleuth)
			{
				if (SchoolGlobals.SchoolAtmosphere <= .8f || this.Grudge)
				{
					this.Indoors = true;
					this.Spawned = true;

					if (this.ShoeRemoval.Locker == null)
					{
						this.ShoeRemoval.Start();
					}

					this.ShoeRemoval.PutOnShoes();

					this.SprintAnim = this.SleuthSprintAnim;

					this.IdleAnim = this.SleuthIdleAnim;
					this.WalkAnim = this.SleuthWalkAnim;
					this.CameraAnims = this.HeroAnims;
					this.SmartPhone.SetActive(true);
					this.Countdown.Speed = 0.075f;
					this.Sleuthing = true;

					if (this.Male)
					{
						this.SmartPhone.transform.localPosition = new Vector3(0.06f, -0.02f, -0.02f);
					}
					else
					{
						this.SmartPhone.transform.localPosition = new Vector3(0.033333f, -0.015f, -0.015f);
					}

					this.SmartPhone.transform.localEulerAngles = new Vector3(12.5f, 120, 180);

					if (this.Club == ClubType.None)
					{
						this.StudentManager.SleuthPhase = 3;
						this.GetSleuthTarget();
					}
					else
					{
						this.SleuthTarget = this.StudentManager.Clubs.List[this.StudentID];
					}

					if (!this.Grudge)
					{
						ScheduleBlock block2 = this.ScheduleBlocks[2];
						block2.destination = "Sleuth";
						block2.action = "Sleuth";

						ScheduleBlock block4 = this.ScheduleBlocks[4];
						block4.destination = "Sleuth";
						block4.action = "Sleuth";

						ScheduleBlock block7 = this.ScheduleBlocks[7];
						block7.destination = "Sleuth";
						block7.action = "Sleuth";
					}
					else
					{
						this.StalkTarget = this.Yandere.transform;
						this.SleuthTarget = this.Yandere.transform;

						ScheduleBlock stalkBlock2 = this.ScheduleBlocks[2];
						stalkBlock2.destination = "Stalk";
						stalkBlock2.action = "Stalk";

						ScheduleBlock stalkBlock4 = this.ScheduleBlocks[4];
						stalkBlock4.destination = "Stalk";
						stalkBlock4.action = "Stalk";

						ScheduleBlock stalkBlock7 = this.ScheduleBlocks[7];
						stalkBlock7.destination = "Stalk";
						stalkBlock7.action = "Stalk";
					}
				}
				else if (SchoolGlobals.SchoolAtmosphere <= .9f)
				{
					this.WalkAnim = this.ParanoidWalkAnim;
					this.CameraAnims = this.HeroAnims;
				}
			}

			if (!this.Slave)
			{
				if (this.StudentManager.Bullies > 1)
				{
					if (this.StudentID == 81 || this.StudentID == 83 || this.StudentID == 85)
					{
						if (this.Persona != PersonaType.Coward)
						{
							this.Pathfinding.canSearch = false;
							this.Pathfinding.canMove = false;
							this.Paired = true;

							this.CharacterAnimation["f02_walkTalk_00"].time += (StudentID - 81);
							this.WalkAnim = "f02_walkTalk_00";
							this.SpeechLines.Play();
						}
					}
					else if (this.StudentID == 82 || this.StudentID == 84)
					{
						this.Pathfinding.canSearch = false;
						this.Pathfinding.canMove = false;
						this.Paired = true;

						this.CharacterAnimation["f02_walkTalk_01"].time += (StudentID - 81);
						this.WalkAnim = "f02_walkTalk_01";
						this.SpeechLines.Play();
					}
				}

				if (this.Club == ClubType.Delinquent)
				{
					if (PlayerGlobals.GetStudentFriend(this.StudentID))
					{
						this.RespectEarned = true;
					}

					if (CounselorGlobals.WeaponsBanned == 0)
					{
						this.MyWeapon = this.Yandere.WeaponManager.DelinquentWeapons[StudentID - 75];

						this.MyWeapon.transform.parent = WeaponBagParent;
						this.MyWeapon.transform.localEulerAngles = new Vector3(0, 0, 0);
						this.MyWeapon.transform.localPosition = new Vector3(0, 0, 0);
						this.MyWeapon.FingerprintID = this.StudentID;
						this.MyWeapon.MyCollider.enabled = false;

						this.WeaponBag.SetActive(true);
					}
					else
					{
						this.OriginalPersona = PersonaType.Heroic;
						this.Persona = PersonaType.Heroic;
					}

					this.ScaredAnim = "delinquentCombatIdle_00";
					this.LeanAnim = "delinquentConcern_00";
					this.ShoveAnim = "pushTough_00";
					this.WalkAnim = "walkTough_00";
					this.IdleAnim = "idleTough_00";

					this.SpeechLines = this.DelinquentSpeechLines;

					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;
					this.Paired = true;

					this.TaskAnims[0] = "delinquentTalk_04";
					this.TaskAnims[1] = "delinquentTalk_01";
					this.TaskAnims[2] = "delinquentTalk_02";
					this.TaskAnims[3] = "delinquentTalk_03";
					this.TaskAnims[4] = "delinquentTalk_00";
					this.TaskAnims[5] = "delinquentTalk_03";
				}
			}

			if (this.StudentID == StudentManager.RivalID)
			{
				RivalPrefix = "Rival ";

				if (DateGlobals.Weekday == System.DayOfWeek.Friday)
				{
					Debug.Log("It's Friday, and " + this.Name + " should leave a note in Senpai's locker ar 5:00 PM.");

					// Move "Change Shoes" up to 5:00 PM. The "Change Shoes" code leads to the confession code.
					ScheduleBlock block7 = this.ScheduleBlocks[7];
					block7.time = 17;
				}
			}

			if (!this.Teacher && this.Name != "Random")
			{
				this.StudentManager.CleaningManager.GetRole(this.StudentID);
				this.CleaningSpot = this.StudentManager.CleaningManager.Spot;
				this.CleaningRole = this.StudentManager.CleaningManager.Role;
			}

			if (this.Club == ClubType.Cooking)
			{
				this.SleuthID = (this.StudentID - 21) * 20;

				/*
					 if (this.StudentID == 21){this.ClubAnim = "f02_rummage_00";}
				else if (this.StudentID == 22){this.ClubAnim = "f02_rummage_00";}
				else if (this.StudentID == 23){this.ClubAnim = "f02_rummage_00";}
				else if (this.StudentID == 24){this.ClubAnim = "f02_rummage_00";}
				else if (this.StudentID == 25){this.ClubAnim = "f02_rummage_00";}
				*/

				this.ClubAnim = this.PrepareFoodAnim;
				this.ClubMemberID = this.StudentID - 20;

				this.MyPlate = this.StudentManager.Plates[this.ClubMemberID];
				this.OriginalPlatePosition = this.MyPlate.position;
				this.OriginalPlateRotation = this.MyPlate.rotation;

				if (!GameGlobals.EmptyDemon)
				{
					this.ApronAttacher.enabled = true;
				}
			}
			else if (this.Club == ClubType.Drama)
			{
				if (this.StudentID == 26)
				{
					this.ClubAnim = "teaching_00";
				}
				else if (this.StudentID == 27 || this.StudentID == 28)
				{
					this.ClubAnim = "sit_00";
				}
				else if (this.StudentID == 29 || this.StudentID == 30)
				{
					this.ClubAnim = "f02_sit_00";
				}

				this.OriginalClubAnim = this.ClubAnim;
			}
			else if (this.Club == ClubType.Occult)
			{
				this.PatrolAnim = this.ThinkAnim;
				this.CharacterAnimation[PatrolAnim].speed = .2f;
			}
			else if (this.Club == ClubType.Gaming)
			{
				this.MiyukiGameScreen.SetActive(true);
				this.ClubMemberID = this.StudentID - 35;

				if (this.StudentID > 36)
				{
					this.ClubAnim = this.GameAnim;
				}

				this.ActivityAnim = this.GameAnim;
			}
			else if (this.Club == ClubType.Art)
			{
				this.ChangingBooth = this.StudentManager.ChangingBooths[4];
				this.ActivityAnim = this.PaintAnim;
				this.Attacher.ArtClub = true;
				this.ClubAnim = this.PaintAnim;
				this.DressCode = true;

				if (DateGlobals.Weekday == System.DayOfWeek.Friday)
				{
					Debug.Log("It's Friday, so the art club is changing their club activity to painting the cherry tree.");

					// Change "Club" activity to painting.
					ScheduleBlock block7 = this.ScheduleBlocks[7];
					block7.destination = "Paint";
					block7.action = "Paint";
				}

				this.ClubMemberID = this.StudentID - 40;
				this.Painting = this.StudentManager.FridayPaintings[this.ClubMemberID];

				this.GetDestinations();
			}
			else if (this.Club == ClubType.LightMusic)
			{
				this.ClubMemberID = this.StudentID - 50;

				this.InstrumentBag[this.ClubMemberID].SetActive(true);

				if (this.StudentID == 51)
				{
					this.ClubAnim = "f02_practiceGuitar_01";
				}
				else if (this.StudentID == 52 || this.StudentID == 53)
				{
					this.ClubAnim = "f02_practiceGuitar_00";
				}
				else if (this.StudentID == 54)
				{
					this.ClubAnim = "f02_practiceDrums_00";

					this.Instruments[4] = this.StudentManager.DrumSet;
				}
				else if (this.StudentID == 55)
				{
					this.ClubAnim = "f02_practiceKeytar_00";
				}
			}
			else if (this.Club == ClubType.MartialArts)
			{
				this.ChangingBooth = this.StudentManager.ChangingBooths[6];
				this.DressCode = true;

				if (this.StudentID == 46)
				{
					this.IdleAnim = AnimNames.MalePose03;
					this.ClubAnim = AnimNames.MalePose03;
				}
				else if (this.StudentID == 47)
				{
					this.GetNewAnimation = true;
					this.ClubAnim = AnimNames.MaleIdle20;
					this.ActivityAnim = AnimNames.MaleKick24;
				}
				else if (this.StudentID == 48)
				{
					this.ClubAnim = AnimNames.MaleSit04;
					this.ActivityAnim = AnimNames.MaleKick24;
				}
				else if (this.StudentID == 49)
				{
					this.GetNewAnimation = true;
					this.ClubAnim = AnimNames.FemaleIdle20;
					this.ActivityAnim = AnimNames.FemaleKick23;
				}
				else if (this.StudentID == 50)
				{
					this.ClubAnim = AnimNames.FemaleSit05;
					this.ActivityAnim = AnimNames.FemaleKick23;
				}

				this.ClubMemberID = this.StudentID - 45;
			}
			else if (this.Club == ClubType.Science)
			{
				this.ChangingBooth = this.StudentManager.ChangingBooths[8];
				this.Attacher.ScienceClub = true;
				this.DressCode = true;

				if (this.StudentID == 61)
				{
					this.ClubAnim = "scienceMad_00";
				}
				else if (this.StudentID == 62)
				{
					this.ClubAnim = "scienceTablet_00";
				}
				else if (this.StudentID == 63)
				{
					this.ClubAnim = "scienceChemistry_00";
				}
				else if (this.StudentID == 64)
				{
					this.ClubAnim = "f02_scienceLeg_00";
				}
				else if (this.StudentID == 65)
				{
					this.ClubAnim = "f02_scienceConsole_00";
				}

				this.ClubMemberID = this.StudentID - 60;
			}
			else if (this.Club == ClubType.Sports)
			{
				this.ChangingBooth = this.StudentManager.ChangingBooths[9];
				this.DressCode = true;

				this.ClubAnim = "stretch_00";

				this.ClubMemberID = this.StudentID - 65;
			}
			else if (this.Club == ClubType.Gardening)
			{
				if (this.StudentID == 71)
				{
					this.PatrolAnim = "f02_thinking_00";
					this.ClubAnim = "f02_thinking_00";

					this.CharacterAnimation[this.PatrolAnim].speed = 0.9f;
				}
				else
				{
					this.ClubAnim = "f02_waterPlant_00";
					this.WateringCan.SetActive(true);
				}
			}

			if (this.OriginalClub != ClubType.Gaming)
			{
				this.Miyuki.gameObject.SetActive(false);
			}

			if (this.Cosmetic.Hairstyle == 20)
			{
				this.IdleAnim = AnimNames.FemaleTsunIdle;
			}

			this.GetDestinations();

			this.OriginalActions = new StudentActionType[this.Actions.Length];
			System.Array.Copy(this.Actions, this.OriginalActions, this.Actions.Length);

			if (this.AoT)
			{
				this.AttackOnTitan();
			}

			if (this.Slave)
			{
				this.NEStairs = GameObject.Find("NEStairs").GetComponent<Collider>();
				this.NWStairs = GameObject.Find("NWStairs").GetComponent<Collider>();
				this.SEStairs = GameObject.Find("SEStairs").GetComponent<Collider>();
				this.SWStairs = GameObject.Find("SWStairs").GetComponent<Collider>();

				this.IdleAnim = this.BrokenAnim;
				this.WalkAnim = this.BrokenWalkAnim;

				this.RightEmptyEye.SetActive(true);
				this.LeftEmptyEye.SetActive(true);
				this.SmartPhone.SetActive(false);

				this.Distracted = true;
				this.Indoors = true;
				this.Safe = false;
				this.Shy = false;

				// [af] Converted while loop to for loop.
				for (this.ID = 0; this.ID < this.ScheduleBlocks.Length; this.ID++)
				{
					this.ScheduleBlocks[this.ID].time = 0.0f;
				}

				if (this.FragileSlave)
				{
					this.HuntTarget = this.StudentManager.Students[StudentGlobals.FragileTarget];
				}
				else
				{
					SchoolGlobals.KidnapVictim = 0;
				}
			}

			if (this.Spooky)
			{
				this.Spook();
			}

			this.Prompt.HideButton[0] = true;
			this.Prompt.HideButton[2] = true;

			// [af] Converted while loop to for loop.
			for (this.ID = 0; this.ID < this.Ragdoll.AllRigidbodies.Length; this.ID++)
			{
				this.Ragdoll.AllRigidbodies[this.ID].isKinematic = true;
				this.Ragdoll.AllColliders[this.ID].enabled = false;
			}

			this.Ragdoll.AllColliders[10].enabled = true;

			if (this.StudentID == 1)
			{
				this.DetectionMarker.GetComponent<DetectionMarkerScript>().Tex.color =
					new Color(1.0f, 0.0f, 0.0f, 0.0f);

				this.Yandere.Senpai = this.transform;
				this.Yandere.LookAt.target = this.Head;

				// [af] Converted while loop to for loop.
				for (this.ID = 0; this.ID < this.Outlines.Length; this.ID++)
				{
					if (this.Outlines[this.ID] != null)
					{
						this.Outlines[this.ID].enabled = true;
						this.Outlines[this.ID].color = new Color(1.0f, 0.0f, 1.0f);
					}
				}

				this.Prompt.ButtonActive[0] = false;
				this.Prompt.ButtonActive[1] = false;
				this.Prompt.ButtonActive[2] = false;
				this.Prompt.ButtonActive[3] = false;

				if (this.StudentManager.MissionMode || GameGlobals.SenpaiMourning)
				{
					this.transform.position = Vector3.zero;
					this.gameObject.SetActive(false);
				}
			}
			else
			{
				if (!StudentGlobals.GetStudentPhotographed(this.StudentID))
				{
					// [af] Converted while loop to for loop.
					for (this.ID = 0; this.ID < this.Outlines.Length; this.ID++)
					{
						if (this.Outlines[this.ID] != null)
						{
							this.Outlines[this.ID].enabled = false;
							this.Outlines[this.ID].color = new Color(0.0f, 1.0f, 0.0f);
						}
					}
				}
			}

			this.PickRandomAnim();
			this.PickRandomSleuthAnim();

			Renderer ArmbandRenderer = this.Armband.GetComponent<Renderer>();
			this.Armband.SetActive(false);

			if (this.Club != 0)
			{
				if (this.StudentID == 21 || this.StudentID == 26 || this.StudentID == 31 ||
					this.StudentID == 36 || this.StudentID == 41 || this.StudentID == 46 ||
					this.StudentID == 51 || this.StudentID == 56 || this.StudentID == 61 ||
					this.StudentID == 66 || this.StudentID == 71)
				{
					this.Armband.SetActive(true);
					this.ClubLeader = true;

					if (GameGlobals.EmptyDemon)
					{
						this.IdleAnim = this.OriginalIdleAnim;
						this.WalkAnim = OriginalOriginalWalkAnim;

						this.OriginalPersona = PersonaType.Evil;
						this.Persona = PersonaType.Evil;

						this.ScaredAnim = this.EvilWitnessAnim;
					}
				}
			}

			if (!this.Teacher)
			{
				//This is only called during the Start function.

				this.CurrentDestination = this.Destinations[this.Phase];//StudentManager.EntranceVectors.List[StudentID];
				this.Pathfinding.target = this.Destinations[this.Phase];//StudentManager.EntranceVectors.List[StudentID];
			}
			else
			{
				this.Indoors = true;
			}

			//Senpai                   //Kuu Dere             //Horuda               //Osana
			if (this.StudentID == 1 || this.StudentID == 4 || this.StudentID == 5 || this.StudentID == 11)
			{
				this.BookRenderer.material.mainTexture = this.RedBookTexture;
			}

			this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;

			if (this.StudentManager.MissionMode)
			{
				if (this.StudentID == MissionModeGlobals.MissionTarget)
				{
					// [af] Converted while loop to for loop.
					for (this.ID = 0; this.ID < this.Outlines.Length; this.ID++)
					{
						this.Outlines[this.ID].enabled = true;
						this.Outlines[this.ID].color = new Color(1.0f, 0.0f, 0.0f);
					}
				}
			}

			// Code for testing rigidbody movement.
			/*
			if (this.UsingRigidbody)
			{
				this.MyController.enabled = false;
				this.MyCollider.enabled = true;
			}
			else
			{
				this.MyController.enabled = true;
				this.MyCollider.enabled = false;
			*/
			Destroy(this.MyRigidbody);
			//}

			this.Started = true;

			if (this.Club == ClubType.Council)
			{
				ArmbandRenderer.material.SetTextureOffset("_MainTex", new Vector2(-0.64375f, 0));
				this.Armband.SetActive(true);
				this.Indoors = true;
				this.Spawned = true;

				if (this.ShoeRemoval.Locker == null)
				{
					this.ShoeRemoval.Start();
				}

				this.ShoeRemoval.PutOnShoes();

					 if (this.StudentID == 86){this.Suffix = "Strict";}
				else if (this.StudentID == 87){this.Suffix = "Casual";}
				else if (this.StudentID == 88){this.Suffix = "Grace";}
				else if (this.StudentID == 89){this.Suffix = "Edgy";}

				this.IdleAnim = "f02_idleCouncil" + this.Suffix + "_00";
				this.WalkAnim = "f02_walkCouncil" + this.Suffix + "_00";
				this.ShoveAnim = "f02_pushCouncil" + this.Suffix + "_00";
				this.PatrolAnim = "f02_scanCouncil" + this.Suffix + "_00";
				this.RelaxAnim = "f02_relaxCouncil" + this.Suffix + "_00";
				this.SprayAnim = "f02_sprayCouncil" + this.Suffix + "_00";
				this.BreakUpAnim = "f02_stopCouncil" + this.Suffix + "_00";
				this.PickUpAnim = "f02_teacherPickUp_00";

				this.ScaredAnim = this.ReadyToFightAnim;
				this.ParanoidAnim = this.GuardAnim;

				this.CameraAnims[1] = this.IdleAnim;
				this.CameraAnims[2] = this.IdleAnim;
				this.CameraAnims[3] = this.IdleAnim;

				this.ClubMemberID = this.StudentID - 85;
				this.VisionDistance *= 2;
			}

			if (!this.StudentManager.NoClubMeeting)
			{
				if (this.Armband.activeInHierarchy)
				{
					if (Clock.Weekday == 5)
					{
						//Debug.Log("My StudentID is " + StudentID + " and I'm going to attend the club meeting.");

						if (this.StudentID < 86)
						{
							ScheduleBlock block6 = this.ScheduleBlocks[6];
							block6.destination = "Meeting";
							block6.action = "Meeting";
						}
						else
						{
							ScheduleBlock block5 = this.ScheduleBlocks[5];
							block5.destination = "Meeting";
							block5.action = "Meeting";
						}

						this.GetDestinations();
					}
				}
			}

			//Musume
			if (this.StudentID == 81)
			{
				if (StudentGlobals.GetStudentBroken(81))
				{
					this.Destinations[2] = this.StudentManager.BrokenSpot;
					this.Destinations[4] = this.StudentManager.BrokenSpot;
					this.Actions[2] = StudentActionType.Shamed;
					this.Actions[4] = StudentActionType.Shamed;
				}
			}
		}

		this.UpdateAnimLayers();

		if (!this.Male)
		{
            //this.GetComponent<FIMSpace.FLook.FLookAnimatorWEyes>().ObjectToFollow = this.Yandere.Head;
            //this.GetComponent<FIMSpace.FLook.FLookAnimatorWEyes>().EyesTarget = this.Yandere.Head;

            if (this.StudentID == 40)
            {
                this.LongHair[0] = this.LongHair[2];
            }

            if (this.StudentID != 55 && this.StudentID != 40)
			{
				this.LongHair[0] = null;
                this.LongHair[1] = null;
                this.LongHair[2] = null;
            }
		}

#if !UNITY_EDITOR

		//Osana or Obstacle
		if (this.StudentID > 9 && this.StudentID < 21)
		{
			Debug.Log("Destroying a character who should not exist.");
			Destroy(this.gameObject);
		}

#endif

        //Must be the final line of code in the Start() function
        this.CharacterAnimation.Sample();
    }

	// [af] A student's perception is a percentage determining how quickly they can recognize 
	// something at a given distance. For example, when something is close, their perception is 
	// high, and they can "evaluate" it much faster. When it's farther away, then it takes them 
	// longer to figure out what it is.
	float GetPerceptionPercent(float distance)
	{
		float distancePercent = Mathf.Clamp01(distance / this.VisionDistance);
		return 1.0f - (distancePercent * distancePercent);
	}

	SubtitleType LostPhoneSubtitleType
	{
		get
		{
			if (this.RivalPrefix == string.Empty)
			{
				return SubtitleType.LostPhone;
			}
			else if (this.RivalPrefix == RIVAL_PREFIX)
			{
				return SubtitleType.RivalLostPhone;
			}
			else
			{
				throw new System.NotImplementedException(
					"\"" + this.RivalPrefix + "\" case not implemented.");
			}
		}
	}

	SubtitleType PickpocketSubtitleType
	{
		get
		{
			if (this.RivalPrefix == string.Empty)
			{
				return SubtitleType.PickpocketReaction;
			}
			else if (this.RivalPrefix == RIVAL_PREFIX)
			{
				return SubtitleType.RivalPickpocketReaction;
			}
			else
			{
				throw new System.NotImplementedException(
					"\"" + this.RivalPrefix + "\" case not implemented.");
			}
		}
	}

	SubtitleType SplashSubtitleType
	{
		get
		{
			if (this.RivalPrefix == string.Empty)
			{
				if (!this.Male)
				{
					return SubtitleType.SplashReaction;
				}
				else
				{
					return SubtitleType.SplashReactionMale;
				}
			}
			else if (this.RivalPrefix == RIVAL_PREFIX)
			{
				return SubtitleType.RivalSplashReaction;
			}
			else
			{
				throw new System.NotImplementedException(
					"\"" + this.RivalPrefix + "\" case not implemented.");
			}
		}
	}

	public SubtitleType TaskLineResponseType
	{
		get
		{
			Debug.Log("Student#" + this.StudentID + " has been asked to display a subtitle about their task.");

			if (this.StudentID == 6)
			{
				bool Real = false;

				#if UNITY_EDITOR
				Real = true;
				#endif

				if (!Real)
				{
					return SubtitleType.TaskGenericLine;
				}
				else
				{
					return SubtitleType.Task6Line;
				}
			}
			/*
			else if (this.StudentID == 7)
			{
				return SubtitleType.Task7Line;
			}*/
			else if (this.StudentID == 8)
			{
				return SubtitleType.Task8Line;
			}
			else if (this.StudentID == 11)
			{
				return SubtitleType.Task11Line;
			}
			else if (this.StudentID == 13)
			{
				return SubtitleType.Task13Line;
			}
			else if (this.StudentID == 14)
			{
				return SubtitleType.Task14Line;
			}
			else if (this.StudentID == 15)
			{
				return SubtitleType.Task15Line;
			}
			else if (this.StudentID == 25)
			{
				return SubtitleType.Task25Line;
			}
			else if (this.StudentID == 28)
			{
				return SubtitleType.Task28Line;
			}
			else if (this.StudentID == 30)
			{
				return SubtitleType.Task30Line;
			}
			/*
			else if (this.StudentID == 33)
			{
				return SubtitleType.Task33Line;
			}
			else if (this.StudentID == 34)
			{
				return SubtitleType.Task34Line;
			}
			*/
			else if (this.StudentID == 36)
			{
				return SubtitleType.Task36Line;
			}
			else if (this.StudentID == 37)
			{
				return SubtitleType.Task37Line;
			}
			else if (this.StudentID == 38)
			{
				return SubtitleType.Task38Line;
			}
			else if (this.StudentID == 52)
			{
				return SubtitleType.Task52Line;
			}
			else if (this.StudentID == 76)
			{
				return SubtitleType.Task76Line;
			}
			else if (this.StudentID == 77)
			{
				return SubtitleType.Task77Line;
			}
			else if (this.StudentID == 78)
			{
				return SubtitleType.Task78Line;
			}
			else if (this.StudentID == 79)
			{
				return SubtitleType.Task79Line;
			}
			else if (this.StudentID == 80)
			{
				return SubtitleType.Task80Line;
			}
			else if (this.StudentID == 81)
			{
				return SubtitleType.Task81Line;
			}
			else
			{
				return SubtitleType.TaskGenericLine;
			}

			/*
			else
			{
				throw new System.NotImplementedException(
					"\"" + this.StudentID.ToString() + "\" case not implemented.");
			}
			*/
		}
	}

	public SubtitleType ClubInfoResponseType
	{
		get
		{
			if (GameGlobals.EmptyDemon)
			{
				return SubtitleType.ClubPlaceholderInfo;
			}
			else if (this.Club == ClubType.Cooking)
			{
				return SubtitleType.ClubCookingInfo;
			}
			else if (this.Club == ClubType.Drama)
			{
				return SubtitleType.ClubDramaInfo;
			}
			else if (this.Club == ClubType.Occult)
			{
				return SubtitleType.ClubOccultInfo;
			}
			else if (this.Club == ClubType.Art)
			{
				return SubtitleType.ClubArtInfo;
			}
			else if (this.Club == ClubType.LightMusic)
			{
				return SubtitleType.ClubLightMusicInfo;
			}
			else if (this.Club == ClubType.MartialArts)
			{
				return SubtitleType.ClubMartialArtsInfo;
			}
			else if (this.Club == ClubType.Photography)
			{
				if (this.Sleuthing)
				{
					return SubtitleType.ClubPhotoInfoDark;
				}
				else
				{
					return SubtitleType.ClubPhotoInfoLight;
				}
			}
			else if (this.Club == ClubType.Science)
			{
				return SubtitleType.ClubScienceInfo;
			}
			else if (this.Club == ClubType.Sports)
			{
				return SubtitleType.ClubSportsInfo;
			}
			else if (this.Club == ClubType.Gardening)
			{
				return SubtitleType.ClubGardenInfo;
			}
			else if (this.Club == ClubType.Gaming)
			{
				return SubtitleType.ClubGamingInfo;
			}
			else if (this.Club == ClubType.Delinquent)
			{
				return SubtitleType.ClubDelinquentInfo;
			}
			else
			{
				return SubtitleType.ClubPlaceholderInfo;

				/*
				throw new System.NotImplementedException(
					"\"" + this.Club.ToString() + "\" case not implemented.");
				*/
			}
		}
	}

	// [af] Returns whether the given point is currently in the student's field of view
	// (ignoring any occlusion -- this only checks against the vision cone).
	bool PointIsInFOV(Vector3 point)
	{
		Vector3 eyePoint = this.Eyes.transform.position;
		Vector3 diff = point - eyePoint;

		// The field of view is cut in half since it's measuring from the center of the 
		// student's vision, not the entire span of their vision cone.
		float maxAngle = 90;//this.VisionFOV * 0.5f;

		//Debug.Log("maxAngle is "+ maxAngle + ", and current angle is " + Vector3.Angle(this.Head.transform.forward, diff));

		return Vector3.Angle(this.Head.transform.forward, diff) <= maxAngle;
	}
		
	public bool SeenByYandere()
	{
		Debug.Log ("A ''SeenByYandere'' check is occuring.");

		Debug.DrawLine(Yandere.transform.position + new Vector3(0, 1, 0), transform.position + new Vector3(0, 1, 0), Color.red);

		RaycastHit hit;
		bool hitExists = Physics.Linecast(Yandere.transform.position + new Vector3(0, 1, 0), transform.position + new Vector3(0, 1, 0), out hit, this.YandereCheckMask);

		if (hitExists)
		{
			Debug.Log("Yandere-chan's raycast hit: " + hit.collider.gameObject.name);

            if (hit.collider.gameObject == Head.gameObject ||
                hit.collider.gameObject == gameObject)
            {
                // Yandere-chan can see the student.
                return true;
            }
		}

		// The student can't see the target.
		return false;
	}

	// [af] Object visibility function specialized for checking specific collider layers.
	// This does not compare game object references.
	public bool CanSeeObject(GameObject obj, Vector3 targetPoint, int[] layers, int mask)
	{
		//Debug.Log(this.Name + " is currently looking for something.");

		Vector3 startPoint = this.Eyes.transform.position;
		Vector3 diff = targetPoint - startPoint;

		// Check if they're in the student's field of view and are close enough.
		bool inFieldOfView = this.PointIsInFOV(targetPoint);

		if (inFieldOfView)
		{
			float maxDistanceSqr = this.VisionDistance * this.VisionDistance;
			bool closeEnough = diff.sqrMagnitude <= maxDistanceSqr;

			if (closeEnough)
			{
				Debug.DrawLine(startPoint, targetPoint, Color.green);

				// Check if there are any occluders blocking the view.
				RaycastHit hit;
				bool hitExists = Physics.Linecast(startPoint, targetPoint, out hit, mask);

				if (hitExists)
				{
					//Debug.Log ("The ray hit: " + hit.collider.gameObject.name);

					// Check if the intersected object is of the correct layer.
					foreach (int layer in layers)
					{
						bool objectHasLayer = hit.collider.gameObject.layer == layer;

						if (objectHasLayer)
						{
							// The student can see the target.
							return true;
						}
					}
				}
			}
		}

		// The student can't see the target.
		return false;
	}

	// [af] Returns whether the given game object can currently be seen by the student at the
	// target point (i.e., head, torso, etc.). The game object must have a collider attached.
	public bool CanSeeObject(GameObject obj, Vector3 targetPoint)
	{
		//This is the code that is used when checking to see if Yandere-chan is doing something suspicious.
		//Debug.Log(this.Name + " is currently looking for something.");

		if (!this.Blind)
		{
			Debug.DrawLine(this.Eyes.position, targetPoint, Color.green);

			Vector3 startPoint = this.Eyes.transform.position;
			Vector3 diff = targetPoint - startPoint;
			float maxDistanceSqr = this.VisionDistance * this.VisionDistance;

			// Check if they're in the student's field of view and are close enough.
			bool inFieldOfView = this.PointIsInFOV(targetPoint);
			bool closeEnough = diff.sqrMagnitude <= maxDistanceSqr;

			//Debug.Log("inFieldOfView is " + inFieldOfView);
			//Debug.Log("closeEnough is " + closeEnough);

			if (inFieldOfView && closeEnough)
			{
				// Check if there are any occluders blocking the view.
				RaycastHit hit;
				bool hitExists = Physics.Linecast(startPoint, targetPoint, out hit, this.Mask);

				if (hitExists)
				{
                    #if UNITY_EDITOR
                    //Debug.Log(this.Name + "'s raycast hit: " + hit.collider.gameObject.name);
                    #endif

                    // Check if the intersected object is the target.
                    bool objectIsTarget = hit.collider.gameObject == obj;

					if (objectIsTarget)
					{
						// The student can see the target.
						return true;
					}
				}
			}
		}

		// The student can't see the target.
		return false;
	}

	// [af] Short-hand version of CanSeeObject() that uses the game object's position
	// (instead of some custom position on the game object).
	public bool CanSeeObject(GameObject obj)
	{
		//Debug.Log(this.Name + " is currently looking for something.");

		return this.CanSeeObject(obj, obj.transform.position);
	}

	void Update()
	{
		if (!this.Stop)
		{
			//this.DistanceToPlayer = this.Prompt.DistanceSqr;
			this.DistanceToPlayer = Vector3.Distance(this.transform.position, this.Yandere.transform.position);
				
			this.UpdateRoutine();

			this.UpdateDetectionMarker();

			if (this.Dying)
			{
				this.UpdateDying();

				if (this.Burning)
				{
					this.UpdateBurning();
				}
			}
			else
			{
				if (this.DistanceToPlayer <= 2)
				{
					this.UpdateTalkInput();
				}

				this.UpdateVision();

				if (this.Pushed)
				{
					this.UpdatePushed();
				}
				else if (this.Drowned)
				{
					this.UpdateDrowned();
				}
				else if (this.WitnessedMurder)
				{
					this.UpdateWitnessedMurder();
				}
				else if (this.Alarmed)
				{
					this.UpdateAlarmed();
				}
				else if (this.TurnOffRadio)
				{
					this.UpdateTurningOffRadio();
				}
				else if (this.Confessing)
				{
					this.UpdateConfessing();
				}
				else if (this.Vomiting)
				{
					this.UpdateVomiting();
				}
				else if (this.Splashed)
				{
					this.UpdateSplashed();
				}
			}

			this.UpdateMisc();
		}
		//If "Stop" is true...
		else
		{
			this.UpdateStop();
		}
	}

	void UpdateStop()
	{
		//If "Pose Mode" is active...
		if (this.StudentManager.Pose)
		{
			this.DistanceToPlayer = Vector3.Distance(this.transform.position, this.Yandere.transform.position);

			if (this.Prompt.Circle[0].fillAmount == 0.0f)
			{
				this.Pose();
			}
		}
		//If "Pose Mode" is NOT active...
		else
		{
			//If we are not currently doing a club activity...
			if (!this.ClubActivity)
			{
				//If Yandere-chan is not currently talking...
				if (!this.Yandere.Talking)
				{
					if (this.Yandere.Sprayed)
					{
						this.CharacterAnimation.CrossFade(this.ScaredAnim);
					}

					//If Yandere-chan was just caught by Senpai or a teacher...
					if (this.Yandere.Noticed || this.StudentManager.YandereDying)
					{
						this.targetRotation = Quaternion.LookRotation(new Vector3(
							this.Yandere.Hips.transform.position.x,
							this.transform.position.y,
							this.Yandere.Hips.transform.position.z)
								- this.transform.position);

						this.transform.rotation = Quaternion.Slerp(
							this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);

						if (Vector3.Distance(this.transform.position, this.Yandere.transform.position) < 1)
						{				
							this.MyController.Move(this.transform.forward * (Time.deltaTime * -1));
						}
					}
				}
			}
			//If the day has ended and we are currently doing a club activity...
			else
			{
				if (this.Police.Darkness.color.a < 1.0f)
				{
					if (this.Club == ClubType.Cooking)
					{
						this.CharacterAnimation[this.SocialSitAnim].layer = 99;
						this.CharacterAnimation.Play(this.SocialSitAnim);
						this.CharacterAnimation[this.SocialSitAnim].weight = 1.0f;

						this.SmartPhone.SetActive(false);
						this.SpeechLines.Play();

						this.CharacterAnimation.CrossFade(this.RandomAnim);

						if (this.CharacterAnimation[this.RandomAnim].time >=
							this.CharacterAnimation[this.RandomAnim].length)
						{
							this.PickRandomAnim();
						}
					}
					else if (this.Club == ClubType.MartialArts)
					{
						this.CharacterAnimation.Play(this.ActivityAnim);

						AudioSource audioSource = this.GetComponent<AudioSource>();

						if (!this.Male)
						{
							if (this.CharacterAnimation[AnimNames.FemaleKick23].time > 1.0f)
							{
								if (!this.PlayingAudio)
								{
									audioSource.clip = this.FemaleAttacks[Random.Range(0, this.FemaleAttacks.Length)];
									audioSource.Play();
									this.PlayingAudio = true;
								}

								if (this.CharacterAnimation[AnimNames.FemaleKick23].time >=
									this.CharacterAnimation[AnimNames.FemaleKick23].length)
								{
									this.CharacterAnimation[AnimNames.FemaleKick23].time = 0.0f;
									this.PlayingAudio = false;
								}
							}
						}
						else
						{
							if (this.CharacterAnimation[AnimNames.MaleKick24].time > 1.0f)
							{
								if (!this.PlayingAudio)
								{
									audioSource.clip = this.MaleAttacks[Random.Range(0, this.MaleAttacks.Length)];
									audioSource.Play();
									this.PlayingAudio = true;
								}

								if (this.CharacterAnimation[AnimNames.MaleKick24].time >=
									this.CharacterAnimation[AnimNames.MaleKick24].length)
								{
									this.CharacterAnimation[AnimNames.MaleKick24].time = 0.0f;
									this.PlayingAudio = false;
								}
							}
						}
					}
					else if (this.Club == ClubType.Drama)
					{
						this.Stop = false;
					}
					else if (this.Club == ClubType.Photography)
					{
						this.CharacterAnimation.CrossFade(this.RandomSleuthAnim);

						if (this.CharacterAnimation[this.RandomSleuthAnim].time >=
							this.CharacterAnimation[this.RandomSleuthAnim].length)
						{
							this.PickRandomSleuthAnim();
						}
					}
					else if (this.Club == ClubType.Art)
					{
						this.CharacterAnimation.Play(this.ActivityAnim);
						this.Paintbrush.SetActive(true);
						this.Palette.SetActive(true);
					}
					else if (this.Club == ClubType.Science)
					{
						this.CharacterAnimation.Play(this.ClubAnim);

						//Hologram Guy
						if (this.StudentID == 62)
						{
							ScienceProps[0].SetActive(true);
						}
						//Chemistry Guy
						else if (this.StudentID == 63)
						{
							ScienceProps[1].SetActive(true);
							ScienceProps[2].SetActive(true);
						}
						//Mecha Girl
						else if (this.StudentID == 64)
						{
							ScienceProps[0].SetActive(true);
						}
					}
					else if (this.Club == ClubType.Sports)
					{
						this.Stop = false;
					}
					else if (this.Club == ClubType.Gardening)
					{
						this.CharacterAnimation.Play(this.ClubAnim);
						this.Stop = false;
					}
					else if (this.Club == ClubType.Gaming)
					{
						this.CharacterAnimation.CrossFade(this.ClubAnim);
					}
				}
			}
		}

		this.Alarm = Mathf.MoveTowards(this.Alarm, 0, Time.deltaTime);
		this.UpdateDetectionMarker();
	}

	void UpdateRoutine()
	{
		if (this.Routine)
		{
			if (this.CurrentDestination != null)
			{
				this.DistanceToDestination = Vector3.Distance(
					this.transform.position, this.CurrentDestination.position);
			}

			if (this.StalkerFleeing)
			{
				if (this.Actions[this.Phase] == StudentActionType.Stalk)
				{
					if (this.DistanceToPlayer > 10)
					{
						this.Pathfinding.target = this.Yandere.transform;
						this.CurrentDestination = this.Yandere.transform;
						this.StalkerFleeing = false;
						this.Pathfinding.speed = 1;
					}
				}
			}
			else
			{
				if (this.Actions[this.Phase] == StudentActionType.Stalk)
				{
					this.TargetDistance = 10;

					if (this.DistanceToPlayer > 20)
					{
						//Debug.Log("Sprinting 1");
						this.Pathfinding.speed = 4;
					}
					else if (this.DistanceToPlayer < 10)
					{
						this.Pathfinding.speed = 1;
					}
				}
			}

            if (!this.Indoors)
			{
				// Special-case code for the clumsy student who
				// trips while entering the school.
				if (this.Hurry)
				{
					if (!Tripped)
					{
						if (this.transform.position.z > -50.5f)
						{
							if (this.transform.position.x < 6)
							{
                                //Debug.Log("He's supposed to trip.");

                                this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
                                this.CharacterAnimation.CrossFade("trip_00");
								this.Pathfinding.canSearch = false;
								this.Pathfinding.canMove = false;
								this.Tripping = true;
								this.Routine = false;
							}
						}
					}
				}

				if (this.Paired)
				{
					if (this.transform.position.z < -50)
					{
						if (this.transform.localEulerAngles != Vector3.zero)
						{
							this.transform.localEulerAngles = Vector3.zero;
						}

						MyController.Move(this.transform.forward * Time.deltaTime);

						if (this.StudentID == 81)
						{
							this.MusumeTimer += Time.deltaTime;

							if (this.MusumeTimer > 5)
							{
								this.MusumeTimer = 0;
								this.MusumeRight = !this.MusumeRight;

								this.WalkAnim = this.MusumeRight ? "f02_walkTalk_00" : "f02_walkTalk_01";
							}
						}
					}
					else
					{
						if (this.Persona == PersonaType.PhoneAddict)
						{
							this.SpeechLines.Stop();
							this.SmartPhone.SetActive(true);
						}
						
						this.Pathfinding.canSearch = true;
						this.Pathfinding.canMove = true;
						this.StopPairing();
					}
				}

				if (this.Phase == 0)
				{
                    if (this.DistanceToDestination < 1.0f)
					{
						this.CurrentDestination = this.MyLocker;
						this.Pathfinding.target = this.MyLocker;
						this.Phase++;
					}
				}
				else
				{
					if (this.DistanceToDestination < 0.50f)
					{
                        if (!this.ShoeRemoval.enabled)
						{
                            if (this.Shy)
							{
								this.CharacterAnimation[this.ShyAnim].weight = 0.50f;
							}

							this.SmartPhone.SetActive(false);

							this.Pathfinding.canSearch = false;
							this.Pathfinding.canMove = false;

							this.ShoeRemoval.StartChangingShoes();

							this.ShoeRemoval.enabled = true;
                            this.ChangingShoes = true;
							this.CanTalk = false;
							this.Routine = false;
						}
					}
				}
			}
			else
			{
				if (this.Phase < (this.ScheduleBlocks.Length - 1))
				{
					if (this.Clock.HourTime >= this.ScheduleBlocks[this.Phase].time)
					{
						if (!this.InEvent && !this.Meeting && this.ClubActivityPhase < 16)
						{
							this.MyRenderer.updateWhenOffscreen = false;

							this.SprintAnim = this.OriginalSprintAnim;

							if (this.Headache)
							{
								this.SprintAnim = this.OriginalSprintAnim;
								this.WalkAnim = this.OriginalWalkAnim;
							}

                            this.Pushable = false;
							this.Headache = false;
							this.Sedated = false;
							this.Hurry = false;
							this.Phase++;

							if (this.Drownable)
							{
								this.Drownable = false;
								this.StudentManager.UpdateMe(this.StudentID);
							}

							//Senpai
							if (this.StudentID == 1)
							{
								//Debug.Log("Senpai's phase has increased to " + this.Phase + ".");
							}

							//Osana
							if (this.StudentID == 11)
							{
								//Debug.Log("Osana's phase has increased to " + this.Phase + ".");
							}

							if ((this.Schoolwear > 1) &&
								(this.Destinations[this.Phase] == this.MyLocker))
							{
								this.Phase++;
							}

                            if (this.Actions[this.Phase] == StudentActionType.SitAndTakeNotes && this.Schoolwear == 2)
                            {
                                this.MustChangeClothing = true;
                                this.ChangeClothingPhase = 0;
                            }

                            if (this.Actions[this.Phase] == StudentActionType.Graffiti)
							{
								if (!this.StudentManager.Bully)
								{
									ScheduleBlock newBlock2 = this.ScheduleBlocks[2];
									newBlock2.destination = "Patrol";
									newBlock2.action = "Patrol";

									this.GetDestinations();
								}
							}

							if (!this.StudentManager.ReactedToGameLeader)
							{
								if (this.Actions[this.Phase] == StudentActionType.Bully)
								{
									if (!this.StudentManager.Bully)
									{
										ScheduleBlock newBlock4 = this.ScheduleBlocks[4];
										newBlock4.destination = "Sunbathe";
										newBlock4.action = "Sunbathe";

										this.GetDestinations();
									}
								}
							}

							if (this.Sedated)
							{
								this.SprintAnim = this.OriginalSprintAnim;
								this.Sedated = false;
							}

							this.CurrentAction = this.Actions[this.Phase];

							//This is only called when "Phase" is incremented.

							this.CurrentDestination = this.Destinations[this.Phase];
							this.Pathfinding.target = this.Destinations[this.Phase];

							if (this.StudentID == this.StudentManager.RivalID && 
								DateGlobals.Weekday == System.DayOfWeek.Friday &&
								!this.InCouple)
							{
								if (this.StudentID == this.StudentManager.RivalID && DateGlobals.Weekday == System.DayOfWeek.Friday)
								{
									Debug.Log("This is the part where Osana decides to put a note in someone's locker.");
                                    CharacterAnimation.CrossFade(this.WalkAnim);
								}

								if (this.Actions[this.Phase] == StudentActionType.ChangeShoes)
								{
									//Checking if rival should confess to suitor
									if (this.StudentManager.LoveManager.ConfessToSuitor)
									{
										this.CurrentDestination = this.StudentManager.SuitorLocker;
										this.Pathfinding.target = this.StudentManager.SuitorLocker;
									}
									else
									{
										this.CurrentDestination = this.StudentManager.SenpaiLocker;
										this.Pathfinding.target = this.StudentManager.SenpaiLocker;
									}

									this.Confessing = true;
                                    this.Distracted = true;
                                    this.Routine = false;
									this.CanTalk = false;
								}
							}

							if (this.CurrentDestination != null)
							{
								this.DistanceToDestination = Vector3.Distance(
									this.transform.position, this.CurrentDestination.position);
							}

							if (this.Bento != null)
							{
								if (this.Bento.activeInHierarchy)
								{
									this.Bento.SetActive(false);

									this.Chopsticks[0].SetActive(false);
									this.Chopsticks[1].SetActive(false);
								}
							}

							if (this.Male)
							{
								this.Cosmetic.MyRenderer.materials[this.Cosmetic.FaceID].SetFloat("_BlendAmount", 0.0f);
								this.PinkSeifuku.SetActive(false);
							}
							else
							{
								this.HorudaCollider.gameObject.SetActive(false);
							}

							if (this.StudentID == 81)
							{
								this.Cigarette.SetActive(false);
								this.Lighter.SetActive(false);
							}

							if (this.Paired == false)
							{
								this.Pathfinding.canSearch = true;
								this.Pathfinding.canMove = true;
							}
								
							if (this.Persona != PersonaType.PhoneAddict && !this.Sleuthing)
							{
								this.SmartPhone.SetActive(false);
							}
							else if (!this.Slave && !this.Phoneless)
							{
								this.IdleAnim = this.PhoneAnims[0];
								this.SmartPhone.SetActive(true);
							}

							this.Paintbrush.SetActive(false);
							this.Sketchbook.SetActive(false);
							this.Palette.SetActive(false);
							this.Pencil.SetActive(false);

							this.OccultBook.SetActive(false);
							this.Scrubber.SetActive(false);
							this.Eraser.SetActive(false);
							this.Pen.SetActive(false);

							foreach (GameObject prop in this.ScienceProps)
							{
								if (prop != null)
								{
									prop.SetActive(false);
								}
							}

							foreach (GameObject prop in this.Fingerfood)
							{
								if (prop != null)
								{
									prop.SetActive(false);
								}
							}

							this.SpeechLines.Stop();
							this.GoAway = false;
							this.ReadPhase = 0;

                            if (!this.ReturningFromSave)
                            {
                                Debug.Log("This student believes we are NOT returning from a save, so PatrolID has been set to 0.");

							    this.PatrolID = 0;
                            }
                            
                            if (this.Phase == this.PhaseFromSave)
                            {
                                Debug.Log("This student has returned to the phase they were at when the game was saved.");

                                if (this.Destinations[this.Phase] == this.StudentManager.Patrols.List[this.StudentID].GetChild(0))
                                {
                                    this.Destinations[this.Phase] = this.StudentManager.Patrols.List[this.StudentID].GetChild(this.PatrolID);

                                    this.CurrentDestination = this.StudentManager.Patrols.List[this.StudentID].GetChild(this.PatrolID);
                                    this.Pathfinding.target = this.CurrentDestination;

                                    Debug.Log("Student's CurrentDestination location is: " + this.CurrentDestination.position);
                                }

                                this.ReturningFromSave = false;
                            }

                            if (this.Actions[this.Phase] == StudentActionType.Clean)
							{
								//Debug.Log("Equipping cleaning items.");

								this.EquipCleaningItems();
							}
							else
							{
								if (!this.Slave)
								{
									if (!this.Phoneless)
									{
										if (this.Persona == PersonaType.PhoneAddict)
										{
											this.SmartPhone.transform.localPosition = new Vector3(0.01f, 0.005f, 0.01f);
											this.SmartPhone.transform.localEulerAngles = new Vector3(0, -160, 165);
											this.WalkAnim = this.PhoneAnims[1];

											//Debug.Log("WalkAnim has reverted back to PhoneAnims[1].");
										}
										else if (this.Sleuthing)
										{
											this.WalkAnim = this.SleuthWalkAnim;
										}
									}
								}
							}

							if (this.Actions[this.Phase] == StudentActionType.Sleuth && this.StudentManager.SleuthPhase == 3)
							{
								this.GetSleuthTarget();
							}

							if (this.Actions[this.Phase] == StudentActionType.Stalk)
							{
								this.TargetDistance = 10;
							}
							else if (this.Actions[this.Phase] == StudentActionType.Follow)
							{
								//Debug.Log ("Raibaru-specific code.");

								this.TargetDistance = 1;
							}
							else if (this.Actions[this.Phase] != StudentActionType.SitAndEatBento)
							{
								this.TargetDistance = .5f;
							}

							if (this.Club == ClubType.Gardening && this.StudentID != 71)
							{
								this.WateringCan.transform.parent = this.Hips;
								this.WateringCan.transform.localPosition = new Vector3(0, .0135f, -.184f);
								this.WateringCan.transform.localEulerAngles = new Vector3(0, 90, 30);

								if (this.Clock.Period == 6)
								{
									this.StudentManager.Patrols.List[this.StudentID] = this.StudentManager.GardeningPatrols[this.StudentID - 71];
									this.ClubAnim = "f02_waterPlantLow_00";

									this.CurrentDestination = this.StudentManager.Patrols.List[this.StudentID].GetChild(this.PatrolID);
									this.Pathfinding.target = this.CurrentDestination;
								}
							}

							if (this.Club == ClubType.LightMusic)
							{
								if (this.StudentID == 51)
								{
									if (this.InstrumentBag[this.ClubMemberID].transform.parent == null)
									{
										this.Instruments[this.ClubMemberID].GetComponent<AudioSource>().playOnAwake = false;
										this.Instruments[this.ClubMemberID].GetComponent<AudioSource>().Stop();

										this.Instruments[this.ClubMemberID].transform.parent = null;
										this.Instruments[this.ClubMemberID].transform.position = new Vector3(-.5f, 4.5f, 22.45666f);
										this.Instruments[this.ClubMemberID].transform.eulerAngles = new Vector3(-15, 0, 0);
									}
									else
									{
										this.Instruments[this.ClubMemberID].SetActive(false);
									}
								}
								else
								{
									this.Instruments[this.ClubMemberID].SetActive(false);
								}

								this.Drumsticks[0].SetActive(false);
								this.Drumsticks[1].SetActive(false);

								this.AirGuitar.Stop();
							}

							if (this.Phase == 8 && this.StudentID == 36)
							{
								this.StudentManager.Clubs.List[this.StudentID].position = this.StudentManager.Clubs.List[71].position;
								this.StudentManager.Clubs.List[this.StudentID].rotation = this.StudentManager.Clubs.List[71].rotation;
								this.ClubAnim = this.GameAnim;
							}

							if (this.MyPlate != null)
							{
								if (this.MyPlate.parent == this.RightHand)
								{
									//Debug.Log("We need to put this plate away...");

									this.CurrentDestination = this.StudentManager.Clubs.List[this.StudentID];
									this.Pathfinding.target = this.StudentManager.Clubs.List[this.StudentID];
								}
							}

							if (this.Persona == PersonaType.Sleuth)
							{
								if (this.Male) {this.SmartPhone.transform.localPosition = new Vector3(0.06f, -0.02f, -0.02f);}
								else           {this.SmartPhone.transform.localPosition = new Vector3(0.033333f, -0.015f, -0.015f);}

								this.SmartPhone.transform.localEulerAngles = new Vector3(12.5f, 120, 180);
							}

                            //Swimming club special case
                            if (this.Character.transform.localPosition.y == -.25f)
                            {
                                Debug.Log("Swimming club special case was reached.");

                                this.Destinations[Phase] = this.StudentManager.Clubs.List[ID].GetChild(this.ClubActivityPhase - 2);

                                this.CurrentDestination = this.StudentManager.Clubs.List[ID].GetChild(this.ClubActivityPhase - 2);
                                this.Pathfinding.target = this.StudentManager.Clubs.List[ID].GetChild(this.ClubActivityPhase - 2);
                            }
						}
					}

					if (!this.Teacher && this.Club != ClubType.Delinquent && this.Club != ClubType.Sports)
					{
						if (this.Clock.Period == 2 || this.Clock.Period == 4)
						{
							if (this.ClubActivityPhase < 16)
							{
								//Debug.Log("Sprinting 2");
								this.Pathfinding.speed = 4.0f;
							}
						}
                        else
                        {
                            this.Pathfinding.speed = 1.0f;
                        }
					}
				}
			}

			if (this.MeetTime > 0.0f)
			{
				if (this.Clock.HourTime > this.MeetTime)
				{
					//Debug.Log("Sprinting 3");

					this.CurrentDestination = this.MeetSpot;
					this.Pathfinding.target = this.MeetSpot;

					this.DistanceToDestination = Vector3.Distance(
						this.transform.position, this.CurrentDestination.position);

					this.Pathfinding.canSearch = true;
					this.Pathfinding.canMove = true;
					this.Pathfinding.speed = 4.0f;

					this.SpeechLines.Stop();

					this.EmptyHands();

					this.Meeting = true;
					this.MeetTime = 0.0f;
				}
			}

			//If we have not yet reached our destination...
			if (this.DistanceToDestination > this.TargetDistance)
			{
				if (this.CurrentDestination == null)
				{
					if (this.Actions[this.Phase] == StudentActionType.Sleuth)
					{
						this.GetSleuthTarget();
					}
				}

                //this.Obstacle.enabled = true;

				if (this.Actions[this.Phase] == StudentActionType.Follow)
				{
					//Debug.Log ("Raibaru-specific code.");

					this.Obstacle.enabled = false;
					this.MyController.radius = 0;

					if (this.FollowTarget.Wet && this.FollowTarget.DistanceToDestination < 5)
					{
						//Debug.Log("Raibaru shouldn't get too close.");

						this.TargetDistance = 4;
					}
					else
					{
						this.TargetDistance = 1;
					}

					if (this.DistanceToDestination > 2 && !this.Calm)
					{
						//Debug.Log("Setting speed to 5 here.");

						this.Pathfinding.speed = 5;
						this.SpeechLines.Stop();
					}
					else
					{
						this.Pathfinding.speed = 1;
						this.SpeechLines.Stop();
					}
				}

				if (this.CuriosityPhase == 1)
				{
					if (this.Actions[this.Phase] == StudentActionType.Relax)
					{
						if (this.StudentManager.Students[this.Crush].Private)
						{
							this.Pathfinding.target = this.Destinations[this.Phase];
							this.CurrentDestination = this.Destinations[this.Phase];
							this.TargetDistance = .5f;
							this.CuriosityTimer = 0;
							this.CuriosityPhase--;
						}
					}
				}

				if (this.Actions[this.Phase] != StudentActionType.Follow ||
					this.Actions[this.Phase] == StudentActionType.Follow &&
					this.DistanceToDestination > this.TargetDistance + .1f)
				{
					//Debug.Log ("Raibaru-specific code.");

					if (this.Clock.Period == 1 && this.Clock.HourTime > 8.4833333f ||
						this.Clock.Period == 3 && this.Clock.HourTime > 13.4833333f)
					{
						if (!this.Teacher)
						{
							//Debug.Log("Sprinting 4");
							this.Pathfinding.speed = 4.0f;
						}
					}

					if (!this.InEvent && !this.Meeting && !this.GoAway)
					{
						if (this.DressCode)
						{
							if (this.Actions[this.Phase] == StudentActionType.ClubAction)
							{
								if (!this.ClubAttire)
								{
									if (!this.ChangingBooth.Occupied)
									{
										this.CurrentDestination = this.ChangingBooth.transform;
										this.Pathfinding.target = this.ChangingBooth.transform;
									}
									else
									{
										this.CurrentDestination = this.ChangingBooth.WaitSpots[this.ClubMemberID];
										this.Pathfinding.target = this.ChangingBooth.WaitSpots[this.ClubMemberID];
									}
								}
								else
								{
									if (this.Indoors)
									{
										if (!this.GoAway)
										{
                                            //This is only called if a clubmember needs to perform a club action.

                                            //Debug.Log("Could it be occuring here?");

                                            this.CurrentDestination = this.Destinations[this.Phase];
											this.Pathfinding.target = this.Destinations[this.Phase];
											this.DistanceToDestination = 100;
										}
									}
								}
							}
							else
							{
								if (this.ClubAttire)
								{
                                    this.TargetDistance = 1;

                                    if (!this.ChangingBooth.Occupied)
									{
										this.CurrentDestination = this.ChangingBooth.transform;
										this.Pathfinding.target = this.ChangingBooth.transform;
									}
									else
									{
										this.CurrentDestination = this.ChangingBooth.WaitSpots[this.ClubMemberID];
										this.Pathfinding.target = this.ChangingBooth.WaitSpots[this.ClubMemberID];
									}
								}
								else
								{
									if (this.Indoors)
									{
										if (this.Actions[this.Phase] != StudentActionType.Clean &&
											this.Actions[this.Phase] != StudentActionType.Sketch)
										{
											//This is only called if a clubmember needs to change into their club attire.

											//Debug.Log("Or, could it be occuring here?");

											this.CurrentDestination = this.Destinations[this.Phase];
											this.Pathfinding.target = this.Destinations[this.Phase];
										}
									}
								}
							}
						}
						else
						{
							if (this.Actions[this.Phase] == StudentActionType.SitAndTakeNotes)
							{
								if (this.Schoolwear > 1 && !this.SchoolwearUnavailable)
								{
									this.CurrentDestination = this.StudentManager.StrippingPositions[this.GirlID];
									this.Pathfinding.target = this.StudentManager.StrippingPositions[this.GirlID];
								}
							}
						}
					}

					if (!this.Pathfinding.canMove)
					{
						if (!this.Spawned)
						{
							this.transform.position = this.StudentManager.SpawnPositions[this.StudentID].position;
							this.Spawned = true;

							if (this.StudentID == 10 && this.StudentManager.Students[11] == null)
							{
								this.transform.position = new Vector3(-4, 0, -96);
								Physics.SyncTransforms();
							}
						}

						if (!this.Paired)
						{
							if (!this.Alarmed)
							{
								this.Pathfinding.canSearch = true;
								this.Pathfinding.canMove = true;
								this.Obstacle.enabled = false;
							}
						}
					}

                    if (!this.InEvent)
                    {
					    if (this.Pathfinding.speed > 0.0f)
					    {
                            //Debug.Log("here?");

						    if (this.Pathfinding.speed == 1.0f)
						    {
							    if (!this.CharacterAnimation.IsPlaying(this.WalkAnim))
							    {
								    if (this.Persona == PersonaType.PhoneAddict && this.Actions[this.Phase] == StudentActionType.Clean)
								    {
									    this.CharacterAnimation.CrossFade(this.OriginalWalkAnim);
								    }
								    else
								    {
									    this.CharacterAnimation.CrossFade(this.WalkAnim);
								    }

								    // [af] Commented in JS code.
								    //CharacterAnimation[WalkAnim].speed = Pathfinding.currentSpeed;
							    }
						    }
						    else
						    {
							    if (!this.Dying)
							    {
								    //Debug.Log("Sprinting 1");
								    this.CharacterAnimation.CrossFade(this.SprintAnim);
							    }
						    }
					    }
                    }

                    if (this.Club == ClubType.Occult)
					{
						if (this.Actions[this.Phase] != StudentActionType.ClubAction)
						{
							this.OccultBook.SetActive(false);
						}
					}

					if (this.Meeting == false && this.GoAway == false && this.InEvent == false)
					{
						if (this.Actions[this.Phase] == StudentActionType.Clean)
						{
							if (this.SmartPhone.activeInHierarchy)
							{
								this.SmartPhone.SetActive(false);
							}

							if (this.CurrentDestination != this.CleaningSpot.GetChild(this.CleanID))
							{
								this.CurrentDestination = this.CleaningSpot.GetChild(this.CleanID);
								this.Pathfinding.target = this.CurrentDestination;
							}
						}

						if (this.Actions[this.Phase] == StudentActionType.Patrol)
						{
							if (this.CurrentDestination != this.StudentManager.Patrols.List[this.StudentID].GetChild(this.PatrolID))
							{
								this.CurrentDestination = this.StudentManager.Patrols.List[this.StudentID].GetChild(this.PatrolID);
								this.Pathfinding.target = this.CurrentDestination;
							}
						}
					}
				}
			}
			//If we have reached our destination...
			else
			{
				if (this.CurrentDestination != null)
				{
					bool StopEarly = false;

					if (this.Actions[this.Phase] == StudentActionType.Sleuth && this.StudentManager.SleuthPhase == 3 ||
						this.Actions[this.Phase] == StudentActionType.Stalk ||
						this.Actions[this.Phase] == StudentActionType.Relax && this.CuriosityPhase == 1)
					{
						StopEarly = true;
					}

					//If this student is "Following"
					if (this.Actions[this.Phase] == StudentActionType.Follow)
					{
						//Debug.Log ("Raibaru-specific code.");

						//Debug.Log("Raibaru has reached her destination.");

						this.FollowTargetDistance = Vector3.Distance(
							this.FollowTarget.transform.position, this.StudentManager.Hangouts.List[this.StudentID].transform.position);
						
						if (this.FollowTargetDistance < 1.1f && !this.FollowTarget.Alone)
						{
							//Debug.Log("Raibaru is sliding towards her hangout location.");

							this.MoveTowardsTarget(this.StudentManager.Hangouts.List[this.StudentID].position);

							float Angle = Quaternion.Angle(this.transform.rotation, this.StudentManager.Hangouts.List[this.StudentID].rotation);

							if (Angle > 1.0f)
							{
								if (this.StopRotating == false)
								{
									this.transform.rotation = Quaternion.Slerp(
										this.transform.rotation, this.StudentManager.Hangouts.List[this.StudentID].rotation, 10.0f * Time.deltaTime);
								}
							}
						}
						else
						{
							if (!this.ManualRotation)
							{
								this.targetRotation = Quaternion.LookRotation(
									this.FollowTarget.transform.position - this.transform.position);
								this.transform.rotation = Quaternion.Slerp(
									this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);
							}
						}
					}
					//All other circumstances
					else
					{
						if (!StopEarly)
						{
							this.MoveTowardsTarget(this.CurrentDestination.position);

							float Angle = Quaternion.Angle(this.transform.rotation, this.CurrentDestination.rotation);

							if (Angle > 1.0f)
							{
								if (this.StopRotating == false)
								{
									this.transform.rotation = Quaternion.Slerp(
										this.transform.rotation, this.CurrentDestination.rotation, 10.0f * Time.deltaTime);
								}
							}
						}
						else
						{
							if (this.Actions[this.Phase] == StudentActionType.Sleuth || this.Actions[this.Phase] == StudentActionType.Stalk)
							{
								this.targetRotation = Quaternion.LookRotation(this.SleuthTarget.position - this.transform.position);
							}
							else
							{
								if (this.Crush > 0)
								{
									this.targetRotation = Quaternion.LookRotation(new Vector3(
										this.StudentManager.Students[this.Crush].transform.position.x,
										this.transform.position.y,
										this.StudentManager.Students[this.Crush].transform.position.z) - this.transform.position);
								}
							}

							float SleuthAngle = Quaternion.Angle(this.transform.rotation, this.targetRotation);

							this.transform.rotation = Quaternion.Slerp(this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);

							if (SleuthAngle > 1.0f)
							{
								this.transform.rotation = Quaternion.Slerp(this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);
							}
						}
					}

					// [af] Commented in JS code.
					//Debug.Log("My name is " + Name + " and my angle is " + Angle);

					if (!this.Hurry)
					{
						this.Pathfinding.speed = 1.0f;
					}
					else
					{
						//Debug.Log("Sprinting 5");
						this.Pathfinding.speed = 4.0f;
					}
				}

				if (this.Pathfinding.canMove)
				{
					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;

					if (this.Actions[this.Phase] != StudentActionType.Clean)
					{
						this.Obstacle.enabled = true;
					}
				}

				if (!this.InEvent && !this.Meeting)
				{
					if (this.DressCode)
					{
						if (this.Actions[this.Phase] == StudentActionType.ClubAction)
						{
							if (!this.ClubAttire)
							{
								if (!this.ChangingBooth.Occupied)
								{
									if (this.CurrentDestination == this.ChangingBooth.transform)
									{
										this.ChangingBooth.Occupied = true;
										this.ChangingBooth.Student = this;
										this.ChangingBooth.CheckYandereClub();
										this.Obstacle.enabled = false;
									}

									this.CurrentDestination = this.ChangingBooth.transform;
									this.Pathfinding.target = this.ChangingBooth.transform;
								}
								else
								{
									this.CharacterAnimation.CrossFade(this.IdleAnim);
								}
							}
							else
							{
								if (!this.GoAway)
								{
									//This is only called if a clubmember needs to perform a club action.

									this.CurrentDestination = this.Destinations[this.Phase];
									this.Pathfinding.target = this.Destinations[this.Phase];
								}
							}
						}
						else
						{
							if (this.ClubAttire)
							{
								if (!this.ChangingBooth.Occupied)
								{
									if (this.CurrentDestination == this.ChangingBooth.transform)
									{
										this.ChangingBooth.Occupied = true;
										this.ChangingBooth.Student = this;
										this.ChangingBooth.CheckYandereClub();
									}

									this.CurrentDestination = this.ChangingBooth.transform;
									this.Pathfinding.target = this.ChangingBooth.transform;
								}
								else
								{
									this.CharacterAnimation.CrossFade(this.IdleAnim);
								}
							}
							else
							{
								//This is only called if a clubmember needs to change into their club attire.

								if (this.Actions[this.Phase] != StudentActionType.Clean)
								{
									this.CurrentDestination = this.Destinations[this.Phase];
									this.Pathfinding.target = this.Destinations[this.Phase];
								}
							}
						}
					}
				}

				if (!this.InEvent)
				{
					if (!this.Meeting)
					{
						if (!this.GoAway)
						{
							// At Locker.
							if (this.Actions[this.Phase] == StudentActionType.AtLocker)
							{
								this.CharacterAnimation.CrossFade(this.IdleAnim);

								this.Pathfinding.canSearch = false;
								this.Pathfinding.canMove = false;
							}
							// Socializing.
							else if (this.Actions[this.Phase] == StudentActionType.Socializing ||
								this.Actions[this.Phase] == StudentActionType.Follow && FollowTargetDistance < 1 &&
								!this.FollowTarget.Alone && !this.FollowTarget.InEvent && !this.FollowTarget.Talking)
							{
								//Debug.Log ("Raibaru-specific code.");

								//Debug.Log("My name is " + this.Name + " and I'm socializing!");

								if (this.MyPlate != null)
								{
									if (this.MyPlate.parent == this.RightHand)
									{
										this.MyPlate.parent = null;
										this.MyPlate.position = this.OriginalPlatePosition;
										this.MyPlate.rotation = this.OriginalPlateRotation;

										this.IdleAnim = this.OriginalIdleAnim;
										this.WalkAnim = this.OriginalWalkAnim;
										this.LeanAnim = this.OriginalLeanAnim;

										this.ResumeDistracting = false;
										this.Distracting = false;
										this.Distracted = false;
										this.CanTalk = true;
									}
								}

								if (this.Paranoia > 1.66666f && !StudentManager.LoveSick && this.Club != ClubType.Delinquent)
								{
									//Debug.Log(this.Name);

									this.CharacterAnimation.CrossFade(this.IdleAnim);
								}
								else
								{
									this.StudentManager.ConvoManager.CheckMe(StudentID);

									if (this.Club == ClubType.Delinquent)
									{
										if (this.Alone)
										{
											if (this.TrueAlone)
											{
												if (!this.SmartPhone.activeInHierarchy)
												{
													this.CharacterAnimation.CrossFade(AnimNames.DelinquentTexting);
													this.SmartPhone.SetActive(true);
													this.SpeechLines.Stop();
												}
											}
											else
											{
												this.CharacterAnimation.CrossFade(this.IdleAnim);
												this.SpeechLines.Stop();
											}
										}
										else
										{
											//This code is a duplicated version of what's below it...
											if (!this.InEvent)
											{
												if (!this.Grudge)
												{
													if (!this.SpeechLines.isPlaying)
													{
														this.SmartPhone.SetActive(false);
														this.SpeechLines.Play();
													}
												}
												else
												{
													this.SmartPhone.SetActive(false);
												}
											}

											this.CharacterAnimation.CrossFade(this.RandomAnim);

											if (this.CharacterAnimation[this.RandomAnim].time >=
												this.CharacterAnimation[this.RandomAnim].length)
											{
												this.PickRandomAnim();
											}
										}
									}
									//If we are not a delinquent...
									else
									{
										if (this.Alone)
										{
											if (!this.Male)
											{
												this.CharacterAnimation.CrossFade(AnimNames.FemaleTexting);
											}
											else
											{
												if (this.StudentID == 36)
												{
													this.CharacterAnimation.CrossFade(this.ClubAnim);
												}
												else
												{
													if (this.StudentID == 66)
													{
														this.CharacterAnimation.CrossFade(AnimNames.DelinquentTexting);
													}
													else
													{
														this.CharacterAnimation.CrossFade(AnimNames.MaleTexting);
													}
												}
											}

											if (!this.SmartPhone.activeInHierarchy && !this.Shy)
											{
												if (this.StudentID == 36)
												{
													this.SmartPhone.transform.localPosition = new Vector3(.0566666f, -.02f, 0);
													this.SmartPhone.transform.localEulerAngles = new Vector3(10, 115, 180);
												}
												else
												{
													this.SmartPhone.transform.localPosition = new Vector3(0.015f, 0.01f, 0.025f);
													this.SmartPhone.transform.localEulerAngles = new Vector3(10, -160, 165);
												}

												this.SmartPhone.SetActive(true);
												this.SpeechLines.Stop();
											}
										}
										else
										{
											if (!this.InEvent)
											{
												if (!this.Grudge)
												{
													if (!this.SpeechLines.isPlaying)
													{
														this.SmartPhone.SetActive(false);
														this.SpeechLines.Play();
													}
												}
												else
												{
													this.SmartPhone.SetActive(false);
												}
											}

											if (this.Club != ClubType.Photography)
											{
												this.CharacterAnimation.CrossFade(this.RandomAnim);

												if (this.CharacterAnimation[this.RandomAnim].time >=
													this.CharacterAnimation[this.RandomAnim].length)
												{
													this.PickRandomAnim();
												}
											}
											else
											{
												this.CharacterAnimation.CrossFade(this.RandomSleuthAnim);

												if (this.CharacterAnimation[this.RandomSleuthAnim].time >=
													this.CharacterAnimation[this.RandomSleuthAnim].length)
												{
													this.PickRandomSleuthAnim();
												}
											}
										}
									}
								}
							}
							// Gossipping.
							else if (this.Actions[this.Phase] == StudentActionType.Gossip)
							{
								if (this.Paranoia > 1.66666f && !StudentManager.LoveSick)
								{
									this.CharacterAnimation.CrossFade(this.IdleAnim);
								}
								else
								{
									this.StudentManager.ConvoManager.CheckMe(StudentID);

									if (this.Alone)
									{
										if (!this.Shy)
										{
											this.CharacterAnimation.CrossFade(AnimNames.FemaleTexting);

											if (!this.SmartPhone.activeInHierarchy)
											{
												this.SmartPhone.SetActive(true);
												this.SpeechLines.Stop();
											}
										}
									}
									else
									{
										if (!this.SpeechLines.isPlaying)
										{
											this.SmartPhone.SetActive(false);
											this.SpeechLines.Play();
										}

										this.CharacterAnimation.CrossFade(this.RandomGossipAnim);

										if (this.CharacterAnimation[this.RandomGossipAnim].time >=
											this.CharacterAnimation[this.RandomGossipAnim].length)
										{
											this.PickRandomGossipAnim();
										}
									}
								}
							}
							// Gaming.
							else if (this.Actions[this.Phase] == StudentActionType.Gaming)
							{
								this.CharacterAnimation.CrossFade(this.GameAnim);
							}
							// Shamed.
							else if (this.Actions[this.Phase] == StudentActionType.Shamed)
							{
								this.CharacterAnimation.CrossFade(this.SadSitAnim);
							}
							// Slave.
							else if (this.Actions[this.Phase] == StudentActionType.Slave)
							{
								this.CharacterAnimation.CrossFade(this.BrokenSitAnim);

								if (this.FragileSlave)
								{
									if (this.HuntTarget == null)
									{
										this.HuntTarget = this;
										this.GoCommitMurder();
									}
									else
									{
										if (this.HuntTarget.Indoors == true)
										{
											this.GoCommitMurder();
										}
									}
								}
							}
							// Relax.
							else if (this.Actions[this.Phase] == StudentActionType.Relax)
							{
								if (this.CuriosityPhase == 0)
								{
									this.CharacterAnimation.CrossFade(this.RelaxAnim);

									if (this.Curious)
									{
										this.CuriosityTimer += Time.deltaTime;

										if (this.CuriosityTimer > 30)
										{
											if (!this.StudentManager.Students[this.Crush].Private &&
												!this.StudentManager.Students[this.Crush].Wet)
											{
												this.Pathfinding.target = this.StudentManager.Students[this.Crush].transform;
												this.CurrentDestination = this.StudentManager.Students[this.Crush].transform;
												this.TargetDistance = 5;
												this.CuriosityTimer = 0;
												this.CuriosityPhase++;
											}
											else
											{
												this.CuriosityTimer = 0;
											}
										}
									}
								}
								else
								{
									this.CharacterAnimation.CrossFade(this.LeanAnim);

									this.CuriosityTimer += Time.deltaTime;

									if (this.CuriosityTimer > 10 || !this.StudentManager.Students[this.Crush].Private ||
										!this.StudentManager.Students[this.Crush].Wet)
									{
										this.Pathfinding.target = this.Destinations[this.Phase];
										this.CurrentDestination = this.Destinations[this.Phase];
										this.TargetDistance = .5f;
										this.CuriosityTimer = 0;
										this.CuriosityPhase--;
									}
								}
							}
							// Sit and Take Notes.
							else if (this.Actions[this.Phase] == StudentActionType.SitAndTakeNotes)
							{
                                if (this.MyPlate != null)
								{
									if (this.MyPlate.parent == this.RightHand)
									{
										this.MyPlate.parent = null;
										this.MyPlate.position = this.OriginalPlatePosition;
										this.MyPlate.rotation = this.OriginalPlateRotation;

										this.CurrentDestination = this.Destinations[this.Phase];
										this.Pathfinding.target = this.Destinations[this.Phase];

										this.IdleAnim = this.OriginalIdleAnim;
										this.WalkAnim = this.OriginalWalkAnim;
										this.LeanAnim = this.OriginalLeanAnim;

										this.ResumeDistracting = false;
										this.Distracting = false;
										this.Distracted = false;
										this.CanTalk = true;
									}
								}

								if (this.MustChangeClothing)
								{
                                    // After reaching the communal locker...
                                    if (this.ChangeClothingPhase == 0)
                                    {
                                        Instantiate(this.StudentManager.CommunalLocker.SteamCloud,
                                        this.transform.position + Vector3.up * 0.81f, Quaternion.identity);

                                        this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
                                        this.ChangeClothingPhase++;
                                    }
                                    // While the steam is visible...
                                    else if (this.ChangeClothingPhase == 1)
                                    {
                                        this.CharacterAnimation.CrossFade(this.StripAnim);

                                        this.Pathfinding.canSearch = false;
                                        this.Pathfinding.canMove = false;

                                        if (this.CharacterAnimation[this.StripAnim].time >= 1.50f)
                                        {
                                            if (this.Schoolwear != 1)
                                            {
                                                this.Schoolwear = 1;
                                                this.ChangeSchoolwear();
                                            }

                                            if (this.CharacterAnimation[this.StripAnim].time >= this.CharacterAnimation[this.StripAnim].length)
                                            {
                                                this.Pathfinding.target = this.Seat;
                                                this.CurrentDestination = this.Seat;

                                                this.ChangeClothingPhase++;
                                                this.MustChangeClothing = false;
                                            }
                                        }
                                    }
								}
								else
								{
									if (this.Bullied)
									{
										if (this.SmartPhone.activeInHierarchy)
										{
											this.SmartPhone.SetActive(false);
										}

										this.CharacterAnimation.CrossFade(SadDeskSitAnim, 1);
									}
									else
									{
										if (this.Phoneless)
										{
											if (this.StudentManager.CommunalLocker.RivalPhone.gameObject.activeInHierarchy)
											{
												if (!this.EndSearch)
												{
													if (this.Yandere.CanMove)
													{
														if (this.StudentID == this.StudentManager.RivalID)
														{
															//If we're waiting for Osana to find her phone...
															if (SchemeGlobals.GetSchemeStage(1) == 7)
															{
																SchemeGlobals.SetSchemeStage(1, 8);
																this.Yandere.PauseScreen.Schemes.UpdateInstructions();
															}
														}

														this.CharacterAnimation.CrossFade (this.DiscoverPhoneAnim);
														this.Subtitle.UpdateLabel(this.LostPhoneSubtitleType, 2, 5.0f);
														this.EndSearch = true;
														this.Routine = false;
													}
												}
											}
										}

										if (!this.EndSearch)
										{
											//If class hasn't actually begun yet...
											if (this.Clock.Period != 2 && this.Clock.Period != 4)
											{
												if (this.DressCode && this.ClubAttire)
												{
													this.CharacterAnimation.CrossFade(this.IdleAnim);
												}
												else
												{
													if (Vector3.Distance(this.transform.position, this.Seat.position) < .5)
													{
														if (!Phoneless)
														{
															if (this.StudentID == 1 && this.StudentManager.Gift.activeInHierarchy)
															{
																this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
																this.CharacterAnimation.CrossFade(this.InspectBloodAnim);

																if (this.CharacterAnimation[this.InspectBloodAnim].time >=
																	this.CharacterAnimation[this.InspectBloodAnim].length)
																{
																	this.StudentManager.Gift.SetActive(false);
																}
															}
															else
															{
																if (this.Club != ClubType.Delinquent)
																{
																	if (!this.SmartPhone.activeInHierarchy)
																	{
																		if (this.Male)
																		{
																			this.SmartPhone.transform.localPosition = new Vector3(0.025f, 0.0025f, 0.025f);
																			this.SmartPhone.transform.localEulerAngles = new Vector3(0, -160.0f, 180);
																		}
																		else
																		{
																			this.SmartPhone.transform.localPosition = new Vector3(0.01f, 0.01f, 0.01f);
																			this.SmartPhone.transform.localEulerAngles = new Vector3(0, -160.0f, 165);
																		}

																		this.SmartPhone.SetActive(true);
																	}

																	this.CharacterAnimation.CrossFade(this.DeskTextAnim);
																}
																else
																{
																	this.CharacterAnimation.CrossFade("delinquentSit_00");
																}
															}	
														}
														else
														{
															this.CharacterAnimation.CrossFade(SadDeskSitAnim);
														}
													}
												}
											}
											//If class has begun...
											else
											{
												if (this.StudentManager.Teachers[Class].SpeechLines.isPlaying &&
													!this.StudentManager.Teachers[Class].Alarmed)
												{
													if (this.DressCode && this.ClubAttire)
													{
														this.CharacterAnimation.CrossFade(this.IdleAnim);
													}
													else
													{
														if (!this.Depressed)
														{
															if (!this.Pen.activeInHierarchy)
															{
																this.Pen.SetActive(true);
															}
														}

														if (this.SmartPhone.activeInHierarchy)
														{
															this.SmartPhone.SetActive(false);
														}

														if (this.MyPaper == null)
														{
															if (this.transform.position.x < 0.0f)
															{
																this.MyPaper = Instantiate(this.Paper,
																	this.Seat.position + new Vector3(-0.40f, 0.772f, 0.0f),
																	Quaternion.identity);
															}
															else
															{
																this.MyPaper = Instantiate(this.Paper,
																	this.Seat.position + new Vector3(0.40f, 0.772f, 0.0f),
																	Quaternion.identity);
															}

															this.MyPaper.transform.eulerAngles = new Vector3(0.0f, 0.0f, -90.0f);
															this.MyPaper.transform.localScale = new Vector3(0.0050f, 0.0050f, 0.0050f);

															this.MyPaper.transform.parent = this.StudentManager.Papers;
														}

														this.CharacterAnimation.CrossFade(this.SitAnim);
													}
												}
												//If the teacher is missing...
												else
												{
													if (this.Club != ClubType.Delinquent)
													{
														this.CharacterAnimation.CrossFade(this.ConfusedSitAnim);
													}
													else
													{
														this.CharacterAnimation.CrossFade("delinquentSit_00");
													}
												}
											}
										}
									}
								}
							}
							// Peek.
							else if (this.Actions[this.Phase] == StudentActionType.Peek)
							{
								this.CharacterAnimation.CrossFade(this.PeekAnim);

								if (this.Male)
								{
									this.Cosmetic.MyRenderer.materials[this.Cosmetic.FaceID].SetFloat("_BlendAmount", 1.0f);
								}
							}
							// Club Action.
							else if (this.Actions[this.Phase] == StudentActionType.ClubAction)
							{
								if (this.DressCode && !this.ClubAttire)
								{
									this.CharacterAnimation.CrossFade(this.IdleAnim);
								}
								else
								{
									if (this.StudentID == 47 || this.StudentID == 49)
									{
										if (this.GetNewAnimation)
										{
											this.StudentManager.ConvoManager.MartialArtsCheck();
										}

										if (this.CharacterAnimation[this.ClubAnim].time >= this.CharacterAnimation[this.ClubAnim].length)
										{
											this.GetNewAnimation = true;
										}
									}

									if (this.Club != ClubType.Occult)
									{
										this.CharacterAnimation.CrossFade(this.ClubAnim);
									}
								}

								//Cooking Club
								if (this.Club == ClubType.Cooking)
								{
									if (this.ClubActivityPhase == 0)
									{
										//this.CharacterAnimation.CrossFade(this.ClubAnim);

										if (this.ClubTimer == 0)
										{
											this.MyPlate.parent = null;
											this.MyPlate.gameObject.SetActive(true);
											this.MyPlate.position = this.OriginalPlatePosition;
											this.MyPlate.rotation = this.OriginalPlateRotation;
										}

										this.ClubTimer += Time.deltaTime;

										if (this.ClubTimer > 60)
										{
											this.MyPlate.parent = this.RightHand;
											this.MyPlate.localPosition = new Vector3(0.02f, -.02f, -0.15f);
											this.MyPlate.localEulerAngles = new Vector3(-5.0f, -90.0f, 172.5f);

											this.IdleAnim = this.PlateIdleAnim;
											this.WalkAnim = this.PlateWalkAnim;
											this.LeanAnim = this.PlateIdleAnim;

											this.GetFoodTarget();
											this.ClubTimer = 0;
											this.ClubActivityPhase++;
										}
									}
									else
									{
										//if (this.SleuthTarget.GetComponent<StudentScript>().Club == ClubType.Cooking)
										//{
											this.GetFoodTarget();
										//}
									}
								}
								//Drama Club
								else if (this.Club == ClubType.Drama)
								{
									this.StudentManager.DramaTimer += Time.deltaTime;

									if (this.StudentManager.DramaPhase == 1)
									{
										this.StudentManager.ConvoManager.CheckMe(StudentID);

										if (this.Alone)
										{
											if (this.Phoneless)
											{
												this.CharacterAnimation.CrossFade(AnimNames.FemaleSit01);
											}
											else
											{
												if (this.Male){this.CharacterAnimation.CrossFade(AnimNames.MaleTexting);}
												else {this.CharacterAnimation.CrossFade(AnimNames.FemaleTexting);}

												if (!this.SmartPhone.activeInHierarchy)
												{
													this.SmartPhone.transform.localPosition = new Vector3(.02f, .01f, .03f);
													this.SmartPhone.transform.localEulerAngles = new Vector3(5, -160, 180);

													this.SmartPhone.SetActive(true);
													this.SpeechLines.Stop();
												}
											}
										}
										else
										{
											if (this.StudentID == 26)
											{
												if (!this.SpeechLines.isPlaying)
												{
													this.SmartPhone.SetActive(false);
													this.SpeechLines.Play();
												}
											}
										}

										if (this.StudentManager.DramaTimer > 100)
										{
											this.StudentManager.DramaTimer = 0;
											this.StudentManager.UpdateDrama();
										}
									}
									else if (StudentManager.DramaPhase == 2)
									{
										if (this.StudentManager.DramaTimer > 300)
										{
											this.StudentManager.DramaTimer = 0;
											this.StudentManager.UpdateDrama();
										}
									}
									else if (this.StudentManager.DramaPhase == 3)
									{
										this.SpeechLines.Play();

										this.CharacterAnimation.CrossFade(this.RandomAnim);

										if (this.CharacterAnimation[this.RandomAnim].time >=
											this.CharacterAnimation[this.RandomAnim].length)
										{
											this.PickRandomAnim();
										}

										if (this.StudentManager.DramaTimer > 100)
										{
											this.StudentManager.DramaTimer = 0;
											this.StudentManager.UpdateDrama();
										}
									}
								}
								//Occult Club
								else if (this.Club == ClubType.Occult)
								{
									if (this.ReadPhase == 0)
									{
										this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
										this.CharacterAnimation.CrossFade(this.BookSitAnim);

										if (this.CharacterAnimation[this.BookSitAnim].time >
											this.CharacterAnimation[this.BookSitAnim].length)
										{
											this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
											this.CharacterAnimation.CrossFade(this.BookReadAnim);
											this.ReadPhase++;
										}
										else if (this.CharacterAnimation[this.BookSitAnim].time > 1.0f)
										{
											this.OccultBook.SetActive(true);
										}
									}
								}
								//Art Club
								else if (this.Club == ClubType.Art)
								{
									if (this.ClubAttire)
									{
										if (!this.Paintbrush.activeInHierarchy)
										{
											if (Vector3.Distance(this.transform.position, CurrentDestination.position) < .5)
											{
												this.Paintbrush.SetActive(true);
												this.Palette.SetActive(true);
											}
										}
									}
								}
								//Light Music Club
								else if (this.Club == ClubType.LightMusic)
								{
									if (Clock.HourTime < 16.9)
									{
										this.Instruments[this.ClubMemberID].SetActive(true);
										this.CharacterAnimation.CrossFade(this.ClubAnim);

										if (this.StudentID == 51)
										{
											if (this.InstrumentBag[this.ClubMemberID].transform.parent != null)
											{
												this.InstrumentBag[this.ClubMemberID].transform.parent = null;
												this.InstrumentBag[this.ClubMemberID].transform.position = new Vector3(.5f, 4.5f, 22.45666f);
												this.InstrumentBag[this.ClubMemberID].transform.eulerAngles = new Vector3(-15, 0, 0);
											}

											if (this.Instruments[this.ClubMemberID].transform.parent == null)
											{
												this.Instruments[this.ClubMemberID].GetComponent<AudioSource>().Play();

												this.Instruments[this.ClubMemberID].transform.parent = this.transform;
												this.Instruments[this.ClubMemberID].transform.localPosition = new Vector3(.340493f, .653502f, -.286104f);
												this.Instruments[this.ClubMemberID].transform.localEulerAngles = new Vector3(-13.6139f, 16.16775f, 72.5293f);
											}
										}
										else
										{
											if (this.StudentID == 54)
											{
												if (!this.Drumsticks[0].activeInHierarchy)
												{
													Drumsticks[0].SetActive(true);
													Drumsticks[1].SetActive(true);
												}
											}
										}
									}
									else
									{
										if (this.StudentID == 51)
										{
											if (!this.InstrumentBag[this.ClubMemberID].transform.parent != null)
											{
												this.InstrumentBag[this.ClubMemberID].transform.position = new Vector3(.5f, 4.5f, 22.45666f);
												this.InstrumentBag[this.ClubMemberID].transform.eulerAngles = new Vector3(-15, 0, 0);
												this.InstrumentBag[this.ClubMemberID].transform.parent = null;
											}

											if (!this.StudentManager.PracticeMusic.isPlaying){this.CharacterAnimation.CrossFade("f02_vocalIdle_00");}
											else
											{
												if (this.StudentManager.PracticeMusic.time > 114.5f){this.CharacterAnimation.CrossFade("f02_vocalCelebrate_00");}
												else if (this.StudentManager.PracticeMusic.time > 104){this.CharacterAnimation.CrossFade("f02_vocalWait_00");}
												else if (this.StudentManager.PracticeMusic.time > 32){this.CharacterAnimation.CrossFade("f02_vocalSingB_00");}
												else if (this.StudentManager.PracticeMusic.time > 24){this.CharacterAnimation.CrossFade("f02_vocalSingB_00");}
												else if (this.StudentManager.PracticeMusic.time > 17){this.CharacterAnimation.CrossFade("f02_vocalSingB_00");}
												else if (this.StudentManager.PracticeMusic.time > 14){this.CharacterAnimation.CrossFade("f02_vocalWait_00");}
												else if (this.StudentManager.PracticeMusic.time > 8){this.CharacterAnimation.CrossFade("f02_vocalSingA_00");}
												else if (this.StudentManager.PracticeMusic.time > 0){this.CharacterAnimation.CrossFade("f02_vocalWait_00");}
											}
										}
										else if (this.StudentID == 52)
										{
											if (!this.Instruments[this.ClubMemberID].activeInHierarchy)
											{
												this.Instruments[this.ClubMemberID].SetActive(true);
												this.Instruments[this.ClubMemberID].GetComponent<AudioSource>().Stop();
												this.Instruments[this.ClubMemberID].GetComponent<AudioSource>().playOnAwake = false;

												this.Instruments[this.ClubMemberID].transform.parent = this.Spine;
												this.Instruments[this.ClubMemberID].transform.localPosition = new Vector3(0.275f, -.16f, .095f);
												this.Instruments[this.ClubMemberID].transform.localEulerAngles = new Vector3(-22.5f, 30, 60);

												this.InstrumentBag[this.ClubMemberID].transform.parent = null;
												this.InstrumentBag[this.ClubMemberID].transform.position = new Vector3(5.5825f, 4.01f, 25);
												this.InstrumentBag[this.ClubMemberID].transform.eulerAngles = new Vector3(-15, -90, 0);
											}

											if (!this.StudentManager.PracticeMusic.isPlaying){this.CharacterAnimation.CrossFade("f02_guitarIdle_00");}
											else
											{
												if (this.StudentManager.PracticeMusic.time > 114.5f){this.CharacterAnimation.CrossFade("f02_guitarCelebrate_00");}
												else if (this.StudentManager.PracticeMusic.time > 112){this.CharacterAnimation.CrossFade("f02_guitarWait_00");}
												else if (this.StudentManager.PracticeMusic.time > 64){this.CharacterAnimation.CrossFade("f02_guitarPlayA_01");}
												else if (this.StudentManager.PracticeMusic.time > 8){this.CharacterAnimation.CrossFade("f02_guitarPlayA_00");}
												else if (this.StudentManager.PracticeMusic.time > 0){this.CharacterAnimation.CrossFade("f02_guitarWait_00");}
											}
										}
										else if (this.StudentID == 53)
										{
											if (!this.Instruments[this.ClubMemberID].activeInHierarchy)
											{
												this.Instruments[this.ClubMemberID].SetActive(true);
												this.Instruments[this.ClubMemberID].GetComponent<AudioSource>().Stop();
												this.Instruments[this.ClubMemberID].GetComponent<AudioSource>().playOnAwake = false;

												this.Instruments[this.ClubMemberID].transform.parent = this.Spine;
												this.Instruments[this.ClubMemberID].transform.localPosition = new Vector3(0.275f, -.16f, .095f);
												this.Instruments[this.ClubMemberID].transform.localEulerAngles = new Vector3(-22.5f, 30, 60);

												this.InstrumentBag[this.ClubMemberID].transform.parent = null;
												this.InstrumentBag[this.ClubMemberID].transform.position = new Vector3(5.5825f, 4.01f, 26);
												this.InstrumentBag[this.ClubMemberID].transform.eulerAngles = new Vector3(-15, -90, 0);
											}

											if (!this.StudentManager.PracticeMusic.isPlaying){this.CharacterAnimation.CrossFade("f02_guitarIdle_00");}
											else
											{
												if (this.StudentManager.PracticeMusic.time > 114.5f){this.CharacterAnimation.CrossFade("f02_guitarCelebrate_00");}
												else if (this.StudentManager.PracticeMusic.time > 112){this.CharacterAnimation.CrossFade("f02_guitarWait_00");}
												else if (this.StudentManager.PracticeMusic.time > 88){this.CharacterAnimation.CrossFade("f02_guitarPlayA_00");}
												else if (this.StudentManager.PracticeMusic.time > 80){this.CharacterAnimation.CrossFade("f02_guitarWait_00");}
												else if (this.StudentManager.PracticeMusic.time > 64){this.CharacterAnimation.CrossFade("f02_guitarPlayB_00");}
												//else if (this.StudentManager.PracticeMusic.time > 24){this.CharacterAnimation.CrossFade("f02_guitarPlayA_01");}
												else if (this.StudentManager.PracticeMusic.time > 0){this.CharacterAnimation.CrossFade("f02_guitarPlaySlowA_01");}
											}
										}
										else if (this.StudentID == 54)
										{
											if (this.InstrumentBag[this.ClubMemberID].transform.parent != null)
											{
												this.InstrumentBag[this.ClubMemberID].transform.parent = null;
												this.InstrumentBag[this.ClubMemberID].transform.position = new Vector3(5.5825f, 4.01f, 23);
												this.InstrumentBag[this.ClubMemberID].transform.eulerAngles = new Vector3(-15, -90, 0);
											}

											Drumsticks[0].SetActive(true);
											Drumsticks[1].SetActive(true);

											if (!this.StudentManager.PracticeMusic.isPlaying){this.CharacterAnimation.CrossFade("f02_drumsIdle_00");}
											else
											{
												if (this.StudentManager.PracticeMusic.time > 114.5f){this.CharacterAnimation.CrossFade("f02_drumsCelebrate_00");}
												else if (this.StudentManager.PracticeMusic.time > 108){this.CharacterAnimation.CrossFade("f02_drumsIdle_00");}
												else if (this.StudentManager.PracticeMusic.time > 96){this.CharacterAnimation.CrossFade("f02_drumsPlaySlow_00");}
												else if (this.StudentManager.PracticeMusic.time > 80){this.CharacterAnimation.CrossFade("f02_drumsIdle_00");}
												else if (this.StudentManager.PracticeMusic.time > 38){this.CharacterAnimation.CrossFade("f02_drumsPlay_00");}
												else if (this.StudentManager.PracticeMusic.time > 46){this.CharacterAnimation.CrossFade("f02_drumsIdle_00");}
												else if (this.StudentManager.PracticeMusic.time > 16){this.CharacterAnimation.CrossFade("f02_drumsPlay_00");}
												else if (this.StudentManager.PracticeMusic.time > 0){this.CharacterAnimation.CrossFade("f02_drumsIdle_00");}
											}
										}
										else if (this.StudentID == 55)
										{
											if (this.InstrumentBag[this.ClubMemberID].transform.parent != null)
											{
												this.InstrumentBag[this.ClubMemberID].transform.parent = null;
												this.InstrumentBag[this.ClubMemberID].transform.position = new Vector3(5.5825f, 4.01f, 24);
												this.InstrumentBag[this.ClubMemberID].transform.eulerAngles = new Vector3(-15, -90, 0);
											}

											if (!this.StudentManager.PracticeMusic.isPlaying){this.CharacterAnimation.CrossFade("f02_keysIdle_00");}
											else
											{
												if (this.StudentManager.PracticeMusic.time > 114.5f){this.CharacterAnimation.CrossFade("f02_keysCelebrate_00");}
												else if (this.StudentManager.PracticeMusic.time > 80){this.CharacterAnimation.CrossFade("f02_keysWait_00");}
												else if (this.StudentManager.PracticeMusic.time > 24){this.CharacterAnimation.CrossFade("f02_keysPlay_00");}
												else if (this.StudentManager.PracticeMusic.time > 0){this.CharacterAnimation.CrossFade("f02_keysWait_00");}
											}
										}
									}
								}
								//Science Club
								else if (this.Club == ClubType.Science)
								{
									if (this.ClubAttire && !this.GoAway)
									{
										if (this.SciencePhase == 0)
										{
											this.CharacterAnimation.CrossFade(this.ClubAnim);
										}
										else
										{
											this.CharacterAnimation.CrossFade(this.RummageAnim);
										}

										if (Vector3.Distance(this.transform.position, CurrentDestination.position) < .5)
										{
											if (this.SciencePhase == 0)
											{
												//Hologram Guy
												if (this.StudentID == 62)
												{
													ScienceProps[0].SetActive(true);
												}
												//Chemistry Guy
												else if (this.StudentID == 63)
												{
													ScienceProps[1].SetActive(true);
													ScienceProps[2].SetActive(true);
												}
												//Mecha Girl
												else if (this.StudentID == 64)
												{
													ScienceProps[0].SetActive(true);
												}
											}

											if (this.StudentID > 61)
											{
												this.ClubTimer += Time.deltaTime;

												if (this.ClubTimer > 60)
												{
													this.ClubTimer = 0;

													this.SciencePhase++;

													if (this.SciencePhase == 1)
													{
														Debug.Log(this.Name + " should be going to a closet now.");

														this.ClubTimer = 50;

                                                        this.Destinations[this.Phase] = this.StudentManager.SupplySpots[this.StudentID - 61];

                                                        this.CurrentDestination = this.StudentManager.SupplySpots[this.StudentID - 61];
														this.Pathfinding.target = this.StudentManager.SupplySpots[this.StudentID - 61];

														foreach (GameObject prop in this.ScienceProps){if (prop != null){prop.SetActive(false);}}
													}
													else
													{
														Debug.Log(this.Name + " should be going to their original clubroom position now.");

														this.SciencePhase = 0;
														this.ClubTimer = 0;

                                                        this.Destinations[this.Phase] = this.StudentManager.Clubs.List[this.StudentID];

                                                        this.CurrentDestination = this.StudentManager.Clubs.List[this.StudentID];
                                                        this.Pathfinding.target = this.StudentManager.Clubs.List[this.StudentID];
													}
												}
											}
										}
									}
									else
									{
										this.CharacterAnimation.CrossFade(this.IdleAnim);
									}
								}
								//Sports Club
								else if (this.Club == ClubType.Sports)
								{
									this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;

									//After performing the first stretching exercise...
									if (this.ClubActivityPhase == 0)
									{
										if (this.CharacterAnimation[this.ClubAnim].time >=
											this.CharacterAnimation[this.ClubAnim].length)
										{
											this.ClubActivityPhase++;
											this.ClubAnim = "stretch_01";
											this.Destinations[this.Phase] = this.StudentManager.Clubs.List[this.StudentID].GetChild(this.ClubActivityPhase);
										}
									}
									//After performing the second stretching exercise...
									else if (this.ClubActivityPhase == 1)
									{
										if (this.CharacterAnimation[this.ClubAnim].time >=
											this.CharacterAnimation[this.ClubAnim].length)
										{
											this.ClubActivityPhase++;
											this.ClubAnim = "stretch_02";
											this.Destinations[this.Phase] = this.StudentManager.Clubs.List[this.StudentID].GetChild(this.ClubActivityPhase);
										}
									}
									//After performing the third stretching exercise...
									else if (this.ClubActivityPhase == 2)
									{
										if (this.CharacterAnimation[this.ClubAnim].time >=
											this.CharacterAnimation[this.ClubAnim].length)
										{
											this.WalkAnim = "trackJog_00";

											this.Hurry = true;
											this.ClubActivityPhase++;
											this.CharacterAnimation[this.ClubAnim].time = 0;
											this.Destinations[this.Phase] = this.StudentManager.Clubs.List[this.StudentID].GetChild(this.ClubActivityPhase);
										}
									}
									//While running around the track...
									else if (this.ClubActivityPhase < 14)
									{
										if (this.CharacterAnimation[this.ClubAnim].time >=
											this.CharacterAnimation[this.ClubAnim].length)
										{
											this.ClubActivityPhase++;
											this.CharacterAnimation[this.ClubAnim].time = 0;
											this.Destinations[this.Phase] = this.StudentManager.Clubs.List[this.StudentID].GetChild(this.ClubActivityPhase);
										}
									}
									//After running around the track...
									else if (this.ClubActivityPhase == 14)
									{
										if (this.CharacterAnimation[this.ClubAnim].time >=
											this.CharacterAnimation[this.ClubAnim].length)
										{
											this.WalkAnim = this.OriginalWalkAnim;

											this.Hurry = false;
											this.ClubActivityPhase = 0;
											this.ClubAnim = "stretch_00";
											this.Destinations[this.Phase] = this.StudentManager.Clubs.List[this.StudentID].GetChild(this.ClubActivityPhase);
										}
									}
									//When diving into the pool...
									else if (this.ClubActivityPhase == 15)
									{
										if (this.CharacterAnimation[this.ClubAnim].time >= 1)
										{
											if (this.MyController.radius > 0)
											{
												this.MyRenderer.updateWhenOffscreen = true;
												this.Obstacle.enabled = false;
												this.MyController.radius = 0;
												this.Distracted = true;
											}
										}

										if (this.CharacterAnimation[this.ClubAnim].time >= 2)
										{
											float Weight = this.Cosmetic.Goggles[this.StudentID].GetComponent<SkinnedMeshRenderer>().GetBlendShapeWeight(0) + Time.deltaTime * 200; 
											this.Cosmetic.Goggles[this.StudentID].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, Weight);
										}

										if (this.CharacterAnimation[this.ClubAnim].time >= 5)
										{
											this.ClubActivityPhase++;
										}
									}
									//After leaving the diving board...
									else if (this.ClubActivityPhase == 16)
									{
										if (this.CharacterAnimation[this.ClubAnim].time >= 6.1)
										{
											this.Cosmetic.Goggles[this.StudentID].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 100);

											this.Cosmetic.MaleHair[this.Cosmetic.Hairstyle].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 100);

											GameObject NewSplash = Instantiate(this.BigWaterSplash, RightHand.transform.position, Quaternion.identity);
											NewSplash.transform.eulerAngles = new Vector3(
												-90.0f,
												NewSplash.transform.eulerAngles.y,
												NewSplash.transform.eulerAngles.z);

											this.SetSplashes(true);

											this.ClubActivityPhase++;
										}
									}
									//After finishing the diving animation...
									else if (this.ClubActivityPhase == 17)
									{
										if (this.CharacterAnimation[this.ClubAnim].time >=
											this.CharacterAnimation[this.ClubAnim].length)
										{
											this.WalkAnim = "poolSwim_00";
											this.ClubAnim = "poolSwim_00";

											this.ClubActivityPhase++;
											this.Destinations[this.Phase] = this.StudentManager.Clubs.List[this.StudentID].GetChild(this.ClubActivityPhase - 2);

											this.transform.position = this.Hips.transform.position;
											this.Character.transform.localPosition = new Vector3(0, -.25f, 0);
											Physics.SyncTransforms();

											this.CharacterAnimation.Play(this.WalkAnim);
										}
									}
									//After reaching the end of the pool...
									else if (this.ClubActivityPhase == 18)
									{
										this.ClubActivityPhase++;
										this.Destinations[this.Phase] = this.StudentManager.Clubs.List[this.StudentID].GetChild(this.ClubActivityPhase - 2);
										this.DistanceToDestination = 100;
									}
									//After reaching the ladder...
									else if (this.ClubActivityPhase == 19)
									{
										this.ClubAnim = "poolExit_00";

										if (this.CharacterAnimation[this.ClubAnim].time >= .1f)
										{
											this.SetSplashes(false);
										}

										if (this.CharacterAnimation[this.ClubAnim].time >= 4.66666f)
										{
											float Weight = this.Cosmetic.Goggles[this.StudentID].GetComponent<SkinnedMeshRenderer>().GetBlendShapeWeight(0) - Time.deltaTime * 200; 
											this.Cosmetic.Goggles[this.StudentID].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, Weight);
										}

										if (this.CharacterAnimation[this.ClubAnim].time >=
											this.CharacterAnimation[this.ClubAnim].length)
										{
											//this.TargetDistance = .5f;
											this.ClubActivityPhase = 15;
											this.ClubAnim = "poolDive_00";
											this.MyController.radius = .1f;
											this.WalkAnim = this.OriginalWalkAnim;
											this.MyRenderer.updateWhenOffscreen = false;
											this.Character.transform.localPosition = new Vector3(0, 0, 0);
											this.Cosmetic.Goggles[this.StudentID].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 0);
											this.Destinations[this.Phase] = this.StudentManager.Clubs.List[this.StudentID].GetChild(this.ClubActivityPhase);

											this.transform.position = new Vector3(this.Hips.position.x, 4, this.Hips.position.z);
											Physics.SyncTransforms();

											this.CharacterAnimation.Play(this.IdleAnim);

											this.Distracted = false;

											if (this.Clock.Period == 2 || this.Clock.Period == 4)
											{
												//Debug.Log("Sprinting 6");
												this.Pathfinding.speed = 4.0f;
											}
										}
									}
								}
								//Gardening Club
								else if (this.Club == ClubType.Gardening)
								{
									if (this.WateringCan.transform.parent != this.RightHand)
									{
										this.WateringCan.transform.parent = this.RightHand;
										this.WateringCan.transform.localPosition = new Vector3(.14f, -.15f, -.05f);
										this.WateringCan.transform.localEulerAngles = new Vector3(10, 15, 45);
									}

									this.PatrolTimer += Time.deltaTime * this.CharacterAnimation[this.PatrolAnim].speed;

									//this.CharacterAnimation.CrossFade(this.ClubAnim);

									if (this.PatrolTimer >= this.CharacterAnimation[this.ClubAnim].length)
									{
										this.PatrolID++;

										if (this.PatrolID == this.StudentManager.Patrols.List[this.StudentID].childCount)
										{
											this.PatrolID = 0;
										}

										this.CurrentDestination =
											this.StudentManager.Patrols.List[this.StudentID].GetChild(this.PatrolID);
										this.Pathfinding.target = this.CurrentDestination;

										this.PatrolTimer = 0.0f;

										this.WateringCan.transform.parent = this.Hips;
										this.WateringCan.transform.localPosition = new Vector3(0, .0135f, -.184f);
										this.WateringCan.transform.localEulerAngles = new Vector3(0, 90, 30);
									}
								}
								//Gaming Club
								else if (this.Club == ClubType.Gaming)
								{
									if (this.Phase < 8)
									{
										if (this.StudentID == 36)
										{
											if (!this.SmartPhone.activeInHierarchy)
											{
												this.SmartPhone.SetActive(true);

												this.SmartPhone.transform.localPosition = new Vector3(.0566666f, -.02f, 0);
												this.SmartPhone.transform.localEulerAngles = new Vector3(10, 115, 180);
											}
										}
									}
									else
									{
										if (!this.ClubManager.GameScreens[this.ClubMemberID].activeInHierarchy)
										{
											this.ClubManager.GameScreens[this.ClubMemberID].SetActive(true);
											this.MyController.radius = .2f;
										}

										if (this.SmartPhone.activeInHierarchy)
										{
											this.SmartPhone.SetActive(false);
										}
									}
								}
							}
							// Sit and Socialize.
							else if (this.Actions[this.Phase] == StudentActionType.SitAndSocialize)
							{
								if (this.Paranoia > 1.66666f)
								{
									this.CharacterAnimation.CrossFade(this.IdleAnim);
								}
								else
								{
									this.StudentManager.ConvoManager.CheckMe(StudentID);

									if (this.Alone)
									{
										if (!this.Male)
										{
											this.CharacterAnimation.CrossFade(AnimNames.FemaleTexting);
										}
										else
										{
											this.CharacterAnimation.CrossFade(AnimNames.MaleTexting);
										}

										if (!this.SmartPhone.activeInHierarchy)
										{
											this.SmartPhone.SetActive(true);
											this.SpeechLines.Stop();
										}
									}
									else
									{
										if (!this.InEvent)
										{
											if (!this.SpeechLines.isPlaying)
											{
												this.SmartPhone.SetActive(false);
												this.SpeechLines.Play();
											}
										}

										if (this.Club != ClubType.Photography)
										{
											this.CharacterAnimation.CrossFade(this.RandomAnim);

											if (this.CharacterAnimation[this.RandomAnim].time >=
												this.CharacterAnimation[this.RandomAnim].length)
											{
												this.PickRandomAnim();
											}
										}
										else
										{
											this.CharacterAnimation.CrossFade(this.RandomSleuthAnim);

											if (this.CharacterAnimation[this.RandomSleuthAnim].time >=
												this.CharacterAnimation[this.RandomSleuthAnim].length)
											{
												this.PickRandomSleuthAnim();
											}
										}
									}
								}
							}
							// Sit and Eat Bento.
							else if (this.Actions[this.Phase] == StudentActionType.SitAndEatBento)
							{
								if (!this.DiscCheck && this.Alarm < 100.0f)
								{
									if (!this.Ragdoll.Poisoned)
									{
										if (!this.Bento.activeInHierarchy || this.Bento.transform.parent == null)
										{
											this.SmartPhone.SetActive(false);

											if (!this.Male)
											{
												this.Bento.transform.parent = this.LeftItemParent;
												this.Bento.transform.localPosition = new Vector3(-0.025f, -0.105f, 0.0f);
												this.Bento.transform.localEulerAngles = new Vector3(0.0f, 165.0f, 82.50f);
											}
											else
											{
												this.Bento.transform.parent = this.LeftItemParent;
												this.Bento.transform.localPosition = new Vector3(-0.05f, -0.085f, 0);
												this.Bento.transform.localEulerAngles = new Vector3(-3.2f, -24.4f, -94.0f);
											}

											this.Chopsticks[0].SetActive(true);
											this.Chopsticks[1].SetActive(true);
											this.Bento.SetActive(true);
											this.Lid.SetActive(false);

											MyBento.Prompt.Hide();
											MyBento.Prompt.enabled = false;

											//Debug.Log("This is the exact moment the bento is taken out.");

											if (MyBento.Tampered)
											{
												if (MyBento.Emetic)
												{
													this.Emetic = true;
												}
												else if (MyBento.Lethal)
												{
													this.Lethal = true;
												}
												else if (MyBento.Tranquil)
												{
													this.Sedated = true;
												}
												else if (MyBento.Headache)
												{
													this.Headache = true;
												}

												this.Distracted = true;
											}
										}
									}

									if (!this.Emetic && !this.Lethal && !this.Sedated && !this.Headache)
									{
										this.CharacterAnimation.CrossFade(this.EatAnim);

										if (this.FollowTarget != null)
										{
											if (this.FollowTarget.CurrentAction != StudentActionType.SitAndEatBento &&
												!this.FollowTarget.Dying)
											{
												Debug.Log ("Osana is no longer eating, so Raibaru is now following Osana.");

												this.EmptyHands();

												Pathfinding.canSearch = true;
												Pathfinding.canMove = true;

												ScheduleBlock block4 = ScheduleBlocks[4];
												block4.destination = "Follow";
												block4.action = "Follow";

												this.GetDestinations();

												this.Pathfinding.target = this.FollowTarget.transform;
												this.CurrentDestination = this.FollowTarget.transform;
											}
										}
									}
									else
									{
										// Emetic Poisoning
										if (this.Emetic)
										{
											if (!this.Private)
											{
												this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
												this.CharacterAnimation.CrossFade(this.EmeticAnim);
												this.Private = true;
												this.CanTalk = false;
											}

											if (this.CharacterAnimation[this.EmeticAnim].time >= 16)
											{
												if (this.StudentID == 10)
												{
													if (this.VomitPhase < 0)
													{
														this.Subtitle.UpdateLabel(SubtitleType.ObstaclePoisonReaction, 0, 9.0f);
														this.VomitPhase++;
													}
												}
											}

											if (this.CharacterAnimation[this.EmeticAnim].time >=
												this.CharacterAnimation[this.EmeticAnim].length)
											{
												Debug.Log(this.Name + " has ingested emetic poison, and should be going to a toilet.");

												this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;

												if (this.Male)
												{
													this.WalkAnim = AnimNames.MaleStomachPainWalk;

													this.StudentManager.GetMaleVomitSpot(this);

													this.Pathfinding.target = this.StudentManager.MaleVomitSpot;
													this.CurrentDestination = this.StudentManager.MaleVomitSpot;
												}
												else
												{
													this.WalkAnim = AnimNames.FemaleStomachPainWalk;

													this.StudentManager.GetFemaleVomitSpot(this);

													this.Pathfinding.target = this.StudentManager.FemaleVomitSpot;
													this.CurrentDestination = this.StudentManager.FemaleVomitSpot;
												}

												//Obstacle
												if (this.StudentID == 10)
												{
													this.Pathfinding.target = this.StudentManager.AltFemaleVomitSpot;
													this.CurrentDestination = this.StudentManager.AltFemaleVomitSpot;
													this.VomitDoor = this.StudentManager.AltFemaleVomitDoor;
												}

												this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;

												this.CharacterAnimation.CrossFade(this.WalkAnim);
												this.DistanceToDestination = 100.0f;
												this.Pathfinding.canSearch = true;
												this.Pathfinding.canMove = true;
												this.Pathfinding.speed = 2.0f;
												this.MyBento.Tampered = false;
												this.Vomiting = true;
												this.Routine = false;

												this.Chopsticks[0].SetActive(false);
												this.Chopsticks[1].SetActive(false);
												this.Bento.SetActive(false);
											}
										}
										// Lethal Poisoning.
										else if (this.Lethal)
										{
											Debug.Log(this.Name + " is currently eating a poisoned bento.");

											if (!this.Private)
											{
												AudioSource audioSource = this.GetComponent<AudioSource>();
												audioSource.clip = this.PoisonDeathClip;
												audioSource.Play();

												if (this.Male)
												{
													this.CharacterAnimation.CrossFade(AnimNames.MalePoisonDeath);
													this.PoisonDeathAnim = AnimNames.MalePoisonDeath;
												}
												else
												{
													//Debug.Log("Attempting to trigger the female poison death animation.");

													this.CharacterAnimation.CrossFade(AnimNames.FemalePoisonDeath);
													this.PoisonDeathAnim = AnimNames.FemalePoisonDeath;
													this.Distracted = true;
												}
													
												this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
												this.MyRenderer.updateWhenOffscreen = true;

												this.Ragdoll.Poisoned = true;
												this.Private = true;

												this.Prompt.Hide();
												this.Prompt.enabled = false;
											}
											else
											{
												//If Osana is the one who is dying of poison...
												if (this.StudentID == 11)
												{
													//If Senpai is at school...
													if (this.StudentManager.Students[1] != null)
													{
														if (!this.StudentManager.Students[1].SenpaiWitnessingRivalDie)
														{
															//If she is next to Senpai...
															if (Vector3.Distance(this.transform.position, this.StudentManager.Students[1].transform.position) < 2)
															{
																Debug.Log("Setting ''SenpaiWitnessingRivalDie'' to true.");

																this.StudentManager.Students[1].CharacterAnimation.CrossFade(AnimNames.MaleWitnessPoisoning);
																this.StudentManager.Students[1].CurrentDestination = this.StudentManager.LunchSpots.List[1];
																this.StudentManager.Students[1].Pathfinding.target = this.StudentManager.LunchSpots.List[1];
																this.StudentManager.Students[1].MyRenderer.updateWhenOffscreen = true;
																this.StudentManager.Students[1].SenpaiWitnessingRivalDie = true;
																this.StudentManager.Students[1].Distracted = true;
																this.StudentManager.Students[1].Routine = false;
															}
														}
													}
												}
											}

											if (!this.Distracted)
											{
												if (this.CharacterAnimation[this.PoisonDeathAnim].time >= 2.5f)
												{
													this.Distracted = true;
												}
											}

											if (this.CharacterAnimation[this.PoisonDeathAnim].time >= 17.50f)
											{
												if (this.Bento.activeInHierarchy)
												{
													this.Police.CorpseList[this.Police.Corpses] = this.Ragdoll;
													this.Police.Corpses++;

													GameObjectUtils.SetLayerRecursively(this.gameObject, 11);
													this.tag = "Blood";

													this.Ragdoll.ChokingAnimation = true;
													this.Ragdoll.Disturbing = true;
													this.Ragdoll.Choking = true;
													this.Dying = true;

													this.MurderSuicidePhase = 100;
													this.SpawnAlarmDisc();

													this.Chopsticks[0].SetActive(false);
													this.Chopsticks[1].SetActive(false);
													this.Bento.SetActive(false);
												}
											}

											if (this.CharacterAnimation[this.PoisonDeathAnim].time >=
												this.CharacterAnimation[this.PoisonDeathAnim].length)
											{
												this.BecomeRagdoll();
												this.DeathType = DeathType.Poison;
												this.Ragdoll.Choking = false;

												if (this.StudentManager.Students[1].SenpaiWitnessingRivalDie)
												{
													this.Ragdoll.Prompt.Hide();
													this.Ragdoll.Prompt.enabled = false;
												}
											}
										}
										// Sedation
										else if (this.Sedated)
										{
											if (!this.Private)
											{
												this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
												this.CharacterAnimation.CrossFade(this.HeadacheAnim);
												this.CanTalk = false;
												this.Private = true;
											}

											if (this.CharacterAnimation[this.HeadacheAnim].time >=
												this.CharacterAnimation[this.HeadacheAnim].length)
											{
												this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;

												if (this.Male)
												{
													this.SprintAnim = AnimNames.MaleHeadacheWalk;
													this.RelaxAnim = "infirmaryRest_00";
												}
												else
												{
													this.SprintAnim = AnimNames.FemaleHeadacheWalk;
													this.RelaxAnim = "f02_infirmaryRest_00";
												}

												this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;

												this.CharacterAnimation.CrossFade(this.SprintAnim);
												this.DistanceToDestination = 100.0f;
												this.Pathfinding.canSearch = true;
												this.Pathfinding.canMove = true;
												this.Pathfinding.speed = 2.0f;
												this.MyBento.Tampered = false;
												this.Distracted = true;
												this.Private = true;

												ScheduleBlock newBlock4 = this.ScheduleBlocks[4];
												newBlock4.destination = "InfirmaryBed";
												newBlock4.action = "Relax";

												this.GetDestinations();

												//This is only called when a character has been sedated.

												this.CurrentDestination = this.Destinations[this.Phase];
												this.Pathfinding.target = this.Destinations[this.Phase];

												this.Chopsticks[0].SetActive(false);
												this.Chopsticks[1].SetActive(false);
												this.Bento.SetActive(false);
											}
										}
										// Headache
										else if (this.Headache)
										{
											if (!this.Private)
											{
												this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
												this.CharacterAnimation.CrossFade(this.HeadacheAnim);
												this.CanTalk = false;
												this.Private = true;
											}

											if (this.CharacterAnimation[this.HeadacheAnim].time >=
												this.CharacterAnimation[this.HeadacheAnim].length)
											{
												this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;

												if (this.Male)
												{
													this.SprintAnim = AnimNames.MaleHeadacheWalk;
													this.IdleAnim = AnimNames.MaleHeadacheIdle;
												}
												else
												{
													this.SprintAnim = AnimNames.FemaleHeadacheWalk;
													this.IdleAnim = AnimNames.FemaleHeadacheIdle;
												}

												this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;

												this.CharacterAnimation.CrossFade(this.SprintAnim);
												this.DistanceToDestination = 100.0f;
												this.Pathfinding.canSearch = true;
												this.Pathfinding.canMove = true;
												this.Pathfinding.speed = 2.0f;
												this.MyBento.Tampered = false;
												this.SeekingMedicine = true;
												this.Routine = false;
												this.Private = true;

												this.Chopsticks[0].SetActive(false);
												this.Chopsticks[1].SetActive(false);
												this.Bento.SetActive(false);
											}
										}
									}
								}
							}
							// Change Shoes.
							else if (this.Actions[this.Phase] == StudentActionType.ChangeShoes)
							{
								if (this.MeetTime == 0.0f)
								{
									if (this.StudentID == 1 &&
										!this.StudentManager.LoveManager.ConfessToSuitor && 
										this.StudentManager.LoveManager.LeftNote ||
										this.StudentID == this.StudentManager.LoveManager.SuitorID &&
										this.StudentManager.LoveManager.ConfessToSuitor &&
										this.StudentManager.LoveManager.LeftNote)
									{
										this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;

										this.CharacterAnimation.CrossFade(AnimNames.MaleKeepNote);

										this.Pathfinding.canSearch = false;
										this.Pathfinding.canMove = false;

										this.Confessing = true;
										this.CanTalk = false;
										this.Routine = false;
									}
									else
									{
										this.SmartPhone.SetActive(false);

										this.Pathfinding.canSearch = false;
										this.Pathfinding.canMove = false;
										this.ShoeRemoval.enabled = true;

										this.CanTalk = false;
										this.Routine = false;

										this.ShoeRemoval.LeavingSchool();
									}
								}
								else
								{
									this.CharacterAnimation.CrossFade(this.IdleAnim);
								}
							}
							// Grade Papers.
							else if (this.Actions[this.Phase] == StudentActionType.GradePapers)
							{
								this.CharacterAnimation.CrossFade(AnimNames.FemaleDeskWrite);
								this.GradingPaper.Writing = true;
								this.Obstacle.enabled = true;
								this.Pen.SetActive(true);
							}
							// Patrol.
							else if (this.Actions[this.Phase] == StudentActionType.Patrol)
							{
								this.PatrolTimer += Time.deltaTime * this.CharacterAnimation[this.PatrolAnim].speed;

								this.CharacterAnimation.CrossFade(this.PatrolAnim);

								if (this.PatrolTimer >= this.CharacterAnimation[this.PatrolAnim].length)
								{
									this.PatrolID++;

									if (this.PatrolID == this.StudentManager.Patrols.List[this.StudentID].childCount)
									{
										this.PatrolID = 0;
									}

									this.CurrentDestination = this.StudentManager.Patrols.List[this.StudentID].GetChild(this.PatrolID);
									this.Pathfinding.target = this.CurrentDestination;

									//Midori
									//if (this.StudentID == 39)
									//{
										//this.CharacterAnimation[AnimNames.FemaleTopHalfTexting].weight = 1.0f;
									//}

									this.PatrolTimer = 0.0f;
								}

								if (this.Restless)
								{
									this.SewTimer += Time.deltaTime;

									if (this.SewTimer > 20)
									{
										this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;

										ScheduleBlock block = this.ScheduleBlocks[this.Phase];
										block.destination = "Sketch";
										block.action = "Sketch";

										this.GetDestinations();

										this.CurrentDestination = this.SketchPosition;
										this.Pathfinding.target = this.SketchPosition;

										this.SewTimer = 0;
									}
								}
							}
							// Read.
							else if (this.Actions[this.Phase] == StudentActionType.Read)
							{
								if (this.ReadPhase == 0)
								{
									if (this.StudentID == 5)
									{
										this.HorudaCollider.gameObject.SetActive(true);
									}

									this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
									this.CharacterAnimation.CrossFade(this.BookSitAnim);

									if (this.CharacterAnimation[this.BookSitAnim].time >
										this.CharacterAnimation[this.BookSitAnim].length)
									{
										this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
										this.CharacterAnimation.CrossFade(this.BookReadAnim);
										this.ReadPhase++;
									}
									else if (this.CharacterAnimation[this.BookSitAnim].time > 1.0f)
									{
										this.OccultBook.SetActive(true);
									}
								}
							}
							// Texting.
							else if (this.Actions[this.Phase] == StudentActionType.Texting)
							{
								this.CharacterAnimation.CrossFade(AnimNames.FemaleMidoriTexting);

								if (this.SmartPhone.transform.localPosition.x != .02f)
								{
									this.SmartPhone.transform.localPosition = new Vector3(.02f, -.0075f, 0);
									this.SmartPhone.transform.localEulerAngles = new Vector3(0, -160, -164);
								}

								if (!this.SmartPhone.activeInHierarchy)
								{
									if (this.transform.position.y > 11.0f)
									{
										this.SmartPhone.SetActive(true);
									}
								}
							}
							// Mourn.
							else if (this.Actions[this.Phase] == StudentActionType.Mourn)
							{
								this.CharacterAnimation.CrossFade(AnimNames.FemaleBrokenSit);
							}
							// Cuddle.
							else if (this.Actions[this.Phase] == StudentActionType.Cuddle)
							{
								if (Vector3.Distance(this.transform.position, this.Partner.transform.position) < 1.0f)
								{
									// [af] This is the (unintuitive) Unity 5.3 way of changing emission.
									ParticleSystem.EmissionModule heartsEmission = this.Hearts.emission;

									if (!heartsEmission.enabled)
									{
										heartsEmission.enabled = true;

										if (!this.Male)
										{
											this.Cosmetic.MyRenderer.materials[2].SetFloat("_BlendAmount", 1.0f);
										}
										else
										{
											this.Cosmetic.MyRenderer.materials[this.Cosmetic.FaceID].SetFloat("_BlendAmount", 1.0f);
										}
									}

									this.CharacterAnimation.CrossFade(this.CuddleAnim);
								}
								else
								{
									this.CharacterAnimation.CrossFade(this.IdleAnim);
								}
							}
							// Teaching.
							else if (this.Actions[this.Phase] == StudentActionType.Teaching)
							{
								//If class hasn't actually begun yet...
								if (this.Clock.Period != 2 && this.Clock.Period != 4)
								{
									this.CharacterAnimation.CrossFade(AnimNames.TeacherPodium);
								}
								else
								{
									if (!this.SpeechLines.isPlaying)
									{
										this.SpeechLines.Play();
									}

									this.CharacterAnimation.CrossFade(this.TeachAnim);
									//this.Teaching = true;
								}
							}
							// Search Patroling.
							else if (this.Actions[this.Phase] == StudentActionType.SearchPatrol)
							{
								if (this.PatrolID == 0)
								{
									if (this.StudentManager.CommunalLocker.RivalPhone.gameObject.activeInHierarchy)
									{
										if (!this.EndSearch)
										{
											//If this student is Osana...
											if (this.StudentID == this.StudentManager.RivalID)
											{
												//If we're waiting for Osana to find her phone...
												if (SchemeGlobals.GetSchemeStage(1) == 7)
												{
													SchemeGlobals.SetSchemeStage(1, 8);
													this.Yandere.PauseScreen.Schemes.UpdateInstructions();
												}
											}

											this.CharacterAnimation.CrossFade (this.DiscoverPhoneAnim);
											this.Subtitle.UpdateLabel(this.LostPhoneSubtitleType, 2, 5.0f);
											this.EndSearch = true;
											this.Routine = false;
										}
									}
								}

								if (!this.EndSearch)
								{
									this.PatrolTimer += Time.deltaTime * this.CharacterAnimation[this.PatrolAnim].speed;

									this.CharacterAnimation.CrossFade(this.SearchPatrolAnim);

									if (this.PatrolTimer >= this.CharacterAnimation[this.SearchPatrolAnim].length)
									{
										this.PatrolID++;

										if (this.PatrolID == this.StudentManager.SearchPatrols.List[this.Class].childCount)
										{
											this.PatrolID = 0;
										}

										this.CurrentDestination =
											this.StudentManager.SearchPatrols.List[this.Class].GetChild(this.PatrolID);
										this.Pathfinding.target = this.CurrentDestination;
										this.DistanceToDestination = 100.0f;

										//Midori
										//if (this.StudentID == 39)
										//{
											//this.CharacterAnimation[AnimNames.FemaleTopHalfTexting].weight = 1.0f;
										//}

										this.PatrolTimer = 0.0f;
									}
								}
							}
							// Wait. (Musume leaning up against the wall behind the school.)
							else if (this.Actions[this.Phase] == StudentActionType.Wait)
							{
								if (this.Cigarette.active == false)
								{
									if (TaskGlobals.GetTaskStatus(81) == 3)
									{
										this.WaitAnim = AnimNames.FemaleSmokeAttempt;

										this.SmartPhone.SetActive(false);
										this.Cigarette.SetActive(true);
										this.Lighter.SetActive(true);
									}
								}

								this.CharacterAnimation.CrossFade(this.WaitAnim);
							}
							// Clean.
							else if (this.Actions[this.Phase] == StudentActionType.Clean)
							{
								this.CleanTimer += Time.deltaTime;

								if (this.CleaningRole == 5)
								{
									if (this.CleanID == 0)
									{
										this.CharacterAnimation.CrossFade(this.CleanAnims[1]);
									}
									else
									{
										if (!this.StudentManager.RoofFenceUp)
										{
											this.Prompt.Label[0].text = "     " + "Push";
											this.Prompt.HideButton[0] = false;
											this.Pushable = true;
										}

										this.CharacterAnimation.CrossFade(this.CleanAnims[this.CleaningRole]);

										if (this.CleanTimer >= 1.166666 && this.CleanTimer <= 6.166666)
										{
											if (!this.ChalkDust.isPlaying)
											{
												this.ChalkDust.Play();
											}
										}
									}
								}
								//Scrubbing Toilet
								else if (this.CleaningRole == 4)
								{
									this.CharacterAnimation.CrossFade(this.CleanAnims[this.CleaningRole]);

									if (!this.Drownable)
									{
										this.Prompt.Label[0].text = "     " + "Drown";
										this.Prompt.HideButton[0] = false;
										this.Drownable = true;
									}
								}
								else
								{
									this.CharacterAnimation.CrossFade(this.CleanAnims[this.CleaningRole]);
								}

								if (this.CleanTimer >= this.CharacterAnimation[this.CleanAnims[this.CleaningRole]].length)
								{
									this.CleanID++;

									if (this.CleanID == this.CleaningSpot.childCount)
									{
										this.CleanID = 0;
									}

									this.CurrentDestination = this.CleaningSpot.GetChild(this.CleanID);
									this.Pathfinding.target = this.CurrentDestination;

									this.DistanceToDestination = 100;
									this.CleanTimer = 0.0f;

									if (this.Pushable)
									{
										this.Prompt.Label[0].text = "     Talk";
										this.Pushable = false;
									}

									if (this.Drownable)
									{
										this.Drownable = false;
										this.StudentManager.UpdateMe(this.StudentID);
									}
								}
							}
							// Graffiti.
							else if (this.Actions[this.Phase] == StudentActionType.Graffiti)
							{
								if (this.KilledMood)
								{
									this.Subtitle.UpdateLabel(SubtitleType.KilledMood, 0, 5.0f);

									this.GraffitiPhase = 4;
									this.KilledMood = false;
								}

								if (this.GraffitiPhase == 0)
								{
									AudioSource.PlayClipAtPoint(this.BullyGiggles[Random.Range(0, this.BullyGiggles.Length)], this.Head.position);
									this.CharacterAnimation.CrossFade("f02_bullyDesk_00");
									this.SmartPhone.SetActive(false);
									this.GraffitiPhase++;
								}
								else if (this.GraffitiPhase == 1)
								{
									if (this.CharacterAnimation["f02_bullyDesk_00"].time >= 8)
									{
										this.StudentManager.Graffiti[this.BullyID].SetActive (true);
										this.GraffitiPhase++;
									}
								}
								else if (this.GraffitiPhase == 2)
								{
									if (this.CharacterAnimation["f02_bullyDesk_00"].time >= 9.66666f)
									{
										AudioSource.PlayClipAtPoint(this.BullyGiggles[Random.Range(0, this.BullyGiggles.Length)], this.Head.position);
										this.GraffitiPhase++;
									}
								}
								else if (this.GraffitiPhase == 3)
								{
									if (this.CharacterAnimation["f02_bullyDesk_00"].time >= this.CharacterAnimation["f02_bullyDesk_00"].length)
									{
										this.GraffitiPhase++;
									}
								}
								else if (this.GraffitiPhase == 4)
								{
									this.DistanceToDestination = 100;

									ScheduleBlock newBlock2 = this.ScheduleBlocks[2];
									newBlock2.destination = "Patrol";
									newBlock2.action = "Patrol";

									this.GetDestinations();

									//This is only called after a student finishes spraying graffiti.

									this.CurrentDestination = this.Destinations[this.Phase];
									this.Pathfinding.target = this.Destinations[this.Phase];
									this.SmartPhone.SetActive(true);
								}
							}
							// Bullying.
							else if (this.Actions[this.Phase] == StudentActionType.Bully)
							{
                                this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;

                                if (this.StudentManager.Students[this.StudentManager.VictimID] != null)
								{
									if (this.StudentManager.Students[this.StudentManager.VictimID].Distracted ||
										this.StudentManager.Students[this.StudentManager.VictimID].Tranquil)
									{
										this.StudentManager.NoBully[this.BullyID] = true;
										this.KilledMood = true;
									}

									if (this.KilledMood)
									{
										this.Subtitle.UpdateLabel(SubtitleType.KilledMood, 0, 5.0f);

										this.BullyPhase = 4;
										this.KilledMood = false;
										this.BullyDust.Stop();
									}

									if (this.StudentManager.Students[81] == null)
									{
										ScheduleBlock newBlock4 = this.ScheduleBlocks[4];
										newBlock4.destination = "Patrol";
										newBlock4.action = "Patrol";

										this.GetDestinations();

										//This is only called if the bully leader isn't present during a bullying event.

										this.CurrentDestination = this.Destinations[this.Phase];
										this.Pathfinding.target = this.Destinations[this.Phase];
									}
									else
									{
                                        this.SmartPhone.SetActive(false);

										if (this.BullyID == 1)
										{
											if (this.BullyPhase == 0)
											{
												this.Scrubber.GetComponent<Renderer>().material.mainTexture = this.Eraser.GetComponent<Renderer>().material.mainTexture;
												this.Scrubber.SetActive(true);
												this.Eraser.SetActive(true);

												this.StudentManager.Students[this.StudentManager.VictimID].CharacterAnimation.CrossFade(
													this.StudentManager.Students[this.StudentManager.VictimID].BullyVictimAnim);
												this.StudentManager.Students[this.StudentManager.VictimID].Routine = false;
												this.CharacterAnimation.CrossFade("f02_bullyEraser_00");
												this.BullyPhase++;
											}
											else if (this.BullyPhase == 1)
											{
												if (this.CharacterAnimation["f02_bullyEraser_00"].time >= .833333f)
												{
													this.BullyDust.Play();
													this.BullyPhase++;
												}
											}
											else if (this.BullyPhase == 2)
											{
												if (this.CharacterAnimation["f02_bullyEraser_00"].time >= this.CharacterAnimation["f02_bullyEraser_00"].length)
												{
													AudioSource.PlayClipAtPoint(this.BullyLaughs[this.BullyID], this.Head.position);
													this.CharacterAnimation.CrossFade("f02_bullyLaugh_00");
													this.Scrubber.SetActive(false);
													this.Eraser.SetActive(false);
													this.BullyPhase++;
												}
											}
											else if (this.BullyPhase == 3)
											{
												if (this.CharacterAnimation["f02_bullyLaugh_00"].time >= this.CharacterAnimation["f02_bullyLaugh_00"].length)
												{
													this.BullyPhase++;
												}
											}
											else if (this.BullyPhase == 4)
											{
                                                this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;

                                                this.StudentManager.Students[this.StudentManager.VictimID].Routine = true;

												ScheduleBlock newBlock4 = this.ScheduleBlocks[4];
												newBlock4.destination = "LunchSpot";
												newBlock4.action = "Wait";

												this.GetDestinations();

												//This is only called after a bullying event has ended.

												this.CurrentDestination = this.Destinations[this.Phase];
												this.Pathfinding.target = this.Destinations[this.Phase];
												this.SmartPhone.SetActive(true);
												this.Scrubber.SetActive(false);
												this.Eraser.SetActive(false);
											}
										}
										else
										{
											if (this.StudentManager.Students[81].BullyPhase < 2)
											{
												if (this.GiggleTimer == 0)
												{
													AudioSource.PlayClipAtPoint(this.BullyGiggles[Random.Range(0, this.BullyGiggles.Length)], this.Head.position);
													this.GiggleTimer = 5;
												}

												this.GiggleTimer = Mathf.MoveTowards(this.GiggleTimer, 0, Time.deltaTime);

												this.CharacterAnimation.CrossFade("f02_bullyGiggle_00");
											}
											else if (this.StudentManager.Students[81].BullyPhase < 4)
											{
												if (this.LaughTimer == 0)
												{
													AudioSource.PlayClipAtPoint(this.BullyLaughs[this.BullyID], this.Head.position);
													this.LaughTimer = 5;
												}

												this.LaughTimer = Mathf.MoveTowards(this.LaughTimer, 0, Time.deltaTime);

												this.CharacterAnimation.CrossFade("f02_bullyLaugh_00");
											}

											if (this.CharacterAnimation["f02_bullyLaugh_00"].time >= this.CharacterAnimation["f02_bullyLaugh_00"].length ||
												this.StudentManager.Students[81].BullyPhase == 4 ||
												this.BullyPhase == 4)
											{
                                                this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;

                                                this.DistanceToDestination = 100;

												ScheduleBlock newBlock4 = this.ScheduleBlocks[4];
												newBlock4.destination = "Patrol";
												newBlock4.action = "Patrol";

												this.GetDestinations();

												//This is only called after a bullying event has ended.

												this.CurrentDestination = this.Destinations[this.Phase];
												this.Pathfinding.target = this.Destinations[this.Phase];
												this.SmartPhone.SetActive(true);
											}
										}
									}
								}
								else
								{
                                    this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;

                                    this.DistanceToDestination = 100;

									ScheduleBlock newBlock4 = this.ScheduleBlocks[4];
									newBlock4.destination = "Patrol";
									newBlock4.action = "Patrol";

									this.GetDestinations();

									//This is only called if a bully's victim disappears.

									this.CurrentDestination = this.Destinations[this.Phase];
									this.Pathfinding.target = this.Destinations[this.Phase];
									this.SmartPhone.SetActive(true);
								}
							}
							// Following.
							else if (this.Actions[this.Phase] == StudentActionType.Follow)
							{
								//Debug.Log ("Raibaru-specific code.");

								//Debug.Log("Following, and at destination.");

								if (this.FollowTarget.Routine && !this.FollowTarget.InEvent &&
									this.FollowTarget.CurrentAction == StudentActionType.Clean &&
									this.FollowTarget.DistanceToDestination < 1)
								{
									//this.EquipCleaningItems();

									//Debug.Log("Raibaru is cleaning.");

									this.CharacterAnimation.CrossFade(this.CleanAnims[this.CleaningRole]);
									this.Scrubber.SetActive(true);
								}
								else if (this.FollowTarget.Routine && !this.FollowTarget.InEvent &&
										!this.FollowTarget.Meeting &&
										this.FollowTarget.CurrentAction == StudentActionType.Socializing &&
										this.FollowTarget.DistanceToDestination < 1)
								{
									//Debug.Log("Raibaru is socializing with Osana.");

									if (this.FollowTarget.Alone || this.FollowTarget.Meeting)
									{
										this.CharacterAnimation.CrossFade(this.IdleAnim);
										this.SpeechLines.Stop();
									}
									else
									{
										this.Scrubber.SetActive(false);
										this.SpeechLines.Play();

										this.CharacterAnimation.CrossFade(this.RandomAnim);

										if (this.CharacterAnimation[this.RandomAnim].time >=
											this.CharacterAnimation[this.RandomAnim].length)
										{
											this.PickRandomAnim();
										}
									}
								}
								else if (this.FollowTarget.Routine && !this.FollowTarget.InEvent &&
									this.FollowTarget.CurrentAction == StudentActionType.SitAndTakeNotes &&
									this.FollowTarget.DistanceToDestination < 1)
								{
									Debug.Log("Raibaru just changed her destination to class.");

									ScheduleBlock CurrentBlock = this.ScheduleBlocks[this.Phase];
									CurrentBlock.destination = "Seat";
									CurrentBlock.action = "SitAndTakeNotes";

									this.Actions[this.Phase] = StudentActionType.SitAndTakeNotes;
									CurrentAction = StudentActionType.SitAndTakeNotes;

									this.GetDestinations();

									this.CurrentDestination = this.Destinations[this.Phase];
									this.Pathfinding.target = this.Destinations[this.Phase];
								}
								else if (this.FollowTarget.Routine && !this.FollowTarget.InEvent &&
									this.FollowTarget.CurrentAction == StudentActionType.SitAndEatBento &&
									this.FollowTarget.DistanceToDestination < 1)
								{
									Debug.Log("Raibaru just changed her destination to lunch.");

									ScheduleBlock CurrentBlock = this.ScheduleBlocks[this.Phase];
									CurrentBlock.destination = "LunchSpot";
									CurrentBlock.action = "SitAndEatBento";

									this.Actions[this.Phase] = StudentActionType.SitAndEatBento;
									CurrentAction = StudentActionType.SitAndEatBento;

									this.GetDestinations();

									this.CurrentDestination = this.Destinations[this.Phase];
									this.Pathfinding.target = this.Destinations[this.Phase];
								}
								else if (this.FollowTarget.Routine && !this.FollowTarget.InEvent &&
									this.FollowTarget.Phase == 8 && this.FollowTarget.DistanceToDestination < 1)
								{
									Debug.Log("Raibaru just changed her destination to the lockers.");

									ScheduleBlock CurrentBlock = this.ScheduleBlocks[this.Phase];
									CurrentBlock.destination = "Locker";
									CurrentBlock.action = "Shoes";

									this.Actions[this.Phase] = StudentActionType.ChangeShoes;
									CurrentAction = StudentActionType.ChangeShoes;

									this.GetDestinations();

									this.CurrentDestination = this.Destinations[this.Phase];
									this.Pathfinding.target = this.Destinations[this.Phase];
								}
								else if (this.StudentManager.LoveManager.RivalWaiting &&
									this.FollowTarget.transform.position.x > 40 && 
									this.FollowTarget.DistanceToDestination < 1)
								{
									Debug.Log("Raibaru just changed her destination to the bush near the matchmaking spot.");

									this.CurrentDestination = this.StudentManager.LoveManager.FriendWaitSpot;
									this.Pathfinding.target = this.StudentManager.LoveManager.FriendWaitSpot;

									this.CharacterAnimation.CrossFade(this.IdleAnim);
								}
								else if (this.FollowTarget.ConfessPhase == 5)
								{
									Debug.Log("Raibaru just changed her action to Sketch and her destination to Paint.");

									ScheduleBlock CurrentBlock = this.ScheduleBlocks[this.Phase];
									CurrentBlock.destination = "Paint";
									CurrentBlock.action = "Sketch";
									CurrentBlock.time = 99;

									this.GetDestinations();

									this.CurrentDestination = this.Destinations[this.Phase];
									this.Pathfinding.target = this.Destinations[this.Phase];

									this.MyController.radius = .1f;
								}
								else
								{
									this.CharacterAnimation.CrossFade(this.IdleAnim);
									this.SpeechLines.Stop();

									if (this.SlideIn)
									{
										this.MoveTowardsTarget(this.CurrentDestination.position);
									}
								}
							}
							// Sulking.
							else if (this.Actions[this.Phase] == StudentActionType.Sulk)
							{
								if (this.Male)
								{
									this.CharacterAnimation.CrossFade(AnimNames.MaleSulk);
								}
								else
								{
									this.CharacterAnimation.CrossFade("f02_railingSulk_0" + SulkPhase, 1);

									this.SulkTimer += Time.deltaTime;

									if (this.SulkTimer > 7.66666f)
									{
										this.SulkTimer = 0;
										this.SulkPhase++;

										if (this.SulkPhase == 3)
										{
											this.SulkPhase = 0;
										}
									}
								}
							}
							// Sleuthing.
							else if (this.Actions[this.Phase] == StudentActionType.Sleuth)
							{
								if (this.StudentManager.SleuthPhase != 3)
								{
									this.StudentManager.ConvoManager.CheckMe(StudentID);

									if (this.Alone)
									{
										if (this.Male){this.CharacterAnimation.CrossFade(AnimNames.MaleTexting);}
										else {this.CharacterAnimation.CrossFade(AnimNames.FemaleTexting);}

										if (this.Male)
										{
											this.SmartPhone.transform.localPosition = new Vector3(0.025f, 0.0025f, 0.025f);
											this.SmartPhone.transform.localEulerAngles = new Vector3(0, -160.0f, 180);
										}
										else
										{
											this.SmartPhone.transform.localPosition = new Vector3(0.01f, 0.01f, 0.01f);
											this.SmartPhone.transform.localEulerAngles = new Vector3(0, -160.0f, 165);
										}

										this.SmartPhone.SetActive(true);
										this.SpeechLines.Stop();
									}
									else
									{
										if (!this.SpeechLines.isPlaying)
										{
											this.SmartPhone.SetActive(false);
											this.SpeechLines.Play();
										}

										this.CharacterAnimation.CrossFade(this.RandomSleuthAnim, 1);

										if (this.CharacterAnimation[this.RandomSleuthAnim].time >=
											this.CharacterAnimation[this.RandomSleuthAnim].length)
										{
											this.PickRandomSleuthAnim();
										}

										this.StudentManager.SleuthTimer += Time.deltaTime;

										if (this.StudentManager.SleuthTimer > 100)
										{
											this.StudentManager.SleuthTimer = 0;
											this.StudentManager.UpdateSleuths();
										}
									}
								}
								// Walk to each student in school.
								else
								{
									this.CharacterAnimation.CrossFade(this.SleuthScanAnim);

									if (this.CharacterAnimation[this.SleuthScanAnim].time >= this.CharacterAnimation[this.SleuthScanAnim].length)
									{
										this.GetSleuthTarget();
									}
								}
							}
							// Stalking.
							else if (this.Actions[this.Phase] == StudentActionType.Stalk)
							{
								this.CharacterAnimation.CrossFade(this.SleuthIdleAnim);

								if (this.DistanceToPlayer < 5)
								{
									if (Vector3.Distance(this.transform.position, this.StudentManager.FleeSpots[0].position) > 10)
									{
										this.Pathfinding.target = this.StudentManager.FleeSpots[0];
										this.CurrentDestination = this.StudentManager.FleeSpots[0];
									}
									else
									{
										this.Pathfinding.target = this.StudentManager.FleeSpots[1];
										this.CurrentDestination = this.StudentManager.FleeSpots[1];
									}

									//Debug.Log("Sprinting 7");
									this.Pathfinding.speed = 4;
									this.StalkerFleeing = true;
								}
							}
							// Sketching.
							else if (this.Actions[this.Phase] == StudentActionType.Sketch)
							{
								this.CharacterAnimation.CrossFade(this.SketchAnim);
								this.Sketchbook.SetActive(true);
								this.Pencil.SetActive(true);

								if (this.Restless)
								{
									this.SewTimer += Time.deltaTime;

									if (this.SewTimer > 20)
									{
										this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;

										ScheduleBlock block = this.ScheduleBlocks[this.Phase];
										block.destination = "Patrol";
										block.action = "Patrol";

										this.GetDestinations();

										this.EmptyHands();

										this.PatrolID = 1;
										this.PatrolTimer = 0;
										this.CharacterAnimation[this.PatrolAnim].time = 0;
										this.CurrentDestination = this.StudentManager.Patrols.List[this.StudentID].GetChild(this.PatrolID);
										this.Pathfinding.target = this.CurrentDestination;

										this.SewTimer = 0;
									}
								}
							}
							// Sunbathing.
							else if (this.Actions[this.Phase] == StudentActionType.Sunbathe)
							{
								// After reaching the communal locker...
								if (this.SunbathePhase == 0)
								{
									this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
									this.StudentManager.CommunalLocker.Open = true;
									this.StudentManager.CommunalLocker.SpawnSteamNoSideEffects(this);
									this.MustChangeClothing = true;
									this.ChangeClothingPhase++;
									this.SunbathePhase++;
								}
								// After the steam fades...
								else if (this.SunbathePhase == 1)
								{
									this.CharacterAnimation.CrossFade(this.StripAnim);

									this.Pathfinding.canSearch = false;
									this.Pathfinding.canMove = false;

									if (this.CharacterAnimation[this.StripAnim].time >= 1.50f)
									{
										if (this.Schoolwear != 2)
										{
											this.Schoolwear = 2;
											this.ChangeSchoolwear();
										}

										if (this.CharacterAnimation[this.StripAnim].time > this.CharacterAnimation[this.StripAnim].length)
										{
											this.Pathfinding.canSearch = true;
											this.Pathfinding.canMove = true;

											this.Stripping = false;

											if (!this.StudentManager.CommunalLocker.SteamCountdown)
											{
												this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;

												this.Destinations[this.Phase] = this.StudentManager.SunbatheSpots[this.StudentID];
												this.Pathfinding.target = this.StudentManager.SunbatheSpots[this.StudentID];
												this.CurrentDestination = this.StudentManager.SunbatheSpots[this.StudentID];
												this.StudentManager.CommunalLocker.Student = null;

												this.SunbathePhase++;
											}
										}
									}
								}
								//After reaching the sunbathing position...
								else if (this.SunbathePhase == 2)
								{
									this.MyRenderer.updateWhenOffscreen = true;

									this.CharacterAnimation.CrossFade("f02_sunbatheStart_00");
									this.SmartPhone.SetActive(false);

									if (this.CharacterAnimation["f02_sunbatheStart_00"].time >= this.CharacterAnimation["f02_sunbatheStart_00"].length)
									{
										this.MyController.radius = 0;
										this.SunbathePhase++;
									}
								}
								//After finishing the lay-down animation...
								else if (this.SunbathePhase == 3)
								{
									this.CharacterAnimation.CrossFade("f02_sunbatheLoop_00");
								}
							}
							// Shocked
							else if (this.Actions[this.Phase] == StudentActionType.Shock)
							{
								if (this.StudentManager.Students[36] == null)
								{
									this.Phase++;
								}
								else if (this.StudentManager.Students[36].Routine &&
									     this.StudentManager.Students[36].DistanceToDestination < 1)
								{
									if (!this.StudentManager.GamingDoor.Open)
									{
										this.StudentManager.GamingDoor.OpenDoor();
									}

									ParticleSystem.EmissionModule heartsEmission = this.Hearts.emission;

									if (this.SmartPhone.activeInHierarchy)
									{
										this.Cosmetic.MyRenderer.materials[2].SetFloat("_BlendAmount", 1.0f);
										this.SmartPhone.SetActive(false);
										this.MyController.radius = 0;

										heartsEmission.rateOverTime = 5;
										heartsEmission.enabled = true;
										this.Hearts.Play();
									}

									this.CharacterAnimation.CrossFade("f02_peeking_0" + (this.StudentID - 80));
								}
								else
								{
									this.CharacterAnimation.CrossFade(this.PatrolAnim);

									if (!this.SmartPhone.activeInHierarchy)
									{
										this.SmartPhone.SetActive(true);
										this.MyController.radius = .1f;

											 if (BullyID == 2){this.MyController.Move(this.transform.right *  1 * Time.timeScale * .2f);}
										else if (BullyID == 3){this.MyController.Move(this.transform.right * -1 * Time.timeScale * .2f);}
										else if (BullyID == 4){this.MyController.Move(this.transform.right *  1 * Time.timeScale * .2f);}
										else if (BullyID == 5){this.MyController.Move(this.transform.right * -1 * Time.timeScale * .2f);}
									}
								}
							}
							// Miyuki AR Game
							else if (this.Actions[this.Phase] == StudentActionType.Miyuki)
							{
								if (this.StudentManager.MiyukiEnemy.Enemy.activeInHierarchy)
								{
									this.CharacterAnimation.CrossFade(this.MiyukiAnim);

									this.MiyukiTimer += Time.deltaTime;

									if (this.MiyukiTimer > 1)
									{
										this.MiyukiTimer = 0;
										this.Miyuki.Shoot();
									}
								}
								else
								{
									this.CharacterAnimation.CrossFade(this.VictoryAnim);
								}
							}
							// Club Meeting
							else if (this.Actions[this.Phase] == StudentActionType.Meeting)
							{
								if (StudentID != 36)
								{
									this.StudentManager.Meeting = true;

									if (this.StudentManager.Speaker == this.StudentID)
									{
										if (!this.SpeechLines.isPlaying)
										{
											this.CharacterAnimation.CrossFade(this.RandomAnim);
											this.SpeechLines.Play();
										}
									}
									else
									{
										this.CharacterAnimation.CrossFade(this.IdleAnim);

										if (this.SpeechLines.isPlaying)
										{
											this.SpeechLines.Stop();
										}
									}
								}
								//If we're Gema...
								else
								{
									this.CharacterAnimation.CrossFade(this.PeekAnim);
								}
							}
							// Lyrics
							else if (this.Actions[this.Phase] == StudentActionType.Lyrics)
							{
								this.LyricsTimer += Time.deltaTime;

								if (this.LyricsPhase == 0)
								{
									this.CharacterAnimation.CrossFade("f02_writingLyrics_00");

									if (!this.Pencil.activeInHierarchy)
									{
										this.Pencil.SetActive(true);
									}

									if (this.LyricsTimer > 18)
									{
										this.StudentManager.LyricsSpot.position = this.StudentManager.AirGuitarSpot.position;
										this.StudentManager.LyricsSpot.eulerAngles = this.StudentManager.AirGuitarSpot.eulerAngles;

										this.Pencil.SetActive(false);
										this.LyricsPhase = 1;
										this.LyricsTimer = 0;
									}
								}
								else
								{
									this.CharacterAnimation.CrossFade("f02_airGuitar_00");

									if (!this.AirGuitar.isPlaying)
									{
										this.AirGuitar.Play();
									}

									if (this.LyricsTimer > 18)
									{
										this.StudentManager.LyricsSpot.position = this.StudentManager.OriginalLyricsSpot.position;
										this.StudentManager.LyricsSpot.eulerAngles = this.StudentManager.OriginalLyricsSpot.eulerAngles;

										this.AirGuitar.Stop();
										this.LyricsPhase = 0;
										this.LyricsTimer = 0;
									}
								}
							}
							// Sew
							else if (this.Actions[this.Phase] == StudentActionType.Sew)
							{
								this.CharacterAnimation.CrossFade("sewing_00");
								this.PinkSeifuku.SetActive(true);

								if (SewTimer < 10)
								{
									if (TaskGlobals.GetTaskStatus(8) == 3)
									{
										this.SewTimer += Time.deltaTime;

										if (SewTimer > 10)
										{
											Instantiate(Yandere.PauseScreen.DropsMenu.GetComponent<DropsScript>().InfoChanWindow.Drops[1], new Vector3(28.289f, .7718928f, 5.196f), Quaternion.identity);
										}
									}
								}
							}
							// Paint
							else if (this.Actions[this.Phase] == StudentActionType.Paint)
							{
								this.Painting.material.color += new Color(0, 0, 0, Time.deltaTime * .00066666f);

								this.CharacterAnimation.CrossFade(this.PaintAnim);
								this.Paintbrush.SetActive(true);
								this.Palette.SetActive(true);
							}
						}
						//If this character has been told to "Go Away"...(goaway)
						//GoAway
						else
						{
							this.CurrentDestination = this.StudentManager.GoAwaySpots.List[this.StudentID];
							this.Pathfinding.target = this.StudentManager.GoAwaySpots.List[this.StudentID];

							this.CharacterAnimation.CrossFade(this.IdleAnim);
							
							this.GoAwayTimer += Time.deltaTime;

							if (this.GoAwayTimer > 10.0f)
							{
								//This is only called after a character has spent 10 seconds standing in a "Go Away" spot.

								this.CurrentDestination = this.Destinations[this.Phase];
								this.Pathfinding.target = this.Destinations[this.Phase];

								this.GoAwayTimer = 0.0f;
								this.GoAway = false;
							}
						}
					}
					// If the character IS meeting somebody...
					else
					{
						if (this.MeetTimer == 0.0f)
						{
							if (this.Yandere.Bloodiness + this.Yandere.GloveBlood == 0 && this.Yandere.Sanity >= 66.66666)
							{
								if (this.CurrentDestination == this.StudentManager.MeetSpots.List[8] ||
									this.CurrentDestination == this.StudentManager.MeetSpots.List[9] ||
									this.CurrentDestination == this.StudentManager.MeetSpots.List[10])
								{
									//Osana
									if (this.StudentID == 11)
									{
										this.StudentManager.OsanaOfferHelp.UpdateLocation();
										this.StudentManager.OsanaOfferHelp.enabled = true;
									}
									//Kokona
									else if (this.StudentID == 30)
									{
										this.StudentManager.OfferHelp.UpdateLocation();
										this.StudentManager.OfferHelp.enabled = true;
									}
									//Fragile Girl
									else if (this.StudentID == 5)
									{
										this.Yandere.BullyPhotoCheck();

										if (this.Yandere.BullyPhoto)
										{
											this.StudentManager.FragileOfferHelp.UpdateLocation();
											this.StudentManager.FragileOfferHelp.enabled = true;
										}
									}
								}
							}

							if (!SchoolGlobals.RoofFence)
							{
								if (this.transform.position.y > 11.0f)
								{
									this.Prompt.Label[0].text = "     " + "Push";
									this.Prompt.HideButton[0] = false;
									this.Pushable = true;
								}
							}

							if (this.CurrentDestination == this.StudentManager.FountainSpot)
							{
								this.Prompt.Label[0].text = "     " + "Drown";
								this.Prompt.HideButton[0] = false;
								this.Drownable = true;
							}
						}

						this.CharacterAnimation.CrossFade(this.IdleAnim);

						this.MeetTimer += Time.deltaTime;

						if (this.MeetTimer > 60.0f)
						{
							if (!this.Male)
							{
								this.Subtitle.UpdateLabel(SubtitleType.NoteReaction, 4, 3.0f);
							}
							else
							{
								//Riku-specific
								if (this.StudentID == 28)
								{
									this.Subtitle.UpdateLabel(SubtitleType.NoteReactionMale, 6, 3.0f);
								}
								else
								{
									this.Subtitle.UpdateLabel(SubtitleType.NoteReactionMale, 4, 3.0f);
								}
							}

							while (this.Clock.HourTime >= this.ScheduleBlocks[this.Phase].time)
							{
								this.Phase++;
							}

							//This is only called after a student has spent 60 seconds waiting at a meeting location.

							this.CurrentDestination = this.Destinations[this.Phase];
							this.Pathfinding.target = this.Destinations[this.Phase];

							this.StopMeeting();
						}
					}
				}
			}
		}
		//If Routine is false...
		else
		{
			if (this.CurrentDestination != null)
			{
				this.DistanceToDestination = Vector3.Distance(
					this.transform.position, this.CurrentDestination.position);
			}

			//If we have witnessed a murder and have entered "Flee" protocol...
			if (this.Fleeing)
			{
				//If we're not dying or spraying...
				if (!this.Dying && !this.Spraying)
				{
					//If Yandere-chan is NOT currently being pinned down by anyone...
					if (!this.PinningDown)
					{
						if (this.Persona == PersonaType.Dangerous)
						{
							this.Yandere.Pursuer = this;

							Debug.Log("This student council member is running to intercept Yandere-chan.");

                            if (this.Yandere.Laughing)
                            {
                                this.Yandere.StopLaughing();
                                this.Yandere.Chased = true;
                                this.Yandere.CanMove = false;
                                //this.Yandere.CharacterAnimation.CrossFade(AnimNames.FemaleReadyToFight);
                            }

							if (this.StudentManager.CombatMinigame.Path > 3 && this.StudentManager.CombatMinigame.Path < 7)
							{
								this.ReturnToRoutine();
							}
						}

						/*
						if (this.Yandere.Chased)
						{
							this.Pathfinding.speed += Time.deltaTime;
						}
						*/

						if (this.Pathfinding.target != null)
						{
							this.DistanceToDestination = Vector3.Distance(
								this.transform.position, this.Pathfinding.target.position);
						}

						if (this.AlarmTimer > 0.0f)
						{
							//Debug.Log("Decreasing AlarmTimer here.");

							this.AlarmTimer = Mathf.MoveTowards(this.AlarmTimer, 0.0f, Time.deltaTime);

							if (this.StudentID == 1)
							{
								Debug.Log("Senpai entered his scared animation.");
							}

							this.CharacterAnimation.CrossFade(this.ScaredAnim);

							if (this.AlarmTimer == 0.0f)
							{
								this.WalkBack = false;
								this.Alarmed = false;
							}

							this.Pathfinding.canSearch = false;
							this.Pathfinding.canMove = false;

							if (this.WitnessedMurder)
							{
								this.targetRotation = Quaternion.LookRotation(new Vector3(
									this.Yandere.Hips.transform.position.x,
									this.transform.position.y,
									this.Yandere.Hips.transform.position.z) - this.transform.position);
								this.transform.rotation = Quaternion.Slerp(
									this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);
							}
							else if (this.WitnessedCorpse)
							{
								this.targetRotation = Quaternion.LookRotation(new Vector3(
									this.Corpse.AllColliders[0].transform.position.x,
									this.transform.position.y,
									this.Corpse.AllColliders[0].transform.position.z) - this.transform.position);
								this.transform.rotation = Quaternion.Slerp(
									this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);
							}
						}
						else
						{
							if (this.Persona == PersonaType.TeachersPet)
							{
								if (this.WitnessedMurder)
								{
									if (this.ReportPhase == 0)
									{
										if (this.StudentManager.Reporter == null)
										{
											if (!this.Police.Called)
											{
												Debug.Log(this.Name + " is setting their teacher as their destination at the beginning of Flee protocol.");

												this.Pathfinding.target = this.StudentManager.Teachers[this.Class].transform;
												this.CurrentDestination = this.StudentManager.Teachers[this.Class].transform;

												this.StudentManager.Reporter = this;
												this.ReportingMurder = true;

												this.DetermineCorpseLocation();
											}
										}
									}
								}
							}

							if (this.transform.position.y < -11.0f)
							{
								if (this.Persona != PersonaType.Coward &&
									this.Persona != PersonaType.Evil &&
									this.Persona != PersonaType.Fragile &&
									this.OriginalPersona != PersonaType.Evil)
								{
									this.Police.Witnesses--;
									this.Police.Show = true;

									if (this.Countdown.gameObject.activeInHierarchy)
									{
										this.PhoneAddictGameOver();
									}
								}

								this.transform.position = new Vector3(
									this.transform.position.x,
									-100,
									this.transform.position.z);
								
								this.gameObject.SetActive(false);
							}

							if (this.transform.position.z < -99.0f)
							{
								this.Prompt.Hide();
								this.Prompt.enabled = false;
								this.Safe = true;
							}

							if (this.DistanceToDestination > this.TargetDistance)
							{
								//Debug.Log(this.Name + " must run to a destination before they can continue their Flee() protocol.");

								if (!this.Phoneless)
								{
									this.CharacterAnimation.CrossFade(this.SprintAnim);
								}
								else
								{
									this.CharacterAnimation.CrossFade(this.OriginalSprintAnim);
								}

								this.Pathfinding.canSearch = true;
								this.Pathfinding.canMove = true;

                                if (this.Yandere.Chased && this.Yandere.Pursuer == this)
								{
									Debug.Log (this.Name + " is chasing Yandere-chan.");

									this.Pathfinding.repathRate = 0.0f;
									this.Pathfinding.speed = 5.0f;

									this.ChaseTimer += Time.deltaTime;

									if (this.ChaseTimer > 10)
									{
										this.transform.position = this.Yandere.transform.position +
											(this.Yandere.transform.forward * .999f);

										this.transform.LookAt(this.Yandere.transform.position);

										Physics.SyncTransforms();
									}
								}
								else
								{
									//Debug.Log("Sprinting 8");
									this.Pathfinding.speed = 4.0f;
								}

								// Phone Addict
								if (this.Persona == PersonaType.PhoneAddict && !this.Phoneless)
								{
									if (!this.CrimeReported)
									{
										if (this.Countdown.Sprite.fillAmount == 0)
										{
											this.Countdown.Sprite.fillAmount = 1;
											this.CrimeReported = true;

											if (this.WitnessedMurder && !this.Countdown.MaskedPhoto)
											{
												this.PhoneAddictGameOver();
											}
											else
											{
												if (this.StudentManager.ChaseCamera == this.ChaseCamera)
												{
													this.StudentManager.ChaseCamera = null;
												}

												this.SprintAnim = this.OriginalSprintAnim;
												this.Countdown.gameObject.SetActive(false);
												this.ChaseCamera.SetActive(false);
												this.Police.Called = true;
												this.Police.Show = true;
											}
										}
										else
										{
											this.SprintAnim = this.PhoneAnims[2];

											if (this.StudentManager.ChaseCamera == null)
											{
												this.StudentManager.ChaseCamera = this.ChaseCamera;
												this.ChaseCamera.SetActive(true);
											}
										}
									}
								}
							}
							//If we have reached our destination...
							else
							{
								this.Pathfinding.canSearch = false;
								this.Pathfinding.canMove = false;

								if (!this.Halt)
								{
									if (this.StudentID > 1)
									{
										this.MoveTowardsTarget(this.Pathfinding.target.position);

										if (!this.Teacher)
										{
											this.transform.rotation = Quaternion.Slerp(
												this.transform.rotation, this.Pathfinding.target.rotation, 10.0f * Time.deltaTime);
										}
									}
								}
								else
								{
									if (this.Spraying)
									{
										this.CharacterAnimation.CrossFade (this.SprayAnim);
									}

									if (this.Persona == PersonaType.TeachersPet)
									{
										this.targetRotation = Quaternion.LookRotation(new Vector3(
											this.StudentManager.Teachers[this.Class].transform.position.x,
											this.transform.position.y,
											this.StudentManager.Teachers[this.Class].transform.position.z) - this.transform.position);
										
										this.transform.rotation = Quaternion.Slerp(
											this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);
									}
									else if (this.Persona == PersonaType.Dangerous)
									{
										if (!this.BreakingUpFight)
										{
											this.targetRotation = Quaternion.LookRotation(new Vector3(
												this.Yandere.Hips.transform.position.x,
												this.transform.position.y,
												this.Yandere.Hips.transform.position.z) - this.transform.position);

											this.transform.rotation = Quaternion.Slerp(
												this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);
										}
									}
								}

								// Teacher's Pet.
								if (this.Persona == PersonaType.TeachersPet)
								{
									if (this.ReportingMurder || this.ReportingBlood)
									{
										//If the teacher is alarmed, this means that the teacher
										// has just spotted the corpse/blood/whatever.
										if (this.StudentManager.Teachers[this.Class].Alarmed)
										{
											//...however, that's only if this character is not in a state
											//where they are watching the teacher leave the area.
											if (this.ReportPhase < 100)
											{
												if (this.Club == ClubType.Council)
												{
													this.Pathfinding.target = this.StudentManager.CorpseLocation;
													this.CurrentDestination = this.StudentManager.CorpseLocation;
												}
												else
												{
													if (this.PetDestination == null)
													{
														this.PetDestination = Instantiate(EmptyGameObject, this.Seat.position + (this.Seat.forward * -.5f), Quaternion.identity).transform;
													}

													this.Pathfinding.target = this.PetDestination;
													this.CurrentDestination = this.PetDestination;
												}
													
												this.ReportPhase = 3;
											}
										}

										//Pet talks to a teacher.
										if (this.ReportPhase == 0)
										{
											Debug.Log (this.Name + ", currently acting as a Teacher's Pet, is talking to a teacher.");

                                            if (this.MyTeacher == null)
                                            {
                                                this.MyTeacher = this.StudentManager.Teachers[this.Class];
                                            }

                                            if (!this.MyTeacher.Alive)
                                            {
                                                this.Persona = PersonaType.Loner;
                                                this.PersonaReaction();
                                            }
                                            else
                                            {
                                                if (this.WitnessedMurder)
											    {
												    this.Subtitle.Speaker = this;
												    this.Subtitle.UpdateLabel(SubtitleType.PetMurderReport, 2, 3.0f);
												    this.CharacterAnimation.CrossFade(this.ScaredAnim);
											    }
											    else if (this.WitnessedCorpse)
											    {
												    this.Subtitle.Speaker = this;
												    this.Subtitle.UpdateLabel(SubtitleType.PetCorpseReport, 2, 3.0f);
												    this.CharacterAnimation.CrossFade(this.ScaredAnim);
											    }
											    else if (this.WitnessedLimb)
											    {
												    this.Subtitle.Speaker = this;
												    this.Subtitle.UpdateLabel(SubtitleType.PetLimbReport, 2, 3.0f);
												    this.CharacterAnimation.CrossFade(this.ScaredAnim);
											    }
											    else if (this.WitnessedBloodyWeapon)
											    {
												    this.Subtitle.Speaker = this;
												    this.Subtitle.UpdateLabel(SubtitleType.PetBloodyWeaponReport, 2, 3.0f);
												    this.CharacterAnimation.CrossFade(this.ScaredAnim);
											    }
											    else if (this.WitnessedBloodPool)
											    {
												    this.Subtitle.Speaker = this;
												    this.Subtitle.UpdateLabel(SubtitleType.PetBloodReport, 2, 3.0f);
												    this.CharacterAnimation.CrossFade(this.IdleAnim);
											    }
											    else if (this.WitnessedWeapon)
											    {
												    this.Subtitle.Speaker = this;
												    this.Subtitle.UpdateLabel(SubtitleType.PetWeaponReport, 2, 3.0f);
												    this.CharacterAnimation.CrossFade(this.ScaredAnim);
											    }

											    this.MyTeacher = this.StudentManager.Teachers[this.Class];

											    this.MyTeacher.CurrentDestination = this.MyTeacher.transform;
											    this.MyTeacher.Pathfinding.target = this.MyTeacher.transform;

											    this.MyTeacher.Pathfinding.canSearch = false;
											    this.MyTeacher.Pathfinding.canMove = false;

											    this.StudentManager.Teachers[this.Class].CharacterAnimation.CrossFade(
												    this.StudentManager.Teachers[this.Class].IdleAnim);
											    this.StudentManager.Teachers[this.Class].Routine = false;

											    if (this.StudentManager.Teachers[this.Class].Investigating)
											    {
												    this.StudentManager.Teachers[this.Class].StopInvestigating();
											    }

											    this.Halt = true;

											    this.ReportPhase++;
                                            }
                                        }
										else if (this.ReportPhase == 1)
										{
											//Debug.Log("Setting teacher as destination in ReportPhase 1.");

											this.Pathfinding.target = this.StudentManager.Teachers[this.Class].transform;
											this.CurrentDestination = this.StudentManager.Teachers[this.Class].transform;

											if (this.WitnessedBloodPool || this.WitnessedWeapon && !this.WitnessedBloodyWeapon)
											{
												//Debug.Log("Here");

												this.CharacterAnimation.CrossFade(this.IdleAnim);
											}
											else if (this.WitnessedMurder || this.WitnessedCorpse ||
													this.WitnessedLimb || this.WitnessedBloodyWeapon)
											{
												//Debug.Log("No, here");

												this.CharacterAnimation.CrossFade(this.ScaredAnim);
											}

											this.StudentManager.Teachers[this.Class].targetRotation = Quaternion.LookRotation(
												this.transform.position - this.StudentManager.Teachers[this.Class].transform.position);

											this.StudentManager.Teachers[this.Class].transform.rotation = Quaternion.Slerp(
												this.StudentManager.Teachers[this.Class].transform.rotation,
												this.StudentManager.Teachers[this.Class].targetRotation,
												10.0f * Time.deltaTime);

											this.ReportTimer += Time.deltaTime;

											if (this.ReportTimer >= 3.0f)
											{
												this.transform.position = new Vector3(
													this.transform.position.x,
													this.transform.position.y + 0.10f,
													this.transform.position.z);

												this.StudentManager.Teachers[this.Class].MyReporter = this;
												this.StudentManager.Teachers[this.Class].Routine = false;
												this.StudentManager.Teachers[this.Class].Fleeing = true;

												this.ReportTimer = 0.0f;

												this.ReportPhase++;
											}
										}
										//Standing there, waiting for teacher to notice stuff.
										else if (this.ReportPhase == 2)
										{
											//Debug.Log("Setting teacher as destination while waiting for teacher to notice a corpse or blood.");

											this.Pathfinding.target = this.StudentManager.Teachers[this.Class].transform;
											this.CurrentDestination = this.StudentManager.Teachers[this.Class].transform;

											if (this.WitnessedBloodPool || this.WitnessedWeapon && !this.WitnessedBloodyWeapon)
											{
												this.CharacterAnimation.CrossFade(this.IdleAnim);
											}
											else if (this.WitnessedMurder || this.WitnessedCorpse ||
													 this.WitnessedLimb || this.WitnessedBloodyWeapon)
											{
												this.CharacterAnimation.CrossFade(this.ScaredAnim);
											}
										}
										//Standing beside the teacher for safety as the teacher guards the corpse/blood/whatever.
										else if (this.ReportPhase == 3)
										{
											Debug.Log(this.Name + " just set their destination to themself.");

											this.Pathfinding.target = this.transform;
											this.CurrentDestination = this.transform;

											if (this.WitnessedBloodPool || this.WitnessedWeapon && !this.WitnessedBloodyWeapon)
											{
												//Debug.Log("Here");

												this.CharacterAnimation.CrossFade(this.IdleAnim);
											}
											else if (this.WitnessedMurder || this.WitnessedCorpse ||
													this.WitnessedLimb || this.WitnessedBloodyWeapon)
											{
												//Debug.Log("No, here");

												this.CharacterAnimation.CrossFade(this.ParanoidAnim);
											}
										}
										else if (this.ReportPhase < 100)
										{
											this.CharacterAnimation.CrossFade(this.ParanoidAnim);
										}
										//If the teacher thinks it's a prank...
										else
										{
											Debug.Log("This character just set their destination to themself.");

											this.Pathfinding.target = this.transform;
											this.CurrentDestination = this.transform;

											this.CharacterAnimation.CrossFade(this.ScaredAnim);

											this.ReportTimer += Time.deltaTime;

											if (this.ReportTimer >= 5.0f)
											{
												//This is only called if a teacher thinks she just got pranked.

												this.ReturnToNormal();
											}
										}
									}
									else
									{
										if (this.Club == ClubType.Council)
										{
											this.CharacterAnimation.CrossFade(this.GuardAnim);
											this.Persona = PersonaType.Dangerous;
											this.Guarding = true;
											this.Fleeing = false;
										}
										else
										{
											this.CharacterAnimation.CrossFade(this.ParanoidAnim);
											this.ReportPhase = 100;
										}
									}
								}
								// Hero.
								else if (this.Persona == PersonaType.Heroic)
								{
									Debug.Log (this.Name + " has the ''Heroic'' Persona and is using the ''Fleeing'' protocol.");

									//If Yandere-chan is already engaged in a struggle with someone else...
									if (this.Yandere.Attacking || this.Yandere.Struggling && this.Yandere.StruggleBar.Student != this)
									{
										Debug.Log (this.Name + " is waiting his turn to fight Yandere-chan.");

										this.CharacterAnimation.CrossFade(this.ReadyToFightAnim);

										this.targetRotation = Quaternion.LookRotation(new Vector3(
											this.Yandere.Hips.transform.position.x,
											this.transform.position.y,
											this.Yandere.Hips.transform.position.z) - this.transform.position);

										this.transform.rotation = Quaternion.Slerp(
											this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);

										this.Pathfinding.canSearch = false;
										this.Pathfinding.canMove = false;
									}
									//If Yandere-chan is not currently engaged in a struggle with anyone else...
									else
									{
										//If Yandere-chan is not in an animation where she's busy...
										if (!this.Yandere.Attacking && !this.StudentManager.PinningDown && !this.Yandere.Shoved)
										{
											//If we're not Senpai...
											if (this.StudentID > 1)
											{
												if (!this.Yandere.Struggling && this.Yandere.ShoulderCamera.Timer == 0)
												{
													this.BeginStruggle();
												}

												Debug.Log(this.Name + " is currently engaged in a stuggle.");

												if (!this.Teacher)
												{
													this.CharacterAnimation[this.StruggleAnim].time =
														this.Yandere.CharacterAnimation[AnimNames.FemaleStruggleA].time;
												}
												else
												{
													this.CharacterAnimation[this.StruggleAnim].time =
														this.Yandere.CharacterAnimation[AnimNames.FemaleTeacherStruggleA].time;
												}

												this.transform.rotation = Quaternion.Slerp(
													this.transform.rotation, this.Yandere.transform.rotation, 10.0f * Time.deltaTime);
												this.MoveTowardsTarget(this.Yandere.transform.position +
													(this.Yandere.transform.forward * 0.425f));

												// [af] Commented in JS code.
												//transform.position = Vector3.Lerp(transform.position, Yandere.transform.position + Yandere.transform.forward * .0001, DeltaTime * 10);

												if (!this.Yandere.Armed || !this.Yandere.EquippedWeapon.Concealable)
												{
													this.Yandere.StruggleBar.HeroWins();
												}

												if (this.Lost)
												{
													this.CharacterAnimation.CrossFade(this.StruggleWonAnim);

													if (this.CharacterAnimation[this.StruggleWonAnim].time > 1.0f)
													{
														this.EyeShrink = 1.0f;
													}

													if (this.CharacterAnimation[this.StruggleWonAnim].time >=
														this.CharacterAnimation[this.StruggleWonAnim].length)
													{
														// [af] Commented in JS code.
														//BecomeRagdoll();
														//Dead = true;
													}
												}
												else if (this.Won)
												{
                                                    //Debug.Log("The code got here.");

													this.CharacterAnimation.CrossFade(this.StruggleLostAnim);
												}
											}
											//Code specifically for Senpai alone
											else
											{
                                                if (this.Yandere.Mask != null)
                                                {
												    this.Yandere.EmptyHands();

												    this.Pathfinding.canSearch = false;
												    this.Pathfinding.canMove = false;
												    this.TargetDistance = 1;

												    this.Yandere.CharacterAnimation.CrossFade("f02_unmasking_00");
												    this.CharacterAnimation.CrossFade("unmasking_00");
												    this.Yandere.CanMove = false;

												    this.targetRotation = Quaternion.LookRotation(
													    this.Yandere.transform.position - this.transform.position);

												    this.transform.rotation = Quaternion.Slerp(
													    this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);

												    this.MoveTowardsTarget(this.Yandere.transform.position +
													    (this.Yandere.transform.forward * 1));

												    if (this.CharacterAnimation["unmasking_00"].time == 0)
												    {
													    this.Yandere.ShoulderCamera.YandereNo();
													    this.Yandere.Jukebox.GameOver();
												    }

												    if (this.CharacterAnimation["unmasking_00"].time >= .66666f)
												    {
													    if (this.Yandere.Mask.transform.parent != this.LeftHand)
													    {
                                                            this.Yandere.CanMove = true;
                                                            this.Yandere.EmptyHands();
                                                            this.Yandere.CanMove = false;

                                                            this.Yandere.Mask.transform.parent = this.LeftHand;
														    this.Yandere.Mask.transform.localPosition = new Vector3(-.1f, -.05f, 0);
														    this.Yandere.Mask.transform.localEulerAngles = new Vector3(-90, 90, 0);
														    this.Yandere.Mask.transform.localScale = new Vector3(1, 1, 1);
													    }
												    }

												    if (this.CharacterAnimation["unmasking_00"].time >= this.CharacterAnimation["unmasking_00"].length)
												    {
													    this.Yandere.Unmasked = true;
													    this.Yandere.ShoulderCamera.GameOver();
                                                        this.Yandere.Mask.Drop();
                                                    }
                                                }
                                            }
										}
									}
								}
								// Coward.
								else if (this.Persona == PersonaType.Coward || this.Persona == PersonaType.Fragile)
								{
									this.targetRotation = Quaternion.LookRotation(new Vector3(
										this.Yandere.Hips.transform.position.x,
										this.transform.position.y,
										this.Yandere.Hips.transform.position.z) - this.transform.position);
									this.transform.rotation = Quaternion.Slerp(
										this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);

									this.CharacterAnimation.CrossFade(this.CowardAnim);

									this.ReactionTimer += Time.deltaTime;

									if (this.ReactionTimer > 5.0f)
									{
										this.CurrentDestination = this.StudentManager.Exit;
										this.Pathfinding.target = this.StudentManager.Exit;
									}
								}
								// Evil.
								else if (this.Persona == PersonaType.Evil)
								{
									this.targetRotation = Quaternion.LookRotation(new Vector3(
										this.Yandere.Hips.transform.position.x,
										this.transform.position.y,
										this.Yandere.Hips.transform.position.z) - this.transform.position);
									this.transform.rotation = Quaternion.Slerp(
										this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);

									this.CharacterAnimation.CrossFade(this.EvilAnim);

									this.ReactionTimer += Time.deltaTime;

									if (this.ReactionTimer > 5.0f)
									{
										this.CurrentDestination = this.StudentManager.Exit;
										this.Pathfinding.target = this.StudentManager.Exit;
									}
								}
								// Social Butterfly.
								else if (this.Persona == PersonaType.SocialButterfly)
								{
									if (this.ReportPhase < 4)
									{
										this.transform.rotation = Quaternion.Slerp(
											this.transform.rotation, this.Pathfinding.target.rotation, 10.0f * Time.deltaTime);
									}

									if (this.ReportPhase == 1)
									{
										if (!this.SmartPhone.activeInHierarchy)
										{
											if ((this.StudentManager.Reporter == null) && !this.Police.Called)
											{
												this.CharacterAnimation.CrossFade(this.SocialReportAnim);
												this.Subtitle.UpdateLabel(SubtitleType.SocialReport, 1, 5.0f);
												this.StudentManager.Reporter = this;
												this.SmartPhone.SetActive(true);

												this.SmartPhone.transform.localPosition = new Vector3(-.015f, -.01f, 0);
												this.SmartPhone.transform.localEulerAngles = new Vector3(0, -170, 165);
											}
											else
											{
												this.ReportTimer = 5.0f;
											}
										}

										this.ReportTimer += Time.deltaTime;

										if (this.ReportTimer > 5.0f)
										{
											if (this.StudentManager.Reporter == this)
											{
												this.Police.Called = true;
												this.Police.Show = true;
											}

											this.CharacterAnimation.CrossFade(this.ParanoidAnim);
											this.SmartPhone.SetActive(false);
											this.ReportPhase++;
											this.Halt = false;
										}
									}
									else if (this.ReportPhase == 2)
									{
										if (this.WitnessedMurder)
										{
											if (!this.SawMask || this.SawMask && (this.Yandere.Mask != null))
											{
												this.LookForYandere();
											}
										}
									}
									else if (this.ReportPhase == 3)
									{
										this.CharacterAnimation.CrossFade(this.SocialFearAnim);
										this.Subtitle.UpdateLabel(SubtitleType.SocialFear, 1, 5.0f);
										this.SpawnAlarmDisc();
										this.ReportPhase++;
										this.Halt = true;
									}
									else if (this.ReportPhase == 4)
									{
										this.targetRotation = Quaternion.LookRotation(new Vector3(
											this.Yandere.Hips.transform.position.x,
											this.transform.position.y,
											this.Yandere.Hips.transform.position.z)- this.transform.position);
										
										this.transform.rotation = Quaternion.Slerp(
											this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);

										if (this.Yandere.Attacking)
										{
											this.LookForYandere();
										}
									}
									else if (this.ReportPhase == 5)
									{
										this.CharacterAnimation.CrossFade(this.SocialTerrorAnim);
										this.Subtitle.UpdateLabel(SubtitleType.SocialTerror, 1, 5.0f);
										this.VisionDistance = 0.0f;
										this.SpawnAlarmDisc();
										this.ReportPhase++;
									}
								}
								// Lovestruck

								//Anti-Osana Code
								#if UNITY_EDITOR
								else if (this.Persona == PersonaType.Lovestruck)
								{
									if (this.ReportPhase < 3)
									{
										if (this.StudentManager.Students[LovestruckTarget].Fleeing == true)
										{
											Debug.Log("Lovestruck Target is fleeing, so destination is being set to Exit.");

											this.Pathfinding.target = this.StudentManager.Exit;
											this.CurrentDestination = this.StudentManager.Exit;
											this.ReportPhase = 3;
										}
									}

									if (this.ReportPhase == 1)
									{
										if (this.StudentManager.Students[LovestruckTarget].Male)
										{
											this.StudentManager.Students[LovestruckTarget].CharacterAnimation.CrossFade(AnimNames.MaleSurprised);
										}
										else
										{
											this.StudentManager.Students[LovestruckTarget].CharacterAnimation.CrossFade(AnimNames.FemaleSurprised);
										}

										this.StudentManager.Students[LovestruckTarget].EmptyHands();

										this.CharacterAnimation.CrossFade(this.ScaredAnim);

										this.StudentManager.Students[LovestruckTarget].Pathfinding.canSearch = false;
										this.StudentManager.Students[LovestruckTarget].Pathfinding.canMove = false;
										this.StudentManager.Students[LovestruckTarget].Pathfinding.enabled = false;

										this.StudentManager.Students[LovestruckTarget].Routine = false;
										this.Pathfinding.enabled = false;

										if (this.WitnessedMurder && !this.SawMask)
										{
											this.Yandere.Jukebox.gameObject.active = false;
											this.Yandere.MainCamera.enabled = false;
											this.Yandere.RPGCamera.enabled = false;
											this.Yandere.Jukebox.Volume = 0;
											this.Yandere.CanMove = false;

											this.StudentManager.LovestruckCamera.transform.parent = this.transform;
											this.StudentManager.LovestruckCamera.transform.localPosition = new Vector3(1, 1, -1);
											this.StudentManager.LovestruckCamera.transform.localEulerAngles = new Vector3(0, -30, 0);
											this.StudentManager.LovestruckCamera.active = true;
										}

										if (this.WitnessedMurder && !this.SawMask)
										{
											this.Subtitle.UpdateLabel(SubtitleType.LovestruckMurderReport, 0, 5.0f);
										}
										else
										{
											if (this.LovestruckTarget == 1)
											{
												this.Subtitle.UpdateLabel(SubtitleType.LovestruckCorpseReport, 0, 5.0f);
											}
											else
											{
												this.Subtitle.UpdateLabel(SubtitleType.LovestruckCorpseReport, 1, 5.0f);
											}
										}

										this.ReportPhase++;
									}
									else if (this.ReportPhase == 2)
									{
										this.targetRotation = Quaternion.LookRotation(new Vector3(
											this.StudentManager.Students[LovestruckTarget].transform.position.x,
											this.transform.position.y,
											this.StudentManager.Students[LovestruckTarget].transform.position.z) - this.transform.position);

										this.transform.rotation = Quaternion.Slerp(
											this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);

										this.targetRotation = Quaternion.LookRotation(new Vector3(
											this.transform.position.x,
											this.StudentManager.Students[LovestruckTarget].transform.position.y,
											this.transform.position.z) - StudentManager.Students[LovestruckTarget].transform.position);

										this.StudentManager.Students[LovestruckTarget].transform.rotation = Quaternion.Slerp(
											this.StudentManager.Students[LovestruckTarget].transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);

										this.ReportTimer += Time.deltaTime;

										if (this.ReportTimer > 5)
										{
											if (this.WitnessedMurder && !this.SawMask)
											{
												this.Yandere.ShoulderCamera.HeartbrokenCamera.SetActive(true);
												this.Yandere.Police.EndOfDay.Heartbroken.Exposed = true;

												this.Yandere.Character.GetComponent<Animation>().CrossFade(AnimNames.FemaleDown22);
												this.Yandere.Collapse = true;

												this.StudentManager.StopMoving();

												this.ReportPhase++;
											}
											else
											{
												Debug.Log("Both reporter and Lovestruck Target should be heading to the Exit.");

												this.StudentManager.Students[LovestruckTarget].Pathfinding.target = this.StudentManager.Exit;
												this.StudentManager.Students[LovestruckTarget].CurrentDestination = this.StudentManager.Exit;

												//Debug.Log("Sprinting 9");

												this.StudentManager.Students[LovestruckTarget].CharacterAnimation.CrossFade(this.StudentManager.Students[LovestruckTarget].SprintAnim);
												this.StudentManager.Students[LovestruckTarget].Pathfinding.canSearch = true;
												this.StudentManager.Students[LovestruckTarget].Pathfinding.canMove = true;
												this.StudentManager.Students[LovestruckTarget].Pathfinding.enabled = true;
												this.StudentManager.Students[LovestruckTarget].Pathfinding.speed = 4.0f;

												this.Pathfinding.target = this.StudentManager.Exit;
												this.CurrentDestination = this.StudentManager.Exit;
												this.Pathfinding.enabled = true;

												this.ReportPhase++;
											}
										}
									}
								}
								#endif

								// Dangerous
								else if (this.Persona == PersonaType.Dangerous)
								{
									if (!this.Yandere.Attacking && !this.StudentManager.PinningDown &&
										!this.Yandere.Struggling && !this.Yandere.Noticed)
									{
										this.Spray();
									}
									else
									{
										this.CharacterAnimation.CrossFade(this.ReadyToFightAnim);
									}
								}
								// Protective

								//Anti-Osana Code
								#if UNITY_EDITOR
								else if (this.Persona == PersonaType.Protective)
								{
									if (!this.Yandere.Dumping && !this.Yandere.Attacking)
									{
										Debug.Log("A protective student is taking down Yandere-chan.");

										if (this.Yandere.Aiming)
										{
											this.Yandere.StopAiming();
										}

										this.Yandere.Mopping = false;
										this.Yandere.EmptyHands();

										this.Subtitle.UpdateLabel(SubtitleType.ObstacleMurderReaction, 4, 0.0f);

										this.AttackReaction();
										this.CharacterAnimation["f02_moCounterB_00"].time = 6.0f;
										this.Yandere.CharacterAnimation["f02_moCounterA_00"].time = 6.0f;
										this.Yandere.ShoulderCamera.ObstacleCounter = true;
										this.Yandere.ShoulderCamera.Timer = 6.0f;
										//this.Yandere.ShoulderCamera.Phase = 3;
										this.Police.Show = false;

										this.Yandere.CameraEffects.MurderWitnessed();
										this.Yandere.Jukebox.GameOver();
									}
									else
									{
										this.CharacterAnimation.CrossFade(this.ReadyToFightAnim);
									}
								}
								#endif

								// Violent.
								else if (this.Persona == PersonaType.Violent)
								{
									if (!this.Yandere.Attacking && !this.Yandere.Struggling && !this.Yandere.Dumping &&
									    !this.StudentManager.PinningDown && !this.RespectEarned)
									{
										if (!this.Yandere.DelinquentFighting)
										{
											Debug.Log(this.Name + " is supposed to begin the combat minigame now.");

                                            //this.SilentlyForgetBloodPool();

                                            this.SmartPhone.SetActive(false);
											this.Threatened = true;
											this.Fleeing = false;
											this.Alarmed = true;
											this.NoTalk = false;
											this.Patience = 0;
										}
									}
									else
									{
										this.CharacterAnimation.CrossFade(this.ReadyToFightAnim);
									}
								}
								// Teacher.
								else if (this.Persona == PersonaType.Strict)
								{
									if (!this.WitnessedMurder)
									{
										// React to student's report.
										if (this.ReportPhase == 0)
										{
											if (this.MyReporter.WitnessedMurder || this.MyReporter.WitnessedCorpse)
											{
												this.Subtitle.Speaker = this;
												this.Subtitle.UpdateLabel(SubtitleType.TeacherReportReaction, 0, 3.0f);

												this.InvestigatingPossibleDeath = true;
											}
											else if (this.MyReporter.WitnessedLimb)
											{
												this.Subtitle.Speaker = this;
												this.Subtitle.UpdateLabel(SubtitleType.TeacherReportReaction, 2, 3.0f);
											}
											else if (this.MyReporter.WitnessedBloodyWeapon)
											{
												this.Subtitle.Speaker = this;
												this.Subtitle.UpdateLabel(SubtitleType.TeacherReportReaction, 3, 3.0f);
											}
											else if (this.MyReporter.WitnessedBloodPool)
											{
												this.Subtitle.Speaker = this;
												this.Subtitle.UpdateLabel(SubtitleType.TeacherReportReaction, 1, 3.0f);
											}
											else if (this.MyReporter.WitnessedWeapon)
											{
												this.Subtitle.Speaker = this;
												this.Subtitle.UpdateLabel(SubtitleType.TeacherReportReaction, 4, 3.0f);
											}

											this.ReportPhase++;
										}
										// Wait for 3 seconds, then take action.
										else if (this.ReportPhase == 1)
										{
											this.CharacterAnimation.CrossFade(this.IdleAnim);

											this.ReportTimer += Time.deltaTime; 

											if (this.ReportTimer >= 3.0f)
											{
												this.transform.position = new Vector3(
													this.transform.position.x,
													this.transform.position.y + 0.10f,
													this.transform.position.z);

												StudentScript RelevantReporter = null;

												if (this.MyReporter.WitnessedMurder || this.MyReporter.WitnessedCorpse)
												{
													RelevantReporter = this.StudentManager.Reporter;
												}
												else if (this.MyReporter.WitnessedBloodPool || this.MyReporter.WitnessedLimb ||
													     this.MyReporter.WitnessedWeapon)
												{
													RelevantReporter = this.StudentManager.BloodReporter;
												}

												if (this.MyReporter.WitnessedLimb)
												{
													this.InvestigatingPossibleLimb = true;
												}

												if (!RelevantReporter.Teacher)
												{
													if (this.MyReporter.WitnessedMurder || this.MyReporter.WitnessedCorpse)
													{
														this.StudentManager.Reporter.CurrentDestination = this.StudentManager.CorpseLocation;
														this.StudentManager.Reporter.Pathfinding.target = this.StudentManager.CorpseLocation;

														this.CurrentDestination = this.StudentManager.CorpseLocation;
														this.Pathfinding.target = this.StudentManager.CorpseLocation;

														this.StudentManager.Reporter.TargetDistance = 2.0f;
													}
													else if (this.MyReporter.WitnessedBloodPool || this.MyReporter.WitnessedLimb ||
													    	 this.MyReporter.WitnessedWeapon)
													{
														this.StudentManager.BloodReporter.CurrentDestination = this.StudentManager.BloodLocation;
														this.StudentManager.BloodReporter.Pathfinding.target = this.StudentManager.BloodLocation;

														this.CurrentDestination = this.StudentManager.BloodLocation;
														this.Pathfinding.target = this.StudentManager.BloodLocation;

														this.StudentManager.BloodReporter.TargetDistance = 2.0f;
													}
												}

												this.TargetDistance = 1.0f;

												this.ReportTimer = 0.0f;
												this.ReportPhase++;
											}
										}
										//See corpse/blood and react
										else if (this.ReportPhase == 2)
										{
											if (this.WitnessedCorpse)
											{
												Debug.Log("A teacher has just witnessed a corpse while on their way to investigate a student's report of a corpse.");

												this.DetermineCorpseLocation();

												if (!this.Corpse.Poisoned)
												{
													this.Subtitle.Speaker = this;
													this.Subtitle.UpdateLabel(SubtitleType.TeacherCorpseInspection, 1, 5.0f);
												}
												else
												{
													this.Subtitle.Speaker = this;
													this.Subtitle.UpdateLabel(SubtitleType.TeacherCorpseInspection, 2, 2.0f);
												}

												this.ReportPhase++;
											}
											else if (this.WitnessedBloodPool || this.WitnessedLimb || this.WitnessedWeapon)
											{
												Debug.Log("A teacher has just witnessed an alarming object while on their way to investigate a student's report.");

												this.DetermineBloodLocation();

												if (!this.VerballyReacted)
												{
													if (this.WitnessedLimb)
													{
														this.Subtitle.Speaker = this;
														this.Subtitle.UpdateLabel(SubtitleType.TeacherCorpseInspection, 4, 5.0f);
													}
													else if (this.WitnessedBloodPool || this.WitnessedBloodyWeapon)
													{
														this.Subtitle.Speaker = this;
														this.Subtitle.UpdateLabel(SubtitleType.TeacherCorpseInspection, 3, 5.0f);
													}
													else if (this.WitnessedWeapon)
													{
														this.Subtitle.Speaker = this;
														this.Subtitle.UpdateLabel(SubtitleType.TeacherCorpseInspection, 5, 5.0f);
													}
												}

												PromptScript BloodPrompt = this.BloodPool.GetComponent<PromptScript>();

												if (BloodPrompt != null)
												{
													Debug.Log("Disabling an object's prompt.");

													BloodPrompt.Hide();
													BloodPrompt.enabled = false;
												}

												this.ReportPhase++;
											}
											//Witnessed nothing; assume prank.
											else
											{
												this.CharacterAnimation.CrossFade(this.GuardAnim);

												this.ReportTimer += Time.deltaTime;

												if (this.ReportTimer > 5.0f)
												{
													this.Subtitle.UpdateLabel(SubtitleType.TeacherPrankReaction, 1, 7.0f);
													this.ReportPhase = 98;
													this.ReportTimer = 0.0f;
												}
											}
										}
										//Inspect the corpse
										else if (this.ReportPhase == 3)
										{
											if (this.WitnessedCorpse)
											{
												this.targetRotation = Quaternion.LookRotation(new Vector3(
													this.Corpse.AllColliders[0].transform.position.x,
													this.transform.position.y,
													this.Corpse.AllColliders[0].transform.position.z) - this.transform.position);
												this.transform.rotation = Quaternion.Slerp(
													this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);

												this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
												this.CharacterAnimation.CrossFade(this.InspectAnim);
											}
											else if (this.WitnessedBloodPool || this.WitnessedLimb || this.WitnessedWeapon)
											{
												this.targetRotation = Quaternion.LookRotation(new Vector3(
													this.BloodPool.transform.position.x,
													this.transform.position.y,
													this.BloodPool.transform.position.z) - this.transform.position);

												this.transform.rotation = Quaternion.Slerp(
													this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);

												this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
												this.CharacterAnimation[InspectBloodAnim].speed = .66666f;
												this.CharacterAnimation.CrossFade(this.InspectBloodAnim);
											}

											this.ReportTimer += Time.deltaTime;

											if (this.ReportTimer >= 6.0f)
											{
												this.ReportTimer = 0.0f;

												if (this.WitnessedWeapon && !this.WitnessedBloodyWeapon)
												{
													this.ReportPhase = 7;
												}
												else
												{
													this.ReportPhase++;
												}
											}
										}
										//Call the cops
										else if (this.ReportPhase == 4)
										{
											if (this.WitnessedCorpse)
											{
												this.Subtitle.Speaker = this;
												this.Subtitle.UpdateLabel(SubtitleType.TeacherPoliceReport, 0, 5.0f);
											}
											else if (this.WitnessedBloodPool || this.WitnessedLimb || this.WitnessedWeapon)
											{
												this.Subtitle.Speaker = this;
												this.Subtitle.UpdateLabel(SubtitleType.TeacherPoliceReport, 1, 5.0f);
											}

											this.SmartPhone.transform.localPosition = new Vector3(-.01f, -.005f, -.02f);
											this.SmartPhone.transform.localEulerAngles = new Vector3(-10, -145, 170);
											this.SmartPhone.SetActive(true);
											this.ReportPhase++;
										}
										//Enter the "guarding corpse" animation
										else if (this.ReportPhase == 5)
										{
											this.CharacterAnimation.CrossFade(this.CallAnim);

											this.ReportTimer += Time.deltaTime;

											if (this.ReportTimer >= 5.0f)
											{
												this.CharacterAnimation.CrossFade(this.GuardAnim);
												this.SmartPhone.SetActive(false);

												this.WitnessedBloodyWeapon = false;
												this.WitnessedBloodPool = false;
												this.WitnessedSomething = false;
												this.WitnessedWeapon = false;
												this.WitnessedLimb = false;

												this.IgnoringPettyActions = true;
												this.Police.Called = true;
												this.Police.Show = true;
												this.ReportTimer = 0.0f;
												this.Guarding = true;
												this.Alarmed = false;
												this.Fleeing = false;
												this.Reacted = false;
												this.ReportPhase++;

												if (this.MyReporter!= null)
												{
													if (this.MyReporter.ReportingBlood)
													{
														Debug.Log("The blood reporter has just been instructed to stop following the teacher.");
														this.MyReporter.ReportPhase++;
													}
												}
											}
										}
										//Guarding
										else if (this.ReportPhase == 6)
										{
											//Nothing needs to go here.
										}
										//Pick up weapon
										else if (this.ReportPhase == 7)
										{
											Debug.Log("Telling reporter to go back to their normal routine.");

											if (this.StudentManager.BloodReporter != this)
											{
												this.StudentManager.BloodReporter = null;
											}

											this.StudentManager.UpdateStudents();

											if (this.MyReporter != null)
											{
												this.MyReporter.CurrentDestination = this.MyReporter.Destinations[this.MyReporter.Phase];
												this.MyReporter.Pathfinding.target = this.MyReporter.Destinations[this.MyReporter.Phase];

												this.MyReporter.Pathfinding.speed = 1.0f;
												this.MyReporter.ReportTimer = 0.0f;
												this.MyReporter.AlarmTimer = 0.0f;

												this.MyReporter.TargetDistance = 1;
												this.MyReporter.ReportPhase = 0;

												this.MyReporter.WitnessedSomething = false;
												this.MyReporter.WitnessedWeapon = false;
												this.MyReporter.Distracted = false;
												this.MyReporter.Reacted = false;
												this.MyReporter.Alarmed = false;
												this.MyReporter.Fleeing = false;
												this.MyReporter.Routine = true;
												this.MyReporter.Halt = false;

												this.MyReporter.Persona = this.OriginalPersona;

												if (this.MyReporter.Club == ClubType.Council)
												{
													this.MyReporter.Persona = PersonaType.Dangerous;
												}

												// [af] Converted while loop to for loop.
												for (this.ID = 0; this.ID < this.MyReporter.Outlines.Length; this.ID++)
												{
													this.MyReporter.Outlines[this.ID].color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
												}
											}

											this.BloodPool.GetComponent<WeaponScript>().Prompt.enabled = false;
											this.BloodPool.GetComponent<WeaponScript>().Prompt.Hide();
											this.BloodPool.GetComponent<WeaponScript>().enabled = false;

											this.ReportPhase++;
										}
										//Pick up weapon
										else if (this.ReportPhase == 8)
										{
											this.CharacterAnimation.CrossFade("f02_teacherPickUp_00");

											if (this.CharacterAnimation["f02_teacherPickUp_00"].time >= .33333f)
											{
												this.Handkerchief.SetActive(true);
											}

											if (this.CharacterAnimation["f02_teacherPickUp_00"].time >= 2)
											{
												this.BloodPool.parent = this.RightHand;
												this.BloodPool.localPosition = new Vector3(0, 0, 0);
												this.BloodPool.localEulerAngles = new Vector3(0, 0, 0);
												this.BloodPool.GetComponent<WeaponScript>().Returner = this;
											}

											if (this.CharacterAnimation["f02_teacherPickUp_00"].time >= this.CharacterAnimation["f02_teacherPickUp_00"].length)
											{
												this.CurrentDestination = this.StudentManager.WeaponBoxSpot;
												this.Pathfinding.target = this.StudentManager.WeaponBoxSpot;

												this.Pathfinding.speed = 1;
												this.Hurry = false;

												this.ReportPhase++;
											}
										}
										//Drop weapon in box.
										else if (this.ReportPhase == 9)
										{
											this.StudentManager.BloodLocation.position = Vector3.zero;

											this.BloodPool.parent = null;
											this.BloodPool.position = this.StudentManager.WeaponBoxSpot.parent.position + new Vector3(0, 1, 0);
											this.BloodPool.eulerAngles = new Vector3(0, 90, 0);

											this.BloodPool.GetComponent<WeaponScript>().Prompt.enabled = true;
											this.BloodPool.GetComponent<WeaponScript>().Returner = null;
											this.BloodPool.GetComponent<WeaponScript>().enabled = true;
											this.BloodPool.GetComponent<WeaponScript>().Drop();

											this.BloodPool = null;

											this.CharacterAnimation.CrossFade(this.RunAnim);

											this.CurrentDestination = this.Destinations[this.Phase];
											this.Pathfinding.target = this.Destinations[this.Phase];

											this.Handkerchief.SetActive(false);

                                            this.StopInvestigating();

                                            this.Pathfinding.canSearch = true;
											this.Pathfinding.canMove = true;
											this.Pathfinding.speed = 1;

                                            this.WitnessedSomething = false;
                                            this.WitnessedSomething = false;
                                            this.VerballyReacted = false;
                                            this.WitnessedWeapon = false;
                                            this.YandereInnocent = false;
                                            this.ReportingBlood = false;
											this.Distracted = false;
											this.Alarmed = false;
											this.Fleeing = false;
											this.Routine = true;
                                            this.Halt = false;

											this.ReportTimer = 0.0f;
											this.ReportPhase = 0;
										}
										//If it's been 5 seconds and there is no corpse in sight...
										else if (this.ReportPhase == 98)
										{
											this.CharacterAnimation.CrossFade(this.IdleAnim);

											this.targetRotation = Quaternion.LookRotation(
												this.MyReporter.transform.position - this.transform.position);
											this.transform.rotation = Quaternion.Slerp(
												this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);

											this.ReportTimer += Time.deltaTime;

											if (this.ReportTimer > 7.0f)
											{
												this.ReportPhase++;
											}
										}
										//Leave and make the reporter enter their reaction state.
										else if (this.ReportPhase == 99)
										{
											this.Subtitle.UpdateLabel(SubtitleType.PrankReaction, 1, 5.0f);

											this.CharacterAnimation.CrossFade(this.RunAnim);

											this.CurrentDestination = this.Destinations[this.Phase];
											this.Pathfinding.target = this.Destinations[this.Phase];

											this.Pathfinding.canSearch = true;
											this.Pathfinding.canMove = true;

											this.MyReporter.Persona = PersonaType.TeachersPet;
											this.MyReporter.ReportPhase = 100;
											this.MyReporter.Fleeing = true;

											this.ReportTimer = 0.0f;
											this.ReportPhase = 0;

											this.InvestigatingPossibleDeath = false;
											this.InvestigatingPossibleLimb = false;
											this.Alarmed = false;
											this.Fleeing = false;
											this.Routine = true;
										}
									}
									else
									{
										if (!this.Yandere.Dumping && !this.Yandere.Attacking)
										{
											if ((this.Yandere.Class.PhysicalGrade + this.Yandere.Class.PhysicalBonus) == 0)
											{
												Debug.Log("A teacher is taking down Yandere-chan.");

												if (this.Yandere.Aiming)
												{
													this.Yandere.StopAiming();
												}

												this.Yandere.Mopping = false;
												this.Yandere.EmptyHands();

												this.AttackReaction();
												this.CharacterAnimation[this.CounterAnim].time = 5.0f;
												//this.Yandere.CharacterAnimation[AnimNames.FemaleCounterA].time = 5.0f;
												this.Yandere.CharacterAnimation["f02_teacherCounterA_00"].time = 5.0f;
												this.Yandere.ShoulderCamera.Timer = 5.0f;
												this.Yandere.ShoulderCamera.Phase = 3;
												this.Police.Show = false;

												this.Yandere.CameraEffects.MurderWitnessed();
												this.Yandere.Jukebox.GameOver();
											}
											else
											{
												this.Persona = PersonaType.Heroic;
											}
										}
										else
										{
											this.CharacterAnimation.CrossFade(this.ReadyToFightAnim);
										}
									}
								}
							}

							if (this.Persona == PersonaType.Strict)
							{
								if (this.BloodPool != null && this.BloodPool.parent == this.Yandere.RightHand)
								{
									Debug.Log("Yandere-chan picked up the weapon that the teacher was investigating!");

									this.WitnessedBloodyWeapon = false;
									this.WitnessedBloodPool = false;
									this.WitnessedSomething = false;
									this.WitnessedCorpse = false;
									this.WitnessedMurder = false;
									this.WitnessedWeapon = false;
									this.WitnessedLimb = false;

									this.YandereVisible = true;

									this.ReportTimer = 0.0f;
									this.BloodPool = null;
									this.ReportPhase = 0;
									this.Alarmed = false;
									this.Fleeing = false;
									this.Routine = true;
									this.Reacted = false;

									this.AlarmTimer = 0;
									this.Alarm = 200.0f;

									this.BecomeAlarmed();
								}
							}
						}
					}
					else
					{
						if (this.PinPhase == 0)
						{
							if (this.DistanceToDestination < 1.0f)
							{
								if (this.Pathfinding.canSearch)
								{
									this.Pathfinding.canSearch = false;
									this.Pathfinding.canMove = false;
								}

								this.targetRotation = Quaternion.LookRotation(new Vector3(
									this.Yandere.Hips.transform.position.x,
									this.transform.position.y,
									this.Yandere.Hips.transform.position.z) - this.transform.position);
								this.transform.rotation = Quaternion.Slerp(
									this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);

								this.CharacterAnimation.CrossFade(this.ReadyToFightAnim);

								this.MoveTowardsTarget(this.CurrentDestination.position);
							}
							else
							{
								//Debug.Log("Sprinting 4");
								this.CharacterAnimation.CrossFade(this.SprintAnim);

								if (!this.Pathfinding.canSearch)
								{
									this.Pathfinding.canSearch = true;
									this.Pathfinding.canMove = true;
								}
							}
						}
						else
						{
							this.transform.rotation = Quaternion.Slerp(
								this.transform.rotation, this.CurrentDestination.rotation, Time.deltaTime * 10.0f);
							this.MoveTowardsTarget(this.CurrentDestination.position);
						}
					}
				}
			}

			if (this.Following)
			{
				if (!this.Waiting)
				{
					this.DistanceToDestination = Vector3.Distance(
						this.transform.position, this.Pathfinding.target.position);

					if (this.DistanceToDestination > 2.0f)
					{
						//Debug.Log("Sprinting 10");

						this.CharacterAnimation.CrossFade(this.RunAnim);
						this.Pathfinding.speed = 4.0f;
						this.Obstacle.enabled = false;
					}
					else if (this.DistanceToDestination > 1.0f)
					{
						//Debug.Log("Raibaru is being told to perform her walk animation.");

						this.CharacterAnimation.CrossFade(this.OriginalWalkAnim);
						this.Pathfinding.canMove = true;
						this.Pathfinding.speed = 1.0f;
						this.Obstacle.enabled = false;
					}
					else
					{
						this.CharacterAnimation.CrossFade(this.IdleAnim);
						this.Pathfinding.canMove = false;
						this.Obstacle.enabled = true;
					}

					if (this.Phase < this.ScheduleBlocks.Length - 1)
					{
						if (this.FollowCountdown.Sprite.fillAmount == 0 ||
							this.Clock.HourTime >= this.ScheduleBlocks[this.Phase].time ||
							this.StudentManager.LockerRoomArea.bounds.Contains(Yandere.transform.position) ||
							this.StudentManager.WestBathroomArea.bounds.Contains(Yandere.transform.position) ||
							this.StudentManager.EastBathroomArea.bounds.Contains(Yandere.transform.position) ||
							this.StudentManager.IncineratorArea.bounds.Contains(Yandere.transform.position) ||
							this.StudentManager.HeadmasterArea.bounds.Contains(Yandere.transform.position))
						{
							if (this.Clock.HourTime >= this.ScheduleBlocks[this.Phase].time)
							{
								//This is only called if a student's daily routine proceeds to the next phase while they are following Yandere-chan.
								this.Phase++;
							}

							this.CurrentDestination = this.Destinations[this.Phase];
							this.Pathfinding.target = this.Destinations[this.Phase];

							ParticleSystem.EmissionModule heartsEmission = this.Hearts.emission;
							heartsEmission.enabled = false;

							this.FollowCountdown.gameObject.SetActive(false);
							this.Pathfinding.canSearch = true;
							this.Pathfinding.canMove = true;
							this.Pathfinding.speed = 1.0f;
                            this.Yandere.Follower = null;
                            this.Yandere.Followers--;
							this.Following = false;
							this.Routine = true;

							if (this.StudentManager.LockerRoomArea.bounds.Contains(Yandere.transform.position) ||
							    this.StudentManager.WestBathroomArea.bounds.Contains(Yandere.transform.position) ||
								this.StudentManager.EastBathroomArea.bounds.Contains(Yandere.transform.position) ||
							    this.StudentManager.IncineratorArea.bounds.Contains(Yandere.transform.position) ||
								this.StudentManager.HeadmasterArea.bounds.Contains(Yandere.transform.position))
							{
								this.Subtitle.UpdateLabel(SubtitleType.StopFollowApology, 1, 3.0f);
							}
							else
							{
								this.Subtitle.UpdateLabel(SubtitleType.StopFollowApology, 0, 3.0f);
							}

							this.Prompt.Label[0].text = "     " + "Talk";
						}
					}
				}
			}

			if (this.Wet)
			{
				if (this.DistanceToDestination < this.TargetDistance)
				{
					if (!this.Splashed)
					{
						if (!this.InDarkness)
						{
                            //This is called once the student has reached their destination.
							if (this.BathePhase == 1)
							{
                                this.CharacterAnimation[this.WetAnim].weight = 0.0f;

                                this.StudentManager.CommunalLocker.Open = true;
								this.StudentManager.CommunalLocker.Student = this;
								this.StudentManager.CommunalLocker.SpawnSteam();

								this.Pathfinding.speed = 1.0f;
								this.Schoolwear = 0;
								this.BathePhase++;
							}
							else if (this.BathePhase == 2)
							{
								this.transform.rotation = Quaternion.Slerp(
									this.transform.rotation, this.CurrentDestination.rotation, Time.deltaTime * 10.0f);
								this.MoveTowardsTarget(this.CurrentDestination.position);
							}
							else if (this.BathePhase == 3)
							{
								this.StudentManager.CommunalLocker.Open = false;
								this.CharacterAnimation.CrossFade(this.WalkAnim);

								if (!this.BatheFast)
								{
									if (!this.Male)
									{
										this.CurrentDestination = this.StudentManager.FemaleBatheSpot;
										this.Pathfinding.target = this.StudentManager.FemaleBatheSpot;
									}
									else
									{
										this.CurrentDestination = this.StudentManager.MaleBatheSpot;
										this.Pathfinding.target = this.StudentManager.MaleBatheSpot;
									}
								}
								else
								{
									if (!this.Male)
									{
										this.CurrentDestination = this.StudentManager.FastBatheSpot;
										this.Pathfinding.target = this.StudentManager.FastBatheSpot;
									}
									else
									{
										this.CurrentDestination = this.StudentManager.MaleBatheSpot;
										this.Pathfinding.target = this.StudentManager.MaleBatheSpot;
									}
								}

								this.Pathfinding.canSearch = true;
								this.Pathfinding.canMove = true;

								this.BathePhase++;
							}
							else if (this.BathePhase == 4)
							{
								this.StudentManager.OpenValue = Mathf.Lerp(this.StudentManager.OpenValue, 0, Time.deltaTime * 10);
								this.StudentManager.FemaleShowerCurtain.SetBlendShapeWeight(0, StudentManager.OpenValue);

								this.transform.rotation = Quaternion.Slerp(
									this.transform.rotation, this.CurrentDestination.rotation, Time.deltaTime * 10.0f);
								this.MoveTowardsTarget(this.CurrentDestination.position);

								this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;

								this.CharacterAnimation.CrossFade(this.BathingAnim);

								if (this.CharacterAnimation[this.BathingAnim].time >=
									this.CharacterAnimation[this.BathingAnim].length)
								{
									this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;

									this.StudentManager.OpenCurtain = true;
									this.LiquidProjector.enabled = false;
									this.Bloody = false;
									this.BathePhase++;
									this.Gas = false;
									this.GoChange();
									this.UnWet();
								}
							}
							else if (this.BathePhase == 5)
							{
								this.StudentManager.CommunalLocker.Open = true;
								this.StudentManager.CommunalLocker.Student = this;
								this.StudentManager.CommunalLocker.SpawnSteam();

								// [af] Replaced if/else statement with ternary expression.
								this.Schoolwear = this.InEvent ? 1 : 3;

								this.BathePhase++;
							}
							else if (this.BathePhase == 6)
							{
								this.transform.rotation = Quaternion.Slerp(
									this.transform.rotation, this.CurrentDestination.rotation, Time.deltaTime * 10.0f);
								this.MoveTowardsTarget(this.CurrentDestination.position);
							}
							else if (this.BathePhase == 7)
							{
								if (this.StudentManager.CommunalLocker.RivalPhone.Stolen && this.Yandere.Inventory.RivalPhoneID == this.StudentID)
								{
									this.CharacterAnimation.CrossFade(AnimNames.FemaleLosingPhone);
									this.Subtitle.UpdateLabel(this.LostPhoneSubtitleType, 1, 5.0f);

									this.RealizePhoneIsMissing();
								
									this.Phoneless = true;

									this.BatheTimer = 6.0f;
									this.BathePhase++;
								}
								else
								{
									this.StudentManager.CommunalLocker.RivalPhone.gameObject.SetActive(false);
                                    this.BathePhase++;
								}
							}
							//Confused, looking for lost phone.
							else if (this.BathePhase == 8)
							{
								if (this.BatheTimer == 0)
								{
									this.BathePhase++;
								}
								else
								{
									this.BatheTimer = Mathf.MoveTowards(BatheTimer, 0.0f, Time.deltaTime);
								}
							}
							else if (this.BathePhase == 9)
							{
								if (this.Persona == PersonaType.PhoneAddict && !this.Phoneless)
								{
									this.SmartPhone.SetActive(true);
								}
								else
								{
									this.WalkAnim = this.OriginalOriginalWalkAnim;
									this.RunAnim = this.OriginalSprintAnim;
									this.IdleAnim = this.OriginalIdleAnim;
								}

								this.StudentManager.CommunalLocker.Student = null;
								this.StudentManager.CommunalLocker.Open = false;

								if (this.Phase == 1)
								{
									this.Phase++;
								}

								//This is only called after a character has finished bathing.

								//Debug.Log(this.Name + " has finished bathing. Returning to normal routine.");

								this.CurrentDestination = this.Destinations[this.Phase];
								this.Pathfinding.target = this.Destinations[this.Phase];
								this.Pathfinding.canSearch = true;
								this.Pathfinding.canMove = true;
								this.DistanceToDestination = 100.0f;

								this.Routine = true;
								this.Wet = false;

								if (this.FleeWhenClean)
								{
									this.CurrentDestination = this.StudentManager.Exit;
									this.Pathfinding.target = this.StudentManager.Exit;
									this.TargetDistance = 0.0f;
									this.Routine = false;
									this.Fleeing = true;
								}
							}
						}
						else
						{
							if (this.BathePhase == -1)
							{
								this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;

								this.Subtitle.UpdateLabel(SubtitleType.LightSwitchReaction, 2, 5.0f);
								this.CharacterAnimation.CrossFade(AnimNames.FemaleElectrocution);

								this.Pathfinding.canSearch = false;
								this.Pathfinding.canMove = false;
								this.Distracted = true;

								this.BathePhase++;
							}
							else if (this.BathePhase == 0)
							{
								this.transform.rotation = Quaternion.Slerp(
									this.transform.rotation, this.CurrentDestination.rotation, Time.deltaTime * 10.0f);
								this.MoveTowardsTarget(this.CurrentDestination.position);

								if ((this.CharacterAnimation[AnimNames.FemaleElectrocution].time > 2.0f) &&
									(this.CharacterAnimation[AnimNames.FemaleElectrocution].time < (7.0f * 0.85f)))
								{
									if (!this.LightSwitch.Panel.useGravity)
									{
										if (!this.Bloody)
										{
											this.Subtitle.Speaker = this;
											this.Subtitle.UpdateLabel(this.SplashSubtitleType, 2, 5.0f);
										}
										else
										{
											this.Subtitle.Speaker = this;
											this.Subtitle.UpdateLabel(this.SplashSubtitleType, 4, 5.0f);
										}

										this.CurrentDestination = this.StudentManager.StrippingPositions[this.GirlID];;
										this.Pathfinding.target = this.StudentManager.StrippingPositions[this.GirlID];

										//Debug.Log("Sprinting 11");

										this.Pathfinding.canSearch = true;
										this.Pathfinding.canMove = true;
										this.Pathfinding.speed = 4.0f;

										this.CharacterAnimation.CrossFade(this.WalkAnim);
										this.BathePhase++;

										this.LightSwitch.Prompt.Label[0].text = "     " + "Turn Off";
										this.LightSwitch.BathroomLight.SetActive(true);
										this.LightSwitch.GetComponent<AudioSource>().clip = this.LightSwitch.Flick[0];
										this.LightSwitch.GetComponent<AudioSource>().Play();
										this.InDarkness = false;
									}
									else
									{
										if (!this.LightSwitch.Flicker)
										{
											this.CharacterAnimation[AnimNames.FemaleElectrocution].speed = 0.85f;

											GameObject NewElectricity = Instantiate(this.LightSwitch.Electricity,
												this.transform.position, Quaternion.identity);
											NewElectricity.transform.parent = this.Bones[1].transform;
											NewElectricity.transform.localPosition = Vector3.zero;

											// @todo: LightSwitchScript.ElectricityCackling is undefined.
											//this.LightSwitch.GetComponent<AudioSource>().clip = this.LightSwitch.ElectricityCackling;

											this.Subtitle.UpdateLabel(SubtitleType.LightSwitchReaction, 3, 0.0f);
											this.LightSwitch.GetComponent<AudioSource>().clip = this.LightSwitch.Flick[2];
											this.LightSwitch.Flicker = true;
											this.LightSwitch.GetComponent<AudioSource>().Play();
											this.EyeShrink = 1.0f;

											this.ElectroSteam[0].SetActive(true);
											this.ElectroSteam[1].SetActive(true);
											this.ElectroSteam[2].SetActive(true);
											this.ElectroSteam[3].SetActive(true);
										}

										this.RightDrill.eulerAngles = new Vector3(
											Random.Range(-360.0f, 360.0f),
											Random.Range(-360.0f, 360.0f),
											Random.Range(-360.0f, 360.0f));
										this.LeftDrill.eulerAngles = new Vector3(
											Random.Range(-360.0f, 360.0f),
											Random.Range(-360.0f, 360.0f),
											Random.Range(-360.0f, 360.0f));

										this.ElectroTimer += Time.deltaTime;

										if (this.ElectroTimer > 0.10f)
										{
											this.ElectroTimer = 0.0f;

											if (this.MyRenderer.enabled)
											{
												this.Spook();
											}
											else
											{
												this.Unspook();
											}
										}
									}
								}
								else if ((this.CharacterAnimation[AnimNames.FemaleElectrocution].time > (7.0f * 0.85f)) &&
									(this.CharacterAnimation[AnimNames.FemaleElectrocution].time < this.CharacterAnimation[AnimNames.FemaleElectrocution].length))
								{
									if (this.LightSwitch.Flicker)
									{
										this.CharacterAnimation[AnimNames.FemaleElectrocution].speed = 1.0f;

										this.Prompt.Label[0].text = "     " + "Turn Off";
										this.LightSwitch.BathroomLight.SetActive(true);

										this.RightDrill.localEulerAngles = new Vector3(0.0f, 0.0f, 68.30447f);
										this.LeftDrill.localEulerAngles = new Vector3(0.0f, -180.0f, -159.417f);

										this.LightSwitch.Flicker = false;

										this.Unspook();
										this.UnWet();
									}
								}
								else if (this.CharacterAnimation[AnimNames.FemaleElectrocution].time >=
									this.CharacterAnimation[AnimNames.FemaleElectrocution].length)
								{
									this.Police.ElectrocutedStudentName = this.Name;
									this.Police.ElectroScene = true;
									this.Electrocuted = true;
									this.BecomeRagdoll();
									this.DeathType = DeathType.Electrocution;
								}
							}
						}
					}
				}
				else
				{
					if (this.Pathfinding.canMove)
					{
						if (this.BathePhase == 1 || this.FleeWhenClean)
						{
                            this.CharacterAnimation[this.WetAnim].weight = 1.0f;
                            this.CharacterAnimation.Play(this.WetAnim);

                            if (this.Persona == PersonaType.PhoneAddict && !this.Phoneless)
							{
								this.CharacterAnimation.CrossFade(this.OriginalSprintAnim);
							}
							else
							{
								this.CharacterAnimation.CrossFade(this.SprintAnim);
							}

							//Debug.Log("Sprinting 12");
							this.Pathfinding.speed = 4.0f;
						}
						else
						{
							if (this.Persona == PersonaType.PhoneAddict && !this.Phoneless)
							{
								this.CharacterAnimation.CrossFade(this.OriginalWalkAnim);
							}
							else
							{
								this.CharacterAnimation.CrossFade(this.WalkAnim);
							}

							this.Pathfinding.speed = 1.0f;
						}
					}
				}
			}

			if (this.Distracting)
			{
				//Debug.Log("This student has been told to distract somebody.");

				if (this.DistractionTarget == null)
				{
					this.Distracting = false;
				}
				else
				{
					if (this.DistractionTarget.Dying)
					{
						//This is only called if a student's DistractionTarget has died.

						this.CurrentDestination = this.Destinations[this.Phase];
						this.Pathfinding.target = this.Destinations[this.Phase];

						this.DistractionTarget.TargetedForDistraction = false;
						this.DistractionTarget.Distracted = false;
						this.DistractionTarget.EmptyHands();

						this.Pathfinding.speed = 1.0f;
						this.Distracting = false;
						this.Distracted = false;
						this.CanTalk = true;
						this.Routine = true;
					}
					else
					{
						if (this.Actions[this.Phase] == StudentActionType.ClubAction &&
							this.Club == ClubType.Cooking && this.ClubActivityPhase > 0)
						{
							if (this.DistractionTarget.InEvent)
							{
								this.GetFoodTarget();
							}
						}

						if (this.DistanceToDestination < 5 || this.DistractionTarget.Leaving)
						{
							//Debug.Log("A distracting student is less than 5 meters away from their target.");

							if (this.DistractionTarget.InEvent || this.DistractionTarget.Talking ||
								this.DistractionTarget.Following || this.DistractionTarget.TurnOffRadio ||
								this.DistractionTarget.Splashed || this.DistractionTarget.Shoving ||
								this.DistractionTarget.Spraying || this.DistractionTarget.FocusOnYandere ||
								this.DistractionTarget.ShoeRemoval.enabled || this.DistractionTarget.Posing ||
								this.DistractionTarget.ClubActivityPhase >= 16 || !this.DistractionTarget.enabled ||
								this.DistractionTarget.Alarmed || this.DistractionTarget.Fleeing ||
								this.DistractionTarget.Wet || this.DistractionTarget.EatingSnack ||
								this.DistractionTarget.MyBento.Tampered || this.DistractionTarget.Meeting ||
								this.DistractionTarget.InvestigatingBloodPool || this.DistractionTarget.ReturningMisplacedWeapon ||
								this.StudentManager.LockerRoomArea.bounds.Contains(this.DistractionTarget.transform.position) ||
								this.StudentManager.WestBathroomArea.bounds.Contains(this.DistractionTarget.transform.position) ||
								this.StudentManager.EastBathroomArea.bounds.Contains(this.DistractionTarget.transform.position) ||
								this.StudentManager.HeadmasterArea.bounds.Contains(this.DistractionTarget.transform.position) ||
								this.DistractionTarget.Actions[this.DistractionTarget.Phase] == StudentActionType.Bully && this.DistractionTarget.DistanceToDestination < 1 ||
								this.DistractionTarget.Leaving || this.DistractionTarget.CameraReacting)
							{
								//Debug.Log("A distracting student's target is in a state which prevents them from being interacted with.");

								//This is only called if a student's DistractionTarget becomes busy with some action or event.

								this.CurrentDestination = this.Destinations[this.Phase];
								this.Pathfinding.target = this.Destinations[this.Phase];

								this.DistractionTarget.TargetedForDistraction = false;
								this.Pathfinding.speed = 1.0f;
								this.Distracting = false;
								this.Distracted = false;
								this.SpeechLines.Stop();
								this.CanTalk = true;
								this.Routine = true;

								if (this.Actions[this.Phase] == StudentActionType.ClubAction &&
									this.Club == ClubType.Cooking && this.ClubActivityPhase > 0)
								{
									this.GetFoodTarget();
								}
							}
							else
							{
								//Debug.Log("There is nothing preventing this student from distracting their target.");

								if (this.DistanceToDestination < this.TargetDistance)
								{
									//Debug.Log("A distracting student is less than " + this.TargetDistance + " from their target.");

									if (!this.DistractionTarget.Distracted)
									{
										if (this.StudentID > 1 && this.DistractionTarget.StudentID > 1)
										{
											if (this.Persona != PersonaType.Fragile && this.DistractionTarget.Persona != PersonaType.Fragile)
											{
												if (this.Club != ClubType.Bully && this.DistractionTarget.Club == ClubType.Bully ||
													this.Club == ClubType.Bully && this.DistractionTarget.Club != ClubType.Bully)
												{
													this.BullyPhotoCollider.SetActive(true);
												}
											}
										}

										if (this.DistractionTarget.Investigating)
										{
											this.DistractionTarget.StopInvestigating();
										}

										//this.DistractionTarget.Prompt.Label[0].text = "     " + "Talk";
										this.StudentManager.UpdateStudents(this.DistractionTarget.StudentID);

										this.DistractionTarget.Pathfinding.canSearch = false;
										this.DistractionTarget.Pathfinding.canMove = false;
										this.DistractionTarget.OccultBook.SetActive(false);
										this.DistractionTarget.SmartPhone.SetActive(false);
										this.DistractionTarget.Distraction = this.transform;
										this.DistractionTarget.CameraReacting = false;
										this.DistractionTarget.Pathfinding.speed = 0.0f;
										this.DistractionTarget.Pen.SetActive(false);
										this.DistractionTarget.Drownable = false;
										this.DistractionTarget.Distracted = true;
										this.DistractionTarget.Pushable = false;
										this.DistractionTarget.Routine = false;
										this.DistractionTarget.CanTalk = false;
										this.DistractionTarget.ReadPhase = 0;

										this.DistractionTarget.SpeechLines.Stop();
										this.DistractionTarget.ChalkDust.Stop();
										this.DistractionTarget.CleanTimer = 0;

										this.DistractionTarget.EmptyHands();

										this.DistractionTarget.Distractor = this;

										//this.DistractionTarget.SpeechLines.Play();
										//this.SpeechLines.Play();

										this.Pathfinding.speed = 0.0f;
										this.Distracted = true;
									}

									this.targetRotation = Quaternion.LookRotation(new Vector3(
										this.DistractionTarget.transform.position.x,
										this.transform.position.y,
										this.DistractionTarget.transform.position.z) - this.transform.position);
									this.transform.rotation = Quaternion.Slerp(
										this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);

									//If we're a member of the cooking club sharing food...
									if (this.Actions[this.Phase] == StudentActionType.ClubAction &&
										this.Club == ClubType.Cooking && this.ClubActivityPhase > 0)
									{
										this.CharacterAnimation.CrossFade(this.IdleAnim);
									}
									//If we're just a normal student...
									else
									{
										this.DistractionTarget.SpeechLines.Play();
										this.SpeechLines.Play();

										this.CharacterAnimation.CrossFade(this.RandomAnim);

										if (this.CharacterAnimation[this.RandomAnim].time >=
											this.CharacterAnimation[this.RandomAnim].length)
										{
											this.PickRandomAnim();
										}
									}

									this.DistractTimer -= Time.deltaTime;

									if (this.DistractTimer <= 0.0f)
									{
										//This is only called after a student has distracted abother student.

										this.CurrentDestination = this.Destinations[this.Phase];
										this.Pathfinding.target = this.Destinations[this.Phase];

										this.DistractionTarget.TargetedForDistraction = false;
										this.DistractionTarget.Pathfinding.canSearch = true;
										this.DistractionTarget.Pathfinding.canMove = true;
										this.DistractionTarget.Pathfinding.speed = 1.0f;
										this.DistractionTarget.Octodog.SetActive(false);
										this.DistractionTarget.Distraction = null;
										this.DistractionTarget.Distracted = false;
										this.DistractionTarget.CanTalk = true;
										this.DistractionTarget.Routine = true;

										this.DistractionTarget.EquipCleaningItems();
										this.DistractionTarget.EatingSnack = false;
										this.Private = false;

										this.DistractionTarget.CurrentDestination = this.DistractionTarget.Destinations[this.Phase];
										this.DistractionTarget.Pathfinding.target = this.DistractionTarget.Destinations[this.Phase];

										if (this.DistractionTarget.Persona == PersonaType.PhoneAddict)
										{
											this.DistractionTarget.SmartPhone.SetActive(true);
										}

										this.DistractionTarget.Distractor = null;

										this.DistractionTarget.SpeechLines.Stop();
										this.SpeechLines.Stop();

										this.BullyPhotoCollider.SetActive(false);
										this.Pathfinding.speed = 1.0f;
										this.Distracting = false;
										this.Distracted = false;
										this.CanTalk = true;
										this.Routine = true;

										if (this.Actions[this.Phase] == StudentActionType.ClubAction &&
											this.Club == ClubType.Cooking && this.ClubActivityPhase > 0)
										{
											//this.DistractionTarget.Fed = true;
											this.GetFoodTarget();
										}
									}
								}
								else
								{
									if (this.Actions[this.Phase] == StudentActionType.ClubAction &&
										this.Club == ClubType.Cooking && this.ClubActivityPhase > 0)
									{
										this.CharacterAnimation.CrossFade(this.WalkAnim);

										this.Pathfinding.canSearch = true;
										this.Pathfinding.canMove = true;
									}
									else
									{
										if (this.Pathfinding.speed == 1)
										{
											this.CharacterAnimation.CrossFade(this.WalkAnim);
										}
										else
										{
											this.CharacterAnimation.CrossFade(this.SprintAnim);
										}
									}
								}
							}
						}
						else
						{
							if (this.Actions[this.Phase] == StudentActionType.ClubAction &&
								this.Club == ClubType.Cooking && this.ClubActivityPhase > 0)
							{
								this.CharacterAnimation.CrossFade(this.WalkAnim);

								this.Pathfinding.canSearch = true;
								this.Pathfinding.canMove = true;

								if (this.Phase < (this.ScheduleBlocks.Length - 1))
								{
									if (this.Clock.HourTime >= this.ScheduleBlocks[this.Phase].time)
									{
										this.Routine = true;
									}
								}
							}
							else
							{
								if (this.Pathfinding.speed == 1)
								{
									this.CharacterAnimation.CrossFade(this.WalkAnim);
								}
								else
								{
									this.CharacterAnimation.CrossFade(this.SprintAnim);
								}
							}
						}
					}
				}
			}

			if (this.Hunting)
			{
				if (HuntTarget != null)
				{
					if (HuntTarget.Prompt.enabled)
					{
						if (!HuntTarget.FightingSlave)
						{
							HuntTarget.Prompt.Hide();
							HuntTarget.Prompt.enabled = false;
						}
					}

					this.Pathfinding.target = HuntTarget.transform;
					this.CurrentDestination = HuntTarget.transform;

					if (HuntTarget.Alive && !HuntTarget.Tranquil && !HuntTarget.PinningDown)
					{
						//If we have not yet reached our victim...
						if (this.DistanceToDestination > this.TargetDistance)
						{
							if (this.MurderSuicidePhase == 0)
							{
								if (this.CharacterAnimation[AnimNames.FemaleBrokenStandUp].time >=
									this.CharacterAnimation[AnimNames.FemaleBrokenStandUp].length)
								{
									this.MurderSuicidePhase++;

									this.Pathfinding.canSearch = true;
									this.Pathfinding.canMove = true;

									this.CharacterAnimation.CrossFade (this.WalkAnim);
									this.Pathfinding.speed = 1.15f;
								}
							}
							else if (this.MurderSuicidePhase > 1)
							{
								this.CharacterAnimation.CrossFade (this.WalkAnim);

								//Debug.Log("We're not close enough. Move closer.");
								HuntTarget.MoveTowardsTarget(this.transform.position +
									(this.transform.forward * 0.010f));
							}

							//Another mind-broken slave beat us to our objective.
							if (HuntTarget.Dying || HuntTarget.Struggling || HuntTarget.Ragdoll.enabled)
							{
								this.Hunting = false;
								this.Suicide = true;
							}
						}
						//If we have to wait for something...
						else if (HuntTarget.ClubActivityPhase >= 16)
						{
							this.CharacterAnimation.CrossFade(this.IdleAnim);
						}
						//If we have reached our victim...
						else
						{
							if (this.NEStairs.bounds.Contains(this.transform.position) ||
								this.NWStairs.bounds.Contains(this.transform.position) ||
								this.SEStairs.bounds.Contains(this.transform.position) ||
								this.SWStairs.bounds.Contains(this.transform.position))
							{
								// Do nothing.
							}
							else if (this.NEStairs.bounds.Contains(HuntTarget.transform.position) ||
								this.NWStairs.bounds.Contains(HuntTarget.transform.position) ||
								this.SEStairs.bounds.Contains(HuntTarget.transform.position) ||
								this.SWStairs.bounds.Contains(HuntTarget.transform.position))
							{
								// Do nothing.
							}
							else if (this.Pathfinding.canMove)
							{
								Debug.Log("Slave is attacking target!");

								if (this.HuntTarget.Strength == 9)
								{
									AttackWillFail = true;
								}

								if (!AttackWillFail)
								{
									this.CharacterAnimation.CrossFade(AnimNames.FemaleMurderSuicide);
								}
								else
								{
									this.CharacterAnimation.CrossFade("f02_brokenAttackFailA_00");
                                    this.CharacterAnimation[this.WetAnim].weight = 0.0f;

                                    this.Police.CorpseList[this.Police.Corpses] = this.Ragdoll;
									this.Police.Corpses++;

									GameObjectUtils.SetLayerRecursively(this.gameObject, 11);
									this.tag = "Blood";

									this.Ragdoll.MurderSuicideAnimation = true;
									this.Ragdoll.Disturbing = true;
									this.Dying = true;

									this.HipCollider.enabled = true;
									this.HipCollider.radius = 1;

									this.MurderSuicidePhase = 9;
								}

								this.Pathfinding.canSearch = false;
								this.Pathfinding.canMove = false;
								this.Broken.Subtitle.text = string.Empty;
								this.MyController.radius = 0.0f;

								this.Broken.Done = true;

								if (!AttackWillFail)
								{
									AudioSource.PlayClipAtPoint(this.MurderSuicideSounds,
										this.transform.position + new Vector3(0.0f, 1.0f, 0.0f));

									AudioSource audioSource = this.GetComponent<AudioSource>();
									audioSource.clip = this.MurderSuicideKiller;
									audioSource.Play();
								}

								if (HuntTarget.Shoving)
								{
									Yandere.CannotRecover = false;
								}

								if (this.StudentManager.CombatMinigame.Delinquent == HuntTarget)
								{
									this.StudentManager.CombatMinigame.Stop();
									this.StudentManager.CombatMinigame.ReleaseYandere();
								}

								if (!AttackWillFail)
								{
									HuntTarget.HipCollider.enabled = true;
									HuntTarget.HipCollider.radius = 1;

									HuntTarget.DetectionMarker.Tex.enabled = false;
								}

                                HuntTarget.CharacterAnimation[HuntTarget.WetAnim].weight = 0.0f;

                                HuntTarget.TargetedForDistraction = false;
								HuntTarget.Pathfinding.canSearch = false;
								HuntTarget.Pathfinding.canMove = false;
								HuntTarget.WitnessCamera.Show = false;
								HuntTarget.CameraReacting = false;
								HuntTarget.Investigating = false;
								HuntTarget.Distracting = false;
								HuntTarget.Splashed = false;
								HuntTarget.Alarmed = false;
								HuntTarget.Burning = false;
								HuntTarget.Fleeing = false;
								HuntTarget.Routine = false;
								HuntTarget.Shoving = false;
								HuntTarget.Tripped = false;
								HuntTarget.Wet = false;

								HuntTarget.Hunter = this;

								HuntTarget.Prompt.Hide();
								HuntTarget.Prompt.enabled = false;

								if (this.Yandere.Pursuer == HuntTarget)
								{
									this.Yandere.Chased = false;
									this.Yandere.Pursuer = null;
								}

								if (!AttackWillFail)
								{
									if (!HuntTarget.Male)
									{
										HuntTarget.CharacterAnimation.CrossFade(AnimNames.FemaleMurderSuicide01);
									}
									else
									{
										HuntTarget.CharacterAnimation.CrossFade(AnimNames.MaleMurderSuicide01);
									}

                                    HuntTarget.CharacterAnimation[HuntTarget.WetAnim].weight = 0.0f;

                                    HuntTarget.Subtitle.UpdateLabel(SubtitleType.Dying, 0, 1.0f);

                                    HuntTarget.GetComponent<AudioSource>().clip = HuntTarget.MurderSuicideVictim;
									HuntTarget.GetComponent<AudioSource>().Play();

									this.Police.CorpseList[this.Police.Corpses] = HuntTarget.Ragdoll;
									this.Police.Corpses++;

									GameObjectUtils.SetLayerRecursively(HuntTarget.gameObject, 11);
									HuntTarget.tag = "Blood";

									HuntTarget.Ragdoll.MurderSuicideAnimation = true;
									HuntTarget.Ragdoll.Disturbing = true;
									HuntTarget.Dying = true;

									HuntTarget.MurderSuicidePhase = 100;
								}
								else
								{
									HuntTarget.CharacterAnimation.CrossFade("f02_brokenAttackFailB_00");
									HuntTarget.FightingSlave = true;
									HuntTarget.Distracted = true;

									StudentManager.UpdateMe(HuntTarget.StudentID);
								}

								HuntTarget.MyController.radius = 0.0f;
								HuntTarget.SpeechLines.Stop();
								HuntTarget.EyeShrink = 1.0f;
								HuntTarget.SpawnAlarmDisc();

								if (HuntTarget.Following)
								{
                                    this.Yandere.Follower = null;
                                    this.Yandere.Followers--;

									ParticleSystem.EmissionModule heartsEmission = this.Hearts.emission;
									heartsEmission.enabled = false;

									HuntTarget.FollowCountdown.gameObject.SetActive(false);
									HuntTarget.Following = false;
								}

								OriginalYPosition = HuntTarget.transform.position.y;

								if (this.MurderSuicidePhase == 0)
								{
									this.MurderSuicidePhase++;
								}
							}
							else
							{
								if (this.MurderSuicidePhase == 0)
								{
									if (this.CharacterAnimation[AnimNames.FemaleBrokenStandUp].time >=
									this.CharacterAnimation[AnimNames.FemaleBrokenStandUp].length)
									{
										this.Pathfinding.canSearch = true;
										this.Pathfinding.canMove = true;
									}
								}

								if (this.MurderSuicidePhase > 0)
								{
									if (!AttackWillFail)
									{
										HuntTarget.targetRotation = Quaternion.LookRotation(
											HuntTarget.transform.position - this.transform.position);

										HuntTarget.MoveTowardsTarget(this.transform.position +
											(this.transform.forward * 0.010f));
									}
									else
									{
										HuntTarget.targetRotation = Quaternion.LookRotation(
											this.transform.position - HuntTarget.transform.position);

										HuntTarget.MoveTowardsTarget(this.transform.position +
											(this.transform.forward * 1));
									}

									HuntTarget.transform.rotation = Quaternion.Slerp(
										HuntTarget.transform.rotation, HuntTarget.targetRotation, Time.deltaTime * 10.0f);

									HuntTarget.transform.position = new Vector3(
										HuntTarget.transform.position.x,
										OriginalYPosition,
										HuntTarget.transform.position.z);

									this.targetRotation = Quaternion.LookRotation(
										HuntTarget.transform.position - this.transform.position);
									this.transform.rotation = Quaternion.Slerp(
										this.transform.rotation, this.targetRotation, Time.deltaTime * 10.0f);
								}

								if (this.MurderSuicidePhase == 1)
								{
									if (this.CharacterAnimation[AnimNames.FemaleMurderSuicide].time >= (72.0f / 30.0f))
									{
										this.MyWeapon.transform.parent = this.ItemParent;
										this.MyWeapon.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
										this.MyWeapon.transform.localPosition = Vector3.zero;
										this.MyWeapon.transform.localEulerAngles = new Vector3(0.0f, 180.0f, 0.0f);

										this.MurderSuicidePhase++;
									}
								}
								else if (this.MurderSuicidePhase == 2)
								{
									if (this.CharacterAnimation[AnimNames.FemaleMurderSuicide].time >= (99.0f / 30.0f))
									{
										GameObject NewBloodPool = Instantiate(this.Ragdoll.BloodPoolSpawner.BloodPool,
											this.transform.position + (this.transform.up * .012f) + this.transform.forward, Quaternion.identity);
										NewBloodPool.transform.localEulerAngles = new Vector3(
											90.0f, Random.Range(0.0f, 360.0f), 0.0f);
										NewBloodPool.transform.parent = this.Police.BloodParent;

										this.MyWeapon.Victims[HuntTarget.StudentID] = true;

										this.MyWeapon.Blood.enabled = true;

										if (!this.MyWeapon.Evidence)
										{
											this.MyWeapon.MurderWeapon = true;
											this.MyWeapon.Evidence = true;
											this.Police.MurderWeapons++;
										}

										Instantiate(this.BloodEffect, this.MyWeapon.transform.position, Quaternion.identity);
										this.KnifeDown = true;

										this.MurderSuicidePhase++;
									}
								}
								else if (this.MurderSuicidePhase == 3)
								{
									if (!this.KnifeDown)
									{
										if (this.MyWeapon.transform.position.y < (this.transform.position.y + (1.0f / 3.0f)))
										{
											Instantiate(this.BloodEffect, this.MyWeapon.transform.position, Quaternion.identity);

											this.KnifeDown = true;

											Debug.Log("Stab!");
										}
									}
									else
									{
										if (this.MyWeapon.transform.position.y > (this.transform.position.y + (1.0f / 3.0f)))
										{
											this.KnifeDown = false;
										}
									}

									if (this.CharacterAnimation[AnimNames.FemaleMurderSuicide].time >= (500.0f / 30.0f))
									{
										Debug.Log("Released knife!");

										this.MyWeapon.transform.parent = null;

										this.MurderSuicidePhase++;
									}
								}
								else if (this.MurderSuicidePhase == 4)
								{
									if (this.CharacterAnimation[AnimNames.FemaleMurderSuicide].time >= (567.0f / 30.0f))
									{
										Debug.Log("Yanked out knife!");

										Instantiate(this.BloodEffect, this.MyWeapon.transform.position, Quaternion.identity);

										this.MyWeapon.transform.parent = this.ItemParent;
										this.MyWeapon.transform.localPosition = Vector3.zero;
										this.MyWeapon.transform.localEulerAngles = Vector3.zero;

										this.MurderSuicidePhase++;
									}
								}
								else if (this.MurderSuicidePhase == 5)
								{
									if (this.CharacterAnimation[AnimNames.FemaleMurderSuicide].time >= (785.0f / 30.0f))
									{
										Debug.Log("Stabbed neck!");

										Instantiate(this.BloodEffect, this.MyWeapon.transform.position, Quaternion.identity);

										this.MyWeapon.Victims[this.StudentID] = true;

										this.MurderSuicidePhase++;
									}
								}
								else if (this.MurderSuicidePhase == 6)
								{
									if (this.CharacterAnimation[AnimNames.FemaleMurderSuicide].time >= (915.0f / 30.0f))
									{
										Debug.Log("BLOOD FOUNTAIN!");

										// [af] Commented in JS code.
										//BloodFountain.gameObject.audio.Play();

										this.BloodFountain.Play();

										this.MurderSuicidePhase++;
									}
								}
								else if (this.MurderSuicidePhase == 7)
								{
									if (this.CharacterAnimation[AnimNames.FemaleMurderSuicide].time >= (945.0f / 30.0f))
									{
										// [af] Commented in JS code.
										//Ragdoll.BloodPoolSpawner.SpawnRow(transform);

										this.BloodSprayCollider.SetActive(true);

										this.MurderSuicidePhase++;
									}
								}
								else if (this.MurderSuicidePhase == 8)
								{
									if (this.CharacterAnimation[AnimNames.FemaleMurderSuicide].time >=
										this.CharacterAnimation[AnimNames.FemaleMurderSuicide].length)
									{
										this.MyWeapon.transform.parent = null;
										this.MyWeapon.Drop();
										this.MyWeapon = null;

										this.StudentManager.StopHesitating();

										HuntTarget.HipCollider.radius = .5f;

										HuntTarget.BecomeRagdoll();
										HuntTarget.Ragdoll.Disturbing = false;
										HuntTarget.Ragdoll.MurderSuicide = true;
										HuntTarget.DeathType = DeathType.Weapon;

										if (this.BloodSprayCollider != null)
										{
											this.BloodSprayCollider.SetActive(false);
										}

										this.BecomeRagdoll();
										this.DeathType = DeathType.Weapon;

										this.StudentManager.MurderTakingPlace = false;
										this.Police.MurderSuicideScene = true;
										this.Ragdoll.MurderSuicide = true;
										this.MurderSuicide = true;

										this.Broken.HairPhysics[0].enabled = true;
										this.Broken.HairPhysics[1].enabled = true;
										this.Broken.enabled = false;

                                        this.Hunting = false;
                                    }
								}
								else if (this.MurderSuicidePhase == 9)
								{
									this.CharacterAnimation.CrossFade("f02_brokenAttackFailA_00");

									if (this.CharacterAnimation["f02_brokenAttackFailA_00"].time >=
										this.CharacterAnimation["f02_brokenAttackFailA_00"].length)
									{
										this.MurderSuicidePhase = 1;
										this.Hunting = false;
										this.Suicide = true;

										this.HuntTarget.MyController.radius = .1f;
										this.HuntTarget.Distracted = false;
										this.HuntTarget.Routine = true;
									}
								}
							}
						}
					}
					else
					{
						this.Hunting = false;
						this.Suicide = true;
					}
				}
				else
				{
					this.Hunting = false;
					this.Suicide = true;
				}
			}

			if (this.Suicide)
			{
				if (this.MurderSuicidePhase == 0)
				{
					if (this.CharacterAnimation[AnimNames.FemaleBrokenStandUp].time >=
						this.CharacterAnimation[AnimNames.FemaleBrokenStandUp].length)
					{
						this.MurderSuicidePhase++;

						this.Pathfinding.canSearch = false;
						this.Pathfinding.canMove = false;
						this.Pathfinding.speed = 0.0f;

						this.CharacterAnimation.CrossFade(AnimNames.FemaleSuicide);
					}
				}
				else if (this.MurderSuicidePhase == 1)
				{
					if (this.Pathfinding.speed > 0.0f)
					{
						this.Pathfinding.canSearch = false;
						this.Pathfinding.canMove = false;
						this.Pathfinding.speed = 0.0f;

						this.CharacterAnimation.CrossFade(AnimNames.FemaleSuicide);
					}

					if (this.CharacterAnimation[AnimNames.FemaleSuicide].time >= (22.0f / 30.0f))
					{
						this.MyWeapon.transform.parent = this.ItemParent;
						this.MyWeapon.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
						this.MyWeapon.transform.localPosition = Vector3.zero;
						this.MyWeapon.transform.localEulerAngles = Vector3.zero;

						this.Broken.Subtitle.text = string.Empty;
						this.Broken.Done = true;

						this.MurderSuicidePhase++;
					}
				}
				else if (this.MurderSuicidePhase == 2)
				{
					if (this.CharacterAnimation[AnimNames.FemaleSuicide].time >= (125.0f / 30.0f))
					{
						Debug.Log("Stabbed neck!");

						Instantiate(this.StabBloodEffect, this.MyWeapon.transform.position, Quaternion.identity);

						this.MyWeapon.Victims[this.StudentID] = true;

						this.MyWeapon.Blood.enabled = true;

						if (!this.MyWeapon.Evidence)
						{
							this.MyWeapon.Evidence = true;
							this.Police.MurderWeapons++;
						}

						this.MurderSuicidePhase++;
					}
				}
				else if (this.MurderSuicidePhase == 3)
				{
					if (this.CharacterAnimation[AnimNames.FemaleSuicide].time >= (185.0f / 30.0f))
					{
						Debug.Log("BLOOD FOUNTAIN!");

						this.BloodFountain.gameObject.GetComponent<AudioSource>().Play();
						this.BloodFountain.Play();

						this.MurderSuicidePhase++;
					}
				}
				else if (this.MurderSuicidePhase == 4)
				{
					if (this.CharacterAnimation[AnimNames.FemaleSuicide].time >= (210.0f / 30.0f))
					{
						this.Ragdoll.BloodPoolSpawner.SpawnPool(this.transform);
						this.BloodSprayCollider.SetActive(true);

						this.MurderSuicidePhase++;
					}
				}
				else if (this.MurderSuicidePhase == 5)
				{
					if (this.CharacterAnimation[AnimNames.FemaleSuicide].time >=
						this.CharacterAnimation[AnimNames.FemaleSuicide].length)
					{
						this.MyWeapon.transform.parent = null;
						this.MyWeapon.Drop();
						this.MyWeapon = null;

						this.StudentManager.StopHesitating();

						if (this.BloodSprayCollider != null)
						{
							this.BloodSprayCollider.SetActive(false);
						}
							
						this.BecomeRagdoll();
						this.DeathType = DeathType.Weapon;

						this.Broken.HairPhysics[0].enabled = true;
						this.Broken.HairPhysics[1].enabled = true;
						this.Broken.enabled = false;
					}
				}
			}

			if (this.CameraReacting)
			{
				this.PhotoPatience = Mathf.MoveTowards(this.PhotoPatience, 0, Time.deltaTime);

				this.Prompt.Circle[0].fillAmount = 1.0f;

				//this.targetRotation = Quaternion.LookRotation(
				//	this.Yandere.transform.position - this.transform.position);

				this.targetRotation = Quaternion.LookRotation(new Vector3(
					this.Yandere.transform.position.x,
					this.transform.position.y,
					this.Yandere.transform.position.z) - this.transform.position);

				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);
				
				if (!this.Yandere.ClubAccessories[7].activeInHierarchy || this.Club == ClubType.Delinquent)
				{
					if (this.CameraReactPhase == 1)
					{
						if (this.CharacterAnimation[this.CameraAnims[1]].time >=
							this.CharacterAnimation[this.CameraAnims[1]].length)
						{
							this.CharacterAnimation.CrossFade(this.CameraAnims[2]);
							this.CameraReactPhase = 2;
							this.CameraPoseTimer = 1.0f;
						}
					}
					else if (this.CameraReactPhase == 2)
					{
						this.CameraPoseTimer -= Time.deltaTime;

						if (this.CameraPoseTimer <= 0.0f)
						{
							this.CharacterAnimation.CrossFade(this.CameraAnims[3]);
							this.CameraReactPhase = 3;
						}
					}
					else if (this.CameraReactPhase == 3)
					{
						if (this.CameraPoseTimer == 1.0f)
						{
							this.CharacterAnimation.CrossFade(this.CameraAnims[2]);
							this.CameraReactPhase = 2;
						}

						if (this.CharacterAnimation[this.CameraAnims[3]].time >=
							this.CharacterAnimation[this.CameraAnims[3]].length)
						{
							if (!this.Phoneless)
							{
								if (this.Persona == PersonaType.PhoneAddict || this.Sleuthing){this.SmartPhone.SetActive(true);}
							}

							if (!this.Male)
							{
								if (this.Cigarette.activeInHierarchy){this.SmartPhone.SetActive(false);}
							}

							this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
							this.Obstacle.enabled = false;
							this.CameraReacting = false;
							this.Routine = true;
							this.ReadPhase = 0;

							if (this.Actions[this.Phase] == StudentActionType.Clean)
							{
								this.Scrubber.SetActive(true);

								if (this.CleaningRole == 5)
								{
									this.Eraser.SetActive(true);
								}
							}
						}
					}
				}
				else
				{
					if (this.Yandere.Shutter.TargetStudent != this.StudentID)
					{
						this.CameraPoseTimer = Mathf.MoveTowards(this.CameraPoseTimer, 0, Time.deltaTime);

						if (this.CameraPoseTimer == 0)
						{
							if (!this.Phoneless)
							{
								if (this.Persona == PersonaType.PhoneAddict || this.Sleuthing){this.SmartPhone.SetActive(true);}
							}

							this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
							this.Obstacle.enabled = false;
							this.CameraReacting = false;
							this.Routine = true;
							this.ReadPhase = 0;

							if (this.Actions[this.Phase] == StudentActionType.Clean)
							{
								this.Scrubber.SetActive(true);

								if (this.CleaningRole == 5)
								{
									this.Eraser.SetActive(true);
								}
							}
						}
					}
					else
					{
						this.CameraPoseTimer = 1.0f;
					}
				}

				if (this.InEvent)
				{
					this.CameraReacting = false;
					this.ReadPhase = 0;
				}
			}

			if (this.Investigating)
			{
				//Debug.Log (this.Name + " is investigating.");

				if (!this.YandereInnocent)
				{
					if (this.InvestigationPhase < 100)
					{
						if (this.CanSeeObject(this.Yandere.gameObject, this.Yandere.HeadPosition))
						{
							if (Vector3.Distance(this.Yandere.transform.position, this.Giggle.transform.position) > 2.50f)
							{
								this.YandereInnocent = true;
							}
							else
							{
								this.CharacterAnimation.CrossFade(this.IdleAnim);

								this.Pathfinding.canSearch = false;
								this.Pathfinding.canMove = false;

								this.InvestigationPhase = 100;
								this.InvestigationTimer = 0.0f;
							}
						}
					}
				}

				if (this.InvestigationPhase == 0)
				{
					if (this.InvestigationTimer < 5.0f)
					{
						//Debug.Log (this.Name + " is hesitating for a moment.");

						if (this.Persona == PersonaType.Heroic)
						{
							this.InvestigationTimer += Time.deltaTime * 5;
						}
						else if (this.Persona == PersonaType.Protective)
						{
							this.InvestigationTimer += Time.deltaTime * 50;
						}
						else
						{
							this.InvestigationTimer += Time.deltaTime;
						}

						this.targetRotation = Quaternion.LookRotation(new Vector3(
							this.Giggle.transform.position.x,
							this.transform.position.y,
							this.Giggle.transform.position.z) - this.transform.position);
						this.transform.rotation = Quaternion.Slerp(
							this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);
					}
					else
					{
						//Debug.Log (this.Name + " reached the part of Investigating where she is being told to run.");

						this.CharacterAnimation.CrossFade(this.IdleAnim);

						this.Pathfinding.target = this.Giggle.transform;
						this.CurrentDestination = this.Giggle.transform;

						this.Pathfinding.canSearch = true;
						this.Pathfinding.canMove = true;

						if (this.Persona == PersonaType.Heroic && this.HeardScream ||
							this.Persona == PersonaType.Protective && this.HeardScream)
						{
							//Debug.Log("Sprinting 13");
							this.Pathfinding.speed = 4;
						}
						else
						{
							this.Pathfinding.speed = 1.0f;
						}

						this.InvestigationPhase++;
					}
				}
				else if (this.InvestigationPhase == 1)
				{
					//Debug.Log (this.Name + " reached Investigation Phase 1.");

					this.Pathfinding.canSearch = true;
					this.Pathfinding.canMove = true;

					if (this.DistanceToDestination > 1.0f)
					{
						if (this.Persona == PersonaType.Heroic && this.HeardScream ||
							this.Persona == PersonaType.Protective && this.HeardScream)
						{
							this.CharacterAnimation.CrossFade(this.SprintAnim);
						}
						else
						{
							this.CharacterAnimation.CrossFade(this.WalkAnim);
						}
					}
					else
					{
						this.CharacterAnimation.CrossFade(this.IdleAnim);

						this.Pathfinding.canSearch = false;
						this.Pathfinding.canMove = false;

						this.InvestigationPhase++;
					}
				}
				else if (this.InvestigationPhase == 2)
				{
					this.InvestigationTimer += Time.deltaTime;

					if (this.InvestigationTimer > 10.0f)
					{
						this.StopInvestigating();
					}
				}
				else if (this.InvestigationPhase == 100)
				{
					this.targetRotation = Quaternion.LookRotation(new Vector3(
						this.Yandere.transform.position.x,
						this.transform.position.y,
						this.Yandere.transform.position.z) - this.transform.position);
					this.transform.rotation = Quaternion.Slerp(
						this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);

					this.InvestigationTimer += Time.deltaTime;

					if (this.InvestigationTimer > 2.0f)
					{
						this.StopInvestigating();
					}
				}
			}

			if (this.EndSearch)
			{
				this.MoveTowardsTarget(this.Pathfinding.target.position);

				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.Pathfinding.target.rotation, 10.0f * Time.deltaTime);

				this.PatrolTimer += Time.deltaTime * this.CharacterAnimation[this.PatrolAnim].speed;

				if (this.PatrolTimer > 5)
				{
					this.StudentManager.CommunalLocker.RivalPhone.ReturnToOrigin();

					ScheduleBlock newBlock2 = this.ScheduleBlocks[2];
					newBlock2.destination = "Hangout";
					newBlock2.action = "Hangout";

					if (this.Club == ClubType.Cooking)
					{
						newBlock2.destination = "Club";
						newBlock2.action = "Club";
					}

					ScheduleBlock newBlock4 = this.ScheduleBlocks[4];
					newBlock4.destination = "LunchSpot";
					newBlock4.action = "Eat";

					ScheduleBlock newBlock7 = this.ScheduleBlocks[7];
					newBlock7.destination = "Hangout";
					newBlock7.action = "Hangout";

					this.GetDestinations();

					System.Array.Copy(this.OriginalActions, this.Actions, this.OriginalActions.Length);

					//This is only called after a student finds their missing phone.
					this.CurrentDestination = this.Destinations[this.Phase];
					this.Pathfinding.target = this.Destinations[this.Phase];
					this.CurrentAction = this.Actions[this.Phase];

					this.DistanceToDestination = 100.0f;

					LewdPhotos = this.StudentManager.CommunalLocker.RivalPhone.LewdPhotos;
					EndSearch = false;
					Phoneless = false;
					Routine = true;
				}
			}

			if (this.Shoving)
			{
				this.CharacterAnimation.CrossFade(this.ShoveAnim);

				if (this.CharacterAnimation[this.ShoveAnim].time > this.CharacterAnimation[this.ShoveAnim].length)
				{
					if (this.Club != ClubType.Council && this.Persona != PersonaType.Violent || this.RespectEarned)
					{
						this.Patience = 999;
					}

					if (this.Patience > 0)
					{
						this.Pathfinding.canSearch = true;
						this.Pathfinding.canMove = true;

						this.Distracted = false;
						this.Shoving = false;
						this.Routine = true;
						this.Paired = false;
					}
					else
					{
						if (this.Club == ClubType.Council)
						{
							this.Shoving = false;
							this.Spray();
						}
						else
						{
							this.SpawnAlarmDisc();

                            //this.SilentlyForgetBloodPool();

                            this.SmartPhone.SetActive(false);
							this.Distracted = true;
							this.Threatened = true;
							this.Shoving = false;
							this.Alarmed = true;
						}
					}
				}
			}

			if (this.Spraying)
			{
				this.Yandere.CharacterAnimation.CrossFade("f02_sprayed_00");
				this.Yandere.Attacking = false;

				if (this.CharacterAnimation[this.SprayAnim].time > .66666)
				{
					if (this.Yandere.Armed)
					{
						this.Yandere.EquippedWeapon.Drop();
					}

					this.Yandere.EmptyHands();

					this.PepperSprayEffect.Play();
					this.Spraying = false;
                    this.enabled = false;
				}
			}

			if (this.SentHome)
			{
				if (this.SentHomePhase == 0)
				{
					if (this.Shy)
					{
						this.CharacterAnimation[this.ShyAnim].weight = 0;
					}

					this.CharacterAnimation.CrossFade(this.SentHomeAnim);
					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;
					this.SentHomePhase++;

					if (this.SmartPhone.activeInHierarchy)
					{
						this.CharacterAnimation[this.SentHomeAnim].time = 1.5f;
						SentHomePhase++;
					}
				}
				else if (this.SentHomePhase == 1)
				{
					if (this.CharacterAnimation[this.SentHomeAnim].time > .66666f)
					{
						this.SmartPhone.SetActive(true);
						this.SentHomePhase++;
					}
				}
				else if (this.SentHomePhase == 2)
				{
					if (this.CharacterAnimation[this.SentHomeAnim].time > this.CharacterAnimation[this.SentHomeAnim].length)
					{
						//Debug.Log("Sprinting 14");

						this.SprintAnim = this.OriginalSprintAnim;
						this.CharacterAnimation.CrossFade(this.SprintAnim);
						this.CurrentDestination = this.StudentManager.Exit;
						this.Pathfinding.target = this.StudentManager.Exit;
						this.Pathfinding.canSearch = true;
						this.Pathfinding.canMove = true;
						this.SmartPhone.SetActive(false);
						this.Pathfinding.speed = 4;
						this.SentHomePhase++;
					}
				}
			}

			if (this.DramaticReaction)
			{
				this.DramaticCamera.transform.Translate(Vector3.forward * Time.deltaTime * .01f);

				if (this.DramaticPhase == 0)
				{
					this.DramaticCamera.rect = new Rect(
						0,
						Mathf.Lerp(this.DramaticCamera.rect.y, 0.25f, Time.deltaTime * 10.0f),
						1,
						Mathf.Lerp(this.DramaticCamera.rect.height, .5f, Time.deltaTime * 10.0f));

					this.DramaticTimer += Time.deltaTime;

					if (this.DramaticTimer > 1)
					{
						this.DramaticTimer = 0;
						this.DramaticPhase++;
					}
				}
				else if (this.DramaticPhase == 1)
				{
					this.DramaticCamera.rect = new Rect(
						0,
						Mathf.Lerp(this.DramaticCamera.rect.y, 0.5f, Time.deltaTime * 10.0f),
						1,
						Mathf.Lerp(this.DramaticCamera.rect.height, 0, Time.deltaTime * 10.0f));

					this.DramaticTimer += Time.deltaTime;

					if (this.DramaticCamera.rect.height < .01f || this.DramaticTimer > .5f)
					{
						Debug.Log("Disabling Dramatic Camera.");

						this.DramaticCamera.gameObject.SetActive(false);
						this.AttackReaction();
						this.DramaticPhase++;
					}
				}
			}

			//This code will only run if the character is reacting to an attack from the Sith easter egg.
			if (this.HitReacting)
			{
				if (this.CharacterAnimation[this.SithReactAnim].time >=
					this.CharacterAnimation[this.SithReactAnim].length)
				{
					this.Persona = PersonaType.SocialButterfly;
					this.PersonaReaction();
					this.HitReacting = false;
				}
			}

			if (this.Eaten)
			{
				if (this.Yandere.Eating)
				{
					this.targetRotation = Quaternion.LookRotation(new Vector3(
						this.Yandere.transform.position.x,
						this.transform.position.y,
						this.Yandere.transform.position.z) - this.transform.position);
					this.transform.rotation = Quaternion.Slerp(
						this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);
				}

				if (this.CharacterAnimation[EatVictimAnim].time >= this.CharacterAnimation[EatVictimAnim].length)
				{
					this.BecomeRagdoll();
				}
			}

			if (this.Electrified)
			{
				if (this.CharacterAnimation[this.ElectroAnim].time >=
					this.CharacterAnimation[this.ElectroAnim].length)
				{
					this.Electrocuted = true;
					this.BecomeRagdoll();
					this.DeathType = DeathType.Electrocution;
				}
			}

			if (this.BreakingUpFight)
			{
				this.targetRotation = this.Yandere.transform.rotation * Quaternion.Euler(0, 90, 0);

				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);

				this.MoveTowardsTarget(this.Yandere.transform.position + this.Yandere.transform.forward * .5f);

				if (this.StudentID == 87)
				{
					if (this.CharacterAnimation[this.BreakUpAnim].time >= 4)
					{
						this.CandyBar.SetActive(false);
					}
					else if (this.CharacterAnimation[this.BreakUpAnim].time >= 0.16666666666)
					{
						this.CandyBar.SetActive(true);
					}
				}

				if (this.CharacterAnimation[this.BreakUpAnim].time >=
					this.CharacterAnimation[this.BreakUpAnim].length)
				{
					//this.Yandere.FightHasBrokenUp = false;
					this.ReturnToRoutine();
				}
			}

			if (this.Tripping)
			{
				this.MoveTowardsTarget(new Vector3(
					0,
					0,
					this.transform.position.z));

				this.CharacterAnimation.CrossFade("trip_00");
				this.Pathfinding.canSearch = false;
				this.Pathfinding.canMove = false;

				if (this.CharacterAnimation["trip_00"].time >= .5f && this.CharacterAnimation["trip_00"].time <= 5.5f)
				{
					if (this.StudentManager.Gate.Crushing)
					{
						this.BecomeRagdoll();
						this.DeathType = DeathType.Weight;

						this.Ragdoll.Decapitated = true;
						Instantiate(this.SquishyBloodEffect, this.Head.position, Quaternion.identity);
					}
				}

				if (this.CharacterAnimation["trip_00"].time >= this.CharacterAnimation["trip_00"].length)
				{
                    this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
                    this.Pathfinding.canSearch = true;
					this.Pathfinding.canMove = true;
					this.Distracted = true;
					this.Tripping = false;
					this.Routine = true;
					this.Tripped = true;
				}
			}

			if (this.SeekingMedicine)
			{
				if (this.StudentManager.Students[90] == null)
				{
					if (this.SeekMedicinePhase < 5)
					{
						this.SeekMedicinePhase = 5;
					}
				}
				else
				{
					if (!this.StudentManager.Students[90].gameObject.activeInHierarchy ||
						this.StudentManager.Students[90].Dying)
					{
						if (this.SeekMedicinePhase < 5)
						{
							this.SeekMedicinePhase = 5;
						}
					}
				}

				//Setting destination to the nurse.
				if (this.SeekMedicinePhase == 0)
				{
					this.CurrentDestination = this.StudentManager.Students[90].transform;
					this.Pathfinding.target = this.StudentManager.Students[90].transform;
					this.SeekMedicinePhase++;
				}
				//Traveling to the nurse.
				else if (this.SeekMedicinePhase == 1)
				{
					this.CharacterAnimation.CrossFade(this.SprintAnim);

					if (this.DistanceToDestination < 1)
					{
						StudentScript Nurse = this.StudentManager.Students[90];

						if (Nurse.Investigating)
						{
							Nurse.StopInvestigating();
						}

						this.StudentManager.UpdateStudents(Nurse.StudentID);

						Nurse.Pathfinding.canSearch = false;
						Nurse.Pathfinding.canMove = false;
						Nurse.RetreivingMedicine = true;
						Nurse.Pathfinding.speed = 0.0f;
						Nurse.CameraReacting = false;
						Nurse.TargetStudent = this;
						Nurse.Routine = false;

						this.CharacterAnimation.CrossFade(this.IdleAnim);
						this.Pathfinding.canSearch = false;
						this.Pathfinding.canMove = false;
						this.Pathfinding.speed = 0.0f;

						this.Subtitle.UpdateLabel(SubtitleType.RequestMedicine, 0, 5);

						this.SeekMedicinePhase++;
					}
				}
				//Speaking to the nurse.
				else if (this.SeekMedicinePhase == 2)
				{
					StudentScript Nurse = this.StudentManager.Students[90];

					this.targetRotation = Quaternion.LookRotation(new Vector3(
						Nurse.transform.position.x,
						this.transform.position.y,
						Nurse.transform.position.z) - this.transform.position);

					this.transform.rotation = Quaternion.Slerp(
						this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);

					this.MedicineTimer += Time.deltaTime;

					if (this.MedicineTimer > 5)
					{
						this.SeekMedicinePhase++;
						this.MedicineTimer = 0;

						Nurse.Pathfinding.canSearch = true;
						Nurse.Pathfinding.canMove = true;
						Nurse.RetrieveMedicinePhase++;
					}
				}
				//Just waiting for the nurse to come back.
				else if (this.SeekMedicinePhase == 3)
				{
					//
				}
				//Receiving medicine from nurse.
				else if (this.SeekMedicinePhase == 4)
				{
					StudentScript Nurse = this.StudentManager.Students[90];

					this.targetRotation = Quaternion.LookRotation(new Vector3(
						Nurse.transform.position.x,
						this.transform.position.y,
						Nurse.transform.position.z) - this.transform.position);

					this.transform.rotation = Quaternion.Slerp(
						this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);
				}
				//Going to sit down on the couch in the infirmary.
				else if (this.SeekMedicinePhase == 5)
				{
					this.Pathfinding.canSearch = true;
					this.Pathfinding.canMove = true;

					ScheduleBlock block = ScheduleBlocks[this.Phase];
					block.destination = "InfirmarySeat";
					block.action = "Relax";

					this.GetDestinations();

					//This is only called when a student is going to sit down in the infirmary.

					this.CurrentDestination = this.Destinations[this.Phase];
					this.Pathfinding.target = this.Destinations[this.Phase];
					this.Pathfinding.speed = 2;

					this.RelaxAnim = this.HeadacheSitAnim;
					this.SeekingMedicine = false;
					this.Routine = true;
				}
			}

			if (this.RetreivingMedicine)
			{
				//Listening to a student ask for medicine.
				if (this.RetrieveMedicinePhase == 0)
				{
					this.CharacterAnimation.CrossFade(this.IdleAnim);

					this.targetRotation = Quaternion.LookRotation(new Vector3(
						TargetStudent.transform.position.x,
						this.transform.position.y,
						TargetStudent.transform.position.z) - this.transform.position);

					this.transform.rotation = Quaternion.Slerp(
						this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);
				}
				//The nurse choses her destination depending on how many times her keys have been stolen.
				else if (this.RetrieveMedicinePhase == 1)
				{
					this.CharacterAnimation.CrossFade(this.WalkAnim);

					this.CurrentDestination = this.StudentManager.MedicineCabinet;
					this.Pathfinding.target = this.StudentManager.MedicineCabinet;
					this.Pathfinding.speed = 1;

					this.RetrieveMedicinePhase++;
				}
				//Traveling to the medicine cabinet.
				else if (this.RetrieveMedicinePhase == 2)
				{
					if (this.DistanceToDestination < 1)
					{
						this.StudentManager.CabinetDoor.Locked = false;
						this.StudentManager.CabinetDoor.Open = true;
						this.StudentManager.CabinetDoor.Timer = 0;

						this.CurrentDestination = this.TargetStudent.transform;
						this.Pathfinding.target = this.TargetStudent.transform;

						this.RetrieveMedicinePhase++;
					}
				}
				//Traveling back to the student.
				else if (this.RetrieveMedicinePhase == 3)
				{
					if (this.DistanceToDestination < 1)
					{
						this.CurrentDestination = this.TargetStudent.transform;
						this.Pathfinding.target = this.TargetStudent.transform;

						this.RetrieveMedicinePhase++;
					}
				}
				//Traveling back to the student.
				else if (this.RetrieveMedicinePhase == 4)
				{
					if (this.DistanceToDestination < 1)
					{
						this.CharacterAnimation.CrossFade("f02_giveItem_00");

						if (this.TargetStudent.Male)
						{
							this.TargetStudent.CharacterAnimation.CrossFade("eatItem_00");
						}
						else
						{
							this.TargetStudent.CharacterAnimation.CrossFade("f02_eatItem_00");
						}

						this.Pathfinding.canSearch = false;
						this.Pathfinding.canMove = false;

						this.TargetStudent.SeekMedicinePhase++;
						this.RetrieveMedicinePhase++;
					}
				}
				//Handing the student medicine.
				else if (this.RetrieveMedicinePhase == 5)
				{
					this.targetRotation = Quaternion.LookRotation(new Vector3(
						TargetStudent.transform.position.x,
						this.transform.position.y,
						TargetStudent.transform.position.z) - this.transform.position);

					this.transform.rotation = Quaternion.Slerp(
						this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);

					this.MedicineTimer += Time.deltaTime;

					if (this.MedicineTimer > 3)
					{
						this.CharacterAnimation.CrossFade(this.WalkAnim);

						this.CurrentDestination = this.StudentManager.MedicineCabinet;
						this.Pathfinding.target = this.StudentManager.MedicineCabinet;

						this.Pathfinding.canSearch = true;
						this.Pathfinding.canMove = true;

						this.TargetStudent.SeekMedicinePhase++;
						this.RetrieveMedicinePhase++;
					}
				}
				//Locking the cabinet
				else if (this.RetrieveMedicinePhase == 6)
				{
					if (this.DistanceToDestination < 1)
					{
						this.StudentManager.CabinetDoor.Locked = true;
						this.StudentManager.CabinetDoor.Open = false;
						this.StudentManager.CabinetDoor.Timer = 0;

						this.RetreivingMedicine = false;
						this.Routine = true;
					}
				}
			}

			if (this.EatingSnack)
			{
				if (this.SnackPhase == 0)
				{
					this.CharacterAnimation.CrossFade(this.EatChipsAnim);
					this.SmartPhone.SetActive(false);

					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;

					this.SnackTimer += Time.deltaTime;

					if (this.SnackTimer > 10)
					{
						Destroy(this.BagOfChips);

						if (this.StudentID != this.StudentManager.RivalID)
						{
							this.StudentManager.GetNearestFountain(this);

							this.Pathfinding.target = this.DrinkingFountain.DrinkPosition;
							this.CurrentDestination = this.DrinkingFountain.DrinkPosition;

							this.Pathfinding.canSearch = true;
							this.Pathfinding.canMove = true;

							this.SnackTimer = 0;
						}

						this.SnackPhase++;
					}
				}
				else if (this.SnackPhase == 1)
				{
					this.CharacterAnimation.CrossFade(this.WalkAnim);

					if (this.Persona == PersonaType.PhoneAddict && !this.Phoneless)
					{
						this.SmartPhone.SetActive(true);
					}

					//Debug.Log("Distance to destination is..." + this.DistanceToDestination);

					if (this.DistanceToDestination < 1)
					{
						this.SmartPhone.SetActive(false);

						this.Pathfinding.canSearch = false;
						this.Pathfinding.canMove = false;

						this.SnackPhase++;
					}
				}
				else if (this.SnackPhase == 2)
				{
					CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;

					this.CharacterAnimation.CrossFade(this.DrinkFountainAnim);

					this.MoveTowardsTarget(this.DrinkingFountain.DrinkPosition.position);

					this.transform.rotation = Quaternion.Slerp(
						this.transform.rotation, this.DrinkingFountain.DrinkPosition.rotation, 10.0f * Time.deltaTime);

					if (this.CharacterAnimation[this.DrinkFountainAnim].time >= this.CharacterAnimation[this.DrinkFountainAnim].length)
					{
						CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;

						this.DrinkingFountain.Occupied = false;

						this.EquipCleaningItems();

						this.EatingSnack = false;
						this.Private = false;
						this.Routine = true;
						this.StudentManager.UpdateMe(this.StudentID);

						this.CurrentDestination = this.Destinations[this.Phase];
						this.Pathfinding.target = this.Destinations[this.Phase];
					}
					else if (this.CharacterAnimation[this.DrinkFountainAnim].time > .5f &&
						     this.CharacterAnimation[this.DrinkFountainAnim].time < 1.5f)
					{
						this.DrinkingFountain.WaterStream.Play();
					}
				}
			}

			if (this.Dodging)
			{
				if (this.CharacterAnimation[this.DodgeAnim].time >=
					this.CharacterAnimation[this.DodgeAnim].length)
				{
					this.Routine = true;
					this.Dodging = false;

					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;

					if (this.GasWarned)
					{
						this.Yandere.Subtitle.UpdateLabel(SubtitleType.GasWarning, 2, 5.0f);
						this.GasWarned = false;
					}
				}
				else
				{
					if (this.CharacterAnimation[this.DodgeAnim].time < .66666f)
					{
						this.MyController.Move(this.transform.forward * -1 * this.DodgeSpeed * Time.deltaTime);
						this.MyController.Move(Physics.gravity * 0.10f);

						if (this.DodgeSpeed > 0)
						{
							this.DodgeSpeed = Mathf.MoveTowards(this.DodgeSpeed, 0, Time.deltaTime * 3);
						}
					}
				}
			}

			if (!this.Guarding && this.InvestigatingBloodPool)
			{
				if (this.DistanceToDestination < 1)
				{
					this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;

					this.CharacterAnimation[InspectBloodAnim].speed = 1;
					this.CharacterAnimation.CrossFade(this.InspectBloodAnim);

					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;
					this.Distracted = true;

					if (this.CharacterAnimation[this.InspectBloodAnim].time >= this.CharacterAnimation[this.InspectBloodAnim].length ||
						this.Persona == PersonaType.Strict)
					{
						bool TooBusy = false;

						if (this.Club == ClubType.Cooking && CurrentAction == StudentActionType.ClubAction)
						{
							TooBusy = true;
						}

						if (this.WitnessedWeapon)
						{
							bool CannotPickUpMetal = false;

							if (!this.Teacher && this.BloodPool.GetComponent<WeaponScript>().Metal &&
								this.StudentManager.MetalDetectors)
							{
								CannotPickUpMetal = true;
							}

							if (!this.WitnessedBloodyWeapon && this.StudentID > 1 && !CannotPickUpMetal &&
								CurrentAction != StudentActionType.SitAndTakeNotes && this.Indoors && !TooBusy &&
								this.Club != ClubType.Delinquent)
							{
								if (!this.BloodPool.GetComponent<WeaponScript>().Dangerous &&
									this.BloodPool.GetComponent<WeaponScript>().Returner == null)
								{
									Debug.Log(this.Name + " has picked up a weapon with intent to return it to its original location.");

									this.CharacterAnimation[this.PickUpAnim].time = 0;

									this.BloodPool.GetComponent<WeaponScript>().Prompt.Hide();
									this.BloodPool.GetComponent<WeaponScript>().Prompt.enabled = false;
									this.BloodPool.GetComponent<WeaponScript>().enabled = false;
									this.BloodPool.GetComponent<WeaponScript>().Returner = this;

									if (this.StudentID == 41){this.Subtitle.UpdateLabel(SubtitleType.Impatience, 6, 5.0f);}
									else{this.Subtitle.UpdateLabel(SubtitleType.ReturningWeapon, 0, 5.0f);}
									this.ReturningMisplacedWeapon = true;
									this.ReportingBlood = false;
									this.Distracted = false;

									this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;

                                    this.Yandere.WeaponManager.ReturnWeaponID = this.BloodPool.GetComponent<WeaponScript>().GlobalID;
                                    this.Yandere.WeaponManager.ReturnStudentID = this.StudentID;
                                }
							}
						}

						this.InvestigatingBloodPool = false;

						this.WitnessCooldownTimer = 5;

						if (this.WitnessedLimb)
						{
							this.SpawnAlarmDisc();
						}

						if (!this.ReturningMisplacedWeapon)
						{
							if (this.StudentManager.BloodReporter == this)
							{
								if (this.WitnessedWeapon && !this.BloodPool.GetComponent<WeaponScript>().Dangerous)
								{
									this.StudentManager.BloodReporter = null;
								}
							}

							//If this is the student who will be reporting the blood...
							if (this.StudentManager.BloodReporter == this && this.StudentID > 1)
							{
								if (this.Persona != PersonaType.Strict && this.Persona != PersonaType.Violent)
								{
									Debug.Log(this.Name + " has changed from their original Persona into a Teacher's Pet.");

									this.Persona = PersonaType.TeachersPet;
								}

								this.PersonaReaction();
							}
							//If this student is not going to report the blood...
							else
							{
								if (this.WitnessedWeapon && !this.WitnessedBloodyWeapon)
								{
                                    this.StopInvestigating();

									this.CurrentDestination = this.Destinations[this.Phase];
									this.Pathfinding.target = this.Destinations[this.Phase];

									this.LastSuspiciousObject2 = this.LastSuspiciousObject;
									this.LastSuspiciousObject = this.BloodPool;

									this.Pathfinding.canSearch = true;
									this.Pathfinding.canMove = true;
									this.Pathfinding.speed = 1.0f;

									if (this.StudentID == 41)
									{
										this.Subtitle.UpdateLabel(SubtitleType.Impatience, 6, 5.0f);
									}
									else if (this.Club == ClubType.Delinquent)
									{
										this.Subtitle.UpdateLabel(SubtitleType.PetWeaponReaction, 2, 3.0f);
									}
									else if (this.BloodPool.GetComponent<WeaponScript>().Dangerous)
									{
										this.Subtitle.UpdateLabel(SubtitleType.PetWeaponReaction, 0, 3.0f);
									}
									else
									{
										this.Subtitle.UpdateLabel(SubtitleType.PetWeaponReaction, 1, 3.0f);
									}

									this.WitnessedSomething = false;
									this.WitnessedWeapon = false;
									this.Distracted = false;
									this.Routine = true;

									this.BloodPool = null;

									if (this.StudentManager.BloodReporter == this)
									{
										if (this.Persona != PersonaType.Strict && this.Persona != PersonaType.Violent)
										{
											Debug.Log(this.Name + " has changed from their original Persona into a Teacher's Pet.");

											this.Persona = PersonaType.TeachersPet;
										}

										this.PersonaReaction();
									}
								}
								else
								{
									//Debug.Log("This student is NOT a blood reporter, and is reacting to the fact that they found blood.");

									if (this.Persona != PersonaType.Strict && this.Persona != PersonaType.Violent)
									{
										Debug.Log(this.Name + " has changed from their original Persona into a Teacher's Pet.");

										this.Persona = PersonaType.TeachersPet;
									}

									this.PersonaReaction();
								}
							}

							this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
						}
					}
				}
                //If the character is currently walking towards the object...
				else
				{
					//For teachers
					if (this.Persona == PersonaType.Strict)
					{
						if (this.WitnessedWeapon && this.BloodPool.GetComponent<WeaponScript>().Returner)
						{
							this.Subtitle.UpdateLabel(SubtitleType.StudentFarewell, 0, 3.0f);

							this.CurrentDestination = this.Destinations[this.Phase];
							this.Pathfinding.target = this.Destinations[this.Phase];

							this.InvestigatingBloodPool = false;
							this.WitnessedBloodyWeapon = false;
							this.WitnessedBloodPool = false;
							this.WitnessedSomething = false;
							this.WitnessedWeapon = false;
							this.Distracted = false;
							this.Routine = true;

							this.BloodPool = null;

							this.WitnessCooldownTimer = 5;
						}
						else
						{
							if (this.BloodPool.parent == this.Yandere.RightHand || !this.BloodPool.gameObject.activeInHierarchy)
							{
                                Debug.Log("Yandere-chan just picked up the weapon that was being investigated.");

								this.InvestigatingBloodPool = false;
								this.WitnessedBloodyWeapon = false;
								this.WitnessedBloodPool = false;
								this.WitnessedSomething = false;
								this.WitnessedWeapon = false;
								this.Distracted = false;
								this.Routine = true;

                                if (this.BloodPool.GetComponent<WeaponScript>() != null && this.BloodPool.GetComponent<WeaponScript>().Suspicious)
                                {
                                    this.WitnessCooldownTimer = 5;
								    this.AlarmTimer = 0.0f;
								    this.Alarm = 200.0f;
                                }

                                this.BloodPool = null;
                            }
						}
					}
					//For everyone else
					else
					{
						if (this.BloodPool == null || this.WitnessedWeapon && this.BloodPool.parent != null ||
							this.WitnessedBloodPool && this.BloodPool.parent == this.Yandere.RightHand ||
							this.WitnessedWeapon && this.BloodPool.GetComponent<WeaponScript>().Returner)
						{
                            this.ForgetAboutBloodPool();
                        }
					}
				}
			}

			if (this.ReturningMisplacedWeapon)
			{
				if (this.ReturningMisplacedWeaponPhase == 0)
				{
					this.CharacterAnimation.CrossFade(this.PickUpAnim);

					if (this.Club == ClubType.Council || this.Teacher)
					{
						if (this.CharacterAnimation[this.PickUpAnim].time >= .33333f)
						{
							this.Handkerchief.SetActive(true);
						}
					}

					if (this.CharacterAnimation[this.PickUpAnim].time >= 2)
					{
						this.BloodPool.parent = this.RightHand;
						this.BloodPool.localPosition = new Vector3(0, 0, 0);
						this.BloodPool.localEulerAngles = new Vector3(0, 0, 0);

						if (this.Club != ClubType.Council && !this.Teacher)
						{
							this.BloodPool.GetComponent<WeaponScript>().FingerprintID = this.StudentID;
						}
					}

					if (this.CharacterAnimation[this.PickUpAnim].time >= this.CharacterAnimation[this.PickUpAnim].length)
					{
						this.CurrentDestination = this.BloodPool.GetComponent<WeaponScript>().Origin;
						this.Pathfinding.target = this.BloodPool.GetComponent<WeaponScript>().Origin;

						this.Pathfinding.canSearch = true;
						this.Pathfinding.canMove = true;

						this.Pathfinding.speed = 1;
						this.Hurry = false;

						this.ReturningMisplacedWeaponPhase++;
					}
				}
				else
				{
					this.CharacterAnimation.CrossFade(this.WalkAnim);

					if (this.DistanceToDestination < 1.1f)
					{
						this.ReturnMisplacedWeapon();
					}
				}
			}

			if (this.SentToLocker)
			{
				if (!this.CheckingNote)
				{
					this.CharacterAnimation.CrossFade(this.RunAnim);
				}
			}

			if (this.Stripping)
			{
				this.CharacterAnimation.CrossFade(this.StripAnim);

				this.Pathfinding.canSearch = false;
				this.Pathfinding.canMove = false;

				if (this.CharacterAnimation[this.StripAnim].time >= 1.50f)
				{
					if (this.Schoolwear != 1)
					{
						this.Schoolwear = 1;
						this.ChangeSchoolwear();
					}

					if (this.CharacterAnimation[this.StripAnim].time > this.CharacterAnimation[this.StripAnim].length)
					{
						this.Pathfinding.canSearch = true;
						this.Pathfinding.canMove = true;

						this.Stripping = false;
						this.Routine = true;
					}
				}
			}

			//Anti-Osana Code
			#if UNITY_EDITOR
			if (this.SenpaiWitnessingRivalDie)
			{
				this.Fleeing = false;

				Debug.Log("Senpai is watching Osana die. Phase is: " + this.WitnessRivalDiePhase);

				if (this.DistanceToDestination < 1)
				{
					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;
				}

				//Senpai reacts to Osana choking on poison.
				if (this.WitnessRivalDiePhase == 0)
				{
					this.CharacterAnimation.CrossFade(AnimNames.MaleWitnessPoisoning);

					//this.CurrentDestination = this.StudentManager.LunchSpots.List[1];
					this.MoveTowardsTarget(this.CurrentDestination.position);

					this.Chopsticks[0].SetActive(true);
					this.Chopsticks[1].SetActive(true);
					this.Bento.SetActive(true);

					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;

					this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
						this.CurrentDestination.rotation, 10.0f * Time.deltaTime);

					if (this.CharacterAnimation[AnimNames.MaleWitnessPoisoning].time >= 18.50f)
					{
						if (this.Bento.activeInHierarchy)
						{
							this.Chopsticks[0].SetActive(false);
							this.Chopsticks[1].SetActive(false);
							this.Bento.SetActive(false);

							this.WitnessRivalDiePhase++;
						}
					}
				}
				//Senpai asks Osana if she's okay.
				else if (this.WitnessRivalDiePhase == 1)
				{
					if (this.CharacterAnimation[AnimNames.MaleWitnessPoisoning].time >= 22.5f)
					{
						this.Subtitle.UpdateLabel(SubtitleType.SenpaiRivalDeathReaction, 0, 8.0f);

						this.WitnessRivalDiePhase++;
					}
				}
				//Senpai realizes that Osana is dead and freaks out with his head in his hands.
				else if (this.WitnessRivalDiePhase == 2)
				{
					if (this.CharacterAnimation[AnimNames.MaleWitnessPoisoning].time >=
						this.CharacterAnimation[AnimNames.MaleWitnessPoisoning].length)
					{
						this.transform.position = new Vector3(this.Hips.position.x, this.transform.position.y, this.Hips.position.z);
						//this.transform.LookAt(this.StudentManager.Students[11].Head.position);
						//this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);

						Physics.SyncTransforms();

						this.CharacterAnimation.Play("senpaiRivalCorpseReaction_00");

						this.TargetDistance = 1.5f;
						this.WitnessRivalDiePhase++;
						this.RivalDeathTimer = 0;
					}
				}
				//After freaking out for 17 seconds, Senpai calls the cops.
				else if (this.WitnessRivalDiePhase == 3)
				{
					this.TargetDistance = 1.5f;

					//Debug.Log("Senpai's Distance is: " + this.DistanceToDestination + " and TargetDistance is: " + this.TargetDistance);

					if (this.DistanceToDestination <= this.TargetDistance)
					{
						//if (this.Pathfinding.canSearch)
						//{
						this.CharacterAnimation.Play("senpaiRivalCorpseReaction_00");
						//this.CharacterAnimation.CrossFade("kneelCorpse_00");
						this.Pathfinding.canSearch = false;
						this.Pathfinding.canMove = false;
						//}

						if (this.WitnessedCorpse)
						{
							this.transform.LookAt(this.StudentManager.Students[11].Head.position);
							this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y - 90, 0);

							/*
							this.targetRotation = Quaternion.LookRotation(new Vector3(
								this.StudentManager.Students[11].Head.position.x,
								this.transform.position.y,
								this.StudentManager.Students[11].Head.position.z) - this.transform.position);

							this.transform.rotation = Quaternion.Slerp(
							this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);
							*/
						}
					}

					if (this.RivalDeathTimer == 0)
					{
						this.Subtitle.UpdateLabel(SubtitleType.SenpaiRivalDeathReaction, 2, 15.0f);
					}

					this.RivalDeathTimer += Time.deltaTime;

					if (this.CharacterAnimation["senpaiRivalCorpseReaction_00"].time >= this.CharacterAnimation["senpaiRivalCorpseReaction_00"].length)
					{
						this.Subtitle.UpdateLabel(SubtitleType.SenpaiRivalDeathReaction, 3, 10.0f);
						this.CharacterAnimation.CrossFade("kneelPhone_00");
						this.SmartPhone.SetActive(true);
						this.WitnessRivalDiePhase++;
						this.RivalDeathTimer = 0;
					}
				}
				//Senpai weeps for the death of his friend.
				else if (this.WitnessRivalDiePhase == 4)
				{
					this.CharacterAnimation.CrossFade("kneelPhone_00");

					this.RivalDeathTimer += Time.deltaTime;

					if (this.RivalDeathTimer > 10)
					{
						this.Subtitle.UpdateLabel(SubtitleType.SenpaiRivalDeathReaction, 4, 10.0f);
						this.CharacterAnimation.CrossFade("senpaiRivalCorpseCry_00");
						this.SmartPhone.SetActive(false);
						this.WitnessRivalDiePhase++;

						this.Police.Called = true;
						this.Police.Show = true;
					}
				}
			}

			if (this.SpecialRivalDeathReaction)
			{
				Debug.Log("Raibaru is reacting to Osana's corpse.");

				//Raibaru calls the cops
				if (this.WitnessRivalDiePhase == 1)
				{
					//Raibaru freaks out over Osana's corpse
					if (this.DistanceToDestination <= 1)
					{
						this.CharacterAnimation.CrossFade("f02_friendCorpseReaction_00");
						this.Pathfinding.canSearch = false;
						this.Pathfinding.canMove = false;

						this.transform.LookAt(this.StudentManager.Students[11].Head.position);
						this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y - 90, 0);
					}
					else
					{
						this.CharacterAnimation.CrossFade(this.RunAnim);

						this.Pathfinding.canSearch = true;
						this.Pathfinding.canMove = true;
						this.Pathfinding.speed = 4;
					}

					if (this.RivalDeathTimer == 0)
					{
						this.Subtitle.UpdateLabel(SubtitleType.RaibaruRivalDeathReaction, 2, 15.0f);
					}

					this.RivalDeathTimer += Time.deltaTime;

					//Raibaru calls the cops
					if (this.CharacterAnimation["f02_friendCorpseReaction_00"].time >= this.CharacterAnimation["f02_friendCorpseReaction_00"].length)
					{
						this.Subtitle.UpdateLabel(SubtitleType.RaibaruRivalDeathReaction, 3, 10.0f);
						this.CharacterAnimation.CrossFade("f02_kneelPhone_00");
						this.SmartPhone.SetActive(true);
						this.WitnessRivalDiePhase++;
						this.RivalDeathTimer = 0;
					}
				}
				else if (this.WitnessRivalDiePhase == 2)
				{
					this.RivalDeathTimer += Time.deltaTime;
					//Raibaru weeps for the death of her friend.
					if (this.RivalDeathTimer > 10)
					{
						this.Subtitle.UpdateLabel(SubtitleType.RaibaruRivalDeathReaction, 4, 10.0f);
						this.CharacterAnimation.CrossFade("f02_friendCorpseCry_00");
						this.SmartPhone.SetActive(false);
						this.WitnessRivalDiePhase++;

						this.Police.Called = true;
						this.Police.Show = true;
					}
				}
			}
			#endif

			if (this.SolvingPuzzle)
			{
				this.PuzzleTimer += Time.deltaTime;

				this.CharacterAnimation.CrossFade(this.PuzzleAnim);

				if (this.PuzzleTimer > 30)
				{
					this.Pathfinding.canSearch = true;
					this.Pathfinding.canMove = true;
                    this.PuzzleTimer = 0;
					this.Routine = true;
					this.DropPuzzle();
				}
			}

			if (this.GoAway)
			{
				this.GoAwayTimer += Time.deltaTime;

				if (this.GoAwayTimer > 15.0f)
				{
					this.CurrentDestination = this.Destinations[this.Phase];
					this.Pathfinding.target = this.Destinations[this.Phase];

					this.GoAwayTimer = 0.0f;
					this.GoAway = false;
					this.Routine = true;
				}
			}

			/*
			if (this.Struggling)
			{
				this.MoveTowardsTarget(this.Yandere.transform.position + (this.Yandere.transform.forward * .433333f));

				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.Yandere.transform.rotation, 10.0f * Time.deltaTime);

				if (!this.Yandere.Lost && !this.Yandere.Won)
				{
					this.CharacterAnimation.CrossFade (this.StruggleAnim);

					if (!this.Teacher)
					{
						this.CharacterAnimation[this.StruggleAnim].time =
							this.Yandere.CharacterAnimation[AnimNames.FemaleStruggleA].time;
					}
					else
					{
						this.CharacterAnimation[this.StruggleAnim].time =
							this.Yandere.CharacterAnimation[AnimNames.FemaleTeacherStruggleA].time;
					}
				}
			}
			*/
		}
	}

	void UpdateVisibleCorpses()
	{
		//bool Debugging = false;
		//if (this.StudentID == 29){Debugging = true;}

		// [af] Erase any previously visible corpses.
		this.VisibleCorpses.Clear();
		
		// [af] Add visible corpse IDs to the list.
		for (this.ID = 0; this.ID < this.Police.Corpses; this.ID++)
		{
			RagdollScript corpse = this.Police.CorpseList[this.ID];

			if (corpse != null && !corpse.Hidden)
			{
				Collider corpseCollider = corpse.AllColliders[0];

				bool Continue = false;

				if (corpseCollider.transform.position.y < transform.position.y + 4)
				{
					Continue = true;
				}

				if (Continue && this.CanSeeObject(corpseCollider.gameObject,
					corpseCollider.transform.position, this.CorpseLayers, this.Mask))
				{
					this.VisibleCorpses.Add(corpse.StudentID);

					this.Corpse = corpse;

					if (this.Club == ClubType.Delinquent && this.Corpse.Student.Club == ClubType.Bully)
					{
						this.ScaredAnim = this.EvilWitnessAnim;
						this.Persona = PersonaType.Evil;
					}

					if ((this.Persona == PersonaType.TeachersPet) &&
						(this.StudentManager.Reporter == null) &&
						!this.Police.Called)
					{
						// [af] Set this student to be the corpse reporter.
						this.StudentManager.CorpseLocation.position =
							this.Corpse.AllColliders[0].transform.position;

						this.StudentManager.CorpseLocation.LookAt(transform.position);
						this.StudentManager.CorpseLocation.Translate(this.StudentManager.CorpseLocation.forward * -1);
						this.StudentManager.LowerCorpsePosition();

						this.StudentManager.Reporter = this;
						this.ReportingMurder = true;

						this.DetermineCorpseLocation();
					}
				}
			}
		}
	}

	void UpdateVisibleBlood()
	{
		for (this.ID = 0; this.ID < this.StudentManager.Blood.Length; this.ID++)
		{
			if (this.BloodPool == null)
			{
				Collider bloodCollider = this.StudentManager.Blood[this.ID];

				if (bloodCollider != null)
				{
					//Debug.Log(Name + " is checking to see if they are close enough to a blood pool to identify blood.");

					if (Vector3.Distance(transform.position, bloodCollider.transform.position) < VisionDistance)
					{
						if (this.CanSeeObject(bloodCollider.gameObject,
							bloodCollider.transform.position))//, this.CorpseLayers, this.Mask))
						{
							//Debug.Log(Name + " student has seen blood.");

							this.BloodPool = bloodCollider.transform;

							this.WitnessedBloodPool = true;

							if (this.Club != ClubType.Delinquent &&
								this.StudentManager.BloodReporter == null &&
								!this.Police.Called && !this.LostTeacherTrust)
							{
								this.StudentManager.BloodLocation.position = this.BloodPool.position;

								this.StudentManager.BloodLocation.LookAt(new Vector3(
									transform.position.x,
									this.StudentManager.BloodLocation.position.y,
									transform.position.z));

								this.StudentManager.BloodLocation.Translate(this.StudentManager.BloodLocation.forward * -1);
								this.StudentManager.LowerBloodPosition();

								this.StudentManager.BloodReporter = this;
								this.ReportingBlood = true;

								this.DetermineBloodLocation();
							}
						}
					}
				}
			}
			else
			{
				break;
			}
		}
	}

	void UpdateVisibleLimbs()
	{
		for (this.ID = 0; this.ID < this.StudentManager.Limbs.Length; this.ID++)
		{
			//Debug.Log(Name + " is checking the list of all limbs.");

			if (this.BloodPool == null)
			{
				//Debug.Log(Name + " doesn't have a limb yet.");

				Collider limbCollider = this.StudentManager.Limbs[this.ID];

				if (limbCollider != null)
				{
					//Debug.Log(Name + " is checking to see if they are close enough to a limb to identify it.");

					if (this.CanSeeObject(limbCollider.gameObject,
						limbCollider.transform.position))
					{
						//Debug.Log(Name + " student has seen a limb.");

						this.BloodPool = limbCollider.transform;
						this.WitnessedLimb = true;

						if (this.Club != ClubType.Delinquent &&
							this.StudentManager.BloodReporter == null &&
							!this.Police.Called && !this.LostTeacherTrust)
						{
							this.StudentManager.BloodLocation.position = this.BloodPool.position;

							this.StudentManager.BloodLocation.LookAt(new Vector3(
								transform.position.x,
								this.StudentManager.BloodLocation.position.y,
								transform.position.z));

							this.StudentManager.BloodLocation.Translate(this.StudentManager.BloodLocation.forward * -1);
							this.StudentManager.LowerBloodPosition();

							this.StudentManager.BloodReporter = this;
							this.ReportingBlood = true;

							this.DetermineBloodLocation();
						}
					}
				}
			}
			else
			{
				//Debug.Log(Name + " found a limb! Exiting loop.");

				break;
			}
		}
	}

	void UpdateVisibleWeapons()
	{
		for (this.ID = 0; this.ID < this.Yandere.WeaponManager.Weapons.Length; this.ID++)
		{
			//Debug.Log(Name + " is checking the list of all weapons.");

			if (this.Yandere.WeaponManager.Weapons[this.ID] != null)
			{
				if (this.Yandere.WeaponManager.Weapons[this.ID].Blood.enabled ||
				    this.Yandere.WeaponManager.Weapons[this.ID].Misplaced && this.Yandere.WeaponManager.Weapons[this.ID].transform.parent == null)
				{
					//Debug.Log(Name + " doesn't have a weapon yet.");

					if (this.BloodPool == null)
					{
						if (this.Yandere.WeaponManager.Weapons[this.ID].transform != this.LastSuspiciousObject &&
							this.Yandere.WeaponManager.Weapons[this.ID].transform != this.LastSuspiciousObject2)
						{
							if (this.Yandere.WeaponManager.Weapons[this.ID].enabled)
							{
								Collider weaponCollider = this.Yandere.WeaponManager.Weapons[this.ID].MyCollider;

								if (weaponCollider != null)
								{
									//Debug.Log(Name + " is checking to see if they are close enough to a weapon to identify it.");

									if (this.CanSeeObject(weaponCollider.gameObject, weaponCollider.transform.position))
									{
										Debug.Log(Name + " has seen a dropped weapon on the ground.");

										this.BloodPool = weaponCollider.transform;

										if (this.Yandere.WeaponManager.Weapons[this.ID].Blood.enabled)
										{
											this.WitnessedBloodyWeapon = true;
										}

										this.WitnessedWeapon = true;

										if (this.Club != ClubType.Delinquent &&
											this.StudentManager.BloodReporter == null &&
											!this.Police.Called && !this.LostTeacherTrust)
										{
											//if (this.WitnessedBloodyWeapon)
											//{
												this.StudentManager.BloodLocation.position = this.BloodPool.position;

												this.StudentManager.BloodLocation.LookAt(new Vector3(
													transform.position.x,
													this.StudentManager.BloodLocation.position.y,
													transform.position.z));

												this.StudentManager.BloodLocation.Translate(this.StudentManager.BloodLocation.forward * -1);
												this.StudentManager.LowerBloodPosition();

												this.StudentManager.BloodReporter = this;
												this.ReportingBlood = true;

												this.DetermineBloodLocation();
											//}
										}
									}
								}
							}
						}
					}
					else
					{
						//Debug.Log(Name + " found a weapon! Exiting loop.");

						break;
					}
				}
			}
		}
	}

	void UpdateVision()
	{
		bool TooDistracted = false;

		if (this.Distracted)
		{
			TooDistracted = true;

			if (!this.Hunting && this.ClubActivityPhase < 15)
			{
				if (this.Yandere.Attacking || this.Yandere.Dragging || this.Yandere.Carrying ||
					this.Yandere.Bloodiness + this.Yandere.GloveBlood > 0.0f && !this.Yandere.Paint ||
					this.Yandere.Armed || this.Yandere.Sanity < 66.66666f ||
					this.Yandere.PickUp != null && this.Yandere.PickUp.BodyPart != null)
				{
					TooDistracted = false;
				}
			}

			if (this.AmnesiaTimer > 0)
			{
				this.AmnesiaTimer = Mathf.MoveTowards(this.AmnesiaTimer, 0, Time.deltaTime);

				if (this.AmnesiaTimer == 0)
				{
					this.Distracted = false;
				}
			}
		}

        /*
        if (this.Broken)
        {
		    Debug.Log(this.Name + " is too distracted? " + TooDistracted);
        }
        */

        if (!TooDistracted)
		{
			bool CanNoticeCorpse = true;

			if (Yandere.Pursuer == null)
			{
				if (this.Yandere.Pursuer == this)
				{
					CanNoticeCorpse = false;
				}
			}

			if (!this.WitnessedMurder && !this.CheckingNote && !this.Shoving && !this.Slave &&
				!this.Struggling && CanNoticeCorpse && !this.Drownable && !this.Fighting)
			{
				if (this.Police.Corpses > 0)
				{
					// [af] This method updates the student's list of corpse IDs. We can obtain 
					// the number of corpses in sight from that list.
					if (!this.Blind)
					{
						this.UpdateVisibleCorpses();
					}

					if (this.VisibleCorpses.Count > 0)
					{
						if (!this.WitnessedCorpse)
						{
							Debug.Log(this.Name + " discovered a corpse.");
							this.SawCorpseThisFrame = true;

							if (this.Club == ClubType.Delinquent)
							{
								if (this.Corpse.Student.Club == ClubType.Bully)
								{
									this.FoundEnemyCorpse = true;
								}
							}

							if (this.Persona == PersonaType.Sleuth)
							{
								if (this.Sleuthing)
								{
									// Switch to the Phone Addict persona.
									this.Persona = PersonaType.PhoneAddict;
									this.SmartPhone.SetActive(true);
								}
								else
								{
									this.Persona = PersonaType.SocialButterfly;
								}
							}

							this.Pathfinding.canSearch = false;
							this.Pathfinding.canMove = false;

							if (!this.Male)
							{
								this.CharacterAnimation[AnimNames.FemaleSmile].weight = 0.0f;
							}

							this.AlarmTimer = 0.0f;
							this.Alarm = 200.0f;

							this.LastKnownCorpse = this.Corpse.AllColliders[0].transform.position;

							this.WitnessedBloodyWeapon = false;
							this.WitnessedBloodPool = false;
							this.WitnessedSomething = false;
							this.WitnessedWeapon = false;
							this.WitnessedCorpse = true;
							this.WitnessedLimb = false;

                            this.Yandere.NotificationManager.CustomText = this.Name + " found a corpse!";
                            this.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);

                            this.SummonWitnessCamera();

                            if (this.ReturningMisplacedWeapon)
							{
								if (this.ReturningMisplacedWeapon)
								{
									this.DropMisplacedWeapon();
								}
							}

                            if (this.StudentManager.BloodReporter == this)
                            {
                                this.StudentManager.BloodReporter = null;
                                this.ReportingBlood = false;
                                this.Fleeing = false;
                            }

                            this.InvestigatingBloodPool = false;
							this.Investigating = false;
							this.EatingSnack = false;
							this.Threatened = false;
							this.SentHome = false;
							this.Routine = false;

                            this.CheckingNote = false;
                            this.SentToLocker = false;
                            this.Meeting = false;
                            this.MeetTimer = 0.0f;

                            // Transform into the Evil Persona depending on the victim.
                            if (this.Persona == PersonaType.Spiteful)
							{
								//Debug.Log("A Spiteful student witnessed a murder.");

								if (this.Bullied && this.Corpse.Student.Club == ClubType.Bully || this.Corpse.Student.Bullied)
								{
									this.ScaredAnim = this.EvilWitnessAnim;
									this.Persona = PersonaType.Evil;
								}
							}

							this.ForgetRadio();

							if (this.Wet)
							{
								this.Persona = PersonaType.Loner;
							}

							if (this.Corpse.Disturbing)
							{
								if (this.Corpse.BurningAnimation)
								{
									this.WalkBackTimer = 5.0f;
									this.WalkBack = true;
									this.Hesitation = 0.5f;
								}

								if (this.Corpse.MurderSuicideAnimation)
								{
									this.WitnessedMindBrokenMurder = true;
									this.WalkBackTimer = 5.0f;
									this.WalkBack = true;
									this.Hesitation = 1.0f;
								}

								if (this.Corpse.ChokingAnimation)
								{
									this.WalkBackTimer = 0.0f;
									this.WalkBack = true;
									this.Hesitation = 0.6f;
								}

								if (this.Corpse.ElectrocutionAnimation)
								{
									this.WalkBackTimer = 0.0f;
									this.WalkBack = true;
									this.Hesitation = 0.5f;
								}
							}

							this.StudentManager.UpdateMe(this.StudentID);

							if (!this.Teacher)
							{
								this.SpawnAlarmDisc();
							}
							else
							{
								this.AlarmTimer = 3.0f;
							}

							// [af] This is the (unintuitive) Unity 5.3 way of changing emission.
							ParticleSystem.EmissionModule heartsEmission = this.Hearts.emission;

							if (this.Talking)
							{
								this.DialogueWheel.End();

								heartsEmission.enabled = false;
								this.Pathfinding.canSearch = true;
								this.Pathfinding.canMove = true;
								this.Obstacle.enabled = false;
								this.Talk.enabled = false;
								this.Talking = false;
								this.Waiting = false;

								this.StudentManager.EnablePrompts();
							}

							if (this.Following)
							{
                                heartsEmission.enabled = false;

                                this.FollowCountdown.gameObject.SetActive(false);
                                this.Yandere.Follower = null;
								this.Yandere.Followers--;
								this.Following = false;
							}
						}

						if (this.Corpse.Dragged || this.Corpse.Dumped)
						{
							if (this.Teacher)
							{
								this.Subtitle.UpdateLabel(SubtitleType.TeacherMurderReaction, Random.Range(1, 3), 3.0f);
								//this.StudentManager.Portal.SetActive(false);
							}

							if (!this.Yandere.Egg)
							{
								this.WitnessMurder();
							}
						}
					}
				}

				if (this.VisibleCorpses.Count == 0)
				{
					if (!this.WitnessedCorpse)
					{
						if (this.WitnessCooldownTimer > 0)
						{
							this.WitnessCooldownTimer = Mathf.MoveTowards(this.WitnessCooldownTimer, 0, Time.deltaTime);
						}
						else
						{
							if (this.StudentID == this.StudentManager.CurrentID || this.Persona == PersonaType.Strict && this.Fleeing)
							{
								if (!this.Wet && !this.Guarding && !this.IgnoreBlood && !this.InvestigatingPossibleDeath && !this.Spraying && !this.Emetic && !this.Threatened &&
                                    !this.Sedated && !this.Headache && !this.SentHome && !this.Slave && !this.Talking && !this.Confessing && this.FollowTarget == null)
								{
									if (this.BloodPool == null)
									{
										if (this.StudentManager.Police.LimbParent.childCount > 0)
										{
											this.UpdateVisibleLimbs();
										}
									}

									if (this.BloodPool == null)
									{
										if (this.Police.BloodyWeapons > 0 || this.Yandere.WeaponManager.MisplacedWeapons > 0)
										{
											if (!this.InvestigatingPossibleLimb && !this.Alarmed)
											{
												this.UpdateVisibleWeapons();
											}
										}
									}

									//Debug.Log("Beginning to look for blood pools.");

									if (this.BloodPool == null)
									{
										//Debug.Log("Blood pool is null...");

										if (this.StudentManager.Police.BloodParent.childCount > 0)
										{
											//Debug.Log("BloodParent.childCount is above zero...");

											if (!this.InvestigatingPossibleLimb)
											{
												this.UpdateVisibleBlood();
											}
										}
									}

									if (this.BloodPool != null)
									{
										if (!this.WitnessedSomething)
										{
											//Debug.Log(this.Name + " discovered a blood pool.");

											this.Pathfinding.canSearch = false;
											this.Pathfinding.canMove = false;

											if (!this.Male)
											{
												this.CharacterAnimation[AnimNames.FemaleSmile].weight = 0.0f;
											}

											this.AlarmTimer = 0.0f;
											this.Alarm = 200.0f;

											this.LastKnownBlood = this.BloodPool.transform.position;
											this.NotAlarmedByYandereChan = true;
											this.WitnessedSomething = true;
											this.Investigating = false;
											this.EatingSnack = false;
											this.Threatened = false;
											this.SentHome = false;
											this.Routine = false;

											this.ForgetRadio();

											this.StudentManager.UpdateMe(this.StudentID);

											if (this.Teacher)
											{
												this.AlarmTimer = 3.0f;
											}

											ParticleSystem.EmissionModule heartsEmission = this.Hearts.emission;

											if (this.Talking)
											{
												this.DialogueWheel.End();

												heartsEmission.enabled = false;
												this.Pathfinding.canSearch = true;
												this.Pathfinding.canMove = true;
												this.Obstacle.enabled = false;
												this.Talk.enabled = false;
												this.Talking = false;
												this.Waiting = false;

												this.StudentManager.EnablePrompts();
											}

											if (this.Following)
											{
                                                heartsEmission.enabled = false;

                                                this.FollowCountdown.gameObject.SetActive(false);
                                                this.Yandere.Follower = null;
                                                this.Yandere.Followers--;
												this.Following = false;
											}
										}
									}
								}
							}
						}
					}
				}

				this.PreviousAlarm = this.Alarm;

				if (this.DistanceToPlayer < this.VisionDistance - this.ChameleonBonus)
				{
                    //Debug.Log("This student can notice Yandere-chan.");

					if (!this.Talking && !this.Spraying && !this.SentHome && !this.Slave)
					{
						if (!this.Yandere.Noticed)
						{
							bool SuspiciousOfBloodyMop = false;

							if (this.Guarding || this.Fleeing || this.InvestigatingBloodPool)
							{
								SuspiciousOfBloodyMop = true;
							}

							//if (!this.Yandere.Chased)
							//{
								if (this.Yandere.Armed &&
									this.Yandere.EquippedWeapon.Suspicious ||
                                    this.Yandere.Armed &&
                                    this.Yandere.EquippedWeapon.Bloody ||
                                    !this.Teacher &&
									this.StudentID > 1 && 
									!this.Teacher && this.Yandere.PickUp != null &&
									this.Yandere.PickUp.Suspicious ||
									this.Teacher && this.Yandere.PickUp != null &&
									this.Yandere.PickUp.Suspicious && !this.Yandere.PickUp.CleaningProduct ||
									this.Yandere.Bloodiness + this.Yandere.GloveBlood > 0.0f && !this.Yandere.Paint ||
									this.Yandere.Sanity < 33.333f ||
									this.Yandere.Pickpocketing ||
									this.Yandere.Attacking ||
                                    this.Yandere.Cauterizing ||
                                    this.Yandere.Struggling ||
									this.Yandere.Dragging ||
									!this.IgnoringPettyActions && this.Yandere.Lewd ||
									this.Yandere.Carrying ||
									this.Yandere.Medusa ||
									this.Yandere.Poisoning ||
									this.Yandere.Pickpocketing ||
									this.Yandere.WeaponTimer > 0 ||
									this.Yandere.MurderousActionTimer > 0 ||
									this.Yandere.PickUp != null && this.Yandere.PickUp.BodyPart != null ||
									!this.IgnoringPettyActions && this.Yandere.Laughing && (this.Yandere.LaughIntensity > 15.0f) ||
									!this.IgnoringPettyActions && this.Yandere.Stance.Current == StanceType.Crouching ||
									!this.IgnoringPettyActions && this.Yandere.Stance.Current == StanceType.Crawling ||
									/*this.Private &&*/ this.Yandere.Trespassing || this.Private && this.Yandere.Eavesdropping ||
									this.Teacher && !this.WitnessedCorpse && this.Yandere.Trespassing ||
									this.Teacher && !this.IgnoringPettyActions && this.Yandere.Rummaging ||
									!this.IgnoringPettyActions && this.Yandere.TheftTimer > 0 ||
									this.StudentID == 1 && this.Yandere.NearSenpai && !this.Yandere.Talking ||
									this.Yandere.Eavesdropping && this.Private ||
									!this.StudentManager.CombatMinigame.Practice && this.Yandere.DelinquentFighting &&
									this.StudentManager.CombatMinigame.Path < 4 && !this.StudentManager.CombatMinigame.Practice && !this.Yandere.SeenByAuthority ||
									SuspiciousOfBloodyMop && this.Yandere.PickUp != null &&
									this.Yandere.PickUp.Mop != null && this.Yandere.PickUp.Mop.Bloodiness > 0 ||
									SuspiciousOfBloodyMop && this.Yandere.PickUp != null &&
									this.Yandere.PickUp.BodyPart != null ||
									this.Yandere.PickUp != null &&
									this.Yandere.PickUp.Clothing && this.Yandere.PickUp.Evidence)
								{
									//Debug.Log(Name + " is looking for Yandere-chan.");

									bool Continue = false;

									if (this.Yandere.transform.position.y < transform.position.y + 4)
									{
										Continue = true;
									}

                                    Vector3 YanTarget = this.Yandere.HeadPosition;

                                    if (this.Yandere.Aiming && this.Yandere.Stance.Current == StanceType.Crawling)
                                    {
                                        YanTarget = this.Yandere.Head.position + new Vector3(0, .25f, 0);
                                    }

                                    if (Continue && this.CanSeeObject(this.Yandere.gameObject, YanTarget))
									{
										//Debug.Log(Name + " can see Yandere-chan.");

										this.YandereVisible = true;

										if (this.Yandere.Attacking ||
                                            this.Yandere.Cauterizing ||
                                            this.Yandere.Struggling ||
											this.WitnessedCorpse && (this.Yandere.NearBodies > 0) && (this.Yandere.Bloodiness + this.Yandere.GloveBlood > 0.0f) && !this.Yandere.Paint ||
											this.WitnessedCorpse && (this.Yandere.NearBodies > 0) && this.Yandere.Armed ||
											this.WitnessedCorpse && (this.Yandere.NearBodies > 0) && (this.Yandere.Sanity < 66.66666f) ||
											this.Yandere.Carrying ||
											this.Yandere.Dragging ||
											this.Yandere.MurderousActionTimer > 0 ||
											this.Guarding && this.Yandere.Bloodiness + this.Yandere.GloveBlood > 0.0f && !this.Yandere.Paint ||
											this.Guarding && this.Yandere.Armed ||
											this.Guarding && this.Yandere.Sanity < 66.66666f ||
											!this.StudentManager.CombatMinigame.Practice && this.Club == ClubType.Council && this.Yandere.DelinquentFighting &&
											this.StudentManager.CombatMinigame.Path < 4 && !this.Yandere.SeenByAuthority ||
											!this.StudentManager.CombatMinigame.Practice && this.Teacher && this.Yandere.DelinquentFighting &&
											this.StudentManager.CombatMinigame.Path < 4 && !this.Yandere.SeenByAuthority ||
											SuspiciousOfBloodyMop && this.Yandere.PickUp != null &&
											this.Yandere.PickUp.Mop != null && this.Yandere.PickUp.Mop.Bloodiness > 0  ||
											SuspiciousOfBloodyMop && this.Yandere.PickUp != null &&
											this.Yandere.PickUp.BodyPart != null ||
											this.Yandere.PickUp != null &&
											this.Teacher && this.Yandere.PickUp.Clothing && this.Yandere.PickUp.Evidence ||
											this.StudentManager.Atmosphere < .33333f && this.Yandere.Bloodiness + this.Yandere.GloveBlood > 0 && this.Yandere.Armed)
										{
											Debug.Log(Name + " is aware that Yandere-chan is misbehaving.");

											if (this.Yandere.Hungry || !this.Yandere.Egg)
											{
												Debug.Log(this.Name + " has just witnessed a murder!");

												if (this.Yandere.PickUp != null)
												{
													if (SuspiciousOfBloodyMop)
													{
														if (this.Yandere.PickUp.Mop != null)
														{
															if (this.Yandere.PickUp.Mop.Bloodiness > 0)
															{
																Debug.Log("This character witnessed Yandere-chan trying to cover up a crime.");

																this.WitnessedCoverUp = true;
															}
														}
														else if (this.Yandere.PickUp.BodyPart != null)
														{
															Debug.Log("This character witnessed Yandere-chan trying to cover up a crime.");

															this.WitnessedCoverUp = true;
														}
													}

													if (this.Teacher && this.Yandere.PickUp.Clothing && this.Yandere.PickUp.Evidence)
													{
														Debug.Log("This character witnessed Yandere-chan trying to cover up a crime.");

														this.WitnessedCoverUp = true;
													}
												}

												if (this.Persona == PersonaType.PhoneAddict && !this.Phoneless)
												{
													Debug.Log (this.Name + ", a Phone Addict, is deciding what to do.");

													this.Countdown.gameObject.SetActive(false);
													this.Countdown.Sprite.fillAmount = 1;
													this.WitnessedMurder = false;
													this.Fleeing = false;

													if (this.CrimeReported)
													{
														Debug.Log (this.Name + "'s ''CrimeReported'' was ''true'', but we're seeing it to ''false''.");

														this.CrimeReported = false;
													}
												}

												if (!this.Yandere.DelinquentFighting)
												{
													this.NoBreakUp = true;
												}
												else
												{
													if (this.Teacher || this.Club == ClubType.Council)
													{
														this.Yandere.SeenByAuthority = true;
													}
												}

												this.WitnessMurder();
											}
										}
										else
										{
											if (!this.Fleeing)
											{
												if (!this.Alarmed || this.CanStillNotice)
												{
													//if (this.Teacher)
													//{
														if (this.Yandere.Rummaging || this.Yandere.TheftTimer > 0)
														{
															this.Alarm = 200.0f;
														}
													//}

													if (this.Yandere.WeaponTimer > 0)
													{
														this.Alarm = 200.0f;
													}

													if (this.IgnoreTimer == 0.0f || this.CanStillNotice)
													{
														if (this.Teacher)
														{
															this.StudentManager.TutorialWindow.ShowTeacherMessage = true;
														}

                                                        //Debug.Log("Increasing alarm by " + Time.deltaTime * (100.0f / this.DistanceToPlayer) * this.Paranoia * this.Perception);

                                                        int WeaponBonus = 1;

                                                        if (this.Yandere.Armed && this.Yandere.EquippedWeapon.Suspicious)
                                                        {
                                                            if (this.Yandere.EquippedWeapon.Type == WeaponType.Bat ||
                                                                this.Yandere.EquippedWeapon.Type == WeaponType.Katana ||
                                                                this.Yandere.EquippedWeapon.Type == WeaponType.Saw ||
                                                                this.Yandere.EquippedWeapon.Type == WeaponType.Weight)
                                                            {
                                                                WeaponBonus = 5;
                                                            }
                                                        }

                                                        this.Alarm += Time.deltaTime * (100.0f / this.DistanceToPlayer) * this.Paranoia * this.Perception * WeaponBonus;

                                                        //Debug.Log("Increasing alarm by " + Time.deltaTime * (100.0f / this.DistanceToPlayer) * this.Paranoia * this.Perception);

                                                        if ((this.StudentID == 1) && this.Yandere.TimeSkipping)
														{
															this.Clock.EndTimeSkip();
														}
													}
												}
											}
										}
									}
									else
									{
										this.Alarm -= Time.deltaTime * 100.0f * (1.0f / this.Paranoia);
									}
								}
								else
								{
									this.Alarm -= Time.deltaTime * 100.0f * (1.0f / this.Paranoia);
								}
							//}
							//else
							//{
							//	this.Alarm -= Time.deltaTime * 100.0f * (1.0f / this.Paranoia);
							//}
						}
						else
						{
							this.Alarm -= Time.deltaTime * 100.0f * (1.0f / this.Paranoia);
						}
					}
					else
					{
						this.Alarm -= Time.deltaTime * 100.0f * (1.0f / this.Paranoia);
					}
				}
				else
				{
					this.Alarm -= Time.deltaTime * 100.0f * (1.0f / this.Paranoia);
				}

				if (this.PreviousAlarm > this.Alarm)
				{
					if (this.Alarm < 100.0f)
					{
						this.YandereVisible = false;
					}
				}

				if (this.Teacher && !this.Yandere.Medusa && this.Yandere.Egg)
				{
					this.Alarm = 0.0f;
				}

				if (this.Alarm > 100.0f)
				{
					this.BecomeAlarmed();
				}
			}
			else
			{
				// [af] Commented in JS code.
				//if (!Alarmed)
				//{
				this.Alarm -= Time.deltaTime * 100.0f * (1.0f / this.Paranoia);
				//}
			}
		}
		else
		{
			//Debug.Log(this.Name + " is currently ''too distracted'' to react to certain things.");

			if (this.Distraction != null)
			{
				this.targetRotation = Quaternion.LookRotation(new Vector3(
					this.Distraction.position.x,
					this.transform.position.y,
					this.Distraction.position.z) - this.transform.position);

				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);

				if (this.Distractor != null)
				{
					//If we're being distracted by a member of the Cooking Club sharing food...
					if (this.Distractor.Club == ClubType.Cooking && this.Distractor.ClubActivityPhase > 0 &&
						this.Distractor.Actions[this.Distractor.Phase] == StudentActionType.ClubAction)
					{
						this.CharacterAnimation.CrossFade(this.PlateEatAnim);

						if (this.CharacterAnimation[this.PlateEatAnim].time > 6.83333)
						{
							this.Fingerfood[Distractor.StudentID - 20].SetActive(false);
						}
						else if (this.CharacterAnimation[this.PlateEatAnim].time > 2.66666)
						{
							this.Fingerfood[Distractor.StudentID - 20].SetActive(true);
						}
					}
					else
					{
						this.CharacterAnimation.CrossFade(this.RandomAnim);

						if (this.CharacterAnimation[this.RandomAnim].time >=
							this.CharacterAnimation[this.RandomAnim].length)
						{
							this.PickRandomAnim();
						}
					}
				}
			}
		}
	}

	public void BecomeAlarmed()
	{
		//Debug.Log("Beginning of BecomeAlarmed(). Is " + this.Name + "'s smartphone active? The answer is: " + this.SmartPhone.activeInHierarchy);

		if (this.Yandere.Medusa && this.YandereVisible)
		{
			this.TurnToStone();
			return;
		}

		if (!this.Alarmed || this.DiscCheck)
		{
			//Debug.Log(this.Name + " has become alarmed.");

			if (this.Persona == PersonaType.PhoneAddict && !this.Phoneless)
			{
				this.SmartPhone.SetActive(true);
			}

			this.Yandere.Alerts++;

			if (this.ReturningMisplacedWeapon)
			{
				this.DropMisplacedWeapon();
				this.ReturnToRoutineAfter = true;
			}

			if (this.StudentID > 1)
			{
				// [af] Converted while loop to for loop.
				for (this.ID = 0; this.ID < this.Outlines.Length; this.ID++)
				{
					if (this.Outlines[this.ID] != null)
					{
						this.Outlines[this.ID].color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
					}
				}
			}

			if (this.DistractionTarget != null)
			{
				this.DistractionTarget.TargetedForDistraction = false;
			}

			if (this.SolvingPuzzle)
			{
				this.DropPuzzle();
			}

			//this.CharacterAnimation[this.LeanAnim].time += this.StudentID * .1f;
			this.CharacterAnimation.CrossFade(this.IdleAnim);
			this.Pathfinding.canSearch = false;
			this.Pathfinding.canMove = false;
			this.CameraReacting = false;
			this.CanStillNotice = false;
			this.Distracting = false;
			this.Distracted = false;
			this.DiscCheck = false;
			this.Reacted = false;
			this.Routine = false;
			this.Alarmed = true;

			this.PuzzleTimer = 0;
			this.ReadPhase = 0;

			if (!this.Male)
			{
				this.HorudaCollider.gameObject.SetActive(false);
			}

			if (this.YandereVisible && this.Yandere.Mask == null)
			{
				this.Witness = true;
			}

			this.EmptyHands();

			if (this.Club == ClubType.Cooking)
			{
				if (this.Actions[this.Phase] == StudentActionType.ClubAction)
				{
					if (this.ClubActivityPhase == 1)
					{
						if (!this.WitnessedCorpse)
						{
							this.ResumeDistracting = true;
							this.MyPlate.gameObject.SetActive (true);
						}
					}
				}
			}

			this.SpeechLines.Stop();
			this.StopPairing();

			// [af] Commented in JS code.
			//Debug.Log("My name is " + Name + " and we just stopped DiscChecking.");

			if (this.Witnessed != StudentWitnessType.Weapon && this.Witnessed != StudentWitnessType.Eavesdropping)
			{
				this.PreviouslyWitnessed = this.Witnessed;
			}

			if (this.DistanceToDestination < 5)
			{
				if (this.Actions[this.Phase] == StudentActionType.Graffiti ||
				    this.Actions[this.Phase] == StudentActionType.Bully)
				{
					this.StudentManager.NoBully[this.BullyID] = true;
					this.KilledMood = true;
				}
			}

			if (this.WitnessedCorpse && !this.WitnessedMurder)
			{
				this.Witnessed = StudentWitnessType.Corpse;
				this.EyeShrink = 0.90f;
			}
			else if (this.WitnessedLimb)
			{
				this.Witnessed = StudentWitnessType.SeveredLimb;
			}
			else if (this.WitnessedBloodyWeapon)
			{
				this.Witnessed = StudentWitnessType.BloodyWeapon;
			}
			else if (this.WitnessedBloodPool)
			{
				this.Witnessed = StudentWitnessType.BloodPool;
			}
			else if (this.WitnessedWeapon)
			{
				this.Witnessed = StudentWitnessType.DroppedWeapon;
			}

			if (this.SawCorpseThisFrame)
			{
				this.YandereVisible = false;
			}

			if (this.YandereVisible && !this.NotAlarmedByYandereChan)
			{
				//This is for delinquents only.
				if (!this.Injured && this.Persona == PersonaType.Violent && this.Yandere.Armed &&
					!this.WitnessedCorpse && !this.RespectEarned ||
					this.Persona == PersonaType.Violent && this.Yandere.DelinquentFighting)
				{
					this.Subtitle.Speaker = this;
					this.Subtitle.UpdateLabel(SubtitleType.DelinquentWeaponReaction, 0, 3.0f);
					this.ThreatDistance = this.DistanceToPlayer;
					this.CheerTimer = Random.Range(1.0f, 2.0f);
					this.SmartPhone.SetActive(false);
                    //this.SilentlyForgetBloodPool();
                    this.Threatened = true;
					this.ThreatPhase = 1;
					this.ForgetGiggle();
				}

				Debug.Log(this.Name + " saw Yandere-chan doing something bad.");

				if (this.Yandere.Attacking || this.Yandere.Struggling || 
					this.Yandere.Carrying || this.Yandere.PickUp != null && 
					this.Yandere.PickUp.BodyPart)
				{
					if (!this.Yandere.Egg)
					{
						this.WitnessMurder();
					}
				}
				else
				{
					if (this.Witnessed != StudentWitnessType.Corpse)
					{
						this.DetermineWhatWasWitnessed();
					}
				}

				if (this.Teacher && this.WitnessedCorpse)
				{
					this.Concern = 1;
				}

				if (this.StudentID == 1 && this.Yandere.Mask == null && !this.Yandere.Egg)
				{
					if (this.Concern == 5)
					{
						Debug.Log("Senpai noticed stalking or lewdness.");

						this.SenpaiNoticed();

						if (this.Witnessed == StudentWitnessType.Stalking || 
							this.Witnessed == StudentWitnessType.Lewd)
						{
							this.CharacterAnimation.CrossFade(this.IdleAnim);

							this.CharacterAnimation[this.AngryFaceAnim].weight = 1.0f;
						}
						else
						{
							Debug.Log("Senpai entered his scared animation.");

							this.CharacterAnimation.CrossFade(this.ScaredAnim);

							this.CharacterAnimation[AnimNames.MaleScaredFace].weight = 1.0f;
						}

						this.CameraEffects.MurderWitnessed();
					}
					else
					{
						this.CharacterAnimation.CrossFade(AnimNames.MaleSuspicious);

						this.CameraEffects.Alarm();
					}
				}
				else
				{
					if (!this.Teacher)
					{
						this.CameraEffects.Alarm();
					}
					//This code will only run if the character is a teacher.
					else
					{
						Debug.Log("A teacher has just witnessed Yandere-chan doing something bad.");

						if (!this.Fleeing)
						{
							if (this.Concern < 5)
							{
								this.CameraEffects.Alarm();
							}
							else
							{
								if (!this.Yandere.Struggling)
								{
									if (!this.StudentManager.PinningDown)
									{
										this.SenpaiNoticed();
										this.CameraEffects.MurderWitnessed();
									}
								}
							}
						}
						else
						{
							this.PersonaReaction();
							this.AlarmTimer = 0.0f;

							if (this.Concern < 5)
							{
								this.CameraEffects.Alarm();
							}
							else
							{
								this.CameraEffects.MurderWitnessed();
							}
						}
					}
				}

				if (!this.Teacher && this.Club != ClubType.Delinquent &&
					this.Witnessed == this.PreviouslyWitnessed)
				{
					this.RepeatReaction = true;
				}

				if (this.Yandere.Mask == null)
				{
					this.RepDeduction = 0.0f;

					this.CalculateReputationPenalty();

					if (this.RepDeduction >= 0.0f)
					{
						this.RepLoss -= this.RepDeduction;
					}

					this.Reputation.PendingRep -= this.RepLoss * this.Paranoia;
					this.PendingRep -= this.RepLoss * this.Paranoia;
				}

				if (this.ToiletEvent != null)
				{
					if (this.ToiletEvent.EventDay == System.DayOfWeek.Monday)
					{
						this.ToiletEvent.EndEvent();
					}
				}
			}
			//If Yandere-chan is not visible...
			else
			{
				if (!this.WitnessedCorpse)
				{
					if (this.Yandere.Caught)
					{
						if (this.Yandere.Mask == null)
						{
							if (this.Yandere.Pickpocketing)
							{
								this.Witnessed = StudentWitnessType.Pickpocketing;
								this.RepLoss += 10;
							}
							else
							{
								this.Witnessed = StudentWitnessType.Theft;
							}

							this.RepDeduction = 0.0f;

							this.CalculateReputationPenalty();

							if (this.RepDeduction >= 0.0f)
							{
								this.RepLoss -= this.RepDeduction;
							}

							this.Reputation.PendingRep -= this.RepLoss * this.Paranoia;
							this.PendingRep -= this.RepLoss * this.Paranoia;
						}
					}
					else if (this.WitnessedLimb)
					{
						this.Witnessed = StudentWitnessType.SeveredLimb;
					}
					else if (this.WitnessedBloodyWeapon)
					{
						this.Witnessed = StudentWitnessType.BloodyWeapon;
					}
					else if (this.WitnessedBloodPool)
					{
						this.Witnessed = StudentWitnessType.BloodPool;
					}
					else if (this.WitnessedWeapon)
					{
						this.Witnessed = StudentWitnessType.DroppedWeapon;
					}
					else
					{
						//Debug.Log(this.Name + " was alarmed by something, but didn't see what it was.");

						this.Witnessed = StudentWitnessType.None;
						this.DiscCheck = true;
						this.Witness = false;
					}
				}
				else
				{
					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;

					//Debug.Log(this.Name + " discovered a corpse.");
				}
			}
		}

		this.NotAlarmedByYandereChan = false;
		this.SawCorpseThisFrame = false;

		//Debug.Log("End of BecomeAlarmed(). Is " + this.Name + "'s smartphone active? The answer is: " + this.SmartPhone.activeInHierarchy);
	}

	void UpdateDetectionMarker()
	{
		if (this.Alarm < 0.0f)
		{
			this.Alarm = 0.0f;

			if (this.Club == ClubType.Council)
			{
				if (!this.Yandere.Noticed)
				{
					//Debug.Log("CanStillNotice was just set to ''true''.");

					this.CanStillNotice = true;
				}
			}
		}

		if (this.DetectionMarker != null)
		{
			// [af] Replaced if/else statement with assignment and ternary expression.

			/*
			this.DetectionMarker.Tex.transform.localScale = new Vector3(
				this.DetectionMarker.Tex.transform.localScale.x,
				(this.Alarm <= 100.0f) ? (this.Alarm / 100.0f) : 1.0f,
				this.DetectionMarker.Tex.transform.localScale.z);
			*/

			if (this.Alarm > 0.0f)
			{
				if (!this.DetectionMarker.Tex.enabled)
				{
					this.DetectionMarker.Tex.enabled = true;
				}

				this.DetectionMarker.Tex.transform.localScale = new Vector3(
				this.DetectionMarker.Tex.transform.localScale.x,
				(this.Alarm <= 100.0f) ? (this.Alarm / 100.0f) : 1.0f,
				this.DetectionMarker.Tex.transform.localScale.z);

				this.DetectionMarker.Tex.color = new Color(
					this.DetectionMarker.Tex.color.r,
					this.DetectionMarker.Tex.color.g,
					this.DetectionMarker.Tex.color.b,
					this.Alarm / 100.0f);
			}
			else
			{
				if (this.DetectionMarker.Tex.color.a != 0.0f)
				{
					this.DetectionMarker.Tex.enabled = false;
					this.DetectionMarker.Tex.color = new Color(
						this.DetectionMarker.Tex.color.r,
						this.DetectionMarker.Tex.color.g,
						this.DetectionMarker.Tex.color.b,
						0.0f);
				}
			}
		}
		else
		{
			this.SpawnDetectionMarker();
		}
	}

	void UpdateTalkInput()
	{
		//The player is trying to talk to a student.
		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			if (!GameGlobals.EmptyDemon)
			{
				if (this.Alarm > 0.0f || this.AlarmTimer > 0.0f || this.Yandere.Armed || this.Yandere.Shoved ||
				 	this.Waiting || this.InEvent || this.SentHome || this.Threatened || 
					this.Distracted && !this.Drownable || this.StudentID == 1)
				{
					if (!this.Slave && !this.BadTime && !this.Yandere.Gazing && !this.FightingSlave)
					{
						this.Prompt.Circle[0].fillAmount = 1.0f;
					}
				}
			}

			if (this.Prompt.Circle[0].fillAmount == 0.0f)
			{
				bool BeingClubLeader = false;

				if (this.StudentID < 86 && this.Armband.activeInHierarchy)
				{
					if (this.Actions[this.Phase] == StudentActionType.ClubAction ||
						this.Actions[this.Phase] == StudentActionType.SitAndSocialize ||
						this.Actions[this.Phase] == StudentActionType.Socializing ||
						this.Actions[this.Phase] == StudentActionType.Sleuth ||
						this.Actions[this.Phase] == StudentActionType.Lyrics ||
						this.Actions[this.Phase] == StudentActionType.Patrol ||
						this.Actions[this.Phase] == StudentActionType.SitAndEatBento)
					{
						if (this.DistanceToDestination < 1 ||
							this.transform.position.y > this.StudentManager.ClubZones[(int)this.Club].position.y - 1 &&
							this.transform.position.y < this.StudentManager.ClubZones[(int)this.Club].position.y + 1 &&
							Vector3.Distance(this.transform.position, this.StudentManager.ClubZones[(int)this.Club].position) < this.ClubThreshold ||
							Vector3.Distance(this.transform.position, this.StudentManager.DramaSpots[1].position) < 12)
						{
							BeingClubLeader = true;
							this.Warned = false;
						}
					}
				}

				//Debug.Log("BlondeHair is: " + GameGlobals.BlondeHair + ". Yandere's Persona is: " + this.Yandere.Persona + ". " +
				//	"Friendships are: " + PlayerGlobals.GetStudentFriend(76) + ", " + PlayerGlobals.GetStudentFriend(77) + ", " + 
				//	PlayerGlobals.GetStudentFriend(78) + ", " + PlayerGlobals.GetStudentFriend(79) + ", " + PlayerGlobals.GetStudentFriend(80));

				if (this.StudentID == 76 && GameGlobals.BlondeHair && this.Reputation.Reputation < -33.33333f &&
					this.Yandere.Persona == YanderePersonaType.Tough &&
					PlayerGlobals.GetStudentFriend(76) &&
					PlayerGlobals.GetStudentFriend(77) &&
					PlayerGlobals.GetStudentFriend(78) &&
					PlayerGlobals.GetStudentFriend(79) &&
					PlayerGlobals.GetStudentFriend(80))
				{
					//Debug.Log("Yandere-chan meets the criteria to talk to the delinquent leader about joining.");

					BeingClubLeader = true;
					this.Warned = false;
				}
				else
				{
					//Debug.Log("Yandere-chan does not meet the criteria to talk to the delinquent leader about joining.");
				}

				//this.OccultBook.SetActive(false);
				//this.SpeechLines.Stop();
				//this.Pen.SetActive(false);

				bool CannotEat = false;

				if (this.Yandere.PickUp != null && this.Yandere.PickUp.Salty && !this.Indoors)
				{
					CannotEat = true;
				}

				//If pose mode is active...
				if (this.StudentManager.Pose)
				{
					this.MyController.enabled = false;
					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;
					this.Stop = true;
					this.Pose();
				}
				//If the "Bad Time Mode" easter egg is active...
				else if (this.BadTime)
				{
					this.Yandere.EmptyHands();
					this.BecomeRagdoll();

					this.Yandere.RagdollPK.connectedBody = this.Ragdoll.AllRigidbodies[5];
					this.Yandere.RagdollPK.connectedAnchor = this.Ragdoll.LimbAnchor[4];

					this.DialogueWheel.PromptBar.ClearButtons();
					this.DialogueWheel.PromptBar.Label[1].text = "Back";
					this.DialogueWheel.PromptBar.UpdateButtons();
					this.DialogueWheel.PromptBar.Show = true;

					this.Yandere.Ragdoll = this.Ragdoll.gameObject;
					this.Yandere.SansEyes[0].SetActive(true);
					this.Yandere.SansEyes[1].SetActive(true);
					this.Yandere.GlowEffect.Play();
					this.Yandere.CanMove = false;
					this.Yandere.PK = true;
					this.DeathType = DeathType.EasterEgg;
				}
				//If the "Six" Easter Egg is active...
				else if (this.StudentManager.Six)
				{
					GameObject NewAlarmDisc = Instantiate(this.AlarmDisc, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
					NewAlarmDisc.GetComponent<AlarmDiscScript>().Originator = this;

					AudioSource.PlayClipAtPoint(this.Yandere.SixTakedown, this.transform.position);
					AudioSource.PlayClipAtPoint(this.Yandere.Snarls[Random.Range(0, this.Yandere.Snarls.Length)], this.transform.position);

                    this.Yandere.CharacterAnimation.CrossFade(AnimNames.SixEat);
					this.Yandere.TargetStudent = this;
					this.Yandere.FollowHips = true;
					this.Yandere.Attacking = true;
					this.Yandere.CanMove = false;
					this.Yandere.Eating = true;

					this.CharacterAnimation.CrossFade(this.EatVictimAnim);
                    this.CharacterAnimation[this.WetAnim].weight = 0.0f;
                    this.Pathfinding.enabled = false;
					this.Routine = false;
					this.Dying = true;
					this.Eaten = true;

					GameObject SixTarget = Instantiate(this.EmptyGameObject, transform.position, Quaternion.identity);

					this.Yandere.SixTarget = SixTarget.transform;
					this.Yandere.SixTarget.LookAt(this.Yandere.transform.position);
					this.Yandere.SixTarget.Translate(this.Yandere.SixTarget.forward);
				}
				//If the "Empty Demon" Easter Egg is active...
				else if (this.Yandere.SpiderGrow)
				{
					if (!this.Eaten && !this.Cosmetic.Empty)
					{
						AudioSource.PlayClipAtPoint(this.Yandere.SixTakedown, this.transform.position);
						AudioSource.PlayClipAtPoint(this.Yandere.Snarls[Random.Range(0, this.Yandere.Snarls.Length)], this.transform.position);

						GameObject NewHusk = Instantiate(this.Yandere.EmptyHusk, this.transform.position + this.transform.forward * .5f, Quaternion.identity);
						NewHusk.GetComponent<EmptyHuskScript>().TargetStudent = this;
						NewHusk.transform.LookAt(this.transform.position);

						this.CharacterAnimation.CrossFade(this.EatVictimAnim);
                        this.CharacterAnimation[this.WetAnim].weight = 0.0f;
                        this.Pathfinding.enabled = false;
						this.Distracted = false;
						this.Routine = false;
						this.Dying = true;
						this.Eaten = true;

						if (this.Investigating)
						{
							this.StopInvestigating();
						}

						if (this.Following)
						{
							this.FollowCountdown.gameObject.SetActive(false);
                            this.Yandere.Follower = null;
                            this.Yandere.Followers--;
							this.Following = false;
						}

						GameObject SixTarget = Instantiate(this.EmptyGameObject, transform.position, Quaternion.identity);
					}
				}
				//If the "Gazer" Easter Egg is active...
				else if (this.StudentManager.Gaze)
				{
					this.Yandere.CharacterAnimation.CrossFade("f02_gazerPoint_00");
					this.Yandere.GazerEyes.Attacking = true;
					this.Yandere.TargetStudent = this;
					this.Yandere.GazeAttacking = true;
					this.Yandere.CanMove = false;

					this.Routine = false;
				}
				//If this character is a mind-broken slave...
				else if (this.Slave)
				{
					this.Yandere.TargetStudent = this;

					this.Yandere.PauseScreen.StudentInfoMenu.Targeting = true;

					this.Yandere.PauseScreen.StudentInfoMenu.gameObject.SetActive(true);
					this.Yandere.PauseScreen.StudentInfoMenu.Column = 0;
					this.Yandere.PauseScreen.StudentInfoMenu.Row = 0;
					this.Yandere.PauseScreen.StudentInfoMenu.UpdateHighlight();
					this.StartCoroutine(this.Yandere.PauseScreen.StudentInfoMenu.UpdatePortraits());
					this.Yandere.PauseScreen.MainMenu.SetActive(false);
					this.Yandere.PauseScreen.Panel.enabled = true;
					this.Yandere.PauseScreen.Sideways = true;
					this.Yandere.PauseScreen.Show = true;
					Time.timeScale = 0.0001f;

					this.Yandere.PromptBar.ClearButtons();
					this.Yandere.PromptBar.Label[1].text = "Cancel";
					this.Yandere.PromptBar.UpdateButtons();
					this.Yandere.PromptBar.Show = true;
				}
				else if (this.FightingSlave)
				{
					this.Yandere.CharacterAnimation.CrossFade("f02_subtleStab_00");
					this.Yandere.SubtleStabbing = true;
					this.Yandere.TargetStudent = this;
					this.Yandere.CanMove = false;
				}
				//If this character is following the player...
				else if (this.Following)
				{
					this.Subtitle.UpdateLabel(SubtitleType.StudentFarewell, 0, 3.0f);
					this.Prompt.Label[0].text = "     " + "Talk";
					this.Prompt.Circle[0].fillAmount = 1.0f;

					// [af] This is the (unintuitive) Unity 5.3 way of changing emission.
					ParticleSystem.EmissionModule heartsEmission = this.Hearts.emission;
					heartsEmission.enabled = false;

					this.FollowCountdown.gameObject.SetActive(false);
                    this.Yandere.Follower = null;
                    this.Yandere.Followers--;
					this.Following = false;
					this.Routine = true;

					//This is only called if a student following the player has been told to stop.

					this.CurrentDestination = this.Destinations[this.Phase];
					this.Pathfinding.target = this.Destinations[this.Phase];
					this.Pathfinding.canSearch = true;
					this.Pathfinding.canMove = true;
					this.Pathfinding.speed = 1.0f;
				}
				//If this character can be pushed off the rooftop...
				else if (this.Pushable)
				{
					this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;

					if (!this.Male)
					{
						this.Subtitle.UpdateLabel(SubtitleType.NoteReaction, 5, 3.0f);
					}
					else
					{
						this.Subtitle.UpdateLabel(SubtitleType.NoteReactionMale, 5, 3.0f);
					}

					this.Prompt.Label[0].text = "     " + "Talk";
					this.Prompt.Circle[0].fillAmount = 1.0f;

					this.Yandere.TargetStudent = this;
					this.Yandere.Attacking = true;
					this.Yandere.RoofPush = true;
					this.Yandere.CanMove = false;

					this.Yandere.EmptyHands();
					this.EmptyHands();

					this.Distracted = true;
					this.Routine = false;
					this.Pushed = true;

					this.CharacterAnimation.CrossFade(this.PushedAnim);
				}
				//If this character can be drowned...
				else if (this.Drownable)
				{
					Debug.Log("Just began to drown someone.");

					if (this.VomitDoor != null)
					{
						this.VomitDoor.Prompt.enabled = true;
						this.VomitDoor.enabled = true;
					}

					this.Yandere.EmptyHands();

					this.Prompt.Hide();
					this.Prompt.enabled = false;

					this.Prompt.Circle[0].fillAmount = 1.0f;

					this.VomitEmitter.gameObject.SetActive(false);
					this.Police.DrownedStudentName = this.Name;
					this.MyController.enabled = false;

					this.VomitEmitter.gameObject.SetActive(false);
					this.SmartPhone.SetActive(false);
					this.Police.DrownVictims++;
					this.Distracted = true;
					this.Routine = false;
					this.Drowned = true;

					if (this.Male)
					{
						this.Subtitle.UpdateLabel(SubtitleType.DrownReaction, 1, 3.0f);
					}
					else
					{
						this.Subtitle.UpdateLabel(SubtitleType.DrownReaction, 0, 3.0f);
					}

					this.Yandere.TargetStudent = this;
					this.Yandere.Attacking = true;
					this.Yandere.CanMove = false;
					this.Yandere.Drown = true;

					this.Yandere.DrownAnim = AnimNames.FemaleFountainDrownA;

					if (this.Male)
					{
						if (Vector3.Distance(this.transform.position, this.StudentManager.transform.position) < 5)
						{
							this.DrownAnim = "fountainDrown_00_B";
						}
						else
						{
							this.DrownAnim = "toiletDrown_00_B";
						}
					}
					else
					{
						if (Vector3.Distance(this.transform.position, this.StudentManager.transform.position) < 5)
						{
							this.DrownAnim = AnimNames.FemaleFountainDrownB;
						}
						else
						{
							this.DrownAnim = AnimNames.FemaleToiletDrownB;
						}
					}

					this.CharacterAnimation.CrossFade(this.DrownAnim);
				}
				//If this class is starting...
				else if ((this.Clock.Period == 2) || (this.Clock.Period == 4) || this.CurrentDestination == this.Seat)
				{
					this.Subtitle.UpdateLabel(SubtitleType.ClassApology, 0, 3.0f);
					this.Prompt.Circle[0].fillAmount = 1.0f;
				}
				//If this character is busy doing something important right now...
				else if (this.InEvent || !this.CanTalk || this.GoAway || this.Fleeing || this.Meeting &&
					!this.Drownable || this.Wet || this.TurnOffRadio || this.InvestigatingBloodPool ||
					this.MyPlate != null && this.MyPlate.parent == this.RightHand || CannotEat ||
					this.ReturningMisplacedWeapon || this.FollowTarget != null ||
					this.Actions[this.Phase] == StudentActionType.Bully ||
					this.Actions[this.Phase] == StudentActionType.Graffiti )
				{
					this.Subtitle.UpdateLabel(SubtitleType.EventApology, 1, 3.0f);
					this.Prompt.Circle[0].fillAmount = 1.0f;
				}
				//If this character is too depressed to speak...
				else if (this.Clock.Period == 3 && this.BusyAtLunch)
				{
					this.Subtitle.UpdateLabel(SubtitleType.SadApology, 1, 3.0f);
					this.Prompt.Circle[0].fillAmount = 1.0f;
				}
				//If this character has a grudge against the player...
				else if (this.Warned)
				{
					Debug.Log("This character refuses to speak to Yandere-chan because of a grudge.");

					this.Subtitle.UpdateLabel(SubtitleType.GrudgeRefusal, 0, 3.0f);
					this.Prompt.Circle[0].fillAmount = 1.0f;
				}
				//If the player has pestered this character too much...
				else if (this.Ignoring)
				{
					this.Subtitle.UpdateLabel(SubtitleType.PhotoAnnoyance, 0, 3.0f);
					this.Prompt.Circle[0].fillAmount = 1.0f;
				}
				//If the player is trying to hand over a puzzle cube...
				else if (this.Yandere.PickUp != null && this.Yandere.PickUp.PuzzleCube)
				{
                    if (this.Investigating)
                    {
                        this.StopInvestigating();
                    }

                    this.EmptyHands();

					this.Prompt.Circle[0].fillAmount = 1.0f;

					this.PuzzleCube = this.Yandere.PickUp;
					this.Yandere.EmptyHands();
					this.PuzzleCube.enabled = false;
					this.PuzzleCube.Prompt.Hide();
					this.PuzzleCube.Prompt.enabled = false;

					this.PuzzleCube.MyRigidbody.useGravity = false;
					this.PuzzleCube.MyRigidbody.isKinematic = true;
					this.PuzzleCube.gameObject.transform.parent = this.RightHand;
					this.PuzzleCube.gameObject.transform.localScale = new Vector3(.1f, .1f, .1f);
					this.PuzzleCube.gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
					this.PuzzleCube.gameObject.transform.localPosition = new Vector3(0, -.0475f, 0);

					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;

					this.SolvingPuzzle = true;
					this.Distracted = true;
					this.Routine = false;
				}
				//If there are no  circumstances that would stop
				//the student from speaking to the player...
				else
				{
					bool WitnessedBlood = false;

					if (this.Yandere.Bloodiness + this.Yandere.GloveBlood > 0.0f && !this.Yandere.Paint)
					{
						WitnessedBlood = true;
					}

					if (!this.Witness && WitnessedBlood)
					{
						this.Prompt.Circle[0].fillAmount = 1.0f;
						this.YandereVisible = true;
						this.Alarm = 200.0f;
					}
					else
					{
						this.SpeechLines.Stop();

						this.Yandere.TargetStudent = this;

						if (!this.Grudge)
						{
							this.ClubManager.CheckGrudge(this.Club);

							if (ClubGlobals.GetClubKicked(this.Club) && BeingClubLeader)
							{
								this.Interaction = StudentInteractionType.ClubGrudge;
								this.TalkTimer = 5.0f;
								this.Warned = true;
							}
							else if (this.Yandere.Club == this.Club && BeingClubLeader && this.ClubManager.ClubGrudge)
							{
								this.Interaction = StudentInteractionType.ClubKick;
								ClubGlobals.SetClubKicked(this.Club, true);
								this.TalkTimer = 5.0f;
								this.Warned = true;
							}
							else if (this.Prompt.Label[0].text == ("     " + "Feed"))
							{
								this.Interaction = StudentInteractionType.Feeding;
								this.TalkTimer = 10.0f;
							}
							else if (this.Prompt.Label[0].text == ("     " + "Give Snack"))
							{
								this.Yandere.Interaction = YandereInteractionType.GivingSnack;
								this.Yandere.TalkTimer = 3.0f;

								this.Interaction = StudentInteractionType.Idle;
							}
							else if (this.Prompt.Label[0].text == ("     " + "Ask For Help"))
							{
								this.Yandere.Interaction = YandereInteractionType.AskingForHelp;
								this.Yandere.TalkTimer = 5.0f;

								this.Interaction = StudentInteractionType.Idle;
							}
							else
							{
								this.DistanceToDestination = Vector3.Distance(
									this.transform.position, this.Destinations[this.Phase].position);

								if (this.Sleuthing)
								{
									this.DistanceToDestination = Vector3.Distance(this.transform.position, this.SleuthTarget.position);
								}

								/*
								Debug.Log("Pathfinding is: " + this.Pathfinding.canMove +
										  ", Distance is: " + this.DistanceToDestination +
										  ", Action is: " + this.Actions[this.Phase] + 
										  ", Armband is: " + this.Armband.activeInHierarchy);
								*/

								//Debug.Log("First, here.");

								if (BeingClubLeader)
								{
									//Debug.Log("Then, here.");

									int ClubBonus = 0;
									if (this.Sleuthing){ClubBonus = 5;}
									else{ClubBonus = 0;}

									if (GameGlobals.EmptyDemon){ClubBonus = (int)this.Club * -1;}

									this.Subtitle.UpdateLabel(SubtitleType.ClubGreeting, (int)this.Club + ClubBonus, 4.0f);
									this.DialogueWheel.ClubLeader = true;
								}
								else
								{
									//Debug.Log("Or, here.");

									this.Subtitle.UpdateLabel(SubtitleType.Greeting, 0, 3.0f);
								}

								if (this.Club != ClubType.Council && this.Club != ClubType.Delinquent)
								{
									if (this.Male &&
										((this.Yandere.Class.Seduction + this.Yandere.Class.SeductionBonus) > 0) ||
										((this.Yandere.Class.Seduction + this.Yandere.Class.SeductionBonus) > 4))
									{
										// [af] This is the (unintuitive) Unity 5.3 way of changing emission.
										ParticleSystem.EmissionModule heartsEmission = this.Hearts.emission;
										heartsEmission.rateOverTime = this.Yandere.Class.Seduction + this.Yandere.Class.SeductionBonus;
										heartsEmission.enabled = true;

										this.Hearts.Play();
									}
								}

								this.StudentManager.DisablePrompts();
								this.StudentManager.VolumeDown();

								this.DialogueWheel.HideShadows();
								this.DialogueWheel.Show = true;
								this.DialogueWheel.Panel.enabled = true;

								this.TalkTimer = 0.0f;

								//Learning about socializing
								if (!ConversationGlobals.GetTopicDiscovered(20))
								{
									this.Yandere.NotificationManager.TopicName = "Socializing";
									this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
									ConversationGlobals.SetTopicDiscovered(20, true);
								}

								//How does this student feel about socializing?
								if (!ConversationGlobals.GetTopicLearnedByStudent(20, this.StudentID))
								{
									this.Yandere.NotificationManager.TopicName = "Socializing";
									this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
									ConversationGlobals.SetTopicLearnedByStudent(20, this.StudentID, true);
								}

								//Learning about being alone
								if (!ConversationGlobals.GetTopicDiscovered(21))
								{
									this.Yandere.NotificationManager.TopicName = "Solitude";
									this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
									ConversationGlobals.SetTopicDiscovered(21, true);
								}

								//How does this student feel about being alone?
								if (!ConversationGlobals.GetTopicLearnedByStudent(21, this.StudentID))
								{
									this.Yandere.NotificationManager.TopicName = "Solitude";
									this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
									ConversationGlobals.SetTopicLearnedByStudent(21, this.StudentID, true);
								}
							}
						}
						else
						{
							if (BeingClubLeader)
							{
								this.Interaction = StudentInteractionType.ClubUnwelcome;
								this.TalkTimer = 5.0f;
								this.Warned = true;
							}
							else
							{
								this.Interaction = StudentInteractionType.PersonalGrudge;
								this.TalkTimer = 5.0f;
								this.Warned = true;
							}
						}

						this.Yandere.ShoulderCamera.OverShoulder = true;
						this.Pathfinding.canSearch = false;
						this.Pathfinding.canMove = false;
						this.Obstacle.enabled = true;
						this.Giggle = null;

						this.Yandere.WeaponMenu.KeyboardShow = false;
						this.Yandere.Obscurance.enabled = false;
						this.Yandere.WeaponMenu.Show = false;
						this.Yandere.YandereVision = false;
						this.Yandere.CanMove = false;
						this.Yandere.Talking = true;

						this.Investigating = false;
						this.Talk.enabled = true;
						this.Reacted = false;
						this.Routine = false;
						this.Talking = true;
						this.ReadPhase = 0;

						this.EmptyHands();

						bool Sunbathing = false;

						if (this.CurrentAction == StudentActionType.Sunbathe && this.SunbathePhase > 2)
						{
							this.SunbathePhase = 2;
							Sunbathing = true;
						}

						if (this.Phoneless)
						{
							this.SmartPhone.SetActive(false);
						}
						else
						{
							if (this.Sleuthing)
							{
								if (!this.Scrubber.activeInHierarchy)
								{
									this.SmartPhone.SetActive(true);
								}
								else
								{
									this.SmartPhone.SetActive(false);
								}
							}
							else if (this.Persona != PersonaType.PhoneAddict)
							{
								this.SmartPhone.SetActive(false);
							}
							else if (!this.Scrubber.activeInHierarchy && !Sunbathing)
							{
								this.SmartPhone.SetActive(true);
							}
						}

						this.ChalkDust.Stop();
						this.StopPairing();
					}
				}
			}
		}

		/////////////////////////
		///// ATTACK INPUT //////
		/////////////////////////

		if (this.Prompt.Circle[2].fillAmount == 0.0f ||
			this.Yandere.Sanity < 33.33333f &&
			this.Yandere.CanMove &&
			!this.Prompt.HideButton[2] &&
			this.Prompt.InSight &&
			this.Club != ClubType.Council &&
			!this.Struggling &&
			!this.Chasing &&
            this.DistanceToPlayer < 1.4f &&
            this.SeenByYandere() &&
            this.StudentID > 1)

			//this.StudentID > 1 && this.DistanceToPlayer <= 1 &&
			//this.Club != ClubType.Council && this.Yandere.Armed && this.Yandere.CanMove && this.Yandere.Sanity < 33.33333f)
		{
			Debug.Log(this.Name + " was attacked because the player pressed the X button, or because Yandere-chan had low sanity.");

			float Angle = Vector3.Angle(-this.transform.forward,
				this.Yandere.transform.position - this.transform.position);

			this.Yandere.AttackManager.Stealth = Mathf.Abs(Angle) <= 45.0f;

			bool NoBlood = false;

			if (this.Yandere.AttackManager.Stealth)
			{
				if (this.Yandere.EquippedWeapon.Type == WeaponType.Bat || this.Yandere.EquippedWeapon.Type == WeaponType.Weight)
				{
					NoBlood = true;
				}
			}

			if (NoBlood || this.StudentManager.OriginalUniforms + this.StudentManager.NewUniforms > 1)
			{
				if (this.ClubActivityPhase < 16)
				{
					bool DelinquentCheck = false;

					if (this.Club == ClubType.Delinquent && !Injured && !this.Yandere.AttackManager.Stealth &&
                        !this.RespectEarned && !this.SolvingPuzzle)
					{
						Debug.Log(this.Name + " knows that Yandere-chan is tyring to attack him.");

						DelinquentCheck = true;

						this.Fleeing = false;
						this.Patience = 1;

						this.Shove();

						this.SpawnAlarmDisc();
					}

					if (this.Yandere.AttackManager.Stealth)
					{
						this.SpawnSmallAlarmDisc();
					}

					if (!DelinquentCheck)
					{
						if (!this.Yandere.NearSenpai && !this.Yandere.Attacking &&
							(this.Yandere.Stance.Current != StanceType.Crouching))
						{
							if (this.Yandere.EquippedWeapon.Flaming ||
								this.Yandere.CyborgParts[1].activeInHierarchy)
							{
								this.Yandere.SanityBased = false;
							}

							if (this.Strength == 9)
							{
								if (!this.Yandere.AttackManager.Stealth)
								{
									this.CharacterAnimation.CrossFade(AnimNames.FemaleDramaticFrontal);
								}
								else
								{
									this.CharacterAnimation.CrossFade(AnimNames.FemaleDramaticStealth);
								} 

								this.Yandere.CharacterAnimation.CrossFade(AnimNames.FemaleReadyToFight);
								this.Yandere.CanMove = false;

								this.DramaticCamera.enabled = true;
								this.DramaticCamera.rect = new Rect(0, .5f, 1, 0);
								this.DramaticCamera.gameObject.SetActive(true);
								this.DramaticCamera.gameObject.GetComponent<AudioSource>().Play();
								this.DramaticReaction = true;

								this.Pathfinding.canSearch = false;
								this.Pathfinding.canMove = false;
								this.Routine = false;
							}
							else
							{
                                if (this.Yandere.EquippedWeapon.WeaponID != 27 ||
                                    this.Yandere.EquippedWeapon.WeaponID == 27 && this.Yandere.AttackManager.Stealth)
                                {
								    this.AttackReaction();
                                }
                                else
                                {
                                    Debug.Log("Can't frontal attack with garrote.");
                                }
                            }
						}
					}
				}
			}
			else
			{
				if (!this.Yandere.ClothingWarning)
				{
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Clothing);

					this.StudentManager.TutorialWindow.ShowClothingMessage = true;
					this.Yandere.ClothingWarning = true;
				}
			}
		}
	}

	void UpdateDying()
	{
		this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;

		this.Alarm -= Time.deltaTime * 100.0f * (1.0f / this.Paranoia);

		if (this.Attacked)
		{
			/*
			if (this.Follower != null)
			{
				this.Follower.DistractionSpot = this.Yandere.transform.position;
				this.Follower.BecomeAlarmed();
			}
			*/

			if (!this.Teacher)
			{
				if (this.Strength == 9)
				{
					if (!this.StudentManager.Stop)
					{
						this.StudentManager.StopMoving();

						this.Yandere.RPGCamera.enabled = false;
						this.SmartPhone.SetActive(false);
						this.Police.Show = false;
					}

					Debug.Log("The mysterious obstacle is counter-attacking!");

					this.CharacterAnimation.CrossFade("f02_moCounterB_00");

					if (!this.WitnessedMurder)
					{
						if (this.CharacterAnimation["f02_moLipSync_00"].weight == 0)
						{
							this.CharacterAnimation["f02_moLipSync_00"].weight = 1;
							this.CharacterAnimation["f02_moLipSync_00"].time = 0;
							this.CharacterAnimation.Play("f02_moLipSync_00");
						}
					}

					this.targetRotation = Quaternion.LookRotation(new Vector3(
						this.Yandere.transform.position.x,
						this.transform.position.y,
						this.Yandere.transform.position.z) - this.transform.position);
					this.transform.rotation = Quaternion.Slerp(
						this.transform.rotation, this.targetRotation, Time.deltaTime * 10.0f);

					this.MoveTowardsTarget(this.Yandere.transform.position + this.Yandere.transform.forward);
				}
				else
				{
					this.EyeShrink = Mathf.Lerp(this.EyeShrink, 1.0f, Time.deltaTime * 10.0f);

					if (this.Alive && !this.Tranquil)
					{
						if (!this.Yandere.SanityBased)
						{
							this.targetRotation = Quaternion.LookRotation(new Vector3(
								this.Yandere.transform.position.x,
								this.transform.position.y,
								this.Yandere.transform.position.z) - this.transform.position);
							this.transform.rotation = Quaternion.Slerp(
								this.transform.rotation, this.targetRotation, Time.deltaTime * 10.0f);

							if (this.Yandere.EquippedWeapon.WeaponID == 11)
							{
								this.CharacterAnimation.CrossFade(this.CyborgDeathAnim);
								this.MoveTowardsTarget(this.Yandere.transform.position + this.Yandere.transform.forward);

								if (this.CharacterAnimation[this.CyborgDeathAnim].time >=
									(this.CharacterAnimation[this.CyborgDeathAnim].length - 0.25f))
								{
									if (this.Yandere.EquippedWeapon.WeaponID == 11)
									{
										Instantiate(this.BloodyScream,
											this.transform.position + Vector3.up, Quaternion.identity);
										this.DeathType = DeathType.EasterEgg;

										this.BecomeRagdoll();
										this.Ragdoll.Dismember();
									}
								}
							}
							else if (this.Yandere.EquippedWeapon.WeaponID == 7)
							{
								this.CharacterAnimation.CrossFade(this.BuzzSawDeathAnim);
								this.MoveTowardsTarget(this.Yandere.transform.position + this.Yandere.transform.forward);
							}
							else
							{
								if (!this.Yandere.EquippedWeapon.Concealable)
								{
									this.CharacterAnimation.CrossFade(this.SwingDeathAnim);
									this.MoveTowardsTarget(this.Yandere.transform.position + this.Yandere.transform.forward);
								}
								else
								{
									this.CharacterAnimation.CrossFade(this.DefendAnim);
									this.MoveTowardsTarget(this.Yandere.transform.position +
										(this.Yandere.transform.forward * 0.10f));
								}
							}
						}
						else
						{
							this.MoveTowardsTarget(this.Yandere.transform.position +
								(this.Yandere.transform.forward * this.Yandere.AttackManager.Distance));

							if (!this.Yandere.AttackManager.Stealth)
							{
								this.targetRotation = Quaternion.LookRotation(new Vector3(
									this.Yandere.transform.position.x,
									this.transform.position.y,
									this.Yandere.transform.position.z) - this.transform.position);
							}
							else
							{
								this.targetRotation = Quaternion.LookRotation(this.transform.position - new Vector3(
									this.Yandere.transform.position.x,
									this.transform.position.y,
									this.Yandere.transform.position.z));
							}

							this.transform.rotation = Quaternion.Slerp(
								this.transform.rotation, this.targetRotation, Time.deltaTime * 10.0f);
						}
					}
					else
					{
						this.CharacterAnimation.CrossFade(this.DeathAnim);

						if (this.CharacterAnimation[this.DeathAnim].time < 1.0f)
						{
							this.transform.Translate(Vector3.back * Time.deltaTime);
						}
						else
						{
							Debug.Log("Reloaded from save, calling BecomeRagdoll()");

							this.BecomeRagdoll();
						}
					}
				}
			}
			else
			{
				if (!this.StudentManager.Stop)
				{
					this.StudentManager.StopMoving();

					this.Yandere.Laughing = false;
					this.Yandere.Dipping = false;
					this.Yandere.RPGCamera.enabled = false;
					this.SmartPhone.SetActive(false);
					this.Police.Show = false;
				}

				this.CharacterAnimation.CrossFade(this.CounterAnim);

				this.targetRotation = Quaternion.LookRotation(new Vector3(
					this.Yandere.transform.position.x,
					this.transform.position.y,
					this.Yandere.transform.position.z) - this.transform.position);
				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.targetRotation, Time.deltaTime * 10.0f);
				this.MoveTowardsTarget(this.Yandere.transform.position + this.Yandere.transform.forward);

				this.transform.localScale = Vector3.Lerp(
					this.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
			}
		}
	}

	void UpdatePushed()
	{
		this.Alarm -= Time.deltaTime * 100.0f * (1.0f / this.Paranoia);

		this.EyeShrink = Mathf.Lerp(this.EyeShrink, 1.0f, Time.deltaTime * 10.0f);

		if (this.CharacterAnimation[this.PushedAnim].time >=
			this.CharacterAnimation[this.PushedAnim].length)
		{
			this.BecomeRagdoll();
		}
	}

	void UpdateDrowned()
	{
		this.SplashTimer += Time.deltaTime;

		if (this.SplashTimer > 3 && this.SplashTimer < 100)
		{
			this.DrowningSplashes.Play();
			this.SplashTimer += 100;
		}

		this.Alarm -= Time.deltaTime * 100.0f * (1.0f / this.Paranoia);

		this.EyeShrink = Mathf.Lerp(this.EyeShrink, 1.0f, Time.deltaTime * 10.0f);

		if (this.CharacterAnimation[this.DrownAnim].time >=
			this.CharacterAnimation[this.DrownAnim].length)
		{
			this.BecomeRagdoll();
		}
	}

	void UpdateWitnessedMurder()
	{
		if (this.Threatened)
		{
			this.UpdateAlarmed();
		}
		else if (!this.Fleeing && !this.Shoving)
		{
			if (this.StudentID > 1)
			{
				if (this.Persona != PersonaType.Evil)
				{
					this.EyeShrink += Time.deltaTime * 0.20f;
				}
			}

			// Beef up the student if their close friend/loved one is targeted.
			if ((this.Yandere.TargetStudent != null) &&
				this.LovedOneIsTargeted(this.Yandere.TargetStudent.StudentID))
			{
				this.Strength = 5;
				this.Persona = PersonaType.Heroic;

				this.SmartPhone.SetActive(false);
				this.SprintAnim = this.OriginalSprintAnim;
			}

			if (this.Club != ClubType.Delinquent || this.Club == ClubType.Delinquent && this.Injured)
			{
				if ((this.Yandere.TargetStudent == null) &&
					this.LovedOneIsTargeted(this.Yandere.NearestCorpseID))
				{
					this.Strength = 5;
					if (this.Injured){this.Strength = 1;}
					this.Persona = PersonaType.Heroic;
				}
			}

			if (this.Yandere.PickUp != null)
			{
				if (this.Yandere.PickUp.BodyPart != null)
				{
					if (this.Yandere.PickUp.BodyPart.Type == 1)
					{
						// Beef up the student if Yandere-chan is holding
						// their close friend/loved one's decapitated head.
						if (this.LovedOneIsTargeted(this.Yandere.PickUp.BodyPart.StudentID))
						{
							this.Strength = 5;
							this.Persona = PersonaType.Heroic;

							this.SmartPhone.SetActive(false);
							this.SprintAnim = this.OriginalSprintAnim;
						}
					}
				}
			}

			if (this.Persona == PersonaType.PhoneAddict && !this.Phoneless)
			{
				if (!this.Attacked)
				{
					this.PhoneAddictCameraUpdate();
				}
			}
			else
			{
				this.CharacterAnimation.CrossFade(this.ScaredAnim);
			}
				
			//Debug.Log("I am watching you commit murder.");

			this.targetRotation = Quaternion.LookRotation(new Vector3(
				this.Yandere.Hips.position.x,
				this.transform.position.y,
				this.Yandere.Hips.position.z) - this.transform.position);
			this.transform.rotation = Quaternion.Slerp(
				this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);

			if (!this.Yandere.Struggling)// && !this.Yandere.DelinquentFighting)
			{
				if (this.Persona != PersonaType.Heroic &&
					this.Persona != PersonaType.Dangerous &&
					//this.Persona != PersonaType.Protective &&
					this.Persona != PersonaType.Violent)
				{
					this.AlarmTimer += Time.deltaTime * this.MurdersWitnessed;

					if (this.Urgent)
					{
						if (this.Yandere.CanMove)
						{
                            if (this.StudentID == 1)
                            {
                                this.SenpaiNoticed();
                            }

							this.AlarmTimer += 5;
						}
					}
				}
				else
				{
					this.AlarmTimer += Time.deltaTime * (this.MurdersWitnessed * 5.0f);
				}
			}
			else if (this.Yandere.Won)
			{
				Debug.Log (this.Name + " was waiting for a struggle to end, and saw Yandere-chan win the struggle.");

				this.Urgent = true;
			}

			if (this.AlarmTimer > 5.0f)
			{
				this.PersonaReaction();
				this.AlarmTimer = 0.0f;
			}
			else if (this.AlarmTimer > 1.0f)
			{
				if (!this.Reacted)
				{
					if ((this.StudentID > 1) || (this.Yandere.Mask != null))
					{
						if (this.StudentID == 1)
						{
							Debug.Log("Senpai saw a mask.");

							this.Persona = PersonaType.Heroic;
							this.PersonaReaction();
						}

						if (!this.Teacher)
						{
							if (this.Persona != PersonaType.Evil)
							{
								if (this.Club == ClubType.Delinquent)
								{
									//this.Subtitle.UpdateLabel(SubtitleType.DelinquentMurderReaction, 1, 3.0f);
									this.SmartPhone.SetActive(false);
								}
								else
								{
									if (this.StudentID == 10)
									{
										this.Subtitle.UpdateLabel(SubtitleType.ObstacleMurderReaction, 1, 3.0f);
									}
									else
									{
										this.Subtitle.UpdateLabel(SubtitleType.MurderReaction, 1, 3.0f);
									}
								}
							}
						}
						else
						{
							if (this.WitnessedCoverUp)
							{
								this.Subtitle.UpdateLabel(SubtitleType.TeacherCoverUpHostile, 1, 5.0f);
							}
							else
							{
								DetermineWhatWasWitnessed();
								DetermineTeacherSubtitle();

								//this.Subtitle.UpdateLabel(SubtitleType.TeacherMurderReaction, Random.Range(1, 3), 3.0f);
							}

							//this.StudentManager.Portal.SetActive(false);
						}
					}
					else
					{
						Debug.Log("Senpai witnessed murder, and entered a specific murder reaction animation.");

						this.MurderReaction = Random.Range(1, 6);

						this.CharacterAnimation.CrossFade("senpaiMurderReaction_0" + this.MurderReaction);

						this.GameOverCause = GameOverType.Murder;

						this.SenpaiNoticed();

						this.CharacterAnimation[AnimNames.MaleScaredFace].weight = 0.0f;
						this.CharacterAnimation[this.AngryFaceAnim].weight = 0.0f;
						this.Yandere.ShoulderCamera.enabled = true;
						this.Yandere.ShoulderCamera.Noticed = true;
						this.Yandere.RPGCamera.enabled = false;
						this.Stop = true;//enabled = false;
					}

					this.Reacted = true;
				}
			}
		}
	}

	void UpdateAlarmed()
	{
		//Debug.Log(Name + " is calling UpdateAlarmed()");

		if (!this.Threatened)
		{
			if (this.Yandere.Medusa && this.YandereVisible)
			{
				this.TurnToStone();
				return;
			}
				
			if (this.Persona != PersonaType.PhoneAddict && !this.Sleuthing){this.SmartPhone.SetActive(false);}

			this.OccultBook.SetActive(false);
			this.Pen.SetActive(false);
			this.SpeechLines.Stop();
			this.ReadPhase = 0;

			if (this.WitnessedCorpse)
			{
				if (!this.WalkBack)
				{
					if (this.StudentID == 1)
					{
						//Debug.Log("Senpai entered his scared animation.");
					}

					if (this.Persona != PersonaType.PhoneAddict){this.CharacterAnimation.CrossFade(this.ScaredAnim);}
					else if (!this.Phoneless)
					{
						if (!this.Attacked)
						{
							this.PhoneAddictCameraUpdate();
						}
					}
				}
				else
				{
					// [af] Commented in JS code.
					//MyRigidbody.MovePosition(transform.position + transform.forward * -.5 * Time.deltaTime);

					Debug.Log(this.Name + " is walking backwards");

					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;

					this.MyController.Move(this.transform.forward * (-0.50f * Time.deltaTime));
					this.CharacterAnimation.CrossFade(this.WalkBackAnim);

					this.WalkBackTimer -= Time.deltaTime;

					if (this.WalkBackTimer <= 0.0f)
					{
						this.WalkBack = false;
					}
				}
			}
			else if (this.WitnessedLimb)
			{
				//Debug.Log("Student just noticed a limb.");
			}
			else if (this.WitnessedBloodyWeapon)
			{
				//Debug.Log("Student just noticed a bloody weapon.");
			}
			else if (this.WitnessedBloodPool)
			{
				//Debug.Log("Student just noticed a blood pool.");
			}
			else if (this.WitnessedWeapon)
			{
				//Debug.Log("Student just noticed a weapon.");
			}
			else
			{
				if (this.StudentID > 1)
				{
					if (this.Witness)
					{
						//Debug.Log(this.Name + " is performing their LeanAnim.");

						this.CharacterAnimation.CrossFade(this.LeanAnim);
					}
					else
					{
						this.CharacterAnimation.CrossFade(this.IdleAnim);

						if (this.FocusOnYandere)
						{
							if (this.DistanceToPlayer < 1 && !this.Injured)
							{
								this.AlarmTimer = 0;

								if (this.Club == ClubType.Council || this.Club == ClubType.Delinquent && !this.Injured)
								{
									this.ThreatTimer += Time.deltaTime;

									if (this.ThreatTimer > 5)
									{
										if (!this.Yandere.Struggling && !this.Yandere.DelinquentFighting)
										{
											//If we're actually within sight of the player
											if (this.Prompt.InSight)
											{
												this.ThreatTimer = 0;
												this.Shove();
											}
										}
									}
								}
							}

							this.DistractionSpot = new Vector3(
								this.Yandere.transform.position.x,
								this.transform.position.y,
								this.Yandere.transform.position.z);
						}
					}
				}
				else
				{
					this.CharacterAnimation.CrossFade(this.LeanAnim);
				}
			}

			if (this.WitnessedMurder)
			{
				this.targetRotation = Quaternion.LookRotation(new Vector3(
					this.Yandere.transform.position.x,
					this.transform.position.y,
					this.Yandere.transform.position.z) - this.transform.position);
				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);
			}
			else if (this.WitnessedCorpse)
			{
				if (this.Corpse != null)
				{
					if (this.Corpse.AllColliders[0] != null)
					{
						this.targetRotation = Quaternion.LookRotation(new Vector3(
							this.Corpse.AllColliders[0].transform.position.x,
							this.transform.position.y,
							this.Corpse.AllColliders[0].transform.position.z) - this.transform.position);
						this.transform.rotation = Quaternion.Slerp(
							this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);
					}
				}
			}
			else if (this.WitnessedBloodPool || this.WitnessedLimb || this.WitnessedWeapon)
			{
				if (this.BloodPool != null)
				{
					this.targetRotation = Quaternion.LookRotation(new Vector3(
						this.BloodPool.transform.position.x,
						this.transform.position.y,
						this.BloodPool.transform.position.z) - this.transform.position);
					this.transform.rotation = Quaternion.Slerp(
						this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);
				}
			}
			else
			{
				if (!this.DiscCheck)
				{
					this.targetRotation = Quaternion.LookRotation(new Vector3(
						this.Yandere.transform.position.x,
						this.transform.position.y,
						this.Yandere.transform.position.z) - this.transform.position);
					this.transform.rotation = Quaternion.Slerp(
						this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);
				}
				else
				{

					if (!this.FocusOnYandere)
					{
						this.targetRotation = Quaternion.LookRotation(
							this.DistractionSpot - this.transform.position);
					}
					else
					{
						this.targetRotation = Quaternion.LookRotation(new Vector3(
							this.DistractionSpot.x,
							this.transform.position.y,
							this.DistractionSpot.z) - this.transform.position);
					}

					this.transform.rotation = Quaternion.Slerp(
							this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);
				}
			}

			if (!this.Fleeing && !this.Yandere.DelinquentFighting)
			{
				//Debug.Log("Increasing AlarmTimer here.");

				this.AlarmTimer += Time.deltaTime * (1.0f - this.Hesitation);
			}

			if (!this.CanStillNotice)
			{
				this.Alarm -= Time.deltaTime * 100.0f * (1.0f / this.Paranoia) * 5;
			}

			//Debug.Log("AlarmTimer is: " + AlarmTimer);

            if (this.AlarmTimer < 5.0f)
            {
                if (this.BloodPool != null && this.CanSeeObject(this.Yandere.gameObject, this.Yandere.HeadPosition) &&
                    this.BloodPool.parent == this.Yandere.RightHand)
                {
                    this.ForgetAboutBloodPool();
                }
            }

			if (this.AlarmTimer > 5.0f)
			{
				this.EndAlarm();
			}
			else if (this.AlarmTimer > 1.0f)
			{
				//Debug.Log("1");

				if (!this.Reacted)
				{
					//Debug.Log("2");

					if (this.Teacher)
					{
						//Debug.Log("3");

						if (!this.WitnessedCorpse)
						{
							Debug.Log("A teacher's subtitle is now being determined.");

							this.CharacterAnimation.CrossFade(this.IdleAnim);

                            switch(this.Witnessed)
                            {
                                case StudentWitnessType.WeaponAndBloodAndInsanity:
                                case StudentWitnessType.WeaponAndInsanity:
                                case StudentWitnessType.BloodAndInsanity:
                                case StudentWitnessType.Insanity:
                                case StudentWitnessType.Poisoning:
                                case StudentWitnessType.CleaningItem:
                                    this.Subtitle.UpdateLabel(SubtitleType.TeacherInsanityReaction, 1, 6.0f);
                                    this.GameOverCause = GameOverType.Insanity;
                                break;
                                
                                case StudentWitnessType.WeaponAndBlood:
                                case StudentWitnessType.Weapon:
                                    this.Subtitle.UpdateLabel(SubtitleType.TeacherWeaponReaction, 1, 6.0f);
                                    this.GameOverCause = GameOverType.Weapon;
                                break;

                                case StudentWitnessType.Blood:
                                    this.Subtitle.UpdateLabel(SubtitleType.TeacherBloodReaction, 1, 6.0f);
                                    this.GameOverCause = GameOverType.Blood;
                                break;

                                case StudentWitnessType.Lewd:
                                    this.Subtitle.UpdateLabel(SubtitleType.TeacherLewdReaction, 1, 6.0f);
                                    this.GameOverCause = GameOverType.Lewd;
                                    break;

                                case StudentWitnessType.Violence:
                                    Debug.Log("A teacher witnessed violence.");
                                    this.Subtitle.UpdateLabel(SubtitleType.TeacherTrespassingReaction, 1, 6.0f);
                                    this.GameOverCause = GameOverType.Violence;
                                    this.Concern = 5;
                                    break;

                                case StudentWitnessType.Trespassing:
                                    this.Subtitle.UpdateLabel(SubtitleType.TeacherTrespassingReaction, this.Concern, 5.0f);
                                    break;

                                case StudentWitnessType.Theft:
                                case StudentWitnessType.Pickpocketing:
                                    this.Subtitle.UpdateLabel(SubtitleType.TeacherTheftReaction, 1, 6.0f);
                                    break;
                            }

							if (this.Club == ClubType.Council)
							{
								Destroy(this.Subtitle.CurrentClip);
								this.Subtitle.UpdateLabel(SubtitleType.CouncilToCounselor, this.ClubMemberID, 6.0f);
							}

							if (this.BloodPool != null)
							{
								Debug.Log("The teacher was alarmed because she saw something weird on the ground.");

								Destroy(this.Subtitle.CurrentClip);
								this.Subtitle.UpdateLabel(SubtitleType.BloodPoolReaction, 2, 5.0f);

								PromptScript BloodPrompt = this.BloodPool.GetComponent<PromptScript>();

								if (BloodPrompt != null)
								{
									Debug.Log("Disabling a bloody object's prompt because a teacher is heading for it.");

									BloodPrompt.Hide();
									BloodPrompt.enabled = false;
								}
							}
						}
						else
						{
							Debug.Log("A teacher found a corpse.");

							this.Concern = 1;

							this.DetermineWhatWasWitnessed();
							this.DetermineTeacherSubtitle();

							if (this.WitnessedMurder)
							{
								this.MurdersWitnessed++;

								if (!this.Yandere.Chased)
								{
									Debug.Log ("A teacher has reached ChaseYandere() through UpdateAlarm().");

									this.ChaseYandere();
								}
							}
						}

						if (!this.Guarding && !this.Chasing)
						{
							//Debug.Log("5");

							if (this.YandereVisible && this.Concern == 5 || this.Yandere.Noticed)
							{
								Debug.Log("Yandere-chan is getting sent to the guidance counselor.");

								if (this.Witnessed == StudentWitnessType.Theft)
								{
									if (this.Yandere.StolenObject != null)
									{
										this.Yandere.StolenObject.SetActive(true);
										this.Yandere.StolenObject = null;

										this.Yandere.Inventory.IDCard = false;
									}
								}

								this.StudentManager.CombatMinigame.Stop();
								this.CharacterAnimation[this.AngryFaceAnim].weight = 1.0f;
								this.Yandere.ShoulderCamera.enabled = true;
								this.Yandere.ShoulderCamera.Noticed = true;
								this.Yandere.RPGCamera.enabled = false;
								this.Stop = true;//enabled = false;
							}
						}
					}
					//If it's not a teacher and it's not Senpai...
					else if (this.StudentID > 1 || (this.Yandere.Mask != null))
					{
						//Geiju
						if (this.StudentID == 41)
						{
							this.Subtitle.UpdateLabel(SubtitleType.Impatience, 6, 5.0f);
						}
						else
						{
							if (this.RepeatReaction)
							{
								this.Subtitle.UpdateLabel(SubtitleType.RepeatReaction, 1, 3.0f);
								this.RepeatReaction = false;
							}
							else
							{
								if (this.Club != ClubType.Delinquent)
								{
									if (this.Witnessed == StudentWitnessType.WeaponAndBloodAndInsanity)
									{
										this.Subtitle.UpdateLabel(SubtitleType.WeaponAndBloodAndInsanityReaction, 1, 3.0f);
									}
									else if (this.Witnessed == StudentWitnessType.WeaponAndBlood)
									{
										this.Subtitle.UpdateLabel(SubtitleType.WeaponAndBloodReaction, 1, 3.0f);
									}
									else if (this.Witnessed == StudentWitnessType.WeaponAndInsanity)
									{
										this.Subtitle.UpdateLabel(SubtitleType.WeaponAndInsanityReaction, 1, 3.0f);
									}
									else if (this.Witnessed == StudentWitnessType.BloodAndInsanity)
									{
										this.Subtitle.UpdateLabel(SubtitleType.BloodAndInsanityReaction, 1, 3.0f);
									}
									else if (this.Witnessed == StudentWitnessType.Weapon)
									{
										this.Subtitle.StudentID = this.StudentID;
										this.Subtitle.UpdateLabel(SubtitleType.WeaponReaction, this.WeaponWitnessed, 3.0f);
									}
									else if (this.Witnessed == StudentWitnessType.Blood)
									{
										if (!this.Bloody)
										{
											this.Subtitle.UpdateLabel(SubtitleType.BloodReaction, 1, 3.0f);
										}
										else
										{
											this.Subtitle.UpdateLabel(SubtitleType.WetBloodReaction, 1, 3.0f);
											this.Witnessed = StudentWitnessType.None;
											this.Witness = false;
										}
									}
									else if (this.Witnessed == StudentWitnessType.Insanity)
									{
										this.Subtitle.UpdateLabel(SubtitleType.InsanityReaction, 1, 3.0f);
									}
									else if (this.Witnessed == StudentWitnessType.Lewd)
									{
										this.Subtitle.UpdateLabel(SubtitleType.LewdReaction, 1, 3.0f);
									}
									else if (this.Witnessed == StudentWitnessType.CleaningItem)
									{
										this.Subtitle.UpdateLabel(SubtitleType.SuspiciousReaction, 0, 5.0f);
									}
									else if (this.Witnessed == StudentWitnessType.Suspicious)
									{
										this.Subtitle.UpdateLabel(SubtitleType.SuspiciousReaction, 1, 5.0f);
									}
									else if (this.Witnessed == StudentWitnessType.Corpse)
									{
										Debug.Log(this.Name + " is currently reacting to a corpse and deciding what subtitle to use.");

										if (this.StudentID == 10 && Corpse.StudentID == this.StudentManager.RivalID)
										{
											this.Subtitle.Speaker = this;
											this.Subtitle.UpdateLabel(SubtitleType.RaibaruRivalDeathReaction, 1, 5.0f);

											Debug.Log("Raibaru is reacting to Osana's corpse with a unique subtitle.");
										}
										else if (this.Club == ClubType.Council)
										{
												 if (this.StudentID == 86){this.Subtitle.UpdateLabel(SubtitleType.CouncilCorpseReaction, 1, 5.0f);}
											else if (this.StudentID == 87){this.Subtitle.UpdateLabel(SubtitleType.CouncilCorpseReaction, 2, 5.0f);}
											else if (this.StudentID == 88){this.Subtitle.UpdateLabel(SubtitleType.CouncilCorpseReaction, 3, 5.0f);}
											else if (this.StudentID == 89){this.Subtitle.UpdateLabel(SubtitleType.CouncilCorpseReaction, 4, 5.0f);}
										}
										else if (this.Persona == PersonaType.Evil)
										{
											this.Subtitle.UpdateLabel(SubtitleType.EvilCorpseReaction, 1, 5.0f);
										}
										else
										{
											if (!this.Corpse.Choking)
											{
												this.Subtitle.UpdateLabel(SubtitleType.CorpseReaction, 0, 5.0f);
											}
											else
											{
												this.Subtitle.UpdateLabel(SubtitleType.CorpseReaction, 1, 5.0f);
											}
										}
									}
									else if (this.Witnessed == StudentWitnessType.Interruption)
									{
										if (this.StudentID == 11)
										{
											this.Subtitle.UpdateLabel(SubtitleType.InterruptionReaction, 1, 5.0f);
										}
										else
										{
											this.Subtitle.UpdateLabel(SubtitleType.InterruptionReaction, 2, 5.0f);
										}
									}
									else if (this.Witnessed == StudentWitnessType.Eavesdropping)
									{
										if (this.StudentID == this.StudentManager.RivalID)
										{
											this.Subtitle.UpdateLabel(SubtitleType.RivalEavesdropReaction, 0, 9.0f);

											this.Hesitation = .6f;
										}
										else if (this.StudentID == 10)
										{
											this.Subtitle.UpdateLabel(SubtitleType.RivalEavesdropReaction, 1, 9.0f);

											this.Hesitation = .6f;
										}
										else if (this.EventInterrupted)
										{
											this.Subtitle.UpdateLabel(SubtitleType.EventEavesdropReaction, 1, 5.0f);
											this.EventInterrupted = false;
										}
										else
										{
											this.Subtitle.UpdateLabel(SubtitleType.EavesdropReaction, 1, 5.0f);
										}
									}
									else if (this.Witnessed == StudentWitnessType.Pickpocketing)
									{
										this.Subtitle.UpdateLabel(this.PickpocketSubtitleType, 1, 5.0f);
									}
									else if (this.Witnessed == StudentWitnessType.Violence)
									{
										this.Subtitle.UpdateLabel(SubtitleType.ViolenceReaction, 5, 5.0f);
									}
									else if (this.Witnessed == StudentWitnessType.Poisoning)
									{
										if (this.Yandere.TargetBento.StudentID != this.StudentID)
										{
											this.Subtitle.UpdateLabel(SubtitleType.PoisonReaction, 1, 5.0f);
										}
										else
										{
											this.Subtitle.UpdateLabel(SubtitleType.PoisonReaction, 2, 5.0f);

											// This is called when a character sees Yandere-chan
											// poisoning their food, and refuses to eat it.

											this.Phase++;

											this.Pathfinding.target = this.Destinations[this.Phase];
											this.CurrentDestination = this.Destinations[this.Phase];
										}
									}
									else if (this.Witnessed == StudentWitnessType.SeveredLimb)
									{
										this.Subtitle.UpdateLabel(SubtitleType.LimbReaction, 0, 5.0f);
									}
									else if (this.Witnessed == StudentWitnessType.BloodyWeapon)
									{
										this.Subtitle.UpdateLabel(SubtitleType.BloodyWeaponReaction, 0, 5.0f);
									}
									else if (this.Witnessed == StudentWitnessType.DroppedWeapon)
									{
										this.Subtitle.UpdateLabel(SubtitleType.BloodyWeaponReaction, 0, 5.0f);
									}
									else if (this.Witnessed == StudentWitnessType.BloodPool)
									{
										this.Subtitle.UpdateLabel(SubtitleType.BloodPoolReaction, 0, 5.0f);
									}
									else if (this.Witnessed == StudentWitnessType.HoldingBloodyClothing)
									{
										this.Subtitle.UpdateLabel(SubtitleType.HoldingBloodyClothingReaction, 0, 5.0f);
									}
									else if (this.Witnessed == StudentWitnessType.Theft)
									{
                                        if (this.StudentID == 2 && this.RingReact)
                                        {
                                            this.Subtitle.UpdateLabel(SubtitleType.TheftReaction, 1, 5.0f);
                                        }
                                        else
                                        {
										    this.Subtitle.UpdateLabel(SubtitleType.TheftReaction, 0, 5.0f);
                                        }
                                    }
									else
									{
										//if (this.Club == ClubType.Council)
										//{
											this.Subtitle.UpdateLabel(SubtitleType.HmmReaction, 1, 3.0f);
										//}
										//else
										//{
											//this.Subtitle.UpdateLabel(SubtitleType.ParanoidReaction, 1, 3.0f);
										//}
									}
								}
								//If this is a delinquent
								else
								{
									if (this.Witnessed == StudentWitnessType.None)
									{
										this.Subtitle.Speaker = this;
										this.Subtitle.UpdateLabel(SubtitleType.DelinquentHmm, 0, 5.0f);
									}
									else if (this.Witnessed == StudentWitnessType.Corpse)
									{
										if (this.FoundEnemyCorpse)
										{
											this.Subtitle.UpdateLabel(SubtitleType.EvilDelinquentCorpseReaction, 1, 5.0f);
										}
										else if (this.Corpse.Student.Club == ClubType.Delinquent)
										{
											this.Subtitle.Speaker = this;
											this.Subtitle.UpdateLabel(SubtitleType.DelinquentFriendCorpseReaction, 1, 5.0f);
											this.FoundFriendCorpse = true;
										}
										else
										{
											this.Subtitle.Speaker = this;
											this.Subtitle.UpdateLabel(SubtitleType.DelinquentCorpseReaction, 1, 5.0f);
										}
									}
									else if (this.Witnessed == StudentWitnessType.Weapon && !this.Injured)
									{
										this.Subtitle.Speaker = this;
										this.Subtitle.UpdateLabel(SubtitleType.DelinquentWeaponReaction, 0, 3.0f);
									}
									else
									{
										this.Subtitle.Speaker = this;

										if (this.WitnessedLimb || this.WitnessedWeapon ||
											this.WitnessedBloodPool || this.WitnessedBloodyWeapon)
										{
											this.Subtitle.UpdateLabel(SubtitleType.LimbReaction, 0, 5.0f);
										}
										else
										{
											this.Subtitle.UpdateLabel(SubtitleType.DelinquentReaction, 0, 5.0f);

											Debug.Log("A delinquent is reacting to Yandere-chan's behavior.");
										}
									}
								}
							}
						}
					}
					//If it's Senpai...
					else
					{
						Debug.Log("We are now determining what Senpai saw...");

						if (this.Witnessed == StudentWitnessType.WeaponAndBloodAndInsanity)
						{
							// [af] Commented in JS code.
							//Subtitle.UpdateLabel("Senpai Insanity Reaction", 1, 4.5);

							this.CharacterAnimation.CrossFade(AnimNames.MaleSenpaiInsanityReaction);
							this.GameOverCause = GameOverType.Insanity;
						}
						else if (this.Witnessed == StudentWitnessType.WeaponAndBlood)
						{
							// [af] Commented in JS code.
							//Subtitle.UpdateLabel("Senpai Weapon Reaction", 1, 4.5);

							this.CharacterAnimation.CrossFade(AnimNames.MaleSenpaiWeaponReaction);
							this.GameOverCause = GameOverType.Weapon;
						}
						else if (this.Witnessed == StudentWitnessType.WeaponAndInsanity)
						{
							// [af] Commented in JS code.
							//Subtitle.UpdateLabel("Senpai Insanity Reaction", 1, 4.5);

							this.CharacterAnimation.CrossFade(AnimNames.MaleSenpaiInsanityReaction);
							this.GameOverCause = GameOverType.Insanity;
						}
						else if (this.Witnessed == StudentWitnessType.BloodAndInsanity)
						{
							// [af] Commented in JS code.
							//Subtitle.UpdateLabel("Senpai Insanity Reaction", 1, 4.5);

							this.CharacterAnimation.CrossFade(AnimNames.MaleSenpaiInsanityReaction);
							this.GameOverCause = GameOverType.Insanity;
						}
						else if (this.Witnessed == StudentWitnessType.Weapon)
						{
							// [af] Commented in JS code.
							//Subtitle.UpdateLabel("Senpai Weapon Reaction", 1, 4.5);

							this.CharacterAnimation.CrossFade(AnimNames.MaleSenpaiWeaponReaction);
							this.GameOverCause = GameOverType.Weapon;
						}
						else if (this.Witnessed == StudentWitnessType.Blood)
						{
							// [af] Commented in JS code.
							//Subtitle.UpdateLabel("Senpai Blood Reaction", 1, 4.5);

							this.CharacterAnimation.CrossFade(AnimNames.MaleSenpaiBloodReaction);
							this.GameOverCause = GameOverType.Blood;
						}
						else if (this.Witnessed == StudentWitnessType.Insanity)
						{
							// [af] Commented in JS code.
							//Subtitle.UpdateLabel("Senpai Insanity Reaction", 1, 4.5);

							this.CharacterAnimation.CrossFade(AnimNames.MaleSenpaiInsanityReaction);
							this.GameOverCause = GameOverType.Insanity;
						}
						else if (this.Witnessed == StudentWitnessType.Lewd)
						{
							this.CharacterAnimation.CrossFade(AnimNames.MaleSenpaiLewdReaction);
							this.GameOverCause = GameOverType.Lewd;
						}
						else if (this.Witnessed == StudentWitnessType.Stalking)
						{
							if (this.Concern < 5)
							{
								this.Subtitle.UpdateLabel(SubtitleType.SenpaiStalkingReaction, this.Concern, 4.50f);
							}
							else
							{
								this.CharacterAnimation.CrossFade(AnimNames.MaleSenpaiCreepyReaction);
								this.GameOverCause = GameOverType.Stalking;
							}

							this.Witnessed = StudentWitnessType.None;
						}
						else if (this.Witnessed == StudentWitnessType.Corpse)
						{
							if (this.Corpse.StudentID == this.StudentManager.RivalID)
							{
								this.Subtitle.Speaker = this;
								this.Subtitle.UpdateLabel(SubtitleType.SenpaiRivalDeathReaction, 1, 5.0f);

								Debug.Log("Senpai is reacting to Osana's corpse with a unique subtitle.");
							}
							else
							{
								this.Subtitle.UpdateLabel(SubtitleType.SenpaiCorpseReaction, 1, 5.0f);
							}
						}
						else if (this.Witnessed == StudentWitnessType.Violence)
						{
							this.CharacterAnimation.CrossFade(AnimNames.MaleSenpaiFightReaction);
							this.GameOverCause = GameOverType.Violence;
							this.Concern = 5;
						}

						if (this.Concern == 5)
						{
							this.CharacterAnimation[AnimNames.MaleScaredFace].weight = 0.0f;
							this.CharacterAnimation[this.AngryFaceAnim].weight = 0.0f;
							this.Yandere.ShoulderCamera.enabled = true;
							this.Yandere.ShoulderCamera.Noticed = true;
							this.Yandere.RPGCamera.enabled = false;
							this.Stop = true;//enabled = false;
						}
					}

					this.Reacted = true;
				}
			}

			//If we can pepper spray Yandere-chan...
			if (this.Club == ClubType.Council)
			{
				//If Yandere-chan is in our personal space...
				if (this.DistanceToPlayer < 1.1f)
				{
					//If the player is armed or transporting a corpse...
					if (this.Yandere.Armed == true || this.Yandere.Carrying || this.Yandere.Dragging)
					{
						//If we're actually within sight of the player
						if (this.Prompt.InSight)
						{
							this.Spray();
						}
					}
				}
			}
		}
		// If this character is "Threatened"...
		else
		{
			//Debug.Log(this.Name + " feels threatened.");

			this.Alarm -= Time.deltaTime * 100.0f * (1.0f / this.Paranoia);

			if (this.StudentManager.CombatMinigame.Delinquent == null || this.StudentManager.CombatMinigame.Delinquent == this)
			{
				this.targetRotation = Quaternion.LookRotation(new Vector3(
					this.Yandere.Hips.transform.position.x,
					this.transform.position.y,
					this.Yandere.Hips.transform.position.z) - this.transform.position);
			}
			else
			{
				this.targetRotation = Quaternion.LookRotation(new Vector3(
					this.StudentManager.CombatMinigame.Midpoint.position.x,
					this.transform.position.y,
					this.StudentManager.CombatMinigame.Midpoint.position.z) - this.transform.position);
			}

			this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);

			if (this.Yandere.DelinquentFighting && this.StudentManager.CombatMinigame.Delinquent != this)
			{
				if (this.StudentManager.CombatMinigame.Path < 4)
				{
					if (this.DistanceToPlayer < 1)
					{
						MyController.Move(this.transform.forward * Time.deltaTime * -1);
					}

					if (Vector3.Distance(this.transform.position, this.StudentManager.CombatMinigame.Delinquent.transform.position) < 1)
					{
						MyController.Move(this.transform.forward * Time.deltaTime * -1);
					}

					if (this.Yandere.enabled)
					{
						this.CheerTimer = Mathf.MoveTowards(this.CheerTimer, 0, Time.deltaTime);

						if (this.CheerTimer == 0)
						{
							this.Subtitle.Speaker = this;
							this.Subtitle.UpdateLabel(SubtitleType.DelinquentCheer, 0, 5.0f);
							this.CheerTimer = Random.Range(2.0f, 3.0f);
						}
					}

					this.CharacterAnimation.CrossFade(this.RandomCheerAnim);

					if (this.CharacterAnimation[this.RandomCheerAnim].time >= this.CharacterAnimation[this.RandomCheerAnim].length)
					{
						this.RandomCheerAnim = this.CheerAnims[Random.Range(0, this.CheerAnims.Length)];
					}

					//this.CharacterAnimation.CrossFade("delinquentCombatIdle_00"); //delinquentCheer_00
					this.ThreatPhase = 3;
					this.ThreatTimer = 0;

					if (this.WitnessedMurder)
					{
						this.Injured = true;
					}
				}
				else
				{
					this.CharacterAnimation.CrossFade(IdleAnim, 5);
					this.NoTalk = true;
				}
			}
			else if (!this.Injured)
			{
				if (this.DistanceToPlayer > 5 + this.ThreatDistance)
				{
					if (this.ThreatPhase < 4)
					{
						this.ThreatPhase = 3;
						this.ThreatTimer = 0;
					}
				}

				if (!this.Yandere.Shoved && !this.Yandere.Dumping)
				{
					if (this.DistanceToPlayer > 1 && this.Patience > 0)
					{
						if (this.ThreatPhase == 1)
						{
							this.CharacterAnimation.CrossFade("delinquentShock_00");

							if (this.CharacterAnimation["delinquentShock_00"].time >= this.CharacterAnimation["delinquentShock_00"].length)
							{
								this.Subtitle.Speaker = this;
								this.Subtitle.UpdateLabel(SubtitleType.DelinquentThreatened, 0, 3.0f);
								this.CharacterAnimation.CrossFade("delinquentCombatIdle_00"); //delinquentThreatened_00
								this.ThreatTimer = 5;
								this.ThreatPhase++;
							}
						}
						else if (this.ThreatPhase == 2)
						{
							this.ThreatTimer -= Time.deltaTime;

							if (this.ThreatTimer < 0)
							{
								this.Subtitle.Speaker = this;
								this.Subtitle.UpdateLabel(SubtitleType.DelinquentTaunt, 0, 5.0f);
								this.ThreatTimer = 5;
								this.ThreatPhase++;
							}
						}
						else if (this.ThreatPhase == 3)
						{
							this.ThreatTimer -= Time.deltaTime;

							if (this.ThreatTimer < 0)
							{
								if (!this.NoTalk)
								{
									this.Subtitle.Speaker = this;
									this.Subtitle.UpdateLabel(SubtitleType.DelinquentCalm, 0, 5.0f);
								}

								this.CharacterAnimation.CrossFade(IdleAnim, 5);
								this.ThreatTimer = 5;
								this.ThreatPhase++;
								//this.NoTalk = false;
							}
						}
						else if (this.ThreatPhase == 4)
						{
							this.ThreatTimer -= Time.deltaTime;

							if (this.ThreatTimer < 0)
							{
								if (this.CurrentDestination != this.Destinations[this.Phase])
								{
									this.StopInvestigating();
								}

								this.Distracted = false;
								this.Threatened = false;
								this.Alarmed = false;
								this.Routine = true;
								this.NoTalk = false;

								this.IgnoreTimer = 5;
								this.AlarmTimer = 0;
							}
						}
					}
					else if (!this.NoTalk)
					{
						//Debug.Log("Combat is beginning.");

						string Prefix = "";

						if (!this.Male)
						{
							Prefix = "Female_";
						}

						if (this.StudentID == 46)
						{
							this.CharacterAnimation.CrossFade("delinquentDrawWeapon_00");
						}

						if (this.StudentManager.CombatMinigame.Delinquent == null)
						{
							this.Yandere.CharacterAnimation.CrossFade("Yandere_CombatIdle");

							if (this.MyWeapon.transform.parent != this.ItemParent)
							{
								Debug.Log("This character should be drawing a weapon.");

								this.CharacterAnimation.CrossFade(Prefix + "delinquentDrawWeapon_00");
							}
							else
							{
								this.CharacterAnimation.CrossFade("delinquentCombatIdle_00");
							}

							if (this.Yandere.Carrying || this.Yandere.Dragging)
							{
								this.Yandere.EmptyHands();
							}

							if (this.Yandere.Pickpocketing)
							{
								this.Yandere.Caught = true;
								this.PickPocket.PickpocketMinigame.End();
							}

							this.Yandere.StopLaughing();
							this.Yandere.StopAiming();
							this.Yandere.Unequip();

							if (this.Yandere.PickUp != null)
							{
								this.Yandere.EmptyHands();
							}

							this.Yandere.DelinquentFighting = true;
							this.Yandere.NearSenpai = false;
							this.Yandere.Degloving = false;
							this.Yandere.CanMove = false;
							this.Yandere.GloveTimer = 0;

							this.Distracted = true;
							this.Fighting = true;
							this.ThreatTimer = 0;

							this.StudentManager.CombatMinigame.Delinquent = this;
							this.StudentManager.CombatMinigame.enabled = true;
							this.StudentManager.CombatMinigame.StartCombat();

							this.SpeechLines.Stop();

							this.SpawnAlarmDisc();

							if (this.WitnessedMurder || this.WitnessedCorpse)
							{
								this.Subtitle.Speaker = this;
								this.Subtitle.UpdateLabel(SubtitleType.DelinquentAvenge, 0, 5.0f);
							}
							else
							{
								if (!this.StudentManager.CombatMinigame.Practice)
								{
									this.Subtitle.Speaker = this;
									this.Subtitle.UpdateLabel(SubtitleType.DelinquentFight, 0, 5.0f);
								}
							}
						}

						this.Yandere.transform.rotation = Quaternion.LookRotation(new Vector3(
						this.Hips.transform.position.x,
						this.Yandere.transform.position.y,
						this.Hips.transform.position.z)
						- this.Yandere.transform.position);

						if (this.CharacterAnimation[Prefix + "delinquentDrawWeapon_00"].time >= .5f)
						{
							this.MyWeapon.transform.parent = this.ItemParent;
							this.MyWeapon.transform.localEulerAngles = new Vector3(0, 15, 0);
							this.MyWeapon.transform.localPosition = new Vector3(0.01f, 0, 0);
						}

						if (this.CharacterAnimation[Prefix + "delinquentDrawWeapon_00"].time >= this.CharacterAnimation[Prefix + "delinquentDrawWeapon_00"].length)
						{
							//Debug.Log("The delinquent reached the end of his ''draw weapon'' animation.");

							this.Threatened = false;
							this.Alarmed = false;
							this.enabled = false;
						}
					}
					else
					{
						this.ThreatTimer -= Time.deltaTime;

						if (this.ThreatTimer < 0)
						{
							if (this.CurrentDestination != this.Destinations[this.Phase])
							{
								this.StopInvestigating();
							}

							this.Distracted = false;
							this.Threatened = false;
							this.Alarmed = false;
							this.Routine = true;
							this.NoTalk = false;

							this.IgnoreTimer = 5;
							this.AlarmTimer = 0;
						}
					}
				}
			}
			else
			{
				this.ThreatTimer += Time.deltaTime;

				if (this.ThreatTimer > 5)
				{
					this.DistanceToDestination = 100;

                    if (this.Yandere.CanMove)
                    {
					    if (!this.WitnessedMurder && !this.WitnessedCorpse)
					    {
						    this.Distracted = false;
						    this.Threatened = false;
						    this.EndAlarm();
					    }
					    else
					    {
						    this.Threatened = false;
						    this.Alarmed = false;
						    this.Injured = false;
						    this.PersonaReaction();
					    }
                    }
                }
			}
		}
	}

	void UpdateBurning()
	{
		if (this.DistanceToPlayer < 1)
		{
			if (!this.Yandere.Shoved && !this.Yandere.Egg)
			{
				this.PushYandereAway();
			}
		}
		if (this.BurnTarget != Vector3.zero)
		{
			this.MoveTowardsTarget(this.BurnTarget);
		}

		if (this.CharacterAnimation[this.BurningAnim].time >
			this.CharacterAnimation[this.BurningAnim].length)
		{
			this.DeathType = DeathType.Burning;
			this.BecomeRagdoll();
		}
	}

	void UpdateSplashed()
	{
		this.CharacterAnimation.CrossFade(this.SplashedAnim);

		if (this.Yandere.Tripping)
		{
			this.targetRotation = Quaternion.LookRotation(new Vector3(
				this.Yandere.Hips.transform.position.x,
				this.transform.position.y,
				this.Yandere.Hips.transform.position.z) - this.transform.position);
			this.transform.rotation = Quaternion.Slerp(
				this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);
		}

		this.SplashTimer += Time.deltaTime;

		if ((this.SplashTimer > 2.0f) && (this.SplashPhase == 1))
		{
			if (this.Gas)
			{
				this.Subtitle.Speaker = this;
				this.Subtitle.UpdateLabel(this.SplashSubtitleType, 5, 5.0f);
			}
			else if (this.Bloody)
			{
				this.Subtitle.Speaker = this;
				this.Subtitle.UpdateLabel(this.SplashSubtitleType, 3, 5.0f);
			}
			else if (this.Yandere.Tripping)
			{
				this.Subtitle.Speaker = this;
				this.Subtitle.UpdateLabel(this.SplashSubtitleType, 7, 5.0f);
			}
			else
			{
				this.Subtitle.Speaker = this;
				this.Subtitle.UpdateLabel(this.SplashSubtitleType, 1, 5.0f);
			}

			this.CharacterAnimation[this.SplashedAnim].speed = 0.50f;

			this.SplashPhase++;
		}

		if ((this.SplashTimer > 12.0f) && (this.SplashPhase == 2))
		{
			if (this.LightSwitch == null)
			{
				if (this.Gas)
				{
					this.Subtitle.Speaker = this;
					this.Subtitle.UpdateLabel(this.SplashSubtitleType, 6, 5.0f);
				}
				else if (this.Bloody)
				{	
					this.Subtitle.Speaker = this;
					this.Subtitle.UpdateLabel(this.SplashSubtitleType, 4, 5.0f);
				}
				else
				{
					this.Subtitle.Speaker = this;
					this.Subtitle.UpdateLabel(this.SplashSubtitleType, 2, 5.0f);
				}

				this.SplashPhase++;

				if (!this.Male)
				{
					this.CurrentDestination = this.StudentManager.StrippingPositions[this.GirlID];
					this.Pathfinding.target = this.StudentManager.StrippingPositions[this.GirlID];
				}
				else
				{
					this.CurrentDestination = this.StudentManager.MaleStripSpot;
					this.Pathfinding.target = this.StudentManager.MaleStripSpot;
				}
			}
			else
			{
				if (!this.LightSwitch.BathroomLight.activeInHierarchy)
				{
					if (this.LightSwitch.Panel.useGravity)
					{
						this.LightSwitch.Prompt.Hide();
						this.LightSwitch.Prompt.enabled = false;

						this.Prompt.Hide();
						this.Prompt.enabled = false;
					}

					this.Subtitle.UpdateLabel(SubtitleType.LightSwitchReaction, 1, 5.0f);

					this.CurrentDestination = this.LightSwitch.ElectrocutionSpot;
					this.Pathfinding.target = this.LightSwitch.ElectrocutionSpot;

					this.Pathfinding.speed = 1.0f;
					this.BathePhase = -1;

					this.InDarkness = true;
				}
				else
				{
					if (!this.Bloody)
					{
						this.Subtitle.Speaker = this;
						this.Subtitle.UpdateLabel(this.SplashSubtitleType, 2, 5.0f);
					}
					else
					{
						this.Subtitle.Speaker = this;
						this.Subtitle.UpdateLabel(this.SplashSubtitleType, 4, 5.0f);
					}

					this.SplashPhase++;

					this.CurrentDestination = this.StudentManager.StrippingPositions[this.GirlID];
					this.Pathfinding.target = this.StudentManager.StrippingPositions[this.GirlID];
				}
			}

            Debug.Log("Student is now running towards the locker room.");

            this.CharacterAnimation[this.WetAnim].weight = 1.0f;

            this.Pathfinding.canSearch = true;
			this.Pathfinding.canMove = true;

			this.Splashed = false;
		}
	}

	void UpdateTurningOffRadio()
	{
		if (this.Radio.On || this.RadioPhase == 3 && this.Radio.transform.parent == null)
		{
			if (this.RadioPhase == 1)
			{
				this.targetRotation = Quaternion.LookRotation(new Vector3(
					this.Radio.transform.position.x,
					this.transform.position.y,
					this.Radio.transform.position.z) - this.transform.position);
				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);

				this.RadioTimer += Time.deltaTime;

				if (this.RadioTimer > 3.0f)
				{
					if (this.Persona == PersonaType.PhoneAddict && !this.Phoneless)
					{
						this.SmartPhone.SetActive(true);
					}

					this.CharacterAnimation.CrossFade(this.WalkAnim);
					this.CurrentDestination = this.Radio.transform;
					this.Pathfinding.target = this.Radio.transform;
					this.Pathfinding.canSearch = true;
					this.Pathfinding.canMove = true;
					this.RadioTimer = 0.0f;
					this.RadioPhase++;
				}
			}
			else if (this.RadioPhase == 2)
			{
				if (this.DistanceToDestination < 0.50f)
				{
					this.CharacterAnimation.CrossFade(this.RadioAnim);
					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;
					this.SmartPhone.SetActive(false);
					this.RadioPhase++;
				}
			}
			else if (this.RadioPhase == 3)
			{
				this.targetRotation = Quaternion.LookRotation(new Vector3(
					this.Radio.transform.position.x,
					this.transform.position.y,
					this.Radio.transform.position.z) - this.transform.position);
				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);

				this.RadioTimer += Time.deltaTime;

				if (this.RadioTimer > 4.0f)
				{
					if (this.Persona == PersonaType.PhoneAddict && !this.Phoneless)
					{
						this.SmartPhone.SetActive(true);
					}

					//This is only called after a student finishes turning off a radio.

					this.CurrentDestination = this.Destinations[this.Phase];
					this.Pathfinding.target = this.Destinations[this.Phase];

					this.Pathfinding.canSearch = true;
					this.Pathfinding.canMove = true;

					this.ForgetRadio();
				}
				else if (this.RadioTimer > 2.0f)
				{
					this.Radio.Victim = null;
					this.Radio.TurnOff();
				}
			}
		}
		else
		{
			if (this.RadioPhase < 100)
			{
				this.CharacterAnimation.CrossFade(this.IdleAnim);
				this.Pathfinding.canSearch = false;
				this.Pathfinding.canMove = false;

				this.RadioPhase = 100;
				this.RadioTimer = 0.0f;
			}

			this.targetRotation = Quaternion.LookRotation(new Vector3(
				this.Radio.transform.position.x,
				this.transform.position.y,
				this.Radio.transform.position.z) - this.transform.position);

			this.RadioTimer += Time.deltaTime;

			if (this.RadioTimer > 1.0f || this.Radio.transform.parent != null)
			{
				//This is only called if a radio turns off while a student is walking to turn it off.

				this.CurrentDestination = this.Destinations[this.Phase];
				this.Pathfinding.target = this.Destinations[this.Phase];

				this.Pathfinding.canSearch = true;
				this.Pathfinding.canMove = true;

				this.ForgetRadio();
			}
		}
	}

	void UpdateVomiting()
	{
		if ((this.VomitPhase != 0) && (this.VomitPhase != 4))
		{
			this.transform.rotation = Quaternion.Slerp(
				this.transform.rotation, this.CurrentDestination.rotation, Time.deltaTime * 10.0f);
			this.MoveTowardsTarget(this.CurrentDestination.position);
		}

		if (this.VomitPhase == 0)
		{
			if (this.DistanceToDestination < 0.50f)
			{
				Debug.Log("Character is now drownable.");

				//Osana or Obstacle
				//if (this.StudentID == 11 || this.StudentID == 6)
				//{
					this.Prompt.Label[0].text = "     " + "Drown";
					this.Prompt.HideButton[0] = false;
					this.Prompt.enabled = true;
					this.Drownable = true;
				//}

				if (this.VomitDoor != null)
				{
					this.VomitDoor.Prompt.enabled = false;
					this.VomitDoor.Prompt.Hide();
					this.VomitDoor.enabled = false;
				}

				this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
				this.CharacterAnimation.CrossFade(this.VomitAnim);
				this.Pathfinding.canSearch = false;
				this.Pathfinding.canMove = false;
				this.VomitPhase++;
			}
		}
		else if (this.VomitPhase == 1)
		{
			if (this.CharacterAnimation[this.VomitAnim].time > 1.0f)
			{
				this.VomitEmitter.gameObject.SetActive(true);
				this.VomitPhase++;
			}
		}
		else if (this.VomitPhase == 2)
		{
			if (this.CharacterAnimation[this.VomitAnim].time > 13.0f)
			{
				this.VomitEmitter.gameObject.SetActive(false);
				this.VomitPhase++;
			}
		}
		else if (this.VomitPhase == 3)
		{
			if (this.CharacterAnimation[this.VomitAnim].time >=
				this.CharacterAnimation[this.VomitAnim].length)
			{
				this.Prompt.Label[0].text = "     " + "Talk";
				this.Drownable = false;

				this.WalkAnim = this.OriginalWalkAnim;
				this.CharacterAnimation.CrossFade(this.WalkAnim);

				if (this.Male)
				{
					this.StudentManager.GetMaleWashSpot(this);

					this.Pathfinding.target = this.StudentManager.MaleWashSpot;
					this.CurrentDestination = this.StudentManager.MaleWashSpot;
				}
				else
				{
					this.StudentManager.GetFemaleWashSpot(this);

					this.Pathfinding.target = this.StudentManager.FemaleWashSpot;
					this.CurrentDestination = this.StudentManager.FemaleWashSpot;
				}

				if (this.VomitDoor != null)
				{
					this.VomitDoor.Prompt.enabled = true;
					this.VomitDoor.enabled = true;
				}

				this.Pathfinding.canSearch = true;
				this.Pathfinding.canMove = true;
				this.Pathfinding.speed = 1.0f;

				this.DistanceToDestination = 100.0f;
				this.VomitPhase++;
			}
		}
		else if (this.VomitPhase == 4)
		{
			if (this.DistanceToDestination < 0.50f)
			{
				this.CharacterAnimation.CrossFade(this.WashFaceAnim);
				this.Pathfinding.canSearch = false;
				this.Pathfinding.canMove = false;
				this.VomitPhase++;
			}
		}
		else if (this.VomitPhase == 5)
		{
			if (this.CharacterAnimation[this.WashFaceAnim].time >
				this.CharacterAnimation[this.WashFaceAnim].length)
			{
				this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
				this.Prompt.Label[0].text = "     " + "Talk";
				this.Pathfinding.canSearch = true;
				this.Pathfinding.canMove = true;
				this.Distracted = false;
				this.Drownable = false;
				this.Vomiting = false;
				this.Private = false;
				this.CanTalk = true;
				this.Routine = true;
				this.Emetic = false;
				this.VomitPhase = 0;

				this.WalkAnim = this.OriginalWalkAnim;

				//This is only called after a student has finished vomiting.

				this.Phase++;

				this.Pathfinding.target = this.Destinations[this.Phase];
				this.CurrentDestination = this.Destinations[this.Phase];
				this.DistanceToDestination = 100.0f;
			}
		}
	}

	void UpdateConfessing()
	{
		if (!this.Male)
		{
			if (this.ConfessPhase == 1)
			{
				if (this.DistanceToDestination < 0.50f)
				{
					this.Cosmetic.MyRenderer.materials[2].SetFloat("_BlendAmount", 1.0f);

					this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;

					this.CharacterAnimation.CrossFade(AnimNames.FemaleInsertNote);

					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;

					this.Note.SetActive(true);

					this.ConfessPhase++;
				}
			}
			else if (this.ConfessPhase == 2)
			{
				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.CurrentDestination.rotation, Time.deltaTime * 10.0f);
				this.MoveTowardsTarget(this.CurrentDestination.position);

				if (this.CharacterAnimation[AnimNames.FemaleInsertNote].time >= 9.0f)
				{
					this.Note.SetActive(false);
					this.ConfessPhase++;
				}
			}
			else if (this.ConfessPhase == 3)
			{
				if (this.CharacterAnimation[AnimNames.FemaleInsertNote].time >=
					this.CharacterAnimation[AnimNames.FemaleInsertNote].length)
				{
					//Debug.Log("Sprinting 15");

					this.CurrentDestination = this.StudentManager.RivalConfessionSpot;
					this.Pathfinding.target = this.StudentManager.RivalConfessionSpot;
					this.Pathfinding.canSearch = true;
					this.Pathfinding.canMove = true;
					this.Pathfinding.speed = 4.0f;

					this.StudentManager.LoveManager.LeftNote = true;
					//Debug.Log("Sprinting 8");
					this.CharacterAnimation.CrossFade(this.SprintAnim);
					this.ConfessPhase++;
				}
			}
			else if (this.ConfessPhase == 4)
			{
				if (this.DistanceToDestination < 0.50f)
				{
					this.CharacterAnimation.CrossFade(this.IdleAnim);
					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;
					this.ConfessPhase++;
				}
			}
			else if (this.ConfessPhase == 5)
			{
				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.CurrentDestination.rotation, Time.deltaTime * 10.0f);
				
				this.CharacterAnimation[this.ShyAnim].weight = Mathf.Lerp(
					this.CharacterAnimation[this.ShyAnim].weight, 1.0f, Time.deltaTime);
				
				this.MoveTowardsTarget(this.CurrentDestination.position);
			}
		}
		else
		{
			if (this.ConfessPhase == 1)
			{
				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.CurrentDestination.rotation, Time.deltaTime * 10.0f);
				this.MoveTowardsTarget(this.CurrentDestination.position);

				if (this.CharacterAnimation[AnimNames.MaleKeepNote].time > 14)
				{
					this.Note.SetActive(false);
				}
				else if (this.CharacterAnimation[AnimNames.MaleKeepNote].time > 4.5)
				{
					this.Note.SetActive(true);
				}

				if (this.CharacterAnimation[AnimNames.MaleKeepNote].time >=
					this.CharacterAnimation[AnimNames.MaleKeepNote].length)
				{
					//Debug.Log("Sprinting 16");

					this.CurrentDestination = this.StudentManager.SuitorConfessionSpot;
					this.Pathfinding.target = this.StudentManager.SuitorConfessionSpot;
					this.Pathfinding.canSearch = true;
					this.Pathfinding.canMove = true;
					this.Pathfinding.speed = 4.0f;

					//Debug.Log("Sprinting 9");
					this.CharacterAnimation.CrossFade(this.SprintAnim);
					this.ConfessPhase++;
				}
			}
			else if (this.ConfessPhase == 2)
			{
				if (this.DistanceToDestination < 0.50f)
				{
					this.CharacterAnimation.CrossFade(AnimNames.MaleExhausted);

					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;

					this.ConfessPhase++;
				}
			}
			else if (this.ConfessPhase == 3)
			{
				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.CurrentDestination.rotation, Time.deltaTime * 10.0f);
				
				this.MoveTowardsTarget(this.CurrentDestination.position);
			}
		}
	}

	void UpdateMisc()
	{
		if (this.IgnoreTimer > 0.0f)
		{
			this.IgnoreTimer = Mathf.MoveTowards(this.IgnoreTimer, 0, Time.deltaTime);
		}

		//If we're not in the "Fleeing" protocol...
		if (!this.Fleeing)
		{
			//If we're currently past the school's exit...
			if (this.transform.position.z < -100.0f)
			{
				//If we've fallen through the ground...
				if (this.transform.position.y < -11.0f)
				{
					//If we're not Senpai...
					if (this.StudentID > 1)
					{
						Destroy(this.gameObject);
					}
				}
			}
			//If we're not past the school's exit...
			else
			{
				//If we're underground...
				if (this.transform.position.y < -0.1f)
				{
					//Pop back up above the ground.
					this.transform.position = new Vector3(
						this.transform.position.x,
						0.0f,
						this.transform.position.z);
				}
							
				//If we're not in circumstances that would prevent us from caring about Yandere-chan...
				if (!this.Dying && !this.Distracted && !this.WalkBack && !this.Waiting && !this.Guarding &&
					!this.WitnessedMurder && !this.WitnessedCorpse && !this.Blind && !this.SentHome &&
					!this.TurnOffRadio && !this.Wet && !this.InvestigatingBloodPool && !this.ReturningMisplacedWeapon &&
					!this.Yandere.Egg && !this.StudentManager.Pose && !this.ShoeRemoval.enabled && !this.Drownable)
					//&& this.AlarmTimer == 0)
				{
					//Debug.Log("Yandere-chan's distance is: " + this.DistanceToPlayer);

					if (this.StudentManager.MissionMode)
					{
						//If Yandere-chan is in our personal space...
						if (this.DistanceToPlayer < .5)
						{
							Debug.Log ("This student cannot be interacted with right now.");

							this.Yandere.Shutter.FaceStudent = this;
							this.Yandere.Shutter.Penalize();
						}
					}

					//If we are a member of the student council...
					if (this.Club == ClubType.Council)
					{
                        //If we are not currently witnessing something...
                        if (!this.WitnessedSomething)
                        {
						    //If Yandere-chan is nearby...
						    if (this.DistanceToPlayer < 5)
						    {
							    if (this.DistanceToPlayer < 2)
							    {
								    this.StudentManager.TutorialWindow.ShowCouncilMessage = true;
							    }

							    float Angle = Vector3.Angle(-this.transform.forward,
								    this.Yandere.transform.position - this.transform.position);

							    //If (Yandere-chan is behind us...
							    if (Mathf.Abs(Angle) <= 45.0f)
							    {
                                    //If Yandere-chan is not crouching or crawling,
                                    //and we haven't witnessed anything distracting...
								    if ((this.Yandere.Stance.Current != StanceType.Crouching) &&
									    (this.Yandere.Stance.Current != StanceType.Crawling))
								    {
									    //If Yandere-chan is actively moving...
									    if (this.Yandere.h != 0 || this.Yandere.v != 0)
									    {
										    //If Yandere-chan is running or walking very close by...
										    if (this.Yandere.Running || this.DistanceToPlayer < 2)
										    {
											    this.DistractionSpot = this.Yandere.transform.position;
											    this.Alarm = 100 + (Time.deltaTime * 100.0f * (1.0f / this.Paranoia));
											    this.FocusOnYandere = true;

											    this.Pathfinding.canSearch = false;
											    this.Pathfinding.canMove = false;

											    this.StopInvestigating();
										    }
									    }
								    }
							    }
						    }

						    //If Yandere-chan is in our personal space...
						    if (this.DistanceToPlayer < 1.1f)
						    {
							    float Angle = Vector3.Angle(-this.transform.forward,
								    this.Yandere.transform.position - this.transform.position);

							    //If (Yandere-chan is front of us...
							    if (Mathf.Abs(Angle) > 45.0f)
							    {
								    //If the player is armed or transporting a corpse...
								    if (this.Yandere.Armed == true || this.Yandere.Carrying || this.Yandere.Dragging)
								    {
									    //If we're actually within sight of the player
									    if (this.Prompt.InSight)
									    {
										    this.Spray();
									    }
								    }
							    }
						    }
                        }
                    }

					//If we can shove Yandere-chan...
					if (this.Club == ClubType.Council && !this.Spraying ||
						this.Club == ClubType.Delinquent && !this.Injured && !this.RespectEarned &&
						!this.Vomiting && !this.Emetic && !this.Headache && !this.Sedated && !this.Lethal)
					{
						//If Yandere-chan is in our personal space...
						if (this.DistanceToPlayer < .5 && this.Yandere.CanMove)
						{
							//If Yandere-chan is actively moving...
							if (this.Yandere.h != 0 || this.Yandere.v != 0)
							{
								//Debug.Log("I am under the impression that Yandere-chan is actively moving, and I am going to shove her now.");

								if (this.Club == ClubType.Delinquent)
								{
									this.Subtitle.Speaker = this;
									this.Subtitle.UpdateLabel(SubtitleType.DelinquentShove, 0, 3.0f);
								}

								this.Shove();
							}
						}
					}
				}
			}
		}
		else
		{
			//If we are a student council member
			if (this.Club == ClubType.Council)
			{
				//If Yandere-chan is in our personal space...
				if (this.DistanceToPlayer < 1.1f)
				{
					float Angle = Vector3.Angle(-this.transform.forward,
						this.Yandere.transform.position - this.transform.position);

					//If (Yandere-chan is front of us...
					if (Mathf.Abs(Angle) > 45.0f)
					{
						//If the player is armed or transporting a corpse...
						if (this.Yandere.Armed == true || this.Yandere.Carrying || this.Yandere.Dragging)
						{
							//If we're actually within sight of the player
							if (this.Prompt.InSight)
							{
								this.Spray();
							}
						}
					}
				}
			}
		}

		/*
		if (this.Pathfinding.canMove)
		{
			if (this.transform.position == this.LastPosition)
			{
				this.StuckTimer += Time.deltaTime;

				if (this.StuckTimer > 1.0f)
				{
					//Rigidbody Test
					//MyRigidbody.MovePosition(transform.position + transform.right * Time.timeScale * .0001);

					this.MyController.Move(this.transform.right * (Time.timeScale * 0.0001f));
					this.StuckTimer = 0.0f;
				}
			}
		}
		*/

		//this.LastPosition = this.transform.position;
	}

	public Transform DefaultTarget;
	public Transform GushTarget;
	public bool Gush = false;

	public float LookSpeed = 2.0f;

	// [af] Unused JS variables.
	//var ShrinkHead = false;
	//var HeadSize = 1.0;

	public float TimeOfDeath = 0;
	public int Fate = 0;

	void LateUpdate()
	{
		/*
		this.CharacterAnimation.enabled =
			(this.CharacterAnimation.cullingType != AnimationCullingType.BasedOnRenderers) ||
			!this.StudentManager.DisableFarAnims || (this.DistanceToPlayer <= 15.0f);
		*/

		if (this.StudentManager.DisableFarAnims && this.DistanceToPlayer >= this.StudentManager.FarAnimThreshold &&
			CharacterAnimation.cullingType != AnimationCullingType.AlwaysAnimate && !this.WitnessCamera.Show)
		{
			this.CharacterAnimation.enabled = false;
		}
		else
		{
			this.CharacterAnimation.enabled = true;
		}

		// [af] Commented in JS code.
		//transform.eulerAngles = Vector3(0, transform.eulerAngles.y, 0);

		/*
		if (this.StudentID == 39)
		{
			Debug.Log("EyeShrink is " + this.EyeShrink + " and PreviousEyeShrink is " + this.PreviousEyeShrink);
		}
		*/

		if (this.EyeShrink > 0)
		{
			if (this.EyeShrink > 1.0f)
			{
				this.EyeShrink = 1.0f;
			}

			this.LeftEye.localPosition = new Vector3(
				this.LeftEye.localPosition.x,
				this.LeftEye.localPosition.y,
				this.LeftEyeOrigin.z - (this.EyeShrink * 0.010f));

			this.RightEye.localPosition = new Vector3(
				this.RightEye.localPosition.x,
				this.RightEye.localPosition.y,
				this.RightEyeOrigin.z + (this.EyeShrink * 0.010f));

			this.LeftEye.localScale = new Vector3(
				1.0f - (this.EyeShrink * 0.50f),
				1.0f - (this.EyeShrink * 0.50f),
				this.LeftEye.localScale.z);

			this.RightEye.localScale = new Vector3(
				1.0f - (this.EyeShrink * 0.50f),
				1.0f - (this.EyeShrink * 0.50f),
				this.RightEye.localScale.z);

			this.PreviousEyeShrink = this.EyeShrink;
		}

		if (!this.Male)
		{
			if (this.Shy)
			{
				if (this.Routine)
				{
					if ((this.Phase == 2) && (this.DistanceToDestination < 1.0f) ||
						(this.Phase == 4) && (this.DistanceToDestination < 1.0f) ||
						(this.Actions[this.Phase] == StudentActionType.SitAndTakeNotes) && (this.DistanceToDestination < 1.0f) ||
						(this.Actions[this.Phase] == StudentActionType.Clean) && (this.DistanceToDestination < 1.0f)  ||
						(this.Actions[this.Phase] == StudentActionType.Read) && (this.DistanceToDestination < 1.0f))
					{
						this.CharacterAnimation[this.ShyAnim].weight = Mathf.Lerp(
							this.CharacterAnimation[this.ShyAnim].weight, 0.0f, Time.deltaTime);
					}
					else
					{
						this.CharacterAnimation[this.ShyAnim].weight = Mathf.Lerp(
							this.CharacterAnimation[this.ShyAnim].weight, 1.0f, Time.deltaTime);
					}
				}
				else
				{
					if (!this.Headache)
					{
						this.CharacterAnimation[this.ShyAnim].weight = Mathf.Lerp(
							this.CharacterAnimation[this.ShyAnim].weight, 0.0f, Time.deltaTime);
					}
				}
			}

			if (!this.BoobsResized)
			{
				this.RightBreast.localScale = new Vector3(this.BreastSize, this.BreastSize, this.BreastSize);
				this.LeftBreast.localScale = new Vector3(this.BreastSize, this.BreastSize, this.BreastSize);

				if (!this.Cosmetic.CustomEyes)
				{
					this.RightBreast.gameObject.name = "RightBreastRENAMED";
					this.LeftBreast.gameObject.name = "LeftBreastRENAMED";
				}

				this.BoobsResized = true;
			}

			if (this.Following)
			{
				if (this.Gush)
				{
					// [af] Commented in JS code.
					//StudentManager.FollowerLookAtTarget.position = Vector3.Lerp(StudentManager.FollowerLookAtTarget.position, GushTarget.position, Time.deltaTime * LookSpeed);

					this.Neck.LookAt(this.GushTarget);

					//this.targetRotation = Quaternion.LookRotation(this.GushTarget.position - this.Neck.position);
				}
				else
				{
					// [af] Commented in JS code.
					//StudentManager.FollowerLookAtTarget.position = Vector3.Lerp(StudentManager.FollowerLookAtTarget.position, new Vector3(DefaultTarget.position.x, Neck.transform.position.y, DefaultTarget.position.z), Time.deltaTime * LookSpeed);

					this.Neck.LookAt(this.DefaultTarget);

					//this.targetRotation = Quaternion.LookRotation(this.DefaultTarget.position - this.Neck.position);
				}

				// [af] Commented in JS code.
				//Neck.LookAt(StudentManager.FollowerLookAtTarget);

				//this.Neck.rotation = Quaternion.Slerp(this.Neck.rotation, this.targetRotation, 10.0f * Time.deltaTime);
			}

			if (this.StudentManager.Egg)
			{
				if (this.SecurityCamera.activeInHierarchy)
				{
					this.Head.localScale = new Vector3(0, 0, 0);
				}
			}

			if (this.Club == ClubType.Bully)
			{
				for (int i = 0; i < 4; i++)
				{
					if (this.Skirt[i] != null)
					{
						Transform skirtTransform = this.Skirt[i].transform;
						skirtTransform.localScale = new Vector3(
							skirtTransform.localScale.x,
							2.0f / 3.0f,
							skirtTransform.localScale.z);
					}
				}
			}

			if (this.LongHair[0] != null)
			{
                if (this.MyBento.gameObject.activeInHierarchy && this.MyBento.transform.parent != null)
                {
                    //Debug.Log("Bento is active, Bento's parent is not null.");

				    LongHair[0].eulerAngles = new Vector3(
					    Spine.eulerAngles.x,
					    Spine.eulerAngles.y,
					    Spine.eulerAngles.z);

				    LongHair[0].RotateAround(LongHair[0].position, transform.right, 180f);

                    if (LongHair[1] != null)
                    {
				        LongHair[1].eulerAngles = new Vector3(
					        Spine.eulerAngles.x,
					        Spine.eulerAngles.y,
					        Spine.eulerAngles.z);

				        LongHair[1].RotateAround(LongHair[1].position, transform.right, 180f);
                    }
                }
            }
		}

		if (this.Routine && !this.InEvent && !this.Meeting && !this.GoAway)
		{
			if (this.DistanceToDestination < this.TargetDistance &&
				this.Actions[this.Phase] == StudentActionType.SitAndSocialize ||
				this.DistanceToDestination < this.TargetDistance && this.StudentID != 36 &&
				this.Actions[this.Phase] == StudentActionType.Meeting ||
				this.DistanceToDestination < this.TargetDistance &&
				this.Actions[this.Phase] == StudentActionType.Sleuth &&
				this.StudentManager.SleuthPhase != 2 && this.StudentManager.SleuthPhase != 3 ||
				this.Club == ClubType.Photography && this.ClubActivity)
			{
				if (this.CharacterAnimation[this.SocialSitAnim].weight != 1)
				{
					this.CharacterAnimation[this.SocialSitAnim].weight = Mathf.Lerp(
						this.CharacterAnimation[this.SocialSitAnim].weight, 1.0f, Time.deltaTime * 10.0f);

					if (this.CharacterAnimation[this.SocialSitAnim].weight > .99)
					{
						this.CharacterAnimation[this.SocialSitAnim].weight = 1;
					}
				}
			}
			else
			{
				if (this.CharacterAnimation[this.SocialSitAnim].weight != 0)
				{
					this.CharacterAnimation[this.SocialSitAnim].weight = Mathf.Lerp(
						this.CharacterAnimation[this.SocialSitAnim].weight, 0.0f, Time.deltaTime * 10.0f);

					if (this.CharacterAnimation[this.SocialSitAnim].weight < .01)
					{
						this.CharacterAnimation[this.SocialSitAnim].weight = 0;
					}
				}
			}
		}
		else
		{
			if (this.CharacterAnimation[this.SocialSitAnim].weight != 0)
			{
				this.CharacterAnimation[this.SocialSitAnim].weight = Mathf.Lerp(
					this.CharacterAnimation[this.SocialSitAnim].weight, 0.0f, Time.deltaTime * 10.0f);

				if (this.CharacterAnimation[this.SocialSitAnim].weight < .01)
				{
					this.CharacterAnimation[this.SocialSitAnim].weight = 0;
				}
			}
		}

		if (this.DK)
		{
			this.Arm[0].localScale = new Vector3(2.0f, 2.0f, 2.0f);
			this.Arm[1].localScale = new Vector3(2.0f, 2.0f, 2.0f);
			this.Head.localScale = new Vector3(2.0f, 2.0f, 2.0f);
		}

		if (this.Fate > 0)
		{
			if (this.Clock.HourTime > this.TimeOfDeath)
			{
				this.Yandere.TargetStudent = this;
				this.StudentManager.Shinigami.Effect = this.Fate - 1;
				this.StudentManager.Shinigami.Attack();
				this.Yandere.TargetStudent = null;
				this.Fate = 0;
			}
		}

		//If Black Hole Mode is active...
		if (this.Yandere.BlackHole)
		{
			if (this.DistanceToPlayer < 2.5f)
			{
				if (this.DeathScream != null)
				{
					Instantiate(this.DeathScream, transform.position + Vector3.up, Quaternion.identity);
				}

				this.BlackHoleEffect[0].enabled = true;
				this.BlackHoleEffect[1].enabled = true;
				this.BlackHoleEffect[2].enabled = true;

                this.CharacterAnimation[this.WetAnim].weight = 0.0f;
                this.DeathType = DeathType.EasterEgg;
				this.CharacterAnimation.Stop();
				this.Suck.enabled = true;
				this.BecomeRagdoll();
				this.Dying = true;
			}
		}

		if (this.CameraReacting)
		{
			if (this.StudentManager.NEStairs.bounds.Contains(this.transform.position) ||
				this.StudentManager.NWStairs.bounds.Contains(this.transform.position) ||
				this.StudentManager.SEStairs.bounds.Contains(this.transform.position) ||
				this.StudentManager.SWStairs.bounds.Contains(this.transform.position)  )
			{
				this.Spine.LookAt(this.Yandere.Spine[0]);
				this.Head.LookAt(this.Yandere.Head);
			}
		}

		// [af] Commented in JS code.
		/*
		if (ShrinkHead)
		{
			this.HeadSize = Mathf.MoveTowards(this.HeadSize, 0, Time.deltaTime);
			
			this.Head.localScale = new Vector3(this.HeadSize, this.HeadSize, this.HeadSize);
		}
		*/
	}

	public void CalculateReputationPenalty()
	{
		if (this.Male && ((this.Yandere.Class.Seduction + this.Yandere.Class.SeductionBonus) > 2) ||
			((this.Yandere.Class.Seduction + this.Yandere.Class.SeductionBonus) > 4))
		{
			this.RepDeduction += this.RepLoss * 0.20f;
		}

		if (PlayerGlobals.Reputation < -33.33333f)
		{
			this.RepDeduction += this.RepLoss * 0.20f;
		}

		if (PlayerGlobals.Reputation > 33.33333f)
		{
			this.RepDeduction -= this.RepLoss * 0.20f;
		}

		if (PlayerGlobals.GetStudentFriend(this.StudentID))
		{
			this.RepDeduction += this.RepLoss * 0.20f;
		}

		if (PlayerGlobals.PantiesEquipped == 1)
		{
			this.RepDeduction += this.RepLoss * 0.20f;
		}

		if (this.Yandere.Class.SocialBonus > 0)
		{
			this.RepDeduction += this.RepLoss * 0.20f;
		}

		this.ChameleonCheck();

		if (this.Chameleon)
		{
			Debug.Log("Chopping reputation loss in half!");

			this.RepLoss *= 0.50f;
		}

		if (Yandere.Persona == YanderePersonaType.Aggressive)
		{
			this.RepLoss *= 2;
		}

		if (this.Club == ClubType.Bully)
		{
			this.RepLoss *= 2;
		}

		if (this.Club == ClubType.Delinquent)
		{
			this.RepDeduction = 0;
			this.RepLoss = 0;
		}
	}

	public void MoveTowardsTarget(Vector3 target)
	{
		if ((Time.timeScale > 0.0001f) && this.MyController.enabled)
		{
			Vector3 diff = target - this.transform.position;
			float distanceSquared = diff.sqrMagnitude;
			const float stoppingRangeSquared = 1.0e-6f;

			if (distanceSquared > stoppingRangeSquared)
			{
				this.MyController.Move(diff * ((Time.deltaTime * 5.0f) / Time.timeScale));
			}
		}
	}

	void LookTowardsTarget(Vector3 target)
	{
		if (Time.timeScale > 0.0001f)
		{
			// Do nothing.
		}
	}

	//Reacting to BEING attacked
	public void AttackReaction()
	{
		Debug.Log(this.Name + " is being attacked.");

		if (this.SolvingPuzzle)
		{
			this.DropPuzzle();
		}

		if (this.HorudaCollider != null)
		{
			this.HorudaCollider.gameObject.SetActive(false);
		}

		if (this.PhotoEvidence)
		{
			this.SmartPhone.GetComponent<SmartphoneScript>().enabled = true;
			this.SmartPhone.GetComponent<PromptScript>().enabled = true;
			this.SmartPhone.GetComponent<Rigidbody>().useGravity = true;
			this.SmartPhone.GetComponent<Collider>().enabled = true;
			this.SmartPhone.transform.parent = null;
			this.SmartPhone.layer = 15;
		}
		else
		{
			this.SmartPhone.SetActive(false);
		}

		if (!this.WitnessedMurder)
		{
			float Angle = Vector3.Angle(-this.transform.forward,
				this.Yandere.transform.position - this.transform.position);

			// [af] Replaced if/else statement with boolean expression.
			this.Yandere.AttackManager.Stealth = Mathf.Abs(Angle) <= 45.0f;
		}

		if (this.ReturningMisplacedWeapon)
		{
			Debug.Log(this.Name + " was in the process of returning a weapon when they were attacked.");

			this.DropMisplacedWeapon();
		}

		if (this.BloodPool != null)
		{
			Debug.Log(this.Name + "'s BloodPool was not null.");

			if (this.BloodPool.GetComponent<WeaponScript>() != null)
			{
				if (this.BloodPool.GetComponent<WeaponScript>().Returner == this)
				{
					this.BloodPool.GetComponent<WeaponScript>().Returner = null;
					this.BloodPool.GetComponent<WeaponScript>().Drop();
					this.BloodPool.GetComponent<WeaponScript>().enabled = true;
					this.BloodPool = null;
				}
			}
		}

        if (this.Yandere.Armed)
        {
            if (this.Yandere.EquippedWeapon.WeaponID == 27)
            {
                this.StudentManager.TranqDetector.GarroteAttack();
            }
        }

        if (!this.Male)
		{
			if (this.Club != ClubType.Council)
			{
				// This is the part of the script where the game determines
				// whether or not the player is going to perform a syringe
				// tranquilization.

				this.StudentManager.TranqDetector.TranqCheck();
			}

			this.CharacterAnimation[AnimNames.FemaleSmile].weight = 0.0f;
			this.SmartPhone.SetActive(false);

			this.CharacterAnimation[this.ShyAnim].weight = 0;
			this.Shy = false;
		}

		this.WitnessCamera.Show = false;

		this.Pathfinding.canSearch = false;
		this.Pathfinding.canMove = false;

		this.Yandere.CharacterAnimation[AnimNames.FemaleIdleShort].time = 0.0f;
		this.Yandere.CharacterAnimation[AnimNames.FemaleSwingA].time = 0.0f;
        this.CharacterAnimation[this.WetAnim].weight = 0.0f;

        this.Yandere.HipCollider.enabled = true;
		//this.Yandere.MyController.radius = 0.0f;
		this.Yandere.TargetStudent = this;
		this.Yandere.Obscurance.enabled = false;
		this.Yandere.YandereVision = false;
		this.Yandere.Attacking = true;
		this.Yandere.CanMove = false;

		if (this.Yandere.Equipped > 0)
		{
			if (this.Yandere.EquippedWeapon.AnimID == 2)
			{
				this.Yandere.CharacterAnimation[this.Yandere.ArmedAnims[2]].weight = 0.0f;
			}
		}

		if (this.DetectionMarker != null)
		{
			this.DetectionMarker.Tex.enabled = false;
		}

		this.EmptyHands();

		this.DropPlate();

		this.MyController.radius = 0.0f;

		// [af] Commented in JS code.
		//MyCollider.radius = 0;

		if (this.Persona == PersonaType.PhoneAddict && !this.Phoneless)
		{
			this.Countdown.gameObject.SetActive(false);
			this.ChaseCamera.SetActive(false);

			if (this.StudentManager.ChaseCamera == this.ChaseCamera)
			{
				this.StudentManager.ChaseCamera = null;
			}
		}

		this.VomitEmitter.gameObject.SetActive(false);
		this.InvestigatingBloodPool = false;
		this.Investigating = false;
		this.Pen.SetActive(false);
		this.EatingSnack = false;
		this.SpeechLines.Stop();
		this.Attacked = true;
		this.Alarmed = false;
		this.Fleeing = false;
		this.Routine = false;
		this.ReadPhase = 0;
		this.Dying = true;
		this.Wet = false;

		this.Prompt.Hide();
		this.Prompt.enabled = false;

		// [af] Commented in JS code.
		//if (!Dying)
		//{

		if (this.Following)
		{
			Debug.Log("Yandere-chan's follower is being set to ''null''.");

			// [af] This is the (unintuitive) Unity 5.3 way of changing emission.
			ParticleSystem.EmissionModule heartsEmission = this.Hearts.emission;
			heartsEmission.enabled = false;

			this.FollowCountdown.gameObject.SetActive(false);
			this.Yandere.Follower = null;
			this.Yandere.Followers--;
			this.Following = false;
		}

		//}

		if (this.Distracting)
		{
			this.DistractionTarget.TargetedForDistraction = false;
			this.DistractionTarget.Octodog.SetActive(false);
			this.DistractionTarget.Distracted = false;
			this.Distracting = false;
		}

		if (this.Teacher)
		{
			if (this.Yandere.Class.PhysicalGrade + this.Yandere.Class.PhysicalBonus > 0 && this.Yandere.EquippedWeapon.Type == WeaponType.Knife)
			{
				Debug.Log(this.Name + " has called the ''BeginStruggle'' function.");

				this.Pathfinding.target = this.Yandere.transform;
				this.CurrentDestination = this.Yandere.transform;

				this.Yandere.Attacking = false;
				this.Attacked = false;
				this.Fleeing = true; //Should this be true or false?
				this.Dying = false;
				this.Persona = PersonaType.Heroic;

				this.BeginStruggle();
			}
			else
			{
				this.Yandere.HeartRate.gameObject.SetActive(false);
				this.Yandere.ShoulderCamera.Counter = true;
				this.Yandere.ShoulderCamera.OverShoulder = false;
				this.Yandere.RPGCamera.enabled = false;
				this.Yandere.Senpai = this.transform;
				this.Yandere.Attacking = true;
				this.Yandere.CanMove = false;
				this.Yandere.Talking = false;
				this.Yandere.Noticed = true;
				this.Yandere.HUD.alpha = 0.0f;
			}
		}
		else if (this.Strength == 9)
		{
			if (!this.WitnessedMurder)
			{
				this.Subtitle.UpdateLabel(SubtitleType.ObstacleMurderReaction, 3, 11.0f);
			}

			this.Yandere.CharacterAnimation.CrossFade("f02_moCounterA_00");
			this.Yandere.HeartRate.gameObject.SetActive(false);
			this.Yandere.ShoulderCamera.ObstacleCounter = true;
			this.Yandere.ShoulderCamera.OverShoulder = false;
			this.Yandere.RPGCamera.enabled = false;
			this.Yandere.Senpai = this.transform;
			this.Yandere.Attacking = true;
			this.Yandere.CanMove = false;
			this.Yandere.Talking = false;
			this.Yandere.Noticed = true;
			this.Yandere.HUD.alpha = 0.0f;
		}
		else
		{
			if (!this.Yandere.AttackManager.Stealth)
			{
				this.Subtitle.UpdateLabel(SubtitleType.Dying, 0, 1.0f);
				this.SpawnAlarmDisc();
			}

			if (this.Yandere.SanityBased)
			{
				this.Yandere.AttackManager.Attack(
					this.Character, this.Yandere.EquippedWeapon);
			}
		}

		if (this.StudentManager.Reporter == this)
		{
			this.StudentManager.Reporter = null;

			if (this.ReportPhase == 0)
			{
				Debug.Log("A reporter died before being able to report a corpse. Corpse position reset.");

				this.StudentManager.CorpseLocation.position = Vector3.zero;
			}
		}

		if (this.Club == ClubType.Delinquent)
		{
			if (this.MyWeapon != null)
			{
				if (this.MyWeapon.transform.parent == this.ItemParent)
				{
					this.MyWeapon.transform.parent = null;
					this.MyWeapon.MyCollider.enabled = true;
					this.MyWeapon.Prompt.enabled = true;

					Rigidbody rigidBody = this.MyWeapon.GetComponent<Rigidbody>();
					rigidBody.constraints = RigidbodyConstraints.None;
					rigidBody.isKinematic = false;
					rigidBody.useGravity = true;
				}
			}
		}

		if (this.PhotoEvidence)
		{
			this.CameraFlash.SetActive(false);
			this.SmartPhone.SetActive(true);
		}
	}

	public void DropPlate()
	{
		if (this.MyPlate != null)
		{
			if (this.MyPlate.parent == this.RightHand)
			{
				this.MyPlate.GetComponent<Rigidbody>().isKinematic = false;
				this.MyPlate.GetComponent<Rigidbody>().useGravity = true;
				this.MyPlate.GetComponent<Collider>().enabled = true;
				this.MyPlate.parent = null;
				this.MyPlate.gameObject.SetActive(true);
			}

			if (this.Distracting)
			{
				this.DistractionTarget.TargetedForDistraction = false;
				this.Distracting = false;

				this.IdleAnim = this.OriginalIdleAnim;
				this.WalkAnim = this.OriginalWalkAnim;
			}
		}
	}

	public void SenpaiNoticed()
	{
		Debug.Log("The ''SenpaiNoticed'' function has been called.");

		if (this.Yandere.Shutter.Snapping)
		{
			this.Yandere.Shutter.ResumeGameplay();
			this.Yandere.StopAiming();
		}

        this.Yandere.Stance.Current = StanceType.Standing;
        this.Yandere.CrawlTimer = 0.0f;
        this.Yandere.Uncrouch();

        this.Yandere.Noticed = true;

		if (this.WeaponToTakeAway != null)
		{
			if (this.Teacher)
			{
				if (!this.Yandere.Attacking)
				{
					Debug.Log("Taking away Yandere-chan's weapon.");

                    //If it's a fire extinguisher...
                    if (this.Yandere.EquippedWeapon.WeaponID == 23)
                    {
                        this.WeaponToTakeAway.transform.parent = null;
                        this.WeaponToTakeAway.transform.position = this.WeaponToTakeAway.GetComponent<WeaponScript>().StartingPosition;
                        this.WeaponToTakeAway.transform.eulerAngles = this.WeaponToTakeAway.GetComponent<WeaponScript>().StartingRotation;

                        this.WeaponToTakeAway.GetComponent<WeaponScript>().Prompt.enabled = true;
                        this.WeaponToTakeAway.GetComponent<WeaponScript>().enabled = true;
                        this.WeaponToTakeAway.GetComponent<WeaponScript>().Drop();
                        this.WeaponToTakeAway.GetComponent<WeaponScript>().MyRigidbody.useGravity = false;
                        this.WeaponToTakeAway.GetComponent<WeaponScript>().MyRigidbody.isKinematic = true;
                    }
                    //If it's anything else...
                    else
                    {
                        this.Yandere.EquippedWeapon.Drop();
                        this.WeaponToTakeAway.transform.position = this.StudentManager.WeaponBoxSpot.parent.position + new Vector3(0, 1, 0);
                        this.WeaponToTakeAway.transform.eulerAngles = new Vector3(0, 90, 0);
                    }
                }
            }
		}

		this.WeaponToTakeAway = null;

		if (!this.Yandere.Attacking)
		{
			this.Yandere.EmptyHands();
		}

		this.Yandere.Senpai = this.transform;

		if (this.Yandere.Aiming)
		{
			this.Yandere.StopAiming();
		}

		this.Yandere.DetectionPanel.alpha = 0.0f;
		this.Yandere.RPGCamera.mouseSpeed = 0.0f;
		this.Yandere.LaughIntensity = 0.0f;
		this.Yandere.HUD.alpha = 0.0f;
		this.Yandere.EyeShrink = 0.0f;
		this.Yandere.Sanity = 100.0f;

		this.Yandere.ProgressBar.transform.parent.gameObject.SetActive(false);
		this.Yandere.HeartRate.gameObject.SetActive(false);
		this.Yandere.Stance.Current = StanceType.Standing;

		this.Yandere.ShoulderCamera.OverShoulder = false;
		this.Yandere.Obscurance.enabled = false;
		this.Yandere.DelinquentFighting = false;
		this.Yandere.YandereVision = false;
		this.Yandere.CannotRecover = true;
		this.Yandere.Police.Show = false;
		this.Yandere.Poisoning = false;
		this.Yandere.Rummaging = false;
		this.Yandere.Laughing = false;
		this.Yandere.CanMove = false;
		this.Yandere.Dipping = false;
		this.Yandere.Mopping = false;
		this.Yandere.Talking = false;

		this.Yandere.Jukebox.GameOver();

        this.StudentManager.GameOverIminent = true;
		this.StudentManager.StopMoving();

		if (this.Teacher || (this.StudentID == 1))
		{
			if (this.Club != ClubType.Council)
			{
				this.IdleAnim = this.OriginalIdleAnim;
			}

            if (this.Giggle != null)
            {
                this.ForgetGiggle();
            }

			this.AlarmTimer = 0;
			this.enabled = true;
			this.Stop = false;
		}

		if (this.StudentID == 1)
		{
			this.StudentManager.FountainAudio[0].Stop();
			this.StudentManager.FountainAudio[1].Stop();
		}
	}

	void WitnessMurder()
	{
		Debug.Log (this.Name + " just realized that Yandere-chan is responsible for a murder!");

		this.RespectEarned = false;

		if (this.Fleeing && this.WitnessedBloodPool || this.ReportPhase == 2)
		{
			this.WitnessedBloodyWeapon = false;
			this.WitnessedBloodPool = false;
			this.WitnessedSomething = false;
			this.WitnessedWeapon = false;
			this.WitnessedLimb = false;
			this.Fleeing = false;

			this.ReportPhase = 0;
		}

		this.CharacterAnimation[this.ScaredAnim].time = 0;
		this.CameraFlash.SetActive(false);

		if (!this.Male)
		{
			this.CharacterAnimation[AnimNames.FemaleSmile].weight = 0.0f;
			this.WateringCan.SetActive(false);
		}

		if (this.MyPlate != null)
		{
			if (this.MyPlate.parent == this.RightHand)
			{
				this.MyPlate.GetComponent<Rigidbody>().isKinematic = false;
				this.MyPlate.GetComponent<Rigidbody>().useGravity = true;
				this.MyPlate.GetComponent<Collider>().enabled = true;
				this.MyPlate.parent = null;
			}
		}

		this.EmptyHands();

		this.MurdersWitnessed++;
		this.SpeechLines.Stop();

		this.WitnessedBloodyWeapon = false;
		this.WitnessedBloodPool = false;
		this.WitnessedSomething = false;
		this.WitnessedWeapon = false;
		this.WitnessedLimb = false;

		if (this.ReturningMisplacedWeapon)
		{
			this.DropMisplacedWeapon();
		}

		this.ReturningMisplacedWeapon = false;
		this.InvestigatingBloodPool = false;
		this.CameraReacting = false;
		this.WitnessedMurder = true;
		this.Investigating = false;
		this.Distracting = false;
		this.EatingSnack = false;
		this.Threatened = false;
		this.Distracted = false;
		this.Reacted = false;
		this.Routine = false;
		this.Alarmed = true;
		this.NoTalk = false;
		this.Wet = false;

		if (this.OriginalPersona != PersonaType.Violent && this.Persona != this.OriginalPersona)
		{
			Debug.Log (this.Name + " is reverting back into their original Persona.");

			this.Persona = this.OriginalPersona;
			this.SwitchBack = false;

			if (this.Persona == PersonaType.Heroic || this.Persona == PersonaType.Dangerous)
			{
				this.PersonaReaction();
			}
		}

		// Transform a Spiteful student into the Evil Persona depending on the victim.
		if (this.Persona == PersonaType.Spiteful && this.Yandere.TargetStudent != null)
		{
			Debug.Log("A Spiteful student witnessed a murder.");

			if (this.Bullied && this.Yandere.TargetStudent.Club == ClubType.Bully || this.Yandere.TargetStudent.Bullied)
			{
				this.ScaredAnim = this.EvilWitnessAnim;
				this.Persona = PersonaType.Evil;
			}
		}

		// Transform a Delinquent into the Evil Persona depending on the victim.
		if (this.Club == ClubType.Delinquent)
		{
			Debug.Log("A Delinquent witnessed a murder.");

			if (this.Yandere.TargetStudent != null)
			{
				if (this.Yandere.TargetStudent.Club == ClubType.Bully)
				{
					this.ScaredAnim = this.EvilWitnessAnim;
					this.Persona = PersonaType.Evil;
				}
			}

			if (this.Yandere.Lifting || this.Yandere.Carrying || this.Yandere.Dragging)
			{
				if (this.Yandere.CurrentRagdoll.Student.Club == ClubType.Bully)
				{
					this.ScaredAnim = this.EvilWitnessAnim;
					this.Persona = PersonaType.Evil;
				}
			}
		}

		// Transform a Sleuth into a Social Butterfly.
		if (this.Persona == PersonaType.Sleuth)
		{
			Debug.Log("A Sleuth is witnessing a murder.");

			if (this.Yandere.Attacking || this.Yandere.Struggling || 
				this.Yandere.Carrying || this.Yandere.Lifting ||
				this.Yandere.PickUp != null && 
				this.Yandere.PickUp.BodyPart)
			{
				if (!this.Sleuthing)
				{
					Debug.Log("A Sleuth is changing their Persona.");

					if (this.StudentID == 56)
					{
						this.Persona = PersonaType.Heroic;
					}
					else
					{
						this.Persona = PersonaType.SocialButterfly;
					}
				}
				else
				{
					this.Persona = PersonaType.SocialButterfly;
				}
			}
		}

        if (this.Persona == PersonaType.Heroic)
        {
            this.Yandere.Pursuer = this;
        }

        //For any student that is not Senpai...
        if (this.StudentID > 1 || this.Yandere.Mask != null)
		{
			// [af] Converted while loop to for loop.
			for (this.ID = 0; this.ID < this.Outlines.Length; this.ID++)
			{
				this.Outlines[this.ID].color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
				this.Outlines[this.ID].enabled = true;
			}

            this.SummonWitnessCamera();

			this.CameraEffects.MurderWitnessed();

			this.Witnessed = StudentWitnessType.Murder;

			//Debug.Log(this.Name + "'s ''Witnessed'' is being set to ''StudentWitnessType.Murder''.");

			if (this.Persona != PersonaType.Evil)
			{
				this.Police.Witnesses++;
			}

			if (this.Teacher)
			{
				this.StudentManager.Reporter = this;
			}

			if (this.Talking)
			{
				this.DialogueWheel.End();

				// [af] This is the (unintuitive) Unity 5.3 way of changing emission.
				ParticleSystem.EmissionModule heartsEmission = this.Hearts.emission;
				heartsEmission.enabled = false;

				this.Pathfinding.canSearch = true;
				this.Pathfinding.canMove = true;
				this.Obstacle.enabled = false;
				this.Talk.enabled = false;
				this.Talking = false;
				this.Waiting = false;

				this.StudentManager.EnablePrompts();
			}

			if (this.Prompt.Label[0] != null)
			{
				if (!GameGlobals.EmptyDemon)
				{
					this.Prompt.Label[0].text = "     " + "Talk";
					this.Prompt.HideButton[0] = true;
				}
			}
		}
		//Senpai's reaction to murder
		else
		{
			if (!this.Yandere.Attacking && !this.Yandere.Struggling)
			{
				this.SenpaiNoticed();
			}

			this.Fleeing = false;
			this.EyeShrink = 0.0f;
			this.Yandere.Noticed = true;
			this.Yandere.Talking = false;
			this.CameraEffects.MurderWitnessed();
			this.Yandere.ShoulderCamera.OverShoulder = false;

			this.CharacterAnimation.CrossFade(this.ScaredAnim);

			this.CharacterAnimation[AnimNames.MaleScaredFace].weight = 1.0f;

			if (this.StudentID == 1)
			{
				Debug.Log("Senpai entered his scared animation.");
			}
		}

		if (this.Persona == PersonaType.TeachersPet)
		{
			if (this.StudentManager.Reporter == null)
			{
				if (!this.Police.Called)
				{
					this.StudentManager.CorpseLocation.position = this.Yandere.transform.position;
					this.StudentManager.LowerCorpsePosition();
					this.StudentManager.Reporter = this;
					this.ReportingMurder = true;
				}
			}
		}

		if (this.Following)
		{
			// [af] This is the (unintuitive) Unity 5.3 way of changing emission.
			ParticleSystem.EmissionModule heartsEmission = this.Hearts.emission;
			heartsEmission.enabled = false;

			this.FollowCountdown.gameObject.SetActive(false);
            this.Yandere.Follower = null;
			this.Yandere.Followers--;
			this.Following = false;
		}
			
		this.Pathfinding.canSearch = false;
		this.Pathfinding.canMove = false;

		if (!this.Phoneless)
		{
			if (this.Persona == PersonaType.PhoneAddict || this.Sleuthing)
			{
				this.SmartPhone.SetActive(true);
			}
		}

		// If we have a phone out...
		if (this.SmartPhone.activeInHierarchy)
		{
			// If we are NOT a Dangerous/Heroic/Evil/Cowardly student or a teacher...
			if (this.Persona != PersonaType.Heroic &&
				this.Persona != PersonaType.Dangerous &&
				this.Persona != PersonaType.Evil &&
				this.Persona != PersonaType.Violent &&
				this.Persona != PersonaType.Coward &&
				!this.Teacher)
			{
				// Switch to the Phone Addict persona.
				this.Persona = PersonaType.PhoneAddict;

				if (!this.Sleuthing)
				{
					this.SprintAnim = this.PhoneAnims[2];
				}
				else
				{
					this.SprintAnim = this.SleuthReportAnim;
				}
			}
			else
			{
				this.SmartPhone.SetActive(false);
			}
		}

		this.StopPairing();

		if (this.Persona != PersonaType.Heroic)
		{
			this.AlarmTimer = 0.0f;
			this.Alarm = 0.0f;
		}

		if (this.Teacher)
		{
			if (!this.Yandere.Chased)
			{
				Debug.Log ("A teacher has reached ChaseYandere through WitnessMurder.");

				this.ChaseYandere();
			}
		}
		else
		{
			this.SpawnAlarmDisc();
		}

		if (!this.PinDownWitness)
		{
			if (this.Persona != PersonaType.Evil)
			{
				this.StudentManager.Witnesses++;
				this.StudentManager.WitnessList[this.StudentManager.Witnesses] = this;
				this.StudentManager.PinDownCheck();
				this.PinDownWitness = true;
			}
		}

		if (this.Persona == PersonaType.Violent)
		{
			this.Pathfinding.canSearch = false;
			this.Pathfinding.canMove = false;
		}

		if (this.Yandere.Mask == null)
		{
			this.SawMask = false;

			if (this.Persona != PersonaType.Evil)
			{
				this.Grudge = true;
			}
		}
		else
		{
			this.SawMask = true;
		}

		this.StudentManager.UpdateMe(this.StudentID);
	}

	public void DropMisplacedWeapon()
	{
		this.WitnessedWeapon = false;

		this.InvestigatingBloodPool = false;

		this.ReturningMisplacedWeaponPhase = 0;
		this.ReturningMisplacedWeapon = false;

		this.BloodPool.GetComponent<WeaponScript>().Returner = null;
		this.BloodPool.GetComponent<WeaponScript>().Drop();
		this.BloodPool.GetComponent<WeaponScript>().enabled = true;
		this.BloodPool = null;
	}

	void ChaseYandere()
	{
		Debug.Log(this.Name + " has begun to chase Yandere-chan.");

		this.CurrentDestination = this.Yandere.transform;
		this.Pathfinding.target = this.Yandere.transform;
		//this.Pathfinding.canSearch = true;
		//this.Pathfinding.canMove = true;
		this.Pathfinding.speed = 5.0f;

		//this.StudentManager.Portal.SetActive(false);

		if (Yandere.Pursuer == null)
		{
			this.Yandere.Pursuer = this;
		}

		// [af] Commented in JS code.
		//Yandere.Chased = true;

		this.TargetDistance = 1.00f;
		this.AlarmTimer = 0.0f;

		this.Chasing = false;
		this.Fleeing = false;

		this.StudentManager.UpdateStudents();
	}

	void PersonaReaction()
	{
		//Debug.Log(this.Name + " has started calling PersonaReaction(). As of now, they are a: " + this.Persona + ".");

		if (this.Persona == PersonaType.Sleuth)
		{
			if (this.Sleuthing)
			{
				// Switch to the Phone Addict persona.
				this.Persona = PersonaType.PhoneAddict;
				this.SmartPhone.SetActive(true);
			}
			else
			{
				this.Persona = PersonaType.SocialButterfly;
			}
		}

		if (this.Persona == PersonaType.PhoneAddict && this.Phoneless)
		{
			//Debug.Log(this.Name + " was a phone addict, but they don't have a phone right now, so they are switching to the Loner Persona.");

			// Switch to the Loner persona.
			this.Persona = PersonaType.Loner;
		}

		// If we haven't even gotten inside the school yet...
		if (!this.Indoors)
		{
			// If we just witnessed a murder...
			if (this.WitnessedMurder)
			{
				// If we are NOT the current rival...
				if (this.StudentID != this.StudentManager.RivalID)
				{
					Debug.Log(this.Name + "'s current Persona is: " + this.Persona);

					// If we are any persona other than Hero/Evil/Coward/PhoneAddict/Protective/Violent...
					if (this.Persona != PersonaType.Evil &&
						this.Persona != PersonaType.Heroic &&
						this.Persona != PersonaType.Coward &&
						this.Persona != PersonaType.PhoneAddict &&
						this.Persona != PersonaType.Protective &&
						this.Persona != PersonaType.Violent ||
						this.Injured)
					{
						Debug.Log(this.Name + " is switching to the Loner Persona.");

						// Switch to the Loner persona.
						this.Persona = PersonaType.Loner;
					}
				}
			}
		}

		if (!this.WitnessedMurder)
		{
			// If a hero discovers a corpse...
			if (this.Persona == PersonaType.Heroic)
			{
				this.SwitchBack = true;

				// [af] Replaced if/else statement with ternary expression.
				this.Persona = (this.Corpse != null) ? PersonaType.TeachersPet : PersonaType.Loner;
			}
			// If a cowardly student or evil student discovers a corpse...
			else if (this.Persona == PersonaType.Coward ||
					 this.Persona == PersonaType.Evil ||
				     this.Persona == PersonaType.Fragile)
			{
				//Debug.Log(this.Name + " is switching to the Loner Persona.");

				this.Persona = PersonaType.Loner;
			}

			// If Raibaru discovers a corpse...

			//Anti-Osana Code
			#if UNITY_EDITOR
			else if (this.Persona == PersonaType.Protective)
			{
				Debug.Log("Raibaru witnessed a corpse, and should be running to tell Osana about it.");

				this.Persona = PersonaType.Lovestruck;
			}
			#endif
		}

		// Loner.
		if (this.Persona == PersonaType.Loner || this.Persona == PersonaType.Spiteful)
		{
			Debug.Log(this.Name + " is looking in the Loner/Spiteful section of PersonaReaction() to decide what to do next.");

			if (this.Club == ClubType.Delinquent)
			{
				Debug.Log("A delinquent turned into a loner, and now he is fleeing.");

				if (this.Injured)
				{
					this.Subtitle.Speaker = this;
					this.Subtitle.UpdateLabel(SubtitleType.DelinquentInjuredFlee, 1, 3.0f);
				}
				else if (this.FoundFriendCorpse)
				{
					this.Subtitle.Speaker = this;
					this.Subtitle.UpdateLabel(SubtitleType.DelinquentFriendFlee, 1, 3.0f);
				}
				else if (this.FoundEnemyCorpse)
				{
					this.Subtitle.Speaker = this;
					this.Subtitle.UpdateLabel(SubtitleType.DelinquentEnemyFlee, 1, 3.0f);
				}
				else
				{
					this.Subtitle.Speaker = this;
					this.Subtitle.UpdateLabel(SubtitleType.DelinquentFlee, 1, 3.0f);
				}
			}
			else
			{
				if (this.WitnessedMurder){this.Subtitle.UpdateLabel(SubtitleType.LonerMurderReaction, 1, 3.0f);}
				else{this.Subtitle.UpdateLabel(SubtitleType.LonerCorpseReaction, 1, 3.0f);}
			}

			if (this.Schoolwear > 0)
			{
				if (!this.Bloody)
				{
					this.Pathfinding.target = this.StudentManager.Exit;
					this.TargetDistance = 0.0f;
					this.Routine = false;
					this.Fleeing = true;
				}
				else
				{
					this.FleeWhenClean = true;
					this.TargetDistance = 1.0f;
					this.BatheFast = true;
				}
			}
			else
			{
				this.FleeWhenClean = true;

				if (!this.Bloody)
				{
					this.BathePhase = 5;
					this.GoChange();
				}
				else
				{
					this.CurrentDestination = this.StudentManager.FastBatheSpot;
					this.Pathfinding.target = this.StudentManager.FastBatheSpot;
					this.TargetDistance = 1.0f;
					this.BatheFast = true;
				}
			}

			if (this.Corpse != null)
			{
				if (this.Corpse.StudentID == this.StudentManager.RivalID)
				{
					this.CurrentDestination = this.Corpse.Student.Hips;
					this.Pathfinding.target = this.Corpse.Student.Hips;
					this.SenpaiWitnessingRivalDie = true;
					this.DistanceToDestination = 1;
					this.WitnessRivalDiePhase = 3;
					this.Routine = false;

					this.TargetDistance = .5f;
				}
			}
		}
		// Teacher's Pet.
		else if (this.Persona == PersonaType.TeachersPet)
		{
			if (this.WitnessedBloodPool || this.WitnessedLimb || this.WitnessedWeapon)
			{
				if (this.StudentManager.BloodReporter == null)
				{
					if (this.Club != ClubType.Delinquent && !this.Police.Called && !this.LostTeacherTrust)
					{
						this.StudentManager.BloodLocation.position = this.BloodPool.transform.position;
						this.StudentManager.LowerBloodPosition();

						Debug.Log(Name + " has become a ''blood reporter''.");

						this.StudentManager.BloodReporter = this;
						this.ReportingBlood = true;

						this.DetermineBloodLocation();
					}
					else
					{
						this.ReportingBlood = false;
					}
				}
			}
			else
			{
				//Debug.Log("For some reason, " + this.Name + " triggered this code.");

				if (this.StudentManager.Reporter == null)
				{
					if (!this.Police.Called)
					{
						this.StudentManager.CorpseLocation.position = this.Corpse.AllColliders[0].transform.position;
						this.StudentManager.LowerCorpsePosition();

						Debug.Log(this.Name + " has become a ''reporter''.");

						this.StudentManager.Reporter = this;
						this.ReportingMurder = true;

						this.DetermineCorpseLocation();
					}
				}
			}

			if (this.StudentManager.Reporter == this)
			{
				Debug.Log(this.Name + " is running to a teacher to report murder.");

				this.Pathfinding.target = this.StudentManager.Teachers[this.Class].transform;
				this.CurrentDestination = this.StudentManager.Teachers[this.Class].transform;

				this.TargetDistance = 2.0f;

				if (this.WitnessedMurder)
				{
					this.Subtitle.UpdateLabel(SubtitleType.PetMurderReport, 1, 3.0f);
				}
				else if (this.WitnessedCorpse)
				{
					this.Subtitle.UpdateLabel(SubtitleType.PetCorpseReport, 1, 3.0f);
				}
			}
			else if (this.StudentManager.BloodReporter == this)
			{
				Debug.Log(this.Name + " is running to a teacher to report something.");

				this.DropPlate();

				this.Pathfinding.target = this.StudentManager.Teachers[this.Class].transform;
				this.CurrentDestination = this.StudentManager.Teachers[this.Class].transform;

				this.TargetDistance = 2.0f;

				if (this.WitnessedLimb)
				{
					this.Subtitle.UpdateLabel(SubtitleType.LimbReaction, 1, 3.0f);
				}
				else if (this.WitnessedBloodyWeapon)
				{
					this.Subtitle.UpdateLabel(SubtitleType.BloodyWeaponReaction, 1, 3.0f);
				}
				else if (this.WitnessedBloodPool)
				{
					this.Subtitle.UpdateLabel(SubtitleType.BloodPoolReaction, 1, 3.0f);
				}
				else if (this.WitnessedWeapon)
				{
					this.Subtitle.UpdateLabel(SubtitleType.PetWeaponReport, 1, 3.0f);
				}
			}
			else
			{
				//Debug.Log("This character has witnessed something that is not a murder or a corpse, and is reacting to it.");

				if (this.Club == ClubType.Council)
				{
					if (this.WitnessedCorpse)
					{
						//Debug.Log("A student council member has been told to travel to ''CorpseGuardLocation''.");

						if (this.StudentManager.CorpseLocation.position == Vector3.zero)
						{
							this.StudentManager.CorpseLocation.position = this.Corpse.AllColliders[0].transform.position;
							this.AssignCorpseGuardLocations();
						}

						     if (this.StudentID == 86) {this.Pathfinding.target = this.StudentManager.CorpseGuardLocation[1];}
						else if (this.StudentID == 87) {this.Pathfinding.target = this.StudentManager.CorpseGuardLocation[2];}
						else if (this.StudentID == 88) {this.Pathfinding.target = this.StudentManager.CorpseGuardLocation[3];}
						else if (this.StudentID == 89) {this.Pathfinding.target = this.StudentManager.CorpseGuardLocation[4];}

						this.CurrentDestination = this.Pathfinding.target;
					}
					else
					{
						Debug.Log("A student council member is being told to travel to ''BloodGuardLocation''.");

						if (this.StudentManager.BloodLocation.position == Vector3.zero)
						{
							this.StudentManager.BloodLocation.position = this.BloodPool.transform.position;
							this.AssignBloodGuardLocations();
						}

						if (this.StudentManager.BloodGuardLocation[1].position == Vector3.zero)
						{
							this.AssignBloodGuardLocations();
						}

						if (this.StudentID == 86) {this.Pathfinding.target = this.StudentManager.BloodGuardLocation[1];}
						else if (this.StudentID == 87) {this.Pathfinding.target = this.StudentManager.BloodGuardLocation[2];}
						else if (this.StudentID == 88) {this.Pathfinding.target = this.StudentManager.BloodGuardLocation[3];}
						else if (this.StudentID == 89) {this.Pathfinding.target = this.StudentManager.BloodGuardLocation[4];}

						this.CurrentDestination = this.Pathfinding.target;

						this.Guarding = true;
					}
				}
				else
				{
					this.PetDestination = Instantiate(EmptyGameObject, this.Seat.position + (this.Seat.forward * -.5f), Quaternion.identity).transform;

					this.Pathfinding.target = this.PetDestination;
					this.CurrentDestination = this.PetDestination;

					this.Distracting = false;

					this.DropPlate();

					//Debug.Log(this.Name + " is supposed to head to their desk now.");
				}

				if (this.WitnessedMurder)
				{
					this.Subtitle.UpdateLabel(SubtitleType.PetMurderReaction, 1, 3.0f);
				}
				else if (this.WitnessedCorpse)
				{
					this.Subtitle.UpdateLabel(SubtitleType.PetCorpseReaction, 1, 3.0f);
				}
				else if (this.WitnessedLimb)
				{
					this.Subtitle.UpdateLabel(SubtitleType.PetLimbReaction, 1, 3.0f);
				}
				else if (this.WitnessedBloodyWeapon)
				{
					this.Subtitle.UpdateLabel(SubtitleType.PetBloodyWeaponReaction, 1, 3.0f);
				}
				else if (this.WitnessedBloodPool)
				{
					this.Subtitle.UpdateLabel(SubtitleType.PetBloodReaction, 1, 3.0f);
				}
				else if (this.WitnessedWeapon)
				{
					this.Subtitle.UpdateLabel(SubtitleType.PetWeaponReaction, 1, 3.0f);
				}

				this.TargetDistance = 1.0f;

				this.ReportingMurder = false;
				this.ReportingBlood = false;
			}

			this.Routine = false;
			this.Fleeing = true;
		}
		// Hero / Protective
		else if (this.Persona == PersonaType.Heroic || this.Persona == PersonaType.Protective)
		{
			if (!this.Yandere.Chased)
			{
				this.StudentManager.PinDownCheck();

				if (!this.StudentManager.PinningDown)
				{
					Debug.Log(this.Name + "'s ''Flee'' was set to ''true'' because Hero persona reaction was called.");

					if (this.Persona == PersonaType.Protective)
					{
						this.Subtitle.UpdateLabel(SubtitleType.ObstacleMurderReaction, 2, 3.0f);
					}
					else if (this.Persona != PersonaType.Violent)
					{
						this.Subtitle.UpdateLabel(SubtitleType.HeroMurderReaction, 3, 3.0f);
					}
					else
					{
						if (this.Defeats > 0)
						{
							this.Subtitle.Speaker = this;
							this.Subtitle.UpdateLabel(SubtitleType.DelinquentResume, 3, 3.0f);
						}
						else
						{
							this.Subtitle.Speaker = this;
							this.Subtitle.UpdateLabel(SubtitleType.DelinquentMurderReaction, 3, 3.0f);
						}
					}

					this.Pathfinding.target = this.Yandere.transform;
					this.Pathfinding.speed = 5.0f;

					//this.StudentManager.Portal.SetActive(false);
					this.Yandere.Pursuer = this;
					this.Yandere.Chased = true;
					this.TargetDistance = 1.0f;

					this.StudentManager.UpdateStudents();

					this.Routine = false;
					this.Fleeing = true;
				}
			}
		}
		// Coward.
		else if (this.Persona == PersonaType.Coward || this.Persona == PersonaType.Fragile)
		{
			Debug.Log("This character just set their destination to themself.");

			this.CurrentDestination = this.transform;
			this.Pathfinding.target = this.transform;

			this.Subtitle.UpdateLabel(SubtitleType.CowardMurderReaction, 1, 5.0f);
			this.Routine = false;
			this.Fleeing = true;
		}
		// Evil.
		else if (this.Persona == PersonaType.Evil)
		{
			Debug.Log("This character just set their destination to themself.");

			this.CurrentDestination = this.transform;
			this.Pathfinding.target = this.transform;

			this.Subtitle.UpdateLabel(SubtitleType.EvilMurderReaction, 1, 5.0f);
			this.Routine = false;
			this.Fleeing = true;
		}
		// Social Butterfly.
		else if (this.Persona == PersonaType.SocialButterfly)
		{
			Debug.Log("A social butterfly is calling PersonaReaction().");

			this.StudentManager.HidingSpots.List[this.StudentID].position = this.StudentManager.PopulationManager.GetCrowdedLocation();

			this.CurrentDestination = this.StudentManager.HidingSpots.List[this.StudentID];
			this.Pathfinding.target = this.StudentManager.HidingSpots.List[this.StudentID];

			this.Subtitle.UpdateLabel(SubtitleType.SocialDeathReaction, 1, 5.0f);
			this.TargetDistance = 2;
			this.ReportPhase = 1;
			this.Routine = false;
			this.Fleeing = true;
			this.Halt = true;
		}
		// Rival / Lovestruck.

		//Anti-Osana Code
		#if UNITY_EDITOR
		else if (this.Persona == PersonaType.Lovestruck)
		{
			if (this.Corpse.StudentID == this.StudentManager.RivalID)
			{
				this.CurrentDestination = this.Corpse.Student.Hips;
				this.Pathfinding.target = this.Corpse.Student.Hips;
				this.SpecialRivalDeathReaction = true;
				this.WitnessRivalDiePhase = 1;
				this.Routine = false;

				this.TargetDistance = .5f;
			}
			else
			{
				if (this.StudentManager.Students[this.LovestruckTarget].WitnessedMurder == false)
				{
					Debug.Log("Destination should be set to Lovestruck Target.");

					this.CurrentDestination = this.StudentManager.Students[this.LovestruckTarget].transform;
					this.Pathfinding.target = this.StudentManager.Students[this.LovestruckTarget].transform;
					this.StudentManager.Students[this.LovestruckTarget].TargetedForDistraction = true;
					this.TargetDistance = 1;
					this.ReportPhase = 1;
				}
				else
				{
					Debug.Log("Destination should be set to Exit.");

					this.CurrentDestination = this.StudentManager.Exit;
					this.Pathfinding.target = this.StudentManager.Exit;
					this.TargetDistance = 0;
					this.ReportPhase = 3;
				}

				if (this.LovestruckTarget == 1)
				{
					this.Subtitle.UpdateLabel(SubtitleType.LovestruckDeathReaction, 0, 5.0f);
				}
				else
				{
					this.Subtitle.UpdateLabel(SubtitleType.LovestruckDeathReaction, 1, 5.0f);
				}

				this.DistanceToDestination = 100;
				this.Pathfinding.canSearch = true;
				this.Pathfinding.canMove = true;

				this.Routine = false;
				this.Fleeing = true;
				this.Halt = true;
			}
		}
		#endif

		// Dangerous.
		else if (this.Persona == PersonaType.Dangerous)
		{
			//Debug.Log("A student council member's PersonaReaction has been triggered.");

			if (this.WitnessedMurder)
			{
				//if (!this.Yandere.Chased)
				//{
					//Debug.Log("Began fleeing because Dangerous persona reaction was called.");

						 if (this.StudentID == 86){this.Subtitle.UpdateLabel(SubtitleType.Chasing, 1, 5.0f);}
					else if (this.StudentID == 87){this.Subtitle.UpdateLabel(SubtitleType.Chasing, 2, 5.0f);}
					else if (this.StudentID == 88){this.Subtitle.UpdateLabel(SubtitleType.Chasing, 3, 5.0f);}
					else if (this.StudentID == 89){this.Subtitle.UpdateLabel(SubtitleType.Chasing, 4, 5.0f);}

					this.Pathfinding.target = this.Yandere.transform;
					this.Pathfinding.speed = 5.0f;

					//this.StudentManager.Portal.SetActive(false);
					this.Yandere.Chased = true;
					this.TargetDistance = 1.00f;

					this.StudentManager.UpdateStudents();

					//Debug.Log("Sprinting 10");
					//this.CharacterAnimation.CrossFade(SprintAnim);
					this.Routine = false;
					this.Fleeing = true;
					this.Halt = true;
				//}
			}
			else
			{
				Debug.Log("A student council member has transformed into a Teacher's Pet.");

				this.Persona = PersonaType.TeachersPet;
				this.PersonaReaction();
			}
		}
		// Phone Addict
		else if (this.Persona == PersonaType.PhoneAddict)
		{
			Debug.Log (this.Name + " is executing the Phone Addict Persona code.");

			this.CurrentDestination = this.StudentManager.Exit;
			this.Pathfinding.target = this.StudentManager.Exit;

			this.Countdown.gameObject.SetActive(true);
			this.Routine = false;
			this.Fleeing = true;

			if (this.StudentManager.ChaseCamera == null)
			{
				this.StudentManager.ChaseCamera = this.ChaseCamera;
				this.ChaseCamera.SetActive(true);
			}
		}
		// Delinquent / Violent
		else if (this.Persona == PersonaType.Violent)
		{
			Debug.Log (this.Name + ", a delinquent, is currently in the ''Violent'' part of PersonaReaction()");

			if (this.WitnessedMurder)
			{
				if (!this.Yandere.Chased)
				{
					this.StudentManager.PinDownCheck();

					if (!this.StudentManager.PinningDown)
					{
						Debug.Log(Name + " began fleeing because Violent persona reaction was called.");

						if (this.Defeats > 0)
						{
							this.Subtitle.Speaker = this;
							this.Subtitle.UpdateLabel(SubtitleType.DelinquentResume, 3, 3.0f);
						}
						else
						{
							this.Subtitle.Speaker = this;
							this.Subtitle.UpdateLabel(SubtitleType.DelinquentMurderReaction, 3, 3.0f);
						}

						this.Pathfinding.target = this.Yandere.transform;
						this.Pathfinding.canSearch = true;
						this.Pathfinding.canMove = true;
						this.Pathfinding.speed = 5.0f;

						//this.StudentManager.Portal.SetActive(false);
						this.Yandere.Pursuer = this;
						this.Yandere.Chased = true;
						this.TargetDistance = 1;

						this.StudentManager.UpdateStudents();

						this.Routine = false;
						this.Fleeing = true;
					}
				}
			}
			else
			{
				Debug.Log("A delinquent has reached the ''Flee'' protocol.");

				if (this.FoundFriendCorpse)
				{
					this.Subtitle.Speaker = this;
					this.Subtitle.UpdateLabel(SubtitleType.DelinquentFriendFlee, 1, 3.0f);
				}
				else
				{
					this.Subtitle.Speaker = this;
					this.Subtitle.UpdateLabel(SubtitleType.DelinquentFlee, 1, 3.0f);
				}

				this.Pathfinding.target = this.StudentManager.Exit;
				this.Pathfinding.canSearch = true;
				this.Pathfinding.canMove = true;

				this.TargetDistance = 0.0f;
				this.Routine = false;
				this.Fleeing = true;
			}
		}
		// Teacher.
		else if (this.Persona == PersonaType.Strict)
		{
			if (this.Yandere.Pursuer == this)
			{
				Debug.Log("This teacher is now pursuing Yandere-chan.");
			}

			if (this.WitnessedMurder)
			{
				if (this.Yandere.Pursuer == this)
				{
					Debug.Log("A teacher is now reacting to the sight of murder.");

					//this.DetermineWhatWasWitnessed();
					//this.DetermineTeacherSubtitle();

					this.Subtitle.UpdateLabel(SubtitleType.TeacherMurderReaction, 3, 3.0f);

					this.Pathfinding.target = this.Yandere.transform;
					this.Pathfinding.speed = 5.0f;

					//this.StudentManager.Portal.SetActive(false);
					this.Yandere.Chased = true;
					this.TargetDistance = 1.00f;

					this.StudentManager.UpdateStudents();

					this.transform.position = new Vector3(
						this.transform.position.x,
						this.transform.position.y + 0.10f,
						this.transform.position.z);

					this.Routine = false;
					this.Fleeing = true;
				}
				else
				{
					if (!this.Yandere.Chased)
					{
						if (this.Yandere.FightHasBrokenUp)
						{
							Debug.Log("This teacher is returning to normal after witnessing a SC member break up a fight.");

							this.WitnessedMurder = false;
							this.PinDownWitness = false;
							this.Alarmed = false;
							this.Reacted = false;
							this.Routine = true;
							this.Grudge = false;
							this.AlarmTimer = 0;
							this.PreviousEyeShrink = 0;
							this.EyeShrink = 0;
							this.PreviousAlarm = 0;
							this.MurdersWitnessed = 0;
							this.Concern = 0;
							this.Witnessed = StudentWitnessType.None;
							this.GameOverCause = GameOverType.None;

							this.CurrentDestination = this.Destinations[this.Phase];
							this.Pathfinding.target = this.Destinations[this.Phase];
						}
						else
						{
							Debug.Log ("A teacher has reached ChaseYandere through PersonaReaction.");

							this.ChaseYandere();
						}
					}
				}
			}
			else if (this.WitnessedCorpse)
			{
				Debug.Log("A teacher is now reacting to the sight of a corpse.");

				if (this.ReportPhase == 0)
				{
					this.Subtitle.UpdateLabel(SubtitleType.TeacherCorpseReaction, 1, 3.0f);
				}

				this.Pathfinding.target = Instantiate(this.EmptyGameObject, new Vector3(
					this.Corpse.AllColliders[0].transform.position.x,
					this.transform.position.y,
					this.Corpse.AllColliders[0].transform.position.z),
					Quaternion.identity).transform;

				this.Pathfinding.target.position = Vector3.MoveTowards(this.Pathfinding.target.position, transform.position, 1.5f);

				this.TargetDistance = 1.0f;
				this.ReportPhase = 2;

				this.transform.position = new Vector3(
					this.transform.position.x,
					this.transform.position.y + 0.10f,
					this.transform.position.z);

				this.IgnoringPettyActions = true;
				this.Routine = false;
				this.Fleeing = true;
			}
			else
			{
				Debug.Log("A teacher is now reacting to the sight of a severed limb, blood pool, or weapon.");

				if (this.ReportPhase == 0)
				{
					if (this.WitnessedBloodPool || this.WitnessedBloodyWeapon)
					{
						this.Subtitle.UpdateLabel(SubtitleType.TeacherCorpseInspection, 3, 3.0f);
					}
					else if (this.WitnessedLimb)
					{
						this.Subtitle.UpdateLabel(SubtitleType.TeacherCorpseInspection, 4, 3.0f);
					}
					else if (this.WitnessedWeapon)
					{
						this.Subtitle.UpdateLabel(SubtitleType.TeacherCorpseInspection, 5, 3.0f);
					}
				}

				this.TargetDistance = 1.0f;
				this.ReportPhase = 2;

				this.VerballyReacted = true;
				this.Routine = false;
				this.Fleeing = true;
				this.Halt = true;
			}
		}

		if (this.StudentID == 41)
		{
			this.Subtitle.UpdateLabel(SubtitleType.Impatience, 6, 5.0f);
		}

		Debug.Log(this.Name + " has finished calling PersonaReaction(). As of now, they are a: " + this.Persona + ".");

		this.Alarm = 0;
		this.UpdateDetectionMarker();
	}

	void BeginStruggle()
	{
		//this.SpawnAlarmDisc();

		Debug.Log(this.Name + " has begun a struggle with Yandere-chan.");

		if (this.Yandere.Dragging)
		{
			this.Yandere.Ragdoll.GetComponent<RagdollScript>().StopDragging();
		}

		if (this.Yandere.Armed)
		{
			this.Yandere.EquippedWeapon.transform.localEulerAngles =
				new Vector3(0.0f, 180.0f, 0.0f);
		}

		this.Yandere.StruggleBar.Strength = this.Strength;
		this.Yandere.StruggleBar.Struggling = true;
		this.Yandere.StruggleBar.Student = this;

		// [af] Added "gameObject" for C# compatibility.
		this.Yandere.StruggleBar.gameObject.SetActive(true);

		this.CharacterAnimation.CrossFade(this.StruggleAnim);

		this.Yandere.ShoulderCamera.LastPosition = this.Yandere.ShoulderCamera.transform.position;
		this.Yandere.ShoulderCamera.Struggle = true;
		this.Pathfinding.canSearch = false;
		this.Pathfinding.canMove = false;
		this.Obstacle.enabled = true;
		this.Struggling = true;
		this.DiscCheck = false;
		this.Alarmed = false;
		this.Halt = true;
		
		if (!this.Teacher)
		{
			this.Yandere.CharacterAnimation[AnimNames.FemaleStruggleA].time = 0.0f;
		}
		else
		{
			this.Yandere.CharacterAnimation[AnimNames.FemaleTeacherStruggleA].time = 0.0f;
			this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		}

		if (this.Yandere.Aiming)
		{
			this.Yandere.StopAiming();
		}

		this.Yandere.StopLaughing();
		this.Yandere.TargetStudent = this;
		this.Yandere.Obscurance.enabled = false;
		this.Yandere.YandereVision = false;
		this.Yandere.NearSenpai = false;
		this.Yandere.Struggling = true;
		this.Yandere.CanMove = false;

		if (this.Yandere.DelinquentFighting)
		{
			this.StudentManager.CombatMinigame.Stop();
		}

		this.Yandere.EmptyHands();

        //Enbling this code means that teachers won't notice
        //Yandere -chan attacking in the faculty room.
		//this.Yandere.MyController.enabled = false;

		this.Yandere.RPGCamera.enabled = false;
		this.MyController.radius = 0.0f;

		// [af] Commented in JS code.
		//MyCollider.radius = 0;

		this.TargetDistance = 100.0f;
		this.AlarmTimer = 0.0f;

		this.SpawnAlarmDisc();
	}

	public void GetDestinations()
	{
		if (this.StudentID == 10)
		{
			//Debug.Log(this.Name + " is getting their destinations now.");
		}

		if (!this.Teacher)
		{
			this.MyLocker = this.StudentManager.LockerPositions[this.StudentID];
		}

		// [af] Commented in JS code.
		//DestinationNames = JSON.StudentDestinations[StudentID];
		//ActionNames = JSON.StudentActions[StudentID];

		if (this.Slave)
		{
			foreach (ScheduleBlock block in this.ScheduleBlocks)
			{
				block.destination = "Slave";
				block.action = "Slave";
			}
		}

		// [af] Converted while loop to for loop.
		for (this.ID = 1; this.ID < this.JSON.Students[this.StudentID].ScheduleBlocks.Length; this.ID++)
		{
			ScheduleBlock block = this.ScheduleBlocks[this.ID];

			// [af] Decide the associated destination first.
			if (block.destination == "Locker")
			{
				this.Destinations[this.ID] = this.MyLocker;//Debug.Log("Ordered to go to my locker?");}
			}
			else if (block.destination == "Seat")
			{
				this.Destinations[this.ID] = this.Seat;
			}
			else if (block.destination == "SocialSeat")
			{
				this.Destinations[this.ID] = this.StudentManager.SocialSeats[this.StudentID];
			}
			else if (block.destination == "Podium")
			{
				this.Destinations[this.ID] = this.StudentManager.Podiums.List[this.Class];
			}
			else if (block.destination == "Exit")
			{
				this.Destinations[this.ID] = this.StudentManager.Hangouts.List[0];
			}
			else if (block.destination == "Hangout")
			{
				//Debug.Log("My number is " + this.StudentID);	

				this.Destinations[this.ID] = this.StudentManager.Hangouts.List[this.StudentID];
			}
			else if (block.destination == "LunchSpot")
			{
				this.Destinations[this.ID] = this.StudentManager.LunchSpots.List[this.StudentID];

				//Debug.Log("Student " + this.StudentID + " was just told to go to a lunch spot.");
			}
			else if (block.destination == "Slave")
			{
				if (!this.FragileSlave)
				{
					this.Destinations[this.ID] = this.StudentManager.SlaveSpot;
				}
				else
				{
					this.Destinations[this.ID] = this.StudentManager.FragileSlaveSpot;
				}
			}
			else if (block.destination == "Patrol")
			{
                Debug.Log("While initially forming list of destinations, Student is setting destination to 0.");

				this.Destinations[this.ID] = this.StudentManager.Patrols.List[this.StudentID].GetChild(0);

				if (this.OriginalClub == ClubType.Gardening || this.OriginalClub == ClubType.Occult)
				{
					if (this.Club == ClubType.None)
					{
						Debug.Log("This student's club disbanded, so their destination has been set to ''Hangout''.");

						this.Destinations[this.ID] = this.StudentManager.Hangouts.List[this.StudentID];
					}
				}
			}
			else if (block.destination == "Search Patrol")
			{
				this.StudentManager.SearchPatrols.List[this.Class].GetChild(0).position = this.Seat.position + this.Seat.forward;
				this.StudentManager.SearchPatrols.List[this.Class].GetChild(0).LookAt(this.Seat);

				this.StudentManager.StolenPhoneSpot.transform.position = this.Seat.position + (this.Seat.forward * .4f);
				this.StudentManager.StolenPhoneSpot.transform.position += Vector3.up;
				this.StudentManager.StolenPhoneSpot.gameObject.SetActive(true);

				this.Destinations[this.ID] = this.StudentManager.SearchPatrols.List[this.Class].GetChild(0);
			}
			else if (block.destination == "Graffiti")
			{
				this.Destinations[this.ID] = this.StudentManager.GraffitiSpots[this.BullyID];
			}
			else if (block.destination == "Bully")
			{
				this.Destinations[this.ID] = this.StudentManager.BullySpots[this.BullyID];
			}
			else if (block.destination == "Mourn")
			{
				this.Destinations[this.ID] = this.StudentManager.MournSpots[this.StudentID];
			}
			else if (block.destination == "Clean")
			{
				this.Destinations[this.ID] = this.CleaningSpot.GetChild(0);
			}
			else if (block.destination == "ShameSpot")
			{
				this.Destinations[this.ID] = this.StudentManager.ShameSpot;
			}
			else if (block.destination == "Follow")
			{
				//Debug.Log("Currently setting destination for block #" + this.ID);

				//Follow Osana
				this.Destinations[this.ID] = this.StudentManager.Students[11].transform;
			}
			else if (block.destination == "Cuddle")
			{
				if (!this.Male)
				{
					this.Destinations[this.ID] = this.StudentManager.FemaleCoupleSpot;
				}
				else
				{
					this.Destinations[this.ID] = this.StudentManager.MaleCoupleSpot;
				}
			}
			else if (block.destination == "Peek")
			{
				if (!this.Male)
				{
					this.Destinations[this.ID] = this.StudentManager.FemaleStalkSpot;
				}
				else
				{
					this.Destinations[this.ID] = this.StudentManager.MaleStalkSpot;
				}
			}
			else if (block.destination == "Club")
			{
				if (this.Club > ClubType.None)
				{
					if (this.Club == ClubType.Sports)
					{
						this.Destinations[this.ID] = this.StudentManager.Clubs.List[this.StudentID].GetChild(0);
					}
					else
					{
						this.Destinations[this.ID] = this.StudentManager.Clubs.List[this.StudentID];
					}
				}
				else
				{
					this.Destinations[this.ID] = this.StudentManager.Hangouts.List[this.StudentID];
				}
			}
			else if (block.destination == "Sulk")
			{
				this.Destinations[this.ID] = this.StudentManager.SulkSpots[this.StudentID];
			}
			else if (block.destination == "Sleuth")
			{
				this.Destinations[this.ID] = this.SleuthTarget;
			}
			else if (block.destination == "Stalk")
			{
				this.Destinations[this.ID] = this.StalkTarget;
			}
			else if (block.destination == "Sunbathe")
			{
				this.Destinations[this.ID] = this.StudentManager.StrippingPositions[this.GirlID];
			}
			else if (block.destination == "Shock")
			{
				this.Destinations[this.ID] = this.StudentManager.ShockedSpots[this.StudentID - 80];
			}
			else if (block.destination == "Miyuki")
			{
				//If the Gaming Club closes, ClubMemberID never gets established,
				//but the students still want to play the Miyuki AR game.
				this.ClubMemberID = this.StudentID - 35;
				this.Destinations[this.ID] = this.StudentManager.MiyukiSpots[this.ClubMemberID].transform;
			}
			else if (block.destination == "Practice")
			{
				if (this.Club > ClubType.None)
				{
					this.Destinations[this.ID] = this.StudentManager.PracticeSpots[this.ClubMemberID];
				}
				else
				{
					this.Destinations[this.ID] = this.StudentManager.Hangouts.List[this.StudentID];
				}
			}
			else if (block.destination == "Lyrics")
			{
				this.Destinations[this.ID] = this.StudentManager.LyricsSpot;
			}
			else if (block.destination == "Meeting")
			{
				this.Destinations[this.ID] = this.StudentManager.MeetingSpots[this.StudentID].transform;
			}
			else if (block.destination == "InfirmaryBed")
			{
				if (this.StudentManager.SedatedStudents == 0)
				{
					this.Destinations[this.ID] = this.StudentManager.MaleRestSpot;
					this.StudentManager.SedatedStudents++;
				}
				else
				{
					this.Destinations[this.ID] = this.StudentManager.FemaleRestSpot;
				}
			}
			else if (block.destination == "InfirmarySeat")
			{
				this.Destinations[this.ID] = this.StudentManager.InfirmarySeat;
			}
			else if (block.destination == "Paint")
			{
				this.Destinations[this.ID] = this.StudentManager.FridaySpots[this.StudentID];
			}

			///////////////////
			///// ACTIONS /////
			///////////////////

			// [af] Now decide the associated action.
			if (block.action == "Stand")
			{
				this.Actions[this.ID] = StudentActionType.AtLocker;
			}
			else if (block.action == "Socialize")
			{
				this.Actions[this.ID] = StudentActionType.Socializing;
			}
			else if (block.action == "Game")
			{
				this.Actions[this.ID] = StudentActionType.Gaming;
			}
			else if (block.action == "Slave")
			{
				this.Actions[this.ID] = StudentActionType.Slave;
			}
			else if (block.action == "Relax")
			{
				this.Actions[this.ID] = StudentActionType.Relax;
			}
			else if (block.action == "Sit")
			{
				this.Actions[this.ID] = StudentActionType.SitAndTakeNotes;
			}
			else if (block.action == "Peek")
			{
				this.Actions[this.ID] = StudentActionType.Peek;
			}
			else if (block.action == "SocialSit")
			{
				this.Actions[this.ID] = StudentActionType.SitAndSocialize;

				if (this.Persona == PersonaType.Sleuth && this.Club == ClubType.None)
				{
					this.Actions[this.ID] = StudentActionType.Socializing;
				}
			}
			else if (block.action == "Eat")
			{
				this.Actions[this.ID] = StudentActionType.SitAndEatBento;
			}
			else if (block.action == "Shoes")
			{
				this.Actions[this.ID] = StudentActionType.ChangeShoes;
			}
			else if (block.action == "Grade")
			{
				this.Actions[this.ID] = StudentActionType.GradePapers;
			}
			else if (block.action == "Patrol")
			{
				this.Actions[this.ID] = StudentActionType.Patrol;

				if (this.OriginalClub == ClubType.Occult && this.Club == ClubType.None)
				{
					Debug.Log("This student's club disbanded, so their action has been set to ''Socialize''.");

					this.Actions[this.ID] = StudentActionType.Socializing;
				}
			}
			else if (block.action == "Search Patrol")
			{
				this.Actions[this.ID] = StudentActionType.SearchPatrol;
			}
			else if (block.action == "Gossip")
			{
				this.Actions[this.ID] = StudentActionType.Gossip;
			}
			else if (block.action == "Graffiti")
			{
				this.Actions[this.ID] = StudentActionType.Graffiti;
			}
			else if (block.action == "Bully")
			{
				this.Actions[this.ID] = StudentActionType.Bully;
			}
			else if (block.action == "Read")
			{
				this.Actions[this.ID] = StudentActionType.Read;
			}
			else if (block.action == "Text")
			{
				this.Actions[this.ID] = StudentActionType.Texting;
			}
			else if (block.action == "Mourn")
			{
				this.Actions[this.ID] = StudentActionType.Mourn;
			}
			else if (block.action == "Cuddle")
			{
				this.Actions[this.ID] = StudentActionType.Cuddle;
			}
			else if (block.action == "Teach")
			{
				this.Actions[this.ID] = StudentActionType.Teaching;
			}
			else if (block.action == "Wait")
			{
				this.Actions[this.ID] = StudentActionType.Wait;
			}
			else if (block.action == "Clean")
			{
				this.Actions[this.ID] = StudentActionType.Clean;
			}
			else if (block.action == "Shamed")
			{
				this.Actions[this.ID] = StudentActionType.Shamed;
			}
			else if (block.action == "Follow")
			{
				this.Actions[this.ID] = StudentActionType.Follow;
			}
			else if (block.action == "Sulk")
			{
				this.Actions[this.ID] = StudentActionType.Sulk;
			}
			else if (block.action == "Sleuth")
			{
				this.Actions[this.ID] = StudentActionType.Sleuth;
			}
			else if (block.action == "Stalk")
			{
				this.Actions[this.ID] = StudentActionType.Stalk;
			}
			else if (block.action == "Sketch")
			{
				this.Actions[this.ID] = StudentActionType.Sketch;
			}
			else if (block.action == "Sunbathe")
			{
				this.Actions[this.ID] = StudentActionType.Sunbathe;
			}
			else if (block.action == "Shock")
			{
				this.Actions[this.ID] = StudentActionType.Shock;
			}
			else if (block.action == "Miyuki")
			{
				this.Actions[this.ID] = StudentActionType.Miyuki;
			}
			else if (block.action == "Meeting")
			{
				this.Actions[this.ID] = StudentActionType.Meeting;
			}
			else if (block.action == "Lyrics")
			{
				this.Actions[this.ID] = StudentActionType.Lyrics;
			}
			else if (block.action == "Practice")
			{
				this.Actions[this.ID] = StudentActionType.Practice;
			}
			else if (block.action == "Sew")
			{
				this.Actions[this.ID] = StudentActionType.Sew;
			}
			else if (block.action == "Paint")
			{
				this.Actions[this.ID] = StudentActionType.Paint;
			}
			else if (block.action == "Club")
			{
				if (this.Club > ClubType.None)
				{
					this.Actions[this.ID] = StudentActionType.ClubAction;
				}
				else
				{
					if (this.OriginalClub == ClubType.Art)
					{
						this.Actions[this.ID] = StudentActionType.Sketch;
					}
					else
					{
						this.Actions[this.ID] = StudentActionType.Socializing;
					}
				}
			}
		}

		//Oka's stalking code.

		/*
		if (this.StudentID == 13)
		{
			if (ClubGlobals.GetClubClosed(ClubType.Occult))
			{
				if (StudentGlobals.GetStudentDead(2) && StudentGlobals.GetStudentDead(3))
				{
					this.Destinations[2] = this.StudentManager.Hangouts.List[this.StudentID];
					this.Actions[2] = StudentActionType.Socializing;
				}
			}
		}
		*/
	}

	void UpdateOutlines()
	{
		// [af] Converted while loop to for loop.
		for (this.ID = 0; this.ID < this.Outlines.Length; this.ID++)
		{
			if (this.Outlines[this.ID] != null)
			{
				this.Outlines[this.ID].color = new Color(1.0f, 0.50f, 0.0f, 1.0f);
				this.Outlines[this.ID].enabled = true;
			}
		}
	}

	public void PickRandomAnim()
	{
		if (this.Grudge)
		{
			this.RandomAnim = this.BulliedIdleAnim;
		}
		else
		{
			if (this.Club != ClubType.Delinquent)
			{
				this.RandomAnim = this.AnimationNames[Random.Range(0, this.AnimationNames.Length)];
			}
			else
			{
				this.RandomAnim = this.DelinquentAnims[Random.Range(0, this.DelinquentAnims.Length)];
			}

			/*
			if (!this.InEvent && this.Actions[this.Phase] == StudentActionType.Socializing)
			{
				if (this.DistanceToPlayer < 3.0f)
				{
					//Learning about socializing
					if (!ConversationGlobals.GetTopicDiscovered(20))
					{
						this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
						ConversationGlobals.SetTopicDiscovered(11, true);
					}

					if (!ConversationGlobals.GetTopicLearnedByStudent(11, this.StudentID))
					{
						this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
						ConversationGlobals.SetTopicLearnedByStudent(11, this.StudentID, true);
					}
				}
			}
			*/
		}
	}

	void PickRandomGossipAnim()
	{
		if (this.Grudge)
		{
			this.RandomAnim = this.BulliedIdleAnim;
		}
		else
		{
			this.RandomGossipAnim = this.GossipAnims[Random.Range(0, this.GossipAnims.Length)];

			if (this.Actions[this.Phase] == StudentActionType.Gossip)
			{
				if (this.DistanceToPlayer < 3.0f)
				{
					if (!ConversationGlobals.GetTopicDiscovered(19))
					{
						this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
						ConversationGlobals.SetTopicDiscovered(19, true);
					}

					if (!ConversationGlobals.GetTopicLearnedByStudent(19, this.StudentID))
					{
						//How does this student feel about gossip?
						this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
						ConversationGlobals.SetTopicLearnedByStudent(19, this.StudentID, true);
					}
				}
			}
		}
	}

	void PickRandomSleuthAnim()
	{
		if (!this.Sleuthing)
		{
			this.RandomSleuthAnim = this.SleuthAnims[Random.Range(0, 3)];
		}
		else
		{
			this.RandomSleuthAnim = this.SleuthAnims[Random.Range(3, 6)];
		}

		//Eventually, other stuff might go here, maybe.
	}

	public LowPolyStudentScript LowPoly;

	void BecomeTeacher()
	{
		this.transform.localScale = new Vector3(1.10f, 1.10f, 1.10f);
		this.StudentManager.Teachers[this.Class] = this;

		if (this.Class != 1)
		{
			this.GradingPaper = this.StudentManager.FacultyDesks[this.Class];
			this.GradingPaper.LeftHand = this.LeftHand.parent;
			this.GradingPaper.Character = this.Character;
			this.GradingPaper.Teacher = this;

			this.SkirtCollider.gameObject.SetActive(false);
			this.LowPoly.MyMesh = this.LowPoly.TeacherMesh;
			this.PantyCollider.enabled = false;
		}

		if (this.Class > 1)
		{
			this.VisionDistance = 12.0f * this.Paranoia;
			this.name = "Teacher_" + this.Class.ToString();

			this.OriginalIdleAnim = "f02_idleShort_00";
			this.IdleAnim = "f02_idleShort_00";
		}
		else if (this.Class == 1)
		{
			this.VisionDistance = 12.0f * this.Paranoia;
			this.PatrolAnim = AnimNames.FemaleIdle;
			this.name = "Nurse";
		}
		else
		{
			this.VisionDistance = 16.0f * this.Paranoia;
			this.PatrolAnim = AnimNames.FemaleStretch;
			this.name = "Coach";

			this.OriginalIdleAnim = AnimNames.FemaleTsunIdle;
			this.IdleAnim = AnimNames.FemaleTsunIdle;
		}

		this.StruggleAnim = AnimNames.FemaleTeacherStruggleB;
		this.StruggleWonAnim = AnimNames.FemaleTeacherStruggleWinB;
		this.StruggleLostAnim = AnimNames.FemaleTeacherStruggleLoseB;

		this.OriginallyTeacher = true;
		this.Spawned = true;
		this.Teacher = true;

		gameObject.tag = "Untagged";
	}

	public void RemoveShoes()
	{
		if (!this.Male)
		{
			this.MyRenderer.materials[0].mainTexture = this.Cosmetic.SocksTexture;
			this.MyRenderer.materials[1].mainTexture = this.Cosmetic.SocksTexture;
		}
		else
		{
			this.MyRenderer.materials[this.Cosmetic.UniformID].mainTexture = this.Cosmetic.SocksTexture;
		}
	}

	public void BecomeRagdoll()
	{
		if (this.BloodPool != null)
		{
			PromptScript BloodPrompt = this.BloodPool.GetComponent<PromptScript>();

			if (BloodPrompt != null)
			{
				Debug.Log("Re-enabling an object's prompt.");

				BloodPrompt.enabled = true;
			}
		}

		this.Meeting = false;

		if (this.StudentID == this.StudentManager.RivalID)
		{
			this.StudentManager.RivalEliminated = true;
		}

		if (this.LabcoatAttacher.newRenderer != null){this.LabcoatAttacher.newRenderer.updateWhenOffscreen = true;}
		if (this.ApronAttacher.newRenderer != null){this.ApronAttacher.newRenderer.updateWhenOffscreen = true;}
		if (this.Attacher.newRenderer != null){this.Attacher.newRenderer.updateWhenOffscreen = true;}

		if (this.DrinkingFountain != null)
		{
			this.DrinkingFountain.Occupied = false;
		}

		if (!this.Ragdoll.enabled)
		{
			this.EmptyHands();

			if (this.Broken != null)
			{
				this.Broken.enabled = false;
				this.Broken.MyAudio.Stop();
			}

			if (this.Club == ClubType.Delinquent)
			{
				if (MyWeapon != null)
				{
					this.MyWeapon.transform.parent = null;
					this.MyWeapon.MyCollider.enabled = true;
					this.MyWeapon.Prompt.enabled = true;

					Rigidbody rigidBody = this.MyWeapon.GetComponent<Rigidbody>();
					rigidBody.constraints = RigidbodyConstraints.None;
					rigidBody.isKinematic = false;
					rigidBody.useGravity = true;

					this.MyWeapon = null;
				}
			}

			if (this.StudentManager.ChaseCamera == this.ChaseCamera)
			{
				this.StudentManager.ChaseCamera = null;
			}

			this.Countdown.gameObject.SetActive(false);
			this.ChaseCamera.SetActive(false);

			if (this.Club == ClubType.Council)
			{
				Police.CouncilDeath = true;
			}

			if (this.WillChase)
			{
				this.Yandere.Chasers--;	
			}

			/*
			int ColliderID = 0;

			while (ColliderID < this.Ragdoll.AllColliders.Length)
			{
				int LimbID = 0;

				while (LimbID < this.Ragdoll.AllColliders.Length)
				{
					Physics.IgnoreCollision(this.Ragdoll.AllColliders[ColliderID], this.Ragdoll.AllColliders[LimbID]);
					LimbID++;
				}

				ColliderID++;
			}
			*/

			// [af] This is the (unintuitive) Unity 5.3 way of changing emission.
			ParticleSystem.EmissionModule heartsEmission = this.Hearts.emission;

			if (this.Following)
			{
                heartsEmission.enabled = false;

                this.FollowCountdown.gameObject.SetActive(false);
                this.Yandere.Follower = null;
                this.Yandere.Followers--;
				this.Following = false;
			}

			if (this == this.StudentManager.Reporter)
			{
				this.StudentManager.Reporter = null;
			}

			if (this.Pushed)
			{
				this.Police.SuicideStudent = this.gameObject;
				this.Police.SuicideScene = true;
				this.Ragdoll.Suicide = true;
				this.Police.Suicide = true;
			}

			if (!this.Tranquil)
			{
				StudentGlobals.SetStudentDying(this.StudentID, true);

				//Debug.Log("The character with the ID of " + StudentID + " has just died.");

				if (!this.Ragdoll.Burning && !this.Ragdoll.Disturbing)
				{
					if (this.Police.Corpses < this.Police.CorpseList.Length)
					{
						this.Police.CorpseList[this.Police.Corpses] = this.Ragdoll;
					}

					this.Police.Corpses++;
				}
			}

			if (!this.Male)
			{
				this.LiquidProjector.ignoreLayers = ~(1 << 11);

				this.RightHandCollider.enabled = false;
				this.LeftHandCollider.enabled = false;
				this.PantyCollider.enabled = false;
				this.SkirtCollider.gameObject.SetActive(false);
			}

			this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
			this.Ragdoll.AllColliders[10].isTrigger = false;
			this.NotFaceCollider.enabled = false;
			this.FaceCollider.enabled = false;
			this.MyController.enabled = false;
			heartsEmission.enabled = false;
			this.SpeechLines.Stop();

			if (this.MyRenderer.enabled)
			{
				this.MyRenderer.updateWhenOffscreen = true;
			}

			//Used if we switch from Controllers to Colliders one day in the future.
			//MyCollider.enabled = false;

			this.Pathfinding.enabled = false;
			this.HipCollider.enabled = true;
			this.enabled = false;
			this.UnWet();

			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.Prompt.Hide();

			this.Ragdoll.CharacterAnimation = this.CharacterAnimation;
			this.Ragdoll.DetectionMarker = this.DetectionMarker;
			this.Ragdoll.RightEyeOrigin = this.RightEyeOrigin;
			this.Ragdoll.LeftEyeOrigin = this.LeftEyeOrigin;
			this.Ragdoll.Electrocuted = this.Electrocuted;
            this.Ragdoll.NeckSnapped = this.NeckSnapped;
            this.Ragdoll.BreastSize = this.BreastSize;
			this.Ragdoll.EyeShrink = this.EyeShrink;
			this.Ragdoll.StudentID = this.StudentID;
			this.Ragdoll.Tranquil = this.Tranquil;
            this.Ragdoll.Burning = this.Burning;
			this.Ragdoll.Drowned = this.Drowned;
			this.Ragdoll.Yandere = this.Yandere;
			this.Ragdoll.Police = this.Police;
			this.Ragdoll.Pushed = this.Pushed;
			this.Ragdoll.Male = this.Male;
			this.Police.Deaths++;

			this.Ragdoll.enabled = true;

			this.Reputation.PendingRep -= this.PendingRep;

			if (this.WitnessedMurder)
			{
				if (this.Persona != PersonaType.Evil)
				{
					this.Police.Witnesses--;
				}
			}

			this.UpdateOutlines();

			if (this.DetectionMarker != null)
			{
				this.DetectionMarker.Tex.enabled = false;
			}

			GameObjectUtils.SetLayerRecursively(this.gameObject, 11);
			this.tag = "Blood";

			this.LowPoly.transform.parent = this.Hips;
			this.LowPoly.transform.localPosition = new Vector3(0, -1, 0);
			this.LowPoly.transform.localEulerAngles = new Vector3(0, 0, 0);
		}

		if (this.SmartPhone.transform.parent == this.ItemParent)
		{
			this.SmartPhone.SetActive(false);
		}
	}

	public void GetWet()
	{
		//If we're waiting for Osana to get splashed with water...
		if (SchemeGlobals.GetSchemeStage(1) == 3)
		{
			if (this.StudentID == this.StudentManager.RivalID)
			{
				SchemeGlobals.SetSchemeStage(1, 4);
				this.Yandere.PauseScreen.Schemes.UpdateInstructions();
			}
		}

		this.TargetDistance = 1;

		this.BeenSplashed = true;

		this.BatheFast = true;

		this.LiquidProjector.enabled = true;

		this.Emetic = false;
		this.Sedated = false;
		this.Headache = false;

		if (this.Gas)
		{
			this.LiquidProjector.material.mainTexture = this.GasTexture;
		}
		else if (this.Bloody)
		{
			this.LiquidProjector.material.mainTexture = this.BloodTexture;
		}
		else
		{
			this.LiquidProjector.material.mainTexture = this.WaterTexture;
		}

		// [af] Converted while loop to for loop.
		for (this.ID = 0; this.ID < this.LiquidEmitters.Length; this.ID++)
		{
			ParticleSystem liquidEmitter = this.LiquidEmitters[this.ID];
			liquidEmitter.gameObject.SetActive(true);

			// [af] This is the (unintuitive) Unity 5.3 way of changing particles.
			ParticleSystem.MainModule mainLiquidEmitter = liquidEmitter.main;

			if (this.Gas)
			{
				mainLiquidEmitter.startColor = new Color(1.0f, 1.0f, 0.0f, 1.0f);
			}
			else if (this.Bloody)
			{
				mainLiquidEmitter.startColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);
			}
			else
			{
				mainLiquidEmitter.startColor = new Color(0.0f, 1.0f, 1.0f, 1.0f);
			}
		}

		if (!this.Slave)
		{
			this.CharacterAnimation[this.SplashedAnim].speed = 1.0f;
			this.CharacterAnimation.CrossFade(this.SplashedAnim);

			this.Subtitle.UpdateLabel(this.SplashSubtitleType, 0, 1.0f);
			this.SpeechLines.Stop();
			this.Hearts.Stop();

			this.StopMeeting();

			this.Pathfinding.canSearch = false;
			this.Pathfinding.canMove = false;

			this.SplashTimer = 0.0f;
			this.SplashPhase = 1;
			this.BathePhase = 1;

			this.ForgetRadio();

			if (this.Distracting)
			{
				this.DistractionTarget.TargetedForDistraction = false;
				this.DistractionTarget.Octodog.SetActive(false);
				this.DistractionTarget.Distracted = false;
				this.Distracting = false;
				this.CanTalk = true;
			}

			if (this.Investigating)
			{
				this.Investigating = false;
			}

			this.SchoolwearUnavailable = true;
			this.Distracted = true;
			this.Splashed = true;
			this.Routine = false;
			this.Wet = true;

			if (this.Following)
			{
				this.FollowCountdown.gameObject.SetActive(false);
                this.Yandere.Follower = null;
                this.Yandere.Followers--;
				this.Following = false;
			}

			this.SpawnAlarmDisc();

			if (this.Club == ClubType.Cooking)
			{
				this.IdleAnim = this.OriginalIdleAnim;
				this.WalkAnim = this.OriginalWalkAnim;
				this.LeanAnim = this.OriginalLeanAnim;

				this.ClubActivityPhase = 0;
				this.ClubTimer = 0;
			}

			if (this.ReturningMisplacedWeapon)
			{
				this.DropMisplacedWeapon();
			}

			this.EmptyHands();
		}
	}

	public void UnWet()
	{
		for (this.ID = 0; this.ID < this.LiquidEmitters.Length; this.ID++)
		{
			this.LiquidEmitters[this.ID].gameObject.SetActive(false);
		}
	}

	public void SetSplashes(bool Bool)
	{
		for (this.ID = 0; this.ID < this.SplashEmitters.Length; this.ID++)
		{
			this.SplashEmitters[this.ID].gameObject.SetActive(Bool);
		}
	}

	void StopMeeting()
	{
		this.Prompt.Label[0].text = "     Talk";

		this.Pathfinding.canSearch = true;
		this.Pathfinding.canMove = true;

		this.Drownable = false;
		this.Pushable = false;
		this.Meeting = false;
		this.MeetTimer = 0.0f;

		//Osana
		if (this.StudentID == 11)
		{
			this.StudentManager.OsanaOfferHelp.gameObject.SetActive(false);
			this.StudentManager.LoveManager.RivalWaiting = false;
		}
		//Kokona
		else if (this.StudentID == 30)
		{
			this.StudentManager.OfferHelp.gameObject.SetActive(false);
			this.StudentManager.LoveManager.RivalWaiting = false;
		}
		//Fragile Girl
		else if (this.StudentID == 5)
		{
			this.StudentManager.FragileOfferHelp.gameObject.SetActive(false);
		}
	}

	public void Combust()
	{
        this.Police.CorpseList[this.Police.Corpses] = this.Ragdoll;
		this.Police.Corpses++;

		GameObjectUtils.SetLayerRecursively(this.gameObject, 11);
		this.tag = "Blood";
		this.Dying = true;

		this.SpawnAlarmDisc();

		this.Character.GetComponent<Animation>().CrossFade(this.BurningAnim);
        this.CharacterAnimation[this.WetAnim].weight = 0.0f;

        this.Pathfinding.canSearch = false;
		this.Pathfinding.canMove = false;

		this.Ragdoll.BurningAnimation = true;
		this.Ragdoll.Disturbing = true;
		this.Ragdoll.Burning = true;

		this.WitnessedCorpse = false;
		this.Investigating = false;
		this.EatingSnack = false;
		this.DiscCheck = false;
		this.WalkBack = false;
		this.Alarmed = false;
		this.CanTalk = false;
		this.Fleeing = false;
		this.Routine = false;
		this.Reacted = false;
		this.Burning = true;
		this.Wet = false;

		AudioSource audioSource = this.GetComponent<AudioSource>();
		audioSource.clip = this.BurningClip;
		audioSource.Play();

		this.LiquidProjector.enabled = false;
		this.UnWet();

		if (this.Following)
		{
			this.FollowCountdown.gameObject.SetActive(false);
            this.Yandere.Follower = null;
            this.Yandere.Followers--;
			this.Following = false;
		}
					
		for (this.ID = 0; this.ID < this.FireEmitters.Length; this.ID++)
		{
			this.FireEmitters[this.ID].gameObject.SetActive(true);
		}

		if (this.Attacked)
		{
			this.BurnTarget = this.Yandere.transform.position + this.Yandere.transform.forward;

			// [af] Commented in JS code.
			//BurnTarget = Vector3(HipCollider.transform.position.x, transform.position.y, HipCollider.transform.position.z);

			this.Attacked = false;
		}
	}

	public GameObject JojoHitEffect;

	public void JojoReact()
	{
		Instantiate(this.JojoHitEffect,
			this.transform.position + new Vector3(0.0f, 1.0f, 0.0f), Quaternion.identity);

		if (!this.Dying)
		{
			this.Dying = true;

			this.SpawnAlarmDisc();

			this.CharacterAnimation.CrossFade(this.JojoReactAnim);
            this.CharacterAnimation[this.WetAnim].weight = 0.0f;

            this.Pathfinding.canSearch = false;
			this.Pathfinding.canMove = false;

			this.WitnessedCorpse = false;
			this.Investigating = false;
			this.EatingSnack = false;
			this.DiscCheck = false;
			this.WalkBack = false;
			this.Alarmed = false;
			this.CanTalk = false;
			this.Fleeing = false;
			this.Routine = false;
			this.Reacted = false;
			this.Wet = false;

			// [af] Commented in JS code.
			//audio.clip = SomeClip;

			AudioSource audioSource = this.GetComponent<AudioSource>();
			audioSource.Play();

			if (this.Following)
			{
				this.FollowCountdown.gameObject.SetActive(false);
                this.Yandere.Follower = null;
                this.Yandere.Followers--;
				this.Following = false;
			}
		}
	}

	public GameObject[] ElectroSteam;
	public GameObject[] CensorSteam;
	public Texture NudeTexture;

	public Mesh BaldNudeMesh;
	public Mesh NudeMesh;

	public Texture TowelTexture;
	public Mesh TowelMesh;

	void Nude()
	{
		if (!this.Male)
		{
			this.PantyCollider.enabled = false;
			this.SkirtCollider.gameObject.SetActive(false);
		}

		//If we're a girl...
		if (!this.Male)
		{
			//this.MyRenderer.sharedMesh = this.BaldNudeMesh;
			this.MyRenderer.sharedMesh = this.TowelMesh;

			if (this.Club == ClubType.Bully)
			{
				this.Cosmetic.DeactivateBullyAccessories();
			}

			this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);

			// Face.
			//this.MyRenderer.materials[0].mainTexture = this.Cosmetic.FaceTexture;
			this.MyRenderer.materials[0].mainTexture = this.TowelTexture;

			// Body.
			//this.MyRenderer.materials[1].mainTexture = this.NudeTexture;
			this.MyRenderer.materials[1].mainTexture = this.TowelTexture;

			// Body.
			//this.MyRenderer.materials[2].mainTexture = this.NudeTexture;
			this.MyRenderer.materials[2].mainTexture = this.Cosmetic.FaceTexture;

			//Hide panties / stockings
			this.Cosmetic.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);
		}
		//If we're a boy...
		else
		{
			this.MyRenderer.sharedMesh = this.BaldNudeMesh;

			// Body.
			this.MyRenderer.materials[0].mainTexture = this.NudeTexture;

			// Body.
			this.MyRenderer.materials[1].mainTexture = null;

			// Face.
			this.MyRenderer.materials[2].mainTexture = this.Cosmetic.FaceTextures[this.SkinColor];
		}

		this.Cosmetic.RemoveCensor();

		if (!this.AoT)
		{
			if (this.Male)
			{
				// [af] Converted while loop to for loop.
				for (this.ID = 0; this.ID < this.CensorSteam.Length; this.ID++)
				{
					this.CensorSteam[this.ID].SetActive(true);
				}
			}
		}
		else
		{
			if (!this.Male)
			{
				this.MyRenderer.sharedMesh = this.BaldNudeMesh;

				this.MyRenderer.materials[0].mainTexture = this.Cosmetic.FaceTexture;
				this.MyRenderer.materials[1].mainTexture = this.NudeTexture;
				this.MyRenderer.materials[2].mainTexture = this.NudeTexture;
			}
			else
			{
				this.MyRenderer.materials[1].mainTexture = this.Cosmetic.FaceTextures[this.SkinColor];
			}
		}
	}

	public Mesh SwimmingTrunks;
	public Mesh SchoolSwimsuit;
	public Mesh GymUniform;

	public Texture UniformTexture;
	public Texture SwimsuitTexture;
	public Texture GyaruSwimsuitTexture;
	public Texture GymTexture;

	public void ChangeSchoolwear()
	{
		// [af] Converted while loop to for loop.
		for (this.ID = 0; this.ID < this.CensorSteam.Length; this.ID++)
		{
			this.CensorSteam[this.ID].SetActive(false);
		}

		if (this.Attacher.gameObject.activeInHierarchy)
		{
			this.Attacher.RemoveAccessory();
		}

		if (this.LabcoatAttacher.enabled)
		{
			Destroy(this.LabcoatAttacher.newRenderer);
			this.LabcoatAttacher.enabled = false;
		}

		if (this.Schoolwear == 0)
		{
			this.Nude();
		}
		else if (this.Schoolwear == 1)
		{
			if (!this.Male)
			{
				this.Cosmetic.SetFemaleUniform();

				this.SkirtCollider.gameObject.SetActive(true);
				if (PantyCollider != null){this.PantyCollider.enabled = true;}

				if (this.Club == ClubType.Bully)
				{
					this.Cosmetic.RightWristband.SetActive(true);
					this.Cosmetic.LeftWristband.SetActive(true);
					this.Cosmetic.Bookbag.SetActive(true);
					this.Cosmetic.Hoodie.SetActive(true);
				}
			}
			else
			{
				this.Cosmetic.SetMaleUniform();
			}
		}
		else if (this.Schoolwear == 2)
		{
			if (this.Club == ClubType.Sports)
			{
				this.MyRenderer.sharedMesh = this.SwimmingTrunks;

				this.MyRenderer.SetBlendShapeWeight(0, 20 * (6 - this.ClubMemberID));
				this.MyRenderer.SetBlendShapeWeight(1, 20 * (6 - this.ClubMemberID));

				this.MyRenderer.materials[0].mainTexture = this.Cosmetic.Trunks[this.StudentID];
				this.MyRenderer.materials[1].mainTexture = this.Cosmetic.FaceTexture;
				this.MyRenderer.materials[2].mainTexture = this.Cosmetic.Trunks[this.StudentID];
			}
			else
			{
				this.MyRenderer.sharedMesh = this.SchoolSwimsuit;

				if (!this.Male)
				{
					if (this.Club == ClubType.Bully)
					{
						this.MyRenderer.materials[0].mainTexture = this.Cosmetic.GanguroSwimsuitTextures[this.BullyID];
						this.MyRenderer.materials[1].mainTexture = this.Cosmetic.GanguroSwimsuitTextures[this.BullyID];

						this.Cosmetic.RightWristband.SetActive(false);
						this.Cosmetic.LeftWristband.SetActive(false);
						this.Cosmetic.Bookbag.SetActive(false);
						this.Cosmetic.Hoodie.SetActive(false);
					}
					else
					{
						this.MyRenderer.materials[0].mainTexture = this.SwimsuitTexture;
						this.MyRenderer.materials[1].mainTexture = this.SwimsuitTexture;
					}

					this.MyRenderer.materials[2].mainTexture = this.Cosmetic.FaceTexture;
				}
				else
				{
					this.MyRenderer.materials[0].mainTexture = this.SwimsuitTexture;
					this.MyRenderer.materials[1].mainTexture = this.Cosmetic.FaceTexture;
					this.MyRenderer.materials[2].mainTexture = this.SwimsuitTexture;
				}
			}
		}
		else if (this.Schoolwear == 3)
		{
			this.MyRenderer.sharedMesh = this.GymUniform;

			if (!this.Male)
			{
				this.MyRenderer.materials[0].mainTexture = this.GymTexture;
				this.MyRenderer.materials[1].mainTexture = this.GymTexture;
				this.MyRenderer.materials[2].mainTexture = this.Cosmetic.FaceTexture;

				if (this.Club == ClubType.Bully)
				{
					this.Cosmetic.ActivateBullyAccessories();
				}
			}
			else
			{
				Debug.Log("A male is putting on a gym uniform.");

				this.MyRenderer.materials[0].mainTexture = this.GymTexture;
				this.MyRenderer.materials[1].mainTexture = this.Cosmetic.SkinTextures[this.Cosmetic.SkinColor];
				this.MyRenderer.materials[2].mainTexture = this.Cosmetic.FaceTexture;
			}
		}

		if (!this.Male)
		{
			// [af] Replaced if/else statement with ternary expression.
			this.Cosmetic.Stockings = (this.Schoolwear == 1) ?
				this.Cosmetic.OriginalStockings : string.Empty;

			this.StartCoroutine(this.Cosmetic.PutOnStockings());

			if (this.StudentManager.Censor)
			{
				this.Cosmetic.CensorPanties();
			}
		}

		// [af] Converted while loop to for loop (no ID initialization).
		for (; this.ID < this.Outlines.Length; this.ID++)
		{
			if (this.Outlines[this.ID] != null)
			{
				if (this.Outlines[this.ID].h != null)
				{
					this.Outlines[this.ID].h.ReinitMaterials();
				}
			}
		}

		// [af] Commented out unused JS variable.
		//CanMove = false;
	}

	public Texture TitanBodyTexture;
	public Texture TitanFaceTexture;

	public void AttackOnTitan()
	{
		this.CharacterAnimation.CrossFade(this.WalkAnim);
		this.AoT = true;

		// [af] Commented in JS code.
		//MyCollider.center.y = 0.0825;
		//MyCollider.radius = .015;
		//MyCollider.height = .15;

		this.MyController.center = new Vector3(
			this.MyController.center.x,
			0.0825f,
			this.MyController.center.z);

		this.MyController.radius = 0.015f;
		this.MyController.height = 0.15f;

		if (!this.Male)
		{
			this.Cosmetic.FaceTexture = this.TitanFaceTexture;
		}
		else
		{
			this.Cosmetic.FaceTextures[this.SkinColor] = this.TitanFaceTexture;
		}

		this.NudeTexture = this.TitanBodyTexture;

		this.Nude();

		// [af] Converted while loop to for loop.
		for (this.ID = 0; this.ID < this.Outlines.Length; this.ID++)
		{
			OutlineScript outline = this.Outlines[this.ID];

			if (outline.h == null)
			{
				outline.Awake();
			}

			outline.h.ReinitMaterials();
		}

		if (!this.Male && !this.Teacher)
		{
			this.PantyCollider.enabled = false;
			this.SkirtCollider.gameObject.SetActive(false);
		}
	}

	public bool Spooky = false;

	public void Spook()
	{
		if (!this.Male)
		{
			this.RightEye.gameObject.SetActive(false);
			this.LeftEye.gameObject.SetActive(false);

			this.MyRenderer.enabled = false;

			// [af] Converted while loop to for loop.
			for (this.ID = 0; this.ID < this.Bones.Length; this.ID++)
			{
				this.Bones[this.ID].SetActive(true);
			}
		}
	}

	void Unspook()
	{
		this.MyRenderer.enabled = true;

		// [af] Converted while loop to for loop.
		for (this.ID = 0; this.ID < this.Bones.Length; this.ID++)
		{
			this.Bones[this.ID].SetActive(false);
		}
	}

	void GoChange()
	{
		if (!this.Male)
		{
			this.CurrentDestination = this.StudentManager.StrippingPositions[this.GirlID];
			this.Pathfinding.target = this.StudentManager.StrippingPositions[this.GirlID];
		}
		else
		{
			this.CurrentDestination = this.StudentManager.MaleStripSpot;
			this.Pathfinding.target = this.StudentManager.MaleStripSpot;
		}

		this.Pathfinding.canSearch = true;
		this.Pathfinding.canMove = true;

		this.Distracted = false;
	}

	public void SpawnAlarmDisc()
	{
		//Debug.Log(name + " is spawning an Alarm Disc.");

		GameObject NewAlarmDisc = Instantiate(this.AlarmDisc,
			this.transform.position + Vector3.up, Quaternion.identity);
		NewAlarmDisc.GetComponent<AlarmDiscScript>().Male = this.Male;
		NewAlarmDisc.GetComponent<AlarmDiscScript>().Originator = this;

		if (this.Splashed)
		{
			NewAlarmDisc.GetComponent<AlarmDiscScript>().Shocking = true;
			NewAlarmDisc.GetComponent<AlarmDiscScript>().NoScream = true;
		}

		if (this.Struggling || this.Shoving || MurderSuicidePhase == 100 || this.StudentManager.CombatMinigame.Delinquent == this)
		{
			NewAlarmDisc.GetComponent<AlarmDiscScript>().NoScream = true;
		}

		if (this.Club == ClubType.Delinquent)
		{
			NewAlarmDisc.GetComponent<AlarmDiscScript>().Delinquent = true;
		}

		if (this.Dying)
		{
			if (this.Yandere.Equipped > 0)
			{
				if (this.Yandere.EquippedWeapon.WeaponID == 7)
				{
					NewAlarmDisc.GetComponent<AlarmDiscScript>().Long = true;
				}
			}
		}
	}

	public void SpawnSmallAlarmDisc()
	{
		//Debug.Log(name + " is spawning a small Alarm Disc.");

		GameObject NewAlarmDisc = Instantiate(this.AlarmDisc,
			this.transform.position + Vector3.up, Quaternion.identity);

		NewAlarmDisc.transform.localScale = new Vector3(100, 1, 100);

		NewAlarmDisc.GetComponent<AlarmDiscScript>().NoScream = true;
	}

	public Mesh JudoGiMesh;
	public Texture JudoGiTexture;
	public RiggedAccessoryAttacher Attacher;

	public void ChangeClubwear()
	{
		if (!this.ClubAttire)
		{
			this.Cosmetic.RemoveCensor();

			this.DistanceToDestination = 100;
			this.ClubAttire = true;

			if (this.Club == ClubType.Art)
			{
				if (!this.Attacher.gameObject.activeInHierarchy)
				{
					this.Attacher.gameObject.SetActive(true);
				}
				else
				{
					this.Attacher.Start();
				}
			}
			else if (this.Club == ClubType.MartialArts)
			{
				this.MyRenderer.sharedMesh = this.JudoGiMesh;

				if (!this.Male)
				{
					this.MyRenderer.materials[0].mainTexture = this.JudoGiTexture;
					this.MyRenderer.materials[1].mainTexture = this.JudoGiTexture;
					this.MyRenderer.materials[2].mainTexture = this.Cosmetic.FaceTexture;

					this.SkirtCollider.gameObject.SetActive(false);
					this.PantyCollider.enabled = false;
				}
				else
				{
					this.MyRenderer.materials[0].mainTexture = this.JudoGiTexture;
					this.MyRenderer.materials[1].mainTexture = this.Cosmetic.FaceTexture;
					this.MyRenderer.materials[2].mainTexture = this.JudoGiTexture;
				}
			}
			else if (this.Club == ClubType.Science)
			{
				this.WearLabCoat();
			}
			else if (this.Club == ClubType.Sports)
			{
				if (this.Clock.Period < 3)
				{
					this.MyRenderer.sharedMesh = this.GymUniform;

					this.MyRenderer.materials[0].mainTexture = this.GymTexture;
					this.MyRenderer.materials[1].mainTexture = this.Cosmetic.SkinTextures[this.Cosmetic.SkinID];
					this.MyRenderer.materials[2].mainTexture = this.Cosmetic.FaceTexture;
				}
				else
				{
					this.MyRenderer.sharedMesh = this.SwimmingTrunks;

					this.MyRenderer.SetBlendShapeWeight(0, 20 * (6 - this.ClubMemberID));
					this.MyRenderer.SetBlendShapeWeight(1, 20 * (6 - this.ClubMemberID));

					this.MyRenderer.materials[0].mainTexture = this.Cosmetic.Trunks[this.StudentID];
					this.MyRenderer.materials[1].mainTexture = this.Cosmetic.FaceTexture;
					this.MyRenderer.materials[2].mainTexture = this.Cosmetic.Trunks[this.StudentID];

					//this.Armband.SetActive(false);
					this.ClubAnim = "poolDive_00";
					this.ClubActivityPhase = 15;
					this.Destinations[this.Phase] = this.StudentManager.Clubs.List[this.StudentID].GetChild(this.ClubActivityPhase);
				}
			}

			if (this.StudentID == 46)
			{
				this.Armband.transform.localPosition = new Vector3(
					this.Armband.transform.localPosition.x,
					this.Armband.transform.localPosition.y,
					0.010f);

				this.Armband.transform.localScale = new Vector3(1.30f, 1.30f, 1.30f);
			}
		}
		else
		{
			this.ClubAttire = false;

			if (this.Club == ClubType.Art)
			{
				this.Attacher.RemoveAccessory();
			}
			else if (this.Club == ClubType.Science)
			{
				this.WearLabCoat();
			}
			else
			{
				this.ChangeSchoolwear();

				if (this.StudentID == 46)
				{
					this.Armband.transform.localPosition = new Vector3(
						this.Armband.transform.localPosition.x,
						this.Armband.transform.localPosition.y,
						0.012f);

					this.Armband.transform.localScale = new Vector3(1.20f, 1.20f, 1.20f);
				}
				else if (this.StudentID == 47)
				{
					this.StudentManager.ConvoManager.Confirmed = false;
					this.ClubAnim = AnimNames.MaleIdle20;
				}
				else if (this.StudentID == 49)
				{
					this.StudentManager.ConvoManager.Confirmed = false;
					this.ClubAnim = AnimNames.FemaleIdle20;
				}
			}
		}
	}

	void WearLabCoat()
	{
		if (!this.LabcoatAttacher.enabled)
		{
			this.MyRenderer.sharedMesh = HeadAndHands;
			this.LabcoatAttacher.enabled = true;

			if (!this.Male)
			{
				this.RightBreast.gameObject.name = "RightBreastRENAMED";
				this.LeftBreast.gameObject.name = "LeftBreastRENAMED";
			}

			if (this.LabcoatAttacher.Initialized)
			{
				this.LabcoatAttacher.AttachAccessory();
			}

			//If we're a girl...
			if (!this.Male)
			{
				this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);

				this.MyRenderer.materials[0].mainTexture = this.Cosmetic.FaceTexture;
				this.MyRenderer.materials[1].mainTexture = this.NudeTexture;
				this.MyRenderer.materials[2].mainTexture = null;

				//Hide panties / stockings
				this.Cosmetic.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

				this.SkirtCollider.gameObject.SetActive(false);
				this.PantyCollider.enabled = false;
			}
			//If we're a boy...
			else
			{
				this.MyRenderer.materials[0].mainTexture = this.Cosmetic.FaceTextures[this.SkinColor];
				this.MyRenderer.materials[1].mainTexture = this.NudeTexture;
				this.MyRenderer.materials[2].mainTexture = this.NudeTexture;
			}
		}
		else
		{
			if (!this.Male)
			{
				this.RightBreast.gameObject.name = "RightBreastRENAMED";
				this.LeftBreast.gameObject.name = "LeftBreastRENAMED";

				this.SkirtCollider.gameObject.SetActive(true);
				this.PantyCollider.enabled = true;
			}

			Destroy(this.LabcoatAttacher.newRenderer);
			this.LabcoatAttacher.enabled = false;
			this.ChangeSchoolwear();
		}
	}

	public Mesh NoArmsNoTorso;
	public GameObject RiggedAccessory;

	public void AttachRiggedAccessory()
	{
		this.RiggedAccessory.GetComponent<RiggedAccessoryAttacher>().ID = this.StudentID;

		if (this.Cosmetic.Accessory > 0)
		{
			this.Cosmetic.FemaleAccessories [this.Cosmetic.Accessory].SetActive (false);
		}

		if (this.StudentID == 26)
		{
			this.MyRenderer.sharedMesh = this.NoArmsNoTorso;
		}
		/*
		else if (this.Cosmetic.EyeType == "Gentle")
		{
			this.MyRenderer.sharedMesh = null;
		}
		*/

		this.RiggedAccessory.SetActive(true);
	}

	public void CameraReact()
	{
		this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;

		this.Pathfinding.canSearch = false;
		this.Pathfinding.canMove = false;
		this.Obstacle.enabled = true;
		this.CameraReacting = true;
		this.CameraReactPhase = 1;
		this.SpeechLines.Stop();
		this.Routine = false;
		this.StopPairing();

		if (!this.Sleuthing)
		{
			this.SmartPhone.SetActive(false);
		}

		this.OccultBook.SetActive(false);
		this.Scrubber.SetActive(false);
		this.Eraser.SetActive(false);
		this.Pen.SetActive(false);

		this.Pencil.SetActive(false);
		this.Sketchbook.SetActive(false);

		if (this.Club == ClubType.Gardening)
		{
			this.WateringCan.transform.parent = this.Hips;
			this.WateringCan.transform.localPosition = new Vector3(0, .0135f, -.184f);
			this.WateringCan.transform.localEulerAngles = new Vector3(0, 90, 30);
		}
		else if (this.Club == ClubType.LightMusic)
		{
			if (this.StudentID == 51)
			{
				if (this.InstrumentBag[this.ClubMemberID].transform.parent == null)
				{
					this.Instruments[this.ClubMemberID].transform.parent = null;
					this.Instruments[this.ClubMemberID].transform.position = new Vector3(-.5f, 4.5f, 22.45666f);
					this.Instruments[this.ClubMemberID].transform.eulerAngles = new Vector3(-15, 0, 0);

					this.Instruments[this.ClubMemberID].GetComponent<AudioSource>().playOnAwake = false;
					this.Instruments[this.ClubMemberID].GetComponent<AudioSource>().Stop();
				}
				else
				{
					this.Instruments[this.ClubMemberID].SetActive(false);
				}
			}
			else
			{
				this.Instruments[this.ClubMemberID].SetActive(false);
			}

			this.Drumsticks[0].SetActive(false);
			this.Drumsticks[1].SetActive(false);
		}

		foreach (GameObject prop in this.ScienceProps)
		{
			if (prop != null)
			{
				prop.SetActive(false);
			}
		}

		foreach (GameObject prop in this.Fingerfood)
		{
			if (prop != null)
			{
				prop.SetActive(false);
			}
		}

		if (!this.Yandere.ClubAccessories[7].activeInHierarchy || this.Club == ClubType.Delinquent)
		{
			this.CharacterAnimation.CrossFade(this.CameraAnims[1]);
		}
		else
		{
			if (this.Club == ClubType.Bully)
			{
				this.SmartPhone.SetActive(true);
			}

			this.CharacterAnimation.CrossFade(this.IdleAnim);
		}

		this.EmptyHands();
	}

	void LookForYandere()
	{
		if (!this.Yandere.Chased &&
			this.CanSeeObject(this.Yandere.gameObject, this.Yandere.HeadPosition))
		{
			this.ReportPhase++;
		}
	}

	public void UpdatePerception()
	{
		if (this.Yandere.Club == ClubType.Occult || this.Yandere.Class.StealthBonus > 0)
		{
			this.Perception = 0.50f;
		}
		else
		{
			this.Perception = 1.0f;
		}

		this.ChameleonCheck();

		if (Chameleon)
		{
			this.Perception *= 0.50f;
		}
	}

	public void StopInvestigating()
	{
		Debug.Log(this.Name + " was invesigating a giggle, but has stopped.");

		Giggle = null;

		if (!this.Sleuthing)
		{
			//This is only called when a character stops investigating something suspicious.

			this.CurrentDestination = this.Destinations[this.Phase];
			this.Pathfinding.target = this.Destinations[this.Phase];

			if (this.Actions[this.Phase] == StudentActionType.Sunbathe)
			{
				if (this.SunbathePhase > 1)
				{
					this.CurrentDestination = this.StudentManager.SunbatheSpots[this.StudentID];
					this.Pathfinding.target = this.StudentManager.SunbatheSpots[this.StudentID];
				}
			}
		}
		else
		{
			this.CurrentDestination = this.SleuthTarget;
			this.Pathfinding.target = this.SleuthTarget;
		}

		this.InvestigationTimer = 0.0f;
		this.InvestigationPhase = 0;

		if (!this.Hurry)
		{
			this.Pathfinding.speed = 1.0f;
		}
		else
		{
			//Debug.Log("Sprinting 17");
			this.Pathfinding.speed = 4.0f;
		}

		if (CurrentAction == StudentActionType.Clean)
		{
			this.SmartPhone.SetActive(false);
			this.Scrubber.SetActive(true);

			if (this.CleaningRole == 5)
			{
				this.Scrubber.GetComponent<Renderer>().material.mainTexture = this.Eraser.GetComponent<Renderer>().material.mainTexture;
				this.Eraser.SetActive(true);
			}
		}

		this.YandereInnocent = false;
		this.Investigating = false;
		this.EatingSnack = false;
		this.HeardScream = false;
		this.DiscCheck = false;
		this.Routine = true;
	}

	public void ForgetGiggle()
	{
		Giggle = null;

		this.InvestigationTimer = 0.0f;
		this.InvestigationPhase = 0;

		this.YandereInnocent = false;
		this.Investigating = false;
		this.DiscCheck = false;
	}

	public bool InCouple { get { return this.CoupleID > 0; } }
	public int CoupleID = 0;

	// [af] Returns whether a student's close friend or loved one is targeted by Yandere-chan.
	bool LovedOneIsTargeted(int yandereTargetID)
	{
		bool protectingLovedOne = this.InCouple && (this.CoupleID == yandereTargetID);

		bool protectingStudent2 = this.StudentID == 3 && yandereTargetID == 2;
		bool protectingStudent3 = this.StudentID == 2 && yandereTargetID == 3;

		bool protectingStudent37 = this.StudentID == 38 && yandereTargetID == 37;
		bool protectingStudent38 = this.StudentID == 37 && yandereTargetID == 38;

		bool protectingStudent26 = this.StudentID == 30 && yandereTargetID == 25;
		bool protectingStudent31 = this.StudentID == 25 && yandereTargetID == 30;

		bool protectingCrush1 = this.StudentID == 28 && yandereTargetID == 30;
		bool protectingCrush2 = this.StudentID == 6 && yandereTargetID == 11;

		bool protectingDelinquent = false;

		bool protectingSleuth = (this.StudentID > 55 && this.StudentID < 61) && (yandereTargetID > 55 && yandereTargetID < 61);

		if (this.Injured)
		{
			protectingDelinquent = (this.Club == ClubType.Delinquent) &&
			(StudentManager.Students[yandereTargetID].Club == ClubType.Delinquent);
		}

		return protectingLovedOne || protectingStudent2 || protectingStudent3 ||
			protectingStudent37 || protectingStudent38 || protectingStudent26 ||
			protectingStudent31 || protectingCrush1 || protectingCrush2 ||
			protectingDelinquent || protectingSleuth;
	}

	void Pose()
	{
		this.StudentManager.PoseMode.ChoosingAction = true;
		this.StudentManager.PoseMode.Panel.enabled = true;
		this.StudentManager.PoseMode.Student = this;
		this.StudentManager.PoseMode.UpdateLabels();
		this.StudentManager.PoseMode.Show = true;

		this.DialogueWheel.PromptBar.ClearButtons();
		this.DialogueWheel.PromptBar.Label[0].text = "Confirm";
		this.DialogueWheel.PromptBar.Label[1].text = "Back";
		this.DialogueWheel.PromptBar.Label[4].text = "Change";
		this.DialogueWheel.PromptBar.Label[5].text = "Increase/Decrease";
		this.DialogueWheel.PromptBar.UpdateButtons();
		this.DialogueWheel.PromptBar.Show = true;

		this.Yandere.Character.GetComponent<Animation>().CrossFade(this.Yandere.IdleAnim);
		this.Yandere.CanMove = false;
		this.Posing = true;
	}

	public void DisableEffects()
	{
		this.LiquidProjector.enabled = false;

		this.ElectroSteam[0].SetActive(false);
		this.ElectroSteam[1].SetActive(false);
		this.ElectroSteam[2].SetActive(false);
		this.ElectroSteam[3].SetActive(false);

		this.CensorSteam[0].SetActive(false);
		this.CensorSteam[1].SetActive(false);
		this.CensorSteam[2].SetActive(false);
		this.CensorSteam[3].SetActive(false);

		// [af] Converted while loop to foreach loop.
		foreach (ParticleSystem liquidEmitter in this.LiquidEmitters)
		{
			liquidEmitter.gameObject.SetActive(false);
		}

		// [af] Converted while loop to foreach loop.
		foreach (ParticleSystem fireEmitter in this.FireEmitters)
		{
			fireEmitter.gameObject.SetActive(false);
		}

		// [af] Converted while loop to for loop.
		for (this.ID = 0; this.ID < this.Bones.Length; this.ID++)
		{
			if (this.Bones[this.ID] != null)
			{
				this.Bones[this.ID].SetActive(false);
			}
		}

		if (this.Persona != PersonaType.PhoneAddict){this.SmartPhone.SetActive(false);}
		this.Note.SetActive(false);

		if (!this.Slave)
		{
            //this.RightEmptyEye.SetActive(false);
            //this.LeftEmptyEye.SetActive(false);
            Destroy(this.Broken);
        }
	}

	public void DetermineSenpaiReaction()
	{
		Debug.Log("We are now determining Senpai's reaction to Yandere-chan's behavior.");

		if (this.Witnessed == StudentWitnessType.WeaponAndBloodAndInsanity)
		{
			this.Subtitle.UpdateLabel(SubtitleType.SenpaiInsanityReaction, 1, 4.50f);
		}
		else if (this.Witnessed == StudentWitnessType.WeaponAndBlood)
		{
			this.Subtitle.UpdateLabel(SubtitleType.SenpaiWeaponReaction, 1, 4.50f);
		}
		else if (this.Witnessed == StudentWitnessType.WeaponAndInsanity)
		{
			this.Subtitle.UpdateLabel(SubtitleType.SenpaiInsanityReaction, 1, 4.50f);
		}
		else if (this.Witnessed == StudentWitnessType.BloodAndInsanity)
		{
			this.Subtitle.UpdateLabel(SubtitleType.SenpaiInsanityReaction, 1, 4.50f);
		}
		else if (this.Witnessed == StudentWitnessType.Weapon)
		{
			this.Subtitle.UpdateLabel(SubtitleType.SenpaiWeaponReaction, 1, 4.50f);
		}
		else if (this.Witnessed == StudentWitnessType.Blood)
		{
			this.Subtitle.UpdateLabel(SubtitleType.SenpaiBloodReaction, 1, 4.50f);
		}
		else if (this.Witnessed == StudentWitnessType.Insanity)
		{
			this.Subtitle.UpdateLabel(SubtitleType.SenpaiInsanityReaction, 1, 4.50f);
		}
		else if (this.Witnessed == StudentWitnessType.Lewd)
		{
			this.Subtitle.UpdateLabel(SubtitleType.SenpaiLewdReaction, 1, 4.50f);
		}
		else if (this.GameOverCause == GameOverType.Stalking)
		{
			this.Subtitle.UpdateLabel(SubtitleType.SenpaiStalkingReaction, this.Concern, 4.50f);
		}
		else if (this.GameOverCause == GameOverType.Murder)
		{
			this.Subtitle.UpdateLabel(SubtitleType.SenpaiMurderReaction, this.MurderReaction, 4.50f);
		}
		else if (this.GameOverCause == GameOverType.Violence)
		{
			this.Subtitle.UpdateLabel(SubtitleType.SenpaiViolenceReaction, 1, 4.50f);
		}
	}

	public void ForgetRadio()
	{
		this.TurnOffRadio = false;
		this.RadioTimer = 0.0f;
		this.RadioPhase = 1;
		this.Routine = true;
		this.Radio = null;
	}

	public void RealizePhoneIsMissing()
	{
		ScheduleBlock block2 = this.ScheduleBlocks[2];
		block2.destination = "Search Patrol";
		block2.action = "Search Patrol";

		ScheduleBlock block4 = this.ScheduleBlocks[4];
		block4.destination = "Search Patrol";
		block4.action = "Search Patrol";

		ScheduleBlock block7 = this.ScheduleBlocks[7];
		block7.destination = "Search Patrol";
		block7.action = "Search Patrol";

		this.GetDestinations();
	}

	public void TeleportToDestination()
	{
        this.GetDestinations();

        if (Phase != 2)
        {
            Debug.Log("My name is: " + this.Name + " and my Phase is: " + this.Phase);
        }

        if (this.Phase < this.ScheduleBlocks.Length && this.Clock.HourTime >= this.ScheduleBlocks[this.Phase].time)
		{
			this.Phase++;
			
			Debug.Log("My ID is: " + StudentID + " and I am teleporting to my destination.");
			
			if (this.Actions[this.Phase] == StudentActionType.Patrol)
			{
				this.CurrentDestination = this.StudentManager.Patrols.List[this.StudentID].GetChild(this.PatrolID);
				this.Pathfinding.target = this.CurrentDestination;
			}
			else
			{
                //This is only called if a character has just been teleported to a destination.

                Debug.Log(this.Name + "'s Phase is: " + this.Phase + " and their Destination is: " + this.Destinations[this.Phase]);

                this.CurrentDestination = this.Destinations[this.Phase];
				this.Pathfinding.target = this.Destinations[this.Phase];
			}

			this.transform.position = this.CurrentDestination.position;
		}
	}

	public void AltTeleportToDestination()
	{
		//Debug.Log("Currently working on: " + this.Name);

		/*
		if (this.CurrentDestination == null)
		{
			this.Phase++;
		}
		*/

		if (this.Club != ClubType.Council)
		{
			this.Phase++;

            if (this.Club == ClubType.Bully)
            {
                ScheduleBlock newBlock2 = this.ScheduleBlocks[2];
                newBlock2.destination = "Patrol";
                newBlock2.action = "Patrol";
            }

            GetDestinations();

            if (this.Actions[this.Phase] == StudentActionType.Patrol)
			{
				this.CurrentDestination = this.StudentManager.Patrols.List[this.StudentID].GetChild(this.PatrolID);
				this.Pathfinding.target = this.CurrentDestination;
			}
			else
			{
				this.CurrentDestination = this.Destinations[this.Phase];
				this.Pathfinding.target = this.Destinations[this.Phase];
			}

			this.transform.position = this.CurrentDestination.position;
        }

		//Debug.Log(this.Name + " was told to teleport to " + this.CurrentDestination);
	}

	public void GoCommitMurder()
	{
		this.StudentManager.MurderTakingPlace = true;
		
		if (!this.FragileSlave)
		{
			this.Yandere.EquippedWeapon.transform.parent = this.HipCollider.transform;
			this.Yandere.EquippedWeapon.transform.localPosition = Vector3.zero;
			this.Yandere.EquippedWeapon.transform.localScale = Vector3.zero;

			this.MyWeapon = this.Yandere.EquippedWeapon;
			this.MyWeapon.FingerprintID = this.StudentID;

			this.Yandere.EquippedWeapon = null;
			this.Yandere.Equipped = 0;
			this.StudentManager.UpdateStudents();
			this.Yandere.WeaponManager.UpdateLabels();
			this.Yandere.WeaponMenu.UpdateSprites();
			this.Yandere.WeaponWarning = false;
		}
		else
		{
			this.StudentManager.FragileWeapon.transform.parent = this.HipCollider.transform;
			this.StudentManager.FragileWeapon.transform.localPosition = Vector3.zero;
			this.StudentManager.FragileWeapon.transform.localScale = Vector3.zero;

			this.MyWeapon = this.StudentManager.FragileWeapon;
			this.MyWeapon.FingerprintID = this.StudentID;
			this.MyWeapon.MyCollider.enabled = false;
		}

		this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
		this.CharacterAnimation.CrossFade(AnimNames.FemaleBrokenStandUp);

		if (this.HuntTarget != this)
		{
			this.DistanceToDestination = 100.0f;
			this.Broken.Hunting = true;
			this.TargetDistance = 1.0f;
			this.Routine = false;
			this.Hunting = true;
		}
		else
		{
			this.Broken.Done = true;
			this.Routine = false;
			this.Suicide = true;
		}

		this.Prompt.Hide();
		this.Prompt.enabled = false;
	}

	public void Shove()
	{
		if (!this.Yandere.Shoved && !this.Dying && !this.Yandere.Egg && !this.Yandere.Lifting &&
			!this.ShoeRemoval.enabled && !this.Yandere.Talking && !this.SentToLocker)
		{
			ForgetRadio();

			Debug.Log(this.Name + " is shoving Yandere-chan.");

			AudioSource audioSource = this.GetComponent<AudioSource>();

			//audioSource.clip = this.ShoveClips[Random.Range(0, this.ShoveClips.Length)];
			//audioSource.Play();

				 if (this.StudentID == 86){this.Subtitle.UpdateLabel(SubtitleType.Shoving, 1, 5.0f);}
			else if (this.StudentID == 87){this.Subtitle.UpdateLabel(SubtitleType.Shoving, 2, 5.0f);}
			else if (this.StudentID == 88){this.Subtitle.UpdateLabel(SubtitleType.Shoving, 3, 5.0f);}
			else if (this.StudentID == 89){this.Subtitle.UpdateLabel(SubtitleType.Shoving, 4, 5.0f);}

			if (this.Yandere.Aiming)
			{
				this.Yandere.StopAiming();
			}

			if (this.Yandere.Laughing)
			{
				this.Yandere.StopLaughing();
			}

			this.transform.rotation = Quaternion.LookRotation(new Vector3(
				this.Yandere.Hips.transform.position.x,
				this.transform.position.y,
				this.Yandere.Hips.transform.position.z)
				- this.transform.position);

			this.Yandere.transform.rotation = Quaternion.LookRotation(new Vector3(
				this.Hips.transform.position.x,
				this.Yandere.transform.position.y,
				this.Hips.transform.position.z)
				- this.Yandere.transform.position);

			CharacterAnimation[this.ShoveAnim].time = 0.0f;
			CharacterAnimation.CrossFade(this.ShoveAnim);

			this.FocusOnYandere = false;
			this.Investigating = false;
			this.Distracted = true;
			this.Alarmed = false;
			this.Routine = false;
			this.Shoving = true;
			this.NoTalk = false;
			this.Patience--;

            if (this.StudentManager.BloodReporter == this)
            {
                this.StudentManager.BloodReporter = null;
                this.ReportingBlood = false;
            }

            this.WitnessedBloodyWeapon = false;
            this.WitnessedBloodPool = false;
            this.WitnessedSomething = false;
            this.WitnessedCorpse = false;
            this.WitnessedMurder = false;
            this.WitnessedWeapon = false;
            this.WitnessedLimb = false;

            this.BloodPool = null;

            if (this.Club != ClubType.Council && this.Persona != PersonaType.Violent)
			{
				this.Patience = 999;
			}

			if (this.Patience < 1)
			{
				this.Yandere.CannotRecover = true;
			}

			if (this.ReturningMisplacedWeapon)
			{
				this.DropMisplacedWeapon();
			}

			this.Yandere.CharacterAnimation[AnimNames.FemaleShoveA].time = 0;
			this.Yandere.CharacterAnimation.CrossFade(AnimNames.FemaleShoveA);
			this.Yandere.YandereVision = false;
			this.Yandere.NearSenpai = false;
			this.Yandere.Degloving = false;
			this.Yandere.Flicking = false;
			this.Yandere.Punching = false;
			this.Yandere.CanMove = false;
			this.Yandere.Shoved = true;
			this.Yandere.EmptyHands();

			this.Yandere.GloveTimer = 0;
			this.Yandere.h = 0;
			this.Yandere.v = 0;

			this.Yandere.ShoveSpeed = 2;

			if (this.Distraction != null)
			{
				this.TargetedForDistraction = false;
				this.Pathfinding.speed = 1.0f;
				this.SpeechLines.Stop();
				this.Distraction = null;
				this.CanTalk = true;
			}

			if (this.Actions[this.Phase] != StudentActionType.Patrol)
			{
				//This is only called after a character has finished shoving the player.

				this.CurrentDestination = this.Destinations[this.Phase];
				this.Pathfinding.target = this.CurrentDestination;
			}

			this.Pathfinding.canSearch = false;
			this.Pathfinding.canMove = false;
		}
	}

	public void PushYandereAway()
	{
		if (this.Yandere.Aiming)
		{
			this.Yandere.StopAiming();
		}

		if (this.Yandere.Laughing)
		{
			this.Yandere.StopLaughing();
		}

		this.Yandere.transform.rotation = Quaternion.LookRotation(new Vector3(
			this.Hips.transform.position.x,
			this.Yandere.transform.position.y,
			this.Hips.transform.position.z)
			- this.Yandere.transform.position);

		this.Yandere.CharacterAnimation[AnimNames.FemaleShoveA].time = 0;
		this.Yandere.CharacterAnimation.CrossFade(AnimNames.FemaleShoveA);
		this.Yandere.YandereVision = false;
		this.Yandere.NearSenpai = false;
		this.Yandere.Degloving = false;
		this.Yandere.Flicking = false;
		this.Yandere.Punching = false;
		this.Yandere.CanMove = false;
		this.Yandere.Shoved = true;
		this.Yandere.EmptyHands();

		this.Yandere.GloveTimer = 0;
		this.Yandere.h = 0;
		this.Yandere.v = 0;

		this.Yandere.ShoveSpeed = 2;
	}

	public void Spray()
	{
		Debug.Log(this.Name + " is trying to Spray Yandere-chan!");

		if (this.Yandere.Attacking)
		{
			this.CharacterAnimation.CrossFade(this.ReadyToFightAnim);
		}
		else
		{
			bool BreakUpFight = false;

			if (this.Yandere.DelinquentFighting && !this.NoBreakUp)
			{
				if (!this.StudentManager.CombatMinigame.Delinquent.WitnessedMurder)
				{
					BreakUpFight = true;
				}
			}

			if (!BreakUpFight)
			{
				if (!this.Yandere.Sprayed && !this.Dying && !this.Blind &&
					!this.Yandere.Egg && !this.Yandere.Dumping &&
					!this.Yandere.Bathing && !this.Yandere.Noticed)
				{
					if (this.SprayTimer > 0)
					{
						this.SprayTimer = Mathf.MoveTowards(this.SprayTimer, 0, Time.deltaTime);
					}
					else
					{
						AudioSource.PlayClipAtPoint(this.PepperSpraySFX, this.transform.position);

							 if (this.StudentID == 86){this.Subtitle.UpdateLabel(SubtitleType.Spraying, 1, 5.0f);}
						else if (this.StudentID == 87){this.Subtitle.UpdateLabel(SubtitleType.Spraying, 2, 5.0f);}
						else if (this.StudentID == 88){this.Subtitle.UpdateLabel(SubtitleType.Spraying, 3, 5.0f);}
						else if (this.StudentID == 89){this.Subtitle.UpdateLabel(SubtitleType.Spraying, 4, 5.0f);}

						if (this.Yandere.Aiming)
						{
							this.Yandere.StopAiming();
						}

						if (this.Yandere.Laughing)
						{
							this.Yandere.StopLaughing();
						}

						this.transform.rotation = Quaternion.LookRotation(new Vector3(
							this.Yandere.Hips.transform.position.x,
							this.transform.position.y,
							this.Yandere.Hips.transform.position.z)
							- this.transform.position);

						this.Yandere.transform.rotation = Quaternion.LookRotation(new Vector3(
							this.Hips.transform.position.x,
							this.Yandere.transform.position.y,
							this.Hips.transform.position.z)
							- this.Yandere.transform.position);

						CharacterAnimation.CrossFade(this.SprayAnim);
						this.PepperSpray.SetActive(true);
						this.Distracted = true;
						this.Spraying = true;
						this.Alarmed = false;
						this.Routine = false;
						this.Fleeing = true;
                        this.Blind = true;

                        this.Yandere.CharacterAnimation.CrossFade("f02_sprayed_00");
						this.Yandere.YandereVision = false;
						this.Yandere.NearSenpai = false;
						this.Yandere.Attacking = false;
						this.Yandere.FollowHips = true;
						this.Yandere.Punching = false;
						this.Yandere.CanMove = false;
						this.Yandere.Sprayed = true;

						this.Pathfinding.canSearch = false;
						this.Pathfinding.canMove = false;

						this.StudentManager.YandereDying = true;
						this.StudentManager.StopMoving();

						this.Yandere.Blur.blurIterations = 1;
						this.Yandere.Jukebox.Volume = 0;

						if (this.Yandere.DelinquentFighting)
						{
							this.StudentManager.CombatMinigame.Stop();
						}
					}
				}
				else
				{
					if (!this.Yandere.Sprayed)
					{
						this.CharacterAnimation.CrossFade(this.ReadyToFightAnim);
					}
				}
			}
			else
			{
				Debug.Log("A student council member is breaking up the fight.");

				this.StudentManager.CombatMinigame.Delinquent.CharacterAnimation.Play("stopFighting_00");
				this.Yandere.CharacterAnimation.Play(AnimNames.FemaleStopFighting);
				this.Yandere.FightHasBrokenUp = true;
				this.Yandere.BreakUpTimer = 10;

				//this.StudentManager.CombatMinigame.DisablePrompts();
				//this.StudentManager.CombatMinigame.MyVocals.Stop();
				//this.StudentManager.CombatMinigame.MyAudio.Stop();
				this.StudentManager.CombatMinigame.Path = 7;

                this.StudentManager.Portal.SetActive(true);

                this.CharacterAnimation.Play(this.BreakUpAnim);
				this.BreakingUpFight = true;
				this.SprayTimer = 1.0f;
			}

			this.StudentManager.CombatMinigame.DisablePrompts();
			this.StudentManager.CombatMinigame.MyVocals.Stop();
			this.StudentManager.CombatMinigame.MyAudio.Stop();

			Time.timeScale = 1;
		}
	}

	void DetermineCorpseLocation()
	{
		Debug.Log(this.Name + " has called the DetermineCorpseLocation() function.");

		if (this.StudentManager.Reporter == null)
		{
			this.StudentManager.Reporter = this;
		}

		if (this.Teacher)
		{
			this.StudentManager.CorpseLocation.position = this.Corpse.AllColliders[0].transform.position;

			this.StudentManager.CorpseLocation.LookAt(new Vector3(
				transform.position.x,
				this.StudentManager.CorpseLocation.position.y,
				transform.position.z));
			this.StudentManager.CorpseLocation.Translate(this.StudentManager.CorpseLocation.forward);

			this.StudentManager.LowerCorpsePosition();
		}

		this.Pathfinding.target = this.StudentManager.CorpseLocation;
		this.CurrentDestination = this.StudentManager.CorpseLocation;

		AssignCorpseGuardLocations();
	}

	void DetermineBloodLocation()
	{
		if (this.StudentManager.BloodReporter == null)
		{
			this.StudentManager.BloodReporter = this;
		}

		if (this.Teacher)
		{
			this.StudentManager.BloodLocation.position = this.BloodPool.transform.position;

			this.StudentManager.BloodLocation.LookAt(new Vector3(
				transform.position.x,
				this.StudentManager.BloodLocation.position.y,
				transform.position.z));

			this.StudentManager.BloodLocation.Translate(this.StudentManager.BloodLocation.forward);

			this.StudentManager.LowerBloodPosition();
		}
	}

	void AssignCorpseGuardLocations()
	{
		this.StudentManager.CorpseGuardLocation[1].position =
			this.StudentManager.CorpseLocation.position + new Vector3(0, 0, 1);
		this.LookAway(this.StudentManager.CorpseGuardLocation [1], this.StudentManager.CorpseLocation);

		this.StudentManager.CorpseGuardLocation[2].position =
			this.StudentManager.CorpseLocation.position + new Vector3(1, 0, 0);
		this.LookAway(this.StudentManager.CorpseGuardLocation[2], this.StudentManager.CorpseLocation);

		this.StudentManager.CorpseGuardLocation[3].position =
			this.StudentManager.CorpseLocation.position + new Vector3(0, 0, -1);
		this.LookAway(this.StudentManager.CorpseGuardLocation[3], this.StudentManager.CorpseLocation);

		this.StudentManager.CorpseGuardLocation[4].position =
			this.StudentManager.CorpseLocation.position + new Vector3(-1, 0, 0);
		this.LookAway(this.StudentManager.CorpseGuardLocation[4], this.StudentManager.CorpseLocation);
	}

	void AssignBloodGuardLocations()
	{
		this.StudentManager.BloodGuardLocation[1].position =
			this.StudentManager.BloodLocation.position + new Vector3(0, 0, 1);
		this.LookAway(this.StudentManager.BloodGuardLocation[1], this.StudentManager.BloodLocation);

		this.StudentManager.BloodGuardLocation[2].position =
			this.StudentManager.BloodLocation.position + new Vector3(1, 0, 0);
		this.LookAway(this.StudentManager.BloodGuardLocation[2], this.StudentManager.BloodLocation);

		this.StudentManager.BloodGuardLocation[3].position =
			this.StudentManager.BloodLocation.position + new Vector3(0, 0, -1);
		this.LookAway(this.StudentManager.BloodGuardLocation[3], this.StudentManager.BloodLocation);

		this.StudentManager.BloodGuardLocation[4].position =
			this.StudentManager.BloodLocation.position + new Vector3(-1, 0, 0);
		this.LookAway(this.StudentManager.BloodGuardLocation[4], this.StudentManager.BloodLocation);
	}

	void AssignTeacherGuardLocations()
	{
		this.StudentManager.TeacherGuardLocation[1].position =
			this.StudentManager.CorpseLocation.position + new Vector3(.75f, 0, .75f);
		this.LookAway(this.StudentManager.TeacherGuardLocation[1], this.StudentManager.CorpseLocation);

		this.StudentManager.TeacherGuardLocation[2].position =
			this.StudentManager.CorpseLocation.position + new Vector3(.75f, 0, -.75f);
		this.LookAway(this.StudentManager.TeacherGuardLocation[2], this.StudentManager.CorpseLocation);

		this.StudentManager.TeacherGuardLocation[3].position =
			this.StudentManager.CorpseLocation.position + new Vector3(-.75f, 0, -.75f);
		this.LookAway(this.StudentManager.TeacherGuardLocation[3], this.StudentManager.CorpseLocation);

		this.StudentManager.TeacherGuardLocation[4].position =
			this.StudentManager.CorpseLocation.position + new Vector3(-.75f, 0, .75f);
		this.LookAway(this.StudentManager.TeacherGuardLocation[4], this.StudentManager.CorpseLocation);

		this.StudentManager.TeacherGuardLocation[5].position =
			this.StudentManager.CorpseLocation.position + new Vector3(0, 0, .5f);
		this.LookAway(this.StudentManager.TeacherGuardLocation[5], this.StudentManager.CorpseLocation);

		this.StudentManager.TeacherGuardLocation[6].position =
			this.StudentManager.CorpseLocation.position + new Vector3(0, 0, -.5f);
		this.LookAway(this.StudentManager.TeacherGuardLocation[6], this.StudentManager.CorpseLocation);
	}

	void LookAway(Transform T1, Transform T2)
	{
		T1.LookAt(T2);
		var TempRotation = T1.eulerAngles.y + 180;
		T1.eulerAngles = new Vector3(T1.eulerAngles.x, TempRotation, T1.eulerAngles.z);
	}

	public void TurnToStone()
	{
		this.Cosmetic.RightEyeRenderer.material.mainTexture = this.Yandere.Stone;
		this.Cosmetic.LeftEyeRenderer.material.mainTexture = this.Yandere.Stone;
		this.Cosmetic.HairRenderer.material.mainTexture = this.Yandere.Stone;

		if (this.Cosmetic.HairRenderer.materials.Length > 1)
		{
			this.Cosmetic.HairRenderer.materials[1].mainTexture = this.Yandere.Stone;
		}

		this.Cosmetic.RightEyeRenderer.material.color = new Color(1, 1, 1, 1);
		this.Cosmetic.LeftEyeRenderer.material.color = new Color(1, 1, 1, 1);
		this.Cosmetic.HairRenderer.material.color = new Color(1, 1, 1, 1);

		this.MyRenderer.materials[0].mainTexture = this.Yandere.Stone;
		this.MyRenderer.materials[1].mainTexture = this.Yandere.Stone;
		this.MyRenderer.materials[2].mainTexture = this.Yandere.Stone;

		if (this.Teacher)
		{
			if (this.Cosmetic.TeacherAccessories[8].activeInHierarchy)
			{
				this.MyRenderer.materials[3].mainTexture = this.Yandere.Stone;
			}
		}

		if (this.PickPocket != null)
		{
			this.PickPocket.enabled = false;
			this.PickPocket.Prompt.Hide();
			this.PickPocket.Prompt.enabled = false;
		}

		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);

		Destroy(this.DetectionMarker.gameObject);

		AudioSource.PlayClipAtPoint(this.Yandere.Petrify,
			this.transform.position + new Vector3(0.0f, 1.0f, 0.0f));

		Instantiate(this.Yandere.Pebbles, this.Hips.position, Quaternion.identity);

		this.Pathfinding.enabled = false;
		this.ShoeRemoval.enabled = false;
		this.CharacterAnimation.Stop();
		this.Prompt.enabled = false;
		this.SpeechLines.Stop();
		this.Prompt.Hide();

		this.enabled = false;
	}

	public void StopPairing()
	{
		if (this.Actions[this.Phase] != StudentActionType.Clean)
		{
			if (this.Persona == PersonaType.PhoneAddict && !this.Phoneless)
			{
				if (!this.LostTeacherTrust)
				{
					this.WalkAnim = this.PhoneAnims[1];

					//Debug.Log("WalkAnim has reverted back to PhoneAnims[1] in StopPairing().");
				}
			}
		}

		this.Paired = false;
	}

	public float ChameleonBonus = 0.0f;
	public bool Chameleon = false;

	public void ChameleonCheck()
	{
		//Debug.Log("We're doing a Chameleon Check!");

		ChameleonBonus = 0.0f;
		Chameleon = false;

		if (Yandere != null)
		{
			if (Yandere.Persona == YanderePersonaType.Scholarly && Persona == PersonaType.TeachersPet ||
				Yandere.Persona == YanderePersonaType.Scholarly && Club == ClubType.Science ||
				Yandere.Persona == YanderePersonaType.Scholarly && Club == ClubType.Art ||

				Yandere.Persona == YanderePersonaType.Chill && Persona == PersonaType.SocialButterfly ||
				Yandere.Persona == YanderePersonaType.Chill && Club == ClubType.Photography ||
				Yandere.Persona == YanderePersonaType.Chill && Club == ClubType.Gaming ||

				Yandere.Persona == YanderePersonaType.Confident && Persona == PersonaType.Heroic ||
				Yandere.Persona == YanderePersonaType.Confident && Club == ClubType.MartialArts ||

				Yandere.Persona == YanderePersonaType.Elegant && Club == ClubType.Drama ||

				Yandere.Persona == YanderePersonaType.Girly && Persona == PersonaType.SocialButterfly ||
				Yandere.Persona == YanderePersonaType.Girly && Club == ClubType.Cooking ||

				Yandere.Persona == YanderePersonaType.Graceful && Club == ClubType.Gardening ||

				Yandere.Persona == YanderePersonaType.Haughty && Club == ClubType.Bully ||

				Yandere.Persona == YanderePersonaType.Lively && Persona == PersonaType.SocialButterfly ||
				Yandere.Persona == YanderePersonaType.Lively && Club == ClubType.LightMusic ||
				Yandere.Persona == YanderePersonaType.Lively && Club == ClubType.Sports ||

				Yandere.Persona == YanderePersonaType.Shy && Persona == PersonaType.Loner ||
				Yandere.Persona == YanderePersonaType.Shy && Club == ClubType.Occult ||

				Yandere.Persona == YanderePersonaType.Tough && Persona == PersonaType.Spiteful ||
				Yandere.Persona == YanderePersonaType.Tough && Club == ClubType.Delinquent)
			{
				Debug.Log("Chameleon is true!");

				ChameleonBonus = VisionDistance * .5f;
				Chameleon = true;
			}
		}
	}

	void PhoneAddictGameOver()
	{
		if (!this.Yandere.Lost)
		{
			this.Yandere.Character.GetComponent<Animation>().CrossFade(AnimNames.FemaleDown22);
			this.Yandere.ShoulderCamera.HeartbrokenCamera.SetActive(true);
			this.Yandere.RPGCamera.enabled = false;
			this.Yandere.Jukebox.GameOver();
			this.Yandere.enabled = false;
			this.Yandere.EmptyHands();

			this.Countdown.gameObject.SetActive(false);
			this.ChaseCamera.SetActive(false);

			this.Police.Heartbroken.Exposed = true;

			this.StudentManager.StopMoving();

			this.Fleeing = false;
		}
	}

	void EndAlarm()
	{
		//Debug.Log(this.Name + " has stopped being alarmed.");

		if (this.ReturnToRoutineAfter)
		{
			this.CurrentDestination = this.Destinations[this.Phase];
			this.Pathfinding.target = this.Destinations[this.Phase];

			this.ReturnToRoutineAfter = false;
		}

		this.Pathfinding.canSearch = true;
		this.Pathfinding.canMove = true;

		if ((this.StudentID == 1) || this.Teacher)
		{
			this.IgnoreTimer = 0.0001f;
		}
		else
		{
			this.IgnoreTimer = 5.0f;
		}

		if (this.Persona == PersonaType.PhoneAddict && !this.Phoneless)
		{
			this.SmartPhone.SetActive(true);
		}

		this.FocusOnYandere = false;
		this.DiscCheck = false;
		this.Alarmed = false;
		this.Reacted = false;

		this.Hesitation = 0.0f;
		this.AlarmTimer = 0.0f;

		if (this.WitnessedCorpse)
		{
			this.PersonaReaction();
		}
		else if (this.WitnessedBloodPool || this.WitnessedLimb || this.WitnessedWeapon)
		{
			if (this.Following)
			{
				ParticleSystem.EmissionModule heartsEmission = this.Hearts.emission;
				heartsEmission.enabled = false;

				this.FollowCountdown.gameObject.SetActive(false);
                this.Yandere.Follower = null;
                this.Yandere.Followers--;
				this.Following = false;
			}

			this.CharacterAnimation.CrossFade(this.WalkAnim);

			this.CurrentDestination = this.BloodPool;
			this.Pathfinding.target = this.BloodPool;

			this.Pathfinding.canSearch = true;
			this.Pathfinding.canMove = true;
			this.Pathfinding.speed = 1;

			this.InvestigatingBloodPool = true;
			this.Routine = false;

			this.IgnoreTimer = 0.0001f;
		}
		else
		{
			if (!this.Following && !this.Wet && !this.Investigating)
			{
				this.Routine = true;
			}
		}

		if (this.ResumeDistracting)
		{
			//Debug.Log("This character was told to resume distracting.");

			this.CharacterAnimation.CrossFade(this.WalkAnim);
			this.Distracting = true;
			this.Routine = false;
		}

		if (CurrentAction == StudentActionType.Clean)
		{
			this.SmartPhone.SetActive(false);
			this.Scrubber.SetActive(true);

			if (this.CleaningRole == 5)
			{
				this.Scrubber.GetComponent<Renderer>().material.mainTexture = this.Eraser.GetComponent<Renderer>().material.mainTexture;
				this.Eraser.SetActive(true);
			}
		}

		if (this.TurnOffRadio)
		{
			this.Routine = false;
		}
	}

	public void GetSleuthTarget()
	{
		this.TargetDistance = 2;
		this.SleuthID++;

		if (this.SleuthID < 98)
		{
			if (this.StudentManager.Students[this.SleuthID] == null)
			{
				this.GetSleuthTarget();
			}
			else if (!this.StudentManager.Students[this.SleuthID].gameObject.activeInHierarchy)
			{
				this.GetSleuthTarget();
			}
			else
			{
				this.SleuthTarget = this.StudentManager.Students[SleuthID].transform;
				this.Pathfinding.target = this.SleuthTarget;
				this.CurrentDestination = this.SleuthTarget;
			}
		}
		else if (this.SleuthID == 98)
		{
			if (this.Yandere.Club == ClubType.Photography)
			{
				this.SleuthID = 0;
				this.GetSleuthTarget();
			}
			else
			{
				this.SleuthTarget = this.Yandere.transform;
				this.Pathfinding.target = this.SleuthTarget;
				this.CurrentDestination = this.SleuthTarget;
			}
		}
		else
		{
			this.SleuthID = 0;
			this.GetSleuthTarget();
		}
	}

	public void GetFoodTarget()
	{
		this.Attempts++;

		if (this.Attempts >= 100)
		{
			this.Phase++;
		}
		else
		{
			this.SleuthID++;

			if (this.SleuthID < 90)
			{
				if (this.SleuthID == this.StudentID)
				{
					this.GetFoodTarget();
				}
				else if (this.StudentManager.Students[this.SleuthID] == null)
				{
					this.GetFoodTarget();
				}
				else if (!this.StudentManager.Students[this.SleuthID].gameObject.activeInHierarchy)
				{
					this.GetFoodTarget();
				}
				else if (this.StudentManager.Students[this.SleuthID].CurrentAction == StudentActionType.SitAndEatBento ||
						 this.StudentManager.Students[this.SleuthID].Club == ClubType.Cooking ||
						 this.StudentManager.Students[this.SleuthID].Club == ClubType.Delinquent ||
						 this.StudentManager.Students[this.SleuthID].Club == ClubType.Sports ||
						 this.StudentManager.Students[this.SleuthID].TargetedForDistraction ||
						 this.StudentManager.Students[this.SleuthID].ClubActivityPhase >= 16 ||
					     this.StudentManager.Students[this.SleuthID].InEvent ||
						 !this.StudentManager.Students[this.SleuthID].Routine ||
						 this.StudentManager.Students[this.SleuthID].Posing ||
						 this.StudentManager.Students[this.SleuthID].Slave ||
						 this.StudentManager.Students[this.SleuthID].Wet ||
						 this.StudentManager.Students[this.SleuthID].Club == ClubType.LightMusic &&
						 this.StudentManager.PracticeMusic.isPlaying)
				{
					//Debug.Log(this.Name + " can't use this student! This student is busy!");

					this.GetFoodTarget();
				}
				else
				{
					//Debug.Log(this.Name + " is choosing Student #" + SleuthID + " as their target. This student is in the " + this.StudentManager.Students[this.SleuthID].Club + " Club.");

					this.CharacterAnimation.CrossFade(this.WalkAnim);

					this.DistractionTarget = this.StudentManager.Students[this.SleuthID];
					this.DistractionTarget.TargetedForDistraction = true;

					this.SleuthTarget = this.StudentManager.Students[SleuthID].transform;
					this.Pathfinding.target = this.SleuthTarget;
					this.CurrentDestination = this.SleuthTarget;

					this.TargetDistance = .75f;
					this.DistractTimer = 8.0f;
					this.Distracting = true;
					//this.Distracted = true;
					this.CanTalk = false;
					this.Routine = false;

					this.Attempts = 0;
				}
			}
			else
			{
				this.SleuthID = 0;
				this.GetFoodTarget();
			}
		}
	}

	void PhoneAddictCameraUpdate()
	{
		//Debug.Log(this.Name + " is calling PhoneAddictCameraUpdate().");

		if (this.SmartPhone.transform.parent != null)
		{
			this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;

			this.SmartPhone.transform.localPosition = new Vector3(0, .005f, -.01f);
			this.SmartPhone.transform.localEulerAngles = new Vector3(7.33333f, -154, 173.66666f);
			this.SmartPhone.SetActive(true);

			if (this.Sleuthing)
			{
				if (this.AlarmTimer < 2)
				{
					this.AlarmTimer = 2;
					this.ScaredAnim = this.SleuthReactAnim;
					this.SprintAnim = this.SleuthReportAnim;
					this.CharacterAnimation.CrossFade(this.ScaredAnim);
				}

				if (!this.CameraFlash.activeInHierarchy)
				{
					if (this.CharacterAnimation[this.ScaredAnim].time > 2)
					{
						this.CameraFlash.SetActive(true);

						if (this.Yandere.Mask != null)
						{
							this.Countdown.MaskedPhoto = true;
						}
					}
				}
			}
			else
			{
				this.ScaredAnim = this.PhoneAnims[4];
				this.CharacterAnimation.CrossFade(this.ScaredAnim);

				if (!this.CameraFlash.activeInHierarchy)
				{
					if (this.CharacterAnimation[this.ScaredAnim].time > 3.66666)
					{
						this.CameraFlash.SetActive(true);

						if (this.Yandere.Mask != null)
						{
							this.Countdown.MaskedPhoto = true;
						}
						else
						{
							if (this.Grudge)
							{
								this.Police.PhotoEvidence++;
								this.PhotoEvidence = true;
							}
						}
					}
				}
			}
		}
	}

	void ReturnToRoutine()
	{
		if (this.Actions[this.Phase] == StudentActionType.Patrol)
		{
			this.CurrentDestination = this.StudentManager.Patrols.List[this.StudentID].GetChild(this.PatrolID);
			this.Pathfinding.target = this.CurrentDestination;
		}
		else
		{
			//This is only called after the ReturnToRoutine function has been called (after a student council member breaks up a fight).

			this.CurrentDestination = this.Destinations[this.Phase];
			this.Pathfinding.target = this.Destinations[this.Phase];
		}

		this.BreakingUpFight = false;
		this.WitnessedMurder = false;
		this.Pathfinding.speed = 1;
		this.Prompt.enabled = true;
		this.Alarmed = false;
		this.Fleeing = false;
		this.Routine = true;
		this.Grudge = false;
	}

	public void EmptyHands()
	{
		//Debug.Log(this.Name + " was told to empty their hands.");

		bool DoNotDisablePhone = false;

		if (this.SentHome && this.SmartPhone.activeInHierarchy || this.PhotoEvidence || this.Persona == PersonaType.PhoneAddict && !this.Dying && !this.Wet)
		{
			DoNotDisablePhone = true;
		}

		if (this.MyPlate != null)
		{
			if (this.MyPlate.parent != null)
			{
				if (this.WitnessedMurder || this.WitnessedCorpse)
				{
					this.DropPlate();
				}
				else
				{
					this.MyPlate.gameObject.SetActive(false);
				}
			}
		}

		if (this.Club == ClubType.Gardening)
		{
			this.WateringCan.transform.parent = this.Hips;
			this.WateringCan.transform.localPosition = new Vector3(0, .0135f, -.184f);
			this.WateringCan.transform.localEulerAngles = new Vector3(0, 90, 30);
		}

		if (this.Club == ClubType.LightMusic)
		{
			if (this.StudentID == 51)
			{
				if (this.InstrumentBag[this.ClubMemberID].transform.parent == null)
				{
					this.Instruments[this.ClubMemberID].transform.parent = null;
					this.Instruments[this.ClubMemberID].transform.position = new Vector3(-.5f, 4.5f, 22.45666f);
					this.Instruments[this.ClubMemberID].transform.eulerAngles = new Vector3(-15, 0, 0);

					this.Instruments[this.ClubMemberID].GetComponent<AudioSource>().playOnAwake = false;
					this.Instruments[this.ClubMemberID].GetComponent<AudioSource>().Stop();
				}
				else
				{
					this.Instruments[this.ClubMemberID].SetActive(false);
				}
			}
			else
			{
				this.Instruments[this.ClubMemberID].SetActive(false);
			}

			this.Drumsticks[0].SetActive(false);
			this.Drumsticks[1].SetActive(false);

			this.AirGuitar.Stop();
		}

		if (!this.Male)
		{
			this.Handkerchief.SetActive(false);
			this.Cigarette.SetActive(false);
			this.Lighter.SetActive(false);
		}
		else
		{
			this.PinkSeifuku.SetActive(false);
		}

		if (!DoNotDisablePhone)
		{
			this.SmartPhone.SetActive(false);
		}

		if (this.BagOfChips != null)
		{
			this.BagOfChips.SetActive(false);
		}

		this.Chopsticks[0].SetActive(false);
		this.Chopsticks[1].SetActive(false);
		this.Sketchbook.SetActive(false);
		this.OccultBook.SetActive(false);
		this.Paintbrush.SetActive(false);
		this.EventBook.SetActive(false);
		this.Scrubber.SetActive(false);
		this.Octodog.SetActive(false);
		this.Palette.SetActive(false);
		this.Eraser.SetActive(false);
		this.Pencil.SetActive(false);
		this.Bento.SetActive(false);
		this.Pen.SetActive(false);

		foreach (GameObject prop in this.ScienceProps)
		{
			if (prop != null)
			{
				prop.SetActive(false);
			}
		}

		foreach (GameObject prop in this.Fingerfood)
		{
			if (prop != null)
			{
				prop.SetActive(false);
			}
		}
	}

	public RiggedAccessoryAttacher LabcoatAttacher;
	public RiggedAccessoryAttacher ApronAttacher;
	public Mesh HeadAndHands;

	public void UpdateAnimLayers()
	{
		this.CharacterAnimation[this.LeanAnim].speed += (this.StudentID * 0.01f);
		this.CharacterAnimation[this.ConfusedSitAnim].speed += (this.StudentID * 0.01f);

		//Debug.Log("My name is: " + this.Name);
		this.CharacterAnimation[this.WalkAnim].time = Random.Range(0.0f, this.CharacterAnimation[this.WalkAnim].length);

		this.CharacterAnimation[this.WetAnim].layer = 9;
		this.CharacterAnimation.Play(this.WetAnim);
		this.CharacterAnimation[this.WetAnim].weight = 0.0f;

		if (!this.Male)
		{
			this.CharacterAnimation[this.StripAnim].speed = 1.50f;
			this.CharacterAnimation[this.GameAnim].speed = 2.0f;

			this.CharacterAnimation["f02_moLipSync_00"].layer = 9;
			this.CharacterAnimation.Play("f02_moLipSync_00");
			this.CharacterAnimation["f02_moLipSync_00"].weight = 0.0f;

			this.CharacterAnimation[AnimNames.FemaleTopHalfTexting].layer = 8;
			this.CharacterAnimation.Play(AnimNames.FemaleTopHalfTexting);
			this.CharacterAnimation[AnimNames.FemaleTopHalfTexting].weight = 0.0f;

			this.CharacterAnimation[this.CarryAnim].layer = 7;
			this.CharacterAnimation.Play(this.CarryAnim);
			this.CharacterAnimation[this.CarryAnim].weight = 0.0f;

			this.CharacterAnimation[this.SocialSitAnim].layer = 6;
			this.CharacterAnimation.Play(this.SocialSitAnim);
			this.CharacterAnimation[this.SocialSitAnim].weight = 0.0f;

			this.CharacterAnimation[this.ShyAnim].layer = 5;
			this.CharacterAnimation.Play(this.ShyAnim);
			this.CharacterAnimation[this.ShyAnim].weight = 0.0f;

			this.CharacterAnimation[this.FistAnim].layer = 4;
			this.CharacterAnimation[this.FistAnim].weight = 0.0f;

			this.CharacterAnimation[this.BentoAnim].layer = 3;
			this.CharacterAnimation.Play(this.BentoAnim);
			this.CharacterAnimation[this.BentoAnim].weight = 0.0f;

			this.CharacterAnimation[this.AngryFaceAnim].layer = 2;
			this.CharacterAnimation.Play(this.AngryFaceAnim);
			this.CharacterAnimation[this.AngryFaceAnim].weight = 0.0f;

			this.CharacterAnimation[AnimNames.FemaleWetIdle].speed = 1.25f;
			this.CharacterAnimation["f02_sleuthScan_00"].speed = 1.4f;
		}
		else
		{
			this.CharacterAnimation[this.ConfusedSitAnim].speed *= -1.0f;

			this.CharacterAnimation[this.ToughFaceAnim].layer = 7;
			this.CharacterAnimation.Play(this.ToughFaceAnim);
			this.CharacterAnimation[this.ToughFaceAnim].weight = 0.0f;

			this.CharacterAnimation[this.SocialSitAnim].layer = 6;
			this.CharacterAnimation.Play(this.SocialSitAnim);
			this.CharacterAnimation[this.SocialSitAnim].weight = 0.0f;

			this.CharacterAnimation[this.CarryShoulderAnim].layer = 5;
			this.CharacterAnimation.Play(this.CarryShoulderAnim);
			this.CharacterAnimation[this.CarryShoulderAnim].weight = 0.0f;

			this.CharacterAnimation[AnimNames.MaleScaredFace].layer = 4;
			this.CharacterAnimation.Play(AnimNames.MaleScaredFace);
			this.CharacterAnimation[AnimNames.MaleScaredFace].weight = 0.0f;

			this.CharacterAnimation[this.SadFaceAnim].layer = 3;
			this.CharacterAnimation.Play(this.SadFaceAnim);
			this.CharacterAnimation[this.SadFaceAnim].weight = 0.0f;

			this.CharacterAnimation[this.AngryFaceAnim].layer = 2;
			this.CharacterAnimation.Play(this.AngryFaceAnim);
			this.CharacterAnimation[this.AngryFaceAnim].weight = 0.0f;

			this.CharacterAnimation["sleuthScan_00"].speed = 1.4f;
		}

		if (this.Persona == PersonaType.Sleuth)
		{
			this.CharacterAnimation[this.WalkAnim].time = Random.Range(0.0f, this.CharacterAnimation[this.WalkAnim].length);
		}

		if (this.Club == ClubType.Bully)
		{
			if (!StudentGlobals.GetStudentBroken(this.StudentID))
			{
				if (this.BullyID > 1)
				{
					this.CharacterAnimation["f02_bullyLaugh_00"].speed = .9f + (BullyID * .1f);
				}
			}
		}
		else if (this.Club == ClubType.Delinquent)
		{
			this.CharacterAnimation[this.WalkAnim].time = Random.Range(0.0f, this.CharacterAnimation[this.WalkAnim].length);
			this.CharacterAnimation[LeanAnim].speed = .5f;
		}
		else if (this.Club == ClubType.Council)
		{
			this.CharacterAnimation["f02_faceCouncil" + this.Suffix + "_00"].layer = 10;
			this.CharacterAnimation.Play("f02_faceCouncil" + this.Suffix + "_00");
		}
		else if (this.Club == ClubType.Gaming)
		{
			this.CharacterAnimation[this.VictoryAnim].speed -= (.1f * (this.StudentID - 36));
			this.CharacterAnimation[this.VictoryAnim].speed = .866666f;
		}
		else if (this.Club == ClubType.Cooking)
		{
			if (this.ClubActivityPhase > 0)
			{
				Debug.Log("This is a cooking club member, and they should be performing the ''PlateWalkAnim''.");
				this.WalkAnim = this.PlateWalkAnim;
			}
		}

		//Gema Taku
		if (this.StudentID == 36)
		{
			this.CharacterAnimation[this.ToughFaceAnim].weight = 1.0f;
		}
		//Sports Club Leader
		else if (this.StudentID == 66)
		{
			this.CharacterAnimation[this.ToughFaceAnim].weight = 1.0f;
		}
	}

	void SpawnDetectionMarker()
	{
		this.DetectionMarker = Instantiate(this.Marker,
			GameObject.Find("DetectionPanel").transform.position,
			Quaternion.identity).GetComponent<DetectionMarkerScript>();
		this.DetectionMarker.transform.parent = GameObject.Find("DetectionPanel").transform;
		this.DetectionMarker.Target = this.transform;
	}

	public void EquipCleaningItems()
	{
		if (this.CurrentAction == StudentActionType.Clean)
		{
			if (!this.Phoneless)
			{
				if (this.Persona == PersonaType.PhoneAddict || this.Persona == PersonaType.Sleuth)
				{
					this.WalkAnim = this.OriginalWalkAnim;
				}
			}

			this.SmartPhone.SetActive(false);
			this.Scrubber.SetActive(true);

			if (this.CleaningRole == 5)
			{
				this.Scrubber.GetComponent<Renderer>().material.mainTexture = this.Eraser.GetComponent<Renderer>().material.mainTexture;
				this.Eraser.SetActive(true);
			}

			if (this.StudentID == 9 || this.StudentID == 60)
			{
				this.WalkAnim = this.OriginalOriginalWalkAnim;
			}
		}
	}

	public void DetermineWhatWasWitnessed()
	{
		Debug.Log("We are now determining what " + this.Name + " witnessed.");

		if (this.Witnessed == StudentWitnessType.Murder)
		{
			Debug.Log("No need to go through the entire chain. We already know that this character witnessed murder.");

			this.Concern = 5;
		}
		else
		{
			if (this.YandereVisible)
			{
				//Debug.Log(this.Name + " can see Yandere-chan.");

				bool WitnessedBlood = false;

				if (this.Yandere.Bloodiness + this.Yandere.GloveBlood > 0.0f && !this.Yandere.Paint)
				{
					WitnessedBlood = true;
				}

				bool suspiciousWeapon = this.Yandere.Armed && this.Yandere.EquippedWeapon.Suspicious;
				bool suspiciousItem = this.Yandere.PickUp != null && this.Yandere.PickUp.Suspicious;
				bool carryingSeveredLimb = this.Yandere.PickUp != null && this.Yandere.PickUp.BodyPart != null;
				bool carryingBloodyClothing = this.Yandere.PickUp != null && this.Yandere.PickUp.Clothing && this.Yandere.PickUp.Evidence;

				//Debug.Log("carryingBloodyClothing is: " + carryingBloodyClothing);

				int OriginalConcern = Concern;

				if (suspiciousWeapon)
				{
					this.WeaponToTakeAway = this.Yandere.EquippedWeapon;
				}

				if (this.Yandere.Rummaging || this.Yandere.TheftTimer > 0)
				{
					Debug.Log("Saw Yandere-chan stealing.");

					this.Witnessed = StudentWitnessType.Theft;
					this.Concern = 5;
				}
				else if (this.Yandere.Pickpocketing || this.Yandere.Caught)
				{
					Debug.Log("Saw Yandere-chan pickpocketing.");

					this.Witnessed = StudentWitnessType.Pickpocketing;
					this.Concern = 5;

					this.Yandere.StudentManager.PickpocketMinigame.Failure = true;
					this.Yandere.StudentManager.PickpocketMinigame.End();
					this.Yandere.Caught = true;

					if (this.Teacher)
					{
						this.Witnessed = StudentWitnessType.Theft;
					}
				}
				else if (suspiciousWeapon && WitnessedBlood &&
					(this.Yandere.Sanity < 33.333f))
				{
					this.Witnessed = StudentWitnessType.WeaponAndBloodAndInsanity;
					this.RepLoss = 30.0f;
					this.Concern = 5;
				}
				else if (suspiciousWeapon && (this.Yandere.Sanity < 33.333f))
				{
					this.Witnessed = StudentWitnessType.WeaponAndInsanity;
					this.RepLoss = 20.0f;
					this.Concern = 5;
				}
				else if (WitnessedBlood && (this.Yandere.Sanity < 33.333f))
				{
					this.Witnessed = StudentWitnessType.BloodAndInsanity;
					this.RepLoss = 20.0f;
					this.Concern = 5;
				}
				else if (suspiciousWeapon && WitnessedBlood)
				{
					this.Witnessed = StudentWitnessType.WeaponAndBlood;
					this.RepLoss = 20.0f;
					this.Concern = 5;
				}
				else if (suspiciousWeapon)
				{
					Debug.Log("Saw Yandere-chan with a weapon.");

					this.WeaponWitnessed = this.Yandere.EquippedWeapon.WeaponID;

					this.Witnessed = StudentWitnessType.Weapon;
					this.RepLoss = 10.0f;
					this.Concern = 5;
				}
				else if (suspiciousItem)
				{
					if (this.Yandere.PickUp.CleaningProduct)
					{
						this.Witnessed = StudentWitnessType.CleaningItem;
					}
					else
					{
						if (this.Teacher)
						{
							this.Witnessed = StudentWitnessType.Suspicious;
						}
						else
						{
							this.Witnessed = StudentWitnessType.Weapon;
						}
					}

					this.RepLoss = 10.0f;
					this.Concern = 5;
				}
				else if (WitnessedBlood)
				{
					this.Witnessed = StudentWitnessType.Blood;

					if (!this.Bloody)
					{
						this.RepLoss = 10.0f;
						this.Concern = 5;
					}
					else
					{
						this.RepLoss = 0.0f;
						this.Concern = 0;
					}
				}
				else if (this.Yandere.Sanity < 33.333f)
				{
					this.Witnessed = StudentWitnessType.Insanity;
					this.RepLoss = 10.0f;
					this.Concern = 5;
				}
				else if (this.Yandere.Lewd)
				{
					this.Witnessed = StudentWitnessType.Lewd;
					this.RepLoss = 10.0f;
					this.Concern = 5;
				}
				else if (this.Yandere.Laughing && this.Yandere.LaughIntensity > 15 ||
					this.Yandere.Stance.Current == StanceType.Crouching ||
					this.Yandere.Stance.Current == StanceType.Crawling)
				{
					this.Witnessed = StudentWitnessType.Insanity;
					this.RepLoss = 10.0f;
					this.Concern = 5;
				}
				else if (this.Yandere.Poisoning)
				{
					this.Witnessed = StudentWitnessType.Poisoning;
					this.RepLoss = 10.0f;
					this.Concern = 5;
				}
				else if (this.Yandere.Trespassing && (this.StudentID > 1))
				{
					this.Witnessed = this.Private ? StudentWitnessType.Interruption : 
						StudentWitnessType.Trespassing;
					this.Witness = false;

					if (!this.Teacher)
					{
						this.RepLoss = 10.0f;
					}

					this.Concern++;
				}
				else if (this.Yandere.NearSenpai)
				{
					this.Witnessed = StudentWitnessType.Stalking;
					//this.Yandere.Creepiness++;
					this.Concern++;
				}
				else if (this.Yandere.Eavesdropping)
				{
					if (this.StudentID == 1)
					{
						this.Witnessed = StudentWitnessType.Stalking;
						this.Concern++;
					}
					else
					{
						if (this.InEvent)
						{
							this.EventInterrupted = true;
						}

						this.Witnessed = StudentWitnessType.Eavesdropping;
						this.RepLoss = 10.0f;
						this.Concern = 5;
					}
				}
				else if (this.Yandere.Aiming)
				{
					this.Witnessed = StudentWitnessType.Stalking;
					this.Concern++;
				}
				else if (this.Yandere.DelinquentFighting)
				{
					this.Witnessed = StudentWitnessType.Violence;
					this.RepLoss = 10.0f;
					this.Concern = 5;
				}
				else if (this.Yandere.PickUp != null && this.Yandere.PickUp.Clothing && this.Yandere.PickUp.Evidence)
				{
					Debug.Log("Saw Yandere-chan with bloody clothing.");

					this.Witnessed = StudentWitnessType.HoldingBloodyClothing;
					this.RepLoss = 10.0f;
					this.Concern = 5;
				}
				else if (carryingSeveredLimb || carryingBloodyClothing)
				{
					this.Witnessed = StudentWitnessType.CoverUp;
				}

				if (this.StudentID == 1 && Witnessed == StudentWitnessType.Insanity)
				{
					if (this.Yandere.Stance.Current == StanceType.Crouching ||
					    this.Yandere.Stance.Current == StanceType.Crawling)
					{
						this.Witnessed = StudentWitnessType.Stalking;
						this.Concern = OriginalConcern;
						this.Concern++;
					}
				}
			}
			else
			{
				Debug.Log(this.Name + " is reacting to something other than Yandere-chan.");

				if (this.WitnessedLimb)
				{
					this.Witnessed = StudentWitnessType.SeveredLimb;
				}
				else if (this.WitnessedBloodyWeapon)
				{
					this.Witnessed = StudentWitnessType.BloodyWeapon;
				}
				else if (this.WitnessedBloodPool)
				{
					this.Witnessed = StudentWitnessType.BloodPool;
				}
				else if (this.WitnessedWeapon)
				{
					this.Witnessed = StudentWitnessType.DroppedWeapon;
				}
				else if (this.WitnessedCorpse)
				{
					this.Witnessed = StudentWitnessType.Corpse;
				}
				else
				{
					Debug.Log("Apparently, we didn't even see anything! 1");

					this.Witnessed = StudentWitnessType.None;
					this.DiscCheck = true;
					this.Witness = false;
				}
			}
		}

		if (this.Concern == 5)
		{
			if (this.Club == ClubType.Council)
			{
				Debug.Log("A member of the student council is being transformed into a teacher.");
				this.Teacher = true;
			}
		}
	}

	public void DetermineTeacherSubtitle()
	{
		Debug.Log("We are now determining what line of dialogue the teacher should say.");

		if (this.Club == ClubType.Council)
		{
			this.Subtitle.UpdateLabel(SubtitleType.CouncilToCounselor, this.ClubMemberID, 5.0f);
		}
		else
		{
			//We need to do some special-case here. If the teacher reached this area of the code
			//specifically while she was guarding, then that changes things.
			if (this.Guarding)
			{
				if (this.Yandere.Bloodiness + this.Yandere.GloveBlood > 0.0f && !this.Yandere.Paint)
				{
					this.Witnessed = StudentWitnessType.Blood;
				}
				else if (this.Yandere.Armed)
				{
					this.Witnessed = StudentWitnessType.Weapon;
				}
				else if (this.Yandere.Sanity < 66.66666f)
				{
					this.Witnessed = StudentWitnessType.Insanity;
				}
			}

			if (this.Witnessed == StudentWitnessType.Murder)
			{
				if (this.WitnessedMindBrokenMurder)
				{
					this.Subtitle.UpdateLabel(SubtitleType.TeacherMurderReaction, 4, 6.0f);
				}
				else
				{
					this.Subtitle.UpdateLabel(SubtitleType.TeacherMurderReaction, 1, 6.0f);
				}

				this.GameOverCause = GameOverType.Murder;
				this.WitnessedMurder = true;
			}
			else if (this.Witnessed == StudentWitnessType.WeaponAndBloodAndInsanity)
			{
				this.Subtitle.UpdateLabel(SubtitleType.TeacherInsanityHostile, 1, 6.0f);
				this.GameOverCause = GameOverType.Insanity;
				this.WitnessedMurder = true;
			}
			else if (this.Witnessed == StudentWitnessType.WeaponAndBlood)
			{
				this.Subtitle.UpdateLabel(SubtitleType.TeacherWeaponHostile, 1, 6.0f);
				this.GameOverCause = GameOverType.Weapon;
				this.WitnessedMurder = true;
			}
			else if (this.Witnessed == StudentWitnessType.WeaponAndInsanity)
			{
				this.Subtitle.UpdateLabel(SubtitleType.TeacherInsanityHostile, 1, 6.0f);
				this.GameOverCause = GameOverType.Insanity;
				this.WitnessedMurder = true;
			}
			else if (this.Witnessed == StudentWitnessType.BloodAndInsanity)
			{
				this.Subtitle.UpdateLabel(SubtitleType.TeacherInsanityHostile, 1, 6.0f);
				this.GameOverCause = GameOverType.Insanity;
				this.WitnessedMurder = true;
			}
			else if (this.Witnessed == StudentWitnessType.Weapon)
			{
				this.Subtitle.UpdateLabel(SubtitleType.TeacherWeaponHostile, 1, 6.0f);
				this.GameOverCause = GameOverType.Weapon;
				this.WitnessedMurder = true;
			}
			else if (this.Witnessed == StudentWitnessType.Blood)
			{
				this.Subtitle.UpdateLabel(SubtitleType.TeacherBloodHostile, 1, 6.0f);
				this.GameOverCause = GameOverType.Blood;
				this.WitnessedMurder = true;
			}
			else if (this.Witnessed == StudentWitnessType.Insanity || this.Witnessed == StudentWitnessType.Poisoning)
			{
				this.Subtitle.UpdateLabel(SubtitleType.TeacherInsanityHostile, 1, 6.0f);
				this.GameOverCause = GameOverType.Insanity;
				this.WitnessedMurder = true;
			}
			else if (this.Witnessed == StudentWitnessType.Lewd)
			{
				this.Subtitle.UpdateLabel(SubtitleType.TeacherLewdReaction, 1, 6.0f);
				this.GameOverCause = GameOverType.Lewd;
			}
			else if (this.Witnessed == StudentWitnessType.Trespassing)
			{
				this.Subtitle.UpdateLabel(SubtitleType.TeacherTrespassingReaction, this.Concern, 5.0f);
			}
			else if (this.Witnessed == StudentWitnessType.Corpse)
			{
				Debug.Log("A teacher just discovered a corpse.");

				this.DetermineCorpseLocation();

				this.Subtitle.UpdateLabel(SubtitleType.TeacherCorpseReaction, 1, 3.0f);
				this.Police.Called = true;
			}
			else if (this.Witnessed == StudentWitnessType.CoverUp)
			{
				this.Subtitle.UpdateLabel(SubtitleType.TeacherCoverUpHostile, 1, 6.0f);
				this.GameOverCause = GameOverType.Blood;
				this.WitnessedMurder = true;
			}
			else if (this.Witnessed == StudentWitnessType.CleaningItem)
			{
				this.Subtitle.UpdateLabel(SubtitleType.TeacherInsanityReaction, 1, 6.0f);
				this.GameOverCause = GameOverType.Insanity;
			}
		}
	}

	public void ReturnMisplacedWeapon()
	{
		Debug.Log (this.Name + " has returned a misplaced weapon.");

        this.StopInvestigating();

		if (StudentManager.BloodReporter == this)
		{
			StudentManager.BloodReporter = null;
		}

		this.BloodPool.parent = null;
		this.BloodPool.position = this.BloodPool.GetComponent<WeaponScript>().StartingPosition;
		this.BloodPool.eulerAngles = this.BloodPool.GetComponent<WeaponScript>().StartingRotation;

        this.BloodPool.GetComponent<WeaponScript>().Prompt.enabled = true;
		this.BloodPool.GetComponent<WeaponScript>().enabled = true;
		this.BloodPool.GetComponent<WeaponScript>().Drop();
		this.BloodPool.GetComponent<WeaponScript>().MyRigidbody.useGravity = false;
		this.BloodPool.GetComponent<WeaponScript>().MyRigidbody.isKinematic = true;
		this.BloodPool.GetComponent<WeaponScript>().Returner = null;

		this.BloodPool = null;

		this.CurrentDestination = this.Destinations[this.Phase];
		this.Pathfinding.target = this.Destinations[this.Phase];

		if (this.Club == ClubType.Council || this.Teacher)
		{
			this.Handkerchief.SetActive(false);
		}

		this.Pathfinding.speed = 1;

		this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;

		this.ReturningMisplacedWeapon = false;
		this.WitnessedSomething = false;
        this.VerballyReacted = false;
        this.WitnessedWeapon = false;
        this.YandereInnocent = false;
        this.ReportingBlood = false;
		this.Distracted = false;
		this.Routine = true;

		this.ReturningMisplacedWeaponPhase = 0;
		this.WitnessCooldownTimer = 0;

        this.Yandere.WeaponManager.ReturnWeaponID = -1;
        this.Yandere.WeaponManager.ReturnStudentID = -1;
    }

	public void StopMusic()
	{
		if (this.StudentID == 51)
		{
			if (this.InstrumentBag[this.ClubMemberID].transform.parent == null)
			{
				this.Instruments[this.ClubMemberID].transform.parent = null;
				this.Instruments[this.ClubMemberID].transform.position = new Vector3(-.5f, 4.5f, 22.45666f);
				this.Instruments[this.ClubMemberID].transform.eulerAngles = new Vector3(-15, 0, 0);

				this.Instruments[this.ClubMemberID].GetComponent<AudioSource>().playOnAwake = false;
				this.Instruments[this.ClubMemberID].GetComponent<AudioSource>().Stop();
			}
			else
			{
				this.Instruments[this.ClubMemberID].SetActive(false);
			}
		}
		else
		{
			this.Instruments[this.ClubMemberID].SetActive(false);
		}

		this.Drumsticks[0].SetActive(false);
		this.Drumsticks[1].SetActive(false);
	}

	public void DropPuzzle()
	{
		this.PuzzleCube.enabled = true;
		this.PuzzleCube.Drop();

		this.SolvingPuzzle = false;
		this.Distracted = false;
		this.PuzzleTimer = 0;
	}

	public void ReturnToNormal()
	{
		Debug.Log(this.Name + " has been instructed to forget everything and return to normal.");

		if (this.StudentManager.Reporter == this)
		{
			this.StudentManager.CorpseLocation.position = Vector3.zero;
			this.StudentManager.Reporter = null;
		}
		else if (this.StudentManager.BloodReporter == this)
		{
			this.StudentManager.BloodLocation.position = Vector3.zero;
			this.StudentManager.BloodReporter = null;
		}

        if (this.Yandere.Pursuer == this)
        {
            this.Yandere.Pursuer = null;
            this.Yandere.PreparedForStruggle = false;
        }

		this.StudentManager.UpdateStudents();

		this.CurrentDestination = this.Destinations[this.Phase];
		this.Pathfinding.target = this.Destinations[this.Phase];
		this.Pathfinding.canSearch = true;
		this.Pathfinding.canMove = true;
		this.Pathfinding.speed = 1.0f;

		this.TargetDistance = 1;
		this.ReportPhase = 0;
		this.ReportTimer = 0.0f;
		this.AlarmTimer = 0.0f;

		this.AmnesiaTimer = 10.0f;

		this.RandomAnim = this.BulliedIdleAnim;
		this.IdleAnim = this.BulliedIdleAnim;
		this.WalkAnim = this.BulliedWalkAnim;

		if (this.WitnessedBloodPool || this.WitnessedLimb || this.WitnessedWeapon)
		{
			this.Persona = this.OriginalPersona;
		}

		this.BloodPool = null;

		this.WitnessedBloodyWeapon = false;
		this.WitnessedBloodPool = false;
		this.WitnessedSomething = false;
		this.WitnessedCorpse = false;
		this.WitnessedMurder = false;
		this.WitnessedWeapon = false;
		this.WitnessedLimb = false;

		this.SmartPhone.SetActive(false);
		this.LostTeacherTrust = true;
		this.ReportingMurder = false;
		this.ReportingBlood = false;
        this.PinDownWitness = false;
        this.Distracted = true;
		this.Reacted = false;
		this.Alarmed = false;
		this.Fleeing = false;
		this.Routine = true;
		this.Halt = false;

		if (this.Club == ClubType.Council)
		{
			this.Persona = PersonaType.Dangerous;
		}

		// [af] Converted while loop to for loop.
		for (this.ID = 0; this.ID < this.Outlines.Length; this.ID++)
		{
			this.Outlines[this.ID].color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
		}
	}

    public void ForgetAboutBloodPool()
    {
        Debug.Log(this.Name + " was told to ForgetAboutBloodPool()");

        this.Subtitle.UpdateLabel(SubtitleType.StudentFarewell, 0, 3.0f);

        if (this.Club == ClubType.Cooking && this.CurrentAction == StudentActionType.ClubAction)
        {
            this.GetFoodTarget();
        }
        else
        {
            this.CurrentDestination = this.Destinations[this.Phase];
            this.Pathfinding.target = this.Destinations[this.Phase];
        }

        this.InvestigatingBloodPool = false;
        this.WitnessedBloodyWeapon = false;
        this.WitnessedBloodPool = false;
        this.WitnessedSomething = false;
        this.WitnessedWeapon = false;
        this.Distracted = false;

        if (!this.Shoving)
        {
            this.Routine = true;
        }

        this.WitnessCooldownTimer = 5;

        if (this.BloodPool != null)
        {
            if (this.CanSeeObject(this.Yandere.gameObject, this.Yandere.HeadPosition) &&
                this.BloodPool.parent == this.Yandere.RightHand)
            {
                this.YandereVisible = true;

                this.ReportTimer = 0.0f;
                this.ReportPhase = 0;
                this.Alarmed = false;
                this.Fleeing = false;
                this.Reacted = false;

                if (this.BloodPool.GetComponent<WeaponScript>() != null && this.BloodPool.GetComponent<WeaponScript>().Suspicious)
                {
                    this.WitnessCooldownTimer = 5;
                    this.AlarmTimer = 0.0f;
                    this.Alarm = 200.0f;

                    this.BecomeAlarmed();
                }
            }
        }

        this.BloodPool = null;
    }

    void SummonWitnessCamera()
    {
        this.WitnessCamera.transform.parent = this.WitnessPOV;
        this.WitnessCamera.transform.localPosition = Vector3.zero;
        this.WitnessCamera.transform.localEulerAngles = Vector3.zero;
        this.WitnessCamera.MyCamera.enabled = true;
        this.WitnessCamera.Show = true;
    }

    public void SilentlyForgetBloodPool()
    {
        Debug.Log(this.Name + " was told to SilentlyForgetBloodPool()");

        this.InvestigatingBloodPool = false;
        this.WitnessedBloodyWeapon = false;
        this.WitnessedBloodPool = false;
        this.WitnessedSomething = false;
        this.WitnessedWeapon = false;
    }
}