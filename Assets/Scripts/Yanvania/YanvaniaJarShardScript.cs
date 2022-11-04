using UnityEngine;

public class YanvaniaJarShardScript : MonoBehaviour
{
	public float MyRotation = 0.0f;
	public float Rotation = 0.0f;

	void Start()
	{
		this.Rotation = Random.Range(-360.0f, 360.0f);

		this.GetComponent<Rigidbody>().AddForce(
			Random.Range(-100.0f, 100.0f),
			Random.Range(0.0f, 100.0f),
			Random.Range(-100.0f, 100.0f));
	}

	void Update()
	{
		this.MyRotation += this.Rotation;

		this.transform.eulerAngles = new Vector3(
			this.MyRotation, this.MyRotation, this.MyRotation);

		if (this.transform.position.y < 6.50f)
		{
			Destroy(this.gameObject);
		}
	}
}
