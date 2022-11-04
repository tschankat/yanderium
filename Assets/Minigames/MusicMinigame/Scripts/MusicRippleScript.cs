using System.Collections.Generic;
using UnityEngine;

public class MusicRippleScript : MonoBehaviour
{
	public Renderer MyRenderer;

	public Texture[] Sprite;

	public float Timer;
	public float FPS;

	public int Frame;

	void Update ()
	{
		Timer += Time.deltaTime;

		if (Timer > FPS)
		{
			Timer = 0;

			Frame++;

			if (Frame == Sprite.Length)
			{
				Destroy (gameObject);
			}
			else
			{
				MyRenderer.material.mainTexture = Sprite[Frame];
			}
		}
	}
}