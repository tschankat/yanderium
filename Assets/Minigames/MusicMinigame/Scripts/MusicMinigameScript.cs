using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicMinigameScript : MonoBehaviour
{
	public GameObject[] NoteIcons;
	public Transform[] Scales;

	public Renderer[] Stars;

	public InputManagerScript InputManager;

	public Renderer HealthBarRenderer;
	public Renderer Black;

	public Transform ReputationMarker;
	public Transform ReputationBar;
	public Transform HealthBar;
	public Transform SadMiyuji;
	public Transform SadAyano;

	public GameObject GameOverScreen;

	public AudioSource MyAudio;

	public UILabel CurrentRep;
	public UILabel RepBonus;

	public Texture EmptyStar;
	public Texture GoldStar;

	public float JumpStrength;
	public float CringeTimer;
	public float StartRep;
	public float Health;
	public float Alpha;
	public float Power;
	public float Speed;
	public float Timer;

	public float[] Phase1Times;
	public int[] Phase1Notes;

	public float[] Phase2Times;
	public int[] Phase2Notes;

	public float[] Times;
	public int[] Notes;

	public int CurrentNote;
	public int Excitement;
	public int Phase;
	public int Note;
	public int ID;

	public bool SettingNotes;
	public bool LockHealth;
	public bool GameOver;
	public bool KeyDown;
	public bool Won;

	public Texture[] ChibiCelebrate;
	public Texture[] ChibiPerform;
	public Texture[] ChibiPerformB;
	public Texture[] ChibiCringe;
	public Texture[] ChibiIdle;

	public ParticleSystem[] MusicNotes;
	public AudioClip[] Celebrations;
	public Renderer[] ChibiRenderer;
	public Transform[] Instruments;

	public float[] AnimTimer;
	public float[] PingPong;
	public float[] Rotation;
	public float[] Jump;

	public bool[] ChibiSway;
	public bool[] FrameB;
	public bool[] Ping;

	void Start()
	{
		Application.targetFrameRate = 60;
		Time.timeScale = 1;

		Black.gameObject.SetActive(true);
		GameOverScreen.SetActive(false);

		Scales[0].localPosition = new Vector3(-1, 0, 0);
		Scales[1].localPosition = new Vector3(0, 0, 0);
		Scales[2].localPosition = new Vector3(1, 0, 0);
		Scales[3].localPosition = new Vector3(2, 0, 0);

		ID = 0;

		while (ID < Phase1Times.Length)
		{
			Times[ID] = Phase1Times[ID];
			Notes[ID] = Phase1Notes[ID];
			ID++;
		}

		ID = 0;

		while (ID < Phase2Times.Length)
		{
			Times[ID + 216] = Phase2Times[ID];
			Notes[ID + 216] = Phase2Notes[ID];
			ID++;
		}

		ID = 0;

		while (ID < Times.Length)
		{
			Times[ID] += 3;
			ID++;
		}

		UpdateHealthBar();

		ReputationBar.localScale = new Vector3(0, 0, 0);

		Black.material.color = new Color(0, 0, 0, 1);
	}

	void Update()
	{
		ID = 0;

		while (ID < Scales.Length)
		{
			Scales[ID].localPosition -= new Vector3(Time.deltaTime * Speed, 0, 0);

			if (Scales[ID].localPosition.x < -2)
			{
				Scales[ID].localPosition += new Vector3(4, 0, 0);
			}

			ID++;
		}

		if (Input.GetKeyDown("escape"))
		{
			GameOver = true;
			Timer = 9;
		}

		if (Input.GetKeyDown("l"))
		{
			LockHealth = !LockHealth;
		}

		if (GameOver)
		{
			MyAudio.pitch = Mathf.MoveTowards(MyAudio.pitch, 0, Time.deltaTime * .33333f);

			Timer += Time.deltaTime;

			if (Timer > 4)
			{
				if (!GameOverScreen.activeInHierarchy)
				{
					SadMiyuji.localPosition = new Vector3(-.51f, -.1f, -.2f);
					SadAyano.localPosition = new Vector3(.495f, -.1f, -.2f);

					GameOverScreen.SetActive(true);
				}

				SadMiyuji.localPosition = Vector3.Lerp(SadMiyuji.localPosition, new Vector3(-.455f, -.1f, -.2f), Time.deltaTime);
				SadAyano.localPosition = Vector3.Lerp(SadAyano.localPosition, new Vector3(.44f, -.1f, -.2f), Time.deltaTime);

				if (Timer > 9)
				{
					Alpha = Mathf.MoveTowards(Alpha, 1, Time.deltaTime);
					Black.material.color = new Color(0, 0, 0, Alpha);

					if (Alpha == 1)
					{
						Quit();
					}
				}
			}
		}
		else if (!Won)
		{
			Alpha = Mathf.MoveTowards(Alpha, 0, Time.deltaTime);
			Black.material.color = new Color(0, 0, 0, Alpha);

			Timer += Time.deltaTime;

			if (!MyAudio.isPlaying)
			{
				if (Timer > 3 || Input.GetKeyDown("space"))
				{
					if (Timer < MyAudio.clip.length)
					{
						MyAudio.Play();
					}
					else
					{
						ChibiRenderer[1].material.mainTexture = ChibiCelebrate[1];
						ChibiRenderer[2].material.mainTexture = ChibiCelebrate[2];
						ChibiRenderer[3].material.mainTexture = ChibiCelebrate[3];
						ChibiRenderer[4].material.mainTexture = ChibiCelebrate[4];
						ChibiRenderer[5].material.mainTexture = ChibiCelebrate[5];
						ChibiRenderer[6].material.mainTexture = ChibiCelebrate[6];

						Jump[1] = JumpStrength;
						Jump[2] = JumpStrength * .9f;
						Jump[3] = JumpStrength * .8f;
						Jump[4] = JumpStrength * .7f;
						Jump[5] = JumpStrength * .6f;
						Jump[6] = JumpStrength * .5f;

						if (Health == 200)
						{
							Excitement = 3;
						}
						else if (Health > 0)
						{
							Excitement = 2;
						}
						else
						{
							Excitement = 1;
						}

						MyAudio.clip = Celebrations[Excitement];
						MyAudio.loop = false;
						MyAudio.Play();
						Won = true;
						Timer = 0;
					}
				}
			}
			else
			{
				if (Input.GetKeyDown("space"))
				{
					MyAudio.time += 10;
					Timer = MyAudio.time + 3;
				}

				if (Input.GetKeyDown("z"))
				{
					MyAudio.time = MyAudio.clip.length - Time.deltaTime;
				}

				if (MyAudio.time > 131)
				{
					ChibiSway[2] = false;
					ChibiSway[6] = false;
					ChibiSway[3] = false;
					ChibiSway[5] = false;
					ChibiSway[4] = false;
				}
				else if (MyAudio.time > 88.2833333)
				{
					ChibiSway[2] = true;
					ChibiSway[6] = true;
					ChibiSway[3] = true;
					ChibiSway[5] = true;
					ChibiSway[4] = true;
				}
				else if (MyAudio.time > 74.25)
				{
					ChibiSway[2] = true;
					ChibiSway[6] = true;
					ChibiSway[3] = true;
					ChibiSway[5] = false;
					ChibiSway[4] = false;
				}
				else if (MyAudio.time > 60)
				{
					ChibiSway[2] = true;
					ChibiSway[6] = true;
					ChibiSway[3] = false;
					ChibiSway[5] = false;
					ChibiSway[4] = false;
				}
				else if (MyAudio.time > 45.933333)
				{
					ChibiSway[2] = true;
					ChibiSway[6] = true;
					ChibiSway[3] = true;
					ChibiSway[5] = true;
					ChibiSway[4] = true;
				}
				else if (MyAudio.time > 45.08)
				{
					ChibiSway[2] = true;
					ChibiSway[6] = true;
					ChibiSway[3] = false;
					ChibiSway[5] = true;
					ChibiSway[4] = true;
				}
				else if (MyAudio.time > 35.33333)
				{
					ChibiSway[2] = true;
					ChibiSway[6] = false;
					ChibiSway[3] = false;
					ChibiSway[5] = true;
					ChibiSway[4] = true;
				}
				else if (MyAudio.time > 31.833333)
				{
					ChibiSway[2] = true;
					ChibiSway[6] = false;
					ChibiSway[3] = false;
					ChibiSway[5] = false;
					ChibiSway[4] = false;
				}
				else if (MyAudio.time > 30.33333)
				{
					ChibiSway[2] = false;
					ChibiSway[6] = false;
					ChibiSway[3] = false;
					ChibiSway[5] = false;
					ChibiSway[4] = false;
				}
				else if (MyAudio.time > 28.2833333)
				{
					ChibiSway[2] = true;
					ChibiSway[6] = true;
					ChibiSway[3] = false;
					ChibiSway[5] = false;
					ChibiSway[4] = false;
				}
				else if (MyAudio.time > 7.1166666)
				{
					ChibiSway[2] = true;
					ChibiSway[6] = true;
					ChibiSway[3] = true;
					ChibiSway[5] = true;
					ChibiSway[4] = false;
				}
				else if (MyAudio.time > 3.5833333)
				{
					ChibiSway[2] = true;
					ChibiSway[6] = true;
					ChibiSway[3] = false;
					ChibiSway[5] = false;
					ChibiSway[4] = false;
				}
				else if (MyAudio.time > 0)
				{
					ChibiSway[2] = true;
					ChibiSway[6] = false;
					ChibiSway[3] = false;
					ChibiSway[5] = false;
					ChibiSway[4] = false;
				}

				     if (MyAudio.time > 33          && MyAudio.time < 36.833333f)   {ChibiSway[1] = true;}
				else if (MyAudio.time > 39.5f       && MyAudio.time < 43.25f)       {ChibiSway[1] = true;}
				else if (MyAudio.time > 46.833333f  && MyAudio.time < 49.75f)       {ChibiSway[1] = true;}
				else if (MyAudio.time > 50.3833333f && MyAudio.time < 53)           {ChibiSway[1] = true;}
				else if (MyAudio.time > 53.9166666f && MyAudio.time < 59)           {ChibiSway[1] = true;}
				else if (MyAudio.time > 59.5f       && MyAudio.time < 74.33333f)    {ChibiSway[1] = true;}
				else if (MyAudio.time > 77          && MyAudio.time < 80.33333f)    {ChibiSway[1] = true;}
				else if (MyAudio.time > 84.05f      && MyAudio.time < 88.166666f)   {ChibiSway[1] = true;}
				else if (MyAudio.time > 91          && MyAudio.time < 98.5f)        {ChibiSway[1] = true;}
				else if (MyAudio.time > 101.833333f && MyAudio.time < 130.5833333f) {ChibiSway[1] = true;}
				else                                                                {ChibiSway[1] = false;}

				if (CringeTimer == 0)
				{
					MyAudio.volume = 1;
				}

				ID = 1;

				while (ID < ChibiSway.Length)
				{
					if (CringeTimer > 0)
					{
						ChibiRenderer[ID].transform.localPosition = new Vector3(
							Random.Range(-.01f, .01f),
							.15f + Random.Range(-.01f, .01f),
							0);

						CringeTimer = Mathf.MoveTowards(CringeTimer, 0, Time.deltaTime);

						if (CringeTimer == 0)
						{
							ChibiRenderer[ID].transform.localPosition = new Vector3(0, .15f, 0);
						}
					}
					else
					{
						if (ChibiSway[ID])
						{
							if (!MusicNotes[ID].isPlaying)
							{
								MusicNotes[ID].Play();
							}

							AnimTimer[ID] += Time.deltaTime;

							if (AnimTimer[ID] > .2f)
							{
								FrameB[ID] = !FrameB[ID];
								AnimTimer[ID] = 0;
							}

							if (FrameB[ID])
							{
								ChibiRenderer[ID].material.mainTexture = ChibiPerform[ID];
							}
							else
							{
								ChibiRenderer[ID].material.mainTexture = ChibiPerformB[ID];
							}

							if (ID < 6)
							{
								if (Ping[ID])
								{
									PingPong[ID] += Time.deltaTime * 5;
									if (PingPong[ID] > 1){Ping[ID] = false;}
								}
								else
								{
									PingPong[ID] -= Time.deltaTime * 5;
									if (PingPong[ID] < -1){Ping[ID] = true;}
								}

								Rotation[ID] += PingPong[ID] * Time.deltaTime * 10;

								if (Rotation[ID] > 7.5f){Rotation[ID] = 7.5f;}
								else if (Rotation[ID] < -7.5f){Rotation[ID] = -7.5f;}
							}
						}
						else
						{
							if (ID < 6)
							{
								Rotation[ID] = Mathf.MoveTowards(Rotation[ID], 0, Time.deltaTime * 100);
							}

							if (ChibiRenderer[ID].material.mainTexture != ChibiIdle[ID])
							{
								ChibiRenderer[ID].material.mainTexture = ChibiIdle[ID];
								MusicNotes[ID].Stop();
								PingPong[ID] = -1;
								Ping[ID] = false;
							}
						}
					}

					Instruments[ID].localEulerAngles = new Vector3(0, 0, Rotation[ID]);

					ID++;
				}
			}

			if (SettingNotes)
			{
				if (Input.GetKeyDown("up"))
				{
					if (Phase == 1)
					{
						Phase1Times[Note] = MyAudio.time;
						Phase1Notes[Note] = 1;
					}
					else
					{
						Phase2Times[Note] = MyAudio.time;
						Phase2Notes[Note] = 1;
					}

					Note++;
				}
				else if (Input.GetKeyDown("right"))
				{
					if (Phase == 1)
					{
						Phase1Times[Note] = MyAudio.time;
						Phase1Notes[Note] = 2;
					}
					else
					{
						Phase2Times[Note] = MyAudio.time;
						Phase2Notes[Note] = 2;
					}

					Note++;
				}
				else if (Input.GetKeyDown("left"))
				{
					if (Phase == 1)
					{
						Phase1Times[Note] = MyAudio.time;
						Phase1Notes[Note] = 3;
					}
					else
					{
						Phase2Times[Note] = MyAudio.time;
						Phase2Notes[Note] = 3;
					}

					Note++;
				}
				else if (Input.GetKeyDown("down"))
				{
					if (Phase == 1)
					{
						Phase1Times[Note] = MyAudio.time;
						Phase1Notes[Note] = 4;
					}
					else
					{
						Phase2Times[Note] = MyAudio.time;
						Phase2Notes[Note] = 4;
					}

					Note++;
				}
			}
			else
			{
				if (Input.GetKeyUp("up") || Input.GetKeyUp("right") ||
				    Input.GetKeyUp("down") || Input.GetKeyUp("left"))
				{
					KeyDown = false;
				}

				if (InputManager.TappedUp == false && InputManager.TappedDown == false &&
					InputManager.TappedLeft == false && InputManager.TappedRight == false)
				{
					KeyDown = false;
				}

				if (Note < Notes.Length && Notes[Note] > 0)
				{
					if (Timer + 2 > Times[Note])
					{
						GameObject NewNote = Instantiate(NoteIcons[Notes[Note]], transform.position, Quaternion.identity);

						NewNote.GetComponent<MusicNoteScript>().InputManager = InputManager;
						NewNote.GetComponent<MusicNoteScript>().MusicMinigame = this;
						NewNote.GetComponent<MusicNoteScript>().ID = Note;

						NewNote.transform.parent = Scales[0].parent;

						     if (Notes[Note] == 1){NewNote.transform.localPosition = new Vector3(1.5f,  .15f, -.0001f);}
						else if (Notes[Note] == 2){NewNote.transform.localPosition = new Vector3(1.5f,  .05f, -.0001f);}
						else if (Notes[Note] == 3){NewNote.transform.localPosition = new Vector3(1.5f, -.05f, -.0001f);}
						else if (Notes[Note] == 4){NewNote.transform.localPosition = new Vector3(1.5f, -.15f, -.0001f);}

						NewNote.transform.localEulerAngles = new Vector3(0, 0, 0);
						NewNote.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

						Note++;
					}
				}
			}
		}
		//If we won...
		else
		{
			ID = 1;

			while (ID < Instruments.Length)
			{
				if (ID != 2 && ID != 6)
				{
					ChibiRenderer[ID].transform.localPosition += new Vector3(0, Jump[ID], 0);
					Jump[ID] -= Time.deltaTime * .01f;

					if (ChibiRenderer[ID].transform.localPosition.y < .15f)
					{
						ChibiRenderer[ID].transform.localPosition = new Vector3(0, .15f, 0);
						Jump[ID] = JumpStrength;
					}
				}

				ID++;
			}

			if (!MyAudio.isPlaying)
			{
				if (Timer == 0)
				{
					StartRep = PlayerPrefs.GetFloat("TempReputation");

					CurrentRep.text = "" + StartRep;

					if (Health > 100)
					{
						RepBonus.text = "+" + (Health - 100);
					}

					ReputationMarker.localPosition = new Vector3(StartRep * .01f, 0, 0);
				}

				ReputationBar.localScale = Vector3.Lerp(ReputationBar.localScale, new Vector3(1, 1, 1), Time.deltaTime * 10);

				Timer += Time.deltaTime;

				if (Timer > 1)
				{
					if (Health > 100)
					{
						float NewRep = StartRep + (Health - 100);

						if (NewRep > 100)
						{
							NewRep = 100;
						}

						CurrentRep.text = "" + NewRep;

						Power += Time.deltaTime;

						ReputationMarker.localPosition = Vector3.Lerp(
							ReputationMarker.localPosition,
							new Vector3(NewRep * .01f, 0, -.0002f),
							Power);
					}
				}

				if (Timer > 5)
				{
					Alpha = Mathf.MoveTowards(Alpha, 1, Time.deltaTime);
					Black.material.color = new Color(0, 0, 0, Alpha);

					if (Alpha == 1)
					{
						Quit();
					}
				}
			}
		}
	}

	public void UpdateHealthBar()
	{
		if (Health > 200)
		{
			Health = 200;
		}

		if (Health <= 0)
		{
			MyAudio.volume = 1;
			GameOver = true;
			Health = 0;
			Timer = 0;
		}
		else
		{
			HealthBar.localScale = new Vector3(1, Health / 200.0f, 1);

			HealthBarRenderer.material.color = new Color(1 - (Health / 200.0f), (Health / 200.0f), 0, 1);
		}

		if (Health > 100){Stars[1].material.mainTexture = GoldStar;}
		else{Stars[1].material.mainTexture = EmptyStar;}

		if (Health > 125){Stars[2].material.mainTexture = GoldStar;}
		else{Stars[2].material.mainTexture = EmptyStar;}

		if (Health > 150){Stars[3].material.mainTexture = GoldStar;}
		else{Stars[3].material.mainTexture = EmptyStar;}

		if (Health > 175){Stars[4].material.mainTexture = GoldStar;}
		else{Stars[4].material.mainTexture = EmptyStar;}

		if (Health == 200){Stars[5].material.mainTexture = GoldStar;}
		else{Stars[5].material.mainTexture = EmptyStar;}
	}

	public void Cringe()
	{
		ID = 1;

		while (ID < ChibiRenderer.Length)
		{
			ChibiRenderer[ID].material.mainTexture = ChibiCringe[ID];
			MusicNotes[ID].Stop();
			Rotation[ID] = 0;

			ID++;
		}

		MyAudio.volume = 0;
		CringeTimer = 1;
	}

	public void Quit()
	{
		if (Health > 100)
		{
			PlayerPrefs.SetFloat("TempReputation", StartRep + (Health - 100));
		}
		else
		{
			PlayerPrefs.SetFloat("TempReputation", 0);
		}

		foreach(GameObject g in SceneManager.GetActiveScene().GetRootGameObjects())
		{
			g.SetActive(true);
		}

		//This shouldn't be a magic number;
		//this should point to the number of
		//the Light Music Club minigame
		SceneManager.UnloadSceneAsync(23);
	}
}