using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CensorBloodScript : MonoBehaviour
{
	public ParticleSystem MyParticles;
	public Texture Flower;

	void Start()
	{
		if (GameGlobals.CensorBlood)
		{
			var main = MyParticles.main;
			main.startColor = new Color(1, 1, 1, 1);

			var col = MyParticles.colorOverLifetime;
			col.enabled = false;

			MyParticles.GetComponent<Renderer>().material.mainTexture = Flower;
		}
	}
}