using UnityEngine;

public class ReputationScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public ArmDetectorScript ArmDetector;
	public PortalScript Portal;

	public Transform CurrentRepMarker;
	public Transform PendingRepMarker;

	public UILabel PendingRepLabel;

	public ClockScript Clock;

	public float Reputation = 0.0f;

	public float PendingRep = 0.0f;

	public int CheckedRep = 1;
	public int Phase = 0;

	public bool MissionMode = false;

	void Start()
	{
		if (MissionModeGlobals.MissionMode)
		{
			this.MissionMode = true;
		}

		this.Reputation = PlayerGlobals.Reputation;

		this.Bully();
	}

	void Update()
	{
		// Currently, it is uncertain whether or not the
		// following code will actually need to be used.
		// A judgement will be made at a future time.

		if (this.Phase == 1)
		{
			if ((this.Clock.PresentTime / 60.0f) > 8.50f)
			{
				this.Phase++;
			}
		}
		else if (this.Phase == 2)
		{
			if ((this.Clock.PresentTime / 60.0f) > 13.50f)
			{
				this.Phase++;
			}
		}
		else if (this.Phase == 3)
		{
			if ((this.Clock.PresentTime / 60.0f) > 18.0f)
			{
				this.Phase++;
			}
		}

		if (this.PendingRep < 0)
		{
			this.StudentManager.TutorialWindow.ShowRepMessage = true;
		}

		if (this.CheckedRep < this.Phase)
		{
			if (!this.StudentManager.Yandere.Struggling && !this.StudentManager.Yandere.DelinquentFighting &&
				!this.StudentManager.Yandere.Pickpocketing && !this.StudentManager.Yandere.Noticed &&
				!this.ArmDetector.SummonDemon)
			{
				this.UpdateRep();

				if (this.Reputation <= -100)
				{
					this.Portal.EndDay();
				}
			}
		}

		if (!this.MissionMode)
		{
			this.CurrentRepMarker.localPosition = new Vector3(
				Mathf.Lerp(this.CurrentRepMarker.localPosition.x, -830.0f + (this.Reputation * 1.50f), Time.deltaTime * 10.0f),
				this.CurrentRepMarker.localPosition.y,
				this.CurrentRepMarker.localPosition.z);

			this.PendingRepMarker.localPosition = new Vector3(
				Mathf.Lerp(this.PendingRepMarker.localPosition.x, this.CurrentRepMarker.transform.localPosition.x + (this.PendingRep * 1.50f), Time.deltaTime * 10.0f),
				this.PendingRepMarker.localPosition.y,
				this.PendingRepMarker.localPosition.z);
		}
		else
		{
			this.PendingRepMarker.localPosition = new Vector3(
				Mathf.Lerp(this.PendingRepMarker.localPosition.x, -980.0f + (this.PendingRep * -3.0f), Time.deltaTime * 10.0f),
				this.PendingRepMarker.localPosition.y,
				this.PendingRepMarker.localPosition.z);
		}

		if (this.CurrentRepMarker.localPosition.x < -980.0f)
		{
			this.CurrentRepMarker.localPosition = new Vector3(
				-980.0f,
				this.CurrentRepMarker.localPosition.y,
				this.CurrentRepMarker.localPosition.z);
		}

		if (this.PendingRepMarker.localPosition.x < -980.0f)
		{
			this.PendingRepMarker.localPosition = new Vector3(
				-980.0f,
				this.PendingRepMarker.localPosition.y,
				this.PendingRepMarker.localPosition.z);
		}

		if (this.CurrentRepMarker.localPosition.x > -680.0f)
		{
			this.CurrentRepMarker.localPosition = new Vector3(
				-680.0f,
				this.CurrentRepMarker.localPosition.y,
				this.CurrentRepMarker.localPosition.z);
		}

		if (this.PendingRepMarker.localPosition.x > -680.0f)
		{
			this.PendingRepMarker.localPosition = new Vector3(
				-680.0f,
				this.PendingRepMarker.localPosition.y,
				this.PendingRepMarker.localPosition.z);
		}

		if (!this.MissionMode)
		{
			if (this.PendingRep > 0.0f)
			{	
				//System.Math.Round(this.PendingRep, 2);
				this.PendingRepLabel.text = "+" + this.PendingRep.ToString();
			}
			else if (this.PendingRep < 0.0f)
			{
				//System.Math.Round(this.PendingRep, 2);
				this.PendingRepLabel.text = this.PendingRep.ToString();
			}
			else
			{
				this.PendingRepLabel.text = string.Empty;
			}
		}
		else
		{
			if (this.PendingRep < 0.0f)
			{
				this.PendingRepLabel.text = (-this.PendingRep).ToString();
			}
			else
			{
				this.PendingRepLabel.text = string.Empty;
			}
		}
	}

	public GameObject FlowerVase;
	public GameObject Grafitti;

	void Bully()
	{
		this.FlowerVase.SetActive(false);
	}

	public void UpdateRep()
	{
		this.Reputation += this.PendingRep;
		this.PendingRep = 0.0f;
		this.CheckedRep++;

		if (this.StudentManager.Yandere.Club == ClubType.Delinquent)
		{
			if (this.Reputation > -33.33333f)
			{
				this.Reputation = -33.33333f;
			}
		}

		this.StudentManager.WipePendingRep();
	}
}