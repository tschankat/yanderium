using UnityEngine;

public class GateScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public PromptScript Prompt;
	public ClockScript Clock;

	public Collider EmergencyDoor;
	public Collider GateCollider;

	public Transform RightGate;
	public Transform LeftGate;

	public bool ManuallyAdjusted = false;
	public bool AudioPlayed = false;
	public bool UpdateGates = false;
	public bool Crushing = false;
	public bool Closed = false;

	public AudioSource RightGateAudio;
	public AudioSource LeftGateAudio;

	public AudioSource RightGateLoop;
	public AudioSource LeftGateLoop;

	public AudioClip Start;
	public AudioClip StopOpen;
	public AudioClip StopClose;

	void Update()
	{
		if (!this.ManuallyAdjusted)
		{
			if (((this.Clock.PresentTime / 60.0f) > 8.0f) && 
				((this.Clock.PresentTime / 60.0f) < 15.50f))
			{
				if (!this.Closed)
				{
					this.PlayAudio();

					this.Closed = true;

					if (this.EmergencyDoor.enabled)
					{
						this.EmergencyDoor.enabled = false;
					}
				}
			}
			else
			{
				if (this.Closed)
				{
					this.PlayAudio();

					this.Closed = false;

					if (!this.EmergencyDoor.enabled)
					{
						this.EmergencyDoor.enabled = true;
					}

				}
			}
		}

		if (this.StudentManager.Students[97] != null)
		{
			if (this.StudentManager.Students[97].CurrentAction == StudentActionType.AtLocker &&
				this.StudentManager.Students[97].Routine && this.StudentManager.Students[97].Alive)
			{
				//Debug.Log(Vector3.Distance(this.StudentManager.Students[97].transform.position, this.Prompt.transform.position));

				if (Vector3.Distance(this.StudentManager.Students[97].transform.position, this.StudentManager.Podiums.List[0].position) < .1f)
				{
					if (this.ManuallyAdjusted)
					{
						this.ManuallyAdjusted = false;
					}

					this.Prompt.enabled = false;
					this.Prompt.Hide();
				}
				else
				{
					this.Prompt.enabled = true;
				}
			}
			else
			{
				this.Prompt.enabled = true;
			}
		}
		else
		{
			this.Prompt.enabled = true;
		}

		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Prompt.Circle[0].fillAmount = 1.0f;

			this.PlayAudio();

			this.EmergencyDoor.enabled = !this.EmergencyDoor.enabled;
			this.ManuallyAdjusted = true;
			this.Closed = !this.Closed;

			if (this.StudentManager.Students[97] != null)
			{
				if (this.StudentManager.Students[97].Investigating)
				{
					this.StudentManager.Students[97].StopInvestigating();
				}
			}
		}

		if (!this.Closed)
		{
			if (this.RightGate.localPosition.x != 7)
			{
				this.RightGate.localPosition = new Vector3(
					Mathf.MoveTowards(this.RightGate.localPosition.x, 7.0f, Time.deltaTime),
					this.RightGate.localPosition.y,
					this.RightGate.localPosition.z);
				
				this.LeftGate.localPosition = new Vector3(
					Mathf.MoveTowards(this.LeftGate.localPosition.x, -7.0f, Time.deltaTime),
					this.LeftGate.localPosition.y,
					this.LeftGate.localPosition.z);

				if (!this.AudioPlayed)
				{
					if (this.RightGate.localPosition.x == 7)
					{
						this.RightGateAudio.clip = this.StopOpen;
						this.LeftGateAudio.clip = this.StopOpen;
						this.RightGateAudio.Play();
						this.LeftGateAudio.Play();

						this.RightGateLoop.Stop();
						this.LeftGateLoop.Stop();

						this.AudioPlayed = true;
					}
				}
			}
		}
		else
		{
			if (this.RightGate.localPosition.x != 2.325f)
			{
				if (this.RightGate.localPosition.x < 2.4f)
				{
					this.Crushing = true;
				}

				this.RightGate.localPosition = new Vector3(
					Mathf.MoveTowards(this.RightGate.localPosition.x, 2.325f, Time.deltaTime),
					this.RightGate.localPosition.y,
					this.RightGate.localPosition.z);

				this.LeftGate.localPosition = new Vector3(
					Mathf.MoveTowards(this.LeftGate.localPosition.x, -2.325f, Time.deltaTime),
					this.LeftGate.localPosition.y,
					this.LeftGate.localPosition.z);

				if (!this.AudioPlayed)
				{
					if (this.RightGate.localPosition.x == 2.325f)
					{
						this.RightGateAudio.clip = this.StopOpen;
						this.LeftGateAudio.clip = this.StopOpen;
						this.RightGateAudio.Play();
						this.LeftGateAudio.Play();

						this.RightGateLoop.Stop();
						this.LeftGateLoop.Stop();

						this.AudioPlayed = true;

						this.Crushing = false;
					}
				}
			}
		}
	}

	public void PlayAudio()
	{
		this.RightGateAudio.clip = this.Start;
		this.LeftGateAudio.clip = this.Start;

		this.RightGateAudio.Play();
		this.LeftGateAudio.Play();

		this.RightGateLoop.Play();
		this.LeftGateLoop.Play();

		this.AudioPlayed = false;
	}
}