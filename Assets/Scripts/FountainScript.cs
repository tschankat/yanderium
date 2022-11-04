using UnityEngine;

public class FountainScript : MonoBehaviour
{
	public ParticleSystem Splashes;
	public UILabel EventSubtitle;
	public Collider[] Colliders;
	public bool Drowning = false;
	public AudioSource SpraySFX;
	public AudioSource DropsSFX;
	public float StartTimer = 0.0f;
	public float Timer = 0.0f;

	void Start()
	{
		this.SpraySFX.volume = 0.10f;
		this.DropsSFX.volume = 0.10f;
	}

	void Update()
	{
		if (this.StartTimer < 1.0f)
		{
			this.StartTimer += Time.deltaTime;

			if (this.StartTimer > 1)
			{
				this.SpraySFX.gameObject.SetActive(true);
				this.DropsSFX.gameObject.SetActive(true);
			}
		}

		if (this.Drowning)
		{
			if (this.Timer == 0.0f)
			{
				if (this.EventSubtitle.transform.localScale.x < 1.0f)
				{
					this.EventSubtitle.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
					this.EventSubtitle.text = "Hey, what are you -";
					this.GetComponent<AudioSource>().Play();
				}
			}

			this.Timer += Time.deltaTime;

			if (this.Timer > 3.0f)
			{
				if (this.EventSubtitle.transform.localScale.x > 0.0f)
				{
					this.EventSubtitle.transform.localScale = Vector3.zero;
					this.EventSubtitle.text = string.Empty;
					this.Splashes.Play();
				}
			}

			if (this.Timer > 9.0f)
			{
				this.Drowning = false;
				this.Splashes.Stop();
				this.Timer = 0.0f;
			}
		}
	}
}
