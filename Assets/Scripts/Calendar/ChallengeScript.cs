using UnityEngine;

public class ChallengeScript : MonoBehaviour
{
	public InputManagerScript InputManager;
	public CalendarScript Calendar;
	public GameObject ViewButton;
	public Transform Arrows;

	public Transform[] ChallengeList;
	public int[] Challenges;
	public UIPanel[] Panels;

	public UIPanel ChallengePanel;
	public UIPanel CalendarPanel;
	public UITexture LargeIcon;
	public UISprite Shadow;

	public bool Viewing = false;
	public bool Switch = false;

	public int Phase = 1;
	public int List = 0;

	void Update()
	{
		if (!this.Viewing)
		{
			if (!this.Switch)
			{
				if (this.InputManager.TappedUp || this.InputManager.TappedDown)
				{
					if (this.List == 0)
					{
						this.Arrows.localPosition = new Vector3(
							this.Arrows.localPosition.x,
							-300.0f,
							this.Arrows.localPosition.z);

						this.ViewButton.SetActive(true);
						this.Panels[0].alpha = 0.50f;
						this.Panels[1].alpha = 1.0f;
						this.List = 1;
					}
					else
					{
						this.Arrows.localPosition = new Vector3(
							this.Arrows.localPosition.x,
							200.0f,
							this.Arrows.localPosition.z);

						this.ViewButton.SetActive(false);
						this.Panels[0].alpha = 1.0f;
						this.Panels[1].alpha = 0.50f;
						this.List = 0;
					}
				}

				Transform listChallenge = this.ChallengeList[this.List];

				if (this.InputManager.DPadRight || Input.GetKey(KeyCode.RightArrow))
				{
					listChallenge.localPosition = new Vector3(
						listChallenge.localPosition.x - (Time.deltaTime * 1000.0f),
						listChallenge.localPosition.y,
						listChallenge.localPosition.z);
				}

				if (this.InputManager.DPadLeft || Input.GetKey(KeyCode.LeftArrow))
				{
					listChallenge.localPosition = new Vector3(
						listChallenge.localPosition.x + (Time.deltaTime * 1000.0f),
						listChallenge.localPosition.y,
						listChallenge.localPosition.z);
				}

				listChallenge.localPosition = new Vector3(
					listChallenge.localPosition.x + (Input.GetAxis("Horizontal") * -10.0f),
					listChallenge.localPosition.y,
					listChallenge.localPosition.z);

				if (listChallenge.localPosition.x > 500.0f)
				{
					listChallenge.localPosition = new Vector3(
						500.0f,
						listChallenge.localPosition.y,
						listChallenge.localPosition.z);
				}
				else if (listChallenge.localPosition.x <
					(-250.0f * (this.Challenges[this.List] - 3.0f)))
				{
					listChallenge.localPosition = new Vector3(
						-250.0f * (this.Challenges[this.List] - 3.0f),
						listChallenge.localPosition.y,
						listChallenge.localPosition.z);
				}

				if (this.LargeIcon.color.a > 0.0f)
				{
					this.LargeIcon.color = new Color(
						this.LargeIcon.color.r,
						this.LargeIcon.color.g,
						this.LargeIcon.color.b,
						this.LargeIcon.color.a - (Time.deltaTime * 10.0f));

					if (this.LargeIcon.color.a < 0.0f)
					{
						this.LargeIcon.color = new Color(
							this.LargeIcon.color.r,
							this.LargeIcon.color.g,
							this.LargeIcon.color.b,
							0.0f);
					}
				}
			}
		}
		else
		{
			if (this.LargeIcon.color.a < 1.0f)
			{
				this.LargeIcon.color = new Color(
					this.LargeIcon.color.r,
					this.LargeIcon.color.g,
					this.LargeIcon.color.b,
					this.LargeIcon.color.a + (Time.deltaTime * 10.0f));

				if (this.LargeIcon.color.a > 1.0f)
				{
					this.LargeIcon.color = new Color(
						this.LargeIcon.color.r,
						this.LargeIcon.color.g,
						this.LargeIcon.color.b,
						1.0f);
				}
			}
		}

		this.Shadow.color = new Color(
			this.Shadow.color.r,
			this.Shadow.color.g,
			this.Shadow.color.b,
			this.LargeIcon.color.a * 0.75f);

		if (!this.Switch)
		{
			if (Input.GetButtonDown(InputNames.Xbox_A))
			{
				if (this.List == 1)
				{
					//if (this.ChallengeList[this.List].localPosition.x > -2375.0f)
					//{
						this.Viewing = true;
					//}
				}
			}
		}

		if (Input.GetButtonDown(InputNames.Xbox_B))
		{
			if (this.Viewing)
			{
				this.Viewing = false;
			}
			else
			{
				this.Switch = true;
			}
		}

		if (this.Switch)
		{
			if (this.Phase == 1)
			{
				this.ChallengePanel.alpha -= Time.deltaTime;

				if (this.ChallengePanel.alpha <= 0.0f)
				{
					this.Phase++;
				}
			}
			else
			{
				this.CalendarPanel.alpha += Time.deltaTime;

				if (this.CalendarPanel.alpha >= 1.0f)
				{
					this.Calendar.enabled = true;
					this.enabled = false;
					this.Switch = false;
					this.Phase = 1;
				}
			}
		}
	}
}
