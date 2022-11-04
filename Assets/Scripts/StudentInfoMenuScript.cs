using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StudentInfoMenuScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public InputManagerScript InputManager;
	public PauseScreenScript PauseScreen;
	public StudentInfoScript StudentInfo;
	public NoteWindowScript NoteWindow;
	public PromptBarScript PromptBar;
	public JsonScript JSON;

	public GameObject StudentPortrait;

	public Texture UnknownPortrait;
	public Texture BlankPortrait;
	public Texture Headmaster;
	public Texture Counselor;
	public Texture InfoChan;

	public Transform PortraitGrid;
	public Transform Highlight;
	public Transform Scrollbar;

	public StudentPortraitScript[] StudentPortraits;

	public Texture[] RivalPortraits;

	public bool[] PortraitLoaded;

	public UISprite[] DeathShadows;
	public UISprite[] Friends;
	public UISprite[] Panties;

	public UITexture[] PrisonBars;
	public UITexture[] Portraits;

	public UILabel NameLabel;

    public bool FiringCouncilMember = false;
    public bool CyberBullying = false;
	public bool CyberStalking = false;
	public bool FindingLocker = false;
	public bool UsingLifeNote = false;
	public bool GettingInfo = false;
	public bool MatchMaking = false;
	public bool Distracting = false;
	public bool SendingHome = false;
	public bool Gossiping = false;
	public bool Targeting = false;
	public bool Dead = false;

	public int[] SetSizes;

	public int StudentID = 0;
	public int Column = 0;
	public int Row = 0;
	public int Set = 0;

	public int Columns = 0;
	public int Rows = 0;

	void Start()
	{
		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < 101; ID++)
		{
			GameObject NewPortrait = Instantiate(this.StudentPortrait,
				this.transform.position, Quaternion.identity);
			NewPortrait.transform.parent = this.PortraitGrid;
			NewPortrait.transform.localPosition = new Vector3(
				-300.0f + (this.Column * 150.0f), 80.0f - (this.Row * 160.0f), 0.0f);
			NewPortrait.transform.localEulerAngles = Vector3.zero;
			NewPortrait.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

			this.StudentPortraits[ID] = NewPortrait.GetComponent<StudentPortraitScript>();

			this.Column++;

			if (this.Column > 4)
			{
				this.Column = 0;
				this.Row++;
			}
		}

		this.Column = 0;
		this.Row = 0;

		// [af] Commented in JS code.
		/*
		while (ID < this.Portraits.length)
		{
			this.Portraits[ID].mainTexture = null;
			this.DeathShadows[ID].enabled = false;
			this.PrisonBars[ID].enabled = false;
			this.Panties[ID].enabled = false;
			this.Friends[ID].enabled = false;
			
			ID++;
		}
		
		this.Portraits[0].mainTexture = this.InfoChan;
		*/
	}

	public bool GrabbedPortraits;

	void Update()
	{
		if (!this.GrabbedPortraits)
		{
			this.StartCoroutine(this.UpdatePortraits());
			this.GrabbedPortraits = true;
		}

		if (Input.GetButtonDown(InputNames.Xbox_A))
		{
			if (this.PromptBar.Label[0].text != string.Empty)
			{
				if (StudentGlobals.GetStudentPhotographed(this.StudentID) || this.StudentID > 97)
				{
					if (this.UsingLifeNote)
					{
						this.PauseScreen.MainMenu.SetActive(true);
						this.PauseScreen.Sideways = false;
						this.PauseScreen.Show = false;
						this.gameObject.SetActive(false);

						this.NoteWindow.TargetStudent = this.StudentID;
						this.NoteWindow.gameObject.SetActive(true);

						this.NoteWindow.SlotLabels[1].text = this.StudentManager.Students[this.StudentID].Name;
						this.NoteWindow.SlotsFilled[1] = true;

						this.UsingLifeNote = false;

						this.PromptBar.Label[0].text = "Confirm";
						this.PromptBar.UpdateButtons();

						this.NoteWindow.CheckForCompletion();
					}
					else
					{
						this.StudentInfo.gameObject.SetActive(true);
						this.StudentInfo.UpdateInfo(this.StudentID);
						this.StudentInfo.Topics.SetActive(false);
						this.gameObject.SetActive(false);

						this.PromptBar.ClearButtons();

						if (this.Gossiping)
						{
							this.PromptBar.Label[0].text = "Gossip";
						}

						if (this.Distracting)
						{
							this.PromptBar.Label[0].text = "Distract";
						}

						if (this.CyberBullying || this.CyberStalking)
						{
							this.PromptBar.Label[0].text = "Accept";
						}

						if (this.FindingLocker)
						{
							this.PromptBar.Label[0].text = "Find Locker";
						}

						if (this.MatchMaking)
						{
							this.PromptBar.Label[0].text = "Match";
						}

						if (this.Targeting || this.UsingLifeNote)
						{
							this.PromptBar.Label[0].text = "Kill";
						}

						if (this.SendingHome)
						{
							this.PromptBar.Label[0].text = "Send Home";
						}

                        if (this.FiringCouncilMember)
                        {
                            this.PromptBar.Label[0].text = "Fire";
                        }

                        if (this.StudentManager.Students[this.StudentID] != null)
						{
							if (this.StudentManager.Students[this.StudentID].gameObject.activeInHierarchy)
							{
								if (this.StudentManager.Tag.Target == this.StudentManager.Students[this.StudentID].Head)
								{
									this.PromptBar.Label[2].text = "Untag";
								}
								else
								{
									this.PromptBar.Label[2].text = "Tag";
								}
							}
							else
							{
								this.PromptBar.Label[2].text = "";
							}
						}
						else
						{
							this.PromptBar.Label[2].text = "";
						}

						this.PromptBar.Label[1].text = "Back";
						this.PromptBar.Label[3].text = "Interests";
						this.PromptBar.Label[6].text = "Reputation";
						this.PromptBar.UpdateButtons();
					}
				}
				else
				{
					StudentGlobals.SetStudentPhotographed(this.StudentID, true);

                    if (this.StudentManager.Students[this.StudentID] != null)
                    {
                        int TempID = 0;

                        while (TempID < this.StudentManager.Students[this.StudentID].Outlines.Length)
                        {
                            this.StudentManager.Students[this.StudentID].Outlines[TempID].enabled = true;
                            TempID++;
                        }
                    }

                    this.PauseScreen.ServiceMenu.gameObject.SetActive(true);
					this.PauseScreen.ServiceMenu.UpdateList();
					this.PauseScreen.ServiceMenu.UpdateDesc();
					this.PauseScreen.ServiceMenu.Purchase();
					this.GettingInfo = false;

					this.gameObject.SetActive(false);
				}
			}
		}

		if (Input.GetButtonDown(InputNames.Xbox_B))
		{
			if (this.Gossiping || this.Distracting || this.MatchMaking || this.Targeting)
			{
				if (this.Targeting)
				{
					this.PauseScreen.Yandere.RPGCamera.enabled = true;
				}

				this.PauseScreen.Yandere.Interaction = YandereInteractionType.Bye;
				this.PauseScreen.Yandere.TalkTimer = 2.0f;
				this.PauseScreen.MainMenu.SetActive(true);
				this.PauseScreen.Sideways = false;
				this.PauseScreen.Show = false;
				this.gameObject.SetActive(false);
				Time.timeScale = 1.0f;

				this.Distracting = false;
				this.MatchMaking = false;
				this.Gossiping = false;
				this.Targeting = false;

				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
			}
			else if (this.CyberBullying || this.CyberStalking || this.FindingLocker)
			{
				this.PauseScreen.MainMenu.SetActive(true);
				this.PauseScreen.Sideways = false;
				this.PauseScreen.Show = false;
				this.gameObject.SetActive(false);
				Time.timeScale = 1.0f;

				if (this.FindingLocker)
				{
					this.PauseScreen.Yandere.RPGCamera.enabled = true;
				}

				this.FindingLocker = false;

				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
			}
			else if (this.SendingHome || this.GettingInfo || this.FiringCouncilMember)
			{
				this.PauseScreen.ServiceMenu.gameObject.SetActive(true);
				this.PauseScreen.ServiceMenu.UpdateList();
				this.PauseScreen.ServiceMenu.UpdateDesc();
				this.gameObject.SetActive(false);

                this.FiringCouncilMember = false;
                this.SendingHome = false;
				this.GettingInfo = false;
			}
			else if (this.UsingLifeNote)
			{
				this.PauseScreen.MainMenu.SetActive(true);
				this.PauseScreen.Sideways = false;
				this.PauseScreen.Show = false;
				this.gameObject.SetActive(false);

				this.NoteWindow.gameObject.SetActive(true);

				this.UsingLifeNote = false;
			}
			else
			{
				this.PauseScreen.MainMenu.SetActive(true);
				this.PauseScreen.Sideways = false;
				this.PauseScreen.PressedB = true;
				this.gameObject.SetActive(false);

				this.PromptBar.ClearButtons();
				this.PromptBar.Label[0].text = "Accept";
				this.PromptBar.Label[1].text = "Exit";
				this.PromptBar.Label[4].text = "Choose";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;
			}
		}

		float scrollSpeed = Time.unscaledDeltaTime * 10.0f;
		float rowMultiplier = ((this.Row % 2) == 0) ? (this.Row / 2) : ((this.Row - 1) / 2);
		float targetY = 320.0f * rowMultiplier;

		this.PortraitGrid.localPosition = new Vector3(
			this.PortraitGrid.localPosition.x,
			Mathf.Lerp(this.PortraitGrid.localPosition.y, targetY, scrollSpeed),
			this.PortraitGrid.localPosition.z);

		this.Scrollbar.localPosition = new Vector3(
			this.Scrollbar.localPosition.x,
			Mathf.Lerp(this.Scrollbar.localPosition.y, 175.0f - 350.0f * (this.PortraitGrid.localPosition.y / 2880.0f), scrollSpeed),
			this.Scrollbar.localPosition.z);

		if (this.InputManager.TappedUp)
		{
			this.Row--;

			if (this.Row < 0)
			{
				this.Row = this.Rows - 1;
			}

			this.UpdateHighlight();
		}

		if (this.InputManager.TappedDown)
		{
			this.Row++;

			if (this.Row > (this.Rows - 1))
			{
				this.Row = 0;
			}

			this.UpdateHighlight();
		}

		if (this.InputManager.TappedRight)
		{
			this.Column++;

			if (this.Column > (this.Columns - 1))
			{
				this.Column = 0;
			}

			this.UpdateHighlight();
		}

		if (this.InputManager.TappedLeft)
		{
			this.Column--;

			if (this.Column < 0)
			{
				this.Column = this.Columns - 1;
			}

			this.UpdateHighlight();
		}
	}

	public void UpdateHighlight()
	{
		this.StudentID = 1 + (this.Column + (this.Row * this.Columns));

		if (StudentGlobals.GetStudentPhotographed(this.StudentID) || this.StudentID > 97)
		{
			this.PromptBar.Label[0].text = "View Info";
			this.PromptBar.UpdateButtons();
		}
		else
		{
			this.PromptBar.Label[0].text = string.Empty;
			this.PromptBar.UpdateButtons();
		}

        if (this.Gossiping)
		{
			if ((this.StudentID == 1) ||
				(this.StudentID == this.PauseScreen.Yandere.TargetStudent.StudentID) ||
				(this.JSON.Students[this.StudentID].Club == ClubType.Sports) ||
				StudentGlobals.GetStudentDead(this.StudentID) ||
				this.StudentID > 97)
			{
				this.PromptBar.Label[0].text = string.Empty;
				this.PromptBar.UpdateButtons();
			}
		}

		if (this.CyberBullying)
		{
			if ((this.JSON.Students[this.StudentID].Gender == 1) ||
				StudentGlobals.GetStudentDead(this.StudentID) ||
				this.StudentID > 97)
			{
				this.PromptBar.Label[0].text = string.Empty;
				this.PromptBar.UpdateButtons();
			}
		}

		if (this.CyberStalking)
		{
			if (StudentGlobals.GetStudentDead(this.StudentID) ||
                StudentGlobals.GetStudentArrested(this.StudentID) ||
                this.StudentID > 97)
			{
				this.PromptBar.Label[0].text = string.Empty;
				this.PromptBar.UpdateButtons();
			}
		}

		if (this.FindingLocker)
		{
			if (this.StudentID == 1 || this.StudentID > 89 ||
                this.StudentManager.Students[this.StudentID].Club == ClubType.Council ||
				StudentGlobals.GetStudentDead(this.StudentID))
			{
				this.PromptBar.Label[0].text = string.Empty;
				this.PromptBar.UpdateButtons();
			}
		}

		if (this.Distracting)
		{
			this.Dead = false;

			if (this.StudentManager.Students[this.StudentID] == null)
			{
				this.Dead = true;
			}

			if (this.Dead)
			{
				this.PromptBar.Label[0].text = string.Empty;
				this.PromptBar.UpdateButtons();
			}
			else if (this.StudentID == 1 || !this.StudentManager.Students[StudentID].Alive ||
				this.StudentID == this.PauseScreen.Yandere.TargetStudent.StudentID ||
				StudentGlobals.GetStudentKidnapped(this.StudentID) ||
				this.StudentManager.Students[StudentID].Tranquil ||
				this.StudentManager.Students[StudentID].Teacher ||
				this.StudentManager.Students[StudentID].Slave ||
				StudentGlobals.GetStudentDead(this.StudentID) ||
				this.StudentManager.Students[StudentID].MyBento.Tampered ||
				this.StudentID > 97)
			{
				this.PromptBar.Label[0].text = string.Empty;
				this.PromptBar.UpdateButtons();
			}
		}

		if (this.MatchMaking)
		{
			if ((this.StudentID == this.PauseScreen.Yandere.TargetStudent.StudentID) ||
				StudentGlobals.GetStudentDead(this.StudentID) ||
				this.StudentID > 97)
			{
				this.PromptBar.Label[0].text = string.Empty;
				this.PromptBar.UpdateButtons();
			}
		}

		if (this.Targeting)
		{
			if (this.StudentID == 1 || this.StudentID > 97 || StudentGlobals.GetStudentDead(this.StudentID) ||
				!this.StudentManager.Students[this.StudentID].gameObject.activeInHierarchy ||
				this.StudentManager.Students[this.StudentID].InEvent || this.StudentManager.Students[this.StudentID].Tranquil)
			{
				this.PromptBar.Label[0].text = string.Empty;
				this.PromptBar.UpdateButtons();
			}
		}

		if (this.SendingHome)
		{
			Debug.Log("Highlighting student number " + this.StudentID);

			if (this.StudentManager.Students[StudentID] != null)
			{
				StudentScript student = this.StudentManager.Students[StudentID];

				if (this.StudentID == 1 || StudentGlobals.GetStudentDead(this.StudentID) ||
					this.StudentID < 98 && student.SentHome ||
					this.StudentID > 97 || StudentGlobals.StudentSlave == this.StudentID ||
					student.Club == ClubType.MartialArts && student.ClubAttire ||
					student.Club == ClubType.Sports && student.ClubAttire ||
					StudentManager.Students[StudentID].CameraReacting ||
					!StudentGlobals.GetStudentPhotographed(this.StudentID))
				{
					this.PromptBar.Label[0].text = string.Empty;
					this.PromptBar.UpdateButtons();
				}
			}
		}

		if (this.GettingInfo)
		{
			if (StudentGlobals.GetStudentPhotographed(this.StudentID) || this.StudentID > 97)
			{
				this.PromptBar.Label[0].text = string.Empty;
			}
			else
			{
				this.PromptBar.Label[0].text = "Get Info";
			}

            this.PromptBar.UpdateButtons();
        }

        if (this.UsingLifeNote)
		{
			if (this.StudentID == 1 || this.StudentID > 97 || this.StudentID > 10 && this.StudentID < 21 ||
				this.StudentPortraits[this.StudentID].DeathShadow.activeInHierarchy ||
				this.StudentManager.Students[this.StudentID] != null && !this.StudentManager.Students[this.StudentID].enabled)
			{
				this.PromptBar.Label[0].text = "";
			}
			else
			{
				this.PromptBar.Label[0].text = "Kill";
			}

			this.PromptBar.UpdateButtons();
		}

        if (this.FiringCouncilMember)
        {
            if (this.StudentManager.Students[this.StudentID] != null)
            {
                if (!StudentGlobals.GetStudentPhotographed(this.StudentID) ||
                    this.StudentManager.Students[this.StudentID].Club != ClubType.Council)
                {
                    this.PromptBar.Label[0].text = "";
                }
                else
                {
                    this.PromptBar.Label[0].text = "Fire";
                }
            }

            this.PromptBar.UpdateButtons();
        }

        if (MissionModeGlobals.MissionMode && this.StudentID == 1)
        {
            this.PromptBar.Label[0].text = "";
        }

        // [af] Commented in JS code.
        /*
		if (this.StudentID < this.SetSizes[1])
		{
			this.Set = 0;
		}
		else
		{
			this.Set = 1;
		}
		*/

        this.Highlight.localPosition = new Vector3(
			-300.0f + (this.Column * 150.0f),
			80.0f - (Row * 160.0f),
			this.Highlight.localPosition.z);

		this.UpdateNameLabel();
	}

	void UpdateNameLabel()
	{
		if (StudentID > 97 || StudentGlobals.GetStudentPhotographed(this.StudentID) || this.GettingInfo)
		{
			this.NameLabel.text = this.JSON.Students[this.StudentID].Name;
		}
		else
		{
			this.NameLabel.text = "Unknown";
		}
	}

	public bool Debugging;

	public IEnumerator UpdatePortraits()
	{
		if (this.Debugging){Debug.Log("The Student Info Menu was instructed to get photos.");}

		for (int ID = 1; ID < 101; ID++)
		{
			if (this.Debugging){Debug.Log("1 - We entered the loop.");}

			if (ID == 0)
			{
				this.StudentPortraits[ID].Portrait.mainTexture = this.InfoChan;
			}
			else
			{
				if (this.Debugging){Debug.Log("2 - ID is not zero.");}

				if (!this.PortraitLoaded[ID])
				{
					if (this.Debugging){Debug.Log("3 - PortraitLoaded is false.");}

					if (ID < 12 || ID > 20 && ID < 98)
					{
						if (this.Debugging){Debug.Log("4 - ID is less than 98.");}

						if (StudentGlobals.GetStudentPhotographed(ID))
						{
							if (this.Debugging){Debug.Log("5 - GetStudentPhotographed is true.");}

							string path = "file:///" + Application.streamingAssetsPath +
								"/Portraits/Student_" + ID.ToString() + ".png";

							if (this.Debugging){Debug.Log("Path is: " + path);}

							WWW www = new WWW(path);

							if (this.Debugging){Debug.Log("Waiting for www to return.");}

							yield return www;

							if (this.Debugging){Debug.Log("www has returned.");}

							if (www.error == null)
							{
								//Debug.Log("6 - Error is null.");

								if (!StudentGlobals.GetStudentReplaced(ID))
								{
									this.StudentPortraits[ID].Portrait.mainTexture = www.texture;
								}
								else
								{
									this.StudentPortraits[ID].Portrait.mainTexture = this.BlankPortrait;
								}
							}
							else
							{
								//Debug.Log("We got an error when trying to retrieve a student's portrait!");

								this.StudentPortraits[ID].Portrait.mainTexture = this.UnknownPortrait;
							}

							this.PortraitLoaded[ID] = true;
						}
						else
						{
							this.StudentPortraits[ID].Portrait.mainTexture = this.UnknownPortrait;
						}
					}
					else
					{
						if (ID == 98)
						{
							this.StudentPortraits[ID].Portrait.mainTexture = this.Counselor;
						}
						else if (ID == 99)
						{
							this.StudentPortraits[ID].Portrait.mainTexture = this.Headmaster;
						}
						else if (ID == 100)
						{
							this.StudentPortraits[ID].Portrait.mainTexture = this.InfoChan;
						}
						else
						{
							this.StudentPortraits[ID].Portrait.mainTexture = this.RivalPortraits[ID];
						}
					}
				}
			}

			if (PlayerGlobals.GetStudentPantyShot(this.JSON.Students[ID].Name))
			{
				this.StudentPortraits[ID].Panties.SetActive(true);
			}

			// [af] Replaced if/else statement with boolean expression.
			this.StudentPortraits[ID].Friend.SetActive(PlayerGlobals.GetStudentFriend(ID));

			if (StudentGlobals.GetStudentDying(ID) || StudentGlobals.GetStudentDead(ID))
			{
				this.StudentPortraits[ID].DeathShadow.SetActive(true);
			}

            if (MissionModeGlobals.MissionMode && ID == 1)
            {
                this.StudentPortraits[ID].DeathShadow.SetActive(true);
            }

            if (SceneManager.GetActiveScene().name == SceneNames.SchoolScene)
			{
				if (this.StudentManager.Students[ID] != null)
				{
					if (this.StudentManager.Students[ID].Tranquil)
					{
						this.StudentPortraits[ID].DeathShadow.SetActive(true);
					}
				}
			}

			if (StudentGlobals.GetStudentArrested(ID))
			{
				this.StudentPortraits[ID].PrisonBars.SetActive(true);
				this.StudentPortraits[ID].DeathShadow.SetActive(true);
			}
		}
	}
}