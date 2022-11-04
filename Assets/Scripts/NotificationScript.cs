using UnityEngine;

public class NotificationScript : MonoBehaviour
{
	public NotificationManagerScript NotificationManager;

	public UISprite[] Icon;

	public UIPanel Panel;

	public UILabel Label;

	public bool Display = false;

	public float Timer = 0.0f;

	public int ID = 0;

	void Start()
	{
		if (MissionModeGlobals.MissionMode)
		{
			this.Icon[0].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			this.Icon[1].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			this.Label.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		}
	}

	void Update()
	{
		if (!this.Display)
		{
			// [af] Replaced if/else statement with ternary expression.
			this.Panel.alpha -= Time.deltaTime *
				((this.NotificationManager.NotificationsSpawned > (this.ID + 2)) ? 3.0f : 1.0f);

			if (this.Panel.alpha <= 0.0f)
			{
				Destroy(this.gameObject);
			}
		}
		else
		{
			this.Timer += Time.deltaTime;

			if (this.Timer > 4.0f)
			{
				this.Display = false;
			}

			if (this.NotificationManager.NotificationsSpawned > (this.ID + 2))
			{
				this.Display = false;
			}
		}
	}
}
