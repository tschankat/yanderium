using UnityEngine;

public class ComputerGamesScript : MonoBehaviour
{
	public PromptScript[] ComputerGames;
	public Collider[] Chairs;

	public StudentManagerScript StudentManager;
	public InputManagerScript InputManager;
	public PromptBarScript PromptBar;
	public YandereScript Yandere;
	public PoliceScript Police;
	public PoisonScript Poison;

	public Quaternion targetRotation;

	public Transform GameWindow;
	public Transform MainCamera;
	public Transform Highlight;

	public bool ShowWindow = false;
	public bool Gaming = false;

	public float Timer = 0.0f;

	public int Subject = 1;
	public int GameID = 0;
	public int ID = 1;

	public Color OriginalColor;

	public string[] Descriptions;

	public UITexture MyTexture;

	public Texture[] Textures;

	public UILabel DescLabel;

	void Start()
	{
		this.GameWindow.gameObject.SetActive(false);

		this.DeactivateAllBenefits();

		this.OriginalColor = this.Yandere.PowerUp.color;

		if (ClubGlobals.Club == ClubType.Gaming)
		{
			this.EnableGames();
		}
		else
		{
			this.DisableGames();
		}
	}

	void Update()
	{
		if (this.ShowWindow)
		{
			this.GameWindow.localScale = Vector3.Lerp(
				this.GameWindow.localScale,
				new Vector3(1.0f, 1.0f, 1.0f),
				Time.deltaTime * 10.0f);

			if (this.InputManager.TappedUp)
			{
				this.Subject--;
				this.UpdateHighlight();
			}
			else if (this.InputManager.TappedDown)
			{
				this.Subject++;
				this.UpdateHighlight();
			}

			if (Input.GetButtonDown(InputNames.Xbox_A))
			{
				this.ShowWindow = false;
				this.PlayGames();

				this.PromptBar.ClearButtons();
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = false;
			}

			if (Input.GetButtonDown(InputNames.Xbox_B))
			{
				this.Yandere.CanMove = true;
				this.ShowWindow = false;

				this.PromptBar.ClearButtons();
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = false;
			}
		}
		else
		{
			if (this.GameWindow.localScale.x > 0.10f)
			{
				this.GameWindow.localScale = Vector3.Lerp(
					this.GameWindow.localScale, Vector3.zero, Time.deltaTime * 10.0f);
			}
			else
			{
				this.GameWindow.localScale = Vector3.zero;
				this.GameWindow.gameObject.SetActive(false);
			}
		}

		if (this.Gaming)
		{
			this.targetRotation = Quaternion.LookRotation(new Vector3(
				this.ComputerGames[this.GameID].transform.position.x,
				this.Yandere.transform.position.y,
				this.ComputerGames[this.GameID].transform.position.z) - this.Yandere.transform.position);
			this.Yandere.transform.rotation = Quaternion.Slerp(
				this.Yandere.transform.rotation, this.targetRotation, Time.deltaTime * 10.0f);

			this.Yandere.MoveTowardsTarget(new Vector3(24.32233f, 4, 12.58998f));

			this.Timer += Time.deltaTime;

			if (this.Timer > 5.0f)
			{
				this.Yandere.PowerUp.transform.parent.gameObject.SetActive(true);
				this.Yandere.MyController.radius = 0.20f;
				this.Yandere.CanMove = true;
				this.Yandere.EmptyHands();
				this.Gaming = false;

				this.ActivateBenefit();
				//this.EnableChairs();
			}
		}
		else
		{
			if (this.Timer < 5.0f)
			{
				for (this.ID = 1; this.ID < this.ComputerGames.Length; this.ID++)
				{
					PromptScript gamePrompt = this.ComputerGames[this.ID];

					if (gamePrompt.Circle[0].fillAmount == 0.0f)
					{
						gamePrompt.Circle[0].fillAmount = 1.0f;

						if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
						{
							this.GameID = this.ID;

							if (this.ID == 1)
							{
								this.PromptBar.ClearButtons();
								this.PromptBar.Label[0].text = "Confirm";
								this.PromptBar.Label[1].text = "Back";
								this.PromptBar.Label[4].text = "Select";
								this.PromptBar.UpdateButtons();
								this.PromptBar.Show = true;

								this.Yandere.Character.GetComponent<Animation>().Play(this.Yandere.IdleAnim);
								this.Yandere.CanMove = false;

								this.GameWindow.gameObject.SetActive(true);
								this.ShowWindow = true;
							}
							else
							{
								this.PlayGames();
							}
						}
					}
				}
			}
		}

		if (this.Yandere.PowerUp.gameObject.activeInHierarchy)
		{
			this.Timer += Time.deltaTime;

			this.Yandere.PowerUp.transform.localPosition = new Vector3(
				this.Yandere.PowerUp.transform.localPosition.x,
				this.Yandere.PowerUp.transform.localPosition.y + Time.deltaTime,
				this.Yandere.PowerUp.transform.localPosition.z);

			this.Yandere.PowerUp.transform.LookAt(this.MainCamera.position);

			this.Yandere.PowerUp.transform.localEulerAngles = new Vector3(
				this.Yandere.PowerUp.transform.localEulerAngles.x,
				this.Yandere.PowerUp.transform.localEulerAngles.y + 180.0f,
				this.Yandere.PowerUp.transform.localEulerAngles.z);

			if (this.Yandere.PowerUp.color != new Color(1.0f, 1.0f, 1.0f, 1.0f))
			{
				this.Yandere.PowerUp.color = this.OriginalColor;
			}
			else
			{
				this.Yandere.PowerUp.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			}

			if (this.Timer > 6.0f)
			{
				this.Yandere.PowerUp.transform.parent.gameObject.SetActive(false);
				this.gameObject.SetActive(false);
			}
		}
	}

	public void EnableGames()
	{
		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < this.ComputerGames.Length; ID++)
		{
			this.ComputerGames[ID].enabled = true;
		}

		this.gameObject.SetActive(true);
	}

	void PlayGames()
	{
		this.Yandere.Character.GetComponent<Animation>().CrossFade(AnimNames.FemalePlayingGames00);
		this.Yandere.MyController.radius = 0.10f;
		this.Yandere.CanMove = false;

		this.Gaming = true;

		//this.DisableChairs();
		this.DisableGames();
		this.UpdateImage();
	}

	void UpdateImage()
	{
		MyTexture.mainTexture = Textures[Subject];
	}

	public void DisableGames()
	{
		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < this.ComputerGames.Length; ID++)
		{
			this.ComputerGames[ID].enabled = false;
			this.ComputerGames[ID].Hide();
		}

		if (!this.Gaming)
		{
			this.gameObject.SetActive(false);
		}
	}

	void EnableChairs()
	{
		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < this.Chairs.Length; ID++)
		{
			this.Chairs[ID].enabled = true;
		}

		this.gameObject.SetActive(true);
	}

	void DisableChairs()
	{
		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < this.Chairs.Length; ID++)
		{
			this.Chairs[ID].enabled = false;
		}
	}

	// Educational Game: Improves Biology / Chemistry / Language / Psychology.
	// Fighting Game: Improves Physical.
	// Dating Game: Improves Seduction.
	// Horror Game: Improves Numbness.
	// Role-Playing Game: Improves Social.
	// Stealth Game: Improves Stealth.
	// Racing Game: Improves Speed.
	// Lunar Scythe: Improves Enlightenment.

	void ActivateBenefit()
	{
		//Biology
		if (this.Subject == 1)
		{
			this.Yandere.Class.BiologyBonus = 1;
		}
		//Chemistry
		else if (this.Subject == 2)
		{
			this.Yandere.Class.ChemistryBonus = 1;
		}
		//LanguageBonus
		else if (this.Subject == 3)
		{
			this.Yandere.Class.LanguageBonus = 1;
		}
		//Psychology
		else if (this.Subject == 4)
		{
			this.Yandere.Class.PsychologyBonus = 1;
		}
		// Physical.
		else if (this.Subject == 5)
		{
			this.Yandere.Class.PhysicalBonus = 1;
		}
		// Seduction.
		else if (this.Subject == 6)
		{
            this.Yandere.Class.SeductionBonus = 1;
		}
		// Numbness.
		else if (this.Subject == 7)
		{
            this.Yandere.Class.NumbnessBonus = 1;
		}
		// Social.
		else if (this.Subject == 8)
		{
            this.Yandere.Class.SocialBonus = 1;
		}
		// Stealth.
		else if (this.Subject == 9)
		{
            this.Yandere.Class.StealthBonus = 1;
		}
		// Speed.
		else if (this.Subject == 10)
		{
            this.Yandere.Class.SpeedBonus = 1;
		}
		// Enlightenment.
		else if (this.Subject == 11)
		{
            this.Yandere.Class.EnlightenmentBonus = 1;
		}

		if (this.Poison != null)
		{
			this.Poison.Start();
		}

		this.StudentManager.UpdatePerception();
		this.Yandere.UpdateNumbness();
		this.Police.UpdateCorpses();
	}

	void DeactivateBenefit()
	{
		//Biology
		if (this.Subject == 1)
		{
			this.Yandere.Class.BiologyBonus = 0;
		}
		//Chemistry
		else if (this.Subject == 2)
		{
			this.Yandere.Class.ChemistryBonus = 0;
		}
		//LanguageBonus
		else if (this.Subject == 3)
		{
			this.Yandere.Class.LanguageBonus = 0;
		}
		//Psychology
		else if (this.Subject == 4)
		{
			this.Yandere.Class.PsychologyBonus = 0;
		}
		// Physical.
		else if (this.Subject == 5)
		{
			this.Yandere.Class.PhysicalBonus = 0;
		}
		// Seduction.
		else if (this.Subject == 6)
		{
            this.Yandere.Class.SeductionBonus = 0;
		}
		// Numbness.
		else if (this.Subject == 7)
		{
            this.Yandere.Class.NumbnessBonus = 0;
		}
		// Social.
		else if (this.Subject == 8)
		{
            this.Yandere.Class.SocialBonus = 0;
		}
		// Stealth.
		else if (this.Subject == 9)
		{
            this.Yandere.Class.StealthBonus = 0;
		}
		// Speed.
		else if (this.Subject == 10)
		{
            this.Yandere.Class.SpeedBonus = 0;
		}
		// Enlightenment.
		else if (this.Subject == 11)
		{
            this.Yandere.Class.EnlightenmentBonus = 0;
		}

		if (this.Poison != null)
		{
			this.Poison.Start();
		}

		this.StudentManager.UpdatePerception();
		this.Yandere.UpdateNumbness();
		this.Police.UpdateCorpses();
	}

	public void DeactivateAllBenefits()
	{
		this.Yandere.Class.BiologyBonus = 0;
		this.Yandere.Class.ChemistryBonus = 0;
		this.Yandere.Class.LanguageBonus = 0;
		this.Yandere.Class.PsychologyBonus = 0;
		this.Yandere.Class.PhysicalBonus = 0;
        this.Yandere.Class.SeductionBonus = 0;
        this.Yandere.Class.NumbnessBonus = 0;
        this.Yandere.Class.SocialBonus = 0;
        this.Yandere.Class.StealthBonus = 0;
        this.Yandere.Class.SpeedBonus = 0;
        this.Yandere.Class.EnlightenmentBonus = 0;

		if (this.Poison != null)
		{
			this.Poison.Start();
		}
	}

	void UpdateHighlight()
	{
		if (this.Subject < 1)
		{
			this.Subject = 11;
		}
		else if (this.Subject > 11)
		{
			this.Subject = 1;
		}

		this.Highlight.localPosition = new Vector3(
			this.Highlight.localPosition.x,
			250.0f - (this.Subject * 50.0f),
			this.Highlight.localPosition.z);

		this.DescLabel.text = Descriptions[this.Subject];
	}
}
