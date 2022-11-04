using System;
using UnityEngine;

public enum WitnessMemoryType
{
	Blood,
	Corpse,
	Murder,
	Insanity,
	Weapon
}

[Serializable]
public class WitnessMemory
{
	// Each of a witness' times that they last saw something.
	[SerializeField] float[] memories;

	// The duration of a witness' memory, in seconds.
	[SerializeField] float memorySpan;

	const float LongMemorySpan = 8.0f * 60.0f * 60.0f; // Eight hours.
	const float MediumMemorySpan = 2.0f * 60.0f * 60.0f; // Two hours.
	const float ShortMemorySpan = 30.0f * 60.0f; // Half hour.
	const float VeryShortMemorySpan = 2.0f * 60.0f; // Two minutes.

	public WitnessMemory()
	{
		this.memories = new float[Enum.GetValues(typeof(WitnessMemoryType)).Length];

		// Start the witness' memory as a clean slate (everything happened forever ago).
		for (int i = 0; i < this.memories.Length; i++)
		{
			this.memories[i] = float.PositiveInfinity;
		}

		this.memorySpan = ShortMemorySpan;
	}

	// Returns whether the witness remembers something.
	public bool Remembers(WitnessMemoryType type)
	{
		return this.memories[(int)type] < this.memorySpan;
	}

	// Refreshes a witness' memory. Use this on the same frame that they see something.
	public void Refresh(WitnessMemoryType type)
	{
		this.memories[(int)type] = 0.0f;
	}

	// Updates each of the 'last witnessed' memory times.
	public void Tick(float dt)
	{
		for (int i = 0; i < this.memories.Length; i++)
		{
			this.memories[i] += dt;
		}
	}
}
