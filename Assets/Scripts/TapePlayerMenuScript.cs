using UnityEngine;

public class TapePlayerMenuScript : MonoBehaviour
{
    public InputManagerScript InputManager;
	public TapePlayerScript TapePlayer;
	public PromptBarScript PromptBar;
    public Animation TapePlayerAnim;
    public AudioSource MyAudio;
    public GameObject Jukebox;

	public Transform TapePlayerCamera;
	public Transform Highlight;
	public Transform TimeBar;
	public Transform List;

	public AudioClip[] Recordings;
	public AudioClip[] BasementRecordings;
	public AudioClip[] HeadmasterRecordings;
	public UILabel[] TapeLabels;
	public GameObject[] NewIcons;

	public AudioClip TapeStop;

	public string CurrentTime;
	public string ClipLength;

	public bool Listening = false;
	public bool Show = false;

	public UILabel HeaderLabel;
	public UILabel Subtitle;
	public UILabel Label;
	public UISprite Bar;

	public int TotalTapes = 10;
	public int Category = 1;
	public int Selected = 1;
	public int Phase = 1;

	public float RoundedTime = 0.0f;
	public float ResumeTime = 0.0f;
	public float Timer = 0.0f;

	public float[] Cues1;
	public float[] Cues2;
	public float[] Cues3;
	public float[] Cues4;
	public float[] Cues5;
	public float[] Cues6;
	public float[] Cues7;
	public float[] Cues8;
	public float[] Cues9;
	public float[] Cues10;

	public string[] Subs1;
	public string[] Subs2;
	public string[] Subs3;
	public string[] Subs4;
	public string[] Subs5;
	public string[] Subs6;
	public string[] Subs7;
	public string[] Subs8;
	public string[] Subs9;
	public string[] Subs10;

	public float[] BasementCues1;
	public float[] BasementCues10;

	public string[] BasementSubs1;
	public string[] BasementSubs10;

	public float[] HeadmasterCues1;
	public float[] HeadmasterCues2;
    public float[] HeadmasterCues3;
    public float[] HeadmasterCues4;
    public float[] HeadmasterCues5;
    public float[] HeadmasterCues6;
    public float[] HeadmasterCues7;
    public float[] HeadmasterCues8;
    public float[] HeadmasterCues9;
    public float[] HeadmasterCues10;

	public string[] HeadmasterSubs1;
	public string[] HeadmasterSubs2;
    public string[] HeadmasterSubs3;
    public string[] HeadmasterSubs4;
    public string[] HeadmasterSubs5;
    public string[] HeadmasterSubs6;
    public string[] HeadmasterSubs7;
    public string[] HeadmasterSubs8;
    public string[] HeadmasterSubs9;
    public string[] HeadmasterSubs10;

	void Start()
	{
		this.List.transform.localPosition = new Vector3(
			-955.0f,
			this.List.transform.localPosition.y,
			this.List.transform.localPosition.z);

		this.TimeBar.localPosition = new Vector3(
			this.TimeBar.localPosition.x,
			100.0f,
			this.TimeBar.localPosition.z);

		this.Subtitle.text = string.Empty;

		this.TapePlayerCamera.position = new Vector3(
			-26.15f,
			this.TapePlayerCamera.position.y,
			5.35f);
	}

	void Update()
	{
		float lerpSpeed = Time.unscaledDeltaTime * 10.0f;

        if (Input.GetKeyDown("="))
        {
            this.MyAudio.pitch++;
        }

        if (Input.GetKeyDown("-"))
        {
            this.MyAudio.pitch--;
        }

        if (!this.Show)
		{
			if (this.List.localPosition.x > -955.0f)
			{
				this.List.localPosition = new Vector3(
					Mathf.Lerp(this.List.localPosition.x, -956.0f, lerpSpeed),
					this.List.localPosition.y,
					this.List.localPosition.z);

				this.TimeBar.localPosition = new Vector3(
					this.TimeBar.localPosition.x,
					Mathf.Lerp(this.TimeBar.localPosition.y, 100.0f, lerpSpeed),
					this.TimeBar.localPosition.z);
			}
			else
			{
				this.TimeBar.gameObject.SetActive(false);
				this.List.gameObject.SetActive(false);
			}
		}
		else
		{
			if (this.Listening)
			{
				this.List.localPosition = new Vector3(
					Mathf.Lerp(this.List.localPosition.x, -955.0f, lerpSpeed),
					this.List.localPosition.y,
					this.List.localPosition.z);

				this.TimeBar.localPosition = new Vector3(
					this.TimeBar.localPosition.x,
					Mathf.Lerp(this.TimeBar.localPosition.y, 0.0f, lerpSpeed),
					this.TimeBar.localPosition.z);

				this.TapePlayerCamera.position = new Vector3(
					Mathf.Lerp(this.TapePlayerCamera.position.x, -26.15f, lerpSpeed),
					this.TapePlayerCamera.position.y,
					Mathf.Lerp(this.TapePlayerCamera.position.z, 5.35f, lerpSpeed));

				if (this.Phase == 1)
				{
					this.TapePlayerAnim["InsertTape"].time += (1.0f / 60.0f) * 3.33333f;

					if (this.TapePlayerAnim["InsertTape"].time >=
						this.TapePlayerAnim["InsertTape"].length)
					{
						this.TapePlayerAnim.Play("PressPlay");
                        this.MyAudio.pitch = 1;
                        this.MyAudio.Play();

						this.PromptBar.Label[0].text = "PAUSE";
						this.PromptBar.Label[1].text = "STOP";
						this.PromptBar.Label[5].text = "REWIND / FAST FORWARD";
						this.PromptBar.UpdateButtons();

						this.Phase++;
					}
				}
				else if (this.Phase == 2)
				{
					this.Timer += 1.0f / 60.0f;

					if (this.MyAudio.isPlaying)
					{
						if (this.Timer > .1f)
						{
							this.TapePlayerAnim["PressPlay"].time += 1.0f / 60.0f;

							if (this.TapePlayerAnim["PressPlay"].time > this.TapePlayerAnim["PressPlay"].length)
							{
								this.TapePlayerAnim["PressPlay"].time = this.TapePlayerAnim["PressPlay"].length;
							}
						}
					}
					else
					{
                        this.TapePlayerAnim["PressPlay"].time -= 1.0f / 60.0f;

						if (this.TapePlayerAnim["PressPlay"].time < 0)
						{
							this.TapePlayerAnim["PressPlay"].time = 0;
						}

						if (Input.GetButtonDown(InputNames.Xbox_A))
						{
							this.PromptBar.Label[0].text = "PAUSE";
							this.TapePlayer.Spin = true;

                            this.MyAudio.time = this.ResumeTime;
                            this.MyAudio.Play();
						}
					}

					if (this.TapePlayerAnim["PressPlay"].time >= this.TapePlayerAnim["PressPlay"].length)
					{
						this.TapePlayer.Spin = true;

                        if (this.MyAudio.time >= this.MyAudio.clip.length - 3)
                        {
                            this.MyAudio.pitch = 1;
                            Time.timeScale = 1;
                        }

                        if (this.MyAudio.time >= this.MyAudio.clip.length - 1)
						{
							this.TapePlayerAnim.Play("PressEject");
							this.TapePlayer.Spin = false;

							if (!this.MyAudio.isPlaying)
							{
                                this.MyAudio.clip = this.TapeStop;
                                this.MyAudio.Play();
							}

							this.Subtitle.text = string.Empty;

							this.Phase++;
						}

						if (Input.GetButtonDown(InputNames.Xbox_A))
						{
							if (this.MyAudio.isPlaying)
							{
								this.PromptBar.Label[0].text = "PLAY";
								this.TapePlayer.Spin = false;

								this.ResumeTime = this.MyAudio.time;
                                this.MyAudio.Stop();
							}
						}
					}

					if (Input.GetButtonDown(InputNames.Xbox_B))
					{
						this.TapePlayerAnim.Play("PressEject");
                        this.TapePlayer.Spin = false;

                        this.MyAudio.clip = this.TapeStop;
                        this.MyAudio.Play();

						this.PromptBar.Label[0].text = string.Empty;
						this.PromptBar.Label[1].text = string.Empty;
						this.PromptBar.Label[5].text = string.Empty;
						this.PromptBar.UpdateButtons();

						this.Subtitle.text = string.Empty;

						this.Phase++;
					}
				}
				else if (this.Phase == 3)
				{
					this.TapePlayerAnim["PressEject"].time += 1.0f / 60.0f;

					if (this.TapePlayerAnim["PressEject"].time >=
						this.TapePlayerAnim["PressEject"].length)
					{
						this.TapePlayerAnim.Play("InsertTape");

                        this.TapePlayerAnim["InsertTape"].time =
							this.TapePlayerAnim["InsertTape"].length;
						this.TapePlayer.FastForward = false;
						this.Phase++;
					}
				}
				else if (this.Phase == 4)
				{
                    Debug.Log("timescale is: " + Time.timeScale + " and animation time is " + this.TapePlayerAnim["InsertTape"].time);

                    if (this.TapePlayerAnim["InsertTape"].time > this.TapePlayerAnim["InsertTape"].length)
                    {
                        this.TapePlayerAnim["InsertTape"].time = this.TapePlayerAnim["InsertTape"].length;
                    }

					this.TapePlayerAnim["InsertTape"].time -= (1.0f / 60.0f) * 3.33333f;

					if (this.TapePlayerAnim["InsertTape"].time <= 0.0f)
					{
						this.TapePlayer.Tape.SetActive(false);
						this.Jukebox.SetActive(true);
						this.Listening = false;
						this.Timer = 0.0f;

						this.PromptBar.Label[0].text = "PLAY";
						this.PromptBar.Label[1].text = "BACK";
						this.PromptBar.Label[4].text = "CHOOSE";
						this.PromptBar.Label[5].text = "CATEGORY";
						this.PromptBar.UpdateButtons();
					}
				}

				if (this.Phase == 2)
				{
					if (this.InputManager.DPadRight || Input.GetKey(KeyCode.RightArrow))
					{
						this.ResumeTime += 1.666666666f;
                        this.MyAudio.time += 1.666666666f;
						this.TapePlayer.FastForward = true;
					}
					else
					{
						this.TapePlayer.FastForward = false;
					}

					if (this.InputManager.DPadLeft || Input.GetKey(KeyCode.LeftArrow))
					{
						this.ResumeTime -= 1.666666666f;
                        this.MyAudio.time -= 1.666666666f;
						this.TapePlayer.Rewind = true;
					}
					else
					{
						this.TapePlayer.Rewind = false;
					}

					int Minutes = 0;
					int Seconds = 0;

					if (this.MyAudio.isPlaying)
					{
						Minutes = Mathf.FloorToInt(this.MyAudio.time / 60.0f);
						Seconds = Mathf.FloorToInt(this.MyAudio.time - (Minutes * 60.0f));

						this.Bar.fillAmount = this.MyAudio.time / this.MyAudio.clip.length;
					}
					else
					{
						Minutes = Mathf.FloorToInt(this.ResumeTime / 60.0f);
						Seconds = Mathf.FloorToInt(this.ResumeTime - (Minutes * 60.0f));

						this.Bar.fillAmount = this.ResumeTime / this.MyAudio.clip.length;
					}

					this.CurrentTime = string.Format("{00:00}:{1:00}", Minutes, Seconds);

					this.Label.text = this.CurrentTime + " / " + this.ClipLength;

					if (this.Category == 1)
					{
						if (this.Selected == 1)
						{
							for (int ID = 0; ID < this.Cues1.Length; ID++)
							{
								if (this.MyAudio.time > this.Cues1[ID])
								{
									this.Subtitle.text = this.Subs1[ID];
								}
							}
						}
						else if (this.Selected == 2)
						{
							for (int ID = 0; ID < this.Cues2.Length; ID++)
							{
								if (this.MyAudio.time > this.Cues2[ID])
								{
									this.Subtitle.text = this.Subs2[ID];
								}
							}
						}
						else if (this.Selected == 3)
						{
							for (int ID = 0; ID < this.Cues3.Length; ID++)
							{
								if (this.MyAudio.time > this.Cues3[ID])
								{
									this.Subtitle.text = this.Subs3[ID];
								}
							}
						}
						else if (this.Selected == 4)
						{
							for (int ID = 0; ID < this.Cues4.Length; ID++)
							{
								if (this.MyAudio.time > this.Cues4[ID])
								{
									this.Subtitle.text = this.Subs4[ID];
								}
							}
						}
						else if (this.Selected == 5)
						{
							for (int ID = 0; ID < this.Cues5.Length; ID++)
							{
								if (this.MyAudio.time > this.Cues5[ID])
								{
									this.Subtitle.text = this.Subs5[ID];
								}
							}
						}
						else if (this.Selected == 6)
						{
							for (int ID = 0; ID < this.Cues6.Length; ID++)
							{
								if (this.MyAudio.time > this.Cues6[ID])
								{
									this.Subtitle.text = this.Subs6[ID];
								}
							}
						}
						else if (this.Selected == 7)
						{
							for (int ID = 0; ID < this.Cues7.Length; ID++)
							{
								if (this.MyAudio.time > this.Cues7[ID])
								{
									this.Subtitle.text = this.Subs7[ID];
								}
							}
						}
						else if (this.Selected == 8)
						{
							for (int ID = 0; ID < this.Cues8.Length; ID++)
							{
								if (this.MyAudio.time > this.Cues8[ID])
								{
									this.Subtitle.text = this.Subs8[ID];
								}
							}
						}
						else if (this.Selected == 9)
						{
							for (int ID = 0; ID < this.Cues9.Length; ID++)
							{
								if (this.MyAudio.time > this.Cues9[ID])
								{
									this.Subtitle.text = this.Subs9[ID];
								}
							}
						}
						else if (this.Selected == 10)
						{
							for (int ID = 0; ID < this.Cues10.Length; ID++)
							{
								if (this.MyAudio.time > this.Cues10[ID])
								{
									this.Subtitle.text = this.Subs10[ID];
								}
							}
						}
					}
					else if (this.Category == 2)
					{
						if (this.Selected == 1)
						{
							for (int ID = 0; ID < this.BasementCues1.Length; ID++)
							{
								if (this.MyAudio.time > this.BasementCues1[ID])
								{
									this.Subtitle.text = this.BasementSubs1[ID];
								}
							}
						}

						if (this.Selected == 10)
						{
							for (int ID = 0; ID < this.BasementCues10.Length; ID++)
							{
								if (this.MyAudio.time > this.BasementCues10[ID])
								{
									this.Subtitle.text = this.BasementSubs10[ID];
								}
							}
						}
					}
					else if (this.Category == 3)
					{
						if (this.Selected == 1)
						{
							for (int ID = 0; ID < this.HeadmasterCues1.Length; ID++)
							{
								if (this.MyAudio.time > this.HeadmasterCues1[ID]){this.Subtitle.text = this.HeadmasterSubs1[ID];}
							}
						}
						else if (this.Selected == 2)
						{
							for (int ID = 0; ID < this.HeadmasterCues2.Length; ID++)
							{
								if (this.MyAudio.time > this.HeadmasterCues2[ID]){this.Subtitle.text = this.HeadmasterSubs2[ID];}
							}
						}
                        else if (this.Selected == 3)
                        {
                            for (int ID = 0; ID < this.HeadmasterCues3.Length; ID++)
                            {
                                if (this.MyAudio.time > this.HeadmasterCues3[ID]){this.Subtitle.text = this.HeadmasterSubs3[ID];}
                            }
                        }
                        else if (this.Selected == 4)
                        {
                            for (int ID = 0; ID < this.HeadmasterCues4.Length; ID++)
                            {
                                if (this.MyAudio.time > this.HeadmasterCues4[ID]){this.Subtitle.text = this.HeadmasterSubs4[ID];}
                            }
                        }
                        else if (this.Selected == 5)
                        {
                            for (int ID = 0; ID < this.HeadmasterCues5.Length; ID++)
                            {
                                if (this.MyAudio.time > this.HeadmasterCues5[ID]){this.Subtitle.text = this.HeadmasterSubs5[ID];}
                            }
                        }
                        else if (this.Selected == 6)
						{
							for (int ID = 0; ID < this.HeadmasterCues6.Length; ID++)
							{
								if (this.MyAudio.time > this.HeadmasterCues6[ID]){this.Subtitle.text = this.HeadmasterSubs6[ID];}
							}
						}
                        else if (this.Selected == 7)
                        {
                            for (int ID = 0; ID < this.HeadmasterCues7.Length; ID++)
                            {
                                if (this.MyAudio.time > this.HeadmasterCues7[ID]){this.Subtitle.text = this.HeadmasterSubs7[ID];}
                            }
                        }
                        else if (this.Selected == 8)
                        {
                            for (int ID = 0; ID < this.HeadmasterCues8.Length; ID++)
                            {
                                if (this.MyAudio.time > this.HeadmasterCues8[ID]){this.Subtitle.text = this.HeadmasterSubs8[ID];}
                            }
                        }
                        else if (this.Selected == 9)
                        {
                            for (int ID = 0; ID < this.HeadmasterCues9.Length; ID++)
                            {
                                if (this.MyAudio.time > this.HeadmasterCues9[ID]){this.Subtitle.text = this.HeadmasterSubs9[ID];}
                            }
                        }
                        else if (this.Selected == 10)
						{
							for (int ID = 0; ID < this.HeadmasterCues10.Length; ID++)
							{
								if (this.MyAudio.time > this.HeadmasterCues10[ID]){this.Subtitle.text = this.HeadmasterSubs10[ID];}
							}
						}
					}
				}
				else
				{
					this.Label.text = "00:00 / 00:00";

					this.Bar.fillAmount = 0.0f;
				}
			}
			else
			{
                this.TapePlayerAnim.Stop();

				this.TapePlayerCamera.position = new Vector3(
					Mathf.Lerp(this.TapePlayerCamera.position.x, -26.2125f, lerpSpeed),
					this.TapePlayerCamera.position.y,
					Mathf.Lerp(this.TapePlayerCamera.position.z, 5.4125f, lerpSpeed));

				this.List.transform.localPosition = new Vector3(
					Mathf.Lerp(this.List.transform.localPosition.x, 0.0f, lerpSpeed),
					this.List.transform.localPosition.y,
					this.List.transform.localPosition.z);

				this.TimeBar.localPosition = new Vector3(
					this.TimeBar.localPosition.x,
					Mathf.Lerp(this.TimeBar.localPosition.y, 100.0f, lerpSpeed),
					this.TimeBar.localPosition.z);

				if (this.InputManager.TappedRight)
				{
					this.Category++;

					if (this.Category > 3)
					{
						this.Category = 1;
					}

					this.UpdateLabels();
				}
				else if (this.InputManager.TappedLeft)
				{
					this.Category--;

					if (this.Category < 1)
					{
						this.Category = 3;
					}

					this.UpdateLabels();
				}

				if (this.InputManager.TappedUp)
				{
					this.Selected--;

					if (this.Selected < 1)
					{
						this.Selected = 10;
					}

					this.Highlight.localPosition = new Vector3(
						this.Highlight.localPosition.x,
						440.0f - (80.0f * this.Selected),
						this.Highlight.localPosition.z);

					this.CheckSelection();
				}
				else if (this.InputManager.TappedDown)
				{
					this.Selected++;

					if (this.Selected > 10)
					{
						this.Selected = 1;
					}

					this.Highlight.localPosition = new Vector3(
						this.Highlight.localPosition.x,
						440.0f - (80.0f * this.Selected),
						this.Highlight.localPosition.z);

					this.CheckSelection();
				}
				else if (Input.GetButtonDown(InputNames.Xbox_A))
				{
					bool PlayTape = false;

					if (this.Category == 1)
					{
						if (CollectibleGlobals.GetTapeCollected(this.Selected))
						{
							CollectibleGlobals.SetTapeListened(this.Selected, true);
							PlayTape = true;
						}
					}
					else if (this.Category == 2)
					{
						if (CollectibleGlobals.GetBasementTapeCollected(this.Selected))
						{
							CollectibleGlobals.SetBasementTapeListened(this.Selected, true);
							PlayTape = true;
						}
					}
					else if (this.Category == 3)
					{
						if (CollectibleGlobals.GetHeadmasterTapeCollected(this.Selected))
						{
							CollectibleGlobals.SetHeadmasterTapeListened(this.Selected, true);
							PlayTape = true;
						}
					}

					if (PlayTape)
					{
						this.NewIcons[this.Selected].SetActive(false);
						this.Jukebox.SetActive(false);
						this.Listening = true;
						this.Phase = 1;

						this.PromptBar.Label[0].text = string.Empty;
						this.PromptBar.Label[1].text = string.Empty;
						this.PromptBar.Label[4].text = string.Empty;
						this.PromptBar.UpdateButtons();

                        this.TapePlayerAnim["InsertTape"].time = 0;
                        this.TapePlayerAnim.Play("InsertTape");
						this.TapePlayer.Tape.SetActive(true);

						if (this.Category == 1)
						{
                            this.MyAudio.clip = this.Recordings[this.Selected];
						}
						else if (this.Category == 2)
						{
                            this.MyAudio.clip = this.BasementRecordings[this.Selected];
						}
						else
						{
                            this.MyAudio.clip = this.HeadmasterRecordings[this.Selected];
						}

                        this.MyAudio.time = 0.0f;

						this.RoundedTime = Mathf.CeilToInt(this.MyAudio.clip.length);

						int Minutes = (int)(this.RoundedTime / 60.0f);
						int Seconds = (int)(this.RoundedTime % 60.0f);

						this.ClipLength = string.Format("{0:00}:{1:00}", Minutes, Seconds);
					}
				}
				else if (Input.GetButtonDown(InputNames.Xbox_B))
				{
					this.TapePlayer.Yandere.HeartCamera.enabled = true;
					this.TapePlayer.Yandere.RPGCamera.enabled = true;
					this.TapePlayer.TapePlayerCamera.enabled = false;
					this.TapePlayer.NoteWindow.SetActive(true);
					this.TapePlayer.PromptBar.ClearButtons();
					this.TapePlayer.Yandere.CanMove = true;
					this.TapePlayer.PromptBar.Show = false;
					this.TapePlayer.Prompt.enabled = true;
					this.TapePlayer.Yandere.HUD.alpha = 1.0f;
					Time.timeScale = 1.0f;
					this.Show = false;
				}
			}
		}
	}

	public void UpdateLabels()
	{
		for (int ID = 0; ID < this.TotalTapes;)
		{
			ID++;

			if (this.Category == 1)
			{
				this.HeaderLabel.text = "Mysterious Tapes";

				if (CollectibleGlobals.GetTapeCollected(ID))
				{
					this.TapeLabels[ID].text = "Mysterious Tape " + ID.ToString();
					this.NewIcons[ID].SetActive(!CollectibleGlobals.GetTapeListened(ID));
				}
				else
				{
					this.TapeLabels[ID].text = "?????";
					this.NewIcons[ID].SetActive(false);
				}
			}
			else if (this.Category == 2)
			{
				this.HeaderLabel.text = "Basement Tapes";

				if (CollectibleGlobals.GetBasementTapeCollected(ID))
				{
					this.TapeLabels[ID].text = "Basement Tape " + ID.ToString();
					this.NewIcons[ID].SetActive(!CollectibleGlobals.GetBasementTapeListened(ID));
				}
				else
				{
					this.TapeLabels[ID].text = "?????";
					this.NewIcons[ID].SetActive(false);
				}
			}
			else
			{
				this.HeaderLabel.text = "Headmaster Tapes";

				if (CollectibleGlobals.GetHeadmasterTapeCollected(ID))
				{
					this.TapeLabels[ID].text = "Headmaster Tape " + ID.ToString();
					this.NewIcons[ID].SetActive(!CollectibleGlobals.GetHeadmasterTapeListened(ID));
				}
				else
				{
					this.TapeLabels[ID].text = "?????";
					this.NewIcons[ID].SetActive(false);
				}
			}
		}
	}

	public void CheckSelection()
	{
		if (this.Category == 1)
		{
			this.TapePlayer.PromptBar.Label[0].text =
				CollectibleGlobals.GetTapeCollected(this.Selected) ? "PLAY" : string.Empty;

			this.TapePlayer.PromptBar.UpdateButtons();
		}
		else if (this.Category == 2)
		{
			this.TapePlayer.PromptBar.Label[0].text =
				CollectibleGlobals.GetBasementTapeCollected(this.Selected) ? "PLAY" : string.Empty;

			this.TapePlayer.PromptBar.UpdateButtons();
		}
		else
		{
			this.TapePlayer.PromptBar.Label[0].text =
				CollectibleGlobals.GetHeadmasterTapeCollected(this.Selected) ? "PLAY" : string.Empty;

			this.TapePlayer.PromptBar.UpdateButtons();
		}
	}
}