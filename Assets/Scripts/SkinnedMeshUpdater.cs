using UnityEngine;
using System;

public class SkinnedMeshUpdater : MonoBehaviour
{
	public SkinnedMeshRenderer MyRenderer;
	public GameObject TransformEffect;
	public GameObject[] Characters;
	public PromptScript Prompt;
	public GameObject BreastR;
	public GameObject BreastL;

	public GameObject FumiGlasses;
	public GameObject NinaGlasses;

	SkinnedMeshRenderer TempRenderer;

	public Texture[] Bodies;
	public Texture[] Faces;

	public float Timer;

	public int ID;

	public void Start()
	{
		GlassesCheck();
	}

	public void Update ()
	{
		if (Prompt.Circle[0].fillAmount == 0)
		{
			Instantiate(TransformEffect, Prompt.Yandere.Hips.position, Quaternion.identity);

			Prompt.Yandere.CharacterAnimation.Play(Prompt.Yandere.IdleAnim);

			Prompt.Yandere.CanMove = false;
			Prompt.Yandere.Egg = true;

			BreastR.name = "RightBreast";
			BreastL.name = "LeftBreast";

			Timer = 1;

			ID++;

			if (ID == Characters.Length)
			{
				ID = 1;
			}

			this.Prompt.Yandere.Hairstyle = 120 + ID;
			this.Prompt.Yandere.UpdateHair();

			GlassesCheck();
			UpdateSkin();
		}

		if (Timer > 0)
		{
			Timer = Mathf.MoveTowards(Timer, 0, Time.deltaTime);

			if (Timer == 0)
			{
				Prompt.Yandere.CanMove = true;
			}
		}
	}

	public void UpdateSkin ()
	{
		GameObject TemporaryObject = GameObject.Instantiate(Characters[ID], Vector3.zero, Quaternion.identity) as GameObject;
		TempRenderer = TemporaryObject.GetComponentInChildren<SkinnedMeshRenderer>();
		UpdateMeshRenderer(TempRenderer);
		UnityEngine.Object.Destroy(TemporaryObject);

		MyRenderer.materials[0].mainTexture = Bodies[ID];
		MyRenderer.materials[1].mainTexture = Bodies[ID];
		MyRenderer.materials[2].mainTexture = Faces[ID];
	}

	void UpdateMeshRenderer (SkinnedMeshRenderer newMeshRenderer)
	{
		var meshrenderer = this.Prompt.Yandere.MyRenderer;
		meshrenderer.sharedMesh = newMeshRenderer.sharedMesh;

		Transform[] childrens = this.Prompt.Yandere.transform.GetComponentsInChildren<Transform> (true);

		// sort bones.
		Transform[] bones = new Transform[newMeshRenderer.bones.Length];
		for (int boneOrder = 0; boneOrder < newMeshRenderer.bones.Length; boneOrder++) {
			bones [boneOrder] = Array.Find<Transform> (childrens, c => c.name == newMeshRenderer.bones [boneOrder].name);
		}

		meshrenderer.bones = bones;
	}

	void GlassesCheck()
	{
		FumiGlasses.SetActive(false);
		NinaGlasses.SetActive(false);

		if (ID == 7)
		{
			FumiGlasses.SetActive(true);
		}
		else if (ID == 8)
		{
			NinaGlasses.SetActive(true);
		}
	}
}