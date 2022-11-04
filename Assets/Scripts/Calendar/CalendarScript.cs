using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CalendarScript : MonoBehaviour
{
	public SelectiveGrayscale GrayscaleEffect;
	public ChallengeScript Challenge;
	public Vignetting Vignette;

	public GameObject ConfirmationWindow;

	public UILabel AtmosphereLabel;
	public UIPanel ChallengePanel;
	public UIPanel CalendarPanel;
	public UISprite Darkness;
	public UITexture Cloud;
	public UITexture Sun;

	public Transform Highlight;
	public Transform Continue;

    public UILabel[] DayNumber;
    public UILabel[] DayLabel;

    public UILabel WeekNumber;

	public bool Incremented = false;
	public bool LoveSick = false;
	public bool FadeOut = false;
	public bool Switch = false;
	public bool Reset = false;

	public float Timer = 0.0f;
	public float Target = 0;

	public int Phase = 1;

	void Start()
	{
		Debug.Log ("Upon entering the Calendar screen, DateGlobals.Weekday is: " + DateGlobals.Weekday);

		this.LoveSickCheck();

		if (!SchoolGlobals.SchoolAtmosphereSet)
		{
			//Debug.Log("Initializing atmosphere.");

			OptionGlobals.EnableShadows = false;

			SchoolGlobals.SchoolAtmosphereSet = true;
			SchoolGlobals.SchoolAtmosphere = 1.0f;

			PlayerGlobals.Money = 10;
		}

		if (SchoolGlobals.SchoolAtmosphere > 1.0f)
		{
			SchoolGlobals.SchoolAtmosphere = 1;
		}

		//Anti-Osana Code
		#if !UNITY_EDITOR
		if (DateGlobals.Weekday > DayOfWeek.Thursday)
		{
			DateGlobals.Weekday = DayOfWeek.Sunday;
			Globals.DeleteAll();
		}
		#endif

		if (DateGlobals.PassDays < 1)
		{
			DateGlobals.PassDays = 1;
		}

		DateGlobals.DayPassed = true;

		this.Sun.color = new Color(
			this.Sun.color.r,
			this.Sun.color.g,
			this.Sun.color.b,
			SchoolGlobals.SchoolAtmosphere);

		this.Cloud.color = new Color(
			this.Cloud.color.r,
			this.Cloud.color.g,
			this.Cloud.color.b,
			1.0f - SchoolGlobals.SchoolAtmosphere);
		
		this.AtmosphereLabel.text = (SchoolGlobals.SchoolAtmosphere * 100.0f).ToString("f0") + "%";

		float EffectStrength = 1.0f - SchoolGlobals.SchoolAtmosphere;

		this.GrayscaleEffect.desaturation = EffectStrength;

		this.Vignette.intensity = EffectStrength * 5.0f;
		this.Vignette.blur = EffectStrength;
		this.Vignette.chromaticAberration = EffectStrength;

		this.Continue.transform.localPosition = new Vector3(
			this.Continue.transform.localPosition.x,
			-610.0f,
			this.Continue.transform.localPosition.z);

		this.Challenge.ViewButton.SetActive(false);
		this.Challenge.LargeIcon.color = new Color(
			this.Challenge.LargeIcon.color.r,
			this.Challenge.LargeIcon.color.g,
			this.Challenge.LargeIcon.color.b,
			0.0f);

		this.Challenge.Panels[1].alpha = 0.50f;
		this.Challenge.Shadow.color = new Color(
			this.Challenge.Shadow.color.r,
			this.Challenge.Shadow.color.g,
			this.Challenge.Shadow.color.b,
			0.0f);

		this.ChallengePanel.alpha = 0.0f;
		this.CalendarPanel.alpha = 1.0f;

		this.Darkness.color = new Color(
			this.Darkness.color.r,
			this.Darkness.color.g,
			this.Darkness.color.b,
			1.0f);

		Time.timeScale = 1.0f;

		this.Highlight.localPosition = new Vector3(
			-750.0f + 66.666f + (250 * ((int)DateGlobals.Weekday)),
			this.Highlight.localPosition.y,
			this.Highlight.localPosition.z);

		if (DateGlobals.Weekday == DayOfWeek.Saturday)
		{
			this.Highlight.localPosition = new Vector3(
				-1150,
				this.Highlight.localPosition.y,
				this.Highlight.localPosition.z);
		}

		if (DateGlobals.Week == 2)
		{
			DayNumber[1].text = "11";
			DayNumber[2].text = "12";
			DayNumber[3].text = "13";
			DayNumber[4].text = "14";
			DayNumber[5].text = "15";
			DayNumber[6].text = "16";
			DayNumber[7].text = "17";
		}

		WeekNumber.text = "Week " + DateGlobals.Week;

		this.LoveSickCheck();

        ChangeDayColor();
    }

	void Update()
	{
		this.Timer += Time.deltaTime;

		if (!this.FadeOut)
		{
			this.Darkness.color = new Color(
				this.Darkness.color.r,
				this.Darkness.color.g,
				this.Darkness.color.b,
				this.Darkness.color.a - Time.deltaTime);

			if (this.Darkness.color.a < 0.0f)
			{
				this.Darkness.color = new Color(
					this.Darkness.color.r,
					this.Darkness.color.g,
					this.Darkness.color.b,
					0.0f);
			}

			if (this.Timer > 1.0f)
			{
				if (!this.Incremented)
				{
					while (DateGlobals.PassDays > 0)
					{
						DateGlobals.Weekday++;
						DateGlobals.PassDays--;
                        ChangeDayColor();
                    }

					this.Target = 250.0f * (int)DateGlobals.Weekday;

					if ((int)DateGlobals.Weekday > 6)
					{
						this.Darkness.color = new Color(0, 0, 0, 0);
						DateGlobals.Weekday = DayOfWeek.Sunday;
						this.Target = 0;
					}

					Debug.Log ("And, as of now, DateGlobals.Weekday is: " + DateGlobals.Weekday);

					this.Incremented = true;
                    this.GetComponent<AudioSource>().pitch = 1 - (.8f - (SchoolGlobals.SchoolAtmosphere * .8f));
                    this.GetComponent<AudioSource>().Play();
				}
				else
				{
					this.Highlight.localPosition = new Vector3(
						Mathf.Lerp(this.Highlight.localPosition.x, -750.0f + 66.666f + Target, Time.deltaTime * 10.0f),
						this.Highlight.localPosition.y,
						this.Highlight.localPosition.z);
				}

				if (this.Timer > 2.0f)
				{
					this.Continue.localPosition = new Vector3(
						this.Continue.localPosition.x,
						Mathf.Lerp(this.Continue.localPosition.y, -500.0f, Time.deltaTime * 10.0f),
						this.Continue.localPosition.z);

					if (!this.Switch)
					{
						if (!this.ConfirmationWindow.activeInHierarchy)
						{
							if (Input.GetButtonDown(InputNames.Xbox_A))
							{
								this.FadeOut = true;
							}

							if (Input.GetButtonDown(InputNames.Xbox_Y))
							{
								this.Switch = true;
							}

							if (Input.GetButtonDown(InputNames.Xbox_B))
							{
								this.ConfirmationWindow.SetActive(true);
							}

							if (Input.GetKeyDown(KeyCode.Z))
							{
								if (SchoolGlobals.SchoolAtmosphere > 0)
								{
									SchoolGlobals.SchoolAtmosphere = SchoolGlobals.SchoolAtmosphere - 0.10f;
								}
								else
								{
									SchoolGlobals.SchoolAtmosphere = 100.0f;
								}

								SceneManager.LoadScene(SceneManager.GetActiveScene().name);
							}
						}
						else
						{
							if (Input.GetButtonDown(InputNames.Xbox_A))
							{
								this.FadeOut = true;
								this.Reset = true;
							}

							if (Input.GetButtonDown(InputNames.Xbox_B))
							{
								this.ConfirmationWindow.SetActive(false);
							}
						}
					}
				}
			}
		}
		else
		{
			this.Continue.localPosition = new Vector3(
				this.Continue.localPosition.x,
				Mathf.Lerp(this.Continue.localPosition.y, -610.0f, Time.deltaTime * 10.0f),
				this.Continue.localPosition.z);

			this.Darkness.color = new Color(
				this.Darkness.color.r,
				this.Darkness.color.g,
				this.Darkness.color.b,
				this.Darkness.color.a + Time.deltaTime);

			if (this.Darkness.color.a >= 1.0f)
			{
				if (this.Reset)
				{
					int Profile = GameGlobals.Profile;

					Globals.DeleteAll();

					PlayerPrefs.SetInt("ProfileCreated_" + Profile, 1);
					GameGlobals.Profile = Profile;

					GameGlobals.LoveSick = this.LoveSick;

					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				}
				else
				{
					if (HomeGlobals.Night)
					{
						HomeGlobals.Night = false;
					}

					if (DateGlobals.Weekday == DayOfWeek.Saturday)
					{
						SceneManager.LoadScene(SceneNames.BusStopScene);
					}
					else
					{
						if (DateGlobals.Weekday == DayOfWeek.Sunday)
						{
							HomeGlobals.Night = true;
						}

						SceneManager.LoadScene(SceneNames.HomeScene);
					}
				}
			}
		}

		if (this.Switch)
		{
			if (this.Phase == 1)
			{
				this.CalendarPanel.alpha -= Time.deltaTime;

				if (this.CalendarPanel.alpha <= 0.0f)
				{
					this.Phase++;
				}
			}
			else
			{
				this.ChallengePanel.alpha += Time.deltaTime;

				if (this.ChallengePanel.alpha >= 1.0f)
				{
					this.Challenge.enabled = true;
					this.enabled = false;
					this.Switch = false;
					this.Phase = 1;
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			DateGlobals.Weekday = DayOfWeek.Monday;
			Target = 250.0f * (int)DateGlobals.Weekday;
            ChangeDayColor();
        }

		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			DateGlobals.Weekday = DayOfWeek.Tuesday;
			Target = 250.0f * (int)DateGlobals.Weekday;
            ChangeDayColor();
        }

		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			DateGlobals.Weekday = DayOfWeek.Wednesday;
			Target = 250.0f * (int)DateGlobals.Weekday;
            ChangeDayColor();
        }

		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			DateGlobals.Weekday = DayOfWeek.Thursday;
			Target = 250.0f * (int)DateGlobals.Weekday;
            ChangeDayColor();
        }

		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			DateGlobals.Weekday = DayOfWeek.Friday;
			Target = 250.0f * (int)DateGlobals.Weekday;
            ChangeDayColor();
        }

		#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.Alpha6))
		{
			DateGlobals.Weekday = DayOfWeek.Saturday;
			Target = 250.0f * (int)DateGlobals.Weekday;
            ChangeDayColor();
        }

		if (Input.GetKeyDown(KeyCode.Alpha7))
		{
			DateGlobals.Weekday = DayOfWeek.Sunday;
			Target = 0;

			this.Darkness.color = new Color(0, 0, 0, 0);

            ChangeDayColor();
        }
			
		if (Input.GetKeyDown(KeyCode.L))
		{
			GameGlobals.LoveSick = !GameGlobals.LoveSick;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		#endif
	}

    public void ChangeDayColor()
    {
        foreach (UILabel Label in DayLabel)
        {
            Label.color = new Color(1, 1, 1, 1);
        }

        foreach (UILabel Label in DayNumber)
        {
            if (Label != null)
            {
                Label.color = new Color(1, 1, 1, .25f);
            }
        }

             if (DateGlobals.Weekday == DayOfWeek.Sunday)   {DayLabel[0].color = new Color(1, .5f, .75f, 1); DayNumber[1].color = new Color(1, .5f, .75f, .25f);}
        else if (DateGlobals.Weekday == DayOfWeek.Monday)   {DayLabel[1].color = new Color(1, .5f, .75f, 1); DayNumber[2].color = new Color(1, .5f, .75f, .25f); }
        else if (DateGlobals.Weekday == DayOfWeek.Tuesday)  {DayLabel[2].color = new Color(1, .5f, .75f, 1); DayNumber[3].color = new Color(1, .5f, .75f, .25f); }
        else if (DateGlobals.Weekday == DayOfWeek.Wednesday){DayLabel[3].color = new Color(1, .5f, .75f, 1); DayNumber[4].color = new Color(1, .5f, .75f, .25f); }
        else if (DateGlobals.Weekday == DayOfWeek.Thursday) {DayLabel[4].color = new Color(1, .5f, .75f, 1); DayNumber[5].color = new Color(1, .5f, .75f, .25f); }
        else if (DateGlobals.Weekday == DayOfWeek.Friday)   {DayLabel[5].color = new Color(1, .5f, .75f, 1); DayNumber[6].color = new Color(1, .5f, .75f, .25f); }
        else if (DateGlobals.Weekday == DayOfWeek.Saturday) {DayLabel[6].color = new Color(1, .5f, .75f, 1); DayNumber[7].color = new Color(1, .5f, .75f, .25f); }
    }

    public void LoveSickCheck()
	{
		if (GameGlobals.LoveSick)
		{
			SchoolGlobals.SchoolAtmosphereSet = true;
			SchoolGlobals.SchoolAtmosphere = 0.0f;

			this.LoveSick = true;

			Camera.main.backgroundColor = new Color (0, 0, 0, 1);

			GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

			foreach(GameObject go in allObjects)
			{
				UISprite sprite = go.GetComponent<UISprite> ();

				if (sprite != null)
				{
					sprite.color = new Color(1.0f, 0.0f, 0.0f, sprite.color.a);
				}

				UITexture texture = go.GetComponent<UITexture> ();

				if (texture != null)
				{
					texture.color = new Color(1.0f, 0.0f, 0.0f, texture.color.a);
				}

				UILabel label = go.GetComponent<UILabel> ();

				if (label != null)
				{
					if (label.color != Color.black)
					{
						label.color = new Color(1.0f, 0.0f, 0.0f, label.color.a);
					}

					if (label.text == "?")
					{
						label.color = new Color(1.0f, 0.0f, 0.0f, label.color.a);
					}
				}
			}

			this.Darkness.color = Color.black;

			this.AtmosphereLabel.enabled = false;
			this.Cloud.enabled = false;
			this.Sun.enabled = false;
		}
	}
}
