using System;
using UnityEngine;

public class RivalDeskScript : MonoBehaviour
{
	public SchemesScript Schemes;
	public ClockScript Clock;

	public PromptScript Prompt;

	public bool Cheating = false;

	void Start()
	{
		if (DateGlobals.Weekday != DayOfWeek.Friday)
		{
			this.enabled = false;
		}
	}

	void Update()
	{
		//if (SchemeGlobals.GetSchemeStage(5) == 5)
		if (this.Prompt.Yandere.Inventory.AnswerSheet == false && this.Prompt.Yandere.Inventory.DuplicateSheet == true)
		{
			//Debug.Log ("The relevant scheme stage is 5.");

			this.Prompt.enabled = true;

			if (this.Clock.HourTime > 13.0f)
			{
				this.Prompt.HideButton[0] = false;

				//The player has failed to put the answer sheet into the desk before lunchtime ended.
				if (this.Clock.HourTime > 13.50f)
				{
					SchemeGlobals.SetSchemeStage(5, 100);
					this.Schemes.UpdateInstructions();

					this.Prompt.HideButton[0] = true;
				}
			}

			//The player slipped the duplicated sheet into Osana's desk.
			if (this.Prompt.Circle[0].fillAmount == 0.0f)
			{
				SchemeGlobals.SetSchemeStage(5, 9);
				this.Schemes.UpdateInstructions();

				this.Prompt.Yandere.Inventory.DuplicateSheet = false;

				this.Prompt.Hide();
				this.Prompt.enabled = false;

				this.Cheating = true;
				this.enabled = false;
			}
		}
	}
}
