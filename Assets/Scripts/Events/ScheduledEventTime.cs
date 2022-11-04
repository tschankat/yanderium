using System;
using UnityEngine;

// [af] These definitions are for each type of event scheduling range. Some events 
// occupy a very specific range of time, while others simply occur on a given day or 
// week. These don't actually define the events themselves; they are just time slots.

public enum ScheduledEventTimeType
{
	Specific,
	TimeOfDay,
	Day,
	Week
}

public interface IScheduledEventTime
{
	ScheduledEventTimeType ScheduleType { get; }
	bool OccurringNow(DateAndTime currentTime);
	bool OccursInTheFuture(DateAndTime currentTime);
	bool OccurredInThePast(DateAndTime currentTime);
}

// [af] A specific event time defines a start and end time on a given weekday and week.
// This is suitable for precise event scheduling (the atomic unit of time is a second).
[Serializable]
public class SpecificEventTime : IScheduledEventTime
{
	[SerializeField] int week;
	[SerializeField] DayOfWeek weekday;
	[SerializeField] Clock startClock;
	[SerializeField] Clock endClock;

	public SpecificEventTime(int week, DayOfWeek weekday, Clock startClock, Clock endClock)
	{
		// The start time must be before the end time.
		Debug.Assert(startClock.IsBefore(endClock));

		this.week = week;
		this.weekday = weekday;
		this.startClock = startClock;
		this.endClock = endClock;
	}

	public ScheduledEventTimeType ScheduleType
	{
		get { return ScheduledEventTimeType.Specific; }
	}

	public bool OccurringNow(DateAndTime currentTime)
	{
		bool equalWeeks = currentTime.Week == this.week;
		bool equalWeekdays = currentTime.Weekday == this.weekday;

		Clock currentClock = currentTime.Clock;
		bool withinTimeRange = (currentClock.TotalSeconds >= this.startClock.TotalSeconds) &&
			(currentClock.TotalSeconds < this.endClock.TotalSeconds);

		return equalWeeks && equalWeekdays && withinTimeRange;
	}

	public bool OccursInTheFuture(DateAndTime currentTime)
	{
		if (currentTime.Week == this.week)
		{
			if (currentTime.Weekday == this.weekday)
			{
				return currentTime.Clock.TotalSeconds < this.startClock.TotalSeconds;
			}
			else
			{
				return currentTime.Weekday < this.weekday;
			}
		}
		else
		{
			return currentTime.Week < this.week;
		}
	}

	public bool OccurredInThePast(DateAndTime currentTime)
	{
		if (currentTime.Week == this.week)
		{
			if (currentTime.Weekday == this.weekday)
			{
				return currentTime.Clock.TotalSeconds >= this.endClock.TotalSeconds;
			}
			else
			{
				return currentTime.Weekday > this.weekday;
			}
		}
		else
		{
			return currentTime.Week > this.week;
		}
	}
}

// [af] A time of day event time defines some inexact range of time, like "the afternoon".
[Serializable]
public class TimeOfDayEventTime : IScheduledEventTime
{
	[SerializeField] int week;
	[SerializeField] DayOfWeek weekday;
	[SerializeField] TimeOfDay timeOfDay;

	public TimeOfDayEventTime(int week, DayOfWeek weekday, TimeOfDay timeOfDay)
	{
		this.week = week;
		this.weekday = weekday;
		this.timeOfDay = timeOfDay;
	}

	public ScheduledEventTimeType ScheduleType
	{
		get { return ScheduledEventTimeType.TimeOfDay; }
	}

	public bool OccurringNow(DateAndTime currentTime)
	{
		bool equalWeeks = currentTime.Week == this.week;
		bool equalWeekdays = currentTime.Weekday == this.weekday;
		bool equalTimesOfDay = currentTime.Clock.TimeOfDay == this.timeOfDay;

		return equalWeeks && equalWeekdays && equalTimesOfDay;
	}

	public bool OccursInTheFuture(DateAndTime currentTime)
	{
		if (currentTime.Week == this.week)
		{
			if (currentTime.Weekday == this.weekday)
			{
				return currentTime.Clock.TimeOfDay < this.timeOfDay;
			}
			else
			{
				return currentTime.Weekday < this.weekday;
			}
		}
		else
		{
			return currentTime.Week < this.week;
		}
	}

	public bool OccurredInThePast(DateAndTime currentTime)
	{
		if (currentTime.Week == this.week)
		{
			if (currentTime.Weekday == this.weekday)
			{
				return currentTime.Clock.TimeOfDay > this.timeOfDay;
			}
			else
			{
				return currentTime.Weekday > this.weekday;
			}
		}
		else
		{
			return currentTime.Week > this.week;
		}
	}
}

// [af] A day event time is scheduled to occur all day for some given day.
[Serializable]
public class DayEventTime : IScheduledEventTime
{
	[SerializeField] int week;
	[SerializeField] DayOfWeek weekday;

	public DayEventTime(int week, DayOfWeek weekday)
	{
		this.week = week;
		this.weekday = weekday;
	}

	public ScheduledEventTimeType ScheduleType
	{
		get { return ScheduledEventTimeType.Day; }
	}

	public bool OccurringNow(DateAndTime currentTime)
	{
		return (currentTime.Week == this.week) && (currentTime.Weekday == this.weekday);
	}

	public bool OccursInTheFuture(DateAndTime currentTime)
	{
		if (currentTime.Week == this.week)
		{
			return currentTime.Weekday < this.weekday;
		}
		else
		{
			return currentTime.Week < this.week;
		}
	}

	public bool OccurredInThePast(DateAndTime currentTime)
	{
		if (currentTime.Week == this.week)
		{
			return currentTime.Weekday > this.weekday;
		}
		else
		{
			return currentTime.Week > this.week;
		}
	}
}

// [af] A week event time takes up an entire week, from Sunday through Saturday.
[Serializable]
public class WeekEventTime : IScheduledEventTime
{
	[SerializeField] int week;

	public WeekEventTime(int week)
	{
		this.week = week;
	}

	public ScheduledEventTimeType ScheduleType
	{
		get { return ScheduledEventTimeType.Week; }
	}

	public bool OccurringNow(DateAndTime currentTime)
	{
		return currentTime.Week == this.week;
	}

	public bool OccursInTheFuture(DateAndTime currentTime)
	{
		return currentTime.Week < this.week;
	}

	public bool OccurredInThePast(DateAndTime currentTime)
	{
		return currentTime.Week > this.week;
	}
}
