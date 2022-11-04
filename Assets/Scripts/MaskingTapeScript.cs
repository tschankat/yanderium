using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskingTapeScript : MonoBehaviour
{
	public CarryableCardboardBoxScript Box;

	public PromptScript Prompt;

	void Update()
	{
		if (Prompt.Circle[0].fillAmount == 0)
		{
			Prompt.Yandere.Inventory.MaskingTape = true;

			Box.Prompt.enabled = true;
			Box.enabled = true;

			Destroy(gameObject);
		}
	}
}