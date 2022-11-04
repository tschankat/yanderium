using UnityEngine;

public class BoneScript : MonoBehaviour
{
    public AudioSource MyAudio;

	public float Height = 0.0f;
	public float Origin = 0.0f;

	public bool Drop = false;

	void Start()
	{
		this.transform.eulerAngles = new Vector3(
			this.transform.eulerAngles.x,
			Random.Range(0.0f, 360.0f),
			this.transform.eulerAngles.z);

		this.Origin = this.transform.position.y;
		this.MyAudio.pitch = Random.Range(0.90f, 1.10f);
	}

	void Update()
	{
		if (!this.Drop)
		{
			if (this.transform.position.y < (this.Origin + 2.0f - 0.00010f))
			{
				this.transform.position = new Vector3(
					this.transform.position.x,
					Mathf.Lerp(this.transform.position.y, this.Origin + 2.0f, Time.deltaTime * 10.0f),
					this.transform.position.z);
			}
			else
			{
				this.Drop = true;
			}
		}
		else
		{
			this.Height -= Time.deltaTime;
			
			this.transform.position = new Vector3(
				this.transform.position.x,
				this.transform.position.y + this.Height,
				this.transform.position.z);

			if (this.transform.position.y < (this.Origin - 2.155f))
			{
				Destroy(this.gameObject);
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

				Rigidbody rigidBody = Student.Ragdoll.AllRigidbodies[0];
				rigidBody.isKinematic = false;
				rigidBody.AddForce(this.transform.up);
			}
		}
	}
}
