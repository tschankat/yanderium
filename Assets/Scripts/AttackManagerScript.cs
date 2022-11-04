using UnityEngine;

public class AttackManagerScript : MonoBehaviour
{
	public GameObject BloodEffect;
	private GameObject OriginalBloodEffect;

	private GameObject Victim;
	private YandereScript Yandere;

	private string VictimAnimName = string.Empty;
	private string AnimName = string.Empty;

	public bool PingPong = false;
	public bool Stealth = false;
	public bool Censor = false;
	public bool Loop = false;

	public int EffectPhase = 0;
	public int LoopPhase = 0;

	public float AttackTimer = 0.0f;
	public float Distance = 0.0f;
	public float Timer = 0.0f;

	public float LoopStart = 0.0f;
	public float LoopEnd = 0.0f;

	public Animation YandereAnim;
	public Animation VictimAnim;

	void Awake()
	{
		this.Yandere = this.GetComponent<YandereScript>();
	}

	void Start()
	{
		this.OriginalBloodEffect = this.BloodEffect;
	}

	public bool IsAttacking()
	{
		return this.Victim != null;
	}

	float GetReachDistance(WeaponType weaponType, SanityType sanityType)
	{
		///////////////////////////////
		///// SHORT SHARP WEAPONS /////
		///////////////////////////////

		if (weaponType == WeaponType.Knife)
		{
			if (this.Stealth)
			{
				return 0.75f;
			}
			else
			{
				if (sanityType == SanityType.High)
				{
					return 1.0f;
				}
				else if (sanityType == SanityType.Medium)
				{
					return 0.75f;
				}
				else
				{
					return 0.50f;
				}
			}
		}

		//////////////////////////////
		///// LONG SHARP WEAPONS /////
		//////////////////////////////

		else if (weaponType == WeaponType.Katana)
		{
			// [af] Replaced if/else statement with ternary expression.
			return this.Stealth ? 0.50f : 1.0f;
		}

		//////////////////////////////
		///// LONG BLUNT WEAPONS /////
		//////////////////////////////

		else if (weaponType == WeaponType.Bat)
		{
			if (this.Stealth)
			{
				return 0.50f;
			}
			else
			{
				if (sanityType == SanityType.High)
				{
					return 0.75f;
				}
				else if (sanityType == SanityType.Medium)
				{
					return 1.0f;
				}
				else
				{
					return 1.0f;
				}
			}
		}

		////////////////////////
		///// CIRCULAR SAW /////
		////////////////////////

		else if (weaponType == WeaponType.Saw)
		{
			// [af] Replaced if/else statement with ternary expression.
			return this.Stealth ? 0.70f : 1.0f;
		}

		///////////////////////////////
		///// SHORT BLUNT WEAPONS /////
		///////////////////////////////

		else if (weaponType == WeaponType.Weight)
		{
			if (this.Stealth)
			{
				return .75f;
			}
			else
			{
				if (sanityType == SanityType.High)
				{
					return .75f;
				}
				else if (sanityType == SanityType.Medium)
				{
					return .75f;
				}
				else
				{
					return .75f;
				}
			}
		}

		///////////////////
		///// SYRINGE /////
		///////////////////

		else if (weaponType == WeaponType.Syringe)
		{
			return 0.50f;
		}

        ///////////////////
        ///// GARROTE /////
        ///////////////////

        else if (weaponType == WeaponType.Garrote)
        {
            return 0.50f;
        }
        else
		{
			Debug.LogError("Weapon type \"" + weaponType.ToString() + "\" not implemented.");
			return 0.0f;
		}
	}

	public void Attack(GameObject victim, WeaponScript weapon)
	{
		this.Victim = victim;

		this.Yandere.FollowHips = true;

		this.AttackTimer = 0.0f;
		this.EffectPhase = 0;

		// [af] Inlined "SanityCheck()" after some refactoring since it's only used here.
		this.Yandere.Sanity = Mathf.Clamp(this.Yandere.Sanity, 0.0f, 100.0f);

		SanityType sanityType = this.Yandere.SanityType;
		string sanityString = this.Yandere.GetSanityString(sanityType);

		string prefix = weapon.GetTypePrefix();

		// [af] Replaced if/else statement with ternary expression.
		string gender = this.Yandere.TargetStudent.Male ? string.Empty : "f02_";

		if (!this.Stealth)
		{
			this.VictimAnimName = gender + prefix + sanityString + "SanityB_00";

			if (weapon.WeaponID == 23)
			{
				prefix = "extin";
			}

			this.AnimName = "f02_" + prefix + sanityString + "SanityA_00";
		}
		else
		{
			this.VictimAnimName = gender + prefix + "StealthB_00";

			if (weapon.WeaponID == 23)
			{
				prefix = "extin";
			}

			this.AnimName = "f02_" + prefix + "StealthA_00";
		}

		YandereAnim = this.Yandere.CharacterAnimation;
		YandereAnim[this.AnimName].time = 0.0f;
		YandereAnim.CrossFade(this.AnimName);

		VictimAnim = this.Yandere.TargetStudent.CharacterAnimation;
		VictimAnim[this.VictimAnimName].time = 0.0f;
		VictimAnim.CrossFade(this.VictimAnimName);

		weapon.MyAudio.clip = weapon.GetClip(this.Yandere.Sanity / 100.0f, this.Stealth);
        weapon.MyAudio.time = 0.0f;
        weapon.MyAudio.Play();

		if (weapon.Type == WeaponType.Knife)
		{
			weapon.Flip = true;
		}

		this.Distance = this.GetReachDistance(weapon.Type, sanityType);
	}

	void Update()
	{
		if (this.IsAttacking())
		{
			VictimAnim.CrossFade(this.VictimAnimName);

			if (this.Censor)
			{
				if (this.AttackTimer == 0)
				{
					Yandere.Blur.enabled = true;
					Yandere.Blur.blurSize = 0;
					Yandere.Blur.blurIterations = 0;
				}

				if (this.AttackTimer < YandereAnim[this.AnimName].length - .5f)
				{
					Yandere.Blur.blurSize = Mathf.MoveTowards(Yandere.Blur.blurSize, 10, Time.deltaTime * 10);

					if (Yandere.Blur.blurSize > Yandere.Blur.blurIterations)
					{
						Yandere.Blur.blurIterations++;
					}
				}
				else
				{
					Yandere.Blur.blurSize = Mathf.Lerp(Yandere.Blur.blurSize, 0, Time.deltaTime * 10);

					if (Yandere.Blur.blurSize < Yandere.Blur.blurIterations)
					{
						Yandere.Blur.blurIterations--;
					}
				}
			}

			this.AttackTimer += Time.deltaTime;

			WeaponScript weapon = this.Yandere.EquippedWeapon;
			SanityType sanityType = this.Yandere.SanityType;

			this.SpecialEffect(weapon, sanityType);
			
			if (sanityType == SanityType.Low)
			{
				this.LoopCheck(weapon);
			}

			this.SpecialEffect(weapon, sanityType);

			if (YandereAnim[this.AnimName].time >
				(YandereAnim[this.AnimName].length - (1.0f / 3.0f)))
			{
				YandereAnim.CrossFade(AnimNames.FemaleIdle);
				weapon.Flip = false;
			}

			if (this.AttackTimer > YandereAnim[this.AnimName].length)
			{
				if (this.Yandere.TargetStudent == this.Yandere.StudentManager.Reporter)
				{
					this.Yandere.StudentManager.Reporter = null;
				}

				if (!this.Yandere.CanTranq)
				{
					this.Yandere.TargetStudent.DeathType = DeathType.Weapon;
				}
				else
				{
					this.Yandere.TargetStudent.Tranquil = true;
					this.Yandere.NoStainGloves = true;
					this.Yandere.CanTranq = false;
					this.Yandere.StainWeapon();

                    this.Yandere.Follower = null;
					this.Yandere.Followers--;

					weapon.Type = WeaponType.Knife;
				}

				this.Yandere.TargetStudent.DeathCause = weapon.WeaponID;

				this.Yandere.TargetStudent.BecomeRagdoll();

				this.Yandere.Sanity -= ((PlayerGlobals.PantiesEquipped == 10) ? 10.0f : 20.0f) * this.Yandere.Numbness;

				this.Yandere.Attacking = false;
				this.Yandere.FollowHips = false;
				this.Yandere.HipCollider.enabled = false;

				bool DoNotMarkAsEvidence;

				DoNotMarkAsEvidence = false;

				if (this.Yandere.EquippedWeapon.Type == WeaponType.Bat)// && this.Stealth)
				{
					DoNotMarkAsEvidence = true;
				}

				if (!DoNotMarkAsEvidence)
				{
					this.Yandere.EquippedWeapon.Evidence = true;
				}

				this.Victim = null;
				this.VictimAnimName = null;
				this.AnimName = null;

				this.Stealth = false;

				this.EffectPhase = 0;
				this.AttackTimer = 0.0f;
				this.Timer = 0.0f;

				this.CheckForSpecialCase(weapon);

				this.Yandere.Blur.enabled = false;
				this.Yandere.Blur.blurSize = 0;

                if (weapon.Blunt)
                {
                    this.Yandere.TargetStudent.Ragdoll.NeckSnapped = true;
                    this.Yandere.TargetStudent.NeckSnapped = true;
                }

				if (!this.Yandere.Noticed)
				{
					//Debug.Log("Finished attacking.");

					this.Yandere.EquippedWeapon.MurderWeapon = true;
					this.Yandere.CanMove = true;
				}
				else
				{
					weapon.Drop();
				}
			}
		}
	}

	void SpecialEffect(WeaponScript weapon, SanityType sanityType)
	{
		this.BloodEffect = this.OriginalBloodEffect;

		if (weapon.WeaponID == 14)
		{
			this.BloodEffect = weapon.HeartBurst;
		}

		///////////////////////////////
		///// SHORT SHARP WEAPONS /////
		///////////////////////////////

		if (weapon.Type == WeaponType.Knife)
		{
			if (!this.Stealth)
			{
				if (sanityType == SanityType.High)
				{
					if (this.EffectPhase == 0)
					{
						if (YandereAnim[this.AnimName].time > (32.0f / 30.0f))
						{
							this.Yandere.Bloodiness += 20.0f;
							this.Yandere.StainWeapon();

							Instantiate(this.BloodEffect,
								weapon.transform.position +
								(weapon.transform.forward * 0.10f), Quaternion.identity);
							this.EffectPhase++;
						}
					}
				}
				else if (sanityType == SanityType.Medium)
				{
					if (this.EffectPhase == 0)
					{
						if (YandereAnim[this.AnimName].time > (65.0f / 30.0f))
						{
							this.Yandere.Bloodiness += 20.0f;
							this.Yandere.StainWeapon();

							Instantiate(this.BloodEffect,
								weapon.transform.position +
								(weapon.transform.forward * 0.10f), Quaternion.identity);
							this.EffectPhase++;
						}
					}
					else if (this.EffectPhase == 1)
					{
						if (YandereAnim[this.AnimName].time > (91.0f / 30.0f))
						{
							Instantiate(this.BloodEffect,
								weapon.transform.position +
								(weapon.transform.forward * 0.10f), Quaternion.identity);
							this.EffectPhase++;
						}
					}
				}
				else
				{
					if (this.EffectPhase == 0)
					{
						if (YandereAnim[this.AnimName].time > (83.0f / 30.0f))
						{
							this.Yandere.Bloodiness += 20.0f;
							this.Yandere.StainWeapon();
							
							Instantiate(this.BloodEffect,
								weapon.transform.position +
								(weapon.transform.forward * 0.10f), Quaternion.identity);
							this.EffectPhase++;
						}
					}
					else if (this.EffectPhase == 1)
					{
						if (YandereAnim[this.AnimName].time > (106.0f / 30.0f))
						{
							Instantiate(this.BloodEffect,
								weapon.transform.position +
								(weapon.transform.forward * 0.10f), Quaternion.identity);
							this.EffectPhase++;
						}
					}
					else if (this.EffectPhase == 2)
					{
						if (YandereAnim[this.AnimName].time > (125.0f / 30.0f))
						{
							Instantiate(this.BloodEffect,
								weapon.transform.position +
								(weapon.transform.forward * 0.10f), Quaternion.identity);
							this.EffectPhase++;
						}
					}
				}
			}
			else
			{
				if (this.EffectPhase == 0)
				{
					if (YandereAnim[this.AnimName].time > (29.0f / 30.0f))
					{
						this.Yandere.Bloodiness += 20.0f;
						this.Yandere.StainWeapon();

						Instantiate(this.BloodEffect,
							weapon.transform.position +
							(weapon.transform.forward * 0.10f), Quaternion.identity);
						this.EffectPhase++;
					}
				}
			}
		}

		//////////////////////////////
		///// LONG SHARP WEAPONS /////
		//////////////////////////////

		else if (weapon.Type == WeaponType.Katana)
		{
			if (!this.Stealth)
			{
				if (sanityType == SanityType.High)
				{
					if (this.EffectPhase == 0)
					{
						if (YandereAnim[this.AnimName].time > (14.50f / 30.0f))
						{
							this.Yandere.Bloodiness += 20.0f;
							this.Yandere.StainWeapon();

							Instantiate(this.BloodEffect,
								weapon.transform.position +
								(weapon.transform.forward * 0.50f), Quaternion.identity);
							this.EffectPhase++;
						}
					}
				}
				else if (sanityType == SanityType.Medium)
				{
					if (this.EffectPhase == 0)
					{
						if (YandereAnim[this.AnimName].time > (16.50f / 30.0f))
						{
							this.Yandere.Bloodiness += 20.0f;
							this.Yandere.StainWeapon();

							Instantiate(this.BloodEffect,
								weapon.transform.position +
								(weapon.transform.forward * 0.50f), Quaternion.identity);
							this.EffectPhase++;
						}
					}
					else if (this.EffectPhase == 1)
					{
						if (YandereAnim[this.AnimName].time > (45.50f / 30.0f))
						{
							Instantiate(this.BloodEffect,
								weapon.transform.position +
								(weapon.transform.forward * 0.50f), Quaternion.identity);
							this.EffectPhase++;
						}
					}
				}
				else
				{
					if (this.EffectPhase == 0)
					{
						if (YandereAnim[this.AnimName].time > (15.0f / 30.0f))
						{
							this.Yandere.Bloodiness += 20.0f;
							this.Yandere.StainWeapon();

							Instantiate(this.BloodEffect,
								weapon.transform.position +
								(weapon.transform.forward * (2.0f / 3.0f)), Quaternion.identity);
							this.EffectPhase++;
						}
					}
					else if (this.EffectPhase == 1)
					{
						if (YandereAnim[this.AnimName].time > (30.0f / 30.0f))
						{
							weapon.transform.localEulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
							this.EffectPhase++;
						}
					}
					else if (this.EffectPhase == 2)
					{
						if (YandereAnim[this.AnimName].time > (70.0f / 30.0f))
						{
							Instantiate(this.BloodEffect,
								weapon.transform.position +
								(weapon.transform.forward * (2.0f / 3.0f)), Quaternion.identity);
							this.EffectPhase++;
						}
					}
					else if (this.EffectPhase == 3)
					{
						if (YandereAnim[this.AnimName].time > (82.0f / 30.0f))
						{
							Instantiate(this.BloodEffect,
								weapon.transform.position +
								(weapon.transform.forward * (2.0f / 3.0f)), Quaternion.identity);
							this.EffectPhase++;
						}
					}
					else if (this.EffectPhase == 4)
					{
						if (YandereAnim[this.AnimName].time > (94.0f / 30.0f))
						{
							Instantiate(this.BloodEffect,
								weapon.transform.position +
								(weapon.transform.forward * (2.0f / 3.0f)), Quaternion.identity);
							this.EffectPhase++;
						}
					}
					else if (this.EffectPhase == 5)
					{
						if (YandereAnim[this.AnimName].time > (106.0f / 30.0f))
						{
							Instantiate(this.BloodEffect,
								weapon.transform.position +
								(weapon.transform.forward * (2.0f / 3.0f)), Quaternion.identity);
							this.EffectPhase++;
						}
					}
					else if (this.EffectPhase == 6)
					{
						if (YandereAnim[this.AnimName].time > (124.0f / 30.0f))
						{
							Instantiate(this.BloodEffect,
								weapon.transform.position +
								(weapon.transform.forward * (2.0f / 3.0f)), Quaternion.identity);
							this.EffectPhase++;
						}
					}
					// [af] Commented in JS code.
					/*
					else if (this.EffectPhase == 7)
					{
						if (YandereAnim[this.AnimName].time > (130.0f / 30.0f))
						{
							Instantiate(this.BloodEffect, 
								weapon.transform.position + 
								(weapon.transform.forward * (2.0f / 3.0f)), Quaternion.identity);
							this.EffectPhase++;
						}
					}
					*/
					else if (this.EffectPhase == 8)
					{
						if (YandereAnim[this.AnimName].time > (150.0f / 30.0f))
						{
							weapon.transform.localEulerAngles = Vector3.zero;
							this.EffectPhase++;
						}
					}
				}
			}
			else
			{
				if (this.EffectPhase == 0)
				{
					if (YandereAnim[this.AnimName].time > (11.0f / 30.0f))
					{
						this.Yandere.Bloodiness += 20.0f;
						this.Yandere.StainWeapon();

						Instantiate(this.BloodEffect,
							weapon.transform.position +
							(weapon.transform.forward * (2.0f / 3.0f)), Quaternion.identity);
						this.EffectPhase++;
					}
				}
				else if (this.EffectPhase == 1)
				{
					if (YandereAnim[this.AnimName].time > (30.0f / 30.0f))
					{
						Instantiate(this.BloodEffect,
							weapon.transform.position +
							(weapon.transform.forward * (1.0f / 3.0f)), Quaternion.identity);
						this.EffectPhase++;
					}
				}
			}
		}

		//////////////////////////////
		///// LONG BLUNT WEAPONS /////
		//////////////////////////////

		else if (weapon.Type == WeaponType.Bat)
		{
			if (!this.Stealth)
			{
				if (sanityType == SanityType.High)
				{
					if (this.EffectPhase == 0)
					{
						if (YandereAnim[this.AnimName].time > (22.0f / 30.0f))
						{
                            if (!weapon.Blunt)
                            {
                                this.Yandere.Bloodiness += 20.0f;
                                this.Yandere.StainWeapon();
                            }

                            Instantiate(this.BloodEffect,
								weapon.transform.position +
								(weapon.transform.forward * 0.50f), Quaternion.identity);
							this.EffectPhase++;
						}
					}
				}
				else if (sanityType == SanityType.Medium)
				{
					if (this.EffectPhase == 0)
					{
						if (YandereAnim[this.AnimName].time > (30.0f / 30.0f))
						{
                            if (!weapon.Blunt)
                            {
                                this.Yandere.Bloodiness += 20.0f;
                                this.Yandere.StainWeapon();
                            }

                            Instantiate(this.BloodEffect,
								weapon.transform.position +
								(weapon.transform.forward * 0.50f), Quaternion.identity);
							this.EffectPhase++;
						}
					}
					else if (this.EffectPhase == 1)
					{
						if (YandereAnim[this.AnimName].time > (89.0f / 30.0f))
						{
							Instantiate(this.BloodEffect,
								weapon.transform.position +
								(weapon.transform.forward * 0.50f), Quaternion.identity);
							this.EffectPhase++;
						}
					}
				}
				else
				{
					if (this.EffectPhase == 0)
					{
						if (YandereAnim[this.AnimName].time > (21.0f / 30.0f))
						{
                            if (!weapon.Blunt)
                            {
                                this.Yandere.Bloodiness += 20.0f;
                                this.Yandere.StainWeapon();
                            }

                            Instantiate(this.BloodEffect,
								weapon.transform.position +
								(weapon.transform.forward * 0.50f), Quaternion.identity);
							this.EffectPhase++;
						}
					}
					else if (this.EffectPhase == 1)
					{
						if (YandereAnim[this.AnimName].time > (93.0f / 30.0f))
						{
							Instantiate(this.BloodEffect,
								weapon.transform.position +
								(weapon.transform.forward * 0.50f), Quaternion.identity);
							this.EffectPhase++;
						}
					}
					else if (this.EffectPhase == 2)
					{
						if (YandereAnim[this.AnimName].time > (113.0f / 30.0f))
						{
							Instantiate(this.BloodEffect,
								weapon.transform.position +
								(weapon.transform.forward * 0.50f), Quaternion.identity);
							this.EffectPhase++;
						}
					}
					else if (this.EffectPhase == 3)
					{
						if (YandereAnim[this.AnimName].time > (132.0f / 30.0f))
						{
							Instantiate(this.BloodEffect,
								weapon.transform.position +
								(weapon.transform.forward * 0.50f), Quaternion.identity);
							this.EffectPhase++;
						}
					}
				}
			}
			else
			{
				this.Yandere.TargetStudent.Ragdoll.NeckSnapped = true;
                this.Yandere.TargetStudent.NeckSnapped = true;
            }
		}

		////////////////////////
		///// CIRCULAR SAW /////
		////////////////////////

		else if (weapon.Type == WeaponType.Saw)
		{
			if (!this.Stealth)
			{
				if (sanityType == SanityType.High)
				{
					if (this.EffectPhase == 0)
					{
						if (YandereAnim[this.AnimName].time > (0.0f / 30.0f))
						{
							weapon.Spin = true;
							this.EffectPhase++;
						}
					}
					else if (this.EffectPhase == 1)
					{
						if (YandereAnim[this.AnimName].time > (22.0f / 30.0f))
						{
							this.Yandere.Bloodiness += 20.0f;
							this.Yandere.StainWeapon();

							weapon.BloodSpray[0].Play();
							weapon.BloodSpray[1].Play();
							this.EffectPhase++;
						}
					}
					else if (this.EffectPhase == 2)
					{
						if (YandereAnim[this.AnimName].time > (43.0f / 30.0f))
						{
							weapon.Spin = false;
							weapon.BloodSpray[0].Stop();
							weapon.BloodSpray[1].Stop();
							this.EffectPhase++;
						}
					}
				}
				else if (sanityType == SanityType.Medium)
				{
					if (this.EffectPhase == 0)
					{
						if (YandereAnim[this.AnimName].time > (0.0f / 30.0f))
						{
							weapon.Spin = true;
							this.EffectPhase++;
						}
					}
					else if (this.EffectPhase == 1)
					{
						if (YandereAnim[this.AnimName].time > (33.0f / 30.0f))
						{
							this.Yandere.Bloodiness += 20.0f;
							this.Yandere.StainWeapon();

							weapon.BloodSpray[0].Play();
							weapon.BloodSpray[1].Play();
							this.EffectPhase++;
						}
					}
					else if (this.EffectPhase == 2)
					{
						if (YandereAnim[this.AnimName].time > (43.0f / 30.0f))
						{
							weapon.BloodSpray[0].Stop();
							weapon.BloodSpray[1].Stop();
							this.EffectPhase++;
						}
					}
					else if (this.EffectPhase == 3)
					{
						if (YandereAnim[this.AnimName].time > (71.0f / 30.0f))
						{
							weapon.BloodSpray[0].Play();
							weapon.BloodSpray[1].Play();
							this.EffectPhase++;
						}
					}
					else if (this.EffectPhase == 4)
					{
						if (YandereAnim[this.AnimName].time > (72.0f / 30.0f))
						{
							weapon.Spin = true;
							weapon.BloodSpray[0].Stop();
							weapon.BloodSpray[1].Stop();
							this.EffectPhase++;
						}
					}
				}
				else
				{
					if (this.EffectPhase == 0)
					{
						if (YandereAnim[this.AnimName].time > (0.0f / 30.0f))
						{
							weapon.Spin = true;
							this.EffectPhase++;
						}
					}
					else if (this.EffectPhase == 1)
					{
						if (YandereAnim[this.AnimName].time > (20.0f / 30.0f))
						{
							this.Yandere.Bloodiness += 20.0f;
							this.Yandere.StainWeapon();

							weapon.BloodSpray[0].Play();
							weapon.BloodSpray[1].Play();
							this.EffectPhase++;
						}
					}
					else if (this.EffectPhase == 2)
					{
						if (YandereAnim[this.AnimName].time > (22.0f / 30.0f))
						{
							weapon.BloodSpray[0].Stop();
							weapon.BloodSpray[1].Stop();
							this.EffectPhase++;
						}
					}
					else if (this.EffectPhase == 3)
					{
						if (YandereAnim[this.AnimName].time > (90.0f / 30.0f))
						{
							weapon.BloodSpray[0].Play();
							weapon.BloodSpray[1].Play();
							this.EffectPhase++;
						}
					}
					else if (this.EffectPhase == 4)
					{
						if (YandereAnim[this.AnimName].time > (146.0f / 30.0f))
						{
							weapon.Spin = false;
							weapon.BloodSpray[0].Stop();
							weapon.BloodSpray[1].Stop();
							this.EffectPhase++;
						}
					}
				}
			}
			else
			{
				if (this.EffectPhase == 0)
				{
					if (YandereAnim[this.AnimName].time > (30.0f / 30.0f))
					{
						this.Yandere.Bloodiness += 20.0f;
						this.Yandere.StainWeapon();

						Instantiate(this.BloodEffect,
							weapon.transform.position +
							(weapon.transform.right * 0.20f) +
							(weapon.transform.forward * (-1.0f / 15.0f)), Quaternion.identity);
						this.EffectPhase++;
					}
				}
			}
		}

		///////////////////////////////
		///// SHORT BLUNT WEAPONS /////
		///////////////////////////////

		else if (weapon.Type == WeaponType.Weight)
		{
			if (!this.Stealth)
			{
				if (sanityType == SanityType.High)
				{
					if (this.EffectPhase == 0)
					{
						if (YandereAnim[this.AnimName].time > (20.0f / 30.0f))
						{
                            if (!weapon.Blunt)
                            {
                                this.Yandere.Bloodiness += 20.0f;
                                this.Yandere.StainWeapon();
                            }

                            Instantiate(this.BloodEffect,
								weapon.transform.position +
								(weapon.transform.forward * 0.10f), Quaternion.identity);
							this.EffectPhase++;
						}
					}
				}
				else if (sanityType == SanityType.Medium)
				{
					if (this.EffectPhase == 0)
					{
						if (YandereAnim[this.AnimName].time > (30.0f / 30.0f))
						{
                            if (!weapon.Blunt)
                            {
                                this.Yandere.Bloodiness += 20.0f;
                                this.Yandere.StainWeapon();
                            }

                            Instantiate(this.BloodEffect,
								weapon.transform.position +
								(weapon.transform.forward * 0.10f), Quaternion.identity);
							this.EffectPhase++;
						}
					}
					else if (this.EffectPhase == 1)
					{
						if (YandereAnim[this.AnimName].time > (85.0f / 30.0f))
						{
							Instantiate(this.BloodEffect,
								weapon.transform.position +
								(weapon.transform.forward * 0.10f), Quaternion.identity);
							this.EffectPhase++;
						}
					}
				}
				else
				{
					if (this.EffectPhase == 0)
					{
						if (YandereAnim[this.AnimName].time > (65.0f / 30.0f))
						{
                            if (!weapon.Blunt)
                            {
                                this.Yandere.Bloodiness += 20.0f;
                                this.Yandere.StainWeapon();
                            }

                            Instantiate(this.BloodEffect,
								weapon.transform.position +
								(weapon.transform.forward * 0.10f), Quaternion.identity);
							this.EffectPhase++;
						}
					}
					else if (this.EffectPhase == 1)
					{
						if (YandereAnim[this.AnimName].time > (125.0f / 30.0f))
						{
							Instantiate(this.BloodEffect,
								weapon.transform.position +
								(weapon.transform.forward * 0.10f), Quaternion.identity);
							this.EffectPhase++;
						}
					}
				}
			}
			else
			{
				this.Yandere.TargetStudent.Ragdoll.NeckSnapped = true;
                this.Yandere.TargetStudent.NeckSnapped = true;
            }
		}

        ///////////////////
        ///// GARROTE /////
        ///////////////////

        else if (weapon.Type == WeaponType.Garrote)
        {
            this.Yandere.TargetStudent.Ragdoll.NeckSnapped = true;
            this.Yandere.TargetStudent.NeckSnapped = true;
        }
    }

	void LoopCheck(WeaponScript weapon)
	{
		if (Input.GetButtonDown(InputNames.Xbox_X) && !this.Yandere.Chased && this.Yandere.Chasers == 0)
		{
			///////////////////////////////
			///// SHORT SHARP WEAPONS /////
			///////////////////////////////

			if (weapon.Type == WeaponType.Knife)
			{
				if (YandereAnim[this.AnimName].time > (106.0f / 30.0f) &&
					YandereAnim[this.AnimName].time < (125.0f / 30.0f))
				{
					this.LoopStart = 106.0f;
					this.LoopEnd = 125.0f;
					this.LoopPhase = 2;
					this.Loop = true;
				}
			}

			//////////////////////////////
			///// LONG SHARP WEAPONS /////
			//////////////////////////////

			else if (weapon.Type == WeaponType.Katana)
			{
				if (YandereAnim[this.AnimName].time > (101.0f / 30.0f) &&
					YandereAnim[this.AnimName].time < (117.0f / 30.0f))
				{
					this.LoopStart = 101.0f;
					this.LoopEnd = 117.0f;
					this.LoopPhase = 5;
					this.Loop = true;
				}
			}

			//////////////////////////////
			///// LONG BLUNT WEAPONS /////
			//////////////////////////////

			else if (weapon.Type == WeaponType.Bat)
			{
				if (YandereAnim[this.AnimName].time > (113.0f / 30.0f) &&
					YandereAnim[this.AnimName].time < (132.0f / 30.0f))
				{
					this.LoopStart = 113.0f;
					this.LoopEnd = 132.0f;
					this.LoopPhase = 2;
					this.Loop = true;
				}
			}

			////////////////////////
			///// CIRCULAR SAW /////
			////////////////////////

			else if (weapon.Type == WeaponType.Saw)
			{
				if (YandereAnim[this.AnimName].time > (91.0f / 30.0f) &&
					YandereAnim[this.AnimName].time < (137.0f / 30.0f))
				{
					this.LoopStart = 91.0f;
					this.LoopEnd = 137.0f;
					this.LoopPhase = 3;
					this.PingPong = true;
				}
			}

			///////////////////////////////
			///// SHORT BLUNT WEAPONS /////
			///////////////////////////////

			else if (weapon.Type == WeaponType.Weight)
			{
				if (YandereAnim[this.AnimName].time > (90.0f / 30.0f) &&
					YandereAnim[this.AnimName].time < (135.0f / 30.0f))
				{
					this.LoopStart = 90.0f;
					this.LoopEnd = 135.0f;
					this.LoopPhase = 1;
					this.Loop = true;
				}
			}
		}

		if (this.PingPong)
		{
			if (YandereAnim[this.AnimName].time > (this.LoopEnd / 30.0f))
			{
				weapon.MyAudio.pitch = 1.0f + Random.Range(0.10f, -0.10f);
                weapon.MyAudio.time = this.LoopStart / 30.0f;

				VictimAnim[this.VictimAnimName].speed = -1.0f;
				YandereAnim[this.AnimName].speed = -1.0f;

				this.EffectPhase = this.LoopPhase;

				this.AttackTimer = 0.0f;
			}
			else if (YandereAnim[this.AnimName].time < (this.LoopStart / 30.0f))
			{
                weapon.MyAudio.pitch = 1.0f + Random.Range(0.10f, -0.10f);
                weapon.MyAudio.time = this.LoopStart / 30.0f;

				VictimAnim[this.VictimAnimName].speed = 1.0f;
				YandereAnim[this.AnimName].speed = 1.0f;

				this.EffectPhase = this.LoopPhase;

				this.AttackTimer = this.LoopStart / 30.0f;

				this.EffectPhase = this.LoopPhase;

				this.PingPong = false;
			}
		}

		if (this.Loop)
		{
			if (YandereAnim[this.AnimName].time > (this.LoopEnd / 30.0f))
			{
                weapon.MyAudio.pitch = 1.0f + Random.Range(0.10f, -0.10f);
                weapon.MyAudio.time = this.LoopStart / 30.0f;

				VictimAnim[this.VictimAnimName].time = this.LoopStart / 30.0f;
				YandereAnim[this.AnimName].time = this.LoopStart / 30.0f;

				this.AttackTimer = this.LoopStart / 30.0f;
				this.EffectPhase = this.LoopPhase;

				this.Loop = false;
			}
		}
	}

	void CheckForSpecialCase(WeaponScript weapon)
	{
		if (weapon.WeaponID == 8)
		{
			if (GameGlobals.Paranormal)
			{
				this.Yandere.TargetStudent.Ragdoll.Sacrifice = true;
				weapon.Effect();
			}
		}
	}
}
