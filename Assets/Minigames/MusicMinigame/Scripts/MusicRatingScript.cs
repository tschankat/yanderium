using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicRatingScript : MonoBehaviour
{
	public Renderer MyRenderer;

	public AudioSource SFX;

	public float Speed;
	public float Timer;

	void Start()
	{
		if (SFX != null)
		{
			SFX.pitch = Random.Range(.9f, 1.1f);
		}
	}

	void Update()
	{
		transform.localPosition += new Vector3(0, Speed * Time.deltaTime, 0);
		transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(.2f, .1f, .1f), Time.deltaTime);

		Timer += Time.deltaTime * 5;

		if (Timer > 1)
		{
			MyRenderer.material.color = new Color(1, 1, 1, 2 - Timer);

			if (MyRenderer.material.color.a <= 0)
			{
				Destroy(gameObject);
			}
		}
	}
}