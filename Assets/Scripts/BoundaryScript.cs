using UnityEngine;

public class BoundaryScript : MonoBehaviour
{
	public TextureCycleScript TextureCycle;
	public Transform Yandere;
	public UITexture Static;
	public UILabel Label;
	public float Intensity = 0.0f;

	void Update()
	{
		float yandereZ = this.Yandere.position.z;

		if (yandereZ < -94.0f)
		{
			this.Intensity = -95.0f + Mathf.Abs(yandereZ);
			this.TextureCycle.gameObject.SetActive(true);
			this.TextureCycle.Sprite.enabled = true;
			this.TextureCycle.enabled = true;

			Color staticColor = this.Static.color + new Color(.0001f, .0001f, .0001f, .0001f);
			Color labelColor = this.Label.color;
			staticColor.a = this.Intensity / 5.0f;
			labelColor.a = this.Intensity / 5.0f;
			this.Static.color = staticColor;
			this.Label.color = labelColor;

			this.GetComponent<AudioSource>().volume = (this.Intensity / 5.0f) * 0.10f;

			Vector3 localPosition = this.Label.transform.localPosition;
			localPosition.x = Random.Range(-10.0f, 10.0f);
			localPosition.y = Random.Range(-10.0f, 10.0f);
			this.Label.transform.localPosition = localPosition;
		}
		// [af] Combined else and if to reduce nesting.
		else if (this.TextureCycle.enabled)
		{
			this.TextureCycle.gameObject.SetActive(false);
			this.TextureCycle.Sprite.enabled = false;
			this.TextureCycle.enabled = false;

			Color staticColor = this.Static.color;
			Color labelColor = this.Label.color;
			staticColor.a = 0.0f;
			labelColor.a = 0.0f;
			this.Static.color = staticColor;
			this.Label.color = labelColor;

			this.GetComponent<AudioSource>().volume = 0.0f;
		}
	}
}
