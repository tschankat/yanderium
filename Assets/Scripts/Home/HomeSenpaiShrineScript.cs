using UnityEngine;

public class HomeSenpaiShrineScript : MonoBehaviour
{
	public InputManagerScript InputManager;
	public PauseScreenScript PauseScreen;
	public HomeYandereScript HomeYandere;
	public HomeCameraScript HomeCamera;
	public HomeWindowScript HomeWindow;
	public GameObject[] Collectibles;
	public Transform[] Destinations;
	public Transform[] Targets;
	public Transform RightDoor;
	public Transform LeftDoor;
	public UILabel NameLabel;
	public UILabel DescLabel;
	public string[] Names;
	public string[] Descs;
	public float Rotation = 0.0f;
	private int Rows = 5;
	private int Columns = 3;
	private int X = 1;
	private int Y = 3;

	void Start()
	{
		this.UpdateText(this.GetCurrentIndex());

		int ID = 1;

		while (ID < 11)
		{
			if (PlayerGlobals.GetShrineCollectible(ID))
			{
				Collectibles[ID].SetActive(true);
			}

			ID++;
		}
	}

	bool InUpperHalf()
	{
		return this.Y < 2;
	}

	int GetCurrentIndex()
	{
		if (this.InUpperHalf())
		{
			// X is ignored in the upper half.
			return this.Y;
		}
		else
		{
			// Bottom half.
			return 2 + (this.X + ((this.Y - 2) * this.Columns));
		}
	}

	void Update()
	{
		if (!this.HomeYandere.CanMove && !this.PauseScreen.Show)
		{
			if (this.HomeCamera.ID == 6)
			{
				this.Rotation = Mathf.Lerp(this.Rotation, 135.0f, Time.deltaTime * 10.0f);

				this.RightDoor.localEulerAngles = new Vector3(
					this.RightDoor.localEulerAngles.x,
					this.Rotation,
					this.RightDoor.localEulerAngles.z);
				
				this.LeftDoor.localEulerAngles = new Vector3(
					this.LeftDoor.localEulerAngles.x,
					-this.Rotation,
					this.LeftDoor.localEulerAngles.z);

				if (this.InputManager.TappedUp)
				{
					this.Y = (this.Y > 0) ? (this.Y - 1) : (this.Rows - 1);
				}

				if (this.InputManager.TappedDown)
				{
					this.Y = (this.Y < (this.Rows - 1)) ? (this.Y + 1) : 0;
				}

				if (this.InputManager.TappedRight)
				{
					if (!this.InUpperHalf())
					{
						this.X = (this.X < (this.Columns - 1)) ? (this.X + 1) : 0;
					}
				}

				if (this.InputManager.TappedLeft)
				{
					if (!this.InUpperHalf())
					{
						this.X = (this.X > 0) ? (this.X - 1) : (this.Columns - 1);
					}
				}

				// [af] Reset the X value to 1 if in the upper half. This isn't used for the 
				// index in the upper half, but it does make the camera behavior more predictable
				// when moving from the upper half to the bottom half.
				if (this.InUpperHalf())
				{
					this.X = 1;
				}

				int index = this.GetCurrentIndex();
				this.HomeCamera.Destination = this.Destinations[index];
				this.HomeCamera.Target = this.Targets[index];

				bool selectionChanged = this.InputManager.TappedUp ||
					this.InputManager.TappedDown || this.InputManager.TappedRight ||
					this.InputManager.TappedLeft;

				if (selectionChanged)
				{
					this.UpdateText(index - 1);
				}

				if (Input.GetButtonDown(InputNames.Xbox_B))
				{
					this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
					this.HomeCamera.Target = this.HomeCamera.Targets[0];
					this.HomeYandere.CanMove = true;

					// [af] Added "gameObject" for C# compatibility.
					this.HomeYandere.gameObject.SetActive(true);

					this.HomeWindow.Show = false;
				}
			}
		}
		else
		{
			this.Rotation = Mathf.Lerp(this.Rotation, 0.0f, Time.deltaTime * 10.0f);

			this.RightDoor.localEulerAngles = new Vector3(
				this.RightDoor.localEulerAngles.x,
				this.Rotation,
				this.RightDoor.localEulerAngles.z);
			this.LeftDoor.localEulerAngles = new Vector3(
				this.LeftDoor.localEulerAngles.x,
				this.Rotation,
				this.LeftDoor.localEulerAngles.z);
		}
	}

	void UpdateText(int newIndex)
	{
		if (newIndex == -1)
		{
			newIndex = 10;
		}

		if (newIndex == 0)
		{
			this.NameLabel.text = this.Names[newIndex];
			this.DescLabel.text = this.Descs[newIndex];
		}
		else
		{
			//Debug.Log("newIndex is: " + newIndex);

			if (PlayerGlobals.GetShrineCollectible(newIndex))
			{
				//Debug.Log("PlayerGlobals.GetShrineCollectible(newIndex) is: " + PlayerGlobals.GetShrineCollectible(newIndex));
					
				this.NameLabel.text = this.Names[newIndex];
				this.DescLabel.text = this.Descs[newIndex];
			}
			else
			{
				this.NameLabel.text = "???";
				this.DescLabel.text = "I'd like to find something that Senpai touched and keep it here...";
			}
		}
	}
}