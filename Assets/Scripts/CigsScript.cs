using UnityEngine;

public class CigsScript : MonoBehaviour
{
	public PromptScript Prompt;

	void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			SchemeGlobals.SetSchemeStage(3, 3);
			this.Prompt.Yandere.Inventory.Schemes.UpdateInstructions();

			this.Prompt.Yandere.Inventory.Cigs = true;
			Destroy(this.gameObject);

			Prompt.Yandere.StudentManager.TaskManager.CheckTaskPickups();
		}
	}
}