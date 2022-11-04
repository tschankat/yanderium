using UnityEngine;

public class YanvaniaWitchScript : MonoBehaviour
{
	public YanvaniaYanmontScript Yanmont;

	public GameObject GroundImpact;
	public GameObject BlackHole;
	public GameObject Character;
	public GameObject HitEffect;
	public GameObject Wall;

	public AudioClip DeathScream;
	public AudioClip HitSound;

	public float HitReactTimer = 0.0f;
	public float AttackTimer = 10.0f;
	public float HP = 100.0f;

	public bool CastSpell = false;
	public bool Casting = false;

	void Update()
	{
		Animation charAnimation = this.Character.GetComponent<Animation>();

		if (this.AttackTimer < 10.0f)
		{
			this.AttackTimer += Time.deltaTime;

			if (this.AttackTimer > 0.80f)
			{
				if (!this.CastSpell)
				{
					this.CastSpell = true;

					Instantiate(this.BlackHole,
						this.transform.position + (Vector3.up * 3.0f) + (Vector3.right * 6.0f),
						Quaternion.identity);
					Instantiate(this.GroundImpact,
						this.transform.position + (Vector3.right * 1.15f),
						Quaternion.identity);
				}
			}

			if (charAnimation["Staff Spell Ground"].time >=
				charAnimation["Staff Spell Ground"].length)
			{
				charAnimation.CrossFade("Staff Stance");
				this.Casting = false;
			}
		}
		else
		{
			if (Vector3.Distance(this.transform.position, this.Yanmont.transform.position) < 5.0f)
			{
				this.AttackTimer = 0.0f;

				this.Casting = true;
				this.CastSpell = false;

				charAnimation["Staff Spell Ground"].time = 0.0f;
				charAnimation.CrossFade("Staff Spell Ground");
			}
		}

		if (!this.Casting)
		{
			if (charAnimation["Receive Damage"].time >=
				charAnimation["Receive Damage"].length)
			{
				charAnimation.CrossFade("Staff Stance");
			}
		}

		this.HitReactTimer += Time.deltaTime * 10.0f;
	}

	void OnTriggerEnter(Collider other)
	{
		if (this.HP > 0.0f)
		{
			if (other.gameObject.tag == "Player")
			{
				this.Yanmont.TakeDamage(5);
			}

			if (other.gameObject.name == "Heart")
			{
				Animation charAnimation = this.Character.GetComponent<Animation>();

				if (!this.Casting)
				{
					charAnimation["Receive Damage"].time = 0.0f;
					charAnimation.Play("Receive Damage");
				}

				if (this.HitReactTimer >= 1.0f)
				{
					Instantiate(this.HitEffect,
						other.transform.position, Quaternion.identity);
					this.HitReactTimer = 0.0f;
					this.HP -= 5.0f + ((this.Yanmont.Level * 5.0f) - 5.0f);

					AudioSource audioSource = this.GetComponent<AudioSource>();

					if (this.HP <= 0.0f)
					{
						audioSource.PlayOneShot(this.DeathScream);

						charAnimation.Play("Die 2");
						this.Yanmont.EXP += 100.0f;
						this.enabled = false;
						Destroy(this.Wall);
					}
					else
					{
						audioSource.PlayOneShot(this.HitSound);
					}
				}
			}
		}
	}
}
