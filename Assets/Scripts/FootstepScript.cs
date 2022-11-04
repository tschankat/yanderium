using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepScript : MonoBehaviour
{
	public StudentScript Student;

	public AudioSource MyAudio;

	public AudioClip[] WalkFootsteps;
	public AudioClip[] RunFootsteps;

	public float DownThreshold = 0.02f;
	public float UpThreshold = 0.025f;

	//public bool Debugging = false;
	public bool FootUp = false;

	void Start ()
	{
		if (!this.Student.Nemesis)
		{
			enabled = false;
		}
	}

	void Update ()
	{
		/*
		if (Debugging)
		{
			Debug.Log("UpThreshold: " + (Student.transform.position.y + UpThreshold).ToString() +
				" | DownThreshold: " + (Student.transform.position.y + DownThreshold).ToString() +
				" | CurrentHeight: " + transform.position.y.ToString());
		}
		*/

		if (!FootUp)
		{
			if (transform.position.y > (Student.transform.position.y + UpThreshold))
			{
				FootUp = true;
			}
		}
		else
		{
			if (transform.position.y < (Student.transform.position.y + DownThreshold))
			{
				if (FootUp)
				{
					if (Student.Pathfinding.speed > 1)
					{
						MyAudio.clip = RunFootsteps[Random.Range(0, RunFootsteps.Length)];
						MyAudio.volume = 0.2f;
					}
					else
					{
						MyAudio.clip = WalkFootsteps[Random.Range(0, WalkFootsteps.Length)];
						MyAudio.volume = 0.1f;
					}

					MyAudio.Play();
				}

				FootUp = false;
			}
		}
	}
}