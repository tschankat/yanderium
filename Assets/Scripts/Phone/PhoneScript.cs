using UnityEngine;
using UnityEngine.SceneManagement;

public class PhoneScript : MonoBehaviour
{
    public OsanaTextMessageScript OsanaMessages;

	public GameObject[] RightMessage;
	public GameObject[] LeftMessage;

	public AudioClip[] VoiceClips;

    public AudioClip SubtleWhoosh;
    public AudioClip AppInstall;

    public GameObject NewMessage;

	public AudioSource Jukebox;

	public Transform OldMessages;
    public Transform PauseMenu;
    public Transform InfoIcon;
    public Transform Buttons;
	public Transform Panel;

	public Vignetting Vignette;

	public UISprite Darkness;

	public UISprite Sprite;

	public int[] Speaker;
	public string[] Text;
	public int[] Height;

	public AudioClip[] KidnapClip;
	public int[] KidnapSpeaker;
	public string[] KidnapText;
	public int[] KidnapHeight;

	public AudioClip[] BefriendClip;
	public int[] BefriendSpeaker;
	public string[] BefriendText;
	public int[] BefriendHeight;

	public AudioClip[] NonlethalClip;
	public string[] NonlethalText;
	public int[] NonlethalHeight;

    public bool ManuallyAdvance = false;
    public bool MeetingInfoChan = false;
    public bool PostElimination = false;
    public bool ShowPauseMenu = false;
    public bool FadeOut = false;
	public bool Auto = false;

    public float PauseMenuTimer = 0.0f;
    public float AutoLimit = 0.0f;
	public float AutoTimer = 0.0f;
	public float Timer = 0.0f;

    public int PauseMenuPhase = 0;
	public int ID = 0;

	void Start()
	{
		this.Buttons.localPosition = new Vector3(
			this.Buttons.localPosition.x,
			-135.0f,
			this.Buttons.localPosition.z);

		this.Darkness.color = new Color(
			this.Darkness.color.r,
			this.Darkness.color.g,
			this.Darkness.color.b,
			1.0f);

		if (DateGlobals.Week > 1 && DateGlobals.Weekday == System.DayOfWeek.Sunday)
		{
			this.Darkness.color = new Color (0, 0, 0, 0);
		}

		if (EventGlobals.KidnapConversation)
		{
			this.VoiceClips = this.KidnapClip;
			this.Speaker = this.KidnapSpeaker;
			this.Text = this.KidnapText;
			this.Height = this.KidnapHeight;

			EventGlobals.BefriendConversation = true;
			EventGlobals.KidnapConversation = false;
		}
		else if (EventGlobals.BefriendConversation)
		{
			this.VoiceClips = this.BefriendClip;
			this.Speaker = this.BefriendSpeaker;
			this.Text = this.BefriendText;
			this.Height = this.BefriendHeight;

			EventGlobals.LivingRoom = true;
			EventGlobals.BefriendConversation = false;
		}
        else if (EventGlobals.OsanaConversation)
        {
            Debug.Log("Osana's text message conversation!");

            this.VoiceClips = this.OsanaMessages.OsanaClips;
            this.Speaker = this.OsanaMessages.OsanaSpeakers;
            this.Text = this.OsanaMessages.OsanaTexts;
            this.Height = this.OsanaMessages.OsanaHeights;

            EventGlobals.LivingRoom = true;
            //Don't set this false, the Living Room needs to know about it.
            //EventGlobals.OsanaConversation = false;
        }
        else
        {
            this.MeetingInfoChan = true;
        }

		if (GameGlobals.LoveSick)
		{
			Camera.main.backgroundColor = Color.black;
			LoveSickColorSwap();
		}

		if (PostElimination)
		{
			if (GameGlobals.NonlethalElimination)
			{
				this.VoiceClips[1] = this.NonlethalClip[1];
				this.VoiceClips[2] = this.NonlethalClip[2];
				this.VoiceClips[3] = this.NonlethalClip[3];

				this.Text[1] = this.NonlethalText[1];
				this.Text[2] = this.NonlethalText[2];
				this.Text[3] = this.NonlethalText[3];

				this.Height[1] = this.NonlethalHeight[1];
				this.Height[2] = this.NonlethalHeight[2];
				this.Height[3] = this.NonlethalHeight[3];
			}
		}
	}

	void Update()
	{
#if (UNITY_EDITOR)
		if (Input.GetKeyDown(KeyCode.K))
		{
			EventGlobals.LivingRoom = false;
			EventGlobals.KidnapConversation = true;
			EventGlobals.BefriendConversation = false;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

        if (Input.GetKeyDown(KeyCode.O))
        {
            EventGlobals.LivingRoom = false;
            EventGlobals.OsanaConversation = true;
            EventGlobals.KidnapConversation = false;
            EventGlobals.BefriendConversation = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.Space))
		{
			SceneManager.LoadScene(SceneNames.CalendarScene);
		}
#endif

		if (!this.FadeOut)
		{
			if (this.Timer > 0.0f && this.Buttons.gameObject.activeInHierarchy)
			{
				this.Darkness.color = new Color(
					this.Darkness.color.r,
					this.Darkness.color.g,
					this.Darkness.color.b,
					Mathf.MoveTowards(this.Darkness.color.a, 0.0f, Time.deltaTime));

				if (this.Darkness.color.a == 0.0f)
				{
					if (!this.Jukebox.isPlaying)
					{
						this.Jukebox.Play();
					}

					if (this.NewMessage == null)
					{
						this.SpawnMessage();
					}
				}
			}

            if (this.ShowPauseMenu)
            {
                this.PauseMenuTimer += Time.deltaTime;

                if (this.PauseMenuPhase == 0)
                {
                    this.PauseMenu.localPosition = Vector3.Lerp(this.PauseMenu.localPosition,
                        new Vector3(0, -15, 0),
                        Time.deltaTime * 10);

                    if (this.PauseMenuTimer > 1)
                    {
                        this.GetComponent<AudioSource>().clip = AppInstall;
                        this.GetComponent<AudioSource>().Play();

                        this.PauseMenuPhase++;
                    }
                }
                else if (this.PauseMenuPhase == 1)
                {
                    this.InfoIcon.localScale = Vector3.Lerp(this.InfoIcon.localScale,
                        new Vector3(1, 1, 1),
                        Time.deltaTime * 10);

                    if (this.PauseMenuTimer > 2)
                    {
                        this.GetComponent<AudioSource>().clip = SubtleWhoosh;
                        this.GetComponent<AudioSource>().Play();

                        this.PauseMenuPhase++;
                    }
                }
                else if (this.PauseMenuPhase == 2)
                {
                    this.PauseMenu.localPosition = Vector3.Lerp(this.PauseMenu.localPosition,
                        new Vector3(-485, -15, 0),
                        Time.deltaTime * 10);

                    if (this.PauseMenuTimer > 3)
                    {
                        this.GetComponent<AudioSource>().volume = 1;

                        this.ShowPauseMenu = false;
                        this.ManuallyAdvance = true;
                    }
                }
            }
            else
            {
			    if (this.NewMessage != null)
			    {
				    this.Buttons.localPosition = new Vector3(
					    this.Buttons.localPosition.x,
					    Mathf.Lerp(this.Buttons.localPosition.y, 0.0f, Time.deltaTime * 10.0f),
					    this.Buttons.localPosition.z);

				    this.AutoTimer += Time.deltaTime;

				    if (this.Auto && this.AutoTimer > this.VoiceClips[this.ID].length + 1 ||
					    Input.GetButtonDown(InputNames.Xbox_A) || ManuallyAdvance)
				    {
                        this.ManuallyAdvance = false;

                        if (this.MeetingInfoChan && this.ID == 16 && this.PauseMenuPhase == 0)
                        {
                            this.GetComponent<AudioSource>().clip = SubtleWhoosh;
                            this.GetComponent<AudioSource>().volume = .5f;
                            this.GetComponent<AudioSource>().Play();

                            this.ShowPauseMenu = true;
                        }
                        else
                        {
					        this.AutoTimer = 0.0f;

					        if (this.ID < (this.Text.Length - 1))
					        {
						        this.ID++;

						        this.SpawnMessage();
					        }
					        else
					        {
						        this.Darkness.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
						        this.FadeOut = true;

						        if (!this.Buttons.gameObject.activeInHierarchy)
						        {
							        this.Darkness.color = new Color(0.0f, 0.0f, 0.0f, 1);
						        }
					        }
                        }
                    }
                }

                if (Input.GetButtonDown(InputNames.Xbox_X))
				{
					this.FadeOut = true;
				}
			}
		}
		else
		{
			this.Buttons.localPosition = new Vector3(
				this.Buttons.localPosition.x,
				Mathf.Lerp(this.Buttons.localPosition.y, -135.0f, Time.deltaTime * 10.0f),
				this.Buttons.localPosition.z);

			this.GetComponent<AudioSource>().volume = 1.0f - this.Darkness.color.a;
			this.Jukebox.volume = .25f - this.Darkness.color.a * .25f;

			if (this.Darkness.color.a >= 1.0f)
			{
				if (DateGlobals.Week == 2)
				{
					SceneManager.LoadScene(SceneNames.CreditsScene);
				}
				else if (DateGlobals.Weekday == System.DayOfWeek.Sunday)
				{
					SceneManager.LoadScene(SceneNames.OsanaWarningScene);
				}
				else
				{
					if (!EventGlobals.BefriendConversation && !EventGlobals.LivingRoom)
					{
						SceneManager.LoadScene(SceneNames.CalendarScene);
					}
					else
					{
						if (EventGlobals.LivingRoom)
						{
							SceneManager.LoadScene(SceneNames.LivingRoomScene);
						}
						else
						{
							SceneManager.LoadScene(SceneManager.GetActiveScene().name);
						}
					}
				}
			}

			this.Darkness.color = new Color(
				this.Darkness.color.r,
				this.Darkness.color.g,
				this.Darkness.color.b,
				this.Darkness.color.a + Time.deltaTime);
		}

		this.Timer += Time.deltaTime;
	}

	void SpawnMessage()
	{
		if (this.NewMessage != null)
		{
			this.NewMessage.transform.parent = this.OldMessages;
			this.OldMessages.localPosition = new Vector3(
				this.OldMessages.localPosition.x,
				this.OldMessages.localPosition.y + (72.0f + (this.Height[this.ID] * 32.0f)),
				this.OldMessages.localPosition.z);
		}

		AudioSource audioSource = this.GetComponent<AudioSource>();
		audioSource.clip = this.VoiceClips[this.ID];
		audioSource.Play();

		if (this.Speaker[this.ID] == 1)
		{
			this.NewMessage = Instantiate(this.LeftMessage[this.Height[this.ID]]);
			this.NewMessage.transform.parent = this.Panel;
			this.NewMessage.transform.localPosition = new Vector3(-225.0f, -375.0f, 0.0f);
			this.NewMessage.transform.localScale = Vector3.zero;
		}
		else
		{
			this.NewMessage = Instantiate(this.RightMessage[this.Height[this.ID]]);
			this.NewMessage.transform.parent = this.Panel;
			this.NewMessage.transform.localPosition = new Vector3(225.0f, -375.0f, 0.0f);
			this.NewMessage.transform.localScale = Vector3.zero;

			if (this.Speaker == this.KidnapSpeaker)
			{
				if (this.Height[this.ID] == 8)
				{
					this.NewMessage.GetComponent<TextMessageScript>().Attachment = true;
				}
			}
		}
			
		if (this.Height[this.ID] == 9 && this.Speaker[this.ID] == 2)
		{
			this.Buttons.gameObject.SetActive(false);
			this.Darkness.enabled = true;
			this.Jukebox.Stop();
			this.Timer = -99999;
		}

		this.AutoLimit = this.Height[this.ID] + 1;

		this.NewMessage.GetComponent<TextMessageScript>().Label.text = this.Text[this.ID];
	}

	void LoveSickColorSwap()
	{
		GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

		foreach(GameObject go in allObjects)
		{
			UISprite sprite = go.GetComponent<UISprite> ();

			if (sprite != null)
			{
				if (sprite.color != Color.black && sprite.transform.parent)
				{
					sprite.color = new Color(1.0f, 0.0f, 0.0f, sprite.color.a);
				}
			}

			UILabel label = go.GetComponent<UILabel> ();

			if (label != null)
			{
				if (label.color != Color.black)
				{
					label.color = new Color(1.0f, 0.0f, 0.0f, label.color.a);
				}
			}

			Darkness.color = Color.black;
		}
	}
}
