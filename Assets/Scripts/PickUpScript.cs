using UnityEngine;

public class PickUpScript : MonoBehaviour
{
	public RigidbodyConstraints OriginalConstraints;

	public BloodCleanerScript BloodCleaner;
	public IncineratorScript Incinerator;
	public BodyPartScript BodyPart;
	public TrashCanScript TrashCan;
	public OutlineScript[] Outline;
	public YandereScript Yandere;
	public Animation MyAnimation;
	public BucketScript Bucket;
	public PromptScript Prompt;
	public ClockScript Clock;
	public MopScript Mop;

	public Mesh ClosedBook;
	public Mesh OpenBook;

	public Rigidbody MyRigidbody;
	public Collider MyCollider;
	public MeshFilter MyRenderer;

	public Vector3 TrashPosition;
	public Vector3 TrashRotation;
	public Vector3 OriginalScale;
	public Vector3 HoldPosition;
	public Vector3 HoldRotation;

	public Color EvidenceColor;
	public Color OriginalColor;

	public bool CleaningProduct = false;
	public bool DisableAtStart = false;
	public bool LockRotation = false;
	public bool BeingLifted = false;
	public bool CanCollide = false;
	public bool Electronic = false;
	public bool Flashlight = false;
	public bool PuzzleCube = false;
	public bool Suspicious = false;
	public bool Blowtorch = false;
	public bool Clothing = false;
	public bool Evidence = false;
	public bool JerryCan = false;
	public bool LeftHand = false;
	public bool RedPaint = false;
	public bool Garbage = false;
	public bool Bleach = false;
	public bool Dumped = false;
	public bool Usable = false;
	public bool Weight = false;
	public bool Radio = false;
	public bool Salty = false;

	public int CarryAnimID = 0;
	public int Strength = 0;
	public int Period = 0;
	public int Food = 0;

	public float KinematicTimer = 0.0f;
	public float DumpTimer = 0.0f;

	public bool Empty = true;

	public GameObject[] FoodPieces;

	public WeaponScript StuckBoxCutter;

	void Start()
	{
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();
		this.Clock = GameObject.Find("Clock").GetComponent<ClockScript>();

		if (!this.CanCollide)
		{
			Physics.IgnoreCollision(this.Yandere.GetComponent<Collider>(), this.MyCollider);
		}

		if (this.Outline.Length > 0)
		{
			this.OriginalColor = this.Outline[0].color;
		}

		this.OriginalScale = this.transform.localScale;

		if (this.MyRigidbody == null)
		{
			this.MyRigidbody = this.GetComponent<Rigidbody>();
		}

		if (this.DisableAtStart)
		{
			this.gameObject.SetActive(false);
		}
	}

	void LateUpdate()
	{
		if (this.CleaningProduct)
		{
			if (this.Clock.Period == 5)
			{
				this.Suspicious = false;
			}
			else
			{
				this.Suspicious = true;
			}
		}

		if (this.Weight)
		{
			if (Period < Clock.Period)
			{
				Strength = this.Prompt.Yandere.Class.PhysicalGrade + this.Prompt.Yandere.Class.PhysicalBonus;
				Period++;
			}

			if (Strength == 0)
			{
				this.Prompt.Label[3].text = "     " + "Physical Stat Too Low";
				this.Prompt.Circle[3].fillAmount = 1.0f;
			}
			else
			{
				this.Prompt.Label[3].text = "     " + "Carry";
			}
		}

		if (this.Prompt.Circle[3].fillAmount == 0.0f)
		{
			this.Prompt.Circle[3].fillAmount = 1;

			if (this.Weight)
			{
				if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
				{
					if (this.Yandere.PickUp != null)
					{
						this.Yandere.CharacterAnimation [this.Yandere.CarryAnims [this.Yandere.PickUp.CarryAnimID]].weight = 0.0f;
					}

					if (this.Yandere.Armed)
					{
						this.Yandere.CharacterAnimation [this.Yandere.ArmedAnims [this.Yandere.EquippedWeapon.AnimID]].weight = 0.0f;
					}

					this.Yandere.targetRotation = Quaternion.LookRotation(
						new Vector3(this.transform.position.x,
							this.Yandere.transform.position.y,
							this.transform.position.z) - this.Yandere.transform.position);
					this.Yandere.transform.rotation = this.Yandere.targetRotation;

					this.Yandere.EmptyHands();	

					this.transform.parent = this.Yandere.transform;
					this.transform.localPosition = new Vector3(0.0f, 0.0f, 0.79184f);
					this.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
					//this.transform.parent = null;

					this.Yandere.Character.GetComponent<Animation>().Play(AnimNames.FemaleHeavyWeightLift);
					this.Yandere.HeavyWeight = true;
					this.Yandere.CanMove = false;
					this.Yandere.Lifting = true;

					this.MyAnimation.Play("Weight_liftUp_00");

					this.MyRigidbody.isKinematic = true;

					this.BeingLifted = true;
				}
			}
			else
			{
				this.BePickedUp();
			}
		}

		if (this.Yandere.PickUp == this)
		{
			this.transform.localPosition = this.HoldPosition;
			this.transform.localEulerAngles = this.HoldRotation;

			if (this.Garbage)
			{
				if (!this.Yandere.StudentManager.IncineratorArea.bounds.Contains(Yandere.transform.position))
				{
					this.Drop();
					this.transform.position = new Vector3(-40, 0, 24);
				}
			}
		}

		if (this.Dumped)
		{
			this.DumpTimer += Time.deltaTime;

			if (this.DumpTimer > 1.0f)
			{
				if (this.Clothing)
				{
					this.Yandere.Incinerator.BloodyClothing++;
				}
				else if (this.BodyPart)
				{
					this.Yandere.Incinerator.BodyParts++;
				}

				Destroy(this.gameObject);
			}
		}

		if (this.Yandere.PickUp != this)
		{
			//if (this.MyRigidbody != null)
			//{
				//Debug.Log("My name is: " + name);

				if (!this.MyRigidbody.isKinematic)
				{
					this.KinematicTimer = Mathf.MoveTowards(this.KinematicTimer, 5.0f, Time.deltaTime);

					if (this.KinematicTimer == 5.0f)
					{
						this.MyRigidbody.isKinematic = true;
						this.KinematicTimer = 0.0f;
					}

					//Gardening Club Zone
					if ((this.transform.position.x > -71.0f) &&
						(this.transform.position.x < -61.0f) &&
						(this.transform.position.z > -37.50f) &&
						(this.transform.position.z < -27.50f))
					{
						this.transform.position = new Vector3(-63, 1.0f, -26.5f);
						this.KinematicTimer = 0.0f;
					}

					/*
					if ((this.transform.position.x > -21.0f) &&
						(this.transform.position.x < 21.0f) &&
						(this.transform.position.z > 79.0f) &&
						(this.transform.position.z < 121.0f))
					{
						this.transform.position = new Vector3(0.0f, 1.0f, 79f);
						this.KinematicTimer = 0.0f;
					}
					*/

					//Pool
					if ((this.transform.position.x > -46.0f) &&
						(this.transform.position.x < -18.0f) &&
						(this.transform.position.z > 66.0f) &&
						(this.transform.position.z < 78.0f))
					{
						this.transform.position = new Vector3(-16f, 5.0f, 72);
						this.KinematicTimer = 0.0f;
					}
				}
			//}
		}

		if (this.Weight)
		{
			if (this.BeingLifted)
			{
				if (this.Yandere.Lifting)
				{
					/*
					if (this.Yandere.CharacterAnimation[AnimNames.FemaleHeavyWeightLift].time >= 3.0f)
					{
						this.transform.parent = this.Yandere.LeftItemParent;
						this.transform.localPosition = this.HoldPosition;
						this.transform.localEulerAngles = this.HoldRotation;

						if (this.Yandere.StudentManager.Stop)
						{
							this.Drop();
						}
					}
					*/

					if (this.Yandere.StudentManager.Stop)
					{
						this.Drop();
					}
				}
				else
				{
					this.BePickedUp();
				}
			}
		}
	}

	public void BePickedUp()
	{
		//Debug.Log ("SchemeGlobals.GetSchemeStage(4) is: " + SchemeGlobals.GetSchemeStage (4));

		if (this.Radio)
		{
			//If we're waiting for Yandere-chan to pick up a radio...
			if (SchemeGlobals.GetSchemeStage(5) == 2)
			{
				SchemeGlobals.SetSchemeStage(5, 3);
				this.Yandere.PauseScreen.Schemes.UpdateInstructions();
			}
		}

		if (this.Salty)
		{
			//If we're waiting for Yandere-chan to pick up a bag of salty chips...
			if (SchemeGlobals.GetSchemeStage(4) == 4)
			{
				SchemeGlobals.SetSchemeStage(4, 5);
				this.Yandere.PauseScreen.Schemes.UpdateInstructions();
			}
		}

		if (this.CarryAnimID == 10)
		{
			this.MyRenderer.mesh = this.OpenBook;
			this.Yandere.LifeNotePen.SetActive(true);
		}

		if (this.MyAnimation != null)
		{
			this.MyAnimation.Stop();
		}

		this.Prompt.Circle[3].fillAmount = 1.0f;

		this.BeingLifted = false;

		if (this.Yandere.PickUp != null)
		{
			this.Yandere.PickUp.Drop();
		}

		if (this.Yandere.Equipped == 3)
		{
			this.Yandere.Weapon[3].Drop();
		}
		else if (this.Yandere.Equipped > 0)
		{
			this.Yandere.Unequip();
		}

		if (this.Yandere.Dragging)
		{
			this.Yandere.Ragdoll.GetComponent<RagdollScript>().StopDragging();
		}

		if (this.Yandere.Carrying)
		{
			this.Yandere.StopCarrying();
		}

		if (!this.LeftHand)
		{
			this.transform.parent = this.Yandere.ItemParent;
		}
		else
		{
			this.transform.parent = this.Yandere.LeftItemParent;
		}

		if (this.GetComponent<RadioScript>() != null)
		{
			if (this.GetComponent<RadioScript>().On)
			{
				this.GetComponent<RadioScript>().TurnOff();
			}
		}

		//this.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
		//this.transform.localEulerAngles = new Vector3(0.0f, 90.0f, 0.0f);

		this.MyCollider.enabled = false;

		if (this.MyRigidbody != null)
		{
			this.MyRigidbody.constraints = RigidbodyConstraints.FreezeAll;
		}

		if (!this.Usable)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.Yandere.NearestPrompt = null;
		}
		else
		{
			this.Prompt.Carried = true;
		}

		this.Yandere.PickUp = this;
		this.Yandere.CarryAnimID = CarryAnimID;

		// [af] Converted while loop to foreach loop.
		foreach (OutlineScript outline in this.Outline)
		{
			outline.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
		}

		if (this.BodyPart)
		{
			this.Yandere.NearBodies++;
		}

		this.Yandere.StudentManager.UpdateStudents();

		this.MyRigidbody.isKinematic = true;
		this.KinematicTimer = 0.0f;
	}

	public void Drop()
	{
		if (this.Salty)
		{
			//If Yandere-chan is supposed to be holding a bag of chips...
			if (SchemeGlobals.GetSchemeStage(4) == 5)
			{
				SchemeGlobals.SetSchemeStage(4, 4);
				this.Yandere.PauseScreen.Schemes.UpdateInstructions();
			}
		}

		if (this.TrashCan)
		{
			this.Yandere.MyController.radius = .2f;
		}

		if (this.CarryAnimID == 10)
		{
			this.MyRenderer.mesh = this.ClosedBook;
			this.Yandere.LifeNotePen.SetActive(false);
		}

		if (this.Weight)
		{
			this.Yandere.IdleAnim = this.Yandere.OriginalIdleAnim;
			this.Yandere.WalkAnim = this.Yandere.OriginalWalkAnim;
			this.Yandere.RunAnim = this.Yandere.OriginalRunAnim;
		}

		if (this.BloodCleaner != null)
		{
			this.BloodCleaner.enabled = true;
			this.BloodCleaner.Pathfinding.enabled = true;
		}

		this.Yandere.PickUp = null;

		if (this.BodyPart)
		{
			this.transform.parent = this.Yandere.LimbParent;
		}
		else
		{
			this.transform.parent = null;
		}

		if (this.LockRotation)
		{
			this.transform.localEulerAngles = new Vector3(
				0.0f,
				this.transform.localEulerAngles.y,
				0.0f);
		}

		if (this.MyRigidbody != null)
		{
			this.MyRigidbody.constraints = this.OriginalConstraints;
			this.MyRigidbody.isKinematic = false;
			this.MyRigidbody.useGravity = true;
		}

		if (this.Dumped)
		{
			this.transform.position = this.Incinerator.DumpPoint.position;
		}
		else
		{
			this.Prompt.enabled = true;
			this.MyCollider.enabled = true;
			this.MyCollider.isTrigger = false;

			if (!this.CanCollide)
			{
				Physics.IgnoreCollision(this.Yandere.GetComponent<Collider>(), this.MyCollider);
			}
		}

		this.Prompt.Carried = false;

		// [af] Converted while loop to foreach loop.
		foreach (OutlineScript outline in this.Outline)
		{
			// [af] Replaced if/else statement with ternary expression.
			outline.color = this.Evidence ? this.EvidenceColor : this.OriginalColor;
		}

		this.transform.localScale = this.OriginalScale;

		if (this.BodyPart)
		{
			this.Yandere.NearBodies--;
		}

		this.Yandere.StudentManager.UpdateStudents();

        /*
		if (this.Clothing && this.Evidence)
		{
			this.transform.parent = this.Yandere.Police.BloodParent;
		}
        */
	}
}