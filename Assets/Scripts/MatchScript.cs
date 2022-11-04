using UnityEngine;

public class MatchScript : MonoBehaviour
{
	public float Timer = 0.0f;
	public Collider MyCollider;

	void Update()
	{
		if (this.GetComponent<Rigidbody>().useGravity)
		{
			this.transform.Rotate(Vector3.right * (Time.deltaTime * 360.0f));

			if (this.Timer > 0.0f)
			{
				if (this.MyCollider.isTrigger)
				{
					this.MyCollider.isTrigger = false;
				}
			}

			this.Timer += Time.deltaTime;

			if (this.Timer > 5.0f)
			{
				this.transform.localScale = new Vector3(
					this.transform.localScale.x,
					this.transform.localScale.y,
					this.transform.localScale.z - Time.deltaTime);

				if (this.transform.localScale.z < 0.0f)
				{
					Destroy(this.gameObject);
				}
			}
		}
	}
}
