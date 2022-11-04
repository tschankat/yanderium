using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenScript : MonoBehaviour
{
	public StudentInfoMenuScript StudentInfoMenu;
    public InputManagerScript InputManager;
    public PhotoGalleryScript PhotoGallery;
	public SaveLoadMenuScript SaveLoadMenu;
	public HomeYandereScript HomeYandere;
    public InputDeviceScript InputDevice;
    public MissionModeScript MissionMode;
	public HomeCameraScript HomeCamera;
	public ServicesScript ServiceMenu;
	public FavorMenuScript FavorMenu;
	public AudioMenuScript AudioMenu;
	public PromptBarScript PromptBar;
    public TaskListScript Tutorials;
    public PassTimeScript PassTime;
	public SettingsScript Settings;
	public TaskListScript TaskList;
	public SchemesScript Schemes;
	public YandereScript Yandere;
	public RPG_Camera RPGCamera;
	public PoliceScript Police;
	public ClockScript Clock;
	public StatsScript Stats;
	public Blur ScreenBlur;
	public MapScript Map;

	public UILabel SelectionLabel;
	public UIPanel Panel;
	public UISprite Wifi;

	public GameObject NewMissionModeWindow;
	public GameObject MissionModeLabel;
	public GameObject MissionModeIcons;
	public GameObject LoadingScreen;
    public GameObject ControlMenu;
    public GameObject SchemesMenu;
	public GameObject StudentInfo;
	public GameObject DropsMenu;
	public GameObject MainMenu;

    public GameObject Keyboard;
    public GameObject Gamepad;

    public Transform PromptParent;

	public string[] SelectionNames;
	public UISprite[] PhoneIcons;
	public Transform[] Eggs;

	public int Prompts = 0;
	public int Selected = 1;
	public float Speed = 0.0f;

	public bool ShowMissionModeDetails = false;
    public bool ViewingControlMenu = false;
    public bool CorrectingTime = false;
	public bool MultiMission = false;
	public bool BypassPhone = false;
	public bool EggsChecked = false;
    public bool AtSchool = false;
    public bool PressedA = false;
	public bool PressedB = false;
	public bool Quitting = false;
	public bool Sideways = false;
    public bool InEditor = false;
    public bool Home = false;
	public bool Show = false;

	void Start()
	{
        #if UNITY_EDITOR
        this.InEditor = true;
        #endif

        if (SceneManager.GetActiveScene().name != SceneNames.SchoolScene)
		{
			MissionModeGlobals.MultiMission = false;
		}
        else
        {
            AtSchool = true;
        }

		if (!MissionModeGlobals.MultiMission)
		{
			this.MissionModeLabel.SetActive(false);
		}

		this.MultiMission = MissionModeGlobals.MultiMission;

		StudentGlobals.SetStudentPhotographed(0, true);
		StudentGlobals.SetStudentPhotographed(1, true);

		this.transform.localPosition = new Vector3(1350.0f, 0.0f, 0.0f);
		this.transform.localScale = new Vector3(0.9133334f, 0.9133334f, 0.9133334f);
		this.transform.localEulerAngles = new Vector3(
			this.transform.localEulerAngles.x, this.transform.localEulerAngles.y, 0.0f);

		this.StudentInfoMenu.gameObject.SetActive(false);
		this.PhotoGallery.gameObject.SetActive(false);
		this.SaveLoadMenu.gameObject.SetActive(false);
		this.ServiceMenu.gameObject.SetActive(false);
        this.AudioMenu.gameObject.SetActive(false);
        this.FavorMenu.gameObject.SetActive(false);
        this.Tutorials.gameObject.SetActive(false);
        this.PassTime.gameObject.SetActive(false);
		this.Settings.gameObject.SetActive(false);
		this.TaskList.gameObject.SetActive(false);
		this.Stats.gameObject.SetActive(false);
		this.LoadingScreen.SetActive(false);
        this.ControlMenu.SetActive(false);
        this.SchemesMenu.SetActive(false);
		this.StudentInfo.SetActive(false);
		this.DropsMenu.SetActive(false);
		this.MainMenu.SetActive(true);

		if (SceneManager.GetActiveScene().name == SceneNames.SchoolScene)
		{
			this.Schemes.UpdateInstructions();
		}
        //If we are NOT at school...
		else
		{
			this.MissionModeIcons.SetActive(false);

            //Info-chan Favors
			UISprite phoneIcon5 = this.PhoneIcons[5];
			phoneIcon5.color = new Color(
				phoneIcon5.color.r, phoneIcon5.color.g, phoneIcon5.color.b, 0.50f);

            //Save
			UISprite phoneIcon9 = this.PhoneIcons[9];
			phoneIcon9.color = new Color(
				phoneIcon9.color.r, phoneIcon9.color.g, phoneIcon9.color.b, 0.50f);
            
            //Music
            UISprite phoneIcon11 = this.PhoneIcons[11];
            phoneIcon11.color = new Color(
                phoneIcon11.color.r, phoneIcon11.color.g, phoneIcon11.color.b, .50f);

            if (NewMissionModeWindow != null)
			{
				NewMissionModeWindow.SetActive(false);
			}
		}

		if (MissionModeGlobals.MissionMode)
		{
            //Load
			UISprite phoneIcon7 = this.PhoneIcons[7];
			phoneIcon7.color = new Color(
				phoneIcon7.color.r, phoneIcon7.color.g, phoneIcon7.color.b, 0.50f);

            //Save
			UISprite phoneIcon9 = this.PhoneIcons[9];
			phoneIcon9.color = new Color(
				phoneIcon9.color.r, phoneIcon9.color.g, phoneIcon9.color.b, 0.50f);

            //Music
			UISprite phoneIcon10 = this.PhoneIcons[10];
			phoneIcon10.color = new Color(
				phoneIcon10.color.r, phoneIcon10.color.g, phoneIcon10.color.b, 1.00f);
        }

		this.UpdateSelection();
		this.CorrectingTime = false;
	}

	void Update()
	{
		this.Speed = Time.unscaledDeltaTime * 10.0f;

		if (!this.Police.FadeOut)
		{
			if (!this.Map.Show)
			{
				if (!this.Show)
				{
					if (this.transform.localPosition.x > 1349.0f)
					{
						if (this.Panel.enabled)
						{
							this.Panel.enabled = false;

							this.transform.localPosition = new Vector3(1350.0f, 50.0f, 0.0f);

							this.transform.localScale = new Vector3(0.9133334f, 0.9133334f, 0.9133334f);

							this.transform.localEulerAngles = new Vector3(0, 0, 0);
						}
					}
					else
					{
						this.transform.localPosition = Vector3.Lerp(
							this.transform.localPosition, new Vector3(1350.0f, 50.0f, 0.0f), this.Speed);

						this.transform.localScale = Vector3.Lerp(
							this.transform.localScale, new Vector3(0.9133334f, 0.9133334f, 0.9133334f), this.Speed);

						this.transform.localEulerAngles = new Vector3(
							this.transform.localEulerAngles.x,
							this.transform.localEulerAngles.y,
							Mathf.Lerp(this.transform.localEulerAngles.z, 0.0f, this.Speed));
					}

					if (this.CorrectingTime)
					{
						if (Time.timeScale < 0.90f)
						{
							Time.timeScale = Mathf.Lerp(Time.timeScale, 1.0f, this.Speed);

							if (Time.timeScale > 0.90f)
							{
								this.CorrectingTime = false;
								Time.timeScale = 1.0f;
							}
						}
					}

					if (Input.GetButtonDown(InputNames.Xbox_Start))
					{
						if (!this.Home)
						{
							if (!this.Yandere.Shutter.Snapping && !this.Yandere.TimeSkipping &&
								!this.Yandere.Talking && !this.Yandere.Noticed &&
								!this.Yandere.InClass && !this.Yandere.Struggling &&
								!this.Yandere.Won && !this.Yandere.Dismembering &&
								!this.Yandere.Attacked && this.Yandere.CanMove &&
								(Time.timeScale > 0.0001f))
							{
								this.Yandere.StopAiming();

								this.PromptParent.localScale = Vector3.zero;
								this.Yandere.Obscurance.enabled = false;
								this.Yandere.YandereVision = false;
								this.ScreenBlur.enabled = true;
								this.Yandere.YandereTimer = 0.0f;
								this.Yandere.Mopping = false;
								this.Panel.enabled = true;
								this.Sideways = false;
								this.Show = true;

								this.PromptBar.ClearButtons();
								this.PromptBar.Label[0].text = "Accept";
								this.PromptBar.Label[1].text = "Exit";
								this.PromptBar.Label[4].text = "Choose";
								this.PromptBar.Label[5].text = "Choose";
								this.PromptBar.UpdateButtons();
								this.PromptBar.Show = true;

								UISprite phoneIcon3 = this.PhoneIcons[3];
								if (!this.Yandere.CanMove || this.Yandere.Dragging ||
									((this.Police.Corpses - this.Police.HiddenCorpses) > 0) &&
									(!this.Police.SuicideScene && !this.Police.PoisonScene))
								{
									phoneIcon3.color = new Color(
										phoneIcon3.color.r, phoneIcon3.color.g,
										phoneIcon3.color.b, 0.50f);
								}
								else
								{
									phoneIcon3.color = new Color(
										phoneIcon3.color.r, phoneIcon3.color.g,
										phoneIcon3.color.b, 1.0f);
								}

                                this.CheckIfSavePossible();
                                this.UpdateSelection();
                            }
						}
						else
						{
							if (this.HomeCamera.Destination == this.HomeCamera.Destinations[0])
							{
								this.PromptBar.ClearButtons();
								this.PromptBar.Label[0].text = "Accept";
								this.PromptBar.Label[1].text = "Exit";
								this.PromptBar.Label[4].text = "Choose";
								this.PromptBar.UpdateButtons();
								this.PromptBar.Show = true;

								this.HomeYandere.CanMove = false;

								UISprite phoneIcon3 = this.PhoneIcons[3];
								phoneIcon3.color = new Color(
										phoneIcon3.color.r, phoneIcon3.color.g,
										phoneIcon3.color.b, 0.50f);

								this.Panel.enabled = true;
								this.Sideways = false;
								this.Show = true;
							}
						}
					}
				}
				else
				{
					if (!this.EggsChecked)
					{
						float ClosestEgg = 99999.0f;

						// [af] Converted while loop to for loop.
						for (int ID = 0; ID < this.Eggs.Length; ID++)
						{
							if (this.Eggs[ID] != null)
							{
								float CurrentEgg = Vector3.Distance(
									this.Yandere.transform.position, this.Eggs[ID].position);

								if (CurrentEgg < ClosestEgg)
								{
									ClosestEgg = CurrentEgg;
								}
							}
						}

						if (ClosestEgg < 5.0f)
						{
							this.Wifi.spriteName = "5Bars";
						}
						else if (ClosestEgg < 10.0f)
						{
							this.Wifi.spriteName = "4Bars";
						}
						else if (ClosestEgg < 15.0f)
						{
							this.Wifi.spriteName = "3Bars";
						}
						else if (ClosestEgg < 20.0f)
						{
							this.Wifi.spriteName = "2Bars";
						}
						else if (ClosestEgg < 25.0f)
						{
							this.Wifi.spriteName = "1Bars";
						}
						else
						{
							this.Wifi.spriteName = "0Bars";
						}

						this.EggsChecked = true;
					}

					if (!this.Home)
					{
						Time.timeScale = Mathf.Lerp(Time.timeScale, 0.0f, this.Speed);
						this.RPGCamera.enabled = false;
					}

					if (this.ShowMissionModeDetails)
					{
						this.transform.localScale = Vector3.Lerp(
							this.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f), this.Speed);
						
						this.transform.localPosition = Vector3.Lerp(
							this.transform.localPosition, new Vector3(0.0f, 1200.0f, 0.0f), this.Speed);
					}
					else if (this.Quitting)
					{
						this.transform.localScale = Vector3.Lerp(
							this.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f), this.Speed);
						
						this.transform.localPosition = Vector3.Lerp(
							this.transform.localPosition, new Vector3(0.0f, -1200.0f, 0.0f), this.Speed);
					}
					else
					{
						if (!this.Sideways)
						{
							if (!this.Settings.gameObject.activeInHierarchy)
							{
								this.transform.localPosition = Vector3.Lerp(
									this.transform.localPosition, new Vector3(0.0f, 50.0f, 0.0f), this.Speed);
							}
							//If we're looking at the Settings menu specifically...
							else
							{
								this.transform.localPosition = Vector3.Lerp(
									this.transform.localPosition, new Vector3(-762.5f, 50.0f, 0.0f), this.Speed);
							}

							this.transform.localScale = Vector3.Lerp(
								this.transform.localScale, new Vector3(0.9133334f, 0.9133334f, 0.9133334f), this.Speed);

							this.transform.localEulerAngles = new Vector3(
								this.transform.localEulerAngles.x,
								this.transform.localEulerAngles.y,
								Mathf.Lerp(this.transform.localEulerAngles.z, 0.0f, this.Speed));
						}
						else
						{
							this.transform.localScale = Vector3.Lerp(
								this.transform.localScale, new Vector3(1.78f, 1.78f, 1.78f), this.Speed);
							this.transform.localPosition = Vector3.Lerp(
								this.transform.localPosition, new Vector3(0.0f, 35.0f, 0.0f), this.Speed);
							this.transform.localEulerAngles = new Vector3(
								this.transform.localEulerAngles.x,
								this.transform.localEulerAngles.y,
								Mathf.Lerp(this.transform.localEulerAngles.z, 90.0f, this.Speed));
						}
					}

					if (this.MainMenu.activeInHierarchy && !this.Quitting)
					{
						//if (this.InputManager.TappedUp || Input.GetKeyDown(KeyCode.W) ||
						//	Input.GetKeyDown(KeyCode.UpArrow))
						if (this.InputManager.TappedUp)
						{
							this.Row--;
							this.UpdateSelection();
						}

						//if (this.InputManager.TappedDown || Input.GetKeyDown(KeyCode.S) ||
						//	Input.GetKeyDown(KeyCode.DownArrow))
						if (this.InputManager.TappedDown)
						{
							this.Row++;
							this.UpdateSelection();
						}

						//if (this.InputManager.TappedRight || Input.GetKeyDown(KeyCode.D) ||
						//	Input.GetKeyDown(KeyCode.RightArrow))
						if (this.InputManager.TappedRight)
						{
							this.Column++;
							this.UpdateSelection();
						}

						//if (this.InputManager.TappedLeft || Input.GetKeyDown(KeyCode.A) ||
						//	Input.GetKeyDown(KeyCode.LeftArrow))
						if (this.InputManager.TappedLeft)
						{
							this.Column--;
							this.UpdateSelection();
						}

						if (Input.GetKeyDown("space"))
						{
							if (this.MultiMission)
							{
								this.ShowMissionModeDetails = !this.ShowMissionModeDetails;
							}
						}

						if (this.ShowMissionModeDetails)
						{
							if (Input.GetButtonDown(InputNames.Xbox_B))
							{
								this.ShowMissionModeDetails = false;
							}
						}

                        // [af] Converted while loop to for loop.
                        for (int ID = 1; ID < this.PhoneIcons.Length; ID++)
						{
							if (this.PhoneIcons[ID] != null)
							{
								// [af] Converted if/else statement to assignment with ternary expression.
								Vector3 lerpEnd = (this.Selected != ID) ?
									(new Vector3(1.0f, 1.0f, 1.0f)) : (new Vector3(1.50f, 1.50f, 1.50f));
								this.PhoneIcons[ID].transform.localScale = Vector3.Lerp(
									this.PhoneIcons[ID].transform.localScale, lerpEnd, this.Speed);
							}
						}

						if (!ShowMissionModeDetails)
						{
							if (Input.GetButtonDown(InputNames.Xbox_A))
							{
								this.PressedA = true;

                                if (this.PhoneIcons[this.Selected].color.a == 1.0f)// || InEditor)
								{
									// Photo Gallery.
									if (this.Selected == 1)
									{
										this.MainMenu.SetActive(false);
										this.LoadingScreen.SetActive(true);

										this.PromptBar.ClearButtons();
										this.PromptBar.Label[1].text = "Back";
										this.PromptBar.Label[4].text = "Choose";
										this.PromptBar.Label[5].text = "Choose";
										this.PromptBar.UpdateButtons();

										this.StartCoroutine(this.PhotoGallery.GetPhotos());
									}
									// Tasks.
									else if (this.Selected == 2)
									{
										this.TaskList.gameObject.SetActive(true);
										this.MainMenu.SetActive(false);
										this.Sideways = true;

										this.PromptBar.ClearButtons();
										this.PromptBar.Label[1].text = "Back";
										this.PromptBar.Label[4].text = "Choose";
										this.PromptBar.UpdateButtons();

										this.TaskList.UpdateTaskList();
										this.StartCoroutine(this.TaskList.UpdateTaskInfo());
									}
									// Pass Time.
									else if (this.Selected == 3)
									{
										if (this.PhoneIcons[3].color.a == 1.0f)
										{
											if (this.Yandere.CanMove && !this.Yandere.Dragging)
											{
												// [af] Converted while loop to for loop.
												for (int ID = 0; ID < this.Yandere.ArmedAnims.Length; ID++)
												{
													this.Yandere.CharacterAnimation[this.Yandere.ArmedAnims[ID]].weight = 0.0f;
												}

												this.MainMenu.SetActive(false);

												this.PromptBar.ClearButtons();
												this.PromptBar.Label[0].text = "Begin";
												this.PromptBar.Label[1].text = "Back";
												this.PromptBar.Label[4].text = "Adjust";
												this.PromptBar.Label[5].text = "Choose";
												this.PromptBar.UpdateButtons();

												this.PassTime.gameObject.SetActive(true);

												this.PassTime.GetCurrentTime();
											}
										}
									}
									// Stats.
									else if (this.Selected == 4)
									{
										this.PromptBar.ClearButtons();
										this.PromptBar.Label[1].text = "Exit";
										this.PromptBar.UpdateButtons();

										this.Stats.gameObject.SetActive(true);
										this.Stats.UpdateStats();
										this.MainMenu.SetActive(false);
										this.Sideways = true;
									}
									// Favors.
									else if (this.Selected == 5)
									{
										if (this.PhoneIcons[5].color.a == 1.0f)
										{
											this.PromptBar.ClearButtons();
											this.PromptBar.Label[0].text = "Accept";
											this.PromptBar.Label[1].text = "Exit";
											this.PromptBar.Label[5].text = "Choose";
											this.PromptBar.UpdateButtons();

											this.FavorMenu.gameObject.SetActive(true);
											this.FavorMenu.gameObject.GetComponent<AudioSource>().Play();
											this.MainMenu.SetActive(false);
											this.Sideways = true;
										}
									}
									// Student Info.
									else if (this.Selected == 6)
									{
										this.StudentInfoMenu.gameObject.SetActive(true);
										this.StartCoroutine(this.StudentInfoMenu.UpdatePortraits());
										this.MainMenu.SetActive(false);
										this.Sideways = true;

										this.PromptBar.ClearButtons();
										this.PromptBar.Label[0].text = "View Info";
										this.PromptBar.Label[1].text = "Back";
										this.PromptBar.UpdateButtons();
										this.PromptBar.Show = true;
									}
									// Load.
									else if (this.Selected == 7)
									{
										this.SaveLoadMenu.gameObject.SetActive(true);
										this.SaveLoadMenu.Header.text = "Load Data";
										this.SaveLoadMenu.Loading = true;
										this.SaveLoadMenu.Saving = false;

										this.SaveLoadMenu.Column = 1;
										this.SaveLoadMenu.Row = 1;
										this.SaveLoadMenu.UpdateHighlight();

										this.StartCoroutine(this.SaveLoadMenu.GetThumbnails());
										this.MainMenu.SetActive(false);
										this.Sideways = true;

										this.PromptBar.ClearButtons();
										this.PromptBar.Label[0].text = "Choose";
										this.PromptBar.Label[1].text = "Back";
										this.PromptBar.Label[2].text = "Debug";
										this.PromptBar.Label[4].text = "Change";
										this.PromptBar.Label[5].text = "Change";
										this.PromptBar.UpdateButtons();
										this.PromptBar.Show = true;
									}
									// Settings.
									else if (this.Selected == 8)
									{
										this.Settings.gameObject.SetActive(true);

										if (this.ScreenBlur != null)
										{
											this.ScreenBlur.enabled = false;
										}

										this.Settings.UpdateText();
										this.MainMenu.SetActive(false);

										this.PromptBar.ClearButtons();
										this.PromptBar.Label[1].text = "Back";
										this.PromptBar.Label[4].text = "Choose";
										this.PromptBar.Label[5].text = "Change";
										this.PromptBar.UpdateButtons();
										this.PromptBar.Show = true;
									}
									// Save.
									else if (this.Selected == 9)
									{
										this.SaveLoadMenu.gameObject.SetActive(true);
										this.SaveLoadMenu.Header.text = "Save Data";
										this.SaveLoadMenu.Loading = false;
										this.SaveLoadMenu.Saving = true;

										this.SaveLoadMenu.Column = 1;
										this.SaveLoadMenu.Row = 1;
										this.SaveLoadMenu.UpdateHighlight();

										this.StartCoroutine(this.SaveLoadMenu.GetThumbnails());
										this.MainMenu.SetActive(false);
										this.Sideways = true;

										this.PromptBar.ClearButtons();
										this.PromptBar.Label[0].text = "Choose";
										this.PromptBar.Label[1].text = "Back";
                                        this.PromptBar.Label[2].text = "Delete";
                                        this.PromptBar.Label[4].text = "Change";
										this.PromptBar.Label[5].text = "Change";
										this.PromptBar.UpdateButtons();
										this.PromptBar.Show = true;
									}
									// Audio
									else if (this.Selected == 10)
									{
										if (!MissionModeGlobals.MissionMode)
										{
											this.AudioMenu.gameObject.SetActive(true);
											this.AudioMenu.UpdateText();
											this.MainMenu.SetActive(false);

											this.PromptBar.ClearButtons();
											this.PromptBar.Label[0].text = "Play";
											this.PromptBar.Label[1].text = "Back";
											this.PromptBar.Label[4].text = "Choose";
											this.PromptBar.UpdateButtons();
											this.PromptBar.Show = true;
										}
										else
										{
											this.PhoneIcons[this.Selected].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
											this.MissionMode.ChangeMusic();
										}
									}
                                    // Tutorials.
                                    else if (this.Selected == 11)
                                    {
                                        this.Tutorials.gameObject.SetActive(true);
                                        this.MainMenu.SetActive(false);
                                        this.Sideways = true;

                                        this.PromptBar.ClearButtons();
                                        this.PromptBar.Label[1].text = "Back";
                                        this.PromptBar.Label[4].text = "Choose";
                                        this.PromptBar.UpdateButtons();

                                        this.Tutorials.UpdateTaskList();
                                        this.StartCoroutine(this.Tutorials.UpdateTaskInfo());
                                    }
                                    // Controls.
                                    else if (this.Selected == 12)
									{
                                        if (this.InputDevice.Type == InputDeviceType.Gamepad)
                                        {
                                            this.Keyboard.SetActive(false);
                                            this.Gamepad.SetActive(true);
                                        }
                                        else
                                        {
                                            this.Keyboard.SetActive(true);
                                            this.Gamepad.SetActive(false);
                                        }

                                        this.ControlMenu.SetActive(false);

                                        this.ControlMenu.SetActive(true);
                                        this.MainMenu.SetActive(false);
                                        this.ViewingControlMenu = true;
                                        this.Sideways = true;

                                        this.PromptBar.ClearButtons();
                                        this.PromptBar.Label[1].text = "Back";
                                        this.PromptBar.UpdateButtons();
                                        this.PromptBar.Show = true;
                                    }
                                    // Quitting.
                                    else if (this.Selected == 14)
                                    {
                                        this.PromptBar.ClearButtons();
                                        this.PromptBar.Show = false;
                                        this.Quitting = true;
                                    }
                                }
							}
							else if (!this.PressedB)
							{
								if (Input.GetButtonDown(InputNames.Xbox_Start) || Input.GetButtonDown(InputNames.Xbox_B))
								{
									this.ExitPhone();
								}
							}
							else if (Input.GetButtonUp(InputNames.Xbox_B))
							{
								this.PressedB = false;
							}
						}
					}

					if (!this.PressedA)
					{
						// [af] Added "gameObject" for C# compatibility.
						if (this.PassTime.gameObject.activeInHierarchy)
						{
							if (Input.GetButtonDown(InputNames.Xbox_A))
							{
								if (this.Yandere.PickUp != null)
								{
									this.Yandere.PickUp.Drop();
								}

								this.Yandere.Unequip();

								this.ScreenBlur.enabled = false;
								this.RPGCamera.enabled = true;

								// [af] Added "gameObject" for C# compatibility.
								this.PassTime.gameObject.SetActive(false);

								this.MainMenu.SetActive(true);
								this.PromptBar.Show = false;
								this.Show = false;

								this.Clock.TargetTime = this.PassTime.TargetTime;
                                this.Clock.StopTime = false;
                                this.Clock.TimeSkip = true;

								Time.timeScale = 1.0f;
							}
							else if (Input.GetButtonDown(InputNames.Xbox_B))
							{
								this.MainMenu.SetActive(true);

								this.PromptBar.ClearButtons();
								this.PromptBar.Label[0].text = "Accept";
								this.PromptBar.Label[1].text = "Exit";
								this.PromptBar.Label[4].text = "Choose";
								this.PromptBar.Label[5].text = "Choose";
								this.PromptBar.UpdateButtons();

								// [af] Added "gameObject" for C# compatibility.
								this.PassTime.gameObject.SetActive(false);
							}
						}

                        if (this.ViewingControlMenu)
                        {
                            if (this.InputDevice.Type == InputDeviceType.Gamepad)
                            {
                                this.Keyboard.SetActive(false);
                                this.Gamepad.SetActive(true);
                            }
                            else
                            {
                                this.Keyboard.SetActive(true);
                                this.Gamepad.SetActive(false);
                            }

                            if (Input.GetButtonDown(InputNames.Xbox_B))
                            {
                                this.MainMenu.SetActive(true);

                                this.PromptBar.ClearButtons();
                                this.PromptBar.Label[0].text = "Accept";
                                this.PromptBar.Label[1].text = "Exit";
                                this.PromptBar.Label[4].text = "Choose";
                                this.PromptBar.Label[5].text = "Choose";
                                this.PromptBar.UpdateButtons();

                                this.ControlMenu.SetActive(false);
                                this.ViewingControlMenu = false;
                                this.Sideways = false;
                            }
                        }

						if (this.Quitting)
						{
							if (Input.GetButtonDown(InputNames.Xbox_A))
							{
								SceneManager.LoadScene(SceneNames.TitleScene);
							}
							else if (Input.GetButtonDown(InputNames.Xbox_B))
							{
								this.PromptBar.ClearButtons();
								this.PromptBar.Label[0].text = "Accept";
								this.PromptBar.Label[1].text = "Exit";
								this.PromptBar.Label[4].text = "Choose";
								this.PromptBar.Label[5].text = "Choose";
								this.PromptBar.UpdateButtons();
								this.PromptBar.Show = true;

								this.Quitting = false;

								if (this.BypassPhone)
								{
									this.transform.localPosition = new Vector3(1350.0f, 0.0f, 0.0f);

									this.ExitPhone();
								}
							}
						}
					}

					if (Input.GetButtonUp(InputNames.Xbox_A))
					{
						this.PressedA = false;
					}
				}
			}
		}
	}

	public void JumpToQuit()
	{
		if (!this.Police.FadeOut && !this.Clock.TimeSkip && !this.Yandere.Noticed)
		{
			this.transform.localPosition = new Vector3(0.0f, -1200.0f, 0.0f);
			this.Yandere.YandereVision = false;

			if (!this.Yandere.Talking && !this.Yandere.Dismembering)
			{
				this.RPGCamera.enabled = false;
				this.Yandere.StopAiming();
			}

			this.ScreenBlur.enabled = true;
			this.Panel.enabled = true;
			this.BypassPhone = true;
			this.Quitting = true;
			this.Show = true;
		}
	}

	public void ExitPhone()
	{
		if (!this.Home)
		{
			this.PromptParent.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			this.ScreenBlur.enabled = false;
			this.CorrectingTime = true;

			if (!this.Yandere.Talking && !this.Yandere.Dismembering)
			{
				this.RPGCamera.enabled = true;
			}

			if (this.Yandere.Laughing)
			{
				this.Yandere.GetComponent<AudioSource>().volume = 1.0f;
			}
		}
		else
		{
			this.HomeYandere.CanMove = true;
		}

		this.PromptBar.ClearButtons();
		this.PromptBar.Show = false;
		this.BypassPhone = false;
		this.EggsChecked = false;
		this.PressedA = false;
		this.Show = false;
	}

	public int Row = 1;
	public int Column = 2;

	void UpdateSelection()
	{
		if (this.Row < 0)
		{
			this.Row = 4;
		}
		else if (this.Row > 4)
		{
			this.Row = 0;
		}

		if (this.Column < 1)
		{
			this.Column = 3;
		}
		else if (this.Column > 3)
		{
			this.Column = 1;
		}

		this.Selected = (this.Row * 3) + this.Column;

		this.SelectionLabel.text = this.SelectionNames[this.Selected];

        if (this.AtSchool && this.Selected == 9 && this.PhoneIcons[9].color.a == .5f)
        {
            this.SelectionLabel.text = this.Reason;
        }
	}

    public string Reason;

    void CheckIfSavePossible()
    {
        this.PhoneIcons[9].color = new Color(1, 1, 1, 1);

        if (this.AtSchool)
        {
            for (int ID = 1; ID < this.Yandere.StudentManager.Students.Length; ID++)
            {
                if (this.Yandere.StudentManager.Students[ID] != null)
                {
                    if (this.Yandere.StudentManager.Students[ID].Alive)
                    {
                        if (this.Yandere.StudentManager.Students[ID].Alarmed ||
                            this.Yandere.StudentManager.Students[ID].Fleeing)
                        {
                            this.PhoneIcons[9].color = new Color(1, 1, 1, .5f);
                            this.Reason = "You cannot save the game while a student is alarmed or fleeing.";
                        }

                        if (this.Yandere.PickUp != null)
                        {
                            this.PhoneIcons[9].color = new Color(1, 1, 1, .5f);
                            this.Reason = "You cannot save the game while you are holding that object.";
                        }
                    }
                }
            }
        }
    }
}