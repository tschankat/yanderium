using UnityEngine;

public class MusicCreditScript : MonoBehaviour
{
	public UILabel SongLabel;
	public UILabel BandLabel;
	public UIPanel Panel;
	public bool Slide = false;
	public float Timer = 0.0f;

	void Start()
	{
		this.transform.localPosition = new Vector3(
			400.0f,
			this.transform.localPosition.y,
			this.transform.localPosition.z);

		this.Panel.enabled = false;
	}

	void Update()
	{
		if (this.Slide)
		{
			this.Timer += Time.deltaTime;

			if (this.Timer < 5.0f)
			{
				this.transform.localPosition = new Vector3(
					Mathf.Lerp(this.transform.localPosition.x, 0.0f, Time.deltaTime * 10.0f),
					this.transform.localPosition.y,
					this.transform.localPosition.z);
			}
			else
			{
				this.transform.localPosition = new Vector3(
					this.transform.localPosition.x + Time.deltaTime,
					this.transform.localPosition.y,
					this.transform.localPosition.z);

				this.transform.localPosition = new Vector3(
					this.transform.localPosition.x + (Mathf.Abs(this.transform.localPosition.x * 0.010f) * (Time.deltaTime * 1000.0f)),
					this.transform.localPosition.y,
					this.transform.localPosition.z);

				if (this.transform.localPosition.x > 400.0f)
				{
					this.transform.localPosition = new Vector3(
						400.0f,
						this.transform.localPosition.y,
						this.transform.localPosition.z);

					this.Panel.enabled = false;
					this.Slide = false;
					this.Timer = 0.0f;
				}
			}
		}
	}
}
