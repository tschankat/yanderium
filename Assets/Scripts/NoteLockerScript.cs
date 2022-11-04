using UnityEngine;

public class NoteLockerScript : MonoBehaviour
{
	public FindStudentLockerScript FindStudentLocker;
	public StudentManagerScript StudentManager;
	public NoteWindowScript NoteWindow;
	public PromptBarScript PromptBar;
	public StudentScript Student;
	public YandereScript Yandere;
	public ListScript MeetSpots;
	public PromptScript Prompt;

	public GameObject NewBall;
	public GameObject NewNote;
	public GameObject Locker;
	public GameObject Ball;
	public GameObject Note;

	public AudioClip NoteSuccess;
	public AudioClip NoteFail;
	public AudioClip NoteFind;

	public bool CheckingNote = false;
	public bool CanLeaveNote = true;
	public bool SpawnedNote = false;
	public bool NoteLeft = false;
	public bool Success = false;

	public float MeetTime = 0.0f;
	public float Timer = 0.0f;

	public int LockerOwner = 0;
	public int MeetID = 0;
	public int Phase = 1;

	void Update()
	{
		if (this.Student != null)
		{
			Vector3 positionAtStudentHeight = new Vector3(
				this.transform.position.x,
				this.Student.transform.position.y,
				this.transform.position.z);

			if (this.Prompt.enabled)
			{
				if ((Vector3.Distance(this.Student.transform.position, positionAtStudentHeight) < 1.0f) ||
					this.Yandere.Armed)
				{
					this.Prompt.Hide();
					this.Prompt.enabled = false;
				}
			}
			else
			{
				if (this.CanLeaveNote)
				{
					if ((Vector3.Distance(this.Student.transform.position, positionAtStudentHeight) > 1.0f) &&
						!this.Yandere.Armed)
					{
						this.Prompt.enabled = true;
					}
				}
			}
		}
		else
		{
			this.Student = this.StudentManager.Students[this.LockerOwner];
		}

		if (this.Prompt != null)
		{
			if (this.Prompt.Circle[0].fillAmount == 0.0f)
			{
				this.Prompt.Circle[0].fillAmount = 1.0f;
				this.NoteWindow.NoteLocker = this;
				this.Yandere.Blur.enabled = true;

				// [af] Added "gameObject" for C# compatibility.
				this.NoteWindow.gameObject.SetActive(true);

				this.Yandere.CanMove = false;
				this.NoteWindow.Show = true;
				this.Yandere.HUD.alpha = 0.0f;
				this.PromptBar.Show = true;
				Time.timeScale = 0.0001f;

				this.PromptBar.Label[0].text = "Confirm";
				this.PromptBar.Label[1].text = "Cancel";
				this.PromptBar.Label[4].text = "Select";
				this.PromptBar.UpdateButtons();
			}
		}

		if (this.NoteLeft)
		{
			if (this.Student != null)
			{
				if (this.Student.Routine && this.Student.Phase == 2 || this.Student.Routine && this.Student.Phase == 8 ||
					this.Student.SentToLocker)
				{
					if (Vector3.Distance(this.transform.position, this.Student.transform.position) < 2.0f)
					{
						if (!this.Student.InEvent)
						{
							this.Student.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;

							if (!this.Success)
							{
								this.Student.CharacterAnimation.CrossFade(this.Student.TossNoteAnim);
							}
							else
							{
								this.Student.CharacterAnimation.CrossFade(this.Student.KeepNoteAnim);
							}

							this.Student.Pathfinding.canSearch = false;
							this.Student.Pathfinding.canMove = false;
							this.Student.CheckingNote = true;
							this.Student.Routine = false;
							this.Student.InEvent = true;

							this.CheckingNote = true;
						}
					}
				}
			}

			if (this.CheckingNote)
			{
				this.Timer += Time.deltaTime;

				this.Student.MoveTowardsTarget(this.Student.MyLocker.position);
				this.Student.transform.rotation = Quaternion.Slerp(
					this.Student.transform.rotation, this.Student.MyLocker.rotation, 10.0f * Time.deltaTime);

				if (this.Student != null)
				{
					if (this.Student.CharacterAnimation[this.Student.TossNoteAnim].time >=
						this.Student.CharacterAnimation[this.Student.TossNoteAnim].length)
					{
						this.Finish();
					}

					if (this.Student.CharacterAnimation[this.Student.KeepNoteAnim].time >=
						this.Student.CharacterAnimation[this.Student.KeepNoteAnim].length)
					{
						this.DetermineSchedule();
						this.Finish();
					}
				}

				if (this.Timer > 3.5f && !this.SpawnedNote)
				{
					this.NewNote = Instantiate(this.Note, this.transform.position, Quaternion.identity);
					this.NewNote.transform.parent = this.Student.LeftHand;

					if (this.Student.Male)
					{
						this.NewNote.transform.localPosition = new Vector3(-0.133333f, -0.030f, 0.0133333f);
					}
					else
					{
						this.NewNote.transform.localPosition = new Vector3(-0.060f, -0.010f, 0.0f);
					}

					this.NewNote.transform.localEulerAngles = new Vector3(-75.0f, -90.0f, 180.0f);
					this.NewNote.transform.localScale = new Vector3(0.10f, 0.20f, 1.0f);

					this.SpawnedNote = true;
				}

				//If the student is tossing the note...
				if (!this.Success)
				{
					if (this.Timer > 10)
					{
						if (this.NewNote != null)
						{
							// [af] Replaced if/else statement with ternary expression.
							if (this.NewNote.transform.localScale.z > 0.10f)
							{
								this.NewNote.transform.localScale = Vector3.MoveTowards(
									this.NewNote.transform.localScale, Vector3.zero, Time.deltaTime * 2);
							}
							else
							{
								Destroy(this.NewNote);
							}
						}
					}

					if ((this.Timer > (367.5f / 30.0f)) && (this.NewBall == null))
					{
						this.NewBall = Instantiate(this.Ball,
							this.Student.LeftHand.position, Quaternion.identity);

						Rigidbody ballRigidBody = this.NewBall.GetComponent<Rigidbody>();
						ballRigidBody.AddRelativeForce(this.Student.transform.forward * - 100.0f);
						ballRigidBody.AddRelativeForce(Vector3.up * 100.0f);
						this.Phase++;
					}
				}
				//If the student is keeping the note...
				else
				{
					if (this.Timer > 12.5f)
					{
						if (this.NewNote != null)
						{
							// [af] Replaced if/else statement with ternary expression.
							if (this.NewNote.transform.localScale.z > 0.10f)
							{
								this.NewNote.transform.localScale = Vector3.MoveTowards(
									this.NewNote.transform.localScale, Vector3.zero, Time.deltaTime * 2);
							}
							else
							{
								Destroy(this.NewNote);
							}
						}
					}
				}

				if (this.Phase == 1)
				{
					if (this.Timer > (70.0f / 30.0f))
					{
						if (!this.Student.Male)
						{
							this.Yandere.Subtitle.Speaker = this.Student;
							this.Yandere.Subtitle.UpdateLabel(SubtitleType.NoteReaction, 1, 3.0f);
						}
						else
						{
							this.Yandere.Subtitle.Speaker = this.Student;
							this.Yandere.Subtitle.UpdateLabel(SubtitleType.NoteReactionMale, 1, 3.0f);
						}

						this.Phase++;
					}
				}
				else if (this.Phase == 2)
				{
					if (!this.Success)
					{
						if (this.Timer > (290.0f / 30.0f))
						{
							if (!this.Student.Male)
							{
								this.Yandere.Subtitle.Speaker = this.Student;
								this.Yandere.Subtitle.UpdateLabel(SubtitleType.NoteReaction, 2, 3.0f);
							}
							else
							{
								this.Yandere.Subtitle.Speaker = this.Student;
								this.Yandere.Subtitle.UpdateLabel(SubtitleType.NoteReactionMale, 2, 3.0f);
							}

							this.Phase++;
						}
					}
					else
					{
						if (this.Timer > (305.0f / 30.0f))
						{
							if (!this.Student.Male)
							{
								this.Yandere.Subtitle.Speaker = this.Student;
								this.Yandere.Subtitle.UpdateLabel(SubtitleType.NoteReaction, 3, 3.0f);
							}
							else
							{
								this.Yandere.Subtitle.Speaker = this.Student;
								this.Yandere.Subtitle.UpdateLabel(SubtitleType.NoteReactionMale, 3, 3.0f);
							}

							this.Phase++;
						}
					}
				}
			}
		}
	}

	void Finish()
	{
		if (this.Success)
		{
			if (this.Student.Clock.HourTime > this.Student.MeetTime)
			{
				this.Student.CurrentDestination = this.Student.MeetSpot;
				this.Student.Pathfinding.target = this.Student.MeetSpot;

				this.Student.Meeting = true;
				this.Student.MeetTime = 0.0f;
			}
			else
			{
				this.Student.CurrentDestination = this.Student.Destinations[this.Student.Phase];
				this.Student.Pathfinding.target = this.Student.Destinations[this.Student.Phase];
			}

			this.Student.Pathfinding.canSearch = true;
			this.Student.Pathfinding.canMove = true;
			this.Student.Pathfinding.speed = 1;
		}
		else
		{
			Debug.Log(this.Student.Name + " has rejected the note, and is being told to travel to the destination of their current phase.");

			this.Student.CurrentDestination = this.Student.Destinations[this.Student.Phase];
			this.Student.Pathfinding.target = this.Student.Destinations[this.Student.Phase];

            this.FindStudentLocker.Prompt.Label[0].text = "     " + "Find Student Locker";
            this.FindStudentLocker.TargetedStudent = null;
            this.FindStudentLocker.Prompt.enabled = true;
            this.FindStudentLocker.Phase = 1;
        }

		Animation studentCharAnim = this.Student.Character.GetComponent<Animation>();
		studentCharAnim.cullingType = AnimationCullingType.BasedOnRenderers;
		studentCharAnim.CrossFade(this.Student.IdleAnim);
		this.Student.DistanceToDestination = 100.0f;
		this.Student.CheckingNote = false;
		this.Student.SentToLocker = false;
		this.Student.InEvent = false;
		this.Student.Routine = true;

		this.CheckingNote = false;
		this.NoteLeft = false;
		this.Phase++;
	}

	void DetermineSchedule()
	{
		this.Student.MeetSpot = this.MeetSpots.List[this.MeetID];
		this.Student.MeetTime = this.MeetTime;
	}
}