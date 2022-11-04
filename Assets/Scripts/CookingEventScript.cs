using System;
using UnityEngine;

public class CookingEventScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public RefrigeratorScript Snacks;
	public SchemesScript Schemes;
	public YandereScript Yandere;
	public ClockScript Clock;

	public GameObject Refrigerator;
	public GameObject RivalPhone;
	public GameObject Octodog;
	public GameObject Sausage;

	public Transform CookingClub;
	public Transform JarLid;
	public Transform Knife;
	public Transform Plate;
	public Transform Jar;

	public Transform[] Octodogs;

	public StudentScript EventStudent;
	public UILabel EventSubtitle;

	public Transform[] EventLocations;
	public AudioClip[] EventClip;
	public string[] EventSpeech;
	public string[] EventAnim;
	public int[] ClubMembers;

	public GameObject VoiceClip;

	public bool EventActive = false;
	public bool EventCheck = false;
	public bool EventOver = false;

	public int EventStudentID = 0;
	public float EventTime = 7.0f;
	public int EventPhase = 1;
	public DayOfWeek EventDay = DayOfWeek.Tuesday;
	public int Loop = 0;

	public float CurrentClipLength = 0.0f;
	public float Rotation = 0.0f;
	public float Timer = 0.0f;

	void Start()
	{
		this.Octodog.SetActive(false);
		this.Sausage.SetActive(false);

		this.Rotation = -90.0f;

		// [af] Converted while loop to foreach loop.
		foreach (Transform transform in this.Octodogs)
		{
			// [af] Added "gameObject" for C# compatibility.
			transform.gameObject.SetActive(false);
		}

		this.EventSubtitle.transform.localScale = Vector3.zero;

		this.EventCheck = true;

		if (ClubGlobals.GetClubClosed(ClubType.Cooking))
		{
			this.enabled = false;
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			// [af] Commented in JS code.
			//EventStudent = StudentManager.Students[EventStudentID];
			//EventStudent.transform.position = Vector3(4, 0, 20);

			//Clock.PresentTime = (EventTime * 60) - .1;
		}

		if (!this.Clock.StopTime)
		{
			if (this.EventCheck)
			{
				if (this.Clock.HourTime > this.EventTime)
				{
					this.EventStudent = this.StudentManager.Students[this.EventStudentID];

					if (this.EventStudent != null)
					{
						if (!this.EventStudent.Distracted && this.EventStudent.MeetTime == 0 && !this.EventStudent.Meeting && !this.EventStudent.Wet)
						{
							if (!this.EventStudent.WitnessedMurder)
							{
								this.Snacks.Prompt.Hide();
								this.Snacks.Prompt.enabled = false;
								this.Snacks.enabled = false;

								this.EventStudent.CurrentDestination = this.EventLocations[0];
								this.EventStudent.Pathfinding.target = this.EventLocations[0];

								this.EventStudent.Scrubber.SetActive(false);
								this.EventStudent.Eraser.SetActive(false);

								this.EventStudent.Obstacle.checkTime = 99.0f;
								this.EventStudent.CookingEvent = this;
								this.EventStudent.InEvent = true;
								this.EventStudent.Private = true;
								this.EventStudent.Prompt.Hide();

								this.EventCheck = false;
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
			if ((this.Clock.HourTime > (this.EventTime + 0.50f)) ||
				this.EventStudent.WitnessedMurder || this.EventStudent.Splashed ||
				this.EventStudent.Alarmed || this.EventStudent.Dying ||
				this.EventStudent.Yandere.Cooking)
			{
				this.EndEvent();
			}
			else
			{
				/*
				if (this.EventStudent.CurrentDestination != this.EventLocations[0])
				{
					this.EventStudent.CurrentDestination = this.EventLocations[0];
					this.EventStudent.Pathfinding.target = this.EventLocations[0];
				}
				*/

				if (this.EventStudent.DistanceToDestination < 1)
				{
					if (this.EventPhase == -1)
					{
						this.EventStudent.CharacterAnimation.CrossFade(this.EventAnim[0]);

						this.Timer += Time.deltaTime;

						if (this.Timer > 5.0f)
						{
							SchemeGlobals.SetSchemeStage(4, 5);
							this.Schemes.UpdateInstructions();

							this.RivalPhone.SetActive(false);
							this.EventSubtitle.text = string.Empty;
							this.EventPhase++;
							this.Timer = 0.0f;
						}
					}
					else if (this.EventPhase == 0)
					{
						if (!this.RivalPhone.activeInHierarchy)
						{
							this.EventStudent.CharacterAnimation.Play(AnimNames.FemalePrepareFood);
							this.EventStudent.SmartPhone.SetActive(false);

							this.Octodog.transform.parent = this.EventStudent.RightHand;
							this.Octodog.transform.localPosition = new Vector3(0.0129f, -0.02475f, 0.0316f);
							this.Octodog.transform.localEulerAngles = new Vector3(-90.0f, 0.0f, 0.0f);

							this.Sausage.transform.parent = this.EventStudent.RightHand;
							this.Sausage.transform.localPosition = new Vector3(0.013f, -0.038f, 0.015f);
							this.Sausage.transform.localEulerAngles = Vector3.zero;

							this.EventPhase++;
						}
						else
						{
							AudioClipPlayer.Play(this.EventClip[0],
								this.EventStudent.transform.position + Vector3.up,
								5.0f, 10.0f, out this.VoiceClip, out this.CurrentClipLength);

							this.EventStudent.CharacterAnimation.CrossFade(this.EventAnim[0]);
							this.EventSubtitle.text = this.EventSpeech[0];
							this.EventPhase--;
						}
					}
					else if (this.EventPhase == 1)
					{
						this.EventStudent.CharacterAnimation.Play(AnimNames.FemalePrepareFood);

						if (this.EventStudent.CharacterAnimation[AnimNames.FemalePrepareFood].time > 1.0f)
						{
							this.EventPhase++;
						}
					}
					else if (this.EventPhase == 2)
					{
						this.Refrigerator.GetComponent<Animation>().Play(AnimNames.FridgeOpen);

						if (this.EventStudent.CharacterAnimation[AnimNames.FemalePrepareFood].time > 3.0f)
						{
							this.Jar.transform.parent = this.EventStudent.RightHand;

							this.EventPhase++;
						}
					}
					else if (this.EventPhase == 3)
					{
						if (this.EventStudent.CharacterAnimation[AnimNames.FemalePrepareFood].time > 5.0f)
						{
							this.JarLid.transform.parent = this.EventStudent.LeftHand;

							this.EventPhase++;
						}
					}
					else if (this.EventPhase == 4)
					{
						if (this.EventStudent.CharacterAnimation[AnimNames.FemalePrepareFood].time > 6.0f)
						{
							this.JarLid.transform.parent = this.CookingClub;
							this.JarLid.transform.localPosition = new Vector3(0.334585f, 1.0f, -0.2528915f);
							this.JarLid.transform.localEulerAngles = new Vector3(0.0f, 30.0f, 0.0f);

							this.Jar.transform.parent = this.CookingClub;
							this.Jar.transform.localPosition = new Vector3(0.29559f, 1.0f, 0.2029152f);
							this.Jar.transform.localEulerAngles = new Vector3(0.0f, -150.0f, 0.0f);

							this.EventPhase++;
						}
					}
					else if (this.EventPhase == 5)
					{
						if (this.EventStudent.CharacterAnimation[AnimNames.FemalePrepareFood].time > 7.0f)
						{
							this.Knife.GetComponent<WeaponScript>().FingerprintID = this.EventStudent.StudentID;

							this.Knife.parent = this.EventStudent.LeftHand;
							this.Knife.localPosition = new Vector3(0.0f, -0.010f, 0.0f);
							this.Knife.localEulerAngles = new Vector3(0.0f, 0.0f, -90.0f);

							this.EventPhase++;
						}
					}
					else if (this.EventPhase == 6)
					{
						if (this.EventStudent.CharacterAnimation[AnimNames.FemalePrepareFood].time >=
							this.EventStudent.CharacterAnimation[AnimNames.FemalePrepareFood].length)
						{
							this.EventStudent.CharacterAnimation.CrossFade(AnimNames.FemaleCutFood);
							this.Sausage.SetActive(true);

							this.EventPhase++;
						}
					}
					else if (this.EventPhase == 7)
					{
						if (this.EventStudent.CharacterAnimation[AnimNames.FemaleCutFood].time > 2.66666f)
						{
							this.Octodog.SetActive(true);
							this.Sausage.SetActive(false);

							this.EventPhase++;
						}
					}
					else if (this.EventPhase == 8)
					{
						if (this.EventStudent.CharacterAnimation[AnimNames.FemaleCutFood].time > 3.0f)
						{
							this.Rotation = Mathf.MoveTowards(this.Rotation, 90.0f, Time.deltaTime * 360.0f);

							this.Octodog.transform.localEulerAngles = new Vector3(
								this.Rotation,
								this.Octodog.transform.localEulerAngles.y,
								this.Octodog.transform.localEulerAngles.z);

							this.Octodog.transform.localPosition = new Vector3(
								this.Octodog.transform.localPosition.x,
								this.Octodog.transform.localPosition.y,
								Mathf.MoveTowards(this.Octodog.transform.localPosition.z, 0.012f, Time.deltaTime));
						}

						if (this.EventStudent.CharacterAnimation[AnimNames.FemaleCutFood].time > 6.0f)
						{
							this.Octodog.SetActive(false);

							// [af] Added "gameObject" for C# compatibility.
							this.Octodogs[this.Loop].gameObject.SetActive(true);

							this.EventPhase++;
						}
					}
					else if (this.EventPhase == 9)
					{
						if (this.EventStudent.CharacterAnimation[AnimNames.FemaleCutFood].time >=
							this.EventStudent.CharacterAnimation[AnimNames.FemaleCutFood].length)
						{
							if (this.Loop < (this.Octodogs.Length - 1))
							{
								this.Octodog.transform.localEulerAngles = new Vector3(
									-90.0f,
									this.Octodog.transform.localEulerAngles.y,
									this.Octodog.transform.localEulerAngles.z);

								this.Octodog.transform.localPosition = new Vector3(
									this.Octodog.transform.localPosition.x,
									this.Octodog.transform.localPosition.y,
									0.0316f);

								this.EventStudent.CharacterAnimation[AnimNames.FemaleCutFood].time = 0.0f;
								this.EventStudent.CharacterAnimation.Play(AnimNames.FemaleCutFood);
								this.Sausage.SetActive(true);
								this.EventPhase = 7;
								this.Rotation = -90.0f;
								this.Loop++;
							}
							else
							{
								this.EventStudent.CharacterAnimation.Play(AnimNames.FemalePrepareFood);
								this.EventStudent.CharacterAnimation[AnimNames.FemalePrepareFood].time =
									this.EventStudent.CharacterAnimation[AnimNames.FemalePrepareFood].length;
								this.EventStudent.CharacterAnimation[AnimNames.FemalePrepareFood].speed = -1.0f;

								this.EventPhase++;
							}
						}
					}
					else if (this.EventPhase == 10)
					{
						if (this.EventStudent.CharacterAnimation[AnimNames.FemalePrepareFood].time <
							(this.EventStudent.CharacterAnimation[AnimNames.FemalePrepareFood].length - 1.0f))
						{
							this.Knife.parent = this.CookingClub;
							this.Knife.localPosition = new Vector3(0.197f, 1.1903f, -0.33333f);
							this.Knife.localEulerAngles = new Vector3(45.0f, -90.0f, -90.0f);

							this.EventPhase++;
						}
					}
					else if (this.EventPhase == 11)
					{
						if (this.EventStudent.CharacterAnimation[AnimNames.FemalePrepareFood].time <
							(this.EventStudent.CharacterAnimation[AnimNames.FemalePrepareFood].length - 2.0f))
						{
							this.JarLid.parent = this.EventStudent.LeftHand;
							this.Jar.parent = this.EventStudent.RightHand;

							this.EventPhase++;
						}
					}
					else if (this.EventPhase == 12)
					{
						if (this.EventStudent.CharacterAnimation[AnimNames.FemalePrepareFood].time <
							(this.EventStudent.CharacterAnimation[AnimNames.FemalePrepareFood].length - 3.0f))
						{
							this.JarLid.parent = this.Jar;
							this.JarLid.localPosition = new Vector3(0.0f, 0.175f, 0.0f);
							this.JarLid.localEulerAngles = Vector3.zero;

							this.Refrigerator.GetComponent<Animation>().Play(AnimNames.FridgeOpen);
							this.Refrigerator.GetComponent<Animation>()[AnimNames.FridgeOpen].time =
								this.Refrigerator.GetComponent<Animation>()[AnimNames.FridgeOpen].length;
							this.Refrigerator.GetComponent<Animation>()[AnimNames.FridgeOpen].speed = -1.0f;

							this.EventPhase++;
						}
					}
					else if (this.EventPhase == 13)
					{
						if (this.EventStudent.CharacterAnimation[AnimNames.FemalePrepareFood].time <
							(this.EventStudent.CharacterAnimation[AnimNames.FemalePrepareFood].length - 5.0f))
						{
							this.Jar.parent = this.CookingClub;
							this.Jar.localPosition = new Vector3(0.10f, 0.941f, 0.75f);
							this.Jar.localEulerAngles = new Vector3(0.0f, 90.0f, 0.0f);

							this.EventPhase++;
						}
					}
					else if (this.EventPhase == 14)
					{
						if (this.EventStudent.CharacterAnimation[AnimNames.FemalePrepareFood].time <= 0.0f)
						{
							this.Knife.GetComponent<Collider>().enabled = true;

							this.Plate.parent = this.EventStudent.RightHand;
							this.Plate.localPosition = new Vector3(0.0f, 0.011f, -0.156765f);
							this.Plate.localEulerAngles = new Vector3(0.0f, -90.0f, -180.0f);

							this.EventStudent.CurrentDestination = this.EventLocations[1];
							this.EventStudent.Pathfinding.target = this.EventLocations[1];

							this.EventStudent.CharacterAnimation[this.EventStudent.CarryAnim].weight = 1.0f;

							this.EventPhase++;
						}
					}
					else if (this.EventPhase == 15)
					{
						this.Plate.parent = this.CookingClub;
						this.Plate.localPosition = new Vector3(-3.66666f, 0.9066666f, -2.379f);
						this.Plate.localEulerAngles = new Vector3(0.0f, -90.0f, 0.0f);

						this.EndEvent();
					}

					float Distance = Vector3.Distance(this.Yandere.transform.position,
						this.EventStudent.transform.position);

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
					else if (Distance < 11.0f)
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

			this.EventStudent.CurrentDestination = this.EventStudent.Destinations[this.EventStudent.Phase];
			this.EventStudent.Pathfinding.target = this.EventStudent.Destinations[this.EventStudent.Phase];

			this.EventStudent.Obstacle.checkTime = 1.0f;

			if (!this.EventStudent.Dying)
			{
				this.EventStudent.Prompt.enabled = true;
			}

			if (this.Plate.parent == this.EventStudent.RightHand)
			{
				this.Plate.parent = null;
				this.Plate.GetComponent<Rigidbody>().useGravity = true;
				this.Plate.GetComponent<BoxCollider>().enabled = true;
			}

			this.EventStudent.CharacterAnimation[this.EventStudent.CarryAnim].weight = 0.0f;
			this.EventStudent.SmartPhone.SetActive(false);
			this.EventStudent.Pathfinding.speed = 1.0f;
			this.EventStudent.TargetDistance = 1.0f;
			this.EventStudent.PhoneEvent = null;
			this.EventStudent.InEvent = false;
			this.EventStudent.Private = false;

			this.EventSubtitle.text = string.Empty;

			if (this.Knife.parent == this.EventStudent.LeftHand)
			{
				this.Knife.parent = this.CookingClub;
				this.Knife.localPosition = new Vector3(0.197f, 1.1903f, -0.33333f);
				this.Knife.localEulerAngles = new Vector3(45.0f, -90.0f, -90.0f);
				this.Knife.GetComponent<Collider>().enabled = true;
			}

			this.StudentManager.UpdateStudents();
		}

		this.EventActive = false;
		this.EventCheck = false;
	}
}
