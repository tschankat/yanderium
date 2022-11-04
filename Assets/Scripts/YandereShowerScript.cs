using UnityEngine;

public class YandereShowerScript : MonoBehaviour
{
	public SkinnedMeshRenderer Curtain;

	public GameObject CensorSteam;

	public YandereScript Yandere;
	public PromptScript Prompt;

	public Transform BatheSpot;

	public float OpenValue = 0.0f;
	public float Timer = 0.0f;

	public bool UpdateCurtain;
	public bool Open;

	public AudioSource MyAudio;

	public AudioClip CurtainClose;
	public AudioClip CurtainOpen;

	void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			if (this.Yandere.Schoolwear > 0 || this.Yandere.Chased || this.Yandere.Chasers > 0 || this.Yandere.Bloodiness == 0)
			{
				this.Prompt.Circle[0].fillAmount = 1;
			}
			else
			{
				AudioSource.PlayClipAtPoint(CurtainOpen, transform.position);

				this.CensorSteam.SetActive(true);

				this.MyAudio.Play();

				this.Yandere.EmptyHands();

				this.Yandere.YandereShower = this;
				this.Yandere.CanMove = false;
				this.Yandere.Bathing = true;

				this.UpdateCurtain = true;
				this.Open = true;

				this.Timer = 6;
			}
		}

		if (this.UpdateCurtain)
		{
			this.Timer = Mathf.MoveTowards(this.Timer, 0, Time.deltaTime);

			if (this.Timer < 1)
			{
				if (this.Open)
				{
					AudioSource.PlayClipAtPoint(CurtainClose, transform.position);
				}

				this.Open = false;

				if (this.Timer == 0)
				{
					this.CensorSteam.SetActive(false);
					this.UpdateCurtain = false;
				}
			}

			if (this.Open)
			{
				this.OpenValue = Mathf.Lerp(this.OpenValue, 0, Time.deltaTime * 10);

				this.Curtain.SetBlendShapeWeight(0, OpenValue);
			}
			else
			{
				this.OpenValue = Mathf.Lerp(this.OpenValue, 100, Time.deltaTime * 10);

				this.Curtain.SetBlendShapeWeight(0, OpenValue);
			}
		}
	}
}