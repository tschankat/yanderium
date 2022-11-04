using System;
using UnityEngine;

public class DateChaser : MonoBehaviour
{
	public int CurrentDate;
	public string CurrentTimeString;

	[Header("Epoch timestamps")]
	[SerializeField]
	private int startDate = 1581724799;
	[SerializeField]
	private int endDate = 1421366399;

	[Space(5)]
	[Header("Settings")]
	[SerializeField]
	private float generalDuration = 10;
	[SerializeField]
	private AnimationCurve curve;

	// When this boolean is set to false, the script resets the countdown
	public bool Animate;

	private float startTime;
	private string[] monthNames = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

	// Event variables
	private int lastFrameDay;

	// Backwards compatibility
	private static DateTime fromUnix(long unix)
	{
		return epoch.AddSeconds(unix);
	}
	private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

	void Start()
	{
		Application.targetFrameRate = 60;
		Time.timeScale = 1;
	}

	void Update()
    {
		if (Animate)
		{
			float time = Time.time - startTime;
			CurrentDate = (int)Mathf.Lerp(startDate, endDate, curve.Evaluate(time/generalDuration));

			DateTime counterTime = fromUnix(CurrentDate);//DateTimeOffset.FromUnixTimeSeconds(CurrentDate).DateTime;
			string daySuffix = (counterTime.Day == 22 || counterTime.Day == 2) ? "nd" : (counterTime.Day == 3 ? "rd" : (counterTime.Day == 1 ? "st" : "th"));

			CurrentTimeString = string.Format("{0} {1}{2}, {3}", monthNames[counterTime.Month-1], counterTime.Day, daySuffix, counterTime.Year);

			if (lastFrameDay != counterTime.Day) onDayTick(counterTime.Day);

			lastFrameDay = counterTime.Day;

			Timer += Time.deltaTime;
		}
		else
		{
			startTime = Time.time;
			CurrentDate = startDate;
		}
    }

	public UILabel Label;
	public float Timer;
	public int Stage;

	private void onDayTick(int day)
	{
		//Debug.Log ("Midpoint is: " + 1501545599);

		Label.text = CurrentTimeString;

		/*
		if (Stage == 1)
		{
			if (Timer > 5)
			{
				generalDuration = Mathf.MoveTowards(generalDuration, 10, 1);

				if (CurrentDate < 1501545599)
				{
					Stage = 2;
				}
			}
		}
		else
		{
			generalDuration = Mathf.MoveTowards(generalDuration, 100, Time.deltaTime * 10);
		}
		*/
	}
}