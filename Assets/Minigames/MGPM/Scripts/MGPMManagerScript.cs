using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MGPMManagerScript : MonoBehaviour
{
	public MGPMSpawnerScript[] EnemySpawner;
	public MGPMMiyukiScript Miyuki;

	public GameObject StageClearGraphic;
	public GameObject GameOverGraphic;
	public GameObject StartGraphic;

	public Renderer[] WaterRenderer;

	public Renderer RightArtwork;
	public Renderer LeftArtwork;

	public Texture RightBloody;
	public Texture LeftBloody;

	public AudioSource Jukebox;

	public AudioClip HardModeVoice;
	public AudioClip GameOverMusic;
	public AudioClip VictoryMusic;
	public AudioClip FinalBoss;
	public AudioClip BGM;

	public Renderer Black;

	public Text ScoreLabel;

	public bool StageClear;
	public bool GameOver;
	public bool FadeOut;
	public bool FadeIn;
	public bool Intro;

	public float GameOverTimer;
	public float Timer;

	public int Score;
	public int ID;

	void Start()
	{
		if (GameGlobals.HardMode)
		{
			Jukebox.clip = HardModeVoice;
			WaterRenderer[0].material.color = Color.red;
			WaterRenderer[1].material.color = Color.red;

			RightArtwork.material.mainTexture = RightBloody;
			LeftArtwork.material.mainTexture = LeftBloody;
		}

		Miyuki.transform.localPosition = new Vector3(0, -300, 0);
		Black.material.color = new Color(0, 0, 0, 1);
		StartGraphic.SetActive(false);
		Miyuki.Gameplay = false;

		ID = 1;

		while (ID < EnemySpawner.Length)
		{
			EnemySpawner[ID].enabled = false;
			ID++;
		}

		Time.timeScale = 1;
	}

	void Update()
	{
		ScoreLabel.text = "Score: " + Score * Miyuki.Health;

		if (StageClear)
		{
			GameOverTimer += Time.deltaTime;

			if (GameOverTimer > 1)
			{
				Miyuki.transform.localPosition = new Vector3(
					Miyuki.transform.localPosition.x,
					Miyuki.transform.localPosition.y + Time.deltaTime * 10,
					Miyuki.transform.localPosition.z);

				if (!StageClearGraphic.activeInHierarchy)
				{
					StageClearGraphic.SetActive(true);
					Jukebox.clip = VictoryMusic;
					Jukebox.loop = false;
					Jukebox.volume = 1;
					Jukebox.Play();
				}

				if (GameOverTimer > 9)
				{
					FadeOut = true;
				}
			}

			if (FadeOut)
			{
				Black.material.color = new Color(0, 0, 0, Mathf.MoveTowards(Black.material.color.a, 1, Time.deltaTime));
				Jukebox.volume = 1 - Black.material.color.a;

				if (Black.material.color.a == 1)
				{
					SceneManager.LoadScene(SceneNames.MiyukiThanksScene);
				}
			}
		}
		else if (!GameOver)
		{
			if (Intro)
			{
				if (FadeIn)
				{
					Black.material.color = new Color(0, 0, 0, Mathf.MoveTowards(Black.material.color.a, 0, Time.deltaTime));

					if (Black.material.color.a == 0)
					{
						Jukebox.Play();
						FadeIn = false;
					}
				}
				else
				{
					Miyuki.transform.localPosition = new Vector3(0, Mathf.MoveTowards(Miyuki.transform.localPosition.y, -120, Time.deltaTime * 60), 0);

					if (Miyuki.transform.localPosition.y == -120)
					{
						if (!Jukebox.isPlaying)
						{
							Jukebox.loop = true;
							Jukebox.clip = BGM;
							Jukebox.Play();

							if (GameGlobals.HardMode)
							{
								Jukebox.pitch = .2f;
							}
						}

						StartGraphic.SetActive(true);

						Timer += Time.deltaTime;

						if (Timer > 3.5)
						{
							StartGraphic.SetActive(false);

							ID = 1;

							while (ID < EnemySpawner.Length)
							{
								EnemySpawner[ID].enabled = true;
								ID++;
							}

							Miyuki.Gameplay = true;
							Intro = false;
						}
					}
				}

				//#if UNITY_EDITOR

				if (Input.GetKeyDown("space"))
				{
					StartGraphic.SetActive(false);

					ID = 1;

					while (ID < EnemySpawner.Length)
					{
						EnemySpawner[ID].enabled = true;
						ID++;
					}

					Black.material.color = new Color(0, 0, 0, 0);
					Miyuki.Gameplay = true;
					Intro = false;

					Jukebox.loop = true;
					Jukebox.clip = BGM;
					Jukebox.Play();

					if (GameGlobals.HardMode)
					{
						Jukebox.pitch = .2f;
					}
				}

				//#endif
			}

			#if UNITY_EDITOR

			if (Input.GetKeyDown("m"))
			{
				if (Jukebox.volume == .5f)
				{
					Jukebox.volume = .1f;
				}
				else
				{
					Jukebox.volume = .5f;
				}
			}

			if (Input.GetKeyDown("e"))
			{
				GameGlobals.EasyMode = !GameGlobals.EasyMode;

				SceneManager.LoadScene(SceneNames.MiyukiGameplayScene);
			}

			#endif
		}
		else
		{
			GameOverTimer += Time.deltaTime;

			if (GameOverTimer > 3)
			{
				if (!GameOverGraphic.activeInHierarchy)
				{
					GameOverGraphic.SetActive(true);
					Jukebox.clip = GameOverMusic;
					Jukebox.loop = false;
					Jukebox.Play();
				}
				else
				{
					if (Input.anyKeyDown)
					{
						FadeOut = true;
					}
				}
			}

			if (FadeOut)
			{
				Black.material.color = new Color(0, 0, 0, Mathf.MoveTowards(Black.material.color.a, 1, Time.deltaTime));
				Jukebox.volume = 1 - Black.material.color.a;

				if (Black.material.color.a == 1)
				{
					SceneManager.LoadScene(SceneNames.MiyukiTitleScene);
				}
			}
		}
	}

	public void BeginGameOver()
	{
		Jukebox.Stop();
		GameOver = true;
		Miyuki.enabled = false;
	}
}