using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalKnifeArrayScript : MonoBehaviour
{
	public TimeStopKnifeScript[] Knives;

	public int ID;

	public void ActivateKnives()
	{
		foreach (TimeStopKnifeScript knife in Knives)
		{
			if (knife != null)
			{
				knife.Unfreeze = true;
			}
		}

		ID = 0;
	}
}