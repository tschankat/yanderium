using UnityEngine;

public class ChangeTextureScript : MonoBehaviour
{
	public Renderer MyRenderer;

	public Texture[] Textures;

	public int ID = 1;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftAlt))
		{
			this.ID++;

			if (this.ID == this.Textures.Length)
			{
				this.ID = 1;
			}

			this.MyRenderer.material.mainTexture = this.Textures[this.ID];
		}
	}
}
