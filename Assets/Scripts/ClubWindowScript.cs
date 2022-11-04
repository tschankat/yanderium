using UnityEngine;

public class ClubWindowScript : MonoBehaviour
{
	public ClubManagerScript ClubManager;
	public PromptBarScript PromptBar;
	public YandereScript Yandere;

	public Transform ActivityWindow;

	public GameObject ClubInfo;
	public GameObject Window;

	public GameObject Warning;

	public string[] ActivityDescs;
	public string[] ClubNames;
	public string[] ClubDescs;

	public string MedAtmosphereDesc;
	public string LowAtmosphereDesc;

	public UILabel ActivityLabel;
	public UILabel BottomLabel;
	public UILabel ClubName;
	public UILabel ClubDesc;

	public bool PerformingActivity = false;
	public bool Activity = false;
	public bool Quitting = false;

	public float Timer = 0.0f;
	public ClubType Club = ClubType.None;

	void Start()
	{
		this.Window.SetActive(false);

		if (SchoolGlobals.SchoolAtmosphere <= .9f)
		{
			this.ActivityDescs[7] = this.LowAtmosphereDesc;
		}
		else if (SchoolGlobals.SchoolAtmosphere <= .8f)
		{
			this.ActivityDescs[7] = this.MedAtmosphereDesc;
		}
	}

	void Update()
	{
		if (this.Window.activeInHierarchy)
		{
			if (this.Timer > 0.50f)
			{
				if (Input.GetButtonDown(InputNames.Xbox_A))
				{
					if (!this.Quitting && !this.Activity)
					{
                        this.Yandere.Club = this.Club;
						this.Yandere.ClubAccessory();
						this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubJoin;

						this.ClubManager.ActivateClubBenefit();
					}
					else if (this.Quitting)
					{
						this.ClubManager.DeactivateClubBenefit();

						ClubGlobals.SetQuitClub(this.Club, true);
                        this.Yandere.Club = ClubType.None;
						this.Yandere.ClubAccessory();
						this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubQuit;
						this.Quitting = false;

						this.Yandere.StudentManager.UpdateBooths();
					}
					else if (this.Activity)
					{
						this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubActivity;
					}

					this.Yandere.TargetStudent.TalkTimer = 100.0f;
					this.Yandere.TargetStudent.ClubPhase = 2;
					this.PromptBar.ClearButtons();
					this.PromptBar.Show = false;
					this.Window.SetActive(false);
				}

				if (Input.GetButtonDown(InputNames.Xbox_B))
				{
					if (!this.Quitting && !this.Activity)
					{
						this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubJoin;
					}
					else if (this.Quitting)
					{
						this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubQuit;
						this.Quitting = false;
					}
					else if (this.Activity)
					{
						this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubActivity;
						this.Activity = false;
					}

					this.Yandere.TargetStudent.TalkTimer = 100.0f;
					this.Yandere.TargetStudent.ClubPhase = 3;
					this.PromptBar.ClearButtons();
					this.PromptBar.Show = false;
					this.Window.SetActive(false);
				}

				if (Input.GetButtonDown(InputNames.Xbox_X))
				{
					if (!this.Quitting && !this.Activity)
					{
						if (!this.Warning.activeInHierarchy)
						{
							this.ClubInfo.SetActive(false);
							this.Warning.SetActive(true);
						}
						else
						{
							this.ClubInfo.SetActive(true);
							this.Warning.SetActive(false);
						}
					}
				}
			}

			this.Timer += Time.deltaTime;
		}

		if (this.PerformingActivity)
		{
			this.ActivityWindow.localScale = Vector3.Lerp(
				this.ActivityWindow.localScale,
				new Vector3(1.0f, 1.0f, 1.0f),
				Time.deltaTime * 10.0f);
		}
		else
		{
			if (this.ActivityWindow.localScale.x > 0.10f)
			{
				this.ActivityWindow.localScale = Vector3.Lerp(
					this.ActivityWindow.localScale,
					Vector3.zero,
					Time.deltaTime * 10.0f);
			}
			else
			{
				if (this.ActivityWindow.localScale.x != 0.0f)
				{
					this.ActivityWindow.localScale = Vector3.zero;
				}
			}
		}
	}

	public void UpdateWindow()
	{
		this.ClubName.text = this.ClubNames[(int)this.Club];

		if (!this.Quitting && !this.Activity)
		{
			this.ClubDesc.text = this.ClubDescs[(int)this.Club];

			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Accept";
			this.PromptBar.Label[1].text = "Refuse";
			this.PromptBar.Label[2].text = "More Info";
			this.PromptBar.UpdateButtons();
			this.PromptBar.Show = true;

			this.BottomLabel.text = "Will you join the " + this.ClubNames[(int)this.Club] + "?";
		}
		else if (this.Activity)
		{
			this.ClubDesc.text = "Club activities last until 6:00 PM. If you choose to participate in club activities now, the day will end." +
				"\n" + "\n" + "If you don't join by 5:30 PM, you won't be able to participate in club activities today." +
				"\n" + "\n" + "If you don't participate in club activities at least once a week, you will be removed from the club.";

			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Yes";
			this.PromptBar.Label[1].text = "No";
			this.PromptBar.UpdateButtons();
			this.PromptBar.Show = true;

			this.BottomLabel.text = "Will you participate in club activities?";
		}
		else if (this.Quitting)
		{
			this.ClubDesc.text = "Are you sure you want to quit this club? If you quit, you will never be allowed to return.";

			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Confirm";
			this.PromptBar.Label[1].text = "Deny";
			this.PromptBar.UpdateButtons();
			this.PromptBar.Show = true;

			this.BottomLabel.text = "Will you quit the " + this.ClubNames[(int)this.Club] + "?";
		}

		this.ClubInfo.SetActive(true);
		this.Warning.SetActive(false);

		this.Window.SetActive(true);

		this.Timer = 0.0f;
	}
}
