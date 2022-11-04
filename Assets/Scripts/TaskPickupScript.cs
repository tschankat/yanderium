using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskPickupScript : MonoBehaviour
{
	public PromptScript Prompt;

	public int ButtonID = 3;

	void Update ()
	{
		if (Prompt.Circle[ButtonID].fillAmount == 0.0f)
		{
			Prompt.Yandere.StudentManager.TaskManager.CheckTaskPickups();
		}
	}
}