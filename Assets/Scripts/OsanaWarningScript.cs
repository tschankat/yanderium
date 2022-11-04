using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;

public class OsanaWarningScript : MonoBehaviour
{
	public PostProcessingProfile Profile;

	public Transform RightHand;

	public UISprite Darkness;

	public float Alpha = 1;

	public bool FadeOut;

	void Start()
	{
		Darkness.color = new Color(0, 0, 0, 1);

		ColorGradingModel.Settings ColorSettings = this.Profile.colorGrading.settings;
		ColorSettings.basic.saturation = 1;
		ColorSettings.channelMixer.red = new Vector3(1, 0, 0);
		ColorSettings.channelMixer.green = new Vector3(0, 1, 0);
		ColorSettings.channelMixer.blue = new Vector3(0, 0, 1);
		this.Profile.colorGrading.settings = ColorSettings;

		DepthOfFieldModel.Settings DepthSettings = this.Profile.depthOfField.settings;
		DepthSettings.focusDistance = 10;
		DepthSettings.aperture = 11.2f;
		this.Profile.depthOfField.settings = DepthSettings;

		BloomModel.Settings BloomSettings = this.Profile.bloom.settings;
		BloomSettings.bloom.intensity = 1;
		this.Profile.bloom.settings = BloomSettings;
	}

	void Update()
	{
		if (FadeOut)
		{
			Alpha = Mathf.MoveTowards(Alpha, 1, Time.deltaTime);
		}
		else
		{
			Alpha = Mathf.MoveTowards(Alpha, 0, Time.deltaTime);
		}

		Darkness.color = new Color(0, 0, 0, Alpha);

		if (Alpha == 0)
		{
			if (Input.GetButtonDown(InputNames.Xbox_A))
			{
				FadeOut = true;
			}
		}
		else if (Alpha == 1)
		{
			SceneManager.LoadScene(SceneNames.CalendarScene);
		}
	}

	void LateUpdate()
	{
		RightHand.localEulerAngles += new Vector3(
			Random.Range(1.0f, -1.0f),
			Random.Range(1.0f, -1.0f),
			Random.Range(1.0f, -1.0f));
	}
}