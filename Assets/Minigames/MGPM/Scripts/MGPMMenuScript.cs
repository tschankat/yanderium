using UnityEngine;
using UnityEngine.SceneManagement;

public class MGPMMenuScript : MonoBehaviour
{
	public InputManagerScript InputManager;

	public AudioSource Jukebox;

	public AudioClip HardModeClip;

	public bool WindowDisplaying;

	public Transform Highlight;
	public Transform Background;

	public GameObject Controls;
	public GameObject Credits;

	public GameObject DifficultySelect;
	public GameObject MainMenu;

	public Renderer Black;
	public Renderer Logo;
	public Renderer BG;

	public Texture BloodyLogo;

	public AudioClip BGM;

	public float Rotation;
	public float Vibrate;

	public bool HardMode;
	public bool FadeOut;
	public bool FadeIn;

	public int ID;

	void Start()
	{
		Black.material.color = new Color(0, 0, 0, 1);
		Time.timeScale = 1;
	}

	void Update()
	{
		Rotation -= Time.deltaTime * 3;

		Background.localEulerAngles = new Vector3(0, 0, Rotation);

		if (FadeIn)
		{
			Black.material.color = new Color(0, 0, 0, Mathf.MoveTowards(Black.material.color.a, 0, Time.deltaTime));

			if (Black.material.color.a == 0)
			{
				Jukebox.Play();
				FadeIn = false;
			}
		}

		if (FadeOut)
		{
			Black.material.color = new Color(0, 0, 0, Mathf.MoveTowards(Black.material.color.a, 1, Time.deltaTime));
			Jukebox.volume = 1 - Black.material.color.a;

			if (Black.material.color.a == 1)
			{
				if (ID == 4)
				{
					SceneManager.LoadScene(SceneNames.HomeScene);
				}
				else
				{
					GameGlobals.HardMode = HardMode;

					SceneManager.LoadScene(SceneNames.MiyukiGameplayScene);
				}
			}
		}

		if (!FadeOut && !FadeIn)
		{
			if (!HardMode)
			{
				if (Input.GetKeyDown("h"))
				{
					AudioSource.PlayClipAtPoint(HardModeClip, transform.position);

					Logo.material.mainTexture = BloodyLogo;
					HardMode = true;
					Vibrate = .1f;
				}
			}

			if (HardMode)
			{
				Jukebox.pitch = Mathf.MoveTowards(Jukebox.pitch, .1f, Time.deltaTime);

				BG.material.color = new Color(
					Mathf.MoveTowards(BG.material.color.r, .5f, Time.deltaTime * .5f),
					Mathf.MoveTowards(BG.material.color.g, 0, Time.deltaTime),
					Mathf.MoveTowards(BG.material.color.b, 0, Time.deltaTime), 1);

				Logo.transform.localPosition = new Vector3(0, .5f, 2) + new Vector3(
					Random.Range(Vibrate * -1.0f, Vibrate),
					Random.Range(Vibrate * -1.0f, Vibrate),
					0);

				Vibrate = Mathf.MoveTowards(Vibrate, 0, Time.deltaTime * .1f);
			}

			if (Jukebox.clip != BGM)
			{
				if (!Jukebox.isPlaying)
				{
					Jukebox.loop = true;
					Jukebox.clip = BGM;
					Jukebox.Play();
				}
			}

			if (!WindowDisplaying)
			{
				if (InputManager.TappedDown)
				{
					ID++;
					UpdateHighlight();
				}

				if (InputManager.TappedUp)
				{
					ID--;
					UpdateHighlight();
				}

				if (Input.GetButtonDown(InputNames.Xbox_A) || Input.GetKeyDown("z") ||
					Input.GetKeyDown("return") | Input.GetKeyDown("space"))
				{
					if (MainMenu.activeInHierarchy)
					{
						if (ID == 1)
						{
							DifficultySelect.SetActive(true);
							MainMenu.SetActive(false);

							ID = 2;
							UpdateHighlight();
						}
						else if (ID == 2)
						{
							Highlight.gameObject.SetActive(false);
							Controls.SetActive(true);
							WindowDisplaying = true;
						}
						else if (ID == 3)
						{
							Highlight.gameObject.SetActive(false);
							Credits.SetActive(true);
							WindowDisplaying = true;
						}
						else if (ID == 4)
						{
							FadeOut = true;
						}
					}
					else
					{
						if (ID == 2)
						{
							GameGlobals.EasyMode = false;
						}
						else
						{
							GameGlobals.EasyMode = true;
						}

						FadeOut = true;
					}
				}
			}
			else
			{
				if (Input.GetButtonDown(InputNames.Xbox_B))
				{
					Highlight.gameObject.SetActive(true);
					Controls.SetActive(false);
					Credits.SetActive(false);
					WindowDisplaying = false;
				}
			}
		}
	}

	void UpdateHighlight()
	{
		if (MainMenu.activeInHierarchy)
		{
			if (ID == 0)
			{
				ID = 4;
			}
			else if (ID == 5)
			{
				ID = 1;
			}
		}
		else
		{
			if (ID == 1)
			{
				ID = 3;
			}
			else if (ID == 4)
			{
				ID = 2;
			}
		}

		Highlight.transform.position = new Vector3(0, -.2f * ID, Highlight.transform.position.z);
	}
}