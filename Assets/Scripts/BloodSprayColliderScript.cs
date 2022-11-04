using UnityEngine;

public class BloodSprayColliderScript : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 13)
		{
			YandereScript Yandere = other.gameObject.GetComponent<YandereScript>();

			if (Yandere != null)
			{
				Yandere.Bloodiness = 100.0f;

				Destroy(this.gameObject);
			}
		}
	}
}
