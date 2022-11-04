using UnityEngine;

public class CardboardBoxScript : MonoBehaviour
{
	public PromptScript Prompt;

	void Start()
	{
		Physics.IgnoreCollision(Prompt.Yandere.GetComponent<Collider>(),
			this.GetComponent<Collider>());
	}

	void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Prompt.MyCollider.enabled = false;
			this.Prompt.Circle[0].fillAmount = 1.0f;
			this.GetComponent<Rigidbody>().isKinematic = true;
			this.GetComponent<Rigidbody>().useGravity = false;
			this.Prompt.enabled = false;
			this.Prompt.Hide();

			this.transform.parent = this.Prompt.Yandere.Hips;
			this.transform.localPosition = new Vector3(0.0f, -0.30f, 0.21f);
			this.transform.localEulerAngles = new Vector3(-13.333f, 0.0f, 0.0f);
		}

		if (this.transform.parent == this.Prompt.Yandere.Hips)
		{
			this.transform.localEulerAngles = Vector3.zero;

			if (this.Prompt.Yandere.Stance.Current != StanceType.Crawling)
			{
				this.transform.eulerAngles = new Vector3(
					0.0f,
					this.transform.eulerAngles.y,
					this.transform.eulerAngles.z);
			}

			if (Input.GetButtonDown(InputNames.Xbox_RB))
			{
				this.Prompt.MyCollider.enabled = true;
				this.GetComponent<Rigidbody>().isKinematic = false;
				this.GetComponent<Rigidbody>().useGravity = true;
				this.transform.parent = null;
				this.Prompt.enabled = true;
			}
		}
	}
}
