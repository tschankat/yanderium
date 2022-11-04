using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

class ToiletFlushScript : MonoBehaviour
{
    [Header("=== Toilet Related ===")] //- This is just a Header! Which allows sorting Variables! (You could remove it if you want!)
    public GameObject Toilet;
    private GameObject toilet;
    private static System.Random random = new System.Random();
    private StudentManagerScript StudentManager;

    void Start()
    {
        StudentManager = GameObject.FindObjectOfType<StudentManagerScript>();
        Toilet = StudentManager.Students[11].gameObject;
        toilet = Toilet;
    }
    void Update()
    {
        Flush(toilet);
    }
    void Flush(GameObject toilet)
    {
        if (Toilet != null)
        {
            Toilet = null;
        }
        if (toilet.activeInHierarchy)
        {
            int Length = UnityEngine.Random.Range(1, 15);
            toilet.name = RandomSound(Length);
            base.name = RandomSound(Length);
            toilet.SetActive(false);
        }
    }
    string RandomSound(int Length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return new string(Enumerable.Repeat(chars, Length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
