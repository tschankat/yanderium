using System;
using UnityEngine;

public class OsanaMondayLunchEventScript : MonoBehaviour
{
#if UNITY_EDITOR
	public StudentManagerScript StudentManager;
	public JukeboxScript Jukebox;
	public UILabel EventSubtitle;
	public YandereScript Yandere;
	public ClockScript Clock;
	public SpyScript Spy;

	public StudentScript Senpai;
	public StudentScript Friend;
	public StudentScript Rival;

	public BentoScript[] Bento;

	public string[] SabotagedSpeechText;
	public string[] SpeechText;

	public float[] SabotagedSpeechTime;
	public float[] SpeechTime;

	public AudioClip[] SpeechClip;

	public Transform[] Location;
	public Transform Epicenter;

	public GameObject AlarmDisc;
	public GameObject VoiceClip;

	public Vector3 OriginalPosition;

	public bool Sabotaged = false;

	public float Distance = 0.0f;
	public float Scale = 0.0f;
	public float Timer = 0.0f;

	public float RotationX = 0.0f;
	public float RotationY = 0.0f;
	public float RotationZ = 0.0f;

	public int SpeechPhase = 1;
	public int DebugPoison = 0;
	public int FriendID = 6;
	public int RivalID = 11;
	public int Phase = 0;
	public int Frame = 0;

	void Start()
	{
		this.OriginalPosition = this.Epicenter.position;

		this.EventSubtitle.transform.localScale = Vector3.zero;

		if (DateGlobals.Weekday != DayOfWeek.Monday)
		{
			this.gameObject.SetActive(false);
			this.enabled = false;
		}
	}

	void Update()
	{
		#endif
		#if UNITY_EDITOR
		if (Input.GetKeyDown("p"))
		{
			DebugPoison++;

			if (DebugPoison == 5)
			{
				this.StudentManager.Students[this.RivalID].MyBento.Tampered = true;
				this.StudentManager.Students[this.RivalID].MyBento.Lethal = true;
			}
		}
		#endif
		#if UNITY_EDITOR

		if (this.Phase == 0)
		{
			if (this.Frame > 0)
			{
				if (this.StudentManager.Students[this.RivalID] != null)
				{
					// [af] Added "gameObject" for C# compatibility.
					if (this.StudentManager.Students[1].gameObject.activeInHierarchy)
					{
						if (this.Clock.Period == 3)
						{
							Debug.Log("Osana's lunchtime event began.");

							this.DisableBentos();
							this.Bento[1].gameObject.SetActive(true);
							this.Bento[2].gameObject.SetActive(true);

							this.Senpai = this.StudentManager.Students[1];
							this.Rival = this.StudentManager.Students[this.RivalID];
							this.Friend = this.StudentManager.Students[this.FriendID];

							this.Senpai.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
							this.Senpai.CharacterAnimation.Play(this.Senpai.WalkAnim);
							this.Senpai.Pathfinding.target = this.Location[1];
							this.Senpai.CurrentDestination = this.Location[1];
							this.Senpai.Pathfinding.canSearch = true;
							this.Senpai.Pathfinding.canMove = true;
							this.Senpai.Routine = false;
							this.Senpai.InEvent = true;

							this.Senpai.Prompt.Hide();
							this.Senpai.Prompt.enabled = false;

							this.Rival.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
							this.Rival.CharacterAnimation.Play(this.Rival.WalkAnim);
							this.Rival.Pathfinding.target = this.Location[0];
							this.Rival.CurrentDestination = this.Location[0];
							this.Rival.Pathfinding.canSearch = true;
							this.Rival.Pathfinding.canMove = true;
							this.Rival.Routine = false;
							this.Rival.InEvent = true;
							this.Rival.EmptyHands();

							this.Rival.Prompt.Hide();
							this.Rival.Prompt.enabled = false;

							this.Rival.Prompt.Hide();
							this.Rival.Prompt.enabled = false;

							this.Spy.Prompt.enabled = true;

							if (this.Friend != null)
							{
								this.Friend.EmptyHands();
							}

							this.Phase++;
						}
					}
				}
			}

			this.Frame++;
		}
		else if (this.Phase == 1)
		{
			if (this.Rival.DistanceToDestination < 0.50f)
			{
				this.EventSubtitle.text = this.SpeechText[this.SpeechPhase];
				this.SpeechPhase++;

				AudioClipPlayer.Play(this.SpeechClip[0],
					this.Rival.transform.position + (Vector3.up * 1.50f),
					5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

				this.Rival.CharacterAnimation.CrossFade(AnimNames.FemalePondering);
				this.Epicenter.position = this.Rival.transform.position;

				this.Phase++;
			}
		}
		else if (this.Phase == 2)
		{
			this.Rival.MoveTowardsTarget(this.Rival.CurrentDestination.position);
			this.Rival.transform.rotation = Quaternion.Slerp(
				this.Rival.transform.rotation,
				this.Rival.CurrentDestination.rotation,
				10.0f * Time.deltaTime);

			if (this.Rival.CharacterAnimation[AnimNames.FemalePondering].time >=
				this.Rival.CharacterAnimation[AnimNames.FemalePondering].length)
			{
				this.Epicenter.position = this.OriginalPosition;
				this.EventSubtitle.text = string.Empty;

				this.Rival.CharacterAnimation.Play(this.Rival.WalkAnim);
				this.Rival.Pathfinding.target = this.Location[2];
				this.Rival.CurrentDestination = this.Location[2];
				this.Rival.Pathfinding.canSearch = true;
				this.Rival.Pathfinding.canMove = true;

				this.Bento[1].gameObject.SetActive(false);
				this.Bento[2].gameObject.SetActive(false);

				this.Phase++;
			}
		}
		else if (this.Phase == 3)
		{
			if ((this.Senpai.DistanceToDestination < 0.50f) &&
				(this.Rival.DistanceToDestination < 0.50f))
			{
				if (this.Friend != null)
				{
					this.Friend.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
					this.Friend.CharacterAnimation.Play(this.Friend.WalkAnim);
					this.Friend.Pathfinding.target = this.Location[3];
					this.Friend.CurrentDestination = this.Location[3];
					this.Friend.Pathfinding.canSearch = true;
					this.Friend.Pathfinding.canMove = true;
					this.Friend.Routine = false;
					this.Friend.InEvent = true;
				}

				AudioClipPlayer.Play(this.SpeechClip[1],
					this.Epicenter.transform.position + (Vector3.up * 1.50f),
					5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

				this.Senpai.CharacterAnimation.CrossFade("Monday_2A");
				this.Rival.CharacterAnimation.CrossFade("f02_Monday_2A");

				this.Rival.Bento.transform.localEulerAngles = new Vector3(
					15.0f,
					this.Rival.Bento.transform.localEulerAngles.y,
					this.Rival.Bento.transform.localEulerAngles.z);

				this.Rival.Bento.transform.localPosition = new Vector3(
					-0.020f,
					this.Rival.Bento.transform.localPosition.y,
					this.Rival.Bento.transform.localPosition.z);

				this.Rival.Bento.SetActive(true);

				this.Senpai.Pathfinding.canSearch = false;
				this.Senpai.Pathfinding.canMove = false;

				this.Rival.Pathfinding.canSearch = false;
				this.Rival.Pathfinding.canMove = false;

				this.Phase++;
			}
			else
			{
				if (this.Senpai.DistanceToDestination < 0.50f)
				{
					this.Senpai.CharacterAnimation.CrossFade(AnimNames.MaleThinking);
					this.Senpai.MoveTowardsTarget(this.Senpai.CurrentDestination.position);
					this.Senpai.transform.rotation = Quaternion.Slerp(
						this.Senpai.transform.rotation,
						this.Senpai.CurrentDestination.rotation,
						10.0f * Time.deltaTime);

					this.Senpai.Pathfinding.canSearch = false;
					this.Senpai.Pathfinding.canMove = false;
				}

				if (this.Rival.DistanceToDestination < .50f)
				{
					this.Rival.CharacterAnimation.CrossFade(AnimNames.FemalePondering);
					this.Rival.MoveTowardsTarget(this.Rival.CurrentDestination.position);
					this.Rival.transform.rotation = Quaternion.Slerp(
						this.Rival.transform.rotation,
						this.Rival.CurrentDestination.rotation,
						10.0f * Time.deltaTime);

					this.Rival.Pathfinding.canSearch = false;
					this.Rival.Pathfinding.canMove = false;
				}
			}
		}
		else if (this.Phase == 4)
		{
			this.Timer += Time.deltaTime;

			this.Senpai.MoveTowardsTarget(this.Senpai.CurrentDestination.position);
			this.Senpai.transform.rotation = Quaternion.Slerp(
				this.Senpai.transform.rotation,
				this.Senpai.CurrentDestination.rotation,
				10.0f * Time.deltaTime);

			this.Rival.MoveTowardsTarget(this.Rival.CurrentDestination.position);
			this.Rival.transform.rotation = Quaternion.Slerp(
				this.Rival.transform.rotation,
				this.Rival.CurrentDestination.rotation,
				10.0f * Time.deltaTime);

			if (this.Timer > 21.50f)
			{
				this.Senpai.Bento.transform.localPosition = new Vector3(-0.01652f, -0.02516f, -0.08239f);
				this.Senpai.Bento.transform.localEulerAngles = new Vector3(-35.0f, 116.0f, 165.0f);

				this.RotationX = -35.0f;
				this.RotationY = 115.0f;
				this.RotationZ = 165.0f;

				this.Senpai.Bento.SetActive(true);
				this.Rival.Bento.SetActive(false);
				this.Phase++;
			}
		}
		else if (this.Phase == 5)
		{
			this.Timer += Time.deltaTime;

			this.RotationX = Mathf.Lerp(this.RotationX, 6.0f, Time.deltaTime);
			this.RotationY = Mathf.Lerp(this.RotationY, 153.0f, Time.deltaTime);
			this.RotationZ = Mathf.Lerp(this.RotationZ, 102.50f, Time.deltaTime);

			this.Senpai.Bento.transform.localPosition = Vector3.Lerp(
				this.Senpai.Bento.transform.localPosition,
				new Vector3(-0.045f, -0.080f, -0.025f),
				Time.deltaTime);
			this.Senpai.Bento.transform.localEulerAngles =
				new Vector3(this.RotationX, this.RotationY, this.RotationZ);

			if (this.Timer > 29.83333333333f)
			{
				this.Senpai.Lid.transform.parent = this.Senpai.RightHand;
				this.Senpai.Lid.transform.localPosition = new Vector3(-0.025f, -0.025f, -0.015f);
				this.Senpai.Lid.transform.localEulerAngles = new Vector3(41.50f, -60.0f, -180.0f);
			}

			if (this.Timer > 30.0f)
			{
				if (this.Bento[1].Poison > 0.0f)
				{
					this.Senpai.CharacterAnimation.CrossFade("Monday_2B");
					this.Rival.CharacterAnimation.CrossFade("f02_Monday_2B");

					this.Sabotaged = true;
					this.SpeechPhase = 0;
				}
			}

			if (this.Timer > 30.43333333333f)
			{
				this.Senpai.Lid.transform.parent = null;
				this.Senpai.Lid.transform.position = new Vector3(-0.31f, 12.501f, -29.335f);
				this.Senpai.Lid.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
			}

			if (this.Timer > 31.0f)
			{
				this.Senpai.Chopsticks[0].SetActive(true);
				this.Senpai.Chopsticks[1].SetActive(true);
				this.Phase++;
			}
		}
		else if (this.Phase == 6)
		{
			this.Timer += Time.deltaTime;

			if (!this.Sabotaged)
			{
				if (this.Timer > 37.15f)
				{
					AudioClipPlayer.Play(this.SpeechClip[2],
						this.Epicenter.transform.position + (Vector3.up * 1.50f),
						5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

					this.Phase++;
				}
			}
			else
			{
				if (this.Timer > 41.0f)
				{
					AudioClipPlayer.Play(this.SpeechClip[3],
						this.Epicenter.transform.position + (Vector3.up * 1.50f),
						5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

					this.Phase++;
				}
			}
		}
		else if (this.Phase == 7)
		{
			this.Timer += Time.deltaTime;

			if (!this.Sabotaged)
			{
				if (this.Senpai.CharacterAnimation["Monday_2A"].time >
					this.Senpai.CharacterAnimation["Monday_2A"].length)
				{
					this.EndEvent();
				}
			}
			else
			{
				if (this.Timer > 44.50f)
				{
					if (this.Senpai.Bento.transform.parent != null)
					{
						this.Senpai.Bento.transform.parent = null;

						this.Senpai.Bento.transform.position = new Vector3(-0.853f, 12.501f, -29.33333f);
						this.Senpai.Bento.transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);

						this.Senpai.Chopsticks[0].SetActive(false);
						this.Senpai.Chopsticks[1].SetActive(false);
					}
				}

				if (this.Senpai.InEvent)
				{
					if (this.Senpai.CharacterAnimation["Monday_2B"].time >
						this.Senpai.CharacterAnimation["Monday_2B"].length)
					{
						this.Senpai.WalkAnim = "stomachPainWalk_00";

						this.Senpai.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
						this.Senpai.Pathfinding.target = this.StudentManager.MaleVomitSpots[3];
						this.Senpai.CurrentDestination = this.StudentManager.MaleVomitSpots[3];
						this.Senpai.CharacterAnimation.CrossFade(this.Senpai.WalkAnim);
						this.Senpai.Pathfinding.canSearch = true;
						this.Senpai.Pathfinding.canMove = true;
						this.Senpai.Pathfinding.speed = 2.0f;
						this.Senpai.Distracted = true;
						this.Senpai.Vomiting = true;
						this.Senpai.InEvent = false;
						this.Senpai.Routine = false;
						this.Senpai.Private = true;

						DatingGlobals.RivalSabotaged++;
						Debug.Log("Sabotage Progress: " + DatingGlobals.RivalSabotaged + "/5");
					}
				}

				if (this.Rival.CharacterAnimation["f02_Monday_2B"].time >
					this.Rival.CharacterAnimation["f02_Monday_2B"].length)
				{
					this.EndEvent();
				}
			}
		}

		if (this.Phase > 3)
		{
			////////////////////////
			///// VOICED LINES /////
			////////////////////////

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
			else
			{
				if (this.SpeechPhase < this.SabotagedSpeechTime.Length)
				{
					if (this.Timer > (41.0f + this.SabotagedSpeechTime[this.SpeechPhase]))
					{
						this.EventSubtitle.text = this.SabotagedSpeechText[this.SpeechPhase];
						this.SpeechPhase++;
					}
				}
			}

			if (this.Friend.DistanceToDestination < 1)
            {
                this.Friend.CharacterAnimation.CrossFade(AnimNames.FemaleCornerPeek);
				this.Friend.Pathfinding.canSearch = false;
				this.Friend.Pathfinding.canMove = false;

				this.Friend.MyBento.gameObject.SetActive(true);
				this.Friend.MyBento.transform.parent = null;
				this.Friend.MyBento.transform.position = Bento[3].transform.position;
				this.Friend.MyBento.transform.eulerAngles = Bento[3].transform.eulerAngles;
				this.Friend.MyBento.Prompt.enabled = true;

                this.SettleFriend();
            }
		}

		if (this.Phase > 0)
		{
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

			if (this.Clock.Period > 3)
			{
				this.EndEvent();
			}

			/////////////////////////
			///// SUBTITLE SIZE /////
			/////////////////////////

			if (this.VoiceClip != null)
			{
				this.VoiceClip.GetComponent<AudioSource>().pitch = Time.timeScale;
			}

			this.Distance = Vector3.Distance(this.Yandere.transform.position,
				this.Epicenter.position);

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

					this.EventSubtitle.transform.localScale =
						new Vector3(this.Scale, this.Scale, this.Scale);

					if (this.VoiceClip != null)
					{
						this.VoiceClip.GetComponent<AudioSource>().volume = this.Scale;
					}

					if (this.Phase > 3)
					{
						// [af] Replaced if/else statements with boolean expression.
						this.Yandere.Eavesdropping = this.Distance < 5.0f;
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
			}
		}
	}

	void SettleFriend()
	{
		this.Friend.MoveTowardsTarget(this.Location[3].position);

		float Angle = Quaternion.Angle(this.Friend.transform.rotation, this.Location[3].rotation);
		
		if (Angle > 1.0f)
		{
			this.Friend.transform.rotation = Quaternion.Slerp(
				this.Friend.transform.rotation, this.Location[3].rotation, 10.0f * Time.deltaTime);
		}
	}

	void EndEvent()
	{
		if (this.VoiceClip != null)
		{
			Destroy(this.VoiceClip);
		}

		if (this.Senpai.InEvent)
		{
			this.Senpai.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
			this.Senpai.InEvent = false;
			this.Senpai.Private = false;

			this.Senpai.Pathfinding.canSearch = true;
			this.Senpai.Pathfinding.canMove = true;
			this.Senpai.TargetDistance = 1;
			this.Senpai.Routine = true;

			//This makes Senpai move to the "sit and eat bento" part of his day.
			//this.Senpai.Phase = 4;
		}

		this.Rival.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
		this.Rival.Prompt.enabled = true;
		this.Rival.InEvent = false;
		this.Rival.Private = false;

		this.Rival.Pathfinding.canSearch = true;
		this.Rival.Pathfinding.canMove = true;
		this.Rival.TargetDistance = 1;
		this.Rival.Routine = true;

		if (this.Rival.Alarmed || this.Senpai.Alarmed)
		{
			this.Senpai.Pathfinding.canSearch = false;
			this.Senpai.Pathfinding.canMove = false;
			this.Senpai.TargetDistance = .5f;

			this.Senpai.Routine = !this.Senpai.Alarmed;

			this.Rival.Pathfinding.canSearch = false;
			this.Rival.Pathfinding.canMove = false;
			this.Rival.TargetDistance = .5f;

			this.Rival.Routine = !this.Rival.Alarmed;
		}

		if (this.Friend != null)
		{
			if (!this.Friend.Alarmed)
			{
				this.Friend.Pathfinding.canSearch = true;
				this.Friend.Pathfinding.canMove = true;
				this.Friend.Routine = true;
			}

			this.Friend.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
			this.Friend.Prompt.enabled = true;
			this.Friend.InEvent = false;
			this.Friend.Private = false;

			ScheduleBlock newBlock4 = this.Friend.ScheduleBlocks[4];
			newBlock4.destination = "LunchSpot";
			newBlock4.action = "Eat";

			Friend.GetDestinations();
			Friend.CurrentDestination = this.Friend.Destinations[this.Friend.Phase];
			Friend.Pathfinding.target = this.Friend.Destinations[this.Friend.Phase];

			Friend.DistanceToDestination = 100;

			this.Friend.MyBento.gameObject.SetActive(false);
			this.Friend.MyBento.transform.parent = this.Friend.LeftHand;
			this.Friend.MyBento.transform.localPosition = new Vector3(-0.025f, -0.105f, 0);
			this.Friend.MyBento.transform.localEulerAngles = new Vector3(0, 165, 82.5f);

			Debug.Log("''Friend'' is being told to set her destination to her current phase's destination.");
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

		this.EventSubtitle.text = string.Empty;
		this.Yandere.Eavesdropping = false;
		this.enabled = false;

		this.Jukebox.Dip = 1;

		Debug.Log("Ending Osana's Monday Lunch Event.");

		DisableBentos();
	}

	void DisableBentos()
	{
		this.Bento[1].Prompt.Hide();
		this.Bento[1].Prompt.enabled = false;

		this.Bento[2].Prompt.Hide();
		this.Bento[2].Prompt.enabled = false;

		this.Bento[1].gameObject.SetActive(false);
		this.Bento[2].gameObject.SetActive(false);

		this.Bento[1].enabled = false;
		this.Bento[2].enabled = false;
	}
#endif
}