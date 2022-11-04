using UnityEngine;

public class TrespassScript : MonoBehaviour
{
	public GameObject YandereObject;
	public YandereScript Yandere;

	public bool HideNotification = false;
	public bool OffLimits = false;

	void OnTriggerEnter(Collider other)
	{
		if (this.enabled)
		{
			if (other.gameObject.layer == 13)
			{
				this.YandereObject = other.gameObject;
				this.Yandere = other.gameObject.GetComponent<YandereScript>();

				if (this.Yandere != null)
				{
					if (!this.Yandere.Trespassing)
					{
						this.Yandere.NotificationManager.DisplayNotification(NotificationType.Intrude);
					}

					this.Yandere.Trespassing = true;
				}
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (this.Yandere != null)
		{
			if (other.gameObject == this.YandereObject)
			{
				this.Yandere.Trespassing = false;
			}
		}
	}
}