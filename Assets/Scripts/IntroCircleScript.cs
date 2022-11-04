using UnityEngine;

public class IntroCircleScript : MonoBehaviour
{
	public UISprite Sprite;
	public UILabel Label;
	public float[] StartTime;
	public float[] Duration;
	public string[] Text;
	public float CurrentTime = 0.0f;
	public float LastTime = 0.0f;
	public float Timer = 0.0f;
	public int ID = 0;

	void Update()
	{
		this.Timer += Time.deltaTime;

		if (this.ID < this.StartTime.Length)
		{
			if (this.Timer > this.StartTime[this.ID])
			{
				this.CurrentTime = this.Duration[this.ID];
				this.LastTime = this.Duration[this.ID];
				this.Label.text = this.Text[this.ID];
				this.ID++;
			}
		}

		if (this.CurrentTime > 0.0f)
		{
			this.CurrentTime -= Time.deltaTime;
		}

		if (this.Timer > 1.0f)
		{
			this.Sprite.fillAmount = this.CurrentTime / this.LastTime;

			if (this.Sprite.fillAmount == 0.0f)
			{
				this.Label.text = string.Empty;
			}
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			this.CurrentTime -= 5.0f;
			this.Timer += 5.0f;
		}
	}
}
