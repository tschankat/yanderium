using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainScript : MonoBehaviour
{
	public SkinnedMeshRenderer[] Curtains;

	public PromptScript Prompt;

	public AudioSource MyAudio;

	public bool Animate;
	public bool Open;

	public float Weight;

	void Update ()
	{
		if (Prompt.Circle[0].fillAmount == 0)
		{
			Prompt.Circle[0].fillAmount = 1;

			MyAudio.Play();

			Animate = true;

			Open = !Open;
		}

		if (Animate)
		{
			if (!Open)
			{
				Weight = Mathf.Lerp(Weight, 0.0f, Time.deltaTime * 10.0f);

				if (Weight < .01f)
				{
					Animate = false;
					Weight = 0;
				}
			}
			else
			{
				Weight = Mathf.Lerp(Weight, 100.0f, Time.deltaTime * 10.0f);

				if (Weight > 99.99f)
				{
					Animate = false;
					Weight = 100;
				}
			}

			Curtains[0].SetBlendShapeWeight(0, Weight);
			Curtains[1].SetBlendShapeWeight(0, Weight);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 13 || other.gameObject.layer == 9)
		{
			if (!Open)
			{
				MyAudio.Play();
				Animate = true;
				Open = true;
			}
		}
	}
}