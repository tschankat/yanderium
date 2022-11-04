using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedTextureScript : MonoBehaviour
{
	[SerializeField] Renderer MyRenderer;
	[SerializeField] int Start;
	[SerializeField] int Frame;
	[SerializeField] int Limit;
	[SerializeField] float FramesPerSecond;
	[SerializeField] float CurrentSeconds;

	public Texture[] Image;

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

		this.MyRenderer.material.mainTexture = this.Image[Frame];
	}
}