using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapStudentScript : MonoBehaviour
{
	public SnappedYandereScript Yandere;
	public Quaternion targetRotation;
	public StudentScript Student;
	public Animation MyAnim;

	public string FearAnim;

	public string[] AttackAnims;

	public bool VoicedConcern;

	public AudioClip[] StudentFear;
	public AudioClip SenpaiFear;

	void Start()
	{
		MyAnim.enabled = false;
		MyAnim[FearAnim].time = Random.Range (0.0f, MyAnim [FearAnim].length);
		MyAnim.enabled = true;
	}

	void Update()
	{
		if (Vector3.Distance(transform.position, Yandere.transform.position) < 1)
		{
			if (Yandere.CanMove)
			{
				//If this is Senpai and Yandere-chan has a knife...
				if (Student.StudentID == 1)
				{
					if (Yandere.Armed && !Yandere.KillingSenpai)
					{
						Yandere.Knife.transform.localEulerAngles = new Vector3(0.0f, 180.0f, 0.0f);

						Yandere.MyAudio.clip = Yandere.EndSNAP;
						Yandere.MyAudio.loop = false;
						Yandere.MyAudio.volume = 1;
						Yandere.MyAudio.pitch = 1;
						Yandere.MyAudio.Play();
						Yandere.Speed = 0;

						Yandere.MyAnim.CrossFade("f02_snapKill_00");
						MyAnim.CrossFade("snapDie_00");

						Yandere.TargetStudent = this;
						Yandere.KillingSenpai = true;
						Yandere.CanMove = false;

						enabled = false;
					}
				}
				//If this is anyone else other than Senpai...
				else
				{
					if (!Yandere.Attacking)
					{
						Yandere.transform.position = transform.position + transform.forward;
						Yandere.transform.LookAt(transform.position);

						Yandere.TargetStudent = this;
						Yandere.Attacking = true;
						Yandere.CanMove = false;

						Yandere.StaticNoise.volume = 0;
						Yandere.Static.Fade = 0;
						Yandere.HurryTimer = 0;

						Yandere.ChooseAttack();

                        Student.Pathfinding.enabled = false;
						enabled = false;
					}
				}
			}
		}
		else if (Vector3.Distance(transform.position, Yandere.transform.position) < 5)
		{
			if (!VoicedConcern)
			{
				if (Yandere.CanMove && !Yandere.SnapVoice.isPlaying)
				{
					if (Student.StudentID == 1)
					{
						Yandere.SnapVoice.clip = SenpaiFear;
						Yandere.SnapVoice.Play();
						Yandere.ListenTimer = 10;
					}
					else
					{
						int PreviousVoiceID = Yandere.VoiceID;

						while (Yandere.VoiceID == PreviousVoiceID)
						{
							Yandere.VoiceID = Random.Range(0, 5);
						}

						Yandere.SnapVoice.clip = StudentFear[Yandere.VoiceID];
						Yandere.SnapVoice.Play();
						Yandere.ListenTimer = 1;
					}

					VoicedConcern = true;
				}
			}

			/*
			if (Student.StudentID == 1)
			{
				Debug.Log ("I'm being told to animate...");
			}
			*/

			MyAnim.Play(FearAnim);
		}
		else
		{
			MyAnim.Play(FearAnim);
		}

		if (!Yandere.Attacking)
		{
			transform.LookAt(new Vector3(
				Yandere.transform.position.x,
				transform.position.y,
				Yandere.transform.position.z));
		}
	}
}