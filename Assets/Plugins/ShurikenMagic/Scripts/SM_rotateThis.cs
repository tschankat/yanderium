using UnityEngine;

public class SM_rotateThis : MonoBehaviour
{
	public float rotationSpeedX = 90.0f;
	public float rotationSpeedY = 0.0f;
	public float rotationSpeedZ = 0.0f;
	// [af] Removed unused "rotationVector" member.

	void Update()
	{
		this.transform.Rotate(
			new Vector3(rotationSpeedX, rotationSpeedY, rotationSpeedZ) * Time.deltaTime);
	}
}
