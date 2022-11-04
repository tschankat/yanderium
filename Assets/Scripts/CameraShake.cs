using UnityEngine;

public class CameraShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;

	// How long the object should shake for.
	public float shake = 0f;

	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;

	Vector3 originalPos;

	void Awake()
	{
		if (this.camTransform == null)
		{
			this.camTransform = this.GetComponent<Transform>();
		}
	}

	void OnEnable()
	{
		this.originalPos = this.camTransform.localPosition;
	}

	void Update()
	{
		if (this.shake > 0.0f)
		{
			this.camTransform.localPosition = this.originalPos +
				(Random.insideUnitSphere * this.shakeAmount);

			this.shake -= (1.0f / 60.0f) * this.decreaseFactor;
		}
		else
		{
			this.shake = 0.0f;
			this.camTransform.localPosition = this.originalPos;
		}
	}
}
