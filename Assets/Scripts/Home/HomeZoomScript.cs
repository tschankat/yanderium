using UnityEngine;

public class HomeZoomScript : MonoBehaviour
{
	public Transform YandereDestination;
	public bool Zoom = false;

	void Update()
	{
		AudioSource audioSource = this.GetComponent<AudioSource>();

		if (Input.GetKeyDown(KeyCode.Z))
		{
			if (!this.Zoom)
			{
				this.Zoom = true;
				audioSource.Play();
			}
			else
			{
				this.Zoom = false;
			}
		}

		if (this.Zoom)
		{
			this.transform.localPosition = new Vector3(
				this.transform.localPosition.x,
				Mathf.MoveTowards(this.transform.localPosition.y, 1.50f, Time.deltaTime * (1.0f / 30.0f)),
				this.transform.localPosition.z);

			this.YandereDestination.localPosition = Vector3.MoveTowards(
				this.YandereDestination.localPosition,
				new Vector3(-1.50f, 1.50f, 1.0f), Time.deltaTime * (1.0f / 30.0f));

			audioSource.volume += Time.deltaTime * 0.010f;
		}
		else
		{
			this.transform.localPosition = new Vector3(
				this.transform.localPosition.x,
				Mathf.MoveTowards(this.transform.localPosition.y, 1.0f, Time.deltaTime * 10.0f),
				this.transform.localPosition.z);

			this.YandereDestination.localPosition = Vector3.MoveTowards(
				this.YandereDestination.localPosition,
				new Vector3(-2.271312f, 2.0f, 3.50f), Time.deltaTime * 10.0f);

			audioSource.volume = 0.0f;
		}
	}
}
