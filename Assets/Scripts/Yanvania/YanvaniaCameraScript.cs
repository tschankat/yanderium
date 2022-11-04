using UnityEngine;

public class YanvaniaCameraScript : MonoBehaviour
{
	public YanvaniaYanmontScript Yanmont;

	public GameObject Jukebox;

	public bool Cutscene = false;
	public bool StopMusic = true;

	public float TargetZoom = 0.0f;
	public float Zoom = 0.0f;

	void Start()
	{
		this.transform.position = this.Yanmont.transform.position +
			new Vector3(0.0f, 1.50f, -5.85f);
	}

	void FixedUpdate()
	{
		this.TargetZoom += Input.GetAxis("Mouse ScrollWheel");

		if (this.TargetZoom < 0.0f)
		{
			this.TargetZoom = 0.0f;
		}

		if (this.TargetZoom > 3.85f)
		{
			this.TargetZoom = 3.85f;
		}

		this.Zoom = Mathf.Lerp(this.Zoom, this.TargetZoom, Time.deltaTime);

		if (!this.Cutscene)
		{
			this.transform.position = this.Yanmont.transform.position +
				new Vector3(0.0f, 1.50f, -5.85f + this.Zoom);

			if (this.transform.position.x > 47.90f)
			{
				this.transform.position = new Vector3(
					47.90f,
					this.transform.position.y,
					this.transform.position.z);
			}

			// [af] Commented out in JS code.
			//if (transform.position.x > 88.75)
			//{
			//	transform.position.x = 88.75;
			//}

			//if (transform.position.x > .95)
			//{
			//	transform.position.x = .95;
			//}
		}
		else
		{
			if (this.StopMusic)
			{
				AudioSource jukeboxSource = this.Jukebox.GetComponent<AudioSource>();

				// [af] Replaced if/else statement with assignment and ternary expression.
				jukeboxSource.volume -= Time.deltaTime *
					((this.Yanmont.Health > 0.0f) ? 0.20f : 0.025f);

				if (jukeboxSource.volume <= 0.0f)
				{
					this.StopMusic = false;
				}
			}

			this.transform.position = new Vector3(
				Mathf.MoveTowards(this.transform.position.x, -34.675f, Time.deltaTime * this.Yanmont.walkSpeed),
				8.0f,
				-5.85f + this.Zoom);
		}
	}
}
