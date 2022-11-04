using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedrunTimerScript : MonoBehaviour
{
	public PoliceScript Police;

	public UILabel Label;

	public float Timer;

	void Start()
	{
		Label.enabled = false;
	}

	void Update()
	{
		if (!Police.FadeOut)
		{
			Timer += Time.deltaTime;

			if (Label.enabled)
			{
				Label.text = "" + FormatTime(Timer);
			}

			if (Input.GetKeyDown(KeyCode.Delete))
			{
				Label.enabled = !Label.enabled;
			}
		}
	}

	string FormatTime (float time)
	{
		int intTime = (int)time;
		int minutes = intTime / 60;
		int seconds = intTime % 60;
		float fraction = time * 1000;
		fraction = (fraction % 1000);
		string timeText = String.Format ("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction);
		return timeText;
	}
}