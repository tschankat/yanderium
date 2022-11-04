using UnityEngine;

public class DropsScript : MonoBehaviour
{
	public InfoChanWindowScript InfoChanWindow;
	public InputManagerScript InputManager;
	public InventoryScript Inventory;
	public PromptBarScript PromptBar;
	public SchemesScript Schemes;
	public GameObject FavorMenu;
	public Transform Highlight;

	public UILabel PantyCount;
	public UITexture DropIcon;
	public UILabel DropDesc;

	public UILabel[] CostLabels;
	public UILabel[] NameLabels;

    public bool[] InfiniteSupply;
    public bool[] Purchased;

	public Texture[] DropIcons;

	public int[] DropCosts;

	public string[] DropDescs;
	public string[] DropNames;

	public int Selected = 1;
	public int ID = 1;

	public AudioClip InfoUnavailable;
	public AudioClip InfoPurchase;
	public AudioClip InfoAfford;

	void Start()
	{
		// [af] Converted while loop to for loop.
		for (this.ID = 1; this.ID < this.DropNames.Length; this.ID++)
		{
			this.NameLabels[this.ID].text = this.DropNames[this.ID];
		}

        if (MissionModeGlobals.MissionMode)
        {
            CostLabels[6].text = "10";
            InfiniteSupply[6] = true;
            DropCosts[6] = 10;
        }
	}

	void Update()
	{
		if (this.InputManager.TappedUp)
		{
			this.Selected--;

			if (this.Selected < 1)
			{
				this.Selected = this.DropNames.Length - 1;
			}

			this.UpdateDesc();
		}

		if (this.InputManager.TappedDown)
		{
			this.Selected++;

			if (this.Selected > (this.DropNames.Length - 1))
			{
				this.Selected = 1;
			}

			this.UpdateDesc();
		}

		if (Input.GetButtonDown(InputNames.Xbox_A))
		{
			AudioSource audioSource = this.GetComponent<AudioSource>();

			if (!this.Purchased[this.Selected] && this.InfoChanWindow.Orders < 11)
			{
				if (this.PromptBar.Label[0].text != string.Empty)
				{
					if (this.Inventory.PantyShots >= this.DropCosts[this.Selected])
					{
						this.Inventory.PantyShots -= this.DropCosts[this.Selected];

                        if (!this.InfiniteSupply[this.Selected])
                        {
    						this.Purchased[this.Selected] = true;
                        }

                        this.InfoChanWindow.Orders++;
						this.InfoChanWindow.ItemsToDrop[this.InfoChanWindow.Orders] = this.Selected;
						this.InfoChanWindow.DropObject();

						this.UpdateList();
						this.UpdateDesc();

						audioSource.clip = this.InfoPurchase;
						audioSource.Play();

						if (this.Selected == 2)
						{
							SchemeGlobals.SetSchemeStage(3, 2);
							this.Schemes.UpdateInstructions();
						}
					}
				}
				else
				{
					if (this.Inventory.PantyShots < this.DropCosts[this.Selected])
					{
						audioSource.clip = this.InfoAfford;
						audioSource.Play();
					}
					else
					{
						audioSource.clip = this.InfoUnavailable;
						audioSource.Play();
					}
				}
			}
			else
			{
				audioSource.clip = this.InfoUnavailable;
				audioSource.Play();
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

			// [af] Added "gameObject" for C# compatibility.
			this.gameObject.SetActive(false);
		}
	}

	public void UpdateList()
	{
		// [af] Converted while loop to for loop.
		for (this.ID = 1; this.ID < this.DropNames.Length; this.ID++)
		{
			UILabel nameLabel = this.NameLabels[this.ID];

			if (!this.Purchased[this.ID])
			{
				this.CostLabels[this.ID].text = this.DropCosts[this.ID].ToString();

				nameLabel.color = new Color(
					nameLabel.color.r,
					nameLabel.color.g,
					nameLabel.color.b,
					1.0f);
			}
			else
			{
				this.CostLabels[this.ID].text = string.Empty;

				nameLabel.color = new Color(
					nameLabel.color.r,
					nameLabel.color.g,
					nameLabel.color.b,
					0.50f);
			}
		}
	}

	public void UpdateDesc()
	{
		if (!this.Purchased[this.Selected])
		{
			if (this.Inventory.PantyShots >= this.DropCosts[this.Selected])
			{
				this.PromptBar.Label[0].text = "Purchase";
				this.PromptBar.UpdateButtons();
			}
			else
			{
				this.PromptBar.Label[0].text = string.Empty;
				this.PromptBar.UpdateButtons();
			}
		}
		else
		{
			this.PromptBar.Label[0].text = string.Empty;
			this.PromptBar.UpdateButtons();
		}

		this.Highlight.localPosition = new Vector3(
			this.Highlight.localPosition.x,
			200.0f - (25.0f * Selected),
			this.Highlight.localPosition.z);

		this.DropIcon.mainTexture = this.DropIcons[this.Selected];
		this.DropDesc.text = this.DropDescs[this.Selected];

		this.UpdatePantyCount();
	}

	public void UpdatePantyCount()
	{
		this.PantyCount.text = this.Inventory.PantyShots.ToString();
	}
}
