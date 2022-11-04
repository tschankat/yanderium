using UnityEngine;

public class DelinquentManagerScript : MonoBehaviour
{
	public GameObject Delinquents;
	public GameObject RapBeat;
	public GameObject Panel;

	public float[] NextTime;

	public DelinquentScript Attacker;
	public MirrorScript Mirror;

	public UILabel TimeLabel;

	public ClockScript Clock;

	public UISprite Circle;

	public float SpeechTimer = 0.0f;
	public float TimerMax = 0.0f;
	public float Timer = 0.0f;

	public bool Aggro = false;

	public int Phase = 1;

	//var RoundedTime = 0;
	//var Minutes = 0;
	//var Seconds = 0;

	void Start()
	{
		this.Delinquents.SetActive(false);

		this.TimerMax = 15.0f;
		this.Timer = 15.0f;
		this.Phase++;
	}

	void Update()
	{
		this.SpeechTimer = Mathf.MoveTowards(this.SpeechTimer, 0.0f, Time.deltaTime);

		if (this.Attacker != null)
		{
			if (!this.Attacker.Attacking)
			{
				if (this.Attacker.ExpressedSurprise)
				{
					if (this.Attacker.Run)
					{
						if (!this.Aggro)
						{
							AudioSource audioSource = this.GetComponent<AudioSource>();
							audioSource.clip = this.Attacker.AggroClips[Random.Range(0, this.Attacker.AggroClips.Length)];
							audioSource.Play();

							this.Aggro = true;
						}
					}
				}
			}
		}

		if (this.Panel.activeInHierarchy)
		{
			if (this.Clock.HourTime > this.NextTime[this.Phase])
			{
				if ((this.Phase == 3) && (this.Clock.HourTime > 7.25f))
				{
					this.TimerMax = 75.0f;
					this.Timer = 75.0f;
					this.Phase++;
				}
				else if ((this.Phase == 5) && (this.Clock.HourTime > 8.50f))
				{
					this.TimerMax = 285.0f;
					this.Timer = 285.0f;
					this.Phase++;
				}
				else if ((this.Phase == 7) && (this.Clock.HourTime > 13.25f))
				{
					this.TimerMax = 15.0f;
					this.Timer = 15.0f;
					this.Phase++;
				}
				else if ((this.Phase == 9) && (this.Clock.HourTime > 13.50f))
				{
					this.TimerMax = 135.0f;
					this.Timer = 135.0f;
					this.Phase++;
				}

				if (Attacker == null)
				{
					this.Timer -= Time.deltaTime * (this.Clock.TimeSpeed / 60.0f);
				}

				this.Circle.fillAmount = 1.0f - (this.Timer / this.TimerMax);

				if (this.Timer <= 0.0f)
				{
					// [af] Replaced if/else statement with boolean expression.
					this.Delinquents.SetActive(!this.Delinquents.activeInHierarchy);

					if (this.Phase < 8)
					{
						this.Phase++;
					}
					else
					{
						this.Delinquents.SetActive(false);
						this.Panel.SetActive(false);
					}
				}

				// [af] Commented in JS code.
				//RoundedTime = Mathf.CeilToInt(Timer * 60);

				//Minutes = RoundedTime / 60;
				//Seconds = RoundedTime % 60;

				//TimeLabel.text = String.Format ("{0:00}:{1:00}", Minutes, Seconds);
			}
		}
	}

	public void CheckTime()
	{
		if (this.Clock.HourTime < 13.0f)
		{
			this.Delinquents.SetActive(false);
			this.TimerMax = 15.0f;
			this.Timer = 15.0f;
			this.Phase = 6;
		}
		else if (this.Clock.HourTime < 15.50f)
		{
			this.Delinquents.SetActive(false);
			this.TimerMax = 15.0f;
			this.Timer = 15.0f;
			this.Phase = 8;
		}
	}

	public void EasterEgg()
	{
		this.RapBeat.SetActive(true);
		this.Mirror.Limit++;
	}
}
