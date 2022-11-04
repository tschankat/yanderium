using System;
using UnityEngine;

// [af] Data exclusive to rivals should go in this class. In the future, a student should 
// be considered a rival if their rival data is not null. Eventually there should be a 
// property like Student.IsRival that returns whether their rival data is null.

// [af] Each student should have a "RivalData" key in Students.json, but the value should 
// be null if they have no rival data. This way we can have consistency with JSON parsing.
// It is an optional field, so to speak.

[Serializable]
public class RivalData
{
	// The week the rival is active on.
	[SerializeField] int week;

	// @todo: Other data... must be exclusive from student data (i.e., only
	// data that rivals have in common).

	public RivalData(int week)
	{
		this.week = week;
	}

	public int Week
	{
		get { return this.week; }
	}
}
