using UnityEngine;
using UnityEngine.SceneManagement;

public class ClassScript : MonoBehaviour
{
	public CutsceneManagerScript CutsceneManager;
	public InputManagerScript InputManager;
	public PromptBarScript PromptBar;
	public SchemesScript Schemes;
	public PortalScript Portal;
	public GameObject Poison;

	public UILabel StudyPointsLabel;
	public UILabel[] SubjectLabels;
	public UILabel GradeUpDesc;
	public UILabel GradeUpName;
	public UILabel DescLabel;

	public UISprite Darkness;

	public Transform[] Subject1Bars;
	public Transform[] Subject2Bars;
	public Transform[] Subject3Bars;
	public Transform[] Subject4Bars;
	public Transform[] Subject5Bars;

	public string[] Subject1GradeText;
	public string[] Subject2GradeText;
	public string[] Subject3GradeText;
	public string[] Subject4GradeText;
	public string[] Subject5GradeText;

	public Transform GradeUpWindow;
	public Transform Highlight;

	public int[] SubjectTemp;
	public int[] Subject;
	public string[] Desc;

	public int GradeUpSubject = 0;
	public int StudyPoints = 0;
	public int Selected = 0;
	public int Grade = 0;

	public bool GradeUp = false;
	public bool Show = false;

    public int Biology;
    public int Chemistry;
    public int Language;
    public int Physical;
    public int Psychology;

    public int BiologyGrade;
    public int ChemistryGrade;
    public int LanguageGrade;
    public int PhysicalGrade;
    public int PsychologyGrade;

    public int BiologyBonus;
    public int ChemistryBonus;
    public int LanguageBonus;
    public int PhysicalBonus;
    public int PsychologyBonus;

    public int Seduction;
    public int Numbness;
    public int Social;
    public int Stealth;
    public int Speed;
    public int Enlightenment;

    public int SpeedBonus;
    public int SocialBonus;
    public int StealthBonus;
    public int SeductionBonus;
    public int NumbnessBonus;
    public int EnlightenmentBonus;

    void Start()
	{
        this.Biology = ClassGlobals.Biology;
        this.Chemistry = ClassGlobals.Chemistry;
        this.Language = ClassGlobals.Language;
        this.Physical = ClassGlobals.Physical;
        this.Psychology = ClassGlobals.Psychology;

        this.BiologyGrade = ClassGlobals.BiologyGrade;
        this.ChemistryGrade = ClassGlobals.ChemistryGrade;
        this.LanguageGrade = ClassGlobals.LanguageGrade;
        this.PhysicalGrade = ClassGlobals.PhysicalGrade;
        this.PsychologyGrade = ClassGlobals.PsychologyGrade;

        this.BiologyBonus = ClassGlobals.BiologyBonus;
        this.ChemistryBonus = ClassGlobals.ChemistryBonus;
        this.LanguageBonus = ClassGlobals.LanguageBonus;
        this.PhysicalBonus = ClassGlobals.PhysicalBonus;
        this.PsychologyBonus = ClassGlobals.PsychologyBonus;

        this.Seduction = PlayerGlobals.Seduction;
        this.Numbness = PlayerGlobals.Numbness;
        this.Enlightenment = PlayerGlobals.Enlightenment;

        this.SpeedBonus = PlayerGlobals.SpeedBonus;
        this.SocialBonus = PlayerGlobals.SocialBonus;
        this.StealthBonus = PlayerGlobals.StealthBonus;
        this.SeductionBonus = PlayerGlobals.SeductionBonus;
        this.NumbnessBonus = PlayerGlobals.NumbnessBonus;
        this.EnlightenmentBonus = PlayerGlobals.EnlightenmentBonus;

        if (SceneManager.GetActiveScene().name != SceneNames.SchoolScene)
        {
            this.enabled = false;
        }
        else
        {
            this.GradeUpWindow.localScale = Vector3.zero;

            this.Subject[1] = this.Biology;
            this.Subject[2] = this.Chemistry;
            this.Subject[3] = this.Language;
            this.Subject[4] = this.Physical;
            this.Subject[5] = this.Psychology;

            this.DescLabel.text = this.Desc[this.Selected];
            this.UpdateSubjectLabels();

            this.Darkness.color = new Color(
                this.Darkness.color.r,
                this.Darkness.color.g,
                this.Darkness.color.b,
                1.0f);

            this.UpdateBars();
        }
    }

	void Update()
	{
		if (this.Show)
		{
			this.Darkness.color = new Color(
				this.Darkness.color.r,
				this.Darkness.color.g,
				this.Darkness.color.b,
				this.Darkness.color.a - Time.deltaTime);

			if (this.Darkness.color.a <= 0.0f)
			{
				if (Input.GetKeyDown(KeyCode.Backslash))
				{
					this.GivePoints();
				}

				if (Input.GetKeyDown(KeyCode.P))
				{
					this.MaxPhysical();
				}

				this.Darkness.color = new Color(
					this.Darkness.color.r,
					this.Darkness.color.g,
					this.Darkness.color.b,
					0.0f);

				if (this.InputManager.TappedDown)
				{
					this.Selected++;

					if (this.Selected > 5)
					{
						this.Selected = 1;
					}

					this.Highlight.localPosition = new Vector3(
						this.Highlight.localPosition.x,
						375.0f - (125.0f * this.Selected),
						this.Highlight.localPosition.z);

					this.DescLabel.text = this.Desc[this.Selected];
					this.UpdateSubjectLabels();
				}

				if (this.InputManager.TappedUp)
				{
					this.Selected--;

					if (this.Selected < 1)
					{
						this.Selected = 5;
					}

					this.Highlight.localPosition = new Vector3(
						this.Highlight.localPosition.x,
						375.0f - (125.0f * this.Selected),
						this.Highlight.localPosition.z);

					this.DescLabel.text = this.Desc[this.Selected];
					this.UpdateSubjectLabels();
				}

				if (this.InputManager.TappedRight)
				{
					if (this.StudyPoints > 0)
					{
						if ((this.Subject[this.Selected] + this.SubjectTemp[this.Selected]) < 100)
						{
							this.SubjectTemp[this.Selected]++;
							this.StudyPoints--;

							this.UpdateLabel();
							this.UpdateBars();
						}
					}
				}

				if (this.InputManager.TappedLeft)
				{
					if (this.SubjectTemp[this.Selected] > 0)
					{
						this.SubjectTemp[this.Selected]--;
						this.StudyPoints++;

						this.UpdateLabel();
						this.UpdateBars();
					}
				}

				if (Input.GetButtonDown(InputNames.Xbox_A))
				{
					if (this.StudyPoints == 0)
					{
						this.Show = false;

						this.PromptBar.ClearButtons();
						this.PromptBar.Show = false;

						this.Biology = this.Subject[1] + this.SubjectTemp[1];
						this.Chemistry = this.Subject[2] + this.SubjectTemp[2];
						this.Language = this.Subject[3] + this.SubjectTemp[3];
						this.Physical = this.Subject[4] + this.SubjectTemp[4];
						this.Psychology = this.Subject[5] + this.SubjectTemp[5];

						// [af] Converted while loop to for loop.
						for (int ID = 0; ID < 6; ID++)
						{
							this.Subject[ID] += this.SubjectTemp[ID];
							this.SubjectTemp[ID] = 0;
						}

						this.CheckForGradeUp();
					}
				}
			}
		}
		else
		{
			this.Darkness.color = new Color(
				this.Darkness.color.r,
				this.Darkness.color.g,
				this.Darkness.color.b,
				this.Darkness.color.a + Time.deltaTime);

			if (this.Darkness.color.a >= 1.0f)
			{
				this.Darkness.color = new Color(
					this.Darkness.color.r,
					this.Darkness.color.g,
					this.Darkness.color.b,
					1.0f);

				if (!this.GradeUp)
				{
					if (this.GradeUpWindow.localScale.x > 0.10f)
					{
						this.GradeUpWindow.localScale = Vector3.Lerp(
							this.GradeUpWindow.localScale, Vector3.zero, Time.deltaTime * 10.0f);
					}
					else
					{
						this.GradeUpWindow.localScale = Vector3.zero;
					}

					if (this.GradeUpWindow.localScale.x < 0.010f)
					{
						this.GradeUpWindow.localScale = Vector3.zero;

						this.CheckForGradeUp();

						if (!this.GradeUp)
						{
							if (this.ChemistryGrade > 0)
							{
								if (this.Poison != null)
								{
									this.Poison.SetActive(true);
								}
							}

							Debug.Log("CutsceneManager.Scheme is: " + this.CutsceneManager.Scheme);

							//The Guidance Counselor needs to speak to the rival about something.
							//End-of-Day
							if (this.CutsceneManager.Scheme > 0)
							{
								SchemeGlobals.SetSchemeStage(this.CutsceneManager.Scheme, 100);

								this.PromptBar.ClearButtons();
								this.PromptBar.Label[0].text = "Continue";
								this.PromptBar.UpdateButtons();

								this.CutsceneManager.gameObject.SetActive(true);
								this.Schemes.UpdateInstructions();
								this.gameObject.SetActive(false);
							}
							//If we don't have to do anything related to schemes...
							else
							{
								if (!this.Portal.FadeOut)
								{
									Portal.Yandere.PhysicalGrade = this.PhysicalGrade;

									this.PromptBar.Show = false;
									this.Portal.Proceed = true;

									this.gameObject.SetActive(false);
								}
							}
						}
					}
				}
				else
				{
					if (this.GradeUpWindow.localScale.x == 0.0f)
					{
						if (this.GradeUpSubject == 1)
						{
							this.GradeUpName.text = "BIOLOGY RANK UP";
							this.GradeUpDesc.text = this.Subject1GradeText[this.Grade];
						}
						else if (this.GradeUpSubject == 2)
						{
							this.GradeUpName.text = "CHEMISTRY RANK UP";
							this.GradeUpDesc.text = this.Subject2GradeText[this.Grade];
						}
						else if (this.GradeUpSubject == 3)
						{
							this.GradeUpName.text = "LANGUAGE RANK UP";
							this.GradeUpDesc.text = this.Subject3GradeText[this.Grade];
						}
						else if (this.GradeUpSubject == 4)
						{
							this.GradeUpName.text = "PHYSICAL RANK UP";
							this.GradeUpDesc.text = this.Subject4GradeText[this.Grade];
						}
						else if (this.GradeUpSubject == 5)
						{
							this.GradeUpName.text = "PSYCHOLOGY RANK UP";
							this.GradeUpDesc.text = this.Subject5GradeText[this.Grade];
						}

						this.PromptBar.ClearButtons();
						this.PromptBar.Label[0].text = "Continue";
						this.PromptBar.UpdateButtons();
						this.PromptBar.Show = true;
					}
					else if (this.GradeUpWindow.localScale.x > 0.99f)
					{
						if (Input.GetButtonDown(InputNames.Xbox_A))
						{
							this.PromptBar.ClearButtons();
							this.GradeUp = false;
						}
					}

					this.GradeUpWindow.localScale = Vector3.Lerp(
						this.GradeUpWindow.localScale,
						new Vector3(1.0f, 1.0f, 1.0f),
						Time.deltaTime * 10.0f);
				}
			}
		}
	}

	void UpdateSubjectLabels()
	{
		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < 6; ID++)
		{
			this.SubjectLabels[ID].color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
		}

		this.SubjectLabels[this.Selected].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	}

	public void UpdateLabel()
	{
		this.StudyPointsLabel.text = "STUDY POINTS: " + this.StudyPoints;

		if (this.StudyPoints == 0)
		{
			this.PromptBar.Label[0].text = "Confirm";
			this.PromptBar.UpdateButtons();
		}
		else
		{
			this.PromptBar.Label[0].text = string.Empty;
			this.PromptBar.UpdateButtons();
		}
	}

	void UpdateBars()
	{
		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < 6; ID++)
		{
			Transform subject1Bar = this.Subject1Bars[ID];

			if ((this.Subject[1] + this.SubjectTemp[1]) > ((ID - 1) * 20))
			{
				subject1Bar.localScale = new Vector3(
					-((((ID - 1) * 20) - (this.Subject[1] + this.SubjectTemp[1])) / 20.0f),
					subject1Bar.localScale.y,
					subject1Bar.localScale.z);

				if (subject1Bar.localScale.x > 1.0f)
				{
					subject1Bar.localScale = new Vector3(
						1.0f,
						subject1Bar.localScale.y,
						subject1Bar.localScale.z);
				}
			}
			else
			{
				subject1Bar.localScale = new Vector3(
					0.0f,
					subject1Bar.localScale.y,
					subject1Bar.localScale.z);
			}
		}

		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < 6; ID++)
		{
			Transform subject2Bar = this.Subject2Bars[ID];

			if ((this.Subject[2] + this.SubjectTemp[2]) > ((ID - 1) * 20))
			{
				subject2Bar.localScale = new Vector3(
					-((((ID - 1) * 20) - (this.Subject[2] + this.SubjectTemp[2])) / 20.0f),
					subject2Bar.localScale.y,
					subject2Bar.localScale.z);

				if (subject2Bar.localScale.x > 1.0f)
				{
					subject2Bar.localScale = new Vector3(
						1.0f,
						subject2Bar.localScale.y,
						subject2Bar.localScale.z);
				}
			}
			else
			{
				subject2Bar.localScale = new Vector3(
					0.0f,
					subject2Bar.localScale.y,
					subject2Bar.localScale.z);
			}
		}

		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < 6; ID++)
		{
			Transform subject3Bar = this.Subject3Bars[ID];

			if ((this.Subject[3] + this.SubjectTemp[3]) > ((ID - 1) * 20))
			{
				subject3Bar.localScale = new Vector3(
					-((((ID - 1) * 20) - (this.Subject[3] + this.SubjectTemp[3])) / 20.0f),
					subject3Bar.localScale.y,
					subject3Bar.localScale.z);

				if (subject3Bar.localScale.x > 1.0f)
				{
					subject3Bar.localScale = new Vector3(
						1.0f,
						subject3Bar.localScale.y,
						subject3Bar.localScale.z);
				}
			}
			else
			{
				subject3Bar.localScale = new Vector3(
					0.0f,
					subject3Bar.localScale.y,
					subject3Bar.localScale.z);
			}
		}

		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < 6; ID++)
		{
			Transform subject4Bar = this.Subject4Bars[ID];

			if ((this.Subject[4] + this.SubjectTemp[4]) > ((ID - 1) * 20))
			{
				subject4Bar.localScale = new Vector3(
					-((((ID - 1) * 20) - (this.Subject[4] + this.SubjectTemp[4])) / 20.0f),
					subject4Bar.localScale.y,
					subject4Bar.localScale.z);

				if (subject4Bar.localScale.x > 1.0f)
				{
					subject4Bar.localScale = new Vector3(
						1.0f,
						subject4Bar.localScale.y,
						subject4Bar.localScale.z);
				}
			}
			else
			{
				subject4Bar.localScale = new Vector3(
					0.0f,
					subject4Bar.localScale.y,
					subject4Bar.localScale.z);
			}
		}

		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < 6; ID++)
		{
			Transform subject5Bar = this.Subject5Bars[ID];

			if ((this.Subject[5] + this.SubjectTemp[5]) > ((ID - 1) * 20))
			{
				subject5Bar.localScale = new Vector3(
					-((((ID - 1) * 20) - (this.Subject[5] + this.SubjectTemp[5])) / 20.0f),
					subject5Bar.localScale.y,
					subject5Bar.localScale.z);

				if (subject5Bar.localScale.x > 1.0f)
				{
					subject5Bar.localScale = new Vector3(
						1.0f,
						subject5Bar.localScale.y,
						subject5Bar.localScale.z);
				}
			}
			else
			{
				subject5Bar.localScale = new Vector3(
					0.0f,
					subject5Bar.localScale.y,
					subject5Bar.localScale.z);
			}
		}
	}

	void CheckForGradeUp()
	{
		if ((this.Biology >= 20) && (this.BiologyGrade < 1))
		{
			this.BiologyGrade = 1;
			this.GradeUpSubject = 1;
			this.GradeUp = true;
			this.Grade = 1;
		}
		else if ((this.Chemistry >= 20) && (this.ChemistryGrade < 1))
		{
			this.ChemistryGrade = 1;
			this.GradeUpSubject = 2;
			this.GradeUp = true;
			this.Grade = 1;
		}
		else if ((this.Language >= 20) && (this.LanguageGrade < 1))
		{
			this.LanguageGrade = 1;
			this.GradeUpSubject = 3;
			this.GradeUp = true;
			this.Grade = 1;
		}
		else if ((this.Physical >= 20) && (this.PhysicalGrade < 1))
		{
			this.PhysicalGrade = 1;
			this.GradeUpSubject = 4;
			this.GradeUp = true;
			this.Grade = 1;
		}
		else if ((this.Physical >= 40) && (this.PhysicalGrade < 2))
		{
			this.PhysicalGrade = 2;
			this.GradeUpSubject = 4;
			this.GradeUp = true;
			this.Grade = 2;
		}
		else if ((this.Physical >= 60) && (this.PhysicalGrade < 3))
		{
			this.PhysicalGrade = 3;
			this.GradeUpSubject = 4;
			this.GradeUp = true;
			this.Grade = 3;
		}
		else if ((this.Physical >= 80) && (this.PhysicalGrade < 4))
		{
			this.PhysicalGrade = 4;
			this.GradeUpSubject = 4;
			this.GradeUp = true;
			this.Grade = 4;
		}
		else if ((this.Physical == 100) && (this.PhysicalGrade < 5))
		{
			this.PhysicalGrade = 5;
			this.GradeUpSubject = 4;
			this.GradeUp = true;
			this.Grade = 5;
		}
		else if ((this.Psychology >= 20) && (this.PsychologyGrade < 1))
		{
			this.PsychologyGrade = 1;
			this.GradeUpSubject = 5;
			this.GradeUp = true;
			this.Grade = 1;
		}
	}

	void GivePoints()
	{
		this.BiologyGrade = 0;
		this.ChemistryGrade = 0;
		this.LanguageGrade = 0;
		this.PhysicalGrade = 0;
		this.PsychologyGrade = 0;

		this.Biology = 19;
		this.Chemistry = 19;
		this.Language = 19;
		this.Physical = 19;
		this.Psychology = 19;

		this.Subject[1] = this.Biology;
		this.Subject[2] = this.Chemistry;
		this.Subject[3] = this.Language;
		this.Subject[4] = this.Physical;
		this.Subject[5] = this.Psychology;

		this.UpdateBars();
	}

	void MaxPhysical()
	{
		this.PhysicalGrade = 0;
		this.Physical = 99;
		this.Subject[4] = this.Physical;

		this.UpdateBars();
	}
}
