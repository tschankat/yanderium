using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugScript : MonoBehaviour
{
	public PromptScript Prompt;
	public Renderer MyRenderer;

	public AudioSource MyAudio;
	public AudioClip[] Praise;

	void Start()
	{
		MyRenderer.enabled = false;
	}

	void Update()
	{
		if (Prompt.Circle[0].fillAmount == 0.0f)
		{
			MyAudio.clip = Praise[Random.Range(0, Praise.Length)];
			MyAudio.Play();

			MyRenderer.enabled = true;
			Prompt.Yandere.Inventory.PantyShots += 10;
			enabled = false;

			Prompt.enabled = false;
			Prompt.Hide();
		}
	}
}
