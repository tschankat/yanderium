using UnityEngine;

public class WitnessCameraScript : MonoBehaviour
{
	public YandereScript Yandere;

	public Transform WitnessPOV;

	public float WitnessTimer = 0.0f;

	public Camera MyCamera;

	public bool Show = false;

	void Start()
	{
		MyCamera.enabled = false;
		MyCamera.rect = new Rect(0.0f, 0.0f, 0.0f, 0.0f);
	}

	void Update()
	{
		if (this.Show)
		{
			this.MyCamera.rect = new Rect(
				this.MyCamera.rect.x,
				this.MyCamera.rect.y,
				Mathf.Lerp(this.MyCamera.rect.width, 0.25f, Time.deltaTime * 10.0f),
				Mathf.Lerp(this.MyCamera.rect.height, 1.0f / 2.25f, Time.deltaTime * 10.0f));

			this.transform.localPosition = new Vector3(
				this.transform.localPosition.x,
				this.transform.localPosition.y,
				this.transform.localPosition.z + (Time.deltaTime * 0.090f));

			this.WitnessTimer += Time.deltaTime;

			if (this.WitnessTimer > 5.0f)
			{
				this.WitnessTimer = 0.0f;
				this.Show = false;
			}

			if (this.Yandere.Struggling)
			{
				this.WitnessTimer = 0.0f;
				this.Show = false;
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
					this.transform.parent = null;
				}
			}
		}
	}
}
