using UnityEngine;

public class CheeseScript : MonoBehaviour
{
	public GameObject GlowingEye;

	public PromptScript Prompt;

	public UILabel Subtitle;

	public float Timer = 0.0f;

	void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Subtitle.text = "Knowing the mouse might one day leave its hole and get the cheese...It fills you with determination.";

			this.Prompt.Hide();
			this.Prompt.enabled = false;

			this.GlowingEye.SetActive(true);

			this.Timer = 5.0f;
		}

		if (this.Timer > 0.0f)
		{
			this.Timer -= Time.deltaTime;

			if (this.Timer <= 0.0f)
			{
				this.Prompt.enabled = true;
				this.Subtitle.text = string.Empty;
			}
		}
	}
}
