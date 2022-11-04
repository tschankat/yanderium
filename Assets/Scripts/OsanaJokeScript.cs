using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OsanaJokeScript : MonoBehaviour
{
	public ConstantRandomRotation[] Rotation;

	public GameObject BloodSplatterEffect;

	public AudioClip BloodSplatterSFX;
	public AudioClip VictoryMusic;

	public AudioSource Jukebox;

	public Transform Head;

	public UILabel Label;

	public bool Advance;

	public float Timer;

	public int ID;

	void Update()
	{
		if (Advance)
		{
			Timer += Time.deltaTime;

            if (Timer > 14)
            {
                Application.Quit();
            }
            else if (Timer > 3)
			{
				Label.text = "Congratulations, you eliminated Osana!";

				if (!Jukebox.isPlaying)
				{
					Jukebox.clip = VictoryMusic;
					Jukebox.Play();
				}
			}
		}
		else
		{
			if (Input.GetKeyDown("f"))
			{
				Rotation[0].enabled = false;
				Rotation[1].enabled = false;
				Rotation[2].enabled = false;
				Rotation[3].enabled = false;
				Rotation[4].enabled = false;
				Rotation[5].enabled = false;
				Rotation[6].enabled = false;
				Rotation[7].enabled = false;

				Instantiate(BloodSplatterEffect, Head.position, Quaternion.identity);

				Head.localScale = new Vector3(0, 0, 0);

				Jukebox.clip = BloodSplatterSFX;
				Jukebox.Play();

				Label.text = "";

				Advance = true;
			}
		}
	}
}