using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSaveFilesScript : MonoBehaviour
{
	public InputManagerScript InputManager;

	public TitleSaveDataScript[] SaveDatas;

	public GameObject ConfirmationWindow;

	public TitleMenuScript Menu;

	public Transform Highlight;

	public bool Show = false;

	public int ID = 1;

	void Start()
	{
		this.transform.localPosition = new Vector3(
			1050.0f,
			this.transform.localPosition.y,
			this.transform.localPosition.z);

		this.UpdateHighlight();
	}

	void Update()
	{
		if (!this.Show)
		{
			this.transform.localPosition = new Vector3(
				Mathf.Lerp(this.transform.localPosition.x, 1050.0f, Time.deltaTime * 10.0f),
				this.transform.localPosition.y,
				this.transform.localPosition.z);
		}
		else
		{
			this.transform.localPosition = new Vector3(
				Mathf.Lerp(this.transform.localPosition.x, 0.0f, Time.deltaTime * 10.0f),
				this.transform.localPosition.y,
				this.transform.localPosition.z);

			if (!this.ConfirmationWindow.activeInHierarchy)
			{
				if (this.InputManager.TappedDown)
				{
					this.ID++;

					if (this.ID > 3)
					{
						this.ID = 1;
					}

					this.UpdateHighlight();
				}

				if (this.InputManager.TappedUp)
				{
					this.ID--;

					if (this.ID < 1)
					{
						this.ID = 3;
					}

					this.UpdateHighlight();
				}
			}

			if (this.transform.localPosition.x < 50.0f)
			{
				if (!this.ConfirmationWindow.activeInHierarchy)
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						//Debug.Log("ID is: " + this.ID);

						GameGlobals.Profile = this.ID;
						Globals.DeleteAll();
						GameGlobals.Profile = this.ID;

						//Debug.Log("GameGlobals.Profile is: " + GameGlobals.Profile);

						this.Menu.FadeOut = true;
						this.Menu.Fading = true;
					}
					else if (Input.GetButtonDown(InputNames.Xbox_X))
					{
						this.ConfirmationWindow.SetActive(true);
					}
				}
				else
				{
					if (Input.GetButtonDown(InputNames.Xbox_A))
					{
						PlayerPrefs.SetInt("ProfileCreated_" + this.ID, 0);
						this.ConfirmationWindow.SetActive(false);
						this.SaveDatas[this.ID].Start();
					}
					else if (Input.GetButtonDown(InputNames.Xbox_B))
					{
						this.ConfirmationWindow.SetActive(false);
					}
				}
			}
		}
	}

	void UpdateHighlight()
	{
		this.Highlight.localPosition = new Vector3(
			0.0f,
			700.0f - (350.0f * this.ID),
			0.0f);
	}
}
