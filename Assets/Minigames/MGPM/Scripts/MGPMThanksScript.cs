using UnityEngine;
using UnityEngine.SceneManagement;

public class MGPMThanksScript : MonoBehaviour
{
	public AudioClip ThanksMusic;

	public AudioSource Jukebox;

	public Renderer Black;

	public int Phase = 1;

	void Start()
	{
		Black.material.color = new Color(0, 0, 0, 1);
	}

	void Update()
	{
		if (Phase == 1)
		{
			Black.material.color = new Color(0, 0, 0, Mathf.MoveTowards(Black.material.color.a, 0, Time.deltaTime));

			if (Black.material.color.a == 0)
			{
				Jukebox.Play();
				Phase++;
			}
		}
		else if (Phase == 2)
		{
			if (!Jukebox.isPlaying)
			{
				Jukebox.clip = ThanksMusic;
				Jukebox.loop = true;
				Jukebox.Play();
				Phase++;
			}
		}
		else if (Phase == 3)
		{
			if (Input.anyKeyDown)
			{
				Phase++;
			}
		}
		else
		{
			Black.material.color = new Color(0, 0, 0, Mathf.MoveTowards(Black.material.color.a, 1, Time.deltaTime));
			Jukebox.volume = 1 - Black.material.color.a;

			if (Black.material.color.a == 1)
			{
				HomeGlobals.MiyukiDefeated = true;

				SceneManager.LoadScene(SceneNames.HomeScene);
			}
		}
	}
}