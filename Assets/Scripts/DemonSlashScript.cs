using UnityEngine;

public class DemonSlashScript : MonoBehaviour
{
	public GameObject FemaleBloodyScream;
	public GameObject MaleBloodyScream;

	public AudioSource MyAudio;

	public Collider MyCollider;

	public float Timer = 0.0f;

	void Start()
	{
		this.MyAudio = this.GetComponent<AudioSource>();
	}

	void Update()
	{
		if (this.MyCollider.enabled)
		{
			this.Timer += Time.deltaTime;

			if (this.Timer > (1.0f / 3.0f))
			{
				this.MyCollider.enabled = false;
				this.Timer = 0.0f;
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		Transform rootTransform = other.gameObject.transform.root;
		StudentScript student = rootTransform.gameObject.GetComponent<StudentScript>();

		if (student != null)
		{
			if (student.StudentID != 1)
			{
				if (student.Alive)
				{
					student.DeathType = DeathType.EasterEgg;

					if (!student.Male)
					{
						Instantiate(this.FemaleBloodyScream,
							rootTransform.transform.position + Vector3.up, Quaternion.identity);
					}
					else
					{
						Instantiate(this.MaleBloodyScream,
							rootTransform.transform.position + Vector3.up, Quaternion.identity);
					}

					//Debug.Log("Began at: " + Time.realtimeSinceStartup);

					student.BecomeRagdoll();

					//Debug.Log("Finished making ragdoll at: " + Time.realtimeSinceStartup);

					//student.Ragdoll.QuickDismember();
					student.Ragdoll.Dismember();

					//Debug.Log("Finished dismembering at: " + Time.realtimeSinceStartup);

					this.MyAudio.Play();

					// [af] Commented in JS code.
					//audio.PlayClipAtPoint(audio.clip, transform.position + Vector3.up);
				}
			}
		}
	}
}