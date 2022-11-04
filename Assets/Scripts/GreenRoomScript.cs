using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenRoomScript : MonoBehaviour
{
	public QualityManagerScript QualityManager;

	public Color[] Colors;

	public Renderer[] Renderers;

	public int ID;

	void Start()
	{
		QualityManager.Obscurance.enabled = false;
		UpdateColor();
	}

	void Update()
	{
		if (Input.GetKeyDown("z"))
		{
			UpdateColor();
		}
	}

	void UpdateColor()
	{
		ID++;

		if (ID > 7)
		{
			ID = 0;
		}

		Renderers[0].material.color = Colors[ID];
		Renderers[1].material.color = Colors[ID];
	}
}