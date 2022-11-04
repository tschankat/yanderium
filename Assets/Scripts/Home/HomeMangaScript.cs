using UnityEngine;

public class HomeMangaScript : MonoBehaviour
{
	static readonly string[] SeductionStrings = new string[]
	{
		"Innocent",
		"Flirty",
		"Charming",
		"Sensual",
		"Seductive",
		"Succubus"
	};

	static readonly string[] NumbnessStrings = new string[]
	{
		"Stoic",
		"Somber",
		"Detached",
		"Unemotional",
		"Desensitized",
		"Dead Inside"
	};

	static readonly string[] EnlightenmentStrings = new string[]
	{
		"Asleep",
		"Awoken",
		"Mindful",
		"Informed",
		"Eyes Open",
		"Omniscient"
	};

	public InputManagerScript InputManager;
	public HomeYandereScript HomeYandere;
	public HomeCameraScript HomeCamera;
	public HomeWindowScript HomeWindow;
	public HomeDarknessScript Darkness;

	private GameObject NewManga;

	public GameObject ReadButtonGroup;
	public GameObject MysteryManga;
	public GameObject AreYouSure;
	public GameObject MangaGroup;

	public GameObject[] MangaList;

	public UILabel MangaNameLabel;
	public UILabel MangaDescLabel;
	public UILabel MangaBuffLabel;

	public UILabel RequiredLabel;
	public UILabel CurrentLabel;
	public UILabel ButtonLabel;

	public Transform MangaParent;

	public bool DestinationReached = false;

	public float TargetRotation = 0.0f;
	public float Rotation = 0.0f;

	public int TotalManga = 0;
	public int Selected = 0;

	public string Title = string.Empty;

	// [af] Removed "ID" member; replaced with local loop variable.

	public GameObject[] MangaModels;
	public string[] MangaNames;
	public string[] MangaDescs;
	public string[] MangaBuffs;

	void Start()
	{
		this.UpdateCurrentLabel();

		// [af] Converted while loop to for loop.
		for (int ID = 0; ID < this.TotalManga; ID++)
		{
			if (CollectibleGlobals.GetMangaCollected(ID + 1))
			{
				this.NewManga = Instantiate(this.MangaModels[ID],
					new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1.0f),
					Quaternion.identity);
			}
			else
			{
				this.NewManga = Instantiate(this.MysteryManga,
					new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1.0f),
					Quaternion.identity);
			}

			this.NewManga.transform.parent = this.MangaParent;

			this.NewManga.GetComponent<HomeMangaBookScript>().Manga = this;
			this.NewManga.GetComponent<HomeMangaBookScript>().ID = ID;

			this.NewManga.transform.localScale = new Vector3(1.45f, 1.45f, 1.45f);

			this.MangaParent.transform.localEulerAngles = new Vector3(
				this.MangaParent.transform.localEulerAngles.x,
				this.MangaParent.transform.localEulerAngles.y + (360.0f / this.TotalManga),
				this.MangaParent.transform.localEulerAngles.z);

			this.MangaList[ID] = this.NewManga;
		}

		this.MangaParent.transform.localEulerAngles = new Vector3(
			this.MangaParent.transform.localEulerAngles.x,
			0.0f,
			this.MangaParent.transform.localEulerAngles.z);

		this.MangaParent.transform.localPosition = new Vector3(
			this.MangaParent.transform.localPosition.x,
			this.MangaParent.transform.localPosition.y,
			1.80f);

		this.UpdateMangaLabels();

		this.MangaParent.transform.localScale = Vector3.zero;
		this.MangaParent.gameObject.SetActive(false);
	}

	void Update()
	{
		if (this.HomeWindow.Show)
		{
			if (!this.AreYouSure.activeInHierarchy)
			{
				this.MangaParent.localScale = Vector3.Lerp(
					this.MangaParent.localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
				this.MangaParent.gameObject.SetActive(true);

				if (this.InputManager.TappedRight)
				{
					this.DestinationReached = false;

					this.TargetRotation += 360.0f / this.TotalManga;
					this.Selected++;

					if (this.Selected > (this.TotalManga - 1))
					{
						this.Selected = 0;
					}

					this.UpdateMangaLabels();
					this.UpdateCurrentLabel();
				}

				if (this.InputManager.TappedLeft)
				{
					this.DestinationReached = false;

					this.TargetRotation -= 360.0f / this.TotalManga;
					this.Selected--;

					if (this.Selected < 0)
					{
						this.Selected = this.TotalManga - 1;
					}

					this.UpdateMangaLabels();
					this.UpdateCurrentLabel();
				}

				this.Rotation = Mathf.Lerp(this.Rotation, this.TargetRotation, Time.deltaTime * 10.0f);

				this.MangaParent.localEulerAngles = new Vector3(
					this.MangaParent.localEulerAngles.x,
					this.Rotation,
					this.MangaParent.localEulerAngles.z);

				if (Input.GetButtonDown(InputNames.Xbox_A))
				{
					if (this.ReadButtonGroup.activeInHierarchy)
					{
						this.MangaGroup.SetActive(false);
						this.AreYouSure.SetActive(true);
					}
				}

				if (Input.GetKeyDown(KeyCode.S))
				{
					PlayerGlobals.Seduction++;
					PlayerGlobals.Numbness++;
					PlayerGlobals.Enlightenment++;

					if (PlayerGlobals.Seduction > 5)
					{
						PlayerGlobals.Seduction = 0;
						PlayerGlobals.Numbness = 0;
						PlayerGlobals.Enlightenment = 0;
					}

					this.UpdateCurrentLabel();
					this.UpdateMangaLabels();
				}

				if (Input.GetButtonDown(InputNames.Xbox_B))
				{
					this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
					this.HomeCamera.Target = this.HomeCamera.Targets[0];
					this.HomeYandere.CanMove = true;
					this.HomeWindow.Show = false;
				}

				if (Input.GetKeyDown(KeyCode.Space))
				{
					// [af] Converted while loop to for loop.
					for (int ID = 0; ID < this.TotalManga; ID++)
					{
						CollectibleGlobals.SetMangaCollected(ID + 1, true);
					}
				}
			}
			else
			{
				if (Input.GetButtonDown(InputNames.Xbox_A))
				{
					if (this.Selected < 5)
					{
						PlayerGlobals.Seduction++;
					}
					else if (this.Selected < 10)
					{
						PlayerGlobals.Numbness++;
					}
					else
					{
						PlayerGlobals.Enlightenment++;
					}

					//HomeGlobals.LateForSchool = true;
					this.AreYouSure.SetActive(false);
					this.Darkness.FadeOut = true;
				}

				if (Input.GetButtonDown(InputNames.Xbox_B))
				{
					this.MangaGroup.SetActive(true);
					this.AreYouSure.SetActive(false);
				}
			}
		}
		else
		{
			this.MangaParent.localScale = Vector3.Lerp(
				this.MangaParent.localScale, Vector3.zero, Time.deltaTime * 10.0f);

			if (this.MangaParent.localScale.x < 0.010f)
			{
				this.MangaParent.gameObject.SetActive(false);
			}
		}
	}

	void UpdateMangaLabels()
	{
		if (this.Selected < 5)
		{
			// [af] Replaced if/else statement with boolean expression.
			this.ReadButtonGroup.SetActive(PlayerGlobals.Seduction == this.Selected);

			if (CollectibleGlobals.GetMangaCollected(this.Selected + 1))
			{
				if (PlayerGlobals.Seduction > this.Selected)
				{
					this.RequiredLabel.text = "You have already read this manga.";
				}
				else
				{
					this.RequiredLabel.text = "Required Seduction Level: " + this.Selected.ToString();
				}
			}
			else
			{
				this.RequiredLabel.text = "You have not yet collected this manga.";
				this.ReadButtonGroup.SetActive(false);
			}
		}
		else if (this.Selected < 10)
		{
			// [af] Replaced if/else statement with boolean expression.
			this.ReadButtonGroup.SetActive(PlayerGlobals.Numbness == (this.Selected - 5));

			if (CollectibleGlobals.GetMangaCollected(this.Selected + 1))
			{
				if (PlayerGlobals.Numbness > (this.Selected - 5))
				{
					this.RequiredLabel.text = "You have already read this manga.";
				}
				else
				{
					this.RequiredLabel.text = "Required Numbness Level: " + (this.Selected - 5).ToString();
				}
			}
			else
			{
				this.RequiredLabel.text = "You have not yet collected this manga.";
				this.ReadButtonGroup.SetActive(false);
			}
		}
		else
		{
			// [af] Replaced if/else statement with boolean expression.
			this.ReadButtonGroup.SetActive(PlayerGlobals.Enlightenment == (this.Selected - 10));

			if (CollectibleGlobals.GetMangaCollected(this.Selected + 1))
			{
				if (PlayerGlobals.Enlightenment > (this.Selected - 10))
				{
					this.RequiredLabel.text = "You have already read this manga.";
				}
				else
				{
					this.RequiredLabel.text = "Required Enlightenment Level: " + (this.Selected - 10).ToString();
				}
			}
			else
			{
				this.RequiredLabel.text = "You have not yet collected this manga.";
				this.ReadButtonGroup.SetActive(false);
			}
		}

		if (CollectibleGlobals.GetMangaCollected(this.Selected + 1))
		{
			this.MangaNameLabel.text = this.MangaNames[this.Selected];
			this.MangaDescLabel.text = this.MangaDescs[this.Selected];
			this.MangaBuffLabel.text = this.MangaBuffs[this.Selected];
		}
		else
		{
			this.MangaNameLabel.text = "?????";
			this.MangaDescLabel.text = "?????";
			this.MangaBuffLabel.text = "?????";
		}
	}

	void UpdateCurrentLabel()
	{
		// [af] Replaced several if/else statements with array look-ups.
		if (this.Selected < 5)
		{
			this.Title = SeductionStrings[PlayerGlobals.Seduction];
			this.CurrentLabel.text = "Current Seduction Level: " +
				PlayerGlobals.Seduction.ToString() + " (" + this.Title + ")"; ;
		}
		else if (this.Selected < 10)
		{
			this.Title = NumbnessStrings[PlayerGlobals.Numbness];
			this.CurrentLabel.text = "Current Numbness Level: " +
				PlayerGlobals.Numbness.ToString() + " (" + this.Title + ")";
		}
		else
		{
			this.Title = EnlightenmentStrings[PlayerGlobals.Enlightenment];
			this.CurrentLabel.text = "Current Enlightenment Level: " +
				PlayerGlobals.Enlightenment.ToString() + " (" + this.Title + ")";
		}
	}
}
