using UnityEngine;

public class SkullScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public VoidGoddessScript VoidGoddess;
	public JukeboxScript Jukebox;
	public YandereScript Yandere;
	public PromptScript Prompt;
	public ClockScript Clock;

	public AudioClip FlameDemonVoice;
	public AudioClip FlameActivation;

	public GameObject HeartbeatCamera;
	public GameObject RitualKnife;
	public GameObject EmptyDemon;
	public GameObject DebugMenu;
	public GameObject DarkAura;
	public GameObject Hell;
	public GameObject FPS;
	public GameObject HUD;

	public Vector3 OriginalPosition;
	public Vector3 OriginalRotation;

	public UISprite Darkness;

	public float FlameTimer = 0.0f;
	public float Timer = 0.0f;

	public bool MissionMode;

	void Start()
	{
		this.OriginalPosition = this.RitualKnife.transform.position;
		this.OriginalRotation = this.RitualKnife.transform.eulerAngles;

		this.MissionMode = MissionModeGlobals.MissionMode;
	}

	void Update()
	{
		if (this.Yandere.Armed)
		{
			if (this.Yandere.EquippedWeapon.WeaponID == 8)
			{
				this.Prompt.enabled = true;
			}
			else
			{
				if (this.Prompt.enabled)
				{
					this.Prompt.Hide();
					this.Prompt.enabled = false;
				}
			}
		}
		else
		{
			if (this.Prompt.enabled)
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
			}
		}

		AudioSource audioSource = this.GetComponent<AudioSource>();

		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Prompt.Circle[0].fillAmount = 1;

			if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
				this.VoidGoddess.Follow = false;

				this.Yandere.EquippedWeapon.Drop();
				this.Yandere.EquippedWeapon = null;
				this.Yandere.Unequip();
				this.Yandere.DropTimer[this.Yandere.Equipped] = 0.0f;

				this.RitualKnife.transform.position = this.OriginalPosition;
				this.RitualKnife.transform.eulerAngles = this.OriginalRotation;
				this.RitualKnife.GetComponent<Rigidbody>().useGravity = false;

				if (!this.MissionMode)
				{
					if (this.RitualKnife.GetComponent<WeaponScript>().Heated &&
						!this.RitualKnife.GetComponent<WeaponScript>().Flaming)
					{
						audioSource.clip = this.FlameDemonVoice;
						audioSource.Play();

						this.FlameTimer = 10.0f;

						this.RitualKnife.GetComponent<WeaponScript>().Prompt.Hide();
						this.RitualKnife.GetComponent<WeaponScript>().Prompt.enabled = false;
					}
					else
					{
						if (this.RitualKnife.GetComponent<WeaponScript>().Blood.enabled)
						{
							this.DebugMenu.SetActive(false);

							this.Yandere.Character.GetComponent<Animation>().CrossFade(this.Yandere.IdleAnim);
							this.Yandere.CanMove = false;
							Instantiate(this.DarkAura, this.Yandere.transform.position + (Vector3.up * 0.81f), Quaternion.identity);
							this.Timer += Time.deltaTime;
							this.Clock.StopTime = true;

							if (this.StudentManager.Students[21] == null || this.StudentManager.Students[26] == null || this.StudentManager.Students[31] == null || 
								this.StudentManager.Students[36] == null || this.StudentManager.Students[41] == null || this.StudentManager.Students[46] == null || 
								this.StudentManager.Students[51] == null || this.StudentManager.Students[56] == null || this.StudentManager.Students[61] == null || 
								this.StudentManager.Students[66] == null || this.StudentManager.Students[71] == null)
							{
								this.EmptyDemon.SetActive(false);
							}
							else
							{
								if (!this.StudentManager.Students[21].Alive || !this.StudentManager.Students[26].Alive || !this.StudentManager.Students[31].Alive  || 
									!this.StudentManager.Students[36].Alive || !this.StudentManager.Students[41].Alive  || !this.StudentManager.Students[46].Alive  || 
									!this.StudentManager.Students[51].Alive || !this.StudentManager.Students[56].Alive  || !this.StudentManager.Students[61].Alive  || 
									!this.StudentManager.Students[66].Alive || !this.StudentManager.Students[71].Alive )
								{
									this.EmptyDemon.SetActive(false);
								}
							}

							if (GameGlobals.EmptyDemon)
							{
								this.EmptyDemon.SetActive(false);
							}
						}
					}
				}
			}
		}

		if (this.FlameTimer > 0.0f)
		{
			this.FlameTimer = Mathf.MoveTowards(this.FlameTimer, 0.0f, Time.deltaTime);

			if (this.FlameTimer == 0.0f)
			{
				this.RitualKnife.GetComponent<WeaponScript>().FireEffect.gameObject.SetActive(true);
				this.RitualKnife.GetComponent<WeaponScript>().Prompt.enabled = true;
				this.RitualKnife.GetComponent<WeaponScript>().FireEffect.Play();
				this.RitualKnife.GetComponent<WeaponScript>().FireAudio.Play();
				this.RitualKnife.GetComponent<WeaponScript>().Flaming = true;
				this.Prompt.enabled = true;

                this.Prompt.Yandere.Police.Invalid = true;

				audioSource.clip = this.FlameActivation;
				audioSource.Play();
			}
		}

		if (this.Timer > 0.0f)
		{
			if (this.Yandere.transform.position.y < 1000.0f)
			{
				this.Timer += Time.deltaTime;

				if (this.Timer > 4.0f)
				{
					this.Darkness.enabled = true;

					this.Darkness.color = new Color(
						this.Darkness.color.r,
						this.Darkness.color.g,
						this.Darkness.color.b,
						Mathf.MoveTowards(this.Darkness.color.a, 1.0f, Time.deltaTime));

					if (this.Darkness.color.a == 1.0f)
					{
						this.Yandere.transform.position = new Vector3(0.0f, 2000.0f, 0.0f);
						this.Yandere.Character.SetActive(true);
						this.Yandere.SetAnimationLayers();

						this.HeartbeatCamera.SetActive(false);
						this.FPS.SetActive(false);
						this.HUD.SetActive(false);

						this.Hell.SetActive(true);
					}
				}
				else if (this.Timer > 1.0f)
				{
					this.Yandere.Character.SetActive(false);
				}
			}
			else
			{
				this.Jukebox.Volume = Mathf.MoveTowards(this.Jukebox.Volume, 0.0f, Time.deltaTime * 0.50f);

				if (this.Jukebox.Volume == 0.0f)
				{
					this.Darkness.color = new Color(
						this.Darkness.color.r,
						this.Darkness.color.g,
						this.Darkness.color.b,
						Mathf.MoveTowards(this.Darkness.color.a, 0.0f, Time.deltaTime));

					if (this.Darkness.color.a == 0.0f)
					{
						this.Yandere.CanMove = true;
						this.Timer = 0.0f;
					}
				}
			}
		}

		if (this.Yandere.Egg)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.enabled = false;
		}
	}
}
