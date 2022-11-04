using UnityEngine;

public enum RagdollDumpType
{
	None,
	Incinerator,
	TranqCase,
	WoodChipper
}

public class RagdollScript : MonoBehaviour
{
	public BloodPoolSpawnerScript BloodPoolSpawner;
	public DetectionMarkerScript DetectionMarker;
	public IncineratorScript Incinerator;
	public WoodChipperScript WoodChipper;
	public TranqCaseScript TranqCase;
	public StudentScript Student;
	public YandereScript Yandere;
	public PoliceScript Police;
	public PromptScript Prompt;

	public SkinnedMeshRenderer MyRenderer;
	public Collider BloodSpawnerCollider;
	public Animation CharacterAnimation;
	public Collider HideCollider;

	public Rigidbody[] AllRigidbodies;
	public Collider[] AllColliders;
	public Rigidbody[] Rigidbodies;
	public Transform[] SpawnPoints;
	public GameObject[] BodyParts;

	public Transform NearestLimb;
	public Transform RightBreast;
	public Transform LeftBreast;
	public Transform PelvisRoot;
	public Transform Ponytail;
	public Transform RightEye;
	public Transform LeftEye;
	public Transform HairR;
	public Transform HairL;
	public Transform[] Limb;
	public Transform Head;

	public Vector3 RightEyeOrigin;
	public Vector3 LeftEyeOrigin;
	public Vector3[] LimbAnchor;

	public GameObject Character;
	public GameObject Zs;

	public bool ElectrocutionAnimation = false;
	public bool MurderSuicideAnimation = false;
	public bool BurningAnimation = false;
	public bool ChokingAnimation = false;

    public bool AddingToCount = false;
	public bool MurderSuicide = false;
	public bool Cauterizable = false;
	public bool Electrocuted = false;
	public bool StopAnimation = true;
	public bool Decapitated = false;
	public bool Dismembered = false;
	public bool NeckSnapped = false;
	public bool Cauterized = false;
	public bool Disturbing = false;
	public bool Sacrifice = false;
	public bool Disposed = false;
	public bool Poisoned = false;
	public bool Tranquil = false;
	public bool Burning = false;
	public bool Carried = false;
	public bool Choking = false;
	public bool Dragged = false;
	public bool Drowned = false;
	public bool Falling = false;
	public bool Nemesis = false;
	public bool Settled = false;
	public bool Suicide = false;
	public bool Burned = false;
	public bool Dumped = false;
	public bool Hidden = false;
	public bool Pushed = false;
	public bool Male = false;

	public float AnimStartTime = 0.0f;
	public float SettleTimer = 0.0f;
	public float BreastSize = 0.0f;
	public float DumpTimer = 0.0f;
	public float EyeShrink = 0.0f;
	public float FallTimer = 0.0f;

	public int StudentID = 0;
	public RagdollDumpType DumpType = RagdollDumpType.None;
	public int LimbID = 0;
	public int Frame = 0;

	public string DumpedAnim = string.Empty;
	public string LiftAnim = string.Empty;
	public string IdleAnim = string.Empty;
	public string WalkAnim = string.Empty;
	public string RunAnim = string.Empty;

    public bool UpdateNextFrame = false;
    public Vector3 NextPosition;
    public Quaternion NextRotation;
    public int Frames;

    void Start()
	{
		this.ElectrocutionAnimation = false;
		this.MurderSuicideAnimation = false;
		this.BurningAnimation = false;
		this.ChokingAnimation = false;
		this.Disturbing = false;

		Physics.IgnoreLayerCollision(11, 13, true);

		this.Zs.SetActive(this.Tranquil);

		if (!this.Tranquil && !this.Poisoned && !this.Drowned &&
			!this.Electrocuted && !this.Burning && !this.NeckSnapped)
		{
			this.Student.StudentManager.TutorialWindow.ShowPoolMessage = true;

			this.BloodPoolSpawner.gameObject.SetActive(true);
            this.BloodPoolSpawner.StudentManager = this.Student.StudentManager;

            if (this.Pushed)
			{
				this.BloodPoolSpawner.Timer = 5.0f;
			}
		}

		for (int ID = 0; ID < this.AllRigidbodies.Length; ID++)
		{
			this.AllRigidbodies[ID].isKinematic = false;
			this.AllColliders[ID].enabled = true;

			if (this.Yandere.StudentManager.NoGravity)
			{
				this.AllRigidbodies[ID].useGravity = false;
			}
		}

		this.Prompt.enabled = true;

		if (((this.Yandere.Class.PhysicalGrade + this.Yandere.Class.PhysicalBonus) > 0) && !this.Tranquil)
		{
			this.Prompt.HideButton[3] = false;
		}

		if (this.Student.Yandere.BlackHole)
		{
			this.DestroyRigidbodies();
		}
	}

	void Update()
	{
        if (UpdateNextFrame)
        {
            //if (Frame > 5)
            //{
                Student.Hips.localPosition = NextPosition;
                Student.Hips.localRotation = NextRotation;
                Physics.SyncTransforms();

                UpdateNextFrame = false;
            //}

            //Frame++;

            //Debug.Log("We have moved Midori's hips to: " + Student.Hips.localPosition);
        }

		if (!this.Dragged && !this.Carried)
		{
			if (!this.Settled)
			{
				if (!this.Yandere.PK)
				{
					if (!this.Yandere.StudentManager.NoGravity)
					{
						this.SettleTimer += Time.deltaTime;

						if (this.SettleTimer > 5.0f)
						{
							this.Settled = true;

							for (int ID = 0; ID < this.AllRigidbodies.Length; ID++)
							{
								this.AllRigidbodies[ID].isKinematic = true;
								this.AllColliders[ID].enabled = false;
							}
						}
					}
				}
			}
		}

		if (this.DetectionMarker != null)
		{
			if (this.DetectionMarker.Tex.color.a > 0.10f)
			{
				this.DetectionMarker.Tex.color = new Color(
					this.DetectionMarker.Tex.color.r,
					this.DetectionMarker.Tex.color.g,
					this.DetectionMarker.Tex.color.b,
					Mathf.MoveTowards(this.DetectionMarker.Tex.color.a, 0.0f, Time.deltaTime * 10.0f));
			}
			else
			{
				this.DetectionMarker.Tex.color = new Color(
					this.DetectionMarker.Tex.color.r,
					this.DetectionMarker.Tex.color.g,
					this.DetectionMarker.Tex.color.b,
					0.0f);

				this.DetectionMarker = null;
			}
		}

		if (!this.Dumped)
		{
			if (this.StopAnimation)
			{
				if (this.Student.CharacterAnimation.isPlaying)
				{
					this.Student.CharacterAnimation.Stop();
				}
			}

			//if (!this.Yandere.Running)
			//{
			if (this.BloodPoolSpawner != null)
			{
				if (this.BloodPoolSpawner.gameObject.activeInHierarchy)
				{
					if (!this.Cauterized)
					{
						if (this.Yandere.PickUp != null)
						{
							if (this.Yandere.PickUp.Blowtorch)
							{
								if (!this.Cauterizable)
								{
									this.Prompt.Label[0].text = "     " + "Cauterize";
									this.Cauterizable = true;
								}
							}
							else
							{
								if (this.Cauterizable)
								{
									this.Prompt.Label[0].text = "     " + "Dismember";
									this.Cauterizable = false;
								}
							}
						}
						else
						{
							if (this.Cauterizable)
							{
								this.Prompt.Label[0].text = "     " + "Dismember";
								this.Cauterizable = false;
							}
						}
					}
				}
			}

			if (this.Prompt.Circle[0].fillAmount == 0.0f)
			{
				this.Prompt.Circle[0].fillAmount = 1.0f;

				if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
				{
					if (this.Cauterizable)
					{
						this.Prompt.Label[0].text = "     " + "Dismember";

						this.BloodPoolSpawner.enabled = false;
						this.Cauterizable = false;
						this.Cauterized = true;

						this.Yandere.CharacterAnimation.CrossFade(AnimNames.FemaleCauterize);
						this.Yandere.Cauterizing = true;
						this.Yandere.CanMove = false;

						this.Yandere.PickUp.GetComponent<BlowtorchScript>().enabled = true;
						this.Yandere.PickUp.GetComponent<AudioSource>().Play();
					}
					else
					{
						if (this.Yandere.StudentManager.OriginalUniforms + this.Yandere.StudentManager.NewUniforms > 1)
						{
							this.Yandere.CharacterAnimation.CrossFade(AnimNames.FemaleDismember);
							this.Yandere.transform.LookAt(this.transform);

							this.Yandere.RPGCamera.transform.position = this.Yandere.DismemberSpot.position;
							this.Yandere.RPGCamera.transform.eulerAngles = this.Yandere.DismemberSpot.eulerAngles;
							this.Yandere.EquippedWeapon.Dismember();
							this.Yandere.RPGCamera.enabled = false;
							this.Yandere.TargetStudent = this.Student;
							this.Yandere.Ragdoll = this.gameObject;
							this.Yandere.Dismembering = true;
							this.Yandere.CanMove = false;
						}
						else
						{
							if (!this.Yandere.ClothingWarning)
							{
								this.Yandere.NotificationManager.DisplayNotification(NotificationType.Clothing);

								this.Yandere.StudentManager.TutorialWindow.ShowClothingMessage = true;
								this.Yandere.ClothingWarning = true;
							}
						}
					}
				}
			}

			if (this.Prompt.Circle[1].fillAmount == 0.0f)
			{
				this.Prompt.Circle[1].fillAmount = 1.0f;

				if (!this.Student.FireEmitters[1].isPlaying)
				{
					if (!this.Dragged)
					{
						this.Yandere.EmptyHands();

						this.Prompt.AcceptingInput[1] = false;
						this.Prompt.Label[1].text = "     " + "Drop";

						this.PickNearestLimb();

						this.Yandere.RagdollDragger.connectedBody = this.Rigidbodies[this.LimbID];
						this.Yandere.RagdollDragger.connectedAnchor = this.LimbAnchor[this.LimbID];

						this.Yandere.Dragging = true;
						this.Yandere.Running = false;
						this.Yandere.DragState = 0;

						this.Yandere.Ragdoll = this.gameObject;

						this.Dragged = true;

						this.Yandere.StudentManager.UpdateStudents();

						if (this.MurderSuicide)
						{
							this.Police.MurderSuicideScene = false;
							this.MurderSuicide = false;
						}

						if (this.Suicide)
						{
							this.Police.Suicide = false;
							this.Suicide = false;
						}

						// [af] Converted while loop to for loop.
						for (int ID = 0; ID < this.Student.Ragdoll.AllRigidbodies.Length; ID++)
						{
							this.Student.Ragdoll.AllRigidbodies[ID].drag = 2.0f;
						}

						// [af] Converted while loop to for loop.
						for (int ID = 0; ID < this.AllRigidbodies.Length; ID++)
						{
							this.AllRigidbodies[ID].isKinematic = false;
							this.AllColliders[ID].enabled = true;

							if (this.Yandere.StudentManager.NoGravity)
							{
								this.AllRigidbodies[ID].useGravity = false;
							}
						}

						// [af] Ignoring "ID = 0;" in JS code since it's a loop variable.
					}
					else
					{
						this.StopDragging();
					}
				}
			}

			if (this.Prompt.Circle[3].fillAmount == 0.0f)
			{
				this.Prompt.Circle[3].fillAmount = 1.0f;

				if (!this.Student.FireEmitters[1].isPlaying)
				{
					this.Yandere.EmptyHands();

					this.Prompt.Label[1].text = "     " + "Drop";
					this.Prompt.HideButton[1] = true;
					this.Prompt.HideButton[3] = true;

					this.Prompt.enabled = false;
					this.Prompt.Hide();

					for (int ID = 0; ID < this.AllRigidbodies.Length; ID++)
					{
						this.AllRigidbodies[ID].isKinematic = true;
						this.AllColliders[ID].enabled = false;
					}

					if (this.Male)
					{
						Rigidbody rigidBody0 = this.AllRigidbodies[0];
						rigidBody0.transform.parent.transform.localPosition = new Vector3(
							rigidBody0.transform.parent.transform.localPosition.x,
							0.20f,
							rigidBody0.transform.parent.transform.localPosition.z);
					}

					this.Yandere.CharacterAnimation.Play(AnimNames.FemaleCarryLiftA);
                    this.Student.CharacterAnimation.enabled = true;
                    this.Student.CharacterAnimation.Play(this.LiftAnim);

					this.BloodSpawnerCollider.enabled = false;

					this.PelvisRoot.localEulerAngles = new Vector3(
						this.PelvisRoot.localEulerAngles.x,
						0.0f,
						this.PelvisRoot.localEulerAngles.z);

					this.Prompt.MyCollider.enabled = false;

					this.PelvisRoot.localPosition = new Vector3(
						this.PelvisRoot.localPosition.x,
						this.PelvisRoot.localPosition.y,
						0.0f);

					this.Yandere.Ragdoll = this.gameObject;
					this.Yandere.CurrentRagdoll = this;
					this.Yandere.CanMove = false;
					this.Yandere.Lifting = true;
					this.StopAnimation = false;
					this.Carried = true;
					this.Falling = false;

					this.FallTimer = 0.0f;
				}
			}

			if (this.Yandere.Running)
			{
				if (this.Yandere.CanMove)
				{
					if (this.Dragged)
					{
						this.StopDragging();
					}
				}
			}

			if (Vector3.Distance(this.Yandere.transform.position, this.Prompt.transform.position) < 2.0f)
			{
				if (!this.Suicide && !this.AddingToCount)
				{
					this.Yandere.NearestCorpseID = this.StudentID;
					this.Yandere.NearBodies++;
					this.AddingToCount = true;
				}
			}
			else
			{
				if (this.AddingToCount)
				{
					this.Yandere.NearBodies--;
					this.AddingToCount = false;
				}
			}

			if (!this.Prompt.AcceptingInput[1])
			{
				if (Input.GetButtonUp(InputNames.Xbox_B))
				{
					this.Prompt.AcceptingInput[1] = true;
				}
			}

			bool ValidItem = false;

			if (this.Yandere.Armed)
			{
				if (this.Yandere.EquippedWeapon.WeaponID == 7 && !this.Student.Nemesis)
				{
					ValidItem = true;
				}
			}

			if (!this.Cauterized)
			{
				if (this.Yandere.PickUp != null)
				{
					if (this.Yandere.PickUp.Blowtorch)
					{
						if (this.BloodPoolSpawner.gameObject.activeInHierarchy)
						{
							ValidItem = true;
						}
					}
				}
			}

			// [af] Replaced if/else statement with boolean expression.
			this.Prompt.HideButton[0] = this.Dragged || this.Carried || this.Tranquil || !ValidItem;
		}
		else
		{
			if (this.DumpType == RagdollDumpType.Incinerator)
			{
				if (this.DumpTimer == 0.0f)
				{
					if (this.Yandere.Carrying)
					{
						this.Student.CharacterAnimation[this.DumpedAnim].time = 2.50f;
					}
				}

				this.Student.CharacterAnimation.CrossFade(this.DumpedAnim);

				this.DumpTimer += Time.deltaTime;

				if (this.Student.CharacterAnimation[this.DumpedAnim].time >=
					this.Student.CharacterAnimation[this.DumpedAnim].length)
				{
					this.Incinerator.Corpses++;
					this.Incinerator.CorpseList[this.Incinerator.Corpses] = this.StudentID;
					this.Remove();

					this.enabled = false;
				}
			}
			else if (this.DumpType == RagdollDumpType.TranqCase)
			{
				if (this.DumpTimer == 0.0f)
				{
					if (this.Yandere.Carrying)
					{
						this.Student.CharacterAnimation[this.DumpedAnim].time = 2.50f;
					}
				}

				this.Student.CharacterAnimation.CrossFade(this.DumpedAnim);

				this.DumpTimer += Time.deltaTime;

				if (this.Student.CharacterAnimation[this.DumpedAnim].time >=
					this.Student.CharacterAnimation[this.DumpedAnim].length)
				{
					this.TranqCase.Open = false;

					if (this.AddingToCount)
					{
						this.Yandere.NearBodies--;
					}

					this.enabled = false;
				}
			}
			else if (this.DumpType == RagdollDumpType.WoodChipper)
			{
				if (this.DumpTimer == 0.0f)
				{
					if (this.Yandere.Carrying)
					{
						this.Student.CharacterAnimation[this.DumpedAnim].time = 2.50f;
					}
				}

				this.Student.CharacterAnimation.CrossFade(this.DumpedAnim);

				this.DumpTimer += Time.deltaTime;

				if (this.Student.CharacterAnimation[this.DumpedAnim].time >=
					this.Student.CharacterAnimation[this.DumpedAnim].length)
				{
					this.WoodChipper.VictimID = this.StudentID;
					this.Remove();

					this.enabled = false;
				}
			}
		}

		if (this.Hidden)
		{
			if (this.HideCollider == null)
			{
				this.Police.HiddenCorpses--;
				this.Hidden = false;
			}
		}

		if (this.Falling)
		{
			this.FallTimer += Time.deltaTime;

			if (this.FallTimer > 2.0f)
			{
				this.BloodSpawnerCollider.enabled = true;
				this.FallTimer = 0.0f;
				this.Falling = false;
			}
		}

		if (this.Burning)
		{
			// [af] Moved assignments into for loop.
			for (int i = 0; i < 3; i++)
			{
				Material material = this.MyRenderer.materials[i];
				material.color = Vector4.MoveTowards(material.color,
					new Vector4(0.10f, 0.10f, 0.10f, 1.0f), Time.deltaTime * 0.10f);
			}

			this.Student.Cosmetic.HairRenderer.material.color = Vector4.MoveTowards(
				this.Student.Cosmetic.HairRenderer.material.color,
				new Vector4(0.10f, 0.10f, 0.10f, 1.0f),
				Time.deltaTime * 0.10f);

			if (this.MyRenderer.materials[0].color == new Color(0.10f, 0.10f, 0.10f, 1.0f))
			{
				this.Burning = false;
				this.Burned = true;
			}
		}

		if (this.Burned)
		{
			// [af] Replaced if/else statement with boolean expression.
			this.Sacrifice = Vector3.Distance(this.Prompt.transform.position,
				this.Yandere.StudentManager.SacrificeSpot.position) < 1.50f;
		}
	}

	void LateUpdate()
	{
		if (!this.Male)
		{
			// [af] Commented in JS code.
			//RightBreast.localScale = Vector3(BreastSize, BreastSize, BreastSize);
			//LeftBreast.localScale = Vector3(BreastSize, BreastSize, BreastSize);

			if (this.LeftEye != null)
			{
				this.LeftEye.localPosition = new Vector3(
					this.LeftEye.localPosition.x,
					this.LeftEye.localPosition.y,
					this.LeftEyeOrigin.z - (this.EyeShrink * 0.010f));

				this.RightEye.localPosition = new Vector3(
					this.RightEye.localPosition.x,
					this.RightEye.localPosition.y,
					this.RightEyeOrigin.z + (this.EyeShrink * 0.010f));

				this.LeftEye.localScale = new Vector3(
					1.0f - (this.EyeShrink * 0.50f),
					1.0f - (this.EyeShrink * 0.50f),
					this.LeftEye.localScale.z);

				this.RightEye.localScale = new Vector3(
					1.0f - (this.EyeShrink * 0.50f),
					1.0f - (this.EyeShrink * 0.50f),
					this.RightEye.localScale.z);
			}

			if (this.StudentID == 81)
			{
				// [af] Moved assignments into for loop.
				for (int i = 0; i < 4; i++)
				{
					Transform skirt = this.Student.Skirt[i];
					skirt.transform.localScale = new Vector3(
						skirt.transform.localScale.x,
						2.0f / 3.0f,
						skirt.transform.localScale.z);
				}
			}
		}

		if (this.Decapitated)
		{
			this.Head.localScale = Vector3.zero;
		}

		if (this.Yandere.Ragdoll == this.gameObject)
		{
			if (this.Yandere.DumpTimer < 1.0f)
			{
				if (this.Yandere.Lifting)
				{
					this.transform.position = this.Yandere.transform.position;
					this.transform.eulerAngles = this.Yandere.transform.eulerAngles;
				}
				else if (this.Carried)
				{
					this.transform.position = this.Yandere.transform.position;
					this.transform.eulerAngles = this.Yandere.transform.eulerAngles;

					float v = Input.GetAxis("Vertical");
					float h = Input.GetAxis("Horizontal");

					if ((v != 0.0f) || (h != 0.0f))
					{
						// [af] Replaced if/else statement with ternary expression.
						this.Student.CharacterAnimation.CrossFade(
							this.Yandere.Running ? this.RunAnim : this.WalkAnim);
					}
					else
					{
						this.Student.CharacterAnimation.CrossFade(this.IdleAnim);
					}

					this.Student.CharacterAnimation[this.IdleAnim].time =
						this.Yandere.CharacterAnimation[AnimNames.FemaleCarryIdleA].time;
					this.Student.CharacterAnimation[this.WalkAnim].time =
						this.Yandere.CharacterAnimation[AnimNames.FemaleCarryWalkA].time;
					this.Student.CharacterAnimation[this.RunAnim].time =
						this.Yandere.CharacterAnimation[AnimNames.FemaleCarryRunA].time;
				}
			}

			if (this.Carried)
			{
				if (this.Male)
				{
					Rigidbody rigidBody0 = this.AllRigidbodies[0];
					rigidBody0.transform.parent.transform.localPosition = new Vector3(
						rigidBody0.transform.parent.transform.localPosition.x,
						0.20f,
						rigidBody0.transform.parent.transform.localPosition.z);
				}

				if (this.Yandere.Struggling || this.Yandere.DelinquentFighting || this.Yandere.Sprayed)
				{
					this.Fall();
				}
			}
		}
	}

	public void StopDragging()
	{
		// [af] Converted while loop to foreach loop.
		foreach (Rigidbody rigidBody in this.Student.Ragdoll.AllRigidbodies)
		{
			rigidBody.drag = 0.0f;
		}

		if (((this.Yandere.Class.PhysicalGrade + this.Yandere.Class.PhysicalBonus) > 0) && !this.Tranquil)
		{
			this.Prompt.HideButton[3] = false;
		}

		this.Prompt.AcceptingInput[1] = true;
		this.Prompt.Circle[1].fillAmount = 1.0f;
		this.Prompt.Label[1].text = "     " + "Drag";

		this.Yandere.RagdollDragger.connectedBody = null;
		this.Yandere.RagdollPK.connectedBody = null;

		this.Yandere.Dragging = false;
		this.Yandere.Ragdoll = null;

		this.Yandere.StudentManager.UpdateStudents();

		this.SettleTimer = 0.0f;
		this.Settled = false;
		this.Dragged = false;
	}

	void PickNearestLimb()
	{
		this.NearestLimb = this.Limb[0];
		this.LimbID = 0;

		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < 4; ID++)
		{
			Transform limb = this.Limb[ID];

			if (Vector3.Distance(limb.position, this.Yandere.transform.position) <
				Vector3.Distance(this.NearestLimb.position, this.Yandere.transform.position))
			{
				this.NearestLimb = limb;
				this.LimbID = ID;
			}
		}
	}

	public void Dump()
	{
		if (this.DumpType == RagdollDumpType.Incinerator)
		{
			this.transform.eulerAngles = this.Yandere.transform.eulerAngles;
			this.transform.position = this.Yandere.transform.position;
			this.Incinerator = this.Yandere.Incinerator;
			this.BloodPoolSpawner.enabled = false;
		}
		else if (this.DumpType == RagdollDumpType.TranqCase)
		{
			this.TranqCase = this.Yandere.TranqCase;
		}
		else if (this.DumpType == RagdollDumpType.WoodChipper)
		{
			this.WoodChipper = this.Yandere.WoodChipper;
		}

		this.Prompt.Hide();
		this.Prompt.enabled = false;
		this.Dumped = true;

		// [af] Converted while loop to foreach loop.
		foreach (Rigidbody rigidBody in this.AllRigidbodies)
		{
			rigidBody.isKinematic = true;
		}
	}

	public void Fall()
	{
		this.transform.position = new Vector3(
			this.transform.position.x,
			this.transform.position.y + 0.00010f,
			this.transform.position.z);

		this.Prompt.Label[1].text = "     " + "Drag";
		this.Prompt.HideButton[1] = false;
		this.Prompt.enabled = true;

		if (((this.Yandere.Class.PhysicalGrade + this.Yandere.Class.PhysicalBonus) > 0) && !this.Tranquil)
		{
			this.Prompt.HideButton[3] = false;
		}

		if (this.Dragged)
		{
			this.Yandere.RagdollDragger.connectedBody = null;
			this.Yandere.RagdollPK.connectedBody = null;
			this.Yandere.Dragging = false;
			this.Dragged = false;
		}

		this.Yandere.Ragdoll = null;

		this.Prompt.MyCollider.enabled = true;
		this.BloodPoolSpawner.NearbyBlood = 0;
		this.StopAnimation = true;
		this.SettleTimer = 0.0f;
		this.Carried = false;
		this.Settled = false;
		this.Falling = true;

		// [af] Converted while loop to for loop.
		for (int ID = 0; ID < this.AllRigidbodies.Length; ID++)
		{
			this.AllRigidbodies[ID].isKinematic = false;
			this.AllColliders[ID].enabled = true;
		}
	}

	public void QuickDismember()
	{
		for (int ID = 0; ID < this.BodyParts.Length; ID++)
		{
			GameObject NewBodyPart = Instantiate(this.BodyParts[ID],
				this.SpawnPoints[ID].position, Quaternion.identity);

			NewBodyPart.transform.eulerAngles = this.SpawnPoints[ID].eulerAngles;

			NewBodyPart.GetComponent<PromptScript>().enabled = false;
			NewBodyPart.GetComponent<PickUpScript>().enabled = false;
			NewBodyPart.GetComponent<OutlineScript>().enabled = false;
		}

		if (this.BloodPoolSpawner.BloodParent == null)
		{
			this.BloodPoolSpawner.Start();
		}

		Debug.Log("BloodPoolSpawner.transform.position is: " + BloodPoolSpawner.transform.position);

		Debug.Log("Student.StudentManager.SEStairs.bounds is: " + Student.StudentManager.SEStairs.bounds);

		if (!Student.StudentManager.NEStairs.bounds.Contains(BloodPoolSpawner.transform.position) &&
			!Student.StudentManager.NWStairs.bounds.Contains(BloodPoolSpawner.transform.position) &&
			!Student.StudentManager.SEStairs.bounds.Contains(BloodPoolSpawner.transform.position) &&
			!Student.StudentManager.SWStairs.bounds.Contains(BloodPoolSpawner.transform.position))
		{
			this.BloodPoolSpawner.SpawnBigPool();
		}

		this.gameObject.SetActive(false);
	}

	public void Dismember()
	{
		if (!this.Dismembered)
		{
			this.Student.LiquidProjector.material.mainTexture = this.Student.BloodTexture;

			// [af] Converted while loop to for loop.
			for (int ID = 0; ID < this.BodyParts.Length; ID++)
			{
				if (this.Decapitated)
				{
					ID++;
					this.Decapitated = false;
				}

				GameObject NewBodyPart = Instantiate(this.BodyParts[ID],
					this.SpawnPoints[ID].position, Quaternion.identity);

				NewBodyPart.transform.parent = this.Yandere.LimbParent;

				NewBodyPart.transform.eulerAngles = this.SpawnPoints[ID].eulerAngles;

				BodyPartScript NewBodyPartScript = NewBodyPart.GetComponent<BodyPartScript>();
				NewBodyPartScript.StudentID = this.StudentID;
				NewBodyPartScript.Sacrifice = this.Sacrifice;

				if (this.Yandere.StudentManager.NoGravity)
				{
					NewBodyPart.GetComponent<Rigidbody>().useGravity = false;
				}

				if (ID == 0)
				{
					if (!this.Student.OriginallyTeacher)
					{
						if (!this.Male)
						{
							// [af] Removed redundant assignment.
							this.Student.Cosmetic.FemaleHair[this.Student.Cosmetic.Hairstyle]
								.transform.parent = NewBodyPart.transform;

							if (this.Student.Cosmetic.FemaleAccessories[this.Student.Cosmetic.Accessory] != null)
							{
								if ((this.Student.Cosmetic.Accessory != 3) &&
									(this.Student.Cosmetic.Accessory != 6))
								{
									this.Student.Cosmetic.FemaleAccessories[this.Student.Cosmetic.Accessory]
										.transform.parent = NewBodyPart.transform;
								}
							}
						}
						else
						{
							// [af] Removed redundant assignment.
							Transform Hair = this.Student.Cosmetic.MaleHair[this.Student.Cosmetic.Hairstyle].transform;
							Hair.parent = NewBodyPart.transform;

							// [af] Replaced scalar multiplications with vector multiplication.
							Hair.localScale *= 1.06382978723f;

							if (Hair.transform.localPosition.y < -1.0f)
							{
								Hair.transform.localPosition = new Vector3(
									Hair.transform.localPosition.x,
									Hair.transform.localPosition.y - 0.095f,
									Hair.transform.localPosition.z);
							}

							if (this.Student.Cosmetic.MaleAccessories[this.Student.Cosmetic.Accessory] != null)
							{
								this.Student.Cosmetic.MaleAccessories[this.Student.Cosmetic.Accessory]
									.transform.parent = NewBodyPart.transform;
							}
						}
					}
					else
					{
						// [af] Removed redundant assignment.
						this.Student.Cosmetic.TeacherHair[this.Student.Cosmetic.Hairstyle].transform.parent = NewBodyPart.transform;

						if (this.Student.Cosmetic.TeacherAccessories[this.Student.Cosmetic.Accessory] != null)
						{
							this.Student.Cosmetic.TeacherAccessories[this.Student.Cosmetic.Accessory]
								.transform.parent = NewBodyPart.transform;
						}
					}

					if (this.Student.Club != ClubType.Photography && this.Student.Club < ClubType.Gaming)
					{
						if (this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club] != null)
						{
							this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club].transform.parent =
								NewBodyPart.transform;

							if (this.Student.Club == ClubType.Occult)
							{
								if (!this.Male)
								{
									this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club]
										.transform.localPosition = new Vector3(0.0f, -1.50f, 0.010f);
									this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club]
										.transform.localEulerAngles = Vector3.zero;
								}
								else
								{
									this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club]
										.transform.localPosition = new Vector3(0.0f, -1.42f, 0.0050f);
									this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club]
										.transform.localEulerAngles = Vector3.zero;
								}
							}
						}
					}

					NewBodyPart.GetComponent<Renderer>().materials[0].mainTexture =
						this.Student.Cosmetic.FaceTexture;

					if (ID == 0)
					{
						NewBodyPart.transform.position += new Vector3(0, 1, 0);
					}
				}
				else if (ID == 1)
				{
					if (this.Student.Club == ClubType.Photography)
					{
						if (this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club] != null)
						{
							this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club].transform.parent =
								NewBodyPart.transform;
						}
					}
				}
				else if (ID == 2)
				{
					if (!this.Student.Male)
					{
						if (this.Student.Cosmetic.Accessory == 6)
						{
							this.Student.Cosmetic.FemaleAccessories[this.Student.Cosmetic.Accessory].transform.parent = NewBodyPart.transform;
						}
					}
				}
			}

			if (this.BloodPoolSpawner.BloodParent == null)
			{
				this.BloodPoolSpawner.Start();
			}
				
			Debug.Log("BloodPoolSpawner.transform.position is: " + BloodPoolSpawner.transform.position);

			Debug.Log("Student.StudentManager.SEStairs.bounds is: " + Student.StudentManager.SEStairs.bounds);

			Debug.Log("Student.StudentManager.SEStairs.bounds.Contains(BloodPoolSpawner.transform.position) is: " +
				Student.StudentManager.SEStairs.bounds.Contains(BloodPoolSpawner.transform.position));

			if (!Student.StudentManager.NEStairs.bounds.Contains(BloodPoolSpawner.transform.position) &&
				!Student.StudentManager.NWStairs.bounds.Contains(BloodPoolSpawner.transform.position) &&
				!Student.StudentManager.SEStairs.bounds.Contains(BloodPoolSpawner.transform.position) &&
				!Student.StudentManager.SWStairs.bounds.Contains(BloodPoolSpawner.transform.position))
			{
				this.BloodPoolSpawner.SpawnBigPool();
			}

			this.Police.PartsIcon.gameObject.SetActive(true);
			this.Police.BodyParts += 6;
			this.Yandere.NearBodies--;
			this.Police.Corpses--;

            //Called as a result of Dismember();
			this.gameObject.SetActive(false);

			this.Dismembered = true;
		}
	}

	public void Remove()
	{
        Debug.Log("The Remove() function has been called on " + this.Student.Name + "'s RagdollScript.");

        this.Student.Removed = true;

		this.BloodPoolSpawner.enabled = false;

		if (this.AddingToCount)
		{
			this.Yandere.NearBodies--;
		}

		if (this.Poisoned)
		{
			this.Police.PoisonScene = false;
		}

		this.gameObject.SetActive(false);
	}

	public void DestroyRigidbodies()
	{
		this.BloodPoolSpawner.gameObject.SetActive(false);

		// [af] Converted while loop to for loop.
		for (int ID = 0; ID < this.AllRigidbodies.Length; ID++)
		{
			if (this.AllRigidbodies[ID].gameObject.GetComponent<CharacterJoint>() != null)
			{
				Destroy(this.AllRigidbodies[ID].gameObject.GetComponent<CharacterJoint>());
			}

			Destroy(this.AllRigidbodies[ID]);
			this.AllColliders[ID].enabled = false;
		}

		this.Prompt.Hide();
		this.Prompt.enabled = false;
		enabled = false;
	}

    public void DisableRigidbodies()
    {
        for (int ID = 0; ID < this.AllRigidbodies.Length; ID++)
        {
            this.AllRigidbodies[ID].isKinematic = true;
            this.AllColliders[ID].enabled = false;
        }
    }

    public void EnableRigidbodies()
    {
        for (int ID = 0; ID < this.AllRigidbodies.Length; ID++)
        {
            this.AllRigidbodies[ID].isKinematic = false;
            this.AllColliders[ID].enabled = true;

            this.AllRigidbodies[ID].useGravity = !this.Yandere.StudentManager.NoGravity;
        }
    }
}