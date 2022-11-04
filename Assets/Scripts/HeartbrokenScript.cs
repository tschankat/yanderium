using UnityEngine;

using XInputDotNetPure;

public class HeartbrokenScript : MonoBehaviour
{
	public ShoulderCameraScript ShoulderCamera;
	public HeartbrokenCursorScript Cursor;
	public CounselorScript Counselor;
	public YandereScript Yandere;
	public ClockScript Clock;

	public AudioListener Listener;

	public AudioClip[] NoticedClips;
	public string[] NoticedLines;

	public UILabel[] Letters;
	public UILabel[] Options;

	public Vector3[] Origins;

	public UISprite Background;
	public UISprite Ground;

	public Camera MainCamera;

	public UILabel Subtitle;

	public GameObject SNAP;

	public AudioClip Slam;

	public bool Headmaster = false;
	public bool Confessed = false;
	public bool Arrested = false;
	public bool Exposed = false;
	public bool Noticed = true;
	public bool Freeze = false;
	public bool NoSnap = false;
    public bool Caught = false;

    public float VibrationTimer = 0.0f;
	public float AudioTimer = 0.0f;
	public float Timer = 0.0f;

	public int Phase = 1;

	public int LetterID = 0;
	public int ShakeID = 0;
	public int GrowID = 0;
	public int StopID = 0;
	public int ID = 0;

	void Start()
	{
        if (GameGlobals.MostRecentSlot == 0)
        {
            Options[2].color = new Color(
                Options[2].color.r * .5f,
                Options[2].color.g * .5f,
                Options[2].color.b * .5f,
                1);
        }

		if (!this.Caught && !this.Noticed && this.Yandere.Bloodiness > 0 && !this.Yandere.RedPaint && !this.Yandere.Unmasked)
		{
			this.Arrested = true;
		}

        if (this.Caught)
        {
            this.Letters[0].text = "";
            this.Letters[1].text = "";
            this.Letters[2].text = "C";
            this.Letters[3].text = "A";
            this.Letters[4].text = "U";
            this.Letters[5].text = "G";
            this.Letters[6].text = "H";
            this.Letters[7].text = "T";
            this.Letters[8].text = "";
            this.Letters[9].text = "";
            this.Letters[10].text = "";

            // [af] Converted while loop to foreach loop.
            foreach (UILabel letter in this.Letters)
            {
                letter.transform.localPosition = new Vector3(
                    letter.transform.localPosition.x + 125.0f,
                    letter.transform.localPosition.y,
                    letter.transform.localPosition.z);
            }

            this.LetterID = 1;
            this.StopID = 8;

            this.NoSnap = true;

            SNAP.SetActive(false);
            this.Cursor.Options = 3;
        }
        else if (this.Confessed)
		{
			this.Letters[0].text = "S";
			this.Letters[1].text = "E";
			this.Letters[2].text = "N";
			this.Letters[3].text = "P";
			this.Letters[4].text = "A";
			this.Letters[5].text = "I";
			this.Letters[6].text = string.Empty;
			this.Letters[7].text = "L";
			this.Letters[8].text = "O";
			this.Letters[9].text = "S";
			this.Letters[10].text = "T";

			this.LetterID = 0;
			this.StopID = 11;

			this.NoSnap = true;
		}
		else if (this.Yandere.Attacked)
		{
			if (!this.Headmaster)
			{
				this.Letters[0].text = string.Empty;
				this.Letters[1].text = "C";
				this.Letters[2].text = "O";
				this.Letters[3].text = "M";
				this.Letters[4].text = "A";
				this.Letters[5].text = "T";
				this.Letters[6].text = "O";
				this.Letters[7].text = "S";
				this.Letters[8].text = "E";
				this.Letters[9].text = string.Empty;
				this.Letters[10].text = string.Empty;

				this.Letters[3].fontSize = 250;
				this.LetterID = 1;
				this.StopID = 9;
			}
			else
			{
				this.Letters[0].text = "?";
				this.Letters[1].text = "?";
				this.Letters[2].text = "?";
				this.Letters[3].text = "?";
				this.Letters[4].text = "?";
				this.Letters[5].text = "?";
				this.Letters[6].text = "?";
				this.Letters[7].text = "?";
				this.Letters[8].text = "?";
				this.Letters[9].text = "?";
				this.Letters[10].text = string.Empty;

				this.LetterID = 0;
				this.StopID = 10;
			}
				
			// [af] Converted while loop to foreach loop.
			foreach (UILabel letter in this.Letters)
			{
				letter.transform.localPosition = new Vector3(
					letter.transform.localPosition.x + 100.0f,
					letter.transform.localPosition.y,
					letter.transform.localPosition.z);
			}
				
			this.SNAP.SetActive(false);
            this.Cursor.Options = 4;

			this.NoSnap = true;
		}
		else if (this.Yandere.Lost || this.ShoulderCamera.LookDown ||
		    	 this.ShoulderCamera.Counter || this.ShoulderCamera.ObstacleCounter)
		{
			this.Letters[0].text = "A";
			this.Letters[1].text = "P";
			this.Letters[2].text = "P";
			this.Letters[3].text = "R";
			this.Letters[4].text = "E";
			this.Letters[5].text = "H";
			this.Letters[6].text = "E";
			this.Letters[7].text = "N";
			this.Letters[8].text = "D";
			this.Letters[9].text = "E";
			this.Letters[10].text = "D";

			this.LetterID = 0;
			this.StopID = 11;

			this.NoSnap = true;
		}
		else if (this.Exposed)
		{
			this.Letters[0].text = string.Empty;
			this.Letters[1].text = string.Empty;
			this.Letters[2].text = "E";
			this.Letters[3].text = "X";
			this.Letters[4].text = "P";
			this.Letters[5].text = "O";
			this.Letters[6].text = "S";
			this.Letters[7].text = "E";
			this.Letters[8].text = "D";
			this.Letters[9].text = string.Empty;
			this.Letters[10].text = string.Empty;

			// [af] Converted while loop to foreach loop.
			foreach (UILabel letter in this.Letters)
			{
				letter.transform.localPosition = new Vector3(
					letter.transform.localPosition.x + 100.0f,
					letter.transform.localPosition.y,
					letter.transform.localPosition.z);
			}

			this.LetterID = 1;
			this.StopID = 9;

			this.NoSnap = true;
		}
		else if (this.Arrested)
		{
			this.Letters[0].text = string.Empty;
			this.Letters[1].text = "A";
			this.Letters[2].text = "R";
			this.Letters[3].text = "R";
			this.Letters[4].text = "E";
			this.Letters[5].text = "S";
			this.Letters[6].text = "T";
			this.Letters[7].text = "E";
			this.Letters[8].text = "D";
			this.Letters[9].text = string.Empty;
			this.Letters[10].text = string.Empty;

			// [af] Converted while loop to foreach loop.
			foreach (UILabel letter in this.Letters)
			{
				letter.transform.localPosition = new Vector3(
					letter.transform.localPosition.x + 100.0f,
					letter.transform.localPosition.y,
					letter.transform.localPosition.z);
			}

			this.LetterID = 1;
			this.StopID = 9;

			this.NoSnap = true;
		}
		else if (this.Counselor.Expelled || this.Yandere.Sprayed)
		{
			this.Letters[0].text = string.Empty;
			this.Letters[1].text = "E";
			this.Letters[2].text = "X";
			this.Letters[3].text = "P";
			this.Letters[4].text = "E";
			this.Letters[5].text = "L";
			this.Letters[6].text = "L";
			this.Letters[7].text = "E";
			this.Letters[8].text = "D";
			this.Letters[9].text = string.Empty;
			this.Letters[10].text = string.Empty;

			// [af] Converted while loop to foreach loop.
			foreach (UILabel letter in this.Letters)
			{
				letter.transform.localPosition = new Vector3(
					letter.transform.localPosition.x + 100.0f,
					letter.transform.localPosition.y,
					letter.transform.localPosition.z);
			}

			this.LetterID = 1;
			this.StopID = 9;

			this.NoSnap = true;
		}
		else
		{
			this.LetterID = 0;
			this.StopID = 11;
		}

		// [af] Converted while loop to for loop.
		for (this.ID = 0; this.ID < this.Letters.Length; this.ID++)
		{
			UILabel letter = this.Letters[this.ID];
			letter.transform.localScale = new Vector3(10.0f, 10.0f, 1.0f);
			letter.color = new Color(
				letter.color.r,
				letter.color.g,
				letter.color.b,
				0.0f);

			this.Origins[this.ID] = letter.transform.localPosition;
		}

		// [af] Converted while loop to for loop.
		for (this.ID = 0; this.ID < this.Options.Length; this.ID++)
		{
			UILabel option = this.Options[this.ID];
			option.color = new Color(
				option.color.r,
				option.color.g,
				option.color.b,
				0.0f);
		}

		this.ID = 0;

		this.Subtitle.color = new Color(
			this.Subtitle.color.r,
			this.Subtitle.color.g,
			this.Subtitle.color.b,
			0.0f);

		if (this.Noticed)
		{
			// [af] Commented in JS code.
			//Listener.enabled = false;

			this.Background.color = new Color(
				this.Background.color.r,
				this.Background.color.g,
				this.Background.color.b,
				0.0f);

			this.Ground.color = new Color(
				this.Ground.color.r,
				this.Ground.color.g,
				this.Ground.color.b,
				0.0f);
		}
		else
		{
			this.transform.parent.transform.position = new Vector3(
				this.transform.parent.transform.position.x,
				100.0f,
				this.transform.parent.transform.position.z);
		}

        if (Cursor.SnappedYandere != null)
        {
		    int Weapons = 0;

		    foreach (WeaponScript weapon in Cursor.SnappedYandere.Weapons)
		    {
			    if (weapon != null)
			    {
				    Weapons++;
			    }
		    }

		    if (Weapons == 0 || this.NoSnap ||
			    this.Yandere.Police.GameOver ||
			    this.Yandere.StudentManager.Clock.HourTime >= 18 ||
			    this.Yandere.transform.position.y < -1)
		    {
			    SNAP.SetActive(false);
			    this.Cursor.Options = 4;
		    }

		    this.Clock.StopTime = true;
        }
    }

	void Update()
	{
		if (Input.GetKeyDown("m"))
		{
			this.gameObject.GetComponent<AudioSource>().Stop();
		}

		this.VibrationTimer = Mathf.MoveTowards(this.VibrationTimer, 0, Time.deltaTime);

		if (this.VibrationTimer == 0)
		{
			GamePad.SetVibration(0, 0, 0);
		}

		if (this.Noticed)
		{
			this.Ground.transform.eulerAngles = new Vector3(90.0f, 0.0f, 0.0f);

			this.Ground.transform.position = new Vector3(
				this.Ground.transform.position.x,
				this.Yandere.transform.position.y,
				this.Ground.transform.position.z);
		}

		this.Timer += Time.deltaTime;

        if (this.Timer > 3.0f)
        {
            if (this.Phase == 1)
            {
                if (this.Noticed)
                {
                    this.UpdateSubtitle();
                }

                this.Phase += (this.Subtitle.color.a > 0.0f) ? 1 : 2;
            }
            else if (this.Phase == 2)
            {
                if (Input.GetButtonDown(InputNames.Xbox_A))
                {
                    this.AudioTimer = 100;
                }

                this.AudioTimer += Time.deltaTime;

                if (this.AudioTimer > this.Subtitle.GetComponent<AudioSource>().clip.length)
                {
                    this.Phase++;
                }
            }
        }
        else
        {
            if (this.Yandere != null)
            {
                if (this.Yandere.Unmasked)
                {
                    this.Yandere.ShoulderCamera.transform.position = Vector3.Lerp(
                                this.Yandere.ShoulderCamera.transform.position,
                                this.Yandere.ShoulderCamera.NoticedPOV.position,
                                Time.deltaTime * 1);

                    this.Yandere.ShoulderCamera.transform.LookAt(this.Yandere.ShoulderCamera.NoticedFocus);

                    if (Vector3.Distance(this.Yandere.transform.position, this.Yandere.Senpai.position) < 1.25f)
                    {
                        this.Yandere.MyController.Move(this.Yandere.transform.forward * (Time.deltaTime * -1));
                    }

                    if (this.Yandere.CharacterAnimation[AnimNames.FemaleDown22].time >=
                        this.Yandere.CharacterAnimation[AnimNames.FemaleDown22].length)
                    {
                        this.Yandere.CharacterAnimation.CrossFade(AnimNames.FemaleDown23);
                    }
                }
            }
        }

		if (this.Background.color.a < 1.0f)
		{
			this.Background.color = new Color(
				this.Background.color.r,
				this.Background.color.g,
				this.Background.color.b,
				this.Background.color.a + Time.deltaTime);

			this.Ground.color = new Color(
				this.Ground.color.r,
				this.Ground.color.g,
				this.Ground.color.b,
				this.Ground.color.a + Time.deltaTime);

			if (this.Background.color.a >= 1.0f)
			{
				this.MainCamera.enabled = false;
			}
		}

		AudioSource audioSource = this.GetComponent<AudioSource>();

		if (this.LetterID < this.StopID)
		{
			UILabel letter = this.Letters[this.LetterID];

			letter.transform.localScale = Vector3.MoveTowards(
				letter.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 100.0f);
			
			letter.color = new Color(
				letter.color.r,
				letter.color.g,
				letter.color.b,
				letter.color.a + (Time.deltaTime * 10.0f));

			if (letter.transform.localScale == new Vector3(1.0f, 1.0f, 1.0f))
			{
				audioSource.PlayOneShot(this.Slam);
				GamePad.SetVibration(0, 1, 1);
				this.VibrationTimer = .1f;
				this.LetterID++;

				if (this.LetterID == this.StopID)
				{
					this.ID = 0;
				}
			}
		}
		else
		{
			if (this.Phase == 3)
			{
				if (this.Options[0].color.a == 0.0f)
				{
					this.Subtitle.color = new Color(
						this.Subtitle.color.r,
						this.Subtitle.color.g,
						this.Subtitle.color.b,
						0.0f);

					audioSource.Play();
				}

				if (this.ID < this.Options.Length)
				{
					UILabel option = this.Options[this.ID];
					option.color = new Color(
						option.color.r,
						option.color.g,
						option.color.b,
						option.color.a + (Time.deltaTime * 5.0f));

					if (option.color.a >= 1.0f)
					{
						this.ID++;
					}
				}
			}
		}

		if (!this.Freeze)
		{
			// [af] Converted while loop to for loop.
			for (this.ShakeID = 0; this.ShakeID < this.Letters.Length; this.ShakeID++)
			{
				UILabel letter = this.Letters[this.ShakeID];
				Vector3 origin = this.Origins[this.ShakeID];
				letter.transform.localPosition = new Vector3(
					origin.x + Random.Range(-5.0f, 5.0f),
					origin.y + Random.Range(-5.0f, 5.0f),
					letter.transform.localPosition.z);
			}
		}

		// [af] Converted while loop to for loop.
		for (this.GrowID = 0; this.GrowID < 5; this.GrowID++)
		{
			UILabel option = this.Options[this.GrowID];

			// [af] Replaced if/else statement with assignment and ternary expression.
			option.transform.localScale = Vector3.Lerp(
				option.transform.localScale,
				((this.Cursor.Selected - 1) != this.GrowID) ? new Vector3(0.50f, 0.50f, 0.50f) : new Vector3(1.0f, 1.0f, 1.0f),
				Time.deltaTime * 10.0f);
		}
	}

	void UpdateSubtitle()
	{
		StudentScript senpai = this.Yandere.Senpai.GetComponent<StudentScript>();

		if (!senpai.Teacher && this.Yandere.Noticed)
		{
			this.Subtitle.color = new Color(
				this.Subtitle.color.r,
				this.Subtitle.color.g,
				this.Subtitle.color.b,
				1.0f);

			GameOverType cause = senpai.GameOverCause;
			int clipID = 0;

			if (cause == GameOverType.Stalking)
			{
				clipID = 4;
			}
			else if (cause == GameOverType.Insanity)
			{
				clipID = 3;
			}
			else if (cause == GameOverType.Weapon)
			{
				clipID = 1;
			}
			else if (cause == GameOverType.Murder)
			{
				clipID = 5;
			}
			else if (cause == GameOverType.Blood)
			{
				clipID = 2;
			}
			else if (cause == GameOverType.Lewd)
			{
				clipID = 6;
			}

			this.Subtitle.text = this.NoticedLines[clipID];
			this.Subtitle.GetComponent<AudioSource>().clip = this.NoticedClips[clipID];
			this.Subtitle.GetComponent<AudioSource>().Play();
		}
		else
		{
			if (this.Headmaster)
			{
				this.Subtitle.color = new Color(
					this.Subtitle.color.r,
					this.Subtitle.color.g,
					this.Subtitle.color.b,
					1.0f);

				this.Subtitle.text = this.NoticedLines[8];
				this.Subtitle.GetComponent<AudioSource>().clip = this.NoticedClips[8];
				this.Subtitle.GetComponent<AudioSource>().Play();
			}
		}
	}

	public void Darken()
	{
		int TempID = 0;
			
		while (TempID < Letters.Length) 
		{
			if (this.Letters[TempID].color.a > 1)
			{
				this.Letters[TempID].color = new Color (1, 0, 0, 1);
			}

			this.Letters[TempID].color = new Color (1, 0, 0, this.Letters[TempID].color.a - 1.0f / 17.0f);
			TempID++;
		}

		TempID = 0;

		while (TempID < 4) 
		{
			if (this.Options[TempID].color.a > 1)
			{
				this.Options[TempID].color = new Color(
					this.Options[TempID].color.r,
					this.Options[TempID].color.g,
					this.Options[TempID].color.b,
					1);
			}

			this.Options[TempID].color = new Color(
				this.Options[TempID].color.r,
				this.Options[TempID].color.g,
				this.Options[TempID].color.b,
				this.Options[TempID].color.a - 1.0f / 17.0f);
			TempID++;
		}
	}
}