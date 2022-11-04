using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenpaiShrineCollectibleScript : MonoBehaviour
{
	public PromptScript Prompt;

	public int ID = 0;

	void Start()
	{
		if (PlayerGlobals.GetShrineCollectible(ID))
		{
			Destroy(gameObject);
		}
	}

	void Update()
	{
		if (Prompt.Circle[0].fillAmount == 0)
		{
			Prompt.Yandere.Inventory.ShrineCollectibles[ID] = true;
			Prompt.Hide();

			Destroy(gameObject);
		}
	}
}