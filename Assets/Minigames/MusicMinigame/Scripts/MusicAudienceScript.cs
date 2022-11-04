using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAudienceScript : MonoBehaviour
{
	public MusicMinigameScript MusicMinigame;

	public float JumpStrength;
	public float Threshold;
	public float Minimum;
	public float Jump;

	void Start()
	{
		JumpStrength += Random.Range(-.0001f, .0001f);
	}

	void Update ()
	{
		if (MusicMinigame.Health >= Threshold)
		{
			Minimum = Mathf.MoveTowards(Minimum, .2f, Time.deltaTime * .1f);
		}
		else
		{
			Minimum = Mathf.MoveTowards(Minimum, 0, Time.deltaTime * .1f);
		}

		transform.localPosition += new Vector3(0, Jump, 0);
		Jump -= Time.deltaTime * .01f;

		if (transform.localPosition.y < Minimum)
		{
			transform.localPosition = new Vector3(transform.localPosition.x, Minimum, 0);
			Jump = JumpStrength;
		}
	}
}