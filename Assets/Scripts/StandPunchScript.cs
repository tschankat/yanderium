using UnityEngine;

public class StandPunchScript : MonoBehaviour
{
	public Collider MyCollider;

	void OnTriggerEnter(Collider other)
	{
		StudentScript student = other.gameObject.GetComponent<StudentScript>();

		if (student != null)
		{
			if (student.StudentID > 1)
			{
				student.JojoReact();
			}
		}
	}
}
