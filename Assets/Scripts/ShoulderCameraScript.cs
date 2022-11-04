using UnityEngine;

public class ShoulderCameraScript : MonoBehaviour
{
	public PauseScreenScript PauseScreen;
	public CounselorScript Counselor;
	public YandereScript Yandere;
	public RPG_Camera RPGCamera;
	public PortalScript Portal;

	public GameObject HeartbrokenCamera;
	public GameObject HUD;

	public Transform Smartphone;
	public Transform Teacher;

	public Transform ShoulderFocus;
	public Transform ShoulderPOV;

	public Transform CameraFocus;
	public Transform CameraPOV;

	public Transform NoticedFocus;
	public Transform NoticedPOV;

	public Transform StruggleFocus;
	public Transform StrugglePOV;

	public Transform Focus;

	public Vector3 LastPosition;

	public Vector3 TeacherLossFocus;
	public Vector3 TeacherLossPOV;

	public Vector3 LossFocus;
	public Vector3 LossPOV;

	public bool GoingToCounselor = false;
	public bool ObstacleCounter = false;
	public bool AimingCamera = false;
	public bool OverShoulder = false;
	public bool Summoning = false;
	public bool LookDown = false;
	public bool Scolding = false;
	public bool Struggle = false;
	public bool Counter = false;
	public bool Noticed = false;
	public bool Spoken = false;
	public bool Skip = false;

	public AudioClip StruggleLose;
	public AudioClip Slam;

	public float NoticedHeight = 0.0f;
	public float NoticedTimer = 0.0f;
	public float NoticedSpeed = 0.0f;
	public float ReturnSpeed = 10.0f;

	public float Height = 0.0f;
	public float Shake = 0.0f;

	public float PullBackTimer = 0.0f;
	public float Timer = 0.0f;

	public int NoticedLimit = 0;
	public int Phase = 0;

	void LateUpdate()
	{
		if (!this.PauseScreen.Show)
		{
			if (this.OverShoulder)
			{
				if (this.RPGCamera.enabled)
				{
					this.ShoulderFocus.position = this.RPGCamera.cameraPivot.position;

					this.LastPosition = this.transform.position;

					this.RPGCamera.enabled = false;
				}

				if (this.Yandere.TargetStudent.Counselor)
				{
					this.transform.position = Vector3.Lerp(
						this.transform.position, this.ShoulderPOV.position + new Vector3(0, -.49f, 0), Time.deltaTime * 10.0f);
				}
				else
				{
					this.transform.position = Vector3.Lerp(
						this.transform.position, this.ShoulderPOV.position, Time.deltaTime * 10.0f);
				}

				this.ShoulderFocus.position = Vector3.Lerp(
					this.ShoulderFocus.position,
					this.Yandere.TargetStudent.transform.position + (Vector3.up * this.Height),
					Time.deltaTime * 10.0f);

				this.transform.LookAt(this.ShoulderFocus);
			}
			else if (this.AimingCamera)
			{
				this.transform.position = this.CameraPOV.position;
				this.transform.LookAt(this.CameraFocus);
			}
			else if (this.Noticed)
			{
				if (this.NoticedTimer == 0.0f)
				{
					this.GetComponent<Camera>().cullingMask &= ~(1 << 13);

					StudentScript student = this.Yandere.Senpai.GetComponent<StudentScript>();

					if (student.Teacher)
					{
						this.GoingToCounselor = true;

						this.NoticedHeight = 1.60f;
						this.NoticedLimit = 6;
					}

					if (student.Club == ClubType.Council)
					{
						this.GoingToCounselor = true;

						this.NoticedHeight = 1.375f;
						this.NoticedLimit = 6;
					}
					else if (student.Witnessed == StudentWitnessType.Stalking)
					{
						this.NoticedHeight = 1.481275f;
						this.NoticedLimit = 7;
					}
					else
					{
						this.NoticedHeight = 1.375f;
						this.NoticedLimit = 6;
					}

					this.NoticedPOV.position = this.Yandere.Senpai.position +
						this.Yandere.Senpai.forward + (Vector3.up * this.NoticedHeight);
					this.NoticedPOV.LookAt(this.Yandere.Senpai.position +
						(Vector3.up * this.NoticedHeight));
					this.NoticedFocus.position = this.transform.position + this.transform.forward;
					this.NoticedSpeed = 10.0f;
				}

				this.NoticedTimer += Time.deltaTime;

				if (this.Phase == 1)
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						if (!this.Yandere.Attacking)
						{
							this.Yandere.transform.rotation = Quaternion.LookRotation(
								this.Yandere.Senpai.position - this.Yandere.transform.position);

							this.NoticedTimer += 10;
						}
					}

					this.NoticedFocus.position = Vector3.Lerp(
						this.NoticedFocus.position,
						this.Yandere.Senpai.position + (Vector3.up * this.NoticedHeight),
						Time.deltaTime * 10.0f);

					this.NoticedPOV.Translate(Vector3.forward * Time.deltaTime * -0.075f);

					if (this.NoticedTimer > 1.0f)
					{
						if (!this.Spoken)
						{
							if (!this.Yandere.Senpai.GetComponent<StudentScript>().Teacher)
							{
								this.Yandere.Senpai.GetComponent<StudentScript>().DetermineSenpaiReaction();
								this.Spoken = true;
							}
						}
					}

					if (this.NoticedTimer > this.NoticedLimit || this.Skip)
					{
						this.Yandere.Senpai.GetComponent<StudentScript>().Character.SetActive(false);

						this.GetComponent<Camera>().cullingMask |= 1 << 13;

						this.NoticedPOV.position = this.Yandere.transform.position +
							this.Yandere.transform.forward + (Vector3.up * 1.375f);

						this.NoticedPOV.LookAt(this.Yandere.transform.position + (Vector3.up * 1.375f));

						this.NoticedFocus.position = this.Yandere.transform.position + (Vector3.up * 1.375f);
						this.transform.position = this.NoticedPOV.position;

						this.NoticedTimer = this.NoticedLimit;
						this.Phase = 2;

						if (this.GoingToCounselor)
						{
							this.Yandere.CharacterAnimation.CrossFade("f02_disappointed_00");
						}
						else
						{
							this.Yandere.CharacterAnimation["f02_sadEyebrows_00"].weight = 1.0f;

							this.Yandere.CharacterAnimation.CrossFade("f02_whimper_00");
							//this.Yandere.CharacterAnimation.CrossFade("f02_scaredIdle_00");
							this.Yandere.Subtitle.UpdateLabel(SubtitleType.YandereWhimper, 1, 3.50f);

							Debug.Log("Yandere-chan shoulder be whimpering now.");
						}
					}
				}
				else if (this.Phase == 2)
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.NoticedTimer += 10;
					}

					if (!this.GoingToCounselor)
					{
						this.Yandere.EyeShrink = Mathf.MoveTowards(this.Yandere.EyeShrink, .5f,
							Time.deltaTime * 0.125f);
					}

					this.NoticedPOV.Translate(Vector3.forward * Time.deltaTime * 0.075f);

					if (this.GoingToCounselor)
					{
						this.Yandere.CharacterAnimation.CrossFade("f02_disappointed_00");
					}
					else
					{
						this.Yandere.CharacterAnimation.CrossFade("f02_whimper_00");
						//this.Yandere.CharacterAnimation.CrossFade("f02_scaredIdle_00");

						if (this.Yandere.CharacterAnimation["f02_whimper_00"].time > 3.5f)
						{
							this.Yandere.CharacterAnimation["f02_whimper_00"].speed -= Time.deltaTime;
						}
					}

					if (this.NoticedTimer > (this.NoticedLimit + 4))
					{
						if (!this.GoingToCounselor)
						{
							this.NoticedPOV.Translate(Vector3.back * 2.0f);

							this.NoticedPOV.transform.position = new Vector3(
								this.NoticedPOV.transform.position.x,
								this.Yandere.transform.position.y + 1.0f,
								this.NoticedPOV.transform.position.z);

							this.NoticedSpeed = 1.0f;

							this.Yandere.Character.GetComponent<Animation>().CrossFade(AnimNames.FemaleDown22);
							this.HeartbrokenCamera.SetActive(true);
                            this.Yandere.Police.Invalid = true;
                            this.Yandere.Collapse = true;
							this.Phase = 3;
						}
						else
						{
							this.Yandere.Police.Darkness.enabled = true;
							this.Yandere.HUD.enabled = true;
							this.Yandere.HUD.alpha = 1;

							if (this.Yandere.Police.Timer == 300 && (this.Yandere.Police.Corpses - this.Yandere.Police.HiddenCorpses) <= 0)
							{
								this.HUD.SetActive(false);
							}

							this.Phase = 4;
						}
					}
				}
				else if (this.Phase == 3)
				{
					this.NoticedFocus.transform.position = new Vector3(
						this.NoticedFocus.transform.position.x,
						Mathf.Lerp(this.NoticedFocus.transform.position.y, this.Yandere.transform.position.y + 1.0f, Time.deltaTime),
						this.NoticedFocus.transform.position.z);
				}
				else if (this.Phase == 4)
				{
					this.Yandere.Police.Darkness.color += new Color(0, 0, 0, Time.deltaTime);

					this.NoticedPOV.Translate(Vector3.forward * Time.deltaTime * 0.075f);

					if (this.Yandere.Police.Darkness.color.a >= 1)
					{
						if (this.Yandere.Police.Timer != 300 || this.Yandere.Police.Corpses - this.Yandere.Police.HiddenCorpses > 0)
						{
                            Debug.Log("Ending day instead of going to counselor.");

                            this.HUD.SetActive(true);
                            this.Portal.EndDay();
                            this.enabled = false;
						}
						else
						{
							if (this.Yandere.Mask != null)
							{
								this.Yandere.Mask.Drop();
							}

							this.Yandere.StudentManager.PreventAlarm();

							this.Counselor.Crime = this.Yandere.Senpai.GetComponent<StudentScript>().Witnessed;
							this.Counselor.MyAnimation.Play("CounselorArmsCrossed");
							this.Counselor.Laptop.SetActive(false);
							this.Counselor.Interrogating = true;
							this.Counselor.LookAtPlayer = true;
							this.Counselor.Stern = true;
							this.Counselor.Timer = 0;

							this.transform.Translate(Vector3.forward * -1);

							this.Yandere.Senpai.GetComponent<StudentScript>().Character.SetActive(true);
							this.Yandere.transform.localEulerAngles = new Vector3(0, -90, 0);
							this.Yandere.transform.position = new Vector3(-27.51f, 0, 12);
							this.Yandere.Police.Darkness.color = new Color(0, 0, 0, 1);
							this.Yandere.CharacterAnimation.Play("f02_sit_00");
							this.Yandere.Noticed = false;
							this.Yandere.Sanity = 100.0f;

							Physics.SyncTransforms();

							this.GoingToCounselor = false;
							this.enabled = false;

							this.NoticedTimer = 0;
							this.Phase = 1;
						}
					}
				}

				if (Phase < 5)
				{
					this.transform.position = Vector3.Lerp(
						this.transform.position, this.NoticedPOV.position, Time.deltaTime * this.NoticedSpeed);
					this.transform.LookAt(this.NoticedFocus);
				}
			}
			else if (this.Scolding)
			{
				if (this.Timer == 0.0f)
				{
					this.NoticedHeight = 1.60f;
					this.NoticedPOV.position = this.Teacher.position +
						this.Teacher.forward + (Vector3.up * this.NoticedHeight);
					this.NoticedPOV.LookAt(this.Teacher.position + (Vector3.up * this.NoticedHeight));
					this.NoticedFocus.position = this.Teacher.position + (Vector3.up * this.NoticedHeight);
					this.NoticedSpeed = 10.0f;
				}

				this.transform.position = Vector3.Lerp(
					this.transform.position, this.NoticedPOV.position, Time.deltaTime * this.NoticedSpeed);
				this.transform.LookAt(this.NoticedFocus);

				this.Timer += Time.deltaTime;

				if (this.Timer > 6.0f)
				{
					this.Portal.ClassDarkness.enabled = true;
					this.Portal.Transition = true;
					this.Portal.FadeOut = true;
				}

				if (this.Timer > 7.0f)
				{
					this.Scolding = false;
					this.Timer = 0.0f;
				}
			}

			///////////////////////////
			///// TEACHER COUNTER /////
			///////////////////////////

			else if (this.Counter)
			{
				if (this.Timer == 0.0f)
				{
					this.StruggleFocus.position = this.transform.position + this.transform.forward;
					this.StrugglePOV.position = this.transform.position;
				}

				this.transform.position = Vector3.Lerp(
					this.transform.position, this.StrugglePOV.position, Time.deltaTime * 10.0f);
				this.transform.LookAt(this.StruggleFocus);

				this.Timer += Time.deltaTime;

				if (this.Timer > 0.50f)
				{
					if (this.Phase < 2)
					{
						this.Yandere.CameraEffects.MurderWitnessed();
						this.Yandere.Jukebox.GameOver();
						this.Phase++;
					}
				}

				if (this.Timer > 1.40f)
				{
					if (this.Phase < 3)
					{
						this.Yandere.Subtitle.UpdateLabel(SubtitleType.TeacherAttackReaction, 1, 4.0f);
						this.Phase++;
					}
				}

				if (this.Timer > 6.0f)
				{
					if (this.Yandere.Armed)
					{
						this.Yandere.EquippedWeapon.Drop();
					}
				}

				if (this.Timer > 6.66666f)
				{
					if (this.Phase < 4)
					{
						this.GetComponent<AudioSource>().PlayOneShot(this.Slam);
						this.Phase++;
					}
				}

				if (this.Timer > 10.0f)
				{
					if (this.Phase < 5)
					{
						this.HeartbrokenCamera.SetActive(true);
						this.Phase++;
					}
				}

				if (this.Timer < 5.0f)
				{
					this.StruggleFocus.localPosition = Vector3.Lerp(
						this.StruggleFocus.localPosition,
						new Vector3(0, 1.4f, .7f),
						Time.deltaTime);

					this.StrugglePOV.localPosition = Vector3.Lerp(
						this.StrugglePOV.localPosition, new Vector3(0.50f, 1.40f, 0.30f), Time.deltaTime);
				}
				else if (this.Timer < 10.0f)
				{
					if (this.Timer < 6.50f)
					{
						this.PullBackTimer = Mathf.MoveTowards(this.PullBackTimer, 1.50f, Time.deltaTime);
					}
					else
					{
						this.PullBackTimer = Mathf.MoveTowards(this.PullBackTimer, 0.0f, Time.deltaTime * (3.0f / 7.0f));
					}

					this.transform.Translate(Vector3.back * Time.deltaTime * 10.0f * this.PullBackTimer);

					this.StruggleFocus.localPosition = Vector3.Lerp(
						this.StruggleFocus.localPosition, new Vector3(0.0f, 0.30f, -0.766666f), Time.deltaTime);

					this.StrugglePOV.localPosition = Vector3.Lerp(
						this.StrugglePOV.localPosition, new Vector3(.75f, 0.30f, -0.966666f), Time.deltaTime);
				}
				else
				{
					this.StruggleFocus.localPosition = Vector3.Lerp(
						this.StruggleFocus.localPosition, new Vector3(0.0f, 0.30f, -0.766666f), Time.deltaTime);

					this.StrugglePOV.localPosition = Vector3.Lerp(
						this.StrugglePOV.localPosition, new Vector3(.75f, 0.30f, -0.966666f), Time.deltaTime);
				}
			}

			///////////////////////////////////////
			///// MYSTERIOUS OBSTACLE COUNTER /////
			///////////////////////////////////////

			else if (this.ObstacleCounter)
			{
				StruggleFocus.position += new Vector3 (Shake * Random.Range (-1.0f, 1.0f), Shake * Random.Range (-.5f, .5f), Shake * Random.Range (-1.0f, 1.0f));
				Shake = Mathf.Lerp (Shake, 0, Time.deltaTime * 5);

				if (this.Yandere.Armed)
				{
					this.Yandere.EquippedWeapon.transform.localEulerAngles =
						new Vector3(0.0f, 180.0f, 0.0f);
				}

				if (this.Timer == 0.0f)
				{
					this.StruggleFocus.position = this.transform.position + this.transform.forward;
					this.StrugglePOV.position = this.transform.position;
				}

				this.transform.position = Vector3.Lerp(
					this.transform.position, this.StrugglePOV.position, Time.deltaTime * 10.0f);
				this.transform.LookAt(this.StruggleFocus);

				this.Timer += Time.deltaTime;

				if (this.Timer > 0.50f)
				{
					if (this.Phase < 2)
					{
						this.Yandere.CameraEffects.MurderWitnessed();
						this.Yandere.Jukebox.GameOver();
						this.Phase++;
					}
				}

				if (this.Timer > 7.6f)
				{
					if (this.Phase < 3)
					{
						if (this.Yandere.Armed)
						{
							this.Yandere.EquippedWeapon.Drop();
						}

						this.Shake += 0.2f;
						Phase++;
					}
				}

				if (this.Timer > 10)
				{
					if (this.Phase < 4)
					{
						this.Shake += 0.2f;
						Phase++;
					}
				}

				if (this.Timer > 12)
				{
					if (this.Phase < 5)
					{
						this.HeartbrokenCamera.GetComponent<Camera>().cullingMask |= (1 << 9);
						this.HeartbrokenCamera.SetActive(true);
						this.Phase++;
					}
				}

				if (this.Timer < 6)
				{
					this.StruggleFocus.localPosition = Vector3.Lerp(
						this.StruggleFocus.localPosition,
						new Vector3(-.166666f, 1.2f, .82f),
						Time.deltaTime);

					this.StrugglePOV.localPosition = Vector3.Lerp(
						this.StrugglePOV.localPosition, new Vector3(0.66666f, 1.2f, .82f), Time.deltaTime);
				}
				else if (this.Timer < 8.5f)
				{
					this.StruggleFocus.localPosition = Vector3.Lerp(
						this.StruggleFocus.localPosition,
						new Vector3(-.166666f, 1.2f, .82f),
						Time.deltaTime);

					this.StrugglePOV.localPosition = Vector3.Lerp(
						this.StrugglePOV.localPosition, new Vector3(2, 1.2f, .82f), Time.deltaTime);
				}
				else if (this.Timer < 12)
				{
					this.StruggleFocus.localPosition = Vector3.Lerp(
						this.StruggleFocus.localPosition, new Vector3(-.85f, 0.5f, 1.75f), Time.deltaTime * 2);

					this.StrugglePOV.localPosition = Vector3.Lerp(
						this.StrugglePOV.localPosition, new Vector3(-.85f, 0.5f, 3), Time.deltaTime * 2);
				}
				else
				{
					this.StruggleFocus.localPosition = Vector3.Lerp(
						this.StruggleFocus.localPosition, new Vector3(-.85f, 1.1f, 1.75f), Time.deltaTime * 2);

					this.StrugglePOV.localPosition = Vector3.Lerp(
						this.StrugglePOV.localPosition, new Vector3(-.85f, 1, 4), Time.deltaTime * 2);
				}
			}
			else if (this.Struggle)
			{
				this.transform.position = Vector3.Lerp(
					this.transform.position, this.StrugglePOV.position, Time.deltaTime * 10.0f);
				this.transform.LookAt(this.StruggleFocus);

				if (this.Yandere.Lost)
				{
					this.StruggleFocus.localPosition = Vector3.MoveTowards(
						this.StruggleFocus.localPosition, this.LossFocus, Time.deltaTime);
					this.StrugglePOV.localPosition = Vector3.MoveTowards(
						this.StrugglePOV.localPosition, this.LossPOV, Time.deltaTime);

					if (this.Timer == 0.0f)
					{
						AudioSource audioSource = this.GetComponent<AudioSource>();
						audioSource.clip = this.StruggleLose;
						audioSource.Play();
					}

					this.Timer += Time.deltaTime;

					if (this.Timer < 3.0f)
					{
						this.transform.Translate(
							Vector3.back * (Time.deltaTime * 10.0f * this.Timer * (3.0f - this.Timer)));
					}
					else
					{
						if (!this.HeartbrokenCamera.activeInHierarchy)
						{
							this.HeartbrokenCamera.SetActive(true);
							this.Yandere.Jukebox.GameOver();
							this.enabled = false;
						}
					}
				}
			}
			else if (this.Yandere.Attacked)
			{
				this.Focus.transform.parent = null;
				this.Focus.transform.position = Vector3.Lerp(
					this.Focus.transform.position, this.Yandere.Hips.position, Time.deltaTime);

				this.transform.LookAt(this.Focus);
			}
			else if (this.LookDown)
			{
				this.Timer += Time.deltaTime;

				if (this.Timer < 5.0f)
				{
					this.transform.position = Vector3.Lerp(
						this.transform.position,
						this.Yandere.Hips.position + (Vector3.up * 3.0f) + (Vector3.right * 0.10f),
						Time.deltaTime * this.Timer);

					this.Focus.transform.parent = null;
					this.Focus.transform.position = Vector3.Lerp(
						this.Focus.transform.position, this.Yandere.Hips.position, Time.deltaTime * this.Timer);

					this.transform.LookAt(this.Focus);
				}
				else
				{
					if (!this.HeartbrokenCamera.activeInHierarchy)
					{
						this.HeartbrokenCamera.SetActive(true);
						this.Yandere.Jukebox.GameOver();
						this.enabled = false;
					}
				}
			}
			else if (this.Summoning)
			{
				if (this.Phase == 1)
				{
					this.NoticedPOV.position = this.Yandere.transform.position +
						(this.Yandere.transform.forward * 1.7f) +
						(this.Yandere.transform.right * 0.15f) +
						(Vector3.up * 1.375f);

					this.NoticedFocus.position = this.transform.position + transform.forward;
					this.NoticedSpeed = 10.0f;

					this.Phase++;
				}
				else if (Phase == 2)
				{
					this.NoticedPOV.Translate(this.NoticedPOV.forward * (Time.deltaTime * -0.10f));
					this.NoticedFocus.position = Vector3.Lerp(this.NoticedFocus.position,
						this.Yandere.transform.position +
						(this.Yandere.transform.right * 0.15f) +
						(Vector3.up * 1.375f),
						Time.deltaTime * 10.0f);

					this.Timer += Time.deltaTime;

					if (this.Timer > 2.0f)
					{
						this.Yandere.Stand.Spawn();

						this.NoticedPOV.position = this.Yandere.transform.position +
							(this.Yandere.transform.forward * 2.0f) + (Vector3.up * 2.4f);

						this.Timer = 0.0f;
						this.Phase++;
					}
				}
				else if (this.Phase == 3)
				{
					this.NoticedPOV.Translate(this.NoticedPOV.forward * (Time.deltaTime * -0.10f));
					this.NoticedFocus.position = this.Yandere.transform.position + (Vector3.up * 2.4f);

					// [af] Added extra "Stand" for gameObject access (for compatibility with C#).
					this.Yandere.Stand.Stand.SetActive(true);

					this.Timer += Time.deltaTime;

					if (this.Timer > 5.0f)
					{
						this.Phase++;
					}
				}
				else if (this.Phase == 4)
				{
					this.Yandere.Stand.transform.localPosition = new Vector3(
						this.Yandere.Stand.transform.localPosition.x,
						0.0f,
						this.Yandere.Stand.transform.localPosition.z);

					//this.Yandere.StudentManager.RestoreStudents();
					this.Yandere.Jukebox.PlayJojo();
					this.Yandere.Talking = true;
					this.Summoning = false;

					this.Timer = 0.0f;
					this.Phase = 1;
				}

				this.transform.position = Vector3.Lerp(this.transform.position,
					this.NoticedPOV.position, Time.deltaTime * this.NoticedSpeed);
				this.transform.LookAt(this.NoticedFocus);
			}
			else
			{
				if (this.Yandere.Talking || this.Yandere.Won)
				{
					if (!this.RPGCamera.enabled)
					{
						this.Timer += Time.deltaTime;

						if (this.Timer < 0.50f)
						{
							this.transform.position = Vector3.Lerp(
								this.transform.position, this.LastPosition, Time.deltaTime * ReturnSpeed);

							if (this.Yandere.Talking)
							{
								this.ShoulderFocus.position = Vector3.Lerp(
									this.ShoulderFocus.position, this.RPGCamera.cameraPivot.position, Time.deltaTime * ReturnSpeed);
								this.transform.LookAt(this.ShoulderFocus);
							}
							else
							{
								this.StruggleFocus.position = Vector3.Lerp(
									this.StruggleFocus.position, this.RPGCamera.cameraPivot.position, Time.deltaTime * ReturnSpeed);
								this.transform.LookAt(this.StruggleFocus);
							}
						}
						else
						{
							this.RPGCamera.enabled = true;

							this.Yandere.MyController.enabled = true;
							this.Yandere.Talking = false;

							if (!this.Yandere.Sprayed)
							{
								this.Yandere.CanMove = true;
							}

							this.Yandere.Pursuer = null;
							this.Yandere.Chased = false;
							this.Yandere.Won = false;
							this.Timer = 0.0f;
						}
					}
				}
			}
		}
	}

	public void YandereNo()
	{
		AudioSource audioSource = this.GetComponent<AudioSource>();
		audioSource.clip = this.StruggleLose;
		audioSource.Play();
	}

	public void GameOver()
	{
        this.NoticedPOV.parent = this.Yandere.transform;
        this.NoticedFocus.parent = this.Yandere.transform;

        this.NoticedFocus.localPosition = new Vector3(0, 1, 0);
        this.NoticedPOV.localPosition = new Vector3(0, 1, 2);
        this.NoticedPOV.LookAt(this.NoticedFocus.position);

        this.Yandere.Character.GetComponent<Animation>().CrossFade(AnimNames.FemaleDown22);
		this.HeartbrokenCamera.SetActive(true);
        this.Yandere.RPGCamera.enabled = false;
        this.Yandere.Collapse = true;
        this.Yandere.HUD.alpha = 0;

        this.Yandere.StudentManager.Students[1].Pathfinding.canSearch = false;
        this.Yandere.StudentManager.Students[1].Pathfinding.canMove = false;
        this.Yandere.StudentManager.Students[1].Fleeing = false;
	}
}