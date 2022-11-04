using UnityEngine;

public class PromptSwapScript : MonoBehaviour
{
	public InputDeviceScript InputDevice;
	public UISprite MySprite;
	public UILabel MyLetter;

	public string KeyboardLetter = string.Empty;
	public string KeyboardName = string.Empty;
	public string GamepadName = string.Empty;

	void Awake()
	{
		if (this.InputDevice == null)
		{
			this.InputDevice = FindObjectOfType<InputDeviceScript>();
		}
	}

	public void UpdateSpriteType(InputDeviceType deviceType)
	{
		if (this.InputDevice == null)
		{
			this.InputDevice = FindObjectOfType<InputDeviceScript>();
		}

		/*
		this.MySprite.spriteName = (deviceType == InputDeviceType.Gamepad) ?
			this.GamepadName : this.KeyboardName;
		*/

		if (deviceType == InputDeviceType.Gamepad)
		{
			MySprite.spriteName = GamepadName;

			if (MyLetter != null)
			{
				MyLetter.text = "";
			}
		}
		else
		{
			MySprite.spriteName = KeyboardName;

			if (MyLetter != null)
			{
				MyLetter.text = KeyboardLetter;
			}
			else
			{
				//Debug.Log("My name is " + gameObject.name + " and I don't have a ''MyLetter'' assigned.");
			}
		}
	}
}
