using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemistScannerScript : MonoBehaviour
{
	public StudentScript Student;

	public Renderer MyRenderer;

	public Texture AlarmedEyes;
	public Texture DeadEyes;
	public Texture SadEyes;

	public Texture[] Textures;

	public float Timer;

	public int PreviousID;
	public int ID;

	void Update ()
	{
		if (Student.Ragdoll != null && Student.Ragdoll.enabled)
		{
			MyRenderer.materials[1].mainTexture = DeadEyes;
			enabled = false;
		}
		else if (Student.Dying)
		{
			if (MyRenderer.materials[1].mainTexture != AlarmedEyes)
			{
				MyRenderer.materials[1].mainTexture = AlarmedEyes;
			}
		}
		else if (Student.Emetic || Student.Lethal || Student.Tranquil || Student.Headache)
		{
			if (MyRenderer.materials[1].mainTexture != Textures[6])
			{
				MyRenderer.materials[1].mainTexture = Textures[6];
			}
		}
		else if (Student.Grudge)
		{
			if (MyRenderer.materials[1].mainTexture != Textures[1])
			{
				MyRenderer.materials[1].mainTexture = Textures[1];
			}
		}
		else if (Student.LostTeacherTrust)
		{
			if (MyRenderer.materials[1].mainTexture != SadEyes)
			{
				MyRenderer.materials[1].mainTexture = SadEyes;
			}
		}
		else if (Student.WitnessedMurder || Student.WitnessedCorpse)
		{
			if (MyRenderer.materials[1].mainTexture != AlarmedEyes)
			{
				MyRenderer.materials[1].mainTexture = AlarmedEyes;
			}
		}
		else
		{
			Timer += Time.deltaTime;

			if (Timer > 2)
			{
				while (ID == PreviousID)
				{
					ID = Random.Range(0, Textures.Length);
				}

				MyRenderer.materials[1].mainTexture = Textures[ID];
				PreviousID = ID;

				Timer = 0;
			}
		}
	}
}