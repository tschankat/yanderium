using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DramaticPanUpScript : MonoBehaviour
{
	public bool Pan;

	public float Height;

	public float Power;

	void Update ()
	{
		if (Input.GetKeyDown("space"))
		{
			Pan = true;
		}

		if (Pan)
		{
			Power += Time.deltaTime * .5f;

			Height = Mathf.Lerp(Height, 1.4f, Power * Time.deltaTime);

			transform.localPosition = new Vector3(0, Height, 1);
		}
	}
}