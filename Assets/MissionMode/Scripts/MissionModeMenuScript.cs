using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;

public class MissionModeMenuScript : MonoBehaviour
{
	public PostProcessingProfile Profile;

	public StudentManagerScript StudentManager;
	public NewMissionWindowScript MultiMission;
	public InputManagerScript InputManager;
	public PromptBarScript PromptBar;
	public JsonScript JSON;

	public UITexture CustomTargetPortrait;
	public UILabel CustomDifficultyLabel;
	public UILabel CustomPopulationLabel;
	public UILabel CustomNemesisLabel;

	public UITexture NemesisPortrait;
	public UITexture TargetPortrait;
	public UILabel LoadMissionLabel;
	public UILabel DescriptionLabel;
	public UILabel DifficultyLabel;
	public UILabel PopulationLabel;
	public UILabel NemesisLabel;
	public UILabel ErrorLabel;
	public UILabel Header;

	public UISprite Highlight;
	public UISprite Darkness;

	public Transform CustomMissionWindow;
	public Transform MultiMissionWindow;
	public Transform ObjectiveHighlight;
	public Transform LoadMissionWindow;
	public Transform MissionWindow;
	public Transform InfoChan;
	public Transform Options;
	public Transform Neck;

	public GameObject NowLoading;

	public string[] ConditionDescs;
	public int[] Conditions;

	public string[] ClothingNames;
	public string[] DisposalNames;
	public string[] WeaponNames;

	public int RequiredClothingID = 0;
	public int RequiredDisposalID = 0;
	public int RequiredWeaponID = 0;

	public Transform[] CustomNemesisObjectives;
	public Transform[] NemesisObjectives;
	public UIPanel[] CustomObjectives;
	public Texture[] ConditionIcons;
	public Transform[] Objectives;
	public Transform[] Option;
	public UITexture[] Icons;

	public UILabel[] CustomDescs;
	public UILabel[] Descs;

	public Texture NemesisGraphic;
	public Texture BlankPortrait;

	public string MissionIDString = string.Empty;
	public string TargetName = string.Empty;

	public int NemesisDifficulty = 0;
	public int CustomSelected = 1;
	public int Difficulty = 1;
	public int Selected = 1;
	public int TargetID = 0;
	public int Phase = 0;

	public int Column = 1;
	public int Row = 1;

	public float Rotation = 0.0f;
	public float Speed = 0.0f;
	public float Timer = 0.0f;

    public AudioSource Jukebox;
    public AudioSource MyAudio;

    public AudioClip[] InfoLines;
	public bool[] InfoSpoke;

	void Start()
	{
        transform.position = new Vector3(0, .95f, -4.266667f);

		ColorGradingModel.Settings ColorSettings = this.Profile.colorGrading.settings;
		ColorSettings.basic.saturation = 1;
		ColorSettings.channelMixer.red = new Vector3(1, 0, 0);
		ColorSettings.channelMixer.green = new Vector3(0, 1, 0);
		ColorSettings.channelMixer.blue = new Vector3(0, 0, 1);
		this.Profile.colorGrading.settings = ColorSettings;

		DepthOfFieldModel.Settings DepthSettings = this.Profile.depthOfField.settings;
		DepthSettings.focusDistance = 10;
		DepthSettings.aperture = 11.2f;
		this.Profile.depthOfField.settings = DepthSettings;

		BloomModel.Settings BloomSettings = this.Profile.bloom.settings;
		BloomSettings.bloom.intensity = 1;
		BloomSettings.bloom.threshold = 1;
		BloomSettings.bloom.softKnee = 1;
		BloomSettings.bloom.radius = 4;
		this.Profile.bloom.settings = BloomSettings;

		MissionModeGlobals.MultiMission = false;

		this.NemesisPortrait.transform.parent.localScale = Vector3.zero;
		this.CustomMissionWindow.transform.localScale = Vector3.zero;
		this.MultiMissionWindow.transform.localScale = Vector3.zero;
		this.LoadMissionWindow.transform.localScale = Vector3.zero;
		this.MissionWindow.transform.localScale = Vector3.zero;

		this.Options.transform.localPosition = new Vector3(
			-700.0f,
			this.Options.transform.localPosition.y,
			this.Options.transform.localPosition.z);

		this.Highlight.color = new Color(
			this.Highlight.color.r,
			this.Highlight.color.g,
			this.Highlight.color.b,
			0.0f);

		this.Darkness.color = new Color(
			this.Darkness.color.r,
			this.Darkness.color.g,
			this.Darkness.color.b,
			1.0f);

		this.Header.color = new Color(
			this.Header.color.r,
			this.Header.color.g,
			this.Header.color.b,
			0.0f);

		Time.timeScale = 1.0f;

		this.CustomDescs[2].text = this.ConditionDescs[1] + " " + this.WeaponNames[1];
		this.CustomDescs[3].text = this.ConditionDescs[2] + " " + this.ClothingNames[1];
		this.CustomDescs[4].text = this.ConditionDescs[3] + " " + this.DisposalNames[1];

		// [af] Moved assignments into for loop.
		for (int i = 1; i < 6; i++)
		{
			Transform transform = this.Option[i].transform;
			transform.localPosition = new Vector3(
				-800.0f,
				transform.localPosition.y,
				transform.localPosition.z);
		}

		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < this.Objectives.Length; ID++)
		{
			this.Objectives[ID].localScale = Vector3.zero;
		}

		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < this.NemesisObjectives.Length; ID++)
		{
			this.NemesisObjectives[ID].localScale = Vector3.zero;
		}

		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < this.CustomObjectives.Length; ID++)
		{
			if (this.CustomObjectives[ID] != null)
			{
				this.CustomObjectives[ID].alpha = 0.50f;
			}
		}

		this.CustomPopulationLabel.text = "";
		this.PopulationLabel.text = "";

        /*
		if (!OptionGlobals.HighPopulation)
		{
			this.CustomPopulationLabel.text = "High School Population: Off";
			this.PopulationLabel.text = "High School Population: Off";
		}
		else
		{
			this.CustomPopulationLabel.text = "High School Population: On";
			this.PopulationLabel.text = "High School Population: On";
		}
		*/

        this.ChangeFont();
	}

	void Update()
	{
		if (this.Phase == 1)
		{
			this.Speed += Time.deltaTime;

			this.transform.position = new Vector3(
				this.transform.position.x,
				this.transform.position.y,
				Mathf.Lerp(this.transform.position.z, 2.0f, this.Speed * Time.deltaTime * 0.25f));

			this.Darkness.color = new Color(
				this.Darkness.color.r,
				this.Darkness.color.g,
				this.Darkness.color.b,
				Mathf.MoveTowards(this.Darkness.color.a, 0.0f, Time.deltaTime / 3.0f));

			if (this.Speed > 1.0f)
			{
				this.Header.color = new Color(
					this.Header.color.r,
					this.Header.color.g,
					this.Header.color.b,
					Mathf.MoveTowards(this.Header.color.a, 1.0f, Time.deltaTime));

				if (this.Speed > 3.0f)
				{
					if (!this.InfoSpoke[0])
					{
						this.MyAudio.PlayOneShot(this.InfoLines[0]);
						this.InfoSpoke[0] = true;
					}

					this.InfoChan.localEulerAngles = new Vector3(
						this.InfoChan.localEulerAngles.x,
						Mathf.Lerp(this.InfoChan.localEulerAngles.y, 180.0f, Time.deltaTime * (this.Speed - 3.0f)),
						this.InfoChan.localEulerAngles.z);

					Transform option1 = this.Option[1];
					option1.localPosition = new Vector3(
						Mathf.Lerp(option1.localPosition.x, 0.0f, Time.deltaTime * 10.0f),
						option1.localPosition.y,
						option1.localPosition.z);

					if (this.Speed > 3.25)
					{
						Transform option2 = this.Option[2];
						option2.localPosition = new Vector3(
							Mathf.Lerp(option2.localPosition.x, 0.0f, Time.deltaTime * 10.0f),
							option2.localPosition.y,
							option2.localPosition.z);

						if (this.Speed > 3.50f)
						{
							Transform option3 = this.Option[3];
							option3.localPosition = new Vector3(
								Mathf.Lerp(option3.localPosition.x, 0.0f, Time.deltaTime * 10.0f),
								option3.localPosition.y,
								option3.localPosition.z);

							if (this.Speed > 3.75)
							{
								Transform option4 = this.Option[4];
								option4.localPosition = new Vector3(
									Mathf.Lerp(option4.localPosition.x, 0.0f, Time.deltaTime * 10.0f),
									option4.localPosition.y,
									option4.localPosition.z);

								if (this.Speed > 4)
								{
									Transform option5 = this.Option[5];
									option5.localPosition = new Vector3(
										Mathf.Lerp(option5.localPosition.x, 0.0f, Time.deltaTime * 10.0f),
										option5.localPosition.y,
										option5.localPosition.z);

									if (this.Speed > 5.0f)
									{
										this.PromptBar.Label[0].text = "Accept";
										this.PromptBar.Label[4].text = "Choose";
										this.PromptBar.UpdateButtons();
										this.PromptBar.Show = true;

										this.Phase++;
									}
								}
							}
						}
					}
				}
			}

			if (Input.GetButtonDown(InputNames.Xbox_A))
			{
				if (!this.InfoSpoke[0])
				{
					this.MyAudio.PlayOneShot(this.InfoLines[0]);
					this.InfoSpoke[0] = true;
				}

				this.InfoChan.localEulerAngles = new Vector3(
					this.InfoChan.localEulerAngles.x,
					180.0f,
					this.InfoChan.localEulerAngles.z);

				this.transform.position = new Vector3(
					this.transform.position.x,
					this.transform.position.y,
					2.0f);

				this.Darkness.color = new Color(
					this.Darkness.color.r,
					this.Darkness.color.g,
					this.Darkness.color.b,
					0.0f);

				this.Header.color = new Color(
					this.Header.color.r,
					this.Header.color.g,
					this.Header.color.b,
					1.0f);

				this.Rotation = 0.0f;

				// [af] Moved assignments into for loop.
				for (int i = 1; i < 6; i++)
				{
					Transform option = this.Option[i];
					option.localPosition = new Vector3(
						0.0f,
						option.localPosition.y,
						option.localPosition.z);
				}

				this.PromptBar.Label[0].text = "Accept";
				this.PromptBar.Label[4].text = "Choose";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;

				this.Phase++;
			}
		}
		else if (this.Phase == 2)
		{
			this.InfoChan.localEulerAngles = new Vector3(
				this.InfoChan.localEulerAngles.x,
				Mathf.Lerp(this.InfoChan.localEulerAngles.y, 180.0f, Time.deltaTime * (this.Speed - 3.0f)),
				this.InfoChan.localEulerAngles.z);

			this.transform.position = new Vector3(
				this.transform.position.x,
				this.transform.position.y,
				Mathf.Lerp(this.transform.position.z, 2.0f, this.Speed * Time.deltaTime * 0.25f));

			this.CustomMissionWindow.localScale = Vector3.Lerp(
				this.CustomMissionWindow.localScale, Vector3.zero, Time.deltaTime * 10.0f);
			this.LoadMissionWindow.localScale = Vector3.Lerp(
				this.LoadMissionWindow.localScale, Vector3.zero, Time.deltaTime * 10.0f);
			this.MissionWindow.localScale = Vector3.Lerp(
				this.MissionWindow.localScale, Vector3.zero, Time.deltaTime * 10.0f);
			this.MultiMissionWindow.localScale = Vector3.Lerp(
				this.MultiMissionWindow.localScale, Vector3.zero, Time.deltaTime * 10.0f);

			this.Options.localPosition = new Vector3(
				Mathf.Lerp(this.Options.localPosition.x, -700.0f, Time.deltaTime * 10.0f),
				this.Options.localPosition.y,
				this.Options.localPosition.z);

			this.transform.position = new Vector3(
				this.transform.position.x,
				this.transform.position.y,
				Mathf.Lerp(this.transform.position.z, 2.0f, this.Speed * Time.deltaTime * 0.25f));

			if (this.InputManager.TappedUp)
			{
				this.Selected--;
				this.UpdateHighlight();
			}

			if (this.InputManager.TappedDown)
			{
				this.Selected++;
				this.UpdateHighlight();
			}

			this.Highlight.transform.localPosition = new Vector3(
				this.Highlight.transform.localPosition.x,
				Mathf.Lerp(this.Highlight.transform.localPosition.y, 150.0f - (50.0f * this.Selected), Time.deltaTime * 10.0f),
				this.Highlight.transform.localPosition.z);

			this.Highlight.color = new Color(
				this.Highlight.color.r,
				this.Highlight.color.g,
				this.Highlight.color.b,
				Mathf.MoveTowards(this.Highlight.color.a, 1.0f, Time.deltaTime));

			// [af] Converted while loop to for loop.
			for (int ID = 1; ID < 6; ID++)
			{
				if (ID != this.Selected)
				{
					Transform option = this.Option[ID];
					option.localPosition = new Vector3(
						Mathf.Lerp(option.transform.localPosition.x, 0.0f, Time.deltaTime * 10.0f),
						option.localPosition.y,
						option.localPosition.z);
				}
			}

			Transform selectedOption = this.Option[this.Selected];
			selectedOption.localPosition = new Vector3(
				Mathf.Lerp(selectedOption.transform.localPosition.x, 50.0f, Time.deltaTime * 10.0f),
				selectedOption.localPosition.y,
				selectedOption.localPosition.z);

			if (Input.GetButtonDown(InputNames.Xbox_A))
			{
				if (!this.InfoSpoke[this.Selected])
				{
                    this.MyAudio.PlayOneShot(this.InfoLines[this.Selected]);
					this.InfoSpoke[this.Selected] = true;
				}

				//////////////////////////
				///// RANDOM MISSION /////
				//////////////////////////

				if (this.Selected == 1)
				{
					this.PromptBar.ClearButtons();
					this.PromptBar.Label[0].text = "Accept";
					this.PromptBar.Label[1].text = "Return";
					this.PromptBar.Label[2].text = "Generate";
					this.PromptBar.Label[3].text = "";
					this.PromptBar.Label[4].text = "Nemesis";
					this.PromptBar.Label[5].text = "Change Difficulty";
					this.PromptBar.UpdateButtons();

					// [af] Converted while loop to for loop.
					for (int ID = 1; ID < this.Conditions.Length; ID++)
					{
						this.Conditions[ID] = 0;
					}

					if (this.TargetID == 0)
					{
						this.ChooseTarget();
					}

					this.RequiredClothingID = 0;
					this.RequiredDisposalID = 0;
					this.RequiredWeaponID = 0;

					this.NemesisDifficulty = 0;
					this.Difficulty = 1;

					this.UpdateNemesisDifficulty();
					this.UpdateDifficultyLabel();

					this.Phase++;
				}

				//////////////////////////
				///// CUSTOM MISSION /////
				//////////////////////////

				else if (this.Selected == 2)
				{
					this.Difficulty = 1;
					this.Phase = 5;

					this.PromptBar.ClearButtons();
					this.PromptBar.Label[0].text = "Toggle";
					this.PromptBar.Label[1].text = "Return";
					this.PromptBar.Label[2].text = "Change";
					this.PromptBar.Label[3].text = "";
					this.PromptBar.Label[4].text = "Selection";
					this.PromptBar.Label[5].text = "Selection";
					this.PromptBar.UpdateButtons();

					this.CustomDescs[2].text = this.ConditionDescs[1] + " " + this.WeaponNames[1];
					this.CustomDescs[3].text = this.ConditionDescs[2] + " " + this.ClothingNames[1];
					this.CustomDescs[4].text = this.ConditionDescs[3] + " " + this.DisposalNames[1];

					this.UpdateObjectiveHighlight();
					this.UpdateDifficultyLabel();

					this.RequiredClothingID = 1;
					this.RequiredDisposalID = 1;
					this.RequiredWeaponID = 1;

					this.TargetID = 2;
					this.ChooseTarget();

					this.CalculateMissionID();
				}

				/////////////////////////
				///// MULTI MISSION /////
				/////////////////////////

				else if (this.Selected == 3)
				{
					this.PromptBar.ClearButtons();
					this.PromptBar.Label[0].text = "";
					this.PromptBar.Label[1].text = "Return";
					this.PromptBar.Label[2].text = "Adjust Up";
					this.PromptBar.Label[3].text = "Adjust Down";
					this.PromptBar.Label[4].text = "Selection";
					this.PromptBar.Label[5].text = "Selection";
					this.PromptBar.UpdateButtons();

					this.MultiMission.enabled = true;

					this.MultiMission.Column = 1;
					this.MultiMission.Row = 1;
					this.MultiMission.UpdateHighlight();

					this.Phase = 6;
				}

				////////////////////////
				///// LOAD MISSION /////
				////////////////////////

				else if (this.Selected == 4)
				{
					Cursor.visible = true;

					this.PromptBar.ClearButtons();
					this.PromptBar.Label[0].text = "Confirm";
					this.PromptBar.Label[1].text = "Back";
					this.PromptBar.UpdateButtons();

					this.Phase = 7;
				}

				else if (this.Selected == 5)
				{
					this.PromptBar.Show = false;
					this.Phase = 4;
					this.Speed = 0.0f;
				}
			}
		}

		//////////////////////////
		///// RANDOM MISSION /////
		//////////////////////////

		else if (this.Phase == 3)
		{
			this.InfoChan.localEulerAngles = new Vector3(
				this.InfoChan.localEulerAngles.x,
				Mathf.Lerp(this.InfoChan.localEulerAngles.y, 180.0f, Time.deltaTime * (this.Speed - 3.0f)),
				this.InfoChan.localEulerAngles.z);

			this.transform.position = new Vector3(
				this.transform.position.x,
				this.transform.position.y,
				Mathf.Lerp(this.transform.position.z, 2.0f, this.Speed * Time.deltaTime * 0.25f));

			this.CustomMissionWindow.localScale = Vector3.Lerp(
				this.CustomMissionWindow.localScale, Vector3.zero, Time.deltaTime * 10.0f);
			this.LoadMissionWindow.localScale = Vector3.Lerp(
				this.LoadMissionWindow.localScale, Vector3.zero, Time.deltaTime * 10.0f);
			this.MissionWindow.localScale = Vector3.Lerp(
				this.MissionWindow.localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
			this.MultiMissionWindow.localScale = Vector3.Lerp(
				this.MultiMissionWindow.localScale, Vector3.zero, Time.deltaTime * 10.0f);

			this.Options.localPosition = new Vector3(
				Mathf.Lerp(this.Options.localPosition.x, -1550.0f, Time.deltaTime * 10.0f),
				this.Options.localPosition.y,
				this.Options.localPosition.z);

			if (this.InputManager.TappedLeft)
			{
				this.Difficulty--;
				this.UpdateDifficulty();
			}

			if (this.InputManager.TappedRight)
			{
				this.Difficulty++;
				this.UpdateDifficulty();
			}

			if (this.InputManager.TappedUp)
			{
				this.NemesisDifficulty--;
				this.UpdateNemesisDifficulty();
			}

			if (this.InputManager.TappedDown)
			{
				this.NemesisDifficulty++;
				this.UpdateNemesisDifficulty();
			}

			// [af] Converted while loop to for loop.
			for (int ID = 1; ID < this.Objectives.Length; ID++)
			{
				Transform objective = this.Objectives[ID];

				// [af] Replaced if/else statement with assignment and ternary expression.
				objective.localScale = Vector3.Lerp(
					objective.localScale,
					(ID > this.Difficulty) ? Vector3.zero : Vector3.one,
					Time.deltaTime * 10.0f);
			}

			if (this.NemesisDifficulty == 0)
			{
				this.NemesisPortrait.transform.parent.localScale = Vector3.Lerp(
					this.NemesisPortrait.transform.parent.localScale, Vector3.zero, Time.deltaTime * 10.0f);
				this.NemesisObjectives[1].localScale = Vector3.Lerp(
					this.NemesisObjectives[1].localScale, Vector3.zero, Time.deltaTime * 10.0f);
				this.NemesisObjectives[2].localScale = Vector3.Lerp(
					this.NemesisObjectives[2].localScale, Vector3.zero, Time.deltaTime * 10.0f);
				this.NemesisObjectives[3].localScale = Vector3.Lerp(
					this.NemesisObjectives[3].localScale, Vector3.zero, Time.deltaTime * 10.0f);
			}
			else
			{
				this.NemesisPortrait.transform.parent.localScale = Vector3.Lerp(
					this.NemesisPortrait.transform.parent.localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
			}

			if (this.NemesisDifficulty == 1)
			{
				this.NemesisObjectives[1].localScale = Vector3.Lerp(
					this.NemesisObjectives[1].localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
				this.NemesisObjectives[2].localScale = Vector3.Lerp(
					this.NemesisObjectives[2].localScale, Vector3.zero, Time.deltaTime * 10.0f);
				this.NemesisObjectives[3].localScale = Vector3.Lerp(
					this.NemesisObjectives[3].localScale, Vector3.zero, Time.deltaTime * 10.0f);
			}
			else if (this.NemesisDifficulty == 2)
			{
				this.NemesisObjectives[1].localScale = Vector3.Lerp(
					this.NemesisObjectives[1].localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
				this.NemesisObjectives[2].localScale = Vector3.Lerp(
					this.NemesisObjectives[2].localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
				this.NemesisObjectives[3].localScale = Vector3.Lerp(
					this.NemesisObjectives[3].localScale, Vector3.zero, Time.deltaTime * 10.0f);
			}
			else if (this.NemesisDifficulty == 3)
			{
				this.NemesisObjectives[1].localScale = Vector3.Lerp(
					this.NemesisObjectives[1].localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
				this.NemesisObjectives[2].localScale = Vector3.Lerp(
					this.NemesisObjectives[2].localScale, Vector3.zero, Time.deltaTime * 10.0f);
				this.NemesisObjectives[3].localScale = Vector3.Lerp(
					this.NemesisObjectives[3].localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
			}
			else if (this.NemesisDifficulty == 4)
			{
				this.NemesisObjectives[1].localScale = Vector3.Lerp(
					this.NemesisObjectives[1].localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
				this.NemesisObjectives[2].localScale = Vector3.Lerp(
					this.NemesisObjectives[2].localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
				this.NemesisObjectives[3].localScale = Vector3.Lerp(
					this.NemesisObjectives[3].localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
			}

			if (Input.GetButtonDown(InputNames.Xbox_A))
			{
				this.StartMission();
			}
			else if (Input.GetButtonDown(InputNames.Xbox_B))
			{
				Cursor.visible = false;

				this.PromptBar.ClearButtons();
				this.PromptBar.Label[0].text = "Accept";
				this.PromptBar.Label[4].text = "Choose";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;

				this.TargetID = 0;
				this.Phase--;
			}
			else if (Input.GetButtonDown(InputNames.Xbox_X))
			{
				this.RequiredClothingID = 0;
				this.RequiredDisposalID = 0;
				this.RequiredWeaponID = 0;

				this.ChooseTarget();

                if (this.Difficulty > 1)
				{
					var OriginalDifficulty = this.Difficulty;
					this.Difficulty = 1;

					while (this.Difficulty < OriginalDifficulty)
					{
						this.Difficulty++;
						this.PickNewCondition();
					}
				}

                this.UpdateDifficultyLabel();
            }
			else if (Input.GetButtonDown(InputNames.Xbox_Y))
			{
				this.UpdatePopulation();
			}
		}

		////////////////
		///// EXIT /////
		////////////////

		else if (this.Phase == 4)
		{
			this.Speed += Time.deltaTime;

			this.CustomMissionWindow.localScale = Vector3.Lerp(
				this.CustomMissionWindow.localScale, Vector3.zero, Time.deltaTime * 10.0f);
			this.LoadMissionWindow.localScale = Vector3.Lerp(
				this.LoadMissionWindow.localScale, Vector3.zero, Time.deltaTime * 10.0f);
			this.MissionWindow.localScale = Vector3.Lerp(
				this.MissionWindow.localScale, Vector3.zero, Time.deltaTime * 10.0f);
			this.MultiMissionWindow.localScale = Vector3.Lerp(
				this.MultiMissionWindow.localScale, Vector3.zero, Time.deltaTime * 10.0f);

			this.InfoChan.localEulerAngles = new Vector3(
				this.InfoChan.localEulerAngles.x,
				Mathf.Lerp(this.InfoChan.localEulerAngles.y, 0.0f, Time.deltaTime * this.Speed),
				this.InfoChan.localEulerAngles.z);

			this.Darkness.color = new Color(
				this.Darkness.color.r,
				this.Darkness.color.g,
				this.Darkness.color.b,
				Mathf.MoveTowards(this.Darkness.color.a, 1.0f, Time.deltaTime * 0.50f));

			Transform option1Parent = this.Option[1].parent;
			option1Parent.localPosition = new Vector3(
				option1Parent.localPosition.x - (this.Speed * 1000.0f * Time.deltaTime),
				option1Parent.localPosition.y,
				option1Parent.localPosition.z);

			this.transform.position = new Vector3(
				this.transform.position.x,
				this.transform.position.y,
				this.transform.position.z - (this.Speed * Time.deltaTime));

			this.Jukebox.volume -= Time.deltaTime;

			this.Header.color = new Color(
				this.Header.color.r,
				this.Header.color.g,
				this.Header.color.b,
				this.Header.color.a - Time.deltaTime);

			if (this.Darkness.color.a == 1.0f)
			{
				if (this.TargetID == 0 && !MissionModeGlobals.MultiMission)
				{
					SceneManager.LoadScene(SceneNames.TitleScene);
				}
				else
				{
					this.NowLoading.SetActive(true);
					SceneManager.LoadScene(SceneNames.SchoolScene);
				}
			}
		}

		//////////////////////////
		///// CUSTOM MISSION /////
		//////////////////////////

		else if (this.Phase == 5)
		{
			this.InfoChan.localEulerAngles = new Vector3(
				this.InfoChan.localEulerAngles.x,
				Mathf.Lerp(this.InfoChan.localEulerAngles.y, 180.0f, Time.deltaTime * (this.Speed - 3.0f)),
				this.InfoChan.localEulerAngles.z);

			this.transform.position = new Vector3(
				this.transform.position.x,
				this.transform.position.y,
				Mathf.Lerp(this.transform.position.z, 2.0f, this.Speed * Time.deltaTime * 0.25f));

			this.CustomMissionWindow.localScale = Vector3.Lerp(
				this.CustomMissionWindow.localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
			this.LoadMissionWindow.localScale = Vector3.Lerp(
				this.LoadMissionWindow.localScale, Vector3.zero, Time.deltaTime * 10.0f);
			this.MissionWindow.localScale = Vector3.Lerp(
				this.MissionWindow.localScale, Vector3.zero, Time.deltaTime * 10.0f);
			this.MultiMissionWindow.localScale = Vector3.Lerp(
				this.MultiMissionWindow.localScale, Vector3.zero, Time.deltaTime * 10.0f);

			this.Options.localPosition = new Vector3(
				Mathf.Lerp(this.Options.localPosition.x, -1550.0f, Time.deltaTime * 10.0f),
				this.Options.localPosition.y,
				this.Options.localPosition.z);

			if (this.InputManager.TappedUp)
			{
				this.Row--;
				this.UpdateObjectiveHighlight();
			}

			if (this.InputManager.TappedDown)
			{
				this.Row++;
				this.UpdateObjectiveHighlight();
			}

			if (this.InputManager.TappedRight)
			{
				this.Column++;
				this.UpdateObjectiveHighlight();
			}

			if (this.InputManager.TappedLeft)
			{
				this.Column--;
				this.UpdateObjectiveHighlight();
			}

			if (Input.GetButtonDown(InputNames.Xbox_A))
			{
				if (this.CustomSelected == 1)
				{
					this.TargetID++;
					this.ChooseTarget();
				}
				else if (this.CustomSelected == 6)
				{
					// [af] Converted while loop to for loop.
					for (int ID = 1; ID < this.Conditions.Length; ID++)
					{
						this.Conditions[ID] = 0;
					}

					int ConditionsSet = 2;

					// [af] Converted while loop to for loop.
					for (int ID = 2; ID < this.CustomObjectives.Length; ID++)
					{
						if (this.CustomObjectives[ID] != null)
						{
							if (this.CustomObjectives[ID].alpha == 1.0f)
							{
								if (ID < 6)
								{
									this.Conditions[ConditionsSet] = ID - 1;
								}
								else if (ID < 12)
								{
									this.Conditions[ConditionsSet] = ID - 2;
								}
								else
								{
									this.Conditions[ConditionsSet] = ID - 3;
								}

								ConditionsSet++;
							}
						}
					}

					this.StartMission();
				}
				else if (this.CustomSelected == 12)
				{
					this.NemesisDifficulty++;
					this.UpdateNemesisDifficulty();
				}

				if (this.PromptBar.Label[0].text == "Toggle")
				{
					if (this.CustomObjectives[this.CustomSelected].alpha == 0.50f)
					{
						if (this.Difficulty < 10)
						{
							this.Difficulty++;
							this.UpdateDifficultyLabel();
							this.CustomObjectives[this.CustomSelected].alpha = 1.0f;
						}
					}
					else
					{
						this.Difficulty--;
						this.UpdateDifficultyLabel();
						this.CustomObjectives[this.CustomSelected].alpha = 0.50f;
					}
				}

				this.CalculateMissionID();
			}
			else if (Input.GetButtonDown(InputNames.Xbox_B))
			{
				Cursor.visible = false;

				this.PromptBar.ClearButtons();
				this.PromptBar.Label[0].text = "Accept";
				this.PromptBar.Label[4].text = "Choose";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;

				// [af] Converted while loop to for loop.
				for (int ID = 1; ID < this.CustomObjectives.Length; ID++)
				{
					if (this.CustomObjectives[ID] != null)
					{
						this.CustomObjectives[ID].alpha = 0.50f;
					}
				}

				this.NemesisDifficulty = 0;
				this.UpdateNemesisDifficulty();

				this.Difficulty = 1;
				this.TargetID = 0;
				this.Phase = 2;
			}
			else if (Input.GetButtonDown(InputNames.Xbox_X))
			{
				if (this.CustomSelected == 1)
				{
					this.TargetID--;
					this.ChooseTarget();
					this.CalculateMissionID();
				}
				else if (this.CustomSelected == 2)
				{
					this.RequiredWeaponID++;

					if (this.RequiredWeaponID == 11)
					{
						this.RequiredWeaponID++;
					}

					if (this.RequiredWeaponID > (this.WeaponNames.Length - 1))
					{
						this.RequiredWeaponID = 1;
					}

					this.CustomDescs[2].text = this.ConditionDescs[1] + " " +
						this.WeaponNames[this.RequiredWeaponID];
				}
				else if (this.CustomSelected == 3)
				{
					this.RequiredClothingID++;

					if (this.RequiredClothingID > (this.ClothingNames.Length - 1))
					{
						this.RequiredClothingID = 1;
					}

					this.CustomDescs[3].text = this.ConditionDescs[2] + " " +
						this.ClothingNames[this.RequiredClothingID];
				}
				else if (this.CustomSelected == 4)
				{
					this.RequiredDisposalID++;

					if (this.RequiredDisposalID > (this.DisposalNames.Length - 1))
					{
						this.RequiredDisposalID = 1;
					}

					this.CustomDescs[4].text = this.ConditionDescs[3] + " " +
						this.DisposalNames[this.RequiredDisposalID];
				}
				else if (this.CustomSelected == 12)
				{
					this.NemesisDifficulty--;
					this.UpdateNemesisDifficulty();
				}

				this.CalculateMissionID();
			}
			else if (Input.GetButtonDown(InputNames.Xbox_Y))
			{
				this.UpdatePopulation();
				this.CalculateMissionID();
			}

			if (this.NemesisDifficulty == 0)
			{
				this.CustomNemesisObjectives[1].localScale = Vector3.Lerp(
					this.CustomNemesisObjectives[1].localScale, Vector3.zero, Time.deltaTime * 10.0f);
				this.CustomNemesisObjectives[2].localScale = Vector3.Lerp(
					this.CustomNemesisObjectives[2].localScale, Vector3.zero, Time.deltaTime * 10.0f);
				this.CustomNemesisObjectives[3].localScale = Vector3.Lerp(
					this.CustomNemesisObjectives[3].localScale, Vector3.zero, Time.deltaTime * 10.0f);
			}
			else if (this.NemesisDifficulty == 1)
			{
				this.CustomNemesisObjectives[1].localScale = Vector3.Lerp(
					this.CustomNemesisObjectives[1].localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
				this.CustomNemesisObjectives[2].localScale = Vector3.Lerp(
					this.CustomNemesisObjectives[2].localScale, Vector3.zero, Time.deltaTime * 10.0f);
				this.CustomNemesisObjectives[3].localScale = Vector3.Lerp(
					this.CustomNemesisObjectives[3].localScale, Vector3.zero, Time.deltaTime * 10.0f);
			}
			else if (this.NemesisDifficulty == 2)
			{
				this.CustomNemesisObjectives[1].localScale = Vector3.Lerp(
					this.CustomNemesisObjectives[1].localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
				this.CustomNemesisObjectives[2].localScale = Vector3.Lerp(
					this.CustomNemesisObjectives[2].localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
				this.CustomNemesisObjectives[3].localScale = Vector3.Lerp(
					this.CustomNemesisObjectives[3].localScale, Vector3.zero, Time.deltaTime * 10.0f);
			}
			else if (this.NemesisDifficulty == 3)
			{
				this.CustomNemesisObjectives[1].localScale = Vector3.Lerp(
					this.CustomNemesisObjectives[1].localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
				this.CustomNemesisObjectives[2].localScale = Vector3.Lerp(
					this.CustomNemesisObjectives[2].localScale, Vector3.zero, Time.deltaTime * 10.0f);
				this.CustomNemesisObjectives[3].localScale = Vector3.Lerp(
					this.CustomNemesisObjectives[3].localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
			}
			else if (this.NemesisDifficulty == 4)
			{
				this.CustomNemesisObjectives[1].localScale = Vector3.Lerp(
					this.CustomNemesisObjectives[1].localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
				this.CustomNemesisObjectives[2].localScale = Vector3.Lerp(
					this.CustomNemesisObjectives[2].localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
				this.CustomNemesisObjectives[3].localScale = Vector3.Lerp(
					this.CustomNemesisObjectives[3].localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
			}

			this.MissionIDLabel.gameObject.GetComponent<UIInput>().value = this.MissionID;
		}

		/////////////////////////
		///// MULTI MISSION /////
		/////////////////////////

		else if (this.Phase == 6)
		{
			this.CustomMissionWindow.localScale = Vector3.Lerp(
				this.CustomMissionWindow.localScale, Vector3.zero, Time.deltaTime * 10.0f);
			this.LoadMissionWindow.localScale = Vector3.Lerp(
				this.LoadMissionWindow.localScale, Vector3.zero, Time.deltaTime * 10.0f);
			this.MissionWindow.localScale = Vector3.Lerp(
				this.MissionWindow.localScale, Vector3.zero, Time.deltaTime * 10.0f);
			this.MultiMissionWindow.localScale = Vector3.Lerp(
				this.MultiMissionWindow.localScale, new Vector3(.9f, .9f, .9f), Time.deltaTime * 10.0f);

			this.Options.localPosition = new Vector3(
				Mathf.Lerp(this.Options.localPosition.x, -1550.0f, Time.deltaTime * 10.0f),
				this.Options.localPosition.y,
				this.Options.localPosition.z);
		}

		////////////////////////
		///// LOAD MISSION /////
		////////////////////////

		else if (this.Phase == 7)
		{
			this.InfoChan.localEulerAngles = new Vector3(
				this.InfoChan.localEulerAngles.x,
				Mathf.Lerp(this.InfoChan.localEulerAngles.y, 180.0f, Time.deltaTime * (this.Speed - 3.0f)),
				this.InfoChan.localEulerAngles.z);

			this.transform.position = new Vector3(
				this.transform.position.x,
				this.transform.position.y,
				Mathf.Lerp(this.transform.position.z, 2.0f, this.Speed * Time.deltaTime * 0.25f));

			this.CustomMissionWindow.localScale = Vector3.Lerp(
				this.CustomMissionWindow.localScale, Vector3.zero, Time.deltaTime * 10.0f);
			this.LoadMissionWindow.localScale = Vector3.Lerp(
				this.LoadMissionWindow.localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
			this.MissionWindow.localScale = Vector3.Lerp(
				this.MissionWindow.localScale, Vector3.zero, Time.deltaTime * 10.0f);
			this.MultiMissionWindow.localScale = Vector3.Lerp(
				this.MultiMissionWindow.localScale, Vector3.zero, Time.deltaTime * 10.0f);

			this.Options.localPosition = new Vector3(
				Mathf.Lerp(this.Options.localPosition.x, -1550.0f, Time.deltaTime * 10.0f),
				this.Options.localPosition.y,
				this.Options.localPosition.z);

			if (!Input.anyKey)
			{
				this.MissionIDString = this.LoadMissionLabel.text;

				if (this.MissionIDString.Length < 19)
				{
					this.ErrorLabel.text = "A Mission ID must be 19 numbers long.";
				}
				else
				{
					if (this.MissionIDString[0] != '-')
					{
						this.GetNumbers();

						bool Invalid = false;

						if (this.TargetNumber > 9 && this.TargetNumber < 21 ||
							this.TargetNumber > 97)
						{
							Invalid = true;
						}

						if (this.TargetNumber == 0)
						{
							this.ErrorLabel.text = "Invalid Mission ID (No target specified)";
						}
						else if (this.TargetNumber == 1)
						{
							this.ErrorLabel.text = "Invalid Mission ID (Target cannot be Senpai)";
						}
						else if (Invalid)
						{
							this.ErrorLabel.text = "Invalid Mission ID (That student has not been implemented yet)";
						}
						else if (this.WeaponNumber == 11)
						{
							this.ErrorLabel.text = "Invalid Mission ID (Weapon does not apply to Mission Mode)";
						}
						else if (this.WeaponNumber > 14)
						{
							this.ErrorLabel.text = "Invalid Mission ID (Weapon does not exist)";
						}
						else if (this.ClothingNumber > 5)
						{
							this.ErrorLabel.text = "Invalid Mission ID (Clothing does not exist)";
						}
						else if (this.DisposalNumber > 3)
						{
							this.ErrorLabel.text = "Invalid Mission ID (Disposal method does not exist)";
						}
						else if (this.NemesisNumber > 4)
						{
							this.ErrorLabel.text = "Invalid Mission ID (Nemesis level too high)";
						}
						else if (this.PopulationNumber > 0)
						{
							this.ErrorLabel.text = "Invalid Mission ID (Final digit must be '0')";
						}
						else if ((this.Condition5Number > 1) ||
								 (this.Condition6Number > 1) ||
								 (this.Condition7Number > 1) ||
								 (this.Condition8Number > 1) ||
								 (this.Condition9Number > 1) ||
								 (this.Condition10Number > 1) ||
								 (this.Condition11Number > 1) ||
								 (this.Condition12Number > 1) ||
								 (this.Condition13Number > 1) ||
								 (this.Condition14Number > 1) ||
								 (this.Condition15Number > 1))
						{
							this.ErrorLabel.text = "Invalid Mission ID (One of those conditions should be 1 or 0)";
						}
						else
						{
							this.ErrorLabel.text = "Valid Mission ID!";
						}
					}
					else
					{
						this.ErrorLabel.text = "Invalid Mission ID (Cannot be negative number)";
					}
				}
			}
			else if (Input.GetButtonDown(InputNames.Xbox_A))
			{
				if (this.ErrorLabel.text == "Valid Mission ID!")
				{
					Debug.Log("Target ID is: " + this.TargetNumber.ToString() +
						" and Weapon ID is: " + this.WeaponNumber.ToString());

					this.TargetID = this.TargetNumber;

					this.Difficulty = 1;

					if (this.WeaponNumber > 0)
					{
						this.Difficulty++;
						this.Conditions[this.Difficulty] = 2;
						this.CustomObjectives[2].alpha = 1.0f;
						this.RequiredWeaponID = this.WeaponNumber;
						this.CustomDescs[2].text = this.ConditionDescs[1] + " " +
							this.WeaponNames[this.RequiredWeaponID];
					}
					else
					{
						this.CustomObjectives[2].alpha = 0.50f;
						this.CustomDescs[2].text = this.ConditionDescs[1] + " " +
							this.WeaponNames[1];
					}

					if (this.ClothingNumber > 0)
					{
						this.Difficulty++;
						this.Conditions[this.Difficulty] = 3;
						this.CustomObjectives[3].alpha = 1.0f;
						this.RequiredClothingID = this.ClothingNumber;
						this.CustomDescs[3].text = this.ConditionDescs[2] + " " +
							this.ClothingNames[this.RequiredClothingID];
					}
					else
					{
						this.CustomObjectives[3].alpha = 0.50f;
						this.CustomDescs[3].text = this.ConditionDescs[2] + " " +
							this.ClothingNames[1];
					}

					if (this.DisposalNumber > 0)
					{
						this.Difficulty++;
						this.Conditions[this.Difficulty] = 4;
						this.CustomObjectives[4].alpha = 1.0f;
						this.RequiredDisposalID = this.DisposalNumber;
						this.CustomDescs[4].text = this.ConditionDescs[3] + " " +
							this.DisposalNames[this.RequiredDisposalID];
					}
					else
					{
						this.CustomObjectives[4].alpha = 0.50f;
						this.CustomDescs[4].text = this.ConditionDescs[3] + " " +
							this.DisposalNames[1];
					}

					if ((this.Difficulty < 10) && (this.Condition5Number == 1))
					{
						this.Difficulty++;
						this.Conditions[this.Difficulty] = 5;
						this.CustomObjectives[5].alpha = 1.0f;
					}

					if ((this.Difficulty < 10) && (this.Condition6Number == 1))
					{
						this.Difficulty++;
						this.Conditions[this.Difficulty] = 6;
						this.CustomObjectives[7].alpha = 1.0f;
					}

					if ((this.Difficulty < 10) && (this.Condition7Number == 1))
					{
						this.Difficulty++;
						this.Conditions[this.Difficulty] = 7;
						this.CustomObjectives[8].alpha = 1.0f;
					}

					if ((this.Difficulty < 10) && (this.Condition8Number == 1))
					{
						this.Difficulty++;
						this.Conditions[this.Difficulty] = 8;
						this.CustomObjectives[9].alpha = 1.0f;
					}

					if ((this.Difficulty < 10) && (this.Condition9Number == 1))
					{
						this.Difficulty++;
						this.Conditions[this.Difficulty] = 9;
						this.CustomObjectives[10].alpha = 1.0f;
					}

					if ((this.Difficulty < 10) && (this.Condition10Number == 1))
					{
						this.Difficulty++;
						this.Conditions[this.Difficulty] = 10;
						this.CustomObjectives[11].alpha = 1.0f;
					}

					if ((this.Difficulty < 10) && (this.Condition11Number == 1))
					{
						this.Difficulty++;
						this.Conditions[this.Difficulty] = 11;
						this.CustomObjectives[13].alpha = 1.0f;
					}

					if ((this.Difficulty < 10) && (this.Condition12Number == 1))
					{
						this.Difficulty++;
						this.Conditions[this.Difficulty] = 12;
						this.CustomObjectives[14].alpha = 1.0f;
					}

					if ((this.Difficulty < 10) && (this.Condition13Number == 1))
					{
						this.Difficulty++;
						this.Conditions[this.Difficulty] = 13;
						this.CustomObjectives[15].alpha = 1.0f;
					}

					if ((this.Difficulty < 10) && (this.Condition14Number == 1))
					{
						this.Difficulty++;
						this.Conditions[this.Difficulty] = 14;
						this.CustomObjectives[16].alpha = 1.0f;
					}

					if ((this.Difficulty < 10) && (this.Condition15Number == 1))
					{
						this.Difficulty++;
						this.Conditions[this.Difficulty] = 15;
						this.CustomObjectives[17].alpha = 1.0f;
					}

					this.NemesisDifficulty = this.NemesisNumber;
					SchoolGlobals.Population = this.PopulationNumber;

					this.Phase = 5;

					this.PromptBar.ClearButtons();
					this.PromptBar.Label[0].text = "Toggle";
					this.PromptBar.Label[1].text = "Return";
					this.PromptBar.Label[2].text = "Change";
					this.PromptBar.Label[3].text = "";
					this.PromptBar.Label[4].text = "Selection";
					this.PromptBar.Label[5].text = "Selection";
					this.PromptBar.UpdateButtons();

					this.UpdateObjectiveHighlight();
					this.UpdateNemesisDifficulty();
					this.UpdateDifficultyLabel();
					this.CalculateMissionID();
					this.ChooseTarget();
				}
			}
			else if (Input.GetButtonDown(InputNames.Xbox_B))
			{
				Cursor.visible = false;

				this.PromptBar.ClearButtons();
				this.PromptBar.Label[0].text = "Accept";
				this.PromptBar.Label[4].text = "Choose";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;

				this.TargetID = 0;
				this.Phase = 2;
			}
		}

		// [af] Commented in JS code.
		/*
		if (Input.GetKeyDown("m"))
		{
			this.Difficulty = 3;
			this.TargetID = 32;
			
			this.Conditions[2] = 1;
			this.Conditions[3] = 2;
			
			this.RequiredClothingID = 2;
			this.RequiredWeaponID = 4;
		}
		*/
	}

	public int TargetNumber = 0;
	public int WeaponNumber = 0;
	public int ClothingNumber = 0;
	public int DisposalNumber = 0;
	public int NemesisNumber = 0;
	public int PopulationNumber = 0;

	public int Condition5Number = 0;
	public int Condition6Number = 0;
	public int Condition7Number = 0;
	public int Condition8Number = 0;
	public int Condition9Number = 0;
	public int Condition10Number = 0;
	public int Condition11Number = 0;
	public int Condition12Number = 0;
	public int Condition13Number = 0;
	public int Condition14Number = 0;
	public int Condition15Number = 0;

	void GetNumbers()
	{
		this.TargetNumber = ((int)char.GetNumericValue(this.MissionIDString[0]) * 10) +
			(int)char.GetNumericValue(this.MissionIDString[1]);
		this.WeaponNumber = ((int)char.GetNumericValue(this.MissionIDString[2]) * 10) +
			(int)char.GetNumericValue(this.MissionIDString[3]);
		this.ClothingNumber = (int)char.GetNumericValue(this.MissionIDString[4]);
		this.DisposalNumber = (int)char.GetNumericValue(this.MissionIDString[5]);

		this.Condition5Number = (int)char.GetNumericValue(this.MissionIDString[6]);
		this.Condition6Number = (int)char.GetNumericValue(this.MissionIDString[7]);
		this.Condition7Number = (int)char.GetNumericValue(this.MissionIDString[8]);
		this.Condition8Number = (int)char.GetNumericValue(this.MissionIDString[9]);
		this.Condition9Number = (int)char.GetNumericValue(this.MissionIDString[10]);
		this.Condition10Number = (int)char.GetNumericValue(this.MissionIDString[11]);
		this.Condition11Number = (int)char.GetNumericValue(this.MissionIDString[12]);
		this.Condition12Number = (int)char.GetNumericValue(this.MissionIDString[13]);
		this.Condition13Number = (int)char.GetNumericValue(this.MissionIDString[14]);
		this.Condition14Number = (int)char.GetNumericValue(this.MissionIDString[15]);
		this.Condition15Number = (int)char.GetNumericValue(this.MissionIDString[16]);

		this.NemesisNumber = (int)char.GetNumericValue(this.MissionIDString[17]);
		this.PopulationNumber = (int)char.GetNumericValue(this.MissionIDString[18]);
	}

	void LateUpdate()
	{
		if (this.Speed > 3.0f)
		{
			this.Rotation = Mathf.Lerp(this.Rotation, 0.0f, Time.deltaTime * (this.Speed - 3.0f));
		}

		this.Neck.transform.localEulerAngles = new Vector3(
			this.Neck.transform.localEulerAngles.x + this.Rotation,
			this.Neck.transform.localEulerAngles.y,
			this.Neck.transform.localEulerAngles.z);
	}

	void UpdateHighlight()
	{
		if (this.Selected == 0)
		{
			this.Selected = 5;
		}
		else if (this.Selected == 6)
		{
			this.Selected = 1;
		}
	}

	void ChooseTarget()
	{
        Debug.Log("Calling the ChooseTarget() function.");

		if (this.Phase != 5)
		{
			// [af] Replaced if/else statement with ternary expression.
			this.TargetID = Random.Range(2, 90);

			if (this.TargetNumber > 9 && this.TargetNumber < 21)
			{
				this.ChooseTarget();
			}
		}
		else
		{
			//if (!OptionGlobals.HighPopulation)
			//{
				if (this.TargetNumber > 9 && this.TargetNumber < 21)
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.TargetID++;
					}
					else
					{
						this.TargetID--;
					}

					this.ChooseTarget();
				}
			//}

			if (this.TargetID > 89)
			{
				this.TargetID = 2;
			}
			else if (this.TargetID < 2)
			{
				this.TargetID = 89;
			}
		}

		string path = "file:///" + Application.streamingAssetsPath +
			"/Portraits/Student_" + this.TargetID + ".png";

		WWW www = new WWW(path);

		if (this.TargetNumber > 9 && this.TargetNumber < 21)
		{
			this.TargetPortrait.mainTexture = this.BlankPortrait;
		}
		else
		{
			this.TargetPortrait.mainTexture = www.texture;
		}

		this.CustomTargetPortrait.mainTexture = this.TargetPortrait.mainTexture;

		if ((this.JSON.Students[this.TargetID].Name == "Random") ||
			(this.JSON.Students[this.TargetID].Name == "Unknown"))
		{
			this.TargetName = this.StudentManager.FirstNames[Random.Range(0, this.StudentManager.FirstNames.Length)] + " " +
				this.StudentManager.LastNames[Random.Range(0, this.StudentManager.LastNames.Length)];
		}
		else
		{
			this.TargetName = this.JSON.Students[this.TargetID].Name;
		}

		this.CustomDescs[1].text = "Kill " + this.TargetName + ".";
		this.Descs[1].text = "Kill " + this.TargetName + ".";

		if (this.TargetID > 9 && this.TargetID < 21)
		{
			if (this.Phase == 5)
			{
				// [af] Replaced if/else statement with ternary expression.
				this.TargetID += Input.GetButtonDown(InputNames.Xbox_A) ? 1 : -1;
			}

			this.ChooseTarget();
		}
	}

	void UpdateDifficulty()
	{
		if (this.Difficulty < 1)
		{
			this.Difficulty = 1;
		}
		else if (this.Difficulty > 10)
		{
			this.Difficulty = 10;
		}

		if (this.InputManager.TappedRight)
		{
			this.PickNewCondition();
		}
		else
		{
			this.ErasePreviousCondition();
		}
	}

	void UpdateDifficultyLabel()
	{
		this.CustomDifficultyLabel.text = "Difficulty Level - " + this.Difficulty.ToString();
		this.DifficultyLabel.text = "Difficulty Level - " + this.Difficulty.ToString();

		string Line1 = "Kill " + this.TargetName + ".";
		string Line2 = string.Empty;
		string Line3 = string.Empty;
		string Line4 = string.Empty;

		if (this.RequiredWeaponID == 0)
		{
			Line2 = "You can kill the target with any weapon.";
		}
		else
		{
			Line2 = "You must kill the target with a " +
				this.WeaponNames[this.RequiredWeaponID];
		}

		if (this.RequiredClothingID == 0)
		{
			Line3 = "You can kill the target wearing any clothing.";
		}
		else
		{
			Line3 = "You must kill the target while wearing " +
				this.ClothingNames[this.RequiredClothingID];
		}

		if (this.RequiredDisposalID == 0)
		{
			Line4 = "It is not necessary to dispose of the target's corpse.";
		}
		else
		{
			Line4 = "You must dispose of the target's corpse by " +
				this.DisposalNames[this.RequiredDisposalID];
		}

		this.DescriptionLabel.text = Line1 + "\n" + "\n" + Line2 + "\n" + "\n" +
			Line3 + "\n" + "\n" + Line4;
	}

	void UpdateNemesisDifficulty()
	{
		if (this.NemesisDifficulty < 0)
		{
			this.NemesisDifficulty = 4;
		}
		else if (this.NemesisDifficulty > 4)
		{
			this.NemesisDifficulty = 0;
		}

		if (this.NemesisDifficulty == 0)
		{
			this.CustomNemesisLabel.text = "Nemesis: Off";
			this.NemesisLabel.text = "Nemesis: Off";
		}
		else
		{
			this.CustomNemesisLabel.text = "Nemesis: On";
			this.NemesisLabel.text = "Nemesis: On";

			// [af] Replaced if/else statement with ternary expression.
			this.NemesisPortrait.mainTexture = (this.NemesisDifficulty > 2) ?
				this.BlankPortrait : this.NemesisGraphic;
		}
	}

	void PickNewCondition()
	{
		int NewCondition = Random.Range(1, this.ConditionDescs.Length);

		this.Conditions[this.Difficulty] = NewCondition;
		this.Descs[this.Difficulty].text = this.ConditionDescs[NewCondition];
		this.Icons[this.Difficulty].mainTexture = this.ConditionIcons[NewCondition];

		bool Reroll = false;

		// [af] Converted while loop to for loop.
		for (int ID = 2; ID < this.Difficulty; ID++)
		{
			if (NewCondition == this.Conditions[ID])
			{
				Reroll = true;
			}
		}

		if (Reroll)
		{
			this.PickNewCondition();
		}
		else
		{
			if (NewCondition > 3)
			{
				this.Descs[this.Difficulty].text = this.ConditionDescs[NewCondition];
			}
			else
			{
				if (NewCondition == 1)
				{
					this.RequiredWeaponID = 11;

					while (this.RequiredWeaponID == 11)
					{
						this.RequiredWeaponID = Random.Range(1, this.WeaponNames.Length);
					}

					this.Descs[this.Difficulty].text = this.ConditionDescs[NewCondition] + " " +
						this.WeaponNames[this.RequiredWeaponID];
				}
				else if (NewCondition == 2)
				{
					this.RequiredClothingID = Random.Range(1, this.ClothingNames.Length);
					this.Descs[this.Difficulty].text = this.ConditionDescs[NewCondition] + " " +
						this.ClothingNames[this.RequiredClothingID];
				}
				else if (NewCondition == 3)
				{
					this.RequiredDisposalID = Random.Range(1, this.DisposalNames.Length);
					this.Descs[this.Difficulty].text = this.ConditionDescs[NewCondition] + " " + this.DisposalNames[this.RequiredDisposalID];
				}
			}
		}

		this.UpdateDifficultyLabel();
	}

	void ErasePreviousCondition()
	{
		if (this.Conditions[this.Difficulty + 1] == 1)
		{
			this.RequiredWeaponID = 0;
		}
		else if (this.Conditions[this.Difficulty + 1] == 2)
		{
			this.RequiredClothingID = 0;
		}
		else if (this.Conditions[this.Difficulty + 1] == 3)
		{
			this.RequiredDisposalID = 0;
		}

		this.Conditions[this.Difficulty + 1] = 0;

		this.UpdateDifficultyLabel();
	}

	public void UpdateGraphics()
	{
        Debug.Log("Populating the Mission Mode criteria list!");

		TargetID = MissionModeGlobals.MissionTarget;

		if (this.TargetNumber > 9 && this.TargetNumber < 21)
		{
			this.TargetPortrait.mainTexture = this.BlankPortrait;
			this.TargetName = MissionModeGlobals.MissionTargetName;
		}
		else
		{
			var path = "file:///" + Application.streamingAssetsPath +
				"/Portraits/Student_" + MissionModeGlobals.MissionTarget.ToString() + ".png";

			WWW www = new WWW(path);

			this.Icons[1].mainTexture = www.texture;
			this.TargetName = this.JSON.Students[MissionModeGlobals.MissionTarget].Name;
		}

		this.Descs[1].text = "Kill " + this.TargetName + ".";
        this.ChangeLabel(this.Descs[1]);

        // [af] Converted while loop to for loop.
        for (int ID = 2; ID < this.Objectives.Length; ID++)
		{
			this.Objectives[ID].gameObject.SetActive(false);
		}

		if (MissionModeGlobals.MissionDifficulty > 1)
		{
			// [af] Converted while loop to for loop.
			for (int ID = 2; ID < (MissionModeGlobals.MissionDifficulty + 1); ID++)
			{
				this.Objectives[ID].gameObject.SetActive(true);
				this.Icons[ID].mainTexture = this.ConditionIcons[MissionModeGlobals.GetMissionCondition(ID)];
                this.ChangeLabel(this.Descs[ID]);

                if (MissionModeGlobals.GetMissionCondition(ID) > 3)
				{
					this.Descs[ID].text = this.ConditionDescs[MissionModeGlobals.GetMissionCondition(ID)];
				}
				else
				{
					if (MissionModeGlobals.GetMissionCondition(ID) == 1)
					{
						this.RequiredWeaponID = 11;

						while (this.RequiredWeaponID == 11)
						{
							this.RequiredWeaponID = Random.Range(1, this.WeaponNames.Length);
						}

						this.Descs[ID].text =
							this.ConditionDescs[MissionModeGlobals.GetMissionCondition(ID)] + " " +
							this.WeaponNames[MissionModeGlobals.MissionRequiredWeapon];
					}
					else if (MissionModeGlobals.GetMissionCondition(ID) == 2)
					{
						this.RequiredClothingID = Random.Range(0, this.ClothingNames.Length);
						this.Descs[ID].text =
							this.ConditionDescs[MissionModeGlobals.GetMissionCondition(ID)] + " " +
							this.ClothingNames[MissionModeGlobals.MissionRequiredClothing];
					}
					else if (MissionModeGlobals.GetMissionCondition(ID) == 3)
					{
						this.RequiredDisposalID = Random.Range(1, this.DisposalNames.Length);
						this.Descs[ID].text =
							this.ConditionDescs[MissionModeGlobals.GetMissionCondition(ID)] + " " +
							this.DisposalNames[MissionModeGlobals.MissionRequiredDisposal];
					}
				}
			}
		}
	}

	void UpdatePopulation()
	{
		this.CustomPopulationLabel.text = "";
		this.PopulationLabel.text = "";
		OptionGlobals.HighPopulation = false;
	}

	void UpdateObjectiveHighlight()
	{
		if (this.Row < 1)
		{
			this.Row = 6;
		}
		else if (this.Row > 6)
		{
			this.Row = 1;
		}
		else if (this.Column < 1)
		{
			this.Column = 3;
		}
		else if (this.Column > 3)
		{
			this.Column = 1;
		}

		if ((this.Row == 6) && (this.Column == 3))
		{
			this.Column = 1;
		}

		// [af] Moved "Bonus" initialization closer to where it's used.
		int Bonus = 0;

		if (this.Row == 6)
		{
			Bonus = 75;
		}

		// [af] Replaced if/else statement with ternary expression.
		this.PromptBar.Label[2].text = ((this.Column == 1) && (this.Row < 5) ||
			(this.Column == 2) && (this.Row == 6)) ? "Change" : string.Empty;

		this.ObjectiveHighlight.localPosition = new Vector3(
			-1050.0f + (650.0f * this.Column),
			450.0f - (150.0f * this.Row) - Bonus,
			this.ObjectiveHighlight.localPosition.z);

		this.CustomSelected = this.Row + ((this.Column - 1) * 6);

		if ((this.CustomSelected == 1) || (this.CustomSelected == 12))
		{
			this.PromptBar.Label[0].text = "Forward";
		}
		else if (this.CustomSelected == 6)
		{
			this.PromptBar.Label[0].text = "Start";
		}
		else
		{
			this.PromptBar.Label[0].text = "Toggle";
		}

		if ((this.CustomSelected == 1) || (this.CustomSelected == 12))
		{
			this.PromptBar.Label[2].text = "Backward";
		}
		else if (this.CustomSelected > 4)
		{
			this.PromptBar.Label[2].text = string.Empty;
		}
		else
		{
			this.PromptBar.Label[2].text = "Change";
		}

		this.PromptBar.UpdateButtons();
	}

	public string TargetString = string.Empty;
	public string WeaponString = string.Empty;
	public string ClothingString = string.Empty;
	public string DisposalString = string.Empty;

	public string MissionID = string.Empty;

	public string[] ConditionString;

	public UILabel MissionIDLabel;

	void CalculateMissionID()
	{
		// [af] Replaced if/else statement with ternary expression.
		this.TargetString = ((this.TargetID < 10) ? "0" : "") + this.TargetID.ToString();

		if (this.CustomObjectives[2].alpha == 1.0f)
		{
			// [af] Replaced if/else statement with ternary expression.
			this.WeaponString = ((this.RequiredWeaponID < 10) ? "0" : "") +
				this.RequiredWeaponID.ToString();
		}
		else
		{
			this.WeaponString = "00";
		}

		// [af] Replaced if/else statement with ternary expression.
		this.ClothingString = (this.CustomObjectives[3].alpha == 1.0f) ?
			this.RequiredClothingID.ToString() : "0";

		// [af] Replaced if/else statement with ternary expression.
		this.DisposalString = (this.CustomObjectives[4].alpha == 1.0f) ?
			this.RequiredDisposalID.ToString() : "0";

		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < this.CustomObjectives.Length; ID++)
		{
			if (this.CustomObjectives[ID] != null)
			{
				// [af] Replaced if/else statement with ternary expression.
				this.ConditionString[ID] =
					(this.CustomObjectives[ID].alpha == 1.0f) ? "1" : "0";
			}
		}

		this.MissionID = this.TargetString + this.WeaponString + this.ClothingString + this.DisposalString + this.ConditionString[5] +
			this.ConditionString[6] + this.ConditionString[7] + this.ConditionString[8] + this.ConditionString[9] +
			this.ConditionString[10] + this.ConditionString[11] + this.ConditionString[12] + this.ConditionString[13] +
			this.ConditionString[14] + this.ConditionString[15] + this.ConditionString[16] + this.ConditionString[17] +
			this.NemesisDifficulty.ToString() + "0";

		//The final digit is "0" because school population is permanently set to "False".

		this.MissionIDLabel.text = this.MissionID;
	}

	void StartMission()
	{
		this.MyAudio.PlayOneShot(this.InfoLines[6]);

		//bool HighPopulation = OptionGlobals.HighPopulation;
		Globals.DeleteAll();

		OptionGlobals.TutorialsOff = true;

		SchoolGlobals.SchoolAtmosphere = 1.0f - (this.Difficulty * .1f);
		MissionModeGlobals.NemesisDifficulty = this.NemesisDifficulty;
		MissionModeGlobals.MissionTargetName = this.TargetName;
		MissionModeGlobals.MissionDifficulty = this.Difficulty;
		//OptionGlobals.HighPopulation = HighPopulation;
		MissionModeGlobals.MissionTarget = this.TargetID;
		SchoolGlobals.SchoolAtmosphereSet = true;
		MissionModeGlobals.MissionMode = true;

		ClassGlobals.BiologyGrade = 1;
		ClassGlobals.ChemistryGrade = 1;
		ClassGlobals.LanguageGrade = 1;
		ClassGlobals.PhysicalGrade = 1;
		ClassGlobals.PsychologyGrade = 1;

		if (this.Difficulty > 1)
		{
			// [af] Converted while loop to for loop.
			for (int ID = 2; ID < (this.Difficulty + 1); ID++)
			{
				if (this.Conditions[ID] == 1)
				{
					MissionModeGlobals.MissionRequiredWeapon = this.RequiredWeaponID;
				}
				else if (this.Conditions[ID] == 2)
				{
					MissionModeGlobals.MissionRequiredClothing = this.RequiredClothingID;
				}
				else if (this.Conditions[ID] == 3)
				{
					MissionModeGlobals.MissionRequiredDisposal = this.RequiredDisposalID;
				}

				MissionModeGlobals.SetMissionCondition(ID, this.Conditions[ID]);
			}
		}

		this.PromptBar.Show = false;
		this.Speed = 0.0f;
		this.Phase = 4;
	}

	public Font Arial;

	void ChangeFont()
	{
		UILabel[] Labels = FindObjectsOfType<UILabel>();

		foreach (UILabel Text in Labels)
		{
			Text.trueTypeFont = this.Arial;

			Text.fontSize += 10;

			if (Text.height == 150)
			{
				Text.height = 100;
			}
		}
	}

    void ChangeLabel(UILabel Text)
    {
        Text.trueTypeFont = this.Arial;
    }
}