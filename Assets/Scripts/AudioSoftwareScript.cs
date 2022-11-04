using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSoftwareScript : MonoBehaviour
{
	public YandereScript Yandere;
	public PromptScript Prompt;

	public Quaternion targetRotation;
	public Collider ChairCollider;
	public UILabel EventSubtitle;
	public GameObject Screen;
	public Transform SitSpot;

	public bool ConversationRecorded;
	public bool AudioDoctored;
	public bool Editing;

	public float Timer;

	void Start ()
	{
		Screen.SetActive(false);
	}

	void Update ()
	{
		if (ConversationRecorded && Yandere.Inventory.RivalPhone)
		{
			if (!Prompt.enabled)
			{
				Prompt.enabled = true;
			}
		}
		else
		{
			if (Prompt.enabled)
			{
				Prompt.enabled = false;
			}
		}
			
		if (Prompt.Circle[0].fillAmount == 0)
		{
			Yandere.Character.GetComponent<Animation>().CrossFade(AnimNames.FemalePlayingGames00);
			Yandere.MyController.radius = 0.10f;
			Yandere.CanMove = false;

			GetComponent<AudioSource>().Play();
			ChairCollider.enabled = false;
			Screen.SetActive(true);
			Editing = true;
		}

		if (Editing)
		{
			this.targetRotation = Quaternion.LookRotation(new Vector3(
				this.Screen.transform.position.x,
				this.Yandere.transform.position.y,
				this.Screen.transform.position.z) - this.Yandere.transform.position);
			this.Yandere.transform.rotation = Quaternion.Slerp(
				this.Yandere.transform.rotation, this.targetRotation, Time.deltaTime * 10.0f);

			this.Yandere.MoveTowardsTarget(SitSpot.position);

			this.Timer += Time.deltaTime;

			if (this.Timer > 1)
			{
				this.EventSubtitle.text = "Okay, how 'bout that boy from class 3-2? What do you think of him?";
			}

			if (this.Timer > 7)
			{
				this.EventSubtitle.text = "He's just my childhood friend.";
			}

			if (this.Timer > 9)
			{
				this.EventSubtitle.text = "Is he your boyfriend?";
			}

			if (this.Timer > 11)
			{
				this.EventSubtitle.text = "What? HIM? Ugh, no way! That guy's a total creep! I wouldn't date him if he was the last man alive on earth! He can go jump off a cliff for all I care!";
			}

			if (this.Timer > 22)
			{
				this.Yandere.MyController.radius = 0.20f;
				this.Yandere.CanMove = true;

				this.ChairCollider.enabled = false;
				this.EventSubtitle.text = "";
				this.Screen.SetActive(false);
				this.AudioDoctored = true;
				this.Editing = false;

				this.Prompt.enabled = false;
				this.Prompt.Hide();

				this.enabled = false;
			}
		}
	}
}
