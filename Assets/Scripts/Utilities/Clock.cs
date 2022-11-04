using System.Collections.Generic;
using UnityEngine;

public enum TimeOfDay
{
	Midnight, // 12am - 3am.
	EarlyMorning, // 3am - 6am.
	Morning, // 6am - 9am.
	LateMorning, // 9am - 12pm.
	Noon, // 12pm - 3pm.
	Afternoon, // 3pm - 6pm.
	Evening, // 6pm - 9pm.
	Night // 9pm - 12am.
}

// A simple 24-hour clock. Allows incrementing of the hour, minute, and second,
// and also allows real-time updating via delta time. It can also compare with
// another clock to see if someone is late for an event, etc..
[System.Serializable]
public class Clock
{
	// Current hours (0->23).
	[SerializeField] int hours;

	// Current minutes (0->59).
	[SerializeField] int minutes;

	// Current seconds (0->59).
	[SerializeField] int seconds;

	// The current fraction of a second. Incremented by delta time and updates 
	// 'seconds' when it reaches 1.
	[SerializeField] float currentSecond;

	static readonly Dictionary<TimeOfDay, string> TimeOfDayStrings =
		new Dictionary<TimeOfDay, string>
		{
			{ TimeOfDay.Midnight, "Midnight" },
			{ TimeOfDay.EarlyMorning, "Early Morning" },
			{ TimeOfDay.Morning, "Morning" },
			{ TimeOfDay.LateMorning, "Late Morning" },
			{ TimeOfDay.Noon, "Noon" },
			{ TimeOfDay.Afternoon, "Afternoon" },
			{ TimeOfDay.Evening, "Evening" },
			{ TimeOfDay.Night, "Night" }
		};

	// Clock constructor for an exact instant in time (hour, minute, second, and 
	// fraction of second).
	public Clock(int hours, int minutes, int seconds, float currentSecond)
	{
		// Verify that each value is in the correct range.
		Debug.Assert(hours >= 0);
		Debug.Assert(hours < 24);
		Debug.Assert(minutes >= 0);
		Debug.Assert(minutes < 60);
		Debug.Assert(seconds >= 0);
		Debug.Assert(seconds < 60);
		Debug.Assert(currentSecond >= 0.0f);
		Debug.Assert(currentSecond < 1.0f);

		this.hours = hours;
		this.minutes = minutes;
		this.seconds = seconds;
		this.currentSecond = currentSecond;
	}

	// Clock constructor for some specific hour, minute, and second.
	public Clock(int hours, int minutes, int seconds)
		: this(hours, minutes, seconds, 0.0f) { }

	// The default clock starts at midnight.
	public Clock()
		: this(0, 0, 0, 0.0f) { }

	// Returns current hour in 24-hour format.
	public int Hours24
	{
		get { return this.hours; }
	}

	// Returns current hour in 12-hour format (more useful for displaying).
	public int Hours12
	{
		get
		{
			int hoursMod = this.hours % 12;
			return (hoursMod == 0) ? 12 : hoursMod;
		}
	}

	// Returns current minutes.
	public int Minutes
	{
		get { return this.minutes; }
	}

	// Returns current seconds.
	public int Seconds
	{
		get { return this.seconds; }
	}

	// Returns current fraction of a second.
	public float CurrentSecond
	{
		get { return this.currentSecond; }
	}

	// Returns the clock's hours, minutes, and seconds combined. This might be useful
	// when doing comparisons between clocks.
	public int TotalSeconds
	{
		get { return (this.hours * 3600) + (this.minutes * 60) + this.seconds; }
	}

	// More precise calculation of the clock's exact time (includes the current fraction
	// of a second).
	public float PreciseTotalSeconds
	{
		get { return this.TotalSeconds + this.currentSecond; }
	}

	// Returns whether it's currently AM or PM.
	public bool IsAM
	{
		get { return this.hours < 12; }
	}

	// Gets the time of day associated with the current hour. This might be useful
	// for things that can only happen during, say, "the afternoon".
	public TimeOfDay TimeOfDay
	{
		get
		{
			if (this.hours < 3)
			{
				return TimeOfDay.Midnight;
			}
			else if (this.hours < 6)
			{
				return TimeOfDay.EarlyMorning;
			}
			else if (this.hours < 9)
			{
				return TimeOfDay.Morning;
			}
			else if (this.hours < 12)
			{
				return TimeOfDay.LateMorning;
			}
			else if (this.hours < 15)
			{
				return TimeOfDay.Noon;
			}
			else if (this.hours < 18)
			{
				return TimeOfDay.Afternoon;
			}
			else if (this.hours < 21)
			{
				return TimeOfDay.Evening;
			}
			else
			{
				return TimeOfDay.Night;
			}
		}
	}

	// Gets the time of day string intended for display.
	public string TimeOfDayString
	{
		get { return TimeOfDayStrings[this.TimeOfDay]; }
	}

	// Returns whether this clock is before another (useful for checking daily events).
	public bool IsBefore(Clock clock)
	{
		return this.TotalSeconds < clock.TotalSeconds;
	}

	// Returns whether this clock is after another (useful for checking if someone
	// is late for an event).
	public bool IsAfter(Clock clock)
	{
		return this.TotalSeconds > clock.TotalSeconds;
	}

	public void IncrementHour()
	{
		this.hours++;

		if (this.hours == 24)
		{
			this.hours = 0;
		}
	}

	public void IncrementMinute()
	{
		this.minutes++;

		if (this.minutes == 60)
		{
			this.IncrementHour();
			this.minutes = 0;
		}
	}

	public void IncrementSecond()
	{
		this.seconds++;

		if (this.seconds == 60)
		{
			this.IncrementMinute();
			this.seconds = 0;
		}
	}

	// Ticks the clock by delta time. Use this method when updating the clock 
	// each frame in real-time.
	public void Tick(float dt)
	{
		this.currentSecond += dt;

		while (this.currentSecond >= 1.0f)
		{
			this.IncrementSecond();
			this.currentSecond -= 1.0f;
		}
	}
}
