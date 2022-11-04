using System;
using UnityEngine;

public enum ClubType
{
	None = 0,
	Cooking = 1,
	Drama = 2,
	Occult = 3,
	Art = 4,
	LightMusic = 5,
	MartialArts = 6,
	Photography = 7,
	Science = 8,
	Sports = 9,
	Gardening = 10,
	Gaming = 11,
	Council = 12,
	Bully = 13,
	Delinquent = 14,

	// [af] Eventually, "?????" (nemesis), "Teacher", and "Gym Teacher" should be moved
	// into an "OccupationType" enumeration, because they're not technically clubs.
	Nemesis = 99,
	Teacher = 100,
	GymTeacher = 101,
	Nurse = 102,
	Counselor = 103,
	Headmaster = 104,
}

[Serializable]
public class Club
{
	[SerializeField] ClubType type;

	public Club(ClubType type)
	{
		this.type = type;
	}

	public ClubType Type
	{
		get { return this.type; }
		set { this.type = value; }
	}

	public static readonly ClubTypeAndStringDictionary ClubNames =
		new ClubTypeAndStringDictionary
		{
			{ ClubType.None, "No Club" },
			{ ClubType.Cooking, "Cooking" },
			{ ClubType.Drama, "Drama" },
			{ ClubType.Occult, "Occult" },
			{ ClubType.Art, "Art" },
			{ ClubType.LightMusic, "Light Music" },
			{ ClubType.MartialArts, "Martial Arts" },
			{ ClubType.Photography, "Photography" },
			{ ClubType.Science, "Science" },
			{ ClubType.Sports, "Sports" },
			{ ClubType.Gardening, "Gardening" },
			{ ClubType.Gaming, "Gaming" },
			{ ClubType.Council, "Student Council" },
			{ ClubType.Delinquent, "Delinquent" },
			{ ClubType.Bully, "No Club" },
			{ ClubType.Nemesis, "?????" },
		};

	// [af] Intended for teachers. An OccupationType enum should be created eventually.
	public static readonly IntAndStringDictionary TeacherClubNames =
		new IntAndStringDictionary
		{
			{ 0, "Gym Teacher" },
			{ 1, "School Nurse" },
			{ 2, "Guidance Counselor" },
			{ 3, "Headmaster" },
			{ 4, "?????" },
			{ 11, "Teacher of Class 1-1" },
			{ 12, "Teacher of Class 1-2" },
			{ 21, "Teacher of Class 2-1" },
			{ 22, "Teacher of Class 2-2" },
			{ 31, "Teacher of Class 3-1" },
			{ 32, "Teacher of Class 3-2" }
		};

	// Other constant data about clubs... etc..
}
