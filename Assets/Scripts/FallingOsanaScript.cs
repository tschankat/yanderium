using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingOsanaScript : MonoBehaviour
{
	public StudentScript Osana;
	public GameObject GroundImpact;

	void Update ()
	{
		if (transform.parent.position.y > 0)
		{
			Osana.CharacterAnimation.Play(Osana.IdleAnim);

			transform.parent.position += new Vector3 (0, -1.0001f, 0);
		}

		if (transform.parent.position.y < 0)
		{
			transform.parent.position = new Vector3(
				transform.parent.position.x,
				0,
				transform.parent.position.z);

			Instantiate(GroundImpact, transform.parent.position, Quaternion.identity);
		}
	}
}