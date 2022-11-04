using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoItScript : MonoBehaviour
{
	public UILabel MyLabel;

	public bool Fade;

	void Start ()
	{
		MyLabel.fontSize = Random.Range(50, 100);
	}

	void Update ()
	{
		transform.localScale += new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);

		if (!Fade)
		{
			MyLabel.alpha += Time.deltaTime;

			if (MyLabel.alpha >= 1)
			{
				Fade = true;
			}
		}
		else
		{
			MyLabel.alpha -= Time.deltaTime;

			if (MyLabel.alpha <= 0)
			{
				Destroy(gameObject);
			}
		}
	}
}