using UnityEngine;
using UnityEngine.SceneManagement;

public class BloodPoolSpawnerScript : MonoBehaviour
{
    public StudentManagerScript StudentManager;

    public RagdollScript Ragdoll;

	public GameObject LastBloodPool;
	public GameObject BloodPool;

	public Transform BloodParent;
	public Transform Hips;

	public Collider MyCollider;
	public Collider GardenArea;
    public Collider TreeArea;
    public Collider NEStairs;
	public Collider NWStairs;
	public Collider SEStairs;
	public Collider SWStairs;

	public Vector3[] Positions;

	public bool CanSpawn = false;
	public bool Falling = false;

	public int PoolsSpawned = 0;
	public int NearbyBlood = 0;

	public float FallTimer = 0.0f;
	public float Height = 0.0f;
	public float Timer = 0.0f;

	public LayerMask Mask;

	public void Start()
	{
        if (SceneManager.GetActiveScene().name == SceneNames.SchoolScene)
		{
            this.PoolsSpawned = this.Ragdoll.Student.BloodPoolsSpawned;

            GardenArea = StudentManager.GardenArea;
            TreeArea = StudentManager.TreeArea;
            NEStairs = StudentManager.NEStairs;
            NWStairs = StudentManager.NWStairs;
            SEStairs = StudentManager.SEStairs;
            SWStairs = StudentManager.SWStairs;
		}

		this.BloodParent = GameObject.Find("BloodParent").transform;

		this.Positions = new Vector3[5];
		this.Positions[0] = Vector3.zero;
		this.Positions[1] = new Vector3(0.50f, 0.012f, 0.0f);
		this.Positions[2] = new Vector3(-0.50f, 0.012f, 0.0f);
		this.Positions[3] = new Vector3(0.0f, 0.012f, 0.50f);
		this.Positions[4] = new Vector3(0.0f, 0.012f, -0.50f);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "BloodPool(Clone)")
		{
			this.LastBloodPool = other.gameObject;
			this.NearbyBlood++;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.name == "BloodPool(Clone)")
		{
			this.NearbyBlood--;
		}
	}

	void Update()
	{
		if (!this.Falling)
		{
			if (this.MyCollider.enabled)
			{
				if (this.Timer > 0.0f)
				{
					this.Timer -= Time.deltaTime;
				}

				// [af] Renamed from "GetHeight()".
				this.SetHeight();

				Vector3 position = this.transform.position;

				if (SceneManager.GetActiveScene().name == SceneNames.SchoolScene)
				{
					// [af] Converted if/else statement to boolean expression.
					this.CanSpawn = !(this.GardenArea.bounds.Contains(position) ||
                        this.TreeArea.bounds.Contains(position) ||
                        this.NEStairs.bounds.Contains(position) ||
						this.NWStairs.bounds.Contains(position) ||
						this.SEStairs.bounds.Contains(position) ||
						this.SWStairs.bounds.Contains(position));

					/*
					if (this.CanSpawn)
					{
						this.CanSpawn = false;

						Debug.Log ("Shooting ray down");

						RaycastHit hit;
						float dist = 1;
						Vector3 dir = new Vector3 (0, -1, 0);

						Debug.DrawRay (transform.position, dir * dist, Color.green);

						bool hitExists = Physics.Linecast(transform.position, transform.position - new Vector3(0, -1, 0), out hit, Mask);

						if (hitExists)
						{
							Debug.Log ("Hit a..." + hit.collider.gameObject.name);

							if (hit.collider.gameObject.layer == 8)
							{
								this.CanSpawn = true;
							}
						}
					}
					*/
				}

				// [af] Combined if statements to reduce nesting.
				if (this.CanSpawn && (position.y < (this.Height + (1.0f / 3.0f))))
				{
					if ((this.NearbyBlood > 0) && (this.LastBloodPool == null))
					{
						this.NearbyBlood--;
					}

					// [af] Combined if statements to reduce nesting.
					if ((this.NearbyBlood < 1) && (this.Timer <= 0.0f))
					{
						this.Timer = 0.10f;

						if (this.PoolsSpawned < 10)
						{
							GameObject NewBloodPool = Instantiate(this.BloodPool,
								new Vector3(position.x, this.Height + 0.012f, position.z),
								Quaternion.identity);
							NewBloodPool.transform.localEulerAngles = new Vector3(
								90.0f, Random.Range(0.0f, 360.0f), 0.0f);
							NewBloodPool.transform.parent = this.BloodParent;

							this.PoolsSpawned++;
                            this.Ragdoll.Student.BloodPoolsSpawned++;
                        }
						// [af] Combined else and if to reduce nesting.
						else if (this.PoolsSpawned < 20)
						{
							GameObject NewBloodPool = Instantiate(this.BloodPool,
								new Vector3(position.x, this.Height + 0.012f, position.z),
								Quaternion.identity);
							NewBloodPool.transform.localEulerAngles = new Vector3(
								90.0f, Random.Range(0.0f, 360.0f), 0.0f);
							NewBloodPool.transform.parent = this.BloodParent;

							this.PoolsSpawned++;
                            this.Ragdoll.Student.BloodPoolsSpawned++;

							NewBloodPool.GetComponent<BloodPoolScript>().TargetSize =
								1.0f - ((this.PoolsSpawned - 10) * 0.10f);

							if (this.PoolsSpawned == 20)
							{
								//Ragdoll.Prompt.HideButton[0] = false;
								//Ragdoll.Prompt.Label[0].text = "";
								gameObject.SetActive (false);
							}
						}
					}
				}
			}
		}
		else
		{
			FallTimer += Time.deltaTime;

			if (FallTimer > 10)
			{
				Falling = false;
			}
		}
	}

	public void SpawnBigPool()
	{
		// [af] Renamed from "GetHeight()".
		this.SetHeight();

		Vector3 hipPosition = new Vector3(
			Hips.position.x, this.Height + 0.012f, Hips.position.z);

		// [af] Converted while loop to for loop.
		for (int id = 0; id < 5; id++)
		{
			GameObject NewBloodPool = Instantiate(this.BloodPool,
				hipPosition + this.Positions[id], Quaternion.identity);
			NewBloodPool.transform.localEulerAngles = new Vector3(
				90.0f, Random.Range(0.0f, 360.0f), 0.0f);
			NewBloodPool.transform.parent = BloodParent;
		}
	}

	void SpawnRow(Transform Location)
	{
		Vector3 position = Location.position;
		Vector3 forward = Location.forward;
		GameObject NewBloodPool = Instantiate(this.BloodPool, 
			position + (forward * 2.0f), Quaternion.identity);
		NewBloodPool.transform.localEulerAngles = new Vector3(90.0f, Random.Range(0.0f, 360.0f), 0.0f);
		NewBloodPool.transform.parent = BloodParent;

		NewBloodPool = Instantiate(this.BloodPool, 
			position + (forward * 2.50f), Quaternion.identity);
		NewBloodPool.transform.localEulerAngles = new Vector3(90.0f, Random.Range(0.0f, 360.0f), 0.0f);
		NewBloodPool.transform.parent = this.BloodParent;

		NewBloodPool = Instantiate(this.BloodPool, 
			position + (forward * 3.0f), Quaternion.identity);
		NewBloodPool.transform.localEulerAngles = new Vector3(90.0f, Random.Range(0.0f, 360.0f), 0.0f);
		NewBloodPool.transform.parent = this.BloodParent;
	}

	public void SpawnPool(Transform Location)
	{
		GameObject NewBloodPool = Instantiate(this.BloodPool, 
			Location.position + Location.forward + new Vector3(0, .0001f, 0), Quaternion.identity);
		NewBloodPool.transform.localEulerAngles = new Vector3(90.0f, Random.Range(0.0f, 360.0f), 0.0f);
		NewBloodPool.transform.parent = this.BloodParent;
	}

	// [af] Renamed from "GetHeight()" to better fit the usage.
	void SetHeight()
	{
		float y = this.transform.position.y;

		if (y < 4.0f)
		{
			this.Height = 0.0f;
		}
		else if (y < 8.0f)
		{
			this.Height = 4.0f;
		}
		else if (y < 12.0f)
		{
			this.Height = 8.0f;
		}
		else
		{
			this.Height = 12.0f;
		}
	}
}
