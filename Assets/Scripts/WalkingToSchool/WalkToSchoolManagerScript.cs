using UnityEngine;
using UnityEngine.SceneManagement;

public class WalkToSchoolManagerScript : MonoBehaviour
{
	public PromptBarScript PromptBar;
	public CosmeticScript Yandere;
	public CosmeticScript Senpai;
	public CosmeticScript Rival;

	public UISprite Darkness;

	//Transforms

	public Transform[] Neighborhood;

	public Transform Window;

	public Transform RivalNeck;
	public Transform RivalHead;
	public Transform RivalEyeR;
	public Transform RivalEyeL;

	public Transform RivalJaw;
	public Transform RivalLipL;
	public Transform RivalLipR;

	public Transform SenpaiNeck;
	public Transform SenpaiHead;
	public Transform SenpaiEyeR;
	public Transform SenpaiEyeL;

	public Transform SenpaiJaw;
	public Transform SenpaiLipL;
	public Transform SenpaiLipR;

	public Transform YandereNeck;
	public Transform YandereHead;
	public Transform YandereEyeR;
	public Transform YandereEyeL;

	public AudioSource MyAudio;

	//Environment-Related
	public float ScrollSpeed = 1.0f;

	//Mouth Movement-Related
	public float LipStrength = 0.0001f;
	public float TimerLimit = 0.10f;
	public float TalkSpeed = 10.0f;
	public float AutoTimer = 0.0f;
	public float Timer = 0.0f;

	public float MouthExtent = 5.0f;
	public float MouthTarget = 0.0f;
	public float MouthTimer = 0.0f;

	//Head, Neck, Eye Rotation-Related
	public float RivalNeckTarget = 0.0f;
	public float RivalHeadTarget = 0.0f;
	public float RivalEyeRTarget = 0.0f;
	public float RivalEyeLTarget = 0.0f;

	public float SenpaiNeckTarget = 0.0f;
	public float SenpaiHeadTarget = 0.0f;
	public float SenpaiEyeRTarget = 0.0f;
	public float SenpaiEyeLTarget = 0.0f;

	public float YandereNeckTarget = 0.0f;
	public float YandereHeadTarget = 0.0f;

	public bool ShowWindow = false;
	public bool Debugging = false;
	public bool FadeOut = false;
	public bool Ending = false;
	public bool Auto = false;
	public bool Talk = false;

	//Typewriter-Related

	public TypewriterEffect Typewriter;
	public UILabel NameLabel;

	public AudioClip[] Speech;
	public string[] Lines;
	public bool[] Speakers;

	public int Frame = 0;
	public int ID = 0;

	void Start()
	{
		Application.targetFrameRate = 60;

		if (SchoolGlobals.SchoolAtmosphere < 0.50f || GameGlobals.LoveSick)
		{
			this.Darkness.color = new Color(0, 0, 0, 1);
		}
		else
		{
			this.Darkness.color = new Color(1, 1, 1, 1);
		}

		this.Window.localScale = new Vector3(0, 0, 0);

		this.Yandere.Character.GetComponent<Animation>()[AnimNames.FemaleNewWalk].time =
			Random.Range(0.0f, this.Yandere.Character.GetComponent<Animation>()[AnimNames.FemaleNewWalk].length);

		this.Yandere.WearOutdoorShoes();
		this.Senpai.WearOutdoorShoes();
		this.Rival.WearOutdoorShoes();
	}

	void Update()
	{
		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < 3; ID++)
		{
			Transform neighborhoodTransform = this.Neighborhood[ID];
			neighborhoodTransform.position = new Vector3(
				neighborhoodTransform.position.x - (Time.deltaTime * this.ScrollSpeed),
				neighborhoodTransform.position.y,
				neighborhoodTransform.position.z);

			if (neighborhoodTransform.position.x < -160.0f)
			{
				neighborhoodTransform.position = new Vector3(
					neighborhoodTransform.position.x + 320.0f,
					neighborhoodTransform.position.y,
					neighborhoodTransform.position.z);
			}
		}

		if (!this.FadeOut)
		{
			this.Darkness.color = new Color(
				this.Darkness.color.r,
				this.Darkness.color.g,
				this.Darkness.color.b,
				Mathf.MoveTowards(this.Darkness.color.a, 0.0f, Time.deltaTime));

			if (this.Darkness.color.a == 0)
			{
				if (!this.ShowWindow)
				{
					if (!Ending)
					{
						if (Input.GetButtonDown(InputNames.Xbox_A))
						{
							Timer = 1;
						}

						Timer += Time.deltaTime;

						if (Timer > 1)
						{
							RivalEyeRTarget = this.RivalEyeR.localEulerAngles.y;
							RivalEyeLTarget = this.RivalEyeL.localEulerAngles.y;

							SenpaiEyeRTarget = this.SenpaiEyeR.localEulerAngles.y;
							SenpaiEyeLTarget = this.SenpaiEyeL.localEulerAngles.y;

							this.ShowWindow = true;

							this.PromptBar.ClearButtons();
							this.PromptBar.Label[0].text = "Continue";
							this.PromptBar.Label[2].text = "Skip";
							this.PromptBar.UpdateButtons();
							this.PromptBar.Show = true;
						}
					}
					else
					{
						this.Window.localScale = Vector3.Lerp(this.Window.localScale, new Vector3(0, 0, 0), Time.deltaTime * 10);

						if (this.Window.localScale.x < .01)
						{
							this.Timer += Time.deltaTime;

							if (this.Timer > 1)
							{
								this.FadeOut = true;
							}
						}
					}
				}
				else
				{
					this.Window.localScale = Vector3.Lerp(this.Window.localScale, new Vector3(1, 1, 1), Time.deltaTime * 10);

					if (this.Window.localScale.x > .99)
					{
						if (this.Frame > 3)
						{
							this.Typewriter.mLabel.color = new Color(1, 1, 1, 1);
						}

						this.Frame++;
					}

					if (!this.Talk)
					{
						if (this.Window.localScale.x > .99)
						{
							this.Talk = true;
							this.UpdateNameLabel();

							this.Typewriter.enabled = true;
							this.Typewriter.ResetToBeginning();
							this.Typewriter.mFullText = this.Lines[this.ID];
							this.Typewriter.mLabel.text = this.Lines[this.ID];
							this.Typewriter.mLabel.color = new Color(1, 1, 1, 0);

							this.MyAudio.clip = this.Speech[this.ID];
							this.MyAudio.Play();
						}
					}
					else
					{
						Debug.Log ("Waiting for button press.");

						if (this.Auto)
						{
							if (!this.MyAudio.isPlaying)
							{
								this.AutoTimer += Time.deltaTime;
							}
						}

						if (Input.GetButtonDown(InputNames.Xbox_A) || this.AutoTimer > 1)
						{
							Debug.Log("Detected button press.");

							this.AutoTimer = 0;

							if (this.ID < (this.Lines.Length - 1))
							{
								if (this.Typewriter.mCurrentOffset < this.Typewriter.mFullText.Length)
								{
									Debug.Log("Line not finished yet.");

									this.Typewriter.Finish();

									this.Typewriter.mCurrentOffset = this.Typewriter.mFullText.Length;
								}
								else
								{
									Debug.Log("Line finished.");

									this.ID++;
									this.Frame = 0;
									this.Typewriter.ResetToBeginning();
									this.Typewriter.mFullText = this.Lines[this.ID];
									this.Typewriter.mLabel.text = this.Lines[this.ID];
									this.Typewriter.mLabel.color = new Color(1, 1, 1, 0);

									this.MyAudio.clip = this.Speech[this.ID];
									this.MyAudio.Play();

									this.UpdateNameLabel();
								}
							}
							else
							{
								if (this.Typewriter.mCurrentOffset < this.Typewriter.mFullText.Length)
								{
									this.Typewriter.Finish();
								}
								else
								{
									this.End();
								}
							}
						}

						if (Input.GetButtonDown(InputNames.Xbox_X))
						{
							this.End();
						}
					}
				}
			}
		}
		else
		{
			this.MyAudio.volume -= Time.deltaTime;

			this.Darkness.color = new Color(
				this.Darkness.color.r,
				this.Darkness.color.g,
				this.Darkness.color.b,
				Mathf.MoveTowards(this.Darkness.color.a, 1.0f, Time.deltaTime));

			if (this.Darkness.color.a == 1)
			{
				if (!this.Debugging)
				{
					SceneManager.LoadScene(SceneNames.LoadingScene);
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.Equals))
		{
			Time.timeScale += 10.0f;
		}

		if (Input.GetKeyDown(KeyCode.Minus))
		{
			Time.timeScale -= 10.0f;
		}
	}

	void LateUpdate()
	{
		if (Talk)
		{
			if (!Ending)
			{
				RivalNeckTarget = Mathf.Lerp (RivalNeckTarget, 15.0f, Time.deltaTime * 3.6f);
				RivalHeadTarget = Mathf.Lerp (RivalHeadTarget, 15.0f, Time.deltaTime * 3.6f);
				RivalEyeRTarget = Mathf.Lerp (RivalEyeRTarget, 95.0f, Time.deltaTime * 3.6f);
				RivalEyeLTarget = Mathf.Lerp (RivalEyeLTarget, 275.0f, Time.deltaTime * 3.6f);

				SenpaiNeckTarget = Mathf.Lerp (SenpaiNeckTarget, -15.0f, Time.deltaTime * 3.6f);
				SenpaiHeadTarget = Mathf.Lerp (SenpaiHeadTarget, -15.0f, Time.deltaTime * 3.6f);
				SenpaiEyeRTarget = Mathf.Lerp (SenpaiEyeRTarget, 85.0f, Time.deltaTime * 3.6f);
				SenpaiEyeLTarget = Mathf.Lerp (SenpaiEyeLTarget, 265.0f, Time.deltaTime * 3.6f);

				YandereNeckTarget = Mathf.Lerp (YandereNeckTarget, 7.5f, Time.deltaTime * 3.6f);
				YandereHeadTarget = Mathf.Lerp (YandereHeadTarget, 7.5f, Time.deltaTime * 3.6f);
			}
			else
			{
				RivalNeckTarget = Mathf.Lerp (RivalNeckTarget, 0.0f, Time.deltaTime * 3.6f);
				RivalHeadTarget = Mathf.Lerp (RivalHeadTarget, 0.0f, Time.deltaTime * 3.6f);
				RivalEyeRTarget = Mathf.Lerp (RivalEyeRTarget, 90.0f, Time.deltaTime * 3.6f);
				RivalEyeLTarget = Mathf.Lerp (RivalEyeLTarget, 270.0f, Time.deltaTime * 3.6f);

				SenpaiNeckTarget = Mathf.Lerp (SenpaiNeckTarget, 0.0f, Time.deltaTime * 3.6f);
				SenpaiHeadTarget = Mathf.Lerp (SenpaiHeadTarget, 0.0f, Time.deltaTime * 3.6f);
				SenpaiEyeRTarget = Mathf.Lerp (SenpaiEyeRTarget, 90.0f, Time.deltaTime * 3.6f);
				SenpaiEyeLTarget = Mathf.Lerp (SenpaiEyeLTarget, 270.0f, Time.deltaTime * 3.6f);

				YandereNeckTarget = Mathf.Lerp (YandereNeckTarget, 0.0f, Time.deltaTime * 3.6f);
				YandereHeadTarget = Mathf.Lerp (YandereHeadTarget, 0.0f, Time.deltaTime * 3.6f);
			}

			this.RivalNeck.localEulerAngles = new Vector3(
				this.RivalNeck.localEulerAngles.x,
				RivalNeckTarget,
				this.RivalNeck.localEulerAngles.z);

			this.RivalHead.localEulerAngles = new Vector3(
				this.RivalHead.localEulerAngles.x,
				RivalHeadTarget,
				this.RivalHead.localEulerAngles.z);

			this.RivalEyeR.localEulerAngles = new Vector3(
				this.RivalEyeR.localEulerAngles.x,
				RivalEyeRTarget,
				this.RivalEyeR.localEulerAngles.z);

			this.RivalEyeL.localEulerAngles = new Vector3(
				this.RivalEyeL.localEulerAngles.x,
				RivalEyeLTarget,
				this.RivalEyeL.localEulerAngles.z);

			this.SenpaiNeck.localEulerAngles = new Vector3(
				this.SenpaiNeck.localEulerAngles.x,
				SenpaiNeckTarget,
				this.SenpaiNeck.localEulerAngles.z);

			this.SenpaiHead.localEulerAngles = new Vector3(
				this.SenpaiHead.localEulerAngles.x,
				SenpaiHeadTarget,
				this.SenpaiHead.localEulerAngles.z);

			this.SenpaiEyeR.localEulerAngles = new Vector3(
				this.SenpaiEyeR.localEulerAngles.x,
				SenpaiEyeRTarget,
				this.SenpaiEyeR.localEulerAngles.z);

			this.SenpaiEyeL.localEulerAngles = new Vector3(
				this.SenpaiEyeL.localEulerAngles.x,
				SenpaiEyeLTarget,
				this.SenpaiEyeL.localEulerAngles.z);

			this.YandereNeck.localEulerAngles = new Vector3(
				this.YandereNeck.localEulerAngles.x,
				YandereNeckTarget,
				this.YandereNeck.localEulerAngles.z);

			this.YandereHead.localEulerAngles = new Vector3(
				this.YandereHead.localEulerAngles.x,
				YandereHeadTarget,
				this.YandereHead.localEulerAngles.z);

			//if (!Ending)
			if (MyAudio.isPlaying)
			{
				this.MouthTimer += Time.deltaTime;

				if (this.MouthTimer > this.TimerLimit)
				{
					this.MouthTarget = Random.Range(40.0f, 40.0f + this.MouthExtent);
					this.MouthTimer = 0.0f;
				}

				if (Speakers[ID])
				{
					this.RivalJaw.localEulerAngles = new Vector3(
						this.RivalJaw.localEulerAngles.x,
						this.RivalJaw.localEulerAngles.y,
						Mathf.Lerp(this.RivalJaw.localEulerAngles.z, this.MouthTarget, Time.deltaTime * this.TalkSpeed));

					this.RivalLipL.localPosition = new Vector3(
						this.RivalLipL.localPosition.x,
						Mathf.Lerp(this.RivalLipL.localPosition.y, 0.02632812f + (this.MouthTarget * this.LipStrength), Time.deltaTime * this.TalkSpeed),
						this.RivalLipL.localPosition.z);

					this.RivalLipR.localPosition = new Vector3(
						this.RivalLipR.localPosition.x,
						Mathf.Lerp(this.RivalLipR.localPosition.y, 0.02632812f + (this.MouthTarget * this.LipStrength), Time.deltaTime * this.TalkSpeed),
						this.RivalLipR.localPosition.z);
				}
				else
				{
					this.SenpaiJaw.localEulerAngles = new Vector3(
						this.SenpaiJaw.localEulerAngles.x,
						this.SenpaiJaw.localEulerAngles.y,
						Mathf.Lerp(this.SenpaiJaw.localEulerAngles.z, this.MouthTarget, Time.deltaTime * this.TalkSpeed));

					this.SenpaiLipL.localPosition = new Vector3(
						this.SenpaiLipL.localPosition.x,
						Mathf.Lerp(this.SenpaiLipL.localPosition.y, 0.02632812f + (this.MouthTarget * this.LipStrength), Time.deltaTime * this.TalkSpeed),
						this.SenpaiLipL.localPosition.z);

					this.SenpaiLipR.localPosition = new Vector3(
						this.SenpaiLipR.localPosition.x,
						Mathf.Lerp(this.SenpaiLipR.localPosition.y, 0.02632812f + (this.MouthTarget * this.LipStrength), Time.deltaTime * this.TalkSpeed),
						this.SenpaiLipR.localPosition.z);
				}
			}
		}
	}

	public void UpdateNameLabel()
	{
		if (this.Speakers[ID])
		{
			this.NameLabel.text = "Osana-chan";
		}
		else
		{
			this.NameLabel.text = "Senpai-kun";
		}
	}

	public void End()
	{
		this.PromptBar.Show = false;
		this.ShowWindow = false;
		this.Ending = true;
		this.Timer = 0;
	}
}