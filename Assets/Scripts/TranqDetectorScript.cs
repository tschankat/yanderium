using UnityEngine;

public class TranqDetectorScript : MonoBehaviour
{
	public YandereScript Yandere;
	public DoorScript Door;

	public UIPanel Checklist;

	public Collider MyCollider;

	public UILabel KidnappingLabel;

	public UISprite TranquilizerIcon;
	public UISprite FollowerIcon;
	public UISprite BiologyIcon;
	public UISprite SyringeIcon;
	public UISprite DoorIcon;

	public bool StopChecking = false;

	public AudioClip[] TranqClips;

	// You may need to use "MyCollider.bounds.Contains(Yandere.Follower.transform.position)" somewhere in here.

	void Start()
	{
		this.Checklist.alpha = 0.0f;
	}

	void Update()
	{
		if (!this.StopChecking)
		{
			if (this.MyCollider.bounds.Contains(this.Yandere.transform.position))
			{
				if (SchoolGlobals.KidnapVictim > 0)
				{
					this.KidnappingLabel.text = "There is no room for another prisoner in your basement.";
				}
				else
				{
					if (this.Yandere.Inventory.Tranquilizer || this.Yandere.Inventory.Sedative)
					{
						this.TranquilizerIcon.spriteName = "Yes";
					}
					else
					{
						this.TranquilizerIcon.spriteName = "No";
					}

					if (this.Yandere.Followers != 1)
					{
						this.FollowerIcon.spriteName = "No";
					}
					else
					{
						if (this.Yandere.Follower.Male)
						{
							this.KidnappingLabel.text = "You cannot kidnap male students at this point in time.";
							this.FollowerIcon.spriteName = "No";
						}
						else
						{
							this.KidnappingLabel.text = "Kidnapping Checklist";
							this.FollowerIcon.spriteName = "Yes";
						}
					}

					// [af] Replaced if/else statement with ternary expression.
					this.BiologyIcon.spriteName = 
						((this.Yandere.Class.BiologyGrade + this.Yandere.Class.BiologyBonus) != 0) ? "Yes" : "No";

					if (!this.Yandere.Armed)
					{
						this.SyringeIcon.spriteName = "No";
					}
					else
					{
						if (this.Yandere.EquippedWeapon.WeaponID != 3)
						{
							this.SyringeIcon.spriteName = "No";
						}
						else
						{
							this.SyringeIcon.spriteName = "Yes";
						}
					}

					if (this.Door.Open || this.Door.Timer < 1)
					{
						this.DoorIcon.spriteName = "No";
					}
					else
					{
						this.DoorIcon.spriteName = "Yes";
					}
				}

				this.Checklist.alpha = Mathf.MoveTowards(this.Checklist.alpha, 1.0f, Time.deltaTime);
			}
			else
			{
				this.Checklist.alpha = Mathf.MoveTowards(this.Checklist.alpha, 0.0f, Time.deltaTime);
			}
		}
		else
		{
			this.Checklist.alpha = Mathf.MoveTowards(this.Checklist.alpha, 0.0f, Time.deltaTime);

			if (this.Checklist.alpha == 0.0f)
			{
				this.enabled = false;
			}
		}
	}

	public void TranqCheck()
	{
		if (!this.StopChecking)
		{
			if (this.KidnappingLabel.text == "Kidnapping Checklist")
			{
				if ((this.TranquilizerIcon.spriteName == "Yes") &&
					(this.FollowerIcon.spriteName == "Yes") &&
					(this.BiologyIcon.spriteName == "Yes") &&
					(this.SyringeIcon.spriteName == "Yes") &&
					(this.DoorIcon.spriteName == "Yes"))
				{
					AudioSource audioSource = this.GetComponent<AudioSource>();
					audioSource.clip = this.TranqClips[Random.Range(0, this.TranqClips.Length)];
					audioSource.Play();

					this.Door.Prompt.Hide();
					this.Door.Prompt.enabled = false;
					this.Door.enabled = false;

					this.Yandere.Inventory.Tranquilizer = false;

					if (!this.Yandere.Follower.Male)
					{
						this.Yandere.CanTranq = true;
					}

					this.Yandere.EquippedWeapon.Type = WeaponType.Syringe;
					this.Yandere.AttackManager.Stealth = true;

					this.StopChecking = true;
				}
			}
		}
	}

    public void GarroteAttack()
    {
        AudioSource audioSource = this.GetComponent<AudioSource>();
        audioSource.clip = this.TranqClips[Random.Range(0, this.TranqClips.Length)];
        audioSource.Play();

        this.Yandere.EquippedWeapon.Type = WeaponType.Syringe;
        this.Yandere.AttackManager.Stealth = true;

        this.StopChecking = true;
    }
}
