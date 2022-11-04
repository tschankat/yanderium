using UnityEngine;

public class PortalScript : MonoBehaviour
{
	public RivalMorningEventManagerScript[] MorningEvents;
	public OsanaMorningFriendEventScript[] FriendEvents;
	public OsanaMondayBeforeClassEventScript OsanaEvent;

	public OsanaFridayBeforeClassEvent1Script OsanaFridayEvent1;
	public OsanaFridayBeforeClassEvent2Script OsanaFridayEvent2;
	public OsanaFridayLunchEventScript OsanaFridayLunchEvent;
	public OsanaClubEventScript OsanaClubEvent;
	public OsanaPoolEventScript OsanaPoolEvent;

	public DelinquentManagerScript DelinquentManager;
	public StudentManagerScript StudentManager;
	public LoveManagerScript LoveManager;
	public ReputationScript Reputation;
	public PromptBarScript PromptBar;
	public YandereScript Yandere;
	public PoliceScript Police;
	public PromptScript Prompt;
	public ClassScript Class;
	public ClockScript Clock;

	public GameObject HeartbeatCamera;
	public GameObject Headmaster;

	public UISprite ClassDarkness;

	public Texture HomeMapMarker;
	public Renderer MapMarker;

	public Transform Teacher;

	public bool CanAttendClass = false;
	public bool LateReport1 = false;
	public bool LateReport2 = false;
	public bool Transition = false;
	public bool FadeOut = false;
	public bool Proceed = false;

	public float Timer = 0.0f;

	public int Late = 0;

	void Start()
	{
		this.ClassDarkness.enabled = false;
	}

	void Update()
	{
		if ((this.Clock.HourTime > 8.52f) &&
			(this.Clock.HourTime < 8.53f) &&
			!this.Yandere.InClass && !this.LateReport1)
		{
			this.LateReport1 = true;
			this.Yandere.NotificationManager.DisplayNotification(NotificationType.Late);
		}

		if ((this.Clock.HourTime > 13.52f) &&
			(this.Clock.HourTime < 13.53f) &&
			!this.Yandere.InClass && !this.LateReport2)
		{
			this.LateReport2 = true;
			this.Yandere.NotificationManager.DisplayNotification(NotificationType.Late);
		}

		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Prompt.Circle[0].fillAmount = 1.0f;

			this.CheckForLateness();
			this.Reputation.UpdateRep();

			bool Skip = false;

			if (this.Police.PoisonScene || this.Police.SuicideScene && 
				(this.Police.Corpses - this.Police.HiddenCorpses) > 0 ||
				(this.Police.Corpses - this.Police.HiddenCorpses) > 0 ||
				(Reputation.Reputation <= -100))
			{
				this.EndDay();
			}
			else
			{
				if (this.Clock.HourTime < 15.50f)
				{
					if (!this.Police.Show)
					{
						bool TeacherPresent = false;

						if (this.StudentManager.Teachers[21] != null)
						{
							if (this.StudentManager.Teachers[21].DistanceToDestination < 1)
							{
								TeacherPresent = true;
							}
						}

						if (TeacherPresent)
						{
							//Debug.Log("The teacher is present.");
						}
						else
						{
							//Debug.Log("The teacher is not present.");
						}

						if (this.Late > 0 && TeacherPresent)
						{
							this.Yandere.Subtitle.UpdateLabel(SubtitleType.TeacherLateReaction, this.Late, 5.50f);
							this.Yandere.RPGCamera.enabled = false;
							this.Yandere.ShoulderCamera.Scolding = true;
							this.Yandere.ShoulderCamera.Teacher = this.Teacher;
						}
						else
						{
							this.ClassDarkness.enabled = true;
							this.Transition = true;
							this.FadeOut = true;
						}

						this.Clock.StopTime = true;
					}
					else
					{
						this.EndDay();
					}
				}
				else
				{
					if (this.Yandere.Inventory.RivalPhone && !this.StudentManager.RivalEliminated)
					{
						this.Yandere.NotificationManager.CustomText = "Put the stolen phone on the owner's desk!";
						this.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
						this.Yandere.NotificationManager.CustomText = "You are carrying a stolen phone!";
						this.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);

						Skip = true;
					}
					else
					{
						this.EndDay();
					}
				}
			}

			if (!Skip)
			{
				this.Yandere.Character.GetComponent<Animation>().CrossFade(Yandere.IdleAnim);
				this.Yandere.YandereVision = false;
				this.Yandere.CanMove = false;

				//Debug.Log("As of right now, the time is: " + this.Clock.HourTime);

				if (this.Clock.HourTime < 15.50f)
				{
					this.Yandere.InClass = true;

					if (this.Clock.HourTime < 8.5f)
					{
						this.EndEvents();
					}
					else
					{
						this.EndLaterEvents();
					}
				}
			}
		}

		if (this.Transition)
		{
			if (this.FadeOut)
			{
				if (this.LoveManager.HoldingHands)
				{
					this.LoveManager.Rival.transform.position = new Vector3(0, 0, -50);
				}

				this.ClassDarkness.color = new Color(
					this.ClassDarkness.color.r,
					this.ClassDarkness.color.g,
					this.ClassDarkness.color.b,
					this.ClassDarkness.color.a + Time.deltaTime);

				if (this.ClassDarkness.color.a >= 1.0f)
				{
					if (this.Yandere.Resting)
					{
						this.Yandere.IdleAnim = "f02_idleShort_00";
						this.Yandere.WalkAnim = "f02_newWalk_00";

						this.Yandere.OriginalIdleAnim = this.Yandere.IdleAnim;
						this.Yandere.OriginalWalkAnim = this.Yandere.WalkAnim;

						this.Yandere.CharacterAnimation.CrossFade(this.Yandere.IdleAnim);

						this.Yandere.MyRenderer.materials[2].SetFloat("_BlendAmount1", 0);
						this.Yandere.Resting = false;
						this.Yandere.Health = 10;
						this.FadeOut = false;
						this.Proceed = true;
					}
					else
					{
						if (this.Yandere.Armed)
						{
							this.Yandere.Unequip();
						}

						this.HeartbeatCamera.SetActive(false);

						this.ClassDarkness.color = new Color(
							this.ClassDarkness.color.r,
							this.ClassDarkness.color.g,
							this.ClassDarkness.color.b,
							1.0f);

						this.FadeOut = false;
						this.Proceed = false;

						this.Yandere.RPGCamera.enabled = false;

						this.PromptBar.Label[4].text = "Choose";
						this.PromptBar.Label[5].text = "Allocate";
						this.PromptBar.UpdateButtons();

						this.PromptBar.Show = true;

						// [af] Replaced if/else statement with ternary expression.
						this.Class.StudyPoints = (PlayerGlobals.PantiesEquipped == 11) ? 10 : 5;

						this.Class.StudyPoints -= this.Late;
						this.Class.UpdateLabel();

						// [af] Added "gameObject" for C# compatibility.
						this.Class.gameObject.SetActive(true);

						this.Class.Show = true;

						if (this.Police.Show)
						{
							this.Police.Timer = 0.000001f;
						}
					}
				}
			}
			else
			{
				if (this.Proceed)
				{
					//Debug.Log("Proceeding.");

					if (this.ClassDarkness.color.a >= 1.0f)
					{
						//Debug.Log("Updating the time of day.");

						this.HeartbeatCamera.SetActive(true);
						this.Clock.enabled = true;

						this.Yandere.FixCamera();
						this.Yandere.RPGCamera.enabled = false;

						if (this.Clock.HourTime < 13.0f)
						{
							if (this.StudentManager.Bully)
							{
								this.StudentManager.UpdateGrafitti();
							}

							this.Yandere.Incinerator.Timer -= (13.0f * 60.0f) - this.Clock.PresentTime;
							this.DelinquentManager.CheckTime();

							//this.Clock.DeactivateTrespassZones();
							this.Clock.PresentTime = 13.0f * 60.0f;
							//this.Clock.Period++;
						}
						else
						{
							this.Yandere.Incinerator.Timer -= (15.50f * 60.0f) - this.Clock.PresentTime;
							this.DelinquentManager.CheckTime();

							//this.Clock.DeactivateTrespassZones();
							this.Clock.PresentTime = 15.50f * 60.0f;
							//this.Clock.Period++;
						}

						this.Clock.HourTime = this.Clock.PresentTime / 60.0f;

						this.StudentManager.AttendClass();
					}

					this.ClassDarkness.color = new Color(
						this.ClassDarkness.color.r,
						this.ClassDarkness.color.g,
						this.ClassDarkness.color.b,
						this.ClassDarkness.color.a - Time.deltaTime);

					if (this.ClassDarkness.color.a <= 0.0f)
					{
						this.ClassDarkness.enabled = false;

						this.ClassDarkness.color = new Color(
							this.ClassDarkness.color.r,
							this.ClassDarkness.color.g,
							this.ClassDarkness.color.b,
							0.0f);

						this.Clock.StopTime = false;
						this.Transition = false;
						this.Proceed = false;

						this.Yandere.RPGCamera.enabled = true;
						this.Yandere.InClass = false;
						this.Yandere.CanMove = true;

						this.StudentManager.ResumeMovement();

						if (!MissionModeGlobals.MissionMode)
						{
							if (this.Headmaster.activeInHierarchy)
							{
								this.Headmaster.SetActive(false);
							}
							else
							{
								this.Headmaster.SetActive(true);
							}
						}
					}
				}
			}
		}

		if (this.Clock.HourTime > 15.50f)
		{
			if (this.transform.position.z < 0.0f)
			{
				this.StudentManager.RemovePapersFromDesks();

				if (!MissionModeGlobals.MissionMode)
				{
					this.MapMarker.material.mainTexture = this.HomeMapMarker;

					this.transform.position = new Vector3(0.0f, 1.0f, -75.0f);
					this.Prompt.Label[0].text = "     " + "Go Home";
					this.Prompt.enabled = true;
				}
				else
				{
					this.transform.position = new Vector3(0.0f, -10.0f, 0.0f);
					this.Prompt.Hide();
					this.Prompt.enabled = false;
				}
			}
		}
		else
		{
			if (!this.Yandere.Police.FadeOut)
			{
				//Debug.Log("Distance is: " + Vector3.Distance(this.Yandere.transform.position, this.transform.position));

				if (Vector3.Distance(this.Yandere.transform.position, this.transform.position) < 1.4f)
				{
					this.CanAttendClass = true;

					this.CheckForProblems();

					if (!this.CanAttendClass)
					{
						if (this.Timer == 0)
						{
							if (this.Yandere.Armed){this.Yandere.NotificationManager.CustomText = "Carrying Weapon";}
							else if (this.Yandere.Bloodiness > 0.0f){this.Yandere.NotificationManager.CustomText = "Bloody";}
							else if (this.Yandere.Sanity < 33.333f){this.Yandere.NotificationManager.CustomText = "Visibly Insane";}

							else if (this.Yandere.Attacking){this.Yandere.NotificationManager.CustomText = "In Combat";}
							else if (this.Yandere.Dragging || this.Yandere.Carrying){this.Yandere.NotificationManager.CustomText = "Holding Corpse";}
							else if (this.Yandere.PickUp != null){this.Yandere.NotificationManager.CustomText = "Carrying Item";}

							else if (this.Yandere.Chased || this.Yandere.Chasers > 0){this.Yandere.NotificationManager.CustomText = "Chased";}
							else if (this.StudentManager.Reporter){this.Yandere.NotificationManager.CustomText = "Murder being reported";}
							else if (this.StudentManager.MurderTakingPlace){this.Yandere.NotificationManager.CustomText = "Murder taking place";}

							this.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
							this.Yandere.NotificationManager.CustomText = "Cannot attend class. Reason:";
							this.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
						}

						this.Prompt.Hide();
						this.Prompt.enabled = false;

						this.Timer += Time.deltaTime;

						if (this.Timer > 5)
						{
							this.Timer = 0;
						}
					}
					else
					{
						this.Prompt.enabled = true;
					}
				}
			}
		}
	}

	public void CheckForProblems()
	{
		if (this.Yandere.Armed || this.Yandere.Bloodiness > 0.0f || this.Yandere.Sanity < 33.333f ||
			this.Yandere.Attacking || this.Yandere.Dragging || this.Yandere.Carrying || this.Yandere.PickUp != null || 
			this.Yandere.Chased || this.Yandere.Chasers > 0 || this.StudentManager.Reporter != null ||
			this.StudentManager.MurderTakingPlace)
		{
			this.CanAttendClass = false;
		}
	}

	public void EndDay()
	{
        this.StudentManager.StopMoving();
		this.Yandere.StopLaughing();
		this.Yandere.EmptyHands();

		this.Clock.StopTime = true;

		this.Police.Darkness.enabled = true;
		this.Police.FadeOut = true;
		this.Police.DayOver = true;
	}

	void CheckForLateness()
	{
		//Debug.Log("Determining if Yandere-chan is late to class.");

		this.Late = 0;

		//if (this.StudentManager.Teachers[21] != null)
		//{
			//if (this.StudentManager.Teachers[21].DistanceToDestination < 1)
			//{
				if (this.Clock.HourTime < 13.0f)
				{
					if (this.Clock.HourTime < 8.52f)
					{
						this.Late = 0;
					}
					else if (this.Clock.HourTime < 10.0f)
					{
						this.Late = 1;
					}
					else if (this.Clock.HourTime < 11.0f)
					{
						this.Late = 2;
					}
					else if (this.Clock.HourTime < 12.0f)
					{
						this.Late = 3;
					}
					else if (this.Clock.HourTime < 13.0f)
					{
						this.Late = 4;
					}
				}
				else
				{
					if (this.Clock.HourTime < 13.52f)
					{
						this.Late = 0;
					}
					else if (this.Clock.HourTime < 14.0f)
					{
						this.Late = 1;
					}
					else if (this.Clock.HourTime < 14.50f)
					{
						this.Late = 2;
					}
					else if (this.Clock.HourTime < 15.0f)
					{
						this.Late = 3;
					}
					else if (this.Clock.HourTime < 15.50f)
					{
						this.Late = 4;
					}
				}

				this.Reputation.PendingRep -= (5 * Late);
			//}
		//}

		if (this.Late > 0)
		{
			//Debug.Log("Yep, Yandere-chan is late.");
		}
		else
		{
			//Debug.Log("Nope, Yandere-chan is not late.");
		}
	}

	public void EndEvents()
	{
		int ID = 0;

		while (ID < this.MorningEvents.Length)
		{
			if (this.MorningEvents[ID].enabled)
			{
				this.MorningEvents[ID].EndEvent();
			}

			ID++;
		}

		#if UNITY_EDITOR

		ID = 0;

		while (ID < this.FriendEvents.Length)
		{
			if (this.FriendEvents[ID].enabled)
			{
				this.FriendEvents[ID].EndEvent();
			}

			ID++;
		}

		if (this.OsanaEvent.enabled){this.OsanaEvent.EndEvent();}
		if (this.OsanaClubEvent.enabled){this.OsanaClubEvent.EndEvent();}

		//Friday
		if (this.OsanaFridayEvent1.enabled){this.OsanaFridayEvent1.EndEvent();}
		if (this.OsanaFridayEvent2.enabled){this.OsanaFridayEvent2.EndEvent();}

		#endif
	}

	public void EndLaterEvents()
	{
		#if UNITY_EDITOR

		if (this.OsanaPoolEvent.Phase > 0){this.OsanaPoolEvent.EndEvent();}

		//Friday
		if (this.OsanaFridayLunchEvent.enabled){this.OsanaFridayLunchEvent.EndEvent();}

		#endif
	}
}