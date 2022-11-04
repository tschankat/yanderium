using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBentoScript : MonoBehaviour
{
	public GameObject EmptyGameObject;

	public Transform PoisonSpot;

	public PromptScript Prompt;

	public bool Emetic;
	public bool Tranquil;
	public bool Headache;
	public bool Lethal;

	public bool Tampered;

	public int StudentID;

	void Update()
	{
		//Emetic
		if (Prompt.Circle[0].fillAmount == 0.0f)
		{
			if (Prompt.Yandere.Inventory.EmeticPoison)
			{
				Prompt.Yandere.Inventory.EmeticPoison = false;
				Prompt.Yandere.PoisonType = 1;
			}
			else
			{
				Prompt.Yandere.Inventory.RatPoison = false;
				Prompt.Yandere.PoisonType = 3;
			}

			Emetic = true;
			ShutOff();
		}
		//Tranquilizer
		else if (Prompt.Circle[1].fillAmount == 0.0f)
		{
			if (Prompt.Yandere.Inventory.Sedative)
			{
				Prompt.Yandere.Inventory.Sedative = false;
			}
			else
			{
				Prompt.Yandere.Inventory.Tranquilizer = false;
			}

			Prompt.Yandere.PoisonType = 4;

			Tranquil = true;
			ShutOff();
		}
		//Lethal
		else if (Prompt.Circle[2].fillAmount == 0.0f)
		{
			if (Prompt.Yandere.Inventory.LethalPoison)
			{
                Prompt.Yandere.Inventory.LethalPoisons--;

                if (Prompt.Yandere.Inventory.LethalPoisons == 0)
                {
                    Prompt.Yandere.Inventory.LethalPoison = false;
                }

                Prompt.Yandere.PoisonType = 2;
			}
			else
			{
				Prompt.Yandere.Inventory.ChemicalPoison = false;
				Prompt.Yandere.PoisonType = 2;
			}

			Lethal = true;
			ShutOff();
		}
		//Headache
		else if (Prompt.Circle[3].fillAmount == 0.0f)
		{
			Prompt.Yandere.Inventory.HeadachePoison = false;
			Prompt.Yandere.PoisonType = 5;

			Headache = true;
			ShutOff();
		}
	}

	void ShutOff()
	{
		GameObject NewObject = Instantiate(EmptyGameObject, transform.position, Quaternion.identity);

		PoisonSpot = NewObject.transform;

		PoisonSpot.position = new Vector3(
			PoisonSpot.position.x,
			Prompt.Yandere.transform.position.y,
			PoisonSpot.position.z);

		PoisonSpot.LookAt(Prompt.Yandere.transform.position);

		PoisonSpot.Translate(Vector3.forward * .25f);

		Prompt.Yandere.CharacterAnimation[AnimNames.FemalePoisoning].speed = 2;
		Prompt.Yandere.CharacterAnimation.CrossFade(AnimNames.FemalePoisoning);

		Prompt.Yandere.StudentManager.UpdateAllBentos();
		Prompt.Yandere.TargetBento = this;

		Prompt.Yandere.Poisoning = true;
		Prompt.Yandere.CanMove = false;

		Tampered = true;
		enabled = false;

		Prompt.enabled = false;
		Prompt.Hide();
	}

	public void UpdatePrompts()
	{
		Prompt.HideButton[0] = true;
		Prompt.HideButton[1] = true;
		Prompt.HideButton[2] = true;
		Prompt.HideButton[3] = true;

		if (Prompt.Yandere.Inventory.EmeticPoison || Prompt.Yandere.Inventory.RatPoison)
		{
			Prompt.HideButton[0] = false;
		}

		if (Prompt.Yandere.Inventory.Tranquilizer || Prompt.Yandere.Inventory.Sedative)
		{
			Prompt.HideButton[1] = false;
		}

		if (Prompt.Yandere.Inventory.LethalPoison || Prompt.Yandere.Inventory.ChemicalPoison)
		{
			Prompt.HideButton[2] = false;
		}

		if (Prompt.Yandere.Inventory.HeadachePoison)
		{
			Prompt.HideButton[3] = false;
		}
	}
}