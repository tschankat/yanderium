using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_UVScroller : MonoBehaviour
{
	public int targetMaterialSlot = 0;
	//var scrollThis:Material;
	public float speedY = 0.5f;
	public float speedX = 0.0f;
	private float timeWentX = 0;
	private float timeWentY = 0;
	void Start () {

	}

	void Update () {
	timeWentY += Time.deltaTime*speedY;
	timeWentX += Time.deltaTime*speedX;


	GetComponent<Renderer>().materials[targetMaterialSlot].SetTextureOffset ("_MainTex", new Vector2(timeWentX, timeWentY));


	}
}