using UnityEngine;

public class HomeVideoCameraScript : MonoBehaviour
{
	public HomePrisonerChanScript HomePrisonerChan;
	public HomeDarknessScript HomeDarkness;
	public HomePrisonerScript HomePrisoner;
	public HomeYandereScript HomeYandere;
	public HomeCameraScript HomeCamera;
	public PromptScript Prompt;
	public UILabel Subtitle;
	public bool AudioPlayed = false;
	public bool TextSet = false;
	public float Timer = 0.0f;

	void Update()
	{
		if (!this.TextSet)
		{
			if (!HomeGlobals.Night)
			{
				this.Prompt.Label[0].text = "     " + "Only Available At Night";
			}
		}

		if (!HomeGlobals.Night)
		{
			this.Prompt.Circle[0].fillAmount = 1.0f;
		}

		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.HomeCamera.Destination = this.HomeCamera.Destinations[11];
			this.HomeCamera.Target = this.HomeCamera.Targets[11];
			this.HomeCamera.ID = 11;

			this.HomePrisonerChan.LookAhead = true;

			this.HomeYandere.CanMove = false;

			this.HomeYandere.gameObject.SetActive(false);
		}

		if (this.HomeCamera.ID == 11)
		{
			if (!this.HomePrisoner.Bantering)
			{
				this.Timer += Time.deltaTime;

				AudioSource audioSource = this.GetComponent<AudioSource>();

				if (this.Timer > 2.0f)
				{
					if (!this.AudioPlayed)
					{
						this.Subtitle.text = "...daddy...please...help...I'm scared...I don't wanna die...";
						this.AudioPlayed = true;
						audioSource.Play();
					}
				}

				if (this.Timer > (2.0f + audioSource.clip.length))
				{
					this.Subtitle.text = string.Empty;
				}

				if (this.Timer > (3.0f + audioSource.clip.length))
				{
					this.HomeDarkness.FadeSlow = true;
					this.HomeDarkness.FadeOut = true;
				}
			}
		}
	}
}
