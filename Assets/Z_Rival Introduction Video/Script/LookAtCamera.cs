using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
	public Camera cameraToLookAt;

	void Start()
	{
		if (cameraToLookAt == null)
		{
			cameraToLookAt = Camera.main;
		}
	}

	void Update()
	{
		Vector3 v = new Vector3(
			0.0f,
			this.cameraToLookAt.transform.position.y - this.transform.position.y,
			0.0f);

		this.transform.LookAt(this.cameraToLookAt.transform.position - v);
	}
}
