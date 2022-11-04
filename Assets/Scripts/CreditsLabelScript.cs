using UnityEngine;

public class CreditsLabelScript : MonoBehaviour
{
	public float RotationSpeed = 0.0f;
	public float MovementSpeed = 0.0f;
	public float Rotation = 0.0f;

	void Start()
	{
		this.Rotation = -90.0f;
		this.transform.localEulerAngles = new Vector3(
			this.transform.localEulerAngles.x,
			this.Rotation,
			this.transform.localEulerAngles.z);
	}

	void Update()
	{
		this.Rotation += Time.deltaTime * this.RotationSpeed;

		this.transform.localEulerAngles = new Vector3(
			this.transform.localEulerAngles.x,
			this.Rotation,
			this.transform.localEulerAngles.z);
		
		this.transform.localPosition = new Vector3(
			this.transform.localPosition.x,
			this.transform.localPosition.y + (Time.deltaTime * this.MovementSpeed),
			this.transform.localPosition.z);

		if (this.Rotation > 90.0f)
		{
			Destroy(this.gameObject);
		}
	}
}
