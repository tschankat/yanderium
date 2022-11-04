using UnityEngine;

public class StandWeaponScript : MonoBehaviour
{
	public PromptScript Prompt;
	public StandScript Stand;

	public float RotationSpeed = 0.0f;

	void Update()
	{
		if (this.Prompt.enabled)
		{
			if (this.Prompt.Circle[0].fillAmount == 0.0f)
			{
				this.MoveToStand();
			}

#if UNITY_EDITOR
			if (Input.GetKeyDown(KeyCode.Space))
			{
				//this.MoveToStand();
			}
#endif
		}
		else
		{
			this.transform.Rotate(Vector3.forward * (Time.deltaTime * this.RotationSpeed));
			this.transform.Rotate(Vector3.right * (Time.deltaTime * this.RotationSpeed));
			this.transform.Rotate(Vector3.up * (Time.deltaTime * this.RotationSpeed));
		}
	}

	void MoveToStand()
	{
		this.Prompt.Hide();
		this.Prompt.enabled = false;
		this.Prompt.MyCollider.enabled = false;

		this.Stand.Weapons++;
		this.transform.parent = this.Stand.Hands[this.Stand.Weapons];

		this.transform.localPosition = new Vector3(-0.277f, 0.0f, 0.0f);
	}
}
