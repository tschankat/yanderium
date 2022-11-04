using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothLookAtScript : MonoBehaviour
{
    public Transform Target;

    public float Speed;

    void LateUpdate()
    {
        Quaternion targetRotation = Quaternion.LookRotation(
            Target.transform.position - transform.position);
    
        transform.rotation = Quaternion.Slerp(
            transform.rotation, targetRotation, Time.deltaTime * Speed);
    }
}