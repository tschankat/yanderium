using UnityEngine;

public class HairBladeScript : MonoBehaviour
{
	public GameObject FemaleBloodyScream;
	public GameObject MaleBloodyScream;

	public Vector3 PreviousPosition;

	public Collider MyCollider;

	public float Timer = 0.0f;

	void Update()
	{
		// [af] Commented in JS code.
		/*
		this.MyCollider.enabled = false;
	
		if (Vector3.Distance(this.transform.position, this.PreviousPosition) > .50f)
		{
			this.MyCollider.enabled = true;
		}
		
		this.PreviousPosition = this.transform.position;
		
		/*
		if (this.MyCollider.enabled)
		{
			this.Timer += Time.deltaTime;
			
			if (this.Timer > 1)
			{
				this.MyCollider.enabled = false;
				this.Timer = 0;
			}
		}
		else
		{
			if (Vector3.Distance(this.transform.position, this.PreviousPosition) > .50f)
			{
				this.MyCollider.enabled = true;
			}
		}
		
		this.PreviousPosition = this.transform.position;
		*/
	}

	public StudentScript Student;

	void OnTriggerEnter(Collider other)
	{
		GameObject rootGameObject = other.gameObject.transform.root.gameObject;

		if (rootGameObject.GetComponent<StudentScript>() != null)
		{
			this.Student = rootGameObject.GetComponent<StudentScript>();

			if (this.Student.StudentID != 1)
			{
				if (this.Student.Alive)
				{
					this.Student.DeathType = DeathType.EasterEgg;

					// [af] Replaced if/else statement with ternary expression.
					Instantiate(this.Student.Male ? this.MaleBloodyScream : this.FemaleBloodyScream,
						this.Student.transform.position + Vector3.up, Quaternion.identity);

					this.Student.BecomeRagdoll();
					this.Student.Ragdoll.Dismember();

					this.GetComponent<AudioSource>().Play();
				}
			}
		}
	}
}
