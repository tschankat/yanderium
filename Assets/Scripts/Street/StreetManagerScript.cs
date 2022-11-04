using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class StreetManagerScript : MonoBehaviour
{
	public StreetShopInterfaceScript StreetShopInterface;

	public Transform BinocularCamera;
	public Transform Yandere;
	public Transform Hips;
	public Transform Sun;

	public Animation MaidAnimation;
	public Animation Gossip1;
	public Animation Gossip2;

	public AudioSource CurrentlyActiveJukebox;
	public AudioSource JukeboxNight;
	public AudioSource JukeboxDay;
	public AudioSource Yakuza;

	public HomeClockScript Clock;

	public Animation[] Civilian;

	public GameObject Couple;

	public UISprite Darkness;

	public Renderer Stars;

	public Light Sunlight;

	public bool Threatened;
	public bool GoToCafe;
	public bool FadeOut;
	public bool Day;

	public float Rotation;
	public float Timer;

	public float DesiredValue;
	public float StarAlpha;
	public float Alpha;

	void Start ()
	{
		MaidAnimation["f02_faceCouncilGrace_00"].layer = 1;
		MaidAnimation.Play("f02_faceCouncilGrace_00");
		MaidAnimation["f02_faceCouncilGrace_00"].weight = 1.0f;

		Gossip1["f02_socialSit_00"].layer = 1;
		Gossip1.Play("f02_socialSit_00");
		Gossip1["f02_socialSit_00"].weight = 1.0f;

		Gossip2["f02_socialSit_00"].layer = 1;
		Gossip2.Play("f02_socialSit_00");
		Gossip2["f02_socialSit_00"].weight = 1.0f;

		int ID = 2;

		while (ID < 5)
		{
			Civilian[ID]["f02_smile_00"].layer = 1;
			Civilian[ID].Play("f02_smile_00");
			Civilian[ID]["f02_smile_00"].weight = 1.0f;

			ID++;
		}

		Darkness.color = new Color(1, 1, 1, 1);
		CurrentlyActiveJukebox = JukeboxNight;
		Alpha = 1;

		if (StudentGlobals.GetStudentDead(30) ||
			StudentGlobals.GetStudentKidnapped(30) ||
			StudentGlobals.GetStudentBroken(81))
		{
			Couple.SetActive(false);
		}
			
		Sunlight.shadows = LightShadows.None;
	}

	void Update ()
	{
		//Gossip1.CrossFade("f02_carefreeTalkLoop_00");
		//Gossip2.CrossFade("f02_carefreeTalkLoop_01");

		if (Input.GetKeyDown("m"))
		{
			PlayerGlobals.Money++;
			Clock.UpdateMoneyLabel();

			if (JukeboxNight.isPlaying)
			{
				JukeboxNight.Stop();
				JukeboxDay.Stop();
			}
			else
			{
				JukeboxNight.Play();
				JukeboxDay.Stop();
			}
		}

		if (Input.GetKeyDown("f"))
		{
			PlayerGlobals.FakeID = !PlayerGlobals.FakeID;
			StreetShopInterface.UpdateFakeID();
		}

		Timer += Time.deltaTime;

		if (Timer > .5f)
		{
			if (Alpha == 1)
			{
				JukeboxNight.volume = .5f;
				JukeboxNight.Play();

				JukeboxDay.volume = 0;
				JukeboxDay.Play();
			}

			if (!FadeOut)
			{
				Alpha = Mathf.MoveTowards(Alpha, 0, Time.deltaTime);

				Darkness.color = new Color(1, 1, 1, Alpha);
			}
			else
			{
				Alpha = Mathf.MoveTowards(Alpha, 1, Time.deltaTime);

                if (!StreetShopInterface.Show)
                {
    				CurrentlyActiveJukebox.volume = (1 - Alpha) * .5f;
                }

                if (GoToCafe)
				{
					Darkness.color = new Color(1, 1, 1, Alpha);

					if (Alpha == 1)
					{		
						SceneManager.LoadScene(SceneNames.MaidMenuScene);
					}
				}
				else
				{
					Darkness.color = new Color(0, 0, 0, Alpha);

					if (Alpha == 1)
					{
						SceneManager.LoadScene(SceneNames.HomeScene);
					}
				}
			}
		}

		if (!FadeOut && !BinocularCamera.gameObject.activeInHierarchy)
		{
			//Debug.Log("Distance between Yandere-chan and the Yakuza is: " + Vector3.Distance(Yandere.position, Yakuza.transform.position));

			if (Vector3.Distance(Yandere.position, Yakuza.transform.position) > 5)
			{
				DesiredValue = .5f;
			}
			else
			{
				DesiredValue = Vector3.Distance(Yandere.position, Yakuza.transform.position) * .1f;
			}

            if (!StreetShopInterface.Show)
            {
                if (Day)
			    {
				    JukeboxDay.volume = Mathf.Lerp(JukeboxDay.volume, DesiredValue, Time.deltaTime * 10);
				    JukeboxNight.volume = Mathf.Lerp(JukeboxNight.volume, 0, Time.deltaTime * 10);
			    }
			    else
			    {
				    JukeboxDay.volume = Mathf.Lerp(JukeboxDay.volume, 0, Time.deltaTime * 10);
				    JukeboxNight.volume = Mathf.Lerp(JukeboxNight.volume, DesiredValue, Time.deltaTime * 10);
			    }
            }

            if (Vector3.Distance(Yandere.position, Yakuza.transform.position) < 1)
			{
				if (!Threatened)
				{
					Threatened = true;
					Yakuza.Play();
				}
			}
		}

		if (Input.GetKeyDown("space"))
		{
			Day = !Day;

			if (Day)
			{
				Clock.HourLabel.text = "12:00 PM";
				Sunlight.shadows = LightShadows.Soft;
			}
			else
			{
				Clock.HourLabel.text = "8:00 PM";
				Sunlight.shadows = LightShadows.None;
			}
		}

        if (StreetShopInterface.Show)
        {
            JukeboxNight.volume = Mathf.Lerp(JukeboxNight.volume, 0, Time.deltaTime * 10);
            JukeboxDay.volume = Mathf.Lerp(JukeboxDay.volume, 0, Time.deltaTime * 10);
        }
        else
        {
            if (Day)
		    {
			    CurrentlyActiveJukebox = JukeboxDay;
			    Rotation = Mathf.Lerp(Rotation, 45, Time.deltaTime * 10);
			    StarAlpha = Mathf.Lerp(StarAlpha, 0, Time.deltaTime * 10);
		    }
		    else
		    {
			    CurrentlyActiveJukebox = JukeboxNight;
			    Rotation = Mathf.Lerp(Rotation, -45, Time.deltaTime * 10);
			    StarAlpha = Mathf.Lerp(StarAlpha, 1, Time.deltaTime * 10);
		    }
        }

        Sun.transform.eulerAngles = new Vector3(Rotation, Rotation, 0);
		Stars.material.SetColor("_TintColor", new Color(1, 1, 1, StarAlpha));
	}

	void LateUpdate()
	{
		Hips.LookAt(BinocularCamera.position);
	}
}