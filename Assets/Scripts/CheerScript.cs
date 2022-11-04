using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheerScript : MonoBehaviour
{
	public AudioSource MyAudio;

	public AudioClip[] Cheers;

	public float Timer;

	void Update ()
	{
		Timer += Time.deltaTime;

		if (Timer > 5)
		{
			MyAudio.clip = Cheers[Random.Range(1, Cheers.Length)];
			MyAudio.Play();

			Timer = 0;
		}
	}
}