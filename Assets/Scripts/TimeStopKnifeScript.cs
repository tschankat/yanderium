using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStopKnifeScript : MonoBehaviour
{
	public GameObject FemaleScream;
	public GameObject MaleScream;

	public bool Unfreeze;

	public float Speed = .1f;

	float Timer = 0;

	void Start()
	{
		transform.localScale = new Vector3(0, 0, 0);
	}

	void Update ()
	{
		if (!Unfreeze)
		{
			Speed = Mathf.MoveTowards(Speed, 0, Time.deltaTime);

			if (transform.localScale.x < .99f)
			{
				transform.localScale = Vector3.Lerp(
					transform.localScale,
					new Vector3(1, 1, 1),
					Time.deltaTime * 10);
			}
		}
		else
		{
			Speed = 10;

			Timer += Time.deltaTime;

			if (Timer > 5)
			{
				Destroy(gameObject);
			}
		}

		transform.Translate(Vector3.forward * Speed * Time.deltaTime, Space.Self);
	}

	void OnTriggerEnter(Collider other)
	{
		if (Unfreeze)
		{
			if (other.gameObject.layer == 9)
			{
				StudentScript Student = other.gameObject.GetComponent<StudentScript>();

				if (Student != null)
				{
					if (Student.StudentID > 1)
					{
						if (Student.Male)
						{
							Instantiate(MaleScream, transform.position, Quaternion.identity);
						}
						else
						{
							Instantiate(FemaleScream, transform.position, Quaternion.identity);
						}

						Student.DeathType = DeathType.EasterEgg;
						Student.BecomeRagdoll();
					}
				}
			}
		}
	}
}