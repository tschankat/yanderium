using UnityEngine;

public class StruggleBarScript : MonoBehaviour
{
	public ShoulderCameraScript ShoulderCamera;
	public PromptSwapScript ButtonPrompt;

	public UISprite[] ButtonPrompts;

	public YandereScript Yandere;
	public StudentScript Student;

	public Transform Spikes;

	public string CurrentButton = string.Empty;

	public bool Struggling = false;
	public bool Invincible = false;

	public float AttackTimer = 0.0f;
	public float ButtonTimer = 0.0f;
	public float Intensity = 0.0f;
	public float Strength = 1.0f;
	public float Struggle = 0.0f;
	public float Victory = 0.0f;

	public int ButtonID = 0;

	void Start()
	{
		this.transform.localScale = Vector3.zero;
		this.ChooseButton();
	}

	void Update()
	{
		if (this.Struggling)
		{
			this.Intensity = Mathf.MoveTowards(this.Intensity, 1.0f, Time.deltaTime);

			this.transform.localScale = Vector3.Lerp(
				this.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);

			this.Spikes.localEulerAngles = new Vector3(
				this.Spikes.localEulerAngles.x,
				this.Spikes.localEulerAngles.y,
				this.Spikes.localEulerAngles.z - (Time.deltaTime * 360.0f));

			this.Victory -= Time.deltaTime * 10.0f * this.Strength * this.Intensity;

			if (this.Yandere.Club == ClubType.MartialArts)
			{
				this.Victory = 100.0f;
			}

			if (Input.GetButtonDown(this.CurrentButton))
			{
				if (this.Invincible)
				{
					this.Victory += 100.0f;
				}

				this.Victory += Time.deltaTime * (500.0f + 
					((this.Yandere.Class.PhysicalGrade + this.Yandere.Class.PhysicalBonus) * 150.0f)) * this.Intensity;
			}

			if (this.Victory >= 100.0f)
			{
				this.Victory = 100.0f;
			}

			if (this.Victory <= -100.0f)
			{
				this.Victory = -100.0f;
			}

			UISprite buttonIDPrompt = this.ButtonPrompts[this.ButtonID];
			buttonIDPrompt.transform.localPosition = new Vector3(
				Mathf.Lerp(buttonIDPrompt.transform.localPosition.x, this.Victory * 6.50f, Time.deltaTime * 10.0f),
				buttonIDPrompt.transform.localPosition.y,
				buttonIDPrompt.transform.localPosition.z);

			this.Spikes.localPosition = new Vector3(
				buttonIDPrompt.transform.localPosition.x,
				this.Spikes.localPosition.y,
				this.Spikes.localPosition.z);

			if (this.Victory == 100.0f)
			{
				Debug.Log("Yandere-chan just won a struggle against " + this.Student.Name + ".");

				this.Yandere.Won = true;
				this.Student.Lost = true;

				this.Struggling = false;
				this.Victory = 0.0f;
			}
			else if (this.Victory == -100.0f)
			{
				if (!this.Invincible)
				{
					this.HeroWins();
				}
			}
			else
			{
				this.ButtonTimer += Time.deltaTime;

				if (this.ButtonTimer >= 1.0f)
				{
					this.ChooseButton();
					this.ButtonTimer = 0.0f;
					this.Intensity = 0;
				}
			}
		}
		else
		{
			if (transform.localScale.x > 0.10f)
			{
				transform.localScale = Vector3.Lerp(this.transform.localScale, Vector3.zero, Time.deltaTime * 10.0f);
			}
			else
			{
				transform.localScale = Vector3.zero;

				if (!Yandere.AttackManager.Censor)
				{
					gameObject.SetActive(false);
				}
				else
				{
					if (AttackTimer == 0)
					{
						Yandere.Blur.enabled = true;
						Yandere.Blur.blurSize = 0;
						Yandere.Blur.blurIterations = 0;
					}

					AttackTimer += Time.deltaTime;

					if (AttackTimer < 3 - .5f)
					{
						Yandere.Blur.blurSize = Mathf.MoveTowards(Yandere.Blur.blurSize, 10, Time.deltaTime * 10);

						if (Yandere.Blur.blurSize > Yandere.Blur.blurIterations)
						{
							Yandere.Blur.blurIterations++;
						}
					}
					else
					{
						Yandere.Blur.blurSize = Mathf.Lerp(Yandere.Blur.blurSize, 0, Time.deltaTime * 10);

						if (Yandere.Blur.blurSize < Yandere.Blur.blurIterations)
						{
							Yandere.Blur.blurIterations--;
						}

						if (AttackTimer >= 3)
						{
							gameObject.SetActive(false);

							Yandere.Blur.enabled = false;
							Yandere.Blur.blurSize = 0;
							Yandere.Blur.blurIterations = 0;

							AttackTimer = 0;
						}
					}
				}
			}
		}
	}

	public void HeroWins()
	{
		if (this.Yandere.Armed)
		{
			this.Yandere.EquippedWeapon.Drop();
		}
			
		this.Yandere.Lost = true;
		this.Student.Won = true;

		this.Struggling = false;
		this.Victory = 0.0f;

		this.Yandere.StudentManager.StopMoving();
	}

	void ChooseButton()
	{
		int PreviousButton = this.ButtonID;
		int ID = 1;

		while (ID < 5)
		{
			this.ButtonPrompts[ID].enabled = false;
			this.ButtonPrompts[ID].transform.localPosition = this.ButtonPrompts[PreviousButton].transform.localPosition;
			ID++;
		}

		while (this.ButtonID == PreviousButton)
		{
			this.ButtonID = Random.Range(1, 5);
		}

		if (this.ButtonID == 1)
		{
			this.CurrentButton = "A";
		}
		else if (this.ButtonID == 2)
		{
			this.CurrentButton = "B";
		}
		else if (this.ButtonID == 3)
		{
			this.CurrentButton = "X";
		}
		else if (this.ButtonID == 4)
		{
			this.CurrentButton = "Y";
		}

		this.ButtonPrompts[this.ButtonID].enabled = true;
	}
}
