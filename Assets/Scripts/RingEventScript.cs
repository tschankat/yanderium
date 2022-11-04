using UnityEngine;

public class RingEventScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public YandereScript Yandere;
	public ClockScript Clock;

	public StudentScript EventStudent;
	public UILabel EventSubtitle;

	public AudioClip[] EventClip;
	public string[] EventSpeech;
	public string[] EventAnim;

	public GameObject VoiceClip;

	public bool EventActive = false;
	public bool EventOver = false;

	public float EventTime = 13.10f;
	public int EventPhase = 1;

	public Vector3 OriginalPosition;
	public Vector3 HoldingPosition;
	public Vector3 HoldingRotation;

	public float CurrentClipLength = 0.0f;
	public float Timer = 0.0f;

	public PromptScript RingPrompt;
	public Collider RingCollider;

	void Start()
	{
		this.HoldingPosition = new Vector3(0.0075f, -0.0355f, 0.0175f);
		this.HoldingRotation = new Vector3(15.0f, -70.0f, -135.0f);
	}

	void Update()
	{
		if (!this.Clock.StopTime)
		{
			if (!this.EventActive)
			{
				if (this.Clock.HourTime > this.EventTime)
				{
					this.EventStudent = this.StudentManager.Students[2];

					if (this.EventStudent != null)
					{
						if (!this.EventStudent.Distracted && !this.EventStudent.Talking)
						{
							if (!this.EventStudent.WitnessedMurder && !this.EventStudent.Bullied)
							{
								if (this.EventStudent.Cosmetic.FemaleAccessories[3].activeInHierarchy)
								{
									if (SchemeGlobals.GetSchemeStage(2) < 100)
									{
										this.RingPrompt = this.EventStudent.Cosmetic.FemaleAccessories[3].GetComponent<PromptScript>();
										this.RingCollider = this.EventStudent.Cosmetic.FemaleAccessories[3].GetComponent<BoxCollider>();

										this.OriginalPosition = this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localPosition;

										this.EventStudent.CurrentDestination = this.EventStudent.Destinations[this.EventStudent.Phase];
										this.EventStudent.Pathfinding.target = this.EventStudent.Destinations[this.EventStudent.Phase];

										this.EventStudent.Obstacle.checkTime = 99.0f;
										this.EventStudent.InEvent = true;
										this.EventStudent.Private = true;
										this.EventStudent.Prompt.Hide();

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
					}
				}
			}
		}

		if (this.EventActive)
		{
			if (this.EventStudent.DistanceToDestination < 0.50f)
			{
				this.EventStudent.Pathfinding.canSearch = false;
				this.EventStudent.Pathfinding.canMove = false;
			}

			if (this.EventStudent.Alarmed && this.Yandere.TheftTimer > 0)
			{
				Debug.Log("Event ended because Sakyu saw theft.");

				this.EventStudent.Cosmetic.FemaleAccessories[3].transform.parent = this.EventStudent.LeftMiddleFinger;
				this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localPosition = this.OriginalPosition;
				this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localEulerAngles = new Vector3(0, 0, 0);
				this.RingCollider.gameObject.SetActive(true);
				this.RingCollider.enabled = false;

                this.EventStudent.RingReact = true;

                this.Yandere.Inventory.Ring = false;

				this.EndEvent();
			}
			else
			{
				if ((this.Clock.HourTime > (this.EventTime + 0.50f)) ||
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
					if (!this.EventStudent.Pathfinding.canMove)
					{
						if (this.EventPhase == 1)
						{
							this.Timer += Time.deltaTime;

							this.EventStudent.Character.GetComponent<Animation>().CrossFade(this.EventAnim[0]);

							this.EventPhase++;
						}
						else if (this.EventPhase == 2)
						{
							this.Timer += Time.deltaTime;

							//Entering eating animation
							if (this.Timer > this.EventStudent.Character.GetComponent<Animation>()[this.EventAnim[0]].length)
							{
								this.EventStudent.Character.GetComponent<Animation>().CrossFade(this.EventStudent.EatAnim);

								this.EventStudent.Bento.transform.localPosition = new Vector3(-0.025f, -0.105f, 0.0f);
								this.EventStudent.Bento.transform.localEulerAngles = new Vector3(0.0f, 165.0f, 82.50f);

								this.EventStudent.Chopsticks[0].SetActive(true);
								this.EventStudent.Chopsticks[1].SetActive(true);
								this.EventStudent.Bento.SetActive(true);
								this.EventStudent.Lid.SetActive(false);
								this.RingCollider.enabled = true;

								this.EventPhase++;

								this.Timer = 0.0f;
							}
							//Putting the ring down on the bench
							else if (this.Timer > 4.0f)
							{
								if (this.EventStudent.Cosmetic.FemaleAccessories[3] != null)
								{
									this.EventStudent.Cosmetic.FemaleAccessories[3].transform.parent = null;
									this.EventStudent.Cosmetic.FemaleAccessories[3].transform.position =
										new Vector3(-2.707666f, 12.4695f, -31.136f);
									this.EventStudent.Cosmetic.FemaleAccessories[3].transform.eulerAngles =
										new Vector3(-20.0f, 180.0f, 0.0f);
								}
							}
							//Pulling the ring off of her finger
							else if (this.Timer > 2.50f)
							{
								this.EventStudent.Cosmetic.FemaleAccessories[3].transform.parent = this.EventStudent.RightHand;
								this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localPosition = this.HoldingPosition;
								this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localEulerAngles = this.HoldingRotation;
							}
						}
						else if (this.EventPhase == 3)
						{
							if (this.Clock.HourTime > 13.375f)
							{
								this.EventStudent.Bento.SetActive(false);
								this.EventStudent.Chopsticks[0].SetActive(false);
								this.EventStudent.Chopsticks[1].SetActive(false);

								if (this.RingCollider != null)
								{
									this.RingCollider.enabled = false;
								}

								if (this.RingPrompt != null)
								{
									this.RingPrompt.Hide();
									this.RingPrompt.enabled = false;
								}

								this.EventStudent.Character.GetComponent<Animation>()[this.EventAnim[0]].time =
									this.EventStudent.Character.GetComponent<Animation>()[this.EventAnim[0]].length;
								this.EventStudent.Character.GetComponent<Animation>()[this.EventAnim[0]].speed = -1.0f;

								// [af] Replaced if/else statement with ternary expression.
								this.EventStudent.Character.GetComponent<Animation>().CrossFade(
									(this.EventStudent.Cosmetic.FemaleAccessories[3] != null) ?
									this.EventAnim[0] : this.EventAnim[1]);

								this.EventPhase++;
							}
						}
						else if (this.EventPhase == 4)
						{
							this.Timer += Time.deltaTime;

							if (this.EventStudent.Cosmetic.FemaleAccessories[3] != null)
							{
								if (this.Timer > 2.0f)
								{
									this.EventStudent.Cosmetic.FemaleAccessories[3].transform.parent = this.EventStudent.RightHand;
									this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localPosition = this.HoldingPosition;
									this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localEulerAngles = this.HoldingRotation;
								}

								if (this.Timer > 3.0f)
								{
									this.EventStudent.Cosmetic.FemaleAccessories[3].transform.parent = this.EventStudent.LeftMiddleFinger;
									this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localPosition = this.OriginalPosition;
									this.RingCollider.enabled = false;
								}

								if (this.Timer > 6.0f)
								{
									this.EndEvent();
								}
							}
							else
							{
								if (this.Timer > 1.50f)
								{
									if (this.Yandere.transform.position.z < 0.0f)
									{
										this.EventSubtitle.text = this.EventSpeech[0];
										AudioClipPlayer.Play(this.EventClip[0],
											this.EventStudent.transform.position + Vector3.up,
											5.0f, 10.0f, out this.VoiceClip, out this.CurrentClipLength);

										this.EventPhase++;
									}
								}
							}
						}
						else if (this.EventPhase == 5)
						{
							this.Timer += Time.deltaTime;

							if (this.Timer > 9.50f)
							{
								this.EndEvent();
							}
						}

						float Distance = Vector3.Distance(this.Yandere.transform.position,
							this.EventStudent.transform.position);

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
		}
	}

	void EndEvent()
	{
        Debug.Log("Rooftop ring event has ended.");

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

			this.EventStudent.Pathfinding.speed = 1.0f;
			this.EventStudent.TargetDistance = .5f;
			this.EventStudent.InEvent = false;
			this.EventStudent.Private = false;

			this.EventSubtitle.text = string.Empty;

			this.StudentManager.UpdateStudents();
		}

		this.EventActive = false;
		this.enabled = false;
	}

	public void ReturnRing()
	{
		if (this.EventStudent.Cosmetic.FemaleAccessories[3] != null)
		{
			this.EventStudent.Cosmetic.FemaleAccessories[3].transform.parent = this.EventStudent.LeftMiddleFinger;
			this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localPosition = this.OriginalPosition;
			this.RingCollider.enabled = false;

			this.RingPrompt.Hide();
			this.RingPrompt.enabled = false;
		}
	}
}
