using UnityEngine;

public class FanCoverScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public YandereScript Yandere;
	public PromptScript Prompt;
	public StudentScript Rival;
	public SM_rotateThis Fan;

	public ParticleSystem BloodEffects;
	public Projector BloodProjector;
	public Rigidbody MyRigidbody;
	public Transform MurderSpot;
	public GameObject Explosion;
	public GameObject OfferHelp;
	public GameObject Smoke;

	public AudioClip RivalReaction;
	public AudioSource FanSFX;

	public Texture[] YandereBloodTextures;
	public Texture[] BloodTexture;

	public bool Reacted = false;

	public float Timer = 0.0f;
	public int RivalID = 11;
	public int Phase = 0;

	void Start()
	{
		if (this.StudentManager.Students[this.RivalID] == null)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.enabled = false;
		}
		else
		{
			this.Rival = this.StudentManager.Students[this.RivalID];
		}
	}

	void Update()
	{
		if (Vector3.Distance(this.transform.position, this.Yandere.transform.position) < 2.0f)
		{
			if (this.Yandere.Armed)
			{
				// [af] Replaced if/else statement with boolean expression.
				this.Prompt.HideButton[0] = (this.Yandere.EquippedWeapon.WeaponID != 6) ||
					!this.Rival.Meeting;
			}
			else
			{
				this.Prompt.HideButton[0] = true;
			}
		}

		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Yandere.CharacterAnimation.CrossFade(AnimNames.FemaleFanMurderA);
			this.Rival.CharacterAnimation.CrossFade(AnimNames.FemaleFanMurderB);
			this.Rival.OsanaHair.GetComponent<Animation>().CrossFade("fanMurderHair");
			this.Yandere.EmptyHands();

			this.Rival.OsanaHair.transform.parent = this.Rival.transform;
			this.Rival.OsanaHair.transform.localEulerAngles = Vector3.zero;
			this.Rival.OsanaHair.transform.localPosition = Vector3.zero;
			this.Rival.OsanaHair.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

			this.Rival.OsanaHairL.enabled = false;
			this.Rival.OsanaHairR.enabled = false;
			this.Rival.Distracted = true;
			this.Yandere.CanMove = false;
			this.Rival.Meeting = false;

			this.FanSFX.enabled = false;
			this.GetComponent<AudioSource>().Play();

			// [af] Commented in JS code.
			//Yandere.Sanity -= 100;
			//Yandere.UpdateSanity();

			this.transform.localPosition = new Vector3(
				-1.733f,
				.465f,
				.952f);

			this.transform.localEulerAngles = new Vector3(
				-90,
				165,
				0);

			Physics.SyncTransforms();

			Rigidbody rigidBody = this.GetComponent<Rigidbody>();
			rigidBody.isKinematic = false;
			rigidBody.useGravity = true;

			this.Prompt.enabled = false;
			this.Prompt.Hide();
			this.Phase++;
		}

		if (this.Phase > 0)
		{
			if (this.Phase == 1)
			{
				this.Yandere.transform.rotation = Quaternion.Slerp(
					this.Yandere.transform.rotation, this.MurderSpot.rotation, Time.deltaTime * 10.0f);
				this.Yandere.MoveTowardsTarget(this.MurderSpot.position);

				if (this.Yandere.CharacterAnimation[AnimNames.FemaleFanMurderA].time > 3.50f)
				{
					if (!this.Reacted)
					{
						AudioSource.PlayClipAtPoint(this.RivalReaction, Rival.transform.position + new Vector3(0, 1, 0));
						this.Yandere.MurderousActionTimer = this.Yandere.CharacterAnimation[AnimNames.FemaleFanMurderA].length - 3.5f;
						this.Reacted = true;
					}
				}

				if (this.Yandere.CharacterAnimation[AnimNames.FemaleFanMurderA].time > 5.0f)
				{
					this.Rival.LiquidProjector.material.mainTexture = this.Rival.BloodTexture;
					this.Rival.LiquidProjector.enabled = true;
					this.Rival.EyeShrink = 1.0f;

					this.Yandere.BloodTextures = this.YandereBloodTextures;
					this.Yandere.Bloodiness += 20.0f;

					// [af] Added "gameObject" for C# compatibility.
					this.BloodProjector.gameObject.SetActive(true);

					this.BloodProjector.material.mainTexture = this.BloodTexture[1];
					this.BloodEffects.transform.parent = this.Rival.Head;
					this.BloodEffects.transform.localPosition = new Vector3(0.0f, 0.10f, 0.0f);
					this.BloodEffects.Play();

					this.Phase++;
				}
			}
			else if (this.Phase < 10)
			{
				if (this.Phase < 6)
				{
					this.Timer += Time.deltaTime;

					if (this.Timer > 1.0f)
					{
						this.Phase++;

						if ((this.Phase - 1) < 5)
						{
							this.BloodProjector.material.mainTexture = this.BloodTexture[this.Phase - 1];
							this.Yandere.Bloodiness += 20.0f;
							this.Timer = 0.0f;
						}
					}
				}

				if (this.Rival.CharacterAnimation[AnimNames.FemaleFanMurderB].time >=
					this.Rival.CharacterAnimation[AnimNames.FemaleFanMurderB].length)
				{
					this.BloodProjector.material.mainTexture = this.BloodTexture[5];
					this.Yandere.Bloodiness += 20.0f;

					this.Rival.Ragdoll.Decapitated = true;
					this.Rival.OsanaHair.SetActive(false);
					this.Rival.DeathType = DeathType.Weapon;

					this.Rival.BecomeRagdoll();
					this.BloodEffects.Stop();

					this.Explosion.SetActive(true);
					this.Smoke.SetActive(true);
					this.Fan.enabled = false;

					this.Phase = 10;
				}
			}
			else
			{
				if (this.Yandere.CharacterAnimation[AnimNames.FemaleFanMurderA].time >=
					this.Yandere.CharacterAnimation[AnimNames.FemaleFanMurderA].length)
				{
					this.OfferHelp.SetActive(false);
					this.Yandere.CanMove = true;
					this.enabled = false;
				}
			}
		}
	}
}
