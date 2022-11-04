using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemorialSceneScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;

	public GameObject[] Canvases;

	public UITexture[] Portraits;

	public GameObject CanvasGroup;
	public GameObject Headmaster;
	public GameObject Counselor;

	public int MemorialStudents;
	public float Speed;

	void Start()
	{
        //Debug.Log("We need to hold a memorial for " + StudentGlobals.MemorialStudents + " students.");

        if (PlayerPrefs.GetInt("LoadingSave") == 1)
        {
            int Profile = GameGlobals.Profile;
            int Slot = PlayerPrefs.GetInt("SaveSlot");

            StudentGlobals.MemorialStudents = PlayerPrefs.GetInt("Profile_" + Profile + "_Slot_" + Slot + "_MemorialStudents");
        }

        MemorialStudents = StudentGlobals.MemorialStudents;

		if (MemorialStudents%2==0)
		{
			CanvasGroup.transform.localPosition = new Vector3(-.5f, 0, -2);
		}

		int PortraitID = 0;
		int ID = 1;

		while (ID < 10)
		{
			Canvases[ID].SetActive(false);
			ID++;
		}

		ID = 0;

		while (MemorialStudents > 0)
		{
			ID++;
			Canvases[ID].SetActive(true);

				 if (MemorialStudents == 1){PortraitID = StudentGlobals.MemorialStudent1;}// Debug.Log("Getting portrait for Student " + StudentGlobals.MemorialStudent1);}
			else if (MemorialStudents == 2){PortraitID = StudentGlobals.MemorialStudent2;}// Debug.Log("Getting portrait for Student " + StudentGlobals.MemorialStudent2);}
			else if (MemorialStudents == 3){PortraitID = StudentGlobals.MemorialStudent3;}// Debug.Log("Getting portrait for Student " + StudentGlobals.MemorialStudent3);}
			else if (MemorialStudents == 4){PortraitID = StudentGlobals.MemorialStudent4;}// Debug.Log("Getting portrait for Student " + StudentGlobals.MemorialStudent4);}
			else if (MemorialStudents == 5){PortraitID = StudentGlobals.MemorialStudent5;}// Debug.Log("Getting portrait for Student " + StudentGlobals.MemorialStudent5);}
			else if (MemorialStudents == 6){PortraitID = StudentGlobals.MemorialStudent6;}// Debug.Log("Getting portrait for Student " + StudentGlobals.MemorialStudent6);}
			else if (MemorialStudents == 7){PortraitID = StudentGlobals.MemorialStudent7;}// Debug.Log("Getting portrait for Student " + StudentGlobals.MemorialStudent7);}
			else if (MemorialStudents == 8){PortraitID = StudentGlobals.MemorialStudent8;}// Debug.Log("Getting portrait for Student " + StudentGlobals.MemorialStudent8);}
			else if (MemorialStudents == 9){PortraitID = StudentGlobals.MemorialStudent9;}// Debug.Log("Getting portrait for Student " + StudentGlobals.MemorialStudent9);}

			string path = "file:///" + Application.streamingAssetsPath +
				"/Portraits/Student_" + PortraitID.ToString() + ".png";

			WWW www = new WWW(path);

			Portraits[ID].mainTexture = www.texture;

			MemorialStudents--;
		}
	}

	public bool Eulogized;
	public bool FadeOut;

	void Update()
	{
		Speed += Time.deltaTime;

		if (Speed > 1)
		{
			if (!Eulogized)
			{
				StudentManager.Yandere.Subtitle.UpdateLabel(SubtitleType.Eulogy, 0, 8.0f);

				StudentManager.Yandere.PromptBar.Label[0].text = "Continue";
				StudentManager.Yandere.PromptBar.UpdateButtons();
				StudentManager.Yandere.PromptBar.Show = true;

				Eulogized = true;
			}

			StudentManager.MainCamera.position = Vector3.Lerp(
					StudentManager.MainCamera.position,
					new Vector3(38, 4.125f, 68.825f),
					(Speed -1) * Time.deltaTime * .15f);

			if (Input.GetButtonDown(InputNames.Xbox_A))
			{
				StudentManager.Yandere.PromptBar.Show = false;
				FadeOut = true;
			}
		}

		if (FadeOut)
		{
			StudentManager.Clock.BloomEffect.bloomIntensity += Time.deltaTime * 10;

			if (StudentManager.Clock.BloomEffect.bloomIntensity > 10)
			{
				StudentManager.Yandere.Casual = !StudentManager.Yandere.Casual;
				StudentManager.Yandere.ChangeSchoolwear();

				StudentManager.Yandere.transform.position = new Vector3(12, 0, 72);
				StudentManager.Yandere.transform.eulerAngles = new Vector3(0, -90, 0);
				StudentManager.Yandere.HeartCamera.enabled = true;
				StudentManager.Yandere.RPGCamera.enabled = true;
				StudentManager.Yandere.CanMove = true;
				StudentManager.Yandere.HUD.alpha = 1;

				StudentManager.Clock.UpdateBloom = true;
				StudentManager.Clock.StopTime = false;
				StudentManager.Clock.PresentTime = 7.5f * 60.0f;
				StudentManager.Clock.HourTime = 7.5f;

				StudentManager.Unstop();
				StudentManager.SkipTo8();

				Headmaster.SetActive(false);
				Counselor.SetActive(false);

				enabled = false;
			}
		}
	}
}