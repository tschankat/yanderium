using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBucketScript : MonoBehaviour
{
	public PromptScript Prompt;

	void Update()
	{
		if (Prompt.Circle[0].fillAmount == 0.0f)
		{
			Prompt.Circle[0].fillAmount = 1;

			if (Prompt.Yandere.Bloodiness == 0)
			{
				Prompt.Yandere.Bloodiness += 100;
				Prompt.Yandere.RedPaint = true;
			}
		}
	}
}