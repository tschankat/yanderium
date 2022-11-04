using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomePrisonerScript : MonoBehaviour
{
	public InputManagerScript InputManager;
	public HomePrisonerChanScript Prisoner;
	public HomeYandereScript HomeYandere;
	public HomeCameraScript HomeCamera;
	public HomeWindowScript HomeWindow;
	public HomeDarknessScript Darkness;

	public UILabel[] OptionLabels;
	public string[] Descriptions;

	public Transform TortureDestination;
	public Transform TortureTarget;
	public GameObject NowLoading;
	public Transform Highlight;
	public AudioSource Jukebox;

	public UILabel SanityLabel;
	public UILabel DescLabel;
	public UILabel Subtitle;

	public bool PlayedAudio = false;
	public bool ZoomIn = false;

	public float Sanity = 100.0f;
	public float Timer = 0.0f;

	public int ID = 1;

	public AudioClip FirstTorture;
	public AudioClip Under50Torture;
	public AudioClip Over50Torture;
	public AudioClip TortureHit;

	public string[] FullSanityBanterText;
	public string[] HighSanityBanterText;
	public string[] LowSanityBanterText;
	public string[] NoSanityBanterText;
	public string[] BanterText;

	public AudioClip[] FullSanityBanter;
	public AudioClip[] HighSanityBanter;
	public AudioClip[] LowSanityBanter;
	public AudioClip[] NoSanityBanter;
	public AudioClip[] Banter;

	public float BanterTimer = 0.0f;
	public bool Bantering = false;
	public int BanterID = 0;

	void Start()
	{
		this.Sanity = StudentGlobals.GetStudentSanity(SchoolGlobals.KidnapVictim);
		this.SanityLabel.text = "Sanity: " + this.Sanity.ToString() + "%";
		this.Prisoner.Sanity = this.Sanity;
		this.Subtitle.text = string.Empty;

		if (this.Sanity == 100.0f)
		{
			this.BanterText = this.FullSanityBanterText;
			this.Banter = this.FullSanityBanter;
		}
		else if (this.Sanity >= 50.0f)
		{
			this.BanterText = this.HighSanityBanterText;
			this.Banter = this.HighSanityBanter;
		}
		else if (this.Sanity == 0.0f)
		{
			this.BanterText = this.NoSanityBanterText;
			this.Banter = this.NoSanityBanter;
		}
		else
		{
			this.BanterText = this.LowSanityBanterText;
			this.Banter = this.LowSanityBanter;
		}

		if (this.Sanity < 100.0f)
		{
			this.Prisoner.Character.GetComponent<Animation>().CrossFade(AnimNames.FemaleKidnapIdle02);
		}

		if (!HomeGlobals.Night)
		{
			UILabel optionsLabel2 = this.OptionLabels[2];
			optionsLabel2.color = new Color(
				optionsLabel2.color.r,
				optionsLabel2.color.g,
				optionsLabel2.color.b,
				0.50f);

			if (HomeGlobals.LateForSchool)
			{
				UILabel optionsLabel1 = this.OptionLabels[1];
				optionsLabel1.color = new Color(
					optionsLabel1.color.r,
					optionsLabel1.color.g,
					optionsLabel1.color.b,
					0.50f);
			}

			if (DateGlobals.Weekday == DayOfWeek.Friday)
			{
				UILabel optionsLabel3 = this.OptionLabels[3];
				optionsLabel3.color = new Color(
					optionsLabel3.color.r,
					optionsLabel3.color.g,
					optionsLabel3.color.b,
					0.50f);

				UILabel optionsLabel4 = this.OptionLabels[4];
				optionsLabel4.color = new Color(
					optionsLabel4.color.r,
					optionsLabel4.color.g,
					optionsLabel4.color.b,
					0.50f);
			}
		}
		else
		{
			UILabel optionsLabel1 = this.OptionLabels[1];
			optionsLabel1.color = new Color(
				optionsLabel1.color.r,
				optionsLabel1.color.g,
				optionsLabel1.color.b,
				0.50f);

			UILabel optionsLabel3 = this.OptionLabels[3];
			optionsLabel3.color = new Color(
				optionsLabel3.color.r,
				optionsLabel3.color.g,
				optionsLabel3.color.b,
				0.50f);

			UILabel optionsLabel4 = this.OptionLabels[4];
			optionsLabel4.color = new Color(
				optionsLabel4.color.r,
				optionsLabel4.color.g,
				optionsLabel4.color.b,
				0.50f);
		}

		if (Sanity > 0)
		{
			OptionLabels[5].text = "?????";

			UILabel optionsLabel5 = this.OptionLabels[5];
			optionsLabel5.color = new Color(
				optionsLabel5.color.r,
				optionsLabel5.color.g,
				optionsLabel5.color.b,
				0.50f);
		}
		else
		{
			OptionLabels[5].text = "Bring to School";

			UILabel optionsLabel1 = this.OptionLabels[1];
			optionsLabel1.color = new Color(
				optionsLabel1.color.r,
				optionsLabel1.color.g,
				optionsLabel1.color.b,
				0.50f);

			UILabel optionsLabel2 = this.OptionLabels[2];
			optionsLabel2.color = new Color(
				optionsLabel2.color.r,
				optionsLabel2.color.g,
				optionsLabel2.color.b,
				0.50f);

			UILabel optionsLabel3 = this.OptionLabels[3];
			optionsLabel3.color = new Color(
				optionsLabel3.color.r,
				optionsLabel3.color.g,
				optionsLabel3.color.b,
				0.50f);

			UILabel optionsLabel4 = this.OptionLabels[4];
			optionsLabel4.color = new Color(
				optionsLabel4.color.r,
				optionsLabel4.color.g,
				optionsLabel4.color.b,
				0.50f);

			UILabel optionsLabel5 = this.OptionLabels[5];
			optionsLabel5.color = new Color(
				optionsLabel5.color.r,
				optionsLabel5.color.g,
				optionsLabel5.color.b,
				1.0f);

			if (HomeGlobals.Night)
			{
				optionsLabel5.color = new Color(
					optionsLabel5.color.r,
					optionsLabel5.color.g,
					optionsLabel5.color.b,
					0.50f);
			}
		}

		this.UpdateDesc();

		if (SchoolGlobals.KidnapVictim == 0)
		{
			this.enabled = false;
		}
	}

	void Update()
	{
		AudioSource audioSource = this.GetComponent<AudioSource>();

		if (Vector3.Distance(this.HomeYandere.transform.position, this.Prisoner.transform.position) < 2.0f)
		{
			if (this.HomeYandere.CanMove)
			{
				this.BanterTimer += Time.deltaTime;

				if (this.BanterTimer > 5.0f)
				{
					if (!this.Bantering)
					{
						this.Bantering = true;

						if (this.BanterID < (this.Banter.Length - 1))
						{
							this.BanterID++;

							this.Subtitle.text = this.BanterText[this.BanterID];
							audioSource.clip = this.Banter[this.BanterID];
							audioSource.Play();
						}
					}
				}
			}
		}

		if (this.Bantering)
		{
			if (!audioSource.isPlaying)
			{
				this.Subtitle.text = string.Empty;
				this.Bantering = false;
				this.BanterTimer = 0.0f;
			}
		}

		if (!this.HomeYandere.CanMove)
		{
			if ((this.HomeCamera.Destination == this.HomeCamera.Destinations[10]) ||
				(this.HomeCamera.Destination == this.TortureDestination))
			{
				if (this.InputManager.TappedDown)
				{
					this.ID++;

					if (this.ID > 5)
					{
						this.ID = 1;
					}

					this.Highlight.localPosition = new Vector3(
						this.Highlight.localPosition.x,
						465.0f - (this.ID * 40.0f),
						this.Highlight.localPosition.z);

					this.UpdateDesc();
				}

				if (this.InputManager.TappedUp)
				{
					this.ID--;

					if (this.ID < 1)
					{
						this.ID = 5;
					}

					this.Highlight.localPosition = new Vector3(
						this.Highlight.localPosition.x,
						465.0f - (this.ID * 40.0f),
						this.Highlight.localPosition.z);

					this.UpdateDesc();
				}

				if (Input.GetKeyDown(KeyCode.X))
				{
					this.Sanity -= 10.0f;

					if (this.Sanity < 0.0f)
					{
						this.Sanity = 100.0f;
					}

					StudentGlobals.SetStudentSanity(SchoolGlobals.KidnapVictim, this.Sanity);
					this.SanityLabel.text = "Sanity: " + this.Sanity.ToString("f0") + "%";
					this.Prisoner.UpdateSanity();
				}

				if (!this.ZoomIn)
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						if (this.OptionLabels[ID].color.a == 1.0f)
						{
							if (this.Sanity > 0.0f)
							{
								if (this.Sanity == 100.0f)
								{
									this.Prisoner.Character.GetComponent<Animation>().CrossFade(AnimNames.FemaleKidnapTorture01);
								}
								else if (this.Sanity >= 50.0f)
								{
									this.Prisoner.Character.GetComponent<Animation>().CrossFade(AnimNames.FemaleKidnapTorture02);
								}
								else
								{
									this.Prisoner.Character.GetComponent<Animation>().CrossFade(AnimNames.FemaleKidnapSurrender);
									this.TortureDestination.localPosition = new Vector3(
										this.TortureDestination.localPosition.x, 0.0f, 1.0f);
									this.TortureTarget.localPosition = new Vector3(
										this.TortureTarget.localPosition.x,
										1.10f,
										this.TortureTarget.localPosition.z);
								}

								this.HomeCamera.Destination = this.TortureDestination;
								this.HomeCamera.Target = this.TortureTarget;
								this.HomeCamera.Torturing = true;
								this.Prisoner.Tortured = true;
								this.Prisoner.RightEyeRotOrigin.x = -6.0f;
								this.Prisoner.LeftEyeRotOrigin.x = 6.0f;
								this.ZoomIn = true;
							}
							else
							{
								this.Darkness.FadeOut = true;
							}

							this.HomeWindow.Show = false;
							this.HomeCamera.PromptBar.ClearButtons();
							this.HomeCamera.PromptBar.Show = false;

							this.Jukebox.volume -= 0.50f;
						}
					}

					if (Input.GetButtonDown(InputNames.Xbox_B))
					{
						this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
						this.HomeCamera.Target = this.HomeCamera.Targets[0];
						this.HomeCamera.PromptBar.ClearButtons();
						this.HomeCamera.PromptBar.Show = false;
						this.HomeYandere.CanMove = true;

						// [af] Added "gameObject" for C# compatibility.
						this.HomeYandere.gameObject.SetActive(true);

						this.HomeWindow.Show = false;
					}
				}
				else
				{
					this.TortureDestination.Translate(Vector3.forward * (Time.deltaTime * 0.020f));
					this.Jukebox.volume -= Time.deltaTime * 0.050f;
					this.Timer += Time.deltaTime;

					if (this.Sanity >= 50.0f)
					{
						this.TortureDestination.localPosition = new Vector3(
							this.TortureDestination.localPosition.x,
							this.TortureDestination.localPosition.y + (Time.deltaTime * 0.050f),
							this.TortureDestination.localPosition.z);

						if (this.Sanity == 100.0f)
						{
							if (this.Timer > 2.0f)
							{
								if (!this.PlayedAudio)
								{
									audioSource.clip = this.FirstTorture;
									this.PlayedAudio = true;
									audioSource.Play();
								}
							}
						}
						else
						{
							if (this.Timer > 1.50f)
							{
								if (!this.PlayedAudio)
								{
									audioSource.clip = this.Over50Torture;
									this.PlayedAudio = true;
									audioSource.Play();
								}
							}
						}
					}
					else
					{
						if (this.Timer > 5.0f)
						{
							if (!this.PlayedAudio)
							{
								audioSource.clip = this.Under50Torture;
								this.PlayedAudio = true;
								audioSource.Play();
							}
						}
					}

					if (this.Timer > 10.0f)
					{
						if (this.Darkness.Sprite.color.a != 1.0f)
						{
							this.Darkness.enabled = false;
							this.Darkness.Sprite.color = new Color(
								this.Darkness.Sprite.color.r,
								this.Darkness.Sprite.color.g,
								this.Darkness.Sprite.color.b,
								1.0f);

							audioSource.clip = this.TortureHit;
							audioSource.Play();
						}
					}

					if (this.Timer > 15.0f)
					{
						if (this.ID == 1)
						{
							Time.timeScale = 1.0f;
							this.NowLoading.SetActive(true);
							HomeGlobals.LateForSchool = true;
							SceneManager.LoadScene(SceneNames.LoadingScene);
							StudentGlobals.SetStudentSanity(SchoolGlobals.KidnapVictim, this.Sanity - 2.50f);
						}
						else if (this.ID == 2)
						{
							SceneManager.LoadScene(SceneNames.CalendarScene);
							StudentGlobals.SetStudentSanity(SchoolGlobals.KidnapVictim, this.Sanity - 10.0f);
							//PlayerGlobals.Reputation -= 20.0f;
						}
						else if (this.ID == 3)
						{
							HomeGlobals.Night = true;
							SceneManager.LoadScene(SceneNames.HomeScene);
							StudentGlobals.SetStudentSanity(SchoolGlobals.KidnapVictim, this.Sanity - 30.0f);
							PlayerGlobals.Reputation -= 20.0f;
						}
						else if (this.ID == 4)
						{
							SceneManager.LoadScene(SceneNames.CalendarScene);
							StudentGlobals.SetStudentSanity(SchoolGlobals.KidnapVictim, this.Sanity - 45.0f);
							PlayerGlobals.Reputation -= 20.0f;
						}

						if (StudentGlobals.GetStudentSanity(SchoolGlobals.KidnapVictim) < 0.0f)
						{
							StudentGlobals.SetStudentSanity(SchoolGlobals.KidnapVictim, 0.0f);
						}
					}
				}
			}
		}
	}

	public void UpdateDesc()
	{
		this.HomeCamera.PromptBar.Label[0].text = "Accept";

		this.DescLabel.text = this.Descriptions[this.ID];

		if (!HomeGlobals.Night)
		{
			if (HomeGlobals.LateForSchool)
			{
				if (this.ID == 1)
				{
					this.DescLabel.text = "This option is unavailable if you are late for school.";
					this.HomeCamera.PromptBar.Label[0].text = string.Empty;
				}
			}

			if (this.ID == 2)
			{
				this.DescLabel.text = "This option is unavailable in the daytime.";
				this.HomeCamera.PromptBar.Label[0].text = string.Empty;
			}

			if (DateGlobals.Weekday == DayOfWeek.Friday)
			{
				if ((this.ID == 3) || (this.ID == 4))
				{
					this.DescLabel.text = "This option is unavailable on Friday.";
					this.HomeCamera.PromptBar.Label[0].text = string.Empty;
				}
			}
		}
		else
		{
			if (this.ID != 2)
			{
				this.DescLabel.text = "This option is unavailable at nighttime.";
				this.HomeCamera.PromptBar.Label[0].text = string.Empty;
			}
		}

		if (this.ID == 5)
		{
			if (this.Sanity > 0.0f)
			{
				this.DescLabel.text = "This option is unavailable until your prisoner's Sanity has reached zero.";
			}

			if (HomeGlobals.Night)
			{
				this.DescLabel.text = "This option is unavailable at nighttime.";
				this.HomeCamera.PromptBar.Label[0].text = string.Empty;
			}
		}

		this.HomeCamera.PromptBar.UpdateButtons();
	}
}
