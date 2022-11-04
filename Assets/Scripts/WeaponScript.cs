using UnityEngine;

public enum WeaponType
{
	Knife = 1,
	Katana,
	Bat,
	Saw,
	Syringe,
	Weight,
    Garrote
}

public class WeaponScript : MonoBehaviour
{
	public ParticleSystem[] ShortBloodSpray;
	public ParticleSystem[] BloodSpray;
	public OutlineScript[] Outline;
	public float[] SoundTime;

	public IncineratorScript Incinerator;
	public StudentScript Returner;
	public YandereScript Yandere;
	public PromptScript Prompt;

	public Transform Origin;
	public Transform Parent;

	public AudioClip[] Clips;
	public AudioClip[] Clips2;
	public AudioClip[] Clips3;

	public AudioClip DismemberClip;
	public AudioClip EquipClip;

	public ParticleSystem FireEffect;
    public GameObject WeaponTrail;
	public GameObject ExtraBlade;
	public AudioSource FireAudio;
	public Rigidbody MyRigidbody;
    public AudioSource MyAudio;
    public Collider MyCollider;
	public Renderer MyRenderer;
	public Transform Blade;
	public Projector Blood;

	public Vector3 StartingPosition;
	public Vector3 StartingRotation;

    public bool UnequipImmediately = false;
    public bool AlreadyExamined = false;
    public bool DelinquentOwned = false;
    public bool DisableCollider = false;
    public bool DoNotDisable = false;
    public bool Dismembering = false;
	public bool MurderWeapon = false;
	public bool WeaponEffect = false;
	public bool Concealable = false;
	public bool Suspicious = false;
	public bool Dangerous = false;
	public bool Misplaced = false;
	public bool Evidence = false;
	public bool StartLow = false;
	public bool Flaming = false;
	public bool Bloody = false;
	public bool Dumped = false;
	public bool Heated = false;
	public bool Rotate = false;
    public bool Blunt = false;
	public bool Metal = false;
	public bool Flip = false;
	public bool Spin = false;

	public Color EvidenceColor;
	public Color OriginalColor;

	public float OriginalOffset = 0.0f;
	public float KinematicTimer = 0.0f;
	public float DumpTimer = 0.0f;
	public float Rotation = 0.0f;
	public float Speed = 0.0f;

	public string SpriteName;
	public string Name;

	public int DismemberPhase = 0;
	public int FingerprintID = 0;
	public int GlobalID = 0;
	public int WeaponID = 0;
	public int AnimID = 0;
	public WeaponType Type = WeaponType.Knife;

	public bool[] Victims;

	private AudioClip OriginalClip;

	private int ID = 0;

	void Start()
	{
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();

		this.StartingPosition = transform.position;
		this.StartingRotation = transform.eulerAngles;

		Physics.IgnoreCollision(this.Yandere.GetComponent<Collider>(), this.MyCollider);

		this.OriginalColor = this.Outline[0].color;

		if (this.StartLow)
		{
			this.OriginalOffset = this.Prompt.OffsetY[3];
			this.Prompt.OffsetY[3] = 0.20f;
		}

		if (this.DisableCollider)
		{
			this.MyCollider.enabled = false;
		}

		this.MyAudio = this.GetComponent<AudioSource>();

		if (this.MyAudio != null)
		{
			this.OriginalClip = MyAudio.clip;
		}

		MyRigidbody = this.GetComponent<Rigidbody>();
		MyRigidbody.isKinematic = true;

		Transform OriginParent = GameObject.Find("WeaponOriginParent").transform;
		Origin = Instantiate(this.Prompt.Yandere.StudentManager.EmptyObject, transform.position, Quaternion.identity).transform;
		Origin.parent = OriginParent;
	}
	
	// [af] Used with weapon animation names.
	public string GetTypePrefix()
	{
		if (this.Type == WeaponType.Knife)
		{
			return "knife";
		}
		else if (this.Type == WeaponType.Katana)
		{
			return "katana";
		}
		else if (this.Type == WeaponType.Bat)
		{
			return "bat";
		}
		else if (this.Type == WeaponType.Saw)
		{
			return "saw";
		}
		else if (this.Type == WeaponType.Syringe)
		{
			return "syringe";
		}
		else if (this.Type == WeaponType.Weight)
		{
			return "weight";
		}
        else if (this.Type == WeaponType.Garrote)
        {
            return "syringe";
        }
        else
		{
			Debug.LogError("Weapon type \"" + this.Type.ToString() + "\" not implemented.");
			return string.Empty;
		}
	}
	
	// [af] Used with weapon sounds in attack manager.
	public AudioClip GetClip(float sanity, bool stealth)
	{
		AudioClip[] selectedClips;

		if (this.Clips2.Length == 0)
		{
			selectedClips = this.Clips;
		}
		else
		{
			int clipSet = Random.Range(2, 4);

			// [af] Replaced if/else statement with ternary expression.
			selectedClips = (clipSet == 2) ? this.Clips2 : this.Clips3;
		}

		if (stealth)
		{
			return selectedClips[0];
		}
		else
		{
			if (sanity > (2.0f / 3.0f))
			{
				return selectedClips[1];
			}
			else if (sanity > (1.0f / 3.0f))
			{
				return selectedClips[2];
			}
			else
			{
				return selectedClips[3];
			}
		}
	}

	void Update()
	{
		if (this.WeaponID == 16)
		{
			if (this.Yandere.EquippedWeapon == this)
			{
				if (Input.GetButtonDown(InputNames.Xbox_RB))
				{
                    if (this.ExtraBlade != null)
                    {
					    this.ExtraBlade.SetActive(!this.ExtraBlade.activeInHierarchy);
                    }
                }
			}
		}

		if (this.Dismembering)
		{
			AudioSource audioSource = this.GetComponent<AudioSource>();

			if (this.DismemberPhase < 4)
			{
				if (audioSource.time > 0.75f)
				{
					if (this.Speed < 36.0f)
					{
						this.Speed += Time.deltaTime + 10.0f;
					}

					this.Rotation += this.Speed;
					this.Blade.localEulerAngles = new Vector3(
						this.Rotation,
						this.Blade.localEulerAngles.y,
						this.Blade.localEulerAngles.z);
				}

				if (audioSource.time > this.SoundTime[this.DismemberPhase])
				{
					this.Yandere.Sanity -= 5.0f * this.Yandere.Numbness;
					this.Yandere.Bloodiness += 25.0f;

					this.ShortBloodSpray[0].Play();
					this.ShortBloodSpray[1].Play();
					this.Blood.enabled = true;
					this.MurderWeapon = true;
					this.DismemberPhase++;
				}
			}
			else
			{
				this.Rotation = Mathf.Lerp(this.Rotation, 0.0f, Time.deltaTime * 2.0f);

				this.Blade.localEulerAngles = new Vector3(
					this.Rotation,
					this.Blade.localEulerAngles.y,
					this.Blade.localEulerAngles.z);

				if (!audioSource.isPlaying)
				{
					audioSource.clip = this.OriginalClip;
					this.Yandere.StainWeapon();
					this.Dismembering = false;
					this.DismemberPhase = 0;
					this.Rotation = 0.0f;
					this.Speed = 0.0f;
				}
			}
		}
		else
		{
			if (this.Yandere.EquippedWeapon == this)
			{
				if (this.Yandere.AttackManager.IsAttacking())
				{
					if (this.Type == WeaponType.Knife)
					{
						// [af] Replaced if/else statement with assignment and ternary expression.
						this.transform.localEulerAngles = new Vector3(
							this.transform.localEulerAngles.x,
							Mathf.Lerp(this.transform.localEulerAngles.y, this.Flip ? 180.0f : 0.0f, Time.deltaTime * 10.0f),
							this.transform.localEulerAngles.z);
					}
					else if (this.Type == WeaponType.Saw)
					{
						if (this.Spin)
						{
							this.Blade.transform.localEulerAngles = new Vector3(
								this.Blade.transform.localEulerAngles.x + (Time.deltaTime * 360.0f),
								this.Blade.transform.localEulerAngles.y,
								this.Blade.transform.localEulerAngles.z);
						}
					}
				}
			}
			else
			{
				if (!MyRigidbody.isKinematic)
				{
					this.KinematicTimer = Mathf.MoveTowards(this.KinematicTimer, 5.0f, Time.deltaTime);

					if (this.KinematicTimer == 5.0f)
					{
						MyRigidbody.isKinematic = true;
						this.KinematicTimer = 0.0f;
					}

					//Gardening Club Zone
					if ((this.transform.position.x > -71.0f) &&
						(this.transform.position.x < -61.0f) &&
						(this.transform.position.z > -37.50f) &&
						(this.transform.position.z < -27.50f))
					{
						this.transform.position = new Vector3(-63, 1.0f, -26.5f);
						this.KinematicTimer = 0.0f;
					}

                    //Cherry Tree Zone
					if ((this.transform.position.x > -21.0f) &&
						(this.transform.position.x < 21.0f) &&
						(this.transform.position.z > 100.0f) &&
						(this.transform.position.z < 135.0f))
					{
						this.transform.position = new Vector3(0.0f, 1.0f, 100f);
						this.KinematicTimer = 0.0f;
					}

					//Pool
					if ((this.transform.position.x > -46.0f) &&
						(this.transform.position.x < -18.0f) &&
						(this.transform.position.z > 66.0f) &&
						(this.transform.position.z < 78.0f))
					{
						this.transform.position = new Vector3(-16f, 5.0f, 72);
						this.KinematicTimer = 0.0f;
					}
				}
			}
		}

		if (Rotate)
		{
			this.transform.Rotate(Vector3.forward * Time.deltaTime * 100);
		}
	}

	void LateUpdate()
	{
		if (this.Prompt.Circle[3].fillAmount == 0.0f)
		{
            //Debug.Log("Yandere-chan just picked up a weapon.");

			if (this.WeaponID == 6)
			{
				//If we're waiting for Yandere-chan to pick up a screwdriver...
				if (SchemeGlobals.GetSchemeStage(4) == 1)
				{
					SchemeGlobals.SetSchemeStage(4, 2);
					this.Yandere.PauseScreen.Schemes.UpdateInstructions();
				}
			}

			this.Prompt.Circle[3].fillAmount = 1.0f;

			if (this.Prompt.Suspicious)
			{
				this.Yandere.TheftTimer = .1f;
			}

			if (this.Dangerous || this.Suspicious)
			{
				this.Yandere.WeaponTimer = .1f;
			}

			if (!this.Yandere.Gloved)
			{
				this.FingerprintID = 100;
			}

			// [af] Converted while loop to for loop.
			for (this.ID = 0; this.ID < this.Outline.Length; this.ID++)
			{
				this.Outline[this.ID].color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
			}

			this.transform.parent = this.Yandere.ItemParent;
			this.transform.localPosition = Vector3.zero;

			if (this.Type == WeaponType.Bat)
			{
				this.transform.localEulerAngles = new Vector3(0, 0, -90);
			}
			else
			{
				this.transform.localEulerAngles = Vector3.zero;
			}

			//Debug.Log("A weapon just deactivated its collider.");
			this.MyCollider.enabled = false;
			MyRigidbody.constraints = RigidbodyConstraints.FreezeAll;

			if (this.Yandere.Equipped == 3)
			{
                if (this.Yandere.Weapon[3] != null)
                {
				    this.Yandere.Weapon[3].Drop();
                }
            }

			if (this.Yandere.PickUp != null)
			{
				this.Yandere.PickUp.Drop();
			}

			if (this.Yandere.Dragging)
			{
				this.Yandere.Ragdoll.GetComponent<RagdollScript>().StopDragging();
			}

			if (this.Yandere.Carrying)
			{
				this.Yandere.StopCarrying();
			}

			if (this.Concealable)
			{
				if (this.Yandere.Weapon[1] == null)
				{
                    //Debug.Log("Yandere-chan did not have a weapon in slot 1.");

                    if (this.Yandere.Weapon[2] != null)
					{
						this.Yandere.Weapon[2].gameObject.SetActive(false);
					}

					this.Yandere.Equipped = 1;
					this.Yandere.EquippedWeapon = this;
                    this.Yandere.WeaponManager.SetEquippedWeapon1(this);
                }
				else if (this.Yandere.Weapon[2] == null)
				{
                    //Debug.Log("Yandere-chan had a weapon in slot 1, but slot 2 was empty.");

                    if (this.Yandere.Weapon[1] != null)
					{
                        if (!this.DoNotDisable)
                        {
                            this.Yandere.Weapon[1].gameObject.SetActive(false);
                        }

                        this.DoNotDisable = false;
                    }

                    this.Yandere.Equipped = 2;
					this.Yandere.EquippedWeapon = this;
                    this.Yandere.WeaponManager.SetEquippedWeapon2(this);
                }
				else
				{
					if (this.Yandere.Weapon[2].gameObject.activeInHierarchy)
					{
						this.Yandere.Weapon[2].Drop();

						this.Yandere.Equipped = 2;
						this.Yandere.EquippedWeapon = this;
                        this.Yandere.WeaponManager.SetEquippedWeapon2(this);
                    }
					else
					{
						this.Yandere.Weapon[1].Drop();

						this.Yandere.Equipped = 1;
						this.Yandere.EquippedWeapon = this;
                        this.Yandere.WeaponManager.SetEquippedWeapon1(this);
                    }
				}
			}
			else
			{
				if (this.Yandere.Weapon[1] != null)
				{
                    this.Yandere.Weapon[1].gameObject.SetActive(false);
				}

				if (this.Yandere.Weapon[2] != null)
				{
					this.Yandere.Weapon[2].gameObject.SetActive(false);
				}

				this.Yandere.Equipped = 3;
				this.Yandere.EquippedWeapon = this;
                this.Yandere.WeaponManager.SetEquippedWeapon3(this);
            }

			this.Yandere.StudentManager.UpdateStudents();

			this.Prompt.Hide();
			this.Prompt.enabled = false;

			this.Yandere.NearestPrompt = null;

            if (this.WeaponID == 9  || this.WeaponID == 10 || this.WeaponID == 12 ||
                this.WeaponID == 14 || this.WeaponID == 16 || this.WeaponID == 22 ||
                this.WeaponID == 25)
			{
				this.SuspicionCheck();
			}

			if (this.Yandere.EquippedWeapon.Suspicious)
			{
				if (!this.Yandere.WeaponWarning)
				{
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Armed);

					this.Yandere.WeaponWarning = true;
				}
			}
			else
			{
				this.Yandere.WeaponWarning = false;
			}

			this.Yandere.WeaponMenu.UpdateSprites();
			this.Yandere.WeaponManager.UpdateLabels();

			if (this.Evidence)
			{
				this.Yandere.Police.BloodyWeapons--;
			}

			if (this.WeaponID == 11)
			{
				this.Yandere.IdleAnim = "CyborgNinja_Idle_Armed";
				this.Yandere.WalkAnim = "CyborgNinja_Walk_Armed";
				this.Yandere.RunAnim = "CyborgNinja_Run_Armed";
			}

			if (this.WeaponID == 26)
			{
				this.WeaponTrail.SetActive(true);
			}

			this.KinematicTimer = 0.0f;

			AudioSource.PlayClipAtPoint(this.EquipClip, this.Yandere.MainCamera.transform.position);

            if (this.UnequipImmediately)
            {
                this.UnequipImmediately = false;
                this.Yandere.Unequip();
            }
		}

		if (this.Yandere.EquippedWeapon == this)
		{
			if (this.Yandere.Armed)
			{
				this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

				if (!this.Yandere.Struggling)
				{
					if (this.Yandere.CanMove)
					{
						this.transform.localPosition = Vector3.zero;

						if (this.Type == WeaponType.Bat)
						{
							this.transform.localEulerAngles = new Vector3(0, 0, -90);
						}
						else
						{
							this.transform.localEulerAngles = Vector3.zero;
						}
					}
				}
				else
				{
					this.transform.localPosition = new Vector3(-0.010f, 0.0050f, -0.010f);
				}
			}
		}

		if (this.Dumped)
		{
			this.DumpTimer += Time.deltaTime;

			if (this.DumpTimer > 1.0f)
			{
				this.Yandere.Incinerator.MurderWeapons++;

				Destroy(this.gameObject);
			}
		}

		if (this.transform.parent == this.Yandere.ItemParent)
		{
			if (this.Concealable)
			{
				if ((this.Yandere.Weapon[1] != this) && (this.Yandere.Weapon[2] != this))
				{
					this.Drop();
				}
			}
		}
	}

	public void Drop()
	{
		if (this.WeaponID == 6)
		{
			//If Yandere-chan needed to be holding a screwdriver...
			if (SchemeGlobals.GetSchemeStage(4) == 2)
			{
				SchemeGlobals.SetSchemeStage(4, 1);
				this.Yandere.PauseScreen.Schemes.UpdateInstructions();
			}
		}

		Debug.Log("A " + this.gameObject.name + " has been dropped.");

		if (this.WeaponID == 11)
		{
			this.Yandere.IdleAnim = "CyborgNinja_Idle_Unarmed";
			this.Yandere.WalkAnim = this.Yandere.OriginalWalkAnim;
			this.Yandere.RunAnim = "CyborgNinja_Run_Unarmed";
		}

		if (this.StartLow)
		{
			this.Prompt.OffsetY[3] = this.OriginalOffset;
		}

        if (this.Yandere.Weapon[1] == this)
        {
            this.Yandere.WeaponManager.YandereWeapon1 = -1;
        }
        else if (this.Yandere.Weapon[2] == this)
        {
            this.Yandere.WeaponManager.YandereWeapon2 = -1;
        }
        else if (this.Yandere.Weapon[3] == this)
        {
            this.Yandere.WeaponManager.YandereWeapon3 = -1;
        }

        if (this.Yandere.EquippedWeapon == this)
		{
			this.Yandere.EquippedWeapon = null;
			this.Yandere.Equipped = 0;
			this.Yandere.StudentManager.UpdateStudents();
		}

		// [af] Added "gameObject" for C# compatibility.
		this.gameObject.SetActive(true);

		this.transform.parent = null;

		MyRigidbody.constraints = RigidbodyConstraints.None;
		MyRigidbody.isKinematic = false;
		MyRigidbody.useGravity = true;

		MyCollider.isTrigger = false;

		if (this.Dumped)
		{
			this.transform.position = this.Incinerator.DumpPoint.position;
		}
		else
		{
			this.Prompt.enabled = true;
			this.MyCollider.enabled = true;

			if (this.Yandere.GetComponent<Collider>().enabled)
			{
				Physics.IgnoreCollision(this.Yandere.GetComponent<Collider>(), this.MyCollider);
			}
		}

		if (this.Evidence)
		{
			this.Yandere.Police.BloodyWeapons++;
		}

		//if (this.Suspicious)
		//{
		if (Vector3.Distance(this.StartingPosition, this.transform.position) > 5 &&
			Vector3.Distance(this.transform.position, this.Yandere.StudentManager.WeaponBoxSpot.parent.position) > 1)
		{
			if (!this.Misplaced)
			{
				this.Prompt.Yandere.WeaponManager.MisplacedWeapons++;
				this.Misplaced = true;
			}
		}
		else
		{
			if (this.Misplaced)
			{
				this.Prompt.Yandere.WeaponManager.MisplacedWeapons--;
				this.Misplaced = false;
			}
		}
		//}

		// [af] Converted while loop to for loop.
		for (this.ID = 0; this.ID < this.Outline.Length; this.ID++)
		{
			// [af] Replaced if/else statement with ternary expression.
			this.Outline[this.ID].color = this.Evidence ? this.EvidenceColor : this.OriginalColor;
		}

		if (this.transform.position.y > 1000.0f)
		{
			this.transform.position = new Vector3(12.0f, 0.0f, 28.0f);
		}

		if (this.WeaponID == 26)
		{
			this.transform.parent = this.Parent;
			this.transform.localEulerAngles = Vector3.zero;
			this.transform.localPosition = Vector3.zero;

			MyRigidbody.isKinematic = true;

			this.WeaponTrail.SetActive(false);
		}
	}

	public void UpdateLabel()
	{
		if (this != null)
		{
			// [af] Added "gameObject" for C# compatibility.
			if (this.gameObject.activeInHierarchy)
			{
				if ((this.Yandere.Weapon[1] != null) && (this.Yandere.Weapon[2] != null) &&
					this.Concealable)
				{
					if (this.Prompt.Label[3] != null)
					{
						if (!this.Yandere.Armed || (this.Yandere.Equipped == 3))
						{
							this.Prompt.Label[3].text = "     " + "Swap " +
								this.Yandere.Weapon[1].Name + " for " + this.Name;
						}
						else
						{
							this.Prompt.Label[3].text = "     " + "Swap " +
								this.Yandere.EquippedWeapon.Name + " for " + this.Name;
						}
					}
				}
				else
				{
					if (this.Prompt.Label[3] != null)
					{
						this.Prompt.Label[3].text = "     " + this.Name;
					}
				}
			}
		}
	}

	public GameObject HeartBurst;

	public void Effect()
	{
		if (this.WeaponID == 7)
		{
			this.BloodSpray[0].Play();
			this.BloodSpray[1].Play();
		}
		// Ritual Knife.
		else if (this.WeaponID == 8)
		{
			this.gameObject.GetComponent<ParticleSystem>().Play();
			this.MyAudio.clip = this.OriginalClip;
			this.MyAudio.Play();
		}
		else if ((this.WeaponID == 2) || (this.WeaponID == 9) ||
			(this.WeaponID == 10) || (this.WeaponID == 12) || (this.WeaponID == 13))
		{
			this.MyAudio.Play();
		}
		else if (this.WeaponID == 14)
		{
			Instantiate(this.HeartBurst, this.Yandere.TargetStudent.Head.position, Quaternion.identity);
			this.MyAudio.Play();
		}
	}

	public void Dismember()
	{
        this.MyAudio.clip = this.DismemberClip;
        this.MyAudio.Play();

		this.Dismembering = true;
	}

	public void SuspicionCheck()
	{
		Debug.Log("Suspicion Check!");

		if (this.WeaponID ==  9 && this.Yandere.Club == ClubType.Sports ||
			this.WeaponID == 10 && this.Yandere.Club == ClubType.Gardening ||
			this.WeaponID == 12 && this.Yandere.Club == ClubType.Sports ||
            this.WeaponID == 14 && this.Yandere.Club == ClubType.Drama ||
            this.WeaponID == 16 && this.Yandere.Club == ClubType.Drama ||
            this.WeaponID == 22 && this.Yandere.Club == ClubType.Drama ||
            this.WeaponID == 25 && this.Yandere.Club == ClubType.LightMusic)
		{
			this.Suspicious = false;
		}
		else
		{
			this.Suspicious = true;
		}

		if (this.Bloody)
		{
			this.Suspicious = true;
		}
	}
}