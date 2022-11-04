using System;
using UnityEngine;

public class OsanaMorningFriendEventScript : MonoBehaviour
{
	public RivalMorningEventManagerScript OtherEvent;
	public StudentManagerScript StudentManager;
	public EndOfDayScript EndOfDay;
	public JukeboxScript Jukebox;
	public UILabel EventSubtitle;
	public YandereScript Yandere;
	public ClockScript Clock;
	public SpyScript Spy;

	public StudentScript CurrentSpeaker;
	public StudentScript Friend;
	public StudentScript Rival;

	public Transform Epicenter;

	public Transform[] Location;

	public AudioClip SpeechClip;
	public string[] SpeechText;
	public float[] SpeechTime;
	public string[] EventAnim;
	public int[] Speaker;

	public AudioClip InterruptedClip;
	public string[] InterruptedSpeech;
	public float[] InterruptedTime;
	public string[] InterruptedAnim;
	public int[] InterruptedSpeaker;

	public AudioClip AltSpeechClip;
	public string[] AltSpeechText;
	public float[] AltSpeechTime;
	public string[] AltEventAnim;
	public int[] AltSpeaker;

	public GameObject AlarmDisc;
	public GameObject VoiceClip;

	public Quaternion targetRotation;

	public float Distance = 0.0f;
	public float Scale = 0.0f;
	public float Timer = 0.0f;

	public DayOfWeek EventDay = DayOfWeek.Sunday;

	public int SpeechPhase = 1;
	public int FriendID = 6;
	public int RivalID = 11;
	public int Phase = 0;
	public int Frame = 0;

	public Vector3 OriginalPosition;
	public Vector3 OriginalRotation;

	public bool LosingFriend;

	#if UNITY_EDITOR

	void Start()
	{
		this.EventSubtitle.transform.localScale = Vector3.zero;

		if (LosingFriend)
		{
			if (StudentGlobals.GetStudentReputation(10) > -33.33333f ||
				PlayerGlobals.RaibaruLoner)
			{
				this.enabled = false;
			}
		}
		else
		{
			if (StudentGlobals.GetStudentReputation(10) <= -33.33333f || 
				DateGlobals.Weekday != this.EventDay ||
				HomeGlobals.LateForSchool ||
				StudentManager.YandereLate ||
				DatingGlobals.SuitorProgress == 2 ||
				StudentGlobals.MemorialStudents > 0 ||
				PlayerGlobals.RaibaruLoner)
			{
				this.enabled = false;
			}
		}
	}

	void Update()
	{
		if (this.Phase == 0)
		{
			if (this.Frame > 0)
			{
				if (this.StudentManager.Students[this.RivalID] != null && this.StudentManager.Students[this.FriendID] != null)
				{
					if (this.Friend == null)
					{
						this.Friend = this.StudentManager.Students[this.FriendID];
					}

					if (this.Rival == null)
					{
						this.Rival = this.StudentManager.Students[this.RivalID];
					}

					if (this.Clock.Period == 1)
					{
						if (!this.StudentManager.Students[1].Alarmed &&
							!this.Friend.DramaticReaction &&
							!this.Friend.Alarmed &&
							!this.Rival.Alarmed)
						{
							if (!this.OtherEvent.enabled)
							{
								Debug.Log ("Osana's ''talk with friend before going to the lockers'' event has begun.");

								this.Friend.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
								this.Rival.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;

								this.Friend.CharacterAnimation.CrossFade(this.Friend.WalkAnim);
								this.Friend.Pathfinding.target = this.Location[1];
								this.Friend.CurrentDestination = this.Location[1];
								this.Friend.Pathfinding.canSearch = true;
								this.Friend.Pathfinding.canMove = true;
								this.Friend.Routine = false;
								this.Friend.InEvent = true;

								this.Rival.CharacterAnimation.CrossFade(this.Rival.WalkAnim);
								this.Rival.Pathfinding.target = this.Location[2];
								this.Rival.CurrentDestination = this.Location[2];
								this.Rival.Pathfinding.canSearch = true;
								this.Rival.Pathfinding.canMove = true;
								this.Rival.Routine = false;
								this.Rival.InEvent = true;

								if (!this.LosingFriend)
								{
									this.Friend.Private = true;
									this.Rival.Private = true;

									if (!this.OtherEvent.NaturalEnd)
									{
										SpeechClip = InterruptedClip;
										SpeechText = InterruptedSpeech;
										SpeechTime = InterruptedTime;
										EventAnim = InterruptedAnim;
										Speaker = InterruptedSpeaker;
									}

									bool MusumeUnavailable = false;

									if (StudentGlobals.GetStudentDead(81) ||
										StudentGlobals.GetStudentKidnapped(81) ||
										StudentGlobals.GetStudentArrested(81) ||
										StudentGlobals.GetStudentExpelled(81) ||
										StudentGlobals.GetStudentReputation(81) < -33.33333f)
									{
										Debug.Log("Musume's unavailable.");

										MusumeUnavailable = true;
									}

									if (DateGlobals.Weekday == DayOfWeek.Friday && MusumeUnavailable)
									{
										if (this.OtherEvent.NaturalEnd)
										{
											SpeechClip = AltSpeechClip;
											SpeechText = AltSpeechText;
											SpeechTime = AltSpeechTime;
											EventAnim = AltEventAnim;
											Speaker = AltSpeaker;
										}
									}
								}

								this.Phase++;
							}
						}
					}
				}
			}

			this.Frame++;
		}
		else
		{
			//Osana and Friend arrive at their destination and talk.
			if (this.Phase == 1)
			{
				this.Friend.Pathfinding.canSearch = true;
				this.Friend.Pathfinding.canMove = true;

				if (this.Rival.DistanceToDestination < 0.50f)
				{
					this.Rival.CharacterAnimation.CrossFade(this.Rival.IdleAnim);
					this.Rival.Pathfinding.canSearch = false;
					this.Rival.Pathfinding.canMove = false;
					this.SettleRival();
				}

				if (this.Friend.DistanceToDestination < 0.50f)
				{
					this.Friend.CharacterAnimation.CrossFade(this.Friend.IdleAnim);
					this.Friend.Pathfinding.canSearch = false;
					this.Friend.Pathfinding.canMove = false;
					this.SettleFriend();
				}

				if (this.Rival.DistanceToDestination < 0.50f && this.Friend.DistanceToDestination < 0.50f)
				{
					AudioClipPlayer.Play(this.SpeechClip, 
						this.Friend.transform.position + (Vector3.up * 1.50f),
						5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

					//Debug.Log("Current SpeechPhase is: " + SpeechPhase);

					this.EventSubtitle.text = this.SpeechText[this.SpeechPhase];

					this.PlayRelevantAnim();

					//this.Rival.CharacterAnimation.CrossFade(this.EventAnim[1]);
					this.Rival.Pathfinding.canSearch = false;
					this.Rival.Pathfinding.canMove = false;
					this.Rival.Obstacle.enabled = true;

					//this.Friend.CharacterAnimation.CrossFade(this.EventAnim[2]);
					this.Friend.Pathfinding.canSearch = false;
					this.Friend.Pathfinding.canMove = false;
					this.Friend.Obstacle.enabled = true;

					this.Phase++;
				}
			}
			//Osana and her friend talk for a little while.
			else if (this.Phase == 2)
			{
				if (this.CurrentSpeaker != null)
				{
					if (this.SpeechPhase > 0)
					{
						if (this.CurrentSpeaker.CharacterAnimation[this.EventAnim[this.SpeechPhase - 1]].time >=
							this.CurrentSpeaker.CharacterAnimation[this.EventAnim[this.SpeechPhase - 1]].length - 1)
						{
							this.CurrentSpeaker.CharacterAnimation.CrossFade(this.CurrentSpeaker.IdleAnim, 1);
						}
					}
				}

				/*
				if (!this.OtherEvent.NaturalEnd)
				{
					this.EndEvent();
				}
				else
				{
				*/

				this.Timer += Time.deltaTime;

				if (this.VoiceClip != null)
				{
					this.VoiceClip.GetComponent<AudioSource>().pitch = Time.timeScale;
				}

				if (this.SpeechPhase < this.SpeechTime.Length)
				{
					if (this.Timer > this.SpeechTime[this.SpeechPhase])
					{
						this.EventSubtitle.text = this.SpeechText[this.SpeechPhase];
						this.PlayRelevantAnim();
						this.SpeechPhase++;
					}
				}

				this.SettleRival();
				this.SettleFriend();

				/*
				if (this.Rival.CharacterAnimation[this.EventAnim[2]].time >= this.Rival.CharacterAnimation[this.EventAnim[2]].length) 
				{
					this.EndEvent();
				}
				*/

				if (this.Timer > SpeechClip.length) 
				{
					this.EndEvent();
				}
				//}
			}

			////////////////////////////////////////////
			///// ACTIONS THAT COULD END THE EVENT /////
			////////////////////////////////////////////

			if (this.Rival.Alarmed || this.Friend.Alarmed || this.Friend.DramaticReaction)
			{
				Debug.Log("The event ended naturally because a character was alarmed.");

				GameObject NewAlarmDisc = Instantiate(this.AlarmDisc,
					this.Yandere.transform.position + Vector3.up, Quaternion.identity);
				NewAlarmDisc.GetComponent<AlarmDiscScript>().NoScream = true;
				NewAlarmDisc.transform.localScale = new Vector3(200, 1, 200);

				this.EndEvent();
			}

			#endif

			#if UNITY_EDITOR
			if (Input.GetKeyDown(KeyCode.LeftControl))
			{
				EndEvent();

				if (this.Rival.ShoeRemoval.Locker == null)
				{
					this.Rival.ShoeRemoval.Start();
				}

				this.Rival.ShoeRemoval.PutOnShoes();
			}
			#endif

			#if UNITY_EDITOR

			/////////////////////////
			///// SUBTITLE SIZE /////
			/////////////////////////

			this.Distance = Vector3.Distance(this.Yandere.transform.position, this.Epicenter.position);

			if ((this.Distance - 4.0f) < 15.0f)
			{
				//Scale = Mathf.Abs(1 - ((Distance + 5) / 22.0));
				this.Scale = Mathf.Abs(1.0f - ((this.Distance - 4.0f) / 15.0f));

				if (this.Scale < 0.0f)
				{
					this.Scale = 0.0f;
				}

				if (this.Scale > 1.0f)
				{
					this.Scale = 1.0f;
				}

				this.Jukebox.Dip = 1 - (.5f * Scale);

				this.EventSubtitle.transform.localScale = new Vector3(this.Scale, this.Scale, this.Scale);

				if (this.VoiceClip != null)
				{
					this.VoiceClip.GetComponent<AudioSource>().volume = this.Scale;
				}

				if (this.Phase > 1)
				{
					this.Yandere.Eavesdropping = this.Distance < 3.0f;
				}
			}
			else
			{
				this.EventSubtitle.transform.localScale = Vector3.zero;

				if (this.VoiceClip != null)
				{
					this.VoiceClip.GetComponent<AudioSource>().volume = 0.0f;
				}
			}

			if (this.VoiceClip == null)
			{
				this.EventSubtitle.text = string.Empty;
			}
		}
	}

	public void EndEvent()
	{
		Debug.Log ("Osana's ''talk with friend before going to the lockers'' event has ended.");

		if (this.VoiceClip != null)
		{
			Destroy(this.VoiceClip);
		}

		if (this.Rival != null)
		{
			if (!this.Rival.Alarmed)
			{
				this.Rival.CharacterAnimation.CrossFade(this.Rival.WalkAnim);
				this.Rival.DistanceToDestination = 100.0f;

				this.Rival.CurrentDestination = this.Rival.Destinations[this.Rival.Phase];
				this.Rival.Pathfinding.target = this.Rival.Destinations[this.Rival.Phase];

				this.Rival.Pathfinding.canSearch = true;
				this.Rival.Pathfinding.canMove = true;
				this.Rival.Routine = true;
			}

			if (this.Rival.Alarmed)
			{
				this.Rival.ReturnToRoutineAfter = true;
			}

			this.Rival.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
			this.Rival.Obstacle.enabled = false;
			this.Rival.Prompt.enabled = true;
			this.Rival.InEvent = false;
			this.Rival.Private = false;
		}

		if (this.Friend != null)
		{
			if (!this.Friend.Alarmed && !this.Friend.DramaticReaction)
			{
				this.Friend.CharacterAnimation.CrossFade(this.Friend.WalkAnim);
				this.Friend.DistanceToDestination = 100.0f;

				this.Friend.CurrentDestination = this.Friend.Destinations[this.Friend.Phase];
				this.Friend.Pathfinding.target = this.Friend.Destinations[this.Friend.Phase];

				this.Friend.Pathfinding.canSearch = true;
				this.Friend.Pathfinding.canMove = true;
				this.Friend.Routine = true;
				this.Friend.Calm = false;
			}

			this.Friend.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
			this.Friend.Obstacle.enabled = false;
			this.Friend.Prompt.enabled = true;
			this.Friend.InEvent = false;
			this.Friend.Private = false;

			if (this.Rival.Alarmed)
			{
				this.Friend.FocusOnYandere = true;
			}
		}

		this.Spy.Prompt.enabled = false;
		this.Spy.Prompt.Hide();

		if (this.Spy.Phase > 0)
		{
			this.Spy.End();
		}

		if (!this.StudentManager.Stop)
		{
			this.StudentManager.UpdateStudents();
		}

		this.Yandere.Eavesdropping = false;

		this.EventSubtitle.text = string.Empty;

		this.Jukebox.Dip = 1;

		this.enabled = false;

		if (LosingFriend)
		{
			Debug.Log("Raibaru will no longer hang out with Osana.");
			this.EndOfDay.RaibaruLoner = true;

			Debug.Log("Raibaru has become a loner, so Osana's schedule has changed.");

			ScheduleBlock newBlock2 = this.Rival.ScheduleBlocks[2];
			newBlock2.destination = "Patrol";
			newBlock2.action = "Patrol";

			ScheduleBlock newBlock7 = this.Rival.ScheduleBlocks[7];
			newBlock7.destination = "Patrol";
			newBlock7.action = "Patrol";

			this.Rival.GetDestinations();
		}
	}

	void SettleRival()
	{
		this.Rival.MoveTowardsTarget(this.Location[2].position);

		float Angle = Quaternion.Angle(this.Rival.transform.rotation, this.Location[2].rotation);

		if (Angle > 1.0f)
		{
			this.Rival.transform.rotation = Quaternion.Slerp(
				this.Rival.transform.rotation, this.Location[2].rotation, 10.0f * Time.deltaTime);
		}
	}

	void SettleFriend()
	{
		this.Friend.MoveTowardsTarget(this.Location[1].position);

		this.Friend.transform.LookAt(this.Rival.transform.position);

		/*
		float Angle = Quaternion.Angle(this.Friend.transform.rotation, this.Location[1].rotation);

		if (Angle > 1.0f)
		{
			this.Friend.transform.rotation = Quaternion.Slerp(
				this.Friend.transform.rotation, this.Location[1].rotation, 10.0f * Time.deltaTime);
		}
		*/
	}

	void PlayRelevantAnim()
	{
		if (this.Speaker[SpeechPhase] == 1)
		{
			this.Rival.CharacterAnimation.CrossFade(this.EventAnim[SpeechPhase]);
			this.Friend.CharacterAnimation.CrossFade(this.Friend.IdleAnim);

			this.CurrentSpeaker = Rival;
		}
		else
		{
			this.Rival.CharacterAnimation.CrossFade(this.Rival.IdleAnim);
			this.Friend.CharacterAnimation.CrossFade(this.EventAnim[SpeechPhase]);

			this.CurrentSpeaker = Friend;
		}
	}

	#endif
}