using UnityEngine;

public class HeadmasterScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public HeartbrokenScript Heartbroken;
	public YandereScript Yandere;
	public JukeboxScript Jukebox;

	public AudioClip[] HeadmasterSpeechClips;
	public AudioClip[] HeadmasterThreatClips;
	public AudioClip[] HeadmasterBoxClips;
	public AudioClip HeadmasterRelaxClip;
	public AudioClip HeadmasterAttackClip;
	public AudioClip HeadmasterCrypticClip;
	public AudioClip HeadmasterShockClip;

	public AudioClip HeadmasterPatienceClip;
	public AudioClip HeadmasterCorpseClip;
	public AudioClip HeadmasterWeaponClip;

	public AudioClip Crumple;

	public AudioClip StandUp;
	public AudioClip SitDown;

	public readonly string[] HeadmasterSpeechText =
	{
		"",
		"Ahh...! It's...it's you!",
		"No, that would be impossible...you must be...her daughter...",
		"I'll tolerate you in my school, but not in my office.",
		"Leave at once.",
		"There is nothing for you to achieve here. Just. Get. Out."
	};

	public readonly string[] HeadmasterThreatText =
	{
		"",
		"Not another step!",
		"You're up to no good! I know it!",
		"I'm not going to let you harm me!",
		"I'll use self-defense if I deem it necessary!",
		"This is your final warning. Get out of here...or else."
	};

	public readonly string[] HeadmasterBoxText =
	{
		"",
		"What...in...blazes are you doing?",
		"Are you trying to re-enact something you saw in a video game?",
		"Ugh, do you really think such a stupid ploy is going to work?",
		"I know who you are. It's obvious. You're not fooling anyone.",
		"I don't have time for this tomfoolery. Leave at once!"
	};

	public readonly string HeadmasterRelaxText = "Hmm...a wise decision.";
	public readonly string HeadmasterAttackText = "You asked for it!";
	public readonly string HeadmasterCrypticText = "Mr. Saikou...the deal is off.";

	public readonly string HeadmasterWeaponText = "How dare you raise a weapon in my office!";
	public readonly string HeadmasterPatienceText = "Enough of this nonsense!";
	public readonly string HeadmasterCorpseText = "You...you murderer!";

	public UILabel HeadmasterSubtitle;
	public Animation MyAnimation;
	public AudioSource MyAudio;

	public GameObject LightningEffect;
	public GameObject Tazer;

	public Transform TazerEffectTarget;
	public Transform CardboardBox;
	public Transform Chair;

	public Quaternion targetRotation;

	public float PatienceTimer;
	public float ScratchTimer;
	public float SpeechTimer;
	public float ThreatTimer;
	public float Distance;

	public int Patience = 10;
	public int ThreatID;
	public int VoiceID;
	public int BoxID;

	public bool PlayedStandSound;
	public bool PlayedSitSound;

	public bool LostPatience;
	public bool Threatened;
	public bool Relaxing;
	public bool Shooting;
	public bool Aiming;

	void Start()
	{
		this.MyAnimation[AnimNames.HeadmasterRaiseTazer].speed = 2.0f;

		this.Tazer.SetActive(false);
	}

	void Update()
	{
		if (this.Yandere.transform.position.y > this.transform.position.y - 1 &&
			this.Yandere.transform.position.y < this.transform.position.y + 1 &&
			this.Yandere.transform.position.x < 6 &&
			this.Yandere.transform.position.x > -6)
		{
			// [af] Distance to Yandere-chan.
			this.Distance = Vector3.Distance(this.transform.position, this.Yandere.transform.position);

			if (this.Shooting)
			{
				this.targetRotation = Quaternion.LookRotation(
					this.transform.position - this.Yandere.transform.position);

				this.Yandere.transform.rotation = Quaternion.Slerp(
					this.Yandere.transform.rotation, this.targetRotation, Time.deltaTime * 10.0f);

				this.AimWeaponAtYandere();
				this.AimBodyAtYandere();
			}
			else
			{
				if (this.Distance < 1.2)
				{
					this.AimBodyAtYandere();

					if (this.Yandere.CanMove && !this.Yandere.Egg)
					{
						if (!this.Shooting)
						{
							this.Shoot();
						}
					}
				}
				else if (this.Distance < 2.8)
				{
					this.PlayedSitSound = false;

					if (!this.StudentManager.Clock.StopTime)
					{
						this.PatienceTimer -= Time.deltaTime;
					}

					if (this.PatienceTimer < 0 && !this.Yandere.Egg)
					{
						this.LostPatience = true;
						this.PatienceTimer = 60;
						this.Patience = 0;
						this.Shoot();
					}

					if (!this.LostPatience)
					{
						this.LostPatience = true;
						this.Patience--;

						if (this.Patience < 1 && !this.Yandere.Egg)
						{
							if (!this.Shooting)
							{
								this.Shoot();
							}
						}
					}

					this.AimBodyAtYandere();

					this.Threatened = true;

					this.AimWeaponAtYandere();

					this.ThreatTimer = Mathf.MoveTowards(this.ThreatTimer, 0, Time.deltaTime);

					if (this.ThreatTimer == 0)
					{
						this.ThreatID++;

						if (this.ThreatID < 5)
						{
							this.HeadmasterSubtitle.text = this.HeadmasterThreatText[ThreatID];

							this.MyAudio.clip = this.HeadmasterThreatClips[ThreatID];
							this.MyAudio.Play();

							this.ThreatTimer = this.HeadmasterThreatClips[ThreatID].length + 1;
						}
					}

					this.CheckBehavior();
				}
				else if (this.Distance < 10)
				{
					this.PlayedStandSound = false;
					this.LostPatience = false;

					this.targetRotation = Quaternion.LookRotation(
						new Vector3(0, 8, 0) - this.transform.position);

					this.transform.rotation = Quaternion.Slerp(
						this.transform.rotation, this.targetRotation, Time.deltaTime * 10.0f);

					this.Chair.localPosition = Vector3.Lerp(this.Chair.localPosition,
						new Vector3(this.Chair.localPosition.x, this.Chair.localPosition.y, -4.66666f),
						Time.deltaTime * 1);

					this.LookAtPlayer = true;

					if (!this.Threatened)
					{
						this.MyAnimation.CrossFade(AnimNames.HeadmasterAttention, 1.0f);
						this.ScratchTimer = 0.0f;

						this.SpeechTimer = Mathf.MoveTowards(this.SpeechTimer, 0, Time.deltaTime);

						if (this.SpeechTimer == 0)
						{
							if (this.CardboardBox.parent == null && this.Yandere.Mask == null)
							{
								this.VoiceID++;

								if (VoiceID < 6)
								{
									this.HeadmasterSubtitle.text = this.HeadmasterSpeechText[VoiceID];

									this.MyAudio.clip = this.HeadmasterSpeechClips[VoiceID];
									this.MyAudio.Play();

									this.SpeechTimer = this.HeadmasterSpeechClips[VoiceID].length + 1;
								}
							}
							else
							{
								this.BoxID++;

								if (BoxID < 6)
								{
									this.HeadmasterSubtitle.text = this.HeadmasterBoxText[BoxID];

									this.MyAudio.clip = this.HeadmasterBoxClips[BoxID];
									this.MyAudio.Play();

									this.SpeechTimer = this.HeadmasterBoxClips[BoxID].length + 1;
								}
							}
						}
					}
					else
					{
						if (!this.Relaxing)
						{
							this.HeadmasterSubtitle.text = this.HeadmasterRelaxText;

							this.MyAudio.clip = this.HeadmasterRelaxClip;
							this.MyAudio.Play();

							//this.SpeechTimer = 10;
							this.Relaxing = true;
							//this.Aiming = false;
						}
						else
						{
							if (!this.PlayedSitSound)
							{
								AudioSource.PlayClipAtPoint(SitDown, this.transform.position);
								this.PlayedSitSound = true;
							}

							this.MyAnimation.CrossFade(AnimNames.HeadmasterLowerTazer);
							this.Aiming = false;

							if (this.MyAnimation[AnimNames.HeadmasterLowerTazer].time > 1.33333)
							{
								this.Tazer.SetActive(false);
							}

							if (this.MyAnimation[AnimNames.HeadmasterLowerTazer].time >
								this.MyAnimation[AnimNames.HeadmasterLowerTazer].length)
							{
								this.Threatened = false;
								this.Relaxing = false;
							}
						}
					}

					this.CheckBehavior();
				}
				//If the player is nowhere near the headmaster...
				else
				{
					if (this.LookAtPlayer)
					{
						this.MyAnimation.CrossFade(AnimNames.HeadmasterType);
						this.LookAtPlayer = false;
						this.Threatened = false;
						this.Relaxing = false;
						this.Aiming = false;
					}

					this.ScratchTimer += Time.deltaTime;

					if (this.ScratchTimer > 10.0f)
					{
						this.MyAnimation.CrossFade(AnimNames.HeadmasterScratch);

						if (this.MyAnimation[AnimNames.HeadmasterScratch].time >
							this.MyAnimation[AnimNames.HeadmasterScratch].length)
						{
							this.MyAnimation.CrossFade(AnimNames.HeadmasterType);
							this.ScratchTimer = 0.0f;
						}
					}
				}
			}

			if (!this.MyAudio.isPlaying)
			{
				this.HeadmasterSubtitle.text = string.Empty;

				if (this.Shooting)
				{
					this.Taze();
				}
			}

			if (this.Yandere.Attacked)
			{
				if (this.Yandere.Character.GetComponent<Animation>()[AnimNames.FemaleSwingB].time >=
					this.Yandere.Character.GetComponent<Animation>()[AnimNames.FemaleSwingB].length * .85f)
				{
					MyAudio.clip = this.Crumple;
					MyAudio.Play();

					this.enabled = false;
				}
			}
		}
		else
		{
			this.HeadmasterSubtitle.text = string.Empty;
		}
	}

	public Vector3 LookAtTarget;
	public bool LookAtPlayer = false;
	public Transform Default;
	public Transform Head;

	void LateUpdate()
	{
		this.LookAtTarget = Vector3.Lerp(
			this.LookAtTarget,
			this.LookAtPlayer ? this.Yandere.Head.position : this.Default.position,
			Time.deltaTime * 10.0f);

		this.Head.LookAt(this.LookAtTarget);
	}

	void AimBodyAtYandere()
	{
		this.targetRotation = Quaternion.LookRotation(
			this.Yandere.transform.position - this.transform.position);

		this.transform.rotation = Quaternion.Slerp(
			this.transform.rotation, this.targetRotation, Time.deltaTime * 5.0f);

		Chair.localPosition = Vector3.Lerp(Chair.localPosition,
			new Vector3(Chair.localPosition.x, Chair.localPosition.y, -5.2f),
			Time.deltaTime * 1);
	}

	void AimWeaponAtYandere()
	{
		if (!this.Aiming)
		{
			this.MyAnimation.CrossFade(AnimNames.HeadmasterRaiseTazer);

			if (!this.PlayedStandSound)
			{
				AudioSource.PlayClipAtPoint(this.StandUp, this.transform.position);
				this.PlayedStandSound = true;
			}

			if (this.MyAnimation[AnimNames.HeadmasterRaiseTazer].time > 1.166666)
			{
				this.Tazer.SetActive(true);
				this.Aiming = true;
			}
		}
		else
		{
			if (this.MyAnimation[AnimNames.HeadmasterRaiseTazer].time >
				this.MyAnimation[AnimNames.HeadmasterRaiseTazer].length)
			{
				this.MyAnimation.CrossFade(AnimNames.HeadmasterAimTazer);
			}
		}
	}

	public void Shoot()
	{
		this.StudentManager.YandereDying = true;

		this.Yandere.StopAiming();
		this.Yandere.StopLaughing();
		this.Yandere.CharacterAnimation.CrossFade(AnimNames.FemaleReadyToFight);

		if (this.Patience < 1)
		{
			this.HeadmasterSubtitle.text = this.HeadmasterPatienceText;
			this.MyAudio.clip = this.HeadmasterPatienceClip;
		}
		else if (this.Yandere.Armed)
		{
			this.HeadmasterSubtitle.text = this.HeadmasterWeaponText;
			this.MyAudio.clip = this.HeadmasterWeaponClip;
		}
		else if (this.Yandere.Carrying || this.Yandere.Dragging || this.Yandere.PickUp != null && this.Yandere.PickUp.BodyPart)
		{
			this.HeadmasterSubtitle.text = this.HeadmasterCorpseText;
			this.MyAudio.clip = this.HeadmasterCorpseClip;
		}
		else
		{
			this.HeadmasterSubtitle.text = this.HeadmasterAttackText;
			this.MyAudio.clip = this.HeadmasterAttackClip;
		}

		this.StudentManager.StopMoving();

		this.Yandere.EmptyHands();
		this.Yandere.CanMove = false;

		this.MyAudio.Play();

		this.Shooting = true;
	}

	void CheckBehavior()
	{
		if (this.Yandere.CanMove && !this.Yandere.Egg)
		{
			if (this.Yandere.Chased || this.Yandere.Chasers > 0)
			{
				if (!this.Shooting)
				{
					this.Shoot();
				}
			}
			else if (this.Yandere.Armed)
			{
				if (!this.Shooting)
				{
					this.Shoot();
				}
			}
			else if (this.Yandere.Carrying || this.Yandere.Dragging || this.Yandere.PickUp != null && this.Yandere.PickUp.BodyPart)
			{
				if (!this.Shooting)
				{
					this.Shoot();
				}
			}
		}
	}

	public void Taze()
	{
		if (this.Yandere.CanMove)
		{
			this.StudentManager.YandereDying = true;

			this.Yandere.StopAiming();
			this.Yandere.StopLaughing();

			this.StudentManager.StopMoving();

			this.Yandere.EmptyHands();
			this.Yandere.CanMove = false;
		}

		Instantiate(this.LightningEffect, this.TazerEffectTarget.position, Quaternion.identity);
		Instantiate(this.LightningEffect, this.Yandere.Spine[3].position, Quaternion.identity);

		this.MyAudio.clip = HeadmasterShockClip;
		this.MyAudio.Play();

		this.Yandere.CharacterAnimation.CrossFade(AnimNames.FemaleSwingB);
		this.Yandere.CharacterAnimation[AnimNames.FemaleSwingB].time = .5f;
		this.Yandere.RPGCamera.enabled = false;
		this.Yandere.Attacked = true;

		this.Heartbroken.Headmaster = true;
		this.Jukebox.Volume = 0;
		this.Shooting = false;
	}
}