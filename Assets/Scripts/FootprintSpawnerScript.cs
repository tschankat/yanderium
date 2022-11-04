using UnityEngine;

public class FootprintSpawnerScript : MonoBehaviour
{
	public YandereScript Yandere;

	public GameObject BloodyFootprint;

	public AudioClip[] WalkFootsteps;
	public AudioClip[] RunFootsteps;

	public Transform BloodParent;

	public Collider GardenArea;
	public Collider PoolStairs;
    public Collider TreeArea;
    public Collider NEStairs;
	public Collider NWStairs;
	public Collider SEStairs;
	public Collider SWStairs;

	public bool Debugging = false;
	public bool CanSpawn = false;
	public bool FootUp = false;

	public float DownThreshold = 0.0f;
	public float UpThreshold = 0.0f;
	public float Height = 0.0f;

	public int Bloodiness = 0;
	public int Collisions = 0;

	void Start()
	{
        this.GardenArea = this.Yandere.StudentManager.GardenArea;
        this.PoolStairs = this.Yandere.StudentManager.PoolStairs;
        this.TreeArea = this.Yandere.StudentManager.TreeArea;
        this.NEStairs = this.Yandere.StudentManager.NEStairs;
        this.NWStairs = this.Yandere.StudentManager.NWStairs;
        this.SEStairs = this.Yandere.StudentManager.SEStairs;
        this.SWStairs = this.Yandere.StudentManager.SWStairs;
	}

	void Update()
	{
        /*
		if (this.Debugging)
		{
			Debug.Log("UpThreshold: " + (this.Yandere.transform.position.y + this.UpThreshold).ToString() +
				" | DownThreshold: " + (this.Yandere.transform.position.y + this.DownThreshold).ToString() +
				" | CurrentHeight: " + this.transform.position.y.ToString());
		}
        */

		if (!this.FootUp)
		{
			if (this.transform.position.y > (this.Yandere.transform.position.y + this.UpThreshold))
			{
				this.FootUp = true;
			}
		}
		else
		{
			if (this.transform.position.y < (this.Yandere.transform.position.y + this.DownThreshold))
			{
				if ((this.Yandere.Stance.Current != StanceType.Crouching) &&
					(this.Yandere.Stance.Current != StanceType.Crawling) &&
					this.Yandere.CanMove && !this.Yandere.NearSenpai)
				{
					if (this.FootUp)
					{
						AudioSource audioSource = this.GetComponent<AudioSource>();

						if (this.Yandere.Running)
						{
							audioSource.clip = this.RunFootsteps[Random.Range(0, this.RunFootsteps.Length)];
							audioSource.volume = 0.15f;
							audioSource.Play();
						}
						else
						{
							audioSource.clip = this.WalkFootsteps[Random.Range(0, this.WalkFootsteps.Length)];
							audioSource.volume = 0.1f;
							audioSource.Play();
						}
					}
				}

				this.FootUp = false;

				if (this.Bloodiness > 0)
				{
                    this.CanSpawn = !this.GardenArea.bounds.Contains(this.transform.position) &&
                    !this.PoolStairs.bounds.Contains(this.transform.position) &&
                    !this.TreeArea.bounds.Contains(this.transform.position) &&
                    !this.NEStairs.bounds.Contains(this.transform.position) &&
                    !this.NWStairs.bounds.Contains(this.transform.position) &&
                    !this.SEStairs.bounds.Contains(this.transform.position) &&
                    !this.SWStairs.bounds.Contains(this.transform.position);

                    if (this.CanSpawn)
					{
						if (this.transform.position.y > -1.0f &&
							this.transform.position.y < 1.0f)
						{
							this.Height = 0.0f;
						}
						else if (this.transform.position.y > 3.0f &&
							this.transform.position.y < 5.0f)
						{
							this.Height = 4.0f;
						}
						else if (this.transform.position.y > 7.0f &&
							this.transform.position.y < 9.0f)
						{
							this.Height = 8.0f;
						}
						else if (this.transform.position.y > 11.0f &&
							this.transform.position.y < 13.0f)
						{
							this.Height = 12.0f;
						}

						GameObject NewFootprint = Instantiate(this.BloodyFootprint, new Vector3(
							this.transform.position.x, this.Height + 0.012f, this.transform.position.z),
							Quaternion.identity);

						NewFootprint.transform.eulerAngles = new Vector3(
							NewFootprint.transform.eulerAngles.x,
							this.transform.eulerAngles.y,
							NewFootprint.transform.eulerAngles.z);

						NewFootprint.transform.GetChild(0).GetComponent<FootprintScript>().Yandere = this.Yandere;
						NewFootprint.transform.parent = this.BloodParent;

						this.Bloodiness--;
					}
				}
			}
		}
	}
}
