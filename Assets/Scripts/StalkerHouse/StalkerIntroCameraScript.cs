using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerIntroCameraScript : MonoBehaviour
{
	public Animation YandereAnim;

	public Transform Yandere;

	public float Speed;

	void Update () 
	{
		if (YandereAnim["f02_wallJump_00"].time > YandereAnim["f02_wallJump_00"].length)
		{
			Speed += Time.deltaTime;

			Yandere.position = Vector3.Lerp(Yandere.position, new Vector3(14.33333f, 0, 15), Time.deltaTime * Speed);

			transform.position = Vector3.Lerp(transform.position, new Vector3(13.75f, 1.4f, 14.5f), Time.deltaTime * Speed);
			transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(15, 180, 0), Time.deltaTime * Speed);
		}
	}
}
