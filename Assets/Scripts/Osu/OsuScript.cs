using UnityEngine;

public class OsuScript : MonoBehaviour
{
	public GameObject Button;
	public GameObject Circle;
	public GameObject New300;

	public GameObject OsuButton;
	public GameObject OsuCircle;
	public GameObject Osu300;

	public Texture ButtonTexture;
	public Texture CircleTexture;

	public float StartTime = 0.0f;

	public float Timer = 0.0f;

	/*
	void Update()
	{
		this.Timer += Time.deltaTime;

		if (this.Timer > this.StartTime)
		{
			if ((this.Button == null) && (this.New300 == null))
			{
				this.Button = Instantiate(this.OsuButton,
					this.transform.position, Quaternion.identity);

				this.Button.transform.parent = this.transform;

				this.Button.transform.localPosition = new Vector3(
					Random.Range(-171.0f, 171.0f),
					Random.Range(-68.0f, 68.0f),
					0.0f);

				this.Button.transform.localEulerAngles = Vector3.zero;
				this.Button.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

				UITexture buttonTexture = this.Button.GetComponent<UITexture>();
				buttonTexture.mainTexture = this.ButtonTexture;
				buttonTexture.color = new Color(
					buttonTexture.color.r,
					buttonTexture.color.g,
					buttonTexture.color.b,
					0.0f);

				this.Circle = Instantiate(this.OsuCircle,
					this.transform.position, Quaternion.identity);

				this.Circle.transform.parent = this.transform;

				this.Circle.transform.localPosition = this.Button.transform.localPosition;

				this.Circle.transform.localEulerAngles = Vector3.zero;
				this.Circle.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);

				UITexture circleTexture = this.Circle.GetComponent<UITexture>();
				circleTexture.mainTexture = this.CircleTexture;
				circleTexture.color = new Color(
					circleTexture.color.r,
					circleTexture.color.g,
					circleTexture.color.b,
					0.0f);
			}
			else
			{
				if (this.Button != null)
				{
					UITexture buttonTexture = this.Button.GetComponent<UITexture>();
					buttonTexture.color = new Color(
						buttonTexture.color.r,
						buttonTexture.color.g,
						buttonTexture.color.b,
						buttonTexture.color.a + Time.deltaTime);

					UITexture circleTexture = this.Circle.GetComponent<UITexture>();
					circleTexture.color = new Color(
						circleTexture.color.r,
						circleTexture.color.g,
						circleTexture.color.b,
						circleTexture.color.a + Time.deltaTime);

					this.Circle.transform.localScale = new Vector3(
						this.Circle.transform.localScale.x - Time.deltaTime,
						this.Circle.transform.localScale.y - Time.deltaTime,
						this.Circle.transform.localScale.z);

					if (this.Circle.transform.localScale.x <= 0.50f)
					{
						this.New300 = Instantiate(this.Osu300,
							this.transform.position, Quaternion.identity);

						this.New300.transform.parent = this.transform;

						this.New300.transform.localPosition = this.Button.transform.localPosition;

						this.New300.transform.localEulerAngles = Vector3.zero;
						this.New300.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

						Destroy(this.Button);
						Destroy(this.Circle);
					}
				}

				if (this.New300 != null)
				{
					UITexture new300Texture = this.New300.GetComponent<UITexture>();
					new300Texture.color = new Color(
						new300Texture.color.r,
						new300Texture.color.g,
						new300Texture.color.b,
						new300Texture.color.a - Time.deltaTime);

					if (new300Texture.color.a <= 0.0f)
					{
						Destroy(this.New300);
					}
				}
			}
		}
	}
	*/
}