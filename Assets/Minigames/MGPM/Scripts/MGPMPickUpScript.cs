using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGPMPickUpScript : MonoBehaviour
{
	public float Speed;

	void Update ()
	{
		transform.Translate(Vector3.up * Time.deltaTime * Speed * -1);

		if (transform.localPosition.y < -300)
		{
			Destroy(gameObject);
		}
	}
}