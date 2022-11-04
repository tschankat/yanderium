using UnityEngine;

public class CabinetDoorScript : MonoBehaviour
{
	public PromptScript Prompt;

	public bool Locked = false;

	public bool Open = false;

	public float Timer;

	void Update()
	{
		/*
		if (this.Locked)
		{
			if (this.Prompt.Circle[0].fillAmount < 1.0f)
			{
				this.Prompt.Label[0].text = "     " + "Locked";
				this.Prompt.Circle[0].fillAmount = 1.0f;
			}

			if (this.Prompt.Yandere.Inventory.LockPick)
			{
				this.Prompt.HideButton[2] = false;

				if (this.Prompt.Circle[2].fillAmount == 0.0f)
				{
					this.Prompt.Yandere.Inventory.LockPick = false;
					this.Prompt.Label[0].text = "     " + "Open";
					this.Prompt.HideButton[2] = true;
					this.Locked = false;
				}
			}
			else
			{
				if (!this.Prompt.HideButton[2])
				{
					this.Prompt.HideButton[2] = true;
				}
			}
		}
		else
		{
			if (this.Prompt.Circle[0].fillAmount == 0.0f)
			{
				this.Prompt.Yandere.TheftTimer = .1f;

				this.Prompt.Circle[0].fillAmount = 1.0f;

				this.Open = !this.Open;
				this.UpdateLabel();
				this.Timer = 0;
			}
		}
		*/

		if (this.Timer < 2.0f)
		{
			this.Timer += Time.deltaTime;

			if (this.Open)
			{
				this.transform.localPosition = new Vector3(
					Mathf.Lerp(this.transform.localPosition.x, 0.41775f, Time.deltaTime * 10.0f),
					this.transform.localPosition.y,
					this.transform.localPosition.z);
			}
			else
			{
				this.transform.localPosition = new Vector3(
					Mathf.Lerp(this.transform.localPosition.x, 0.0f, Time.deltaTime * 10.0f),
					this.transform.localPosition.y,
					this.transform.localPosition.z);
			}
		}
	}

	/*
	void UpdateLabel()
	{
		if (this.Open)
		{
			this.Prompt.Label[0].text = "     " + "Close";
		}
		else
		{
			this.Prompt.Label[0].text = "     " + "Open";
		}
	}
	*/
}