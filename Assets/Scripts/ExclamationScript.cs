using UnityEngine;

public class ExclamationScript : MonoBehaviour
{
	public Renderer Graphic;

	public float Alpha = 0.0f;
	public float Timer = 0.0f;

	public Camera MainCamera;

	void Start()
	{
		this.transform.localScale = Vector3.zero;
		this.Graphic.material.SetColor("_TintColor", new Color(0.50f, 0.50f, 0.50f, 0.0f));

		MainCamera = Camera.main;
	}

	void Update()
	{
		this.Timer -= Time.deltaTime;

		if (this.Timer > 0.0f)
		{
			this.transform.LookAt(MainCamera.transform);

			if (this.Timer > 1.50f)
			{
				this.transform.localScale = Vector3.Lerp(
					this.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);
				this.Alpha = Mathf.Lerp(this.Alpha, 0.50f, Time.deltaTime * 10.0f);

				this.Graphic.material.SetColor(
					"_TintColor", new Color(0.50f, 0.50f, 0.50f, this.Alpha));
			}
			else
			{
				if (this.transform.localScale.x > 0.10f)
				{
					this.transform.localScale = Vector3.Lerp(
						this.transform.localScale, Vector3.zero, Time.deltaTime * 10.0f);
				}
				else
				{
					this.transform.localScale = Vector3.zero;
				}

				this.Alpha = Mathf.Lerp(this.Alpha, 0.0f, Time.deltaTime * 10.0f);

				this.Graphic.material.SetColor(
					"_TintColor", new Color(0.50f, 0.50f, 0.50f, this.Alpha));
			}
		}
	}
}
