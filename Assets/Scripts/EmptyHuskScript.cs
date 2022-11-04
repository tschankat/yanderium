using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyHuskScript : MonoBehaviour
{
	public StudentScript TargetStudent;

	public Animation MyAnimation;

	public GameObject DarkAura;

	public Transform Mouth;

	public float[] BloodTimes;

	public int EatPhase;

	void Update ()
	{
		if (EatPhase < BloodTimes.Length)
		{
			if (MyAnimation[AnimNames.SixEat].time > BloodTimes[EatPhase])
			{
				GameObject Blood = Instantiate(TargetStudent.StabBloodEffect, Mouth.position, Quaternion.identity);
				Blood.GetComponent<RandomStabScript>().Biting = true;
				EatPhase++;
			}
		}

		if (MyAnimation[AnimNames.SixEat].time >= this.MyAnimation[AnimNames.SixEat].length)
		{
			if (DarkAura != null)
			{
				Instantiate(DarkAura, transform.position + (Vector3.up * .81f), Quaternion.identity);
			}

			Destroy(gameObject);
		}
	}
}