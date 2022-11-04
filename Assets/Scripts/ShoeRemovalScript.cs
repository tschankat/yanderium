using UnityEngine;

public class ShoeRemovalScript : MonoBehaviour
{
	public StudentScript Student;

	public Vector3 RightShoePosition;
	public Vector3 LeftShoePosition;

	public Transform RightCurrentShoe;
	public Transform LeftCurrentShoe;

	public Transform RightCasualShoe;
	public Transform LeftCasualShoe;

	public Transform RightSchoolShoe;
	public Transform LeftSchoolShoe;

	public Transform RightNewShoe;
	public Transform LeftNewShoe;

	public Transform RightFoot;
	public Transform LeftFoot;

	public Transform RightHand;
	public Transform LeftHand;

	public Transform ShoeParent;
	public Transform Locker;

	public GameObject NewPairOfShoes;
	public GameObject Character;

	public string[] LockerAnims;

	public Texture OutdoorShoes;
	public Texture IndoorShoes;
	public Texture TargetShoes;
	public Texture Socks;

	public Renderer MyRenderer;

	public bool RemovingCasual = true;
	public bool Male = false;

	public int Height = 0;
	public int Phase = 1;

	public float X = 0.0f;
	public float Y = 0.0f;
	public float Z = 0.0f;

	public string RemoveCasualAnim = string.Empty;
	public string RemoveSchoolAnim = string.Empty;
	public string RemovalAnim = string.Empty;

	public void Start()
	{
		if (this.Locker == null)
		{
			this.GetHeight(this.Student.StudentID);

			this.Locker = this.Student.StudentManager.Lockers.List[this.Student.StudentID];

			GameObject NewShoes = Instantiate(this.NewPairOfShoes, this.transform.position, Quaternion.identity);
			NewShoes.transform.parent = this.Locker;
			NewShoes.transform.localEulerAngles = new Vector3(0.0f, -180.0f, 0.0f);

			// [af] Replaced if/else statement with assignment and ternary expression.
			NewShoes.transform.localPosition = new Vector3(
				0.0f,
				-0.29f + (0.30f * this.Height),
				this.Male ? 0.040f : 0.050f);

			this.LeftSchoolShoe = NewShoes.transform.GetChild(0);
			this.RightSchoolShoe = NewShoes.transform.GetChild(1);

			this.RemovalAnim = this.RemoveCasualAnim;

			this.RightCurrentShoe = this.RightCasualShoe;
			this.LeftCurrentShoe = this.LeftCasualShoe;

			// [af] Added "gameObject" for C# compatibility.
			//this.RightCasualShoe.gameObject.SetActive(true);
			//this.LeftCasualShoe.gameObject.SetActive(true);

			this.RightNewShoe = this.RightSchoolShoe;
			this.LeftNewShoe = this.LeftSchoolShoe;

			this.ShoeParent = NewShoes.transform;
			this.TargetShoes = this.IndoorShoes;

			this.RightShoePosition = this.RightCurrentShoe.localPosition;
			this.LeftShoePosition = this.LeftCurrentShoe.localPosition;

			this.RightCurrentShoe.localScale = new Vector3(1.111113f, 1.0f, 1.111113f);
			this.LeftCurrentShoe.localScale = new Vector3(1.111113f, 1.0f, 1.111113f);

			this.OutdoorShoes = this.Student.Cosmetic.CasualTexture;
			this.IndoorShoes = this.Student.Cosmetic.UniformTexture;
			this.Socks = this.Student.Cosmetic.SocksTexture;

			this.TargetShoes = this.IndoorShoes;
		}
	}

	public void StartChangingShoes()
	{
		if (!this.Student.AoT)
		{
			this.RightCasualShoe.gameObject.SetActive(true);
			this.LeftCasualShoe.gameObject.SetActive(true);

			if (!this.Male)
			{
				this.MyRenderer.materials[0].mainTexture = this.Socks;
				this.MyRenderer.materials[1].mainTexture = this.Socks;
			}
			else
			{
				this.MyRenderer.materials[this.Student.Cosmetic.UniformID].mainTexture = this.Socks;
			}
		}
	}

	void Update()
	{
		if (!this.Student.DiscCheck && !this.Student.Dying &&
			!this.Student.Alarmed && !this.Student.Splashed && !this.Student.TurnOffRadio)
		{
			if (this.Student.CurrentDestination == null)
			{
				this.Student.CurrentDestination = this.Student.Destinations[this.Student.Phase];
				this.Student.Pathfinding.target = this.Student.CurrentDestination;
			}

			this.Student.MoveTowardsTarget(this.Student.CurrentDestination.position);
			this.transform.rotation = Quaternion.Slerp(
				this.transform.rotation, this.Student.CurrentDestination.rotation, 10.0f * Time.deltaTime);

			this.Student.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
			this.Student.CharacterAnimation.CrossFade(this.RemovalAnim);

			if (this.Phase == 1)
			{
				if (this.Student.CharacterAnimation[this.RemovalAnim].time >= .833333f)
				{
					this.ShoeParent.parent = this.LeftHand;
					this.Phase++;
				}
			}
			else if (this.Phase == 2)
			{
				if (this.Student.CharacterAnimation[this.RemovalAnim].time >= 1.833333f)
				{
					this.ShoeParent.parent = this.Locker;

					this.X = this.ShoeParent.localEulerAngles.x;
					this.Y = this.ShoeParent.localEulerAngles.y;
					this.Z = this.ShoeParent.localEulerAngles.z;

					this.Phase++;
				}
			}
			else if (this.Phase == 3)
			{
				this.X = Mathf.MoveTowards(this.X, 0.0f, Time.deltaTime * 360.0f);
				this.Y = Mathf.MoveTowards(this.Y, 186.878f, Time.deltaTime * 360.0f);
				this.Z = Mathf.MoveTowards(this.Z, 0.0f, Time.deltaTime * 360.0f);

				this.ShoeParent.localEulerAngles = new Vector3(this.X, this.Y, this.Z);

				this.ShoeParent.localPosition = Vector3.MoveTowards(
					this.ShoeParent.localPosition, new Vector3(0.272f, 0.0f, 0.552f), Time.deltaTime);

				if (this.ShoeParent.localPosition.y == 0.0f)
				{
					this.ShoeParent.localPosition = new Vector3(0.272f, 0.0f, 0.552f);
					this.ShoeParent.localEulerAngles = new Vector3(0.0f, 186.878f, 0.0f);

					this.Phase++;
				}
			}
			else if (this.Phase == 4)
			{
				if (this.Student.CharacterAnimation[this.RemovalAnim].time >= 3.50f)
				{
					this.RightCurrentShoe.parent = null;
					this.RightCurrentShoe.position = new Vector3(
						this.RightCurrentShoe.position.x,
						0.050f,
						this.RightCurrentShoe.position.z);

					this.RightCurrentShoe.localEulerAngles = new Vector3(
						0.0f,
						this.RightCurrentShoe.localEulerAngles.y,
						0.0f);

					this.Phase++;
				}
			}
			else if (this.Phase == 5)
			{
				if (this.Student.CharacterAnimation[this.RemovalAnim].time >= 4.0f)
				{
					this.LeftCurrentShoe.parent = null;
					this.LeftCurrentShoe.position = new Vector3(
						this.LeftCurrentShoe.position.x,
						0.050f,
						this.LeftCurrentShoe.position.z);

					this.LeftCurrentShoe.localEulerAngles = new Vector3(
						0.0f,
						this.LeftCurrentShoe.localEulerAngles.y,
						0.0f);

					this.Phase++;
				}
			}
			else if (this.Phase == 6)
			{
				if (this.Student.CharacterAnimation[this.RemovalAnim].time >= 5.50f)
				{
					this.LeftNewShoe.parent = this.LeftFoot;
					this.LeftNewShoe.localPosition = this.LeftShoePosition;
					this.LeftNewShoe.localEulerAngles = Vector3.zero;

					this.Phase++;
				}
			}
			else if (this.Phase == 7)
			{
				if (this.Student.CharacterAnimation[this.RemovalAnim].time >= 6.66666f)
				{
					if (!this.Student.AoT)
					{
						if (!this.Male)
						{
							this.MyRenderer.materials[0].mainTexture = this.TargetShoes;
							this.MyRenderer.materials[1].mainTexture = this.TargetShoes;
						}
						else
						{
							this.MyRenderer.materials[this.Student.Cosmetic.UniformID].mainTexture = this.TargetShoes;
						}
					}

					this.RightNewShoe.parent = this.RightFoot;
					this.RightNewShoe.localPosition = this.RightShoePosition;
					this.RightNewShoe.localEulerAngles = Vector3.zero;

					this.RightNewShoe.gameObject.SetActive(false);
					this.LeftNewShoe.gameObject.SetActive(false);

					this.Phase++;
				}
			}
			else if (this.Phase == 8)
			{
				if (this.Student.CharacterAnimation[this.RemovalAnim].time >= 7.666666f)
				{
					this.ShoeParent.transform.position =
						(this.RightCurrentShoe.position - this.LeftCurrentShoe.position) * 0.50f;

					this.RightCurrentShoe.parent = this.ShoeParent;
					this.LeftCurrentShoe.parent = this.ShoeParent;

					this.ShoeParent.parent = this.RightHand;

					this.Phase++;
				}
			}
			else if (this.Phase == 9)
			{
				if (this.Student.CharacterAnimation[this.RemovalAnim].time >= 8.50f)
				{
					this.ShoeParent.parent = this.Locker;

					// [af] Replaced if/else statement with ternary expression.
					this.ShoeParent.localPosition = new Vector3(
						0.0f,
						((this.TargetShoes == this.IndoorShoes) ? -0.14f : -0.29f) + (0.30f * this.Height),
						-0.010f);

					this.ShoeParent.localEulerAngles = new Vector3(0.0f, 180.0f, 0.0f);

					this.RightCurrentShoe.localPosition = new Vector3(0.041f, 0.04271515f, 0.0f);
					this.LeftCurrentShoe.localPosition = new Vector3(-0.041f, 0.04271515f, 0.0f);

					this.RightCurrentShoe.localEulerAngles = Vector3.zero;
					this.LeftCurrentShoe.localEulerAngles = Vector3.zero;

					this.Phase++;
				}
			}
			else if (this.Phase == 10)
			{
				if (this.Student.CharacterAnimation[this.RemovalAnim].time >=
					this.Student.CharacterAnimation[this.RemovalAnim].length)
				{
					this.Student.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
                    this.Student.ChangingShoes = false;
                    this.Student.Routine = true;
					this.enabled = false;

					// Entering school.
					if (!this.Student.Indoors)
					{
						if (this.Student.Persona == PersonaType.PhoneAddict || this.Student.Sleuthing)
						{
							this.Student.SmartPhone.SetActive(true);

							if (!this.Student.Sleuthing)
							{
								this.Student.WalkAnim = this.Student.PhoneAnims[1];
							}
						}

						this.Student.Indoors = true;
						this.Student.CanTalk = true;
					}
					// Leaving school.
					else
					{
						//this.Student.CurrentDestination = this.Student.StudentManager.Hangouts.List[0];
						//this.Student.Pathfinding.target = this.Student.StudentManager.Hangouts.List[0];

						if (this.Student.Destinations[this.Student.Phase + 1] != null)
						{
							this.Student.CurrentDestination = this.Student.Destinations[this.Student.Phase + 1];
							this.Student.Pathfinding.target = this.Student.Destinations[this.Student.Phase + 1];
						}
						else
						{
							this.Student.CurrentDestination = this.Student.StudentManager.Hangouts.List[0];
							this.Student.Pathfinding.target = this.Student.StudentManager.Hangouts.List[0];
						}
						//this.Locker.gameObject.GetComponent<Animation>().Stop();

						this.Student.CanTalk = false;
						this.Student.Leaving = true;
						this.Student.Phase++;
						this.enabled = false;
						this.Phase++;
					}
				}
			}
		}
		else
		{
			this.PutOnShoes();
			this.Student.Routine = false;
		}
	}

	void LateUpdate()
	{
		if (this.Phase < 7)
		{
			this.RightFoot.localScale = new Vector3(0.90f, 1.0f, 0.90f);
			this.LeftFoot.localScale = new Vector3(0.90f, 1.0f, 0.90f);
		}
	}

	public void PutOnShoes()
	{
		this.CloseLocker();

		this.ShoeParent.parent = this.LeftHand;
		this.ShoeParent.parent = this.Locker;
		this.ShoeParent.localPosition = new Vector3(0.272f, 0.0f, 0.552f);
		this.ShoeParent.localEulerAngles = new Vector3(0.0f, 186.878f, 0.0f);
		this.RightCurrentShoe.parent = null;

		this.RightCurrentShoe.position = new Vector3(
			this.RightCurrentShoe.position.x,
			0.050f,
			this.RightCurrentShoe.position.z);

		this.RightCurrentShoe.localEulerAngles = new Vector3(
			0.0f,
			this.RightCurrentShoe.localEulerAngles.y,
			0.0f);

		this.LeftCurrentShoe.parent = null;

		this.LeftCurrentShoe.position = new Vector3(
			this.LeftCurrentShoe.position.x,
			0.050f,
			this.LeftCurrentShoe.position.z);

		this.LeftCurrentShoe.localEulerAngles = new Vector3(
			0.0f,
			this.LeftCurrentShoe.localEulerAngles.y,
			0.0f);

		this.LeftNewShoe.parent = this.LeftFoot;
		this.LeftNewShoe.localPosition = this.LeftShoePosition;
		this.LeftNewShoe.localEulerAngles = Vector3.zero;

		if (!this.Student.AoT)
		{
			if (!this.Male)
			{
				this.MyRenderer.materials [0].mainTexture = this.TargetShoes;
				this.MyRenderer.materials [1].mainTexture = this.TargetShoes;
			}
			else
			{
				this.MyRenderer.materials [this.Student.Cosmetic.UniformID].mainTexture = this.TargetShoes;
			}
		}

		this.RightNewShoe.parent = this.RightFoot;
		this.RightNewShoe.localPosition = this.RightShoePosition;
		this.RightNewShoe.localEulerAngles = Vector3.zero;
		this.RightNewShoe.gameObject.SetActive(false);
		this.LeftNewShoe.gameObject.SetActive(false);
		this.ShoeParent.transform.position =
			(this.RightCurrentShoe.position - this.LeftCurrentShoe.position) * 0.50f;
		this.RightCurrentShoe.parent = this.ShoeParent;
		this.LeftCurrentShoe.parent = this.ShoeParent;
		this.ShoeParent.parent = this.RightHand;
		this.ShoeParent.parent = this.Locker;

		// [af] Replaced if/else statement with ternary expression.
		this.ShoeParent.localPosition = new Vector3(
			0.0f,
			((this.TargetShoes == this.IndoorShoes) ? -0.14f : -0.29f) + (0.30f * this.Height),
			-0.010f);

		this.ShoeParent.localEulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
		this.RightCurrentShoe.localPosition = new Vector3(0.041f, 0.04271515f, 0.0f);
		this.LeftCurrentShoe.localPosition = new Vector3(-0.041f, 0.04271515f, 0.0f);
		this.RightCurrentShoe.localEulerAngles = Vector3.zero;
		this.LeftCurrentShoe.localEulerAngles = Vector3.zero;
		this.Student.Indoors = true;
		this.Student.CanTalk = true;
		this.enabled = false;

		this.Student.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;

		this.Student.StopPairing();
	}

	public void CloseLocker()
	{
		//this.Locker.gameObject.GetComponent<Animation>()[this.LockerAnims[this.Height]].time =
			//this.Locker.gameObject.GetComponent<Animation>()[this.LockerAnims[this.Height]].length;
	}

	void UpdateShoes()
	{
		this.Student.Indoors = true;

		if (!this.Student.AoT)
		{
			if (!this.Male)
			{
				this.MyRenderer.materials [0].mainTexture = this.IndoorShoes;
				this.MyRenderer.materials [1].mainTexture = this.IndoorShoes;
			}
			else
			{
				this.MyRenderer.materials [this.Student.Cosmetic.UniformID].mainTexture = this.IndoorShoes;
			}
		}
	}

	public void LeavingSchool()
	{
		if (this.Locker == null)
		{
			this.Start();
		}

		this.Student.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;

		this.OutdoorShoes = this.Student.Cosmetic.CasualTexture;
		this.IndoorShoes = this.Student.Cosmetic.UniformTexture;
		this.Socks = this.Student.Cosmetic.SocksTexture;

		this.RemovalAnim = this.RemoveSchoolAnim;

		//this.Locker.gameObject.GetComponent<Animation>()[this.LockerAnims[this.Height]].time = 0.0f;
		//this.Locker.gameObject.GetComponent<Animation>().Play(this.LockerAnims[this.Height]);

		if (!this.Student.AoT)
		{
			if (!this.Male)
			{
				this.MyRenderer.materials[0].mainTexture = this.Socks;
				this.MyRenderer.materials[1].mainTexture = this.Socks;
			}
			else
			{
				this.MyRenderer.materials [this.Student.Cosmetic.UniformID].mainTexture = this.Socks;
			}
		}

		this.Student.CharacterAnimation.CrossFade(this.RemovalAnim);

		this.RightNewShoe.gameObject.SetActive(true);
		this.LeftNewShoe.gameObject.SetActive(true);

		this.RightCurrentShoe = this.RightSchoolShoe;
		this.LeftCurrentShoe = this.LeftSchoolShoe;

		this.RightNewShoe = this.RightCasualShoe;
		this.LeftNewShoe = this.LeftCasualShoe;

		this.TargetShoes = this.OutdoorShoes;

		this.Phase = 1;

		this.RightFoot.localScale = new Vector3(0.90f, 1.0f, 0.90f);
		this.LeftFoot.localScale = new Vector3(0.90f, 1.0f, 0.90f);

		this.RightCurrentShoe.localScale = new Vector3(1.111113f, 1.0f, 1.111113f);
		this.LeftCurrentShoe.localScale = new Vector3(1.111113f, 1.0f, 1.111113f);
	}

	void GetHeight(int StudentID)
	{
		/*
		this.Height = 6;

		while (StudentID > 0)
		{
			this.Height--;
			StudentID--;

			if (this.Height == 0)
			{
				this.Height = 5;
			}
		}
		*/

		this.Height = 5;

		    //Kokona                       //Fragile Girl
		if (this.Student.StudentID == 30 || this.Student.StudentID == 5 ||
			this.Student.StudentID == this.Student.StudentManager.RivalID ||
			this.Student.StudentID == this.Student.StudentManager.SuitorID)
		{
			this.Height = 5;
		}

		this.RemoveCasualAnim = this.RemoveCasualAnim + this.Height.ToString() + "_00";
		this.RemoveSchoolAnim = this.RemoveSchoolAnim + this.Height.ToString() + "_01";
	}
}
