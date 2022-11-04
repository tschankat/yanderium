using UnityEngine;

public class MatchboxScript : MonoBehaviour
{
	public YandereScript Yandere;
	public PromptScript Prompt;
	public PickUpScript PickUp;

	public GameObject Match;

	void Start()
	{
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();
	}

	void Update()
	{
		if (!this.Prompt.PauseScreen.Show)
		{
			if (this.Yandere.PickUp == this.PickUp)
			{
				if (this.Prompt.HideButton[0])
				{
					this.Yandere.Arc.SetActive(true);

					this.Prompt.HideButton[0] = false;
					this.Prompt.HideButton[3] = true;
				}

				if (this.Prompt.Circle[0].fillAmount == 0.0f)
				{
					this.Prompt.Circle[0].fillAmount = 1.0f;

					if (!this.Yandere.Chased && this.Yandere.Chasers == 0 && this.Yandere.CanMove)
					{
						if (!this.Yandere.Flicking)
						{
							GameObject NewMatch = Instantiate(this.Match,
								this.transform.position, Quaternion.identity);
							NewMatch.transform.parent = this.Yandere.ItemParent;
							NewMatch.transform.localPosition = new Vector3(0.0159f, 0.0043f, 0.0152f);
							NewMatch.transform.localEulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
							NewMatch.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
							this.Yandere.Match = NewMatch;

							this.Yandere.Character.GetComponent<Animation>().CrossFade(
								AnimNames.FemaleFlickingMatch);

							this.Yandere.YandereVision = false;
							this.Yandere.Arc.SetActive(false);
							this.Yandere.Flicking = true;
							this.Yandere.CanMove = false;

							this.Prompt.Hide();
							this.Prompt.enabled = false;
						}
					}
				}
			}
			else
			{
				if (!this.Prompt.HideButton[0])
				{
					this.Yandere.Arc.SetActive(false);

					this.Prompt.HideButton[0] = true;
					this.Prompt.HideButton[3] = false;
				}
			}
		}
	}
}
