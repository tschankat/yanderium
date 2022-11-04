using UnityEngine;

public class RooftopScript : MonoBehaviour
{
	public GameObject[] DumpPoints;
	public GameObject Railing;
	public GameObject Fence;

	void Start()
	{
		if (SchoolGlobals.RoofFence)
		{
			// [af] Converted while loop to foreach loop.
			foreach (GameObject dumpPoint in this.DumpPoints)
			{
				dumpPoint.SetActive(false);
			}

			this.Railing.SetActive(false);
			this.Fence.SetActive(true);
		}
	}
}
