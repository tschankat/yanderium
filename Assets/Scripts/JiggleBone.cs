//	============================================================
//	Name:		Jiggle Bone v.1.0
//	Author: 	Michael Cook (Fishypants)
//	Date:		9-25-2011
//	License:	Free to use. Any credit would be nice :)
//
//  Updated by tinyBuild for Yandere Simulator in Unity 5, 5/25/2017.
//
//	To Use:
// 		Drag this script onto a bone. (ideally bones at the end)
//		Set the boneAxis to be the front facing axis of the bone.
//		Done! Now you have bones with jiggle dynamics.
//
//	============================================================

using UnityEngine;

public class JiggleBone : MonoBehaviour
{
	public bool debugMode = true;

	// Target and dynamic positions.
	//Vector3 targetPos = new Vector3(); // Use local variable instead.
	Vector3 dynamicPos = new Vector3();

	// Bone settings.
	public Vector3 boneAxis = new Vector3(0.0f, 0.0f, 1.0f);
	public float targetDistance = 2.0f;

	// Dynamics settings.
	public float bStiffness = 0.1f;
	public float bMass = 0.9f;
	public float bDamping = 0.75f;
	public float bGravity = 0.75f;

	// Dynamics variables.
	Vector3 force = new Vector3();
	Vector3 acc = new Vector3();
	Vector3 vel = new Vector3();

	// Squash and stretch variables.
	public bool SquashAndStretch = true;
	public float sideStretch = 0.15f;
	public float frontStretch = 0.2f;

	void Awake()
	{
		// Set targetPos and dynamicPos at startup.
		Vector3 targetPos = this.transform.position +
			this.transform.TransformDirection(this.boneAxis * this.targetDistance);

		this.dynamicPos = targetPos;
	}

	void LateUpdate()
	{
		// Reset the bone rotation so we can recalculate the upVector and forwardVector.
		this.transform.rotation = new Quaternion();

		// Update forwardVector and upVector.
		Vector3 forwardVector = this.transform.TransformDirection(this.boneAxis * this.targetDistance);
		Vector3 upVector = this.transform.TransformDirection(new Vector3(0.0f, 1.0f, 0.0f));

		// Calculate target position.
		Vector3 targetPos = this.transform.position +
			this.transform.TransformDirection(this.boneAxis * this.targetDistance);

		// Calculate force, acceleration, and velocity per X, Y and Z.
		this.force.x = (targetPos.x - this.dynamicPos.x) * this.bStiffness;
		this.acc.x = this.force.x / this.bMass;
		this.vel.x += this.acc.x * (1.0f - this.bDamping);

		this.force.y = (targetPos.y - this.dynamicPos.y) * this.bStiffness;
		this.force.y -= this.bGravity / 10.0f; // Add some gravity.
		this.acc.y = this.force.y / this.bMass;
		this.vel.y += this.acc.y * (1.0f - this.bDamping);

		this.force.z = (targetPos.z - this.dynamicPos.z) * this.bStiffness;
		this.acc.z = this.force.z / this.bMass;
		this.vel.z += this.acc.z * (1.0f - this.bDamping);

		// Update dynamic position.
		this.dynamicPos += this.vel + this.force;

		// Set bone rotation to look at dynamicPos.
		this.transform.LookAt(this.dynamicPos, upVector);

		// ==================================================
		// Squash and Stretch section
		// ==================================================
		if (this.SquashAndStretch)
		{
			// Create a vector from target position to dynamic position.
			// We will measure the magnitude of the vector to determine
			// how much squash and stretch we will apply.
			Vector3 dynamicVec = this.dynamicPos - targetPos;

			// Get the magnitude of the vector.
			float stretchMag = dynamicVec.magnitude;

			// Here we determine the amount of squash and stretch based on stretchMag
			// and the direction the Bone Axis is pointed in. Ideally there should be
			// a vector with two values at 0 and one at 1. Like Vector3(0,0,1)
			// for the 0 values, we assume those are the sides, and 1 is the direction
			// the bone is facing.
			float xStretch = 1.0f + (this.boneAxis.x == 0.0f ?
				(-stretchMag * this.sideStretch) : (stretchMag * this.frontStretch));

			float yStretch = 1.0f + (this.boneAxis.y == 0.0f ?
				(-stretchMag * this.sideStretch) : (stretchMag * this.frontStretch));

			float zStretch = 1.0f + (this.boneAxis.z == 0.0f ?
				(-stretchMag * this.sideStretch) : (stretchMag * this.frontStretch));

			// Set the bone scale.
			//this.transform.localScale = new Vector3(xStretch, yStretch, zStretch);
		}

		// ==================================================
		// DEBUG VISUALIZATION
		// ==================================================
		// Green line is the bone's local up vector.
		// Blue line is the bone's local foward vector.
		// Yellow line is the target postion.
		// Red line is the dynamic postion.
		if (this.debugMode)
		{
			Debug.DrawRay(this.transform.position, forwardVector, Color.blue);
			Debug.DrawRay(this.transform.position, upVector, Color.green);
			Debug.DrawRay(targetPos, Vector3.up * 0.20f, Color.yellow);
			Debug.DrawRay(this.dynamicPos, Vector3.up * 0.20f, Color.red);
		}
		// ==================================================
	}
}