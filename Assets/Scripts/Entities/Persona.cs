using System;
using UnityEngine;

public enum PersonaType
{
	None = 0, // @todo: All entities should have a persona. Maybe have a default, like "Passive".
	Loner = 1,
	TeachersPet = 2,
	Heroic = 3,
	Coward = 4,
	Evil = 5,
	SocialButterfly = 6,
	Lovestruck = 7,
	Dangerous = 8,
	Strict = 9,
	PhoneAddict = 10,
	Fragile = 11,
	Spiteful = 12,
	Sleuth = 13,
	Vengeful = 14,
	Protective = 15,
	Violent = 16,
	Nemesis = 99,
}

// [af] This class should manage all data related to an entity's persona.
// Various traits like bravery and camera reaction should be a function of
// the persona type.
[Serializable]
public class Persona
{
	[SerializeField] PersonaType type;

	public Persona(PersonaType type)
	{
		this.type = type;
	}

	public PersonaType Type
	{
		get { return this.type; }
	}

	public static readonly PersonaTypeAndStringDictionary PersonaNames =
		new PersonaTypeAndStringDictionary
		{
			{ PersonaType.None, "None" },
			{ PersonaType.Loner, "Loner" },
			{ PersonaType.TeachersPet, "Teacher's Pet" },
			{ PersonaType.Heroic, "Heroic" },
			{ PersonaType.Coward, "Coward" },
			{ PersonaType.Evil, "Evil" },
			{ PersonaType.SocialButterfly, "Social Butterfly" },
			{ PersonaType.Lovestruck, "Lovestruck" },
			{ PersonaType.Dangerous, "Dangerous" },
			{ PersonaType.Strict, "Strict" },
			{ PersonaType.PhoneAddict, "Phone Addict" },
			{ PersonaType.Fragile, "Fragile" },
			{ PersonaType.Spiteful, "Spiteful" },
			{ PersonaType.Sleuth, "Sleuth" },
			{ PersonaType.Vengeful, "Vengeful" },
			{ PersonaType.Protective, "Protective" },
			{ PersonaType.Violent, "Violent" },
			{ PersonaType.Nemesis, "?????" }
		};

	// @todo: Other traits of each persona...
	// I.e., bravery (scared/fearless), camera reaction, etc.
}