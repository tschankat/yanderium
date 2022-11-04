using UnityEngine;

public class DialogueWheelScript : MonoBehaviour
{
	// [af] A lot of the functionality in this class could be put into a helper class 
	// instead. It would take input state and the number of dialogue choices as inputs, 
	// and would output the selected index (going clockwise with 0 at the top).

	public AppearanceWindowScript AppearanceWindow;
	public PracticeWindowScript PracticeWindow;
	public ClubManagerScript ClubManager;
	public LoveManagerScript LoveManager;
	public PauseScreenScript PauseScreen;
	public TaskManagerScript TaskManager;
	public ClubWindowScript ClubWindow;
	public NoteLockerScript NoteLocker;
	public ReputationScript Reputation;
	public TaskWindowScript TaskWindow;
	public PromptBarScript PromptBar;
	public JukeboxScript Jukebox;
	public YandereScript Yandere;
	public ClockScript Clock;
	public UIPanel Panel;

	public GameObject SwitchTopicsWindow;
	public GameObject TaskDialogueWindow;
	public GameObject ClubLeaderWindow;
	public GameObject DatingMinigame;
	public GameObject LockerWindow;

	public Transform Interaction;
	public Transform Favors;
	public Transform Club;
	public Transform Love;

	public UISprite TaskIcon;

	public UISprite Impatience;
	public UILabel CenterLabel;

	public UISprite[] Segment;
	public UISprite[] Shadow;

	public string[] Text;

	public UISprite[] FavorSegment;
	public UISprite[] FavorShadow;

	public UISprite[] ClubSegment;
	public UISprite[] ClubShadow;

	public UISprite[] LoveSegment;
	public UISprite[] LoveShadow;

	public string[] FavorText;
	public string[] ClubText;
	public string[] LoveText;

	public int Selected = 0;
	public int Victim = 0;

	public bool AskingFavor = false;
	public bool Matchmaking = false;
	public bool ClubLeader = false;
	public bool Pestered = false;
	public bool Show = false;

	public Vector3 PreviousPosition;
	public Vector2 MouseDelta;

	public Color OriginalColor;

	void Start()
	{
		this.Interaction.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		this.Favors.localScale = Vector3.zero;
		this.Club.localScale = Vector3.zero;
		this.Love.localScale = Vector3.zero;

		this.transform.localScale = Vector3.zero;

		this.OriginalColor = this.CenterLabel.color;
	}

	void Update()
	{
		if (!this.Show)
		{
			if (this.transform.localScale.x > 0.10f)
			{
				this.transform.localScale = Vector3.Lerp(
					this.transform.localScale, Vector3.zero, Time.deltaTime * 10.0f);
			}
			else
			{
				if (this.Panel.enabled)
				{
					this.transform.localScale = Vector3.zero;
					this.Panel.enabled = false;
				}
			}
		}
		else
		{
			if (this.Yandere.PauseScreen.Show)
			{
				this.Yandere.PauseScreen.ExitPhone();
			}

			if (this.ClubLeader)
			{
				this.Interaction.localScale = Vector3.Lerp(
					this.Interaction.localScale, Vector3.zero, Time.deltaTime * 10.0f);
				this.Favors.localScale = Vector3.Lerp(
					this.Favors.localScale, Vector3.zero, Time.deltaTime * 10.0f);
				this.Club.localScale = Vector3.Lerp(
					this.Club.localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
				this.Love.localScale = Vector3.Lerp(
					this.Love.localScale, Vector3.zero, Time.deltaTime * 10.0f);
			}
			else if (this.AskingFavor)
			{
				this.Interaction.localScale = Vector3.Lerp(
					this.Interaction.localScale, Vector3.zero, Time.deltaTime * 10.0f);
				this.Favors.localScale = Vector3.Lerp(
					this.Favors.localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
				this.Club.localScale = Vector3.Lerp(
					this.Club.localScale, Vector3.zero, Time.deltaTime * 10.0f);
				this.Love.localScale = Vector3.Lerp(
					this.Love.localScale, Vector3.zero, Time.deltaTime * 10.0f);
			}
			else if (this.Matchmaking)
			{
				this.Interaction.localScale = Vector3.Lerp(
					this.Interaction.localScale, Vector3.zero, Time.deltaTime * 10.0f);
				this.Favors.localScale = Vector3.Lerp(
					this.Favors.localScale, Vector3.zero, Time.deltaTime * 10.0f);
				this.Club.localScale = Vector3.Lerp(
					this.Club.localScale, Vector3.zero, Time.deltaTime * 10.0f);
				this.Love.localScale = Vector3.Lerp(
					this.Love.localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
			}
			else
			{
				this.Interaction.localScale = Vector3.Lerp(
					this.Interaction.localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
				this.Favors.localScale = Vector3.Lerp(
					this.Favors.localScale, Vector3.zero, Time.deltaTime * 10.0f);
				this.Club.localScale = Vector3.Lerp(
					this.Club.localScale, Vector3.zero, Time.deltaTime * 10.0f);
				this.Love.localScale = Vector3.Lerp(
					this.Love.localScale, Vector3.zero, Time.deltaTime * 10.0f);
			}

			this.MouseDelta.x += Input.GetAxis("Mouse X");
			this.MouseDelta.y += Input.GetAxis("Mouse Y");

			// [af] Commented in JS code.
			//MouseDelta.x += Input.mousePosition.x - PreviousPosition.x;
			//MouseDelta.y += Input.mousePosition.y - PreviousPosition.y;

			if (this.MouseDelta.x > 11.0f)
			{
				this.MouseDelta.x = 11.0f;
			}
			else if (this.MouseDelta.x < -11.0f)
			{
				this.MouseDelta.x = -11.0f;
			}

			if (this.MouseDelta.y > 11.0f)
			{
				this.MouseDelta.y = 11.0f;
			}
			else if (this.MouseDelta.y < -11.0f)
			{
				this.MouseDelta.y = -11.0f;
			}

			this.transform.localScale = Vector3.Lerp(
				this.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);

			//////////////////////////////////////////////////
			///// NORMAL INTERACTION OR CLUB INTERACTION /////
			//////////////////////////////////////////////////

			if (!this.AskingFavor && !this.Matchmaking)
			{
				if ((Input.GetAxis("Vertical") < 0.50f) &&
					(Input.GetAxis("Vertical") > -0.50f) &&
					(Input.GetAxis("Horizontal") < 0.50f) &&
					(Input.GetAxis("Horizontal") > -0.50f))
				{
					this.Selected = 0;
				}

				if ((Input.GetAxis("Vertical") > 0.50f) &&
					(Input.GetAxis("Horizontal") < 0.50f) &&
					(Input.GetAxis("Horizontal") > -0.50f) ||
					(this.MouseDelta.y > 10.0f) &&
					(this.MouseDelta.x < 10.0f) &&
					(this.MouseDelta.x > -10.0f))
				{
					this.Selected = 1;
				}

				if ((Input.GetAxis("Vertical") > 0.0f) &&
					(Input.GetAxis("Horizontal") > 0.50f) ||
					(this.MouseDelta.y > 0.0f) &&
					(this.MouseDelta.x > 10.0f))
				{
					this.Selected = 2;
				}

				if ((Input.GetAxis("Vertical") < 0.0f) &&
					(Input.GetAxis("Horizontal") > 0.50f) ||
					(this.MouseDelta.y < 0.0f) &&
					(this.MouseDelta.x > 10.0f))
				{
					this.Selected = 3;
				}

				if ((Input.GetAxis("Vertical") < -0.50f) &&
					(Input.GetAxis("Horizontal") < 0.50f) &&
					(Input.GetAxis("Horizontal") > -0.50f) ||
					(this.MouseDelta.y < -10.0f) &&
					(this.MouseDelta.x < 10.0f) &&
					(this.MouseDelta.x > -10.0f))
				{
					this.Selected = 4;
				}

				if ((Input.GetAxis("Vertical") < 0.0f) &&
					(Input.GetAxis("Horizontal") < -0.50f) ||
					(this.MouseDelta.y < 0.0f) &&
					(this.MouseDelta.x < -10.0f))
				{
					this.Selected = 5;
				}

				if ((Input.GetAxis("Vertical") > 0.0f) &&
					(Input.GetAxis("Horizontal") < -0.50f) ||
					(this.MouseDelta.y > 0.0f) &&
					(this.MouseDelta.x < -10.0f))
				{
					this.Selected = 6;
				}

				this.CenterLabel.text = this.Text[this.Selected];
				this.CenterLabel.color = this.OriginalColor;

				if (!this.ClubLeader)
				{
					if (this.Selected == 5)
					{
						if (PlayerGlobals.GetStudentFriend(this.Yandere.TargetStudent.StudentID))
						{
							this.CenterLabel.text = "Love";
						}
					}
					else if (this.Selected == 6)
					{
						if (this.Yandere.Club == ClubType.Delinquent)
						{
							this.CenterLabel.text = "Intimidate";
							this.CenterLabel.color = new Color(1, 0, 0, 1);
						}
					}
				}
				else
				{
					this.CenterLabel.text = this.ClubText[this.Selected];
				}
			}

			/////////////////////////////////////////////
			///// ASKING FOR A FAVOR OR MATCHMAKING /////
			/////////////////////////////////////////////

			else
			{
				if ((Input.GetAxis("Vertical") < 0.50f) &&
					(Input.GetAxis("Vertical") > -0.50f) &&
					(Input.GetAxis("Horizontal") < 0.50f) &&
					(Input.GetAxis("Horizontal") > -0.50f))
				{
					this.Selected = 0;
				}

				if ((Input.GetAxis("Vertical") > 0.50f) &&
					(Input.GetAxis("Horizontal") < 0.50f) &&
					(Input.GetAxis("Horizontal") > -0.50f) ||
					(this.MouseDelta.y > 10.0f) &&
					(this.MouseDelta.x < 10.0f) &&
					(this.MouseDelta.x > -10.0f))
				{
					this.Selected = 1;
				}

				if ((Input.GetAxis("Vertical") < 0.50f) &&
					(Input.GetAxis("Vertical") > -0.50f) &&
					(Input.GetAxis("Horizontal") > 0.50f) ||
					(this.MouseDelta.y < 10.0f) &&
					(this.MouseDelta.y > -10.0f) &&
					(this.MouseDelta.x > 10.0f))
				{
					this.Selected = 2;
				}

				if ((Input.GetAxis("Vertical") < -0.50f) &&
					(Input.GetAxis("Horizontal") < 0.50f) &&
					(Input.GetAxis("Horizontal") > -0.50f) ||
					(this.MouseDelta.y < -10.0f) &&
					(this.MouseDelta.x < 10.0f) &&
					(this.MouseDelta.x > -10.0f))
				{
					this.Selected = 3;
				}

				if ((Input.GetAxis("Vertical") < 0.50f) &&
					(Input.GetAxis("Vertical") > -0.50f) &&
					(Input.GetAxis("Horizontal") < -0.50f) ||
					(this.MouseDelta.y < 10.0f) &&
					(this.MouseDelta.y > -10.0f) &&
					(this.MouseDelta.x < -10.0f))
				{
					this.Selected = 4;
				}

				// [af] Simplified if/else statements for readability.
				if (this.Selected < this.FavorText.Length)
				{
					this.CenterLabel.text = this.AskingFavor ?
						this.FavorText[this.Selected] : this.LoveText[this.Selected];
				}
			}

			if (!this.ClubLeader)
			{
				// [af] Converted while loop to for loop.
				for (int ID = 1; ID < 7; ID++)
				{
					Transform segmentTransform = this.Segment[ID].transform;

					// [af] Replaced if/else statement with assignment and ternary expression.
					segmentTransform.localScale = Vector3.Lerp(
						segmentTransform.localScale,
						(this.Selected == ID) ? new Vector3(1.30f, 1.30f, 1.0f) : new Vector3(1.0f, 1.0f, 1.0f),
						Time.deltaTime * 10.0f);
				}
			}
			else
			{
				// [af] Converted while loop to for loop.
				for (int ID = 1; ID < 7; ID++)
				{
					Transform segmentTransform = this.ClubSegment[ID].transform;

					// [af] Replaced if/else statement with assignment and ternary expression.
					segmentTransform.localScale = Vector3.Lerp(
						segmentTransform.localScale,
						(this.Selected == ID) ? new Vector3(1.30f, 1.30f, 1.0f) : new Vector3(1.0f, 1.0f, 1.0f),
						Time.deltaTime * 10.0f);
				}
			}

			if (!this.Matchmaking)
			{
				// [af] Converted while loop to for loop.
				for (int ID = 1; ID < 5; ID++)
				{
					Transform segmentTransform = this.FavorSegment[ID].transform;

					// [af] Replaced if/else statement with assignment and ternary expression.
					segmentTransform.localScale = Vector3.Lerp(
						segmentTransform.localScale,
						(this.Selected == ID) ? new Vector3(1.30f, 1.30f, 1.0f) : new Vector3(1.0f, 1.0f, 1.0f),
						Time.deltaTime * 10.0f);
				}
			}
			else
			{
				// [af] Converted while loop to for loop.
				for (int ID = 1; ID < 5; ID++)
				{
					Transform segmentTransform = this.LoveSegment[ID].transform;

					// [af] Replaced if/else statement with assignment and ternary expression.
					segmentTransform.localScale = Vector3.Lerp(
						segmentTransform.localScale,
						(this.Selected == ID) ? new Vector3(1.30f, 1.30f, 1.0f) : new Vector3(1.0f, 1.0f, 1.0f),
						Time.deltaTime * 10.0f);
				}
			}

			if (Input.GetButtonDown(InputNames.Xbox_A))
			{
				////////////////////////////
				///// CLUB INTERACTION /////
				////////////////////////////

				if (this.ClubLeader)
				{
					if (this.Selected != 0)
					{
						if (this.ClubShadow[this.Selected].color.a == 0.0f)
						{
							int ClubBonus = 0;

							if (this.Yandere.TargetStudent.Sleuthing)
							{
								ClubBonus = 5;
							}

							////////////////
							///// INFO /////
							////////////////
							if (this.Selected == 1)
							{
								this.Impatience.fillAmount = 0.0f;
								this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubInfo;
								this.Yandere.TargetStudent.TalkTimer = 100.0f;
								this.Yandere.TargetStudent.ClubPhase = 1;
								this.Show = false;
							}

							////////////////
							///// JOIN /////
							////////////////
							else if (this.Selected == 2)
							{
								this.Impatience.fillAmount = 0.0f;
								this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubJoin;
								this.Yandere.TargetStudent.TalkTimer = 100.0f;
								this.Show = false;

								this.ClubManager.CheckGrudge(this.Yandere.TargetStudent.Club);

								if (ClubGlobals.GetQuitClub(this.Yandere.TargetStudent.Club))
								{
									this.Yandere.TargetStudent.ClubPhase = 4;
								}
								else if (this.Yandere.Club != 0)
								{
									this.Yandere.TargetStudent.ClubPhase = 5;
								}
								else if (this.ClubManager.ClubGrudge)
								{
									this.Yandere.TargetStudent.ClubPhase = 6;
								}
								else
								{
									this.Yandere.TargetStudent.ClubPhase = 1;
								}
							}

							////////////////
							///// QUIT /////
							////////////////
							else if (this.Selected == 3)
							{
								this.Impatience.fillAmount = 0.0f;
								this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubQuit;
								this.Yandere.TargetStudent.TalkTimer = 100.0f;
								this.Yandere.TargetStudent.ClubPhase = 1;
								this.Show = false;
							}

							///////////////
							///// BYE /////
							///////////////
							else if (this.Selected == 4)
							{
								this.Impatience.fillAmount = 0.0f;
								this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubBye;
								this.Yandere.TargetStudent.TalkTimer =
									this.Yandere.Subtitle.ClubFarewellClips[(int)this.Yandere.TargetStudent.Club + ClubBonus].length;
								this.Show = false;

								Debug.Log("This club leader exchange is over.");
							}

							////////////////////
							///// ACTIVITY /////
							////////////////////
							else if (this.Selected == 5)
							{
								this.Impatience.fillAmount = 0.0f;
								this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubActivity;
								this.Yandere.TargetStudent.TalkTimer = 100.0f;

								if (this.Clock.HourTime < 17.0f)
								{
									this.Yandere.TargetStudent.ClubPhase = 4;
								}
								else if (this.Clock.HourTime > 17.50f)
								{
									this.Yandere.TargetStudent.ClubPhase = 5;
								}
								else
								{
									this.Yandere.TargetStudent.ClubPhase = 1;
								}

								this.Show = false;
							}

							//////////////////////////////////////
							///// "PRACTICE" - CLUB-SPECIFIC /////
							//////////////////////////////////////
							else if (this.Selected == 6)
							{
								this.Impatience.fillAmount = 0.0f;
								this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubPractice;
								this.Yandere.TargetStudent.TalkTimer = 100.0f;
								this.Yandere.TargetStudent.ClubPhase = 1;
								this.Show = false;
							}
						}
					}
				}

				//////////////////////////////
				///// ASKING FOR A FAVOR /////
				//////////////////////////////

				else if (this.AskingFavor)
				{
					if (this.Selected != 0)
					{
						if (this.Selected < this.FavorShadow.Length)
						{
							if (this.FavorShadow[this.Selected] != null)
							{
								if (this.FavorShadow[this.Selected].color.a == 0.0f)
								{
									/////////////////////
									///// FOLLOW ME /////
									/////////////////////

									if (this.Selected == 1)
									{
										this.Impatience.fillAmount = 0.0f;
										this.Yandere.Interaction = YandereInteractionType.FollowMe;
										this.Yandere.TalkTimer = 3.0f;
										this.Show = false;
									}

									///////////////////
									///// GO AWAY /////
									///////////////////

									else if (this.Selected == 2)
									{
										this.Impatience.fillAmount = 0.0f;
										this.Yandere.Interaction = YandereInteractionType.GoAway;
										this.Yandere.TalkTimer = 3.0f;
										this.Show = false;
									}

									/////////////////////////
									///// DISTRACT THEM /////
									/////////////////////////

									else if (this.Selected == 4)
									{
										this.PauseScreen.StudentInfoMenu.Distracting = true;

										this.PauseScreen.StudentInfoMenu.gameObject.SetActive(true);
										this.PauseScreen.StudentInfoMenu.Column = 0;
										this.PauseScreen.StudentInfoMenu.Row = 0;
										this.PauseScreen.StudentInfoMenu.UpdateHighlight();
										this.StartCoroutine(this.PauseScreen.StudentInfoMenu.UpdatePortraits());
										this.PauseScreen.MainMenu.SetActive(false);
										this.PauseScreen.Panel.enabled = true;
										this.PauseScreen.Sideways = true;
										this.PauseScreen.Show = true;
										Time.timeScale = 0.0001f;

										this.PromptBar.ClearButtons();
										this.PromptBar.Label[1].text = "Cancel";
										this.PromptBar.UpdateButtons();
										this.PromptBar.Show = true;

										this.Impatience.fillAmount = 0.0f;
										this.Yandere.Interaction = YandereInteractionType.DistractThem;
										this.Yandere.TalkTimer = 3.0f;
										this.Show = false;
									}
								}
							}
						}

						if (this.Selected == 3)
						{
							this.AskingFavor = false;
						}
					}
				}

				////////////////////////
				///// MATCHMAKING  /////
				////////////////////////

				else if (this.Matchmaking)
				{
					if (this.Selected != 0)
					{
						if (this.Selected < this.LoveShadow.Length)
						{
							if (this.LoveShadow[this.Selected] != null)
							{
								if (this.LoveShadow[this.Selected].color.a == 0.0f)
								{
									//////////////////////
									///// APPEARANCE /////
									//////////////////////

									if (this.Selected == 1)
									{
										this.PromptBar.ClearButtons();
										this.PromptBar.Label[0].text = "Select";
										this.PromptBar.Label[4].text = "Change";
										this.PromptBar.UpdateButtons();
										this.PromptBar.Show = true;

										this.AppearanceWindow.gameObject.SetActive(true);
										this.AppearanceWindow.Show = true;
										this.Show = false;
									}

									/////////////////
									///// COURT /////
									/////////////////

									else if (this.Selected == 2)
									{
										this.Impatience.fillAmount = 0.0f;
										this.Yandere.Interaction = YandereInteractionType.Court;
										this.Yandere.TalkTimer = 5.0f;
										this.Show = false;
									}

									///////////////////
									///// CONFESS /////
									///////////////////

									else if (this.Selected == 4)
									{
										this.Impatience.fillAmount = 0.0f;
										this.Yandere.Interaction = YandereInteractionType.Confess;
										this.Yandere.TalkTimer = 5.0f;
										this.Show = false;
									}
								}
							}
						}

						if (this.Selected == 3)
						{
							this.Matchmaking = false;
						}
					}
				}

				//////////////////////////////
				///// NORMAL INTERACTION /////
				//////////////////////////////

				else
				{
					if (this.Selected != 0)
					{
						if (this.Shadow[this.Selected].color.a == 0.0f)
						{
							///////////////////////
							///// APOLOGIZING /////
							///////////////////////
							if (this.Selected == 1)
							{
								this.Impatience.fillAmount = 0.0f;
								this.Yandere.Interaction = YandereInteractionType.Apologizing;
								this.Yandere.TalkTimer = 3.0f;
								this.Show = false;
							}

							/////////////////////////
							///// COMPLIMENTING /////
							/////////////////////////
							else if (this.Selected == 2)
							{
								this.Impatience.fillAmount = 0.0f;
								this.Yandere.Interaction = YandereInteractionType.Compliment;
								this.Yandere.TalkTimer = 3.0f;
								this.Show = false;
							}

							/////////////////////
							///// GOSSIPING /////
							/////////////////////
							else if (this.Selected == 3)
							{
								this.PauseScreen.StudentInfoMenu.Gossiping = true;

								this.PauseScreen.StudentInfoMenu.gameObject.SetActive(true);
								this.PauseScreen.StudentInfoMenu.Column = 0;
								this.PauseScreen.StudentInfoMenu.Row = 0;
								this.PauseScreen.StudentInfoMenu.UpdateHighlight();
								this.StartCoroutine(this.PauseScreen.StudentInfoMenu.UpdatePortraits());
								this.PauseScreen.MainMenu.SetActive(false);
								this.PauseScreen.Panel.enabled = true;
								this.PauseScreen.Sideways = true;
								this.PauseScreen.Show = true;

								Time.timeScale = 0.0001f;

								this.PromptBar.ClearButtons();
								this.PromptBar.Label[0].text = string.Empty;
								this.PromptBar.Label[1].text = "Cancel";
								this.PromptBar.UpdateButtons();
								this.PromptBar.Show = true;

								this.Impatience.fillAmount = 0.0f;
								this.Yandere.Interaction = YandereInteractionType.Gossip;
								this.Yandere.TalkTimer = 3.0f;
								this.Show = false;
							}

							///////////////////
							///// LEAVING /////
							///////////////////
							else if (this.Selected == 4)
							{
								this.Impatience.fillAmount = 0.0f;
								this.Yandere.Interaction = YandereInteractionType.Bye;
								this.Yandere.TalkTimer = 2.0f;
								this.Show = false;

								Debug.Log("This exchange is over.");
							}

							//////////////////////////////
							///// FRIENDSHIP-RELATED /////
							//////////////////////////////
							else if (this.Selected == 5)
							{
								///////////////////////////
								///// ASKING FOR TASK /////
								///////////////////////////

								if (!PlayerGlobals.GetStudentFriend(this.Yandere.TargetStudent.StudentID))
								{
									this.CheckTaskCompletion();

									if (this.Yandere.TargetStudent.TaskPhase == 0)
									{
										this.Impatience.fillAmount = 0.0f;
										this.Yandere.TargetStudent.Interaction = StudentInteractionType.GivingTask;
										this.Yandere.TargetStudent.TalkTimer = 100.0f;
										this.Yandere.TargetStudent.TaskPhase = 1;
									}
									else
									{
										this.Impatience.fillAmount = 0.0f;
										this.Yandere.TargetStudent.Interaction = StudentInteractionType.GivingTask;
										this.Yandere.TargetStudent.TalkTimer = 100.0f;
									}

									this.Show = false;
								}

								////////////////
								///// LOVE /////
								////////////////

								else
								{
									if (this.Yandere.LoveManager.SuitorProgress == 0)
									{
										this.PauseScreen.StudentInfoMenu.MatchMaking = true;

										this.PauseScreen.StudentInfoMenu.gameObject.SetActive(true);
										this.PauseScreen.StudentInfoMenu.Column = 0;
										this.PauseScreen.StudentInfoMenu.Row = 0;
										this.PauseScreen.StudentInfoMenu.UpdateHighlight();
										this.StartCoroutine(this.PauseScreen.StudentInfoMenu.UpdatePortraits());
										this.PauseScreen.MainMenu.SetActive(false);
										this.PauseScreen.Panel.enabled = true;
										this.PauseScreen.Sideways = true;
										this.PauseScreen.Show = true;
										Time.timeScale = 0.0001f;

										this.PromptBar.ClearButtons();
										this.PromptBar.Label[0].text = "View Info";
										this.PromptBar.Label[1].text = "Cancel";
										this.PromptBar.UpdateButtons();
										this.PromptBar.Show = true;

										this.Impatience.fillAmount = 0.0f;
										this.Yandere.Interaction = YandereInteractionType.NamingCrush;
										this.Yandere.TalkTimer = 3.0f;
										this.Show = false;
									}
									else
									{
										//Anti-Osana code
										#if UNITY_EDITOR
										this.Matchmaking = true;
										#endif
									}
								}
							}

							////////////////////////
							///// ASKING FAVOR /////
							////////////////////////
							else if (this.Selected == 6)
							{
								this.AskingFavor = true;
							}
						}
					}
				}
			}
			else if (Input.GetButtonDown(InputNames.Xbox_X))
			{
				if (this.TaskDialogueWindow.activeInHierarchy)
				{
					this.Impatience.fillAmount = 0.0f;
					this.Yandere.Interaction = YandereInteractionType.TaskInquiry;
					this.Yandere.TalkTimer = 3.0f;
					this.Show = false;
				}
				else if (this.SwitchTopicsWindow.activeInHierarchy)
				{
					this.ClubLeader = !this.ClubLeader;
					this.HideShadows();
				}
			}
			else if (Input.GetButtonDown(InputNames.Xbox_B))
			{
				if (this.LockerWindow.activeInHierarchy)
				{
					this.Impatience.fillAmount = 0.0f;
					this.Yandere.Interaction = YandereInteractionType.SendingToLocker;
					this.Yandere.TalkTimer = 5.0f;
					this.Show = false;
				}
			}
		}

		this.PreviousPosition = Input.mousePosition;
	}

	public void HideShadows()
	{
		this.Jukebox.Dip = .5f;

		this.TaskDialogueWindow.SetActive(false);
		this.ClubLeaderWindow.SetActive(false);
		this.LockerWindow.SetActive(false);

		if (this.ClubLeader && !this.Yandere.TargetStudent.Talk.Fake)
		{
			this.SwitchTopicsWindow.SetActive(true);
		}
		else
		{
			this.SwitchTopicsWindow.SetActive(false);
		}

		if (this.Yandere.TargetStudent.Armband.activeInHierarchy && !this.ClubLeader &&
			this.Yandere.TargetStudent.Club != ClubType.Council)
		{
			this.ClubLeaderWindow.SetActive(true);
		}

		if (this.NoteLocker.NoteLeft && this.NoteLocker.Student == this.Yandere.TargetStudent)
		{
			this.LockerWindow.SetActive(true);
		}

		if (this.Yandere.TargetStudent.Club == ClubType.Bully &&
			TaskGlobals.GetTaskStatus(36) == 1)
		{
			this.TaskDialogueWindow.SetActive(true);
		}

		// [af] Replaced if/else statement with ternary expression.
		this.TaskIcon.spriteName =
			PlayerGlobals.GetStudentFriend(this.Yandere.TargetStudent.StudentID) ?
			"Heart" : "Task";

		this.Impatience.fillAmount = 0.0f;

		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < 7; ID++)
		{
			UISprite shadow = this.Shadow[ID];
			shadow.color = new Color(
				shadow.color.r,
				shadow.color.g,
				shadow.color.b,
				0.0f);
		}

		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < 5; ID++)
		{
			UISprite favorShadow = this.FavorShadow[ID];
			favorShadow.color = new Color(
				favorShadow.color.r,
				favorShadow.color.g,
				favorShadow.color.b,
				0.0f);
		}

		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < 7; ID++)
		{
			UISprite clubShadow = this.ClubShadow[ID];
			clubShadow.color = new Color(
				clubShadow.color.r,
				clubShadow.color.g,
				clubShadow.color.b,
				0.0f);
		}

		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < 5; ID++)
		{
			UISprite loveShadow = this.LoveShadow[ID];
			loveShadow.color = new Color(
				loveShadow.color.r,
				loveShadow.color.g,
				loveShadow.color.b,
				0.0f);
		}

		//////////////////////////////
		///// NORMAL INTERACTION /////
		//////////////////////////////

		// Hiding the Forgive button.
		if (!this.Yandere.TargetStudent.Witness || this.Yandere.TargetStudent.Forgave || this.Yandere.TargetStudent.Club == ClubType.Council)
		{
			UISprite shadow1 = this.Shadow[1];
			shadow1.color = new Color(
				shadow1.color.r,
				shadow1.color.g,
				shadow1.color.b,
				0.75f);
		}

		// Hiding the Compliment button.
		if (this.Yandere.TargetStudent.Complimented || this.Yandere.TargetStudent.Club == ClubType.Council)
		{
			UISprite shadow2 = this.Shadow[2];
			shadow2.color = new Color(
				shadow2.color.r,
				shadow2.color.g,
				shadow2.color.b,
				0.75f);
		}

		// Hiding the Gossip button.
		if (this.Yandere.TargetStudent.Gossiped || this.Yandere.TargetStudent.Club == ClubType.Council)
		{
			UISprite shadow3 = this.Shadow[3];
			shadow3.color = new Color(
				shadow3.color.r,
				shadow3.color.g,
				shadow3.color.b,
				0.75f);
		}

		// If Yandere-chan is bloody or insane, disable these buttons.
		if ((this.Yandere.Bloodiness > 0.0f) || (this.Yandere.Sanity < 33.33333f) || this.Yandere.TargetStudent.Club == ClubType.Council)
		{
			UISprite shadow3 = this.Shadow[3];
			shadow3.color = new Color(
				shadow3.color.r,
				shadow3.color.g,
				shadow3.color.b,
				0.75f);

			this.Shadow[5].color = new Color(0, 0, 0, 0.75f);

			UISprite shadow6 = this.Shadow[6];
			shadow6.color = new Color(
				shadow6.color.r,
				shadow6.color.g,
				shadow6.color.b,
				0.75f);
		}
		else if (this.Reputation.Reputation < -33.33333f)
		{
			UISprite shadow3 = this.Shadow[3];
			shadow3.color = new Color(
				shadow3.color.r,
				shadow3.color.g,
				shadow3.color.b,
				0.75f);
		}
			
		// Hiding the Task / Matchmake button.

		// If the student has not even arrived at school yet, hide the Task button.
		if (!this.Yandere.TargetStudent.Indoors || this.Yandere.TargetStudent.Club == ClubType.Council)
		{
			this.Shadow[5].color = new Color(0, 0, 0, 0.75f);
		}
		// If Yandere-chan is not friends with the student, use the Task protocol.
		else if (!PlayerGlobals.GetStudentFriend(this.Yandere.TargetStudent.StudentID))
		{
            Debug.Log("Yandere.TargetStudent.TaskPhase is: " + this.Yandere.TargetStudent.TaskPhase + ".");
            Debug.Log("TaskGlobals.GetTaskStatus of current student is: " + TaskGlobals.GetTaskStatus(this.Yandere.TargetStudent.StudentID));

            bool generic = false;

			if (this.Yandere.TargetStudent.StudentID != 8 &&
				this.Yandere.TargetStudent.StudentID != 11 &&
				this.Yandere.TargetStudent.StudentID != 25 &&
				this.Yandere.TargetStudent.StudentID != 28 &&
				this.Yandere.TargetStudent.StudentID != 30 &&
				this.Yandere.TargetStudent.StudentID != 36 &&
				this.Yandere.TargetStudent.StudentID != 37 &&
				this.Yandere.TargetStudent.StudentID != 38 &&
				this.Yandere.TargetStudent.StudentID != 52 &&
				this.Yandere.TargetStudent.StudentID != 76 &&
				this.Yandere.TargetStudent.StudentID != 77 &&
				this.Yandere.TargetStudent.StudentID != 78 &&
				this.Yandere.TargetStudent.StudentID != 79 &&
				this.Yandere.TargetStudent.StudentID != 80 &&
				this.Yandere.TargetStudent.StudentID != 81)
			{
				generic = true;
			}

			#if UNITY_EDITOR
			if (this.Yandere.TargetStudent.StudentID == 6)
			{
				Debug.Log("Speaking to Osana's suitor.");

				generic = false;
			}
			#endif

			if (this.Yandere.TargetStudent.StudentID == 1 ||
			    this.Yandere.TargetStudent.StudentID == 41)
			{
				this.Shadow[5].color = new Color(0, 0, 0, 0.75f);
			}
			else
			{
				// If the Task is currently in progress, or has already been completed, 
				// hide the Task button.
				if (this.Yandere.TargetStudent.TaskPhase > 0 &&
					this.Yandere.TargetStudent.TaskPhase < 5 ||
					TaskGlobals.GetTaskStatus(this.Yandere.TargetStudent.StudentID) > 0 &&
					TaskGlobals.GetTaskStatus(this.Yandere.TargetStudent.StudentID) < 5 &&
					TaskGlobals.GetTaskStatus(this.Yandere.TargetStudent.StudentID) != 2 ||
					this.Yandere.TargetStudent.TaskPhase == 100)
				{
					Debug.Log("Hiding task button.");

					this.Shadow[5].color = new Color(0, 0, 0, 0.75f);
				}

				//Debug.Log("This student's Task Phase is: " + this.Yandere.TargetStudent.TaskPhase);

				if (this.Yandere.TargetStudent.TaskPhase == 5)
				{
					Debug.Log("Unhiding task button.");

					this.Shadow[5].color = new Color(0, 0, 0, 0.0f);
				}

				//Osana Suitor special case
				if (this.Yandere.TargetStudent.StudentID == 6)
				{
					Debug.Log("The status of Task #6 is:" + TaskGlobals.GetTaskStatus(6));

					// If this student's task has been accepted...
					if (TaskGlobals.GetTaskStatus(6) == 1)
					{
						// Make the Task button available if the player has a headset in their inventory.
						if (Yandere.Inventory.Headset)
						{
							this.Shadow[5].color = new Color(0, 0, 0, 0);
							Debug.Log("Player has a headset.");
						}
					}
				}
				// Gema special case
				else if (this.Yandere.TargetStudent.StudentID == 36)
				{
					if (TaskGlobals.GetTaskStatus(36) == 0)
					{
						if (StudentGlobals.GetStudentDead(81) ||
							StudentGlobals.GetStudentDead(82) ||
							StudentGlobals.GetStudentDead(83) ||
							StudentGlobals.GetStudentDead(84) ||
							StudentGlobals.GetStudentDead(85))
						{
							this.Shadow[5].color = new Color(0, 0, 0, 0.75f);
						}
					}
				}
                // Musume special case
                else if (this.Yandere.TargetStudent.StudentID == 81)
                {
                    if (TaskGlobals.GetTaskStatus(81) == 0)
                    {
                        if (StudentGlobals.GetStudentDead(5))
                        {
                            this.Shadow[5].color = new Color(0, 0, 0, 0.75f);
                        }
                    }
                }

                ////////////////////////////
                ///// DELINQUENT TASKS /////
                ////////////////////////////

                else if (this.Yandere.TargetStudent.StudentID == 76)
				{
					Debug.Log("The status of Task #76 is:" + TaskGlobals.GetTaskStatus(76));

					// If this student's task has been accepted...
					if (TaskGlobals.GetTaskStatus(76) == 1)
					{
						// Make the Task button available if the player has over $100.
						if (Yandere.Inventory.Money >= 100)
						{
							this.Shadow[5].color = new Color(0, 0, 0, 0);
							Debug.Log("Player has over $100.");
						}
					}
				}
				else if (this.Yandere.TargetStudent.StudentID == 77)
				{
					// If this student's task has been accepted...
					if (TaskGlobals.GetTaskStatus(77) == 1)
					{
						// Make the Task button available if the player has a knife in their inventory.
						if (this.Yandere.Weapon[1] != null && this.Yandere.Weapon[1].WeaponID == 1 ||
							this.Yandere.Weapon[1] != null && this.Yandere.Weapon[1].WeaponID == 8 ||
							this.Yandere.Weapon[2] != null && this.Yandere.Weapon[2].WeaponID == 1 ||
							this.Yandere.Weapon[2] != null && this.Yandere.Weapon[2].WeaponID == 8)
						{
							this.Shadow[5].color = new Color(0, 0, 0, 0);
							Debug.Log("Player has a knife.");
						}
					}
				}
				else if (this.Yandere.TargetStudent.StudentID == 78)
				{
					// If this student's task has been accepted...
					if (TaskGlobals.GetTaskStatus(78) == 1)
					{
						// Make the Task button available if the player has sake in their inventory.
						if (this.Yandere.Inventory.Sake)
						{
							this.Shadow[5].color = new Color(0, 0, 0, 0);
							Debug.Log("Player has sake.");
						}
					}
				}
				else if (this.Yandere.TargetStudent.StudentID == 79)
				{
					// If this student's task has been accepted...
					if (TaskGlobals.GetTaskStatus(79) == 1)
					{
						// Make the Task button available if the player has cigarettes in their inventory.
						if (this.Yandere.Inventory.Cigs)
						{
							this.Shadow[5].color = new Color(0, 0, 0, 0);
							Debug.Log("Player has ciggies.");
						}
					}
				}
				else if (this.Yandere.TargetStudent.StudentID == 80)
				{
					// If this student's task has been accepted...
					if (TaskGlobals.GetTaskStatus(80) == 1)
					{
						// Make the Task button available if the player has an answer sheet in their inventory.
						if (this.Yandere.Inventory.AnswerSheet)
						{
							this.Shadow[5].color = new Color(0, 0, 0, 0);
							Debug.Log("Player has the answer sheet.");
						}
					}
				}

				// Generic placeholder Task
				if (generic)
				{
					if (TaskGlobals.GetTaskStatus(this.Yandere.TargetStudent.StudentID) == 1)
					{
						// Make the Task button available if the player has a book.
						if (this.Yandere.Inventory.Book)
						{
							this.Shadow[5].color = new Color(0, 0, 0, 0);
							Debug.Log("The player has a library book.");
						}
					}
				}
			}
		}
		// If Yandere-chan is friends with the student, use the Matchmake protocol.
		else
		{
			// If we're not speaking to Kokona or Riku...
			if (this.Yandere.TargetStudent.StudentID != this.LoveManager.RivalID &&
				this.Yandere.TargetStudent.StudentID != this.LoveManager.SuitorID)
			{
				this.Shadow[5].color = new Color(0, 0, 0, 0.75f);
			}
			// If we ARE spekaing to Kokona or Riku...
			else
			{
				// If we're talking to the girl before we've identified the suitor...
				if (!this.Yandere.TargetStudent.Male && (this.LoveManager.SuitorProgress == 0))
				{
					this.Shadow[5].color = new Color(0, 0, 0, 0.75f);
				}
			}
		}

		// Hiding the Favor button.

		// If the student has not even arrived at school yet, hide the Favor button.
		if (!this.Yandere.TargetStudent.Indoors ||
			this.Yandere.TargetStudent.Club == ClubType.Council)
		{
			UISprite shadow6 = this.Shadow[6];
			shadow6.color = new Color(
				shadow6.color.r,
				shadow6.color.g,
				shadow6.color.b,
				0.75f);
		}
		// If the student is indoors...
		else
		{
			// If Yandere-chan has not befriended this student yet, hide the Favor button.
			if (!PlayerGlobals.GetStudentFriend(this.Yandere.TargetStudent.StudentID))
			{
				this.Shadow[6].color = new Color(0, 0, 0, .75f);
			}
				
			if (this.Yandere.TargetStudent.Male &&
				((this.Yandere.Class.Seduction + this.Yandere.Class.SeductionBonus) > 3) ||
				((this.Yandere.Class.Seduction + this.Yandere.Class.SeductionBonus) > 4) ||
                this.Yandere.Club == ClubType.Delinquent)
			{
				this.Shadow[6].color = new Color(0, 0, 0, 0);
			}

			// If Yandere-chan's target is a delinquent, hide the Favor button.
			if (this.Yandere.TargetStudent.Club == ClubType.Delinquent)
			{
				this.Shadow[6].color = new Color(0, 0, 0, .75f);
			}

			// If Yandere-chan's target is sunbathing, hide the Favor button.
			if (this.Yandere.TargetStudent.CurrentAction == StudentActionType.Sunbathe)
			{
				this.Shadow[6].color = new Color(0, 0, 0, .75f);
			}
		}

		////////////////////////////
		///// CLUB INTERACTION /////
		////////////////////////////

		//If the player is in this club...
		if (this.Yandere.Club == this.Yandere.TargetStudent.Club)
		{
			//Disable the option to ask for information about the club.
			UISprite clubShadow1 = this.ClubShadow[1];
			clubShadow1.color = new Color(
				clubShadow1.color.r,
				clubShadow1.color.g,
				clubShadow1.color.b,
				0.75f);

			//Disable the option to join the club.
			UISprite clubShadow2 = this.ClubShadow[2];
			clubShadow2.color = new Color(
				clubShadow2.color.r,
				clubShadow2.color.g,
				clubShadow2.color.b,
				0.75f);
		}

		//If the player is currently wearing club equipment...
		if (this.Yandere.ClubAttire || (this.Yandere.Mask != null) ||
			(this.Yandere.Gloves != null) || (this.Yandere.Container != null))
		{
			//Disable the option to leave the club.
			UISprite clubShadow3 = this.ClubShadow[3];
			clubShadow3.color = new Color(
				clubShadow3.color.r,
				clubShadow3.color.g,
				clubShadow3.color.b,
				0.75f);
		}

		//If the player is NOT in this club...
		if (this.Yandere.Club != this.Yandere.TargetStudent.Club)
		{
			//Enable the option to join the club.
			UISprite clubShadow2 = this.ClubShadow[2];
			clubShadow2.color = new Color(
				clubShadow2.color.r,
				clubShadow2.color.g,
				clubShadow2.color.b,
				0.00f);

			//Disable the option to quit the club.
			UISprite clubShadow3 = this.ClubShadow[3];
			clubShadow3.color = new Color(
				clubShadow3.color.r,
				clubShadow3.color.g,
				clubShadow3.color.b,
				0.75f);

			//Disable the option to perform a club acitvity.
			this.ClubShadow[5].color = new Color(0, 0, 0, 0.75f);
		}

		//If a murder is about to take place...
		if (this.Yandere.StudentManager.MurderTakingPlace)
		{
			//Disable the option to perform a club acitvity.
			this.ClubShadow[5].color = new Color(0, 0, 0, 0.75f);
		}

		//Disable the "Practice" option for everyone except Budo, unless the police are coming.
		if (this.Yandere.TargetStudent.StudentID != 46 && this.Yandere.TargetStudent.StudentID != 51 || this.Yandere.Police.Show)
		{
			UISprite clubShadow6 = this.ClubShadow[6];
			clubShadow6.color = new Color(
				clubShadow6.color.r,
				clubShadow6.color.g,
				clubShadow6.color.b,
				0.75f);
		}

		//However, if we're talking to the LMC leader and any of the LMC members are not available...
		if (this.Yandere.TargetStudent.StudentID == 51)
		{
			int AvailableStudents = 4;

			if (this.Yandere.Club != ClubType.LightMusic || PracticeWindow.PlayedRhythmMinigame)
			{
				AvailableStudents = 0;
			}

			int TempStudentID = 52;

			while (TempStudentID < 56)
			{
				if (this.Yandere.StudentManager.Students[TempStudentID] == null)
				{
					AvailableStudents--;
				}
				else
				{
					if (!this.Yandere.StudentManager.Students[TempStudentID].gameObject.activeInHierarchy ||
						this.Yandere.StudentManager.Students[TempStudentID].Investigating ||
						this.Yandere.StudentManager.Students[TempStudentID].Distracting ||
						this.Yandere.StudentManager.Students[TempStudentID].Distracted ||
						this.Yandere.StudentManager.Students[TempStudentID].SentHome ||
						this.Yandere.StudentManager.Students[TempStudentID].Tranquil ||
						this.Yandere.StudentManager.Students[TempStudentID].GoAway ||
						!this.Yandere.StudentManager.Students[TempStudentID].Routine ||
						!this.Yandere.StudentManager.Students[TempStudentID].Alive)
					{
						AvailableStudents--;
					}
				}

				TempStudentID++;
			}

			//Enable the shadow.
			if (AvailableStudents < 4)
			{
				UISprite clubShadow6 = this.ClubShadow[6];
					clubShadow6.color = new Color(
						clubShadow6.color.r,
						clubShadow6.color.g,
						clubShadow6.color.b,
						0.75f);
			}
		}

		//////////////////////////////
		///// ASKING FOR A FAVOR /////
		//////////////////////////////

		if (this.Yandere.Followers > 0)
		{
            Debug.Log("Can't do task because of follower."); ;

			UISprite favorShadow1 = this.FavorShadow[1];
			favorShadow1.color = new Color(
				favorShadow1.color.r,
				favorShadow1.color.g,
				favorShadow1.color.b,
				0.75f);
		}

		if (this.Yandere.TargetStudent.DistanceToDestination > 0.50f)
		{
			UISprite favorShadow2 = this.FavorShadow[2];
			favorShadow2.color = new Color(
				favorShadow2.color.r,
				favorShadow2.color.g,
				favorShadow2.color.b,
				0.75f);
		}

		////////////////
		///// LOVE /////
		////////////////

		// Hiding the Appearance button.
		if (!this.Yandere.TargetStudent.Male)
		{
			UISprite loveShadow1 = this.LoveShadow[1];
			loveShadow1.color = new Color(
				loveShadow1.color.r,
				loveShadow1.color.g,
				loveShadow1.color.b,
				0.75f);
		}

		// Hiding the Court button.
		if ((this.DatingMinigame == null) ||
			this.Yandere.TargetStudent.Male && !this.LoveManager.RivalWaiting ||
			this.LoveManager.Courted)
		{
			UISprite loveShadow2 = this.LoveShadow[2];
			loveShadow2.color = new Color(
				loveShadow2.color.r,
				loveShadow2.color.g,
				loveShadow2.color.b,
				0.75f);
		}

		// Hiding the Gift button.
		if (!this.Yandere.TargetStudent.Male || !this.Yandere.Inventory.Rose ||
			this.Yandere.TargetStudent.Rose)
		{
			UISprite loveShadow4 = this.LoveShadow[4];
			loveShadow4.color = new Color(
				loveShadow4.color.r,
				loveShadow4.color.g,
				loveShadow4.color.b,
				0.75f);
		}
	}

	void CheckTaskCompletion()
	{
		Debug.Log("This student's Task Status is: " + TaskGlobals.GetTaskStatus(this.Yandere.TargetStudent.StudentID));

		Debug.Log("Checking for task completion.");

		if (this.Yandere.TargetStudent.StudentID == 6 && TaskGlobals.GetTaskStatus(6) == 1)
		{
			if (this.Yandere.Inventory.Headset)
			{
				this.Yandere.TargetStudent.TaskPhase = 5;
				this.Yandere.LoveManager.SuitorProgress = 1;

				DatingGlobals.SuitorProgress = 1;
			}
		}

		if (this.Yandere.TargetStudent.StudentID == 76 && TaskGlobals.GetTaskStatus(76) == 1)
		{
			this.Yandere.TargetStudent.RespectEarned = true;
			this.Yandere.TargetStudent.TaskPhase = 5;
			this.Yandere.Inventory.Money -= 100;
			this.Yandere.Inventory.UpdateMoney();
		}
		else if (this.Yandere.TargetStudent.StudentID == 77 && TaskGlobals.GetTaskStatus(77) == 1)
		{
			this.Yandere.TargetStudent.RespectEarned = true;
			this.Yandere.TargetStudent.TaskPhase = 5;
			WeaponScript NewWeapon;

			if (this.Yandere.Weapon[1] != null && this.Yandere.Weapon[1].WeaponID == 1 ||
				this.Yandere.Weapon[1] != null && this.Yandere.Weapon[1].WeaponID == 8)
			{
				NewWeapon = this.Yandere.Weapon[1];
				this.Yandere.Weapon[1] = null;
			}
			else
			{
				NewWeapon = this.Yandere.Weapon[2];
				this.Yandere.Weapon[2] = null;
			}

			NewWeapon.Drop();
			NewWeapon.FingerprintID = 77;
			NewWeapon.gameObject.SetActive(false);

			this.Yandere.WeaponManager.UpdateLabels();
			this.Yandere.WeaponMenu.UpdateSprites();
		}
		else if (this.Yandere.TargetStudent.StudentID == 78 && TaskGlobals.GetTaskStatus(78) == 1)
		{
			this.Yandere.TargetStudent.RespectEarned = true;
			this.Yandere.TargetStudent.TaskPhase = 5;
			this.Yandere.Inventory.Sake = false;
		}
		else if (this.Yandere.TargetStudent.StudentID == 79 && TaskGlobals.GetTaskStatus(79) == 1)
		{
			this.Yandere.TargetStudent.RespectEarned = true;
			this.Yandere.TargetStudent.TaskPhase = 5;
			this.Yandere.Inventory.Cigs = false;
		}
		else if (this.Yandere.TargetStudent.StudentID == 80 && TaskGlobals.GetTaskStatus(80) == 1)
		{
			this.Yandere.TargetStudent.RespectEarned = true;
			this.Yandere.TargetStudent.TaskPhase = 5;
			this.Yandere.Inventory.AnswerSheet = false;
		}

		bool generic = false;

		if ((this.Yandere.TargetStudent.StudentID != 8) &&
			(this.Yandere.TargetStudent.StudentID != 11) &&
			(this.Yandere.TargetStudent.StudentID != 25) &&
			(this.Yandere.TargetStudent.StudentID != 28) &&
			(this.Yandere.TargetStudent.StudentID != 30) &&
			(this.Yandere.TargetStudent.StudentID != 36) &&
			(this.Yandere.TargetStudent.StudentID != 37) &&
			(this.Yandere.TargetStudent.StudentID != 38) &&
			(this.Yandere.TargetStudent.StudentID != 52) &&
			(this.Yandere.TargetStudent.StudentID != 76) &&
			(this.Yandere.TargetStudent.StudentID != 77) &&
			(this.Yandere.TargetStudent.StudentID != 78) &&
			(this.Yandere.TargetStudent.StudentID != 79) &&
			(this.Yandere.TargetStudent.StudentID != 80) &&
			(this.Yandere.TargetStudent.StudentID != 81))
		{
			generic = true;
		}

		if (generic)
		{
			if (TaskGlobals.GetTaskStatus(this.Yandere.TargetStudent.StudentID) == 1)
			{
				if (this.Yandere.Inventory.Book)
				{
					this.Yandere.TargetStudent.TaskPhase = 5;
				}
			}
		}

		if (this.Yandere.Club == ClubType.Delinquent)
		{
			this.Text[6] = "Intimidate";
		}
		else
		{
			this.Text[6] = "Ask Favor";
		}
	}

	public void End()
	{
		if (this.Yandere.TargetStudent != null)
		{
			if (this.Yandere.TargetStudent.Pestered >= 10)
			{
				this.Yandere.TargetStudent.Ignoring = true;
			}

			if (!this.Pestered)
			{
				this.Yandere.Subtitle.Label.text = string.Empty;
			}

			this.Yandere.TargetStudent.Interaction = 0;
			this.Yandere.TargetStudent.WaitTimer = 1.0f;

			if (this.Yandere.TargetStudent.enabled)
			{
				//Debug.Log(this.Yandere.TargetStudent.Name + " has been told to travel to the destination of their current phase.");

				this.Yandere.TargetStudent.CurrentDestination =
					this.Yandere.TargetStudent.Destinations[this.Yandere.TargetStudent.Phase];
				this.Yandere.TargetStudent.Pathfinding.target =
					this.Yandere.TargetStudent.Destinations[this.Yandere.TargetStudent.Phase];

				if (this.Yandere.TargetStudent.Actions[this.Yandere.TargetStudent.Phase] == StudentActionType.Clean)
				{
					this.Yandere.TargetStudent.EquipCleaningItems();
				}

				if (this.Yandere.TargetStudent.Actions[this.Yandere.TargetStudent.Phase] == StudentActionType.Patrol)
				{
					this.Yandere.TargetStudent.CurrentDestination =
						this.Yandere.TargetStudent.StudentManager.Patrols.List[this.Yandere.TargetStudent.StudentID].GetChild(this.Yandere.TargetStudent.PatrolID);
					this.Yandere.TargetStudent.Pathfinding.target = this.Yandere.TargetStudent.CurrentDestination;
				}

				if (this.Yandere.TargetStudent.Actions[this.Yandere.TargetStudent.Phase] == StudentActionType.Sleuth)
				{
					this.Yandere.TargetStudent.CurrentDestination = this.Yandere.TargetStudent.SleuthTarget;
					this.Yandere.TargetStudent.Pathfinding.target = this.Yandere.TargetStudent.SleuthTarget;
				}

				if (this.Yandere.TargetStudent.Actions[this.Yandere.TargetStudent.Phase] == StudentActionType.Sunbathe)
				{
					if (this.Yandere.TargetStudent.SunbathePhase > 1)
					{
						this.Yandere.TargetStudent.CurrentDestination = this.Yandere.StudentManager.SunbatheSpots[this.Yandere.TargetStudent.StudentID];
						this.Yandere.TargetStudent.Pathfinding.target = this.Yandere.StudentManager.SunbatheSpots[this.Yandere.TargetStudent.StudentID];
					}
				}
			}

			if (this.Yandere.TargetStudent.Persona == PersonaType.PhoneAddict)
			{
				bool Sunbathing = false;

				if (this.Yandere.TargetStudent.CurrentAction == StudentActionType.Sunbathe && this.Yandere.TargetStudent.SunbathePhase > 2)
				{
					Sunbathing = true;
				}

				//if (this.Yandere.TargetStudent.Actions[this.Yandere.TargetStudent.Phase] != StudentActionType.Clean)
				if (!this.Yandere.TargetStudent.Scrubber.activeInHierarchy &&
					!Sunbathing && !this.Yandere.TargetStudent.Phoneless)
				{
					this.Yandere.TargetStudent.SmartPhone.SetActive(true);
					this.Yandere.TargetStudent.WalkAnim = this.Yandere.TargetStudent.PhoneAnims[1];
				}
				else
				{
					this.Yandere.TargetStudent.SmartPhone.SetActive(false);
				}
			}

			if (this.Yandere.TargetStudent.LostTeacherTrust)
			{
				this.Yandere.TargetStudent.WalkAnim = this.Yandere.TargetStudent.BulliedWalkAnim;
				this.Yandere.TargetStudent.SmartPhone.SetActive(false);
			}

			if (this.Yandere.TargetStudent.EatingSnack)
			{
				this.Yandere.TargetStudent.Scrubber.SetActive(false);
				this.Yandere.TargetStudent.Eraser.SetActive(false);
			}

			if (this.Yandere.TargetStudent.SentToLocker)
			{
				this.Yandere.TargetStudent.CurrentDestination = this.Yandere.TargetStudent.MyLocker;
				this.Yandere.TargetStudent.Pathfinding.target = this.Yandere.TargetStudent.MyLocker;
			}

			this.Yandere.TargetStudent.Talk.NegativeResponse = false;
			this.Yandere.ShoulderCamera.OverShoulder = false;
			this.Yandere.TargetStudent.Waiting = true;
			this.Yandere.TargetStudent = null;
		}

		this.Yandere.StudentManager.VolumeUp();

		this.Jukebox.Dip = 1;

		this.AskingFavor = false;
		this.Matchmaking = false;
		this.ClubLeader = false;
		this.Pestered = false;
		this.Show = false;
	}
}
