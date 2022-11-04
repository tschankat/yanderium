using UnityEngine;

public class RainbowScript : MonoBehaviour
{
	[SerializeField] Renderer MyRenderer;
	[SerializeField] float cyclesPerSecond;
	[SerializeField] float percent;

	void Start()
	{
		this.MyRenderer.material.color = Color.red;
		this.cyclesPerSecond = 0.25f;
	}

	void Update()
	{
		this.percent = (this.percent + (Time.deltaTime * this.cyclesPerSecond)) % 1.0f;
		this.MyRenderer.material.color = Color.HSVToRGB(percent, 1.0f, 1.0f);
	}
}
