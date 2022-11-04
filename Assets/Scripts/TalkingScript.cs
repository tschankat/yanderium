using UnityEngine;

public class TalkingScript : MonoBehaviour
{
	// [af] Some reusable values found through TalkingScript.
	const float LongestTime = 100.0f;
	const float LongTime = 5.0f;
	const float MediumTime = 3.0f;
	const float ShortTime = 2.0f;

	public StudentScript S;

	public WeaponScript StuckBoxCutter;

	public bool NegativeResponse = false;
	public bool Follow = false;
	public bool Grudge = false;
	public bool Refuse = false;
	public bool Fake = false;

	public string IdleAnim = "";

	public int ClubBonus;

	void Update()
	{
		if (this.S.Talking)
		{
			if (this.S.Sleuthing)
			{
				this.ClubBonus = 5;
			}
			else
			{
				this.ClubBonus = 0;
			}

			if (GameGlobals.EmptyDemon){ClubBonus = (int)S.Club * -1;}

			if (this.S.Interaction == StudentInteractionType.Idle)
			{
				if (!this.Fake)
				{
					//If this character is "Sleuthing"...
					if (this.S.Sleuthing)
					{
						this.IdleAnim = this.S.SleuthCalmAnim;
					}
					//If this is Geiju...
					else if (this.S.Club == ClubType.Art && this.S.DialogueWheel.ClubLeader && this.S.Paintbrush.activeInHierarchy)
					{
						this.IdleAnim = "paintingIdle_00";
					}
					//If this character is not a bully...
					else if (this.S.Club != ClubType.Bully)
					{
						this.IdleAnim = this.S.IdleAnim;
					}
					//If this character is a bully...
					else
					{
						if (this.S.StudentManager.Reputation.Reputation < 33.33333f || this.S.Persona == PersonaType.Coward)
						{
							if (this.S.CurrentAction == StudentActionType.Sunbathe && this.S.SunbathePhase > 2)
							{
								this.IdleAnim = this.S.OriginalIdleAnim;
							}
							else
							{
								this.IdleAnim = this.S.IdleAnim;
							}
						}
						else
						{
							this.IdleAnim = this.S.CuteAnim;
						}
					}

					this.S.CharacterAnimation.CrossFade(this.IdleAnim);
				}
				else
				{
					if (this.IdleAnim != "")
					{
						this.S.CharacterAnimation.CrossFade(this.IdleAnim);
					}
				}

				if (this.S.TalkTimer == 0.0f)
				{
					if (!this.S.DialogueWheel.AppearanceWindow.Show)
					{
						this.S.DialogueWheel.Impatience.fillAmount += Time.deltaTime * 0.10f;
					}

					if (this.S.DialogueWheel.Impatience.fillAmount > 0.50f)
					{
						if (this.S.Subtitle.Timer == 0.0f)
						{
							//Geiju
							if (this.S.StudentID == 41)
							{
								this.S.Subtitle.UpdateLabel(SubtitleType.Impatience, 4, 5.0f);
							}
							else if (this.S.Pestered == 0)
							{
								this.S.Subtitle.UpdateLabel(SubtitleType.Impatience, 0, 5.0f);
							}
							else
							{
								this.S.Subtitle.UpdateLabel(SubtitleType.Impatience, 2, 5.0f);
							}
						}
					}

					if (this.S.DialogueWheel.Impatience.fillAmount == 1.0f)
					{
						if (this.S.DialogueWheel.Show)
						{
							//Geiju
							if (this.S.StudentID == 41)
							{
								this.S.Subtitle.UpdateLabel(SubtitleType.Impatience, 4, 5.0f);
							}
							else if (this.S.Pestered == 0)
							{
								this.S.Subtitle.UpdateLabel(SubtitleType.Impatience, 1, 5.0f);
							}
							else
							{
								this.S.Subtitle.UpdateLabel(SubtitleType.Impatience, 3, 5.0f);
							}

							this.S.WaitTimer = 0.0f;
							this.S.Pestered += 5;

							this.S.DialogueWheel.Pestered = true;
							this.S.DialogueWheel.End();
						}
					}
				}
			}

			/////////////////////////////
			///// ACCEPTING APOLOGY /////
			/////////////////////////////

			else if (this.S.Interaction == StudentInteractionType.Forgiving)
			{
				if (this.S.TalkTimer == MediumTime)
				{
					if (this.S.Club != ClubType.Delinquent)
					{
						this.S.CharacterAnimation.CrossFade(this.S.Nod2Anim);

						this.S.RepRecovery = 5.0f;

						if (PlayerGlobals.PantiesEquipped == 6)
						{
							this.S.RepRecovery += 2.50f;
						}
						if (this.S.Yandere.Class.SocialBonus > 0)
						{
							this.S.RepRecovery += 2.50f;
						}

						this.S.PendingRep += this.S.RepRecovery;

						this.S.Reputation.PendingRep += this.S.RepRecovery;

						// Maybe the student's ID shouldn't be used as the index variable.
						for (this.S.ID = 0; this.S.ID < this.S.Outlines.Length; this.S.ID++)
						{
							this.S.Outlines[this.S.ID].color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
						}

						this.S.Forgave = true;

						if (this.S.Witnessed == StudentWitnessType.Insanity ||
							this.S.Witnessed == StudentWitnessType.WeaponAndBloodAndInsanity ||
							this.S.Witnessed == StudentWitnessType.WeaponAndInsanity ||
							this.S.Witnessed == StudentWitnessType.BloodAndInsanity)
						{
							this.S.Subtitle.UpdateLabel(SubtitleType.ForgivingInsanity, 0, 3.0f);
						}
						else if (this.S.Witnessed == StudentWitnessType.Accident)
						{
							this.S.Subtitle.UpdateLabel(SubtitleType.ForgivingAccident, 0, 5.0f);
						}
						else
						{
							this.S.Subtitle.UpdateLabel(SubtitleType.Forgiving, 0, 3.0f);
						}
					}
					else
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.Dismissive, 0, 5.0f);
					}
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.S.TalkTimer = 0.0f;
					}

					if (this.S.CharacterAnimation[this.S.Nod2Anim].time >=
						this.S.CharacterAnimation[this.S.Nod2Anim].length)
					{
						this.S.CharacterAnimation.CrossFade(this.IdleAnim);
					}

					if (this.S.TalkTimer <= 0.0f)
					{
						this.S.IgnoreTimer = 5.0f;
						this.S.DialogueWheel.End();
					}
				}

				this.S.TalkTimer -= Time.deltaTime;
			}

			//////////////////////////////
			///// BEING COMPLIMENTED /////
			//////////////////////////////

			else if (this.S.Interaction == StudentInteractionType.ReceivingCompliment)
			{
				if (this.S.TalkTimer == MediumTime)
				{
					this.S.Subtitle.PersonaSubtitle.UpdateLabel(S.Persona, S.Reputation.Reputation, 5);

					if (this.S.Club != ClubType.Delinquent)
					{
                        this.S.CharacterAnimation.CrossFade(this.S.LookDownAnim);
                        this.CalculateRepBonus();

						this.S.Reputation.PendingRep += 1.0f + this.S.RepBonus;
						this.S.PendingRep += 1.0f + this.S.RepBonus;
					}

					this.S.Complimented = true;
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.S.TalkTimer = 0.0f;
					}
				}

				this.S.TalkTimer -= Time.deltaTime;

				if (this.S.TalkTimer <= 0.0f)
				{
					this.S.DialogueWheel.End();
				}
			}

			/////////////////////
			///// GOSSIPING /////
			/////////////////////

			else if (this.S.Interaction == StudentInteractionType.Gossiping)
			{
				if (this.S.TalkTimer == MediumTime)
				{
					if (this.S.Club != ClubType.Delinquent)
					{
						this.S.CharacterAnimation.CrossFade(this.S.GossipAnim);

						this.S.Subtitle.UpdateLabel(SubtitleType.StudentGossip, 0, 3.0f);

						this.S.GossipBonus = 0;

						if (this.S.Reputation.Reputation > 33.33333f)
						{
							this.S.GossipBonus++;
						}

						if (PlayerGlobals.PantiesEquipped == 9)
						{
							this.S.GossipBonus++;
						}

						if (SchemeGlobals.DarkSecret && this.S.DialogueWheel.Victim == this.S.StudentManager.RivalID)
						{
							this.S.GossipBonus++;
						}

						if (PlayerGlobals.GetStudentFriend(this.S.StudentID))
						{
							this.S.GossipBonus++;
						}

						if (this.S.Male && (this.S.Yandere.Class.Seduction + this.S.Yandere.Class.SeductionBonus) > 0 ||
                            this.S.Yandere.Class.Seduction == 5)
						{
							this.S.GossipBonus++;
						}

						if (this.S.Yandere.Class.SocialBonus > 0)
						{
							this.S.GossipBonus++;
						}

						StudentGlobals.SetStudentReputation(this.S.DialogueWheel.Victim,
							StudentGlobals.GetStudentReputation(this.S.DialogueWheel.Victim) - (1 + this.S.GossipBonus));

						if (this.S.Club != ClubType.Bully)
						{
							this.S.Reputation.PendingRep -= 2.0f;
							this.S.PendingRep -= 2.0f;
						}

						this.S.Gossiped = true;

						this.S.Yandere.NotificationManager.TopicName = "Gossip";

						if (!ConversationGlobals.GetTopicDiscovered(19))
						{
							this.S.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
							ConversationGlobals.SetTopicDiscovered(19, true);
						}

						if (!ConversationGlobals.GetTopicLearnedByStudent(19, this.S.StudentID))
						{
							this.S.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
							ConversationGlobals.SetTopicLearnedByStudent(19, this.S.StudentID, true);
						}
					}
					else
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.Dismissive, 2, 3.0f);
					}
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.S.TalkTimer = 0.0f;
					}

					if (this.S.CharacterAnimation[this.S.GossipAnim].time >=
						this.S.CharacterAnimation[this.S.GossipAnim].length)
					{
						this.S.CharacterAnimation.CrossFade(this.IdleAnim);
					}

					if (this.S.TalkTimer <= 0.0f)
					{
						this.S.DialogueWheel.End();
					}
				}

				this.S.TalkTimer -= Time.deltaTime;
			}

			///////////////
			///// BYE /////
			///////////////

			else if (this.S.Interaction == StudentInteractionType.Bye)
			{
				if (this.S.TalkTimer == ShortTime)
				{
					if (this.S.Club != ClubType.Delinquent)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.StudentFarewell, 0, 2.0f);
					}
					else
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.Dismissive, 3, 3.0f);
					}
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.S.TalkTimer = 0.0f;
					}
				}

				this.S.CharacterAnimation.CrossFade(this.IdleAnim);

				this.S.TalkTimer -= Time.deltaTime;

				if (this.S.TalkTimer <= 0.0f)
				{
					this.S.Pestered += 2;
					this.S.DialogueWheel.End();
				}
			}

			///////////////////////
			///// GIVING TASK /////
			///////////////////////

			else if (this.S.Interaction == StudentInteractionType.GivingTask)
			{
				//Debug.Log("We're in the block of code where a student describes a task.");

				if (this.S.TalkTimer == LongestTime)
				{
					//Debug.Log("Student is beginning to describe a task.");

					this.S.Subtitle.UpdateLabel(this.S.TaskLineResponseType,
						this.S.TaskPhase, this.S.Subtitle.GetClipLength(this.S.StudentID, this.S.TaskPhase));

					this.S.CharacterAnimation.CrossFade(this.S.TaskAnims[this.S.TaskPhase]);
					this.S.CurrentAnim = this.S.TaskAnims[this.S.TaskPhase];
					this.S.TalkTimer = this.S.Subtitle.GetClipLength(this.S.StudentID, this.S.TaskPhase);
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.S.Subtitle.Label.text = string.Empty;
						Destroy(this.S.Subtitle.CurrentClip);
						this.S.TalkTimer = 0.0f;
					}
				}

				if (this.S.CharacterAnimation[this.S.CurrentAnim].time >=
					this.S.CharacterAnimation[this.S.CurrentAnim].length)
				{
					this.S.CharacterAnimation.CrossFade(this.IdleAnim);
				}

				this.S.TalkTimer -= Time.deltaTime;

				if (this.S.TalkTimer <= 0.0f)
				{
					// Task complete.
					if (this.S.TaskPhase == 5)
					{
						this.S.DialogueWheel.TaskWindow.TaskComplete = true;
						TaskGlobals.SetTaskStatus(this.S.StudentID, 3);
						PlayerGlobals.SetStudentFriend(this.S.StudentID, true);
						this.S.Police.EndOfDay.NewFriends++;
						this.S.Interaction = 0;

                        if (this.S.Club != ClubType.Delinquent)
                        {
    						this.CalculateRepBonus();

						    this.S.Reputation.PendingRep += 1.0f + this.S.RepBonus;
						    this.S.PendingRep += 1.0f + this.S.RepBonus;
                        }
                    }
					// Yandere-chan has confirmed or denied.
					else if ((this.S.TaskPhase == 4) || (this.S.TaskPhase == 0))
					{
						this.S.StudentManager.TaskManager.UpdateTaskStatus();
						this.S.DialogueWheel.End();
					}
					// Asking Yandere-chan to confirm or deny.
					else if (this.S.TaskPhase == 3)
					{
						this.S.DialogueWheel.TaskWindow.UpdateWindow(this.S.StudentID);
						this.S.Interaction = 0;
					}
					// Explaining Task.
					else
					{
						//Debug.Log("Student is continuing to describe a task.");

						this.S.TaskPhase++;
						this.S.Subtitle.UpdateLabel(this.S.TaskLineResponseType,
							this.S.TaskPhase, this.S.Subtitle.GetClipLength(this.S.StudentID, this.S.TaskPhase));
                        this.S.Subtitle.Timer = 0;

                        this.S.CharacterAnimation.CrossFade(this.S.TaskAnims[this.S.TaskPhase]);
						this.S.CurrentAnim = this.S.TaskAnims[this.S.TaskPhase];
						this.S.TalkTimer = this.S.Subtitle.GetClipLength(this.S.StudentID, this.S.TaskPhase);
					}
				}
			}

			/////////////////////////
			///// FOLLOW PLAYER /////
			/////////////////////////

			else if (this.S.Interaction == StudentInteractionType.FollowingPlayer)
			{
				if (this.S.TalkTimer == ShortTime)
				{
					if (this.S.Club != ClubType.Delinquent)
					{
						if (this.S.Clock.HourTime > 8.0f &&
							this.S.Clock.HourTime < 13.0f ||
							this.S.Clock.HourTime > 13.375f &&
							this.S.Clock.HourTime < 15.50f ||
                            this.S.StudentID == this.S.StudentManager.RivalID)
						{
							this.S.CharacterAnimation.CrossFade(this.S.GossipAnim);
							this.NegativeResponse = true;

                            if (this.S.StudentID == this.S.StudentManager.RivalID)
                            {
                                this.S.Subtitle.UpdateLabel(SubtitleType.StudentStay, 2, 5.0f);
                            }
                            else
                            {
                                this.S.Subtitle.UpdateLabel(SubtitleType.StudentStay, 0, 5.0f);
                            }
                        }
						else if (this.S.StudentManager.LockerRoomArea.bounds.Contains(S.Yandere.transform.position) ||
								 this.S.StudentManager.WestBathroomArea.bounds.Contains(S.Yandere.transform.position) ||
								 this.S.StudentManager.EastBathroomArea.bounds.Contains(S.Yandere.transform.position) ||
								 this.S.StudentManager.HeadmasterArea.bounds.Contains(S.Yandere.transform.position) ||
								 this.S.MyRenderer.sharedMesh == this.S.SchoolSwimsuit ||
								 this.S.MyRenderer.sharedMesh == this.S.SwimmingTrunks)
						{
							this.S.CharacterAnimation.CrossFade(this.S.GossipAnim);
							this.S.Subtitle.UpdateLabel(SubtitleType.StudentStay, 1, 5.0f);
							this.NegativeResponse = true;
						}
						else
						{
							int TempID = 0;if (this.S.Yandere.Club == ClubType.Delinquent)
							{
								this.S.Reputation.PendingRep -= 10.0f;
								this.S.PendingRep -= 10.0f;
								TempID++;
							}

							this.S.CharacterAnimation.CrossFade(this.S.Nod1Anim);
							this.S.Subtitle.UpdateLabel(SubtitleType.StudentFollow, TempID, 2.0f);
							this.Follow = true;
						}
					}
					else
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.Dismissive, 4, 5.0f);
					}
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.S.TalkTimer = 0.0f;
					}

					if (this.S.CharacterAnimation[this.S.Nod1Anim].time >=
						this.S.CharacterAnimation[this.S.Nod1Anim].length)
					{
						this.S.CharacterAnimation.CrossFade(this.IdleAnim);
					}

					if (this.S.TalkTimer <= 0.0f)
					{
						this.S.DialogueWheel.End();

						if (this.Follow)
						{
							this.S.Pathfinding.target = this.S.Yandere.transform;
							this.S.Prompt.Label[0].text = "     " + "Stop";

							//Kokona
							if (this.S.StudentID == 30)
							{
								this.S.StudentManager.FollowerLookAtTarget.position =
									this.S.DefaultTarget.position;
								this.S.StudentManager.LoveManager.Follower = this.S;
							}

							this.S.FollowCountdown.Sprite.fillAmount = 1;

							//If Yandere-chan is not a delinquent...
							if (this.S.Yandere.Club != ClubType.Delinquent)
							{
								this.S.FollowCountdown.Speed = 1.0f / (35.0f + (this.S.Reputation.Reputation * .25f));
							}
							else
							{
								this.S.FollowCountdown.Speed = 1.0f / (35.0f + (this.S.Reputation.Reputation * -.25f));
							}

							Debug.Log("Reputation is: " + this.S.Reputation.Reputation + " and Countdown Speed is: " + this.S.FollowCountdown.Speed);

                            this.S.FollowCountdown.gameObject.SetActive(true);
							this.S.Yandere.Follower = this.S;
							this.S.Yandere.Followers++;
							this.S.Following = true;
							this.S.Hurry = false;

                            this.S.StudentManager.InterestManager.FollowerID = this.S.StudentID;
                            this.S.StudentManager.InterestManager.UpdateIgnore();
                        }

						this.Follow = false;
					}
				}

				this.S.TalkTimer -= Time.deltaTime;
			}

			///////////////////
			///// GO AWAY /////
			///////////////////

			else if (this.S.Interaction == StudentInteractionType.GoingAway)
			{
				if (this.S.TalkTimer == MediumTime)
				{
					if (this.S.Club != ClubType.Delinquent)
					{
						if ((this.S.Clock.HourTime > 8.0f) &&
							(this.S.Clock.HourTime < 13.0f) ||
							(this.S.Clock.HourTime > 13.375f) &&
							(this.S.Clock.HourTime < 15.50f))
						{
							this.S.CharacterAnimation.CrossFade(this.S.GossipAnim);
							this.S.Subtitle.UpdateLabel(SubtitleType.StudentStay, 0, 5.0f);
						}
						else
						{
							int TempID = 0;if (this.S.Yandere.Club == ClubType.Delinquent)
							{
								this.S.Reputation.PendingRep -= 10.0f;
								this.S.PendingRep -= 10.0f;
								TempID++;
							}

							this.S.CharacterAnimation.CrossFade(this.S.Nod1Anim);
							this.S.Subtitle.UpdateLabel(SubtitleType.StudentLeave, TempID, 3.0f);
							this.S.GoAway = true;
						}
					}
					else
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.Dismissive, 5, 5.0f);
					}
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.S.TalkTimer = 0.0f;
					}

					if (this.S.CharacterAnimation[this.S.Nod1Anim].time >=
						this.S.CharacterAnimation[this.S.Nod1Anim].length)
					{
						this.S.CharacterAnimation.CrossFade(this.IdleAnim);
					}

					if (this.S.TalkTimer <= 0.0f)
					{
						this.S.DialogueWheel.End();
					}
				}

				this.S.TalkTimer -= Time.deltaTime;
			}

			/////////////////////////
			///// DISTRACT THEM /////
			/////////////////////////

			else if (this.S.Interaction == StudentInteractionType.DistractingTarget)
			{
				if (this.S.TalkTimer == MediumTime)
				{
					if (this.S.Club != ClubType.Delinquent)
					{
						if ((this.S.Clock.HourTime > 8.0f) &&
							(this.S.Clock.HourTime < 13.0f) ||
							(this.S.Clock.HourTime > 13.375f) &&
							(this.S.Clock.HourTime < 15.50f))
						{
							this.S.CharacterAnimation.CrossFade(this.S.GossipAnim);
							this.S.Subtitle.UpdateLabel(SubtitleType.StudentStay, 0, 5.0f);
						}
						else
						{
							StudentScript victim = this.S.StudentManager.Students[this.S.DialogueWheel.Victim];

							this.Grudge = false;

							if (victim.Club == ClubType.Delinquent ||
								this.S.Bullied && victim.Club == ClubType.Bully ||
								victim.StudentID == 36 && TaskGlobals.GetTaskStatus(36) < 3)
							{
								this.Grudge = true;
							}

							if (victim.Routine && !victim.TargetedForDistraction &&
							   !victim.InEvent && !this.Grudge && victim.Indoors &&
							    victim.gameObject.activeInHierarchy && victim.ClubActivityPhase < 16 &&
								victim.CurrentAction != StudentActionType.Sunbathe &&
							    victim.FollowTarget == null)

							//It's likely that the below code can be completely deleted...but I'm not sure yet.
							//We'll wait for bug reports and then make a decision.

							/*
							!victim.InEvent && !victim.Slave && !victim.Wet &&
							!victim.Meeting && !victim.TargetedForDistraction &&
							!victim.Following && !victim.TurnOffRadio && !victim.GoAway)
							*/
							{
								int TempID = 0;if (this.S.Yandere.Club == ClubType.Delinquent)
								{
									this.S.Reputation.PendingRep -= 10.0f;
									this.S.PendingRep -= 10.0f;
									TempID++;
								}

								this.S.CharacterAnimation.CrossFade(this.S.Nod1Anim);
								this.S.Subtitle.UpdateLabel(SubtitleType.StudentDistract, TempID, 3.0f);
								this.Refuse = false;
							}
							else
							{
								this.S.CharacterAnimation.CrossFade(this.S.GossipAnim);

								if (this.Grudge)
								{
									this.S.Subtitle.UpdateLabel(SubtitleType.StudentDistractBullyRefuse, 0, 3.0f);
								}
								else
								{
									this.S.Subtitle.UpdateLabel(SubtitleType.StudentDistractRefuse, 0, 3.0f);
								}

								this.Refuse = true;
							}
						}
					}
					else
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.Dismissive, 6, 5.0f);
						this.Refuse = true;
					}
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.S.TalkTimer = 0.0f;
					}

					if (this.S.CharacterAnimation[this.S.Nod1Anim].time >=
						this.S.CharacterAnimation[this.S.Nod1Anim].length)
					{
						this.S.CharacterAnimation.CrossFade(this.IdleAnim);
					}

					if (this.S.TalkTimer <= 0.0f)
					{
						this.S.DialogueWheel.End();

						if (!this.Refuse)
						{
							if ((this.S.Clock.HourTime < 8.0f) ||
								(this.S.Clock.HourTime > 13.0f) &&
								(this.S.Clock.HourTime < 13.375f) ||
								(this.S.Clock.HourTime > 15.50f))
							{
								if (!this.S.Distracting)
								{
									this.S.DistractionTarget = this.S.StudentManager.Students[this.S.DialogueWheel.Victim];
									this.S.DistractionTarget.TargetedForDistraction = true;

									this.S.CurrentDestination = this.S.DistractionTarget.transform;
									this.S.Pathfinding.target = this.S.DistractionTarget.transform;

									this.S.Pathfinding.speed = 4.0f;
									this.S.TargetDistance = 1.0f;
									this.S.DistractTimer = 10.0f;

									this.S.Distracting = true;
									this.S.Routine = false;
									this.S.CanTalk = false;
								}
							}
						}
					}
				}

				this.S.TalkTimer -= Time.deltaTime;
			}

			//////////////////
			///// GRUDGE /////
			//////////////////

			else if (this.S.Interaction == StudentInteractionType.PersonalGrudge)
			{
				if (this.S.TalkTimer == LongTime)
				{
					if (this.S.Persona == PersonaType.Coward || this.S.Persona == PersonaType.Fragile)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.CowardGrudge, 0, 5.0f);
						this.S.CharacterAnimation.CrossFade(this.S.CowardAnim);
						this.S.TalkTimer = 5.0f;
					}
					/*
					else if (this.S.Persona == PersonaType.Spiteful)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.EvilGrudge, 0, 5.0f);
						this.S.CharacterAnimation.CrossFade(this.S.EvilAnim);
						this.S.TalkTimer = 5.0f;
					}
					*/
					else
					{
						if (!this.S.Male)
						{
							this.S.Subtitle.UpdateLabel(SubtitleType.GrudgeWarning, 0, 99.0f);
						}
						else
						{
							if (this.S.Club == ClubType.Delinquent)
							{
								this.S.Subtitle.UpdateLabel(SubtitleType.DelinquentGrudge, 1, 99.0f);
							}
							else
							{
								this.S.Subtitle.UpdateLabel(SubtitleType.GrudgeWarning, 1, 99.0f);
							}
						}

						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
						this.S.CharacterAnimation.CrossFade(this.S.GrudgeAnim);
					}
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.S.TalkTimer = 0.0f;
					}

					if (this.S.TalkTimer <= 0.0f)
					{
						this.S.DialogueWheel.End();
					}
				}

				this.S.TalkTimer -= Time.deltaTime;
			}

			/////////////////////
			///// CLUB INFO /////
			/////////////////////

			else if (this.S.Interaction == StudentInteractionType.ClubInfo)
			{
				if (this.S.TalkTimer == LongestTime)
				{
					this.S.Subtitle.UpdateLabel(this.S.ClubInfoResponseType,
						this.S.ClubPhase, 99.0f);
					this.S.TalkTimer = this.S.Subtitle.GetClubClipLength(this.S.Club, this.S.ClubPhase);
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.S.Subtitle.Label.text = string.Empty;
						Destroy(this.S.Subtitle.CurrentClip);
						this.S.TalkTimer = 0.0f;
					}
				}

				this.S.TalkTimer -= Time.deltaTime;

				if (this.S.TalkTimer <= 0.0f)
				{
					// Return to Dialogue Wheel.
					if (this.S.ClubPhase == 3)
					{
						this.S.DialogueWheel.Panel.enabled = true;
						this.S.DialogueWheel.Show = true;
						this.S.Subtitle.Label.text = string.Empty;
						this.S.Interaction = 0;
						this.S.TalkTimer = 0.0f;
					}
					// Move To Next Line Of Info.
					else
					{
						this.S.ClubPhase++;
						this.S.Subtitle.UpdateLabel(this.S.ClubInfoResponseType,
							this.S.ClubPhase, 99.0f);
						this.S.TalkTimer = this.S.Subtitle.GetClubClipLength(this.S.Club, this.S.ClubPhase);
					}
				}
			}

			/////////////////////
			///// CLUB JOIN /////
			/////////////////////

			else if (this.S.Interaction == StudentInteractionType.ClubJoin)
			{
				if (this.S.TalkTimer == LongestTime)
				{
					if (this.S.ClubPhase == 1)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubJoin, (int)this.S.Club + this.ClubBonus, 99.0f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
					else if (this.S.ClubPhase == 2)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubAccept, (int)this.S.Club + this.ClubBonus, 99.0f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
					else if (this.S.ClubPhase == 3)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubRefuse, (int)this.S.Club + this.ClubBonus, 99.0f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
					else if (this.S.ClubPhase == 4)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubRejoin, (int)this.S.Club + this.ClubBonus, 99.0f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
					else if (this.S.ClubPhase == 5)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubExclusive, (int)this.S.Club + this.ClubBonus, 99.0f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
					else if (this.S.ClubPhase == 6)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubGrudge, (int)this.S.Club + this.ClubBonus, 99.0f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.S.Subtitle.Label.text = string.Empty;
						Destroy(this.S.Subtitle.CurrentClip);
						this.S.TalkTimer = 0.0f;
					}
				}

				this.S.TalkTimer -= Time.deltaTime;

				if (this.S.TalkTimer <= 0.0f)
				{
					if (this.S.ClubPhase == 1)
					{
						this.S.DialogueWheel.ClubWindow.Club = this.S.Club;
						this.S.DialogueWheel.ClubWindow.UpdateWindow();

						this.S.Subtitle.Label.text = string.Empty;
						this.S.Interaction = 0;
					}
					else
					{
						this.S.DialogueWheel.End();

						if (this.S.Club == ClubType.MartialArts)
						{
							this.S.ChangingBooth.CheckYandereClub();
						}
					}
				}
			}

			/////////////////////
			///// CLUB QUIT /////
			/////////////////////

			else if (this.S.Interaction == StudentInteractionType.ClubQuit)
			{
				if (this.S.TalkTimer == LongestTime)
				{
					if (this.S.ClubPhase == 1)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubQuit, (int)this.S.Club + this.ClubBonus, 99.0f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
					else if (this.S.ClubPhase == 2)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubConfirm, (int)this.S.Club + this.ClubBonus, 99.0f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
					else if (this.S.ClubPhase == 3)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubDeny, (int)this.S.Club + this.ClubBonus, 99.0f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.S.Subtitle.Label.text = string.Empty;
						Destroy(this.S.Subtitle.CurrentClip);
						this.S.TalkTimer = 0.0f;
					}
				}

				this.S.TalkTimer -= Time.deltaTime;

				if (this.S.TalkTimer <= 0.0f)
				{
					if (this.S.ClubPhase == 1)
					{
						this.S.DialogueWheel.ClubWindow.Club = this.S.Club;
						this.S.DialogueWheel.ClubWindow.Quitting = true;
						this.S.DialogueWheel.ClubWindow.UpdateWindow();
						this.S.Subtitle.Label.text = string.Empty;
						this.S.Interaction = 0;
					}
					else
					{
						this.S.DialogueWheel.End();

						if (this.S.Club == ClubType.MartialArts)
						{
							this.S.ChangingBooth.CheckYandereClub();
						}

						//Quitting Clubs
						if (this.S.ClubPhase == 2)
						{
							/*
							if (this.S.Club == ClubType.Gaming)
							{
								this.S.StudentManager.ComputerGames.DeactivateAllBenefits();
							}
							*/
						}
					}
				}
			}

			////////////////////
			///// CLUB BYE /////
			////////////////////

			else if (this.S.Interaction == StudentInteractionType.ClubBye)
			{
				if (this.S.TalkTimer == this.S.Subtitle.ClubFarewellClips[(int)this.S.Club + this.ClubBonus].length)
				{
					this.S.Subtitle.UpdateLabel(SubtitleType.ClubFarewell, (int)this.S.Club + this.ClubBonus,
						this.S.Subtitle.ClubFarewellClips[(int)this.S.Club + this.ClubBonus].length);
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.S.TalkTimer = 0.0f;
					}
				}

				this.S.TalkTimer -= Time.deltaTime;

				if (this.S.TalkTimer <= 0.0f)
				{
					this.S.DialogueWheel.End();
				}
			}

			/////////////////////////
			///// CLUB ACTIVITY /////
			/////////////////////////

			else if (this.S.Interaction == StudentInteractionType.ClubActivity)
			{
				//Debug.Log("ClubBonus is: " + this.ClubBonus);

				if (this.S.TalkTimer == LongestTime)
				{
					//Debug.Log("Club Bonus is: " + this.ClubBonus);

					//Debug.Log("Club plus Club Bonus is: " + ((int)this.S.Club + this.ClubBonus));

					if (this.S.ClubPhase == 1)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubActivity, ((int)this.S.Club + this.ClubBonus), 99.0f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
					else if (this.S.ClubPhase == 2)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubYes, ((int)this.S.Club + this.ClubBonus), 99.0f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
					else if (this.S.ClubPhase == 3)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubNo, ((int)this.S.Club + this.ClubBonus), 99.0f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
					else if (this.S.ClubPhase == 4)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubEarly, ((int)this.S.Club + this.ClubBonus), 99.0f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
					else if (this.S.ClubPhase == 5)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubLate, ((int)this.S.Club + this.ClubBonus), 99.0f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.S.Subtitle.Label.text = string.Empty;
						Destroy(this.S.Subtitle.CurrentClip);
						this.S.TalkTimer = 0.0f;
					}
				}

				this.S.TalkTimer -= Time.deltaTime;

				if (this.S.TalkTimer <= 0.0f)
				{
					if (this.S.ClubPhase == 1)
					{
						this.S.DialogueWheel.ClubWindow.Club = this.S.Club;
						this.S.DialogueWheel.ClubWindow.Activity = true;
						this.S.DialogueWheel.ClubWindow.UpdateWindow();

						this.S.Subtitle.Label.text = string.Empty;
						this.S.Interaction = 0;
					}
					else if (this.S.ClubPhase == 2)
					{
						this.S.Police.Darkness.enabled = true;
						this.S.Police.ClubActivity = true;
						this.S.Police.FadeOut = true;

						this.S.Subtitle.Label.text = string.Empty;
						this.S.Interaction = 0;
					}
					else
					{
						this.S.DialogueWheel.End();
					}
				}
			}

			//////////////////////////
			///// CLUB UNWELCOME /////
			//////////////////////////

			else if (this.S.Interaction == StudentInteractionType.ClubUnwelcome)
			{
				this.S.CharacterAnimation.CrossFade(this.S.IdleAnim);

				if (this.S.TalkTimer == LongTime)
				{
					this.S.Subtitle.UpdateLabel(SubtitleType.ClubUnwelcome, (int)this.S.Club + this.ClubBonus, 99.0f);
					this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.S.TalkTimer = 0.0f;
					}

					if (this.S.TalkTimer <= 0.0f)
					{
						this.S.DialogueWheel.End();
					}
				}

				this.S.TalkTimer -= Time.deltaTime;
			}

			/////////////////////
			///// CLUB KICK /////
			/////////////////////

			else if (this.S.Interaction == StudentInteractionType.ClubKick)
			{
				this.S.CharacterAnimation.CrossFade(this.S.IdleAnim);

				if (this.S.TalkTimer == LongTime)
				{
					this.S.Subtitle.UpdateLabel(SubtitleType.ClubKick, (int)this.S.Club + this.ClubBonus, 99.0f);
					this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.S.TalkTimer = 0.0f;
					}

					if (this.S.TalkTimer <= 0.0f)
					{
						S.ClubManager.DeactivateClubBenefit();
                        this.S.Yandere.Club = ClubType.None;
						this.S.DialogueWheel.End();

						this.S.Yandere.ClubAccessory();
					}
				}

				this.S.TalkTimer -= Time.deltaTime;
			}

			////////////////////////
			///// CLUB GRUDGE  /////
			////////////////////////

			else if (this.S.Interaction == StudentInteractionType.ClubGrudge)
			{
				this.S.CharacterAnimation.CrossFade(this.S.IdleAnim);

				if (this.S.TalkTimer == LongTime)
				{
					this.S.Subtitle.UpdateLabel(SubtitleType.ClubGrudge, (int)this.S.Club + this.ClubBonus, 99.0f);
					this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.S.TalkTimer = 0.0f;
					}

					if (this.S.TalkTimer <= 0.0f)
					{
						this.S.DialogueWheel.End();
					}
				}

				this.S.TalkTimer -= Time.deltaTime;
			}

			/////////////////////////
			///// CLUB PRACTICE /////
			/////////////////////////

			else if (this.S.Interaction == StudentInteractionType.ClubPractice)
			{
				if (this.S.TalkTimer == 100)
				{
					if (this.S.ClubPhase == 1)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubPractice, (int)this.S.Club + this.ClubBonus, 99.0f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
					else if (this.S.ClubPhase == 2)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubPracticeYes, (int)this.S.Club + this.ClubBonus, 99.0f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
					else if (this.S.ClubPhase == 3)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubPracticeNo, (int)this.S.Club + this.ClubBonus, 99.0f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.S.Subtitle.Label.text = string.Empty;
						Destroy(this.S.Subtitle.CurrentClip);
						this.S.TalkTimer = 0.0f;
					}
				}

				this.S.TalkTimer -= Time.deltaTime;

				if (this.S.TalkTimer <= 0.0f)
				{
					if (this.S.ClubPhase == 1)
					{
						this.S.DialogueWheel.PracticeWindow.Club = this.S.Club;
						this.S.DialogueWheel.PracticeWindow.UpdateWindow();
						this.S.DialogueWheel.PracticeWindow.ID = 1;
						this.S.Subtitle.Label.text = string.Empty;
						this.S.Interaction = 0;
					}
					else if (this.S.ClubPhase == 2)
					{
						this.S.DialogueWheel.PracticeWindow.Club = this.S.Club;
						this.S.DialogueWheel.PracticeWindow.FadeOut = true;
						this.S.Subtitle.Label.text = string.Empty;
						this.S.Interaction = 0;
					}
					else if (this.S.ClubPhase == 3)
					{
						this.S.DialogueWheel.End();
					}
				}
			}

			////////////////////////
			///// NAMING CRUSH /////
			////////////////////////

			else if (this.S.Interaction == StudentInteractionType.NamingCrush)
			{
				if (this.S.TalkTimer == MediumTime)
				{
					if (this.S.DialogueWheel.Victim != this.S.Crush)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.SuitorLove, 0, 3.0f);
						this.S.CharacterAnimation.CrossFade(this.S.GossipAnim);
						this.S.CurrentAnim = this.S.GossipAnim;
					}
					else
					{
						DatingGlobals.SuitorProgress = 1;
						this.S.Yandere.LoveManager.SuitorProgress++;

						this.S.Subtitle.UpdateLabel(SubtitleType.SuitorLove, 1, 3.0f);
						this.S.CharacterAnimation.CrossFade(this.S.Nod1Anim);
						this.S.CurrentAnim = this.S.Nod1Anim;
					}
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.S.TalkTimer = 0.0f;
					}

					if (this.S.CharacterAnimation[this.S.CurrentAnim].time >=
						this.S.CharacterAnimation[this.S.CurrentAnim].length)
					{
						this.S.CharacterAnimation.CrossFade(this.IdleAnim);
					}

					if (this.S.TalkTimer <= 0.0f)
					{
						this.S.DialogueWheel.End();
					}
				}

				this.S.TalkTimer -= Time.deltaTime;
			}

			///////////////////////////////
			///// CHANGING APPEARANCE /////
			///////////////////////////////

			else if (this.S.Interaction == StudentInteractionType.ChangingAppearance)
			{
				if (this.S.TalkTimer == MediumTime)
				{
					this.S.Subtitle.UpdateLabel(SubtitleType.SuitorLove, 2, 3.0f);
					this.S.CharacterAnimation.CrossFade(this.S.Nod1Anim);
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.S.TalkTimer = 0.0f;
					}

					if (this.S.CharacterAnimation[this.S.Nod1Anim].time >=
						this.S.CharacterAnimation[this.S.Nod1Anim].length)
					{
						this.S.CharacterAnimation.CrossFade(this.IdleAnim);
					}

					if (this.S.TalkTimer <= 0.0f)
					{
						this.S.DialogueWheel.End();
					}
				}

				this.S.TalkTimer -= Time.deltaTime;
			}

			/////////////////////
			///// COURTSHIP /////
			/////////////////////

			else if (this.S.Interaction == StudentInteractionType.Court)
			{
				if (this.S.TalkTimer == MediumTime)
				{
					if (this.S.Male)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.SuitorLove, 3, 5.0f);
					}
					else
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.SuitorLove, 4, 5.0f);
					}

					this.S.CharacterAnimation.CrossFade(this.S.Nod1Anim);
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.S.TalkTimer = 0.0f;
					}

					if (this.S.CharacterAnimation[this.S.Nod1Anim].time >=
						this.S.CharacterAnimation[this.S.Nod1Anim].length)
					{
						this.S.CharacterAnimation.CrossFade(this.IdleAnim);
					}

					if (this.S.TalkTimer <= 0.0f)
					{
						this.S.MeetTime = this.S.Clock.HourTime - 1;

						if (this.S.Male)
						{
							this.S.MeetSpot = this.S.StudentManager.SuitorSpot;
						}
						else
						{
							this.S.MeetSpot = this.S.StudentManager.RomanceSpot;
							this.S.StudentManager.LoveManager.RivalWaiting = true;
						}

						this.S.DialogueWheel.End();
					}
				}

				this.S.TalkTimer -= Time.deltaTime;
			}

			////////////////
			///// GIFT /////
			////////////////

			else if (this.S.Interaction == StudentInteractionType.Gift)
			{
				if (this.S.TalkTimer == LongTime)
				{
					this.S.Subtitle.UpdateLabel(SubtitleType.SuitorLove, 5, 99.0f);
					this.S.CharacterAnimation.CrossFade(this.S.Nod1Anim);
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.S.TalkTimer = 0.0f;
					}

					if (this.S.CharacterAnimation[this.S.Nod1Anim].time >=
						this.S.CharacterAnimation[this.S.Nod1Anim].length)
					{
						this.S.CharacterAnimation.CrossFade(this.IdleAnim);
					}

					if (this.S.TalkTimer <= 0.0f)
					{
						this.S.Rose = true;
						this.S.DialogueWheel.End();
					}
				}

				this.S.TalkTimer -= Time.deltaTime;
			}

			///////////////////
			///// FEEDING /////
			///////////////////

			else if (this.S.Interaction == StudentInteractionType.Feeding)
			{
				Debug.Log("Feeding.");

				if (this.S.TalkTimer == 10)
				{
					if (this.S.Club == ClubType.Delinquent)
					{
						this.S.CharacterAnimation.CrossFade(this.S.IdleAnim);

						this.S.Subtitle.UpdateLabel(SubtitleType.RejectFood, 1, 3.0f);
					}
					else if (this.S.Fed || this.S.Club == ClubType.Council || this.S.StudentID == 22)
					{
						this.S.CharacterAnimation.CrossFade(this.S.GossipAnim);
						this.S.Subtitle.UpdateLabel(SubtitleType.RejectFood, 0, 3.0f);
						this.S.Fed = true;
					}
					else
					{
						this.S.CharacterAnimation.CrossFade(this.S.Nod2Anim);

						this.S.Subtitle.UpdateLabel(SubtitleType.AcceptFood, 0, 3.0f);

						this.CalculateRepBonus();

						this.S.Reputation.PendingRep += 1.0f + this.S.RepBonus;
						this.S.PendingRep += 1.0f + this.S.RepBonus;
					}
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.S.TalkTimer = 0.0f;
					}
				}

				if (this.S.CharacterAnimation[this.S.Nod2Anim].time >=
					this.S.CharacterAnimation[this.S.Nod2Anim].length)
				{
					this.S.CharacterAnimation.CrossFade(this.S.IdleAnim);
				}

				if (this.S.CharacterAnimation[this.S.GossipAnim].time >=
					this.S.CharacterAnimation[this.S.GossipAnim].length)
				{
					this.S.CharacterAnimation.CrossFade(this.S.IdleAnim);
				}

				this.S.TalkTimer -= Time.deltaTime;

				if (this.S.TalkTimer <= 0.0f)
				{
					if (!this.S.Fed && this.S.Club != ClubType.Delinquent)
					{
						this.S.Yandere.PickUp.FoodPieces[this.S.Yandere.PickUp.Food].SetActive(false);
						this.S.Yandere.PickUp.Food--;
						this.S.Fed = true;
					}

					this.S.DialogueWheel.End();
					this.S.StudentManager.UpdateStudents();
				}
			}

			////////////////////////
			///// TASK INQUIRY /////
			////////////////////////

			else if (this.S.Interaction == StudentInteractionType.TaskInquiry)
			{
				if (this.S.TalkTimer == 10)
				{
					this.S.CharacterAnimation.CrossFade("f02_embar_00");
					this.S.Subtitle.UpdateLabel(SubtitleType.TaskInquiry, this.S.StudentID - 80, 10.0f);
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.S.TalkTimer = 0.0f;
					}
				}

				if (this.S.CharacterAnimation["f02_embar_00"].time >=
					this.S.CharacterAnimation["f02_embar_00"].length)
				{
					this.S.CharacterAnimation.CrossFade(this.IdleAnim);
				}

				this.S.TalkTimer -= Time.deltaTime;

				if (this.S.TalkTimer <= 0.0f)
				{
					this.S.StudentManager.TaskManager.GirlsQuestioned[this.S.StudentID - 80] = true;
					//this.S.StudentManager.TaskManager.UpdateTaskStatus();
					this.S.DialogueWheel.End();
				}
			}

			////////////////////////
			///// TAKING SNACK /////
			////////////////////////

			else if (this.S.Interaction == StudentInteractionType.TakingSnack)
			{
				Debug.Log(this.S.Name + " is reacting to being offered a snack.");

				if (this.S.TalkTimer == 5)
				{
					bool RivalRefuse = false;

					if (this.S.StudentID == this.S.StudentManager.RivalID && !this.S.Hungry)
					{
						Debug.Log ("Osana is not hungry, so she is going to refuse the snack.");

						RivalRefuse = true;
					}

					if (this.S.Club == ClubType.Delinquent)
					{
						this.S.CharacterAnimation.CrossFade(this.S.IdleAnim);

						this.S.Subtitle.UpdateLabel(SubtitleType.RejectFood, 1, 3.0f);
					}
					else if (this.S.Fed || this.S.Club == ClubType.Council || RivalRefuse || this.S.StudentID == 22)
					{
						this.S.CharacterAnimation.CrossFade(this.S.GossipAnim);
						this.S.Subtitle.UpdateLabel(SubtitleType.RejectFood, 0, 3.0f);
						this.S.Fed = true;

						if (this.S.StudentID == this.S.StudentManager.RivalID)
						{
							this.S.Subtitle.UpdateLabel(SubtitleType.RejectFood, 2, 5.0f);
						}

						Debug.Log (this.S.Name + " has refused the snack.");
					}
					else
					{
						this.S.CharacterAnimation.CrossFade(this.S.Nod2Anim);

						this.S.Subtitle.UpdateLabel(SubtitleType.AcceptFood, 0, 10.0f);

						this.CalculateRepBonus();

						this.S.Reputation.PendingRep += 1.0f + this.S.RepBonus;
						this.S.PendingRep += 1.0f + this.S.RepBonus;
					}
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.S.TalkTimer = 0.0f;
					}
				}

				if (this.S.CharacterAnimation[this.S.Nod2Anim].time >=
					this.S.CharacterAnimation[this.S.Nod2Anim].length)
				{
					this.S.CharacterAnimation.CrossFade(this.S.IdleAnim);
				}

				if (this.S.CharacterAnimation[this.S.GossipAnim].time >=
					this.S.CharacterAnimation[this.S.GossipAnim].length)
				{
					this.S.CharacterAnimation.CrossFade(this.S.IdleAnim);
				}

				this.S.TalkTimer -= Time.deltaTime;

				if (this.S.TalkTimer <= 0.0f)
				{
					if (!this.S.Fed && this.S.Club != ClubType.Delinquent)
					{
						if (this.S.StudentID == this.S.StudentManager.RivalID)
						{
							//If we're waiting for Yandere-chan to hand a snack to Osana...
							if (SchemeGlobals.GetSchemeStage(4) == 5)
							{
								SchemeGlobals.SetSchemeStage(4, 6);
								this.S.Yandere.PauseScreen.Schemes.UpdateInstructions();
							}
						}

						PickUpScript BagOfChips = this.S.Yandere.PickUp;

						this.S.Yandere.EmptyHands();
						this.S.EmptyHands();

						BagOfChips.GetComponent<MeshFilter>().mesh = this.S.StudentManager.OpenChipBag;
						BagOfChips.transform.parent = this.S.LeftItemParent;
						BagOfChips.transform.localPosition = new Vector3(-0.02f, -0.075f, 0);
						BagOfChips.transform.localEulerAngles = new Vector3(-15, -15, 30);
						BagOfChips.MyRigidbody.useGravity = false;
						BagOfChips.MyRigidbody.isKinematic = true;

						BagOfChips.Prompt.Hide();
						BagOfChips.Prompt.enabled = false;
						BagOfChips.enabled = false;

						this.S.BagOfChips = BagOfChips.gameObject;
						this.S.EatingSnack = true;
						this.S.Private = true;
						this.S.Hungry = false;
						this.S.Fed = true;
					}

					this.S.DialogueWheel.End();
					this.S.StudentManager.UpdateStudents();
				}
			}

			///////////////////////
			///// GIVING HELP /////
			///////////////////////

			else if (this.S.Interaction == StudentInteractionType.GivingHelp)
			{
				if (this.S.TalkTimer == 4)
				{
					if (this.S.Club == ClubType.Council || this.S.Club == ClubType.Delinquent)
					{
						this.S.CharacterAnimation.CrossFade(this.S.GossipAnim);
						this.S.Subtitle.UpdateLabel(SubtitleType.RejectHelp, 0, 4.0f);
					}
					else
					{
						if (this.S.Yandere.Bloodiness > 0)
						{
							this.S.CharacterAnimation.CrossFade(this.S.GossipAnim);
							this.S.Subtitle.UpdateLabel(SubtitleType.RejectHelp, 1, 4.0f);
						}
						else
						{
							this.S.CharacterAnimation.CrossFade(this.S.PullBoxCutterAnim);
							this.S.SmartPhone.SetActive(false);

							this.S.Subtitle.UpdateLabel(SubtitleType.GiveHelp, 0, 4.0f);
						}
					}
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						//The one time it shouldn't be possible to skip dialouge!
						//this.S.TalkTimer = 0.0f;
					}
				}

				if (this.S.CharacterAnimation[this.S.GossipAnim].time >=
					this.S.CharacterAnimation[this.S.GossipAnim].length)
				{
					this.S.CharacterAnimation.CrossFade(this.S.IdleAnim);
				}

				if (this.S.CharacterAnimation[this.S.PullBoxCutterAnim].time >=
					this.S.CharacterAnimation[this.S.PullBoxCutterAnim].length)
				{
					this.S.CharacterAnimation.CrossFade(this.S.IdleAnim);
				}

				this.S.TalkTimer -= Time.deltaTime;

				if (this.S.Club != ClubType.Council && this.S.Club != ClubType.Delinquent)
				{
					this.S.MoveTowardsTarget(this.S.Yandere.transform.position + (this.S.Yandere.transform.forward * 0.75f));

					if (this.S.CharacterAnimation[this.S.PullBoxCutterAnim].time >=
						this.S.CharacterAnimation[this.S.PullBoxCutterAnim].length)
					{
						this.S.CharacterAnimation.CrossFade(this.S.IdleAnim);
						this.StuckBoxCutter = null;
					}
					else if (this.S.CharacterAnimation[this.S.PullBoxCutterAnim].time >= 2)
					{
						if (StuckBoxCutter.transform.parent != S.RightEye)
						{
							StuckBoxCutter.Prompt.enabled = true;
							StuckBoxCutter.enabled = true;

							StuckBoxCutter.transform.parent = this.S.Yandere.PickUp.transform;
							StuckBoxCutter.transform.localPosition = new Vector3(0, 0.19f, 0);
							StuckBoxCutter.transform.localEulerAngles = new Vector3(0, -90, 0);
						}
					}
					else if (this.S.CharacterAnimation[this.S.PullBoxCutterAnim].time >= 1.166666f)
					{
						if (StuckBoxCutter == null)
						{
							StuckBoxCutter = this.S.Yandere.PickUp.StuckBoxCutter;

							this.S.Yandere.PickUp.StuckBoxCutter = null;

							StuckBoxCutter.FingerprintID = this.S.StudentID;
							StuckBoxCutter.transform.parent = this.S.RightHand;
							StuckBoxCutter.transform.localPosition = new Vector3(0, 0, 0);
							StuckBoxCutter.transform.localEulerAngles = new Vector3(0, 180, 0);
						}
					}
				}

				if (this.S.TalkTimer <= 0.0f)
				{
					this.S.DialogueWheel.End();
					this.S.StudentManager.UpdateStudents();
				}
			}

			//////////////////////////
			///// SENT TO LOCKER /////
			//////////////////////////

			else if (this.S.Interaction == StudentInteractionType.SentToLocker)
			{
				bool BeRude = false;

				if (this.S.Club == ClubType.Delinquent)
				{
					BeRude = true;
				}

				if (PlayerGlobals.GetStudentFriend(this.S.StudentID))
				{
					BeRude = false;
				}

				if (this.S.TalkTimer == 5)
				{
					if (!BeRude)
					{
						this.Refuse = false;

						if ((this.S.Clock.HourTime > 8.0f) &&
							(this.S.Clock.HourTime < 13.0f) ||
							(this.S.Clock.HourTime > 13.375f) &&
							(this.S.Clock.HourTime < 15.50f))
						{
							this.S.CharacterAnimation.CrossFade(this.S.GossipAnim);
							this.S.Subtitle.UpdateLabel(SubtitleType.SendToLocker, 1, 5.0f);
							this.Refuse = true;
						}
						else if (this.S.Club == ClubType.Council)
						{
							this.S.CharacterAnimation.CrossFade(this.S.GossipAnim);
							this.S.Subtitle.UpdateLabel(SubtitleType.SendToLocker, 3, 5.0f);
							this.Refuse = true;
						}
						else
						{
							this.S.CharacterAnimation.CrossFade(this.S.Nod1Anim);
							this.S.Subtitle.UpdateLabel(SubtitleType.SendToLocker, 2, 5.0f);
						}
					}
					else
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.Dismissive, 5, 5.0f);
					}
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.S.TalkTimer = 0.0f;
					}

					if (this.S.CharacterAnimation[this.S.Nod1Anim].time >=
						this.S.CharacterAnimation[this.S.Nod1Anim].length)
					{
						this.S.CharacterAnimation.CrossFade(this.IdleAnim);
					}

					if (this.S.TalkTimer <= 0.0f)
					{
						if (!this.Refuse)
						{
							if (!BeRude)
							{
								this.S.Pathfinding.speed = 4.0f;
								this.S.TargetDistance = 1.0f;

								this.S.SentToLocker = true;
								this.S.Routine = false;
								this.S.CanTalk = false;
							}
							else
							{
								this.S.Pathfinding.speed = 1.0f;
								this.S.TargetDistance = .5f;

								this.S.Routine = true;
								this.S.CanTalk = true;
							}
						}
							
						this.S.DialogueWheel.End();
					}
				}

				this.S.TalkTimer -= Time.deltaTime;
			}

			//////////////////////////
			///// GEIJU-SPECIFIC /////
			//////////////////////////

			if (this.S.StudentID == 41 && !this.S.DialogueWheel.ClubLeader && this.S.Interaction != StudentInteractionType.ClubUnwelcome)
			{
				if (this.S.TalkTimer > 0)
				{
					Debug.Log("Geiju response.");

					if (this.NegativeResponse)
					{
						Debug.Log("Negative response.");

						this.S.Subtitle.UpdateLabel(SubtitleType.Impatience, 6, 5.0f);
					}
					else
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.Impatience, 5, 5.0f);
					}
				}
			}

			///////////////////
			///// WAITING /////
			///////////////////

			if (this.S.Waiting)
			{
				this.S.WaitTimer -= Time.deltaTime;

				if (this.S.WaitTimer <= 0.0f)
				{
					this.S.DialogueWheel.TaskManager.UpdateTaskStatus();

					this.S.Talking = false;
					this.S.Waiting = false;
					this.enabled = false;

					if (!this.Fake)
					{
						if (!this.S.StudentManager.CombatMinigame.Practice)
						{
							this.S.Pathfinding.canSearch = true;
							this.S.Pathfinding.canMove = true;
							this.S.Obstacle.enabled = false;
							this.S.Alarmed = false;

							if (!this.S.Following && !this.S.Distracting &&
								!this.S.Wet && !this.S.EatingSnack && !this.S.SentToLocker)
							{
								this.S.Routine = true;
							}

							if (!this.S.Following)
							{
								// [af] This is the (unintuitive) Unity 5.3 way of changing emission.
								ParticleSystem.EmissionModule heartsEmission = this.S.Hearts.emission;
								heartsEmission.enabled = false;
							}
						}
					}

					this.S.StudentManager.EnablePrompts();

					if (this.S.GoAway)
					{
						Debug.Log("This student was just told to go away.");

						this.S.CurrentDestination =
							this.S.StudentManager.GoAwaySpots.List[this.S.StudentID];
						this.S.Pathfinding.target =
							this.S.StudentManager.GoAwaySpots.List[this.S.StudentID];

						this.S.Pathfinding.canSearch = true;
						this.S.Pathfinding.canMove = true;

						this.S.DistanceToDestination = 100;
					}
				}
			}
			else
			{
				this.S.targetRotation = Quaternion.LookRotation(new Vector3(
					this.S.Yandere.transform.position.x,
					this.transform.position.y,
					this.S.Yandere.transform.position.z) - this.transform.position);
				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.S.targetRotation, 10.0f * Time.deltaTime);
			}
		}
	}

	void CalculateRepBonus()
	{
		this.S.RepBonus = 0;

		if (PlayerGlobals.PantiesEquipped == 3)
		{
			this.S.RepBonus++;
		}

		if (this.S.Male && (this.S.Yandere.Class.Seduction + this.S.Yandere.Class.SeductionBonus) > 0 || this.S.Yandere.Class.Seduction == 5)
		{
			this.S.RepBonus++;
		}

		if (this.S.Yandere.Class.SocialBonus > 0)
		{
			this.S.RepBonus++;
		}

		this.S.ChameleonCheck();

		if (this.S.Chameleon)
		{
			this.S.RepBonus++;
		}
	}

	// [af] Commented in JS code.
	/*
	
	void LateUpdate()
	{
		if (this.S.Talking)
		{
			if (this.S.Interaction == 0)
			{
				var NeckTargetRotation = Quaternion.LookRotation(this.S.Yandere.Spine[4].position - this.S.Neck.position);
				S.Neck.rotation = NeckTargetRotation;//Quaternion.Slerp(S.Neck.rotation, NeckTargetRotation, 10 * Time.deltaTime);
			}
		}
	}
	
	*/
}
