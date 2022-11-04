using UnityEngine;

public class walk : MonoBehaviour
{
	private float timer = 0.0f;
	public float bobbingSpeed = 0.20f;
	public float bobbingAmount = 0.020f;
	public float midpoint = 0.80f;
	public Camera activeCamera;

	void Awake()
	{
		// [af] Moved from class scope for compatibility with C#.
		Cursor.visible = false;
	}

	void FixedUpdate()
	{
		// Script gets camera from game object, otherwise sets 0.8.
		if (this.activeCamera)
		{
			this.midpoint = this.activeCamera.transform.localPosition.y;
		}

		float waveslice = 0.0f;
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		if ((Mathf.Abs(horizontal) == 0.0f) && (Mathf.Abs(vertical) == 0.0f))
		{
			this.timer = 0.0f;
		}
		else
		{
			waveslice = Mathf.Sin(this.timer);
			this.timer += this.bobbingSpeed;

			if (this.timer > (Mathf.PI * 2.0f))
			{
				this.timer -= Mathf.PI * 2.0f;
			}
		}
		if (waveslice != 0.0f)
		{
			float translateChange = waveslice * bobbingAmount;
			float totalAxes = Mathf.Clamp(Mathf.Abs(horizontal) + Mathf.Abs(vertical), 0.0f, 1.0f);
			translateChange = totalAxes * translateChange;

			this.transform.localPosition = new Vector3(
				this.transform.localPosition.x,
				this.midpoint + translateChange,
				this.transform.localPosition.z);
		}
		else
		{
			this.transform.localPosition = new Vector3(
				this.transform.localPosition.x,
				midpoint,
				this.transform.localPosition.z);
		}
	}
}
