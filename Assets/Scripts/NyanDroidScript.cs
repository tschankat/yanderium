using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NyanDroidScript : MonoBehaviour
{
	public Animation Character;
	public PromptScript Prompt;
	public AIPath Pathfinding;

	public Vector3 OriginalPosition;

	public string Prefix;

	public float Timer;

	void Start()
	{
		OriginalPosition = transform.position;
	}

	void Update ()
	{
		if (!Pathfinding.canSearch)
		{
			if (Prompt.Circle[0].fillAmount == 0)
			{
				Prompt.Label[0].text = "     " + "Stop";
				Prompt.Circle[0].fillAmount = 1;

				Pathfinding.canSearch = true;
				Pathfinding.canMove = true;
			}
		}
		else
		{
			Timer += Time.deltaTime;

			if (Timer > 1)
			{
				Timer = 0;

				transform.position += new Vector3(0, 0.0001f, 0);

				if (transform.position.y < 0)
				{
					transform.position = new Vector3 (
						transform.position.x,
						0.001f,
						transform.position.z);
				}

				Physics.SyncTransforms();
			}

			if (Input.GetButtonDown("RB"))
			{
				transform.position = OriginalPosition;
			}

			if (Vector3.Distance(transform.position, Pathfinding.target.position) <= 1)
			{
				Character.CrossFade(Prefix + "_Idle");
				Pathfinding.speed = 0;
			}
			else if (Vector3.Distance(transform.position, Pathfinding.target.position) <= 2)
			{
				Character.CrossFade(Prefix + "_Walk");
				Pathfinding.speed = .5f;
			}
			else
			{
				Character.CrossFade(Prefix + "_Run");
				Pathfinding.speed = 5f;
			}

			if (Prompt.Circle[0].fillAmount == 0)
			{
				Prompt.Label[0].text = "     " + "Follow";
				Prompt.Circle[0].fillAmount = 1;

				Character.CrossFade(Prefix + "_Idle");

				Pathfinding.canSearch = false;
				Pathfinding.canMove = false;
			}
		}
	}
}
