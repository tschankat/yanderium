using UnityEngine;

public class DoorBoxScript : MonoBehaviour
{
	public UILabel Label;
	public bool Show = false;

	void Update()
	{
		// [af] Converted if/else statement to assignment with ternary expression.
		float localY = Mathf.Lerp(this.transform.localPosition.y,
			this.Show ? -530.0f : -630.0f, Time.deltaTime * 10.0f);
		this.transform.localPosition = new Vector3(
			this.transform.localPosition.x, localY, this.transform.localPosition.z);
	}
}
