using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptManagerScript : MonoBehaviour
{
	public PromptScript[] Prompts;
	public int ID;

	public Transform Yandere;

	public bool Outside;

	void Update ()
	{
		if (Yandere.transform.position.z < -38)
		{
			if (!Outside)
			{
				Outside = true;

				foreach (PromptScript prompt in Prompts)
				{
					if (prompt != null)
					{
						prompt.enabled = false;
					}
				}
			}
		}
		else
		{
			if (Outside)
			{
				Outside = false;

				foreach (PromptScript prompt in Prompts)
				{
					if (prompt != null)
					{
						prompt.enabled = true;
					}
				}
			}
		}
	}
}