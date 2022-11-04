using UnityEngine;

public class LockpickScript : MonoBehaviour
{
	public PromptScript Prompt;

	void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			InventoryScript inventory = this.Prompt.Yandere.Inventory;

			inventory.LockPick = true;

			Destroy(this.gameObject);
		}
	}
}