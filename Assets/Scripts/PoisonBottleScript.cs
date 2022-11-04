using UnityEngine;

public class PoisonBottleScript : MonoBehaviour
{
	public PromptScript Prompt;

	public bool Theft;

	public int ID = 0;

	void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			if (Theft)
			{
				this.Prompt.Yandere.TheftTimer = .1f;
			}

			if (this.ID == 1)
			{
				this.Prompt.Yandere.Inventory.EmeticPoison = true;
			}
			else if (this.ID == 2)
			{
				this.Prompt.Yandere.Inventory.LethalPoison = true;
                this.Prompt.Yandere.Inventory.LethalPoisons++;
            }
			else if (this.ID == 3)
			{
				this.Prompt.Yandere.Inventory.RatPoison = true;
			}
			else if (this.ID == 4)
			{
				this.Prompt.Yandere.Inventory.HeadachePoison = true;
			}
			else if (this.ID == 5)
			{
				this.Prompt.Yandere.Inventory.Tranquilizer = true;
			}
			else if (this.ID == 6)
			{
				this.Prompt.Yandere.Inventory.Sedative = true;
			}

			this.Prompt.Yandere.StudentManager.UpdateAllBentos();

			Destroy(this.gameObject);
		}
	}
}
