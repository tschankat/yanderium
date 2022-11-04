using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckmarkScript : MonoBehaviour
{
	public GameObject[] Checkmarks;
	public int ButtonPresses;
	public int ID;

	void Start ()
	{
		while (ID < Checkmarks.Length)
		{
			Checkmarks[ID].SetActive(false);
			ID++;
		}

		ID = 0;
	}

	void Update ()
	{
		if (Input.GetKeyDown("space"))
		{
			if (ButtonPresses < 26)
			{
				ButtonPresses++;

				ID = Random.Range (0, Checkmarks.Length - 4);

				while (Checkmarks[ID].active == true)
				{
					ID = Random.Range (0, Checkmarks.Length - 4);
				}

				Checkmarks[ID].SetActive (true);
			}
		}
	}
}
