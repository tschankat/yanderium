using UnityEngine;

public class GradingPaperScript : MonoBehaviour
{
	public StudentScript Teacher;

	public GameObject Character;

	public Transform LeftHand;
	public Transform Chair;
	public Transform Paper;

	public float PickUpTime1 = 0.0f;
	public float SetDownTime1 = 0.0f;

	public float PickUpTime2 = 0.0f;
	public float SetDownTime2 = 0.0f;

	public Vector3 OriginalPosition;

	public Vector3 PickUpPosition1;
	public Vector3 SetDownPosition1;
	public Vector3 PickUpPosition2;

	public Vector3 PickUpRotation1;
	public Vector3 SetDownRotation1;
	public Vector3 PickUpRotation2;

	public int Phase = 1;
	public float Speed = 1.0f;

	public bool Writing = false;

	void Start()
	{
		this.OriginalPosition = this.Chair.position;
	}

	void Update()
	{
		if (!this.Writing)
		{
			if (Vector3.Distance(this.Chair.position, this.OriginalPosition) > .01f)
			{
				this.Chair.position = Vector3.Lerp(
					this.Chair.position, this.OriginalPosition, Time.deltaTime * 10.0f);
			}
		}
		else
		{
			if (this.Character != null && this.Teacher != null)
			{
                if (Vector3.Distance(this.Chair.position, this.Character.transform.position + (this.Character.transform.forward * 0.10f)) > .01f)
				{
                    this.Chair.position = Vector3.Lerp(
						this.Chair.position,
						this.Character.transform.position + (this.Character.transform.forward * 0.10f),
						Time.deltaTime * 10.0f);
				}

				if (this.Phase == 1)
				{
					if (this.Teacher.CharacterAnimation[AnimNames.FemaleDeskWrite].time > this.PickUpTime1)
					{
						this.Teacher.CharacterAnimation[AnimNames.FemaleDeskWrite].speed = this.Speed;

						this.Paper.parent = this.LeftHand;
						this.Paper.localPosition = this.PickUpPosition1;
						this.Paper.localEulerAngles = this.PickUpRotation1;

						this.Paper.localScale = new Vector3(0.9090909f, 0.9090909f, 0.9090909f);

						this.Phase++;
					}
				}
				else if (this.Phase == 2)
				{
					if (this.Teacher.CharacterAnimation[AnimNames.FemaleDeskWrite].time > this.SetDownTime1)
					{
						this.Paper.parent = this.Character.transform;
						this.Paper.localPosition = this.SetDownPosition1;
						this.Paper.localEulerAngles = this.SetDownRotation1;

						this.Phase++;
					}
				}
				else if (this.Phase == 3)
				{
					if (this.Teacher.CharacterAnimation[AnimNames.FemaleDeskWrite].time > this.PickUpTime2)
					{
						this.Paper.parent = this.LeftHand;
						this.Paper.localPosition = this.PickUpPosition2;
						this.Paper.localEulerAngles = this.PickUpRotation2;

						this.Phase++;
					}
				}
				else if (this.Phase == 4)
				{
					if (this.Teacher.CharacterAnimation[AnimNames.FemaleDeskWrite].time > this.SetDownTime2)
					{
						this.Paper.parent = this.Character.transform;

						this.Paper.localScale = Vector3.zero;

						this.Phase++;
					}
				}
				else if (this.Phase == 5)
				{
					if (this.Teacher.CharacterAnimation[AnimNames.FemaleDeskWrite].time >=
						this.Teacher.CharacterAnimation[AnimNames.FemaleDeskWrite].length)
					{
						this.Teacher.CharacterAnimation[AnimNames.FemaleDeskWrite].time = 0.0f;
						this.Teacher.CharacterAnimation.Play(AnimNames.FemaleDeskWrite);
						this.Phase = 1;
					}
				}

				if ((this.Teacher.Actions[this.Teacher.Phase] != StudentActionType.GradePapers) ||
					!this.Teacher.Routine || this.Teacher.Stop)
				{
					this.Paper.localScale = Vector3.zero;
					this.Teacher.Obstacle.enabled = false;
					this.Teacher.Pen.SetActive(false);
					this.Writing = false;
					this.Phase = 1;
				}
			}
		}
	}
}
