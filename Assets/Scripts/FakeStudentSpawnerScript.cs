using UnityEngine;

public class FakeStudentSpawnerScript : MonoBehaviour
{
	public Transform FakeStudentParent;

	public GameObject NewStudent;
	public GameObject FakeFemale;
	public GameObject FakeMale;
	public GameObject Student;

	public bool AlreadySpawned = false;

	public int CurrentFloor = 0;
	public int CurrentRow = 0;

	public int FloorLimit = 0;
	public int RowLimit = 0;

	public int StudentIDLimit = 0;
	public int StudentID = 0;
	public int Spawned = 0;
	public int Height = 0;
	public int NESW = 0;
	public int ID = 0;

	public GameObject[] SuspiciousObjects;

	#if UNITY_EDITOR

	public void Update()
	{
		/*
		if (Input.GetKeyDown("space"))
		{
			if (!this.AlreadySpawned)
			{
				SuspiciousObjects[ID].SetActive(true);
				ID++;
			}
		}

		if (this.AlreadySpawned)
		{
			this.StudentID += 10;

			if (this.StudentID > this.StudentIDLimit)
			{
				this.StudentID = 0;
			}
		}
		*/
	}

	#endif

	public void Spawn()
	{
		if (!this.AlreadySpawned)
		{
			this.Student = this.FakeFemale;

			this.NESW = 1;

			//while (this.Spawned < (this.FloorLimit * 3))
			while (this.Spawned < 100)
			{
				//var NESW = Random.Range(1, 5);

				if (this.NESW == 1)
				{
					this.NewStudent = Instantiate(this.Student,
						new Vector3(Random.Range(-21.0f, 21.0f),
						this.Height,
						Random.Range(21.0f, 19.0f)),
						Quaternion.identity);
				}
				else if (this.NESW == 2)
				{
					this.NewStudent = Instantiate(this.Student,
						new Vector3(Random.Range(19.0f, 21.0f),
						this.Height,
						Random.Range(29.0f, -37.0f)),
						Quaternion.identity);
				}
				else if (this.NESW == 3)
				{
					this.NewStudent = Instantiate(this.Student,
						new Vector3(Random.Range(-21.0f, 21.0f),
						this.Height,
						Random.Range(-21.0f, -19.0f)),
						Quaternion.identity);
				}
				else if (this.NESW == 4)
				{
					this.NewStudent = Instantiate(this.Student,
						new Vector3(Random.Range(-19.0f, -21.0f),
						this.Height,
						Random.Range(29.0f, -37.0f)),
						Quaternion.identity);
				}

				this.StudentID++;

				this.NewStudent.GetComponent<PlaceholderStudentScript>().FakeStudentSpawner = this;
				this.NewStudent.GetComponent<PlaceholderStudentScript>().StudentID = this.StudentID;
				this.NewStudent.GetComponent<PlaceholderStudentScript>().NESW = this.NESW;
				this.NewStudent.transform.parent = this.FakeStudentParent;

				this.CurrentFloor++;
				this.CurrentRow++;
				this.Spawned++;

				if (this.CurrentFloor == this.FloorLimit)
				{
					this.CurrentFloor = 0;
					this.Height += 4;
				}

				if (this.CurrentRow == this.RowLimit)
				{
					this.CurrentRow = 0;
					this.NESW++;

					if (this.NESW > 4)
					{
						this.NESW = 1;
					}
				}

				// [af] Replaced if/else statement with ternary expression.
				this.Student = (this.Student == this.FakeFemale) ?
					this.FakeMale : this.FakeFemale;
			}

			this.StudentIDLimit = this.StudentID;
			this.StudentID = 1;

			this.AlreadySpawned = true;
		}
		else
		{
			// [af] Replaced if/else statement with boolean expression.
			// [af] Added "gameObject" for C# compatibility.
			this.FakeStudentParent.gameObject.SetActive(!this.FakeStudentParent.gameObject.activeInHierarchy);
		}
	}
}