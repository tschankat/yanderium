using UnityEngine;

public class WeaponMenuScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public InputDeviceScript InputDevice;
	public PauseScreenScript PauseScreen;
	public YandereScript Yandere;
	public InputManagerScript IM;
	public UIPanel KeyboardPanel;
	public UIPanel Panel;

	public Transform KeyboardMenu;

	public bool KeyboardShow = false;
	public bool Released = true;
	public bool Show = false;

	public UISprite[] BG;
	public UISprite[] Outline;
	public UISprite[] Item;

	public UISprite[] KeyboardBG;
	public UISprite[] KeyboardOutline;
	public UISprite[] KeyboardItem;

	public int Selected = 1;

	public Color OriginalColor;

	public Transform Button;

	public float Timer = 0.0f;

	void Start()
	{
		this.KeyboardMenu.localScale = Vector3.zero;
		this.transform.localScale = Vector3.zero;

		this.OriginalColor = this.BG[1].color;

		this.UpdateSprites();
	}

	void Update()
	{
		if (!this.PauseScreen.Show)
		{
			if (this.Yandere.CanMove && !this.Yandere.Aiming || this.Yandere.Chased && !this.Yandere.Sprayed && !this.Yandere.DelinquentFighting)
			{
				if (this.IM.DPadUp && this.IM.TappedUp ||
					this.IM.DPadDown && this.IM.TappedDown ||
					this.IM.DPadLeft && this.IM.TappedLeft ||
					this.IM.DPadRight && this.IM.TappedRight)
				{
					this.Yandere.EmptyHands();

					if (this.IM.DPadLeft || this.IM.DPadRight || this.IM.DPadUp ||
						(this.Yandere.Mask != null))
					{
						this.KeyboardShow = false;
						this.Panel.enabled = true;
						this.Show = true;
					}

					if (this.IM.DPadLeft)
					{
						this.Button.localPosition = new Vector3(-340.0f, 0.0f, 0.0f);
						this.Selected = 1;
					}
					else if (this.IM.DPadRight)
					{
						this.Button.localPosition = new Vector3(340.0f, 0.0f, 0.0f);
						this.Selected = 2;
					}
					else if (this.IM.DPadUp)
					{
						this.Button.localPosition = new Vector3(0.0f, 340.0f, 0.0f);
						this.Selected = 3;
					}
					else if (this.IM.DPadDown)
					{
						if (this.Selected == 4)
						{
							this.Button.localPosition = new Vector3(0.0f, -310.0f, 0.0f);
							this.Selected = 5;
						}
						else
						{
							this.Button.localPosition = new Vector3(0.0f, -190.0f, 0.0f);
							this.Selected = 4;
						}
					}

					this.UpdateSprites();
				}

				if (!this.Yandere.EasterEggMenu.activeInHierarchy)
				{
					if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) ||
						Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4) ||
						Input.GetKeyDown(KeyCode.Alpha5))
					{
						this.Yandere.EmptyHands();

						this.KeyboardPanel.enabled = true;
						this.KeyboardShow = true;
						this.Show = false;

						this.Timer = 0.0f;

						if (Input.GetKeyDown(KeyCode.Alpha1))
						{
							this.Selected = 4;

							if (this.Yandere.Equipped > 0)
							{
								this.Yandere.CharacterAnimation["f02_reachForWeapon_00"].time = 0;
								this.Yandere.ReachWeight = 1;

								this.Yandere.Unequip();
							}

							if (this.Yandere.PickUp != null)
							{
								this.Yandere.PickUp.Drop();
							}

							this.Yandere.Mopping = false;
						}
						else if (Input.GetKeyDown(KeyCode.Alpha2))
						{
							this.Selected = 1;

							this.Equip();
						}
						else if (Input.GetKeyDown(KeyCode.Alpha3))
						{
							this.Selected = 2;

							this.Equip();
						}
						else if (Input.GetKeyDown(KeyCode.Alpha4))
						{
							this.Selected = 3;

							if (this.Yandere.Container != null)
							{
								if (this.Yandere.ObstacleDetector.Obstacles == 0)
								{
									// [af] Added "gameObject" for C# compatibility.
									this.Yandere.ObstacleDetector.gameObject.SetActive(false);

									this.Yandere.Container.Drop();
									this.UpdateSprites();
								}
							}
						}
						else if (Input.GetKeyDown(KeyCode.Alpha5))
						{
							this.Selected = 5;

							this.DropMask();
						}

						this.UpdateSprites();
					}
				}
			}

			if (this.Yandere.CanMove || this.Yandere.Chased && !this.Yandere.Sprayed && !this.StudentManager.PinningDown)
			{
				if (!this.Show)
				{
					if (Input.GetAxis("DpadY") < -0.50f)
					{
						//Debug.Log("This code was triggered.");

						if (this.Yandere.Equipped > 0)
						{
							//Debug.Log("Yandere-chan is currently carrying a: " + this.Yandere.EquippedWeapon.gameObject.name +
								      //". Is it concealable? " + this.Yandere.EquippedWeapon.Concealable);

							if (this.Yandere.EquippedWeapon.Concealable)
							{
								this.Yandere.CharacterAnimation["f02_reachForWeapon_00"].time = 0;
								this.Yandere.ReachWeight = 1;
							}

							this.Yandere.Unequip();
						}

						if (this.Yandere.PickUp != null)
						{
							this.Yandere.PickUp.Drop();
						}

						this.Yandere.Mopping = false;
					}
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						if (this.Selected < 3)
						{
							if (this.Yandere.Weapon[this.Selected] != null)
							{
								this.Equip();
							}
						}
						else if (this.Selected == 3)
						{
							if (this.Yandere.Container != null)
							{
								if (this.Yandere.ObstacleDetector.Obstacles == 0)
								{
									// [af] Added "gameObject" for C# compatibility.
									this.Yandere.ObstacleDetector.gameObject.SetActive(false);

									this.Yandere.Container.Drop();
									this.UpdateSprites();
								}
							}
						}
						else if (this.Selected == 5)
						{
							this.DropMask();
						}
						else
						{
							if (this.Yandere.Equipped > 0)
							{
								this.Yandere.Unequip();
							}

							if (this.Yandere.PickUp != null)
							{
								this.Yandere.PickUp.Drop();
							}

							this.Yandere.Mopping = false;
						}
					}

					if (Input.GetButtonDown(InputNames.Xbox_B))
					{
						this.Show = false;
					}
				}
			}
		}

		if (!this.Show)
		{
			if (this.transform.localScale.x > 0.10f)
			{
				this.transform.localScale = Vector3.Lerp(
					this.transform.localScale, Vector3.zero, Time.deltaTime * 10.0f);
			}
			else
			{
				if (this.Panel.enabled)
				{
					this.transform.localScale = Vector3.zero;
					this.Panel.enabled = false;
				}
			}
		}
		else
		{
			this.transform.localScale = Vector3.Lerp(
				this.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);

			if (!this.Yandere.CanMove || this.Yandere.Aiming || this.PauseScreen.Show || 
				(this.InputDevice.Type == InputDeviceType.MouseAndKeyboard))
			{
				if (!this.Yandere.Chased || this.Yandere.Sprayed)
				{
					this.Show = false;
				}
			}
		}

		if (!this.KeyboardShow)
		{
			if (this.KeyboardMenu.localScale.x > 0.10f)
			{
				this.KeyboardMenu.localScale = Vector3.Lerp(
					this.KeyboardMenu.localScale, Vector3.zero, Time.deltaTime * 10.0f);
			}
			else
			{
				if (this.KeyboardPanel.enabled)
				{
					this.KeyboardMenu.localScale = Vector3.zero;
					this.KeyboardPanel.enabled = false;
				}
			}
		}
		else
		{
			this.KeyboardMenu.localScale = Vector3.Lerp(
				this.KeyboardMenu.localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);

			this.Timer += Time.deltaTime;

			if (this.Timer > 3.0f)
			{
				this.KeyboardShow = false;
			}

			if (!this.Yandere.CanMove || this.Yandere.Aiming || this.PauseScreen.Show || 
				(this.InputDevice.Type == InputDeviceType.Gamepad) ||
				Input.GetButton(InputNames.Xbox_Y))
			{
				this.KeyboardShow = false;
			}
		}
	}

	public void Equip()
	{
		if (this.Yandere.Weapon[this.Selected] != null)
		{
			this.Yandere.CharacterAnimation["f02_reachForWeapon_00"].time = 0;
			this.Yandere.ReachWeight = 1;

			if (this.Yandere.PickUp != null)
			{
				this.Yandere.PickUp.Drop();
			}

			if (this.Yandere.Equipped == 3)
			{
				this.Yandere.Weapon[3].Drop();
			}

			if (this.Yandere.Weapon[1] != null)
			{
				// [af] Added "gameObject" for C# compatibility.
				this.Yandere.Weapon[1].gameObject.SetActive(false);
			}

			if (this.Yandere.Weapon[2] != null)
			{
				// [af] Added "gameObject" for C# compatibility.
				this.Yandere.Weapon[2].gameObject.SetActive(false);
			}

			this.Yandere.Equipped = this.Selected;

			// [af] Added "gameObject" for C# compatibility.
			this.Yandere.EquippedWeapon.gameObject.SetActive(true);

			if (this.Yandere.EquippedWeapon.Flaming)
			{
				this.Yandere.EquippedWeapon.FireEffect.Play();
			}

			if (!this.Yandere.Gloved)
			{
				this.Yandere.EquippedWeapon.FingerprintID = 100;
			}

			this.Yandere.StudentManager.UpdateStudents();
			this.Yandere.WeaponManager.UpdateLabels();

			if (this.Yandere.EquippedWeapon.Suspicious)
			{
				if (!this.Yandere.WeaponWarning)
				{
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Armed);

					this.Yandere.WeaponWarning = true;
				}
			}
			else
			{
				this.Yandere.WeaponWarning = false;
			}

			AudioSource.PlayClipAtPoint(this.Yandere.EquippedWeapon.EquipClip, Camera.main.transform.position);

			this.Show = false;
		}
	}

	public void UpdateSprites()
	{
		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < 3; ID++)
		{
			UISprite keyboardBackground = this.KeyboardBG[ID];
			UISprite background = this.BG[ID];

			if (this.Selected == ID)
			{
				keyboardBackground.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
				background.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			}
			else
			{
				keyboardBackground.color = this.OriginalColor;
				background.color = this.OriginalColor;
			}

			UISprite item = this.Item[ID];
			UISprite outline = this.Outline[ID];
			UISprite keyboardItem = this.KeyboardItem[ID];
			UISprite keyboardOutline = this.KeyboardOutline[ID];

			if (this.Yandere.Weapon[ID] == null)
			{
				item.color = new Color(
					item.color.r,
					item.color.g,
					item.color.b,
					0.0f);

				background.color = new Color(
					background.color.r,
					background.color.g,
					background.color.b,
					0.50f);

				outline.color = new Color(
					outline.color.r,
					outline.color.g,
					outline.color.b,
					0.50f);

				keyboardItem.color = new Color(
					keyboardItem.color.r,
					keyboardItem.color.g,
					keyboardItem.color.b,
					0.0f);

				keyboardBackground.color = new Color(
					keyboardBackground.color.r,
					keyboardBackground.color.g,
					keyboardBackground.color.b,
					0.50f);

				keyboardOutline.color = new Color(
					keyboardOutline.color.r,
					keyboardOutline.color.g,
					keyboardOutline.color.b,
					0.50f);
			}
			else
			{
				item.spriteName = this.Yandere.Weapon[ID].SpriteName;

				item.color = new Color(
					item.color.r,
					item.color.g,
					item.color.b,
					1.0f);

				background.color = new Color(
					background.color.r,
					background.color.g,
					background.color.b,
					1.0f);

				outline.color = new Color(
					outline.color.r,
					outline.color.g,
					outline.color.b,
					1.0f);

				keyboardItem.spriteName = this.Yandere.Weapon[ID].SpriteName;

				keyboardItem.color = new Color(
					keyboardItem.color.r,
					keyboardItem.color.g,
					keyboardItem.color.b,
					1.0f);

				keyboardBackground.color = new Color(
					keyboardBackground.color.r,
					keyboardBackground.color.g,
					keyboardBackground.color.b,
					1.0f);

				keyboardOutline.color = new Color(
					keyboardOutline.color.r,
					keyboardOutline.color.g,
					keyboardOutline.color.b,
					1.0f);
			}
		}

		UISprite keyboardItem3 = this.KeyboardItem[3];
		UISprite item3 = this.Item[3];
		UISprite keyboardBackground3 = this.KeyboardBG[3];
		UISprite background3 = this.BG[3];
		UISprite outline3 = this.Outline[3];
		UISprite keyboardOutline3 = this.KeyboardOutline[3];

		if (this.Yandere.Container == null)
		{
			keyboardItem3.color = new Color(
				keyboardItem3.color.r,
				keyboardItem3.color.g,
				keyboardItem3.color.b,
				0.0f);

			item3.color = new Color(
				item3.color.r,
				item3.color.g,
				item3.color.b,
				0.0f);

			if (this.Selected == 3)
			{
				keyboardBackground3.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
				background3.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			}
			else
			{
				keyboardBackground3.color = this.OriginalColor;
				background3.color = this.OriginalColor;
			}

			background3.color = new Color(
				background3.color.r,
				background3.color.g,
				background3.color.b,
				0.50f);

			outline3.color = new Color(
				outline3.color.r,
				outline3.color.g,
				outline3.color.b,
				0.50f);

			keyboardBackground3.color = new Color(
				keyboardBackground3.color.r,
				keyboardBackground3.color.g,
				keyboardBackground3.color.b,
				0.50f);

			keyboardOutline3.color = new Color(
				keyboardOutline3.color.r,
				keyboardOutline3.color.g,
				keyboardOutline3.color.b,
				0.50f);
		}
		else
		{
			item3.color = new Color(
				item3.color.r,
				item3.color.g,
				item3.color.b,
				1.0f);

			background3.color = new Color(
				this.OriginalColor.r,
				this.OriginalColor.g,
				this.OriginalColor.b,
				1.0f);

			outline3.color = new Color(
				outline3.color.r,
				outline3.color.g,
				outline3.color.b,
				1.0f);

			keyboardItem3.spriteName = this.Yandere.Container.SpriteName;

			keyboardItem3.color = new Color(
				keyboardItem3.color.r,
				keyboardItem3.color.g,
				keyboardItem3.color.b,
				1.0f);

			keyboardBackground3.color = new Color(
				this.OriginalColor.r,
				this.OriginalColor.g,
				this.OriginalColor.b,
				1.0f);

			keyboardOutline3.color = new Color(
				keyboardOutline3.color.r,
				keyboardOutline3.color.g,
				keyboardOutline3.color.b,
				1.0f);
		}

		UISprite keyboardItem5 = this.KeyboardItem[5];
		UISprite item5 = this.Item[5];
		UISprite keyboardBackground5 = this.KeyboardBG[5];
		UISprite background5 = this.BG[5];
		UISprite outline5 = this.Outline[5];
		UISprite keyboardOutline5 = this.KeyboardOutline[5];

		if (this.Yandere.Mask == null)
		{
			keyboardItem5.color = new Color(
				keyboardItem5.color.r,
				keyboardItem5.color.g,
				keyboardItem5.color.b,
				0.0f);

			item5.color = new Color(
				item5.color.r,
				item5.color.g,
				item5.color.b,
				0.0f);

			if (this.Selected == 5)
			{
				keyboardBackground5.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
				background5.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			}
			else
			{
				keyboardBackground5.color = this.OriginalColor;
				background5.color = this.OriginalColor;
			}

			background5.color = new Color(
				background5.color.r,
				background5.color.g,
				background5.color.b,
				0.50f);

			outline5.color = new Color(
				outline5.color.r,
				outline5.color.g,
				outline5.color.b,
				0.50f);

			keyboardBackground5.color = new Color(
				keyboardBackground5.color.r,
				keyboardBackground5.color.g,
				keyboardBackground5.color.b,
				0.50f);

			keyboardOutline5.color = new Color(
				keyboardOutline5.color.r,
				keyboardOutline5.color.g,
				keyboardOutline5.color.b,
				0.50f);
		}
		else
		{
			keyboardItem5.color = new Color(
				keyboardItem5.color.r,
				keyboardItem5.color.g,
				keyboardItem5.color.b,
				1.0f);

			item5.color = new Color(
				item5.color.r,
				item5.color.g,
				item5.color.b,
				1.0f);

			background5.color = new Color(
				this.OriginalColor.r,
				this.OriginalColor.g,
				this.OriginalColor.b,
				1.0f);

			outline5.color = new Color(
				outline5.color.r,
				outline5.color.g,
				outline5.color.b,
				1.0f);

			keyboardItem5.color = new Color(
				keyboardItem5.color.r,
				keyboardItem5.color.g,
				keyboardItem5.color.b,
				1.0f);

			keyboardBackground5.color = new Color(
				this.OriginalColor.r,
				this.OriginalColor.g,
				this.OriginalColor.b,
				1.0f);

			keyboardOutline5.color = new Color(
				keyboardOutline5.color.r,
				keyboardOutline5.color.g,
				keyboardOutline5.color.b,
				1.0f);
		}

		if (this.Selected == 4)
		{
			this.KeyboardBG[4].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			this.BG[4].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		}
		else
		{
			this.KeyboardBG[4].color = this.OriginalColor;
			this.BG[4].color = this.OriginalColor;
		}
	}

	void DropMask()
	{
		if (this.Yandere.Mask != null)
		{
			this.StudentManager.CanAnyoneSeeYandere();

			if (!this.StudentManager.YandereVisible && !this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
				this.Yandere.Mask.Drop();
				this.UpdateSprites();

				this.StudentManager.UpdateStudents();
			}
			else
			{
				this.Yandere.NotificationManager.CustomText = "Not now. Too suspicious.";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
			}
		}
	}
}
