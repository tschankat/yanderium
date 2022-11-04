using System;
using UnityEngine;

public class BatheEventScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public YandereScript Yandere;
	public ClockScript Clock;

	public StudentScript EventStudent;
	public UILabel EventSubtitle;

	public AudioClip[] EventClip;
	public string[] EventSpeech;
	public string[] EventAnim;

	public GameObject RivalPhone;
	public GameObject VoiceClip;

	public bool EventActive = false;
	public bool EventOver = false;

	public float EventTime = 15.10f;
	public int EventPhase = 1;
	public DayOfWeek EventDay = DayOfWeek.Thursday;

	public Vector3 OriginalPosition;

	public float CurrentClipLength = 0.0f;
	public float Timer = 0.0f;

	void Start()
	{
		this.RivalPhone.SetActive(false);

		if (DateGlobals.Weekday != this.EventDay)
		{
			this.enabled = false;
		}
	}

	void Update()
	{
		if (!this.Clock.StopTime)
		{
			if (!this.EventActive)
			{
				if (this.Clock.HourTime > this.EventTime)
				{
					this.EventStudent = this.StudentManager.Students[30];

					if (this.EventStudent != null)
					{
						if (!this.EventStudent.Distracted && !this.EventStudent.Talking &&
							!this.EventStudent.Meeting && this.EventStudent.Indoors)
						{
							if (!this.EventStudent.WitnessedMurder)
							{
								this.OriginalPosition = this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localPosition;

								this.EventStudent.CurrentDestination = this.StudentManager.FemaleStripSpot;
								this.EventStudent.Pathfinding.target = this.StudentManager.FemaleStripSpot;

								this.EventStudent.Character.GetComponent<Animation>().CrossFade(this.EventStudent.WalkAnim);
								this.EventStudent.Pathfinding.canSearch = true;
								this.EventStudent.Pathfinding.canMove = true;
								this.EventStudent.Pathfinding.speed = 1.0f;
								this.EventStudent.SpeechLines.Stop();

								this.EventStudent.DistanceToDestination = 100.0f;
								this.EventStudent.SmartPhone.SetActive(false);
								this.EventStudent.Obstacle.checkTime = 99.0f;
								this.EventStudent.InEvent = true;
								this.EventStudent.Private = true;
								this.EventStudent.Prompt.Hide();
								this.EventStudent.Hearts.Stop();

								this.EventActive = true;

								if (this.EventStudent.Following)
								{
									this.EventStudent.Pathfinding.canMove = true;
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
			if ((this.Clock.HourTime > (this.EventTime + 1.0f)) ||
				this.EventStudent.WitnessedMurder ||
				this.EventStudent.Splashed ||
				this.EventStudent.Alarmed ||
				this.EventStudent.Dying ||
				!this.EventStudent.Alive)
			{
				this.EndEvent();
			}
			else
			{
				if (this.EventStudent.DistanceToDestination < 0.50f)
				{
					if (this.EventPhase == 1)
					{
						this.EventStudent.Routine = false;
						this.EventStudent.BathePhase = 1;
						this.EventStudent.Wet = true;

						this.EventPhase++;
					}
					else if (this.EventPhase == 2)
					{
						if (this.EventStudent.BathePhase == 4)
						{
							this.RivalPhone.SetActive(true);
							this.EventPhase++;
						}
					}
					else if (this.EventPhase == 3)
					{
						if (!this.EventStudent.Wet)
						{
							/*
							if (!this.RivalPhone.activeInHierarchy)
							{
								this.EventStudent.Character.GetComponent<Animation>().CrossFade(this.EventAnim[0]);
								this.EventStudent.Pathfinding.canSearch = false;
								this.EventStudent.Pathfinding.canMove = false;
								this.EventStudent.Routine = false;

								this.StudentManager.CommunalLocker.Open = true;

								this.EventSubtitle.text = this.EventSpeech[0];
								AudioClipPlayer.Play(this.EventClip[0],
									this.EventStudent.transform.position + Vector3.up,
									5.0f, 10.0f, out this.VoiceClip, out this.CurrentClipLength);

								this.EventPhase++;
							}
							else
							{
							*/
								this.EndEvent();							
							//}
						}
					}
				}

				if (this.EventPhase == 4)
				{
					this.Timer += Time.deltaTime;

					if (this.Timer > (this.CurrentClipLength + 1.0f))
					{
						this.EventStudent.Routine = true;
						this.EndEvent();
					}
				}

				float Distance = Vector3.Distance(
					this.Yandere.transform.position, this.EventStudent.transform.position);

				if (Distance < 11.0f)
				{
					if (Distance < 10.0f)
					{
						float Scale = Mathf.Abs((Distance - 10.0f) * 0.20f);

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
			}
		}
	}

	void EndEvent()
	{
		if (!this.EventOver)
		{
			if (this.VoiceClip != null)
			{
				Destroy(this.VoiceClip);
			}

			this.EventStudent.CurrentDestination =
				this.EventStudent.Destinations[this.EventStudent.Phase];
			this.EventStudent.Pathfinding.target =
				this.EventStudent.Destinations[this.EventStudent.Phase];

			this.EventStudent.Obstacle.checkTime = 1.0f;

			if (!this.EventStudent.Dying)
			{
				this.EventStudent.Prompt.enabled = true;

				this.EventStudent.Pathfinding.canSearch = true;
				this.EventStudent.Pathfinding.canMove = true;
				this.EventStudent.Pathfinding.speed = 1.0f;
				this.EventStudent.TargetDistance = 1.0f;
				this.EventStudent.Private = false;
			}

			this.EventStudent.InEvent = false;

			this.EventSubtitle.text = string.Empty;

			this.StudentManager.UpdateStudents();
		}

		this.EventActive = false;
		this.enabled = false;
	}
}
