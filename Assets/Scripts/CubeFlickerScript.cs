using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFlickerScript : MonoBehaviour
{
	public Transform[] Cube;

	void Update()
	{
		Cube[0].localScale = new Vector3(Random.Range(0.0f, 0.1f), Random.Range(0.0f, 0.1f), Random.Range(0.0f, 0.1f));
		Cube[1].localScale = new Vector3(Random.Range(0.0f, 0.1f), Random.Range(0.0f, 0.1f), Random.Range(0.0f, 0.1f));
		Cube[2].localScale = new Vector3(Random.Range(0.0f, 0.1f), Random.Range(0.0f, 0.1f), Random.Range(0.0f, 0.1f));
		Cube[3].localScale = new Vector3(Random.Range(0.0f, 0.1f), Random.Range(0.0f, 0.1f), Random.Range(0.0f, 0.1f));
		Cube[4].localScale = new Vector3(Random.Range(0.0f, 0.1f), Random.Range(0.0f, 0.1f), Random.Range(0.0f, 0.1f));

		Cube[0].position = transform.position + new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(1.0f, 2.0f), Random.Range(-1.0f, 1.0f));
		Cube[1].position = transform.position + new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(1.0f, 2.0f), Random.Range(-1.0f, 1.0f));
		Cube[2].position = transform.position + new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(1.0f, 2.0f), Random.Range(-1.0f, 1.0f));
		Cube[3].position = transform.position + new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(1.0f, 2.0f), Random.Range(-1.0f, 1.0f));
		Cube[4].position = transform.position + new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(1.0f, 2.0f), Random.Range(-1.0f, 1.0f));
	}
}