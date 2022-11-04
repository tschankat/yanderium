using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushSpawnerScript : MonoBehaviour
{
	public GameObject Bush;

	public bool Begin;

	void Update ()
	{
		if (Input.GetKeyDown("z"))
		{
			Begin = true;
		}

		if (Begin)
		{
			Instantiate(Bush, new Vector3(Random.Range(-16.0f, 16.0f), Random.Range(0.0f, 4.0f), Random.Range(-16.0f, 16.0f)), Quaternion.identity);
		}
	}
}