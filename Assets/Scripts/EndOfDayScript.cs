using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum RivalEliminationType
{
	None,
	Dead,
	Vanished,
	Arrested,
	Ruined,
	Expelled,
	Rejected
}

public class EndOfDayScript : MonoBehaviour
{
	public SecuritySystemScript SecuritySystem;
	public StudentManagerScript StudentManager;
	public WeaponManagerScript WeaponManager;
	public ClubManagerScript ClubManager;
	public HeartbrokenScript Heartbroken;
	public IncineratorScript Incinerator;
	public LoveManagerScript LoveManager;
	public RummageSpotScript RummageSpot;
	public VoidGoddessScript VoidGoddess;
	public WoodChipperScript WoodChipper;
	public ReputationScript Reputation;
	public DumpsterLidScript Dumpster;
	public CounselorScript Counselor;
	public WeaponScript MurderWeapon;
	public TranqCaseScript TranqCase;
    public YandereScript Yandere;
	public RagdollScript Corpse;
	public StudentScript Senpai;
	public StudentScript Patsy;
	public PoliceScript Police;
	public Transform EODCamera;
	public StudentScript Rival;
    public ClassScript Class;
    public ClockScript Clock;
	public JsonScript JSON;

	public GardenHoleScript[] GardenHoles;
	public StudentScript[] WitnessList;
	public Animation[] CopAnimation;

	public GameObject MainCamera;

	public UISprite EndOfDayDarkness;
	public UILabel Label;

	public bool PreviouslyActivated = false;
	public bool PoliceArrived = false;
	public bool RaibaruLoner = false;
	public bool StopMourning = false;
	public bool ClubClosed = false;
	public bool ClubKicked = false;
	public bool ErectFence = false;
	public bool GameOver = false;
	public bool Darken = false;

	public int ClothingWithRedPaint = 0;
	public int FragileTarget = 0;
	public int EyeWitnesses = 0;
	public int NewFriends = 0;
	public int DeadPerps = 0;
	public int Arrests = 0;
	public int Corpses = 0;
	public int Victims = 0;
	public int Weapons = 0;
	public int Phase = 1;

	public int MatchmakingGifts = 0;
	public int SenpaiGifts = 0;

	public int WeaponID = 0;
	public int ArrestID = 0;
	public int ClubID = 0; // [af] Used as an index, not a ClubType.
	public int ID = 0;

	public string[] ClubNames;

	public int[] VictimArray;
	public ClubType[] ClubArray;

	private SaveFile saveFile;

	public GameObject TextWindow;
	public GameObject Cops;
	public GameObject SearchingCop;
	public GameObject MurderScene;
	public GameObject ShruggingCops;
	public GameObject TabletCop;
	public GameObject SecuritySystemScene;
	public GameObject OpenTranqCase;
	public GameObject ClosedTranqCase;
	public GameObject GaudyRing;
	public GameObject AnswerSheet;
	public GameObject Fence;
	public GameObject SCP;
	public GameObject ArrestingCops;
	public GameObject Mask;
	public GameObject EyeWitnessScene;
	public GameObject ScaredCops;

	public StudentScript KidnappedVictim;

	public Renderer TabletPortrait;

	public Transform CardboardBox;

	public RivalEliminationType RivalEliminationMethod;

	public Vector3 YandereInitialPosition;
	public Quaternion YandereInitialRotation;

	public void Start()
	{
        GameGlobals.PoliceYesterday = false;

        this.YandereInitialPosition = this.Yandere.transform.position;
		this.YandereInitialRotation = this.Yandere.transform.rotation;

		if (GameGlobals.SenpaiMourning)
		{
			this.StopMourning = true;
		}

		this.Yandere.MainCamera.gameObject.SetActive(false);

		this.EndOfDayDarkness.color = new Color(
			this.EndOfDayDarkness.color.r,
			this.EndOfDayDarkness.color.g,
			this.EndOfDayDarkness.color.b,
			1.0f);

		this.PreviouslyActivated = true;
		this.GetComponent<AudioSource>().volume = 0.0f;

		this.Clock.enabled = false;

		Clock.MainLight.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		RenderSettings.ambientLight = new Color(0.75f, 0.75f, 0.75f, 1.0f);
		RenderSettings.skybox.SetColor("_Tint", new Color(0.50f, 0.50f, 0.50f));

		this.UpdateScene();

		this.CopAnimation[5]["idleShort_00"].speed = Random.Range(.9f, 1.1f);
		this.CopAnimation[6]["idleShort_00"].speed = Random.Range(.9f, 1.1f);
		this.CopAnimation[7]["idleShort_00"].speed = Random.Range(.9f, 1.1f);

		Time.timeScale = 1;

		int CreepyID = 1;

		while (CreepyID < 6)
		{
			Yandere.CharacterAnimation[Yandere.CreepyIdles[CreepyID]].weight = 0;
			Yandere.CharacterAnimation[Yandere.CreepyWalks[CreepyID]].weight = 0;
			CreepyID++;
		}

		Debug.Log("BloodParent.childCount is: " + Police.BloodParent.childCount);

		foreach (Transform child in Police.BloodParent)
		{
			PickUpScript PickUp = child.gameObject.GetComponent<PickUpScript>();

			if (PickUp != null)
			{
				if (PickUp.RedPaint)
				{
					ClothingWithRedPaint++;
				}
			}
		}

		Debug.Log("Clothing with red paint is: " + ClothingWithRedPaint);
	}

	void Update()
	{
		this.Yandere.UpdateSlouch();

        //Debug.Log("Number of blood stains present at school is: " + this.Police.BloodParent.childCount);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerPrefs.SetInt("LoadingSave", 1);
            PlayerPrefs.SetInt("SaveSlot", GameGlobals.MostRecentSlot);

            for (int ID = 0; ID < this.StudentManager.NPCsTotal; ID++)
            {
                if (StudentGlobals.GetStudentDying(ID))
                {
                    StudentGlobals.SetStudentDying(ID, false);
                }
            }

            SceneManager.LoadScene(SceneNames.LoadingScene);
        }

        if (Input.GetKeyDown("space"))
		{
			this.EndOfDayDarkness.color = new Color(0, 0, 0, 1.0f);
			this.Darken = true;
		}

		if (this.EndOfDayDarkness.color.a == 0.0f)
		{
			if (Input.GetButtonDown(InputNames.Xbox_A))
			{
				this.Darken = true;
			}
		}

		if (this.Darken)
		{
			this.EndOfDayDarkness.color = new Color(
				this.EndOfDayDarkness.color.r,
				this.EndOfDayDarkness.color.g,
				this.EndOfDayDarkness.color.b,
				Mathf.MoveTowards(this.EndOfDayDarkness.color.a, 1.0f, Time.deltaTime * 2));

			if (this.EndOfDayDarkness.color.a == 1.0f)
			{
				if (this.Senpai == null)
				{
					if (this.StudentManager.Students[1] != null)
					{
						this.Senpai = this.StudentManager.Students[1];
						this.Senpai.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
						this.Senpai.CharacterAnimation.enabled = true;
					}
				}

				if (this.Senpai != null)
				{
					this.Senpai.gameObject.SetActive(false);
				}

				if (this.Rival == null)
				{
					if (this.StudentManager.Students[this.StudentManager.RivalID] != null)
					{
						this.Rival = this.StudentManager.Students[this.StudentManager.RivalID];
						this.Rival.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
						this.Rival.CharacterAnimation.enabled = true;
					}
				}

				if (this.Rival != null)
				{
					this.Rival.gameObject.SetActive(false);
				}

				this.Yandere.transform.parent = null;
				this.Yandere.transform.position = new Vector3(0, 0, -75);

				this.EODCamera.localPosition = new Vector3(1, 1.8f, -2.5f);
				this.EODCamera.localEulerAngles = new Vector3(22.5f, -22.5f, 0);

				if (this.KidnappedVictim != null)
				{
					KidnappedVictim.gameObject.SetActive(false);
				}

				this.CardboardBox.parent = null;

				this.SearchingCop.SetActive(false);
				this.MurderScene.SetActive(false);
				this.Cops.SetActive(false);
				this.TabletCop.SetActive(false);
				this.ShruggingCops.SetActive(false);
				this.SecuritySystemScene.SetActive(false);
				this.OpenTranqCase.SetActive(false);
				this.ClosedTranqCase.SetActive(false);
				this.GaudyRing.SetActive(false);
				this.AnswerSheet.SetActive(false);
				this.Fence.SetActive(false);
				this.SCP.SetActive(false);
				this.ArrestingCops.SetActive(false);
				this.Mask.SetActive(false);
				this.EyeWitnessScene.SetActive(false);
				this.ScaredCops.SetActive(false);

				if (this.WitnessList[1] != null){this.WitnessList[1].gameObject.SetActive(false);}
				if (this.WitnessList[2] != null){this.WitnessList[2].gameObject.SetActive(false);}
				if (this.WitnessList[3] != null){this.WitnessList[3].gameObject.SetActive(false);}
				if (this.WitnessList[4] != null){this.WitnessList[4].gameObject.SetActive(false);}
				if (this.WitnessList[5] != null){this.WitnessList[5].gameObject.SetActive(false);}

				if (this.Patsy != null)
				{
					this.Patsy.gameObject.SetActive(false);
				}

				if (!this.GameOver)
				{
					this.Darken = false;
					this.UpdateScene();
				}
				else
				{
					this.Heartbroken.transform.parent.transform.parent = null;

					this.Heartbroken.transform.parent.gameObject.SetActive(true);
					this.Heartbroken.Cursor.HeartbrokenCamera.depth = 6;

                    if (this.Police.Deaths + PlayerGlobals.Kills > 50)
                    {
                        this.Heartbroken.Noticed = true;
                    }
                    else
                    {
					    this.Heartbroken.Noticed = false;
					    this.Heartbroken.Arrested = true;
                    }

                    this.MainCamera.SetActive(false);
					this.gameObject.SetActive(false);

					Time.timeScale = 1.0f;
				}
			}
		}
		else
		{
			this.EndOfDayDarkness.color = new Color(
				this.EndOfDayDarkness.color.r,
				this.EndOfDayDarkness.color.g,
				this.EndOfDayDarkness.color.b,
				Mathf.MoveTowards(this.EndOfDayDarkness.color.a, 0.0f, Time.deltaTime * 2));
		}

		AudioSource audioSource = this.GetComponent<AudioSource>();
		audioSource.volume = Mathf.MoveTowards(audioSource.volume, 1.0f, Time.deltaTime * 2);

		if (this.WitnessList[2] != null){this.WitnessList[2].CharacterAnimation.Play(this.WitnessList[2].IdleAnim);}
		if (this.WitnessList[3] != null){this.WitnessList[3].CharacterAnimation.Play(this.WitnessList[3].IdleAnim);}
		if (this.WitnessList[4] != null){this.WitnessList[4].CharacterAnimation.Play(this.WitnessList[4].IdleAnim);}
		if (this.WitnessList[5] != null){this.WitnessList[5].CharacterAnimation.Play(this.WitnessList[5].IdleAnim);}
	}

	public void UpdateScene()
	{
		this.Label.color = new Color(0, 0, 0, 1);

		for (this.ID = 0; this.ID < this.WeaponManager.Weapons.Length; this.ID++)
		{
			if (this.WeaponManager.Weapons[this.ID] != null)
			{
				this.WeaponManager.Weapons[this.ID].gameObject.SetActive(false);
			}
		}

		if (this.PoliceArrived)
		{
			if (Input.GetKeyDown(KeyCode.Backspace))
			{
				this.Finish();
			}

			////////////////////////////////////////
			///// The police arrive at school. /////
			////////////////////////////////////////

			if (this.Phase == 1)
			{
                GameGlobals.PoliceYesterday = true;

				this.CopAnimation[1]["walk_00"].speed = Random.Range(.9f, 1.1f);
				this.CopAnimation[2]["walk_00"].speed = Random.Range(.9f, 1.1f);
				this.CopAnimation[3]["walk_00"].speed = Random.Range(.9f, 1.1f);

				this.Cops.SetActive(true);

				if (this.Yandere.Egg)
				{
					this.Label.text = "The police arrive at school.";
					this.Phase = 999;
				}
				else if (this.Police.PoisonScene)
				{
					this.Label.text = "The police and the paramedics arrive at school.";
					this.Phase = 103;
				}
				else if (this.Police.DrownVictims == 1)
				{
					this.Label.text = "The police arrive at school.";
					this.Phase = 104;
				}
				else if (this.Police.ElectroScene)
				{
					this.Label.text = "The police arrive at school.";
					this.Phase = 105;
				}
				else if (this.Police.MurderSuicideScene)
				{
					this.Label.text = "The police arrive at school, and discover what appears to be the scene of a murder-suicide.";
					this.Phase++;
				}
				else
				{
					this.Label.text = "The police arrive at school.";

					if (this.Police.SuicideScene)
					{
						this.Phase = 102;
					}
					else
					{
						this.Phase++;
					}
				}
			}

			//////////////////////////////////////////
			///// The police search for corpses. /////
			//////////////////////////////////////////

			else if (this.Phase == 2)
			{
				if (this.Police.Corpses == 0)
				{
					if (!this.Police.PoisonScene && !this.Police.SuicideScene)
					{
						if (this.Police.LimbParent.childCount > 0)
						{
							if (this.Police.LimbParent.childCount == 1)
							{
								this.Label.text = "The police find a severed body part at school.";
							}
							else
							{
								this.Label.text = "The police find multiple severed body parts at school.";
							}

							this.MurderScene.SetActive(true);
						}
						else
						{
							this.SearchingCop.SetActive(true);

							if (this.Police.BloodParent.childCount - this.ClothingWithRedPaint > 0)
							{
								this.Label.text = "The police find mysterious blood stains, but are unable to locate any corpses on school grounds.";
							}
							else
							{
								if (this.ClothingWithRedPaint == 0)
								{
									this.Label.text = "The police are unable to locate any corpses on school grounds.";
								}
								else
								{
									this.Label.text = "The police find clothing that is stained with red paint, but are unable to locate any actual blood stains, and cannot locate any corpses, either.";
								}
							}
						}

						this.Phase++;
					}
					else
					{
						this.SearchingCop.SetActive(true);

						this.Label.text = "The police are unable to locate any other corpses on school grounds.";
						this.Phase++;
					}
				}
				else
				{
					this.MurderScene.SetActive(true);

					List<string> corpseNames = new List<string>();

					foreach (RagdollScript corpse in this.Police.CorpseList)
					{
						if (corpse != null)
						{
							if (!corpse.Disposed)
							{
								if (corpse.Student.StudentID == this.StudentManager.RivalID)
								{
									this.RivalEliminationMethod = RivalEliminationType.Dead;
								}

								this.VictimArray[this.Corpses] = corpse.Student.StudentID;
								corpseNames.Add(corpse.Student.Name);
								this.Corpses++;
							}
						}
					}

					// [af] Sort corpse names alphabetically.
					corpseNames.Sort();

					string policePrefixText = "The police discover the corpse" +
						((corpseNames.Count == 1) ? string.Empty : "s") + " of ";

					// [af] Set the label text depending on how many corpses there are 
					// (guaranteed to be at least 1 corpse).
					if (corpseNames.Count == 1)
					{
						this.Label.text = policePrefixText + corpseNames[0] + ".";
					}
					else if (corpseNames.Count == 2)
					{
						this.Label.text = policePrefixText + corpseNames[0] + " and " + 
							corpseNames[1] + ".";
					}
					else if (corpseNames.Count < 6)
					{
						this.Label.text = "The police discover multiple corpses on school grounds.";

						// More than three corpses
						StringBuilder sb = new StringBuilder();
						for (int i = 0; i < (corpseNames.Count - 1); i++)
						{
							sb.Append(corpseNames[i] + ", ");
						}

						sb.Append("and " + corpseNames[corpseNames.Count - 1] + ".");
						this.Label.text = policePrefixText + sb.ToString();
					}
					else
					{
						this.Label.text = "The police discover more than five corpses on school grounds.";
					}

					this.Phase++;
				}
			}

			/////////////////////////////////////////////////
			///// The police search for murder weapons. /////
			/////////////////////////////////////////////////

			else if (this.Phase == 3)
			{
				this.WeaponManager.CheckWeapons();

				if (this.WeaponManager.MurderWeapons == 0)
				{
					this.ShruggingCops.SetActive(true);

					if (this.Weapons == 0)
					{
						this.Label.text = "The police are unable to locate any murder weapons.";
						this.Phase += 2;
					}
					else
					{
						this.Phase += 2;
						this.UpdateScene();
					}
				}
				else
				{
					this.MurderWeapon = null;

					// [af] Converted while loop to for loop.
					for (this.ID = 0; this.ID < this.WeaponManager.Weapons.Length; this.ID++)
					{
						if (this.MurderWeapon == null)
						{
							WeaponScript weapon = this.WeaponManager.Weapons[this.ID];

							if (weapon != null)
							{
								if (weapon.Blood.enabled)
								{
									if (!weapon.AlreadyExamined)
									{
										weapon.gameObject.SetActive(true);
										weapon.AlreadyExamined = true;
										this.MurderWeapon = weapon;
										this.WeaponID = this.ID;
									}
									else
									{
										weapon.gameObject.SetActive(false);
									}
								}
							}
						}
					}					

					List<string> victimNames = new List<string>();

					// [af] Converted while loop to for loop.
					for (this.ID = 0; this.ID < this.MurderWeapon.Victims.Length; this.ID++)
					{
						if (this.MurderWeapon.Victims[this.ID])
						{							
							victimNames.Add(this.JSON.Students[this.ID].Name);
						}
					}

					// [af] Sort victim names alphabetically.
					victimNames.Sort();

					this.Victims = victimNames.Count;

					// [af] Make the displayed weapon name a little prettier (instead of something 
					// like "a scissors"). This code could be refined to use some traits of the
					// weapon that determine whether it's actually considered "plural".
					string weaponName = this.MurderWeapon.Name;
					bool weaponIsPlural = weaponName[weaponName.Length - 1] == 's';
					string weaponContext = !weaponIsPlural ?
						("a " + weaponName.ToLower() + " that is") :
						(weaponName.ToLower() + " that are");
					string policePrefixText = "The police discover " + weaponContext +
						" stained with the blood of ";

					// [af] Set the label text depending on the number of victims.
					if (victimNames.Count == 1)
					{
						this.Label.text = policePrefixText + victimNames[0] + ".";
					}
					else if (victimNames.Count == 2)
					{
						this.Label.text = policePrefixText + victimNames[0] + " and " +
							victimNames[1] + ".";
					}
					else
					{
						// [af] Several victims.
						StringBuilder sb = new StringBuilder();
						for (int i = 0; i < (victimNames.Count - 1); i++)
						{
							sb.Append(victimNames[i] + ", ");
						}

						sb.Append("and " + victimNames[victimNames.Count - 1] + ".");
						this.Label.text = policePrefixText + sb.ToString();
					}

					this.Weapons++;
					this.Phase++;

					this.MurderWeapon.transform.parent = this.transform;
					this.MurderWeapon.transform.localPosition = new Vector3(.6f, 1.4f, -1.5f);
					this.MurderWeapon.transform.localEulerAngles = new Vector3(-45, 90, -90);

					//GameObjectUtils.SetLayerRecursively(this.MurderWeapon.gameObject, 0);
					this.MurderWeapon.MyRigidbody.useGravity = false;
					this.MurderWeapon.Rotate = true;
				}
			}

			///////////////////////////////////////////////
			///// The police search for fingerprints. /////
			///////////////////////////////////////////////

			else if (this.Phase == 4)
			{
				if (this.MurderWeapon.FingerprintID == 0)
				{
					this.ShruggingCops.SetActive(true);

					this.Label.text = "The police find no fingerprints on the weapon.";
					this.Phase = 3;
				}
				else if (this.MurderWeapon.FingerprintID == 100)
				{
					this.TeleportYandere();
					this.Yandere.CharacterAnimation.Play("f02_disappointed_00");

					this.Label.text = "The police find Ayano's fingerprints on the weapon.";
					this.Phase = 100;
				}
				else
				{
					int Victim = this.WeaponManager.Weapons[this.WeaponID].FingerprintID;

					this.TabletCop.SetActive(true);
					this.CopAnimation[4]["scienceTablet_00"].speed = 0;
					this.TabletPortrait.material.mainTexture = this.VoidGoddess.Portraits[Victim].mainTexture;

					this.Label.text = "The police find the fingerprints of " +
						this.JSON.Students[Victim].Name + " on the weapon.";
					this.Phase = 101;
				}
			}

			///////////////////////////////////////////////////////////////
			///// The police find a cell phone with damning evidence. /////
			///////////////////////////////////////////////////////////////

			else if (this.Phase == 5)
			{
				if (this.Police.PhotoEvidence > 0)
				{
					this.TeleportYandere();
					this.Yandere.CharacterAnimation.Play("f02_disappointed_00");

					this.Label.text = "The police find a smartphone with photographic evidence of Ayano committing a crime.";
					this.Phase = 100;
				}
				else
				{
					this.Phase++;
					this.UpdateScene();
				}
			}

			////////////////////////////////////////////////////////
			///// The police check security camera recordings. /////
			////////////////////////////////////////////////////////

			else if (this.Phase == 6)
			{
				if (SchoolGlobals.HighSecurity)
				{
					this.SecuritySystemScene.SetActive(true);

					if (this.SecuritySystem.Evidence == false)
					{
						this.Label.text = "The police investigate the security camera recordings, but cannot find anything incriminating in the footage.";
						this.Phase++;
					}
					else
					{
						if (this.SecuritySystem.Masked == false)
						{
							this.Label.text = "The police investigate the security camera recordings, and find incriminating footage of Ayano.";
							this.Phase = 100;
						}
						else
						{
							this.Label.text = "The police investigate the security camera recordings, and find footage of a suspicious masked person.";
							this.Police.MaskReported = true;
							this.Phase++;
						}
					}
				}
				else
				{
					this.Phase++;
					this.UpdateScene();
				}
			}

			//////////////////////////////////////////////
			///// The police hear from eyewitnesses. /////
			//////////////////////////////////////////////

			else if (this.Phase == 7)
			{
				for (this.ID = 1; this.ID < this.StudentManager.Students.Length; this.ID++)
				{
					if (this.StudentManager.Students[ID] != null && 
						//this.StudentManager.Students[ID].gameObject.activeInHierarchy &&
						this.StudentManager.Students[ID].Alive &&
						this.StudentManager.Students[ID].Persona != PersonaType.Coward &&
						this.StudentManager.Students[ID].Persona != PersonaType.Spiteful &&
						this.StudentManager.Students[ID].Club != ClubType.Delinquent &&
						!this.StudentManager.Students[ID].SawMask)
					{
						if (this.StudentManager.Students[ID].WitnessedMurder)
						{
							this.EyeWitnesses++;
							this.WitnessList[this.EyeWitnesses] = this.StudentManager.Students[ID];
						}
					}
				}	

				if (this.EyeWitnesses > 0)
				{
					DisableThings(this.WitnessList[1]);
					DisableThings(this.WitnessList[2]);
					DisableThings(this.WitnessList[3]);
					DisableThings(this.WitnessList[4]);
					DisableThings(this.WitnessList[5]);

					this.WitnessList[1].transform.localPosition = Vector3.zero;
					if (this.WitnessList[2] != null){this.WitnessList[2].transform.localPosition = new Vector3(-1, 0, -.5f);}
					if (this.WitnessList[3] != null){this.WitnessList[3].transform.localPosition = new Vector3(1, 0, -.5f);}
					if (this.WitnessList[4] != null){this.WitnessList[4].transform.localPosition = new Vector3(-2f, 0, -1);}
					if (this.WitnessList[5] != null){this.WitnessList[5].transform.localPosition = new Vector3(1.5f, 0, -1);}

					if (this.WitnessList[1].Male)
					{
						this.WitnessList[1].CharacterAnimation.Play("carefreeTalk_02");
					}
					else
					{
						this.WitnessList[1].CharacterAnimation.Play("f02_carefreeTalk_02");
					}

					this.EyeWitnessScene.SetActive(true);

					if (this.EyeWitnesses == 1)
					{
						this.Label.text = "One student accuses Ayano of murder, but nobody else can corroborate that students' claims, " +
										  "so the police are unable to develop reasonable justification to arrest Ayano.";
						this.Phase++;
					}
					else if (this.EyeWitnesses < 5)
					{
						this.Label.text = "Several students accuse Ayano of murder, " +
						                  "but there are not enough witnesses to provide the police with reasonable justification to arrest her.";
						this.Phase++;
					}
					else
					{
						this.Label.text = "Numerous students accuse Ayano of murder, " +
										  "providing the police with enough justification to arrest her.";
						this.Phase = 100;
					}
				}
				else
				{
					this.Phase++;
					this.UpdateScene();
				}
			}

			//////////////////////////////////////
			///// The police question Ayano. /////
			//////////////////////////////////////

			else if (this.Phase == 8)
			{
				this.ShruggingCops.SetActive(false);

				if (this.Yandere.Sanity > 33.33333f)
				{
					if (this.Yandere.Bloodiness > 0.0f && !this.Yandere.RedPaint ||
						this.Yandere.Gloved &&
						this.Yandere.Gloves.Blood.enabled)
					{
						if (this.Arrests == 0)
						{
							this.TeleportYandere();
							this.Yandere.CharacterAnimation.Play("f02_disappointed_00");

							this.Label.text = "The police notice that Ayano's clothing is bloody." + " " +
								"They confirm that the blood is not hers." + " " +
								"Ayano is unable to convince the police that she did not commit murder.";
							this.Phase = 100;
						}
						else
						{
							this.TeleportYandere();
							this.Yandere.CharacterAnimation["YandereConfessionRejected"].time = 4;
							this.Yandere.CharacterAnimation.Play("YandereConfessionRejected");

							this.Label.text = "The police notice that Ayano's clothing is bloody." + " " +
								"They confirm that the blood is not hers." + " " +
								"Ayano is able to convince the police that she was splashed with blood while witnessing a murder.";

							if (!this.TranqCase.Occupied)
							{
								this.Phase += 2;
							}
							else
							{
								this.Phase++;
							}
						}
					}
					else if (this.Police.BloodyClothing - this.ClothingWithRedPaint > 0)
					{
						this.TeleportYandere();
						this.Yandere.CharacterAnimation.Play("f02_disappointed_00");

						this.Label.text = "The police find bloody clothing that has traces of Ayano's DNA." + " " +
							"Ayano is unable to convince the police that she did not commit murder.";
						this.Phase = 100;
					}
					else
					{
						this.TeleportYandere();
						this.Yandere.CharacterAnimation["YandereConfessionRejected"].time = 4;
						this.Yandere.CharacterAnimation.Play("YandereConfessionRejected");

						this.Label.text = "The police question all students in the school, including Ayano. The police are unable to link Ayano to any crimes.";

						if (!this.TranqCase.Occupied)
						{
							this.Phase += 2;
						}
						else
						{
							if (this.TranqCase.VictimID == this.ArrestID)
							{
								this.Phase += 2;
							}
							else
							{
								this.Phase++;
							}
						}
					}
				}
				else
				{
					this.TeleportYandere();
					this.Yandere.CharacterAnimation.Play("f02_disappointed_00");

					if (this.Yandere.Bloodiness == 0)
					{
						this.Label.text = "The police question Ayano, who exhibits extremely unusual behavior." + " " +
							"The police decide to investigate Ayano further, and eventually learn of her crimes.";
						this.Phase = 100;
					}
					else
					{
						this.Label.text = "The police notice that Ayano is covered in blood and exhibiting extremely unusual behavior." + " " +
							"The police decide to investigate Ayano further, and eventually learn of her crimes.";
						this.Phase = 100;
					}
				}
			}

			////////////////////////////////////////////
			///// The police find a girl in a box. /////
			////////////////////////////////////////////

			else if (this.Phase == 9)
			{
				this.KidnappedVictim = this.StudentManager.Students[this.TranqCase.VictimID];
				this.KidnappedVictim.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
				this.KidnappedVictim.CharacterAnimation.enabled = true;

				this.KidnappedVictim.gameObject.SetActive(true);
				this.KidnappedVictim.Ragdoll.Zs.SetActive(false);
				this.KidnappedVictim.transform.parent = this.transform;
				this.KidnappedVictim.transform.localPosition = new Vector3(0, .145f, 0);
				this.KidnappedVictim.transform.localEulerAngles = new Vector3(0, 90, 0);
				this.KidnappedVictim.CharacterAnimation.Play("f02_sit_06");
				this.KidnappedVictim.WhiteQuestionMark.SetActive(true);

				this.OpenTranqCase.SetActive(true);

				this.Label.text = "The police discover " + this.JSON.Students[this.TranqCase.VictimID].Name +
					" inside of a musical instrument case. However, she is unable to recall how she got inside of the case." + " " +
					"The police are unable to determine what happened.";

				StudentGlobals.SetStudentKidnapped(this.TranqCase.VictimID, false);
				StudentGlobals.SetStudentMissing(this.TranqCase.VictimID, false);
				this.TranqCase.VictimClubType = ClubType.None;
				this.TranqCase.VictimID = 0;

				this.TranqCase.Occupied = false;
				this.Phase++;
			}

			//////////////////////////////////
			///// The school bans masks. /////
			//////////////////////////////////

			else if (this.Phase == 10)
			{
				if (this.Police.MaskReported)
				{
					this.Mask.SetActive(true);

					GameGlobals.MasksBanned = true;

					if (this.SecuritySystem.Masked)
					{
						this.Label.text = "In security camera footage, the killer was wearing a mask." + " " +
							"As a result, the police are unable to gather meaningful information about the murderer." + " " +
							"To prevent this from ever happening again, the Headmaster decides to ban all masks from the school from this day forward.";
					}
					else
					{
						this.Label.text = "Witnesses state that the killer was wearing a mask." + " " +
							"As a result, the police are unable to gather meaningful information about the murderer." + " " +
							"To prevent this from ever happening again, the Headmaster decides to ban all masks from the school from this day forward.";
					}	

					this.Police.MaskReported = false;
					this.Phase++;
				}
				else
				{
					this.Phase++;
					this.UpdateScene();
				}
			}

			/////////////////////////////
			///// The police leave. /////
			/////////////////////////////

			else if (this.Phase == 11)
			{
				this.Cops.transform.eulerAngles = new Vector3(0, 180, 0);
				this.Cops.SetActive(true);

				if (this.Arrests == 0)
				{
					if (this.DeadPerps == 0)
					{
						this.Label.text = "The police do not have enough evidence to perform an arrest." + " " +
							"The police investigation ends, and students are free to leave.";
					}
					else
					{
						this.Label.text = "The police conclude that a murder-suicide took place, but are unable to take any further action." + " " +
							"The police investigation ends, and students are free to leave.";
					}
				}
				else if (this.Arrests == 1)
				{
					this.Label.text = "The police believe that they have arrested the perpetrator of the crime." + " " +
						"The police investigation ends, and students are free to leave.";
				}
				else
				{
					this.Label.text = "The police believe that they have arrested the perpetrators of the crimes." + " " +
						"The police investigation ends, and students are free to leave.";
				}

				if (this.StudentManager.RivalEliminated)
				{
					this.Phase++;
				}
				else
				{
					if (DateGlobals.Weekday == System.DayOfWeek.Friday)
					{
						this.Phase = 22;
					}
					else
					{
						this.Phase += 2;
					}
				}
			}

			/////////////////////////////////////////////
			///// Senpai reacts to a rival's death. /////
			/////////////////////////////////////////////

			else if (this.Phase == 12)
			{
				this.Senpai.enabled = false;
				this.Senpai.transform.parent = this.transform;
				this.Senpai.gameObject.SetActive(true);
				this.Senpai.transform.localPosition = new Vector3(0, 0, 0);
				this.Senpai.transform.localEulerAngles = new Vector3(0, 0, 0);
				this.Senpai.EmptyHands();

				Physics.SyncTransforms();

				string PostText = "";

				if (this.Yandere.Egg)
				{
					if (this.StudentManager.Students[11] != null && !this.StudentManager.Students[11].Alive)
					{
						this.RivalEliminationMethod = RivalEliminationType.Dead;
					}
				}

				if (this.RivalEliminationMethod == RivalEliminationType.Dead)
				{
					this.Senpai.CharacterAnimation.Play("kneelCry_00");

					if (DateGlobals.Weekday != System.DayOfWeek.Friday)
					{
						PostText = "\n" + "Senpai will stay home from school for one day to mourn Osana's death.";
						GameGlobals.SenpaiMourning = true;
					}

					this.Label.text = "Senpai is absolutely devastated by the death of his childhood friend. His mental stability has been greatly affected." + PostText;
				}
				else
				{
					this.Senpai.transform.localEulerAngles = new Vector3(0, 180, 0);

					if (this.RivalEliminationMethod == RivalEliminationType.Vanished)
					{
						this.Senpai.CharacterAnimation.Play(this.Senpai.BulliedIdleAnim);
						this.Label.text = "Senpai is concerned about the sudden disappearance of his childhood friend. His mental stability has been slightly affected.";
					}
					else if (this.RivalEliminationMethod == RivalEliminationType.Arrested)
					{
						this.Senpai.CharacterAnimation["refuse_02"].speed = .5f;
						this.Senpai.CharacterAnimation.Play("refuse_02");
						this.Label.text = "Senpai is disgusted to learn that his childhood friend would actually commit murder. He is deeply disappointed in her.";
					}
					else if (this.RivalEliminationMethod == RivalEliminationType.Ruined)
					{
						this.Senpai.CharacterAnimation["refuse_02"].speed = .5f;
						this.Senpai.CharacterAnimation.Play("refuse_02");
						this.Label.text = "Senpai is disturbed by the rumors circulating about his childhood friend. He is deeply disappointed in her.";
					}
					else if (this.RivalEliminationMethod == RivalEliminationType.Expelled)
					{
						this.Senpai.CharacterAnimation.Play("surprisedPose_00");
						this.Label.text = "Senpai is shocked by the expulsion of his childhood friend. He is deeply disappointed in her.";
					}
					else if (this.RivalEliminationMethod == RivalEliminationType.Rejected)
					{
						this.Senpai.CharacterAnimation.Play(this.Senpai.BulliedIdleAnim);
						this.Label.text = "Senpai feels guilty for turning down Osana's feelings, but also he knows that he cannot take back what has been said.";
					}
				}

				this.Phase++;
			}

			////////////////////////////////
			///// Ayano returns home. /////
			///////////////////////////////

			else if (this.Phase == 13)
			{
				this.Senpai.enabled = false;
				this.Senpai.transform.parent = this.transform;
				this.Senpai.gameObject.SetActive(true);
				this.Senpai.transform.localPosition = new Vector3(0, 0, 0);
				this.Senpai.transform.localEulerAngles = new Vector3(0, 180, 0);
				this.Senpai.EmptyHands();

				if (this.StudentManager.RivalEliminated)
				{
					this.Senpai.CharacterAnimation.Play(this.Senpai.BulliedWalkAnim);
				}
				else
				{
					this.Senpai.CharacterAnimation.Play(this.Senpai.WalkAnim);
				}

				this.Yandere.LookAt.enabled = true;
				this.Yandere.MyController.enabled = false;
				this.Yandere.transform.parent = this.transform;
				this.Yandere.transform.localPosition = new Vector3(2.5f, 0, 2.5f);
				this.Yandere.transform.localEulerAngles = new Vector3(0, 180, 0);
				this.Yandere.CharacterAnimation.Play(this.Yandere.WalkAnim);

				this.Label.text = "Ayano stalks Senpai until he has returned home, and then returns to her own home.";

				if (GameGlobals.SenpaiMourning)
				{
					this.Senpai.gameObject.SetActive(false);

					this.Yandere.LookAt.enabled = false;
					this.Yandere.transform.localPosition = new Vector3(0, 0, 0);
					this.Yandere.transform.localEulerAngles = new Vector3(0, 180, 0);

					this.Label.text = "Ayano returns home, thinking of Senpai every step of the way.";
				}

				Physics.SyncTransforms();

				this.Phase++;
			}

			/////////////////////////////////////////
			///// Check for Guidance Counselor. /////
			/////////////////////////////////////////

			else if (this.Phase == 14)
			{
				Debug.Log ("We're currently in the End-of-Day sequence, checking to see if the Counselor has to lecture anyone.");

				if (!StudentGlobals.GetStudentDying(11) && !StudentGlobals.GetStudentDead(11) &&
					!StudentGlobals.GetStudentArrested(11))
				{
					Debug.Log ("Osana is not dying, dead, or arrested.");

					Debug.Log ("Counselor.LectureID is: " + this.Counselor.LectureID);

					if (this.Counselor.LectureID > 0)
					{
						this.Yandere.gameObject.SetActive(false);

						int DisableID = 1;

						while (DisableID < 101)
						{
							StudentManager.DisableStudent(DisableID);
							DisableID++;
						}

						this.EODCamera.position = new Vector3(-18.5f, 1, 6.5f);
						this.EODCamera.eulerAngles = new Vector3(0, -45, 0);

						this.Counselor.Lecturing = true;
						this.enabled = false;

						Debug.Log ("The counselor is going to lecture somebody! Exiting End-of-Day sequence.");
					}
					else
					{
						this.Phase++;
						this.UpdateScene();
					}
				}
				else
				{
					this.Phase++;
					this.UpdateScene();
				}
			}

			//////////////////////////////////////
			///// Check for schemes failing. /////
			//////////////////////////////////////

			else if (this.Phase == 15)
			{
				Debug.Log ("We've moved on, and now we're checking to see if any schemes are failing.");

				this.EODCamera.localPosition = new Vector3(1, 1.8f, -2.5f);
				this.EODCamera.localEulerAngles = new Vector3(22.5f, -22.5f, 0);
				this.TextWindow.SetActive(true);

				//Debug.Log("Phase 12.");

				if (SchemeGlobals.GetSchemeStage(2) == 3)
				{
					if (!StudentGlobals.GetStudentDying(11) && !StudentGlobals.GetStudentDead(11) && !StudentGlobals.GetStudentArrested(11))
					{
						this.GaudyRing.SetActive(true);

						this.Label.text = "Osana discovers Sakyu's ring inside of her book bag." + " " +
							"She returns the ring to Sakyu, who decides to stop bringing it to school.";

						SchemeGlobals.SetSchemeStage(2, 100);
					}
					else
					{
						this.GaudyRing.SetActive(true);

						this.Label.text = "Ayano decides to keep Sakyu Basu's ring.";

						SchemeGlobals.SetSchemeStage(2, 100);
					}
				}
				else if (this.RummageSpot.Phase == 2)
				{
					this.AnswerSheet.SetActive(true);

					this.Label.text = "A faculty member discovers that an answer sheet for an upcoming test is missing." + " " +
						"She changes all of the questions for the test and keeps the new answer sheet with her at all times.";

					GameGlobals.AnswerSheetUnavailable = true;
					this.RummageSpot.Phase = 0;
				}
				else
				{
					this.Phase++;
					this.UpdateScene();
				}
			}

            //////////////////////////////////////////////////////////////////
            ///// Checking whether half the school's population is dead. /////
            //////////////////////////////////////////////////////////////////

            else if (this.Phase == 16)
            {
                if (this.Police.Deaths + PlayerGlobals.Kills > 50)
                {
                    this.EODCamera.position = new Vector3(-6, .15f, -49);
                    this.EODCamera.eulerAngles = new Vector3(-22.5f, 22.5f, 0);

                    this.Label.text = "More than half of the school's population is dead. " +
                                      "For the safety of the remaining students, " +
                                      "the headmaster of Akademi makes the decision to shut down the school. " +
                                      "Senpai enrolls in a school far away. " +
                                      "Ayano will not be able to follow him, " +
                                      "and another girl will steal his heart. " +
                                      "Ayano has permanently lost her chance to be with Senpai.";

                    this.Heartbroken.NoSnap = true;
                    this.GameOver = true;
                }
                else
                {
                    this.Phase++;
                    this.UpdateScene();
                }
            }

            //////////////////////////////////////////
            ///// Check for clubs shutting down. /////
            //////////////////////////////////////////

            else if (this.Phase == 17)
			{
				//Debug.Log("Phase 13 - checking for clubs shutting down.");

				this.ClubClosed = false;
				this.ClubKicked = false;

				float DistanceToMoveForward = 1.2f;

				if (this.ClubID < this.ClubArray.Length)
				{
					if (!ClubGlobals.GetClubClosed(this.ClubArray[this.ClubID]))
					{
						//Debug.Log("Right now, we're checking the " + this.ClubNames[this.ClubID].ToString() + ".");

						this.ClubManager.CheckClub(this.ClubArray[this.ClubID]);

						if (this.ClubManager.ClubMembers < 5)
						{
							this.EODCamera.position = this.ClubManager.ClubVantages[this.ClubID].position;
							this.EODCamera.eulerAngles = this.ClubManager.ClubVantages[this.ClubID].eulerAngles;
							this.EODCamera.Translate(Vector3.forward * DistanceToMoveForward, Space.Self);

							ClubGlobals.SetClubClosed(this.ClubArray[this.ClubID], true);

							this.Label.text = "The " + this.ClubNames[this.ClubID].ToString() +
								" no longer has enough members to remain operational. The school forces the club to disband.";

							this.ClubClosed = true;

							if (this.Yandere.Club == this.ClubArray[this.ClubID])
							{
                                this.Yandere.Club = ClubType.None;
							}
						}

						if (this.ClubManager.LeaderMissing)
						{
							this.EODCamera.position = this.ClubManager.ClubVantages[this.ClubID].position;
							this.EODCamera.eulerAngles = this.ClubManager.ClubVantages[this.ClubID].eulerAngles;
							this.EODCamera.Translate(Vector3.forward * DistanceToMoveForward, Space.Self);

							ClubGlobals.SetClubClosed(this.ClubArray[this.ClubID], true);

							this.Label.text = "The leader of the " + this.ClubNames[this.ClubID].ToString() +
								" has gone missing. The " + this.ClubNames[this.ClubID].ToString() +
								" cannot operate without its leader. The club disbands.";

							this.ClubClosed = true;

							if (this.Yandere.Club == this.ClubArray[this.ClubID])
							{
                                this.Yandere.Club = ClubType.None;
							}
						}
						else if (this.ClubManager.LeaderDead)
						{
							this.EODCamera.position = this.ClubManager.ClubVantages[this.ClubID].position;
							this.EODCamera.eulerAngles = this.ClubManager.ClubVantages[this.ClubID].eulerAngles;
							this.EODCamera.Translate(Vector3.forward * DistanceToMoveForward, Space.Self);

							ClubGlobals.SetClubClosed(this.ClubArray[this.ClubID], true);

							this.Label.text = "The leader of the " + this.ClubNames[this.ClubID].ToString() +
								" is gone. The " + this.ClubNames[this.ClubID].ToString() +
								" cannot operate without its leader. The club disbands.";

							this.ClubClosed = true;

							if (this.Yandere.Club == this.ClubArray[this.ClubID])
							{
                                this.Yandere.Club = ClubType.None;
							}
						}
						else if (this.ClubManager.LeaderAshamed)
						{
							this.EODCamera.position = this.ClubManager.ClubVantages[this.ClubID].position;
							this.EODCamera.eulerAngles = this.ClubManager.ClubVantages[this.ClubID].eulerAngles;
							this.EODCamera.Translate(Vector3.forward * DistanceToMoveForward, Space.Self);

							ClubGlobals.SetClubClosed(this.ClubArray[this.ClubID], true);

							this.Label.text = "The leader of the " + this.ClubNames[this.ClubID].ToString() +
								" has unexpectedly disbanded the club without explanation.";

							this.ClubClosed = true;

							this.ClubManager.LeaderAshamed = false;

							if (this.Yandere.Club == this.ClubArray[this.ClubID])
							{
                                this.Yandere.Club = ClubType.None;
							}
						}
					}

					if (!ClubGlobals.GetClubClosed(this.ClubArray[this.ClubID]))
					{
						if (!ClubGlobals.GetClubKicked(this.ClubArray[this.ClubID]) &&
							(this.Yandere.Club == this.ClubArray[this.ClubID]))
						{
							this.ClubManager.CheckGrudge(this.ClubArray[this.ClubID]);

							if (this.ClubManager.LeaderGrudge)
							{
								this.EODCamera.position = this.ClubManager.ClubVantages[this.ClubID].position;
								this.EODCamera.eulerAngles = this.ClubManager.ClubVantages[this.ClubID].eulerAngles;
								this.EODCamera.Translate(Vector3.forward * DistanceToMoveForward, Space.Self);

								this.Label.text = "Ayano receives a text message from the president of the " +
									this.ClubNames[this.ClubID].ToString() + ". Ayano is no longer a member of the " +
									this.ClubNames[this.ClubID].ToString() + ", and is not welcome in the " +
									this.ClubNames[this.ClubID].ToString() + " room.";

								ClubGlobals.SetClubKicked(this.ClubArray[this.ClubID], true);
                                this.Yandere.Club = ClubType.None;
								this.ClubKicked = true;
							}
							else
							{
								if (this.ClubManager.ClubGrudge)
								{
									this.EODCamera.position = this.ClubManager.ClubVantages[this.ClubID].position;
									this.EODCamera.eulerAngles = this.ClubManager.ClubVantages[this.ClubID].eulerAngles;
									this.EODCamera.Translate(Vector3.forward * DistanceToMoveForward, Space.Self);

									this.Label.text = "Ayano receives a text message from the president of the " +
										this.ClubNames[this.ClubID].ToString() + ". There is someone in the " +
										this.ClubNames[this.ClubID].ToString() + " who hates and fears Ayano. Ayano is no longer a member of the " +
										this.ClubNames[this.ClubID].ToString() + ", and is not welcome in the " +
										this.ClubNames[this.ClubID].ToString() + " room.";

									ClubGlobals.SetClubKicked(this.ClubArray[this.ClubID], true);
                                    this.Yandere.Club = ClubType.None;
									this.ClubKicked = true;
								}
							}
						}
					}

					if (!this.ClubClosed && !this.ClubKicked)
					{
						this.ClubID++;
						this.UpdateScene();
					}

					this.ClubManager.LeaderAshamed = false;
				}
				else
				{
					this.Phase++;
					this.UpdateScene();
				}
			}

			////////////////////////////////////
			///// Ayano kidnaps a student. /////
			////////////////////////////////////

			else if (this.Phase == 18)
			{
				//Debug.Log("Phase 14.");

				if (this.TranqCase.Occupied)
				{
					this.ClosedTranqCase.SetActive(true);

					this.Label.color = new Color(this.Label.color.r, this.Label.color.g, this.Label.color.b, 1);

					this.Label.text = "Ayano waits until midnight, sneaks into school, and returns to the musical instrument case that contains her unconscious victim. " +
									  "She pushes the case back to her house and ties the victim to a chair in her basement.";

					/*
					this.Label.text = "Ayano waits until the clock strikes midnight." + "\n" + "\n" +
						"Under the cover of darkness, Ayano travels back to school and sneaks inside of the main school building." + "\n" + "\n" +
						"Ayano returns to the instrument case that carries her unconscious victim." + "\n" + "\n" +
						"She pushes the case back to her house, pretending to be a young musician returning home from a late-night show." + "\n" + "\n" +
						"Ayano drags the case down to her basement and ties up her victim." + "\n" + "\n" +
						"Exhausted, Ayano goes to sleep.";
					*/

					this.Phase++;
				}
				else
				{
					this.Phase++;
					this.UpdateScene();
				}
			}

			//////////////////////////////////////////
			///// The headmaster erects a fence. /////
			//////////////////////////////////////////

			else if (this.Phase == 19)
			{
				//Debug.Log("Phase 15.");

				if (this.ErectFence)
				{
					this.Fence.SetActive(true);

					this.Label.text = "To prevent any other students from falling off of the school rooftop, the school erects a fence around the roof.";
					SchoolGlobals.RoofFence = true;
					this.ErectFence = false;
				}
				else
				{
					this.Phase++;
					this.UpdateScene();
				}
			}

			//////////////////////////////////////////////////////
			///// Checking for the death of a Councilmember. /////
			//////////////////////////////////////////////////////

			else if (this.Phase == 20)
			{
				if (!SchoolGlobals.HighSecurity && this.Police.CouncilDeath)
				{
					this.SCP.SetActive(true);
					//this.SCP.GetComponent<Animation>().Play();

					this.Label.text = "The student council president has ordered the implementation of heightened security precautions. Security cameras and metal detectors are now present at school.";
					this.Police.CouncilDeath = false;
				}
				else
				{
					this.Phase++;
					this.UpdateScene();
				}
			}

            ////////////////////////////
            ///// Load home scene. /////
            ////////////////////////////

            else if (this.Phase == 21)
			{
				this.Finish();
			}

			/////////////////////////////////////////////////////////////////
			///// Rival confesses after police investigation on Friday. /////
			/////////////////////////////////////////////////////////////////

			else if (this.Phase == 22)
			{
				this.Senpai.enabled = false;
				this.Senpai.Pathfinding.enabled = false;
				this.Senpai.transform.parent = this.transform;
				this.Senpai.gameObject.SetActive(true);
				this.Senpai.transform.localPosition = new Vector3(0, 0, 0);
				this.Senpai.transform.localEulerAngles = new Vector3(0, 0, 0);
				this.Senpai.EmptyHands();
				this.Senpai.MyController.enabled = false;
				this.Senpai.CharacterAnimation.enabled = true;
				this.Senpai.CharacterAnimation.CrossFade(this.Senpai.IdleAnim);

				this.Rival.enabled = false;
				this.Rival.Pathfinding.enabled = false;
				this.Rival.transform.parent = this.transform;
				this.Rival.gameObject.SetActive(true);
				this.Rival.transform.localPosition = new Vector3(0, 0, 1);
				this.Rival.transform.localEulerAngles = new Vector3(0, 180, 0);
				this.Rival.EmptyHands();
				this.Rival.MyController.enabled = false;
				this.Rival.CharacterAnimation.enabled = true;
				this.Rival.CharacterAnimation.CrossFade(this.Rival.IdleAnim);
				this.Rival.CharacterAnimation[AnimNames.FemaleShy].weight = 1;
				this.Rival.CharacterAnimation.Play(AnimNames.FemaleShy);

				this.Label.text = "After the police investigation ends, Osana asks Senpai to speak with her under the cherry tree behind the school.";

				this.Phase++;
			}

			////////////////////////////////////////////////////////////////////
			///// Exit ''End Of Day'' mode, and begin confession cutscene. /////
			////////////////////////////////////////////////////////////////////

			else if (this.Phase == 23)
			{
				int DisableID = 1;

				while (DisableID < 101)
				{
					StudentManager.DisableStudent(DisableID);
					DisableID++;
				}

				this.LoveManager.Suitor = this.Senpai;
				this.LoveManager.Rival = this.Rival;
				this.LoveManager.Rival.CharacterAnimation[AnimNames.FemaleShy].weight = 0;

				this.LoveManager.Suitor.gameObject.SetActive(true);
				this.LoveManager.Rival.gameObject.SetActive(true);
				this.Yandere.gameObject.SetActive(true);

				this.LoveManager.Suitor.transform.parent = null;
				this.LoveManager.Rival.transform.parent = null;
				this.Yandere.gameObject.transform.parent = null;

				this.LoveManager.BeginConfession();

				this.Clock.NightLighting();
				this.Clock.enabled = false;

				this.gameObject.SetActive(false);
			}

			////////////////////////////////////
			///// The police arrest Ayano. /////
			////////////////////////////////////

			else if (this.Phase == 100)
			{
				this.Yandere.MyController.enabled = false;
				this.Yandere.transform.parent = this.transform;
				this.Yandere.transform.localPosition = new Vector3(0, 0, 0);
				this.Yandere.transform.localEulerAngles = new Vector3(0, 0, 0);
				this.Yandere.CharacterAnimation.Play("f02_handcuffs_00");
				this.Yandere.Handcuffs.SetActive(true);

				this.ArrestingCops.SetActive(true);

				Physics.SyncTransforms();

				this.Label.text = "Ayano is arrested by the police. She will never have Senpai.";
				this.GameOver = true;
			}

			////////////////////////////////////////
			///// The police arrest a student. /////
			////////////////////////////////////////

			else if (this.Phase == 101)
			{
				int weaponFingerprintID = this.WeaponManager.Weapons[this.WeaponID].FingerprintID;
				StudentScript arrestedStudent = this.StudentManager.Students[weaponFingerprintID];

				if (arrestedStudent.Alive)
				{
					Patsy = this.StudentManager.Students[weaponFingerprintID];

					Patsy.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
					Patsy.CharacterAnimation.enabled = true;

					if (Patsy.WeaponBag != null)
					{
						Patsy.WeaponBag.SetActive(false);
					}

					Patsy.EmptyHands();
					Patsy.SpeechLines.Stop();
					Patsy.Handcuffs.SetActive(true);
					Patsy.gameObject.SetActive(true);
					Patsy.Ragdoll.Zs.SetActive(false);
					Patsy.SmartPhone.SetActive(false);
					Patsy.MyController.enabled = false;
					Patsy.transform.parent = this.transform;
					Patsy.transform.localPosition = new Vector3(0, 0, 0);
					Patsy.transform.localEulerAngles = new Vector3(0, 0, 0);
					Patsy.ShoeRemoval.enabled = false;

					if (StudentManager.Students[weaponFingerprintID].Male)
					{
						this.StudentManager.Students[weaponFingerprintID].CharacterAnimation.Play("handcuffs_00");
					}
					else
					{
						this.StudentManager.Students[weaponFingerprintID].CharacterAnimation.Play("f02_handcuffs_00");
					}

					this.ArrestingCops.SetActive(true);

					if (!arrestedStudent.Tranquil)
					{
						this.Label.text = this.JSON.Students[weaponFingerprintID].Name + " is arrested by the police.";

						StudentGlobals.SetStudentArrested(weaponFingerprintID, true);

						this.Arrests++;
					}
					else
					{
						this.Label.text = this.JSON.Students[weaponFingerprintID].Name +
							" is found asleep inside of a musical instrument case." + " " +
							"The police assume that she hid herself inside of the box after committing murder, and arrest her.";

						StudentGlobals.SetStudentArrested(weaponFingerprintID, true);

						this.ArrestID = weaponFingerprintID;

						this.TranqCase.Occupied = false;

						this.Arrests++;
					}
				}
				else
				{
					this.ShruggingCops.SetActive(true);

					bool Mystery = false;

					// [af] Converted while loop to for loop.
					for (this.ID = 0; this.ID < this.VictimArray.Length; this.ID++)
					{
						if (this.VictimArray[this.ID] == weaponFingerprintID)
						{
							if (!arrestedStudent.MurderSuicide)
							{
								Mystery = true;
							}
						}
					}

					if (!Mystery)
					{
						this.Label.text = this.JSON.Students[weaponFingerprintID].Name +
							" is dead. The police cannot perform an arrest.";
						this.DeadPerps++;
					}
					else
					{
						this.Label.text = this.JSON.Students[weaponFingerprintID].Name +
							"'s fingerprints are on the same weapon that killed them. The police cannot solve this mystery.";
					}
				}

				this.Phase = 3;
			}

			///////////////////////////////////////////////////////////////////////////////
			///// The police inspect a student who has fallen off of the school roof. /////
			///////////////////////////////////////////////////////////////////////////////

			else if (this.Phase == 102)
			{
				if (this.Police.SuicideStudent.activeInHierarchy)
				{
					this.MurderScene.SetActive(true);

					this.Label.text = "The police inspect the corpse of a student who appears to have fallen to their death from the school rooftop." + " " +
						"The police treat the incident as a murder case, and search the school for any other victims.";
					this.ErectFence = true;
				}
				else
				{
					this.ShruggingCops.SetActive(true);

					this.Label.text = "The police attempt to determine whether or not a student fell to their death from the school rooftop." + " " +
						"The police are unable to reach a conclusion.";
				}

				// [af] Converted while loop to for loop.
				for (this.ID = 0; this.ID < this.Police.CorpseList.Length; this.ID++)
				{
					RagdollScript corpse = this.Police.CorpseList[this.ID];

					if (corpse != null)
					{
						if (corpse.Suicide)
						{
							this.Police.SuicideVictims++;

							corpse = null;

							if (this.Police.Corpses > 0)
							{
								this.Police.Corpses--;
							}
						}
					}
				}

				this.Phase = 2;
			}

			/////////////////////////////////////////////////
			///// The police inspect a poisoned corpse. /////
			/////////////////////////////////////////////////

			else if (this.Phase == 103)
			{
				this.MurderScene.SetActive(true);

				this.Label.text = "The paramedics attempt to resuscitate the poisoned student, but they are unable to revive her." + " " +
					"The police treat the incident as a murder case, and search the school for any other victims.";

				// [af] Converted while loop to for loop.
				for (this.ID = 0; this.ID < this.Police.CorpseList.Length; this.ID++)
				{
					RagdollScript corpse = this.Police.CorpseList[this.ID];

					if (corpse != null)
					{
						if (corpse.Poisoned)
						{
							corpse = null;

							if (this.Police.Corpses > 0)
							{
								this.Police.Corpses--;
							}
						}
					}
				}

				this.Phase = 2;
			}

			////////////////////////////////////////////////
			///// The police inspect a drowned corpse. /////
			////////////////////////////////////////////////

			else if (this.Phase == 104)
			{
				this.MurderScene.SetActive(true);

				this.Label.text = "The police determine that " + this.Police.DrownedStudentName +
					" died from drowning. The police treat the death as a possible murder, and search the school for any other victims.";
				
				// [af] Converted while loop to for loop.
				for (this.ID = 0; this.ID < this.Police.CorpseList.Length; this.ID++)
				{
					RagdollScript corpse = this.Police.CorpseList[this.ID];

					if (corpse != null)
					{
						if (corpse.Drowned)
						{
							corpse = null;

							if (this.Police.Corpses > 0)
							{
								this.Police.Corpses--;
							}
						}
					}
				}

				this.Phase = 2;
			}

			//////////////////////////////////////////////////////
			///// The police inspect an electrocuted corpse. /////
			//////////////////////////////////////////////////////

			else if (this.Phase == 105)
			{
				this.MurderScene.SetActive(true);

				this.Label.text = "The police determine that " + this.Police.ElectrocutedStudentName +
					" died from being electrocuted. The police treat the death as a possible murder, and search the school for any other victims.";

				// [af] Converted while loop to for loop.
				for (this.ID = 0; this.ID < this.Police.CorpseList.Length; this.ID++)
				{
					RagdollScript corpse = this.Police.CorpseList[this.ID];

					if (corpse != null)
					{
						if (corpse.Electrocuted)
						{
							corpse = null;

							if (this.Police.Corpses > 0)
							{
								this.Police.Corpses--;
							}
						}
					}
				}

				this.Phase = 2;
			}

			///////////////////////////////////
			///// The police flee in fear /////
			///////////////////////////////////

			else if (this.Phase == 999)
			{
				this.ScaredCops.SetActive(true);

				this.Yandere.MyController.enabled = false;
				this.Yandere.transform.parent = this.transform;
				this.Yandere.transform.localPosition = new Vector3(0, 0, -1);
				this.Yandere.transform.localEulerAngles = new Vector3(0, 0, 0);
				Physics.SyncTransforms();

				this.Label.text = "The police witness actual evidence of the supernatural, are absolutely horrified, and run for their lives.";

				if (this.StudentManager.RivalEliminated)
				{
					this.Phase = 12;
				}
				else
				{
					this.Phase = 13;
				}
			}
		}
	}

	void TeleportYandere()
	{
		this.Yandere.MyController.enabled = false;
		this.Yandere.transform.parent = this.transform;
		this.Yandere.transform.localPosition = new Vector3(.75f, .33333f, -1.9f);
		this.Yandere.transform.localEulerAngles = new Vector3(-22.5f, 157.5f, 0);

		Physics.SyncTransforms();
	}

	void Finish()
	{
		Debug.Log("We have reached the end of the End-of-Day sequence.");

		if (RivalEliminationMethod == RivalEliminationType.Expelled)
		{
			Debug.Log("Osana was expelled.");

			StudentGlobals.SetStudentExpelled(this.StudentManager.RivalID, true);
			GameGlobals.NonlethalElimination = true;
			GameGlobals.RivalEliminationID = 5;
		}

		PlayerGlobals.Reputation = this.Reputation.Reputation;
        ClubGlobals.Club = this.Yandere.Club;
        StudentGlobals.MemorialStudents = 0;
		HomeGlobals.Night = true;

		this.Police.KillStudents();

		if (this.Police.Suspended)
		{
			DateGlobals.PassDays = this.Police.SuspensionLength;
		}

		if (this.StudentManager.Students[SchoolGlobals.KidnapVictim] != null)
		{
			if (this.StudentManager.Students[SchoolGlobals.KidnapVictim].Ragdoll.enabled)
			{
				SchoolGlobals.KidnapVictim = 0;
			}
		}

		if (!this.TranqCase.Occupied)
		{
			SceneManager.LoadScene(SceneNames.HomeScene);
		}
		else
		{
			SchoolGlobals.KidnapVictim = this.TranqCase.VictimID;
			StudentGlobals.SetStudentKidnapped(this.TranqCase.VictimID, true);
			StudentGlobals.SetStudentSanity(this.TranqCase.VictimID, 100.0f);

			SceneManager.LoadScene(SceneNames.CalendarScene);
		}

		if (this.Dumpster.StudentToGoMissing > 0)
		{
			this.Dumpster.SetVictimMissing();
		}
			
		this.ID = 0;

		while (ID < this.GardenHoles.Length)
		{
			this.GardenHoles[ID].EndOfDayCheck();
			this.ID++;
		}

		this.ID = 1;

		while (ID < this.Yandere.Inventory.ShrineCollectibles.Length)
		{
			if (this.Yandere.Inventory.ShrineCollectibles[ID])
			{
				PlayerGlobals.SetShrineCollectible(ID, true);
			}

			this.ID++;
		}

		this.Incinerator.SetVictimsMissing();
		this.WoodChipper.SetVictimsMissing();

		if (this.FragileTarget > 0)
		{
			Debug.Log("Setting target for Fragile student.");

			StudentGlobals.FragileTarget = this.FragileTarget;
			StudentGlobals.FragileSlave = 5;
		}

		if (this.StudentManager.ReactedToGameLeader)
		{
			SchoolGlobals.ReactedToGameLeader = true;
		}

		if (this.NewFriends > 0)
		{
			PlayerGlobals.Friends += this.NewFriends;
		}

		if (this.Yandere.Alerts > 0)
		{
			PlayerGlobals.Alerts += this.Yandere.Alerts;
		}

		SchoolGlobals.SchoolAtmosphere += (Arrests * .05f);

		if (this.Counselor.ExpelledDelinquents)
		{
			SchoolGlobals.SchoolAtmosphere += .25f;
		}

		if (this.Yandere.Inventory.FakeID)
		{
			PlayerGlobals.FakeID = true;
		}

		if (this.RaibaruLoner)
		{
			PlayerGlobals.RaibaruLoner = true;
		}

		if (this.StopMourning)
		{
			GameGlobals.SenpaiMourning = false;
		}

		CollectibleGlobals.MatchmakingGifts = MatchmakingGifts;
		CollectibleGlobals.SenpaiGifts = SenpaiGifts;

		PlayerGlobals.PantyShots = this.Yandere.Inventory.PantyShots;
		PlayerGlobals.Money = this.Yandere.Inventory.Money;

        ClassGlobals.Biology = Class.Biology;
        ClassGlobals.Chemistry = Class.Chemistry;
        ClassGlobals.Language = Class.Language;
        ClassGlobals.Physical = Class.Physical;
        ClassGlobals.Psychology = Class.Psychology;

        ClassGlobals.BiologyGrade = Class.BiologyGrade;
        ClassGlobals.ChemistryGrade = Class.ChemistryGrade;
        ClassGlobals.LanguageGrade = Class.LanguageGrade;
        ClassGlobals.PhysicalGrade = Class.PhysicalGrade;
        ClassGlobals.PsychologyGrade = Class.PsychologyGrade;

        this.WeaponManager.TrackDumpedWeapons();

		this.StudentManager.CommunalLocker.RivalPhone.StolenPhoneDropoff.SetPhonesHacked();
	}

	void DisableThings(StudentScript TargetStudent)
	{
		if (TargetStudent != null)
		{
			TargetStudent.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
			TargetStudent.CharacterAnimation.enabled = true;
			TargetStudent.CharacterAnimation.Play(TargetStudent.IdleAnim);

			TargetStudent.EmptyHands();
			TargetStudent.SpeechLines.Stop();
			TargetStudent.Ragdoll.Zs.SetActive(false);
			TargetStudent.SmartPhone.SetActive(false);
			TargetStudent.MyController.enabled = false;
			TargetStudent.ShoeRemoval.enabled = false;
			TargetStudent.enabled = false;

			TargetStudent.gameObject.SetActive(true);
			TargetStudent.transform.parent = this.transform;
			TargetStudent.transform.localEulerAngles = new Vector3(0, 0, 0);
		}
	}
}