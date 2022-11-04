using UnityEngine;

public class AccessoryScript : MonoBehaviour
{
	public PromptScript Prompt;

	public Transform Target;

	public float X = 0.0f;
	public float Y = 0.0f;
	public float Z = 0.0f;

	void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.Prompt.MyCollider.enabled = false;

			this.transform.parent = this.Target;
			this.transform.localPosition = new Vector3(this.X, this.Y, this.Z);
			this.transform.localEulerAngles = Vector3.zero;
			this.enabled = false;
		}
	}
}
