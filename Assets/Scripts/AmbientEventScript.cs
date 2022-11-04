using System;
using UnityEngine;

public class AmbientEventScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
    public AmbientEventScript GrudgeReaction;
    public AmbientEventScript PoliceReaction;
    public HidingSpotScript HidingSpot;
    public UILabel EventSubtitle;
	public YandereScript Yandere;
	public ClockScript Clock;

	public StudentScript[] EventStudent;
	public Transform[] EventLocation;
	public AudioClip[] EventClip;
	public string[] EventSpeech;
	public string[] EventAnim;
	public int[] EventSpeaker;

	public GameObject VoiceClip;

    public bool RotateSpine = false;
    public bool Sitting = false;
    public bool EventOn = false;
	public bool Spoken = false;
    public bool Private = false;

    public int EventPhase = 0;

    public float StartTime = 13.001f;
    public float Delay = 0.5f;
    public float Timer = 0.0f;
	public float Scale = 0.0f;

	public int[] StudentID;

	public DayOfWeek EventDay;

	void Start()
	{
        if (Sitting)
        {
            if (DateGlobals.Weekday != this.EventDay)
            {
                this.enabled = false;
            }
            else if (StudentGlobals.GetStudentGrudge(2) || StudentGlobals.GetStudentGrudge(3))
            {
                this.EventClip = this.GrudgeReaction.EventClip;
                this.EventSpeech = this.GrudgeReaction.EventSpeech;
                this.EventSpeaker = this.GrudgeReaction.EventSpeaker;
            }
            else if (GameGlobals.PoliceYesterday)
            {
                this.EventClip = this.PoliceReaction.EventClip;
                this.EventSpeech = this.PoliceReaction.EventSpeech;
                this.EventSpeaker = this.PoliceReaction.EventSpeaker;
            }
        }
        else
        {
            if (DateGlobals.Weekday != this.EventDay)
            {
                this.enabled = false;
            }
        }
	}

	void Update()
	{
		if (!this.EventOn)
		{
			for (int i = 1; i < 3; i++)
			{
				if (this.EventStudent[i] == null)
				{
					this.EventStudent[i] = this.StudentManager.Students[this.StudentID[i]];
				}
				else
				{
					if (!this.EventStudent[i].Alive || this.EventStudent[i].Slave)
					{
						this.enabled = false;
					}
				}
			}

			if (this.Clock.HourTime > StartTime)
			{
				if (this.EventStudent[1] != null && this.EventStudent[2] != null)
				{
                    if (this.EventStudent[1].Indoors && this.EventStudent[2].Indoors)
                    {
                        if (this.EventStudent[1].Pathfinding.canMove &&
						    this.EventStudent[2].Pathfinding.canMove)
					    {
                            if (this.Sitting)
                            {
                                if (this.Yandere.Hiding && this.Yandere.HidingSpot == this.HidingSpot.Spot)
                                {
                                    this.Yandere.PromptBar.ClearButtons();
                                    this.Yandere.PromptBar.Show = false;
                                    this.Yandere.Exiting = true;

                                    this.HidingSpot.Prompt.enabled = false;
                                    this.HidingSpot.Prompt.Hide();
                                }
                            }

                            this.EventStudent[1].CharacterAnimation.CrossFade(this.EventStudent[1].WalkAnim);
						    this.EventStudent[1].CurrentDestination = this.EventLocation[1];
						    this.EventStudent[1].Pathfinding.target = this.EventLocation[1];
						    this.EventStudent[1].InEvent = true;

						    this.EventStudent[2].CharacterAnimation.CrossFade(this.EventStudent[2].WalkAnim);
						    this.EventStudent[2].CurrentDestination = this.EventLocation[2];
						    this.EventStudent[2].Pathfinding.target = this.EventLocation[2];
						    this.EventStudent[2].InEvent = true;

						    this.EventOn = true;
					    }
                    }
                }
			}
		}
		//If the event is on...
		else
		{
			float Distance = Vector3.Distance(this.Yandere.transform.position, EventLocation[1].parent.position);

			if ((this.Clock.HourTime > StartTime + .5f) ||
				this.EventStudent[1].WitnessedCorpse ||
				this.EventStudent[2].WitnessedCorpse ||
				this.EventStudent[1].Alarmed ||
				this.EventStudent[2].Alarmed ||
				this.EventStudent[1].Dying ||
				this.EventStudent[2].Dying ||
                this.Yandere.Noticed)
			{
				this.EndEvent();
			}
			else
			{
				for (int i = 1; i < 3; i++)
				{
					if (!this.EventStudent[i].Pathfinding.canMove && !this.EventStudent[i].Private)
					{
						this.EventStudent[i].Character.GetComponent<Animation>().CrossFade(this.EventStudent[i].IdleAnim);
						this.EventStudent[i].Private = true;

						this.StudentManager.UpdateStudents();
					}
				}
					
				if (!this.EventStudent[1].Pathfinding.canMove && !this.EventStudent[2].Pathfinding.canMove)
				{
                    if (this.Sitting)
                    {
                        this.EventStudent[1].CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
                        this.EventStudent[1].CharacterAnimation[this.EventStudent[1].SocialSitAnim].layer = 99;
                        this.EventStudent[1].CharacterAnimation.Play(this.EventStudent[1].SocialSitAnim);
                        this.EventStudent[1].CharacterAnimation[this.EventStudent[1].SocialSitAnim].weight = 1.0f;

                        this.EventStudent[2].CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
                        this.EventStudent[2].CharacterAnimation[this.EventStudent[2].SocialSitAnim].layer = 99;
                        this.EventStudent[2].CharacterAnimation.Play(this.EventStudent[2].SocialSitAnim);
                        this.EventStudent[2].CharacterAnimation[this.EventStudent[2].SocialSitAnim].weight = 1.0f;

                        this.EventStudent[1].MyController.radius = 0;
                        this.EventStudent[2].MyController.radius = 0;

                        //Debug.Log("Setting Radius to 0");

                        this.RotateSpine = true;
                    }

					if (!this.Spoken)
					{
                        if (this.Sitting)
                        {
                            this.EventStudent[this.EventSpeaker[1]].CharacterAnimation.CrossFade("f02_benchSit_00");
                            this.EventStudent[this.EventSpeaker[2]].CharacterAnimation.CrossFade("f02_benchSit_00");
                        }
                        else
                        {
                            this.EventStudent[this.EventSpeaker[1]].CharacterAnimation.CrossFade(this.EventStudent[1].IdleAnim);
						    this.EventStudent[this.EventSpeaker[2]].CharacterAnimation.CrossFade(this.EventStudent[2].IdleAnim);
                        }

                        this.EventStudent[this.EventSpeaker[this.EventPhase]].PickRandomAnim();
						this.EventStudent[this.EventSpeaker[this.EventPhase]].CharacterAnimation.CrossFade(this.EventStudent[this.EventSpeaker[this.EventPhase]].RandomAnim);

						if (!Sitting && StartTime < 16 && DateGlobals.Weekday == DayOfWeek.Monday && this.EventPhase == 13)
						{
							this.EventStudent[this.EventSpeaker[this.EventPhase]].CharacterAnimation.CrossFade(AnimNames.MaleJojoPose);
						}

						AudioClipPlayer.Play(this.EventClip[this.EventPhase],
							this.EventStudent[this.EventSpeaker[this.EventPhase]].transform.position + (Vector3.up * 1.50f),
							5.0f, 10.0f, out this.VoiceClip, this.Yandere.transform.position.y);

						this.Spoken = true;
					}
					else
					{
						int Speaker = this.EventSpeaker[this.EventPhase];

						if (this.EventStudent[Speaker].CharacterAnimation[this.EventStudent[Speaker].RandomAnim].time >=
							this.EventStudent[Speaker].CharacterAnimation[this.EventStudent[Speaker].RandomAnim].length)
						{
							this.EventStudent[Speaker].PickRandomAnim();
							this.EventStudent[Speaker].CharacterAnimation.CrossFade(this.EventStudent[Speaker].RandomAnim);
						}

						this.Timer += Time.deltaTime;

						if (this.Yandere.transform.position.y > EventLocation[1].parent.position.y - 1 &&
							this.Yandere.transform.position.y < EventLocation[1].parent.position.y + 1)
						{
							if (this.VoiceClip != null)
							{
								this.VoiceClip.GetComponent<AudioSource>().volume = 1;
							}
								
							if (Distance < 10.0f)
							{
								if (this.Timer > this.EventClip[this.EventPhase].length)
								{
									this.EventSubtitle.text = string.Empty;
								}
								else
								{
									this.EventSubtitle.text = this.EventSpeech[this.EventPhase];
								}

								this.Scale = Mathf.Abs((Distance - 10.0f) * 0.20f);

								if (this.Scale < 0.0f)
								{
									this.Scale = 0.0f;
								}

								if (this.Scale > 1.0f)
								{
									this.Scale = 1.0f;
								}

								this.EventSubtitle.transform.localScale =
									new Vector3(this.Scale, this.Scale, this.Scale);

                                //Debug.Log("This script is setting Scale to " + Scale);
							}
							else
							{
                                //Debug.Log("This script is setting scale to 0.");

                                this.EventSubtitle.transform.localScale = Vector3.zero;
								this.EventSubtitle.text = string.Empty;
							}
						}
						else
						{
							if (this.VoiceClip != null)
							{
								this.VoiceClip.GetComponent<AudioSource>().volume = 0;
							}
						}

						/*
						Animation studentCharAnim = this.EventStudent[this.EventSpeaker[this.EventPhase]].Character.GetComponent<Animation>();

						if (studentCharAnim[this.EventAnim[this.EventPhase]].time >=
							studentCharAnim[this.EventAnim[this.EventPhase]].length)
						{
							studentCharAnim.CrossFade(this.EventStudent[this.EventSpeaker[this.EventPhase]].IdleAnim);
						}
						*/

						if (this.Timer > (this.EventClip[this.EventPhase].length + Delay))
						{
							this.Spoken = false;
							this.EventPhase++;
							this.Timer = 0.0f;

							if (this.EventPhase == this.EventSpeech.Length)
							{
								this.EndEvent();
							}
						}
					}

                    if (Private)
                    {
                        if (Distance < 5)
                        {
                            this.Yandere.Eavesdropping = true;
                        }
                        else
                        {
                            this.Yandere.Eavesdropping = false;
                        }
                    }
                }
			}
		}
	}

    public float MouthTimer;
    public float MouthTarget;
    public float MouthExtent;

    public float TimerLimit = .1f;
    public float TalkSpeed;

    public float LipStrength;

    void LateUpdate()
    {
        if (this.RotateSpine)
        {
            this.EventStudent[1].Head.transform.localEulerAngles += new Vector3(0, 15, 0);
            this.EventStudent[1].Neck.transform.localEulerAngles += new Vector3(0, 15, 0);
            this.EventStudent[1].Spine.transform.localEulerAngles += new Vector3(0, 15, 0);
            this.EventStudent[1].LeftEye.transform.localEulerAngles += new Vector3(0, 5, 0);
            this.EventStudent[1].RightEye.transform.localEulerAngles += new Vector3(0, 5, 0);

            this.EventStudent[2].Head.transform.localEulerAngles += new Vector3(0, -15, 0);
            this.EventStudent[2].Neck.transform.localEulerAngles += new Vector3(0, -15, 0);
            this.EventStudent[2].Spine.transform.localEulerAngles += new Vector3(0, -15, 0);
            this.EventStudent[2].LeftEye.transform.localEulerAngles += new Vector3(0, -5, 0);
            this.EventStudent[2].RightEye.transform.localEulerAngles += new Vector3(0, -5, 0);

            this.MouthTimer += Time.deltaTime;

            if (this.MouthTimer > this.TimerLimit)
            {
                this.MouthTarget = UnityEngine.Random.Range(40.0f, 40.0f + this.MouthExtent);
                this.MouthTimer = 0.0f;
            }

            Transform Jaw = EventStudent[this.EventSpeaker[this.EventPhase]].Jaw;
            Transform LipL = EventStudent[this.EventSpeaker[this.EventPhase]].LipL;
            Transform LipR = EventStudent[this.EventSpeaker[this.EventPhase]].LipR;

            Jaw.localEulerAngles = new Vector3(
                Jaw.localEulerAngles.x,
                Jaw.localEulerAngles.y,
                Mathf.Lerp(Jaw.localEulerAngles.z, this.MouthTarget, Time.deltaTime * this.TalkSpeed));

            LipL.localPosition = new Vector3(
                LipL.localPosition.x,
                Mathf.Lerp(LipL.localPosition.y, 0.02632812f + (this.MouthTarget * this.LipStrength), Time.deltaTime * this.TalkSpeed),
                LipL.localPosition.z);

            LipR.localPosition = new Vector3(
                LipR.localPosition.x,
                Mathf.Lerp(LipR.localPosition.y, 0.02632812f + (this.MouthTarget * this.LipStrength), Time.deltaTime * this.TalkSpeed),
                LipR.localPosition.z);
        }
    }

    public void EndEvent()
	{
        Debug.Log("An Ambient Event named " + this.gameObject.name + " has ended.");

		if (this.VoiceClip != null)
		{
			Destroy(this.VoiceClip);
		}

		for (int i = 1; i < 3; i++)
		{
            this.EventStudent[i].CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
            this.EventStudent[i].CharacterAnimation.Stop(this.EventStudent[i].SocialSitAnim);

            this.EventStudent[1].MyController.radius = .1f;

            if (this.EventStudent[i].Meeting &&
                this.EventStudent[i].Clock.HourTime > this.EventStudent[i].MeetTime)
            {
                this.EventStudent[i].CurrentDestination = this.EventStudent[i].MeetSpot;
                this.EventStudent[i].Pathfinding.target = this.EventStudent[i].MeetSpot;

                this.EventStudent[i].DistanceToDestination = Vector3.Distance(
                    this.transform.position, this.EventStudent[i].CurrentDestination.position);

                this.EventStudent[i].Pathfinding.canSearch = true;
                this.EventStudent[i].Pathfinding.canMove = true;
                this.EventStudent[i].Pathfinding.speed = 4.0f;

                this.EventStudent[i].SpeechLines.Stop();

                this.EventStudent[i].EmptyHands();

                this.EventStudent[i].Meeting = true;
                this.EventStudent[i].MeetTime = 0.0f;
            }
            else
            {
                this.EventStudent[i].CurrentDestination =
				    this.EventStudent[i].Destinations[this.EventStudent[i].Phase];

			    this.EventStudent[i].Pathfinding.target =
				    this.EventStudent[i].Destinations[this.EventStudent[i].Phase];
            }

            this.EventStudent[i].InEvent = false;
			this.EventStudent[i].Private = false;
		}

		if (!this.StudentManager.Stop)
		{
			this.StudentManager.UpdateStudents();
		}

        if (this.HidingSpot != null)
        {
            this.HidingSpot.Prompt.enabled = true;
        }

        this.EventSubtitle.text = string.Empty;
        this.Yandere.Eavesdropping = false;
        this.enabled = false;
	}
}
