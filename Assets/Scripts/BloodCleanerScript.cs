using UnityEngine;
using Pathfinding;

public class BloodCleanerScript : MonoBehaviour
{
	public Transform BloodParent;

	public PromptScript Prompt;

	public AIPath Pathfinding;

	public GameObject Lens;

	public UILabel Label;

	public float Distance = 0.0f;

	public float Blood = 0.0f;

	public bool Super;

	void Start()
	{
		Physics.IgnoreLayerCollision(11, 15, true);
		this.Prompt.Hide();
		this.Prompt.enabled = false;
	}

	void Update()
	{
		if (this.Blood < 100.0f)
		{
			if (this.BloodParent.childCount > 0)
			{
				this.Pathfinding.target = this.BloodParent.GetChild(0);
				this.Pathfinding.speed = 4;

				if (this.Pathfinding.target.position.y < 4.0f)
				{
					this.Label.text = "1";
				}
				else if (this.Pathfinding.target.position.y < 8.0f)
				{
					this.Label.text = "2";
				}
				else if (this.Pathfinding.target.position.y < 12.0f)
				{
					this.Label.text = "3";
				}
				else
				{
					this.Label.text = "R";
				}

				if (this.Pathfinding.target != null)
				{
					this.Distance = Vector3.Distance(this.transform.position,
						this.Pathfinding.target.position);

					if (this.Distance < 1)
					{
						this.Pathfinding.speed = 0.0f;

						Transform bloodParentChild0 = this.BloodParent.GetChild(0);

						if (bloodParentChild0.GetComponent("BloodPoolScript") != null)
						{
							bloodParentChild0.localScale = new Vector3(
								bloodParentChild0.localScale.x - Time.deltaTime,
								bloodParentChild0.localScale.y - Time.deltaTime,
								bloodParentChild0.localScale.z);

							this.Blood += Time.deltaTime;

							if (this.Blood >= 100.0f)
							{
								this.Lens.SetActive(true);
							}

							if (bloodParentChild0.transform.localScale.x < 0.10f)
							{
								Destroy(bloodParentChild0.gameObject);
							}
						}
						else
						{
							Destroy(bloodParentChild0.gameObject);
						}
					}
					else
					{
						this.Pathfinding.speed = 4.0f;
					}
				}
			}
			else
			{
				if (this.Super)
				{
					this.Pathfinding.target = this.Prompt.Yandere.transform;
					this.Pathfinding.speed = 4;
				}
			}
		}
	}
}