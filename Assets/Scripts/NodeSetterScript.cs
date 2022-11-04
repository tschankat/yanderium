using UnityEngine;

public class NodeSetterScript : MonoBehaviour
{
	public GameObject[] Nodes;
	public GameObject Node;
	public bool Stairs = false;
	public bool Door = false;
	public float Height = 0.0f;
	public int Column = 0;
	public int Row = 0;

	void Start()
	{
		// [af] All commented in JS.
		/*
		var NodeParent = GameObject.Find("NodeParent").transform;

		var ID = 0;

		Column = -1;
		Row = -1;

		if (Stairs)
		{
			Height = .7;
		}
		else
		{
			Height = .1;
		}

		while (ID < 9)
		{
			Nodes[ID] = Instantiate(Node, transform.position + (Row * Vector3.right) + (Column * Vector3.forward) + (Height * Vector3.up), Quaternion.identity);

			Nodes[ID].transform.parent = NodeParent;

			Row++;

			if (Row > 1)
			{
				if (Stairs)
				{
					Height += .7;
				}

				Column++;
				Row = -1;
			}

			ID++;
		}

		if (Door)
		{
			Nodes[ID].transform.parent = NodeParent;
		}

		//Destroy(this);
		*/
	}
}
