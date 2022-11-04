using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SodaScript : MonoBehaviour
{
	public PromptScript Prompt;

	void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Prompt.Yandere.Inventory.Soda = true;
			this.Prompt.Yandere.StudentManager.TaskManager.UpdateTaskStatus();

			Destroy(this.gameObject);
		}
	}
}