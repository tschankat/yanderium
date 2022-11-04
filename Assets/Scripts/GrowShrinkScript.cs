using UnityEngine;

public class GrowShrinkScript : MonoBehaviour
{
	public float FallSpeed = 0.0f;
	public float Threshold = 1.0f;
	public float Slowdown = 0.5f;
	public float Strength = 1.0f;
	public float Target = 1.0f;
	public float Scale = 0.0f;
	public float Speed = 5.0f;
	public float Timer = 0.0f;
	public bool Shrink = false;
	public Vector3 OriginalPosition;

	void Start()
	{
		this.OriginalPosition = this.transform.localPosition;
		this.transform.localScale = Vector3.zero;
	}

	void Update()
	{
		this.Timer += Time.deltaTime;
		this.Scale += Time.deltaTime * (this.Strength * this.Speed);

		if (!this.Shrink)
		{
			this.Strength += Time.deltaTime * this.Speed;

			if (this.Strength > this.Threshold)
			{
				this.Strength = this.Threshold;
			}

			if (this.Scale > this.Target)
			{
				this.Threshold *= this.Slowdown;
				this.Shrink = true;
			}
		}
		else
		{
			this.Strength -= Time.deltaTime * this.Speed;

			float negativeThreshold = this.Threshold * -1.0f;
			if (this.Strength < negativeThreshold)
			{
				this.Strength = negativeThreshold;
			}

			if (this.Scale < this.Target)
			{
				this.Threshold *= this.Slowdown;
				this.Shrink = false;
			}
		}

		if (this.Timer > 3.33333f)
		{
			this.FallSpeed += Time.deltaTime * 10.0f;
			this.transform.localPosition = new Vector3(
				this.transform.localPosition.x,
				this.transform.localPosition.y - (this.FallSpeed * this.FallSpeed),
				this.transform.localPosition.z);
		}

		this.transform.localScale = new Vector3(this.Scale, this.Scale, this.Scale);
	}

	public void Return()
	{
		this.transform.localPosition = this.OriginalPosition;
		this.transform.localScale = Vector3.zero;

		this.FallSpeed = 0.0f;
		this.Threshold = 1.0f;
		this.Slowdown = 0.5f;
		this.Strength = 1.0f;
		this.Target = 1.0f;
		this.Scale = 0.0f;
		this.Speed = 5.0f;
		this.Timer = 0.0f;

		// [af] Added "gameObject" for C# compatibility.
		this.gameObject.SetActive(false);
	}
}
