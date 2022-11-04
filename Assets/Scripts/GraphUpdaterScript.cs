using UnityEngine;

public class GraphUpdaterScript : MonoBehaviour
{
	public AstarPath Graph;
	public int Frames = 0;

	void Update()
	{
		if (this.Frames > 0)
		{
			this.Graph.Scan();

			Destroy(this);
		}

		this.Frames++;
	}
}
