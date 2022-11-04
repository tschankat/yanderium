using UnityEngine;

public class TitleSponsorScript : MonoBehaviour
{
	public InputManagerScript InputManager;

	public string[] SponsorURLs;
	public string[] Sponsors;

	public UILabel SponsorName;

	public Transform Highlight;

	public bool Show = false;

	public int Columns;
	public int Rows;

	private int Column = 0;
	private int Row = 0;

	public UISprite BlackSprite;
	public UISprite[] RedSprites;
	public UILabel[] Labels;

	void Start()
	{
		this.transform.localPosition = new Vector3(
			1050.0f,
			this.transform.localPosition.y,
			this.transform.localPosition.z);

		this.UpdateHighlight();

		if (GameGlobals.LoveSick)
		{
			this.TurnLoveSick();
		}
	}

	public int GetSponsorIndex()
	{
		return this.Column + (this.Row * this.Columns);
	}

	public bool SponsorHasWebsite(int index)
	{
		return !string.IsNullOrEmpty(this.SponsorURLs[index]);
	}

	void Update()
	{
		if (!this.Show)
		{
			this.transform.localPosition = new Vector3(
				Mathf.Lerp(this.transform.localPosition.x, 1050.0f, Time.deltaTime * 10.0f),
				this.transform.localPosition.y,
				this.transform.localPosition.z);
		}
		else
		{
			this.transform.localPosition = new Vector3(
				Mathf.Lerp(this.transform.localPosition.x, 0.0f, Time.deltaTime * 10.0f),
				this.transform.localPosition.y,
				this.transform.localPosition.z);

			if (this.InputManager.TappedUp)
			{
				this.Row = (this.Row > 0) ? (this.Row - 1) : (this.Rows - 1);
			}

			if (this.InputManager.TappedDown)
			{
				this.Row = (this.Row < (this.Rows - 1)) ? (this.Row + 1) : 0;
			}

			if (this.InputManager.TappedRight)
			{
				this.Column = (this.Column < (this.Columns - 1)) ? (this.Column + 1) : 0;
			}

			if (this.InputManager.TappedLeft)
			{
				this.Column = (this.Column > 0) ? (this.Column - 1) : (this.Columns - 1);
			}

			bool highlightChanged = this.InputManager.TappedUp ||
				this.InputManager.TappedDown || this.InputManager.TappedRight || 
				this.InputManager.TappedLeft;

			if (highlightChanged)
			{
				this.UpdateHighlight();
			}

			if (Input.GetButtonDown(InputNames.Xbox_A))
			{
				int sponsorIndex = this.GetSponsorIndex();

				if (this.SponsorHasWebsite(sponsorIndex))
				{
					Application.OpenURL(this.SponsorURLs[sponsorIndex]);
				}
			}
		}
	}

	void UpdateHighlight()
	{
		this.Highlight.localPosition = new Vector3(
			-384.0f + (this.Column * 256.0f),
			128.0f - (this.Row * 256.0f),
			this.Highlight.localPosition.z);
		
		this.SponsorName.text = this.Sponsors[this.GetSponsorIndex()];
	}

	void TurnLoveSick()
	{
		BlackSprite.color = Color.black;

		foreach (UISprite sprite in this.RedSprites)
		{
			sprite.color = new Color(1.0f, 0.0f, 0.0f, sprite.color.a);
		}
			
		foreach (UILabel label in this.Labels)
		{
			label.color = new Color(1.0f, 0.0f, 0.0f, label.color.a);
		}
	}
}
