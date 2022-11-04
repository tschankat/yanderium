using UnityEngine;

public class RoseBushScript : MonoBehaviour
{
	public PromptScript Prompt;

	void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Prompt.Yandere.Inventory.Rose = true;

			this.enabled = false;
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
	}
}
