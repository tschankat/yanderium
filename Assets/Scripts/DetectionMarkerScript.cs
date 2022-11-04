using UnityEngine;

[System.Serializable]

public class DetectionMarkerScript : MonoBehaviour
{
	public Transform Target;
	public UITexture Tex;

	void Start()
	{
		this.transform.LookAt(new Vector3(
			this.Target.position.x, this.transform.position.y, this.Target.position.z));

		this.Tex.transform.localScale = new Vector3(1.0f, 0.0f, 1.0f);
		this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

		this.Tex.color = new Color(
			this.Tex.color.r, this.Tex.color.g, this.Tex.color.b, 0.0f);
	}

	void Update()
	{
		if (this.Tex.color.a > 0.0f)
		{
			if ((this.transform != null) && (this.Target != null))
			{
				//Debug.Log("We here?");

				this.transform.LookAt(new Vector3(
					this.Target.position.x, this.transform.position.y, this.Target.position.z));
			}
		}
	}
}