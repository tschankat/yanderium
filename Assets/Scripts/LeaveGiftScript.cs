using UnityEngine;

public class LeaveGiftScript : MonoBehaviour
{
	public EndOfDayScript EndOfDay;

	public PromptScript Prompt;

	public GameObject Box;

	void Start()
	{
		this.Box.SetActive(false);

		//Debug.Log("CollectibleGlobals.SenpaiGifts is: " + CollectibleGlobals.SenpaiGifts);
		//Debug.Log("CollectibleGlobals.MatchmakingGifts is: " + CollectibleGlobals.MatchmakingGifts);

		EndOfDay.SenpaiGifts = CollectibleGlobals.SenpaiGifts;

		if (EndOfDay.SenpaiGifts == 0)
		{
			Prompt.Hide();
			Prompt.enabled = false;

			enabled = false;
		}
	}

	void Update()
	{
		//Debug.Log(Vector3.Distance(this.Prompt.Yandere.transform.position, this.Prompt.Yandere.Senpai.position));

		if (this.Prompt.InView)
		{
			if (Vector3.Distance(this.Prompt.Yandere.transform.position,
				this.Prompt.Yandere.Senpai.position) > 10)
			{
				if (this.Prompt.Circle[0].fillAmount == 0.0f)
				{
					EndOfDay.SenpaiGifts--;

					this.Prompt.Hide();
					this.Prompt.enabled = false;

					this.Box.SetActive(true);
					this.enabled = false;
				}
			}
			else
			{
				this.Prompt.Hide();
			}
		}
	}
}