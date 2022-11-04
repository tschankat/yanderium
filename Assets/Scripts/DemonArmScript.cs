using UnityEngine;

public class DemonArmScript : MonoBehaviour
{
	public GameObject DismembermentCollider;
	public Animation MyAnimation;

	public Collider ClawCollider;

	public bool Attacking = false;
	public bool Attacked = false;
	public bool Rising = true;

	public string IdleAnim = AnimNames.DemonArmIdle;
	public string AttackAnim = AnimNames.DemonArmAttack;

	public AudioClip Whoosh;

	public float AnimSpeed = 1;

	public float AnimTime;

	void Start()
	{
		MyAnimation = this.GetComponent<Animation>();

		if (!Rising)
		{
			MyAnimation[this.IdleAnim].speed = AnimSpeed * .5f;
		}

		MyAnimation[AttackAnim].speed = 0;
	}

	void Update()
	{
		if (!this.Rising)
		{
			if (!this.Attacking)
			{
				MyAnimation.CrossFade(this.IdleAnim);
			}
			else
			{
				AnimTime += (1.0f / 60.0f);

				MyAnimation[this.AttackAnim].time = AnimTime;

				//Debug.Log(MyAnimation[this.AttackAnim].time);

				if (!this.Attacked)
				{
					if (MyAnimation[AttackAnim].time >= MyAnimation[AttackAnim].length * 0.15f)
					{
						this.ClawCollider.enabled = true;
						this.Attacked = true;
					}
				}
				else
				{
					if (MyAnimation[AttackAnim].time >= MyAnimation[AttackAnim].length * 0.4f)
					{
						this.ClawCollider.enabled = false;
					}

					if (MyAnimation[AttackAnim].time >= MyAnimation[AttackAnim].length)
					{
						MyAnimation.CrossFade(this.IdleAnim);
						this.ClawCollider.enabled = false;
						this.Attacking = false;
						this.Attacked = false;
						this.AnimTime = 0;
					}
				}
			}
		}
		else
		{
			if (MyAnimation[AttackAnim].time > MyAnimation[AttackAnim].length)
			{
				this.Rising = false;
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		StudentScript student = other.gameObject.GetComponent<StudentScript>();

		if (student != null)
		{
			if (student.StudentID > 1)
			{
				AudioSource audioSource = this.GetComponent<AudioSource>();
				audioSource.clip = this.Whoosh;
				audioSource.pitch = Random.Range(-0.90f, 1.10f);
				audioSource.Play();

				this.GetComponent<Animation>().CrossFade(AttackAnim);
				this.Attacking = true;
			}
		}
	}
}