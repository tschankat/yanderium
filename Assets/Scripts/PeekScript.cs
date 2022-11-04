using UnityEngine;

public class PeekScript : MonoBehaviour
{
	public InfoChanWindowScript InfoChanWindow;
	public PromptBarScript PromptBar;
	public SubtitleScript Subtitle;
	public JukeboxScript Jukebox;
	public PromptScript Prompt;

	public GameObject PeekCamera;

	public bool Spoke = false;

	public float Timer = 0.0f;

	void Start()
	{
		this.Prompt.Door = true;
	}

	void Update()
	{
		float DistanceToPlayer = Vector3.Distance(this.transform.position, this.Prompt.Yandere.transform.position);

		if (DistanceToPlayer < 2)
		{
			this.Prompt.Yandere.StudentManager.TutorialWindow.ShowInfoMessage = true;
		}

		if (this.InfoChanWindow.Drop)
		{
			this.Prompt.Circle[0].fillAmount = 1.0f;
		}

		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Prompt.Circle[0].fillAmount = 1.0f;

			if (!this.Prompt.Yandere.Chased && this.Prompt.Yandere.Chasers == 0)
			{
				this.Prompt.Yandere.CanMove = false;
				this.PeekCamera.SetActive(true);

				this.PromptBar.ClearButtons();
				this.PromptBar.Label[1].text = "Stop";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;
			}
		}

		if (this.PeekCamera.activeInHierarchy)
		{
			this.Timer += Time.deltaTime;

			if (this.Timer > 5.0f)
			{
				if (!this.Spoke)
				{
					this.Subtitle.UpdateLabel(SubtitleType.InfoNotice, 0, 6.50f);
					this.Spoke = true;
					this.GetComponent<AudioSource>().Play();
				}
			}

			if (Input.GetButtonDown(InputNames.Xbox_B) || this.Prompt.Yandere.Noticed || this.Prompt.Yandere.Sprayed)
			{
				if (!this.Prompt.Yandere.Noticed && !this.Prompt.Yandere.Sprayed)
				{
					this.Prompt.Yandere.CanMove = true;
				}

				this.PeekCamera.SetActive(false);

				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;

				this.Timer = 0.0f;
			}
		}
	}
}
