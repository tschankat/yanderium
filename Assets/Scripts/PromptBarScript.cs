using UnityEngine;

public class PromptBarScript : MonoBehaviour
{
	public UISprite[] Button;
	public UILabel[] Label;

	public UIPanel Panel;

	public bool Show = false;

	public int ID = 0;

	void Awake()
	{
		this.transform.localPosition = new Vector3(
			this.transform.localPosition.x,
			-627.0f,
			this.transform.localPosition.z);

		// [af] Converted while loop to for loop.
		for (this.ID = 0; this.ID < this.Label.Length; this.ID++)
		{
			this.Label[this.ID].text = string.Empty;
		}
	}

	void Start()
	{
		this.UpdateButtons();
	}

	void Update()
	{
		float speed = Time.unscaledDeltaTime * 10.0f;

		if (!this.Show)
		{
			if (this.Panel.enabled)
			{
				this.transform.localPosition = new Vector3(
					this.transform.localPosition.x,
					Mathf.Lerp(this.transform.localPosition.y, -628.0f, speed),
					this.transform.localPosition.z);

				if (this.transform.localPosition.y < -627.0f)
				{
					this.transform.localPosition = new Vector3(
						this.transform.localPosition.x,
						-628.0f,
						this.transform.localPosition.z);

					if (this.Panel != null)
					{
						this.Panel.enabled = false;
					}
				}
			}
		}
		else
		{
			this.transform.localPosition = new Vector3(
				this.transform.localPosition.x,
				Mathf.Lerp(this.transform.localPosition.y, -528.5f, speed),
				this.transform.localPosition.z);
		}
	}

	public void UpdateButtons()
	{
		if (this.Panel != null)
		{
			this.Panel.enabled = true;
		}
		
		// [af] Converted while loop to for loop.
		for (this.ID = 0; this.ID < this.Label.Length; this.ID++)
		{
			// [af] Converted if/else statement to boolean expression.
			this.Button[this.ID].enabled = this.Label[this.ID].text.Length > 0;
		}
	}

	public void ClearButtons()
	{
		// [af] Converted while loop to for loop.
		for (this.ID = 0; this.ID < this.Label.Length; this.ID++)
		{
			this.Label[this.ID].text = string.Empty;
			this.Button[this.ID].enabled = false;
		}
	}
}
