using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnappedYandereScript : MonoBehaviour
{
	public CharacterController MyController;

	public CameraFilterPack_FX_Glitch1 Glitch1;
	public CameraFilterPack_FX_Glitch2 Glitch2;
	public CameraFilterPack_FX_Glitch3 Glitch3;

	public CameraFilterPack_Glitch_Mozaic Glitch4;

	public CameraFilterPack_NewGlitch1 Glitch5;
	public CameraFilterPack_NewGlitch2 Glitch6;
	public CameraFilterPack_NewGlitch3 Glitch7;
	public CameraFilterPack_NewGlitch4 Glitch8;
	public CameraFilterPack_NewGlitch5 Glitch9;
	public CameraFilterPack_NewGlitch6 Glitch10;
	public CameraFilterPack_NewGlitch7 Glitch11;

	public CameraFilterPack_TV_CompressionFX CompressionFX;
	public CameraFilterPack_TV_Distorted Distorted;

	public CameraFilterPack_Blur_Tilt_Shift TiltShift;
	public CameraFilterPack_Blur_Tilt_Shift_V TiltShiftV;

	public CameraFilterPack_Noise_TV Static;

	public StudentManagerScript StudentManager;

	public SnapStudentScript TargetStudent;
	public InputDeviceScript InputDevice;

	public GameObject StabBloodEffect;
	public GameObject BloodEffect;
	public GameObject NewDoIt;

	public WeaponScript Knife;

	public AudioListener MyListener;

	public Transform SnapAttackPivot;
	public Transform FinalSnapPOV;
	public Transform SuicidePOV;

	public Transform RightFoot;
	public Transform RightHand;
	public Transform LeftHand;
	public Transform Spine;

	public AudioSource StaticNoise;
	public AudioSource AttackAudio;
	public AudioSource SnapStatic;
	public AudioSource SnapVoice;
	public AudioSource Jukebox;
	public AudioSource MyAudio;
	public AudioSource Rumble;

	public AudioClip EndSNAP;

	public UILabel SNAPLabel;

	public Camera MainCamera;

	public Animation MyAnim;

	public AudioClip Buzz;

	public AudioClip[] Whispers;

	public AudioClip[] FemaleDeathScreams;
	public AudioClip[] MaleDeathScreams;
	public AudioClip[] AttackSFX;

	public GameObject DoIt;

	public UISprite SuicideSprite;
	public UILabel SuicidePrompt;

	public bool KillingSenpai;
	public bool Attacking;
	public bool CanMove;
	public bool SpeedUp;
	public bool Whisper;
	public bool Armed;

	public string IdleAnim;
	public string WalkAnim;

	public float ImpatienceLimit;
	public float GlitchTimeLimit;
	public float WhisperTimer;
	public float AttackTimer;
	public float GlitchTimer;

	public float ImpatienceTimer;
	public float ListenTimer;
	public float HurryTimer;
	public float AnimSpeed;
	public float Target;
	public float Speed;

	public int BloodSpawned;
	public int AttackPhase;
	public int Teleports;
	public int AttackID;
	public int VoiceID;
	public int Attacks;
	public int Taps;

	public string[] AttackAnims;

	public WeaponScript[] Weapons;

	public bool[] AttacksUsed;

	void Start()
	{
		MyAnim[AttackAnims[1]].speed = 1.5f;
		MyAnim[AttackAnims[2]].speed = 1.5f;
		MyAnim[AttackAnims[3]].speed = 1.5f;
		MyAnim[AttackAnims[4]].speed = 1.5f;
		MyAnim[AttackAnims[5]].speed = 1.5f;
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

		if (Glitch1.enabled)
		{
			//Debug.Log(AttackAnims[AttackID] + "'s speed is: " + MyAnim[AttackAnims[AttackID]].speed);

			if (Attacking)
			{
				GlitchTimer += Time.deltaTime * MyAnim[AttackAnims[AttackID]].speed;
			}
			else
			{
				GlitchTimer += Time.deltaTime;
			}

			if (GlitchTimer > GlitchTimeLimit)
			{
				SetGlitches(false);

				if (MyAudio.clip != EndSNAP)
				{
					MyAudio.Stop();
				}

				if (Attacking)
				{
					SnapAttackPivot.position = TargetStudent.Student.Head.position;

					SnapAttackPivot.localEulerAngles = new Vector3(0, 0, 0);

					MainCamera.transform.parent = SnapAttackPivot;

					MainCamera.transform.localPosition = new Vector3(0, 0, -1);
					MainCamera.transform.localEulerAngles = new Vector3(0, 0, 0);

					SnapAttackPivot.localEulerAngles = new Vector3(
						Random.Range(-45.0f, 45.0f),
						Random.Range(0.0f, 360.0f),
						0);

					while (MainCamera.transform.position.y < transform.position.y + .1f)
					{
						SnapAttackPivot.localEulerAngles = new Vector3(
							Random.Range(-45.0f, 45.0f),
							Random.Range(0.0f, 360.0f),
							0);
					}

					MyAnim[AttackAnims[AttackID]].time = 0;
					MyAnim.Play(AttackAnims[AttackID]);
					MyAnim[AttackAnims[AttackID]].time = 0;
					MyAnim[AttackAnims[AttackID]].speed += .1f;

					//if (MyAnim[AttackAnims[AttackID]].speed > 2)
					//{
					//	MyAnim[AttackAnims[AttackID]].speed = 2f;
					//}

					TargetStudent.MyAnim[TargetStudent.AttackAnims[AttackID]].time = 0;
					TargetStudent.MyAnim.Play(TargetStudent.AttackAnims[AttackID]);
					TargetStudent.MyAnim[TargetStudent.AttackAnims[AttackID]].time = 0;
					TargetStudent.MyAnim[TargetStudent.AttackAnims[AttackID]].speed = MyAnim[AttackAnims[AttackID]].speed;

					if (TargetStudent.Student.Male)
					{
						MyAudio.clip = MaleDeathScreams[Random.Range(0, MaleDeathScreams.Length)];
						MyAudio.pitch = 1;
						MyAudio.Play();
					}
					else
					{
						MyAudio.clip = FemaleDeathScreams[Random.Range(0, FemaleDeathScreams.Length)];
						MyAudio.pitch = 1;
						MyAudio.Play();
					}

					AttackAudio.clip = AttackSFX[AttackID];
					AttackAudio.pitch = MyAnim[AttackAnims[AttackID]].speed;
					AttackAudio.Play();
				}
			}
		}

		if (!Armed)
		{
			foreach (WeaponScript weapon in Weapons)
			{
				if (weapon != null)
				{
					if (Vector3.Distance(transform.position, weapon.transform.position) < 1.5f)
					{
						weapon.Prompt.Circle[3].fillAmount = 0;
						SNAPLabel.text = "Kill him.";
						StaticNoise.volume = 0;
						Static.Fade = 0;
						HurryTimer = 0;
						Knife = weapon;
						Armed = true;
					}
				}
			}
		}
		else
		{
			Knife.gameObject.SetActive(true);
		}

		if (CanMove)
		{
			SNAPLabel.alpha = Mathf.MoveTowards(SNAPLabel.alpha, 1, Time.deltaTime * .2f);

			HurryTimer += Time.deltaTime;

			if (HurryTimer > 40 || transform.position.y < -.1f ||
				StudentManager.MaleLockerRoomArea.bounds.Contains(transform.position))
			{
				Teleport();

				HurryTimer = 0;
				Static.Fade = 0;
				StaticNoise.volume = 0;
			}
			else if (HurryTimer > 30)
			{
				StaticNoise.volume += Time.deltaTime * .1f;
				Static.Fade += Time.deltaTime * .1f;
			}

			UpdateMovement();
		}
		else
		{
			if (Attacking)
			{
				SNAPLabel.alpha = 0;

				if (MyAnim[AttackAnims[AttackID]].speed == 0)
				{
					MyAnim[AttackAnims[AttackID]].speed = 1;
				}

				//Debug.Log(AttackAnims[AttackID] + "'s time is: " + MyAnim[AttackAnims[AttackID]].time);

				if (MyAnim[AttackAnims[AttackID]].time >= MyAnim[AttackAnims[AttackID]].length)
				{
					//If we haven't attacked 5 times yet...
					if (Attacks < 5)
					{
						ChooseAttack();
					}
					//If we're ready to stop attacking...
					else
					{
						MainCamera.transform.parent = transform;
						MainCamera.transform.localPosition = new Vector3(.25f, 1.546664f, -.5473595f);
						MainCamera.transform.localEulerAngles = new Vector3(15, 0, 0);

						SetGlitches(true);
						GlitchTimeLimit = .5f;

						TargetStudent.Student.BecomeRagdoll();

						AttacksUsed[1] = false;
						AttacksUsed[2] = false;
						AttacksUsed[3] = false;
						AttacksUsed[4] = false;
						AttacksUsed[5] = false;

						Attacking = false;
						CanMove = true;

						Attacks = 0;
					}
				}
				else
				{
					if (!Glitch1.enabled)
					{
						if (BloodSpawned < 2)
						{
							//Bash
							if (AttackID == 1)
							{
								if (BloodSpawned == 0)
								{
									if (MyAnim[AttackAnims[AttackID]].time > .25f)
									{
										Instantiate(BloodEffect, RightHand.position, Quaternion.identity);
										MyAudio.Stop();
										BloodSpawned++;
									}
								}
								else
								{
									if (MyAnim[AttackAnims[AttackID]].time > 1)
									{
										Instantiate(BloodEffect, LeftHand.position, Quaternion.identity);
										BloodSpawned++;
									}
								}
							}
							//Gouge
							else if (AttackID == 2)
							{
								if (MyAnim[AttackAnims[AttackID]].time > 1)
								{
									Instantiate(BloodEffect, RightHand.position, Quaternion.identity);
									BloodSpawned += 2;
									MyAudio.Stop();
								}
							}
							//Impale
							else if (AttackID == 3)
							{
								if (MyAnim[AttackAnims[AttackID]].time > .5f)
								{
									Instantiate(BloodEffect, RightHand.position, Quaternion.identity);
									BloodSpawned += 2;
									MyAudio.Stop();
								}
							}
							//Neck Snap
							else if (AttackID == 4)
							{
								if (MyAnim[AttackAnims[AttackID]].time > .5f)
								{
									MyAudio.Stop();
								}
							}
							//Stomp
							else if (AttackID == 5)
							{
								if (BloodSpawned == 0)
								{
									if (MyAnim[AttackAnims[AttackID]].time > .25f)
									{
										Instantiate(BloodEffect, RightFoot.position, Quaternion.identity);
										MyAudio.Stop();
										BloodSpawned++;
									}
								}
								else
								{
									if (MyAnim[AttackAnims[AttackID]].time > .9f)
									{
										Instantiate(BloodEffect, RightFoot.position, Quaternion.identity);
										BloodSpawned++;
									}
								}
							}
						}
					}
				}
			}
			else if (KillingSenpai)
			{
				CompressionFX.Parasite = Mathf.MoveTowards(CompressionFX.Parasite, 0, Time.deltaTime);
				Distorted.Distortion = Mathf.MoveTowards(Distorted.Distortion, 0, Time.deltaTime);

				StaticNoise.volume -= Time.deltaTime * .5f;
				Static.Fade = Mathf.MoveTowards(Static.Fade, 0, Time.deltaTime * .5f);

				Jukebox.volume -= Time.deltaTime * .5f;
				SnapStatic.volume -= Time.deltaTime * .5f * .2f;
				SNAPLabel.alpha = Mathf.MoveTowards(SNAPLabel.alpha, 0, Time.deltaTime * .5f);

				SnapVoice.volume -= Time.deltaTime;

				///////////////////////////////
				///// MOVING YANDERE-CHAN /////
				///////////////////////////////

				Quaternion targetRotation = Quaternion.LookRotation(
					TargetStudent.transform.position - this.transform.position);
				
				transform.rotation = Quaternion.Slerp(
					transform.rotation, targetRotation, Time.deltaTime);

				transform.position = Vector3.MoveTowards(
					transform.position,
					TargetStudent.transform.position + (TargetStudent.transform.forward * 1),
					Time.deltaTime);

				/////////////////////////////
				///// MOVING THE CAMERA /////
				/////////////////////////////

				Speed += Time.deltaTime;

				if (AttackPhase < 3)
				{
					MainCamera.transform.position = Vector3.Lerp(
						MainCamera.transform.position,
						FinalSnapPOV.position,
						Time.deltaTime * Speed * .33333f);
					
					MainCamera.transform.rotation = Quaternion.Slerp(
						MainCamera.transform.rotation,
						FinalSnapPOV.rotation,
						Time.deltaTime * Speed * .33333f);
				}
				else
				{
					MainCamera.transform.position = Vector3.Lerp(
						MainCamera.transform.position,
						SuicidePOV.position,
						Time.deltaTime * Speed * .1f);

					MainCamera.transform.rotation = Quaternion.Slerp(
						MainCamera.transform.rotation,
						SuicidePOV.rotation,
						Time.deltaTime * Speed * .1f);

					if (Whisper)
					{
						//Rumble is the chorus of "Do It!" voices.
						Rumble.volume = Mathf.MoveTowards(Rumble.volume, .5f, Time.deltaTime * .05f);

						WhisperTimer += Time.deltaTime;

						if (WhisperTimer > .5f)
						{
							WhisperTimer = 0;

							int WhisperID = Random.Range(1, Whispers.Length);

							//Debug.Log("10 - (10 * (1 - Rumble.volume) is: " + (10 - (10 * Rumble.volume * 2)));

							AudioSource.PlayClipAtPoint(Whispers[WhisperID], MainCamera.transform.position + new Vector3(
								11 - (10 * Rumble.volume * 2),
								11 - (10 * Rumble.volume * 2),
								11 - (10 * Rumble.volume * 2)));

							NewDoIt = Instantiate(DoIt, SNAPLabel.parent.transform.position, Quaternion.identity);

							NewDoIt.transform.parent = SNAPLabel.parent.transform;

							NewDoIt.transform.localScale = new Vector3(1, 1, 1);

							NewDoIt.transform.localPosition = new Vector3(
								Random.Range(-700.0f, 700.0f),
								Random.Range(-450.0f, 450.0f),
								0);

							NewDoIt.transform.localEulerAngles = new Vector3(
								Random.Range(-15.0f, 15.0f),
								Random.Range(-15.0f, 15.0f),
								Random.Range(-15.0f, 15.0f));
						}
					}
				}

				if (AttackPhase == 0)
				{
					if (MyAnim["f02_snapKill_00"].time > MyAnim["f02_snapKill_00"].length * .2f)
					{
						Instantiate(BloodEffect, Knife.transform.position, Quaternion.identity);
						AttackPhase++;
					}
				}
				else if (AttackPhase == 1)
				{
					if (MyAnim["f02_snapKill_00"].time > MyAnim["f02_snapKill_00"].length * .36f)
					{
						Instantiate(BloodEffect, Knife.transform.position, Quaternion.identity);
						AttackPhase++;
					}
				}
				else if (AttackPhase == 2)
				{
					if (MyAnim["f02_snapKill_00"].time > 13)
					{
						//Knife.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
						MyAnim["f02_stareAtKnife_00"].layer = 100;
						MyAnim.Play("f02_stareAtKnife_00");
						MyAnim["f02_stareAtKnife_00"].weight = 0;

						Whisper = true;
						Rumble.Play();
						Speed = 0;

						AttackPhase++;
					}
				}
				else if (AttackPhase == 3)
				{
					Knife.transform.localEulerAngles = Vector3.Lerp(
						Knife.transform.localEulerAngles,
						new Vector3(0.0f, 0.0f, 0.0f),
						Time.deltaTime * Speed);

					MyAnim["f02_stareAtKnife_00"].weight = Mathf.Lerp(MyAnim["f02_stareAtKnife_00"].weight, 1, Time.deltaTime * Speed);

					if (MyAnim["f02_stareAtKnife_00"].weight > .999f)
					{
						SuicidePrompt.alpha += Time.deltaTime;

						ImpatienceTimer += Time.deltaTime;

						if (Input.GetButtonDown(InputNames.Xbox_X) || ImpatienceTimer > ImpatienceLimit)
						{
							MyAnim["f02_suicide_00"].layer = 101;
							MyAnim.Play("f02_suicide_00");
							MyAnim["f02_suicide_00"].weight = 0;
							MyAnim["f02_suicide_00"].time = 2;
							MyAnim["f02_suicide_00"].speed = 0;

							AttackPhase++;

							if (ImpatienceTimer > ImpatienceLimit)
							{
								ImpatienceLimit = 2;
								ImpatienceTimer = 0;
							}

							Taps++;
						}
					}
				}
				else if (AttackPhase == 4)
				{
					SuicidePrompt.alpha += Time.deltaTime;

					ImpatienceTimer += Time.deltaTime;

					if (Input.GetButtonDown(InputNames.Xbox_X) || ImpatienceTimer > ImpatienceLimit)
					{
						Target += .1f;
						SpeedUp = true;

						if (ImpatienceTimer > ImpatienceLimit)
						{
							ImpatienceLimit = 2;
							ImpatienceTimer = 0;
						}

						Taps++;
					}

					if (SpeedUp)
					{
						AnimSpeed += Time.deltaTime;

						if (AnimSpeed > 1)
						{
							SpeedUp = false;
						}
					}
					else
					{
						AnimSpeed = Mathf.MoveTowards(AnimSpeed, 0, Time.deltaTime);
					}

					MyAnim["f02_suicide_00"].weight = Mathf.Lerp(MyAnim["f02_suicide_00"].weight, Target, AnimSpeed * Time.deltaTime);

					if (MyAnim["f02_suicide_00"].weight >= 1) 
					{
						SpeedUp = false;
						AnimSpeed = 0;
						Target = 2;

						AttackPhase++;
					}
				}
				else if (AttackPhase == 5)
				{
					ImpatienceTimer += Time.deltaTime;

					if (Input.GetButtonDown(InputNames.Xbox_X) || ImpatienceTimer > ImpatienceLimit)
					{
						Target += .1f;
						SpeedUp = true;

						if (ImpatienceTimer > ImpatienceLimit)
						{
							ImpatienceLimit = 2;
							ImpatienceTimer = 0;
						}

						Taps++;
					}

					if (SpeedUp)
					{
						AnimSpeed += Time.deltaTime;

						if (AnimSpeed > 1)
						{
							SpeedUp = false;
						}
					}
					else
					{
						AnimSpeed = Mathf.MoveTowards(AnimSpeed, 0, Time.deltaTime);
					}

					MyAnim["f02_suicide_00"].time = Mathf.Lerp(MyAnim["f02_suicide_00"].time, Target, AnimSpeed * Time.deltaTime);

					if (MyAnim["f02_suicide_00"].time >= 3.66666f)
					{
						MyAnim["f02_suicide_00"].speed = 1;
						SuicidePrompt.alpha = 0;
						Rumble.volume = 0;
						Destroy(NewDoIt);
						Whisper = false;

						AttackPhase++;
					}
				}
				else if (AttackPhase == 6)
				{
					if (MyAnim["f02_suicide_00"].time >= MyAnim["f02_suicide_00"].length * .355f)
					{
						Instantiate(StabBloodEffect, Knife.transform.position, Quaternion.identity);
						AttackPhase++;
					}
				}
				else
				{
					if (MyAnim["f02_suicide_00"].time >= MyAnim["f02_suicide_00"].length * .475f)
					{
						MyListener.enabled = false;
						MainCamera.transform.parent = null;

						MainCamera.transform.position = new Vector3(0, 2025, -11);
						MainCamera.transform.eulerAngles = new Vector3(0, 0, 0);

						if (MyAnim["f02_suicide_00"].time >= MyAnim["f02_suicide_00"].length)
						{
							SceneManager.LoadScene(SceneNames.LoadingScene);
						}
					}
				}

				/*
				if (Input.GetKeyDown("r"))
				{
					MyAnim["f02_suicide_00"].layer = 101;
					MyAnim.Play("f02_suicide_00");
					MyAnim["f02_suicide_00"].weight = 0;
					MyAnim["f02_suicide_00"].time = 2;
					MyAnim["f02_suicide_00"].speed = 0;

					SpeedUp = false;
					AnimSpeed = 0;
					Target = 0;
				}
				*/
			}
		}

		if (InputDevice.Type == InputDeviceType.MouseAndKeyboard)
		{
			SuicidePrompt.text = "F";
			SuicideSprite.enabled = false;
		}
		else
		{
			SuicidePrompt.text = "";
			SuicideSprite.enabled = true;
		}

		if (ListenTimer > 0)
		{
			ListenTimer = Mathf.MoveTowards(ListenTimer, 0, Time.deltaTime);
		}
	}

	void UpdateMovement()
	{
		MyController.Move(Physics.gravity * Time.deltaTime);

		float v = Input.GetAxis("Vertical");
		float h = Input.GetAxis("Horizontal");

		Vector3 forward = transform.TransformDirection(Vector3.forward);
		forward.y = 0.0f;
		forward = forward.normalized;

		Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);

		Vector3 targetDirection = (h * right) + (v * forward);

		if (Mathf.Abs(v) > .5f || Mathf.Abs(h) > .5f)
		{
			MyAnim[WalkAnim].speed = Mathf.Abs(v) + Mathf.Abs(h);
			if (MyAnim[WalkAnim].speed > 1){MyAnim [WalkAnim].speed = 1;}

			MyAnim.CrossFade(WalkAnim);

			MyController.Move(targetDirection * Time.deltaTime);
		}
		else
		{
			MyAnim.CrossFade(IdleAnim);
		}

		float mouseX = Input.GetAxis("Mouse X") * OptionGlobals.Sensitivity;

		if (mouseX != 0)
		{
			transform.eulerAngles = new Vector3 (
				transform.eulerAngles.x,
				transform.eulerAngles.y + (mouseX * 36 * Time.deltaTime),
				transform.eulerAngles.z);
		}

		if (Input.GetButtonDown(InputNames.Xbox_LB))
		{
			MyController.Move(targetDirection * 4);
			SetGlitches(true);
			GlitchTimeLimit = .1f;
		}
	}

	void MoveTowardsTarget(Vector3 target)
	{
		Vector3 offset = target - transform.position;
		MyController.Move(offset * (Time.deltaTime * 10.0f));
	}

	void RotateTowardsTarget(Quaternion target)
	{
		transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 10.0f);
	}

	void SetGlitches (bool State)
	{
		GlitchTimer = 0;

		Glitch1.enabled = State;
		Glitch2.enabled = State;
		//Glitch3.enabled = State;
		Glitch4.enabled = State;
		Glitch5.enabled = State;
		Glitch6.enabled = State;
		Glitch7.enabled = State;
		//Glitch8.enabled = State;
		//Glitch9.enabled = State;
		Glitch10.enabled = State;
		Glitch11.enabled = State;

		if (State)
		{
			MyAudio.clip = Buzz;
			MyAudio.volume = .5f;
			MyAudio.pitch = Random.Range(.5f, 2f);
			MyAudio.Play();
		}
	}

	public void ChooseAttack()
	{
		BloodSpawned = 0;

		SetGlitches(true);
		GlitchTimeLimit = .5f;

		AttackID = Random.Range(1, 6);

		while (AttacksUsed[AttackID])
		{
			AttackID = Random.Range(1, 6);
		}

		AttacksUsed[AttackID] = true;

		Attacks++;

		//Bash
		if (AttackID == 1)
		{
			TargetStudent.transform.position = transform.position + (transform.forward * .0001f);
			TargetStudent.transform.LookAt(transform.position);
		}
		//Gouge
		else if (AttackID == 2)
		{
			TargetStudent.transform.position = transform.position + (transform.forward * .5f);
			TargetStudent.transform.LookAt(transform.position);
		}
		//Impale
		else if (AttackID == 3)
		{
			TargetStudent.transform.position = transform.position + (transform.forward * .3f);
			TargetStudent.transform.LookAt(transform.position);
		}
		//Snap
		else if (AttackID == 4)
		{
			TargetStudent.transform.position = transform.position + (transform.forward * .3f);
			TargetStudent.transform.rotation = transform.rotation;
		}
		//Stomp
		else if (AttackID == 5)
		{
			TargetStudent.transform.position = transform.position + (transform.forward * .66666f);
			TargetStudent.transform.rotation = transform.rotation;
		}
			
		Physics.SyncTransforms();

		MyAnim.Play(AttackAnims[AttackID]);
		MyAnim[AttackAnims[AttackID]].time = 0;
		//MyAnim[AttackAnims[AttackID]].speed = 1;

		TargetStudent.MyAnim.Play(TargetStudent.AttackAnims[AttackID]);
		TargetStudent.MyAnim[TargetStudent.AttackAnims[AttackID]].time = 0;
		//TargetStudent.MyAnim[TargetStudent.AttackAnims[AttackID]].speed = 1;
	}

	public void Teleport()
	{
		if (!Armed)
		{
			bool Stop = false;

			while (!Stop)
			{
				foreach (WeaponScript weapon in Weapons)
				{
					if (weapon != null)
					{
						SetGlitches(true);
						GlitchTimeLimit = 1;
						transform.position = weapon.transform.position;

						Stop = true;
					}
				}
			}
		}
		else
		{
			Teleports++;

			SetGlitches(true);
			GlitchTimeLimit = 1;

			if (Teleports == 1)
			{
				transform.position = StudentManager.Students[1].transform.position + StudentManager.Students[1].transform.forward * 2;
				transform.LookAt(StudentManager.Students [1].transform.position);
			}
			else
			{
				transform.position = StudentManager.Students[1].transform.position + StudentManager.Students[1].transform.forward * .9f;
				transform.LookAt(StudentManager.Students [1].transform.position);
			}
		}

		Physics.SyncTransforms();
	}
}