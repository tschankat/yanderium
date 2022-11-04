using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeVideoGamesScript : MonoBehaviour
{
	public InputManagerScript InputManager;
	public HomeDarknessScript HomeDarkness;
	public HomeYandereScript HomeYandere;
	public HomeCameraScript HomeCamera;
	public HomeWindowScript HomeWindow;
	public PromptBarScript PromptBar;
	public Texture[] TitleScreens;
	public UITexture TitleScreen;
	public GameObject Controller;
	public Transform Highlight;
	public UILabel[] GameTitles;
	public Transform TV;
	public int ID = 1;

	void Start()
	{
		if (TaskGlobals.GetTaskStatus(38) == 0)
		{
			this.TitleScreens[1] = this.TitleScreens[5];

			UILabel gameTitle1 = this.GameTitles[1];
			gameTitle1.text = this.GameTitles[5].text;
			gameTitle1.color = new Color(
				gameTitle1.color.r, gameTitle1.color.g, gameTitle1.color.b, 0.50f);
		}

		this.TitleScreen.mainTexture = this.TitleScreens[1];
	}

	void Update()
	{
		if (this.HomeCamera.Destination == this.HomeCamera.Destinations[5])
		{
			if (Input.GetKeyDown("y"))
			{
				//Pippi's Task.
				TaskGlobals.SetTaskStatus(38, 1);
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}

			this.TV.localScale = Vector3.Lerp(
				this.TV.localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);

			if (!this.HomeYandere.CanMove)
			{
				if (!this.HomeDarkness.FadeOut)
				{
					if (this.InputManager.TappedDown)
					{
						this.ID++;

						if (this.ID > 5)
						{
							this.ID = 1;
						}

						this.TitleScreen.mainTexture = this.TitleScreens[this.ID];
						this.Highlight.localPosition = new Vector3(
							this.Highlight.localPosition.x,
							150.0f - (this.ID * 50.0f),
							this.Highlight.localPosition.z);
					}

					if (this.InputManager.TappedUp)
					{
						this.ID--;

						if (this.ID < 1)
						{
							this.ID = 5;
						}

						this.TitleScreen.mainTexture = this.TitleScreens[this.ID];
						this.Highlight.localPosition = new Vector3(
							this.Highlight.localPosition.x,
							150.0f - (this.ID * 50.0f),
							this.Highlight.localPosition.z);
					}

					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						if (this.GameTitles[this.ID].color.a == 1.0f)
						{
							Transform target = this.HomeCamera.Targets[5];
							target.localPosition = new Vector3(
								target.localPosition.x,
								1.153333f,
								target.localPosition.z);

							this.HomeDarkness.Sprite.color = new Color(
								this.HomeDarkness.Sprite.color.r,
								this.HomeDarkness.Sprite.color.g,
								this.HomeDarkness.Sprite.color.b,
								-1.0f);

							this.HomeDarkness.FadeOut = true;
							this.HomeWindow.Show = false;
							this.PromptBar.Show = false;
							this.HomeCamera.ID = 5;
						}
					}

					if (Input.GetButtonDown(InputNames.Xbox_B))
					{
						this.Quit();
					}
				}
				else
				{
					Transform destination = this.HomeCamera.Destinations[5];
					Transform target = this.HomeCamera.Targets[5];
					destination.position = new Vector3(
						Mathf.Lerp(destination.position.x, target.position.x, Time.deltaTime * 0.75f),
						Mathf.Lerp(destination.position.y, target.position.y, Time.deltaTime * 10.0f),
						Mathf.Lerp(destination.position.z, target.position.z, Time.deltaTime * 10.0f));
				}
			}
		}
		else
		{
			this.TV.localScale = Vector3.Lerp(
				this.TV.localScale, Vector3.zero, Time.deltaTime * 10.0f);
		}
	}

	public void Quit()
	{
		this.Controller.transform.localPosition = new Vector3(0.20385f, 0.0595f, 0.0215f);
		this.Controller.transform.localEulerAngles = new Vector3 (-90, -90, 0);
		this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
		this.HomeCamera.Target = this.HomeCamera.Targets[0];
		this.HomeYandere.CanMove = true;
		this.HomeYandere.enabled = true;
		this.HomeWindow.Show = false;

		this.HomeCamera.PlayMusic();

		this.PromptBar.ClearButtons();
		this.PromptBar.Show = false;
	}
}
