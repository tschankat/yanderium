using UnityEngine;

public class RivalBagScript : MonoBehaviour
{
	public SchemesScript Schemes;
	public ClockScript Clock;

	public PromptScript Prompt;

	void Start()
	{
		//Anti-Osana Code
		#if !UNITY_EDITOR
		Prompt.enabled = false;
		Prompt.Hide();
		enabled = false;
		#endif
	}

	void Update()
	{
		if (this.Clock.Period == 2 || this.Clock.Period == 4)
		{
			this.Prompt.HideButton[0] = true;
		}
		else
		{
			if (Prompt.Yandere.Inventory.Cigs)
			{
				this.Prompt.HideButton[0] = false;
			}
			else
			{
				this.Prompt.HideButton[0] = true;
			}
		}

		//if (SchemeGlobals.GetSchemeStage(3) == 3)
		if (Prompt.Yandere.Inventory.Cigs)
		{
			this.Prompt.enabled = true;

			if (this.Prompt.Circle[0].fillAmount == 0.0f)
			{
				SchemeGlobals.SetSchemeStage(3, 4);
				this.Schemes.UpdateInstructions();

				this.Prompt.Yandere.Inventory.Cigs = false;
				this.Prompt.enabled = false;
				this.Prompt.Hide();

				this.enabled = false;
			}
		}

		if (this.Clock.Period == 2 || this.Clock.Period == 4)
		{
			this.Prompt.HideButton[1] = true;
		}
		else
		{
			if (Prompt.Yandere.Inventory.Ring)
			{
				this.Prompt.HideButton[1] = false;
			}
			else
			{
				this.Prompt.HideButton[1] = true;
			}
		}

		//if (SchemeGlobals.GetSchemeStage(2) == 2)
		if (this.Prompt.Yandere.Inventory.Ring)
		{
			this.Prompt.enabled = true;

			if (this.Prompt.Circle[1].fillAmount == 0.0f)
			{
				SchemeGlobals.SetSchemeStage(2, 3);
				this.Schemes.UpdateInstructions();

				this.Prompt.Yandere.Inventory.Ring = false;
				this.Prompt.enabled = false;
				this.Prompt.Hide();

				this.enabled = false;
			}
		}
	}
}
