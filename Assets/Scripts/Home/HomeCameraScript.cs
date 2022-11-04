using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeCameraScript : MonoBehaviour
{
	public HomeWindowScript[] HomeWindows;
	public HomeTriggerScript[] Triggers;

	public HomePantyChangerScript HomePantyChanger;
	public HomeSenpaiShrineScript HomeSenpaiShrine;
	public HomeVideoGamesScript HomeVideoGames;
	public HomeCorkboardScript HomeCorkboard;
	public HomeDarknessScript HomeDarkness;
	public HomeInternetScript HomeInternet;
	public HomePrisonerScript HomePrisoner;
	public HomeYandereScript HomeYandere;
	public HomeSleepScript HomeAnime;
	public HomeMangaScript HomeManga;
	public HomeSleepScript HomeSleep;
	public HomeExitScript HomeExit;

	public PromptBarScript PromptBar;
	public Vignetting Vignette;

	public UILabel PantiesMangaLabel;
	public UISprite Button;

	public GameObject CyberstalkWindow;
	public GameObject ComputerScreen;
	public GameObject CorkboardLabel;
	public GameObject LoveSickCamera;
	public GameObject LoadingScreen;
	public GameObject CeilingLight;
	public GameObject SenpaiLight;
	public GameObject Controller;
	public GameObject NightLight;
	public GameObject RopeGroup;
	public GameObject DayLight;
	public GameObject Tripod;
	public GameObject Victim;

	public Transform Destination;
	public Transform Target;
	public Transform Focus;

	public Transform[] Destinations;
	public Transform[] Targets;

	public int ID = 0;

	public AudioSource BasementJukebox;
	public AudioSource RoomJukebox;

	public AudioClip NightBasement;
	public AudioClip NightRoom;
	public AudioClip HomeLoveSick;

	public bool Torturing = false;

	public CosmeticScript SenpaiCosmetic;
	public Renderer HairLock;

	public void Start()
	{
		this.Button.color = new Color(
			this.Button.color.r,
			this.Button.color.g,
			this.Button.color.b,
			0.0f);

		this.Focus.position = this.Target.position;
		this.transform.position = this.Destination.position;

		if (HomeGlobals.Night)
		{
			this.CeilingLight.SetActive(true);
			this.SenpaiLight.SetActive(true);
			this.NightLight.SetActive(true);
			this.DayLight.SetActive(false);

			this.Triggers[7].Disable();

			this.BasementJukebox.clip = NightBasement;
			this.RoomJukebox.clip = NightRoom;

			this.PlayMusic();

			this.PantiesMangaLabel.text = "Read Manga";
		}
		else
		{
			this.BasementJukebox.Play();
			this.RoomJukebox.Play();

			this.ComputerScreen.SetActive(false);
			this.Triggers[2].Disable();
			this.Triggers[3].Disable();
			this.Triggers[5].Disable();
			this.Triggers[9].Disable();
		}

		if (SchoolGlobals.KidnapVictim == 0)
		{
			this.RopeGroup.SetActive(false);
			this.Tripod.SetActive(false);
			this.Victim.SetActive(false);
			this.Triggers[10].Disable();
		}
		else
		{
			int StudentID = SchoolGlobals.KidnapVictim;

			if (StudentGlobals.GetStudentArrested(StudentID) ||
				StudentGlobals.GetStudentDead(StudentID))
			{
				this.RopeGroup.SetActive(false);
				this.Victim.SetActive(false);
				this.Triggers[10].Disable();
			}
		}

		if (GameGlobals.LoveSick)
		{
			LoveSickColorSwap();
		}

		Time.timeScale = 1.0f;

		HairLock.material.color = SenpaiCosmetic.ColorValue;

		if (SchoolGlobals.SchoolAtmosphere > 1.0f)
		{
			SchoolGlobals.SchoolAtmosphere = 1;
		}
	}

	void LateUpdate()
	{
		if (this.HomeYandere.transform.position.y > -5.0f)
		{
			Transform destination = this.Destinations[0];
			destination.position = new Vector3(
				-this.HomeYandere.transform.position.x,
				destination.position.y,
				destination.position.z);
		}

		this.Focus.position = Vector3.Lerp(
			this.Focus.position, this.Target.position, Time.deltaTime * 10.0f);
		this.transform.position = Vector3.Lerp(
			this.transform.position, this.Destination.position, Time.deltaTime * 10.0f);

		this.transform.LookAt(this.Focus.position);

		// [af] Combined if statements to reduce nesting.
		if ((this.ID != 11) && Input.GetButtonDown(InputNames.Xbox_A) &&
			this.HomeYandere.CanMove && (this.ID != 0))
		{
			this.Destination = this.Destinations[this.ID];
			this.Target = this.Targets[this.ID];

			this.HomeWindows[this.ID].Show = true;
			this.HomeYandere.CanMove = false;

			// Exiting the room or the basement.
			if ((this.ID == 1) || (this.ID == 8))
			{
				this.HomeExit.enabled = true;
			}
			// Going to sleep.
			else if (this.ID == 2)
			{
				this.HomeSleep.enabled = true;
			}
			// Using the Internet.
			else if (this.ID == 3)
			{
				this.HomeInternet.enabled = true;
			}
			// Using the corkboard.
			else if (this.ID == 4)
			{
				this.CorkboardLabel.SetActive(false);
				this.HomeCorkboard.enabled = true;
				this.LoadingScreen.SetActive(true);

				// [af] Added "gameObject" for C# compatibility.
				this.HomeYandere.gameObject.SetActive(false);
			}
			// Playing video games!
			else if (this.ID == 5)
			{
				this.HomeYandere.enabled = false;
				this.Controller.transform.localPosition = new Vector3(0.1245f, 0.032f, 0.0f);

				this.HomeYandere.transform.position = new Vector3(1.0f, 0.0f, 0.0f);
				this.HomeYandere.transform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
				this.HomeYandere.Character.GetComponent<Animation>().Play(AnimNames.FemaleGaming);

				this.PromptBar.ClearButtons();
				this.PromptBar.Label[0].text = "Play";
				this.PromptBar.Label[1].text = "Back";
				this.PromptBar.Label[4].text = "Select";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;
			}
			// Worshipping Senpai.
			else if (this.ID == 6)
			{
				this.HomeSenpaiShrine.enabled = true;

				// [af] Added "gameObject" for C# compatibility.
				this.HomeYandere.gameObject.SetActive(false);
			}
			// Changing panties.
			else if (this.ID == 7)
			{
				this.HomePantyChanger.enabled = true;
			}
			// Reading manga.
			else if (this.ID == 9)
			{
				this.HomeManga.enabled = true;
			}
			// Examining prisoner.
			else if (this.ID == 10)
			{
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[0].text = "Accept";
				this.PromptBar.Label[1].text = "Back";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;
				this.HomePrisoner.UpdateDesc();

				// [af] Added "gameObject" for C# compatibility.
				this.HomeYandere.gameObject.SetActive(false);
			}
			// Watching Anime
			else if (this.ID == 12)
			{
				this.HomeAnime.enabled = true;
			}
		}

		if (this.Destination == this.Destinations[0])
		{
			// [af] Replaced if/else statement with ternary expression.
			this.Vignette.intensity = (this.HomeYandere.transform.position.y > -1.0f) ?
				Mathf.MoveTowards(this.Vignette.intensity, 1.0f, Time.deltaTime) :
				Mathf.MoveTowards(this.Vignette.intensity, 5.0f, Time.deltaTime * 5.0f);

			this.Vignette.chromaticAberration = Mathf.MoveTowards(
				this.Vignette.chromaticAberration, 1.0f, Time.deltaTime);
			this.Vignette.blur = Mathf.MoveTowards(this.Vignette.blur, 1.0f, Time.deltaTime);
		}
		else
		{
			// [af] Replaced if/else statement with ternary expression.
			this.Vignette.intensity = (this.HomeYandere.transform.position.y > -1.0f) ?
				Mathf.MoveTowards(this.Vignette.intensity, 0.0f, Time.deltaTime) :
				Mathf.MoveTowards(this.Vignette.intensity, 0.0f, Time.deltaTime * 5.0f);

			this.Vignette.chromaticAberration = Mathf.MoveTowards(
				this.Vignette.chromaticAberration, 0.0f, Time.deltaTime);
			this.Vignette.blur = Mathf.MoveTowards(this.Vignette.blur, 0.0f, Time.deltaTime);
		}

		// [af] Replaced if/else statement with assignment and ternary expression.
		this.Button.color = new Color(
			this.Button.color.r,
			this.Button.color.g,
			this.Button.color.b,
			Mathf.MoveTowards(this.Button.color.a, ((this.ID > 0) && this.HomeYandere.CanMove) ? 1.0f : 0.0f, Time.deltaTime * 10.0f));

		if (this.HomeDarkness.FadeOut)
		{
			this.BasementJukebox.volume = Mathf.MoveTowards(
				this.BasementJukebox.volume, 0.0f, Time.deltaTime);
			this.RoomJukebox.volume = Mathf.MoveTowards(
				this.RoomJukebox.volume, 0.0f, Time.deltaTime);
		}
		else
		{
			if (this.HomeYandere.transform.position.y > -1.0f)
			{
				this.BasementJukebox.volume = Mathf.MoveTowards(
					this.BasementJukebox.volume, 0.0f, Time.deltaTime);
				this.RoomJukebox.volume = Mathf.MoveTowards(
					this.RoomJukebox.volume, 0.5f, Time.deltaTime);
			}
			else
			{
				if (!this.Torturing)
				{
					this.BasementJukebox.volume = Mathf.MoveTowards(
						this.BasementJukebox.volume, 0.5f, Time.deltaTime);
					this.RoomJukebox.volume = Mathf.MoveTowards(
						this.RoomJukebox.volume, 0.0f, Time.deltaTime);
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.Y))
		{
			//Pippi's Task.
			TaskGlobals.SetTaskStatus(38, 1);
		}

		if (Input.GetKeyDown(KeyCode.M))
		{
			BasementJukebox.gameObject.SetActive(false);
			RoomJukebox.gameObject.SetActive(false);
		}

		if (Input.GetKeyDown(KeyCode.BackQuote))
		{
			HomeGlobals.Night = !HomeGlobals.Night;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		if (Input.GetKeyDown(KeyCode.Equals))
		{
			Time.timeScale++;
		}

		if (Input.GetKeyDown(KeyCode.Minus))
		{
			if (Time.timeScale > 1)
			{
				Time.timeScale--;
			}
		}

#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.L))
		{
			GameGlobals.LoveSick = !GameGlobals.LoveSick;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
#endif
	}

	public void PlayMusic()
	{
		if (!YanvaniaGlobals.DraculaDefeated && !HomeGlobals.MiyukiDefeated)
		{
			if (!this.BasementJukebox.isPlaying)
			{
				this.BasementJukebox.Play();
			}

			if (!this.RoomJukebox.isPlaying)
			{
				this.RoomJukebox.Play();
			}
		}
	}

	public Transform PromptBarPanel;
	public Transform PauseScreen;

	void LoveSickColorSwap()
	{
		GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

		foreach(GameObject go in allObjects)
		{
			if (go.transform.parent != PauseScreen && go.transform.parent != PromptBarPanel)
			{
				UISprite sprite = go.GetComponent<UISprite> ();

				if (sprite != null)
				{
					if (sprite.color != Color.black)
					{
						sprite.color = new Color(1.0f, 0.0f, 0.0f, sprite.color.a);
					}
				}

				UILabel label = go.GetComponent<UILabel> ();

				if (label != null)
				{
					if (label.color != Color.black)
					{
						label.color = new Color(1.0f, 0.0f, 0.0f, label.color.a);
					}
				}
			}
		}
			
		this.DayLight.GetComponent<Light>().color = new Color (.5f, .5f, .5f, 1);
		this.HomeDarkness.Sprite.color = Color.black;
		this.BasementJukebox.clip = HomeLoveSick;
		this.RoomJukebox.clip = HomeLoveSick;
		this.LoveSickCamera.SetActive(true);
		this.PlayMusic();
	}
}
