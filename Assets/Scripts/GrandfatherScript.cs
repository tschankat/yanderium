using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandfatherScript : MonoBehaviour
{
	public ClockScript Clock;

	public Transform MinuteHand;
	public Transform HourHand;
	public Transform Pendulum;

	public float Rotation;
	public float Force;
	public float Speed;

	public bool Flip;

	void Update ()
	{
		if (!Flip){if (Force < .1){Force += Time.deltaTime * .1f * Speed;}}
		else{if (Force > -.1){Force -= Time.deltaTime * .1f * Speed;}}

		Rotation += Force;

		if (Rotation > 1){Flip = true;}
		else if (Rotation < -1){Flip = false;}
			
		if (Rotation > 5){Rotation = 5;}
		else if (Rotation < -5){Rotation = -5;}

		Pendulum.localEulerAngles = new Vector3(0, 0, Rotation);

		this.MinuteHand.localEulerAngles = new Vector3(
			this.MinuteHand.localEulerAngles.x,
			this.MinuteHand.localEulerAngles.y,
			this.Clock.Minute * 6.0f);

		this.HourHand.localEulerAngles = new Vector3(
			this.HourHand.localEulerAngles.x,
			this.HourHand.localEulerAngles.y,
			this.Clock.Hour * 30.0f);
	}
}