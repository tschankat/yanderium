using UnityEngine;

public class TornadoScript : MonoBehaviour
{
	public GameObject FemaleBloodyScream;
	public GameObject MaleBloodyScream;
	public GameObject Scream;

	public Collider MyCollider;

	public float Strength = 10000.0f;
	public float Timer = 0.0f;

	void Update()
	{
		this.Timer += Time.deltaTime;

		if (this.Timer > 0.50f)
		{
			this.transform.position = new Vector3(
				this.transform.position.x,
				this.transform.position.y + Time.deltaTime,
				this.transform.position.z);

			this.MyCollider.enabled = true;
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
					// [af] Replaced if/else statement with ternary expression.
					this.Scream = Instantiate(
						Student.Male ? this.MaleBloodyScream : this.FemaleBloodyScream,
						Student.transform.position + Vector3.up,
						Quaternion.identity);

					this.Scream.transform.parent = Student.HipCollider.transform;
					this.Scream.transform.localPosition = Vector3.zero;

					Student.DeathType = DeathType.EasterEgg;
					Student.BecomeRagdoll();

					Rigidbody studentRigidBody = Student.Ragdoll.AllRigidbodies[0];
					studentRigidBody.isKinematic = false;
					studentRigidBody.AddForce(Vector3.up * this.Strength);
				}
			}
		}
	}
}
