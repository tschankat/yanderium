using UnityEngine;

public class TranquilizerScript : MonoBehaviour
{
	public PromptScript Prompt;

	void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Prompt.Yandere.Inventory.Tranquilizer = true;
			this.Prompt.Yandere.StudentManager.UpdateAllBentos();

			this.Prompt.Yandere.TheftTimer = .1f;

			Destroy(this.gameObject);
		}
	}
}