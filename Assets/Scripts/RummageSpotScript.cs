using System;
using UnityEngine;

public class RummageSpotScript : MonoBehaviour
{
	public GameObject AlarmDisc;

	public DoorGapScript DoorGap;
	public SchemesScript Schemes;
	public YandereScript Yandere;
	public PromptScript Prompt;
	public ClockScript Clock;

	public Transform Target;

	public int Phase = 0;
	public int ID = 0;

	void Start()
	{
		if (this.ID == 1)
		{
			if (GameGlobals.AnswerSheetUnavailable)
			{
				Debug.Log("The answer sheet is no longer available, due to events on a previous day.");

				this.Prompt.Hide();
				this.Prompt.enabled = false;

				// [af] Added "gameObject" for C# compatibility.
				this.gameObject.SetActive(false);
			}
			else
			{
				if ((DateGlobals.Weekday == DayOfWeek.Friday) && (this.Clock.HourTime > 13.50f))
				{
					this.Prompt.Hide();
					this.Prompt.enabled = false;

					// [af] Added "gameObject" for C# compatibility.
					this.gameObject.SetActive(false);
				}
			}
		}
	}

	void Update()
	{
		AudioSource audioSource = this.GetComponent<AudioSource>();

		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Prompt.Circle[0].fillAmount = 1;

			if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
				this.Yandere.EmptyHands();

				this.Yandere.CharacterAnimation.CrossFade(AnimNames.FemaleRummage);
				this.Yandere.ProgressBar.transform.parent.gameObject.SetActive(true);
				this.Yandere.RummageSpot = this;
				this.Yandere.Rummaging = true;
				this.Yandere.CanMove = false;
				audioSource.Play();
			}
		}

		if (this.Yandere.Rummaging)
		{
			GameObject Alarm = Instantiate(this.AlarmDisc, this.transform.position, Quaternion.identity);
			Alarm.GetComponent<AlarmDiscScript>().NoScream = true;
			Alarm.transform.localScale = new Vector3(
				750.0f,
				Alarm.transform.localScale.y,
				750.0f);
		}

		if (this.Yandere.Noticed)
		{
			audioSource.Stop();
		}
	}

	public void GetReward()
	{
		if (this.ID == 1)
		{
			if (this.Phase == 1)
			{
				//Yandere-chan has obtained the answer sheet.
				SchemeGlobals.SetSchemeStage(5, 5);
				this.Schemes.UpdateInstructions();

				this.Yandere.Inventory.AnswerSheet = true;

				this.Prompt.Hide();
				this.Prompt.enabled = false;

				this.DoorGap.Prompt.enabled = true;

				this.Phase++;
			}
			else if (this.Phase == 2)
			{
				//Yandere-chan just just returned the answer sheet.
				SchemeGlobals.SetSchemeStage(5, 8);
				this.Schemes.UpdateInstructions();

				this.Prompt.Yandere.Inventory.AnswerSheet = false;

				this.Prompt.Hide();
				this.Prompt.enabled = false;

				// [af] Added "gameObject" for C# compatibility.
				this.gameObject.SetActive(false);

				this.Phase++;
			}
		}
	}
}
