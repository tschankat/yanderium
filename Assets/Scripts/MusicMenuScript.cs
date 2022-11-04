using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MusicMenuScript : MonoBehaviour
{
	public InputManagerScript InputManager;
	public PauseScreenScript PauseScreen;
	public PromptBarScript PromptBar;

	public GameObject AudioMenu;

	public JukeboxScript Jukebox;

	public int SelectionLimit = 9;
	public int Selected = 0;

	public Transform Highlight;
	public string path = string.Empty;

	public AudioClip CustomMusic;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			this.AudioMenu.SetActive(true);
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

		if (Input.GetButtonDown(InputNames.Xbox_A))
		{
			this.StartCoroutine(DownloadCoroutine());
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

			// [af] Added ".gameObject." for C# compatibility.
			this.gameObject.SetActive(false);
		}
	}

	IEnumerator DownloadCoroutine()
	{
		WWW CurrentDownload = new WWW("File:///" + Application.streamingAssetsPath + "/Music/track" + this.Selected + ".ogg");
		yield return CurrentDownload;

		//CustomMusic = WWWAudioExtensions.GetAudioClipCompressed(CurrentDownload);
		CustomMusic = CurrentDownload.GetAudioClipCompressed();

		this.Jukebox.Custom.clip = CustomMusic;
		this.Jukebox.PlayCustom();
	}

	void UpdateHighlight()
	{
		if (this.Selected < 0)
		{
			this.Selected = this.SelectionLimit;
		}
		else if (this.Selected > this.SelectionLimit)
		{
			this.Selected = 0;
		}

		this.Highlight.localPosition = new Vector3(
			this.Highlight.localPosition.x,
			365.0f - (80.0f * this.Selected),
			this.Highlight.localPosition.z);
	}
}
