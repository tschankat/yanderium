using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResolutionScript : MonoBehaviour
{
	public InputManagerScript InputManager;

	public UILabel ResolutionLabel;
	public UILabel FullScreenLabel;
	public UILabel QualityLabel;

	public Transform Highlight;

	public UISprite Darkness;

	public float Alpha = 1;

    public bool FullScreen;
    public bool FadeOut;

	public string[] Qualities;

    public int[] Widths;
    public int[] Heights;

    public int QualityID = 0;
	public int ResID = 1;

	public int ID = 1;

    void Start()
    {
		Darkness.color = new Color(1, 1, 1, 1);

        Cursor.visible = false;
        Screen.fullScreen = false;
        Screen.SetResolution(Screen.width, Screen.height, false);

        ResolutionLabel.text = Screen.width + " x " + Screen.height;
		QualityLabel.text = "" + Qualities[QualitySettings.GetQualityLevel()];
		FullScreenLabel.text = "No";

        Debug.Log("The quality level is set to: " + QualitySettings.GetQualityLevel());

        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-us");
    }
		
    void Update()
    {
		if (FadeOut)
		{
			Alpha = Mathf.MoveTowards(Alpha, 1, Time.deltaTime);

			if (Alpha == 1)
			{
				SceneManager.LoadScene(SceneNames.WelcomeScene);
			}
		}
		else
		{
			Alpha = Mathf.MoveTowards(Alpha, 0, Time.deltaTime);
		}

		Darkness.color = new Color(1, 1, 1, Alpha);

		if (Alpha == 0)
		{
			if (InputManager.TappedDown)
			{
				ID++;
				UpdateHighlight();
			}

			if (InputManager.TappedUp)
			{
				ID--;
				UpdateHighlight();
			}

			if (ID == 1)
			{
				if (InputManager.TappedRight)
				{
					ResID++;
					if (ResID == Widths.Length){ResID = 0;}
					UpdateRes();
				}
				else if (InputManager.TappedLeft)
				{
					ResID--;
					if (ResID < 0){ResID = Widths.Length - 1;}
					UpdateRes();
				}
			}
			else if (ID == 2)
			{
				if (InputManager.TappedRight || InputManager.TappedLeft)
				{
                    FullScreen = !FullScreen;

					if (FullScreen)
					{
						FullScreenLabel.text = "Yes";
                    }
					else
					{
						FullScreenLabel.text = "No";
                    }

                    Screen.SetResolution(Screen.width, Screen.height, FullScreen);
                }
			}
			else if (ID == 3)
			{
				if (InputManager.TappedRight)
				{
					QualityID++;

					if (QualityID == Qualities.Length)
					{
						QualityID = 0;
					}

					UpdateQuality();
				}
				else if (InputManager.TappedLeft)
				{
					QualityID--;

					if (QualityID < 0)
					{
						QualityID = Qualities.Length - 1;
					}

					UpdateQuality();
				}
			}
			else if (ID == 4)
			{
				if (Input.GetButtonUp(InputNames.Xbox_A))
				{
					FadeOut = true;
				}
			}
		}
			
		Highlight.localPosition = Vector3.Lerp(Highlight.localPosition, new Vector3(-307.5f, 250 - (ID * 100), 0), Time.deltaTime * 10);
    }

	void UpdateRes()
	{
		Screen.SetResolution(Widths[ResID], Heights[ResID], Screen.fullScreen);
		ResolutionLabel.text = Widths[ResID] + " x " + Heights[ResID];
	}

	void UpdateQuality()
	{
		QualitySettings.SetQualityLevel(QualityID, true);
		QualityLabel.text = "" + Qualities[QualityID];

        Debug.Log("The quality level is set to: " + QualitySettings.GetQualityLevel());
    }

	void UpdateHighlight()
	{
		if (ID < 1)
		{
			ID = 4;
		}
		else if (ID > 4)
		{
			ID = 1;
		}
	}
}