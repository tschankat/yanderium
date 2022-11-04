using UnityEngine;

public class DemonScript : MonoBehaviour
{
	public SkinnedMeshRenderer Face;

	public YandereScript Yandere;
	public PromptScript Prompt;

	public UILabel DemonSubtitle;
	public UISprite Darkness;
	public UISprite Button;

	public AudioClip MouthOpen;
	public AudioClip MouthClose;

	public AudioClip[] Clips;
	public string[] Lines;

	public bool Communing = false;
	public bool Open = false;

	public float Intensity = 1.0f;
	public float Value = 0.0f;

	public Color MyColor;

	public int DemonID = 0;
	public int Phase = 1;
	public int ID = 0;

	void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Yandere.Character.GetComponent<Animation>().CrossFade(this.Yandere.IdleAnim);
			this.Yandere.CanMove = false;
			this.Communing = true;
		}

		if (this.DemonID == 1)
		{
			if (Vector3.Distance(this.Yandere.transform.position, transform.position) < 2.5)
			{
				if (!Open)
				{
					AudioSource.PlayClipAtPoint(MouthOpen, transform.position);
				}

				Open = true;
			}
			else
			{
				if (Open)
				{
					AudioSource.PlayClipAtPoint(MouthClose, transform.position);
				}

				Open = false;
			}

			if (Open)
			{
				Value = Mathf.Lerp(Value, 100.0f, Time.deltaTime * 10);
			}
			else
			{
				Value = Mathf.Lerp(Value, 0.0f, Time.deltaTime * 10);
			}

			Face.SetBlendShapeWeight(0, Value);
		}

		if (this.Communing)
		{
			AudioSource audioSource = this.GetComponent<AudioSource>();

			if (this.Phase == 1)
			{
				this.Darkness.color = new Color(
					this.Darkness.color.r,
					this.Darkness.color.g,
					this.Darkness.color.b,
					Mathf.MoveTowards(this.Darkness.color.a, 1.0f, Time.deltaTime));

				if (this.Darkness.color.a == 1.0f)
				{
					this.DemonSubtitle.transform.localPosition = Vector3.zero;
					this.DemonSubtitle.text = this.Lines[this.ID];
					this.DemonSubtitle.color = this.MyColor;

					this.DemonSubtitle.color = new Color(
						this.DemonSubtitle.color.r,
						this.DemonSubtitle.color.g,
						this.DemonSubtitle.color.b,
						0.0f);

					this.Phase++;

					if (this.Clips[this.ID] != null)
					{
						audioSource.clip = this.Clips[this.ID];
						audioSource.Play();
					}
				}
			}
			else if (this.Phase == 2)
			{
				Debug.Log("Demon Phase is 2.");

				this.DemonSubtitle.transform.localPosition = new Vector3(
					Random.Range(-this.Intensity, this.Intensity),
					Random.Range(-this.Intensity, this.Intensity),
					Random.Range(-this.Intensity, this.Intensity));

				this.DemonSubtitle.color = new Color(
					this.DemonSubtitle.color.r,
					this.DemonSubtitle.color.g,
					this.DemonSubtitle.color.b,
					Mathf.MoveTowards(this.DemonSubtitle.color.a, 1.0f, Time.deltaTime ));

				this.Button.color = new Color(
					this.Button.color.r,
					this.Button.color.g,
					this.Button.color.b,
					Mathf.MoveTowards(this.Button.color.a, 1.0f, Time.deltaTime));

				if (this.DemonSubtitle.color.a > .9f)
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.Phase++;
					}
				}
			}
			else if (this.Phase == 3)
			{
				this.DemonSubtitle.transform.localPosition = new Vector3(
					Random.Range(-this.Intensity, this.Intensity),
					Random.Range(-this.Intensity, this.Intensity),
					Random.Range(-this.Intensity, this.Intensity));

				this.DemonSubtitle.color = new Color(
					this.DemonSubtitle.color.r,
					this.DemonSubtitle.color.g,
					this.DemonSubtitle.color.b,
					Mathf.MoveTowards(this.DemonSubtitle.color.a, 0.0f, Time.deltaTime));

				if (this.DemonSubtitle.color.a == 0.0f)
				{
					this.ID++;

					if (this.ID < this.Lines.Length)
					{
						this.Phase--;
						this.DemonSubtitle.text = this.Lines[this.ID];

						if (this.Clips[this.ID] != null)
						{
							audioSource.clip = this.Clips[this.ID];
							audioSource.Play();
						}
					}
					else
					{
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

				this.Button.color = new Color(
					this.Button.color.r,
					this.Button.color.g,
					this.Button.color.b,
					Mathf.MoveTowards(this.Button.color.a, 0.0f, Time.deltaTime));

				if (this.Darkness.color.a == 0.0f)
				{
					this.Yandere.CanMove = true;
					this.Communing = false;
					this.Phase = 1;
					this.ID = 0;

					SchoolGlobals.SetDemonActive(this.DemonID, true);
					StudentGlobals.FemaleUniform = 1;
					StudentGlobals.MaleUniform = 1;
					GameGlobals.Paranormal = true;
				}
			}
		}
	}
}