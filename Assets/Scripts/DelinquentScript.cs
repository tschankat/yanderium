using UnityEngine;

public class DelinquentScript : MonoBehaviour
{
	private Quaternion targetRotation;

	public DelinquentManagerScript DelinquentManager;
	public YandereScript Yandere;

	public Quaternion OriginalRotation;
	public Vector3 LookAtTarget;
	public GameObject Character;
	public SkinnedMeshRenderer MyRenderer; // [af] Replaced Renderer with SkinnedMeshRenderer.
	public GameObject MyWeapon;
	public GameObject Jukebox;
	public Mesh LongSkirt;
	public Camera Eyes;

	public Transform RightBreast;
	public Transform LeftBreast;
	public Transform Default;
	public Transform Weapon;
	public Transform Neck;
	public Transform Head;

	public Plane[] Planes;

	public string CooldownAnim = AnimNames.FemaleIdleShort;
	public string ThreatenAnim = AnimNames.FemaleThreaten;
	public string SurpriseAnim = AnimNames.FemaleSurprise;
	public string ShoveAnim = AnimNames.FemaleShoveB;
	public string SwingAnim = AnimNames.FemaleSwingA;
	public string RunAnim = AnimNames.FemaleSpring;
	public string IdleAnim = string.Empty;
	public string Prefix = "f02_";

	public bool ExpressedSurprise = false;
	public bool LookAtPlayer = false;
	public bool Threatening = false;
	public bool Attacking = false;
	public bool HeadStill = false;
	public bool Cooldown = false;
	public bool Shoving = false;
	public bool Rapping = false;
	public bool Run = false;

	public float DistanceToPlayer = 0.0f;
	public float RunSpeed = 0.0f;
	public float BustSize = 0.0f;
	public float Rotation = 0.0f;
	public float Timer = 0.0f;

	public int AudioPhase = 1;
	public int Spaces = 0;

	public AudioClip[] ProximityClips;
	public AudioClip[] SurrenderClips;
	public AudioClip[] SurpriseClips;
	public AudioClip[] ThreatenClips;
	public AudioClip[] AggroClips;
	public AudioClip[] ShoveClips;
	public AudioClip[] CaseClips;

	public AudioClip SurpriseClip;
	public AudioClip AttackClip;

	public AudioClip Crumple;
	public AudioClip Strike;

	public GameObject DefaultHair;
	public GameObject Mask;

	public GameObject EasterHair;
	public GameObject Bandanas;

	public Renderer HairRenderer;
	public Color HairColor;

	public Texture BlondThugHair;

	public Transform TimePortal;
	public bool Suck;

	void Start()
	{
		this.EasterHair.SetActive(false);
		this.Bandanas.SetActive(false);

		this.OriginalRotation = this.transform.rotation;
		this.LookAtTarget = this.Default.position;

		if (this.Weapon != null)
		{
			this.Weapon.localPosition = new Vector3(
				this.Weapon.localPosition.x,
				-0.145f,
				this.Weapon.localPosition.z);

			this.Rotation = 90.0f;

			this.Weapon.localEulerAngles = new Vector3(
				this.Rotation,
				this.Weapon.localEulerAngles.y,
				this.Weapon.localEulerAngles.z);
		}
	}

	void Update()
	{
		this.DistanceToPlayer = Vector3.Distance(this.transform.position,
			this.Yandere.transform.position);

		AudioSource audioSource = this.GetComponent<AudioSource>();

		if (this.DistanceToPlayer < 7.0f)
		{
			this.Planes = GeometryUtility.CalculateFrustumPlanes(this.Eyes);

			if (GeometryUtility.TestPlanesAABB(this.Planes, this.Yandere.GetComponent<Collider>().bounds))
			{
				RaycastHit hit;

				if (Physics.Linecast(this.Eyes.transform.position,
					this.Yandere.transform.position + Vector3.up, out hit))
				{
					if (hit.collider.gameObject == this.Yandere.gameObject)
					{
						this.LookAtPlayer = true;

						if (this.Yandere.Armed)
						{
							if (!this.Threatening)
							{
								audioSource.clip = this.SurpriseClips[Random.Range(0, this.SurpriseClips.Length)];
								audioSource.pitch = Random.Range(0.90f, 1.10f);
								audioSource.Play();
							}

							this.Threatening = true;

							if (this.Cooldown)
							{
								this.Cooldown = false;
								this.Timer = 0.0f;
							}
						}
						else
						{
							if (this.Yandere.CorpseWarning)
							{
								if (!this.Threatening)
								{
									audioSource.clip = this.SurpriseClips[Random.Range(0, this.SurpriseClips.Length)];
									audioSource.pitch = Random.Range(0.90f, 1.10f);
									audioSource.Play();
								}

								this.Threatening = true;

								if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
								{
									this.DelinquentManager.Attacker = this;
									this.Run = true;
								}

								this.Yandere.Chased = true;
							}
							else
							{
								if (!this.Threatening)
								{
									if (this.DelinquentManager.SpeechTimer == 0.0f)
									{
										// [af] Replaced if/else statement with ternary expression.
										audioSource.clip = (this.Yandere.Container == null) ?
											this.ProximityClips[Random.Range(0, this.ProximityClips.Length)] :
											this.CaseClips[Random.Range(0, this.CaseClips.Length)];

										audioSource.Play();
										this.DelinquentManager.SpeechTimer = 10.0f;
									}
								}
							}

							this.LookAtPlayer = true;
						}
					}
					else
					{
						this.LookAtPlayer = false;
					}
				}
			}
			else
			{
				this.LookAtPlayer = false;
			}
		}

		if (!this.Threatening)
		{
			if (this.Shoving)
			{
				this.targetRotation = Quaternion.LookRotation(
					this.Yandere.transform.position - this.transform.position);
				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);

				this.targetRotation = Quaternion.LookRotation(
					this.transform.position - this.Yandere.transform.position);
				this.Yandere.transform.rotation = Quaternion.Slerp(
					this.Yandere.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);

				if (this.Character.GetComponent<Animation>()[this.ShoveAnim].time >=
					this.Character.GetComponent<Animation>()[this.ShoveAnim].length)
				{
					this.LookAtTarget = this.Neck.position + this.Neck.forward;

					this.Character.GetComponent<Animation>().CrossFade(this.IdleAnim, 1.0f);
					this.Shoving = false;
				}

				if (this.Weapon != null)
				{
					this.Weapon.localPosition = new Vector3(
						this.Weapon.localPosition.x,
						Mathf.Lerp(this.Weapon.localPosition.y, 0.0f, Time.deltaTime * 10.0f),
						this.Weapon.localPosition.z);

					this.Rotation = Mathf.Lerp(this.Rotation, 0.0f, Time.deltaTime * 10.0f);

					this.Weapon.localEulerAngles = new Vector3(
						this.Rotation,
						this.Weapon.localEulerAngles.y,
						this.Weapon.localEulerAngles.z);
				}
			}
			else
			{
				this.Shove();

				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.OriginalRotation, Time.deltaTime);

				if (this.Weapon != null)
				{
					this.Weapon.localPosition = new Vector3(
						this.Weapon.localPosition.x,
						Mathf.Lerp(this.Weapon.localPosition.y, -0.145f, Time.deltaTime * 10.0f),
						this.Weapon.localPosition.z);

					this.Rotation = Mathf.Lerp(this.Rotation, 90.0f, Time.deltaTime * 10.0f);

					this.Weapon.localEulerAngles = new Vector3(
						this.Rotation,
						this.Weapon.localEulerAngles.y,
						this.Weapon.localEulerAngles.z);
				}
			}
		}
		else
		{
			this.targetRotation = Quaternion.LookRotation(
				this.Yandere.transform.position - this.transform.position);
			this.transform.rotation = Quaternion.Slerp(
				this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);

			if (this.Weapon != null)
			{
				this.Weapon.localPosition = new Vector3(
					this.Weapon.localPosition.x,
					Mathf.Lerp(this.Weapon.localPosition.y, 0.0f, Time.deltaTime * 10.0f),
					this.Weapon.localPosition.z);

				this.Rotation = Mathf.Lerp(this.Rotation, 0.0f, Time.deltaTime * 10.0f);

				this.Weapon.localEulerAngles = new Vector3(
					this.Rotation,
					this.Weapon.localEulerAngles.y,
					this.Weapon.localEulerAngles.z);
			}

			if (this.DistanceToPlayer < 1.0f)
			{
				if (this.Yandere.Armed || this.Run)
				{
					if (!this.Yandere.Attacked)
					{
						if (this.Yandere.CanMove)
						{
							if (!this.Yandere.Chased && this.Yandere.Chasers == 0 ||
								this.Yandere.Chased && this.DelinquentManager.Attacker == this)
							{
								AudioSource delinquentAudioSource =
									this.DelinquentManager.GetComponent<AudioSource>();

								if (!delinquentAudioSource.isPlaying)
								{
									delinquentAudioSource.clip = this.AttackClip;
									delinquentAudioSource.Play();

									this.DelinquentManager.enabled = false;
								}

								if (this.Yandere.Laughing)
								{
									this.Yandere.StopLaughing();
								}

								if (this.Yandere.Aiming)
								{
									this.Yandere.StopAiming();
								}

								this.Character.GetComponent<Animation>().CrossFade(this.SwingAnim);
								this.MyWeapon.SetActive(true);
								this.Attacking = true;

								this.Yandere.Character.GetComponent<Animation>().CrossFade(AnimNames.FemaleSwingB);
								this.Yandere.RPGCamera.enabled = false;
								this.Yandere.CanMove = false;
								this.Yandere.Attacked = true;
								this.Yandere.EmptyHands();
							}
						}
					}
					else
					{
						if (this.Attacking)
						{
							if (this.AudioPhase == 1)
							{
								if (this.Character.GetComponent<Animation>()[this.SwingAnim].time >=
									(this.Character.GetComponent<Animation>()[this.SwingAnim].length * 0.30f))
								{
									this.Jukebox.SetActive(false);
									this.AudioPhase++;

									audioSource.pitch = 1.0f;
									audioSource.clip = this.Strike;
									audioSource.Play();
								}
							}
							else if (this.AudioPhase == 2)
							{
								if (this.Character.GetComponent<Animation>()[this.SwingAnim].time >=
									(this.Character.GetComponent<Animation>()[this.SwingAnim].length * 0.85f))
								{
									this.AudioPhase++;

									audioSource.pitch = 1.0f;
									audioSource.clip = this.Crumple;
									audioSource.Play();
								}
							}

							this.targetRotation = Quaternion.LookRotation(
								this.transform.position - this.Yandere.transform.position);
							this.Yandere.transform.rotation = Quaternion.Slerp(
								this.Yandere.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);
						}
					}
				}
				else
				{
					this.Shove();
				}
			}
			else
			{
				if (!this.ExpressedSurprise)
				{
					this.Character.GetComponent<Animation>().CrossFade(this.SurpriseAnim);

					if (this.Character.GetComponent<Animation>()[this.SurpriseAnim].time >=
						this.Character.GetComponent<Animation>()[this.SurpriseAnim].length)
					{
						this.ExpressedSurprise = true;
					}
				}
				else
				{
					if (this.Run)
					{
						if (this.DistanceToPlayer > 1.0f)
						{
							this.transform.position = Vector3.MoveTowards(
								this.transform.position,
								this.Yandere.transform.position,
								Time.deltaTime * this.RunSpeed);
							this.Character.GetComponent<Animation>().CrossFade(this.RunAnim);
							this.RunSpeed += Time.deltaTime;
						}
					}
					else
					{
						if (!this.Cooldown)
						{
							this.Character.GetComponent<Animation>().CrossFade(this.ThreatenAnim);

							if (!this.Yandere.Armed)
							{
								this.Timer += Time.deltaTime;

								if (this.Timer > 2.50f)
								{
									this.Cooldown = true;

									if (!this.DelinquentManager.GetComponent<AudioSource>().isPlaying)
									{
										this.DelinquentManager.SpeechTimer = Time.deltaTime;
									}
								}
							}
							else
							{
								this.Timer = 0.0f;

								if (this.DelinquentManager.SpeechTimer == 0.0f)
								{
									this.DelinquentManager.GetComponent<AudioSource>().clip =
										this.ThreatenClips[Random.Range(0, this.ThreatenClips.Length)];
									this.DelinquentManager.GetComponent<AudioSource>().Play();
									this.DelinquentManager.SpeechTimer = 10.0f;
								}
							}
						}
						else
						{
							if (this.DelinquentManager.SpeechTimer == 0.0f)
							{
								AudioSource delinquentAudioSource =
									this.DelinquentManager.GetComponent<AudioSource>();

								if (!delinquentAudioSource.isPlaying)
								{
									delinquentAudioSource.clip =
										this.SurrenderClips[Random.Range(0, this.SurrenderClips.Length)];
									delinquentAudioSource.Play();
									this.DelinquentManager.SpeechTimer = 5.0f;
								}
							}

							this.Character.GetComponent<Animation>().CrossFade(this.CooldownAnim, 2.50f);

							this.Timer += Time.deltaTime;

							if (this.Timer > 5.0f)
							{
								this.Character.GetComponent<Animation>().CrossFade(this.IdleAnim, 1.0f);
								this.ExpressedSurprise = false;
								this.Threatening = false;
								this.Cooldown = false;
								this.Timer = 0.0f;
							}

							this.Shove();
						}
					}
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.V))
		{
			if (this.LongSkirt != null)
			{
				this.MyRenderer.sharedMesh = this.LongSkirt;
			}
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (Vector3.Distance(this.Yandere.transform.position,
				this.DelinquentManager.transform.position) < 10.0f)
			{
				this.Spaces++;

				if (this.Spaces == 9)
				{
					if (this.HairRenderer == null)
					{
						this.DefaultHair.SetActive(false);
						this.EasterHair.SetActive(true);
					
						this.EasterHair.GetComponent<Renderer>().material.mainTexture = BlondThugHair;
					}
				}
				else if (this.Spaces == 10)
				{
					this.Rapping = true;
					this.MyWeapon.SetActive(false);
					this.IdleAnim = this.Prefix + "gruntIdle_00";

					Animation charAnimation = this.Character.GetComponent<Animation>();
					charAnimation.CrossFade(this.IdleAnim);
					charAnimation[this.IdleAnim].time =
						Random.Range(0.0f, charAnimation[this.IdleAnim].length);

					this.DefaultHair.SetActive(false);
					this.Mask.SetActive(false);

					this.EasterHair.SetActive(true);
					this.Bandanas.SetActive(true);

					if (this.HairRenderer != null)
					{
						this.HairRenderer.material.color = this.HairColor;
					}

					this.DelinquentManager.EasterEgg();
				}
			}
		}

		if (Suck)
		{
			transform.position = Vector3.MoveTowards(transform.position, TimePortal.position, Time.deltaTime * 10);

			if (transform.position == TimePortal.position)
			{
				Destroy(gameObject);
			}
		}
	}

	void Shove()
	{
		if (!this.Yandere.Shoved && !this.Yandere.Tripping)
		{
			if (this.DistanceToPlayer < 0.50f)
			{
				AudioSource delinquentAudioSource =
					this.DelinquentManager.GetComponent<AudioSource>();

				delinquentAudioSource.clip = this.ShoveClips[Random.Range(0, this.ShoveClips.Length)];
				delinquentAudioSource.Play();
				this.DelinquentManager.SpeechTimer = 5.0f;

				if (this.Yandere.transform.position.x > this.transform.position.x)
				{
					this.Yandere.transform.position = new Vector3(
						this.transform.position.x - 0.0010f,
						this.Yandere.transform.position.y,
						this.Yandere.transform.position.z);
				}

				if (this.Yandere.Aiming)
				{
					this.Yandere.StopAiming();
				}

				Animation charAnimation = this.Character.GetComponent<Animation>();
				charAnimation[this.ShoveAnim].time = 0.0f;
				charAnimation.CrossFade(this.ShoveAnim);
				this.Shoving = true;

				this.Yandere.Character.GetComponent<Animation>().CrossFade(AnimNames.FemaleShoveA);
				this.Yandere.Punching = false;
				this.Yandere.CanMove = false;
				this.Yandere.Shoved = true;
				this.Yandere.ShoveSpeed = 2;

				this.ExpressedSurprise = false;
				this.Threatening = false;
				this.Cooldown = false;
				this.Timer = 0.0f;
			}
		}
	}

	void LateUpdate()
	{
		if (!this.Threatening)
		{
			if (!this.Shoving)
			{
				if (!this.Rapping)
				{
					// [af] Replaced if/else statement with assignment and ternary expression.
					this.LookAtTarget = Vector3.Lerp(
						this.LookAtTarget,
						this.LookAtPlayer ? this.Yandere.Head.position : this.Default.position,
						Time.deltaTime * 2.0f);

					this.Neck.LookAt(this.LookAtTarget);
				}
			}

			if (this.HeadStill)
			{
				this.Head.transform.localEulerAngles = Vector3.zero;
			}
		}

		if (this.BustSize > 0.0f)
		{
			this.RightBreast.localScale = new Vector3(this.BustSize, this.BustSize, this.BustSize);
			this.LeftBreast.localScale = new Vector3(this.BustSize, this.BustSize, this.BustSize);
		}
	}

	void OnEnable()
	{
		this.Character.GetComponent<Animation>().CrossFade(this.IdleAnim, 1.0f);
	}
}
