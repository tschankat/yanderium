using UnityEngine;

public class BoneSetsScript : MonoBehaviour
{
	public Transform[] BoneSet1;
	public Transform[] BoneSet2;
	public Transform[] BoneSet3;
	public Transform[] BoneSet4;
	public Transform[] BoneSet5;
	public Transform[] BoneSet6;
	public Transform[] BoneSet7;
	public Transform[] BoneSet8;
	public Transform[] BoneSet9;
	public Vector3[] BoneSet1Pos;
	public Vector3[] BoneSet2Pos;
	public Vector3[] BoneSet3Pos;
	public Vector3[] BoneSet4Pos;
	public Vector3[] BoneSet5Pos;
	public Vector3[] BoneSet6Pos;
	public Vector3[] BoneSet7Pos;
	public Vector3[] BoneSet8Pos;
	public Vector3[] BoneSet9Pos;
	public float Timer = 0.0f;
	public Transform RightArm;
	public Transform LeftArm;
	public Transform RightLeg;
	public Transform LeftLeg;
	public Transform Head;
	public Vector3 RightArmPosition;
	public Vector3 RightArmRotation;
	public Vector3 LeftArmPosition;
	public Vector3 LeftArmRotation;
	public Vector3 RightLegPosition;
	public Vector3 RightLegRotation;
	public Vector3 LeftLegPosition;
	public Vector3 LeftLegRotation;
	public Vector3 HeadPosition;

	void Start()
	{
		// Do nothing.
		/*
		ID = 1; while (ID < BoneSet1.length){BoneSet1Pos[ID] = BoneSet1[ID].localPosition;ID++;}
		ID = 1; while (ID < BoneSet2.length){BoneSet2Pos[ID] = BoneSet2[ID].localPosition;ID++;}
		ID = 1; while (ID < BoneSet3.length){BoneSet3Pos[ID] = BoneSet3[ID].localPosition;ID++;}
		ID = 1; while (ID < BoneSet4.length){BoneSet4Pos[ID] = BoneSet4[ID].localPosition;ID++;}
		ID = 1; while (ID < BoneSet5.length){BoneSet5Pos[ID] = BoneSet5[ID].localPosition;ID++;}
		ID = 1; while (ID < BoneSet6.length){BoneSet6Pos[ID] = BoneSet6[ID].localPosition;ID++;}
		ID = 1; while (ID < BoneSet7.length){BoneSet7Pos[ID] = BoneSet7[ID].localPosition;ID++;}
		ID = 1; while (ID < BoneSet8.length){BoneSet8Pos[ID] = BoneSet8[ID].localPosition;ID++;}
		ID = 1; while (ID < BoneSet9.length){BoneSet9Pos[ID] = BoneSet9[ID].localPosition;ID++;}
		*/
	}

	void Update()
	{
		/*
		Timer += 1;

		if (Timer > 30)
		{
			ID = 1; while (ID < BoneSet1Pos.length){BoneSet1[ID].localPosition = BoneSet1Pos[ID];ID++;}
			ID = 1; while (ID < BoneSet2Pos.length){BoneSet2[ID].localPosition = BoneSet2Pos[ID];ID++;}
			ID = 1; while (ID < BoneSet3Pos.length){BoneSet3[ID].localPosition = BoneSet3Pos[ID];ID++;}
			ID = 1; while (ID < BoneSet4Pos.length){BoneSet4[ID].localPosition = BoneSet4Pos[ID];ID++;}
			ID = 1; while (ID < BoneSet5Pos.length){BoneSet5[ID].localPosition = BoneSet5Pos[ID];ID++;}
			ID = 1; while (ID < BoneSet6Pos.length){BoneSet6[ID].localPosition = BoneSet6Pos[ID];ID++;}
			ID = 1; while (ID < BoneSet7Pos.length){BoneSet7[ID].localPosition = BoneSet7Pos[ID];ID++;}
			ID = 1; while (ID < BoneSet8Pos.length){BoneSet8[ID].localPosition = BoneSet8Pos[ID];ID++;}
			ID = 1; while (ID < BoneSet9Pos.length){BoneSet9[ID].localPosition = BoneSet9Pos[ID];ID++;}

			enabled = false;
		}
		*/

		if (this.Head != null)
		{
			this.RightArm.localPosition = this.RightArmPosition;
			this.RightArm.localEulerAngles = this.RightArmRotation;

			this.LeftArm.localPosition = this.LeftArmPosition;
			this.LeftArm.localEulerAngles = this.LeftArmRotation;

			this.RightLeg.localPosition = this.RightLegPosition;
			this.RightLeg.localEulerAngles = this.RightLegRotation;

			this.LeftLeg.localPosition = this.LeftLegPosition;
			this.LeftLeg.localEulerAngles = this.LeftLegRotation;

			this.Head.localPosition = this.HeadPosition;
		}

		this.enabled = false;
	}
}
