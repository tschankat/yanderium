using UnityEngine;

public class RoseSpawnerScript : MonoBehaviour
{
	public Transform DramaGirl;
	public Transform Target;

	public GameObject Rose;

	public float Timer = 0.0f;

	public float ForwardForce = 0.0f;
	public float UpwardForce = 0.0f;

	void Start()
	{
		this.SpawnRose();
	}

	void Update()
	{
		this.Timer += Time.deltaTime;

		if (this.Timer > 0.10f)
		{
			this.SpawnRose();
		}
	}

	void SpawnRose()
	{
		GameObject NewRose = Instantiate(this.Rose, this.transform.position, Quaternion.identity);
		NewRose.GetComponent<Rigidbody>().AddForce(this.transform.forward * this.ForwardForce);
		NewRose.GetComponent<Rigidbody>().AddForce(this.transform.up * this.UpwardForce);

		NewRose.transform.localEulerAngles = new Vector3(
			Random.Range(0.0f, 360.0f),
			Random.Range(0.0f, 360.0f),
			Random.Range(0.0f, 360.0f));

		this.transform.localPosition = new Vector3(
			Random.Range(-5.0f, 5.0f),
			this.transform.localPosition.y,
			this.transform.localPosition.z);

		this.transform.LookAt(this.DramaGirl);

		this.Timer = 0.0f;
	}
}
