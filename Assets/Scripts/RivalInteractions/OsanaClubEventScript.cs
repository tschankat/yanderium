using System;
using UnityEngine;

public class OsanaClubEventScript : MonoBehaviour
{
#if UNITY_EDITOR
	public StudentManagerScript StudentManager;
	public UILabel EventSubtitle;
	public YandereScript Yandere;
	public JukeboxScript Jukebox;
	public ClockScript Clock;

	public StudentScript[] EventStudent;
	public Transform[] EventLocation;
	public AudioClip[] EventClip;
	public string[] EventSpeech;
	public string[] EventAnim;
	public int[] EventSpeaker;
	public int[] ClubIDs;

	public GameObject VoiceClip;

	public bool EventOn = false;
	public bool Spoken = false;

	public int EventPhase = 0;

	public float Timer = 0.0f;
	public float Scale = 0.0f;

	public int[] StudentID;

	public DayOfWeek EventDay;

	void Start()
	{
		if (DateGlobals.Weekday != EventDay)
		{
			enabled = false;
		}
	}

	void Update()
	{
		if (!EventOn)
		{
			for (int i = 1; i < 3; i++)
			{
				if (EventStudent[i] == null)
				{
					EventStudent[i] = StudentManager.Students[StudentID[i]];
				}
				else
				{
					if (!EventStudent[i].Alive || EventStudent[i].Slave)
					{
						enabled = false;
					}
				}
			}

			if (EventStudent[1] != null && EventStudent[2] != null)
			{
				if (EventStudent[1].Pathfinding.canMove &&
					EventStudent[2].Pathfinding.canMove &&
					EventStudent[1].Routine && !EventStudent[1].Wet)
				{
					EventStudent[1].CharacterAnimation.CrossFade(EventStudent[1].WalkAnim);
					EventStudent[1].CurrentDestination = EventLocation[1];
					EventStudent[1].Pathfinding.target = EventLocation[1];
					EventStudent[1].TargetDistance = .5f;
					EventStudent[1].Private = false;
					EventStudent[1].InEvent = true;

					EventStudent[2].CharacterAnimation.CrossFade(EventStudent[2].WalkAnim);
					EventStudent[2].CurrentDestination = EventLocation[2];
					EventStudent[2].Pathfinding.target = EventLocation[2];
					EventStudent[2].TargetDistance = 1;
					EventStudent[2].Private = false;
					EventStudent[2].InEvent = true;

					EventOn = true;
				}
			}
		}
		//If the event is on...
		else
		{
			Vector3 Midpoint = ((EventStudent[1].transform.position - EventStudent[2].transform.position) * 0.5f) + EventStudent[2].transform.position; 

			float Distance = Vector3.Distance(Yandere.transform.position, Midpoint);

			if (this.EventPhase > 0 && this.EventPhase < 7)
			{
				this.Yandere.Eavesdropping = Distance < 3.0f;
			}
			else
			{
				this.Yandere.Eavesdropping = false;
			}

			if ((Clock.HourTime > 13.50f) ||
				EventStudent[1].WitnessedCorpse ||
				EventStudent[2].WitnessedCorpse ||
				EventStudent[1].Alarmed ||
				EventStudent[2].Alarmed ||
				EventStudent[1].Dying ||
				EventStudent[2].Dying)
			{
				EndEvent();
			}
			else
			{
				for (int i = 1; i < 3; i++)
				{
					if (!EventStudent[i].Pathfinding.canMove && !EventStudent[i].Private)
					{
						EventStudent[i].CharacterAnimation.CrossFade(EventStudent[i].IdleAnim);
						EventStudent[i].Private = true;

						StudentManager.UpdateStudents();
					}
				}
					
				if (!EventStudent[1].Pathfinding.canMove && !EventStudent[2].Pathfinding.canMove)
				{
					if (!Spoken)
					{
						EventStudent[EventSpeaker[1]].CharacterAnimation.CrossFade(EventStudent[1].IdleAnim);
						EventStudent[EventSpeaker[2]].CharacterAnimation.CrossFade(EventStudent[2].IdleAnim);

						EventStudent[EventSpeaker[EventPhase]].PickRandomAnim();
						EventStudent[EventSpeaker[EventPhase]].CharacterAnimation.CrossFade(EventStudent[EventSpeaker[EventPhase]].RandomAnim);

						AudioClipPlayer.Play(EventClip[EventPhase],
							EventStudent[EventSpeaker[EventPhase]].transform.position + (Vector3.up * 1.50f),
							5.0f, 10.0f, out VoiceClip, Yandere.transform.position.y);

						Spoken = true;

						if (EventSpeaker[EventPhase] == 1 && EventPhase > 7 && EventPhase < 33 && EventPhase != 24)
						{
							if (Distance < 10)
							{
								if (EventPhase == 30)
								{
									Debug.Log("Current EventPhase is: 30 and Osana is talking about the delinquents.");

									this.Yandere.NotificationManager.TopicName = "Violence";
									this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
									ConversationGlobals.SetTopicLearnedByStudent(ClubIDs[EventPhase], 17, true);
								}
								else
								{
									Debug.Log("Current EventPhase is: " + this.EventPhase + " and ClubID is: " + ClubIDs[EventPhase]);

									this.Yandere.NotificationManager.TopicName = this.Yandere.NotificationManager.ClubNames[ClubIDs[EventPhase]];
									this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
									ConversationGlobals.SetTopicLearnedByStudent(ClubIDs[EventPhase], 11, true);
								}
							}
						}
					}
					else
					{
						int Speaker = EventSpeaker[EventPhase];

						if (Speaker == 1){EventStudent[2].CharacterAnimation.CrossFade(EventStudent[2].IdleAnim);}
						else{EventStudent[1].CharacterAnimation.CrossFade(EventStudent[2].IdleAnim);}

						if (EventStudent[Speaker].CharacterAnimation[EventStudent[Speaker].RandomAnim].time >=
							EventStudent[Speaker].CharacterAnimation[EventStudent[Speaker].RandomAnim].length)
						{
							EventStudent[Speaker].PickRandomAnim();
							EventStudent[Speaker].CharacterAnimation.CrossFade(EventStudent[Speaker].RandomAnim);
						}

						Timer += Time.deltaTime;

						if (Yandere.transform.position.y > EventStudent[1].transform.position.y - 1 &&
							Yandere.transform.position.y < EventStudent[1].transform.position.y + 1)
						{
							if (VoiceClip != null)
							{
								VoiceClip.GetComponent<AudioSource>().volume = 1;
							}
								
							if (Distance < 10.0f)
							{
								if (Timer > EventClip[EventPhase].length)
								{
									EventSubtitle.text = string.Empty;
								}
								else
								{
									EventSubtitle.text = EventSpeech[EventPhase];
								}

								Scale = Mathf.Abs((Distance - 10.0f) * 0.20f);

								if (Scale < 0.0f)
								{
									Scale = 0.0f;
								}

								if (Scale > 1.0f)
								{
									Scale = 1.0f;
								}

								this.Jukebox.Dip = 1 - (.5f * Scale);

								EventSubtitle.transform.localScale =
									new Vector3(Scale, Scale, Scale);
							}
							else
							{
								EventSubtitle.transform.localScale = Vector3.zero;
								EventSubtitle.text = string.Empty;
							}
						}
						else
						{
							if (VoiceClip != null)
							{
								VoiceClip.GetComponent<AudioSource>().volume = 0;
							}
						}

						if (Timer > (EventClip[EventPhase].length + .5f))
						{
							Spoken = false;
							EventPhase++;
							Timer = 0.0f;

							if (EventPhase == EventSpeech.Length)
							{
								EndEvent();
							}
							else if (EventPhase > 6)
							{
								EventStudent[1].CurrentDestination = EventLocation[EventPhase];
								EventStudent[1].Pathfinding.target = EventLocation[EventPhase];

								EventStudent[2].CurrentDestination = EventStudent[1].transform;
								EventStudent[2].Pathfinding.target = EventStudent[1].transform;
							}
						}
					}
				}
				else
				{
					//Debug.Log("One or both characters have not yet reached their destination.");

					if (!EventStudent[1].Pathfinding.canMove)
					{
						EventStudent[1].CharacterAnimation.CrossFade(EventStudent[1].IdleAnim);
					}
					else
					{
						EventStudent[1].CharacterAnimation.CrossFade(EventStudent[1].WalkAnim);
					}

					if (!EventStudent[2].Pathfinding.canMove)
					{
						EventStudent[2].CharacterAnimation.CrossFade(EventStudent[2].IdleAnim);

						if (EventPhase == 1)
						{
							SettleFriend();
						}
					}
					else
					{
						if (EventStudent[2].Pathfinding.speed == 1)
						{
							EventStudent[2].CharacterAnimation.CrossFade(EventStudent[2].WalkAnim);
						}
						else
						{
							EventStudent[2].CharacterAnimation.CrossFade(EventStudent[2].RunAnim);
						}
					}
				}
			}
		}
	}

	void SettleFriend()
	{
		EventStudent[2].MoveTowardsTarget(EventStudent[2].Pathfinding.target.position);
	}

#endif

	public void EndEvent()
	{

#if UNITY_EDITOR

		Debug.Log("Ending Osana's club event.");

		if (VoiceClip != null)
		{
			Destroy(VoiceClip);
		}

		for (int i = 1; i < 3; i++)
		{
			EventStudent[i].CurrentDestination =
				EventStudent[i].Destinations[EventStudent[i].Phase];
			EventStudent[i].Pathfinding.target =
				EventStudent[i].Destinations[EventStudent[i].Phase];

			EventStudent[i].InEvent = false;
			EventStudent[i].Private = false;
		}

		if (!StudentManager.Stop)
		{
			StudentManager.UpdateStudents();
		}

		this.Yandere.Eavesdropping = false;

		this.Jukebox.Dip = 1;
					
		EventSubtitle.text = string.Empty;
		enabled = false;

#endif

	}
}