using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	[SerializeField] UILabel loadingText;
	[SerializeField] UILabel crashText;
	float timer;

	public UILabel[] ControllerText;
	public UILabel[] KeyboardText;

	public GameObject LightAnimation;
	public GameObject DarkAnimation;

	public GameObject Keyboard;
	public GameObject Gamepad;

	public UITexture ControllerLines;
	public UITexture KeyboardGraphic;

	public bool Debugging;

	public float Timer;

	void Start()
	{
        Application.runInBackground = true;

        Time.timeScale = 1.0f;

		if (!SchoolGlobals.SchoolAtmosphereSet)
		{
			SchoolGlobals.SchoolAtmosphereSet = true;
			SchoolGlobals.SchoolAtmosphere = 1.0f;

			PlayerGlobals.Money = 10;
		}

		if (SchoolGlobals.SchoolAtmosphere < 0.50f || GameGlobals.LoveSick)
		{
			Camera.main.backgroundColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
			this.loadingText.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
			this.crashText.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);

			this.KeyboardGraphic.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);

			this.ControllerLines.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);

			this.LightAnimation.SetActive(false);
			this.DarkAnimation.SetActive(true);

			int ID = 1;

			while (ID < ControllerText.Length)
			{
				ControllerText[ID].color = new Color(1, 0, 0, 1);
				ID++;
			}

			ID = 1;

			while (ID < KeyboardText.Length)
			{
				KeyboardText[ID].color = new Color(1, 0, 0, 1);
				ID++;
			}
		}

        //Debug.Log("At the moment we transition into the loading screen, PlayerGlobals.UsingGamepad is: " + PlayerGlobals.UsingGamepad);

        if (PlayerGlobals.UsingGamepad)
		{
			Keyboard.SetActive(false);
			Gamepad.SetActive(true);
		}
        else
        {
            Keyboard.SetActive(true);
            Gamepad.SetActive(false);
        }

		if (!Debugging)
		{
			StartCoroutine(this.LoadNewScene());
		}
	}

	void Update()
	{
		//pulse the transparency of the loading text to let the player know that the computer is still working.
		//loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));

		if (Debugging)
		{
			Timer += Time.deltaTime;

			if (Timer > 10)
			{
				Debugging = false;
				StartCoroutine(this.LoadNewScene());
			}
		}
	}

	// The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
	IEnumerator LoadNewScene()
	{
		//yield return new WaitForSeconds(1);

		// Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
		AsyncOperation async = SceneManager.LoadSceneAsync(SceneNames.SchoolScene);

		// While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
		while (!async.isDone)
		{
			yield return null;
		}
	}
}
