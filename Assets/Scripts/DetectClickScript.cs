using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectClickScript : MonoBehaviour
{
	public Vector3 OriginalPosition;
	public Color OriginalColor;
	public Collider MyCollider;
	public Camera GUICamera;
	public UISprite Sprite;
	public UILabel Label;
	public bool Clicked;

	void Start()
	{
		OriginalPosition = transform.localPosition;
		OriginalColor = Sprite.color;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = GUICamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100))
			{
				if (hit.collider == MyCollider)
				{
					if (Label.color.a == 1)
					{
						Sprite.color = new Color(1, 1, 1, 1);
						Clicked = true;
					}
				}
			}
		}
	}

	void OnTriggerEnter()
	{
		if (Label.color.a == 1)
		{
			Sprite.color = Color.white;
		}
	}

	void OnTriggerExit()
	{
		Sprite.color = OriginalColor;
	}
}