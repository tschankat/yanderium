using UnityEngine;

public class GasterBeamScript : MonoBehaviour
{
	public float Strength = 1000.0f;
	public float Target = 2.0f;

	public bool LoveLoveBeam = false;

	void Start()
	{
		if (LoveLoveBeam)
		{
			transform.localScale = new Vector3(0, 0, 0);
		}
	}

	void Update()
	{
		if (LoveLoveBeam)
		{
			transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(100, Target, Target), Time.deltaTime * 10);

			if (transform.localScale.x > 99.99f)
			{
				Target = 0;

				if (transform.localScale.y < .1f)
				{
					Destroy(gameObject);
				}
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			StudentScript Student = other.gameObject.GetComponent<StudentScript>();

			if (Student != null)
			{
				Student.DeathType = DeathType.EasterEgg;
				Student.BecomeRagdoll();

				Rigidbody studentRigidBody = Student.Ragdoll.AllRigidbodies[0];
				studentRigidBody.isKinematic = false;

				studentRigidBody.AddForce((studentRigidBody.transform.root.position - 
					this.transform.root.position) * this.Strength);
				studentRigidBody.AddForce(Vector3.up * 1000.0f);
			}
		}
	}
}