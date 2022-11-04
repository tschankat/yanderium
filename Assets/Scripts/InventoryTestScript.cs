using UnityEngine;

public class InventoryTestScript : MonoBehaviour
{
	public SimpleDetectClickScript[] Items;

	public Animation SkirtAnimation;
	public Animation GirlAnimation;

	public GameObject Skirt;
	public GameObject Girl;

	public Renderer SkirtRenderer;
	public Renderer GirlRenderer;

	public Transform RightGridHighlightParent;
	public Transform LeftGridHighlightParent;

	public Transform RightGridItemParent;
	public Transform LeftGridItemParent;

	public Transform Highlight;
	public Transform RightGrid;
	public Transform LeftGrid;

	public float Alpha = 0.0f;

	public bool Open = true;

	public int OpenSpace = 1;
	public int UseColumn = 0;
	public int UseGrid = 0;

	public int Column = 1;
	public int Grid = 1;
	public int Row = 1;

	public bool[] LeftSpaces1;
	public bool[] LeftSpaces2;
	public bool[] LeftSpaces3;
	public bool[] LeftSpaces4;

	public bool[] RightSpaces1;
	public bool[] RightSpaces2;
	public bool[] RightSpaces3;
	public bool[] RightSpaces4;

	void Start()
	{
		this.RightGrid.localScale = new Vector3(0.00f, 0.00f, 0.00f);
		this.LeftGrid.localScale = new Vector3(0.00f, 0.00f, 0.00f);

		Time.timeScale = 1;
	}

	void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			this.Open = !this.Open;
		}

		AnimationState skirtState = SkirtAnimation[AnimNames.InverseSkirtOpen];
		AnimationState charState =  GirlAnimation[AnimNames.FemaleInventory];

		if (this.Open)
		{
			this.RightGrid.localScale = Vector3.MoveTowards(
				this.RightGrid.localScale, new Vector3(.9f, .9f, .9f), Time.deltaTime * 10);
			this.LeftGrid.localScale = Vector3.MoveTowards(
				this.LeftGrid.localScale, new Vector3(.9f, .9f, .9f), Time.deltaTime * 10);
			
			this.transform.position = new Vector3(
				this.transform.position.x,
				this.transform.position.y,
				Mathf.Lerp(this.transform.position.z, 0.37f, Time.deltaTime * 10));

			skirtState.time = Mathf.Lerp(charState.time, 1, Time.deltaTime * 10);
			charState.time = skirtState.time;

			Alpha = Mathf.Lerp(Alpha, 1, Time.deltaTime * 10);

			SkirtRenderer.material.color = new Color(1, 1, 1, Alpha);
			GirlRenderer.materials[0].color = new Color(0, 0, 0, Alpha);
			GirlRenderer.materials[1].color = new Color(0, 0, 0, Alpha);

			if (Input.GetKeyDown("right")){Column++;UpdateHighlight();}
			if (Input.GetKeyDown("left")){Column--;UpdateHighlight();}

			if (Input.GetKeyDown("up")){Row--;UpdateHighlight();}
			if (Input.GetKeyDown("down")){Row++;UpdateHighlight();}
		}
		else
		{
			this.RightGrid.localScale = Vector3.MoveTowards(
				this.RightGrid.localScale, new Vector3(0.00f, 0.00f, 0.00f), Time.deltaTime * 10);
			this.LeftGrid.localScale = Vector3.MoveTowards(
				this.LeftGrid.localScale, new Vector3(0.00f, 0.00f, 0.00f), Time.deltaTime * 10);
			
			this.transform.position = new Vector3(
				this.transform.position.x,
				this.transform.position.y,
				Mathf.Lerp(this.transform.position.z, 1.0f, Time.deltaTime * 10));

			skirtState.time = Mathf.Lerp(charState.time, 0, Time.deltaTime * 10);
			charState.time = skirtState.time;

			Alpha = Mathf.Lerp(Alpha, 0, Time.deltaTime * 10);

			SkirtRenderer.material.color = new Color(1, 1, 1, Alpha);
			GirlRenderer.materials[0].color = new Color(0, 0, 0, Alpha);
			GirlRenderer.materials[1].color = new Color(0, 0, 0, Alpha);
		}

		int ID = 0;

		while (ID < Items.Length)
		{
			if (Items[ID].Clicked)
			{
				Debug.Log("Item width is " + Items[ID].InventoryItem.Width + " and item height is " + Items[ID].InventoryItem.Height + ". Open space is: " + OpenSpace);

				if (Items[ID].InventoryItem.Height * Items[ID].InventoryItem.Width < OpenSpace)
				{
					Debug.Log("We might have enough open space to add the item to the inventory.");

					CheckOpenSpace();

					if (UseGrid == 1)
					{
						Items[ID].transform.parent = LeftGridItemParent;

						float Size = Items[ID].InventoryItem.InventorySize;

						Items[ID].transform.localScale = new Vector3(Size, Size, Size);

						Items[ID].transform.localEulerAngles = new Vector3(90, 180, 0);
						Items[ID].transform.localPosition = Items[ID].InventoryItem.InventoryPosition;

						int GridID = 1;

						if (UseColumn == 1)
						{
							while (GridID < Items[ID].InventoryItem.Height + 1)
							{
								LeftSpaces1[GridID] = true;
								GridID++;
							}
						}
						else if (UseColumn == 2)
						{
							while (GridID < Items[ID].InventoryItem.Height + 1)
							{
								LeftSpaces2[GridID] = true;
								GridID++;
							}
						}

						if (UseColumn > 1)
						{
							Items[ID].transform.localPosition -= new Vector3(.05f * (UseColumn - 1), 0, 0);
						}
					}
				}

				Items[ID].Clicked = false;
			}

			ID++;
		}
	}

	void CheckOpenSpace()
	{
		UseColumn = 0;
		UseGrid = 0;

		int ID = 1;

		while (ID < LeftSpaces1.Length)
		{
			if (UseGrid == 0)
			{
				if (!LeftSpaces1[ID])
				{
					UseColumn = 1;
					UseGrid = 1;
				}
			}

			ID++;
		}

		ID = 1;

		if (UseGrid == 0)
		{
			while (ID < LeftSpaces2.Length)
			{
				if (UseGrid == 0)
				{
					if (!LeftSpaces2[ID])
					{
						UseColumn = 2;
						UseGrid = 1;
					}
				}

				ID++;
			}
		}
	}

	void UpdateHighlight()
	{
		if (Column == 5)
		{
			if (Grid == 1){Grid = 2;}else{Grid = 1;}
			Column = 1;
		}
		else if (Column == 0)
		{
			if (Grid == 1){Grid = 2;}else{Grid = 1;}
			Column = 4;
		}

		if (Row == 6)
		{
			Row = 1;
		}
		else if (Row == 0)
		{
			Row = 5;
		}

		if (Grid == 1)
		{
			Highlight.transform.parent = LeftGridHighlightParent;
		}
		else
		{
			Highlight.transform.parent = RightGridHighlightParent;
		}

		Highlight.localPosition = new Vector3(Column, Row * -1, 0);
	}
}