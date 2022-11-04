using System;
using UnityEngine;

public class OsanaWednesdayLunchEventScript : MonoBehaviour
{
	#if UNITY_EDITOR
	public StudentManagerScript StudentManager;
	public JukeboxScript Jukebox;
	public UILabel EventSubtitle;
	public YandereScript Yandere;
	public ClockScript Clock;

	public StudentScript Rival;

	public Transform Location;

	public AudioClip SpeechClip;
	public string SpeechText;
	public string EventAnim;

	public GameObject AlarmDisc;
	public GameObject VoiceClip;

	public float Distance = 0.0f;
	public float Scale = 0.0f;

	public DayOfWeek EventDay = DayOfWeek.Sunday;

	public int StartPeriod = 0;
	public int RivalID = 11;
	public int Phase = 0;
	public int Frame = 0;

	void Start()
	{
		this.EventSubtitle.transform.localScale = Vector3.zero;

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
				if (this.StudentManager.Students[this.RivalID] != null)
				{
					if (this.Rival == null)
					{
						this.Rival = this.StudentManager.Students [this.RivalID];
					}

					if (this.Clock.Period == 3 || this.Clock.Period == 6)
					{
						if (!this.Rival.InEvent && !this.Rival.Phoneless)
						{
							Debug.Log ("Osana's Wednesday lunchtime event has begun.");

							this.Rival.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
							this.Rival.CharacterAnimation.Play (this.Rival.WalkAnim);
							this.Rival.Pathfinding.target = this.Location;
							this.Rival.CurrentDestination = this.Location;
							this.Rival.Pathfinding.canSearch = true;
							this.Rival.Pathfinding.canMove = true;
							this.Rival.Routine = false;
							this.Rival.InEvent = true;
							this.Rival.EmptyHands();

							this.Rival.Prompt.Hide ();
							this.Rival.Prompt.enabled = false;

							this.StartPeriod = this.Clock.Period;

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
					this.Yandere.transform.position = this.Location.position + new Vector3(2.0f, 0.0f, 2.0f);
					this.Rival.transform.position = this.Location.position + new Vector3(1.0f, 0.0f, 1.0f);
				}

				if (this.Rival.DistanceToDestination < 0.50f)
				{
					AudioClipPlayer.Play(this.SpeechClip, 
						this.Rival.transform.position + (Vector3.up * 1.50f),
						5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

					this.EventSubtitle.text = SpeechText;

					this.Rival.CharacterAnimation.CrossFade("f02_" + this.EventAnim);
					this.Rival.Pathfinding.canSearch = false;
					this.Rival.Pathfinding.canMove = false;
					this.Rival.Obstacle.enabled = true;
					this.Phase++;
				}
			}
			//Osana takes her phone out.
			else if (this.Phase == 2)
			{
				if (this.Rival.CharacterAnimation["f02_" + this.EventAnim].time >= 1.33333)
				{
					this.Rival.SmartPhone.SetActive(true);
					this.Phase++;
				}
			}
			//Osana puts her phone away.
			else if (this.Phase == 3)
			{
				if (this.Rival.CharacterAnimation["f02_" + this.EventAnim].time >= 6.833333)
				{
					this.Rival.SmartPhone.SetActive(false);
					this.Phase++;
				}
			}
			//Osana reaches the end of the "take photo" animation and ends the event. 
			else if (this.Phase == 4)
			{
				if (this.Rival.CharacterAnimation ["f02_" + this.EventAnim].time >= this.Rival.CharacterAnimation ["f02_" + this.EventAnim].length) 
				{
					//Osana.Pickpocket.enabled = true;
					EndEvent();
				}
			}

			////////////////////////////////////////////
			///// ACTIONS THAT COULD END THE EVENT /////
			////////////////////////////////////////////

			if (this.Clock.Period > this.StartPeriod)
			{
				this.EndEvent();
			}

			if (this.Rival.Alarmed)
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

	void EndEvent()
	{
		Debug.Log ("Osana's Wednesday lunchtime event has ended.");

		if (this.VoiceClip != null)
		{
			Destroy(this.VoiceClip);
		}

		if (!this.Rival.Alarmed)
		{
			this.Rival.CharacterAnimation.CrossFade(this.Rival.WalkAnim);
			this.Rival.DistanceToDestination = 100.0f;

			this.Rival.Pathfinding.canSearch = true;
			this.Rival.Pathfinding.canMove = true;
			this.Rival.Routine = true;
		}

		this.Rival.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
		this.Rival.Obstacle.enabled = false;
		this.Rival.Prompt.enabled = true;
		this.Rival.InEvent = false;
		this.Rival.Private = false;

		if (!this.StudentManager.Stop)
		{
			this.StudentManager.UpdateStudents();
		}

		this.Jukebox.Dip = 1;

		this.EventSubtitle.text = string.Empty;
		this.enabled = false;
	}
	#endif
}