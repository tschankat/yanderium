using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DokiScript : MonoBehaviour
{
	public MusicCreditScript Credits;
	public YandereScript Yandere;

	public PromptScript OtherPrompt;
	public PromptScript Prompt;

	public GameObject TransformEffect;
	public Texture DokiTexture;
	public Texture[] DokiSocks;
	public Texture[] DokiHair;

	public string[] DokiName;

	public int ID;

	void Update ()
	{
		if (!Yandere.Egg)
		{
			if (OtherPrompt.Circle[0].fillAmount == 0)
			{
				Prompt.Hide();
				Prompt.enabled = false;
				enabled = false;
			}

			if (Prompt.Circle[0].fillAmount == 0)
			{
				Yandere.PantyAttacher.newRenderer.enabled = false;

				Prompt.Circle[0].fillAmount = 1;

				Instantiate(TransformEffect, Yandere.Hips.position, Quaternion.identity);

				Yandere.MyRenderer.sharedMesh = Yandere.Uniforms[4];

				Yandere.MyRenderer.materials[0].mainTexture = DokiTexture;
				Yandere.MyRenderer.materials[1].mainTexture = DokiTexture;

				ID++;

				if (ID > 4)
				{
					ID = 1;
				}

				Credits.SongLabel.text = DokiName[ID] + " from Doki Doki Literature Club";
				Credits.BandLabel.text = "by Team Salvato";
				Credits.Panel.enabled = true;
				Credits.Slide = true;
				Credits.Timer = 0;

				if (ID == 1)
				{
					Yandere.MyRenderer.materials[0].SetTexture("_OverlayTex", this.DokiSocks[0]);
					Yandere.MyRenderer.materials[1].SetTexture("_OverlayTex", this.DokiSocks[0]);
				}
				else
				{
					Yandere.MyRenderer.materials[0].SetTexture("_OverlayTex", this.DokiSocks[1]);
					Yandere.MyRenderer.materials[1].SetTexture("_OverlayTex", this.DokiSocks[1]);
				}

				Debug.Log("Activating shadows on Yandere-chan.");
							
				Yandere.MyRenderer.materials[0].SetFloat("_BlendAmount", 1.0f);
				Yandere.MyRenderer.materials[1].SetFloat("_BlendAmount", 1.0f);

				Yandere.MyRenderer.materials[2].mainTexture = DokiHair[ID];
				Yandere.Hairstyle = 136 + ID;
				Yandere.UpdateHair();
			}
		}
		else
		{
			Prompt.Hide();
			Prompt.enabled = false;
			enabled = false;
		}
	}
}
