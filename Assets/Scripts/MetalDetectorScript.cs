using UnityEngine;

public class MetalDetectorScript : MonoBehaviour
{
	public MissionModeScript MissionMode;
	public YandereScript Yandere;
	public PromptScript Prompt;

	public ParticleSystem PepperSprayEffect;

	public AudioSource MyAudio;

	public AudioClip PepperSpraySFX;
	public AudioClip Alarm;

	public Collider MyCollider;

	public float SprayTimer;
	public bool Spraying;

	void Start()
	{
		this.MyAudio = this.GetComponent<AudioSource>();
	}

	void Update()
	{
		if (this.Yandere.Armed)
		{
			if (this.Yandere.EquippedWeapon.WeaponID == 6)
			{
				this.Prompt.enabled = true;

				if (this.Prompt.Circle[0].fillAmount == 0.0f)
				{
					this.MyAudio.Play();

					this.MyCollider.enabled = false;

					this.Prompt.Hide();
					this.Prompt.enabled = false;

					this.enabled = false;
				}
			}
			else
			{
				if (this.Prompt.enabled)
				{
					this.Prompt.Hide();
					this.Prompt.enabled = false;
				}
			}
		}
		else
		{
			if (this.Prompt.enabled)
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
			}
		}

		if (this.Spraying == true)
		{
			this.SprayTimer += Time.deltaTime ;

			if (this.SprayTimer > .66666)
			{
				if (this.Yandere.Armed)
				{
					this.Yandere.EquippedWeapon.Drop();
				}

				this.Yandere.EmptyHands();

				this.PepperSprayEffect.Play();
				this.Spraying = false;
			}
		}

		this.MyAudio.volume -= Time.deltaTime * .01f;
	}

	void OnTriggerStay(Collider other)
	{
		if (this.Yandere.enabled)
		{
			bool CarryingMetal = false;

			if (this.MissionMode.GameOverID == 0)
			{
				if (other.gameObject.layer == 13)
				{
					// [af] Replaced several if/else statements with loop and or operator.
					for (int i = 1; i < 4; i++)
					{
						WeaponScript weapon = this.Yandere.Weapon[i];

						CarryingMetal |= (weapon != null) && (weapon.Metal);

						if (!CarryingMetal)
						{
							if (this.Yandere.Container != null)
							{
								if (this.Yandere.Container.Weapon != null)
								{
									weapon = this.Yandere.Container.Weapon;
									CarryingMetal = weapon.Metal;
								}
							}

							if (this.Yandere.PickUp != null)
							{
								if (this.Yandere.PickUp.TrashCan != null)
								{
									if (this.Yandere.PickUp.TrashCan.Weapon)
									{
										weapon = this.Yandere.PickUp.TrashCan.Item.GetComponent<WeaponScript>();
										CarryingMetal = weapon.Metal;
									}
								}

								if (this.Yandere.PickUp.StuckBoxCutter != null)
								{
									weapon = this.Yandere.PickUp.StuckBoxCutter;
									CarryingMetal = true;
								}
							}
						}
					}

					if (CarryingMetal)
					{
						if (!this.Yandere.Inventory.IDCard)
						{
							if (this.MissionMode.enabled)
							{
								this.MissionMode.GameOverID = 16;
								this.MissionMode.GameOver();
								this.MissionMode.Phase = 4;
								this.enabled = false;
							}
							else
							{
								if (this.Yandere.Sprayed == false)
								{
									this.MyAudio.clip = Alarm;
									this.MyAudio.loop = true;
									this.MyAudio.Play();
									this.MyAudio.volume = .1f;

									AudioSource.PlayClipAtPoint(this.PepperSpraySFX, this.transform.position);

									if (this.Yandere.Aiming)
									{
										this.Yandere.StopAiming();
									}

									this.PepperSprayEffect.transform.position = new Vector3 (
										this.transform.position.x,
										this.Yandere.transform.position.y + 1.8f,
										this.Yandere.transform.position.z);

									this.Spraying = true;

									this.Yandere.CharacterAnimation.CrossFade("f02_sprayed_00");
									this.Yandere.FollowHips = true;
									this.Yandere.Punching = false;
									this.Yandere.CanMove = false;
									this.Yandere.Sprayed = true;

									this.Yandere.StudentManager.YandereDying = true;
									this.Yandere.StudentManager.StopMoving();

									this.Yandere.Blur.blurIterations = 1;
									this.Yandere.Jukebox.Volume = 0;

									Time.timeScale = 1;
								}
							}
						}
					}
				}
			}
		}
	}
}