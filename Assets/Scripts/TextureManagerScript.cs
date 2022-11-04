using UnityEngine;

public class TextureManagerScript : MonoBehaviour
{
	public Texture[] UniformTextures;
	public Texture[] CasualTextures;
	public Texture[] SocksTextures;
	public Texture2D PurpleStockings;
	public Texture2D GreenStockings;
	public Texture2D Base2D;
	public Texture2D Overlay2D;

	public Texture2D MergeTextures(Texture2D BackgroundTex, Texture2D TopTex)
	{
		Texture2D NewTexture = new Texture2D(1024, 1024);
		Color32[] BackPixs = BackgroundTex.GetPixels32();
		Color32[] TopPixs = TopTex.GetPixels32();

		// [af] Converted while loop to for loop.
		for (int ID = 0; ID < TopPixs.Length; ID++)
		{
			if (TopPixs[ID].a != 0)
			{
				BackPixs[ID] = TopPixs[ID];
			}
		}

		NewTexture.SetPixels32(BackPixs);
		NewTexture.Apply();

		return NewTexture;
	}
}
