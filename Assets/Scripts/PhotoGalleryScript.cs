using System.Collections;
using UnityEngine;

public class PhotoGalleryScript : MonoBehaviour
{
	public HomeCorkboardPhotoScript[] CorkboardPhotographs;
	public StringScript[] CorkboardStrings;
	public int PhotoID;

	public InputManagerScript InputManager;
	public PauseScreenScript PauseScreen;
	public TaskManagerScript TaskManager;
	public InputDeviceScript InputDevice;
    public HomeCursorScript HomeCursor;
    public PromptBarScript PromptBar;
	public YandereScript Yandere;
    public StringScript String;

    public GameObject MovingPhotograph;
	public GameObject LoadingScreen;
	public GameObject Photograph;
	public GameObject StringSet;

	public Transform CorkboardPanel;
	public Transform Destination;
	public Transform Highlight;
	public Transform Gallery;
	public Transform StringParent;

	public UITexture[] Photographs;
	public UISprite[] Hearts;
	public AudioClip[] Sighs;

	public UITexture ViewPhoto;
	public Texture NoPhoto;

	public Vector2 PreviousPosition;
	public Vector2 MouseDelta;

	public bool DoNotRaisePromptBar = false;
	public bool SpawnedPhotos = false;
	public bool MovingString = false;
	public bool NamingBully = false;
	public bool Adjusting = false;
	public bool CanAdjust = false;
	public bool Corkboard = false;
	public bool GotPhotos = false;
	public bool Viewing = false;
	public bool Moving = false;
	public bool Reset = false;

	public int StringPhase = 0;
	public int Strings = 0;
	public int Photos = 0;

	public int Column = 0;
	public int Row = 0;

	// [af] Corkboard min/max photo coordinates in world space.
	// @todo: Double these so they represent side lengths, not distance from center.
	public float MaxPhotoX = 4150.0f;
	public float MaxPhotoY = 2500.0f;

	// [af] Corkboard min/max cursor coordinates in world space.
	// @todo: Double these so they represent side lengths, not distance from center.
	const float MaxCursorX = 4788.0f;
	const float MaxCursorY = 3122.0f;

	// [af] Corkboard aspect ratio (for aspect-independent cursor motion).
	// @todo: Use this when moving the cursor or selected picture so that the delta X and Y
	// percents are equal velocities in world space (use the reciprocal of the aspect ratio; 
	// i.e., the delta X percent is halved if the corkboard is twice as wide).
	const float CorkboardAspectRatio = MaxCursorX / MaxCursorY;

	public float SpeedLimit;

	void Start()
	{
		if (this.HomeCursor != null)
		{
			// [af] Added "gameObject" for C# compatibility.
			this.HomeCursor.gameObject.SetActive(false);

			this.enabled = false;
		}

		if (this.Corkboard)
		{
			this.StartCoroutine(this.GetPhotos());
		}
	}

	// [af] The index of the highlighted picture.
	int CurrentIndex
	{
		get { return this.Column + ((this.Row - 1) * 5); }
	}

	// [af] The speed of interpolation for various things in the interface.
	float LerpSpeed
	{
		get { return Time.unscaledDeltaTime * 10.0f; }
	}

	// [af] The X position of the highlight in world space.
	float HighlightX
	{
		get { return -450.0f + (150.0f * this.Column); }
	}

	// [af] The Y position of the highlight in world space.
	float HighlightY
	{
		get { return 225.0f - (75.0f * this.Row); }
	}

	// [af] Horizontal percent of the selected corkboard photograph (0 = left).
	float MovingPhotoXPercent
	{
		get
		{
			float minX = -MaxPhotoX;
			float maxX = MaxPhotoX;
			return (this.MovingPhotograph.transform.localPosition.x - minX) / (maxX - minX);
		}
		set
		{
			this.MovingPhotograph.transform.localPosition = new Vector3(
				-MaxPhotoX + (2.0f * (MaxPhotoX * Mathf.Clamp01(value))),
				this.MovingPhotograph.transform.localPosition.y,
				this.MovingPhotograph.transform.localPosition.z);
		}
	}

	// [af] Vertical percent of the selected corkboard photograph (0 = bottom).
	float MovingPhotoYPercent
	{
		get
		{
			float minY = -MaxPhotoY;
			float maxY = MaxPhotoY;
			return (this.MovingPhotograph.transform.localPosition.y - minY) / (maxY - minY);
		}
		set
		{
			this.MovingPhotograph.transform.localPosition = new Vector3(
				this.MovingPhotograph.transform.localPosition.x,
				-MaxPhotoY + (2.0f * (MaxPhotoY * Mathf.Clamp01(value))),
				this.MovingPhotograph.transform.localPosition.z);
		}
	}

	// [af] Rotation in degrees of the selected corkboard photograph.
	float MovingPhotoRotation
	{
		get { return this.MovingPhotograph.transform.localEulerAngles.z; }
		set
		{
			this.MovingPhotograph.transform.localEulerAngles = new Vector3(
				this.MovingPhotograph.transform.localEulerAngles.x,
				this.MovingPhotograph.transform.localEulerAngles.y,
				value);
		}
	}

	// [af] Horizontal percent of the corkboard cursor (0 = left).
	float CursorXPercent
	{
		get
		{
			float minX = -MaxCursorX;
			float maxX = MaxCursorX;
			return (this.HomeCursor.transform.localPosition.x - minX) / (maxX - minX);
		}
		set
		{
			this.HomeCursor.transform.localPosition = new Vector3(
				-MaxCursorX + (2.0f * (MaxCursorX * Mathf.Clamp01(value))),
				this.HomeCursor.transform.localPosition.y,
				this.HomeCursor.transform.localPosition.z);
		}
	}

	// [af] Vertical percent of the corkboard cursor (0 = bottom).
	float CursorYPercent
	{
		get
		{
			float minY = -MaxCursorY;
			float maxY = MaxCursorY;
			return (this.HomeCursor.transform.localPosition.y - minY) / (maxY - minY);
		}
		set
		{
			this.HomeCursor.transform.localPosition = new Vector3(
				this.HomeCursor.transform.localPosition.x,
				-MaxCursorY + (2.0f * (MaxCursorY * Mathf.Clamp01(value))),
				this.HomeCursor.transform.localPosition.z);
		}
	}

	void UpdatePhotoSelection()
	{
		if (Input.GetButtonDown(InputNames.Xbox_A))
		{
			if (!this.NamingBully)
			{
				UITexture photograph = this.Photographs[this.CurrentIndex];

				if (photograph.mainTexture != this.NoPhoto)
				{
					this.ViewPhoto.mainTexture = photograph.mainTexture;
					this.ViewPhoto.transform.position = photograph.transform.position;
					this.ViewPhoto.transform.localScale = photograph.transform.localScale;
					this.Destination.position = photograph.transform.position;
					this.Viewing = true;

					if (!this.Corkboard)
					{
						for (int ID = 1; ID < 26; ID++)
						{
							this.Hearts[ID].gameObject.SetActive(false);
						}
					}

					this.CanAdjust = false;
				}

				this.UpdateButtonPrompts();
			}
			else
			{
				UITexture photograph = this.Photographs[this.CurrentIndex];

				if (photograph.mainTexture != this.NoPhoto && PlayerGlobals.GetBullyPhoto(this.CurrentIndex) > 0)
				{
					this.Yandere.Police.EndOfDay.FragileTarget = PlayerGlobals.GetBullyPhoto(this.CurrentIndex);

					this.Yandere.StudentManager.FragileOfferHelp.Continue();
					this.PauseScreen.MainMenu.SetActive(true);
					this.Yandere.RPGCamera.enabled = true;
					this.gameObject.SetActive(false);
					this.PauseScreen.Show = false;
					this.PromptBar.Show = false;
					this.NamingBully = false;

					Time.timeScale = 1;
				}
			}
		}

		if (!this.NamingBully)
		{
			if (Input.GetButtonDown(InputNames.Xbox_B))
			{
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[0].text = "Accept";
				this.PromptBar.Label[1].text = "Exit";
				this.PromptBar.Label[4].text = "Choose";
				this.PromptBar.Label[5].text = "Choose";
				this.PromptBar.UpdateButtons();

				this.PauseScreen.MainMenu.SetActive(true);
				this.PauseScreen.Sideways = false;
				this.PauseScreen.PressedB = true;

				this.gameObject.SetActive(false);

				this.UpdateButtonPrompts();
			}
		}

		if (Input.GetButtonDown(InputNames.Xbox_X))
		{
			this.ViewPhoto.mainTexture = null;

			int ID = this.CurrentIndex;

			if (this.Photographs[ID].mainTexture != this.NoPhoto)
			{
				this.Photographs[ID].mainTexture = this.NoPhoto;
				PlayerGlobals.SetPhoto(ID, false);
				PlayerGlobals.SetSenpaiPhoto(ID, false);
				TaskGlobals.SetGuitarPhoto(ID, false);
				TaskGlobals.SetKittenPhoto(ID, false);
				this.Hearts[ID].gameObject.SetActive(false);

				this.TaskManager.UpdateTaskStatus();
			}

			this.UpdateButtonPrompts();
		}

		if (this.Corkboard)
		{
			if (Input.GetButtonDown(InputNames.Xbox_Y))
			{
				this.CanAdjust = false;
				this.HomeCursor.gameObject.SetActive(true);

				this.Adjusting = true;

				this.UpdateButtonPrompts();
			}
		}
		else
		{
			if (Input.GetButtonDown(InputNames.Xbox_Y))
			{
				if (PlayerGlobals.GetSenpaiPhoto(this.CurrentIndex))
				{
					int ID = this.CurrentIndex;
					PlayerGlobals.SetSenpaiPhoto(ID, false);
					this.Hearts[ID].gameObject.SetActive(false);

					this.CanAdjust = false;

					this.Yandere.Sanity += 20.0f;

					this.UpdateButtonPrompts();

					AudioSource.PlayClipAtPoint(this.Sighs[Random.Range(0, this.Sighs.Length)], Yandere.Head.position);
				}
			}
		}

		// @todo: Make indices zero-based.
		const int minColumn = 1;
		const int maxColumn = 5;
		const int minRow = 1;
		const int maxRow = 5;

		if (this.InputManager.TappedRight)
		{
			this.Column = (this.Column < maxColumn) ? (this.Column + 1) : minColumn;
		}

		if (this.InputManager.TappedLeft)
		{
			this.Column = (this.Column > minColumn) ? (this.Column - 1) : maxColumn;
		}

		if (this.InputManager.TappedUp)
		{
			this.Row = (this.Row > minRow) ? (this.Row - 1) : maxRow;
		}

		if (this.InputManager.TappedDown)
		{
			this.Row = (this.Row < maxRow) ? (this.Row + 1) : minRow;
		}

		bool columnChanged = this.InputManager.TappedRight || this.InputManager.TappedLeft;
		bool rowChanged = this.InputManager.TappedUp || this.InputManager.TappedDown;

		if (columnChanged || rowChanged)
		{
			this.Highlight.transform.localPosition = new Vector3(
				this.HighlightX,
				this.HighlightY,
				this.Highlight.transform.localPosition.z);

			this.UpdateButtonPrompts();
		}

		this.ViewPhoto.transform.localScale = Vector3.Lerp(
			this.ViewPhoto.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f), this.LerpSpeed);
		this.ViewPhoto.transform.position = Vector3.Lerp(
			this.ViewPhoto.transform.position, this.Destination.position, this.LerpSpeed);

		if (this.Corkboard)
		{
			this.Gallery.transform.localPosition = new Vector3(
				this.Gallery.transform.localPosition.x,
				Mathf.Lerp(this.Gallery.transform.localPosition.y, 0.0f, Time.deltaTime * 10.0f),
				this.Gallery.transform.localPosition.z);
		}
	}

	void UpdatePhotoViewing()
	{
		this.ViewPhoto.transform.localScale = Vector3.Lerp(
			this.ViewPhoto.transform.localScale,
			this.Corkboard ? new Vector3(5.80f, 5.80f, 5.80f) : new Vector3(6.50f, 6.50f, 6.50f),
			this.LerpSpeed);

		this.ViewPhoto.transform.localPosition = Vector3.Lerp(
			this.ViewPhoto.transform.localPosition, Vector3.zero, this.LerpSpeed);

		bool makeNewPhoto = this.Corkboard && Input.GetButtonDown(InputNames.Xbox_A);

		if (makeNewPhoto)
		{
			GameObject newPhotograph = Instantiate(this.Photograph,
				this.transform.position, Quaternion.identity);
			newPhotograph.transform.parent = this.CorkboardPanel;
			newPhotograph.transform.localEulerAngles = Vector3.zero;
			newPhotograph.transform.localPosition = Vector3.zero;
			newPhotograph.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

			newPhotograph.GetComponent<UITexture>().mainTexture =
				this.Photographs[this.CurrentIndex].mainTexture;

			this.MovingPhotograph = newPhotograph;

			this.CanAdjust = false;
			this.Adjusting = true;
			this.Viewing = false;
			this.Moving = true;

			this.CorkboardPhotographs[this.Photos] = newPhotograph.GetComponent<HomeCorkboardPhotoScript>();
			this.CorkboardPhotographs[this.Photos].ID = this.CurrentIndex;
			this.CorkboardPhotographs[this.Photos].ArrayID = this.Photos;
			this.Photos++;

			this.UpdateButtonPrompts();
		}

		if (Input.GetButtonDown(InputNames.Xbox_B))
		{
			this.Viewing = false;

			if (this.Corkboard)
			{
				this.HomeCursor.Highlight.transform.position = new Vector3(
					this.HomeCursor.Highlight.transform.position.x,
					100.0f,
					this.HomeCursor.Highlight.transform.position.z);

				this.CanAdjust = true;
			}
			else
			{
				for (int ID = 1; ID < 26; ID++)
				{
					if (PlayerGlobals.GetSenpaiPhoto(ID))
					{
						this.Hearts[ID].gameObject.SetActive(true);
						this.CanAdjust = true;
					}
				}
			}

			this.UpdateButtonPrompts();
		}
	}

	void UpdateCorkboardPhoto()
	{
        Cursor.lockState = CursorLockMode.None;

        // @todo: Replace literal photo movement code with X and Y percent accessors
        // using MovingPhoto{X,Y}Percent. Modify using "scroll speed" and delta time.
        if (Input.GetMouseButton(InputNames.Mouse_RMB))
		{
			this.MovingPhotoRotation += this.MouseDelta.x;
		}
		else
		{
			this.MovingPhotograph.transform.localPosition = new Vector3(
				this.MovingPhotograph.transform.localPosition.x + (this.MouseDelta.x * 8.66666f),
				this.MovingPhotograph.transform.localPosition.y + (this.MouseDelta.y * 8.66666f),
				0.0f);
		}

		if (Input.GetButton(InputNames.Xbox_LB))
		{
			this.MovingPhotoRotation += Time.deltaTime * 100.0f;
		}

		if (Input.GetButton(InputNames.Xbox_RB))
		{
			this.MovingPhotoRotation -= Time.deltaTime * 100.0f;
		}

		if (Input.GetButton(InputNames.Xbox_Y))
		{
			this.MovingPhotograph.transform.localScale += new Vector3 (Time.deltaTime, Time.deltaTime, Time.deltaTime);

			if (this.MovingPhotograph.transform.localScale.x > 2)
			{
				this.MovingPhotograph.transform.localScale = new Vector3(2, 2, 2);
			}
		}

		if (Input.GetButton(InputNames.Xbox_X))
		{
			this.MovingPhotograph.transform.localScale -= new Vector3 (Time.deltaTime, Time.deltaTime, Time.deltaTime);

			if (this.MovingPhotograph.transform.localScale.x < 1)
			{
				this.MovingPhotograph.transform.localScale = new Vector3(1, 1, 1);
			}
		}

		Vector2 photoPosition = new Vector2(
			this.MovingPhotograph.transform.localPosition.x,
			this.MovingPhotograph.transform.localPosition.y);
		Vector2 photoPositionDelta = new Vector2(
			Input.GetAxis("Horizontal") * 86.66666f * SpeedLimit,
			Input.GetAxis("Vertical") * 86.66666f * SpeedLimit);

		this.MovingPhotograph.transform.localPosition = new Vector3(
			Mathf.Clamp(photoPosition.x + photoPositionDelta.x, -MaxPhotoX, MaxPhotoX),
			Mathf.Clamp(photoPosition.y + photoPositionDelta.y, -MaxPhotoY, MaxPhotoY),
			this.MovingPhotograph.transform.localPosition.z);

		if (Input.GetButtonDown(InputNames.Xbox_A))
		{
			this.HomeCursor.transform.localPosition = this.MovingPhotograph.transform.localPosition;
			this.HomeCursor.gameObject.SetActive(true);

			this.Moving = false;

			this.UpdateButtonPrompts();

			this.PhotoID++;
		}
	}

	void UpdateString()
	{
		this.MouseDelta.x += Input.GetAxis("Horizontal") * 8.66666f * SpeedLimit;
		this.MouseDelta.y += Input.GetAxis("Vertical") * 8.66666f * SpeedLimit;

		Transform Tack = null;

		if (this.StringPhase == 0)
		{
			Tack = String.Origin;
			String.Target.position = String.Origin.position;
		}
		else
		{
			Tack = String.Target;
		}

		Tack.localPosition = new Vector3(
			Tack.localPosition.x - (this.MouseDelta.x * Time.deltaTime * .33333f),
			Tack.localPosition.y + (this.MouseDelta.y * Time.deltaTime * .33333f),
			0.0f);

		if (Tack.localPosition.x > .971f)
		{
			Tack.localPosition = new Vector3(
				.971f,
				Tack.localPosition.y,
				Tack.localPosition.z);
		}
		else if (Tack.localPosition.x < -.971f)
		{
			Tack.localPosition = new Vector3(
				-.971f,
				Tack.localPosition.y,
				Tack.localPosition.z);
		}

		if (Tack.localPosition.y > .637f)
		{
			Tack.localPosition = new Vector3(
				Tack.localPosition.x,
				.637f,
				Tack.localPosition.z);
		}
		else if (Tack.localPosition.y < -.637f)
		{
			Tack.localPosition = new Vector3(
				Tack.localPosition.x,
				-.637f,
				Tack.localPosition.z);
		}

		if (Input.GetButtonDown(InputNames.Xbox_A))
		{
			if (this.StringPhase == 0)
			{
				this.StringPhase++;
			}
			else if (this.StringPhase == 1)
			{
				this.HomeCursor.transform.localPosition = Tack.localPosition;
				this.HomeCursor.gameObject.SetActive(true);

				this.MovingString = false;
				this.StringPhase = 0;

				this.UpdateButtonPrompts();
			}
		}
	}

	void UpdateCorkboardCursor()
	{
		Vector2 cursorPosition = new Vector2(
			this.HomeCursor.transform.localPosition.x,
			this.HomeCursor.transform.localPosition.y);
		Vector2 cursorPositionDelta = new Vector2(
			(this.MouseDelta.x * 8.66666f) + (Input.GetAxis("Horizontal") * 86.66666f * SpeedLimit),
			(this.MouseDelta.y * 8.66666f) + (Input.GetAxis("Vertical") * 86.66666f * SpeedLimit));

		// @todo: Eventually replace usage of ...localPosition.{x,y} with
		// Cursor{X,Y}Percent, and MaxCursor{X,Y} with percents.
		this.HomeCursor.transform.localPosition = new Vector3(
			Mathf.Clamp(cursorPosition.x + cursorPositionDelta.x, -MaxCursorX, MaxCursorX),
			Mathf.Clamp(cursorPosition.y + cursorPositionDelta.y, -MaxCursorY, MaxCursorY),
			this.HomeCursor.transform.localPosition.z);

		if (Input.GetButtonDown(InputNames.Xbox_A))
		{
			if (this.HomeCursor.Photograph != null)
			{
				this.HomeCursor.Highlight.transform.position = new Vector3(
					this.HomeCursor.Highlight.transform.position.x,
					100.0f,
					this.HomeCursor.Highlight.transform.position.z);

				this.MovingPhotograph = this.HomeCursor.Photograph;
				this.HomeCursor.gameObject.SetActive(false);

				this.Moving = true;

				this.UpdateButtonPrompts();
			}
		}

		if (Input.GetButtonDown(InputNames.Xbox_Y))
		{
			GameObject NewStringSet = Instantiate(this.StringSet, transform.position, Quaternion.identity);
			NewStringSet.transform.parent = StringParent;

			NewStringSet.transform.localPosition = new Vector3(0, 0, 0);
			NewStringSet.transform.localScale = new Vector3(1, 1, 1);

			this.String = NewStringSet.GetComponent<StringScript>();
			this.HomeCursor.gameObject.SetActive(false);
			this.MovingString = true;

			this.CorkboardStrings[this.Strings] = String.GetComponent<StringScript>();
			this.CorkboardStrings[this.Strings].ArrayID = this.Strings;
			this.Strings++;

			this.UpdateButtonPrompts();
		}

		if (Input.GetButtonDown(InputNames.Xbox_B))
		{
			if (this.HomeCursor.Photograph != null)
			{
				this.HomeCursor.Photograph = null;
			}

			this.HomeCursor.transform.localPosition = new Vector3(
				0.0f,
				0.0f,
				this.HomeCursor.transform.localPosition.z);

			this.HomeCursor.Highlight.transform.position = new Vector3(
				this.HomeCursor.Highlight.transform.position.x,
				100.0f,
				this.HomeCursor.Highlight.transform.position.z);

			this.CanAdjust = true;
			this.HomeCursor.gameObject.SetActive(false);

			this.Adjusting = false;

			this.UpdateButtonPrompts();
		}

		if (Input.GetButtonDown(InputNames.Xbox_X))
		{
			if (this.HomeCursor.Photograph != null)
			{
				this.HomeCursor.Highlight.transform.position = new Vector3(
					this.HomeCursor.Highlight.transform.position.x,
					100.0f,
					this.HomeCursor.Highlight.transform.position.z);

				this.Shuffle(this.HomeCursor.Photograph.GetComponent<HomeCorkboardPhotoScript>().ArrayID);
				Destroy(this.HomeCursor.Photograph);
				this.Photos--;

				this.HomeCursor.Photograph = null;
				this.UpdateButtonPrompts();
			}

			if (this.HomeCursor.Tack != null)
			{
				this.HomeCursor.CircleHighlight.transform.position = new Vector3(
					this.HomeCursor.CircleHighlight.transform.position.x,
					100.0f,
					this.HomeCursor.CircleHighlight.transform.position.z);

				this.ShuffleStrings(this.HomeCursor.Tack.transform.parent.GetComponent<StringScript>().ArrayID);
				Destroy(this.HomeCursor.Tack.transform.parent.gameObject);
				this.Strings--;

				this.HomeCursor.Tack = null;
				this.UpdateButtonPrompts();
			}
		}
	}

	void Update()
	{
		if (this.GotPhotos)
		{
			if (this.Corkboard && !this.SpawnedPhotos)
			{
				this.SpawnPhotographs();
				this.SpawnStrings();

				this.enabled = false;
				
				this.gameObject.SetActive(false);

				this.PromptBar.Label[0].text = string.Empty;
				this.PromptBar.Label[1].text = string.Empty;
				this.PromptBar.Label[2].text = string.Empty;
				this.PromptBar.Label[3].text = string.Empty;
				this.PromptBar.Label[4].text = string.Empty;
				this.PromptBar.Label[5].text = string.Empty;

				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = false;
			}
		}

		if (!this.Adjusting)
		{
			if (!this.Viewing)
			{
				this.UpdatePhotoSelection();
			}
			else
			{
				this.UpdatePhotoViewing();
			}
		}
		else
		{
			if (this.Corkboard)
			{
				this.Gallery.transform.localPosition = new Vector3(
					this.Gallery.transform.localPosition.x,
					Mathf.Lerp(this.Gallery.transform.localPosition.y, 1000.0f, Time.deltaTime * 10.0f),
					this.Gallery.transform.localPosition.z);
			}

			this.MouseDelta = new Vector2(
				Input.mousePosition.x - this.PreviousPosition.x,
				Input.mousePosition.y - this.PreviousPosition.y);

			if (this.InputDevice.Type == InputDeviceType.MouseAndKeyboard)
			{
				this.SpeedLimit = .1f;
			}
			else
			{
				this.SpeedLimit = 1;
			}

			// If we are currently moving a photograph...
			if (this.Moving)
			{
				this.UpdateCorkboardPhoto();
			}
			// If we are currently moving a photograph...
			else if (this.MovingString)
			{
				this.UpdateString();
			}
			// If we are currently moving the cursor around...
			else
			{
				this.UpdateCorkboardCursor();
			}
		}

		this.PreviousPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
	}

	public IEnumerator GetPhotos()
	{
		//Debug.Log ("We were told to get photos.");

		if (!this.Corkboard)
		{
			for (int ID = 1; ID < 26; ID++)
			{
				this.Hearts[ID].gameObject.SetActive(false);
			}
		}
		
		for (int ID = 1; ID < 26; ID++)
		{
			if (PlayerGlobals.GetPhoto(ID))
			{
				//Debug.Log ("Photo " + ID + " is ''true''.");

				string path = "file:///" + Application.streamingAssetsPath + "/Photographs/Photo_" + ID + ".png";

				//Debug.Log ("Attempting to get " + path);

				WWW www = new WWW(path);

				yield return www;

				if (www.error == null)
				{
					this.Photographs[ID].mainTexture = www.texture;

					if (!this.Corkboard)
					{
						if (PlayerGlobals.GetSenpaiPhoto(ID))
						{
							this.Hearts[ID].gameObject.SetActive(true);
						}
					}
				}
				else
				{
					Debug.Log("Could not retrieve Photo " + ID + ". Maybe it was deleted from Streaming Assets? Setting Photo " + ID + " to ''false''.");

					PlayerGlobals.SetPhoto(ID, false);
				}
			}
		}

		this.LoadingScreen.SetActive(false);

		if (!this.Corkboard)
		{
			this.PauseScreen.Sideways = true;
		}

		this.UpdateButtonPrompts();

		this.enabled = true;
		
		this.gameObject.SetActive(true);

		this.GotPhotos = true;
	}

	public void UpdateButtonPrompts()
	{
		if (this.NamingBully)
		{
			UITexture photograph = this.Photographs[this.CurrentIndex];

			if (photograph.mainTexture != this.NoPhoto && PlayerGlobals.GetBullyPhoto(this.CurrentIndex) > 0)
			{
				if (PlayerGlobals.GetBullyPhoto(this.CurrentIndex) > 0)
				{
					this.PromptBar.Label[0].text = "Name Bully";
				}
				else
				{
					this.PromptBar.Label[0].text = string.Empty;
				}
			}
			else
			{
				this.PromptBar.Label[0].text = string.Empty;
			}

			this.PromptBar.Label[1].text = string.Empty;
			this.PromptBar.Label[2].text = string.Empty;
			this.PromptBar.Label[3].text = string.Empty;
			this.PromptBar.Label[4].text = "Move";
			this.PromptBar.Label[5].text = "Move";
		}
		else if (this.Moving || this.MovingString)
		{
			this.PromptBar.Label[0].text = "Place";
			this.PromptBar.Label[1].text = string.Empty;
			this.PromptBar.Label[2].text = string.Empty;
			this.PromptBar.Label[3].text = string.Empty;
			this.PromptBar.Label[4].text = "Move";
			this.PromptBar.Label[5].text = "Move";

			if (!this.MovingString)
			{
				this.PromptBar.Label[2].text = "Resize";
				this.PromptBar.Label[3].text = "Resize";
			}
		}
		else if (this.Adjusting)
		{
			if (this.HomeCursor.Photograph != null)
			{
				this.PromptBar.Label[0].text = "Adjust";
				this.PromptBar.Label[1].text = string.Empty;
				this.PromptBar.Label[2].text = "Remove";
				this.PromptBar.Label[3].text = string.Empty;
			}
			else if (this.HomeCursor.Tack != null)
			{
				this.PromptBar.Label[2].text = "Remove";
			}
			else
			{
				this.PromptBar.Label[0].text = string.Empty;
				this.PromptBar.Label[2].text = string.Empty;
			}

			this.PromptBar.Label[1].text = "Back";
			this.PromptBar.Label[3].text = "Place Pin";
			this.PromptBar.Label[4].text = "Move";
			this.PromptBar.Label[5].text = "Move";
		}
		else if (!this.Viewing)
		{
			int ID = this.CurrentIndex;

			if (this.Photographs[ID].mainTexture != this.NoPhoto)
			{
				this.PromptBar.Label[0].text = "View";
				this.PromptBar.Label[2].text = "Delete";
			}
			else
			{
				this.PromptBar.Label[0].text = string.Empty;
				this.PromptBar.Label[2].text = string.Empty;
			}

			if (!this.Corkboard)
			{
				this.PromptBar.Label[3].text =
					PlayerGlobals.GetSenpaiPhoto(ID) ? "Use" : string.Empty;
			}
			else
			{
				this.PromptBar.Label[3].text = "Corkboard";
			}

			this.PromptBar.Label[1].text = "Back";
			this.PromptBar.Label[4].text = "Choose";
			this.PromptBar.Label[5].text = "Choose";
		}
		else
		{
			if (this.Corkboard)
			{
				this.PromptBar.Label[0].text = "Place";
			}
			else
			{
				this.PromptBar.Label[0].text = string.Empty;
			}

			this.PromptBar.Label[2].text = string.Empty;
			this.PromptBar.Label[3].text = string.Empty;
			this.PromptBar.Label[4].text = string.Empty;
			this.PromptBar.Label[5].text = string.Empty;
		}

		this.PromptBar.UpdateButtons();
		this.PromptBar.Show = true;
	}

	void Shuffle(int Start)
	{
		for (int ShuffleID = Start; ShuffleID < (this.CorkboardPhotographs.Length - 1); ShuffleID++)
		{
			if (this.CorkboardPhotographs[ShuffleID] != null)
			{
				this.CorkboardPhotographs[ShuffleID].ArrayID--;
				this.CorkboardPhotographs[ShuffleID] = this.CorkboardPhotographs[ShuffleID + 1];
			}
		}
	}

	void ShuffleStrings(int Start)
	{
		for (int ShuffleID = Start; ShuffleID < (this.CorkboardPhotographs.Length - 1); ShuffleID++)
		{
			if (this.CorkboardStrings[ShuffleID] != null)
			{
				this.CorkboardStrings[ShuffleID].ArrayID--;
				this.CorkboardStrings[ShuffleID] = this.CorkboardStrings[ShuffleID + 1];
			}
		}
	}

	public void SaveAllPhotographs()
	{
		for (int ID = 0; ID < 100; ID++)
		{
			if (CorkboardPhotographs[ID] != null)
			{
				PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_Exists", 1);

				PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_PhotoID", CorkboardPhotographs[ID].ID);

				PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_PositionX", CorkboardPhotographs[ID].transform.localPosition.x);
				PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_PositionY", CorkboardPhotographs[ID].transform.localPosition.y);
				PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_PositionZ", CorkboardPhotographs[ID].transform.localPosition.z);

				PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_RotationX", CorkboardPhotographs[ID].transform.localEulerAngles.x);
				PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_RotationY", CorkboardPhotographs[ID].transform.localEulerAngles.y);
				PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_RotationZ", CorkboardPhotographs[ID].transform.localEulerAngles.z);

				PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_ScaleX", CorkboardPhotographs[ID].transform.localScale.x);
				PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_ScaleY", CorkboardPhotographs[ID].transform.localScale.y);
				PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_ScaleZ", CorkboardPhotographs[ID].transform.localScale.z);
			}
			else
			{
				PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_Exists", 0);
			}
		}
	}

	public void SpawnPhotographs()
	{
		for (int ID = 0; ID < 100; ID++)
		{
			if (PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_Exists") == 1)
			{
				GameObject newPhotograph = Instantiate(this.Photograph, this.transform.position, Quaternion.identity);

				newPhotograph.transform.parent = this.CorkboardPanel;

				newPhotograph.transform.localPosition = new Vector3(
					PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_PositionX"),
					PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_PositionY"),
					PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_PositionZ"));

				newPhotograph.transform.localEulerAngles = new Vector3(
					PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_RotationX"),
					PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_RotationY"),
					PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_RotationZ"));

				newPhotograph.transform.localScale = new Vector3(
					PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_ScaleX"),
					PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_ScaleY"),
					PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_ScaleZ"));

				newPhotograph.GetComponent<UITexture>().mainTexture =
					this.Photographs[PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_PhotoID")].mainTexture;

				this.CorkboardPhotographs[this.Photos] = newPhotograph.GetComponent<HomeCorkboardPhotoScript>();
				this.CorkboardPhotographs[this.Photos].ID = PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_PhotoID");
				this.CorkboardPhotographs[this.Photos].ArrayID = this.Photos;
				this.Photos++;
			}
		}

		SpawnedPhotos = true;
	}

	public void SaveAllStrings()
	{
		Debug.Log("Saved strings.");

		for (int ID = 0; ID < 100; ID++)
		{
			if (CorkboardStrings[ID] != null)
			{
				PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CorkboardString_" + ID + "_Exists", 1);

				PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_CorkboardString_" + ID + "_PositionX", CorkboardStrings[ID].Origin.localPosition.x);
				PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_CorkboardString_" + ID + "_PositionY", CorkboardStrings[ID].Origin.localPosition.y);
				PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_CorkboardString_" + ID + "_PositionZ", CorkboardStrings[ID].Origin.localPosition.z);

				PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_CorkboardString2_" + ID + "_PositionX", CorkboardStrings[ID].Target.localPosition.x);
				PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_CorkboardString2_" + ID + "_PositionY", CorkboardStrings[ID].Target.localPosition.y);
				PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_CorkboardString2_" + ID + "_PositionZ", CorkboardStrings[ID].Target.localPosition.z);
			}
			else
			{
				PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CorkboardString_" + ID + "_Exists", 0);
			}
		}
	}

	public void SpawnStrings()
	{
		for (int ID = 0; ID < 100; ID++)
		{
			if (PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CorkboardString_" + ID + "_Exists") == 1)
			{
				GameObject NewStringSet = Instantiate(this.StringSet, transform.position, Quaternion.identity);
				NewStringSet.transform.parent = StringParent;
				NewStringSet.transform.localPosition = new Vector3(0, 0, 0);
				NewStringSet.transform.localScale = new Vector3(1, 1, 1);

				String = NewStringSet.GetComponent<StringScript>();

				String.Origin.localPosition = new Vector3(
					PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_CorkboardString_" + ID + "_PositionX"),
					PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_CorkboardString_" + ID + "_PositionY"),
					PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_CorkboardString_" + ID + "_PositionZ"));

				String.Target.localPosition = new Vector3(
					PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_CorkboardString2_" + ID + "_PositionX"),
					PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_CorkboardString2_" + ID + "_PositionY"),
					PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_CorkboardString2_" + ID + "_PositionZ"));
								
				this.CorkboardStrings[this.Strings] = String.GetComponent<StringScript>();
				this.CorkboardStrings[this.Strings].ArrayID = this.Strings;
				this.Strings++;
			}
			else
			{
				PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CorkboardString_" + ID + "_Exists", 0);
			}
		}
	}
}