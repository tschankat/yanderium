using UnityEngine;

public class MatchTriggerScript : MonoBehaviour
{
	public PickUpScript PickUp;

	public StudentScript Student;
	public bool Fireball = false;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			this.Student = other.gameObject.GetComponent<StudentScript>();

			if (this.Student == null)
			{
				GameObject rootObject = other.gameObject.transform.root.gameObject;
				this.Student = rootObject.GetComponent<StudentScript>();
			}

			if (this.Student != null)
			{
				if (this.Student.Gas || this.Fireball)
				{
					this.Student.Combust();

					if (this.PickUp != null)
					{
						if (this.PickUp.Yandere.PickUp != null)
						{
							if (this.PickUp.Yandere.PickUp == this.PickUp)
							{
								this.PickUp.Yandere.TargetStudent = this.Student;
								this.PickUp.Yandere.MurderousActionTimer = 1;
							}
						}
					}

					if (this.Fireball)
					{
						Destroy(this.gameObject);
					}
				}
			}
		}
	}
}