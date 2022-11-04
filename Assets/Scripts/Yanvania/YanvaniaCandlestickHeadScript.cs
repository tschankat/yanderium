using UnityEngine;

public class YanvaniaCandlestickHeadScript : MonoBehaviour
{
	public GameObject Fire;

	public Vector3 Rotation;

	public float Value;

	void Start()
	{
		Rigidbody rigidBody = this.GetComponent<Rigidbody>();
		rigidBody.AddForce(this.transform.up * 100.0f);
		rigidBody.AddForce(this.transform.right * 100.0f);

		this.Value = Random.Range(-1.0f, 1.0f);
	}

	void Update()
	{
		this.Rotation += new Vector3(this.Value, this.Value, this.Value);

		this.transform.localEulerAngles = this.Rotation;

		if (this.transform.localPosition.y < 0.23f)
		{
			Instantiate(this.Fire, this.transform.position, Quaternion.identity);

			Destroy(this.gameObject);
		}
	}
}
