using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SabotageVendingMachineScript : MonoBehaviour
{
	public VendingMachineScript VendingMachine;
	public GameObject SabotageSparks;
	public YandereScript Yandere;
	public PromptScript Prompt;

	void Start()
	{
		//Anti-Osana Code
		#if !UNITY_EDITOR
		//enabled = false;
		#endif

		Prompt.enabled = false;
		Prompt.Hide();
	}

	void Update ()
	{
		if (Yandere.Armed)
		{
			if (Yandere.EquippedWeapon.WeaponID == 6)
			{
				Prompt.enabled = true;

				if (this.Prompt.Circle[0].fillAmount == 0.0f)
				{
					//If we're waiting for Yandere-chan to sabotage this machine...
					if (SchemeGlobals.GetSchemeStage(4) == 2)
					{
						SchemeGlobals.SetSchemeStage(4, 3);
						this.Yandere.PauseScreen.Schemes.UpdateInstructions();
					}

					if (this.Yandere.StudentManager.Students[11] != null)
					{
						if (DateGlobals.Weekday == System.DayOfWeek.Thursday)
						{
							this.Yandere.StudentManager.Students[11].Hungry = true;
							this.Yandere.StudentManager.Students[11].Fed = false;
						}
					}

					Instantiate(SabotageSparks, new Vector3(-2.5f, 5.3605f, -32.982f), Quaternion.identity);

					VendingMachine.Sabotaged = true;

					Prompt.enabled = false;
					Prompt.Hide();

					enabled = false;
				}
			}
		}
		else
		{
			if (Prompt.enabled)
			{
				Prompt.enabled = false;
				Prompt.Hide();
			}	
		}
	}
}