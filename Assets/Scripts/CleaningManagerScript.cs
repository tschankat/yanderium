using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningManagerScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;

	public Transform[] Windows;
	public Transform[] Desks;
	public Transform[] Floors;
	public Transform[] Toilets;
	public Transform[] Rooftops;

	public Transform[] ClappingSpots;

	public Transform Spot;
	public int Role;

	void Start()
	{
		if (SchoolGlobals.RoofFence)
		{
			int ID = 1;

			while (ID < ClappingSpots.Length)
			{
				ClappingSpots[ID].transform.position = new Vector3(
					ClappingSpots[ID].transform.position.x,
					ClappingSpots[ID].transform.position.y,
					ClappingSpots[ID].transform.position.z + .5f);

				ID++;
			}
		}
	}

	public void GetRole(int StudentID)
	{
		switch (StudentID)
		{
			////////////////////////////////////////////////
			///// Senpai and Osana clean the fountain. /////
			////////////////////////////////////////////////

			case 1:
				this.Role = 4;
				this.Spot = Toilets[0];
			break;

			case 11:
				this.Role = 4;
				this.Spot = Toilets[0];
			break;

			/////////////////////////////////////////////////////
			///// Sakyu and the Cooking Club clean windows. /////
			/////////////////////////////////////////////////////

			case 2:
				this.Role = 1;
				this.Spot = Windows[4];
			break;

			case 21:
				this.Role = 1;
				this.Spot = Windows[6];
			break;

			case 22:
				this.Role = 1;
				this.Spot = Windows[5];
			break;

			case 23:
				this.Role = 1;
				this.Spot = Windows[3];
			break;

			case 24:
				this.Role = 1;
				this.Spot = Windows[2];
			break;

			case 25:
				this.Role = 1;
				this.Spot = Windows[1];
			break;

			/////////////////////////////////////////////////
			///// Inkyu and the Drama Club clean desks. /////
			/////////////////////////////////////////////////

			case 3:
				this.Role = 2;
				this.Spot = Desks[4];
			break;

			case 26:
				this.Role = 2;
				this.Spot = Desks[6];
			break;

			case 27:
				this.Role = 2;
				this.Spot = Desks[5];
			break;

			case 28:
				this.Role = 2;
				this.Spot = Desks[3];
			break;

			case 29:
				this.Role = 2;
				this.Spot = Desks[2];
			break;

			case 30:
				this.Role = 2;
				this.Spot = Desks[1];
			break;

			////////////////////////////////////////////////////////////////
			///// Kuu Dere and the Occult Club scrub classroom floors. /////
			////////////////////////////////////////////////////////////////

			case 4:
				this.Role = 3;
				this.Spot = Floors[4];
			break;

			case 31:
				this.Role = 3;
				this.Spot = Floors[6];
			break;

			case 32:
				this.Role = 3;
				this.Spot = Floors[5];
			break;

			case 33:
				this.Role = 3;
				this.Spot = Floors[3];
			break;

			case 34:
				this.Role = 3;
				this.Spot = Floors[2];
			break;

			case 35:
				this.Role = 3;
				this.Spot = Floors[1];
			break;

			//////////////////////////////////////////////////
			///// The Gaming Club scrubs hallway floors. /////
			//////////////////////////////////////////////////

			case 36:
				this.Role = 3;
				this.Spot = Floors[7];
			break;

			case 37:
				this.Role = 3;
				this.Spot = Floors[8];
			break;

			case 38:
				this.Role = 3;
				this.Spot = Floors[9];
			break;

			case 39:
				this.Role = 3;
				this.Spot = Floors[10];
			break;

			case 40:
				this.Role = 3;
				this.Spot = Floors[11];
			break;

			/////////////////////////////////////////////////
			///// Horuda and the Art Club clap erasers. /////
			/////////////////////////////////////////////////

			case 5:
				this.Role = 5;
				this.Spot = Rooftops[4];
			break;

			case 41:
				this.Role = 5;
				this.Spot = Rooftops[6];
			break;

			case 42:
				this.Role = 5;
				this.Spot = Rooftops[5];
			break;

			case 43:
				this.Role = 5;
				this.Spot = Rooftops[3];
			break;

			case 44:
				this.Role = 5;
				this.Spot = Rooftops[2];
			break;

			case 45:
				this.Role = 5;
				this.Spot = Rooftops[1];
			break;

			///////////////////////////////////////////////////////////////////////////
			///// Male Martial Artists and Male Photographers scrub male toilets. /////
			///////////////////////////////////////////////////////////////////////////

			case 46:
				this.Role = 4;
				this.Spot = Toilets[1];
			break;

			case 47:
				this.Role = 4;
				this.Spot = Toilets[2];
			break;

			case 48:
				this.Role = 4;
				this.Spot = Toilets[3];
			break;

			case 56:
				this.Role = 4;
				this.Spot = Toilets[4];
			break;

			case 57:
				this.Role = 4;
				this.Spot = Toilets[5];
			break;

			case 58:
				this.Role = 4;
				this.Spot = Toilets[6];
			break;

			/////////////////////////////////////////////////
			///// The Music Club scrubs female toilets. /////
			/////////////////////////////////////////////////

			case 51:
				this.Role = 4;
				this.Spot = Toilets[7];
			break;

			case 52:
				this.Role = 4;
				this.Spot = Toilets[8];
			break;

			case 53:
				this.Role = 4;
				this.Spot = Toilets[9];
			break;

			case 54:
				this.Role = 4;
				this.Spot = Toilets[10];
			break;

			case 55:
				this.Role = 4;
				this.Spot = Toilets[11];
			break;

			/////////////////////////////////////////////////////////////
			///// The Suitors scrub the floor in various locations. /////
			/////////////////////////////////////////////////////////////

			case 6:
				this.Role = 3;
				this.Spot = Floors[12];
			break;

			case 7:
				this.Role = 3;
				this.Spot = Floors[13];
			break;

			case 8:
				this.Role = 3;
				this.Spot = Floors[14];
			break;

			case 9:
				this.Role = 3;
				this.Spot = Floors[15];
			break;

			//////////////////////////////////////////////////////////////
			///// Osana's friend cleans the ground where she stands. /////
			//////////////////////////////////////////////////////////////

			case 10:
				if (this.StudentManager.Students[11] != null)
				{
					this.Role = 3;
					this.Spot = this.StudentManager.Students[11].transform;
				}
			break;

			/////////////////////////////////////////////////////////////////////////
			///// Female Martial Artists and Female Photographers scrub floors. /////
			/////////////////////////////////////////////////////////////////////////

			case 49:
				this.Role = 3;
				this.Spot = Floors[16];
			break;

			case 50:
				this.Role = 3;
				this.Spot = Floors[17];
			break;

			case 59:
				this.Role = 3;
				this.Spot = Floors[18];
			break;

			case 60:
				this.Role = 3;
				this.Spot = Floors[19];
			break;

			///////////////////////////////////////////////////////
			///// Scientists scrub floors in storage closets. /////
			///////////////////////////////////////////////////////

			case 61:
				this.Role = 3;
				this.Spot = Floors[20];
			break;

			case 62:
				this.Role = 3;
				this.Spot = Floors[21];
			break;

			case 63:
				this.Role = 3;
				this.Spot = Floors[22];
			break;

			case 64:
				this.Role = 3;
				this.Spot = Floors[23];
			break;

			case 65:
				this.Role = 3;
				this.Spot = Floors[24];
			break;

			//////////////////////////////////////////////////////////
			///// The Sports Club scrubs around the school pool. /////
			//////////////////////////////////////////////////////////

			case 66:
				this.Role = 3;
				this.Spot = Floors[25];
			break;

			case 67:
				this.Role = 3;
				this.Spot = Floors[26];
			break;

			case 68:
				this.Role = 3;
				this.Spot = Floors[27];
			break;

			case 69:
				this.Role = 3;
				this.Spot = Floors[28];
			break;

			case 70:
				this.Role = 3;
				this.Spot = Floors[29];
			break;

			////////////////////////////////////////////////////////////////////
			///// The Gardening Club scrubs the ground around flower pots. /////
			////////////////////////////////////////////////////////////////////

			case 71:
				this.Role = 3;
				this.Spot = Floors[30];
			break;

			case 72:
				this.Role = 3;
				this.Spot = Floors[31];
			break;

			case 73:
				this.Role = 3;
				this.Spot = Floors[32];
			break;

			case 74:
				this.Role = 3;
				this.Spot = Floors[33];
			break;

			case 75:
				this.Role = 3;
				this.Spot = Floors[34];
			break;
		}
	}
}