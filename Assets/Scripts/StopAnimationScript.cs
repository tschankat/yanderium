using UnityEngine;

public class StopAnimationScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;

	public Transform Yandere;
	private Animation Anim;

	void Start()
	{
		this.StudentManager = GameObject.Find("StudentManager").GetComponent<StudentManagerScript>();
		this.Anim = this.GetComponent<Animation>();
	}

	void Update()
	{
		if (this.StudentManager.DisableFarAnims)
		{
			if (Vector3.Distance(this.Yandere.position, this.transform.position) > 15.0f)
			{
				if (this.Anim.enabled)
				{
					this.Anim.enabled = false;
				}
			}
			else
			{
				if (!this.Anim.enabled)
				{
					this.Anim.enabled = true;
				}
			}
		}
		else
		{
			if (!this.Anim.enabled)
			{
				this.Anim.enabled = true;
			}
		}
	}
}
