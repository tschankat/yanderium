using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheapFilmGrainScript : MonoBehaviour
{
	public Renderer MyRenderer;

	public float Floor = 100.0f;
	public float Ceiling = 200.0f;

	void Update ()
	{
		MyRenderer.material.mainTextureScale = new Vector2(Random.Range(Floor, Ceiling), Random.Range(Floor, Ceiling));
	}
}
