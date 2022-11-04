using System;
using UnityEngine;

// [af] This class defines a specific point in time on a given day and week.
// It should be very useful for scheduling events.
[Serializable]
public class DateAndTime
{
	[SerializeField] int week;
	[SerializeField] DayOfWeek weekday;
	[SerializeField] Clock clock;

	public DateAndTime(int week, DayOfWeek weekday, Clock clock)
	{
		this.week = week;
		this.weekday = weekday;
		this.clock = clock;
	}

	public int Week
	{
		get { return this.week; }
	}

	public DayOfWeek Weekday
	{
		get { return this.weekday; }
	}

	public Clock Clock
	{
		get { return this.clock; }
	}

	// Returns the week, weekday, and clock combined into seconds. This is useful for 
	// comparing two DateAndTime objects.
	public int TotalSeconds
	{
		get
		{
			int weekSeconds = this.week * 604800;
			int weekdaySeconds = ((int)this.weekday) * 86400;
			int clockSeconds = this.clock.TotalSeconds;
			return weekSeconds + weekdaySeconds + clockSeconds;
		}
	}

	public void IncrementWeek()
	{
		this.week++;
	}

	public void IncrementWeekday()
	{
		int weekdayInteger = (int)this.weekday;
		weekdayInteger++;

		if (weekdayInteger == 7)
		{
			this.IncrementWeek();
			weekdayInteger = 0;
		}

		this.weekday = (DayOfWeek)weekdayInteger;
	}

	// [af] Call this method the same way you would call Clock.Tick() if updating
	// in real-time. Do not call DateAndTime.Clock.Tick().
	public void Tick(float dt)
	{
		int oldHour = this.clock.Hours24;
		this.clock.Tick(dt);
		int newHour = this.clock.Hours24;
		
		// See if the hour looped back around, indicating that a new day has started.
		if (newHour < oldHour)
		{
			this.IncrementWeekday();
		}
	}
}
