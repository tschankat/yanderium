using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedStringScript : MonoBehaviour
{
	public Transform Target;
	
	void LateUpdate ()
	{
		transform.LookAt(Target.position);
		transform.localScale = new Vector3(1, 1, Vector3.Distance(transform.position, Target.position));
	}
}