using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiyukiEnemyScript : MonoBehaviour
{
	public float Float;
	public float Limit;
	public float Speed;

	public bool Dead;
	public bool Down;

	public GameObject DeathEffect;
	public GameObject HitEffect;
	public GameObject Enemy;

	public Transform[] SpawnPoints;
	public float RespawnTimer;
	public float Health;
	public int ID;

	void Start()
	{
		transform.position = SpawnPoints[ID].position;
		transform.rotation = SpawnPoints[ID].rotation;
	}

	void Update ()
	{
		if (Enemy.activeInHierarchy)
		{
			if (!Down)
			{
				Float += Time.deltaTime * Speed;

				if (Float > Limit)
				{
					Down = true;
				}
			}
			else
			{
				Float -= Time.deltaTime * Speed;

				if (Float < -1 * Limit)
				{
					Down = false;
				}
			}

			Enemy.transform.position += new Vector3(0, Float * Time.deltaTime, 0);

			if (Enemy.transform.position.y > SpawnPoints[ID].position.y + 1.5f){Enemy.transform.position = new Vector3(Enemy.transform.position.x, SpawnPoints[ID].position.y + 1.5f, Enemy.transform.position.z);}
			if (Enemy.transform.position.y < SpawnPoints[ID].position.y + .5f){Enemy.transform.position = new Vector3(Enemy.transform.position.x, SpawnPoints[ID].position.y + .5f, Enemy.transform.position.z);}
		}
		else
		{
			RespawnTimer += Time.deltaTime;

			if (RespawnTimer > 5)
			{
				transform.position = SpawnPoints[ID].position;
				transform.rotation = SpawnPoints[ID].rotation;

				Enemy.SetActive(true);

				RespawnTimer = 0;
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (Enemy.activeInHierarchy && other.gameObject.tag == "missile")
		{
			Instantiate(HitEffect, other.transform.position, Quaternion.identity);

			Destroy(other.gameObject);

			Health--;

			if (Health == 0)
			{
				Instantiate(DeathEffect, other.transform.position, Quaternion.identity);

				Enemy.SetActive(false);

				Health = 50;

				ID++;

				if (ID >= SpawnPoints.Length)
				{
					ID = 0;
				}
			}
		}
	}
}