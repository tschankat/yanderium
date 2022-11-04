using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceScript : MonoBehaviour
{
	public InputManagerScript InputManager;
	public PromptBarScript PromptBar;

	public Transform Highlight;

	public UISprite Darkness;

	public int Selected;
	public int Phase;

	void Start ()
	{
		Darkness.color = new Color(1, 1, 1, 1);
	}

	void Update ()
	{
		Highlight.transform.localPosition = Vector3.Lerp(Highlight.transform.localPosition, new Vector3(-360 + (720 * Selected), Highlight.transform.localPosition.y, Highlight.transform.localPosition.z), Time.deltaTime * 10);

		if (Phase == 0)
		{
			Darkness.color = new Color(1, 1, 1, Mathf.MoveTowards(Darkness.color.a, 0.0f, Time.deltaTime * 2));

			if (Darkness.color.a == 0.0f)
			{
				Phase++;
			}
		}
		else if (Phase == 1)
		{
			if (InputManager.TappedLeft)
			{
				Darkness.color = new Color(1, 1, 1, 0);
				Selected = 0;
			}
			else if (InputManager.TappedRight)
			{
				Darkness.color = new Color(0, 0, 0, 0);
				Selected = 1;
			}

			if (Input.GetButtonDown(InputNames.Xbox_A))
			{
				Phase++;
			}
		}
		else if (Phase == 2)
		{
			Darkness.color = new Color(Darkness.color.r, Darkness.color.g, Darkness.color.b, Mathf.MoveTowards(Darkness.color.a, 1.0f, Time.deltaTime * 2));

			if (Darkness.color.a == 1.0f)
			{
				GameGlobals.LoveSick = Selected == 1;
				SceneManager.LoadScene(SceneNames.TitleScene);
			}
		}
	}
}