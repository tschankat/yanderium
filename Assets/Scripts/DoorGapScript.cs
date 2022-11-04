using UnityEngine;

public class DoorGapScript : MonoBehaviour
{
	public RummageSpotScript RummageSpot;
	public SchemesScript Schemes;
	public PromptScript Prompt;
	public Transform[] Papers;

	public bool[] PhoneHacked;

	public bool StolenPhoneDropoff;

	public float Timer = 0.0f;

	public int Phase = 1;

	void Start()
	{
		this.Papers[1].gameObject.SetActive(false);
	}

	void Update()
	{
		if (!this.StolenPhoneDropoff)
		{
			if (this.Prompt.Circle[0].fillAmount == 0.0f)
			{
				if (this.Phase == 1)
				{
					this.Prompt.Hide();
					this.Prompt.enabled = false;

					this.Prompt.Yandere.Inventory.AnswerSheet = false;

					this.Papers[1].gameObject.SetActive(true);

					SchemeGlobals.SetSchemeStage(5, 6);
					this.Schemes.UpdateInstructions();

					this.GetComponent<AudioSource>().Play();
				}
				else
				{
					this.Prompt.Hide();
					this.Prompt.enabled = false;

					this.Prompt.Yandere.Inventory.AnswerSheet = true;
					this.Prompt.Yandere.Inventory.DuplicateSheet = true;

					this.Papers[2].gameObject.SetActive(false);

					this.RummageSpot.Prompt.Label[0].text = "     " + "Return Answer Sheet";
					this.RummageSpot.Prompt.enabled = true;

					SchemeGlobals.SetSchemeStage(5, 7);
					this.Schemes.UpdateInstructions();
				}

				this.Phase++;
			}

			if (this.Phase == 2)
			{
				this.Timer += Time.deltaTime;

				if (this.Timer > 4.0f)
				{
					this.Prompt.Label[0].text = "     " + "Pick Up Sheets";
					this.Prompt.enabled = true;
					this.Phase = 2;
				}
				else if (this.Timer > 3.0f)
				{
					Transform paper2 = this.Papers[2];
					paper2.localPosition = new Vector3(
						paper2.localPosition.x,
						paper2.localPosition.y,
						Mathf.Lerp(paper2.localPosition.z, -0.166f, Time.deltaTime * 10.0f));
				}
				else if (this.Timer > 1.0f)
				{
					Transform paper1 = this.Papers[1];
					paper1.localPosition = new Vector3(
						paper1.localPosition.x,
						paper1.localPosition.y,
						Mathf.Lerp(paper1.localPosition.z, 0.166f, Time.deltaTime * 10.0f));
				}
			}
		}

		///////////////////////////////////////////////////
		///// PROVIDING INFO-CHAN WITH A STOLEN PHONE /////
		///////////////////////////////////////////////////

		else
		{
			//Player has activated the prompt
			if (this.Prompt.Circle[0].fillAmount == 0.0f)
			{
				this.Prompt.Circle[0].fillAmount = 1;

				if (this.Phase == 1)
				{
					if (StudentGlobals.GetStudentPhoneStolen(this.Prompt.Yandere.StudentManager.CommunalLocker.RivalPhone.StudentID))
					{
						this.Prompt.Yandere.NotificationManager.CustomText = "Info-chan doesn't need this phone";
						this.Prompt.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
					}
					else
					{
						this.Prompt.Hide();
						this.Prompt.enabled = false;

						this.Prompt.Yandere.Inventory.RivalPhone = false;
						this.Prompt.Yandere.RivalPhone = false;

						this.PhoneHacked[this.Prompt.Yandere.StudentManager.CommunalLocker.RivalPhone.StudentID] = true;
						this.Prompt.Yandere.Inventory.PantyShots += 10;

						this.Papers[1].gameObject.SetActive(true);

						this.GetComponent<AudioSource>().Play();

						this.Phase++;
					}
				}
				else if (this.Phase == 2)
				{
					this.Prompt.Yandere.Inventory.RivalPhone = true;

					this.Papers[1].gameObject.SetActive(false);

					this.Prompt.Hide();
					this.Prompt.enabled = false;

					this.Phase++;
				}
			}

			//Player has placed the phone down.
			if (this.Phase == 2)
			{
				this.Timer += Time.deltaTime;

				if (this.Timer > 4.0f)
				{
					this.Prompt.Label[0].text = "     " + "Pick Up Phone";
					this.Prompt.enabled = true;
				}
				else if (this.Timer > 3.0f)
				{
					this.Papers[1].localPosition = new Vector3(
						this.Papers[1].localPosition.x,
						this.Papers[1].localPosition.y,
						Mathf.Lerp(this.Papers[1].localPosition.z, -0.166f, Time.deltaTime * 10.0f));
				}
				else if (this.Timer > 1.0f)
				{
					this.Papers[1].localPosition = new Vector3(
						this.Papers[1].localPosition.x,
						this.Papers[1].localPosition.y,
						Mathf.Lerp(this.Papers[1].localPosition.z, 0.166f, Time.deltaTime * 10.0f));
				}
			}
		}
	}

	public void SetPhonesHacked()
	{
		int ID = 1;

		while (ID < 101)
		{
			if (this.PhoneHacked[ID])
			{
				StudentGlobals.SetStudentPhoneStolen(ID, true);
			}

			ID++;
		}
	}
}