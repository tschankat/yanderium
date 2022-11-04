using UnityEngine;
using UnityEngine.SceneManagement;

public class LivingRoomCutsceneScript : MonoBehaviour
{
	public ColorCorrectionCurves ColorCorrection;
	public CosmeticScript YandereCosmetic;
	public AmbientObscurance Obscurance;
    public RivalDataScript RivalData;
	public Vignetting Vignette;
	public NoiseAndGrain Noise;

	public SkinnedMeshRenderer YandereRenderer;
	public Renderer RightEyeRenderer;
	public Renderer LeftEyeRenderer;

    public Transform KettleCameraDestination;
    public Transform KettleCameraOrigin;

    public Transform FriendshipCamera;
	public Transform LivingRoomCamera;
	public Transform CutsceneCamera;
    public Transform TeaCamera;

    public UIPanel EliminationPanel;
    public UIPanel Panel;

    public UISprite SubDarknessBG;
    public UISprite SubDarkness;
    public UISprite Darkness;

    public UILabel PrologueLabel;
    public UILabel Subtitle;

	public Vector3 RightEyeOrigin;
	public Vector3 LeftEyeOrigin;

	public AudioClip DramaticBoom;
	public AudioClip RivalProtest;

    public AudioSource Jukebox;
    public AudioSource MyAudio;

    public GameObject TeaSteam;
    public GameObject CatStuff;
    public GameObject Prologue;
	public GameObject Yandere;
    public GameObject TeaSet;
    public GameObject Rival;

	public Transform RightEye;
	public Transform LeftEye;

	public float ShakeStrength = 0.0f;
	public float AnimOffset = 0.0f;
	public float EyeShrink = 0.0f;
	public float xOffset = 0.0f;
	public float zOffset = 0.0f;
	public float Timer = 0.0f;
    public float Speed = 0.0f;

    public bool OsanaCutscene;
    public bool DecisionMade;
    public bool DruggedTea;

    public string[] Lines;
	public float[] Times;

    public int Branch = 1;
	public int Phase = 1;
	public int ID = 1;

	public Texture ZTR;
	public int ZTRID = 0;

	void Start()
	{
		this.YandereCosmetic.SetFemaleUniform();
		this.YandereCosmetic.RightWristband.SetActive(false);
		this.YandereCosmetic.LeftWristband.SetActive(false);
		this.YandereCosmetic.ThickBrows.SetActive(false);

		for (this.ID = 0; this.ID < this.YandereCosmetic.FemaleHair.Length; this.ID++)
		{
			GameObject hair = this.YandereCosmetic.FemaleHair[this.ID];

			if (hair != null)
			{
				hair.SetActive(false);
			}
		}

		for (this.ID = 0; this.ID < this.YandereCosmetic.TeacherHair.Length; this.ID++)
		{
			GameObject hair = this.YandereCosmetic.TeacherHair[this.ID];

			if (hair != null)
			{
				hair.SetActive(false);
			}
		}

		for (this.ID = 0; this.ID < this.YandereCosmetic.FemaleAccessories.Length; this.ID++)
		{
			GameObject acc = this.YandereCosmetic.FemaleAccessories[this.ID];

			if (acc != null)
			{
				acc.SetActive(false);
			}
		}

		for (this.ID = 0; this.ID < this.YandereCosmetic.TeacherAccessories.Length; this.ID++)
		{
			GameObject acc = this.YandereCosmetic.TeacherAccessories[this.ID];

			if (acc != null)
			{
				acc.SetActive(false);
			}
		}

		for (this.ID = 0; this.ID < this.YandereCosmetic.ClubAccessories.Length; this.ID++)
		{
			GameObject acc = this.YandereCosmetic.ClubAccessories[this.ID];

			if (acc != null)
			{
				acc.SetActive(false);
			}
		}

		foreach (GameObject scanner in this.YandereCosmetic.Scanners)
		{
			if (scanner != null)
			{
				scanner.SetActive(false);
			}
		}

		foreach (GameObject flower in this.YandereCosmetic.Flowers)
		{
			if (flower != null)
			{
				flower.SetActive(false);
			}
		}

		foreach (GameObject punk in this.YandereCosmetic.PunkAccessories)
		{
			if (punk != null)
			{
				punk.SetActive(false);
			}
		}

		foreach (GameObject cloth in this.YandereCosmetic.RedCloth)
		{
			if (cloth != null)
			{
				cloth.SetActive(false);
			}
		}

		foreach (GameObject kerchief in this.YandereCosmetic.Kerchiefs)
		{
			if (kerchief != null)
			{
				kerchief.SetActive(false);
			}
		}

		int NailID = 0;

		while (NailID < 10)
		{
			this.YandereCosmetic.Fingernails[NailID].gameObject.SetActive(false);
			NailID++;
		}

		ID = 0;

		this.YandereCosmetic.FemaleHair[1].SetActive(true);
		this.YandereCosmetic.MyRenderer.materials[2].mainTexture = this.YandereCosmetic.DefaultFaceTexture;

		this.Subtitle.text = string.Empty;

		this.RightEyeRenderer.material.color = new Color(0.33f, 0.33f, 0.33f, 1.0f);
		this.LeftEyeRenderer.material.color = new Color(0.33f, 0.33f, 0.33f, 1.0f);

		this.RightEyeOrigin = this.RightEye.localPosition;
		this.LeftEyeOrigin = this.LeftEye.localPosition;

		this.EliminationPanel.alpha = 0.0f;
		this.Panel.alpha = 1.0f;

		this.ColorCorrection.saturation = 1.0f;
		this.Noise.intensityMultiplier = 0.0f;
		this.Obscurance.intensity = 0.0f;

		this.Vignette.enabled = false;
		this.Vignette.intensity = 1.0f;
		this.Vignette.blur = 1.0f;
		this.Vignette.chromaticAberration = 1.0f;

        //Anti-Osana Code
        #if (UNITY_EDITOR)
        if (EventGlobals.OsanaConversation)
        {
            this.PrologueLabel.text =
                "Osana is eager to report her stalker to the police." + "\n" + "\n" +
                "However, she knows that the process could take a long time, so she decides to visit Ayano's house and get her cat back before contacting the police." + "\n" + "\n" +
                "The next morning, Osana arrives at Ayano's house...";

            this.CatStuff.SetActive(true);
            this.OsanaCutscene = true;

            this.Lines = this.RivalData.OsanaIntroLines;
            this.Times = this.RivalData.OsanaIntroTimes;

            this.MyAudio.clip = this.RivalData.OsanaIntro;
        }
        #endif
    }

    void Update()
	{
        #if (UNITY_EDITOR)
        if (Input.GetKeyDown("o"))
        {
            EventGlobals.OsanaConversation = true;
            SceneManager.LoadScene(SceneNames.LivingRoomScene);
        }

        if (Input.GetKeyDown("space"))
        {
            this.Yandere.GetComponent<Animation>()["FriendshipYandere"].time = 10.0f;
            this.Rival.GetComponent<Animation>()["FriendshipRival"].time = 10.0f;

            this.Yandere.GetComponent<Animation>()["FriendshipYandere"].speed = 0.0f;
            this.Rival.GetComponent<Animation>()["FriendshipRival"].speed = 0.0f; ;

            this.Darkness.color = new Color(0, 0, 0, 0);
            this.Prologue.SetActive(false);
            this.Timer += 10;
            this.Phase = 4;
        }
        #endif

        //Displaying the prologue.
        if (this.Phase == 1)
		{
			this.Timer += Time.deltaTime;

			if (this.Timer > 1.0f)
			{
				this.Darkness.color = new Color(
					this.Darkness.color.r,
					this.Darkness.color.g,
					this.Darkness.color.b,
					Mathf.MoveTowards(this.Darkness.color.a, 0.0f, Time.deltaTime));

				if (this.Darkness.color.a == 0.0f)
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.Timer = 0;
						this.Phase++;
					}
				}
			}
		}
        //Fading from the prologue.
		else if (this.Phase == 2)
		{
			this.Darkness.color = new Color(
					this.Darkness.color.r,
					this.Darkness.color.g,
					this.Darkness.color.b,
					Mathf.MoveTowards(this.Darkness.color.a, 1.0f, Time.deltaTime));

			if (this.Darkness.color.a == 1.0f)
			{
				this.transform.parent = this.LivingRoomCamera;
				this.transform.localPosition = new Vector3(-0.65f, 0.0f, 0.0f);
				this.transform.localEulerAngles = new Vector3(0.0f, -90.0f, 0.0f);

				this.Vignette.enabled = true;
				this.Prologue.SetActive(false);
				this.Phase++;
			}
		}
        //Establishing shot, panning over to Yandere/Rival
		else if (this.Phase == 3)
		{
			this.Timer += Time.deltaTime;

			if (this.Timer > 1.0f)
			{
				this.Panel.alpha = Mathf.MoveTowards(this.Panel.alpha, 0.0f, Time.deltaTime);

				if (this.Panel.alpha == 0.0f)
				{
					this.Yandere.GetComponent<Animation>()["FriendshipYandere"].time = 0.0f;
					this.Rival.GetComponent<Animation>()["FriendshipRival"].time = 0.0f;

                    if (this.OsanaCutscene)
                    {
                        this.Yandere.GetComponent<Animation>()["FriendshipYandere"].speed = 0.0f;
                        this.Rival.GetComponent<Animation>()["FriendshipRival"].speed = 0.0f;
                    }

                    this.LivingRoomCamera.gameObject.GetComponent<Animation>().Play();

					this.Timer = 0.0f;
					this.Phase++;
				}
			}
		}
        //Dialogue Begins
		else if (this.Phase == 4)
		{
			this.Timer += Time.deltaTime;

			if (this.Timer > 10.0f)
			{
				this.transform.parent = this.FriendshipCamera;
				this.transform.localPosition = new Vector3(-0.65f, 0.0f, 0.0f);
				this.transform.localEulerAngles = new Vector3(0.0f, -90.0f, 0.0f);

				this.FriendshipCamera.gameObject.GetComponent<Animation>().Play();

				this.MyAudio.Play();

				this.Subtitle.text = this.Lines[0];

				this.Timer = 0.0f;
				this.Phase++;
			}
		}
        //Entire conversation
		else if (this.Phase == 5)
		{
			if (Input.GetKeyDown(KeyCode.Z))
			{
				this.Timer += 2.0f;
				this.MyAudio.time += 2.0f;

                if (this.FriendshipCamera.gameObject.GetComponent<Animation>()["FriendshipCameraFlat"].speed > 0)
                {
    				this.FriendshipCamera.gameObject.GetComponent<Animation>()["FriendshipCameraFlat"].time += 2.0f;
                }
            }

			this.Timer += Time.deltaTime;

			if (this.Timer < 166.0f)
			{
                if (!this.OsanaCutscene)
                {
				    this.Yandere.GetComponent<Animation>()["FriendshipYandere"].time =
					    this.MyAudio.time + this.AnimOffset;
				    this.Rival.GetComponent<Animation>()["FriendshipRival"].time =
					    this.MyAudio.time + this.AnimOffset;
                }
            }

			if (this.ID < this.Times.Length)
			{
				//Debug.Log (this.MyAudio.time);	

				if (this.MyAudio.time > this.Times[this.ID])
				{
                    if (this.OsanaCutscene)
                    {
                        this.Yandere.GetComponent<Animation>()["FriendshipYandere"].time =
                            this.MyAudio.time + this.AnimOffset;
                        this.Rival.GetComponent<Animation>()["FriendshipRival"].time =
                            this.MyAudio.time + this.AnimOffset;
                    }

                    this.Subtitle.text = this.Lines[this.ID];
					this.ID++;
				}
                else
                {
                    if (this.OsanaCutscene)
                    {
                        if (this.Branch == 1)
                        {
                            if (!this.DruggedTea)
                            {
                                this.Lines = this.RivalData.OsanaBefriendLines;
                                this.Times = this.RivalData.OsanaBefriendTimes;

                                this.MyAudio.clip = this.RivalData.OsanaBefriend;
                                this.MyAudio.Play();

                                this.Branch = 2;
                            }
                            else
                            {
                                this.Branch = 3;
                            }
                        }
                    }
                }
			}

            if (this.OsanaCutscene)
            {
                //Intro
                if (this.Branch == 1)
                {
                    if (this.ID == 12)
                    {
                        if (!this.TeaSteam.activeInHierarchy)
                        {
                            this.transform.parent = null;
                            this.transform.position = this.KettleCameraOrigin.position;
                            this.transform.rotation = this.KettleCameraOrigin.rotation;

                            this.TeaSteam.SetActive(true);
                        }
                        else
                        {
                            this.Speed += Time.deltaTime * .2f;

                            this.transform.position = Vector3.Lerp(
                                this.transform.position,
                                this.KettleCameraDestination.position,
                                Time.deltaTime * Speed);
                        }
                    }
                    else if (this.ID > 12 && this.ID < 16)
                    {
                        this.transform.position = new Vector3(-6, 1, 3);
                        this.transform.localEulerAngles = new Vector3(0, 90.0f, 0);
                    }
                    else if (this.ID > 17 && this.ID < 24 && !this.DecisionMade)
                    {
                        if (!this.TeaSet.activeInHierarchy)
                        {
                            this.transform.parent = null;
                            this.transform.position = this.TeaCamera.position;
                            this.transform.rotation = this.TeaCamera.rotation;

                            this.TeaSet.SetActive(true);
                            this.Yandere.SetActive(false);

                            this.AnimOffset += 2;
                        }

                        if (Input.GetButtonDown(InputNames.Xbox_A))
                        {
                            this.DecisionMade = true;
                        }

                        if (Input.GetButtonDown(InputNames.Xbox_B))
                        {
                            this.DecisionMade = true;
                            this.DruggedTea = true;
                        }
                    }
                    else
                    {
                        this.transform.parent = this.FriendshipCamera;
                        this.transform.localEulerAngles = new Vector3(0, -90.0f, 0);

                        if (this.ID == 16)
                        {
                            if (this.FriendshipCamera.gameObject.GetComponent<Animation>()["FriendshipCameraFlat"].time < 44)
                            {
                                this.FriendshipCamera.gameObject.GetComponent<Animation>()["FriendshipCameraFlat"].time = 44;
                                this.FriendshipCamera.gameObject.GetComponent<Animation>()["FriendshipCameraFlat"].speed = 0;
                            }
                        }
                    }
                }
                //Befriending Osana
                else if (this.Branch == 2)
                {

                }
            }

            if (!this.OsanaCutscene)
            {
			    if (this.MyAudio.time > 54.0f)
			    {
                    this.DecreaseYandereEffects();
			    }
			    else if (this.MyAudio.time > 42.0f)
			    {
                    this.IncreaseYandereEffects();
			    }
            }
            else
            {
                if (this.DecisionMade || this.MyAudio.time > 60.0f)
                {
                    this.DecreaseYandereEffects();
                }
                else if (this.MyAudio.time > 43.0f)
                {
                    this.IncreaseYandereEffects();
                }
            }

            if (this.Timer > 167.0f)
			{
				Animation yandereAnim = this.Yandere.GetComponent<Animation>();
				yandereAnim["FriendshipYandere"].speed = -0.20f;
				yandereAnim.Play("FriendshipYandere");
				yandereAnim["FriendshipYandere"].time = yandereAnim["FriendshipYandere"].length;

				this.Subtitle.text = string.Empty;
				this.Phase = 10;
			}
		}
        //Dramatic Boom
		else if (this.Phase == 6)
		{
			if (!this.MyAudio.isPlaying)
			{
				this.MyAudio.clip = this.DramaticBoom;
				this.MyAudio.Play();

				this.Subtitle.text = string.Empty;

				this.Phase++;
			}
		}
        //Exiting scene, Yandere betrayed Osana
		else if (this.Phase == 7)
		{
			if (!this.MyAudio.isPlaying)
			{
				StudentGlobals.SetStudentKidnapped(81, false);
				StudentGlobals.SetStudentBroken(81, true);

				StudentGlobals.SetStudentKidnapped(30, true);
				StudentGlobals.SetStudentSanity(30, 100.0f);

				SchoolGlobals.KidnapVictim = 30;
				HomeGlobals.StartInBasement = true;

				SceneManager.LoadScene(SceneNames.CalendarScene);
			}
		}
        //Exiting scene, Yandere befriended Osana
        else if (this.Phase == 10)
		{
			this.SubDarkness.color = new Color(
				this.SubDarkness.color.r,
				this.SubDarkness.color.g,
				this.SubDarkness.color.b,
				Mathf.MoveTowards(this.SubDarkness.color.a, 1.0f, Time.deltaTime * 0.20f));

			if (this.SubDarkness.color.a == 1.0f)
			{
				StudentGlobals.SetStudentKidnapped(81, false);
				StudentGlobals.SetStudentBroken(81, true);
				SchoolGlobals.KidnapVictim = 0;
				SceneManager.LoadScene(SceneNames.CalendarScene);
			}
		}

		if (Input.GetKeyDown(KeyCode.Minus))
		{
			Time.timeScale -= 1.0f;
		}

		if (Input.GetKeyDown(KeyCode.Equals))
		{
			Time.timeScale += 1.0f;
		}

		this.MyAudio.pitch = Time.timeScale;
	}

	void LateUpdate()
	{
		if (this.Phase > 2)
		{
            if (this.transform.parent != null)
            {
                //Debug.Log(this.FriendshipCamera.position.z);

                if (this.FriendshipCamera.position.z > 2.4f)
                {
                    //Debug.Log("yo");

                    this.transform.localPosition = new Vector3(
                        -1.4f + (this.ShakeStrength * Random.Range(-1.0f, 1.0f)),
                        this.ShakeStrength * Random.Range(-1.0f, 1.0f),
                        this.ShakeStrength * Random.Range(-1.0f, 1.0f));
                }
                else
                {
			        this.transform.localPosition = new Vector3(
                        -.65f + (this.ShakeStrength * Random.Range(-1.0f, 1.0f)),
				        this.ShakeStrength * Random.Range(-1.0f, 1.0f),
				        this.ShakeStrength * Random.Range(-1.0f, 1.0f));
                }
            }

            this.CutsceneCamera.position = new Vector3(
				this.CutsceneCamera.position.x + this.xOffset,
				this.CutsceneCamera.position.y,
				this.CutsceneCamera.position.z + this.zOffset);

			this.LeftEye.localPosition = new Vector3(
				this.LeftEye.localPosition.x,
				this.LeftEye.localPosition.y,
				this.LeftEyeOrigin.z - (this.EyeShrink * 0.010f));

			this.RightEye.localPosition = new Vector3(
				this.RightEye.localPosition.x,
				this.RightEye.localPosition.y,
				this.RightEyeOrigin.z + (this.EyeShrink * 0.010f));

			this.LeftEye.localScale = new Vector3(
				1.0f - (this.EyeShrink * 0.50f),
				1.0f - (this.EyeShrink * 0.50f),
				this.LeftEye.localScale.z);

			this.RightEye.localScale = new Vector3(
				1.0f - (this.EyeShrink * 0.50f),
				1.0f - (this.EyeShrink * 0.50f),
				this.RightEye.localScale.z);
		}
	}

    void IncreaseYandereEffects()
    {
        if (!this.Jukebox.isPlaying)
        {
            this.Jukebox.Play();
        }

        // Over the course of 10 seconds.
        this.Jukebox.volume = Mathf.MoveTowards(
            this.Jukebox.volume, 0.1f, Time.deltaTime * .1f);

        //this.MyAudio.volume = Mathf.MoveTowards(
            //this.MyAudio.volume, 0.5f, Time.deltaTime * .1f);

        this.Vignette.intensity = Mathf.MoveTowards(
            this.Vignette.intensity, 5.0f, (Time.deltaTime * 4.0f) / 10.0f);
        this.Vignette.blur = this.Vignette.intensity;
        this.Vignette.chromaticAberration = this.Vignette.intensity;

        this.ColorCorrection.saturation = Mathf.MoveTowards(
            this.ColorCorrection.saturation, 0.0f, Time.deltaTime / 10.0f);
        this.Noise.intensityMultiplier = Mathf.MoveTowards(
            this.Noise.intensityMultiplier, 3.0f, (Time.deltaTime * 3.0f) / 10.0f);
        this.Obscurance.intensity = Mathf.MoveTowards(
            this.Obscurance.intensity, 3.0f, (Time.deltaTime * 3.0f) / 10.0f);

        if (!this.OsanaCutscene)
        {
            this.ShakeStrength = Mathf.MoveTowards(
                this.ShakeStrength, 0.010f, (Time.deltaTime * 0.010f) / 10.0f);
        }

        // Over the course of 1 second.
        this.EyeShrink = Mathf.MoveTowards(this.EyeShrink, 0.90f, Time.deltaTime);

        if (this.MyAudio.time > 45.0f)
        {
            if (this.MyAudio.time > 54.0f)
            {
                this.EliminationPanel.alpha = Mathf.MoveTowards(
                    this.EliminationPanel.alpha, 0.0f, Time.deltaTime);
            }
            else
            {
                if (!this.OsanaCutscene)
                {
                    this.EliminationPanel.alpha = Mathf.MoveTowards(
                        this.EliminationPanel.alpha, 1.0f, Time.deltaTime);

                    if (Input.GetButtonDown(InputNames.Xbox_X))
                    {
                        this.MyAudio.clip = this.RivalProtest;
                        this.MyAudio.volume = 1.0f;
                        this.MyAudio.Play();

                        // [af] Added "gameObject" for C# compatibility.
                        this.Jukebox.gameObject.SetActive(false);

                        this.Subtitle.text = "Wait, what are you doing?! That's not funny! Stop! Let me go! ...n...NO!!!";
                        this.SubDarknessBG.color = new Color(
                            this.SubDarknessBG.color.r,
                            this.SubDarknessBG.color.g,
                            this.SubDarknessBG.color.b,
                            1.0f);

                        this.Phase++;
                    }
                }
            }
        }
    }

    void DecreaseYandereEffects()
    {
        // Over 5 seconds.
        this.Jukebox.volume = Mathf.MoveTowards(
            this.Jukebox.volume, 0.0f, Time.deltaTime / 5.0f);
        this.MyAudio.volume = Mathf.MoveTowards(
            this.MyAudio.volume, 1.0f, (Time.deltaTime * 0.10f) / 5.0f);

        this.Vignette.intensity = Mathf.MoveTowards(
            this.Vignette.intensity, 1.0f, (Time.deltaTime * 4.0f) / 5.0f);
        this.Vignette.blur = this.Vignette.intensity;
        this.Vignette.chromaticAberration = this.Vignette.intensity;

        this.ColorCorrection.saturation = Mathf.MoveTowards(
            this.ColorCorrection.saturation, 1.0f, Time.deltaTime / 5.0f);
        this.Noise.intensityMultiplier = Mathf.MoveTowards(
            this.Noise.intensityMultiplier, 0.0f, (Time.deltaTime * 3.0f) / 5.0f);
        this.Obscurance.intensity = Mathf.MoveTowards(
            this.Obscurance.intensity, 0.0f, (Time.deltaTime * 3.0f) / 5.0f);

        this.ShakeStrength = Mathf.MoveTowards(
            this.ShakeStrength, 0.0f, (Time.deltaTime * 0.010f) / 5.0f);

        // Over 1 second.
        this.EliminationPanel.alpha = Mathf.MoveTowards(
            this.EliminationPanel.alpha, 0.0f, Time.deltaTime);
        this.EyeShrink = Mathf.MoveTowards(this.EyeShrink, 0.0f, Time.deltaTime);
    }
}
