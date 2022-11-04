using System;
using UnityEngine;

public class OsanaThursdayAfterClassEventScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public PhoneMinigameScript PhoneMinigame;
	public JukeboxScript Jukebox;
	public UILabel EventSubtitle;
	public YandereScript Yandere;
	public ClockScript Clock;

	public StudentScript Friend;
	public StudentScript Rival;

	public Transform FriendLocation;
	public Transform Location;

	public AudioClip[] SpeechClip;
	public string[] SpeechText;
	public string[] EventAnim;

	public GameObject AlarmDisc;
	public GameObject VoiceClip;

	public float FriendWarningTimer = 0.0f;
	public float Distance = 0.0f;
	public float Scale = 0.0f;
	public float Timer = 0.0f;

	public DayOfWeek EventDay = DayOfWeek.Sunday;

	public int FriendID = 10;
	public int RivalID = 11;
	public int Phase = 0;
	public int Frame = 0;

	public bool FriendWarned;
	public bool Sabotaged;

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

					if (this.StudentManager.Students[this.FriendID] != null &&
						!PlayerGlobals.RaibaruLoner)
					{
						this.Friend = this.StudentManager.Students[this.FriendID];
					}

					if (this.Clock.Period == 6)
					{
						if (!this.Rival.InEvent && !this.Rival.Phoneless)
						{
							Debug.Log ("Osana's Thursday after class event has begun.");

							this.Rival.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
							this.Rival.CharacterAnimation.Play (this.Rival.WalkAnim);
							this.Rival.Pathfinding.target = this.Location;
							this.Rival.CurrentDestination = this.Location;
							this.Rival.Pathfinding.canSearch = true;
							this.Rival.Pathfinding.canMove = true;
							this.Rival.Routine = false;
							this.Rival.InEvent = true;

							//this.Rival.Prompt.Hide ();
							//this.Rival.Prompt.enabled = false;

							this.Rival.Scrubber.SetActive(false);
							this.Rival.Eraser.SetActive(false);

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
					AudioClipPlayer.Play(this.SpeechClip[1], 
						this.Rival.transform.position + (Vector3.up * 1.50f),
						5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

					this.Rival.CharacterAnimation.CrossFade(this.EventAnim[1]);
					this.Rival.Pathfinding.canSearch = false;
					this.Rival.Pathfinding.canMove = false;
					this.Rival.Obstacle.enabled = true;
					this.Phase++;

					if (this.Friend != null)
					{
						//this.StudentManager.Patrols.List[this.FriendID].GetChild(0).transform.eulerAngles += new Vector3(0, 180, 0);
						//this.StudentManager.Patrols.List[this.FriendID].GetChild(2).transform.eulerAngles += new Vector3(0, 180, 0);

						ScheduleBlock block = this.Friend.ScheduleBlocks[7];
						block.destination = "Sketch";
						block.action = "Sketch";

						this.Friend.GetDestinations();

						this.Friend.SketchPosition = this.FriendLocation;
						this.Friend.CurrentDestination = this.Friend.SketchPosition;
						this.Friend.Pathfinding.target = this.Friend.SketchPosition;

						this.Friend.Restless = true;
					}
				}
			}
			//Display the first subtitle.
			else if (this.Phase == 2)
			{
				this.Rival.transform.position = Vector3.Lerp(
					this.Rival.transform.position, this.Rival.CurrentDestination.position, 10.0f * Time.deltaTime);

				this.Rival.transform.rotation = Quaternion.Slerp(
					this.Rival.transform.rotation, this.Rival.CurrentDestination.rotation, 10.0f * Time.deltaTime);

				if (this.Rival.CharacterAnimation[this.EventAnim[1]].time >= 3.2)
				{
					this.EventSubtitle.text = SpeechText[1];
					this.Timer = 0;
					this.Phase++;
				}
			}
			//Osana takes out her cell phone.
			else if (this.Phase == 3)
			{
				if (this.Rival.CharacterAnimation[this.EventAnim[1]].time >= 6)
				{
					this.Rival.SmartPhoneScreen.enabled = true;
					this.Rival.SmartPhone.SetActive(true);
					this.Phase++;
				}
			}
			//Osana puts down her cell phone.
			else if (this.Phase == 4)
			{
				if (this.Rival.CharacterAnimation[this.EventAnim[1]].time >= 13.33333)
				{
					this.OriginalPosition = this.Rival.SmartPhone.transform.localPosition;
					this.OriginalRotation = this.Rival.SmartPhone.transform.localEulerAngles;

					this.Rival.SmartPhone.transform.parent = null;
					this.Rival.SmartPhone.transform.position = new Vector3(.5f, 12.5042f, -29.365f);
					this.Rival.SmartPhone.transform.eulerAngles = new Vector3(0, 180, 180);

					this.Phase++;
				}
			}
			//Osana enters her sleeping animation.
			else if (this.Phase == 5)
			{
				if (this.Rival.CharacterAnimation [this.EventAnim[1]].time >= this.Rival.CharacterAnimation [this.EventAnim[1]].length) 
				{
					this.Rival.CharacterAnimation.Play(this.EventAnim[2]);
					this.PhoneMinigame.Prompt.enabled = true;
					this.Rival.Ragdoll.Zs.SetActive(true);
					this.EventSubtitle.text = "";
					this.Rival.Distracted = true;
					this.Phase++;

					this.StudentManager.UpdateMe(this.RivalID);
				}
			}
			//Time passes, and Yandere-chan has the opportunity to sabotage Osana's phone.
			else if (this.Phase == 6)
			{
				if (!this.Sabotaged && !this.PhoneMinigame.Tampering)
				{
					if (this.Friend != null)
					{
						if (!this.FriendWarned)
						{
							if (this.FriendWarningTimer == 0)
							{
								if (Vector3.Distance(this.Yandere.transform.position, this.Friend.transform.position) < 5)
								{
									AudioClipPlayer.Play(this.SpeechClip[3], 
									this.Friend.transform.position + (Vector3.up * 1.50f),
									5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

									this.EventSubtitle.text = SpeechText[3];
									this.FriendWarningTimer += Time.deltaTime;
								}
							}
							else
							{
								this.FriendWarningTimer += Time.deltaTime;

								if (this.FriendWarningTimer > 5)
								{
									this.FriendWarned = true;
								}
							}
						}
					}

					if (this.Clock.HourTime > 17.2)
					{
						AudioClipPlayer.Play(this.SpeechClip[2], 
							this.Rival.transform.position + (Vector3.up * 1.50f),
							5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

						this.Rival.CharacterAnimation.CrossFade(this.EventAnim[3]);
						this.Rival.Ragdoll.Zs.SetActive(false);
						this.Rival.Hurry = true;
						this.Phase++;

						this.PhoneMinigame.Prompt.enabled = false;
						this.PhoneMinigame.Prompt.Hide();
					}
				}
			}
			//Osana picks up her phone.
			else if (this.Phase == 7)
			{
				if (this.Rival.CharacterAnimation[this.EventAnim[3]].time >= 2.5) 
				{
					this.Rival.SmartPhone.transform.parent = this.Rival.ItemParent;
					this.Rival.SmartPhone.transform.localPosition = this.OriginalPosition;
					this.Rival.SmartPhone.transform.localEulerAngles = this.OriginalRotation;
					this.Phase++;
				}
			}
			//Osana puts away her phone.
			else if (this.Phase == 8)
			{
				if (this.Rival.CharacterAnimation[this.EventAnim[3]].time >= 3.5) 
				{
					this.Rival.SmartPhone.SetActive(false);
					this.Phase++;
				}
			}
			//Display the second subtitle.
			else if (this.Phase == 9)
			{
				if (this.Rival.CharacterAnimation[this.EventAnim[3]].time >= 4.65)
				{
					this.EventSubtitle.text = SpeechText[2];
					this.Phase++;
				}
			}
			//Osana's alarm goes off, she wakes up, and she ends the event. 
			else if (this.Phase == 10)
			{
				if (this.Rival.CharacterAnimation[this.EventAnim[3]].time >= this.Rival.CharacterAnimation[this.EventAnim[3]].length) 
				{
					this.EndEvent();
				}
			}

			////////////////////////////////////////////
			///// ACTIONS THAT COULD END THE EVENT /////
			////////////////////////////////////////////

			if (this.Rival.Alarmed)
			{
				this.EndEvent();
			}

			/////////////////////////
			///// SUBTITLE SIZE /////
			/////////////////////////

			if (!this.Sabotaged)
			{
				//Debug.Log("Got here.");

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

					//Debug.Log("Scale should be: " + Scale);

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
	}

	void EndEvent()
	{
		Debug.Log ("Osana's Thursday after class event has ended.");

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
		this.Rival.SmartPhoneScreen.enabled = false;
		this.Rival.Ragdoll.Zs.SetActive(false);
		this.Rival.Obstacle.enabled = false;
		this.Rival.Prompt.enabled = true;
		this.Rival.InEvent = false;
		this.Rival.Private = false;

		this.Rival.SmartPhone.transform.parent = this.Rival.ItemParent;
		this.Rival.SmartPhone.transform.localPosition = this.OriginalPosition;
		this.Rival.SmartPhone.transform.localEulerAngles = this.OriginalRotation;

		if (this.Friend != null)
		{
			ScheduleBlock block = this.Friend.ScheduleBlocks[7];
			block.destination = "Follow";
			block.action = "Follow";

			this.Friend.GetDestinations();

			this.Friend.CurrentDestination = this.Friend.FollowTarget.transform;
			this.Friend.Pathfinding.target = this.Friend.FollowTarget.transform;

			this.Friend.Restless = false;
			this.Friend.EmptyHands();
		}

		this.PhoneMinigame.Prompt.enabled = false;
		this.PhoneMinigame.Prompt.Hide();

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