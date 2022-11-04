using UnityEngine;

public class DelinquentMaskScript : MonoBehaviour
{
	public MeshFilter MyRenderer;

	public Mesh[] Meshes;

	public int ID;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftAlt))
		{
			ID++;

			if (ID > 4)
			{
				ID = 0;
			}

			this.MyRenderer.mesh = Meshes[ID];
		}
	}
}