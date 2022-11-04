using UnityEngine;

public class VibrateScript : MonoBehaviour
{
	public Vector3 Origin;

	void Start()
	{
		this.Origin = this.transform.localPosition;
	}

	void Update()
	{
		this.transform.localPosition = new Vector3(
			this.Origin.x + Random.Range(-5.0f, 5.0f),
			this.Origin.y + Random.Range(-5.0f, 5.0f),
			this.transform.localPosition.z);
	}
}
