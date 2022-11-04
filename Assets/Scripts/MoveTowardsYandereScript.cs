using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsYandereScript : MonoBehaviour
{
	public ParticleSystem Smoke;

	public Transform Yandere;

	public float Distance;
	public float Speed;

	public bool Fall;

	void Start()
	{
		Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>().Spine[3];
		Distance = Vector3.Distance(transform.position, Yandere.position);
	}

	void Update()
	{
		if (Vector3.Distance(transform.position, Yandere.position) > Distance * .5f)
		{
			if (transform.position.y < Yandere.position.y + .5f)
			{
				transform.position = new Vector3(
					transform.position.x,
					transform.position.y + Time.deltaTime,
					transform.position.z);
			}
		}

		Speed += Time.deltaTime;

		transform.position = Vector3.MoveTowards(transform.position, Yandere.position, Speed * Time.deltaTime);

		if (Vector3.Distance(transform.position, Yandere.position) == 0)
		{
			ParticleSystem.EmissionModule Emission = this.Smoke.emission;
			Emission.enabled = false;
		}
	}
}
