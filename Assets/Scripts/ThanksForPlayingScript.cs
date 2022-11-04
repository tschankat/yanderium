using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class ThanksForPlayingScript : MonoBehaviour
{
	public UIPanel ThankYouPanel;
	public UIPanel FinalGamePanel;
	public UIPanel RivalPanel;
	public UIPanel QualityPanel;
	public UIPanel WeaponsPanel;
	public UIPanel StoryPanel;
	public UIPanel MorePanel;
	public UIPanel CrowdfundPanel;

	public Transform Yandere;

	public UISprite Darkness;

	public Animation YandereKun;
	public Animation Ryoba;

	public bool FadeOut;

	public float Alpha;

	void Start ()
	{
		Ryoba["f02_faceCouncilGrace_00"].layer = 1;
		Ryoba.Play("f02_faceCouncilGrace_00");

		YandereKun["AltYanKunFace"].layer = 1;
		YandereKun.Play("AltYanKunFace");

		Darkness.color = new Color (0, 0, 0, 1);
		Alpha = 1;
	}

	void Update ()
	{
		if (!FadeOut)
		{
			Alpha = Mathf.MoveTowards (Alpha, 0, Time.deltaTime * .5f);
			Darkness.color = new Color (0, 0, 0, Alpha);
		}
		else
		{
			Alpha = Mathf.MoveTowards (Alpha, 1, Time.deltaTime * .5f);
			Darkness.color = new Color (1, 1, 1, Alpha);

			if (Alpha == 1)
			{
				SceneManager.LoadScene(SceneNames.TitleScene);
			}
		}
			
		if (Yandere.position.z > 1 && Yandere.position.z < 10)
		{
			ThankYouPanel.alpha = Mathf.MoveTowards (ThankYouPanel.alpha, 1, Time.deltaTime * .5f);
		}
		else
		{
			ThankYouPanel.alpha = Mathf.MoveTowards (ThankYouPanel.alpha, 0, Time.deltaTime * .5f);
		}

		if (Yandere.position.z > 20 && Yandere.position.z < 120)
		{
			FinalGamePanel.alpha = Mathf.MoveTowards (FinalGamePanel.alpha, 1, Time.deltaTime * .5f);
		}
		else
		{
			FinalGamePanel.alpha = Mathf.MoveTowards (FinalGamePanel.alpha, 0, Time.deltaTime * .5f);
		}

		if (Yandere.position.z > 30 && Yandere.position.z < 40)
		{
			RivalPanel.alpha = Mathf.MoveTowards (RivalPanel.alpha, 1, Time.deltaTime * .5f);
		}
		else
		{
			RivalPanel.alpha = Mathf.MoveTowards (RivalPanel.alpha, 0, Time.deltaTime * .5f);
		}

		if (Yandere.position.z > 50 && Yandere.position.z < 60)
		{
			QualityPanel.alpha = Mathf.MoveTowards (QualityPanel.alpha, 1, Time.deltaTime * .5f);
		}
		else
		{
			QualityPanel.alpha = Mathf.MoveTowards (QualityPanel.alpha, 0, Time.deltaTime * .5f);
		}

		if (Yandere.position.z > 70 && Yandere.position.z < 80)
		{
			WeaponsPanel.alpha = Mathf.MoveTowards (WeaponsPanel.alpha, 1, Time.deltaTime * .5f);
		}
		else
		{
			WeaponsPanel.alpha = Mathf.MoveTowards (WeaponsPanel.alpha, 0, Time.deltaTime * .5f);
		}

		if (Yandere.position.z > 90 && Yandere.position.z < 100)
		{
			StoryPanel.alpha = Mathf.MoveTowards (StoryPanel.alpha, 1, Time.deltaTime * .5f);
		}
		else
		{
			StoryPanel.alpha = Mathf.MoveTowards (StoryPanel.alpha, 0, Time.deltaTime * .5f);
		}

		if (Yandere.position.z > 110 && Yandere.position.z < 120)
		{
			MorePanel.alpha = Mathf.MoveTowards (MorePanel.alpha, 1, Time.deltaTime * .5f);
		}
		else
		{
			MorePanel.alpha = Mathf.MoveTowards (MorePanel.alpha, 0, Time.deltaTime * .5f);
		}

		if (Yandere.position.z > 130 && Yandere.position.z < 140)
		{
			CrowdfundPanel.alpha = Mathf.MoveTowards (CrowdfundPanel.alpha, 1, Time.deltaTime * .5f);

			if (Input.GetButtonDown(InputNames.Xbox_A))
			{
				FadeOut = true;
			}
		}
		else
		{
			CrowdfundPanel.alpha = Mathf.MoveTowards (CrowdfundPanel.alpha, 0, Time.deltaTime * .5f);
		}
	}
}