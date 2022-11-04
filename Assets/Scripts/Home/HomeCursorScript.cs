using UnityEngine;

public class HomeCursorScript : MonoBehaviour
{
	public PhotoGalleryScript PhotoGallery;
	public GameObject Photograph;
	public Transform Highlight;

	public GameObject Tack;
	public Transform CircleHighlight;

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == this.Photograph)
		{
			this.PhotographNull();
		}

		if (other.gameObject == this.Tack)
		{
			this.CircleHighlight.position = new Vector3(
				this.CircleHighlight.position.x, 100.0f, this.Highlight.position.z);

			this.Tack = null;

			this.PhotoGallery.UpdateButtonPrompts();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 16)
		{
			if (this.Tack == null)
			{
				this.Photograph = other.gameObject;

				this.Highlight.localEulerAngles = this.Photograph.transform.localEulerAngles;
				this.Highlight.localPosition = this.Photograph.transform.localPosition;
				this.Highlight.localScale = new Vector3(
					this.Photograph.transform.localScale.x * 1.12f,
					this.Photograph.transform.localScale.y * 1.2f,
					1);

				this.PhotoGallery.UpdateButtonPrompts();
			}
		}
		else
		{
			if (other.gameObject.name != "SouthWall")
			{
				this.Tack = other.gameObject;
				this.CircleHighlight.position = this.Tack.transform.position;

				this.PhotoGallery.UpdateButtonPrompts();
				this.PhotographNull();
			}
		}
	}

	void PhotographNull()
	{
		this.Highlight.position = new Vector3(
			this.Highlight.position.x, 100.0f, this.Highlight.position.z);

		this.Photograph = null;

		this.PhotoGallery.UpdateButtonPrompts();
	}
}
