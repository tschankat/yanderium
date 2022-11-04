using UnityEngine;

public class BlowtorchScript : MonoBehaviour
{
	public YandereScript Yandere;
	public RagdollScript Corpse;
	public PickUpScript PickUp;
	public PromptScript Prompt;
	public Transform Flame;

	public float Timer = 0.0f;

	void Start()
	{
		this.Flame.localScale = Vector3.zero;
		this.enabled = false;
	}

	void Update()
	{
		this.Timer = Mathf.MoveTowards(this.Timer, 5.0f, Time.deltaTime);

		float RandomNumber = Random.Range(0.90f, 1.0f);

		this.Flame.localScale = new Vector3(RandomNumber, RandomNumber, RandomNumber);

		if (this.Timer == 5.0f)
		{
			this.Flame.localScale = Vector3.zero;
			this.Yandere.Cauterizing = false;
			this.Yandere.CanMove = true;
			this.enabled = false;
			this.GetComponent<AudioSource>().Stop();
			this.Timer = 0.0f;
		}
	}
}
