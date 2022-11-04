using UnityEngine;

public class TaskWindowScript : MonoBehaviour
{
	public DialogueWheelScript DialogueWheel;
	public SewingMachineScript SewingMachine;
	public CheckOutBookScript CheckOutBook;
	public TaskManagerScript TaskManager;
	public PromptBarScript PromptBar;
	public UILabel TaskDescLabel;
	public YandereScript Yandere;

	public UITexture Portrait;
	public UITexture Icon;

	public GameObject[] TaskCompleteLetters;
	public string[] Descriptions;
	public Texture[] Portraits;
	public Texture[] Icons;

	public bool TaskComplete = false;
	public bool Generic = false;

	public GameObject Window;

	public int StudentID = 0;
	public int ID = 0;

	public float TrueTimer = 0.0f;
	public float Timer = 0.0f;

	void Start()
	{
		this.Window.SetActive(false);
		this.UpdateTaskObjects(30);
	}

	public void UpdateWindow(int ID)
	{
		this.PromptBar.ClearButtons();
		this.PromptBar.Label[0].text = "Accept";
		this.PromptBar.Label[1].text = "Refuse";
		this.PromptBar.UpdateButtons();
		this.PromptBar.Show = true;

		this.GetPortrait(ID);
		this.StudentID = ID;

		GenericCheck();

		if (Generic)
		{
			ID = 0;
			this.Generic = false;
		}

		this.TaskDescLabel.transform.parent.gameObject.SetActive(true);
		this.TaskDescLabel.text = this.Descriptions[ID];
		this.Icon.mainTexture = this.Icons[ID];
		this.Window.SetActive(true);

		Time.timeScale = 0.0001f;
	}

	void Update()
	{
		if (this.Window.activeInHierarchy)
		{
			if (Input.GetButtonDown(InputNames.Xbox_A))
			{
				TaskGlobals.SetTaskStatus(this.StudentID, 1);
				this.Yandere.TargetStudent.TalkTimer = 100.0f;
				this.Yandere.TargetStudent.Interaction = StudentInteractionType.GivingTask;
				this.Yandere.TargetStudent.TaskPhase = 4;
				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
				this.Window.SetActive(false);

				this.UpdateTaskObjects(this.StudentID);

				Time.timeScale = 1;
			}
			else if (Input.GetButtonDown(InputNames.Xbox_B))
			{
				this.Yandere.TargetStudent.TalkTimer = 100.0f;
				this.Yandere.TargetStudent.Interaction = StudentInteractionType.GivingTask;
				this.Yandere.TargetStudent.TaskPhase = 0;
				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
				this.Window.SetActive(false);

				Time.timeScale = 1;
			}
		}

		if (this.TaskComplete)
		{
			if (this.TrueTimer == 0.0f)
			{
				this.GetComponent<AudioSource>().Play();
			}

			this.TrueTimer += Time.deltaTime;
			this.Timer += Time.deltaTime;

			if (this.ID < this.TaskCompleteLetters.Length)
			{
				if (this.Timer > 0.050f)
				{
					this.TaskCompleteLetters[this.ID].SetActive(true);
					this.Timer = 0.0f;
					this.ID++;
				}
			}

			if (this.TaskCompleteLetters[12].transform.localPosition.y < -725.0f)
			{
				// [af] Converted while loop to for loop.
				for (this.ID = 0; this.ID < this.TaskCompleteLetters.Length; this.ID++)
				{
					this.TaskCompleteLetters[this.ID].GetComponent<GrowShrinkScript>().Return();
				}

				this.TaskCheck();

				this.DialogueWheel.End();
				this.TaskComplete = false;
				this.TrueTimer = 0.0f;
				this.Timer = 0.0f;
				this.ID = 0;
			}
		}
	}

	void TaskCheck()
	{
		//Ryuto
		if (this.Yandere.TargetStudent.StudentID == 37)
		{
			this.DialogueWheel.Yandere.TargetStudent.Cosmetic.MaleAccessories[1].SetActive(true);
		}

		GenericCheck();

		if (Generic)
		{
			this.Yandere.Inventory.Book = false;
			this.CheckOutBook.UpdatePrompt();
			this.Generic = false;
		}
	}

	void GetPortrait(int ID)
	{
		string path = "file:///" + Application.streamingAssetsPath + "/Portraits/Student_" + ID.ToString() + ".png";

		WWW www = new WWW(path);

		this.Portrait.mainTexture = www.texture;
	}

	void UpdateTaskObjects(int StudentID)
	{
		//Kokona
		if (this.StudentID == 30)
		{
			this.SewingMachine.Check = true;
		}
	}

	public void GenericCheck()
	{
		Generic = false;

		if (this.Yandere.TargetStudent.StudentID != 8 &&
			this.Yandere.TargetStudent.StudentID != 11 &&
			this.Yandere.TargetStudent.StudentID != 25 &&
			this.Yandere.TargetStudent.StudentID != 28 &&
			this.Yandere.TargetStudent.StudentID != 30 &&
			this.Yandere.TargetStudent.StudentID != 36 &&
			this.Yandere.TargetStudent.StudentID != 37 &&
			this.Yandere.TargetStudent.StudentID != 38 &&
			this.Yandere.TargetStudent.StudentID != 52 &&
			this.Yandere.TargetStudent.StudentID != 76 &&
			this.Yandere.TargetStudent.StudentID != 77 &&
			this.Yandere.TargetStudent.StudentID != 78 &&
			this.Yandere.TargetStudent.StudentID != 79 &&
			this.Yandere.TargetStudent.StudentID != 80 &&
			this.Yandere.TargetStudent.StudentID != 81)
		{
			Generic = true;
		}

		//Anti-Osana Code
		#if UNITY_EDITOR
		if (this.Yandere.TargetStudent.StudentID == 6)
		{
			Generic = false;
		}
		#endif
	}


	public void AltGenericCheck(int TempID)
	{
		Generic = false;

		if (TempID != 8  &&
			TempID != 11 &&
			TempID != 25 &&
			TempID != 28 &&
			TempID != 30 &&
			TempID != 36 &&
			TempID != 37 &&
			TempID != 38 &&
			TempID != 52 &&
			TempID != 76 &&
			TempID != 77 &&
			TempID != 78 &&
			TempID != 79 &&
			TempID != 80 &&
			TempID != 81)
		{
			Generic = true;
		}

		//Anti-Osana Code
		#if UNITY_EDITOR
		if (TempID == 6)
		{
			Generic = false;
		}
		#endif
	}
}