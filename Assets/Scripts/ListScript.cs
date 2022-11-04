using UnityEngine;

public class ListScript : MonoBehaviour
{
	public Transform[] List;

	public bool AutoFill;

	public void Start()
	{
		if (AutoFill)
		{
			int ID = 1;

			for (ID = 1; ID < List.Length; ID++)
			{
				List[ID] = transform.GetChild(ID - 1);
			}
		}
	}
}