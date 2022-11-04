using JsonFx.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EditorManagerScript : MonoBehaviour
{
	[SerializeField] UIPanel mainPanel;
	[SerializeField] UIPanel[] editorPanels;
	[SerializeField] UILabel cursorLabel;
	[SerializeField] PromptBarScript promptBar;

	int buttonIndex;
	const int ButtonCount = 3;

	InputManagerScript inputManager;

	void Awake()
	{
		// Start at students button.
		this.buttonIndex = 0;

		this.inputManager = FindObjectOfType<InputManagerScript>();
	}

	void Start()
	{
		this.promptBar.Label[0].text = "Select";
		this.promptBar.Label[1].text = "Exit";
		this.promptBar.Label[4].text = "Choose";
		this.promptBar.UpdateButtons();
	}

	void OnEnable()
	{
		this.promptBar.Label[0].text = "Select";
		this.promptBar.Label[1].text = "Exit";
		this.promptBar.Label[4].text = "Choose";
		this.promptBar.UpdateButtons();
	}

	// Helper method used by each editor for deserializing their .json file.
	public static Dictionary<string, object>[] DeserializeJson(string filename)
	{
		string path = Path.Combine(Application.streamingAssetsPath,
			Path.Combine("JSON", filename));
		string jsonText = File.ReadAllText(path);
		return JsonReader.Deserialize<Dictionary<string, object>[]>(jsonText);
	}

	void HandleInput()
	{
		bool exit = Input.GetButtonDown(InputNames.Xbox_B);

		if (exit)
		{
			SceneManager.LoadScene(SceneNames.TitleScene);
		}

		bool moveUp = this.inputManager.TappedUp;
		bool moveDown = this.inputManager.TappedDown;

		if (moveUp)
		{
			this.buttonIndex = (this.buttonIndex > 0) ? (this.buttonIndex - 1) : (ButtonCount - 1);
		}
		else if (moveDown)
		{
			this.buttonIndex = (this.buttonIndex < (ButtonCount - 1)) ? (this.buttonIndex + 1) : 0;
		}

		bool selectionChanged = moveUp || moveDown;

		if (selectionChanged)
		{
			// Update cursor position.
			Transform cursorTransform = this.cursorLabel.transform;
			cursorTransform.localPosition = new Vector3(
				cursorTransform.localPosition.x,
				100.0f - (this.buttonIndex * 100.0f),
				cursorTransform.localPosition.z);
		}

		bool select = Input.GetButtonDown(InputNames.Xbox_A);

		if (select)
		{
			// Switch to the panel at the specified index.
			this.editorPanels[this.buttonIndex].gameObject.SetActive(true);
			this.mainPanel.gameObject.SetActive(false);
		}
	}

	void Update()
	{
		this.HandleInput();
	}
}
