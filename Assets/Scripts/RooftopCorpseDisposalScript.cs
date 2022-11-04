using UnityEngine;

public class RooftopCorpseDisposalScript : MonoBehaviour
{
	public YandereScript Yandere;

	public PromptScript Prompt;

	public Collider MyCollider;

	public Transform DropSpot;

	void Start()
	{
		if (SchoolGlobals.RoofFence)
		{
			Destroy(this.gameObject);
		}
	}

	void Update()
	{
		if (this.MyCollider.bounds.Contains(this.Yandere.transform.position))
		{
			if (this.Yandere.Ragdoll != null)
			{
				if (!this.Yandere.Dropping)
				{
					this.Prompt.enabled = true;

					this.Prompt.transform.position = new Vector3(
						this.Yandere.transform.position.x,
						this.Yandere.transform.position.y + 1.66666f,
						this.Yandere.transform.position.z);

					if (this.Prompt.Circle[0].fillAmount == 0)
					{
						this.DropSpot.position = new Vector3(
							this.DropSpot.position.x,
							this.DropSpot.position.y,
							this.Yandere.transform.position.z);

						// [af] Replaced if/else statement with ternary expression.
						this.Yandere.CharacterAnimation.CrossFade(
							this.Yandere.Carrying ? AnimNames.FemaleCarryIdleA : AnimNames.FemaleDragIdle);

						this.Yandere.DropSpot = this.DropSpot;
						this.Yandere.Dropping = true;
						this.Yandere.CanMove = false;

						this.Prompt.Hide();
						this.Prompt.enabled = false;

						this.Yandere.Ragdoll.GetComponent<RagdollScript>().BloodPoolSpawner.Falling = true;
					}
				}
			}
			else
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
			}
		}
		else
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
	}
}
