using System;
using UnityEngine;

// [af] A wrapper for keeping an integer within some [min, max] inclusive range.
[Serializable]
public class RangeInt
{
	[SerializeField] int value;
	[SerializeField] int min;
	[SerializeField] int max;

	// [af] Constructs a RangeInt with a preset value.
	public RangeInt(int value, int min, int max)
	{
		// [af] Verify that the range is well-defined.
		Debug.Assert(min >= 0);
		Debug.Assert(max >= min);
		Debug.Assert(value >= min);
		Debug.Assert(value <= max);

		this.value = value;
		this.min = min;
		this.max = max;
	}

	// [af] Constructs a RangeInt with the value set to the minimum.
	public RangeInt(int min, int max) : this(min, min, max) {  }

	public int Value
	{
		get { return this.value; }
		set
		{
			// [af] Programmer error if a bad value is assigned.
			Debug.Assert(value >= this.min);
			Debug.Assert(value <= this.max);

			this.value = value;
		}
	}

	public int Min { get { return this.min; } }
	public int Max { get { return this.max; } }

	// [af] Next and Previous properties for range iteration. These make it more convenient 
	// when looping through the range, for example with interface selections.
	public int Next { get { return (this.value == this.max) ? this.min : (this.value + 1); } }
	public int Previous { get { return (this.value == this.min) ? this.max : (this.value - 1); } }
}
