using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeScript : MonoBehaviour
{
	[SerializeField] JsonScript JSON;

	[SerializeField] GameObject WelcomePanel;

	[SerializeField] UILabel[] FlashingLabels;
	[SerializeField] UILabel AltBeginLabel;
	[SerializeField] UILabel BeginLabel;

	[SerializeField] UISprite Darkness;

	[SerializeField] bool Continue = false;
	[SerializeField] bool FlashRed = false;

	[SerializeField] float VersionNumber = 0.0f;

	[SerializeField] float Timer = 0.0f;

	string Text;

	int ID = 0;

	void Start()
	{
		Time.timeScale = 1;

		this.BeginLabel.color = new Color(
			this.BeginLabel.color.r,
			this.BeginLabel.color.g,
			this.BeginLabel.color.b,
			0.0f);

		this.AltBeginLabel.color = this.BeginLabel.color;

		this.Darkness.color = new Color(
			this.Darkness.color.r,
			this.Darkness.color.g,
			this.Darkness.color.b,
			2.0f);

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		if (ApplicationGlobals.VersionNumber != this.VersionNumber)
		{
			//Globals.DeleteAll();
			ApplicationGlobals.VersionNumber = this.VersionNumber;
		}

		if (!Application.CanStreamedLevelBeLoaded(SceneNames.FunScene))
		{
			Application.Quit();
		}

		if (System.IO.File.Exists(Application.streamingAssetsPath + "/Fun.txt"))
		{
			Text = System.IO.File.ReadAllText(Application.streamingAssetsPath + "/Fun.txt");
		}

		if (Text == "0" || Text == "1" || Text == "2" || Text == "3" || Text == "4" || Text == "5" ||
			Text == "6" || Text == "7" || Text == "8" || Text == "9" || Text == "10" || Text == "69" ||
			Text == "666")
		{
			SceneManager.LoadScene(SceneNames.VeryFunScene);
		}

#if !UNITY_EDITOR
		this.ID = 0;

		while (this.ID < 100)
		{
			if (ID != 10)
			{
				//Debug.Log(this.JSON.Students[ID].Hairstyle);
				//Debug.Log(this.JSON.Students[ID].Persona);

				if (this.JSON.Students[ID].Hairstyle == "21" ||
					this.JSON.Students[ID].Persona == PersonaType.Protective)
				{
					Debug.Log("Player is cheating!");

					if (Application.CanStreamedLevelBeLoaded(SceneNames.FunScene))
					{
						SceneManager.LoadScene(SceneNames.FunScene);
					}
				}
			}

			ID++;
		}
#endif
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.S))
		{
			//SceneManager.LoadScene(SceneNames.SchoolScene);
		}	

		if (Input.GetKeyDown(KeyCode.Y))
		{
			//SceneManager.LoadScene(SceneNames.YanvaniaScene);
		}

		if (!this.Continue)
		{
			this.Darkness.color = new Color(
				this.Darkness.color.r,
				this.Darkness.color.g,
				this.Darkness.color.b,
				this.Darkness.color.a - Time.deltaTime);

			if (this.Darkness.color.a <= 0.0f)
			{
				if (Input.GetKeyDown(KeyCode.W))
				{
					// [af] Commented in JS code.
					//WelcomePanel.active = false;
					//WarningPanel.active = true;
				}

				if (Input.anyKeyDown)
				{
					this.Timer = 5.0f;
				}

				this.Timer += Time.deltaTime;

				if (this.Timer > 5.0f)
				{
					this.BeginLabel.color = new Color(
						this.BeginLabel.color.r,
						this.BeginLabel.color.g,
						this.BeginLabel.color.b,
						this.BeginLabel.color.a + Time.deltaTime);

					this.AltBeginLabel.color = this.BeginLabel.color;

					if (this.BeginLabel.color.a >= 1.0f)
					{
						//if (this.WelcomePanel.activeInHierarchy)
						//{
							if (Input.anyKeyDown)
							{
								this.Darkness.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
								this.Continue = true;
							}
						//}
					}
				}
			}
		}
		else
		{
			this.Darkness.color = new Color(
				this.Darkness.color.r,
				this.Darkness.color.g,
				this.Darkness.color.b,
				this.Darkness.color.a + Time.deltaTime);

			if (this.Darkness.color.a >= 1.0f)
			{
				SceneManager.LoadScene(SceneNames.SponsorScene);
			}
		}

		if (!this.FlashRed)
		{
			this.ID = 0;

			while (this.ID < 3)
			{
				this.ID++;

				this.FlashingLabels[ID].color = new Color(
					this.FlashingLabels[ID].color.r + (Time.deltaTime * 10.0f),
					this.FlashingLabels[ID].color.g,
					this.FlashingLabels[ID].color.b,
					this.FlashingLabels[ID].color.a);

				if (this.FlashingLabels[ID].color.r > 1.0f)
				{
					this.FlashRed = true;
				}
			}
		}
		else
		{
			this.ID = 0;

			while (this.ID < 3)
			{
				this.ID++;

				this.FlashingLabels[ID].color = new Color(
					this.FlashingLabels[ID].color.r - (Time.deltaTime * 10.0f),
					this.FlashingLabels[ID].color.g,
					this.FlashingLabels[ID].color.b,
					this.FlashingLabels[ID].color.a);

				if (this.FlashingLabels[ID].color.r < 0.0f)
				{
					this.FlashRed = false;
				}
			}
		}
	}
}
