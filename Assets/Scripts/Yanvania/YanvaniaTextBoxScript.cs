using UnityEngine;

public class YanvaniaTextBoxScript : MonoBehaviour
{
	private TypewriterEffect NewTypewriter;
	private UILabel NewLabelScript;
	private GameObject NewLabel;

	public YanvaniaJukeboxScript Jukebox;
	public YanvaniaDraculaScript Dracula;
	public YanvaniaYanmontScript Yanmont;

	public Transform NewLabelSpawnPoint;
	public GameObject Glass;
	public GameObject Label;

	public UILabel SpeakerLabel;
	public UITexture BloodWipe;
	public UITexture Portrait;
	public UITexture Border;
	public UITexture BG;

	public bool UpdatePortrait = false;
	public bool Display = false;
	public bool Leave = false;
	public bool Grow = false;

	public string[] SpeakerNames;
	public Texture[] Portraits;
	public AudioClip[] Voices;
	public string[] Lines;

	public int PortraitID = 1;
	public int LineID = 0;

	public float NewLineTimer = 0.0f;
	public float AnimTimer = 0.0f;
	public float Timer = 0.0f;

	void Start()
	{
		this.Portrait.transform.localScale = Vector3.zero;

		this.BloodWipe.transform.localScale = new Vector3(
			0.0f,
			this.BloodWipe.transform.localScale.y,
			this.BloodWipe.transform.localScale.z);

		this.SpeakerLabel.text = string.Empty;

		this.Border.color = new Color(
			this.Border.color.r,
			this.Border.color.g,
			this.Border.color.b,
			0.0f);

		this.BG.color = new Color(
			this.BG.color.r,
			this.BG.color.g,
			this.BG.color.b,
			0.0f);

		// [af] Added "gameObject" for C# compatibility.
		this.gameObject.SetActive(false);
	}

	void Update()
	{
		if (!this.Leave)
		{
			if (this.BloodWipe.transform.localScale.x == 0.0f)
			{
				this.BloodWipe.transform.localScale = new Vector3(
					this.BloodWipe.transform.localScale.x + Time.deltaTime,
					this.BloodWipe.transform.localScale.y,
					this.BloodWipe.transform.localScale.z);
			}

			if (this.BloodWipe.transform.localScale.x > 50.0f)
			{
				this.BloodWipe.color = new Color(
					this.BloodWipe.color.r,
					this.BloodWipe.color.g,
					this.BloodWipe.color.b,
					this.BloodWipe.color.a - Time.deltaTime);

				this.Border.color = new Color(
					this.Border.color.r,
					this.Border.color.g,
					this.Border.color.b,
					this.Border.color.a + Time.deltaTime);

				this.BG.color = new Color(
					this.BG.color.r,
					this.BG.color.g,
					this.BG.color.b,
					0.50f);
			}
			else
			{
				this.BloodWipe.transform.localScale = new Vector3(
					this.BloodWipe.transform.localScale.x + (this.BloodWipe.transform.localScale.x * 0.10f),
					this.BloodWipe.transform.localScale.y,
					this.BloodWipe.transform.localScale.z);
			}

			if (this.BloodWipe.color.a <= 0.0f)
			{
				if (!this.Display)
				{
					if (this.LineID < (this.Lines.Length - 1))
					{
						if (this.NewLabel != null)
						{
							Destroy(this.NewLabel);
						}

						this.UpdatePortrait = true;
						this.Display = true;

						// [af] Replaced if/else statement with ternary expression.
						this.PortraitID = (this.PortraitID == 1) ? 2 : 1;
						this.SpeakerLabel.text = string.Empty;
					}
				}
				else
				{
					if (this.NewLabelScript != null)
					{
						AudioSource audioSource = this.GetComponent<AudioSource>();

						if (!this.NewLabelScript.enabled)
						{
							this.NewLabelScript.enabled = true;
							audioSource.clip = this.Voices[this.LineID];
							this.NewLineTimer = 0.0f;
							audioSource.Play();
						}
						else
						{
							this.NewLineTimer += Time.deltaTime;

							if ((this.NewLineTimer > (audioSource.clip.length + 0.50f)) ||
								Input.GetButtonDown(InputNames.Xbox_A) ||
								Input.GetKeyDown("z") || Input.GetKeyDown("x"))
							{
								this.Display = false;
							}
						}
					}
				}
			}

			if (this.UpdatePortrait)
			{
				if (!this.Grow)
				{
					this.Portrait.transform.localScale = Vector3.MoveTowards(
						this.Portrait.transform.localScale,
						Vector3.zero,
						Time.deltaTime * 10.0f);

					if (this.Portrait.transform.localScale.x == 0.0f)
					{
						this.Portrait.mainTexture = this.Portraits[this.PortraitID];
						this.Grow = true;
					}
				}
				else
				{
					this.Portrait.transform.localScale = Vector3.MoveTowards(
						this.Portrait.transform.localScale,
						new Vector3(1.0f, 1.0f, 1.0f),
						Time.deltaTime * 10.0f);

					if (this.Portrait.transform.localScale.x == 1.0f)
					{
						this.SpeakerLabel.text = this.SpeakerNames[this.PortraitID];
						this.UpdatePortrait = false;
						this.AnimTimer = 0.0f;
						this.Grow = false;

						this.LineID++;
						this.SpawnLabel();
					}
				}
			}

			this.AnimTimer += Time.deltaTime;

			if (this.LineID == 2)
			{
				this.NewTypewriter.charsPerSecond = 15;
				this.NewTypewriter.delayOnPeriod = 1.50f;

				if (this.AnimTimer < 0.50f)
				{
					this.NewTypewriter.delayOnComma = true;
				}
			}

			Animation yanmontCharAnim = this.Yanmont.Character.GetComponent<Animation>();

			if (this.LineID == 3)
			{
				this.NewTypewriter.delayOnComma = true;

				this.NewTypewriter.delayOnPeriod = 0.75f;

				if (this.AnimTimer < 1.0f)
				{
					yanmontCharAnim.CrossFade(AnimNames.FemaleYanvaniaCutsceneAction1);
				}

				if (yanmontCharAnim[AnimNames.FemaleYanvaniaCutsceneAction1].time >=
					yanmontCharAnim[AnimNames.FemaleYanvaniaCutsceneAction1].length)
				{
					yanmontCharAnim.CrossFade(AnimNames.FemaleYanvaniaDramaticIdle);
				}
			}

			Animation draculaCharAnim = this.Dracula.Character.GetComponent<Animation>();

			if (this.LineID == 5)
			{
				this.NewTypewriter.charsPerSecond = 15;

				yanmontCharAnim.CrossFade(AnimNames.FemaleYanvaniaCutsceneAction2);

				if (yanmontCharAnim[AnimNames.FemaleYanvaniaCutsceneAction2].time >=
					yanmontCharAnim[AnimNames.FemaleYanvaniaCutsceneAction2].length)
				{
					yanmontCharAnim.CrossFade(AnimNames.FemaleYanvaniaDramaticIdle);
				}

				if (this.AnimTimer > 4.0f)
				{
					draculaCharAnim.CrossFade(AnimNames.DraculaDrink);
				}

				if (this.AnimTimer > 4.50f)
				{
					this.Glass.GetComponent<Renderer>().materials[0].color =
						new Color(1.0f, 1.0f, 1.0f, 0.50f);
				}

				if (this.AnimTimer > 5.0f)
				{
					if (draculaCharAnim[AnimNames.DraculaDrink].time >=
						draculaCharAnim[AnimNames.DraculaDrink].length)
					{
						draculaCharAnim.CrossFade(AnimNames.DraculaIdle);
					}
				}
			}

			if (this.LineID == 6)
			{
				yanmontCharAnim.CrossFade(AnimNames.FemaleYanvaniaDramaticIdle);

				if (this.AnimTimer < 1.0f)
				{
					NewTypewriter.delayOnPeriod = 2.25f;
				}

				if ((this.AnimTimer > 1.0f) && (this.AnimTimer < 2.0f))
				{
					draculaCharAnim.CrossFade(AnimNames.DraculaToss);
				}

				if (this.Glass != null)
				{
					Rigidbody glassRigidBody = this.Glass.GetComponent<Rigidbody>();
					
					if ((this.AnimTimer > 1.40f) && !glassRigidBody.useGravity)
					{
						glassRigidBody.useGravity = true;
						this.Glass.transform.parent = null;
						glassRigidBody.AddForce(-100.0f, 100.0f, -200.0f);
					}
				}

				if ((this.AnimTimer > (2.0f + draculaCharAnim[AnimNames.DraculaToss].length)) &&
					(this.AnimTimer < 6.0f))
				{
					draculaCharAnim.CrossFade(AnimNames.DraculaIdle);
				}

				if (this.AnimTimer > 4.0f)
				{
					this.NewTypewriter.delayOnPeriod = 1.0f;
					this.NewTypewriter.charsPerSecond = 15;
				}

				if ((this.AnimTimer > 6.0f) && (this.AnimTimer < 9.50f))
				{
					this.Dracula.transform.position = Vector3.Lerp(
						this.Dracula.transform.position,
						new Vector3(-34.675f, 7.50f, 2.80f),
						Time.deltaTime * 10.0f);

					draculaCharAnim.CrossFade("succubus_a_idle_01");
				}

				if (this.AnimTimer > 9.50f)
				{
					this.NewLabelScript.text = string.Empty;
					this.SpeakerLabel.text = string.Empty;

					this.Dracula.SpawnTeleportEffect();
					this.Dracula.enabled = true;
					this.Jukebox.BossBattle();
					this.Leave = true;
				}
			}

			if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				if (this.NewLabel != null)
				{
					Destroy(this.NewLabel);
				}

				if (this.NewLabelScript != null)
				{
					this.NewLabelScript.text = string.Empty;
				}

				this.SpeakerLabel.text = string.Empty;

				this.Dracula.SpawnTeleportEffect();
				this.Dracula.enabled = true;
				this.Jukebox.BossBattle();
				Destroy(this.BloodWipe);
				Destroy(this.Glass);
				this.Leave = true;
			}
		}
		else
		{
			this.Portrait.transform.localScale = Vector3.MoveTowards(
				this.Portrait.transform.localScale,
				Vector3.zero,
				Time.deltaTime * 10.0f);

			if (this.Portrait.transform.localScale.x == 0.0f)
			{
				this.Border.transform.position = new Vector3(
					this.Border.transform.position.x,
					this.Border.transform.position.y + Time.deltaTime,
					this.Border.transform.position.z);

				this.BG.transform.position = new Vector3(
					this.BG.transform.position.x,
					this.BG.transform.position.y + Time.deltaTime,
					this.BG.transform.position.z);

				if (!this.Yanmont.enabled)
				{
					this.Yanmont.EnterCutscene = false;
					this.Yanmont.Cutscene = false;
					this.Yanmont.enabled = true;
				}
			}
		}
	}

	void SpawnLabel()
	{
		this.NewLabel = Instantiate(this.Label, this.transform.position, Quaternion.identity);
		this.NewLabel.transform.parent = this.NewLabelSpawnPoint;
		this.NewLabel.transform.localEulerAngles = Vector3.zero;
		this.NewLabel.transform.localPosition = Vector3.zero;
		this.NewLabel.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

		this.NewTypewriter = this.NewLabel.GetComponent<TypewriterEffect>();
		this.NewLabelScript = this.NewLabel.GetComponent<UILabel>();
		this.NewLabelScript.text = this.Lines[this.LineID];
		this.NewLabelScript.enabled = false;
	}
}
