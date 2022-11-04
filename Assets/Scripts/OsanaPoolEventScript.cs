using System;
using UnityEngine;

public class OsanaPoolEventScript : MonoBehaviour
{
#if UNITY_EDITOR
	public StudentManagerScript StudentManager;
	public JukeboxScript Jukebox;
	public UILabel EventSubtitle;
	public YandereScript Yandere;
	public PromptScript Prompt;
	public ClockScript Clock;

	public StudentScript Friend;
	public StudentScript Rival;

	public Transform[] Location;

	public AudioClip[] SpeechClip;
	public string[] SpeechText;
	public string[] EventAnim;

	public GameObject AlarmDisc;
	public GameObject BigSplash;
	public GameObject VoiceClip;
	public GameObject Weight;

	public bool Murdering = false;

	public float Distance = 0.0f;
	public float Scale = 0.0f;
	public float Timer = 0.0f;

	public DayOfWeek EventDay = DayOfWeek.Sunday;

	public int MurderPhase = 1;
	public int FriendID = 10;
	public int RivalID = 11;
	public int Phase = 0;
	public int Frame = 0;

	void Start()
	{
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
						Debug.Log("Osana's pool event has begun.");

						if (this.StudentManager.Students[this.FriendID] != null &&
							!PlayerGlobals.RaibaruLoner)
						{
							this.Friend = this.StudentManager.Students[this.FriendID];
						}

						this.Rival = this.StudentManager.Students[this.RivalID];

						this.Rival.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
						this.Rival.CharacterAnimation.CrossFade(this.Rival.WalkAnim);
						this.Rival.Pathfinding.target = this.StudentManager.FemaleStripSpot;
						this.Rival.CurrentDestination = this.StudentManager.FemaleStripSpot;
						this.Rival.Pathfinding.canSearch = true;
						this.Rival.Pathfinding.canMove = true;
						this.Rival.Pen.SetActive(false);
						this.Rival.Routine = false;
						this.Rival.InEvent = true;

						this.Rival.Prompt.Hide();
						this.Rival.Prompt.enabled = false;
						this.Rival.SmartPhone.SetActive(false);

						this.Phase++;
					}
				}
			}

			this.Frame++;
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				if (this.Phase == 1)
				{
					this.Yandere.transform.position =
						this.StudentManager.FemaleStripSpot.position + new Vector3(-2.0f, 0.0f, 0.0f);
					this.Rival.transform.position =
						this.StudentManager.FemaleStripSpot.position + new Vector3(-1.0f, 0.0f, 0.0f);
				}
				else if (this.Phase == 3)
				{
					this.Rival.transform.position = this.Location[1].position + new Vector3(1, 0.0f, 0.0f);
					this.Yandere.transform.position = this.Location[1].position + new Vector3(2, 0.0f, 0.0f);
					this.Weight.transform.position = this.Location[1].position + new Vector3(3, 0.0f, 0.0f);
				}
				else if (this.Phase == 5)
				{
					this.Timer += 60.0f;
				}
				else if (this.Phase == 6)
				{
					this.Timer += 600.0f;
				}
				else if (this.Phase == 7)
				{
					this.Timer += 60.0f;
				}

				Physics.SyncTransforms();
			}

			// Go to locker room.
			if (this.Phase == 1)
			{
				if (this.Rival.DistanceToDestination < 0.50f)
				{
					if (this.StudentManager.CommunalLocker.Student == null)
					{
						this.Rival.StudentManager.CommunalLocker.Open = true;
						this.Rival.StudentManager.CommunalLocker.Student = this.Rival;
						this.Rival.StudentManager.CommunalLocker.SpawnSteam();
						this.Rival.Schoolwear = 2;
						this.Phase++;
					}
					else
					{
						this.Rival.CharacterAnimation.CrossFade(this.Rival.IdleAnim);
					}
				}
			}
			// Change into swimsuit.
			else if (this.Phase == 2)
			{
				if (!this.Rival.StudentManager.CommunalLocker.SteamCountdown)
				{
					this.StudentManager.CommunalLocker.Student = null;

					if (this.Friend != null)
					{

						ScheduleBlock CurrentBlock = this.Friend.ScheduleBlocks[this.Friend.Phase];
						CurrentBlock.destination = "Sunbathe";
						CurrentBlock.action = "Sunbathe";

						this.Friend.Actions[this.Friend.Phase] = StudentActionType.Sunbathe;
						this.Friend.CurrentAction = StudentActionType.Sunbathe;

						this.Friend.GetDestinations();

						this.Friend.CurrentDestination = this.StudentManager.FemaleStripSpot;
						this.Friend.Pathfinding.target = this.StudentManager.FemaleStripSpot;

						this.Friend.Pathfinding.canSearch = true;
						this.Friend.Pathfinding.canMove = true;
					}

					this.Rival.Pathfinding.target = this.Location[1];
					this.Rival.CurrentDestination = this.Location[1];

					this.Phase++;
				}
			}
			// Laying down.
			else if (this.Phase == 3)
			{
				if (this.Rival.DistanceToDestination < 0.50f)
				{
					AudioClipPlayer.Play(this.SpeechClip[1],
						this.Rival.transform.position + (Vector3.up * 1.50f),
						5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

					this.EventSubtitle.text = this.SpeechText[1];

					this.Rival.CharacterAnimation["f02_" + this.EventAnim[1]].time = 0.0f;
					this.Rival.CharacterAnimation.Play("f02_" + this.EventAnim[1]);

					this.Rival.OsanaHair.GetComponent<Animation>().Play("Hair_" + this.EventAnim[1]);
					this.Rival.OsanaHair.transform.parent = this.Rival.transform;
					this.Rival.OsanaHair.transform.localEulerAngles = Vector3.zero;
					this.Rival.OsanaHair.transform.localPosition = Vector3.zero;
					this.Rival.OsanaHair.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
					this.Rival.OsanaHairL.enabled = false;
					this.Rival.OsanaHairR.enabled = false;

					this.Phase++;
				}
			}
			// Sunbathing.
			else if (this.Phase == 4)
			{
				this.Timer += Time.deltaTime;

				if (this.Timer > 5.53333f)
				//if (Rival.CharacterAnimation["f02_" + EventAnim[1]].time >= Rival.CharacterAnimation["f02_" + EventAnim[1]].length);
				{
					this.Rival.CharacterAnimation.CrossFade("f02_" + this.EventAnim[2]);
					this.Rival.OsanaHair.GetComponent<Animation>().CrossFade("Hair_" + this.EventAnim[2]);

					this.Timer = 0.0f;
					this.Phase++;
				}
			}
			// Go to sleep.
			else if (this.Phase == 5)
			{
				this.Timer += Time.deltaTime;

				if (this.Timer > 10.0f)
				{
					AudioClipPlayer.Play(this.SpeechClip[2],
						this.Rival.transform.position + (Vector3.up * 1.50f),
						5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

					this.EventSubtitle.text = this.SpeechText[2];

					this.Rival.CharacterAnimation.CrossFade("f02_" + this.EventAnim[3]);
					this.Rival.OsanaHair.GetComponent<Animation>().CrossFade("Hair_" + this.EventAnim[3]);

					this.Rival.Ragdoll.Zs.SetActive(true);
					this.Timer = 0.0f;
					this.Phase++;
				}
			}
			// Wake up.
			else if (this.Phase == 6)
			{
				if (!this.Murdering)
				{
					this.Timer += Time.deltaTime;

					if (this.Clock.HourTime > 13.375f)
					{
						AudioClipPlayer.Play(this.SpeechClip[3],
							this.Rival.transform.position + (Vector3.up * 1.50f),
							5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

						this.EventSubtitle.text = this.SpeechText[3];

						this.Rival.CharacterAnimation.CrossFade("f02_" + this.EventAnim[2]);
						this.Rival.OsanaHair.GetComponent<Animation>().CrossFade("Hair_" + this.EventAnim[2]);

						this.Prompt.Hide();
						this.Prompt.gameObject.SetActive(false);
						this.Rival.Ragdoll.Zs.SetActive(false);
						this.Timer = 0.0f;
						this.Phase++;
					}
				}
			}
			// Get up.
			else if (this.Phase == 7)
			{
				this.Timer += Time.deltaTime;

				if (this.Timer > 5.0f)
				{
					AudioClipPlayer.Play(this.SpeechClip[4],
						this.Rival.transform.position + (Vector3.up * 1.50f),
						5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

					this.EventSubtitle.text = this.SpeechText[4];

					this.Rival.CharacterAnimation.CrossFade("f02_" + this.EventAnim[4]);
					this.Rival.OsanaHair.GetComponent<Animation>().CrossFade("Hair_" + this.EventAnim[4]);

					this.Timer = 0.0f;
					this.Phase++;
				}
			}
			// Go to locker room.
			else if (this.Phase == 8)
			{
				this.Timer += Time.deltaTime;

				if (this.Timer > 4.33333f)
				//if (Rival.CharacterAnimation["f02_" + EventAnim[4]].time >= Rival.CharacterAnimation["f02_" + EventAnim[4]].length);
				{
					this.Rival.OsanaHair.GetComponent<Animation>().Stop();
					this.Rival.OsanaHair.transform.parent = this.Rival.Head;
					this.Rival.OsanaHair.transform.localEulerAngles = Vector3.zero;
					this.Rival.OsanaHair.transform.localPosition = new Vector3(0.0f, -1.442789f, 0.01900469f);
					this.Rival.OsanaHair.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
					this.Rival.OsanaHairL.enabled = true;
					this.Rival.OsanaHairR.enabled = true;

					this.Rival.Pathfinding.target = this.StudentManager.FemaleStripSpot;
					this.Rival.CurrentDestination = this.StudentManager.FemaleStripSpot;

					if (this.Friend != null)
					{
						ScheduleBlock CurrentBlock = this.Friend.ScheduleBlocks[this.Friend.Phase];
						CurrentBlock.destination = "Follow";
						CurrentBlock.action = "Follow";

						this.Friend.GetDestinations();

						this.Friend.CurrentDestination = this.Friend.FollowTarget.transform;
						this.Friend.Pathfinding.target = this.Friend.FollowTarget.transform;

						this.Friend.Pathfinding.canSearch = true;
						this.Friend.Pathfinding.canMove = true;
					}

					this.Phase++;
				}
			}
			// Change into school uniform.
			else if (this.Phase == 9)
			{
				if (this.Rival.DistanceToDestination < 0.50f)
				{
					if (this.StudentManager.CommunalLocker.Student == null)
					{
						this.Rival.StudentManager.CommunalLocker.Open = true;
						this.Rival.StudentManager.CommunalLocker.Student = this.Rival;
						this.Rival.StudentManager.CommunalLocker.SpawnSteam();
						this.Rival.Schoolwear = 1;

						if (this.Friend != null)
						{
							this.StudentManager.CommunalLocker.SpawnSteamNoSideEffects(this.Friend);
						}

						this.Phase++;
					}
					else
					{
						this.Rival.CharacterAnimation.CrossFade(this.Rival.IdleAnim);
					}
				}
			}
			else if (this.Phase == 10)
			{
				if (!this.Rival.StudentManager.CommunalLocker.SteamCountdown)
				{
					this.Rival.StudentManager.CommunalLocker.Student = null;
					this.EndEvent();
				}
			}

			//////////////////
			///// MURDER /////
			//////////////////

			if (this.Phase == 6)
			{
				if (this.Yandere.PickUp != null)
				{
					// [af] Replaced if/else statement with boolean expression.
					this.Prompt.enabled = this.Yandere.PickUp.Weight;
				}
				else
				{
					this.Prompt.enabled = false;
				}
			}

			if (this.Prompt.Circle[0].fillAmount == 0.0f)
			{
				// [af] Commented in JS code.
				//Yandere.EmptyHands();
				//Weight.transform.parent = Location[3];
				//Weight.transform.localPosition = Vector3(0, 0, 0);
				//Weight.transform.localEulerAngles = Vector3(0, 0, 0);
				//Weight.animation.Play("Weight_" + EventAnim[5]);

				this.Prompt.Hide();
				this.Prompt.gameObject.SetActive(false);

				this.Murdering = true;
				this.Yandere.CanMove = false;
				this.Rival.Ragdoll.Zs.SetActive(false);
				this.Yandere.CharacterAnimation.CrossFade("f02_" + this.EventAnim[5]);
				this.Rival.CharacterAnimation.CrossFade("f02_" + this.EventAnim[6]);
				this.Rival.OsanaHair.GetComponent<Animation>().CrossFade("Hair_" + this.EventAnim[6]);
			}

			if (this.Murdering)
			{
				this.Yandere.MoveTowardsTarget(this.Location[2].position);
				this.Yandere.transform.rotation = Quaternion.Slerp(
					this.Yandere.transform.rotation, this.Location[2].rotation, Time.deltaTime * 10.0f);

				if (this.Yandere.CharacterAnimation["f02_" + this.EventAnim[5]].time > 1.0f)
				{
					if (this.Weight.transform.parent != this.Location[3])
					{
						this.Yandere.EmptyHands();
						this.Weight.transform.parent = this.Location[3];
						this.Weight.transform.localPosition = Vector3.zero;
						this.Weight.transform.localEulerAngles = Vector3.zero;
						this.Weight.GetComponent<Animation>().Play("Weight_" + this.EventAnim[5]);
						this.Weight.GetComponent<Animation>()["Weight_" + this.EventAnim[5]].time =
							this.Yandere.CharacterAnimation["f02_" + this.EventAnim[5]].time;
					}
				}

				if (this.MurderPhase == 1)
				{
					if (this.Yandere.CharacterAnimation["f02_" + this.EventAnim[5]].time > 10.0f)
					{
						AudioClipPlayer.Play(this.SpeechClip[5],
							this.Rival.transform.position + (Vector3.up * 1.50f),
							5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

						this.EventSubtitle.text = this.SpeechText[5];
						this.MurderPhase++;
					}
				}
				else if (this.MurderPhase == 2)
				{
					if (this.Yandere.CharacterAnimation["f02_" + this.EventAnim[5]].time > 14.5f)
					{
						/*
						GameObject NewSplash = Instantiate(this.BigSplash,
							this.Weight.transform.position, Quaternion.identity);
						*/

						GameObject NewSplash = Instantiate(this.BigSplash, Location[4].position, Quaternion.identity);

						NewSplash.transform.eulerAngles = new Vector3(
							-90.0f,
							NewSplash.transform.eulerAngles.y,
							NewSplash.transform.eulerAngles.z);

						this.MurderPhase++;
					}
				}
				else if (this.MurderPhase == 3)
				{
					if (this.Yandere.CharacterAnimation["f02_" + this.EventAnim[5]].time > 14.833333f)
					{
						/*
						GameObject NewerSplash = Instantiate(this.BigSplash,
							this.Rival.Head.transform.position, Quaternion.identity);
						*/

						GameObject NewerSplash = Instantiate(this.BigSplash, Location[4].position, Quaternion.identity);

						NewerSplash.transform.eulerAngles = new Vector3(
							-90.0f,
							NewerSplash.transform.eulerAngles.y,
							NewerSplash.transform.eulerAngles.z);

						this.MurderPhase++;
					}
				}

				if (this.Yandere.CharacterAnimation["f02_" + this.EventAnim[5]].time >
					this.Yandere.CharacterAnimation["f02_" + this.EventAnim[5]].length)
				{
					this.Yandere.CanMove = true;
					this.Murdering = false;
				}
			}

			////////////////////////////////////////////
			///// ACTIONS THAT COULD END THE EVENT /////
			////////////////////////////////////////////

			if (this.Clock.HourTime > 13.50f)
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

	public void EndEvent()
	{
		Debug.Log("Osana's pool event has ended.");

		if (this.VoiceClip != null)
		{
			Destroy(this.VoiceClip);
		}

		if (!this.Rival.Alarmed)
		{
			this.Rival.Pathfinding.canSearch = true;
			this.Rival.Pathfinding.canMove = true;
			this.Rival.Routine = true;
		}

		this.Rival.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
		this.Rival.Ragdoll.Zs.SetActive(false);
		this.Rival.Obstacle.enabled = false;
		this.Rival.Prompt.enabled = true;
		this.Rival.InEvent = false;
		this.Rival.Private = false;

		if (!this.StudentManager.Stop)
		{
			this.StudentManager.UpdateStudents();
		}

		this.Rival.OsanaHair.GetComponent<Animation>().Stop();
		this.Rival.OsanaHair.transform.parent = this.Rival.Head;
		this.Rival.OsanaHair.transform.localEulerAngles = Vector3.zero;
		this.Rival.OsanaHair.transform.localPosition = new Vector3(0.0f, -1.442789f, 0.01900469f);
		this.Rival.OsanaHair.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		this.Rival.OsanaHairL.enabled = true;
		this.Rival.OsanaHairR.enabled = true;

		this.Rival.Schoolwear = 1;
		this.Rival.ChangeSchoolwear();

		if (this.Friend != null)
		{
			ScheduleBlock CurrentBlock = this.Friend.ScheduleBlocks[this.Friend.Phase];
			CurrentBlock.destination = "Follow";
			CurrentBlock.action = "Follow";

			this.Friend.GetDestinations();

			this.Friend.CurrentDestination = this.Friend.FollowTarget.transform;
			this.Friend.Pathfinding.target = this.Friend.FollowTarget.transform;

			this.Friend.Pathfinding.canSearch = true;
			this.Friend.Pathfinding.canMove = true;
		}

		this.EventSubtitle.text = string.Empty;
		this.enabled = false;

		this.Jukebox.Dip = 1;
	}
#endif
}