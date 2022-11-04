using UnityEngine;

public class NoteWindowScript : MonoBehaviour
{
	public InputManagerScript InputManager;
	public NoteLockerScript NoteLocker;
	public PromptBarScript PromptBar;
	public YandereScript Yandere;
	public ClockScript Clock;

	public Transform SubHighlight;
	public Transform SubMenu;

	public UISprite[] SlotHighlights;
	public UILabel[] SlotLabels;
	public UILabel[] SubLabels;

	public string[] OriginalText;
	public string[] Subjects;
	public string[] Locations;
	public string[] Times;
	public float[] Hours;

	public bool[] SlotsFilled;

	public int SubSlot = 0;
	public int MeetID = 0;
	public int Slot = 1;

	public float Rotation = 0.0f;
	public float TimeID = 0.0f;
	public int ID = 0;

	public bool Selecting = false;
	public bool Fade = false;
	public bool Show = false;

	void Start()
	{
		this.SubMenu.transform.localScale = Vector3.zero;

		this.transform.localPosition = new Vector3(455.0f, -965.0f, 0.0f);
		this.transform.localEulerAngles = new Vector3(0.0f, 0.0f, -90.0f);

		this.OriginalText[1] = this.SlotLabels[1].text;
		this.OriginalText[2] = this.SlotLabels[2].text;
		this.OriginalText[3] = this.SlotLabels[3].text;

		this.UpdateHighlights();
		this.UpdateSubLabels();
	}

	public UITexture Stationery;
	public UISprite Background1;
	public UISprite Background2;

	public Texture LifeNoteTexture;
	public UILabel[] Labels;
	public bool LifeNote;

	public int TargetStudent;

	public string[] MurderMethods;

	public void BecomeLifeNote()
	{
		this.Stationery.mainTexture = LifeNoteTexture;
		this.Stationery.color = new Color(1, 1, 1, 1);
		this.Background2.color = new Color(0, 0, 0, 1);

		foreach (UILabel Label in Labels)
		{
			if (Label != null)
			{
				Label.color = new Color(1, 1, 1, 1);
			}
		}

		Labels[1].color = new Color(1, 1, 1, 0);
		Labels[2].color = new Color(1, 1, 1, 0);
		Labels[3].transform.localPosition = new Vector3(-365, 265, 0);
		Labels[3].text = "______________";
		Labels[4].text = "will die from";
		Labels[8].color = new Color(1, 1, 1, 0);

		SlotHighlights[1].transform.localPosition = new Vector3(-100, 280, 0);

		foreach (UILabel Label in SubLabels)
		{
			if (Label != null)
			{
				Label.color = new Color(1, 1, 1, 1);
			}
		}

		LifeNote = true;
	}

	void Update()
	{
		float lerpSpeed = Time.unscaledDeltaTime * 10.0f;

		if (!this.Show)
		{
			if (this.Rotation > -90.0f)
			{
				this.transform.localPosition = Vector3.Lerp(
					this.transform.localPosition, new Vector3(455.0f, -965.0f, 0.0f), lerpSpeed);
				this.Rotation = Mathf.Lerp(this.Rotation, -91.0f, lerpSpeed);

				this.transform.localEulerAngles = new Vector3(
					this.transform.localEulerAngles.x,
					this.transform.localEulerAngles.y,
					this.Rotation);
			}
			else
			{
				this.gameObject.SetActive(false);
			}
		}
		else
		{
			this.transform.localPosition = Vector3.Lerp(
				this.transform.localPosition, Vector3.zero, lerpSpeed);
			this.Rotation = Mathf.Lerp(this.Rotation, 0.0f, lerpSpeed);

			this.transform.localEulerAngles = new Vector3(
				this.transform.localEulerAngles.x,
				this.transform.localEulerAngles.y,
				this.Rotation);

			if (!this.Selecting)
			{
				if (this.SubMenu.transform.localScale.x > 0.10f)
				{
					this.SubMenu.transform.localScale = Vector3.Lerp(
						this.SubMenu.transform.localScale, Vector3.zero, lerpSpeed);
				}
				else
				{
					this.SubMenu.transform.localScale = Vector3.zero;
				}

				if (this.InputManager.TappedDown)
				{
					this.Slot++;

					if (this.Slot > 3)
					{
						this.Slot = 1;
					}

					this.UpdateHighlights();
				}

				if (this.InputManager.TappedUp)
				{
					this.Slot--;

					if (this.Slot < 1)
					{
						this.Slot = 3;
					}

					this.UpdateHighlights();
				}

				if (Input.GetButtonDown(InputNames.Xbox_A))
				{
					if (this.LifeNote && this.Slot == 1)
					{
						this.Yandere.PauseScreen.transform.parent.GetComponent<UIPanel>().alpha = 1;

						this.Yandere.PauseScreen.StudentInfoMenu.UsingLifeNote = true;

						this.Yandere.PauseScreen.StudentInfoMenu.gameObject.SetActive(true);
						this.Yandere.PauseScreen.StudentInfoMenu.Column = 0;
						this.Yandere.PauseScreen.StudentInfoMenu.Row = 0;
						this.Yandere.PauseScreen.StudentInfoMenu.UpdateHighlight();
						this.Yandere.PauseScreen.StudentInfoMenu.GrabbedPortraits = false;
						//this.StartCoroutine(this.Yandere.PauseScreen.StudentInfoMenu.UpdatePortraits());
						this.Yandere.PauseScreen.MainMenu.SetActive(false);
						this.Yandere.PauseScreen.Panel.enabled = true;
						this.Yandere.PauseScreen.Sideways = true;
						this.Yandere.PauseScreen.Show = true;
						Time.timeScale = 0.0001f;

						this.Yandere.PromptBar.ClearButtons();
						this.Yandere.PromptBar.Label[1].text = "Cancel";
						this.Yandere.PromptBar.UpdateButtons();
						this.Yandere.PromptBar.Show = true;

						this.gameObject.SetActive(false);
					}
					else
					{
						this.PromptBar.Label[2].text = string.Empty;
						this.PromptBar.UpdateButtons();
						this.Selecting = true;

						this.UpdateSubLabels();
					}
				}

				if (Input.GetButtonDown(InputNames.Xbox_B))
				{
					this.Exit();
				}

				if (Input.GetButtonDown(InputNames.Xbox_X))
				{
					if (this.SlotsFilled[1] && this.SlotsFilled[2] && this.SlotsFilled[3])
					{
						if (this.LifeNote)
						{
							AudioSource.PlayClipAtPoint(this.Yandere.DramaticWriting, this.Yandere.transform.position);

							this.Yandere.CharacterAnimation.CrossFade(this.Yandere.IdleAnim);

							this.Yandere.CharacterAnimation["f02_dramaticWriting_00"].speed = 2;
							this.Yandere.CharacterAnimation["f02_dramaticWriting_00"].time = 0;
							this.Yandere.CharacterAnimation["f02_dramaticWriting_00"].weight = .75f;

							this.Yandere.CharacterAnimation.CrossFade("f02_dramaticWriting_00");

							this.Yandere.WritingName = true;

							this.Exit();
						}
						else
						{
							this.NoteLocker.MeetID = this.MeetID;
							this.NoteLocker.MeetTime = this.TimeID;

							this.NoteLocker.Prompt.enabled = false;
							this.NoteLocker.CanLeaveNote = false;
							this.NoteLocker.NoteLeft = true;

							/*

							Subject 1: Making Friends
							Subject 2: Low Grades
							Subject 3: Fighting Evil
							Subject 4: Suspicious Activity
							Subject 5: Your Friends
							Subject 6: Social Media
							Subject 7: Bullying
							Subject 8: The Supernatural
							Subject 9: Compensated Dating
							Subject 10: Domestic Abuse

							Loner
							TeachersPet
							Heroic
							Coward
							Evil
							SocialButterfly
							Lovestruck
							Dangerous
							Strict
							PhoneAddict 
							Fragile
							Spiteful 
							Sleuth
							Vengeful
							Protective
							Violent
							Nemesis

							*/

							//Kokona
							if (this.NoteLocker.Student.StudentID == 30)
							{
								if (this.SlotLabels[1].text == this.Subjects[9])
								{
									this.NoteLocker.Success = true;
								}
							}
							//Fragile Girl
							else if (this.NoteLocker.Student.StudentID == 5)
							{
								if (this.NoteLocker.Student.Bullied &&
									this.SlotLabels[1].text == this.Subjects[7] && this.MeetID > 7)
								{
									this.NoteLocker.Success = true;
								}
							}
                            //Osana
                            else if (this.NoteLocker.Student.StudentID == 11)
                            {
                                if (this.SlotLabels[1].text == this.Subjects[10])
                                {
                                    this.NoteLocker.Success = true;
                                }
                            }

                            //Loner
                            if (this.NoteLocker.Student.Persona == PersonaType.Loner &&
								this.SlotLabels[1].text == this.Subjects[1])
							{
								this.NoteLocker.Success = true;
							}
							//Teacher's Pet
							else if (this.NoteLocker.Student.Persona == PersonaType.TeachersPet &&
								this.SlotLabels[1].text == this.Subjects[2])
							{
								this.NoteLocker.Success = true;
							}
							//Heroic or Sleuth
							else if (this.NoteLocker.Student.Persona == PersonaType.Heroic ||
									 this.NoteLocker.Student.Persona == PersonaType.Sleuth)
							{
								if (this.SlotLabels[1].text == this.Subjects[3])
								{
									this.NoteLocker.Success = true;
								}
							}
							//Coward
							else if (this.NoteLocker.Student.Persona == PersonaType.Coward &&
								this.SlotLabels[1].text == this.Subjects[4])
							{
								this.NoteLocker.Success = true;
							}
							//Social Butterfly
							else if (this.NoteLocker.Student.Persona == PersonaType.SocialButterfly)
							{
								if (this.SlotLabels[1].text == this.Subjects[1] ||
									this.SlotLabels[1].text == this.Subjects[5])
								{
									this.NoteLocker.Success = true;
								}
							}
							//Phone Addicts
							else if (this.NoteLocker.Student.Persona == PersonaType.PhoneAddict &&
								this.SlotLabels[1].text == this.Subjects[6])
							{
								this.NoteLocker.Success = true;
							}
							//Occult Club or Basu Sisters
							else if (this.NoteLocker.Student.StudentID == 2 ||
									 this.NoteLocker.Student.StudentID == 3 || 
									 this.NoteLocker.Student.Club == ClubType.Occult)
							{
								if (this.SlotLabels[1].text == this.Subjects[8])
								{
									this.NoteLocker.Success = true;
								}
							}
							//Bullies
							else if (this.NoteLocker.Student.Club == ClubType.Bully)
							{
								if (this.SlotLabels[1].text == this.Subjects[5] ||
									this.SlotLabels[1].text == this.Subjects[10])
								{
									this.NoteLocker.Success = true;
								}
							}

							this.NoteLocker.FindStudentLocker.Prompt.Hide();
							this.NoteLocker.FindStudentLocker.Prompt.Label[0].text = "     " + "You Must Wait For Other Student";
							this.NoteLocker.FindStudentLocker.TargetedStudent = this.NoteLocker.Student;
							//this.NoteLocker.FindStudentLocker.enabled = false;

							//Heart Arrow
							this.NoteLocker.transform.GetChild(0).gameObject.SetActive(false);
						}

						this.Exit();
					}
				}
			}
			else
			{
				this.SubMenu.transform.localScale = Vector3.Lerp(
					this.SubMenu.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f), lerpSpeed);

				if (this.InputManager.TappedDown)
				{
					this.SubSlot++;

					if (this.LifeNote && this.Slot == 2)
					{
						if (this.SubSlot > 6)
						{
							this.SubSlot = 1;
						}
					}
					else
					{
						if (this.SubSlot > 10)
						{
							this.SubSlot = 1;
						}
					}

					this.SubHighlight.localPosition = new Vector3(
						this.SubHighlight.localPosition.x,
						550.0f - (100.0f * this.SubSlot),
						this.SubHighlight.localPosition.z);
				}

				if (this.InputManager.TappedUp)
				{
					this.SubSlot--;

					if (this.LifeNote && this.Slot == 2)
					{
						if (this.SubSlot < 1)
						{
							this.SubSlot = 6;
						}
					}
					else
					{
						if (this.SubSlot < 1)
						{
							this.SubSlot = 10;
						}
					}

					this.SubHighlight.localPosition = new Vector3(
						this.SubHighlight.localPosition.x,
						550.0f - (100.0f * this.SubSlot),
						this.SubHighlight.localPosition.z);
				}

				if (Input.GetButtonDown(InputNames.Xbox_A))
				{
					if (this.SubLabels[this.SubSlot].color.a > 0.50f)
					{
						if ((this.SubLabels[this.SubSlot].text != string.Empty) &&
							(this.SubLabels[this.SubSlot].text != "??????????"))
						{
							this.SlotLabels[this.Slot].text = this.SubLabels[this.SubSlot].text;
							this.SlotsFilled[this.Slot] = true;

							if (this.Slot == 2)
							{
								this.MeetID = this.SubSlot;
							}

							if (this.Slot == 3)
							{
								this.TimeID = this.Hours[this.SubSlot];
							}

							this.CheckForCompletion();

							this.Selecting = false;

							this.SubSlot = 1;

							this.SubHighlight.localPosition = new Vector3(
								this.SubHighlight.localPosition.x,
								450.0f,
								this.SubHighlight.localPosition.z);
						}
					}
				}

				if (Input.GetButtonDown(InputNames.Xbox_B))
				{
					this.CheckForCompletion();

					this.Selecting = false;

					this.SubSlot = 1;

					this.SubHighlight.localPosition = new Vector3(
						this.SubHighlight.localPosition.x,
						450.0f,
						this.SubHighlight.localPosition.z);
				}
			}

			UISprite slotHighlight = this.SlotHighlights[this.Slot];

			if (!this.Fade)
			{
				slotHighlight.color = new Color(
					slotHighlight.color.r,
					slotHighlight.color.g,
					slotHighlight.color.b,
					slotHighlight.color.a + (1.0f / 60.0f));

				if (slotHighlight.color.a >= 0.50f)
				{
					this.Fade = true;
				}
			}
			else
			{
				slotHighlight.color = new Color(
					slotHighlight.color.r,
					slotHighlight.color.g,
					slotHighlight.color.b,
					slotHighlight.color.a - (1.0f / 60.0f));

				if (slotHighlight.color.a <= 0.0f)
				{
					this.Fade = false;
				}
			}
		}
	}

	void UpdateHighlights()
	{
		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < this.SlotHighlights.Length; ID++)
		{
			UISprite slotHighlight = this.SlotHighlights[ID];
			slotHighlight.color = new Color(
				slotHighlight.color.r,
				slotHighlight.color.g,
				slotHighlight.color.b,
				0.0f);
		}
	}

	void UpdateSubLabels()
	{
		if (this.Slot == 1)
		{
			// [af] Converted while loop to for loop.
			for (this.ID = 1; this.ID < this.SubLabels.Length; this.ID++)
			{
				UILabel subLabel = this.SubLabels[this.ID];
				subLabel.text = this.Subjects[this.ID];
				subLabel.color = new Color(
					subLabel.color.r,
					subLabel.color.g,
					subLabel.color.b,
					1.0f);
			}

            //If the player has eavesdropped on Kokona...
			if (!EventGlobals.Event1)
			{
				this.SubLabels[9].text = "??????????";
			}

            //If the player has eavesdropped on both Osana conversations...
            if (!EventGlobals.OsanaEvent1 || !EventGlobals.OsanaEvent2)
            {
                this.SubLabels[10].text = "??????????";
            }
        }
		else if (this.Slot == 2)
		{
			for (this.ID = 1; this.ID < this.SubLabels.Length; this.ID++)
			{
				UILabel subLabel = this.SubLabels[this.ID];

				subLabel.color = new Color(
					subLabel.color.r,
					subLabel.color.g,
					subLabel.color.b,
					1.0f);

				if (this.LifeNote)
				{
					subLabel.text = this.MurderMethods[this.ID];
				}
				else
				{
					subLabel.text = this.Locations[this.ID];
				}
			}
		}
		else if (this.Slot == 3)
		{
			// [af] Converted while loop to for loop.
			for (this.ID = 1; this.ID < this.SubLabels.Length; this.ID++)
			{
				UILabel subLabel = this.SubLabels[this.ID];
				subLabel.text = this.Times[this.ID];
				subLabel.color = new Color(
					subLabel.color.r,
					subLabel.color.g,
					subLabel.color.b,
					1.0f);
			}

			this.DisableOptions();
		}
	}

	public void CheckForCompletion()
	{
		if (this.SlotsFilled[1] && this.SlotsFilled[2] && this.SlotsFilled[3])
		{
			this.PromptBar.Label[2].text = "Finish";
			this.PromptBar.UpdateButtons();
		}
	}

	void Exit()
	{
		this.UpdateHighlights();

		if (!this.Yandere.WritingName)
		{
			this.Yandere.CanMove = true;
		}

		this.Yandere.RPGCamera.enabled = true;
		this.Yandere.Blur.enabled = false;
		this.Yandere.HUD.alpha = 1.0f;

		Time.timeScale = 1.0f;
		this.Show = false;
		this.Slot = 1;

		this.PromptBar.Label[0].text = string.Empty;
		this.PromptBar.Label[1].text = string.Empty;
		this.PromptBar.Label[2].text = string.Empty;
		this.PromptBar.Label[4].text = string.Empty;
		this.PromptBar.Show = false;
		this.PromptBar.UpdateButtons();

		this.SlotLabels[1].text = this.OriginalText[1];
		this.SlotLabels[2].text = this.OriginalText[2];
		this.SlotLabels[3].text = this.OriginalText[3];

		this.SlotsFilled[1] = false;
		this.SlotsFilled[2] = false;
		this.SlotsFilled[3] = false;
	}

	void DisableOptions()
	{
		if (this.Clock.HourTime >= 7.25f)
		{
			UILabel subLabel1 = this.SubLabels[1];
			subLabel1.color = new Color(
				subLabel1.color.r,
				subLabel1.color.g,
				subLabel1.color.b,
				0.50f);
		}

		if (this.Clock.HourTime >= 7.50f)
		{
			UILabel subLabel2 = this.SubLabels[2];
			subLabel2.color = new Color(
				subLabel2.color.r,
				subLabel2.color.g,
				subLabel2.color.b,
				0.50f);
		}

		if (this.Clock.HourTime >= 7.75f)
		{
			UILabel subLabel3 = this.SubLabels[3];
			subLabel3.color = new Color(
				subLabel3.color.r,
				subLabel3.color.g,
				subLabel3.color.b,
				0.50f);
		}

		if (this.Clock.HourTime >= 8.00f)
		{
			UILabel subLabel4 = this.SubLabels[4];
			subLabel4.color = new Color(
				subLabel4.color.r,
				subLabel4.color.g,
				subLabel4.color.b,
				0.50f);
		}

		if (this.Clock.HourTime >= 8.25f)
		{
			UILabel subLabel5 = this.SubLabels[5];
			subLabel5.color = new Color(
				subLabel5.color.r,
				subLabel5.color.g,
				subLabel5.color.b,
				0.50f);
		}

		if (this.Clock.HourTime >= 15.50f)
		{
			UILabel subLabel6 = this.SubLabels[6];
			subLabel6.color = new Color(
				subLabel6.color.r,
				subLabel6.color.g,
				subLabel6.color.b,
				0.50f);
		}

		if (this.Clock.HourTime >= 16.00f)
		{
			UILabel subLabel7 = this.SubLabels[7];
			subLabel7.color = new Color(
				subLabel7.color.r,
				subLabel7.color.g,
				subLabel7.color.b,
				0.50f);
		}

		if (this.Clock.HourTime >= 16.50f)
		{
			UILabel subLabel8 = this.SubLabels[8];
			subLabel8.color = new Color(
				subLabel8.color.r,
				subLabel8.color.g,
				subLabel8.color.b,
				0.50f);
		}

		if (this.Clock.HourTime >= 17.00f)
		{
			UILabel subLabel9 = this.SubLabels[9];
			subLabel9.color = new Color(
				subLabel9.color.r,
				subLabel9.color.g,
				subLabel9.color.b,
				0.50f);
		}

		if (this.Clock.HourTime >= 17.50f)
		{
			UILabel subLabel10 = this.SubLabels[10];
			subLabel10.color = new Color(
				subLabel10.color.r,
				subLabel10.color.g,
				subLabel10.color.b,
				0.50f);
		}
	}
}