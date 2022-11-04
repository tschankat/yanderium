using UnityEngine;
using UnityEngine.SceneManagement;

public class BloodPoolScript : MonoBehaviour
{
	public float TargetSize = 0.0f;
	public bool Blood = true;
	public bool Grow = false;

	public Renderer MyRenderer;
	public Texture Flower;

	void Start()
	{
		if (PlayerGlobals.PantiesEquipped == 7)
		{
			if (this.Blood)
			{
				this.TargetSize *= 0.50f;
			}
		}

		if (GameGlobals.CensorBlood)
		{
			this.MyRenderer.material.color = new Color(1, 1, 1, 1);
			this.MyRenderer.material.mainTexture = this.Flower;
		}

		this.transform.localScale = new Vector3(0.10f, 0.10f, 0.10f);

		Vector3 position = this.transform.position;

		if ((position.x > 125.0f) || (position.x < -125.0f) ||
			(position.z > 200.0f) || (position.z < -100.0f))
		{
			Destroy(this.gameObject);
		}

		if (Application.loadedLevelName == "IntroScene" || Application.loadedLevelName == "NewIntroScene")
		{
			MyRenderer.material.SetColor("_TintColor", new Color(.1f, .1f, .1f));
		}
	}

	void Update()
	{
		if (this.Grow)
		{
			this.transform.localScale = Vector3.Lerp(this.transform.localScale,
				new Vector3(this.TargetSize, this.TargetSize, this.TargetSize), Time.deltaTime);

			if (this.transform.localScale.x > (this.TargetSize * 0.99f))
			{
				this.Grow = false;
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "BloodSpawner")
		{
			this.Grow = true;
		}
	}
}