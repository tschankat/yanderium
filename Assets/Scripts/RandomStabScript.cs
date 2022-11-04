using UnityEngine;

public class RandomStabScript : MonoBehaviour
{
	public AudioClip[] Stabs;
	public AudioClip Bite;
	public bool Biting;

	void Start()
	{
		AudioSource audioSource = this.GetComponent<AudioSource>();

		if (!Biting)
		{
			audioSource.clip = this.Stabs[Random.Range(0, this.Stabs.Length)];
			audioSource.Play();
		}
		else
		{
			audioSource.clip = this.Bite;
			audioSource.pitch = Random.Range(.5f, 1.0f);
			audioSource.Play();
		}
	}
}
