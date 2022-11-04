using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleExtrasScript : MonoBehaviour
{
	public bool Show = false;

	void Start()
	{
		this.transform.localPosition = new Vector3(
			1050.0f,
			this.transform.localPosition.y,
			this.transform.localPosition.z);
	}

	void Update()
	{
		if (!this.Show)
		{
			this.transform.localPosition = new Vector3(
				Mathf.Lerp(this.transform.localPosition.x, 1050.0f, Time.deltaTime * 10.0f),
				this.transform.localPosition.y,
				this.transform.localPosition.z);
		}
		else
		{
			this.transform.localPosition = new Vector3(
				Mathf.Lerp(this.transform.localPosition.x, 0.0f, Time.deltaTime * 10.0f),
				this.transform.localPosition.y,
				this.transform.localPosition.z);
		}
	}
}