using UnityEngine;

public class LowPolyStudentScript : MonoBehaviour
{
	public StudentScript Student;

	public Renderer TeacherMesh;
	public Renderer MyMesh;

	void Update()
	{
		if (this.Student.StudentManager.LowDetailThreshold > 0.0f)
		{
			/*
			float studentDistance = Vector3.Distance(
				this.Student.Yandere.MainCamera.transform.position,
				this.Student.Hips.position);
			*/

			float studentDistance = this.Student.Prompt.DistanceSqr;

			if (studentDistance > this.Student.StudentManager.LowDetailThreshold)
			{
				if (!this.MyMesh.enabled)
				{
					this.Student.MyRenderer.enabled = false;
					this.MyMesh.enabled = true;
				}
			}
			else
			{
				if (this.MyMesh.enabled)
				{
					this.Student.MyRenderer.enabled = true;
					this.MyMesh.enabled = false;
				}
			}
		}
		else
		{
			if (this.MyMesh.enabled)
			{
				this.Student.MyRenderer.enabled = true;
				this.MyMesh.enabled = false;
			}
		}
	}
}
