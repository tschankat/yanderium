using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CosmeticScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public TextureManagerScript TextureManager;
	public SkinnedMeshUpdater SkinUpdater;
	public LoveManagerScript LoveManager;
	public Animation CharacterAnimation;
	public ModelSwapScript ModelSwap;
	public StudentScript Student;
	public JsonScript JSON;

	public GameObject[] TeacherAccessories;
	public GameObject[] FemaleAccessories;
	public GameObject[] MaleAccessories;
	public GameObject[] ClubAccessories;
	public GameObject[] PunkAccessories;

	public GameObject[] RightStockings;
	public GameObject[] LeftStockings;
	public GameObject[] PhoneCharms;
	public GameObject[] TeacherHair;
	public GameObject[] FacialHair;
	public GameObject[] FemaleHair;
	public GameObject[] MusicNotes;
	public GameObject[] Kerchiefs;
	public GameObject[] CatGifts;
	public GameObject[] MaleHair;
	public GameObject[] RedCloth;
	public GameObject[] Scanners;
	public GameObject[] Eyewear;
	public GameObject[] Goggles;
	public GameObject[] Flowers;
	public GameObject[] Roses;

	public Renderer[] TeacherHairRenderers;
	public Renderer[] FacialHairRenderers;
	public Renderer[] FemaleHairRenderers;
	public Renderer[] MaleHairRenderers;
	public Renderer[] Fingernails;

	public Texture[] GanguroSwimsuitTextures;
	public Texture[] GanguroUniformTextures;
	public Texture[] GanguroCasualTextures;
	public Texture[] GanguroSocksTextures;

	public Texture[] ObstacleUniformTextures;
	public Texture[] ObstacleCasualTextures;
	public Texture[] ObstacleSocksTextures;

	public Texture[] OccultUniformTextures;
	public Texture[] OccultCasualTextures;
	public Texture[] OccultSocksTextures;

	public Texture[] FemaleUniformTextures;
	public Texture[] FemaleCasualTextures;
	public Texture[] FemaleSocksTextures;

	public Texture[] MaleUniformTextures;
	public Texture[] MaleCasualTextures;
	public Texture[] MaleSocksTextures;

	public Texture[] SmartphoneTextures;
	public Texture[] HoodieTextures;

	public Texture[] FaceTextures;
	public Texture[] SkinTextures;

	public Texture[] WristwearTextures;
	public Texture[] CardiganTextures;
	public Texture[] BookbagTextures;

	public Texture[] EyeTextures;
	public Texture[] CheekTextures;
	public Texture[] ForeheadTextures;
	public Texture[] MouthTextures;
	public Texture[] NoseTextures;

	public Texture[] ApronTextures;
	public Texture[] CanTextures;
	public Texture[] Trunks;

	public Texture[] MusicStockings;

	public Mesh[] FemaleUniforms;
	public Mesh[] MaleUniforms;
	public Mesh[] Berets;

	public Color[] BullyColor;

	public SkinnedMeshRenderer CardiganRenderer;
	public SkinnedMeshRenderer MyRenderer;
	public Renderer FacialHairRenderer;
	public Renderer RightEyeRenderer;
	public Renderer LeftEyeRenderer;
	public Renderer HoodieRenderer;
	public Renderer ScarfRenderer;
	public Renderer HairRenderer;
	public Renderer CanRenderer;

	public Mesh DelinquentMesh;
	public Mesh SchoolUniform;

	public Texture DefaultFaceTexture;
	public Texture TeacherBodyTexture;

	public Texture CoachPaleBodyTexture;
	public Texture CoachBodyTexture;
	public Texture CoachFaceTexture;

	public Texture UniformTexture;
	public Texture CasualTexture;
	public Texture SocksTexture;
	public Texture FaceTexture;

	public Texture PurpleStockings;
	public Texture YellowStockings;
	public Texture BlackStockings;
	public Texture GreenStockings;
	public Texture BlueStockings;
	public Texture CyanStockings;
	public Texture RedStockings;

	public Texture BlackKneeSocks;
	public Texture GreenSocks;

	public Texture KizanaStockings;
	public Texture OsanaStockings;

	public Texture TurtleStockings;
	public Texture TigerStockings;
	public Texture BirdStockings;
	public Texture DragonStockings;

	public Texture[] CustomStockings;
	public Texture MyStockings;

	public Texture BlackBody;
	public Texture BlackFace;
	public Texture GrayFace;

	public Texture DelinquentUniformTexture;
	public Texture DelinquentCasualTexture;
	public Texture DelinquentSocksTexture;

	public Texture OsanaSwimsuitTexture;

	public Texture ObstacleSwimsuitTexture;
	public Texture ObstacleTowelTexture;
	public Texture ObstacleGymTexture;

	public Texture TanSwimsuitTexture;
	public Texture TanTowelTexture;
	public Texture TanGymTexture;

	public GameObject RightIrisLight;
	public GameObject LeftIrisLight;
	public GameObject RightWristband;
	public GameObject LeftWristband;
	public GameObject Cardigan;
	public GameObject Bookbag;

	public GameObject ThickBrows;
	public GameObject Character;
	public GameObject RightShoe;
	public GameObject LeftShoe;
	public GameObject SadBrows;
	public GameObject Armband;
	public GameObject Hoodie;
	public GameObject Tongue;

	public Transform RightBreast;
	public Transform LeftBreast;

	public Transform RightTemple;
	public Transform LeftTemple;

	public Transform Head;
	public Transform Neck;

	public Color CorrectColor;
	public Color ColorValue;

	public Mesh TeacherMesh;
	public Mesh CoachMesh;
	public Mesh NurseMesh;

	public bool MysteriousObstacle = false;
	public bool DoNotChangeFace = false;
	public bool TakingPortrait = false;
	public bool Initialized = false;
	public bool CustomEyes = false;
	public bool CustomHair = false;
	public bool LookCamera = false;
	public bool HomeScene = false;
	public bool Kidnapped = false;
	public bool Randomize = false;
	public bool Cutscene = false;
	public bool Modified = false;
	public bool TurnedOn = false;
	public bool Teacher = false;
	public bool Yandere = false;
	public bool Empty = false;
	public bool Male = false;

	public float BreastSize = 0.0f;

	public string OriginalStockings = string.Empty;
	public string HairColor = string.Empty;
	public string Stockings = string.Empty;
	public string EyeColor = string.Empty;
	public string EyeType = string.Empty;
	public string Name = string.Empty;

	public int FacialHairstyle = 0;
	public int Accessory = 0;
	public int Direction = 0;
	public int Hairstyle = 0;
	public int SkinColor = 0;
	public int StudentID = 0;
	public int EyewearID = 0;
	public ClubType Club = ClubType.None;
	public int ID = 0;

	public GameObject[] GaloAccessories;

	public Material[] NurseMaterials;

	public GameObject CardiganPrefab;

	public void Start()
	{
        //Anti-Osana Code
        #if (UNITY_EDITOR)
        if (this.Cutscene)
        {
            if (EventGlobals.OsanaConversation)
            {
                this.StudentID = 11;
            }
        }
        #endif

        if (this.RightShoe != null)
		{
			this.RightShoe.SetActive(false);
			this.LeftShoe.SetActive(false);
		}

		this.ColorValue = new Color(1.0f, 1.0f, 1.0f, 1.0f);

		if (this.JSON == null)
		{
			this.JSON = this.Student.JSON;
		}

		string Name = string.Empty;

		if (!this.Initialized)
		{
			this.Accessory = int.Parse(this.JSON.Students[this.StudentID].Accessory);
			this.Hairstyle = int.Parse(this.JSON.Students[this.StudentID].Hairstyle);
			this.Stockings = this.JSON.Students[this.StudentID].Stockings;
			this.BreastSize = this.JSON.Students[this.StudentID].BreastSize;
			this.EyeType = this.JSON.Students[this.StudentID].EyeType;
			this.HairColor = this.JSON.Students[this.StudentID].Color;
			this.EyeColor = this.JSON.Students[this.StudentID].Eyes;
			this.Club = this.JSON.Students[this.StudentID].Club;
			this.Name = this.JSON.Students[this.StudentID].Name;

			if (this.Yandere)
			{
				this.Accessory = 0;	
				this.Hairstyle = 1;
				this.Stockings = "Black";
				this.BreastSize = 1;
				this.HairColor = "White";
				this.EyeColor = "Black";
				this.Club = ClubType.None;
			}

			this.OriginalStockings = this.Stockings;

			this.Initialized = true;
		}

		if (this.StudentID == 36)
		{
			if (TaskGlobals.GetTaskStatus(36) < 3)
			{
				this.FacialHairstyle = 12;
				this.EyewearID = 8;
			}
			else
			{
				this.FacialHairstyle = 0;
				this.EyewearID = 9;
				this.Hairstyle = 49;
				this.Accessory = 0;
			}
		}

		if (this.StudentID == 51)
		{
			if (ClubGlobals.GetClubClosed(ClubType.LightMusic))
			{
				this.Hairstyle = 51;
			}
		}

		if (GameGlobals.EmptyDemon)
		{
			if (this.StudentID == 21 || this.StudentID == 26 || this.StudentID == 31 ||
					this.StudentID == 36 || this.StudentID == 41 || this.StudentID == 46 ||
					this.StudentID == 51 || this.StudentID == 56 || this.StudentID == 61 ||
					this.StudentID == 66 || this.StudentID == 71)
			{
				if (!this.Male)
				{
					this.Hairstyle = 52;
				}
				else
				{
					this.Hairstyle = 53;
				}

				this.FacialHairstyle = 0;
				this.EyewearID = 0;
				this.Accessory = 0;
				this.Stockings = "";
				this.BreastSize = 1;

				this.Empty = true;
			}
		}

		if (this.Name == "Random")
		{
			this.Randomize = true;

			if (!this.Male)
			{
				Name = this.StudentManager.FirstNames[Random.Range(0, this.StudentManager.FirstNames.Length)] + " " +
					this.StudentManager.LastNames[Random.Range(0, this.StudentManager.LastNames.Length)];
				this.JSON.Students[this.StudentID].Name = Name;
				this.Student.Name = Name;
			}
			else
			{
				Name = this.StudentManager.MaleNames[Random.Range(0, this.StudentManager.MaleNames.Length)] + " " +
					this.StudentManager.LastNames[Random.Range(0, this.StudentManager.LastNames.Length)];
				this.JSON.Students[this.StudentID].Name = Name;
				this.Student.Name = Name;
			}

			if (MissionModeGlobals.MissionMode)
			{
				if (MissionModeGlobals.MissionTarget == this.StudentID)
				{
					this.JSON.Students[this.StudentID].Name = MissionModeGlobals.MissionTargetName;
					this.Student.Name = MissionModeGlobals.MissionTargetName;
					Name = MissionModeGlobals.MissionTargetName;
				}
			}
		}

		if (this.Randomize)
		{
			this.Teacher = false;
			this.BreastSize = Random.Range(0.50f, 2.0f);
			this.Accessory = 0;
			this.Club = ClubType.None;

			if (!this.Male)
			{
				this.Hairstyle = 1;

				while (this.Hairstyle == 1 || this.Hairstyle == 20 || this.Hairstyle == 21)
				{
					this.Hairstyle = Random.Range(1, this.FemaleHair.Length);
				}
			}
			else
			{
				this.SkinColor = Random.Range(0, this.SkinTextures.Length);
				this.Hairstyle = Random.Range(1, this.MaleHair.Length);
			}
		}

		if (!this.Male)
		{
			#if !UNITY_EDITOR

			if (this.Hairstyle == 20 || this.Hairstyle == 21)
			{
				if (this.Direction == 1)
				{
					this.Hairstyle = 22;
				}
				else
				{
					this.Hairstyle = 19;
				}
			}

			#endif

			this.ThickBrows.SetActive(false);

			if (!this.TakingPortrait)
			{
				this.Tongue.SetActive(false);
			}

			foreach (GameObject charm in this.PhoneCharms)
			{
				if (charm != null)
				{
					charm.SetActive(false);
				}
			}

            if (QualitySettings.GetQualityLevel() > 1)
            {
                this.Student.BreastSize = 1;
                this.BreastSize = 1;
            }

			this.RightBreast.localScale = new Vector3(this.BreastSize, this.BreastSize, this.BreastSize);
			this.LeftBreast.localScale = new Vector3(this.BreastSize, this.BreastSize, this.BreastSize);

			this.RightWristband.SetActive(false);
			this.LeftWristband.SetActive(false);

			if (this.StudentID == 51)
			{
				this.RightTemple.name = "RENAMED";
				this.LeftTemple.name = "RENAMED";

				this.RightTemple.localScale = new Vector3(0, 1, 1);
				this.LeftTemple.localScale = new Vector3(0, 1, 1);

				if (ClubGlobals.GetClubClosed(ClubType.LightMusic))
				{
					this.SadBrows.SetActive(true);
				}
				else
				{
					this.ThickBrows.SetActive(true);
				}
			}

			if (this.Club == ClubType.Bully)
			{
				/*
				GameObject NewCardigan = Instantiate(CardiganPrefab, transform.position, Quaternion.identity);
				CardiganScript NewCardiganScript = NewCardigan.GetComponent<CardiganScript>();

				Cardigan = NewCardigan;
				CardiganRenderer = NewCardiganScript.MyRenderer;
				*/

				if (!this.Kidnapped)
				{
					this.Student.SmartPhone.GetComponent<Renderer>().material.mainTexture = this.SmartphoneTextures[this.StudentID];
					this.Student.SmartPhone.transform.localPosition = new Vector3(0.01f, 0.005f, 0.01f);
					this.Student.SmartPhone.transform.localEulerAngles = new Vector3(0, -160, 165);
				}

				this.RightWristband.GetComponent<Renderer>().material.mainTexture = this.WristwearTextures[this.StudentID];
				this.LeftWristband.GetComponent<Renderer>().material.mainTexture = this.WristwearTextures[this.StudentID];
				this.Bookbag.GetComponent<Renderer>().material.mainTexture = this.BookbagTextures[this.StudentID];
				this.HoodieRenderer.material.mainTexture = this.HoodieTextures[this.StudentID];

				if (this.PhoneCharms.Length > 0)
				{
					this.PhoneCharms[this.StudentID].SetActive(true);
				}

				if (StudentGlobals.FemaleUniform < 2 || StudentGlobals.FemaleUniform == 3)
				{
					this.RightWristband.SetActive(true);
					this.LeftWristband.SetActive(true);
				}

				this.Bookbag.SetActive(true);
				this.Hoodie.SetActive(true);

				int NailID = 0;

				while (NailID < 10)
				{
					this.Fingernails[NailID].material.color = this.BullyColor[this.StudentID];
					NailID++;
				}

				this.Student.GymTexture = TanGymTexture;
				this.Student.TowelTexture = TanTowelTexture;
			}
			else
			{
				int NailID = 0;

				while (NailID < 10)
				{
					this.Fingernails[NailID].gameObject.SetActive(false);
					NailID++;
				}

				if (this.Club == ClubType.Gardening)
				{
					if (!this.TakingPortrait && !this.Kidnapped)
					{
						this.CanRenderer.material.mainTexture = this.CanTextures[this.StudentID];
					}
				}
			}

			/////////////////////
			///// TEST CODE /////
			/////////////////////

			/*
			if (!this.Male)
			{
				GameObject TestCharacter = GameObject.Find("TestGirl");
				CharacterAnimation = TestCharacter.GetComponent<Animation>();
			}
			else
			{
				GameObject TestCharacter = GameObject.Find("TestBoy");
				CharacterAnimation = TestCharacter.GetComponent<Animation>();
			}
			*/

			/////////////////////
			///// TEST CODE /////
			/////////////////////

			if (!this.Kidnapped)
			{
				if (SceneManager.GetActiveScene().name == SceneNames.PortraitScene)
				{
					//These are all females.

					//Sakyu Basu
					if (this.StudentID == 2)
					{
						this.CharacterAnimation.Play("succubus_a_idle_twins_01");
						this.transform.position = new Vector3(.094f, 0, 0);
						this.LookCamera = true;

						this.CharacterAnimation[AnimNames.FemaleSmile].layer = 1;
						this.CharacterAnimation.Play(AnimNames.FemaleSmile);
						this.CharacterAnimation[AnimNames.FemaleSmile].weight = 1.0f;
					}

					//Inkyu Basu
					else if (this.StudentID == 3)
					{
						this.CharacterAnimation.Play("succubus_b_idle_twins_01");
						this.transform.position = new Vector3(-.332f, 0, 0);
						this.LookCamera = true;

						this.CharacterAnimation[AnimNames.FemaleSmile].layer = 1;
						this.CharacterAnimation.Play(AnimNames.FemaleSmile);
						this.CharacterAnimation[AnimNames.FemaleSmile].weight = 1.0f;
					}

					//Kuu Dere
					else if (this.StudentID == 4)
					{
						this.CharacterAnimation.Play("f02_idleShort_00");
						this.transform.position = new Vector3(.015f, 0, 0);
						this.LookCamera = true;
					}

					//Fragile Girl
					else if (this.StudentID == 5)
					{
						this.CharacterAnimation.Play(AnimNames.FemaleShy);this.CharacterAnimation.Play(AnimNames.FemaleShy);
						this.CharacterAnimation[AnimNames.FemaleShy].time = 1;
					}

					//Raibaru
					else if (this.StudentID == 10){this.CharacterAnimation.Play(AnimNames.FemaleGirlyIdle);}

					//Cooking Girls
					else if (this.StudentID == 24){this.CharacterAnimation.Play(AnimNames.FemaleGirlyIdle);this.CharacterAnimation[AnimNames.FemaleGirlyIdle].time = 1;}
					else if (this.StudentID == 25){this.CharacterAnimation.Play(AnimNames.FemaleGirlyIdle);this.CharacterAnimation[AnimNames.FemaleGirlyIdle].time = 0;}

					//Kokona
					else if (this.StudentID == 30){this.CharacterAnimation.Play(AnimNames.FemaleGirlyIdle);this.CharacterAnimation[AnimNames.FemaleGirlyIdle].time = 0;}

					//Female Occultists
					else if (this.StudentID == 34){this.CharacterAnimation.Play("f02_idleShort_00");this.transform.position = new Vector3(.015f, 0, 0);this.LookCamera = true;}
					else if (this.StudentID == 35){this.CharacterAnimation.Play("f02_idleShort_00");this.transform.position = new Vector3(.015f, 0, 0);this.LookCamera = true;}

					//Pippi
					else if (this.StudentID == 38){this.CharacterAnimation.Play(AnimNames.FemaleGirlyIdle);this.CharacterAnimation[AnimNames.FemaleGirlyIdle].time = 0;}

					//Midori
					else if (this.StudentID == 39)
					{
						this.CharacterAnimation.Play(AnimNames.FemaleSocialCameraPose);
						this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.05f, this.transform.position.z);
					}

					//Mai Waifu
					else if (this.StudentID == 40){this.CharacterAnimation.Play(AnimNames.FemaleGirlyIdle);this.CharacterAnimation[AnimNames.FemaleGirlyIdle].time = 1;}

					//Miyuji
					else if (this.StudentID == 51)
					{
						this.CharacterAnimation.Play("f02_musicPose_00");
						this.Tongue.SetActive(true);
					}

					//Female Sleuths
					else if (this.StudentID == 59){this.CharacterAnimation.Play("f02_sleuthPortrait_00");}
					else if (this.StudentID == 60){this.CharacterAnimation.Play("f02_sleuthPortrait_01");}

					//Female Scientists
					else if (this.StudentID == 64){this.CharacterAnimation.Play("f02_idleShort_00");this.transform.position = new Vector3(.015f, 0, 0);this.LookCamera = true;}
					else if (this.StudentID == 65){this.CharacterAnimation.Play("f02_idleShort_00");this.transform.position = new Vector3(.015f, 0, 0);this.LookCamera = true;}

					//Gardening Club
					else if (this.StudentID == 71){this.CharacterAnimation.Play(AnimNames.FemaleGirlyIdle);this.CharacterAnimation[AnimNames.FemaleGirlyIdle].time = 0;}
					else if (this.StudentID == 72){this.CharacterAnimation.Play(AnimNames.FemaleGirlyIdle);this.CharacterAnimation[AnimNames.FemaleGirlyIdle].time = 0.66666f;}
					else if (this.StudentID == 73){this.CharacterAnimation.Play(AnimNames.FemaleGirlyIdle);this.CharacterAnimation[AnimNames.FemaleGirlyIdle].time = 0.66666f * 2;}
					else if (this.StudentID == 74){this.CharacterAnimation.Play(AnimNames.FemaleGirlyIdle);this.CharacterAnimation[AnimNames.FemaleGirlyIdle].time = 0.66666f * 3;}
					else if (this.StudentID == 75){this.CharacterAnimation.Play(AnimNames.FemaleGirlyIdle);this.CharacterAnimation[AnimNames.FemaleGirlyIdle].time = 0.66666f * 4;}

					//Musume
					else if (this.StudentID == 81)
					{
						this.CharacterAnimation.Play(AnimNames.FemaleSocialCameraPose);
						this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.05f, this.transform.position.z);
					}
					//Bullies
					else if (this.StudentID == 82 || this.StudentID == 52){this.CharacterAnimation.Play("f02_galPose_01");}
					else if (this.StudentID == 83 || this.StudentID == 53){this.CharacterAnimation.Play("f02_galPose_02");}
					else if (this.StudentID == 84 || this.StudentID == 54){this.CharacterAnimation.Play("f02_galPose_03");}
					else if (this.StudentID == 85 || this.StudentID == 55){this.CharacterAnimation.Play("f02_galPose_04");}

					//Everyone Else
					else
					{
						if (this.Club != ClubType.Council)
						{
							this.CharacterAnimation.Play("f02_idleShort_01");
							this.transform.position = new Vector3(.015f, 0, 0);
							this.LookCamera = true;
						}
					}
				}
			}
		}
		else
		{
			this.ThickBrows.SetActive(false);

			// [af] Converted while loop to foreach loop.
			foreach (GameObject accessory in this.GaloAccessories)
			{
				accessory.SetActive(false);
			}

			if (this.Club == ClubType.Occult)
			{
				this.CharacterAnimation[AnimNames.MaleSadFace].layer = 1;
				this.CharacterAnimation.Play(AnimNames.MaleSadFace);
				this.CharacterAnimation[AnimNames.MaleSadFace].weight = 1.0f;
			}

			bool SuitorA = false;

			//Anti-Osana Code
			#if UNITY_EDITOR
			if (this.StudentID == 6){SuitorA = true;}
			#endif

			#if !UNITY_EDITOR
			if (this.StudentID == 28){SuitorA = true;}
			#endif

			if (SuitorA == true)
			{
				if (StudentGlobals.CustomSuitor)
				{
					if (StudentGlobals.CustomSuitorHair > 0)
					{
						this.Hairstyle = StudentGlobals.CustomSuitorHair;
						//this.HairColor = "Purple";
						//this.EyeColor = "Purple";
					}

					if (StudentGlobals.CustomSuitorAccessory > 0)
					{
						this.Accessory = StudentGlobals.CustomSuitorAccessory;

						if (this.Accessory == 1)
						{
							Transform maleAccessory1Transform = this.MaleAccessories[1].transform;

							maleAccessory1Transform.localScale = new Vector3(
								1.066666f,
								1,
								1);

							maleAccessory1Transform.localPosition = new Vector3(
								0,
								-1.525f,
								.0066666f);
						}
					}

					if (StudentGlobals.CustomSuitorBlack)
					{
						this.HairColor = "SolidBlack";
					}

					if (StudentGlobals.CustomSuitorJewelry > 0)
					{
						// [af] Converted while loop to foreach loop.
						foreach (GameObject accessory in this.GaloAccessories)
						{
							accessory.SetActive(true);
						}
					}
				}
			}

			if (this.StudentID == 36 || this.StudentID == 66)
			{
				this.CharacterAnimation["toughFace_00"].layer = 1;
				this.CharacterAnimation.Play("toughFace_00");
				this.CharacterAnimation["toughFace_00"].weight = 1.0f;

				if (this.StudentID == 66)
				{
					this.ThickBrows.SetActive(true);
				}
			}

			//These are all males.
			if (SceneManager.GetActiveScene().name == SceneNames.PortraitScene)
			{
				     if (this.StudentID == 26){this.CharacterAnimation.Play("idleHaughty_00");}
				else if (this.StudentID == 36){this.CharacterAnimation.Play("slouchIdle_00");}

				else if (this.StudentID == 56){this.CharacterAnimation.Play("idleConfident_00");}
				else if (this.StudentID == 57){this.CharacterAnimation.Play("sleuthPortrait_00");}
				else if (this.StudentID == 58){this.CharacterAnimation.Play("sleuthPortrait_01");}

				//Male Science Leader
				else if (this.StudentID == 61){this.CharacterAnimation.Play("scienceMad_00"); transform.position = new Vector3(0, .1f, 0);}

				//Male Scientist
				else if (this.StudentID == 62){this.CharacterAnimation.Play("idleFrown_00");}

				//Male Swimmer
				else if (this.StudentID == 69){this.CharacterAnimation.Play("idleFrown_00");}

				else if (this.StudentID == 76){this.CharacterAnimation.Play("delinquentPoseB");}
				else if (this.StudentID == 77){this.CharacterAnimation.Play("delinquentPoseA");}
				else if (this.StudentID == 78){this.CharacterAnimation.Play("delinquentPoseC");}
				else if (this.StudentID == 79){this.CharacterAnimation.Play("delinquentPoseD");}
				else if (this.StudentID == 80){this.CharacterAnimation.Play("delinquentPoseE");}
			}
		}

		if (this.Club == ClubType.Teacher)
		{
			this.MyRenderer.sharedMesh = this.TeacherMesh;
			this.Teacher = true;
		}
		else if (this.Club == ClubType.GymTeacher)
		{
			if (!StudentGlobals.GetStudentReplaced(this.StudentID))
			{
				this.CharacterAnimation[AnimNames.FemaleSmile].layer = 1;
				this.CharacterAnimation.Play(AnimNames.FemaleSmile);
				this.CharacterAnimation[AnimNames.FemaleSmile].weight = 1.0f;

				this.RightEyeRenderer.gameObject.SetActive(false);
				this.LeftEyeRenderer.gameObject.SetActive(false);
			}

			this.MyRenderer.sharedMesh = this.CoachMesh;
			this.Teacher = true;
		}
		else if (this.Club == ClubType.Nurse)
		{
			this.MyRenderer.sharedMesh = this.NurseMesh;
			this.Teacher = true;
		}
		else if (this.Club == ClubType.Council)
		{
			this.Armband.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(-0.64375f, 0));
			this.Armband.SetActive(true);

			string Suffix = "";

			if (this.StudentID == 86){Suffix = "Strict";}
			if (this.StudentID == 87){Suffix = "Casual";}
			if (this.StudentID == 88){Suffix = "Grace";}
			if (this.StudentID == 89){Suffix = "Edgy";}

			this.CharacterAnimation["f02_faceCouncil" + Suffix + "_00"].layer = 1;
			this.CharacterAnimation.Play("f02_faceCouncil" + Suffix + "_00");

			this.CharacterAnimation["f02_idleCouncil" + Suffix + "_00"].time = 1;
			this.CharacterAnimation.Play("f02_idleCouncil" + Suffix + "_00");
		}

		if (!ClubGlobals.GetClubClosed(this.Club))
		{
			if (this.StudentID == 21 || this.StudentID == 26 || 
				this.StudentID == 31 || this.StudentID == 36 ||
				this.StudentID == 41 || this.StudentID == 46 ||
				this.StudentID == 51 || this.StudentID == 56 ||
				this.StudentID == 61 || this.StudentID == 66 ||
				this.StudentID == 71)
			{
				this.Armband.SetActive(true);

				Renderer ArmbandRenderer = this.Armband.GetComponent<Renderer>();

				//Cooking
				if (this.StudentID == 21)
				{
					ArmbandRenderer.material.SetTextureOffset("_MainTex", new Vector2(-0.63f, -.22f));
				}
				//Drama
				else if (this.StudentID == 26)
				{
					ArmbandRenderer.material.SetTextureOffset("_MainTex", new Vector2(0, -.22f));
				}
				//Occult
				else if (this.StudentID == 31)
				{
					ArmbandRenderer.material.SetTextureOffset("_MainTex", new Vector2(0.69f, 0.01f));
				}
				//Gaming
				else if (this.StudentID == 36)
				{
					ArmbandRenderer.material.SetTextureOffset("_MainTex", new Vector2(-.633333f, -.44f));
				}
				//Art
				else if (this.StudentID == 41)
				{
					ArmbandRenderer.material.SetTextureOffset("_MainTex", new Vector2(-.62f, -.66666f));
				}
				//Martial Arts
				else if (this.StudentID == 46)
				{
					ArmbandRenderer.material.SetTextureOffset("_MainTex", new Vector2(0, -.66666f));
				}
				//Music
				else if (this.StudentID == 51)
				{
					ArmbandRenderer.material.SetTextureOffset("_MainTex", new Vector2(0.69f, .5566666f));
				}
				//Photography
				else if (this.StudentID == 56)
				{
					ArmbandRenderer.material.SetTextureOffset("_MainTex", new Vector2(0, .5533333f));
				}
				//Science
				else if (this.StudentID == 61)
				{
					ArmbandRenderer.material.SetTextureOffset("_MainTex", new Vector2(0, 0));
				}
				//Sports
				else if (this.StudentID == 66)
				{
					ArmbandRenderer.material.SetTextureOffset("_MainTex", new Vector2(0.69f, -.22f));
				}
				//Gardening
				else if (this.StudentID == 71)
				{
					ArmbandRenderer.material.SetTextureOffset("_MainTex", new Vector2(0.69f, .335f));
				}
			}
		}

		///////////////////////////////
		///// DISABLE ACCESSORIES /////
		///////////////////////////////

		foreach (GameObject accessory in this.FemaleAccessories)
		{
			if (accessory != null)
			{
				accessory.SetActive(false);
			}
		}

		foreach (GameObject accessory in this.MaleAccessories)
		{
			if (accessory != null)
			{
				accessory.SetActive(false);
			}
		}

		foreach (GameObject accessory in this.ClubAccessories)
		{
			if (accessory != null)
			{
				accessory.SetActive(false);
			}
		}

		foreach (GameObject accessory in this.TeacherAccessories)
		{
			if (accessory != null)
			{
				accessory.SetActive(false);
			}
		}

		foreach (GameObject hair in this.TeacherHair)
		{
			if (hair != null)
			{
				hair.SetActive(false);
			}
		}

		foreach (GameObject hair in this.FemaleHair)
		{
			if (hair != null)
			{
				hair.SetActive(false);
			}
		}

		foreach (GameObject hair in this.MaleHair)
		{
			if (hair != null)
			{
				hair.SetActive(false);
			}
		}

		foreach (GameObject facialHair in this.FacialHair)
		{
			if (facialHair != null)
			{
				facialHair.SetActive(false);
			}
		}

		foreach (GameObject eyewear in this.Eyewear)
		{
			if (eyewear != null)
			{
				eyewear.SetActive(false);
			}
		}

		foreach (GameObject stockings in this.RightStockings)
		{
			if (stockings != null)
			{
				stockings.SetActive(false);
			}
		}

		foreach (GameObject stockings in this.LeftStockings)
		{
			if (stockings != null)
			{
				stockings.SetActive(false);
			}
		}

		foreach (GameObject scanner in this.Scanners)
		{
			if (scanner != null)
			{
				scanner.SetActive(false);
			}
		}

		foreach (GameObject flower in this.Flowers)
		{
			if (flower != null)
			{
				flower.SetActive(false);
			}
		}

		foreach (GameObject rose in this.Roses)
		{
			if (rose != null)
			{
				rose.SetActive(false);
			}
		}

		foreach (GameObject goggles in this.Goggles)
		{
			if (goggles != null)
			{
				goggles.SetActive(false);
			}
		}

		foreach (GameObject redCloth in this.RedCloth)
		{
			if (redCloth != null)
			{
				redCloth.SetActive(false);
			}
		}

		foreach (GameObject kerchief in this.Kerchiefs)
		{
			if (kerchief != null)
			{
				kerchief.SetActive(false);
			}
		}

		foreach (GameObject catGift in this.CatGifts)
		{
			if (catGift != null)
			{
				catGift.SetActive(false);
			}
		}

		foreach (GameObject punks in this.PunkAccessories)
		{
			if (punks != null)
			{
				punks.SetActive(false);
			}
		}

		foreach (GameObject musicNotes in this.MusicNotes)
		{
			if (musicNotes != null)
			{
				musicNotes.SetActive(false);
			}
		}

		bool SuitorB = false;

		//Anti-Osana Code
		#if UNITY_EDITOR
		if (this.StudentID == 6){SuitorB = true;}
		#endif

		#if !UNITY_EDITOR
		if (this.StudentID == 28){SuitorB = true;}
		#endif

		if (SuitorB == true)
		{
			if (StudentGlobals.CustomSuitor)
			{
				if (StudentGlobals.CustomSuitorEyewear > 0)
				{
					this.Eyewear[StudentGlobals.CustomSuitorEyewear].SetActive(true);
				}
			}
		}

		///////////////////////////
		///// SENPAI-SPECIFIC /////
		///////////////////////////

		if (this.StudentID == 1)
		{
			if (SenpaiGlobals.CustomSenpai)
			{
				if (SenpaiGlobals.SenpaiEyeWear > 0)
				{
					this.Eyewear[SenpaiGlobals.SenpaiEyeWear].SetActive(true);
				}

				this.FacialHairstyle = SenpaiGlobals.SenpaiFacialHair;
				this.HairColor = SenpaiGlobals.SenpaiHairColor;
				this.EyeColor = SenpaiGlobals.SenpaiEyeColor;
				this.Hairstyle = SenpaiGlobals.SenpaiHairStyle;
			}
		}

		////////////////////
		///// CLOTHING /////
		////////////////////

		//If we are female...
		if (!this.Male)
		{
			if (!this.Teacher)
			{
				//Debug.Log("My hairstyle is: " + this.Hairstyle);

				this.FemaleHair[this.Hairstyle].SetActive(true);
				this.HairRenderer = this.FemaleHairRenderers[this.Hairstyle];
				this.SetFemaleUniform();
			}
			else
			{
				this.TeacherHair[this.Hairstyle].SetActive(true);
				this.HairRenderer = this.TeacherHairRenderers[this.Hairstyle];

				if (this.Club == ClubType.Teacher)
				{
					// Body.
					this.MyRenderer.materials[1].mainTexture = this.TeacherBodyTexture;

					// Face.
					this.MyRenderer.materials[2].mainTexture = this.DefaultFaceTexture;

					// Arms.
					this.MyRenderer.materials[0].mainTexture = this.TeacherBodyTexture;
				}
				else if (this.Club == ClubType.GymTeacher)
				{
					if (StudentGlobals.GetStudentReplaced(this.StudentID))
					{
						// Face.
						this.MyRenderer.materials[2].mainTexture = this.DefaultFaceTexture;

						// Body.
						this.MyRenderer.materials[0].mainTexture = this.CoachPaleBodyTexture;

						// Arms.
						this.MyRenderer.materials[1].mainTexture = this.CoachPaleBodyTexture;
					}
					else
					{
						// Face.
						this.MyRenderer.materials[2].mainTexture = this.CoachFaceTexture;

						// Body.
						this.MyRenderer.materials[0].mainTexture = this.CoachBodyTexture;

						// Arms.
						this.MyRenderer.materials[1].mainTexture = this.CoachBodyTexture;
					}
				}
				else if (this.Club == ClubType.Nurse)
				{
					this.MyRenderer.materials = this.NurseMaterials;

					/*
					// Body.
					this.MyRenderer.materials[0].mainTexture = this.NurseBodyTexture;

					// Camisole.
					this.MyRenderer.materials[1].mainTexture = this.NurseCamiTexture;

					// Stockings.
					this.MyRenderer.materials[2].mainTexture = this.NurseStockingsTexture;

					// Face.
					//this.MyRenderer.materials[3].mainTexture = this.DefaultFaceTexture;
					*/
				}
			}
		}
		//If we are male...
		else
		{
			if (this.Hairstyle > 0)
			{
				this.MaleHair[this.Hairstyle].SetActive(true);
				this.HairRenderer = this.MaleHairRenderers[this.Hairstyle];
			}

			if (this.FacialHairstyle > 0)
			{
				this.FacialHair[this.FacialHairstyle].SetActive(true);
				this.FacialHairRenderer = this.FacialHairRenderers[this.FacialHairstyle];
			}

			if (this.EyewearID > 0)
			{
				this.Eyewear[this.EyewearID].SetActive(true);
			}

			this.SetMaleUniform();
		}

		///////////////////////
		///// ACCESSORIES /////
		///////////////////////

		if (!this.Male)
		{
			if (!this.Teacher)
			{
				if (this.FemaleAccessories[this.Accessory] != null)
				{
					this.FemaleAccessories[this.Accessory].SetActive(true);
				}
			}
			else
			{
				if (this.TeacherAccessories[this.Accessory] != null)
				{
					this.TeacherAccessories[this.Accessory].SetActive(true);
				}
			}
		}
		else
		{
			if (this.MaleAccessories[this.Accessory] != null)
			{
				this.MaleAccessories[this.Accessory].SetActive(true);
			}
		}

		////////////////////////////
		///// CLUB ACCESSORIES /////
		////////////////////////////

		if (!this.Empty)
		{
			if (this.Club < ClubType.Gaming)
			{
				if (this.ClubAccessories[(int)this.Club] != null)
				{
					if (!ClubGlobals.GetClubClosed(this.Club))
					{
						if (this.StudentID != 26)
						{
							this.ClubAccessories[(int)this.Club].SetActive(true);
						}
					}
				}
			}

			if (this.StudentID == 36)
			{
				this.ClubAccessories[(int)this.Club].SetActive(true);
			}

			if (this.Club == ClubType.Cooking)
			{
				this.ClubAccessories[(int)this.Club].SetActive(false);
				this.ClubAccessories[(int)this.Club] = Kerchiefs[this.StudentID];

				if (!ClubGlobals.GetClubClosed(this.Club))
				{
					this.ClubAccessories[(int)this.Club].SetActive(true);
				}
			}
			else if (this.Club == ClubType.Drama)
			{
				this.ClubAccessories[(int)this.Club].SetActive(false);
				this.ClubAccessories[(int)this.Club] = Roses[this.StudentID];

				if (!ClubGlobals.GetClubClosed(this.Club))
				{
					this.ClubAccessories[(int)this.Club].SetActive(true);
				}
			}
			else if (this.Club == ClubType.Art)
			{
				this.ClubAccessories[(int)this.Club].GetComponent<MeshFilter>().sharedMesh = Berets[this.StudentID];
			}
			else if (this.Club == ClubType.Science)
			{
				this.ClubAccessories[(int)this.Club].SetActive(false);
				this.ClubAccessories[(int)this.Club] = Scanners[this.StudentID];

				if (!ClubGlobals.GetClubClosed(this.Club))
				{
					this.ClubAccessories[(int)this.Club].SetActive(true);
				}
			}
			else if (this.Club == ClubType.LightMusic)
			{
				this.ClubAccessories[(int)this.Club].SetActive(false);
				this.ClubAccessories[(int)this.Club] = MusicNotes[this.StudentID - 50];

				if (!ClubGlobals.GetClubClosed(this.Club))
				{
					this.ClubAccessories[(int)this.Club].SetActive(true);
				}
			}
			else if (this.Club == ClubType.Sports)
			{
				this.ClubAccessories[(int)this.Club].SetActive(false);
				this.ClubAccessories[(int)this.Club] = Goggles[this.StudentID];

				if (!ClubGlobals.GetClubClosed(this.Club))
				{
					this.ClubAccessories[(int)this.Club].SetActive(true);
				}
			}
			else if (this.Club == ClubType.Gardening)
			{
				this.ClubAccessories[(int)this.Club].SetActive(false);
				this.ClubAccessories[(int)this.Club] = Flowers[this.StudentID];

				if (!ClubGlobals.GetClubClosed(this.Club))
				{
					this.ClubAccessories[(int)this.Club].SetActive(true);
				}
			}
			else if (this.Club == ClubType.Gaming)
			{
				this.ClubAccessories[(int)this.Club].SetActive(false);
				this.ClubAccessories[(int)this.Club] = RedCloth[this.StudentID];

				if (!ClubGlobals.GetClubClosed(this.Club))
				{
					if (this.ClubAccessories[(int)this.Club] != null)
					{
						this.ClubAccessories[(int)this.Club].SetActive(true);
					}
				}
			}
		}

		if (this.StudentID == 36)
		{
			if (TaskGlobals.GetTaskStatus(36) == 3)
			{
				this.ClubAccessories[(int)this.Club].SetActive(false);
			}
		}

		/////////////////
		///// GIFTS /////
		/////////////////

		//Anti-Osana Code
		#if UNITY_EDITOR
		if (this.StudentID == 11)
		{
			if (!TakingPortrait && !Cutscene)
			{
				this.CatGifts[1].SetActive(CollectibleGlobals.GetGiftGiven(1));
				this.CatGifts[2].SetActive(CollectibleGlobals.GetGiftGiven(2));
				this.CatGifts[3].SetActive(CollectibleGlobals.GetGiftGiven(3));
				this.CatGifts[4].SetActive(CollectibleGlobals.GetGiftGiven(4));
			}
		}
		#endif

		/////////////////////
		///// STOCKINGS /////
		/////////////////////

		if (!this.Male)
		{
			this.StartCoroutine(this.PutOnStockings());
		}

		////////////////
		///// EYES /////
		////////////////

		if (!this.Randomize)
		{
			if (this.EyeColor != string.Empty)
			{
				if (this.EyeColor == "White")
				{
					this.CorrectColor = new Color(1.0f, 1.0f, 1.0f);
				}
				else if (this.EyeColor == "Black")
				{
					this.CorrectColor = new Color(0.50f, 0.50f, 0.50f);
				}
				else if (this.EyeColor == "Red")
				{
					this.CorrectColor = new Color(1.0f, 0.0f, 0.0f);
				}
				else if (this.EyeColor == "Yellow")
				{
					this.CorrectColor = new Color(1.0f, 1.0f, 0.0f);
				}
				else if (this.EyeColor == "Green")
				{
					this.CorrectColor = new Color(0.0f, 1.0f, 0.0f);
				}
				else if (this.EyeColor == "Cyan")
				{
					this.CorrectColor = new Color(0.0f, 1.0f, 1.0f);
				}
				else if (this.EyeColor == "Blue")
				{
					this.CorrectColor = new Color(0.0f, 0.0f, 1.0f);
				}
				else if (this.EyeColor == "Purple")
				{
					this.CorrectColor = new Color(1.0f, 0.0f, 1.0f);
				}
				else if (this.EyeColor == "Orange")
				{
					this.CorrectColor = new Color(1.0f, 0.50f, 0.0f);
				}
				else if (this.EyeColor == "Brown")
				{
					this.CorrectColor = new Color(0.50f, 0.25f, 0.0f);
				}
				else
				{
					this.CorrectColor = new Color(0.0f, 0.0f, 0.0f);
				}

				if (this.StudentID > 90 && this.StudentID < 97)
				{
					this.CorrectColor.r = this.CorrectColor.r * .5f;
					this.CorrectColor.g = this.CorrectColor.g * .5f;
					this.CorrectColor.b = this.CorrectColor.b * .5f;
				}

				if (this.CorrectColor != new Color(0.0f, 0.0f, 0.0f))
				{
					this.RightEyeRenderer.material.color = this.CorrectColor;
					this.LeftEyeRenderer.material.color = this.CorrectColor;
				}
			}
		}
		else
		{
			float R = Random.Range(0.0f, 1.0f);
			float G = Random.Range(0.0f, 1.0f);
			float B = Random.Range(0.0f, 1.0f);

			this.RightEyeRenderer.material.color = new Color(R, G, B);
			this.LeftEyeRenderer.material.color = new Color(R, G, B);
		}

		////////////////
		///// HAIR /////
		////////////////

		if (!this.Randomize)
		{
			if (this.HairColor == "White")
			{
				this.ColorValue = new Color(1.0f, 1.0f, 1.0f);
			}
			else if (this.HairColor == "Black")
			{
				this.ColorValue = new Color(0.50f, 0.50f, 0.50f);
			}
			else if (this.HairColor == "SolidBlack")
			{
				this.ColorValue = new Color(0.0001f, 0.0001f, 0.0001f);
			}
			else if (this.HairColor == "Red")
			{
				this.ColorValue = new Color(1.0f, 0.0f, 0.0f);
			}
			else if (this.HairColor == "Yellow")
			{
				this.ColorValue = new Color(1.0f, 1.0f, 0.0f);
			}
			else if (this.HairColor == "Green")
			{
				this.ColorValue = new Color(0.0f, 1.0f, 0.0f);
			}
			else if (this.HairColor == "Cyan")
			{
				this.ColorValue = new Color(0.0f, 1.0f, 1.0f);
			}
			else if (this.HairColor == "Blue")
			{
				this.ColorValue = new Color(0.0f, 0.0f, 1.0f);
			}
			else if (this.HairColor == "Purple")
			{
				this.ColorValue = new Color(1.0f, 0.0f, 1.0f);
			}
			else if (this.HairColor == "Orange")
			{
				this.ColorValue = new Color(1.0f, 0.50f, 0.0f);
			}
			else if (this.HairColor == "Brown")
			{
				this.ColorValue = new Color(0.50f, 0.25f, 0.0f);
			}
			else
			{
				this.ColorValue = new Color(0.0f, 0.0f, 0.0f);

				this.RightIrisLight.SetActive(false);
				this.LeftIrisLight.SetActive(false);
			}

			if (this.StudentID > 90 && this.StudentID < 97)
			{
				this.ColorValue.r = this.ColorValue.r * .5f;
				this.ColorValue.g = this.ColorValue.g * .5f;
				this.ColorValue.b = this.ColorValue.b * .5f;
			}

			if (this.ColorValue == new Color(0.0f, 0.0f, 0.0f))
			{
				this.RightEyeRenderer.material.mainTexture = this.HairRenderer.material.mainTexture;
				this.LeftEyeRenderer.material.mainTexture = this.HairRenderer.material.mainTexture;

				if (!this.DoNotChangeFace)
				{
					//if (this.StudentID == 6){Debug.Log ("Setting face texture to hair texture.");}
					this.FaceTexture = this.HairRenderer.material.mainTexture;
				}

				if (this.Empty)
				{
					this.FaceTexture = this.GrayFace;
				}

				this.CustomHair = true;
			}

			if (!this.CustomHair)
			{
				if (this.Hairstyle > 0)
				{
					if (GameGlobals.LoveSick)
					{
						this.HairRenderer.material.color = new Color(0.1f, 0.1f, 0.1f);

						if (this.HairRenderer.materials.Length > 1)
						{
							this.HairRenderer.materials[1].color = new Color(0.1f, 0.1f, 0.1f);
						}
					}
					else
					{
						this.HairRenderer.material.color = this.ColorValue;
					}
				}
			}
			else
			{
				if (GameGlobals.LoveSick)
				{
					this.HairRenderer.material.color = new Color(0.1f, 0.1f, 0.1f);

					if (this.HairRenderer.materials.Length > 1)
					{
						this.HairRenderer.materials[1].color = new Color(0.1f, 0.1f, 0.1f);
					}
				}
			}

			if (!this.Male)
			{
				if (this.StudentID == 25)
				{
					this.FemaleAccessories[6].GetComponent<Renderer>().material.color = new Color(0.0f, 1.0f, 1.0f);
				}
				else if (this.StudentID == 30)
				{
					this.FemaleAccessories[6].GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 1.0f);
				}
			}
		}
		else
		{
			this.HairRenderer.material.color = new Color(
				Random.Range(0.0f, 1.0f),
				Random.Range(0.0f, 1.0f),
				Random.Range(0.0f, 1.0f));
		}

		if (!this.Teacher)
		{
			if (this.CustomHair)
			{
				if (!this.Male)
				{
					this.MyRenderer.materials[2].mainTexture = this.FaceTexture;
				}
				else
				{
					if (StudentGlobals.MaleUniform == 1)
					{
						this.MyRenderer.materials[2].mainTexture = this.FaceTexture;
					}
					else if (StudentGlobals.MaleUniform < 4)
					{
						this.MyRenderer.materials[1].mainTexture = this.FaceTexture;
					}
					else
					{
						this.MyRenderer.materials[0].mainTexture = this.FaceTexture;
					}
				}
			}
			else
			{
				// [af] Commented in JS code.
				//RightEyeRenderer.material.color = ColorValue;
				//LeftEyeRenderer.material.color = ColorValue;
			}
		}
		else if (this.Teacher)
		{
			// [af] Commented in JS code.
			//if (Club == 100)
			//{
			//HairRenderer.material.color = Color(.5 * 1.0, .25 * 1.0, 0, 1);

			if (StudentGlobals.GetStudentReplaced(this.StudentID))
			{
				Color TeacherRGBA = StudentGlobals.GetStudentColor(this.StudentID);				
				Color TeacherEyeRGBA = StudentGlobals.GetStudentEyeColor(this.StudentID);

				this.HairRenderer.material.color = TeacherRGBA;
				RightEyeRenderer.material.color = TeacherEyeRGBA;
				LeftEyeRenderer.material.color = TeacherEyeRGBA;
			}

			// [af] Commented in JS code.
			//RightEyeRenderer.material.color = HairRenderer.material.color;
			//LeftEyeRenderer.material.color = HairRenderer.material.color;
			//}
		}

		if (this.Male)
		{
			if (this.Accessory == 2)
			{
				this.RightIrisLight.SetActive(false);
				this.LeftIrisLight.SetActive(false);
			}

			if (SceneManager.GetActiveScene().name == SceneNames.PortraitScene)
			{
				this.Character.transform.localScale = new Vector3(0.93f, 0.93f, 0.93f);
			}

			if (this.FacialHairRenderer != null)
			{
				this.FacialHairRenderer.material.color = this.ColorValue;

				if (this.FacialHairRenderer.materials.Length > 1)
				{
					this.FacialHairRenderer.materials[1].color = this.ColorValue;
				}
			}
		}

		// Special Cases.

		//Mysterious Obstacle
		if (this.StudentID == 10)
		{
			//
		}
		// Rainbow Friendship Bracelets.
		if (this.StudentID == 25 || this.StudentID == 30)
		{
			this.FemaleAccessories[6].SetActive(true);

			if (StudentGlobals.GetStudentReputation(this.StudentID) < -33.33333f)
			{
				this.FemaleAccessories[6].SetActive(false);
			}
		}

		// Sakyu Basu's stolen ring.
		if (this.StudentID == 2)
		{
			if (SchemeGlobals.GetSchemeStage(2) == 2 || SchemeGlobals.GetSchemeStage(2) == 100)
			{
				this.FemaleAccessories[3].SetActive(false);
			}
		}
		// Mai Waifu's rainbow eyes.
		else if (this.StudentID == 40)
		{
			if (this.transform.position != Vector3.zero)
			{
				this.RightEyeRenderer.material.mainTexture = this.DefaultFaceTexture;
				this.LeftEyeRenderer.material.mainTexture = this.DefaultFaceTexture;

				this.RightEyeRenderer.gameObject.GetComponent<RainbowScript>().enabled = true;
				this.LeftEyeRenderer.gameObject.GetComponent<RainbowScript>().enabled = true;
			}
		}
		// Geiju's moody eyes.
		else if (this.StudentID == 41)
		{
			this.CharacterAnimation["moodyEyes_00"].layer = 1;
			this.CharacterAnimation.Play("moodyEyes_00");
			this.CharacterAnimation["moodyEyes_00"].weight = 1.0f;
			this.CharacterAnimation.Play("moodyEyes_00");
		}
		// Miyuji's accessories.
		else if (this.StudentID == 51)
		{
			if (!ClubGlobals.GetClubClosed(ClubType.LightMusic))
			{
				this.PunkAccessories[1].SetActive(true);
				this.PunkAccessories[2].SetActive(true);
				this.PunkAccessories[3].SetActive(true);
			}
		}
		// Daphne's Camera.
		else if (this.StudentID == 59)
		{
			this.ClubAccessories[7].transform.localPosition = new Vector3(0, -1.04f, .5f);
			this.ClubAccessories[7].transform.localEulerAngles = new Vector3(-22.5f, 0, 0);
		}
		// Velma's glasses.
		else if (this.StudentID == 60)
		{
			this.FemaleAccessories[13].SetActive(true);
		}

		if (this.Student != null)
		{
			if (this.Student.AoT)
			{
				this.Student.AttackOnTitan();
			}
		}

		if (HomeScene)
		{
			this.Student.CharacterAnimation["idle_00"].time = 9.0f;
			this.Student.CharacterAnimation["idle_00"].speed = 0;
		}

		this.TaskCheck();
		this.TurnOnCheck();

		if (!this.Male)
		{
			//if (this.StudentID < 91)
			//{
				this.EyeTypeCheck();
			//}
		}

		if (this.Kidnapped)
		{
			this.WearIndoorShoes();
		}

		if (!this.Male)
		{
			#if !UNITY_EDITOR

			if (this.Hairstyle == 20 || this.Hairstyle == 21)
			{
				Destroy(this.gameObject);
			}

			#endif
		}
	}

	public int FaceID = 0;
	public int SkinID = 0;
	public int UniformID = 0;

	public void SetMaleUniform()
	{
		if (this.StudentID == 1)
		{
			this.SkinColor = SenpaiGlobals.SenpaiSkinColor;
			this.FaceTexture = this.FaceTextures[this.SkinColor];
		}
		else
		{
			// [af] Replaced if/else statement with ternary expression.
			this.FaceTexture = this.CustomHair ? this.HairRenderer.material.mainTexture :
				this.FaceTextures[this.SkinColor];

			bool SuitorC = false;

			//Anti-Osana Code
			#if UNITY_EDITOR
			if (this.StudentID == 6){SuitorC = true;}
			#endif

			#if !UNITY_EDITOR
			if (this.StudentID == 28){SuitorC = true;}
			#endif

			if (SuitorC == true)
			{
				if (StudentGlobals.CustomSuitor)
				{
					if (StudentGlobals.CustomSuitorTan)
					{
						this.SkinColor = 6;
						this.DoNotChangeFace = true;
						this.FaceTexture = this.FaceTextures[6];

						//Debug.Log ("Setting tan face texture.");
					}
				}
			}
		}

		this.MyRenderer.sharedMesh = this.MaleUniforms[StudentGlobals.MaleUniform];
		this.SchoolUniform = this.MaleUniforms[StudentGlobals.MaleUniform];

		this.UniformTexture = this.MaleUniformTextures[StudentGlobals.MaleUniform];
		this.CasualTexture = this.MaleCasualTextures[StudentGlobals.MaleUniform];
		this.SocksTexture = this.MaleSocksTextures[StudentGlobals.MaleUniform];

		if (StudentGlobals.MaleUniform == 1)
		{
			this.SkinID = 0;
			this.UniformID = 1;
			this.FaceID = 2;
		}
		else if (StudentGlobals.MaleUniform == 2)
		{
			this.UniformID = 0;
			this.FaceID = 1;
			this.SkinID = 2;
		}
		else if (StudentGlobals.MaleUniform == 3)
		{
			this.UniformID = 0;
			this.FaceID = 1;
			this.SkinID = 2;
		}
		else if (StudentGlobals.MaleUniform == 4)
		{
			this.FaceID = 0;
			this.SkinID = 1;
			this.UniformID = 2;
		}
		else if (StudentGlobals.MaleUniform == 5)
		{
			this.FaceID = 0;
			this.SkinID = 1;
			this.UniformID = 2;
		}
		else if (StudentGlobals.MaleUniform == 6)
		{
			this.FaceID = 0;
			this.SkinID = 1;
			this.UniformID = 2;
		}

		if (StudentGlobals.MaleUniform < 2)
		{
			if (this.Club == ClubType.Delinquent)
			{
				this.MyRenderer.sharedMesh = this.DelinquentMesh;

				if (this.StudentID == 76)
				{
					this.UniformTexture = this.EyeTextures[0];
					this.CasualTexture = this.EyeTextures[1];
					this.SocksTexture = this.EyeTextures[2];
				}
				else if (this.StudentID == 77)
				{
					this.UniformTexture = this.CheekTextures[0];
					this.CasualTexture = this.CheekTextures[1];
					this.SocksTexture = this.CheekTextures[2];
				}
				else if (this.StudentID == 78)
				{
					this.UniformTexture = this.ForeheadTextures[0];
					this.CasualTexture = this.ForeheadTextures[1];
					this.SocksTexture = this.ForeheadTextures[2];
				}
				else if (this.StudentID == 79)
				{
					this.UniformTexture = this.MouthTextures[0];
					this.CasualTexture = this.MouthTextures[1];
					this.SocksTexture = this.MouthTextures[2];
				}
				else if (this.StudentID == 80)
				{
					this.UniformTexture = this.NoseTextures[0];
					this.CasualTexture = this.NoseTextures[1];
					this.SocksTexture = this.NoseTextures[2];
				}
			}
		}

		if (this.StudentID == 10)
		{
			this.Student.GymTexture = ObstacleGymTexture;
			this.Student.TowelTexture = ObstacleTowelTexture;
			this.Student.SwimsuitTexture = ObstacleSwimsuitTexture;
		}

		if (this.StudentID == 11)
		{
			this.Student.SwimsuitTexture = OsanaSwimsuitTexture;
		}

		if (this.StudentID == 58)
		{
			this.SkinColor = 8;
			//this.Student.GymTexture = TanGymTexture;
			this.Student.TowelTexture = TanTowelTexture;
			this.Student.SwimsuitTexture = TanSwimsuitTexture;
		}

		if (this.Empty)
		{
			this.UniformTexture = this.MaleUniformTextures[7];
			this.CasualTexture = this.MaleCasualTextures[7];
			this.SocksTexture = this.MaleSocksTextures[7];
			this.FaceTexture = this.GrayFace;
			this.SkinColor = 7;
		}

		if (!this.Student.Indoors)
		{
			this.MyRenderer.materials[this.FaceID].mainTexture = this.FaceTexture;
			this.MyRenderer.materials[this.SkinID].mainTexture = this.SkinTextures[this.SkinColor];
			this.MyRenderer.materials[this.UniformID].mainTexture = this.CasualTexture;
		}
		else
		{
			this.MyRenderer.materials[this.FaceID].mainTexture = this.FaceTexture;
			this.MyRenderer.materials[this.SkinID].mainTexture = this.SkinTextures[this.SkinColor];
			this.MyRenderer.materials[this.UniformID].mainTexture = this.UniformTexture;
		}
	}

	public void SetFemaleUniform()
	{
		if (this.Club != ClubType.Council)
		{
			this.MyRenderer.sharedMesh = this.FemaleUniforms[StudentGlobals.FemaleUniform];
			this.SchoolUniform = this.FemaleUniforms[StudentGlobals.FemaleUniform];

			// Oka.
			/*
			if (this.StudentID == 26)
			{
				this.UniformTexture = this.OccultUniformTextures[StudentGlobals.FemaleUniform];
				this.CasualTexture = this.OccultCasualTextures[StudentGlobals.FemaleUniform];
				this.SocksTexture = this.OccultSocksTextures[StudentGlobals.FemaleUniform];
			}
			*/

			// Ganguro.
			if (this.Club == ClubType.Bully)
			{
				/*
				ModelSwap.Attach(Cardigan, true);
				CardiganRenderer.materials = MyRenderer.materials;

				Destroy(this.MyRenderer);
				MyRenderer = CardiganRenderer;
				Student.MyRenderer = MyRenderer;
				Student.ShoeRemoval.MyRenderer = MyRenderer;
				*/

				this.UniformTexture = this.GanguroUniformTextures[StudentGlobals.FemaleUniform];
				this.CasualTexture = this.GanguroCasualTextures[StudentGlobals.FemaleUniform];
				this.SocksTexture = this.GanguroSocksTextures[StudentGlobals.FemaleUniform];
			}
			// Mysterious Obstacle
			else if (this.StudentID == 10)
			{
				this.UniformTexture = this.ObstacleUniformTextures[StudentGlobals.FemaleUniform];
				this.CasualTexture = this.ObstacleCasualTextures[StudentGlobals.FemaleUniform];
				this.SocksTexture = this.ObstacleSocksTextures[StudentGlobals.FemaleUniform];
			}
			// Rivals
			else if (this.StudentID > 11 && this.StudentID < 21)
			{
				this.MysteriousObstacle = true;

				this.UniformTexture = this.BlackBody;
				this.CasualTexture = this.BlackBody;
				this.SocksTexture = this.BlackBody;
				this.HairRenderer.enabled = false;

				this.RightEyeRenderer.enabled = false;
				this.LeftEyeRenderer.enabled = false;

				this.RightIrisLight.SetActive(false);
				this.LeftIrisLight.SetActive(false);
			}
			// Everyone Else.
			else
			{
				this.UniformTexture = this.FemaleUniformTextures[StudentGlobals.FemaleUniform];
				this.CasualTexture = this.FemaleCasualTextures[StudentGlobals.FemaleUniform];
				this.SocksTexture = this.FemaleSocksTextures[StudentGlobals.FemaleUniform];
			}
		}
		else
		{
			this.RightIrisLight.SetActive(false);
			this.LeftIrisLight.SetActive(false);

			this.MyRenderer.sharedMesh = this.FemaleUniforms[4];
			this.SchoolUniform = this.FemaleUniforms[4];

			this.UniformTexture = this.FemaleUniformTextures[7];
			this.CasualTexture = this.FemaleCasualTextures[7];
			this.SocksTexture = this.FemaleSocksTextures[7];
		}

		if (this.Empty)
		{
			this.UniformTexture = this.FemaleUniformTextures[8];
			this.CasualTexture = this.FemaleCasualTextures[8];
			this.SocksTexture = this.FemaleSocksTextures[8];
		}

		if (!this.Cutscene)
		{
			if (!this.Kidnapped)
			{
				if (!this.Student.Indoors)
				{
					this.MyRenderer.materials[0].mainTexture = this.CasualTexture;
					this.MyRenderer.materials[1].mainTexture = this.CasualTexture;
				}
				else
				{
					this.MyRenderer.materials[0].mainTexture = this.UniformTexture;
					this.MyRenderer.materials[1].mainTexture = this.UniformTexture;
				}
			}
			else
			{
				this.MyRenderer.materials[0].mainTexture = this.UniformTexture;
				this.MyRenderer.materials[1].mainTexture = this.UniformTexture;
			}
		}
		else
		{
			this.UniformTexture = this.FemaleUniformTextures[StudentGlobals.FemaleUniform];
			this.FaceTexture = this.DefaultFaceTexture;

			this.MyRenderer.materials[0].mainTexture = this.UniformTexture;
			this.MyRenderer.materials[1].mainTexture = this.UniformTexture;
		}

		if (this.Club == ClubType.Bully)
		{
			//this.MyRenderer.materials[1].mainTexture = this.CardiganTextures[this.StudentID];
		}

		if (this.MysteriousObstacle)
		{
			this.FaceTexture = this.BlackBody;
		}

		this.MyRenderer.materials[2].mainTexture = this.FaceTexture;

		if (!this.TakingPortrait)
		{
			if (this.Student != null)
			{
				if (this.Student.StudentManager != null)
				{
					if (this.Student.StudentManager.Censor)
					{
						this.CensorPanties();
					}
				}
			}
		}

		if (this.MyStockings != null)
		{
			this.StartCoroutine(this.PutOnStockings());
		}
	}

	public void CensorPanties()
	{
		if (!this.Student.ClubAttire && (this.Student.Schoolwear == 1))
		{
			this.MyRenderer.materials[0].SetFloat("_BlendAmount1", 1.0f);
			this.MyRenderer.materials[1].SetFloat("_BlendAmount1", 1.0f);
		}
		else
		{
			this.RemoveCensor();
		}
	}

	public void RemoveCensor()
	{
		this.MyRenderer.materials[0].SetFloat("_BlendAmount1", 0.0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount1", 0.0f);
	}

	void TaskCheck()
	{
		//Ryuto's bandana
		if (this.StudentID == 37)
		{
			if (TaskGlobals.GetTaskStatus(37) < 3)
			{
				if (!TakingPortrait)
				{
					this.MaleAccessories[1].SetActive(false);
				}
				else
				{
					this.MaleAccessories[1].SetActive(true);
				}
			}
		}
		//Osana
		else if (this.StudentID == 11)
		{
			if (this.PhoneCharms.Length > 0)
			{
				if (TaskGlobals.GetTaskStatus(11) < 3)
				{
					this.PhoneCharms[11].SetActive(false);
				}
				else
				{
					this.PhoneCharms[11].SetActive(true);
				}
			}
		}
	}

	void TurnOnCheck()
	{
		if (!this.TurnedOn)
		{
			if (!this.TakingPortrait)
			{
				if (this.Male)
				{
					if (this.HairColor == "Purple")
					{
						this.LoveManager.Targets[this.LoveManager.TotalTargets] = this.Student.Head;
						this.LoveManager.TotalTargets++;
					}
					else if (this.Hairstyle == 30)
					{
						this.LoveManager.Targets[this.LoveManager.TotalTargets] = this.Student.Head;
						this.LoveManager.TotalTargets++;
					}
					else if (this.Accessory > 1 && this.Accessory < 5 || this.Accessory == 13)
					{
						this.LoveManager.Targets[this.LoveManager.TotalTargets] = this.Student.Head;
						this.LoveManager.TotalTargets++;
					}
					else if (this.Student.Persona == PersonaType.TeachersPet)
					{
						this.LoveManager.Targets[this.LoveManager.TotalTargets] = this.Student.Head;
						this.LoveManager.TotalTargets++;
					}
					else if (this.EyewearID > 0)
					{
						this.LoveManager.Targets[this.LoveManager.TotalTargets] = this.Student.Head;
						this.LoveManager.TotalTargets++;
					}
				}
			}
		}

		this.TurnedOn = true;
	}

	void DestroyUnneccessaryObjects()
	{
		// [af] Converted while loop to foreach loop.
		foreach (GameObject accessory in this.FemaleAccessories)
		{
			if (accessory != null)
			{
				if (!accessory.activeInHierarchy)
				{
					Destroy(accessory);
				}
			}
		}

		// [af] Converted while loop to foreach loop.
		foreach (GameObject accessory in this.MaleAccessories)
		{
			if (accessory != null)
			{
				if (!accessory.activeInHierarchy)
				{
					Destroy(accessory);
				}
			}
		}

		// [af] Converted while loop to foreach loop.
		foreach (GameObject accessory in this.ClubAccessories)
		{
			if (accessory != null)
			{
				if (!accessory.activeInHierarchy)
				{
					Destroy(accessory);
				}
			}
		}

		// [af] Converted while loop to foreach loop.
		foreach (GameObject accessory in this.TeacherAccessories)
		{
			if (accessory != null)
			{
				if (!accessory.activeInHierarchy)
				{
					Destroy(accessory);
				}
			}
		}

		// [af] Converted while loop to foreach loop.
		foreach (GameObject hair in this.TeacherHair)
		{
			if (hair != null)
			{
				if (!hair.activeInHierarchy)
				{
					Destroy(hair);
				}
			}
		}

		// [af] Converted while loop to foreach loop.
		foreach (GameObject hair in this.FemaleHair)
		{
			if (hair != null)
			{
				if (!hair.activeInHierarchy)
				{
					Destroy(hair);
				}
			}
		}

		// [af] Converted while loop to foreach loop.
		foreach (GameObject hair in this.MaleHair)
		{
			if (hair != null)
			{
				if (!hair.activeInHierarchy)
				{
					Destroy(hair);
				}
			}
		}

		// [af] Converted while loop to foreach loop.
		foreach (GameObject facialHair in this.FacialHair)
		{
			if (facialHair != null)
			{
				if (!facialHair.activeInHierarchy)
				{
					Destroy(facialHair);
				}
			}
		}

		// [af] Converted while loop to foreach loop.
		foreach (GameObject eyewear in this.Eyewear)
		{
			if (eyewear != null)
			{
				if (!eyewear.activeInHierarchy)
				{
					Destroy(eyewear);
				}
			}
		}

		// [af] Converted while loop to two foreach loops.
		foreach (GameObject stockings in this.RightStockings)
		{
			if (stockings != null)
			{
				if (!stockings.activeInHierarchy)
				{
					Destroy(stockings);
				}
			}
		}

		foreach (GameObject stockings in this.LeftStockings)
		{
			if (stockings != null)
			{
				if (!stockings.activeInHierarchy)
				{
					Destroy(stockings);
				}
			}
		}
	}

	public IEnumerator PutOnStockings()
	{
		this.RightStockings[0].SetActive(false);
		this.LeftStockings[0].SetActive(false);

		if (this.Stockings == string.Empty)
		{
			this.MyStockings = null;
		}
		else if (this.Stockings == "Red")
		{
			this.MyStockings = this.RedStockings;
		}
		else if (this.Stockings == "Yellow")
		{
			this.MyStockings = this.YellowStockings;
		}
		else if (this.Stockings == "Green")
		{
			this.MyStockings = this.GreenStockings;
		}
		else if (this.Stockings == "Cyan")
		{
			this.MyStockings = this.CyanStockings;
		}
		else if (this.Stockings == "Blue")
		{
			this.MyStockings = this.BlueStockings;
		}
		else if (this.Stockings == "Purple")
		{
			this.MyStockings = this.PurpleStockings;
		}
		else if (this.Stockings == "ShortGreen")
		{
			this.MyStockings = this.GreenSocks;
		}
		else if (this.Stockings == "ShortBlack")
		{
			this.MyStockings = this.BlackKneeSocks;
		}
		else if (this.Stockings == "Black")
		{
			this.MyStockings = this.BlackStockings;
		}
		else if (this.Stockings == "Osana")
		{
			this.MyStockings = this.OsanaStockings;
		}
		else if (this.Stockings == "Kizana")
		{
			this.MyStockings = this.KizanaStockings;
		}
		else if (this.Stockings == "Council1")
		{
			this.MyStockings = this.TurtleStockings;
		}
		else if (this.Stockings == "Council2")
		{
			this.MyStockings = this.TigerStockings;
		}
		else if (this.Stockings == "Council3")
		{
			this.MyStockings = this.BirdStockings;
		}
		else if (this.Stockings == "Council4")
		{
			this.MyStockings = this.DragonStockings;
		}
		else if (this.Stockings == "Music1")
		{
			if (!ClubGlobals.GetClubClosed(ClubType.LightMusic))
			{
				this.MyStockings = this.MusicStockings[1];
			}
		}
		else if (this.Stockings == "Music2")
		{
			this.MyStockings = this.MusicStockings[2];
		}
		else if (this.Stockings == "Music3")
		{
			this.MyStockings = this.MusicStockings[3];
		}
		else if (this.Stockings == "Music4")
		{
			this.MyStockings = this.MusicStockings[4];
		}
		else if (this.Stockings == "Music5")
		{
			this.MyStockings = this.MusicStockings[5];
		}
		else if (this.Stockings == "Custom1")
		{
			WWW NewCustomStockings = new WWW("file:///" + Application.streamingAssetsPath + "/CustomStockings1.png");
			yield return NewCustomStockings;
			if (NewCustomStockings.error == null){this.CustomStockings[1] = NewCustomStockings.texture;}
			this.MyStockings = this.CustomStockings[1];
		}
		else if (this.Stockings == "Custom2")
		{
			WWW NewCustomStockings = new WWW("file:///" + Application.streamingAssetsPath + "/CustomStockings2.png");
			yield return NewCustomStockings;
			if (NewCustomStockings.error == null){this.CustomStockings[2] = NewCustomStockings.texture;}
			this.MyStockings = this.CustomStockings[2];
		}
		else if (this.Stockings == "Custom3")
		{
			WWW NewCustomStockings = new WWW("file:///" + Application.streamingAssetsPath + "/CustomStockings3.png");
			yield return NewCustomStockings;
			if (NewCustomStockings.error == null){this.CustomStockings[3] = NewCustomStockings.texture;}
			this.MyStockings = this.CustomStockings[3];
		}
		else if (this.Stockings == "Custom4")
		{
			WWW NewCustomStockings = new WWW("file:///" + Application.streamingAssetsPath + "/CustomStockings4.png");
			yield return NewCustomStockings;
			if (NewCustomStockings.error == null){this.CustomStockings[4] = NewCustomStockings.texture;}
			this.MyStockings = this.CustomStockings[4];
		}
		else if (this.Stockings == "Custom5")
		{
			WWW NewCustomStockings = new WWW("file:///" + Application.streamingAssetsPath + "/CustomStockings5.png");
			yield return NewCustomStockings;
			if (NewCustomStockings.error == null){this.CustomStockings[5] = NewCustomStockings.texture;}
			this.MyStockings = this.CustomStockings[5];
		}
		else if (this.Stockings == "Custom6")
		{
			WWW NewCustomStockings = new WWW("file:///" + Application.streamingAssetsPath + "/CustomStockings6.png");
			yield return NewCustomStockings;
			if (NewCustomStockings.error == null){this.CustomStockings[6] = NewCustomStockings.texture;}
			this.MyStockings = this.CustomStockings[6];
		}
		else if (this.Stockings == "Custom7")
		{
			WWW NewCustomStockings = new WWW("file:///" + Application.streamingAssetsPath + "/CustomStockings7.png");
			yield return NewCustomStockings;
			if (NewCustomStockings.error == null){this.CustomStockings[7] = NewCustomStockings.texture;}
			this.MyStockings = this.CustomStockings[7];
		}
		else if (this.Stockings == "Custom8")
		{
			WWW NewCustomStockings = new WWW("file:///" + Application.streamingAssetsPath + "/CustomStockings8.png");
			yield return NewCustomStockings;
			if (NewCustomStockings.error == null){this.CustomStockings[8] = NewCustomStockings.texture;}
			this.MyStockings = this.CustomStockings[8];
		}
		else if (this.Stockings == "Custom9")
		{
			WWW NewCustomStockings = new WWW("file:///" + Application.streamingAssetsPath + "/CustomStockings9.png");
			yield return NewCustomStockings;
			if (NewCustomStockings.error == null){this.CustomStockings[9] = NewCustomStockings.texture;}
			this.MyStockings = this.CustomStockings[9];
		}
		else if (this.Stockings == "Custom10")
		{
			WWW NewCustomStockings = new WWW("file:///" + Application.streamingAssetsPath + "/CustomStockings10.png");
			yield return NewCustomStockings;
			if (NewCustomStockings.error == null){this.CustomStockings[10] = NewCustomStockings.texture;}
			this.MyStockings = this.CustomStockings [10];
		}

		else if (this.Stockings == "Loose")
		{
			this.MyStockings = null;

			this.RightStockings[0].SetActive(true);
			this.LeftStockings[0].SetActive(true);
		}

		if (this.MyStockings != null)
		{
			this.MyRenderer.materials[0].SetTexture("_OverlayTex", this.MyStockings);
			this.MyRenderer.materials[1].SetTexture("_OverlayTex", this.MyStockings);

			this.MyRenderer.materials[0].SetFloat("_BlendAmount", 1.0f);
			this.MyRenderer.materials[1].SetFloat("_BlendAmount", 1.0f);
		}
		else
		{
			this.MyRenderer.materials[0].SetTexture("_OverlayTex", null);
			this.MyRenderer.materials[1].SetTexture("_OverlayTex", null);

			this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
			this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);
		}
	}

	public void WearIndoorShoes()
	{
		if (!this.Male)
		{
			this.MyRenderer.materials[0].mainTexture = this.CasualTexture;
			this.MyRenderer.materials[1].mainTexture = this.CasualTexture;
		}
		else
		{
			this.MyRenderer.materials[this.UniformID].mainTexture = this.CasualTexture;
		}
	}

	public void WearOutdoorShoes()
	{
		if (!this.Male)
		{
			this.MyRenderer.materials[0].mainTexture = this.UniformTexture;
			this.MyRenderer.materials[1].mainTexture = this.UniformTexture;
		}
		else
		{
			this.MyRenderer.materials[this.UniformID].mainTexture = this.UniformTexture;
		}
	}

	public void EyeTypeCheck()
	{
		//0 - Smile
		//1 - Angry
		//2 - Mouth Open
		//3 - Ears
		//4 - BigNose
		//5 - Eyes Closed
		//6 - Sad
		//7 - Insanity
		//8 - Thin
		//9 - Round
		//10 - Delinquent
		//11 - Naughty
		//12 - Gentle

		int Modifier = 0;

		if (this.EyeType == "Thin")
		{
			MyRenderer.SetBlendShapeWeight(8, 100);//Thin
			MyRenderer.SetBlendShapeWeight(9, 100);//Round
			StudentManager.Thins++;

			Modifier = StudentManager.Thins;
		}
		else if (this.EyeType == "Serious")
		{
			MyRenderer.SetBlendShapeWeight(5, 50);//Eyes Closed
			MyRenderer.SetBlendShapeWeight(9, 100);//Round
			StudentManager.Seriouses++;

			Modifier = StudentManager.Seriouses;
		}
		else if (this.EyeType == "Round")
		{
			MyRenderer.SetBlendShapeWeight(5, 15);//Eyes closed
			MyRenderer.SetBlendShapeWeight(9, 100);//Round
			StudentManager.Rounds++;

			Modifier = StudentManager.Rounds;
		}
		else if (this.EyeType == "Sad")
		{
			MyRenderer.SetBlendShapeWeight(0, 50);//Smile
			MyRenderer.SetBlendShapeWeight(5, 15);//Eyes Closed
			MyRenderer.SetBlendShapeWeight(6, 50);//Sad
			MyRenderer.SetBlendShapeWeight(8, 50);//Thin
			MyRenderer.SetBlendShapeWeight(9, 100);//Round
			StudentManager.Sads++;

			Modifier = StudentManager.Sads;
		}
		else if (this.EyeType == "Mean")
		{
			MyRenderer.SetBlendShapeWeight(10, 100);//Delinquent
			StudentManager.Means++;

			Modifier = StudentManager.Means;
		}
		else if (this.EyeType == "Smug")
		{
			MyRenderer.SetBlendShapeWeight(0, 50);//Smile
			MyRenderer.SetBlendShapeWeight(5, 25);//Eyes Closed
			StudentManager.Smugs++;

			Modifier = StudentManager.Smugs;
		}
		else if (this.EyeType == "Gentle")
		{
			MyRenderer.SetBlendShapeWeight(9, 100);//Round
			MyRenderer.SetBlendShapeWeight(12, 100);//Gentle
			StudentManager.Gentles++;

			Modifier = StudentManager.Gentles;
		}
		else if (this.EyeType == "MO")
		{
			MyRenderer.SetBlendShapeWeight(8, 50);//Thin
			MyRenderer.SetBlendShapeWeight(9, 100);//Round
			MyRenderer.SetBlendShapeWeight(12, 100);//Gentle
			StudentManager.Gentles++;

			Modifier = StudentManager.Gentles;
		}
		else if (this.EyeType == "Rival1")
		{
			//MyRenderer.SetBlendShapeWeight(0, 35);
			MyRenderer.SetBlendShapeWeight(8, 5);
			MyRenderer.SetBlendShapeWeight(9, 20);
			MyRenderer.SetBlendShapeWeight(10, 50);
			MyRenderer.SetBlendShapeWeight(11, 50);
			MyRenderer.SetBlendShapeWeight(12, 10);
			StudentManager.Rival1s++;

			Modifier = StudentManager.Rival1s;
		}

		if (!this.Modified)
		{
			if (this.EyeType == "Thin" && StudentManager.Thins > 1 || this.EyeType == "Serious" && StudentManager.Seriouses > 1 ||
				this.EyeType == "Round" && StudentManager.Rounds > 1 || this.EyeType == "Sad" && StudentManager.Sads > 1 ||
				this.EyeType == "Mean" && StudentManager.Means > 1 || this.EyeType == "Smug" && StudentManager.Smugs > 1 ||
				this.EyeType == "Gentle" && StudentManager.Gentles > 1)
			{
				MyRenderer.SetBlendShapeWeight(8, MyRenderer.GetBlendShapeWeight(8) + (Modifier * 1));
				MyRenderer.SetBlendShapeWeight(9, MyRenderer.GetBlendShapeWeight(9) + (Modifier * 1));
				MyRenderer.SetBlendShapeWeight(10, MyRenderer.GetBlendShapeWeight(10) + (Modifier * 1));
				//MyRenderer.SetBlendShapeWeight(11, MyRenderer.GetBlendShapeWeight(11) + (Modifier * 1));
				MyRenderer.SetBlendShapeWeight(12, MyRenderer.GetBlendShapeWeight(12) + (Modifier * 1));
			}

			this.Modified = true;
		}
	}

	public void DeactivateBullyAccessories()
	{
		if (StudentGlobals.FemaleUniform < 2 || StudentGlobals.FemaleUniform == 3)
		{
			this.RightWristband.SetActive(false);
			this.LeftWristband.SetActive(false);
		}

		this.Bookbag.SetActive(false);
		this.Hoodie.SetActive(false);
	}

	public void ActivateBullyAccessories()
	{
		if (StudentGlobals.FemaleUniform < 2 || StudentGlobals.FemaleUniform == 3)
		{
			this.RightWristband.SetActive(true);
			this.LeftWristband.SetActive(true);
		}

		this.Bookbag.SetActive(true);
		this.Hoodie.SetActive(true);
	}

	public void LoadCosmeticSheet(StudentCosmeticSheet mySheet)
	{
		//If the loaded CosmeticSheet is not for this student's gender then the function returns
		if (Male != mySheet.Male)
			return;

		// Apply Cosmetics
		Accessory =  mySheet.Accessory;
		Hairstyle =  mySheet.Hairstyle;
		Stockings =  mySheet.Stockings;
		BreastSize =  mySheet.BreastSize;

		//Enables Cosmetics.
		Start();

		// Apply Haircolor
		ColorValue = mySheet.HairColor;
		HairRenderer.material.color = ColorValue;

		if (mySheet.CustomHair)
		{
			RightEyeRenderer.material.mainTexture = HairRenderer.material.mainTexture;
			LeftEyeRenderer.material.mainTexture = HairRenderer.material.mainTexture;
			FaceTexture = HairRenderer.material.mainTexture;
			LeftIrisLight.SetActive(false);
			RightIrisLight.SetActive(false);
			CustomHair = true;
		}

		// Apply Eyecolor
		CorrectColor = mySheet.EyeColor;

		RightEyeRenderer.material.color = CorrectColor;
		LeftEyeRenderer.material.color = CorrectColor;

		// Applying Schoolwear
		Student.Schoolwear = mySheet.Schoolwear;
		Student.ChangeSchoolwear();

		//Apply Bloodiness
		if (mySheet.Bloody)
		{
			Student.LiquidProjector.material.mainTexture = Student.BloodTexture;
			Student.LiquidProjector.enabled = true;
		}

		//Female only
		if (!Male)
		{
			// Apply Stockings
			Stockings = mySheet.Stockings;
			base.StartCoroutine(Student.Cosmetic.PutOnStockings());

			//Apply Blendshapes
			for (int count = 0; count < MyRenderer.sharedMesh.blendShapeCount; count++)
			{
				MyRenderer.SetBlendShapeWeight(count, mySheet.Blendshapes[count]);
			}
		}
	}

	public StudentCosmeticSheet CosmeticSheet()
	{
		// Creating a Cosmetic Sheet
		StudentCosmeticSheet mySheet = new StudentCosmeticSheet();
		mySheet.Blendshapes = new List<float>();

		// Add Cosmetics
		mySheet.Male = Male;
		mySheet.CustomHair = CustomHair;
		mySheet.Accessory = Accessory;
		mySheet.Hairstyle = Hairstyle;
		mySheet.Stockings = Stockings;
		mySheet.BreastSize = BreastSize;

		// Add CustomHair
		mySheet.CustomHair = CustomHair;

		// Add Schoolwear
		mySheet.Schoolwear = Student.Schoolwear;

		// Add Bloodiness		
		mySheet.Bloody = Student.LiquidProjector.enabled && Student.LiquidProjector.material.mainTexture == Student.BloodTexture;

		// Add Haircolor
		mySheet.HairColor = HairRenderer.material.color;

		// Add Eyecolor
		mySheet.EyeColor = RightEyeRenderer.material.color;


		// Add Blendshapes
		if (!Male)
		{
			for (int count = 0; count < MyRenderer.sharedMesh.blendShapeCount; count++)
			{
				mySheet.Blendshapes.Add(MyRenderer.GetBlendShapeWeight(count));
			}
		}

		// Returning the created sheet
		return mySheet;
	}
}