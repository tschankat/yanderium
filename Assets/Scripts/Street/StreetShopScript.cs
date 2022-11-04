using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetShopScript : MonoBehaviour
{
	public StreetShopInterfaceScript StreetShopInterface;
	public StreetManagerScript StreetManager;
	public InputDeviceScript InputDevice;
	public StalkerYandereScript Yandere;
	public PromptBarScript PromptBar;
	public HomeClockScript HomeClock;

	public GameObject BinocularOverlay;
	public Renderer BinocularRenderer;
	public Camera BinocularCamera;
	public AudioSource MyAudio;

    public AudioClip StoreTheme;
	public AudioClip InsertCoin;
	public AudioClip Fail;

	public UILabel MyLabel;

	public Texture[] ShopkeeperPortraits;
	public string[] ShopkeeperSpeeches;

	public bool[] AdultProducts;

	public string[] Products;
	public float[] Costs;

	public float RotationX = 0;
	public float RotationY = 0;

	public float Alpha = 0;
	public float Zoom = 0;

	public int ShopkeeperPosition = 500;
	public int Limit = 0;

	public bool Binoculars;
	public bool MaidCafe;
	public bool Exit;

	public string StoreName;

	public ShopType StoreType;

	void Start ()
	{
		MyLabel.color = new Color(1, 1, 1, 0);
	}

	void Update ()
	{
		//MyLabel.transform.LookAt(Yandere.MainCamera.transform.position);

		if (Vector3.Distance(Yandere.transform.position, transform.position) < 1)
		{
			Alpha = Mathf.MoveTowards(Alpha, 1, Time.deltaTime * 10);
		}
		else
		{
			Alpha = Mathf.MoveTowards(Alpha, 0, Time.deltaTime * 10);
		}	

		MyLabel.color = new Color(1, .75f, 1, Alpha);

		if (Alpha == 1)
		{
			if (Input.GetButtonDown(InputNames.Xbox_A))
			{
				if (Exit)
				{
					StreetManager.FadeOut = true;

					Yandere.MyAnimation.CrossFade(Yandere.IdleAnim);
					Yandere.CanMove = false;
				}
				else if (MaidCafe)
				{
					StreetShopInterface.ShowMaid = true;

					Yandere.MyAnimation.CrossFade(Yandere.IdleAnim);
					Yandere.RPGCamera.enabled = false;
					Yandere.CanMove = false;
				}
				else
				{
					if (!Binoculars)
					{
						if (!StreetShopInterface.Show)
						{
							StreetShopInterface.CurrentStore = StoreType;

							StreetShopInterface.Show = true;

							PromptBar.ClearButtons();
							PromptBar.Label[0].text = "Purchase";
							PromptBar.Label[1].text = "Exit";
							PromptBar.UpdateButtons();
							PromptBar.Show = true;

							Yandere.MyAnimation.CrossFade(Yandere.IdleAnim);
							Yandere.CanMove = false;

							UpdateShopInterface();
						}
					}
					else
					{
						if (PlayerGlobals.Money >= .25f)
						{
							MyAudio.clip = InsertCoin;

							PlayerGlobals.Money -= .25f;
							HomeClock.UpdateMoneyLabel();

							BinocularCamera.gameObject.SetActive(true);
							BinocularRenderer.enabled = false;
							BinocularOverlay.SetActive(true);

							PromptBar.ClearButtons();
							PromptBar.Label[1].text = "Exit";
							PromptBar.UpdateButtons();
							PromptBar.Show = true;

							Yandere.MyAnimation.CrossFade(Yandere.IdleAnim);
							Yandere.transform.position = new Vector3(5, 0, 3);
							Yandere.CanMove = false;

							MyAudio.Play();
						}
						else
						{
							HomeClock.MoneyFail();
						}
					}
				}
			}
		}

		if (Binoculars)
		{
			if (BinocularCamera.gameObject.activeInHierarchy)
			{
				if (InputDevice.Type == InputDeviceType.MouseAndKeyboard)
				{
					RotationX -= Input.GetAxis("Mouse Y") * (BinocularCamera.fieldOfView / 60);
					RotationY += Input.GetAxis("Mouse X") * (BinocularCamera.fieldOfView / 60);
				}
				else
				{
					RotationX -= Input.GetAxis("Mouse Y") * (BinocularCamera.fieldOfView / 60);
					RotationY += Input.GetAxis("Mouse X") * (BinocularCamera.fieldOfView / 60);
				}

				BinocularCamera.transform.eulerAngles = new Vector3(RotationX, RotationY + 90, 0);

				if (RotationX > 45){RotationX = 45;}
				if (RotationX < -45){RotationX = -45;}

				if (RotationY > 90){RotationY = 90;}
				if (RotationY < -90){RotationY = -90;}

				Zoom -= Input.GetAxis("Mouse ScrollWheel") * 10;

				Zoom -= Input.GetAxis("Vertical") * .1f;

				if (Zoom > 60)
				{
					Zoom = 60;
				}
				else if (Zoom < 1)
				{
					Zoom = 1;
				}

				BinocularCamera.fieldOfView = Mathf.Lerp(BinocularCamera.fieldOfView, Zoom, Time.deltaTime * 10.0f);
				StreetManager.CurrentlyActiveJukebox.volume = (BinocularCamera.fieldOfView / 60) * .5f;

				if (Input.GetButtonUp(InputNames.Xbox_B))
				{
					BinocularCamera.gameObject.SetActive(false);
					BinocularRenderer.enabled = true;
					BinocularOverlay.SetActive(false);

					RotationX = 0;
					RotationY = 0;
					Zoom = 60;

					StreetManager.CurrentlyActiveJukebox.volume = .5f;

					PromptBar.ClearButtons();
					PromptBar.Show = false;

					Yandere.CanMove = true;
				}
			}
		}
	}

	void UpdateShopInterface()
	{
		Yandere.MainCamera.GetComponent<RPG_Camera>().enabled = false;

		StreetShopInterface.StoreNameLabel.text = StoreName;
		StreetShopInterface.MoneyLabel.text = "$" + PlayerGlobals.Money.ToString("F2", System.Globalization.NumberFormatInfo.InvariantInfo);
        StreetShopInterface.Shopkeeper.mainTexture = ShopkeeperPortraits[1];
		StreetShopInterface.SpeechBubbleLabel.text = ShopkeeperSpeeches[1];
		StreetShopInterface.ShopkeeperPortraits = ShopkeeperPortraits;
		StreetShopInterface.ShopkeeperSpeeches = ShopkeeperSpeeches;
		StreetShopInterface.ShopkeeperPosition = ShopkeeperPosition;
		StreetShopInterface.AdultProducts = AdultProducts;
        StreetShopInterface.SpeechPhase = 0;
		StreetShopInterface.Costs = Costs;
		StreetShopInterface.Limit = Limit;
		StreetShopInterface.Selected = 1;
		StreetShopInterface.Timer = 0;

        StreetShopInterface.Jukebox.clip = StoreTheme;
        StreetShopInterface.Jukebox.Play();

        StreetShopInterface.UpdateHighlight();

		int ID = 1;

		while (ID < 11)
		{
			StreetShopInterface.ProductsLabel[ID].text = Products[ID];
			StreetShopInterface.PricesLabel[ID].text = "$" + Costs[ID];

			if (StreetShopInterface.PricesLabel[ID].text == "$0")
			{
				StreetShopInterface.PricesLabel[ID].text = "";
			}

			if (StoreType == ShopType.Salon)
			{
				StreetShopInterface.PricesLabel[ID].text = "Free";
			}

			ID++;
		}

		StreetShopInterface.UpdateIcons();
	}
}