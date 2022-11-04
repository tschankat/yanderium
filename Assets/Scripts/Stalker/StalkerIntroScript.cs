using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.PostProcessing;

public class StalkerIntroScript : MonoBehaviour
{
	public PostProcessingProfile Profile;

	public StalkerYandereScript Yandere;

	public RPG_Camera RPGCamera;

	public Transform CameraFocus;
	public Transform Moon;

	public Renderer Darkness;

	public float Alpha;
	public float Speed;
	public float Timer;
	public float DOF;

	public int Phase;

	public GameObject[] Neighborhood;

	void Start()
	{
		RenderSettings.ambientIntensity = 8;

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		transform.position = new Vector3(12.5f, 5, 13);
		transform.LookAt(Moon);

		CameraFocus.parent = transform;
		CameraFocus.localPosition = new Vector3(0, 0, 100);
		CameraFocus.parent = null;

		UpdateDOF(3);
		DOF = 4;

		Alpha = 1;
	}

	void Update()
	{
		Moon.LookAt(transform);

		if (Phase == 0)
		{
			if (Input.GetKeyDown("space"))
			{
				Timer = 2;
				Alpha = 0;
			}

			Alpha = Mathf.MoveTowards(Alpha, 0, Time.deltaTime * .5f);

			Darkness.material.color = new Color(0, 0, 0, Alpha);

			if (Alpha == 0)
			{
				Timer += Time.deltaTime;

				if (Timer > 2)
				{
					Phase++;
				}
			}
		}
		else if (Phase == 1)
		{
			if (Speed == 0)
			{
				Yandere.MyAnimation.Play();
			}

			if (Input.GetKeyDown("space"))
			{
				if (Yandere.MyAnimation["f02_jumpOverWall_00"].time < 12)
				{
					Yandere.MyAnimation["f02_jumpOverWall_00"].time = 12;
					Speed = 100;
				}
			}

			Speed += Time.deltaTime;

			transform.position = Vector3.Lerp(transform.position,
				new Vector3(11.5f, 1, 13), Time.deltaTime * Speed);

			CameraFocus.position = Vector3.Lerp(CameraFocus.position,
				new Vector3(13.62132f, 1, 15.12132f), Time.deltaTime * Speed);

			DOF = Mathf.Lerp(DOF, 2, Time.deltaTime * Speed);

			UpdateDOF(DOF);

			transform.LookAt(CameraFocus);

			if (Yandere.MyAnimation["f02_jumpOverWall_00"].time > 13)
			{
				Yandere.transform.position = new Vector3(13.15f, 0, 13);

				transform.position = new Vector3 (12.75f, 1.3f, 12.4f);
				transform.eulerAngles = new Vector3 (0, 45, 0);

				UpdateDOF(.5f);
                DOF = .5f;

				Speed = -1;
				Phase++;
			}
		}
		else if (Phase == 2)
		{
			if (Input.GetKeyDown("space"))
			{
				Speed = 100;
			}

			Speed += Time.deltaTime;

			if (Speed > 0)
			{
				transform.position = Vector3.Lerp(transform.position, new Vector3(13.15f, 1.51515f, 14.92272f), Time.deltaTime * Speed);
				transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(15, 180, 0), Time.deltaTime * Speed * 2);

				DOF = Mathf.MoveTowards(DOF, 2, Time.deltaTime * Speed);
				UpdateDOF(DOF);

				if (Speed > 4)
				{
                    DOF = 2;
                    UpdateDOF(DOF);

                    RPGCamera.enabled = true;
					Yandere.enabled = true;
                    Phase++;
				}
			}
		}
	}

	void UpdateDOF(float Value)
	{
		DepthOfFieldModel.Settings DepthSettings = Profile.depthOfField.settings;
		DepthSettings.focusDistance = Value;
		DepthSettings.aperture = 5.6f;
		Profile.depthOfField.settings = DepthSettings;
	}
}