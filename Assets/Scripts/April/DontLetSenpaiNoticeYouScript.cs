using UnityEngine;

public class DontLetSenpaiNoticeYouScript : MonoBehaviour
{
	public UILabel[] Letters;

	public Vector3[] Origins;

	public AudioClip Slam;

	public bool Proceed = false;

	public int ShakeID = 0;
	public int ID = 0;

	void Start()
	{
		// [af] Converted while loop to for loop.
		for (; this.ID < this.Letters.Length; this.ID++)
		{
			UILabel letter = this.Letters[this.ID];
			letter.transform.localScale = new Vector3(10.0f, 10.0f, 1.0f);
			letter.color = new Color(
				letter.color.r,
				letter.color.g,
				letter.color.b,
				0.0f);

			this.Origins[this.ID] = letter.transform.localPosition;
		}

		this.ID = 0;
	}

	void Update()
	{
		if (Input.GetButtonDown(InputNames.Xbox_A))
		{
			this.Proceed = true;
		}

		if (this.Proceed)
		{
			if (this.ID < this.Letters.Length)
			{
				UILabel letter = this.Letters[this.ID];
				letter.transform.localScale = Vector3.MoveTowards(
					letter.transform.localScale, Vector3.one, Time.deltaTime * 100.0f);

				letter.color = new Color(
					letter.color.r,
					letter.color.g,
					letter.color.b,
					letter.color.a + (Time.deltaTime * 10.0f));

				if (letter.transform.localScale == Vector3.one)
				{
					this.GetComponent<AudioSource>().PlayOneShot(this.Slam);
					this.ID++;
				}
			}

			// [af] Converted while loop to for loop.
			for (this.ShakeID = 0; this.ShakeID < this.Letters.Length; this.ShakeID++)
			{
				UILabel letter = this.Letters[this.ShakeID];
				Vector3 origin = this.Origins[this.ShakeID];
				letter.transform.localPosition = new Vector3(
					origin.x + Random.Range(-5.0f, 5.0f),
					origin.y + Random.Range(-5.0f, 5.0f),
					letter.transform.localPosition.z);
			}
		}
	}
}
