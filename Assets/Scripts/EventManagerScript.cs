using System;
using UnityEngine;

public class EventManagerScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public NoteLockerScript NoteLocker;
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

	public GameObject VoiceClip;

	public bool StopWalking = false;
	public bool EventCheck = false;
	public bool EventOn = false;
	public bool Suitor = false;
	public bool Spoken = false;
	public bool Osana = false;

	public float StartTimer = 0.0f;
	public float Timer = 0.0f;
	public float Scale = 0.0f;

	public float StartTime = 13.01f;
	public float EndTime = 13.50f;

	public int EventStudent1;
	public int EventStudent2;

	public int EventPhase = 0;
	public int OsanaID = 1;

	void Start()
	{
		this.EventSubtitle.transform.localScale = Vector3.zero;

		if (DateGlobals.Weekday == DayOfWeek.Monday)
		{
			this.EventCheck = true;
		}

		if (this.OsanaID == 3)
		{
			if (DateGlobals.Weekday != DayOfWeek.Thursday)
			{
				enabled = false;
			}
			else
			{
				this.EventCheck = true;
			}
		}

		this.NoteLocker.Prompt.enabled = true;
		this.NoteLocker.CanLeaveNote = true;

		#if !UNITY_EDITOR
		if (this.EventStudent1 == 11)
		{
			Destroy(this);
		}
		#endif
	}

	void Update()
	{
		if (!this.Clock.StopTime)
		{
			if (this.EventCheck)
			{
				if (this.Clock.HourTime > this.StartTime)
				{
					if (this.EventStudent[1] == null)
					{
						this.EventStudent[1] = this.StudentManager.Students[EventStudent1];
					}
					else
					{
						if (!this.EventStudent[1].Alive)
						{
							this.EventCheck = false;
							this.enabled = false;
						}
					}

					if (this.EventStudent[2] == null)
					{
						this.EventStudent[2] = this.StudentManager.Students[EventStudent2];
					}
					else
					{
						if (!this.EventStudent[2].Alive)
						{
							this.EventCheck = false;
							this.enabled = false;
						}
					}

					if (this.EventStudent[1] != null && this.EventStudent[2] != null)
					{
						if (!this.EventStudent[1].Slave && !this.EventStudent[2].Slave &&
							this.EventStudent[1].Indoors && !this.EventStudent[1].Wet &&
							!this.EventStudent[1].Meeting)
						{
							if (OsanaID < 2 || OsanaID > 1 &&
								Vector3.Distance(this.EventStudent[1].transform.position, this.EventLocation[1].position) < 1)
							{
								StartTimer += Time.deltaTime;

								if (StartTimer > 1)
								{
									if (this.EventStudent[1].Routine &&
										this.EventStudent[2].Routine &&
										!this.EventStudent[1].InEvent &&
										!this.EventStudent[2].InEvent)
									{
										this.EventStudent[1].CurrentDestination = this.EventLocation[1];
										this.EventStudent[1].Pathfinding.target = this.EventLocation[1];
										this.EventStudent[1].EventManager = this;
										this.EventStudent[1].InEvent = true;
										this.EventStudent[1].EmptyHands();

										if (!this.Osana)
										{
											this.EventStudent[2].CurrentDestination = this.EventLocation[2];
											this.EventStudent[2].Pathfinding.target = this.EventLocation[2];
											this.EventStudent[2].EventManager = this;
											this.EventStudent[2].InEvent = true;
										}
										else
										{
											Debug.Log("One of Osana's ''talk privately with Raibaru'' events is beginning.");
										}

										this.EventStudent[2].EmptyHands();

										this.EventStudent[1].SpeechLines.Stop();
										this.EventStudent[2].SpeechLines.Stop();

										this.EventCheck = false;
										this.EventOn = true;
									}
								}
							}
						}
					}
				}
			}
		}

		if (this.EventOn)
		{
			float Distance = Vector3.Distance(this.Yandere.transform.position,
				this.EventStudent[this.EventSpeaker[this.EventPhase]].transform.position);

			if ((this.Clock.HourTime > this.EndTime) ||
				this.EventStudent[1].WitnessedCorpse ||
				this.EventStudent[2].WitnessedCorpse ||
				this.EventStudent[1].Dying ||
				this.EventStudent[2].Dying ||
				this.EventStudent[1].Splashed ||
				this.EventStudent[2].Splashed ||
				this.EventStudent[1].Alarmed ||
				this.EventStudent[2].Alarmed)
			{
				this.EndEvent();
			}
			else
			{
				if (this.Osana)
				{
					if (this.EventStudent[1].DistanceToDestination < 1)
					{
						this.EventStudent[2].CurrentDestination = this.EventLocation[2];
						this.EventStudent[2].Pathfinding.target = this.EventLocation[2];
						this.EventStudent[2].EventManager = this;
						this.EventStudent[2].InEvent = true;
					}
				}

				if (!this.EventStudent[1].Pathfinding.canMove && !this.EventStudent[1].Private)
				{
					this.EventStudent[1].CharacterAnimation.CrossFade(this.EventStudent[1].IdleAnim);
					this.EventStudent[1].Private = true;

					this.StudentManager.UpdateStudents();
				}

				if (Vector3.Distance(this.EventStudent[2].transform.position, EventLocation[2].position) < 1)
				{
					if (!this.EventStudent[2].Pathfinding.canMove && !this.StopWalking)
					{
						this.StopWalking = true;

						this.EventStudent[2].CharacterAnimation.CrossFade(this.EventStudent[2].IdleAnim);
						this.EventStudent[2].Private = true;

						this.StudentManager.UpdateStudents();
					}
				}

				if (this.StopWalking && this.EventPhase == 1)
				{
					this.EventStudent[2].CharacterAnimation.CrossFade(this.EventStudent[2].IdleAnim);
				}

				if (Vector3.Distance(this.EventStudent[1].transform.position, EventLocation[1].position) < 1)
				{
					if (!this.EventStudent[1].Pathfinding.canMove && !this.EventStudent[2].Pathfinding.canMove)
					{
						if (this.EventPhase == 1)
						{
							this.EventStudent[1].CharacterAnimation.CrossFade(this.EventStudent[1].IdleAnim);
						}

						if (this.Osana)
						{
							SettleFriend();
						}

						if (!this.Spoken)
						{
							this.EventStudent[this.EventSpeaker[this.EventPhase]].
								CharacterAnimation.CrossFade(this.EventAnim[this.EventPhase]);

							if (Distance < 10.0f)
							{
								this.EventSubtitle.text = this.EventSpeech[this.EventPhase];
							}
							
							AudioClipPlayer.Play(this.EventClip[this.EventPhase],
								this.EventStudent[this.EventSpeaker[this.EventPhase]].transform.position + (Vector3.up * 1.50f),
								5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

							this.Spoken = true;
						}
						else
						{
							//if (this.Yandere.transform.position.z > 0.0f)
							//{
								this.Timer += Time.deltaTime;

								if (this.Timer > this.EventClip[this.EventPhase].length)
								{
									this.EventSubtitle.text = string.Empty;
								}

								if (this.Yandere.transform.position.y < (this.EventStudent[1].transform.position.y - 1.0f))
								{
									this.EventSubtitle.transform.localScale = Vector3.zero;
								}
								else
								{
									if (Distance < 10.0f)
									{
										this.Scale = Mathf.Abs((Distance - 10.0f) * 0.20f);

										if (this.Scale < 0.0f)
										{
											this.Scale = 0.0f;
										}

										if (this.Scale > 1.0f)
										{
											this.Scale = 1.0f;
										}

										this.Jukebox.Dip = 1 - (.5f * Scale);

										this.EventSubtitle.transform.localScale =
											new Vector3(this.Scale, this.Scale, this.Scale);
									}
									else
									{
										this.EventSubtitle.transform.localScale = Vector3.zero;
									}
								}

								Animation studentCharAnim =
									this.EventStudent[this.EventSpeaker[this.EventPhase]].CharacterAnimation;
								
								if (studentCharAnim[this.EventAnim[this.EventPhase]].time >=
									studentCharAnim[this.EventAnim[this.EventPhase]].length - 1)
								{
									studentCharAnim.CrossFade(this.EventStudent[this.EventSpeaker[this.EventPhase]].IdleAnim, 1);
								}

								if (this.Timer > (this.EventClip[this.EventPhase].length + 1.0f))
								{
									this.Spoken = false;
									this.EventPhase++;
									this.Timer = 0.0f;

									if (this.EventPhase == this.EventSpeech.Length)
									{
										this.EndEvent();
									}
								}
							//}

							if (!this.Suitor)
							{
								if ((this.Yandere.transform.position.y > (this.EventStudent[1].transform.position.y - 1.0f)) &&
									(this.EventPhase == 7) && (Distance < 5.0f))
								{
									if (this.EventStudent1 == 25)
									{
										if (!EventGlobals.Event1)
										{
											this.Yandere.NotificationManager.DisplayNotification(NotificationType.Info);
											EventGlobals.Event1 = true;
										}
									}
									else if (this.OsanaID < 2)
									{
										if (!EventGlobals.OsanaEvent2)
										{
											this.Yandere.NotificationManager.DisplayNotification(NotificationType.Info);
											EventGlobals.OsanaEvent2 = true;
										}
									}
								}
							}
						}

						if (this.enabled)
						{
							if (Distance < 3)
							{
								Yandere.Eavesdropping = true;
							}
							else
							{
								Yandere.Eavesdropping = false;
							}
						}
					}
				}
			}
		}
	}

	void SettleFriend()
	{
		this.EventStudent[2].MoveTowardsTarget(this.EventLocation[2].position);

		float Angle = Quaternion.Angle(this.EventStudent[2].transform.rotation, this.EventLocation[2].rotation);
		
		if (Angle > 1.0f)
		{
			this.EventStudent[2].transform.rotation = Quaternion.Slerp(
				this.EventStudent[2].transform.rotation, this.EventLocation[2].rotation, 10.0f * Time.deltaTime);
		}
	}

	public void EndEvent()
	{
		if (this.VoiceClip != null)
		{
			Destroy(this.VoiceClip);
		}

		this.EventStudent[1].CurrentDestination =
			this.EventStudent[1].Destinations[this.EventStudent[1].Phase];
		this.EventStudent[1].Pathfinding.target =
			this.EventStudent[1].Destinations[this.EventStudent[1].Phase];

		// [af] Commented in JS code.
		//EventStudent[1].Prompt.enabled = true;

		this.EventStudent[1].EventManager = null;
		this.EventStudent[1].InEvent = false;
		this.EventStudent[1].Private = false;

		this.EventStudent[2].CurrentDestination =
			this.EventStudent[2].Destinations[this.EventStudent[2].Phase];
		this.EventStudent[2].Pathfinding.target =
			this.EventStudent[2].Destinations[this.EventStudent[2].Phase];

		// [af] Commented in JS code.
		//EventStudent[2].Prompt.enabled = true;

		this.EventStudent[2].EventManager = null;
		this.EventStudent[2].InEvent = false;
		this.EventStudent[2].Private = false;

		if (!this.StudentManager.Stop)
		{
			this.StudentManager.UpdateStudents();
		}

		this.Jukebox.Dip = 1;

		this.Yandere.Eavesdropping = false;
		this.EventSubtitle.text = string.Empty;
		this.EventCheck = false;
		this.EventOn = false;
		this.enabled = false;
	}
}