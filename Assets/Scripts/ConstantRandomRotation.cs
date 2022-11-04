using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRandomRotation : MonoBehaviour
{
	void Update()
	{
		var rotX = Random.Range(0, 360);
		var rotY = Random.Range(0, 360);
		var rotZ = Random.Range(0, 360);

		transform.Rotate(rotX, rotY, rotZ);
	}
}