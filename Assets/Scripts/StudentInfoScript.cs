using System.IO;
using UnityEngine;

public class StudentInfoScript : MonoBehaviour
{
	public StudentInfoMenuScript StudentInfoMenu;
	public StudentManagerScript StudentManager;
	public DialogueWheelScript DialogueWheel;
	public HomeInternetScript HomeInternet;
	public TopicManagerScript TopicManager;
	public NoteLockerScript NoteLocker;
	public RadarChart ReputationChart;
	public PromptBarScript PromptBar;
	public ShutterScript Shutter;
	public YandereScript Yandere;
	public JsonScript JSON;

	public Texture GuidanceCounselor;
	public Texture DefaultPortrait;
	public Texture BlankPortrait;
	public Texture Headmaster;
	public Texture InfoChan;

	public Transform ReputationBar;

	public GameObject Static;
	public GameObject Topics;

	public UILabel OccupationLabel;
	public UILabel ReputationLabel;
    public UILabel RealNameLabel;
    public UILabel StrengthLabel;
	public UILabel PersonaLabel;
	public UILabel ClassLabel;
	public UILabel CrushLabel;
	public UILabel ClubLabel;
	public UILabel InfoLabel;
	public UILabel NameLabel;

	public UITexture Portrait;

	public string[] OpinionSpriteNames;
	public string[] Strings;

	public int CurrentStudent = 0;

	public bool ShowRep = false;
	public bool Back = false;

	public UISprite[] TopicIcons;
	public UISprite[] TopicOpinionIcons;

	static readonly IntAndStringDictionary StrengthStrings = 
		new IntAndStringDictionary
	{
		{ 0, "Incapable" },
		{ 1, "Very Weak" },
		{ 2, "Weak" },
		{ 3, "Strong" },
		{ 4, "Very Strong" },
		{ 5, "Peak Physical Strength" },
		{ 6, "Extensive Training" },
		{ 7, "Carries Pepper Spray" },
		{ 8, "Armed" },
		{ 9, "Invincible" },
		{ 99, "?????" }
	};

	void Start()
	{
		StudentGlobals.SetStudentPhotographed(98, true);
		StudentGlobals.SetStudentPhotographed(99, true);
		StudentGlobals.SetStudentPhotographed(100, true);

		this.Topics.SetActive(false);
	}

	public void UpdateInfo(int ID)
	{
		// [af] Get a reference to the student's JSON data.
		StudentJson studentJson = this.JSON.Students[ID];

		//////////////////////
		///// NAME LABEL /////
		//////////////////////

        if (studentJson.RealName == "")
        {
            this.NameLabel.transform.localPosition = new Vector3(-228, 195, 0);
            this.RealNameLabel.text = "";
        }
        else
        {
            this.NameLabel.transform.localPosition = new Vector3(-228, 210, 0);
            this.RealNameLabel.text = "Real Name: " + studentJson.RealName;
        }

        this.NameLabel.text = studentJson.Name;

        ///////////////////////
        ///// CLASS LABEL /////
        ///////////////////////

        string Text = "" + studentJson.Class;
		Text = Text.Insert(1,"-");

		this.ClassLabel.text = "Class " + Text;

		if (ID == 90 || ID > 96)
		{
			this.ClassLabel.text = "";
		}

		//////////////////////
		///// REPUTATION /////
		//////////////////////

		if (StudentGlobals.GetStudentReputation(ID) < 0)
		{
			this.ReputationLabel.text = StudentGlobals.GetStudentReputation(ID).ToString();
		}
		else if (StudentGlobals.GetStudentReputation(ID) > 0)
		{
			this.ReputationLabel.text = "+" + StudentGlobals.GetStudentReputation(ID).ToString();
		}
		else
		{
			this.ReputationLabel.text = "0";
		}

		this.ReputationBar.localPosition = new Vector3(
			StudentGlobals.GetStudentReputation(ID) * 0.96f,
			this.ReputationBar.localPosition.y,
			this.ReputationBar.localPosition.z);

		if (this.ReputationBar.localPosition.x > 96.0f)
		{
			this.ReputationBar.localPosition = new Vector3(
				96.0f,
				this.ReputationBar.localPosition.y,
				this.ReputationBar.localPosition.z);
		}

		if (this.ReputationBar.localPosition.x < -96.0f)
		{
			this.ReputationBar.localPosition = new Vector3(
				-96.0f,
				this.ReputationBar.localPosition.y,
				this.ReputationBar.localPosition.z);
		}

		/////////////////////////////
		///// PERSONALITY LABEL /////
		/////////////////////////////

		this.PersonaLabel.text = Persona.PersonaNames[studentJson.Persona];

		if ((studentJson.Persona == PersonaType.Strict) && 
			(studentJson.Club == ClubType.GymTeacher) &&
			(!StudentGlobals.GetStudentReplaced(ID)))
		{
			this.PersonaLabel.text = "Friendly but Strict";
		}

		///////////////////////
		///// CRUSH LABEL /////
		///////////////////////

		if (studentJson.Crush == 0)
		{
			this.CrushLabel.text = "None";
		}
		else if (studentJson.Crush == 99)
		{
			this.CrushLabel.text = "?????";
		}
		else
		{
			this.CrushLabel.text = this.JSON.Students[studentJson.Crush].Name;
		}

		////////////////////////////
		///// OCCUPATION LABEL /////
		////////////////////////////

		if (studentJson.Club < ClubType.Teacher)
		{
			this.OccupationLabel.text = "Club";
		}
		else
		{
			this.OccupationLabel.text = "Occupation";
		}

		//////////////////////
		///// CLUB LABEL /////
		//////////////////////

		if (studentJson.Club < ClubType.Teacher)
		{
			this.ClubLabel.text = Club.ClubNames[studentJson.Club];
		}
		else
		{
			this.ClubLabel.text = Club.TeacherClubNames[studentJson.Class];
		}

		if (ClubGlobals.GetClubClosed(studentJson.Club))
		{
			this.ClubLabel.text = "No Club";
		}

		//////////////////////////
		///// STRENGTH LABEL /////
		//////////////////////////

		this.StrengthLabel.text = StrengthStrings[studentJson.Strength];

		////////////////////
		///// PORTRAIT /////
		////////////////////

		AudioSource audioSource = this.GetComponent<AudioSource>();

		audioSource.enabled = false;
		this.Static.SetActive(false);
		audioSource.volume = 0.0f;
		audioSource.Stop();

		if (ID < 12 || ID > 20 && ID < 98)
		{
			string path = "file:///" + Application.streamingAssetsPath +
				"/Portraits/Student_" + ID.ToString() + ".png";

			WWW www = new WWW(path);

			if (!StudentGlobals.GetStudentReplaced(ID))
			{
				this.Portrait.mainTexture = www.texture;
			}
			else
			{
				this.Portrait.mainTexture = this.BlankPortrait;
			}
		}
		else
		{
			if (ID == 98)
			{
				this.Portrait.mainTexture = this.GuidanceCounselor;
			}
			else if (ID == 99)
			{
				this.Portrait.mainTexture = this.Headmaster;
			}
			else if (ID == 100)
			{
				this.Portrait.mainTexture = this.InfoChan;
				this.Static.SetActive(true);

				if (!this.StudentInfoMenu.Gossiping && !this.StudentInfoMenu.Distracting &&
					!this.StudentInfoMenu.CyberBullying && !this.StudentInfoMenu.CyberStalking)
				{
					audioSource.enabled = true;
					audioSource.volume = 1.0f;
					audioSource.Play();
				}
			}
			else
			{
				this.Portrait.mainTexture = this.StudentInfoMenu.RivalPortraits[ID];
			}
		}

		this.UpdateAdditionalInfo(ID);

		this.CurrentStudent = ID;

		this.UpdateRepChart();
	}

	void Update()
	{
		if (this.CurrentStudent == 100)
		{
			this.UpdateRepChart();
		}

		if (Input.GetButtonDown(InputNames.Xbox_A))
		{
			if (this.StudentInfoMenu.Gossiping)
			{
				this.StudentInfoMenu.PauseScreen.MainMenu.SetActive(true);
				this.StudentInfoMenu.PauseScreen.Show = false;

				this.DialogueWheel.Victim = this.CurrentStudent;
				this.StudentInfoMenu.Gossiping = false;
				this.gameObject.SetActive(false);
				Time.timeScale = 1.0f;

				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
			}
			else if (this.StudentInfoMenu.Distracting)
			{
				this.StudentInfoMenu.PauseScreen.MainMenu.SetActive(true);
				this.StudentInfoMenu.PauseScreen.Show = false;

				this.DialogueWheel.Victim = this.CurrentStudent;
				this.StudentInfoMenu.Distracting = false;
				this.gameObject.SetActive(false);
				Time.timeScale = 1.0f;

				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
			}
			else if (this.StudentInfoMenu.CyberBullying)
			{
				this.HomeInternet.PostLabels[1].text = this.JSON.Students[this.CurrentStudent].Name;
				this.HomeInternet.Student = this.CurrentStudent;

				this.StudentInfoMenu.PauseScreen.MainMenu.SetActive(true);
				this.StudentInfoMenu.PauseScreen.Show = false;

				this.StudentInfoMenu.CyberBullying = false;
				this.gameObject.SetActive(false);

				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
			}
			else if (this.StudentInfoMenu.CyberStalking)
			{
				this.HomeInternet.HomeCamera.CyberstalkWindow.SetActive(true);

				this.HomeInternet.Student = this.CurrentStudent;

				this.StudentInfoMenu.PauseScreen.MainMenu.SetActive(true);
				this.StudentInfoMenu.PauseScreen.Show = false;

				this.StudentInfoMenu.CyberStalking = false;
				this.gameObject.SetActive(false);

				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
			}
			else if (this.StudentInfoMenu.MatchMaking)
			{
				this.StudentInfoMenu.PauseScreen.MainMenu.SetActive(true);
				this.StudentInfoMenu.PauseScreen.Show = false;

				this.DialogueWheel.Victim = this.CurrentStudent;
				this.StudentInfoMenu.MatchMaking = false;
				this.gameObject.SetActive(false);
				Time.timeScale = 1.0f;

				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
			}
			else if (this.StudentInfoMenu.Targeting)
			{
				this.StudentInfoMenu.PauseScreen.MainMenu.SetActive(true);
				this.StudentInfoMenu.PauseScreen.Show = false;

				this.Yandere.TargetStudent.HuntTarget = this.StudentManager.Students[this.CurrentStudent];
				this.Yandere.TargetStudent.HuntTarget.Hunted = true;
				this.Yandere.TargetStudent.GoCommitMurder();

				this.Yandere.RPGCamera.enabled = true;
				this.Yandere.TargetStudent = null;

				this.StudentInfoMenu.Targeting = false;
				this.gameObject.SetActive(false);
				Time.timeScale = 1.0f;

				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
			}
			else if (this.StudentInfoMenu.SendingHome)
			{
				if (this.CurrentStudent == 10)
				{
					this.StudentInfoMenu.PauseScreen.ServiceMenu.TextMessageManager.SpawnMessage(10);

					this.Yandere.Inventory.PantyShots += this.Yandere.PauseScreen.ServiceMenu.ServiceCosts[8];

					this.gameObject.SetActive(false);

					this.PromptBar.ClearButtons();
					this.PromptBar.Label[0].text = string.Empty;
					this.PromptBar.Label[1].text = "Back";
					this.PromptBar.UpdateButtons();
				}
				else
				{
					if (this.StudentManager.Students[this.CurrentStudent].Routine &&
						!this.StudentManager.Students[this.CurrentStudent].InEvent &&
						!this.StudentManager.Students[this.CurrentStudent].TargetedForDistraction &&
						this.StudentManager.Students[this.CurrentStudent].ClubActivityPhase < 16 &&
						!this.StudentManager.Students[this.CurrentStudent].MyBento.Tampered)
					{
						this.StudentManager.Students[this.CurrentStudent].Routine = false;
						this.StudentManager.Students[this.CurrentStudent].SentHome = true;
						this.StudentManager.Students[this.CurrentStudent].CameraReacting = false;
						this.StudentManager.Students[this.CurrentStudent].SpeechLines.Stop();
						this.StudentManager.Students[this.CurrentStudent].EmptyHands();

						this.StudentInfoMenu.PauseScreen.ServiceMenu.gameObject.SetActive(true);
						this.StudentInfoMenu.PauseScreen.ServiceMenu.UpdateList();
						this.StudentInfoMenu.PauseScreen.ServiceMenu.UpdateDesc();
						this.StudentInfoMenu.PauseScreen.ServiceMenu.Purchase();
						this.StudentInfoMenu.SendingHome = false;

						this.gameObject.SetActive(false);

						this.PromptBar.ClearButtons();
						this.PromptBar.Show = false;
					}
					else
					{
						this.StudentInfoMenu.PauseScreen.ServiceMenu.TextMessageManager.SpawnMessage(0);

						this.gameObject.SetActive(false);

						this.PromptBar.ClearButtons();
						this.PromptBar.Label[0].text = string.Empty;
						this.PromptBar.Label[1].text = "Back";
						this.PromptBar.UpdateButtons();
					}
				}
			}
			else if (this.StudentInfoMenu.FindingLocker)
			{
				this.NoteLocker.gameObject.SetActive(true);
				this.NoteLocker.transform.position = this.StudentManager.Students[this.StudentInfoMenu.StudentID].MyLocker.position;
				this.NoteLocker.transform.position += new Vector3(0, 1.355f, 0);
				this.NoteLocker.transform.position += this.StudentManager.Students[this.StudentInfoMenu.StudentID].MyLocker.forward * .33333f;
				this.NoteLocker.Prompt.Label[0].text = "     " + "Leave note for " + this.StudentManager.Students[this.StudentInfoMenu.StudentID].Name;

				this.NoteLocker.Student = this.StudentManager.Students[this.StudentInfoMenu.StudentID];
				this.NoteLocker.LockerOwner = this.StudentInfoMenu.StudentID;
				this.NoteLocker.Prompt.enabled = true;

				this.NoteLocker.transform.GetChild(0).gameObject.SetActive(true);
				this.NoteLocker.CheckingNote = false;
				this.NoteLocker.CanLeaveNote = true;
				this.NoteLocker.SpawnedNote = false;
				this.NoteLocker.NoteLeft = false;
				this.NoteLocker.Success = false;
				this.NoteLocker.Timer = 0;

				this.StudentInfoMenu.PauseScreen.MainMenu.SetActive(true);
				this.StudentInfoMenu.PauseScreen.Show = false;

				this.StudentInfoMenu.FindingLocker = false;
				this.gameObject.SetActive(false);

				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;

				this.Yandere.RPGCamera.enabled = true;
				Time.timeScale = 1.0f;
			}
            else if (this.StudentInfoMenu.FiringCouncilMember)
            {
                if (this.StudentManager.Students[this.CurrentStudent].Routine &&
                    !this.StudentManager.Students[this.CurrentStudent].InEvent &&
                    !this.StudentManager.Students[this.CurrentStudent].TargetedForDistraction &&
                    this.StudentManager.Students[this.CurrentStudent].ClubActivityPhase < 16 &&
                    !this.StudentManager.Students[this.CurrentStudent].MyBento.Tampered)
                {
                    this.StudentManager.Students[this.CurrentStudent].OriginalPersona = PersonaType.Heroic;
                    this.StudentManager.Students[this.CurrentStudent].Persona = PersonaType.Heroic;
                    this.StudentManager.Students[this.CurrentStudent].Club = ClubType.None;
                    this.StudentManager.Students[this.CurrentStudent].CameraReacting = false;
                    this.StudentManager.Students[this.CurrentStudent].SpeechLines.Stop();
                    this.StudentManager.Students[this.CurrentStudent].EmptyHands();

                    //StudentScript student = this.StudentManager.Students[this.CurrentStudent];

                    this.StudentManager.Students[this.CurrentStudent].IdleAnim = this.StudentManager.Students[this.CurrentStudent].BulliedIdleAnim;
                    this.StudentManager.Students[this.CurrentStudent].WalkAnim = this.StudentManager.Students[this.CurrentStudent].BulliedWalkAnim;
                    this.StudentManager.Students[this.CurrentStudent].Armband.SetActive(false);

                    StudentScript student = this.StudentManager.Students[this.CurrentStudent];

                    ScheduleBlock newBlock3 = student.ScheduleBlocks[3];
                    newBlock3.destination = "LunchSpot";
                    newBlock3.action = "Eat";

                    student.GetDestinations();

                    student.CurrentDestination = student.Destinations[student.Phase];
                    student.Pathfinding.target = student.Destinations[student.Phase];

                    this.StudentInfoMenu.PauseScreen.ServiceMenu.gameObject.SetActive(true);
                    this.StudentInfoMenu.PauseScreen.ServiceMenu.UpdateList();
                    this.StudentInfoMenu.PauseScreen.ServiceMenu.UpdateDesc();
                    this.StudentInfoMenu.PauseScreen.ServiceMenu.Purchase();
                    this.StudentInfoMenu.FiringCouncilMember = false;

                    this.StudentInfoMenu.PauseScreen.ServiceMenu.TextMessageManager.SpawnMessage(9);
                }
                else
                {
                    this.StudentInfoMenu.PauseScreen.ServiceMenu.TextMessageManager.SpawnMessage(0);
                }

                this.gameObject.SetActive(false);

                this.PromptBar.ClearButtons();
                this.PromptBar.Label[0].text = string.Empty;
                this.PromptBar.Label[1].text = "Back";
                this.PromptBar.UpdateButtons();
            }
        }

		if (Input.GetButtonDown(InputNames.Xbox_B))
		{
			this.ShowRep = false;
			this.Topics.SetActive(false);
			this.GetComponent<AudioSource>().Stop();
			this.ReputationChart.transform.localScale = new Vector3(0, 0, 0);

			if (this.Shutter != null)
			{
				if (!this.Shutter.PhotoIcons.activeInHierarchy)
				{
					this.Back = true;
				}
			}
			else
			{
				this.Back = true;
			}

			if (this.Back)
			{
				this.StudentInfoMenu.gameObject.SetActive(true);
				this.gameObject.SetActive(false);

				this.PromptBar.ClearButtons();
				this.PromptBar.Label[0].text = "View Info";

				// [af] Changed typo from "Gossipping" to "Gossiping".
				if (!this.StudentInfoMenu.Gossiping)
				{
					this.PromptBar.Label[1].text = "Back";
				}

				this.PromptBar.UpdateButtons();

				this.Back = false;
			}
		}

		if (Input.GetButtonDown(InputNames.Xbox_X))
		{
			if (this.PromptBar.Button[2].enabled)
			{
				if (this.StudentManager.Tag.Target != this.StudentManager.Students[this.CurrentStudent].Head)
				{
					this.StudentManager.Tag.Target = this.StudentManager.Students[this.CurrentStudent].Head;

					this.PromptBar.Label[2].text = "Untag";
				}
				else
				{
					this.StudentManager.Tag.Target = null;

					this.PromptBar.Label[2].text = "Tag";
				}
			}
		}

		if (Input.GetButtonDown(InputNames.Xbox_Y))
		{
			if (this.PromptBar.Button[3].enabled)
			{
				if (!this.Topics.activeInHierarchy)
				{
					this.PromptBar.Label[3].text = "Basic Info";
					this.PromptBar.UpdateButtons();

					this.Topics.SetActive(true);
					this.UpdateTopics();
				}
				else
				{
					this.PromptBar.Label[3].text = "Interests";
					this.PromptBar.UpdateButtons();

					this.Topics.SetActive(false);
				}
			}
		}

		if (Input.GetButtonDown(InputNames.Xbox_LB))
		{
			this.UpdateRepChart();

			this.ShowRep = !this.ShowRep;
		}

		if (Input.GetKeyDown(KeyCode.Equals))
		{
			StudentGlobals.SetStudentReputation(this.CurrentStudent,
				StudentGlobals.GetStudentReputation(this.CurrentStudent) + 10);

			this.UpdateInfo(this.CurrentStudent);
		}

		if (Input.GetKeyDown(KeyCode.Minus))
		{
			StudentGlobals.SetStudentReputation(this.CurrentStudent,
				StudentGlobals.GetStudentReputation(this.CurrentStudent) - 10);

			this.UpdateInfo(this.CurrentStudent);
		}

		StudentInfoMenuScript SIM = this.StudentInfoMenu;

		if (!SIM.CyberBullying && !SIM.CyberStalking && !SIM.FindingLocker &&
			!SIM.UsingLifeNote&& !SIM.GettingInfo && !SIM.MatchMaking &&
			!SIM.Distracting && !SIM.SendingHome && !SIM.Gossiping &&
			!SIM.Targeting && !SIM.Dead)
		{
			if (this.StudentInfoMenu.PauseScreen.InputManager.TappedRight)
			{
				this.CurrentStudent++;

				if (this.CurrentStudent > 100)
				{
					this.CurrentStudent = 1;
				}

				while (!StudentGlobals.GetStudentPhotographed(CurrentStudent))
				{
					this.CurrentStudent++;

					if (this.CurrentStudent > 100)
					{
						this.CurrentStudent = 1;
					}
				}

				this.UpdateInfo(this.CurrentStudent);
			}

			if (this.StudentInfoMenu.PauseScreen.InputManager.TappedLeft)
			{
				this.CurrentStudent--;

				if (this.CurrentStudent < 1)
				{
					this.CurrentStudent = 100;
				}

				while (!StudentGlobals.GetStudentPhotographed(CurrentStudent))
				{
					this.CurrentStudent--;

					if (this.CurrentStudent < 1)
					{
						this.CurrentStudent = 100;
					}
				}

				this.UpdateInfo(this.CurrentStudent);
			}
		}

		if (this.ShowRep)
		{
			this.ReputationChart.transform.localScale = Vector3.Lerp(
				this.ReputationChart.transform.localScale,
				new Vector3(138, 138, 138),
				Time.unscaledDeltaTime * 10);
		}
		else
		{
			this.ReputationChart.transform.localScale = Vector3.Lerp(
				this.ReputationChart.transform.localScale,
				new Vector3(0, 0, 0),
				Time.unscaledDeltaTime * 10);
		}
	}

	void UpdateAdditionalInfo(int ID)
	{
		Debug.Log ("EventGlobals.Event1 is: " + EventGlobals.Event1);

		/////////////////////////
		///// SPECIAL CASES /////
		/////////////////////////

		//Osana
		if (ID == 11)
		{
			// [af] Replaced if/else statement with ternary expression.
			this.Strings[1] = EventGlobals.OsanaEvent1 ?
				"May be a victim of blackmail." : "?????";

			// [af] Replaced if/else statement with ternary expression.
			this.Strings[2] = EventGlobals.OsanaEvent2 ?
				"Has a stalker." : "?????";

			this.InfoLabel.text = this.Strings[1] + "\n" + "\n" + this.Strings[2];
		}
		//Kokona
		else if (ID == 30)
		{
			// [af] Replaced if/else statement with ternary expression.
			this.Strings[1] = EventGlobals.Event1 ?
				"May be a victim of domestic abuse." : "?????";

			// [af] Replaced if/else statement with ternary expression.
			this.Strings[2] = EventGlobals.Event2 ?
				"May be engaging in compensated dating in Shisuta Town." : "?????";

			this.InfoLabel.text = this.Strings[1] + "\n" + "\n" + this.Strings[2];
		}
		//Miyuji
		else if (ID == 51)
		{
			if (ClubGlobals.GetClubClosed(ClubType.LightMusic))
			{
				this.InfoLabel.text = "Disbanded the Light Music Club, dyed her hair back to its original color, removed her piercings, and stopped socializing with others.";
			}
			else
			{
				this.InfoLabel.text = this.JSON.Students[ID].Info;
			}
		}

		////////////////////////
		///// NORMAL CASES /////
		////////////////////////

		else if (!StudentGlobals.GetStudentReplaced(ID))
		{
			if (this.JSON.Students[ID].Info == string.Empty)
			{
				this.InfoLabel.text = "No additional information is available at this time.";
			}
			else
			{
				this.InfoLabel.text = this.JSON.Students[ID].Info;
			}
		}

		//////////////////////////////////
		///// RELPACED STUDENT CASES /////
		//////////////////////////////////

		else
		{
			this.InfoLabel.text = "No additional information is available at this time.";
		}
	}


	void UpdateTopics()
	{
		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < this.TopicIcons.Length; ID++)
		{
			// [af] Replaced if/else statement with ternary expression.
			this.TopicIcons[ID].spriteName = 
				(!ConversationGlobals.GetTopicDiscovered(ID) ? 0 : ID).ToString();
		}

		// [af] Iterate topics 1 through 25.
		for (int i = 1; i <= 25; i++)
		{
			UISprite opinionIcon = this.TopicOpinionIcons[i];

			if (!ConversationGlobals.GetTopicLearnedByStudent(i, this.CurrentStudent))
			{
				opinionIcon.spriteName = "Unknown";
			}
			else
			{
				int[] studentTopics = this.JSON.Topics[this.CurrentStudent].Topics;
				opinionIcon.spriteName = this.OpinionSpriteNames[studentTopics[i]];
			}
		}
	}

	void UpdateRepChart()
	{
		Vector3 Triangle;

		if (this.CurrentStudent < 100)
		{
			Triangle = StudentGlobals.GetReputationTriangle(this.CurrentStudent);
		}
		else
		{
			Triangle = new Vector3(
				Random.Range(-100, 101),
				Random.Range(-100, 101),
				Random.Range(-100, 101));
		}

		ReputationChart.fields[0].Value = Triangle.x;
		ReputationChart.fields[1].Value = Triangle.y;
		ReputationChart.fields[2].Value = Triangle.z;
	}
}