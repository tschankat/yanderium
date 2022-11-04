using UnityEngine;

public class TextMessageManagerScript : MonoBehaviour
{
	public PauseScreenScript PauseScreen;
	public PromptBarScript PromptBar;
	public GameObject ServicesMenu;

	public string[] Messages;

	void Update()
	{
		if (Input.GetButtonDown(InputNames.Xbox_B))
		{
			Destroy(this.NewMessage);

			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Accept";
			this.PromptBar.Label[1].text = "Exit";
			this.PromptBar.Label[5].text = "Choose";
			this.PromptBar.UpdateButtons();

			this.PauseScreen.Sideways = true;
			this.ServicesMenu.SetActive(true);

			// [af] Added "gameObject" for C# compatibility.
			this.gameObject.SetActive(false);
		}
	}

	private GameObject NewMessage;

	public GameObject Message;
	public int MessageHeight = 0;
	public string MessageText = string.Empty;

	public void SpawnMessage(int ServiceID)
	{
		this.PromptBar.ClearButtons();
		this.PromptBar.Label[1].text = "Exit";
		this.PromptBar.UpdateButtons();

		this.PauseScreen.Sideways = false;
		this.ServicesMenu.SetActive(false);

		// [af] Added "gameObject" for C# compatibility.
		this.gameObject.SetActive(true);

		if (this.NewMessage != null)
		{
			Destroy(this.NewMessage);
		}

		this.NewMessage = Instantiate(this.Message);
		this.NewMessage.transform.parent = this.transform;
		this.NewMessage.transform.localPosition = new Vector3(-225.0f, -275.0f, 0.0f);
		this.NewMessage.transform.localEulerAngles = Vector3.zero;
		this.NewMessage.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

		this.MessageText = Messages[ServiceID];

		if (ServiceID == 7 || ServiceID == 4)
		{
			this.MessageHeight = 11;
		}
		else if (ServiceID == 9)
        {
            this.MessageHeight = 6;
        }
        else
        {
			this.MessageHeight = 5;
		}

		this.NewMessage.GetComponent<UISprite>().height = 36 + (36 * this.MessageHeight);
		this.NewMessage.GetComponent<TextMessageScript>().Label.text = this.MessageText;
	}
}
