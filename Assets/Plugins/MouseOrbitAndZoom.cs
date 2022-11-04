using UnityEngine;

[AddComponentMenu("Camera-Control/Mouse Orbit")]
public class MouseOrbitAndZoom : MonoBehaviour
{
	public Transform target;
	public float distance = 10.0f;

	public float xSpeed = 250.0f;
	public float ySpeed = 120.0f;
	public float zSpeed = -100.0f;

	public float zMaxLimit = 10.0f;
	public float zMinLimit = 0.50f;

	public float yMinLimit = -20.0f;
	public float yMaxLimit = 80.0f;

	private float x = 0.0f;
	private float y = 0.0f;

	void Start()
	{
		Vector3 angles = this.transform.eulerAngles;
		this.x = angles.y;
		this.y = angles.x;

		// Make the rigid body not change rotation.
		Rigidbody rigidBody = this.GetComponent<Rigidbody>();
		if (rigidBody != null)
		{
			rigidBody.freezeRotation = true;
		}
	}

	void LateUpdate()
	{
		if (this.target != null)
		{
			this.x += Input.GetAxis("Mouse X") * this.xSpeed * 0.020f;
			this.y -= Input.GetAxis("Mouse Y") * this.ySpeed * 0.020f;

			this.y = ClampAngle(this.y, this.yMinLimit, this.yMaxLimit);
			this.distance += Input.GetAxis("Mouse ScrollWheel") * this.zSpeed * 0.020f;
			this.distance = ClampAngle(this.distance, this.zMinLimit, this.zMaxLimit);

			Quaternion rotation = Quaternion.Euler(this.y, this.x, 0.0f);
			Vector3 position = rotation * new Vector3(0.0f, 0.0f, -this.distance) +
				this.target.position;

			this.transform.rotation = rotation;
			this.transform.position = position;
		}
	}

	static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360.0f)
		{
			angle += 360.0f;
		}

		if (angle > 360.0f)
		{
			angle -= 360.0f;
		}

		return Mathf.Clamp(angle, min, max);
	}
}
