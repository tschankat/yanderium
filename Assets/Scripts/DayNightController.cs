using UnityEngine;

/// <summary>
/// Implements a Day/Night cycle relative to the game world, with a World-Time clock, and optional Direcitonal Light control.
/// </summary>
/// <!-- 
/// Version 0.0.1.0 (beta)
/// By Reed Kimble
/// Last Revision 5/19/2011
/// 
/// Updated by tinyBuild for Yandere Simulator in Unity 5, 5/25/2017.
/// -->
/// <remarks>
/// Add this script to a new GameObject to create a Day/Night cycle for the scene. The day/night cycle effect is achieved by modifying the
/// scene ambient light color, fog color, and skybox material.  The script will also rotate, fade, and enable/disable a directional
/// light if one is attached to the same GameObject as the DayNightController script.  The length of a complete day (in seconds) and the number of
/// hours per day are modifiable in the script fields and allow calculation of the World-time hour-of-day.  Each 'phase' of the day is considered
/// to be 1/4 of the dayCycleLength.
/// 
/// Note that the script will rotate the GameObject transform it is attached to, even if no directional light is attached. You will probably want to 
/// use a dedicated GameObject for this script and any attached directional light.
/// 
/// The GameObject with this script should be placed roughly in the center of your scene, with a height of about 2/3 the length (or width) of the terrain.
/// If that GameObject has a light, it should be a directional light pointed straight down (x:90, y:0, z:0).  This light will then be rotated around its
/// x-axis (relative to the scene; eg. as if you used the rotation tool locked on the green x axis) and will reach its horizontal peeks during the
/// end of dusk and beginning of dawn, turning off during the night (upside-down rotation).
/// 
/// The reset command will attempt to use the default skybox assets DawnDusk, Sunny2, and StarryNight if that package has been imported.  The
/// command will also choose acceptable color values and set the day cycle to two minutes. It is suggested that the directional light be a light-
/// yellow or peach in color with a roughly 0.33f intensity.  The script will not set any default values for the light, if one exists, so the light
/// must be configured manually.
/// </remarks>
public class DayNightController : MonoBehaviour
{
	public enum DayPhase
	{
		Night = 0,
		Dawn = 1,
		Day = 2,
		Dusk = 3
	}

	/// <summary>
	/// The number of real-world seconds in one game day.
	/// </summary>
	public float dayCycleLength;

	/// <summary>
	/// The current time within the day cycle. Modify to change the World Time.
	/// </summary>
	public float currentCycleTime;

	//Would be the amount of time the sky takes to transition if UpdateSkybox were used.
	//public float skyTransitionTime;

	/// <summary>
	/// The current 'phase' of the day; Dawn, Day, Dusk, or Night
	/// </summary>
	public DayPhase currentPhase;

	/// <summary>
	/// The number of hours per day used in the WorldHour time calculation.
	/// </summary>
	public float hoursPerDay;

	/// <summary>
	/// Dawn occurs at currentCycleTime = 0.0f, so this offsets the WorldHour time to make
	/// dawn occur at a specified hour. A value of 3 results in a 5am dawn for a 24 hour world clock.
	/// </summary>
	public float dawnTimeOffset;

	/// <summary>
	/// The calculated hour of the day, based on the hoursPerDay setting. Do not set this value.
	/// Change the time of day by calculating and setting the currentCycleTime.
	/// </summary>
	public int worldTimeHour;

	/// <summary>
	/// The scene ambient color used for full daylight.
	/// </summary>
	public Color fullLight;

	/// <summary>
	/// The scene ambient color used for full night.
	/// </summary>
	public Color fullDark;

	/// <summary>
	/// The scene skybox material to use at dawn and dusk.
	/// </summary>
	public Material dawnDuskSkybox;

	/// <summary>
	/// The scene fog color to use at dawn and dusk.
	/// </summary>
	public Color dawnDuskFog;

	/// <summary>
	/// The scene skybox material to use during the day.
	/// </summary>
	public Material daySkybox;

	/// <summary>
	/// The scene fog color to use during the day.
	/// </summary>
	public Color dayFog;

	/// <summary>
	/// The scene skybox material to use at night.
	/// </summary>
	public Material nightSkybox;

	/// <summary>
	/// The scene fog color to use at night.
	/// </summary>
	public Color nightFog;

	/// <summary>
	/// The calculated time at which dawn occurs based on 1/4 of dayCycleLength.
	/// </summary>
	private float dawnTime;

	/// <summary>
	/// The calculated time at which day occurs based on 1/4 of dayCycleLength.
	/// </summary>
	private float dayTime;

	/// <summary>
	/// The calculated time at which dusk occurs based on 1/4 of dayCycleLength.
	/// </summary>
	private float duskTime;

	/// <summary>
	/// The calculated time at which night occurs based on 1/4 of dayCycleLength.
	/// </summary>
	private float nightTime;

	/// <summary>
	/// One quarter the value of dayCycleLength.
	/// </summary>
	private float quarterDay;

	//Would be the amount of time remaining in the skybox transition if UpdateSkybox were used.
	//private float remainingTransition;

	/// <summary>
	/// The specified intensity of the directional light, if one exists. This value will be
	/// faded to 0 during dusk, and faded from 0 back to this value during dawn.
	/// </summary>
	private float lightIntensity;

	/// <summary>
	/// Initializes working variables and performs starting calculations.
	/// </summary>
	void Initialize()
	{
		//remainingTransition = skyTransitionTime; //Would indicate that the game should start with an active transition, if UpdateSkybox were used.
		this.quarterDay = this.dayCycleLength * 0.25f;
		this.dawnTime = 0.0f;
		this.dayTime = this.dawnTime + this.quarterDay;
		this.duskTime = this.dayTime + this.quarterDay;
		this.nightTime = this.duskTime + this.quarterDay;

		Light light = this.GetComponent<Light>();

		if (light != null)
		{
			this.lightIntensity = light.intensity;
		}
	}

	/// <summary>
	/// Sets the script control fields to reasonable default values for an acceptable day/night cycle effect.
	/// </summary>
	void Reset()
	{
		this.dayCycleLength = 120.0f;
		//skyTransitionTime = 3.0f; //would be set if UpdateSkybox were used.
		this.hoursPerDay = 24.0f;
		this.dawnTimeOffset = 3.0f;
		this.fullDark = new Color(32.0f / 255.0f, 28.0f / 255.0f, 46.0f / 255.0f);
		this.fullLight = new Color(253.0f / 255.0f, 248.0f / 255.0f, 223.0f / 255.0f);
		this.dawnDuskFog = new Color(133.0f / 255.0f, 124.0f / 255.0f, 102.0f / 255.0f);
		this.dayFog = new Color(180.0f / 255.0f, 208.0f / 255.0f, 209.0f / 255.0f);
		this.nightFog = new Color(12.0f / 255.0f, 15.0f / 255.0f, 91.0f / 255.0f);

		Skybox[] skyboxes = Resources.FindObjectsOfTypeAll<Skybox>();
		foreach (Skybox box in skyboxes)
		{
			if (box.name == "DawnDusk Skybox")
			{
				this.dawnDuskSkybox = box.material;
			}
			else if (box.name == "StarryNight Skybox")
			{
				this.nightSkybox = box.material;
			}
			else if (box.name == "Sunny2 Skybox")
			{
				this.daySkybox = box.material;
			}
		}
	}

	// Use this for initialization
	void Start()
	{
		this.Initialize();
	}

	// Update is called once per frame
	void Update()
	{
		// Rudementary phase-check algorithm:
		if ((this.currentCycleTime > this.nightTime) && (this.currentPhase == DayPhase.Dusk))
		{
			this.SetNight();
		}
		else if ((this.currentCycleTime > this.duskTime) && (this.currentPhase == DayPhase.Day))
		{
			this.SetDusk();
		}
		else if ((this.currentCycleTime > this.dayTime) && (this.currentPhase == DayPhase.Dawn))
		{
			this.SetDay();
		}
		else if ((this.currentCycleTime > this.dawnTime) &&
			(this.currentCycleTime < this.dayTime) && (this.currentPhase == DayPhase.Night))
		{
			this.SetDawn();
		}

		// Perform standard updates:
		this.UpdateWorldTime();
		this.UpdateDaylight();
		this.UpdateFog();
		//UpdateSkybox(); //would be called if UpdateSkybox were used.

		// Update the current cycle time:
		this.currentCycleTime += Time.deltaTime;
		this.currentCycleTime = this.currentCycleTime % this.dayCycleLength;
	}

	/// <summary>
	/// Sets the currentPhase to Dawn, turning on the directional light, if any.
	/// </summary>
	public void SetDawn()
	{
		RenderSettings.skybox = this.dawnDuskSkybox; //would be commented out or removed if UpdateSkybox were used.													 
													 //remainingTransition = skyTransitionTime; //would be set if UpdateSkybox were used.

		Light light = this.GetComponent<Light>();

		if (light != null)
		{
			light.enabled = true;
		}

		this.currentPhase = DayPhase.Dawn;
	}

	/// <summary>
	/// Sets the currentPhase to Day, ensuring full day color ambient light, and full
	/// directional light intensity, if any.
	/// </summary>
	public void SetDay()
	{
		RenderSettings.skybox = this.daySkybox; //would be commented out or removed if UpdateSkybox were used.										   
												//remainingTransition = skyTransitionTime; //would be set if UpdateSkybox were used.

		RenderSettings.ambientLight = this.fullLight;
		Light light = this.GetComponent<Light>();

		if (light != null)
		{
			light.intensity = this.lightIntensity;
		}

		this.currentPhase = DayPhase.Day;
	}

	/// <summary>
	/// Sets the currentPhase to Dusk.
	/// </summary>
	public void SetDusk()
	{
		RenderSettings.skybox = this.dawnDuskSkybox; //would be commented out or removed if UpdateSkybox were used.
													 //remainingTransition = skyTransitionTime; //would be set if UpdateSkybox were used.
		this.currentPhase = DayPhase.Dusk;
	}

	/// <summary>
	/// Sets the currentPhase to Night, ensuring full night color ambient light, and
	/// turning off the directional light, if any.
	/// </summary>
	public void SetNight()
	{
		RenderSettings.skybox = this.nightSkybox; //would be commented out or removed if UpdateSkybox were used.
												  //remainingTransition = skyTransitionTime; //would be set if UpdateSkybox were used.
		RenderSettings.ambientLight = this.fullDark;

		Light light = this.GetComponent<Light>();

		if (light != null)
		{
			light.enabled = false;
		}

		this.currentPhase = DayPhase.Night;
	}

	/// <summary>
	/// If the currentPhase is dawn or dusk, this method adjusts the ambient light color and direcitonal
	/// light intensity (if any) to a percentage of full dark or full light as appropriate. Regardless
	/// of currentPhase, the method also rotates the transform of this component, thereby rotating the
	/// directional light, if any.
	/// </summary>
	private void UpdateDaylight()
	{
		if (this.currentPhase == DayPhase.Dawn)
		{
			float relativeTime = this.currentCycleTime - this.dawnTime;
			RenderSettings.ambientLight = Color.Lerp(
				this.fullDark, this.fullLight, relativeTime / this.quarterDay);

			Light light = this.GetComponent<Light>();

			if (light != null)
			{
				light.intensity = this.lightIntensity * (relativeTime / this.quarterDay);
			}
		}
		else if (this.currentPhase == DayPhase.Dusk)
		{
			float relativeTime = this.currentCycleTime - this.duskTime;
			RenderSettings.ambientLight = Color.Lerp(
				this.fullLight, this.fullDark, relativeTime / this.quarterDay);

			Light light = this.GetComponent<Light>();

			if (light != null)
			{
				light.intensity = this.lightIntensity *
					((this.quarterDay - relativeTime) / this.quarterDay);
			}
		}

		this.transform.Rotate(Vector3.up *
			((Time.deltaTime / this.dayCycleLength) * 360.0f), Space.Self);
	}

	/// <summary>
	/// Interpolates the fog color between the specified phase colors during each phase's transition.
	/// eg. From DawnDusk to Day, Day to DawnDusk, DawnDusk to Night, and Night to DawnDusk
	/// </summary>
	private void UpdateFog()
	{
		if (this.currentPhase == DayPhase.Dawn)
		{
			float relativeTime = this.currentCycleTime - this.dawnTime;
			RenderSettings.fogColor = Color.Lerp(
				this.dawnDuskFog, this.dayFog, relativeTime / this.quarterDay);
		}
		else if (this.currentPhase == DayPhase.Day)
		{
			float relativeTime = this.currentCycleTime - this.dayTime;
			RenderSettings.fogColor = Color.Lerp(
				this.dayFog, this.dawnDuskFog, relativeTime / this.quarterDay);
		}
		else if (this.currentPhase == DayPhase.Dusk)
		{
			float relativeTime = this.currentCycleTime - this.duskTime;
			RenderSettings.fogColor = Color.Lerp(
				this.dawnDuskFog, this.nightFog, relativeTime / this.quarterDay);
		}
		else if (this.currentPhase == DayPhase.Night)
		{
			float relativeTime = this.currentCycleTime - this.nightTime;
			RenderSettings.fogColor = Color.Lerp(
				this.nightFog, this.dawnDuskFog, relativeTime / this.quarterDay);
		}
	}

	//Not yet implemented, but would be nice to allow a smoother transition of the Skybox material.
	//private void UpdateSkybox()
	//{
	//    if (remainingTransition > 0.0f)
	//    {
	//        if (currentPhase == DayCycle.Dawn)
	//        {
	//            //RenderSettings.skybox.Lerp(dawnDuskSkybox, nightSkybox, remainingTransition / skyTransitionTime);
	//        }
	//        if (currentPhase == DayCycle.Day)
	//        {

	//        }
	//        if (currentPhase == DayCycle.Dusk)
	//        {

	//        }
	//        if (currentPhase == DayCycle.Night)
	//        {

	//        }
	//        remainingTransition -= Time.deltaTime;
	//    }
	//}

	/// <summary>
	/// Updates the World-time hour based on the current time of day.
	/// </summary>
	private void UpdateWorldTime()
	{
		this.worldTimeHour = (int)((Mathf.Ceil((this.currentCycleTime / this.dayCycleLength) *
			this.hoursPerDay) + this.dawnTimeOffset) % this.hoursPerDay) + 1;
	}
}
