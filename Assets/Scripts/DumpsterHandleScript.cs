using UnityEngine;

public class DumpsterHandleScript : MonoBehaviour
{
	public DumpsterLidScript DumpsterLid;
	public PromptBarScript PromptBar;
	public PromptScript Prompt;

	public Transform GrabSpot;

	public GameObject Panel;

	public bool Grabbed = false;

	public float Direction = 0.0f;

	public float PullLimit = 0.0f;
	public float PushLimit = 0.0f;

	void Start()
	{
		this.Panel.SetActive(false);
	}

	void Update()
	{
		// [af] Replaced if/else statement with boolean expression.
		this.Prompt.HideButton[3] = (this.Prompt.Yandere.PickUp != null) ||
			this.Prompt.Yandere.Dragging || this.Prompt.Yandere.Carrying;

		if (this.Prompt.Circle[3].fillAmount == 0.0f)
		{
			this.Prompt.Circle[3].fillAmount = 1.0f;

			if (!this.Prompt.Yandere.Chased && this.Prompt.Yandere.Chasers == 0)
			{
				this.Prompt.Yandere.DumpsterGrabbing = true;
				this.Prompt.Yandere.DumpsterHandle = this;
				this.Prompt.Yandere.CanMove = false;

				this.PromptBar.ClearButtons();
				this.PromptBar.Label[1].text = "STOP";
				this.PromptBar.Label[5].text = "PUSH / PULL";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;

				this.Grabbed = true;
			}
		}

		if (this.Grabbed)
		{
			this.Prompt.Yandere.transform.rotation = Quaternion.Lerp(
				this.Prompt.Yandere.transform.rotation, this.GrabSpot.rotation, Time.deltaTime * 10.0f);

			if (Vector3.Distance(this.Prompt.Yandere.transform.position, this.GrabSpot.position) > 0.10f)
			{
				this.Prompt.Yandere.MoveTowardsTarget(this.GrabSpot.position);
			}
			else
			{
				this.Prompt.Yandere.transform.position = this.GrabSpot.position;
			}

			if ((Input.GetAxis("Horizontal") > 0.50f) ||
				(Input.GetAxis("DpadX") > 0.50f) ||
				Input.GetKey("right"))
			{
				this.transform.parent.transform.position = new Vector3(
					this.transform.parent.transform.position.x,
					this.transform.parent.transform.position.y,
					this.transform.parent.transform.position.z - Time.deltaTime);
			}
			else if ((Input.GetAxis("Horizontal") < -0.50f) ||
				(Input.GetAxis("DpadX") < -0.50f) ||
				Input.GetKey("left"))
			{
				this.transform.parent.transform.position = new Vector3(
					this.transform.parent.transform.position.x,
					this.transform.parent.transform.position.y,
					this.transform.parent.transform.position.z + Time.deltaTime);
			}

			if (this.PullLimit < this.PushLimit)
			{
				if (this.transform.parent.transform.position.z < this.PullLimit)
				{
					this.transform.parent.transform.position = new Vector3(
						this.transform.parent.transform.position.x,
						this.transform.parent.transform.position.y,
						this.PullLimit);
				}
				else if (this.transform.parent.transform.position.z > this.PushLimit)
				{
					this.transform.parent.transform.position = new Vector3(
						this.transform.parent.transform.position.x,
						this.transform.parent.transform.position.y,
						this.PushLimit);
				}
			}
			else
			{
				if (this.transform.parent.transform.position.z > this.PullLimit)
				{
					this.transform.parent.transform.position = new Vector3(
						this.transform.parent.transform.position.x,
						this.transform.parent.transform.position.y,
						this.PullLimit);
				}
				else if (this.transform.parent.transform.position.z < this.PushLimit)
				{
					this.transform.parent.transform.position = new Vector3(
						this.transform.parent.transform.position.x,
						this.transform.parent.transform.position.y,
						this.PushLimit);
				}
			}

			// [af] Replaced if/else statement with boolean expression.
			this.Panel.SetActive(
				(this.DumpsterLid.transform.position.z > (this.DumpsterLid.DisposalSpot - 0.050f)) &&
				(this.DumpsterLid.transform.position.z < (this.DumpsterLid.DisposalSpot + 0.050f)));

			if (this.Prompt.Yandere.Chased || this.Prompt.Yandere.Chasers > 0 ||
				Input.GetButtonDown(InputNames.Xbox_B))
			{
				StopGrabbing();
			}
		}
	}

	void StopGrabbing()
	{
		this.Prompt.Yandere.DumpsterGrabbing = false;
		this.Prompt.Yandere.CanMove = true;

		this.PromptBar.ClearButtons();
		this.PromptBar.Show = false;

		this.Panel.SetActive(false);

		this.Grabbed = false;
	}
}