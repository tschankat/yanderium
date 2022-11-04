using UnityEngine;

public class GloveScript : MonoBehaviour
{
	public PromptScript Prompt;
	public PickUpScript PickUp;

	public Collider MyCollider;

	public Projector Blood;

	void Start()
	{
		YandereScript Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();

		Physics.IgnoreCollision(Yandere.GetComponent<Collider>(), this.MyCollider);

		if (this.transform.position.y > 1000.0f)
		{
			this.transform.position = new Vector3(12.0f, 0.0f, 28.0f);
		}
	}

	void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.transform.parent = this.Prompt.Yandere.transform;
			this.transform.localPosition = new Vector3(0.0f, 1.0f, 0.25f);

			this.Prompt.Yandere.Gloves = this;
			this.Prompt.Yandere.WearGloves();

			// [af] Added "gameObject" for C# compatibility.
			this.gameObject.SetActive(false);
		}

		// [af] Replaced if/else statement with boolean expression.
		this.Prompt.HideButton[0] = (this.Prompt.Yandere.Schoolwear != 1) ||
			this.Prompt.Yandere.ClubAttire;
	}
}
