using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DipJukeboxScript : MonoBehaviour
{
	public JukeboxScript Jukebox;
	public AudioSource MyAudio;
	public Transform Yandere;

	void Update()
	{
		if (MyAudio.isPlaying)
		{
			float Distance = Vector3.Distance(Yandere.position, this.transform.position);

			if (Distance < 8)
			{
				Jukebox.ClubDip = Mathf.MoveTowards(this.Jukebox.ClubDip, ((7 - Distance) * .25f) * this.Jukebox.Volume, Time.deltaTime);

				if (this.Jukebox.ClubDip < 0){this.Jukebox.ClubDip = 0;}
				if (this.Jukebox.ClubDip > this.Jukebox.Volume){this.Jukebox.ClubDip = this.Jukebox.Volume;}
			}
		}
		else
		{
			if (this.MyAudio.isPlaying)
			{
				this.Jukebox.ClubDip = 0;
			}
		}
	}
}