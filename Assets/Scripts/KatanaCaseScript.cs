using UnityEngine;

public class KatanaCaseScript : MonoBehaviour
{
	public PromptScript CasePrompt;
	public PromptScript KeyPrompt;

	public Transform Door;

	public GameObject Key;

	public float Rotation = 0.0f;

	public bool Open = false;

	void Start()
	{
		this.CasePrompt.enabled = false;
	}

	void Update()
	{
		if (this.Key.activeInHierarchy)
		{
			if (this.KeyPrompt.Circle[0].fillAmount == 0.0f)
			{
				this.KeyPrompt.Yandere.Inventory.CaseKey = true;
				this.CasePrompt.HideButton[0] = false;
				this.CasePrompt.enabled = true;
				this.Key.SetActive(false);
			}
		}

		if (this.CasePrompt.Circle[0].fillAmount == 0.0f)
		{
			this.KeyPrompt.Yandere.Inventory.CaseKey = false;
			this.Open = true;

			this.CasePrompt.Hide();
			this.CasePrompt.enabled = false;
		}

		if (this.CasePrompt.Yandere.Inventory.LockPick)
		{
			this.CasePrompt.HideButton[2] = false;
			this.CasePrompt.enabled = true;

			if (this.CasePrompt.Circle[2].fillAmount == 0.0f)
			{
				this.KeyPrompt.Hide();
				this.KeyPrompt.enabled = false;

				this.CasePrompt.Yandere.Inventory.LockPick = false;
				this.CasePrompt.Label[0].text = "     " + "Open";
				this.CasePrompt.HideButton[2] = true;
				this.CasePrompt.HideButton[0] = true;
				this.Open = true;
			}
		}
		else
		{
			if (!this.CasePrompt.HideButton[2])
			{
				this.CasePrompt.HideButton[2] = true;
			}
		}

		if (this.Open)
		{
			this.Rotation = Mathf.Lerp(this.Rotation, -180.0f, Time.deltaTime * 10.0f);
			this.Door.eulerAngles = new Vector3(
				this.Door.eulerAngles.x,
				this.Door.eulerAngles.y,
				this.Rotation);

			if (this.Rotation < -179.90f)
			{
				this.enabled = false;
			}
		}
	}
}
