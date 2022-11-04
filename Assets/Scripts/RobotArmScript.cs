using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotArmScript : MonoBehaviour
{
	public SkinnedMeshRenderer RobotArms;
	public AudioSource MyAudio;
	public PromptScript Prompt;

	public Transform TerminalTarget;

	public ParticleSystem[] Sparks;

	public AudioClip ArmsOff;
	public AudioClip ArmsOn;

	public float StartWorkTimer;
	public float StopWorkTimer;

	public float[] ArmValue;
	public float[] Timer;

	public bool UpdateArms;
	public bool Work;

	public bool[] On;

	public int ID;

	void Update()
	{
		if (Prompt.Circle[0].fillAmount == 0)
		{
			ActivateArms();
		}

		if (Prompt.Circle[1].fillAmount == 0)
		{
			ToggleWork();
		}

		if (UpdateArms)
		{
			if (On[0])
			{
				ArmValue[0] = Mathf.Lerp(ArmValue[0], 0, Time.deltaTime * 5);

				RobotArms.SetBlendShapeWeight(0, ArmValue[0]);

				if (ArmValue[0] < .1f)
				{
					RobotArms.SetBlendShapeWeight(0, 0);
					UpdateArms = false;
					ArmValue[0] = 0;
				}
			}
			else
			{
				ArmValue[0] = Mathf.Lerp(ArmValue[0], 100, Time.deltaTime * 5);

				RobotArms.SetBlendShapeWeight(0, ArmValue[0]);

				if (ArmValue[0] > 99.9f)
				{
					RobotArms.SetBlendShapeWeight(0, 100);
					UpdateArms = false;
					ArmValue[0] = 100;
				}
			}
		}

		///////////////////
		///// WORKING /////
		///////////////////

		if (Work)
		{
			if (StartWorkTimer > 0)
			{
				ID = 1;

				while (ID < 9)
				{
					ArmValue[ID] = Mathf.Lerp(ArmValue[ID], 100, Time.deltaTime * 5);
					RobotArms.SetBlendShapeWeight(ID, ArmValue[ID]);

					ID += 2;
				}

				StartWorkTimer -= Time.deltaTime;

				if (StartWorkTimer < 0)
				{
					ID = 1;

					while (ID < 9)
					{
						RobotArms.SetBlendShapeWeight(ID, 100);
						ID += 2;
					}
				}
			}
			else
			{
				ID = 1;

				while (ID < 9)
				{
					Timer[ID] -= Time.deltaTime;

					if (Timer[ID] < 0)
					{
						Sparks[ID].Stop();
						Sparks[ID + 1].Stop();

						int RandomNumber = Random.Range(0, 2);

						if (RandomNumber == 1)
						{
							On[ID] = true;
						}
						else
						{
							On[ID] = false;
						}

						Timer[ID] = Random.Range(1.0f, 2.0f);
					}

					if (On[ID])
					{
						ArmValue[ID] = Mathf.Lerp(ArmValue[ID], 0, Time.deltaTime * 5);
						ArmValue[ID + 1] = Mathf.Lerp(ArmValue[ID + 1], 100, Time.deltaTime * 5);

						RobotArms.SetBlendShapeWeight(ID, ArmValue[ID]);
						RobotArms.SetBlendShapeWeight(ID + 1, ArmValue[ID + 1]);

						if (ArmValue[ID] < 1f)
						{
							Sparks[ID].Play();

							RobotArms.SetBlendShapeWeight(ID, 0);
							RobotArms.SetBlendShapeWeight(ID + 1, 100);
							ArmValue[ID] = 0;
							ArmValue[ID + 1] = 100;
						}
					}
					else
					{
						ArmValue[ID] = Mathf.Lerp(ArmValue[ID], 100, Time.deltaTime * 5);
						ArmValue[ID + 1] = Mathf.Lerp(ArmValue[ID + 1], 0, Time.deltaTime * 5);

						RobotArms.SetBlendShapeWeight(ID, ArmValue[ID]);
						RobotArms.SetBlendShapeWeight(ID + 1, ArmValue[ID + 1]);

						if (ArmValue[ID] > 99f)
						{
							Sparks[ID + 1].Play();

							RobotArms.SetBlendShapeWeight(ID, 100);
							RobotArms.SetBlendShapeWeight(ID + 1, 0);
							ArmValue[ID] = 100;
							ArmValue[ID + 1] = 0;
						}
					}

					ID += 2;
				}
			}
		}
		else
		{
			if (StopWorkTimer > 0)
			{
				ID = 1;

				while (ID < 9)
				{
					ArmValue[ID] = Mathf.Lerp(ArmValue[ID], 0, Time.deltaTime * 5);
					RobotArms.SetBlendShapeWeight(ID, ArmValue[ID]);
					Sparks[ID].Stop();

					ID++;
				}

				StopWorkTimer -= Time.deltaTime;

				if (StopWorkTimer < 0)
				{
					ID = 1;

					while (ID < 9)
					{
						RobotArms.SetBlendShapeWeight(ID, 0);
						On[ID] = false;
						ID++;
					}
				}
			}
		}
	}

	public void ActivateArms()
	{
		Prompt.Circle[0].fillAmount = 1;
		UpdateArms = true;
		On[0] = !On[0];

		if (On[0])
		{
			Prompt.HideButton[1] = false;
			MyAudio.clip = ArmsOn;
		}
		else
		{
			Prompt.HideButton[1] = true;
			MyAudio.clip = ArmsOff;
			StopWorkTimer = 5;
			Work = false;
		}

		MyAudio.Play();
	}

	public void ToggleWork()
	{
		Prompt.Circle[1].fillAmount = 1;
		StartWorkTimer = 1;
		StopWorkTimer = 5;
		Work = !Work;
	}
}