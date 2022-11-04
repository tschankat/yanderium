using UnityEngine;

public class ChangingBoothScript : MonoBehaviour
{
	public YandereScript Yandere;
	public StudentScript Student;
	public PromptScript Prompt;

	public SkinnedMeshRenderer Curtains;
	public Transform ExitSpot;

	public Transform[] WaitSpots;

	public bool YandereChanging = false;
	public bool CannotChange = false;
	public bool Occupied = false;

	public AudioSource MyAudioSource;

	public AudioClip CurtainSound;
	public AudioClip ClothSound;

	public float OccupyTimer = 0.0f;
	public float Weight = 0.0f;

	public ClubType ClubID = ClubType.None;
	public int Phase = 0;

	void Start()
	{
		this.CheckYandereClub();
	}

	void Update()
	{
		if (!this.Occupied)
		{
			if (this.Prompt.Circle[0].fillAmount == 0.0f)
			{
				this.Yandere.EmptyHands();

				this.Yandere.CanMove = false;
				this.YandereChanging = true;

				this.Occupied = true;

				this.Prompt.Hide();
				this.Prompt.enabled = false;
			}
		}

		if (this.Occupied)
		{
			if (this.OccupyTimer == 0.0f)
			{
				if ((this.Yandere.transform.position.y > (this.transform.position.y - 1.0f)) &&
					(this.Yandere.transform.position.y < (this.transform.position.y + 1.0f)))
				{
					//Debug.Log("Sound effect came from: " + this.transform.position);

					//Debug.Log("The student who just stepped into me is: " + this.Student.Name);

					MyAudioSource.clip = this.CurtainSound;
					MyAudioSource.Play();
				}
			}
			else if (this.OccupyTimer > 1.0f)
			{
				if (this.Phase == 0)
				{
					if ((this.Yandere.transform.position.y > (this.transform.position.y - 1.0f)) &&
						(this.Yandere.transform.position.y < (this.transform.position.y + 1.0f)))
					{
						MyAudioSource.clip = this.ClothSound;
						MyAudioSource.Play();
					}

					this.Phase++;
				}
			}

			this.OccupyTimer += Time.deltaTime;

			if (this.YandereChanging)
			{
				if (this.OccupyTimer < 2.0f)
				{
					this.Yandere.CharacterAnimation.CrossFade(this.Yandere.IdleAnim);

					this.Weight = Mathf.Lerp(this.Weight, 0.0f, Time.deltaTime * 10.0f);
					this.Curtains.SetBlendShapeWeight(0, this.Weight);

					this.Yandere.MoveTowardsTarget(this.transform.position);
				}
				else if (this.OccupyTimer < 3.0f)
				{
					this.Weight = Mathf.Lerp(this.Weight, 100.0f, Time.deltaTime * 10.0f);
					this.Curtains.SetBlendShapeWeight(0, this.Weight);

					if (this.Phase < 2)
					{
						MyAudioSource.clip = this.CurtainSound;
						MyAudioSource.Play();

						if (!this.Yandere.ClubAttire)
						{
							this.Yandere.PreviousSchoolwear = this.Yandere.Schoolwear;
						}

						this.Yandere.ChangeClubwear();
						this.Phase++;
					}

					this.Yandere.transform.rotation = Quaternion.Slerp(
						this.Yandere.transform.rotation, this.transform.rotation, 10.0f * Time.deltaTime);
					this.Yandere.MoveTowardsTarget(this.ExitSpot.position);
				}
				else
				{
					this.YandereChanging = false;
					this.Yandere.CanMove = true;
					this.Prompt.enabled = true;
					this.Occupied = false;
					this.OccupyTimer = 0.0f;
					this.Phase = 0;
				}
			}
			else
			{
				if (this.OccupyTimer < 2.0f)
				{
					this.Weight = Mathf.Lerp(this.Weight, 0.0f, Time.deltaTime * 10.0f);
					this.Curtains.SetBlendShapeWeight(0, this.Weight);
				}
				else if (this.OccupyTimer < 3.0f)
				{
					this.Weight = Mathf.Lerp(this.Weight, 100.0f, Time.deltaTime * 10.0f);
					this.Curtains.SetBlendShapeWeight(0, this.Weight);

					if (this.Phase < 2)
					{
						if ((this.Yandere.transform.position.y > (this.transform.position.y - 1.0f)) &&
							(this.Yandere.transform.position.y < (this.transform.position.y + 1.0f)))
						{
							MyAudioSource.clip = this.CurtainSound;
							MyAudioSource.Play();
						}

						this.Student.ChangeClubwear();
						this.Phase++;
					}
				}
				else
				{
					this.Student.WalkAnim = this.Student.OriginalWalkAnim;
					this.Occupied = false;
					this.OccupyTimer = 0.0f;
					this.Student = null;
					this.Phase = 0;

					this.CheckYandereClub();
				}
			}
		}
	}

	public void CheckYandereClub()
	{
		//Debug.Log("Yandere-chan's club is: " + this.Yandere.Club);

		if (this.Yandere.Club != this.ClubID)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
		else
		{
			if (this.Yandere.Bloodiness == 0.0f &&
				!this.CannotChange &&
				this.Yandere.Schoolwear > 0)
			{
				if (!this.Occupied)
				{
					this.Prompt.enabled = true;
				}
				else
				{
					this.Prompt.Hide();
					this.Prompt.enabled = false;
				}
			}
			else
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
			}
		}
	}
}
