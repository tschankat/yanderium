using UnityEngine;

public class InputManagerScript : MonoBehaviour
{
	public bool TappedUp = false;
	public bool TappedDown = false;
	public bool TappedRight = false;
	public bool TappedLeft = false;

	public bool DPadUp = false;
	public bool DPadDown = false;
	public bool DPadRight = false;
	public bool DPadLeft = false;

	public bool StickUp = false;
	public bool StickDown = false;
	public bool StickRight = false;
	public bool StickLeft = false;

	void Update()
	{
		this.TappedUp = false;
		this.TappedDown = false;
		this.TappedRight = false;
		this.TappedLeft = false;

		///////////////////////
		///// UP AND DOWN /////
		///////////////////////

		if (Input.GetAxisRaw("DpadY") > 0.50f)
		{
			// [af] Converted if/else statement to boolean expression.
			this.TappedUp = !this.DPadUp;
			this.DPadUp = true;
		}
		else if (Input.GetAxisRaw("DpadY") < -0.50f)
		{
			// [af] Converted if/else statement to boolean expression.
			this.TappedDown = !this.DPadDown;
			this.DPadDown = true;
		}
		else
		{
			this.DPadUp = false;
			this.DPadDown = false;
		}

		if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
		{
			if (Input.GetAxisRaw("Vertical") > 0.50f)
			{
				// [af] Converted if/else statement to boolean expression.
				this.TappedUp = !this.StickUp;

				// [af] Converted if/else statement to boolean expression.
				this.StickUp = !this.TappedDown;
			}
			else if (Input.GetAxisRaw("Vertical") < -0.50f)
			{
				// [af] Converted if/else statement to boolean expression.
				this.TappedDown = !this.StickDown;

				// [af] Converted if/else statement to boolean expression.
				this.StickDown = !this.TappedUp;
			}
			else
			{
				StickUp = false;
				StickDown = false;
			}
		}

		//////////////////////////
		///// LEFT AND RIGHT /////
		//////////////////////////

		if (Input.GetAxisRaw("DpadX") > 0.50f)
		{
			// [af] Converted if/else statement to boolean expression.
			this.TappedRight = !this.DPadRight;
			this.DPadRight = true;
		}
		else if (Input.GetAxisRaw("DpadX") < -0.50f)
		{
			// [af] Converted if/else statement to boolean expression.
			this.TappedLeft = !this.DPadLeft;
			this.DPadLeft = true;
		}
		else
		{
			this.DPadRight = false;
			this.DPadLeft = false;
		}

		if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
		{
			if (Input.GetAxisRaw("Horizontal") > 0.50f)
			{
				// [af] Converted if/else statement to boolean expression.
				this.TappedRight = !this.StickRight;
				this.StickRight = true;
			}
			else if (Input.GetAxisRaw("Horizontal") < -0.50f)
			{
				// [af] Converted if/else statement to boolean expression.
				this.TappedLeft = !this.StickLeft;
				this.StickLeft = true;
			}
			else
			{
				this.StickRight = false;
				this.StickLeft = false;
			}
		}

		if ((Input.GetAxisRaw("Horizontal") < 0.50f) && 
			(Input.GetAxisRaw("Horizontal") > -0.50f) && 
			(Input.GetAxisRaw("DpadX") < 0.50f) && 
			(Input.GetAxisRaw("DpadX") > -0.50f))
		{
			this.TappedRight = false;
			this.TappedLeft = false;
		}

		if ((Input.GetAxisRaw("Vertical") < 0.50f) && 
			(Input.GetAxisRaw("Vertical") > -0.50f) && 
			(Input.GetAxisRaw("DpadY") < 0.50f) && 
			(Input.GetAxisRaw("DpadY") > -0.50f))
		{
			this.TappedUp = false;
			this.TappedDown = false;
		}

		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
		{
			this.TappedUp = true;
			this.NoStick();
		}

		if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
		{
			this.TappedDown = true;
			this.NoStick();
		}

		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
			this.TappedLeft = true;
			this.NoStick();
		}

		if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
		{
			this.TappedRight = true;
			this.NoStick();
		}

        /*
        if (TappedUp)
        {
            Debug.Log("Tapped up.");
        }

        if (TappedDown)
        {
            Debug.Log("Tapped down.");
        }

        if (TappedRight)
        {
            Debug.Log("Tapped right.");
        }

        if (TappedLeft)
        {
            Debug.Log("Tapped left.");
        }
        */
    }

	void NoStick()
	{
		this.StickUp = false;
		this.StickDown = false;
		this.StickLeft = false;
		this.StickRight = false;
	}
}
