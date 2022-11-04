using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DiscordVerificationScript : MonoBehaviour
{
	void Update()
	{
		if (Input.GetKeyDown("q"))
		{
			SceneManager.LoadScene(SceneNames.MissionModeScene);
		}
	}
}