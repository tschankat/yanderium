using UnityEngine;

public class LiquidColliderScript : MonoBehaviour
{
	private GameObject NewPool;

	public AudioClip SplashSound;

	public GameObject GroundSplash;
	public GameObject Splash;
	public GameObject Pool;

	public bool AlreadyDoused = false;
	public bool Static = false;
	public bool Bucket = false;
	public bool Blood = false;
	public bool Gas = false;

	void Start()
	{
		if (this.Bucket)
		{
			this.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 400.0f);
		}
	}

	void Update()
	{
		if (!this.Static)
		{
			if (!this.Bucket)
			{
				if (this.transform.position.y < 0.0f)
				{
					Instantiate(this.GroundSplash,
						new Vector3(this.transform.position.x, 0.0f, this.transform.position.z),
						Quaternion.identity);

					this.NewPool = Instantiate(this.Pool,
						new Vector3(this.transform.position.x, 0.012f, this.transform.position.z),
						Quaternion.identity);
					this.NewPool.transform.localEulerAngles =
						new Vector3(90.0f, Random.Range(0.0f, 360.0f), 0.0f);

					if (this.Blood)
					{
						this.NewPool.transform.parent = GameObject.Find("BloodParent").transform;
					}

					Destroy(this.gameObject);
				}
			}
			else
			{
				this.transform.localScale = new Vector3(
					this.transform.localScale.x + (Time.deltaTime * 2.0f),
					this.transform.localScale.y + (Time.deltaTime * 2.0f),
					this.transform.localScale.z + (Time.deltaTime * 2.0f));
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (!AlreadyDoused)
		{
			if (other.gameObject.layer == 9)
			{
				StudentScript Student = other.gameObject.GetComponent<StudentScript>();

				if (Student != null)
				{
					if (!Student.BeenSplashed && Student.StudentID > 1 && Student.StudentID != 10 &&
					    !Student.Teacher && Student.Club != ClubType.Council && !Student.Fleeing &&
						Student.CurrentAction != StudentActionType.Sunbathe && !Student.GasWarned)
					{
						AudioSource.PlayClipAtPoint(this.SplashSound, this.transform.position);

						Instantiate(this.Splash,
							new Vector3(this.transform.position.x, 1.50f, this.transform.position.z),
							Quaternion.identity);

						if (this.Blood)
						{
							Student.Bloody = true;
						}
						else if (this.Gas)
						{
							Student.Gas = true;
						}

						Student.GetWet();

						AlreadyDoused = true;
						Destroy(this.gameObject);
					}
					else
					{
						if (!Student.Wet && !Student.Fleeing)
						{
							Debug.Log(Student.Name + " just dodged a bucket of water.");

							if (Student.Investigating)
							{
								Student.StopInvestigating();
							}

							if (Student.ReturningMisplacedWeapon)
							{
								Student.DropMisplacedWeapon();
							}

							Student.CharacterAnimation.CrossFade(Student.DodgeAnim);

							Student.Pathfinding.canSearch = false;
							Student.Pathfinding.canMove = false;

							Student.Routine = false;
							Student.DodgeSpeed = 2;
							Student.Dodging = true;

							if (Student.Following)
							{
								ParticleSystem.EmissionModule heartsEmission = Student.Hearts.emission;
                                heartsEmission.enabled = false;

                                Student.FollowCountdown.gameObject.SetActive(false);
                                Student.Yandere.Follower = null;
                                Student.Yandere.Followers--;
								Student.Following = false;

								Student.CurrentDestination = Student.Destinations[Student.Phase];
								Student.Pathfinding.target = Student.Destinations[Student.Phase];
								Student.Pathfinding.speed = 1.0f;
							}
						}
					}
				}
			}
		}
	}
}