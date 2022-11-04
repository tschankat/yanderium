using UnityEngine;
using UnityEngine.SceneManagement;

public class ContainerScript : MonoBehaviour
{
	public Transform[] BodyPartPositions;
	public Transform WeaponSpot;
	public Transform Lid;

	public Collider GardenArea;
	public Collider NEStairs;
	public Collider NWStairs;
	public Collider SEStairs;
	public Collider SWStairs;

	public PickUpScript[] BodyParts;
	public PickUpScript BodyPart;
	public WeaponScript Weapon;

	public PromptScript Prompt;

	public string SpriteName = string.Empty;

	public bool CanDrop = false;
	public bool Open = false;

	public int Contents = 0;

	public int ID = 0;

	public void Start()
	{
		this.GardenArea = GameObject.Find("GardenArea").GetComponent<Collider>();
		this.NEStairs = GameObject.Find("NEStairs").GetComponent<Collider>();
		this.NWStairs = GameObject.Find("NWStairs").GetComponent<Collider>();
		this.SEStairs = GameObject.Find("SEStairs").GetComponent<Collider>();
		this.SWStairs = GameObject.Find("SWStairs").GetComponent<Collider>();
	}

	void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Prompt.Circle[0].fillAmount = 1.0f;

			this.Open = !this.Open;

			this.UpdatePrompts();
		}

		if (this.Prompt.Circle[1].fillAmount == 0.0f)
		{
			this.Prompt.Circle[1].fillAmount = 1.0f;

			if (this.Prompt.Yandere.Armed)
			{
				this.Weapon = this.Prompt.Yandere.EquippedWeapon;

				this.Prompt.Yandere.EmptyHands();

				this.Weapon.transform.parent = this.WeaponSpot;
				this.Weapon.transform.localPosition = Vector3.zero;
				this.Weapon.transform.localEulerAngles = Vector3.zero;
				this.Weapon.gameObject.GetComponent<Rigidbody>().useGravity = false;
				this.Weapon.MyCollider.enabled = false;
				this.Weapon.Prompt.Hide();
				this.Weapon.Prompt.enabled = false;
			}
			else
			{
				this.BodyPart = this.Prompt.Yandere.PickUp;

				this.Prompt.Yandere.EmptyHands();

				this.BodyPart.transform.parent = this.BodyPartPositions[this.BodyPart.GetComponent<BodyPartScript>().Type];
				this.BodyPart.transform.localPosition = Vector3.zero;
				this.BodyPart.transform.localEulerAngles = Vector3.zero;
				this.BodyPart.gameObject.GetComponent<Rigidbody>().useGravity = false;
				this.BodyPart.MyCollider.enabled = false;

				this.BodyParts[this.BodyPart.GetComponent<BodyPartScript>().Type] = this.BodyPart;
			}

			this.Contents++;

			this.UpdatePrompts();
		}

		if (this.Prompt.Circle[3].fillAmount == 0.0f)
		{
			this.Prompt.Circle[3].fillAmount = 1.0f;

			if (!this.Open)
			{
				this.transform.parent = this.Prompt.Yandere.Backpack;
				this.transform.localPosition = Vector3.zero;
				this.transform.localEulerAngles = Vector3.zero;

				this.Prompt.Yandere.Container = this;
				this.Prompt.Yandere.WeaponMenu.UpdateSprites();
				this.Prompt.Yandere.ObstacleDetector.gameObject.SetActive(true);

				this.Prompt.MyCollider.enabled = false;
				this.Prompt.Hide();
				this.Prompt.enabled = false;

				Rigidbody rigidBody = this.GetComponent<Rigidbody>();
				rigidBody.isKinematic = true;
				rigidBody.useGravity = false;
			}
			else
			{
				if (this.Weapon != null)
				{
					this.Weapon.Prompt.Circle[3].fillAmount = -1.0f;
					this.Weapon.Prompt.enabled = true;
					this.Weapon = null;
				}
				else
				{
					this.BodyPart = null;

					// [af] Converted while loop to for loop.
					for (this.ID = 1; this.BodyPart == null; this.ID++)
					{
						this.BodyPart = this.BodyParts[this.ID];
						this.BodyParts[this.ID] = null;
					}

					this.BodyPart.Prompt.Circle[3].fillAmount = -1.0f;
				}

				this.Contents--;

				this.UpdatePrompts();
			}
		}

		// [af] Replaced if/else statement with assignment and ternary expression.
		this.Lid.localEulerAngles = new Vector3(
			this.Lid.localEulerAngles.x,
			this.Lid.localEulerAngles.y,
			Mathf.Lerp(this.Lid.localEulerAngles.z, this.Open ? 90.0f : 0.0f, Time.deltaTime * 10.0f));

		if (this.Weapon != null)
		{
			this.Weapon.transform.localPosition = Vector3.zero;
			this.Weapon.transform.localEulerAngles = Vector3.zero;
		}

		// [af] Converted while loop to for loop.
		for (this.ID = 1; this.ID < this.BodyParts.Length; this.ID++)
		{
			if (this.BodyParts[this.ID] != null)
			{
				this.BodyParts[this.ID].transform.localPosition = Vector3.zero;
				this.BodyParts[this.ID].transform.localEulerAngles = Vector3.zero;
			}
		}
	}

	public void Drop()
	{
		/*
		this.CanDrop = !(this.GardenArea.bounds.Contains(transform.position) ||
			this.NEStairs.bounds.Contains(transform.position) ||
			this.NWStairs.bounds.Contains(transform.position) ||
			this.SEStairs.bounds.Contains(transform.position) ||
			this.SWStairs.bounds.Contains(transform.position));
		*/

		//if (this.CanDrop)
		//{
			this.transform.parent = null;

			if (this.enabled)
			{			
				this.transform.position = this.Prompt.Yandere.ObstacleDetector.transform.position + new Vector3(0, .5f, 0);
				this.transform.eulerAngles = this.Prompt.Yandere.ObstacleDetector.transform.eulerAngles;
			}

			this.Prompt.Yandere.Container = null;
			this.Prompt.MyCollider.enabled = true;
			this.Prompt.enabled = true;

			Rigidbody rigidBody = this.GetComponent<Rigidbody>();
			rigidBody.isKinematic = false;
			rigidBody.useGravity = true;
		//}
	}

	public void UpdatePrompts()
	{
		if (this.Open)
		{
			this.Prompt.Label[0].text = "     Close";

			if (this.Contents > 0)
			{
				this.Prompt.Label[3].text = "     Remove";
				this.Prompt.HideButton[3] = false;
			}
			else
			{
				this.Prompt.HideButton[3] = true;
			}

			if (this.Prompt.Yandere.Armed)
			{
				if (!this.Prompt.Yandere.EquippedWeapon.Concealable)
				{
					if (this.Weapon == null)
					{
						this.Prompt.Label[1].text = "     Insert";
						this.Prompt.HideButton[1] = false;
					}
					else
					{
						this.Prompt.HideButton[1] = true;
					}
				}
				else
				{
					this.Prompt.HideButton[1] = true;
				}
			}
			else if (this.Prompt.Yandere.PickUp != null)
			{
				if (this.Prompt.Yandere.PickUp.BodyPart != null)
				{
					if (this.BodyParts[this.Prompt.Yandere.PickUp.gameObject.GetComponent<BodyPartScript>().Type] == null)
					{
						this.Prompt.Label[1].text = "     Insert";
						this.Prompt.HideButton[1] = false;
					}
					else
					{
						this.Prompt.HideButton[1] = true;
					}
				}
				else
				{
					this.Prompt.HideButton[1] = true;
				}
			}
			else
			{
				this.Prompt.HideButton[1] = true;
			}
		}
		else
		{
			if (this.Prompt.Label[0] != null)
			{
				this.Prompt.Label[0].text = "     Open";
				this.Prompt.HideButton[1] = true;

				this.Prompt.Label[3].text = "     Wear";
				this.Prompt.HideButton[3] = false;
			}
		}
	}
}
