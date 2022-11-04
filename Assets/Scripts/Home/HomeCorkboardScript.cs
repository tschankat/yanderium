using UnityEngine;

public class HomeCorkboardScript : MonoBehaviour
{
	public InputManagerScript InputManager;
	public PhotoGalleryScript PhotoGallery;
	public HomeYandereScript HomeYandere;
	public HomeCameraScript HomeCamera;
	public HomeWindowScript HomeWindow;
	public bool Loaded = false;

	void Update()
	{
		if (!this.HomeYandere.CanMove)
		{
			if (!this.Loaded)
			{
				this.PhotoGallery.LoadingScreen.SetActive(false);
				this.PhotoGallery.UpdateButtonPrompts();
				this.PhotoGallery.enabled = true;
				this.PhotoGallery.gameObject.SetActive(true);

				this.Loaded = true;
			}

			if (!this.PhotoGallery.Adjusting &&
				!this.PhotoGallery.Viewing &&
				!this.PhotoGallery.LoadingScreen.activeInHierarchy &&
				Input.GetButtonDown(InputNames.Xbox_B))
			{
				this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
				this.HomeCamera.Target = this.HomeCamera.Targets[0];
				this.HomeCamera.CorkboardLabel.SetActive(true);
				this.PhotoGallery.PromptBar.Show = false;
				this.PhotoGallery.enabled = false;
				this.HomeYandere.CanMove = true;
				this.HomeYandere.gameObject.SetActive(true);

				this.HomeWindow.Show = false;
				this.enabled = false;
				this.Loaded = false;

				this.PhotoGallery.SaveAllPhotographs();
				this.PhotoGallery.SaveAllStrings();
			}
		}
	}
}