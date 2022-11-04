using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentenceScript : MonoBehaviour
{
	public UILabel Sentence;

	public string[] Words;

	public int ID;

	void Update ()
	{
		if (Input.GetButtonDown("A"))
		{
			Sentence.text = Words[ID];
			ID++;
		}
	}
}