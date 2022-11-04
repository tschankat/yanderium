using UnityEngine;

public class BentoScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public YandereScript Yandere;
	public Transform PoisonSpot;
	public PromptScript Prompt;
	public int Poison = 0;
	public int ID = 0;

	void Start()
	{
		if (this.Prompt.Yandere != null)
		{
			this.Yandere = this.Prompt.Yandere;
		}
	}

	void Update()
	{
		if (this.Yandere == null)
		{
			if (this.Prompt.Yandere != null)
			{
				this.Yandere = this.Prompt.Yandere;
			}
		}
		else
		{
			if (this.Yandere.Inventory.EmeticPoison || this.Yandere.Inventory.RatPoison || this.Yandere.Inventory.LethalPoison)
			{
				this.Prompt.enabled = true;

				if (!this.Yandere.Inventory.EmeticPoison && !this.Yandere.Inventory.RatPoison)
				{
					this.Prompt.HideButton[0] = true;
				}
				else
				{
					this.Prompt.HideButton[0] = false;
				}

				if (this.Prompt.Circle[0].fillAmount == 0.0f)
				{
					if (this.Yandere.Inventory.EmeticPoison)
					{
						this.Yandere.Inventory.EmeticPoison = false;
						this.Yandere.PoisonType = 1;
					}
					else
					{
						this.Yandere.Inventory.RatPoison = false;
						this.Yandere.PoisonType = 3;
					}

					this.Yandere.CharacterAnimation.CrossFade(AnimNames.FemalePoisoning);
					this.Yandere.PoisonSpot = this.PoisonSpot;
					this.Yandere.Poisoning = true;
					this.Yandere.CanMove = false;
					this.enabled = false;
					this.Poison = 1;

					if (this.ID != 1)
					{
						this.StudentManager.Students[this.ID].Emetic = true;
					}

					this.Prompt.Hide();
					this.Prompt.enabled = false;
				}

				//Osana or Obstacle
				if (this.ID == 11 || this.ID == 6)
				{
					// [af] Replaced if/else statement with boolean expression.
					this.Prompt.HideButton[1] = !this.Prompt.Yandere.Inventory.LethalPoison;

					if (this.Prompt.Circle[1].fillAmount == 0.0f)
					{
						this.Prompt.Yandere.CharacterAnimation.CrossFade(AnimNames.FemalePoisoning);
						this.Prompt.Yandere.Inventory.LethalPoison = false;
						this.StudentManager.Students[this.ID].Lethal = true;
						this.Prompt.Yandere.PoisonSpot = this.PoisonSpot;
						this.Prompt.Yandere.Poisoning = true;
						this.Prompt.Yandere.CanMove = false;
						this.Prompt.Yandere.PoisonType = 2;
						this.enabled = false;
						this.Poison = 2;

						this.Prompt.Hide();
						this.Prompt.enabled = false;
					}
				}
			}
			else
			{
				this.Prompt.enabled = false;
			}
		}
	}
}