using System.IO;
using UnityEngine;

public class StatsScript : MonoBehaviour
{
	public PauseScreenScript PauseScreen;
	public PromptBarScript PromptBar;
    public ClassScript Class;

	public UISprite[] Subject1Bars;
	public UISprite[] Subject2Bars;
	public UISprite[] Subject3Bars;
	public UISprite[] Subject4Bars;
	public UISprite[] Subject5Bars;
	public UISprite[] Subject6Bars;
	public UISprite[] Subject7Bars;
	public UISprite[] Subject8Bars;

	public UILabel[] Ranks;
	public UILabel ClubLabel;

	public int Grade = 0;
	public int BarID = 0;

	public UITexture Portrait;
	
	ClubTypeAndStringDictionary ClubLabels;

	void Awake()
	{
		// [af] Initialize "club type -> club label" associations.
		this.ClubLabels = new ClubTypeAndStringDictionary
		{
			{ ClubType.None, "None" },
			{ ClubType.Cooking, "Cooking" },
			{ ClubType.Drama, "Drama" },
			{ ClubType.Occult, "Occult" },
			{ ClubType.Art, "Art" },
			{ ClubType.LightMusic, "Light Music" },
			{ ClubType.MartialArts, "Martial Arts" },
			{ ClubType.Photography, "Photography" },
			{ ClubType.Science, "Science" },
			{ ClubType.Sports, "Sports" },
			{ ClubType.Gardening, "Gardening" },
			{ ClubType.Gaming, "Gaming" }
		};
	}

	void Start()
	{
		if (File.Exists(Application.streamingAssetsPath + "/CustomPortrait.txt"))
		{
			string Text = File.ReadAllText(Application.streamingAssetsPath + "/CustomPortrait.txt");

			if (Text == "1")
			{
				string path = "file:///" + Application.streamingAssetsPath + "/CustomPortrait.png";
				WWW www = new WWW(path);
				this.Portrait.mainTexture = www.texture;
			}
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Backslash))
		{
			this.Class.BiologyGrade = 1;
            this.Class.ChemistryGrade = 5;
            this.Class.LanguageGrade = 2;
            this.Class.PhysicalGrade = 4;
            this.Class.PsychologyGrade = 3;

            this.Class.Seduction = 4;
            this.Class.Numbness = 2;
            this.Class.Enlightenment = 5;

			this.UpdateStats();
		}

		if (Input.GetButtonDown(InputNames.Xbox_B))
		{
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Accept";
			this.PromptBar.Label[1].text = "Exit";
			this.PromptBar.Label[4].text = "Choose";
			this.PromptBar.UpdateButtons();

			this.PauseScreen.MainMenu.SetActive(true);
			this.PauseScreen.Sideways = false;
			this.PauseScreen.PressedB = true;

			// [af] Added "gameObject" for C# compatibility.
			this.gameObject.SetActive(false);
		}
	}

	public void UpdateStats()
	{
		this.Grade = this.Class.BiologyGrade;

		// [af] Converted while loop to for loop.
		for (this.BarID = 1; this.BarID < 6; this.BarID++)
		{
			UISprite bar = this.Subject1Bars[this.BarID];

			if (this.Grade > 0)
			{
				bar.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
				this.Grade--;
			}
			else
			{
				bar.color = new Color(1.0f, 1.0f, 1.0f, 0.50f);
			}
		}

		if (this.Class.BiologyGrade < 5)
		{
			// [af] Replaced if/else statement with ternary expression.
			this.Subject1Bars[this.Class.BiologyGrade + 1].color = 
				(this.Class.BiologyBonus > 0) ? new Color(1.0f, 0.0f, 0.0f, 1.0f) : 
				new Color(1.0f, 1.0f, 1.0f, 0.50f);
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////

		this.Grade = this.Class.ChemistryGrade;

		// [af] Converted while loop to for loop.
		for (this.BarID = 1; this.BarID < 6; this.BarID++)
		{
			UISprite bar = this.Subject2Bars[this.BarID];

			if (this.Grade > 0)
			{
				bar.color = new Color(bar.color.r, bar.color.g, bar.color.b, 1.0f);
				this.Grade--;
			}
			else
			{
				bar.color = new Color(bar.color.r, bar.color.g, bar.color.b, 0.50f);
			}
		}

		if (this.Class.ChemistryGrade < 5)
		{
			// [af] Replaced if/else statement with ternary expression.
			this.Subject2Bars[this.Class.ChemistryGrade + 1].color = 
				(this.Class.ChemistryBonus > 0) ? new Color(1.0f, 0.0f, 0.0f, 1.0f) : 
				new Color(1.0f, 1.0f, 1.0f, 0.50f);
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////

		this.Grade = this.Class.LanguageGrade;

		// [af] Converted while loop to for loop.
		for (this.BarID = 1; this.BarID < 6; this.BarID++)
		{
			UISprite bar = this.Subject3Bars[this.BarID];

			if (this.Grade > 0)
			{
				bar.color = new Color(bar.color.r, bar.color.g, bar.color.b, 1.0f);
				this.Grade--;
			}
			else
			{
				bar.color = new Color(bar.color.r, bar.color.g, bar.color.b, 0.50f);
			}
		}

		if (this.Class.LanguageGrade < 5)
		{
			// [af] Replaced if/else statement with ternary expression.
			this.Subject3Bars[this.Class.LanguageGrade + 1].color = 
				(this.Class.LanguageBonus > 0) ? new Color(1.0f, 0.0f, 0.0f, 1.0f) : 
				new Color(1.0f, 1.0f, 1.0f, 0.50f);
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////

		this.Grade = this.Class.PhysicalGrade;

		// [af] Converted while loop to for loop.
		for (this.BarID = 1; this.BarID < 6; this.BarID++)
		{
			UISprite bar = this.Subject4Bars[this.BarID];

			if (this.Grade > 0)
			{
				bar.color = new Color(bar.color.r, bar.color.g, bar.color.b, 1.0f);
				this.Grade--;
			}
			else
			{
				bar.color = new Color(bar.color.r, bar.color.g, bar.color.b, 0.50f);
			}
		}

		if (this.Class.PhysicalGrade < 5)
		{
			// [af] Replaced if/else statement with ternary expression.
			this.Subject4Bars[this.Class.PhysicalGrade + 1].color = 
				(this.Class.PhysicalBonus > 0) ? new Color(1.0f, 0.0f, 0.0f, 1.0f) : 
				new Color(1.0f, 1.0f, 1.0f, 0.50f);
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////

		this.Grade = this.Class.PsychologyGrade;

		// [af] Converted while loop to for loop.
		for (this.BarID = 1; this.BarID < 6; this.BarID++)
		{
			UISprite bar = this.Subject5Bars[this.BarID];

			if (this.Grade > 0)
			{
				bar.color = new Color(bar.color.r, bar.color.g, bar.color.b, 1.0f);
				this.Grade--;
			}
			else
			{
				bar.color = new Color(bar.color.r, bar.color.g, bar.color.b, 0.50f);
			}
		}

		if (this.Class.PsychologyGrade < 5)
		{
			// [af] Replaced if/else statement with ternary expression.
			this.Subject5Bars[this.Class.PsychologyGrade + 1].color = 
				(this.Class.PsychologyBonus > 0) ? new Color(1.0f, 0.0f, 0.0f, 1.0f) : 
				new Color(1.0f, 1.0f, 1.0f, 0.50f);
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////

		this.Grade = this.Class.Seduction;

		// [af] Converted while loop to for loop.
		for (this.BarID = 1; this.BarID < 6; this.BarID++)
		{
			UISprite bar = this.Subject6Bars[this.BarID];

			if (this.Grade > 0)
			{
				bar.color = new Color(bar.color.r, bar.color.g, bar.color.b, 1.0f);
				this.Grade--;
			}
			else
			{
				bar.color = new Color(bar.color.r, bar.color.g, bar.color.b, 0.50f);
			}
		}

		if (this.Class.Seduction < 5)
		{
			// [af] Replaced if/else statement with ternary expression.
			this.Subject6Bars[this.Class.Seduction + 1].color = (this.Class.SeductionBonus > 0) ?
				new Color(1.0f, 0.0f, 0.0f, 1.0f) : new Color(1.0f, 1.0f, 1.0f, 0.50f);
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////

		this.Grade = this.Class.Numbness;

		// [af] Converted while loop to for loop.
		for (this.BarID = 1; this.BarID < 6; this.BarID++)
		{
			UISprite bar = this.Subject7Bars[this.BarID];

			if (this.Grade > 0)
			{
				bar.color = new Color(bar.color.r, bar.color.g, bar.color.b, 1.0f);
				this.Grade--;
			}
			else
			{
				bar.color = new Color(bar.color.r, bar.color.g, bar.color.b, 0.50f);
			}
		}

		if (this.Class.Numbness < 5)
		{
			// [af] Replaced if/else statement with ternary expression.
			this.Subject7Bars[this.Class.Numbness + 1].color = (this.Class.NumbnessBonus > 0) ?
				new Color(1.0f, 0.0f, 0.0f, 1.0f) : new Color(1.0f, 1.0f, 1.0f, 0.50f);
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////

		this.Grade = this.Class.Enlightenment;

		// [af] Converted while loop to for loop.
		for (this.BarID = 1; this.BarID < 6; this.BarID++)
		{
			UISprite bar = this.Subject8Bars[this.BarID];

			if (this.Grade > 0)
			{
				bar.color = new Color(bar.color.r, bar.color.g, bar.color.b, 1.0f);
				this.Grade--;
			}
			else
			{
				bar.color = new Color(bar.color.r, bar.color.g, bar.color.b, 0.50f);
			}
		}

		if (this.Class.Enlightenment < 5)
		{
			// [af] Replaced if/else statement with ternary expression.
			this.Subject8Bars[this.Class.Enlightenment + 1].color = (this.Class.EnlightenmentBonus > 0) ?
				new Color(1.0f, 0.0f, 0.0f, 1.0f) : new Color(1.0f, 1.0f, 1.0f, 0.50f);
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////

		this.Ranks[1].text = "Rank: " + this.Class.BiologyGrade.ToString();
		this.Ranks[2].text = "Rank: " + this.Class.ChemistryGrade.ToString();
		this.Ranks[3].text = "Rank: " + this.Class.LanguageGrade.ToString();
		this.Ranks[4].text = "Rank: " + this.Class.PhysicalGrade.ToString();
		this.Ranks[5].text = "Rank: " + this.Class.PsychologyGrade.ToString();
		this.Ranks[6].text = "Rank: " + this.Class.Seduction.ToString();
		this.Ranks[7].text = "Rank: " + this.Class.Numbness.ToString();
		this.Ranks[8].text = "Rank: " + this.Class.Enlightenment.ToString();

		ClubType Club = PauseScreen.Yandere.Club;

		// [af] Get the label associated with the club. If there is no association,
		// then it needs to be added in the initializer in Awake().
		string label;
		bool labelExists = this.ClubLabels.TryGetValue(Club, out label);
		Debug.Assert(labelExists, "\"" + Club.ToString() + "\" missing from club labels.");
		this.ClubLabel.text = "Club: " + label;
	}
}
