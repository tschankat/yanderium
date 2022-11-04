using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeTextScript : MonoBehaviour
{
	public UILabel Label;

	public string[] String;

	public int ID;

	void Start()
	{
		Label.text = String[ID];
	}

	void Update ()
	{
		if (Input.GetKeyDown("space"))
		{
			ID++;
			Label.text = String[ID];
		}
	}
}
