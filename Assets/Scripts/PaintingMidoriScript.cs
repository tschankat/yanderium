using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingMidoriScript : MonoBehaviour
{
	public Animation Anim;

	public float Rotation;

	public int ID;

	void Update ()
	{
		if (Input.GetKeyDown("z"))
		{
			ID++;
		}

		if (ID == 0)
		{
			Anim.CrossFade("f02_painting_00");
		}
		else if (ID == 1)
		{
			Anim.CrossFade("f02_shock_00");
			Rotation = Mathf.Lerp(Rotation, -180, Time.deltaTime * 10);
		}
		else if (ID == 2)
		{
			transform.position -= new Vector3(Time.deltaTime * 2, 0, 0);
		}

		transform.localEulerAngles = new Vector3(0, Rotation, 0);
	}
}