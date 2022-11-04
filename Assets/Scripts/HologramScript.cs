using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramScript : MonoBehaviour
{
	public GameObject[] Holograms;

	public void UpdateHolograms()
	{
		foreach (GameObject hologram in Holograms)
		{
			hologram.SetActive(TrueFalse());
		}
	}

	bool TrueFalse()
	{
		if (Random.value >= .5f)
		{
			return true;
		}

		return false;
	}
}