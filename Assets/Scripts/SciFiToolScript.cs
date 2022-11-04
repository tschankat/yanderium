using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SciFiToolScript : MonoBehaviour
{
	public StudentScript Student;
	public ParticleSystem Sparks;

	public Transform Target;
	public Transform Tip;

	void Start()
	{
		Target = Student.StudentManager.ToolTarget;
	}

	void Update ()
	{
		if (Vector3.Distance(Tip.position, Target.position) < .1)
		{
			Sparks.Play();
		}
		else
		{
			Sparks.Stop();
		}
	}
}
