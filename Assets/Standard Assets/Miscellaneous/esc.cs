using UnityEngine;

public class esc : MonoBehaviour
{
	void Awake()
	{
		// [af] Moved from class scope for compatibility with C#.
		Cursor.visible = false;
	}

	void Update()
	{
		if (Input.GetKey("escape"))
		{
			Application.Quit();
		}
	}
}
