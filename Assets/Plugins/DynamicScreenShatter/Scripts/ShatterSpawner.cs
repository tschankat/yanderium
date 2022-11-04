using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScreenShatter;

public class ShatterSpawner : MonoBehaviour
{

    public bool UseScreenSize = true;
    public int Width = 1920;
    public int Height = 1080;

    [Tooltip("The layer to use for physics and rendering")]
    [Range(0, 31)]
    public int Layer = 31;

    [Header("Shatter")]
    [Tooltip("The position where you want the crack to start")]
    public Vector2 ShatterOrigin;

    [Tooltip("Enable this if you want it to start at a random point")]
    public bool RandomizeShatterOrigin;

    [Header("Style")]
    [Tooltip("The material you want to use for the screen")]
    public Material ScreenMaterial;

    [Range(2, 20)]
    [Tooltip("The number of rings that you want in the shatter")]
    public int NumberOfRings = 10;

    [Range(2, 20)]
    [Tooltip("The number of linear cracks you want from the origin")]
    public int NumberOfRadials = 10;
    
    [Range(0, 1)]
    [Tooltip("The amount of jitteryness you want in the linear cracks")]
    public float Jitteryness = 0.5f;

    [Range(0, 1)]
    [Tooltip("The chance that one of the square shards will break into two triangle shard")]
    public float BreakQuadsChance = 0.5f;

    [Header("Physics")]
    [Tooltip("The width of the simulation area in meters")]
    public float PhysicsScale = 10f;

    [Tooltip("How hard to push away the shards")]
    public float PushForce = 100f;

    [Header("Camera Settings")]
    [Tooltip("Enable to have a solid background")]
    public bool ClearCamera = false;

    [Tooltip("The background color to use if ClearCamera is set to true")]
    public Color BackgroundColor = Color.black;

    [Tooltip("The camera depth to use when creating the camera")]
    public float CameraDepth = 1f;

    [Header("Cleanup")]

    [Tooltip("Destroys the object this script is attached to on cleanup when set to true")]
    public bool DestroyGameObjectOnEnd = false;

    [Tooltip("if greater than 0 cleans up the shards after the number of seconds")]
    public float MaxPlayTime = 4f;

    private ShatterGenerator shatterGenerator;
    private List<GameObject> shards = new List<GameObject>();
    private List<Polygon> polygonShards = new List<Polygon>();

    private float secondsActive = 0.0f;
    
    private GameObject shatterContainer;

    private Camera shatterCamera;
    private GameObject cameraObject;

    // Use this for initialization
    void Start ()
	{
        // If needed set the width and height to the screen size
        if (UseScreenSize)
        {
            Width = Screen.width;
            Height = Screen.height;
        }

        // If needed pick a random origin to start the shatter
	    if (RandomizeShatterOrigin)
	        ShatterOrigin = new Vector2(Random.value*Width, Random.value*Height);

        // Create a ShatterGenerator to generate the shatter geometry
        shatterGenerator = new ShatterGenerator(Width, Height, ShatterOrigin, NumberOfRadials, NumberOfRings, Jitteryness, BreakQuadsChance);

        // Generate the shatter
        shatterGenerator.GenerateShatter();

	    int i = 0;
        // Create a GameObject to contain the shards
        shatterContainer = new GameObject("Shatter Container");

        // Generate the Meshes for each shard
        foreach (var p in shatterGenerator.Polygons)
            GeneratePolygonMesh(p, i++);

        // Create a temporary camera
        SetupCamera();

        // Generate the colliders and rigidbodies for the shards
        FallWithPhysics();
	}

    private void GeneratePolygonMesh(Polygon polygon, int polygonNum)
    {
        var vertices2D = polygon.points;
        // Use the triangulator to get indices for creating triangles
        Triangulator tr = new Triangulator(vertices2D);
        int[] indices = tr.Triangulate();

        // Calculate the UVs
        var uvs = vertices2D.Select(v => new Vector2(v.x/Width, v.y/Height)).ToArray();

        // Create the Vector3 vertices
        Vector3[] vertices = new Vector3[vertices2D.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            var v2 = vertices2D[i] - polygon.center;
            vertices[i] = new Vector3(v2.x, v2.y, 0);
        }

        // Create the mesh
        Mesh msh = new Mesh();
        msh.vertices = vertices;
        msh.triangles = indices;
        msh.uv = uvs;
        msh.RecalculateNormals();
        msh.RecalculateBounds();

        // Create GameObject for the shard
        var shard = new GameObject();
        shard.name = "Shard #" + polygonNum;
        shard.transform.Translate(new Vector3(polygon.center.x, polygon.center.y), 0);
        shard.transform.parent = shatterContainer.transform;

        // Assign the GameObject to the proper layer
        shard.layer = Layer;

        // Assign the Mesh to the GameObject
        var renderer = shard.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        renderer.material = ScreenMaterial;

        MeshFilter filter = shard.AddComponent(typeof(MeshFilter)) as MeshFilter;
        filter.mesh = msh;

        // Store the GameObject and Polygon for later
        polygonShards.Add(polygon);
        shards.Add(shard);
    }

    private void FallWithPhysics()
    {
        // For each shard
        for(int i=0; i<shards.Count; i++)
        {
            // Get the shard GameObject and it's Polygon
            var shard = shards[i];
            var polygon = polygonShards[i];
            
            // Add a PolygonCollider2D
            var collider = shard.AddComponent<PolygonCollider2D>();
            collider.points = polygon.points.Select(s =>  s - polygon.center).ToArray();

            // Add a rigidbody
            var rb = shard.AddComponent<Rigidbody2D>();
            rb.angularVelocity = Random.Range(-2.0f * Mathf.PI, 2.0f*Mathf.PI);
            rb.useAutoMass = true;
            
            // Push the shard
            var delta = (polygon.center - ShatterOrigin).normalized * PushForce;
            rb.AddForce(delta);
        }

        // Scale down the shards to match the PhysicsScale
        float scale = PhysicsScale/Width;
        shatterContainer.transform.localScale = new Vector3(scale, scale, scale);
    }

    private void SetupCamera()
    {
        // Create the camera object
        cameraObject = new GameObject("Temporary Camera");
        shatterCamera = cameraObject.AddComponent<Camera>();

        shatterCamera.orthographic = true;
        shatterCamera.orthographicSize = PhysicsScale * Screen.height / Screen.width * 0.5f;
        shatterCamera.cullingMask = 1 << Layer;

        shatterCamera.clearFlags = ClearCamera ? CameraClearFlags.Color : CameraClearFlags.Depth;

        shatterCamera.backgroundColor = BackgroundColor;

        // Center the camera on the shards
        var camTransform = cameraObject.transform;
        camTransform.Translate(-camTransform.localPosition);

        float scale = PhysicsScale / Width;
        camTransform.Translate(Width * scale / 2f, Height * scale / 2f, -1f);
    }

    // Update is called once per frame
	void Update ()
	{
        // Update Timer
	    secondsActive += Time.deltaTime;
	    if (secondsActive > MaxPlayTime && MaxPlayTime > 0)
	    {
            Destroy(this);

            if (DestroyGameObjectOnEnd)
                Destroy(gameObject);
        }
	}

    void OnDestroy()
    {
        // Cleanup
        Destroy(shatterContainer);
        Destroy(cameraObject);
        Destroy(this);
    }
}
