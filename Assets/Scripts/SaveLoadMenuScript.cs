using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SaveLoadMenuScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public InputManagerScript InputManager;
	public PauseScreenScript PauseScreen;
	public GameObject ConfirmWindow;
    public GameObject WarningWindow;
    public ClockScript Clock;

    public Texture DefaultThumbnail;

    public UILabel AreYouSureLabel;
	public UILabel Header;

    public UITexture[] Thumbnails;
	public UILabel[] DataLabels;

	public Transform Highlight;
	public Camera UICamera;

	public bool GrabScreenshot;
	public bool Loading;
	public bool Saving;

	public int Profile;

	public void Start()
	{
		if (GameGlobals.Profile == 0)
		{
			GameGlobals.Profile = 1;
		}

		Profile = GameGlobals.Profile;

        WarningWindow.SetActive(true);
        ConfirmWindow.SetActive(false);
        StartCoroutine(GetThumbnails());
    }

	public void Update()
	{
		if (!ConfirmWindow.activeInHierarchy)
		{
			if (InputManager.TappedUp)
			{
				Row--;
				UpdateHighlight();
			}
			else if (InputManager.TappedDown)
			{
				Row++;
				UpdateHighlight();
			}

			if (InputManager.TappedLeft)
			{
				Column--;
				UpdateHighlight();
			}
			else if (InputManager.TappedRight)
			{
				Column++;
				UpdateHighlight();
			}
		}

		if (GrabScreenshot)
		{
			if (GameGlobals.Profile == 0)
			{
				GameGlobals.Profile = 1;
				Profile = 1;
			}

			//Debug.Log("Saving data to " + "Profile_" + Profile + "_Slot_" + Selected + ".");

			PauseScreen.ScreenBlur.enabled = true;
			UICamera.enabled = true;

			StudentManager.Save();
			StartCoroutine(GetThumbnails());

			//PlayerPrefs.SetFloat("Profile_" + Profile + "_Slot_" + Selected + "_Time", Clock.PresentTime);

			//Debug.Log("Saving time! Profile_" + Profile + "_Slot_" + Selected + "_Time" + " is " + PlayerPrefs.GetFloat("Profile_" + Profile + "_Slot_" + Selected + "_Time"));

			     if (DateGlobals.Weekday == DayOfWeek.Monday)    {PlayerPrefs.SetInt("Profile_" + Profile + "_Slot_" + Selected + "_Weekday", 1);}
			else if (DateGlobals.Weekday == DayOfWeek.Tuesday)   {PlayerPrefs.SetInt("Profile_" + Profile + "_Slot_" + Selected + "_Weekday", 2);}
			else if (DateGlobals.Weekday == DayOfWeek.Wednesday) {PlayerPrefs.SetInt("Profile_" + Profile + "_Slot_" + Selected + "_Weekday", 3);}
			else if (DateGlobals.Weekday == DayOfWeek.Thursday)  {PlayerPrefs.SetInt("Profile_" + Profile + "_Slot_" + Selected + "_Weekday", 4);}
			else if (DateGlobals.Weekday == DayOfWeek.Friday)    {PlayerPrefs.SetInt("Profile_" + Profile + "_Slot_" + Selected + "_Weekday", 5);}

			GrabScreenshot = false;
		}

        if (WarningWindow.activeInHierarchy)
        {
            if (Input.GetButtonDown("A"))
            {
                WarningWindow.SetActive(false);
            }
            else if (Input.GetButtonDown("B"))
            {
                PauseScreen.MainMenu.SetActive(true);
                PauseScreen.Sideways = false;
                PauseScreen.PressedB = true;
                gameObject.SetActive(false);

                PauseScreen.PromptBar.ClearButtons();
                PauseScreen.PromptBar.Label[0].text = "Accept";
                PauseScreen.PromptBar.Label[1].text = "Exit";
                PauseScreen.PromptBar.Label[4].text = "Choose";
                PauseScreen.PromptBar.UpdateButtons();
                PauseScreen.PromptBar.Show = true;
            }
        }
        else
        {
		    if (Input.GetButtonDown("A"))
		    {
			    if (Loading)
			    {
				    if (DataLabels[Selected].text != "No Data")
				    {
					    if (!ConfirmWindow.activeInHierarchy)
					    {
						    AreYouSureLabel.text = "Are you sure you'd like to load?";
						    ConfirmWindow.SetActive(true);
					    }
					    else
					    {
						    if (DataLabels[Selected].text != "No Data")
						    {
							    PlayerPrefs.SetInt("LoadingSave", 1);
							    PlayerPrefs.SetInt("SaveSlot", Selected);

                                //Debug.Log("BEFORE LOADING PlayerPrefs.GetInt(''Profile_1_StudentDead_4'') is: " + PlayerPrefs.GetInt("Profile_1_StudentDead_4"));
                                //Debug.Log("BEFORE LOADING StudentGlobals.GetStudentDead(4) is: " + StudentGlobals.GetStudentDead(4));

                                YanSave.LoadPrefs("Profile_" + GameGlobals.Profile + "_Slot_" + Selected);

                                //Debug.Log("AFTER LOADING PlayerPrefs.GetInt(''Profile_1_StudentDead_4'') is: " + PlayerPrefs.GetInt("Profile_1_StudentDead_4"));
                                //Debug.Log("AFTER LOADING StudentGlobals.GetStudentDead(4) is: " + StudentGlobals.GetStudentDead(4));

                                SceneManager.LoadScene(SceneNames.LoadingScene);
						    }
					    }
				    }
			    }
			    else if (Saving)
			    {
				    if (!ConfirmWindow.activeInHierarchy)
				    {
					    AreYouSureLabel.text = "Are you sure you'd like to save?";
					    ConfirmWindow.SetActive(true);
				    }
				    else 
				    {
					    ConfirmWindow.SetActive(false);

					    PlayerPrefs.SetInt("SaveSlot", Selected);
                        GameGlobals.MostRecentSlot = Selected;

                        PlayerPrefs.SetString("Profile_" + Profile + "_Slot_" + Selected + "_DateTime", System.DateTime.Now.ToString());

					    ScreenCapture.CaptureScreenshot (Application.streamingAssetsPath + "/SaveData/Profile_" + Profile + "/Slot_" + Selected + "_Thumbnail.png");

					    PauseScreen.ScreenBlur.enabled = false;
					    UICamera.enabled = false;

					    GrabScreenshot = true;
				    }
			    }
		    }

		    if (Input.GetButtonDown("X"))
		    {
                if (Loading)
                {
                    if (DataLabels[Selected].text != "No Data")
			        {
				        PlayerPrefs.SetInt("SaveSlot", Selected);
				        StudentManager.Load();
                        Physics.SyncTransforms();

                        if (PlayerPrefs.GetInt("Profile_" + Profile + "_Slot_" + Selected + "_Weekday") == 1){DateGlobals.Weekday = DayOfWeek.Monday;}
				        else if (PlayerPrefs.GetInt("Profile_" + Profile + "_Slot_" + Selected + "_Weekday") == 2){DateGlobals.Weekday = DayOfWeek.Tuesday;}
				        else if (PlayerPrefs.GetInt("Profile_" + Profile + "_Slot_" + Selected + "_Weekday") == 3){DateGlobals.Weekday = DayOfWeek.Wednesday;}
				        else if (PlayerPrefs.GetInt("Profile_" + Profile + "_Slot_" + Selected + "_Weekday") == 4){DateGlobals.Weekday = DayOfWeek.Tuesday;}
				        else if (PlayerPrefs.GetInt("Profile_" + Profile + "_Slot_" + Selected + "_Weekday") == 5){DateGlobals.Weekday = DayOfWeek.Wednesday;}

				        //Clock.PresentTime = PlayerPrefs.GetFloat("Profile_" + Profile + "_Slot_" + Selected + "_Time", Clock.PresentTime);
				        Clock.DayLabel.text = Clock.GetWeekdayText(DateGlobals.Weekday);

				        PauseScreen.MainMenu.SetActive(true);
				        PauseScreen.Sideways = false;
				        PauseScreen.PressedB = true;
				        gameObject.SetActive(false);
				        PauseScreen.ExitPhone();
			        }
                }
                else if (Saving)
                {
                    if (PlayerPrefs.GetString("Profile_" + Profile + "_Slot_" + Selected + "_DateTime") != "")
                    {
                        File.Delete(Application.streamingAssetsPath + "/SaveData/Profile_" + Profile + "/Slot_" + Selected + "_Thumbnail.png");
                        PlayerPrefs.SetString("Profile_" + Profile + "_Slot_" + Selected + "_DateTime", "");

                        Thumbnails[Selected].mainTexture = DefaultThumbnail;
                        DataLabels[Selected].text = "No Data";
                    }
                }
            }

		    if (Input.GetButtonDown("B"))
		    {
			    if (ConfirmWindow.activeInHierarchy)
			    {
				    ConfirmWindow.SetActive(false);
			    }
			    else
			    {
				    PauseScreen.MainMenu.SetActive(true);
				    PauseScreen.Sideways = false;
				    PauseScreen.PressedB = true;
				    gameObject.SetActive(false);

				    PauseScreen.PromptBar.ClearButtons();
				    PauseScreen.PromptBar.Label[0].text = "Accept";
				    PauseScreen.PromptBar.Label[1].text = "Exit";
				    PauseScreen.PromptBar.Label[4].text = "Choose";
				    PauseScreen.PromptBar.UpdateButtons();
				    PauseScreen.PromptBar.Show = true;
			    }
		    }
        }
    }

	public IEnumerator GetThumbnails()
	{
		//Debug.Log ("We were told to get save file thumbnails.");

		for (int ID = 1; ID < 11; ID++)
		{
			if (PlayerPrefs.GetString("Profile_" + Profile + "_Slot_" + ID + "_DateTime") != "")
			{
				DataLabels[ID].text = PlayerPrefs.GetString("Profile_" + Profile + "_Slot_" + ID + "_DateTime");

				string path = "file:///" + Application.streamingAssetsPath + "/SaveData/Profile_" + Profile + "/Slot_" + ID + "_Thumbnail.png";

				WWW www = new WWW(path);

				yield return www;

				if (www.error == null)
				{
					Thumbnails[ID].mainTexture = www.texture;
				}
				else
				{
					Debug.Log("Could not retrieve the thumbnail. Maybe it was deleted from Streaming Assets?");
				}
			}
			else
			{
				DataLabels[ID].text = "No Data";
			}
		}
	}

	public int Row = 1;
	public int Column = 1;
	public int Selected = 1;

	public void UpdateHighlight()
	{
		if (Row < 1)
		{
			Row = 2;
		}
		else if (Row > 2)
		{
			Row = 1;
		}

		if (Column < 1)
		{
			Column = 5;
		}
		else if (Column > 5)
		{
			Column = 1;
		}

		Highlight.localPosition = new Vector3(
			-510 + (170 * Column),
			313 - (226 * Row),
			Highlight.localPosition.z);

		Selected = Column + ((Row - 1) * 5);
	}
}