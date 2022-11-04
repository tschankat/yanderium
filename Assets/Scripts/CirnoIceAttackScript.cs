using UnityEngine;

public class CirnoIceAttackScript : MonoBehaviour
{
	public GameObject IceExplosion;

	void Start()
	{
		Physics.IgnoreLayerCollision(18, 13, true);
		Physics.IgnoreLayerCollision(18, 18, true);
	}

	void OnCollisionEnter(Collision collision)
	{
		Instantiate(this.IceExplosion, this.transform.position, Quaternion.identity);

		if (collision.gameObject.layer == 9)
		{
			StudentScript NewStudent = collision.gameObject.GetComponent<StudentScript>();

			if (NewStudent != null)
			{
				NewStudent.SpawnAlarmDisc();
				NewStudent.BecomeRagdoll();
			}
		}

		Destroy(this.gameObject);
	}
}
