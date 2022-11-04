using System;
using UnityEngine;

public class OsanaFridayBeforeClassEvent2Script : MonoBehaviour
{
	public OsanaFridayBeforeClassEvent1Script OtherEvent;
	public StudentManagerScript StudentManager;
	public AudioSoftwareScript AudioSoftware;
	public JukeboxScript Jukebox;
	public UILabel EventSubtitle;
	public YandereScript Yandere;
	public ClockScript Clock;
	public SpyScript Spy;

	public StudentScript Ganguro;
	public StudentScript Friend;
	public StudentScript Rival;

	public Transform[] Location;

	public AudioClip[] SpeechClip;
	public string[] SpeechText;
	public float[] SpeechTime;
	public string[] EventAnim;

	public GameObject AlarmDisc;
	public GameObject VoiceClip;

	public Quaternion targetRotation;

	public float Distance = 0.0f;
	public float Scale = 0.0f;
	public float Timer = 0.0f;

	public DayOfWeek EventDay = DayOfWeek.Sunday;
	public int SpeechPhase = 1;
	public int GanguroID = 81;
	public int FriendID = 10;
	public int RivalID = 11;
	public int Phase = 0;
	public int Frame = 0;

	public Vector3 OriginalPosition;
	public Vector3 OriginalRotation;

	#if UNITY_EDITOR

	void Start()
	{
		this.EventSubtitle.transform.localScale = Vector3.zero;

		if (DateGlobals.Weekday != this.EventDay)
		{
			this.enabled = false;
		}
		else
		{
			if (StudentGlobals.GetStudentDead(81) ||
				StudentGlobals.GetStudentKidnapped(81) ||
				StudentGlobals.GetStudentArrested(81) ||
				StudentGlobals.GetStudentExpelled(81) ||
				StudentGlobals.GetStudentReputation(81) < -33.33333f)
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
				if (this.StudentManager.Students[this.RivalID] != null && this.StudentManager.Students[this.GanguroID] != null)
				{
					if (this.Ganguro == null)
					{
						this.Ganguro = this.StudentManager.Students[this.GanguroID];
					}

					if (this.Rival == null)
					{
						this.Rival = this.StudentManager.Students[this.RivalID];
					}

					if (this.Friend == null)
					{
						if (this.StudentManager.Students[this.FriendID] != null &&
							!PlayerGlobals.RaibaruLoner)
						{
							this.Friend = this.StudentManager.Students[this.FriendID];
						}
					}

					if (this.Clock.HourTime > 7.25)
					{
						if (!this.Rival.InEvent && this.Rival.Indoors && /*!this.OtherEvent.enabled &&*/ !this.Rival.Wet)
						{
							Debug.Log ("Osana's ''Talk with Musume'' event has begun.");

							this.Ganguro.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
							this.Rival.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;

							this.Ganguro.CharacterAnimation.CrossFade(this.Ganguro.SprintAnim);
							this.Ganguro.Pathfinding.target = this.Rival.transform;
							this.Ganguro.CurrentDestination = this.Rival.transform;
							this.Ganguro.Pathfinding.canSearch = true;
							this.Ganguro.Pathfinding.canMove = true;
							this.Ganguro.Pathfinding.speed = 4;
							this.Ganguro.SpeechLines.Stop();
							this.Ganguro.Routine = false;
							this.Ganguro.InEvent = true;

							this.Rival.Prompt.Hide ();
							this.Rival.Prompt.enabled = false;

							this.Phase++;
						}
					}
				}
			}

			this.Frame++;
		}
		else
		{
			//Osana reaches her destination and performs an animation.
			if (this.Phase == 1)
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					//Figure out some way to advance to the first phase of the event.
				}

				if (this.Ganguro.DistanceToDestination < 1.00f)
				{
					AudioClipPlayer.Play(this.SpeechClip[1], 
						this.Ganguro.transform.position + (Vector3.up * 1.50f),
						5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

					this.Rival.CharacterAnimation.CrossFade(this.EventAnim[1]);
					this.Rival.Pathfinding.canSearch = false;
					this.Rival.Pathfinding.canMove = false;
					this.Rival.Obstacle.enabled = true;
					this.Rival.SpeechLines.Stop();
					this.Rival.Routine = false;
					this.Rival.InEvent = true;

					this.Ganguro.CharacterAnimation.CrossFade(this.EventAnim[2]);
					this.Ganguro.Pathfinding.canSearch = false;
					this.Ganguro.Pathfinding.canMove = false;
					this.Ganguro.Obstacle.enabled = true;

					this.EventSubtitle.text = SpeechText[1];
					this.Phase++;
				}
			}
			//Musume talks to Osana.
			else if (this.Phase == 2)
			{
				this.targetRotation = Quaternion.LookRotation(
					this.Ganguro.transform.position - this.Rival.transform.position);

				this.Rival.transform.rotation = Quaternion.Slerp(
					this.Rival.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);

				this.targetRotation = Quaternion.LookRotation(
						this.Rival.transform.position - this.Ganguro.transform.position);
					this.Ganguro.transform.rotation = Quaternion.Slerp(
						this.Ganguro.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);

				if (this.Rival.CharacterAnimation[this.EventAnim[1]].time >= 4)
				{
					this.EventSubtitle.text = SpeechText[2];
					this.Ganguro.Pathfinding.speed = 1;
					this.Phase++;
				}
			}
			//Once the animation ends, Musume and Osana walk somewhere.
			else if (this.Phase == 3)
			{
				if (this.Rival.CharacterAnimation[this.EventAnim[1]].time >= this.Rival.CharacterAnimation[this.EventAnim[1]].length)
				{
					this.Rival.CharacterAnimation.CrossFade(this.Rival.WalkAnim);
					this.Rival.Pathfinding.target = this.Location[1];
					this.Rival.CurrentDestination = this.Location[1];
					this.Rival.Pathfinding.canSearch = true;
					this.Rival.Pathfinding.canMove = true;

					this.Ganguro.CharacterAnimation.CrossFade(this.Ganguro.WalkAnim);
					this.Ganguro.Pathfinding.target = this.Location[2];
					this.Ganguro.CurrentDestination = this.Location[2];
					this.Ganguro.Pathfinding.canSearch = true;
					this.Ganguro.Pathfinding.canMove = true;

					this.Spy.Prompt.enabled = true;

					this.Phase++;
				}
			}
			//Musume and Osana arrive at their destination and talk again.
			else if (this.Phase == 4)
			{
				if (this.Friend != null)
				{
					if (this.Rival.DistanceToDestination < 5)
					{
						this.Friend.CurrentDestination = this.Location[3];
						this.Friend.Pathfinding.target = this.Location[3];
						this.Friend.DistanceToDestination = .5f;

						this.Friend.IdleAnim = AnimNames.FemaleSpying;
						this.Friend.SlideIn = true;
					}
				}

				if (this.Rival.DistanceToDestination < 0.50f)
				{
					this.Rival.CharacterAnimation.CrossFade(this.Rival.IdleAnim);

					this.SettleRival();
				}

				if (this.Ganguro.DistanceToDestination < 0.50f)
				{
					this.Ganguro.CharacterAnimation.CrossFade(this.Ganguro.IdleAnim);

					this.SettleGanguro();
				}

				if (this.Rival.DistanceToDestination < 0.50f && this.Ganguro.DistanceToDestination < 0.50f)
				{
					AudioClipPlayer.Play(this.SpeechClip[2], 
						this.Ganguro.transform.position + (Vector3.up * 1.50f),
						5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

					this.Rival.CharacterAnimation.CrossFade(this.EventAnim[3]);
					this.Rival.Pathfinding.canSearch = false;
					this.Rival.Pathfinding.canMove = false;
					this.Rival.Obstacle.enabled = true;

					this.Ganguro.CharacterAnimation.CrossFade(this.EventAnim[4]);
					this.Ganguro.Pathfinding.canSearch = false;
					this.Ganguro.Pathfinding.canMove = false;
					this.Ganguro.Obstacle.enabled = true;

					this.Jukebox.Volume = this.Jukebox.Volume * .1f;

					this.Phase++;
				}
			}
			//Musume and Osana talk for a little while.
			else if (this.Phase == 5)
			{
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
						this.SpeechPhase++;
					}
				}

				if (this.Timer > 3.9)
				{
					if (this.Spy.CanRecord)
					{
						this.Spy.PromptBar.Label[0].text = "";
						this.Spy.PromptBar.UpdateButtons();
						this.Spy.CanRecord = false;
					}
				}

				this.SettleRival();
				this.SettleGanguro();

				if (this.Rival.CharacterAnimation[this.EventAnim[3]].time >= this.Rival.CharacterAnimation [this.EventAnim[3]].length) 
				{
					this.EndEvent();
				}
			}

			////////////////////////////////////////////
			///// ACTIONS THAT COULD END THE EVENT /////
			////////////////////////////////////////////

			if (this.Rival.Alarmed || this.Clock.HourTime > 8)
			{
				this.EndEvent();
			}

			/////////////////////////
			///// SUBTITLE SIZE /////
			/////////////////////////

			this.Distance = Vector3.Distance(this.Yandere.transform.position, this.Rival.transform.position);

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
		Debug.Log ("Osana's second Friday before class event has ended.");

		if (this.VoiceClip != null)
		{
			Destroy(this.VoiceClip);
		}

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

		this.Rival.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
		this.Rival.Obstacle.enabled = false;
		this.Rival.Prompt.enabled = true;
		this.Rival.InEvent = false;
		this.Rival.Private = false;

		if (!this.Ganguro.Alarmed)
		{
			this.Ganguro.CharacterAnimation.CrossFade(this.Ganguro.WalkAnim);
			this.Ganguro.DistanceToDestination = 100.0f;

			this.Ganguro.CurrentDestination = this.Ganguro.Destinations[this.Ganguro.Phase];
			this.Ganguro.Pathfinding.target = this.Ganguro.Destinations[this.Ganguro.Phase];

			this.Ganguro.Pathfinding.canSearch = true;
			this.Ganguro.Pathfinding.canMove = true;
			this.Ganguro.Routine = true;
		}

		this.Ganguro.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
		this.Ganguro.Obstacle.enabled = false;
		this.Ganguro.Prompt.enabled = true;
		this.Ganguro.InEvent = false;
		this.Ganguro.Private = false;

		if (this.Friend != null)
		{
			this.Friend.CurrentDestination = this.Friend.FollowTarget.transform;
			this.Friend.Pathfinding.target = this.Friend.FollowTarget.transform;

			this.Friend.IdleAnim = this.Friend.OriginalIdleAnim;
			this.Friend.DistanceToDestination = 1;
			this.Friend.SlideIn = false;
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

		if (this.Spy.Recording)
		{
			AudioSoftware.ConversationRecorded = true;
		}

		this.EventSubtitle.text = string.Empty;
		this.Jukebox.Dip = 1;
		this.enabled = false;
	}

	void SettleRival()
	{
		this.Rival.MoveTowardsTarget(this.Location[1].position);

		float Angle = Quaternion.Angle(this.Rival.transform.rotation, this.Location[1].rotation);

		if (Angle > 1.0f)
		{
			this.Rival.transform.rotation = Quaternion.Slerp(
				this.Rival.transform.rotation, this.Location[1].rotation, 10.0f * Time.deltaTime);
		}
	}

	void SettleGanguro()
	{
		this.Ganguro.MoveTowardsTarget(this.Location[2].position);

		float Angle = Quaternion.Angle(this.Ganguro.transform.rotation, this.Location[2].rotation);

		if (Angle > 1.0f)
		{
			this.Ganguro.transform.rotation = Quaternion.Slerp(
				this.Ganguro.transform.rotation, this.Location[2].rotation, 10.0f * Time.deltaTime);
		}
	}

	#endif
}