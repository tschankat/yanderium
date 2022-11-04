using UnityEngine;

public class SecurityCameraScript : MonoBehaviour
{
	public SecuritySystemScript SecuritySystem;
	public MissionModeScript MissionMode;
	public YandereScript Yandere;

	public float Rotation = 0.0f;

	public int Direction = 1;

	void Update()
	{
		this.Rotation += (this.Direction * 36.0f) * Time.deltaTime;

		this.transform.parent.localEulerAngles = new Vector3(
			this.transform.parent.localEulerAngles.x,
			this.Rotation,
			this.transform.parent.localEulerAngles.z);

		if (this.Direction > 0)
		{
			if (this.Rotation > 86.50f)
			{
				this.Direction = -1;
			}
		}
		else
		{
			if (this.Rotation < -86.50f)
			{
				this.Direction = 1;
			}
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (this.MissionMode.GameOverID == 0)
		{
			if (other.gameObject.layer == 13)
			{
				if (this.Yandere.Armed &&
					this.Yandere.EquippedWeapon.Suspicious ||
					this.Yandere.Bloodiness > 0.0f &&
					!this.Yandere.RedPaint ||
					this.Yandere.Sanity < 33.333f ||
					this.Yandere.Attacking ||
					this.Yandere.Struggling ||
					this.Yandere.Dragging ||
					this.Yandere.Lewd ||
					this.Yandere.Dragging ||
					this.Yandere.Carrying ||
					this.Yandere.Laughing && this.Yandere.LaughIntensity > 15.0f ||
					this.Yandere.PickUp != null && this.Yandere.PickUp.Clothing && this.Yandere.PickUp.Evidence && !this.Yandere.PickUp.RedPaint)
				{
					if (this.Yandere.Mask == null)
					{
						if (this.MissionMode.enabled)
						{
							this.MissionMode.GameOverID = 15;
							this.MissionMode.GameOver();
							this.MissionMode.Phase = 4;
							this.enabled = false;
						}
						else
						{
							if (!this.SecuritySystem.Evidence)
							{
								this.Yandere.NotificationManager.DisplayNotification(NotificationType.Evidence);
								this.SecuritySystem.Evidence = true;
								this.SecuritySystem.Masked = false;
							}
						}
					}
					else
					{
						if (!this.SecuritySystem.Masked)
						{
							this.Yandere.NotificationManager.DisplayNotification(NotificationType.Evidence);
							this.SecuritySystem.Evidence = true;
							this.SecuritySystem.Masked = true;
						}
					}
				}
			}
			else if (other.gameObject.layer == 11)
			{
				if (this.MissionMode.enabled)
				{
					this.MissionMode.GameOverID = 15;
					this.MissionMode.GameOver();
					this.MissionMode.Phase = 4;
					this.enabled = false;
				}
			}
		}
	}
}
