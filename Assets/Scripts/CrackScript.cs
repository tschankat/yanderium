using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackScript : MonoBehaviour
{
	public UITexture Texture;

	void Update()
	{
		Texture.fillAmount += Time.deltaTime * 10;
	}
}