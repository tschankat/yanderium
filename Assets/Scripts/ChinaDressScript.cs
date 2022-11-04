using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChinaDressScript : MonoBehaviour
{
	public PromptScript Prompt;

	void Update ()
	{
		if (Prompt.Circle[0].fillAmount == 0)
		{
			Prompt.Yandere.WearChinaDress();

			Prompt.Hide();
			Prompt.enabled = false;
			enabled = false;
		}
	}
}