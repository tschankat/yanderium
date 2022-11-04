using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_animRandomizer : MonoBehaviour
{
    public AnimationClip[] animList = null;
    public AnimationClip actualAnim = null;

    public float minSpeed = 0.7f;
    public float maxSpeed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        Animation animationComponent = GetComponent<Animation>();

        int rnd = Mathf.RoundToInt(Random.Range(0, animList.Length));
        actualAnim = animList[rnd];
        animationComponent.Play(actualAnim.name);
        animationComponent[actualAnim.name].speed = Random.Range(minSpeed, maxSpeed);
    }
}
