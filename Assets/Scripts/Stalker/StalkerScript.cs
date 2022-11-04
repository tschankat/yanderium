using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerScript : MonoBehaviour
{
    public StalkerYandereScript Yandere;

    public AudioClip CrunchSound;

    public Animation MyAnimation;

    public AudioSource Jukebox;
    public AudioSource MyAudio;

    public AudioClip Crunch;

    public UILabel Subtitle;

    public AudioClip[] AlarmedClip;
    public string[] AlarmedText;
    public float[] AlarmedTime;

    public AudioClip[] SpeechClip;
    public string[] SpeechText;
    public float[] SpeechTime;

    public Collider[] Boundary;

    public float MinimumDistance;
    public float Distance;
    public float Scale;
    public float Timer;

    public bool Alarmed;
    public bool Started;

    public int SpeechPhase;
    public int Limit;

    void Update()
    {
        Distance = Vector3.Distance(Yandere.transform.position, transform.position);

        //////////////////////////
        ///// ALARM CRITERIA /////
        //////////////////////////

        if (!Alarmed)
        {
            for (int ID = 0; ID < this.Boundary.Length; ID++)
            {
                Collider boundary = this.Boundary[ID];

                if (boundary.bounds.Contains(Yandere.transform.position))
                {
                    AudioSource.PlayClipAtPoint(CrunchSound, Camera.main.transform.position);
                    TriggerAlarm();
                }
            }

            if (Distance < .5f)
            {
                TriggerAlarm();
            }
        }
        else
        {
            transform.LookAt(Yandere.transform.position);
        }

        /////////////////////
        ///// SUBTITLES /////
        /////////////////////

        if (Distance < MinimumDistance)
        {
            if (!Started)
            {
                Timer += Time.deltaTime;

                if (Timer > 1)
                {
                    Subtitle.transform.localScale = new Vector3(1, 1, 1);
                    Subtitle.text = SpeechText[0];
                    MyAudio.clip = SpeechClip[0];
                    MyAudio.Play();
                    Started = true;

                    SpeechPhase++;
                }
            }
            else
            {
                MyAudio.pitch = Time.timeScale;

                if (!Alarmed)
                {
                    if (SpeechPhase < SpeechTime.Length)
                    {
                        if (!MyAudio.isPlaying)
                        {
                            MyAudio.clip = SpeechClip[SpeechPhase];
                            MyAudio.Play();

                            Subtitle.text = SpeechText[SpeechPhase];
                            SpeechPhase++;
                        }
                    }
                }
                else
                {
                    if (SpeechPhase < Limit)
                    {
                        if (!MyAudio.isPlaying)
                        {
                            MyAudio.clip = SpeechClip[SpeechPhase];
                            MyAudio.Play();

                            Subtitle.text = SpeechText[SpeechPhase];
                            SpeechPhase++;
                        }
                    }
                }

                if (MyAudio.isPlaying)
                {
                    Jukebox.volume = .1f;
                }
                else
                {
                    Jukebox.volume = 1;
                }
            }
        }
        else
        {
            Subtitle.text = "";
        }
    }

    void TriggerAlarm()
    {
        //AudioSource.PlayClipAtPoint(Crunch, Yandere.MainCamera.transform.position);

        MyAnimation.CrossFade("readyToFight_00");

        SpeechClip = AlarmedClip;
        SpeechText = AlarmedText;
        SpeechTime = AlarmedTime;

        Subtitle.text = "";

        Started = false;
        Alarmed = true;

        SpeechPhase = 0;
        Timer = 0;

        MyAudio.Stop();
    }
}