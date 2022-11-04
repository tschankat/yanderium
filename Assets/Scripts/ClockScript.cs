using System;
using UnityEngine;

public class ClockScript : MonoBehaviour
{
	private string MinuteNumber = string.Empty;
	private string HourNumber = string.Empty;

	public Collider MeetingRoomTrespassZone;

	public Collider[] TrespassZones;

	public StudentManagerScript StudentManager;
	public LoveManagerScript LoveManager;
	public YandereScript Yandere;
	public PoliceScript Police;
	public ClockScript Clock;
	public Bloom BloomEffect;
	public MotionBlur Blur;

	public Transform PromptParent;
	public Transform MinuteHand;
	public Transform HourHand;
	public Transform Sun;

	public GameObject SunFlare;

	public UILabel PeriodLabel;
	public UILabel TimeLabel;
	public UILabel DayLabel;

	public Light MainLight;

	public float HalfwayTime = 0.0f;
	public float PresentTime = 0.0f;
	public float TargetTime = 0.0f;
	public float StartTime = 0.0f;
	public float HourTime = 0.0f;

	public float AmbientLightDim = 0.0f;
	public float CameraTimer = 0.0f;
	public float DayProgress = 0.0f;
	public float LastMinute = 0.0f;
	public float StartHour = 0.0f;
	public float TimeSpeed = 0.0f;
	public float Minute = 0.0f;
	public float Timer = 0.0f;
	public float Hour = 0.0f;

	public PhaseOfDay Phase;

	public int Period = 0;
	public int Weekday;
	public int ID = 0;

	public string TimeText = string.Empty;

	public bool IgnorePhotographyClub = false;
	public bool LateStudent = false;
	public bool UpdateBloom = false;
	public bool MissionMode = false;
	public bool StopTime = false;
	public bool TimeSkip = false;
	public bool FadeIn = false;
	public bool Horror = false;

	public AudioSource SchoolBell;

	public Color SkyboxColor;

	void Start ()
	{
		RenderSettings.ambientLight = new Color(.75f, .75f, .75f);

		this.PeriodLabel.text = "BEFORE CLASS";
		this.PresentTime = this.StartHour * 60.0f;

		if (PlayerPrefs.GetInt ("LoadingSave") == 1)
		{
			int SaveProfile = GameGlobals.Profile;
			int SaveSlot = PlayerPrefs.GetInt("SaveSlot");

            /*
			Debug.Log("Loading time! Profile_" + SaveProfile + "_Slot_" + SaveSlot + "_Time" + " is " + PlayerPrefs.GetFloat("Profile_" + SaveProfile + "_Slot_" + SaveSlot + "_Time"));

			this.PresentTime = PlayerPrefs.GetFloat("Profile_" + SaveProfile + "_Slot_" + SaveSlot + "_Time");
            */

            this.Weekday = PlayerPrefs.GetInt("Profile_" + SaveProfile + "_Slot_" + SaveSlot + "_Weekday");

				 if (Weekday == 1){DateGlobals.Weekday = DayOfWeek.Monday;}
			else if (Weekday == 2){DateGlobals.Weekday = DayOfWeek.Tuesday;}
			else if (Weekday == 3){DateGlobals.Weekday = DayOfWeek.Wednesday;}
			else if (Weekday == 4){DateGlobals.Weekday = DayOfWeek.Thursday;}
			else if (Weekday == 5){DateGlobals.Weekday = DayOfWeek.Friday;}
		}

		if (DateGlobals.Weekday == DayOfWeek.Sunday)
		{
			DateGlobals.Weekday = DayOfWeek.Monday;
		}

		if (!SchoolGlobals.SchoolAtmosphereSet)
		{
			SchoolGlobals.SchoolAtmosphereSet = true;
			SchoolGlobals.SchoolAtmosphere = 1.0f;
		}

		if (SchoolGlobals.SchoolAtmosphere < 0.50f)
		{
			this.BloomEffect.bloomIntensity = 0.2f;
			this.BloomEffect.bloomThreshhold = 0.0f;
			this.Police.Darkness.enabled = true;

			this.Police.Darkness.color = new Color(
				this.Police.Darkness.color.r,
				this.Police.Darkness.color.g,
				this.Police.Darkness.color.b,
				1.0f);

			this.FadeIn = true;
		}
		else
		{
			this.BloomEffect.bloomIntensity = 10f;
			this.BloomEffect.bloomThreshhold = 0.0f;
			this.UpdateBloom = true;
		}

		this.BloomEffect.bloomThreshhold = 0.0f;

		this.DayLabel.text = this.GetWeekdayText(DateGlobals.Weekday);

		this.MainLight.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		RenderSettings.ambientLight = new Color(0.75f, 0.75f, 0.75f, 1.0f);
		RenderSettings.skybox.SetColor("_Tint", new Color(0.50f, 0.50f, 0.50f));

		if (ClubGlobals.GetClubClosed(ClubType.Photography) || 
			StudentGlobals.GetStudentGrudge(56) ||
			StudentGlobals.GetStudentGrudge(57) ||
			StudentGlobals.GetStudentGrudge(58) ||
			StudentGlobals.GetStudentGrudge(59) ||
			StudentGlobals.GetStudentGrudge(60))
		{
			this.IgnorePhotographyClub = true;
		}

		this.MissionMode = MissionModeGlobals.MissionMode;

		this.HourTime = this.PresentTime / 60.0f;
		this.Hour = Mathf.Floor(this.PresentTime / 60.0f);
		this.Minute = Mathf.Floor(((this.PresentTime / 60.0f) - Hour) * 60.0f);

		this.UpdateClock();
	}

	void Update()
	{
		if (this.FadeIn)
		{
			if (Time.deltaTime < 1.0f)
			{
				this.Police.Darkness.color = new Color(
					this.Police.Darkness.color.r,
					this.Police.Darkness.color.g,
					this.Police.Darkness.color.b,
					Mathf.MoveTowards(this.Police.Darkness.color.a, 0.0f, Time.deltaTime));

				if (this.Police.Darkness.color.a == 0.0f)
				{
					this.Police.Darkness.enabled = false;
					this.FadeIn = false;
				}
			}
		}

		if (!this.MissionMode)
		{
			if (this.CameraTimer < 1)
			{
				this.CameraTimer += Time.deltaTime;

				if (this.CameraTimer > 1)
				{
					if (!this.StudentManager.MemorialScene.enabled)
					{
						this.Yandere.RPGCamera.enabled = true;
						this.Yandere.CanMove = true;
					}
				}
			}
		}

		if (this.PresentTime < 1080.0f)
		{
			if (this.UpdateBloom)
			{
				this.BloomEffect.bloomIntensity = Mathf.MoveTowards(this.BloomEffect.bloomIntensity, .2f, Time.deltaTime * 5f);
				//this.BloomEffect.bloomThreshhold = Mathf.MoveTowards(this.BloomEffect.bloomThreshhold, 0, Time.deltaTime);

				if (BloomEffect.bloomIntensity == .2f)
				{
					this.UpdateBloom = false;
				}
			}
		}
		else
		{
			if (this.LoveManager.WaitingToConfess)
			{
				if (!this.StopTime)
				{
					this.LoveManager.BeginConfession();
				}
			}
			else if (!this.Police.FadeOut)
			{
				if (!this.Yandere.Attacking && !this.Yandere.Struggling && !this.Yandere.DelinquentFighting &&
					!this.Yandere.Pickpocketing && !this.Yandere.Noticed)
				{
					this.Police.DayOver = true;

					this.Yandere.StudentManager.StopMoving();
					this.Police.Darkness.enabled = true;
					this.Police.FadeOut = true;
					this.StopTime = true;
				}
			}
		}

		if (!this.StopTime)
		{
			if (this.Period == 3)
			{
				this.PresentTime += (Time.deltaTime * (1.0f / 60.0f)) * this.TimeSpeed * 0.5f;
			}
			else
			{
				this.PresentTime += (Time.deltaTime * (1.0f / 60.0f)) * this.TimeSpeed;
			}
		}

		/*
		if (this.PresentTime > 1440.0f)
		{
			this.PresentTime -= 1440.0f;
		}
		*/

		this.HourTime = this.PresentTime / 60.0f;
		this.Hour = Mathf.Floor(this.PresentTime / 60.0f);
		this.Minute = Mathf.Floor(((this.PresentTime / 60.0f) - Hour) * 60.0f);

		if (this.Minute != this.LastMinute)
		{
			this.UpdateClock();
		}

		/////////////////////////////////////
		///// UPDATING THE ANALOG CLOCK /////
		/////////////////////////////////////

		this.MinuteHand.localEulerAngles = new Vector3(
			this.MinuteHand.localEulerAngles.x,
			this.MinuteHand.localEulerAngles.y,
			this.Minute * 6.0f);

		this.HourHand.localEulerAngles = new Vector3(
			this.HourHand.localEulerAngles.x,
			this.HourHand.localEulerAngles.y,
			this.Hour * 30.0f);

		///////////////////////////////////
		///// ACTIVATING LATE STUDENT /////
		///////////////////////////////////

		if (this.LateStudent)
		{
			if (this.HourTime > 7.9f)
			{
				this.ActivateLateStudent();
			}
		}

		///////////////////////////////////////
		///// CHANGING THE SCHOOL PERIODS /////
		///////////////////////////////////////

		//Entering before-class time.
		if (this.HourTime < 8.50f)
		{
			if (this.Period < 1)
			{
				this.PeriodLabel.text = "BEFORE CLASS";

				this.DeactivateTrespassZones();
				this.Period++;
			}
		}
		//Entering before-lunch class time.
		else if (this.HourTime < 13.0f)
		{
			if (this.Period < 2)
			{
				this.PeriodLabel.text = "CLASS TIME";

				this.ActivateTrespassZones();
				this.Period++;
			}
		}
		//Entering lunch time.
		else if (this.HourTime < 13.50f)
		{
			if (this.Period < 3)
			{
				this.PeriodLabel.text = "LUNCH TIME";

				this.StudentManager.DramaPhase = 0;
				this.StudentManager.UpdateDrama();
				this.DeactivateTrespassZones();
				this.Period++;
			}
		}
		//Entering after-lunch class time.
		else if (this.HourTime < 15.50f)
		{
			if (this.Period < 4)
			{
				this.PeriodLabel.text = "CLASS TIME";

				this.ActivateTrespassZones();
				this.Period++;
			}
		}
		//Entering Cleaning Time.
		else if (this.HourTime < 16.0f)
		{
			if (this.Period < 5)
			{
				foreach (GameObject graffiti in this.StudentManager.Graffiti)
				{
					if (graffiti != null)
					{
						graffiti.SetActive(false);
					}
				}

				this.PeriodLabel.text = "CLEANING TIME";

				this.DeactivateTrespassZones();

				if (Weekday == 5)
				{
					this.MeetingRoomTrespassZone.enabled = true;
				}

				this.Period++;
			}
		}
		else
		{
			if (this.Period < 6)
			{
				this.PeriodLabel.text = "AFTER SCHOOL";

				this.StudentManager.DramaPhase = 0;
				this.StudentManager.UpdateDrama();
				this.Period++;
			}
		}

		if (!this.IgnorePhotographyClub)
		{
			if (this.HourTime > 16.75f)
			{
				if (this.StudentManager.SleuthPhase < 4)
				{
					this.StudentManager.SleuthPhase = 3;
					this.StudentManager.UpdateSleuths();
				}
			}
		}

		/////////////////////////////////
		///// CHANGING THE LIGHTING /////
		/////////////////////////////////

		this.Sun.eulerAngles = new Vector3(
			this.Sun.eulerAngles.x,
			this.Sun.eulerAngles.y,
			-45.0f + ((90.0f * (this.PresentTime - 420.0f)) / 660.0f));

		if (!this.Horror)
		{
			if (this.StudentManager.WestBathroomArea.bounds.Contains(Yandere.transform.position) ||
				this.StudentManager.EastBathroomArea.bounds.Contains(Yandere.transform.position))
			{
				this.AmbientLightDim = Mathf.MoveTowards(this.AmbientLightDim, .1f, Time.deltaTime);
			}
			else
			{
				this.AmbientLightDim = Mathf.MoveTowards(this.AmbientLightDim, .75f, Time.deltaTime);
			}

			if (this.PresentTime > 930.0f)
			{
				this.DayProgress = (this.PresentTime - 930.0f) / 150.0f;

				this.MainLight.color = new Color(
					1.0f - ((1.0f - (217.0f / 255.0f)) * this.DayProgress),
					1.0f - ((1.0f - (152.0f / 255.0f)) * this.DayProgress),
					1.0f - ((1.0f - (74.0f / 255.0f)) * this.DayProgress));

				RenderSettings.ambientLight = new Color(
					1.0f - ((1.0f - (217.0f / 255.0f)) * this.DayProgress) - (1.0f - this.AmbientLightDim) * (1.0f - this.DayProgress),
					1.0f - ((1.0f - (152.0f / 255.0f)) * this.DayProgress) - (1.0f - this.AmbientLightDim) * (1.0f - this.DayProgress),
					1.0f - ((1.0f - (74.0f / 255.0f)) * this.DayProgress) - (1.0f - this.AmbientLightDim) * (1.0f - this.DayProgress));

				this.SkyboxColor = new Color(
					1.0f - ((1.0f - (217.0f / 255.0f)) * this.DayProgress) - 0.50f * (1.0f - this.DayProgress),
					1.0f - ((1.0f - (152.0f / 255.0f)) * this.DayProgress) - 0.50f * (1.0f - this.DayProgress),
					1.0f - ((1.0f - (74.0f / 255.0f)) * this.DayProgress) - 0.50f * (1.0f - this.DayProgress));

				RenderSettings.skybox.SetColor("_Tint",
					new Color(this.SkyboxColor.r, this.SkyboxColor.g, this.SkyboxColor.b));
			}
			else
			{
				RenderSettings.ambientLight = new Color(
					this.AmbientLightDim, this.AmbientLightDim, this.AmbientLightDim);
			}
		}

		////////////////////////
		///// PASSING TIME /////
		////////////////////////

		if (this.TimeSkip)
		{
			if (this.HalfwayTime == 0.0f)
			{
				this.HalfwayTime = this.PresentTime + ((this.TargetTime - this.PresentTime) * 0.50f);
				this.Yandere.TimeSkipHeight = this.Yandere.transform.position.y;
				this.Yandere.Phone.SetActive(true);
				this.Yandere.TimeSkipping = true;
				this.Yandere.CanMove = false;
				this.Blur.enabled = true;

				if (this.Yandere.Armed)
				{
					this.Yandere.Unequip();
				}
			}

			if (Time.timeScale < 25.0f)
			{
				Time.timeScale += 1.0f;
			}

			this.Yandere.Character.GetComponent<Animation>()[AnimNames.FemaleTimeSkip].speed = 1.0f / Time.timeScale;

			this.Blur.blurAmount = 0.92f * (Time.timeScale / 100.0f);

			if (this.PresentTime > this.TargetTime)
			{
				this.EndTimeSkip();
			}

			if ((this.Yandere.CameraEffects.Streaks.color.a > 0.0f) ||
				(this.Yandere.CameraEffects.MurderStreaks.color.a > 0.0f) ||
				this.Yandere.NearSenpai || Input.GetButtonDown(InputNames.Xbox_Start))
			{
				this.EndTimeSkip();
			}
		}

		#if UNITY_EDITOR
		if (Input.GetKeyDown("space"))
		{
			//NightLighting();
		}
		#endif
	}

	public void EndTimeSkip()
	{
        if (GameGlobals.AlphabetMode)
        {
            this.StopTime = true;
        }

		this.PromptParent.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		this.Yandere.Phone.SetActive(false);
		this.Yandere.TimeSkipping = false;
		this.Blur.enabled = false;
		Time.timeScale = 1.0f;
		this.TimeSkip = false;
		this.HalfwayTime = 0.0f;

		if (!this.Yandere.Noticed && !this.Police.FadeOut)
		{
			this.Yandere.CharacterAnimation.CrossFade(this.Yandere.IdleAnim);
			this.Yandere.CanMoveTimer = .5f;
		}
	}

	public string GetWeekdayText(DayOfWeek weekday)
	{
		if (weekday == DayOfWeek.Sunday)
		{
			Weekday = 0;
			return "SUNDAY";
		}
		else if (weekday == DayOfWeek.Monday)
		{
			Weekday = 1;
			return "MONDAY";
		}
		else if (weekday == DayOfWeek.Tuesday)
		{
			Weekday = 2;
			return "TUESDAY";
		}
		else if (weekday == DayOfWeek.Wednesday)
		{
			Weekday = 3;
			return "WEDNESDAY";
		}
		else if (weekday == DayOfWeek.Thursday)
		{
			Weekday = 4;
			return "THURSDAY";
		}
		else if (weekday == DayOfWeek.Friday)
		{
			Weekday = 5;
			return "FRIDAY";
		}
		else
		{
			Weekday = 6;
			return "SATURDAY";
		}
	}

	void ActivateTrespassZones()
	{
		if (!this.SchoolBell.isPlaying || this.SchoolBell.time > 1)
		{
			this.SchoolBell.Play();
		}

		// [af] Converted while loop to foreach loop.
		foreach (Collider zone in this.TrespassZones)
		{
			zone.enabled = true;
		}
	}

	public void DeactivateTrespassZones()
	{
		this.Yandere.Trespassing = false;

		if (!this.SchoolBell.isPlaying || this.SchoolBell.time > 1)
		{
			this.SchoolBell.Play();
		}

		// [af] Converted while loop to foreach loop.
		foreach (Collider zone in this.TrespassZones)
		{
			if (!zone.GetComponent<TrespassScript>().OffLimits)
			{
				zone.enabled = false;
			}
		}
	}

	public void ActivateLateStudent()
	{
		if (this.StudentManager.Students[7] != null)
		{
			this.StudentManager.Students[7].gameObject.SetActive(true);
			this.StudentManager.Students[7].Pathfinding.speed = 4;
			this.StudentManager.Students[7].Spawned = true;
			this.StudentManager.Students[7].Hurry = true;
		}

		this.LateStudent = false;
	}

	public void NightLighting()
	{
		this.MainLight.color = new Color(.25f, .25f, .5f);

		RenderSettings.ambientLight = new Color(.25f, .25f, .5f);

		this.SkyboxColor = new Color(.1f, .1f, .2f);

		RenderSettings.skybox.SetColor("_Tint", new Color(.1f, .1f, .2f));
	}

	//////////////////////////////////////
	///// UPDATING THE DIGITAL CLOCK /////
	//////////////////////////////////////

	public void UpdateClock()
	{
		this.LastMinute = this.Minute;

		//Debug.Log("Updating clock!");

		if (this.Hour == 0.0f || this.Hour == 12.0f)
		{
			this.HourNumber = "12";
		}
		else if (this.Hour < 12.0f)
		{
			this.HourNumber = this.Hour.ToString("f0");
		}
		else
		{
			this.HourNumber = (this.Hour - 12.0f).ToString("f0");
		}

		if (this.Minute < 10.0f)
		{
			this.MinuteNumber = "0" + this.Minute.ToString("f0");
		}
		else
		{
			this.MinuteNumber = this.Minute.ToString("f0");
		}

		this.TimeText = this.HourNumber + ":" + this.MinuteNumber +
			((this.Hour < 12.0f) ? " AM" : " PM");

		this.TimeLabel.text = this.TimeText;
	}
}