using UnityEngine;

public class ScrollingTexture : MonoBehaviour
{
	public Renderer MyRenderer;
	public float Offset;
	public float Speed;

	void Start()
	{
		MyRenderer = this.GetComponent<Renderer>();
	}

	void Update()
	{
		//Debug.Log("My name is " + this.gameObject.name);

		this.Offset += Time.deltaTime * this.Speed;
		this.MyRenderer.material.SetTextureOffset(
			"_MainTex", new Vector2(this.Offset, this.Offset));
	}
}