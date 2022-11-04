using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGPMGifScript : MonoBehaviour
{
	public Texture[] Frames;

	public Renderer MyRenderer;

	public float Timer;

	public float FPS;

	public int ID;

	void Update()
	{
		Timer += Time.deltaTime;

		if (Timer > FPS)
		{
			ID++;

			if (ID == Frames.Length)
			{
				ID = 0;
			}

			MyRenderer.material.mainTexture = Frames[ID];
			Timer = 0;
		}
	}
}