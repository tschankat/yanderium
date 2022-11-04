using UnityEngine;

public class GardenHoleScript : MonoBehaviour
{
	public YandereScript Yandere;
	public RagdollScript Corpse;
	public PromptScript Prompt;

	public Collider MyCollider;
	public MeshFilter MyMesh;

	public GameObject Carrots;
	public GameObject Pile;

	public Mesh MoundMesh;
	public Mesh HoleMesh;

	public bool Bury = false;
	public bool Dug = false;

	public int VictimID = 0;
	public int ID = 0;

	void Start()
	{
		if (SchoolGlobals.GetGardenGraveOccupied(this.ID))
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;

			this.enabled = false;
		}
		else
		{
			// [af] Commented in JS code.
			//Prompt.enabled = true;
		}
	}

	void Update()
	{
		if (this.Yandere.transform.position.z < (this.transform.position.z - 0.50f))
		{
			if (this.Yandere.Equipped > 0)
			{
				if (this.Yandere.EquippedWeapon.WeaponID == 10)
				{
					this.Prompt.enabled = true;
				}
				else
				{
					if (this.Prompt.enabled)
					{
						this.Prompt.Hide();
						this.Prompt.enabled = false;
					}
				}
			}
			else
			{
				if (this.Prompt.enabled)
				{
					this.Prompt.Hide();
					this.Prompt.enabled = false;
				}
			}
		}
		else
		{
			if (this.Prompt.enabled)
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
			}
		}

		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Prompt.Circle[0].fillAmount = 1;

			if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
				// [af] Converted while loop to foreach loop.
				foreach (string armedAnim in this.Yandere.ArmedAnims)
				{
					this.Yandere.CharacterAnimation[armedAnim].weight = 0.0f;
				}

				this.Yandere.transform.rotation = Quaternion.LookRotation(new Vector3(
					this.transform.position.x,
					this.Yandere.transform.position.y,
					this.transform.position.z) - this.Yandere.transform.position);
				this.Yandere.RPGCamera.transform.eulerAngles = this.Yandere.DigSpot.eulerAngles;
				this.Yandere.RPGCamera.transform.position = this.Yandere.DigSpot.position;
				this.Yandere.EquippedWeapon.gameObject.SetActive(false);
				this.Yandere.CharacterAnimation[AnimNames.FemaleShovelBury].time = 0.0f;
				this.Yandere.CharacterAnimation[AnimNames.FemaleShovelDig].time = 0.0f;
				this.Yandere.FloatingShovel.SetActive(true);
				this.Yandere.RPGCamera.enabled = false;
				this.Yandere.CanMove = false;
				this.Yandere.DigPhase = 1;

				this.Carrots.SetActive(false);

				this.Prompt.Circle[0].fillAmount = 1.0f;

				if (!this.Dug)
				{
					this.Yandere.FloatingShovel.GetComponent<Animation>()["Dig"].time = 0.0f;
					this.Yandere.FloatingShovel.GetComponent<Animation>().Play("Dig");

					this.Yandere.Character.GetComponent<Animation>().Play(AnimNames.FemaleShovelDig);
					this.Yandere.Digging = true;

					this.Prompt.Label[0].text = "     " + "Fill";
					this.MyCollider.isTrigger = true;
					this.MyMesh.mesh = this.HoleMesh;
					this.Pile.SetActive(true);
					this.Dug = true;
				}
				else
				{
					this.Yandere.FloatingShovel.GetComponent<Animation>()["Bury"].time = 0.0f;
					this.Yandere.FloatingShovel.GetComponent<Animation>().Play("Bury");

					this.Yandere.CharacterAnimation.Play(AnimNames.FemaleShovelBury);
					this.Yandere.Burying = true;

					this.Prompt.Label[0].text = "     " + "Dig";
					this.MyCollider.isTrigger = false;
					this.MyMesh.mesh = this.MoundMesh;
					this.Pile.SetActive(false);
					this.Dug = false;
				}

				if (this.Bury)
				{
					this.Yandere.Police.Corpses--;

					if (this.Yandere.Police.SuicideScene)
					{
						if (this.Yandere.Police.Corpses == 1)
						{
							this.Yandere.Police.MurderScene = false;
						}
					}

					if (this.Yandere.Police.Corpses == 0)
					{
						this.Yandere.Police.MurderScene = false;
					}

					this.VictimID = this.Corpse.StudentID;
					this.Corpse.Remove();

					this.Prompt.Hide();
					this.Prompt.enabled = false;

					this.enabled = false;
				}
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (this.Dug)
		{
			if (other.gameObject.layer == 11)
			{
				this.Prompt.Label[0].text = "     " + "Bury";
				this.Corpse = other.transform.root.gameObject.GetComponent<RagdollScript>();
				this.Bury = true;
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (this.Dug)
		{
			if (other.gameObject.layer == 11)
			{
				this.Prompt.Label[0].text = "     " + "Fill";
				this.Corpse = null;
				this.Bury = false;
			}
		}
	}

	public void EndOfDayCheck()
	{
		if (this.VictimID > 0)
		{
			StudentGlobals.SetStudentMissing(this.VictimID, true);
			SchoolGlobals.SetGardenGraveOccupied(this.ID, true);
		}
	}
}