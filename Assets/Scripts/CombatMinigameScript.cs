using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMinigameScript : MonoBehaviour
{
	public UISprite[] ButtonPrompts;
	public UISprite Circle;
	public UISprite BG;

	public GameObject HitEffect;

	public PracticeWindowScript PracticeWindow;
	public StudentScript Delinquent;
	public YandereScript Yandere;

	public Transform CombatTarget;
	public Transform MainCamera;
	public Transform Midpoint;

	public Vector3 CameraTarget;
	public Vector3 CameraStart;
	public Vector3 StartPoint;

	public UITexture RedVignette;
	public UILabel Label;

	public string CurrentButton;

	public float SlowdownFactor;
	public float ShakeFactor;
	public float Difficulty;
	public float StartTime;
	public float Strength;
	public float Shake;
	public float Timer;

	public bool KnockedOut;
	public bool Practice;
	public bool Success;
	public bool Zoom;

	public string Prefix;

	public int ButtonID;
	public int Strike;
	public int Phase;
	public int Path;

	public AudioSource MyVocals;
	public AudioSource MyAudio;

	public AudioClip[] CombatSFX;
	public AudioClip[] Vocals;

	void Start()
	{
		RedVignette.color = new Color(1, 1, 1, 0);

		ButtonPrompts[1].enabled = false;
		ButtonPrompts[2].enabled = false;
		ButtonPrompts[3].enabled = false;
		ButtonPrompts[4].enabled = false;

        ButtonPrompts[1].alpha = 0;
        ButtonPrompts[2].alpha = 0;
        ButtonPrompts[3].alpha = 0;
        ButtonPrompts[4].alpha = 0;

        Circle.enabled = false;
		BG.enabled = false;
	}

	public void StartCombat()
	{
		StartPoint = MainCamera.transform.position;
		Midpoint.transform.position = MainCamera.transform.position + MainCamera.transform.forward;
		MainCamera.transform.parent = CombatTarget;
		Yandere.RPGCamera.enabled = false;
		Zoom = true;

		if (Delinquent.Male)
		{
			Prefix = "";
		}
		else
		{
			Prefix = "Female_";
		}

		if (!Practice)
		{
			Difficulty = 1;
		}
		else
		{
			Delinquent.MyWeapon.GetComponent<Rigidbody>().isKinematic = true;
			Delinquent.MyWeapon.GetComponent<Rigidbody>().useGravity = false;
		}
	}

	void Update ()
	{
		#if UNITY_EDITOR

		if (Input.GetKeyDown("z")){Yandere.Health = 0;}

		if (Input.GetKeyDown("h")){Yandere.Health = 10;}

		if (Input.GetKeyDown("space") && Delinquent != null){StartCombat();}

		#endif

		if (Zoom)
		{
			MainCamera.transform.localPosition = Vector3.Lerp(MainCamera.transform.localPosition, new Vector3(1.5f, .25f, -.5f), Time.deltaTime * 2);
			RedVignette.color = Vector4.Lerp(RedVignette.color, new Color(1, 1, 1, 1 - ((Yandere.Health * 1.0f) / 10)), Time.deltaTime);

			if (Timer < 1)
			{
				Delinquent.MoveTowardsTarget(this.Yandere.transform.position + this.Yandere.transform.forward);
			}

			Timer += Time.deltaTime;

			AdjustMidpoint();

			if (Timer > 1.5f)
			{
				Debug.Log(this.name + " is being instructed to perform the first combat animation of the combat minigame.");

				Delinquent.CharacterAnimation.CrossFade(Prefix + "Delinquent_CombatA");
				Yandere.CharacterAnimation.CrossFade("Yandere_CombatA");

				CameraStart = MainCamera.localPosition;

				Label.text = "State: A";

				Zoom = false;

				Timer = 0;
				Phase = 1;
				Path = 1;

				MyAudio.clip = CombatSFX[Phase];
				MyAudio.Play();

				MyVocals.clip = Vocals[Phase];
				MyVocals.Play();
			}
		}

		if (this.Phase > 0)
		{
			MainCamera.position += new Vector3 (Shake * Random.Range (-1.0f, 1.0f), Shake * Random.Range (-1.0f, 1.0f), Shake * Random.Range (-1.0f, 1.0f));
			Shake = Mathf.Lerp (Shake, 0, Time.deltaTime * 10);
			AdjustMidpoint();
		}

		if (ButtonID > 0)
		{
			Timer += Time.deltaTime;
			Circle.fillAmount = 1 - (Timer / (.33333f));

			if ((Input.GetButtonDown(InputNames.Xbox_A) && (CurrentButton != "A")) ||
				(Input.GetButtonDown(InputNames.Xbox_B) && (CurrentButton != "B")) ||
				(Input.GetButtonDown(InputNames.Xbox_X) && (CurrentButton != "X")) ||
				(Input.GetButtonDown(InputNames.Xbox_Y) && (CurrentButton != "Y")))
			{
				Time.timeScale = 1;
				MyVocals.pitch = 1;
				MyAudio.pitch = 1;
				DisablePrompts();
				Phase++;
			}
			else if (Input.GetButtonDown(CurrentButton))
			{
				Success = true;
			}
		}

		////////////////////
		///// PATH ONE /////
		////////////////////

		if (Path == 1)
		{
			if (!Delinquent.CharacterAnimation.IsPlaying(Prefix + "Delinquent_CombatA"))
			{
				Delinquent.CharacterAnimation.CrossFade(Prefix + "Delinquent_CombatA");
				Delinquent.CharacterAnimation[Prefix + "Delinquent_CombatA"].time = Yandere.CharacterAnimation["Yandere_CombatA"].time;
			}

			MainCamera.localPosition = Vector3.Lerp(MainCamera.localPosition, CameraStart, Time.deltaTime);

			if (Phase == 1)
			{
				if (Yandere.CharacterAnimation["Yandere_CombatA"].time > 1)
				{
					StartTime = Yandere.CharacterAnimation["Yandere_CombatA"].time - 1;
					ChooseButton();
					Slowdown();
					Phase++;
				}
			}
			else if (Phase == 2)
			{
				if (Yandere.CharacterAnimation["Yandere_CombatA"].time > 1.33333f)
				{
					Time.timeScale = 1;
					MyVocals.pitch = 1;
					MyAudio.pitch = 1;
					DisablePrompts();
					Phase++;
				}
				else
				{
					if (Success)
					{
						Delinquent.CharacterAnimation[Prefix + "Delinquent_CombatB"].time = Delinquent.CharacterAnimation[Prefix + "Delinquent_CombatA"].time;
						Yandere.CharacterAnimation["Yandere_CombatB"].time = Yandere.CharacterAnimation["Yandere_CombatA"].time;

						Delinquent.CharacterAnimation.Play(Prefix + "Delinquent_CombatB");
						Yandere.CharacterAnimation.Play("Yandere_CombatB");

						Label.text = "State: B";
						Time.timeScale = 1;
						MyAudio.pitch = 1;
						DisablePrompts();
						Strike = 0;
						Path = 2;
						Phase++;

						MyAudio.clip = CombatSFX[Path];
						MyAudio.time = Yandere.CharacterAnimation["Yandere_CombatB"].time;
						MyAudio.Play();

						MyVocals.clip = Vocals[Path];
						MyVocals.time = Yandere.CharacterAnimation["Yandere_CombatB"].time + .5f;
						MyVocals.Play();
					}
				}
			}
			else if (Phase == 3)
			{
				if (Strike < 1)
				{
					if (Yandere.CharacterAnimation["Yandere_CombatA"].time > 1.66666f)
					{
						Instantiate(HitEffect, Yandere.LeftArmRoll.position, Quaternion.identity);

						Shake += ShakeFactor;
						Strike++;
						Yandere.Health--;
						RedVignette.color = new Color(1, 1, 1, 1 - ((Yandere.Health * 1.0f) / 10));
					}
				}
				else if (Strike < 2)
				{
					if (Yandere.CharacterAnimation["Yandere_CombatA"].time > 2.5f)
					{
						Instantiate(HitEffect, Yandere.RightArmRoll.position, Quaternion.identity);

						Shake += ShakeFactor;
						Strike++;
						Yandere.Health--;

						if (Yandere.Health < 0)
						{
							Yandere.Health = 0;
						}

						RedVignette.color = new Color(1, 1, 1, 1 - ((Yandere.Health * 1.0f) / 10));

						if (!Practice)
						{
							Yandere.MyRenderer.materials[2].SetFloat("_BlendAmount1", (1 - ((Yandere.Health * 1.0f) / 10)));
						}

						if (Yandere.Health < 1)
						{
							//If the delinquent is willing to spare Yandere-chan...
							if (!Delinquent.WitnessedMurder && !Delinquent.WitnessedCorpse)
							{
								Delinquent.CharacterAnimation[Prefix + "Delinquent_CombatF"].time = Delinquent.CharacterAnimation[Prefix + "Delinquent_CombatA"].time;
								Yandere.CharacterAnimation["Yandere_CombatF"].time = Yandere.CharacterAnimation["Yandere_CombatA"].time;

								Delinquent.CharacterAnimation.CrossFade(Prefix + "Delinquent_CombatF");
								Yandere.CharacterAnimation.CrossFade("Yandere_CombatF");

								Shake += ShakeFactor;

								Label.text = "State: F";
								Time.timeScale = 1;
								MyVocals.pitch = 1;
								MyAudio.pitch = 1;
								DisablePrompts();
								Timer = 0;
								Path = 6;
								Phase++;

								MyAudio.clip = CombatSFX[Path];
								MyAudio.time = Yandere.CharacterAnimation["Yandere_CombatF"].time;
								MyAudio.Play();

								MyVocals.clip = Vocals[Path];
								MyVocals.time = Yandere.CharacterAnimation["Yandere_CombatF"].time;
								MyVocals.Play();
							}
							//If the delinquent is determined to knock out Yandere-chan...
							else
							{
								Delinquent.CharacterAnimation[Prefix + "Delinquent_CombatE"].time = Delinquent.CharacterAnimation[Prefix + "Delinquent_CombatA"].time;
								Yandere.CharacterAnimation["Yandere_CombatE"].time = Yandere.CharacterAnimation["Yandere_CombatA"].time;

								Delinquent.CharacterAnimation.CrossFade(Prefix + "Delinquent_CombatE");
								Yandere.CharacterAnimation.CrossFade("Yandere_CombatE");

								CameraTarget = MainCamera.position + new Vector3(0, 1, 0);
								MainCamera.parent = null;
								Shake += ShakeFactor;
								KnockedOut = true;

								Label.text = "State: E";
								Time.timeScale = 1;
								MyVocals.pitch = 1;
								MyAudio.pitch = 1;
								DisablePrompts();
								Timer = 0;
								Path = 5;
								Phase++;

								MyAudio.clip = CombatSFX[Path];
								MyAudio.time = Yandere.CharacterAnimation["Yandere_CombatE"].time;
								MyAudio.Play();

								MyVocals.clip = Vocals[Path];
								MyVocals.time = Yandere.CharacterAnimation["Yandere_CombatE"].time;
								MyVocals.Play();
							}
						}
					}
				}

				if (Yandere.CharacterAnimation["Yandere_CombatA"].time > Yandere.CharacterAnimation["Yandere_CombatA"].length)
				{
					Delinquent.CharacterAnimation[Prefix + "Delinquent_CombatA"].time = 0;
					Yandere.CharacterAnimation["Yandere_CombatA"].time = 0;

					Label.text = "State: A";
					Strike = 0;
					Phase = 1;

					MyAudio.clip = CombatSFX[Path];
					MyAudio.time = Yandere.CharacterAnimation["Yandere_CombatA"].time;
					MyAudio.Play();

					MyVocals.clip = Vocals[Path];
					MyVocals.time = Yandere.CharacterAnimation["Yandere_CombatA"].time;
					MyVocals.Play();
				}
			}
		}

		////////////////////
		///// PATH TWO /////
		////////////////////

		else if (Path == 2)
		{
			if (Phase == 3)
			{
				if (Yandere.CharacterAnimation["Yandere_CombatB"].time > 1.833333f)
				{
					StartTime = Yandere.CharacterAnimation["Yandere_CombatB"].time - 1.833333f;
					ChooseButton();
					Slowdown();
					Phase++;
				}
			}
			else if (Phase == 4)
			{
				if (Yandere.CharacterAnimation["Yandere_CombatB"].time > 2.166666f)
				{
					Time.timeScale = 1;
					MyVocals.pitch = 1;
					MyAudio.pitch = 1;
					DisablePrompts();
					Phase++;
				}
				else
				{
					if (Success)
					{
						Delinquent.CharacterAnimation[Prefix + "Delinquent_CombatC"].time = Delinquent.CharacterAnimation[Prefix + "Delinquent_CombatB"].time;
						Yandere.CharacterAnimation["Yandere_CombatC"].time = Yandere.CharacterAnimation["Yandere_CombatB"].time;

						Delinquent.CharacterAnimation.Play(Prefix + "Delinquent_CombatC");
						Yandere.CharacterAnimation.Play("Yandere_CombatC");

						Label.text = "State: C";
						Time.timeScale = 1;
						MyVocals.pitch = 1;
						MyAudio.pitch = 1;
						DisablePrompts();
						Strike = 0;
						Path = 3;
						Phase++;

						MyAudio.clip = CombatSFX[Path];
						MyAudio.time = Yandere.CharacterAnimation["Yandere_CombatC"].time;
						MyAudio.Play();

						MyVocals.clip = Vocals[Path];
						MyVocals.time = Yandere.CharacterAnimation["Yandere_CombatC"].time;
						MyVocals.Play();
					}
				}
			}
			else if (Phase == 5)
			{
				if (Strike < 1)
				{
					if (Yandere.CharacterAnimation["Yandere_CombatB"].time > 2.66666f)
					{
						Instantiate(HitEffect, Delinquent.LeftHand.position, Quaternion.identity);

						Shake += ShakeFactor;
						Strike++;
					}
				}

				if (Yandere.CharacterAnimation["Yandere_CombatB"].time > Yandere.CharacterAnimation["Yandere_CombatB"].length)
				{
					Delinquent.CharacterAnimation.CrossFade(Prefix + "Delinquent_CombatA");
					Yandere.CharacterAnimation.CrossFade("Yandere_CombatA");

					Label.text = "State: A";
					Strike = 0;
					Phase = 1;
					Path = 1;

					MyAudio.clip = CombatSFX[Path];
					MyAudio.time = Yandere.CharacterAnimation["Yandere_CombatA"].time;
					MyAudio.Play();

					MyVocals.clip = Vocals[Path];
					MyVocals.time = Yandere.CharacterAnimation["Yandere_CombatA"].time;
					MyVocals.Play();
				}
			}
		}

		//////////////////////
		///// PATH THREE /////
		//////////////////////

		else if (Path == 3)
		{
			if (Phase == 5)
			{
				if (Strike < 1)
				{
					if (Yandere.CharacterAnimation["Yandere_CombatC"].time > 2.5f)
					{
						Instantiate(HitEffect, Yandere.RightHand.position, Quaternion.identity);

						Shake += ShakeFactor;
						Strike++;
					}
				}

				if (Yandere.CharacterAnimation["Yandere_CombatC"].time > 3.166666f)
				{
					StartTime = Yandere.CharacterAnimation["Yandere_CombatC"].time - 3.166666f;
					ChooseButton();
					Slowdown();
					Phase++;
				}
			}
			else if (Phase == 6)
			{
				if (Yandere.CharacterAnimation["Yandere_CombatC"].time > 3.5f)
				{
					DisablePrompts();
					Time.timeScale = 1;
					MyVocals.pitch = 1;
					MyAudio.pitch = 1;
					Phase++;
				}
				else
				{
					if (Success)
					{
						Delinquent.CharacterAnimation[Prefix + "Delinquent_CombatD"].time = Delinquent.CharacterAnimation[Prefix + "Delinquent_CombatC"].time;
						Yandere.CharacterAnimation["Yandere_CombatD"].time = Yandere.CharacterAnimation["Yandere_CombatC"].time;

						Delinquent.CharacterAnimation.Play(Prefix + "Delinquent_CombatD");
						Yandere.CharacterAnimation.Play("Yandere_CombatD");

						Label.text = "State: D";
						Time.timeScale = 1;
						MyVocals.pitch = 1;
						MyAudio.pitch = 1;
						DisablePrompts();
						Strike = 0;
						Path = 4;
						Phase++;

						MyAudio.clip = CombatSFX[Path];
						MyAudio.time = Yandere.CharacterAnimation["Yandere_CombatD"].time;
						MyAudio.Play();

						MyVocals.clip = Vocals[Path];
						MyVocals.time = Yandere.CharacterAnimation["Yandere_CombatD"].time;
						MyVocals.Play();
					}
				}
			}
			else if (Phase == 7)
			{
				if (Yandere.CharacterAnimation["Yandere_CombatC"].time > Yandere.CharacterAnimation["Yandere_CombatC"].length)
				{
					Delinquent.CharacterAnimation.CrossFade(Prefix + "Delinquent_CombatA");
					Yandere.CharacterAnimation.CrossFade("Yandere_CombatA");

					Label.text = "State: A";
					Strike = 0;
					Phase = 1;
					Path = 1;

					MyAudio.clip = CombatSFX[Path];
					MyAudio.time = Yandere.CharacterAnimation["Yandere_CombatA"].time;
					MyAudio.Play();

					MyVocals.clip = Vocals[Path];
					MyVocals.time = Yandere.CharacterAnimation["Yandere_CombatA"].time;
					MyVocals.Play();
				}
			}
		}

		/////////////////////////////////////////
		///// PATH FOUR - YANDERE-CHAN WINS /////
		/////////////////////////////////////////

		else if (Path == 4)
		{
			if (Phase == 7)
			{
				if (Strike < 1)
				{
					if (Yandere.CharacterAnimation["Yandere_CombatD"].time > 4f)
					{
						Instantiate(HitEffect, Yandere.RightKnee.position, Quaternion.identity);

						if (!Delinquent.WitnessedMurder && !Delinquent.WitnessedCorpse)
						{
							Delinquent.MyWeapon.transform.parent = null;
							Delinquent.MyWeapon.MyCollider.enabled = true;
							Delinquent.MyWeapon.MyCollider.isTrigger = false;
							Delinquent.MyWeapon.Prompt.enabled = true;

							Delinquent.IgnoreBlood = true;

							Rigidbody rigidBody = Delinquent.MyWeapon.GetComponent<Rigidbody>();
							rigidBody.constraints = RigidbodyConstraints.None;
							rigidBody.isKinematic = false;
							rigidBody.useGravity = true;

							if (!this.Practice)
							{
                                Delinquent.MyWeapon.DelinquentOwned = false;
                                Delinquent.MyWeapon = null;
							}
						}

						Shake += ShakeFactor;
						Strike++;
					}
				}
				else
				{
					if (Yandere.CharacterAnimation["Yandere_CombatD"].time > 5.5f)
					{
						MainCamera.transform.parent = null;
						Strength += Time.deltaTime;

						MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, StartPoint, Time.deltaTime * Strength);
						RedVignette.color = Vector4.Lerp(RedVignette.color, new Vector4(1, 0, 0, 0), Time.deltaTime * Strength);
						Zoom = false;
					}
				}

				if (Yandere.CharacterAnimation["Yandere_CombatD"].time > Yandere.CharacterAnimation["Yandere_CombatD"].length)
				{
					//Debug.Log("Player won.");

					if (Delinquent.WitnessedMurder || Delinquent.WitnessedCorpse)
					{
						Yandere.Subtitle.UpdateLabel(SubtitleType.DelinquentNoSurrender, 0, 5.0f);

						if (!Delinquent.WillChase)
						{
							Delinquent.WillChase = true;
							Yandere.Chasers++;
						}
					}
					else
					{
						if (!Practice)
						{
							Yandere.Subtitle.UpdateLabel(SubtitleType.DelinquentSurrender, 0, 5.0f);
							Delinquent.Persona = PersonaType.Loner;
						}
					}

					if (!Practice)
					{
						ScheduleBlock newBlock2 = Delinquent.ScheduleBlocks[2];
						newBlock2.destination = "Sulk";
						newBlock2.action = "Sulk";

						ScheduleBlock newBlock4 = Delinquent.ScheduleBlocks[4];
						newBlock4.destination = "Sulk";
						newBlock4.action = "Sulk";

						ScheduleBlock newBlock6 = Delinquent.ScheduleBlocks[6];
						newBlock6.destination = "Sulk";
						newBlock6.action = "Sulk";

						ScheduleBlock newBlock7 = Delinquent.ScheduleBlocks[7];
						newBlock7.destination = "Sulk";
						newBlock7.action = "Sulk";

                        if (Delinquent.Phase == 0)
                        {
                            Delinquent.Phase++;
                        }

                        Delinquent.GetDestinations();
                        Delinquent.CurrentDestination = Delinquent.Destinations[Delinquent.Phase];
                        Delinquent.Pathfinding.target = Delinquent.Destinations[Delinquent.Phase];

                        if (Delinquent.CurrentDestination == null)
                        {
                            Debug.Log("Manually setting Delinquent's destination to locker, to fix a saving/loading bug.");

                            Delinquent.CurrentDestination = Delinquent.Destinations[1];
                            Delinquent.Pathfinding.target = Delinquent.Destinations[1];
                        }

                        Delinquent.IdleAnim = AnimNames.MaleInjuredIdle;
						Delinquent.WalkAnim = AnimNames.MaleInjuredWalk;

						Delinquent.OriginalIdleAnim = Delinquent.IdleAnim;
						Delinquent.OriginalWalkAnim = Delinquent.WalkAnim;

						Delinquent.LeanAnim = Delinquent.IdleAnim;

						Delinquent.CharacterAnimation.CrossFade(Delinquent.IdleAnim);

						Delinquent.Threatened = true;
						Delinquent.Alarmed = true;
						Delinquent.Injured = true;

						Delinquent.Strength = 0;
						Delinquent.Defeats++;
					}
					else
					{
						Delinquent.Threatened = false;
						Delinquent.Alarmed = false;

						PracticeWindow.Finish();
						Yandere.Health = 10;
						Practice = false;
					}

					Delinquent.Fighting = false;
					Delinquent.enabled = true;

					Delinquent.Distracted = false;
					Delinquent.Shoving = false;
					Delinquent.Paired = false;

					Delinquent = null;

					ReleaseYandere();

					ResetValues();

					Yandere.StudentManager.UpdateStudents();
				}
			}
		}

		//////////////////////////////////////////////////////////
		///// PATH FIVE - DELINQUENT KNOCKS YANDERE-CHAN OUT /////
		//////////////////////////////////////////////////////////

		else if (Path == 5)
		{
			if (Phase == 4)
			{
				MainCamera.position = Vector3.Lerp(MainCamera.position, CameraTarget, Time.deltaTime);

				if (Yandere.CharacterAnimation["Yandere_CombatE"].time > Yandere.CharacterAnimation["Yandere_CombatE"].length)
				{
					this.Timer += Time.deltaTime;

					if (Timer > 1)
					{
						this.Yandere.ShoulderCamera.HeartbrokenCamera.SetActive(true);
						//this.Yandere.Police.Heartbroken.Arrested = true;
						this.Yandere.ShoulderCamera.enabled = false;
						this.Yandere.RPGCamera.enabled = false;
						this.Yandere.Jukebox.GameOver();
						this.Yandere.enabled = false;
						this.Yandere.EmptyHands();
						this.Yandere.Lost = true;
						this.Phase++;
					}
				}
			}
		}

		/////////////////////////////////////////////////////
		///// PATH SIX - DELINQUENT SPARES YANDERE-CHAN /////
		/////////////////////////////////////////////////////

		else if (Path == 6)
		{
			if (Phase == 4)
			{
				if (Yandere.CharacterAnimation["Yandere_CombatF"].time > 6.33333f)
				{
					MainCamera.transform.parent = null;
					Strength += Time.deltaTime;

					MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, StartPoint, Time.deltaTime * Strength);
					RedVignette.color = Vector4.Lerp(RedVignette.color, new Vector4(1, 0, 0, 0), Time.deltaTime * Strength);
					Zoom = false;
				}

				if (Delinquent.CharacterAnimation[Prefix + "Delinquent_CombatF"].time > 7.83333f)
				{
					Delinquent.MyWeapon.transform.parent = Delinquent.WeaponBagParent;
					Delinquent.MyWeapon.transform.localEulerAngles = new Vector3(0, 0, 0);
					Delinquent.MyWeapon.transform.localPosition = new Vector3(0, 0, 0);
				}

				if (Yandere.CharacterAnimation["Yandere_CombatF"].time > Yandere.CharacterAnimation["Yandere_CombatF"].length)
				{
					//Debug.Log("Player lost.");

					if (!Practice)
					{
						Yandere.Subtitle.UpdateLabel(SubtitleType.DelinquentWin, 0, 5.0f);
						
						Yandere.IdleAnim = AnimNames.FemaleInjuredIdle;
						Yandere.WalkAnim = AnimNames.FemaleInjuredWalk;

						Yandere.OriginalIdleAnim = Yandere.IdleAnim;
						Yandere.OriginalWalkAnim = Yandere.WalkAnim;

						Yandere.StudentManager.Rest.Prompt.enabled = true;
					}
					else
					{
						PracticeWindow.Finish();
						Yandere.Health = 10;
						Practice = false;
					}

					Yandere.CharacterAnimation.CrossFade(Yandere.IdleAnim);

					Yandere.DelinquentFighting = false;
					Yandere.RPGCamera.enabled = true;
					Yandere.CannotRecover = false;
					Yandere.CanMove = true;
					Yandere.Chased = false;

					Delinquent.Threatened = false;
					Delinquent.Fighting = false;
					Delinquent.Injured = false;
					Delinquent.Alarmed = false;
					Delinquent.Routine = true;
					Delinquent.enabled = true;

					Delinquent.Distracted = false;
					Delinquent.Shoving = false;
					Delinquent.Paired = false;
					Delinquent.Patience = 5;

					ResetValues();

					Yandere.StudentManager.UpdateStudents();
				}
			}
		}

		///////////////////////////////////////////////////////
		///// PATH SEVEN - COUNCIL MEMBER BREAKS UP FIGHT /////
		///////////////////////////////////////////////////////

		else if (Path == 7)
		{
			if (Yandere.CharacterAnimation[AnimNames.FemaleStopFighting].time > 1)
			{
				MainCamera.transform.parent = null;
				Strength += Time.deltaTime;

				MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, StartPoint, Time.deltaTime * Strength);
				RedVignette.color = Vector4.Lerp(RedVignette.color, new Vector4(1, 0, 0, 0), Time.deltaTime * Strength);
				Zoom = false;
			}

			if (Delinquent.CharacterAnimation["stopFighting_00"].time > 3.83333f)
			{
				Delinquent.MyWeapon.transform.parent = Delinquent.WeaponBagParent;
				Delinquent.MyWeapon.transform.localEulerAngles = new Vector3(0, 0, 0);
				Delinquent.MyWeapon.transform.localPosition = new Vector3(0, 0, 0);
			}

			if (Yandere.CharacterAnimation[AnimNames.FemaleStopFighting].time >
				Yandere.CharacterAnimation[AnimNames.FemaleStopFighting].length)
			{
                if (Delinquent.Phase == 0)
                {
                    Delinquent.Phase++;
                }

				Delinquent.GetDestinations();
				Delinquent.CurrentDestination = Delinquent.Destinations[Delinquent.Phase];
				Delinquent.Pathfinding.target = Delinquent.Destinations[Delinquent.Phase];

                if (Delinquent.CurrentDestination == null)
                {
                    Debug.Log("Manually setting Delinquent's destination to locker, to fix a saving/loading bug.");

                    Delinquent.CurrentDestination = Delinquent.Destinations[1];
                    Delinquent.Pathfinding.target = Delinquent.Destinations[1];
                }

                ReleaseYandere();

				Delinquent.Threatened = false;
				Delinquent.Fighting = false;
				Delinquent.Alarmed = false;
				Delinquent.enabled = true;

				Delinquent.Distracted = false;
				Delinquent.Shoving = false;
				Delinquent.Paired = false;
				Delinquent.Routine = true;
				Delinquent.Patience = 5;

				Delinquent = null;

				DisablePrompts();
				ResetValues();

				Yandere.StudentManager.UpdateStudents();
			}
		}
	}

	void Slowdown()
	{
		Time.timeScale = SlowdownFactor * Difficulty;
		MyVocals.pitch = SlowdownFactor * Difficulty;
		MyAudio.pitch = SlowdownFactor * Difficulty;
	}

	void ChooseButton()
	{
		ButtonPrompts[1].enabled = false;
		ButtonPrompts[2].enabled = false;
		ButtonPrompts[3].enabled = false;
		ButtonPrompts[4].enabled = false;

        ButtonPrompts[1].alpha = 0;
        ButtonPrompts[2].alpha = 0;
        ButtonPrompts[3].alpha = 0;
        ButtonPrompts[4].alpha = 0;

        int PreviousButton = ButtonID;

		while (ButtonID == PreviousButton)
		{
			ButtonID = Random.Range(1, 5);
		}

		if (ButtonID == 1)
		{
			CurrentButton = "A";
		}
		else if (ButtonID == 2)
		{
			CurrentButton = "B";
		}
		else if (ButtonID == 3)
		{
			CurrentButton = "X";
		}
		else if (ButtonID == 4)
		{
			CurrentButton = "Y";
		}

		ButtonPrompts[ButtonID].enabled = true;
        ButtonPrompts[ButtonID].alpha = 1;
        Circle.enabled = true;
		BG.enabled = true;
		Timer = StartTime;
	}

	public void DisablePrompts()
	{
		ButtonPrompts[1].enabled = false;
		ButtonPrompts[2].enabled = false;
		ButtonPrompts[3].enabled = false;
		ButtonPrompts[4].enabled = false;

        ButtonPrompts[1].alpha = 0;
        ButtonPrompts[2].alpha = 0;
        ButtonPrompts[3].alpha = 0;
        ButtonPrompts[4].alpha = 0;

        Circle.fillAmount = 1;
		Circle.enabled = false;
		BG.enabled = false;
		Success = false;

		ButtonID = 0;
	}

	void AdjustMidpoint()
	{
		if (Strength == 0)
		{
			if (!KnockedOut)
			{
				Midpoint.position = ((Delinquent.Hips.position - Yandere.Hips.position) * 0.5f) + Yandere.Hips.position; 
				Midpoint.position += new Vector3(0, .25f, 0);
			}
			else
			{
				Midpoint.position = Vector3.Lerp(Midpoint.position, Yandere.Hips.position + new Vector3(0, 0.5f, 0), Time.deltaTime);
			}
		}
		else
		{
			Midpoint.position = Vector3.Lerp(Midpoint.position, Yandere.RPGCamera.cameraPivot.position, Time.deltaTime * Strength);
		}

		MainCamera.LookAt(Midpoint.position);
	}

	public void Stop()
	{
		if (this.Delinquent != null)
		{
			Delinquent.CharacterAnimation.CrossFade("delinquentCombatIdle_00");

			ResetValues();

			enabled = false;
		}
	}

	public void ResetValues()
	{
		Label.text = "State: A";
		Strength = 0;
		Strike = 0;
		Phase = 0;
		Path = 0;

		MyAudio.clip = CombatSFX[Path];
		MyAudio.time = 0;
		MyAudio.Stop();

		MyVocals.clip = Vocals[Path];
		MyVocals.time = 0;
		MyVocals.Stop();

		Delinquent = null;
	}

	public void ReleaseYandere()
	{
        Debug.Log("Yandere-chan has been released from combat.");

		Yandere.CharacterAnimation.CrossFade(Yandere.IdleAnim);
		Yandere.DelinquentFighting = false;
		Yandere.RPGCamera.enabled = true;
		Yandere.CannotRecover = false;
		Yandere.CanMove = true;
		Yandere.Chased = false;
	}
}