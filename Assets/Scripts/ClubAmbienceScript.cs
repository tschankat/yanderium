using UnityEngine;

public class ClubAmbienceScript : MonoBehaviour
{
	public JukeboxScript Jukebox;
	public Transform Yandere;
	public bool CreateAmbience = false;
	public bool EffectJukebox = false;
	public float ClubDip = 0.0f;
	public float MaxVolume = 0.0f;

	void Update()
	{
		if ((this.Yandere.position.y > (this.transform.position.y - 0.10f)) &&
			(this.Yandere.position.y < (this.transform.position.y + 0.10f)))
		{
			if (Vector3.Distance(this.transform.position, this.Yandere.position) < 4.0f)
			{
				this.CreateAmbience = true;
				this.EffectJukebox = true;
			}
			else
			{
				this.CreateAmbience = false;
			}
		}

		if (this.EffectJukebox)
		{
			AudioSource audioSource = this.GetComponent<AudioSource>();

			if (this.CreateAmbience)
			{
				audioSource.volume = Mathf.MoveTowards(
					audioSource.volume, this.MaxVolume, Time.deltaTime * 0.10f);
				this.Jukebox.ClubDip = Mathf.MoveTowards(
					this.Jukebox.ClubDip, this.ClubDip, Time.deltaTime * 0.10f);
			}
			else
			{
				audioSource.volume = Mathf.MoveTowards(
					audioSource.volume, 0.0f, Time.deltaTime * 0.10f);
				this.Jukebox.ClubDip = Mathf.MoveTowards(
					this.Jukebox.ClubDip, 0.0f, Time.deltaTime * 0.10f);

				if (this.Jukebox.ClubDip == 0.0f)
				{
					this.EffectJukebox = false;
				}
			}
		}
	}
}
