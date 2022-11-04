using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WritheScript : MonoBehaviour
{
	public float Rotation;

	public float StartTime;
	public float Duration;

	public float StartValue;
	public float EndValue;

	public int ID;

	public bool SpecialCase;

	void Start()
	{
		StartTime = Time.time;
		Duration = Random.Range(1.0f, 5.0f);
	}

	void Update()
	{
		if (Rotation == EndValue)
		{
			StartValue = EndValue;
			EndValue = Random.Range(-45.0f, 45.0f);

			StartTime = Time.time;
			Duration = Random.Range(1.0f, 5.0f);
		}

		/*
		if (Input.GetButtonDown(InputNames.Xbox_RB))
		{
			StartValue = Rotation;
			EndValue = 15;
			Duration = 1;
		}
		*/

		float t = (Time.time - StartTime) / Duration;

		Rotation = Mathf.SmoothStep(StartValue, EndValue, t);

		switch (ID)
		{
			case 1:
				transform.localEulerAngles = new Vector3 (
					Rotation,
					transform.localEulerAngles.y,
					transform.localEulerAngles.z);
			break;

			case 2:

				if (SpecialCase)
				{
					Rotation += 180;
				}

				transform.localEulerAngles = new Vector3 (
					transform.localEulerAngles.x,
					Rotation,
					transform.localEulerAngles.z);


			break;

			case 3:
				transform.localEulerAngles = new Vector3 (
					transform.localEulerAngles.x,
					transform.localEulerAngles.y,
					Rotation);
			break;

			default:
			break;
		}
	}
}