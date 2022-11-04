using System;
using UnityEngine;

public class OsanaTuesdayLunchEventScript : MonoBehaviour
{
#if UNITY_EDITOR
	public RivalAfterClassEventManagerScript AfterClassEvent;
	public StudentManagerScript StudentManager;
	public JukeboxScript Jukebox;
	public PromptScript PushPrompt;
	public UILabel EventSubtitle;
	public YandereScript Yandere;
	public ClockScript Clock;

	public StudentScript Friend;
	public StudentScript Rival;

	public Transform[] Location;

	public AudioClip[] SpeechClip;
	public string[] SpeechText;
	public string[] EventAnim;

	public GameObject AlarmDisc;
	public GameObject VoiceClip;

	public bool Sabotaging = false;
	public bool Sabotaged = false;

	public float Distance = 0.0f;
	public float Scale = 0.0f;
	public float Timer = 0.0f;

	public DayOfWeek EventDay = DayOfWeek.Sunday;

	public int StretchPhase = 0;
	public int FriendID = 10;
	public int RivalID = 11;
	public int Phase = 0;
	public int Frame = 0;

	void Start()
	{
		this.EventSubtitle.transform.localScale = Vector3.zero;
		this.PushPrompt.gameObject.SetActive(false);

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
					if (this.Clock.Period == 3)
					{
						Debug.Log("Osana's Tuesday lunchtime event has begun.");

						this.Rival = this.StudentManager.Students[this.RivalID];

						this.Rival.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
						this.Rival.CharacterAnimation.Play(this.Rival.WalkAnim);
						this.Rival.Pathfinding.target = this.Location[1];
						this.Rival.CurrentDestination = this.Location[1];
						this.Rival.Pathfinding.canSearch = true;
						this.Rival.Pathfinding.canMove = true;
						this.Rival.Routine = false;
						this.Rival.InEvent = true;
						this.Rival.EmptyHands();

						this.Rival.Prompt.Hide();
						this.Rival.Prompt.enabled = false;

						if (this.StudentManager.Students[this.FriendID] != null &&
							!PlayerGlobals.RaibaruLoner)
						{
							this.Friend = this.StudentManager.Students[this.FriendID];

							ScheduleBlock newBlock = this.Friend.ScheduleBlocks[4];
							newBlock.destination = "Patrol";
							newBlock.action = "Patrol";

							this.Friend.GetDestinations();
						}

						this.Phase++;
					}
				}
			}

			this.Frame++;
		}
		else
		{
			//Osana sits down at the fountain and begins to read the book.
			if (this.Phase == 1)
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					this.Yandere.transform.position = new Vector3(0.0f, 0.10f, -6.0f);
					this.Rival.transform.position = new Vector3(0.0f, 0.10f, -5.0f);
				}

				if (this.Rival.DistanceToDestination < 0.50f)
				{
					AudioClipPlayer.Play(this.SpeechClip[1], 
						this.Rival.transform.position + (Vector3.up * 1.50f),
						5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

					this.Rival.CharacterAnimation.CrossFade("f02_" + this.EventAnim[1]);
					this.Rival.AnimatedBook.GetComponent<Animation>().CrossFade(this.EventAnim[1]);
					this.Rival.Pathfinding.canSearch = false;
					this.Rival.Pathfinding.canMove = false;
					this.Rival.Obstacle.enabled = true;
					this.Phase++;
				}
			}
			//Osana reaches the end of the "take out book" animation and begins the looping "read book" animation. 
			else if (this.Phase == 2)
			{
				if (this.Rival.CharacterAnimation["f02_" + this.EventAnim [1]].time >= 0.833333f)
				{
					this.Rival.AnimatedBook.SetActive(true);
				}

				if (this.Rival.CharacterAnimation["f02_" + this.EventAnim[1]].time >= 5.0f)
				{
					this.EventSubtitle.text = this.SpeechText[1];
				}

				if (this.Rival.CharacterAnimation["f02_" + this.EventAnim[1]].time >=
					this.Rival.CharacterAnimation["f02_" + this.EventAnim[1]].length)
				{
					this.Rival.CharacterAnimation.CrossFade("f02_" + this.EventAnim[2]);
					this.Rival.AnimatedBook.GetComponent<Animation>().CrossFade(this.EventAnim[2]);
					this.Phase++;
				}
			}
			//Osana spends 60 seconds in the "read book" animation and then stands up.
			else if (this.Phase == 3)
			{
				this.Timer += Time.deltaTime;

				if (this.Timer > 60.0f)
				{
					AudioClipPlayer.Play(this.SpeechClip[2], 
						this.Rival.transform.position + (Vector3.up * 1.50f),
						5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

					this.Rival.CharacterAnimation.CrossFade("f02_" + this.EventAnim[3]);
					this.Rival.AnimatedBook.GetComponent<Animation>().CrossFade(this.EventAnim[3]);
					this.EventSubtitle.text = this.SpeechText[2];
					this.StretchPhase = 2;
					this.Phase++;
				}
			}
			//Osana puts the book down and then begins to walk around the fountain.
			else if (this.Phase == 4)
			{
				if (this.Rival.CharacterAnimation["f02_" + this.EventAnim[3]].time >=
					this.Rival.CharacterAnimation["f02_" + this.EventAnim[3]].length)
				{
					this.Rival.AnimatedBook.transform.parent = null;
					this.PushPrompt.gameObject.SetActive(true);
					this.PushPrompt.enabled = true;

					this.Rival.CharacterAnimation.CrossFade(this.Rival.WalkAnim);
					this.Rival.Pathfinding.target = this.Location[this.StretchPhase];
					this.Rival.CurrentDestination = this.Location[this.StretchPhase];
					this.Rival.DistanceToDestination = 100.0f;
					this.Rival.Pathfinding.canSearch = true;
					this.Rival.Pathfinding.canMove = true;
					this.Phase++;
				}
			}
			//Osana walks around the fountain, performing the stretching animation a few times.
			else if (this.Phase == 5)
			{
				if (this.Rival.DistanceToDestination < 0.50f)
				{
					if (this.StretchPhase == 2)
					{
						AudioClipPlayer.Play(this.SpeechClip[3], 
							this.Rival.transform.position + (Vector3.up * 1.50f),
							5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

						this.EventSubtitle.text = this.SpeechText[3];
					}

					this.Rival.CharacterAnimation.CrossFade("f02_" + this.EventAnim[4]);
					this.Rival.Pathfinding.canSearch = false;
					this.Rival.Pathfinding.canMove = false;
					this.Phase++;
				}
			}
			//Osana finishes stretching, returns to the book, and either picks it up or reacts to the player's sabotage.
			else if (this.Phase == 6)
			{
				if (this.Rival.CharacterAnimation["f02_" + this.EventAnim[4]].time >=
					this.Rival.CharacterAnimation["f02_" + this.EventAnim[4]].length)
				{
					this.Rival.CharacterAnimation.CrossFade(this.Rival.WalkAnim);
					this.StretchPhase++;

					if (this.StretchPhase < 6)
					{
						this.Rival.Pathfinding.target = this.Location[this.StretchPhase];
						this.Rival.CurrentDestination = this.Location[this.StretchPhase];
						this.Rival.DistanceToDestination = 100.0f;
						this.Rival.Pathfinding.canSearch = true;
						this.Rival.Pathfinding.canMove = true;
						this.Phase--;
					}
					else
					{
						this.PushPrompt.gameObject.SetActive(false);

						if (!this.Sabotaged)
						{
							this.Rival.Pathfinding.target = this.Location[1];
							this.Rival.CurrentDestination = this.Location[1];
							this.Rival.DistanceToDestination = 100.0f;
							this.Rival.Pathfinding.canSearch = true;
							this.Rival.Pathfinding.canMove = true;
						}
						else
						{
							this.Rival.Pathfinding.target = this.Location[7];
							this.Rival.CurrentDestination = this.Location[7];
							this.Rival.DistanceToDestination = 100.0f;
							this.Rival.Pathfinding.canSearch = true;
							this.Rival.Pathfinding.canMove = true;
						}

						this.Phase++;
					}
				}
			}
			else if (this.Phase == 7)
			{
				if (!this.Sabotaged)
				{
					if (this.Rival.DistanceToDestination < 0.50f)
					{
						AudioClipPlayer.Play(this.SpeechClip[4], 
							this.Rival.transform.position + (Vector3.up * 1.50f),
							5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

						this.Rival.CharacterAnimation.CrossFade("f02_" + this.EventAnim[5]);
						this.Rival.AnimatedBook.GetComponent<Animation>().CrossFade(this.EventAnim[5]);
						this.EventSubtitle.text = this.SpeechText[4];
						this.Phase++;
					}
				}
				//If the player DID sabotage the event...
				else
				{
					if (this.Rival.DistanceToDestination < 0.50f)
					{
						this.Rival.WalkAnim = AnimNames.FemaleSadWalk;
						this.Rival.SitAnim = AnimNames.FemaleSadDeskSit;

						AudioClipPlayer.Play(this.SpeechClip[6], 
							this.Rival.transform.position + (Vector3.up * 1.50f),
							5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

						this.Rival.CharacterAnimation.CrossFade("f02_" + this.EventAnim[8]);
						this.Rival.AnimatedBook.GetComponent<Animation>().CrossFade(this.EventAnim[8]);
						this.EventSubtitle.text = this.SpeechText[6];
						this.Rival.Depressed = true;
						this.Phase = 11;
					}
				}
			}
			else if (this.Phase == 8)
			{
				if (this.Rival.CharacterAnimation["f02_" + this.EventAnim[5]].time >=
					this.Rival.CharacterAnimation["f02_" + this.EventAnim[5]].length)
				{
					this.Rival.CharacterAnimation.CrossFade("f02_" + this.EventAnim[2]);
					this.Rival.AnimatedBook.GetComponent<Animation>().CrossFade(this.EventAnim[2]);
					this.Phase++;
				}
			}
			else if (this.Phase == 9)
			{
				if (this.Clock.HourTime > 13.375f)
				{
					AudioClipPlayer.Play(this.SpeechClip[5], 
						this.Rival.transform.position + (Vector3.up * 1.50f),
						5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

					this.Rival.CharacterAnimation.CrossFade("f02_" + this.EventAnim[6]);
					this.Rival.AnimatedBook.GetComponent<Animation>().CrossFade(this.EventAnim[6]);
					this.EventSubtitle.text = this.SpeechText[5];
					this.Phase++;
				}
			}
			else if (this.Phase == 10)
			{
				if (this.Rival.AnimatedBook.activeInHierarchy)
				{
					if (this.Rival.CharacterAnimation["f02_" + this.EventAnim[6]].time > 2.0f)
					{
						this.Rival.AnimatedBook.SetActive(false);
					}
				}

				if (this.Rival.CharacterAnimation["f02_" + this.EventAnim[6]].time >=
					this.Rival.CharacterAnimation["f02_" + this.EventAnim[6]].length)
				{
					this.EndEvent();
				}
			}
			else if (this.Phase == 11)
			{
				if (this.Rival.AnimatedBook.activeInHierarchy)
				{
					if (this.Rival.CharacterAnimation["f02_" + this.EventAnim[8]].time > 7.0f)
					{
						this.Rival.AnimatedBook.SetActive(false);
					}
				}

				if (this.Rival.CharacterAnimation["f02_" + this.EventAnim[8]].time >=
					this.Rival.CharacterAnimation["f02_" + this.EventAnim[8]].length)
				{
					this.Rival.Destinations[4] = this.Location[8];
					this.Friend.Destinations[4] = this.Location[9];
					this.StudentManager.LunchSpots.List[this.FriendID] = this.Location[9];

					this.EndEvent();
				}
			}

			////////////////////
			///// SABOTAGE /////
			////////////////////

			if (this.PushPrompt.Circle[0].fillAmount == 0.0f)
			{
				this.PushPrompt.Hide();
				this.PushPrompt.gameObject.SetActive(false);

				this.Sabotaging = true;
				this.Yandere.CanMove = false;
				this.Yandere.CharacterAnimation.CrossFade("f02_" + this.EventAnim[7]);
				this.Rival.AnimatedBook.GetComponent<Animation>().Play(this.EventAnim[7]);

				this.Rival.AnimatedBook.transform.eulerAngles = new Vector3(
					this.Rival.AnimatedBook.transform.eulerAngles.x,
					0.0f,
					this.Rival.AnimatedBook.transform.eulerAngles.z);

				this.Rival.AnimatedBook.transform.position = new Vector3(
					this.Rival.AnimatedBook.transform.position.x,
					this.Rival.AnimatedBook.transform.position.y,
					-2.80f);

				this.AfterClassEvent.Sabotaged = true;
			}

			if (this.Sabotaging)
			{
				this.Yandere.MoveTowardsTarget(this.Location[6].position);
				this.Yandere.transform.rotation = Quaternion.Slerp(
					this.Yandere.transform.rotation, this.Location[6].rotation, Time.deltaTime * 10.0f);

				if ((this.Yandere.CharacterAnimation["f02_" + this.EventAnim[7]].time > 1.50f) &&
					(this.Yandere.CharacterAnimation["f02_" + this.EventAnim[7]].time < 2.0f))
				{
					AudioSource audioSource = this.GetComponent<AudioSource>();

					if (!audioSource.isPlaying)
					{
						audioSource.Play();
					}
				}

				if (this.Yandere.CharacterAnimation["f02_" + this.EventAnim[7]].time >
					this.Yandere.CharacterAnimation["f02_" + this.EventAnim[7]].length)
				{
					this.Yandere.CanMove = true;
					this.Sabotaging = false;
					this.Sabotaged = true;
				}
			}

			////////////////////////////////////////////
			///// ACTIONS THAT COULD END THE EVENT /////
			////////////////////////////////////////////

			if (this.Clock.Period > 3)
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
		Debug.Log("Osana's Tuesday lunchtime event ended.");

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

		if (this.Friend != null)
		{
			ScheduleBlock newBlock = this.Friend.ScheduleBlocks[4];
			newBlock.destination = "Follow";
			newBlock.action = "Follow";

			this.Friend.GetDestinations();

			this.Friend.Pathfinding.target = this.Friend.FollowTarget.transform;
			this.Friend.CurrentDestination = this.Friend.FollowTarget.transform;

			Debug.Log("Raibaru was told to resume ''Follow'' protocol.");
		}

		this.Rival.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
		this.Rival.AnimatedBook.SetActive(false);
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