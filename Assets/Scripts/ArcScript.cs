using UnityEngine;

public class ArcScript : MonoBehaviour
{
	static readonly Vector3 NEW_ARC_RELATIVE_FORCE = Vector3.forward * 250.0f;

	public GameObject ArcTrail;
	public float Timer = 0.0f;

	void Update()
	{
		this.Timer += Time.deltaTime;

		if (this.Timer > 1.0f)
		{
			GameObject NewArcTrail = Instantiate(
				this.ArcTrail, this.transform.position, this.transform.rotation);
			NewArcTrail.GetComponent<Rigidbody>().AddRelativeForce(NEW_ARC_RELATIVE_FORCE);

			this.Timer = 0.0f;
		}
	}
}
