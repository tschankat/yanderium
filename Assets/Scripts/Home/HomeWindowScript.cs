using UnityEngine;

public class HomeWindowScript : MonoBehaviour
{
	public UISprite Sprite;
	public bool Show = false;

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
		// [af] Replaced if/else statement with assignment and ternary expression.
		this.Sprite.color = new Color(
			this.Sprite.color.r,
			this.Sprite.color.g,
			this.Sprite.color.b,
			Mathf.Lerp(this.Sprite.color.a, this.Show ? 1.0f : 0.0f, Time.deltaTime * 10.0f));
	}
}
