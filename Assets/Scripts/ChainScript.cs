using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainScript : MonoBehaviour
{
	public PromptScript Prompt;
	public TarpScript Tarp;

	public AudioClip ChainRattle;

	public GameObject[] Chains;

	public int Unlocked;

	void Update ()
	{
		if (Prompt.Circle[0].fillAmount == 0)
		{
			Prompt.Circle[0].fillAmount = 1;

			if (Prompt.Yandere.Inventory.MysteriousKeys > 0)
			{
				AudioSource.PlayClipAtPoint(ChainRattle, transform.position);

				Unlocked++;
				Chains[Unlocked].SetActive(false);

				Prompt.Yandere.Inventory.MysteriousKeys--;

				if (Unlocked == 5)
				{
					Tarp.Prompt.enabled = true;
					Tarp.enabled = true;

					Prompt.Hide();
					Prompt.enabled = false;

					Destroy(gameObject);
				}
			}
		}
	}
}