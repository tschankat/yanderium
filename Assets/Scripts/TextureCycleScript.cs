using UnityEngine;

public class TextureCycleScript : MonoBehaviour
{
	public UITexture Sprite;
	[SerializeField] int Start;
	[SerializeField] int Frame;
	[SerializeField] int Limit;
	[SerializeField] float FramesPerSecond;
	[SerializeField] float CurrentSeconds;
	[SerializeField] Texture[] Textures;

	public int ID;

	void Awake()
	{
		Debug.Assert(this.FramesPerSecond > 0.0f, this.gameObject);
	}

	float SecondsPerFrame { get { return 1.0f / this.FramesPerSecond; } }

	void Update()
	{
		//this.CurrentSeconds += Time.unscaledDeltaTime;

		//while (this.CurrentSeconds >= this.SecondsPerFrame)
		//{
			//this.CurrentSeconds -= this.SecondsPerFrame;

		this.ID++;

		if (ID > 1)
		{
			ID = 0;

			this.Frame++;

			if (this.Frame > this.Limit)
			{
				this.Frame = this.Start;
			}
		
		}

		this.Sprite.mainTexture = Textures[Frame];
	}
}
