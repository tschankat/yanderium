using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeCorkboardPhotoScript : MonoBehaviour
{
	public int ArrayID;
	public int ID;

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.layer == 4)
		{
			transform.localScale = new Vector3(
				Mathf.MoveTowards(transform.localScale.x, 1, Time.deltaTime * 10),
				Mathf.MoveTowards(transform.localScale.y, 1, Time.deltaTime * 10),
				Mathf.MoveTowards(transform.localScale.z, 1, Time.deltaTime * 10));
		}
	}
}