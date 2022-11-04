using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbFollowScript : MonoBehaviour
{
	public Transform Yandere;

	void Update ()
	{
		transform.position = new Vector3(
			transform.position.x,
			Yandere.position.y,
			transform.position.z);
	}
}
