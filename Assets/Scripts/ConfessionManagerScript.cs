using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfessionManagerScript : MonoBehaviour
{
#if UNITY_EDITOR
	public ShoulderCameraScript ShoulderCamera;
	public StudentManagerScript StudentManager;
	public HeartbrokenScript Heartbroken;
	public JukeboxScript OriginalJukebox;
	public CosmeticScript OsanaCosmetic;

	public AudioClip ConfessionAccepted;
	public AudioClip ConfessionRejected;
	public AudioClip ConfessionGiggle;

	public AudioClip[] ConfessionMusic;

	public GameObject OriginalBlossoms;
	public GameObject HeartBeatCamera;
	public GameObject MainCamera;

	public Transform ConfessionCamera;
	public Transform OriginalPOV;
	public Transform ReactionPOV;
	public Transform SenpaiNeck;
	public Transform SenpaiPOV;

	public string[] ConfessSubs;
	public string[] AcceptSubs;
	public string[] RejectSubs;

	public float[] ConfessTimes;
	public float[] AcceptTimes;
	public float[] RejectTimes;

	public UISprite TimelessDarkness;
	public UILabel SubtitleLabel;
	public UISprite Darkness;
	public UIPanel Panel;

	public AudioSource MyAudio;
	public AudioSource Jukebox;

	public Animation Yandere;
	public Animation Senpai;
	public Animation Osana;

	public Renderer Tears;

	public float RotateSpeed;
	public float TearSpeed;
	public float TearTimer;
	public float Timer;

	public bool CheatRejection;
	public bool ReverseTears;
	public bool FadeOut;
	public bool Reject;

	public int TearPhase;
	public int Phase;

	public int MusicID;
	public int SubID;

	void Start()
	{
		Senpai["SenpaiConfession"].speed = .9f;

		//ConfessionCamera.gameObject.SetActive(true);

		TimelessDarkness.color = new Color(0, 0, 0, 0);
		Darkness.color = new Color(0, 0, 0, 1);

		SubtitleLabel.text = "";
	}

	void Update()
	{
		Timer += Time.deltaTime;

		if (Phase == -1)
		{
			this.TimelessDarkness.color = new Color(
				this.TimelessDarkness.color.r,
				this.TimelessDarkness.color.g,
				this.TimelessDarkness.color.b,
				Mathf.MoveTowards(this.TimelessDarkness.color.a, 1.0f, Time.deltaTime));

			this.Panel.alpha = Mathf.MoveTowards(this.Panel.alpha, 0.0f, Time.deltaTime);
			this.OriginalJukebox.Volume = Mathf.MoveTowards(this.OriginalJukebox.Volume, 0.0f, Time.deltaTime);

			if (this.TimelessDarkness.color.a == 1.0f)
			{
				if (this.Timer > 2.0f)
				{
					TimelessDarkness.color = new Color(0, 0, 0, 0);
					Darkness.color = new Color(0, 0, 0, 1);

					ConfessionCamera.gameObject.SetActive(true);
					MainCamera.SetActive(false);

					OsanaCosmetic = StudentManager.Students[StudentManager.RivalID].Cosmetic;
					Osana = StudentManager.Students[StudentManager.RivalID].CharacterAnimation;
					Tears = StudentManager.Students[StudentManager.RivalID].Tears;
					Senpai = StudentManager.Students[1].CharacterAnimation;
					SenpaiNeck = StudentManager.Students[1].Neck;

					Osana[OsanaCosmetic.Student.ShyAnim].weight = 0.0f;
					Senpai["SenpaiConfession"].speed = .9f;
					OriginalBlossoms.SetActive(false);
					Tears.gameObject.SetActive(true);

					Osana.transform.position = new Vector3(0, 6, 98.5f + 21);
					Senpai.transform.position = new Vector3(0, 6, 98.5f + 21);

					Osana.transform.eulerAngles = new Vector3(0, 180, 0);
					Senpai.transform.eulerAngles = new Vector3(0, 180, 0);

					OsanaCosmetic.MyRenderer.materials[OsanaCosmetic.FaceID].SetFloat("_BlendAmount", 1.0f);

					Debug.Log("The characters were told to perform their confession animations.");

					Senpai.Play("SenpaiConfession");
					Osana.Play("OsanaConfession");

					OriginalBlossoms.SetActive(false);

					this.HeartBeatCamera.SetActive(false);
					//this.ConfessionBG.SetActive(true);

					this.GetComponent<AudioSource>().Play();
					this.Jukebox.Play();

					//this.MainCamera.position = this.CameraDestinations[1].position;
					//this.MainCamera.eulerAngles = this.CameraDestinations[1].eulerAngles;
					this.Timer = 0.0f;
					this.Phase++;

					this.Yandere.transform.parent.position = new Vector3(5, 5.73f, 98 + 21);
					this.Yandere.transform.parent.eulerAngles = new Vector3(0, -90, 0);
				}
			}
		}
		else if (Phase == 0)
		{
			if (Timer > 11)
			{
				if (!CheatRejection)
				{
					FadeOut = true;
					Timer = 0;
					Phase++;
				}
				else
				{
					if (Osana["OsanaConfessionRejected"].time < 45)
					{
						Senpai.CrossFade("SenpaiConfessionRejected", 1);
						Osana["OsanaConfessionRejected"].time = 45;
						Osana.CrossFade("OsanaConfessionRejected", 1);
					}
				}
			}
			else
			{
				//Senpai.Play("SenpaiConfession");
				//Osana.Play("OsanaConfession");
			}
		}
		else if (Phase == 1)
		{
			if (Timer > 2)
			{
				ConfessionCamera.eulerAngles = SenpaiPOV.eulerAngles;
				ConfessionCamera.position = SenpaiPOV.position;
				Senpai.gameObject.SetActive(false);
				Osana["OsanaConfession"].time = 11;
				MyAudio.volume = 1;
				MyAudio.time = 8;
				FadeOut = false;
				Timer = 0;
				Phase++;
			}
		}
		else if (Phase == 2)
		{
			if (SubID < ConfessTimes.Length)
			{
				if (Osana["OsanaConfession"].time > ConfessTimes[SubID] + 3)
				{
					SubtitleLabel.text = "" + ConfessSubs[SubID];
					SubID++;
				}
			}

			RotateSpeed += Time.deltaTime * .2f;

			ConfessionCamera.eulerAngles = Vector3.Lerp(ConfessionCamera.eulerAngles, new Vector3(0, 0, 0), Time.deltaTime * RotateSpeed);
			ConfessionCamera.position = Vector3.Lerp(ConfessionCamera.position, new Vector3(0, 7.25f, 97 + 21), Time.deltaTime * RotateSpeed);

			#if UNITY_EDITOR

			if (Input.GetKeyDown("z"))
			{
				if (DatingGlobals.RivalSabotaged == 5)
				{
					DatingGlobals.RivalSabotaged = 0;
				}
				else
				{
					DatingGlobals.RivalSabotaged = 5;
				}

				Debug.Log("Sabotage Progress: " + DatingGlobals.RivalSabotaged + "/5");
			}

			if (Input.GetKeyDown("space"))
			{
				Osana["OsanaConfession"].time = Osana["OsanaConfession"].length - 1;
				MyAudio.time = MyAudio.clip.length - 1;
			}

			#endif

			if (Osana["OsanaConfession"].time >= Osana["OsanaConfession"].length)
			{
				if (DatingGlobals.RivalSabotaged > 4)
				{
					Reject = true;
				}

				if (!Reject)
				{
					Osana.CrossFade("OsanaConfessionAccepted");
					MyAudio.clip = ConfessionAccepted;
				}
				else
				{
					Osana.CrossFade("OsanaConfessionRejected");
					MyAudio.clip = ConfessionRejected;
				}
					
				MyAudio.time = 0;
				MyAudio.Play();
				Jukebox.Stop();

				SubtitleLabel.text = "";
				RotateSpeed = 0;
				SubID = 0;
				Timer = 0;
				Phase++;
			}
		}
		else if (Phase == 3)
		{
			if (!Reject)
			{
				if (SubID < AcceptTimes.Length)
				{
					if (Osana["OsanaConfessionAccepted"].time > AcceptTimes[SubID])
					{
						SubtitleLabel.text = "" + AcceptSubs[SubID];
						SubID++;
					}
				}

				if (TearPhase == 0)
				{
					if (Timer > 26)
					{
						ReverseTears = true;
						TearSpeed = 5;
						TearPhase++;
					}
				}
				else if (TearPhase == 1)
				{
					if (Timer > 33.33333)
					{
						ReverseTears = true;
						TearSpeed = 5;
						TearPhase++;
					}
				}
				else if (TearPhase == 2)
				{
					if (Timer > 39)
					{
						ReverseTears = true;
						TearSpeed = 5;
						TearPhase++;
					}
				}
				else if (TearPhase == 3)
				{
					if (Timer > 40)
					{
						TearPhase++;
					}
				}

				if (Timer > 10)
				{
					if (!Jukebox.isPlaying)
					{
						Jukebox.clip = ConfessionMusic[4];
						Jukebox.loop = true;
						Jukebox.volume = 0;
						Jukebox.Play();
					}

					Jukebox.volume = Mathf.MoveTowards(Jukebox.volume, .05f, Time.deltaTime * .01f);

					if (!ReverseTears)
					{
						TearTimer = Mathf.MoveTowards(TearTimer, 1, Time.deltaTime * TearSpeed);
					}
					else
					{
						TearTimer = Mathf.MoveTowards(TearTimer, 0, Time.deltaTime * TearSpeed);

						if (TearTimer == 0)
						{
							ReverseTears = false;
							TearSpeed = .2f;
						}
					}

					if (TearPhase < 4)
					{
						Tears.materials[0].SetFloat ("_TearReveal", TearTimer);
					}

					Tears.materials[1].SetFloat("_TearReveal", TearTimer);
				}

				if (Input.GetKeyDown("space"))
				{
					Jukebox.clip = ConfessionMusic[4];
					Jukebox.loop = true;
					Jukebox.volume = .05f;
					Jukebox.Play();

					Osana["OsanaConfessionAccepted"].time = 43;
					MyAudio.Stop();
					Timer = 43;
				}

				if (Timer > 43)
				{
					TearSpeed = .1f;
					FadeOut = true;
					Timer = 0;
					Phase++;
				}
			}
			//If Senpai is rejecting Osana...
			else
			{
				if (SubID < RejectTimes.Length)
				{
					if (Osana["OsanaConfessionRejected"].time > RejectTimes[SubID])
					{
						SubtitleLabel.text = "" + RejectSubs[SubID];
						SubID++;
					}
				}

				if (Input.GetKeyDown("space"))
				{
					Osana["OsanaConfessionRejected"].time = 41;
					MyAudio.time = 41;
					Timer = 41;
				}

				if (Timer > 41)
				{
					TearTimer = Mathf.MoveTowards(TearTimer, 1, Time.deltaTime * TearSpeed);
					Tears.materials[0].SetFloat ("_TearReveal", TearTimer);
					Tears.materials[1].SetFloat("_TearReveal", TearTimer);
				}

				if (Timer > 47)
				{
					RotateSpeed += Time.deltaTime * .01f;

					ConfessionCamera.eulerAngles = new Vector3(
						ConfessionCamera.eulerAngles.x,
						ConfessionCamera.eulerAngles.y - RotateSpeed * 2,
						ConfessionCamera.eulerAngles.z);

					ConfessionCamera.position = new Vector3(
						ConfessionCamera.position.x,
						ConfessionCamera.position.y,
						ConfessionCamera.position.z - RotateSpeed * .05f);
				}

				if (Timer > 51)
				{
					FadeOut = true;
					Timer = 0;
					Phase++;
				}
			}
		}
		else if (Phase == 4)
		{
			if (Reject)
			{
				RotateSpeed += Time.deltaTime * .01f;

				ConfessionCamera.eulerAngles = new Vector3(
					ConfessionCamera.eulerAngles.x,
					ConfessionCamera.eulerAngles.y - RotateSpeed * 2,
					ConfessionCamera.eulerAngles.z);

				ConfessionCamera.position = new Vector3(
					ConfessionCamera.position.x,
					ConfessionCamera.position.y,
					ConfessionCamera.position.z - RotateSpeed * .05f);
			}

			if (Timer > 2)
			{
				ConfessionCamera.eulerAngles = OriginalPOV.eulerAngles;
				ConfessionCamera.position = OriginalPOV.position;
				Senpai.gameObject.SetActive(true);

				if (!Reject)
				{
					Senpai.Play("SenpaiConfessionAccepted");
					Senpai["SenpaiConfessionAccepted"].time = Osana["OsanaConfessionAccepted"].time;
					Senpai.Play("SenpaiConfessionAccepted");

					Yandere.Play("YandereConfessionAccepted");
				}
				else
				{
					Senpai.Play("SenpaiConfessionRejected");
					Senpai["SenpaiConfessionRejected"].time += 2;
				}

				SubtitleLabel.text = "";
				FadeOut = false;
				RotateSpeed = 0;
				Timer = 0;
				Phase++;
			}
		}
		else if (Phase == 5)
		{
			if (Timer > 5)
			{
				if (Reject)
				{
					Yandere.Play("YandereConfessionRejected");
				}

				Jukebox.pitch = Mathf.MoveTowards(Jukebox.pitch, 0, Time.deltaTime * .1f);
				RotateSpeed += Time.deltaTime * .5f;

				ConfessionCamera.position = Vector3.Lerp(ConfessionCamera.position, new Vector3(7, 7, 97.5f + 21), Time.deltaTime * RotateSpeed);

				if (Timer > 10)
				{
					if (Reject)
					{
						AudioSource.PlayClipAtPoint (ConfessionGiggle, Yandere.transform.position);
					}

					ConfessionCamera.eulerAngles = ReactionPOV.eulerAngles;
					ConfessionCamera.position = ReactionPOV.position;
					RotateSpeed = 0;
					Timer = 0;
					Phase++;
				}
			}
		}
		else if (Phase == 6)
		{
			Jukebox.pitch = Mathf.MoveTowards(Jukebox.pitch, 0, Time.deltaTime * .1f);

			if (!Reject)
			{
				if (!Heartbroken.Confessed)
				{
					MainCamera.transform.eulerAngles = ConfessionCamera.eulerAngles;
					MainCamera.transform.position = ConfessionCamera.position;
					//ConfessionCamera.gameObject.SetActive(false);
					Heartbroken.Confessed = true;
					MainCamera.SetActive(true);
					Camera.main.enabled = true;

					ShoulderCamera.enabled = true;
					ShoulderCamera.Noticed = true;
					ShoulderCamera.Skip = true;
				}

				ConfessionCamera.position = MainCamera.transform.position;
			}
			else
			{
				RotateSpeed += Time.deltaTime * .5f;
				ConfessionCamera.position = Vector3.Lerp(ConfessionCamera.position, new Vector3(4, 7, 98 + 21), Time.deltaTime * RotateSpeed);

				if (Timer > 5)
				{
					FadeOut = true;
				}
			}
		}

		if (FadeOut)
		{
			Darkness.color = new Color(0, 0, 0, Mathf.MoveTowards(Darkness.color.a, 1, Time.deltaTime *.5f));
		}
		else
		{
			Darkness.color = new Color(0, 0, 0, Mathf.MoveTowards(Darkness.color.a, 0, Time.deltaTime *.5f));
		}

		if (Input.GetKeyDown("-"))
		{
			Time.timeScale--;
			MyAudio.pitch--;
			Jukebox.pitch--;
		}

		if (Input.GetKeyDown("="))
		{
			Time.timeScale++;
			MyAudio.pitch++;
			Jukebox.pitch++;
		}
	}

	void LateUpdate()
	{
		if (Phase > 4)
		{
			if (Reject)
			{
				SenpaiNeck.eulerAngles = new Vector3(
					SenpaiNeck.eulerAngles.x + 15f,
					SenpaiNeck.eulerAngles.y,
					SenpaiNeck.eulerAngles.z);
			}
		}
	}
#endif
}