using UnityEngine;

public class ShoePairScript : MonoBehaviour
{
	public PoliceScript Police;
	public PromptScript Prompt;
	public GameObject Note;

	void Start()
	{
		this.Police = GameObject.Find("Police").GetComponent<PoliceScript>();

		if ((this.Prompt.Yandere.Class.LanguageGrade + this.Prompt.Yandere.Class.LanguageBonus) < 1)
		{
			this.Prompt.enabled = false;
		}

		this.Note.SetActive(false);
	}

	//Suicide Note

	void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.Police.Suicide = true;
			this.Note.SetActive(true);
		}
	}
}
