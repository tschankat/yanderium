using System;
using UnityEngine;

public class HomeClockScript : MonoBehaviour
{
	public UILabel MoneyLabel;
	public UILabel HourLabel;
	public UILabel DayLabel;

	public AudioSource MyAudio;

	public bool ShakeMoney;

	public float Shake;
	public float G;
	public float B;

	void Start()
	{
		this.DayLabel.text = this.GetWeekdayText(DateGlobals.Weekday);

		if (HomeGlobals.Night)
		{
			this.HourLabel.text = "8:00 PM";
		}
		else
		{
			// [af] Replaced if/else statement with ternary expression.
			this.HourLabel.text = HomeGlobals.LateForSchool ? "7:30 AM" : "6:30 AM";
		}

		this.UpdateMoneyLabel();
	}

	void Update()
	{
		if (this.ShakeMoney == true)
		{
			this.Shake = Mathf.MoveTowards(this.Shake, 0, Time.deltaTime * 10);

			this.MoneyLabel.transform.localPosition = new Vector3(
				1020 + UnityEngine.Random.Range(this.Shake * -1.0f, this.Shake * 1.0f),
				375 + UnityEngine.Random.Range(this.Shake * -1.0f, this.Shake * 1.0f),
				0);

			this.G = Mathf.MoveTowards(this.G, .75f, Time.deltaTime);
			this.B = Mathf.MoveTowards(this.B, 1, Time.deltaTime);

			this.MoneyLabel.color = new Color(1, G, B, 1);

			if (this.Shake == 0)
			{
				this.ShakeMoney = false;
			}
		}
	}

	string GetWeekdayText(DayOfWeek weekday)
	{
		if (weekday == DayOfWeek.Sunday)
		{
			return "SUNDAY";
		}
		else if (weekday == DayOfWeek.Monday)
		{
			return "MONDAY";
		}
		else if (weekday == DayOfWeek.Tuesday)
		{
			return "TUESDAY";
		}
		else if (weekday == DayOfWeek.Wednesday)
		{
			return "WEDNESDAY";
		}
		else if (weekday == DayOfWeek.Thursday)
		{
			return "THURSDAY";
		}
		else if (weekday == DayOfWeek.Friday)
		{
			return "FRIDAY";
		}
		else
		{
			return "SATURDAY";
		}
	}

	public void UpdateMoneyLabel()
	{
		this.MoneyLabel.text = "$" + PlayerGlobals.Money.ToString("F2", System.Globalization.NumberFormatInfo.InvariantInfo);
    }

	public void MoneyFail()
	{
		ShakeMoney = true;
		Shake = 10;
		G = 0;
		B = 0;

		MyAudio.Play();
	}
}