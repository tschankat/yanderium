using UnityEngine;

public class YanvaniaTripleFireballScript : MonoBehaviour
{
	public Transform[] Fireballs;

	public Transform Dracula;

	public int Direction = 0;

	public float Speed = 0.0f;

	public float Timer = 0.0f;

	void Start()
	{
		// [af] Replaced if/else statement with ternary expression.
		this.Direction = (this.Dracula.position.x > this.transform.position.x) ? -1 : 1;
	}

	void Update()
	{
		Transform fireball1 = this.Fireballs[1];
		Transform fireball2 = this.Fireballs[2];
		Transform fireball3 = this.Fireballs[3];

		if (fireball1 != null)
		{
			fireball1.position = new Vector3(
				fireball1.position.x,
				Mathf.MoveTowards(fireball1.position.y, 7.50f, Time.deltaTime * this.Speed),
				fireball1.position.z);
		}

		if (fireball2 != null)
		{
			fireball2.position = new Vector3(
				fireball2.position.x,
				Mathf.MoveTowards(fireball2.position.y, 7.16666f, Time.deltaTime * this.Speed),
				fireball2.position.z);
		}

		if (fireball3 != null)
		{
			fireball3.position = new Vector3(
				fireball3.position.x,
				Mathf.MoveTowards(fireball3.position.y, 6.83333f, Time.deltaTime * this.Speed),
				fireball3.position.z);
		}

		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < 4; ID++)
		{
			Transform fireball = this.Fireballs[ID];

			if (fireball != null)
			{
				fireball.position = new Vector3(
					fireball.position.x + (this.Direction * Time.deltaTime * this.Speed),
					fireball.position.y,
					fireball.position.z);
			}
		}

		// [af] Removed unused "ID" assignment.

		this.Timer += Time.deltaTime;

		if (this.Timer > 10.0f)
		{
			Destroy(this.gameObject);
		}
	}
}
