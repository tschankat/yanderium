using UnityEngine;

public class TallLockerScript : MonoBehaviour
{
	public GameObject[] BloodyClubUniform;
	public GameObject[] BloodyUniform;
	public GameObject[] Schoolwear;
	public bool[] Removed;
	public bool[] Bloody;

	public GameObject CleanUniform;
	public GameObject SteamCloud;

	public StudentManagerScript StudentManager;
	public RivalPhoneScript RivalPhone;
	public StudentScript Student;
	public YandereScript Yandere;
	public PromptScript Prompt;

	public Transform Hinge;

	public bool RemovingClubAttire = false;
	public bool DropCleanUniform = false;
	public bool SteamCountdown = false;
	public bool YandereLocker = false;
	public bool Swapping = false;
	public bool Open = false;

	public float Rotation = 0.0f;
	public float Timer = 0.0f;

	public int AvailableUniforms = 2;
	public int Phase = 1;

	void Start()
	{
		// [af] Commented in JS code.
		//UpdateSchoolwear();

		this.Prompt.HideButton[1] = true;
		this.Prompt.HideButton[2] = true;
		this.Prompt.HideButton[3] = true;
	}

	void Update()
	{
		//Player holds down A button to open locker...
		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
				this.Prompt.Circle[0].fillAmount = 1.0f;

				//If the locker wasn't open already...
				if (!this.Open)
				{
					//Open the locker now.
					this.Open = true;

					if (this.YandereLocker)
					{
						if (!this.Yandere.ClubAttire || this.Yandere.ClubAttire &&
							(this.Yandere.Bloodiness > 0.0f))
						{
							if (this.Yandere.Bloodiness == 0.0f)
							{
								if (!this.Bloody[1])
								{
									this.Prompt.HideButton[1] = false;
								}

								if (!this.Bloody[2])
								{
									this.Prompt.HideButton[2] = false;
								}

								if (!this.Bloody[3])
								{
									this.Prompt.HideButton[3] = false;
								}
							}
							else
							{
								if (this.Yandere.Schoolwear > 0)
								{
									if (!this.Yandere.ClubAttire)
									{
										this.Prompt.HideButton[this.Yandere.Schoolwear] = false;
									}
									else
									{
										this.Prompt.HideButton[1] = false;
									}
								}
							}
						}
						else
						{
							this.Prompt.HideButton[1] = true;
							this.Prompt.HideButton[2] = true;
							this.Prompt.HideButton[3] = true;
						}
					}

					this.UpdateSchoolwear();

					this.Prompt.Label[0].text = "     " + "Close";
				}
				else
				{
					this.Open = false;

					this.Prompt.HideButton[1] = true;
					this.Prompt.HideButton[2] = true;
					this.Prompt.HideButton[3] = true;

					this.Prompt.Label[0].text = "     " + "Open";
				}
			}
		}

		if (!this.Open)
		{
			if (this.YandereLocker)
			{
				this.Rotation = Mathf.Lerp(this.Rotation, 0.0f, Time.deltaTime * 10.0f);
			}

			this.Prompt.HideButton[1] = true;
			this.Prompt.HideButton[2] = true;
			this.Prompt.HideButton[3] = true;
		}
		else
		{
			if (this.YandereLocker)
			{
				this.Rotation = Mathf.Lerp(this.Rotation, -180.0f, Time.deltaTime * 10.0f);
			}

			if (this.Prompt.Circle[1].fillAmount == 0.0f)
			{
				this.Yandere.EmptyHands();

				if (this.Yandere.ClubAttire)
				{
					this.RemovingClubAttire = true;
				}

				this.Yandere.PreviousSchoolwear = this.Yandere.Schoolwear;

				if (this.Yandere.Schoolwear == 1)
				{
					this.Yandere.Schoolwear = 0;

					if (!this.Removed[1])
					{
						if (this.Yandere.Bloodiness == 0.0f)
						{
							this.DropCleanUniform = true;
						}
					}
					else
					{
						this.Removed[1] = false;
					}
				}
				else
				{
					this.Yandere.Schoolwear = 1;
					this.Removed[1] = true;
				}

				this.SpawnSteam();
				this.Yandere.CurrentUniformOrigin = 1;
			}
			else if (this.Prompt.Circle[2].fillAmount == 0.0f)
			{
				bool Continue = false;

				if (this.Yandere.Schoolwear > 0)
				{
					Debug.Log("Checking to see if it's okay for the player to take off clothing.");

					this.CheckAvailableUniforms();

					if (this.AvailableUniforms > 0)
					{
						Continue = true;
					}
				}
				else
				{
					Continue = true;
				}

				if (Continue)
				{
					this.Yandere.EmptyHands();

					if (this.Yandere.ClubAttire)
					{
						this.RemovingClubAttire = true;
					}

					this.Yandere.PreviousSchoolwear = this.Yandere.Schoolwear;

					if ((this.Yandere.Schoolwear == 1) && !this.Removed[1])
					{
						this.DropCleanUniform = true;
					}

					if (this.Yandere.Schoolwear == 2)
					{
						this.Yandere.Schoolwear = 0;
						this.Removed[2] = false;
					}
					else
					{
						this.Yandere.Schoolwear = 2;
						this.Removed[2] = true;
					}

					this.SpawnSteam();
					this.Yandere.CurrentUniformOrigin = 1;
				}
				else
				{
					this.Prompt.Circle[2].fillAmount = 1.0f;

					Debug.Log("Error Message.");
				}
			}
			else if (this.Prompt.Circle[3].fillAmount == 0.0f)
			{
				this.Yandere.EmptyHands();

				if (this.Yandere.ClubAttire)
				{
					this.RemovingClubAttire = true;
				}

				this.Yandere.PreviousSchoolwear = this.Yandere.Schoolwear;

				if ((this.Yandere.Schoolwear == 1) && !this.Removed[1])
				{
					this.DropCleanUniform = true;
				}

				if (this.Yandere.Schoolwear == 3)
				{
					this.Yandere.Schoolwear = 0;
					this.Removed[3] = false;
				}
				else
				{
					this.Yandere.Schoolwear = 3;
					this.Removed[3] = true;
				}

				this.SpawnSteam();
				this.Yandere.CurrentUniformOrigin = 1;
			}
		}

		if (this.YandereLocker)
		{
			this.Hinge.localEulerAngles = new Vector3(0.0f, this.Rotation, 0.0f);
		}

		if (this.SteamCountdown)
		{
			this.Timer += Time.deltaTime;

			if (this.Phase == 1)
			{
				if (this.Timer > 1.50f)
				{
					if (this.YandereLocker)
					{
						if (this.Yandere.Gloved)
						{
							this.Yandere.Gloves.GetComponent<PickUpScript>().MyRigidbody.isKinematic = false;
							this.Yandere.Gloves.transform.localPosition = new Vector3(0.0f, 1.0f, -1.0f);
							this.Yandere.Gloves.transform.parent = null;

							this.Yandere.GloveAttacher.newRenderer.enabled = false;

							this.Yandere.Gloves.gameObject.SetActive(true);

							this.Yandere.Gloved = false;
							this.Yandere.Gloves = null;
						}

						this.Yandere.ChangeSchoolwear();

						if (this.Yandere.Bloodiness > 0.0f)
						{
                            this.Yandere.Police.BloodyClothing++;

							PickUpScript DroppedBloodyUniform = null;

							if (this.RemovingClubAttire)
							{
								GameObject NewUniform = Instantiate(this.BloodyClubUniform[(int)this.Yandere.Club],
									this.Yandere.transform.position + (Vector3.forward * 0.50f) + Vector3.up,
									Quaternion.identity);

								DroppedBloodyUniform = NewUniform.GetComponent<PickUpScript>();

								this.StudentManager.ChangingBooths[(int)this.Yandere.Club].CannotChange = true;
								this.StudentManager.ChangingBooths[(int)this.Yandere.Club].CheckYandereClub();
								this.Prompt.HideButton[1] = true;
								this.Prompt.HideButton[2] = true;
								this.Prompt.HideButton[3] = true;
								this.RemovingClubAttire = false;
							}
							else
							{
								GameObject NewUniform = Instantiate(this.BloodyUniform[this.Yandere.PreviousSchoolwear],
									this.Yandere.transform.position + (Vector3.forward * 0.50f) + Vector3.up,
									Quaternion.identity);

								DroppedBloodyUniform = NewUniform.GetComponent<PickUpScript>();

								this.Prompt.HideButton[this.Yandere.PreviousSchoolwear] = true;
								this.Bloody[this.Yandere.PreviousSchoolwear] = true;
							}

							if (this.Yandere.RedPaint)
							{
								DroppedBloodyUniform.RedPaint = true;
							}
						}
					}
					else
					{
						if (this.Student.Schoolwear == 0)
						{
                            if (!this.RivalPhone.gameObject.activeInHierarchy && !this.Yandere.Inventory.RivalPhone)
                            {
							    this.RivalPhone.transform.parent = this.StudentManager.StrippingPositions[this.Student.GirlID];
							    this.RivalPhone.transform.localPosition = new Vector3(0, .92f, .2375f);
							    this.RivalPhone.transform.localEulerAngles = new Vector3(-80, 0, 0);

							    this.RivalPhone.gameObject.SetActive(true);
							    this.RivalPhone.StudentID = this.Student.StudentID;
							    this.RivalPhone.MyRenderer.material.mainTexture = this.Student.SmartPhone.GetComponent<Renderer>().material.mainTexture;
                            }
                        }

						this.Student.ChangeSchoolwear();
					}

					this.UpdateSchoolwear();

					this.Phase++;
				}
			}
			else
			{
				if (this.Timer > 3.0f)
				{
					if (!this.YandereLocker)
					{
						this.Student.BathePhase++;
					}

					this.SteamCountdown = false;
					this.Phase = 1;
					this.Timer = 0.0f;
				}
			}
		}
	}

	public void SpawnSteam()
	{
		this.SteamCountdown = true;

		if (this.YandereLocker)
		{
			Instantiate(this.SteamCloud,
				this.Yandere.transform.position + Vector3.up * 0.81f, Quaternion.identity);
			this.Yandere.Character.GetComponent<Animation>().CrossFade(AnimNames.FemaleStripping);
			this.Yandere.Stripping = true;
			this.Yandere.CanMove = false;
		}
		else
		{
			GameObject NewCloud = Instantiate(this.SteamCloud,
				this.Student.transform.position + Vector3.up * 0.81f, Quaternion.identity);
			NewCloud.transform.parent = this.Student.transform;

			this.Student.CharacterAnimation.CrossFade(this.Student.StripAnim);

			this.Student.Pathfinding.canSearch = false;
			this.Student.Pathfinding.canMove = false;
		}
	}

	public void SpawnSteamNoSideEffects(StudentScript SteamStudent)
	{
		Debug.Log("Changing clothes, no strings attached.");

		GameObject NewCloud = Instantiate(this.SteamCloud,
			SteamStudent.transform.position + Vector3.up * 0.81f, Quaternion.identity);

		NewCloud.transform.parent = SteamStudent.transform;

		SteamStudent.CharacterAnimation.CrossFade(SteamStudent.StripAnim);

		SteamStudent.Pathfinding.canSearch = false;
		SteamStudent.Pathfinding.canMove = false;

		SteamStudent.MustChangeClothing = false;
		SteamStudent.Stripping = true;
		SteamStudent.Routine = false;
	}

	public void UpdateSchoolwear()
	{
		if (this.DropCleanUniform)
		{
			Instantiate(this.CleanUniform,
				this.Yandere.transform.position + (Vector3.forward * -0.50f) + Vector3.up,
				Quaternion.identity);
			this.DropCleanUniform = false;
		}

		if (!this.Bloody[1])
		{
			this.Schoolwear[1].SetActive(true);
		}

		if (!this.Bloody[2])
		{
			this.Schoolwear[2].SetActive(true);
		}

		if (!this.Bloody[3])
		{
			this.Schoolwear[3].SetActive(true);
		}

		this.Prompt.Label[1].text = "     " + "School Uniform";
		this.Prompt.Label[2].text = "     " + "School Swimsuit";
		this.Prompt.Label[3].text = "     " + "Gym Uniform";

		if (this.YandereLocker)
		{
			if (!this.Yandere.ClubAttire)
			{
				if (this.Yandere.Schoolwear > 0)
				{
					this.Prompt.Label[this.Yandere.Schoolwear].text = "     " + "Towel";

					if (this.Removed[this.Yandere.Schoolwear])
					{
						this.Schoolwear[this.Yandere.Schoolwear].SetActive(false);
					}
				}
			}
			else
			{
				this.Prompt.Label[1].text = "     " + "Towel";
			}
		}
		else
		{
			if (this.Student != null)
			{
				if (this.Student.Schoolwear > 0)
				{
					this.Prompt.HideButton[this.Student.Schoolwear] = true;
					this.Schoolwear[this.Student.Schoolwear].SetActive(false);
					this.Student.Indoors = true;
				}
			}
		}
	}

	public void UpdateButtons()
	{
		if (!this.Yandere.ClubAttire || this.Yandere.ClubAttire &&
			(this.Yandere.Bloodiness > 0.0f))
		{
			if (this.Open)
			{
				if (this.Yandere.Bloodiness > 0.0f)
				{
					this.Prompt.HideButton[1] = true;
					this.Prompt.HideButton[2] = true;
					this.Prompt.HideButton[3] = true;

					if (this.Yandere.Schoolwear > 0)
					{
						if (!this.Yandere.ClubAttire)
						{
							this.Prompt.HideButton[this.Yandere.Schoolwear] = false;
						}
					}

					if (this.Yandere.ClubAttire)
					{
						Debug.Log("Don't hide Prompt 1!");

						this.Prompt.HideButton[1] = false;
					}
				}
				else
				{
					if (!this.Bloody[1])
					{
						this.Prompt.HideButton[1] = false;
					}

					if (!this.Bloody[2])
					{
						this.Prompt.HideButton[2] = false;
					}

					if (!this.Bloody[3])
					{
						this.Prompt.HideButton[3] = false;
					}
				}
			}
		}
		else
		{
			this.Prompt.HideButton[1] = true;
			this.Prompt.HideButton[2] = true;
			this.Prompt.HideButton[3] = true;
		}
	}

	void CheckAvailableUniforms()
	{
		this.AvailableUniforms = this.StudentManager.OriginalUniforms;

		Debug.Log(this.AvailableUniforms + " of the original uniforms are still clean.");

		Debug.Log("There are " + this.StudentManager.NewUniforms + " new uniforms in school.");

		if (this.StudentManager.NewUniforms > 0)
		{
			int tempID = 0;

			for (tempID = 0; tempID < this.StudentManager.Uniforms.Length; tempID++)
			{
				Transform uniform = this.StudentManager.Uniforms[tempID];

				if (uniform != null)
				{
					if (this.StudentManager.LockerRoomArea.bounds.Contains(uniform.position))
					{
						Debug.Log("Cool, there's a uniform in the locker room.");

						AvailableUniforms++;
					}
				}
			}
		}
	}
}