using UnityEngine;

public class WeaponManagerScript : MonoBehaviour
{
	public WeaponScript[] DelinquentWeapons;
	public WeaponScript[] Weapons;
    public YandereScript Yandere;
    public JsonScript JSON;

	//public Transform[] MisplacedWeapons;

	public int[] Victims;

	public int MisplacedWeapons = 0;
	public int MurderWeapons = 0;
	public int Fingerprints = 0;

    public int YandereWeapon1 = -1;
    public int YandereWeapon2 = -1;
    public int YandereWeapon3 = -1;

    public int ReturnWeaponID = -1;
    public int ReturnStudentID = -1;

    public int OriginalEquipped = -1;
    public int OriginalWeapon = -1;

    public int Frame = 0;

    public Texture Flower;
	public Texture Blood;

	public void Start()
	{
		for (int ID = 0; ID < this.Weapons.Length; ID++)
		{
			this.Weapons[ID].GlobalID = ID;

			//Debug.Log("It's the beginning of a new day. Checking Weapon #" + ID);

			if (WeaponGlobals.GetWeaponStatus(ID) == 1)
			{
				//Debug.Log("Weapon #" + ID + " was destroyed! Disabling it!");

				this.Weapons[ID].gameObject.SetActive(false);
			}
		}

		this.ChangeBloodTexture();
	}

	public void UpdateLabels()
	{
		// [af] Converted while loop to foreach loop.
		foreach (WeaponScript weapon in this.Weapons)
		{
            if (weapon != null)
            {
			    weapon.UpdateLabel();
            }
        }
	}

	public bool YandereGuilty = false;

	public void CheckWeapons()
	{
		this.MurderWeapons = 0;
		this.Fingerprints = 0;

		// [af] Converted while loop to for loop.
		for (int ID = 0; ID < this.Victims.Length; ID++)
		{
			this.Victims[ID] = 0;
		}

		// [af] Converted while loop to foreach loop.
		foreach (WeaponScript weapon in this.Weapons)
		{
			if (weapon != null)
			{
				if (weapon.Blood.enabled)
				{
					if (!weapon.AlreadyExamined)
					{
						this.MurderWeapons++;

						if (weapon.FingerprintID > 0)
						{
							this.Fingerprints++;

							// [af] Converted while loop to for loop.
							for (int GuiltID = 0; GuiltID < weapon.Victims.Length; GuiltID++)
							{
								if (weapon.Victims[GuiltID])
								{
									this.Victims[GuiltID] = weapon.FingerprintID;
								}
							}
						}
					}
				}
			}
		}
	}

	public void CleanWeapons()
	{
		// [af] Converted while loop to foreach loop.
		foreach (WeaponScript weapon in this.Weapons)
		{
			if (weapon != null)
			{
				weapon.Blood.enabled = false;
				weapon.FingerprintID = 0;
			}
		}
	}

	public void ChangeBloodTexture()
	{
		foreach (WeaponScript weapon in this.Weapons)
		{
			if (weapon != null)
			{
				if (!GameGlobals.CensorBlood)
				{
					weapon.Blood.material.mainTexture = Blood;
					weapon.Blood.material.SetColor("_TintColor", new Color(.25f, .25f, .25f, .5f));
				}
				else
				{
					weapon.Blood.material.mainTexture = Flower;
					weapon.Blood.material.SetColor("_TintColor", new Color(.5f, .5f, .5f, .5f));
				}
			}
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Z))
		{
			this.CheckWeapons();

			// [af] Converted while loop to for loop.
			for (int ID = 0; ID < this.Victims.Length; ID++)
			{
				if (this.Victims[ID] != 0)
				{
					if (this.Victims[ID] == 100)
					{
						Debug.Log("The student named " + this.JSON.Students[ID].Name + 
							" was killed by Yandere-chan!");
					}
					else
					{
						Debug.Log("The student named " + this.JSON.Students[ID].Name + 
							" was killed by " + this.JSON.Students[this.Victims[ID]].Name + "!");
					}
				}
			}
		}

        if (this.OriginalWeapon > -1)
        {
            //if (this.Frame > 1)
            //{
                Debug.Log("Re-equipping original weapon.");

                this.Yandere.WeaponMenu.Selected = this.OriginalEquipped;
                this.Yandere.WeaponMenu.Equip();

                this.OriginalWeapon = -1;

            //  this.Frame = 0;
            //}

            this.Frame++;
        }
	}

	public void TrackDumpedWeapons()
	{
		for (int ID = 0; ID < this.Weapons.Length; ID++)
		{
			//Debug.Log("It's the end of the day. Checking Weapon #" + ID);

			if (Weapons[ID] == null)
			{
				Debug.Log("Weapon #" + ID + " was destroyed! Setting status to 1!");

				//WeaponGlobals.SetWeaponStatus(ID, 1);
			}
		}
	}

    public void SetEquippedWeapon1(WeaponScript Weapon)
    {
        for (int ID = 0; ID < this.Weapons.Length; ID++)
        {
            if (Weapons[ID] == Weapon)
            {
                YandereWeapon1 = ID;
            }
        }
    }

    public void SetEquippedWeapon2(WeaponScript Weapon)
    {
        for (int ID = 0; ID < this.Weapons.Length; ID++)
        {
            if (Weapons[ID] == Weapon)
            {
                YandereWeapon2 = ID;
            }
        }
    }

    public void SetEquippedWeapon3(WeaponScript Weapon)
    {
        for (int ID = 0; ID < this.Weapons.Length; ID++)
        {
            if (Weapons[ID] == Weapon)
            {
                YandereWeapon3 = ID;
            }
        }
    }

    public void EquipWeaponsFromSave()
    {
        this.OriginalEquipped = this.Yandere.Equipped;

             if (this.Yandere.Equipped == 1){this.OriginalWeapon = YandereWeapon1;}
        else if (this.Yandere.Equipped == 2){this.OriginalWeapon = YandereWeapon2;}
        else if (this.Yandere.Equipped == 3){this.OriginalWeapon = YandereWeapon3;}

        if (this.Yandere.Equipped > 0)
        {
            //Debug.Log("The player had a weapon equipped in Slot #" + Yandere.Equipped + ". That weapon was #" + this.OriginalWeapon + " in the list of all weapons.");
            this.Yandere.Unequip();
        }

        if (this.Yandere.Weapon[1] != null)
        {
            this.Yandere.Weapon[1].Drop();
        }

        if (this.Yandere.Weapon[2] != null)
        {
            this.Yandere.Weapon[2].Drop();
        }

        if (YandereWeapon1 > -1)
        {
            //Debug.Log("Looks like the player had a " + Weapons[YandereWeapon1].gameObject.name + " in their possession when they saved.");

            Weapons[YandereWeapon1].Prompt.Circle[3].fillAmount = 0;
            Weapons[YandereWeapon1].gameObject.SetActive(true);
            Weapons[YandereWeapon1].UnequipImmediately = true;
        }

        if (YandereWeapon2 > -1)
        {
            //Debug.Log("Looks like the player had a " + Weapons[YandereWeapon2].gameObject.name + " in their possession when they saved.");

            Weapons[YandereWeapon2].Prompt.Circle[3].fillAmount = 0;
            Weapons[YandereWeapon2].gameObject.SetActive(true);
            Weapons[YandereWeapon2].UnequipImmediately = true;
        }

        if (YandereWeapon3 > -1)
        {
            //Debug.Log("Looks like the player had a " + Weapons[YandereWeapon3].gameObject.name + " equipped when they saved.");

            Weapons[YandereWeapon3].Prompt.Circle[3].fillAmount = 0;
            Weapons[YandereWeapon3].gameObject.SetActive(true);
            Weapons[YandereWeapon3].UnequipImmediately = true;
        }
    }

    public void UpdateDelinquentWeapons()
    {
        for (int ID = 1; ID < this.DelinquentWeapons.Length; ID++)
        {
            if (DelinquentWeapons[ID].DelinquentOwned)
            {
                //Debug.Log("Updating the position of " + this.DelinquentWeapons[ID].gameObject.name);

                this.DelinquentWeapons[ID].transform.localEulerAngles = new Vector3(0, 0, 0);
                this.DelinquentWeapons[ID].transform.localPosition = new Vector3(0, 0, 0);
            }
            else
            {
                this.DelinquentWeapons[ID].transform.parent = null;
            }
        }
    }

    public void RestoreWeaponToStudent()
    {
        if (this.ReturnWeaponID > -1)
        {
            this.Yandere.StudentManager.Students[this.ReturnStudentID].BloodPool = this.Weapons[this.ReturnWeaponID].transform;
            this.Yandere.StudentManager.Students[this.ReturnStudentID].BloodPool = this.Weapons[this.ReturnWeaponID].transform;
            this.Yandere.StudentManager.Students[this.ReturnStudentID].BloodPool = this.Weapons[this.ReturnWeaponID].transform;

            this.Yandere.StudentManager.Students[this.ReturnStudentID].CurrentDestination = this.Weapons[this.ReturnWeaponID].Origin;
            this.Yandere.StudentManager.Students[this.ReturnStudentID].Pathfinding.target = this.Weapons[this.ReturnWeaponID].Origin;

            this.Weapons[this.ReturnWeaponID].Prompt.Hide();
            this.Weapons[this.ReturnWeaponID].Prompt.enabled = false;
            this.Weapons[this.ReturnWeaponID].enabled = false;
            this.Weapons[this.ReturnWeaponID].Returner = this.Yandere.StudentManager.Students[this.ReturnStudentID];

            this.Weapons[this.ReturnWeaponID].transform.parent = this.Yandere.StudentManager.Students[this.ReturnStudentID].RightHand;
            this.Weapons[this.ReturnWeaponID].transform.localPosition = new Vector3(0, 0, 0);
            this.Weapons[this.ReturnWeaponID].transform.localEulerAngles = new Vector3(0, 0, 0);

            this.Yandere.StudentManager.Students[this.ReturnStudentID].CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
        }
    }

    /*

    1. Bloody weapon with no fingerprints, corpse is present.
    2. Bloody weapon with no fingerprints, corpse is gone.
    3. Bloody weapon with Yandere-chan's fingerprints, corpse is present.
    4. Bloody weapon with Yandere-chan's fingerprints, corpse is gone.
    5. Bloody weapon with another girl's fingerprints, corpse is present.
    6. Bloody weapon with another girl's fingerprints, corpse is gone.

    */
}