using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_materialColorRandomizer : MonoBehaviour
{
    public Color color1 = new Color(0.6f, 0.6f, 0.6f, 1f);
    public Color color2 = new Color(0.4f, 0.4f, 0.4f, 1f);
    public bool unifiedColor = true;

    private float ColR = 0f;
    private float ColG = 0f;
    private float ColB = 0f;
    private float ColA = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (unifiedColor == false)
        {
            ColR = Random.Range(color1.r, color2.r);
            ColG = Random.Range(color1.g, color2.g);
            ColB = Random.Range(color1.b, color2.b);
            ColA = Random.Range(color1.a, color2.a);
        }

        if (unifiedColor == true)
        {
            float rnd = Random.value;

            ColR = Mathf.Min(color1.r, color2.r) + (Mathf.Abs(color1.r - color2.r) * rnd);
            ColG = Mathf.Min(color1.g, color2.g) + (Mathf.Abs(color1.g - color2.g) * rnd);
            ColB = Mathf.Min(color1.b, color2.b) + (Mathf.Abs(color1.b - color2.b) * rnd);


        }


        //renderer.material.SetColor("_TintColor", Color(ColR,ColG,ColB,ColA));
        GetComponent<Renderer>().material.color = new Color(ColR, ColG, ColB, ColA);
    }
}
