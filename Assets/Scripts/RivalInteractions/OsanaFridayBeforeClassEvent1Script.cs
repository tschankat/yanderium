using System;
using UnityEngine;

public class OsanaFridayBeforeClassEvent1Script : MonoBehaviour
{
	public OsanaFridayBeforeClassEvent2Script OtherEvent;
	public StudentManagerScript StudentManager;
	public JukeboxScript Jukebox;
	public UILabel EventSubtitle;
	public YandereScript Yandere;
	public ClockScript Clock;

	public StudentScript Rival;

	public Transform Location;

	public AudioClip[] SpeechClip;
	public string[] SpeechText;
	public string EventAnim;

	public GameObject AlarmDisc;
	public GameObject VoiceClip;
	public GameObject Yoogle;

	public float Distance = 0.0f;
	public float Scale = 0.0f;
	public float Timer = 0.0f;

	public DayOfWeek EventDay = DayOfWeek.Sunday;

	public int RivalID = 11;
	public int Phase = 0;
	public int Frame = 0;

	public Vector3 OriginalPosition;
	public Vector3 OriginalRotation;

	void Start()
	{
		this.EventSubtitle.transform.localScale = Vector3.zero;

		if (DateGlobals.Weekday != this.EventDay)
		{
			this.enabled = false;
		}

		this.Yoogle.SetActive(false);
	}

	#if UNITY_EDITOR

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
						
					if (!this.Rival.InEvent && !this.Rival.Phoneless && this.Rival.Indoors && !this.OtherEvent.enabled)
					{
						Debug.Log ("Osana's Friday before class event has begun.");

						this.Rival.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
						this.Rival.CharacterAnimation.Play (this.Rival.WalkAnim);
						this.Rival.Pathfinding.target = this.Location;
						this.Rival.CurrentDestination = this.Location;
						this.Rival.Pathfinding.canSearch = true;
						this.Rival.Pathfinding.canMove = true;
						this.Rival.Routine = false;
						this.Rival.InEvent = true;

						this.Rival.Prompt.Hide ();
						this.Rival.Prompt.enabled = false;

						this.Phase++;
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
					AudioClipPlayer.Play(this.SpeechClip[1], 
						this.Rival.transform.position + (Vector3.up * 1.50f),
						5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);
					
					this.EventSubtitle.text = SpeechText[1];

					this.Rival.CharacterAnimation.CrossFade(this.EventAnim);
					this.Rival.Pathfinding.canSearch = false;
					this.Rival.Pathfinding.canMove = false;
					this.Rival.Obstacle.enabled = true;

					this.Yoogle.SetActive(true);

					this.Phase++;
				}
			}
			//Wait 60 seconds, end the event.
			else if (this.Phase == 2)
			{
				this.Rival.MoveTowardsTarget(this.Location.position);

				float Angle = Quaternion.Angle(this.Rival.transform.rotation, this.Location.rotation);

				if (Angle > 1.0f)
				{
					this.Rival.transform.rotation = Quaternion.Slerp(
						this.Rival.transform.rotation, this.Location.rotation, 10.0f * Time.deltaTime);
				}
					
				this.Timer += Time.deltaTime;

				if (Input.GetKeyDown ("space"))
				{
					this.Timer += 60;
				}

				if (this.Timer > 60)
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
		Debug.Log ("Osana's Friday before class event has ended.");

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
		this.Rival.SmartPhoneScreen.enabled = false;
		this.Rival.Ragdoll.Zs.SetActive(false);
		this.Rival.Obstacle.enabled = false;
		this.Rival.Prompt.enabled = true;
		this.Rival.InEvent = false;
		this.Rival.Private = false;

		this.Yoogle.SetActive(false);

		this.Rival.SmartPhone.transform.parent = this.Rival.ItemParent;
		this.Rival.SmartPhone.transform.localPosition = this.OriginalPosition;
		this.Rival.SmartPhone.transform.localEulerAngles = this.OriginalRotation;

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