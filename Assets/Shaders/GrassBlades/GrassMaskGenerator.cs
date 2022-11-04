using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class GrassMaskGenerator : MonoBehaviour 
{
	[SerializeField] private float aspectWidth;
	[SerializeField] private float aspectHeight;
	[SerializeField] private float mapScale;
	[SerializeField] private int mapUpscale;

	private Camera camera;
	private RenderTexture targetTexture;

	public void Start()
	{
		#if !UNITY_EDITOR
		Destroy(this.gameObject);
		#endif
	}

	void Update()
	{
		if (camera == null) camera = GetComponent<Camera>();

		aspectWidth = Mathf.Clamp(aspectWidth, 1, int.MaxValue);
		aspectHeight = Mathf.Clamp(aspectHeight, 1, int.MaxValue);
		mapUpscale = Mathf.Clamp(mapUpscale, 1, 1000);

		// Create texture
		if (targetTexture == null || targetTexture.width != aspectWidth * mapUpscale || targetTexture.height != aspectHeight * mapUpscale)
		{
			if (targetTexture != null) targetTexture.Release();
			targetTexture = new RenderTexture(Mathf.RoundToInt(aspectWidth) * mapUpscale, Mathf.RoundToInt(aspectHeight) * mapUpscale, 1);
		}

		camera.enabled = false;
		camera.farClipPlane = 0.1f;
		camera.orthographic = true;
		camera.orthographicSize = mapScale;
		camera.targetTexture = targetTexture;
	}

	[ContextMenu("Generate and save the grass occlusion map")]
	public void GenerateMap()
	{
		Camera cam = GetComponent<Camera>();

		cam.Render();

		// Create texture
		if (targetTexture == null || targetTexture.width != aspectWidth * mapUpscale || targetTexture.height != aspectHeight * mapUpscale)
		{
			if (targetTexture != null) targetTexture.Release();
			targetTexture = new RenderTexture(Mathf.RoundToInt(aspectWidth) * mapUpscale, Mathf.RoundToInt(aspectHeight) * mapUpscale, 1);
		}

		// Grab the texture
		RenderTexture temp = RenderTexture.active;
		RenderTexture.active = targetTexture;

		Texture2D map = new Texture2D(targetTexture.width, targetTexture.height);
		map.ReadPixels(new Rect(0, 0, targetTexture.width, targetTexture.height), 0, 0);

		RenderTexture.active = temp;

		// Make the mesh
		List<Vector3> vertices = new List<Vector3>();
		List<int> indices = new List<int>();

		int pixel = 0;
		for(int x = 0; x < map.width; x++)
		{
			for(int y = 0; y < map.height; y++)
			{
				if(map.GetPixel(x, y).a == 0)
				{
					Vector3 offset = new Vector3(x, 0, y) / 3;
					vertices.Add(new Vector3(0, 0, 0) + offset);
					vertices.Add(new Vector3(0, 0, 1) + offset);
					vertices.Add(new Vector3(1, 0, 1) + offset);
					vertices.Add(new Vector3(1, 0, 0) + offset);

					indices.Add(pixel + 0);
					indices.Add(pixel + 1);
					indices.Add(pixel + 2);

					indices.Add(pixel + 0);
					indices.Add(pixel + 2);
					indices.Add(pixel + 3);

					pixel += 4;
				}
			}
		}

		Mesh m = new Mesh();
		m.subMeshCount = 1;
		m.SetVertices(vertices);
		m.SetIndices(indices.ToArray(), MeshTopology.Triangles, 0);
		m.RecalculateNormals();

		GameObject newMeshObject = new GameObject("GrassMesh");
		newMeshObject.transform.position = transform.position;
		newMeshObject.AddComponent<MeshRenderer>().gameObject.AddComponent<MeshFilter>().mesh = m;
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;

		Matrix4x4 temp = Gizmos.matrix;
		Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
		float spread = camera.farClipPlane - camera.nearClipPlane;
		float center = (camera.farClipPlane + camera.nearClipPlane) * 0.5f;
		Gizmos.DrawWireCube(new Vector3(0, 0, center), new Vector3(camera.orthographicSize * 2 * camera.aspect, camera.orthographicSize * 2, spread));
	}
}
