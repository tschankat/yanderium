using System;
using UnityEngine;

public enum PhaseOfDay
{
	None = 0,
	BeforeClass = 1,
	FirstPeriod = 2,
	Lunchtime = 3,
	SecondPeriod = 4,
	CleaningTime = 5,
	AfterClass = 6,
}

[Serializable]
public class Phase
{
	[SerializeField] PhaseOfDay type;

	public Phase(PhaseOfDay type)
	{
		this.type = type;
	}

	public PhaseOfDay Type
	{
		get { return this.type; }
	}

	/*
	public static readonly PersonaTypeAndStringDictionary PhaseNames =
		new PersonaTypeAndStringDictionary
		{
			{ PhaseOfDay.None, "None" },
			{ PhaseOfDay.BeforeClass, "Before Class" },
			{ PhaseOfDay.FirstPeriod, "Classtime" },
			{ PhaseOfDay.Lunchtime, "Lunchtime" },
			{ PhaseOfDay.SecondPeriod, "Classtime" },
			{ PhaseOfDay.CleaningTime, "Cleaning Time" },
			{ PhaseOfDay.AfterClass, "After School" },
		};
	*/
}