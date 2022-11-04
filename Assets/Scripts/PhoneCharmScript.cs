using UnityEngine;

public class PhoneCharmScript : MonoBehaviour
{
	void Update()
	{
		this.transform.eulerAngles = new Vector3 (
			90.0f,
			this.transform.eulerAngles.y,
			this.transform.eulerAngles.z);
	}
}
