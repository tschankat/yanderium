using UnityEngine;

public class FramerateScript : MonoBehaviour
{
	public float updateInterval = 0.50f;

	float accum = 0.0f; // FPS accumulated over the interval.
	int frames = 0; // Frames drawn over the interval.
	float timeleft; // Left time for current interval.

	public float FPS = 0.0f;

	public UILabel FPSLabel;

	void Start()
	{
		this.timeleft = this.updateInterval;
	}

	void Update()
	{
		this.timeleft -= Time.deltaTime;
		this.accum += Time.timeScale / Time.deltaTime;
		this.frames++;

		// Interval ended - update GUI text and start new interval.
		if (this.timeleft <= 0.0f)
		{
			this.FPS = this.accum / this.frames;

			// [af] Sanitize FPS value (to avoid displaying infinity and NaN).
			int displayedFPS = Mathf.Clamp((int)this.FPS, 0, Application.targetFrameRate);
			
			if (displayedFPS > 0)
			{
				//this.fpsText.text = "FPS: " + displayedFPS.ToString();
				this.FPSLabel.text = "FPS: " + displayedFPS.ToString();
			}
			
			this.timeleft = this.updateInterval;
			this.accum = 0.0f;
			this.frames = 0;
		}
	}
}
