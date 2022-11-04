using UnityEngine;

public class UniformSwapperScript : MonoBehaviour
{
	public Texture[] UniformTextures;
	public Mesh[] UniformMeshes;
	public Texture FaceTexture;
	public SkinnedMeshRenderer MyRenderer; // [af] Changed Renderer to SkinnedMeshRenderer.
	public int UniformID = 0;
	public int FaceID = 0;
	public int SkinID = 0;

	void Start()
	{
		int maleUniformPref = StudentGlobals.MaleUniform;
		
		this.MyRenderer.sharedMesh = this.UniformMeshes[maleUniformPref];

		Texture UniformTexture = this.UniformTextures[maleUniformPref];

		if (maleUniformPref == 1)
		{
			this.SkinID = 0;
			this.UniformID = 1;
			this.FaceID = 2;
		}
		else if (maleUniformPref == 2)
		{
			this.UniformID = 0;
			this.FaceID = 1;
			this.SkinID = 2;
		}
		else if (maleUniformPref == 3)
		{
			this.UniformID = 0;
			this.FaceID = 1;
			this.SkinID = 2;
		}
		else if (maleUniformPref == 4)
		{
			this.FaceID = 0;
			this.SkinID = 1;
			this.UniformID = 2;
		}
		else if (maleUniformPref == 5)
		{
			this.FaceID = 0;
			this.SkinID = 1;
			this.UniformID = 2;
		}
		else if (maleUniformPref == 6)
		{
			this.FaceID = 0;
			this.SkinID = 1;
			this.UniformID = 2;
		}

		this.MyRenderer.materials[this.FaceID].mainTexture = this.FaceTexture;
		this.MyRenderer.materials[this.SkinID].mainTexture = UniformTexture;
		this.MyRenderer.materials[this.UniformID].mainTexture = UniformTexture;
	}

	public Transform LookTarget;
	public Transform Head;

	void LateUpdate()
	{
		if (this.LookTarget != null)
		{
			this.Head.LookAt(this.LookTarget);
		}
	}
}
