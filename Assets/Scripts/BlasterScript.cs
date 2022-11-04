using UnityEngine;

public class BlasterScript : MonoBehaviour
{
	public Transform Skull;
	public Renderer Eyes;
	public Transform Beam;
	public float Size = 0.0f;

	void Start()
	{
		this.Skull.localScale = Vector3.zero;
		this.Beam.localScale = Vector3.zero;
	}

	void Update()
	{
		AnimationState blastState = this.GetComponent<Animation>()["Blast"];

		if (blastState.time > 1.0f)
		{
			this.Beam.localScale = Vector3.Lerp(this.Beam.localScale,
				new Vector3(15.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
			this.Eyes.material.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
		}

		if (blastState.time >= blastState.length)
		{
			Destroy(this.gameObject);
		}
	}

	void LateUpdate()
	{
		AnimationState blastState = this.GetComponent<Animation>()["Blast"];

		// [af] Converted if/else statement to ternary expression.
		this.Size = (blastState.time < 1.50f) ?
			Mathf.Lerp(this.Size, 2.0f, Time.deltaTime * 5.0f) :
			Mathf.Lerp(this.Size, 0.0f, Time.deltaTime * 10.0f);

		this.Skull.localScale = new Vector3(this.Size, this.Size, this.Size);
	}
}
