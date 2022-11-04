using UnityEngine;

public class YanvaniaBigFireballScript : MonoBehaviour
{
	public GameObject Explosion;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "YanmontChan")
		{
			other.gameObject.GetComponent<YanvaniaYanmontScript>().TakeDamage(15);

			Instantiate(this.Explosion, this.transform.position, Quaternion.identity);
			Destroy(this.gameObject);
		}
	}
}
