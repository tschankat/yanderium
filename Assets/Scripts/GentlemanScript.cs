using UnityEngine;

public class GentlemanScript : MonoBehaviour
{
	public YandereScript Yandere;

	public AudioClip[] Clips;

	void Update()
	{
		if (Input.GetButtonDown(InputNames.Xbox_RB))
		{
			AudioSource audioSource = this.GetComponent<AudioSource>();

			if (!audioSource.isPlaying)
			{
				// @todo: Should the random range max be "this.Clips.Length"?
				audioSource.clip = this.Clips[Random.Range(0, this.Clips.Length - 1)];
				audioSource.Play();

				this.Yandere.Sanity += 10.0f;
			}
		}
	}
}
