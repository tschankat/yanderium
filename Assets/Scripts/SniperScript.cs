using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;

	public float Timer;

	void Update ()
	{
		Timer += Time.deltaTime;

		if (Timer > 10)
		{
			//Anti-Osana Code
			#if !UNITY_EDITOR
			if (StudentManager.Students[10] != null)
			{
				StudentManager.Students[10].BecomeRagdoll();
			}

			if (StudentManager.Students[11] != null)
			{
				StudentManager.Students[11].BecomeRagdoll();
			}
			#endif

			Destroy(gameObject);
		}
	}
}