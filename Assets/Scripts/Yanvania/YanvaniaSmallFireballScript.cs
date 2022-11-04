using UnityEngine;

public class YanvaniaSmallFireballScript : MonoBehaviour
{
	public GameObject Explosion;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Heart")
		{
			Instantiate(this.Explosion, this.transform.position, Quaternion.identity);
			Destroy(this.gameObject);
		}

		if (other.gameObject.name == "YanmontChan")
		{
			other.gameObject.GetComponent<YanvaniaYanmontScript>().TakeDamage(10);

			Instantiate(this.Explosion, this.transform.position, Quaternion.identity);
			Destroy(this.gameObject);
		}
	}
}
