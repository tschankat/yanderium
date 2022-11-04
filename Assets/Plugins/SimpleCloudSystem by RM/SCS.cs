using UnityEngine;

public class SCS : MonoBehaviour
{
	public Transform Player;
	public float CloudsSpeed;

	private float height = 0.0f;
	private float heightDamping = 0.0f;

	void Update()
	{
		// Rotate around the object's local Y axis.
		this.transform.Rotate(0.0f, this.CloudsSpeed * Time.deltaTime, 0.0f);
	}

	void LateUpdate()
	{
		if (this.Player == null)
		{
			return;
		}

		float wantedHeight = this.Player.position.y + this.height;
		float currentHeight = Mathf.Lerp(this.transform.position.y, wantedHeight,
			this.heightDamping * Time.deltaTime);

		this.transform.position = new Vector3(
			this.Player.position.x,
			currentHeight,
			this.Player.position.z);
	}
}
