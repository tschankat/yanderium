using UnityEngine;

public class AudioMenuScript : MonoBehaviour
{
	// @todo: We should prefer the naming convention of having things enabled by default.
	// That is, it should be "Enable Bloom" instead of "Disable Bloom", etc..

	public InputManagerScript InputManager;
	public PauseScreenScript PauseScreen;
	public PromptBarScript PromptBar;
	public JukeboxScript Jukebox;

	public UILabel CurrentTrackLabel;
	public UILabel MusicVolumeLabel;
	public UILabel MusicOnOffLabel;

	public int SelectionLimit = 5;
	public int Selected = 1;

	public Transform Highlight;

	public GameObject CustomMusicMenu;

	void Start()
	{
		this.UpdateText();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			this.CustomMusicMenu.SetActive(true);
			this.gameObject.SetActive(false);
		}

		if (this.InputManager.TappedUp)
		{
			this.Selected--;
			this.UpdateHighlight();
		}
		else if (this.InputManager.TappedDown)
		{
			this.Selected++;
			this.UpdateHighlight();
		}

		// Volume
		if (this.Selected == 1)
		{
			if (this.InputManager.TappedRight)
			{
				if (this.Jukebox.Volume < 1.0f)
				{
					this.Jukebox.Volume += 0.05f;
				}

				this.UpdateText();
			}
			else if (this.InputManager.TappedLeft)
			{
				if (this.Jukebox.Volume > 0f)
				{
					this.Jukebox.Volume -= 0.05f;
				}

				this.UpdateText();
			}
		}
		// On/Off
		else if (this.Selected == 2)
		{
			if (this.InputManager.TappedRight || this.InputManager.TappedLeft)
			{
				this.Jukebox.StartStopMusic();
				this.UpdateText();
			}
		}

		if (Input.GetButtonDown(InputNames.Xbox_B))
		{
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Accept";
			this.PromptBar.Label[1].text = "Exit";
			this.PromptBar.Label[4].text = "Choose";
			this.PromptBar.UpdateButtons();

			this.PauseScreen.ScreenBlur.enabled = true;
			this.PauseScreen.MainMenu.SetActive(true);
			this.PauseScreen.Sideways = false;
			this.PauseScreen.PressedB = true;

			// [af] Added "gameObject" for C# compatibility.
			this.gameObject.SetActive(false);
		}
	}

	public void UpdateText()
	{
		if (this.Jukebox != null)
		{
			this.CurrentTrackLabel.text = "Current Track: " + this.Jukebox.BGM;

			this.MusicVolumeLabel.text = "" + (this.Jukebox.Volume * 10.0f).ToString("F1");

			if (this.Jukebox.Volume == 0)
			{
				this.MusicOnOffLabel.text = "Off";
			}
			else
			{
				this.MusicOnOffLabel.text = "On";
			}
		}
	}

	void UpdateHighlight()
	{
		if (this.Selected == 0)
		{
			this.Selected = this.SelectionLimit;
		}
		else if (this.Selected > this.SelectionLimit)
		{
			this.Selected = 1;
		}

		this.Highlight.localPosition = new Vector3(
			this.Highlight.localPosition.x,
			440.0f - (60.0f * this.Selected),
			this.Highlight.localPosition.z);
	}
}