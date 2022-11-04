using UnityEngine;

public class HideColliderScript : MonoBehaviour
{
	public RagdollScript Corpse;

	public Collider MyCollider;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 11)
		{
			GameObject rootObject = other.gameObject.transform.root.gameObject;

			if (!rootObject.GetComponent<StudentScript>().Alive)
			{
				this.Corpse = rootObject.GetComponent<RagdollScript>();

				if (!this.Corpse.Hidden)
				{
					this.Corpse.HideCollider = this.MyCollider;
					this.Corpse.Police.HiddenCorpses++;
					this.Corpse.Hidden = true;
				}
			}
		}
	}
}
