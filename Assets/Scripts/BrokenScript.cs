using UnityEngine;

public class BrokenScript : MonoBehaviour
{
	public DynamicBone[] HairPhysics;
	public string[] MutterTexts;
	public AudioClip[] Mutters;

	public Vector3 PermanentAngleR;
	public Vector3 PermanentAngleL;

	public Transform TwintailR;
	public Transform TwintailL;

	public AudioClip KillKillKill;
	public AudioClip Stab;
	public AudioClip DoIt;

	public GameObject VoiceClip;
	public GameObject Yandere;

	public UILabel Subtitle;

	public AudioSource MyAudio;

	public bool Hunting = false;
	public bool Stabbed = false;
	public bool Began = false;
	public bool Done = false;

	public float SuicideTimer = 0.0f;
	public float Timer = 0.0f;

	public int ID = 1;

	void Start()
	{
		this.HairPhysics[0].enabled = false;
		this.HairPhysics[1].enabled = false;

		this.PermanentAngleR = TwintailR.eulerAngles;
		this.PermanentAngleL = TwintailL.eulerAngles;

		this.Subtitle = GameObject.Find("EventSubtitle").GetComponent<UILabel>();
		this.Yandere = GameObject.Find("YandereChan");
	}

	void Update()
	{
		if (!this.Done)
		{
			float Distance = Vector3.Distance(
				this.Yandere.transform.position, this.transform.root.position);

			if (Distance < 6.0f)
			{
				if (Distance < 5.0f)
				{
					if (!this.Hunting)
					{
						this.Timer += Time.deltaTime;

						if (this.VoiceClip == null)
						{
							this.Subtitle.text = "";
						}

						if (this.Timer > 5.0f)
						{
							this.Timer = 0.0f;

							this.Subtitle.text = this.MutterTexts[this.ID];

							AudioClipPlayer.PlayAttached(this.Mutters[this.ID],
								this.transform.position, this.transform, 1.0f, 5.0f,
								out this.VoiceClip, this.Yandere.transform.position.y);

							this.ID++;

							// [af] Is the first clip (at index 0) ever played?
							if (this.ID == this.Mutters.Length)
							{
								this.ID = 1;
							}
						}
					}
					else
					{
						if (!this.Began)
						{
							if (this.VoiceClip != null)
							{
								Destroy(this.VoiceClip);
							}

							this.Subtitle.text = "Do it.";
							AudioClipPlayer.PlayAttached(this.DoIt, this.transform.position,
								this.transform, 1.0f, 5.0f, out this.VoiceClip,
								this.Yandere.transform.position.y);

							this.Began = true;
						}
						else
						{
							if (this.VoiceClip == null)
							{
								this.Subtitle.text = "...kill...kill...kill...";
								AudioClipPlayer.PlayAttached(this.KillKillKill,
									this.transform.position, this.transform, 1.0f, 5.0f,
									out this.VoiceClip, this.Yandere.transform.position.y);
							}
						}
					}

					float Scale = Mathf.Abs((Distance - 5.0f) * 0.20f);
					Scale = (Scale > 1.0f) ? 1.0f : Scale;

					this.Subtitle.transform.localScale = new Vector3(Scale, Scale, Scale);
				}
				else
				{
					this.Subtitle.transform.localScale = Vector3.zero;
				}
			}
		}

		Vector3 rightAngles = this.TwintailR.eulerAngles;
		Vector3 leftAngles = this.TwintailL.eulerAngles;
		rightAngles.x = this.PermanentAngleR.x;
		rightAngles.z = this.PermanentAngleR.z;
		leftAngles.x = this.PermanentAngleL.x;
		leftAngles.z = this.PermanentAngleL.z;
		this.TwintailR.eulerAngles = rightAngles;
		this.TwintailL.eulerAngles = leftAngles;
	}
}
