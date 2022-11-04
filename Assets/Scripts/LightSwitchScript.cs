using UnityEngine;

public class LightSwitchScript : MonoBehaviour
{
	public ToiletEventScript ToiletEvent;
	public YandereScript Yandere;
	public PromptScript Prompt;

	public Transform ElectrocutionSpot;
	public GameObject BathroomLight;
	public GameObject Electricity;
	public Rigidbody Panel;
	public Transform Wires;

	public AudioClip[] ReactionClips;
	public string[] ReactionTexts;

	public AudioClip[] Flick;

	public float SubtitleTimer = 0.0f;
	public float FlickerTimer = 0.0f;

	public int ReactionID = 0;

	public bool Flicker = false;

	void Start()
	{
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();
	}

	void Update()
	{
		if (this.Flicker)
		{
			this.FlickerTimer += Time.deltaTime;

			if (this.FlickerTimer > 0.10f)
			{
				this.FlickerTimer = 0.0f;

				// [af] Replaced if/else statement with boolean expression.
				this.BathroomLight.SetActive(!this.BathroomLight.activeInHierarchy);
			}
		}

		if (!this.Panel.useGravity)
		{
			if (this.Yandere.Armed)
			{
				// [af] Replaced if/else statement with boolean expression.
				this.Prompt.HideButton[3] = this.Yandere.EquippedWeapon.WeaponID != 6;
			}
			else
			{
				this.Prompt.HideButton[3] = true;
			}
		}

		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Prompt.Circle[0].fillAmount = 1.0f;

			AudioSource audioSource = this.GetComponent<AudioSource>();

			if (this.BathroomLight.activeInHierarchy)
			{
				this.Prompt.Label[0].text = "     " + "Turn On";
				this.BathroomLight.SetActive(false);
				audioSource.clip = this.Flick[1];
				audioSource.Play();

				if (this.ToiletEvent.EventActive)
				{
					if ((this.ToiletEvent.EventPhase == 2) ||
						(this.ToiletEvent.EventPhase == 3))
					{
						this.ReactionID = Random.Range(1, 4);

						AudioClipPlayer.Play(this.ReactionClips[this.ReactionID],
							this.ToiletEvent.EventStudent.transform.position,
							5.0f, 10.0f, out this.ToiletEvent.VoiceClip);

						this.ToiletEvent.EventSubtitle.text = this.ReactionTexts[this.ReactionID];

						this.SubtitleTimer += Time.deltaTime;
					}
				}
			}
			else
			{
				this.Prompt.Label[0].text = "     " + "Turn Off";
				this.BathroomLight.SetActive(true);
				audioSource.clip = this.Flick[0];
				audioSource.Play();
			}
		}

		if (this.SubtitleTimer > 0.0f)
		{
			this.SubtitleTimer += Time.deltaTime;

			if (this.SubtitleTimer > 3.0f)
			{
				this.ToiletEvent.EventSubtitle.text = string.Empty;
				this.SubtitleTimer = 0.0f;
			}
		}

		if (this.Prompt.Circle[3].fillAmount == 0.0f)
		{
			this.Prompt.HideButton[3] = true;

			this.Wires.localScale = new Vector3(
				this.Wires.localScale.x,
				this.Wires.localScale.y,
				1.0f);

			this.Panel.useGravity = true;
			this.Panel.AddForce(0.0f, 0.0f, 10.0f);
		}
	}
}
