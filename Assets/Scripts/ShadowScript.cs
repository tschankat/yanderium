using UnityEngine;

public class ShadowScript : MonoBehaviour
{
	public Transform Foot;

	void Update()
	{
		Vector3 position = this.transform.position;
		Vector3 footPosition = this.Foot.position;
		position.x = footPosition.x;
		position.z = footPosition.z;
		this.transform.position = position;
	}
}
