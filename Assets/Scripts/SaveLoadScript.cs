using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JsonFx;

public class SaveLoadScript : MonoBehaviour
{
	public StudentScript Student;

	public string SerializedData;

	public string SaveFilePath;

	public int SaveProfile;
	public int SaveSlot;

	void DetermineFilePath()
	{
		SaveProfile = GameGlobals.Profile;
		SaveSlot = PlayerPrefs.GetInt("SaveSlot");

		SaveFilePath = Application.streamingAssetsPath + "/SaveData/Profile_" + SaveProfile + "/Slot_" + SaveSlot + "/Student_" + Student.StudentID + "_Data.txt";
	}

	public void SaveData()
	{
		DetermineFilePath();

		SerializedData = JsonUtility.ToJson(Student);

		System.IO.File.WriteAllText(SaveFilePath, SerializedData);

		PlayerPrefs.SetFloat("Profile_" + SaveProfile + "_Slot_" + SaveSlot + "Student_" + Student.StudentID + "_posX", this.transform.position.x);
		PlayerPrefs.SetFloat("Profile_" + SaveProfile + "_Slot_" + SaveSlot + "Student_" + Student.StudentID + "_posY", this.transform.position.y);
		PlayerPrefs.SetFloat("Profile_" + SaveProfile + "_Slot_" + SaveSlot + "Student_" + Student.StudentID + "_posZ", this.transform.position.z);

		PlayerPrefs.SetFloat("Profile_" + SaveProfile + "_Slot_" + SaveSlot + "Student_" + Student.StudentID + "_rotX", this.transform.eulerAngles.x);
		PlayerPrefs.SetFloat("Profile_" + SaveProfile + "_Slot_" + SaveSlot + "Student_" + Student.StudentID + "_rotY", this.transform.eulerAngles.y);
		PlayerPrefs.SetFloat("Profile_" + SaveProfile + "_Slot_" + SaveSlot + "Student_" + Student.StudentID + "_rotZ", this.transform.eulerAngles.z);
	}

	public void LoadData()
	{
		DetermineFilePath();

		//SaveReferences();

		if (System.IO.File.Exists(SaveFilePath))
		{
			this.transform.position = new Vector3(
				PlayerPrefs.GetFloat("Profile_" + SaveProfile + "_Slot_" + SaveSlot + "Student_" + Student.StudentID + "_posX"),
				PlayerPrefs.GetFloat("Profile_" + SaveProfile + "_Slot_" + SaveSlot + "Student_" + Student.StudentID + "_posY"),
				PlayerPrefs.GetFloat("Profile_" + SaveProfile + "_Slot_" + SaveSlot + "Student_" + Student.StudentID + "_posZ"));

			this.transform.eulerAngles = new Vector3(
				PlayerPrefs.GetFloat("Profile_" + SaveProfile + "Slot_" + SaveSlot + "Student_" + Student.StudentID + "_rotX"),
				PlayerPrefs.GetFloat("Profile_" + SaveProfile + "Slot_" + SaveSlot + "Student_" + Student.StudentID + "_rotY"),
				PlayerPrefs.GetFloat("Profile_" + SaveProfile + "Slot_" + SaveSlot + "Student_" + Student.StudentID + "_rotZ"));

			string json = System.IO.File.ReadAllText(SaveFilePath);
			JsonUtility.FromJsonOverwrite(json, Student);
		}

		//LoadReferences();
	}

	/*
	public SelectiveGrayscale ChaseSelectiveGrayscale;
	public RiggedAccessoryAttacher LabcoatAttacher;
	public RiggedAccessoryAttacher ApronAttacher;
	public ChemistScannerScript ChemistScanner;
	public DialogueWheelScript DialogueWheel;
	public WitnessCameraScript WitnessCamera;
	public RiggedAccessoryAttacher Attacher;
	public CharacterController MyController;
	public ClubManagerScript ClubManager;
	public ShoeRemovalScript ShoeRemoval;
	public LowPolyStudentScript LowPoly;
	public DynamicGridObstacle Obstacle;
	public Animation CharacterAnimation;
	public ReputationScript Reputation;
	public CountdownScript Countdown;
	public Projector LiquidProjector;
	public CosmeticScript Cosmetic;
	public Camera DramaticCamera;
	public ARMiyukiScript Miyuki;
	public RagdollScript Ragdoll;
	public PromptScript Prompt;
	public AIPath Pathfinding;
	public TalkingScript Talk;

	public ParticleSystem DelinquentSpeechLines;
	public ParticleSystem PepperSprayEffect;
	public ParticleSystem BloodFountain;
	public ParticleSystem VomitEmitter;
	public ParticleSystem SpeechLines;
	public ParticleSystem BullyDust;
	public ParticleSystem ChalkDust;
	public ParticleSystem Hearts;

	public SkinnedMeshRenderer MyRenderer;
	public Renderer SmartPhoneScreen;
	public Renderer BookRenderer;
	public Renderer Tears;

	public Transform WeaponBagParent;
	public Transform LeftItemParent;
	public Transform DefaultTarget;
	public Transform ItemParent;
	public Transform WitnessPOV;
	public Transform RightHand;
	public Transform LeftHand;
	public Transform MyLocker;
	public Transform RightEye;
	public Transform LeftEye;
	public Transform Eyes;
	public Transform Head;
	public Transform Hips;
	public Transform Neck;

	public SphereCollider HipCollider;
	public Collider RightHandCollider;
	public Collider LeftHandCollider;
	public Collider NotFaceCollider;
	public Collider FaceCollider;

	public GameObject BullyPhotoCollider;
	public GameObject WhiteQuestionMark;
	public GameObject MiyukiGameScreen;
	public GameObject RiggedAccessory;
	public GameObject SecurityCamera;
	public GameObject RightEmptyEye;
	public GameObject LeftEmptyEye;
	public GameObject AnimatedBook;
	public GameObject Handkerchief;
	public GameObject CameraFlash;
	public GameObject ChaseCamera;
	public GameObject PepperSpray;
	public GameObject PinkSeifuku;
	public GameObject BloodSpray;
	public GameObject Sketchbook;
	public GameObject SmartPhone;
	public GameObject OccultBook;
	public GameObject Paintbrush;
	public GameObject Character;
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
	public GameObject Octodog;
	public GameObject Palette;
	public GameObject Eraser;
	public GameObject Pencil;
	public GameObject Bento;
	public GameObject Note;
	public GameObject Lid;
	public GameObject Pen;

	public ParticleSystem[] LiquidEmitters;
	public ParticleSystem[] SplashEmitters;
	public ParticleSystem[] FireEmitters;

	// Female-Only

	public AudioSource AirGuitar;

	public GameObject WateringCan;
	public GameObject Cigarette;
	public GameObject Lighter;

	public Transform LeftMiddleFinger;
	public Transform RightBreast;
	public Transform LeftBreast;
	public Transform RightDrill;
	public Transform LeftDrill;
	public Transform Spine;

	public Collider PantyCollider;
	public Collider SkirtCollider;

	public GameObject[] InstrumentBag;
	public GameObject[] ElectroSteam;
	public GameObject[] ScienceProps;
	public GameObject[] CensorSteam;
	public GameObject[] Instruments;
	public GameObject[] Chopsticks;
	public GameObject[] Drumsticks;
	public GameObject[] Fingerfood;
	public Transform[] Skirt;

	// References which should already exist within the scene

	public StudentManagerScript StudentManager;

	void SaveReferences()
	{
		//Misc

		ChaseSelectiveGrayscale = Student.ChaseSelectiveGrayscale;
		CharacterAnimation = Student.CharacterAnimation;
		LabcoatAttacher = Student.LabcoatAttacher;
		LiquidProjector = Student.LiquidProjector;
		ChemistScanner = Student.ChemistScanner;
		DramaticCamera = Student.DramaticCamera;
		ApronAttacher = Student.ApronAttacher;
		DialogueWheel = Student.DialogueWheel;
		WitnessCamera = Student.WitnessCamera;
		MyController = Student.MyController;
		ClubManager = Student.ClubManager;
		Pathfinding = Student.Pathfinding;
		ShoeRemoval = Student.ShoeRemoval;
		Countdown = Student.Countdown;
		Attacher = Student.Attacher;
		Cosmetic = Student.Cosmetic;
		Obstacle = Student.Obstacle;
		LowPoly = Student.LowPoly;
		Ragdoll = Student.Ragdoll;
		Miyuki = Student.Miyuki;
		Prompt = Student.Prompt;
		Talk = Student.Talk;

		//ParticleSystems

		DelinquentSpeechLines = Student.DelinquentSpeechLines;
		PepperSprayEffect = Student.PepperSprayEffect;
		BloodFountain = Student.BloodFountain;
		VomitEmitter = Student.VomitEmitter;
		SpeechLines = Student.SpeechLines;
		BullyDust = Student.BullyDust;
		ChalkDust = Student.ChalkDust;
		Hearts = Student.Hearts;

		//Renderers

		SmartPhoneScreen = Student.SmartPhoneScreen;
		BookRenderer = Student.BookRenderer;
		MyRenderer = Student.MyRenderer;
		Tears = Student.Tears;

		//Transforms

		WeaponBagParent = Student.WeaponBagParent;
		LeftItemParent = Student.LeftItemParent;
		DefaultTarget = Student.DefaultTarget;
		ItemParent = Student.ItemParent;
		WitnessPOV = Student.WitnessPOV;
		RightHand = Student.RightHand;
		LeftHand = Student.LeftHand;
		MyLocker = Student.MyLocker;
		RightEye = Student.RightEye;
		LeftEye = Student.LeftEye;
		Eyes = Student.Eyes;
		Head = Student.Head;
		Hips = Student.Hips;
		Neck = Student.Neck;

		//Colliders

		RightHandCollider = Student.RightHandCollider;
		LeftHandCollider = Student.LeftHandCollider;
		NotFaceCollider = Student.NotFaceCollider;
		FaceCollider = Student.FaceCollider;
		HipCollider = Student.HipCollider;

		//GameObjects

		BullyPhotoCollider = Student.BullyPhotoCollider;
		WhiteQuestionMark = Student.WhiteQuestionMark;
		MiyukiGameScreen = Student.MiyukiGameScreen;
		RiggedAccessory = Student.RiggedAccessory;
		SecurityCamera = Student.SecurityCamera;
		RightEmptyEye = Student.RightEmptyEye;
		LeftEmptyEye = Student.LeftEmptyEye;
		AnimatedBook = Student.AnimatedBook;
		Handkerchief = Student.Handkerchief;
		CameraFlash = Student.CameraFlash;
		ChaseCamera = Student.ChaseCamera;
		PepperSpray = Student.PepperSpray;
		PinkSeifuku = Student.PinkSeifuku;
		BloodSpray = Student.BloodSpray;
		Sketchbook = Student.Sketchbook;
		SmartPhone = Student.SmartPhone;
		OccultBook = Student.OccultBook;
		Paintbrush = Student.Paintbrush;
		Character = Student.Character;
		EventBook = Student.EventBook;
		Handcuffs = Student.Handcuffs;
		HealthBar = Student.HealthBar;
		OsanaHair = Student.OsanaHair;
		WeaponBag = Student.WeaponBag;
		CandyBar = Student.CandyBar;
		Earpiece = Student.Earpiece;
		Scrubber = Student.Scrubber;
		Armband = Student.Armband;
		BookBag = Student.BookBag;
		Octodog = Student.Octodog;
		Palette = Student.Palette;
		Eraser = Student.Eraser;
		Pencil = Student.Pencil;
		Bento = Student.Bento;
		Note = Student.Note;
		Lid = Student.Lid;
		Pen = Student.Pen;

		//Female-Only

		AirGuitar = Student.AirGuitar;

		PantyCollider = Student.PantyCollider;
		SkirtCollider = Student.SkirtCollider;

		LeftMiddleFinger = Student.LeftMiddleFinger;
		RightBreast = Student.RightBreast;
		LeftBreast = Student.LeftBreast;
		RightDrill = Student.RightDrill;
		LeftDrill = Student.LeftDrill;
		Spine = Student.Spine;

		WateringCan = Student.WateringCan;
		Cigarette = Student.Cigarette;
		Lighter = Student.Lighter;

		// References which should already exist within the scene

		StudentManager = Student.StudentManager;
	}

	void LoadReferences()
	{
		//Debug.Log("Student " + Student.StudentID + " is loading references now.");

		Student.SaveLoad = this;

		//Misc

		Student.ChaseSelectiveGrayscale = ChaseSelectiveGrayscale;
		Student.CharacterAnimation = CharacterAnimation;
		Student.LabcoatAttacher = LabcoatAttacher;
		Student.LiquidProjector = LiquidProjector;
		Student.ChemistScanner = ChemistScanner;
		Student.DramaticCamera = DramaticCamera;
		Student.ApronAttacher = ApronAttacher;
		Student.DialogueWheel = DialogueWheel;
		Student.WitnessCamera = WitnessCamera;
		Student.MyController = MyController;
		Student.ClubManager = ClubManager;
		Student.Pathfinding = Pathfinding;
		Student.ShoeRemoval = ShoeRemoval;
		Student.Countdown = Countdown;
		Student.Attacher = Attacher;
		Student.Cosmetic = Cosmetic;
		Student.Obstacle = Obstacle;
		Student.LowPoly = LowPoly;
		Student.Ragdoll = Ragdoll;
		Student.Miyuki = Miyuki;
		Student.Prompt = Prompt;
		Student.Talk = Talk;

		//ParticleSystems

		Student.DelinquentSpeechLines = DelinquentSpeechLines;
		Student.PepperSprayEffect = PepperSprayEffect;
		Student.BloodFountain = BloodFountain;
		Student.VomitEmitter = VomitEmitter;
		Student.SpeechLines = SpeechLines;
		Student.BullyDust = BullyDust;
		Student.ChalkDust = ChalkDust;
		Student.Hearts = Hearts;

		//Renderers

		Student.SmartPhoneScreen = SmartPhoneScreen;
		Student.BookRenderer = BookRenderer;
		Student.MyRenderer = MyRenderer;
		Student.Tears = Tears;

		//Transforms

		Student.WeaponBagParent = WeaponBagParent;
		Student.LeftItemParent = LeftItemParent;
		Student.DefaultTarget = DefaultTarget;
		Student.ItemParent = ItemParent;
		Student.WitnessPOV = WitnessPOV;
		Student.RightHand = RightHand;
		Student.LeftHand = LeftHand;
		Student.MyLocker = MyLocker;
		Student.RightEye = RightEye;
		Student.LeftEye = LeftEye;
		Student.Eyes = Eyes;
		Student.Head = Head;
		Student.Hips = Hips;
		Student.Neck = Neck;

		//Colliders

		Student.RightHandCollider = RightHandCollider;
		Student.LeftHandCollider = LeftHandCollider;
		Student.NotFaceCollider = NotFaceCollider;
		Student.FaceCollider = FaceCollider;
		Student.HipCollider = HipCollider;

		//GameObjects

		Student.BullyPhotoCollider = BullyPhotoCollider;
		Student.WhiteQuestionMark = WhiteQuestionMark;
		Student.MiyukiGameScreen = MiyukiGameScreen;
		Student.RiggedAccessory = RiggedAccessory;
		Student.SecurityCamera = SecurityCamera;
		Student.RightEmptyEye = RightEmptyEye;
		Student.LeftEmptyEye = LeftEmptyEye;
		Student.AnimatedBook = AnimatedBook;
		Student.Handkerchief = Handkerchief;
		Student.CameraFlash = CameraFlash;
		Student.ChaseCamera = ChaseCamera;
		Student.PepperSpray = PepperSpray;
		Student.PinkSeifuku = PinkSeifuku;
		Student.BloodSpray = BloodSpray;
		Student.Sketchbook = Sketchbook;
		Student.SmartPhone = SmartPhone;
		Student.OccultBook = OccultBook;
		Student.Paintbrush = Paintbrush;
		Student.Character = Character;
		Student.EventBook = EventBook;
		Student.Handcuffs = Handcuffs;
		Student.HealthBar = HealthBar;
		Student.OsanaHair = OsanaHair;
		Student.WeaponBag = WeaponBag;
		Student.CandyBar = CandyBar;
		Student.Earpiece = Earpiece;
		Student.Scrubber = Scrubber;
		Student.Armband = Armband;
		Student.BookBag = BookBag;
		Student.Octodog = Octodog;
		Student.Palette = Palette;
		Student.Eraser = Eraser;
		Student.Pencil = Pencil;
		Student.Bento = Bento;
		Student.Note = Note;
		Student.Lid = Lid;
		Student.Pen = Pen;

		Student.ScienceProps = ScienceProps;
		Student.Fingerfood = Fingerfood;

		//Female-Only

		Student.LiquidEmitters = LiquidEmitters;
		Student.SplashEmitters = SplashEmitters;
		Student.FireEmitters = FireEmitters;

		Student.PantyCollider = PantyCollider;
		Student.SkirtCollider = SkirtCollider;

		Student.AirGuitar = AirGuitar;

		Student.WateringCan = WateringCan;
		Student.Cigarette = Cigarette;
		Student.Lighter = Lighter;

		Student.LeftMiddleFinger = LeftMiddleFinger;
		Student.RightBreast = RightBreast;
		Student.LeftBreast = LeftBreast;
		Student.RightDrill = RightDrill;
		Student.LeftDrill = LeftDrill;
		Student.Spine = Spine;

		Student.SplashEmitters = SplashEmitters;
		Student.InstrumentBag = InstrumentBag;
		Student.ElectroSteam = ElectroSteam;
		Student.CensorSteam = CensorSteam;
		Student.Instruments = Instruments;
		Student.Chopsticks = Chopsticks;
		Student.Drumsticks = Drumsticks;
		Student.Skirt = Skirt;

		// References which should already exist within the scene

		Student.StudentManager = StudentManager;

		//Debug.Log("My ID number is: " + Student.StudentID);

		if ((int)Student.Club < 11)
		{
			Student.ChangingBooth = StudentManager.ChangingBooths[(int)Student.Club];
		}

		if (Student.Club == ClubType.Cooking)
		{
			Student.MyPlate = StudentManager.Plates[Student.ClubMemberID];
		}

		if (!Student.Teacher)
		{
			StudentManager.CleaningManager.GetRole(Student.StudentID);
			Student.CleaningSpot = StudentManager.CleaningManager.Spot;
			Student.CleaningRole = StudentManager.CleaningManager.Role;
		}

		if (Student.StudentID == 54)
		{
			this.Instruments[4] = this.StudentManager.DrumSet;
		}

		Student.Reputation = StudentManager.Reputation;
		Student.Yandere = StudentManager.Yandere;
		Student.Police = StudentManager.Police;
		Student.Clock = StudentManager.Clock;
		Student.JSON = StudentManager.JSON;
		Student.Seat = StudentManager.Seats[Student.Class].List[Student.JSON.Students[Student.StudentID].Seat];

		Student.Subtitle = Student.Yandere.Subtitle;

		// Re-initializing things that got un-initialized

		/*
		Student.Started = false;
		Student.Start();
		*/

		/*
		Student.GetDestinations();
		Student.CurrentDestination = Student.Destinations[Student.Phase];
		Student.Pathfinding.target = Student.Destinations[Student.Phase];

		// Teacher-Only

		/*
		if (Student.Teacher)
		{
			Student.GradingPaper = StudentManager.FacultyDesks[Student.Class];
		}
		*/
	//}
}