using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckScript : MonoBehaviour
{
	public StudentScript Student;

	public float Strength;

	void Update()
	{
		Strength += Time.deltaTime;

		transform.position = Vector3.MoveTowards(transform.position, Student.Yandere.Hips.position + (transform.up * .25f), Time.deltaTime * Strength);

		if (Vector3.Distance(transform.position, Student.Yandere.Hips.position + (transform.up * .25f)) < 1)
		{
			transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, Time.deltaTime);

			if (transform.localScale == Vector3.zero)
			{
				transform.parent.parent.parent.gameObject.SetActive(false);
			}
		}
	}
}