using UnityEngine;

public class GridScript : MonoBehaviour
{
	public GameObject Tile;
	public int Row = 0;
	public int Column = 0;
	public int Rows = 25;
	public int Columns = 25;
	public int ID = 0;

	void Start()
	{
		// [af] Converted while loop to for loop.
		for (; this.ID < (this.Rows * this.Columns); this.ID++)
		{
			GameObject NewTile = Instantiate(this.Tile, 
				new Vector3(this.Row, 0.0f, this.Column), Quaternion.identity);
			NewTile.transform.parent = this.transform;

			this.Row++;

			if (this.Row > this.Rows)
			{
				this.Row = 1;
				this.Column++;
			}
		}

		this.transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
		this.transform.position = new Vector3(-52.0f, 0.0f, -52.0f);
	}
}
