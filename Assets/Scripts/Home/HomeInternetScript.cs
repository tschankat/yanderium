using UnityEngine;

public class HomeInternetScript : MonoBehaviour
{
	public StudentInfoMenuScript StudentInfoMenu;
	public InputManagerScript InputManager;
	public PauseScreenScript PauseScreen;
	public PromptBarScript PromptBar;
	public HomeClockScript Clock;

	public HomeYandereScript HomeYandere;
	public HomeCameraScript HomeCamera;
	public HomeWindowScript HomeWindow;

	public UILabel YanderePostLabel;
	public UILabel YancordLabel;
	public UILabel AcceptLabel;

	public UITexture YancordLogo;

	public GameObject InternetPrompts;
	public GameObject NavigationMenu;
	public GameObject OnlineShopping;
	public GameObject SocialMedia;
	public GameObject NewPostText;
	public GameObject ChangeLabel;
	public GameObject ChangeIcon;
	public GameObject WriteLabel;
	public GameObject WriteIcon;
	public GameObject PostLabel;
	public GameObject PostIcon;
	public GameObject BG;

	public Transform MenuHighlight;
	public Transform StudentPost1;
	public Transform StudentPost2;
	public Transform YandereReply;
	public Transform YanderePost;
	public Transform LameReply;
	public Transform NewPost;
	public Transform Menu;

	public Transform[] StudentReplies;

	public UISprite[] Highlights;
	public UILabel[] PostLabels;

	public UILabel[] MenuLabels;

	public string[] Locations;
	public string[] Actions;

	public bool PostSequence = false;
	public bool WritingPost = false;
	public bool ShowMenu = false;
	public bool FadeOut = false;
	public bool Success = false;
	public bool Posted = false;

	public int MenuSelected = 1;
	public int Selected = 1;
	public int ID = 1;

	public int Location = 0;
	public int Student = 0;
	public int Action = 0;

	public float Timer = 0.0f;

	public UITexture StudentPost1Portrait;
	public UITexture StudentPost2Portrait;
	public UITexture LamePostPortrait;
	public Texture CurrentPortrait;

	public UITexture[] Portraits;

	public int Height = 0;
	public Transform Highlight;
	public Transform ItemList;
	public GameObject AreYouSure;
	public AudioSource MyAudio;
	public UILabel MoneyLabel;
	public float Shake;

	void Awake()
	{
		this.StudentPost1.localPosition = new Vector3(
			this.StudentPost1.localPosition.x,
			-180.0f,
			this.StudentPost1.localPosition.z);

		this.StudentPost2.localPosition = new Vector3(
			this.StudentPost2.localPosition.x,
			-365.0f,
			this.StudentPost2.localPosition.z);

		this.YandereReply.localPosition = new Vector3(
			this.YandereReply.localPosition.x,
			-88.0f,
			this.YandereReply.localPosition.z);

		this.YanderePost.localPosition = new Vector3(
			this.YanderePost.localPosition.x,
			-2.0f,
			this.YanderePost.localPosition.z);

		// [af] Moved assignments into for loop.
		for (int i = 1; i < 6; i++)
		{
			Transform replyTransform = this.StudentReplies[i];
			replyTransform.localPosition = new Vector3(
				replyTransform.localPosition.x,
				-40.0f,
				replyTransform.localPosition.z);
		}

		this.LameReply.localPosition = new Vector3(
			this.LameReply.localPosition.x,
			-40.0f,
			this.LameReply.localPosition.z);

		this.Highlights[1].enabled = false;
		this.Highlights[2].enabled = false;
		this.Highlights[3].enabled = false;

		// [af] Added "gameObject" for C# compatibility.
		this.YanderePost.gameObject.SetActive(false);

		this.NavigationMenu.SetActive(true);

		this.ChangeLabel.SetActive(false);
		this.ChangeIcon.SetActive(false);
		this.PostLabel.SetActive(false);
		this.PostIcon.SetActive(false);

		this.OnlineShopping.SetActive(false);
		this.NewPostText.SetActive(false);
		this.BG.SetActive(false);

		if (!EventGlobals.Event2 || StudentGlobals.GetStudentExposed(30))
		{
			this.WriteLabel.SetActive(false);
			this.WriteIcon.SetActive(false);
		}

		//Sakyu
		GetPortrait(2);
		StudentPost1Portrait.mainTexture = CurrentPortrait;

		//Midori
		GetPortrait(39);
		StudentPost2Portrait.mainTexture = CurrentPortrait;

		//Saki Miyu
		GetPortrait(25);
		LamePostPortrait.mainTexture = CurrentPortrait;

		ID = 1;

		while (ID < 6)
		{
			GetPortrait(86 - ID);
			Portraits[ID].mainTexture = CurrentPortrait;

			ID++;
		}

		if (!DateGlobals.DayPassed)
		{
			this.YancordLabel.color = new Color(1, 1, 1, .2f);
			this.YancordLogo.color = new Color(1, 1, 1, .2f);
		}
	}

	void Update()
	{
		if (!this.HomeYandere.CanMove)
		{
			if (!this.PauseScreen.Show)
			{
				if (this.NavigationMenu.activeInHierarchy && !this.HomeCamera.CyberstalkWindow.activeInHierarchy)
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.NavigationMenu.SetActive(false);
						this.SocialMedia.SetActive(true);
					}
					else if (Input.GetButtonDown(InputNames.Xbox_X))
					{
						if (DateGlobals.DayPassed)
						{
							this.HomeCamera.HomeDarkness.FadeOut = true;
						}
					}
					else if (Input.GetButtonDown(InputNames.Xbox_Y))
					{
						this.PauseScreen.MainMenu.SetActive(false);
						this.PauseScreen.Panel.enabled = true;
						this.PauseScreen.Sideways = true;
						this.PauseScreen.Show = true;

						this.StudentInfoMenu.gameObject.SetActive(true);
						this.StudentInfoMenu.CyberStalking = true;
						this.StartCoroutine(this.StudentInfoMenu.UpdatePortraits());

						this.PromptBar.ClearButtons();
						this.PromptBar.Label[0].text = "View Info";
						this.PromptBar.Label[1].text = "Back";
						this.PromptBar.UpdateButtons();
						this.PromptBar.Show = true;
					}
					else if (Input.GetButtonDown(InputNames.Xbox_LB))
					{
						this.NavigationMenu.SetActive(false);
						this.OnlineShopping.SetActive(true);

						this.MoneyLabel.text = "$" + PlayerGlobals.Money.ToString("F2", System.Globalization.NumberFormatInfo.InvariantInfo);
                    }
					else if (Input.GetButtonDown(InputNames.Xbox_B))
					{
						this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
						this.HomeCamera.Target = this.HomeCamera.Targets[0];
						this.HomeYandere.CanMove = true;
						this.HomeWindow.Show = false;
						this.enabled = false;
					}
				}
				else if (this.SocialMedia.activeInHierarchy)
				{
					// [af] Replaced if/else statement with assignment and ternary expression.
					this.Menu.localScale = Vector3.Lerp(
						this.Menu.localScale,
						this.ShowMenu ? new Vector3(1.0f, 1.0f, 1.0f) : Vector3.zero,
						Time.deltaTime * 10.0f);

					if (this.WritingPost)
					{
						this.NewPost.transform.localPosition = Vector3.Lerp(
							this.NewPost.transform.localPosition,
							Vector3.zero,
							Time.deltaTime * 10.0f);
						this.NewPost.transform.localScale = Vector3.Lerp(
							this.NewPost.transform.localScale,
							new Vector3(2.43f, 2.43f, 2.43f),
							Time.deltaTime * 10.0f);

						// [af] Converted while loop to for loop.
						for (int ID = 1; ID < this.Highlights.Length; ID++)
						{
							UISprite highlight = this.Highlights[ID];

							// [af] Replaced if/else statement with assignment and ternary expression.
							highlight.color = new Color(
								highlight.color.r,
								highlight.color.g,
								highlight.color.b,
								Mathf.MoveTowards(highlight.color.a, this.FadeOut ? 0.0f : 1.0f, Time.deltaTime));
						}

						if (this.Highlights[this.Selected].color.a == 1.0f)
						{
							this.FadeOut = true;
						}
						else if (this.Highlights[this.Selected].color.a == 0.0f)
						{
							this.FadeOut = false;
						}

						if (!this.ShowMenu)
						{
							if (this.InputManager.TappedRight)
							{
								this.Selected++;
								this.UpdateHighlight();
							}

							if (this.InputManager.TappedLeft)
							{
								this.Selected--;
								this.UpdateHighlight();
							}
						}
						else
						{
							if (this.InputManager.TappedDown)
							{
								this.MenuSelected++;
								this.UpdateMenuHighlight();
							}

							if (this.InputManager.TappedUp)
							{
								this.MenuSelected--;
								this.UpdateMenuHighlight();
							}
						}
					}
					else
					{
						this.NewPost.transform.localPosition = Vector3.Lerp(
							this.NewPost.transform.localPosition,
							new Vector3(175.0f, -10.0f, 0.0f),
							Time.deltaTime * 10.0f);
						this.NewPost.transform.localScale = Vector3.Lerp(
							this.NewPost.transform.localScale,
							new Vector3(1.0f, 1.0f, 1.0f),
							Time.deltaTime * 10.0f);
					}

					if (!this.PostSequence)
					{
						if (Input.GetButtonDown(InputNames.Xbox_A))
						{
							if (this.WriteIcon.activeInHierarchy)
							{
								if (!this.Posted)
								{
									if (!this.ShowMenu)
									{
										if (!this.WritingPost)
										{
											this.AcceptLabel.text = "Select";

											this.ChangeLabel.SetActive(true);
											this.ChangeIcon.SetActive(true);

											this.NewPostText.SetActive(true);
											this.BG.SetActive(true);

											this.WritingPost = true;

											this.Selected = 1;

											this.UpdateHighlight();
										}
										else
										{
											if (this.Selected == 1)
											{
												this.PauseScreen.MainMenu.SetActive(false);
												this.PauseScreen.Panel.enabled = true;
												this.PauseScreen.Sideways = true;
												this.PauseScreen.Show = true;

												this.StudentInfoMenu.gameObject.SetActive(true);
												this.StudentInfoMenu.CyberBullying = true;
												this.StartCoroutine(this.StudentInfoMenu.UpdatePortraits());

												this.PromptBar.ClearButtons();
												this.PromptBar.Label[0].text = "View Info";
												this.PromptBar.Label[1].text = "Back";
												this.PromptBar.UpdateButtons();
												this.PromptBar.Show = true;
											}
											else if (this.Selected == 2)
											{
												this.MenuSelected = 1;
												this.UpdateMenuHighlight();
												this.ShowMenu = true;

												// [af] Converted while loop to for loop.
												for (int ID = 1; ID < this.MenuLabels.Length; ID++)
												{
													this.MenuLabels[ID].text = this.Locations[ID];
												}
											}
											else if (this.Selected == 3)
											{
												this.MenuSelected = 1;
												this.UpdateMenuHighlight();
												this.ShowMenu = true;

												// [af] Converted while loop to for loop.
												for (int ID = 1; ID < this.MenuLabels.Length; ID++)
												{
													this.MenuLabels[ID].text = this.Actions[ID];
												}
											}
										}
									}
									else
									{
										if (this.Selected == 2)
										{
											this.Location = this.MenuSelected;

											this.PostLabels[2].text = this.Locations[this.MenuSelected];
											this.ShowMenu = false;
										}
										else if (this.Selected == 3)
										{
											this.Action = this.MenuSelected;

											this.PostLabels[3].text = this.Actions[this.MenuSelected];
											this.ShowMenu = false;
										}

										this.CheckForCompletion();
									}
								}
							}
						}

						if (Input.GetButtonDown(InputNames.Xbox_B))
						{
							if (!this.ShowMenu)
							{
								if (!this.WritingPost)
								{
									this.NavigationMenu.SetActive(true);
									this.SocialMedia.SetActive(false);
								}
								else
								{
									this.AcceptLabel.text = "Write";

									this.ChangeLabel.SetActive(false);
									this.ChangeIcon.SetActive(false);
									this.PostLabel.SetActive(false);
									this.PostIcon.SetActive(false);

									this.ExitPost();
								}
							}
							else
							{
								this.ShowMenu = false;
							}
						}

						if (Input.GetButtonDown(InputNames.Xbox_X))
						{
							if (this.PostIcon.activeInHierarchy)
							{
								this.YanderePostLabel.text = "Today, I saw " + this.PostLabels[1].text +
									" in " + this.PostLabels[2].text + ". She was " + this.PostLabels[3].text + ".";

								this.ExitPost();

								this.InternetPrompts.SetActive(false);
								this.ChangeLabel.SetActive(false);
								this.ChangeIcon.SetActive(false);
								this.WriteLabel.SetActive(false);
								this.WriteIcon.SetActive(false);
								this.PostLabel.SetActive(false);
								this.PostIcon.SetActive(false);
								this.PostSequence = true;

								this.Posted = true;

								if ((this.Student == 30) && (this.Location == 7) && (this.Action == 9))
								{
									this.Success = true;
								}
							}
						}

						if (Input.GetKeyDown("space"))
						{
							this.WriteLabel.SetActive(true);
							this.WriteIcon.SetActive(true);
						}
					}

					if (this.PostSequence)
					{
						if (Input.GetButtonDown(InputNames.Xbox_A))
						{
							this.Timer += 2.0f;
						}

						this.Timer += Time.deltaTime;

						if ((this.Timer > 1.0f) && (this.Timer < 3.0f))
						{
							// [af] Added "gameObject" for C# compatibility.
							this.YanderePost.gameObject.SetActive(true);

							this.YanderePost.transform.localPosition = new Vector3(
								this.YanderePost.transform.localPosition.x,
								Mathf.Lerp(this.YanderePost.transform.localPosition.y, -155.0f, Time.deltaTime * 10.0f),
								this.YanderePost.transform.localPosition.z);

							this.StudentPost1.transform.localPosition = new Vector3(
								this.StudentPost1.transform.localPosition.x,
								Mathf.Lerp(this.StudentPost1.transform.localPosition.y, -365.0f, Time.deltaTime * 10.0f),
								this.StudentPost1.transform.localPosition.z);

							this.StudentPost2.transform.localPosition = new Vector3(
								this.StudentPost2.transform.localPosition.x,
								Mathf.Lerp(this.StudentPost2.transform.localPosition.y, -550.0f, Time.deltaTime * 10.0f),
								this.StudentPost2.transform.localPosition.z);
						}

						if (!this.Success)
						{
							if ((this.Timer > 3.0f) && (this.Timer < 5.0f))
							{
								this.LameReply.localPosition = new Vector3(
									this.LameReply.localPosition.x,
									Mathf.Lerp(this.LameReply.transform.localPosition.y, -88.0f, Time.deltaTime * 10.0f),
									this.LameReply.localPosition.z);

								this.YandereReply.localPosition = new Vector3(
									this.YandereReply.localPosition.x,
									Mathf.Lerp(this.YandereReply.transform.localPosition.y, -137.0f, Time.deltaTime * 10.0f),
									this.YandereReply.localPosition.z);

								this.StudentPost1.localPosition = new Vector3(
									this.StudentPost1.localPosition.x,
									Mathf.Lerp(this.StudentPost1.transform.localPosition.y, -415.0f, Time.deltaTime * 10.0f),
									this.StudentPost1.localPosition.z);
							}

							if (this.Timer > 5.0f)
							{
								PlayerGlobals.Reputation -= 5.0f;
								this.InternetPrompts.SetActive(true);
								this.PostSequence = false;
							}
						}
						else
						{
							if ((this.Timer > 3.0f) && (this.Timer < 5.0f))
							{
								Transform reply = this.StudentReplies[1];
								reply.localPosition = new Vector3(
									reply.localPosition.x,
									Mathf.Lerp(reply.transform.localPosition.y, -88.0f, Time.deltaTime * 10.0f),
									reply.localPosition.z);

								this.YandereReply.localPosition = new Vector3(
									this.YandereReply.localPosition.x,
									Mathf.Lerp(this.YandereReply.transform.localPosition.y, -137.0f, Time.deltaTime * 10.0f),
									this.YandereReply.localPosition.z);

								this.StudentPost1.localPosition = new Vector3(
									this.StudentPost1.localPosition.x,
									Mathf.Lerp(this.StudentPost1.transform.localPosition.y, -415.0f, Time.deltaTime * 10.0f),
									this.StudentPost1.localPosition.z);
							}

							if ((this.Timer > 5.0f) && (this.Timer < 7.0f))
							{
								Transform reply2 = this.StudentReplies[2];
								reply2.localPosition = new Vector3(
									reply2.localPosition.x,
									Mathf.Lerp(reply2.transform.localPosition.y, -88.0f, Time.deltaTime * 10.0f),
									reply2.localPosition.z);

								Transform reply1 = this.StudentReplies[1];
								reply1.localPosition = new Vector3(
									reply1.localPosition.x,
									Mathf.Lerp(reply1.transform.localPosition.y, -136.0f, Time.deltaTime * 10.0f),
									reply1.localPosition.z);

								this.YandereReply.localPosition = new Vector3(
									this.YandereReply.localPosition.x,
									Mathf.Lerp(this.YandereReply.transform.localPosition.y, -185.0f, Time.deltaTime * 10.0f),
									this.YandereReply.localPosition.z);

								this.StudentPost1.localPosition = new Vector3(
									this.StudentPost1.localPosition.x,
									Mathf.Lerp(this.StudentPost1.transform.localPosition.y, -465.0f, Time.deltaTime * 10.0f),
									this.StudentPost1.localPosition.z);
							}

							if ((this.Timer > 7.0f) && (this.Timer < 9.0f))
							{
								Transform reply3 = this.StudentReplies[3];
								reply3.localPosition = new Vector3(
									reply3.localPosition.x,
									Mathf.Lerp(reply3.transform.localPosition.y, -88.0f, Time.deltaTime * 10.0f),
									reply3.localPosition.z);

								Transform reply2 = this.StudentReplies[2];
								reply2.localPosition = new Vector3(
									reply2.localPosition.x,
									Mathf.Lerp(reply2.transform.localPosition.y, -136.0f, Time.deltaTime * 10.0f),
									reply2.localPosition.z);

								Transform reply1 = this.StudentReplies[1];
								reply1.localPosition = new Vector3(
									reply1.localPosition.x,
									Mathf.Lerp(reply1.transform.localPosition.y, -184.0f, Time.deltaTime * 10.0f),
									reply1.localPosition.z);

								this.YandereReply.localPosition = new Vector3(
									this.YandereReply.localPosition.x,
									Mathf.Lerp(this.YandereReply.transform.localPosition.y, -233.0f, Time.deltaTime * 10.0f),
									this.YandereReply.localPosition.z);

								this.StudentPost1.localPosition = new Vector3(
									this.StudentPost1.localPosition.x,
									Mathf.Lerp(this.StudentPost1.transform.localPosition.y, -510.0f, Time.deltaTime * 10.0f),
									this.StudentPost1.localPosition.z);
							}

							if ((this.Timer > 9.0f) && (this.Timer < 11.0f))
							{
								Transform reply4 = this.StudentReplies[4];
								reply4.localPosition = new Vector3(
									reply4.localPosition.x,
									Mathf.Lerp(reply4.transform.localPosition.y, -88.0f, Time.deltaTime * 10.0f),
									reply4.localPosition.z);

								Transform reply3 = this.StudentReplies[3];
								reply3.localPosition = new Vector3(
									reply3.localPosition.x,
									Mathf.Lerp(reply3.transform.localPosition.y, -136.0f, Time.deltaTime * 10.0f),
									reply3.localPosition.z);

								Transform reply2 = this.StudentReplies[2];
								reply2.localPosition = new Vector3(
									reply2.localPosition.x,
									Mathf.Lerp(reply2.transform.localPosition.y, -184.0f, Time.deltaTime * 10.0f),
									reply2.localPosition.z);

								Transform reply1 = this.StudentReplies[1];
								reply1.localPosition = new Vector3(
									reply1.localPosition.x,
									Mathf.Lerp(reply1.transform.localPosition.y, -232.0f, Time.deltaTime * 10.0f),
									reply1.localPosition.z);

								this.YandereReply.localPosition = new Vector3(
									this.YandereReply.localPosition.x,
									Mathf.Lerp(this.YandereReply.transform.localPosition.y, -281.0f, Time.deltaTime * 10.0f),
									this.YandereReply.localPosition.z);

								this.StudentPost1.localPosition = new Vector3(
									this.StudentPost1.localPosition.x,
									Mathf.Lerp(this.StudentPost1.transform.localPosition.y, -560.0f, Time.deltaTime * 10.0f),
									this.StudentPost1.localPosition.z);
							}

							if ((this.Timer > 11.0f) && (this.Timer < 13.0f))
							{
								Transform reply5 = this.StudentReplies[5];
								reply5.localPosition = new Vector3(
									reply5.localPosition.x,
									Mathf.Lerp(reply5.transform.localPosition.y, -88.0f, Time.deltaTime * 10.0f),
									reply5.localPosition.z);

								Transform reply4 = this.StudentReplies[4];
								reply4.localPosition = new Vector3(
									reply4.localPosition.x,
									Mathf.Lerp(reply4.transform.localPosition.y, -136.0f, Time.deltaTime * 10.0f),
									reply4.localPosition.z);

								Transform reply3 = this.StudentReplies[3];
								reply3.localPosition = new Vector3(
									reply3.localPosition.x,
									Mathf.Lerp(reply3.transform.localPosition.y, -184.0f, Time.deltaTime * 10.0f),
									reply3.localPosition.z);

								Transform reply2 = this.StudentReplies[2];
								reply2.localPosition = new Vector3(
									reply2.localPosition.x,
									Mathf.Lerp(reply2.transform.localPosition.y, -232.0f, Time.deltaTime * 10.0f),
									reply2.localPosition.z);

								Transform reply1 = this.StudentReplies[1];
								reply1.localPosition = new Vector3(
									reply1.localPosition.x,
									Mathf.Lerp(reply1.transform.localPosition.y, -280.0f, Time.deltaTime * 10.0f),
									reply1.localPosition.z);

								this.YandereReply.localPosition = new Vector3(
									this.YandereReply.localPosition.x,
									Mathf.Lerp(this.YandereReply.transform.localPosition.y, -329.0f, Time.deltaTime * 10.0f),
									this.YandereReply.localPosition.z);
							}

							if (this.Timer > 13.0f)
							{
								StudentGlobals.SetStudentExposed(30, true);
								StudentGlobals.SetStudentReputation(30, 
									StudentGlobals.GetStudentReputation(30) - 50);
								this.InternetPrompts.SetActive(true);
								this.PostSequence = false;
							}
						}
					}
				}
				else if (this.OnlineShopping.activeInHierarchy)
				{
					if (Input.GetKeyDown("m"))
					{
						PlayerGlobals.Money = 100;
					}

					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						if (this.Height == 0 || this.Height > 1)
						{
							if (PlayerGlobals.Money > 33.33f)
							{
								if (!this.AreYouSure.activeInHierarchy)
								{
									this.AreYouSure.SetActive(true);
								}
								else
								{
									this.AreYouSure.SetActive(false);
									GameGlobals.SpareUniform = true;
									PlayerGlobals.Money -= 33.33f;
									this.MyAudio.Play();

									this.MoneyLabel.text = "$" + PlayerGlobals.Money.ToString("F2", System.Globalization.NumberFormatInfo.InvariantInfo);
                                    this.Clock.UpdateMoneyLabel();
								}
							}
							else
							{
								this.Shake = 10;
							}
						}
						else if (this.Height == 1)
						{
							if (PlayerGlobals.Money > 8.49f)
							{
								if (!this.AreYouSure.activeInHierarchy)
								{
									this.AreYouSure.SetActive(true);
								}
								else
								{
									this.AreYouSure.SetActive(false);
									GameGlobals.BlondeHair = true;
									PlayerGlobals.Money -= 8.49f;
									this.MyAudio.Play();

									this.MoneyLabel.text = "$" + PlayerGlobals.Money.ToString("F2", System.Globalization.NumberFormatInfo.InvariantInfo);
                                    this.Clock.UpdateMoneyLabel();
								}
							}
							else
							{
								this.Shake = 10;
							}
						}
					}

					this.Shake = Mathf.MoveTowards(this.Shake, 0, Time.deltaTime * 10);

					this.MoneyLabel.transform.localPosition = new Vector3(
						445 + Random.Range(this.Shake * -1.0f, this.Shake * 1.0f),
						410 + Random.Range(this.Shake * -1.0f, this.Shake * 1.0f),
						0);

					if (Input.GetButtonDown(InputNames.Xbox_B))
					{
						if (!this.AreYouSure.activeInHierarchy)
						{
							this.NavigationMenu.SetActive(true);
							this.OnlineShopping.SetActive(false);
						}
						else
						{
							this.AreYouSure.SetActive(false);
						}
					}

					if (InputManager.TappedDown)
					{
						this.Height++;
					}

					if (InputManager.TappedUp)
					{
						this.Height--;
					}

					if (this.Height < 0)
					{
						this.Height = 0;
					}

					if (this.Height > 4)
					{
						this.Height = 4;
					}

					if (this.Height == 0)
					{
						this.Highlight.localPosition = Vector3.Lerp(this.Highlight.localPosition,
							new Vector3(
								this.Highlight.localPosition.x,
								130,
								this.Highlight.localPosition.z),
							Time.deltaTime * 10);
					}
					else if (this.Height > 0)
					{
						this.Highlight.localPosition = Vector3.Lerp(this.Highlight.localPosition,
							new Vector3(
								this.Highlight.localPosition.x,
								-85,
								this.Highlight.localPosition.z),
							Time.deltaTime * 10);
					}

					if (this.Height < 2)
					{
						this.ItemList.localPosition = Vector3.Lerp(this.ItemList.localPosition,
							new Vector3(
								this.ItemList.localPosition.x,
								130,
								this.ItemList.localPosition.z),
							Time.deltaTime * 10);
					}
					else
					{
						this.ItemList.localPosition = Vector3.Lerp(this.ItemList.localPosition,
							new Vector3(
								this.ItemList.localPosition.x,
								130 + (215 * (Height - 1)),
								this.ItemList.localPosition.z),
							Time.deltaTime * 10);
					}
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			StudentGlobals.SetStudentExposed(7, false);
		}
	}

	void ExitPost()
	{
		this.Highlights[1].enabled = false;
		this.Highlights[2].enabled = false;
		this.Highlights[3].enabled = false;

		this.NewPostText.SetActive(false);
		this.BG.SetActive(false);

		this.PostLabels[1].text = string.Empty;
		this.PostLabels[2].text = string.Empty;
		this.PostLabels[3].text = string.Empty;

		this.WritingPost = false;
	}

	void UpdateHighlight()
	{
		if (this.Selected > 3)
		{
			this.Selected = 1;
		}

		if (this.Selected < 1)
		{
			this.Selected = 3;
		}

		this.Highlights[1].enabled = false;
		this.Highlights[2].enabled = false;
		this.Highlights[3].enabled = false;

		this.Highlights[this.Selected].enabled = true;
	}

	void UpdateMenuHighlight()
	{
		if (this.MenuSelected > 10)
		{
			this.MenuSelected = 1;
		}

		if (this.MenuSelected < 1)
		{
			this.MenuSelected = 10;
		}
		
		this.MenuHighlight.transform.localPosition = new Vector3(
			this.MenuHighlight.transform.localPosition.x,
			220.0f - (40.0f * this.MenuSelected),
			this.MenuHighlight.transform.localPosition.z);
	}

	void CheckForCompletion()
	{
		if ((this.PostLabels[1].text != string.Empty) &&
			(this.PostLabels[2].text != string.Empty) &&
			(this.PostLabels[3].text != string.Empty))
		{
			this.PostLabel.SetActive(true);
			this.PostIcon.SetActive(true);
		}
	}

	void GetPortrait(int ID)
	{
		string path = "file:///" + Application.streamingAssetsPath + "/Portraits/Student_" + ID.ToString() + ".png";

		WWW www = new WWW(path);

		this.CurrentPortrait = www.texture;
	}
}