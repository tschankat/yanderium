using System;
using UnityEngine;

public class ToiletEventScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public LightSwitchScript LightSwitch;
	public BucketPourScript BucketPour;
	public ParticleSystem Splashes;
	public UILabel EventSubtitle;
	public YandereScript Yandere;
	public DoorScript StallDoor;
	public PromptScript Prompt;
	public ClockScript Clock;
	public Collider Toilet;

	public StudentScript EventStudent;
	public Transform[] EventLocation;
	public AudioClip[] EventClip;
	public string[] EventSpeech;
	public string[] EventAnim;

	public GameObject VoiceClip;

	public bool EventActive = false;
	public bool EventCheck = false;
	public bool EventOver = false;

	public float EventTime = 7.0f;
	public int EventPhase = 1;
	public DayOfWeek EventDay = DayOfWeek.Thursday;

	public float ToiletCountdown = 0.0f;
	public float Distance = 0.0f;
	public float Timer = 0.0f;

	void Start()
	{
		this.EventSubtitle.transform.localScale = Vector3.zero;

		if (DateGlobals.Weekday == this.EventDay)
		{
			this.EventCheck = true;
		}
	}

	void Update()
	{
		if (!this.Clock.StopTime)
		{
			if (this.EventCheck)
			{
				if (this.Clock.HourTime > this.EventTime)
				{
					this.EventStudent = this.StudentManager.Students[30];

					if (this.EventStudent != null)
					{
						if (this.EventStudent.Routine &&
							!this.EventStudent.Distracted &&
							!this.EventStudent.Talking &&
							!this.EventStudent.Alarmed &&
							!this.EventStudent.Meeting)
						{
							if (!this.EventStudent.WitnessedMurder)
							{
								this.EventStudent.CharacterAnimation.CrossFade(this.EventStudent.WalkAnim);

								this.EventStudent.CurrentDestination = this.EventLocation[1];
								this.EventStudent.Pathfinding.target = this.EventLocation[1];

								this.EventStudent.Pathfinding.canSearch = true;
								this.EventStudent.Pathfinding.canMove = true;
								this.EventStudent.LightSwitch = this.LightSwitch;
								this.EventStudent.Obstacle.checkTime = 99.0f;
								this.EventStudent.SpeechLines.Stop();
								this.EventStudent.ToiletEvent = this;
								//this.EventStudent.Routine = false;
								this.EventStudent.InEvent = true;

								// [af] Commented in JS code.
								//EventStudent.Private = true;

								this.EventStudent.Prompt.Hide();

								this.Prompt.enabled = true;
								this.EventCheck = false;
								this.EventActive = true;

								if (this.EventStudent.Following)
								{
									this.EventStudent.Pathfinding.speed = 1.0f;
									this.EventStudent.Following = false;
									this.EventStudent.Routine = true;

                                    this.Yandere.Follower = null;
                                    this.Yandere.Followers--;

									this.EventStudent.Subtitle.UpdateLabel(SubtitleType.StopFollowApology, 0, 3.0f);
									this.EventStudent.Prompt.Label[0].text = "     " + "Talk";
								}
							}
							else
							{
								this.enabled = false;
							}
						}
					}
				}
			}
		}

		if (this.EventActive)
		{
			if (this.Prompt.Circle[0].fillAmount == 0.0f)
			{
				this.Yandere.EmptyHands();

				this.Prompt.Hide();
				this.Prompt.enabled = false;

				this.EventPhase = 5;
				this.Timer = 0.0f;

				AudioClipPlayer.Play(this.EventClip[1],
					this.EventStudent.transform.position + (Vector3.up * 1.50f),
					5.0f, 10.0f, out this.VoiceClip);

				this.EventSubtitle.text = this.EventSpeech[1];

				this.EventStudent.MyController.enabled = false;
				this.EventStudent.Distracted = true;
				this.EventStudent.Routine = false;
				this.EventStudent.Drowned = true;

				this.Yandere.TargetStudent = this.EventStudent;
				this.Yandere.Attacking = true;
				this.Yandere.CanMove = false;
				this.Yandere.Drown = true;

				this.Yandere.DrownAnim = AnimNames.FemaleToiletDrownA;
				this.EventStudent.DrownAnim = AnimNames.FemaleToiletDrownB;

				this.EventStudent.CharacterAnimation.CrossFade(this.EventStudent.DrownAnim);
			}

			if ((this.Clock.HourTime > (this.EventTime + 0.50f)) ||
				this.EventStudent.WitnessedMurder ||
				this.EventStudent.Splashed ||
				this.EventStudent.Dying || 
				this.EventStudent.Alarmed)
			{
				this.EndEvent();
			}
			else
			{
				// [af] Commented in JS code.
				//if (!EventStudent.Alarmed)
				//{

				if (!this.EventStudent.Pathfinding.canMove)
				{
					if (this.EventPhase == 1)
					{
						if (this.Timer == 0.0f)
						{
							this.EventStudent.Character.GetComponent<Animation>().CrossFade(this.EventStudent.IdleAnim);
							this.Prompt.HideButton[0] = false;
							this.EventStudent.Prompt.Hide();
							this.EventStudent.Prompt.enabled = false;

							this.StallDoor.Prompt.enabled = false;
							this.StallDoor.Prompt.Hide();
						}

						this.Timer += Time.deltaTime;

						if (this.Timer > 3.0f)
						{
							this.StallDoor.Locked = true;
							this.StallDoor.CloseDoor();

							this.Toilet.enabled = false;

							this.Prompt.Hide();
							this.Prompt.enabled = false;

							this.EventStudent.CurrentDestination = this.EventLocation[2];
							this.EventStudent.Pathfinding.target = this.EventLocation[2];
							this.EventStudent.TargetDistance = 2.0f;

							this.EventPhase++;
							this.Timer = 0.0f;
						}
					}
					else if (this.EventPhase == 2)
					{
						if (this.Timer == 0.0f)
						{
							this.EventStudent.Character.GetComponent<Animation>().CrossFade(this.EventAnim[1]);

							this.BucketPour.enabled = true;
						}

						this.Timer += Time.deltaTime;

						if (this.Timer > 10.0f)
						{
							AudioClipPlayer.Play(this.EventClip[2], this.Toilet.transform.position,
								5.0f, 10.0f, out this.VoiceClip);

							this.EventPhase++;
							this.Timer = 0.0f;
						}
					}
					else if (this.EventPhase == 3)
					{
						this.Timer += Time.deltaTime;

						if (this.Timer > 4.0f)
						{
							this.EventStudent.CurrentDestination = this.EventLocation[3];
							this.EventStudent.Pathfinding.target = this.EventLocation[3];
							this.EventStudent.TargetDistance = 2.0f;

							this.StallDoor.gameObject.SetActive(true);
							this.StallDoor.Prompt.enabled = true;
							this.StallDoor.Locked = false;

							this.EventPhase++;
							this.Timer = 0.0f;
						}
					}
					else if (this.EventPhase == 4)
					{
						this.EventStudent.Character.GetComponent<Animation>().CrossFade(AnimNames.FemaleWashHands);

						this.Timer += Time.deltaTime;

						if (this.Timer > 5.0f)
						{
							this.EndEvent();
						}
					}
					else if (this.EventPhase == 5)
					{
						this.Timer += Time.deltaTime;

						if (this.Timer > 9.0f)
						{
							this.Splashes.Stop();

							this.EventOver = true;
							this.EndEvent();
						}
						else if (this.Timer > 3.0f)
						{
							this.EventSubtitle.text = string.Empty;
							this.Splashes.Play();
						}
					}

					this.Distance = Vector3.Distance(this.Yandere.transform.position,
						this.EventStudent.transform.position);

					if (this.Distance < 10.0f)
					{
						float Scale = Mathf.Abs((this.Distance - 10.0f) * 0.20f);

						if (Scale < 0.0f)
						{
							Scale = 0.0f;
						}

						if (Scale > 1.0f)
						{
							Scale = 1.0f;
						}

						this.EventSubtitle.transform.localScale = new Vector3(Scale, Scale, Scale);
					}
					else
					{
						this.EventSubtitle.transform.localScale = Vector3.zero;
					}
				}

				//}
				//else
				//{
				//EndEvent();
				//}
			}
		}

		if (this.ToiletCountdown > 0.0f)
		{
			this.ToiletCountdown -= Time.deltaTime;

			if (this.ToiletCountdown < 0.0f)
			{
				this.Toilet.enabled = true;
			}
		}
	}

	public void EndEvent()
	{
		if (!this.EventOver)
		{
			if (this.VoiceClip != null)
			{
				Destroy(this.VoiceClip);
			}

			this.EventStudent.CurrentDestination = this.EventStudent.Destinations[this.EventStudent.Phase];
			this.EventStudent.Pathfinding.target = this.EventStudent.Destinations[this.EventStudent.Phase];

			this.EventStudent.Obstacle.checkTime = 1.0f;

			if (!this.EventStudent.Dying)
			{
				this.EventStudent.Prompt.enabled = true;
			}

			this.EventStudent.TargetDistance = 1.0f;
			this.EventStudent.ToiletEvent = null;
			this.EventStudent.InEvent = false;
			this.EventStudent.Private = false;

			this.EventSubtitle.text = string.Empty;

			this.StudentManager.UpdateStudents();
		}

		this.StallDoor.gameObject.SetActive(true);
		this.StallDoor.Prompt.enabled = true;
		this.StallDoor.Locked = false;

		this.BucketPour.enabled = false;
		this.BucketPour.Prompt.Hide();
		this.BucketPour.Prompt.enabled = false;

		this.EventActive = false;
		this.EventCheck = false;

		this.Prompt.Hide();
		this.Prompt.enabled = false;

		this.ToiletCountdown = 1.0f;
	}
}
