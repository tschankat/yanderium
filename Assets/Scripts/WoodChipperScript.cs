using UnityEngine;

public class WoodChipperScript : MonoBehaviour
{
	public ParticleSystem BloodSpray;

	public PromptScript BucketPrompt;
	public YandereScript Yandere;
	public PickUpScript Bucket;
	public PromptScript Prompt;

	public AudioClip CloseAudio;
	public AudioClip ShredAudio;
	public AudioClip OpenAudio;

	public Transform BucketPoint;
	public Transform DumpPoint;
	public Transform Lid;

	public float Rotation = 0.0f;
	public float Timer = 0.0f;

	public bool Shredding = false;
	public bool Occupied = false;
	public bool Open = false;

	public int VictimID = 0;
	public int Victims = 0;
	public int ID = 0;

	public int[] VictimList;

	void Update()
	{
		if (this.Yandere.PickUp != null)
		{
			if (this.Yandere.PickUp.Bucket != null)
			{
				if (!this.Yandere.PickUp.Bucket.Full)
				{
                    if (this.Bucket == null)
                    {
					    this.BucketPrompt.HideButton[0] = false;

					    if (this.BucketPrompt.Circle[0].fillAmount == 0.0f)
					    {
						    this.Bucket = this.Yandere.PickUp;
						    this.Yandere.EmptyHands();

						    this.Bucket.transform.eulerAngles = this.BucketPoint.eulerAngles;
						    this.Bucket.transform.position = this.BucketPoint.position;
						    this.Bucket.GetComponent<Rigidbody>().useGravity = false;
						    this.Bucket.MyCollider.enabled = false;
					    }
                    }
                    else
                    {
                        this.BucketPrompt.HideButton[0] = true;
                    }
                }
				else
				{
					this.BucketPrompt.HideButton[0] = true;
				}
			}
			else
			{
				this.BucketPrompt.HideButton[0] = true;
			}
		}
		else
		{
			this.BucketPrompt.HideButton[0] = true;
		}

		///////////////////////////////
		///// OPENING AND CLOSING /////
		///////////////////////////////

		AudioSource audioSource = this.GetComponent<AudioSource>();

		if (!this.Open)
		{
			this.Rotation = Mathf.MoveTowards(this.Rotation, 0.0f, Time.deltaTime * 360.0f);

			if (this.Rotation > -36.0f)
			{
				if (this.Rotation < 0.0f)
				{
					audioSource.clip = this.CloseAudio;
					audioSource.Play();
				}

				this.Rotation = 0.0f;
			}

			this.Lid.transform.localEulerAngles = new Vector3(
				this.Rotation,
				this.Lid.transform.localEulerAngles.y,
				this.Lid.transform.localEulerAngles.z);
		}
		else
		{
			if (this.Lid.transform.localEulerAngles.x == 0.0f)
			{
				audioSource.clip = this.OpenAudio;
				audioSource.Play();
			}

			this.Rotation = Mathf.MoveTowards(this.Rotation, -90.0f, Time.deltaTime * 360.0f);

			this.Lid.transform.localEulerAngles = new Vector3(
				this.Rotation,
				this.Lid.transform.localEulerAngles.y,
				this.Lid.transform.localEulerAngles.z);
		}

		/////////////////////////
		///// HIDING PROMPT /////
		/////////////////////////

		// If the machine is not active...
		if (!this.BloodSpray.isPlaying)
		{
			// If there is no corpse inside of the machine...
			if (!this.Occupied)
			{
				// If the player is not carrying a corpse...
				if (this.Yandere.Ragdoll == null)
				{
					// Hide the dump button.
					this.Prompt.HideButton[3] = true;
				}
				// If the player is carrying a corpse...
				else
				{
					// Allow the player to dump the corpse.
					this.Prompt.HideButton[3] = false;
				}
			}
			// If there is a corpse inside of the machine...
			else
			{
				// If there is no bucket...
				if (this.Bucket == null)
				{
					// Hide the activation button.
					this.Prompt.HideButton[0] = true;
				}
				// If there is a bucket...
				else
				{
					// If the bucket is full...
					if (this.Bucket.Bucket.Full)
					{
						//Hide the activation button.
						this.Prompt.HideButton[0] = true;
					}
					// If the bucket is not full...
					else
					{
						// Allow the player to activate the machine.
						this.Prompt.HideButton[0] = false;
					}
				}
			}
		}

		///////////////////
		///// DUMPING /////
		///////////////////

		if (this.Prompt.Circle[3].fillAmount == 0.0f)
		{
			Time.timeScale = 1.0f;

			if (this.Yandere.Ragdoll != null)
			{
				if (!this.Yandere.Carrying)
				{
					this.Yandere.Character.GetComponent<Animation>().CrossFade(AnimNames.FemaleDragIdle);
				}
				else
				{
					this.Yandere.Character.GetComponent<Animation>().CrossFade(AnimNames.FemaleCarryIdleA);
				}

				this.Yandere.YandereVision = false;
				this.Yandere.Chipping = true;
				this.Yandere.CanMove = false;

				this.Victims++;
				this.VictimList[this.Victims] =
					this.Yandere.Ragdoll.GetComponent<RagdollScript>().StudentID;

				this.Open = true;
			}
		}

		//////////////////////
		///// ACTIVATING /////
		//////////////////////

		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			audioSource.clip = this.ShredAudio;
			audioSource.Play();

			this.Prompt.HideButton[3] = false;
			this.Prompt.HideButton[0] = true;

			this.Prompt.Hide();
			this.Prompt.enabled = false;

			// [af] Commented in JS code.
			//Yandere.Police.IncineratedWeapons += MurderWeapons;
			//Yandere.Police.BloodyClothing -= BloodyClothing;
			//Yandere.Police.BloodyWeapons -= MurderWeapons;
			//Yandere.Police.BodyParts -= BodyParts;
			//Yandere.Police.Corpses -= Corpses;

			this.Yandere.Police.Corpses--;

			if (this.Yandere.Police.SuicideScene)
			{
				if (this.Yandere.Police.Corpses == 1)
				{
					this.Yandere.Police.MurderScene = false;
				}
			}

			if (this.Yandere.Police.Corpses == 0)
			{
				this.Yandere.Police.MurderScene = false;
			}

			if (this.Yandere.StudentManager.Students[VictimID].Drowned)
			{
				this.Yandere.Police.DrownVictims--;
			}

			this.Shredding = true;

			this.Yandere.StudentManager.Students[VictimID].Ragdoll.Disposed = true;
		}

		if (this.Shredding)
		{
			if (this.Bucket != null)
			{
				this.Bucket.Bucket.UpdateAppearance = true;
			}

			this.Timer += Time.deltaTime;

			if (this.Timer >= 10.0f)
			{
				this.Prompt.enabled = true;

				this.Shredding = false;
				this.Occupied = false;
				this.Timer = 0.0f;

				// [af] Commented in JS code.
				//BloodyClothing = 0;
				//MurderWeapons = 0;
				//BodyParts = 0;
				//Corpses = 0;
			}
			else if (this.Timer >= 9.0f)
			{
				if (this.Bucket != null)
				{
					this.Bucket.MyCollider.enabled = true;
					this.Bucket.Bucket.FillSpeed = 1.0f;
					this.Bucket = null;

					this.BloodSpray.Stop();
				}
			}
			else if (this.Timer >= .33333f)
			{
				if (!this.Bucket.Bucket.Full)
				{
					this.BloodSpray.GetComponent<AudioSource>().Play();
					this.BloodSpray.Play();

					this.Bucket.Bucket.Bloodiness = 100.0f;
					this.Bucket.Bucket.FillSpeed = 0.050f;
					this.Bucket.Bucket.Full = true;
				}
			}
		}
	}

	public void SetVictimsMissing()
	{
		// [af] Converted while loop to foreach loop.
		foreach (int corpseID in this.VictimList)
		{
			StudentGlobals.SetStudentMissing(corpseID, true);
		}
	}
}