using UnityEngine;

// [af] To be used for editing Rivals.json.
public class RivalEditorScript : MonoBehaviour
{
	[SerializeField] UIPanel mainPanel;
	[SerializeField] UIPanel rivalPanel;
	[SerializeField] UILabel titleLabel;

	[SerializeField] PromptBarScript promptBar;

	InputManagerScript inputManager;

	void Awake()
	{
		this.inputManager = FindObjectOfType<InputManagerScript>();
	}

	void OnEnable()
	{
		this.promptBar.Label[0].text = string.Empty;
		this.promptBar.Label[1].text = "Back";
		this.promptBar.Label[4].text = string.Empty;
		this.promptBar.UpdateButtons();
	}

	void HandleInput()
	{
		bool back = Input.GetButtonDown(InputNames.Xbox_B);

		if (back)
		{
			this.mainPanel.gameObject.SetActive(true);
			this.rivalPanel.gameObject.SetActive(false);
		}
	}

	void Update()
	{
		this.HandleInput();
	}
}
