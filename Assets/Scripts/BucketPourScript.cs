using UnityEngine;

public class BucketPourScript : MonoBehaviour
{
	public SplashCameraScript SplashCamera;
	public YandereScript Yandere;
	public PromptScript Prompt;

	public string PourHeight = string.Empty;

	public float PourDistance = 0.0f;
	public float PourTime = 0.0f;

	public int ID = 0;

	void Start()
	{
		//this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();

		//this.Prompt.Hide();
		//this.Prompt.enabled = false;
		//this.enabled = false;
	}

	void Update()
	{
		/*
		if (this.Yandere == null)
		{
			Debug.Log("My name is: " + this.gameObject.name);
		}
		*/

		if (this.Yandere.PickUp != null)
		{
			if (this.Yandere.PickUp.Bucket != null)
			{
				if (this.Yandere.PickUp.Bucket.Full)
				{
					if (!this.Prompt.enabled)
					{
						this.Prompt.Label[0].text = "     " + "Pour";
						this.Prompt.enabled = true;
					}
				}
				else
				{
					if (this.Yandere.PickUp.Bucket.Dumbbells == 5)
					{
						if (!this.Prompt.enabled)
						{
							this.Prompt.Label[0].text = "     " + "Drop";
							this.Prompt.enabled = true;
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

		if (this.Prompt.Circle[0] != null)
		{
			if (this.Prompt.Circle[0].fillAmount == 0.0f)
			{
				this.Prompt.Circle[0].fillAmount = 1.0f;

				if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
				{
					if (this.Prompt.Label[0].text == "     " + "Pour")
					{
						this.Yandere.Stool = this.transform;
						this.Yandere.CanMove = false;
						this.Yandere.Pouring = true;

						this.Yandere.PourDistance = this.PourDistance;
						this.Yandere.PourHeight = this.PourHeight;
						this.Yandere.PourTime = this.PourTime;
					}
					else
					{
						this.Yandere.Character.GetComponent<Animation>().CrossFade(AnimNames.FemaleBucketDrop);

						this.Yandere.MyController.radius = 0.0f;
						this.Yandere.BucketDropping = true;
						this.Yandere.DropSpot = this.transform;
						this.Yandere.CanMove = false;
					}
				}
			}
		}

		if (this.Yandere.Pouring)
		{
			if (this.PourHeight == "Low")
			{
				if (Input.GetButtonDown(InputNames.Xbox_B) && this.Prompt.DistanceSqr < 1)
				{
					this.SplashCamera.Show = true;
					this.SplashCamera.MyCamera.enabled = true;

					if (this.ID == 1)
					{
						this.SplashCamera.transform.position = new Vector3(32.1f, 0.80f, 26.9f);
						this.SplashCamera.transform.eulerAngles = new Vector3(0.0f, -45.0f, 0.0f);
					}
					else
					{
						this.SplashCamera.transform.position = new Vector3(1.1f, 0.80f, 32.1f);
						this.SplashCamera.transform.eulerAngles = new Vector3(0.0f, -135.0f, 0.0f);
					}
				}
			}
		}
		else if (this.Yandere.BucketDropping)
		{
			if (Input.GetButtonDown(InputNames.Xbox_B) && this.Prompt.DistanceSqr < 1)
			{
				this.SplashCamera.Show = true;
				this.SplashCamera.MyCamera.enabled = true;

				if (this.ID == 1)
				{
					this.SplashCamera.transform.position = new Vector3(32.1f, 0.80f, 26.9f);
					this.SplashCamera.transform.eulerAngles = new Vector3(0.0f, -45.0f, 0.0f);
				}
				else
				{
					this.SplashCamera.transform.position = new Vector3(1.1f, 0.80f, 32.1f);
					this.SplashCamera.transform.eulerAngles = new Vector3(0.0f, -135.0f, 0.0f);
				}
			}
		}
	}
}
