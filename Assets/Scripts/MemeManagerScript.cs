using UnityEngine;

public class MemeManagerScript : MonoBehaviour
{
	[SerializeField] GameObject[] Memes;

	void Start()
	{
		if (GameGlobals.LoveSick)
		{
			foreach (GameObject meme in this.Memes)
			{
				meme.SetActive(false);
			}
		}
	}
}