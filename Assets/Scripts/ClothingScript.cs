using UnityEngine;

public class ClothingScript : MonoBehaviour
{
	public YandereScript Yandere;
	public PromptScript Prompt;
	public GameObject FoldedUniform;
	public bool CanPickUp = false;

	void Start()
	{
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();
	}

	void Update()
	{
		if (this.CanPickUp)
		{
			if (this.Yandere.Bloodiness == 0.0f)
			{
				this.CanPickUp = false;

				this.Prompt.Hide();
				this.Prompt.enabled = false;
			}
		}
		else
		{
			if (this.Yandere.Bloodiness > 0.0f)
			{
				this.CanPickUp = true;

				this.Prompt.enabled = true;
			}
		}

		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Prompt.Yandere.Bloodiness = 0.0f;

			Instantiate(this.FoldedUniform,
				this.transform.position + Vector3.up, Quaternion.identity);

			Destroy(this.gameObject);
		}
	}
}
