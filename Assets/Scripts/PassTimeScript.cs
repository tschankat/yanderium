using UnityEngine;

public class PassTimeScript : MonoBehaviour
{
	public InputManagerScript InputManager;

	public ClockScript Clock;

	public UILabel TimeDisplay;

	public Transform Highlight;

	public float[] MinimumDigits;
	public float[] Digits;

	public int TargetTime = 0;
	public int Selected = 1;

	public string AMPM = "AM";

	void Update()
	{
		if (this.InputManager.TappedLeft || Input.GetKeyDown(KeyCode.A) || 
			Input.GetKeyDown(KeyCode.LeftArrow))
		{
			this.Selected--;

			if (this.Selected < 1)
			{
				this.Selected = 3;
			}

			this.UpdateHighlightPosition();
		}

		if (this.InputManager.TappedRight || Input.GetKeyDown(KeyCode.D) || 
			Input.GetKeyDown(KeyCode.RightArrow))
		{
			this.Selected++;

			if (this.Selected > 3)
			{
				this.Selected = 1;
			}

			this.UpdateHighlightPosition();
		}

		if (this.InputManager.TappedUp || Input.GetKeyDown(KeyCode.W) || 
			Input.GetKeyDown(KeyCode.UpArrow))
		{
			this.UpdateTime(1);
		}

		if (this.InputManager.TappedDown || Input.GetKeyDown(KeyCode.S) || 
			Input.GetKeyDown(KeyCode.DownArrow))
		{
			this.UpdateTime(-1);
		}
	}

	void UpdateHighlightPosition()
	{
		if (this.Selected == 1)
		{
			this.Highlight.localPosition = new Vector3(
				-130.0f,
				this.Highlight.localPosition.y,
				this.Highlight.localPosition.z);
		}
		else if (this.Selected == 2)
		{
			this.Highlight.localPosition = new Vector3(
				-40.0f,
				this.Highlight.localPosition.y,
				this.Highlight.localPosition.z);
		}
		else if (this.Selected == 3)
		{
			this.Highlight.localPosition = new Vector3(
				15.0f,
				this.Highlight.localPosition.y,
				this.Highlight.localPosition.z);
		}
	}

	public void GetCurrentTime()
	{
		this.Digits[1] = this.Clock.Hour;

		if (this.Clock.Minute < 9.0f)
		{
			this.Digits[2] = 0.0f;

			this.Digits[3] = this.Clock.Minute;
		}
		else
		{
			this.Digits[2] = this.Clock.Minute * 0.10f;
			this.Digits[2] = Mathf.Floor(this.Digits[2]);

			this.Digits[3] = this.Clock.Minute - (this.Digits[2] * 10.0f);
		}

		this.MinimumDigits[1] = this.Digits[1];
		this.MinimumDigits[2] = this.Digits[2];
		this.MinimumDigits[3] = this.Digits[3];

		this.UpdateTime(0);
	}

	void UpdateTime(int Increment)
	{
		this.Digits[this.Selected] += Increment;

		if (this.Selected == 1)
		{
			if (this.Digits[1] < this.MinimumDigits[1])
			{
				this.Digits[1] = this.MinimumDigits[1];
			}
			else if (this.Digits[1] > 17.0f)
			{
				this.Digits[1] = 17.0f;
			}

			if (this.Digits[1] == this.MinimumDigits[1])
			{
				if (this.Digits[2] < this.MinimumDigits[2])
				{
					this.Digits[2] = this.MinimumDigits[2];
				}

				if (this.Digits[2] == this.MinimumDigits[2])
				{
					if (this.Digits[3] < this.MinimumDigits[3])
					{
						this.Digits[3] = this.MinimumDigits[3];
					}
				}
			}
		}
		else if (this.Selected == 2)
		{
			if (this.Digits[1] == this.MinimumDigits[1])
			{
				if (this.Digits[2] < this.MinimumDigits[2])
				{
					this.Digits[2] = this.MinimumDigits[2];
				}
				else if (this.Digits[2] > 5.0f)
				{
					this.Digits[2] = this.MinimumDigits[2];
				}

				if (this.Digits[2] == this.MinimumDigits[2])
				{
					if (this.Digits[3] < this.MinimumDigits[3])
					{
						this.Digits[3] = this.MinimumDigits[3];
					}
				}
			}
			else
			{
				if (this.Digits[2] < 0.0f)
				{
					this.Digits[2] = 5.0f;
				}
				else if (this.Digits[2] > 5.0f)
				{
					this.Digits[2] = 0.0f;
				}
			}
		}
		else if (this.Selected == 3)
		{
			if ((this.Digits[1] == this.MinimumDigits[1]) &&
				(this.Digits[2] == this.MinimumDigits[2]))
			{
				if (this.Digits[3] < this.MinimumDigits[3])
				{
					this.Digits[3] = this.MinimumDigits[3];
				}
				else if (this.Digits[3] > 9.0f)
				{
					this.Digits[3] = this.MinimumDigits[3];
				}
			}
			else
			{
				if (this.Digits[3] < 0.0f)
				{
					this.Digits[3] = 9.0f;
				}
				else if (this.Digits[3] > 9.0f)
				{
					this.Digits[3] = 0.0f;
				}
			}
		}

		if (this.Digits[1] < 12.0f)
		{
			this.AMPM = " AM";
		}
		else
		{
			this.AMPM = " PM";
		}

		if (this.Digits[1] < 10.0f)
		{
			this.TimeDisplay.text = "0" + this.Digits[1] + ":" + this.Digits[2] + this.Digits[3] + this.AMPM;
		}
		else if (this.Digits[1] < 13.0f)
		{
			this.TimeDisplay.text = this.Digits[1] + ":" + this.Digits[2] + this.Digits[3] + this.AMPM;
		}
		else
		{
			this.TimeDisplay.text = "0" + (this.Digits[1] - 12.0f) + ":" + this.Digits[2] + this.Digits[3] + this.AMPM;
		}

		this.TargetTime = (int)((this.Digits[1] * 60.0f) + (this.Digits[2] * 10.0f) + this.Digits[3]);
	}
}
