using UnityEngine;

public class OfferHelpScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public JukeboxScript Jukebox;
	public StudentScript Student;
	public YandereScript Yandere;
	public PromptScript Prompt;

	public UILabel EventSubtitle;

	public Transform[] Locations;
	public AudioClip[] EventClip;
	public string[] EventSpeech;
	public string[] EventAnim;
	public int[] EventSpeaker;

	public bool Offering = false;
	public bool Spoken = false;
	public bool Unable = false;

	public int EventStudentID = 0;
	public int EventPhase = 1;

	public float Timer = 0.0f;

	void Start()
	{
		//this.Prompt.MyCollider.enabled = false;
		this.Prompt.enabled = true;
	}

	void Update()
	{
		if (!this.Unable)
		{
			if (this.Prompt.Circle[0].fillAmount == 0.0f)
			{
				this.Prompt.Circle[0].fillAmount = 1;

				if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
				{
					this.Jukebox.Dip = 0.10f;

					this.Yandere.EmptyHands();
					this.Yandere.CanMove = false;

					this.Student = this.StudentManager.Students[EventStudentID];

					this.Student.Prompt.Label[0].text = "     Talk";
					this.Student.Pushable = false;
					this.Student.Meeting = false;
					this.Student.Routine = false;
					this.Student.MeetTimer = 0.0f;

					this.Offering = true;
				}
			}

			if (this.Offering)
			{
				this.Yandere.transform.rotation = Quaternion.Slerp(
					this.Yandere.transform.rotation, this.transform.rotation, Time.deltaTime * 10.0f);
				this.Yandere.MoveTowardsTarget(
					this.transform.position + Vector3.down);

				Quaternion targetRotation = Quaternion.LookRotation(
					this.Yandere.transform.position - this.Student.transform.position);
				this.Student.transform.rotation = Quaternion.Slerp(
					this.Student.transform.rotation, targetRotation, Time.deltaTime * 10.0f);

				// [af] Commented in JS code.
				//Student.MoveTowardsTarget(transform.position - Vector3.forward);

				Animation yandereCharAnim = this.Yandere.Character.GetComponent<Animation>();
				Animation studentCharAnim = this.Student.Character.GetComponent<Animation>();

				if (!this.Spoken)
				{
					if (this.EventSpeaker[this.EventPhase] == 1)
					{
						yandereCharAnim.CrossFade(this.EventAnim[this.EventPhase]);
						studentCharAnim.CrossFade(this.Student.IdleAnim, 1);
					}
					else
					{
						studentCharAnim.CrossFade(this.EventAnim[this.EventPhase]);
						yandereCharAnim.CrossFade(this.Yandere.IdleAnim, 1);
					}

					this.EventSubtitle.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
					this.EventSubtitle.text = this.EventSpeech[this.EventPhase];

					AudioSource audioSource = this.GetComponent<AudioSource>();
					audioSource.clip = this.EventClip[this.EventPhase];
					audioSource.Play();

					this.Spoken = true;
				}
				else
				{
					if (!this.Yandere.PauseScreen.Show && Input.GetButtonDown(InputNames.Xbox_A))
					{
						this.Timer += this.EventClip[this.EventPhase].length + 1.0f;
					}

					if (this.EventSpeaker[this.EventPhase] == 1)
					{
						if (yandereCharAnim[this.EventAnim[this.EventPhase]].time >=
							yandereCharAnim[this.EventAnim[this.EventPhase]].length)
						{
							yandereCharAnim.CrossFade(this.Yandere.IdleAnim);
						}
					}
					else
					{
						if (studentCharAnim[this.EventAnim[this.EventPhase]].time >=
							studentCharAnim[this.EventAnim[this.EventPhase]].length)
						{
							studentCharAnim.CrossFade(this.Student.IdleAnim);
						}
					}

					this.Timer += Time.deltaTime;

					if (this.Timer > this.EventClip[this.EventPhase].length)
					{
						Debug.Log("Emptying string.");

						this.EventSubtitle.text = string.Empty;
					}
					else
					{
						this.EventSubtitle.text = this.EventSpeech[this.EventPhase];
					}

					if (this.Timer > this.EventClip[this.EventPhase].length + 1.0f)
					{
						//Fragile Girl
						if (this.EventStudentID == 5 && this.EventPhase == 2)
						{
							this.Yandere.PauseScreen.StudentInfoMenu.Targeting = true;

							this.StartCoroutine(this.Yandere.PauseScreen.PhotoGallery.GetPhotos());
							this.Yandere.PauseScreen.PhotoGallery.gameObject.SetActive(true);
							this.Yandere.PauseScreen.PhotoGallery.NamingBully = true;
							this.Yandere.PauseScreen.MainMenu.SetActive(false);
							this.Yandere.PauseScreen.Panel.enabled = true;
							this.Yandere.PauseScreen.Sideways = true;
							this.Yandere.PauseScreen.Show = true;
							Time.timeScale = 0.0001f;

							this.Yandere.PauseScreen.PhotoGallery.UpdateButtonPrompts();

							this.Offering = false;
						}
						else
						{
							Continue();
						}
					}
				}
			}
			else
			{
				if (this.StudentManager.Students[EventStudentID].Pushed ||
					!this.StudentManager.Students[EventStudentID].Alive)
				{
					// [af] Added "gameObject" for C# compatibility.
					this.gameObject.SetActive(false);
				}
			}
		}
		else
		{
			this.Prompt.Circle[0].fillAmount = 1;
		}
	}

	public void UpdateLocation()
	{
		Debug.Log("The ''Offer Help'' prompt for Student " + EventStudentID + " was told to update its location.");

		this.Student = this.StudentManager.Students[EventStudentID];

		//Fountain
		if (this.Student.CurrentDestination == this.StudentManager.MeetSpots.List[7])
		{
			this.transform.position = this.Locations[1].position;
			this.transform.eulerAngles = this.Locations[1].eulerAngles;
		}
		//Behind School
		else if (this.Student.CurrentDestination == this.StudentManager.MeetSpots.List[8])
		{
			this.transform.position = this.Locations[2].position;
			this.transform.eulerAngles = this.Locations[2].eulerAngles;
		}
		//Rooftop
		else if (this.Student.CurrentDestination == this.StudentManager.MeetSpots.List[9])
		{
			this.transform.position = this.Locations[3].position;
			this.transform.eulerAngles = this.Locations[3].eulerAngles;
		}
		//Outdoor Cafeteria
		else if (this.Student.CurrentDestination == this.StudentManager.MeetSpots.List[10])
		{
			this.transform.position = this.Locations[4].position;
			this.transform.eulerAngles = this.Locations[4].eulerAngles;
		}

        //Osana
        if (this.EventStudentID == 11)
        {
            if (!PlayerGlobals.GetStudentFriend(11))
            {
                this.Prompt.Label[0].text = "     " + "Must Befriend Student First";
                this.Unable = true;
            }

            this.Prompt.MyCollider.enabled = true;
        }
        //Kokona
        else if (this.EventStudentID == 30)
		{
			if (!PlayerGlobals.GetStudentFriend(30))
			{
				this.Prompt.Label[0].text = "     " + "Must Befriend Student First";
				this.Unable = true;
			}

			this.Prompt.MyCollider.enabled = true;
		}
		//Horuda
		else if (this.EventStudentID == 5)
		{
			this.Prompt.MyCollider.enabled = true;
		}
	}

	public void Continue()
	{
		Debug.Log("Proceeding to next line.");

		this.Offering = true;
		this.Spoken = false;
		this.EventPhase++;
		this.Timer = 0.0f;

		if (this.EventStudentID == 30)
		{
			if (this.EventPhase == 14)
			{
				//Learning about family.
				if (!ConversationGlobals.GetTopicDiscovered(23))
				{
					this.Yandere.NotificationManager.TopicName = "Family";
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
					ConversationGlobals.SetTopicDiscovered(23, true);
				}

				//Kokona values family.
				if (!ConversationGlobals.GetTopicLearnedByStudent(23, EventStudentID))
				{
					this.Yandere.NotificationManager.TopicName = "Family";
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
					ConversationGlobals.SetTopicLearnedByStudent(23, EventStudentID, true);
				}
			}
		}

		if (this.EventPhase == this.EventSpeech.Length)
		{
            if (this.EventStudentID == 11)
            {
                Debug.Log("Scheme #6 has advanced to stage 5.");

                SchemeGlobals.SetSchemeStage(6, 5);
            }
            else if (this.EventStudentID == 30)
			{
                SchemeGlobals.HelpingKokona = true;

                Debug.Log("SchemeGlobals.HelpingKokona is now true.");
			}

			this.Student.CurrentDestination = this.Student.Destinations[this.Student.Phase];
			this.Student.Pathfinding.target = this.Student.Destinations[this.Student.Phase];
			this.Student.Pathfinding.canSearch = true;
			this.Student.Pathfinding.canMove = true;
			this.Student.Routine = true;

			//this.EventSubtitle.transform.localScale = Vector3.zero;

			this.Yandere.CanMove = true;

			this.Jukebox.Dip = 1.0f;

			Destroy(this.gameObject);
		}
	}
}