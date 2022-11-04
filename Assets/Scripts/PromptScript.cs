using UnityEngine;

// [af] This enumeration should be fairly general since it might eventually encompass
// all "interactable" objects in the game. "Unknown" is the catch-all for unimplemented
// prompt owner types.
// - Some ideas for types: Door, Item, Entity, Switch, etc.
public enum PromptOwnerType
{
	Unknown,
	Door,
	Person
}

public class PromptScript : MonoBehaviour
{
	public PauseScreenScript PauseScreen;
	public StudentScript MyStudent;
	public YandereScript Yandere;

	public GameObject[] ButtonObject;
	public GameObject SpeakerObject;
	public GameObject CircleObject;
	public GameObject LabelObject;

	public PromptParentScript PromptParent;
	public Collider MyCollider;
	public Camera MainCamera;
	public Camera UICamera;

	public bool[] AcceptingInput;
	public bool[] ButtonActive;
	public bool[] HideButton;

	public UISprite[] Button;
	public UISprite[] Circle;
	public UILabel[] Label;

	public UISprite Speaker;
	public UISprite Square;

	public float[] OffsetX;
	public float[] OffsetY;
	public float[] OffsetZ;

	public string[] Text;

	public PromptOwnerType OwnerType;

	public bool DisableAtStart = false;
	public bool Suspicious = false;
	public bool Debugging = false;
	public bool SquareSet = false;
	public bool Carried = false;

	[Tooltip("This means that the prompt's renderer is within the camera's cone of vision.")]
	public bool InSight = false;

	[Tooltip("This means that a raycast can hit the prompt's collider.")]
	public bool InView = false;

	public bool NoCheck = false;
	public bool Attack = false;
	public bool Weapon = false;
	public bool Noisy = false;
	public bool Local = true;

	public float RelativePosition = 0.0f;
	public float MaximumDistance = 5.0f;
	public float MinimumDistance = 0.0f;
	public float DistanceSqr = 0.0f;
	public float Height = 0.0f;

	public int ButtonHeld = 0;
	public int BloodMask = 0;
	public int Priority = 0;
	public int ID = 0;

	public GameObject YandereObject;
	public Transform RaycastTarget;

	public float MinimumDistanceSqr; //{ get { return this.MinimumDistance * this.MinimumDistance; } }
	public float MaximumDistanceSqr; //{ get { return this.MaximumDistance * this.MaximumDistance; } }

	void Awake()
	{
		if (this.Student == null)
		{
			MinimumDistanceSqr = this.MinimumDistance * this.MinimumDistance;
			MaximumDistanceSqr = this.MaximumDistance * this.MaximumDistance;
		}
		else
		{
			MinimumDistanceSqr = MinimumDistance;
			MaximumDistanceSqr = MaximumDistance;
		}

		this.DistanceSqr = float.PositiveInfinity;

		this.OwnerType = this.DecideOwnerType();

		if (this.RaycastTarget == null)
		{
			this.RaycastTarget = this.transform;
		}

		if (this.OffsetZ.Length == 0)
		{
			this.OffsetZ = new float[4];
		}

		if (this.Yandere == null)
		{
			this.YandereObject = GameObject.Find("YandereChan");

			if (this.YandereObject != null)
			{
				this.Yandere = this.YandereObject.GetComponent<YandereScript>();
			}
		}

		if (this.Yandere != null)
		{
			/*
			if (this.transform.position.z > -38)
			{
				PromptManagerScript PromptManager = GameObject.Find("PromptManager").GetComponent<PromptManagerScript>();
				PromptManager.Prompts[PromptManager.ID] = this;
				PromptManager.ID++;
			}
			*/

			this.PauseScreen = this.Yandere.PauseScreen;
			this.PromptParent = this.Yandere.PromptParent;
			this.UICamera = this.Yandere.UICamera;
			this.MainCamera = this.Yandere.MainCamera;

			if (this.Noisy)
			{
				this.Speaker = Instantiate(this.SpeakerObject, this.transform.position,
					Quaternion.identity).GetComponent<UISprite>();
				
				this.Speaker.transform.parent = this.PromptParent.transform;
				this.Speaker.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
				this.Speaker.transform.localEulerAngles = Vector3.zero;
				this.Speaker.enabled = false;
			}

			this.Square = Instantiate(this.PromptParent.SquareObject,
				this.transform.position, Quaternion.identity).GetComponent<UISprite>();
			
			this.Square.transform.parent = this.PromptParent.transform;
			this.Square.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			this.Square.transform.localEulerAngles = Vector3.zero;

			Color squareColor = this.Square.color;
			squareColor.a = 0.0f;
			this.Square.color = squareColor;

			this.Square.enabled = false;

			// [af] Converted while loop to for loop.
			for (this.ID = 0; this.ID < 4; this.ID++)
			{
				if (this.ButtonActive[this.ID])
				{
					this.Button[this.ID] = Instantiate(this.ButtonObject[this.ID],
						this.transform.position, Quaternion.identity).GetComponent<UISprite>();

					UISprite button = this.Button[this.ID];
					button.transform.parent = this.PromptParent.transform;
					button.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
					button.transform.localEulerAngles = Vector3.zero;
					button.color = new Color(
						button.color.r,
						button.color.g,
						button.color.b,
						0.0f);

					button.enabled = false;

					this.Circle[this.ID] = Instantiate(this.CircleObject,
						this.transform.position, Quaternion.identity).GetComponent<UISprite>();

					UISprite circle = this.Circle[this.ID];
					circle.transform.parent = this.PromptParent.transform;
					circle.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
					circle.transform.localEulerAngles = Vector3.zero;
					circle.color = new Color(
						circle.color.r,
						circle.color.g,
						circle.color.b,
						0.0f);

					circle.enabled = false;

					this.Label[this.ID] = Instantiate(this.LabelObject,
						this.transform.position, Quaternion.identity).GetComponent<UILabel>();

					UILabel label = this.Label[this.ID];
					label.transform.parent = this.PromptParent.transform;
					label.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
					label.transform.localEulerAngles = Vector3.zero;
					label.color = new Color(
						label.color.r,
						label.color.g,
						label.color.b,
						0.0f);

					label.enabled = false;

					if (this.Suspicious)
					{
						label.color = new Color(1.0f, 0.0f, 0.0f, 0.0f);
					}

					label.text = "     " + this.Text[ID];
				}

				this.AcceptingInput[ID] = true;
			}

			this.BloodMask = 1 << 1;
			this.BloodMask |= 1 << 2;
			this.BloodMask |= 1 << 9;
			this.BloodMask |= 1 << 13;
			this.BloodMask |= 1 << 14;
			this.BloodMask |= 1 << 16;
			this.BloodMask |= 1 << 21;
			this.BloodMask = ~this.BloodMask;
		}
		//else
		//{
			//this.PauseScreen = GameObject.Find("PauseScreen").GetComponent<PauseScreenScript>();
			//this.PromptParent = GameObject.Find("PromptParent").GetComponent<PromptParentScript>();
			//this.UICamera = GameObject.Find("UI Camera").GetComponent<Camera>();
			//this.MainCamera = Camera.main;
		//}
	}

	void Start()
	{
		// [af] This check isn't in Awake() because Hide() depends on YandereScript.
		if (this.DisableAtStart)
		{
			this.Hide();
			this.enabled = false;
		}
	}

	//float MinimumDistanceSqr { get { return this.MinimumDistance * this.MinimumDistance; } }
	//float MaximumDistanceSqr { get { return this.MaximumDistance * this.MaximumDistance; } }

	// [af] This should be called once at object initialization to determine the type
	// of the prompt owner.
	PromptOwnerType DecideOwnerType()
	{
		if (this.GetComponent<DoorScript>() != null)
		{
			// The prompt owner is a door.
			return PromptOwnerType.Door;
		}
		else
		{
			return PromptOwnerType.Unknown;
		}
	}

	bool AllowedWhenCrouching(PromptOwnerType ownerType)
	{
		return ownerType == PromptOwnerType.Door;
	}

	bool AllowedWhenCrawling(PromptOwnerType ownerType)
	{
		// [af] None yet, maybe in the future.
		return false;
	}

	public float Timer = 0.0f;
	public bool Student;
	public bool Door;

	void Update()
	{
		if (!this.PauseScreen.Show)
		{
			Vector3 promptPosition;

			if (this.InView)
			{
				//bool Proceed;

				if (this.MyStudent == null)
				{
					promptPosition = new Vector3(
						this.transform.position.x,
						this.Yandere.transform.position.y,
						this.transform.position.z);

					this.DistanceSqr = (promptPosition - this.Yandere.transform.position).sqrMagnitude;

					//Proceed = this.DistanceSqr < this.MaximumDistanceSqr;
				}
				else
				{
					this.DistanceSqr = this.MyStudent.DistanceToPlayer;

					//Proceed = this.DistanceSqr < this.MaximumDistance;
				}

				//this.DistanceSqr = Vector3.Distance(promptPosition, this.Yandere.transform.position);
				//this.DistanceSqr = (promptPosition - this.Yandere.transform.position).sqrMagnitude;

				if (this.DistanceSqr < this.MaximumDistanceSqr)
				{
					//this.Timer += Time.deltaTime;

					//if (this.Timer > 10 || this.NoCheck)
					//{
						//this.Timer = 0;

						//Debug.Log("Within maximum distance.");

						NoCheck = true;

						//bool isStanding = this.Yandere.Stance.Current == StanceType.Standing;

						bool isCrouching = this.Yandere.Stance.Current == StanceType.Crouching;
						bool isCrawling = this.Yandere.Stance.Current == StanceType.Crawling;

						if (this.Yandere.CanMove &&
							(!isCrouching || this.AllowedWhenCrouching(this.OwnerType)) &&
							(!isCrawling || this.AllowedWhenCrawling(this.OwnerType)) &&
							!this.Yandere.Aiming && !this.Yandere.Mopping && !this.Yandere.NearSenpai)
						{
							RaycastHit hit;

							#if UNITY_EDITOR
							if (this.Debugging){
							Debug.DrawLine(this.Yandere.Eyes.position + (Vector3.down * this.Height),
								this.RaycastTarget.position, Color.green);
							}
							#endif

							if (Physics.Linecast(this.Yandere.Eyes.position + (Vector3.down * this.Height),
								this.RaycastTarget.position, out hit, this.BloodMask))
							{
								#if UNITY_EDITOR
								if (this.Debugging){Debug.Log("We hit a collider named " + hit.collider.name);}
								#endif

								// [af] Converted if/else statement to boolean expression.
								this.InSight = hit.collider == this.MyCollider;
							}

							if (this.Carried || this.InSight)
							{
								this.SquareSet = false;
								this.Hidden = false;

								// [af] Moved this declaration out of the for loop scope since it's used
								// in another scope later on in the JS code.
								Vector2 WorldButtonPosition = Vector2.zero;

								// [af] Converted while loop to for loop.
								for (this.ID = 0; this.ID < 4; this.ID++)
								{
									if (this.ButtonActive[this.ID])
									{
                                        if (!this.Button[this.ID].gameObject.activeInHierarchy)
                                        {
                                            Button[this.ID].gameObject.SetActive(true);
                                        }

                                        float Angle = Vector3.Angle(Yandere.MainCamera.transform.forward, Yandere.MainCamera.transform.position - transform.position);

										if (Angle > 90)
										{
											if (this.Local)
											{
												Vector2 LocalButtonPosition = MainCamera.WorldToScreenPoint(
													this.transform.position +
													(this.transform.right * this.OffsetX[this.ID]) +
													(this.transform.up * this.OffsetY[this.ID]) +
													(this.transform.forward * this.OffsetZ[this.ID]));
												this.Button[this.ID].transform.position = this.UICamera.ScreenToWorldPoint(
													new Vector3(LocalButtonPosition.x, LocalButtonPosition.y, 1.0f));
												this.Circle[this.ID].transform.position = this.UICamera.ScreenToWorldPoint(
													new Vector3(LocalButtonPosition.x, LocalButtonPosition.y, 1.0f));

												if (!SquareSet)
												{
													this.Square.transform.position = this.UICamera.ScreenToWorldPoint (
														new Vector3 (LocalButtonPosition.x, LocalButtonPosition.y, 1.0f));

													SquareSet = true;
												}

											    Vector2 LocalLabelPosition = MainCamera.WorldToScreenPoint(
													this.transform.position +
													(this.transform.right * this.OffsetX[this.ID]) +
													(this.transform.up * this.OffsetY[this.ID]) +
													(this.transform.forward * this.OffsetZ[this.ID]));
												this.Label[this.ID].transform.position = this.UICamera.ScreenToWorldPoint(
													new Vector3(LocalLabelPosition.x + this.OffsetX[this.ID], LocalLabelPosition.y, 1.0f));

												this.RelativePosition = LocalButtonPosition.x;
											}
											else
											{
												// [af] Moved WorldButtonPosition declaration out of this scope since it's used
												// in another scope later on in the JS code.
												WorldButtonPosition = MainCamera.WorldToScreenPoint(
													this.transform.position +
													new Vector3(this.OffsetX[this.ID], this.OffsetY[this.ID], this.OffsetZ[this.ID]));
												this.Button[this.ID].transform.position = this.UICamera.ScreenToWorldPoint(
													new Vector3(WorldButtonPosition.x, WorldButtonPosition.y, 1.0f));
												this.Circle[this.ID].transform.position = this.UICamera.ScreenToWorldPoint(
													new Vector3(WorldButtonPosition.x, WorldButtonPosition.y, 1.0f));

												if (!SquareSet)
												{
													this.Square.transform.position = this.UICamera.ScreenToWorldPoint(
														new Vector3(WorldButtonPosition.x, WorldButtonPosition.y, 1.0f));
													SquareSet = true;
												}

												Vector2 WorldLabelPosition = MainCamera.WorldToScreenPoint(
													this.transform.position +
													new Vector3(this.OffsetX[this.ID], this.OffsetY[this.ID], this.OffsetZ[this.ID]));
												this.Label[this.ID].transform.position = this.UICamera.ScreenToWorldPoint(
													new Vector3(WorldLabelPosition.x + this.OffsetX[this.ID], WorldLabelPosition.y, 1.0f));

												this.RelativePosition = WorldButtonPosition.x;
											}

											if (!this.HideButton[this.ID])
											{
												this.Square.enabled = true;

												this.Square.color = new Color(
													this.Square.color.r, this.Square.color.g,
													this.Square.color.b, 1.0f);
											}
										}
									}
								}

								if (this.Noisy)
								{
									this.Speaker.transform.position = this.UICamera.ScreenToWorldPoint(
										new Vector3(WorldButtonPosition.x, WorldButtonPosition.y + 40.0f, 1.0f));
								}
								
								if (this.DistanceSqr < this.MinimumDistanceSqr)
								{
									if (this.Yandere.NearestPrompt == null)
									{
										this.Yandere.NearestPrompt = this;
									}
									else
									{
										if (Mathf.Abs(this.RelativePosition - (Screen.width * 0.50f)) <
											Mathf.Abs(this.Yandere.NearestPrompt.RelativePosition - (Screen.width * 0.50f)))
										{
											this.Yandere.NearestPrompt = this;
										}
									}

									#if UNITY_EDITOR
									if (this.Debugging){Debug.Log("The nearest prompt to Yandere-chan is: " + this.Yandere.NearestPrompt);}
									#endif

									if (this.Yandere.NearestPrompt == this)
									{
										this.Square.enabled = false;

										this.Square.color = new Color(
											this.Square.color.r, this.Square.color.g,
											this.Square.color.b, 0.0f);

										// [af] Converted while loop to for loop.
										for (this.ID = 0; this.ID < 4; this.ID++)
										{
											if (this.ButtonActive[this.ID])
											{
												if (!this.Button[this.ID].enabled)
												{
													this.Button[this.ID].enabled = true;
													this.Circle[this.ID].enabled = true;
													this.Label[this.ID].enabled = true;
												}

												this.Button[this.ID].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
												this.Circle[this.ID].color = new Color(0.50f, 0.50f, 0.50f, 1.0f);

												Color labelColor = this.Label[this.ID].color;
												labelColor.a = 1.0f;
												this.Label[this.ID].color = labelColor;

												if (this.Speaker != null)
												{
													this.Speaker.enabled = true;

													Color speakerColor = this.Speaker.color;
													speakerColor.a = 1.0f;
													this.Speaker.color = speakerColor;
												}
											}
										}

										if (Input.GetButton(InputNames.Xbox_A))
										{
											this.ButtonHeld = 1;
										}
										else if (Input.GetButton(InputNames.Xbox_B))
										{
											this.ButtonHeld = 2;
										}
										else if (Input.GetButton(InputNames.Xbox_X))
										{
											this.ButtonHeld = 3;
										}
										else if (Input.GetButton(InputNames.Xbox_Y))
										{
											this.ButtonHeld = 4;
										}
										else
										{
											this.ButtonHeld = 0;
										}

										if (this.ButtonHeld > 0)
										{
											// [af] Converted while loop to for loop.
											for (this.ID = 0; this.ID < 4; this.ID++)
											{
												if (this.ButtonActive[this.ID] && (this.ID != (this.ButtonHeld - 1)) ||
													this.HideButton[this.ID])
												{
													if (this.Circle[this.ID] != null)
													{
														this.Circle[this.ID].fillAmount = 1.0f;
													}
												}
											}

											if (this.ButtonActive[this.ButtonHeld - 1] &&
												!this.HideButton[this.ButtonHeld - 1] &&
												this.AcceptingInput[this.ButtonHeld - 1])
											{
												if (!this.Yandere.Attacking)
												{
													this.Circle[this.ButtonHeld - 1].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

													if (!this.Attack)
													{
														this.Circle[this.ButtonHeld - 1].fillAmount -= Time.deltaTime * 2.0f;
													}
													else
													{
														this.Circle[this.ButtonHeld - 1].fillAmount = 0.0f;
													}

													this.ID = 0;
												}
											}
										}
										else
										{
											// [af] Converted while loop to for loop.
											for (this.ID = 0; this.ID < 4; this.ID++)
											{
												if (this.ButtonActive[this.ID])
												{
													this.Circle[this.ID].fillAmount = 1.0f;
												}
											}
										}
									}
									else
									{
										this.Square.color = new Color(
											this.Square.color.r, this.Square.color.g,
											this.Square.color.b, 1.0f);

										// [af] Converted while loop to for loop.
										for (this.ID = 0; this.ID < 4; this.ID++)
										{
											if (this.ButtonActive[this.ID])
											{
												UISprite button = this.Button[this.ID];
												UISprite circle = this.Circle[this.ID];
												UILabel label = this.Label[this.ID];
												button.enabled = false;
												circle.enabled = false;
												label.enabled = false;

												Color buttonColor = button.color;
												Color circleColor = circle.color;
												Color labelColor = label.color;
												buttonColor.a = 0.0f;
												circleColor.a = 0.0f;
												labelColor.a = 0.0f;
												button.color = buttonColor;
												circle.color = circleColor;
												label.color = labelColor;
											}
										}

										if (this.Speaker != null)
										{
											this.Speaker.enabled = false;

											Color speakerColor = this.Speaker.color;
											speakerColor.a = 0.0f;
											this.Speaker.color = speakerColor;
										}
									}
								}
								else
								{
									if (this.Yandere.NearestPrompt == this)
									{
										this.Yandere.NearestPrompt = null;
									}

									this.Square.color = new Color(
										this.Square.color.r, this.Square.color.g,
										this.Square.color.b, 1.0f);

									// [af] Converted while loop to for loop.
									for (this.ID = 0; this.ID < 4; this.ID++)
									{
										if (this.ButtonActive[this.ID])
										{
											UISprite button = this.Button[this.ID];
											UISprite circle = this.Circle[this.ID];
											UILabel label = this.Label[this.ID];

											circle.fillAmount = 1.0f;

											button.enabled = false;
											circle.enabled = false;
											label.enabled = false;

											Color buttonColor = button.color;
											Color circleColor = circle.color;
											Color labelColor = label.color;
											buttonColor.a = 0.0f;
											circleColor.a = 0.0f;
											labelColor.a = 0.0f;
											button.color = buttonColor;
											circle.color = circleColor;
											label.color = labelColor;
										}
									}

									if (this.Speaker != null)
									{
										this.Speaker.enabled = false;

										Color speakerColor = this.Speaker.color;
										speakerColor.a = 0.0f;
										this.Speaker.color = speakerColor;
									}
								}

								Color squareColor = this.Square.color;
								squareColor.a = 1.0f;
								this.Square.color = squareColor;

								// [af] Converted while loop to for loop.
								for (this.ID = 0; this.ID < 4; this.ID++)
								{
									if (this.ButtonActive[this.ID])
									{
										if (this.HideButton[this.ID])
										{
											UISprite button = this.Button[this.ID];
											UISprite circle = this.Circle[this.ID];
											UILabel label = this.Label[this.ID];
											button.enabled = false;
											circle.enabled = false;
											label.enabled = false;

											Color buttonColor = button.color;
											Color circleColor = circle.color;
											Color labelColor = label.color;
											buttonColor.a = 0.0f;
											circleColor.a = 0.0f;
											labelColor.a = 0.0f;
											button.color = buttonColor;
											circle.color = circleColor;
											label.color = labelColor;

											if (this.Speaker != null)
											{
												this.Speaker.enabled = false;

												Color speakerColor = this.Speaker.color;
												speakerColor.a = 0.0f;
												this.Speaker.color = speakerColor;
											}
										}
									}
								}
							}
							else
							{
								//if (this.Debugging){Debug.Log("Yandere-chan's raycast is not hitting this object.");}

								this.Hide();
							}
						}
						else
						{
							//if (this.Debugging){Debug.Log("Yandere-chan is in a state which prevents her from being able to interact with propts.");}

							this.Hide();
						}
					//}
					//else
					//{
						//if (this.Debugging){Debug.Log("Yandere-chan is too far away.");}

						//this.Hide();
					//}
				}
				else
				{
					#if UNITY_EDITOR
					if (this.Debugging){Debug.Log("Yandere-chan is too far away.");}
					#endif

					this.Hide();
				}
			}
			else
			{
				//if (this.Debugging){Debug.Log("This object is not in view.");}

				/*
				if (this.Student)
				{
					promptPosition = new Vector3(
						this.transform.position.x,
						this.transform.root.transform.position.y,
						this.transform.position.z);

					this.DistanceSqr = (promptPosition - this.Yandere.transform.position).sqrMagnitude;
				}
				else
				{
				*/

				this.DistanceSqr = float.PositiveInfinity;
				//}

				this.Hide();
			}
		}
		else
		{
			//if (this.Debugging){Debug.Log("The pause screen is showing.");}

			this.Hide();
		}
	}

	void OnBecameVisible()
	{
		this.InView = true;
	}

	void OnBecameInvisible()
	{
		this.InView = false;

		//if (this.Debugging){Debug.Log("Became invisible.");}

		this.Hide();
	}

	public bool Hidden = false;

	public void Hide()
	{
		if (!this.Hidden)
		{
			this.NoCheck = false;
			this.Hidden = true;

			if (this.Yandere != null)
			{
				if (this.Yandere.NearestPrompt == this)
				{
					this.Yandere.NearestPrompt = null;
				}

				if (this.Square == null)
				{
					Debug.Log("Yo, some prompt named " + this.gameObject.name + " apparently doesn't have a ''Square'' Sprite.");
				}

				if (this.Square.enabled)
				{
					this.Square.enabled = false;
					this.Square.color = new Color(
						this.Square.color.r, this.Square.color.g, this.Square.color.b, 0.0f);
				}

				// [af] Converted while loop to for loop.
				for (this.ID = 0; this.ID < 4; this.ID++)
				{
					if (this.ButtonActive[this.ID])
					{
						UISprite button = this.Button[this.ID];

						if (button.enabled)
						{
							UISprite circle = this.Circle[this.ID];
							UILabel label = this.Label[this.ID];

							circle.fillAmount = 1.0f;

							button.enabled = false;
							circle.enabled = false;
							label.enabled = false;

							button.color = new Color(
								button.color.r, button.color.g, button.color.b, 0.0f);
							circle.color = new Color(
								circle.color.r, circle.color.g, circle.color.b, 0.0f);
							label.color = new Color(
								label.color.r, label.color.g, label.color.b, 0.0f);
						}
                    }

                    if (Button[this.ID] != null)
                    {
                        Button[this.ID].gameObject.SetActive(false);
                    }
                }

				if (this.Speaker != null)
				{
					this.Speaker.enabled = false;
					this.Speaker.color = new Color(
						this.Speaker.color.r, this.Speaker.color.g,
						this.Speaker.color.b, 0.0f);
				}
			}
		}
	}
}
