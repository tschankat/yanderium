using UnityEngine;

public class ShutterScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public TaskManagerScript TaskManager;
	public PauseScreenScript PauseScreen;
	public StudentInfoScript StudentInfo;
	public PromptBarScript PromptBar;
	public SubtitleScript Subtitle;
	public SchemesScript Schemes;
	public StudentScript Student;
	public YandereScript Yandere;

	public StudentScript FaceStudent;

	public RenderTexture SmartphoneScreen;
	public Camera SmartphoneCamera;
	public Transform TextMessages;
	public Transform ErrorWindow;
	public Camera MainCamera;

	public UILabel PhotoDescLabel;
	public UISprite Sprite;

	public GameObject NotificationManager;
	public GameObject BullyPhotoCollider;
	public GameObject PhotoDescription;
	public GameObject HeartbeatCamera;
	public GameObject CameraButtons;
	public GameObject NewMessage;
	public GameObject PhotoIcons;
	public GameObject MainMenu;
	public GameObject SubPanel;
	public GameObject Message;
	public GameObject Panel;

	public GameObject ViolenceX;
	public GameObject PantiesX;
	public GameObject SenpaiX;
	public GameObject BullyX;
	public GameObject InfoX;

	public bool AirGuitarShot = false;
	public bool DisplayError = false;
	public bool MissionMode = false;
	public bool HorudaShot = false;
	public bool KittenShot = false;
	public bool OsanaShot = false;
	public bool FreeSpace = false;
	public bool TakePhoto = false;
	public bool TookPhoto = false;
	public bool Snapping = false;
	public bool Close = false;

	public bool Disguise = false;
	public bool Nemesis = false;
	public bool NotFace = false;
	public bool Skirt = false;

	public RaycastHit hit;

	public float ReactionDistance = 0.0f;
	public float PenaltyTimer = 0.0f;
	public float Timer = 0.0f;

	// [af] Value for incrementing current shutter frame. It is decremented by 1 
	// while greater than 1 for each frame increment.
	float currentPercent;

	public int TargetStudent = 0;
	public int NemesisShots = 0;
	public int Frame = 0;
	public int Slot = 0;
	public int ID = 0;

	public int OnlyPhotography { get { return (1 << 16) | (1 << 0); } }
	public int OnlyCharacters { get { return (1 << 9) | (1 << 0); } }
	public int OnlyRagdolls { get { return (1 << 11) | (1 << 0); } }
	public int OnlyBlood { get { return (1 << 14) | (1 << 0); } }

	public AudioSource MyAudio;

	public Transform SelfieRayParent;

	void Start()
	{
		if (MissionModeGlobals.MissionMode)
		{
			this.MissionMode = true;
		}

		this.ErrorWindow.transform.localScale = Vector3.zero;
		this.CameraButtons.SetActive(false);
		this.PhotoIcons.SetActive(false);
		this.Sprite.color = new Color(
			this.Sprite.color.r,
			this.Sprite.color.g,
			this.Sprite.color.b,
			0.0f);
	}

	void Update()
	{
		if (!this.Yandere.Selfie)
		{
			//Debug.DrawRay(SmartphoneCamera.transform.position, SmartphoneCamera.transform.TransformDirection(Vector3.forward) * 10, Color.green);
		}
		else
		{
			//Debug.DrawRay(SmartphoneCamera.transform.position, SelfieRayParent.TransformDirection(Vector3.forward) * 10, Color.green);
		}

		if (this.Snapping)
		{
			if (this.Yandere.Noticed)
			{
				this.Yandere.Shutter.ResumeGameplay();
				this.Yandere.StopAiming();
			}
			else
			{
				const float shutterFPS = 60.0f;
				const int minFrame = 1;
				const int maxFrame = 8;

				if (this.Close)
				{
					this.currentPercent += shutterFPS * Time.unscaledDeltaTime;
					
					while (this.currentPercent >= 1.0f)
					{
						this.Frame = Mathf.Min(this.Frame + 1, maxFrame);
						this.currentPercent -= 1.0f;
					}

					this.Sprite.spriteName = "Shutter" + this.Frame.ToString();

					if (this.Frame == 8)
					{
						// [af] Commented in JS code.
						//StudentManager.GhostChan.GhostEye.eulerAngles = StudentManager.GhostChan.GhostEyeLocation.eulerAngles;
						//StudentManager.GhostChan.GhostEye.position = StudentManager.GhostChan.GhostEyeLocation.position;

						// [af] Added "gameObject" for C# compatibility.
						this.StudentManager.GhostChan.gameObject.SetActive(true);

						this.PhotoDescription.SetActive(false);
						this.PhotoDescLabel.text = "";

						this.StudentManager.GhostChan.Look();
						this.CheckPhoto();

						if (PhotoDescLabel.text == "")
						{
							this.PhotoDescLabel.text = "Cannot determine subject of photo. Try again.";
						}

						this.PhotoDescription.SetActive(true);

						this.SmartphoneCamera.targetTexture = null;
						this.Yandere.PhonePromptBar.Show = false;
						this.NotificationManager.SetActive(false);
						this.HeartbeatCamera.SetActive(false);

						// [af] Commented in JS code.
						//CameraButtons.active = true;

						this.Yandere.SelfieGuide.SetActive(false);
						this.MainCamera.enabled = false;
						this.PhotoIcons.SetActive(true);
						this.SubPanel.SetActive(false);
						this.Panel.SetActive(false);
						this.Close = false;

						this.PromptBar.ClearButtons();
						this.PromptBar.Label[0].text = "Save";
						this.PromptBar.Label[1].text = "Delete";

						if (!this.Yandere.RivalPhone)
						{
							this.PromptBar.Label[2].text = "Send";
						}
						else
						{
							if (this.PantiesX.activeInHierarchy)
							{
								this.PromptBar.Label[0].text = "";
							}
						}

						this.PromptBar.UpdateButtons();
						this.PromptBar.Show = true;

						Time.timeScale = 0.0001f;
					}
				}
				else
				{
					this.currentPercent += shutterFPS * Time.unscaledDeltaTime;

					while (this.currentPercent >= 1.0f)
					{
						this.Frame = Mathf.Max(this.Frame - 1, minFrame);
						this.currentPercent -= 1.0f;
					}

					this.Sprite.spriteName = "Shutter" + this.Frame.ToString();

					if (this.Frame == 1)
					{
						this.Sprite.color = new Color(
							this.Sprite.color.r,
							this.Sprite.color.g,
							this.Sprite.color.b,
							0.0f);

						this.Snapping = false;
					}
				}
			}
		}
		else
		{
			if (this.Yandere.Aiming)
			{
				this.TargetStudent = 0;

				this.Timer += Time.deltaTime;

				if (this.Timer > 0.50f)
				{
					Vector3 dir;

					if (!this.Yandere.Selfie)
					{
						dir = SmartphoneCamera.transform.TransformDirection(Vector3.forward);
					}
					else
					{
						dir = SelfieRayParent.TransformDirection(Vector3.forward);
					}

					if (Physics.Raycast(this.SmartphoneCamera.transform.position, dir, out this.hit, Mathf.Infinity, this.OnlyPhotography))
					{
						if (this.hit.collider.gameObject.name == "Face")
						{
							GameObject TempStudent1 = this.hit.collider.gameObject.transform.root.gameObject;
							this.FaceStudent = TempStudent1.GetComponent<StudentScript>();

							if (this.FaceStudent != null)
							{
								this.TargetStudent = this.FaceStudent.StudentID;

								if (this.TargetStudent > 1)
								{
									this.ReactionDistance = 1.66666f;
								}
								else
								{
									this.ReactionDistance = this.FaceStudent.VisionDistance;
								}

								bool ShoeRemovalDisabled;

								ShoeRemovalDisabled = this.FaceStudent.ShoeRemoval.enabled;

								if (!this.FaceStudent.Alarmed && !this.FaceStudent.Dying &&
									!this.FaceStudent.Distracted && !this.FaceStudent.InEvent &&
									!this.FaceStudent.Wet && this.FaceStudent.Schoolwear > 0 &&
									!this.FaceStudent.Fleeing && !this.FaceStudent.Following &&
									!ShoeRemovalDisabled && !this.FaceStudent.HoldingHands &&
									(this.FaceStudent.Actions[this.FaceStudent.Phase] != StudentActionType.Mourn) && 
									!this.FaceStudent.Guarding && !this.FaceStudent.Confessing &&
									!this.FaceStudent.DiscCheck && !this.FaceStudent.TurnOffRadio &&
									!this.FaceStudent.Investigating && !this.FaceStudent.Distracting &&
                                    !this.FaceStudent.WitnessedLimb && !this.FaceStudent.WitnessedWeapon &&
                                    !this.FaceStudent.WitnessedBloodPool && !this.FaceStudent.WitnessedBloodyWeapon)
                                {
									if (Vector3.Distance(this.Yandere.transform.position,
										TempStudent1.transform.position) < this.ReactionDistance)
									{
										if (this.FaceStudent.CanSeeObject(this.Yandere.gameObject, 
											this.Yandere.transform.position + Vector3.up))
										{
											if (this.MissionMode)
											{
												this.PenaltyTimer += Time.deltaTime;

												if (this.PenaltyTimer > 1.0f)
												{
													this.FaceStudent.Reputation.PendingRep -= -10.0f;
													this.PenaltyTimer = 0.0f;
												}
											}

											if (!this.FaceStudent.CameraReacting)
											{
												if (this.FaceStudent.enabled)
												{
													if (!this.FaceStudent.Stop)
													{
														if (this.FaceStudent.DistanceToDestination < 5 &&
															this.FaceStudent.Actions[this.FaceStudent.Phase] == StudentActionType.Graffiti || 
															this.FaceStudent.DistanceToDestination < 5 &&
															this.FaceStudent.Actions[this.FaceStudent.Phase] == StudentActionType.Bully)
														{
															this.FaceStudent.PhotoPatience = 0;
															this.FaceStudent.KilledMood = true;
															this.FaceStudent.Ignoring = true;

															this.PenaltyTimer = 1;
															this.Penalize();
														}
														else if (this.FaceStudent.PhotoPatience > 0)
														{
															if (this.FaceStudent.StudentID > 1)
															{
																if (this.Yandere.Bloodiness > 0 && !this.Yandere.Paint || this.Yandere.Sanity < 33.33333)
																{
																	this.FaceStudent.Alarm += 200;
																}
																else
																{
																	this.FaceStudent.CameraReact();
																}
															}
															else
															{
																//Here, we should have some
																//code that makes Senpai slowly
																//get more alarmed.

																this.FaceStudent.Alarm += Time.deltaTime * (100.0f / this.FaceStudent.DistanceToPlayer) *
																	this.FaceStudent.Paranoia * this.FaceStudent.Perception * this.FaceStudent.DistanceToPlayer * 2;

																this.FaceStudent.YandereVisible = true;
															}
														}
														else
														{
															this.Penalize();
														}
													}
												}
											}
											else
											{
												this.FaceStudent.PhotoPatience = Mathf.MoveTowards(this.FaceStudent.PhotoPatience, 0, Time.deltaTime);

												if (this.FaceStudent.PhotoPatience > 0)
												{
													this.FaceStudent.CameraPoseTimer = 1.0f;

													if (this.MissionMode)
													{
														this.FaceStudent.PhotoPatience = 0;
													}
												}
											}
										}
									}
								}
							}
						}
						else if ((this.hit.collider.gameObject.name == "Panties") ||
							(this.hit.collider.gameObject.name == "Skirt"))
						{
							GameObject TempStudent2 = this.hit.collider.gameObject.transform.root.gameObject;

							if (Physics.Raycast(this.SmartphoneCamera.transform.position, dir, out this.hit, Mathf.Infinity, this.OnlyCharacters))
							{
								if (Vector3.Distance(this.Yandere.transform.position,
									TempStudent2.transform.position) < 5.0f)
								{
									if (this.hit.collider.gameObject == TempStudent2)
									{
										if (!this.Yandere.Lewd)
										{
											this.Yandere.NotificationManager.DisplayNotification(NotificationType.Lewd);
										}

										this.Yandere.Lewd = true;
									}
									else
									{
										this.Yandere.Lewd = false;
									}
								}
								else
								{
									this.Yandere.Lewd = false;
								}
							}
						}
						else
						{
							this.Yandere.Lewd = false;
						}
					}
					else
					{
						this.Yandere.Lewd = false;
					}
				}
			}
			else
			{
				this.Timer = 0.0f;
			}
		}

		if (this.TookPhoto)
		{
			this.ResumeGameplay();
		}

		if (!this.DisplayError)
		{
			// [af] Added "gameObject" for C# compatibility.
			if (this.PhotoIcons.activeInHierarchy && !this.Snapping &&
				!this.TextMessages.gameObject.activeInHierarchy)
			{
				if (Input.GetButtonDown(InputNames.Xbox_A))
				{
					if (!this.Yandere.RivalPhone)
					{
						bool BullyShot = !this.BullyX.activeInHierarchy;

						bool SenpaiShot = !this.SenpaiX.activeInHierarchy;

						// [af] Commented in JS code.
						//PromptBar.ClearButtons();
						//PromptBar.Label[1].text = "Exit";
						//PromptBar.UpdateButtons();

						this.PromptBar.transform.localPosition = new Vector3(
							this.PromptBar.transform.localPosition.x,
							-627.0f,
							this.PromptBar.transform.localPosition.z);

						this.PromptBar.ClearButtons();
						this.PromptBar.Show = false;

						this.PhotoIcons.SetActive(false);

						this.ID = 0;

						this.FreeSpace = false;

						while (this.ID < 26)
						{
							this.ID++;

							if (!PlayerGlobals.GetPhoto(this.ID))
							{
								this.FreeSpace = true;
								this.Slot = this.ID;
								this.ID = 26;
							}
						}

						if (this.FreeSpace)
						{
							ScreenCapture.CaptureScreenshot(Application.streamingAssetsPath +
							"/Photographs/" + "Photo_" + this.Slot.ToString() + ".png");

							this.TookPhoto = true;

							Debug.Log("Setting Photo " + this.Slot + " to ''true''.");
							PlayerGlobals.SetPhoto(this.Slot, true);

							if (BullyShot)
							{
								Debug.Log("Saving a bully photo!");

								int ColliderOwner = this.BullyPhotoCollider.transform.parent.gameObject.GetComponent<StudentScript>().StudentID;

								if (this.StudentManager.Students[ColliderOwner].Club != ClubType.Bully)
								{
									PlayerGlobals.SetBullyPhoto(this.Slot, ColliderOwner);
								}
								else
								{
									PlayerGlobals.SetBullyPhoto(this.Slot, this.StudentManager.Students[ColliderOwner].DistractionTarget.StudentID);
								}
							}

							if (SenpaiShot)
							{
								PlayerGlobals.SetSenpaiPhoto(this.Slot, true);
							}

							if (this.AirGuitarShot)
							{
								TaskGlobals.SetGuitarPhoto(this.Slot, true);
								this.TaskManager.UpdateTaskStatus();
							}

							if (this.KittenShot)
							{
								TaskGlobals.SetKittenPhoto(this.Slot, true);
								this.TaskManager.UpdateTaskStatus();
							}

							if (this.HorudaShot)
							{
								TaskGlobals.SetHorudaPhoto(this.Slot, true);
								this.TaskManager.UpdateTaskStatus();
							}

							if (this.OsanaShot)
							{
								//If we're waiting for Yandere-chan to take a photo of Osana kicking a vending machine...
								if (SchemeGlobals.GetSchemeStage(4) == 6)
								{
									SchemeGlobals.SetSchemeStage(4, 7);
									this.Yandere.PauseScreen.Schemes.UpdateInstructions();
								}
							}
						}
						else
						{
							this.DisplayError = true;
						}
					}
					else
					{
						if (!this.PantiesX.activeInHierarchy)
						{
							//If we're waiting for Yandere-chan to snap a panty shot with Osana's phone...
							if (SchemeGlobals.GetSchemeStage(1) == 5)
							{
								SchemeGlobals.SetSchemeStage(1, 6);
								this.Schemes.UpdateInstructions();
							}

							this.StudentManager.CommunalLocker.RivalPhone.LewdPhotos = true;
							//SchemeGlobals.SetSchemeStage(4, 3);
							//this.Schemes.UpdateInstructions();
							this.ResumeGameplay();
						}
					}
				}

				if (!this.Yandere.RivalPhone)
				{
					if (Input.GetButtonDown(InputNames.Xbox_X))
					{
						this.Panel.SetActive(true);
						this.MainMenu.SetActive(false);
						this.PauseScreen.Show = true;
						this.PauseScreen.Panel.enabled = true;

						this.PromptBar.ClearButtons();
						this.PromptBar.Label[1].text = "Exit";

						if (this.PantiesX.activeInHierarchy)
						{
							this.PromptBar.Label[3].text = "Interests";
						}
						else
						{
							this.PromptBar.Label[3].text = "";
						}

						this.PromptBar.UpdateButtons();

						// [af] Commented in JS code.
						//PromptBar.transform.localPosition.y = -627;
						//PromptBar.ClearButtons();
						//PromptBar.Show = false;

						if (!this.InfoX.activeInHierarchy)
						{
							this.PauseScreen.Sideways = true;

							StudentGlobals.SetStudentPhotographed(this.Student.StudentID, true);

							// [af] Converted while loop to for loop.
							for (this.ID = 0; this.ID < this.Student.Outlines.Length; this.ID++)
							{
								this.Student.Outlines[this.ID].enabled = true;
							}

							this.StudentInfo.UpdateInfo(this.Student.StudentID);

							// [af] Added "gameObject" for C# compatibility.
							this.StudentInfo.gameObject.SetActive(true);
						}
						else
						{
							// [af] Added "gameObject" for C# compatibility.
							if (!this.TextMessages.gameObject.activeInHierarchy)
							{
								this.PauseScreen.Sideways = false;

								// [af] Added "gameObject" for C# compatibility.
								this.TextMessages.gameObject.SetActive(true);

								this.SpawnMessage();
							}
						}
					}
				}

				if (Input.GetButtonDown(InputNames.Xbox_B))
				{
					this.ResumeGameplay();
				}
			}
			else if (this.PhotoIcons.activeInHierarchy)
			{
				if (Input.GetButtonDown(InputNames.Xbox_B))
				{
					this.ResumeGameplay();
				}
			}
		}
		else
		{
			float lerpSpeed = Time.unscaledDeltaTime * 10.0f;

			this.ErrorWindow.transform.localScale = Vector3.Lerp(
				this.ErrorWindow.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f), lerpSpeed);

			if (Input.GetButtonDown(InputNames.Xbox_A))
			{
				this.ResumeGameplay();
			}
		}
	}

	public void Snap()
	{
		this.ErrorWindow.transform.localScale = Vector3.zero;
		this.Yandere.HandCamera.gameObject.SetActive(false);

		this.Sprite.color = new Color(
			this.Sprite.color.r,
			this.Sprite.color.g,
			this.Sprite.color.b,
			1.0f);

		this.MyAudio.Play();

		this.Snapping = true;
		this.Close = true;
		this.Frame = 0;
	}

	void CheckPhoto()
	{
		this.InfoX.SetActive(true);
		this.BullyX.SetActive(true);
		this.SenpaiX.SetActive(true);
		this.PantiesX.SetActive(true);
		this.ViolenceX.SetActive(true);

		this.AirGuitarShot = false;
		this.HorudaShot = false;
		this.KittenShot = false;
		this.OsanaShot = false;
		this.Nemesis = false;
		this.NotFace = false;
		this.Skirt = false;

		Vector3 dir;

		if (!this.Yandere.Selfie)
		{
			dir = SmartphoneCamera.transform.TransformDirection(Vector3.forward);
		}
		else
		{
			dir = SelfieRayParent.TransformDirection(Vector3.forward);
		}

        this.StudentManager.UpdateSkirts(true);

		if (Physics.Raycast(this.SmartphoneCamera.transform.position, dir, out this.hit, Mathf.Infinity, this.OnlyPhotography))
		{
			Debug.Log("Took a picture of " + hit.collider.gameObject.name);

			Debug.Log("The root is " + hit.collider.gameObject.transform.root.name);

			if (this.hit.collider.gameObject.name == "Panties")
			{
				this.Student = this.hit.collider.gameObject.transform.root.gameObject.GetComponent<StudentScript>();

				/*
				string Name = "";

				if (this.Student != null)
				{
					Name = this.Student.Name;
				}
				else
				{
					Name = "Nemesis";
				}

				PhotoDescLabel.text = "Photo of: " + Name + "'s Panties";
				*/

				PhotoDescLabel.text = "Photo of: " + this.Student.Name + "'s Panties";
				
				this.PantiesX.SetActive(false);
			}
			else if (this.hit.collider.gameObject.name == "Face")
			{
				if (this.hit.collider.gameObject.tag == "Nemesis")
				{
					PhotoDescLabel.text = "Photo of: Nemesis";

					this.Nemesis = true;
					this.NemesisShots++;
				}
				else if (this.hit.collider.gameObject.tag == "Disguise")
				{
					PhotoDescLabel.text = "Photo of: ?????";
					this.Disguise = true;
				}
				else
				{
					this.Student = this.hit.collider.gameObject.transform.root.gameObject.GetComponent<StudentScript>();

					if (this.Student.StudentID == 1)
					{
						PhotoDescLabel.text = "Photo of: Senpai";
						this.SenpaiX.SetActive(false);
					}
					else
					{
						PhotoDescLabel.text = "Photo of: " + this.Student.Name;
						this.InfoX.SetActive(false);
					}
				}
			}
			else if (this.hit.collider.gameObject.name == "NotFace")
			{
				PhotoDescLabel.text = "Photo of: Blocked Face";
				this.NotFace = true;
			}
			else if (this.hit.collider.gameObject.name == "Skirt")
			{
				PhotoDescLabel.text = "Photo of: Skirt";
				this.Skirt = true;
			}

			if (this.hit.collider.transform.root.gameObject.name == "Student_51 (Miyuji Shan)")
			{
				if (this.StudentManager.Students[51].AirGuitar.isPlaying)
				{
					this.AirGuitarShot = true;

					PhotoDescription.SetActive(true);
					PhotoDescLabel.text = "Photo of: Miyuji's True Nature?";
				}
			}

			if (this.hit.collider.gameObject.name == "Kitten")
			{
				this.KittenShot = true;

				PhotoDescription.SetActive(true);
				PhotoDescLabel.text = "Photo of: Kitten";

				if (!ConversationGlobals.GetTopicDiscovered(15))
				{
					ConversationGlobals.SetTopicDiscovered(15, true);
					this.Yandere.NotificationManager.TopicName = "Cats";
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
				}
			}

			if (this.hit.collider.gameObject.tag == "Horuda")
			{
				this.HorudaShot = true;

				PhotoDescription.SetActive(true);
				PhotoDescLabel.text = "Photo of: Horuda's Hiding Spot";
			}

			if (this.hit.collider.gameObject.tag == "Bully")
			{
				PhotoDescLabel.text = "Photo of: Student Speaking With Bully";

				this.BullyPhotoCollider = hit.collider.gameObject;
				this.BullyX.SetActive(false);
			}

			if (this.hit.collider.gameObject.tag == "RivalEvidence")
			{
				this.OsanaShot = true;

				PhotoDescription.SetActive(true);
				PhotoDescLabel.text = "Photo of: Osana Vandalizing School Property";
			}
		}

		if (Physics.Raycast(this.SmartphoneCamera.transform.position, dir, out this.hit, Mathf.Infinity, this.OnlyRagdolls))
		{
			if (this.hit.collider.gameObject.layer == 11)
			{
				PhotoDescLabel.text = "Photo of: Corpse";
				this.ViolenceX.SetActive(false);
			}
		}

		if (Physics.Raycast(this.SmartphoneCamera.transform.position,
			this.SmartphoneCamera.transform.TransformDirection(Vector3.forward), out this.hit,
			Mathf.Infinity, this.OnlyBlood))
		{
			if (this.hit.collider.gameObject.layer == 14)
			{
				PhotoDescLabel.text = "Photo of: Blood";
				this.ViolenceX.SetActive(false);
			}
		}

        this.StudentManager.UpdateSkirts(false);
    }

	void SpawnMessage()
	{
		if (this.NewMessage != null)
		{
			Destroy(this.NewMessage);
		}

		this.NewMessage = Instantiate(this.Message);
		this.NewMessage.transform.parent = this.TextMessages;
		this.NewMessage.transform.localPosition = new Vector3(-225.0f, -275.0f, 0.0f);
		this.NewMessage.transform.localEulerAngles = Vector3.zero;
		this.NewMessage.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

		bool Kitten = false;

		if (this.hit.collider != null)
		{
			if (this.hit.collider.gameObject.name == "Kitten")
			{
				Kitten = true;
			}
		}

		string MessageText = string.Empty;
		int MessageHeight = 0;

		if (Kitten)
		{
			MessageText = "Why are you showing me this? I don't care.";
			MessageHeight = 2;
		}
		else if (!this.InfoX.activeInHierarchy)
		{
			MessageText = "I recognize this person. Here's some information about them.";
			MessageHeight = 3;
		}
		else if (!this.PantiesX.activeInHierarchy)
		{
			if (this.Student != null)
			{
				if (!PlayerGlobals.GetStudentPantyShot(this.Student.Name))
				{
					PlayerGlobals.SetStudentPantyShot(this.Student.Name, true);

					if (this.Student.Nemesis)
					{
						MessageText = "Hey, wait a minute...I recognize those panties! This person is extremely dangerous! Avoid her at all costs!";
					}
					else if (this.Student.Club == ClubType.Bully ||
							this.Student.Club == ClubType.Council ||
							this.Student.Club == ClubType.Nurse ||
							this.Student.StudentID == 20)
					{
						MessageText = "A high value target! " + this.Student.Name + "'s panties were in high demand. I owe you a big favor for this one.";
						Yandere.Inventory.PantyShots += 5;
					}
					else 
					{
						MessageText = "Excellent! Now I have a picture of " + this.Student.Name + "'s panties. I owe you a favor for this one.";
						Yandere.Inventory.PantyShots++;
					}

					MessageHeight = 5;
				}
				else
				{
					if (!this.Student.Nemesis)
					{
						MessageText = "I already have a picture of " + this.Student.Name +
						"'s panties. I don't need this shot.";
						MessageHeight = 4;
					}
					else
					{
						MessageText = "You are in danger. Avoid her.";
						MessageHeight = 2;
					}
				}
			}
			else
			{
				MessageText = "How peculiar. I don't recognize these panties.";
				MessageHeight = 2;
			}
		}
		else if (!this.ViolenceX.activeInHierarchy)
		{
			MessageText = "Good work, but don't send me this stuff. I have no use for it.";
			MessageHeight = 3;
		}
		else if (!this.SenpaiX.activeInHierarchy)
		{
			if (PlayerGlobals.SenpaiShots == 0)
			{
				MessageText = "I don't need any pictures of your Senpai.";
				MessageHeight = 2;
			}
			else if (PlayerGlobals.SenpaiShots == 1)
			{
				MessageText = "I know how you feel about this person, but I have no use for these pictures.";
				MessageHeight = 4;
			}
			else if (PlayerGlobals.SenpaiShots == 2)
			{
				MessageText = "Okay, I get it, you love your Senpai, and you love taking pictures of your Senpai. I still don't need these shots.";
				MessageHeight = 5;
			}
			else if (PlayerGlobals.SenpaiShots == 3)
			{
				MessageText = "You're spamming my inbox. Cut it out.";
				MessageHeight = 2;
			}
			else
			{
				MessageText = "...";
				MessageHeight = 1;
			}

			PlayerGlobals.SenpaiShots++;
		}
		else if (!this.BullyX.activeInHierarchy)
		{
			MessageText = "I have no interest in this.";
			MessageHeight = 2;
		}
		else if (this.NotFace)
		{
			MessageText = "Do you want me to identify this person? Please get me a clear shot of their face.";
			MessageHeight = 4;
		}
		else if (this.Skirt)
		{
			MessageText = "Is this supposed to be a panty shot? My clients are picky. The panties need to be in the EXACT center of the shot.";
			MessageHeight = 5;
		}
		else if (this.Nemesis)
		{
			if (this.NemesisShots == 1)
			{
				MessageText = "Strange. I have no profile for this student.";
				MessageHeight = 2;
			}
			else if (this.NemesisShots == 2)
			{
				MessageText = "...wait. I think I know who she is.";
				MessageHeight = 2;
			}
			else if (this.NemesisShots == 3)
			{
				MessageText = "You are in danger. Avoid her.";
				MessageHeight = 2;
			}
			else if (this.NemesisShots == 4)
			{
				MessageText = "Do not engage.";
				MessageHeight = 1;
			}
			else
			{
				MessageText = "I repeat: Do. Not. Engage.";
				MessageHeight = 2;
			}
		}
		else if (this.Disguise)
		{
			MessageText = "Something about that student seems...wrong.";
			MessageHeight = 2;
		}
		else
		{
			MessageText = "I don't get it. What are you trying to show me? Make sure the subject is in the EXACT center of the photo.";
			MessageHeight = 5;
		}

		this.NewMessage.GetComponent<UISprite>().height = 36 + (36 * MessageHeight);
		this.NewMessage.GetComponent<TextMessageScript>().Label.text = MessageText;
	}

	public void ResumeGameplay()
	{
		// [af] Commented in JS code.
		//StudentManager.GhostChan.GhostEye.transform.position = Vector3(0, 100, 0);

		this.ErrorWindow.transform.localScale = Vector3.zero;
		this.SmartphoneCamera.targetTexture = this.SmartphoneScreen;

		// [af] Added "gameObject" for C# compatibility.
		this.StudentManager.GhostChan.gameObject.SetActive(false);

		this.Yandere.HandCamera.gameObject.SetActive(true);
		this.NotificationManager.SetActive(true);
		this.PauseScreen.CorrectingTime = true;
		this.HeartbeatCamera.SetActive(true);

		// [af] Added "gameObject" for C# compatibility.
		this.TextMessages.gameObject.SetActive(false);
		this.StudentInfo.gameObject.SetActive(false);

		this.MainCamera.enabled = true;
		this.PhotoIcons.SetActive(false);
		this.PauseScreen.Show = false;
		this.SubPanel.SetActive(true);
		this.MainMenu.SetActive(true);
		this.Yandere.CanMove = true;

		// [af] Removed unused "TakingPicture" variable.

		this.DisplayError = false;
		this.Panel.SetActive(true);
		Time.timeScale = 1.0f;
		this.TakePhoto = false;
		this.TookPhoto = false;

		this.Yandere.PhonePromptBar.Panel.enabled = true;
		this.Yandere.PhonePromptBar.Show = true;

		this.PromptBar.ClearButtons();
		this.PromptBar.Show = false;

		if (this.NewMessage != null)
		{
			Destroy(this.NewMessage);
		}

		if (!this.Yandere.CameraEffects.OneCamera)
		{
			if (!OptionGlobals.Fog)
			{
				this.Yandere.MainCamera.clearFlags = CameraClearFlags.Skybox;
			}
			else
			{
				this.Yandere.MainCamera.clearFlags = CameraClearFlags.SolidColor;
			}

			this.Yandere.MainCamera.farClipPlane = OptionGlobals.DrawDistance;
		}

		this.Yandere.UpdateSelfieStatus();
	}

	public void Penalize()
	{
		this.PenaltyTimer += Time.deltaTime;

		if (this.PenaltyTimer >= 1.0f)
		{
			this.Subtitle.UpdateLabel(SubtitleType.PhotoAnnoyance, 0, 3.0f);

			if (this.MissionMode)
			{
				if (this.FaceStudent.TimesAnnoyed < 5)
				{
					this.FaceStudent.TimesAnnoyed++;
				}
				else
				{
					this.FaceStudent.RepDeduction = 0.0f;
					this.FaceStudent.RepLoss = 20.0f;

					this.FaceStudent.Reputation.PendingRep -= this.FaceStudent.RepLoss * this.FaceStudent.Paranoia;
					this.FaceStudent.PendingRep -= this.FaceStudent.RepLoss * this.FaceStudent.Paranoia;
				}
			}
			else
			{
				this.FaceStudent.RepDeduction = 0.0f;
				this.FaceStudent.RepLoss = 1.0f;

				this.FaceStudent.CalculateReputationPenalty();

				if (this.FaceStudent.RepDeduction >= 0.0f)
				{
					this.FaceStudent.RepLoss -= this.FaceStudent.RepDeduction;
				}

				this.FaceStudent.Reputation.PendingRep -= this.FaceStudent.RepLoss * this.FaceStudent.Paranoia;
				this.FaceStudent.PendingRep -= this.FaceStudent.RepLoss * this.FaceStudent.Paranoia;
			}

			this.PenaltyTimer = 0;
		}
	}
}
