using UnityEngine;

public class RandomAnimScript : MonoBehaviour
{
	public string[] AnimationNames;
	public string CurrentAnim = string.Empty;

	void Start()
	{
		this.PickRandomAnim();
		this.GetComponent<Animation>().CrossFade(this.CurrentAnim);
	}

	void Update()
	{
		AnimationState animState = this.GetComponent<Animation>()[this.CurrentAnim];

		if (animState.time >= animState.length)
		{
			this.PickRandomAnim();
		}
	}

	void PickRandomAnim()
	{
		this.CurrentAnim = this.AnimationNames[Random.Range(0, this.AnimationNames.Length)];
		this.GetComponent<Animation>().CrossFade(this.CurrentAnim);
	}
}
