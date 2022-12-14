using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiggedAttacher : MonoBehaviour
{
    public Transform BasePelvisRoot;
    public Transform AttachmentPelvisRoot;

    void Start()
    {
        //We start the attach process!
        Attaching(BasePelvisRoot, AttachmentPelvisRoot);
    }
    void Attaching(Transform Base, Transform Attachment)
    {
        //Attaching process, we first parent the attachment to the original pelvis!
        //Then we set the local position and local eulerangles to 0,
        //Then we make 2 arrays to get all Bones in our 2 PelvisRoot's
        //WARNING! ALL BONES NEED TO HAVE THE SAME IDENTICAL NAME!
        //Then we attach the attachment bones  to the Base bones and set the local position and local eulerangles to 0!
        //The attachment isn't allowed to play a animation, while we attach it!

        Attachment.transform.SetParent(Base);
        Base.localEulerAngles = Vector3.zero;
        Base.localPosition = Vector3.zero;
        Transform[] BaseBones = Base.GetComponentsInChildren<Transform>();
        Transform[] AttachmentBones = Attachment.GetComponentsInChildren<Transform>();
        foreach (Transform AttachmentBones1 in AttachmentBones)
        {
            foreach (Transform BaseBones1 in BaseBones)
            {
                if (AttachmentBones1.name == BaseBones1.name)
                {
                    AttachmentBones1.SetParent(BaseBones1);
                    AttachmentBones1.localEulerAngles = Vector3.zero;
                    AttachmentBones1.localPosition = Vector3.zero;
                }
            }
        }
    }
}
