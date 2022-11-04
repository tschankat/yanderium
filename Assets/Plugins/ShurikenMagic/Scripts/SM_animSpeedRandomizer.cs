using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_animSpeedRandomizer : MonoBehaviour
{
    public float minSpeed = 0.7f;
    public float maxSpeed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        Animation animationComponent = GetComponent<Animation>();
        animationComponent[animationComponent.clip.name].speed = Random.Range(minSpeed, maxSpeed);
    }
}
