using UnityEngine;

using XInputDotNetPure;

public class CameraEffectsScript : MonoBehaviour
{
	public YandereScript Yandere;

	public Vignetting Vignette;

	public UITexture MurderStreaks;
	public UITexture Streaks;
	public Bloom AlarmBloom;

	public float EffectStrength = 0.0f;
	public float VibrationTimer = 0.0f;

	public Bloom QualityBloom;
	public Vignetting QualityVignetting;
	public AntialiasingAsPostEffect QualityAntialiasingAsPostEffect;

	public bool VibrationCheck = false;
	public bool OneCamera = false;

	public AudioClip MurderNoticed;
	public AudioClip SenpaiNoticed;
	public AudioClip Noticed;

	void Start()
	{
		this.MurderStreaks.color = new Color(
			this.MurderStreaks.color.r,
			this.MurderStreaks.color.g,
			this.MurderStreaks.color.b,
			0.0f);

		this.Streaks.color = new Color(
			this.Streaks.color.r,
			this.Streaks.color.g,
			this.Streaks.color.b,
			0.0f);
	}

	void Update()
	{
		if (this.VibrationCheck)
		{
			this.VibrationTimer = Mathf.MoveTowards(this.VibrationTimer, 0, Time.deltaTime);

			if (this.VibrationTimer == 0)
			{
				GamePad.SetVibration(0, 0, 0);
				this.VibrationCheck = false;
			}
		}

		if (this.Streaks.color.a > 0.0f)
		{
			this.AlarmBloom.bloomIntensity -= Time.deltaTime;

			this.Streaks.color = new Color(
				this.Streaks.color.r,
				this.Streaks.color.g,
				this.Streaks.color.b,
				this.Streaks.color.a - Time.deltaTime);

			if (this.Streaks.color.a <= 0.0f)
			{
				this.AlarmBloom.enabled = false;
			}
		}

		if (this.MurderStreaks.color.a > 0.0f)
		{
			this.MurderStreaks.color = new Color(
				this.MurderStreaks.color.r,
				this.MurderStreaks.color.g,
				this.MurderStreaks.color.b,
				this.MurderStreaks.color.a - Time.deltaTime);
		}

		this.EffectStrength = 1.0f - (this.Yandere.Sanity * 0.010f);

		this.Vignette.intensity = Mathf.Lerp(
			this.Vignette.intensity, this.EffectStrength * 5.0f, Time.deltaTime);

		this.Vignette.blur = Mathf.Lerp(
			this.Vignette.blur, this.EffectStrength, Time.deltaTime);

		this.Vignette.chromaticAberration = Mathf.Lerp(
			this.Vignette.chromaticAberration, this.EffectStrength * 5.0f, Time.deltaTime);
	}

	public void Alarm()
	{
		GamePad.SetVibration(0, 1, 1);
		this.VibrationCheck = true;
		this.VibrationTimer = .1f;

		this.AlarmBloom.bloomIntensity = 1.0f;

		this.Streaks.color = new Color(
			this.Streaks.color.r,
			this.Streaks.color.g,
			this.Streaks.color.b,
			1.0f);

		this.AlarmBloom.enabled = true;

		this.Yandere.Jukebox.SFX.PlayOneShot(this.Noticed);
	}

	public void MurderWitnessed()
	{
		GamePad.SetVibration(0, 1, 1);
		this.VibrationCheck = true;
		this.VibrationTimer = .1f;

		this.MurderStreaks.color = new Color(
			this.MurderStreaks.color.r,
			this.MurderStreaks.color.g,
			this.MurderStreaks.color.b,
			1.0f);

		// [af] Replaced if/else statement with ternary expression.
		this.Yandere.Jukebox.SFX.PlayOneShot(
			this.Yandere.Noticed ? this.SenpaiNoticed : this.MurderNoticed);
	}

	public void DisableCamera()
	{
		if (!this.OneCamera)
		{
			this.OneCamera = true;
		}
		else
		{
			this.OneCamera = false;
		}
	}
}
