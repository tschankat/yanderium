using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugMenuScript : MonoBehaviour
{
	public FakeStudentSpawnerScript FakeStudentSpawner;
	public DelinquentManagerScript DelinquentManager;
	public StudentManagerScript StudentManager;
	public CameraEffectsScript CameraEffects;
	public WeaponManagerScript WeaponManager;
	public ReputationScript Reputation;
	public CounselorScript Counselor;
	public YandereScript Yandere;
	public BentoScript Bento;
	public ClockScript Clock;
	public PrayScript Turtle;
	public ZoomScript Zoom;
	public AstarPath Astar;

	public OsanaFridayBeforeClassEvent1Script OsanaEvent1;
	public OsanaFridayBeforeClassEvent2Script OsanaEvent2;
	public OsanaFridayLunchEventScript OsanaEvent3;

	public GameObject EasterEggWindow;
	public GameObject SacrificialArm;
	public GameObject DebugPoisons;
	public GameObject CircularSaw;
	public GameObject GreenScreen;
	public GameObject Knife;

	public Transform[] TeleportSpot;

	public Transform RooftopSpot;
	public Transform MidoriSpot;
	public Transform Lockers;

	public GameObject MissionModeWindow;
	public GameObject Window;

	public GameObject[] ElectrocutionKit;

	public bool WaitingForNumber = false;
	public bool TryNextFrame = false;
	public bool MissionMode = false;
	public bool NoDebug = false;

	public int RooftopStudent = 7;
	public int DebugInputs = 0;

	public float Timer = 0;

	public int ID = 0;

	void Start()
	{
		this.transform.localPosition = new Vector3(
			this.transform.localPosition.x,
			0.0f,
			this.transform.localPosition.z);

		this.MissionModeWindow.SetActive(false);
		this.Window.SetActive(false);

		if (MissionModeGlobals.MissionMode || GameGlobals.AlphabetMode)
		{
			this.MissionMode = true;
		}

		if (GameGlobals.LoveSick)
		{
			this.NoDebug = true;
		}

#if UNITY_EDITOR
		//this.MissionMode = false;
		//this.NoDebug = false;
#endif
	}

	void Update()
	{
		if (!this.MissionMode && !this.NoDebug)
		{
			if (!this.Yandere.InClass && !this.Yandere.Chased && this.Yandere.Chasers == 0 && this.Yandere.CanMove)
			{
				if (Input.GetKeyDown(KeyCode.Backslash))
				{
					if (this.Yandere.transform.position.y < 100.0f)
					{
						this.EasterEggWindow.SetActive(false);

						// [af] Replaced if/else statement with boolean expression.
						this.Window.SetActive(!this.Window.activeInHierarchy);
					}
				}
			}
			else
			{
				if (this.Window.activeInHierarchy)
				{
					this.Window.SetActive(false);
				}
			}

			if (this.Window.activeInHierarchy)
			{
				if (Input.GetKeyDown(KeyCode.F1))
				{
					StudentGlobals.FemaleUniform = 1;
					StudentGlobals.MaleUniform = 1;
					SceneManager.LoadScene(SceneNames.LoadingScene);
				}
				else if (Input.GetKeyDown(KeyCode.F2))
				{
					StudentGlobals.FemaleUniform = 2;
					StudentGlobals.MaleUniform = 2;
					SceneManager.LoadScene(SceneNames.LoadingScene);
				}
				else if (Input.GetKeyDown(KeyCode.F3))
				{
					StudentGlobals.FemaleUniform = 3;
					StudentGlobals.MaleUniform = 3;
					SceneManager.LoadScene(SceneNames.LoadingScene);
				}
				else if (Input.GetKeyDown(KeyCode.F4))
				{
					StudentGlobals.FemaleUniform = 4;
					StudentGlobals.MaleUniform = 4;
					SceneManager.LoadScene(SceneNames.LoadingScene);
				}
				else if (Input.GetKeyDown(KeyCode.F5))
				{
					StudentGlobals.FemaleUniform = 5;
					StudentGlobals.MaleUniform = 5;
					SceneManager.LoadScene(SceneNames.LoadingScene);
				}
				else if (Input.GetKeyDown(KeyCode.F6))
				{
					StudentGlobals.FemaleUniform = 6;
					StudentGlobals.MaleUniform = 6;
					SceneManager.LoadScene(SceneNames.LoadingScene);
				}
				else if (Input.GetKeyDown(KeyCode.F7))
				{
					ID = 1;

					while (ID < 8)
					{
						this.StudentManager.DrinkingFountains[ID].PowerSwitch.PowerOutlet.SabotagedOutlet.SetActive(true);
						this.StudentManager.DrinkingFountains[ID].Puddle.SetActive(true);

						ID++;
					}

					this.Window.SetActive(false);
				}
				else if (Input.GetKeyDown(KeyCode.F8))
				{
					GameGlobals.CensorBlood = !GameGlobals.CensorBlood;

					this.WeaponManager.ChangeBloodTexture();
					this.Yandere.Bloodiness += 0;

					this.Window.SetActive(false);
				}
				else if (Input.GetKeyDown(KeyCode.F9))
				{
					this.Yandere.AttackManager.Censor = !this.Yandere.AttackManager.Censor;

					this.Window.SetActive(false);
				}
                else if (Input.GetKeyDown(KeyCode.F10))
                {
                    StudentManager.Students[21].Attempts = 101;
                    StudentManager.Students[22].Attempts = 101;
                    StudentManager.Students[23].Attempts = 101;
                    StudentManager.Students[24].Attempts = 101;
                    StudentManager.Students[25].Attempts = 101;

                    this.Window.SetActive(false);
                }
                else if (Input.GetKeyDown(KeyCode.F12))
				{
					//This doesn't actually work, sadly.
					//Astar.Scan();
				}
				else if (Input.GetKeyDown(KeyCode.Alpha1))
				{
					DateGlobals.Weekday = DayOfWeek.Monday;
					SceneManager.LoadScene(SceneNames.LoadingScene);
				}
				else if (Input.GetKeyDown(KeyCode.Alpha2))
				{
					DateGlobals.Weekday = DayOfWeek.Tuesday;
					SceneManager.LoadScene(SceneNames.LoadingScene);
				}
				else if (Input.GetKeyDown(KeyCode.Alpha3))
				{
					DateGlobals.Weekday = DayOfWeek.Wednesday;
					SceneManager.LoadScene(SceneNames.LoadingScene);
				}
				else if (Input.GetKeyDown(KeyCode.Alpha4))
				{
					DateGlobals.Weekday = DayOfWeek.Thursday;
					SceneManager.LoadScene(SceneNames.LoadingScene);
				}
				else if (Input.GetKeyDown(KeyCode.Alpha5))
				{
					DateGlobals.Weekday = DayOfWeek.Friday;
					SceneManager.LoadScene(SceneNames.LoadingScene);
				}
				else if (Input.GetKeyDown(KeyCode.Alpha6))
				{
					this.Yandere.transform.position = this.TeleportSpot[1].position;

					if (this.Yandere.Followers > 0)
					{
						this.Yandere.Follower.transform.position = this.Yandere.transform.position;
					}

                    Physics.SyncTransforms();
					this.Window.SetActive(false);
				}
				else if (Input.GetKeyDown(KeyCode.Alpha7))
				{
					this.Yandere.transform.position = this.TeleportSpot[2].position + new Vector3(.75f, 0, 0);

					if (this.Yandere.Followers > 0)
					{
						this.Yandere.Follower.transform.position = this.Yandere.transform.position;
					}

                    Physics.SyncTransforms();
                    this.Window.SetActive(false);
				}
				else if (Input.GetKeyDown(KeyCode.Alpha8))
				{
					this.Yandere.transform.position = this.TeleportSpot[3].position;

					if (this.Yandere.Followers > 0)
					{
						this.Yandere.Follower.transform.position = this.Yandere.transform.position;
					}

                    Physics.SyncTransforms();
                    this.Window.SetActive(false);
				}
				else if (Input.GetKeyDown(KeyCode.Alpha9))
				{
					this.Yandere.transform.position = this.TeleportSpot[4].position;

					if (this.Yandere.Followers > 0)
					{
						this.Yandere.Follower.transform.position = this.Yandere.transform.position;
					}

					//if (this.Clock.HourTime < 7.10f)
					//{
						this.Clock.PresentTime = 60.0f * 7.10f;

						//Kokona

						StudentScript student30 = this.StudentManager.Students[30];

						if (student30 != null)
						{
							if (student30.Phase < 2)
							{
								student30.ShoeRemoval.Start ();
								student30.ShoeRemoval.PutOnShoes ();
								student30.CanTalk = true;
								student30.Phase = 2;
								student30.CurrentDestination = student30.Destinations [2];
								student30.Pathfinding.target = student30.Destinations [2];
							}

							student30.transform.position = student30.Destinations [2].position;
						}

						//Riku

						StudentScript student28 = this.StudentManager.Students [28];

						if (student28 != null)
						{
							//if (student28.Phase < 2)
							//{
								student28.ShoeRemoval.Start ();
								student28.ShoeRemoval.PutOnShoes ();
								student28.Phase = 2;
								student28.CurrentDestination = student28.Destinations [2];
								student28.Pathfinding.target = student28.Destinations [2];
							//}

							student28.transform.position = student28.Destinations [2].position;
						}

						//Midori

						StudentScript student39 = this.StudentManager.Students [39];

						if (student39 != null)
						{
							//if (student39.Phase < 2)
							//{
								student39.ShoeRemoval.Start();
								student39.ShoeRemoval.PutOnShoes();
								student39.Phase = 2;

								ScheduleBlock newBlock2 = student39.ScheduleBlocks[2];
								newBlock2.action = "Stand";

								student39.GetDestinations();

								student39.CurrentDestination = this.MidoriSpot;
								student39.Pathfinding.target = this.MidoriSpot;
							//}

							student39.transform.position = this.MidoriSpot.position;
						}
					//}

					this.Window.SetActive(false);
                    Physics.SyncTransforms();
                }
				else if (Input.GetKeyDown(KeyCode.Alpha0))
				{
					this.Yandere.transform.position = this.TeleportSpot[11].position;

                    if (this.Yandere.Followers > 0)
                    {
                        this.Yandere.Follower.transform.position = this.Yandere.transform.position;
                    }

                    this.Window.SetActive(false);
                    Physics.SyncTransforms();
				}
				else if (Input.GetKeyDown(KeyCode.A))
				{
					if (SchoolAtmosphere.Type == SchoolAtmosphereType.High)
					{
						SchoolGlobals.SchoolAtmosphere = 0.50f;
					}
					else if (SchoolAtmosphere.Type == SchoolAtmosphereType.Medium)
					{
						SchoolGlobals.SchoolAtmosphere = 0.0f;
					}
					else
					{
						SchoolGlobals.SchoolAtmosphere = 1.0f;
					}

					SceneManager.LoadScene(SceneNames.LoadingScene);
				}
				else if (Input.GetKeyDown(KeyCode.C))
				{
					// [af] Converted while loop to for loop.
					for (this.ID = 1; this.ID < 11; this.ID++)
					{
						CollectibleGlobals.SetTapeCollected(this.ID, true);
					}

					this.Window.SetActive(false);
				}
				else if (Input.GetKeyDown(KeyCode.D))
				{
					this.ID = 0;

					while (this.ID < 5)
					{
						StudentScript student = this.StudentManager.Students[76 + ID];

						if (student != null)
						{
							if (student.Phase < 2)
							{
								student.ShoeRemoval.Start ();
								student.ShoeRemoval.PutOnShoes ();
								student.Phase = 2;
								student.CurrentDestination = student.Destinations[2];
								student.Pathfinding.target = student.Destinations[2];
							}

							student.transform.position = student.Destinations[2].position;
						}

						this.ID++;
					}

                    Physics.SyncTransforms();
                    this.Window.SetActive(false);

					#if UNITY_EDITOR

					PlayerGlobals.SetStudentFriend(76, true);
					PlayerGlobals.SetStudentFriend(77, true);
					PlayerGlobals.SetStudentFriend(78, true);
					PlayerGlobals.SetStudentFriend(79, true);
					PlayerGlobals.SetStudentFriend(80, true);

					StudentManager.Students[76].RespectEarned = true;
					StudentManager.Students[77].RespectEarned = true;
					StudentManager.Students[78].RespectEarned = true;
					StudentManager.Students[79].RespectEarned = true;
					StudentManager.Students[80].RespectEarned = true;

					PlayerGlobals.Reputation = -66.66666f;
					this.Reputation.Reputation = PlayerGlobals.Reputation;

					Yandere.Persona = YanderePersonaType.Tough;
					GameGlobals.BlondeHair = true;
					Yandere.Inventory.Money = 100;
					PlayerGlobals.Money = 100;

                    CounselorGlobals.DelinquentPunishments = 5;

                    //Yandere.Club = ClubType.Delinquent;
                    //Yandere.ClubAccessory();

#endif
                }
#if UNITY_EDITOR
                else if (Input.GetKeyDown(KeyCode.E))
				{
					Debug.Log("Press a number 1 ~ 5.");

					StudentGlobals.SetStudentExpelled(11, false);
					this.WaitingForNumber = true;
					this.Window.SetActive(false);
				}
#endif
				else if (Input.GetKeyDown(KeyCode.F))
				{
					/*
					this.FakeStudentSpawner.Spawn();
					*/

					this.GreenScreen.SetActive(true);

					this.Window.SetActive(false);
				}
				else if (Input.GetKeyDown(KeyCode.G))
				{
					StudentScript rooftopStudent = this.StudentManager.Students[this.RooftopStudent];

					if (this.Clock.HourTime < 15.0f)
					{
						PlayerGlobals.SetStudentFriend(this.RooftopStudent, true);

						this.Yandere.transform.position =
							this.RooftopSpot.position + new Vector3(1.0f, 0.0f, 0.0f);
						this.WeaponManager.Weapons[6].transform.position = this.Yandere.transform.position + new Vector3(0.0f, 0.0f, 1.915f);

						if (rooftopStudent != null)
						{
							this.StudentManager.OfferHelp.UpdateLocation();
							this.StudentManager.OfferHelp.enabled = true;

							if (!rooftopStudent.Indoors)
							{
								if (rooftopStudent.ShoeRemoval.Locker == null)
								{
									rooftopStudent.ShoeRemoval.Start();
								}

								rooftopStudent.ShoeRemoval.PutOnShoes();
							}

							rooftopStudent.CharacterAnimation.Play(rooftopStudent.IdleAnim);

							rooftopStudent.transform.position = this.RooftopSpot.position;
							rooftopStudent.transform.rotation = this.RooftopSpot.rotation;

							rooftopStudent.Prompt.Label[0].text = "     " + "Push";

							rooftopStudent.CurrentDestination = this.RooftopSpot;
							rooftopStudent.Pathfinding.target = this.RooftopSpot;

							rooftopStudent.Pathfinding.canSearch = false;
							rooftopStudent.Pathfinding.canMove = false;

							rooftopStudent.SpeechLines.Stop();
							rooftopStudent.Pushable = true;
							rooftopStudent.Routine = false;
							rooftopStudent.Meeting = true;
							rooftopStudent.MeetTime = 0.0f;
						}

						if (this.Clock.HourTime < 7.10f)
						{
							this.Clock.PresentTime = 60.0f * 7.10f;
						}
					}
					else
					{
						this.Clock.PresentTime = 60.0f * 16.0f;

						rooftopStudent.transform.position = this.Lockers.position;
					}

                    Physics.SyncTransforms();
                    this.Window.SetActive(false);
				}
				else if (Input.GetKeyDown(KeyCode.K))
				{
					SchoolGlobals.KidnapVictim = 25;
					StudentGlobals.StudentSlave = 25;
					SceneManager.LoadScene(SceneNames.LoadingScene);
				}
				else if (Input.GetKeyDown(KeyCode.L))
				{
					SchemeGlobals.SetSchemeStage(1, 2);
					EventGlobals.Event1 = true;
					this.Window.SetActive(false);
				}
				else if (Input.GetKeyDown(KeyCode.M))
				{
					PlayerGlobals.Money = 100;
					this.Yandere.Inventory.Money = 100;
					this.Yandere.Inventory.UpdateMoney();
					this.Window.SetActive(false);

//					StudentGlobals.SetStudentBroken(81, true);
//					SceneManager.LoadScene(SceneNames.LoadingScene);
				}
				else if (Input.GetKeyDown(KeyCode.O))
				{
					this.StudentManager.LockDownOccultClub();

					this.Yandere.Inventory.RivalPhone = true;
					this.Window.SetActive(false);
				}
				else if (Input.GetKeyDown(KeyCode.P))
				{
					for (this.ID = 2; this.ID < 93; this.ID++)
					{
						StudentScript student = this.StudentManager.Students[this.ID];

						if (student != null)
						{
							student.Patience = 999;
							student.Pestered = -999;
							student.Ignoring = false;
						}
					}

					#if UNITY_EDITOR
					this.DebugPoisons.SetActive(true);
					#endif

					this.Yandere.Inventory.PantyShots += 20;
					PlayerGlobals.PantyShots += 20;
					this.Window.SetActive(false);
				}
				else if (Input.GetKeyDown(KeyCode.Q))
				{
					this.Censor();

					this.Window.SetActive(false);
				}
				else if (Input.GetKeyDown(KeyCode.R))
				{
					if (PlayerGlobals.Reputation == -100)
					{
						PlayerGlobals.Reputation = -66.66666f;
					}
					else if (PlayerGlobals.Reputation == -66.66666f)
					{
						PlayerGlobals.Reputation = 0;
					}
					else if (PlayerGlobals.Reputation == 0)
					{
						PlayerGlobals.Reputation = 66.66666f;
					}
					else if (PlayerGlobals.Reputation == 66.66666f)
					{
						PlayerGlobals.Reputation = 100;
					}
					else if (PlayerGlobals.Reputation == 100)
					{
						PlayerGlobals.Reputation = -100;
					}

					this.Reputation.Reputation = PlayerGlobals.Reputation;

					#if !UNITY_EDITOR
					this.Window.SetActive(false);
					#endif
				}
				else if (Input.GetKeyDown(KeyCode.S))
				{
                    Yandere.Class.PhysicalGrade = 5;
                    Yandere.Class.Seduction = 5;

                    //ClassGlobals.PhysicalGrade = 5;
					//PlayerGlobals.Seduction = 5;

					this.StudentManager.Police.UpdateCorpses();

					for (this.ID = 1; this.ID < 101; this.ID++)
					{
						StudentGlobals.SetStudentPhotographed(this.ID, true);
					}

					int ID = 0;

					/*
					foreach (WeaponScript weapon in WeaponManager.Weapons)
					{
						weapon.gameObject.SetActive(true);
					}
					*/

					this.Window.SetActive(false);
				}
				else if (Input.GetKeyDown(KeyCode.T))
				{
					// [af] Replaced if/else statement with boolean expression.
					this.Zoom.OverShoulder = !this.Zoom.OverShoulder;

					this.Window.SetActive(false);
				}
				else if (Input.GetKeyDown(KeyCode.U))
				{
					PlayerGlobals.SetStudentFriend(28, true);
					PlayerGlobals.SetStudentFriend(30, true);

					// [af] Converted while loop to for loop.
					for (this.ID = 1; this.ID < 26; this.ID++)
					{
						ConversationGlobals.SetTopicLearnedByStudent(this.ID, 30, true);
						ConversationGlobals.SetTopicDiscovered(this.ID, true);
					}

					this.Window.SetActive(false);
				}
#if UNITY_EDITOR
				else if (Input.GetKeyDown(KeyCode.Y))
				{
					// [af] Converted while loop to for loop.
					for (this.ID = 1; this.ID < 11; this.ID++)
					{
						Instantiate(this.SacrificialArm,
							new Vector3(12.0f, 2.0f, 26.0f), Quaternion.identity);
					}

					this.Window.SetActive(false);
				}
#endif
				else if (Input.GetKeyDown(KeyCode.Z))
				{
                    this.Yandere.Police.Invalid = true;

					if (Input.GetKey(KeyCode.LeftShift))
					{
						for (this.ID = 2; this.ID < 93; this.ID++)
						{
							StudentScript student = this.StudentManager.Students[this.ID];

							if (student != null)
							{
								//if (student.Club != ClubType.Council)
								//{
									//StudentGlobals.SetStudentMissing(this.ID, true);
								//}
							}
						}
					}
					else
					{
						for (this.ID = 2; this.ID < 101; this.ID++)
						{
							StudentScript student = this.StudentManager.Students[this.ID];

							if (student != null)
							{
								//if (student.Club != ClubType.Council)
								//{
									student.SpawnAlarmDisc();
									student.BecomeRagdoll();
									student.DeathType = DeathType.EasterEgg;
									//StudentGlobals.SetStudentDead(this.ID, true);
								//}
							}
						}
					}

					this.Window.SetActive(false);
				}
				//Complete Gema's Task.
				else if (Input.GetKeyDown(KeyCode.X))
				{
					TaskGlobals.SetTaskStatus(36, 3);
					SchoolGlobals.ReactedToGameLeader = false;
					SceneManager.LoadScene(SceneNames.LoadingScene);
				}
				else if (Input.GetKeyDown(KeyCode.Backspace))
				{
					Time.timeScale = 1.0f;
					this.Clock.PresentTime = 1079.0f;
					this.Clock.HourTime = this.Clock.PresentTime / 60.0f;
					this.Window.SetActive(false);
				}
				else if (Input.GetKeyDown(KeyCode.BackQuote))
				{
					Globals.DeleteAll();
					SceneManager.LoadScene(SceneNames.LoadingScene);
				}
				else if (Input.GetKeyDown(KeyCode.Space))
				{
					this.Yandere.transform.position = this.TeleportSpot[5].position;

					if (this.Yandere.Follower != null)
					{
						this.Yandere.Follower.transform.position = this.Yandere.transform.position;
					}

					int MartialArtsID = 46;

					while (MartialArtsID < 51)
					{
						if (this.StudentManager.Students[MartialArtsID] != null)
						{
							this.StudentManager.Students[MartialArtsID].transform.position = this.TeleportSpot[5].position;

							if (!this.StudentManager.Students[MartialArtsID].Indoors)
							{
								if (this.StudentManager.Students[MartialArtsID].ShoeRemoval.Locker == null)
								{
									this.StudentManager.Students[MartialArtsID].ShoeRemoval.Start();
								}

								this.StudentManager.Students[MartialArtsID].ShoeRemoval.PutOnShoes();
							}
						}

						MartialArtsID++;
					}

					this.Clock.PresentTime = 1015.0f;
					this.Clock.HourTime = this.Clock.PresentTime / 60.0f;
					this.Window.SetActive(false);

					OsanaEvent1.enabled = false;
					OsanaEvent2.enabled = false;
					OsanaEvent3.enabled = false;

                    Physics.SyncTransforms();
                }
				else if (Input.GetKeyDown(KeyCode.LeftAlt))
				{
					this.Turtle.SpawnWeapons();

					// [af] Commented in JS code.
					//Knife.transform.position = TeleportSpot[6].position + Vector3(.5, 0, 0);
					//CircularSaw.transform.position = TeleportSpot[6].position + Vector3(1, 0, 0);

					this.Yandere.transform.position = this.TeleportSpot[6].position;

					if (this.Yandere.Follower != null)
					{
						this.Yandere.Follower.transform.position = this.Yandere.transform.position;
					}

					// [af] Commented in JS code.
					/*
					if (this.StudentManager.Students[26] != null)
					{
						this.StudentManager.Students[26].transform.position = this.TeleportSpot[6].position;
					}
					*/

					this.Clock.PresentTime = 425.0f;
					this.Clock.HourTime = this.Clock.PresentTime / 60.0f;

                    Physics.SyncTransforms();
                    this.Window.SetActive(false);
				}
				else if (Input.GetKeyDown(KeyCode.LeftControl))
				{
					this.Yandere.transform.position = this.TeleportSpot[7].position;

					if (this.Yandere.Follower != null)
					{
						this.Yandere.Follower.transform.position = this.Yandere.transform.position;
					}

					//if (this.StudentManager.Students[31] != null)
					//{
					//	this.StudentManager.Students[31].transform.position = this.TeleportSpot[30].position;
					//}

					//this.Clock.PresentTime = 1015.0f;
					//this.Clock.HourTime = this.Clock.PresentTime / 60.0f;

                    Physics.SyncTransforms();
                    this.Window.SetActive(false);
				}
				else if (Input.GetKeyDown(KeyCode.RightControl))
				{
					this.Yandere.transform.position = this.TeleportSpot[8].position;

					if (this.Yandere.Follower != null)
					{
						this.Yandere.Follower.transform.position = this.Yandere.transform.position;
					}

                    Physics.SyncTransforms();
                    this.Window.SetActive(false);
				}
				else if (Input.GetKeyDown(KeyCode.Equals))
				{
					this.Clock.PresentTime += 10.0f;
					this.Window.SetActive(false);
				}
				else if (Input.GetKeyDown(KeyCode.Return))
				{
					this.Yandere.transform.eulerAngles = this.TeleportSpot[10].eulerAngles;
					this.Yandere.transform.position = this.TeleportSpot[10].position;

					if (this.Yandere.Follower != null)
					{
						this.Yandere.Follower.transform.position = this.Yandere.transform.position;
					}

					//Senpai
					this.StudentManager.Students[1].ShoeRemoval.Start();
					this.StudentManager.Students[1].ShoeRemoval.PutOnShoes();
					this.StudentManager.Students[1].transform.position = new Vector3(0.0f, 12.10f, -25.0f);
					this.StudentManager.Students[1].Alarmed = true;

					//Osana
					this.StudentManager.Students[11].Lethal = true;
					this.StudentManager.Students[11].ShoeRemoval.Start();
					this.StudentManager.Students[11].ShoeRemoval.PutOnShoes();
					this.StudentManager.Students[11].transform.position = new Vector3(0.0f, 12.10f, -25.0f);

					// [af] Commented in JS code.
					//Bento.Poison = 1;

					this.Clock.PresentTime = 780.0f;
					this.Clock.HourTime = this.Clock.PresentTime / 60.0f;

                    Physics.SyncTransforms();
                    this.Window.SetActive(false);
				}
				else if (Input.GetKeyDown(KeyCode.B))
				{
					this.Yandere.Inventory.Headset = true;

					this.StudentManager.LoveManager.SuitorProgress = 1;
					DatingGlobals.SuitorProgress = 1;

					PlayerGlobals.SetStudentFriend(6, true);
					PlayerGlobals.SetStudentFriend(11, true);

					int ID = 0;

					while (ID < 11)
					{
						DatingGlobals.SetComplimentGiven(ID, false);
						ID++;
					}

					for (this.ID = 1; this.ID < 26; this.ID++)
					{
						ConversationGlobals.SetTopicLearnedByStudent(this.ID, 11, true);
						ConversationGlobals.SetTopicDiscovered(this.ID, true);
					}
						
					//Osana
					StudentScript student11 = this.StudentManager.Students[11];

					if (student11 != null)
					{
						student11.ShoeRemoval.Start();
						student11.ShoeRemoval.PutOnShoes();
						student11.CanTalk = true;
						student11.Phase = 2;
						student11.Pestered = 0;
						student11.Patience = 999;
						student11.Ignoring = false;
						student11.CurrentDestination = student11.Destinations[2];
						student11.Pathfinding.target = student11.Destinations[2];
						student11.transform.position = student11.Destinations[2].position;
					}

					//Suitor
					StudentScript student6 = this.StudentManager.Students[6];

					if (student6 != null)
					{
						student6.ShoeRemoval.Start();
						student6.ShoeRemoval.PutOnShoes();
						student6.Phase = 2;
						student6.Pestered = 0;
						student6.Patience = 999;
						student6.Ignoring = false;
						student6.CurrentDestination = student6.Destinations[2];
						student6.Pathfinding.target = student6.Destinations[2];
						student6.transform.position = student6.Destinations[2].position;
					}

					//Raibaru
					StudentScript student10 = this.StudentManager.Students[10];

					if (student6 != null)
					{
						student6.transform.position = student11.transform.position;
					}

					CollectibleGlobals.SetGiftPurchased(6, true);
					CollectibleGlobals.SetGiftPurchased(7, true);
					CollectibleGlobals.SetGiftPurchased(8, true);
					CollectibleGlobals.SetGiftPurchased(9, true);

					Physics.SyncTransforms();

					this.Window.SetActive(false);
				}
				else if (Input.GetKeyDown(KeyCode.Pause))
				{
					this.Clock.StopTime = !this.Clock.StopTime;
					this.Window.SetActive(false);
				}
				else if (Input.GetKeyDown(KeyCode.W))
				{
					this.StudentManager.ToggleBookBags();

					this.Window.SetActive(false);
				}
				//Send Horuda to kill Riku
				else if (Input.GetKeyDown(KeyCode.H))
				{
                    StudentGlobals.FragileSlave = 5;
                    StudentGlobals.FragileTarget = 31;

					SceneManager.LoadScene(SceneNames.LoadingScene);
				}
				//Frame two people for murder
				else if (Input.GetKeyDown(KeyCode.I))
				{
					StudentManager.Students[3].BecomeRagdoll();

					WeaponManager.Weapons[1].Blood.enabled = true;
					WeaponManager.Weapons[1].FingerprintID = 2;
					WeaponManager.Weapons[1].Victims[3] = true;

					StudentManager.Students[5].BecomeRagdoll();

					WeaponManager.Weapons[2].Blood.enabled = true;
					WeaponManager.Weapons[2].FingerprintID = 4;
					WeaponManager.Weapons[2].Victims[5] = true;
				}
				//Test Empty Husk Mode
				else if (Input.GetKeyDown(KeyCode.J))
				{
					#if UNITY_EDITOR
					GameGlobals.EmptyDemon = true;
					SceneManager.LoadScene(SceneNames.LoadingScene);
					#endif
				}
				else if (Input.GetKeyDown(KeyCode.V))
				{
					this.StudentManager.LoveManager.ConfessToSuitor = true;
					this.StudentManager.DatingMinigame.Affection = 100.0f;
					DateGlobals.Weekday = System.DayOfWeek.Friday;

					this.Window.SetActive(false);
				}
				else if (Input.GetKeyDown(KeyCode.N))
				{
					this.ElectrocutionKit[0].transform.position = this.Yandere.transform.position;
					this.ElectrocutionKit[1].transform.position = this.Yandere.transform.position;
					this.ElectrocutionKit[2].transform.position = this.Yandere.transform.position;
					this.ElectrocutionKit[3].transform.position = this.Yandere.transform.position;
					this.ElectrocutionKit[3].SetActive(true);
				}

#if UNITY_EDITOR
                if (Input.GetKey(KeyCode.R))
				{
					this.Timer += Time.deltaTime;

					if (this.Timer > 1)
					{
						StudentGlobals.SetStudentReputation(10, -40);

						SceneManager.LoadScene(SceneNames.LoadingScene);
					}
				}

				if (Input.GetKeyUp(KeyCode.R))
				{
					this.Timer = 0;
				}
					
				#endif

				if (Input.GetKeyDown(KeyCode.Tab))
				{
					DatingGlobals.SuitorProgress = 2;
					SceneManager.LoadScene(SceneNames.LoadingScene);
				}

				if (Input.GetKeyDown(KeyCode.CapsLock))
				{
					this.StudentManager.LoveManager.ConfessToSuitor = true;
				}
			}
			else
			{
				if (Input.GetKey(KeyCode.BackQuote))
				{
					this.Timer += Time.deltaTime;

					if (this.Timer > 1)
					{
						for (this.ID = 0; this.ID < this.StudentManager.NPCsTotal; this.ID++)
						{
							if (StudentGlobals.GetStudentDying(this.ID))
							{
								StudentGlobals.SetStudentDying(this.ID, false);
							}
						}

						SceneManager.LoadScene(SceneNames.LoadingScene);
					}
				}

				if (Input.GetKeyUp(KeyCode.BackQuote))
				{
					this.Timer = 0;
				}
			}

			if (this.TryNextFrame)
			{
				this.UpdateCensor();
			}
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.Backslash))
			{
				this.MissionModeWindow.SetActive(!this.MissionModeWindow.activeInHierarchy);

				this.DebugInputs++;

				#if UNITY_EDITOR
				if (this.DebugInputs == 10)
				{
					this.MissionMode = false;
					this.NoDebug = false;
				}
				#endif
			}

			if (this.MissionModeWindow.activeInHierarchy)
			{
				if (Input.GetKeyDown(KeyCode.Alpha1))
				{
					this.Censor();
				}

				if (Input.GetKeyDown(KeyCode.Alpha2))
				{
					GameGlobals.CensorBlood = !GameGlobals.CensorBlood;

					this.WeaponManager.ChangeBloodTexture();
					this.Yandere.Bloodiness += 0;
				}

				if (Input.GetKeyDown(KeyCode.Alpha3))
				{
					this.Yandere.AttackManager.Censor = !this.Yandere.AttackManager.Censor;
				}
			}
		}

		// [af] Commented in JS code.
		/*if (Yandere.transform.position.x > 13 && Yandere.transform.position.z < -75)
		{
			if (Input.GetButtonDown(InputNames.Xbox_A))
			{
				ID = 1;

				while (ID < 11)
				{
					Instantiate(SacrificialArm, Vector3(12, 2, 26), Quaternion.identity);
					ID++;
				}
			}
		}*/

		if (this.WaitingForNumber)
		{
			if (Input.GetKey("1"))
			{
				Debug.Log("Going to class should trigger panty shot lecture.");

				SchemeGlobals.SetSchemeStage(1, 100);
				StudentGlobals.ExpelProgress = 0;

				this.Counselor.CutsceneManager.Scheme = 1;
				this.Counselor.LectureID = 1;

				this.WaitingForNumber = false;
			}
			else if (Input.GetKey("2"))
			{
				Debug.Log("Going to class should trigger theft lecture.");

				SchemeGlobals.SetSchemeStage(2, 100);
				StudentGlobals.ExpelProgress = 1;

				this.Counselor.CutsceneManager.Scheme = 2;
				this.Counselor.LectureID = 2;

				this.WaitingForNumber = false;
			}
			else if (Input.GetKey("3"))
			{
				Debug.Log("Going to class should trigger contraband lecture.");

				SchemeGlobals.SetSchemeStage(3, 100);
				StudentGlobals.ExpelProgress = 2;

				this.Counselor.CutsceneManager.Scheme = 3;
				this.Counselor.LectureID = 3;

				this.WaitingForNumber = false;
			}
			else if (Input.GetKey("4"))
			{
				Debug.Log("Going to class should trigger Vandalism lecture.");

				SchemeGlobals.SetSchemeStage(4, 100);
				StudentGlobals.ExpelProgress = 3;

				this.Counselor.CutsceneManager.Scheme = 4;
				this.Counselor.LectureID = 4;

				this.WaitingForNumber = false;
			}
			else if (Input.GetKey("5"))
			{
				Debug.Log("Going to class at lunchtime should get Osana expelled!");

				SchemeGlobals.SetSchemeStage(5, 100);
				StudentGlobals.ExpelProgress = 4;

				this.Counselor.CutsceneManager.Scheme = 5;
				this.Counselor.LectureID = 5;

				this.WaitingForNumber = false;
			}
		}
	}

	public Texture PantyCensorTexture;

	public void Censor()
	{
		if (!this.StudentManager.Censor)
		{
			Debug.Log("We're turning the censor ON.");

			if (this.Yandere.Schoolwear == 1)
			{
				if (!this.Yandere.Sans && !this.Yandere.SithLord && !this.Yandere.BanchoActive)
				{
					if (!this.Yandere.FlameDemonic && !this.Yandere.TornadoHair.activeInHierarchy)
					{
						this.Yandere.MyRenderer.materials[0].SetFloat("_BlendAmount1", 1.0f);
						this.Yandere.MyRenderer.materials[1].SetFloat("_BlendAmount1", 1.0f);
						this.Yandere.MyRenderer.materials[0].SetFloat("_BlendAmount", 1.0f);
						this.Yandere.MyRenderer.materials[1].SetFloat("_BlendAmount", 1.0f);
						this.Yandere.PantyAttacher.newRenderer.enabled = false;
					}
					else
					{
						Debug.Log("This block of code activated a shadow.");

						this.Yandere.MyRenderer.materials[2].SetTexture("_OverlayTex", this.PantyCensorTexture);

						this.Yandere.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
						this.Yandere.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);
						this.Yandere.MyRenderer.materials[2].SetFloat("_BlendAmount", 1.0f);
					}
				}
				else
				{
					this.Yandere.PantyAttacher.newRenderer.enabled = false;
				}
			}

			if (this.Yandere.MiyukiCostume.activeInHierarchy || this.Yandere.Rain.activeInHierarchy)
			{
				this.Yandere.PantyAttacher.newRenderer.enabled = false;

				this.Yandere.MyRenderer.materials[1].SetTexture("_OverlayTex", this.PantyCensorTexture);
				this.Yandere.MyRenderer.materials[2].SetTexture("_OverlayTex", this.PantyCensorTexture);

				this.Yandere.MyRenderer.materials[0].SetFloat("_BlendAmount1", 0.0f);
				this.Yandere.MyRenderer.materials[1].SetFloat("_BlendAmount1", 1.0f);
				this.Yandere.MyRenderer.materials[2].SetFloat("_BlendAmount1", 1.0f);
			}

			if (this.Yandere.NierCostume.activeInHierarchy ||
				this.Yandere.MyRenderer.sharedMesh == this.Yandere.NudeMesh ||
				this.Yandere.MyRenderer.sharedMesh == this.Yandere.SchoolSwimsuit)
			{
				this.EasterEggCheck();
			}

			this.StudentManager.Censor = true;
			this.StudentManager.CensorStudents();
		}
		//If we are turning off the censor...
		else
		{
			Debug.Log("We're turning the censor OFF.");

			this.Yandere.MyRenderer.materials[1].SetFloat("_BlendAmount1", 0.0f);
			this.Yandere.MyRenderer.materials[1].SetFloat("_BlendAmount", 1.0f);
			this.Yandere.MyRenderer.materials[2].SetFloat("_BlendAmount", 0.0f);

			//If Yandere-chan is not nude...
			if (this.Yandere.MyRenderer.sharedMesh != this.Yandere.NudeMesh && this.Yandere.MyRenderer.sharedMesh != this.Yandere.SchoolSwimsuit)
			{
				//Turn on shadows and panties.
				this.Yandere.MyRenderer.materials[0].SetFloat("_BlendAmount1", 1.0f);
				this.Yandere.MyRenderer.materials[0].SetFloat("_BlendAmount", 1.0f);
				this.Yandere.PantyAttacher.newRenderer.enabled = true;

				this.EasterEggCheck();
			}
			//If Yandere-chan IS nude...
			else
			{
				//Turn off shadows and panties.
				this.Yandere.MyRenderer.materials[0].SetFloat("_BlendAmount1", 0.0f);
				this.Yandere.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
				this.Yandere.PantyAttacher.newRenderer.enabled = false;

				this.EasterEggCheck();
			}

			if (this.Yandere.MiyukiCostume.activeInHierarchy)
			{
				this.Yandere.PantyAttacher.newRenderer.enabled = false;
			}

			this.StudentManager.Censor = false;
			this.StudentManager.CensorStudents();
		}
	}

	public void EasterEggCheck()
	{
		Debug.Log("Checking for easter eggs.");

		if (this.Yandere.BanchoActive || this.Yandere.Sans || this.Yandere.Raincoat.activeInHierarchy ||
			this.Yandere.KLKSword.activeInHierarchy || this.Yandere.Gazing || this.Yandere.Ninja || this.Yandere.ClubAttire ||
			this.Yandere.LifeNotebook.activeInHierarchy || this.Yandere.FalconHelmet.activeInHierarchy||
			this.Yandere.MyRenderer.sharedMesh == this.Yandere.NudeMesh || this.Yandere.MyRenderer.sharedMesh == this.Yandere.SchoolSwimsuit) // || this.Yandere.NierCostume.activeInHierarchy)
		{
			Debug.Log("A pants-wearing easter egg is active, so we're going to disable all shadows and panties.");

			this.Yandere.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
			this.Yandere.MyRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);
			this.Yandere.MyRenderer.materials[2].SetFloat("_BlendAmount", 0.0f);

			this.Yandere.MyRenderer.materials[0].SetFloat("_BlendAmount1", 0.0f);
			this.Yandere.MyRenderer.materials[1].SetFloat("_BlendAmount1", 0.0f);
			this.Yandere.MyRenderer.materials[2].SetFloat("_BlendAmount1", 0.0f);

			this.Yandere.PantyAttacher.newRenderer.enabled = false;
		}

		//For flame demon and tornado specifically...
		if (this.Yandere.FlameDemonic || this.Yandere.TornadoHair.activeInHierarchy)
		{
			Debug.Log("This other block of code activated a shadow.");

			this.Yandere.MyRenderer.materials[1].SetTexture("_OverlayTex", this.PantyCensorTexture);

			this.Yandere.MyRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
			this.Yandere.MyRenderer.materials[1].SetFloat("_BlendAmount", 1.0f);
			this.Yandere.MyRenderer.materials[2].SetFloat("_BlendAmount", 0.0f);

			this.Yandere.MyRenderer.materials[0].SetFloat("_BlendAmount1", 0.0f);
			this.Yandere.MyRenderer.materials[1].SetFloat("_BlendAmount1", 0.0f);
			this.Yandere.MyRenderer.materials[2].SetFloat("_BlendAmount1", 0.0f);
		}

		if (this.Yandere.NierCostume.activeInHierarchy)
		{
			Debug.Log("Nier costume special case.");

			this.Yandere.PantyAttacher.newRenderer.enabled = false;

			SkinnedMeshRenderer NierRenderer = this.Yandere.NierCostume.GetComponent<RiggedAccessoryAttacher>().newRenderer;

			if (NierRenderer == null)
			{
				this.TryNextFrame = true;
			}
			else
			{
				this.TryNextFrame = false;

				if (!this.StudentManager.Censor)
				{
					NierRenderer.materials[0].SetFloat("_BlendAmount", 1.0f);
					NierRenderer.materials[1].SetFloat("_BlendAmount", 1.0f);
					NierRenderer.materials[2].SetFloat("_BlendAmount", 1.0f);
					NierRenderer.materials[3].SetFloat("_BlendAmount", 1.0f);
				}
				else
				{
					NierRenderer.materials[0].SetFloat("_BlendAmount", 0.0f);
					NierRenderer.materials[1].SetFloat("_BlendAmount", 0.0f);
					NierRenderer.materials[2].SetFloat("_BlendAmount", 0.0f);
					NierRenderer.materials[3].SetFloat("_BlendAmount", 0.0f);
				}
			}
		}
	}

	public void UpdateCensor()
	{
		this.Censor();
		this.Censor();
	}


	int DebugInt;
	public GameObject Mop;

	public void DebugTest()
	{
		if (DebugInt == 0)
		{
			StudentScript student39 = this.StudentManager.Students[39];

			student39.ShoeRemoval.Start();
			student39.ShoeRemoval.PutOnShoes();
			student39.Phase = 2;

			ScheduleBlock newBlock2 = student39.ScheduleBlocks[2];
			newBlock2.action = "Stand";

			student39.GetDestinations();

			student39.CurrentDestination = this.MidoriSpot;
			student39.Pathfinding.target = this.MidoriSpot;

			student39.transform.position = Yandere.transform.position;

			Physics.SyncTransforms();
		}
		else if (DebugInt == 1)
		{
			Knife.transform.position = Yandere.transform.position + new Vector3(-1, 1, 0);
			Knife.GetComponent<Rigidbody>().isKinematic = false;
			Knife.GetComponent<Rigidbody>().useGravity = true;
		}
		else if (DebugInt == 2)
		{
			Mop.transform.position = Yandere.transform.position + new Vector3(1, 1, 0);
			Mop.GetComponent<Rigidbody>().isKinematic = false;
			Mop.GetComponent<Rigidbody>().useGravity = true;
		}

		DebugInt++;
	}
}