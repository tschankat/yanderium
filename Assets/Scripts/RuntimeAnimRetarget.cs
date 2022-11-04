using UnityEngine;
using System.Collections;

public class RuntimeAnimRetarget : MonoBehaviour
{
	public GameObject Source;
	public GameObject Target;

	private Component[] SourceSkelNodes;
	private Component[] TargetSkelNodes;

	void Start ()
	{
		Debug.Log (Source.name);

		// init skeletons
		SourceSkelNodes = Source.GetComponentsInChildren<Component>();
		TargetSkelNodes = Target.GetComponentsInChildren<Component>();
	}

	void LateUpdate ()
	{
		// retarget hip position (!!!) very important
		TargetSkelNodes [1].transform.localPosition = SourceSkelNodes [1].transform.localPosition;

		for(int i = 0; i < 154; i++)
		{
			TargetSkelNodes[i].transform.localRotation = SourceSkelNodes[i].transform.localRotation;
		}
	}
}