using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeDarknessScript : MonoBehaviour
{
	public HomeVideoGamesScript HomeVideoGames;
	public HomeYandereScript HomeYandere;
	public HomeCameraScript HomeCamera;
	public HomeExitScript HomeExit;

	public InputDeviceScript InputDevice;

	public UILabel BasementLabel;
	public UISprite Sprite;

	public bool Cyberstalking = false;
	public bool FadeSlow = false;
	public bool FadeOut = false;

	void Start()
	{
		if (GameGlobals.LoveSick)
		{
			this.Sprite.color = new Color(
				0,
				0,
				0,
				1.0f);
		}

		this.Sprite.color = new Color(
			this.Sprite.color.r,
			this.Sprite.color.g,
			this.Sprite.color.b,
			1.0f);
	}

	void Update()
	{
        //Debug.Log("While walking around the home, InputDevice.Type is: " + InputDevice.Type);

        if (this.FadeOut)
		{
			// [af] Replaced if/else statement with assignment and ternary expression.
			this.Sprite.color = new Color(
				this.Sprite.color.r,
				this.Sprite.color.g,
				this.Sprite.color.b,
				this.Sprite.color.a + (Time.deltaTime * (this.FadeSlow ? 0.20f : 1.0f)));

			if (this.Sprite.color.a >= 1.0f)
			{
				if (this.HomeCamera.ID != 2)
				{
					if (this.HomeCamera.ID == 3)
					{
						if (this.Cyberstalking)
						{
							SceneManager.LoadScene(SceneNames.CalendarScene);
						}
						else
						{
							SceneManager.LoadScene(SceneNames.YancordScene);
						}
					}
					else if (this.HomeCamera.ID == 5)
					{
						if (HomeVideoGames.ID == 1)
						{
							SceneManager.LoadScene(SceneNames.YanvaniaTitleScene);
						}
						else
						{
							SceneManager.LoadScene(SceneNames.MiyukiTitleScene);
						}
					}
					else if (this.HomeCamera.ID == 9)
					{
						SceneManager.LoadScene(SceneNames.CalendarScene);
					}
					else if (this.HomeCamera.ID == 10)
					{
						StudentGlobals.SetStudentKidnapped(SchoolGlobals.KidnapVictim, false);
						StudentGlobals.StudentSlave = SchoolGlobals.KidnapVictim;

						this.CheckForOsanaThursday();
					}
					else if (this.HomeCamera.ID == 11)
					{
						EventGlobals.KidnapConversation = true;

						SceneManager.LoadScene(SceneNames.PhoneScene);
					}
					else if (this.HomeCamera.ID == 12)
					{
						SceneManager.LoadScene(SceneNames.LifeNoteScene);
					}
					else
					{
						if (this.HomeExit.ID == 1)
						{
							CheckForOsanaThursday();
						}
						else if (this.HomeExit.ID == 2)
						{
							//SceneManager.LoadScene(SceneNames.MaidMenuScene);
							SceneManager.LoadScene(SceneNames.StreetScene);
						}
						else if (this.HomeExit.ID == 3)
						{
							if (this.HomeYandere.transform.position.y > -5.0f)
							{
								//Debug.Log("Teleporting to basement.");

								this.HomeYandere.transform.position = new Vector3(-2.0f, -10.0f, -2.75f);
								this.HomeYandere.transform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
								this.HomeYandere.CanMove = true;
								this.FadeOut = false;

								this.HomeCamera.Destinations[0].position = new Vector3(2.425f, -8.0f, 0.0f);
								this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
								this.HomeCamera.transform.position = this.HomeCamera.Destination.position;

								this.HomeCamera.Target = this.HomeCamera.Targets[0];
								this.HomeCamera.Focus.position = this.HomeCamera.Target.position;

								this.BasementLabel.text = "Upstairs";

								this.HomeCamera.DayLight.SetActive(true);
								this.HomeCamera.DayLight.GetComponent<Light>().intensity = .66666f;

								Physics.SyncTransforms();
							}
							else
							{
								//Debug.Log("Teleporting to room.");

								this.HomeYandere.transform.position = new Vector3(-1.60f, 0.0f, -1.60f);
								this.HomeYandere.transform.eulerAngles = new Vector3(0.0f, 45.0f, 0.0f);
								this.HomeYandere.CanMove = true;
								this.FadeOut = false;

								this.HomeCamera.Destinations[0].position = new Vector3(-2.0615f, 2.0f, 2.418f);
								this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
								this.HomeCamera.transform.position = this.HomeCamera.Destination.position;

								this.HomeCamera.Target = this.HomeCamera.Targets[0];
								this.HomeCamera.Focus.position = this.HomeCamera.Target.position;

								this.BasementLabel.text = "Basement";

								if (HomeGlobals.Night)
								{
									this.HomeCamera.DayLight.SetActive(false);
								}

								this.HomeCamera.DayLight.GetComponent<Light>().intensity = 2;

								Physics.SyncTransforms();
							}
						}
                        else if (this.HomeExit.ID == 4)
                        {
                            SceneManager.LoadScene(SceneNames.StalkerHouseScene);
                        }
                    }
                }
				else
				{
					SceneManager.LoadScene(SceneNames.CalendarScene);
				}
			}
		}
		else
		{
			this.Sprite.color = new Color(
				this.Sprite.color.r,
				this.Sprite.color.g,
				this.Sprite.color.b,
				this.Sprite.color.a - Time.deltaTime);

			if (this.Sprite.color.a < 0.0f)
			{
				this.Sprite.color = new Color(
					this.Sprite.color.r,
					this.Sprite.color.g,
					this.Sprite.color.b,
					0.0f);
			}
		}
	}

	void CheckForOsanaThursday()
	{
		Debug.Log("Time to check if we need to display the Osana-walks-to-school cutscene...");

		if (InputDevice.Type == InputDeviceType.Gamepad)
		{
			PlayerGlobals.UsingGamepad = true;
		}
		else
		{
			PlayerGlobals.UsingGamepad = false;
		}

        //Debug.Log("At the moment we transition into the loading screen, PlayerGlobals.UsingGamepad is: " + PlayerGlobals.UsingGamepad);

        //Anti-Osana Code
        #if UNITY_EDITOR
        if (!StudentGlobals.GetStudentDead(11) &&
			DateGlobals.Weekday == System.DayOfWeek.Thursday &&
			!HomeGlobals.LateForSchool)
		{
			SceneManager.LoadScene(SceneNames.WalkToSchoolScene);
		}
		else
		#endif
		{
			SceneManager.LoadScene(SceneNames.LoadingScene);
		}
	}
}