using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecuritySystemScript : MonoBehaviour
{
	public PromptScript Prompt;
	public bool Evidence;
	public bool Masked;

	public SecurityCameraScript[] Cameras;
	public MetalDetectorScript[] Detectors;

	void Start ()
	{
		if (!SchoolGlobals.HighSecurity)
		{
			enabled = false;

			Prompt.Hide();
			Prompt.enabled = false;
		}
	}

	void Update ()
	{
		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			int ID = 0;

			while (ID < Cameras.Length)
			{
				Cameras[ID].transform.parent.transform.parent.gameObject.GetComponent<AudioSource>().Stop();
				Cameras[ID].gameObject.SetActive(false);
				ID++;
			}

			ID = 0;

			while (ID < Detectors.Length)
			{
				Detectors[ID].MyCollider.enabled = false;
				Detectors[ID].enabled = false;
				ID++;
			}

			GetComponent<AudioSource>().Play();

			Prompt.Hide();
			Prompt.enabled = false;

			Evidence = false;
			enabled = false;
		}
	}
}
