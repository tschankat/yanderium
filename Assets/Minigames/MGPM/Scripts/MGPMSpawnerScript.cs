using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGPMSpawnerScript : MonoBehaviour
{
	public MGPMManagerScript GameplayManager;
	public MGPMMiyukiScript Miyuki;

	public Transform[] SpawnPositions;
	public float[] SpawnTimers;
	public int[] SpawnEnemies;

	public GameObject[] LoadBearer;
	public GameObject[] Enemy;

	public Transform HealthBar;

	public float SpawnRate;
	public float Timer;

	public bool RandomMode;

	public int Wave;
	public int ID;

	void Start()
	{
		if (Wave == 8 || Wave == 9)
		{
			ID = 1;

			while (ID < 100)
			{
				SpawnTimers[ID] = SpawnTimers[ID - 1] + .1f;
				ID++;
			}

			ID = 0;
		}
	}

	void Update ()
	{
		#if UNITY_EDITOR

		if (Input.GetKeyDown("space"))
		{
			Timer += 10;
		}

		#endif

		Timer += Time.deltaTime;

		if (ID < SpawnTimers.Length)
		{
			if (Timer > SpawnTimers[ID])
			{
				GameObject NewEnemy;

				NewEnemy = Instantiate(Enemy[SpawnEnemies[ID]], transform.position, Quaternion.identity);

				NewEnemy.transform.parent = transform.parent;

				if (SpawnEnemies[ID] == 4 || SpawnEnemies[ID] == 11)
				{
					NewEnemy.transform.localScale = new Vector3(1, 1, 1);
				}
				else if (SpawnEnemies[ID] == 6 || SpawnEnemies[ID] == 9 || SpawnEnemies[ID] == 12)
				{
					NewEnemy.transform.localScale = new Vector3(128, 128, 1);
				}
				else
				{
					NewEnemy.transform.localScale = new Vector3(64, 64, 1);
				}

				NewEnemy.transform.position = SpawnPositions[ID].position;

				MGPMEnemyScript NewEnemyScript = NewEnemy.GetComponent<MGPMEnemyScript>();
				NewEnemyScript.GameplayManager = GameplayManager;
				NewEnemyScript.Miyuki = Miyuki;

				if (Wave == 9)
				{
					if (ID < 100)
					{
						SpawnPositions[ID].localPosition = new Vector3(
							Random.Range(-100.0f, 100.0f),
							0,
							0);
					}
					else
					{
						if (ID == 100)
						{
							LoadBearer[1] = NewEnemy;
						}
						else if (ID == 101)
						{
							LoadBearer[2] = NewEnemy;
						}
					}
				}

				ID++;
			}
		}
		else
		{
			if (Wave == 9)
			{
				if (LoadBearer[1] == null && LoadBearer[2] == null)
				{
					GameplayManager.Jukebox.volume = Mathf.MoveTowards(GameplayManager.Jukebox.volume, 0, Time.deltaTime * .5f);

					if (GameplayManager.Jukebox.volume == 0)
					{
						GameObject NewEnemy;

						NewEnemy = Instantiate(Enemy[SpawnEnemies[ID]], transform.position, Quaternion.identity);

						NewEnemy.transform.parent = transform.parent;
						NewEnemy.transform.localScale = new Vector3(256, 128, 1);
						NewEnemy.transform.position = SpawnPositions[ID].position;

						MGPMEnemyScript NewEnemyScript = NewEnemy.GetComponent<MGPMEnemyScript>();
						NewEnemyScript.GameplayManager = GameplayManager;
						NewEnemyScript.HealthBar = HealthBar;
						NewEnemyScript.Miyuki = Miyuki;

						HealthBar.parent.gameObject.SetActive(true);

						GameplayManager.Jukebox.clip = GameplayManager.FinalBoss;
						GameplayManager.Jukebox.volume = .5f;
						GameplayManager.Jukebox.Play();

						enabled = false;
					}
				}
			}
		}
	}
}