using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectrifiedPuddleScript : MonoBehaviour
{
	public PowerSwitchScript PowerSwitch;

	void OnTriggerEnter(Collider other)
	{
		//Debug.Log(other.name + " touched me!");

		if (other.gameObject.layer == 9)
		{
			StudentScript Student = other.gameObject.GetComponent<StudentScript>();

			if (Student != null)
			{
				if (!Student.Electrified)
				{
					Student.Yandere.GazerEyes.ElectrocuteStudent(Student);

					gameObject.SetActive(false);

					PowerSwitch.On = false;
				}
			}
		}

		if (other.gameObject.layer == 13)
		{
			YandereScript Yandere = other.gameObject.GetComponent<YandereScript>();

			if (Yandere != null)
			{
				Yandere.StudentManager.Headmaster.Taze();
				Yandere.StudentManager.Headmaster.Heartbroken.Headmaster = false;
			}
		}
	}
}
