using UnityEngine;

public class HomeTriggerScript : MonoBehaviour
{
	public HomeCameraScript HomeCamera;
	public UILabel Label;
	public bool FadeIn = false;
	public int ID = 0;

	void Start()
	{
		this.Label.color = new Color(
			this.Label.color.r,
			this.Label.color.g,
			this.Label.color.b,
			0.0f);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			this.HomeCamera.ID = ID;
			this.FadeIn = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			this.HomeCamera.ID = 0;
			this.FadeIn = false;
		}
	}

	void Update()
	{
		// [af] Converted if/else statement to assignment with ternary expression.
		this.Label.color = new Color(
			this.Label.color.r,
			this.Label.color.g,
			this.Label.color.b,
			Mathf.MoveTowards(this.Label.color.a, this.FadeIn ? 1.0f : 0.0f, Time.deltaTime * 10.0f));
	}

	public void Disable()
	{
		this.Label.color = new Color(
			this.Label.color.r,
			this.Label.color.g,
			this.Label.color.b,
			0.0f);

		// [af] Added "gameObject" for C# compatibility.
		this.gameObject.SetActive(false);
	}
}
