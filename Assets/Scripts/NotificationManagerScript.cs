using System.Collections.Generic;
using UnityEngine;

public enum NotificationType
{
	Bloody,
	Body,
	Insane,
	Armed,
	Lewd,
	Intrude,
	Late,
	Info,
	Topic,
	Opinion,
	Complete,
	Exfiltrate,
	Evidence,
	ClassSoon,
	ClassNow,
	Eavesdropping,
	Persona,
	Clothing,
	Custom
}

public class NotificationManagerScript : MonoBehaviour
{
	public YandereScript Yandere;

	public Transform NotificationSpawnPoint;
	public Transform NotificationParent;

	public GameObject Notification;

	public int NotificationsSpawned = 0;
	public int Phase = 1;

	public ClockScript Clock;

	public string PersonaName;

    public string PreviousText;
    public string CustomText;
	public string TopicName;

	public string[] ClubNames;

	NotificationTypeAndStringDictionary NotificationMessages;

	void Awake()
	{
		// [af] Initialize "notification type -> message" associations.
		this.NotificationMessages = new NotificationTypeAndStringDictionary
		{
			{ NotificationType.Bloody, "Visibly Bloody" },
			{ NotificationType.Body, "Near Body" },
			{ NotificationType.Insane, "Visibly Insane" },
			{ NotificationType.Armed, "Visibly Armed" },
			{ NotificationType.Lewd, "Visibly Lewd" },
			{ NotificationType.Intrude, "Intruding" },
			{ NotificationType.Late, "Late For Class" },
			{ NotificationType.Info, "Learned New Info" },
			{ NotificationType.Topic, "Learned New Topic: " },
			{ NotificationType.Opinion, "Learned Opinion on: " },
			{ NotificationType.Complete, "Mission Complete" },
			{ NotificationType.Exfiltrate, "Leave School" },
			{ NotificationType.Evidence, "Evidence Recorded" },
			{ NotificationType.ClassSoon, "Class Begins Soon" },
			{ NotificationType.ClassNow, "Class Begins Now" },
			{ NotificationType.Eavesdropping, "Eavesdropping" },
			{ NotificationType.Clothing, "Cannot Attack; No Spare Clothing" },
			{ NotificationType.Persona, "Persona"},
			{ NotificationType.Custom, CustomText}
		};
	}

	void Update()
	{
		if (this.NotificationParent.localPosition.y >
			(0.0010f + (-0.049f * this.NotificationsSpawned)))
		{
			this.NotificationParent.localPosition = new Vector3(
				this.NotificationParent.localPosition.x,
				Mathf.Lerp(this.NotificationParent.localPosition.y, -0.049f * this.NotificationsSpawned, Time.deltaTime * 10.0f),
				this.NotificationParent.localPosition.z);
		}

		if (this.Phase == 1)
		{
			if (this.Clock.HourTime > 8.40f)
			{
				if (!this.Yandere.InClass)
				{
					this.Yandere.StudentManager.TutorialWindow.ShowClassMessage = true;
					this.DisplayNotification(NotificationType.ClassSoon);
				}

				this.Phase++;
			}
		}
		else if (this.Phase == 2)
		{
			if (this.Clock.HourTime > 8.50f)
			{
				if (!this.Yandere.InClass)
				{
					this.DisplayNotification(NotificationType.ClassNow);
				}

				this.Phase++;
			}
		}
		else if (this.Phase == 3)
		{
			if (this.Clock.HourTime > 13.40f)
			{
				if (!this.Yandere.InClass)
				{
					this.DisplayNotification(NotificationType.ClassSoon);
				}

				this.Phase++;
			}
		}
		else if (this.Phase == 4)
		{
			if (this.Clock.HourTime > 13.50f)
			{
				if (!this.Yandere.InClass)
				{
					this.DisplayNotification(NotificationType.ClassNow);
				}

				this.Phase++;
			}
		}
	}

	public void DisplayNotification(NotificationType Type)
	{
		if (!this.Yandere.Egg)
		{
			GameObject NewNotification = Instantiate(this.Notification);
			NotificationScript NewNotificationScript = NewNotification.GetComponent<NotificationScript>();

			NewNotification.transform.parent = this.NotificationParent;
			NewNotification.transform.localPosition = new Vector3(
				0.0f, 0.60275f + (0.049f * this.NotificationsSpawned), 0.0f);
			NewNotification.transform.localEulerAngles = Vector3.zero;

			NewNotificationScript.NotificationManager = this;

			// [af] Get the message associated with the notification type. If there is no association,
			// then it needs to be added in the initializer in Awake().
			string message;
			bool messageExists = this.NotificationMessages.TryGetValue(Type, out message);
			Debug.Assert(messageExists, "\"" + Type.ToString() + "\" missing from notification messages.");

			if (Type != NotificationType.Persona && Type != NotificationType.Custom)
			{
				string PostText = "";

				if (Type == NotificationType.Topic || Type == NotificationType.Opinion)
				{
					PostText = TopicName;
				}

				NewNotificationScript.Label.text = message + PostText;
			}
			else
			{
				if (Type == NotificationType.Custom)
				{
					NewNotificationScript.Label.text = CustomText;
				}
				else
				{
					NewNotificationScript.Label.text = PersonaName + " " + message;
				}
			}

			this.NotificationsSpawned++;

			NewNotificationScript.ID = this.NotificationsSpawned;

            PreviousText = CustomText;
		}
	}
}
