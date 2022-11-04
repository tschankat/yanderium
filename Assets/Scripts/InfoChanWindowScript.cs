using UnityEngine;

public class InfoChanWindowScript : MonoBehaviour
{
	public Transform DropPoint;
	public GameObject[] Drops;
	public int[] ItemsToDrop;
	public int Orders = 0;
	public int ID = 0;
	public float Rotation = 0.0f;
	public float Timer = 0.0f;
	public bool Dropped = false;
	public bool Drop = false;
	public bool Open = true;
	public bool Test = false;

	void Update()
	{
		if (this.Drop)
		{
			// [af] Converted if/else statement to assignment with ternary expression.
			this.Rotation = Mathf.Lerp(this.Rotation,
				this.Drop ? -90.0f : 0.0f, Time.deltaTime * 10.0f);

			this.transform.localEulerAngles = new Vector3(
				this.transform.localEulerAngles.x,
				this.Rotation,
				this.transform.localEulerAngles.z);

			this.Timer += Time.deltaTime;

			if (this.Timer > 1.0f)
			{
				if (this.Orders > 0.0f)
				{
					// Drop a new item.
					Instantiate(this.Drops[this.ItemsToDrop[this.Orders]],
						this.DropPoint.position, Quaternion.identity);

					this.Timer = 0.0f;
					this.Orders--;
				}
				else
				{
					this.Open = false;

					if (this.Timer > 3.0f)
					{
						this.transform.localEulerAngles = new Vector3(
							this.transform.localEulerAngles.x,
							0.0f,
							this.transform.localEulerAngles.z);

						this.Drop = false;
					}
				}
			}
		}

		if (this.Test)
		{
			this.DropObject();
		}
	}

	public void DropObject()
	{
		this.Rotation = 0.0f;
		this.Timer = 0.0f;

		this.Dropped = false;
		this.Test = false;
		this.Drop = true;
		this.Open = true;
	}
}
