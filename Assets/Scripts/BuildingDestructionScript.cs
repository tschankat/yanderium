using UnityEngine;

public class BuildingDestructionScript : MonoBehaviour
{
	public Transform NewSchool;
	public bool Sink = false;
	public int Phase = 0;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			this.Phase++;
			this.Sink = true;
		}

		if (this.Sink)
		{
			if (this.Phase == 1)
			{
				this.transform.position = new Vector3(
					Random.Range(-1.0f, 1.0f),
					this.transform.position.y - (Time.deltaTime * 10.0f),
					Random.Range(-19.0f, -21.0f));
			}
			else
			{
				if (this.NewSchool.position.y != 0.0f)
				{
					this.NewSchool.position = new Vector3(
						this.NewSchool.position.x,
						Mathf.MoveTowards(this.NewSchool.position.y, 0.0f, Time.deltaTime * 10.0f),
						this.NewSchool.position.z);

					this.transform.position = new Vector3(
						Random.Range(-1.0f, 1.0f),
						this.transform.position.y,
						Random.Range(13.0f, 15.0f));
				}
				else
				{
					this.transform.position = new Vector3(0.0f, this.transform.position.y, 14.0f);
				}
			}
		}
	}
}
