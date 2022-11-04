using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOutBookScript : MonoBehaviour
{
	public PromptScript Prompt;

	void Update ()
	{
		if (Prompt.Circle[0].fillAmount == 0.0f)
		{
			Prompt.Yandere.Inventory.Book = true;
			UpdatePrompt();
		}
	}

	public void UpdatePrompt()
	{
		if (Prompt.Yandere.Inventory.Book)
		{
			Prompt.enabled = false;
			Prompt.Hide();
		}
		else
		{
			Prompt.enabled = true;
			Prompt.Hide();
		}	
	}
}