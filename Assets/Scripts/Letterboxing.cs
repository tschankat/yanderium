using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Letterboxing : MonoBehaviour
{
	const float KEEP_ASPECT = 16.0f / 9.0f;

	void Start()
	{
		float aspectRatio = Screen.width / ((float)Screen.height);
		float percentage = 1.0f - (aspectRatio / KEEP_ASPECT);
		this.GetComponent<Camera>().rect =
			new Rect(0.0f, percentage / 2.0f, 1.0f, (1.0f - percentage));
	}
}
