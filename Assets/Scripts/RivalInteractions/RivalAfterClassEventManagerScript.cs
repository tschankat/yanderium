using System;
using UnityEngine;

public class RivalAfterClassEventManagerScript : MonoBehaviour
{
#if UNITY_EDITOR
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

	public AudioClip CancelledSpeechClip;
	public string[] CancelledSpeechText;
	public float[] CancelledSpeechTime;

	public AudioClip SabotagedSpeechClip;
	public string[] SabotagedSpeechText;
	public float[] SabotagedSpeechTime;

	public AudioClip SpeechClip;
	public string[] SpeechText;
	public float[] SpeechTime;

	public AudioClip ImpatientSpeechClip;
	public string ImpatientSpeechText;

	public GameObject AlarmDisc;
	public GameObject VoiceClip;

	public bool LookAtSenpai = false;
	public bool EventActive = false;
	public bool NaturalEnd = false;
	public bool Cancelled = false;
	public bool Impatient = false;
	public bool Sabotaged = false;
	public bool Transfer = false;
	public bool TakeOut = false;
	public bool PutAway = false;
	public bool Return = false;

	public float TransferTime = 0.0f;
	public float ReturnTime = 0.0f;

	public float TakeOutTime = 0.0f;
	public float PutAwayTime = 0.0f;

	public float Distance = 0.0f;
	public float Scale = 0.0f;
	public float Timer = 0.0f;

	public DayOfWeek EventDay = DayOfWeek.Sunday;

	public int SpeechPhase = 1;
	public int FriendID = 10;
	public int RivalID = 11;
	public int Phase = 0;
	public int Frame = 0;

	public string Weekday = string.Empty;
	public string Suffix = string.Empty;

	void Start()
	{
		this.EventSubtitle.transform.localScale = Vector3.zero;
		this.Spy.Prompt.enabled = false;
		this.Spy.Prompt.Hide();

		if (DateGlobals.Weekday != this.EventDay)
		{
			this.enabled = false;
		}
	}

	void Update()
	{
		if (this.Phase == 0)
		{
			if (this.Frame > 0)
			{
				this.Senpai = this.StudentManager.Students[1];

				if (this.StudentManager.Students[this.RivalID] != null)
				{
					this.Rival = this.StudentManager.Students[this.RivalID];
				}
				else
				{
					this.enabled = false;
				}
			}

			if (this.Frame > 1)
			{
				/*
				if (Input.GetKeyDown(KeyCode.Space))
				{
					this.Clock.PresentTime = 60 * 17;
					
					this.Senpai.ShoeRemoval.Start();
					this.Senpai.ShoeRemoval.PutOnShoes();
					
					this.Rival.ShoeRemoval.Start();
					this.Rival.ShoeRemoval.PutOnShoes();
				}
				*/

				if (this.Clock.HourTime > 17.25f)
				{
					// [af] Added ".gameObject." for compatibility with C#.
					if (this.Senpai.gameObject.activeInHierarchy && (this.Rival != null))
					{
						if (this.Senpai.Leaving || this.Senpai.CurrentDestination == this.StudentManager.Exit)
						{
							if (!this.Senpai.InEvent)
							{
								//Senpai = StudentManager.Students[1];

								this.Senpai.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
								this.Senpai.CharacterAnimation.CrossFade(this.Senpai.WalkAnim);
								this.Senpai.Pathfinding.target = this.Location[1];
								this.Senpai.CurrentDestination = this.Location[1];
								this.Senpai.Pathfinding.canSearch = true;
								this.Senpai.Pathfinding.canMove = true;
								//this.Senpai.Routine = false;
								this.Senpai.InEvent = true;

								this.Senpai.DistanceToDestination = 100;

								this.Senpai.Prompt.Hide();
								this.Senpai.Prompt.enabled = false;

								this.Spy.Prompt.enabled = true;
							}
						}

						if (this.Rival.Leaving  || this.Rival.CurrentDestination == this.StudentManager.Exit)
						{
							if (!this.Rival.InEvent)
							{
								//Rival = StudentManager.Students[this.RivalID];

								this.Rival.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
								this.Rival.CharacterAnimation.CrossFade(this.Rival.WalkAnim);
								this.Rival.Pathfinding.target = this.Location[2];
								this.Rival.CurrentDestination = this.Location[2];
								this.Rival.Pathfinding.canSearch = true;
								this.Rival.Pathfinding.canMove = true;
								//this.Rival.Routine = false;
								this.Rival.InEvent = true;

								this.Rival.DistanceToDestination = 100;

								this.Rival.Prompt.Hide();
								this.Rival.Prompt.enabled = false;

								this.Spy.Prompt.enabled = true;
							}
						}

						if (this.Senpai.CurrentDestination == this.Location[1] && this.Senpai.DistanceToDestination < 0.50f)
						{
							if (!Impatient)
							{
								this.Senpai.CharacterAnimation.CrossFade(AnimNames.MaleWait);
								this.Senpai.Pathfinding.canSearch = false;
								this.Senpai.Pathfinding.canMove = false;

								if (this.Clock.HourTime > 17.9166666f)
								{
									AudioClipPlayer.Play(this.ImpatientSpeechClip, 
										this.Epicenter.position + (Vector3.up * 1.50f),
										5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

									this.Senpai.CharacterAnimation.CrossFade("impatience_00");
									this.EventSubtitle.text = this.ImpatientSpeechText;
									this.Impatient = true;
								}
							}
							else
							{
								if (this.Senpai.CharacterAnimation["impatience_00"].time >= this.Senpai.CharacterAnimation["impatience_00"].length)
								{
									DatingGlobals.RivalSabotaged++;
									Debug.Log("Sabotage Progress: " + DatingGlobals.RivalSabotaged + "/5");

									EndEvent();
								}
							}
						}

						if (this.Rival.CurrentDestination == this.Location[2] && this.Rival.DistanceToDestination < 0.50f)
						{
							this.Rival.CharacterAnimation.CrossFade(this.Rival.IdleAnim);
							this.Rival.Pathfinding.canSearch = false;
							this.Rival.Pathfinding.canMove = false;
						}

						if (this.Rival.CurrentDestination == this.Location[2] && this.Senpai.CurrentDestination == this.Location[1])
						{
							if ((this.Senpai.DistanceToDestination < 0.50f) &&
								(this.Rival.DistanceToDestination < 0.50f) &&
								!this.Impatient)
							{
								Debug.Log ("Osana's Wednesday after school event has begun.");

								this.Phase++;
							}
						}
					}
				}
			}

			this.Frame++;
		}
		else if (this.Phase == 1)
		{
			if (this.StudentManager.Students[this.FriendID] != null  &&
				!PlayerGlobals.RaibaruLoner)
			{
				this.Friend = this.StudentManager.Students[this.FriendID];

				this.Friend.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
				this.Friend.Pathfinding.target = this.Location[3];
				this.Friend.CurrentDestination = this.Location[3];

				this.Friend.ManualRotation = true;
				this.Friend.Cheer.enabled = true;
				this.Friend.InEvent = true;
			}

			/////////////////////////
			///// SPECIAL CASES /////
			/////////////////////////

			if (this.EventDay == DayOfWeek.Tuesday)
			{
				this.Rival.EventBook.SetActive(true);

				if (!this.Sabotaged)
				{
					AudioClipPlayer.Play(this.SpeechClip, 
						this.Epicenter.position + (Vector3.up * 1.50f),
						5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

					this.TransferTime = 8.50f;
					this.Suffix = "A";
				}
				else
				{
					AudioClipPlayer.Play(this.SabotagedSpeechClip, 
						this.Epicenter.position + (Vector3.up * 1.50f),
						5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

					this.TransferTime = 11.0f;
					this.Suffix = "B";
				}
			}
			else if (this.EventDay == DayOfWeek.Wednesday)
			{
				this.Sabotaged = this.Rival.LewdPhotos;

				if (this.Rival.Phoneless)
				{
					this.Cancelled = true;
				}

				if (this.Cancelled)
				{
					AudioClipPlayer.Play(this.CancelledSpeechClip, 
						this.Epicenter.position + (Vector3.up * 1.50f),
						5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

					this.Transfer = false;
					this.TakeOut = false;

					this.Suffix = "C";
				}
				else if (!this.Sabotaged)
				{
					AudioClipPlayer.Play(this.SpeechClip, 
						this.Epicenter.position + (Vector3.up * 1.50f),
						5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

					this.TransferTime = 4.833333f;
					this.ReturnTime = 35.0f;

					this.TakeOutTime = 0.75f;
					this.PutAwayTime = 36.5f;

					this.Suffix = "A";
				}
				else
				{
					AudioClipPlayer.Play(this.SabotagedSpeechClip, 
						this.Epicenter.position + (Vector3.up * 1.50f),
						5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

					this.TransferTime = 4.833333f;
					this.ReturnTime = 26.5f;

					this.TakeOutTime = 0.75f;
					this.PutAwayTime = 50.0f;

					this.Suffix = "B";
				}
			}
			else if (this.EventDay == DayOfWeek.Thursday)
			{
				AudioClipPlayer.Play(this.SpeechClip, 
					this.Epicenter.position + (Vector3.up * 1.50f),
					5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

				this.Suffix = "A";
			}
				
			this.Rival.CharacterAnimation.CrossFade ("f02_" + this.Weekday + "_3" + this.Suffix);

			if (this.EventDay == DayOfWeek.Thursday)
			{
				//Senpai doesn't have any spoken lines during this interaction.
				this.Senpai.CharacterAnimation.CrossFade (this.Senpai.IdleAnim);
			}
			else
			{
				this.Senpai.CharacterAnimation.CrossFade ("" + this.Weekday + "_3" + this.Suffix);
			}

			this.Timer = 0.0f;
			this.Phase++;
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

			if (this.Cancelled)
			{
				if (this.SpeechPhase < this.CancelledSpeechTime.Length)
				{
					if (this.Timer > this.CancelledSpeechTime[this.SpeechPhase])
					{
						this.EventSubtitle.text = this.CancelledSpeechText[this.SpeechPhase];
						this.SpeechPhase++;
					}
				}
			}
			else
			{
				if (!this.Sabotaged)
				{
					if (this.SpeechPhase < this.SpeechTime.Length)
					{
						if (this.Timer > this.SpeechTime[this.SpeechPhase])
						{
							this.EventSubtitle.text = this.SpeechText[this.SpeechPhase];
							this.SpeechPhase++;
						}
					}
				}
				//If the event was sabotaged...
				else
				{
					if (this.SpeechPhase < this.SabotagedSpeechTime.Length)
					{
						if (this.Timer > this.SabotagedSpeechTime[this.SpeechPhase])
						{
							this.EventSubtitle.text = this.SabotagedSpeechText[this.SpeechPhase];
							this.SpeechPhase++;
						}
					}

					if (this.Senpai.CharacterAnimation[this.Weekday + "_3" + this.Suffix].time >=
						this.Senpai.CharacterAnimation[this.Weekday + "_3" + this.Suffix].length)
					{
						this.Rival.StopRotating = true;
						this.LookAtSenpai = true;
						EndSenpai();
					}

					if (this.LookAtSenpai)
					{
						this.Rival.targetRotation = Quaternion.LookRotation(
							this.Senpai.transform.position - this.Rival.transform.position);

						this.Rival.transform.rotation = Quaternion.Slerp(
							this.Rival.transform.rotation, this.Rival.targetRotation, 10.0f * Time.deltaTime);
					}
				}
			}

			if (this.Rival.CharacterAnimation["f02_" + this.Weekday + "_3" + this.Suffix].time >=
				this.Rival.CharacterAnimation["f02_" + this.Weekday + "_3" + this.Suffix].length)
			{
				this.NaturalEnd = true;
				this.EndEvent();
			}

			/////////////////////////////////////////////
			///// TAKING OUT AND PUTTING AWAY ITEMS /////
			/////////////////////////////////////////////

			if (this.TakeOut)
			{
				if (this.EventDay == DayOfWeek.Wednesday)
				{
					if (this.Rival.CharacterAnimation["f02_" + this.Weekday + "_3" + this.Suffix].time > this.TakeOutTime)
					{
						this.Rival.SmartPhone.SetActive(true);
						this.TakeOut = false;
						this.PutAway = true;
					}
				}
			}

			if (this.PutAway)
			{
				if (this.EventDay == DayOfWeek.Wednesday)
				{
					if (this.Rival.CharacterAnimation["f02_" + this.Weekday + "_3" + this.Suffix].time > this.PutAwayTime)
					{
						this.Rival.SmartPhone.SetActive(false);
						this.PutAway = false;
					}
				}
			}

			/////////////////////////////////////////////////////////////
			///// TRANSFERING AN ITEM FROM ONE CHARACTER TO ANOTHER /////
			/////////////////////////////////////////////////////////////

			if (this.Transfer)
			{
				if (this.EventDay == DayOfWeek.Tuesday)
				{
					if (this.Rival.CharacterAnimation["f02_" + this.Weekday + "_3" + this.Suffix].time > this.TransferTime)
					{
						this.Rival.EventBook.SetActive(false);
						this.Senpai.EventBook.SetActive(true);
						this.Transfer = false;
						this.Return = true;
					}
				}
				else if (this.EventDay == DayOfWeek.Wednesday)
				{
					if (this.Rival.CharacterAnimation["f02_" + this.Weekday + "_3" + this.Suffix].time > this.TransferTime)
					{
						this.Rival.SmartPhone.SetActive(false);
						this.Senpai.SmartPhone.SetActive(true);
						this.Transfer = false;
						this.Return = true;
					}
				}
			}

			if (this.Return)
			{
				if (this.EventDay == DayOfWeek.Wednesday)
				{
					if (this.Rival.CharacterAnimation["f02_" + this.Weekday + "_3" + this.Suffix].time > this.ReturnTime)
					{
						this.Rival.SmartPhone.SetActive(true);
						this.Senpai.SmartPhone.SetActive(false);
						this.Return = false;
					}
				}
			}

			////////////////////////////////////////////
			///// ACTIONS THAT COULD END THE EVENT /////
			////////////////////////////////////////////

			if (this.Senpai.Alarmed || this.Rival.Alarmed)
			{
				GameObject NewAlarmDisc = Instantiate(this.AlarmDisc,
					this.Yandere.transform.position + Vector3.up, Quaternion.identity);
				NewAlarmDisc.GetComponent<AlarmDiscScript>().NoScream = true;

				this.EndEvent();
			}
		}

		/////////////////////////
		///// SUBTITLE SIZE /////
		/////////////////////////

		if (this.Phase > 0 || this.Impatient)
		{
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

				// [af] Replaced if/else statement with boolean expression.
				this.Yandere.Eavesdropping = this.Distance < 5.0f;
			}
			else
			{
				this.EventSubtitle.transform.localScale = Vector3.zero;

				if (this.VoiceClip != null)
				{
					this.VoiceClip.GetComponent<AudioSource>().volume = 0.0f;
				}
			}
		}

		if (this.Phase > 0)
		{
			if (this.Friend != null)
			{
				if (this.Friend.DistanceToDestination < 1)
				{
					this.Friend.CharacterAnimation.CrossFade(AnimNames.FemaleCornerPeek);
					this.Friend.MoveTowardsTarget(this.Friend.CurrentDestination.position);

					this.Friend.targetRotation = this.Friend.CurrentDestination.rotation;

					this.Friend.transform.rotation = Quaternion.Slerp(
						this.Friend.transform.rotation, this.Friend.targetRotation, 10.0f * Time.deltaTime);

					this.Friend.MyController.radius = 0;
				}
			}
			else
			{
				if (this.StudentManager.Students[this.FriendID] != null &&
					!PlayerGlobals.RaibaruLoner)
				{
					this.Friend = this.StudentManager.Students[this.FriendID];
				}	
			}
		}
	}

	void EndEvent()
	{
		//Debug.Log("After Class event ended.");

		if (this.VoiceClip != null)
		{
			Destroy(this.VoiceClip);
		}

		if (this.Senpai.InEvent)
		{
			EndSenpai ();
		}

		if (!this.Rival.Ragdoll.Zs.activeInHierarchy)
		{
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
			//this.Rival.Phase++;

			this.Rival.CurrentDestination = this.Rival.Destinations[this.Rival.Phase];
			this.Rival.Pathfinding.target = this.Rival.Destinations[this.Rival.Phase];
			this.Rival.DistanceToDestination = 100.0f;
			this.Rival.Pathfinding.speed = 1.0f;
			this.Rival.Hurry = false;
		}

		if (this.Friend != null)
		{
			if (!this.Friend.Alarmed && !this.Friend.DramaticReaction)
			{
				this.Friend.Pathfinding.canSearch = true;
				this.Friend.Pathfinding.canMove = true;
			}

			if (this.NaturalEnd)
			{
				this.Friend.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
				this.Friend.Pathfinding.target = this.Rival.transform;
				this.Friend.CurrentDestination = this.Rival.transform;
				this.Friend.Pathfinding.canSearch = true;
				this.Friend.Pathfinding.canMove = true;
				this.Friend.MyController.radius = .1f;
				this.Friend.ManualRotation = false;
				this.Friend.Prompt.enabled = true;
				this.Friend.InEvent = false;
				this.Friend.Private = false;
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

		if (this.Sabotaged)
		{
			this.Rival.WalkAnim = AnimNames.FemaleSadWalk;

			DatingGlobals.RivalSabotaged++;
			Debug.Log("Sabotage Progress: " + DatingGlobals.RivalSabotaged + "/5");
		}

		this.Yandere.Eavesdropping = false;
		this.EventSubtitle.text = string.Empty;
		this.enabled = false;

		this.Jukebox.Dip = 1;
	}

	void EndSenpai()
	{
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
		//this.Senpai.Phase++;

		this.Senpai.CurrentDestination = this.Senpai.Destinations[this.Senpai.Phase];
		this.Senpai.Pathfinding.target = this.Senpai.Destinations[this.Senpai.Phase];
		this.Senpai.DistanceToDestination = 100.0f;
	}
#endif
}
