using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGPMEnemyScript : MonoBehaviour
{
	public MGPMManagerScript GameplayManager;
	public MGPMMiyukiScript Miyuki;

	public AudioSource MyAudio;

	public GameObject FinalBossExplosion;
	public GameObject Projectile;
	public GameObject Explosion;
	public GameObject PickUp;
	public GameObject Impact;

	public Renderer ExtraRenderer;
	public Renderer MyRenderer;

	public Collider MyCollider;

	public Transform HealthBar;

	public Texture[] Sprite;

	public int Pattern;
	public int Health;
	public int Frame;
	public int Phase;
	public int Side;

	public float AttackFrequency;
	public float ExplosionTimer;
	public float AttackTimer;
	public float DeathTimer;
	public float PhaseTimer;
	public float FlashWhite;
	public float Rotation;
	public float Speed;
	public float Timer;
	public float Spin;
	public float FPS;

	public float PositionX;
	public float PositionY;

	public bool ShootEverywhere;

	public bool QuintupleAttack;
	public bool SextupleAttack;
	public bool DoubleAttack;
	public bool TripleAttack;
	public bool Homing;

	void Start()
	{
		if (Pattern != 10)
		{
			if (GameGlobals.HardMode)
			{
				Health += 6;
			}
		}

		if (transform.localPosition.x < 0){Side = 1;}
		else {Side = -1;}

		if (Pattern == 11)
		{
			MyCollider.enabled = false;
		}

		if (GameplayManager.GameOver)
		{
			MyAudio.volume = 0;
			AttackFrequency = 0;
		}
	}

	void Update()
	{
		if (Health > 0)
		{
			/////////////////////
			///// ANIMATION /////
			/////////////////////

			Timer += Time.deltaTime;

			if (Timer > FPS)
			{
				Timer = 0;

				Frame++;

				if (Frame == Sprite.Length)
				{
					Frame = 0;
				}

				MyRenderer.material.mainTexture = Sprite[Frame];

				if (ExtraRenderer != null)
				{
					ExtraRenderer.material.mainTexture = Sprite[Frame];
				}
			}

			////////////////////
			///// MOVEMENT /////
			////////////////////

			switch (Pattern)
			{
				//Original prototype test movement
				case 0:
					transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - Speed * Time.deltaTime, 0);
					Speed = Mathf.Lerp(Speed, 0, Time.deltaTime);
				break;

				//Lerp from above, then rotate 180 and fly away.
				case 1:
					if (Phase == 1)
					{
						transform.localPosition = Vector3.Lerp(transform.localPosition, Miyuki.transform.localPosition, Speed * Time.deltaTime);
						Speed = Mathf.Lerp(Speed, 0, Time.deltaTime);

						PhaseTimer += Time.deltaTime;

						if (PhaseTimer > 2)
						{
							AttackTimer = -100;
							Phase++;
						}
					}
					else
					{
						Rotation = Mathf.Lerp(Rotation, 90 * Side, Speed * Time.deltaTime);

						transform.localEulerAngles = new Vector3(
							transform.localEulerAngles.x,
							transform.localEulerAngles.y,
							Rotation);

						transform.Translate(transform.up * -1 * Speed * Time.deltaTime);
						Speed += Time.deltaTime;

						if (transform.localPosition.y > 288)
						{
							Destroy(gameObject);
						}
					}
				break;
				
				//Side-to-side snaking motion.
				case 2:
					transform.localPosition = new Vector3(
						transform.localPosition.x + Speed * Time.deltaTime,
						transform.localPosition.y - (100 * Time.deltaTime),
						transform.localPosition.z);

					if (Phase == 1)
					{
						Speed -= Time.deltaTime * 200;

						if (Speed < -200)
						{
							Phase++;
						}
					}
					else
					{
						Speed += Time.deltaTime * 200;

						if (Speed > 200)
						{
							Phase--;
						}
					}

					if (transform.localPosition.y < -288)
					{
						Destroy(gameObject);
					}
				break;
				
				//Straight down, facing down.
				case 3:
					transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - Speed * Time.deltaTime, 0);

					if (transform.localPosition.y < -288)
					{
						Destroy(gameObject);
					}
				break;
				
				//Straight down, facing player.
				case 4:

					transform.LookAt(Miyuki.transform.position);

					transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - Speed  * Time.deltaTime, 0);

					if (transform.localPosition.y < -288)
					{
						Destroy(gameObject);
					}
				break;
				
				//Lerp to middle, then lerp away.
				case 5:
					if (Phase == 1)
					{
						transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(
							transform.localPosition.x,
							0,
							transform.localPosition.z), Speed * Time.deltaTime);

						PhaseTimer += Time.deltaTime;

						if (PhaseTimer > 1)
						{
							Speed = 1;
							Phase++;
						}
					}
					else
					{
						Speed += Speed * Time.deltaTime * 2.5f;

						transform.localPosition = new Vector3(
							transform.localPosition.x,
							Speed * -1,
							transform.localPosition.z);
					}

					if (transform.localPosition.y < -288)
					{
						Destroy(gameObject);
					}
				break;
				
				//Lerp to top-middle of screen.
				case 6:
					transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(
						transform.localPosition.x,
						135,
						transform.localPosition.z), Speed * Time.deltaTime);
				break;
				
				//Move across screen left
				case 7:
					transform.localEulerAngles = new Vector3(0, 0, 90);

					transform.localPosition = new Vector3(
						transform.localPosition.x - Speed * Time.deltaTime,
						transform.localPosition.y - Speed * .25f * Time.deltaTime,
						transform.localPosition.z);

					if (transform.localPosition.x < -160)
					{
						Destroy(gameObject);
					}
				break;
				
				//Move across screen right
				case 8:
					transform.localEulerAngles = new Vector3(0, 0, -90);

					transform.localPosition = new Vector3(
						transform.localPosition.x + Speed * Time.deltaTime,
						transform.localPosition.y - Speed * .25f * Time.deltaTime,
						transform.localPosition.z);

					if (transform.localPosition.x > 160)
					{
						Destroy(gameObject);
					}
				break;

				//Miniboss side-to-side snaking motion.
				case 9:
					transform.localPosition = new Vector3(
						transform.localPosition.x + Speed  * Time.deltaTime,
						transform.localPosition.y - (20 * Time.deltaTime),
						transform.localPosition.z);

					if (transform.localPosition.x > 60)
					{
						transform.localPosition = new Vector3(
							60,
							transform.localPosition.y,
							transform.localPosition.z);
					}
					else if (transform.localPosition.x < -60)
					{
						transform.localPosition = new Vector3(
							-60,
							transform.localPosition.y,
							transform.localPosition.z);
					}

					if (Phase == 1)
					{
						Speed -= Time.deltaTime * 120;

						if (Speed < -120)
						{
							Phase++;
						}
					}
					else
					{
						Speed += Time.deltaTime * 120;

						if (Speed > 120)
						{
							Phase--;
						}
					}

					if (transform.localPosition.y < -288)
					{
						Destroy(gameObject);
					}
				break;

				//Kamikaze
				case 10:
					if (Phase == 1)
					{
						transform.LookAt(Miyuki.transform);
						Phase++;
					}
					else
					{
						transform.Translate(Vector3.forward * Speed * Time.deltaTime, Space.Self);
					}

					if (transform.localPosition.y < -288)
					{
						Destroy(gameObject);
					}
				break;

				//Final Boss
				case 11:
					if (Phase == 1)
					{
						transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(
						transform.localPosition.x,
						150,
						transform.localPosition.z), Speed * Time.deltaTime);

						PhaseTimer += Time.deltaTime;

						if (PhaseTimer > 5)
						{
							MyCollider.enabled = true;
							AttackFrequency = .5f;
							PhaseTimer = 0;
							Speed = 0;
							Phase++;
						}
					}
					else if (Phase == 2)
					{
						PhaseTimer += Time.deltaTime;

						if (PhaseTimer > 10)
						{
							QuintupleAttack = false;
							SextupleAttack = false;
							ShootEverywhere = true;

							AttackFrequency = .1f;
							PhaseTimer = 0;
							Speed = .1f;
							Spin = 45;
							Phase++;
						}
					}
					else if (Phase == 3)
					{
						PhaseTimer += Time.deltaTime;

						Speed += Speed * Time.deltaTime;

						transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(
						transform.localPosition.x,
						-214,
						transform.localPosition.z), Speed * Time.deltaTime);

						if (PhaseTimer > 5)
						{
							PhaseTimer = 0;
							Speed = .1f;
							Phase++;
						}
					}
					else if (Phase == 4)
					{
						PhaseTimer += Time.deltaTime;

						Speed += Speed * Time.deltaTime;

						transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(
						transform.localPosition.x,
						150,
						transform.localPosition.z), Speed * Time.deltaTime);

						if (PhaseTimer > 5)
						{
							QuintupleAttack = true;
							SextupleAttack = true;
							ShootEverywhere = false;

							AttackFrequency = .5f;
							PhaseTimer = 0;
							Phase = 2;
						}
					}
				break;
			}

			/////////////////////
			///// ATTACKING /////
			/////////////////////

			if (AttackFrequency > 0)
			{
				AttackTimer += Time.deltaTime;

				if (AttackTimer > AttackFrequency)
				{
					if (ShootEverywhere)
					{
						//Attack(2.5f, Random.Range(0.0f, 360.0f));

						Attack(5, Spin);
						Spin += 5;
					}
					else if (SextupleAttack)
					{
						Attack(5, 115);
						Attack(5, 105);
						Attack(5, 95);
						Attack(5, 85);
						Attack(5, 75);
						Attack(5, 65);

						QuintupleAttack = true;
						SextupleAttack = false;
					}
					else if (QuintupleAttack)
					{
						Attack(5, 105);
						Attack(5, 97.5f);
						Attack(5, 90);
						Attack(5, 82.5f);
						Attack(5, 75);

						QuintupleAttack = false;
						SextupleAttack = true;
					}
					else if (TripleAttack)
					{
						Attack(5, 90);
						Attack(5, 75);
						Attack(5, 105);
					}
					else if (DoubleAttack)
					{
						Attack(2.5f, 90);
						Attack(5, 90);
					}
					else
					{
						Attack(5, 90);
					}

					AttackTimer = 0;
				}
			}
		}
		else
		{
			if (Pattern < 11)
			{
				GameObject NewExplosion;

				NewExplosion = Instantiate(Explosion, transform.position, Quaternion.identity);
				NewExplosion.transform.parent = transform.parent;

				if (Pattern == 6 || Pattern == 9 || Pattern == 12)
				{
					NewExplosion.transform.localScale = new Vector3(128, 128, 1);
				}
				else
				{
					NewExplosion.transform.localScale = new Vector3(64, 64, 1);
				}

				if (GameGlobals.EasyMode)
				{
					GameObject NewPickUp;

					NewPickUp = Instantiate(PickUp, transform.position, Quaternion.identity);
					NewPickUp.transform.parent = transform.parent;
					NewPickUp.transform.localScale = new Vector3(16, 16, 1);
				}

				GameplayManager.Score += 100;
				Destroy(gameObject);
			}
			else
			{
				GameplayManager.Jukebox.volume -= Time.deltaTime * .1f;

				AttackFrequency = 0;
				Pattern = 100;

				DeathTimer += Time.deltaTime;

				if (DeathTimer < 5)
				{
					if (ExplosionTimer == 0)
					{
						GameObject NewExplosion;
						NewExplosion = Instantiate(Explosion, transform.position, Quaternion.identity);
						NewExplosion.transform.parent = transform.parent;

						NewExplosion.transform.localPosition += new Vector3(
							Random.Range(-100.0f, 100.0f),
							Random.Range(-50.0f, 50.0f),
							0);

						NewExplosion.transform.localScale = new Vector3(128, 128, 1);

						GameplayManager.Score += 100;

						ExplosionTimer = .1f;
					}
					else
					{
						ExplosionTimer = Mathf.MoveTowards(ExplosionTimer, 0, Time.deltaTime);
					}
				}
				else
				{
					GameObject NewExplosion;
					NewExplosion = Instantiate(FinalBossExplosion, transform.position, Quaternion.identity);
					NewExplosion.transform.parent = transform.parent;
					NewExplosion.transform.localScale = new Vector3(256, 256, 1);

					GameplayManager.StageClear = true;
					GameplayManager.Score += 1000;

					Destroy(gameObject);
				}
			}
		}

		if (FlashWhite > 0)
		{
			FlashWhite = Mathf.MoveTowards(FlashWhite, 0, Time.deltaTime);

			if (FlashWhite == 0)
			{
				MyRenderer.material.SetColor("_EmissionColor", new Color(0, 0, 0, 0));

				if (ExtraRenderer != null)
				{
					ExtraRenderer.material.SetColor("_EmissionColor", new Color(0, 0, 0, 0));
				}
			}
		}

		#if UNITY_EDITOR

		if (Input.GetKeyDown("space"))
		{
			if (Pattern < 11)
			{
				if (GameGlobals.EasyMode)
				{
					GameObject NewPickUp;

					NewPickUp = Instantiate(PickUp, transform.position, Quaternion.identity);
					NewPickUp.transform.parent = transform.parent;
					NewPickUp.transform.localScale = new Vector3(16, 16, 1);
				}

				GameplayManager.Score += 100;
				Destroy(gameObject);
			}
			else
			{
				Health = 10;
			}
		}

		#endif
	}

	void Attack(float AttackSpeed, float AttackRotation)
	{
		GameObject NewProjectile;

		NewProjectile = Instantiate(Projectile, transform.position, Quaternion.identity);
		NewProjectile.transform.parent = transform.parent;
		NewProjectile.transform.localScale = new Vector3(1, 1, 1);
		NewProjectile.transform.localPosition = new Vector3(NewProjectile.transform.localPosition.x, NewProjectile.transform.localPosition.y, 1);

		if (Homing)
		{
			NewProjectile.transform.LookAt(Miyuki.transform);
		}
		else
		{
			NewProjectile.transform.localEulerAngles = new Vector3(AttackRotation, 90, 0);
		}

		NewProjectile.GetComponent<MGPMProjectileScript>().Speed = AttackSpeed;
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == 8)
		{
			GameObject BulletImpact;

			BulletImpact = Instantiate(Impact, transform.position, Quaternion.identity);
			BulletImpact.transform.parent = transform.parent;
			BulletImpact.transform.localScale = new Vector3(32, 32, 32);
			BulletImpact.transform.localPosition = new Vector3(collision.gameObject.transform.localPosition.x, collision.gameObject.transform.localPosition.y, 1);

			MyRenderer.material.SetColor("_EmissionColor", new Color(1, 1, 1, 1));

			if (ExtraRenderer != null)
			{
				ExtraRenderer.material.SetColor("_EmissionColor", new Color(1, 1, 1, 1));
			}

			Destroy(collision.gameObject);
			FlashWhite = .05f;
			Health--;

			if (Health == 0)
			{
				MyCollider.enabled = false;
			}

			if (HealthBar != null)
			{
				HealthBar.localScale = new Vector3(
					Health / 500.0f,
					1,
					1);
			}
		}
	}
}