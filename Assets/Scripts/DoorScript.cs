using UnityEngine;

public class DoorScript : MonoBehaviour
{
	[SerializeField] Transform RelativeCharacter;

    [SerializeField] YanSaveIdentifier Identifier;

    [SerializeField] HideColliderScript HideCollider;
	public StudentScript Student;
	[SerializeField] YandereScript Yandere;
	[SerializeField] BucketScript Bucket;
	public PromptScript Prompt;

	[SerializeField] Collider[] DoorColliders;
	[SerializeField] float[] ClosedPositions;
	[SerializeField] float[] OpenPositions;
	[SerializeField] Transform[] Doors;
	[SerializeField] Texture[] Plates;
	[SerializeField] UILabel[] Labels;
	[SerializeField] float[] OriginX;

	[SerializeField] bool CanSetBucket = false;
	[SerializeField] bool HidingSpot = false;
	[SerializeField] bool BucketSet = false;
	[SerializeField] bool Swinging = false;
	bool Double { get { return this.Doors.Length == 2; } }
	public bool Locked = false;
	[SerializeField] bool NoTrap = false;
	[SerializeField] bool North = false;
	public bool Open = false;
	[SerializeField] bool Near = false;

	[SerializeField] float ShiftNorth = -0.10f;
	[SerializeField] float ShiftSouth = 0.10f;
	[SerializeField] float Rotation = 0.0f;
	public float TimeLimit = 2.0f;
	public float Timer = 0.0f;

	[SerializeField] float TrapSwing = 12.15f;
	[SerializeField] float Swing = 150.0f;

	[SerializeField] Renderer Sign;

	[SerializeField] string RoomName = string.Empty;
	[SerializeField] string Facing = string.Empty;

	[SerializeField] int RoomID = 0;
	[SerializeField] ClubType Club = ClubType.None;

	[SerializeField] bool DisableSelf = false;

	StudentManagerScript StudentManager;

	public OcclusionPortal Portal;

	public int DoorID = 0;

	void Start()
	{
        this.Identifier = GetComponent<YanSaveIdentifier>();

        this.TrapSwing = 12.15f;
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();
		this.StudentManager = this.Yandere.StudentManager;
		this.StudentManager.Doors[this.StudentManager.DoorID] = this;
		this.StudentManager.DoorID++;
		this.DoorID = this.StudentManager.DoorID;

        if (this.Identifier != null)
        {
            this.Identifier.ObjectID = "Door_" + this.DoorID;
        }
        else
        {
            Debug.Log(this.gameObject.name + " doesn't have an identifier.");
        }

        if (this.StudentManager.EastBathroomArea.bounds.Contains(transform.position) ||
			this.StudentManager.WestBathroomArea.bounds.Contains(transform.position))
		{
			RoomName = "Toilet Stall";
		}

		if (this.Swinging)
		{
			this.OriginX[0] = this.Doors[0].transform.localPosition.z;

			if (this.OriginX.Length > 1)
			{
				this.OriginX[1] = this.Doors[1].transform.localPosition.z;
			}

			this.TimeLimit = 1;
		}

		if (this.Labels.Length > 0)
		{
			this.Labels[0].text = this.RoomName;
			this.Labels[1].text = this.RoomName;
			this.UpdatePlate();
		}

		if ((this.Club != ClubType.None) && ClubGlobals.GetClubClosed(this.Club))
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.enabled = false;
		}

		if (this.DisableSelf)
		{
			this.enabled = false;
		}

		this.Prompt.Student = false;
		this.Prompt.Door = true;

		DoorColliders = new Collider[2];
		DoorColliders[0] = Doors[0].gameObject.GetComponent<BoxCollider>();

		if (DoorColliders[0] == null)
		{
			DoorColliders[0] = Doors[0].GetChild(0).gameObject.GetComponent<BoxCollider>();
		}

		if (Doors.Length > 1)
		{
			DoorColliders[1] = Doors[1].GetComponent<BoxCollider>();
		}
    }

	void Update()
	{
		/*
		// [af] Check if the distance squared to Yandere-chan is close enough.
		Vector3 diff = this.transform.position - this.Yandere.transform.position;
		const float maxDistSqr = 1.0f;
		*/

		//If Yandere-chan is close enough to the door...
		//if (diff.sqrMagnitude <= maxDistSqr)

		if (this.Prompt.DistanceSqr <= 1)
		{
			if (Vector3.Distance(this.Yandere.transform.position, this.transform.position) < 2)
			{
				if (!this.Near)
				{
					this.TopicCheck();
					this.Yandere.Location.Label.text = this.RoomName;
					this.Yandere.Location.Show = true;
					this.Near = true;
				}

				if (this.Prompt.Circle[0].fillAmount == 0.0f)
				{
					this.Prompt.Circle[0].fillAmount = 1.0f;

					if (!this.Open)
					{
						this.OpenDoor();
					}
					else
					{
						this.CloseDoor();
					}
				}

				if (this.Double && this.Swinging)
				{
					if (this.Prompt.Circle[1].fillAmount == 0.0f)
					{
						this.Prompt.Circle[1].fillAmount = 1;

						if (!this.BucketSet)
						{
							//If we're waiting for Yandere-chan to put a bucket on top of a door...
							if (SchemeGlobals.GetSchemeStage(1) == 2)
							{
								SchemeGlobals.SetSchemeStage(1, 3);
								this.Yandere.PauseScreen.Schemes.UpdateInstructions();
							}

							this.Bucket = this.Yandere.PickUp.Bucket;
							this.Yandere.EmptyHands();

							this.Bucket.transform.parent = this.transform;
							this.Bucket.transform.localEulerAngles = new Vector3(0, 0, 0);
							this.Bucket.Trap = true;

							this.Bucket.Prompt.Hide();
							this.Bucket.Prompt.enabled = false;

							this.CheckDirection();

							if (this.North)
							{
								this.Bucket.transform.localPosition = new Vector3(0, 2.25f, .2975f);
							}
							else
							{
								this.Bucket.transform.localPosition = new Vector3(0, 2.25f, -.2975f);
							}

							this.Bucket.GetComponent<Rigidbody>().isKinematic = true;
							this.Bucket.GetComponent<Rigidbody>().useGravity = false;

							if (this.Open)
							{
								DoorColliders[0].isTrigger = true;
								DoorColliders[1].isTrigger = true;
							}

							this.Prompt.Label[1].text = "     " + "Remove Bucket";

							this.Prompt.HideButton[0] = true;
							this.CanSetBucket = false;
							this.BucketSet = true;
							this.Open = false;
							this.Timer = 0.0f;

							//this.Prompt.enabled = false;
							//this.Prompt.Hide();
						}
						//Bucket Set
						else
						{
							this.Yandere.EmptyHands();

							this.Bucket.PickUp.BePickedUp();

							this.Prompt.HideButton[0] = false;
							this.Prompt.Label[1].text = "     " + "Set Trap";
							this.BucketSet = false;
							this.Timer = 0;
						}
					}
				}
			}
		}
		else
		{
			if (this.Near)
			{
				this.Yandere.Location.Show = false;
				this.Near = false;
			}
		}

		if (this.Timer < TimeLimit)
		{
			this.Timer += Time.deltaTime;

			if (this.Timer >= TimeLimit)
			{
				DoorColliders[0].isTrigger = false;
				if (DoorColliders[1] != null){DoorColliders[1].isTrigger = false;}

				if (this.Portal != null)
				{
					this.Portal.open = Open;
				}
			}

			//If a bucket has been set above the door...
			if (this.BucketSet)
			{
				for (int ID = 0; ID < this.Doors.Length; ID++)
				{
					Transform door = this.Doors[ID];
					
					door.localPosition = new Vector3(
						door.localPosition.x,
						door.localPosition.y,
						Mathf.Lerp(door.localPosition.z, this.OriginX[ID] + (this.North ? this.ShiftSouth : this.ShiftNorth), Time.deltaTime * 3.60f));
					
					this.Rotation = Mathf.Lerp(
						this.Rotation,
						this.North ? -this.TrapSwing : this.TrapSwing,
						Time.deltaTime * 3.60f);
					
					door.localEulerAngles = new Vector3(
						door.localEulerAngles.x,
						(ID == 0) ? this.Rotation : -this.Rotation,
						door.localEulerAngles.z);
				}
			}
			else
			{
				//If the door is closed...
				if (!this.Open)
				{
					for (int ID = 0; ID < this.Doors.Length; ID++)
					{
						Transform door = this.Doors[ID];

						if (!this.Swinging)
						{
							door.localPosition = new Vector3(
								Mathf.Lerp(door.localPosition.x, this.ClosedPositions[ID], Time.deltaTime * 3.60f),
								door.localPosition.y,
								door.localPosition.z);
						}
						else
						{
							this.Rotation = Mathf.Lerp(this.Rotation, 0.0f, Time.deltaTime * 3.60f);

							door.localPosition = new Vector3(
								door.localPosition.x,
								door.localPosition.y,
								Mathf.Lerp(door.localPosition.z, this.OriginX[ID], Time.deltaTime * 3.60f));
							
							door.localEulerAngles = new Vector3(
								door.localEulerAngles.x,
								(ID == 0) ? this.Rotation : -this.Rotation,
								door.localEulerAngles.z);
						}
					}
				}
				//If the door is open...
				else
				{
					for (int ID = 0; ID < this.Doors.Length; ID++)
					{
						Transform door = this.Doors[ID];

						if (!this.Swinging)
						{
							door.localPosition = new Vector3(
								Mathf.Lerp(door.localPosition.x, this.OpenPositions[ID], Time.deltaTime * 3.60f),
								door.localPosition.y,
								door.localPosition.z);
						}
						else
						{
							door.localPosition = new Vector3(
								door.localPosition.x,
								door.localPosition.y,
								Mathf.Lerp(door.localPosition.z, this.OriginX[ID] + (this.North ? this.ShiftNorth : this.ShiftSouth), Time.deltaTime * 3.60f));
							
							this.Rotation = Mathf.Lerp(
								this.Rotation,
								this.North ? this.Swing : -this.Swing,
								Time.deltaTime * 3.60f);
							
							door.localEulerAngles = new Vector3(
								door.localEulerAngles.x,
								(ID == 0) ? this.Rotation : -this.Rotation,
								door.localEulerAngles.z);
						}
					}
				}
			}
		}
		else
		{
			if (this.Locked)
			{
				if (this.Prompt.Circle[0].fillAmount < 1.0f)
				{
					this.Prompt.Label[0].text = "     " + "Locked";
					this.Prompt.Circle[0].fillAmount = 1.0f;
				}

				if (this.Yandere.Inventory.LockPick)
				{
					this.Prompt.HideButton[2] = false;

					if (this.Prompt.Circle[2].fillAmount == 0.0f)
					{
						this.Prompt.Yandere.Inventory.LockPick = false;
						this.Prompt.HideButton[2] = true;
						this.Locked = false;
					}
				}
				else
				{
					if (!this.Prompt.HideButton[2])
					{
						this.Prompt.HideButton[2] = true;
					}
				}
			}
		}
			
		if (!this.NoTrap && this.Swinging && this.Double)
		{
			if (this.Yandere.PickUp != null)
			{
				if (this.Yandere.PickUp.Bucket != null)
				{
					if (this.Yandere.PickUp.GetComponent<BucketScript>().Full)
					{
                        if (this.Bucket == null)
                        {
                            //Debug.Log("Because the code got here.");

                            this.Prompt.HideButton[1] = false;
						    this.CanSetBucket = true;
                        }
                    }
					else if (this.CanSetBucket)
					{
						this.Prompt.HideButton[1] = true;
						this.CanSetBucket = false;
					}
				}
				else if (this.CanSetBucket)
				{
					this.Prompt.HideButton[1] = true;
					this.CanSetBucket = false;
				}
			}
			else if (this.CanSetBucket)
			{
				this.Prompt.HideButton[1] = true;
				this.CanSetBucket = false;
			}
		}

		if (this.BucketSet)
		{
			//Debug.Log ("Bucket set.");

			if (this.Bucket.Gasoline)
			{
				//Debug.Log ("It's full of gasoline.");

				if (this.StudentManager.Students[this.StudentManager.RivalID] != null)
				{
					//Debug.Log ("The rival exists.");

					if (this.StudentManager.Students[this.StudentManager.RivalID].Follower != null)
					{
						//Debug.Log ("The rival has a follower.");

						if (Vector3.Distance(this.transform.position, this.StudentManager.Students[this.StudentManager.RivalID].transform.position) < 5)
						{
							//Debug.Log ("The follower has warned the rival.");

							this.Yandere.Subtitle.UpdateLabel(SubtitleType.GasWarning, 1, 5.0f);
							this.StudentManager.Students[this.StudentManager.RivalID].GasWarned = true;
						}
					}
				}
			}
		}

		//if (Input.GetKeyDown(KeyCode.Space))
		//{
		//	this.UpdatePlate();
		//}
	}

	public void OpenDoor()
	{
		if (this.Portal != null)
		{
			this.Portal.open = true;
		}

		this.Open = true;
		this.Timer = 0.0f;
		this.UpdateLabel();

		if (this.HidingSpot)
		{
			Destroy(this.HideCollider.GetComponent<BoxCollider>());
		}

		this.CheckDirection();

		if (this.BucketSet)
		{
			this.Bucket.GetComponent<Rigidbody>().isKinematic = false;
			this.Bucket.GetComponent<Rigidbody>().useGravity = true;
			this.Bucket.UpdateAppearance = true;
			this.Bucket.Prompt.enabled = true;
			this.Bucket.Full = false;
			this.Bucket.Fly = true;

			this.Prompt.HideButton[0] = false;
			this.Prompt.HideButton[1] = true;
            this.Prompt.Label[1].text = "     " + "Set Trap";

            this.Prompt.enabled = true;
			this.BucketSet = false;
		}
	}

	void LockDoor()
	{
		this.Open = false;
		this.Prompt.Hide();
		this.Prompt.enabled = false;
	}

	void CheckDirection()
	{
		this.North = false;

		// [af] Replaced if/else statement with ternary expression.
		this.RelativeCharacter = (this.Student != null) ?
			this.Student.transform : this.Yandere.transform;

		if (this.Facing == "North")
		{
			if (this.RelativeCharacter.position.z < this.transform.position.z)
			{
				this.North = true;
			}
		}
		else if (this.Facing == "South")
		{
			if (this.RelativeCharacter.position.z > this.transform.position.z)
			{
				this.North = true;
			}
		}
		else if (this.Facing == "East")
		{
			if (this.RelativeCharacter.position.x < this.transform.position.x)
			{
				this.North = true;
			}
		}
		else if (this.Facing == "West")
		{
			if (this.RelativeCharacter.position.x > this.transform.position.x)
			{
				this.North = true;
			}
		}

		this.Student = null;
	}

	public void CloseDoor()
	{
		this.Open = false;
		this.Timer = 0.0f;
		this.UpdateLabel();

		DoorColliders[0].isTrigger = true;
		if (DoorColliders[1] != null){DoorColliders[1].isTrigger = true;}

		if (this.HidingSpot)
		{
			this.HideCollider.gameObject.AddComponent<BoxCollider>();

			BoxCollider boxCollider = this.HideCollider.GetComponent<BoxCollider>();
			boxCollider.size = new Vector3(
				boxCollider.size.x,
				boxCollider.size.y,
				2.0f);

			boxCollider.isTrigger = true;
			this.HideCollider.MyCollider = boxCollider;
		}
	}

	void UpdateLabel()
	{
		if (this.Open)
		{
			this.Prompt.Label[0].text = "     " + "Close";
		}
		else
		{
			this.Prompt.Label[0].text = "     " + "Open";
		}
	}

	void UpdatePlate()
	{
		switch (this.RoomID)
		{
			// Plate Set 1.

			// Class 1-1.
			case 1:
				this.Sign.material.mainTexture = this.Plates[1];
				this.Sign.material.mainTextureOffset = new Vector2(0.0f, 0.75f);
				break;
			// Class 1-1.
			case 2:
				this.Sign.material.mainTexture = this.Plates[1];
				this.Sign.material.mainTextureOffset = new Vector2(0.0f, 0.50f);
				break;
			// Class 2-1.
			case 3:
				this.Sign.material.mainTexture = this.Plates[1];
				this.Sign.material.mainTextureOffset = new Vector2(0.0f, 0.25f);
				break;
			// Class 2-2.
			case 4:
				this.Sign.material.mainTexture = this.Plates[1];
				this.Sign.material.mainTextureOffset = new Vector2(0.0f, 0.0f);
				break;
			// Class 3-1.
			case 5:
				this.Sign.material.mainTexture = this.Plates[1];
				this.Sign.material.mainTextureOffset = new Vector2(0.25f, 0.75f);
				break;
			// Class 3-2.
			case 6:
				this.Sign.material.mainTexture = this.Plates[1];
				this.Sign.material.mainTextureOffset = new Vector2(0.25f, 0.50f);
				break;
			// Headmaster's Office.
			case 7:
				this.Sign.material.mainTexture = this.Plates[1];
				this.Sign.material.mainTextureOffset = new Vector2(0.25f, 0.25f);
				break;
			// Infirmary.
			case 8:
				this.Sign.material.mainTexture = this.Plates[1];
				this.Sign.material.mainTextureOffset = new Vector2(0.25f, 0.0f);
				break;
			// Faculty Room.
			case 9:
				this.Sign.material.mainTexture = this.Plates[1];
				this.Sign.material.mainTextureOffset = new Vector2(0.50f, 0.75f);
				break;
			// Counselor's Office.
			case 10:
				this.Sign.material.mainTexture = this.Plates[1];
				this.Sign.material.mainTextureOffset = new Vector2(0.50f, 0.50f);
				break;
			// Calligraphy Room.
			case 11:
				this.Sign.material.mainTexture = this.Plates[1];
				this.Sign.material.mainTextureOffset = new Vector2(0.50f, 0.25f);
				break;
			// Student Council Room.
			case 12:
				this.Sign.material.mainTexture = this.Plates[1];
				this.Sign.material.mainTextureOffset = new Vector2(0.50f, 0.0f);
				break;
			// Library.
			case 13:
				this.Sign.material.mainTexture = this.Plates[1];
				this.Sign.material.mainTextureOffset = new Vector2(0.75f, 0.75f);
				break;
			// Announcement Room.
			case 14:
				this.Sign.material.mainTexture = this.Plates[1];
				this.Sign.material.mainTextureOffset = new Vector2(0.75f, 0.50f);
				break;
			// Computer Lab.
			case 15:
				this.Sign.material.mainTexture = this.Plates[1];
				this.Sign.material.mainTextureOffset = new Vector2(0.75f, 0.25f);
				break;
			// Audiovisual Room.
			case 16:
				this.Sign.material.mainTexture = this.Plates[1];
				this.Sign.material.mainTextureOffset = new Vector2(0.75f, 0.0f);
				break;

			// Plate Set 2.

			// Home Economics Room.
			case 17:
				this.Sign.material.mainTexture = this.Plates[2];
				this.Sign.material.mainTextureOffset = new Vector2(0.0f, 0.75f);
				break;
			// Sewing Room.
			case 18:
				this.Sign.material.mainTexture = this.Plates[2];
				this.Sign.material.mainTextureOffset = new Vector2(0.0f, 0.50f);
				break;
			// Meeting Room.
			case 19:
				this.Sign.material.mainTexture = this.Plates[2];
				this.Sign.material.mainTextureOffset = new Vector2(0.0f, 0.25f);
				break;
			// Science Lab.
			case 20:
				this.Sign.material.mainTexture = this.Plates[2];
				this.Sign.material.mainTextureOffset = new Vector2(0.0f, 0.0f);
				break;
			// Workshop.
			case 21:
				this.Sign.material.mainTexture = this.Plates[2];
				this.Sign.material.mainTextureOffset = new Vector2(0.25f, 0.75f);
				break;
			// Sociology Classroom.
			case 22:
				this.Sign.material.mainTexture = this.Plates[2];
				this.Sign.material.mainTextureOffset = new Vector2(0.25f, 0.50f);
				break;
			// English Classroom.
			case 23:
				this.Sign.material.mainTexture = this.Plates[2];
				this.Sign.material.mainTextureOffset = new Vector2(0.25f, 0.25f);
				break;
			// Biology Lab.
			case 24:
				this.Sign.material.mainTexture = this.Plates[2];
				this.Sign.material.mainTextureOffset = new Vector2(0.25f, 0.0f);
				break;
			// Art Room.
			case 25:
				this.Sign.material.mainTexture = this.Plates[2];
				this.Sign.material.mainTextureOffset = new Vector2(0.50f, 0.75f);
				break;
			// Cooking Club.
			case 26:
				this.Sign.material.mainTexture = this.Plates[2];
				this.Sign.material.mainTextureOffset = new Vector2(0.50f, 0.50f);
				break;
			// Drama Club.
			case 27:
				this.Sign.material.mainTexture = this.Plates[2];
				this.Sign.material.mainTextureOffset = new Vector2(0.50f, 0.25f);
				break;
			// Occult Club.
			case 28:
				this.Sign.material.mainTexture = this.Plates[2];
				this.Sign.material.mainTextureOffset = new Vector2(0.50f, 0.0f);
				break;
			// Art Room.
			case 29:
				this.Sign.material.mainTexture = this.Plates[2];
				this.Sign.material.mainTextureOffset = new Vector2(0.75f, 0.75f);
				break;
			// Light Music Club.
			case 30:
				this.Sign.material.mainTexture = this.Plates[2];
				this.Sign.material.mainTextureOffset = new Vector2(0.75f, 0.50f);
				break;
			// Martial Arts Club.
			case 31:
				this.Sign.material.mainTexture = this.Plates[2];
				this.Sign.material.mainTextureOffset = new Vector2(0.75f, 0.25f);
				break;
			// Photography Club.
			case 32:
				this.Sign.material.mainTexture = this.Plates[2];
				this.Sign.material.mainTextureOffset = new Vector2(0.75f, 0.0f);
				break;

			// Plate Set 4.

			// Info Club.
			case 33:
				this.Sign.material.mainTexture = this.Plates[3];
				this.Sign.material.mainTextureOffset = new Vector2(0.0f, 0.75f);
				break;
			// Science Club.
			case 34:
				this.Sign.material.mainTexture = this.Plates[3];
				this.Sign.material.mainTextureOffset = new Vector2(0.0f, 0.50f);
				break;

			default:
				// Do nothing.
				break;
		}
	}

	void TopicCheck()
	{
		if (this.RoomID > 25 && this.RoomID < 37)
		{
			this.StudentManager.TutorialWindow.ShowClubMessage = true;
		}

		switch (this.RoomID)
		{
			// Class 1-1.
			case 1:
				//
				break;
			// Class 1-1.
			case 2:
				//
				break;
			// Class 2-1.
			case 3:
				if (!ConversationGlobals.GetTopicDiscovered(22))
				{
					ConversationGlobals.SetTopicDiscovered(22, true);
					this.Yandere.NotificationManager.TopicName = "School";
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
				}
				break;
			// Class 2-2.
			case 4:
				//
				break;
			// Class 3-1.
			case 5:
				//
				break;
			// Class 3-2.
			case 6:
				//
				break;
			// Headmaster's Office.
			case 7:
				//
				break;
			// Infirmary.
			case 8:
				//
				break;
			// Faculty Room.
			case 9:
				//
				break;
			// Counselor's Office.
			case 10:
				//
				break;
			// Calligraphy Room.
			case 11:
				//
				break;
			// Student Council Room.
			case 12:
				//
				break;
			// Library.
			case 13:
				if (!ConversationGlobals.GetTopicDiscovered(18))
				{
					ConversationGlobals.SetTopicDiscovered(18, true);
					this.Yandere.NotificationManager.TopicName = "Reading";
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
				}
				break;
			// Announcement Room.
			case 14:
				//
				break;
			// Computer Lab.
			case 15:
				//
				break;
			// Audiovisual Room.
			case 16:
				//
				break;
			// Home Economics Room.
			case 17:
				//
				break;
			// Sewing Room.
			case 18:
				//
				break;
			// Meeting Room.
			case 19:
				//
				break;
			// Science Lab.
			case 20:
				//
				break;
			// Workshop.
			case 21:
				//
				break;
			// Sociology Classroom.
			case 22:
			if (!ConversationGlobals.GetTopicDiscovered(11))
			{
				ConversationGlobals.SetTopicDiscovered(11, true);
				ConversationGlobals.SetTopicDiscovered(12, true);
				ConversationGlobals.SetTopicDiscovered(13, true);
				ConversationGlobals.SetTopicDiscovered(14, true);

				this.Yandere.NotificationManager.TopicName = "Video Games";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);

				this.Yandere.NotificationManager.TopicName = "Anime";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);

				this.Yandere.NotificationManager.TopicName = "Cosplay";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);

				this.Yandere.NotificationManager.TopicName = "Memes";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
			}
				break;
			// English Classroom.
			case 23:
				//
				break;
			// Biology Lab.
			case 24:
				//
				break;
			// Art Room.
			case 25:
				//
				break;
			// Cooking Club.
			case 26:
				if (!ConversationGlobals.GetTopicDiscovered(1))
				{
					ConversationGlobals.SetTopicDiscovered(1, true);
					this.Yandere.NotificationManager.TopicName = "Cooking";
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
				}
				break;
			// Drama Club.
			case 27:
				if (!ConversationGlobals.GetTopicDiscovered(2))
				{
					ConversationGlobals.SetTopicDiscovered(2, true);
					this.Yandere.NotificationManager.TopicName = "Drama";
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
				}
				break;
			// Occult Club.
			case 28:
				if (!ConversationGlobals.GetTopicDiscovered(3))
				{
					ConversationGlobals.SetTopicDiscovered(3, true);
					this.Yandere.NotificationManager.TopicName = "Occult";
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
				}
				break;
			// Art Room.
			case 29:
				if (!ConversationGlobals.GetTopicDiscovered(4))
				{
					ConversationGlobals.SetTopicDiscovered(4, true);
					this.Yandere.NotificationManager.TopicName = "Art";
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
				}
				break;
			// Light Music Club.
			case 30:
				if (!ConversationGlobals.GetTopicDiscovered(5))
				{
					ConversationGlobals.SetTopicDiscovered(5, true);
					this.Yandere.NotificationManager.TopicName = "Music";
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
				}
				break;
			// Martial Arts Club.
			case 31:
				if (!ConversationGlobals.GetTopicDiscovered(6))
				{
					ConversationGlobals.SetTopicDiscovered(6, true);
					this.Yandere.NotificationManager.TopicName = "Martial Arts";
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
				}
				break;
			// Photography Club.
			case 32:
				if (!ConversationGlobals.GetTopicDiscovered(7))
				{
					ConversationGlobals.SetTopicDiscovered(7, true);
					this.Yandere.NotificationManager.TopicName = "Photography";
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
				}
				break;
			// Info Club.
			case 33:
				//
				break;
			// Science Club.
			case 34:
				if (!ConversationGlobals.GetTopicDiscovered(8))
				{
					ConversationGlobals.SetTopicDiscovered(8, true);
					this.Yandere.NotificationManager.TopicName = "Science";
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
				}
				break;
			// Gym
			case 35:
				if (!ConversationGlobals.GetTopicDiscovered(9))
				{
					ConversationGlobals.SetTopicDiscovered(9, true);
					this.Yandere.NotificationManager.TopicName = "Sports";
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
				}
				break;
			// Greenhouse
			case 36:
				if (!ConversationGlobals.GetTopicDiscovered(10))
				{
					ConversationGlobals.SetTopicDiscovered(10, true);
					this.Yandere.NotificationManager.TopicName = "Gardening";
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
				}

				if (!ConversationGlobals.GetTopicDiscovered(24))
				{
					ConversationGlobals.SetTopicDiscovered(24, true);
					this.Yandere.NotificationManager.TopicName = "Nature";
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
				}
				break;
			// Tea House.
			case 37:
				//
				break;

			default:
				// Do nothing.
				break;
		}
	}
}
