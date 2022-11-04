using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PoliceScript : MonoBehaviour
{
	public LowRepGameOverScript LowRepGameOver;
	public StudentManagerScript StudentManager;
	public ClubManagerScript ClubManager;
	public HeartbrokenScript Heartbroken;
	public LoveManagerScript LoveManager;
	public PauseScreenScript PauseScreen;
	public ReputationScript Reputation;
	public TranqCaseScript TranqCase;
	public EndOfDayScript EndOfDay;
	public JukeboxScript Jukebox;
	public YandereScript Yandere;
	public ClockScript Clock;
	public JsonScript JSON;
	public UIPanel Panel;

	public GameObject HeartbeatCamera;
	public GameObject DetectionCamera;
	public GameObject SuicideStudent;
	public GameObject UICamera;
	public GameObject Icons;
	public GameObject FPS;

	public Transform BloodParent;
	public Transform LimbParent;

	public RagdollScript[] CorpseList;

	public UILabel[] ResultsLabels;
	public UILabel ContinueLabel;
	public UILabel TimeLabel;

	public UISprite ContinueButton;
	public UISprite Darkness;

	public UISprite BloodIcon;
	public UISprite UniformIcon;
	public UISprite WeaponIcon;
	public UISprite CorpseIcon;
	public UISprite PartsIcon;
	public UISprite SanityIcon;

	public string ElectrocutedStudentName = string.Empty;
	public string DrownedStudentName = string.Empty;

	public bool BloodDisposed = false;
	public bool UniformDisposed = false;
	public bool WeaponDisposed = false;
	public bool CorpseDisposed = false;
	public bool PartsDisposed = false;
	public bool SanityRestored = false;

	public bool MurderSuicideScene = false;
    public bool ElectroScene = false;
	public bool SuicideScene = false;
	public bool PoisonScene = false;
	public bool MurderScene = false;

	public bool BeginConfession = false;
    public bool GenocideEnding = false;
    public bool TeacherReport = false;
	public bool ClubActivity = false;
	public bool CouncilDeath = false;
	public bool MaskReported = false;
	public bool FadeResults = false;
	public bool ShowResults = false;
    public bool GameOver = false;
	public bool DayOver = false;
	public bool Delayed = false;
	public bool FadeOut = false;
    public bool Invalid = false;
    public bool Suicide = false;
	public bool Called = false;
	public bool LowRep = false;
	public bool Show = false;

	public int IncineratedWeapons = 0;
	public int SuicideVictims = 0;
	public int BloodyClothing = 0;
	public int BloodyWeapons = 0;
	public int HiddenCorpses = 0;
	public int MurderWeapons = 0;
	public int PhotoEvidence = 0;
	public int DrownVictims = 0;
	public int BodyParts = 0;
	public int Witnesses = 0;
	public int Corpses = 0;
	public int Deaths = 0;

	public float ResultsTimer = 0.0f;
	public float Timer = 0.0f;

	public int Minutes;
	public int Seconds;

	void Start()
	{
		this.PartsIcon.gameObject.SetActive(false);

		if (SchoolGlobals.SchoolAtmosphere > 0.50f)
		{
			this.Darkness.color = new Color(
				this.Darkness.color.r,
				this.Darkness.color.g,
				this.Darkness.color.b,
				0.0f);

			this.Darkness.enabled = false;
		}

		this.transform.localPosition = new Vector3(
			-260.0f,
			this.transform.localPosition.y,
			this.transform.localPosition.z);

		// [af] Converted while loop to foreach loop.
		foreach (UILabel label in this.ResultsLabels)
		{
			label.color = new Color(
				label.color.r,
				label.color.g,
				label.color.b,
				0.0f);
		}

		this.ContinueLabel.color = new Color(
			this.ContinueLabel.color.r,
			this.ContinueLabel.color.g,
			this.ContinueLabel.color.b,
			0.0f);

		this.ContinueButton.color = new Color(
			this.ContinueButton.color.r,
			this.ContinueButton.color.g,
			this.ContinueButton.color.b,
			0.0f);

		this.Icons.SetActive(false);
	}

	void Update()
	{
		if (this.Show)
		{
			this.StudentManager.TutorialWindow.ShowPoliceMessage = true;

			if (this.PoisonScene)
			{
				//this.Panel.alpha = 0.0f;
			}

			if (!this.Icons.activeInHierarchy)
			{
				this.Icons.SetActive(true);
			}

			this.transform.localPosition = new Vector3(
				Mathf.Lerp(this.transform.localPosition.x, 0.0f, Time.deltaTime * 10.0f),
				this.transform.localPosition.y,
				this.transform.localPosition.z);

			if (this.BloodParent.childCount == 0)
			{
				if (!this.BloodDisposed)
				{
					this.BloodIcon.spriteName = "Yes";
					this.BloodDisposed = true;
				}
			}
			else
			{
				if (this.BloodDisposed)
				{
					this.BloodIcon.spriteName = "No";
					this.BloodDisposed = false;
				}
			}

			if (this.BloodyClothing == 0)
			{
				if (!this.UniformDisposed)
				{
					this.UniformIcon.spriteName = "Yes";
					this.UniformDisposed = true;
				}
			}
			else
			{
				if (this.UniformDisposed)
				{
					this.UniformIcon.spriteName = "No";
					this.UniformDisposed = false;
				}
			}

			if (this.MurderWeapons == 0 || this.IncineratedWeapons == this.MurderWeapons)
			{
				if (!this.WeaponDisposed)
				{
					this.WeaponIcon.spriteName = "Yes";
					this.WeaponDisposed = true;
				}
			}
			else
			{
				if (this.WeaponDisposed)
				{
					this.WeaponIcon.spriteName = "No";
					this.WeaponDisposed = false;
				}
			}

			if (this.Corpses == 0)
			{
				if (!this.CorpseDisposed)
				{
					this.CorpseIcon.spriteName = "Yes";
					this.CorpseDisposed = true;
				}
			}
			else
			{
				if (this.CorpseDisposed)
				{
					this.CorpseIcon.spriteName = "No";
					this.CorpseDisposed = false;
				}
			}

			if (this.BodyParts == 0)
			{
				if (!this.PartsDisposed)
				{
					this.PartsIcon.spriteName = "Yes";
					this.PartsDisposed = true;
				}
			}
			else
			{
				if (this.PartsDisposed)
				{
					this.PartsIcon.spriteName = "No";
					this.PartsDisposed = false;
				}
			}

			if (this.Yandere.Sanity == 100)
			{
				if (!this.SanityRestored)
				{
					this.SanityIcon.spriteName = "Yes";
					this.SanityRestored = true;
				}
			}
			else
			{
				if (this.SanityRestored)
				{
					this.SanityIcon.spriteName = "No";
					this.SanityRestored = false;
				}
			}

			if (!this.Clock.StopTime)
			{
				this.Timer = Mathf.MoveTowards(this.Timer, 0, Time.deltaTime);
			}

			if (this.Timer <= 0.0f)
			{
				this.Timer = 0.0f;

				if (!this.Yandere.Attacking && !this.Yandere.Struggling && !this.Yandere.Egg)
				{
					if (!this.FadeOut)
					{
						this.BeginFadingOut();
					}
				}
			}

			int RoundedTime = Mathf.CeilToInt(this.Timer);

			this.Minutes = RoundedTime / 60;
			this.Seconds = RoundedTime % 60;

			this.TimeLabel.text = string.Format("{0:00}:{1:00}", this.Minutes, this.Seconds);
		}
        else
        {
            if (Deaths > 84 &&
                !this.Invalid &&
                !this.Yandere.Egg &&
                this.Clock.Weekday == 1 &&
                this.StudentManager.Students[1].gameObject.activeInHierarchy &&
                this.StudentManager.Students[1].Fleeing == false)
            {
                this.GenocideEnding = true;
                this.BeginFadingOut();
            }
        }

		if (this.FadeOut)
		{
			if (this.Yandere.Laughing)
			{
				this.Yandere.StopLaughing();
			}

			if (this.Clock.TimeSkip || this.Yandere.CanMove)
			{
				if (this.Clock.TimeSkip)
				{
					this.Clock.EndTimeSkip();
				}

				this.Yandere.StopAiming();
				this.Yandere.CanMove = false;
				this.Yandere.YandereVision = false;
				this.Yandere.PauseScreen.enabled = false;
				this.Yandere.Character.GetComponent<Animation>().CrossFade(AnimNames.FemaleIdleShort);

				if (this.Yandere.Mask != null)
				{
					this.Yandere.Mask.Drop();
				}

				if (this.Yandere.PickUp != null)
				{
					this.Yandere.EmptyHands();
				}

				if (this.Yandere.Dragging || this.Yandere.Carrying)
				{
					this.Yandere.EmptyHands();
				}
			}

			this.PauseScreen.Panel.alpha = Mathf.MoveTowards(
				this.PauseScreen.Panel.alpha, 0.0f, Time.deltaTime);

			this.Darkness.color = new Color(
				this.Darkness.color.r,
				this.Darkness.color.g,
				this.Darkness.color.b,
				Mathf.MoveTowards(this.Darkness.color.a, 1, Time.deltaTime));

			if (this.Darkness.color.a >= 1.0f)
			{
				if (!this.ShowResults)
				{
					this.HeartbeatCamera.SetActive(false);
					this.DetectionCamera.SetActive(false);

					if (this.ClubActivity)
					{
						this.ClubManager.Club = this.Yandere.Club;
						this.ClubManager.ClubActivity();
						this.FadeOut = false;
					}
					else
					{
						this.Yandere.MyController.enabled = false;
						this.Yandere.enabled = false;
						this.DetermineResults();
						this.ShowResults = true;
						Time.timeScale = 2.0f;
						this.Jukebox.Volume = 0.0f;
					}

                    if (this.GenocideEnding)
                    {
                        SceneManager.LoadScene(SceneNames.GenocideScene);
                    }
                }
            }
		}

		if (this.ShowResults)
		{
			this.ResultsTimer += Time.deltaTime;

			if (this.ResultsTimer > 1.0f)
			{
				UILabel resultLabel0 = this.ResultsLabels[0];
				resultLabel0.color = new Color(
					resultLabel0.color.r,
					resultLabel0.color.g,
					resultLabel0.color.b,
					resultLabel0.color.a + Time.deltaTime);
			}

			if (this.ResultsTimer > 2.0f)
			{
				UILabel resultLabel1 = this.ResultsLabels[1];
				resultLabel1.color = new Color(
					resultLabel1.color.r,
					resultLabel1.color.g,
					resultLabel1.color.b,
					resultLabel1.color.a + Time.deltaTime);
			}

			if (this.ResultsTimer > 3.0f)
			{
				UILabel resultLabel2 = this.ResultsLabels[2];
				resultLabel2.color = new Color(
					resultLabel2.color.r,
					resultLabel2.color.g,
					resultLabel2.color.b,
					resultLabel2.color.a + Time.deltaTime);
			}

			if (this.ResultsTimer > 4.0f)
			{
				UILabel resultLabel3 = this.ResultsLabels[3];
				resultLabel3.color = new Color(
					resultLabel3.color.r,
					resultLabel3.color.g,
					resultLabel3.color.b,
					resultLabel3.color.a + Time.deltaTime);
			}

			if (this.ResultsTimer > 5.0f)
			{
				UILabel resultLabel4 = this.ResultsLabels[4];
				resultLabel4.color = new Color(
					resultLabel4.color.r,
					resultLabel4.color.g,
					resultLabel4.color.b,
					resultLabel4.color.a + Time.deltaTime);
			}

			if (this.ResultsTimer > 6.0f)
			{
				this.ContinueButton.color = new Color(
					this.ContinueButton.color.r,
					this.ContinueButton.color.g,
					this.ContinueButton.color.b,
					this.ContinueButton.color.a + Time.deltaTime);

				this.ContinueLabel.color = new Color(
					this.ContinueLabel.color.r,
					this.ContinueLabel.color.g,
					this.ContinueLabel.color.b,
					this.ContinueLabel.color.a + Time.deltaTime);

				if (this.ContinueButton.color.a > 1.0f)
				{
					this.ContinueButton.color = new Color(
						this.ContinueButton.color.r,
						this.ContinueButton.color.g,
						this.ContinueButton.color.b,
						1.0f);
				}

				if (this.ContinueLabel.color.a > 1.0f)
				{
					this.ContinueLabel.color = new Color(
						this.ContinueLabel.color.r,
						this.ContinueLabel.color.g,
						this.ContinueLabel.color.b,
						1.0f);
				}
			}

			if (Input.GetKeyDown("space"))
			{
				this.ShowResults = false;
				this.FadeResults = true;
				this.FadeOut = false;

				this.ResultsTimer = 0.0f;
			}

			if (this.ResultsTimer > 7.0f)
			{
				if (Input.GetButtonDown(InputNames.Xbox_A))
				{
					this.ShowResults = false;
					this.FadeResults = true;
					this.FadeOut = false;

					this.ResultsTimer = 0.0f;
				}
			}
		}

		// [af] Converted while loop to foreach loop.
		foreach (UILabel label in this.ResultsLabels)
		{
			if (label.color.a > 1.0f)
			{
				label.color = new Color(
					label.color.r,
					label.color.g,
					label.color.b,
					1.0f);
			}
		}
			
		if (this.FadeResults)
		{
			// [af] Converted while loop to foreach loop.
			foreach (UILabel label in this.ResultsLabels)
			{
				label.color = new Color(
					label.color.r,
					label.color.g,
					label.color.b,
					label.color.a - Time.deltaTime);
			}

			this.ContinueButton.color = new Color(
				this.ContinueButton.color.r,
				this.ContinueButton.color.g,
				this.ContinueButton.color.b,
				this.ContinueButton.color.a - Time.deltaTime);

			this.ContinueLabel.color = new Color(
				this.ContinueLabel.color.r,
				this.ContinueLabel.color.g,
				this.ContinueLabel.color.b,
				this.ContinueLabel.color.a - Time.deltaTime);

			if (ResultsLabels[0].color.a <= 0.0f)
			{
				if (this.BeginConfession)
				{
					this.LoveManager.Suitor = this.StudentManager.Students[1];
					this.LoveManager.Rival = this.StudentManager.Students[this.StudentManager.RivalID];

					this.LoveManager.Suitor.CharacterAnimation.enabled = true;
					this.LoveManager.Rival.CharacterAnimation.enabled = true;

					this.LoveManager.BeginConfession();

					Time.timeScale = 1.0f;

					enabled = false;
				}
				else if (this.GameOver)
				{
					this.Heartbroken.transform.parent.transform.parent = null;

					// [af] Added "gameObject" for C# compatibility.
					this.Heartbroken.transform.parent.gameObject.SetActive(true);

					this.Heartbroken.Noticed = false;

					this.transform.parent.transform.parent.gameObject.SetActive(false);

					if (!this.EndOfDay.gameObject.activeInHierarchy)
					{
						Time.timeScale = 1.0f;
					}
				}
				else if (this.LowRep)
				{
					this.Yandere.RPGCamera.enabled = false;
					this.Yandere.RPGCamera.transform.parent = LowRepGameOver.MyCamera;
					this.Yandere.RPGCamera.transform.localPosition = new Vector3(0, 0, 0);
					this.Yandere.RPGCamera.transform.localEulerAngles = new Vector3(0, 0, 0);

					this.LowRepGameOver.gameObject.SetActive(true);
					this.UICamera.SetActive(false);
					this.FPS.SetActive(false);
					Time.timeScale = 1;

					this.enabled = false;
				}
				else if (!this.TeacherReport)
				{
					if (this.EndOfDay.Phase == 1)
					{
						this.EndOfDay.gameObject.SetActive(true);
						this.EndOfDay.enabled = true;
						this.EndOfDay.Phase = 14;

						if (this.EndOfDay.PreviouslyActivated)
						{
							this.EndOfDay.Start();
						}

						// [af] Moved assignments into a loop.
						for (int i = 0; i < 5; i++)
						{
							this.ResultsLabels[i].text = string.Empty;
						}

						this.enabled = false;
					}
				}
				else
				{
					this.DetermineResults();

					this.TeacherReport = false;

					this.FadeResults = false;
					this.ShowResults = true;
				}
			}
		}
	}

	public int SuspensionLength;
	public int RemainingDays;
	public bool Suspended;

	void DetermineResults()
	{
		this.ResultsLabels[0].transform.parent.gameObject.SetActive(true);

		// If the police were called...
		if (this.Show)
		{
			this.EndOfDay.gameObject.SetActive(true);
			this.enabled = false;

			// [af] Moved assignments into a loop.
			for (int i = 0; i < 5; i++)
			{
				this.ResultsLabels[i].text = string.Empty;
			}
		}
		// If the police were not called...
		else
		{
			if (this.Yandere.ShoulderCamera.GoingToCounselor)
			{
				this.ResultsLabels[0].text = "While Ayano was in the counselor's office,";
				this.ResultsLabels[1].text = "a corpse was discovered on school grounds.";
				this.ResultsLabels[2].text = "The school faculty was informed of the corpse,";
				this.ResultsLabels[3].text = "and the police were called to the school.";
				this.ResultsLabels[4].text = "No one is allowed to leave school until a police investigation has taken place.";

				this.TeacherReport = true;
				this.Show = true;
			}
			else
			{
				if (this.Reputation.Reputation <= -100)
				{
					this.ResultsLabels[0].text = "Ayano's bizarre conduct has been observed and discussed by many people.";
					this.ResultsLabels[1].text = "Word of Ayano's strange behavior has reached Senpai.";
					this.ResultsLabels[2].text = "Senpai is now aware that Ayano is a deranged person.";
					this.ResultsLabels[3].text = "From this day forward, Senpai will fear and avoid Ayano.";
					this.ResultsLabels[4].text = "Ayano will never have her Senpai's love.";

					this.LowRep = true;
				}
				else
				{
					//Anti-Osana Code
					bool DebugBuild = false;

					#if !UNITY_EDITOR
					DebugBuild = true;
					#endif

					//If it's Friday and the player is on the debug build, we can just end it here.
					if (DebugBuild && DateGlobals.Weekday == DayOfWeek.Friday)
					{
						this.ResultsLabels[0].text = "This is the part where the game will determine whether or not the player has eliminated their rival.";
						this.ResultsLabels[1].text = "This game is still in development.";
						this.ResultsLabels[2].text = "The ''player eliminated rival'' state has not yet been implemented.";
						this.ResultsLabels[3].text = "Thank you for playtesting Yandere Simulator!";
						this.ResultsLabels[4].text = "Please check back soon for more updates!";

						this.GameOver = true;
					}
					//Outside of that one specific circumstance...
					else
					{
						// If the player did not stage a suicide or a natural death...
						if (!this.Suicide && !this.PoisonScene)
						{
							if (this.Clock.HourTime < 18.0f)
							{
								if (this.Yandere.InClass)
								{
									this.ResultsLabels[0].text = "Ayano attempts to attend class without disposing of a corpse.";
								}
								else if (this.Yandere.Resting && this.Corpses > 0)
								{
									this.ResultsLabels[0].text = "Ayano rests without disposing of a corpse.";
								}
								else if (this.Yandere.Resting)
								{
									if (GameGlobals.SenpaiMourning){this.ResultsLabels[0].text = "Ayano recovers from her injuries, and is ready to leave school.";}
									else {this.ResultsLabels[0].text = "Ayano recovers from her injuries, and is ready to leave school.";}
								}
								else
								{
									if (GameGlobals.SenpaiMourning){this.ResultsLabels[0].text = "Ayano is ready to leave school.";}
									else {this.ResultsLabels[0].text = "Ayano is ready to leave school.";}
								}
							}
							else
							{
								this.ResultsLabels[0].text = "The school day has ended. Faculty members must walk through the school and tell any lingering students to leave.";
							}

							if (this.Suspended)
							{
								     if (this.Clock.Weekday == 1){RemainingDays = 5;}
								else if (this.Clock.Weekday == 2){RemainingDays = 4;}
								else if (this.Clock.Weekday == 3){RemainingDays = 3;}
								else if (this.Clock.Weekday == 4){RemainingDays = 2;}
								else if (this.Clock.Weekday == 5){RemainingDays = 1;}

								if (this.RemainingDays - this.SuspensionLength <= 0)
								{
									this.ResultsLabels[0].text = "Due to her suspension,";
									this.ResultsLabels[1].text = "Ayano will be unable";
									this.ResultsLabels[2].text = "to prevent her rival";
									this.ResultsLabels[3].text = "from confessing to Senpai.";
									this.ResultsLabels[4].text = "Ayano will never have Senpai.";

									this.GameOver = true;
								}
								else if (this.SuspensionLength == 1)
								{
									this.ResultsLabels[0].text = "Ayano has been sent home early.";
									this.ResultsLabels[1].text = "";
									this.ResultsLabels[2].text = "She won't be able to see Senpai again until tomorrow.";
									this.ResultsLabels[3].text = "";
									this.ResultsLabels[4].text = "Ayano's heart aches as she thinks of Senpai.";
								}
								else if (this.SuspensionLength == 2)
								{
									this.ResultsLabels[0].text = "Ayano has been sent home early.";
									this.ResultsLabels[1].text = "";
									this.ResultsLabels[2].text = "She will have to wait one day before returning to school.";
									this.ResultsLabels[3].text = "";
									this.ResultsLabels[4].text = "Ayano's heart aches as she thinks of Senpai.";
								}
								else
								{
									this.ResultsLabels[0].text = "Ayano has been sent home early.";
									this.ResultsLabels[1].text = "";
									this.ResultsLabels[2].text = "She will have to wait " + (this.SuspensionLength - 1) + " days before returning to school.";
									this.ResultsLabels[3].text = "";
									this.ResultsLabels[4].text = "Ayano's heart aches as she thinks of Senpai.";
								}
							}
							else 
							{
								if (this.Yandere.RedPaint)
								{
									BloodyClothing--;
								}

								// If there is no evidence of murder anywhere at school...
								if (this.Corpses == 0 && this.LimbParent.childCount == 0 && this.BloodParent.childCount == 0 &&
									this.BloodyWeapons == 0 && this.BloodyClothing == 0 && !this.SuicideScene)
								{
									// If Ayano is insane or bloody...
									if (this.Yandere.Sanity < 66.66666f || this.Yandere.Bloodiness > 0.0f && !this.Yandere.RedPaint)
									{
										this.ResultsLabels[1].text = "Ayano is approached by a faculty member.";

										// If Ayano is bloody...
										if (this.Yandere.Bloodiness > 0.0f)
										{
											this.ResultsLabels[2].text = "The faculty member immediately notices the blood staining her clothing.";
											this.ResultsLabels[3].text = "Ayano is not able to convince the faculty member that nothing is wrong.";
											this.ResultsLabels[4].text = "The faculty member calls the police.";

											this.TeacherReport = true;
											this.Show = true;
										}
										// If Ayano is insane...
										else
										{
											this.ResultsLabels[2].text = "Ayano exhibited extremely erratic behavior, frightening the faculty member.";
											this.ResultsLabels[3].text = "The faculty member becomes angry with Ayano, but Ayano leaves before the situation gets worse.";
											this.ResultsLabels[4].text = "Ayano returns home.";
										}
									}
									// If Ayano is not insane or bloody...
									else
									{
										//If the player is attempting to leave school with stolen property...
										if (this.Yandere.Inventory.RivalPhone &&
											this.StudentManager.CommunalLocker.RivalPhone.StudentID == this.StudentManager.RivalID &&
											!this.StudentManager.RivalEliminated ||
											this.Yandere.Inventory.RivalPhone &&
											this.StudentManager.CommunalLocker.RivalPhone.StudentID != this.StudentManager.RivalID &&
											this.StudentManager.Students[this.StudentManager.CommunalLocker.RivalPhone.StudentID].Alive)
										{
											//If Osana's phone is stolen...
											if (this.StudentManager.CommunalLocker.RivalPhone.StudentID == this.StudentManager.RivalID)
											{
												this.ResultsLabels[1].text = "Osana tells the faculty that her phone is missing.";
												this.ResultsLabels[2].text = "Suspecting theft, the faculty check all students' belongings before they are allowed to leave school.";
												this.ResultsLabels[3].text = "Osana's stolen phone is found on Ayano's person.";
												this.ResultsLabels[4].text = "Ayano is expelled from school for stealing from another student.";
											}
											else
											{
												this.ResultsLabels[1].text = "A student tells the faculty that her phone is missing.";
												this.ResultsLabels[2].text = "Suspecting theft, the faculty check all students' belongings before they are allowed to leave school.";
												this.ResultsLabels[3].text = "The student's stolen phone is found on Ayano's person.";
												this.ResultsLabels[4].text = "Ayano is expelled from school for stealing from another student.";
											}

											this.GameOver = true;
											this.Heartbroken.Counselor.Expelled = true;
										}
										//If the player has done absolutely nothing wrong and it's time to end the day normally...
										else
										{											
											// If it is Friday...
											if (DateGlobals.Weekday == DayOfWeek.Friday)
											{
												//If the player has not eliminated their rival...
												if (!this.StudentManager.RivalEliminated)
												{
													ResultsLabels[0].text = "Ayano has failed to eliminate Osana before Friday evening.";
													ResultsLabels[1].text = "Osana asks Senpai to meet her under the cherry tree behind the school.";
													ResultsLabels[2].text = "As cherry blossoms fall around them...";
													ResultsLabels[3].text = "...Osana confesses her feelings for Senpai.";
													ResultsLabels[4].text = "Ayano watches from a short distance away...";

													this.BeginConfession = true;
												}
												//If the player has eliminated their rival...
												else
												{
													this.ResultsLabels[0].text = "Ayano no longer has to worry about competing with Osana for Senpai's love.";
													this.ResultsLabels[1].text = "Ayano considers confessing her love to Senpai...";
													this.ResultsLabels[2].text = "...but she cannot build up the courage to speak to him.";
													this.ResultsLabels[3].text = "Ayano follows Senpai out of school and watches him from a distance until he has returned to his home.";
													this.ResultsLabels[4].text = "Then, Ayano returns to her own home, and considers what she should do next...";
												}
											}
											//If it is not Friday...
											else
											{
												//If the player manually exited school...
												if (this.Clock.HourTime < 18.0f)
												{
													//Debug.Log ("Senpai's position is: " + this.Yandere.Senpai.position);

													//If Senpai has not yet left the school...
													if (this.Yandere.Senpai.position.z > -75f)
													{
														this.ResultsLabels[1].text = "However, she can't bring herself to leave before Senpai does.";
														this.ResultsLabels[2].text = "Ayano waits at the school's entrance until Senpai eventually appears.";
														this.ResultsLabels[3].text = "She follows him and watches him from a distance until he has returned to his home.";
														this.ResultsLabels[4].text = "Then, Ayano returns to her house.";
													}
													//Senpai has already left school...
													else
													{
														this.ResultsLabels[1].text = "Ayano quickly runs out of school, determined to catch a glimpse of Senpai as he walks home.";
														this.ResultsLabels[2].text = "Eventually, she catches up to him.";
														this.ResultsLabels[3].text = "Ayano follows Senpai and watches him from a distance until he has returned to his home.";
														this.ResultsLabels[4].text = "Then, Ayano returns to her house.";
													}
												}
												//If the player let time pass until 6:00 PM...
												else
												{
													//this.ResultsLabels[0].text = "The school day has ended. Faculty members must walk through the school and tell any lingering students to leave.";
													this.ResultsLabels[1].text = "Like all other students, Ayano is instructed to leave school.";
													this.ResultsLabels[2].text = "After exiting school, Ayano locates Senpai.";
													this.ResultsLabels[3].text = "Ayano follows Senpai and watches him from a distance until he has returned to his home.";
													this.ResultsLabels[4].text = "Then, Ayano returns to her house.";
												}

												if (GameGlobals.SenpaiMourning)
												{
													this.ResultsLabels[1].text = "Like all other students, Ayano is instructed to leave school.";
													this.ResultsLabels[2].text = "Ayano leaves school.";
													this.ResultsLabels[3].text = "Ayano returns to her home.";
													this.ResultsLabels[4].text = "Her heart aches as she thinks of Senpai.";
												}
											}
										}
									}
								}
								else
								{
									// If all corpses have been disposed of...
									if (this.Corpses > 0)
									{
										this.ResultsLabels[1].text = "While walking around the school, a faculty member discovers a corpse.";
										this.ResultsLabels[2].text = "The faculty member immediately calls the police.";
										this.ResultsLabels[3].text = "The faculty member informs the rest of the faculty about her discovery.";
										this.ResultsLabels[4].text = "The faculty do not allow any students to leave the school until a police investigation has taken place.";

										this.TeacherReport = true;
										this.Show = true;
									}
									// If the player has not disposed of all limbs...
									else if (this.LimbParent.childCount > 0)
									{
										this.ResultsLabels[1].text = "While walking around the school, a faculty member discovers a dismembered body part.";
										this.ResultsLabels[2].text = "The faculty member decides to call the police.";
										this.ResultsLabels[3].text = "The faculty member informs the rest of the faculty about her discovery.";
										this.ResultsLabels[4].text = "The faculty do not allow any students to leave the school until a police investigation has taken place.";

										this.TeacherReport = true;
										this.Show = true;
									}
									// If the player has not disposed of all blood...
									else if (this.BloodParent.childCount > 0 || this.BloodyClothing > 0)
									{
										this.ResultsLabels[1].text = "While walking around the school, a faculty member discovers a mysterious blood stain.";
										this.ResultsLabels[2].text = "The faculty member decides to call the police.";
										this.ResultsLabels[3].text = "The faculty member informs the rest of the faculty about her discovery.";
										this.ResultsLabels[4].text = "The faculty do not allow any students to leave the school until a police investigation has taken place.";

										this.TeacherReport = true;
										this.Show = true;
									}
									// If the player has not disposed of the murder weapons...
									else if (this.BloodyWeapons > 0)
									{
										this.ResultsLabels[1].text = "While walking around the school, a faculty member discovers a mysterious bloody weapon.";
										this.ResultsLabels[2].text = "The faculty member decides to call the police.";
										this.ResultsLabels[3].text = "The faculty member informs the rest of the faculty about her discovery.";
										this.ResultsLabels[4].text = "The faculty do not allow any students to leave the school until a police investigation has taken place.";

										this.TeacherReport = true;
										this.Show = true;
									}
									// If the player left a suicide scene at school...
									else if (this.SuicideScene)
									{
										this.ResultsLabels[1].text = "While walking around the school, a faculty member discovers a pair of shoes on the rooftop.";
										this.ResultsLabels[2].text = "The faculty member fears that there has been a suicide, but cannot find a corpse anywhere. The faculty member does not take any action.";
										this.ResultsLabels[3].text = "Ayano leaves school and follows Senpai, watching him as he walks home.";
										this.ResultsLabels[4].text = "Once he is safely home, Ayano returns to her own home.";

										if (GameGlobals.SenpaiMourning)
										{
											this.ResultsLabels[3].text = "Ayano leaves school.";
											this.ResultsLabels[4].text = "Ayano returns home.";
										}
									}
								}
							}
						}
						// If the player staged a death...
						else
						{
							// If the player staged a suicide...
							if (this.Suicide)
							{
								if (!this.Yandere.InClass)
								{
									this.ResultsLabels[0].text = "The school day has ended. Faculty members must walk through the school and tell any lingering students to leave.";
								}
								else
								{
									this.ResultsLabels[0].text = "Ayano attempts to attend class without disposing of a corpse.";

								}

								this.ResultsLabels[1].text = "While walking around the school, a faculty member discovers a corpse.";
								this.ResultsLabels[2].text = "It appears as though a student has committed suicide.";
								this.ResultsLabels[3].text = "The faculty member informs the rest of the faculty about her discovery.";
								this.ResultsLabels[4].text = "The faculty members agree to call the police and report the student's death.";

								this.TeacherReport = true;
								this.Show = true;
							}
							// If the player used poison...
							else if (this.PoisonScene)
							{
								this.ResultsLabels[0].text = "A faculty member discovers the student who Ayano poisoned.";
								this.ResultsLabels[1].text = "The faculty member calls for an ambulance immediately.";
								this.ResultsLabels[2].text = "The faculty member suspects that the student's death was a murder.";
								this.ResultsLabels[3].text = "The faculty member also calls for the police.";
								this.ResultsLabels[4].text = "The school's students are not allowed to leave until a police investigation has taken place.";

								this.TeacherReport = true;
								this.Show = true;
							}
						}
					}
				}
			}
		}
	}

	public void KillStudents()
	{
		//Debug.Log("KillStudents() is being called.");

		if (this.Deaths > 0)
		{
			//Debug.Log("There were deaths at school today.");

			// [af] Converted while loop to for loop.
			for (int ID = 2; ID < this.StudentManager.NPCsTotal + 1; ID++)
			{
				if (StudentGlobals.GetStudentDying(ID))
				{
					if (ID < 90)
					{
						SchoolGlobals.SchoolAtmosphere -= 0.05f;
					}
					else
					{
						SchoolGlobals.SchoolAtmosphere -= 0.15f;
					}

					if (this.JSON.Students[ID].Club == ClubType.Council)
					{
						SchoolGlobals.SchoolAtmosphere -= 1;
						SchoolGlobals.HighSecurity = true;
					}

					StudentGlobals.SetStudentDead(ID, true);
					PlayerGlobals.Kills++;
				}
			}

			SchoolGlobals.SchoolAtmosphere -= (this.Corpses * 0.05f);

			//YD - Attempting a new formula for calculating school atmosphere.
			//May revert to old formula in the future.
			//SchoolGlobals.SchoolAtmosphere -= (this.Deaths * 0.050f) + (this.Corpses * 0.050f);

			if (this.SuicideVictims + this.DrownVictims + this.Corpses > 0)
			{
				//Debug.Log("There are corpses on school grounds.");

				foreach (RagdollScript corpse in this.CorpseList)
				{
					if (corpse != null)
					{
						if (StudentGlobals.MemorialStudents < 9)
						{
							//Debug.Log("''MemorialStudents'' is being incremented upwards.");

							StudentGlobals.MemorialStudents++;

								 if (StudentGlobals.MemorialStudents == 1){StudentGlobals.MemorialStudent1 = corpse.Student.StudentID;}// Debug.Log("We need to hold a memorial for Student " + StudentGlobals.MemorialStudent1);}
							else if (StudentGlobals.MemorialStudents == 2){StudentGlobals.MemorialStudent2 = corpse.Student.StudentID;}// Debug.Log("We need to hold a memorial for Student " + StudentGlobals.MemorialStudent2);}
							else if (StudentGlobals.MemorialStudents == 3){StudentGlobals.MemorialStudent3 = corpse.Student.StudentID;}// Debug.Log("We need to hold a memorial for Student " + StudentGlobals.MemorialStudent3);}
							else if (StudentGlobals.MemorialStudents == 4){StudentGlobals.MemorialStudent4 = corpse.Student.StudentID;}// Debug.Log("We need to hold a memorial for Student " + StudentGlobals.MemorialStudent4);}
							else if (StudentGlobals.MemorialStudents == 5){StudentGlobals.MemorialStudent5 = corpse.Student.StudentID;}// Debug.Log("We need to hold a memorial for Student " + StudentGlobals.MemorialStudent5);}
							else if (StudentGlobals.MemorialStudents == 6){StudentGlobals.MemorialStudent6 = corpse.Student.StudentID;}// Debug.Log("We need to hold a memorial for Student " + StudentGlobals.MemorialStudent6);}
							else if (StudentGlobals.MemorialStudents == 7){StudentGlobals.MemorialStudent7 = corpse.Student.StudentID;}// Debug.Log("We need to hold a memorial for Student " + StudentGlobals.MemorialStudent7);}
							else if (StudentGlobals.MemorialStudents == 8){StudentGlobals.MemorialStudent8 = corpse.Student.StudentID;}// Debug.Log("We need to hold a memorial for Student " + StudentGlobals.MemorialStudent8);}
							else if (StudentGlobals.MemorialStudents == 9){StudentGlobals.MemorialStudent9 = corpse.Student.StudentID;}// Debug.Log("We need to hold a memorial for Student " + StudentGlobals.MemorialStudent9);}
						}
					}
				}
			}
		}
		else
		{
			if (!SchoolGlobals.HighSecurity)
			{
				SchoolGlobals.SchoolAtmosphere += 0.20f;
			}
		}

		SchoolGlobals.SchoolAtmosphere = Mathf.Clamp01(SchoolGlobals.SchoolAtmosphere);

		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < this.StudentManager.StudentsTotal; ID++)
		{
			StudentScript student = this.StudentManager.Students[ID];

			if (student != null)
			{
				if (student.Grudge)
				{
					if (student.Persona != PersonaType.Evil)
					{
						StudentGlobals.SetStudentGrudge(ID, true);

						if (student.OriginalPersona == PersonaType.Sleuth && !StudentGlobals.GetStudentDying(ID))
						{
							StudentGlobals.SetStudentGrudge(56, true);
							StudentGlobals.SetStudentGrudge(57, true);
							StudentGlobals.SetStudentGrudge(58, true);
							StudentGlobals.SetStudentGrudge(59, true);
							StudentGlobals.SetStudentGrudge(60, true);
						}
					}
				}
			}
		}
	}

	public void BeginFadingOut()
	{
		//Debug.Log("BeginFadingOut() has been called.");

		//This has to come first.
		this.DayOver = true;

		this.StudentManager.StopMoving();
		this.Darkness.enabled = true;
		this.Yandere.StopLaughing();
		this.Clock.StopTime = true;
		this.FadeOut = true;

		if (!this.EndOfDay.gameObject.activeInHierarchy)
		{
			Time.timeScale = 1.0f;
		}
	}

	public void UpdateCorpses()
	{
		foreach (RagdollScript corpse in this.CorpseList)
		{
			if (corpse != null)
			{
				corpse.Prompt.HideButton[3] = true;

				if (((this.Yandere.Class.PhysicalGrade + this.Yandere.Class.PhysicalBonus) > 0) && !corpse.Tranquil)
				{
					corpse.Prompt.HideButton[3] = false;
				}
			}
		}
	}
}