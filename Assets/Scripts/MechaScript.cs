using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaScript : MonoBehaviour
{
	public CharacterController MyController;

	public GameObject StudentCrusher;

	public GameObject DestructiveShell;
	public GameObject MechaShell;
	public GameObject ShellType;

	public GameObject[] Sparks;

	public PromptScript Prompt;

	public Transform[] SpawnPoints;

	public Transform[] Wheels;

	public Camera MainCamera;

	public float Speed;
	public float Timer;

	public int ShotsFired = 0;

	public bool Running;
	public bool Fire;

	void Update ()
	{
		if (Prompt.Circle[0].fillAmount == 0)
		{
			Prompt.Yandere.CharacterAnimation.CrossFade("f02_riding_00");
			Prompt.Yandere.enabled = false;
			Prompt.Yandere.Riding = true;
			Prompt.Yandere.Egg = true;

			Prompt.Yandere.Jukebox.Egg = true;
			Prompt.Yandere.Jukebox.KillVolume();
			Prompt.Yandere.Jukebox.Ninja.enabled = true;

			Prompt.Yandere.transform.parent = transform;
			Prompt.Yandere.transform.localPosition = new Vector3(0, 0, 0);
			Prompt.Yandere.transform.localEulerAngles = new Vector3(0, 0, 0);

			Physics.SyncTransforms();

			Prompt.enabled = false;
			Prompt.Hide();

			MainCamera = Prompt.Yandere.MainCamera;

			transform.parent = null;

			StudentCrusher.SetActive(true);

			gameObject.layer = 9;
		}

		if (Prompt.Yandere.Riding)
		{
			if (Prompt.Yandere.transform.localPosition != Vector3.zero)
			{
				transform.position = Prompt.Yandere.transform.position;
				Prompt.Yandere.transform.localPosition = Vector3.zero;

				Physics.SyncTransforms();
			}

			UpdateMovement();

			if (Input.GetButtonDown(InputNames.Xbox_RB))
			{
				Fire = true;
			}

			if (Input.GetButtonDown(InputNames.Xbox_X))
			{
				if (ShellType == MechaShell)
				{
					ShellType = DestructiveShell;

					Sparks[1].SetActive(true);
					Sparks[2].SetActive(true);
					Sparks[3].SetActive(true);
					Sparks[4].SetActive(true);
				}
				else
				{
					ShellType = MechaShell;

					Sparks[1].SetActive(false);
					Sparks[2].SetActive(false);
					Sparks[3].SetActive(false);
					Sparks[4].SetActive(false);
				}
			}

			if (Fire)
			{
				Timer += Time.deltaTime;

				if (ShotsFired < 1)
				{
					if (Timer > 0)
					{
						Instantiate(ShellType, SpawnPoints[1].position, transform.rotation);
						ShotsFired++;
					}
				}
				else if (ShotsFired < 2)
				{
					if (Timer > .1f)
					{
						Instantiate(ShellType, SpawnPoints[2].position, transform.rotation);
						ShotsFired++;
					}
				}
				else if (ShotsFired < 3)
				{
					if (Timer > .2f)
					{
						Instantiate(ShellType, SpawnPoints[3].position, transform.rotation);
						ShotsFired++;
					}
				}
				else if (ShotsFired < 4)
				{
					if (Timer > .3f)
					{
						Instantiate(ShellType, SpawnPoints[4].position, transform.rotation);

						ShotsFired = 0;
						Fire = false;
						Timer = 0;
					}
				}
			}

			if (Input.GetButtonDown(InputNames.Xbox_RS) ||Input.GetButtonDown(InputNames.Xbox_LS))
			{
				Prompt.Yandere.transform.parent = null;

				Prompt.Yandere.enabled = true;
				Prompt.Yandere.CanMove = true;
				Prompt.Yandere.Riding = false;

				Prompt.enabled = true;

				gameObject.layer = 17;
			}
		}
	}

	void UpdateMovement()
	{
		if (!Prompt.Yandere.ToggleRun)
		{
			Running = false;

			if (Input.GetButton(InputNames.Xbox_LB))
			{
				Running = true;
			}
		}
		else
		{
			if (Input.GetButtonDown(InputNames.Xbox_LB))
			{
				Running = !Running;
			}
		}

		MyController.Move(Physics.gravity * Time.deltaTime);

		float v = Input.GetAxis("Vertical");
		float h = Input.GetAxis("Horizontal");

		Vector3 forward = MainCamera.transform.TransformDirection(Vector3.forward);
		forward.y = 0.0f;
		forward = forward.normalized;

		Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);

		Vector3 targetDirection = (h * right) + (v * forward);

		Quaternion targetRotation = Quaternion.identity;

		if (targetDirection != Vector3.zero)
		{
			targetRotation = Quaternion.LookRotation(targetDirection);
		}

		if (targetDirection != Vector3.zero)
		{
			transform.rotation = Quaternion.Lerp(
				transform.rotation, targetRotation, Time.deltaTime);

			Wheels[0].rotation = Quaternion.Lerp(Wheels[0].rotation, targetRotation, Time.deltaTime * 10);
		}
		else
		{
			targetRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
		}

		// If we are getting directional input...
		if (v != 0.0f || h != 0.0f)
		{
			// If the Run button is held down...
			if (Running)
			{
				Speed = Mathf.MoveTowards(Speed, 20, Time.deltaTime * 2);
			}
			// If the Run button is not held down...
			else
			{
				Speed = Mathf.MoveTowards(Speed, 1, Time.deltaTime * 10);
			}
		}
		else
		{
			Speed = Mathf.Lerp(Speed, 0, Time.deltaTime);
		}

		MyController.Move(transform.forward * Speed * Time.deltaTime);

		int ID = 0;

		while (ID < 3)
		{
			Wheels[ID].Rotate(Speed * Time.deltaTime * 360, 0, 0);
			ID++;
		}
	}
}