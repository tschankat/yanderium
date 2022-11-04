using UnityEngine;

public class HomeExitScript : MonoBehaviour
{
	public InputManagerScript InputManager;
	public HomeDarknessScript HomeDarkness;
	public HomeYandereScript HomeYandere;
	public HomeCameraScript HomeCamera;
	public HomeWindowScript HomeWindow;
	public Transform Highlight;
	public UILabel[] Labels;
	public int ID = 1;

	void Start()
	{
		UILabel label2 = this.Labels[2];
		label2.color = new Color(
			label2.color.r,
			label2.color.g,
			label2.color.b,
			0.50f);

		if (HomeGlobals.Night)
		{
			UILabel label1 = this.Labels[1];
			label1.color = new Color(label1.color.r, label1.color.g, label1.color.b, 0.50f);
			label2.color = new Color(label2.color.r, label2.color.g, label2.color.b, 1f);

            Debug.Log("Scheme #6 is at stage: " + SchemeGlobals.GetSchemeStage(6));

            if (SchemeGlobals.GetSchemeStage(6) == 5)
            {
                UILabel label4 = this.Labels[4];
                label4.color = new Color(label4.color.r, label4.color.g, label4.color.b, 1f);
                label4.text = "Stalker's House";
            }
		}
	}

	void Update()
	{
		// [af] Combined if statements to reduce nesting.
		if (!this.HomeYandere.CanMove && !this.HomeDarkness.FadeOut)
		{
			if (this.InputManager.TappedDown)
			{
				this.ID++;

				if (this.ID > 4)
				{
					this.ID = 1;
				}

				this.Highlight.localPosition = new Vector3(
					this.Highlight.localPosition.x,
					50.0f - (this.ID * 50.0f),
					this.Highlight.localPosition.z);
			}

			if (this.InputManager.TappedUp)
			{
				this.ID--;

				if (this.ID < 1)
				{
					this.ID = 4;
				}

				this.Highlight.localPosition = new Vector3(
					this.Highlight.localPosition.x,
					50.0f - (this.ID * 50.0f),
					this.Highlight.localPosition.z);
			}

			if (Input.GetButtonDown(InputNames.Xbox_A))
			{
				if (Labels[this.ID].color.a == 1)
				{
					if (this.ID == 1)
					{
						if (SchoolGlobals.SchoolAtmosphere < 0.50f || GameGlobals.LoveSick)
						{
							this.HomeDarkness.Sprite.color = new Color (0.0f, 0.0f, 0.0f, 0.0f);
						}
						else
						{
							this.HomeDarkness.Sprite.color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
						}
					}
					else if (this.ID == 2)
					{
						this.HomeDarkness.Sprite.color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
					}
                    else if (this.ID == 3)
                    {
						this.HomeDarkness.Sprite.color = new Color (0.0f, 0.0f, 0.0f, 0.0f);
					}
                    else if (this.ID == 4)
                    {
                        this.HomeDarkness.Sprite.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
                    }

                    this.HomeDarkness.FadeOut = true;
					this.HomeWindow.Show = false;
					this.enabled = false;
				}
			}

			if (Input.GetButtonDown(InputNames.Xbox_B))
			{
				this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
				this.HomeCamera.Target = this.HomeCamera.Targets[0];
				this.HomeYandere.CanMove = true;
				this.HomeWindow.Show = false;
				this.enabled = false;
			}
		}
	}
}