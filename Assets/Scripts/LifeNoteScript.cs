using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeNoteScript : MonoBehaviour
{
	public UITexture Darkness;
	public UITexture TextWindow;
	public UITexture FinalDarkness;

	public Transform BackgroundArt;

	public TypewriterEffect Typewriter;
	public GameObject Controls;

	public AudioSource MyAudio;

	public AudioClip[] Voices;
	public string[] Lines;
	public int[] Alphas;
	public bool[] Reds;

	public UILabel Label;

	public float Timer = 0.0f;

	public int Frame = 0;
	public int ID = 0;

	public float AutoTimer;
	public float Alpha;

	public string Text;

	public AudioClip[] SFX;

	public bool Spoke;
	public bool Auto;

	public AudioSource SFXAudioSource;
	public AudioSource Jukebox;

	void Start()
	{
		Application.targetFrameRate = 60;

		this.Label.text = this.Lines[this.ID];

		this.Controls.SetActive(false);
		this.Label.gameObject.SetActive(false);
		this.Darkness.color = new Color(0, 0, 0, 1.0f);

		this.BackgroundArt.localPosition = new Vector3(0, -540, 0);
		this.BackgroundArt.localScale = new Vector3(2.5f, 2.5f, 1);

		this.TextWindow.color = new Color(1, 1, 1, 0);
	}

	void Update()
	{
		if (this.Controls.activeInHierarchy)
		{
			if (this.Typewriter.mCurrentOffset == 1)
			{
				if (this.Reds[this.ID])
				{
					this.Label.color = new Color(1, 0, 0, 1);
				}
				else
				{
					this.Label.color = new Color(1, 1, 1, 1);
				}
			}

			if (Input.GetButtonDown(InputNames.Xbox_A) || this.AutoTimer > .5f)
			{
				if (this.ID < (this.Lines.Length - 1))
				{
					if (this.Typewriter.mCurrentOffset < this.Typewriter.mFullText.Length)
					{
						this.Typewriter.Finish();
					}
					else
					{
						this.ID++;

						this.Alpha = this.Alphas[this.ID];
						this.Darkness.color = new Color(0, 0, 0, Alpha);

						this.Typewriter.ResetToBeginning();
						this.Typewriter.mFullText = this.Lines[this.ID];

						this.Label.text = "";
						this.Spoke = false;
						this.Frame = 0;

						if (this.Alphas[this.ID] == 1)
						{
							this.Jukebox.Stop();
						}
						else
						{
							if (!this.Jukebox.isPlaying)
							{
								this.Jukebox.Play();
							}
						}

						if (this.ID == 17)
						{
							SFXAudioSource.clip = SFX[1];
							SFXAudioSource.Play();
						}

						if (this.ID == 18)
						{
							SFXAudioSource.clip = SFX[2];
							SFXAudioSource.Play();
						}

						if (this.ID > 25)
						{
							this.Typewriter.charsPerSecond = 15;
						}

						this.AutoTimer = 0;
					}
				}
				else
				{
					if (!this.FinalDarkness.enabled)
					{
						this.FinalDarkness.enabled = true;
						this.Alpha = 0;
					}
				}
			}

			if (!this.Spoke)
			{
				if (!this.SFXAudioSource.isPlaying)
				{
					this.MyAudio.clip = Voices[this.ID];
					this.MyAudio.Play();

					this.Spoke = true;
				}
			}

			if (this.Auto)
			{
				if (this.Typewriter.mCurrentOffset == this.Typewriter.mFullText.Length &&
					!this.SFXAudioSource.isPlaying &&
					!this.MyAudio.isPlaying)
				{
					this.AutoTimer += Time.deltaTime;
				}
			}

			if (this.FinalDarkness.enabled)
			{
				this.Alpha = Mathf.MoveTowards(this.Alpha, 1, Time.deltaTime * .2f);

				this.FinalDarkness.color = new Color(0, 0, 0, Alpha);

				if (this.Alpha == 1)
				{
					SceneManager.LoadScene(SceneNames.HomeScene);
				}
			}
		}

		if (this.TextWindow.color.a < 1)
		{
			if (Input.GetButtonDown(InputNames.Xbox_A))
			{
				this.Darkness.color = new Color(0, 0, 0, 0);

				this.BackgroundArt.localPosition = new Vector3(0, 0, 0);
				this.BackgroundArt.localScale = new Vector3(1, 1, 1);

				this.TextWindow.color = new Color(1, 1, 1, 1);
				this.Label.color = new Color(1, 1, 1, 0);

				this.Label.gameObject.SetActive(true);
				this.Controls.SetActive(true);

				this.Timer = 0;
			}

			this.Timer += Time.deltaTime;

			if (this.Timer > 6.0f)
			{
				this.Alpha = Mathf.MoveTowards(this.Alpha, 1, Time.deltaTime);

				this.TextWindow.color = new Color(1, 1, 1, Alpha);

				if (this.TextWindow.color.a == 1)
				{
					if (!this.Typewriter.mActive)
					{
						this.Label.color = new Color(1, 1, 1, 0);
						
						this.Label.gameObject.SetActive(true);
						this.Controls.SetActive(true);
						this.Timer = 0;
					}
				}
			}
			else if (this.Timer > 2.0f)
			{
				this.BackgroundArt.localScale = Vector3.Lerp(
					this.BackgroundArt.localScale,
					new Vector3(1, 1, 1),
					Time.deltaTime * (Timer - 2));

				this.BackgroundArt.localPosition = Vector3.Lerp(
					this.BackgroundArt.localPosition,
					new Vector3(0, 0, 0),
					Time.deltaTime * (Timer - 2));
			}
			else if (this.Timer > 0.0f)
			{
				this.Darkness.color = new Color(0, 0, 0, Mathf.MoveTowards(this.Darkness.color.a, 0.0f, Time.deltaTime));
			}
		}
	}
}