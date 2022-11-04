using System;
using UnityEngine;

public class OsanaVendingMachineEventScript : MonoBehaviour
{
	#if UNITY_EDITOR
	public StudentManagerScript StudentManager;
	public JukeboxScript Jukebox;
	public UILabel EventSubtitle;
	public YandereScript Yandere;
	public ClockScript Clock;

	public StudentScript Rival;

	public Transform Location;

	public AudioSource MyAudio;

	public AudioClip[] SpeechClip;
	public AudioClip Bang;

	public string[] SpeechText;
	public string[] EventAnim;

	public GameObject OsanaVandalismCollider;
	public GameObject AlarmDisc;
	public GameObject VoiceClip;

	public float MinimumDistance = 0.5f;
	public float Distance = 0.0f;
	public float Scale = 0.0f;
	public float Timer = 0.0f;

	public DayOfWeek EventDay = DayOfWeek.Sunday;

	public int StartPeriod = 0;
	public int RivalID = 11;
	public int Phase = 0;
	public int Frame = 0;

	public bool PlaySound;

	void Start()
	{
		this.EventSubtitle.transform.localScale = Vector3.zero;

		/*
		if (DateGlobals.Weekday != this.EventDay)
		{
			this.enabled = false;
		}
		*/
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
						this.Rival = this.StudentManager.Students[this.RivalID];
					}

					if (this.Rival.SnackPhase == 1)
					{
						Debug.Log ("Osana's vending machine event has begun.");

						AudioClipPlayer.Play(this.SpeechClip[0], 
							this.Rival.transform.position + (Vector3.up * 1.50f),
							5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

						this.EventSubtitle.text = SpeechText[0];

						this.Rival.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
						this.Rival.CharacterAnimation.Play (this.Rival.WalkAnim);
						this.Rival.Pathfinding.target = this.Location;
						this.Rival.CurrentDestination = this.Location;
						this.Rival.Pathfinding.canSearch = true;
						this.Rival.Pathfinding.canMove = true;
						this.Rival.EatingSnack = false;
						this.Rival.Routine = false;
						this.Rival.InEvent = true;

						this.Rival.EmptyHands();

						//this.Rival.Prompt.Hide ();
						//this.Rival.Prompt.enabled = false;

						this.Phase++;
					}
				}
			}

			this.Frame++;
		}
		else
		{
			if (this.Rival.DistanceToDestination < this.MinimumDistance)
			{
				this.Rival.MoveTowardsTarget(this.Location.position);

				float Angle = Quaternion.Angle(this.Rival.transform.rotation, this.Location.rotation);

				if (Angle > 1.0f)
				{
					this.Rival.transform.rotation = Quaternion.Slerp(
						this.Rival.transform.rotation, this.Location.rotation, 10.0f * Time.deltaTime);
				}
			}

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
					AudioClipPlayer.Play(this.SpeechClip[1], 
						this.Rival.transform.position + (Vector3.up * 1.50f),
						5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

					this.EventSubtitle.text = SpeechText[1];

					this.Rival.CharacterAnimation.CrossFade(this.EventAnim[1]);
					this.Rival.Pathfinding.canSearch = false;
					this.Rival.Pathfinding.canMove = false;
					this.Rival.Obstacle.enabled = true;
					this.Phase++;
				}
			}
			//Osana waits for the drink to come out.
			else if (this.Phase == 2)
			{
				if (this.Rival.CharacterAnimation[this.EventAnim[1]].time >=
					this.Rival.CharacterAnimation[this.EventAnim[1]].length) 
				{
					this.Rival.CharacterAnimation[this.EventAnim[2]].time = 7;
					this.Rival.CharacterAnimation.CrossFade(this.EventAnim[2]);
					this.Phase++;
				}
			}
			//Osana gets mad when the drink doesn't come out.
			else if (this.Phase == 3)
			{
				this.Timer += Time.deltaTime;

				if (this.Timer > 5)
				{
					AudioClipPlayer.Play(this.SpeechClip[3], 
						this.Rival.transform.position + (Vector3.up * 1.50f),
						5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

					this.EventSubtitle.text = SpeechText[3];

					this.Rival.CharacterAnimation[this.EventAnim[3]].time = 7;
					this.Rival.CharacterAnimation.CrossFade(this.EventAnim[3]);
					this.Timer = 0;
					this.Phase++;
				}
			}
			//Osana kicks the machine
			else if (this.Phase == 4)
			{
				this.Timer += Time.deltaTime;

				if (this.Timer > 5)
				{
					AudioClipPlayer.Play(this.SpeechClip[4], 
						this.Rival.transform.position + (Vector3.up * 1.50f),
						5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

					this.Rival.CharacterAnimation[this.EventAnim[4]].speed = 0;
					this.Rival.CharacterAnimation.CrossFade(this.EventAnim[4]);
					this.MinimumDistance = 1;
					this.Timer = 0;
					this.Phase++;
				}
			}
			//Osana gives up and decides to leave
			else if (this.Phase == 5)
			{
				this.Timer += Time.deltaTime;

				if (this.Timer > .5f)
				{
					this.Rival.CharacterAnimation[this.EventAnim[4]].speed = 1;
					this.OsanaVandalismCollider.SetActive(true);
				}
				else
				{
					this.Location.position = Vector3.MoveTowards(
						this.Location.position,
						new Vector3(-2, 4, -31.7f),
						Time.deltaTime * 5);
				}

				Debug.Log(this.Rival.CharacterAnimation[this.EventAnim[4]].time);

				if (this.Rival.CharacterAnimation[this.EventAnim[4]].time < .3f)
				{
					this.PlaySound = true;
				}
				else
				{
					if (this.PlaySound)
					{
						//AudioSource.PlayClipAtPoint(this.Bang, transform.position);

						this.MyAudio.pitch = UnityEngine.Random.Range(.9f, 1.1f);
						this.MyAudio.Play();

						this.PlaySound = false;
					}
				}

				if (this.Rival.CharacterAnimation[this.EventAnim[4]].time > this.Rival.CharacterAnimation[this.EventAnim[4]].length)
				{
					this.Rival.CharacterAnimation[this.EventAnim[4]].time = 0;
				}

				if (this.Timer > 5.5f)
				{
					this.Rival.CharacterAnimation[this.EventAnim[4]].speed = 0;
					this.OsanaVandalismCollider.SetActive(false);
				}

				if (this.Timer > 6)
				{
					AudioClipPlayer.Play(this.SpeechClip[5], 
						this.Rival.transform.position + (Vector3.up * 1.50f),
						5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

					this.EventSubtitle.text = SpeechText[5];

					this.Rival.CharacterAnimation[this.EventAnim[5]].time = 0;
					this.Rival.CharacterAnimation.CrossFade(this.EventAnim[5]);
					this.Timer = 0;
					this.Phase++;
				}
			}
			//Osana reaches the end of the event. 
			else if (this.Phase == 6)
			{
				this.Timer += Time.deltaTime;

				if (this.Timer > 5)
				{
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
		Debug.Log ("Osana's vending machine event has ended.");

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

		if (!this.StudentManager.Stop)
		{
			this.StudentManager.UpdateStudents();
		}

		this.OsanaVandalismCollider.SetActive(false);

		this.Jukebox.Dip = 1;

		this.EventSubtitle.text = string.Empty;
		this.enabled = false;
	}
	#endif
}