using UnityEngine;

public enum BodyPartType
{
	Head,
	Torso,
	LeftArm,
	RightArm,
	LeftLeg,
	RightLeg
}

public class BodyPartScript : MonoBehaviour
{
	public bool Sacrifice = false;
	public int StudentID = 0;
	public int Type = 0;
}
