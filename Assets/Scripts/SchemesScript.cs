using System;
using UnityEngine;

public class SchemesScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public SchemeManagerScript SchemeManager;
	public InputManagerScript InputManager;
	public InventoryScript Inventory;
	public PromptBarScript PromptBar;
	public GameObject FavorMenu;
	public Transform Highlight;
	public Transform Arrow;

	public UILabel SchemeInstructions;
	public UITexture SchemeIcon;
	public UILabel PantyCount;
	public UILabel SchemeDesc;

	public UILabel[] SchemeDeadlineLabels;
	public UILabel[] SchemeCostLabels;
	public UILabel[] SchemeNameLabels;

	public UISprite[] Exclamations;

	public Texture[] SchemeIcons;

	public int[] SchemeCosts;

	public Transform[] SchemeDestinations;
	public string[] SchemeDeadlines;
	public string[] SchemeSkills;
	public string[] SchemeDescs;
	public string[] SchemeNames;

	[Multiline]
	[SerializeField]
	public string[] SchemeSteps;

	public int ID = 1;
	public string[] Steps;
	public AudioClip InfoPurchase;
	public AudioClip InfoAfford;

	public Transform[] Scheme1Destinations;
	public Transform[] Scheme2Destinations;
	public Transform[] Scheme3Destinations;
	public Transform[] Scheme4Destinations;
	public Transform[] Scheme5Destinations;

	void Start()
	{
		// [af] Converted while loop to for loop.
		for (int ListID = 1; ListID < this.SchemeNames.Length; ListID++)
		{
			if (!SchemeGlobals.GetSchemeStatus(ListID))
			{
				this.SchemeDeadlineLabels[ListID].text = this.SchemeDeadlines[ListID];
				this.SchemeNameLabels[ListID].text = this.SchemeNames[ListID];
			}
		}

		this.SchemeNameLabels[1].color = new Color(0, 0, 0, .5f);
		this.SchemeNameLabels[2].color = new Color(0, 0, 0, .5f);
		this.SchemeNameLabels[3].color = new Color(0, 0, 0, .5f);
		this.SchemeNameLabels[4].color = new Color(0, 0, 0, .5f);
		this.SchemeNameLabels[5].color = new Color(0, 0, 0, .5f);

		if (DateGlobals.Weekday == DayOfWeek.Monday){this.SchemeNameLabels[1].color = new Color(0, 0, 0, 1);}
		if (DateGlobals.Weekday == DayOfWeek.Tuesday){this.SchemeNameLabels[2].color = new Color(0, 0, 0, 1);}
		if (DateGlobals.Weekday == DayOfWeek.Wednesday){this.SchemeNameLabels[3].color = new Color(0, 0, 0, 1);}
		if (DateGlobals.Weekday == DayOfWeek.Thursday){this.SchemeNameLabels[4].color = new Color(0, 0, 0, 1);}
		if (DateGlobals.Weekday == DayOfWeek.Friday){this.SchemeNameLabels[5].color = new Color(0, 0, 0, 1);}
	}

	void Update()
	{
		if (this.InputManager.TappedUp)
		{
			this.ID--;

			if (this.ID < 1)
			{
				this.ID = this.SchemeNames.Length - 1;
			}

			this.UpdateSchemeInfo();
		}

		if (this.InputManager.TappedDown)
		{
			this.ID++;

			if (this.ID > (this.SchemeNames.Length - 1))
			{
				this.ID = 1;
			}

			this.UpdateSchemeInfo();
		}

		if (Input.GetButtonDown(InputNames.Xbox_A))
		{
			AudioSource audioSource = this.GetComponent<AudioSource>();

			if (this.PromptBar.Label[0].text != string.Empty)
			{
				if (this.SchemeNameLabels[this.ID].color.a == 1)
				{
					SchemeManager.enabled = true;

					if (this.ID == 5)
					{
						SchemeManager.ClockCheck = true;
					}

					if (!SchemeGlobals.GetSchemeUnlocked(this.ID))
					{
						if (this.Inventory.PantyShots >= this.SchemeCosts[this.ID])
						{
							this.Inventory.PantyShots -= this.SchemeCosts[this.ID];
							SchemeGlobals.SetSchemeUnlocked(this.ID, true);
							SchemeGlobals.CurrentScheme = this.ID;

							if (SchemeGlobals.GetSchemeStage(this.ID) == 0)
							{
								SchemeGlobals.SetSchemeStage(this.ID, 1);
							}

							this.UpdateSchemeDestinations();
							this.UpdateInstructions();
							this.UpdateSchemeList();
							this.UpdateSchemeInfo();

							audioSource.clip = this.InfoPurchase;
							audioSource.Play();
						}
					}
					else
					{
						if (SchemeGlobals.CurrentScheme == this.ID)
						{
							SchemeGlobals.CurrentScheme = 0;
							SchemeManager.enabled = false;
						}
						else
						{
							SchemeGlobals.CurrentScheme = this.ID;
						}

						this.UpdateSchemeDestinations();
						this.UpdateInstructions();
						this.UpdateSchemeInfo();
					}
				}
			}
			else
			{
				if (SchemeGlobals.GetSchemeStage(this.ID) != 100)
				{
					if (this.Inventory.PantyShots < this.SchemeCosts[this.ID])
					{
						audioSource.clip = this.InfoAfford;
						audioSource.Play();
					}
				}
			}
		}

		if (Input.GetButtonDown(InputNames.Xbox_B))
		{
			// [af] Removed redundant if statement.
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Accept";
			this.PromptBar.Label[1].text = "Exit";
			this.PromptBar.Label[5].text = "Choose";
			this.PromptBar.UpdateButtons();

			this.FavorMenu.SetActive(true);

			// [af] Added "gameObject" for C# compatibility.
			this.gameObject.SetActive(false);
		}
	}

	public void UpdateSchemeList()
	{
		// [af] Converted while loop to for loop.
		for (int ListID = 1; ListID < this.SchemeNames.Length; ListID++)
		{
			if (SchemeGlobals.GetSchemeStage(ListID) == 100)
			{
				UILabel schemeNameLabel = this.SchemeNameLabels[ListID];
				schemeNameLabel.color = new Color(
					schemeNameLabel.color.r,
					schemeNameLabel.color.g,
					schemeNameLabel.color.b,
					0.50f);

				this.Exclamations[ListID].enabled = false;
				this.SchemeCostLabels[ListID].text = string.Empty;
			}
			else
			{
				// [af] Converted if/else statement to ternary expression.
				this.SchemeCostLabels[ListID].text = !SchemeGlobals.GetSchemeUnlocked(ListID) ?
					this.SchemeCosts[ListID].ToString() : string.Empty;

				if (SchemeGlobals.GetSchemeStage(ListID) >
					SchemeGlobals.GetSchemePreviousStage(ListID))
				{
					SchemeGlobals.SetSchemePreviousStage(ListID, 
						SchemeGlobals.GetSchemeStage(ListID));

					this.Exclamations[ListID].enabled = true;
				}
				else
				{
					this.Exclamations[ListID].enabled = false;
				}
			}
		}
	}

	public void UpdateSchemeInfo()
	{
		if (SchemeGlobals.GetSchemeStage(this.ID) != 100)
		{
			if (!SchemeGlobals.GetSchemeUnlocked(this.ID))
			{
				this.Arrow.gameObject.SetActive(false);

				// [af] Replaced if/else statement with ternary expression.
				this.PromptBar.Label[0].text = (this.Inventory.PantyShots >= this.SchemeCosts[this.ID]) ?
					"Purchase" : string.Empty;

				this.PromptBar.UpdateButtons();
			}
			else
			{
				if (SchemeGlobals.CurrentScheme == this.ID)
				{
					this.Arrow.gameObject.SetActive(true);
					this.Arrow.localPosition = new Vector3(
						this.Arrow.localPosition.x,
						-10.0f - (20.0f * SchemeGlobals.GetSchemeStage(this.ID)),
						this.Arrow.localPosition.z);

					this.PromptBar.Label[0].text = "Stop Tracking";
					this.PromptBar.UpdateButtons();
				}
				else
				{
					this.Arrow.gameObject.SetActive(false);

					this.PromptBar.Label[0].text = "Start Tracking";
					this.PromptBar.UpdateButtons();
				}
			}
		}
		else
		{
			this.PromptBar.Label[0].text = string.Empty;
			this.PromptBar.UpdateButtons();
		}

		this.Highlight.localPosition = new Vector3(
			this.Highlight.localPosition.x,
			200.0f - (25.0f * this.ID),
			this.Highlight.localPosition.z);

		this.SchemeIcon.mainTexture = this.SchemeIcons[this.ID];
		this.SchemeDesc.text = this.SchemeDescs[this.ID];

		if (SchemeGlobals.GetSchemeStage(this.ID) == 100)
		{
			this.SchemeInstructions.text = "This scheme is no longer available.";
		}
		else
		{
			// [af] Replaced if/else statement with ternary expression.
			this.SchemeInstructions.text = !SchemeGlobals.GetSchemeUnlocked(this.ID) ?
				("Skills Required:" + "\n" + this.SchemeSkills[this.ID]) : 
				this.SchemeSteps[this.ID];
		}

		this.UpdatePantyCount();
	}

	public void UpdatePantyCount()
	{
		this.PantyCount.text = this.Inventory.PantyShots.ToString();
	}

	public GameObject HUDIcon;
	public UILabel HUDInstructions;

	public void UpdateInstructions()
	{
		this.Steps = this.SchemeSteps[SchemeGlobals.CurrentScheme].Split('\n');

		if (SchemeGlobals.CurrentScheme > 0)
		{
			if (SchemeGlobals.GetSchemeStage(SchemeGlobals.CurrentScheme) < 100)
			{
				this.HUDIcon.SetActive(true);
				this.HUDInstructions.text = this.Steps[
					SchemeGlobals.GetSchemeStage(SchemeGlobals.CurrentScheme) - 1].ToString();
			}
			else
			{
				this.Arrow.gameObject.SetActive(false);
				this.HUDIcon.gameObject.SetActive(false);
				this.HUDInstructions.text = string.Empty;

				SchemeGlobals.CurrentScheme = 0;
			}
		}
		else
		{
			this.HUDIcon.SetActive(false);
			this.HUDInstructions.text = string.Empty;
		}
	}

	public void UpdateSchemeDestinations()
	{
		if (this.StudentManager.Students[this.StudentManager.RivalID] != null)
		{
			this.Scheme1Destinations[3] = this.StudentManager.Students[this.StudentManager.RivalID].transform;
			this.Scheme1Destinations[7] = this.StudentManager.Students[this.StudentManager.RivalID].transform;

			this.Scheme4Destinations[5] = this.StudentManager.Students[this.StudentManager.RivalID].transform;
			this.Scheme4Destinations[6] = this.StudentManager.Students[this.StudentManager.RivalID].transform;
		}

		if (this.StudentManager.Students[2] != null)
		{
			this.Scheme2Destinations[1] = this.StudentManager.Students[2].transform;
		}

		if (this.StudentManager.Students[97] != null)
		{
			this.Scheme5Destinations[3] = this.StudentManager.Students[97].transform;
		}

             if (SchemeGlobals.CurrentScheme == 1){this.SchemeDestinations = this.Scheme1Destinations;}
		else if (SchemeGlobals.CurrentScheme == 2){this.SchemeDestinations = this.Scheme2Destinations;}
		else if (SchemeGlobals.CurrentScheme == 3){this.SchemeDestinations = this.Scheme3Destinations;}
		else if (SchemeGlobals.CurrentScheme == 4){this.SchemeDestinations = this.Scheme4Destinations;}
		else if (SchemeGlobals.CurrentScheme == 5){this.SchemeDestinations = this.Scheme5Destinations;}
	}
}