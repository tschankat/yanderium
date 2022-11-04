using System.Collections;
using UnityEngine;

public class TaskListScript : MonoBehaviour
{
    public TutorialWindowScript TutorialWindow;
    public InputManagerScript InputManager;
	public PauseScreenScript PauseScreen;
	public TaskWindowScript TaskWindow;
    public JsonScript JSON;

	public GameObject MainMenu;

	public UITexture StudentIcon;
	public UITexture TaskIcon;
	public UILabel TaskDesc;

	public Texture QuestionMark;
	public Transform Highlight;
	public Texture Silhouette;

	public UILabel[] TaskNameLabels;
	public UISprite[] Checkmarks;

    public Texture[] TutorialTextures;
    public string[] TutorialDescs;
    public string[] TutorialNames;

    public int ListPosition = 0;
    public int Limit = 84;
	public int ID = 1;

    public bool Tutorials;

	void Update()
	{
		if (this.InputManager.TappedUp)
		{
			if (this.ID == 1)
			{
				this.ListPosition--;

				if (this.ListPosition < 0)
				{
					this.ListPosition = Limit - 16;
					this.ID = 16;
				}
			}
			else
			{
				this.ID--;
			}

			this.UpdateTaskList();
			this.StartCoroutine(this.UpdateTaskInfo());
		}

		if (this.InputManager.TappedDown)
		{
			if (this.ID == 16)
			{
				this.ListPosition++;

				if (this.ID + ListPosition > Limit)
				{
					this.ListPosition = 0;
					this.ID = 1;
				}
			}
			else
			{
				this.ID++;
			}

			this.UpdateTaskList();
			this.StartCoroutine(this.UpdateTaskInfo());
		}

        if (this.Tutorials)
        {
            if (!this.TutorialWindow.Hide && !this.TutorialWindow.Show)
            {
                if (Input.GetButtonDown(InputNames.Xbox_A))
                {
                    OptionGlobals.TutorialsOff = false;

                    this.TutorialWindow.ForceID = ListPosition + ID;
                    this.TutorialWindow.ShowTutorial();
                    this.TutorialWindow.enabled = true;
                    this.TutorialWindow.SummonWindow();
                }

                if (Input.GetButtonDown(InputNames.Xbox_B))
	            {
                    Exit();
                }
            }
        }
        else
        {
            if (Input.GetButtonDown(InputNames.Xbox_B))
            {
                Exit();
            }
        }
    }

    public void UpdateTaskList()
	{
        if (Tutorials)
        {
            for (int ListID = 1; ListID < this.TaskNameLabels.Length; ListID++)
            {
                this.TaskNameLabels[ListID].text = this.TutorialNames[ListID + ListPosition];
            }
        }
        else
        {
		    for (int ListID = 1; ListID < this.TaskNameLabels.Length; ListID++)
		    {
			    if (TaskGlobals.GetTaskStatus(ListID + ListPosition) == 0)
			    {
				    this.TaskNameLabels[ListID].text = "Undiscovered Task #" + (ListID + ListPosition);
			    }
			    else
			    {
				    this.TaskNameLabels[ListID].text = this.JSON.Students[ListID + ListPosition].Name + "'s Task";
			    }

			    this.Checkmarks[ListID].enabled = TaskGlobals.GetTaskStatus(ListID + ListPosition) == 3;
		    }
        }
    }

	public IEnumerator UpdateTaskInfo()
	{
		this.Highlight.localPosition = new Vector3(
			this.Highlight.localPosition.x,
			200.0f - (25.0f * this.ID),
			this.Highlight.localPosition.z);

        if (Tutorials)
        {
            this.TaskIcon.mainTexture = this.TutorialTextures[this.ID + ListPosition];
            //this.TaskDesc.text = this.TutorialDescs[this.ID + ListPosition];
            this.TaskDesc.text = "This tutorial will teach you about " + this.TutorialNames[this.ID + ListPosition];
        }
		else
        {
            if (TaskGlobals.GetTaskStatus(this.ID + ListPosition) == 0)
		    {
			    this.StudentIcon.mainTexture = this.Silhouette;
			    this.TaskIcon.mainTexture = this.QuestionMark;
			    this.TaskDesc.text = "This task has not been discovered yet.";
		    }
		    else
		    {
			    string path = "file:///" + Application.streamingAssetsPath +
				    "/Portraits/Student_" + (ID + ListPosition).ToString() + ".png";
			    WWW www = new WWW(path);

			    yield return www;

			    this.StudentIcon.mainTexture = www.texture;

			    this.TaskWindow.AltGenericCheck(this.ID + ListPosition);

			    if (this.TaskWindow.Generic)
			    {
				    this.TaskIcon.mainTexture = this.TaskWindow.Icons[0];
				    this.TaskDesc.text = this.TaskWindow.Descriptions[0];
			    }
			    else
			    {
				    this.TaskIcon.mainTexture = this.TaskWindow.Icons[this.ID + ListPosition];
				    this.TaskDesc.text = this.TaskWindow.Descriptions[this.ID + ListPosition];
			    }
		    }
        }
    }

    public void Exit()
    {
        this.PauseScreen.PromptBar.ClearButtons();
        this.PauseScreen.PromptBar.Label[0].text = "Accept";
        this.PauseScreen.PromptBar.Label[1].text = "Back";
        this.PauseScreen.PromptBar.Label[4].text = "Choose";
        this.PauseScreen.PromptBar.Label[5].text = "Choose";
        this.PauseScreen.PromptBar.UpdateButtons();

        this.PauseScreen.Sideways = false;
        this.PauseScreen.PressedB = true;
        this.MainMenu.SetActive(true);

        this.gameObject.SetActive(false);
    }
}