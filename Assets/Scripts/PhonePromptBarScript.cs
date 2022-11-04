using UnityEngine;

public class PhonePromptBarScript : MonoBehaviour
{
	public UIPanel Panel;
	public bool Show = false;
	public UILabel Label;

	void Start()
	{
		this.transform.localPosition = new Vector3(
			this.transform.localPosition.x,
			630.0f,
			this.transform.localPosition.z);

		this.Panel.enabled = false;
	}

	void Update()
	{
		float lerpSpeed = Time.unscaledDeltaTime * 10.0f;

		if (!this.Show)
		{
			if (this.Panel.enabled)
			{
				this.transform.localPosition = new Vector3(
					this.transform.localPosition.x,
					Mathf.Lerp(this.transform.localPosition.y, 631.0f, lerpSpeed),
					this.transform.localPosition.z);

				if (this.transform.localPosition.y < 630.0f)
				{
					this.transform.localPosition = new Vector3(
						this.transform.localPosition.x,
						631.0f,
						this.transform.localPosition.z);

					this.Panel.enabled = false;
				}
			}
		}
		else
		{
			this.transform.localPosition = new Vector3(
				this.transform.localPosition.x,
				Mathf.Lerp(this.transform.localPosition.y, 530.0f, lerpSpeed),
				this.transform.localPosition.z);
		}
	}
}
