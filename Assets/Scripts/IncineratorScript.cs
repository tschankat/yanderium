using UnityEngine;

public class IncineratorScript : MonoBehaviour
{
	public YandereScript Yandere;
	public PromptScript Prompt;
	public ClockScript Clock;

	public AudioClip IncineratorActivate;
	public AudioClip IncineratorClose;
	public AudioClip IncineratorOpen;

	public AudioSource FlameSound;

	public ParticleSystem Flames;
	public ParticleSystem Smoke;

	public Transform DumpPoint;
	public Transform RightDoor;
	public Transform LeftDoor;

	public GameObject Panel;

	public UILabel TimeLabel;
	public UISprite Circle;

	public bool YandereHoldingEvidence = false;
	public bool Ready = false;
	public bool Open = false;

	public int DestroyedEvidence = 0;
	public int BloodyClothing = 0;
	public int MurderWeapons = 0;
	public int BodyParts = 0;
	public int Corpses = 0;
	public int Victims = 0;
	public int Limbs = 0;
	public int ID = 0;

	public float OpenTimer = 0.0f;
	public float Timer = 0.0f;

	public int[] EvidenceList;
	public int[] CorpseList;
	public int[] VictimList;
	public int[] LimbList;

	public int[] ConfirmedDead;

	void Start()
	{
		this.Panel.SetActive(false);
		this.Prompt.enabled = true;
	}

	void Update()
	{
		AudioSource audioSource = this.GetComponent<AudioSource>();

		if (!this.Open)
		{
			this.RightDoor.transform.localEulerAngles = new Vector3(
				this.RightDoor.transform.localEulerAngles.x,
				Mathf.MoveTowards(this.RightDoor.transform.localEulerAngles.y, 0.0f, Time.deltaTime * 360.0f),
				this.RightDoor.transform.localEulerAngles.z);

			this.LeftDoor.transform.localEulerAngles = new Vector3(
				this.LeftDoor.transform.localEulerAngles.x,
				Mathf.MoveTowards(this.LeftDoor.transform.localEulerAngles.y, 0.0f, Time.deltaTime * 360.0f),
				this.LeftDoor.transform.localEulerAngles.z);

			if (this.RightDoor.transform.localEulerAngles.y < 36.0f)
			{
				if (this.RightDoor.transform.localEulerAngles.y > 0.0f)
				{
					audioSource.clip = this.IncineratorClose;
					audioSource.Play();
				}

				this.RightDoor.transform.localEulerAngles = new Vector3(
					this.RightDoor.transform.localEulerAngles.x,
					0.0f,
					this.RightDoor.transform.localEulerAngles.z);
			}
		}
		else
		{
			if (this.RightDoor.transform.localEulerAngles.y == 0.0f)
			{
				audioSource.clip = this.IncineratorOpen;
				audioSource.Play();
			}

			this.RightDoor.transform.localEulerAngles = new Vector3(
				this.RightDoor.transform.localEulerAngles.x,
				Mathf.Lerp(this.RightDoor.transform.localEulerAngles.y, 135.0f, Time.deltaTime * 10.0f),
				this.RightDoor.transform.localEulerAngles.z);

			this.LeftDoor.transform.localEulerAngles = new Vector3(
				this.LeftDoor.transform.localEulerAngles.x,
				Mathf.Lerp(this.LeftDoor.transform.localEulerAngles.y, 135.0f, Time.deltaTime * 10.0f),
				this.LeftDoor.transform.localEulerAngles.z);

			if (this.RightDoor.transform.localEulerAngles.y > 134.0f)
			{
				this.RightDoor.transform.localEulerAngles = new Vector3(
					this.RightDoor.transform.localEulerAngles.x,
					135.0f,
					this.RightDoor.transform.localEulerAngles.z);
			}
		}

		if (this.OpenTimer > 0.0f)
		{
			this.OpenTimer -= Time.deltaTime;

			if (this.OpenTimer <= 1.0f)
			{
				this.Open = false;
			}

			if (this.OpenTimer <= 0.0f)
			{
				this.Prompt.enabled = true;
			}
		}
		else
		{
			if (!this.Smoke.isPlaying)
			{
				// [af] Replaced if/else statement with boolean expression.
				this.YandereHoldingEvidence = this.Yandere.Ragdoll != null;

				if (!this.YandereHoldingEvidence)
				{
					if (this.Yandere.PickUp != null)
					{
						// [af] Replaced if/else statements with boolean expression.
						this.YandereHoldingEvidence = this.Yandere.PickUp.Evidence ||
							this.Yandere.PickUp.Garbage;
					}
					else
					{
						this.YandereHoldingEvidence = false;
					}
				}

				if (!this.YandereHoldingEvidence)
				{
					if (this.Yandere.EquippedWeapon != null)
					{
						// [af] Replaced if/else statement with boolean expression.
						this.YandereHoldingEvidence = this.Yandere.EquippedWeapon.MurderWeapon;
					}
					else
					{
						this.YandereHoldingEvidence = false;
					}
				}

				if (!this.YandereHoldingEvidence)
				{
					if (!this.Prompt.HideButton[3])
					{
						this.Prompt.HideButton[3] = true;
					}
				}
				else
				{
					if (this.Prompt.HideButton[3])
					{
						this.Prompt.HideButton[3] = false;
					}
				}

				if (this.Yandere.Chased || this.Yandere.Chasers > 0 || !this.YandereHoldingEvidence)
				{
					if (!this.Prompt.HideButton[3])
					{
						this.Prompt.HideButton[3] = true;
					}
				}

				if (this.Ready)
				{
					if (!this.Smoke.isPlaying)
					{
						if (this.Prompt.HideButton[0])
						{
							this.Prompt.HideButton[0] = false;
						}
					}
					else
					{
						if (!this.Prompt.HideButton[0])
						{
							this.Prompt.HideButton[0] = true;
						}
					}
				}
			}
		}

		///////////////////
		///// DUMPING /////
		///////////////////

		if (this.Prompt.Circle[3].fillAmount == 0.0f)
		{
			Time.timeScale = 1.0f;

			if (this.Yandere.Ragdoll != null)
			{
				// [af] Replaced if/else statement with ternary expression.
				this.Yandere.Character.GetComponent<Animation>().CrossFade(
					this.Yandere.Carrying ? AnimNames.FemaleCarryIdleA : AnimNames.FemaleDragIdle);

				this.Yandere.YandereVision = false;
				this.Yandere.CanMove = false;
				this.Yandere.Dumping = true;

				this.Prompt.Hide();
				this.Prompt.enabled = false;

				this.Victims++;
				this.VictimList[this.Victims] =
					this.Yandere.Ragdoll.GetComponent<RagdollScript>().StudentID;

				this.Open = true;
			}

			if (this.Yandere.PickUp != null)
			{
				if (this.Yandere.PickUp.BodyPart != null)
				{
					this.Limbs++;
					this.LimbList[this.Limbs] =
						this.Yandere.PickUp.GetComponent<BodyPartScript>().StudentID;
				}

				this.Yandere.PickUp.Incinerator = this;
				this.Yandere.PickUp.Dumped = true;
				this.Yandere.PickUp.Drop();

				this.Prompt.Hide();
				this.Prompt.enabled = false;

				this.OpenTimer = 2.0f;

				this.Ready = true;
				this.Open = true;
			}

			WeaponScript yandereWeapon = this.Yandere.EquippedWeapon;

			if (yandereWeapon != null)
			{
				this.DestroyedEvidence++;
				this.EvidenceList[this.DestroyedEvidence] = yandereWeapon.WeaponID;

				yandereWeapon.Incinerator = this;
				yandereWeapon.Dumped = true;
				yandereWeapon.Drop();

				this.Prompt.Hide();
				this.Prompt.enabled = false;

				this.OpenTimer = 2.0f;

				this.Ready = true;
				this.Open = true;
			}
		}

		//////////////////////
		///// ACTIVATING /////
		//////////////////////

		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Prompt.Circle[0].fillAmount = 1;
			this.Panel.SetActive(true);
			this.Timer = 60.0f;

			audioSource.clip = this.IncineratorActivate;
			audioSource.Play();

			this.Flames.Play();
			this.Smoke.Play();

			this.Prompt.Hide();
			this.Prompt.enabled = false;

            Debug.Log("Incinerating " + this.BloodyClothing + " bloody clothing.");

			this.Yandere.Police.IncineratedWeapons += this.MurderWeapons;
			this.Yandere.Police.BloodyClothing -= this.BloodyClothing;
			this.Yandere.Police.BloodyWeapons -= this.MurderWeapons;
			this.Yandere.Police.BodyParts -= this.BodyParts;
			this.Yandere.Police.Corpses -= this.Corpses;

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

			this.BloodyClothing = 0;
			this.MurderWeapons = 0;
			this.BodyParts = 0;
			this.Corpses = 0;

			this.ID = 0;

			while (this.ID < 101)
			{
				if (this.Yandere.StudentManager.Students[CorpseList[this.ID]] != null)
				{
					this.Yandere.StudentManager.Students[CorpseList[this.ID]].Ragdoll.Disposed = true;
					this.ConfirmedDead[this.ID] = this.CorpseList[this.ID];

					if (this.Yandere.StudentManager.Students[CorpseList[this.ID]].Ragdoll.Drowned)
					{
						this.Yandere.Police.DrownVictims--;
					}
				}

				this.ID++;
			}
		}

		if (this.Smoke.isPlaying)
		{
			this.Timer -= Time.deltaTime * (this.Clock.TimeSpeed / 60.0f);

			this.FlameSound.volume += Time.deltaTime;

			this.Circle.fillAmount = 1.0f - (this.Timer / 60.0f);

			if (this.Timer <= 0.0f)
			{
				this.Prompt.HideButton[0] = true;
				this.Prompt.enabled = true;
				this.Panel.SetActive(false);
				this.Ready = false;

				this.Flames.Stop();
				this.Smoke.Stop();
			}
		}
		else
		{
			this.FlameSound.volume -= Time.deltaTime;
		}

		if (this.Panel.activeInHierarchy)
		{
			float RoundedTime = Mathf.CeilToInt(this.Timer * 60.0f);

			float Minutes = Mathf.Floor(RoundedTime / 60.0f);
			float Seconds = Mathf.RoundToInt(RoundedTime % 60.0f);

			this.TimeLabel.text = string.Format("{0:00}:{1:00}", Minutes, Seconds);
		}
	}

	public void SetVictimsMissing()
	{
		foreach (int corpseID in this.ConfirmedDead)
		{
			StudentGlobals.SetStudentMissing(corpseID, true);
		}
	}
}