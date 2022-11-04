using UnityEngine;

public class RivalPoseScript : MonoBehaviour
{
	public GameObject Character;

	public SkinnedMeshRenderer MyRenderer;

	public Texture[] FemaleUniformTextures;
	public Mesh[] FemaleUniforms;

	public Texture[] TestTextures;
	public Texture HairTexture;

	public string[] AnimNames;

	public int ID = -1;

	void Start()
	{
		int femaleUniformPref = StudentGlobals.FemaleUniform;

		this.MyRenderer.sharedMesh = this.FemaleUniforms[femaleUniformPref];

		if (femaleUniformPref == 1)
		{
			this.MyRenderer.materials[0].mainTexture = this.FemaleUniformTextures[femaleUniformPref];
			this.MyRenderer.materials[1].mainTexture = this.HairTexture;
			this.MyRenderer.materials[2].mainTexture = this.HairTexture;
			this.MyRenderer.materials[3].mainTexture = this.FemaleUniformTextures[femaleUniformPref];
		}
		else if (femaleUniformPref == 2)
		{
			this.MyRenderer.materials[0].mainTexture = this.FemaleUniformTextures[femaleUniformPref];
			this.MyRenderer.materials[1].mainTexture = this.FemaleUniformTextures[femaleUniformPref];
			this.MyRenderer.materials[2].mainTexture = this.HairTexture;
			this.MyRenderer.materials[3].mainTexture = this.HairTexture;
		}
		else if (femaleUniformPref == 3)
		{
			this.MyRenderer.materials[0].mainTexture = this.HairTexture;
			this.MyRenderer.materials[1].mainTexture = this.HairTexture;
			this.MyRenderer.materials[2].mainTexture = this.FemaleUniformTextures[femaleUniformPref];
			this.MyRenderer.materials[3].mainTexture = this.FemaleUniformTextures[femaleUniformPref];
		}
		else if (femaleUniformPref == 4)
		{
			this.MyRenderer.materials[0].mainTexture = this.HairTexture;
			this.MyRenderer.materials[1].mainTexture = this.HairTexture;
			this.MyRenderer.materials[2].mainTexture = this.FemaleUniformTextures[femaleUniformPref];
			this.MyRenderer.materials[3].mainTexture = this.FemaleUniformTextures[femaleUniformPref];
		}
		else if (femaleUniformPref == 5)
		{
			this.MyRenderer.materials[0].mainTexture = this.HairTexture;
			this.MyRenderer.materials[1].mainTexture = this.HairTexture;
			this.MyRenderer.materials[2].mainTexture = this.FemaleUniformTextures[femaleUniformPref];
			this.MyRenderer.materials[3].mainTexture = this.FemaleUniformTextures[femaleUniformPref];
		}
		else if (femaleUniformPref == 6)
		{
			this.MyRenderer.materials[0].mainTexture = this.FemaleUniformTextures[femaleUniformPref];
			this.MyRenderer.materials[1].mainTexture = this.FemaleUniformTextures[femaleUniformPref];
			this.MyRenderer.materials[2].mainTexture = this.HairTexture;
			this.MyRenderer.materials[3].mainTexture = this.HairTexture;
		}
	}

	/*
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			this.ID++;

			if (this.ID > (this.AnimNames.Length - 1))
			{
				this.ID = 0;
			}

			this.Character.GetComponent<Animation>().Play(this.AnimNames[this.ID]);
		}
	}
	*/
}