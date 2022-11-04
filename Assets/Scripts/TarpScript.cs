using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarpScript : MonoBehaviour
{
	public PromptScript Prompt;

	public MechaScript Mecha;

	public AudioClip Tarp;

	public float PreviousSpeed = 0.0f;
	public float Speed = 0.0f;

	public bool Unwrap = false;

	void Start ()
	{
		transform.localScale = new Vector3(1, 1, 1);
	}

	void Update ()
	{
		if (Prompt.Circle[0].fillAmount == 0)
		{
			AudioSource.PlayClipAtPoint(Tarp, transform.position);

			Unwrap = true;

			Prompt.Hide();
			Prompt.enabled = false;

			Mecha.enabled = true;
			Mecha.Prompt.enabled = true;
		}

		if (Unwrap)
		{
			Speed += Time.deltaTime * 10;

			transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles,
				new Vector3(90, 90, 0),
				Time.deltaTime * Speed);

			if (transform.localEulerAngles.x > 45)
			{
				if (PreviousSpeed == 0)
				{
					PreviousSpeed = Speed;
				}

				Speed += Time.deltaTime  * 10;

				transform.localScale = Vector3.Lerp(transform.localScale,
					new Vector3(1, 1, .0001f),
					(Speed - PreviousSpeed) * Time.deltaTime);
			}
		}
	}
}