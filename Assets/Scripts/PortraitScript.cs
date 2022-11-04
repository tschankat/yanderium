using UnityEngine;

public class PortraitScript : MonoBehaviour
{
	public GameObject[] StudentObject;
	public Renderer Renderer1;
	public Renderer Renderer2;
	public Renderer Renderer3;
	public Texture[] HairSet1;
	public Texture[] HairSet2;
	public Texture[] HairSet3;
	public int Selected = 0;
	public int CurrentHair = 0;

	void Start()
	{
		this.StudentObject[1].SetActive(false);
		this.StudentObject[2].SetActive(false);
		this.Selected = 1;

		this.UpdateHair();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			this.StudentObject[0].SetActive(true);
			this.StudentObject[1].SetActive(false);
			this.StudentObject[2].SetActive(false);
			this.Selected = 1;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			this.StudentObject[0].SetActive(false);
			this.StudentObject[1].SetActive(true);
			this.StudentObject[2].SetActive(false);
			this.Selected = 2;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			this.StudentObject[0].SetActive(false);
			this.StudentObject[1].SetActive(false);
			this.StudentObject[2].SetActive(true);
			this.Selected = 3;
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			this.CurrentHair++;

			if (this.CurrentHair > (this.HairSet1.Length - 1))
			{
				this.CurrentHair = 0;
			}

			this.UpdateHair();
		}
	}

	void UpdateHair()
	{
		Texture hair = this.HairSet2[this.CurrentHair];

		this.Renderer1.materials[0].mainTexture = hair;
		this.Renderer1.materials[3].mainTexture = hair;

		this.Renderer2.materials[2].mainTexture = hair;
		this.Renderer2.materials[3].mainTexture = hair;

		this.Renderer3.materials[0].mainTexture = hair;
		this.Renderer3.materials[1].mainTexture = hair;
	}
}
