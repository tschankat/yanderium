using UnityEngine;

public class SinkScript : MonoBehaviour
{
	public YandereScript Yandere;
	public PromptScript Prompt;

	void Start()
	{
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();
	}

	void Update()
	{
		if (this.Yandere.PickUp != null)
		{
			if (this.Yandere.PickUp.Bucket != null)
			{
				if (this.Yandere.PickUp.Bucket.Dumbbells == 0)
				{
					this.Prompt.enabled = true;

					if (!this.Yandere.PickUp.Bucket.Full)
					{
						this.Prompt.Label[0].text = "     " + "Fill Bucket";
					}
					else
					{
						this.Prompt.Label[0].text = "     " + "Empty Bucket";
					}
				}
				else
				{
					if (this.Prompt.enabled)
					{
						this.Prompt.Hide();
						this.Prompt.enabled = false;
					}
				}
			}
			else if (this.Yandere.PickUp.BloodCleaner != null)
			{
				if (this.Yandere.PickUp.BloodCleaner.Blood > 0.0f)
				{
					this.Prompt.Label[0].text = "     " + "Empty Robot";
					this.Prompt.enabled = true;
				}
				else
				{
					this.Prompt.Hide();
					this.Prompt.enabled = false;
				}
			}
			else
			{
				if (this.Prompt.enabled)
				{
					this.Prompt.Hide();
					this.Prompt.enabled = false;
				}
			}
		}
		else
		{
			if (this.Prompt.enabled)
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
			}
		}

		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			if (this.Yandere.PickUp.Bucket != null)
			{
				if (!this.Yandere.PickUp.Bucket.Full)
				{
					this.Yandere.PickUp.Bucket.Fill();
				}
				else
				{
					this.Yandere.PickUp.Bucket.Empty();
				}

				if (!this.Yandere.PickUp.Bucket.Full)
				{
					this.Prompt.Label[0].text = "     " + "Fill Bucket";
				}
				else
				{
					this.Prompt.Label[0].text = "     " + "Empty Bucket";
				}
			}
			else if (this.Yandere.PickUp.BloodCleaner != null)
			{
				this.Yandere.PickUp.BloodCleaner.Blood = 0.0f;
				this.Yandere.PickUp.BloodCleaner.Lens.SetActive(false);
			}

			this.Prompt.Circle[0].fillAmount = 1.0f;
		}
	}
}
