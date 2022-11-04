using UnityEngine;

public class TapePlayerScript : MonoBehaviour
{
	public TapePlayerMenuScript TapePlayerMenu;
	public PromptBarScript PromptBar;
	public YandereScript Yandere;
	public PromptScript Prompt;

	public Transform RWButton;
	public Transform FFButton;

	public Camera TapePlayerCamera;

	public Transform[] Rolls;

	public GameObject NoteWindow;
	public GameObject Tape;

	public bool FastForward = false;
	public bool Rewind = false;
	public bool Spin = false;

	public float SpinSpeed = 0.0f;

	void Start()
	{
		this.Tape.SetActive(false);
	}

	void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Yandere.HeartCamera.enabled = false;
			this.Yandere.RPGCamera.enabled = false;

			this.TapePlayerMenu.TimeBar.gameObject.SetActive(true);
			this.TapePlayerMenu.List.gameObject.SetActive(true);
			this.TapePlayerCamera.enabled = true;
			this.TapePlayerMenu.UpdateLabels();
			this.TapePlayerMenu.Show = true;

			this.NoteWindow.SetActive(false);
			this.Yandere.CanMove = false;
			this.Yandere.HUD.alpha = 0.0f;
			Time.timeScale = 0.0001f;

			this.PromptBar.ClearButtons();
			this.PromptBar.Label[1].text = "EXIT";
			this.PromptBar.Label[4].text = "CHOOSE";
			this.PromptBar.Label[5].text = "CATEGORY";
			this.TapePlayerMenu.CheckSelection();
			this.PromptBar.Show = true;

			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}

		if (this.Spin)
		{
			Transform roll0 = this.Rolls[0];
			roll0.localEulerAngles = new Vector3(
				roll0.localEulerAngles.x,
				roll0.localEulerAngles.y + ((1.0f / 60.0f) * (360.0f * this.SpinSpeed)),
				roll0.localEulerAngles.z);

			Transform roll1 = this.Rolls[1];
			roll1.localEulerAngles = new Vector3(
				roll1.localEulerAngles.x,
				roll1.localEulerAngles.y + ((1.0f / 60.0f) * (360.0f * this.SpinSpeed)),
				roll1.localEulerAngles.z);
		}

		if (this.FastForward)
		{
			this.FFButton.localEulerAngles = new Vector3(
				Mathf.MoveTowards(this.FFButton.localEulerAngles.x, 6.25f, 01.666666666f),
				this.FFButton.localEulerAngles.y,
				this.FFButton.localEulerAngles.z);

			this.SpinSpeed = 2.0f;
		}
		else
		{
			this.FFButton.localEulerAngles = new Vector3(
				Mathf.MoveTowards(this.FFButton.localEulerAngles.x, 0.0f, 01.666666666f),
				this.FFButton.localEulerAngles.y,
				this.FFButton.localEulerAngles.z);

			this.SpinSpeed = 1.0f;
		}

		if (this.Rewind)
		{
			this.RWButton.localEulerAngles = new Vector3(
				Mathf.MoveTowards(this.RWButton.localEulerAngles.x, 6.25f, 01.666666666f),
				this.RWButton.localEulerAngles.y,
				this.RWButton.localEulerAngles.z);

			this.SpinSpeed = -2.0f;
		}
		else
		{
			this.RWButton.localEulerAngles = new Vector3(
				Mathf.MoveTowards(this.RWButton.localEulerAngles.x, 0.0f, 01.666666666f),
				this.RWButton.localEulerAngles.y,
				this.RWButton.localEulerAngles.z);
		}
	}
}
