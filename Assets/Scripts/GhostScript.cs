using UnityEngine;

public class GhostScript : MonoBehaviour
{
	public Transform SmartphoneCamera;
	public Transform Neck;
	public Transform GhostEyeLocation;
	public Transform GhostEye;
	public int Frame = 0;

	public bool Move;

	void Update()
	{
		/*
		if (Input.GetKeyDown("space"))
		{
			Move = true;
		}

		if (Move)
		{
			this.transform.Translate(this.transform.forward * -1 * Time.deltaTime);
		}
		*/

		if (Time.timeScale > 0.0001f)
		{
			if (this.Frame > 0)
			{
				this.GetComponent<Animation>().enabled = false;

				// [af] Added "gameObject" for C# compatibility.
				this.gameObject.SetActive(false);

				this.Frame = 0;
			}

			this.Frame++;
		}
	}

	public void Look()
	{
		this.Neck.LookAt(this.SmartphoneCamera.position);
	}
}
