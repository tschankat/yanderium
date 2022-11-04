using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_moveThis : MonoBehaviour
{
    public float translationSpeedX = 0;
    public float translationSpeedY = 1;
    public float translationSpeedZ = 0;
    public bool local = true;
    
    // Update is called once per frame
    void Update()
    {
        if (local == true)
        {
            transform.Translate(new Vector3(translationSpeedX, translationSpeedY, translationSpeedZ) * Time.deltaTime);
        }

        if (local == false)
        {
            transform.Translate(new Vector3(translationSpeedX, translationSpeedY, translationSpeedZ) * Time.deltaTime, Space.World);
        }
    }
}
