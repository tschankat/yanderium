using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMissionWindowScript : MonoBehaviour
{
	public MissionModeMenuScript MissionModeMenu;
	public InputManagerScript InputManager;
	public JsonScript JSON;

	public GameObject[] DeathSkulls;
	public GameObject[] Button;

	public UILabel[] MethodLabel;
	public UILabel[] NameLabel;

	public UITexture[] Portrait;

	public bool ChangingDifficulty;

	public int[] UnsafeNumbers;
	public int[] Target;
	public int[] Method;

	public string[] MethodNames;

	public int Selected;
	public int Column;
	public int Row;

	public Transform DifficultyOptions;
	public Transform Highlight;

	public Texture BlankPortrait;

    public Font Arial;

	void Start()
	{
		UpdateHighlight();

		int ID = 1;

		while (ID < 11)
		{
            Portrait[ID].mainTexture = BlankPortrait;
			NameLabel[ID].text = "Kill: (Nobody)";
			MethodLabel[ID].text = "By: Attacking";
			DeathSkulls[ID].SetActive(false);
			ID++;
		}

		DifficultyOptions.localScale = new Vector3(0, 0, 0);
	}

    void ChangeFont(UILabel Text)
    {
        Text.trueTypeFont = this.Arial;

        //Text.fontSize += 10;

        if (Text.height == 150)
        {
            Text.height = 100;
        }
    }

	void Update()
	{
		if (!ChangingDifficulty)
		{
			if (InputManager.TappedDown)
			{
				Row++;
				UpdateHighlight();
			}

			if (InputManager.TappedUp)
			{
				Row--;
				UpdateHighlight();
			}

			if (InputManager.TappedRight)
			{
				Column++;
				UpdateHighlight();
			}

			if (InputManager.TappedLeft)
			{
				Column--;
				UpdateHighlight();
			}

			int TargetID = 0;

			if (Input.GetButtonDown(InputNames.Xbox_A))
			{
				int Targets = 0;
				int ID = 0;

				while (ID < 11)
				{
					if (Target[ID] > 0)
					{
						Targets++;
					}

					ID++;
				}

				if (Row == 5)
				{
					if (Column == 1)
					{
						if (Targets > 0)
						{
							Globals.DeleteAll();
							SaveInfo();

							MissionModeMenu.GetComponent<AudioSource>().PlayOneShot(MissionModeMenu.InfoLines[6]);

							SchoolGlobals.SchoolAtmosphere = 1.0f - (Targets * .1f);
							SchoolGlobals.SchoolAtmosphereSet = true;
							MissionModeGlobals.MissionMode = true;
							MissionModeGlobals.MultiMission = true;
							MissionModeGlobals.MissionDifficulty = Targets;

							ClassGlobals.BiologyGrade = 1;
							ClassGlobals.ChemistryGrade = 1;
							ClassGlobals.LanguageGrade = 1;
							ClassGlobals.PhysicalGrade = 1;
							ClassGlobals.PsychologyGrade = 1;

							MissionModeMenu.PromptBar.Show = false;
							MissionModeMenu.Speed = 0.0f;
							MissionModeMenu.Phase = 4;

							enabled = false;
						}
					}
					else if (Column == 2)
					{
						Randomize();
					}
					else if (Column == 3)
					{
						ChangingDifficulty = true;

						MissionModeMenu.PromptBar.ClearButtons();
						MissionModeMenu.PromptBar.Label[0].text = "Change";
						MissionModeMenu.PromptBar.Label[1].text = "Back";
						MissionModeMenu.PromptBar.Label[2].text = "Aggression";
						MissionModeMenu.PromptBar.UpdateButtons();
						MissionModeMenu.PromptBar.Show = true;
					}
				}
			}

			if (Input.GetButtonDown(InputNames.Xbox_B))
			{
				MissionModeMenu.PromptBar.ClearButtons();
				MissionModeMenu.PromptBar.Label[0].text = "Accept";
				MissionModeMenu.PromptBar.Label[4].text = "Choose";
				MissionModeMenu.PromptBar.UpdateButtons();
				MissionModeMenu.PromptBar.Show = true;

				MissionModeMenu.TargetID = 0;
				MissionModeMenu.Phase = 2;

				enabled = false;
			}

			//INCREMENT
			if (Input.GetButtonDown(InputNames.Xbox_X))
			{
				if (Row == 1)
				{
					int ID = 1;while (ID < 11){UnsafeNumbers[ID] = Target[ID];ID++;}

					Increment(0);

					if (Target[Column] != 0)
					{
						while (Target[Column] != 0 && Target[Column] == UnsafeNumbers[1] ||
							Target[Column] != 0 && Target[Column] == UnsafeNumbers[2] ||
							Target[Column] != 0 && Target[Column] == UnsafeNumbers[3] ||
							Target[Column] != 0 && Target[Column] == UnsafeNumbers[4] ||
							Target[Column] != 0 && Target[Column] == UnsafeNumbers[5] ||
							Target[Column] != 0 && Target[Column] == UnsafeNumbers[6] ||
							Target[Column] != 0 && Target[Column] == UnsafeNumbers[7] ||
							Target[Column] != 0 && Target[Column] == UnsafeNumbers[8] ||
							Target[Column] != 0 && Target[Column] == UnsafeNumbers[9] ||
							Target[Column] != 0 && Target[Column] == UnsafeNumbers[10])
						{
							Increment(0);
						}
					}

					UnsafeNumbers[Column] = Target[Column];
				}
				else if (Row == 2)
				{
					Method[Column]++;

					if (Method[Column] == MethodNames.Length)
					{
						Method[Column] = 0;
					}

					MethodLabel[Column].text = "By: " + MethodNames[Method[Column]];
				}
				else if (Row == 3)
				{
					int ID = 1;while (ID < 11){UnsafeNumbers[ID] = Target[ID];ID++;}

					Increment(5);

					if (Target[Column + 5] != 0)
					{
						while (Target[Column + 5] != 0 && Target[Column + 5] == UnsafeNumbers[1] ||
							Target[Column + 5] != 0 && Target[Column + 5] == UnsafeNumbers[2] ||
							Target[Column + 5] != 0 && Target[Column + 5] == UnsafeNumbers[3] ||
							Target[Column + 5] != 0 && Target[Column + 5] == UnsafeNumbers[4] ||
							Target[Column + 5] != 0 && Target[Column + 5] == UnsafeNumbers[5] ||
							Target[Column + 5] != 0 && Target[Column + 5] == UnsafeNumbers[6] ||
							Target[Column + 5] != 0 && Target[Column + 5] == UnsafeNumbers[7] ||
							Target[Column + 5] != 0 && Target[Column + 5] == UnsafeNumbers[8] ||
							Target[Column + 5] != 0 && Target[Column + 5] == UnsafeNumbers[9] ||
							Target[Column + 5] != 0 && Target[Column + 5] == UnsafeNumbers[10])
						{
							Increment(5);
						}
					}

					UnsafeNumbers[Column + 5] = Target[Column + 5];
				}
				else if (Row == 4)
				{
					Method[Column + 5]++;

					if (Method[Column + 5] == MethodNames.Length)
					{
						Method[Column + 5] = 0;
					}

					MethodLabel[Column + 5].text = "By: " + MethodNames[Method[Column + 5]];
				}
			}

			//DECREMENT
			if (Input.GetButtonDown(InputNames.Xbox_Y))
			{
				if (Row == 1)
				{
					int ID = 1;while (ID < 11){UnsafeNumbers[ID] = Target[ID];ID++;}

					Decrement(0);

					if (Target[Column] != 0)
					{
						while (Target[Column] != 0 && Target[Column] == UnsafeNumbers[1] ||
							Target[Column] != 0 && Target[Column] == UnsafeNumbers[2] ||
							Target[Column] != 0 && Target[Column] == UnsafeNumbers[3] ||
							Target[Column] != 0 && Target[Column] == UnsafeNumbers[4] ||
							Target[Column] != 0 && Target[Column] == UnsafeNumbers[5] ||
							Target[Column] != 0 && Target[Column] == UnsafeNumbers[6] ||
							Target[Column] != 0 && Target[Column] == UnsafeNumbers[7] ||
							Target[Column] != 0 && Target[Column] == UnsafeNumbers[8] ||
							Target[Column] != 0 && Target[Column] == UnsafeNumbers[9] ||
							Target[Column] != 0 && Target[Column] == UnsafeNumbers[10])
						{
							Debug.Log("Unsafe number. We're going to have to decrement.");

							Decrement(0);
						}
					}

					UnsafeNumbers[Column] = Target[Column];
				}
				else if (Row == 2)
				{
					Method[Column]--;

					if (Method[Column] < 0)
					{
						Method[Column] = MethodNames.Length - 1;
					}

					MethodLabel[Column].text = "By: " + MethodNames[Method[Column]];
				}
				else if (Row == 3)
				{
					int ID = 1;while (ID < 11){UnsafeNumbers[ID] = Target[ID];ID++;}

					Decrement(5);

					if (Target[Column + 5] != 0)
					{
						while (Target[Column + 5] != 0 && Target[Column + 5] == UnsafeNumbers[1] ||
							Target[Column + 5] != 0 && Target[Column + 5] == UnsafeNumbers[2] ||
							Target[Column + 5] != 0 && Target[Column + 5] == UnsafeNumbers[3] ||
							Target[Column + 5] != 0 && Target[Column + 5] == UnsafeNumbers[4] ||
							Target[Column + 5] != 0 && Target[Column + 5] == UnsafeNumbers[5] ||
							Target[Column + 5] != 0 && Target[Column + 5] == UnsafeNumbers[6] ||
							Target[Column + 5] != 0 && Target[Column + 5] == UnsafeNumbers[7] ||
							Target[Column + 5] != 0 && Target[Column + 5] == UnsafeNumbers[8] ||
							Target[Column + 5] != 0 && Target[Column + 5] == UnsafeNumbers[9] ||
							Target[Column + 5] != 0 && Target[Column + 5] == UnsafeNumbers[10])
						{
							Debug.Log("Unsafe number. We're going to have to decrement.");

							Decrement(5);
						}
					}

					UnsafeNumbers[Column + 5] = Target[Column + 5];
				}
				else if (Row == 4)
				{
					Method[Column + 5]--;

					if (Method[Column + 5] < 0)
					{
						Method[Column + 5] = MethodNames.Length - 1;
					}

					MethodLabel[Column + 5].text = "By: " + MethodNames[Method[Column + 5]];
				}
			}

			if (Input.GetKeyDown("space"))
			{
				FillOutInfo();
			}

			DifficultyOptions.localScale = Vector3.Lerp (DifficultyOptions.localScale, new Vector3 (0, 0, 0), Time.deltaTime * 10);
		}
		else
		{
			DifficultyOptions.localScale = Vector3.Lerp (DifficultyOptions.localScale, new Vector3 (1, 1, 1), Time.deltaTime * 10);

			if (Input.GetButtonDown(InputNames.Xbox_A))
			{
				NemesisDifficulty++;
				UpdateNemesisDifficulty();
			}

			if (Input.GetButtonDown(InputNames.Xbox_X))
			{
				NemesisAggression = !NemesisAggression;
				UpdateNemesisDifficulty();
			}

			if (Input.GetButtonDown(InputNames.Xbox_B))
			{
				MissionModeMenu.PromptBar.ClearButtons();
				MissionModeMenu.PromptBar.Label[0].text = "";
				MissionModeMenu.PromptBar.Label[1].text = "Return";
				MissionModeMenu.PromptBar.Label[2].text = "Adjust Up";
				MissionModeMenu.PromptBar.Label[3].text = "Adjust Down";
				MissionModeMenu.PromptBar.Label[4].text = "Selection";
				MissionModeMenu.PromptBar.Label[5].text = "Selection";
				MissionModeMenu.PromptBar.UpdateButtons();

				Column = 1;
				Row = 1;
				UpdateHighlight();

				ChangingDifficulty = false;
			}
		}

		UpdateNemesisList();
	}

	void Increment(int Number)
	{
		Target[Column + Number]++;

		if (Target[Column + Number] == 1)
		{
			Target[Column + Number] = 2;
		}
		else if (Target[Column + Number] == 10)
		{
			Target[Column + Number] = 21;
		}
		else if (Target[Column + Number] > 89)
		{
			Target[Column + Number] = 0;
		}

		if (Target[Column + Number] == 0)
		{
			NameLabel[Column + Number].text = "Kill: Nobody";
		}
		else
		{
			NameLabel[Column + Number].text = "Kill: " + JSON.Students[Target[Column + Number]].Name;
		}

		string path = "file:///" + Application.streamingAssetsPath + "/Portraits/Student_" + Target[Column + Number] + ".png";
		WWW www = new WWW(path);
		Portrait[Column + Number].mainTexture = www.texture;
	}

	void Decrement(int Number)
	{
		Target[Column + Number]--;

		Debug.Log("Decremented. Number has become: " + Target[Column + Number]);

		if (Target[Column + Number] == 1)
		{
			Target[Column + Number] = 0;

			Debug.Log("Correcting to 0.");
		}
		else if (Target[Column + Number] == 20)
		{
			Target[Column + Number] = 9;

			Debug.Log("Correcting to 9.");
		}
		else if (Target[Column + Number] == -1)
		{
			Target[Column + Number] = 89;

			Debug.Log("Correcting to 89.");
		}

		if (Target[Column + Number] == 0)
		{
			NameLabel[Column + Number].text = "Kill: Nobody";
		}
		else
		{
			NameLabel[Column + Number].text = "Kill: " + JSON.Students[Target[Column + Number]].Name;
		}

		string path = "file:///" + Application.streamingAssetsPath + "/Portraits/Student_" + Target[Column + Number] + ".png";
		WWW www = new WWW(path);
		Portrait[Column + Number].mainTexture = www.texture;
	}

	void Randomize()
	{
		int TempID = 1;

		while (TempID < 11)
		{
			Target[TempID] = Random.Range(2, 89);

			Method[TempID] = Random.Range(0, 7);

			MethodLabel[TempID].text = "By: " + MethodNames[Method[TempID]];

			TempID++;
		}

		TempID = 1;

		Column = 0;

		while (TempID < 11)
		{
			int ID = 1;while (ID < 11){UnsafeNumbers[ID] = Target[ID];ID++;}

			while (Target[TempID] == UnsafeNumbers[1] ||
				Target[TempID] == UnsafeNumbers[2] ||
				Target[TempID] == UnsafeNumbers[3] ||
				Target[TempID] == UnsafeNumbers[4] ||
				Target[TempID] == UnsafeNumbers[5] ||
				Target[TempID] == UnsafeNumbers[6] ||
				Target[TempID] == UnsafeNumbers[7] ||
				Target[TempID] == UnsafeNumbers[8] ||
				Target[TempID] == UnsafeNumbers[9] ||
				Target[TempID] == UnsafeNumbers[10] ||
				Target[TempID] == 0 || Target[TempID] > 9 && Target[TempID] < 21)
			{
				Increment(TempID);
			}

			TempID++;
		}

		Column = 2;
	}

	public void UpdateHighlight()
	{
		this.MissionModeMenu.PromptBar.Label[0].text = "";

		if (Row < 1)
		{
			Row = 5;
		}
		else if (Row > 5)
		{
			Row = 1;
		}

		if (Row < 5)
		{
			if (Column < 1)
			{
				Column = 5;
			}
			else if (Column > 5)
			{
				Column = 1;
			}

			int Height = 0;

			     if (Row == 1){Height = 225;}
			else if (Row == 2){Height = 125;}
			else if (Row == 3){Height = -300;}
			else if (Row == 4){Height = -400;}

			Highlight.localPosition = new Vector3(
				-1200 + (400 * Column),
				Height,
				0);
		}
		else
		{
			if (Column < 1)
			{
				Column = 3;
			}
			else if (Column > 3)
			{
				Column = 1;
			}

			Highlight.localPosition = new Vector3(
				-1200 + (600 * Column),
				-525,
				0);

			if (Column == 1)
			{
				if (Target[1] + Target[2] + Target[3] + 
					Target[4] + Target[5] + Target[6] + 
					Target[7] + Target[8] + Target[9] + 
					Target[10] == 0)
				{
					this.MissionModeMenu.PromptBar.Label[0].text = "";
				}
				else
				{
					this.MissionModeMenu.PromptBar.Label[0].text = "Confirm";
				}
			}
			else if (Column == 2)
			{
				this.MissionModeMenu.PromptBar.Label[0].text = "Confirm";
			}
			else
			{
				this.MissionModeMenu.PromptBar.Label[0].text = "";
			}

			this.MissionModeMenu.PromptBar.UpdateButtons();
		}
	}

	void SaveInfo()
	{
		int ID = 1;

		while (ID < 11)
		{
			PlayerPrefs.SetInt("MissionModeTarget" + ID, Target[ID]);
			PlayerPrefs.SetInt("MissionModeMethod" + ID, Method[ID]);

			ID++;
		}

		MissionModeGlobals.NemesisDifficulty = NemesisDifficulty;
		MissionModeGlobals.NemesisAggression = NemesisAggression;
	}

	public void FillOutInfo()
	{
		int ID = 1;

		while (ID < 11)
		{
            ChangeFont(NameLabel[ID]);
            ChangeFont(MethodLabel[ID]);

            Target[ID] = PlayerPrefs.GetInt("MissionModeTarget" + ID);
			Method[ID] = PlayerPrefs.GetInt("MissionModeMethod" + ID);

			if (Target[ID] == 0)
			{
				NameLabel[ID].text = "Kill: Nobody";
			}
			else
			{
				NameLabel[ID].text = "Kill: " + JSON.Students[Target[ID]].Name;
			}

			string path = "file:///" + Application.streamingAssetsPath + "/Portraits/Student_" + Target[ID] + ".png";
			WWW www = new WWW(path);
			Portrait[ID].mainTexture = www.texture;

			MethodLabel[ID].text = "By: " + MethodNames[Method[ID]];

			DeathSkulls[ID].SetActive(false);

			ID++;
		}
	}

	public void HideButtons()
	{
		Button[0].SetActive(false);
		Button[1].SetActive(false);
		Button[2].SetActive(false);
		Button[3].SetActive(false);
	}

	public int NemesisDifficulty = 0;

	public bool NemesisAggression;

	public UILabel NemesisLabel;
	public UITexture NemesisPortrait;

	public Texture AnonymousPortrait;
	public Texture NemesisGraphic;

	void UpdateNemesisDifficulty()
	{
		if (NemesisDifficulty < 0)
		{
			NemesisDifficulty = 4;
		}
		else if (this.NemesisDifficulty > 4)
		{
			NemesisDifficulty = 0;
		}

		if (this.NemesisDifficulty == 0)
		{
			NemesisLabel.text = "Nemesis: Off";
		}
		else
		{
			NemesisLabel.text = "Nemesis: On";

			NemesisPortrait.mainTexture = (NemesisDifficulty > 2) ?
				AnonymousPortrait : NemesisGraphic;
		}
	}

	public Transform[] NemesisObjectives;

	void UpdateNemesisList()
	{
		if (this.NemesisDifficulty == 0)
		{
			this.NemesisObjectives[1].localScale = Vector3.Lerp(
				this.NemesisObjectives[1].localScale, Vector3.zero, Time.deltaTime * 10.0f);
			this.NemesisObjectives[2].localScale = Vector3.Lerp(
				this.NemesisObjectives[2].localScale, Vector3.zero, Time.deltaTime * 10.0f);
			this.NemesisObjectives[3].localScale = Vector3.Lerp(
				this.NemesisObjectives[3].localScale, Vector3.zero, Time.deltaTime * 10.0f);
		}
		else if (this.NemesisDifficulty == 1)
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

		if (this.NemesisAggression)
		{
			this.NemesisObjectives[4].localScale = Vector3.Lerp(
				this.NemesisObjectives[4].localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
		}
		else
		{
			this.NemesisObjectives[4].localScale = Vector3.Lerp(
				this.NemesisObjectives[4].localScale, new Vector3(0.0f, 0.0f, 0.0f), Time.deltaTime * 10.0f);
		}
	}
}