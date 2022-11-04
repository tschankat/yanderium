using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeTestScript : MonoBehaviour
{
	public Animation MyAnimation;

	void Start ()
	{
		this.MyAnimation["moodyEyes_00"].layer = 1;
		this.MyAnimation.Play("moodyEyes_00");
		this.MyAnimation["moodyEyes_00"].weight = 1.0f;
		this.MyAnimation.Play("moodyEyes_00");
	}
}