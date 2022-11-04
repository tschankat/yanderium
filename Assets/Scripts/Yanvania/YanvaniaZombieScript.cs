using UnityEngine;

public class YanvaniaZombieScript : MonoBehaviour
{
	public GameObject ZombieEffect;
	public GameObject BloodEffect;
	public GameObject DeathEffect;
	public GameObject HitEffect;
	public GameObject Character;

	public YanvaniaYanmontScript Yanmont;

	public int HP = 0;

	public float WalkSpeed1 = 0.0f;
	public float WalkSpeed2 = 0.0f;
	public float Damage = 0.0f;

	public float HitReactTimer = 0.0f;
	public float DeathTimer = 0.0f;
	public float WalkTimer = 0.0f;
	public float Timer = 0.0f;

	public int HitReactState = 0;
	public int WalkType = 0;

	public float LeftBoundary = 0.0f;
	public float RightBoundary = 0.0f;

	public bool EffectSpawned = false;
	public bool Dying = false;
	public bool Sink = false;
	public bool Walk = false;

	public Texture[] Textures;

	public Renderer MyRenderer;
	public Collider MyCollider;

	public AudioClip DeathSound;
	public AudioClip HitSound;
	public AudioClip RisingSound;
	public AudioClip SinkingSound;

	void Start()
	{
		// [af] Replaced if/else statement with assignment and ternary expression.
		this.transform.eulerAngles = new Vector3(
			this.transform.eulerAngles.x,
			(this.Yanmont.transform.position.x > this.transform.position.x) ? 90.0f : -90.0f,
			this.transform.eulerAngles.z);

		Instantiate(this.ZombieEffect, this.transform.position, Quaternion.identity);

		this.transform.position = new Vector3(
			this.transform.position.x,
			-0.630f,
			this.transform.position.z);

		Animation charAnimation = this.Character.GetComponent<Animation>();
		charAnimation["getup1"].speed = 2.0f;
		charAnimation.Play("getup1");

		this.GetComponent<AudioSource>().PlayOneShot(this.RisingSound);

		this.MyRenderer.material.mainTexture = this.Textures[Random.Range(0, 22)];
		this.MyCollider.enabled = false;
	}

	void Update()
	{
		AudioSource audioSource = this.GetComponent<AudioSource>();

		if (this.Dying)
		{
			this.DeathTimer += Time.deltaTime;

			if (this.DeathTimer > 1.0f)
			{
				if (!this.EffectSpawned)
				{
					Instantiate(this.ZombieEffect, this.transform.position, Quaternion.identity);
					audioSource.PlayOneShot(this.SinkingSound);
					this.EffectSpawned = true;
				}

				this.transform.position = new Vector3(
					this.transform.position.x,
					this.transform.position.y - Time.deltaTime,
					this.transform.position.z);

				if (this.transform.position.y < -0.40f)
				{
					Destroy(this.gameObject);
				}
			}
		}
		else
		{
			Animation charAnimation = this.Character.GetComponent<Animation>();

			if (this.Sink)
			{
				this.transform.position = new Vector3(
					this.transform.position.x,
					this.transform.position.y - (Time.deltaTime * 0.740f),
					this.transform.position.z);

				if (this.transform.position.y < -0.630f)
				{
					Destroy(this.gameObject);
				}
			}
			else
			{
				if (this.Walk)
				{
					this.WalkTimer += Time.deltaTime;

					if (this.WalkType == 1)
					{
						this.transform.Translate(
							Vector3.forward * Time.deltaTime * this.WalkSpeed1);
						charAnimation.CrossFade("walk1");
					}
					else
					{
						this.transform.Translate(
							Vector3.forward * Time.deltaTime * this.WalkSpeed2);
						charAnimation.CrossFade("walk2");
					}

					if (this.WalkTimer > 10.0f)
					{
						this.SinkNow();
					}
				}
				else
				{
					this.Timer += Time.deltaTime;

					if (this.transform.position.y < 0.0f)
					{
						this.transform.position = new Vector3(
							this.transform.position.x,
							this.transform.position.y + (Time.deltaTime * 0.740f),
							this.transform.position.z);

						if (this.transform.position.y > 0.0f)
						{
							this.transform.position = new Vector3(
								this.transform.position.x,
								0.0f,
								this.transform.position.z);
						}
					}

					if (this.Timer > 0.850f)
					{
						this.Walk = true;
						this.MyCollider.enabled = true;
						this.WalkType = Random.Range(1, 3);
					}
				}
			}

			if (this.transform.position.x < this.LeftBoundary)
			{
				this.transform.position = new Vector3(
					this.LeftBoundary,
					this.transform.position.y,
					this.transform.position.z);

				this.SinkNow();
			}

			if (this.transform.position.x > this.RightBoundary)
			{
				this.transform.position = new Vector3(
					this.RightBoundary,
					this.transform.position.y,
					this.transform.position.z);

				this.SinkNow();
			}

			if (this.HP <= 0)
			{
				Instantiate(this.DeathEffect,
					new Vector3(this.transform.position.x, this.transform.position.y + 1.0f, this.transform.position.z),
					Quaternion.identity);
				charAnimation.Play("die");
				audioSource.PlayOneShot(this.DeathSound);
				this.MyCollider.enabled = false;
				this.Yanmont.EXP += 10.0f;
				this.Dying = true;
			}
		}

		if (this.HitReactTimer < 1.0f)
		{
			this.MyRenderer.material.color =
				new Color(1.0f, this.HitReactTimer, this.HitReactTimer, 1.0f);
			this.HitReactTimer += Time.deltaTime * 10.0f;

			if (this.HitReactTimer >= 1.0f)
			{
				this.MyRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			}
		}
	}

	void SinkNow()
	{
		Animation charAnimation = this.Character.GetComponent<Animation>();
		charAnimation["getup1"].time = charAnimation["getup1"].length;
		charAnimation["getup1"].speed = -2.0f;
		charAnimation.Play("getup1");

		AudioSource audioSource = this.GetComponent<AudioSource>();
		audioSource.PlayOneShot(this.SinkingSound);

		Instantiate(this.ZombieEffect, this.transform.position, Quaternion.identity);
		this.MyCollider.enabled = false;
		this.Sink = true;
	}

	void OnTriggerEnter(Collider other)
	{
		if (!this.Dying)
		{
			if (other.gameObject.tag == "Player")
			{
				this.Yanmont.TakeDamage(5);
			}

			if (other.gameObject.name == "Heart")
			{
				if (this.HitReactTimer >= 1.0f)
				{
					Instantiate(this.HitEffect, other.transform.position, Quaternion.identity);

					AudioSource audioSource = this.GetComponent<AudioSource>();
					audioSource.PlayOneShot(this.HitSound);

					this.HitReactTimer = 0.0f;
					this.HP -= 20 + ((this.Yanmont.Level * 5) - 5);
				}
			}
		}
	}
}
