using UnityEngine;

public class PoisonScript : MonoBehaviour
{
	public YandereScript Yandere;
	public PromptScript Prompt;

	public GameObject Bottle;

	public void Start()
	{
		this.gameObject.SetActive((this.Yandere.Class.ChemistryGrade + this.Yandere.Class.ChemistryBonus) >= 1);
	}

	void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Yandere.Inventory.ChemicalPoison = true;
			this.Yandere.StudentManager.UpdateAllBentos();

			Destroy(this.gameObject);
			Destroy(this.Bottle);
		}
	}
}
