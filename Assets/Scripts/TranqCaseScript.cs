using UnityEngine;

public class TranqCaseScript : MonoBehaviour
{
	public YandereScript Yandere;
	public RagdollScript Ragdoll;
	public PromptScript Prompt;
	public DoorScript Door;

	public Transform Hinge;

	public bool Occupied = false;

	public bool Open = false;

	public int VictimID = 0;
	public ClubType VictimClubType;

	public float Rotation = 0;
	public bool Animate;

	void Start()
	{
		this.Prompt.enabled = false;
	}

	void Update()
	{
		if ((this.Yandere.transform.position.x > this.transform.position.x) &&
			(Vector3.Distance(this.transform.position, this.Yandere.transform.position) < 1.0f))
		{
			if (this.Yandere.Dragging)
			{
				if (this.Ragdoll == null)
				{
					this.Ragdoll = this.Yandere.Ragdoll.GetComponent<RagdollScript>();
				}

				if (this.Ragdoll.Tranquil)
				{
					if (!this.Prompt.enabled)
					{
						this.Prompt.enabled = true;
					}
				}
				else
				{
					if (this.Prompt.enabled)
					{
						this.Prompt.Hide();
						this.Prompt.enabled = false;
					}
				}
			}
			else
			{
				if (this.Prompt.enabled)
				{
					this.Prompt.Hide();
					this.Prompt.enabled = false;
				}
			}
		}
		else
		{
			if (this.Prompt.enabled)
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
			}
		}
			
		if (this.Prompt.enabled && this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Prompt.Circle[0].fillAmount = 1.0f;
	
			if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
				this.Yandere.TranquilHiding = true;
				this.Yandere.CanMove = false;

				this.Prompt.enabled = false;
				this.Prompt.Hide();

				this.Ragdoll.TranqCase = this;

				this.VictimClubType = this.Ragdoll.Student.Club;
				this.VictimID = this.Ragdoll.StudentID;

				this.Door.Prompt.enabled = true;

				this.Door.enabled = true;

				this.Occupied = true;
				this.Animate = true;
				this.Open = true;
			}
		}

		if (this.Animate)
		{
			if (Open) 
			{
				Rotation = Mathf.Lerp(Rotation, 105, Time.deltaTime * 10.0f);
			}
			else
			{
				Rotation = Mathf.Lerp(Rotation, 0, Time.deltaTime * 10.0f);

				this.Ragdoll.Student.OsanaHairL.transform.localScale = Vector3.MoveTowards (
					this.Ragdoll.Student.OsanaHairL.transform.localScale,
					new Vector3 (0, 0, 0),
					Time.deltaTime * 10);

				this.Ragdoll.Student.OsanaHairR.transform.localScale = Vector3.MoveTowards (
					this.Ragdoll.Student.OsanaHairR.transform.localScale,
					new Vector3 (0, 0, 0),
					Time.deltaTime * 10);

				if (Rotation < 1)
				{
					this.Animate = false;
					this.Rotation = 0;
				}
			}

			this.Hinge.localEulerAngles = new Vector3(
				0,
				0,
				Rotation);
		}
	}
}