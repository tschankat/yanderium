using UnityEngine;
using UnityEngine.SceneManagement;

public class YanvaniaTitleScript : MonoBehaviour
{
	public InputManagerScript InputManager;

	public GameObject ButtonEffect;
	public GameObject ErrorWindow;
	public GameObject SkipButton;

	public Transform Highlight;
	public Transform Prologue;

	public UIPanel Controls;
	public UIPanel Credits;
	public UIPanel Buttons;

	public UISprite Darkness;

	public UITexture Midori;
	public UITexture Logo;

	public AudioClip SelectSound;
	public AudioClip ExitSound;
	public AudioClip BGM;

	public Transform[] BackButtons;

	public Texture SadMidori;

	public bool FadeButtons = false;
	public bool ErrorLeave = false;
	public bool FadeOut = false;

	public float ScrollSpeed = 0.0f;

	public int Selected = 1;

	void Start()
	{
		this.Midori.transform.localPosition = new Vector3(1540.0f, 0.0f, 0.0f);
		this.Midori.transform.localEulerAngles = Vector3.zero;
		this.Midori.gameObject.SetActive(false);

		if (YanvaniaGlobals.DraculaDefeated)
		{
			//Pippi's Task.
			TaskGlobals.SetTaskStatus(38, 2);
			this.SkipButton.SetActive(true);

			// [af] Added "gameObject" for C# compatibility.
			this.Logo.gameObject.SetActive(false);
		}
		else
		{
			this.SkipButton.SetActive(false);
		}

		this.Prologue.gameObject.SetActive(false);

		this.Prologue.localPosition = new Vector3(
			this.Prologue.localPosition.x,
			-2665.0f,
			this.Prologue.localPosition.z);

		this.Darkness.color = new Color(
			this.Darkness.color.r,
			this.Darkness.color.g,
			this.Darkness.color.b,
			1.0f);

		this.Buttons.alpha = 0.0f;

		this.Logo.color = new Color(
			this.Logo.color.r,
			this.Logo.color.g,
			this.Logo.color.b,
			0.0f);
	}

	void Update()
	{
		// [af] Added "gameObject" for C# compatibility.
		if (!this.Logo.gameObject.activeInHierarchy)
		{
			if (Input.GetKeyDown(KeyCode.M))
			{
				YanvaniaGlobals.DraculaDefeated = true;
				YanvaniaGlobals.MidoriEasterEgg = true;
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
		}

		if (Input.GetKeyDown(KeyCode.End))
		{
			YanvaniaGlobals.DraculaDefeated = true;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		if (Input.GetKeyDown(KeyCode.BackQuote))
		{
			YanvaniaGlobals.DraculaDefeated = false;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		AudioSource audioSource = this.GetComponent<AudioSource>();

		if (!this.FadeOut)
		{
			if (this.Darkness.color.a > 0.0f)
			{
				if (Input.GetButtonDown(InputNames.Xbox_A))
				{
					this.Skip();
				}

				if (!this.ErrorWindow.activeInHierarchy)
				{
					this.Darkness.color = new Color(
						this.Darkness.color.r,
						this.Darkness.color.g,
						this.Darkness.color.b,
						this.Darkness.color.a - Time.deltaTime);
				}
			}
			else
			{
				if (this.Darkness.color.a <= 0.0f)
				{
					if (!YanvaniaGlobals.MidoriEasterEgg)
					{
						if (YanvaniaGlobals.DraculaDefeated)
						{
							if (!this.Prologue.gameObject.activeInHierarchy)
							{
								// [af] Added "gameObject" for C# compatibility.
								this.Prologue.gameObject.SetActive(true);

								audioSource.volume = 0.50f;
								audioSource.loop = true;
								audioSource.clip = this.BGM;
								audioSource.Play();
							}

							if (Input.GetButtonDown(InputNames.Xbox_B))
							{
								this.Prologue.localPosition = new Vector3(
									this.Prologue.localPosition.x,
									2501.0f,
									this.Prologue.localPosition.z);

								this.Prologue.GetComponent<AudioSource>().Stop();
							}

							if (this.Prologue.localPosition.y > 2500.0f)
							{
								if (audioSource.isPlaying)
								{
									this.Midori.mainTexture = this.SadMidori;
									Time.timeScale = 1.0f;

									this.Midori.gameObject.GetComponent<AudioSource>().Stop();
									audioSource.Stop();
								}

								if (!this.ErrorLeave)
								{
									this.ErrorWindow.SetActive(true);
									this.ErrorWindow.transform.localScale = Vector3.Lerp(
										this.ErrorWindow.transform.localScale,
										new Vector3(1.0f, 1.0f, 1.0f),
										Time.deltaTime * 10.0f);

									if (this.ErrorWindow.transform.localScale.x > 0.90f)
									{
										if (Input.anyKeyDown)
										{
											AudioSource errorAudioSource =
												this.ErrorWindow.GetComponent<AudioSource>();
											errorAudioSource.clip = this.ExitSound;
											errorAudioSource.Play();
											this.ErrorLeave = true;
										}
									}
								}
								else
								{
									this.FadeOut = true;
								}
							}
							else
							{
								this.Prologue.localPosition = new Vector3(
									this.Prologue.localPosition.x,
									this.Prologue.localPosition.y + (Time.deltaTime * this.ScrollSpeed),
									this.Prologue.localPosition.z);

								if (Input.GetKeyDown(KeyCode.Equals))
								{
									Time.timeScale = 100.0f;
								}

								if (Input.GetKeyDown(KeyCode.Minus))
								{
									Time.timeScale = 1.0f;
								}
							}
						}
						else
						{
							if (!audioSource.isPlaying)
							{
								if (this.Logo.color.a == 0.0f)
								{
									audioSource.Play();
								}
								else
								{
									audioSource.loop = true;
									audioSource.clip = this.BGM;
									audioSource.Play();
								}
							}
							else
							{
								if (audioSource.clip != this.BGM)
								{
									this.Logo.color = new Color(
										this.Logo.color.r,
										this.Logo.color.g,
										this.Logo.color.b,
										this.Logo.color.a + Time.deltaTime);

									if (Input.GetButtonDown(InputNames.Xbox_A))
									{
										this.Skip();
									}
								}
								else
								{
									if (!this.FadeButtons)
									{
										this.Controls.alpha = Mathf.MoveTowards(
											this.Controls.alpha, 0.0f, Time.deltaTime);
										this.Credits.alpha = Mathf.MoveTowards(
											this.Credits.alpha, 0.0f, Time.deltaTime);

										if ((this.Controls.alpha == 0.0f) && (this.Credits.alpha == 0.0f))
										{
											this.Highlight.localPosition = new Vector3(
												this.Highlight.localPosition.x,
												-100.0f - (100.0f * this.Selected),
												this.Highlight.localPosition.z);

											this.Buttons.alpha += Time.deltaTime;

											if (this.Buttons.alpha >= 1.0f)
											{
												if (Input.GetButtonDown(InputNames.Xbox_A))
												{
													Instantiate(this.ButtonEffect,
														this.Highlight.position, Quaternion.identity);

													if ((this.Selected == 1) || (this.Selected == 4))
													{
														this.FadeOut = true;
													}

													if ((this.Selected == 2) || (this.Selected == 3))
													{
														this.FadeButtons = true;
													}
												}

												AudioSource highlightAudioSource =
													this.Highlight.gameObject.GetComponent<AudioSource>();

												if (this.InputManager.TappedUp)
												{
													highlightAudioSource.Play();

													this.Selected--;

													if (this.Selected < 1)
													{
														this.Selected = 4;
													}
												}

												if (this.InputManager.TappedDown)
												{
													highlightAudioSource.Play();

													this.Selected++;

													if (this.Selected > 4)
													{
														this.Selected = 1;
													}
												}
											}
										}
									}
									else
									{
										this.Buttons.alpha -= Time.deltaTime;

										if (this.Buttons.alpha == 0.0f)
										{
											if (this.Selected == 2)
											{
												this.Controls.alpha = Mathf.MoveTowards(
													this.Controls.alpha, 1.0f, Time.deltaTime);
											}
											else
											{
												this.Credits.alpha = Mathf.MoveTowards(
													this.Credits.alpha, 1.0f, Time.deltaTime);
											}
										}

										if ((this.Controls.alpha == 1.0f) || (this.Credits.alpha == 1.0f))
										{
											if (Input.GetButtonDown(InputNames.Xbox_B))
											{
												Instantiate(this.ButtonEffect,
													this.BackButtons[this.Selected].position, Quaternion.identity);

												this.FadeButtons = false;
											}
										}
									}
								}
							}
						}
					}
					else
					{
						this.Prologue.GetComponent<AudioSource>().enabled = false;
						this.Midori.gameObject.SetActive(true);
						this.ScrollSpeed = 60.0f;

						this.Midori.transform.localPosition = new Vector3(
							Mathf.Lerp(this.Midori.transform.localPosition.x, 875.0f, Time.deltaTime * 2.0f),
							this.Midori.transform.localPosition.y,
							this.Midori.transform.localPosition.z);

						this.Midori.transform.localEulerAngles = new Vector3(
							this.Midori.transform.localEulerAngles.x,
							this.Midori.transform.localEulerAngles.y,
							Mathf.Lerp(this.Midori.transform.localEulerAngles.z, 45.0f, Time.deltaTime * 2.0f));

						if (this.Midori.gameObject.GetComponent<AudioSource>().time > 3.0f)
						{
							YanvaniaGlobals.MidoriEasterEgg = false;
						}
					}
				}
			}
		}
		else
		{
			this.ErrorWindow.transform.localScale = Vector3.Lerp(
				this.ErrorWindow.transform.localScale, Vector3.zero, Time.deltaTime * 10.0f);

			this.Darkness.color = new Color(
				this.Darkness.color.r,
				this.Darkness.color.g,
				this.Darkness.color.b,
				this.Darkness.color.a + Time.deltaTime);

			audioSource.volume -= Time.deltaTime;

			if (this.Darkness.color.a >= 1.0f)
			{
				if (YanvaniaGlobals.DraculaDefeated)
				{
					SceneManager.LoadScene(SceneNames.HomeScene);
				}
				else
				{
					if (this.Selected == 1)
					{
						SceneManager.LoadScene(SceneNames.YanvaniaScene);
					}
					else
					{
						SceneManager.LoadScene(SceneNames.HomeScene);
					}
				}
			}
		}
	}

	void Skip()
	{
		this.Darkness.color = new Color(
			this.Darkness.color.r,
			this.Darkness.color.g,
			this.Darkness.color.b,
			0.0f);

		this.Logo.color = new Color(
			this.Logo.color.r,
			this.Logo.color.g,
			this.Logo.color.b,
			1.0f);

		AudioSource audioSource = this.GetComponent<AudioSource>();
		audioSource.loop = true;
		audioSource.clip = this.BGM;
		audioSource.Play();
	}
}
