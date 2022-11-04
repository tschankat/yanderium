using System;
using UnityEngine;

public enum InputDeviceType
{
	Gamepad = 1,
	MouseAndKeyboard = 2
}

public class InputDeviceScript : MonoBehaviour
{
	public InputDeviceType Type = InputDeviceType.Gamepad;

	public Vector3 MousePrevious;
	public Vector3 MouseDelta;

	public float Horizontal = 0.0f;
	public float Vertical = 0.0f;

	void Update()
	{
		this.MouseDelta = Input.mousePosition - this.MousePrevious;
		this.MousePrevious = Input.mousePosition;

		InputDeviceType type = this.Type;

		int joysticksCount = Input.GetJoystickNames().Length;

		if (joysticksCount == 0 && Input.anyKey ||
			Input.GetKey(KeyCode.W) ||
			Input.GetKey(KeyCode.A) ||
			Input.GetKey(KeyCode.S) ||
			Input.GetKey(KeyCode.D) ||
			Input.GetMouseButton(0) ||
			Input.GetMouseButton(1) ||
			Input.GetMouseButton(2) ||
			this.MouseDelta != Vector3.zero)
		{
			this.Type = InputDeviceType.MouseAndKeyboard;

            //Debug.Log("Using Keyboard.");
		}
		else
		{
			bool JoystickButtonPressed = false;

			for (int i = 0; i < 20; i++)
			{
				if (Input.GetKey("joystick 1 button " + i))
				{
					JoystickButtonPressed = true;
					break;
				}
			}

			bool DPadPressed = Math.Abs(Input.GetAxis("DpadX")) > 0.5f || Math.Abs(Input.GetAxis("DpadY")) > 0.5f;

			bool StickMoved = Mathf.Abs(Input.GetAxis("Vertical")) == 1f || Mathf.Abs(Input.GetAxis("Horizontal")) == 1f;

			if (JoystickButtonPressed || DPadPressed || StickMoved)
			{
                //Debug.Log("Because JoystickButtonPressed: " + JoystickButtonPressed);
                //Debug.Log("Because DPadPressed: " + DPadPressed);
                //Debug.Log("Because StickMoved: " + StickMoved);

                this.Type = InputDeviceType.Gamepad;

                //Debug.Log("Using gamepad.");
            }
		}

		if (this.Type != type)
		{
			PromptSwapScript[] array = Resources.FindObjectsOfTypeAll<PromptSwapScript>();

			foreach (PromptSwapScript promptSwapScript in array)
			{
				promptSwapScript.UpdateSpriteType(this.Type);
			}

		}

		this.Horizontal = Input.GetAxis("Horizontal");
		this.Vertical = Input.GetAxis("Vertical");
	}
}

/*
 * using UnityEngine;

public enum InputDeviceType
{
	Gamepad = 1,
	MouseAndKeyboard = 2
}

public class InputDeviceScript : MonoBehaviour
{
	public InputDeviceType Type = InputDeviceType.Gamepad;
	public Vector3 MousePrevious;
	public Vector3 MouseDelta;
	public float Horizontal = 0.0f;
	public float Vertical = 0.0f;

	void Update()
	{
		this.MouseDelta = Input.mousePosition - this.MousePrevious;
		this.MousePrevious = Input.mousePosition;

		InputDeviceType previousType = this.Type;

		if (Input.anyKey || Input.GetMouseButton(InputNames.Mouse_LMB) || 
			Input.GetMouseButton(InputNames.Mouse_RMB) ||
			Input.GetMouseButton(InputNames.Mouse_MMB) || 
			(this.MouseDelta != Vector3.zero))
		{
			this.Type = InputDeviceType.MouseAndKeyboard;
		}

		if (Input.GetKey(KeyCode.Joystick1Button0) ||
			Input.GetKey(KeyCode.Joystick1Button1) ||
			Input.GetKey(KeyCode.Joystick1Button2) ||
			Input.GetKey(KeyCode.Joystick1Button3) ||
			Input.GetKey(KeyCode.Joystick1Button4) ||
			Input.GetKey(KeyCode.Joystick1Button5) ||
			Input.GetKey(KeyCode.Joystick1Button6) ||
			Input.GetKey(KeyCode.Joystick1Button7) ||
			Input.GetKey(KeyCode.Joystick1Button8) ||
			Input.GetKey(KeyCode.Joystick1Button9) ||
			Input.GetKey(KeyCode.Joystick1Button10) ||
			Input.GetKey(KeyCode.Joystick1Button11) ||
			Input.GetKey(KeyCode.Joystick1Button12) ||
			Input.GetKey(KeyCode.Joystick1Button13) ||
			Input.GetKey(KeyCode.Joystick1Button14) ||
			Input.GetKey(KeyCode.Joystick1Button15) ||
			Input.GetKey(KeyCode.Joystick1Button16) ||
			Input.GetKey(KeyCode.Joystick1Button17) ||
			Input.GetKey(KeyCode.Joystick1Button18) ||
			Input.GetKey(KeyCode.Joystick1Button19) ||
			(Input.GetAxis("DpadX") > 0.50f) ||
			(Input.GetAxis("DpadX") < -0.50f) ||
			(Input.GetAxis("DpadY") > 0.50f) ||
			(Input.GetAxis("DpadY") < -0.50f))
		{
			this.Type = InputDeviceType.Gamepad;
		}

		if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && 
			!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
		{
			if ((Input.GetAxis("Vertical") == 1.0f) || 
				(Input.GetAxis("Vertical") == -1.0f) || 
				(Input.GetAxis("Horizontal") == 1.0f) || 
				(Input.GetAxis("Horizontal") == -1.0f))
			{
				this.Type = InputDeviceType.Gamepad;
			}
		}

		// [af] If the type is different, update all prompt swap scripts.
		if (this.Type != previousType)
		{
			PromptSwapScript[] promptSwaps = Resources.FindObjectsOfTypeAll<PromptSwapScript>();

			foreach (PromptSwapScript promptSwap in promptSwaps)
			{
				promptSwap.UpdateSpriteType(this.Type);
			}
		}

		this.Horizontal = Input.GetAxis("Horizontal");
		this.Vertical = Input.GetAxis("Vertical");
	}
}

*/