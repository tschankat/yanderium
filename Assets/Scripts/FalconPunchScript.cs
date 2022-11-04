using UnityEngine;

public class FalconPunchScript : MonoBehaviour
{
	public GameObject FalconExplosion;
	public Rigidbody MyRigidbody;
	public Collider MyCollider;

	public float Strength = 100.0f;
	public float Speed = 100f;

	public bool Destructive = false;
	public bool IgnoreTime = false;
	public bool Shipgirl = false;
	public bool Bancho = false;
	public bool Falcon = false;
	public bool Mecha = false;

	public float TimeLimit = 0.50f;
	public float Timer = 0.0f;

	void Start()
	{
		if (Mecha)
		{
			MyRigidbody.AddForce(transform.forward * Speed * 10);
		}
	}

	void Update()
	{
		if (!this.IgnoreTime)
		{
			this.Timer += Time.deltaTime;

			if (this.Timer > this.TimeLimit)
			{
				this.MyCollider.enabled = false;
			}
		}

		if (Shipgirl)
		{
			MyRigidbody.AddForce(transform.forward * Speed);
			//MyRigidbody.AddForce(transform.up * Speed * .15f);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log("A punch collided with something.");

		if (other.gameObject.layer == 9)
		{
			Debug.Log("A punch collided with something on the Characters layer.");

			StudentScript Student = other.gameObject.GetComponent<StudentScript>();

			if (Student != null)
			{
				Debug.Log("A punch collided with a student.");

				if (Student.StudentID > 1)
				{
					Debug.Log("A punch collided with a student and killed them.");

					Instantiate(this.FalconExplosion,
						Student.transform.position + new Vector3(0.0f, 1.0f, 0.0f),
						Quaternion.identity);
					//Instantiate(this.FalconExplosion, this.transform.position, Quaternion.identity);

					Student.DeathType = DeathType.EasterEgg;
					Student.BecomeRagdoll();

					Rigidbody studentRigidBody = Student.Ragdoll.AllRigidbodies[0];
					studentRigidBody.isKinematic = false;

					Vector3 Direction = studentRigidBody.transform.position - Student.Yandere.transform.position;

					//Debug.Log("Direction is: " + Direction);

					if (this.Falcon)
					{
						studentRigidBody.AddForce(Direction * this.Strength);
					}
					else if (this.Bancho)
					{
						studentRigidBody.AddForce(Direction.x * this.Strength, 5000, Direction.z * this.Strength);
					}
					else
					{
						studentRigidBody.AddForce(Direction.x * this.Strength, 10000, Direction.z * this.Strength);
					}
				}
			}
		}

		if (Destructive)
		{
			if (other.gameObject.layer != 2 &&
			    other.gameObject.layer != 8 &&
				other.gameObject.layer != 9 &&
				other.gameObject.layer != 13 &&
			 	other.gameObject.layer != 17)
			{
				GameObject Target = null;

				StudentScript Student = other.gameObject.transform.root.GetComponent<StudentScript>();

				if (Student != null)
				{
					if (Student.StudentID > 1)
					{
						//
					}
					else
					{
						Target = Student.gameObject;
					}
				}
				else
				{
					Target = other.gameObject;
				}

				if (Target != null)
				{
					Instantiate(this.FalconExplosion, transform.position + new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
					Destroy(Target);
					Destroy(gameObject);
				}
			}
		}
	}
}