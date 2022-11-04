using UnityEngine;

public class JukeboxScript : MonoBehaviour
{
	public YandereScript Yandere;
	public AudioSource SFX;

	public AudioSource AttackOnTitan;
	public AudioSource Megalovania;
	public AudioSource MissionMode;
	public AudioSource Skeletons;
	public AudioSource Vaporwave;
	public AudioSource AzurLane;
	public AudioSource LifeNote;
	public AudioSource Berserk;
	public AudioSource Metroid;
	public AudioSource Nuclear;
	public AudioSource Slender;
	public AudioSource Sukeban;
	public AudioSource Custom;
	public AudioSource Hatred;
	public AudioSource Hitman;
	public AudioSource Horror;
	public AudioSource Touhou;
	public AudioSource Falcon;
	public AudioSource Miyuki;
	public AudioSource Ebola;
	public AudioSource Demon;
	public AudioSource Ninja;
	public AudioSource Punch;
	public AudioSource Galo;
	public AudioSource Jojo;
	public AudioSource Lied;
	public AudioSource Nier;
	public AudioSource Sith;
	public AudioSource DK;

	public AudioSource Confession;

	public AudioSource FullSanity;
	public AudioSource HalfSanity;
	public AudioSource NoSanity;
	public AudioSource Chase;

	public float LastVolume = 0.0f;
	public float FadeSpeed = 0.0f;
	public float ClubDip = 0.0f;
	public float Volume = 0.0f;
	public int Track = 0;
	public int BGM = 0;

	public float Dip = 1.0f;

	public bool StartMusic = false;
	public bool Egg = false;

	public AudioClip[] FullSanities;
	public AudioClip[] HalfSanities;
	public AudioClip[] NoSanities;

	public AudioClip[] OriginalFull;
	public AudioClip[] OriginalHalf;
	public AudioClip[] OriginalNo;

	public AudioClip[] AlternateFull;
	public AudioClip[] AlternateHalf;
	public AudioClip[] AlternateNo;

	public AudioClip[] ThirdFull;
	public AudioClip[] ThirdHalf;
	public AudioClip[] ThirdNo;

	public AudioClip[] FourthFull;
	public AudioClip[] FourthHalf;
	public AudioClip[] FourthNo;

	public AudioClip[] FifthFull;
	public AudioClip[] FifthHalf;
	public AudioClip[] FifthNo;

	public AudioClip[] SixthFull;
	public AudioClip[] SixthHalf;
	public AudioClip[] SixthNo;

	public AudioClip[] SeventhFull;
	public AudioClip[] SeventhHalf;
	public AudioClip[] SeventhNo;

	public AudioClip[] EighthFull;
	public AudioClip[] EighthHalf;
	public AudioClip[] EighthNo;

    public AudioClip[] NinthFull;
    public AudioClip[] NinthHalf;
    public AudioClip[] NinthNo;

    public AudioClip[] TenthFull;
    public AudioClip[] TenthHalf;
    public AudioClip[] TenthNo;

    public void Start()
	{
		if (this.BGM == 0)
		{
			this.BGM = Random.Range (0, 11);
		}
		else
		{
			this.BGM++;

			if (this.BGM > 10)
			{
				this.BGM = 1;
			}
		}

		if (this.BGM == 1)
		{
			this.FullSanities = this.OriginalFull;
			this.HalfSanities = this.OriginalHalf;
			this.NoSanities = this.OriginalNo;
		}
		else if (this.BGM == 2)
		{
			this.FullSanities = this.AlternateFull;
			this.HalfSanities = this.AlternateHalf;
			this.NoSanities = this.AlternateNo;
		}
		else if (this.BGM == 3)
		{
			this.FullSanities = this.ThirdFull;
			this.HalfSanities = this.ThirdHalf;
			this.NoSanities = this.ThirdNo;
		}
		else if (this.BGM == 4)
		{
			this.FullSanities = this.FourthFull;
			this.HalfSanities = this.FourthHalf;
			this.NoSanities = this.FourthNo;
		}
		else if (this.BGM == 5)
		{
			this.FullSanities = this.FifthFull;
			this.HalfSanities = this.FifthHalf;
			this.NoSanities = this.FifthNo;
		}
		else if (this.BGM == 6)
		{
			this.FullSanities = this.SixthFull;
			this.HalfSanities = this.SixthHalf;
			this.NoSanities = this.SixthNo;
		}
		else if (this.BGM == 7)
		{
			this.FullSanities = this.SeventhFull;
			this.HalfSanities = this.SeventhHalf;
			this.NoSanities = this.SeventhNo;
		}
		else if (this.BGM == 8)
		{
			this.FullSanities = this.EighthFull;
			this.HalfSanities = this.EighthHalf;
			this.NoSanities = this.EighthNo;
		}
        else if (this.BGM == 9)
        {
            this.FullSanities = this.NinthFull;
            this.HalfSanities = this.NinthHalf;
            this.NoSanities = this.NinthNo;
        }
        else if (this.BGM == 10)
        {
            this.FullSanities = this.TenthFull;
            this.HalfSanities = this.TenthHalf;
            this.NoSanities = this.TenthNo;
        }

        if (!SchoolGlobals.SchoolAtmosphereSet)
		{
			SchoolGlobals.SchoolAtmosphereSet = true;
			SchoolGlobals.SchoolAtmosphere = 1.0f;
		}

		int Atmosphere = 0;

		if (SchoolAtmosphere.Type == SchoolAtmosphereType.High)
		{
			Atmosphere = 3;
		}
		else if (SchoolAtmosphere.Type == SchoolAtmosphereType.Medium)
		{
			Atmosphere = 2;
		}
		else
		{
			Atmosphere = 1;
		}

		this.FullSanity.clip = this.FullSanities[Atmosphere];
		this.HalfSanity.clip = this.HalfSanities[Atmosphere];
		this.NoSanity.clip = this.NoSanities[Atmosphere];

		//this.FullSanity.Play();
		//this.HalfSanity.Play();
		//this.NoSanity.Play();

		this.Volume = 0.25f;
		this.FullSanity.volume = 0;

		this.Hitman.time = 26.0f;
	}

	void Update()
	{
		if (!this.Yandere.PauseScreen.Show && !this.Yandere.EasterEggMenu.activeInHierarchy)
		{
			if (Input.GetKeyDown(KeyCode.M))
			{
				this.StartStopMusic();
			}
		}

		if (!this.Egg)
		{
			if (!this.Yandere.Police.Clock.SchoolBell.isPlaying && !this.Yandere.StudentManager.MemorialScene.enabled)
			{
				if (!this.StartMusic)
				{
					this.FullSanity.Play();
					this.HalfSanity.Play();
					this.NoSanity.Play();
					this.StartMusic = true;
				}

				if (this.Yandere.Sanity >= (200.0f / 3.0f))
				{
					this.FullSanity.volume = Mathf.MoveTowards(
						this.FullSanity.volume,
						(this.Volume * this.Dip) - this.ClubDip,
						0.01666666666f * this.FadeSpeed);
					this.HalfSanity.volume = Mathf.MoveTowards(
						this.HalfSanity.volume,
						0.0f,
						0.01666666666f * this.FadeSpeed);
					this.NoSanity.volume = Mathf.MoveTowards(
						this.NoSanity.volume,
						0.0f,
						0.01666666666f * this.FadeSpeed);
				}
				else if (Yandere.Sanity >= (100.0f / 3.0f))
				{
					this.FullSanity.volume = Mathf.MoveTowards(
						this.FullSanity.volume,
						0.0f,
						0.01666666666f * this.FadeSpeed);
					this.HalfSanity.volume = Mathf.MoveTowards(
						this.HalfSanity.volume,
						(this.Volume * this.Dip) - this.ClubDip,
						0.01666666666f * this.FadeSpeed);
					this.NoSanity.volume = Mathf.MoveTowards(
						this.NoSanity.volume,
						0.0f,
						0.01666666666f * this.FadeSpeed);
				}
				else
				{
					this.FullSanity.volume = Mathf.MoveTowards(
						this.FullSanity.volume,
						0.0f,
						0.01666666666f * this.FadeSpeed);
					this.HalfSanity.volume = Mathf.MoveTowards(
						this.HalfSanity.volume,
						0.0f,
						0.01666666666f * this.FadeSpeed);
					this.NoSanity.volume = Mathf.MoveTowards(
						this.NoSanity.volume,
						(this.Volume * this.Dip) - this.ClubDip,
						0.01666666666f * this.FadeSpeed);
				}
			}
		}
		else
		{
			this.AttackOnTitan.volume = Mathf.MoveTowards(
				this.AttackOnTitan.volume, this.Volume * this.Dip, 0.01666666666f * 10.0f);
			this.Megalovania.volume = Mathf.MoveTowards(
				this.Megalovania.volume, this.Volume * this.Dip, 0.01666666666f * 10.0f);
			this.MissionMode.volume = Mathf.MoveTowards(
				this.MissionMode.volume, this.Volume * this.Dip, 0.01666666666f * 10.0f);
			this.Skeletons.volume = Mathf.MoveTowards(
				this.Skeletons.volume, this.Volume * this.Dip, 0.01666666666f * 10.0f);
			this.Vaporwave.volume = Mathf.MoveTowards(
				this.Vaporwave.volume, this.Volume * this.Dip, 0.01666666666f * 10.0f);
			this.AzurLane.volume = Mathf.MoveTowards(
				this.AzurLane.volume, this.Volume * this.Dip, 0.01666666666f * 10.0f);
			this.LifeNote.volume = Mathf.MoveTowards(
				this.LifeNote.volume, this.Volume * this.Dip, 0.01666666666f * 10.0f);
			this.Berserk.volume = Mathf.MoveTowards(
				this.Berserk.volume, this.Volume * this.Dip, 0.01666666666f * 10.0f);
			this.Metroid.volume = Mathf.MoveTowards(
				this.Metroid.volume, this.Volume * this.Dip, 0.01666666666f * 10.0f);
			this.Nuclear.volume = Mathf.MoveTowards(
				this.Nuclear.volume, this.Volume * this.Dip, 0.01666666666f * 10.0f);
			this.Slender.volume = Mathf.MoveTowards(
				this.Slender.volume, this.Volume * this.Dip, 0.01666666666f * 10.0f);
			this.Sukeban.volume = Mathf.MoveTowards(
				this.Sukeban.volume, this.Volume * this.Dip, 0.01666666666f * 10.0f);
			this.Custom.volume = Mathf.MoveTowards(
				this.Custom.volume, this.Volume * this.Dip, 0.01666666666f * 10.0f);
			this.Hatred.volume = Mathf.MoveTowards(
				this.Hatred.volume, this.Volume * this.Dip, 0.01666666666f * 10.0f);
			this.Hitman.volume = Mathf.MoveTowards(
				this.Hitman.volume, this.Volume * this.Dip, 0.01666666666f * 10.0f);
			this.Touhou.volume = Mathf.MoveTowards(
				this.Touhou.volume, this.Volume * this.Dip, 0.01666666666f * 10.0f);
			this.Falcon.volume = Mathf.MoveTowards(
				this.Falcon.volume, this.Volume * this.Dip, 0.01666666666f * 10.0f);
			this.Miyuki.volume = Mathf.MoveTowards(
				this.Miyuki.volume, this.Volume * this.Dip, 0.01666666666f * 10.0f);
			this.Demon.volume = Mathf.MoveTowards(
				this.Demon.volume, this.Volume * this.Dip, 0.01666666666f * 10.0f);
			this.Ebola.volume = Mathf.MoveTowards(
				this.Ebola.volume, this.Volume * this.Dip, 0.01666666666f * 10.0f);
			this.Ninja.volume = Mathf.MoveTowards(
				this.Ninja.volume, this.Volume * this.Dip, 0.01666666666f * 10.0f);
			this.Punch.volume = Mathf.MoveTowards(
				this.Punch.volume, this.Volume * this.Dip, 0.01666666666f * 10.0f);
			this.Galo.volume = Mathf.MoveTowards(
				this.Galo.volume, this.Volume * this.Dip, 0.01666666666f * 10.0f);
			this.Jojo.volume = Mathf.MoveTowards(
				this.Jojo.volume, this.Volume * this.Dip, 0.01666666666f * 10.0f);
			this.Lied.volume = Mathf.MoveTowards(
				this.Lied.volume, this.Volume * this.Dip, 0.01666666666f * 10.0f);
			this.Nier.volume = Mathf.MoveTowards(
				this.Nier.volume, this.Volume * this.Dip, 0.01666666666f * 10.0f);
			this.Sith.volume = Mathf.MoveTowards(
				this.Sith.volume, this.Volume * this.Dip, 0.01666666666f * 10.0f);
			this.DK.volume = Mathf.MoveTowards(
				this.DK.volume, this.Volume * this.Dip, 0.01666666666f * 10.0f);
			this.Horror.volume = Mathf.MoveTowards(
				this.Horror.volume, this.Volume * this.Dip, 0.01666666666f * 10.0f);
		}

		if (!this.Yandere.PauseScreen.Show &&
			!this.Yandere.Noticed &&
			this.Yandere.CanMove &&
			this.Yandere.EasterEggMenu.activeInHierarchy &&
			!this.Egg)
		{
			if (Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.Alpha4))
			{
				this.Egg = true;
				this.KillVolume();
				this.AttackOnTitan.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.P))
			{
				this.Egg = true;
				this.KillVolume();
				this.Nuclear.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.H))
			{
				this.Egg = true;
				this.KillVolume();
				this.Hatred.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.B))
			{
				this.Egg = true;
				this.KillVolume();
				this.Sukeban.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Z))
			{
				this.Egg = true;
				this.KillVolume();
				this.Slender.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.G))
			{
				this.Egg = true;
				this.KillVolume();
				this.Galo.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.L))
			{
				this.Egg = true;
				this.KillVolume();
				this.Hitman.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.S))
			{
				this.Egg = true;
				this.KillVolume();
				this.Skeletons.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.K))
			{
				this.Egg = true;
				this.KillVolume();
				this.DK.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.C))
			{
				this.Egg = true;
				this.KillVolume();
				this.Touhou.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.F))
			{
				this.Egg = true;
				this.KillVolume();
				this.Falcon.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.O))
			{
				this.Egg = true;
				this.KillVolume();
				this.Punch.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.U))
			{
				this.Egg = true;
				this.KillVolume();
				this.Megalovania.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.Q))
			{
				this.Egg = true;
				this.KillVolume();
				this.Metroid.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.Y))
			{
				this.Egg = true;
				this.KillVolume();
				this.Ninja.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.M) ||
				Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) ||
				Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.F5) ||
				Input.GetKeyDown(KeyCode.W))
			{
				this.Egg = true;
				this.KillVolume();
				this.Ebola.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.Alpha6))
			{
				this.Egg = true;
				this.KillVolume();
				this.Demon.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.D))
			{
				this.Egg = true;
				this.KillVolume();
				this.Sith.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.F2))
			{
				this.Egg = true;
				this.KillVolume();
				this.Horror.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.F3))
			{
				this.Egg = true;
				this.KillVolume();
				this.LifeNote.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.F6) || Input.GetKeyDown(KeyCode.F9))
			{
				this.Egg = true;
				this.KillVolume();
				this.Lied.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.F7))
			{
				this.Egg = true;
				this.KillVolume();
				this.Berserk.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.F8))
			{
				this.Egg = true;
				this.KillVolume();
				this.Nier.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.V))
			{
				this.Egg = true;
				this.KillVolume();
				this.Vaporwave.enabled = true;
			}
		}
	}

	public void StartStopMusic()
	{
		if (this.Custom.isPlaying)
		{
			this.Egg = false;
			this.Custom.Stop();

			this.FadeSpeed = 1.0f;
			this.StartMusic = false;
			this.Volume = this.LastVolume;

			this.Start();
		}
		else
		{
			if (this.Volume == 0.0f)
			{
				this.FadeSpeed = 1.0f;
				this.StartMusic = false;
				this.Volume = this.LastVolume;

				this.Start();
			}
			else
			{
				this.LastVolume = this.Volume;
				this.FadeSpeed = 10.0f;
				this.Volume = 0.0f;
			}
		}
	}

	public void Shipgirl()
	{
		this.Egg = true;
		this.KillVolume();
		this.AzurLane.enabled = true;
	}

	public void MiyukiMusic()
	{
		this.Egg = true;
		this.KillVolume();
		this.Miyuki.enabled = true;
	}

	public void KillVolume()
	{
		this.FullSanity.volume = 0.0f;
		this.HalfSanity.volume = 0.0f;
		this.NoSanity.volume = 0.0f;
		this.Volume = 0.50f;
	}

	public void GameOver()
	{
		this.AttackOnTitan.Stop();
		this.Megalovania.Stop();
		this.MissionMode.Stop();
		this.Skeletons.Stop();
		this.Vaporwave.Stop();
		this.AzurLane.Stop();
		this.LifeNote.Stop();
		this.Berserk.Stop();
		this.Metroid.Stop();
		this.Nuclear.Stop();
		this.Sukeban.Stop();
		this.Custom.Stop();
		this.Slender.Stop();
		this.Hatred.Stop();
		this.Hitman.Stop();
		this.Horror.Stop();
		this.Touhou.Stop();
		this.Falcon.Stop();
		this.Miyuki.Stop();
		this.Ebola.Stop();
		this.Punch.Stop();
		this.Ninja.Stop();
		this.Jojo.Stop();
		this.Galo.Stop();
		this.Lied.Stop();
		this.Nier.Stop();
		this.Sith.Stop();
		this.DK.Stop();

		this.Confession.Stop();

		this.FullSanity.Stop();
		this.HalfSanity.Stop();
		this.NoSanity.Stop();
		//Chase.Stop();
	}

	public void PlayJojo()
	{
		this.Egg = true;
		this.KillVolume();
		this.Jojo.enabled = true;
	}

	public void PlayCustom()
	{
		this.Egg = true;
		this.KillVolume();
		this.Custom.enabled = true;
		this.Custom.Play();
	}
}