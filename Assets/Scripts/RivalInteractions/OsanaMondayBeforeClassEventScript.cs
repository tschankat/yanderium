using System;
using UnityEngine;

public class OsanaMondayBeforeClassEventScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public EventManagerScript NextEvent;
	public JukeboxScript Jukebox;
	public UILabel EventSubtitle;
	public YandereScript Yandere;
	public ClockScript Clock;

	public StudentScript Rival;

	public Transform Destination;

	public AudioClip SpeechClip;
	public string[] SpeechText;
	public float[] SpeechTime;

	public GameObject AlarmDisc;
	public GameObject VoiceClip;
	public GameObject[] Bentos;

	public bool EventActive = false;

	public float Distance = 0.0f;
	public float Scale = 0.0f;
	public float Timer = 0.0f;

	public int SpeechPhase = 1;
	public int RivalID = 11;
	public int Phase = 0;
	public int Frame = 0;

	void Start()
	{
		this.EventSubtitle.transform.localScale = Vector3.zero;
		this.Bentos[1].SetActive(false);
		this.Bentos[2].SetActive(false);

		if (DateGlobals.Weekday != DayOfWeek.Monday)
		{
			this.enabled = false;
		}
	}

	#if UNITY_EDITOR

	void Update()
	{
		if (this.Phase == 0)
		{
			if (this.Frame > 0)
			{
				if (this.Clock.Period == 1)
				{
					if (this.StudentManager.Students[this.RivalID] != null)
					{
						if (this.Rival == null)
						{
							this.Rival = this.StudentManager.Students[this.RivalID];
						}
						else
						{
							if (this.Rival.Indoors)
							{
								Debug.Log("Osana's before class event is beginning. Putting two bento boxes on her desk.");

								this.Rival.CharacterAnimation[AnimNames.FemalePondering].speed = .65f;

								this.Rival.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
								this.Rival.CharacterAnimation.CrossFade(this.Rival.WalkAnim);
								this.Rival.Pathfinding.target = this.Destination;
								this.Rival.CurrentDestination = this.Destination;
								this.Rival.Pathfinding.canSearch = true;
								this.Rival.Pathfinding.canMove = true;
								this.Rival.Routine = false;
								this.Rival.InEvent = true;

								this.Rival.Prompt.Hide();
								this.Rival.Prompt.enabled = false;

								if (this.Rival.Follower != null)
								{
									this.Rival.Follower.TargetDistance = 1.5f;
								}

								this.Phase++;
							}
						}
					}
					else
					{
						this.enabled = false;
					}
				}
				else
				{
					this.enabled = false;
				}
			}

			this.Frame++;
		}
		else if (this.Phase == 1)
		{
			if (this.Rival.DistanceToDestination < 0.50f)
			{
				AudioClipPlayer.Play(this.SpeechClip,
					this.Rival.transform.position + (Vector3.up * 1.50f),
					5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

				this.Rival.CharacterAnimation.CrossFade(AnimNames.FemalePondering);
				this.Rival.Pathfinding.canSearch = false;
				this.Rival.Pathfinding.canMove = false;
				this.Bentos[1].SetActive(true);
				this.Bentos[2].SetActive(true);
				this.Phase++;
			}
			/*
			else
			{
				Debug.Log ("Here.");

				this.Rival.Follower.Pathfinding.canSearch = true;
				this.Rival.Follower.Pathfinding.canMove = true;
			}
			*/
		}
		else
		{
			this.Rival.MoveTowardsTarget(this.Rival.CurrentDestination.position);
			this.Rival.transform.rotation = Quaternion.Slerp(
				this.Rival.transform.rotation,
				this.Rival.CurrentDestination.rotation,
				10.0f * Time.deltaTime);

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
				if (this.Rival.CharacterAnimation[AnimNames.FemalePondering].time > this.Rival.CharacterAnimation[AnimNames.FemalePondering].length)
				{
					this.EndEvent();
				}
			}

			if (this.Rival.Alarmed || this.Rival.Splashed ||
				this.Rival.Follower != null && this.Rival.Follower.DramaticReaction)
			{
				GameObject NewAlarmDisc = Instantiate(this.AlarmDisc,
					this.Yandere.transform.position + Vector3.up, Quaternion.identity);
				NewAlarmDisc.GetComponent<AlarmDiscScript>().NoScream = true;

				this.EndEvent();
			}

			/////////////////////////
			///// SUBTITLE SIZE /////
			/////////////////////////

			this.Distance = Vector3.Distance(this.Yandere.transform.position,
				this.Rival.transform.position);

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

					// [af] Commented in JS code.
					/*
					if (this.Distance < 5)
					{
						this.Yandere.Eavesdropping = true;
					}
					else
					{
						this.Yandere.Eavesdropping = false;
					}
					*/
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

		if (this.Phase > 0)
		{
			////////////////////////////////////////////
			///// ACTIONS THAT COULD END THE EVENT /////
			////////////////////////////////////////////

			if (this.Clock.Period > 1)
			{
				this.EndEvent();
			}

			#endif

			#if UNITY_EDITOR
			if (Input.GetKeyDown(KeyCode.LeftControl))
			{
				this.Bentos[1].SetActive(true);
				this.Bentos[2].SetActive(true);

				EndEvent();
			}
			#endif

			#if UNITY_EDITOR
		}
	}

	public void EndEvent()
	{
		Debug.Log("Osana's before class event ended.");

		this.Bentos[1].GetComponent<PromptScript>().enabled = true;
		this.Bentos[2].GetComponent<PromptScript>().enabled = true;

		if (this.VoiceClip != null)
		{
			Destroy(this.VoiceClip);
		}

		if (!this.Rival.Alarmed)
		{
			//if (this.Rival.Phase == 1)
			//{
				//Debug.Log("Osana's phase was 1, so we're increasing her phase.");

				//this.Rival.Phase++;
				this.Rival.CurrentDestination = this.Rival.Destinations[this.Rival.Phase];
				this.Rival.Pathfinding.target = this.Rival.Destinations[this.Rival.Phase];
			//}

			this.Rival.Pathfinding.canSearch = true;
			this.Rival.Pathfinding.canMove = true;
			this.Rival.Routine = true;
		}
		else
		{
			Debug.Log("The event ended specifically because Osana was alarmed.");

			this.Rival.Pathfinding.canSearch = false;
			this.Rival.Pathfinding.canMove = false;
		}

		this.Rival.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
		this.Rival.DistanceToDestination = 999;
		this.Rival.Prompt.enabled = true;
		this.Rival.InEvent = false;
		this.Rival.Private = false;

		if (!this.StudentManager.Stop)
		{
			this.StudentManager.UpdateStudents();
		}

		this.Rival.CharacterAnimation[AnimNames.FemalePondering].speed = 1;

		this.Jukebox.Dip = 1;

		if (this.Rival.Follower != null)
		{
			this.Rival.Follower.TargetDistance = 1;
		}

		this.EventSubtitle.text = string.Empty;
		this.NextEvent.enabled = true;
		this.enabled = false;
	}
#endif
}