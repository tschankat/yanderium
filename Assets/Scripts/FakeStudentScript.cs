using UnityEngine;

public class FakeStudentScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public DialogueWheelScript DialogueWheel;
	public SubtitleScript Subtitle;
	public YandereScript Yandere;
	public StudentScript Student;
	public PromptScript Prompt;

	public Quaternion targetRotation;

	public float RotationTimer = 0.0f;
	public bool Rotate = false;

	public ClubType Club = ClubType.None;

	public string LeaderAnim;

	void Start()
	{
		this.targetRotation = this.transform.rotation;
		this.Student.Club = this.Club;
	}

	void Update()
	{
		if (!this.Student.Talking)
		{
			if (this.LeaderAnim != "")
			{
				this.GetComponent<Animation>().CrossFade(this.LeaderAnim);
			}

			if (this.Rotate)
			{
				this.transform.rotation = Quaternion.Slerp(
					this.transform.rotation, this.targetRotation, 10.0f * Time.deltaTime);
				this.RotationTimer += Time.deltaTime;

				if (this.RotationTimer > 1.0f)
				{
					this.RotationTimer = 0.0f;
					this.Rotate = false;
				}
			}
		}

		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
				this.Yandere.TargetStudent = this.Student;

				this.Subtitle.UpdateLabel(SubtitleType.ClubGreeting, (int)this.Student.Club, 4.0f);
				this.DialogueWheel.ClubLeader = true;

				this.StudentManager.DisablePrompts();
				this.DialogueWheel.HideShadows();
				this.DialogueWheel.Show = true;
				this.DialogueWheel.Panel.enabled = true;

				this.Student.Talking = true;
				this.Student.TalkTimer = 0.0f;

				this.Yandere.ShoulderCamera.OverShoulder = true;
				this.Yandere.WeaponMenu.KeyboardShow = false;
				this.Yandere.Obscurance.enabled = false;
				this.Yandere.WeaponMenu.Show = false;
				this.Yandere.YandereVision = false;
				this.Yandere.CanMove = false;
				this.Yandere.Talking = true;

				this.RotationTimer = 0;
				this.Rotate = true;
			}
		}
	}
}
