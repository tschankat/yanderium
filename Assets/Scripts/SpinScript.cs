using UnityEngine;

public class SpinScript : MonoBehaviour
{
	public float X = 0.0f;
	public float Y = 0.0f;
	public float Z = 0.0f;
	private float RotationX = 0.0f;
	private float RotationY = 0.0f;
	private float RotationZ = 0.0f;

	void Update()
	{
		this.RotationX += this.X * Time.deltaTime;
		this.RotationY += this.Y * Time.deltaTime;
		this.RotationZ += this.Z * Time.deltaTime;
		
		this.transform.localEulerAngles = new Vector3(
			this.RotationX, this.RotationY, this.RotationZ);
	}
}
