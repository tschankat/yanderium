using UnityEngine;

public class FoldingChairScript : MonoBehaviour
{
	public GameObject[] Student;

	void Start()
	{
		int ID = Random.Range(0, this.Student.Length);

		Instantiate(this.Student[ID],
			this.transform.position - new Vector3(0.0f, 0.40f, 0.0f),
			this.transform.rotation);
	}
}
