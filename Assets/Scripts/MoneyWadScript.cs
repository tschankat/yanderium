using UnityEngine;

public class MoneyWadScript : MonoBehaviour
{
	public PromptScript Prompt;

	void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Prompt.Yandere.Inventory.Money += 100;
            this.Prompt.Yandere.Inventory.UpdateMoney();
			Destroy(this.gameObject);
		}
	}
}
