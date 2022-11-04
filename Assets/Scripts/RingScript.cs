using UnityEngine;

public class RingScript : MonoBehaviour
{
	public PromptScript Prompt;

	void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			SchemeGlobals.SetSchemeStage(2, 2);
			this.Prompt.Yandere.Inventory.Schemes.UpdateInstructions();

			this.Prompt.Yandere.Inventory.Ring = true;
			this.Prompt.Yandere.TheftTimer = .1f;

			this.gameObject.SetActive(false);
		}
	}
}