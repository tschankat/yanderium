using UnityEngine;

public class VendingMachineScript : MonoBehaviour
{
	public PromptScript Prompt;

	public Transform CanSpawn;

	public GameObject[] Cans;

	public bool SnackMachine;
	public bool Sabotaged;

	public int Price;

	void Start()
	{
		if (SnackMachine)
		{
			Prompt.Text[0] = "Buy Snack for $" + Price + ".00";
		}
		else
		{
			Prompt.Text[0] = "Buy Drink for $" + Price + ".00";
		}

		Prompt.Label[0].text = "     " + Prompt.Text[0];
	}

	void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Prompt.Circle[0].fillAmount = 1.0f;

			if (Prompt.Yandere.Inventory.Money >= Price)
			{
				if (!Sabotaged)
				{
					GameObject NewCan = Instantiate(Cans[Random.Range(0, Cans.Length)], CanSpawn.position, CanSpawn.rotation);

					NewCan.GetComponent<AudioSource>().pitch = Random.Range(0.90f, 1.10f);
				}

				if (SnackMachine)
				{
					//If we're waiting for Yandere-chan to buy a salty snack...
					if (SchemeGlobals.GetSchemeStage(4) == 3)
					{
						SchemeGlobals.SetSchemeStage(4, 4);
						Prompt.Yandere.PauseScreen.Schemes.UpdateInstructions();
					}
				}

				Prompt.Yandere.Inventory.Money -= Price;
				Prompt.Yandere.Inventory.UpdateMoney();
			}
			else
			{
                this.Prompt.Yandere.StudentManager.TutorialWindow.ShowMoneyMessage = true;
				this.Prompt.Yandere.NotificationManager.CustomText = "Not enough money!";
				this.Prompt.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
			}
		}
	}
}