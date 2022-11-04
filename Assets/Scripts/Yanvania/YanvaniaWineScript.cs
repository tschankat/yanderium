using UnityEngine;

public class YanvaniaWineScript : MonoBehaviour
{
	public GameObject Shards;

	public float Rotation = 0.0f;

	void Update()
	{
		if (this.transform.parent == null)
		{
			this.Rotation += Time.deltaTime * 360.0f;

			this.transform.localEulerAngles = new Vector3(
				this.Rotation, this.Rotation, this.Rotation);

			if (this.transform.position.y < 6.50f)
			{
				var NewShards = Instantiate(this.Shards,
					new Vector3(this.transform.position.x, 6.50f, this.transform.position.z),
					Quaternion.identity);
				NewShards.transform.localEulerAngles = new Vector3(-90.0f, 0.0f, 0.0f);

				AudioSource.PlayClipAtPoint(
					this.GetComponent<AudioSource>().clip, this.transform.position);

				Destroy(this.gameObject);
			}
		}
	}
}
