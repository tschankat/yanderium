using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YanvaniaDraculaScript : MonoBehaviour
{
	public YanvaniaCameraScript YanvaniaCamera;
	public YanvaniaYanmontScript Yanmont;

	public UITexture HealthBarParent;
	public UITexture Photograph;

	public AudioClip DeathScream;
	public AudioClip FinalLine;

	public GameObject NewTeleportEffect;
	public GameObject NewAttack;

	public GameObject DoubleFireball;
	public GameObject TripleFireball;

	public GameObject MainCamera;
	public GameObject EndCamera;

	public GameObject TeleportEffect;
	public GameObject Explosion;
	public GameObject Character;

	public Transform HealthBar;
	public Transform RightHand;

	public Renderer MyRenderer;

	public AudioClip[] Injuries;

	public float ExplosionTimer = 0.0f;
	public float TeleportTimer = 10.0f;
	public float FinalTimer = 0.0f;
	public float DeathTimer = 0.0f;
	public float FlashTimer = 0.0f;
	public float Distance = 0.0f;

	public float MaxHealth = 100.0f;
	public float Health = 100.0f;

	public bool FinalLineSpoken = false;
	public bool PhotoTaken = false;
	public bool Screamed = false;
	public bool Injured = false;
	public bool Shrink = false;
	public bool Grow = false;
	public bool Red = false;

	public int AttackID = 0;
	public int Frames = 0;
	public int Frame = 0;

	void Awake()
	{
		// [af] Moved here from class scope for compatibility with C#.
		Animation charAnimation = this.Character.GetComponent<Animation>();
		charAnimation["succubus_a_damage_l"].speed = 0.10f;
		charAnimation["succubus_a_charm_02"].speed = 2.40f;
		charAnimation["succubus_a_charm_03"].speed = 4.66666f;
	}

	void Update()
	{
		Animation charAnimation = this.Character.GetComponent<Animation>();

		if (this.Yanmont.Health > 0.0f)
		{
			if (this.Health > 0.0f)
			{
				if (this.transform.position.z == 0.0f)
				{
					// [af] Replaced if/else statement with ternary expression.
					this.transform.localEulerAngles = new Vector3(
						this.transform.localEulerAngles.x,
						(this.Yanmont.transform.position.x > this.transform.position.x) ? -90.0f : 90.0f,
						this.transform.localEulerAngles.z);
				}

				if (this.NewTeleportEffect == null)
				{
					if (this.transform.position.y > 0.0f)
					{
						this.Distance = Mathf.Abs(this.Yanmont.transform.position.x) -
							Mathf.Abs(this.transform.position.x);

						if (Mathf.Abs(this.Distance) < 0.61f)
						{
							if (this.Yanmont.FlashTimer == 0.0f)
							{
								this.Yanmont.TakeDamage(5);
							}
						}

						if (this.AttackID == 0)
						{
							this.AttackID = Random.Range(1, 3);
							this.TeleportTimer = 5.0f;

							if (this.AttackID == 1)
							{
								charAnimation["succubus_a_charm_02"].time = 0.0f;
								charAnimation.CrossFade("succubus_a_charm_02");
							}
							else
							{
								charAnimation["succubus_a_charm_03"].time = 0.0f;
								charAnimation.CrossFade("succubus_a_charm_03");
							}
						}
						else
						{
							if (this.AttackID == 1)
							{
								if (charAnimation["succubus_a_charm_02"].time > 4.0f)
								{
									if (this.NewAttack == null)
									{
										this.NewAttack = Instantiate(this.TripleFireball,
											this.RightHand.position, Quaternion.identity);
										this.NewAttack.GetComponent<YanvaniaTripleFireballScript>().Dracula = this.transform;
									}
								}

								if (charAnimation["succubus_a_charm_02"].time >=
									charAnimation["succubus_a_charm_02"].length)
								{
									charAnimation.CrossFade("succubus_a_idle_01");
								}
							}
							else
							{
								if (charAnimation["succubus_a_charm_03"].time > 4.0f)
								{
									if (this.NewAttack == null)
									{
										this.NewAttack = Instantiate(this.DoubleFireball,
											this.RightHand.position, Quaternion.identity);
										this.NewAttack.GetComponent<YanvaniaDoubleFireballScript>().Dracula = this.transform;
									}
								}

								if (charAnimation["succubus_a_charm_03"].time >=
									charAnimation["succubus_a_charm_03"].length)
								{
									charAnimation.CrossFade("succubus_a_idle_01");
								}
							}

							this.TeleportTimer -= Time.deltaTime;
						}
					}
					else
					{
						this.TeleportTimer -= Time.deltaTime;
					}

					if (this.TeleportTimer < 0.0f)
					{
						if (this.transform.position.y < 0.0f)
						{
							this.transform.position = new Vector3(
								Random.Range(-38.50f, -31.0f),
								this.transform.position.y,
								this.transform.position.z);
						}

						this.SpawnTeleportEffect();
					}
				}
				else
				{
					if (this.Shrink)
					{
						this.transform.localScale = Vector3.MoveTowards(
							this.transform.localScale,
							new Vector3(0.0f, 2.0f, 0.0f),
							Time.deltaTime * 0.50f);

						if (this.transform.localScale.x == 0.0f)
						{
							this.NewTeleportEffect = null;
							this.Shrink = false;
							this.Teleport();
						}
					}

					if (this.Grow)
					{
						this.transform.localScale = Vector3.Lerp(
							this.transform.localScale,
							new Vector3(1.50f, 1.50f, 1.50f),
							Time.deltaTime * 1.50f);

						if (this.transform.localScale.x > 1.49f)
						{
							this.NewTeleportEffect = null;
							this.transform.localScale = new Vector3(1.50f, 1.50f, 1.50f);
							this.Grow = false;
						}
					}
				}

				if (this.FlashTimer > 0.0f)
				{
					this.FlashTimer -= Time.deltaTime;

					// [af] Removed unused "ID" variable.

					if (!this.Red)
					{
						// [af] Converted while loop to foreach loop.
						foreach (Material material in this.MyRenderer.materials)
						{
							material.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
						}

						this.Frames++;

						if (this.Frames == 5)
						{
							this.Frames = 0;
							this.Red = true;
						}
					}
					else
					{
						// [af] Converted while loop to foreach loop.
						foreach (Material material in this.MyRenderer.materials)
						{
							material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
						}

						this.Frames++;

						if (this.Frames == 5)
						{
							this.Frames = 0;
							this.Red = false;
						}
					}
				}
				else
				{
					if (this.MyRenderer.materials[0].color != new Color(1.0f, 1.0f, 1.0f, 1.0f))
					{
						// [af] Converted while loop to foreach loop.
						foreach (Material material in this.MyRenderer.materials)
						{
							material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
						}
					}
				}
			}
			else
			{
				this.HealthBarParent.transform.localPosition = new Vector3(
					this.HealthBarParent.transform.localPosition.x,
					this.HealthBarParent.transform.localPosition.y - (Time.deltaTime * 0.20f),
					this.HealthBarParent.transform.localPosition.z);

				this.HealthBarParent.transform.localScale = new Vector3(
					this.HealthBarParent.transform.localScale.x,
					this.HealthBarParent.transform.localScale.y + (Time.deltaTime * 0.20f),
					this.HealthBarParent.transform.localScale.z);

				this.HealthBarParent.color = new Color(
					this.HealthBarParent.color.r,
					this.HealthBarParent.color.g,
					this.HealthBarParent.color.b,
					this.HealthBarParent.color.a - (Time.deltaTime * 0.20f));

				charAnimation.Play("succubus_a_damage_l");

				this.ExplosionTimer += Time.deltaTime;
				this.DeathTimer += Time.deltaTime;

				AudioSource audioSource = this.GetComponent<AudioSource>();

				if (this.DeathTimer < 5.0f)
				{
					if (this.DeathTimer > 1.0f)
					{
						if (!this.FinalLineSpoken)
						{
							this.FinalLineSpoken = true;

							audioSource.clip = this.FinalLine;
							audioSource.Play();
						}
					}

					if (this.ExplosionTimer > 0.10f)
					{
						Instantiate(this.Explosion,
							this.transform.position + new Vector3(Random.Range(-1.0f, 1.0f),
							Random.Range(0, 2.50f),
							Random.Range(-1.0f, 1.0f)),
							Quaternion.identity);

						this.ExplosionTimer = 0.0f;
					}
				}
				else
				{
					if (!this.Screamed)
					{
						this.Screamed = true;
						audioSource.clip = this.DeathScream;
						audioSource.Play();
					}

					if (this.DeathTimer > 5.0f)
					{
						if (!this.PhotoTaken)
						{
							Time.timeScale = Mathf.MoveTowards(Time.timeScale, 0.0f, 1.0f / 60.0f);

							if (Time.timeScale == 0.0f)
							{
								ScreenCapture.CaptureScreenshot(
									Application.streamingAssetsPath + "/Dracula" + ".png");

								if (this.Frame > 0)
								{
									this.StartCoroutine(this.ApplyScreenshot());
								}

								this.Frame++;
							}
						}
						else
						{
							if (this.Photograph.mainTexture != null)
							{
								this.Photograph.transform.localEulerAngles = new Vector3(
									this.Photograph.transform.localEulerAngles.x + (Time.deltaTime * 3.60f),
									this.Photograph.transform.localEulerAngles.y,
									this.Photograph.transform.localEulerAngles.z - (Time.deltaTime * 3.60f));

								this.Photograph.transform.localScale = Vector3.MoveTowards(
									this.Photograph.transform.localScale,
									Vector3.zero,
									Time.deltaTime * 0.20f);

								this.Photograph.color = new Color(
									this.Photograph.color.r,
									this.Photograph.color.g - (Time.deltaTime * 0.20f),
									this.Photograph.color.b - (Time.deltaTime * 0.20f),
									this.Photograph.color.a);

								if (this.Photograph.transform.localScale == Vector3.zero)
								{
									this.FinalTimer += Time.deltaTime;

									if (this.FinalTimer > 1.0f)
									{
										YanvaniaGlobals.DraculaDefeated = true;
										SceneManager.LoadScene(SceneNames.YanvaniaTitleScene);
									}
								}
							}
						}
					}
				}
			}
		}
		else
		{
			charAnimation.CrossFade("succubus_a_talk_01");
		}

		this.HealthBar.parent.transform.localPosition = new Vector3(
			Mathf.Lerp(this.HealthBar.parent.transform.localPosition.x, 1025.0f, Time.deltaTime * 10.0f),
			this.HealthBar.parent.transform.localPosition.y,
			this.HealthBar.parent.transform.localPosition.z);

		this.HealthBar.localScale = new Vector3(
			this.HealthBar.localScale.x,
			Mathf.Lerp(this.HealthBar.localScale.y, this.Health / this.MaxHealth, Time.deltaTime * 10.0f),
			this.HealthBar.localScale.z);

		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			this.transform.position = new Vector3(this.transform.position.x, 6.50f, 0.0f);

			this.Health = 1.0f;
			this.TakeDamage();
		}
	}

	public void SpawnTeleportEffect()
	{
		Animation charAnimation = this.Character.GetComponent<Animation>();

		if (this.transform.position.y > 0.0f)
		{
			this.NewTeleportEffect = Instantiate(this.TeleportEffect,
				new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z),
				Quaternion.identity);
			charAnimation["DraculaTeleport"].time = charAnimation["DraculaTeleport"].length;
			charAnimation["DraculaTeleport"].speed = -1.0f;
			charAnimation.Play("DraculaTeleport");

			this.Shrink = true;
		}
		else
		{
			this.Teleport();

			this.NewTeleportEffect = Instantiate(this.TeleportEffect,
				new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z),
				Quaternion.identity);
			charAnimation["DraculaTeleport"].speed = 0.85f;
			charAnimation["DraculaTeleport"].time = 0.0f;
			charAnimation.Play("DraculaTeleport");

			this.Grow = true;
		}

		this.NewTeleportEffect.GetComponent<YanvaniaTeleportEffectScript>().Dracula = this;

		this.TeleportTimer = 1.0f;
		this.AttackID = 0;
	}

	void Teleport()
	{
		// [af] Replaced if/else statement with assignment and ternary expression.
		this.transform.position = new Vector3(
			this.transform.position.x,
			(this.transform.position.y > 0) ? -10.0f : 6.50f,
			0.0f);
	}

	public void TakeDamage()
	{
		this.Health -= 5.0f + ((this.Yanmont.Level * 5.0f) - 5.0f);

		if (this.Health <= 0.0f)
		{
			this.YanvaniaCamera.StopMusic = true;
			this.Health = 0.0f;
		}
		else
		{
			this.FlashTimer = 1.0f;
			this.Injured = true;

			AudioSource audioSource = this.GetComponent<AudioSource>();
			audioSource.clip = this.Injuries[Random.Range(0, this.Injuries.Length)];
			audioSource.Play();
		}
	}

	IEnumerator ApplyScreenshot()
	{
		this.PhotoTaken = true;

		string path = "file:///" + Application.streamingAssetsPath + "/Dracula" + ".png";

		WWW www = new WWW(path);

		yield return www;

		this.Photograph.mainTexture = www.texture;

		this.MainCamera.SetActive(false);
		this.EndCamera.GetComponent<AudioListener>().enabled = true;

		Time.timeScale = 1.0f;
	}
}
