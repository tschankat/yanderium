using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamilyVoiceScript : MonoBehaviour
{
    public StalkerYandereScript Yandere;

    public DetectionMarkerScript Marker;

    public AudioClip GameOverSound;
    public AudioClip GameOverLine;
    public AudioClip CrunchSound;

    public GameObject Heartbroken;

    public Animation MyAnimation;

    public Transform YandereHead;
    public Transform Head;

    public AudioSource Jukebox;
    public AudioSource MyAudio;

    public Renderer Darkness;

    public UILabel Subtitle;

    public AudioClip[] SpeechClip;
    public Transform[] Boundary;
    public string[] SpeechText;
    public float[] SpeechTime;

    public string GameOverText;

    public float MinimumDistance;
    public float NoticeSpeed;
    public float Distance;
    public float Alpha;
    public float Scale;
    public float Timer;

    public int GameOverPhase;
    public int SpeechPhase;
    public int AnimPhase;

    public bool MultiClip;
    public bool GameOver;
    public bool Started;

    void Start()
    {
        Subtitle.transform.localScale = new Vector3(0, 0, 0);
    }

    void Update()
    {
        if (!GameOver)
        {
            if (Yandere.transform.position.y > transform.position.y - 1 &&
                Yandere.transform.position.y < transform.position.y + 1)
            {
                Distance = Vector3.Distance(Yandere.transform.position, transform.position);

                if (Distance < MinimumDistance)
                {
                    if (!Started)
                    {
                        Subtitle.text = SpeechText[0];
                        MyAudio.Play();
                        Started = true;
                    }
                    else
                    {
                        MyAudio.pitch = Time.timeScale;

                        if (MultiClip)
                        {
                            Debug.Log("Anim Time is: " + MyAnimation["fatherFixing_00"].time);

                            if (MyAnimation["fatherFixing_00"].time > MyAnimation["fatherFixing_00"].length)
                            {
                                MyAnimation["fatherFixing_00"].time = MyAnimation["fatherFixing_00"].time - MyAnimation["fatherFixing_00"].length;
                            }

                            if (AnimPhase == 0)
                            {
                                if (MyAnimation["fatherFixing_00"].time > 18 && MyAnimation["fatherFixing_00"].time < 18.1f)
                                {
                                    Subtitle.text = SpeechText[SpeechPhase];
                                    MyAudio.clip = SpeechClip[SpeechPhase];
                                    MyAudio.Play();
                                    SpeechPhase++;

                                    AnimPhase = 1;
                                }
                            }
                            else
                            {
                                if (MyAnimation["fatherFixing_00"].time < 1)
                                {
                                    Subtitle.text = SpeechText[SpeechPhase];
                                    MyAudio.clip = SpeechClip[SpeechPhase];
                                    MyAudio.Play();
                                    SpeechPhase++;

                                    AnimPhase = 0;
                                }
                            }
                        }
                        else if (SpeechPhase < SpeechTime.Length)
                        {
                            if (MyAudio.time > SpeechTime[SpeechPhase])
                            {
                                Subtitle.text = SpeechText[SpeechPhase];
                                SpeechPhase++;
                            }
                        }

                        /////////////////////////
                        ///// SUBTITLE SIZE /////
                        /////////////////////////

                        Scale = Mathf.Abs(1.0f - ((Distance - 1) / (MinimumDistance - 1)));

                        if (Scale < 0.0f)
                        {
                            Scale = 0.0f;
                        }

                        if (Scale > 1.0f)
                        {
                            Scale = 1.0f;
                        }

                        Jukebox.volume = 1 - (.9f * Scale);

                        Subtitle.transform.localScale = new Vector3(Scale, Scale, Scale);

                        MyAudio.volume = Scale;
                    }

                    //////////////////////////////
                    ///// GAME OVER CRITERIA /////
                    //////////////////////////////

                    for (int ID = 0; ID < this.Boundary.Length; ID++)
                    {
                        Transform boundary = this.Boundary[ID];

                        if (boundary != null)
                        {
                            float BoundaryDistance = Vector3.Distance(Yandere.transform.position, boundary.position);

                            //Debug.Log(this.gameObject.name + "'s BoundaryDistance is: " + BoundaryDistance);

                            if (BoundaryDistance < .33333f)
                            {
                                Debug.Log("Got a ''proximity'' game over from " + this.gameObject.name);

                                AudioSource.PlayClipAtPoint(CrunchSound, Camera.main.transform.position);
                                TransitionToGameOver();
                            }
                        }
                    }

                    if (YandereIsInFOV())
                    {
                        if (YandereIsInLOS())
                        {
                            Alpha = Mathf.MoveTowards(Alpha, 1, Time.deltaTime * NoticeSpeed);
                        }
                        else
                        {
                            Alpha = Mathf.MoveTowards(Alpha, 0, Time.deltaTime * NoticeSpeed);
                        }
                    }
                    else
                    {
                        Alpha = Mathf.MoveTowards(Alpha, 0, Time.deltaTime * NoticeSpeed);
                    }

                    if (Alpha == 1)
                    {
                        Debug.Log("Got a ''witnessed'' game over from " + this.gameObject.name);

                        AudioSource.PlayClipAtPoint(GameOverSound, Camera.main.transform.position);
                        TransitionToGameOver();
                    }
                }
                else
                {
                    if (Distance < MinimumDistance + 1)
                    {
                        Jukebox.volume = 1;
                        MyAudio.volume = 0;
                        Subtitle.transform.localScale = new Vector3(0, 0, 0);
                    }
                }

                Marker.Tex.transform.localScale = new Vector3(1, Alpha, 1);
                Marker.Tex.color = new Color(1, 0, 0, Alpha);
            }
        }
        else
        {
            if (GameOverPhase == 0)
            {
                Timer += Time.deltaTime;

                if (Timer > 1)
                {
                    if (!MyAudio.isPlaying)
                    {
                        Debug.Log("Should be updating the subtitle with the Game Over text.");

                        Subtitle.transform.localScale = new Vector3(1, 1, 1);
                        Subtitle.text = "" + GameOverText;
                        MyAudio.clip = GameOverLine;
                        MyAudio.Play();

                        GameOverPhase++;
                    }
                }
            }
            else
            {
                if (!MyAudio.isPlaying || Input.GetButton(InputNames.Xbox_A))
                {
                    Heartbroken.SetActive(true);
                    Subtitle.text = "";
                    enabled = false;
                    MyAudio.Stop();
                }
            }
        }
    }

    bool YandereIsInFOV()
    {
        Vector3 diff = Yandere.transform.position - Head.position;
        float maxAngle = 90;

        return Vector3.Angle(Head.forward, diff) <= maxAngle;
    }

    bool YandereIsInLOS()
    {
        Debug.DrawLine(Head.position, new Vector3(Yandere.transform.position.x, YandereHead.position.y, Yandere.transform.position.z), Color.red);

        RaycastHit hit;
        bool hitExists = Physics.Linecast(Head.position, new Vector3(Yandere.transform.position.x, YandereHead.position.y, Yandere.transform.position.z), hitInfo: out hit);

        if (hitExists)
        {
            //Debug.Log(gameObject.name + " shot out a raycast that hit ''" + hit.collider.gameObject.name + "''");

            if (hit.collider.gameObject.layer == 13)
            {
                //This character can see Yandere-chan.
                return true;
            }
        }

        //This character can't see Yandere-chan.
        return false;
    }

    void TransitionToGameOver()
    {
        Marker.Tex.transform.localScale = new Vector3(1, 0, 1);
        Marker.Tex.color = new Color(1, 0, 0, 0);
        Darkness.material.color = new Color(0, 0, 0, 1);
        Yandere.RPGCamera.enabled = false;
        Yandere.CanMove = false;
        Subtitle.text = "";
        GameOver = true;
        Jukebox.Stop();
        MyAudio.Stop();
        Alpha = 0;
    }
}