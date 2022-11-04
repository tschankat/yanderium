using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingTestScript : MonoBehaviour
{
	byte[] bytes;

	void Update ()
	{
		if (Input.GetKeyDown("left"))
		{
			bytes = AstarPath.active.astarData.SerializeGraphs ();
		}

		if (Input.GetKeyDown("right"))
		{
			AstarPath.active.astarData.DeserializeGraphs (bytes);
			AstarPath.active.Scan();
		}
	}
}