using UnityEngine;
using UnityEngine.SceneManagement;

public class ArmDetectorScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public DebugMenuScript DebugMenu;
	public JukeboxScript Jukebox;
	public YandereScript Yandere;
	public PoliceScript Police;
	public SkullScript Skull;

	public UILabel DemonSubtitle;
	public UISprite Darkness;

	public Transform LimbParent;

	public Transform[] SpawnPoints;
	public GameObject[] BodyArray;
	public GameObject[] ArmArray;

	public GameObject RiggedAccessory;
	public GameObject BloodProjector;
	public GameObject SmallDarkAura;
	public GameObject DemonDress;
	public GameObject RightFlame;
	public GameObject LeftFlame;
	public GameObject DemonArm;

	public bool SummonEmptyDemon = false;
	public bool SummonFlameDemon = false;
	public bool SummonDemon = false;

	public Mesh FlameDemonMesh;

	public int CorpsesCounted = 0;
	public int ArmsSpawned = 0;
	public int Sacrifices = 0;
	public int Phase = 1;

	public int Bodies = 0;
	public int Arms = 0;

	public float Timer = 0.0f;

	public AudioClip FlameDemonLine;
	public AudioClip FlameActivate;

	public AudioClip DemonMusic;
	public AudioClip DemonLine;

	public AudioClip EmptyDemonLine;

    public AudioSource MyAudio;

	void Start()
	{
        this.DemonDress.SetActive(false);
	}

	void Update()
	{
		if (!this.SummonDemon)
		{
			// [af] Converted while loop to for loop.
			for (int ID = 1; ID < this.ArmArray.Length; ID++)
			{
				if (this.ArmArray[ID] != null)
				{
					if (this.ArmArray[ID].transform.parent != LimbParent)
					{
						this.ArmArray[ID] = null;

						if (ID != (this.ArmArray.Length - 1))
						{
							this.Shuffle(ID);
						}

						this.Arms--;

						Debug.Log("Decrement arm count?");
					}
				}
			}

			if (this.Arms > 9)
			{
				this.Yandere.Character.GetComponent<Animation>().CrossFade(this.Yandere.IdleAnim);
				this.Yandere.CanMove = false;
				this.SummonDemon = true;
				this.MyAudio.Play();
				this.Arms = 0;
			}
		}

		if (!this.SummonFlameDemon)
		{
			this.CorpsesCounted = 0;
			this.Sacrifices = 0;

			// [af] Converted while loop to for loop.
			for (int ID = 0; this.CorpsesCounted < this.Police.Corpses; ID++)
			{
				RagdollScript ragdoll = this.Police.CorpseList[ID];

				if (ragdoll != null)
				{
					this.CorpsesCounted++;

					if (ragdoll.Burned && ragdoll.Sacrifice &&
						!ragdoll.Dragged && !ragdoll.Carried)
					{
						this.Sacrifices++;
					}
				}
			}

			if (this.Sacrifices > 4)
			{
				if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
				{
					this.Yandere.Character.GetComponent<Animation>().CrossFade(this.Yandere.IdleAnim);
					this.Yandere.CanMove = false;
					this.SummonFlameDemon = true;
					this.MyAudio.Play();
				}
			}
		}

		if (!this.SummonEmptyDemon)
		{
			if (this.Bodies > 10)
			{
				if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
				{
					this.Yandere.Character.GetComponent<Animation>().CrossFade(this.Yandere.IdleAnim);
					this.Yandere.CanMove = false;
					this.SummonEmptyDemon = true;
					this.MyAudio.Play();
				}
			}
		}

		////////////////////////////////////
		///// SUMMONING THE PAIN DEMON /////
		////////////////////////////////////

		if (this.SummonDemon)
		{
			if (this.Phase == 1)
			{
				if (this.ArmArray[1] != null)
				{
					for (int ID = 1; ID < 11; ID++)
					{
						if (this.ArmArray[ID] != null)
						{
							Instantiate(this.SmallDarkAura,
								this.ArmArray[ID].transform.position, Quaternion.identity);
							Destroy(this.ArmArray[ID]);
						}
					}
				}

				this.Timer += Time.deltaTime;

				if (this.Timer > 1.0f)
				{
					this.Timer = 0.0f;
					this.Phase++;
				}
			}
			else if (this.Phase == 2)
			{
				this.Darkness.color = new Color(
					this.Darkness.color.r,
					this.Darkness.color.g,
					this.Darkness.color.b,
					Mathf.MoveTowards(this.Darkness.color.a, 1.0f, Time.deltaTime));

				this.Jukebox.Volume = Mathf.MoveTowards(this.Jukebox.Volume, 0.0f, Time.deltaTime);

				if (this.Darkness.color.a == 1.0f)
				{
					SchoolGlobals.SchoolAtmosphere = 0.0f;
					this.StudentManager.SetAtmosphere();

					this.Yandere.transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
					this.Yandere.transform.position = new Vector3(12.0f, 0.10f, 26.0f);
					this.DemonSubtitle.text = "...revenge...at last...";
					this.BloodProjector.SetActive(true);

					this.DemonSubtitle.color = new Color(
						this.DemonSubtitle.color.r,
						this.DemonSubtitle.color.g,
						this.DemonSubtitle.color.b,
						0.0f);

					this.Skull.Prompt.Hide();
					this.Skull.Prompt.enabled = false;
					this.Skull.enabled = false;

					this.MyAudio.clip = this.DemonLine;
                    this.MyAudio.Play();

					this.Phase++;
				}
			}
			else if (this.Phase == 3)
			{
				this.DemonSubtitle.transform.localPosition = new Vector3(
					Random.Range(-10.0f, 10.0f),
					Random.Range(-10.0f, 10.0f),
					Random.Range(-10.0f, 10.0f));

				this.DemonSubtitle.color = new Color(
					this.DemonSubtitle.color.r,
					this.DemonSubtitle.color.g,
					this.DemonSubtitle.color.b,
					Mathf.MoveTowards(this.DemonSubtitle.color.a, 1.0f, Time.deltaTime));

				if (this.DemonSubtitle.color.a == 1.0f)
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.Phase++;
					}
				}
			}
			else if (this.Phase == 4)
			{
				this.DemonSubtitle.transform.localPosition = new Vector3(
					Random.Range(-10.0f, 10.0f),
					Random.Range(-10.0f, 10.0f),
					Random.Range(-10.0f, 10.0f));

				this.DemonSubtitle.color = new Color(
					this.DemonSubtitle.color.r,
					this.DemonSubtitle.color.g,
					this.DemonSubtitle.color.b,
					Mathf.MoveTowards(this.DemonSubtitle.color.a, 0.0f, Time.deltaTime));

				if (this.DemonSubtitle.color.a == 0.0f)
				{
                    this.MyAudio.clip = this.DemonMusic;
                    this.MyAudio.loop = true;
                    this.MyAudio.Play();

					this.DemonSubtitle.text = string.Empty;
					this.Phase++;
				}
			}
			else if (this.Phase == 5)
			{
				this.Darkness.color = new Color(
					this.Darkness.color.r,
					this.Darkness.color.g,
					this.Darkness.color.b,
					Mathf.MoveTowards(this.Darkness.color.a, 0.0f, Time.deltaTime));

				if (this.Darkness.color.a == 0.0f)
				{
					this.Yandere.Character.GetComponent<Animation>().CrossFade(AnimNames.FemaleDemonSummon);
					this.Phase++;
				}
			}
			else if (this.Phase == 6)
			{
				this.Timer += Time.deltaTime;

				if (this.Timer > this.ArmsSpawned)
				{
					GameObject NewArm = Instantiate(this.DemonArm,
						this.SpawnPoints[this.ArmsSpawned].position, Quaternion.identity);
					NewArm.transform.parent = this.Yandere.transform;
					NewArm.transform.LookAt(this.Yandere.transform);
					NewArm.transform.localEulerAngles = new Vector3(
						NewArm.transform.localEulerAngles.x,
						NewArm.transform.localEulerAngles.y + 180.0f,
						NewArm.transform.localEulerAngles.z);

					this.ArmsSpawned++;

					// [af] Replaced if/else statement with ternary expression.
					NewArm.GetComponent<DemonArmScript>().IdleAnim = ((this.ArmsSpawned % 2) == 1) ? 
						AnimNames.DemonArmIdleOld : AnimNames.DemonArmIdle;
				}

				if (this.ArmsSpawned == 10)
				{
					this.Yandere.CanMove = true;
					this.Yandere.IdleAnim = AnimNames.FemaleDemonIdle;
					this.Yandere.WalkAnim = AnimNames.FemaleDemonWalk;
					this.Yandere.RunAnim = AnimNames.FemaleDemonRun;
					this.Yandere.Demonic = true;
					this.SummonDemon = false;
				}
			}
		}

		/////////////////////////////////////
		///// SUMMONING THE FLAME DEMON /////
		/////////////////////////////////////

		if (this.SummonFlameDemon)
		{
			if (this.Phase == 1)
			{
				// [af] Converted while loop to foreach loop.
				foreach (RagdollScript ragdoll in this.Police.CorpseList)
				{
					if (ragdoll != null)
					{
						if (ragdoll.Burned && ragdoll.Sacrifice &&
							!ragdoll.Dragged && !ragdoll.Carried)
						{
							Instantiate(this.SmallDarkAura,
								ragdoll.Prompt.transform.position, Quaternion.identity);
							Destroy(ragdoll.gameObject);
							this.Yandere.NearBodies--;
							this.Police.Corpses--;
						}
					}
				}

				this.Phase++;
			}
			else if (this.Phase == 2)
			{
				this.Timer += Time.deltaTime;

				if (this.Timer > 1.0f)
				{
					this.Timer = 0.0f;
					this.Phase++;
				}
			}
			else if (this.Phase == 3)
			{
				this.Darkness.color = new Color(
					this.Darkness.color.r,
					this.Darkness.color.g,
					this.Darkness.color.b,
					Mathf.MoveTowards(this.Darkness.color.a, 1.0f, Time.deltaTime));

				this.Jukebox.Volume = Mathf.MoveTowards(this.Jukebox.Volume, 0.0f, Time.deltaTime);

				if (this.Darkness.color.a == 1.0f)
				{
					this.Yandere.transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
					this.Yandere.transform.position = new Vector3(12.0f, 0.10f, 26.0f);
					this.DemonSubtitle.text = "You have proven your worth. Very well. I shall lend you my power.";
					this.DemonSubtitle.color = new Color(1.0f, 0.0f, 0.0f, 0.0f);

					this.Skull.Prompt.Hide();
					this.Skull.Prompt.enabled = false;
					this.Skull.enabled = false;

                    this.MyAudio.clip = this.FlameDemonLine;
                    this.MyAudio.Play();

					this.Phase++;
				}
			}
			else if (this.Phase == 4)
			{
				this.DemonSubtitle.transform.localPosition = new Vector3(
					Random.Range(-5.0f, 5.0f),
					Random.Range(-5.0f, 5.0f),
					Random.Range(-5.0f, 5.0f));

				this.DemonSubtitle.color = new Color(
					this.DemonSubtitle.color.r,
					this.DemonSubtitle.color.g,
					this.DemonSubtitle.color.b,
					Mathf.MoveTowards(this.DemonSubtitle.color.a, 1.0f, Time.deltaTime));

				if (this.DemonSubtitle.color.a == 1.0f)
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.Phase++;
					}
				}
			}
			else if (this.Phase == 5)
			{
				this.DemonSubtitle.transform.localPosition = new Vector3(
					Random.Range(-10.0f, 10.0f),
					Random.Range(-10.0f, 10.0f),
					Random.Range(-10.0f, 10.0f));

				this.DemonSubtitle.color = new Color(
					this.DemonSubtitle.color.r,
					this.DemonSubtitle.color.g,
					this.DemonSubtitle.color.b,
					Mathf.MoveTowards(this.DemonSubtitle.color.a, 0.0f, Time.deltaTime));

				if (this.DemonSubtitle.color.a == 0.0f)
				{
					this.DemonDress.SetActive(true);

					this.Yandere.MyRenderer.sharedMesh = this.FlameDemonMesh;
					this.RiggedAccessory.SetActive(true);
					this.Yandere.FlameDemonic = true;

					this.Yandere.Stance.Current = StanceType.Standing;
					this.Yandere.Sanity = 100.0f;

					this.Yandere.MyRenderer.materials[0].mainTexture = this.Yandere.FaceTexture;
					this.Yandere.MyRenderer.materials[1].mainTexture = this.Yandere.NudePanties;
					this.Yandere.MyRenderer.materials[2].mainTexture = this.Yandere.NudePanties;

					this.DebugMenu.UpdateCensor();

                    this.MyAudio.clip = this.DemonMusic;
                    this.MyAudio.loop = true;
                    this.MyAudio.Play();

					this.DemonSubtitle.text = string.Empty;
					this.Phase++;
				}
			}
			else if (this.Phase == 6)
			{
				this.Darkness.color = new Color(
					this.Darkness.color.r,
					this.Darkness.color.g,
					this.Darkness.color.b,
					Mathf.MoveTowards(this.Darkness.color.a, 0.0f, Time.deltaTime));

				if (this.Darkness.color.a == 0.0f)
				{
					this.Yandere.Character.GetComponent<Animation>().CrossFade(
						AnimNames.FemaleDemonSummon);
					this.Phase++;
				}
			}
			else if (this.Phase == 7)
			{
				this.Timer += Time.deltaTime;

				if (this.Timer > 5.0f)
				{
                    this.MyAudio.PlayOneShot(this.FlameActivate);
					this.RightFlame.SetActive(true);
					this.LeftFlame.SetActive(true);

					this.Phase++;
				}
			}
			else if (this.Phase == 8)
			{
				this.Timer += Time.deltaTime;

				if (this.Timer > 10.0f)
				{
					this.Yandere.CanMove = true;
					this.Yandere.IdleAnim = AnimNames.FemaleDemonIdle;
					this.Yandere.WalkAnim = AnimNames.FemaleDemonWalk;
					this.Yandere.RunAnim = AnimNames.FemaleDemonRun;
					this.SummonFlameDemon = false;
				}
			}
		}

		/////////////////////////////////////
		///// SUMMONING THE EMPTY DEMON /////
		/////////////////////////////////////

		if (this.SummonEmptyDemon)
		{
			if (this.Phase == 1)
			{
				if (this.BodyArray[1] != null)
				{
					for (int ID = 1; ID < 12; ID++)
					{
						if (this.BodyArray[ID] != null)
						{
							Instantiate(this.SmallDarkAura,
								this.BodyArray[ID].transform.position, Quaternion.identity);
							Destroy(this.BodyArray[ID]);
						}
					}
				}

				this.Timer += Time.deltaTime;

				if (this.Timer > 1.0f)
				{
					this.Timer = 0.0f;
					this.Phase++;
				}
			}
			else if (this.Phase == 2)
			{
				this.Darkness.color = new Color(
					this.Darkness.color.r,
					this.Darkness.color.g,
					this.Darkness.color.b,
					Mathf.MoveTowards(this.Darkness.color.a, 1.0f, Time.deltaTime));

				this.Jukebox.Volume = Mathf.MoveTowards(this.Jukebox.Volume, 0.0f, Time.deltaTime);

				if (this.Darkness.color.a == 1.0f)
				{
					this.Yandere.transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
					this.Yandere.transform.position = new Vector3(12.0f, 0.10f, 26.0f);
					this.DemonSubtitle.text = "At last...it is time to reclaim our rightful place.";
					this.BloodProjector.SetActive(true);

					this.DemonSubtitle.color = new Color(
						this.DemonSubtitle.color.r,
						this.DemonSubtitle.color.g,
						this.DemonSubtitle.color.b,
						0.0f);

					this.Skull.Prompt.Hide();
					this.Skull.Prompt.enabled = false;
					this.Skull.enabled = false;

                    this.MyAudio.clip = this.EmptyDemonLine;
                    this.MyAudio.Play();

					this.Phase++;
				}
			}
			else if (this.Phase == 3)
			{
				this.DemonSubtitle.transform.localPosition = new Vector3(
					Random.Range(-10.0f, 10.0f),
					Random.Range(-10.0f, 10.0f),
					Random.Range(-10.0f, 10.0f));

				this.DemonSubtitle.color = new Color(
					this.DemonSubtitle.color.r,
					this.DemonSubtitle.color.g,
					this.DemonSubtitle.color.b,
					Mathf.MoveTowards(this.DemonSubtitle.color.a, 1.0f, Time.deltaTime));

				if (this.DemonSubtitle.color.a == 1.0f)
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.Phase++;
					}
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
					GameGlobals.EmptyDemon = true;
					SceneManager.LoadScene(SceneNames.LoadingScene);
				}
			}
		}

#if UNITY_EDITOR
		// [af] Commented in JS code.
		/*
		if (Input.GetKeyDown(KeyCode.Space))
		{
			this.Yandere.Character.animation.CrossFade(this.Yandere.IdleAnim);
			this.Yandere.CanMove = false;
			this.SummonFlameDemon = true;
			audioSource.Play();
		}
		*/
#endif
	}

	void OnTriggerEnter(Collider other)
	{
		//Debug.Log("Trigger collision!");

		if (other.transform.parent == LimbParent)
		{
			//Debug.Log("The object is a child of LimbParent!");

			PickUpScript pickUpScript = other.gameObject.GetComponent<PickUpScript>();

			if (pickUpScript != null)
			{
				//Debug.Log("The object has a PickUpScript!");

				BodyPartScript BodyPart = pickUpScript.BodyPart;

				if (BodyPart.Sacrifice)
				{
					//Debug.Log("The object is a sacrifice!");

					if (BodyPart.Type == 3 || BodyPart.Type == 4)
					{
						//Debug.Log("The object is an arm or a leg!");

						bool Add = true;

						// [af] Converted while loop to for loop.
						for (int ID = 1; ID < 11; ID++)
						{
							if (this.ArmArray[ID] == other.gameObject)
							{
								Add = false;
							}
						}

						if (Add)
						{
							//Debug.Log("Increment arm count!");

							this.Arms++;

							if (this.Arms < this.ArmArray.Length)
							{
								this.ArmArray[this.Arms] = other.gameObject;
							}
						}
					}
				}
			}
		}

		if (other.transform.parent != null)
		{
			if (other.transform.parent.parent != null)
			{
				if (other.transform.parent.parent.parent != null)
				{
					StudentScript student = other.transform.parent.parent.parent.gameObject.GetComponent<StudentScript>();

					//Debug.Log(other.transform.parent.parent.parent.gameObject.name);

					if (student != null)
					{
						if (student.Ragdoll.Sacrifice)
						{
							if (student.Armband.activeInHierarchy)
							{
								bool Add = true;

								for (int ID = 1; ID < 11; ID++)
								{
									if (this.BodyArray[ID] == other.gameObject)
									{
										Add = false;
									}
								}

								if (Add)
								{
									this.Bodies++;

									if (this.Bodies < this.BodyArray.Length)
									{
										this.BodyArray[this.Bodies] = other.gameObject;
									}
								}
							}
						}
					}
				}
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		PickUpScript pickUpScript = other.gameObject.GetComponent<PickUpScript>();

		if (pickUpScript != null)
		{
			if (pickUpScript.BodyPart)
			{
				BodyPartScript bodyPartScript = other.gameObject.GetComponent<BodyPartScript>();

				if (bodyPartScript.Sacrifice)
				{
					if ((other.gameObject.name == "FemaleRightArm(Clone)") ||
						(other.gameObject.name == "FemaleLeftArm(Clone)") ||
						(other.gameObject.name == "MaleRightArm(Clone)") ||
						(other.gameObject.name == "MaleLeftArm(Clone)") ||
						(other.gameObject.name == "SacrificialArm(Clone)"))
					{
						this.Arms--;

						Debug.Log("Decrement arm count again?");
					}
				}
			}
		}

		if (other.transform.parent != null)
		{
			if (other.transform.parent.parent != null)
			{
				if (other.transform.parent.parent.parent != null)
				{
					StudentScript student = other.transform.parent.parent.parent.gameObject.GetComponent<StudentScript>();

					//Debug.Log(other.transform.parent.parent.parent.gameObject.name);

					if (student != null)
					{
						if (student.Ragdoll.Sacrifice)
						{
							if (student.Armband.activeInHierarchy)
							{
								this.Bodies--;
							}
						}
					}
				}
			}
		}
	}

	void Shuffle(int Start)
	{
		for (int ShuffleID = Start; ShuffleID < (this.ArmArray.Length - 1); ShuffleID++)
		{
			this.ArmArray[ShuffleID] = this.ArmArray[ShuffleID + 1];
		}
	}

	void ShuffleBodies(int Start)
	{
		for (int ShuffleID = Start; ShuffleID < (this.BodyArray.Length - 1); ShuffleID++)
		{
			this.BodyArray[ShuffleID] = this.BodyArray[ShuffleID + 1];
		}
	}
}