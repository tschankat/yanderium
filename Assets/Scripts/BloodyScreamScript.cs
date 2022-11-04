using UnityEngine;

public class BloodyScreamScript : MonoBehaviour
{
	public AudioClip[] Screams;

	void Start()
	{
		AudioSource audio = this.GetComponent<AudioSource>();
		audio.clip = this.Screams[Random.Range(0, Screams.Length)];
		audio.Play();
	}
}
