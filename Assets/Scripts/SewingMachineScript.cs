using UnityEngine;

public class SewingMachineScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;

	public YandereScript Yandere;

	public PromptScript Prompt;

	public Quaternion targetRotation;

	public PickUpScript Uniform;

	public Collider Chair;

	public bool MoveAway = false;
	public bool Sewing = false;
	public bool Check = false;

	public float Timer = 0.0f;

	void Start()
	{
		if (TaskGlobals.GetTaskStatus(30) == 1)
		{
			this.Check = true;
		}
		else if (TaskGlobals.GetTaskStatus(30) > 2)
		{
			this.enabled = false;
		}
	}

	void Update()
	{
		if (Check)
		{
			if (this.Yandere.PickUp != null)
			{
				if (this.Yandere.PickUp.Clothing)
				{
					if (this.Yandere.PickUp.GetComponent<FoldedUniformScript>().Clean &&
						this.Yandere.PickUp.GetComponent<FoldedUniformScript>().Type == 1)
					{
						if (this.Yandere.PickUp.gameObject.GetComponent<FoldedUniformScript>().Type == 1)
						{
							this.Prompt.enabled = true;
						}
					}
				}
			}
			else
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
			}
		}

		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Prompt.Circle[0].fillAmount = 1.0f;

			if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
				this.Yandere.Character.GetComponent<Animation>().CrossFade(AnimNames.FemaleSewing);
				this.Yandere.MyController.radius = 0.10f;
				this.Yandere.CanMove = false;
				this.Chair.enabled = false;
				this.Sewing = true;

				this.GetComponent<AudioSource>().Play();

				this.Uniform = this.Yandere.PickUp;
				this.Yandere.EmptyHands();

				Uniform.transform.parent = this.Yandere.RightHand;
				Uniform.transform.localPosition = new Vector3 (0, 0, 0.09f);
				Uniform.transform.localEulerAngles = new Vector3 (0, 0, 0);
				Uniform.MyRigidbody.useGravity = false;
				Uniform.MyCollider.enabled = false;
			}
		}

		if (this.Sewing)
		{
			this.Timer += Time.deltaTime;

			if (this.Timer < 5.0f)
			{
				this.targetRotation = Quaternion.LookRotation(
					this.transform.parent.transform.parent.position - this.Yandere.transform.position);
				this.Yandere.transform.rotation = Quaternion.Slerp(
					this.Yandere.transform.rotation, this.targetRotation, Time.deltaTime * 10.0f);

				this.Yandere.MoveTowardsTarget(this.Chair.transform.position);
			}
			else
			{
				if (!this.MoveAway)
				{
					this.Yandere.Character.GetComponent<Animation>().CrossFade(this.Yandere.IdleAnim);
					this.Yandere.Inventory.ModifiedUniform = true;
					this.StudentManager.Students[30].TaskPhase = 5;
					TaskGlobals.SetTaskStatus(30, 2);
					Destroy(this.Uniform.gameObject);

					this.MoveAway = true;
					this.Check = false;
				}
				else
				{
					this.Yandere.MoveTowardsTarget(
						this.Chair.gameObject.transform.position + new Vector3(-0.50f, 0.0f, 0.0f));

					if (this.Timer > 6.0f)
					{
						this.Yandere.MyController.radius = 0.20f;
						this.Yandere.CanMove = true;
						this.Chair.enabled = true;
						this.enabled = false;
						this.Sewing = false;

						this.Prompt.Hide();
						this.Prompt.enabled = false;
					}
				}
			}
		}
	}
}