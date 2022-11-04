using UnityEngine;

public class PickpocketScript : MonoBehaviour
{
	public PickpocketMinigameScript PickpocketMinigame;
	public StudentScript Student;
	public PromptScript Prompt;

	public UIPanel PickpocketPanel;
	public UISprite TimeBar;

	public Transform PickpocketSpot;

	public GameObject AlarmDisc;
	public GameObject Key;

	public float Timer = 0.0f;

	public int ID = 1;

	public bool NotNurse = false;
	public bool Test = false;

	void Start()
	{
		if (/* this.Student.StudentID != this.Student.StudentManager.NurseID && */ this.Student.StudentID != 71)
		{
			this.Prompt.transform.parent.gameObject.SetActive(false);
			this.enabled = false;
		}
		else
		{
			this.PickpocketMinigame = Student.StudentManager.PickpocketMinigame;

			if (this.Student.StudentID == this.Student.StudentManager.NurseID)
			{
				this.ID = 2;
			}
			else
			{
				if (ClubGlobals.GetClubClosed(this.Student.OriginalClub))
				{
					this.Prompt.transform.parent.gameObject.SetActive(false);
					this.enabled = false;
				}
				else
				{
					this.Prompt.Label[3].text = "     " + "Steal Shed Key";
					this.NotNurse = true;
				}
			}
		}
	}

	void Update()
	{
		if (this.Prompt.transform.parent != null)
		{
			if (Student.Routine)
			{
				if (Student.DistanceToDestination > .5f)
				{
					if (this.Prompt.enabled)
					{
						this.Prompt.Hide();
						this.Prompt.enabled = false;

						this.PickpocketPanel.enabled = false;
					}

					if (this.Student.Yandere.Pickpocketing && this.PickpocketMinigame.ID == this.ID)
					{
						this.Prompt.Yandere.Caught = true;
						this.PickpocketMinigame.End();
						this.Punish();
					}
				}
				else
				{
					this.PickpocketPanel.enabled = true;

					if (this.Student.Yandere.PickUp == null && this.Student.Yandere.Pursuer == null)
					{
						this.Prompt.enabled = true;
					}
					else
					{
						this.Prompt.enabled = false;
						this.Prompt.Hide();
					}

					this.Timer += (Time.deltaTime * this.Student.CharacterAnimation[this.Student.PatrolAnim].speed);

					this.TimeBar.fillAmount = 1.0f - (this.Timer / this.Student.CharacterAnimation[this.Student.PatrolAnim].length);

					if (this.Timer > this.Student.CharacterAnimation[this.Student.PatrolAnim].length)
					{
						if (this.Student.Yandere.Pickpocketing && this.PickpocketMinigame.ID == this.ID)
						{
							this.Prompt.Yandere.Caught = true;
							this.PickpocketMinigame.End();
							this.Punish();
						}

						this.Timer = 0.0f;
					}
				}
			}
			else
			{
				if (this.Prompt.enabled)
				{
					this.Prompt.Hide();
					this.Prompt.enabled = false;

					this.PickpocketPanel.enabled = false;

					if (this.Student.Yandere.Pickpocketing && this.PickpocketMinigame.ID == this.ID)
					{
						this.Prompt.Yandere.Caught = true;
						this.PickpocketMinigame.End();
						this.Punish();
					}
				}
			}
					
			if (this.Prompt.Circle[3].fillAmount == 0.0f)
			{
				this.Prompt.Circle[3].fillAmount = 1;

				if (!this.Prompt.Yandere.Chased && this.Prompt.Yandere.Chasers == 0)
				{
					this.PickpocketMinigame.StartingAlerts = this.Prompt.Yandere.Alerts;
					this.PickpocketMinigame.PickpocketSpot = this.PickpocketSpot;
					this.PickpocketMinigame.NotNurse = this.NotNurse;
					this.PickpocketMinigame.Show = true;
					this.PickpocketMinigame.ID = this.ID;

					this.Student.Yandere.CharacterAnimation.CrossFade(AnimNames.FemalePickpocketing);
					this.Student.Yandere.Pickpocketing = true;
					this.Student.Yandere.EmptyHands();

					this.Student.Yandere.CanMove = false;
				}
			}

			if (this.PickpocketMinigame != null)
			{
				if (this.PickpocketMinigame.ID == this.ID)
				{
					if (this.PickpocketMinigame.Success)
					{
						this.PickpocketMinigame.Success = false;
						this.PickpocketMinigame.ID = 0;

						this.Succeed();

						//this.Prompt.gameObject.SetActive(false);

						this.PickpocketPanel.enabled = false;
						this.Prompt.enabled = false;
						this.Prompt.Hide();

						this.Key.SetActive(false);
						this.enabled = false;
					}

					if (this.PickpocketMinigame.Failure)
					{
						this.PickpocketMinigame.Failure = false;
						this.PickpocketMinigame.ID = 0;
						this.Punish();
					}
				}
			}

			if (this.Student.Alive == false)
			{
				this.transform.position = new Vector3(
					this.Student.transform.position.x,
					this.Student.transform.position.y + 1,
					this.Student.transform.position.z);
				
				this.Prompt.gameObject.GetComponent<BoxCollider>().isTrigger = false;
				this.Prompt.gameObject.GetComponent<Rigidbody>().isKinematic = false;
				this.Prompt.gameObject.GetComponent<Rigidbody>().useGravity = true;
				this.Prompt.enabled = true;

				this.transform.parent = null;
			}
		}
		else
		{
			if (this.Prompt.Circle[3].fillAmount == 0.0f)
			{
				this.Succeed();
				this.Prompt.Hide();

				this.PickpocketPanel.enabled = false;
				this.Prompt.enabled = false;
				this.Prompt.Hide();

				this.Key.SetActive(false);
				this.enabled = false;
			}
		}
	}

	void Punish()
	{
		Debug.Log("Punishing Yandere-chan for pickpocketing.");

		GameObject NewAlarmDisc = Instantiate(this.AlarmDisc,
			this.Student.Yandere.transform.position + Vector3.up, Quaternion.identity);
		NewAlarmDisc.GetComponent<AlarmDiscScript>().NoScream = true;

		if (!this.NotNurse && !this.Prompt.Yandere.Egg)
		{
			Debug.Log("A faculty member saw pickpocketing.");

			this.Student.Witnessed = StudentWitnessType.Theft;
			this.Student.SenpaiNoticed();
			this.Student.CameraEffects.MurderWitnessed();
			this.Student.Concern = 5;
		}
		else
		{
			this.Student.Witnessed = StudentWitnessType.Pickpocketing;
			this.Student.CameraEffects.Alarm();
			this.Student.Alarm += 200;
		}

		this.Timer = 0.0f;
		this.Prompt.Hide();
		this.Prompt.enabled = false;
		this.PickpocketPanel.enabled = false;

		this.Student.CharacterAnimation[this.Student.PatrolAnim].time = 0;
		this.Student.PatrolTimer = 0;
	}

	void Succeed()
	{
		if (this.ID == 1)
		{
			this.Student.StudentManager.ShedDoor.Prompt.Label[0].text = "     " + "Open";
			this.Student.StudentManager.ShedDoor.Locked = false;
			this.Student.ClubManager.Padlock.SetActive(false);

			this.Student.Yandere.Inventory.ShedKey = true;
		}
		else
		{
			this.Student.StudentManager.CabinetDoor.Prompt.Label[0].text = "     " + "Open";
			this.Student.StudentManager.CabinetDoor.Locked = false;

			this.Student.Yandere.Inventory.CabinetKey = true;
		}
	}
}