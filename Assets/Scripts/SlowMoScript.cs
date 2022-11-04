using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMoScript : MonoBehaviour
{
	public bool Spinning;
	public float Speed;

	void Update()
	{
		if (Input.GetKeyDown("s"))
		{
			Spinning = !Spinning;
		}

		if (Input.GetKeyDown("a"))
		{
			Time.timeScale = .1f;
		}

		if (Input.GetKeyDown("-"))
		{
			Time.timeScale--;
		}

		if (Input.GetKeyDown("="))
		{
			Time.timeScale++;
		}

		if (Input.GetKeyDown("z"))
		{
			Speed += Time.deltaTime;
		}

		if (Speed > 0)
		{
			transform.position += new Vector3(Time.deltaTime * .1f, 0, Time.deltaTime * .1f);
		}

		if (Spinning)
		{
			transform.parent.transform.localEulerAngles += new Vector3(0, Time.deltaTime * 36, 0);
		}
	}
}