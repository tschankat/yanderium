using UnityEngine;

public class LocationScript : MonoBehaviour
{
	public UILabel Label;
	public UISprite BG;
	public bool Show = false;

	void Start()
	{
		this.Label.color = new Color(
			this.Label.color.r, this.Label.color.g, this.Label.color.b, 0.0f);
		this.BG.color = new Color(
			this.BG.color.r, this.BG.color.g, this.BG.color.b, 0.0f);
	}

	void Update()
	{
		if (this.Show)
		{
			this.BG.color = new Color(
				this.BG.color.r, this.BG.color.g, this.BG.color.b, 
				this.BG.color.a + (Time.deltaTime * 10.0f));

			if (this.BG.color.a > 1.0f)
			{
				this.BG.color = new Color(
					this.BG.color.r, this.BG.color.g, this.BG.color.b, 1.0f);
			}
			
			this.Label.color = new Color(
				this.Label.color.r, this.Label.color.g, this.Label.color.b, this.BG.color.a);
		}
		else
		{
			this.BG.color = new Color(
				this.BG.color.r, this.BG.color.g,
				this.BG.color.b, this.BG.color.a - (Time.deltaTime * 10.0f));

			if (this.BG.color.a < 0.0f)
			{
				this.BG.color = new Color(
					this.BG.color.r, this.BG.color.g, this.BG.color.b, 0.0f);
			}

			this.Label.color = new Color(
				this.Label.color.r, this.Label.color.g, this.Label.color.b, this.BG.color.a);
		}
	}
}
