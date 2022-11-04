using UnityEngine;

public class HomeMangaBookScript : MonoBehaviour
{
	public HomeMangaScript Manga;
	public float RotationSpeed = 0.0f;
	public int ID = 0;

	void Start()
	{
		this.transform.eulerAngles = new Vector3(
			90.0f, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
	}

	void Update()
	{
		// [af] Replaced if/else statement with assignment and ternary expression.
		float eulerY = (this.Manga.Selected == this.ID) ?
			(this.transform.eulerAngles.y + (Time.deltaTime * this.RotationSpeed)) : 0.0f;
		this.transform.eulerAngles = new Vector3(
			this.transform.eulerAngles.x, eulerY, this.transform.eulerAngles.z);
	}
}
