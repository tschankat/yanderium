using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentCrusherScript : MonoBehaviour
{
	public MechaScript Mecha;

	void OnTriggerEnter(Collider other)
	{
		//Debug.Log("Collided with " + other.gameObject.name);

		//Debug.Log("That object's root is: " + other.transform.root.gameObject.name);

		if (other.transform.root.gameObject.layer == 9)
		{
			//Debug.Log("Collided with a student!");

			StudentScript Student = other.transform.root.gameObject.GetComponent<StudentScript>();

			if (Student != null)
			{
				if (Student.StudentID > 1)
				{
					//Debug.Log("Attempting to kill student.");

					if (Mecha.Speed > .9f)
					{
						Instantiate(Student.BloodyScream, transform.position + Vector3.up, Quaternion.identity);
						Student.BecomeRagdoll();
					}

					if (Mecha.Speed > 5)
					{
						Student.Ragdoll.Dismember();
					}
				}
			}
		}
	}
}