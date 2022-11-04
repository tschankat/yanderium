using UnityEngine;

public class PlaceholderStudentScript : MonoBehaviour
{
	public FakeStudentSpawnerScript FakeStudentSpawner;

	public GameObject EmptyGameObject;
	public bool ShootRaycasts;
	public Transform Target;
	public Transform Eyes;

	public int StudentID = 0;
	public int Phase = 0;
	public int NESW = 0;

	void Start()
	{
		this.Target = Instantiate(this.EmptyGameObject).transform;
		this.ChooseNewDestination();
	}

	void Update()
	{
		this.transform.LookAt(this.Target.position);

		this.transform.position = Vector3.MoveTowards(
			this.transform.position, this.Target.position, Time.deltaTime);

		if (Vector3.Distance(this.transform.position, this.Target.position) < 1.0f)
		{
			this.ChooseNewDestination();
		}

		if (Input.GetKeyDown("space"))
		{
			if (!ShootRaycasts)
			{
				ShootRaycasts = true;
			}
			else
			{
				Phase++;
			}
		}

		if (this.transform.position.y < 1)
		{
			if (ShootRaycasts)
			{
				if (Phase == 0)
				{
					//if (Vector3.Distance(transform.position, FakeStudentSpawner.SuspiciousObjects[0].transform.position) < 5)
					{
						Debug.DrawLine(Eyes.position, FakeStudentSpawner.SuspiciousObjects[0].transform.position, Color.red);
					}

					//if (Vector3.Distance(transform.position, FakeStudentSpawner.SuspiciousObjects[1].transform.position) < 5)
					{
						Debug.DrawLine(Eyes.position, FakeStudentSpawner.SuspiciousObjects[1].transform.position, Color.red);
					}

					//if (Vector3.Distance(transform.position, FakeStudentSpawner.SuspiciousObjects[2].transform.position) < 5)
					{
						Debug.DrawLine(Eyes.position, FakeStudentSpawner.SuspiciousObjects[2].transform.position, Color.red);
					}
				}
				else
				{
					if (StudentID < (FakeStudentSpawner.StudentID + 5) && StudentID > (FakeStudentSpawner.StudentID - 5) )
					{
						if (Vector3.Distance(transform.position, FakeStudentSpawner.SuspiciousObjects[0].transform.position) < 5)
						{
							Debug.DrawLine(Eyes.position, FakeStudentSpawner.SuspiciousObjects[0].transform.position, Color.red);
						}

						if (Vector3.Distance(transform.position, FakeStudentSpawner.SuspiciousObjects[1].transform.position) < 5)
						{
							Debug.DrawLine(Eyes.position, FakeStudentSpawner.SuspiciousObjects[1].transform.position, Color.red);
						}

						if (Vector3.Distance(transform.position, FakeStudentSpawner.SuspiciousObjects[2].transform.position) < 5)
						{
							Debug.DrawLine(Eyes.position, FakeStudentSpawner.SuspiciousObjects[2].transform.position, Color.red);
						}
					}
				}
			}
		}
	}

	void ChooseNewDestination()
	{
		if (this.NESW == 1)
		{
			this.Target.position = new Vector3(
				Random.Range(-21.0f, 21.0f), 
				this.transform.position.y, 
				Random.Range(21.0f, 19.0f));
		}
		else if (this.NESW == 2)
		{
			this.Target.position = new Vector3(
				Random.Range(19.0f, 21.0f),
				this.transform.position.y, 
				Random.Range(29.0f, -37.0f));
		}
		else if (this.NESW == 3)
		{
			this.Target.position = new Vector3(
				Random.Range(-21.0f, 21.0f),
				this.transform.position.y, 
				Random.Range(-21.0f, -19.0f));
		}
		else if (this.NESW == 4)
		{
			this.Target.position = new Vector3(
				Random.Range(-19.0f, -21.0f),
				this.transform.position.y, 
				Random.Range(29.0f, -37.0f));
		}
	}
}
