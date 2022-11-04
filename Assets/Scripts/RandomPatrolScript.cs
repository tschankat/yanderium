using UnityEngine;

public class RandomPatrolScript : MonoBehaviour
{
	public Transform[] PatrolPoints;
	public int[] Height;

	void Start()
	{
		// [af] Converted while loop to for loop.
		for (int ID = 1; ID < 5; ID++)
		{
			this.Height[ID] = Random.Range(1, 5);

			// [af] This could be refactored to be a simple multiplication by 4.
			if (this.Height[ID] == 1)
			{
				this.Height[ID] = 0;
			}
			else if (this.Height[ID] == 2)
			{
				this.Height[ID] = 4;
			}
			else if (this.Height[ID] == 3)
			{
				this.Height[ID] = 8;
			}
			else if (this.Height[ID] == 4)
			{
				this.Height[ID] = 12;
			}
		}

		Transform point1 = this.PatrolPoints[1];
		Transform point2 = this.PatrolPoints[2];
		Transform point3 = this.PatrolPoints[3];
		Transform point4 = this.PatrolPoints[4];

		point1.position = new Vector3(
			Random.Range(-21.0f, 21.0f), this.Height[1], Random.Range(21.0f, 19.0f));
		point2.position = new Vector3(
			Random.Range(19.0f, 21.0f), this.Height[2], Random.Range(29.0f, -37.0f));
		point3.position = new Vector3(
			Random.Range(-21.0f, 21.0f), this.Height[3], Random.Range(-21.0f, -19.0f));
		point4.position = new Vector3(
			Random.Range(-19.0f, -21.0f), this.Height[4], Random.Range(29.0f, -37.0f));
		
		point1.localEulerAngles = new Vector3(
			point1.localEulerAngles.x,
			Random.Range(0.0f, 360.0f),
			point1.localEulerAngles.z);
		point2.localEulerAngles = new Vector3(
			point2.localEulerAngles.x,
			Random.Range(0.0f, 360.0f),
			point2.localEulerAngles.z);
		point3.localEulerAngles = new Vector3(
			point3.localEulerAngles.x,
			Random.Range(0.0f, 360.0f),
			point3.localEulerAngles.z);
		point4.localEulerAngles = new Vector3(
			point4.localEulerAngles.x,
			Random.Range(0.0f, 360.0f),
			point4.localEulerAngles.z);
	}
}
