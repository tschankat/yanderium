using System.Collections;
using System.Collections.Generic;

using UnityEngine.PostProcessing;
using UnityEngine;

public enum ShopType
{
	Nonfunctional = 0,
	Hardware = 1,
	Manga = 2,
	Maid = 3,
	Salon = 4,
	Gift = 5,
	Convenience = 6,
	Games = 7,
	Electronics = 8,
	Lingerie = 9
}

public class StreetShopInterfaceScript : MonoBehaviour
{
	public StreetManagerScript StreetManager;
	public InputManagerScript InputManager;

	public PostProcessingProfile Profile;

	public StalkerYandereScript Yandere;

	public PromptBarScript PromptBar;

	public UILabel SpeechBubbleLabel;
	public UILabel StoreNameLabel;
	public UILabel MoneyLabel;

	public Texture[] ShopkeeperPortraits;
	public string[] ShopkeeperSpeeches;

	public UILabel[] ProductsLabel;
	public UILabel[] PricesLabel;
	public UISprite[] Icons;

	public bool[] AdultProducts;
	public float[] Costs;

	public UITexture Shopkeeper;

	public Transform SpeechBubbleParent;
	public Transform MaidWindow;
	public Transform Highlight;
	public Transform Interface;

	public GameObject FakeIDBox;

    public AudioSource Jukebox;
    public AudioSource MyAudio;

	public int ShopkeeperPosition;
	public int SpeechPhase;
	public int Selected;
	public int Limit;

	public float BlurAmount;
	public float Timer;

	public bool ShowMaid;
	public bool Show;

	public ShopType CurrentStore;

	void Start()
	{
		Shopkeeper.transform.localPosition = new Vector3(1485, 0, 0);
		Interface.localPosition = new Vector3(-815.5f, 0, 0);
		SpeechBubbleParent.localScale = new Vector3(0, 0, 0);

		UpdateFakeID();
	}

	void Update ()
	{
		if (this.Show)
		{
            Jukebox.volume = Mathf.Lerp(Jukebox.volume, 1, Time.deltaTime * 10);

            Shopkeeper.transform.localPosition = Vector3.Lerp(Shopkeeper.transform.localPosition,
				new Vector3(ShopkeeperPosition, 0, 0), Time.deltaTime * 10);

			Interface.localPosition = Vector3.Lerp(Interface.localPosition,
				new Vector3(100, 0, 0), Time.deltaTime * 10);

			BlurAmount = Mathf.Lerp(BlurAmount, 0, Time.deltaTime * 10);

			if (Input.GetButtonUp(InputNames.Xbox_B))
			{
				Yandere.RPGCamera.enabled = true;

				this.PromptBar.Show = false;
				this.Yandere.CanMove = true;
				this.Show = false;
			}

			if (Timer > .5f)
			{
				if (Input.GetButtonUp(InputNames.Xbox_A))
				{
					if (Icons[Selected].spriteName != "Yes")
					{
						CheckStore();
						UpdateIcons();
					}
				}
			}

			if (InputManager.TappedDown)
			{
				Selected++;

				if (Selected > Limit)
				{
					Selected = 1;
				}

				UpdateHighlight();
			}
			else if (InputManager.TappedUp)
			{
				Selected--;

				if (Selected < 1)
				{
					Selected = Limit;
				}

				UpdateHighlight();
			}

			Timer += Time.deltaTime;

			if (Timer > .5f)
			{
				SpeechBubbleParent.localScale = Vector3.Lerp(
					SpeechBubbleParent.localScale,
					new Vector3(1, 1, 1),
					Time.deltaTime * 10);
			}

			if (SpeechPhase == 0)
			{
				Shopkeeper.mainTexture = ShopkeeperPortraits[1];
				SpeechPhase++;
			}
			else
			{
				if (Timer > 10)
				{
					if (SpeechPhase == 1)
					{
						SpeechBubbleLabel.text = ShopkeeperSpeeches[2];
						Shopkeeper.mainTexture = ShopkeeperPortraits[2];
						SpeechBubbleParent.localScale = new Vector3(0, 0, 0);

						SpeechPhase++;
					}
					else if (SpeechPhase == 2)
					{
						if (Timer > 10.1f)
						{
							int TempInt = Random.Range(2, 4);
							Shopkeeper.mainTexture = ShopkeeperPortraits[TempInt];
							Timer = 10;
						}
					}
				}
			}
		}
		else
		{
            Jukebox.volume = Mathf.Lerp(Jukebox.volume, 0, Time.deltaTime);

            SpeechBubbleParent.localScale = new Vector3(0, 0, 0);

			Shopkeeper.transform.localPosition = Vector3.Lerp(Shopkeeper.transform.localPosition,
				new Vector3(1604, 0, 0), Time.deltaTime * 10);

			Interface.localPosition = Vector3.Lerp(Interface.localPosition,
				new Vector3(-815.5f, 0, 0), Time.deltaTime * 10);

			if (ShowMaid)
			{
				BlurAmount = Mathf.Lerp(BlurAmount, 0, Time.deltaTime * 10);

				MaidWindow.localScale = Vector3.Lerp(
					MaidWindow.localScale,
					new Vector3(1, 1, 1),
					Time.deltaTime * 10);

				if (Input.GetButtonDown(InputNames.Xbox_A))
				{
					StreetManager.FadeOut = true;
					StreetManager.GoToCafe = true;
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_B))
					{
						Yandere.RPGCamera.enabled = true;
						Yandere.CanMove = true;
						ShowMaid = false;
					}
				}
			}
			else
			{
				BlurAmount = Mathf.Lerp(BlurAmount, .6f, Time.deltaTime * 10);

				MaidWindow.localScale = Vector3.Lerp(
					MaidWindow.localScale,
					new Vector3(0, 0, 0),
					Time.deltaTime * 10);
			}
		}

		AdjustBlur();
	}

	void AdjustBlur()
	{
		DepthOfFieldModel.Settings DepthSettings = Profile.depthOfField.settings;
		DepthSettings.focusDistance = BlurAmount;
		Profile.depthOfField.settings = DepthSettings;
	}

	public void UpdateHighlight()
	{
		Highlight.localPosition = new Vector3(-50, 50 - (50 * Selected), 0);
	}

	//1-5 are erotic manga
	//6-10 are horror manga
	//11-15 are Life Note

	public void CheckStore()
	{
		if (AdultProducts[Selected] && !PlayerGlobals.FakeID)
		{
			SpeechBubbleLabel.text = ShopkeeperSpeeches[3];
			SpeechBubbleParent.localScale = new Vector3(0, 0, 0);
			SpeechPhase = 0;
			Timer = 1;
		}
		else if (PlayerGlobals.Money < Costs[Selected])
		{
			StreetManager.Clock.MoneyFail();

			SpeechBubbleLabel.text = ShopkeeperSpeeches[4];
			SpeechBubbleParent.localScale = new Vector3(0, 0, 0);
			SpeechPhase = 0;
			Timer = 1;
		}
		else
		{
			switch (CurrentStore)
			{
				case ShopType.Nonfunctional:
				{
					SpeechBubbleLabel.text = ShopkeeperSpeeches[6];
					SpeechBubbleParent.localScale = new Vector3(0, 0, 0);
					SpeechPhase = 0;
					Timer = 1;

					break;
				}

				case ShopType.Manga:
				{
					PurchaseEffect();

					switch (Selected)
					{
						case 1:
						{
							CollectibleGlobals.SetMangaCollected(6, true);
							break;
						}
						case 2:
						{
							CollectibleGlobals.SetMangaCollected(7, true);
							break;
						}
						case 3:
						{
							CollectibleGlobals.SetMangaCollected(8, true);
							break;
						}
						case 4:
						{
							CollectibleGlobals.SetMangaCollected(9, true);
							break;
						}
						case 5:
						{
							CollectibleGlobals.SetMangaCollected(10, true);
							break;
						}
						case 6:
						{
							CollectibleGlobals.SetMangaCollected(1, true);
							break;
						}
						case 7:
						{
							CollectibleGlobals.SetMangaCollected(2, true);
							break;
						}
						case 8:
						{
							CollectibleGlobals.SetMangaCollected(3, true);
							break;
						}
						case 9:
						{
							CollectibleGlobals.SetMangaCollected(4, true);
							break;
						}
						case 10:
						{
							CollectibleGlobals.SetMangaCollected(5, true);
							break;
						}
					}

					break;
				}

				case ShopType.Gift:
				{
					PurchaseEffect();

					if (Selected < 6)
					{
						CollectibleGlobals.SenpaiGifts++;
					}
					else
					{
						CollectibleGlobals.MatchmakingGifts++;
					}

					CollectibleGlobals.SetGiftPurchased(Selected, true);
					break;
				}

				case ShopType.Lingerie:
				{
					PurchaseEffect();
					CollectibleGlobals.SetPantyPurchased(Selected, true);
					break;
				}

				case ShopType.Salon:
				{
					SpeechBubbleLabel.text = ShopkeeperSpeeches[6];
					SpeechBubbleParent.localScale = new Vector3(0, 0, 0);
					SpeechPhase = 0;
					Timer = 1;

					break;
				}
			}
		}
	}

	public void PurchaseEffect()
	{
		SpeechBubbleLabel.text = ShopkeeperSpeeches[5];
		SpeechBubbleParent.localScale = new Vector3(0, 0, 0);
		SpeechPhase = 0;
		Timer = 1;

		PlayerGlobals.Money -= Costs[Selected];

		MoneyLabel.text = "$" + PlayerGlobals.Money.ToString("F2", System.Globalization.NumberFormatInfo.InvariantInfo);
        StreetManager.Clock.UpdateMoneyLabel();
		MyAudio.Play();
	}

	public void UpdateFakeID()
	{
		FakeIDBox.SetActive(PlayerGlobals.FakeID);
	}

	public void UpdateIcons()
	{
		int ID = 1;

		while (ID < 11)
		{
			Icons[ID].spriteName = "";
			Icons[ID].gameObject.SetActive(false);
			ProductsLabel[ID].color = new Color(1, 1, 1, 1);
			ID++;
		}

		ID = 1;

		while (ID < 11)
		{
			if (AdultProducts[ID] == true)
			{
				Icons[ID].spriteName = "18+";
			}

			ID++;
		}

		switch (CurrentStore)
		{
			case ShopType.Manga:
			{
				if (CollectibleGlobals.GetMangaCollected(1) == true){Icons[6].spriteName = "Yes";PricesLabel[6].text = "Owned";}
				if (CollectibleGlobals.GetMangaCollected(2) == true){Icons[7].spriteName = "Yes";PricesLabel[7].text = "Owned";}
				if (CollectibleGlobals.GetMangaCollected(3) == true){Icons[8].spriteName = "Yes";PricesLabel[8].text = "Owned";}
				if (CollectibleGlobals.GetMangaCollected(4) == true){Icons[9].spriteName = "Yes";PricesLabel[9].text = "Owned";}
				if (CollectibleGlobals.GetMangaCollected(5) == true){Icons[10].spriteName = "Yes";PricesLabel[10].text = "Owned";}

				if (CollectibleGlobals.GetMangaCollected(6) == true){Icons[1].spriteName = "Yes";PricesLabel[1].text = "Owned";}
				if (CollectibleGlobals.GetMangaCollected(7) == true){Icons[2].spriteName = "Yes";PricesLabel[2].text = "Owned";}
				if (CollectibleGlobals.GetMangaCollected(8) == true){Icons[3].spriteName = "Yes";PricesLabel[3].text = "Owned";}
				if (CollectibleGlobals.GetMangaCollected(9) == true){Icons[4].spriteName = "Yes";PricesLabel[4].text = "Owned";}
				if (CollectibleGlobals.GetMangaCollected(10) == true){Icons[5].spriteName = "Yes";PricesLabel[5].text = "Owned";}

				break;
			}
			case ShopType.Gift:
			{
				ID = 1;

				while (ID < 11)
				{
					if (CollectibleGlobals.GetGiftPurchased(ID) == true){Icons[ID].spriteName = "Yes";PricesLabel[ID].text = "Owned";}
					ID++;
				}

				break;
			}
			case ShopType.Lingerie:
			{
				ID = 1;

				while (ID < 11)
				{
					if (CollectibleGlobals.GetPantyPurchased(ID) == true){Icons[ID].spriteName = "Yes";PricesLabel[ID].text = "Owned";}
					ID++;
				}

				break;
			}
		}

		ID = 1;

		while (ID < 11)
		{
			if (Icons[ID].spriteName != "")
			{
				Icons[ID].gameObject.SetActive(true);

				if (Icons[ID].spriteName == "Yes")
				{
					ProductsLabel[ID].color = new Color(1, 1, 1, .5f);
				}
			}

			ID++;
		}
	}
}