using UnityEngine;

public class StolenPhoneSpotScript : MonoBehaviour
{
	public RivalPhoneScript RivalPhone;

	public PromptScript Prompt;

	public Transform PhoneSpot;

	void Update()
	{
		if (this.Prompt.Yandere.Inventory.RivalPhone)
		{
			this.Prompt.enabled = true;

			if (this.Prompt.Circle[0].fillAmount == 0.0f)
			{
				//If this is Osana's phone...
				if (this.RivalPhone.StudentID == this.Prompt.Yandere.StudentManager.RivalID)
				{
					//If we're waiting for Yandere-chan to return Osana's phone to her desk...
					if (SchemeGlobals.GetSchemeStage(1) == 6)
					{
						SchemeGlobals.SetSchemeStage(1, 7);
						this.Prompt.Yandere.PauseScreen.Schemes.UpdateInstructions();
					}
				}

				this.Prompt.Yandere.SmartphoneRenderer.material.mainTexture =
					this.Prompt.Yandere.YanderePhoneTexture;
				
				this.Prompt.Yandere.Inventory.RivalPhone = false;
				this.Prompt.Yandere.RivalPhone = false;

				this.RivalPhone.transform.parent = null;

				if (this.PhoneSpot == null)
				{
					this.RivalPhone.transform.position = this.transform.position;
				}
				else
				{
					this.RivalPhone.transform.position = this.PhoneSpot.position;
				}

				this.RivalPhone.transform.eulerAngles = this.transform.eulerAngles;
				this.RivalPhone.gameObject.SetActive(true);

				this.gameObject.SetActive(false);
			}
		}
		else
		{
			if (this.Prompt.enabled)
			{
				this.Prompt.enabled = false;
				this.Prompt.Hide();
			}
		}
	}
}
