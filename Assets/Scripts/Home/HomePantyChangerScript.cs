using UnityEngine;

public class HomePantyChangerScript : MonoBehaviour
{
	public InputManagerScript InputManager;
	public HomeYandereScript HomeYandere;
	public HomeCameraScript HomeCamera;
	public HomeWindowScript HomeWindow;
	private GameObject NewPanties;
	public UILabel PantyNameLabel;
	public UILabel PantyDescLabel;
	public UILabel PantyBuffLabel;
	public UILabel ButtonLabel;
	public Transform PantyParent;
	public bool DestinationReached = false;
	public float TargetRotation = 0.0f;
	public float Rotation = 0.0f;
	public int TotalPanties = 0;
	public int Selected = 0;
	// [af] Removed "ID" member; replaced with local loop variable.
	public GameObject[] PantyModels;
	public string[] PantyNames;
	public string[] PantyDescs;
	public string[] PantyBuffs;

	void Start()
	{
		// [af] Converted while loop to for loop.
		for (int ID = 0; ID < this.TotalPanties; ID++)
		{
			this.NewPanties = Instantiate(this.PantyModels[ID], 
				new Vector3(this.transform.position.x, this.transform.position.y -.85f, this.transform.position.z - 1.0f), 
				Quaternion.identity);
			this.NewPanties.transform.parent = this.PantyParent;

			this.NewPanties.GetComponent<HomePantiesScript>().PantyChanger = this;
			this.NewPanties.GetComponent<HomePantiesScript>().ID = ID;
			
			this.PantyParent.transform.localEulerAngles = new Vector3(
				this.PantyParent.transform.localEulerAngles.x,
				this.PantyParent.transform.localEulerAngles.y + (360.0f / this.TotalPanties),
				this.PantyParent.transform.localEulerAngles.z);
		}
		
		this.PantyParent.transform.localEulerAngles = new Vector3(
			this.PantyParent.transform.localEulerAngles.x,
			0.0f,
			this.PantyParent.transform.localEulerAngles.z);
		
		this.PantyParent.transform.localPosition = new Vector3(
			this.PantyParent.transform.localPosition.x,
			this.PantyParent.transform.localPosition.y,
			1.80f);

		this.UpdatePantyLabels();
		this.PantyParent.transform.localScale = Vector3.zero;
		this.PantyParent.gameObject.SetActive(false);
	}

	void Update()
	{
		if (this.HomeWindow.Show)
		{
			this.PantyParent.localScale = Vector3.Lerp(
				this.PantyParent.localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
			this.PantyParent.gameObject.SetActive(true);

			if (this.InputManager.TappedRight)
			{
				this.DestinationReached = false;

				this.TargetRotation += 360.0f / this.TotalPanties;
				this.Selected++;

				if (this.Selected > (this.TotalPanties - 1))
				{
					this.Selected = 0;
				}

				this.UpdatePantyLabels();
			}

			if (this.InputManager.TappedLeft)
			{
				this.DestinationReached = false;

				this.TargetRotation -= 360.0f / this.TotalPanties;
				this.Selected--;

				if (this.Selected < 0)
				{
					this.Selected = this.TotalPanties - 1;
				}

				this.UpdatePantyLabels();
			}

			this.Rotation = Mathf.Lerp(this.Rotation, this.TargetRotation, Time.deltaTime * 10.0f);
			
			this.PantyParent.localEulerAngles = new Vector3(
				this.PantyParent.localEulerAngles.x,
				this.Rotation,
				this.PantyParent.localEulerAngles.z);

			if (Input.GetButtonDown(InputNames.Xbox_A))
			{
				if (this.Selected == 0 || CollectibleGlobals.GetPantyPurchased(this.Selected))
				{
					PlayerGlobals.PantiesEquipped = this.Selected;
					Debug.Log("Yandere-chan should now be equipped with Panties #" + PlayerGlobals.PantiesEquipped);
				}
				else
				{
					Debug.Log("Yandere-chan doesn't own those panties.");
				}

				this.UpdatePantyLabels();
			}

			if (Input.GetButtonDown(InputNames.Xbox_B))
			{
				this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
				this.HomeCamera.Target = this.HomeCamera.Targets[0];
				this.HomeYandere.CanMove = true;
				this.HomeWindow.Show = false;
			}
		}
		else
		{
			this.PantyParent.localScale = Vector3.Lerp(
				this.PantyParent.localScale, Vector3.zero, Time.deltaTime * 10.0f);

			if (this.PantyParent.localScale.x < 0.010f)
			{
				this.PantyParent.gameObject.SetActive(false);
			}
		}
	}

	void UpdatePantyLabels()
	{
		if (this.Selected == 0 || CollectibleGlobals.GetPantyPurchased(this.Selected))
		{
			this.PantyNameLabel.text = this.PantyNames[this.Selected];
			this.PantyDescLabel.text = this.PantyDescs[this.Selected];
			this.PantyBuffLabel.text = this.PantyBuffs[this.Selected];
		}
		else
		{
			this.PantyNameLabel.text = "?????";
			this.PantyBuffLabel.text = "?????";

			if (this.Selected < 11)
			{
				this.PantyDescLabel.text = "Unlock these panties by going shopping in town at night!";
			}
			else
			{
				this.PantyDescLabel.text = "Unlock these panties by locating them and picking them up!";
			}
		}

		if (this.Selected == 0 || CollectibleGlobals.GetPantyPurchased(this.Selected))
		{
			this.ButtonLabel.text = (this.Selected == PlayerGlobals.PantiesEquipped) ?
				"Equipped" : "Wear";
		}
		else
		{
			this.ButtonLabel.text = "Unavailable";
		}
	}
}