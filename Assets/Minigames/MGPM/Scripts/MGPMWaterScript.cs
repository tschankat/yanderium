using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGPMWaterScript : MonoBehaviour
{
	public Renderer MyRenderer;

	public Texture[] Sprite;

	public float Speed;
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
				Frame = 0;
			}

			MyRenderer.material.mainTexture = Sprite[Frame];
		}

		transform.localPosition = new Vector3(0, transform.localPosition.y - Speed * Time.deltaTime, 3);

		if (transform.localPosition.y < -640)
		{
			transform.localPosition = new Vector3(0, transform.localPosition.y + 1280, 3);
		}
	}
}
