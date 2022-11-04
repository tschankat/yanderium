using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public InputDeviceScript InputDevice;
	public PauseScreenScript PauseScreen;
	public PromptBarScript PromptBar;
	public YandereScript Yandere;

	public Transform YandereMapMarker;
	public Transform PortalMapMarker;

	public UILabel ElevationLabel;

	public UISprite Border;

	public Camera MyCamera;

	public float HorizontalLimit = 0;
	public float VerticalLimit = 0;

	public float X;
	public float Y;
	public float W;
	public float H;

	public bool Show;

	void Start()
	{
		DisableCamera();
		X = .5f;
		Y = .5f;
	}

	void Update ()
	{
		if (Input.GetButtonDown(InputNames.Xbox_Back))
		{
			if (this.Yandere.CanMove && !this.Yandere.StudentManager.TutorialWindow.Show && this.Yandere.Police.Darkness.color.a <= 0)
			{
				if (!Show)
				{
					if (!PauseScreen.Show)
					{
						PauseScreen.Show = true;

						Yandere.RPGCamera.enabled = false;
						ElevationLabel.enabled = true;
						Yandere.Blur.enabled = true;
						MyCamera.enabled = true;

						Time.timeScale = 0.001f;

						this.PromptBar.ClearButtons();
						this.PromptBar.Label[1].text = "Exit";
						this.PromptBar.Label[2].text = "Lower Floor";
						this.PromptBar.Label[3].text = "Higher Floor";
						this.PromptBar.UpdateButtons();
						this.PromptBar.Show = true;

						Show = true;
					}
				}
				else
				{
					Yandere.RPGCamera.enabled = true;
					ElevationLabel.enabled = false;
					Yandere.Blur.enabled = false;
					PauseScreen.Show = false;
					Time.timeScale = 1;

					this.PromptBar.ClearButtons();
					this.PromptBar.Show = false;

					Show = false;
				}
			}
		}

		if (Show)
		{
			Border.transform.localScale = Vector3.Lerp(Border.transform.localScale, new Vector3(1.3f, 1.315f, 1.3f), Time.unscaledDeltaTime * 10);

			X = Mathf.Lerp(X, .1f, Time.unscaledDeltaTime * 10);
			Y = Mathf.Lerp(Y, .1f, Time.unscaledDeltaTime * 10);
			W = Mathf.Lerp(W, .8f, Time.unscaledDeltaTime * 10);
			H = Mathf.Lerp(H, .8f, Time.unscaledDeltaTime * 10);

			MyCamera.rect = new Rect(X, Y, W, H);

			if (Border.transform.localScale.x > 1.2f)
			{
				float v = 0;
				float h = 0;

				if (InputDevice.Type == InputDeviceType.MouseAndKeyboard)
				{
					v = Input.GetAxis("Mouse Y");
					h = Input.GetAxis("Mouse X");

					transform.position += new Vector3(h * Time.unscaledDeltaTime * 50, 0, v * Time.unscaledDeltaTime * 50);

					MyCamera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * Time.unscaledDeltaTime * 1000;
				}
				else
				{
					v = Input.GetAxis("Vertical");
					h = Input.GetAxis("Horizontal");

					transform.position += new Vector3(h * Time.unscaledDeltaTime * 100, 0, v * Time.unscaledDeltaTime * 100);

					MyCamera.orthographicSize -= Input.GetAxis("Mouse Y") * Time.unscaledDeltaTime * 100;
				}

				//Debug.Log("V is " + v + " and H is " + h);

				if (MyCamera.orthographicSize < 4)
				{
					MyCamera.orthographicSize = 4;
				}

				if (MyCamera.orthographicSize > 40.75f)
				{
					MyCamera.orthographicSize = 40.75f;
				}

				/////////////////////////////
				///// CHANGING ALTITUDE /////
				/////////////////////////////

				if (Input.GetButtonDown(InputNames.Xbox_X))
				{
					transform.position += new Vector3(0, -4, 0);

					if (transform.position.y < 3)
					{
						transform.position = new Vector3(transform.position.x, 3, transform.position.z);
					}
				}

				if (Input.GetButtonDown(InputNames.Xbox_Y))
				{
					transform.position += new Vector3(0, 4, 0);

					if (transform.position.y > 15)
					{
						transform.position = new Vector3(transform.position.x, 15, transform.position.z);
					}
				}

				     if (transform.position.y == 3) {ElevationLabel.text = "Floor 1";}
				else if (transform.position.y == 7) {ElevationLabel.text = "Floor 2";}
				else if (transform.position.y == 11){ElevationLabel.text = "Floor 3";}
				else if (transform.position.y == 15){ElevationLabel.text = "The Rooftop";}

				//////////////////////////
				///// SETTING LIMITS /////
				//////////////////////////

				HorizontalLimit = 70.72f - ((MyCamera.orthographicSize / 40.75f) * 70.72f);

				if (transform.position.x > HorizontalLimit)
				{
					transform.position = new Vector3(HorizontalLimit, transform.position.y, transform.position.z);
				}

				if (transform.position.x < HorizontalLimit * -1)
				{
					transform.position = new Vector3(HorizontalLimit * -1, transform.position.y, transform.position.z);
				}

				VerticalLimit = 102.0f - ((MyCamera.orthographicSize / 40.75f));

				if (transform.position.z > VerticalLimit)
				{
					transform.position = new Vector3(transform.position.x, transform.position.y, VerticalLimit);
				}

				if (transform.position.z < VerticalLimit * -1)
				{
					transform.position = new Vector3(transform.position.x, transform.position.y, VerticalLimit * -1);
				}

				///////////////////////////////////
				///// YANDERE-CHAN MAP MARKER /////
				///////////////////////////////////

				YandereMapMarker.localScale = new Vector3(
					(MyCamera.orthographicSize / 40.75f) * 10,
					(MyCamera.orthographicSize / 40.75f) * 10,
					(MyCamera.orthographicSize / 40.75f) * 10);

				PortalMapMarker.localScale = new Vector3(
					(MyCamera.orthographicSize / 40.75f) * 10,
					(MyCamera.orthographicSize / 40.75f) * 10,
					(MyCamera.orthographicSize / 40.75f) * 10);

				StudentManager.Students[1].MapMarker.localScale = new Vector3(
					(MyCamera.orthographicSize / 40.75f) * 10,
					(MyCamera.orthographicSize / 40.75f) * 10,
					(MyCamera.orthographicSize / 40.75f) * 10);

				StudentManager.Students[1].MapMarker.eulerAngles = new Vector3(90, 0, 0);

				if (Input.GetButtonDown(InputNames.Xbox_B))
				{
					ElevationLabel.enabled = false;

					PauseScreen.Show = false;
					Yandere.Blur.enabled = false;
					Time.timeScale = 1;

					this.PromptBar.ClearButtons();
					this.PromptBar.Show = false;

					Yandere.RPGCamera.enabled = true;
					Show = false;
				}
			}
		}
		else
		{
			if (MyCamera.enabled)
			{
				Border.transform.localScale = Vector3.Lerp(Border.transform.localScale, new Vector3(0, 0, 0), Time.unscaledDeltaTime * 10);

				X = Mathf.Lerp(X, .5f, Time.unscaledDeltaTime * 10);
				Y = Mathf.Lerp(Y, .5f, Time.unscaledDeltaTime * 10);
				W = Mathf.Lerp(W, 0, Time.unscaledDeltaTime * 10);
				H = Mathf.Lerp(H, 0, Time.unscaledDeltaTime * 10);

				MyCamera.rect = new Rect(X, Y, W, H);

				if (W < .01f)
				{
					DisableCamera();
				}
			}
		}
	}

	void DisableCamera()
	{
		Border.transform.localScale = new Vector3(0, 0, 0);
		MyCamera.rect = new Rect(.5f, .5f, 0, 0);
		ElevationLabel.enabled = false;
		MyCamera.enabled = false;
	}
}