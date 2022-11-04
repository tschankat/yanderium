using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_prefabGenerator : MonoBehaviour
{
    public GameObject[] createThis = null;

    private float rndNr = 0.0f;

    public int thisManyTimes = 3;
    public float overThisTime = 1.0f;

    public float xWidth = 0.0f;
    public float yWidth = 0.0f;
    public float zWidth = 0.0f;

    public float xRotMax = 0.0f;
    public float yRotMax = 180.0f;
    public float zRotMax = 0.0f;

    public bool allUseSameRotation = false;
    private bool allRotationDecided = false;

    public bool detachToWorld = true;

    private float x_cur = 0.0f;
    private float y_cur = 0.0f;
    private float z_cur = 0.0f;

    private float xRotCur = 0.0f;
    private float yRotCur = 0.0f;
    private float zRotCur = 0.0f;

    private float timeCounter = 0.0f;
    private int effectCounter = 0;

    private float trigger = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (thisManyTimes < 1) thisManyTimes = 1; //hack to avoid division with zero and negative numbers
        trigger = overThisTime / thisManyTimes;  //define the intervals of time of the prefab generation.
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;

        if (timeCounter > trigger && effectCounter <= thisManyTimes)
        {
            rndNr = Mathf.Floor(Random.value * createThis.Length);  //decide which prefab to create


            x_cur = transform.position.x + (Random.value * xWidth) - (xWidth * 0.5f);  // decide an actual place
            y_cur = transform.position.y + (Random.value * yWidth) - (yWidth * 0.5f);
            z_cur = transform.position.z + (Random.value * zWidth) - (zWidth * 0.5f);

            if (allUseSameRotation == false || allRotationDecided == false)  // basically this plays only once if allRotationDecided=true, otherwise it plays all the time
            {
                xRotCur = transform.rotation.x + (Random.value * xRotMax * 2) - (xRotMax);  // decide rotation
                yRotCur = transform.rotation.y + (Random.value * yRotMax * 2) - (yRotMax);
                zRotCur = transform.rotation.z + (Random.value * zRotMax * 2) - (zRotMax);
                allRotationDecided = true;
            }


            GameObject justCreated = Instantiate(createThis[Mathf.FloorToInt(rndNr)], new Vector3(x_cur, y_cur, z_cur), transform.rotation);  //create the prefab
            justCreated.transform.Rotate(xRotCur, yRotCur, zRotCur);

            if (detachToWorld == false)  // if needed we attach the freshly generated prefab to the object that is holding this script
            {
                justCreated.transform.parent = transform;
            }

            timeCounter -= trigger;  //administration :p
            effectCounter += 1;
        }
    }
}
