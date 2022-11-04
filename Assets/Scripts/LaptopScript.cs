using UnityEngine;

public class LaptopScript : MonoBehaviour
{
	public SkinnedMeshRenderer SCPRenderer;

	public Camera LaptopCamera;

	public JukeboxScript Jukebox;
	public YandereScript Yandere;
	public AudioSource MyAudio;
	public DynamicBone Hair;

	public Transform LaptopScreen;

	public AudioClip ShutDown;

	public GameObject SCP;

	public bool React = false;
	public bool Off = false;

	public float[] Cues;
	public string[] Subs;
	public Mesh[] Uniforms;

	public int FirstFrame = 0;

	public float Timer = 0.0f;

	public UILabel EventSubtitle;

	void Start()
	{
		if (SchoolGlobals.SCP)
		{
			this.LaptopScreen.localScale = Vector3.zero;
			this.LaptopCamera.enabled = false;
			this.SCP.SetActive(false);
			this.enabled = false;
		}
		else
		{
			this.SCPRenderer.sharedMesh = Uniforms[StudentGlobals.FemaleUniform];

			Animation scpAnimation = this.SCP.GetComponent<Animation>();
			scpAnimation[AnimNames.FemaleSCP].speed = 0.0f;
			scpAnimation[AnimNames.FemaleSCP].time = 0.0f;

			this.MyAudio = this.GetComponent<AudioSource>();
		}
	}

	void Update()
	{
		if (this.FirstFrame == 2)
		{
			this.LaptopCamera.enabled = false;
		}

		this.FirstFrame++;

		if (!this.Off)
		{
			Animation scpAnimation = this.SCP.GetComponent<Animation>();

			if (!this.React)
			{
				if (this.Yandere.transform.position.x > (this.transform.position.x + 1.0f))
				{
					if (Vector3.Distance(this.Yandere.transform.position,
						new Vector3(this.transform.position.x, 4.0f, this.transform.position.z)) < 2.0f)
					{
						if (this.Yandere.Followers == 0)
						{
							this.EventSubtitle.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

							// [af] Commented in JS code.
							//scpAnimation["f02_scp_00"].speed = 1;

							scpAnimation[AnimNames.FemaleSCP].time = 0.0f;
							this.LaptopCamera.enabled = true;
							scpAnimation.Play();
							this.Hair.enabled = true;
							this.Jukebox.Dip = 0.50f;
							this.MyAudio.Play();
							this.React = true;
						}
					}
				}
			}
			else
			{
				this.MyAudio.pitch = Time.timeScale;
				this.MyAudio.volume = 1.0f;

				// [af] Combined if statements for readability.
				if ((this.Yandere.transform.position.y > (this.transform.position.y + 3.0f)) ||
					(this.Yandere.transform.position.y < (this.transform.position.y - 3.0f)))
				{
					this.MyAudio.volume = 0.0f;
				}

				// [af] Converted while loop to for loop.
				for (int ID = 0; ID < this.Cues.Length; ID++)
				{
					if (this.MyAudio.time > this.Cues[ID])
					{
						this.EventSubtitle.text = this.Subs[ID];
					}
				}

				if ((this.MyAudio.time >= (this.MyAudio.clip.length - 1.0f)) ||
					(this.MyAudio.time == 0.0f))
				{
					scpAnimation[AnimNames.FemaleSCP].speed = 1.0f;

					this.Timer += Time.deltaTime;
				}
				else
				{
					scpAnimation[AnimNames.FemaleSCP].time = this.MyAudio.time;
				}

				if (this.Timer > 1.0f || 
					Vector3.Distance(this.Yandere.transform.position,
						new Vector3(this.transform.position.x, 4.0f, this.transform.position.z)) > 5.0f)
				{
					TurnOff ();
				}
			}

			if (this.Yandere.StudentManager.Clock.HourTime > 16 || this.Yandere.Police.FadeOut)
			{
				TurnOff ();
			}
		}
		else
		{
			if (this.LaptopScreen.localScale.x > 0.10f)
			{
				this.LaptopScreen.localScale = Vector3.Lerp(
					this.LaptopScreen.localScale, Vector3.zero, Time.deltaTime * 10.0f);
			}
			else
			{
				if (this.enabled)
				{
					this.LaptopScreen.localScale = Vector3.zero;
					this.Hair.enabled = false;
					this.enabled = false;
				}
			}
		}
	}

	void TurnOff()
	{
		this.MyAudio.clip = this.ShutDown;
		this.MyAudio.Play();

		this.EventSubtitle.text = string.Empty;
		SchoolGlobals.SCP = true;
		this.LaptopCamera.enabled = false;
		this.Jukebox.Dip = 1.0f;
		this.React = false;
		this.Off = true;
	}
}