using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniformSetterScript : MonoBehaviour
{
	public Texture[] FemaleUniformTextures;
	public Texture[] MaleUniformTextures;

	public SkinnedMeshRenderer MyRenderer;

	public Mesh[] FemaleUniforms;
	public Mesh[] MaleUniforms;

	public Texture SenpaiFace;
	public Texture SenpaiSkin;
	public Texture RyobaFace;
	public Texture AyanoFace;
	public Texture OsanaFace;

	public int FaceID = 0;
	public int SkinID = 0;
	public int UniformID = 0;
	public int StudentID = 0;

	public bool AttachHair;
	public bool Male;

	public Transform Head;
	public GameObject[] Hair;
	public int HairID;

	public void Start()
	{
		if (MyRenderer == null)
		{
			MyRenderer = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>();
		}

		if (Male)
		{
			SetMaleUniform();
		}
		else
		{
			SetFemaleUniform();
		}

		if (AttachHair)
		{
			GameObject NewHair = Instantiate(Hair[HairID], transform.position, transform.rotation);

			Head = transform.Find("Character/PelvisRoot/Hips/Spine/Spine1/Spine2/Spine3/Neck/Head").transform;

			NewHair.transform.parent = Head;
		}
	}

	public void SetMaleUniform()
	{
		//Debug.Log("StudentGlobals.MaleUniform is: " + StudentGlobals.MaleUniform);

		MyRenderer.sharedMesh = MaleUniforms[StudentGlobals.MaleUniform];

		if (StudentGlobals.MaleUniform == 1)
		{
			SkinID = 0;
			UniformID = 1;
			FaceID = 2;
		}
		else if (StudentGlobals.MaleUniform == 2 ||
				 StudentGlobals.MaleUniform == 3)
		{
			UniformID = 0;
			FaceID = 1;
			SkinID = 2;
		}
		else if (StudentGlobals.MaleUniform == 4 ||
		 		 StudentGlobals.MaleUniform == 5 ||
	   			 StudentGlobals.MaleUniform == 6)
		{
			FaceID = 0;
			SkinID = 1;
			UniformID = 2;
		}

		MyRenderer.materials[FaceID].mainTexture = SenpaiFace;
		MyRenderer.materials[SkinID].mainTexture = SenpaiSkin;
		MyRenderer.materials[UniformID].mainTexture = MaleUniformTextures[StudentGlobals.MaleUniform];
	}

	public void SetFemaleUniform()
	{
		//Debug.Log("StudentGlobals.FemaleUniform is: " + StudentGlobals.FemaleUniform);

		MyRenderer.sharedMesh = FemaleUniforms[StudentGlobals.FemaleUniform];

		MyRenderer.materials[0].mainTexture = FemaleUniformTextures[StudentGlobals.FemaleUniform];
		MyRenderer.materials[1].mainTexture = FemaleUniformTextures[StudentGlobals.FemaleUniform];

		if (StudentID == 0)
		{
			MyRenderer.materials[2].mainTexture = RyobaFace;
		}
		else if (StudentID == 1)
		{
			MyRenderer.materials[2].mainTexture = AyanoFace;
		}
		else
		{
			MyRenderer.materials[2].mainTexture = OsanaFace;
		}
	}
}