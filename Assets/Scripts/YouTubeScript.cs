using UnityEngine;

public class YouTubeScript : MonoBehaviour
{
	public float Strength = 0.0f;

	public bool Begin;

	void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			Begin = true;
		}

		if (Begin)
		{
			Strength += Time.deltaTime;

			transform.localPosition = Vector3.Lerp(
				transform.localPosition,
				new Vector3(0, 1.15f, 1),
				Time.deltaTime * Strength);
		}
	}
}