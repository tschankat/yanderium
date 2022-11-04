using UnityEngine;

public class AlarmDiscScript : MonoBehaviour
{
	public AudioClip[] LongFemaleScreams;
	public AudioClip[] LongMaleScreams;

	public AudioClip[] FemaleScreams;
	public AudioClip[] MaleScreams;

	public AudioClip[] DelinquentScreams;

	public StudentScript Originator;
	public RadioScript SourceRadio;
	public StudentScript Student;

	public bool FocusOnYandere = false;
	public bool StudentIsBusy = false;
	public bool Delinquent = false;
	public bool NoScream = false;
	public bool Shocking = false;
	public bool Radio = false;
	public bool Male = false;
	public bool Long = false;

	public float Hesitation = 1.0f;

	public int Frame = 0;

	void Start()
	{
		/*
		if (Originator != null)
		{
			Debug.Log("An alarm disc was created by " + Originator.Name + ".");

			if (Originator.WitnessedMurder)
			{
				Debug.Log(Originator.Name + " witnessed murder.");

				Debug.Log("Everyone who was touched by this disc should ignore other discs, and look towards Yandere-chan.");
			}
		}
		*/

		Vector3 localScale = this.transform.localScale;
		localScale.x *= 2.0f - SchoolGlobals.SchoolAtmosphere;
		localScale.z = localScale.x;
		this.transform.localScale = localScale;
	}

	void Update()
	{
		if (this.Frame > 0)
		{
			Destroy(this.gameObject);
		}
		else
		{
			if (!this.NoScream)
			{
				if (!this.Long)
				{
					if (this.Originator != null)
					{
						this.Male = this.Originator.Male;
					}

					if (!this.Male)
					{
						this.PlayClip(this.FemaleScreams[Random.Range(0, this.FemaleScreams.Length)], this.transform.position);
					}
					else
					{
						if (this.Delinquent)
						{
							this.PlayClip(this.DelinquentScreams[Random.Range(0, this.DelinquentScreams.Length)], this.transform.position);
						}
						else
						{	
							this.PlayClip(this.MaleScreams[Random.Range(0, this.MaleScreams.Length)], this.transform.position);
						}
					}
				}
				else
				{
					if (!this.Male)
					{
						this.PlayClip(LongFemaleScreams[Random.Range(0, LongFemaleScreams.Length)], this.transform.position);
					}
					else
					{
						this.PlayClip(LongMaleScreams[Random.Range(0, LongMaleScreams.Length)], this.transform.position);
					}
				}
			}
		}

		Frame++;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			this.Student = other.gameObject.GetComponent<StudentScript>();

			if (this.Student != null)
			{
                Debug.Log("Student's ''Hurry'' is: " + this.Student.Hurry);

				if (this.Student.enabled)
				{
					if (this.Student.DistractionSpot != new Vector3(
						this.transform.position.x, this.Student.transform.position.y, this.transform.position.z))
					{
						Destroy(this.Student.Giggle);

						this.Student.InvestigationTimer = 0.0f;
						this.Student.InvestigationPhase = 0;
						this.Student.Investigating = false;
						this.Student.DiscCheck = false;
						this.Student.VisionDistance++;

						this.Student.ChalkDust.Stop();
						this.Student.CleanTimer = 0;

						//Not a radio...
						if (!this.Radio)
						{
							if (this.Student != this.Originator)
							{
								if (this.Student.Clock.Period == 3 && this.Student.BusyAtLunch)
								{
									//Debug.Log(this.Student.Name + " is ''BusyAtLunch''");
									StudentIsBusy = true;
								}

								//The two sparring martial artists.
								if (this.Student.StudentID == 47 || this.Student.StudentID == 49)
								{
									if (this.Student.StudentManager.ConvoManager.Confirmed)
									{
										StudentIsBusy = true;
									}
								}

                                //The late boy.
                                if (this.Student.StudentID == 7 && this.Student.Hurry)
                                {
                                    this.Student.Distracted = false;
                                }

                                if (this.Student.StudentID == this.Student.StudentManager.RivalID || this.Student.StudentID == 1)
								{
									if (this.Student.CurrentAction == StudentActionType.SitAndEatBento)
									{
										//StudentIsBusy = true;
									}
								}

								Debug.Log("An alarm disc has come into contact with: " + this.Student.Name);

								if (!this.Student.TurnOffRadio && this.Student.Alive && !this.Student.Pushed &&
								    !this.Student.Dying && !this.Student.Alarmed && !this.Student.Guarding &&
								    !this.Student.Wet && !this.Student.Slave && !this.Student.CheckingNote &&
								    !this.Student.WitnessedMurder && !this.Student.WitnessedCorpse &&
								    !this.StudentIsBusy && !this.Student.FocusOnYandere && !this.Student.Fleeing &&
								    !this.Student.Shoving && !this.Student.SentHome && this.Student.ClubActivityPhase < 16 &&
								    !this.Student.Vomiting && !this.Student.Lethal && !this.Student.Headache && !this.Student.Sedated &&
								    !this.Student.SenpaiWitnessingRivalDie ||
									this.Student.Persona == PersonaType.Protective && this.Originator.StudentID == 11)
								{
									Debug.Log("Nothing is stopping " + this.Student.Name + " from reacting.");

									if (this.Student.Male)
									{
										//Student.CharacterAnimation[Student.LeanAnim].time = 4;
									}
							
									if (!this.Student.Struggling)
									{
										this.Student.Character.GetComponent<Animation>().CrossFade(this.Student.LeanAnim);
										//Debug.Log(this.Student.Name + " was told to perform their LeanAnim.");
									}

									if (this.Originator != null)
									{
										if (this.Originator.WitnessedMurder)
										{
											//Debug.Log(this.Student.Name + " witnessed murder, and is directing attention towards Yandere-chan.");

											this.Student.DistractionSpot = new Vector3(
												this.transform.position.x,
												this.Student.Yandere.transform.position.y,
												this.transform.position.z);
										}
										else
										{
											if (this.Originator.Corpse == null)
											{
												this.Student.DistractionSpot = new Vector3(
													this.transform.position.x,
													this.Student.transform.position.y,
													this.transform.position.z);
											}
											else
											{
												this.Student.DistractionSpot = new Vector3(
													this.Originator.Corpse.transform.position.x,
													this.Student.transform.position.y,
													this.Originator.Corpse.transform.position.z);
											}
										}
									}
									else
									{
										this.Student.DistractionSpot = new Vector3(
											this.transform.position.x,
											this.Student.transform.position.y,
											this.transform.position.z);
									}

									this.Student.DiscCheck = true;

									//Debug.Log(this.Student.name + "'s ''DiskCheck'' was just set to ''true''.");

									if (this.Shocking)
									{
										this.Student.Hesitation = 0.50f;
									}

									this.Student.Alarm = 200.0f;

									if (!this.NoScream)
									{
										this.Student.Giggle = null;
										this.InvestigateScream();
									}

									if (this.FocusOnYandere)
									{
										this.Student.FocusOnYandere = true;
									}

									if (this.Hesitation != 1)
									{
										this.Student.Hesitation = this.Hesitation;
									}
								}
							}
						}
						//The code that governs a response to a radio.
						else
						{
							Debug.Log ("A student just heard a radio...");

							if (this.Student.Giggle != null)
							{
								this.Student.StopInvestigating();
							}

							if (!this.Student.Nemesis && this.Student.Alive && !this.Student.Dying && !this.Student.Guarding &&
								!this.Student.Alarmed && !this.Student.Wet && !this.Student.Slave && !this.Student.Headache &&
								!this.Student.WitnessedMurder && !this.Student.WitnessedCorpse && !this.Student.Lethal &&
								!this.Student.InEvent && !this.Student.Following && !this.Student.Distracting &&
								this.Student.Actions[this.Student.Phase] != StudentActionType.Teaching &&
								this.Student.Actions[this.Student.Phase] != StudentActionType.SitAndTakeNotes &&
								!this.Student.GoAway && this.Student.Routine && !this.Student.CheckingNote &&
								!this.Student.SentHome && this.Student.Persona != PersonaType.Protective)
							{
								if (this.Student.CharacterAnimation != null)
								{
									if (this.SourceRadio.Victim == null)
									{
                                        if (Student.StudentManager.LockerRoomArea.bounds.Contains(transform.position) ||
                                            Student.StudentManager.WestBathroomArea.bounds.Contains(transform.position) ||
                                            Student.StudentManager.EastBathroomArea.bounds.Contains(transform.position) ||
                                            Student.Club != ClubType.Delinquent && Student.StudentManager.IncineratorArea.bounds.Contains(transform.position) ||
                                            Student.StudentManager.HeadmasterArea.bounds.Contains(transform.position))
                                        {
                                            this.Student.Yandere.NotificationManager.CustomText = this.Student.Name + " ignored a radio.";

                                            if (this.Student.Yandere.NotificationManager.CustomText != this.Student.Yandere.NotificationManager.PreviousText)
                                            {
                                                this.Student.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
                                            }
                                        }
                                        else
                                        {
                                            Debug.Log(this.Student.Name + " has just been alarmed by a radio!");

										    this.Student.CharacterAnimation.CrossFade(this.Student.LeanAnim);
										    this.Student.Pathfinding.canSearch = false;
										    this.Student.Pathfinding.canMove = false;
										    this.Student.EatingSnack = false;
										    this.Student.Radio = SourceRadio;
										    this.Student.TurnOffRadio = true;
										    this.Student.Routine = false;
										    this.Student.GoAway = false;

										    bool Eating = false;

										    if (this.Student.Bento.activeInHierarchy && this.Student.StudentID > 1)
										    {
											    Eating = true;
										    }

										    this.Student.EmptyHands();

										    if (Eating)
										    {
											    GenericBentoScript Bento = this.Student.Bento.GetComponent<GenericBentoScript>();

											    Bento.enabled = true;
											    Bento.Prompt.enabled = true;

											    this.Student.Bento.SetActive(true);

											    this.Student.Bento.transform.parent = this.Student.transform;

											    if (this.Student.Male)
											    {
												    this.Student.Bento.transform.localPosition = new Vector3(0, 0.4266666f, -.075f);
											    }
											    else
											    {
												    this.Student.Bento.transform.localPosition = new Vector3(0, 0.461f, -.075f);
											    }

											    this.Student.Bento.transform.localEulerAngles = new Vector3(0, 0, 0);

											    this.Student.Bento.transform.parent = null;
										    }

										    this.Student.SpeechLines.Stop();
										    this.Student.ChalkDust.Stop();

										    this.Student.CleanTimer = 0;
										    this.Student.RadioTimer = 0;
										    this.Student.ReadPhase = 0;

										    this.SourceRadio.Victim = Student;

										    if (this.Student.StudentID == 97)
										    {
											    //Debug.Log ("It's Kyoshi!");

											    //If we're waiting for Yandere-chan to use a radio to distract a teacher...
											    if (SchemeGlobals.GetSchemeStage(5) == 3)
											    {
												    SchemeGlobals.SetSchemeStage(5, 4);
												    this.Student.Yandere.PauseScreen.Schemes.UpdateInstructions();

												    enabled = false;
											    }
										    }

										    this.Student.Yandere.NotificationManager.CustomText = this.Student.Name + " hears the radio.";
										    this.Student.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
                                        }
                                    }
								}
							}
						}
					}
				}
			}
		}

		this.Student = null;
	}

	void PlayClip(AudioClip clip, Vector3 pos)
	{
		GameObject tempGO = new GameObject("TempAudio");
		tempGO.transform.position = pos;

		AudioSource aSource = tempGO.AddComponent<AudioSource>();
		aSource.clip = clip;
		aSource.Play();

		Destroy(tempGO, clip.length);

		aSource.rolloffMode = AudioRolloffMode.Linear;
		aSource.minDistance = 5.0f;
		aSource.maxDistance = 10.0f;
		aSource.spatialBlend = 1.0f;
		aSource.volume = 0.50f;

		if (this.Student != null)
		{
			this.Student.DeathScream = tempGO;
		}
	}

	void InvestigateScream()
	{
		//Debug.Log (this.Student.Name + " just heard a scream.");

		//if (this.Student.Giggle == null)
		//{
			if (this.Student.Clock.Period == 3 && this.Student.BusyAtLunch)
			{
				StudentIsBusy = true;
			}

		/*
		Debug.Log (this.Student.YandereVisible + "," + this.Student.Alarmed + "," +
		this.Student.Distracted + "," + this.Student.Wet + "," +
		this.Student.Slave + "," + this.Student.WitnessedMurder + "," +
		this.Student.WitnessedCorpse + "," + //!this.Student.Investigating &&
		this.Student.InEvent + "," + this.Student.Following + "," +
		this.Student.Confessing + "," + this.Student.Meeting + "," +
		this.Student.TurnOffRadio + "," + this.Student.Fleeing + "," +
		this.Student.Distracting + "," + this.Student.GoAway + "," +
		this.Student.FocusOnYandere + "," + this.StudentIsBusy + "," +
		this.Student.Actions [this.Student.Phase] + "," +
		this.Student.Routine + "," + this.Student.Headache);
		*/

			if (!this.Student.YandereVisible && !this.Student.Alarmed &&
				!this.Student.Distracted && !this.Student.Wet &&
				!this.Student.Slave && !this.Student.WitnessedMurder &&
				!this.Student.WitnessedCorpse && //!this.Student.Investigating &&
				!this.Student.InEvent && !this.Student.Following &&
				!this.Student.Confessing && !this.Student.Meeting &&
				!this.Student.TurnOffRadio && !this.Student.Fleeing &&
				!this.Student.Distracting && !this.Student.GoAway &&
				!this.Student.FocusOnYandere && !this.StudentIsBusy &&
				this.Student.Actions[this.Student.Phase] != StudentActionType.Teaching &&
				this.Student.Actions[this.Student.Phase] != StudentActionType.SitAndTakeNotes &&
				this.Student.Actions[this.Student.Phase] != StudentActionType.Graffiti &&
				this.Student.Actions[this.Student.Phase] != StudentActionType.Bully &&
				/*this.Student.Routine &&*/ !this.Student.Headache)
			{
				//Debug.Log (this.Student.Name + " should be going to investigate that scream now.");

				this.Student.Character.GetComponent<Animation>().CrossFade(this.Student.IdleAnim);

				GameObject Giggle = Instantiate(this.Student.EmptyGameObject, new Vector3(
					this.transform.position.x,
					this.Student.transform.position.y,
					this.transform.position.z),
					Quaternion.identity);

				this.Student.Giggle = Giggle;

				if (this.Student.Pathfinding != null)
				{
					if (!this.Student.Nemesis)
					{
						this.Student.Pathfinding.canSearch = false;
						this.Student.Pathfinding.canMove = false;
						this.Student.InvestigationPhase = 0;
						this.Student.InvestigationTimer = 0.0f;
						this.Student.Investigating = true;
						this.Student.EatingSnack = false;
						this.Student.SpeechLines.Stop();
						this.Student.ChalkDust.Stop();
						this.Student.DiscCheck = true;
						this.Student.Routine = false;
						this.Student.CleanTimer = 0;
						this.Student.ReadPhase = 0;
						this.Student.StopPairing();

						this.Student.EmptyHands();

						this.Student.HeardScream = true;

						//Debug.Log(this.Student.Name + "'s ''DiskCheck'' was just set to ''true''.");
					}
				}
			}
		//}
	}
}