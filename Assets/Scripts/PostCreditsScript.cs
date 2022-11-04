using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class PostCreditsScript : MonoBehaviour
{
	#if UNITY_EDITOR
	public GameObject LovesickLogo;
	public GameObject Logo;

	public AudioSource Headmaster;
	public AudioSource Jukebox;
	public AudioSource Buzzing;

	public AudioClip CinematicHit;

	public Transform Destination;

	public UISprite Darkness;

	public UILabel Subtitle;

	public string[] Lines;
	public float[] Times;

	public float Rotation;
	public float Alpha;
	public float Speed;
	public float Timer;

	public int SpeechID;
	public int Phase;

	void Start()
	{
		Darkness.color = new Color (0, 0, 0, 1);
		Subtitle.text = "";
		Time.timeScale = 1;

		Logo.SetActive (false);
		LovesickLogo.SetActive (false);
	}

	void Update()
	{
		if (Input.GetKeyDown("="))
		{
			Time.timeScale++;
		}

		if (Input.GetKeyDown("-"))
		{
			Time.timeScale--;
		}

		Speed += Time.deltaTime * .001f;

		transform.position = Vector3.Lerp(
			transform.position,
			Destination.position,
			Time.deltaTime * Speed);

		Rotation = Mathf.Lerp(Rotation, -45, Time.deltaTime * Speed);

		transform.eulerAngles = new Vector3(0, Rotation, 0);

		if (Headmaster.time > 69)
		{
			Jukebox.volume -= Time.deltaTime * .2f;
		}

		if (Phase == 0)
		{
			if (Input.GetKeyDown("space"))
			{
				Alpha = 0;
			}

			Alpha = Mathf.MoveTowards(Alpha, 0, Time.deltaTime * .2f);

			Darkness.color = new Color(0, 0, 0, Alpha);

			if (Alpha == 0)
			{
				Subtitle.text = Lines[SpeechID];
				Headmaster.Play();
				SpeechID++;
				Phase++;
			}
		}
		else if (Phase == 1)
		{
			if (Input.GetKeyDown("space"))
			{
				Headmaster.time = 68;
			}

			Headmaster.pitch = Time.timeScale;

			if (Headmaster.time >= Times[SpeechID])
			{
				Subtitle.text = Lines[SpeechID];
				SpeechID++;

				if (SpeechID == 16)
				{
					Darkness.color = new Color (0, 0, 0, 1);
				}
				else if (SpeechID == 17)
				{
					Jukebox.clip = CinematicHit;
					Jukebox.volume = 1;
					Jukebox.Play();

					Logo.SetActive(true);
					Phase++;
				}
			}
		}
		else if (Phase == 2)
		{
			Timer += Time.deltaTime;

			if (Timer > 13)
			{
				SceneManager.LoadScene(SceneNames.ThanksForPlayingScene);
			}
			else if (Timer > 10)
			{
				Logo.SetActive (false);
			}
			else if (Timer > 6.4f)
			{
				Logo.SetActive (true);
				LovesickLogo.SetActive (false);

				Buzzing.volume = 0;
			}
			else if (Timer > 6.3f)
			{
				Logo.SetActive (false);
				LovesickLogo.SetActive (true);

				Buzzing.volume = 1;
			}
			else if (Timer > 6.2f)
			{
				Logo.SetActive (true);
				LovesickLogo.SetActive (false);

				Buzzing.volume = 0;
			}
			else if (Timer > 6.1f)
			{
				Logo.SetActive (false);
				LovesickLogo.SetActive (true);

				Buzzing.volume = 1;
			}
			else if (Timer > 5.1f)
			{
				Logo.SetActive (true);
				LovesickLogo.SetActive (false);

				Buzzing.volume = 0;
			}
			else if (Timer > 5)
			{
				Logo.SetActive (false);
				LovesickLogo.SetActive (true);

				Buzzing.Play();
			}

			Logo.transform.localScale += new Vector3(
				Time.deltaTime * .02f,
				Time.deltaTime * .02f,
				Time.deltaTime * .02f);
			
			LovesickLogo.transform.localScale += new Vector3(
				Time.deltaTime * .02f,
				Time.deltaTime * .02f,
				Time.deltaTime * .02f);
		}
	}
	#endif
}