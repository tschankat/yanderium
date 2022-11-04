using System;
using UnityEngine;

public enum EntityType
{
	Player,
	Student,
	Teacher
}

public enum GenderType
{
	Male,
	Female
}

public enum DeathType
{
	None, // If still alive.
	Burning,
	Disposed,
	Drowning,
	EasterEgg,
	Electrocution,
	Falling,
	Poison,
	Weapon,
	Mystery,
	Weight
}

// [af] Every entity in the game (i.e., every person) should have an override for
// this class depending on their behavior. When some code wants to interact with
// an entity, they will check the entity type to see what to do.

// [af] I think during student refactoring there should be an EntityScript,
// and then there would be StudentScript, TeacherScript, and PlayerScript which
// derive from that. When gameobjects want to see if something is a entity, they 
// check if it has an EntityScript component, and that should return true if 
// they are any of the derived MonoBehaviours. They can then get the entity type
// from the overridden property.

[Serializable]
public abstract class Entity
{
	[SerializeField] GenderType gender;
	[SerializeField] DeathType deathType;

	public Entity(GenderType gender)
	{
		this.gender = gender;
		this.deathType = DeathType.None;
	}

	public GenderType Gender
	{
		get { return this.gender; }
	}

	public DeathType DeathType
	{
		get { return this.deathType; }
		set { this.deathType = value; }
	}

	public abstract EntityType EntityType { get; }

	// [af] Don't have a Persona property unless the player can have one.
}
