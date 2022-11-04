using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePortalScript : MonoBehaviour
{
	public DelinquentScript[] Delinquent;

	public GameObject BlackHole;

	public float Timer;

 	public bool Suck;

	public int ID;

	void Update ()
	{
		if (Input.GetKeyDown("space"))
		{
			Suck = true;
		}

		if (Suck)
		{
			Instantiate(BlackHole, transform.position + new Vector3(0, 1, 0), Quaternion.identity);

			Timer += Time.deltaTime;

			if (Timer > 1.1f)
			{
				Delinquent[ID].Suck = true;
				Timer = 1;
				ID++;

				if (ID > 9)
				{
					enabled = false;
				}
			}
		}
	}
}
