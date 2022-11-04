using UnityEngine;

public class YanvaniaJukeboxScript : MonoBehaviour
{
	public AudioClip BossIntro;
	public AudioClip BossMain;

	public AudioClip ApproachIntro;
	public AudioClip ApproachMain;

	public bool Boss = false;

	void Update()
	{
		AudioSource audioSource = this.GetComponent<AudioSource>();

		// [af] Simplified this code for readability.
		if ((audioSource.time + Time.deltaTime) > audioSource.clip.length)
		{
			audioSource.clip = this.Boss ? this.BossMain : this.ApproachMain;
			audioSource.loop = true;
			audioSource.Play();
		}
	}

	public void BossBattle()
	{
		AudioSource audioSource = this.GetComponent<AudioSource>();

		audioSource.clip = this.BossIntro;
		audioSource.loop = false;
		audioSource.volume = 0.25f;
		audioSource.Play();

		this.Boss = true;
	}
}
