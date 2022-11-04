using UnityEngine;

using XInputDotNetPure;

public class CounselorScript : MonoBehaviour
{
	public CutsceneManagerScript CutsceneManager;
	public StudentManagerScript StudentManager;
	public CounselorDoorScript CounselorDoor;
	public InputManagerScript InputManager;
	public PromptBarScript PromptBar;
	public EndOfDayScript EndOfDay;
	public SubtitleScript Subtitle;
	public SchemesScript Schemes;
	public StudentScript Student;
	public YandereScript Yandere;
	public Animation MyAnimation;
	public AudioSource MyAudio;
	public PromptScript Prompt;

	public AudioClip[] CounselorGreetingClips;
	public AudioClip[] CounselorLectureClips;
	public AudioClip[] CounselorReportClips;
	public AudioClip[] RivalClips;

	public AudioClip CounselorFarewellClip;
	public readonly string CounselorFarewellText = "Don't misbehave.";

	public AudioClip CounselorBusyClip;
	public readonly string CounselorBusyText = "I'm sorry, I've got my hands full for the rest of today. I won't be available until tomorrow.";

	public readonly string[] CounselorGreetingText =
	{
		"",
		"What can I help you with?",
		"Can I help you?"
	};

	//Panty Shots
	//Theft
	//Contraband
	//Vandalism
	//Cheating

	public readonly string[] CounselorLectureText =
	{
		"",
		//"Your \"after-school activities\" are completely unacceptable. You should not be conducting yourself in such a manner. This kind of behavior could cause a scandal! You could run the school's reputation into the ground!",
		"May I see your phone for a moment? ...what is THIS?! Would you care to explain why something like this is on your phone?",
		"May I take a look inside your bag? ...this doesn't belong to you, does it?! What are you doing with someone else's property?",
		"I need to take a look in your bag. ...cigarettes?! You have absolutely no excuse to be carrying something like this around!",
		"It has come to my attention that you've been vandalizing the school's property. What, exactly, do you have to say for yourself?",
		"Obviously, we need to have a long talk about the kind of behavior that will not tolerated at this school!",
		"That's it! I've given you enough second chances. You have repeatedly broken school rules and ignored every warning that I have given you. You have left me with no choice but to permanently expel you!"
	};

	public readonly string[] CounselorReportText =
	{
		"",
		//"This is...! Thank you for bringing this to my attention. This kind of conduct will definitely harm the school's reputation. I'll have to have a word with her later today.",
		"That's a very serious accusation. I hope you're not lying to me. Hopefully, it's just a misunderstanding. I'll investigate the matter.",
		"Is that true? I'd hate to think we have a thief here at school. Don't worry - I'll get to the bottom of this.",
		"That's a clear violation of school rules, not to mention completely illegal. If what you're saying is true, she will face serious consequences. I'll confront her about this.",
		"It's appalling to learn that there is a student at this school who thinks they can get away with this kind of misbehavior. I'll be sure to speak with her about this later today.",
		"That's a bold claim. Are you certain? I'll investigate the matter. If she is cheating, I'll catch her in the act."
	};

	public readonly string[] LectureIntro =
	{
		"",
		"During class, the guidance counselor enters the classroom and says that she needs to speak with Osana...",
		"During class, the guidance counselor enters the classroom and says that she needs to speak with Osana...",
		"During class, the guidance counselor enters the classroom and says that she needs to speak with Osana...",
		"During class, the guidance counselor enters the classroom and says that she needs to speak with Osana...",
		"During class, the guidance counselor enters the classroom and says that she needs to speak with Osana..."
	};

	public readonly string[] RivalText =
	{
		"",
		"What?! I've never taken and pictures like that! How did this get on my phone?!",
		"No! I'm not the one who did this! I would never steal from anyone!",
		"Huh? I don't smoke! I don't know why something like this was in my desk!",
		"W-wait, I can explain! It's not what you think!",
		"I'm telling the truth! I didn't steal the answer sheet! I don't know why it was in my desk!",
		"No...! P-please! Don't do this!"
	};

	public UILabel[] Labels;

	public Transform CounselorWindow;
	public Transform Highlight;
	public Transform Chibi;

	public SkinnedMeshRenderer Face;

	public UILabel CounselorSubtitle;
	public UISprite EndOfDayDarkness;
	public UILabel LectureSubtitle;
	public UISprite ExpelProgress;
	public UILabel LectureLabel;

	public bool ShowWindow = false;
	public bool Lecturing = false;
	public bool Busy = false;

	public int Selected = 1;

	public int LecturePhase = 1;
	public int LectureID = 5;

	public float ExpelTimer = 0.0f;
	public float ChinTimer = 0.0f;
	public float TalkTimer = 1.0f;
	public float Timer = 0.0f;

	void Start()
	{
		this.CounselorWindow.localScale = Vector3.zero;
		this.CounselorWindow.gameObject.SetActive(false);
		this.CounselorOptions.SetActive(false);
		this.CounselorBar.SetActive(false);
		this.Reticle.SetActive(false);

		this.ExpelProgress.color = new Color(
			this.ExpelProgress.color.r,
			this.ExpelProgress.color.g,
			this.ExpelProgress.color.b,
			0.0f);

		this.Chibi.localPosition = new Vector3(
				this.Chibi.localPosition.x,
				250.0f + (StudentGlobals.ExpelProgress * -90.0f),
				this.Chibi.localPosition.z);
	}

	void Update()
	{
		if (this.LookAtPlayer)
		{
			if (this.TalkTimer < 1)
			{
				this.TalkTimer = Mathf.MoveTowards(this.TalkTimer, 1, Time.deltaTime);

				if (this.TalkTimer == 1)
				{
					int RandomNumber = Random.Range(1, 3);

					this.CounselorSubtitle.text = this.CounselorGreetingText[RandomNumber];

					MyAudio.clip = this.CounselorGreetingClips[RandomNumber];
					MyAudio.Play();
				}
			}

			if (this.InputManager.TappedUp)
			{
				this.Selected--;

				if (this.Selected == 6)
				{
					this.Selected = 5;
				}

				this.UpdateHighlight();
			}

			if (this.InputManager.TappedDown)
			{
				this.Selected++;

				if (this.Selected == 6)
				{
					this.Selected = 7;
				}

				this.UpdateHighlight();
			}

			if (this.ShowWindow)
			{
				if (this.CounselorDoor.Darkness.color.a == 0)
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						if (this.Selected == 7)
						{
							if (!CounselorDoor.Exit)
							{
								this.CounselorSubtitle.text = this.CounselorFarewellText;
								MyAudio.clip = this.CounselorFarewellClip;
								MyAudio.Play();

								CounselorDoor.FadeOut = true;
								CounselorDoor.FadeIn = false;
								CounselorDoor.Exit = true;
							}
						}
						else
						{
							if (this.Labels[this.Selected].color.a == 1.0f)
							{
								//Panty Shots
								if (this.Selected == 1)
								{
									SchemeGlobals.SetSchemeStage(1, 9);
									this.Schemes.UpdateInstructions();
								}
								//Theft
								else if (this.Selected == 2)
								{
									SchemeGlobals.SetSchemeStage(2, 4);
									this.Schemes.UpdateInstructions();
								}
								//Contraband
								else if (this.Selected == 3)
								{
									SchemeGlobals.SetSchemeStage(3, 5);
									this.Schemes.UpdateInstructions();
								}
								//Vandalism.
								else if (this.Selected == 4)
								{
									SchemeGlobals.SetSchemeStage(4, 8);
									this.Schemes.UpdateInstructions();
								}
								//Cheating
								else if (this.Selected == 5)
								{
									SchemeGlobals.SetSchemeStage(5, 10);
									this.Schemes.UpdateInstructions();
								}

								this.CounselorSubtitle.text = this.CounselorReportText[this.Selected];
								MyAudio.clip = this.CounselorReportClips[this.Selected];
								MyAudio.Play();

								this.ShowWindow = false;
								this.Angry = true;

								this.CutsceneManager.Scheme = this.Selected;
								this.LectureID = this.Selected;

								this.PromptBar.ClearButtons();
								this.PromptBar.Show = false;

								// [af] Commented in JS code.
								//Prompt.Hide();
								//Prompt.enabled = false;

								this.Busy = true;
							}
						}
					}
				}
			}
			else
			{
				if (!this.Interrogating)
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						MyAudio.Stop();
					}

					if (!MyAudio.isPlaying)
					{
						this.Timer += Time.deltaTime;

						if (this.Timer > 0.50f)
						{
							this.CounselorDoor.FadeOut = true;
							this.CounselorDoor.Exit = true;
							this.LookAtPlayer = false;

							this.UpdateList();
						}
					}
				}
			}
		}
		else
		{
			if (!this.Interrogating)
			{
				/*
				this.ChinTimer += Time.deltaTime;

				if (this.ChinTimer > 10.0f)
				{
					MyAnimation.CrossFade("CounselorComputerChin");

					if (MyAnimation["CounselorComputerChin"].time >
						MyAnimation["CounselorComputerChin"].length)
					{
						MyAnimation.CrossFade("CounselorComputerLoop");
						this.ChinTimer = 0.0f;
					}
				}
				*/
			}
		}

		if (this.ShowWindow)
		{
			this.CounselorWindow.localScale = Vector3.Lerp(
				this.CounselorWindow.localScale,
				new Vector3(1.0f, 1.0f, 1.0f),
				Time.deltaTime * 10.0f);
		}
		else
		{
			if (this.CounselorWindow.localScale.x > 0.10f)
			{
				this.CounselorWindow.localScale = Vector3.Lerp(
					this.CounselorWindow.localScale,
					Vector3.zero,
					Time.deltaTime * 10.0f);
			}
			else
			{
				if (this.CounselorWindow.gameObject.activeInHierarchy)
				{
					this.CounselorWindow.localScale = Vector3.zero;
					this.CounselorWindow.gameObject.SetActive(false);
				}
			}
		}

		if (this.Lecturing)
		{
			Debug.Log ("The guidance counselor is lecturing!");

			this.Chibi.localPosition = new Vector3(
				this.Chibi.localPosition.x,
				Mathf.Lerp(this.Chibi.localPosition.y, 250.0f + (StudentGlobals.ExpelProgress * -90.0f), Time.deltaTime * 3.0f),
				this.Chibi.localPosition.z);

			if (this.LecturePhase == 1)
			{
				Debug.Log ("Lecture Phase 1.");

				this.LectureLabel.text = this.LectureIntro[this.LectureID];
				this.EndOfDayDarkness.color = new Color(
					this.EndOfDayDarkness.color.r,
					this.EndOfDayDarkness.color.g,
					this.EndOfDayDarkness.color.b,
					Mathf.MoveTowards(this.EndOfDayDarkness.color.a, 0.0f, Time.deltaTime));

				if (this.EndOfDayDarkness.color.a == 0.0f)
				{
					this.PromptBar.ClearButtons();
					this.PromptBar.Label[0].text = "Continue";
					this.PromptBar.UpdateButtons();
					this.PromptBar.Show = true;

					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.LecturePhase++;

						this.PromptBar.ClearButtons();
						this.PromptBar.Show = false;
					}
				}
			}
			else if (this.LecturePhase == 2)
			{
				Debug.Log ("Lecture Phase 2.");

				this.LectureLabel.color = new Color(
					this.LectureLabel.color.r,
					this.LectureLabel.color.g,
					this.LectureLabel.color.b,
					Mathf.MoveTowards(this.LectureLabel.color.a, 0.0f, Time.deltaTime));

				if (this.LectureLabel.color.a == 0.0f)
				{
					this.EndOfDay.TextWindow.SetActive(false);

					this.EndOfDay.EODCamera.GetComponent<AudioListener>().enabled = true;

					this.LectureSubtitle.text = this.CounselorLectureText[this.LectureID];
					MyAudio.clip = this.CounselorLectureClips[this.LectureID];
					MyAudio.Play();

					this.LecturePhase++;
				}
			}
			else if (this.LecturePhase == 3)
			{
				Debug.Log ("Lecture Phase 3.");

				if (!MyAudio.isPlaying || Input.GetButtonDown(InputNames.Xbox_A))
				{
					this.LectureSubtitle.text = this.RivalText[this.LectureID];
					MyAudio.clip = this.RivalClips[this.LectureID];
					MyAudio.Play();

					this.LecturePhase++;
				}
			}
			else if (this.LecturePhase == 4)
			{
				Debug.Log ("Lecture Phase 4.");

				if (!MyAudio.isPlaying || Input.GetButtonDown(InputNames.Xbox_A))
				{
					this.LectureSubtitle.text = string.Empty;

					if (StudentGlobals.ExpelProgress < 5)
					{
						this.LecturePhase++;
					}
					else
					{
						this.LecturePhase = 7;
						this.ExpelTimer = 0.0f;
					}
				}
			}
			else if (this.LecturePhase == 5)
			{
				Debug.Log ("Lecture Phase 5.");

				this.ExpelProgress.color = new Color(
					this.ExpelProgress.color.r,
					this.ExpelProgress.color.g,
					this.ExpelProgress.color.b,
					Mathf.MoveTowards(this.ExpelProgress.color.a, 1.0f, Time.deltaTime));

				this.ExpelTimer += Time.deltaTime;

				if (this.ExpelTimer > 2.0f)
				{
					StudentGlobals.ExpelProgress++;
					this.LecturePhase++;

					Debug.Log ("StudentGlobals.ExpelProgress is now: " + StudentGlobals.ExpelProgress);
				}
			}
			else if (this.LecturePhase == 6)
			{
				Debug.Log ("Lecture Phase 6.");

				this.ExpelTimer += Time.deltaTime;

				if (this.ExpelTimer > 4.0f)
				{
					this.LecturePhase += 2;
				}
			}
			else if (this.LecturePhase == 7)
			{
				Debug.Log ("Lecture Phase 7.");

				this.ExpelTimer += Time.deltaTime;

				if (this.ExpelTimer > 1)
				{
					this.RIVAL.gameObject.SetActive(true);
				}

				if (this.ExpelTimer > 3)
				{
					this.EXPELLED.gameObject.SetActive(true);
				}

				if (this.ExpelTimer > 5)
				{
					this.RIVAL.color = new Color(this.RIVAL.color.r, this.RIVAL.color.g, this.RIVAL.color.b, this.RIVAL.color.a - Time.deltaTime);
					this.EXPELLED.color = new Color(this.EXPELLED.color.r, this.EXPELLED.color.g, this.EXPELLED.color.b, this.EXPELLED.color.a - Time.deltaTime);
				}

				if (this.ExpelTimer > 7)
				{
					this.RIVAL.gameObject.SetActive(false);
					this.EXPELLED.gameObject.SetActive(false);

					this.LecturePhase++;
				}
			}
			else if (this.LecturePhase == 8)
			{
				Debug.Log ("Lecture Phase 8.");

				this.ExpelProgress.color = new Color(
					this.ExpelProgress.color.r,
					this.ExpelProgress.color.g,
					this.ExpelProgress.color.b,
					Mathf.MoveTowards(this.ExpelProgress.color.a, 0.0f, Time.deltaTime));

				this.ExpelTimer += Time.deltaTime;

				if (this.ExpelTimer > 6.0f)
				{
					if ((StudentGlobals.ExpelProgress == 5) && !StudentGlobals.GetStudentExpelled(11) &&
						this.EndOfDay.RivalEliminationMethod != RivalEliminationType.Expelled &&
						this.StudentManager.Police.TranqCase.VictimID != 11 || this.StudentManager.Students[11].SentHome)
					{
						Debug.Log("Osana has now been expelled.");

						this.EndOfDay.RivalEliminationMethod = RivalEliminationType.Expelled;
						this.StudentManager.RivalEliminated = true;

						this.EndOfDayDarkness.color = new Color(
							this.EndOfDayDarkness.color.r,
							this.EndOfDayDarkness.color.g,
							this.EndOfDayDarkness.color.b,
							0.0f);

						this.LectureLabel.color = new Color(
							this.LectureLabel.color.r,
							this.LectureLabel.color.g,
							this.LectureLabel.color.b,
							0.0f);

						this.LecturePhase = 2;
						this.ExpelTimer = 0.0f;
						this.LectureID = 6;
					}
					else
					{
						Debug.Log("We are leaving the lecture and returning to gameplay.");

						// [af] Added "gameObject" for C# compatibility.
						this.EndOfDay.gameObject.SetActive(false);

						this.EndOfDay.Phase = 1;

						this.CutsceneManager.Phase++;
						this.Lecturing = false;

						this.Yandere.PauseScreen.Schemes.SchemeManager.enabled = false;
						this.Yandere.MainCamera.gameObject.SetActive(true);
						this.Yandere.gameObject.SetActive(true);

						this.StudentManager.ComeBack();

						this.StudentManager.Students[this.StudentManager.RivalID].IdleAnim = this.StudentManager.Students[this.StudentManager.RivalID].BulliedIdleAnim;
						this.StudentManager.Students[this.StudentManager.RivalID].WalkAnim = this.StudentManager.Students[this.StudentManager.RivalID].BulliedWalkAnim;

						if (this.LectureID == 6)
						{
							if (this.StudentManager.Students[10] != null)
							{
								StudentScript Raibaru = this.StudentManager.Students[10];

								Debug.Log("Osana is gone, so Raibaru's routine has to change.");

								ScheduleBlock newBlock4 = Raibaru.ScheduleBlocks[4];
								newBlock4.destination = "Mourn";
								newBlock4.action = "Mourn";

								ScheduleBlock newBlock5 = Raibaru.ScheduleBlocks[5];
								newBlock5.destination = "Seat";
								newBlock5.action = "Sit";

								ScheduleBlock newBlock6 = Raibaru.ScheduleBlocks[6];
								newBlock6.destination = "Locker";
								newBlock6.action = "Shoes";

								ScheduleBlock newBlock7 = Raibaru.ScheduleBlocks[7];
								newBlock7.destination = "Exit";
								newBlock7.action = "Exit";

								ScheduleBlock newBlock8 = Raibaru.ScheduleBlocks[8];
								newBlock8.destination = "Exit";
								newBlock8.action = "Exit";

								ScheduleBlock newBlock9 = Raibaru.ScheduleBlocks[9];
								newBlock9.destination = "Exit";
								newBlock9.action = "Exit";

								Raibaru.TargetDistance = .5f;

								Raibaru.IdleAnim = Raibaru.BulliedIdleAnim;
								Raibaru.WalkAnim = Raibaru.BulliedWalkAnim;
								Raibaru.OriginalIdleAnim = Raibaru.IdleAnim;
								Raibaru.Pathfinding.speed = 1;

								Raibaru.GetDestinations();
							}
						}

						this.LectureID = 0;
					}
				}
			}
		}

		if (!MyAudio.isPlaying)
		{
			this.CounselorSubtitle.text = string.Empty;
		}

		if (this.Interrogating)
		{
			this.UpdateInterrogation();
		}
	}

	public void Talk()
	{
		MyAnimation.CrossFade("CounselorComputerAttention", 1.0f);
		this.ChinTimer = 0.0f;

		this.Yandere.TargetStudent = this.Student;

		this.TalkTimer = 0;

		this.StudentManager.DisablePrompts();

		this.CounselorWindow.gameObject.SetActive(true);
		this.LookAtPlayer = true;
		this.ShowWindow = true;

		this.Yandere.ShoulderCamera.OverShoulder = true;
		this.Yandere.WeaponMenu.KeyboardShow = false;
		this.Yandere.Obscurance.enabled = false;
		this.Yandere.WeaponMenu.Show = false;
		this.Yandere.YandereVision = false;
		this.Yandere.CanMove = false;
		this.Yandere.Talking = true;

		this.PromptBar.ClearButtons();
		this.PromptBar.Label[0].text = "Accept";
		this.PromptBar.Label[4].text = "Choose";
		this.PromptBar.UpdateButtons();
		this.PromptBar.Show = true;

		this.UpdateList();
	}

	void UpdateList()
	{
		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < this.Labels.Length; ID++)
		{
			UILabel label = this.Labels[ID];
			label.color = new Color(
				label.color.r,
				label.color.g,
				label.color.b,
				0.50f);
		}

		if (this.StudentManager.Students[11] != null)
		{
			// Reporting Panty Shots.
			if (SchemeGlobals.GetSchemeStage(1) == 8)
			{
				UILabel label1 = this.Labels[1];
				label1.color = new Color(
					label1.color.r,
					label1.color.g,
					label1.color.b,
					1.0f);
			}

			// Reporting Theft.
			if (SchemeGlobals.GetSchemeStage(2) == 3)
			{
				UILabel label2 = this.Labels[2];
				label2.color = new Color(
					label2.color.r,
					label2.color.g,
					label2.color.b,
					1.0f);
			}

			// Reporting Contraband.
			if (SchemeGlobals.GetSchemeStage(3) == 4)
			{
				UILabel label3 = this.Labels[3];
				label3.color = new Color(
					label3.color.r,
					label3.color.g,
					label3.color.b,
					1.0f);
			}

			// Reporting Vandalism
			if (SchemeGlobals.GetSchemeStage(4) == 7)
			{
				UILabel label4 = this.Labels[4];
				label4.color = new Color(
					label4.color.r,
					label4.color.g,
					label4.color.b,
					1.0f);
			}

			// Reporting Cheating.
			if (SchemeGlobals.GetSchemeStage(5) == 9)
			{
				UILabel label5 = this.Labels[5];
				label5.color = new Color(
					label5.color.r,
					label5.color.g,
					label5.color.b,
					1.0f);
			}
		}
	}

	void UpdateHighlight()
	{
		if (this.Selected < 1)
		{
			this.Selected = 7;
		}
		else if (this.Selected > 7)
		{
			this.Selected = 1;
		}

		this.Highlight.transform.localPosition = new Vector3(
			this.Highlight.transform.localPosition.x,
			200.0f - (50.0f * this.Selected),
			this.Highlight.transform.localPosition.z);
	}

	public Vector3 LookAtTarget;
	public bool LookAtPlayer = false;
	public Transform Default;
	public Transform Head;

	public bool Angry;
	public bool Stern;
	public bool Sad;

	public float MouthTarget;
	public float MouthTimer;
	public float TimerLimit;
	public float MouthOpen;
	public float TalkSpeed;

	public float BS_SadMouth;
	public float BS_MadBrow;
	public float BS_SadBrow;
	public float BS_AngryEyes;

	void LateUpdate()
	{
		if (Vector3.Distance(this.transform.position, Yandere.transform.position) < 5)
		{
			if (this.Angry)
			{
				this.BS_SadMouth = Mathf.Lerp(this.BS_SadMouth, 100.0f, Time.deltaTime * 10);
				this.BS_MadBrow = Mathf.Lerp(this.BS_MadBrow, 100.0f, Time.deltaTime * 10);
				this.BS_SadBrow = Mathf.Lerp(this.BS_SadBrow, 0.0f, Time.deltaTime * 10);
				this.BS_AngryEyes = Mathf.Lerp(this.BS_AngryEyes, 100.0f, Time.deltaTime * 10);
			}
			else if (this.Stern)
			{
				this.BS_SadMouth = Mathf.Lerp(this.BS_SadMouth, 0.0f, Time.deltaTime * 10);
				this.BS_MadBrow = Mathf.Lerp(this.BS_MadBrow, 100.0f, Time.deltaTime * 10);
				this.BS_SadBrow = Mathf.Lerp(this.BS_SadBrow, 0.0f, Time.deltaTime * 10);
				this.BS_AngryEyes = Mathf.Lerp(this.BS_AngryEyes, 0.0f, Time.deltaTime * 10);
			}
			else if (this.Sad)
			{
				this.BS_SadMouth = Mathf.Lerp(this.BS_SadMouth, 100.0f, Time.deltaTime * 10);
				this.BS_MadBrow = Mathf.Lerp(this.BS_MadBrow, 0.0f, Time.deltaTime * 10);
				this.BS_SadBrow = Mathf.Lerp(this.BS_SadBrow, 100.0f, Time.deltaTime * 10);
				this.BS_AngryEyes = Mathf.Lerp(this.BS_AngryEyes, 0.0f, Time.deltaTime * 10);
			}
			else
			{
				this.BS_SadMouth = Mathf.Lerp(this.BS_SadMouth, 0.0f, Time.deltaTime * 10);
				this.BS_MadBrow = Mathf.Lerp(this.BS_MadBrow, 0.0f, Time.deltaTime * 10);
				this.BS_SadBrow = Mathf.Lerp(this.BS_SadBrow, 0.0f, Time.deltaTime * 10);
				this.BS_AngryEyes = Mathf.Lerp(this.BS_AngryEyes, 0.0f, Time.deltaTime * 10);
			}

			this.Face.SetBlendShapeWeight(1, this.BS_SadMouth);
			this.Face.SetBlendShapeWeight(5, this.BS_MadBrow);
			this.Face.SetBlendShapeWeight(6, this.BS_SadBrow);
			this.Face.SetBlendShapeWeight(9, this.BS_AngryEyes);

			if (MyAudio.isPlaying)
			{
				if (InterrogationPhase != 6)
				{
					this.MouthTimer += Time.deltaTime;

					if (this.MouthTimer > this.TimerLimit)
					{
						this.MouthTarget = Random.Range(0.0f, 100.0f);
						this.MouthTimer = 0.0f;
					}

					this.MouthOpen = Mathf.Lerp(this.MouthOpen, this.MouthTarget, Time.deltaTime * this.TalkSpeed);
				}
				else
				{
					this.MouthOpen = Mathf.Lerp(this.MouthOpen, 0, Time.deltaTime * this.TalkSpeed);
				}
			}
			else
			{
				this.MouthOpen = Mathf.Lerp(this.MouthOpen, 0, Time.deltaTime * this.TalkSpeed);
			}

			this.Face.SetBlendShapeWeight(2, this.MouthOpen);

			// [af] Replaced if/else statement with assignment and ternary expression.
			this.LookAtTarget = Vector3.Lerp(
				this.LookAtTarget,
				this.LookAtPlayer ? this.Yandere.Head.position : this.Default.position,
				Time.deltaTime * 2.0f);

			this.Head.LookAt(this.LookAtTarget);
		}
	}

	public void Quit()
	{
		Debug.Log ("CounselorScript has called the Quit() function.");

		this.Yandere.Senpai = this.StudentManager.Students[1].transform;

		this.Yandere.DetectionPanel.alpha = 1.0f;
		this.Yandere.RPGCamera.mouseSpeed = 8.0f;
		this.Yandere.HUD.alpha = 1.0f;

		this.Yandere.WeaponTimer = 0;
		this.Yandere.TheftTimer = 0;

		this.Yandere.HeartRate.gameObject.SetActive(true);
		this.Yandere.CannotRecover = false;
		this.Yandere.Noticed = false;
		this.Yandere.Talking = true;

		/////

		this.Yandere.ShoulderCamera.GoingToCounselor = false;
		this.Yandere.ShoulderCamera.HUD.SetActive(true);
		this.Yandere.ShoulderCamera.Noticed = false;
		this.Yandere.ShoulderCamera.enabled = true;
		this.Yandere.TargetStudent = this.Student;

		/////

		if (!this.Yandere.Jukebox.FullSanity.isPlaying)
		{
			this.Yandere.Jukebox.FullSanity.volume = 0;
			this.Yandere.Jukebox.HalfSanity.volume = 0;
			this.Yandere.Jukebox.NoSanity.volume = 0;

			this.Yandere.Jukebox.FullSanity.Play();
			this.Yandere.Jukebox.HalfSanity.Play();
			this.Yandere.Jukebox.NoSanity.Play();
		}

		//MyAnimation.CrossFade("CounselorComputerLoop", 1);

		this.Yandere.transform.position = new Vector3(-21.5f, 0, 8);
		this.Yandere.transform.eulerAngles = new Vector3(0, 90, 0);

		this.Yandere.ShoulderCamera.OverShoulder = false;
		this.CounselorBar.SetActive(false);
		this.StudentManager.EnablePrompts();
		this.Laptop.SetActive(true);
		this.LookAtPlayer = false;
		this.ShowWindow = false;
		this.TalkTimer = 1;
		this.Patience = 0;

		this.Stern = false;
		this.Angry = false;
		this.Sad = false;

		this.PromptBar.ClearButtons();
		this.PromptBar.Show = false;

		this.StudentManager.ComeBack();
		this.StudentManager.GracePeriod(10);

		StudentManager.Reputation.UpdateRep();

		Physics.SyncTransforms();
	}

	public DetectClickScript[] CounselorOption;

	public InputDeviceScript InputDevice;
	public StudentWitnessType Crime;
	public UITexture GenkaChibi;
	public CameraShake Shake;

	public Texture HappyChibi;
	public Texture AnnoyedChibi;
	public Texture MadChibi;

	public GameObject CounselorOptions;
	public GameObject CounselorBar;
	public GameObject Reticle;
	public GameObject Laptop;

	public Transform CameraTarget;

	public int InterrogationPhase;
	public int Patience;
	public int CrimeID;
	public int Answer;

	public bool MustExpelDelinquents;
	public bool ExpelledDelinquents;
	public bool SilentTreatment;
	public bool Interrogating;
	public bool Expelled;
	public bool Slammed;

	public AudioSource Rumble;

	public AudioClip Countdown;
	public AudioClip Choice;
	public AudioClip Slam;

	// Counselor's Lines

	public AudioClip[] GreetingClips;
	public string[] Greetings;

	public AudioClip[] BloodLectureClips;
	public string[] BloodLectures;

	public AudioClip[] InsanityLectureClips;
	public string[] InsanityLectures;

	public AudioClip[] LewdLectureClips;
	public string[] LewdLectures;

	public AudioClip[] TheftLectureClips;
	public string[] TheftLectures;

	public AudioClip[] TrespassLectureClips;
	public string[] TrespassLectures;

	public AudioClip[] WeaponLectureClips;
	public string[] WeaponLectures;

	public AudioClip[] SilentClips;
	public string[] Silents;

	public AudioClip[] SuspensionClips;
	public string[] Suspensions;

	public AudioClip[] AcceptExcuseClips;
	public string[] AcceptExcuses;

	public AudioClip[] RejectExcuseClips;
	public string[] RejectExcuses;

	public AudioClip[] RejectLieClips;
	public string[] RejectLies;

	public AudioClip[] AcceptBlameClips;
	public string[] AcceptBlames;

	public AudioClip[] RejectApologyClips;
	public string[] RejectApologies;

	public AudioClip[] RejectBlameClips;
	public string[] RejectBlames;

	public AudioClip[] RejectFlirtClips;
	public string[] RejectFlirts;

	public AudioClip[] BadClosingClips;
	public string[] BadClosings;

	public AudioClip[] BlameClosingClips;
	public string[] BlameClosings;

	public AudioClip[] FreeToLeaveClips;
	public string[] FreeToLeaves;

	public AudioClip AcceptApologyClip;
	public string AcceptApology;

	public AudioClip RejectThreatClip;
	public string RejectThreat;

	public AudioClip ExpelDelinquentsClip;
	public string ExpelDelinquents;

	public AudioClip DelinquentsDeadClip;
	public string DelinquentsDead;

	public AudioClip DelinquentsExpelledClip;
	public string DelinquentsExpelled;

	public AudioClip DelinquentsGoneClip;
	public string DelinquentsGone;

	// Yandere-chan's lines

	public AudioClip[] ExcuseClips;
	public string[] Excuses;

	public AudioClip[] LieClips;
	public string[] Lies;

	public AudioClip[] DelinquentClips;
	public string[] Delinquents;

	public AudioClip ApologyClip;
	public string Apology;

	public AudioClip FlirtClip;
	public string Flirt;

	public AudioClip ThreatenClip;
	public string Threaten;

	public AudioClip Silence;

	public float VibrationTimer = 0.0f;
	public bool VibrationCheck = false;

	public UILabel RIVAL;
	public UILabel EXPELLED;

	void UpdateInterrogation()
	{
		if (this.VibrationCheck)
		{
			this.VibrationTimer = Mathf.MoveTowards(this.VibrationTimer, 0, Time.deltaTime);

			if (this.VibrationTimer == 0)
			{
				GamePad.SetVibration(0, 0, 0);
				this.VibrationCheck = false;
			}
		}

		Timer += Time.deltaTime;

		if (Input.GetButtonDown(InputNames.Xbox_A))
		{
			if (InterrogationPhase != 4)
			{
				Timer += 20;
			}
		}

		//Teleport the camera.
		if (InterrogationPhase == 0)
		{
			if (Timer > 1 || Input.GetButtonDown(InputNames.Xbox_A))
			{
				Debug.Log("Previous Punishments: " + CounselorGlobals.CounselorPunishments);

				Patience -= CounselorGlobals.CounselorPunishments;

				if (Patience < -6)
				{
					Patience = -6;
				}

				GenkaChibi.transform.localPosition = new Vector3(
					0,
					0 + (90 * Patience),
					0);

				Yandere.MainCamera.transform.eulerAngles = CameraTarget.eulerAngles;
				Yandere.MainCamera.transform.position = CameraTarget.position;
				Yandere.MainCamera.transform.Translate(Vector3.forward * -1);

				if (CounselorGlobals.CounselorVisits < 3)
				{
					CounselorGlobals.CounselorVisits++;
				}

				if (CounselorGlobals.CounselorTape == 0)
				{
					CounselorOption[4].Label.color = new Color(0, 0, 0, .5f);
				}
				else
				{
					CounselorOption[4].Label.color = new Color(0, 0, 0, 1);
					CounselorOption[4].Label.text = "Blame Delinquents";
				}

				if (Yandere.Subtitle.CurrentClip != null)
				{
					Destroy(Yandere.Subtitle.CurrentClip);
				}

				GenkaChibi.mainTexture = AnnoyedChibi;
				CounselorBar.SetActive(true);
				Subtitle.Label.text = "";
				InterrogationPhase++;
				Time.timeScale = 1;
				Timer = 0;
			}
		}
		//Greet Yandere-chan.
		else if (InterrogationPhase == 1)
		{
			Yandere.Police.Darkness.color -= new Color(0, 0, 0, Time.deltaTime);

			Yandere.MainCamera.transform.position = Vector3.Lerp(
				Yandere.MainCamera.transform.position,
				CameraTarget.position,
				Timer * Time.deltaTime * .5f);

			if (Timer > 5 || Input.GetButtonDown(InputNames.Xbox_A))
			{
				Yandere.MainCamera.transform.position = CameraTarget.position;

				MyAudio.clip = GreetingClips[CounselorGlobals.CounselorVisits];
				CounselorSubtitle.text = Greetings[CounselorGlobals.CounselorVisits];
				Yandere.Police.Darkness.color = new Color(0, 0, 0, 0);
				InterrogationPhase++;
				MyAudio.Play();
				Timer = 0;
			}
		}
		//Ask Yandere-chan to explain herself.
		else if (InterrogationPhase == 2)
		{
			if (Input.GetButtonDown(InputNames.Xbox_A))
			{
				MyAudio.Stop();
			}

			if (Timer > MyAudio.clip.length + .5f)
			{
				if (Crime == StudentWitnessType.Blood || Crime == StudentWitnessType.BloodAndInsanity)
				{
					MyAudio.clip = BloodLectureClips[CounselorGlobals.BloodVisits];
					CounselorSubtitle.text = BloodLectures[CounselorGlobals.BloodVisits];

					if (CounselorGlobals.BloodVisits < 2)
					{
						CounselorGlobals.BloodVisits++;
					}

					CrimeID = 1;
				}
				else if (Crime == StudentWitnessType.Insanity || Crime == StudentWitnessType.CleaningItem ||
						 Crime == StudentWitnessType.HoldingBloodyClothing || Crime == StudentWitnessType.Poisoning)
				{
					MyAudio.clip = InsanityLectureClips[CounselorGlobals.InsanityVisits];
					CounselorSubtitle.text = InsanityLectures[CounselorGlobals.InsanityVisits];

					if (CounselorGlobals.InsanityVisits < 2)
					{
						CounselorGlobals.InsanityVisits++;
					}

					CrimeID = 2;
				}
				else if (Crime == StudentWitnessType.Lewd)
				{
					MyAudio.clip = LewdLectureClips[CounselorGlobals.LewdVisits];
					CounselorSubtitle.text = LewdLectures[CounselorGlobals.LewdVisits];

					if (CounselorGlobals.LewdVisits < 2)
					{
						CounselorGlobals.LewdVisits++;
					}

					CrimeID = 3;
				}
				else if (Crime == StudentWitnessType.Theft || Crime == StudentWitnessType.Pickpocketing)
				{
					MyAudio.clip = TheftLectureClips[CounselorGlobals.TheftVisits];
					CounselorSubtitle.text = TheftLectures[CounselorGlobals.TheftVisits];

					if (CounselorGlobals.TheftVisits < 2)
					{
						CounselorGlobals.TheftVisits++;
					}

					CrimeID = 4;
				}
				else if (Crime == StudentWitnessType.Trespassing)
				{
					MyAudio.clip = TrespassLectureClips[CounselorGlobals.TrespassVisits];
					CounselorSubtitle.text = TrespassLectures[CounselorGlobals.TrespassVisits];

					if (CounselorGlobals.TrespassVisits < 2)
					{
						CounselorGlobals.TrespassVisits++;
					}

					CrimeID = 5;
				}
				else if (Crime == StudentWitnessType.Weapon || Crime == StudentWitnessType.WeaponAndBlood ||
				         Crime == StudentWitnessType.WeaponAndInsanity || Crime == StudentWitnessType.WeaponAndBloodAndInsanity)
				{
					MyAudio.clip = WeaponLectureClips[CounselorGlobals.WeaponVisits];
					CounselorSubtitle.text = WeaponLectures[CounselorGlobals.WeaponVisits];

					if (CounselorGlobals.WeaponVisits < 2)
					{
						CounselorGlobals.WeaponVisits++;
					}

					CrimeID = 6;
				}

				InterrogationPhase++;
				MyAudio.Play();
				Timer = 0;
			}
		}
		//Teleport camera to Yandere-chan's face.
		else if (InterrogationPhase == 3)
		{
			if (Input.GetButtonDown(InputNames.Xbox_A))
			{
				MyAudio.Stop();
			}

			if (Timer > MyAudio.clip.length + .5f)
			{
				int ID = 1;

				//Set up all the counselor options
				while (ID < 7)
				{
					CounselorOption[ID].transform.localPosition = CounselorOption[ID].OriginalPosition;
					CounselorOption[ID].Sprite.color = CounselorOption[ID].OriginalColor;
					CounselorOption[ID].transform.localScale = new Vector3(.9f, .9f, 1);
					CounselorOption[ID].gameObject.SetActive(true);
					CounselorOption[ID].Clicked = false;
					ID++;
				}

				Yandere.CharacterAnimation["f02_countdown_00"].speed = 1;
				Yandere.CharacterAnimation.Play("f02_countdown_00");

				Yandere.transform.position = new Vector3(-27.818f, 0, 12);
				Yandere.transform.eulerAngles = new Vector3(0, -90, 0);

				Yandere.MainCamera.transform.position = new Vector3(-28, 1.1f, 12);
				Yandere.MainCamera.transform.eulerAngles = new Vector3(0, 90, 0);

				Reticle.transform.localPosition = new Vector3(0, 0, 0);

				CounselorOptions.SetActive(true);
				CounselorBar.SetActive(false);
				CounselorSubtitle.text = "";

				MyAudio.clip = Countdown;
				MyAudio.Play();

				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;

				InterrogationPhase++;
				Timer = 0;
			}
		}
		//Zoom in on Yandere-chan's face, spin options around.
		else if (InterrogationPhase == 4)
		{
			Yandere.MainCamera.transform.Translate(Vector3.forward * Time.deltaTime * .01f);

			CounselorOptions.transform.localEulerAngles += new Vector3(0, 0, Time.deltaTime * -36);

			if (InputDevice.Type == InputDeviceType.Gamepad)
			{
				Reticle.SetActive(true);
				Cursor.visible = false;

				Reticle.transform.localPosition += new Vector3(
					Input.GetAxis("Horizontal") * 20,
					Input.GetAxis("Vertical") * 20,
					0);

				if (Reticle.transform.localPosition.x > 975){Reticle.transform.localPosition = new Vector3(975, Reticle.transform.localPosition.y, Reticle.transform.localPosition.z);}
				if (Reticle.transform.localPosition.x < -975){Reticle.transform.localPosition = new Vector3(-975, Reticle.transform.localPosition.y, Reticle.transform.localPosition.z);}
				if (Reticle.transform.localPosition.y > 525){Reticle.transform.localPosition = new Vector3(Reticle.transform.localPosition.x, 525, Reticle.transform.localPosition.z);}
				if (Reticle.transform.localPosition.y < -525){Reticle.transform.localPosition = new Vector3(Reticle.transform.localPosition.x, -525, Reticle.transform.localPosition.z);}
			}
			else
			{
				Reticle.SetActive(false);
				Cursor.visible = true;
			}

			int ID = 1;

			while (ID < 7)
			{
				CounselorOption[ID].transform.eulerAngles = new Vector3(
					CounselorOption[ID].transform.eulerAngles.x,
					CounselorOption[ID].transform.eulerAngles.y,
					0);

				if (CounselorOption[ID].Clicked ||
					CounselorOption[ID].Sprite.color != CounselorOption[ID].OriginalColor &&
					Input.GetButtonDown(InputNames.Xbox_A))
				{
					int NewID = 1;

					while (NewID < 7)
					{
						if (NewID != ID)
						{
							CounselorOption[NewID].gameObject.SetActive(false);
						}

						NewID++;
					}

					Yandere.CharacterAnimation["f02_countdown_00"].time = 10;

					MyAudio.clip = Choice;
					MyAudio.pitch = 1;
					MyAudio.Play();

					Cursor.lockState = CursorLockMode.Locked;
					Cursor.visible = false;

					Reticle.SetActive(false);
					InterrogationPhase++;
					Answer = ID;
					Timer = 0;
				}

				ID++;
			}

			if (Timer > 10)
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;

				Reticle.SetActive(false);
				SilentTreatment = true;
				InterrogationPhase++;
				Timer = 0;
			}
		}
		//Return camera to normal.
		else if (InterrogationPhase == 5)
		{
			int ID = 1;

			if (SilentTreatment)
			{
				CounselorOptions.transform.localScale += new Vector3(Time.deltaTime * 2, Time.deltaTime * 2, Time.deltaTime * 2);

				while (ID < 7)
				{
					CounselorOption[ID].transform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);

					ID++;
				}
			}

			if (Timer > 3 || Input.GetButtonDown(InputNames.Xbox_A))
			{
				CounselorOptions.transform.localScale = new Vector3(1, 1, 1);
				CounselorOptions.SetActive(false);
				CounselorBar.SetActive(true);

				Yandere.transform.position = new Vector3(-27.51f, 0, 12);
				Yandere.MainCamera.transform.position = CameraTarget.position;
				Yandere.MainCamera.transform.eulerAngles = CameraTarget.eulerAngles;

				if (SilentTreatment)
				{
					MyAudio.clip = Silence;
					CounselorSubtitle.text = "...";
				}
				//Yandere-chan gives her response.
				else
				{
					//Excuse
					if (Answer == 1)
					{
						MyAudio.clip = ExcuseClips[CrimeID];
						CounselorSubtitle.text = Excuses[CrimeID];

						     if (CrimeID == 1){CounselorGlobals.BloodExcuseUsed++;}
						else if (CrimeID == 2){CounselorGlobals.InsanityExcuseUsed++;}
						else if (CrimeID == 3){CounselorGlobals.LewdExcuseUsed++;}
						else if (CrimeID == 4){CounselorGlobals.TheftExcuseUsed++;}
						else if (CrimeID == 5){CounselorGlobals.TrespassExcuseUsed++;}
						else if (CrimeID == 6){CounselorGlobals.WeaponExcuseUsed++;}
					}
					//Apologize
					else if (Answer == 2)
					{
						MyAudio.clip = ApologyClip;
						CounselorSubtitle.text = Apology;
						CounselorGlobals.ApologiesUsed++;
					}
					//Lie
					else if (Answer == 3)
					{
						MyAudio.clip = LieClips[CrimeID];
						CounselorSubtitle.text = Lies[CrimeID];
					}
					//Blame Delinquents
					else if (Answer == 4)
					{
						MyAudio.clip = DelinquentClips[CrimeID];
						CounselorSubtitle.text = Delinquents[CrimeID];
					}
					//Flirt
					else if (Answer == 5)
					{
						MyAudio.clip = FlirtClip;
						CounselorSubtitle.text = Flirt;
					}
					//Threaten
					else if (Answer == 6)
					{
						MyAudio.clip = ThreatenClip;
						CounselorSubtitle.text = Threaten;
					}
				}

				Yandere.CharacterAnimation.Play("f02_sit_00");
				InterrogationPhase++;
				MyAudio.Play();
				Timer = 0;
			}
		}
		//Yandere-chan gives her response.
		else if (InterrogationPhase == 6)
		{
			if (Answer == 6)
			{
				Yandere.Sanity = Mathf.MoveTowards(Yandere.Sanity, 0, Time.deltaTime * 7.5f);
				Rumble.volume += Time.deltaTime * .075f;
			}

			//Guidance Counselor reacts to Yandere-chan's response.
			if (Timer > MyAudio.clip.length + .5f || Input.GetButtonDown(InputNames.Xbox_A))
			{
				if (SilentTreatment)
				{
					int ID = Random.Range(0, 3);

					MyAudio.clip = SilentClips[ID];
					CounselorSubtitle.text = Silents[ID];
					Patience--;
				}
				//React to Excuse
				else if (Answer == 1)
				{
						 if (CrimeID == 1){Debug.Log("The player's crime is Bloodiness.");}
					else if (CrimeID == 2){Debug.Log("The player's crime is Insanity.");}
					else if (CrimeID == 3){Debug.Log("The player's crime is Lewdness.");}
					else if (CrimeID == 4){Debug.Log("The player's crime is Theft.");}
					else if (CrimeID == 5){Debug.Log("The player's crime is Trespassing.");}
					else if (CrimeID == 6){Debug.Log("The player's crime is Weaponry.");}

					Debug.Log("The player has chosen to use an exuse.");

					bool ExcuseInvalid = false;

					if (CrimeID == 1 && CounselorGlobals.BloodExcuseUsed > 1 ||
						CrimeID == 2 && CounselorGlobals.InsanityExcuseUsed > 1 ||
						CrimeID == 3 && CounselorGlobals.LewdExcuseUsed > 1 ||
						CrimeID == 4 && CounselorGlobals.TheftExcuseUsed > 1 ||
						CrimeID == 5 && CounselorGlobals.TrespassExcuseUsed > 1 ||
						CrimeID == 6 && CounselorGlobals.WeaponExcuseUsed > 1)
					{
						Debug.Log("Yandere-chan has already used this excuse before.");

						ExcuseInvalid = true;
					}

					if (!ExcuseInvalid)
					{
						Debug.Log("Yandere-chan's excuse is not invalid!");

						MyAudio.clip = AcceptExcuseClips[CrimeID];
						CounselorSubtitle.text = AcceptExcuses[CrimeID];
						MyAnimation.CrossFade("CounselorRelief", 1);

						Stern = false;
						Patience = 1;
					}
					else
					{
						Debug.Log("Yandere-chan's excuse has been deemed invalid.");

						int ID = Random.Range(0, 3);

						MyAudio.clip = RejectExcuseClips[ID];
						CounselorSubtitle.text = RejectExcuses[ID];
						MyAnimation.CrossFade("CounselorAnnoyed");

						Angry = true;
						Patience--;
					}
				}
				//React to Apology
				else if (Answer == 2)
				{
					if (CounselorGlobals.ApologiesUsed == 1)
					{
						MyAudio.clip = AcceptApologyClip;
						CounselorSubtitle.text = AcceptApology;
						MyAnimation.CrossFade("CounselorRelief", 1);

						Stern = false;
						Patience = 1;
					}
					else
					{
						int ID = Random.Range(0, 3);

						MyAudio.clip = RejectApologyClips[ID];
						CounselorSubtitle.text = RejectApologies[ID];
						MyAnimation.CrossFade("CounselorAnnoyed");

						Patience--;
					}
				}
				//React to Lie
				else if (Answer == 3)
				{
					int ID = Random.Range(0, 5);

					MyAudio.clip = RejectLieClips[ID];
					CounselorSubtitle.text = RejectLies[ID];
					MyAnimation.CrossFade("CounselorAnnoyed");

					Angry = true;
					Patience--;
				}
				//React to Blaming Delinquents
				else if (Answer == 4)
				{
					bool DelinquentsNotPresent = false;
					bool DelinquentsKickedOut = false;
					bool DelinquentsAreDead = false;
					int Delinquents = 5;

					if (StudentGlobals.GetStudentDead(76) &&
						StudentGlobals.GetStudentDead(77) &&
						StudentGlobals.GetStudentDead(78) &&
						StudentGlobals.GetStudentDead(79) &&
						StudentGlobals.GetStudentDead(80))
					{
						DelinquentsAreDead = true;
					}
					else if (StudentGlobals.GetStudentExpelled(76) &&
						StudentGlobals.GetStudentExpelled(77) &&
						StudentGlobals.GetStudentExpelled(78) &&
						StudentGlobals.GetStudentExpelled(79) &&
						StudentGlobals.GetStudentExpelled(80))
					{
						DelinquentsKickedOut = true;
					}
					else
					{
						if (StudentManager.Students[76] == null){Delinquents--;}
						else if (!StudentManager.Students[76].gameObject.activeInHierarchy){Delinquents--;}
						if (StudentManager.Students[77] == null){Delinquents--;}
						else if (!StudentManager.Students[77].gameObject.activeInHierarchy){Delinquents--;}
						if (StudentManager.Students[78] == null){Delinquents--;}
						else if (!StudentManager.Students[78].gameObject.activeInHierarchy){Delinquents--;}
						if (StudentManager.Students[79] == null){Delinquents--;}
						else if (!StudentManager.Students[79].gameObject.activeInHierarchy){Delinquents--;}
						if (StudentManager.Students[80] == null){Delinquents--;}
						else if (!StudentManager.Students[80].gameObject.activeInHierarchy){Delinquents--;}

						if (Delinquents == 0)
						{
							DelinquentsNotPresent = true;
						}
					}

					bool BlameInvalid = false;

					if (CrimeID == 1 && CounselorGlobals.BloodBlameUsed > 1 ||
						CrimeID == 2 && CounselorGlobals.InsanityBlameUsed > 1 ||
						CrimeID == 3 && CounselorGlobals.LewdBlameUsed > 1 ||
						CrimeID == 4 && CounselorGlobals.TheftBlameUsed > 1 ||
						CrimeID == 5 && CounselorGlobals.TrespassBlameUsed > 1 ||
						CrimeID == 6 && CounselorGlobals.WeaponBlameUsed > 1)
					{
						BlameInvalid = true;
					}

					if (DelinquentsAreDead)
					{
						MyAudio.clip = DelinquentsDeadClip;
						CounselorSubtitle.text = DelinquentsDead;
						MyAnimation.CrossFade("CounselorAnnoyed");

						Angry = true;
						Patience--;
					}
					else if (DelinquentsKickedOut)
					{
						MyAudio.clip = DelinquentsExpelledClip;
						CounselorSubtitle.text = DelinquentsExpelled;
						MyAnimation.CrossFade("CounselorAnnoyed");

						Patience--;
					}
					else if (DelinquentsNotPresent)
					{
						MyAudio.clip = DelinquentsGoneClip;
						CounselorSubtitle.text = DelinquentsGone;
						MyAnimation.CrossFade("CounselorAnnoyed");

						Patience--;
					}
					else if (!BlameInvalid)
					{
						if (CrimeID == 1)
						{
							Debug.Log("Banning weapons.");
							CounselorGlobals.WeaponsBanned++;
						}

						MyAudio.clip = AcceptBlameClips[CrimeID];
						CounselorSubtitle.text = AcceptBlames[CrimeID];
						MyAnimation.CrossFade("CounselorSad", 1);

						Stern = false;
						Sad = true;

						Patience = 1;

						CounselorGlobals.DelinquentPunishments++;

						if (CrimeID == 1){CounselorGlobals.BloodBlameUsed++;}
						else if (CrimeID == 2){CounselorGlobals.InsanityBlameUsed++;}
						else if (CrimeID == 3){CounselorGlobals.LewdBlameUsed++;}
						else if (CrimeID == 4){CounselorGlobals.TheftBlameUsed++;}
						else if (CrimeID == 5){CounselorGlobals.TrespassBlameUsed++;}
						else if (CrimeID == 6){CounselorGlobals.WeaponBlameUsed++;}

						if (CounselorGlobals.DelinquentPunishments > 5)
						{
							this.MustExpelDelinquents = true;
						}
					}
					else
					{
						int ID = Random.Range(0, 3);

						MyAudio.clip = RejectBlameClips[ID];
						CounselorSubtitle.text = RejectBlames[ID];
						MyAnimation.CrossFade("CounselorAnnoyed");

						Patience--;
					}
				}
				//React to Flirting
				else if (Answer == 5)
				{
					int ID = Random.Range(0, 3);

					MyAudio.clip = RejectFlirtClips[ID];
					CounselorSubtitle.text = RejectFlirts[ID];
					MyAnimation.CrossFade("CounselorAnnoyed");

					Angry = true;
					Patience--;
				}
				//React to Threat
				else if (Answer == 6)
				{
					MyAudio.clip = RejectThreatClip;
					CounselorSubtitle.text = RejectThreat;
					MyAnimation.CrossFade("CounselorAnnoyed");

					InterrogationPhase += 2;
					Patience = -6;
					Angry = true;
				}

				if (Patience < -6)
				{
					Patience = -6;
				}

				     if (Patience == 1)  {GenkaChibi.mainTexture = HappyChibi;}
				else if (Patience == -6) {GenkaChibi.mainTexture = MadChibi;}
				else                     {GenkaChibi.mainTexture = AnnoyedChibi;}

				InterrogationPhase++;
				MyAudio.Play();
				Timer = 0;
			}
		}
		//The counselor reacts to Yandere-chan's response.
		else if (InterrogationPhase == 7)
		{
			if (Timer > MyAudio.clip.length + .5f || Input.GetButtonDown(InputNames.Xbox_A))
			{
				//Give negative closing statement
				if (Patience < 0)
				{
					int ID = Random.Range(0, 3);

					MyAudio.clip = BadClosingClips[ID];
					CounselorSubtitle.text = BadClosings[ID];
					MyAnimation.CrossFade("CounselorArmsCrossed", 1);

					InterrogationPhase += 2;
				}
				//Give positive closing statement
				else
				{
					if (MustExpelDelinquents)
					{
						MyAudio.clip = ExpelDelinquentsClip;
						CounselorSubtitle.text = ExpelDelinquents;
						MustExpelDelinquents = false;

						StudentGlobals.SetStudentExpelled(76, true);
						StudentGlobals.SetStudentExpelled(77, true);
						StudentGlobals.SetStudentExpelled(78, true);
						StudentGlobals.SetStudentExpelled(79, true);
						StudentGlobals.SetStudentExpelled(80, true);

                        StudentManager.Students[76].gameObject.SetActive(false);
                        StudentManager.Students[77].gameObject.SetActive(false);
                        StudentManager.Students[78].gameObject.SetActive(false);
                        StudentManager.Students[79].gameObject.SetActive(false);
                        StudentManager.Students[80].gameObject.SetActive(false);

                        ExpelledDelinquents = true;
					}
					else if (Answer == 4)
					{
						MyAudio.clip = BlameClosingClips[CrimeID];
						CounselorSubtitle.text = BlameClosings[CrimeID];
					}
					else
					{
						int ID = Random.Range(0, 3);

						MyAudio.clip = FreeToLeaveClips[ID];
						CounselorSubtitle.text = FreeToLeaves[ID];
						MyAnimation.CrossFade("CounselorArmsCrossed", 1);

						Stern = true;
					}

					InterrogationPhase++;
				}

				MyAudio.Play();
				Timer = 0;
			}
		}
		//The counselor gives a positive closing statement.
		else if (InterrogationPhase == 8)
		{
			if (Timer > MyAudio.clip.length + .5f || Input.GetButtonDown(InputNames.Xbox_A))
			{
				//Let player leave

				CounselorDoor.FadeOut = true;
				CounselorDoor.Exit = true;
				Interrogating = false;

				InterrogationPhase = 0;
				Timer = 0;
			}
		}
		//The counselor gives a negative closing statement.
		else if (InterrogationPhase == 9)
		{
			if (Timer > MyAudio.clip.length + .5f || Input.GetButtonDown(InputNames.Xbox_A))
			{
				MyAnimation.Play("CounselorSlamDesk");
				InterrogationPhase++;
				MyAudio.Stop();
				Stern = false;
				Angry = true;
				Timer = 0;
			}
		}
		//SLAM HANDS ON DESK
		else if (InterrogationPhase == 10)
		{
			if (Timer > .5f)
			{
				if (!Slammed)
				{
					GamePad.SetVibration(0, 1, 1);
					this.VibrationCheck = true;
					this.VibrationTimer = .2f;

					AudioSource.PlayClipAtPoint(Slam, transform.position);
					Shake.shakeAmount = .1f;
					Shake.enabled = true;
					Shake.shake = .5f;
					Slammed = true;
				}

				Shake.shakeAmount = Mathf.Lerp(Shake.shakeAmount, 0, Time.deltaTime);
			}

			Shake.shakeAmount = Mathf.Lerp(Shake.shakeAmount, 0, Time.deltaTime * 10);

			if (Timer > 1.5f || Input.GetButtonDown(InputNames.Xbox_A))
			{
				MyAudio.clip = SuspensionClips[Mathf.Abs(Patience)];
				CounselorSubtitle.text = Suspensions[Mathf.Abs(Patience)];

				MyAnimation.Play("CounselorSlamIdle");
				Shake.enabled = false;
				InterrogationPhase++;
				MyAudio.Play();
				Timer = 0;
			}
		}
		//Suspend Yandere-chan
		else if (InterrogationPhase == 11)
		{
			if (Timer > MyAudio.clip.length + .5f || Input.GetButtonDown(InputNames.Xbox_A))
			{
				//Yandere.Police.Darkness.color += new Color(0, 0, 0, Time.deltaTime);

				if (!Yandere.Police.FadeOut)
				{
					CounselorGlobals.CounselorPunishments++;

					Yandere.Police.Darkness.color = new Color(0, 0, 0, 0);

					Yandere.Police.SuspensionLength = Mathf.Abs(Patience);
					Yandere.Police.Darkness.enabled = true;
					Yandere.Police.ClubActivity = false;
					Yandere.Police.Suspended = true;
					Yandere.Police.FadeOut = true;

					Yandere.ShoulderCamera.HUD.SetActive(true);

					InterrogationPhase++;
					Expelled = true;
					Timer = 0;

					this.Yandere.Senpai = this.StudentManager.Students[1].transform;

					StudentManager.Reputation.PendingRep -= 10;
					StudentManager.Reputation.UpdateRep();
				}
			}
		}

		if (InterrogationPhase > 6)
		{
			Yandere.Sanity = Mathf.Lerp(Yandere.Sanity, 100, Time.deltaTime);
			Rumble.volume = Mathf.Lerp(Rumble.volume, 0, Time.deltaTime); 

			GenkaChibi.transform.localPosition = Vector3.Lerp(
				GenkaChibi.transform.localPosition,
				new Vector3(0, 0 + (90 * Patience), 0),
				Time.deltaTime * 10);
		}
	}
}