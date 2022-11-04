using UnityEngine;
using Pathfinding;

public class GardeningClubMemberScript : MonoBehaviour
{
	public PickpocketMinigameScript PickpocketMinigame;
	public DetectionMarkerScript DetectionMarker;
	public CameraEffectsScript CameraEffects;
	public CharacterController MyController;
	public CabinetDoorScript CabinetDoor;
	public ReputationScript Reputation;
	public SubtitleScript Subtitle;
	public YandereScript Yandere;
	public PromptScript Prompt;
	public DoorScript ShedDoor;
	public AIPath Pathfinding;

	public UIPanel PickpocketPanel;
	public UISprite TimeBar;

	public Transform PickpocketSpot;
	public Transform Destination;

	public GameObject Padlock;
	public GameObject Marker;
	public GameObject Key;

	public bool Moving = false;
	public bool Leader = false;
	public bool Angry = false;

	public string AngryAnim = "idle_01";
	public string IdleAnim = string.Empty;
	public string WalkAnim = string.Empty;

	public float Timer = 0.0f;

	public int Phase = 1;
	public int ID = 1;

	void Start()
	{
		Animation animation = this.GetComponent<Animation>();
		animation[AnimNames.FemaleAngryFace].layer = 2;
		animation.Play(AnimNames.FemaleAngryFace);
		animation[AnimNames.FemaleAngryFace].weight = 0.0f;

		if (!this.Leader)
		{
			if (GameObject.Find("DetectionCamera") != null)
			{
				this.DetectionMarker = Instantiate(this.Marker,
					GameObject.Find("DetectionPanel").transform.position,
					Quaternion.identity).GetComponent<DetectionMarkerScript>();
				this.DetectionMarker.transform.parent = GameObject.Find("DetectionPanel").transform;
				this.DetectionMarker.Target = this.transform;
			}
		}
	}

	void Update()
	{
		if (!this.Angry)
		{
			if (this.Phase == 1)
			{
				while (Vector3.Distance(this.transform.position, this.Destination.position) < 1.0f)
				{
					if (this.ID == 1)
					{
						this.Destination.position = new Vector3(
							Random.Range(-61.0f, -71.0f),
							this.Destination.position.y,
							Random.Range(-14.0f, 14.0f));
					}
					else
					{
						this.Destination.position = new Vector3(
							Random.Range(-28.0f, -23.0f),
							this.Destination.position.y,
							Random.Range(-15.0f, -7.0f));
					}
				}

				this.GetComponent<Animation>().CrossFade(this.WalkAnim);

				//this.Pathfinding.enabled = true;
				this.Moving = true;

				if (this.Leader)
				{
					this.Prompt.Hide();
					this.Prompt.enabled = false;

					this.PickpocketPanel.enabled = false;
				}

				this.Phase++;
			}
			else
			{
				if (this.Moving)
				{
					if (Vector3.Distance(this.transform.position, this.Destination.position) >= 1.0f)
					{
						Quaternion targetRotation = Quaternion.LookRotation(
							this.Destination.transform.position - this.transform.position);
						this.transform.rotation = Quaternion.Slerp(
							this.transform.rotation, targetRotation, 1.0f * Time.deltaTime);

						this.MyController.Move(transform.forward * Time.deltaTime);
						//this.transform.Translate(transform.forward * Time.deltaTime);
						//this.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
					}	
					else
					{
						this.GetComponent<Animation>().CrossFade(this.IdleAnim);
						//this.Pathfinding.enabled = false;
						this.Moving = false;

						if (this.Leader)
						{
							this.PickpocketPanel.enabled = true;
						}
					}
				}
				else
				{
					this.Timer += Time.deltaTime;

					if (this.Leader)
					{
						this.TimeBar.fillAmount = 1.0f -
							(this.Timer / this.GetComponent<Animation>()[this.IdleAnim].length);
					}

					if (this.Timer > this.GetComponent<Animation>()[this.IdleAnim].length)
					{
						if (this.Leader)
						{
							if (this.Yandere.Pickpocketing && (this.PickpocketMinigame.ID == this.ID))
							{
								this.PickpocketMinigame.Failure = true;
								this.PickpocketMinigame.End();
								this.Punish();
							}
						}

						this.Timer = 0.0f;
						this.Phase = 1;
					}
				}
			}

			if (this.Leader)
			{
				if (this.Prompt.Circle[0].fillAmount == 0.0f)
				{
					this.Prompt.Circle[0].fillAmount = 1;

					if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
						this.PickpocketMinigame.PickpocketSpot = this.PickpocketSpot;
						this.PickpocketMinigame.Show = true;
						this.PickpocketMinigame.ID = this.ID;

						this.Yandere.Character.GetComponent<Animation>().CrossFade(AnimNames.FemalePickpocketing);
						this.Yandere.Pickpocketing = true;
						this.Yandere.EmptyHands();

						this.Yandere.CanMove = false;
					}
				}

				if (this.PickpocketMinigame.ID == this.ID)
				{
					if (this.PickpocketMinigame.Success)
					{
						this.PickpocketMinigame.Success = false;
						this.PickpocketMinigame.ID = 0;

						if (this.ID == 1)
						{
							this.ShedDoor.Prompt.Label[0].text = "     " + "Open";
							this.Padlock.SetActive(false);
							this.ShedDoor.Locked = false;

							this.Yandere.Inventory.ShedKey = true;
						}
						else
						{
							this.CabinetDoor.Prompt.Label[0].text = "     " + "Open";
							this.CabinetDoor.Locked = false;

							this.Yandere.Inventory.CabinetKey = true;
						}

						this.Prompt.gameObject.SetActive(false);
						this.Key.SetActive(false);
					}

					if (this.PickpocketMinigame.Failure)
					{
						this.PickpocketMinigame.Failure = false;
						this.PickpocketMinigame.ID = 0;
						this.Punish();
					}
				}
			}
			else
			{
				this.LookForYandere();
			}
		}
		else
		{
			Quaternion targetRotation = Quaternion.LookRotation(
				this.Yandere.transform.position - this.transform.position);
			this.transform.rotation = Quaternion.Slerp(
				this.transform.rotation, targetRotation, 10.0f * Time.deltaTime);

			this.Timer += Time.deltaTime;

			if (this.Timer > 10.0f)
			{
				this.GetComponent<Animation>()[AnimNames.FemaleAngryFace].weight = 0.0f;
				this.Angry = false;
				this.Timer = 0.0f;
			}
			else if (this.Timer > 1.0f)
			{
				if (this.Phase == 0)
				{
					this.Subtitle.UpdateLabel(SubtitleType.PickpocketReaction, 0, 8.0f);
					this.Phase++;
				}
			}
		}

		if (Leader)
		{
			if (this.PickpocketPanel.enabled)
			{
				if (Yandere.PickUp == null && Yandere.Pursuer == null)
				{
					this.Prompt.enabled = true;
				}
				else
				{
					this.Prompt.enabled = false;
					this.Prompt.Hide();
				}
			}
		}
	}

	void Punish()
	{
		Animation animation = this.GetComponent<Animation>();
		animation[AnimNames.FemaleAngryFace].weight = 1.0f;
		animation.CrossFade(AngryAnim);

		this.Reputation.PendingRep -= 10.0f;
		this.CameraEffects.Alarm();
		this.Angry = true;
		this.Phase = 0;
		this.Timer = 0.0f;

		this.Prompt.Hide();
		this.Prompt.enabled = false;
		this.PickpocketPanel.enabled = false;
	}

	public GardeningClubMemberScript ClubLeader;
	public Camera VisionCone;
	public Transform Eyes;
	public float Alarm = 0.0f;

	void LookForYandere()
	{
		float DistanceToPlayer = Vector3.Distance(this.transform.position,
			this.Yandere.transform.position);

		if (DistanceToPlayer < this.VisionCone.farClipPlane)
		{
			Plane[] Planes = GeometryUtility.CalculateFrustumPlanes(this.VisionCone);

			if (GeometryUtility.TestPlanesAABB(Planes, this.Yandere.GetComponent<Collider>().bounds))
			{
				RaycastHit hit;

				/*
				Debug.DrawLine(this.Eyes.transform.position, new Vector3(
					this.Yandere.transform.position.x,
					this.Yandere.Head.position.y,
					this.Yandere.transform.position.z),
					Color.green);
				*/

				Vector3 linecastEnd = new Vector3(
					this.Yandere.transform.position.x,
					this.Yandere.Head.position.y,
					this.Yandere.transform.position.z);

				if (Physics.Linecast(this.Eyes.transform.position, linecastEnd, out hit))
				{
					if (hit.collider.gameObject == this.Yandere.gameObject)
					{
						if (this.Yandere.Pickpocketing)
						{
							if (!this.ClubLeader.Angry)
							{
								this.Alarm = Mathf.MoveTowards(
									this.Alarm, 100.0f, Time.deltaTime * (100.0f / DistanceToPlayer));

								if (this.Alarm == 100.0f)
								{
									this.PickpocketMinigame.NotNurse = true;
									this.PickpocketMinigame.End();
									this.ClubLeader.Punish();
								}
							}
							else
							{
								this.Alarm = Mathf.MoveTowards(this.Alarm, 0.0f, Time.deltaTime * 100.0f);
							}
						}
						else
						{
							this.Alarm = Mathf.MoveTowards(this.Alarm, 0.0f, Time.deltaTime * 100.0f);
						}
					}
					else
					{
						this.Alarm = Mathf.MoveTowards(this.Alarm, 0.0f, Time.deltaTime * 100.0f);
					}
				}
				else
				{
					this.Alarm = Mathf.MoveTowards(this.Alarm, 0.0f, Time.deltaTime * 100.0f);
				}
			}
			else
			{
				this.Alarm = Mathf.MoveTowards(this.Alarm, 0.0f, Time.deltaTime * 100.0f);
			}
		}

		this.DetectionMarker.Tex.transform.localScale = new Vector3(
			this.DetectionMarker.Tex.transform.localScale.x,
			this.Alarm / 100.0f,
			this.DetectionMarker.Tex.transform.localScale.z);

		if (this.Alarm > 0.0f)
		{
			if (!this.DetectionMarker.Tex.enabled)
			{
				this.DetectionMarker.Tex.enabled = true;
			}

			this.DetectionMarker.Tex.color = new Color(
				this.DetectionMarker.Tex.color.r,
				this.DetectionMarker.Tex.color.g,
				this.DetectionMarker.Tex.color.b,
				this.Alarm / 100.0f);
		}
		else
		{
			if (this.DetectionMarker.Tex.color.a != 0.0f)
			{
				this.DetectionMarker.Tex.enabled = false;
				this.DetectionMarker.Tex.color = new Color(
					this.DetectionMarker.Tex.color.r,
					this.DetectionMarker.Tex.color.g,
					this.DetectionMarker.Tex.color.b,
					0.0f);
			}
		}
	}
}