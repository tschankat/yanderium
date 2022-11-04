using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWindowScript : MonoBehaviour
{
	public YandereScript Yandere;

    public bool ShowClothingMessage = false;
	public bool ShowCouncilMessage = false;
	public bool ShowTeacherMessage = false;
	public bool ShowLockerMessage = false;
	public bool ShowPoliceMessage = false;
	public bool ShowSanityMessage = false;
	public bool ShowSenpaiMessage = false;
	public bool ShowVisionMessage = false;
	public bool ShowWeaponMessage = false;
	public bool ShowBloodMessage = false;
	public bool ShowClassMessage = false;
    public bool ShowMoneyMessage = false;
    public bool ShowPhotoMessage = false;
    public bool ShowClubMessage = false;
	public bool ShowInfoMessage = false;
	public bool ShowPoolMessage = false;
	public bool ShowRepMessage = false;

	public bool IgnoreClothing = false;
	public bool IgnoreCouncil = false;
	public bool IgnoreTeacher = false;
	public bool IgnoreLocker = false;
	public bool IgnorePolice = false;
	public bool IgnoreSanity = false;
	public bool IgnoreSenpai = false;
	public bool IgnoreVision = false;
	public bool IgnoreWeapon = false;
	public bool IgnoreBlood = false;
	public bool IgnoreClass = false;
    public bool IgnoreMoney = false;
    public bool IgnorePhoto = false;
	public bool IgnoreClub = false;
	public bool IgnoreInfo = false;
	public bool IgnorePool = false;
	public bool IgnoreRep = false;

	public bool Hide = false;
	public bool Show = false;

	public UILabel TutorialLabel;
	public UILabel ShadowLabel;
	public UILabel TitleLabel;

	public UITexture TutorialImage;

	public string DisabledString;
	public Texture DisabledTexture;

	public string ClothingString;
	public Texture ClothingTexture;

	public string CouncilString;
	public Texture CouncilTexture;

	public string TeacherString;
	public Texture TeacherTexture;

	public string LockerString;
	public Texture LockerTexture;

	public string PoliceString;
	public Texture PoliceTexture;

	public string SanityString;
	public Texture SanityTexture;

	public string SenpaiString;
	public Texture SenpaiTexture;

	public string VisionString;
	public Texture VisionTexture;

	public string WeaponString;
	public Texture WeaponTexture;

	public string BloodString;
	public Texture BloodTexture;

	public string ClassString;
	public Texture ClassTexture;

    public string MoneyString;
    public Texture MoneyTexture;

    public string PhotoString;
	public Texture PhotoTexture;

	public string ClubString;
	public Texture ClubTexture;

	public string InfoString;
	public Texture InfoTexture;

	public string PoolString;
	public Texture PoolTexture;

	public string RepString;
	public Texture RepTexture;

	public string PointsString;

	public float Timer;

    public bool ForcingTutorial;
    public int ForceID;

    void Start ()
	{
		this.transform.localScale = new Vector3(0, 0, 0);	

		if (OptionGlobals.TutorialsOff)
		{
			enabled = false;
		}
		else
		{
			IgnoreClothing = TutorialGlobals.IgnoreClothing;
			IgnoreCouncil = TutorialGlobals.IgnoreCouncil;
			IgnoreTeacher = TutorialGlobals.IgnoreTeacher;
			IgnoreLocker = TutorialGlobals.IgnoreLocker;
			IgnorePolice = TutorialGlobals.IgnorePolice;
			IgnoreSanity = TutorialGlobals.IgnoreSanity;
			IgnoreSenpai = TutorialGlobals.IgnoreSenpai;
			IgnoreVision = TutorialGlobals.IgnoreVision;
			IgnoreWeapon = TutorialGlobals.IgnoreWeapon;
			IgnoreBlood = TutorialGlobals.IgnoreBlood;
			IgnoreClass = TutorialGlobals.IgnoreClass;
            IgnoreMoney = TutorialGlobals.IgnoreMoney;
            IgnorePhoto = TutorialGlobals.IgnorePhoto;
			IgnoreClub = TutorialGlobals.IgnoreClub;
			IgnoreInfo = TutorialGlobals.IgnoreInfo;
			IgnorePool = TutorialGlobals.IgnorePool;
			IgnoreRep = TutorialGlobals.IgnoreRep;
		}

		/*
		string path = "file:///" + Application.streamingAssetsPath +
				"/Portraits/Student_1.png";

		WWW www = new WWW(path);

		this.SenpaiTexture = www.texture;
		*/
	}

	void Update()
	{
		if (Show)
		{
			transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(
				1.2925f,
				1.2925f,
				1.2925f),
				Time.unscaledDeltaTime * 10);

			if (transform.localScale.x > 1)
			{
				if (Input.GetButtonDown(InputNames.Xbox_B))
				{
					OptionGlobals.TutorialsOff = true;

					TitleLabel.text = "Tutorials Disabled";
					TutorialLabel.text = DisabledString;
					TutorialLabel.text = TutorialLabel.text.Replace('@', '\n');
					TutorialImage.mainTexture = DisabledTexture;
					ShadowLabel.text = TutorialLabel.text;
				}
				else if (Input.GetButtonDown(InputNames.Xbox_A))
				{
                    if (this.ForcingTutorial)
                    {
                        ShowTutorial();
                    }

                    Yandere.RPGCamera.enabled = true;
					Yandere.Blur.enabled = false;
					Time.timeScale = 1;
					Show = false;
					Hide = true;
				}
			}
		}
		else if (Hide)
		{
			transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, 0, 0), Time.unscaledDeltaTime * 10);

			if (transform.localScale.x < .1f)
			{
				transform.localScale = new Vector3(0, 0, 0);
				Hide = false;

				if (OptionGlobals.TutorialsOff)
				{
					enabled = false;
				}
			}
		}

		if (this.ForcingTutorial || this.Yandere.CanMove && !this.Yandere.Egg && !this.Yandere.Aiming &&
			!this.Yandere.PauseScreen.Show && !this.Yandere.CinematicCamera.activeInHierarchy)
		{
            Timer += Time.deltaTime;

			if (Timer > 5)
			{
				if (ForcingTutorial || !IgnoreClothing)
				{
					if (ShowClothingMessage && !Show)
					{
                        if (!ForcingTutorial)
                        {
						    TutorialGlobals.IgnoreClothing = true;
						    IgnoreClothing = true;
                        }

                        TitleLabel.text = "No Spare Clothing";
						TutorialLabel.text = ClothingString;
						TutorialLabel.text = TutorialLabel.text.Replace('@', '\n');
						TutorialImage.mainTexture = ClothingTexture;

						SummonWindow();
					}
				}

				if (ForcingTutorial || !IgnoreCouncil)
				{
					if (ShowCouncilMessage && !Show)
					{
                        if (!ForcingTutorial)
                        {
                            TutorialGlobals.IgnoreCouncil = true;
						    IgnoreCouncil = true;
                        }

                        TitleLabel.text = "Student Council";
						TutorialLabel.text = CouncilString;
						TutorialLabel.text = TutorialLabel.text.Replace('@', '\n');
						TutorialImage.mainTexture = CouncilTexture;

						SummonWindow();
					}
				}

				if (ForcingTutorial || !IgnoreTeacher)
				{
					if (ShowTeacherMessage && !Show)
					{
                        if (!ForcingTutorial)
                        {
                            TutorialGlobals.IgnoreTeacher = true;
						    IgnoreTeacher = true;
                        }

                        TitleLabel.text = "Teachers";
						TutorialLabel.text = TeacherString;
						TutorialLabel.text = TutorialLabel.text.Replace('@', '\n');
						TutorialImage.mainTexture = TeacherTexture;

						SummonWindow();
					}
				}

				if (ForcingTutorial || !IgnoreLocker)
				{
					if (ShowLockerMessage && !Show)
					{
                        if (!ForcingTutorial)
                        {
                            TutorialGlobals.IgnoreLocker = true;
						    IgnoreLocker = true;
                        }

                        TitleLabel.text = "Notes In Lockers";
						TutorialLabel.text = LockerString;
						TutorialLabel.text = TutorialLabel.text.Replace('@', '\n');
						TutorialImage.mainTexture = LockerTexture;

						SummonWindow();
					}
				}

				if (ForcingTutorial || !IgnorePolice)
				{
					if (ShowPoliceMessage && !Show)
					{
                        if (!ForcingTutorial)
                        {
                            TutorialGlobals.IgnorePolice = true;
						    IgnorePolice = true;
                        }

                        TitleLabel.text = "Police";
						TutorialLabel.text = PoliceString;
						TutorialLabel.text = TutorialLabel.text.Replace('@', '\n');
						TutorialImage.mainTexture = PoliceTexture;

						SummonWindow();
					}
				}

				if (ForcingTutorial || !IgnoreSanity)
				{
					if (ShowSanityMessage && !Show)
					{
                        if (!ForcingTutorial)
                        {
                            TutorialGlobals.IgnoreSanity = true;
                            IgnoreSanity = true;
                        }

						TitleLabel.text = "Restoring Sanity";
						TutorialLabel.text = SanityString;
						TutorialLabel.text = TutorialLabel.text.Replace('@', '\n');
						TutorialImage.mainTexture = SanityTexture;

						SummonWindow();
					}
				}

				if (ForcingTutorial || !IgnoreSenpai)
				{
					if (ShowSenpaiMessage && !Show)
					{
                        if (!ForcingTutorial)
                        {
                            TutorialGlobals.IgnoreSenpai = true;
                            IgnoreSenpai = true;
                        }

						TitleLabel.text = "Your Senpai";
						TutorialLabel.text = SenpaiString;
						TutorialLabel.text = TutorialLabel.text.Replace('@', '\n');
						TutorialImage.mainTexture = SenpaiTexture;

						SummonWindow();
					}
				}

				if (ForcingTutorial || !IgnoreVision)
				{
					if (Yandere.StudentManager.WestBathroomArea.bounds.Contains(Yandere.transform.position) ||
						Yandere.StudentManager.EastBathroomArea.bounds.Contains(Yandere.transform.position))
					{
						ShowVisionMessage = true;
					}

					if (ShowVisionMessage && !Show)
					{
                        if (!ForcingTutorial)
                        {
                            TutorialGlobals.IgnoreVision = true;
                            IgnoreVision = true;
                        }

						TitleLabel.text = "Yandere Vision";
						TutorialLabel.text = VisionString;
						TutorialLabel.text = TutorialLabel.text.Replace('@', '\n');
						TutorialImage.mainTexture = VisionTexture;

						SummonWindow();
					}
				}

				if (ForcingTutorial || !IgnoreWeapon)
				{
					if (Yandere.Armed)
					{
						ShowWeaponMessage = true;
					}

					if (ShowWeaponMessage && !Show)
					{
                        if (!ForcingTutorial)
                        {
                            TutorialGlobals.IgnoreWeapon = true;
                            IgnoreWeapon = true;
                        }

						TitleLabel.text = "Weapons";
						TutorialLabel.text = WeaponString;
						TutorialLabel.text = TutorialLabel.text.Replace('@', '\n');
						TutorialImage.mainTexture = WeaponTexture;

						SummonWindow();
					}
				}

				if (ForcingTutorial || !IgnoreBlood)
				{
					if (ShowBloodMessage && !Show)
					{
                        if (!ForcingTutorial)
                        {
                            TutorialGlobals.IgnoreBlood = true;
                            IgnoreBlood = true;
                        }

						TitleLabel.text = "Bloody Clothing";
						TutorialLabel.text = BloodString;
						TutorialLabel.text = TutorialLabel.text.Replace('@', '\n');
						TutorialImage.mainTexture = BloodTexture;

						SummonWindow();
					}
				}

				if (ForcingTutorial || !IgnoreClass)
				{
					if (ShowClassMessage && !Show)
					{
                        if (!ForcingTutorial)
                        {
                            TutorialGlobals.IgnoreClass = true;
                            IgnoreClass = true;
                        }

						TitleLabel.text = "Attending Class";
						TutorialLabel.text = ClassString;
						TutorialLabel.text = TutorialLabel.text.Replace('@', '\n');
						TutorialImage.mainTexture = ClassTexture;

						SummonWindow();
					}
				}

                if (ForcingTutorial || !IgnoreMoney)
                {
                    if (ShowMoneyMessage && !Show)
                    {
                        if (!ForcingTutorial)
                        {
                            TutorialGlobals.IgnoreMoney = true;
                            IgnoreMoney = true;
                        }

                        TitleLabel.text = "Getting Money";
                        TutorialLabel.text = MoneyString;
                        TutorialLabel.text = TutorialLabel.text.Replace('@', '\n');
                        TutorialImage.mainTexture = MoneyTexture;

                        SummonWindow();
                    }
                }

                if (ForcingTutorial || !IgnorePhoto)
				{
					if (!ForcingTutorial && Yandere.transform.position.z > -50)
					{
						ShowPhotoMessage = true;
					}

					if (ShowPhotoMessage && !Show)
					{
                        if (!ForcingTutorial)
                        {
                            TutorialGlobals.IgnorePhoto = true;
                            IgnorePhoto = true;
                        }

						TitleLabel.text = "Taking Photographs";
						TutorialLabel.text = PhotoString;
						TutorialLabel.text = TutorialLabel.text.Replace('@', '\n');
						TutorialImage.mainTexture = PhotoTexture;

						SummonWindow();
					}
				}

				if (ForcingTutorial || !IgnoreClub)
				{
					if (ShowClubMessage && !Show)
					{
                        if (!ForcingTutorial)
                        {
                            TutorialGlobals.IgnoreClub = true;
                            IgnoreClub = true;
                        }

						TitleLabel.text = "Joining Clubs";
						TutorialLabel.text = ClubString;
						TutorialLabel.text = TutorialLabel.text.Replace('@', '\n');
						TutorialImage.mainTexture = ClubTexture;

						SummonWindow();
					}
				}

				if (ForcingTutorial || !IgnoreInfo)
				{
					if (ShowInfoMessage && !Show)
					{
                        if (!ForcingTutorial)
                        {
                            TutorialGlobals.IgnoreInfo = true;
                            IgnoreInfo = true;
                        }

						TitleLabel.text = "Info-chan's Services";
						TutorialLabel.text = InfoString;
						TutorialLabel.text = TutorialLabel.text.Replace('@', '\n');
						TutorialImage.mainTexture = InfoTexture;

						SummonWindow();
					}
				}

				if (ForcingTutorial || !IgnorePool)
				{
					if (ShowPoolMessage && !Show)
					{
                        if (!ForcingTutorial)
                        {
                            TutorialGlobals.IgnorePool = true;
                            IgnorePool = true;
                        }

						TitleLabel.text = "Cleaning Blood";
						TutorialLabel.text = PoolString;
						TutorialLabel.text = TutorialLabel.text.Replace('@', '\n');
						TutorialImage.mainTexture = PoolTexture;

						SummonWindow();
					}
				}

				if (ForcingTutorial || !IgnoreRep)
				{
					if (ShowRepMessage && !Show)
					{
                        if (!ForcingTutorial)
                        {
                            TutorialGlobals.IgnoreRep = true;
                            IgnoreRep = true;
                        }

						TitleLabel.text = "Reputation";
						TutorialLabel.text = RepString;
						TutorialLabel.text = TutorialLabel.text.Replace('@', '\n');
						TutorialImage.mainTexture = RepTexture;

						SummonWindow();
					}
				}
            }
		}
	}

	public void SummonWindow()
	{
		Debug.Log ("Summoning tutorial window.");

		ShadowLabel.text = TutorialLabel.text;
		Yandere.RPGCamera.enabled = false;
		Yandere.Blur.enabled = true;
		Time.timeScale = 0;
		Show = true;
		Timer = 0;
	}

    public void ShowTutorial()
    {
        if (!ForcingTutorial)
        {
            ForcingTutorial = true;
            Timer = 6;
        }
        else
        {
            ForcingTutorial = false;
            Timer = 0;
        }

        switch (ForceID)
        {
            case 1:
                ShowClothingMessage = ForcingTutorial;
                IgnoreClothing = false;
                break;

            case 2:
                ShowCouncilMessage = ForcingTutorial;
                IgnoreCouncil = false;
                break;

            case 3:
                ShowTeacherMessage = ForcingTutorial;
                IgnoreTeacher = false;
                break;

            case 4:
                ShowLockerMessage = ForcingTutorial;
                IgnoreLocker = false;
                break;

            case 5:
                ShowPoliceMessage = ForcingTutorial;
                IgnorePolice = false;
                break;

            case 6:
                ShowSenpaiMessage = ForcingTutorial;
                IgnoreSenpai = false;
                break;

            case 7:
                ShowVisionMessage = ForcingTutorial;
                IgnoreVision = false;
                break;

            case 8:
                ShowWeaponMessage = ForcingTutorial;
                IgnoreWeapon = false;
                break;

            case 9:
                ShowSanityMessage = ForcingTutorial;
                IgnoreSanity = false;
                break;

            case 10:
                ShowBloodMessage = ForcingTutorial;
                IgnoreBlood = false;
                break;

            case 11:
                ShowClassMessage = ForcingTutorial;
                IgnoreClass = false;
                break;

            case 12:
                ShowPhotoMessage = ForcingTutorial;
                IgnorePhoto = false;
                break;

            case 13:
                ShowClubMessage = ForcingTutorial;
                IgnoreClub = false;
                break;

            case 14:
                ShowInfoMessage = ForcingTutorial;
                IgnoreInfo = false;
                break;

            case 15:
                ShowPoolMessage = ForcingTutorial;
                IgnorePool = false;
                break;

            case 16:
                ShowRepMessage = ForcingTutorial;
                IgnoreRep = false;
                break;

            case 17:
                ShowMoneyMessage = ForcingTutorial;
                IgnoreMoney = false;
                break;
        }

        Update();
    }
}