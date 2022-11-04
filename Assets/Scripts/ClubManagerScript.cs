using UnityEngine;

public class ClubManagerScript : MonoBehaviour
{
	public ShoulderCameraScript ShoulderCamera;
	public StudentManagerScript StudentManager;
	public ComputerGamesScript ComputerGames;
	public BloodCleanerScript BloodCleaner;
	public RefrigeratorScript Refrigerator;
	public ClubWindowScript ClubWindow;
	public ContainerScript Container;
	public PromptBarScript PromptBar;
	public TranqCaseScript TranqCase;
	public YandereScript Yandere;
	public RPG_Camera MainCamera;
	public DoorScript ShedDoor;
	public PoliceScript Police;
	public GloveScript Gloves;
	public UISprite Darkness;

	public GameObject Reputation;
	public GameObject Heartrate;
	public GameObject Watermark;
	public GameObject Padlock;
	public GameObject Ritual;
	public GameObject Clock;
	public GameObject Cake;

	public AudioClip[] MotivationalQuotes;
	public Transform[] ClubPatrolPoints;
	public GameObject[] ClubPosters;
	public GameObject[] GameScreens;
	public Transform[] ClubVantages;
	public MaskScript[] Masks;

	public GameObject[] Cultists;

	public Transform[] Club1ActivitySpots;
	public Transform[] Club4ActivitySpots;
	public Transform[] Club6ActivitySpots;
	public Transform Club7ActivitySpot;
	public Transform[] Club8ActivitySpots;
	public Transform[] Club10ActivitySpots;

	public int[] Club1Students;
	public int[] Club2Students;
	public int[] Club3Students;
	public int[] Club4Students;
	public int[] Club5Students;
	public int[] Club6Students;
	public int[] Club7Students;
	public int[] Club8Students;
	public int[] Club9Students;
	public int[] Club10Students;
	public int[] Club11Students;
	public int[] Club14Students;

	public bool LeaderAshamed = false;
	public bool ClubEffect = false;

	public AudioClip OccultAmbience;

	public int ClubPhase = 0;
	public int Phase = 1;
	public ClubType Club = ClubType.None;
	public int ID = 0;

	public float TimeLimit = 0.0f;
	public float Timer = 0.0f;

	public ClubType[] ClubArray;

	void Start()
	{
		this.ClubWindow.ActivityWindow.localScale = Vector3.zero;
		this.ClubWindow.ActivityWindow.gameObject.SetActive(false);
		this.ActivateClubBenefit();

		int ClosedClubs = 0;

		ID = 1;

		while (ID < this.ClubArray.Length)
		{
			//Debug.Log("Checking to see which clubs are closed. ID is currently: " + ID);

			if (ClubGlobals.GetClubClosed(this.ClubArray[this.ID]) == true)
			{
				//Debug.Log(this.ClubArray[this.ID] + " is closed.");

				this.ClubPosters[this.ID].SetActive(false);

				if (this.ClubArray[this.ID] == ClubType.Gardening)
				{
					ClubPatrolPoints[ID].transform.position = new Vector3(
						-36,
						ClubPatrolPoints[ID].transform.position.y,
						ClubPatrolPoints[ID].transform.position.z);
				}
				else if (this.ClubArray[this.ID] == ClubType.Gaming)
				{
					ClubPatrolPoints[ID].transform.position = new Vector3(
						20,
						ClubPatrolPoints[ID].transform.position.y,
						ClubPatrolPoints[ID].transform.position.z);
				}
				else if (this.ClubArray[this.ID] != ClubType.Sports)
				{
					//Debug.Log("Adjusting patrol point of " + this.ClubArray[this.ID]);

					ClubPatrolPoints[ID].transform.position = new Vector3(
						ClubPatrolPoints[ID].transform.position.x,
						ClubPatrolPoints[ID].transform.position.y,
						20);
				}

				ClosedClubs++;
			}

			ID++;
		}

		if (ClosedClubs > 10)
		{
			StudentManager.NoClubMeeting = true;
		}

		if (ClubGlobals.GetClubClosed(this.ClubArray[2]))
		{
			this.StudentManager.HidingSpots.List[56] = this.StudentManager.Hangouts.List[56];
			this.StudentManager.HidingSpots.List[57] = this.StudentManager.Hangouts.List[57];
			this.StudentManager.HidingSpots.List[58] = this.StudentManager.Hangouts.List[58];
			this.StudentManager.HidingSpots.List[59] = this.StudentManager.Hangouts.List[59];
			this.StudentManager.HidingSpots.List[60] = this.StudentManager.Hangouts.List[60];

			this.StudentManager.SleuthPhase = 3;
		}

		ID = 0;
	}

	void Update()
	{
		if (this.Club != 0)
		{
			if (this.Phase == 1)
			{
				this.Darkness.color = new Color(
					this.Darkness.color.r,
					this.Darkness.color.g,
					this.Darkness.color.b,
					Mathf.MoveTowards(this.Darkness.color.a, 0.0f, Time.deltaTime));
			}

			AudioSource audioSource = this.GetComponent<AudioSource>();

			if (this.Darkness.color.a == 0.0f)
			{
				if (this.Phase == 1)
				{
					this.PromptBar.ClearButtons();
					this.PromptBar.Label[0].text = "Continue";
					this.PromptBar.UpdateButtons();
					this.PromptBar.Show = true;

					this.ClubWindow.PerformingActivity = true;
					this.ClubWindow.ActivityWindow.gameObject.SetActive(true);
					this.ClubWindow.ActivityLabel.text = this.ClubWindow.ActivityDescs[(int)this.Club];
					this.Phase++;
				}
				else if (this.Phase == 2)
				{
					if (this.ClubWindow.ActivityWindow.localScale.x > 0.90f)
					{
						if (this.Club == ClubType.MartialArts)
						{
							if (this.ClubPhase == 0)
							{
								audioSource.clip =
									this.MotivationalQuotes[Random.Range(0, this.MotivationalQuotes.Length)];
								audioSource.Play();

								this.ClubEffect = true;
								this.ClubPhase++;

								this.TimeLimit = audioSource.clip.length;
							}
							else if (this.ClubPhase == 1)
							{
								this.Timer += Time.deltaTime;

								if (this.Timer > this.TimeLimit)
								{
									// [af] Converted while loop to for loop.
									for (this.ID = 0; this.ID < this.Club6Students.Length; this.ID++)
									{
										if (this.StudentManager.Students[this.ID] != null)
										{
											if (!this.StudentManager.Students[this.ID].Tranquil)
											{
												this.StudentManager.Students[this.Club6Students[this.ID]].
													GetComponent<AudioSource>().volume = 1.0f;
											}
										}
									}

									this.ClubPhase++;
								}
							}
						}

						if (Input.GetButtonDown(InputNames.Xbox_A))
						{
							this.ClubWindow.PerformingActivity = false;
							this.PromptBar.Show = false;
							this.Phase++;
						}
					}
				}
				else
				{
					if (this.ClubWindow.ActivityWindow.localScale.x < 0.10f)
					{
						this.Police.Darkness.enabled = true;
						this.Police.ClubActivity = false;
						this.Police.FadeOut = true;
					}
				}
			}

			if (this.Club == ClubType.Occult)
			{
				audioSource.volume = 1.0f - this.Darkness.color.a;
			}
		}
	}

	public void ClubActivity()
	{
		this.StudentManager.StopMoving();

		this.ShoulderCamera.enabled = false;

		this.MainCamera.enabled = false;
		this.MainCamera.transform.position = this.ClubVantages[(int)this.Club].position;
		this.MainCamera.transform.rotation = this.ClubVantages[(int)this.Club].rotation;

		if (this.Club == ClubType.Cooking)
		{
			this.Cake.SetActive(true);

			// [af] Converted while loop to for loop.
			for (this.ID = 0; this.ID < this.Club1Students.Length; this.ID++)
			{
				StudentScript club1Student = this.StudentManager.Students[this.Club1Students[this.ID]];

				if (club1Student != null)
				{
					if (!club1Student.Tranquil && club1Student.Alive)
					{
						club1Student.transform.position = this.Club1ActivitySpots[this.ID].position;
						club1Student.transform.rotation = this.Club1ActivitySpots[this.ID].rotation;

						club1Student.CharacterAnimation[club1Student.SocialSitAnim].layer = 99;
						club1Student.CharacterAnimation.Play(club1Student.SocialSitAnim);
						club1Student.CharacterAnimation[club1Student.SocialSitAnim].weight = 1.0f;

						club1Student.SmartPhone.SetActive(false);
						club1Student.SpeechLines.Play();

						club1Student.ClubActivity = true;
						club1Student.Talking = false;
						club1Student.Routine = false;
						club1Student.GetComponent<AudioSource>().volume = 0.10f;
					}
				}
			}

			this.Yandere.Talking = false;
			this.Yandere.CanMove = false;
			this.Yandere.ClubActivity = true;

			this.Yandere.CharacterAnimation.Play("f02_sit_00");

			this.Yandere.transform.position = this.Club1ActivitySpots[6].position;
			this.Yandere.transform.rotation = this.Club1ActivitySpots[6].rotation;
		}
		else if (this.Club == ClubType.Drama)
		{
			//this.StudentManager.DramaPhase = 1;
			//this.StudentManager.UpdateDrama();

			// [af] Converted while loop to for loop.
			for (this.ID = 0; this.ID < this.Club2Students.Length; this.ID++)
			{
				this.StudentManager.DramaPhase = 1;
				this.StudentManager.UpdateDrama();

				StudentScript club2Student = this.StudentManager.Students[this.Club2Students[this.ID]];

				if (club2Student != null)
				{
					if (!club2Student.Tranquil && club2Student.Alive)
					{
						if (!this.StudentManager.MemorialScene.gameObject.activeInHierarchy)
						{
							club2Student.transform.position = club2Student.CurrentDestination.position;
							club2Student.transform.rotation = club2Student.CurrentDestination.rotation;
						}
						else
						{
							club2Student.transform.position = new Vector3(0, 0, 0);
						}

						club2Student.ClubActivity = true;
						club2Student.Talking = false;
						club2Student.Routine = true;
						club2Student.GetComponent<AudioSource>().volume = 0.10f;
					}
				}
			}

			this.Yandere.Talking = false;
			this.Yandere.CanMove = false;
			this.Yandere.ClubActivity = true;

			if (!this.StudentManager.MemorialScene.gameObject.activeInHierarchy)
			{
				this.Yandere.transform.position = new Vector3(42, 1.3775f, 72);
				this.Yandere.transform.eulerAngles = new Vector3(0, -90, 0);
			}
		}
		else if (this.Club == ClubType.Occult)
		{
			// [af] Converted while loop to for loop.
			for (this.ID = 0; this.ID < this.Club3Students.Length; this.ID++)
			{
				StudentScript club3Student = this.StudentManager.Students[this.Club3Students[this.ID]];

				if (club3Student != null)
				{
					if (!club3Student.Tranquil)
					{
						// [af] Added "gameObject" for C# compatibility.
						club3Student.gameObject.SetActive(false);
					}
				}
			}

			this.MainCamera.GetComponent<AudioListener>().enabled = true;

			AudioSource audioSource = this.GetComponent<AudioSource>();
			audioSource.clip = this.OccultAmbience;
			audioSource.loop = true;
			audioSource.volume = 0.0f;
			audioSource.Play();

			// [af] Added "gameObject" for C# compatibility.
			this.Yandere.gameObject.SetActive(false);

			this.Ritual.SetActive(true);

			this.CheckClub(ClubType.Occult);

			foreach (GameObject cultist in this.Cultists)
			{
				if (cultist != null)
				{
					cultist.SetActive(false);
				}
			}

			while (this.ClubMembers > 0)
			{
				this.Cultists[ClubMembers].SetActive(true);
				this.ClubMembers--;
			}

			this.CheckClub(ClubType.Occult);
		}
		else if (this.Club == ClubType.Art)
		{
			// [af] Converted while loop to for loop.
			for (this.ID = 0; this.ID < this.Club4Students.Length; this.ID++)
			{
				StudentScript club4Student = this.StudentManager.Students[this.Club4Students[this.ID]];

				if (club4Student != null)
				{
					if (!club4Student.Tranquil && club4Student.Alive)
					{
						club4Student.transform.position = this.Club4ActivitySpots[this.ID].position;
						club4Student.transform.rotation = this.Club4ActivitySpots[this.ID].rotation;
						club4Student.ClubActivity = true;
						club4Student.Talking = false;
						club4Student.Routine = true;

						if (!club4Student.ClubAttire)
						{
							club4Student.ChangeClubwear();
						}
					}
				}
			}

			this.Yandere.Talking = false;
			this.Yandere.CanMove = false;
			this.Yandere.ClubActivity = true;
			this.Yandere.transform.position = this.Club4ActivitySpots[5].position;
			this.Yandere.transform.rotation = this.Club4ActivitySpots[5].rotation;

			if (!this.Yandere.ClubAttire)
			{
				this.Yandere.ChangeClubwear();
			}
		}
		else if (this.Club == ClubType.LightMusic)
		{
			// [af] Converted while loop to for loop.
			for (this.ID = 0; this.ID < this.Club5Students.Length; this.ID++)
			{
				StudentScript club5Student = this.StudentManager.Students[this.Club5Students[this.ID]];

				if (club5Student != null)
				{
					if (!club5Student.Tranquil && club5Student.Alive)
					{
						club5Student.transform.position = club5Student.CurrentDestination.position;
						club5Student.transform.rotation = club5Student.CurrentDestination.rotation;
						club5Student.ClubActivity = false;
						club5Student.Talking = false;
						club5Student.Routine = true;
						club5Student.Stop = false;
					}
				}
			}
		}
		else if (this.Club == ClubType.MartialArts)
		{
			// [af] Converted while loop to for loop.
			for (this.ID = 0; this.ID < this.Club6Students.Length; this.ID++)
			{
				StudentScript club6Student = this.StudentManager.Students[this.Club6Students[this.ID]];

				if (club6Student != null)
				{
					if (!club6Student.Tranquil && club6Student.Alive)
					{
						club6Student.transform.position = this.Club6ActivitySpots[this.ID].position;
						club6Student.transform.rotation = this.Club6ActivitySpots[this.ID].rotation;
						club6Student.ClubActivity = true;
						club6Student.GetComponent<AudioSource>().volume = 0.10f;

						//club6Student.ClubManager.GameScreens[this.ID - 45].SetActive(true);

						if (!club6Student.ClubAttire)
						{
							club6Student.ChangeClubwear();
						}
					}
				}
			}

			this.Yandere.CanMove = false;
			this.Yandere.ClubActivity = true;
			this.Yandere.transform.position = this.Club6ActivitySpots[5].position;
			this.Yandere.transform.rotation = this.Club6ActivitySpots[5].rotation;

			if (!this.Yandere.ClubAttire)
			{
				this.Yandere.ChangeClubwear();
			}
		}
		else if (this.Club == ClubType.Photography)
		{
			// [af] Converted while loop to for loop.
			for (this.ID = 0; this.ID < this.Club7Students.Length; this.ID++)
			{
				StudentScript club7Student = this.StudentManager.Students[this.Club7Students[this.ID]];

				if (club7Student != null)
				{
					if (!club7Student.Tranquil && club7Student.Alive)
					{
						club7Student.transform.position = this.StudentManager.Clubs.List[club7Student.StudentID].position;
						club7Student.transform.rotation = this.StudentManager.Clubs.List[club7Student.StudentID].rotation;
						club7Student.CharacterAnimation[club7Student.SocialSitAnim].weight = 1.0f;
						club7Student.SmartPhone.SetActive(false);
						club7Student.ClubActivity = true;
						club7Student.SpeechLines.Play();
						club7Student.Talking = false;
						club7Student.Routine = true;
						club7Student.Hearts.Stop();
					}
				}
			}

			this.Yandere.CanMove = false;
			this.Yandere.Talking = false;
			this.Yandere.ClubActivity = true;
			this.Yandere.transform.position = this.Club7ActivitySpot.position;
			this.Yandere.transform.rotation = this.Club7ActivitySpot.rotation;

			if (!this.Yandere.ClubAttire)
			{
				this.Yandere.ChangeClubwear();
			}
		}
		else if (this.Club == ClubType.Science)
		{
			// [af] Converted while loop to for loop.
			for (this.ID = 0; this.ID < this.Club8Students.Length; this.ID++)
			{
				StudentScript club8Student = this.StudentManager.Students[this.Club8Students[this.ID]];

				if (club8Student != null)
				{
					if (!club8Student.Tranquil && club8Student.Alive)
					{
						club8Student.transform.position = this.Club8ActivitySpots[this.ID].position;
						club8Student.transform.rotation = this.Club8ActivitySpots[this.ID].rotation;
						club8Student.ClubActivity = true;
						club8Student.Talking = false;
						club8Student.Routine = true;

						if (!club8Student.ClubAttire)
						{
							club8Student.ChangeClubwear();
						}
					}
				}
			}

			this.Yandere.Talking = false;
			this.Yandere.CanMove = false;
			this.Yandere.ClubActivity = true;

			if (!this.Yandere.ClubAttire)
			{
				this.Yandere.ChangeClubwear();
			}
		}
		else if (this.Club == ClubType.Sports)
		{
			// [af] Converted while loop to for loop.
			for (this.ID = 0; this.ID < this.Club9Students.Length; this.ID++)
			{
				StudentScript club9Student = this.StudentManager.Students[this.Club9Students[this.ID]];

				if (club9Student != null)
				{
					if (!club9Student.Tranquil && club9Student.Alive)
					{
						club9Student.transform.position = club9Student.CurrentDestination.position;
						club9Student.transform.rotation = club9Student.CurrentDestination.rotation;

						club9Student.ClubActivity = true;
						club9Student.Talking = false;
						club9Student.Routine = true;
					}
				}
			}

			this.Yandere.Talking = false;
			this.Yandere.CanMove = false;
			this.Yandere.ClubActivity = true;

			this.Yandere.Schoolwear = 2;
			this.Yandere.ChangeSchoolwear();
		}
		else if (this.Club == ClubType.Gardening)
		{
			// [af] Converted while loop to for loop.
			for (this.ID = 0; this.ID < this.Club10Students.Length; this.ID++)
			{
				StudentScript club10Student = this.StudentManager.Students[this.Club10Students[this.ID]];

				if (club10Student != null)
				{
					if (!club10Student.Tranquil && club10Student.Alive)
					{
						club10Student.transform.position = club10Student.CurrentDestination.position;
						club10Student.transform.rotation = club10Student.CurrentDestination.rotation;

						club10Student.ClubActivity = true;
						club10Student.Talking = false;
						club10Student.Routine = true;
						club10Student.GetComponent<AudioSource>().volume = 0.10f;
					}
				}
			}

			this.Yandere.Talking = false;
			this.Yandere.CanMove = false;
			this.Yandere.ClubActivity = true;

			if (!this.Yandere.ClubAttire)
			{
				this.Yandere.ChangeClubwear();
			}
		}
		else if (this.Club == ClubType.Gaming)
		{
			// [af] Converted while loop to for loop.
			for (this.ID = 0; this.ID < this.Club11Students.Length; this.ID++)
			{
				StudentScript club11Student = this.StudentManager.Students[this.Club11Students[this.ID]];

				if (club11Student != null)
				{
					if (!club11Student.Tranquil && club11Student.Alive)
					{
						club11Student.transform.position = club11Student.CurrentDestination.position;
						club11Student.transform.rotation = club11Student.CurrentDestination.rotation;

						club11Student.ClubManager.GameScreens[ID].SetActive(true);
						club11Student.SmartPhone.SetActive(false);

						club11Student.ClubActivity = true;
						club11Student.Talking = false;
						club11Student.Routine = false;
						club11Student.GetComponent<AudioSource>().volume = 0.10f;
					}
				}
			}

			this.Yandere.Talking = false;
			this.Yandere.CanMove = false;
			this.Yandere.ClubActivity = true;

			this.Yandere.transform.position = this.StudentManager.ComputerGames.Chairs[1].transform.position;
			this.Yandere.transform.rotation = this.StudentManager.ComputerGames.Chairs[1].transform.rotation;
		}
		else if (this.Club == ClubType.Delinquent)
		{
			Debug.Log("Calling the Delinquent 'club activity'.");

			this.Yandere.gameObject.SetActive(false);

			for (this.ID = 0; this.ID < this.Club14Students.Length; this.ID++)
			{
				StudentScript club14Student = this.StudentManager.Students[this.Club14Students[this.ID]];

				if (club14Student != null)
				{
					if (club14Student.Alive)
					{
						Debug.Log("Telling a delinquent #" + club14Student.StudentID + " to leave school.");

						club14Student.Pathfinding.target = StudentManager.Exit;
						club14Student.CurrentDestination = StudentManager.Exit;
						club14Student.Pathfinding.canSearch = true;
						club14Student.Pathfinding.canMove = true;
						club14Student.Pathfinding.speed = 1.0f;

						club14Student.DistanceToDestination = 100;
						club14Student.Talking = false;
						club14Student.Stop = false;
					}
				}
			}
		}

		this.Clock.SetActive(false);
		this.Reputation.SetActive(false);
		this.Heartrate.SetActive(false);
		this.Watermark.SetActive(false);
	}

	public bool LeaderMissing = false;
	public bool LeaderDead = false;
	public int ClubMembers = 0;

	public int[] Club1IDs;
	public int[] Club2IDs;
	public int[] Club3IDs;
	public int[] Club4IDs;
	public int[] Club5IDs;
	public int[] Club6IDs;
	public int[] Club7IDs;
	public int[] Club8IDs;
	public int[] Club9IDs;
	public int[] Club10IDs;
	public int[] Club11IDs;
	public int[] Club14IDs;
	public int[] ClubIDs;

	public void CheckClub(ClubType Check)
	{
		if (Check == ClubType.Cooking)
		{
			this.ClubIDs = this.Club1IDs;
		}
		else if (Check == ClubType.Drama)
		{
			this.ClubIDs = this.Club2IDs;
		}
		else if (Check == ClubType.Occult)
		{
			this.ClubIDs = this.Club3IDs;
		}
		else if (Check == ClubType.Art)
		{
			this.ClubIDs = this.Club4IDs;
		}
		else if (Check == ClubType.LightMusic)
		{
			this.ClubIDs = this.Club5IDs;
		}
		else if (Check == ClubType.MartialArts)
		{
			this.ClubIDs = this.Club6IDs;
		}
		else if (Check == ClubType.Photography)
		{
			this.ClubIDs = this.Club7IDs;
		}
		else if (Check == ClubType.Science)
		{
			this.ClubIDs = this.Club8IDs;
		}
		else if (Check == ClubType.Sports)
		{
			this.ClubIDs = this.Club9IDs;
		}
		else if (Check == ClubType.Gardening)
		{
			this.ClubIDs = this.Club10IDs;
		}
		else if (Check == ClubType.Gaming)
		{
			this.ClubIDs = this.Club11IDs;
		}

		this.LeaderMissing = false;
		this.LeaderDead = false;
		this.ClubMembers = 0;

		// [af] Converted while loop to for loop.
		for (this.ID = 1; this.ID < this.ClubIDs.Length; this.ID++)
		{
			if (!StudentGlobals.GetStudentDead(this.ClubIDs[this.ID]) &&
				!StudentGlobals.GetStudentDying(this.ClubIDs[this.ID]) &&
				!StudentGlobals.GetStudentKidnapped(this.ClubIDs[this.ID]) &&
				!StudentGlobals.GetStudentArrested(this.ClubIDs[this.ID]) &&
				!StudentGlobals.GetStudentExpelled(this.ClubIDs[this.ID]) &&
				(StudentGlobals.GetStudentReputation(this.ClubIDs[this.ID]) > -100))
			{
				this.ClubMembers++;
			}
		}

		if (TranqCase.VictimClubType == Check)
		{
			this.ClubMembers--;
		}

		if (Check == ClubType.LightMusic)
		{
			if (this.ClubMembers < 5)
			{
				this.LeaderAshamed = true;
			}
		}

		if (this.Yandere.Club == Check)
		{
			this.ClubMembers++;
		}

		if (Check == ClubType.Cooking)
		{
			int studentID21 = 21;

			if (StudentGlobals.GetStudentDead(studentID21) ||
				StudentGlobals.GetStudentDying(studentID21) ||
				StudentGlobals.GetStudentArrested(studentID21) ||
				(StudentGlobals.GetStudentReputation(studentID21) <= -100))
			{
				this.LeaderDead = true;
			}

			if (StudentGlobals.GetStudentMissing(studentID21) ||
				StudentGlobals.GetStudentKidnapped(studentID21) ||
				(this.TranqCase.VictimID == studentID21))
			{
				this.LeaderMissing = true;
			}
		}
		else if (Check == ClubType.Drama)
		{
			int studentID26 = 26;

			if (StudentGlobals.GetStudentDead(studentID26) ||
				StudentGlobals.GetStudentDying(studentID26) ||
				StudentGlobals.GetStudentArrested(studentID26) ||
				(StudentGlobals.GetStudentReputation(studentID26) <= -100))
			{
				this.LeaderDead = true;
			}

			if (StudentGlobals.GetStudentMissing(studentID26) ||
				StudentGlobals.GetStudentKidnapped(studentID26) ||
				(this.TranqCase.VictimID == studentID26))
			{
				this.LeaderMissing = true;
			}
		}
		else if (Check == ClubType.Occult)
		{
			int studentID31 = 31;

			if (StudentGlobals.GetStudentDead(studentID31) ||
				StudentGlobals.GetStudentDying(studentID31) ||
				StudentGlobals.GetStudentArrested(studentID31) ||
				(StudentGlobals.GetStudentReputation(studentID31) <= -100))
			{
				this.LeaderDead = true;
			}

			if (StudentGlobals.GetStudentMissing(studentID31) ||
				StudentGlobals.GetStudentKidnapped(studentID31) ||
				(this.TranqCase.VictimID == studentID31))
			{
				this.LeaderMissing = true;
			}
		}
		else if (Check == ClubType.Gaming)
		{
			int studentID36 = 36;

			if (StudentGlobals.GetStudentDead(studentID36) ||
				StudentGlobals.GetStudentDying(studentID36) ||
				StudentGlobals.GetStudentArrested(studentID36) ||
				(StudentGlobals.GetStudentReputation(studentID36) <= -100))
			{
				this.LeaderDead = true;
			}

			if (StudentGlobals.GetStudentMissing(studentID36) ||
				StudentGlobals.GetStudentKidnapped(studentID36) ||
				(this.TranqCase.VictimID == studentID36))
			{
				this.LeaderMissing = true;
			}
		}
		else if (Check == ClubType.Art)
		{
			int studentID41 = 41;

			if (StudentGlobals.GetStudentDead(studentID41) ||
				StudentGlobals.GetStudentDying(studentID41) ||
				StudentGlobals.GetStudentArrested(studentID41) ||
				(StudentGlobals.GetStudentReputation(studentID41) <= -100))
			{
				this.LeaderDead = true;
			}

			if (StudentGlobals.GetStudentMissing(studentID41) ||
				StudentGlobals.GetStudentKidnapped(studentID41) ||
				(this.TranqCase.VictimID == studentID41))
			{
				this.LeaderMissing = true;
			}
		}
		else if (Check == ClubType.MartialArts)
		{
			int studentID46 = 46;

			if (StudentGlobals.GetStudentDead(studentID46) ||
				StudentGlobals.GetStudentDying(studentID46) ||
				StudentGlobals.GetStudentArrested(studentID46) ||
				(StudentGlobals.GetStudentReputation(studentID46) <= -100))
			{
				this.LeaderDead = true;
			}

			if (StudentGlobals.GetStudentMissing(studentID46) ||
				StudentGlobals.GetStudentKidnapped(studentID46) ||
				(this.TranqCase.VictimID == studentID46))
			{
				this.LeaderMissing = true;
			}
		}
		else if (Check == ClubType.LightMusic)
		{
			int studentID51 = 51;

			if (StudentGlobals.GetStudentDead(studentID51) ||
				StudentGlobals.GetStudentDying(studentID51) ||
				StudentGlobals.GetStudentArrested(studentID51) ||
				(StudentGlobals.GetStudentReputation(studentID51) <= -100))
			{
				this.LeaderDead = true;
			}

			if (StudentGlobals.GetStudentMissing(studentID51) ||
				StudentGlobals.GetStudentKidnapped(studentID51) ||
				(this.TranqCase.VictimID == studentID51))
			{
				this.LeaderMissing = true;
			}
		}
		else if (Check == ClubType.Photography)
		{
			int studentID56 = 56;

			if (StudentGlobals.GetStudentDead(studentID56) ||
				StudentGlobals.GetStudentDying(studentID56) ||
				StudentGlobals.GetStudentArrested(studentID56) ||
				(StudentGlobals.GetStudentReputation(studentID56) <= -100))
			{
				this.LeaderDead = true;
			}

			if (StudentGlobals.GetStudentMissing(studentID56) ||
				StudentGlobals.GetStudentKidnapped(studentID56) ||
				(this.TranqCase.VictimID == studentID56))
			{
				this.LeaderMissing = true;
			}
		}
		else if (Check == ClubType.Science)
		{
			int studentID61 = 61;

			if (StudentGlobals.GetStudentDead(studentID61) ||
				StudentGlobals.GetStudentDying(studentID61) ||
				StudentGlobals.GetStudentArrested(studentID61) ||
				(StudentGlobals.GetStudentReputation(studentID61) <= -100))
			{
				this.LeaderDead = true;
			}

			if (StudentGlobals.GetStudentMissing(studentID61) ||
				StudentGlobals.GetStudentKidnapped(studentID61) ||
				(this.TranqCase.VictimID == studentID61))
			{
				this.LeaderMissing = true;
			}
		}
		else if (Check == ClubType.Sports)
		{
			int studentID66 = 66;

			if (StudentGlobals.GetStudentDead(studentID66) ||
				StudentGlobals.GetStudentDying(studentID66) ||
				StudentGlobals.GetStudentArrested(studentID66) ||
				(StudentGlobals.GetStudentReputation(studentID66) <= -100))
			{
				this.LeaderDead = true;
			}

			if (StudentGlobals.GetStudentMissing(studentID66) ||
				StudentGlobals.GetStudentKidnapped(studentID66) ||
				(this.TranqCase.VictimID == studentID66))
			{
				this.LeaderMissing = true;
			}
		}
		else if (Check == ClubType.Gardening)
		{
			int studentID71 = 71;

			if (StudentGlobals.GetStudentDead(studentID71) ||
				StudentGlobals.GetStudentDying(studentID71) ||
				StudentGlobals.GetStudentArrested(studentID71) ||
				(StudentGlobals.GetStudentReputation(studentID71) <= -100))
			{
				this.LeaderDead = true;
			}

			if (StudentGlobals.GetStudentMissing(studentID71) ||
				StudentGlobals.GetStudentKidnapped(studentID71) ||
				(this.TranqCase.VictimID == studentID71))
			{
				this.LeaderMissing = true;
			}
		}

		if (!this.LeaderDead && !this.LeaderMissing)
		{
			if (Check == ClubType.LightMusic)
			{
				if (StudentGlobals.GetStudentReputation(51) < -33.33333)
				{
					this.LeaderAshamed = true;
				}
			}
		}
	}

	public bool LeaderGrudge = false;
	public bool ClubGrudge = false;

	public void CheckGrudge(ClubType Check)
	{
		if (Check == ClubType.Cooking)
		{
			this.ClubIDs = this.Club1IDs;
		}
		else if (Check == ClubType.Drama)
		{
			this.ClubIDs = this.Club2IDs;
		}
		else if (Check == ClubType.Occult)
		{
			this.ClubIDs = this.Club3IDs;
		}
		else if (Check == ClubType.LightMusic)
		{
			this.ClubIDs = this.Club5IDs;
		}
		else if (Check == ClubType.MartialArts)
		{
			this.ClubIDs = this.Club6IDs;
		}
		else if (Check == ClubType.Photography)
		{
			this.ClubIDs = this.Club7IDs;
		}
		else if (Check == ClubType.Science)
		{
			this.ClubIDs = this.Club8IDs;
		}
		else if (Check == ClubType.Sports)
		{
			this.ClubIDs = this.Club9IDs;
		}
		else if (Check == ClubType.Gardening)
		{
			this.ClubIDs = this.Club10IDs;
		}
		else if (Check == ClubType.Gaming)
		{
			this.ClubIDs = this.Club11IDs;
		}

		this.LeaderGrudge = false;
		this.ClubGrudge = false;

		// [af] Converted while loop to for loop.
		for (this.ID = 1; this.ID < this.ClubIDs.Length; this.ID++)
		{
			if (this.StudentManager.Students[this.ClubIDs[this.ID]] != null)
			{
				if (StudentGlobals.GetStudentGrudge(this.ClubIDs[this.ID]))
				{
					this.ClubGrudge = true;
				}
			}
		}

		if (Check == ClubType.Cooking)
		{
			if (this.StudentManager.Students[21].Grudge)
			{
				this.LeaderGrudge = true;
			}
		}
		else if (Check == ClubType.Drama)
		{
			if (this.StudentManager.Students[26].Grudge)
			{
				this.LeaderGrudge = true;
			}
		}
		else if (Check == ClubType.Occult)
		{
			if (this.StudentManager.Students[31].Grudge)
			{
				this.LeaderGrudge = true;
			}
		}
		else if (Check == ClubType.Art)
		{
			if (this.StudentManager.Students[41].Grudge)
			{
				this.LeaderGrudge = true;
			}
		}
		else if (Check == ClubType.MartialArts)
		{
			if (this.StudentManager.Students[46].Grudge)
			{
				this.LeaderGrudge = true;
			}
		}
		else if (Check == ClubType.LightMusic)
		{
			if (this.StudentManager.Students[51].Grudge)
			{
				this.LeaderGrudge = true;
			}
		}
		else if (Check == ClubType.Photography)
		{
			if (this.StudentManager.Students[56].Grudge)
			{
				this.LeaderGrudge = true;
			}
		}
		else if (Check == ClubType.Science)
		{
			if (this.StudentManager.Students[61].Grudge)
			{
				this.LeaderGrudge = true;
			}
		}
		else if (Check == ClubType.Sports)
		{
			if (this.StudentManager.Students[66].Grudge)
			{
				this.LeaderGrudge = true;
			}
		}
		else if (Check == ClubType.Gardening)
		{
			if (this.StudentManager.Students[71].Grudge)
			{
				this.LeaderGrudge = true;
			}
		}
		else if (Check == ClubType.Gaming)
		{
			if (this.StudentManager.Students[36].Grudge)
			{
				this.LeaderGrudge = true;
			}
		}
	}

	public void ActivateClubBenefit()
	{
		// Cooking.
		if (Yandere.Club == ClubType.Cooking)
		{
			if (!this.Refrigerator.CookingEvent.EventActive)
			{
				this.Refrigerator.enabled = true;
				this.Refrigerator.Prompt.enabled = true;
			}
		}
		// Drama.
		else if (Yandere.Club == ClubType.Drama)
		{
			// [af] Converted while loop to for loop.
			for (this.ID = 1; this.ID < this.Masks.Length; this.ID++)
			{
				this.Masks[this.ID].enabled = true;
				this.Masks[this.ID].Prompt.enabled = true;
			}

			this.Gloves.enabled = true;
			this.Gloves.Prompt.enabled = true;
		}
		// Occult.
		else if (Yandere.Club == ClubType.Occult)
		{
			this.StudentManager.UpdatePerception();
			this.Yandere.Numbness -= 0.50f;
		}
		// Art.
		else if (Yandere.Club == ClubType.Art)
		{
			this.StudentManager.UpdateBooths();
		}
		// Light Music Club.
		else if (Yandere.Club == ClubType.LightMusic)
		{
			this.Container.enabled = true;
			this.Container.Prompt.enabled = true;
		}
		// Martial Arts Club.
		else if (Yandere.Club == ClubType.MartialArts)
		{
			this.StudentManager.UpdateBooths();
		}
		// Photography Club.
		else if (Yandere.Club == ClubType.Photography)
		{
			// Photography handles itself.
		}
		// Science Club.
		else if (Yandere.Club == ClubType.Science)
		{
			this.BloodCleaner.Prompt.enabled = true;
			this.StudentManager.UpdateBooths();
		}
		// Sports Club.
		else if (Yandere.Club == ClubType.Sports)
		{
			this.Yandere.RunSpeed++;

			if (this.Yandere.Armed)
			{
				this.Yandere.EquippedWeapon.SuspicionCheck();
			}
		}
		// Gardening Club.
		else if (Yandere.Club == ClubType.Gardening)
		{
			this.ShedDoor.Prompt.Label[0].text = "     " + "Open";
			this.Padlock.SetActive(false);
			this.ShedDoor.Locked = false;

			if (this.Yandere.Armed)
			{
				this.Yandere.EquippedWeapon.SuspicionCheck();
			}
		}
		// Gaming Club.
		else if (Yandere.Club == ClubType.Gaming)
		{
			this.ComputerGames.EnableGames();
		}
	}

	public void DeactivateClubBenefit()
	{
		// Cooking.
		if (Yandere.Club == ClubType.Cooking)
		{
			this.Refrigerator.enabled = false;
			this.Refrigerator.Prompt.Hide();
			this.Refrigerator.Prompt.enabled = false;
		}
		// Drama.
		else if (Yandere.Club == ClubType.Drama)
		{
			// [af] Converted while loop to for loop.
			for (this.ID = 1; this.ID < this.Masks.Length; this.ID++)
			{
				if (this.Masks[this.ID] != null)
				{
					this.Masks[this.ID].enabled = false;
					this.Masks[this.ID].Prompt.Hide();
					this.Masks[this.ID].Prompt.enabled = false;
				}
			}

			this.Gloves.enabled = false;
			this.Gloves.Prompt.Hide();
			this.Gloves.Prompt.enabled = false;
		}
		// Occult.
		else if (Yandere.Club == ClubType.Occult)
		{
            Yandere.Club = ClubType.None;
			this.StudentManager.UpdatePerception();
			this.Yandere.Numbness += 0.50f;
		}
		// Art.
		else if (Yandere.Club == ClubType.Art)
		{
			//Just updating the booth, which is done in the ClubWindowScript.
		}
		// Light Music Club.
		else if (Yandere.Club == ClubType.LightMusic)
		{
			this.Container.enabled = false;
			this.Container.Prompt.Hide();
			this.Container.Prompt.enabled = false;
		}
		// Martial Arts Club.
		else if (Yandere.Club == ClubType.MartialArts)
		{
			//Just updating the booth, which is done in the ClubWindowScript.
		}
		// Photography Club.
		else if (Yandere.Club == ClubType.Photography)
		{
			// Photography handles itself.
		}
		// Science Club.
		else if (Yandere.Club == ClubType.Science)
		{
			this.BloodCleaner.enabled = false;
			this.BloodCleaner.Prompt.Hide();
			this.BloodCleaner.Prompt.enabled = false;
		}
		// Sports.
		else if (Yandere.Club == ClubType.Sports)
		{
			this.Yandere.RunSpeed--;

			if (this.Yandere.Armed)
			{
                Yandere.Club = ClubType.None;
				this.Yandere.EquippedWeapon.SuspicionCheck();
			}
		}
		// Gardening Club.
		else if (Yandere.Club == ClubType.Gardening)
		{
			if (!this.Yandere.Inventory.ShedKey)
			{
				this.ShedDoor.Prompt.Label[0].text = "     " + "Locked";
				this.Padlock.SetActive(true);
				this.ShedDoor.Locked = true;
				this.ShedDoor.CloseDoor();
			}

			if (this.Yandere.Armed)
			{
                Yandere.Club = ClubType.None;
				this.Yandere.EquippedWeapon.SuspicionCheck();
			}
		}
		// Gaming Club.
		else if (Yandere.Club == ClubType.Gaming)
		{
			this.ComputerGames.DeactivateAllBenefits();
			this.ComputerGames.DisableGames();
		}
	}

	public void UpdateMasks()
	{
		bool yandereHasMask = this.Yandere.Mask != null;

		// [af] Converted while loops to a single for loop.
		for (this.ID = 1; this.ID < this.Masks.Length; this.ID++)
		{
			this.Masks[this.ID].Prompt.HideButton[0] = yandereHasMask;
		}
	}
}