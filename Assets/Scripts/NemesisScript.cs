using UnityEngine.SceneManagement;

using UnityEngine;

public class NemesisScript : MonoBehaviour
{
	public MissionModeScript MissionMode;
	public CosmeticScript Cosmetic;
	public StudentScript Student;
	public YandereScript Yandere;

	public AudioClip YandereDeath;

	public Texture NemesisUniform;
	public Texture NemesisFace;
	public Texture NemesisEyes;

	public GameObject BloodEffect;
	public GameObject NemesisHair;
	public GameObject Knife;

	public bool PutOnDisguise = false;
	public bool Aggressive = false;
	public bool Attacking = false;
	public bool Chasing = false;
	public bool InView = false;
	public bool Dying = false;

	public int EffectPhase = 0;
	public int Difficulty = 0;
	public int ID = 0;

	public float OriginalYPosition = 0.0f;
	public float ScanTimer = 6.0f;

	void Start()
	{
		foreach (GameObject hair in this.Cosmetic.FemaleHair)
		{
			if (hair != null)
			{
				hair.SetActive(false);
			}
		}
			
		foreach (GameObject hair in this.Cosmetic.TeacherHair)
		{
			if (hair != null)
			{
				hair.SetActive(false);
			}
		}
			
		foreach (GameObject accessory in this.Cosmetic.FemaleAccessories)
		{
			if (accessory != null)
			{
				accessory.SetActive(false);
			}
		}

		foreach (GameObject accessory in this.Cosmetic.TeacherAccessories)
		{
			if (accessory != null)
			{
				accessory.SetActive(false);
			}
		}

		foreach (GameObject accessory in this.Cosmetic.ClubAccessories)
		{
			if (accessory != null)
			{
				accessory.SetActive(false);
			}
		}

		foreach (GameObject accessory in this.Cosmetic.Kerchiefs)
		{
			if (accessory != null)
			{
				accessory.SetActive(false);
			}
		}

		foreach (GameObject accessory in this.Cosmetic.CatGifts)
		{
			if (accessory != null)
			{
				accessory.SetActive(false);
			}
		}

		this.Difficulty = MissionModeGlobals.NemesisDifficulty;

		if (this.Difficulty == 0)
		{
			this.Difficulty = 1;
		}

		this.Student.StudentManager = GameObject.Find("StudentManager").GetComponent<StudentManagerScript>();
		this.Student.WitnessCamera = GameObject.Find("WitnessCamera").GetComponent<WitnessCameraScript>();
		this.Student.Police = GameObject.Find("Police").GetComponent<PoliceScript>();
		this.Student.JSON = GameObject.Find("JSON").GetComponent<JsonScript>();
		this.Student.CharacterAnimation = this.Student.Character.GetComponent<Animation>();
		this.Student.Ragdoll.Nemesis = true;
		this.Student.Yandere = this.Yandere;

		this.Student.IdleAnim = "f02_newIdle_00";
		this.Student.WalkAnim = "f02_newWalk_00";

		this.Student.ShoeRemoval.RightCasualShoe.gameObject.SetActive(false);
		this.Student.ShoeRemoval.LeftCasualShoe.gameObject.SetActive(false);

		if (this.Difficulty < 3)
		{
			this.Student.Character.GetComponent<Animation>()[AnimNames.FemaleNemesisEyes].layer = 2;
			this.Student.Character.GetComponent<Animation>().Play(AnimNames.FemaleNemesisEyes);

			this.Cosmetic.MyRenderer.sharedMesh = this.Cosmetic.FemaleUniforms[5];

			this.Cosmetic.MyRenderer.materials[0].mainTexture = this.NemesisUniform;
			this.Cosmetic.MyRenderer.materials[1].mainTexture = this.NemesisUniform;
			this.Cosmetic.MyRenderer.materials[2].mainTexture = this.NemesisFace;

			this.Cosmetic.RightEyeRenderer.material.mainTexture = this.NemesisEyes;
			this.Cosmetic.LeftEyeRenderer.material.mainTexture = this.NemesisEyes;

			this.Student.FaceCollider.tag = "Nemesis";
			this.NemesisHair.SetActive(true);
		}
		else
		{
			this.NemesisHair.SetActive(false);
			this.PutOnDisguise = true;
		}

		this.Student.LowPoly.enabled = false;
		this.Student.DisableEffects();
		this.HideObjects();

		for (this.ID = 0; this.ID < this.Student.Ragdoll.AllRigidbodies.Length; this.ID++)
		{
			this.Student.Ragdoll.AllRigidbodies[this.ID].isKinematic = true;
			this.Student.Ragdoll.AllColliders[this.ID].enabled = false;
		}

		this.Student.Ragdoll.AllColliders[10].enabled = true;

		this.Student.Prompt.HideButton[0] = true;
		this.Student.Prompt.HideButton[2] = true;

		Destroy(this.Student.MyRigidbody);

		this.transform.position = this.MissionMode.SpawnPoints[Random.Range(0, 4)].position;

		this.MissionMode.LastKnownPosition.position = new Vector3(0.0f, 0.0f, -36.0f);
		this.UpdateLKP();

		this.transform.parent = null;
		this.Student.Name = "Nemesis";

		this.Aggressive = MissionModeGlobals.NemesisAggression;
	}

	void Update()
	{
		if (this.PutOnDisguise)
		{
            bool TryAgain = false;
			int StudentID = 1;

			while (this.Student.StudentManager.Students[StudentID] != null && this.Student.StudentManager.Students[StudentID].Male ||
				StudentID >  5 && StudentID < 21 ||
				StudentID == 21 || StudentID == 26 || StudentID == 31 || StudentID == 36 ||
				StudentID == 41 || StudentID == 46 || StudentID == 51 || StudentID == 56 ||
				StudentID == 61 || StudentID == 66 || StudentID == 71 ||
				StudentID == this.MissionMode.TargetID || TryAgain)
			{
				StudentID = Random.Range(2, 90);

                if (MissionMode.MultiMission)
                {
                    TryAgain = false;
                    int TempID = 1;

                    while (TempID < 11)
                    {
                        if (StudentID == PlayerPrefs.GetInt("MissionModeTarget" + TempID))
                        {
                            TryAgain = true;
                        }

                        TempID++;
                    }
                }
			}

			// [af] Added "gameObject" for C# compatibility.
			this.Student.StudentManager.Students[StudentID].gameObject.SetActive(false);
			this.Student.StudentManager.Students[StudentID].Replaced = true;

			this.Cosmetic.StudentID = StudentID;
			this.Cosmetic.Start();

			OutlineScript femaleHairOutline =
				this.Cosmetic.FemaleHair[this.Cosmetic.Hairstyle].GetComponent<OutlineScript>();

			if (femaleHairOutline != null)
			{
				femaleHairOutline.enabled = false;
			}
			else
			{
				femaleHairOutline =
				this.Cosmetic.FemaleHairRenderers[this.Cosmetic.Hairstyle].GetComponent<OutlineScript>();

				if (femaleHairOutline != null)
				{
					femaleHairOutline.enabled = false;
				}
			}

			this.Student.FaceCollider.tag = "Disguise";

			Debug.Log("Nemesis has disguised herself as " + this.Student.StudentManager.Students[StudentID].Name);

			this.PutOnDisguise = false;
		}

		if (!this.Dying)
		{
			if (!this.Attacking)
			{
				if (this.Yandere.Laughing)
				{
					if (Vector3.Distance(this.transform.position, this.Yandere.transform.position) < 10.0f)
					{
						this.MissionMode.LastKnownPosition.position = this.Yandere.transform.position;
						this.UpdateLKP();
					}
				}

				if (!this.Yandere.CanMove && !this.Yandere.Laughing)
				{
					if (this.Student.Pathfinding.canSearch)
					{
						this.Student.Character.GetComponent<Animation>().CrossFade("f02_idleShort_00");
						this.Student.Pathfinding.canSearch = false;
						this.Student.Pathfinding.canMove = false;
						this.Student.Pathfinding.speed = 0.0f;
					}
				}
				else
				{
					if ((this.Yandere.Stance.Current != StanceType.Crouching) &&
						(this.Yandere.Stance.Current != StanceType.Crawling))
					{
						if (Vector3.Distance(this.transform.position, this.Yandere.transform.position) < 10.0f)
						{
							if (this.Yandere.Running)
							{
								this.MissionMode.LastKnownPosition.position = this.Yandere.transform.position;
								this.UpdateLKP();
							}
						}
					}

					if (!this.Student.Pathfinding.canSearch)
					{
						if (!this.Chasing)
						{
							this.Student.Character.GetComponent<Animation>().CrossFade(this.Student.WalkAnim);
							this.Student.Pathfinding.speed = 1.0f;
						}
						else
						{
							this.Student.Character.GetComponent<Animation>().CrossFade("f02_sithRun_00");
							this.Student.Pathfinding.speed = 5.0f;
						}

						this.Student.Pathfinding.canSearch = true;
						this.Student.Pathfinding.canMove = true;
					}

					this.InView = false;

					this.LookForYandere();

					if (!this.Chasing)
					{
						this.Student.Pathfinding.speed = Mathf.MoveTowards(
							this.Student.Pathfinding.speed,
							this.InView ? 2.0f : 1.0f,
							Time.deltaTime * 0.10f);

						this.Student.Character.GetComponent<Animation>()[this.Student.WalkAnim].speed =
							this.Student.Pathfinding.speed;
					}
					else
					{
						this.Student.Pathfinding.speed = 5;
					}

					if (Vector3.Distance(this.transform.position, this.Yandere.transform.position) < 1.0f)
					{
						if (this.InView || this.Chasing)
						{
							this.Student.CharacterAnimation.CrossFade(AnimNames.FemaleKnifeLowSanityA);
							this.Yandere.CharacterAnimation.CrossFade(AnimNames.FemaleKnifeLowSanityB);

							AudioSource.PlayClipAtPoint(this.YandereDeath, this.transform.position);
							this.Student.Pathfinding.canSearch = false;
							this.Student.Pathfinding.canMove = false;
							this.Knife.SetActive(true);
							this.Attacking = true;

							OriginalYPosition = Yandere.transform.position.y;

							this.Yandere.StudentManager.YandereDying = true;
							this.Yandere.StudentManager.StopMoving();

							AudioSource audioSource = this.GetComponent<AudioSource>();
							audioSource.Play();

							this.Yandere.YandereVision = false;
							this.Yandere.FollowHips = true;
							this.Yandere.Laughing = false;
							this.Yandere.CanMove = false;
							this.Yandere.EyeShrink = .5f;
							this.Yandere.StopAiming();
							this.Yandere.EmptyHands();
						}
					}
					else if (Vector3.Distance(this.transform.position,
						this.MissionMode.LastKnownPosition.position) < 1.0f)
					{
						this.Student.Character.GetComponent<Animation>().CrossFade(AnimNames.FemaleNemesisScan);
						this.Student.Pathfinding.speed = 0.0f;

						this.ScanTimer += Time.deltaTime;

						if (this.ScanTimer > 6.0f)
						{
							// [af] Replaced if/else statement with ternary expression.
							Vector3 lastKnownZPosition = new Vector3(0.0f, 0.0f, -2.50f);
							this.MissionMode.LastKnownPosition.position =
								(this.MissionMode.LastKnownPosition.position == lastKnownZPosition) ?
								this.Yandere.transform.position : lastKnownZPosition;

							this.Chasing = false;
							this.UpdateLKP();
						}
					}
				}

				if ((this.Difficulty == 1) || (this.Difficulty == 3))
				{
					if (Vector3.Distance(this.transform.position, this.Yandere.transform.position) < 1.0f)
					{
						float Angle = Vector3.Angle(-this.transform.forward,
							this.Yandere.transform.position - this.transform.position);

						if (Mathf.Abs(Angle) > 45.0f)
						{
							this.Student.Prompt.HideButton[2] = true;
						}
						else
						{
							if (this.Yandere.Armed)
							{
								this.Student.Prompt.HideButton[2] = false;
							}
						}

						if (!this.Yandere.Armed)
						{
							this.Student.Prompt.HideButton[2] = true;
						}

						if (this.Student.Prompt.Circle[2].fillAmount < 1.0f)
						{
							this.Yandere.TargetStudent = this.Student;

							this.Yandere.AttackManager.Stealth = true;
							this.Student.AttackReaction();

							// [af] Commented in JS code.
							//Yandere.AttackManager.Victim = Student.Character;
							//Yandere.AttackManager.Attack();

							this.Student.Pathfinding.canSearch = false;
							this.Student.Pathfinding.canMove = false;
							this.Student.Prompt.HideButton[2] = true;
							this.Dying = true;
						}
					}
					else
					{
						this.Student.Prompt.HideButton[2] = true;
					}
				}
			}
			//If we are currently killing Yandere-chan...
			else
			{
				// [af] Commented in JS code.
				//Yandere.audio.volume = 1;

				this.SpecialEffect();

				this.Yandere.targetRotation = Quaternion.LookRotation(
					this.transform.position - this.Yandere.transform.position);
				this.Yandere.transform.rotation = Quaternion.Slerp(
					this.Yandere.transform.rotation, this.Yandere.targetRotation, Time.deltaTime * 10.0f);

				this.Yandere.MoveTowardsTarget(
					this.transform.position + (this.transform.forward * 0.50f));
				this.Yandere.EyeShrink = .5f;

				this.Yandere.transform.position = new Vector3(
					this.Yandere.transform.position.x,
					OriginalYPosition,
					this.Yandere.transform.position.z);

				Quaternion targetRotation = Quaternion.LookRotation(
					this.Yandere.transform.position - this.transform.position);
				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, targetRotation, Time.deltaTime * 10.0f);

				Animation studentCharAnim = this.Student.Character.GetComponent<Animation>();
				if (studentCharAnim[AnimNames.FemaleKnifeLowSanityA].time >=
					studentCharAnim[AnimNames.FemaleKnifeLowSanityA].length)
				{
					if (this.MissionMode.enabled)
					{
						this.MissionMode.GameOverID = 13;
						this.MissionMode.GameOver();
						this.MissionMode.Phase = 4;

						this.enabled = false;
					}
					else
					{
						SceneManager.LoadScene(SceneNames.LoadingScene);
					}
				}
			}
		}
		else
		{
			if (this.Student.Alive)
			{
				this.Student.MoveTowardsTarget(this.Yandere.transform.position +
					(this.Yandere.transform.forward * this.Yandere.AttackManager.Distance));

				Quaternion targetRotation = Quaternion.LookRotation(
					this.transform.position - new Vector3(
						this.Yandere.transform.position.x,
						this.transform.position.y,
						this.Yandere.transform.position.z));
				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, targetRotation, Time.deltaTime * 10.0f);
			}
			else
			{
				this.enabled = false;
			}
		}
	}

	void LookForYandere()
	{
		Debug.Log ("Nemesis is looking for Yan-chan...");

		this.Student.VisionDistance = 25;

		if (this.Student.CanSeeObject(this.Yandere.gameObject, this.Yandere.HeadPosition))
		{
			Debug.Log ("Nemesis has spotted Yan-chan.");

			this.MissionMode.LastKnownPosition.position = this.Yandere.transform.position;
			this.InView = true;
			this.UpdateLKP();

			if (this.Aggressive)
			{
				this.Chasing = true;
			}
		}
		else
		{
			Debug.Log ("Nemesis currently cannot see Yandere-chan.");
		}
	}

	void UpdateLKP()
	{
		if (!this.Chasing)
		{
			this.Student.Character.GetComponent<Animation>().CrossFade(this.Student.WalkAnim);
		}
		else
		{
			this.Student.Character.GetComponent<Animation>().CrossFade("f02_sithRun_00");
		}

		if (this.Student.Pathfinding.speed == 0.0f)
		{
			if (!this.Chasing)
			{
				this.Student.Pathfinding.speed = 1.0f;
			}
			else
			{
				this.Student.Pathfinding.speed = 5.0f;
			}
		}

		this.ScanTimer = 0.0f;
		this.InView = true;
	}

	void SpecialEffect()
	{
		Animation studentCharAnim = this.Student.Character.GetComponent<Animation>();

		if (this.EffectPhase == 0)
		{
			if (studentCharAnim[AnimNames.FemaleKnifeLowSanityA].time > (83.0f / 30.0f))
			{
				Instantiate(this.BloodEffect,
					this.Knife.transform.position + (this.Knife.transform.forward * 0.10f),
					Quaternion.identity);
				this.EffectPhase++;
			}
		}
		else if (this.EffectPhase == 1)
		{
			if (studentCharAnim[AnimNames.FemaleKnifeLowSanityA].time > (106.0f / 30.0f))
			{
				Instantiate(this.BloodEffect,
					this.Knife.transform.position + (this.Knife.transform.forward * 0.10f),
					Quaternion.identity);
				this.EffectPhase++;
			}
		}
		else if (this.EffectPhase == 2)
		{
			if (studentCharAnim[AnimNames.FemaleKnifeLowSanityA].time > (125.0f / 30.0f))
			{
				Instantiate(this.BloodEffect,
					this.Knife.transform.position + (this.Knife.transform.forward * 0.10f),
					Quaternion.identity);
				this.EffectPhase++;
			}
		}
	}

	void HideObjects()
	{
		this.Student.Cosmetic.RightStockings[0].SetActive(false);
		this.Student.Cosmetic.LeftStockings[0].SetActive(false);

		this.Student.Cosmetic.RightWristband.SetActive(false);
		this.Student.Cosmetic.LeftWristband.SetActive(false);

		this.Student.FollowCountdown.gameObject.SetActive(false);
		this.Student.DramaticCamera.gameObject.SetActive(false);
		this.Student.VomitEmitter.gameObject.SetActive(false);
		this.Student.Countdown.gameObject.SetActive(false);
		this.Student.ScienceProps[0].SetActive(false);
		this.Student.Chopsticks[0].SetActive(false);
		this.Student.Chopsticks[1].SetActive(false);
		this.Student.Handkerchief.SetActive(false);
		this.Student.ChaseCamera.SetActive(false);
		this.Student.PepperSpray.SetActive(false);
		this.Student.WateringCan.SetActive(false);
		this.Student.OccultBook.SetActive(false);
		this.Student.Cigarette.SetActive(false);
		this.Student.EventBook.SetActive(false);
		this.Student.Handcuffs.SetActive(false);
		this.Student.CandyBar.SetActive(false);
		this.Student.Scrubber.SetActive(false);
		this.Student.Lighter.SetActive(false);
		this.Student.Octodog.SetActive(false);
		this.Student.Eraser.SetActive(false);
		this.Student.Bento.SetActive(false);
		this.Student.Pen.SetActive(false);
		this.Student.SpeechLines.Stop();

		this.Student.InstrumentBag[1].SetActive(false);
		this.Student.InstrumentBag[2].SetActive(false);
		this.Student.InstrumentBag[3].SetActive(false);
		this.Student.InstrumentBag[4].SetActive(false);
		this.Student.InstrumentBag[5].SetActive(false);

		this.Student.Instruments[1].SetActive(false);
		this.Student.Instruments[2].SetActive(false);
		this.Student.Instruments[3].SetActive(false);
		this.Student.Instruments[4].SetActive(false);
		this.Student.Instruments[5].SetActive(false);

		this.Student.Drumsticks[0].SetActive(false);
		this.Student.Drumsticks[1].SetActive(false);

		this.Student.Cosmetic.ThickBrows.SetActive(false);

		foreach (GameObject prop in this.Student.Cosmetic.PunkAccessories)
		{
			if (prop != null)
			{
				prop.SetActive(false);
			}
		}

		foreach (GameObject prop in this.Student.Fingerfood)
		{
			if (prop != null)
			{
				prop.SetActive(false);
			}
		}
	}
}