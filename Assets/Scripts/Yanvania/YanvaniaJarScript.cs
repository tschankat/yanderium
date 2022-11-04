using UnityEngine;

public class YanvaniaJarScript : MonoBehaviour
{
	public GameObject Explosion;

	public bool Destroyed = false;

	public AudioClip Break;

	public GameObject Shard;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 19)
		{
			if (!this.Destroyed)
			{
				Instantiate(this.Explosion,
					this.transform.position + (Vector3.up * 0.50f), Quaternion.identity);

				this.Destroyed = true;

				AudioClipPlayer.Play2D(this.Break, this.transform.position);

				// [af] Converted while loop to for loop.
				for (int ID = 1; ID < 11; ID++)
				{
					Instantiate(this.Shard,
						this.transform.position +
						(Vector3.up * Random.Range(0.0f, 1.0f)) +
						(Vector3.right * Random.Range(-0.50f, 0.50f)),
						Quaternion.identity);
				}

				Destroy(this.gameObject);
			}
		}
	}
}
