using UnityEngine;

public class BucketScript : MonoBehaviour
{
	public PhoneEventScript PhoneEvent;
	public ParticleSystem PourEffect;
	public ParticleSystem Sparkles;
	public YandereScript Yandere;
	public PickUpScript PickUp;
	public PromptScript Prompt;

	public GameObject WaterCollider;
	public GameObject BloodCollider;
	public GameObject GasCollider;

	[SerializeField] GameObject BloodSpillEffect;
	[SerializeField] GameObject GasSpillEffect;
	[SerializeField] GameObject SpillEffect;
	[SerializeField] GameObject Effect;

	[SerializeField] GameObject[] Dumbbell;
	[SerializeField] Transform[] Positions;

	[SerializeField] Renderer Water;
	[SerializeField] Renderer Blood;
	[SerializeField] Renderer Gas;

	public float Bloodiness = 0.0f;
	public float FillSpeed = 1.0f;
	public float Timer = 0.0f;

	[SerializeField] float Distance = 0.0f;
	[SerializeField] float Rotate = 0.0f;

	public int Dumbbells = 0;

	public bool UpdateAppearance = false;
    public bool Bleached = false;
    public bool Dippable = false;
    public bool Gasoline = false;
	public bool Dropped = false;
	public bool Poured = false;
	public bool Full = false;
	public bool Trap = false;
	public bool Fly = false;

	void Start()
	{
		this.Water.transform.localPosition = new Vector3(
			this.Water.transform.localPosition.x,
			0.0f,
			this.Water.transform.localPosition.z);

		this.Water.transform.localScale = new Vector3(0.235f, 1.0f, 0.14f);

		this.Water.material.color = new Color(
			this.Water.material.color.r,
			this.Water.material.color.g,
			this.Water.material.color.b,
			0.0f);

		this.Blood.material.color = new Color(
			this.Blood.material.color.r,
			this.Blood.material.color.g,
			this.Blood.material.color.b,
			0.0f);

		this.Gas.transform.localPosition = new Vector3(
			this.Gas.transform.localPosition.x,
			0.0f,
			this.Gas.transform.localPosition.z);

		this.Gas.transform.localScale = new Vector3(0.235f, 1.0f, 0.14f);

		this.Gas.material.color = new Color(
			this.Gas.material.color.r,
			this.Gas.material.color.g,
			this.Gas.material.color.b,
			0.0f);

		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();
	}

	void Update()
	{
		if (this.PickUp.Clock.Period == 5)
		{
			this.PickUp.Suspicious = false;
		}
		else
		{
			this.PickUp.Suspicious = true;
		}

		this.Distance = Vector3.Distance(this.transform.position,
			this.Yandere.transform.position);

		if (this.Distance < 1.0f)
		{
			RaycastHit hit;

			if (this.Yandere.Bucket == null)
			{
				if ((this.transform.position.y > (this.Yandere.transform.position.y - 0.10f)) &&
					(this.transform.position.y < (this.Yandere.transform.position.y + 0.10f)))
				{
					if (Physics.Linecast(this.transform.position,
						this.Yandere.transform.position + Vector3.up, out hit))
					{
						if (hit.collider.gameObject == this.Yandere.gameObject)
						{
							this.Yandere.Bucket = this;
						}
					}
				}
			}
			else
			{
				if (Physics.Linecast(this.transform.position,
					this.Yandere.transform.position + Vector3.up, out hit))
				{
					if (hit.collider.gameObject != this.Yandere.gameObject)
					{
						this.Yandere.Bucket = null;
					}
				}

				if ((this.transform.position.y < (this.Yandere.transform.position.y - 0.10f)) ||
					(this.transform.position.y > (this.Yandere.transform.position.y + 0.10f)))
				{
					this.Yandere.Bucket = null;
				}
			}
		}
		else
		{
			if (this.Yandere.Bucket == this)
			{
				this.Yandere.Bucket = null;
			}
		}

		if (this.Yandere.Bucket == this && this.Yandere.Dipping)
		{
			this.transform.position = Vector3.Lerp(this.transform.position,
				this.Yandere.transform.position + (this.Yandere.transform.forward * 0.55f),
				Time.deltaTime * 10.0f);

			Quaternion targetRotation = Quaternion.LookRotation(new Vector3(
				this.Yandere.transform.position.x,
				this.transform.position.y,
				this.Yandere.transform.position.z) - this.transform.position);
			this.transform.rotation = Quaternion.Slerp(
				this.transform.rotation, targetRotation, Time.deltaTime * 10.0f);
		}

		if (this.Yandere.PickUp != null)
		{
            if (this.Yandere.Mop != null)
            {
                if (!this.Yandere.Chased && this.Yandere.Chasers == 0 &&
                    this.Full && !this.Gasoline && this.Bleached &&
                    this.Bloodiness < 100.0f)
                {
                    this.Prompt.Label[3].text = "     " + "Dip";
                    this.Dippable = true;
                }
                else
                {
                    this.Prompt.Label[3].text = "     " + "Carry";
                    this.Dippable = false;
                }
            }
            else if (this.Yandere.PickUp.JerryCan)
			{
                if (!this.Full)
                {
				    if (!this.Yandere.PickUp.Empty)
				    {
					    this.Prompt.Label[0].text = "     " + "Pour Gasoline";
					    this.Prompt.HideButton[0] = false;
				    }
				    else
				    {
					    this.Prompt.HideButton[0] = true;
				    }
                }
                else
                {
                    this.Prompt.HideButton[0] = true;
                }
            }
			else if (this.Yandere.PickUp.Bleach)
			{
				if (this.Full && !this.Gasoline && !this.Bleached)
				{
					this.Prompt.Label[0].text = "     " + "Pour Bleach";
					this.Prompt.HideButton[0] = false;
				}
				else
				{
					this.Prompt.HideButton[0] = true;
				}
			}
            else if(this.Dippable)
            {
                this.Prompt.Label[3].text = "     " + "Carry";
                this.Dippable = false;
            }

#if UNITY_EDITOR
            if (this.Yandere.PickUp == this.PickUp)
			{
				if (this.Full)
				{
					if (Input.GetButtonDown(InputNames.Xbox_RB))
					{
						Instantiate(this.SpillEffect,
						this.transform.position,
						this.transform.rotation);
					}
				}
			}
			#endif
		}
		else
		{
            if (this.Dippable)
            {
                this.Prompt.Label[3].text = "     " + "Carry";
                this.Dippable = false;
            }

            if (this.Yandere.Equipped > 0 && this.Yandere.EquippedWeapon != null)
			{
				if (!this.Full)
				{
					if (this.Yandere.EquippedWeapon.WeaponID == 12)
					{
						if (this.Dumbbells < 5)
						{
							this.Prompt.Label[0].text = "     " + "Place Dumbbell";
							this.Prompt.HideButton[0] = false;
						}
						else
						{
							this.Prompt.HideButton[0] = true;
						}
					}
					else
					{
						this.Prompt.HideButton[0] = true;
					}
				}
				else
				{
					this.Prompt.HideButton[0] = true;
				}
			}
			else
			{
				if (this.Dumbbells == 0)
				{
					this.Prompt.HideButton[0] = true;
				}
				else
				{
					this.Prompt.Label[0].text = "     " + "Remove Dumbbell";
					this.Prompt.HideButton[0] = false;
				}
			}
		}

        if (this.Yandere.Mop != null)
        {
            if (this.Prompt.Circle[3].fillAmount == 0.0f)
            {
                this.Prompt.Circle[3].fillAmount = 1;
                this.Yandere.Mop.Dip();
            }
        }

        if (this.Dumbbells > 0)
		{
			if (this.Prompt.Yandere.Class.PhysicalGrade + this.Prompt.Yandere.Class.PhysicalBonus == 0)
			{
				this.Prompt.Label[3].text = "     " + "Physical Stat Too Low";
				this.Prompt.Circle[3].fillAmount = 1.0f;
			}
			else
			{
				this.Prompt.Label[3].text = "     " + "Carry";
			}
		}

		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Prompt.Circle[0].fillAmount = 1.0f;

			if (this.Prompt.Label[0].text == ("     " + "Place Dumbbell"))
			{
				this.Dumbbells++;

				this.Dumbbell[this.Dumbbells] = this.Yandere.EquippedWeapon.gameObject;
				this.Yandere.EmptyHands();

				this.Dumbbell[this.Dumbbells].GetComponent<WeaponScript>().enabled = false;
				this.Dumbbell[this.Dumbbells].GetComponent<PromptScript>().enabled = false;
				this.Dumbbell[this.Dumbbells].GetComponent<PromptScript>().Hide();
				this.Dumbbell[this.Dumbbells].GetComponent<Collider>().enabled = false;

				Rigidbody dumbbellRigidBody = this.Dumbbell[this.Dumbbells].GetComponent<Rigidbody>();
				dumbbellRigidBody.useGravity = false;
				dumbbellRigidBody.isKinematic = true;

				this.Dumbbell[this.Dumbbells].transform.parent = this.transform;
				this.Dumbbell[this.Dumbbells].transform.localPosition =
					this.Positions[this.Dumbbells].localPosition;
				this.Dumbbell[this.Dumbbells].transform.localEulerAngles =
					new Vector3(90.0f, 0.0f, 0.0f);
			}
			else if (this.Prompt.Label[0].text == ("     " + "Remove Dumbbell"))
			{
				this.Yandere.EmptyHands();

				this.Dumbbell[this.Dumbbells].GetComponent<WeaponScript>().enabled = true;
				this.Dumbbell[this.Dumbbells].GetComponent<PromptScript>().enabled = true;
				this.Dumbbell[this.Dumbbells].GetComponent<WeaponScript>().Prompt.Circle[3].fillAmount = 0.0f;

				Rigidbody dumbbellRigidBody = this.Dumbbell[this.Dumbbells].GetComponent<Rigidbody>();
				dumbbellRigidBody.isKinematic = false;

				this.Dumbbell[this.Dumbbells] = null;
				this.Dumbbells--;
			}
			else if (this.Prompt.Label[0].text == ("     " + "Pour Gasoline"))
			{
				//this.Yandere.PickUp.Empty = true;
				this.Gasoline = true;
				this.Fill();
			}
			else
			{
				this.Sparkles.Play();
				this.Bleached = true;
			}
		}

		if (this.UpdateAppearance)
		{
			if (this.Full)
			{
				if (!this.Gasoline)
				{
					this.Water.transform.localScale = Vector3.Lerp(
						this.Water.transform.localScale,
						new Vector3(0.285f, 1.0f, 0.17f),
						Time.deltaTime * 5.0f * this.FillSpeed);

					this.Water.transform.localPosition = new Vector3(
						this.Water.transform.localPosition.x,
						Mathf.Lerp(this.Water.transform.localPosition.y, 0.20f, Time.deltaTime * 5.0f * this.FillSpeed),
						this.Water.transform.localPosition.z);

					this.Water.material.color = new Color(
						this.Water.material.color.r,
						this.Water.material.color.g,
						this.Water.material.color.b,
						Mathf.Lerp(this.Water.material.color.a, 0.50f, Time.deltaTime * 5.0f));
				}
				else
				{
					this.Gas.transform.localScale = Vector3.Lerp(
						this.Gas.transform.localScale,
						new Vector3(0.285f, 1.0f, 0.17f),
						Time.deltaTime * 5.0f * this.FillSpeed);

					this.Gas.transform.localPosition = new Vector3(
						this.Gas.transform.localPosition.x,
						Mathf.Lerp(this.Gas.transform.localPosition.y, 0.20f, Time.deltaTime * 5.0f * this.FillSpeed),
						this.Gas.transform.localPosition.z);

					this.Gas.material.color = new Color(
						this.Gas.material.color.r,
						this.Gas.material.color.g,
						this.Gas.material.color.b,
						Mathf.Lerp(this.Gas.material.color.a, 0.50f, Time.deltaTime * 5.0f));
				}
			}
			else
			{
				this.Water.transform.localScale = Vector3.Lerp(
					this.Water.transform.localScale,
					new Vector3(0.235f, 1.0f, 0.14f),
					Time.deltaTime * 5.0f);

				this.Water.transform.localPosition = new Vector3(
					this.Water.transform.localPosition.x,
					Mathf.Lerp(this.Water.transform.localPosition.y, 0.0f, Time.deltaTime * 5.0f),
					this.Water.transform.localPosition.z);

				this.Water.material.color = new Color(
					this.Water.material.color.r,
					this.Water.material.color.g,
					this.Water.material.color.b,
					Mathf.Lerp(this.Water.material.color.a, 0.0f, Time.deltaTime * 5.0f));

				this.Gas.transform.localScale = Vector3.Lerp(
					this.Gas.transform.localScale,
					new Vector3(0.235f, 1.0f, 0.14f),
					Time.deltaTime * 5.0f);

				this.Gas.transform.localPosition = new Vector3(
					this.Gas.transform.localPosition.x,
					Mathf.Lerp(this.Gas.transform.localPosition.y, 0.0f, Time.deltaTime * 5.0f),
					this.Gas.transform.localPosition.z);

				this.Gas.material.color = new Color(
					this.Gas.material.color.r,
					this.Gas.material.color.g,
					this.Gas.material.color.b,
					Mathf.Lerp(this.Gas.material.color.a, 0.0f, Time.deltaTime * 5.0f));
			}

			this.Blood.material.color = new Color(
				this.Blood.material.color.r,
				this.Blood.material.color.g,
				this.Blood.material.color.b,
				Mathf.Lerp(this.Blood.material.color.a, this.Bloodiness / 100.0f, Time.deltaTime));

			this.Blood.transform.localPosition = new Vector3(
				this.Blood.transform.localPosition.x,
				this.Water.transform.localPosition.y + 0.0010f,
				this.Blood.transform.localPosition.z);

			this.Blood.transform.localScale = this.Water.transform.localScale;

			this.Timer = Mathf.MoveTowards(this.Timer, 5, Time.deltaTime);

			if (this.Timer == 5)
			{
				this.UpdateAppearance = false;
				this.Timer = 0;
			}
		}

		if (this.Yandere.PickUp != null)
		{
			if (this.Yandere.PickUp.Bucket == this)
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;

				if (Input.GetKeyDown(KeyCode.B))
				{
					this.UpdateAppearance = true;

					if (this.Bloodiness == 0)
					{
						this.Bloodiness = 100.0f;
						this.Gasoline = false;
					}
					else
					{
						this.Bloodiness = 0;
						this.Gasoline = true;
					}
				}
			}
			else
			{
				if (!Trap)
				{
					this.Prompt.enabled = true;
				}
			}
		}
		else
		{
			if (!this.Trap)
			{
				this.Prompt.enabled = true;
			}
		}

		if (this.Fly)
		{
			if (this.Rotate < 360.0f)
			{
				if (this.Rotate == 0.0f)
				{
					if (this.Bloodiness < 50.0f)
					{
						if (!this.Gasoline)
						{
							this.Effect = Instantiate(this.SpillEffect,
								this.transform.position +
								(this.transform.forward * 0.50f) +
								(this.transform.up * 0.50f), this.transform.rotation);
						}
						else
						{
							this.Effect = Instantiate(this.GasSpillEffect,
								this.transform.position +
								(this.transform.forward * 0.50f) +
								(this.transform.up * 0.50f), this.transform.rotation);
							this.Gasoline = false;
						}
					}
					else
					{
						this.Effect = Instantiate(this.BloodSpillEffect,
							this.transform.position +
							(this.transform.forward * 0.50f) +
							(this.transform.up * 0.50f), this.transform.rotation);

						this.Bloodiness = 0.0f;
					}

					if (this.Trap)
					{
						this.Effect.transform.LookAt(
							this.Effect.transform.position - Vector3.up);
					}
					else
					{
						Rigidbody rigidBody = this.GetComponent<Rigidbody>();
						rigidBody.AddRelativeForce(Vector3.forward * 150.0f);
						rigidBody.AddRelativeForce(Vector3.up * 250.0f);

						this.transform.Translate(Vector3.forward * 0.50f);
					}
				}

				this.Rotate += Time.deltaTime * 360.0f;
				this.transform.Rotate(Vector3.right * Time.deltaTime * 360.0f);
			}
			else
			{
				// [af] Removed unused "Spilled" variable.
				this.Sparkles.Stop();
				this.Rotate = 0.0f;
				this.Trap = false;
				this.Fly = false;
			}
		}

		if (Dropped)
		{
			if (this.transform.position.y < .5f)
			{
				this.Dropped = false;
			}
		}
	}

	public void Empty()
	{
		//If Yandere-chan needed to put that bucket of water someplace...
		if (SchemeGlobals.GetSchemeStage(1) == 2)
		{
			SchemeGlobals.SetSchemeStage(1, 1);
			this.Yandere.PauseScreen.Schemes.UpdateInstructions();
		}

		this.UpdateAppearance = true;
		this.Bloodiness = 0.0f;
		this.Bleached = false;
		this.Gasoline = false;
		this.Sparkles.Stop();
		this.Full = false;
	}

	public void Fill()
	{
		//If we're waiting for Yandere-chan to fill a bucket with water...
		if (SchemeGlobals.GetSchemeStage(1) == 1)
		{
			SchemeGlobals.SetSchemeStage(1, 2);
			this.Yandere.PauseScreen.Schemes.UpdateInstructions();
		}

		this.UpdateAppearance = true;
		this.Full = true;
	}

	void OnCollisionEnter(Collision other)
	{
		if (this.Dropped)
		{
			StudentScript Student = other.gameObject.GetComponent<StudentScript>();

			if (Student != null)
			{
				this.GetComponent<AudioSource>().Play();

				while (this.Dumbbells > 0)
				{
					this.Dumbbell[this.Dumbbells].GetComponent<WeaponScript>().enabled = true;
					this.Dumbbell[this.Dumbbells].GetComponent<PromptScript>().enabled = true;
					this.Dumbbell[this.Dumbbells].GetComponent<Collider>().enabled = true;

					Rigidbody dumbbellRigidBody = this.Dumbbell[this.Dumbbells].GetComponent<Rigidbody>();
					dumbbellRigidBody.constraints = RigidbodyConstraints.None;
					dumbbellRigidBody.isKinematic = false;
					dumbbellRigidBody.useGravity = true;

					this.Dumbbell[this.Dumbbells].transform.parent = null;
					this.Dumbbell[this.Dumbbells] = null;
					this.Dumbbells--;
				}

				Student.DeathType = DeathType.Weight;
				Student.BecomeRagdoll();

				this.Dropped = false;

				GameObjectUtils.SetLayerRecursively(this.gameObject, 15);

				Debug.Log (Student.Name + "'s ''Alive'' variable is: " + Student.Alive);
			}
		}
	}
}