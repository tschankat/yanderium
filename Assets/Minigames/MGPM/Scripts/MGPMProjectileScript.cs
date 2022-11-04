using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGPMProjectileScript : MonoBehaviour
{
	public Transform Sprite;

	public int Angle;

	public float Speed;

	void Update ()
	{
		if (gameObject.layer == 8)
		{
			transform.Translate(Vector3.up * Time.deltaTime * Speed);
		}
		else
		{
			transform.Translate(Vector3.forward * Time.deltaTime * Speed);
		}

		if (Angle == 1)
		{
			transform.Translate(Vector3.right * Time.deltaTime * Speed * .2f);
		}
		else if (Angle == -1)
		{
			transform.Translate(Vector3.right * Time.deltaTime * Speed * -.2f);
		}

		if (transform.localPosition.y > 300 || transform.localPosition.y < -300 ||
			transform.localPosition.x > 134 || transform.localPosition.x < -134)
		{
			Destroy(gameObject);
		}

		#if UNITY_EDITOR

		if (Input.GetKeyDown("space"))
		{
			Destroy(gameObject);
		}

		#endif
	}
}