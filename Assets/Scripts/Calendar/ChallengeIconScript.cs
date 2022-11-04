using UnityEngine;

public class ChallengeIconScript : MonoBehaviour
{
	public UITexture LargeIcon;
	public UISprite IconFrame;
	public UISprite NameFrame;
	public UITexture Icon;
	public UILabel Name;

	public float Dark = 0.0f;

	float R;
	float G;
	float B;

	void Start()
	{
		if (GameGlobals.LoveSick)
		{
			this.R = 1.0f;
			this.G = 0.0f;
			this.B = 0.0f;
		}
		else
		{
			this.R = 1.0f;
			this.G = 1.0f;
			this.B = 1.0f;
		}
	}

	void Update()
	{
		if ((this.transform.position.x > -0.125f) &&
			(this.transform.position.x < 0.125f))
		{
			if (this.Icon != null)
			{
				this.LargeIcon.mainTexture = this.Icon.mainTexture;
			}

			this.Dark -= Time.deltaTime * 10.0f;

			if (this.Dark < 0.0f)
			{
				this.Dark = 0.0f;
			}
		}
		else
		{
			this.Dark += Time.deltaTime * 10.0f;

			if (this.Dark > 1.0f)
			{
				this.Dark = 1.0f;
			}
		}

		this.IconFrame.color = new Color(this.Dark * R, this.Dark * G, this.Dark * B, 1.0f);
		this.NameFrame.color = new Color(this.Dark * R, this.Dark * G, this.Dark * B, 1.0f);
		this.Name.color = new Color(this.Dark * R, this.Dark * G, this.Dark * B, 1.0f);

		if (GameGlobals.LoveSick)
		{
			if ((this.transform.position.x > -0.125f) &&
				(this.transform.position.x < 0.125f))
			{
				this.IconFrame.color = Color.white;
				this.NameFrame.color = Color.white;
				this.Name.color = Color.white;
			}
			else
			{
				this.IconFrame.color = new Color(R, G, B, 1.0f);
				this.NameFrame.color = new Color(R, G, B, 1.0f);
				this.Name.color = new Color(R, G, B, 1.0f);
			}
		}
	}
}
