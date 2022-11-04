using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeShapeScript : MonoBehaviour
{
	public Transform eyelid_und1_Left;
	public Transform eyelid_und1_Right;

	public Transform eyelid_und2_Left;
	public Transform eyelid_und2_Right;

	public Transform eyelid_und_Left;
	public Transform eyelid_und_Right;

	public Transform eyerid1_Left;
	public Transform eyerid1_Right;

	public Transform eyerid2_Left;
	public Transform eyerid2_Right;

	public Transform eyerid_Left;
	public Transform eyerid_Right;

	public Transform inner_corner_of_eye_Left;
	public Transform inner_corner_of_eye_Reft;

	public Transform tail_of_eye_Left;
	public Transform tail_of_eye_Right;

	public float PosOffsetX;
	public float PosOffsetY;
	public float PosOffsetZ;

	public float RotOffsetX;
	public float RotOffsetY;
	public float RotOffsetZ;

	void Start ()
	{
		PosOffsetX = Random.Range(-0.002f, 0.002f);
		PosOffsetY = Random.Range(-0.002f, 0.002f);
		PosOffsetZ = Random.Range(-0.002f, 0.002f);

		RotOffsetX = Random.Range(-15.0f, 15.0f);
		RotOffsetY = Random.Range(-15.0f, 15.0f);
		RotOffsetZ = Random.Range(-15.0f, 15.0f);
	}

	void LateUpdate ()
	{
		//Positions

		eyelid_und1_Left.localPosition += new Vector3(PosOffsetX, PosOffsetY, PosOffsetZ);
		eyelid_und1_Right.localPosition += new Vector3(PosOffsetX, PosOffsetY, PosOffsetZ * -1);

		eyelid_und2_Left.localPosition += new Vector3(PosOffsetX, PosOffsetY, PosOffsetZ);
		eyelid_und2_Right.localPosition += new Vector3(PosOffsetX, PosOffsetY, PosOffsetZ * -1);

		eyelid_und_Left.localPosition += new Vector3(PosOffsetX, PosOffsetY, PosOffsetZ);
		eyelid_und_Right.localPosition += new Vector3(PosOffsetX, PosOffsetY, PosOffsetZ * -1);

		eyerid1_Left.localPosition += new Vector3(PosOffsetX, PosOffsetY, PosOffsetZ);
		eyerid1_Right.localPosition += new Vector3(PosOffsetX, PosOffsetY, PosOffsetZ * -1);

		eyerid2_Left.localPosition += new Vector3(PosOffsetX, PosOffsetY, PosOffsetZ);
		eyerid2_Right.localPosition += new Vector3(PosOffsetX, PosOffsetY, PosOffsetZ * -1);

		eyerid_Left.localPosition += new Vector3(PosOffsetX, PosOffsetY, PosOffsetZ);
		eyerid_Right.localPosition += new Vector3(PosOffsetX, PosOffsetY, PosOffsetZ * -1);

		inner_corner_of_eye_Left.localPosition += new Vector3(PosOffsetX, PosOffsetY, PosOffsetZ);
		inner_corner_of_eye_Reft.localPosition += new Vector3(PosOffsetX, PosOffsetY, PosOffsetZ * -1);

		tail_of_eye_Left.localPosition += new Vector3(PosOffsetX, PosOffsetY, PosOffsetZ);
		tail_of_eye_Right.localPosition += new Vector3(PosOffsetX, PosOffsetY, PosOffsetZ * -1);

		//Rotations

		eyelid_und1_Left.localEulerAngles += new Vector3(RotOffsetX, RotOffsetY, RotOffsetZ);
		eyelid_und1_Right.localEulerAngles += new Vector3(RotOffsetX * -1, RotOffsetY * -1, RotOffsetZ);

		eyelid_und2_Left.localEulerAngles += new Vector3(RotOffsetX, RotOffsetY, RotOffsetZ);
		eyelid_und2_Right.localEulerAngles += new Vector3(RotOffsetX * -1, RotOffsetY * -1, RotOffsetZ);

		eyelid_und_Left.localEulerAngles += new Vector3(RotOffsetX, RotOffsetY, RotOffsetZ);
		eyelid_und_Right.localEulerAngles += new Vector3(RotOffsetX * -1, RotOffsetY * -1, RotOffsetZ);

		eyerid1_Left.localEulerAngles += new Vector3(RotOffsetX, RotOffsetY, RotOffsetZ);
		eyerid1_Right.localEulerAngles += new Vector3(RotOffsetX * -1, RotOffsetY * -1, RotOffsetZ);

		eyerid2_Left.localEulerAngles += new Vector3(RotOffsetX, RotOffsetY, RotOffsetZ);
		eyerid2_Right.localEulerAngles += new Vector3(RotOffsetX * -1, RotOffsetY * -1, RotOffsetZ);

		eyerid_Left.localEulerAngles += new Vector3(RotOffsetX, RotOffsetY, RotOffsetZ);
		eyerid_Right.localEulerAngles += new Vector3(RotOffsetX * -1, RotOffsetY * -1, RotOffsetZ);

		inner_corner_of_eye_Left.localEulerAngles += new Vector3(RotOffsetX, RotOffsetY, RotOffsetZ);
		inner_corner_of_eye_Reft.localEulerAngles += new Vector3(RotOffsetX * -1, RotOffsetY * -1, RotOffsetZ);

		tail_of_eye_Left.localEulerAngles += new Vector3(RotOffsetX, RotOffsetY, RotOffsetZ);
		tail_of_eye_Right.localEulerAngles += new Vector3(RotOffsetX * -1, RotOffsetY * -1, RotOffsetZ);
	}
}
