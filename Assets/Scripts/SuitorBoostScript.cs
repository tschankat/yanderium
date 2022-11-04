using UnityEngine;

public class SuitorBoostScript : MonoBehaviour
{
	public LoveManagerScript LoveManager;
	public PromptBarScript PromptBar;
	public YandereScript Yandere;
	public PromptScript Prompt;

	public UISprite Darkness;

	public Transform YandereSitSpot;
	public Transform SuitorSitSpot;

	public Transform YandereChair;
	public Transform SuitorChair;

	public Transform YandereSpot;
	public Transform SuitorSpot;

	public Transform LookTarget;
	public Transform TextBox;

	public bool Boosting = false;
	public bool FadeOut = false;

	public float Timer = 0.0f;

	public int Phase = 1;

	void Update()
	{
		if (this.Yandere.Followers > 0)
		{
			if (this.Yandere.Follower.StudentID == this.LoveManager.SuitorID && this.Yandere.Follower.DistanceToPlayer < 2)
			{
				this.Prompt.enabled = true;
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

		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Prompt.Circle[0].fillAmount = 1;

			if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
				this.Yandere.Character.GetComponent<Animation>().CrossFade(this.Yandere.IdleAnim);

				this.Yandere.Follower.CharacterAnimation.CrossFade(this.Yandere.Follower.IdleAnim);
				this.Yandere.Follower.Pathfinding.canSearch = false;
				this.Yandere.Follower.Pathfinding.canMove = false;
				this.Yandere.Follower.enabled = false;

				this.Yandere.RPGCamera.enabled = false;
				this.Darkness.enabled = true;
				this.Yandere.CanMove = false;
				this.Boosting = true;
				this.FadeOut = true;
			}
		}

		if (this.Boosting)
		{
			if (this.FadeOut)
			{
				this.Darkness.color = new Color(
					this.Darkness.color.r,
					this.Darkness.color.g,
					this.Darkness.color.b,
					Mathf.MoveTowards(this.Darkness.color.a, 1.0f, Time.deltaTime));

				if (this.Darkness.color.a == 1.0f)
				{
					this.Timer += Time.deltaTime;

					if (this.Timer > 1.0f)
					{
						if (this.Phase == 1)
						{
							Camera.main.transform.position = new Vector3(-26.0f, 5.30f, 17.50f);
							Camera.main.transform.eulerAngles = new Vector3(15.0f, 180.0f, 0.0f);

							this.Yandere.Follower.Character.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

							this.YandereChair.transform.localPosition = new Vector3(
								this.YandereChair.transform.localPosition.x,
								this.YandereChair.transform.localPosition.y,
								-0.60f);

							this.SuitorChair.transform.localPosition = new Vector3(
								this.SuitorChair.transform.localPosition.x,
								this.SuitorChair.transform.localPosition.y,
								-0.60f);

							this.Yandere.Character.GetComponent<Animation>().Play(AnimNames.FemaleSit01);
							this.Yandere.Follower.Character.GetComponent<Animation>().Play(AnimNames.MaleSit01);

							this.Yandere.transform.eulerAngles = Vector3.zero;
							this.Yandere.Follower.transform.eulerAngles = Vector3.zero;

							this.Yandere.transform.position = this.YandereSitSpot.position;
							this.Yandere.Follower.transform.position = this.SuitorSitSpot.position;
						}
						else
						{
							this.Yandere.FixCamera();

							this.Yandere.Follower.Character.transform.localScale = new Vector3(0.94f, 0.94f, 0.94f);

							this.YandereChair.transform.localPosition = new Vector3(
								this.YandereChair.transform.localPosition.x,
								this.YandereChair.transform.localPosition.y,
								-1.0f / 3.0f);

							this.SuitorChair.transform.localPosition = new Vector3(
								this.SuitorChair.transform.localPosition.x,
								this.SuitorChair.transform.localPosition.y,
								-1.0f / 3.0f);

							this.Yandere.Character.GetComponent<Animation>().Play(this.Yandere.IdleAnim);
							this.Yandere.Follower.Character.GetComponent<Animation>().Play(this.Yandere.Follower.IdleAnim);

							this.Yandere.transform.position = this.YandereSpot.position;
							this.Yandere.Follower.transform.position = this.SuitorSpot.position;
						}

						this.PromptBar.ClearButtons();
						this.FadeOut = false;
						this.Phase++;
					}
				}
			}
			else
			{
				this.Darkness.color = new Color(
					this.Darkness.color.r,
					this.Darkness.color.g,
					this.Darkness.color.b,
					Mathf.MoveTowards(this.Darkness.color.a, 0.0f, Time.deltaTime));

				if (this.Darkness.color.a == 0.0f)
				{
					if (this.Phase == 2)
					{
						// [af] Added "gameObject" for C# compatibility.
						this.TextBox.gameObject.SetActive(true);

						this.TextBox.localScale = Vector3.Lerp(
							this.TextBox.localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);

						if (this.TextBox.localScale.x > 0.90f)
						{
							if (!this.PromptBar.Show)
							{
								this.PromptBar.ClearButtons();
								this.PromptBar.Label[0].text = "Continue";
								this.PromptBar.UpdateButtons();
								this.PromptBar.Show = true;
							}

							if (Input.GetButtonDown(InputNames.Xbox_A))
							{
								this.PromptBar.Show = false;
								this.Phase++;
							}
						}
					}
					else if (this.Phase == 3)
					{
						if (this.TextBox.localScale.x > 0.10f)
						{
							this.TextBox.localScale = Vector3.Lerp(
								this.TextBox.localScale, Vector3.zero, Time.deltaTime * 10.0f);
						}
						else
						{
							// [af] Added "gameObject" for C# compatibility.
							this.TextBox.gameObject.SetActive(false);

							this.FadeOut = true;
							this.Phase++;
						}
					}
					else if (this.Phase == 5)
					{
						DatingGlobals.SetSuitorTrait(2, DatingGlobals.GetSuitorTrait(2) + 1);

						this.Yandere.RPGCamera.enabled = true;
						this.Darkness.enabled = false;
						this.Yandere.CanMove = true;
						this.Boosting = false;

						this.Yandere.Follower.Pathfinding.canSearch = true;
						this.Yandere.Follower.Pathfinding.canMove = true;
						this.Yandere.Follower.enabled = true;

						this.Prompt.Hide();
						this.Prompt.enabled = false;

						this.enabled = false;
					}
				}
			}
		}
	}

	void LateUpdate()
	{
		if (this.Boosting)
		{
			if ((this.Phase > 1) && (this.Phase < 5))
			{
				this.Yandere.Head.LookAt(this.LookTarget);
				this.Yandere.Follower.Head.LookAt(this.LookTarget);
			}
		}
	}
}
