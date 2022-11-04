using UnityEngine;

public class SplashCameraScript : MonoBehaviour
{
	public Camera MyCamera;
	public bool Show = false;
	public float Timer = 0.0f;

	void Start()
	{
		this.MyCamera.enabled = false;
		this.MyCamera.rect = new Rect(0.0f, 0.219f, 0.0f, 0.0f);
	}

	void Update()
	{
		if (this.Show)
		{
			this.MyCamera.rect = new Rect(
				this.MyCamera.rect.x,
				this.MyCamera.rect.y,
				Mathf.Lerp(this.MyCamera.rect.width, 0.40f, Time.deltaTime * 10.0f),
				Mathf.Lerp(this.MyCamera.rect.height, 0.71104f, Time.deltaTime * 10.0f));

			this.Timer += Time.deltaTime;

			if (this.Timer > 15.0f)
			{
				this.Show = false;
				this.Timer = 0.0f;
			}
		}
		else
		{
			this.MyCamera.rect = new Rect(
				this.MyCamera.rect.x,
				this.MyCamera.rect.y,
				Mathf.Lerp(this.MyCamera.rect.width, 0.0f, Time.deltaTime * 10.0f),
				Mathf.Lerp(this.MyCamera.rect.height, 0.0f, Time.deltaTime * 10.0f));

			if (this.MyCamera.enabled)
			{
				if (this.MyCamera.rect.width < 0.10f)
				{
					this.MyCamera.enabled = false;
				}
			}
		}
	}
}
