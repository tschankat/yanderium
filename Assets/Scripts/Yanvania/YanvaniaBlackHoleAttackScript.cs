using UnityEngine;

public class YanvaniaBlackHoleAttackScript : MonoBehaviour
{
	public YanvaniaYanmontScript Yanmont;

	public GameObject BlackExplosion;

	void Start()
	{
		this.Yanmont = GameObject.Find("YanmontChan").GetComponent<YanvaniaYanmontScript>();
	}

	void Update()
	{
		this.transform.position = Vector3.MoveTowards(
			this.transform.position,
			this.Yanmont.transform.position + Vector3.up,
			Time.deltaTime);

		if ((Vector3.Distance(this.transform.position, this.Yanmont.transform.position) > 10.0f) ||
			this.Yanmont.EnterCutscene)
		{
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			Instantiate(this.BlackExplosion, this.transform.position, Quaternion.identity);

			this.Yanmont.TakeDamage(20);
		}

		if (other.gameObject.name == "Heart")
		{
			Instantiate(this.BlackExplosion, this.transform.position, Quaternion.identity);

			Destroy(this.gameObject);
		}
	}
}
