using UnityEngine;

public class LoveManagerScript : MonoBehaviour
{
	public ConfessionManagerScript ConfessionManager;
	public ConfessionSceneScript ConfessionScene;
	public StudentManagerScript StudentManager;
	public YandereScript Yandere;
	public ClockScript Clock;

	public StudentScript Follower;
	public StudentScript Suitor;
	public StudentScript Rival;

	public Transform FriendWaitSpot;
	public Transform[] Targets;
	public Transform MythHill;

	public int SuitorProgress = 0;
	public int TotalTargets = 0;
	public int Phase = 1;
	public int ID = 0;

	public int SuitorID = 28;
	public int RivalID = 30;

	public float AngleLimit = 0.0f;

	public bool WaitingToConfess = false;
	public bool ConfessToSuitor = false;
	public bool HoldingHands = false;
	public bool RivalWaiting = false;
	public bool LeftNote = false;
	public bool Courted = false;

	void Start()
	{
		this.SuitorProgress = DatingGlobals.SuitorProgress;

		//Debug.Log ("DatingGlobals.Affection is: " + DatingGlobals.Affection);

		if (DatingGlobals.Affection == 100)
		{
			this.ConfessToSuitor = true;
		}

		//Anti-Osana Code

		#if UNITY_EDITOR
		SuitorID = 6;
		RivalID = 11;
		#endif
	}

	void LateUpdate()
	{
		if (this.Follower != null)
		{
			if (this.Follower.Alive && !this.Follower.InCouple)
			{
				// [af] Converted while loop to for loop.
				for (this.ID = 0; this.ID < this.TotalTargets; this.ID++)
				{
					Transform target = this.Targets[this.ID];

					if (target != null)
					{
						if ((this.Follower.transform.position.y > (target.position.y - 2.0f)) &&
							(this.Follower.transform.position.y < (target.position.y + 2.0f)))
						{
							if (Vector3.Distance(this.Follower.transform.position, new Vector3(
								target.position.x, this.Follower.transform.position.y, target.position.z)) < 2.50f)
							{
								float Angle = Vector3.Angle(this.Follower.transform.forward, this.Follower.transform.position -
									new Vector3(target.position.x, this.Follower.transform.position.y, target.position.z));

								if (Mathf.Abs(Angle) > this.AngleLimit)
								{
									if (!this.Follower.Gush)
									{
										this.Follower.Cosmetic.MyRenderer.materials[2].SetFloat("_BlendAmount", 1.0f);
										this.Follower.GushTarget = target;

										// [af] This is the (unintuitive) Unity 5.3 way of changing emission.
										ParticleSystem.EmissionModule heartEmission = this.Follower.Hearts.emission;
										heartEmission.enabled = true;
										heartEmission.rateOverTime = 5.0f;

										this.Follower.Hearts.Play();
										this.Follower.Gush = true;
									}

									//Debug.Log("Something that she likes is in her cone of vision!");
								}
								else
								{
									this.Follower.Cosmetic.MyRenderer.materials[2].SetFloat("_BlendAmount", 0.0f);

									// [af] This is the (unintuitive) Unity 5.3 way of changing emission.
									ParticleSystem.EmissionModule heartEmission = this.Follower.Hearts.emission;
									heartEmission.enabled = false;

									this.Follower.Gush = false;
								}
							}
						}
					}
				}
			}
		}

		if (this.LeftNote)
		{
			if (this.Rival == null)
			{
				this.Rival = this.StudentManager.Students[this.RivalID];
			}

			if (this.Suitor == null)
			{
				if (this.ConfessToSuitor)
				{
					this.Suitor = this.StudentManager.Students[this.SuitorID];
				}
				else
				{
					this.Suitor = this.StudentManager.Students[1];
				}
			}

			if (Rival != null && Suitor != null)
			{
				if (Rival.Alive && Suitor.Alive && !Rival.Dying && !Suitor.Dying)
				{
					if (Rival.ConfessPhase == 5 && Suitor.ConfessPhase == 3)
					{
						this.WaitingToConfess = true;

						float mythHillDistance = Vector3.Distance(
							this.Yandere.transform.position, this.MythHill.position);

						if (this.WaitingToConfess)
						{
							if (!this.Yandere.Chased && this.Yandere.Chasers == 0 && mythHillDistance > 10.0f && mythHillDistance < 25.0f)
							{
								this.BeginConfession();
							}
						}
					}
				}
			}
		}

		if (this.HoldingHands)
		{
			if (this.Rival == null)
			{
				this.Rival = this.StudentManager.Students[this.RivalID];
			}

			if (this.Suitor == null)
			{
				this.Suitor = this.StudentManager.Students[this.SuitorID];
			}

			Rival.MyController.Move(this.transform.forward * Time.deltaTime);
			Suitor.transform.position = new Vector3(
				Rival.transform.position.x - 0.50f,
				Rival.transform.position.y,
				Rival.transform.position.z);

			if (Rival.transform.position.z > -50.0f)
			{
				Suitor.MyController.radius = 0.12f;
				Suitor.enabled = true;

				Suitor.Cosmetic.MyRenderer.materials[Suitor.Cosmetic.FaceID].SetFloat("_BlendAmount", 0.0f);

				// [af] This is the (unintuitive) Unity 5.3 way of changing emission.
				ParticleSystem.EmissionModule SuitorHeartEmission = Suitor.Hearts.emission;
				SuitorHeartEmission.enabled = false;

				Rival.MyController.radius = 0.12f;
				Rival.enabled = true;

				Rival.Cosmetic.MyRenderer.materials[2].SetFloat("_BlendAmount", 0.0f);

				// [af] This is the (unintuitive) Unity 5.3 way of changing emission.
				ParticleSystem.EmissionModule RivalHeartEmission = Rival.Hearts.emission;
				RivalHeartEmission.enabled = false;

				Suitor.HoldingHands = false;
				Rival.HoldingHands = false;

				this.HoldingHands = false;
			}
		}
	}

	public void CoupleCheck()
	{
		if (this.SuitorProgress == 2)
		{
			Rival = this.StudentManager.Students[this.RivalID];
			Suitor = this.StudentManager.Students[this.SuitorID];

			if ((Rival != null) && (Suitor != null))
			{
				Suitor.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
				Rival.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
				Suitor.CharacterAnimation.enabled = true;
				Rival.CharacterAnimation.enabled = true;

				Suitor.CharacterAnimation.Play(AnimNames.MaleWalkHands);
				Suitor.transform.eulerAngles = Vector3.zero;
				Suitor.transform.position = new Vector3(-0.25f, 0.0f, -90.0f);
				Suitor.Pathfinding.canSearch = false;
				Suitor.Pathfinding.canMove = false;
				Suitor.MyController.radius = 0.0f;
				Suitor.enabled = false;

				Rival.CharacterAnimation.Play(AnimNames.FemaleWalkHands);
				Rival.transform.eulerAngles = Vector3.zero;
				Rival.transform.position = new Vector3(0.25f, 0.0f, -90.0f);
				Rival.Pathfinding.canSearch = false;
				Rival.Pathfinding.canMove = false;
				Rival.MyController.radius = 0.0f;
				Rival.enabled = false;

				Physics.SyncTransforms();

				Suitor.Cosmetic.MyRenderer.materials[Suitor.Cosmetic.FaceID].SetFloat("_BlendAmount", 1.0f);

				// [af] This is the (unintuitive) Unity 5.3 way of changing emission.
				ParticleSystem.EmissionModule SuitorHeartEmission = Suitor.Hearts.emission;
				SuitorHeartEmission.enabled = true;
				SuitorHeartEmission.rateOverTime = 5.0f;
				Suitor.Hearts.Play();

				Rival.Cosmetic.MyRenderer.materials[2].SetFloat("_BlendAmount", 1.0f);

				// [af] This is the (unintuitive) Unity 5.3 way of changing emission.
				ParticleSystem.EmissionModule RivalHeartEmission = Rival.Hearts.emission;
				RivalHeartEmission.enabled = true;
				RivalHeartEmission.rateOverTime = 5.0f;
				Rival.Hearts.Play();

				Suitor.HoldingHands = true;
				Rival.HoldingHands = true;

				Suitor.CoupleID = SuitorID;
				Rival.CoupleID = RivalID;

				this.HoldingHands = true;

				Debug.Log("Students are now holding hands.");
			}
		}
	}

	public void BeginConfession()
	{
		this.Suitor.EmptyHands();
		this.Rival.EmptyHands();

		this.Yandere.CharacterAnimation.CrossFade(this.Yandere.IdleAnim);
		this.Yandere.RPGCamera.enabled = false;
		this.Yandere.CanMove = false;

		this.StudentManager.DisableEveryone();

		this.Suitor.gameObject.SetActive(true);
		this.Rival.gameObject.SetActive(true);

		this.Suitor.enabled = false;
		this.Rival.enabled = false;

		if (!this.ConfessToSuitor)
		{
			#if UNITY_EDITOR
			this.ConfessionManager.Senpai = this.StudentManager.Students[1].CharacterAnimation;
			#endif

			this.ConfessionManager.gameObject.SetActive(true);
		}
		else
		{
			this.ConfessionScene.enabled = true;
		}
			
		this.Clock.StopTime = true;
		this.LeftNote = false;
	}
}