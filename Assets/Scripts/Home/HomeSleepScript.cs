using UnityEngine;

public class HomeSleepScript : MonoBehaviour
{
	public HomeDarknessScript HomeDarkness;
	public HomeYandereScript HomeYandere;
	public HomeCameraScript HomeCamera;
	public HomeWindowScript HomeWindow;

	void Update()
	{
		// [af] Combined if statements to reduce nesting.
		if (!this.HomeYandere.CanMove && !this.HomeDarkness.FadeOut)
		{
			if (Input.GetButtonDown(InputNames.Xbox_A))
			{
				this.HomeDarkness.Sprite.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
				this.HomeDarkness.Cyberstalking = true;
				this.HomeDarkness.FadeOut = true;
				this.HomeWindow.Show = false;
				this.enabled = false;
			}

			if (Input.GetButtonDown(InputNames.Xbox_B))
			{
				this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
				this.HomeCamera.Target = this.HomeCamera.Targets[0];
				this.HomeYandere.CanMove = true;
				this.HomeWindow.Show = false;
				this.enabled = false;
			}
		}
	}
}