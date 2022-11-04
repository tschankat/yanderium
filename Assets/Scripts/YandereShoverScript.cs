using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YandereShoverScript : MonoBehaviour
{
	public YandereScript Yandere;

	public bool PreventNudity;

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.layer == 13)
		{
			bool Continue = false;

			if (PreventNudity)
			{
				if (this.Yandere.Schoolwear == 0)
				{
					Continue = true;

					if (Yandere.NotificationManager.NotificationParent.childCount == 0)
					{
						Yandere.NotificationManager.CustomText = "Get dressed first!";
						Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
					}
				}
			}
			else
			{
				Continue = true;

				if (Yandere.NotificationManager.NotificationParent.childCount == 0)
				{
					Yandere.NotificationManager.CustomText = "That's the boys' locker room!";
					Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
				}
			}

			if (Continue)
			{
				if (this.Yandere.Aiming)
				{
					this.Yandere.StopAiming();
				}

				if (this.Yandere.Laughing)
				{
					this.Yandere.StopLaughing();
				}

				this.Yandere.transform.rotation = Quaternion.LookRotation(new Vector3(
				this.transform.position.x,
				this.Yandere.transform.position.y,
				this.transform.position.z)
				- this.Yandere.transform.position);

				this.Yandere.CharacterAnimation[AnimNames.FemaleShoveA].time = 0;
				this.Yandere.CharacterAnimation.CrossFade(AnimNames.FemaleShoveA);
				this.Yandere.YandereVision = false;
				this.Yandere.NearSenpai = false;
				this.Yandere.Degloving = false;
				this.Yandere.Flicking = false;
				this.Yandere.Punching = false;
				this.Yandere.CanMove = false;
				this.Yandere.Shoved = true;
				this.Yandere.EmptyHands();

				this.Yandere.GloveTimer = 0;
				this.Yandere.h = 0;
				this.Yandere.v = 0;

				this.Yandere.ShoveSpeed = 2;
			}
		}
	}
}