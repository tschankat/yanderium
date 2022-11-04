using UnityEngine;

public class RandomSoundScript : MonoBehaviour
{
	public AudioClip[] Clips;

	void Start()
	{
		AudioSource audioSource = this.GetComponent<AudioSource>();
		audioSource.clip = this.Clips[Random.Range(1, this.Clips.Length)];
		audioSource.Play();
	}
}