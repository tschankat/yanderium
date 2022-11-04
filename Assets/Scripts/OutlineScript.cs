using HighlightingSystem;
using UnityEngine;

public class OutlineScript : MonoBehaviour
{
	public YandereScript Yandere;

	public Highlighter h;

	public Color color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

	public void Awake()
	{
		//this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();

		this.h = this.GetComponent<Highlighter>();

		if (this.h == null)
		{
			this.h = this.gameObject.AddComponent<Highlighter>();
		}
	}

	void Update()
	{
		/*
		if (this.Yandere.YandereVision)
		{
			this.h.enabled = true;
		*/
			this.h.ConstantOnImmediate(this.color);
		/*
		}
		else
		{
			if (this.h.enabled)
			{
				this.h.enabled = false;
			}
		}
		*/
	}
}