using System;
using UnityEngine;

public class PhoneEventScript : MonoBehaviour
{
	public OsanaClubEventScript OsanaClubEvent;

	public StudentManagerScript StudentManager;
	public BucketPourScript DumpPoint;
	public YandereScript Yandere;
	public JukeboxScript Jukebox;
	public ClockScript Clock;

	public StudentScript EventStudent;
	public StudentScript EventFriend;
	public UILabel EventSubtitle;

	public Transform EventLocation;
	public Transform SpyLocation;

	public AudioClip[] EventClip;
	public string[] EventSpeech;
	public float[] SpeechTimes;
	public string[] EventAnim;

	public GameObject VoiceClip;

	public bool EventActive = false;
	public bool EventCheck = false;
	public bool EventOver = false;

	public int EventStudentID = 7;
	public int EventFriendID = 34;
	public float EventTime = 7.5f;
	public int EventPhase = 1;
	public DayOfWeek EventDay = DayOfWeek.Monday;

	public float CurrentClipLength = 0.0f;
	public float FailSafe = 0.0f;
	public float Timer = 0.0f;

	void Start()
	{
		this.EventSubtitle.transform.localScale = Vector3.zero;

		if (DateGlobals.Weekday == this.EventDay)
		{
			this.EventCheck = true;
		}

		if (HomeGlobals.LateForSchool || StudentManager.YandereLate)
		{
			this.enabled = false;
		}

		#if !UNITY_EDITOR

		//Osana
		if (EventStudentID == 11)
		{
			this.enabled = false;
		}

		#endif
	}

	void OnAwake()
	{
		#if !UNITY_EDITOR

		//Osana
		if (EventStudentID == 11)
		{
			this.enabled = false;
		}

		#endif
	}

	void Update()
	{
		if (!this.Clock.StopTime)
		{
			if (this.EventCheck)
			{
				if (this.Clock.HourTime > (this.EventTime + 0.50f))
				{
					this.enabled = false;
				}
				else
				{
					if (this.Clock.HourTime > this.EventTime)
					{
						this.EventStudent = this.StudentManager.Students[this.EventStudentID];

						if (this.EventStudent != null && !this.EventStudent.InEvent && this.EventStudent.DistanceToDestination < 1)
						{
							if (!this.StudentManager.CommunalLocker.RivalPhone.Stolen)
							{
								this.EventStudent.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;

								//Osana
								if (this.EventStudentID == 11)
								{
									this.EventFriend = this.StudentManager.Students[this.EventFriendID];

									if (this.EventFriend != null)
									{
										this.EventFriend.CharacterAnimation.CrossFade(this.EventFriend.IdleAnim);

										this.EventFriend.Pathfinding.canSearch = false;
										this.EventFriend.Pathfinding.canMove = false;

										this.EventFriend.TargetDistance = .5f;
										this.EventFriend.SpeechLines.Stop();
										this.EventFriend.PhoneEvent = this;
										this.EventFriend.CanTalk = false;
										this.EventFriend.Routine = false;
										this.EventFriend.InEvent = true;
										//this.EventFriend.Private = true;
										this.EventFriend.Prompt.Hide();
									}
								}

								if (this.EventStudent.Routine &&
									!this.EventStudent.Distracted &&
									!this.EventStudent.Talking &&
									!this.EventStudent.Meeting &&
									!this.EventStudent.Investigating &&
									this.EventStudent.Indoors)
								{
									if (!this.EventStudent.WitnessedMurder)
									{
										this.EventStudent.CurrentDestination =
											this.EventStudent.Destinations[this.EventStudent.Phase];
										this.EventStudent.Pathfinding.target =
											this.EventStudent.Destinations[this.EventStudent.Phase];

										this.EventStudent.Obstacle.checkTime = 99.0f;
										this.EventStudent.SpeechLines.Stop();
										this.EventStudent.PhoneEvent = this;
										this.EventStudent.CanTalk = false;
										this.EventStudent.InEvent = true;
										//this.EventStudent.Private = true;
										this.EventStudent.Prompt.Hide();

										this.EventCheck = false;
										this.EventActive = true;

										if (this.EventStudent.Following)
										{
											this.EventStudent.Pathfinding.canMove = true;
											this.EventStudent.Pathfinding.speed = 1.0f;
											this.EventStudent.Following = false;
											this.EventStudent.Routine = true;

                                            this.Yandere.Follower = null;
											this.Yandere.Followers--;

											this.EventStudent.Subtitle.UpdateLabel(SubtitleType.StopFollowApology, 0, 3.0f);
											this.EventStudent.Prompt.Label[0].text = "     " + "Talk";
										}
									}
									else
									{
										this.enabled = false;
									}
								}
							}
						}
					}
				}
			}
		}

		if (this.EventActive)
		{
			// [af] Commented in JS code.
			//FailSafe += Time.deltaTime;

			//if (FailSafe > 1)
			//{

			if (this.EventStudent.DistanceToDestination < 0.50f)
			{
				this.EventStudent.Pathfinding.canSearch = false;
				this.EventStudent.Pathfinding.canMove = false;
			}

			if ((this.Clock.HourTime > (this.EventTime + 0.50f)) ||
				this.EventStudent.WitnessedMurder ||
				this.EventStudent.Splashed ||
				this.EventStudent.Alarmed ||
				this.EventStudent.Dying ||
				!this.EventStudent.Alive)
			{
				this.EndEvent();
			}
			else
			{
				if (!this.EventStudent.Pathfinding.canMove)
				{
					if (this.EventPhase == 1)
					{
						this.Timer += Time.deltaTime;

						this.EventStudent.CharacterAnimation.CrossFade(this.EventAnim[0]);

						AudioClipPlayer.Play(this.EventClip[0],
							this.EventStudent.transform.position,
							5.0f, 10.0f, out this.VoiceClip, out this.CurrentClipLength);

						this.EventPhase++;
					}
					else if (this.EventPhase == 2)
					{
						this.Timer += Time.deltaTime;

						if (this.Timer > 1.50f)
						{
							this.EventStudent.SmartPhone.SetActive(true);
							this.EventStudent.SmartPhone.transform.localPosition = new Vector3(-.015f, -.005f, -.015f);
							this.EventStudent.SmartPhone.transform.localEulerAngles = new Vector3(0, -150, 165);
						}

						//The amount of time that Osana's friend waits before following after her.
						if (this.Timer > 2.0f)
						{
							// [af] Commented in JS code.
							//EventStudent.Character.animation.CrossFade(EventAnim[1]);

							AudioClipPlayer.Play(this.EventClip[1],
								this.EventStudent.transform.position,
								5.0f, 10.0f, out this.VoiceClip, out this.CurrentClipLength);

							this.EventSubtitle.text = this.EventSpeech[1];

							this.Timer = 0.0f;

							this.EventPhase++;
						}
					}
					else if (this.EventPhase == 3)
					{
						this.Timer += Time.deltaTime;

						if (this.Timer > this.CurrentClipLength)
						{
							this.EventStudent.Character.GetComponent<Animation>().CrossFade(this.EventStudent.RunAnim);
							this.EventStudent.CurrentDestination = this.EventLocation;
							this.EventStudent.Pathfinding.target = this.EventLocation;
							this.EventStudent.Pathfinding.canSearch = true;
							this.EventStudent.Pathfinding.canMove = true;
							this.EventStudent.Pathfinding.speed = 4.0f;
                            this.EventSubtitle.text = string.Empty;
                            this.EventStudent.Hurry = true;

                            Debug.Log(this.EventStudent.Name + " has been given a pathfinding speed of 4.");

							this.Timer = 0.0f;

							this.EventPhase++;
						}
					}
					else if (this.EventPhase == 4)
					{
						if (this.EventStudentID != 11)
						{
							this.DumpPoint.enabled = true;
						}

						this.EventStudent.Private = true;

						this.EventStudent.Character.GetComponent<Animation>().CrossFade(this.EventAnim[2]);

						AudioClipPlayer.Play(this.EventClip[2],
							this.EventStudent.transform.position,
							5.0f, 10.0f, out this.VoiceClip, out this.CurrentClipLength);

						this.EventPhase++;
					}
					else if (this.EventPhase < 13)
					{
						if (this.VoiceClip != null)
						{
							this.VoiceClip.GetComponent<AudioSource>().pitch = Time.timeScale;

							this.EventStudent.Character.GetComponent<Animation>()[this.EventAnim[2]].time =
								this.VoiceClip.GetComponent<AudioSource>().time;

							if (this.VoiceClip.GetComponent<AudioSource>().time >
								this.SpeechTimes[this.EventPhase - 3])
							{
								this.EventSubtitle.text = this.EventSpeech[this.EventPhase - 3];
								this.EventPhase++;
							}
						}
					}
					else
					{
						if (this.EventStudent.Character.GetComponent<Animation>()[this.EventAnim[2]].time >=
							(this.EventStudent.Character.GetComponent<Animation>()[this.EventAnim[2]].length * 90.33333f))
						{
							this.EventStudent.SmartPhone.SetActive(true);
						}

						if (this.EventStudent.Character.GetComponent<Animation>()[this.EventAnim[2]].time >=
							this.EventStudent.Character.GetComponent<Animation>()[this.EventAnim[2]].length)
						{
							this.EndEvent();
						}
					}

					float Distance = Vector3.Distance(this.Yandere.transform.position,
						this.EventStudent.transform.position);

					//Debug.Log("Distance is: " + Distance);

					if (Distance < 10.0f)
					{
						float Scale = Mathf.Abs((Distance - 10.0f) * 0.20f);

						//Debug.Log("Scale is: " + Scale);

						if (Scale < 0.0f)
						{
							Scale = 0.0f;
						}

						if (Scale > 1.0f)
						{
							Scale = 1.0f;
						}

						this.Jukebox.Dip = 1 - (.5f * Scale);

						this.EventSubtitle.transform.localScale = new Vector3(Scale, Scale, Scale);
					}
					else
					{
						this.EventSubtitle.transform.localScale = Vector3.zero;
					}

					if (this.enabled)
					{
						if (this.EventPhase > 4)
						{
							if (Distance < 5)
							{
								Yandere.Eavesdropping = true;
							}
							else
							{
								Yandere.Eavesdropping = false;
							}
						}
					}

					if ((this.EventPhase == 11) && (Distance < 5.0f))
					{
						if (this.EventStudentID == 30)
						{
							if (!EventGlobals.Event2)
							{
								EventGlobals.Event2 = true;
								this.Yandere.NotificationManager.DisplayNotification(NotificationType.Info);

								//Learning about money.
								ConversationGlobals.SetTopicDiscovered(25, true);
								this.Yandere.NotificationManager.TopicName = "Money";
								this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);

								//Kokona values money.
								this.Yandere.NotificationManager.TopicName = "Money";
								this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
								ConversationGlobals.SetTopicLearnedByStudent(25, this.EventStudentID, true);
							}
						}
						else
						{
							if (!EventGlobals.OsanaEvent1)
							{
								EventGlobals.OsanaEvent1 = true;
								this.Yandere.NotificationManager.DisplayNotification(NotificationType.Info);
							}
						}
					}
				}
                else
                {
                    this.EventStudent.Character.GetComponent<Animation>().CrossFade(this.EventStudent.RunAnim);
                    this.EventStudent.Pathfinding.speed = 4.0f;
                }

				if (this.EventStudent.Pathfinding.canMove || this.EventPhase > 3)
				{
					if (this.EventFriend != null)
					{
						if (this.EventPhase > 3)
						{
							if (this.EventFriend.CurrentDestination != this.SpyLocation)
							{
								this.Timer += Time.deltaTime;

								if (this.Timer > 3)
								{
									this.EventFriend.CharacterAnimation.CrossFade(this.EventStudent.RunAnim);
									this.EventFriend.CurrentDestination = this.SpyLocation;
									this.EventFriend.Pathfinding.target = this.SpyLocation;
									this.EventFriend.Pathfinding.canSearch = true;
									this.EventFriend.Pathfinding.canMove = true;
									this.EventFriend.Pathfinding.speed = 4.0f;
									this.EventFriend.Routine = true;

									this.Timer = 0.0f;
								}
								else
								{
									this.EventFriend.targetRotation = Quaternion.LookRotation(
										this.StudentManager.Students[this.EventStudentID].transform.position - this.EventFriend.transform.position);
									this.EventFriend.transform.rotation = Quaternion.Slerp(
										this.EventFriend.transform.rotation, this.EventFriend.targetRotation, 10.0f * Time.deltaTime);
								}
							}
							else
							{
								//Debug.Log("Friend is heading to destination.");

								if (this.EventFriend.DistanceToDestination < 1)
								{
									//this.EventFriend.CharacterAnimation.CrossFade(this.EventStudent.RunAnim);
									this.EventFriend.CharacterAnimation.CrossFade(AnimNames.FemaleCornerPeek);
									this.EventFriend.Pathfinding.canSearch = false;
									this.EventFriend.Pathfinding.canMove = false;

									this.SettleFriend();
								}
							}
						}
					}
				}
			}
			//}
		}
	}

	void SettleFriend()
	{
		this.EventFriend.MoveTowardsTarget(this.SpyLocation.position);

		float Angle = Quaternion.Angle(this.EventFriend.transform.rotation, this.SpyLocation.rotation);
		
		if (Angle > 1.0f)
		{
			this.EventFriend.transform.rotation = Quaternion.Slerp(
				this.EventFriend.transform.rotation, this.SpyLocation.rotation, 10.0f * Time.deltaTime);
		}
	}

	void EndEvent()
	{
		Debug.Log("A phone event ended.");

		if (!this.EventOver)
		{
			this.EventStudent.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;

			if (this.VoiceClip != null)
			{
				Destroy(this.VoiceClip);
			}

			if (this.EventFriend != null)
			{
				Debug.Log("Osana's friend is exiting the phone event.");

				this.EventFriend.CurrentDestination =
					this.EventFriend.Destinations[this.EventFriend.Phase];

				this.EventFriend.Pathfinding.target =
					this.EventFriend.Destinations[this.EventFriend.Phase];

				this.EventFriend.Obstacle.checkTime = 1.0f;
				this.EventFriend.Pathfinding.speed = 1.0f;
				this.EventFriend.TargetDistance = 1.0f;
				this.EventFriend.InEvent = false;
				this.EventFriend.Private = false;
				this.EventFriend.Routine = true;
				this.EventFriend.CanTalk = true;

				this.OsanaClubEvent.enabled = true;
			}

			this.EventStudent.CurrentDestination =
				this.EventStudent.Destinations[this.EventStudent.Phase];

			this.EventStudent.Pathfinding.target =
				this.EventStudent.Destinations[this.EventStudent.Phase];

			this.EventStudent.Obstacle.checkTime = 1.0f;

			if (!this.EventStudent.Dying)
			{
				this.EventStudent.Prompt.enabled = true;
			}

			if (!this.EventStudent.WitnessedMurder)
			{
				this.EventStudent.SmartPhone.SetActive(false);
			}

			this.EventStudent.Pathfinding.speed = 1.0f;
			this.EventStudent.TargetDistance = 1.0f;
			this.EventStudent.PhoneEvent = null;
			this.EventStudent.InEvent = false;
			this.EventStudent.Private = false;
			this.EventStudent.CanTalk = true;

			this.EventSubtitle.text = string.Empty;

			this.StudentManager.UpdateStudents();

			//this.DumpPoint.enabled = false;
			//this.DumpPoint.Prompt.Hide();
			//this.DumpPoint.Prompt.enabled = false;
		}

        this.EventStudent.Hurry = false;

        this.Yandere.Eavesdropping = false;

		this.Jukebox.Dip = 1;

		this.EventActive = false;
		this.EventCheck = false;
		this.enabled = false;
	}
}
