using UnityEngine;
//using UnityEditor;

public class UpthrustScript : MonoBehaviour
{
    [SerializeField]
    private float amplitude = 0.1f;
    [SerializeField]
    private float frequency = 0.6f;
    [SerializeField]
    private Vector3 rotationAmplitude = new Vector3(4.45f, 4.45f, 4.45f);

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.localPosition;
    }

    void Update()
    {
        var sine = amplitude * Mathf.Sin(2 * Mathf.PI * frequency * Time.time);
        transform.localPosition = startPosition + evaluatePosition(Time.time);
        transform.Rotate(rotationAmplitude * sine);
    }

    private Vector3 evaluatePosition(float time)
    {
        var sine = amplitude * Mathf.Sin(2 * Mathf.PI * frequency * time);
        return new Vector3(0, sine, 0);
    }

#if UNITY_EDITOR
/*
    private void OnDrawGizmosSelected()
    {
        float time = (float)EditorApplication.timeSinceStartup;
        MeshFilter filter = GetComponent<MeshFilter>();
        if(filter != null && !EditorApplication.isPlaying)
        {
            Mesh m = filter.sharedMesh;
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireMesh(m, transform.position + evaluatePosition(time));
            SceneView.RepaintAll();
        }
    }
    */
#endif
}
