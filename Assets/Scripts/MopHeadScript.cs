using UnityEngine;

public class MopHeadScript : MonoBehaviour
{
	public BloodPoolScript BloodPool;

	public MopScript Mop;

	void OnTriggerStay(Collider other)
	{
		if (this.Mop.Bloodiness < 100.0f)
		{
			if (other.tag == "Puddle")
			{
				this.BloodPool = other.gameObject.GetComponent<BloodPoolScript>();

				if (this.BloodPool != null)
				{
					// [af] Replaced unnecessary component access with class member.
					this.BloodPool.Grow = false;

					other.transform.localScale -= new Vector3(
						Time.deltaTime, Time.deltaTime, Time.deltaTime);

					if (this.BloodPool.Blood)
					{
						this.Mop.Bloodiness += Time.deltaTime * 10.0f;
						this.Mop.UpdateBlood();
					}

					if (other.transform.localScale.x < 0.10f)
					{
						Destroy(other.gameObject);
					}
				}
				else
				{
					Destroy(other.gameObject);
				}
			}
		}
	}
}
