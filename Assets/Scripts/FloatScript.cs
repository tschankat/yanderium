using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatScript : MonoBehaviour
{
	public bool Down;

	public float Float;
	public float Speed;
	public float Limit;

	public float DownLimit;
	public float UpLimit;

	void Update()
	{
		if (!Down)
		{
			Float += Time.deltaTime * Speed;

			if (Float > Limit)
			{
				Down = true;
			}
		}
		else
		{
			Float -= Time.deltaTime * Speed;

			if (Float < -1 * Limit)
			{
				Down = false;
			}
		}

		transform.localPosition += new Vector3(0, Float * Time.deltaTime, 0);

		if (transform.localPosition.y > UpLimit){transform.localPosition = new Vector3(transform.localPosition.x, UpLimit, transform.localPosition.z);}
		if (transform.localPosition.y < DownLimit){transform.localPosition = new Vector3(transform.localPosition.x, DownLimit, transform.localPosition.z);}
	}
}
