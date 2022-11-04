using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBombScript : MonoBehaviour
{
	public StudentScript[] Students;

	public float Timer;

	public bool Amnesia;
	public bool Stink;

	public int ID;

	void Update()
	{
		Timer += Time.deltaTime;

		if (Timer > 15)
		{
			if (!Stink)
			{
				foreach (StudentScript Student in Students)
				{
					if (Student != null)
					{
						Student.Blind = false;
					}
				}
			}

			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			StudentScript Student = other.gameObject.GetComponent<StudentScript>();

			if (Student != null)
			{
				if (Stink)
				{
					GoAway(Student);
				}
				else
				{
					if (Amnesia)
					{
						if (!Student.Chasing)
						{
							Student.ReturnToNormal();
						}
					}

					Students[ID] = Student;
					Student.Blind = true;
					ID++;
				}
			}
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (Stink)
		{
			if (other.gameObject.layer == 9)
			{
				StudentScript Student = other.gameObject.GetComponent<StudentScript>();

				if (Student != null)
				{
					GoAway(Student);
				}
			}
		}
		else if (Amnesia)
		{
			if (other.gameObject.layer == 9)
			{
				StudentScript Student = other.gameObject.GetComponent<StudentScript>();

				if (Student != null)
				{
					if (Student.Alarmed)
					{
						if (!Student.Chasing)
						{
							Student.ReturnToNormal();
						}
					}
				}
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (!Stink && !Amnesia)
		{
			if (other.gameObject.layer == 9)
			{
				StudentScript Student = other.gameObject.GetComponent<StudentScript>();

				if (Student != null)
				{
					Debug.Log(Student.Name + " left a smoke cloud and stopped being blind.");

					Student.Blind = false;
				}
			}
		}
	}

	void GoAway(StudentScript Student)
	{
		if (!Student.Chasing && !Student.WitnessedMurder && !Student.WitnessedCorpse && !Student.Fleeing)
		{
            if (Student.Following)
            {
                Student.Yandere.Follower = null;
                Student.Yandere.Followers--;

                ParticleSystem.EmissionModule heartsEmission = Student.Hearts.emission;
                heartsEmission.enabled = false;

                Student.FollowCountdown.gameObject.SetActive(false);
                Student.Following = false;
            }

            if (Student.SolvingPuzzle)
            {
                Student.PuzzleTimer = 0;
                Student.DropPuzzle();
            }

            //Student.Blind = true;
			//Student.BecomeAlarmed();
            //Student.Blind = false;

			Student.CurrentDestination = Student.StudentManager.GoAwaySpots.List[Student.StudentID];
			Student.Pathfinding.target = Student.StudentManager.GoAwaySpots.List[Student.StudentID];
			Student.Pathfinding.canSearch = true;
			Student.Pathfinding.canMove = true;

			Student.CharacterAnimation.CrossFade(Student.SprintAnim);
			Student.DistanceToDestination = 100;
			Student.Pathfinding.speed = 4;
			Student.AmnesiaTimer = 10.0f;
			Student.Distracted = true;
			Student.Alarmed = false;
			Student.Routine = false;
			Student.GoAway = true;

            Student.AlarmTimer = 0.0f;
        }
	}
}