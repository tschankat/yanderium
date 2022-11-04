using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideCutsceneScript : MonoBehaviour
{
	public Light PointLight;

	public Transform Door;

	public float Timer;

	public float Rotation;
	public float Speed;

	void Start()
	{
		PointLight.color = new Color(.1f, .1f, .1f, 1);
		Door.eulerAngles = new Vector3 (0, 0, 0);
	}

	void Update()
	{
		Timer += Time.deltaTime;

		if (Timer > 2)
		{
			Speed += Time.deltaTime;

			Rotation = Mathf.Lerp(Rotation, -45,Time.deltaTime * Speed);
			PointLight.color = new Color(
				.1f + (Rotation / -45) * .9f, 
				.1f + (Rotation / -45) * .9f, 
				.1f + (Rotation / -45) * .9f, 
				1);

			Door.eulerAngles = new Vector3(0, Rotation, 0);
		}
	}
}
