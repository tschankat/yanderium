using UnityEngine;

public class DemonPortalScript : MonoBehaviour
{
	public YandereScript Yandere;
	public JukeboxScript Jukebox;
	public PromptScript Prompt;
	public ClockScript Clock;

	public AudioSource DemonRealmAudio;

	public GameObject HeartbeatCamera;
	public GameObject DarkAura;
	public GameObject FPS;
	public GameObject HUD;

	public UISprite Darkness;

	public float Timer = 0.0f;

	void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Yandere.Character.GetComponent<Animation>().CrossFade(this.Yandere.IdleAnim);
			this.Yandere.CanMove = false;
			Instantiate(this.DarkAura,
				this.Yandere.transform.position + (Vector3.up * 0.81f), Quaternion.identity);
			this.Timer += Time.deltaTime;
		}

		// [af] Replaced if/else statement with ternary expression.
		this.DemonRealmAudio.volume = Mathf.MoveTowards(
			this.DemonRealmAudio.volume,
			(this.Yandere.transform.position.y > 1000.0f) ? 0.50f : 0.0f,
			Time.deltaTime * 0.10f);

		if (this.Timer > 0.0f)
		{
			if (this.Yandere.transform.position.y > 1000.0f)
			{
				this.Timer += Time.deltaTime;

				if (this.Timer > 4.0f)
				{
					this.Darkness.color = new Color(
						this.Darkness.color.r,
						this.Darkness.color.g,
						this.Darkness.color.b,
						Mathf.MoveTowards(this.Darkness.color.a, 1.0f, Time.deltaTime));

					if (this.Darkness.color.a == 1.0f)
					{
						this.Yandere.transform.position = new Vector3(12.0f, 0.0f, 28.0f);
						this.Yandere.Character.SetActive(true);
						this.Yandere.SetAnimationLayers();

						this.HeartbeatCamera.SetActive(true);
						this.FPS.SetActive(true);
						this.HUD.SetActive(true);
					}
				}
				else if (this.Timer > 1.0f)
				{
					this.Yandere.Character.SetActive(false);
				}
			}
			else
			{
				this.Jukebox.Volume = Mathf.MoveTowards(
					this.Jukebox.Volume, 0.50f, Time.deltaTime * 0.50f);

				if (this.Jukebox.Volume == 0.50f)
				{
					this.Darkness.color = new Color(
						this.Darkness.color.r,
						this.Darkness.color.g,
						this.Darkness.color.b,
						Mathf.MoveTowards(this.Darkness.color.a, 0.0f, Time.deltaTime));

					if (this.Darkness.color.a == 0.0f)
					{
						this.transform.parent.gameObject.SetActive(false);
						this.Darkness.enabled = false;
						this.Yandere.CanMove = true;
						this.Clock.StopTime = false;
						this.Timer = 0.0f;
					}
				}
			}
		}
	}
}
