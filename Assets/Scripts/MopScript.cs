using UnityEngine;

public class MopScript : MonoBehaviour
{
	public ParticleSystem Sparkles;

	public YandereScript Yandere;
	public PromptScript Prompt;
	public PickUpScript PickUp;

	public Collider HeadCollider;

	public Vector3 Rotation;

	public Renderer Blood;

	public Transform Head;

	public float Bloodiness = 0.0f;

	public bool Bleached;

	void Start()
	{
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();

		this.HeadCollider.enabled = false;

		this.UpdateBlood();
	}

	void Update()
	{
		if (this.PickUp.Clock.Period == 5)
		{
			this.PickUp.Suspicious = false;
		}
		else
		{
			this.PickUp.Suspicious = true;
		}

		if (!this.Prompt.PauseScreen.Show)
		{
			if (this.Yandere.PickUp == this.PickUp)
			{
				if (this.Prompt.HideButton[0])
				{
					this.Prompt.HideButton[0] = false;
					this.Prompt.HideButton[3] = true;

					this.Yandere.Mop = this;
				}

				if (this.Yandere.Bucket == null)
				{
					if (this.Bleached)
					{
						this.Prompt.HideButton[0] = false;

						if (this.Prompt.Button[0].color.a > 0.0f)
						{
							this.Prompt.Label[0].text = "     " + "Sweep";

							if (Input.GetButtonDown(InputNames.Xbox_A))
							{
								this.Yandere.Mopping = true;
								this.HeadCollider.enabled = true;
							}
						}
					}
					else
					{
						this.Prompt.Label[0].text = "     " + "Dip In Bucket First!";

						this.Prompt.HideButton[0] = false;
					}
				}
				else
				{
					if (this.Prompt.Button[0].color.a > 0.0f)
					{
						if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
						{
							if (this.Yandere.Bucket.Full)
							{
								if (!this.Yandere.Bucket.Gasoline)
								{
									if (this.Yandere.Bucket.Bleached)
									{
										if (this.Yandere.Bucket.Bloodiness < 100.0f)
										{
											this.Prompt.Label[0].text = "     " + "Dip";

											if (Input.GetButtonDown(InputNames.Xbox_A))
											{
                                                this.Dip();
											}
										}
										else
										{
											this.Prompt.Label[0].text = "     " + "Water Too Bloody!";
										}
									}
									else
									{
										this.Prompt.Label[0].text = "     " + "Add Bleach First!";
									}
								}
								else
								{
									this.Prompt.Label[0].text = "     " + "Can't Use Gasoline!";
								}
							}
							else
							{
								this.Prompt.Label[0].text = "     " + "Fill Bucket First!";
							}
						}
					}
				}

				if (this.Yandere.Mopping)
				{
					this.Head.LookAt(this.Head.position + Vector3.down);

					// [af] Changed Z value from 0 to 180 for Unity 5 (not sure why it's needed).
					this.Head.localEulerAngles = new Vector3(
						this.Head.localEulerAngles.x + 90.0f,
						this.Head.localEulerAngles.y,
						180.0f);
				}
				else
				{
					this.Rotation = Vector3.Lerp(
						this.Head.localEulerAngles, Vector3.zero, Time.deltaTime * 10.0f);

					this.Head.localEulerAngles = this.Rotation;
				}
			}
			else
			{
				//if (!this.Prompt.HideButton[0])
				//{
					this.Prompt.HideButton[0] = true;
					this.Prompt.HideButton[3] = false;

					if (this.Yandere.Mop == this)
					{
						this.Yandere.Mop = null;
					}
				//}
			}

			if (!this.Yandere.Mopping && this.HeadCollider.enabled)
			{
				this.HeadCollider.enabled = false;
			}
		}

		#if UNITY_EDITOR

		if (Input.GetKeyDown(KeyCode.F1))
		{
			foreach (Transform child in Yandere.Police.BloodParent)
			{
				Destroy(child.gameObject);
			}

			this.Yandere.Police.Corpses = 0;
			this.Yandere.NearBodies = 0;
		}

		#endif
	}

	public void UpdateBlood()
	{
		if (this.Bloodiness > 100.0f)
		{
			this.Bloodiness = 100.0f;
			this.Sparkles.Stop();
			this.Bleached = false;
		}

		this.Blood.material.color = new Color(
			this.Blood.material.color.r,
			this.Blood.material.color.g,
			this.Blood.material.color.b,
			(this.Bloodiness / 100.0f) * 0.90f);
	}

    public void Dip()
    {
        this.Yandere.YandereVision = false;
        this.Yandere.CanMove = false;
        this.Yandere.Dipping = true;

        this.Prompt.Hide();
        this.Prompt.enabled = false;
    }
}