using UnityEngine;

public class FallCheckerScript : MonoBehaviour
{
	public DumpsterLidScript Dumpster;
	public RagdollScript Ragdoll;

	public Collider MyCollider;

	void OnTriggerEnter(Collider other)
	{
		if (this.Ragdoll == null)
		{
			if (other.gameObject.layer == 11)
			{
				this.Ragdoll = other.transform.root.gameObject.GetComponent<RagdollScript>();

				this.Ragdoll.Prompt.Hide();
				this.Ragdoll.Prompt.enabled = false;
				this.Ragdoll.Prompt.MyCollider.enabled = false;
				this.Ragdoll.BloodPoolSpawner.enabled = false;
				this.Ragdoll.HideCollider = this.MyCollider;
				this.Ragdoll.Police.HiddenCorpses++;
				this.Ragdoll.Hidden = true;

				this.Dumpster.Corpse = this.Ragdoll.gameObject;
				this.Dumpster.Victim = this.Ragdoll.Student;
			}
		}
	}

	void Update()
	{
		if (this.Ragdoll != null)
		{
			if (this.Ragdoll.Prompt.transform.localPosition.y > -10.50f)
			{
				this.Ragdoll.Prompt.transform.localEulerAngles = new Vector3(-90.0f, 90.0f, 0.0f);
				this.Ragdoll.AllColliders[2].transform.localEulerAngles = Vector3.zero;
				this.Ragdoll.AllColliders[7].transform.localEulerAngles = new Vector3(0.0f, 0.0f, -80.0f);

				this.Ragdoll.Prompt.transform.position = new Vector3(
					this.Dumpster.transform.position.x,
					this.Ragdoll.Prompt.transform.position.y,
					this.Dumpster.transform.position.z);
			}
			else
			{
				this.GetComponent<AudioSource>().Play();
				this.Dumpster.Slide = true;
				this.Ragdoll = null;
			}
		}
	}
}
