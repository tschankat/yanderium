using UnityEngine;

public class FadeInOut : MonoBehaviour
{
	public Texture2D fadeOutTexture;
	public float fadeSpeed = 0.3f;

	public int drawDepth = -1000;

	private float alpha = 1.0f;
	private float fadeDir = -1.0f;

	void OnGUI()
	{
		this.alpha += this.fadeDir * this.fadeSpeed * Time.deltaTime;
		this.alpha = Mathf.Clamp01(this.alpha);

		GUI.color = new Color(
			GUI.color.r,
			GUI.color.g,
			GUI.color.b,
			this.alpha);

		GUI.depth = this.drawDepth;

		GUI.DrawTexture(new Rect(-10.0f, -10.0f, Screen.width + 10.0f, Screen.height + 10.0f), this.fadeOutTexture);
	}

	void fadeIn()
	{
		this.fadeDir = 1.0f;
	}

	void fadeOut()
	{
		this.fadeDir = -1.0f;
	}

	void Start()
	{
		this.alpha = 1.0f;
	}
}
