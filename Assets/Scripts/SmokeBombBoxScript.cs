using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBombBoxScript : MonoBehaviour
{
	public AlphabetScript Alphabet;

	public UITexture BombTexture;

	public PromptScript Prompt;

	public AudioSource MyAudio;

	public bool Amnesia;
	public bool Stink;

	void Update()
	{
		if (Prompt.Circle[0].fillAmount == 0)
		{
			if (!Amnesia)
			{
				Alphabet.RemainingBombs = 5;
				Alphabet.BombLabel.text = "" + 5;
			}
			else
			{
				Alphabet.RemainingBombs = 1;
				Alphabet.BombLabel.text = "" + 1;
			}

			Prompt.Circle[0].fillAmount = 1;

			if (Stink)
			{
				BombTexture.color = new Color(0, .5f, 0, 1);

				Prompt.Yandere.Inventory.AmnesiaBomb = false;
				Prompt.Yandere.Inventory.SmokeBomb = false;
				Prompt.Yandere.Inventory.StinkBomb = true;
			}
			else if (Amnesia)
			{
				BombTexture.color = new Color(1, .5f, 1, 1);

				Prompt.Yandere.Inventory.AmnesiaBomb = true;
				Prompt.Yandere.Inventory.SmokeBomb = false;
				Prompt.Yandere.Inventory.StinkBomb = false;
			}
			else
			{
				BombTexture.color = new Color(.5f, .5f, .5f, 1);

				Prompt.Yandere.Inventory.AmnesiaBomb = false;
				Prompt.Yandere.Inventory.StinkBomb = false;
				Prompt.Yandere.Inventory.SmokeBomb = true;
			}

			MyAudio.Play();
		}
	}
}