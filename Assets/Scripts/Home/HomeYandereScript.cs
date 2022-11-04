using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeYandereScript : MonoBehaviour
{
	public CharacterController MyController;

	public AudioSource MyAudio;

	public HomeVideoGamesScript HomeVideoGames;
	public HomeCameraScript HomeCamera;
	public UISprite HomeDarkness;

	public GameObject CutsceneYandere;
	public GameObject Controller;
	public GameObject Character;
	public GameObject Disc;

	public float WalkSpeed = 0.0f;
	public float RunSpeed = 0.0f;

	public bool CanMove = false;
	public bool Running = false;

	public AudioClip MiyukiReaction;
	public AudioClip DiscScratch;

	public Renderer PonytailRenderer;
	public Renderer PigtailR;
	public Renderer PigtailL;
	public Renderer Drills;

	public Transform Ponytail;
	public Transform HairR;
	public Transform HairL;

	public bool HidePony = false;

	public int Hairstyle = 0;
	public int VictimID = 0;

	public float Timer = 0.0f;

	public Texture BlondePony;

	public void Start()
	{
		if (this.CutsceneYandere != null)
		{
			this.CutsceneYandere.GetComponent<Animation>()[AnimNames.FemaleMidoriTexting].speed = 0.10f;
		}

		if (SceneManager.GetActiveScene().name == SceneNames.HomeScene)
		{
			// Start Normally.
			if (!YanvaniaGlobals.DraculaDefeated && !HomeGlobals.MiyukiDefeated)
			{
				this.transform.position = Vector3.zero;
				this.transform.eulerAngles = Vector3.zero;

				if (!HomeGlobals.Night)
				{
					this.ChangeSchoolwear();
					this.StartCoroutine(this.ApplyCustomCostume());
				}
				else
				{
					this.WearPajamas();
				}

				if (DateGlobals.Weekday == System.DayOfWeek.Sunday)
				{
					this.Nude();
				}
			}
			// Start in Basement.
			else if (HomeGlobals.StartInBasement)
			{
				HomeGlobals.StartInBasement = false;

				this.transform.position = new Vector3(0.0f, -135.0f, 0.0f);
				this.transform.eulerAngles = Vector3.zero;
			}
			// Start after Miyuki
			else if (HomeGlobals.MiyukiDefeated)
			{
				this.transform.position = new Vector3(1.0f, 0.0f, 0.0f);
				this.transform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
				this.Character.GetComponent<Animation>().Play(AnimNames.FemaleDiscScratch);

				this.Controller.transform.localPosition = new Vector3(0.09425f, 0.0095f, 0.01878f);
				this.Controller.transform.localEulerAngles = new Vector3(0.0f, 0.0f, -180.0f);

				this.HomeCamera.Destination = this.HomeCamera.Destinations[5];
				this.HomeCamera.Target = this.HomeCamera.Targets[5];

				this.Disc.SetActive(true);

				this.WearPajamas();

				this.MyAudio.clip = this.MiyukiReaction;
			}
			// Start after Yanvania.
			else
			{
				this.transform.position = new Vector3(1.0f, 0.0f, 0.0f);
				this.transform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
				this.Character.GetComponent<Animation>().Play(AnimNames.FemaleDiscScratch);

				this.Controller.transform.localPosition = new Vector3(0.09425f, 0.0095f, 0.01878f);
				this.Controller.transform.localEulerAngles = new Vector3(0.0f, 0.0f, -180.0f);

				this.HomeCamera.Destination = this.HomeCamera.Destinations[5];
				this.HomeCamera.Target = this.HomeCamera.Targets[5];

				this.Disc.SetActive(true);

				this.WearPajamas();
			}

			if (GameGlobals.BlondeHair)
			{
				this.PonytailRenderer.material.mainTexture = this.BlondePony;
			}
		}

		Time.timeScale = 1.0f;

		this.UpdateHair();
	}

	void Update()
	{
		if (!this.Disc.activeInHierarchy)
		{
			Animation charAnimation = this.Character.GetComponent<Animation>();

			if (this.CanMove)
			{
				if (!OptionGlobals.ToggleRun)
				{
					this.Running = false;

					if (Input.GetButton(InputNames.Xbox_LB))
					{
						this.Running = true;
					}
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_LB))
					{
						this.Running = !this.Running;
					}
				}

				this.MyController.Move(Physics.gravity * 0.010f);

				float v = Input.GetAxis("Vertical");
				float h = Input.GetAxis("Horizontal");

				Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
				forward.y = 0.0f;
				forward = forward.normalized;
				Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);

				Vector3 targetDirection = (h * right) + (v * forward);

				if (targetDirection != Vector3.zero)
				{
					Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
					this.transform.rotation = Quaternion.Lerp(
						this.transform.rotation, targetRotation, Time.deltaTime * 10.0f);
				}
				// [af] Removed unnecessary else statement.

				// If we are getting directional input...
				if ((v != 0.0f) || (h != 0.0f))
				{
					// If the Run button is held down...
					if (this.Running)
					{
						// Run animation.
						charAnimation.CrossFade(AnimNames.FemaleRun);
						this.MyController.Move(this.transform.forward * this.RunSpeed * Time.deltaTime);
					}
					else
					{
						// Walk animation.
						charAnimation.CrossFade(AnimNames.FemaleNewWalk);
						this.MyController.Move(this.transform.forward * this.WalkSpeed * Time.deltaTime);
					}
				}
				else
				{
					// Idle animation.
					charAnimation.CrossFade(AnimNames.FemaleIdleShort);
				}
			}
			else
			{
				// Idle animation.
				charAnimation.CrossFade(AnimNames.FemaleIdleShort);
			}
		}
		else
		{
			if (this.HomeDarkness.color.a == 0.0f)
			{
				if (this.Timer == 0.0f)
				{
					MyAudio.Play();
				}
				else if (this.Timer > (MyAudio.clip.length + 1.0f))
				{
					YanvaniaGlobals.DraculaDefeated = false;
					HomeGlobals.MiyukiDefeated = false;
					this.Disc.SetActive(false);
					this.HomeVideoGames.Quit();
				}

				this.Timer += Time.deltaTime;
			}
		}

		Rigidbody rigidBody = this.GetComponent<Rigidbody>();

		if (rigidBody != null)
		{
			rigidBody.velocity = Vector3.zero;
		}

		if (Input.GetKeyDown(KeyCode.H))
		{
			this.UpdateHair();
		}

		if (Input.GetKeyDown(KeyCode.K))
		{
            SchemeGlobals.HelpingKokona = true;
            SchoolGlobals.KidnapVictim = this.VictimID;
			StudentGlobals.SetStudentSanity(this.VictimID, 100.0f);
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		#if UNITY_EDITOR

		if (Input.GetKeyDown(KeyCode.Y))
		{
			YanvaniaGlobals.DraculaDefeated = true;

			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		#endif

		if (Input.GetKeyDown(KeyCode.F1))
		{
			StudentGlobals.MaleUniform = 1;
			StudentGlobals.FemaleUniform = 1;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		else if (Input.GetKeyDown(KeyCode.F2))
		{
			StudentGlobals.MaleUniform = 2;
			StudentGlobals.FemaleUniform = 2;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		else if (Input.GetKeyDown(KeyCode.F3))
		{
			StudentGlobals.MaleUniform = 3;
			StudentGlobals.FemaleUniform = 3;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		else if (Input.GetKeyDown(KeyCode.F4))
		{
			StudentGlobals.MaleUniform = 4;
			StudentGlobals.FemaleUniform = 4;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		else if (Input.GetKeyDown(KeyCode.F5))
		{
			StudentGlobals.MaleUniform = 5;
			StudentGlobals.FemaleUniform = 5;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		else if (Input.GetKeyDown(KeyCode.F6))
		{
			StudentGlobals.MaleUniform = 6;
			StudentGlobals.FemaleUniform = 6;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		if (this.transform.position.y < -10)
		{
			this.transform.position = new Vector3(transform.position.x, -10, transform.position.z);
		}
	}

	public int AlphabetID = 0;
	public string[] Letter;

	void LateUpdate()
	{
		if (this.HidePony)
		{
			this.Ponytail.parent.transform.localScale = new Vector3(1.0f, 1.0f, 0.93f);
			this.Ponytail.localScale = new Vector3(0.00010f, 0.00010f, 0.00010f);
			this.HairR.localScale = new Vector3(0.00010f, 0.00010f, 0.00010f);
			this.HairL.localScale = new Vector3(0.00010f, 0.00010f, 0.00010f);
		}
			
		if (Input.GetKeyDown(Letter[AlphabetID]))
		{
			AlphabetID++;

			if (AlphabetID == Letter.Length)
			{
				GameGlobals.AlphabetMode = true;
                StudentGlobals.MemorialStudents = 0;

			    int tempID = 1;

			    while (tempID < 101)
			    {
                    StudentGlobals.SetStudentDead(tempID, false);
					StudentGlobals.SetStudentKidnapped(tempID, false);
                    StudentGlobals.SetStudentArrested(tempID, false);
                    StudentGlobals.SetStudentExpelled(tempID, false);

                    tempID++;
			    }

                SceneManager.LoadScene("LoadingScene");
			}
		}
	}

	void UpdateHair()
	{
		// [af] This is hard to read... refactor eventually.
		this.PigtailR.transform.parent.transform.parent.transform.localScale = new Vector3(1.0f, 0.75f, 1.0f);
		this.PigtailL.transform.parent.transform.parent.transform.localScale = new Vector3(1.0f, 0.75f, 1.0f);

		// [af] Added "gameObject" for C# compatibility.
		this.PigtailR.gameObject.SetActive(false);
		this.PigtailL.gameObject.SetActive(false);
		this.Drills.gameObject.SetActive(false);
		this.HidePony = true;

		this.Hairstyle++;

		if (this.Hairstyle > 7)
		{
			this.Hairstyle = 1;
		}

		if (this.Hairstyle == 1)
		{
			this.HidePony = false;
			this.Ponytail.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			this.HairR.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			this.HairL.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		}
		else if (this.Hairstyle == 2)
		{
			this.PigtailR.gameObject.SetActive(true);
		}
		else if (this.Hairstyle == 3)
		{
			this.PigtailL.gameObject.SetActive(true);
		}
		else if (this.Hairstyle == 4)
		{
			this.PigtailR.gameObject.SetActive(true);
			this.PigtailL.gameObject.SetActive(true);
		}
		else if (this.Hairstyle == 5)
		{
			this.PigtailR.gameObject.SetActive(true);
			this.PigtailL.gameObject.SetActive(true);
			this.HidePony = false;
			this.Ponytail.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			this.HairR.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			this.HairL.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		}
		else if (this.Hairstyle == 6)
		{
			this.PigtailR.gameObject.SetActive(true);
			this.PigtailL.gameObject.SetActive(true);

			// [af] This is hard to read... refactor eventually.
			this.PigtailR.transform.parent.transform.parent.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
			this.PigtailL.transform.parent.transform.parent.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
		}
		else if (this.Hairstyle == 7)
		{
			this.Drills.gameObject.SetActive(true);
		}
	}

	public SkinnedMeshRenderer MyRenderer;
	public Texture[] UniformTextures;
	public Texture FaceTexture;
	public Mesh[] Uniforms;

	void ChangeSchoolwear()
	{
		this.MyRenderer.sharedMesh = this.Uniforms[StudentGlobals.FemaleUniform];

		this.MyRenderer.materials[0].mainTexture = this.UniformTextures[StudentGlobals.FemaleUniform];
		this.MyRenderer.materials[1].mainTexture = this.UniformTextures[StudentGlobals.FemaleUniform];
		this.MyRenderer.materials[2].mainTexture = this.FaceTexture;

		this.StartCoroutine(this.ApplyCustomCostume());
	}

	public Texture PajamaTexture;
	public Mesh PajamaMesh;

	void WearPajamas()
	{
		this.MyRenderer.sharedMesh = this.PajamaMesh;

		this.MyRenderer.materials[0].mainTexture = this.PajamaTexture;
		this.MyRenderer.materials[1].mainTexture = this.PajamaTexture;
		this.MyRenderer.materials[2].mainTexture = this.FaceTexture;

		this.StartCoroutine(this.ApplyCustomFace());
	}

	public Texture NudeTexture;
	public Mesh NudeMesh;

	void Nude()
	{
		this.MyRenderer.sharedMesh = this.NudeMesh;

		this.MyRenderer.materials[0].mainTexture = this.NudeTexture;
		this.MyRenderer.materials[1].mainTexture = this.NudeTexture;
		this.MyRenderer.materials[2].mainTexture = this.NudeTexture;
	}

	IEnumerator ApplyCustomCostume()
	{
		if (StudentGlobals.FemaleUniform == 1)
		{
			WWW CustomUniform = new WWW("file:///" +
				Application.streamingAssetsPath + "/CustomUniform.png");

			yield return CustomUniform;

			if (CustomUniform.error == null)
			{
				this.MyRenderer.materials[0].mainTexture = CustomUniform.texture;
				this.MyRenderer.materials[1].mainTexture = CustomUniform.texture;
			}
		}
		else if (StudentGlobals.FemaleUniform == 2)
		{
			WWW CustomLong = new WWW("file:///" +
				Application.streamingAssetsPath + "/CustomLong.png");

			yield return CustomLong;

			if (CustomLong.error == null)
			{
				this.MyRenderer.materials[0].mainTexture = CustomLong.texture;
				this.MyRenderer.materials[1].mainTexture = CustomLong.texture;
			}
		}
		else if (StudentGlobals.FemaleUniform == 3)
		{
			WWW CustomSweater = new WWW("file:///" +
				Application.streamingAssetsPath + "/CustomSweater.png");

			yield return CustomSweater;

			if (CustomSweater.error == null)
			{
				this.MyRenderer.materials[0].mainTexture = CustomSweater.texture;
				this.MyRenderer.materials[1].mainTexture = CustomSweater.texture;
			}
		}
		else if ((StudentGlobals.FemaleUniform == 4) || (StudentGlobals.FemaleUniform == 5))
		{
			WWW CustomBlazer = new WWW("file:///" +
				Application.streamingAssetsPath + "/CustomBlazer.png");

			yield return CustomBlazer;

			if (CustomBlazer.error == null)
			{
				this.MyRenderer.materials[0].mainTexture = CustomBlazer.texture;
				this.MyRenderer.materials[1].mainTexture = CustomBlazer.texture;
			}
		}

		this.StartCoroutine(this.ApplyCustomFace());
	}

	IEnumerator ApplyCustomFace()
	{
		WWW CustomFace = new WWW("file:///" +
			Application.streamingAssetsPath + "/CustomFace.png");

		yield return CustomFace;

		if (CustomFace.error == null)
		{
			this.MyRenderer.materials[2].mainTexture = CustomFace.texture;
			this.FaceTexture = CustomFace.texture;
		}

		WWW CustomHair = new WWW("file:///" +
			Application.streamingAssetsPath + "/CustomHair.png");

		yield return CustomHair;

		if (CustomHair.error == null)
		{
			this.PonytailRenderer.material.mainTexture = CustomHair.texture;
			this.PigtailR.material.mainTexture = CustomHair.texture;
			this.PigtailL.material.mainTexture = CustomHair.texture;
		}

		WWW CustomDrills = new WWW("file:///" +
			Application.streamingAssetsPath + "/CustomDrills.png");

		yield return CustomDrills;

		if (CustomDrills.error == null)
		{
			this.Drills.materials[0].mainTexture = CustomDrills.texture;
			this.Drills.materials[1].mainTexture = CustomDrills.texture;
			this.Drills.materials[2].mainTexture = CustomDrills.texture;
		}
	}
}
