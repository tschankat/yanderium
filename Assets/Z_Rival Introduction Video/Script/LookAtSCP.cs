using UnityEngine;

public class LookAtSCP : MonoBehaviour
{
	public Transform SCP;

	void Start()
	{
		if (this.SCP == null)
		{
			this.SCP = GameObject.Find("SCPTarget").transform;
		}

		this.transform.LookAt(this.SCP);
	}

	void LateUpdate()
	{
		this.transform.LookAt(this.SCP);
	}
}
