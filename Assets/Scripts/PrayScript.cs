using UnityEngine;

public class PrayScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public WeaponManagerScript WeaponManager;
	public InputManagerScript InputManager;
	public PromptBarScript PromptBar;
	public StudentScript Student;
	public YandereScript Yandere;
	public PoliceScript Police;

	public UILabel SanityLabel;
	public UILabel VictimLabel;

	public PromptScript GenderPrompt;
	public PromptScript Prompt;

	public Transform PrayWindow;
	public Transform SummonSpot;
	public Transform Highlight;

	public Transform[] WeaponSpot;
	public GameObject[] Weapon;

	public GameObject FemaleTurtle;

	public int StudentNumber = 0;
	public int StudentID = 0;
	public int Selected = 0;
	public int Victims = 0;
	public int Uses = 0;

	public bool FemaleVictimChecked = false;
	public bool MaleVictimChecked = false;
	public bool JustSummoned = false;
	public bool SpawnMale = false;
	public bool Show = false;

	void Start()
	{
		if (StudentGlobals.GetStudentDead(39))
		{
			this.VictimLabel.color = new Color(
				this.VictimLabel.color.r,
				this.VictimLabel.color.g,
				this.VictimLabel.color.b,
				0.50f);
		}

		this.PrayWindow.localScale = Vector3.zero;

		if (MissionModeGlobals.MissionMode || GameGlobals.AlphabetMode)
		{
			this.Disable();
		}

#if UNITY_EDITOR
		this.GenderPrompt.gameObject.SetActive(true);
		this.Prompt.enabled = true;
		this.enabled = true;
#endif

		if (GameGlobals.LoveSick || GameGlobals.AlphabetMode)
		{
			this.Disable();
		}
	}

	void Disable()
	{
		this.GenderPrompt.gameObject.SetActive(false);
		this.enabled = false;

		this.Prompt.enabled = false;
		this.Prompt.Hide();
	}

	void Update()
	{
		if (!this.FemaleVictimChecked)
		{
			if (this.StudentManager.Students[39] != null)
			{
				if (!this.StudentManager.Students[39].Alive)
				{
					this.FemaleVictimChecked = true;
					this.Victims++;
				}
			}
		}
		else
		{
			if (this.StudentManager.Students[39] == null)
			{
				this.FemaleVictimChecked = false;
				this.Victims--;
			}
		}

		if (!this.MaleVictimChecked)
		{
			if (this.StudentManager.Students[37] != null)
			{
				if (!this.StudentManager.Students[37].Alive)
				{
					this.MaleVictimChecked = true;
					this.Victims++;
				}
			}
		}
		else
		{
			if (this.StudentManager.Students[37] == null)
			{
				this.MaleVictimChecked = false;
				this.Victims--;
			}
		}

		if (this.JustSummoned)
		{
			this.StudentManager.UpdateMe(this.StudentID);
			this.JustSummoned = false;
		}

		if (this.GenderPrompt.Circle[0].fillAmount == 0.0f)
		{
			this.GenderPrompt.Circle[0].fillAmount = 1.0f;

			if (!this.SpawnMale)
			{
				// [af] Replaced if/else statement with ternary expression.
				this.VictimLabel.color = new Color(
					this.VictimLabel.color.r,
					this.VictimLabel.color.g,
					this.VictimLabel.color.b,
					StudentGlobals.GetStudentDead(37) ? 0.50f : 1.0f);

				this.GenderPrompt.Label[0].text = "     " + "Male Victim";
				this.SpawnMale = true;
			}
			else
			{
				// [af] Replaced if/else statement with ternary expression.
				this.VictimLabel.color = new Color(
					this.VictimLabel.color.r,
					this.VictimLabel.color.g,
					this.VictimLabel.color.b,
					StudentGlobals.GetStudentDead(39) ? 0.50f : 1.0f);

				this.GenderPrompt.Label[0].text = "     " + "Female Victim";
				this.SpawnMale = false;
			}
		}

		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Prompt.Circle[0].fillAmount = 1.0f;

			if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
				this.Yandere.TargetStudent = this.Student;

				this.StudentManager.DisablePrompts();

				this.PrayWindow.gameObject.SetActive(true);
				this.Show = true;

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

				// [af] Replaced if/else statement with ternary expression.
				this.StudentNumber = this.SpawnMale ? 37 : 39;

				if (this.StudentManager.Students[StudentNumber] != null)
				{
					// [af] Added "gameObject" for C# compatibility.
					if (!this.StudentManager.Students[StudentNumber].gameObject.activeInHierarchy)
					{
						this.VictimLabel.color = new Color(
							this.VictimLabel.color.r,
							this.VictimLabel.color.g,
							this.VictimLabel.color.b,
							0.50f);
					}
					else
					{
						this.VictimLabel.color = new Color(
							this.VictimLabel.color.r,
							this.VictimLabel.color.g,
							this.VictimLabel.color.b,
							1f);
					}
				}
			}
		}

		if (!this.Show)
		{
			if (this.PrayWindow.gameObject.activeInHierarchy)
			{
				if (this.PrayWindow.localScale.x > 0.10f)
				{
					this.PrayWindow.localScale = Vector3.Lerp(
						this.PrayWindow.localScale, Vector3.zero, Time.deltaTime * 10.0f);
				}
				else
				{
					this.PrayWindow.localScale = Vector3.zero;
					this.PrayWindow.gameObject.SetActive(false);
				}
			}
		}
		else
		{
			this.PrayWindow.localScale = Vector3.Lerp(
				this.PrayWindow.localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);

			if (this.InputManager.TappedUp)
			{
				this.Selected--;

				if (this.Selected == 7)
				{
					this.Selected = 6;
				}

				this.UpdateHighlight();
			}

			if (this.InputManager.TappedDown)
			{
				this.Selected++;

				if (this.Selected == 7)
				{
					this.Selected = 8;
				}

				this.UpdateHighlight();
			}

			if (Input.GetButtonDown(InputNames.Xbox_A))
			{
				if (this.Selected == 1)
				{
					if (!this.Yandere.SanityBased)
					{
						this.SanityLabel.text = "Disable Sanity Anims";
						this.Yandere.SanityBased = true;
					}
					else
					{
						this.SanityLabel.text = "Enable Sanity Anims";
						this.Yandere.SanityBased = false;
					}

					this.Exit();
				}
				else if (this.Selected == 2)
				{
					this.Yandere.Sanity -= 50.0f;

					this.Exit();
				}
				else if (this.Selected == 3)
				{
					if (this.VictimLabel.color.a == 1.0f)
					{
						if (this.StudentManager.NPCsSpawned >= this.StudentManager.NPCsTotal)
						{
							if (this.SpawnMale)
							{
								this.MaleVictimChecked = false;
								this.StudentID = 37;
							}
							else
							{
								this.FemaleVictimChecked = false;
								this.StudentID = 39;
							}

							if (this.StudentManager.Students[this.StudentID] != null)
							{
								Destroy(this.StudentManager.Students[this.StudentID].gameObject);
							}

							this.StudentManager.Students[this.StudentID] = null;
							this.StudentManager.ForceSpawn = true;
							this.StudentManager.SpawnPositions[this.StudentID] = this.SummonSpot;
							this.StudentManager.SpawnID = this.StudentID;
							this.StudentManager.SpawnStudent(this.StudentManager.SpawnID);
							this.StudentManager.SpawnID = 0;
							this.Police.Corpses -= this.Victims;
							this.Victims = 0;

							this.JustSummoned = true;
							this.Exit();
						}
					}
				}
				else if (this.Selected == 4)
				{
					this.SpawnWeapons();

					this.Exit();
				}
				else if (this.Selected == 5)
				{
					if (this.Yandere.Gloved)
					{
						this.Yandere.Gloves.Blood.enabled = false;
					}

					if (this.Yandere.CurrentUniformOrigin == 1)
					{						
						this.StudentManager.OriginalUniforms++;
					}
					else
					{
						this.StudentManager.NewUniforms++;
					}

					this.Police.BloodyClothing = 0;
					this.Yandere.Bloodiness = 0.0f;
					this.Yandere.Sanity = 100.0f;

					this.Exit();
				}
				else if (this.Selected == 6)
				{
					this.WeaponManager.CleanWeapons();

					this.Exit();
				}
				else if (this.Selected == 8)
				{
					this.Exit();
				}
			}
		}
	}

	void UpdateHighlight()
	{
		if (this.Selected < 1)
		{
			this.Selected = 8;
		}
		else if (this.Selected > 8)
		{
			this.Selected = 1;
		}

		this.Highlight.transform.localPosition = new Vector3(
			this.Highlight.transform.localPosition.x,
			225.0f - (50.0f * this.Selected),
			this.Highlight.transform.localPosition.z);
	}

	void Exit()
	{
		this.Selected = 1;
		this.UpdateHighlight();

		this.Yandere.ShoulderCamera.OverShoulder = false;
		this.StudentManager.EnablePrompts();
		this.Yandere.TargetStudent = null;

		this.PromptBar.ClearButtons();
		this.PromptBar.Show = false;

		this.Show = false;

		this.Uses++;

		if (this.Uses > 9)
		{
			this.FemaleTurtle.SetActive(true);
		}
	}

	public void SpawnWeapons()
	{
		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < 6; ID++)
		{
			if (this.Weapon[ID] != null)
			{
				this.Weapon[ID].transform.position = this.WeaponSpot[ID].position;
			}
		}
	}
}
