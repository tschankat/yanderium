using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class YanvaniaYanmontScript : MonoBehaviour
{
	private GameObject NewBlood;

	public YanvaniaCameraScript YanvaniaCamera;
	public InputManagerScript InputManager;
	public YanvaniaDraculaScript Dracula;

	public CharacterController MyController;

	public GameObject BossHealthBar;
	public GameObject LevelUpEffect;
	public GameObject DeathBlood;
	public GameObject Character;
	public GameObject BlackBG;
	public GameObject TextBox;

	public Renderer MyRenderer;

	public Transform TryAgainWindow;
	public Transform HealthBar;
	public Transform EXPBar;
	public Transform Hips;

	public Transform TrailStart;
	public Transform TrailEnd;

	public UITexture Photograph;

	public UILabel LevelLabel;

	public UISprite Darkness;

	public Collider[] SphereCollider;
	public Collider[] WhipCollider;
	public Transform[] WhipChain;

	public AudioClip[] Voices;
	public AudioClip[] Injuries;

	public AudioClip DeathSound;
	public AudioClip LandSound;
	public AudioClip WhipSound;

	public bool Attacking = false;
	public bool Crouching = false;
	public bool Dangling = false;

	public bool EnterCutscene = false;
	public bool Cutscene = false;
	public bool CanMove = false;
	public bool Injured = false;
	public bool Loose = false;
	public bool Red = false;

	public bool SpunUp = false;
	public bool SpunDown = false;
	public bool SpunRight = false;
	public bool SpunLeft = false;

	public float TargetRotation = 0.0f;
	public float Rotation = 0.0f;

	public float RecoveryTimer = 0.0f;
	public float DeathTimer = 0.0f;
	public float FlashTimer = 0.0f;
	public float IdleTimer = 0.0f;
	public float WhipTimer = 0.0f;
	public float TapTimer = 0.0f;

	public float PreviousY = 0.0f;
	public float MaxHealth = 100.0f;
	public float Health = 100.0f;
	public float EXP = 0.0f;

	public int Frames = 0;
	public int Level = 0;
	public int Taps = 0;

	public float walkSpeed = 6.0f;
	public float runSpeed = 11.0f;

	// If true, diagonal speed (when strafing + moving forward or back) can't 
	// exceed normal move speed; otherwise it's about 1.4 times faster.
	public bool limitDiagonalSpeed = true;

	// If checked, the run key toggles between running and walking. Otherwise player 
	// runs if the key is held down and walks otherwise. There must be a button set up 
	// in the Input Manager called "Run".
	public bool toggleRun = false;

	public float jumpSpeed = 8.0f;
	public float gravity = 20.0f;

	// Units that player can fall before a falling damage function is run. To disable, 
	// type "infinity" in the inspector.
	public float fallingDamageThreshold = 10.0f;

	// If the player ends up on a slope which is at least the Slope Limit as set on the 
	// character controller, then he will slide down.
	public bool slideWhenOverSlopeLimit = false;

	// If checked and the player is on an object tagged "Slide", he will slide down it 
	// regardless of the slope limit.
	public bool slideOnTaggedObjects = false;

	public float slideSpeed = 12.0f;

	// If checked, then the player can change direction while in the air.
	public bool airControl = false;

	// Small amounts of this results in bumping when walking down slopes, but large amounts 
	// results in falling too fast.
	public float antiBumpFactor = 0.75f;

	// Player must be grounded for at least this many physics frames before being able to 
	// jump again; set to 0 to allow bunny hopping.
	public int antiBunnyHopFactor = 1;

	private Vector3 moveDirection = Vector3.zero;
	public bool grounded = false;
	private CharacterController controller;
	private Transform myTransform;
	private float speed;
	private RaycastHit hit;
	private float fallStartLevel;
	private bool falling = false;
	private float slideLimit;
	private float rayDistance;
	private Vector3 contactPoint;
	private bool playerControl = false;
	private int jumpTimer;
	private float originalThreshold;

	public float inputX = 0.0f;

	void Awake()
	{
		// [af] Moved here from class scope for compatibility with C#.
		Animation charAnimation = this.Character.GetComponent<Animation>();
		charAnimation[AnimNames.FemaleYanvaniaDeath].speed = 0.25f;
		charAnimation[AnimNames.FemaleYanvaniaAttack].speed = 2.0f;
		charAnimation[AnimNames.FemaleYanvaniaCrouchAttack].speed = 2.0f;
		charAnimation[AnimNames.FemaleYanvaniaWalk].speed = 2.0f / 3.0f;

		charAnimation[AnimNames.FemaleYanvaniaWhipNeutral].speed = 0.0f;
		charAnimation[AnimNames.FemaleYanvaniaWhipUp].speed = 0.0f;
		charAnimation[AnimNames.FemaleYanvaniaWhipRight].speed = 0.0f;
		charAnimation[AnimNames.FemaleYanvaniaWhipDown].speed = 0.0f;
		charAnimation[AnimNames.FemaleYanvaniaWhipLeft].speed = 0.0f;

		charAnimation[AnimNames.FemaleYanvaniaCrouchPose].layer = 1;
		charAnimation.Play(AnimNames.FemaleYanvaniaCrouchPose);
		charAnimation[AnimNames.FemaleYanvaniaCrouchPose].weight = 0.0f;

		Physics.IgnoreLayerCollision(19, 13, true);
		Physics.IgnoreLayerCollision(19, 19, true);
	}

	void Start()
	{
		this.WhipChain[0].transform.localScale = Vector3.zero;

		this.Character.GetComponent<Animation>().Play(AnimNames.FemaleYanvaniaIdle);

		this.controller = this.GetComponent<CharacterController>();
		this.myTransform = this.transform;
		this.speed = this.walkSpeed;
		this.rayDistance = (this.controller.height * 0.50f) + this.controller.radius;
		this.slideLimit = this.controller.slopeLimit - 0.10f;
		this.jumpTimer = this.antiBunnyHopFactor;

		// [af] Removed unused "oldPos" variable.

		this.originalThreshold = this.fallingDamageThreshold;
	}

	void FixedUpdate()
	{
		Animation charAnimation = this.Character.GetComponent<Animation>();

		if (this.CanMove)
		{
			if (!this.Injured)
			{
				if (!this.Cutscene)
				{
					if (this.grounded)
					{
						if (!this.Attacking)
						{
							if (!this.Crouching)
							{
								if (Input.GetAxis(InputNames.Yanvania_Horizontal) > 0.0f)
								{
									this.inputX = 1.0f;
								}
								else if (Input.GetAxis(InputNames.Yanvania_Horizontal) < 0.0f)
								{
									this.inputX = -1.0f;
								}
								else
								{
									this.inputX = 0.0f;
								}
							}
						}
						else
						{
							if (this.grounded)
							{
								this.fallingDamageThreshold = 100.0f;
								this.moveDirection.x = 0.0f;
								this.inputX = 0.0f;
								this.speed = 0.0f;
							}
						}
					}
					else
					{
						if (Input.GetAxis(InputNames.Yanvania_Horizontal) != 0.0f)
						{
							if (Input.GetAxis(InputNames.Yanvania_Horizontal) > 0.0f)
							{
								this.inputX = 1.0f;
							}
							else if (Input.GetAxis(InputNames.Yanvania_Horizontal) < 0.0f)
							{
								this.inputX = -1.0f;
							}
							else
							{
								this.inputX = 0.0f;
							}
						}
						else
						{
							this.inputX = Mathf.MoveTowards(this.inputX, 0.0f, Time.deltaTime * 10.0f);
						}
					}

					float inputY = 0.0f;

					// If both horizontal and vertical are used simultaneously, limit speed 
					// (if allowed), so the total doesn't exceed normal move speed.
					// [af] First ternary value is (sqrt(2) / 2).
					float inputModifyFactor = ((this.inputX != 0.0f) && (inputY != 0.0f) &&
						this.limitDiagonalSpeed) ? 0.70710678f : 1.0f;

					if (!this.Attacking)
					{
						if (Input.GetAxis(InputNames.Yanvania_Horizontal) < 0.0f)
						{
							this.Character.transform.localEulerAngles = new Vector3(
								this.Character.transform.localEulerAngles.x,
								-90.0f,
								this.Character.transform.localEulerAngles.z);

							this.Character.transform.localScale = new Vector3(
								1.0f,
								this.Character.transform.localScale.y,
								this.Character.transform.localScale.z);
						}
						else if (Input.GetAxis(InputNames.Yanvania_Horizontal) > 0.0f)
						{
							this.Character.transform.localEulerAngles = new Vector3(
								this.Character.transform.localEulerAngles.x,
								90.0f,
								this.Character.transform.localEulerAngles.z);

							this.Character.transform.localScale = new Vector3(
								-1.0f,
								this.Character.transform.localScale.y,
								this.Character.transform.localScale.z);
						}
					}

					if (this.grounded)
					{
						if (!this.Attacking && !this.Dangling)
						{
							if (Input.GetAxis(InputNames.Yanvania_Vertical) < -0.50f)
							{
								this.MyController.center = new Vector3(
									this.MyController.center.x,
									0.50f,
									this.MyController.center.z);

								this.MyController.height = 1.0f;
								this.Crouching = true;
								this.IdleTimer = 10.0f;
								this.inputX = 0.0f;
							}

							if (this.Crouching)
							{
								charAnimation.CrossFade(AnimNames.FemaleYanvaniaCrouch, 0.10f);

								if (!this.Attacking)
								{
									//If we are NOT dangling...
									if (!this.Dangling)
									{
										if (Input.GetAxis(InputNames.Yanvania_Vertical) > -0.50f)
										{
											charAnimation[AnimNames.FemaleYanvaniaCrouchPose].weight = 0.0f;
											this.MyController.center = new Vector3(
												this.MyController.center.x,
												0.75f,
												this.MyController.center.z);

											this.MyController.height = 1.50f;
											this.Crouching = false;
										}
									}
									//If we ARE dangling...
									else
									{
										if ((Input.GetAxis(InputNames.Yanvania_Vertical) > -0.50f) && Input.GetButton(InputNames.Xbox_X))
										{
											charAnimation[AnimNames.FemaleYanvaniaCrouchPose].weight = 0.0f;

											this.MyController.center = new Vector3(
												this.MyController.center.x,
												0.75f,
												this.MyController.center.z);

											this.MyController.height = 1.50f;
											this.Crouching = false;
										}
									}
								}
							}
							else
							{
								if (this.inputX == 0.0f)
								{
									if (this.IdleTimer > 0.0f)
									{
										charAnimation.CrossFade(AnimNames.FemaleYanvaniaIdle, 0.10f);

										charAnimation[AnimNames.FemaleYanvaniaIdle].speed = this.IdleTimer / 10.0f;
									}
									else
									{
										charAnimation.CrossFade(AnimNames.FemaleYanvaniaDramaticIdle, 1.0f);
									}

									this.IdleTimer -= Time.deltaTime;
								}
								else
								{
									this.IdleTimer = 10.0f;

									// [af] Replaced if/else statement with ternary expression.
									charAnimation.CrossFade((this.speed == this.walkSpeed) ?
										AnimNames.FemaleYanvaniaWalk : AnimNames.FemaleYanvaniaRun, 0.10f);
								}
							}
						}

						bool sliding = false;

						// See if surface immediately below should be slid down. We use this normally 
						// rather than a ControllerColliderHit point, because that interferes with step 
						// climbing amongst other annoyances.
						if (Physics.Raycast(this.myTransform.position, -Vector3.up, out this.hit, this.rayDistance))
						{
							if (Vector3.Angle(this.hit.normal, Vector3.up) > this.slideLimit)
							{
								sliding = true;
							}
						}
						// However, just raycasting straight down from the center can fail when on 
						// steep slopes. So if the above raycast didn't catch anything, raycast down 
						// from the stored ControllerColliderHit point instead.
						else
						{
							Physics.Raycast(this.contactPoint + Vector3.up, -Vector3.up, out this.hit);

							if (Vector3.Angle(this.hit.normal, Vector3.up) > this.slideLimit)
							{
								sliding = true;
							}
						}

						// If we were falling, and we fell a vertical distance greater than the 
						// threshold, run a falling damage routine.
						if (this.falling)
						{
							this.falling = false;

							if (this.myTransform.position.y < (this.fallStartLevel - this.fallingDamageThreshold))
							{
								this.FallingDamageAlert(this.fallStartLevel - this.myTransform.position.y);
							}

							this.fallingDamageThreshold = this.originalThreshold;
						}

						// If running isn't on a toggle, then use the appropriate speed depending on 
						// whether the run button is down.
						if (!this.toggleRun)
						{
							this.speed = Input.GetKey(KeyCode.LeftShift) ? this.runSpeed : this.walkSpeed;
						}

						// If sliding (and it's allowed), or if we're on an object tagged "Slide", get a 
						// vector pointing down the slope we're on.
						if ((sliding && this.slideWhenOverSlopeLimit) ||
							(this.slideOnTaggedObjects && (this.hit.collider.tag == "Slide")))
						{
							Vector3 hitNormal = this.hit.normal;
							this.moveDirection = new Vector3(hitNormal.x, -hitNormal.y, hitNormal.z);
							Vector3.OrthoNormalize(ref hitNormal, ref this.moveDirection);
							this.moveDirection *= this.slideSpeed;
							this.playerControl = false;
						}
						// Otherwise recalculate moveDirection directly from axes, adding a bit of 
						// -y to avoid bumping down inclines.
						else
						{
							this.moveDirection = new Vector3(
								this.inputX * inputModifyFactor,
								-this.antiBumpFactor,
								inputY * inputModifyFactor);
							this.moveDirection = this.myTransform.TransformDirection(this.moveDirection) * this.speed;
							this.playerControl = true;
						}

						// Jump! But only if the jump button has been released and player has been 
						// grounded for a given number of frames.
						if (!Input.GetButton(InputNames.Yanvania_Jump))
						{
							this.jumpTimer++;
						}
						else if (this.jumpTimer >= this.antiBunnyHopFactor)
						{
							if (!this.Attacking)
							{
								this.Crouching = false;
								this.fallingDamageThreshold = 0.0f;
								this.moveDirection.y = this.jumpSpeed;
								this.IdleTimer = 10.0f;
								this.jumpTimer = 0;

								AudioSource audioSource = this.GetComponent<AudioSource>();
								audioSource.clip = this.Voices[Random.Range(0, this.Voices.Length)];
								audioSource.Play();
							}
						}
					}
					else
					{
						if (!this.Attacking)
						{
							// [af] Replaced if/else statement with ternary expression.
							charAnimation.CrossFade((this.transform.position.y > this.PreviousY) ?
								AnimNames.FemaleYanvaniaJump : AnimNames.FemaleYanvaniaFall, 0.40f);
						}

						this.PreviousY = transform.position.y;

						// If we stepped over a cliff or something, set the height at which we 
						// started falling.
						if (!this.falling)
						{
							this.falling = true;
							this.fallStartLevel = this.myTransform.position.y;
						}

						// If air control is allowed, check movement but don't touch the y component.
						if (this.airControl && this.playerControl)
						{
							this.moveDirection.x = this.inputX * this.speed * inputModifyFactor;
							this.moveDirection.z = inputY * this.speed * inputModifyFactor;
							this.moveDirection = this.myTransform.TransformDirection(this.moveDirection);
						}
					}
				}
				else
				{
					this.moveDirection.x = 0.0f;

					if (this.grounded)
					{
						if (this.transform.position.x > -34.0f)
						{
							this.Character.transform.localEulerAngles = new Vector3(
								this.Character.transform.localEulerAngles.x,
								-90.0f,
								this.Character.transform.localEulerAngles.z);

							this.Character.transform.localScale = new Vector3(
								1.0f,
								this.Character.transform.localScale.y,
								this.Character.transform.localScale.z);

							this.transform.position = new Vector3(
								Mathf.MoveTowards(this.transform.position.x, -34.0f, Time.deltaTime * this.walkSpeed),
								this.transform.position.y,
								this.transform.position.z);

							charAnimation.CrossFade(AnimNames.FemaleYanvaniaWalk);
						}
						else if (transform.position.x < -34.0f)
						{
							this.Character.transform.localEulerAngles = new Vector3(
								this.Character.transform.localEulerAngles.x,
								90.0f,
								this.Character.transform.localEulerAngles.z);

							this.Character.transform.localScale = new Vector3(
								-1.0f,
								this.Character.transform.localScale.y,
								this.Character.transform.localScale.z);

							this.transform.position = new Vector3(
								Mathf.MoveTowards(this.transform.position.x, -34.0f, Time.deltaTime * this.walkSpeed),
								this.transform.position.y,
								this.transform.position.z);

							charAnimation.CrossFade(AnimNames.FemaleYanvaniaWalk);
						}
						else
						{
							charAnimation.CrossFade(AnimNames.FemaleYanvaniaDramaticIdle, 1.0f);

							this.Character.transform.localEulerAngles = new Vector3(
								this.Character.transform.localEulerAngles.x,
								-90.0f,
								this.Character.transform.localEulerAngles.z);

							this.Character.transform.localScale = new Vector3(
								1.0f,
								this.Character.transform.localScale.y,
								this.Character.transform.localScale.z);

							this.WhipChain[0].transform.localScale = Vector3.zero;
							this.fallingDamageThreshold = 100.0f;
							this.TextBox.SetActive(true);
							this.Attacking = false;
							this.enabled = false;
						}
					}

					Physics.SyncTransforms();
				}
			}
			else
			{
				charAnimation.CrossFade(AnimNames.FemaleDamage25);

				this.RecoveryTimer += Time.deltaTime;

				if (this.RecoveryTimer > 1.0f)
				{
					this.RecoveryTimer = 0.0f;
					this.Injured = false;
				}
			}

			// Apply gravity.
			this.moveDirection.y -= this.gravity * Time.deltaTime;

			// Move the controller, and set grounded true or false depending on whether 
			// we're standing on something.
			this.grounded = (this.controller.Move(
				this.moveDirection * Time.deltaTime) & CollisionFlags.Below) != 0;

			if (this.grounded)
			{
				if (this.EnterCutscene)
				{
					this.YanvaniaCamera.Cutscene = true;
					this.Cutscene = true;
				}
			}

			if ((this.controller.collisionFlags & CollisionFlags.Above) != 0)
			{
				if (this.moveDirection.y > 0.0f)
				{
					this.moveDirection.y = 0.0f;
				}
			}
		}
		else
		{
			if (this.Health == 0.0f)
			{
				this.DeathTimer += Time.deltaTime;

				if (this.DeathTimer > 5.0f)
				{
					this.Darkness.color = new Color(
						this.Darkness.color.r,
						this.Darkness.color.g,
						this.Darkness.color.b,
						this.Darkness.color.a + (Time.deltaTime * 0.20f));

					if (this.Darkness.color.a >= 1.0f)
					{
						if (this.Darkness.gameObject.activeInHierarchy)
						{
							this.HealthBar.parent.gameObject.SetActive(false);
							this.EXPBar.parent.gameObject.SetActive(false);
							this.Darkness.gameObject.SetActive(false);
							this.BossHealthBar.SetActive(false);
							this.BlackBG.SetActive(true);
						}

						this.TryAgainWindow.transform.localScale = Vector3.Lerp(
							this.TryAgainWindow.transform.localScale,
							new Vector3(1.0f, 1.0f, 1.0f),
							Time.deltaTime * 10.0f);
					}
				}
			}
		}
	}

	void Update()
	{
		Animation charAnimation = this.Character.GetComponent<Animation>();

		if (!this.Injured)
		{
			if (this.CanMove)
			{
				if (!this.Cutscene)
				{
					// If the run button is set to toggle, then switch between walk/run speed. 
					// (We use Update for this... FixedUpdate is a poor place to use GetButtonDown, 
					// since it doesn't necessarily run every frame and can miss the event).
					//if (toggleRun && grounded && Input.GetKeyDown(KeyCode.LeftShift))
					//speed = (speed == walkSpeed? runSpeed : walkSpeed);

					if (this.grounded)
					{
						if (this.InputManager.TappedRight || this.InputManager.TappedLeft)
						{
							this.TapTimer = 0.25f;
							this.Taps++;
						}

						if (this.Taps > 1)
						{
							this.speed = this.runSpeed;
						}
					}

					if (this.inputX == 0.0f)
					{
						this.speed = this.walkSpeed;
					}

					this.TapTimer -= Time.deltaTime;

					if (this.TapTimer < 0.0f)
					{
						this.Taps = 0;
					}

					if (Input.GetButtonDown(InputNames.Yanvania_Attack))
					{
						if (!this.Attacking)
						{
							AudioSource.PlayClipAtPoint(this.WhipSound, this.transform.position);

							AudioSource audioSource = this.GetComponent<AudioSource>();
							audioSource.clip = this.Voices[Random.Range(0, this.Voices.Length)];
							audioSource.Play();

							this.WhipChain[0].transform.localScale = Vector3.zero;
							this.Attacking = true;
							this.IdleTimer = 10.0f;

							if (this.Crouching)
							{
								charAnimation[AnimNames.FemaleYanvaniaCrouchAttack].time = 0.0f;
								charAnimation.Play(AnimNames.FemaleYanvaniaCrouchAttack);
							}
							else
							{
								charAnimation[AnimNames.FemaleYanvaniaAttack].time = 0.0f;
								charAnimation.Play(AnimNames.FemaleYanvaniaAttack);
							}

							if (this.grounded)
							{
								this.moveDirection.x = 0.0f;
								this.inputX = 0.0f;
								this.speed = 0.0f;
							}
						}
					}

					if (this.Attacking)
					{
						//If we are NOT dangling...
						if (!this.Dangling)
						{
							this.WhipChain[0].transform.localScale = Vector3.MoveTowards(
								this.WhipChain[0].transform.localScale,
								new Vector3(1.0f, 1.0f, 1.0f),
								Time.deltaTime * 5.0f);

							this.StraightenWhip();
						}
						//If we ARE dangling...
						else
						{
							this.LoosenWhip();

							if ((Input.GetAxis(InputNames.Yanvania_Horizontal) > -0.50f) &&
								(Input.GetAxis(InputNames.Yanvania_Horizontal) < 0.50f) &&
								(Input.GetAxis(InputNames.Yanvania_Vertical) > -0.50f) &&
								(Input.GetAxis(InputNames.Yanvania_Vertical) < 0.50f))
							{
								charAnimation.CrossFade(AnimNames.FemaleYanvaniaWhipNeutral);

								if (this.Crouching)
								{
									charAnimation[AnimNames.FemaleYanvaniaCrouchPose].weight = 1.0f;
								}

								this.SpunUp = false;
								this.SpunDown = false;
								this.SpunRight = false;
								this.SpunLeft = false;
							}
							else
							{
								if (Input.GetAxis(InputNames.Yanvania_Vertical) > .50f)
								{
									if (!this.SpunUp)
									{
										AudioClipPlayer.Play2D(this.WhipSound, this.transform.position,
											Random.Range(0.90f, 1.10f));

										this.StraightenWhip();
										this.TargetRotation = -360.0f;
										this.Rotation = 0.0f;
										this.SpunUp = true;
									}

									charAnimation.CrossFade(AnimNames.FemaleYanvaniaWhipUp, 0.10f);
									//LoosenWhip();
								}
								else
								{
									this.SpunUp = false;
								}

								if (Input.GetAxis(InputNames.Yanvania_Vertical) < -.50f)
								{
									if (!this.SpunDown)
									{
										AudioClipPlayer.Play2D(this.WhipSound, this.transform.position,
											Random.Range(0.90f, 1.10f));

										this.StraightenWhip();
										this.TargetRotation = 360.0f;
										this.Rotation = 0.0f;
										this.SpunDown = true;
									}

									charAnimation.CrossFade(AnimNames.FemaleYanvaniaWhipDown, 0.10f);
									//LoosenWhip();
								}
								else
								{
									this.SpunDown = false;
								}

								if (Input.GetAxis(InputNames.Yanvania_Horizontal) > 0.50f)
								{
									if (this.Character.transform.localScale.x == 1.0f)
									{
										this.SpinRight();
									}
									else
									{
										this.SpinLeft();
									}

									//LoosenWhip();
								}
								else
								{
									if (this.Character.transform.localScale.x == 1.0f)
									{
										this.SpunRight = false;
									}
									else
									{
										this.SpunLeft = false;
									}

									//LoosenWhip();
								}

								if (Input.GetAxis(InputNames.Yanvania_Horizontal) < -0.50f)
								{
									if (this.Character.transform.localScale.x == 1.0f)
									{
										this.SpinLeft();
									}
									else
									{
										this.SpinRight();
									}

									//LoosenWhip();
								}
								else
								{
									if (this.Character.transform.localScale.x == 1.0f)
									{
										this.SpunLeft = false;
									}
									else
									{
										this.SpunRight = false;
									}

									//LoosenWhip();
								}
							}

							this.Rotation = Mathf.MoveTowards(this.Rotation,
								this.TargetRotation, Time.deltaTime * 3600.0f * 0.50f);

							this.WhipChain[1].transform.localEulerAngles = new Vector3(0.0f, 0.0f, this.Rotation);

							if (!Input.GetButton(InputNames.Yanvania_Attack))
							{
								this.StopAttacking();
							}
						}
					}
					else
					{
						if (this.WhipCollider[1].enabled)
						{
							for (int ID = 1; ID < this.WhipChain.Length; ID++)
							{
								this.SphereCollider[ID].enabled = false;
								this.WhipCollider[ID].enabled = false;
							}
						}

						this.WhipChain[0].transform.localScale = Vector3.MoveTowards(
							this.WhipChain[0].transform.localScale,
							Vector3.zero,
							Time.deltaTime * 10.0f);
					}

					if (!this.Crouching && (charAnimation[AnimNames.FemaleYanvaniaAttack].time >= charAnimation[AnimNames.FemaleYanvaniaAttack].length) ||
						this.Crouching && (charAnimation[AnimNames.FemaleYanvaniaCrouchAttack].time >= charAnimation[AnimNames.FemaleYanvaniaCrouchAttack].length))
					{
						if (Input.GetButton(InputNames.Yanvania_Attack))
						{
							if (this.Crouching)
							{
								charAnimation[AnimNames.FemaleYanvaniaCrouchPose].weight = 1.0f;
							}

							this.Dangling = true;
						}
						else
						{
							this.StopAttacking();
						}
					}
				}
			}
		}

		if (this.FlashTimer > 0.0f)
		{
			this.FlashTimer -= Time.deltaTime;

			// [af] Removed unused "ID" variable.

			if (!this.Red)
			{
				// [af] Converted while loop to foreach loop.
				foreach (Material material in this.MyRenderer.materials)
				{
					material.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
				}

				this.Frames++;

				if (this.Frames == 5)
				{
					this.Frames = 0;
					this.Red = true;
				}
			}
			else
			{
				// [af] Converted while loop to foreach loop.
				foreach (Material material in this.MyRenderer.materials)
				{
					material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
				}

				this.Frames++;

				if (this.Frames == 5)
				{
					this.Frames = 0;
					this.Red = false;
				}
			}
		}
		else
		{
			this.FlashTimer = 0.0f;

			if (this.MyRenderer.materials[0].color != new Color(1.0f, 1.0f, 1.0f, 1.0f))
			{
				// [af] Converted while loop to foreach loop.
				foreach (Material material in this.MyRenderer.materials)
				{
					material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
				}
			}
		}

		this.HealthBar.localScale = new Vector3(
			this.HealthBar.localScale.x,
			Mathf.Lerp(this.HealthBar.localScale.y, this.Health / this.MaxHealth, Time.deltaTime * 10.0f),
			this.HealthBar.localScale.z);

		if (this.Health > 0.0f)
		{
			if (this.EXP >= 100.0f)
			{
				this.Level++;

				if (this.Level >= 99)
				{
					this.Level = 99;
				}
				else
				{
					GameObject NewEffect = Instantiate(this.LevelUpEffect,
						this.LevelLabel.transform.position, Quaternion.identity);
					NewEffect.transform.parent = this.LevelLabel.transform;

					this.MaxHealth += 20.0f;
					this.Health = this.MaxHealth;
					this.EXP -= 100.0f;
				}

				this.LevelLabel.text = this.Level.ToString();
			}

			this.EXPBar.localScale = new Vector3(
				this.EXPBar.localScale.x,
				Mathf.Lerp(this.EXPBar.localScale.y, this.EXP / 100.0f, Time.deltaTime * 10.0f),
				this.EXPBar.localScale.z);
		}

		this.transform.position = new Vector3(
			this.transform.position.x,
			this.transform.position.y,
			0.0f);

		if (Input.GetKeyDown(KeyCode.BackQuote))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			this.transform.position = new Vector3(-31.75f, 6.51f, 0.0f);
			Physics.SyncTransforms();
		}

		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			this.Level = 5;
			this.LevelLabel.text = this.Level.ToString();
		}

		if (Input.GetKeyDown(KeyCode.Equals))
		{
			Time.timeScale += 10.0f;
		}

		if (Input.GetKeyDown(KeyCode.Minus))
		{
			Time.timeScale -= 10.0f;

			if (Time.timeScale < 0.0f)
			{
				Time.timeScale = 1.0f;
			}
		}
	}

	void LateUpdate()
	{
		// [af] Commented in JS code.
		//TrailStart.position = this.WhipChain[1].position;
		//TrailEnd.position = this.WhipChain[10].position;
	}

	// Store point that we're in contact with for use in FixedUpdate if needed.
	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		this.contactPoint = this.hit.point;
	}

	// If falling damage occurred, this is the place to do something about it. You can make 
	// the player have hitpoints and remove some of them based on the distance fallen, add 
	// sound effects, etc..
	void FallingDamageAlert(float fallDistance)
	{
		AudioClipPlayer.Play2D(this.LandSound,
			this.transform.position, Random.Range(0.90f, 1.10f));

		this.Character.GetComponent<Animation>().Play(AnimNames.FemaleYanvaniaCrouch);

		this.fallingDamageThreshold = this.originalThreshold;
	}

	void SpinRight()
	{
		if (!this.SpunRight)
		{
			AudioClipPlayer.Play2D(this.WhipSound,
				this.transform.position, Random.Range(0.90f, 1.10f));

			this.StraightenWhip();
			this.TargetRotation = 360.0f;
			this.Rotation = 0.0f;
			this.SpunRight = true;
		}

		this.Character.GetComponent<Animation>().CrossFade(AnimNames.FemaleYanvaniaWhipRight, 0.10f);
	}

	void SpinLeft()
	{
		if (!this.SpunLeft)
		{
			AudioClipPlayer.Play2D(this.WhipSound,
				this.transform.position, Random.Range(0.90f, 1.10f));

			this.StraightenWhip();
			this.TargetRotation = -360.0f;
			this.Rotation = 0.0f;
			this.SpunLeft = true;
		}

		this.Character.GetComponent<Animation>().CrossFade(AnimNames.FemaleYanvaniaWhipLeft, 0.10f);
	}

	void StraightenWhip()
	{
		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < this.WhipChain.Length; ID++)
		{
			this.WhipCollider[ID].enabled = true;
			this.WhipChain[ID].gameObject.GetComponent<Rigidbody>().isKinematic = true;

			Transform transform = this.WhipChain[ID].transform;
			transform.localPosition = new Vector3(0.0f, -0.030f, 0.0f);
			transform.localEulerAngles = Vector3.zero;
		}

		this.WhipChain[1].transform.localPosition = new Vector3(0.0f, -0.10f, 0.0f);

		this.WhipTimer = 0;
		this.Loose = false;
	}

	void LoosenWhip()
	{
		if (!this.Loose)
		{
			this.WhipTimer += Time.deltaTime;

			if (this.WhipTimer > .25f)
			{
				for (int ID = 1; ID < this.WhipChain.Length; ID++)
				{
					this.WhipChain[ID].gameObject.GetComponent<Rigidbody>().isKinematic = false;
					this.SphereCollider[ID].enabled = true;
				}

				this.Loose = true;
			}
		}
	}

	void StopAttacking()
	{
		this.Character.GetComponent<Animation>()[AnimNames.FemaleYanvaniaCrouchPose].weight = 0.0f;
		this.TargetRotation = 0.0f;
		this.Rotation = 0.0f;

		this.Attacking = false;
		this.Dangling = false;
		this.SpunUp = false;
		this.SpunDown = false;
		this.SpunRight = false;
		this.SpunLeft = false;
	}

	public void TakeDamage(int Damage)
	{
		if (this.WhipCollider[1].enabled)
		{
			for (int ID = 1; ID < this.WhipChain.Length; ID++)
			{
				this.SphereCollider[ID].enabled = false;
				this.WhipCollider[ID].enabled = false;
			}
		}

		AudioSource audioSource = this.GetComponent<AudioSource>();
		audioSource.clip = this.Injuries[Random.Range(0, this.Injuries.Length)];
		audioSource.Play();

		this.WhipChain[0].transform.localScale = Vector3.zero;

		Animation charAnimation = this.Character.GetComponent<Animation>();
		charAnimation[AnimNames.FemaleDamage25].time = 0.0f;

		this.fallingDamageThreshold = 100.0f;
		this.moveDirection.x = 0.0f;
		this.RecoveryTimer = 0.0f;
		this.FlashTimer = 2.0f;
		this.Injured = true;

		this.StopAttacking();

		this.Health -= Damage;

		if (this.Dracula.Health <= 0.0f)
		{
			this.Health = 1.0f;
		}

		if (this.Dracula.Health > 0.0f)
		{
			if (this.Health <= 0.0f)
			{
				if (this.NewBlood == null)
				{
					this.MyController.enabled = false;
					this.YanvaniaCamera.StopMusic = true;

					audioSource.clip = this.DeathSound;
					audioSource.Play();

					this.NewBlood = Instantiate(this.DeathBlood,
						this.transform.position, Quaternion.identity);
					this.NewBlood.transform.parent = this.Hips;
					this.NewBlood.transform.localPosition = Vector3.zero;

					charAnimation.CrossFade(AnimNames.FemaleYanvaniaDeath);
					this.CanMove = false;
				}

				this.Health = 0.0f;
			}
		}
	}
}
