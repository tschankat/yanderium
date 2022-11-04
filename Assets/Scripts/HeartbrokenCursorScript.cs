using UnityEngine;
using UnityEngine.SceneManagement;

public class HeartbrokenCursorScript : MonoBehaviour
{
	public SnappedYandereScript SnappedYandere;
	public StudentManagerScript StudentManager;
	public InputManagerScript InputManager;
	public HeartbrokenScript Heartbroken;

	public VibrateScript[] Vibrations;

	public UISprite CursorSprite;
	public UISprite Darkness;

	public AudioClip SelectSound;
	public AudioClip MoveSound;
	public AudioSource MyAudio;

	public UILabel Continue;
	public UILabel MyLabel;

	public GameObject FPS;

	public bool LoveSick = false;
	public bool FadeOut = false;
	public bool Nudge = false;

	public int CracksSpawned = 0;
	public int Selected = 1;
	public int Options = 5;

	public int LastRandomCrack = 0;
	public int RandomCrack = 0;

	public CameraFilterPack_Gradients_FireGradient HeartbrokenFilter;
	public CameraFilterPack_Gradients_FireGradient MainFilter;

	public Camera HeartbrokenCamera;

	public AudioSource GameOverMusic;
	public AudioSource SnapStatic;
	public AudioSource SnapMusic;

	public AudioClip GlassShatter;
	public AudioClip ReverseHit;

	public AudioClip[] CrackSound;

	public GameObject ShatterPrefab;
	public GameObject SNAPLetters;
	public GameObject SnapUICamera;

	public UIPanel SNAPPanel;

	public GameObject[] Background;

    public GameObject[] CrackMeshes;
    public GameObject[] Cracks;

	public AudioClip[] CracksTier1;
	public AudioClip[] CracksTier2;
	public AudioClip[] CracksTier3;
	public AudioClip[] CracksTier4;

	public Texture BlackTexture;

	public Transform SnapDestination;
	public Transform SnapFocus;
	public Transform SnapPOV;

    public bool SnapSequence;
    public bool ReloadScene;
    public bool NeverSnap;

	public float SnapTimer;
	public float Speed;

	public int TwitchID;

	void Start()
	{
		this.Darkness.transform.localPosition = new Vector3(
			this.Darkness.transform.localPosition.x,
			this.Darkness.transform.localPosition.y,
			-989.0f);

		this.Continue.color = new Color(
			this.Continue.color.r,
			this.Continue.color.g,
			this.Continue.color.b,
			0.0f);

        if (this.StudentManager != null)
        {
		    this.StudentManager.Yandere.Jukebox.gameObject.SetActive(false);

		    if (this.StudentManager.Yandere.Weapon[1] != null && this.StudentManager.Yandere.Weapon[1].Type == WeaponType.Knife)
		    {
			    this.StudentManager.Yandere.Weapon[1].Drop();
		    }

		    if (this.StudentManager.Yandere.Weapon[2] != null && this.StudentManager.Yandere.Weapon[2].Type == WeaponType.Knife)
		    {
			    this.StudentManager.Yandere.Weapon[2].Drop();
		    }
        }
    }

	void Update()
	{
		//this.StudentManager.Yandere.Twitch = Vector3.Lerp(this.StudentManager.Yandere.Twitch, Vector3.zero, Time.deltaTime * 10.0f);

		this.transform.localPosition = new Vector3(
			this.transform.localPosition.x,
			Mathf.Lerp(this.transform.localPosition.y, 255.0f - (this.Selected * 50.0f), Time.deltaTime * 10.0f),
			this.transform.localPosition.z);

		if (this.Selected == 5)
		{
			GameOverMusic.volume = Mathf.MoveTowards(GameOverMusic.volume, 0, Time.deltaTime * .5f);
		}
		else
		{
			GameOverMusic.volume = Mathf.MoveTowards(GameOverMusic.volume, 1, Time.deltaTime * .5f);
		}

		if (!this.FadeOut)
		{
			if (this.MyLabel.color.a >= 1.0f)
			{
				if (this.InputManager.TappedDown)
				{
					this.Selected++;

					if (this.Selected > this.Options)
					{
						this.Selected = 1;
					}

					MyAudio.clip = this.MoveSound;
					MyAudio.Play();
				}

				if (this.InputManager.TappedUp)
				{
					this.Selected--;

					if (this.Selected < 1)
					{
						this.Selected = this.Options;
					}

					MyAudio.clip = this.MoveSound;
					MyAudio.Play();
				}

				// [af] Replaced if/else statement with assignment and ternary expression.
				this.Continue.color = new Color(
					this.Continue.color.r,
					this.Continue.color.g,
					this.Continue.color.b,
					(this.Selected != 4) ? 1.0f : 0.0f);

				if (Input.GetButtonDown(InputNames.Xbox_A))
				{
					this.Nudge = true;

					if (this.Selected != 5)
					{
						MyAudio.clip = this.SelectSound;
						MyAudio.Play();

                        if (Selected != 3 || Selected == 3 && GameGlobals.MostRecentSlot > 0)
                        {
						    this.FadeOut = true;
                        }
                    }
					else
					{
                        StudentManager.Yandere.ShoulderCamera.enabled = false;

						if (CracksSpawned == 0)
						{
                            GameObjectUtils.SetLayerRecursively(StudentManager.Yandere.gameObject, 5);

                            Cracks[1].transform.parent.position = StudentManager.Yandere.Head.position;
							Cracks[1].transform.parent.position = Vector3.MoveTowards(Cracks[1].transform.parent.position, Heartbroken.transform.parent.position, -1);

                            foreach (VibrateScript vibe in this.Vibrations)
							{
								vibe.enabled = false;
							}

							Heartbroken.Freeze = true;
						}

						if (CracksSpawned < 17)
						{
							Heartbroken.Darken();

							//MyAudio.pitch = Random.Range(.9f, 1.1f);

							while (RandomCrack == LastRandomCrack)
							{
								RandomCrack = Random.Range(0, 3);
							}

							LastRandomCrack = RandomCrack;

							/*
							if (CracksSpawned < 4)
							{
								MyAudio.clip = this.CracksTier1[RandomCrack];
							}
							else if (CracksSpawned < 8)
							{
								MyAudio.clip = this.CracksTier2[RandomCrack];
							}
							else if (CracksSpawned < 12)
							{
								MyAudio.clip = this.CracksTier3[RandomCrack];
							}
							else if (CracksSpawned < 17)
							{
								MyAudio.clip = this.CracksTier4[RandomCrack];
							}
							*/

							MyAudio.clip = this.CrackSound[RandomCrack];
							MyAudio.Play();

							this.TwitchID++;

							if (this.TwitchID > 5)
							{
								this.TwitchID = 0;
							}

							this.StudentManager.Yandere.CharacterAnimation["f02_snapTwitch_0" + this.TwitchID].time = .1f;
							this.StudentManager.Yandere.CharacterAnimation.Play("f02_snapTwitch_0" + this.TwitchID);

							this.StudentManager.MainCamera.Translate(this.StudentManager.MainCamera.forward * .1f, Space.World);
							this.StudentManager.MainCamera.position = new Vector3(
								this.StudentManager.MainCamera.position.x,
								this.StudentManager.MainCamera.position.y - .01f,
								this.StudentManager.MainCamera.position.z);

							int StartCracks = CracksSpawned;

							while (StartCracks == CracksSpawned)
							{
								int RandomNumber = Random.Range(1, Cracks.Length);

								if (!Cracks[RandomNumber].activeInHierarchy)
								{
                                    //CrackMeshes[RandomNumber].SetActive(true);

                                    Cracks[RandomNumber].SetActive(true);
									CracksSpawned++;
								}
							}

							if (NeverSnap && CracksSpawned == 16)
							{
								while (CracksSpawned > 0)
								{
                                    //CrackMeshes[CracksSpawned].SetActive(false);

                                    Cracks[CracksSpawned].SetActive(false);
                                    CracksSpawned--;
								}
							}

							//Debug.Log("Cracks Spawned: " + CracksSpawned);

							StudentManager.SnapSomeStudents();
							StudentManager.OpenSomeDoors();
						}
						else
						{
							int CrackID = 1;

							while (CrackID < Cracks.Length)
							{
                                //CrackMeshes[CracksSpawned].SetActive(false);

                                Cracks[CrackID].GetComponent<UITexture>().fillAmount = 0.425f;
                                Cracks[CrackID].SetActive(false);
                                CrackID++;
							}

							CracksSpawned = 0;

							StudentManager.Yandere.CameraEffects.AlarmBloom.enabled = false;
							StudentManager.Yandere.CameraEffects.QualityBloom.enabled = false;
							StudentManager.Yandere.CameraEffects.QualityVignetting.enabled = false;
							StudentManager.Yandere.CameraEffects.Vignette.enabled = false;
							StudentManager.Yandere.CameraEffects.QualityAntialiasingAsPostEffect.enabled = false;

							StudentManager.Yandere.ColorCorrection.enabled = false;
							StudentManager.Yandere.YandereColorCorrection.enabled = false;
							StudentManager.Yandere.Vignette.enabled = false;
							StudentManager.Yandere.DepthOfField.enabled = false;
							StudentManager.Yandere.Obscurance.enabled = false;

							StudentManager.SelectiveGreyscale.enabled = false;
							StudentManager.Vignettes[2].enabled = false;

							StudentManager.QualityManager.ExperimentalBloomAndLensFlares.enabled = false;

							StudentManager.Yandere.RPGCamera.mouseSpeed = 8;
							StudentManager.Yandere.RPGCamera.distance = .566666f;
							StudentManager.Yandere.RPGCamera.distanceMax = .666666f;
							StudentManager.Yandere.RPGCamera.distanceMin = .666666f;
							StudentManager.Yandere.RPGCamera.desiredDistance = .666666f;

							StudentManager.Yandere.RPGCamera.mouseX = StudentManager.Yandere.transform.eulerAngles.y;
							StudentManager.Yandere.RPGCamera.mouseXSmooth = StudentManager.Yandere.transform.eulerAngles.y;

							StudentManager.Yandere.RPGCamera.mouseY = 15;
							StudentManager.Yandere.RPGCamera.mouseY = 15;

							StudentManager.Yandere.Zoom.OverShoulder = true;
							StudentManager.Yandere.Zoom.TargetZoom = .4f;
							StudentManager.Yandere.Zoom.Zoom = .4f;
							StudentManager.Yandere.Zoom.enabled = false;

							StudentManager.Yandere.RightYandereEye.material.color = new Color(1, 1, 1, 1);
							StudentManager.Yandere.LeftYandereEye.material.color = new Color(1, 1, 1, 1);

							SnapPOV.localPosition = new Vector3(1.25f, 1.546664f, -.5473595f);
							SnapFocus.parent = null;

							StudentManager.Yandere.MainCamera.enabled = true;
							Continue.color = new Color(0, 0, 0, 0);
							MyLabel.color = new Color(0, 0, 0, 0);
							CursorSprite.enabled = false;
							MainFilter.enabled = true;
							FPS.SetActive(false);
							SnapSequence = true;

							MyAudio.clip = GlassShatter;
							MyAudio.volume = 1;
							MyAudio.Play();

							Background[0].SetActive(false);
							Background[1].SetActive(false);
							SNAPLetters.SetActive(false);

							Time.timeScale = .5f;

							GameObject shatter = Instantiate(ShatterPrefab);
							ShatterSpawner shatterScript = shatter.GetComponent<ShatterSpawner>();
							shatterScript.ScreenMaterial.mainTexture = BlackTexture;
							shatterScript.ShatterOrigin = new Vector2(Screen.width * .5f, Screen.height * .5f);

							StudentManager.Yandere.CharacterAnimation["f02_snapRise_00"].speed = 2;
							StudentManager.Yandere.CharacterAnimation.CrossFade("f02_snapRise_00");

							StudentManager.Yandere.enabled = false;

							StudentManager.Students[1].Character.SetActive(true);

							SnapUICamera.SetActive(true);

							StudentManager.SnapSomeStudents();
							StudentManager.OpenSomeDoors();
							StudentManager.DarkenAllStudents();

							StudentManager.Headmaster.gameObject.SetActive(false);
						}
					}
				}
			}
				
			#if UNITY_EDITOR
			if (Input.GetKeyDown("z"))
			{
				NeverSnap = !NeverSnap;
			}
			#endif

			if (this.SnapSequence)
			{
				this.SnapTimer += Time.deltaTime;

				if (this.SnapTimer > 10.0f)
				{
                    GameObjectUtils.SetLayerRecursively(StudentManager.Yandere.gameObject, 13);

                    StudentManager.Yandere.CharacterAnimation["f02_sadEyebrows_00"].weight = 0.0f;

					HeartbrokenCamera.cullingMask = StudentManager.Yandere.MainCamera.cullingMask;
					HeartbrokenCamera.clearFlags = StudentManager.Yandere.MainCamera.clearFlags;
					HeartbrokenCamera.farClipPlane = StudentManager.Yandere.MainCamera.farClipPlane;

					Heartbroken.MainCamera.enabled = false;

					StudentManager.Yandere.RPGCamera.transform.parent = StudentManager.Yandere.transform;
					SnappedYandere.enabled = true;
					SnappedYandere.CanMove = true;

					SnapStatic.Play();
					SnapMusic.Play();
					enabled = false;
					MyAudio.Stop();

					Debug.Log("The player now has control over Yandere-chan again.");
				}
				else if (this.SnapTimer > 3.0f)
				{
					if (MyAudio.clip != ReverseHit)
					{
						MyAudio.clip = ReverseHit;
						MyAudio.time = 1;
						MyAudio.Play();
					}

					Time.timeScale = 1;

					Speed += Time.deltaTime * .5f;

					SnapPOV.localPosition = Vector3.Lerp(
						SnapPOV.localPosition,
						new Vector3(.25f, 1.546664f, -.5473595f),
						Time.deltaTime * Speed);

					StudentManager.MainCamera.position = Vector3.Lerp(
						StudentManager.MainCamera.position,
						SnapPOV.position,
						Time.deltaTime * Speed);

					SnapFocus.position = Vector3.Lerp(
						SnapFocus.position,
						SnapDestination.position,
						Time.deltaTime * Speed);

					StudentManager.MainCamera.LookAt(SnapFocus);

					MainFilter.Fade = Mathf.MoveTowards(MainFilter.Fade, 0, Time.deltaTime * (1.0f/7.0f));
					HeartbrokenFilter.Fade = Mathf.MoveTowards(HeartbrokenFilter.Fade, 1, Time.deltaTime * (1.0f/7.0f));

					SnappedYandere.CompressionFX.Parasite = Mathf.MoveTowards(SnappedYandere.CompressionFX.Parasite, 1, Time.deltaTime * (1.0f/7.0f));
					SnappedYandere.TiltShift.Size = Mathf.MoveTowards(SnappedYandere.TiltShift.Size, .75f, Time.deltaTime * (1.0f/7.0f));
					SnappedYandere.TiltShiftV.Size = Mathf.MoveTowards(SnappedYandere.TiltShiftV.Size, .75f, Time.deltaTime * (1.0f/7.0f));
				}
			}
		}
		else
		{
			this.Heartbroken.GetComponent<AudioSource>().volume -= Time.deltaTime;

			this.Darkness.color = new Color(
				this.Darkness.color.r,
				this.Darkness.color.g,
				this.Darkness.color.b,
				this.Darkness.color.a + Time.deltaTime);

			if (this.Darkness.color.a >= 1.0f)
			{
				if (this.Selected == 1)
				{
                    if (this.ReloadScene)
                    {
                        SceneManager.LoadScene(Application.loadedLevel);
                    }
                    else
                    {
                        for (int ID = 0; ID < this.StudentManager.NPCsTotal; ID++)
					    {
						    if (StudentGlobals.GetStudentDying(ID))
						    {
							    StudentGlobals.SetStudentDying(ID, false);
						    }
					    }

					    SceneManager.LoadScene(SceneNames.LoadingScene);
                    }
                }
				else if (this.Selected == 2)
				{
                    if (this.ReloadScene)
                    {
                        SceneManager.LoadScene(SceneNames.HomeScene);
                    }
                    else
                    {
                        this.LoveSick = GameGlobals.LoveSick;

					    Globals.DeleteAll();

					    GameGlobals.LoveSick = this.LoveSick;

					    SceneManager.LoadScene(SceneNames.CalendarScene);
                    }
                }
                //Loading Most Recent Save
                else if (this.Selected == 3)
                {
                    PlayerPrefs.SetInt("LoadingSave", 1);
                    PlayerPrefs.SetInt("SaveSlot", GameGlobals.MostRecentSlot);

                    for (int ID = 0; ID < this.StudentManager.NPCsTotal; ID++)
                    {
                        if (StudentGlobals.GetStudentDying(ID))
                        {
                            StudentGlobals.SetStudentDying(ID, false);
                        }
                    }

                    SceneManager.LoadScene(SceneNames.LoadingScene);
                }
                else if (this.Selected == 4)
				{
					SceneManager.LoadScene(SceneNames.TitleScene);
				}
			}
		}

		if (this.Nudge)
		{
			this.transform.localPosition = new Vector3(
				this.transform.localPosition.x + (Time.deltaTime * 250.0f),
				this.transform.localPosition.y,
				this.transform.localPosition.z);

			if (this.transform.localPosition.x > -225.0f)
			{
				this.transform.localPosition = new Vector3(
					-225.0f,
					this.transform.localPosition.y,
					this.transform.localPosition.z);

				this.Nudge = false;
			}
		}
		else
		{
			this.transform.localPosition = new Vector3(
				this.transform.localPosition.x - (Time.deltaTime * 250.0f),
				this.transform.localPosition.y,
				this.transform.localPosition.z);

			if (this.transform.localPosition.x < -250.0f)
			{
				this.transform.localPosition = new Vector3(
					-250.0f,
					this.transform.localPosition.y,
					this.transform.localPosition.z);
			}
		}
	}
}
