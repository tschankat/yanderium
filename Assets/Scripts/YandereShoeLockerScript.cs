using UnityEngine;

public class YandereShoeLockerScript : MonoBehaviour
{
	public YandereScript Yandere;
	public PromptScript Prompt;

	public int Label = 1;

	void Update()
	{
		if ((this.Yandere.Schoolwear == 1) && !this.Yandere.ClubAttire && !this.Yandere.Egg)
		{
			if (this.Label == 2)
			{
				this.Prompt.Label[0].text = "     " + "Change Shoes";
				this.Label = 1;
			}

			if (this.Prompt.Circle[0].fillAmount == 0.0f)
			{
				this.Prompt.Circle[0].fillAmount = 1.0f;

				// [af] Replaced if/else statement with boolean expression.
				this.Yandere.Casual = !this.Yandere.Casual;

				this.Yandere.ChangeSchoolwear();
				this.Yandere.CanMove = true;
			}
		}
		else
		{
			this.Prompt.Circle[0].fillAmount = 1.0f;

			if (this.Label == 1)
			{
				this.Prompt.Label[0].text = "     " + "Not Available";
				this.Label = 2;
			}
		}
	}
}
