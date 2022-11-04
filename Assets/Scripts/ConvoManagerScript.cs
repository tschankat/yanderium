using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvoManagerScript : MonoBehaviour
{
	public StudentManagerScript SM;
	public int NearbyStudents;
	public int ID;

	public void CheckMe(int StudentID)
	{
		//Old Rainbow Student Code
		/*
		if (StudentID > 1 && StudentID < 14)
		{
			ID = 2;

			while (ID < 14)
			{
				if (ID != StudentID)
				{
					if (SM.Students[ID] != null)
					{
						if (SM.Students[ID].Routine && Vector3.Distance(SM.Students[ID].transform.position, SM.Students[StudentID].transform.position) < 2.5)
						{
							SM.Students[StudentID].Alone = false;
							break;
						}
						else
						{
							SM.Students[StudentID].Alone = true;
						}
					}
				}

				ID++;
			}
		}
		else
		*/

		//Sakyu Basu
		if (StudentID == 2)
		{
			if (SM.Students[3].Routine && Vector3.Distance(SM.Students[2].transform.position, SM.Students[3].transform.position) < 1.4)
			{
				SM.Students[2].Alone = false;
			}
			else
			{
				SM.Students[2].Alone = true;
			}
		}
		//Inkyu Basu
		else if (StudentID == 3)
		{
			if (SM.Students[2].Routine && Vector3.Distance(SM.Students[3].transform.position, SM.Students[2].transform.position) < 1.4)
			{
				SM.Students[3].Alone = false;
			}
			else
			{
				SM.Students[3].Alone = true;
			}
		}
		//Osana
		else if (StudentID == 11)
		{
			if (SM.Students[10] != null)
			{
				if (SM.Students[10].Routine && Vector3.Distance(SM.Students[11].transform.position, SM.Students[10].transform.position) < 1.1f)
				{
					//SM.Hangouts.List[11].transform.LookAt(SM.Students[10].transform.position);

					SM.Students[11].Alone = false;
				}
				else
				{
					SM.Students[11].Alone = true;
				}
			}
			else
			{
				SM.Students[11].Alone = true;
			}

			/*
			if (SM.Students[11].Talking)
			{
				SM.Students[11].Alone = true;
			}
			*/

			//Debug.Log("ConvoManager has set Osana's Alone boolean to " + SM.Students[11].Alone);
		}
		//Cooking Club
		else if (StudentID > 20 && StudentID < 26)
		{
			//Debug.Log("Checking a member of the Cooking Club.");

			NearbyStudents = 0;

			ID = 21;

			while (ID < 26)
			{
				if (ID != StudentID)
				{
					if (SM.Students[ID] != null)
					{
						if (SM.Students[ID].Routine && Vector3.Distance(SM.Students[ID].transform.position, SM.Students[StudentID].transform.position) < 2.5)
						{
							//Debug.Log("The nearest student to me is: " + SM.Students[ID].Name + "!");

							SM.Students[StudentID].Alone = false;
							break;
						}
						else
						{
							//Debug.Log("Nobody's around.");

							SM.Students[StudentID].Alone = true;
						}
					}
					else
					{
						SM.Students[StudentID].Alone = true;
					}
				}

				ID++;

				if (ID == StudentID)
				{
					//Reached the end of the code without finding a nearby student.
					SM.Students[StudentID].Alone = true;
				}
			}
		}
		//Drama Club
		else if (StudentID > 25 && StudentID < 31)
		{
			ID = 26;

			while (ID < 31)
			{
				if (ID != StudentID)
				{
					if (SM.Students[ID] != null)
					{
						if (SM.Students[ID].Routine && Vector3.Distance(SM.Students[ID].transform.position, SM.Students[StudentID].transform.position) < 2.5)
						{
							SM.Students[StudentID].Alone = false;
							break;
						}
						else
						{
							SM.Students[StudentID].Alone = true;
						}
					}
					else
					{
						SM.Students[StudentID].Alone = true;
					}
				}

				ID++;
			}
		}
		//Gaming Club
		else if (StudentID > 35 && StudentID < 41)
		{
			//Debug.Log("Checking a member of the Cooking Club.");

			NearbyStudents = 0;

			ID = 36;

			while (ID < 41)
			{
				if (ID != StudentID)
				{
					if (SM.Students[ID] != null)
					{
						if (SM.Students[ID].Routine && Vector3.Distance(SM.Students[ID].transform.position, SM.Students[StudentID].transform.position) < 2.5)
						{
							//Debug.Log("The nearest student to me is: " + SM.Students[ID].Name + "!");

							SM.Students[StudentID].Alone = false;
							break;
						}
						else
						{
							//Debug.Log("Nobody's around.");

							SM.Students[StudentID].Alone = true;
						}
					}
					else
					{
						SM.Students[StudentID].Alone = true;
					}
				}

				ID++;

				if (ID == StudentID)
				{
					//Reached the end of the code without finding a nearby student.
					SM.Students[StudentID].Alone = true;
				}
			}
		}
		//Martial Arts Club
		else if (StudentID > 45 && StudentID < 51)
		{
			ID = 46;

			while (ID < 51)
			{
				if (ID != StudentID)
				{
					if (SM.Students[ID] != null)
					{
						if (SM.Students[ID].Routine && Vector3.Distance(SM.Students[ID].transform.position, SM.Students[StudentID].transform.position) < 2.5)
						{
							SM.Students[StudentID].Alone = false;
							break;
						}
						else
						{
							SM.Students[StudentID].Alone = true;
						}
					}
					else
					{
						SM.Students[StudentID].Alone = true;
					}
				}

				ID++;
			}
		}
		//Occult Club
		else if (StudentID > 30 && StudentID < 36)
		{
			ID = 31;

			while (ID < 36)
			{
				if (ID != StudentID)
				{
					if (SM.Students[ID] != null)
					{
						if (SM.Students[ID].Routine && Vector3.Distance(SM.Students[ID].transform.position, SM.Students[StudentID].transform.position) < 2.5)
						{
							SM.Students[StudentID].Alone = false;
							break;
						}
						else
						{
							SM.Students[StudentID].Alone = true;
						}
					}
					else
					{
						SM.Students[StudentID].Alone = true;
					}
				}

				ID++;
			}
		}
		//Osana
		else if (StudentID == 11)
		{
			if (SM.Students[6] != null)
			{
				if (SM.Students[6].Routine && Vector3.Distance(SM.Students[11].transform.position, SM.Students[6].transform.position) < 1.4)
				{
					SM.Students[11].Alone = false;
				}
				else
				{
					SM.Students[11].Alone = true;
				}
			}
			else
			{
				SM.Students[11].Alone = true;
			}
		}
		//Obstacle
		else if (StudentID == 6)
		{
			if (SM.Students[11].Routine && Vector3.Distance(SM.Students[6].transform.position, SM.Students[11].transform.position) < 1.4)
			{
				SM.Students[6].Alone = false;
			}
			else
			{
				SM.Students[6].Alone = true;
			}
		}
		//Artists
		else if (StudentID > 55 && StudentID < 61)
		{
			ID = 56;

			while (ID < 61)
			{
				if (ID != StudentID)
				{
					if (SM.Students[ID] != null)
					{
						if (SM.Students[ID].Routine && Vector3.Distance(SM.Students[ID].transform.position, SM.Students[StudentID].transform.position) < 2.66666f)
						{
							SM.Students[StudentID].Alone = false;
							break;
						}
						else
						{
							SM.Students[StudentID].Alone = true;
						}
					}
					else
					{
						SM.Students[StudentID].Alone = true;
					}
				}

				ID++;
			}
		}
		//Scientists
		else if (StudentID > 60 && StudentID < 66)
		{
			ID = 61;

			while (ID < 66)
			{
				if (ID != StudentID)
				{
					if (SM.Students[ID] != null)
					{
						if (SM.Students[ID].Routine && Vector3.Distance(SM.Students[ID].transform.position, SM.Students[StudentID].transform.position) < 2.66666f)
						{
							SM.Students[StudentID].Alone = false;
							break;
						}
						else
						{
							SM.Students[StudentID].Alone = true;
						}
					}
					else
					{
						SM.Students[StudentID].Alone = true;
					}
				}

				ID++;
			}
		}
		//Atheletes
		else if (StudentID > 65 && StudentID < 71)
		{
			ID = 66;

			while (ID < 71)
			{
				if (ID != StudentID)
				{
					if (SM.Students[ID] != null)
					{
						if (SM.Students[ID].Routine && Vector3.Distance(SM.Students[ID].transform.position, SM.Students[StudentID].transform.position) < 2.66666f)
						{
							SM.Students[StudentID].Alone = false;
							break;
						}
						else
						{
							SM.Students[StudentID].Alone = true;
						}
					}
					else
					{
						SM.Students[StudentID].Alone = true;
					}
				}

				ID++;
			}
		}
		//Delinquents
		else if (StudentID > 75 && StudentID < 81)
		{
			ID = 76;

			//Debug.Log("Checking if we're alone...1");

			while (ID < 81)
			{
				//Debug.Log("Checking if we're alone...2");

				if (ID != StudentID)
				{
					//Debug.Log("Checking if we're alone...3");

					if (SM.Students[ID] != null)
					{
						//Debug.Log("Checking if we're alone...4");

						if (Vector3.Distance(SM.Students[ID].transform.position, SM.Students[StudentID].transform.position) < 2.5)
						{
							SM.Students[StudentID].TrueAlone = false;

							if (SM.Students[ID].Routine)
							{
								SM.Students[StudentID].Alone = false;
								break;
							}
							else
							{
								SM.Students[StudentID].Alone = true;
							}
						}
						else
						{
							SM.Students[StudentID].TrueAlone = true;
							SM.Students[StudentID].Alone = true;
						}
					}
					else
					{
						SM.Students[StudentID].TrueAlone = true;
						SM.Students[StudentID].Alone = true;
					}
				}

				ID++;
			}
		}
		//Bullies
		else if (StudentID > 80 && StudentID < 86)
		{
			ID = 81;

			while (ID < 86)
			{
				if (ID != StudentID)
				{
					if (SM.Students[ID] != null)
					{
						if (SM.Students[ID].Routine && Vector3.Distance(SM.Students[ID].transform.position, SM.Students[StudentID].transform.position) < 2.5)
						{
							SM.Students[StudentID].Alone = false;
							break;
						}
						else
						{
							SM.Students[StudentID].Alone = true;
						}
					}
					else
					{
						SM.Students[StudentID].Alone = true;
					}
				}

				ID++;
			}
		}
	}

	public string[] FemaleCombatAnims;
	public string[] MaleCombatAnims;
	public int CombatAnimID;
	public float CheckTimer;
	public bool Confirmed;
	public int Cycles;

	public void MartialArtsCheck()
	{
		CheckTimer += Time.deltaTime;

		if (CheckTimer > 1 || Confirmed)
		{
			if (SM.Students[47] != null && SM.Students[49] != null)
			{
				if (SM.Students[47].Routine && SM.Students[49].Routine)
				{
					if (SM.Students[47].DistanceToDestination < .1f && SM.Students[49].DistanceToDestination < .1f)
					{
						Confirmed = true;
						CombatAnimID++;

						if (CombatAnimID > 2)
						{
							CombatAnimID = 1;
						}

						SM.Students[47].ClubAnim = MaleCombatAnims[CombatAnimID];
						SM.Students[49].ClubAnim = FemaleCombatAnims[CombatAnimID];

						SM.Students[47].GetNewAnimation = false;
						SM.Students[49].GetNewAnimation = false;

						Cycles++;

						if (Cycles == 5)
						{
							SM.UpdateMartialArts();
							Cycles = 0;
						}
					}
				}
			}
		}
	}

	public void LateUpdate()
	{
		CheckTimer = Mathf.MoveTowards(CheckTimer, 0, Time.deltaTime);

		if (Confirmed == true)
		{
			if (SM.Students[47].Routine && SM.Students[49].Routine)
			{
				if (SM.Students[47].DistanceToPlayer < 1.5f || SM.Students[49].DistanceToPlayer < 1.5f ||
					SM.Students[47].Talking || SM.Students[49].Talking ||
					SM.Students[47].Distracted || SM.Students[49].Distracted ||
					SM.Students[47].TurnOffRadio || SM.Students[49].TurnOffRadio)
				{
					if (SM.Students[47].DistanceToPlayer < 1.5f || SM.Students[49].DistanceToPlayer < 1.5f)
					{
						SM.Students[47].Subtitle.UpdateLabel(SubtitleType.IntrusionReaction, 2, 5.0f);
					}

					SM.Students[47].ClubAnim = AnimNames.MaleIdle20;
					SM.Students[49].ClubAnim = AnimNames.FemaleIdle20;

					Confirmed = false;
				}
			}
			else
			{
				SM.Students[47].ClubAnim = AnimNames.MaleIdle20;
				SM.Students[49].ClubAnim = AnimNames.FemaleIdle20;

				Confirmed = false;
			}
		}
	}
}