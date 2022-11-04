using System;
using UnityEngine;

// [af] Simple timer class for checking when some amount of time has elapsed.
[Serializable]
public class Timer
{
	[SerializeField] float currentSeconds;
	[SerializeField] float targetSeconds;

	public Timer(float targetSeconds)
	{
		this.currentSeconds = 0.0f;
		this.targetSeconds = targetSeconds;
	}

	public float CurrentSeconds { get { return this.currentSeconds; } }
	public float TargetSeconds { get { return this.targetSeconds; } }
	public bool IsDone { get { return this.currentSeconds >= this.targetSeconds; } }
	public float Progress { get { return Mathf.Clamp01(this.currentSeconds / this.targetSeconds); } }

	public void Reset()
	{
		this.currentSeconds = 0.0f;
	}

	// Reduces the current time by the target time. Intended for partially resetting the 
	// timer -- i.e., if the current is 1.1 and the target is 1.0, it preserves the 0.1 so
	// the timer's delta time remains accurate for future ticks.
	public void SubtractTarget()
	{
		this.currentSeconds -= this.targetSeconds;
	}

	// Ticks the timer by delta time.
	public void Tick(float dt)
	{
		this.currentSeconds += dt;
	}
}
