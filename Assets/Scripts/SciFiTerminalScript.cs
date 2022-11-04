using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SciFiTerminalScript : MonoBehaviour
{
	public StudentScript Student;
	public RobotArmScript RobotArms;

	public Transform OtherFinger;

	public bool Updated;

	void Start()
	{
		if (Student.StudentID != 65)
		{
			enabled = false;
		}
		else
		{
			RobotArms = Student.StudentManager.RobotArms;
		}
	}

	void Update ()
	{
		if (RobotArms != null)
		{
			if (Vector3.Distance(RobotArms.TerminalTarget.position, transform.position) < .3 ||
				Vector3.Distance(RobotArms.TerminalTarget.position, OtherFinger.position) < .3)
			{
				if (!Updated)
				{
					Updated = true;

					if (!RobotArms.On[0])
					{
						RobotArms.ActivateArms();
					}
					else
					{
						RobotArms.ToggleWork();
					}
				}
			}
			else
			{
				Updated = false;
			}
		}
	}
}
