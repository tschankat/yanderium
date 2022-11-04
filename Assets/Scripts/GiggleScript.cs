using UnityEngine;

public class GiggleScript : MonoBehaviour
{
	public GameObject EmptyGameObject;
	public GameObject Giggle;

	public StudentScript Student;

	public bool StudentIsBusy = false;
	public bool Distracted = false;

	public int Frame = 0;

	void Start()
	{
		float localScaleX = 500.0f * (2.0f - SchoolGlobals.SchoolAtmosphere);
		this.transform.localScale = new Vector3(
			localScaleX, this.transform.localScale.y, localScaleX);
	}

	void Update()
	{
		if (this.Frame > 0)
		{
			Destroy(this.gameObject);
		}

		this.Frame++;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			if (!this.Distracted)
			{
				this.Student = other.gameObject.GetComponent<StudentScript>();

				if (this.Student != null)
				{
					if (this.Student.Giggle == null)
					{
						if (Student.StudentManager.LockerRoomArea.bounds.Contains(transform.position) ||
							Student.StudentManager.WestBathroomArea.bounds.Contains(transform.position) ||
							Student.StudentManager.EastBathroomArea.bounds.Contains(transform.position) ||
							Student.Club != ClubType.Delinquent && Student.StudentManager.IncineratorArea.bounds.Contains(transform.position) ||
							Student.StudentManager.HeadmasterArea.bounds.Contains(transform.position))
						{
							this.Student.Yandere.NotificationManager.CustomText = this.Student.Name + " ignored a giggle.";
							this.Student.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
						}
						else
						{
							if (this.Student.Clock.Period == 3 && this.Student.BusyAtLunch)
							{
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

							if (this.Student.StudentID == this.Student.StudentManager.RivalID || this.Student.StudentID == 1)
							{
								if (this.Student.CurrentAction == StudentActionType.SitAndEatBento)
								{
									//StudentIsBusy = true;
								}
							}

							if (!this.Student.YandereVisible && !this.Student.Alarmed &&
								!this.Student.Distracted && !this.Student.Wet &&
								!this.Student.Slave && !this.Student.WitnessedMurder &&
								!this.Student.WitnessedCorpse && !this.Student.Investigating &&
								!this.Student.InEvent && !this.Student.Following &&
								!this.Student.Confessing && !this.Student.Meeting &&
								!this.Student.TurnOffRadio && !this.Student.Fleeing &&
								!this.Student.Distracting && !this.Student.GoAway &&
								!this.Student.FocusOnYandere && !this.StudentIsBusy &&
								this.Student.Actions[this.Student.Phase] != StudentActionType.Teaching &&
								this.Student.Actions[this.Student.Phase] != StudentActionType.SitAndTakeNotes &&
								this.Student.Actions[this.Student.Phase] != StudentActionType.Graffiti &&
								this.Student.Actions[this.Student.Phase] != StudentActionType.Bully &&
								this.Student.Routine && !this.Student.Headache &&
								this.Student.Persona != PersonaType.Protective &&
								!this.Student.MyBento.Tampered)
							{
								this.Student.CharacterAnimation.CrossFade(this.Student.IdleAnim);

								this.Giggle = Instantiate(this.EmptyGameObject, new Vector3(
									this.transform.position.x,
									this.Student.transform.position.y,
									this.transform.position.z),
									Quaternion.identity);

								this.Student.Giggle = this.Giggle;

								if (this.Student.Pathfinding != null)
								{
									if (!this.Student.Nemesis)
									{
										if (this.Student.Drownable)
										{
											this.Student.Drownable = false;
											this.Student.StudentManager.UpdateMe(this.Student.StudentID);
										}

										if (this.Student.ChalkDust.isPlaying)
										{
											this.Student.ChalkDust.Stop();
											this.Student.Pushable = false;
											this.Student.StudentManager.UpdateMe(this.Student.StudentID);
										}

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

										if (this.Student.SunbathePhase > 2)
										{
											this.Student.SunbathePhase = 2;
										}

										if (this.Student.Persona != PersonaType.PhoneAddict && !this.Student.Sleuthing)
										{
											this.Student.SmartPhone.SetActive(false);
										}
										else
										{
                                            if (!this.Student.Phoneless)
                                            {
    											this.Student.SmartPhone.SetActive(true);
                                            }
                                        }

										this.Student.OccultBook.SetActive(false);
										this.Student.Pen.SetActive(false);

										if (!this.Student.Male)
										{
											this.Student.Cigarette.SetActive(false);
											this.Student.Lighter.SetActive(false);
										}

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

										this.Student.Yandere.NotificationManager.CustomText = this.Student.Name + " heard a giggle.";
										this.Student.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
									}
								}

								this.Distracted = true;
							}
						}
					}
				}
			}
		}
	}
}