using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuzzleScript : MonoBehaviour
{
	public Vector3 OriginalRotation;

	public float Rotate;
	public float Limit;
	public float Speed;

	bool Down;

	void Start()
	{
		OriginalRotation = transform.localEulerAngles;
	}

	void Update ()
	{
		if (!Down)
		{
			Rotate += Time.deltaTime * Speed;

			if (Rotate > Limit)
			{
				Down = true;
			}
		}
		else
		{
			Rotate -= Time.deltaTime * Speed;

			if (Rotate < -1 * Limit)
			{
				Down = false;
			}
		}

		transform.localEulerAngles = OriginalRotation + new Vector3(Rotate, 0, 0);
	}
}