using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateReverseScript : MonoBehaviour
{
	public AudioSource MyAudio;

	public string[] MonthName;

	public string Prefix;

	public UILabel Label;

	public AudioClip Finish;

	public float TimeLimit;
	public float LifeTime;
	public float Timer;

	public int RollDirection;

	public int Month;
	public int Year;
	public int Day;

	public int SlowMonth;
	public int SlowYear;
	public int SlowDay;

	public int EndMonth;
	public int EndYear;
	public int EndDay;

	public bool Rollback;

	void Start()
	{
		Time.timeScale = 1;

		UpdateDate();
	}

	void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			Rollback = true;
		}

		if (Rollback)
		{
			LifeTime += Time.deltaTime;

			Timer += Time.deltaTime;

			if (Timer > TimeLimit)
			{
				//If it's almost time to stop...
				if (Year == SlowYear && Month == SlowMonth && Day < SlowDay ||
					Year == SlowYear && Month < SlowMonth)
				{
					TimeLimit = TimeLimit * 1.09f;

					if (Month == EndMonth && Day == EndDay + 1)
					{
						MyAudio.clip = Finish;

						Label.color = new Color (1, 0, 0, 1);
						enabled = false;
					}
				}
				//If it's not yet time to stop...
				else
				{
					if (TimeLimit > .01f)
					{
						TimeLimit = TimeLimit * .9f;
					}
					else
					{
						Day += (RollDirection * 19);
					}
				}

				Timer = 0;

				Day += RollDirection;

				UpdateDate();

				MyAudio.Play();

				if (MyAudio.clip != Finish)
				{
					//MyAudio.pitch -= .001f;
				}
				else
				{
					MyAudio.pitch = 1;
				}
			}
		}
	}

	void UpdateDate()
	{
		if (Day < 1)
		{
			Day = 31;

			Month--;

			if (Month < 1)
			{
				Month = 12;

				Year--;
			}
		}
		else if (Day > 31)
		{
			Day = 1;

			Month++;

			if (Month > 11)
			{
				Month = 1;

				Year++;
			}
		}

		if (Day == 1 || Day == 21 || Day == 31)
		{
			Prefix = "st";
		}
		else if (Day == 2 || Day == 22)
		{
			Prefix = "nd";
		}
		else if (Day == 3 || Day == 23)
		{
			Prefix = "rd";
		}
		else
		{
			Prefix = "th";
		}

		Label.text = MonthName[Month] + " " + Day + Prefix + ", " + Year;
	}
}