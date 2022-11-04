using UnityEngine;

public class PoliceWalk : MonoBehaviour
{
	void Update()
	{
		Vector3 position = this.transform.position;
		position.z += Time.deltaTime;
		this.transform.position = position;
	}
}
