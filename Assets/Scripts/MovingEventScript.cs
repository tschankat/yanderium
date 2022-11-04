using System;
using UnityEngine;

public class MovingEventScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public UILabel EventSubtitle;
	public YandereScript Yandere;
	public PortalScript Portal;
	public PromptScript Prompt;
	public ClockScript Clock;

	public StudentScript EventStudent;
	public Transform[] EventLocation;
	public AudioClip[] EventClip;
	public string[] EventSpeech;
	public string[] EventAnim;

	public Collider BenchCollider;

	public GameObject VoiceClip;

	public bool EventActive = false;
	public bool EventCheck = false;
	public bool EventOver = false;
	public bool Poisoned = false;

	public int EventPhase = 1;
	public DayOfWeek EventDay = DayOfWeek.Wednesday;

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
				if (this.Clock.HourTime > 13.0f)
				{
					this.EventStudent = this.StudentManager.Students[30];

					if (this.EventStudent != null)
					{
						this.EventStudent.Character.GetComponent<Animation>()[this.EventStudent.BentoAnim].weight = 1.0f;
						this.EventStudent.CurrentDestination = this.EventLocation[0];
						this.EventStudent.Pathfinding.target = this.EventLocation[0];

						this.EventStudent.SmartPhone.SetActive(false);
						this.EventStudent.Scrubber.SetActive(false);
						this.EventStudent.Bento.SetActive(true);
						this.EventStudent.Pen.SetActive(false);
						this.EventStudent.MovingEvent = this;
						this.EventStudent.InEvent = true;
						this.EventStudent.Private = true;

						this.EventCheck = false;
						this.EventActive = true;
					}
				}
			}
		}

		if (this.EventActive)
		{
			if (this.Prompt.Circle[0].fillAmount == 0.0f)
			{
				this.Poisoned = true;

				this.Prompt.Hide();
				this.Prompt.enabled = false;
			}

			if ((this.Clock.HourTime > 13.375f) && !this.Poisoned ||
				this.EventStudent.Dying || this.EventStudent.Splashed)
			{
				this.EndEvent();
			}
			else
			{
				this.Distance = Vector3.Distance(this.Yandere.transform.position,
					this.EventStudent.transform.position);

				if (!this.EventStudent.Alarmed &&
					(this.EventStudent.DistanceToDestination < this.EventStudent.TargetDistance) &&
					!this.EventStudent.Pathfinding.canMove)
				{
					if (this.EventPhase == 0)
					{
						this.EventStudent.Character.GetComponent<Animation>().CrossFade(this.EventStudent.IdleAnim);

						if (this.Clock.HourTime > 13.05f)
						{
							this.EventStudent.CurrentDestination = this.EventLocation[1];
							this.EventStudent.Pathfinding.target = this.EventLocation[1];

							this.EventPhase++;
						}
					}
					else if (this.EventPhase == 1)
					{
						if (!this.StudentManager.Students[1].WitnessedCorpse)
						{
							if (this.Timer == 0.0f)
							{
								AudioClipPlayer.Play(this.EventClip[1],
									this.EventStudent.transform.position + (Vector3.up * 1.50f),
									5.0f, 10.0f, out this.VoiceClip);

								this.EventStudent.Character.GetComponent<Animation>().CrossFade(this.EventStudent.IdleAnim);

								if (this.Distance < 10.0f)
								{
									this.EventSubtitle.text = this.EventSpeech[1];
								}

								this.EventStudent.Prompt.Hide();
								this.EventStudent.Prompt.enabled = false;
							}

							this.Timer += Time.deltaTime;

							if (this.Timer > 2.0f)
							{
								this.EventStudent.CurrentDestination = this.EventLocation[2];
								this.EventStudent.Pathfinding.target = this.EventLocation[2];

								if (this.Distance < 10.0f)
								{
									this.EventSubtitle.text = string.Empty;
								}

								this.EventPhase++;
								this.Timer = 0.0f;
							}
						}
						else
						{
							this.EventPhase = 7;
							this.Timer = 3.0f;
						}
					}
					else if (this.EventPhase == 2)
					{
						Animation eventCharAnim = this.EventStudent.Character.GetComponent<Animation>();
						eventCharAnim[this.EventStudent.BentoAnim].weight -= Time.deltaTime;

						if (this.Timer == 0.0f)
						{
							eventCharAnim.CrossFade(AnimNames.FemaleBentoPlace);
						}

						this.Timer += Time.deltaTime;

						if (this.Timer > 1.0f)
						{
							if (this.EventStudent.Bento.transform.parent != null)
							{
								this.EventStudent.Bento.transform.parent = null;
								this.EventStudent.Bento.transform.position = new Vector3(8.0f, 0.50f, -2.2965f);
								this.EventStudent.Bento.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
								this.EventStudent.Bento.transform.localScale = new Vector3(1.40f, 1.50f, 1.40f);
							}
						}

						if (this.Timer > 2.0f)
						{
							if (this.Yandere.Inventory.ChemicalPoison || this.Yandere.Inventory.LethalPoison)
							{
								this.Prompt.HideButton[0] = false;
							}

							this.EventStudent.CurrentDestination = this.EventLocation[3];
							this.EventStudent.Pathfinding.target = this.EventLocation[3];
							this.EventPhase++;
							this.Timer = 0.0f;
						}
					}
					else if (this.EventPhase == 3)
					{
						AudioClipPlayer.Play(this.EventClip[2],
							this.EventStudent.transform.position + (Vector3.up * 1.50f),
							5.0f, 10.0f, out this.VoiceClip);

						this.EventStudent.Character.GetComponent<Animation>().CrossFade(AnimNames.FemaleCornerPeek);

						if (this.Distance < 10.0f)
						{
							this.EventSubtitle.text = this.EventSpeech[2];
						}

						this.EventPhase++;
					}
					else if (this.EventPhase == 4)
					{
						this.Timer += Time.deltaTime;

						if (this.Timer > 5.50f)
						{
							AudioClipPlayer.Play(this.EventClip[3],
								this.EventStudent.transform.position + (Vector3.up * 1.50f),
								5.0f, 10.0f, out this.VoiceClip);

							if (this.Distance < 10.0f)
							{
								this.EventSubtitle.text = this.EventSpeech[3];
							}

							this.EventPhase++;
							this.Timer = 0.0f;
						}
					}
					else if (this.EventPhase == 5)
					{
						this.Timer += Time.deltaTime;

						if (this.Timer > 5.50f)
						{
							AudioClipPlayer.Play(this.EventClip[4],
								this.EventStudent.transform.position + (Vector3.up * 1.50f),
								5.0f, 10.0f, out this.VoiceClip);

							if (this.Distance < 10.0f)
							{
								this.EventSubtitle.text = this.EventSpeech[4];
							}

							this.EventPhase++;
							this.Timer = 0.0f;
						}
					}
					else if (this.EventPhase == 6)
					{
						this.Timer += Time.deltaTime;

						if (this.Timer > 3.0f)
						{
							this.EventStudent.CurrentDestination = this.EventLocation[2];
							this.EventStudent.Pathfinding.target = this.EventLocation[2];

							if (this.Distance < 10.0f)
							{
								this.EventSubtitle.text = string.Empty;
							}

							this.EventPhase++;

							this.Prompt.Hide();
							this.Prompt.enabled = false;
							this.Timer = 0.0f;
						}
					}
					else if (this.EventPhase == 7)
					{
						if (this.Timer == 0.0f)
						{
							Animation eventCharAnim = this.EventStudent.Character.GetComponent<Animation>();
							eventCharAnim[AnimNames.FemaleBentoPlace].time = eventCharAnim[AnimNames.FemaleBentoPlace].length;
							eventCharAnim[AnimNames.FemaleBentoPlace].speed = -1.0f;
							eventCharAnim.CrossFade(AnimNames.FemaleBentoPlace);
						}

						this.Timer += Time.deltaTime;

						if (this.Timer > 1.0f)
						{
							if (this.EventStudent.Bento.transform.parent == null)
							{
								this.EventStudent.Bento.transform.parent = this.EventStudent.LeftHand;
								this.EventStudent.Bento.transform.localPosition = new Vector3(0.0f, -0.0906f, -0.032f);
								this.EventStudent.Bento.transform.localEulerAngles = new Vector3(0.0f, 90.0f, 90.0f);
								this.EventStudent.Bento.transform.localScale = new Vector3(1.40f, 1.50f, 1.40f);
							}
						}

						if (this.Timer > 2.0f)
						{
							this.EventStudent.Bento.transform.localPosition = new Vector3(-0.025f, -0.105f, 0.0f);
							this.EventStudent.Bento.transform.localEulerAngles = new Vector3(0.0f, 165.0f, 82.50f);
							this.EventStudent.CurrentDestination = this.EventLocation[4];
							this.EventStudent.Pathfinding.target = this.EventLocation[4];
							this.EventStudent.Prompt.Hide();
							this.EventStudent.Prompt.enabled = false;

							this.EventPhase++;
							this.Timer = 0.0f;
						}
					}
					else if (this.EventPhase == 8)
					{
						Animation eventCharAnim = this.EventStudent.Character.GetComponent<Animation>();

						if (!this.Poisoned)
						{
							eventCharAnim[this.EventStudent.BentoAnim].weight = 0.0f;
							eventCharAnim.CrossFade(this.EventStudent.EatAnim);

							if (!this.EventStudent.Chopsticks[0].activeInHierarchy)
							{
								this.EventStudent.Chopsticks[0].SetActive(true);
								this.EventStudent.Chopsticks[1].SetActive(true);
							}
						}
						else
						{
							eventCharAnim.CrossFade(AnimNames.FemalePoisonDeath);

							this.Timer += Time.deltaTime;

							if (this.Timer < 13.55f)
							{
								if (!this.EventStudent.Chopsticks[0].activeInHierarchy)
								{
									AudioClipPlayer.Play(this.EventClip[5],
										this.EventStudent.transform.position + Vector3.up,
										5.0f, 10.0f, out this.VoiceClip);

									this.EventStudent.Chopsticks[0].SetActive(true);
									this.EventStudent.Chopsticks[1].SetActive(true);

									this.EventStudent.Distracted = true;
								}
							}
							else if (this.Timer < 16.33333f)
							{
								if (this.EventStudent.Chopsticks[0].transform.parent !=
									this.EventStudent.Bento.transform)
								{
									this.EventStudent.Chopsticks[0].transform.parent =
										this.EventStudent.Bento.transform;
									this.EventStudent.Chopsticks[1].transform.parent =
										this.EventStudent.Bento.transform;
								}

								this.EventStudent.EyeShrink += Time.deltaTime;

								if (this.EventStudent.EyeShrink > 0.90f)
								{
									this.EventStudent.EyeShrink = 0.90f;
								}
							}
							else
							{
								if (this.EventStudent.Bento.transform.parent != null)
								{
									// [af] Commented in JS code.
									/*BoxCollider benchBoxCollider = (BoxCollider)this.BenchCollider;
									benchBoxCollider.center = new Vector3(
										benchBoxCollider.center.x,
										0.02402199f,
										benchBoxCollider.center.z);
									benchBoxCollider.size = new Vector3(
										benchBoxCollider.size.x,
										0.04804402f,
										benchBoxCollider.size.z);*/

									this.EventStudent.Bento.transform.parent = null;
									this.EventStudent.Bento.GetComponent<Collider>().isTrigger = false;
									this.EventStudent.Bento.AddComponent<Rigidbody>();

									Rigidbody bentoRigidBody = this.EventStudent.Bento.GetComponent<Rigidbody>();
									bentoRigidBody.AddRelativeForce(Vector3.up * 100.0f);
									bentoRigidBody.AddRelativeForce(Vector3.left * 100.0f);
									bentoRigidBody.AddRelativeForce(Vector3.forward * -100.0f);
								}
							}

							if (eventCharAnim[AnimNames.FemalePoisonDeath].time >
								eventCharAnim[AnimNames.FemalePoisonDeath].length)
							{
								this.EventStudent.Ragdoll.Poisoned = true;
								this.EventStudent.BecomeRagdoll();

								this.Yandere.Police.PoisonScene = true;

								this.EventOver = true;
								this.EndEvent();
							}
						}
					}

					if (this.Distance < 11.0f)
					{
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

			Animation eventCharAnim = this.EventStudent.Character.GetComponent<Animation>();
			eventCharAnim[this.EventStudent.BentoAnim].weight = 0.0f;

			this.EventStudent.Chopsticks[0].SetActive(false);
			this.EventStudent.Chopsticks[1].SetActive(false);
			this.EventStudent.Bento.SetActive(false);

			this.EventStudent.Prompt.enabled = true;
			this.EventStudent.MovingEvent = null;
			this.EventStudent.InEvent = false;
			this.EventStudent.Private = false;

			this.EventSubtitle.text = string.Empty;

			this.StudentManager.UpdateStudents();
		}

		this.EventActive = false;
		this.EventCheck = false;

		this.Prompt.Hide();
		this.Prompt.enabled = false;
	}
}
