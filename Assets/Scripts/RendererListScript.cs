using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererListScript : MonoBehaviour
{
	public Renderer[] Renderers;

	void Start ()
	{
		Transform[] Transforms = gameObject.GetComponentsInChildren<Transform>();

		int ID = 0;

		foreach (Transform Trans in Transforms)
		{
			if (Trans.gameObject.GetComponent<Renderer>() != null)
			{
				Renderers[ID] = Trans.gameObject.GetComponent<Renderer>();
				ID++;
			}
		}
	}

	//public int ID;

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			foreach (Renderer Ren in Renderers)
			{
				//Debug.Log("Currently, we're on number: " + ID);
				//ID++;

				Ren.enabled = !Ren.enabled;
			}
		}
	}
}