using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour
{
	[SerializeField] JsonScript JSON;

	[SerializeField] Transform SpawnPoint;
	[SerializeField] Transform Panel;

	[SerializeField] GameObject SmallCreditsLabel;
	[SerializeField] GameObject BigCreditsLabel;

    [SerializeField] UILabel SkipLabel;

    [SerializeField] UISprite Darkness;

	[SerializeField] int ID = 0;

	[SerializeField] float SpeedUpFactor = 0.0f;
	[SerializeField] float TimerLimit = 0.0f;
	[SerializeField] float FadeTimer = 0.0f;
	[SerializeField] float Speed = 1.0f;
	[SerializeField] float Timer = 0.0f;

	[SerializeField] bool FadeOut = false;
	[SerializeField] bool Begin = false;

    [SerializeField] bool Dark = false;

    const int SmallTextSize = 1;
	const int BigTextSize = 2;

	public AudioClip DarkCreditsMusic;
	public AudioSource Jukebox;

    public ParticleSystem Blossoms;

	bool ShouldStopCredits
	{
		get { return this.ID == this.JSON.Credits.Length; }
	}

	GameObject SpawnLabel(int size)
	{
		return Instantiate(
			(size == SmallTextSize) ? this.SmallCreditsLabel : this.BigCreditsLabel,
			this.SpawnPoint.position,
			Quaternion.identity);
	}

	void Start()
	{
		if (DateGlobals.Weekday == DayOfWeek.Sunday || GameGlobals.DarkEnding)
		{
            GameGlobals.DarkEnding = false;

            Jukebox.clip = DarkCreditsMusic;
			Darkness.color = new Color (0, 0, 0, 0);
			Speed = 1.1f;

            Blossoms.startColor = new Color(.5f, 0, 0, 1);
            SkipLabel.color = new Color(.5f, 0, 0, 1);

            Dark = true;
		}
	}

	void Update()
	{
        if (Input.GetKeyDown("d"))
        {
            GameGlobals.DarkEnding = true;
            Application.LoadLevel(Application.loadedLevel);
        }

		if (!this.Begin)
		{
			this.Timer += Time.deltaTime;

			if (this.Timer > 1.0f)
			{
				this.Begin = true;
				Jukebox.Play();
				this.Timer = 0.0f;
			}
		}
		else
		{
			if (!this.ShouldStopCredits)
			{
				if (this.Timer == 0.0f)
				{
					// [af] Select the current credit data.
					CreditJson credit = this.JSON.Credits[this.ID];

					GameObject newCreditsLabel = this.SpawnLabel(credit.Size);
					this.TimerLimit = credit.Size * this.SpeedUpFactor;

					newCreditsLabel.transform.parent = this.Panel;
					newCreditsLabel.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
					newCreditsLabel.GetComponent<UILabel>().text = credit.Name;

                    if (this.Dark)
                    {
                        newCreditsLabel.GetComponent<UILabel>().color = new Color(.5f, 0, 0, 1);
                    }

					this.ID++;
				}
				
				this.Timer += Time.deltaTime * Speed;

				if (this.Timer >= this.TimerLimit)
				{
					this.Timer = 0.0f;
				}
			}

			if (Input.GetButtonDown(InputNames.Xbox_B) || Jukebox.time >= Jukebox.clip.length)
			{
				this.FadeOut = true;
			}
		}

		if (this.FadeOut)
		{
			this.Darkness.color = new Color(
				this.Darkness.color.r,
				this.Darkness.color.g,
				this.Darkness.color.b,
				Mathf.MoveTowards(this.Darkness.color.a, 1.0f, Time.deltaTime));

			Jukebox.volume -= Time.deltaTime;

			if (this.Darkness.color.a == 1.0f)
			{
				if (this.Darkness.color.r == 1)
				{
					SceneManager.LoadScene(SceneNames.TitleScene);
				}
				else
				{
					SceneManager.LoadScene(SceneNames.PostCreditsScene);
				}
			}
		}

		bool slower = Input.GetKeyDown(KeyCode.Minus);
		bool faster = Input.GetKeyDown(KeyCode.Equals);

		if (slower)
		{
			Time.timeScale -= 1.0f;
		}
		else if (faster)
		{
			Time.timeScale += 1.0f;
		}

		if (slower || faster)
		{
			Jukebox.pitch = Time.timeScale;
		}
	}
}