using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParentScript : MonoBehaviour
{
    public YandereScript Yandere;

    public GameObject Bloodpool;
    public GameObject Footprint;

    public Vector3[] FootprintPositions;
    public Vector3[] BloodPositions;

    public Vector3[] FootprintRotations;
    public Vector3[] BloodRotations;

    public float[] BloodSizes;

    public int FootprintID = 0;
    public int PoolID = 0;

    public void RecordAllBlood()
    {
        PoolID = 0;
        FootprintID = 0;

        foreach (Transform child in transform)
        {
            BloodPoolScript poolScript = child.GetComponent<BloodPoolScript>();

            if (poolScript != null)
            {
                PoolID++;

                if (PoolID < 100)
                {
                    BloodPositions[PoolID] = child.position;
                    BloodRotations[PoolID] = child.eulerAngles;
                    BloodSizes[PoolID] = poolScript.TargetSize;
                }
            }
            else
            {
                FootprintID++;

                if (FootprintID < 100)
                {
                    FootprintPositions[FootprintID] = child.position;
                    FootprintRotations[FootprintID] = child.eulerAngles;
                }
            }
        }
    }

    public void RestoreAllBlood()
    {
        while (PoolID > 0)
        {
            GameObject NewBloodPool = Instantiate(Bloodpool, BloodPositions[PoolID], Quaternion.identity);
            NewBloodPool.GetComponent<BloodPoolScript>().TargetSize = BloodSizes[PoolID];
            NewBloodPool.transform.eulerAngles = BloodRotations[PoolID];
            NewBloodPool.transform.parent = transform;
            PoolID--;
        }

        while (FootprintID > 0)
        {
            GameObject NewFootprint = Instantiate(Footprint, FootprintPositions[FootprintID], Quaternion.identity);
            NewFootprint.transform.GetChild(0).GetComponent<FootprintScript>().Yandere = Yandere;
            NewFootprint.transform.eulerAngles = FootprintRotations[FootprintID];
            NewFootprint.transform.parent = transform;
            FootprintID--;
        }
    }
}
