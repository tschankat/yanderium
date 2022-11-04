using UnityEngine;

public class PromoCameraScript : MonoBehaviour
{
	public PortraitChanScript PromoCharacter;

	public Vector3[] StartPositions;
	public Vector3[] StartRotations;

	public Renderer PromoBlack;
	public Renderer Noose;
	public Renderer Rope;

	public Camera MyCamera;

	public Transform Drills;

	public float Timer = 0.0f;

	public int ID = 0;

	void Start()
	{
		this.transform.eulerAngles = this.StartRotations[this.ID];
		this.transform.position = this.StartPositions[this.ID];

		this.PromoCharacter.gameObject.SetActive(false);

		this.PromoBlack.material.color = new Color(
			this.PromoBlack.material.color.r,
			this.PromoBlack.material.color.g,
			this.PromoBlack.material.color.b,
			0.0f);

		this.Noose.material.color = new Color(
			this.Noose.material.color.r,
			this.Noose.material.color.g,
			this.Noose.material.color.b,
			0.0f);

		this.Rope.material.color = new Color(
			this.Rope.material.color.r,
			this.Rope.material.color.g,
			this.Rope.material.color.b,
			0.0f);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (this.ID < 3)
			{
				this.ID++;

				this.UpdatePosition();
			}
		}

		if (this.ID == 0)
		{
			this.transform.Translate(Vector3.back * (Time.deltaTime * 0.010f));
		}
		else if (this.ID == 1)
		{
			this.transform.Translate(Vector3.back * (Time.deltaTime * 0.010f));
		}
		else if (this.ID == 2)
		{
			this.transform.Translate(Vector3.forward * (Time.deltaTime * 0.010f));
			this.PromoCharacter.gameObject.SetActive(true);
		}
		else if ((this.ID == 1) || (this.ID == 3))
		{
			this.transform.Translate(Vector3.back * (Time.deltaTime * 0.10f));
		}

		this.Timer += Time.deltaTime;

		if (this.Timer > 20.0f)
		{
			this.Noose.material.color = new Color(
				this.Noose.material.color.r,
				this.Noose.material.color.g,
				this.Noose.material.color.b,
				this.Noose.material.color.a + (Time.deltaTime * 0.20f));

			this.Rope.material.color = new Color(
				this.Rope.material.color.r,
				this.Rope.material.color.g,
				this.Rope.material.color.b,
				this.Rope.material.color.a + (Time.deltaTime * 0.20f));
		}
		else if (this.Timer > 15.0f)
		{
			this.PromoBlack.material.color = new Color(
				this.PromoBlack.material.color.r,
				this.PromoBlack.material.color.g,
				this.PromoBlack.material.color.b,
				this.PromoBlack.material.color.a + (Time.deltaTime * 0.20f));
		}

		if (this.Timer > 10.0f)
		{
			this.Drills.LookAt(this.Drills.position - Vector3.right);

			if (this.ID == 2)
			{
				this.ID = 3;

				this.UpdatePosition();
			}
		}
		else if (this.Timer > 5.0f)
		{
			this.PromoCharacter.EyeShrink += Time.deltaTime * 0.10f;

			if (this.ID == 1)
			{
				this.ID = 2;

				this.UpdatePosition();
			}
		}
	}

	void UpdatePosition()
	{
		this.transform.position = this.StartPositions[this.ID];
		this.transform.eulerAngles = this.StartRotations[this.ID];

		if (this.ID == 2)
		{
			this.MyCamera.farClipPlane = 3.0f;
			this.Timer = 5.0f;
		}

		if (this.ID == 3)
		{
			this.MyCamera.farClipPlane = 5.0f;
			this.Timer = 10.0f;
		}
	}

	// [af] Commented in JS code.
	//-22, 9, 8.5

	//-21.85, 9.125, 8.35

	//-21.8333333333333333, 9.2, 8.313334
}
