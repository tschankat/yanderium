using UnityEngine;

public class ConfessionSceneScript : MonoBehaviour
{
	public Transform[] CameraDestinations;

	public StudentManagerScript StudentManager;
	public LoveManagerScript LoveManager;
	public PromptBarScript PromptBar;
	public JukeboxScript Jukebox;
	public YandereScript Yandere;
	public ClockScript Clock;
	public Bloom BloomEffect;

	public StudentScript Suitor;
	public StudentScript Rival;

	public ParticleSystem MythBlossoms;

	public GameObject HeartBeatCamera;
	public GameObject ConfessionBG;

	public Transform MainCamera;
	public Transform RivalSpot;
	public Transform KissSpot;

	public string[] Text;

	public GameObject[] Letters;

	public UISprite Darkness;
	public UILabel Label;
	public UIPanel Panel;

	public AudioSource Jingle;

	public bool MoveSuitor = false;
	public bool ShowLabel = false;
	public bool Kissing = false;

	public int TextPhase = 1;
	public int LetterID = 1;
	public int Phase = 1;

	public float LetterTimer = 0.1f;
	public float Speed = 0.0f;
	public float Timer = 0.0f;

	void Start()
	{
		Time.timeScale = 1;
	}

	void Update()
	{
		if (this.Phase == 1)
		{
			this.Darkness.color = new Color(
				this.Darkness.color.r,
				this.Darkness.color.g,
				this.Darkness.color.b,
				Mathf.MoveTowards(this.Darkness.color.a, 1.0f, Time.deltaTime));

			this.Panel.alpha = Mathf.MoveTowards(this.Panel.alpha, 0.0f, Time.deltaTime);
			this.Jukebox.Volume = Mathf.MoveTowards(this.Jukebox.Volume, 0.0f, Time.deltaTime);

			if (this.Darkness.color.a == 1.0f)
			{
				this.Timer += Time.deltaTime;

				if (this.Timer > 1.0f)
				{
					this.BloomEffect.bloomIntensity = 1.0f;
					this.BloomEffect.bloomThreshhold = 0.0f;
					this.BloomEffect.bloomBlurIterations = 1;

					this.Suitor = this.StudentManager.Students[this.LoveManager.SuitorID];
					this.Rival = this.StudentManager.Students[this.LoveManager.RivalID];

					this.Rival.transform.position = this.RivalSpot.position;
					this.Rival.transform.eulerAngles = this.RivalSpot.eulerAngles;

					this.Suitor.Cosmetic.MyRenderer.materials[this.Suitor.Cosmetic.FaceID].SetFloat("_BlendAmount", 1.0f);
					this.Suitor.transform.eulerAngles = this.StudentManager.SuitorConfessionSpot.eulerAngles;
					this.Suitor.transform.position = this.StudentManager.SuitorConfessionSpot.position;
					this.Suitor.CharacterAnimation.Play(this.Suitor.IdleAnim);

					// [af] This is the (unintuitive) Unity 5.3 way to change emission.
					ParticleSystem.EmissionModule mythBlossomsEmission = this.MythBlossoms.emission;
					mythBlossomsEmission.rateOverTime = 100.0f;

					this.HeartBeatCamera.SetActive(false);
					this.ConfessionBG.SetActive(true);
					this.GetComponent<AudioSource>().Play();

					this.MainCamera.position = this.CameraDestinations[1].position;
					this.MainCamera.eulerAngles = this.CameraDestinations[1].eulerAngles;
					this.Timer = 0.0f;
					this.Phase++;
				}
			}
		}
		else if (this.Phase == 2)
		{
			this.Darkness.color = new Color(
				this.Darkness.color.r,
				this.Darkness.color.g,
				this.Darkness.color.b,
				Mathf.MoveTowards(this.Darkness.color.a, 0.0f, Time.deltaTime));

			if (this.Darkness.color.a == 0.0f)
			{
				if (!this.ShowLabel)
				{
					this.Label.color = new Color(
						this.Label.color.r,
						this.Label.color.g,
						this.Label.color.b,
						Mathf.MoveTowards(this.Label.color.a, 0.0f, Time.deltaTime));

					if (this.Label.color.a == 0.0f)
					{
						if (this.TextPhase < 5)
						{
							this.MainCamera.position = this.CameraDestinations[this.TextPhase].position;
							this.MainCamera.eulerAngles = this.CameraDestinations[this.TextPhase].eulerAngles;

							if (this.TextPhase == 4)
							{
								if (!this.Kissing)
								{
									// [af] This is the (unintuitive) Unity 5.3 way to change emission.
									ParticleSystem.EmissionModule suitorEmission = this.Suitor.Hearts.emission;
									suitorEmission.enabled = true;
									suitorEmission.rateOverTime = 10.0f;
									this.Suitor.Hearts.Play();

									ParticleSystem.EmissionModule rivalEmission = this.Rival.Hearts.emission;
									rivalEmission.enabled = true;
									rivalEmission.rateOverTime = 10.0f;
									this.Rival.Hearts.Play();

									this.Suitor.Character.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
									this.Suitor.CharacterAnimation.Play(AnimNames.MaleKiss);
									this.Suitor.transform.position = this.KissSpot.position;

									this.Rival.CharacterAnimation[this.Rival.ShyAnim].weight = 0.0f;
									this.Rival.CharacterAnimation.Play(AnimNames.FemaleKiss);

									this.Kissing = true;
								}
							}

							this.Label.text = this.Text[this.TextPhase];

							this.ShowLabel = true;
						}
						else
						{
							this.Jingle.Play();
							this.Phase++;
						}
					}
				}
				else
				{
					this.Label.color = new Color(
						this.Label.color.r,
						this.Label.color.g,
						this.Label.color.b,
						Mathf.MoveTowards(this.Label.color.a, 1.0f, Time.deltaTime));

					if (this.Label.color.a == 1.0f)
					{
						if (!this.PromptBar.Show)
						{
							this.PromptBar.ClearButtons();
							this.PromptBar.Label[0].text = "Continue";
							this.PromptBar.UpdateButtons();
							this.PromptBar.Show = true;
						}

						if (Input.GetButtonDown(InputNames.Xbox_A))
						{
							this.TextPhase++;

							this.ShowLabel = false;
						}
					}
				}
			}
		}
		else if (this.Phase == 3)
		{
			this.LetterTimer += Time.deltaTime;

			if (this.LetterTimer > .1f)
			{
				if (this.LetterID < this.Letters.Length)
				{
					this.Letters[this.LetterID].SetActive(true);
					this.LetterTimer = 0;
					this.LetterID++;
				}
			}

			if (this.LetterTimer > 5)
			{
				this.Phase++;
			}
		}
		else if (this.Phase == 4)
		{
			this.Darkness.color = new Color(
				this.Darkness.color.r,
				this.Darkness.color.g,
				this.Darkness.color.b,
				Mathf.MoveTowards(this.Darkness.color.a, 1.0f, Time.deltaTime));

			if (this.Darkness.color.a == 1.0f)
			{
				this.Timer += Time.deltaTime;

				if (this.Timer > 1.0f)
				{
					DatingGlobals.SuitorProgress = 2;

					this.Suitor.Character.transform.localScale = new Vector3(0.94f, 0.94f, 0.94f);

					this.PromptBar.ClearButtons();
					this.PromptBar.UpdateButtons();
					this.PromptBar.Show = false;

					this.ConfessionBG.SetActive(false);
					this.Yandere.FixCamera();

					this.Phase++;
				}
			}
		}
		else
		{
			this.Darkness.color = new Color(
				this.Darkness.color.r,
				this.Darkness.color.g,
				this.Darkness.color.b,
				Mathf.MoveTowards(this.Darkness.color.a, 0.0f, Time.deltaTime));

			this.Panel.alpha = Mathf.MoveTowards(this.Panel.alpha, 1.0f, Time.deltaTime);

			if (this.Darkness.color.a == 0.0f)
			{
				this.StudentManager.ComeBack();

				this.Suitor.enabled = false;
				this.Suitor.Prompt.enabled = false;
				this.Suitor.Pathfinding.canMove = false;
				this.Suitor.Pathfinding.canSearch = false;

				this.Rival.enabled = false;
				this.Rival.Prompt.enabled = false;
				this.Rival.Pathfinding.canMove = false;
				this.Rival.Pathfinding.canSearch = false;

				this.Yandere.RPGCamera.enabled = true;
				this.Yandere.CanMove = true;

				this.HeartBeatCamera.SetActive(true);

				// [af] This is the (unintuitive) Unity 5.3 way to change emission.
				ParticleSystem.EmissionModule emission = this.MythBlossoms.emission;
				emission.rateOverTime = 20.0f;

				this.Clock.StopTime = false;
				this.enabled = false;

				this.Suitor.CoupleID = this.LoveManager.SuitorID;
				this.Rival.CoupleID = this.LoveManager.RivalID;

				this.Suitor.CharacterAnimation.CrossFade("holdHandsLoop_00");
				this.Rival.CharacterAnimation.CrossFade("f02_holdHandsLoop_00");
			}
		}

		if (this.Kissing)
		{
			if (this.Suitor.CharacterAnimation["kiss_00"].time >= this.Suitor.CharacterAnimation["kiss_00"].length *.66666f)
			{
				this.Suitor.Character.transform.localScale = Vector3.Lerp(
					this.Suitor.Character.transform.localScale,
					new Vector3(.94f, .94f, .94f),
					Time.deltaTime);
			}

			if (this.Suitor.CharacterAnimation["kiss_00"].time >= this.Suitor.CharacterAnimation["kiss_00"].length)
			{
				this.Rival.CharacterAnimation.CrossFade("f02_introHoldHands_00");
				this.Suitor.CharacterAnimation.CrossFade("introHoldHands_00");

				//this.Suitor.CharacterAnimation.CrossFade(this.Suitor.IdleAnim);
				//this.Rival.CharacterAnimation.CrossFade(this.Rival.IdleAnim);

				this.Kissing = false;
				this.MoveSuitor = true;
			}
		}
		else
		{
			if (this.Suitor != null)
			{
				this.Suitor.Character.transform.localScale = Vector3.Lerp(
					this.Suitor.Character.transform.localScale,
					new Vector3(.94f, .94f, .94f),
					Time.deltaTime);

				if (this.MoveSuitor)
				{
					Speed += Time.deltaTime;

					this.Suitor.Character.transform.position = Vector3.Lerp(
						this.Suitor.Character.transform.position,
						new Vector3(0, 6.6f, 119.2f),
						Time.deltaTime * Speed);
				}
			}
		}
	}
}