using UnityEngine;

public class TaskManagerScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public YandereScript Yandere;

	public GameObject[] TaskObjects;
	public PromptScript[] Prompts;

	public bool[] GirlsQuestioned;

	void Start()
	{
		this.UpdateTaskStatus();
	}

	public void CheckTaskPickups()
	{
		Debug.Log("Checking Tasks that are completed by picking something up!");

		//Osana's Task
		if (TaskGlobals.GetTaskStatus(11) == 1)
		{
			if (this.Prompts[11].Circle[3] != null)
			{
				if (this.Prompts[11].Circle[3].fillAmount == 0.0f)
				{
					if (this.StudentManager.Students[11] != null)
					{
						this.StudentManager.Students[11].TaskPhase = 5;
					}

					ConversationGlobals.SetTopicDiscovered(15, true);
					this.Yandere.NotificationManager.TopicName = "Cats";
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);

					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
					ConversationGlobals.SetTopicLearnedByStudent(15, 11, true);

					TaskGlobals.SetTaskStatus(11, 2);
					Destroy(this.TaskObjects[11]);
				}
			}
		}

		//Saki's Task
		if (TaskGlobals.GetTaskStatus(25) == 1)
		{
			if (this.Prompts[25].Circle[3].fillAmount == 0.0f)
			{
				if (this.StudentManager.Students[25] != null)
				{
					this.StudentManager.Students[25].TaskPhase = 5;
				}

				TaskGlobals.SetTaskStatus(25, 2);
				Destroy(this.TaskObjects[25]);
			}
		}

		//Ryuto's Task
		if (TaskGlobals.GetTaskStatus(37) == 1)
		{
			if (this.Prompts[37].Circle[3] != null)
			{
				if (this.Prompts[37].Circle[3].fillAmount == 0.0f)
				{
					if (this.StudentManager.Students[37] != null)
					{
						this.StudentManager.Students[37].TaskPhase = 5;
					}

					TaskGlobals.SetTaskStatus(37, 2);
					Destroy(this.TaskObjects[37]);
				}
			}
		}
	}

	public void UpdateTaskStatus()
	{
		////////////////////////////////////
		///// Task 8 - Hazu Kashibuchi /////
		////////////////////////////////////

		if (TaskGlobals.GetTaskStatus(8) == 1)
		{
			if (this.StudentManager.Students[8] != null)
			{
				if (this.StudentManager.Students[8].TaskPhase == 0)
				{
					this.StudentManager.Students[8].TaskPhase = 4;
				}

				if (this.Yandere.Inventory.Soda)
				{
					this.StudentManager.Students[8].TaskPhase = 5;
				}
			}
		}

		//////////////////////////////////
		///// Task 11 - Osana Najimi /////
		//////////////////////////////////

		if (TaskGlobals.GetTaskStatus(11) == 1)
		{
			if (this.StudentManager.Students[11] != null)
			{
				if (this.StudentManager.Students[11].TaskPhase == 0)
				{
					this.StudentManager.Students[11].TaskPhase = 4;
				}

				this.TaskObjects[11].SetActive(true);
			}
		}
		else
		{
			if (this.TaskObjects[11] != null)
			{
				this.TaskObjects[11].SetActive(false);
			}
		}

		///////////////////////////////
		///// Task 25 - Saki Miyu /////
		///////////////////////////////

		if (TaskGlobals.GetTaskStatus(25) == 1)
		{
			if (this.StudentManager.Students[25] != null)
			{
				if (this.StudentManager.Students[25].TaskPhase == 0)
				{
					this.StudentManager.Students[25].TaskPhase = 4;
				}

				this.TaskObjects[25].SetActive(true);
			}
		}
		else
		{
			if (this.TaskObjects[25] != null)
			{
				this.TaskObjects[25].SetActive(false);
			}
		}

		///////////////////////////////
		///// Task 28 - Riku Soma /////
		///////////////////////////////

		if (TaskGlobals.GetTaskStatus(28) == 1)
		{
			if (this.StudentManager.Students[28] != null)
			{
				if (this.StudentManager.Students[28].TaskPhase == 0)
				{
					this.StudentManager.Students[28].TaskPhase = 4;
				}

				// [af] Converted while loop to for loop.
				for (int ID = 1; ID < 26; ID++)
				{
					if (TaskGlobals.GetKittenPhoto(ID))
					{
						Debug.Log("Riku's Task can be turned in.");

						this.StudentManager.Students[28].TaskPhase = 5;
					}
				}
			}
		}

		///////////////////////////////////
		///// Task 30 - Kokona Haruka /////
		///////////////////////////////////

		if (TaskGlobals.GetTaskStatus(30) == 1)
		{
			if (this.StudentManager.Students[30] != null)
			{
				if (this.StudentManager.Students[30].TaskPhase == 0)
				{
					this.StudentManager.Students[30].TaskPhase = 4;
				}
			}
		}

		///////////////////////////////
		///// Task 36 - Gema Taku /////
		///////////////////////////////

		//Debug.Log("Gema's task status is: " + TaskGlobals.GetTaskStatus(36));

		if (TaskGlobals.GetTaskStatus(36) == 1)
		{
			if (this.StudentManager.Students[36] != null)
			{
				if (this.StudentManager.Students[36].TaskPhase == 0)
				{
					this.StudentManager.Students[36].TaskPhase = 4;
				}

				if (this.GirlsQuestioned[1] && this.GirlsQuestioned[2] &&
					this.GirlsQuestioned[3] && this.GirlsQuestioned[4] &&
					this.GirlsQuestioned[5])
				{
					Debug.Log("Gema's task should be ready to turn in!");

					this.StudentManager.Students[36].TaskPhase = 5;
				}
			}
		}

		///////////////////////////////////
		///// Task 37 - Ryuto Ippongo /////
		///////////////////////////////////

		if (TaskGlobals.GetTaskStatus(37) == 1)
		{
			if (this.StudentManager.Students[37] != null)
			{
				if (this.StudentManager.Students[37].TaskPhase == 0)
				{
					this.StudentManager.Students[37].TaskPhase = 4;
				}

				this.TaskObjects[37].SetActive(true);
			}
		}
		else
		{
			if (this.TaskObjects[37] != null)
			{
				this.TaskObjects[37].SetActive(false);
			}
		}

		///////////////////////////////
		///// Task 38 - Pippi Osu /////
		///////////////////////////////

		if (TaskGlobals.GetTaskStatus(38) == 1)
		{
			if (this.StudentManager.Students[38] != null)
			{
				if (this.StudentManager.Students[38].TaskPhase == 0)
				{
					this.StudentManager.Students[38].TaskPhase = 4;
				}
			}
		}
		else if (TaskGlobals.GetTaskStatus(38) == 2)
		{
			if (this.StudentManager.Students[38] != null)
			{
				this.StudentManager.Students[38].TaskPhase = 5;
			}
		}

		///////////////////////////////////
		///// Task 52 - Gita Yahamato /////
		///////////////////////////////////

		if (ClubGlobals.GetClubClosed(ClubType.LightMusic) ||
			this.StudentManager.Students[51] == null)
		{
			if (this.StudentManager.Students[52] != null)
			{
				this.StudentManager.Students[52].TaskPhase = 100;
			}

			TaskGlobals.SetTaskStatus(52, 100);
		}
		else if (TaskGlobals.GetTaskStatus(52) == 1)
		{
			if (this.StudentManager.Students[52] != null)
			{
				this.StudentManager.Students[52].TaskPhase = 4;

				// [af] Converted while loop to for loop.
				for (int ID = 1; ID < 52; ID++)
				{
					if (TaskGlobals.GetGuitarPhoto(ID))
					{
						this.StudentManager.Students[52].TaskPhase = 5;
					}
				}
			}
		}

		/////////////////////////////////////
		///// Task 81 - Musume Ronshaku /////
		/////////////////////////////////////

		if (TaskGlobals.GetTaskStatus(81) == 1)
		{
			if (this.StudentManager.Students[81] != null)
			{
				if (this.StudentManager.Students[81].TaskPhase == 0)
				{
					this.StudentManager.Students[81].TaskPhase = 4;
				}

				// [af] Converted while loop to for loop.
				for (int ID = 1; ID < 26; ID++)
				{
					if (TaskGlobals.GetHorudaPhoto(ID))
					{
						Debug.Log("Musume's Task can be turned in.");

						this.StudentManager.Students[81].TaskPhase = 5;
					}
				}
			}
		}

		/////////////////////////////////////
		///// Task 81 - Musume Ronshaku /////
		/////////////////////////////////////

		if (TaskGlobals.GetTaskStatus(81) == 3)
		{
			//No code needs to be here, theoretically.
			//All the code can go into Musume's "Wait" protocol.
		}
	}
}