using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_destroyThisTimed : MonoBehaviour
{
    public float destroyTime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
