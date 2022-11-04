using UnityEngine;

public class AnimatedGifScript : MonoBehaviour
{
	[SerializeField] UISprite Sprite;
	[SerializeField] string SpriteName;
	[SerializeField] int Start;
	[SerializeField] int Frame;
	[SerializeField] int Limit;
	[SerializeField] float FramesPerSecond;
	[SerializeField] float CurrentSeconds;

	void Awake()
	{
		Debug.Assert(this.FramesPerSecond > 0.0f, this.gameObject);
	}

	float SecondsPerFrame { get { return 1.0f / this.FramesPerSecond; } }

	void Update()
	{
		this.CurrentSeconds += Time.unscaledDeltaTime;

		while (this.CurrentSeconds >= this.SecondsPerFrame)
		{
			this.CurrentSeconds -= this.SecondsPerFrame;
			this.Frame++;

			if (this.Frame > this.Limit)
			{
				this.Frame = this.Start;
			}
		}

		this.Sprite.spriteName = this.SpriteName + this.Frame.ToString();
	}
}