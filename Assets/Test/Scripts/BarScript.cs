using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarScript : MonoBehaviour
{
	public float Speed;

	void Start ()
	{
		transform.localScale = new Vector3(0, 1, 1);
	}

	void Update ()
	{
		transform.localScale = new Vector3(transform.localScale.x + (Speed * Time.deltaTime), 1, 1);

		if (transform.localScale.x > .1)
		{
			transform.localScale = new Vector3(0, 1, 1);
		}
	}
}
