using UnityEngine;

public class RadioScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public JukeboxScript Jukebox;

	public GameObject RadioNotes;
	public GameObject AlarmDisc;

	public AudioSource MyAudio;

	public Renderer MyRenderer;

	public Texture OffTexture;
	public Texture OnTexture;

	public StudentScript Victim;
	public PromptScript Prompt;

	public float CooldownTimer = 0.0f;

	public bool Delinquent = false;

	public bool On = false;

	public int Proximity;
	public int ID;

	void Update()
	{
		if (this.transform.parent == null)
		{
			if (this.CooldownTimer > 0)
			{
				this.CooldownTimer = Mathf.MoveTowards (this.CooldownTimer, 0, Time.deltaTime);

				if (this.CooldownTimer == 0)
				{
					this.Prompt.enabled = true;
				}
			}
			else
			{
				UISprite circle = this.Prompt.Circle[0];

				if (circle.fillAmount == 0.0f) //|| Input.GetButtonDown(InputNames.Xbox_B))
				{
					circle.fillAmount = 1.0f;

					if (!this.On)
					{
						this.Prompt.Label[0].text = "     " + "Turn Off";

						this.MyRenderer.material.mainTexture = OnTexture;
						this.RadioNotes.SetActive(true);
                        this.MyAudio.Play();
                        this.On = true;
					}
					else
					{
						this.CooldownTimer = 1;
						this.TurnOff();
					}
				}
			}

			if (this.On)
			{
				if (this.Victim == null)
				{
					if (this.AlarmDisc != null)
					{
						GameObject Alarm = Instantiate(this.AlarmDisc,
							this.transform.position + Vector3.up, Quaternion.identity);

						AlarmDiscScript alarmDiscScript = Alarm.GetComponent<AlarmDiscScript>();
						alarmDiscScript.SourceRadio = this;
						alarmDiscScript.NoScream = true;
						alarmDiscScript.Radio = true;
					}
				}
			}
		}
		else
		{
			if (this.Prompt.enabled)
			{
				this.Prompt.enabled = false;
				this.Prompt.Hide();
			}
		}

		if (this.Delinquent)
		{
			this.Proximity = 0;
			this.ID = 1;

			while (this.ID < 6)
			{
				if (this.StudentManager.Students[75 + ID] != null)
				{
					if (Vector3.Distance(transform.position, this.StudentManager.Students[75 + ID].transform.position) < 1.1f)
					{
						if (!this.StudentManager.Students[75 + ID].Alarmed &&
							!this.StudentManager.Students[75 + ID].Threatened &&
							this.StudentManager.Students[75 + ID].Alive)
						{
							this.Proximity++;
						}
						else
						{
							this.Proximity = -100;
							this.ID = 5;

							this.MyAudio.Stop();
							this.Jukebox.ClubDip = 0;
						}
					}
				}

				ID++;
			}

			//Debug.Log("Proximity is " + this.Proximity);

			if (this.Proximity > 0)
			{
				if (!this.MyAudio.isPlaying)
				{
					this.MyAudio.Play();
				}

				float Distance = Vector3.Distance(this.Prompt.Yandere.transform.position, this.transform.position);

				if (Distance < 11)
				{
					this.Jukebox.ClubDip = Mathf.MoveTowards(this.Jukebox.ClubDip, ((10 - Distance) * .2f) * this.Jukebox.Volume, Time.deltaTime);

					if (this.Jukebox.ClubDip < 0){this.Jukebox.ClubDip = 0;}
					if (this.Jukebox.ClubDip > this.Jukebox.Volume){this.Jukebox.ClubDip = this.Jukebox.Volume;}
				}
			}
			else
			{
				if (this.MyAudio.isPlaying)
				{
					this.MyAudio.Stop();
					this.Jukebox.ClubDip = 0;
				}
			}
		}
	}

	public void TurnOff()
	{
		this.Prompt.Label[0].text = "     " + "Turn On";
		this.Prompt.enabled = false;
		this.Prompt.Hide();

		this.MyRenderer.material.mainTexture = OffTexture;
		this.RadioNotes.SetActive(false);
		this.CooldownTimer = 1;
        this.MyAudio.Stop();
        this.Victim = null;
		this.On = false;
	}
}