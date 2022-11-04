using UnityEngine;

public class PianoScript : MonoBehaviour
{
	public PromptScript Prompt;

	public AudioSource[] Notes;

	public int ID = 0;

	void Update()
	{
		if (this.Prompt.Circle[0].fillAmount < 1.0f)
		{
			if (this.Prompt.Circle[0].fillAmount > 0.0f)
			{
				this.Prompt.Circle[0].fillAmount = 0.0f;

				this.Notes[this.ID].Play();

				this.ID++;

				if (this.ID == this.Notes.Length)
				{
					this.ID = 0;
				}
			}
		}
	}
}
