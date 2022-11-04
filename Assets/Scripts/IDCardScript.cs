using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDCardScript : MonoBehaviour
{
	public PromptScript Prompt;

	public bool Fake = false;

	void Update()
	{
		if (Prompt.Circle[0].fillAmount == 0)
		{
			Prompt.Circle[0].fillAmount = 1;

			Prompt.Yandere.StolenObject = gameObject;

			if (!Fake)
			{
				Prompt.Yandere.Inventory.IDCard = true;
				Prompt.Yandere.TheftTimer = 1;
			}
			else
			{
				Prompt.Yandere.Inventory.FakeID = true;
			}

			Prompt.Hide();

			gameObject.SetActive(false);
		}
	}
}