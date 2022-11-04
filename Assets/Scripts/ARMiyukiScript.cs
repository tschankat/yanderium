using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARMiyukiScript : MonoBehaviour
{
	public Transform BulletSpawnPoint;
	public StudentScript MyStudent;
	public YandereScript Yandere;
	public GameObject Bullet;
	public Transform Enemy;

	public GameObject MagicalGirl;

	public bool Student;

	void Start()
	{
		if (Enemy == null)
		{
			Enemy = MyStudent.StudentManager.MiyukiCat;
		}
	}

	void Update()
	{
		if (!Student)
		{
			if (Yandere.AR)
			{
				transform.LookAt(Enemy.position);

				if (Input.GetButtonDown(InputNames.Xbox_X))
				{
					Shoot();
				}
			}
		}
	}

	public void Shoot()
	{
		if (Enemy == null)
		{
			Enemy = MyStudent.StudentManager.MiyukiCat;
		}

		transform.LookAt(Enemy.position);
		Instantiate(Bullet, BulletSpawnPoint.position, transform.rotation);
	}
}