using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDistanceDisableScript : MonoBehaviour
{
	public Transform RenderTarget;
	public Transform Yandere;

	public Camera MyCamera;

	void Update ()
	{
		if (Vector3.Distance(Yandere.position, RenderTarget.position) > 15)
		{
			MyCamera.enabled = false;
		}
		else
		{
			MyCamera.enabled = true;
		}
	}
}