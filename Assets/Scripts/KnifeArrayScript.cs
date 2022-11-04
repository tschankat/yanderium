using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeArrayScript : MonoBehaviour
{
	public GlobalKnifeArrayScript GlobalKnifeArray;

	public Transform KnifeTarget;

	public float[] SpawnTimes;

	public GameObject Knife;

	public float Timer;

	public int ID;

	void Update ()
	{
		Timer += Time.deltaTime;

		if (ID < 10)
		{
			if (Timer > SpawnTimes[ID])
			{
				if (GlobalKnifeArray.ID < 1000)
				{
					GameObject NewKnife = Instantiate(Knife, transform.position, Quaternion.identity);
					NewKnife.transform.parent = transform;

					NewKnife.transform.localPosition = new Vector3(
						Random.Range(-1.0f, 1.0f),
						Random.Range(0.5f, 2.0f),
						Random.Range(-.75f, -1.75f));

					NewKnife.transform.parent = null;

					NewKnife.transform.LookAt(KnifeTarget);
					GlobalKnifeArray.Knives[GlobalKnifeArray.ID] = NewKnife.GetComponent<TimeStopKnifeScript>();
					GlobalKnifeArray.ID++;

					ID++;
				}
			}
		}
		else
		{
			Destroy(gameObject);
		}
	}
}