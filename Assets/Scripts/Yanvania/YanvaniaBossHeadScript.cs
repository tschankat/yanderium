using UnityEngine;

public class YanvaniaBossHeadScript : MonoBehaviour
{
	public YanvaniaDraculaScript Dracula;

	public GameObject HitEffect;

	public float Timer = 0.0f;

	void Update()
	{
		this.Timer -= Time.deltaTime;
	}

	void OnTriggerEnter(Collider other)
	{
		if (this.Timer <= 0.0f)
		{
			if (this.Dracula.NewTeleportEffect == null)
			{
				if (other.gameObject.name == "Heart")
				{
					Instantiate(this.HitEffect, this.transform.position, Quaternion.identity);

					this.Timer = 1.0f;

					this.Dracula.TakeDamage();
				}
			}
		}
	}
}
