using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSwitchScript : MonoBehaviour
{
	public DrinkingFountainScript DrinkingFountain;

	public PowerOutletScript PowerOutlet;

	public GameObject Electricity;

	public Light BathroomLight;

	public PromptScript Prompt;

	public AudioSource MyAudio;

	public AudioClip[] Flick;

	public bool On;

	void Update ()
	{
		if (Prompt.Circle[0].fillAmount == 0.0f)
		{
			Prompt.Circle[0].fillAmount = 1.0f;

			On = !On;

			if (On)
			{
				Prompt.Label[0].text = "     " + "Turn Off";
				MyAudio.clip = this.Flick[1];
			}
			else
			{
				this.Prompt.Label[0].text = "     " + "Turn On";
				MyAudio.clip = this.Flick[0];
			}

			if (BathroomLight != null)
			{
				BathroomLight.enabled = !BathroomLight.enabled;
			}

			CheckPuddle();

			MyAudio.Play();
		}
	}

	public void CheckPuddle()
	{
		if (On)
		{
			if (DrinkingFountain.Puddle != null)
			{
				if (DrinkingFountain.Puddle.gameObject.activeInHierarchy)
				{
					if (PowerOutlet.SabotagedOutlet.activeInHierarchy)
					{
						Electricity.SetActive(true);
					}
				}
			}
		}
		else
		{
			Electricity.SetActive(false);
		}
	}
}