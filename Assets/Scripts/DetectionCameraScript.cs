using UnityEngine;

public class DetectionCameraScript : MonoBehaviour
{
	public Transform YandereChan;

	void Update()
	{
		this.transform.position = this.YandereChan.transform.position + (Vector3.up * 100.0f);
		this.transform.eulerAngles = new Vector3(
			90.0f,
			this.transform.eulerAngles.y,
			this.transform.eulerAngles.z);
	}
}
