using UnityEngine;

public class InterestManagerScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public YandereScript Yandere;

	public Transform[] Clubs;

	public Transform DelinquentZone;
	public Transform Library;
	public Transform Kitten;

	public string[] TopicNames;
    public bool[] Ignore;

    public int FollowerID = 0;

    void Start()
	{
		//ConversationGlobals.SetTopicDiscovered(23, true);
	}

	void Update()
	{
		if (this.Yandere.Follower != null)
		{
			int ID = 1;

			while (ID < 11)
			{
                //Debug.Log("Distance to " + TopicNames[ID] + " Club is: " + Vector3.Distance(this.Yandere.Follower.transform.position, this.Clubs[ID].position));

                if (!Ignore[ID] && Vector3.Distance(this.Yandere.Follower.transform.position, this.Clubs[ID].position) < 4.0f)
                {
                    // Club Opinions
                    if (!ConversationGlobals.GetTopicLearnedByStudent(ID, FollowerID))
				    {
						this.Yandere.NotificationManager.TopicName = TopicNames[ID];

						if (!ConversationGlobals.GetTopicDiscovered(ID))
						{
							this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
							ConversationGlobals.SetTopicDiscovered(ID, true);
						}

						this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
						ConversationGlobals.SetTopicLearnedByStudent(ID, FollowerID, true);

                        Ignore[ID] = true;
					}
				}

				ID++;
			}

            // Games, Anime, Cosplay, Memes
            if (!Ignore[11] && Vector3.Distance(this.Yandere.Follower.transform.position, this.Clubs[11].position) < 4.0f)
            {
                if (!ConversationGlobals.GetTopicLearnedByStudent(11, FollowerID))
			    {
					if (!ConversationGlobals.GetTopicDiscovered(11))
					{
						this.Yandere.NotificationManager.TopicName = "Video Games";
						this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);

						this.Yandere.NotificationManager.TopicName = "Anime";
						this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);

						this.Yandere.NotificationManager.TopicName = "Cosplay";
						this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);

						this.Yandere.NotificationManager.TopicName = "Memes";
						this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);

						ConversationGlobals.SetTopicDiscovered(11, true);
						ConversationGlobals.SetTopicDiscovered(12, true);
						ConversationGlobals.SetTopicDiscovered(13, true);
						ConversationGlobals.SetTopicDiscovered(14, true);
					}

					this.Yandere.NotificationManager.TopicName = "Video Games";
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);

					this.Yandere.NotificationManager.TopicName = "Anime";
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);

					this.Yandere.NotificationManager.TopicName = "Cosplay";
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);

					this.Yandere.NotificationManager.TopicName = "Memes";
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);

					ConversationGlobals.SetTopicLearnedByStudent(11, FollowerID, true);
					ConversationGlobals.SetTopicLearnedByStudent(12, FollowerID, true);
					ConversationGlobals.SetTopicLearnedByStudent(13, FollowerID, true);
					ConversationGlobals.SetTopicLearnedByStudent(14, FollowerID, true);

                    Ignore[11] = true;
				}
			}

            // Cats
            if (!Ignore[15] && Vector3.Distance(this.Yandere.Follower.transform.position, this.Kitten.position) < 2.5f)
            {
                if (!ConversationGlobals.GetTopicLearnedByStudent(15, FollowerID))
		    	{
					this.Yandere.NotificationManager.TopicName = "Cats";

					if (!ConversationGlobals.GetTopicDiscovered(15))
					{
						this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
						ConversationGlobals.SetTopicDiscovered(15, true);
					}

					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
					ConversationGlobals.SetTopicLearnedByStudent(15, FollowerID, true);

                    Ignore[15] = true;
                }
			}

            // Justice
            if (!Ignore[16] && Vector3.Distance(this.Yandere.Follower.transform.position, this.Clubs[6].position) < 4.0f)
            {
                if (!ConversationGlobals.GetTopicLearnedByStudent(16, FollowerID))
    			{
					this.Yandere.NotificationManager.TopicName = "Justice";

					if (!ConversationGlobals.GetTopicDiscovered(16))
					{
						this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
						ConversationGlobals.SetTopicDiscovered(16, true);
					}

					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
					ConversationGlobals.SetTopicLearnedByStudent(16, FollowerID, true);

                    Ignore[16] = true;
                }
			}

            // Violence
            if (!Ignore[17] && Vector3.Distance(this.Yandere.Follower.transform.position, this.DelinquentZone.position) < 4.0f)
            {
                if (!ConversationGlobals.GetTopicLearnedByStudent(17, FollowerID))
    			{
					this.Yandere.NotificationManager.TopicName = "Violence";

					if (!ConversationGlobals.GetTopicDiscovered(17))
					{
						this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
						ConversationGlobals.SetTopicDiscovered(17, true);
					}

					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
					ConversationGlobals.SetTopicLearnedByStudent(17, FollowerID, true);

                    Ignore[17] = true;
                }
			}

            // Reading
            if (!Ignore[18] && Vector3.Distance(this.Yandere.Follower.transform.position, this.Library.position) < 4.0f)
            {
                if (!ConversationGlobals.GetTopicLearnedByStudent(18, FollowerID))
				{
					this.Yandere.NotificationManager.TopicName = "Reading";

					if (!ConversationGlobals.GetTopicDiscovered(18))
					{
						this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
						ConversationGlobals.SetTopicDiscovered(18, true);
					}

					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
					ConversationGlobals.SetTopicLearnedByStudent(18, FollowerID, true);

                    Ignore[18] = true;
                }
			}
		}
	}

    public void UpdateIgnore()
    {
        int ID = 1;

        while (ID < 26)
        {
            Ignore[ID] = false;
            ID++;
        }

        int FollowerID = this.Yandere.Follower.StudentID;

        ID = 1;

        while (ID < 26)
        {
            if (ConversationGlobals.GetTopicLearnedByStudent(ID, FollowerID))
            {
                Debug.Log(this.Yandere.Follower + "'s opinion on " + TopicNames[ID] + " is: " + ConversationGlobals.GetTopicLearnedByStudent(ID, FollowerID));

                Ignore[ID] = true;
            }

            ID++;
        }
    }
}