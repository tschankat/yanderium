using UnityEngine;

public class YanvaniaZombieSpawnerScript : MonoBehaviour
{
	public YanvaniaZombieScript NewZombieScript;

	public GameObject Zombie;

	public YanvaniaYanmontScript Yanmont;

	public float SpawnTimer = 0.0f;

	public float RelativePoint = 0.0f;
	public float RightBoundary = 0.0f;
	public float LeftBoundary = 0.0f;
	public int SpawnSide = 0;
	public int ID = 0;

	public GameObject[] Zombies;
	public Vector3[] SpawnPoints;

	void Update()
	{
		if (this.Yanmont.transform.position.y > 0.0f)
		{
			this.ID = 0;

			this.SpawnTimer += Time.deltaTime;

			if (this.SpawnTimer > 1.0f)
			{
				// [af] Converted while loop to for loop.
				for (; this.ID < 4; this.ID++)
				{
					if (this.Zombies[this.ID] == null)
					{
						this.SpawnSide = Random.Range(1, 3);

						if (this.Yanmont.transform.position.x < (this.LeftBoundary + 5.0f))
						{
							this.SpawnSide = 2;
						}

						if (this.Yanmont.transform.position.x > (this.RightBoundary - 5.0f))
						{
							this.SpawnSide = 1;
						}

						if (this.Yanmont.transform.position.x < this.LeftBoundary)
						{
							this.RelativePoint = this.LeftBoundary;
						}
						else if (this.Yanmont.transform.position.x > this.RightBoundary)
						{
							this.RelativePoint = this.RightBoundary;
						}
						else
						{
							this.RelativePoint = this.Yanmont.transform.position.x;
						}

						if (this.SpawnSide == 1)
						{
							this.SpawnPoints[0].x = this.RelativePoint - 2.50f;
							this.SpawnPoints[1].x = this.RelativePoint - 3.50f;
							this.SpawnPoints[2].x = this.RelativePoint - 4.50f;
							this.SpawnPoints[3].x = this.RelativePoint - 5.50f;
						}
						else
						{
							this.SpawnPoints[0].x = this.RelativePoint + 2.50f;
							this.SpawnPoints[1].x = this.RelativePoint + 3.50f;
							this.SpawnPoints[2].x = this.RelativePoint + 4.50f;
							this.SpawnPoints[3].x = this.RelativePoint + 5.50f;
						}

						this.Zombies[this.ID] = Instantiate(this.Zombie,
							this.SpawnPoints[this.ID], Quaternion.identity);
						this.NewZombieScript = this.Zombies[this.ID].GetComponent<YanvaniaZombieScript>();
						this.NewZombieScript.LeftBoundary = this.LeftBoundary;
						this.NewZombieScript.RightBoundary = this.RightBoundary;
						this.NewZombieScript.Yanmont = this.Yanmont;

						break;
					}
				}

				this.SpawnTimer = 0.0f;
			}
		}
	}
}
