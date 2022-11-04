using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SundayRivalCutsceneScript : MonoBehaviour
{
	#if UNITY_EDITOR
	public HomeYandereScript HomeYandere;
	public PhoneScript Phone;

	public GameObject GrabbyHand;
	public GameObject HomeClock;

	public GameObject InfoTextConvo;
	public GameObject InfoTextPanel;

	public AudioClip YoureSafeNow;
	public AudioSource Vibration;

	public float Alpha = 1;
	public float Speed;
	public float Timer;

	public float X;
	public float Y;
	public float Z;

	public int Phase;
	#endif

	void Start ()
	{
		
		if (DateGlobals.Weekday != System.DayOfWeek.Sunday)
		{
			gameObject.SetActive(false);
		}
		#if UNITY_EDITOR
		else
		{
			Vibration.gameObject.SetActive(true);

			HomeYandere.Start();
			HomeYandere.HomeCamera.Start();

			HomeClock.SetActive (false);
			HomeYandere.enabled = false;
			HomeYandere.HomeCamera.enabled = false;
			HomeYandere.HomeCamera.RoomJukebox.enabled = false;
			HomeYandere.HomeCamera.HomeSenpaiShrine.enabled = false;
			Destroy (HomeYandere.HomeCamera.PauseScreen.gameObject);

			HomeYandere.HomeCamera.HomeSenpaiShrine.RightDoor.localEulerAngles = new Vector3(
				HomeYandere.HomeCamera.HomeSenpaiShrine.RightDoor.localEulerAngles.x,
				135,
				HomeYandere.HomeCamera.HomeSenpaiShrine.RightDoor.localEulerAngles.z);

			HomeYandere.HomeCamera.HomeSenpaiShrine.LeftDoor.localEulerAngles = new Vector3(
				HomeYandere.HomeCamera.HomeSenpaiShrine.LeftDoor.localEulerAngles.x,
				-135,
				HomeYandere.HomeCamera.HomeSenpaiShrine.LeftDoor.localEulerAngles.z);

			HomeYandere.transform.position = new Vector3 (-1.655f, 0, 1.93f);
			HomeYandere.transform.eulerAngles = new Vector3 (0, -45, 0);

			HomeYandere.HomeCamera.transform.position = new Vector3 (-1.905f, 1.48f, 2.15f);
			HomeYandere.HomeCamera.transform.eulerAngles = new Vector3 (0, -22.5f, 0);
		}
		#endif
	}

	#if UNITY_EDITOR
	void Update ()
	{
		if (Input.GetKeyDown("="))
		{
			Time.timeScale++;
		}

		if (Phase < 4)
		{
			HomeYandere.HomeCamera.transform.Translate(
				Vector3.forward * Time.deltaTime * -.01f, Space.Self);
		}

		if (Phase == 0)
		{
			Alpha = Mathf.MoveTowards (Alpha, 0, Time.deltaTime * .25f);

			HomeYandere.HomeDarkness.color = new Color (0, 0, 0, Alpha);

			if (Alpha == 0)
			{
				Phase++;
			}
		}
		else if (Phase == 1)
		{
			Timer += Time.deltaTime;

			if (Timer > 1)
			{
				HomeYandere.Character.GetComponent<Animation>().Play("f02_caressPhoto_00");
				Timer = 0;
				Phase++;
			}
		}
		else if (Phase == 2)
		{
			Timer += Time.deltaTime;

			if (Timer > 2.5f)
			{
				Vibration.PlayOneShot(YoureSafeNow);
				Timer = 0;
				Phase++;
			}
		}
		else if (Phase == 3)
		{
			Timer += Time.deltaTime;

			if (Timer > 3)
			{
				if (!Vibration.isPlaying)
				{
					Vibration.Play();
				}
			}

			if (Timer > 4)
			{
				X = 0;
				Y = -22.5f;
				Z = 0;

				Timer = 0;

				Phase++;
			}
		}
		else if (Phase == 4)
		{
			Timer += Time.deltaTime;

			Speed += Time.deltaTime;

			HomeYandere.HomeCamera.transform.position = Vector3.Lerp(
				HomeYandere.HomeCamera.transform.position,
				new Vector3 (-1.966666f, 1.07f, 1.9433333f),
				Time.deltaTime * Speed);

			X = Mathf.Lerp(X, 67.5f, Time.deltaTime * Speed);
			Y = Mathf.Lerp(Y, -22.5f, Time.deltaTime * Speed);
			Z = Mathf.Lerp(Z, 0, Time.deltaTime * Speed);

			HomeYandere.HomeCamera.transform.eulerAngles = new Vector3(X, Y, Z);

			if (Timer > 2.5f)
			{
				GrabbyHand.SetActive(true);
			}

			if (Timer > 4.5f)
			{
				HomeYandere.HomeCamera.transform.position = new Vector3(
					-1.638197f,
					1.4925f,
					2);

				HomeYandere.HomeCamera.transform.eulerAngles = new Vector3(
					0,
					-45,
					0);

				HomeYandere.gameObject.SetActive(false);
				GrabbyHand.SetActive(false);

				InfoTextConvo.SetActive(true);

				Timer = 0;

				Phase++;
			}
		}
		else if (Phase == 5)
		{
			Timer += Time.deltaTime;

			InfoTextPanel.transform.localPosition = Vector3.Lerp (
				InfoTextPanel.transform.localPosition,
				new Vector3 (0, 0, 1),
				Time.deltaTime * 10);

			if (Timer > 1)
			{
				Phone.enabled = true;
				Time.timeScale = 1;
				Phase++;
			}
		}
	}
	#endif
}