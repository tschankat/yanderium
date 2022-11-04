using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedrunMenuScript : MonoBehaviour
{
	public Animation YandereAnim;

	void Start ()
	{
		YandereAnim["f02_nierRun_00"].speed = 1.5f;
	}

	void Update ()
	{
		
	}
}