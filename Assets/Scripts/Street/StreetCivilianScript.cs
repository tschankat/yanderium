using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class StreetCivilianScript : MonoBehaviour
{
	public CharacterController MyController;

	public Animation MyAnimation;

	public AIPath Pathfinding;

	public Transform[] Destinations;

	public float Timer = 0;

	public int ID = 0;

	void Start ()
	{
		Pathfinding.target = Destinations[0];
	}

	void Update ()
	{
		if (Vector3.Distance(transform.position, Destinations[ID].position) < .55f)
		{
			MoveTowardsTarget(Destinations[ID].position);

			MyAnimation.CrossFade("f02_idle_00");

			Pathfinding.canSearch = false;
			Pathfinding.canMove = false;

			Timer += Time.deltaTime;

			if (Timer > 13.5f)
			{
				MyAnimation.CrossFade("f02_newWalk_00");

				ID++;

				if (ID == Destinations.Length)
				{
					ID = 0;
				}

				Pathfinding.target = Destinations[ID];
				Pathfinding.canSearch = true;
				Pathfinding.canMove = true;

				Timer = 0;
			}
		}
	}

	public void MoveTowardsTarget(Vector3 target)
	{
		Vector3 diff = target - transform.position;
		float distanceSquared = diff.sqrMagnitude;
		const float stoppingRangeSquared = 1.0e-6f;

		if (distanceSquared > stoppingRangeSquared)
		{
			MyController.Move(diff * ((Time.deltaTime * 1.0f) / Time.timeScale));
		}

		transform.rotation = Quaternion.Slerp(
			transform.rotation, Destinations[ID].rotation, 10.0f * Time.deltaTime);
	}
}