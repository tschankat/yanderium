using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SciFiTabletScript : MonoBehaviour
{
	public StudentScript Student;
	public HologramScript Holograms;

	public Transform Finger;

	public bool Updated;

	void Start()
	{
		Holograms = Student.StudentManager.Holograms;
	}

	void Update ()
	{
		if (Vector3.Distance(Finger.position, transform.position) < .1)
		{
			if (!Updated)
			{
				Holograms.UpdateHolograms();
				Updated = true;
			}
		}
		else
		{
			Updated = false;
		}
	}
}
