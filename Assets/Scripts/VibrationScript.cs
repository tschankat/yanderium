using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using XInputDotNetPure;

public class VibrationScript : MonoBehaviour
{
	public float Strength1;
	public float Strength2;

	public float TimeLimit;
	public float Timer;

	void Start()
	{
		GamePad.SetVibration(0, Strength1, Strength2);
	}

	void Update()
	{
		Timer += Time.deltaTime;

		if (Timer > TimeLimit)
		{
			GamePad.SetVibration(0, 0, 0);
			enabled = false;
		}
	}
}