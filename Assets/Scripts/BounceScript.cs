using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceScript : MonoBehaviour
{
    public float StartingMotion;

    public float DecliningSpeed;

    public float Motion;

    public float PositionX;
    public float Speed;

    public Transform MyCamera;

    public bool Go;

    void Start()
    {
        StartingMotion += Random.Range(-0.001f, .001f);
        DecliningSpeed += Random.Range(-0.001f, .001f);
    }

    void Update()
    {
        transform.position += new Vector3(0, Motion, 0);

        Motion -= Time.deltaTime * DecliningSpeed;

        if (transform.position.y < .5f)
        {
            Motion = StartingMotion;
        }

        if (MyCamera != null)
        {
            if (Go)
            {
                Speed += Time.deltaTime;

                PositionX = Mathf.Lerp(PositionX, -.999f, Time.deltaTime * Speed);

                MyCamera.position = new Vector3(
                    PositionX,
                    1,
                    -10);
            }
        }
    }
}
