using UnityEngine;

public class ArcTrailScript : MonoBehaviour
{
	static readonly Color TRAIL_TINT_COLOR = new Color(1.0f, 0.0f, 0.0f, 1.0f);

	public TrailRenderer Trail;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			this.Trail.material.SetColor("_TintColor", TRAIL_TINT_COLOR);
		}
	}
}
