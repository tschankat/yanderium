using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowYandereScript : MonoBehaviour
{
	public Transform Yandere;

	void Update ()
	{
		transform.position = new Vector3(Yandere.position.x, transform.position.y, Yandere.position.z);	
	}
}