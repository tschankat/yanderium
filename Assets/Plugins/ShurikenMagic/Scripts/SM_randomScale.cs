using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_randomScale : MonoBehaviour
{

	public float minScale = 1;
	public float maxScale = 2;

	void Start ()
	{
		var actualRandom=Random.Range(minScale, maxScale);
		transform.localScale = new Vector3(actualRandom, actualRandom, actualRandom);

	}


	void Update () {


	}
}