using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDetectClickScript : MonoBehaviour
{
	public InventoryItemScript InventoryItem;
	public Collider MyCollider;

	public bool Clicked;

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100))
			{
				if (hit.collider == MyCollider)
				{
					Clicked = true;
				}
			}
		}
	}
}