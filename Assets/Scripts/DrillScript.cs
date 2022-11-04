using UnityEngine;

public class DrillScript : MonoBehaviour
{
	void LateUpdate()
	{
		this.transform.Rotate(Vector3.up * Time.deltaTime * 3600.0f);
	}
}
