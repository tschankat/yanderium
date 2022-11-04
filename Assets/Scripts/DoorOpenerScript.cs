using UnityEngine;

public class DoorOpenerScript : MonoBehaviour
{
	public StudentScript Student;
	public DoorScript Door;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			this.Student = other.gameObject.GetComponent<StudentScript>();

			if (this.Student != null && !this.Student.Dying &&
                !this.Door.Open && !this.Door.Locked)
			{
				this.Door.Student = this.Student;
				this.Door.OpenDoor();
			}
		}
	}

    void OnTriggerStay(Collider other)
    {
        if (!Door.Open)
        {
            if (other.gameObject.layer == 9)
            {
                this.Student = other.gameObject.GetComponent<StudentScript>();

                if (this.Student != null && !this.Student.Dying &&
                    !this.Door.Open && !this.Door.Locked)
                {
                    this.Door.Student = this.Student;
                    this.Door.OpenDoor();
                }
            }
        }
    }
}