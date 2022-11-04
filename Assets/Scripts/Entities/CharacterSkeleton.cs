using System;
using UnityEngine;

// [af] Various gender-neutral transforms on humanoid bodies, to be assigned in the editor.
[Serializable]
public class CharacterSkeleton
{
	[SerializeField] Transform head;
	[SerializeField] Transform neck;
	[SerializeField] Transform chest;
	[SerializeField] Transform stomach;
	[SerializeField] Transform pelvis;
	[SerializeField] Transform rightShoulder;
	[SerializeField] Transform leftShoulder;
	[SerializeField] Transform rightUpperArm;
	[SerializeField] Transform leftUpperArm;
	[SerializeField] Transform rightElbow;
	[SerializeField] Transform leftElbow;
	[SerializeField] Transform rightLowerArm;
	[SerializeField] Transform leftLowerArm;
	[SerializeField] Transform rightPalm;
	[SerializeField] Transform leftPalm;
	[SerializeField] Transform rightUpperLeg;
	[SerializeField] Transform leftUpperLeg;
	[SerializeField] Transform rightKnee;
	[SerializeField] Transform leftKnee;
	[SerializeField] Transform rightLowerLeg;
	[SerializeField] Transform leftLowerLeg;
	[SerializeField] Transform rightFoot;
	[SerializeField] Transform leftFoot;

	public Transform Head { get { return this.head; } }
	public Transform Neck { get { return this.neck; } }
	public Transform Chest { get { return this.chest; } }
	public Transform Stomach { get { return this.stomach; } }
	public Transform Pelvis { get { return this.pelvis; } }
	public Transform RightShoulder { get { return this.rightShoulder; } }
	public Transform LeftShoulder { get { return this.leftShoulder; } }
	public Transform RightUpperArm { get { return this.rightUpperArm; } }
	public Transform LeftUpperArm { get { return this.leftUpperArm; } }
	public Transform RightElbow { get { return this.rightElbow; } }
	public Transform LeftElbow { get { return this.leftElbow; } }
	public Transform RightLowerArm { get { return this.rightLowerArm; } }
	public Transform LeftLowerArm { get { return this.leftLowerArm; } }
	public Transform RightPalm { get { return this.rightPalm; } }
	public Transform LeftPalm { get { return this.leftPalm; } }
	public Transform RightUpperLeg { get { return this.rightUpperLeg; } }
	public Transform LeftUpperLeg { get { return this.leftUpperLeg; } }
	public Transform RightKnee { get { return this.rightKnee; } }
	public Transform LeftKnee { get { return this.leftKnee; } }
	public Transform RightLowerLeg { get { return this.rightLowerLeg; } }
	public Transform LeftLowerLeg { get { return this.leftLowerLeg; } }
	public Transform RightFoot { get { return this.rightFoot; } }
	public Transform LeftFoot { get { return this.leftFoot; } }
}
