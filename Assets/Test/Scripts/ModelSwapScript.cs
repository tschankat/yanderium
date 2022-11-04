using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSwapScript : MonoBehaviour
{
	public Transform PelvisRoot;
	public GameObject Attachment;

	public void Update()
	{
		if (Input.GetKeyDown("z"))
		{
			//Attach(Attachment, true);
		}
	}

	public void Attach(GameObject Attachment, bool Inactives)
	{
		StartCoroutine(Attach_Threat(this.PelvisRoot, Attachment, Inactives));
	}

	public IEnumerator Attach_Threat(Transform PelvisRoot, GameObject Attachment, bool Inactives)
	{
		Attachment.transform.SetParent(PelvisRoot);
		PelvisRoot.localEulerAngles = Vector3.zero;
		PelvisRoot.localPosition = Vector3.zero;

		Transform[] StudentBones = PelvisRoot.GetComponentsInChildren<Transform>(Inactives);
		Transform[] Bones = Attachment.GetComponentsInChildren<Transform>(Inactives);

		foreach(Transform Attachment_Bone in Bones)
		{
			foreach(Transform Student_Bone in StudentBones)
			{
				if(Attachment_Bone.name == Student_Bone.name)
				{
					Attachment_Bone.SetParent(Student_Bone);

					Attachment_Bone.localEulerAngles = Vector3.zero;
					Attachment_Bone.localPosition = Vector3.zero;
				}
			}
		}
		yield return null;
	}
}