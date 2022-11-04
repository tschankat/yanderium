using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeCyberstalkScript : MonoBehaviour
{
	public HomeDarknessScript HomeDarkness;

	void Update ()
	{
		if (Input.GetButtonDown(InputNames.Xbox_A))
		{
			this.HomeDarkness.Sprite.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
			this.HomeDarkness.Cyberstalking = true;
			this.HomeDarkness.FadeOut = true;
			this.gameObject.SetActive(false);

			int ID = 1;

			for (ID = 1; ID < 26; ID++)
			{
				ConversationGlobals.SetTopicLearnedByStudent(ID, this.HomeDarkness.HomeCamera.HomeInternet.Student, true);
				ConversationGlobals.SetTopicDiscovered(ID, true);
			}
		}

		if (Input.GetButtonDown(InputNames.Xbox_B))
		{
			this.gameObject.SetActive(false);
		}
	}
}