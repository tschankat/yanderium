using UnityEngine;

public class DirectionalMicScript : MonoBehaviour
{
	public PromptScript Prompt;

	void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			InventoryScript inventory = this.Prompt.Yandere.Inventory;

			inventory.DirectionalMic = true;

			Destroy(this.gameObject);
		}
	}
}
