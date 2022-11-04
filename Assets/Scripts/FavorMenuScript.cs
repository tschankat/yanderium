using UnityEngine;

public class FavorMenuScript : MonoBehaviour
{
	public TutorialWindowScript TutorialWindow;

	public InputManagerScript InputManager;
	public PauseScreenScript PauseScreen;
	public ServicesScript ServicesMenu;
	public SchemesScript SchemesMenu;
	public DropsScript DropsMenu;

	public PromptBarScript PromptBar;

	public Transform Highlight;

	public int ID = 1;

	void Update()
	{
		if (this.InputManager.TappedRight)
		{
			this.ID++;
			this.UpdateHighlight();
		}
		else if (this.InputManager.TappedLeft)
		{
			this.ID--;
			this.UpdateHighlight();
		}

		if (!this.TutorialWindow.Hide && !this.TutorialWindow.Show)
		{
			if (Input.GetButtonDown(InputNames.Xbox_A))
			{
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[0].text = "Accept";
				this.PromptBar.Label[1].text = "Exit";
				this.PromptBar.Label[4].text = "Choose";
				this.PromptBar.UpdateButtons();

				//Anti-Osana Code
				if (this.ID == 1)
				{
					//Anti-Osana Countermeasure

					#if UNITY_EDITOR
					this.SchemesMenu.UpdatePantyCount();
					this.SchemesMenu.UpdateSchemeList();
					this.SchemesMenu.UpdateSchemeInfo();

					this.SchemesMenu.gameObject.SetActive(true);

					// [af] Added "gameObject" for C# compatibility.
					this.gameObject.SetActive(false);
					#endif
				}
				else if (this.ID == 2)
				{
					this.ServicesMenu.UpdatePantyCount();
					this.ServicesMenu.UpdateList();
					this.ServicesMenu.UpdateDesc();

					this.ServicesMenu.gameObject.SetActive(true);

					// [af] Added "gameObject" for C# compatibility.
					this.gameObject.SetActive(false);
				}
				else if (this.ID == 3)
				{
					this.DropsMenu.UpdatePantyCount();
					this.DropsMenu.UpdateList();
					this.DropsMenu.UpdateDesc();

					this.DropsMenu.gameObject.SetActive(true);

					// [af] Added "gameObject" for C# compatibility.
					this.gameObject.SetActive(false);
				}
			}

			if (Input.GetButtonDown(InputNames.Xbox_X))
			{
				//TutorialGlobals.IgnoreClothing = true;
				//TutorialWindow.IgnoreClothing = true;

				TutorialWindow.TitleLabel.text = "Info Points";
				TutorialWindow.TutorialLabel.text = TutorialWindow.PointsString;
				TutorialWindow.TutorialLabel.text = TutorialWindow.TutorialLabel.text.Replace('@', '\n');
				TutorialWindow.TutorialImage.mainTexture = TutorialWindow.InfoTexture;

				TutorialWindow.enabled = true;
				TutorialWindow.SummonWindow();
			}

			if (Input.GetButtonDown(InputNames.Xbox_B))
			{
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[0].text = "Accept";
				this.PromptBar.Label[1].text = "Exit";
				this.PromptBar.Label[4].text = "Choose";
				this.PromptBar.UpdateButtons();

				this.PauseScreen.MainMenu.SetActive(true);
				this.PauseScreen.Sideways = false;
				this.PauseScreen.PressedB = true;

				// [af] Added "gameObject" for C# compatibility.
				this.gameObject.SetActive(false);
			}
		}
	}

	void UpdateHighlight()
	{
		if (this.ID > 3)
		{
			this.ID = 1;
		}
		else if (this.ID < 1)
		{
			this.ID = 3;
		}
		
		this.Highlight.transform.localPosition = new Vector3(
			-500.0f + (250.0f * this.ID),
			this.Highlight.transform.localPosition.y,
			this.Highlight.transform.localPosition.z);
	}
}
