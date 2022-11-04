using UnityEngine;

public class EmergencyExitScript : MonoBehaviour
{
	public StudentScript Student;

	public Transform Yandere;

	public Transform Pivot;

	public float Timer = 0.0f;

	public bool Open = false;

	void Update()
	{
		if (Vector3.Distance(this.Yandere.position, this.transform.position) < 2)
		{
			Open = true;
		}
		else
		{
			if (Timer == 0.0f)
			{
				this.Student = null;
				Open = false;
			}
		}

		if (!this.Open)
		{
			this.Pivot.localEulerAngles = new Vector3(
				this.Pivot.localEulerAngles.x,
				Mathf.Lerp(this.Pivot.localEulerAngles.y, 0.0f, Time.deltaTime * 10.0f),
				this.Pivot.localEulerAngles.z);
		}
		//If "Open" is true...
		else
		{
			this.Pivot.localEulerAngles = new Vector3(
				this.Pivot.localEulerAngles.x,
				Mathf.Lerp(this.Pivot.localEulerAngles.y, 90.0f, Time.deltaTime * 10.0f),
				this.Pivot.localEulerAngles.z);

			this.Timer = Mathf.MoveTowards(this.Timer, 0.0f, Time.deltaTime);
		}
	}

	void OnTriggerStay(Collider other)
	{
		this.Student = other.gameObject.GetComponent<StudentScript>();

		if (this.Student != null)
		{
			this.Timer = 1.0f;
			this.Open = true;
		}
	}
}
