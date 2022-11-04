using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SithBeamScript : MonoBehaviour
{
	public GameObject BloodEffect;

	public Collider MyCollider;

	public float Damage = 10.0f;

	public float Lifespan = 0.0f;

	public int RandomNumber;

	public AudioClip Hit;

	public AudioClip[] FemalePain;

	public AudioClip[] MalePain;

	public bool Projectile;

	void Update()
	{
		if (Projectile)
		{
			transform.Translate(transform.forward * Time.deltaTime * 15, Space.World);
		}

		Lifespan = Mathf.MoveTowards(Lifespan, 0, Time.deltaTime);

		if (Lifespan == 0)
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			StudentScript Student = other.gameObject.GetComponent<StudentScript>();

			if (Student != null)
			{
				if (Student.StudentID > 1)
				{
					AudioSource.PlayClipAtPoint(Hit, transform.position); 

					RandomNumber = Random.Range(0, 3);

					if (this.MalePain.Length > 0)
					{
						if (Student.Male)
						{
							AudioSource.PlayClipAtPoint(MalePain[RandomNumber], transform.position); 
						}
						else
						{
							AudioSource.PlayClipAtPoint(FemalePain[RandomNumber], transform.position); 
						}
					}

					Instantiate(this.BloodEffect,
						Student.transform.position + new Vector3(0.0f, 1.0f, 0.0f),
						Quaternion.identity);

					Student.Health -= this.Damage;
					Student.HealthBar.transform.parent.gameObject.SetActive(true);

					Student.HealthBar.transform.localScale = new Vector3(Student.Health / 100.0f, 1, 1);

					Student.Character.transform.localScale = new Vector3(
						Student.Character.transform.localScale.x * -1,
						Student.Character.transform.localScale.y,
						Student.Character.transform.localScale.z);

					if (Student.Health <= 0)
					{
						Student.DeathType = DeathType.EasterEgg;
						Student.HealthBar.transform.parent.gameObject.SetActive(false);
						Student.BecomeRagdoll();

						Rigidbody studentRigidBody = Student.Ragdoll.AllRigidbodies[0];
						studentRigidBody.isKinematic = false;
					}
					else
					{
						Student.CharacterAnimation[Student.SithReactAnim].time = 0;
						Student.CharacterAnimation.Play(Student.SithReactAnim);
						Student.Pathfinding.canSearch = false;
						Student.Pathfinding.canMove = false;
						Student.HitReacting = true;
						Student.Routine = false;
						Student.Fleeing = false;
					}

					if (Projectile)
					{
						Destroy(gameObject);
					}
				}
			}
		}
	}
}