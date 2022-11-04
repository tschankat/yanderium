using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGPMMiyukiScript : MonoBehaviour
{
	public MGPMManagerScript GameplayManager;

	public InputManagerScript InputManager;

	public AudioClip DamageSound;
	public AudioClip PickUpSound;
	public AudioClip DeathSound;

	public GameObject Projectile;

	public GameObject DeathExplosion;
	public GameObject Explosion;

	public Transform SpawnPoint;
	public Transform MagicBar;

	public Renderer MyRenderer;

	public Texture[] ForwardSprite;

	public Texture[] ReverseRightSprite;
	public Texture[] TurnRightSprite;
	public Texture[] RightSprite;

	public Texture[] ReverseLeftSprite;
	public Texture[] TurnLeftSprite;
	public Texture[] LeftSprite;

	public GameObject[] Hearts;

	public int MagicLevel;
	public int Frame;

	public int RightPhase;
	public int LeftPhase;
	public int Health;

	public float Invincibility;
	public float ShootTimer;
	public float Magic;
	public float Speed;
	public float Timer;
	public float FPS;

	public float PositionX;
	public float PositionY;

	public bool Gameplay;

	void Start()
	{
		Time.timeScale = 1;

		if (!GameGlobals.EasyMode)
		{
			MagicBar.parent.gameObject.SetActive(false);
		}
	}

	void Update()
	{
		Timer += Time.deltaTime;

		if (Timer > FPS)
		{
			Timer = 0;

			Frame++;

			if (Frame == 3)
			{
				Frame = 0;

				if (RightPhase == 1)
				{
					RightPhase = 2;
				}
				else if (RightPhase == 3)
				{
					RightPhase = 0;
				}

				if (LeftPhase == 1)
				{
					LeftPhase = 2;
				}
				else if (LeftPhase == 3)
				{
					LeftPhase = 0;
				}
			}

			if (RightPhase == 0 && LeftPhase == 0)
			{
				MyRenderer.material.mainTexture = ForwardSprite[Frame];
			}
			else if (RightPhase == 1)
			{
				MyRenderer.material.mainTexture = TurnRightSprite[Frame];
			}
			else if (RightPhase == 2)
			{
				MyRenderer.material.mainTexture = RightSprite[Frame];
			}
			else if (RightPhase == 3)
			{
				MyRenderer.material.mainTexture = ReverseRightSprite[Frame];
			}
			else if (LeftPhase == 1)
			{
				MyRenderer.material.mainTexture = TurnLeftSprite[Frame];
			}
			else if (LeftPhase == 2)
			{
				MyRenderer.material.mainTexture = LeftSprite[Frame];
			}
			else if (LeftPhase == 3)
			{
				MyRenderer.material.mainTexture = ReverseLeftSprite[Frame];
			}
		}

		float MovementSpeed = 0;

		if (Input.GetButton(InputNames.Xbox_LB))
		{
			MovementSpeed = Speed * .5f;
		}
		else
		{
			MovementSpeed = Speed;
		}

		if (Gameplay)
		{
			if (Input.GetKey("right") || InputManager.DPadRight || Input.GetAxis("Horizontal") > .5f)
			{
				if (RightPhase < 1)
				{
					RightPhase = 1;
					LeftPhase = 0;
					Frame = 0;
				}

				PositionX += MovementSpeed * Time.deltaTime;
			}
			else
			{
				if (RightPhase == 1 || RightPhase == 2)
				{
					RightPhase = 3;
					Frame = 0;
				}
			}

			if (Input.GetKey("left") || InputManager.DPadLeft || Input.GetAxis("Horizontal") < -.5f)
			{
				if (LeftPhase < 1)
				{
					RightPhase = 0;
					LeftPhase = 1;
					Frame = 0;
				}

				PositionX -= MovementSpeed * Time.deltaTime;
			}
			else
			{
				if (LeftPhase == 1 || LeftPhase == 2)
				{
					LeftPhase = 3;
					Frame = 0;
				}
			}

			if (Input.GetKey("up") || InputManager.DPadUp || Input.GetAxis("Vertical") > .5f){PositionY += MovementSpeed * Time.deltaTime;}
			if (Input.GetKey("down") || InputManager.DPadDown || Input.GetAxis("Vertical") < -.5f){PositionY -= MovementSpeed * Time.deltaTime;}	

			if (PositionX > 108){PositionX = 108;}
			if (PositionX < -110){PositionX = -110;}
			if (PositionY > 224){PositionY = 224;}
			if (PositionY < -224){PositionY = -224;}

			transform.localPosition = new Vector3(PositionX, PositionY, 0);

			if (Input.GetKey("z") || Input.GetKey("y") || Input.GetButton(InputNames.Xbox_A))
			{
				if (ShootTimer == 0)
				{
					GameObject NewProjectile;

					if (MagicLevel == 0)
					{
						NewProjectile = Instantiate(Projectile, SpawnPoint.position, Quaternion.identity);
						NewProjectile.transform.parent = transform.parent;
						NewProjectile.transform.localPosition = new Vector3(NewProjectile.transform.localPosition.x, NewProjectile.transform.localPosition.y, 1);
						NewProjectile.transform.localScale = new Vector3(16, 16, 1);
					}
					else if (MagicLevel == 1)
					{
						NewProjectile = Instantiate(Projectile, SpawnPoint.position + new Vector3(.1f, 0, 0), Quaternion.identity);
						NewProjectile.transform.parent = transform.parent;
						NewProjectile.transform.localPosition = new Vector3(NewProjectile.transform.localPosition.x, NewProjectile.transform.localPosition.y, 1);
						NewProjectile.transform.localScale = new Vector3(16, 16, 1);

						NewProjectile = Instantiate(Projectile, SpawnPoint.position + new Vector3(-.1f, 0, 0), Quaternion.identity);
						NewProjectile.transform.parent = transform.parent;
						NewProjectile.transform.localPosition = new Vector3(NewProjectile.transform.localPosition.x, NewProjectile.transform.localPosition.y, 1);
						NewProjectile.transform.localScale = new Vector3(16, 16, 1);
					}
					else if (MagicLevel == 2)
					{
						NewProjectile = Instantiate(Projectile, SpawnPoint.position, Quaternion.identity);
						NewProjectile.transform.parent = transform.parent;
						NewProjectile.transform.localPosition = new Vector3(NewProjectile.transform.localPosition.x, NewProjectile.transform.localPosition.y, 1);
						NewProjectile.transform.localScale = new Vector3(16, 16, 1);

						NewProjectile = Instantiate(Projectile, SpawnPoint.position + new Vector3(.2f, 0, 0), Quaternion.identity);
						NewProjectile.transform.parent = transform.parent;
						NewProjectile.transform.localPosition = new Vector3(NewProjectile.transform.localPosition.x, NewProjectile.transform.localPosition.y, 1);
						NewProjectile.transform.localScale = new Vector3(16, 16, 1);

						NewProjectile = Instantiate(Projectile, SpawnPoint.position + new Vector3(-.2f, 0, 0), Quaternion.identity);
						NewProjectile.transform.parent = transform.parent;
						NewProjectile.transform.localPosition = new Vector3(NewProjectile.transform.localPosition.x, NewProjectile.transform.localPosition.y, 1);
						NewProjectile.transform.localScale = new Vector3(16, 16, 1);
					}
					else
					{
						NewProjectile = Instantiate(Projectile, SpawnPoint.position, Quaternion.identity);
						NewProjectile.transform.parent = transform.parent;
						NewProjectile.transform.localPosition = new Vector3(NewProjectile.transform.localPosition.x, NewProjectile.transform.localPosition.y, 1);
						NewProjectile.transform.localScale = new Vector3(16, 16, 1);

						NewProjectile = Instantiate(Projectile, SpawnPoint.position + new Vector3(.2f, 0, 0), Quaternion.identity);
						NewProjectile.transform.parent = transform.parent;
						NewProjectile.transform.localPosition = new Vector3(NewProjectile.transform.localPosition.x, NewProjectile.transform.localPosition.y, 1);
						NewProjectile.transform.localScale = new Vector3(16, 16, 1);

						NewProjectile = Instantiate(Projectile, SpawnPoint.position + new Vector3(-.2f, 0, 0), Quaternion.identity);
						NewProjectile.transform.parent = transform.parent;
						NewProjectile.transform.localPosition = new Vector3(NewProjectile.transform.localPosition.x, NewProjectile.transform.localPosition.y, 1);
						NewProjectile.transform.localScale = new Vector3(16, 16, 1);

						NewProjectile = Instantiate(Projectile, SpawnPoint.position + new Vector3(.4f, 0, 0), Quaternion.identity);
						NewProjectile.transform.parent = transform.parent;
						NewProjectile.transform.localPosition = new Vector3(NewProjectile.transform.localPosition.x, NewProjectile.transform.localPosition.y, 1);
						NewProjectile.transform.localScale = new Vector3(16, 16, 1);
						NewProjectile.GetComponent<MGPMProjectileScript>().Angle = 1;

						NewProjectile = Instantiate(Projectile, SpawnPoint.position + new Vector3(-.4f, 0, 0), Quaternion.identity);
						NewProjectile.transform.parent = transform.parent;
						NewProjectile.transform.localPosition = new Vector3(NewProjectile.transform.localPosition.x, NewProjectile.transform.localPosition.y, 1);
						NewProjectile.transform.localScale = new Vector3(16, 16, 1);
						NewProjectile.GetComponent<MGPMProjectileScript>().Angle = -1;
					}

					ShootTimer = 0;
				}

				ShootTimer += Time.deltaTime;

				if (ShootTimer >= .075f)
				{
					ShootTimer = 0;
				}
			}

			if (Input.GetKeyUp("z") || Input.GetKeyUp("y") || Input.GetButtonUp(InputNames.Xbox_A))
			{
				ShootTimer = 0;
			}

			if (Input.GetKeyDown("r"))
			{
				Application.LoadLevel(Application.loadedLevel);
			}
		}

		if (Invincibility > 0)
		{
			Invincibility = Mathf.MoveTowards(Invincibility, 0, Time.deltaTime);

			if (Invincibility == 0)
			{
				MyRenderer.material.SetColor("_EmissionColor", new Color(0, 0, 0, 0));
			}
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == 9)
		{
			if (Invincibility == 0)
			{
				//Destroy(collision.gameObject);
				Health--;

				if (GameGlobals.EasyMode)
				{
					MyRenderer.material.SetColor("_EmissionColor", new Color(1, 1, 1, 1));
					Invincibility = 1;
				}

				GameObject NewExplosion;

				if (Health > 0)
				{
					NewExplosion = Instantiate(Explosion, transform.position, Quaternion.identity);
					NewExplosion.transform.parent = transform.parent;
					NewExplosion.transform.localScale = new Vector3(64, 64, 1);

					AudioSource.PlayClipAtPoint(DamageSound, transform.position);
				}
				else
				{
					NewExplosion = Instantiate(DeathExplosion, transform.position, Quaternion.identity);
					NewExplosion.transform.parent = transform.parent;
					NewExplosion.transform.localScale = new Vector3(128, 128, 1);

					AudioSource.PlayClipAtPoint(DeathSound, transform.position);

					GameplayManager.BeginGameOver();

					gameObject.SetActive(false);
				}
			}

			UpdateHearts();
		}
		else if (collision.gameObject.layer == 15)
		{
			AudioSource.PlayClipAtPoint(PickUpSound, transform.position);
			GameplayManager.Score += 10;

			Magic++;

			if (Magic == 20)
			{
				MagicLevel++;

				if (MagicLevel > 3)
				{
					if (Health < 3)
					{
						Health++;
						UpdateHearts();
					}
				}

				Magic = 0;
			}

			MagicBar.localScale = new Vector3(
				Magic / 20.0f,
				1,
				1);

			Destroy(collision.gameObject);
		}
	}

	void UpdateHearts()
	{
		Hearts[1].SetActive(false);
		Hearts[2].SetActive(false);
		Hearts[3].SetActive(false);

		int ID = 1;

		while (ID < Health + 1)
		{
			Hearts[ID].SetActive(true);
			ID++;
		}
	}
}