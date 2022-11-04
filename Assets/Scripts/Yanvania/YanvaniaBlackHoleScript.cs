using UnityEngine;

public class YanvaniaBlackHoleScript : MonoBehaviour
{
	public GameObject BlackHoleAttack;

	public int Attacks = 0;

	public float SpawnTimer = 0.0f;

	public float Timer = 0.0f;

	void Update()
	{
		this.Timer += Time.deltaTime;

		if (this.Timer > 1.0f)
		{
			this.SpawnTimer -= Time.deltaTime;

			if (this.SpawnTimer <= 0.0f)
			{
				if (this.Attacks < 5)
				{
					Instantiate(this.BlackHoleAttack, this.transform.position, Quaternion.identity);

					this.SpawnTimer = 0.50f;

					this.Attacks++;
				}
			}
		}
	}
}
