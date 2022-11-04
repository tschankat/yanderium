using UnityEngine;

public class SplashSpawnerScript : MonoBehaviour
{
	public GameObject BloodSplash;
	public Transform Yandere;
	public bool Bloody = false;
	public bool FootUp = false;

	public float DownThreshold = 0.0f;
	public float UpThreshold = 0.0f;
	public float Height = 0.0f;

	void Update()
	{
		if (!this.FootUp)
		{
			if (this.transform.position.y > (this.Yandere.transform.position.y + this.UpThreshold))
			{
				this.FootUp = true;
			}
		}
		else
		{
			if (this.transform.position.y < (this.Yandere.transform.position.y + this.DownThreshold))
			{
				this.FootUp = false;

				if (this.Bloody)
				{
					GameObject NewBloodSplash = Instantiate(this.BloodSplash,
						new Vector3(this.transform.position.x, this.Yandere.position.y, this.transform.position.z),
						Quaternion.identity);
					NewBloodSplash.transform.eulerAngles = new Vector3(
						-90.0f,
						NewBloodSplash.transform.eulerAngles.y,
						NewBloodSplash.transform.eulerAngles.z);

					this.Bloody = false;
				}
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "BloodPool(Clone)")
		{
			this.Bloody = true;
		}
	}
}
