using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_randomRotation : MonoBehaviour
{
    public float rotationMaxX = 0.0f;
    public float rotationMaxY = 360.0f;
    public float rotationMaxZ = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        var rotX = Random.Range(-rotationMaxX, rotationMaxX);
        var rotY = Random.Range(-rotationMaxY, rotationMaxY);
        var rotZ = Random.Range(-rotationMaxZ, rotationMaxZ);

        transform.Rotate(rotX, rotY, rotZ);
    }
}
