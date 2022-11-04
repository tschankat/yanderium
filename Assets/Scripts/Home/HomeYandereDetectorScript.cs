using UnityEngine;

public class HomeYandereDetectorScript : MonoBehaviour
{
	public bool YandereDetected = false;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			this.YandereDetected = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			this.YandereDetected = false;
		}
	}
}
