using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTestScript : MonoBehaviour
{
	public Animation CharacterA;
	public Animation CharacterB;

	public int ID;

	void Start()
	{
		Time.timeScale = 1f;
	}

	void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			ID++;

			if (ID > 4)
			{
				ID = 1;
			}
		}

		if (ID == 1)
		{
			CharacterB.transform.eulerAngles = new Vector3(0, -90, 0);

			CharacterA.Play("f02_weightHighSanityA_00");
			CharacterB.Play("f02_weightHighSanityB_00");
		}
		else if (ID == 2)
		{
			CharacterA.Play("f02_weightMedSanityA_00");
			CharacterB.Play("f02_weightMedSanityB_00");
		}
		else if (ID == 3)
		{
			CharacterA.Play("f02_weightLowSanityA_00");
			CharacterB.Play("f02_weightLowSanityB_00");
		}
		else if (ID == 4)
		{
			CharacterB.transform.eulerAngles = new Vector3(0, 90, 0);

			CharacterA.Play("f02_weightStealthA_00");
			CharacterB.Play("f02_weightStealthB_00");
		}
	}
}