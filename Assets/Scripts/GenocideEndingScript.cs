using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenocideEndingScript : MonoBehaviour
{
    public AudioSource MyAudio;

    public UISprite Darkness;

    public UILabel Subtitle;

    public Animation Senpai;

    public Transform Neck;

    public AudioClip[] SpeechClip;

    public string[] SpeechText;

    public float[] SpeechDelay;
    public float[] SpeechTime;

    public int SpeechPhase;

    public float Alpha;
    public float Delay;
    public float Timer;

    void Start()
    {
        Senpai["kidnapTorture_01"].speed = .9f;
        GameGlobals.DarkEnding = true;
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown("="))
        {
            Time.timeScale++;
            MyAudio.pitch = Time.timeScale;
        }

        if (Input.GetKeyDown("-"))
        {
            Time.timeScale--;
            MyAudio.pitch = Time.timeScale;
        }

        if (SpeechPhase > 9)
        {
            transform.Translate(Vector3.forward * -.1f * Time.deltaTime);

            if (MyAudio.isPlaying)
            {
                Senpai.Play();

                if (MyAudio.time < 7)
                {
                    Alpha = Mathf.MoveTowards(Alpha, 0, Time.deltaTime * .25f);
                }
                else
                {
                    Alpha = Mathf.MoveTowards(Alpha, 1, Time.deltaTime * .25f);
                }
            }

            Darkness.color = new Color(0, 0, 0, Alpha);
        }

        if (!MyAudio.isPlaying || Input.GetButtonDown(InputNames.Xbox_A))
        {
            if (Input.GetButtonDown(InputNames.Xbox_A))
            {
                Timer = 1;
            }

            Timer += Time.deltaTime;

            if (Timer > Delay)
            {
                SpeechPhase++;
                Timer = 0;

                if (SpeechPhase < SpeechClip.Length)
                {
                    Subtitle.text = SpeechText[SpeechPhase];
                    MyAudio.clip = SpeechClip[SpeechPhase];
                    Delay = SpeechDelay[SpeechPhase];
                    MyAudio.Play();
                }
                else
                {
                    SceneManager.LoadScene(SceneNames.CreditsScene);
                }
            }
        }
    }

    private void LateUpdate()
    {
        Neck.transform.localEulerAngles = new Vector3(
            0,
            Neck.transform.localEulerAngles.y,
            Neck.transform.localEulerAngles.z);
    }
}