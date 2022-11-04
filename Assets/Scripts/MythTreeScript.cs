using UnityEngine;

public class MythTreeScript : MonoBehaviour
{
	public UILabel EventSubtitle;

	public JukeboxScript Jukebox;

	public YandereScript Yandere;

	public bool Spoken = false;

	public AudioSource MyAudio;

	void Start()
	{
		if (SchemeGlobals.GetSchemeStage(2) > 2)
		{
			Destroy(this);
		}
	}

	void Update()
	{
		if (!this.Spoken)
		{
			//if (SchemeGlobals.GetSchemeStage(2) == 2)
			if (this.Yandere.Inventory.Ring)
			{
				if (Vector3.Distance(this.Yandere.transform.position, this.transform.position) < 5.0f)
				{
					this.EventSubtitle.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
					this.EventSubtitle.text = "...that...ring...";
					this.Jukebox.Dip = 0.50f;
					this.Spoken = true;
					this.MyAudio.Play();
				}
			}
		}
		else
		{
			if (!this.MyAudio.isPlaying)
			{
				this.EventSubtitle.transform.localScale = Vector3.zero;
				this.EventSubtitle.text = string.Empty;
				this.Jukebox.Dip = 1.0f;
				Destroy(this);
			}
		}
	}
}
