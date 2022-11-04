using UnityEngine;

public class PickpocketMinigameScript : MonoBehaviour
{
	public Transform PickpocketSpot;

	public UISprite[] ButtonPrompts;
	public UISprite Circle;
	public UISprite BG;

	public YandereScript Yandere;

	public string CurrentButton = string.Empty;

	public bool NotNurse = false;
	public bool Sabotage = false;
	public bool Failure = false;
	public bool Success = false;
	public bool Show = false;

	public int StartingAlerts = 0;
	public int ButtonID = 0;
	public int Progress = 0;
	public int ID = 0;

	public float Timer = 0.0f;

	void Start()
	{
		this.transform.localScale = Vector3.zero;

		this.ButtonPrompts[1].enabled = false;
		this.ButtonPrompts[2].enabled = false;
		this.ButtonPrompts[3].enabled = false;
		this.ButtonPrompts[4].enabled = false;

        this.ButtonPrompts[1].alpha = 0;
        this.ButtonPrompts[2].alpha = 0;
        this.ButtonPrompts[3].alpha = 0;
        this.ButtonPrompts[4].alpha = 0;

        this.Circle.enabled = false;
		this.BG.enabled = false;
	}

	void Update()
	{
		if (this.Show)
		{
			if (this.PickpocketSpot != null)
			{
				this.Yandere.MoveTowardsTarget(this.PickpocketSpot.position);
				this.Yandere.transform.rotation = Quaternion.Slerp(
					this.Yandere.transform.rotation, this.PickpocketSpot.rotation, Time.deltaTime * 10.0f);
			}

			this.transform.localScale = Vector3.Lerp(
				this.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);

			this.Timer += Time.deltaTime;

			//Debug.Log("Starting Alerts is: " + this.StartingAlerts + ". Yandere's current Alerts are: " + this.Yandere.Alerts);

			if (this.Timer > 1.0f)
			{
				if (this.ButtonID == 0 && this.Yandere.Alerts == this.StartingAlerts)
				{
					this.ChooseButton();
					this.Timer = 0.0f;
				}
				else
				{
					this.Yandere.Caught = true;
					this.Failure = true;
					this.End();
				}
			}
			else
			{
				if (this.ButtonID > 0)
				{
					this.Circle.fillAmount = 1.0f - (this.Timer / 1.0f);

					// [af] Combined if statements for readability.
					if ((Input.GetButtonDown(InputNames.Xbox_A) && (this.CurrentButton != "A")) ||
						(Input.GetButtonDown(InputNames.Xbox_B) && (this.CurrentButton != "B")) ||
						(Input.GetButtonDown(InputNames.Xbox_X) && (this.CurrentButton != "X")) ||
						(Input.GetButtonDown(InputNames.Xbox_Y) && (this.CurrentButton != "Y")))
					{
						this.Yandere.Caught = true;
						this.Failure = true;
						this.End();
					}
					else if (Input.GetButtonDown(this.CurrentButton))
					{
						this.ButtonPrompts[this.ButtonID].enabled = false;
                        this.ButtonPrompts[this.ButtonID].alpha = 0;
                        this.Circle.enabled = false;
						this.BG.enabled = false;
						this.ButtonID = 0;
						this.Timer = 0.0f;

						this.Progress++;

						if (this.Progress == 5)
						{
							if (this.Sabotage)
							{
								this.Yandere.NotificationManager.CustomText = "Sabotage Success";
								this.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
							}
							else
							{
								this.Yandere.NotificationManager.CustomText = "Pickpocket Success";
								this.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
							}

							this.Yandere.Pickpocketing = false;
							this.Yandere.CanMove = true;
							this.Success = true;
							this.End();
						}
					}
				}
			}
		}
		else
		{
			if (this.transform.localScale.x > 0.10f)
			{
				this.transform.localScale = Vector3.Lerp(
					this.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);

				if (this.transform.localScale.x < 0.10f)
				{
					this.transform.localScale = Vector3.zero;
				}
			}
		}
	}

	void ChooseButton()
	{
		this.ButtonPrompts[1].enabled = false;
		this.ButtonPrompts[2].enabled = false;
		this.ButtonPrompts[3].enabled = false;
		this.ButtonPrompts[4].enabled = false;

        this.ButtonPrompts[1].alpha = 0;
        this.ButtonPrompts[2].alpha = 0;
        this.ButtonPrompts[3].alpha = 0;
        this.ButtonPrompts[4].alpha = 0;

        int PreviousButton = this.ButtonID;

		while (this.ButtonID == PreviousButton)
		{
			this.ButtonID = Random.Range(1, 5);
		}

		if (this.ButtonID == 1)
		{
			this.CurrentButton = "A";
		}
		else if (this.ButtonID == 2)
		{
			this.CurrentButton = "B";
		}
		else if (this.ButtonID == 3)
		{
			this.CurrentButton = "X";
		}
		else if (this.ButtonID == 4)
		{
			this.CurrentButton = "Y";
		}

		this.ButtonPrompts[this.ButtonID].enabled = true;
        this.ButtonPrompts[this.ButtonID].alpha = 1;
        this.Circle.enabled = true;
		this.BG.enabled = true;
	}

	public void End()
	{
		Debug.Log("Ending minigame.");

		this.ButtonPrompts[1].enabled = false;
		this.ButtonPrompts[2].enabled = false;
		this.ButtonPrompts[3].enabled = false;
		this.ButtonPrompts[4].enabled = false;

        this.ButtonPrompts[1].alpha = 0;
        this.ButtonPrompts[2].alpha = 0;
        this.ButtonPrompts[3].alpha = 0;
        this.ButtonPrompts[4].alpha = 0;

        this.Circle.enabled = false;
		this.BG.enabled = false;

		//if (!this.NotNurse)
		//{
			this.Yandere.CharacterAnimation.CrossFade("f02_readyToFight_00");
		//}

		this.Progress = 0;
		this.ButtonID = 0;
		this.Show = false;
		this.Timer = 0.0f;
	}
}