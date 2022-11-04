using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazerHairScript : MonoBehaviour
{
	public SkinnedMeshRenderer MyMesh;

	public float[] TargetWeight;
	public float[] Weight;

	public float Strength = 100;
	public int ID;

	void Update ()
	{
		ID = 0;

		while (ID < Weight.Length)
		{
			Weight[ID] = Mathf.MoveTowards(Weight[ID], TargetWeight[ID], Time.deltaTime * Strength);

			if (Weight[ID] == TargetWeight[ID]){TargetWeight[ID] = Random.Range(0.0f, 100.0f);}

			this.MyMesh.SetBlendShapeWeight(ID, Weight[ID]);

			ID++;
		}
	}
}