using UnityEngine;

public class HeadsetScript : MonoBehaviour
{
	public PromptScript Prompt;

	void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			//PlayerGlobals.Headset = true;

			this.Prompt.Yandere.Inventory.Schemes.UpdateInstructions();
			this.Prompt.Yandere.Inventory.Headset = true;

			Destroy(this.gameObject);
		}
	}
}
