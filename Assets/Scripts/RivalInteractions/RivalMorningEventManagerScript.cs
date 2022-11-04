using System;
using UnityEngine;

public class RivalMorningEventManagerScript : MonoBehaviour
{
#if UNITY_EDITOR

	public OsanaMorningFriendEventScript OsanaLoseFriendEvent;
	public StudentManagerScript StudentManager;
	public JukeboxScript Jukebox;
	public UILabel EventSubtitle;
	public YandereScript Yandere;
	public ClockScript Clock;
	public SpyScript Spy;

	public StudentScript Friend;
	public StudentScript Senpai;
	public StudentScript Rival;

	public Transform[] Location;
	public Transform Epicenter;

	public AudioClip SpeechClip;
	public string[] SpeechText;
	public float[] SpeechTime;

	public GameObject AlarmDisc;
	public GameObject VoiceClip;

	public bool NaturalEnd = false;
	public bool Transfer = false;
	public bool End = false;

	public float TransferTime = 0.0f;
	public float Distance = 0.0f;
	public float Scale = 0.0f;
	public float Timer = 0.0f;

	public DayOfWeek EventDay = DayOfWeek.Sunday;

	public int SpeechPhase = 1;
	public int FriendID = 6;
	public int RivalID = 11;
	public int Phase = 0;
	public int Frame = 0;

	public string Weekday = string.Empty;

	void Start()
	{
		this.EventSubtitle.transform.localScale = Vector3.zero;
		this.Spy.Prompt.enabled = true;

		//Debug.Log("The day of the week is: " + DateGlobals.Weekday + "!");

		if (DateGlobals.Weekday == DayOfWeek.Sunday)
		{
			DateGlobals.Weekday = DayOfWeek.Monday;
		}

		if (DateGlobals.Weekday != this.EventDay ||
			HomeGlobals.LateForSchool ||
			StudentManager.YandereLate ||
			DatingGlobals.SuitorProgress == 2 ||
			StudentGlobals.MemorialStudents > 0)
		{
			this.enabled = false;
		}

		if (this.enabled)
		{
			if (StudentGlobals.GetStudentReputation(10) <= -33.33333f)
			{
				OsanaLoseFriendEvent.OtherEvent = this;
			}
		}
	}

	void Update()
	{
		if (this.Phase == 0)
		{
			if (this.Frame > 0)
			{
				if (this.StudentManager.Students[this.RivalID] != null)
				{
					// [af] Added "gameObject" for C# compatibility.
					if (this.StudentManager.Students[1].gameObject.activeInHierarchy &&
						(this.StudentManager.Students[this.RivalID] != null))
					{
						Debug.Log("Osana's morning Senpai interaction event is now taking place.");

						if (this.StudentManager.Students[this.FriendID] != null &&
							!PlayerGlobals.RaibaruLoner)
						{
							this.Friend = this.StudentManager.Students[this.FriendID];

							if (this.Friend.Investigating){this.Friend.StopInvestigating ();}

							if (StudentGlobals.GetStudentReputation(10) > -33.33333f)
							{
								this.Friend.CharacterAnimation.Play(AnimNames.FemaleCornerPeek);
								this.Friend.Cheer.enabled = true;
							}
							else
							{
								this.Friend.CharacterAnimation.Play(this.Friend.BulliedIdleAnim);
							}

							this.Friend.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
							this.Friend.transform.position = this.Location[3].position;
							this.Friend.transform.eulerAngles = this.Location[3].eulerAngles;
							this.Friend.Pathfinding.canSearch = false;
							this.Friend.Pathfinding.canMove = false;
							this.Friend.Routine = false;
							this.Friend.InEvent = true;
							this.Friend.Spawned = true;

							/*
							this.Friend.Prompt.Hide();
							this.Friend.Prompt.enabled = false;
							*/
						}

						this.Senpai = this.StudentManager.Students[1];
						this.Rival = this.StudentManager.Students[this.RivalID];

						this.Senpai.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
						this.Senpai.CharacterAnimation.Play(this.Senpai.IdleAnim);
						this.Senpai.transform.position = this.Location[1].position;
						this.Senpai.transform.eulerAngles = this.Location[1].eulerAngles;
						this.Senpai.Pathfinding.canSearch = false;
						this.Senpai.Pathfinding.canMove = false;
						this.Senpai.Routine = false;
						this.Senpai.InEvent = true;
						this.Senpai.Spawned = true;

						this.Senpai.Prompt.Hide();
						this.Senpai.Prompt.enabled = false;

						if (this.Rival.Investigating)
						{
							this.Rival.StopInvestigating ();
						}

						this.Rival.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
						this.Rival.CharacterAnimation.Play(this.Rival.IdleAnim);
						this.Rival.transform.position = this.Location[2].position;
						this.Rival.transform.eulerAngles = this.Location[2].eulerAngles;
						this.Rival.Pathfinding.canSearch = false;
						this.Rival.Pathfinding.canMove = false;
						this.Rival.Routine = false;
						this.Rival.InEvent = true;
						this.Rival.Spawned = true;
						this.Rival.Private = true;

						this.Rival.Prompt.Hide();
						this.Rival.Prompt.enabled = false;

						this.Spy.Prompt.enabled = true;

						this.Phase++;

						/////////////////////////
						///// SPECIAL CASES /////
						/////////////////////////

						if (this.EventDay == DayOfWeek.Tuesday)
						{
							this.StudentManager.Students[1].EventBook.SetActive(true);
						}
					}
				}
			}

			this.Frame++;
		}
		else if (this.Phase == 1)
		{
			this.Timer += Time.deltaTime;

			if (this.Timer > 1.0f)
			{
				AudioClipPlayer.Play(this.SpeechClip, 
					this.Epicenter.position + (Vector3.up * 1.50f),
					5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

				this.Rival.CharacterAnimation.CrossFade("f02_" + this.Weekday + "_1");
				this.Senpai.CharacterAnimation.CrossFade(this.Weekday + "_1");
				this.Timer = 0.0f;
				this.Phase++;
			}
		}
		else
		{
			////////////////////////
			///// VOICED LINES /////
			////////////////////////

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
			else
			{
				if (this.Senpai.CharacterAnimation[this.Weekday + "_1"].time >=
					this.Senpai.CharacterAnimation[this.Weekday + "_1"].length)
				{
					Debug.Log("This rival morning event ended naturally because the animation finished playing.");

					this.NaturalEnd = true;
					this.EndEvent();
				}
			}

			/////////////////////////////////////////////////////////////
			///// TRANSFERING AN ITEM FROM ONE CHARACTER TO ANOTHER /////
			/////////////////////////////////////////////////////////////

			if (this.Transfer)
			{
				if (this.Rival.CharacterAnimation["f02_" + this.Weekday + "_1"].time > this.TransferTime)
				{
					this.Senpai.EventBook.SetActive(false);
					this.Rival.EventBook.SetActive(true);

					this.Transfer = false;
				}
			}

			////////////////////////////////////////////
			///// ACTIONS THAT COULD END THE EVENT /////
			////////////////////////////////////////////

			if (this.Clock.Period > 1)
			{
				Debug.Log("The event ended because the school period has advanced.");

				this.EndEvent();
			}

			if (this.Senpai.Alarmed || this.Rival.Alarmed || this.Friend != null && this.Friend.DramaticReaction)
			{
				Debug.Log("The event ended naturally because a character was alarmed.");

				GameObject NewAlarmDisc = Instantiate(this.AlarmDisc,
					this.Rival.transform.position + Vector3.up, Quaternion.identity);
				NewAlarmDisc.GetComponent<AlarmDiscScript>().NoScream = true;
				NewAlarmDisc.transform.localScale = new Vector3(150, 1, 150);

				NewAlarmDisc.GetComponent<AlarmDiscScript>().FocusOnYandere = true;
				NewAlarmDisc.GetComponent<AlarmDiscScript>().Hesitation = .6f;

				this.EndEvent();
			}

			#endif

			#if UNITY_EDITOR
			if (Input.GetKeyDown(KeyCode.LeftControl))
			{
				EndEvent();
			}
			#endif

			#if UNITY_EDITOR

			/////////////////////////
			///// SUBTITLE SIZE /////
			/////////////////////////

			this.Distance = Vector3.Distance(this.Yandere.transform.position, this.Epicenter.position);

			if (this.enabled)
			{
				if ((this.Distance - 4.0f) < 15.0f)
				{
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

					// [af] Replaced if/else statements with boolean expression.
					this.Yandere.Eavesdropping = this.Distance < 3.0f;
				}
				else
				{
                    if ((this.Distance - 4.0f) < 16.0f)
                    {
					    this.EventSubtitle.transform.localScale = Vector3.zero;
                    }

                    if (this.VoiceClip != null)
					{
						this.VoiceClip.GetComponent<AudioSource>().volume = 0.0f;
					}
				}
			}
		}

		if (this.End)
		{
			Debug.Log("The event ended naturally because the ''End'' variable was set to ''true''.");

			EndEvent();
		}
	}

#endif

	public void EndEvent()
	{

#if UNITY_EDITOR

		Debug.Log("Osana's morning ''Talk with Senpai'' event has ended.");

		if (Phase > 0)
		{
			/////////////////////////
			///// SPECIAL CASES /////
			/////////////////////////

			if (this.EventDay == DayOfWeek.Tuesday)
			{
				ScheduleBlock block2 = this.Senpai.ScheduleBlocks[2];
				block2.destination = "Patrol";
				block2.action = "Patrol";

				ScheduleBlock block7 = this.Senpai.ScheduleBlocks[7];
				block7.destination = "Patrol";
				block7.action = "Patrol";

				this.Senpai.GetDestinations();
			}

			if (this.VoiceClip != null)
			{
				Destroy(this.VoiceClip);
			}

			if (!this.Senpai.Alarmed)
			{
				this.Senpai.Pathfinding.canSearch = true;
				this.Senpai.Pathfinding.canMove = true;
				this.Senpai.Routine = true;
			}

			this.Senpai.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
			this.Senpai.EventBook.SetActive(false);
			this.Senpai.InEvent = false;
			this.Senpai.Private = false;

			if (!this.Rival.Alarmed)
			{
				this.Rival.Pathfinding.canSearch = true;
				this.Rival.Pathfinding.canMove = true;
				this.Rival.Routine = true;
			}

			this.Rival.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
			this.Rival.EventBook.SetActive(false);
			this.Rival.Prompt.enabled = true;
			this.Rival.InEvent = false;
			this.Rival.Private = false;

			if (this.Friend != null)
			{
				if (!this.Friend.Alarmed && !this.Friend.DramaticReaction)
				{
					this.Friend.Pathfinding.canSearch = true;
					this.Friend.Pathfinding.canMove = true;
					this.Friend.Routine = true;
				}

				if (this.NaturalEnd)
				{
					this.Friend.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
					this.Friend.Prompt.enabled = true;
					this.Friend.InEvent = false;
					this.Friend.Private = false;
				}
				else
				{
					this.Friend.Pathfinding.target = this.Location[3];
					this.Friend.CurrentDestination = this.Location[3];

					//this.Senpai.FocusOnYandere = true;
				}

				this.Friend.Cheer.enabled = false;
			}

			if (!this.StudentManager.Stop)
			{
				this.StudentManager.UpdateStudents();
			}

			this.Spy.Prompt.Hide();
			this.Spy.Prompt.enabled = false;

			if (this.Spy.Phase > 0)
			{
				this.Spy.End();
			}

			this.Yandere.Eavesdropping = false;
			this.EventSubtitle.text = string.Empty;
			this.enabled = false;

			this.Jukebox.Dip = 1;
		}

		#endif

	}
}