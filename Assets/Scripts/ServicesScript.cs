using UnityEngine;

public class ServicesScript : MonoBehaviour
{
	public TextMessageManagerScript TextMessageManager;
	public StudentManagerScript StudentManager;
	public InputManagerScript InputManager;
	public ReputationScript Reputation;
	public InventoryScript Inventory;
	public PromptBarScript PromptBar;
	public SchemesScript Schemes;
	public YandereScript Yandere;
	public GameObject FavorMenu;
	public Transform Highlight;
    public AudioSource MyAudio;
    public PoliceScript Police;

	public UITexture ServiceIcon;

	public UILabel ServiceLimit;
	public UILabel ServiceDesc;
	public UILabel PantyCount;

	public UILabel[] CostLabels;
	public UILabel[] NameLabels;

	public Texture[] ServiceIcons;

	public string[] ServiceLimits;
	public string[] ServiceDescs;
	public string[] ServiceNames;

	public bool[] ServiceAvailable;
	public bool[] ServicePurchased;

	public int[] ServiceCosts;

	public int Selected = 1;
	public int ID = 1;

	public AudioClip InfoUnavailable;
	public AudioClip InfoPurchase;
	public AudioClip InfoAfford;

	void Start()
	{
		for (int ID = 1; ID < this.ServiceNames.Length; ID++)
		{
			SchemeGlobals.SetServicePurchased(ID, false);
			this.NameLabels[ID].text = this.ServiceNames[ID];
		}
	}

	void Update()
	{
		if (this.InputManager.TappedUp)
		{
			this.Selected--;

			if (this.Selected < 1)
			{
				this.Selected = this.ServiceNames.Length - 1;
			}

			this.UpdateDesc();
		}

		if (this.InputManager.TappedDown)
		{
			this.Selected++;

			if (this.Selected > (this.ServiceNames.Length - 1))
			{
				this.Selected = 1;
			}

			this.UpdateDesc();
		}
		if (Input.GetButtonDown(InputNames.Xbox_A))
		{
			if (!SchemeGlobals.GetServicePurchased(this.Selected) &&
				this.NameLabels[this.Selected].color.a == 1.0)
			{
				if (this.PromptBar.Label[0].text != string.Empty)
				{
					if (Inventory.PantyShots >= this.ServiceCosts[this.Selected])
					{
						// Provide Student Info
						if (this.Selected == 1)
						{
							this.Yandere.PauseScreen.StudentInfoMenu.GettingInfo = true;

                            this.Yandere.PauseScreen.StudentInfoMenu.Column = 0;
                            this.Yandere.PauseScreen.StudentInfoMenu.Row = 0;
                            this.Yandere.PauseScreen.StudentInfoMenu.UpdateHighlight();
                            this.Yandere.PauseScreen.Sideways = true;

                            this.Yandere.PauseScreen.StudentInfoMenu.gameObject.SetActive(true);
                            //this.Yandere.PauseScreen.StudentInfoMenu.GrabbedPortraits = false;
                            this.StartCoroutine(this.Yandere.PauseScreen.StudentInfoMenu.UpdatePortraits());

							this.Yandere.PromptBar.ClearButtons();
							this.Yandere.PromptBar.Label[1].text = "Cancel";
							this.Yandere.PromptBar.UpdateButtons();
							this.Yandere.PromptBar.Show = true;

							this.gameObject.SetActive(false);
						}
						// Increase Reputation
						if (this.Selected == 2)
						{
							this.Reputation.PendingRep += 5;

							this.Purchase();
						}
						// Decrease Reputation
						else if (this.Selected == 3)
						{
							StudentGlobals.SetStudentReputation(this.StudentManager.RivalID,
								StudentGlobals.GetStudentReputation(this.StudentManager.RivalID) - 5);

							this.Purchase();
						}
						// Rival's Dark Secret
						else if (this.Selected == 4)
						{
							SchemeGlobals.SetServicePurchased(this.Selected, true);
							SchemeGlobals.DarkSecret = true;

							this.Purchase();
						}
						// Send Student Home
						else if (this.Selected == 5)
						{
							this.Yandere.PauseScreen.StudentInfoMenu.SendingHome = true;

                            this.Yandere.PauseScreen.StudentInfoMenu.Column = 0;
                            this.Yandere.PauseScreen.StudentInfoMenu.Row = 0;
                            this.Yandere.PauseScreen.StudentInfoMenu.UpdateHighlight();
                            this.Yandere.PauseScreen.Sideways = true;

                            this.Yandere.PauseScreen.StudentInfoMenu.gameObject.SetActive(true);
                            this.Yandere.PauseScreen.StudentInfoMenu.GrabbedPortraits = false;
                            this.StartCoroutine(this.Yandere.PauseScreen.StudentInfoMenu.UpdatePortraits());

							this.Yandere.PromptBar.ClearButtons();
							this.Yandere.PromptBar.Label[1].text = "Cancel";
							this.Yandere.PromptBar.UpdateButtons();
							this.Yandere.PromptBar.Show = true;

							this.gameObject.SetActive(false);
						}
						// Delay Police
						else if (this.Selected == 6)
						{
							this.Police.Timer += 300;
							this.Police.Delayed = true;

							this.Purchase();
						}
						// Guidance Counselor's Recording
						else if (this.Selected == 7)
						{
							SchemeGlobals.SetServicePurchased(this.Selected, true);

							CounselorGlobals.CounselorTape = 1;

							this.Purchase();
						}
						// Learn Osana's likes and dislikes
						else if (this.Selected == 8)
						{
							SchemeGlobals.SetServicePurchased(this.Selected, true);

							int ID = 1;

							while (ID < 26)
							{
								ConversationGlobals.SetTopicLearnedByStudent(ID, 11, true);
								ID++;
							}

							this.Purchase();
						}
                        // Fire student council member
                        else if (this.Selected == 9)
                        {
                            this.Yandere.PauseScreen.StudentInfoMenu.FiringCouncilMember = true;

                            this.Yandere.PauseScreen.StudentInfoMenu.Column = 0;
                            this.Yandere.PauseScreen.StudentInfoMenu.Row = 0;
                            this.Yandere.PauseScreen.StudentInfoMenu.UpdateHighlight();
                            this.Yandere.PauseScreen.Sideways = true;

                            this.Yandere.PauseScreen.StudentInfoMenu.gameObject.SetActive(true);
                            this.Yandere.PauseScreen.StudentInfoMenu.GrabbedPortraits = false;
                            this.StartCoroutine(this.Yandere.PauseScreen.StudentInfoMenu.UpdatePortraits());

                            this.Yandere.PromptBar.ClearButtons();
                            this.Yandere.PromptBar.Label[1].text = "Cancel";
                            this.Yandere.PromptBar.UpdateButtons();
                            this.Yandere.PromptBar.Show = true;

                            this.gameObject.SetActive(false);
                        }
                    }
				}
				else
				{
					if (Inventory.PantyShots < this.ServiceCosts[this.Selected])
					{
						MyAudio.clip = this.InfoAfford;
                        MyAudio.Play();
					}
					else
					{
                        MyAudio.clip = this.InfoUnavailable;
                        MyAudio.Play();
					}
				}
			}
			else
			{
                MyAudio.clip = this.InfoUnavailable;
                MyAudio.Play();
			}
		}

		if (Input.GetButtonDown(InputNames.Xbox_B))
		{
			// [af] Removed redundant if statement.
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Accept";
			this.PromptBar.Label[1].text = "Exit";
			this.PromptBar.Label[5].text = "Choose";
			this.PromptBar.UpdateButtons();

			this.FavorMenu.SetActive(true);

			this.gameObject.SetActive(false);
		}
	}

	public void UpdateList()
	{
		// [af] Converted while loop to for loop.
		for (this.ID = 1; this.ID < ServiceNames.Length; this.ID++)
		{
			this.CostLabels[this.ID].text = this.ServiceCosts[this.ID].ToString();

			bool servicePurchased = SchemeGlobals.GetServicePurchased(this.ID);

			this.ServiceAvailable[ID] = false;

			if (this.ID == 1 || this.ID == 2 || this.ID == 3)
			{
				this.ServiceAvailable[ID] = true;
			}
			else if (this.ID == 4)
			{
				if (!SchemeGlobals.DarkSecret)
				{
					this.ServiceAvailable[ID] = true;
				}
			}
			else if (this.ID == 5)
			{
				if (!this.ServicePurchased[ID])
				{
					this.ServiceAvailable[ID] = true;
				}
			}
			else if (this.ID == 6)
			{
				if (this.Police.Show && !this.Police.Delayed)
				{
					this.ServiceAvailable[ID] = true;
				}
			}
			else if (this.ID == 7)
			{
				if (CounselorGlobals.CounselorTape == 0)
				{
					this.ServiceAvailable[ID] = true;
				}
			}
			else if (this.ID == 8)
			{
				if (!SchemeGlobals.GetServicePurchased(8))
				{
					this.ServiceAvailable[ID] = true;
				}
			}
            else if (this.ID == 9)
            {
                if (MissionModeGlobals.MissionMode)
                {
                    this.ServiceAvailable[ID] = true;
                }
            }

            // [af] Replaced if/else statement with assignment and ternary expression.
            UILabel nameLabel = this.NameLabels[this.ID];
			nameLabel.color = new Color(
				nameLabel.color.r,
				nameLabel.color.g,
				nameLabel.color.b,
				(this.ServiceAvailable[this.ID] && !servicePurchased) ? 1.0f : 0.50f);
		}
	}

	public void UpdateDesc()
	{
		if (this.ServiceAvailable[this.Selected] && 
			!SchemeGlobals.GetServicePurchased(this.Selected))
		{
			// [af] Replaced if/else statement with ternary expression.
			this.PromptBar.Label[0].text =
				(Inventory.PantyShots >= this.ServiceCosts[this.Selected]) ?
				"Purchase" : string.Empty;

			this.PromptBar.UpdateButtons();
		}
		else
		{
			this.PromptBar.Label[0].text = string.Empty;
			this.PromptBar.UpdateButtons();
		}

		this.Highlight.localPosition = new Vector3(
			this.Highlight.localPosition.x,
			200.0f - (25.0f * this.Selected),
			this.Highlight.localPosition.z);

		this.ServiceIcon.mainTexture = this.ServiceIcons[this.Selected];
		this.ServiceLimit.text = this.ServiceLimits[this.Selected];
		this.ServiceDesc.text = this.ServiceDescs[this.Selected];

		if (this.Selected == 5)
		{
			this.ServiceDesc.text = this.ServiceDescs[this.Selected] + "\n" + "\n" + "If student portraits don't appear, back out of the menu, load the Student Info menu, then return to this screen.";
		}

		this.UpdatePantyCount();
	}

	public void UpdatePantyCount()
	{
		this.PantyCount.text = Inventory.PantyShots.ToString();
	}

	public void Purchase()
	{
		this.ServicePurchased[this.Selected] = true;
		this.TextMessageManager.SpawnMessage(this.Selected);
		Inventory.PantyShots -= this.ServiceCosts[this.Selected];
		AudioSource.PlayClipAtPoint(this.InfoPurchase, this.transform.position);

		this.UpdateList();
		this.UpdateDesc();

		this.PromptBar.Label[0].text = string.Empty;
		this.PromptBar.Label[1].text = "Back";
		this.PromptBar.UpdateButtons();
	}
}
