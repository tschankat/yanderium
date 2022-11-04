using UnityEngine;

public class AccessoryGroupScript : MonoBehaviour
{
	public GameObject[] Parts;

	public void SetPartsActive(bool active)
	{
		foreach (GameObject part in this.Parts)
		{
			part.SetActive(active);
		}
	}
}
