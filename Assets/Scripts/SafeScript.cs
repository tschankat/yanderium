using UnityEngine;

public class SafeScript : MonoBehaviour
{
	public MissionModeScript MissionMode;

	public PromptScript ContentsPrompt;
	public PromptScript SafePrompt;
	public PromptScript KeyPrompt;

	public Transform Door;

	public GameObject Key;

	public float Rotation = 0.0f;

	public bool Open = false;

	void Start()
	{
		this.ContentsPrompt.MyCollider.enabled = false;
		this.SafePrompt.enabled = false;
	}

	void Update()
	{
		if (this.Key.activeInHierarchy)
		{
			if (this.KeyPrompt.Circle[0].fillAmount == 0.0f)
			{
				this.KeyPrompt.Yandere.Inventory.SafeKey = true;
				this.SafePrompt.HideButton[0] = false;
				this.SafePrompt.enabled = true;
				this.Key.SetActive(false);
			}
		}

		if (this.SafePrompt.Circle[0].fillAmount == 0.0f)
		{
			this.KeyPrompt.Yandere.Inventory.SafeKey = false;
			this.ContentsPrompt.MyCollider.enabled = true;
			this.Open = true;

			this.SafePrompt.Hide();
			this.SafePrompt.enabled = false;
		}

		if (this.ContentsPrompt.Circle[0].fillAmount == 0.0f)
		{
			this.MissionMode.DocumentsStolen = true;
			this.enabled = false;

			this.ContentsPrompt.Hide();
			this.ContentsPrompt.enabled = false;
			this.ContentsPrompt.gameObject.SetActive(false);
		}

		if (this.Open)
		{
			this.Rotation = Mathf.Lerp(this.Rotation, 0.0f, Time.deltaTime * 10.0f);
			this.Door.localEulerAngles = new Vector3(
				this.Door.localEulerAngles.x,
				this.Rotation,
				this.Door.localEulerAngles.z);

			if (this.Rotation < 1.0f)
			{
				this.Open = false;
			}
		}
		else
		{
			if (this.SafePrompt.Yandere.Inventory.LockPick)
			{
				this.SafePrompt.HideButton[2] = false;
				this.SafePrompt.enabled = true;

				if (this.SafePrompt.Circle[2].fillAmount == 0.0f)
				{
					this.KeyPrompt.Hide();
					this.KeyPrompt.enabled = false;

					this.SafePrompt.Yandere.Inventory.LockPick = false;
					this.SafePrompt.HideButton[2] = true;

					this.ContentsPrompt.MyCollider.enabled = true;
					this.Open = true;
				}
			}
			else
			{
				if (!this.SafePrompt.HideButton[2])
				{
					this.SafePrompt.HideButton[2] = true;
				}
			}
		}
	}
}
