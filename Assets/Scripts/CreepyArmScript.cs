using UnityEngine;

public class CreepyArmScript : MonoBehaviour
{
	void Update()
	{
		this.transform.position = new Vector3(
			this.transform.position.x,
			this.transform.position.y + (Time.deltaTime * 0.10f),
			this.transform.position.z);
	}
}
