using UnityEngine;
using UnityEngine.SceneManagement;

public class WarningScript : MonoBehaviour
{
	public float[] Triggers;
	public string[] Text;
	public UILabel WarningLabel;
	public UISprite Darkness;
	public UILabel Label;
	public bool FadeOut = false;
	public float Timer = 0.0f;
	public int ID = 0;

	void Start()
	{
		// [af] Added "gameObject" for C# compatibility.
		this.WarningLabel.gameObject.SetActive(false);

		this.Label.text = string.Empty;

		this.Darkness.color = new Color(
			this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 1.0f);
	}

	void Update()
	{
		AudioSource audioSource = this.GetComponent<AudioSource>();

		if (!this.FadeOut)
		{
			this.Darkness.color = new Color(
				this.Darkness.color.r,
				this.Darkness.color.g,
				this.Darkness.color.b,
				Mathf.MoveTowards(this.Darkness.color.a, 0.0f, Time.deltaTime));

			if (this.Darkness.color.a == 0.0f)
			{
				if (this.Timer == 0.0f)
				{
					// [af] Added "gameObject" for C# compatibility.
					this.WarningLabel.gameObject.SetActive(true);

					audioSource.Play();
				}

				this.Timer += Time.deltaTime;

				if (this.ID < this.Triggers.Length)
				{
					if (this.Timer > this.Triggers[this.ID])
					{
						this.Label.text = this.Text[this.ID];
						this.ID++;
					}
				}
			}
		}
		else
		{
			audioSource.volume = Mathf.MoveTowards(audioSource.volume, 0.0f, Time.deltaTime);

			this.Darkness.color = new Color(
				this.Darkness.color.r,
				this.Darkness.color.g,
				this.Darkness.color.b,
				Mathf.MoveTowards(this.Darkness.color.a, 1.0f, Time.deltaTime));

			if (this.Darkness.color.a == 1.0f)
			{
				SceneManager.LoadScene(SceneNames.SponsorScene);
			}
		}

		if (Input.anyKey)
		{
			this.FadeOut = true;
		}
	}
}
