using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
using UnityEditorInternal;
#endif

using System.Collections.Generic;

public class GrassPlaneGenerator : MonoBehaviour
{
    public float Width = 10;
    public float Height = 10;
    public bool Intersect;
    public float IntersectHeight = 1;
    public bool OffsetCorrection;
    public LayerMask IntersectLayers;
    [Range(0.1f, 10f)] public float quadSize = 0.1f;

    void OnDrawGizmosSelected()
    {
        quadSize = Mathf.Clamp(quadSize, 0.1f, 10f);

        Vector3 center = transform.position;
        Vector3 size = new Vector3(Width, 0, Height);
        Vector3 denseSize = new Vector3(quadSize, 0, quadSize);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(center, size);

		if (Intersect)
		{
            Gizmos.color = new Color(0.5f, 0.5f, 1, 0.5f);
            Gizmos.DrawCube(center + Vector3.up * IntersectHeight / 2, size + Vector3.up * IntersectHeight);
        }

        Gizmos.color = Color.cyan;
        for (float x = 0; x < Width + 0.09f - quadSize; x += quadSize)
        {
            for (float y = 0; y < Height + 0.09f - quadSize; y += quadSize)
            {
                Gizmos.DrawWireCube(center - size / 2 + denseSize / 2 + new Vector3(x, 0, y), denseSize);
            }
        }
    }

    public void Bake()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> indices = new List<int>();

        Vector3 center = transform.position;
        Vector3 size = new Vector3(Width, 0, Height);
        Vector3 denseSize = new Vector3(quadSize, 0, quadSize);

        int widthCount = (int)(Width / quadSize);
        int heightCount = (int)(Height / quadSize);

        float denseWidth = widthCount * quadSize;
        float denseHeight = heightCount * quadSize;

        float widthFill = (Width - denseWidth);
        float heightFill = (Height - denseHeight);

        Vector3 fitScale = new Vector3(Width / denseWidth, 1, Height / denseHeight);

        Texture2D intersectMap = null;

        if (Intersect)
        {
            RenderTexture activeTex = RenderTexture.active;

            Camera scanCamera = new GameObject("Temporary Camera").AddComponent<Camera>();
            scanCamera.transform.rotation = Quaternion.Euler(90, 0, 0);
            scanCamera.transform.position = center + Vector3.up * IntersectHeight;
            scanCamera.orthographic = true;
            scanCamera.aspect = Width / Height;
            scanCamera.orthographicSize = Height / 2;
            scanCamera.nearClipPlane = 0.01f;
            scanCamera.farClipPlane = IntersectHeight + 0.01f;
            scanCamera.clearFlags = CameraClearFlags.Color;
            scanCamera.backgroundColor = new Color(0, 0, 0, 0);
            scanCamera.cullingMask = IntersectLayers;

            RenderTexture rt = new RenderTexture((int)Width * 100, (int)Height * 100, 1);
            scanCamera.targetTexture = rt;
            scanCamera.forceIntoRenderTexture = true;

            RenderTexture.active = rt;

            scanCamera.Render();
            intersectMap = new Texture2D(rt.width, rt.height, TextureFormat.RGBA32, false);
            intersectMap.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
            intersectMap.filterMode = FilterMode.Trilinear;
            intersectMap.Apply();

            RenderTexture.active = activeTex;

            DestroyImmediate(scanCamera.gameObject);
        }

        int square = 0;
        for (float x = 0; x < Width + 0.09f - quadSize; x += quadSize)
        {
            for (float y = 0; y < Height + 0.09f - quadSize; y += quadSize)
            {
				if (Intersect)
				{
                    float correction = OffsetCorrection ? quadSize : 0;
                    int pX = (int)((x + correction) * 100);
                    int pY = (int)((y + correction) * 100);
                    float avg = 0;
                    for(int xx = -1; xx < 2; xx++)
					{
                        for(int yy = -1; yy < 2; yy++)
						{
                            avg += intersectMap.GetPixel((int)((x + correction) * 100 + xx), (int)((y + correction) * 100 + yy)).a;
                        }
					}

                    if (avg != 0)
                        continue;
				}
                Vector3 vCenter = new Vector3(x - Width / 2, 0, y - Height / 2);
                vertices.Add((new Vector3(0      , 0, 0)         + vCenter));
                vertices.Add((new Vector3(0      , 0, quadSize)   + vCenter));
                vertices.Add((new Vector3(quadSize, 0, quadSize)   + vCenter));
                vertices.Add((new Vector3(quadSize, 0, 0)         + vCenter));

                indices.Add(square + 0);
                indices.Add(square + 1);
                indices.Add(square + 2);

                indices.Add(square + 0);
                indices.Add(square + 2);
                indices.Add(square + 3);

                square += 4;
            }
        }

        // Apply stretch
        for(int i = 0; i < vertices.Count; i++)
        {
            Vector3 vertex = vertices[i];
            vertex.x *= fitScale.x;
            vertex.z *= fitScale.z;

            vertex.x += (widthFill*fitScale.x) / 2;
            vertex.z += (heightFill*fitScale.z) / 2;

            vertices[i] = vertex;
        }

        Mesh m = new Mesh();
        m.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        m.subMeshCount = 1;
        m.SetVertices(vertices);
        m.SetIndices(indices.ToArray(), MeshTopology.Triangles, 0);
        m.RecalculateNormals();

        GameObject newMeshObject = new GameObject("GrassMesh");
        newMeshObject.transform.position = transform.position;
        newMeshObject.AddComponent<MeshRenderer>().gameObject.AddComponent<MeshFilter>().mesh = m;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GrassPlaneGenerator))]
public class GrassPlaneEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var marginLabel = new GUIStyle(EditorStyles.label);
        marginLabel.margin = new RectOffset(30, 0, 0, 0);

        GrassPlaneGenerator gen = (GrassPlaneGenerator)target;

        GUILayout.BeginHorizontal();
        GUILayout.Label("Size");
        gen.Width = EditorGUILayout.FloatField(gen.Width);
        gen.Height = EditorGUILayout.FloatField(gen.Height);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Density");
        gen.quadSize = EditorGUILayout.Slider(gen.quadSize, 0.1f, 10f);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Scan for intersections");
        gen.Intersect = EditorGUILayout.Toggle(gen.Intersect);
        GUILayout.EndHorizontal();

		if (gen.Intersect)
		{
            GUILayout.BeginHorizontal();
            GUILayout.Label("Scan roof height", marginLabel);
            GUILayout.FlexibleSpace();
            gen.IntersectHeight = EditorGUILayout.FloatField(gen.IntersectHeight);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Intersect with layers", marginLabel);
            GUILayout.FlexibleSpace();
            gen.IntersectLayers = InternalEditorUtility.ConcatenatedLayersMaskToLayerMask(EditorGUILayout.MaskField(InternalEditorUtility.LayerMaskToConcatenatedLayersMask(gen.IntersectLayers), InternalEditorUtility.layers));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Offset correction", marginLabel);
            GUILayout.FlexibleSpace();
            gen.OffsetCorrection = EditorGUILayout.Toggle(gen.OffsetCorrection);
            GUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Bake"))
            gen.Bake();

        serializedObject.ApplyModifiedProperties();
    }
}
#endif