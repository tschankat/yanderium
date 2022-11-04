using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodScript : MonoBehaviour
{
	public GameObject Projectile;

	public Transform SpawnPoint;
	public Transform PodTarget;
	public Transform AimTarget;

	public float FireRate;
	public float Timer;

	void Start()
	{
		Timer = 1;
	}

	void LateUpdate()
	{
		PodTarget.transform.parent.eulerAngles = new Vector3(
			0,
			AimTarget.parent.eulerAngles.y,
			0);

		transform.position = Vector3.Lerp(transform.position, PodTarget.position, Time.deltaTime * 100);

		transform.rotation = AimTarget.parent.rotation;
		transform.eulerAngles += new Vector3(-15, 7.5f, 0);

		//transform.LookAt(AimTarget);

		if (Input.GetButton(InputNames.Xbox_RB))
		{
			Timer += Time.deltaTime;

			if (Timer > FireRate)
			{
				Instantiate(Projectile, SpawnPoint.position, transform.rotation);

				Timer = 0;
			}
		}
	}
}