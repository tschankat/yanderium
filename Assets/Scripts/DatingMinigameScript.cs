using UnityEngine;

public class DatingMinigameScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public InputManagerScript InputManager;
	public LoveManagerScript LoveManager;
	public PromptBarScript PromptBar;
	public YandereScript Yandere;
	public StudentScript Suitor;
	public StudentScript Rival;
	public PromptScript Prompt;
	public JsonScript JSON;

	public Transform AffectionSet;
	public Transform OptionSet;

	public GameObject HeartbeatCamera;
	public GameObject SeductionIcon;
	public GameObject PantyIcon;

	public Transform TopicHighlight;
	public Transform ComplimentSet;
	public Transform AffectionBar;
	public Transform Highlight;
	public Transform GiveGift;
	public Transform PeekSpot;
	public Transform[] Options;
	public Transform ShowOff;
	public Transform Topics;
	public Texture X;

	public UISprite[] OpinionIcons;
	public UISprite[] TopicIcons;

	public UITexture[] MultiplierIcons;
	public UILabel[] ComplimentLabels;
	public UISprite[] ComplimentBGs;
	public UILabel MultiplierLabel;
	public UILabel SeductionLabel;
	public UILabel TopicNameLabel;
	public UILabel DialogueLabel;
	public UIPanel DatingSimHUD;
	public UILabel WisdomLabel;
	public UIPanel Panel;

	public UITexture[] GiftIcons;
	public UISprite[] TraitBGs;
	public UISprite[] GiftBGs;
	public UILabel[] Labels;

	public string[] OpinionSpriteNames;
	public string[] Compliments;
	public string[] TopicNames;
	public string[] GiveGifts;
	public string[] Greetings;
	public string[] Farewells;
	public string[] Negatives;
	public string[] Positives;
	public string[] ShowOffs;

	public bool SelectingTopic = false;
	public bool AffectionGrow = false;
	public bool Complimenting = false;
	public bool Matchmaking = false;
	public bool GivingGift = false;
	public bool ShowingOff = false;
	public bool Negative = false;
	public bool SlideOut = false;
	public bool Testing = false;

	public float HighlightTarget = 0.0f;
	public float Affection = 0.0f;
	public float Rotation = 0.0f;
	public float Speed = 0.0f;
	public float Timer = 0.0f;

	public int ComplimentSelected = 1;
	public int TraitSelected = 1;
	public int TopicSelected = 1;
	public int GiftSelected = 1;
	public int Selected = 1;

	public int AffectionLevel = 0;
	public int Multiplier = 0;
	public int Opinion = 0;
	public int Phase = 1;

	public int GiftColumn = 1;
	public int GiftRow = 1;

	public int Column = 1;
	public int Row = 1;

	public int Side = 1;
	public int Line = 1;

	public string CurrentAnim = string.Empty;

	public Color OriginalColor;

	public Camera MainCamera;

	void Start()
	{
		MainCamera = Camera.main;

		this.Affection = DatingGlobals.Affection;

		this.AffectionBar.localScale = new Vector3(
			this.Affection / 100.0f,
			this.AffectionBar.localScale.y,
			this.AffectionBar.localScale.z);

		this.CalculateAffection();

		this.OriginalColor = this.ComplimentBGs[1].color;

		this.ComplimentSet.localScale = Vector3.zero;
		this.GiveGift.localScale = Vector3.zero;
		this.ShowOff.localScale = Vector3.zero;
		this.Topics.localScale = Vector3.zero;

		// [af] Added "gameObject" for C# compatibility.
		this.DatingSimHUD.gameObject.SetActive(false);

		this.DatingSimHUD.alpha = 0.0f;

		for (int ID = 1; ID < 26; ID++)
		{
			if (DatingGlobals.GetTopicDiscussed(ID))
			{
				UISprite topicIcon = this.TopicIcons[ID];
				topicIcon.color = new Color(
					topicIcon.color.r,
					topicIcon.color.g,
					topicIcon.color.b,
					0.50f);
			}
		}

		for (int ID = 1; ID < 11; ID++)
		{
			if (DatingGlobals.GetComplimentGiven(ID))
			{
				UILabel complimentLabel = this.ComplimentLabels[ID];
				complimentLabel.color = new Color(
					complimentLabel.color.r,
					complimentLabel.color.g,
					complimentLabel.color.b,
					0.50f);
			}
		}

		this.UpdateComplimentHighlight();
		this.UpdateTraitHighlight();
		this.UpdateGiftHighlight();
	}

	void CalculateAffection()
	{
		if (this.Affection > 100)
		{
			this.Affection = 100;
		}
			
		if (this.Affection == 0.0f)
		{
			this.AffectionLevel = 0;
		}
		else if (this.Affection < 25.0f)
		{
			this.AffectionLevel = 1;
		}
		else if (this.Affection < 50.0f)
		{
			this.AffectionLevel = 2;
		}
		else if (this.Affection < 75.0f)
		{
			this.AffectionLevel = 3;
		}
		else if (this.Affection < 100.0f)
		{
			this.AffectionLevel = 4;
		}
		else
		{
			this.AffectionLevel = 5;
		}

        //AffectionLevel = Mathf.Ceil(Affection / 25f); AffectionLevel = AffectionLevel > 5 ? 5 : AffectionLevel;

        //Debug.Log("Affection is: " + this.Affection);
        //Debug.Log("AffectionLevel is now: " + this.AffectionLevel);
    }

    void Update()
	{
		if (this.Testing)
		{
			this.Prompt.enabled = true;
		}
		else
		{
			if (this.LoveManager.RivalWaiting)
			{
				if (this.Rival == null)
				{
					this.Suitor = this.StudentManager.Students[this.LoveManager.SuitorID];
					this.Rival = this.StudentManager.Students[this.LoveManager.RivalID];
				}

				if ((this.Rival.MeetTimer > 0.0f) && (this.Suitor.MeetTimer > 0.0f))
				{
					this.Prompt.enabled = true;
				}
			}
			else
			{
				if (this.Prompt.enabled)
				{
					this.Prompt.Hide();
					this.Prompt.enabled = false;
				}
			}
		}

		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Prompt.Circle[0].fillAmount = 1;

			if (!this.Yandere.Chased && this.Yandere.Chasers == 0 && !this.Rival.Hunted)
			{
				this.Suitor.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
				this.Rival.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
				this.Suitor.CharacterAnimation.enabled = true;
				this.Rival.CharacterAnimation.enabled = true;

				this.Suitor.enabled = false;
				this.Rival.enabled = false;

				this.Rival.CharacterAnimation[AnimNames.FemaleSmile].layer = 1;
				this.Rival.CharacterAnimation.Play(AnimNames.FemaleSmile);
				this.Rival.CharacterAnimation[AnimNames.FemaleSmile].weight = 0.0f;

				this.StudentManager.Clock.StopTime = true;
				this.Yandere.RPGCamera.enabled = false;
				this.HeartbeatCamera.SetActive(false);
				this.Yandere.Headset.SetActive(true);
				this.Yandere.CanMove = false;
				this.Yandere.EmptyHands();

				if (this.Yandere.YandereVision)
				{
					this.Yandere.ResetYandereEffects();
					this.Yandere.YandereVision = false;
				}

				this.Yandere.transform.position = this.PeekSpot.position;
				this.Yandere.transform.eulerAngles = this.PeekSpot.eulerAngles;
				this.Yandere.CharacterAnimation.Play(AnimNames.FemaleTreePeeking);

				MainCamera.transform.position = new Vector3(48.0f, 3.0f, -44.0f);
				MainCamera.transform.eulerAngles = new Vector3(15.0f, 90.0f, 0.0f);

				this.WisdomLabel.text = "Wisdom: " + DatingGlobals.GetSuitorTrait(2).ToString();

				this.GiftIcons[1].enabled = CollectibleGlobals.GetGiftPurchased(6);
				this.GiftIcons[2].enabled = CollectibleGlobals.GetGiftPurchased(7);
				this.GiftIcons[3].enabled = CollectibleGlobals.GetGiftPurchased(8);
				this.GiftIcons[4].enabled = CollectibleGlobals.GetGiftPurchased(9);

				this.Matchmaking = true;

				this.UpdateTopics();

				Time.timeScale = 1;
			}
		}

		if (this.Matchmaking)
		{
			if (this.CurrentAnim != string.Empty)
			{
				if (this.Rival.CharacterAnimation[this.CurrentAnim].time >=
					this.Rival.CharacterAnimation[this.CurrentAnim].length)
				{
					this.Rival.CharacterAnimation.Play(this.Rival.IdleAnim);
				}
			}

			if (this.Phase == 1)
			{
				this.Panel.alpha = Mathf.MoveTowards(this.Panel.alpha, 0.0f, Time.deltaTime);

				this.Timer += Time.deltaTime;

				MainCamera.transform.position = Vector3.Lerp(Camera
					.main.transform.position,
					new Vector3(54.0f, 1.25f, -45.25f),
					this.Timer * 0.020f);
				MainCamera.transform.eulerAngles = Vector3.Lerp(
					MainCamera.transform.eulerAngles,
					new Vector3(0.0f, 45.0f, 0.0f),
					this.Timer * 0.020f);

				if (this.Timer > 5)
				{
					this.Suitor.CharacterAnimation.Play(AnimNames.MaleInsertEarpiece);
					this.Suitor.CharacterAnimation[AnimNames.MaleInsertEarpiece].time = 0.0f;
					this.Suitor.CharacterAnimation.Play(AnimNames.MaleInsertEarpiece);

					this.Suitor.Earpiece.SetActive(true);

					MainCamera.transform.position = new Vector3(45.50f, 1.25f, -44.50f);
					MainCamera.transform.eulerAngles = new Vector3(0.0f, -45.0f, 0.0f);
					this.Rotation = -45.0f;

					this.Timer = 0.0f;
					this.Phase++;
				}
			}
			else if (this.Phase == 2)
			{
				this.Timer += Time.deltaTime;

				if (this.Timer > 4.0f)
				{
					this.Suitor.Earpiece.transform.parent = this.Suitor.Head;
					this.Suitor.Earpiece.transform.localPosition = new Vector3(0.0f, -1.12f, 1.14f);
					this.Suitor.Earpiece.transform.localEulerAngles = new Vector3(45.0f, -180.0f, 0.0f);

					MainCamera.transform.position = Vector3.Lerp(
						MainCamera.transform.position,
						new Vector3(45.11f, 1.375f, -44.0f),
						(this.Timer - 4.0f) * 0.020f);
					this.Rotation = Mathf.Lerp(this.Rotation, 90.0f, (this.Timer - 4.0f) * 0.020f);

					MainCamera.transform.eulerAngles = new Vector3(
						MainCamera.transform.eulerAngles.x,
						this.Rotation,
						MainCamera.transform.eulerAngles.z);

					if (this.Rotation > 89.9f)
					{
						this.Rival.CharacterAnimation[AnimNames.FemaleTurnAround].time = 0.0f;
						this.Rival.CharacterAnimation.CrossFade(AnimNames.FemaleTurnAround);

						this.AffectionBar.localScale = new Vector3(
							this.Affection / 100.0f,
							this.AffectionBar.localScale.y,
							this.AffectionBar.localScale.z);

						this.DialogueLabel.text = this.Greetings[this.AffectionLevel];

						this.CalculateMultiplier();

						// [af] Added "gameObject" for C# compatibility.
						this.DatingSimHUD.gameObject.SetActive(true);

						this.Timer = 0.0f;
						this.Phase++;
					}
				}
			}
			else if (this.Phase == 3)
			{
				this.DatingSimHUD.alpha = Mathf.MoveTowards(this.DatingSimHUD.alpha, 1.0f, Time.deltaTime);

				if (this.Rival.CharacterAnimation[AnimNames.FemaleTurnAround].time >=
					this.Rival.CharacterAnimation[AnimNames.FemaleTurnAround].length)
				{
					this.Rival.transform.eulerAngles = new Vector3(
						this.Rival.transform.eulerAngles.x,
						-90.0f,
						this.Rival.transform.eulerAngles.z);

					this.Rival.CharacterAnimation.Play(AnimNames.FemaleTurnAround);
					this.Rival.CharacterAnimation[AnimNames.FemaleTurnAround].time = 0.0f;
					this.Rival.CharacterAnimation[AnimNames.FemaleTurnAround].speed = 0.0f;
					this.Rival.CharacterAnimation.Play(AnimNames.FemaleTurnAround);

					this.Rival.CharacterAnimation.CrossFade(this.Rival.IdleAnim);

					Time.timeScale = 1;

					this.PromptBar.ClearButtons();
					this.PromptBar.Label[0].text = "Confirm";
					this.PromptBar.Label[1].text = "Back";
					this.PromptBar.Label[4].text = "Select";
					this.PromptBar.UpdateButtons();
					this.PromptBar.Show = true;

					this.Phase++;
				}
			}
			else if (this.Phase == 4)
			{
				if (this.AffectionGrow)
				{
					this.Affection = Mathf.MoveTowards(this.Affection, 100.0f, Time.deltaTime * 10.0f);
					this.CalculateAffection();
				}

				this.Rival.Cosmetic.MyRenderer.materials[2].SetFloat("_BlendAmount", this.Affection * 0.010f);
				this.Rival.CharacterAnimation[AnimNames.FemaleSmile].weight = this.Affection * 0.010f;

				this.Highlight.localPosition = new Vector3(
					this.Highlight.localPosition.x,
					Mathf.Lerp(this.Highlight.localPosition.y, this.HighlightTarget, Time.deltaTime * 10.0f),
					this.Highlight.localPosition.z);

				for (int ID = 1; ID < this.Options.Length; ID++)
				{
					Transform option = this.Options[ID];

					// [af] Replaced if/else statement with assignment and ternary expression.
					option.localPosition = new Vector3(
						Mathf.Lerp(option.localPosition.x, (ID == this.Selected) ? 750.0f : 800.0f, Time.deltaTime * 10.0f),
						option.localPosition.y,
						option.localPosition.z);
				}

				this.AffectionBar.localScale = new Vector3(
					Mathf.Lerp(this.AffectionBar.localScale.x, this.Affection / 100.0f, Time.deltaTime * 10.0f),
					this.AffectionBar.localScale.y,
					this.AffectionBar.localScale.z);

				if (!this.SelectingTopic && !this.Complimenting &&
					!this.ShowingOff && !this.GivingGift)
				{
					this.Topics.localScale = Vector3.Lerp(
						this.Topics.localScale, Vector3.zero, Time.deltaTime * 10.0f);
					this.ComplimentSet.localScale = Vector3.Lerp(
						this.ComplimentSet.localScale, Vector3.zero, Time.deltaTime * 10.0f);
					this.ShowOff.localScale = Vector3.Lerp(
						this.ShowOff.localScale, Vector3.zero, Time.deltaTime * 10.0f);
					this.GiveGift.localScale = Vector3.Lerp(
						this.GiveGift.localScale, Vector3.zero, Time.deltaTime * 10.0f);

					if (this.InputManager.TappedUp)
					{
						this.Selected--;
						this.UpdateHighlight();
					}

					if (this.InputManager.TappedDown)
					{
						this.Selected++;
						this.UpdateHighlight();
					}

					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						if (this.Labels[this.Selected].color.a == 1.0f)
						{
							if (this.Selected == 1)
							{
								this.SelectingTopic = true;
								this.Negative = true;
							}
							else if (this.Selected == 2)
							{
								this.SelectingTopic = true;
								this.Negative = false;
							}
							else if (this.Selected == 3)
							{
								this.Complimenting = true;
							}
							else if (this.Selected == 4)
							{
								this.ShowingOff = true;
							}
							else if (this.Selected == 5)
							{
								this.GivingGift = true;
							}
							else if (this.Selected == 6)
							{
								this.PromptBar.ClearButtons();
								this.PromptBar.Label[0].text = "Confirm";
								this.PromptBar.UpdateButtons();

								this.CalculateAffection();

								this.DialogueLabel.text = this.Farewells[this.AffectionLevel];

								this.Phase++;
							}
						}
					}
				}
				else
				{
					if (this.SelectingTopic)
					{
						this.Topics.localScale = Vector3.Lerp(
							this.Topics.localScale,
							new Vector3(1.0f, 1.0f, 1.0f),
							Time.deltaTime * 10.0f);

						if (this.InputManager.TappedUp)
						{
							this.Row--;
							this.UpdateTopicHighlight();
						}
						else if (this.InputManager.TappedDown)
						{
							this.Row++;
							this.UpdateTopicHighlight();
						}
						if (this.InputManager.TappedLeft)
						{
							this.Column--;
							this.UpdateTopicHighlight();
						}
						else if (this.InputManager.TappedRight)
						{
							this.Column++;
							this.UpdateTopicHighlight();
						}

						if (Input.GetButtonDown(InputNames.Xbox_A))
						{
							if (this.TopicIcons[this.TopicSelected].color.a == 1.0f)
							{
								this.SelectingTopic = false;

								UISprite selectedTopicIcon = this.TopicIcons[this.TopicSelected];
								selectedTopicIcon.color = new Color(
									selectedTopicIcon.color.r,
									selectedTopicIcon.color.g,
									selectedTopicIcon.color.b,
									0.50f);

								DatingGlobals.SetTopicDiscussed(this.TopicSelected, true);

								this.DetermineOpinion();

								if (!ConversationGlobals.GetTopicLearnedByStudent(this.Opinion, LoveManager.RivalID))
								{
									ConversationGlobals.SetTopicLearnedByStudent(this.Opinion, LoveManager.RivalID, true);
								}

								if (this.Negative)
								{
									UILabel label1 = this.Labels[1];
									label1.color = new Color(
										label1.color.r,
										label1.color.g,
										label1.color.b,
										0.50f);

									// If the player just insulted something that the character likes...
									if (this.Opinion == 2)
									{
										this.DialogueLabel.text = "Hey! Just so you know, I take offense to that...";

										this.Rival.CharacterAnimation.CrossFade(AnimNames.FemaleRefuse);
										this.CurrentAnim = AnimNames.FemaleRefuse;
										this.Affection -= 1.0f;

										this.CalculateAffection();
									}
									// If the player just insulted something that the character dislikes...
									else if (this.Opinion == 1)
									{
										this.DialogueLabel.text = this.Negatives[this.AffectionLevel];

										this.Rival.CharacterAnimation.CrossFade(AnimNames.FemaleLookDown);
										this.CurrentAnim = AnimNames.FemaleLookDown;
										this.Affection += this.Multiplier;

										this.CalculateAffection();
									}
									else if (this.Opinion == 0)
									{
										this.DialogueLabel.text = "Um...okay.";
									}
								}
								else
								{
									UILabel label2 = this.Labels[2];
									label2.color = new Color(
										label2.color.r,
										label2.color.g,
										label2.color.b,
										0.50f);

									// If the player just praised something that the character likes...
									if (this.Opinion == 2)
									{
										this.DialogueLabel.text = this.Positives[this.AffectionLevel];

										this.Rival.CharacterAnimation.CrossFade(AnimNames.FemaleLookDown);
										this.CurrentAnim = AnimNames.FemaleLookDown;
										this.Affection += this.Multiplier;

										this.CalculateAffection();
									}
									// If the player just praised something that the character dislikes...
									else if (this.Opinion == 1)
									{
										this.DialogueLabel.text = "To be honest with you, I strongly disagree...";

										this.Rival.CharacterAnimation.CrossFade(AnimNames.FemaleRefuse);
										this.CurrentAnim = AnimNames.FemaleRefuse;
										this.Affection -= 1.0f;

										this.CalculateAffection();
									}
									else if (this.Opinion == 0)
									{
										this.DialogueLabel.text = "Um...all right.";
									}
								}

								if (this.Affection > 100.0f)
								{
									this.Affection = 100.0f;
								}
								else if (this.Affection < 0.0f)
								{
									this.Affection = 0.0f;
								}
							}
						}

						if (Input.GetButtonDown(InputNames.Xbox_B))
						{
							this.SelectingTopic = false;
						}
					}
					else if (this.Complimenting)
					{
						this.ComplimentSet.localScale = Vector3.Lerp(
							this.ComplimentSet.localScale,
							new Vector3(1.0f, 1.0f, 1.0f),
							Time.deltaTime * 10.0f);

						if (this.InputManager.TappedUp)
						{
							this.Line--;
							this.UpdateComplimentHighlight();
						}
						else if (this.InputManager.TappedDown)
						{
							this.Line++;
							this.UpdateComplimentHighlight();
						}
						if (this.InputManager.TappedLeft)
						{
							this.Side--;
							this.UpdateComplimentHighlight();
						}
						else if (this.InputManager.TappedRight)
						{
							this.Side++;
							this.UpdateComplimentHighlight();
						}

						if (Input.GetButtonDown(InputNames.Xbox_A))
						{
							if (this.ComplimentLabels[this.ComplimentSelected].color.a == 1.0f)
							{
								UILabel label3 = this.Labels[3];
								label3.color = new Color(
									label3.color.r,
									label3.color.g,
									label3.color.b,
									0.50f);

								this.Complimenting = false;
								this.DialogueLabel.text = this.Compliments[this.ComplimentSelected];
								DatingGlobals.SetComplimentGiven(this.ComplimentSelected, true);

								if ((this.ComplimentSelected == 1) ||
									(this.ComplimentSelected == 4) ||
									(this.ComplimentSelected == 5) ||
									(this.ComplimentSelected == 8) ||
									(this.ComplimentSelected == 9))
								{
									this.Rival.CharacterAnimation.CrossFade(AnimNames.FemaleLookDown);
									this.CurrentAnim = AnimNames.FemaleLookDown;
									this.Affection += this.Multiplier;

									this.CalculateAffection();
								}
								else
								{
									this.Rival.CharacterAnimation.CrossFade(AnimNames.FemaleRefuse);
									this.CurrentAnim = AnimNames.FemaleRefuse;
									this.Affection -= 1.0f;

									this.CalculateAffection();
								}

								if (this.Affection > 100.0f)
								{
									this.Affection = 100.0f;
								}
								else if (this.Affection < 0.0f)
								{
									this.Affection = 0.0f;
								}
							}
						}

						if (Input.GetButtonDown(InputNames.Xbox_B))
						{
							this.Complimenting = false;
						}
					}
					else if (this.ShowingOff)
					{
						this.ShowOff.localScale = Vector3.Lerp(
							this.ShowOff.localScale,
							new Vector3(1.0f, 1.0f, 1.0f),
							Time.deltaTime * 10.0f);

						if (this.InputManager.TappedUp)
						{
							this.TraitSelected--;
							this.UpdateTraitHighlight();
						}
						else if (this.InputManager.TappedDown)
						{
							this.TraitSelected++;
							this.UpdateTraitHighlight();
						}

						if (Input.GetButtonDown(InputNames.Xbox_A))
						{
							UILabel label4 = this.Labels[4];
							label4.color = new Color(
								label4.color.r,
								label4.color.g,
								label4.color.b,
								0.50f);

							this.ShowingOff = false;

							if (this.TraitSelected == 2)
							{
								Debug.Log("Wisdom trait is " + DatingGlobals.GetSuitorTrait(2) + ". Wisdom Demonstrated is " + DatingGlobals.GetTraitDemonstrated(2) + ".");

								if (DatingGlobals.GetSuitorTrait(2) > DatingGlobals.GetTraitDemonstrated(2))
								{
									DatingGlobals.SetTraitDemonstrated(
										2, DatingGlobals.GetTraitDemonstrated(2) + 1);

									this.DialogueLabel.text = this.ShowOffs[this.AffectionLevel];

									this.Rival.CharacterAnimation.CrossFade(AnimNames.FemaleLookDown);
									this.CurrentAnim = AnimNames.FemaleLookDown;
									this.Affection += this.Multiplier;

									this.CalculateAffection();
								}
								else if (DatingGlobals.GetSuitorTrait(2) == 0)
								{
									this.DialogueLabel.text = "Uh...that doesn't sound correct...";
								}
								else if (DatingGlobals.GetSuitorTrait(2) == DatingGlobals.GetTraitDemonstrated(2))
								{
									this.DialogueLabel.text = "Uh...you already told me about that...";
								}
							}
							else
							{
								this.DialogueLabel.text = "Um...well...that sort of thing doesn't really matter to me...";
							}

							if (this.Affection > 100.0f)
							{
								this.Affection = 100.0f;
							}
							else if (this.Affection < 0.0f)
							{
								this.Affection = 0.0f;
							}
						}

						if (Input.GetButtonDown(InputNames.Xbox_B))
						{
							this.ShowingOff = false;
						}
					}
					else if (this.GivingGift)
					{
						this.GiveGift.localScale = Vector3.Lerp(
							this.GiveGift.localScale,
							new Vector3(1.0f, 1.0f, 1.0f),
							Time.deltaTime * 10.0f);

						if (this.InputManager.TappedUp)
						{
							this.GiftRow--;
							this.UpdateGiftHighlight();
						}
						else if (this.InputManager.TappedDown)
						{
							this.GiftRow++;
							this.UpdateGiftHighlight();
						}
						if (this.InputManager.TappedLeft)
						{
							this.GiftColumn--;
							this.UpdateGiftHighlight();
						}
						else if (this.InputManager.TappedRight)
						{
							this.GiftColumn++;
							this.UpdateGiftHighlight();
						}

						if (Input.GetButtonDown(InputNames.Xbox_A))
						{
							if (this.GiftIcons[this.GiftSelected].enabled)
							{
								CollectibleGlobals.SetGiftPurchased(this.GiftSelected + 5, false);
								CollectibleGlobals.SetGiftGiven(this.GiftSelected, false);

								this.Rival.Cosmetic.CatGifts[this.GiftSelected].SetActive(true);

								UILabel label5 = this.Labels[5];
								label5.color = new Color(
									label5.color.r,
									label5.color.g,
									label5.color.b,
									0.50f);

								this.GivingGift = false;

								this.DialogueLabel.text = this.GiveGifts[this.GiftSelected];

								this.Rival.CharacterAnimation.CrossFade(AnimNames.FemaleLookDown);
								this.CurrentAnim = AnimNames.FemaleLookDown;
								this.Affection += this.Multiplier;

								this.CalculateAffection();
							}

							if (this.Affection > 100.0f)
							{
								this.Affection = 100.0f;
							}
							else if (this.Affection < 0.0f)
							{
								this.Affection = 0.0f;
							}
						}

						if (Input.GetButtonDown(InputNames.Xbox_B))
						{
							this.GivingGift = false;
						}
					}
				}
			}
			else if (this.Phase == 5)
			{
				this.Speed += Time.deltaTime * 100.0f;

				this.AffectionSet.localPosition = new Vector3(
					this.AffectionSet.localPosition.x,
					this.AffectionSet.localPosition.y + this.Speed,
					this.AffectionSet.localPosition.z);

				this.OptionSet.localPosition = new Vector3(
					this.OptionSet.localPosition.x + this.Speed,
					this.OptionSet.localPosition.y,
					this.OptionSet.localPosition.z);

				if (this.Speed > 100.0f)
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.Phase++;
					}
				}
			}
			else if (this.Phase == 6)
			{
				this.DatingSimHUD.alpha = Mathf.MoveTowards(this.DatingSimHUD.alpha, 0.0f, Time.deltaTime);

				if (this.DatingSimHUD.alpha == 0.0f)
				{
					// [af] Added "gameObject" for C# compatibility.
					this.DatingSimHUD.gameObject.SetActive(false);

					this.Phase++;
				}
			}
			//Exiting
			else if (this.Phase == 7)
			{
				if (this.Panel.alpha == 0.0f)
				{
					this.Suitor.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;

					this.LoveManager.RivalWaiting = false;
					this.LoveManager.Courted = true;

					this.Suitor.enabled = true;
					this.Rival.enabled = true;

					this.Suitor.CurrentDestination = this.Suitor.Destinations[this.Suitor.Phase];
					this.Suitor.Pathfinding.target = this.Suitor.Destinations[this.Suitor.Phase];
					this.Suitor.Prompt.Label[0].text = "     Talk";
					this.Suitor.Pathfinding.canSearch = true;
					this.Suitor.Pathfinding.canMove = true;
					this.Suitor.Pushable = false;
					this.Suitor.Meeting = false;
					this.Suitor.Routine = true;
					this.Suitor.MeetTimer = 0.0f;

					this.Rival.Cosmetic.MyRenderer.materials[2].SetFloat("_BlendAmount", 0.0f);
					this.Rival.CurrentDestination = this.Rival.Destinations[this.Rival.Phase];
					this.Rival.Pathfinding.target = this.Rival.Destinations[this.Rival.Phase];
					this.Rival.CharacterAnimation[AnimNames.FemaleSmile].weight = 0.0f;
					this.Rival.Prompt.Label[0].text = "     Talk";
					this.Rival.Pathfinding.canSearch = true;
					this.Rival.Pathfinding.canMove = true;
					this.Rival.Pushable = false;
					this.Rival.Meeting = false;
					this.Rival.Routine = true;
					this.Rival.MeetTimer = 0.0f;

					this.StudentManager.Clock.StopTime = false;
					this.Yandere.RPGCamera.enabled = true;
					this.Suitor.Earpiece.SetActive(false);
					this.HeartbeatCamera.SetActive(true);
					this.Yandere.Headset.SetActive(false);

					DatingGlobals.Affection = this.Affection;

					if (this.AffectionLevel == 5)
					{
						this.LoveManager.ConfessToSuitor = true;
					}

					this.PromptBar.ClearButtons();
					this.PromptBar.Show = false;

					if (this.StudentManager.Students[10] != null)
					{
						this.StudentManager.Students[10].CurrentDestination = this.StudentManager.Students[10].FollowTarget.transform;
						this.StudentManager.Students[10].Pathfinding.target = this.StudentManager.Students[10].FollowTarget.transform;
					}
				}
				else if (this.Panel.alpha == 1.0f)
				{
					this.Matchmaking = false;
					this.Yandere.CanMove = true;
					this.gameObject.SetActive(false);
				}

				this.Panel.alpha = Mathf.MoveTowards(this.Panel.alpha, 1.0f, Time.deltaTime);
			}

#if UNITY_EDITOR
			if (Input.GetKeyDown(KeyCode.Space))
			{
				this.Yandere.CharacterAnimation[AnimNames.FemaleTreePeeking].time = 0.0f;
				this.Yandere.CharacterAnimation.Play(AnimNames.FemaleTreePeeking);

				MainCamera.transform.position = new Vector3(48.0f, 3.0f, -44.0f);
				MainCamera.transform.eulerAngles = new Vector3(15.0f, 90.0f, 0.0f);

				this.Rival.transform.eulerAngles = new Vector3(
					this.Rival.transform.eulerAngles.x,
					90.0f,
					this.Rival.transform.eulerAngles.z);

				this.Rival.CharacterAnimation.Play(this.Rival.IdleAnim);
				this.Rival.CharacterAnimation[AnimNames.FemaleTurnAround].speed = 1.0f;

				DatingGlobals.SetComplimentGiven(1, false);
				DatingGlobals.SetComplimentGiven(4, false);
				DatingGlobals.SetComplimentGiven(5, false);
				DatingGlobals.SetComplimentGiven(8, false);
				DatingGlobals.SetComplimentGiven(9, false);

				DatingGlobals.SetTraitDemonstrated(2, 0);

				DatingGlobals.AffectionLevel = 0.0f;
				DatingGlobals.Affection = 0.0f;

				this.AffectionBar.localScale = new Vector3(
					0.0f,
					this.AffectionBar.localScale.y,
					this.AffectionBar.localScale.z);

				this.AffectionLevel = 0;
				this.Affection = 0.0f;

				// [af] Moved assignments into a loop.
				for (int i = 1; i < 6; i++)
				{
					UILabel label = this.Labels[i];
					label.color = new Color(
						label.color.r,
						label.color.g,
						label.color.b,
						1.0f);
				}

				this.Phase = 1;
				this.Timer = 0.0f;

				// [af] Converted while loop to for loop.
				for (int ID = 1; ID < 26; ID++)
				{
					DatingGlobals.SetTopicDiscussed(ID, false);
					UISprite topicIcon = this.TopicIcons[ID];
					topicIcon.color = new Color(
						topicIcon.color.r,
						topicIcon.color.g,
						topicIcon.color.b,
						1.0f);
				}

				this.UpdateTopics();
			}

			if (Input.GetKeyDown("="))
			{
				Time.timeScale++;
			}

			if (Input.GetKeyDown(KeyCode.LeftControl))
			{
				this.Affection += 10;
				this.CalculateAffection();
				this.DialogueLabel.text = this.Greetings[this.AffectionLevel];
			}
#endif
		}

#if UNITY_EDITOR
		/*
		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			this.Affection = 100.0f;
		}

		if (Input.GetKeyDown(KeyCode.Q))
		{
			// [af] Replaced if/else statement with boolean expression.
			this.AffectionGrow = !this.AffectionGrow;
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			for (int ID = 1; ID < Labels.Length; ID++)
			{
				UILabel label = this.Labels[ID];
				label.color = new Color(
					label.color.r,
					label.color.g,
					label.color.b,
					1.00f);
			}
		}

		if (Input.GetKeyDown(KeyCode.Z))
		{
			DatingGlobals.SetSuitorTrait(2, DatingGlobals.GetSuitorTrait(2) + 1);
		}
		*/
#endif
	}

	void LateUpdate()
	{
		if (this.Phase == 4)
		{
			// [af] Commented in JS code.
			//Rival.Head.LookAt(MainCamera);
		}
	}

	void CalculateMultiplier()
	{
		this.Multiplier = 5;

		//Ponytail
		if (!this.Suitor.Cosmetic.MaleHair[55].activeInHierarchy)
		{
			this.MultiplierIcons[1].mainTexture = this.X;
			this.Multiplier--;
		}

		//Piercings
		if (!this.Suitor.Cosmetic.MaleAccessories[17].activeInHierarchy)
		{
			this.MultiplierIcons[2].mainTexture = this.X;
			this.Multiplier--;
		}

		//Glasses
		if (!this.Suitor.Cosmetic.Eyewear[6].activeInHierarchy)
		{
			this.MultiplierIcons[3].mainTexture = this.X;
			this.Multiplier--;
		}

		//Tan skin
		if (this.Suitor.Cosmetic.SkinColor != 6)
		{
			this.MultiplierIcons[4].mainTexture = this.X;
			this.Multiplier--;
		}

		if (PlayerGlobals.PantiesEquipped == 2)
		{
			this.PantyIcon.SetActive(true);
			this.Multiplier++;
		}
		else
		{
			this.PantyIcon.SetActive(false);
		}

		if (this.Yandere.Class.Seduction + this.Yandere.Class.SeductionBonus > 0)
		{
			this.SeductionLabel.text = (this.Yandere.Class.Seduction + this.Yandere.Class.SeductionBonus).ToString();
			this.Multiplier += this.Yandere.Class.Seduction + this.Yandere.Class.SeductionBonus;
			this.SeductionIcon.SetActive(true);
		}
		else
		{
			this.SeductionIcon.SetActive(false);
		}

		this.MultiplierLabel.text = "Multiplier: " + this.Multiplier.ToString() + "x";
	}

	void UpdateHighlight()
	{
		if (this.Selected < 1)
		{
			this.Selected = 6;
		}
		else if (this.Selected > 6)
		{
			this.Selected = 1;
		}

		this.HighlightTarget = 450.0f - (100.0f * this.Selected);
	}

	void UpdateTopicHighlight()
	{
		if (this.Row < 1)
		{
			this.Row = 5;
		}
		else if (this.Row > 5)
		{
			this.Row = 1;
		}

		if (this.Column < 1)
		{
			this.Column = 5;
		}
		else if (this.Column > 5)
		{
			this.Column = 1;
		}

		this.TopicHighlight.localPosition = new Vector3(
			-375 + (125 * this.Column),
			375 - (125 * this.Row),
			this.TopicHighlight.localPosition.z);

		this.TopicSelected = ((this.Row - 1) * 5) + this.Column;

		// [af] Replaced if/else statement with ternary expression.
		this.TopicNameLabel.text = ConversationGlobals.GetTopicDiscovered(this.TopicSelected) ?
			this.TopicNames[this.TopicSelected] : "??????????";
	}

	void DetermineOpinion()
	{
		int[] rivalTopics = this.JSON.Topics[LoveManager.RivalID].Topics;
		this.Opinion = rivalTopics[this.TopicSelected];
	}

	void UpdateTopics()
	{
		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < this.TopicIcons.Length; ID++)
		{
			UISprite topicIcon = this.TopicIcons[ID];

			if (!ConversationGlobals.GetTopicDiscovered(ID))
			{
				topicIcon.spriteName = 0.ToString();
				topicIcon.color = new Color(
					topicIcon.color.r,
					topicIcon.color.g,
					topicIcon.color.b,
					0.50f);
			}
			else
			{
				topicIcon.spriteName = ID.ToString();
			}
		}

		// [af] Iterate topics 1 through 25.
		for (int i = 1; i <= 25; i++)
		{
			UISprite opinionIcon = this.OpinionIcons[i];

			if (!ConversationGlobals.GetTopicLearnedByStudent(i, LoveManager.RivalID))
			{
				opinionIcon.spriteName = "Unknown";
			}
			else
			{
				int[] studentTopics = this.JSON.Topics[LoveManager.RivalID].Topics;
				opinionIcon.spriteName = this.OpinionSpriteNames[studentTopics[i]];
			}
		}
	}

	void UpdateComplimentHighlight()
	{
		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < this.TopicIcons.Length; ID++)
		{
			this.ComplimentBGs[this.ComplimentSelected].color = this.OriginalColor;
		}

		if (this.Line < 1)
		{
			this.Line = 5;
		}
		else if (this.Line > 5)
		{
			this.Line = 1;
		}

		if (this.Side < 1)
		{
			this.Side = 2;
		}
		else if (this.Side > 2)
		{
			this.Side = 1;
		}

		this.ComplimentSelected = ((this.Line - 1) * 2) + this.Side;

		this.ComplimentBGs[this.ComplimentSelected].color = Color.white;
	}

	void UpdateTraitHighlight()
	{
		if (this.TraitSelected < 1)
		{
			this.TraitSelected = 3;
		}
		else if (this.TraitSelected > 3)
		{
			this.TraitSelected = 1;
		}

		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < this.TraitBGs.Length; ID++)
		{
			this.TraitBGs[ID].color = this.OriginalColor;
		}

		this.TraitBGs[this.TraitSelected].color = Color.white;
	}

	void UpdateGiftHighlight()
	{
		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < this.GiftBGs.Length; ID++)
		{
			this.GiftBGs[ID].color = this.OriginalColor;
		}

		if (this.GiftRow < 1)
		{
			this.GiftRow = 2;
		}
		else if (this.GiftRow > 2)
		{
			this.GiftRow = 1;
		}
		if (this.GiftColumn < 1)
		{
			this.GiftColumn = 2;
		}
		else if (this.GiftColumn > 2)
		{
			this.GiftColumn = 1;
		}

		this.GiftSelected = ((this.GiftRow - 1) * 2) + this.GiftColumn;

		this.GiftBGs[this.GiftSelected].color = Color.white;
	}
}
