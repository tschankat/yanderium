using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SakeScript : MonoBehaviour
{
	public PromptScript Prompt;

	void Update ()
	{
		if (Prompt.Circle[0].fillAmount == 0.0f)
		{
			Prompt.Yandere.Inventory.Sake = true;
			UpdatePrompt();
		}
	}

	public void UpdatePrompt()
	{
		if (Prompt.Yandere.Inventory.Sake)
		{
			Prompt.enabled = false;
			Prompt.Hide();

			Destroy(gameObject);
		}
		else
		{
			Prompt.enabled = true;
			Prompt.Hide();
		}	
	}
}