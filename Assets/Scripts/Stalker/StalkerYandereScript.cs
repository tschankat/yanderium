using Bayat.SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerYandereScript : MonoBehaviour
{
	public CharacterController MyController;

    public AutoSaveManager SaveManager;

	public Transform TrellisClimbSpot;
    public Transform CameraTarget;
    public Transform ObjectTarget;
    public Transform RightHand;
    public Transform EntryPOV;
    public Transform RightArm;
    public Transform Object;
    public Transform Hips;

	public RPG_Camera RPGCamera;

	public Animation MyAnimation;

	public AudioSource Jukebox;

	public Camera MainCamera;

	public bool Climbing;
	public bool Running;
	public bool CanMove;
	public bool Street;

	public Stance Stance = new Stance(StanceType.Standing);

	public string IdleAnim;
	public string WalkAnim;
	public string RunAnim;

	public string CrouchIdleAnim;
	public string CrouchWalkAnim;
	public string CrouchRunAnim;

	public float WalkSpeed;
	public float RunSpeed;

	public float CrouchWalkSpeed;
	public float CrouchRunSpeed;

	public int ClimbPhase;
	public int Frame;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
	{
#if !UNITY_EDITOR
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
#endif

        if (Input.GetKeyDown("="))
		{
			if (Time.timeScale < 10)
			{
				Time.timeScale++;
			}
		}

		if (Input.GetKeyDown("-"))
		{
			if (Time.timeScale > 1)
			{
				Time.timeScale--;
			}
		}

		if (Input.GetKeyDown("m"))
		{
			PlayerGlobals.Money++;

			if (Jukebox != null)
			{
				if (Jukebox.isPlaying)
				{
					Jukebox.Stop();
				}
				else
				{
					Jukebox.Play();
				}
			}
		}

		#if UNITY_EDITOR
		if (Input.GetKeyDown("1"))
		{
			transform.position = new Vector3(-13, 0, -2.5f);
            Physics.SyncTransforms();
		}

		if (Input.GetKeyDown("2"))
		{
			transform.position = new Vector3(-9.5f, 4, -2.5f);
			RenderSettings.ambientIntensity = 8;
			Physics.SyncTransforms();
		}

        if (Input.GetKeyDown("3"))
        {
            transform.position = new Vector3(4, 0, 1);
            RenderSettings.ambientIntensity = 8;
            Physics.SyncTransforms();
        }

        if (Input.GetKeyDown("4"))
        {
            transform.position = new Vector3(-8.5f, 0, -8.5f);
            RenderSettings.ambientIntensity = 8;
            Physics.SyncTransforms();
        }
#endif

        //Debug.Log ("1");

        if (CanMove)
		{
			if (CameraTarget != null)
			{
				CameraTarget.localPosition = new Vector3(0, 1 + (RPGCamera.distanceMax - RPGCamera.distance) * .2f, 0);
			}

			UpdateMovement();
		}
		else
		{
			if (CameraTarget != null)
			{
				//Debug.Log ("2");

				if (Climbing)
				{
					//Debug.Log ("3");

					if (ClimbPhase == 1)
					{
						if (MyAnimation["f02_climbTrellis_00"].time < MyAnimation["f02_climbTrellis_00"].length - 1)
						{
							//Debug.Log ("4");

							CameraTarget.position = Vector3.MoveTowards(CameraTarget.position, Hips.position
								+ new Vector3(0, 0.103729f, 0.003539f), Time.deltaTime);
						}
						else
						{
							CameraTarget.position = Vector3.MoveTowards(CameraTarget.position,
								new Vector3(-9.5f, 5, -2.5f), Time.deltaTime);
						}

						MoveTowardsTarget(TrellisClimbSpot.position);
						SpinTowardsTarget(TrellisClimbSpot.rotation);

						if (MyAnimation["f02_climbTrellis_00"].time > 7.5f)
						{
							RPGCamera.transform.position = EntryPOV.position;
							RPGCamera.transform.eulerAngles = EntryPOV.eulerAngles;
							RPGCamera.enabled = false;

							RenderSettings.ambientIntensity = 8;
							ClimbPhase++;
						}
					}
					else
					{
						RPGCamera.transform.position = EntryPOV.position;
						RPGCamera.transform.eulerAngles = EntryPOV.eulerAngles;

						if (MyAnimation["f02_climbTrellis_00"].time > 11)
						{
							transform.position = Vector3.MoveTowards (
								transform.position,
								TrellisClimbSpot.position + new Vector3(.4f, 0, 0),
								Time.deltaTime * .5f);
						}
					}

					if (MyAnimation["f02_climbTrellis_00"].time > MyAnimation["f02_climbTrellis_00"].length)
					{
						MyAnimation.Play("f02_idleShort_00");
						transform.position = new Vector3(-9.1f, 4, -2.5f);
						CameraTarget.position = transform.position + new Vector3(0, 1, 0);
						RPGCamera.enabled = true;
						Climbing = false;
						CanMove = true;

                        Physics.SyncTransforms();
					}
				}
			}
		}

		if (Street)
		{
			if (transform.position.x < -16)
			{
				transform.position = new Vector3(-16, 0, transform.position.z);
			}
		}
	}

	void UpdateMovement()
	{
		if (!OptionGlobals.ToggleRun)
		{
			this.Running = false;

			if (Input.GetButton(InputNames.Xbox_LB))
			{
				this.Running = true;
			}
		}
		else
		{
			if (Input.GetButtonDown(InputNames.Xbox_LB))
			{
				this.Running = !this.Running;
			}
		}

		MyController.Move(Physics.gravity * Time.deltaTime);

		float v = Input.GetAxis("Vertical");
		float h = Input.GetAxis("Horizontal");

		Vector3 forward = MainCamera.transform.TransformDirection(Vector3.forward);
		forward.y = 0.0f;
		forward = forward.normalized;

		Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);

		Vector3 targetDirection = (h * right) + (v * forward);

		Quaternion targetRotation = Quaternion.identity;

		if (targetDirection != Vector3.zero)
		{
			targetRotation = Quaternion.LookRotation(targetDirection);
		}

		if (targetDirection != Vector3.zero)
		{
			transform.rotation = Quaternion.Lerp(
				transform.rotation, targetRotation, Time.deltaTime * 10.0f);
		}
		else
		{
			targetRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
		}

		if (!Street)
		{
			if (Stance.Current == StanceType.Standing)
			{
				if (Input.GetButtonDown(InputNames.Xbox_RS))
				{
					Stance.Current = StanceType.Crouching;
                    MyController.center = new Vector3(0, .5f, 0);
                    MyController.height = 1;
                }
			}
			else
			{
				if (Input.GetButtonDown(InputNames.Xbox_RS))
				{
					Stance.Current = StanceType.Standing;
                    MyController.center = new Vector3(0, .75f, 0);
                    MyController.height = 1.5f;
                }
			}
		}

		// If we are getting directional input...
		if (v != 0.0f || h != 0.0f)
		{
			// If the Run button is held down...
			if (this.Running)
			{
				if (Stance.Current == StanceType.Crouching)
				{
					// Crouch-Run animation.
					MyAnimation.CrossFade(CrouchRunAnim);
					MyController.Move(transform.forward * CrouchRunSpeed *
						Time.deltaTime);
				}
				else
				{
					// Run animation.
					MyAnimation.CrossFade(RunAnim);
					MyController.Move(transform.forward * RunSpeed *
						Time.deltaTime);
				}
			}
			// If the Run button is not held down...
			else
			{
				if (Stance.Current == StanceType.Crouching)
				{
					// Crouch-walk animation.
					MyAnimation.CrossFade(CrouchWalkAnim);
					MyController.Move(transform.forward * (CrouchWalkSpeed *
						Time.deltaTime));
				}
				else
				{
					// Walk animation.
					MyAnimation.CrossFade(WalkAnim);
					MyController.Move(transform.forward * (WalkSpeed *
						Time.deltaTime));
				}
			}
		}
		// If we are not getting directional input...
		else
		{
			if (Stance.Current == StanceType.Crouching)
			{
				MyAnimation.CrossFade(CrouchIdleAnim);
			}
			else
			{
				MyAnimation.CrossFade(IdleAnim);
			}
		}
	}

    private void LateUpdate()
    {
        if (Object != null)
        {
            if (RightArm != null)
            {
                RightArm.localEulerAngles = new Vector3(
                    RightArm.localEulerAngles.x,
                    RightArm.localEulerAngles.y + 15,
                    RightArm.localEulerAngles.z);
            }

            Object.LookAt(ObjectTarget);
        }
    }

    void MoveTowardsTarget(Vector3 target)
	{
		Vector3 offset = target - this.transform.position;
		this.MyController.Move(offset * (Time.deltaTime * 10.0f));
	}

	void SpinTowardsTarget(Quaternion target)
	{
		this.transform.rotation = Quaternion.Slerp(this.transform.rotation, target,
			Time.deltaTime * 10.0f);
	}
}