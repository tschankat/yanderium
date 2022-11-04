using UnityEngine;

public class CautionScript : MonoBehaviour
{
	public YandereScript Yandere;

	public UISprite Sprite;

	void Start()
	{
		this.Sprite.color = new Color(
			this.Sprite.color.r,
			this.Sprite.color.g,
			this.Sprite.color.b,
			0.0f);
	}

	void Update()
	{
		if (this.Yandere.Armed && this.Yandere.EquippedWeapon.Suspicious ||
			(this.Yandere.Bloodiness > 0.0f) || (this.Yandere.Sanity < (100.0f / 3.0f)) ||
			(this.Yandere.NearBodies > 0))
		{
			this.Sprite.color = new Color(
				this.Sprite.color.r,
				this.Sprite.color.g,
				this.Sprite.color.b,
				this.Sprite.color.a + Time.deltaTime);

			if (this.Sprite.color.a > 1.0f)
			{
				this.Sprite.color = new Color(
					this.Sprite.color.r,
					this.Sprite.color.g,
					this.Sprite.color.b,
					1.0f);
			}
		}
		else
		{
			this.Sprite.color = new Color(
				this.Sprite.color.r,
				this.Sprite.color.g,
				this.Sprite.color.b,
				this.Sprite.color.a - Time.deltaTime);

			if (this.Sprite.color.a < 0.0f)
			{
				this.Sprite.color = new Color(
					this.Sprite.color.r,
					this.Sprite.color.g,
					this.Sprite.color.b,
					0.0f);
			}
		}
	}
}
