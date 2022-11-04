using UnityEngine;

public class DumpsterLidScript : MonoBehaviour
{
	public StudentScript Victim;

	public Transform SlideLocation;
	public Transform GarbageDebris;
	public Transform Hinge;

	public GameObject FallChecker;
	public GameObject Corpse;

	public PromptScript[] DragPrompts;
	public PromptScript Prompt;

	public float DisposalSpot = 0.0f;
	public float Rotation = 0.0f;

	public bool Slide = false;
	public bool Fill = false;
	public bool Open = false;

	public int StudentToGoMissing;

	void Start()
	{
		this.FallChecker.SetActive(false);
		this.Prompt.HideButton[3] = true;
	}

	void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Prompt.Circle[0].fillAmount = 1.0f;

			if (!this.Open)
			{
				this.Prompt.Label[0].text = "     " + "Close";
				this.Open = true;
			}
			else
			{
				this.Prompt.Label[0].text = "     " + "Open";
				this.Open = false;
			}
		}

		if (!this.Open)
		{
			this.Rotation = Mathf.Lerp(this.Rotation, 0.0f, Time.deltaTime * 10.0f);

			this.Prompt.HideButton[3] = true;
		}
		else
		{
			this.Rotation = Mathf.Lerp(this.Rotation, -115.0f, Time.deltaTime * 10.0f);

			if (this.Corpse != null)
			{
				if (this.Prompt.Yandere.PickUp != null)
				{
					// [af] Replaced if/else statement with boolean expression.
					this.Prompt.HideButton[3] = !this.Prompt.Yandere.PickUp.Garbage;
				}
				else
				{
					this.Prompt.HideButton[3] = true;
				}
			}
			else
			{
				this.Prompt.HideButton[3] = true;
			}

			if (this.Prompt.Circle[3].fillAmount == 0.0f)
			{
				Destroy(this.Prompt.Yandere.PickUp.gameObject);

				this.Prompt.Circle[3].fillAmount = 1.0f;

				this.Prompt.HideButton[3] = false;

				this.Fill = true;
			}

			if ((this.transform.position.z > (this.DisposalSpot - 0.050f)) &&
				(this.transform.position.z < (this.DisposalSpot + 0.050f)))
			{
				// [af] Replaced if/else statement with boolean expression.
				this.FallChecker.SetActive(this.Prompt.Yandere.RoofPush);
			}
			else
			{
				this.FallChecker.SetActive(false);
			}

			if (this.Slide)
			{
				this.transform.eulerAngles = Vector3.Lerp(this.transform.eulerAngles, this.SlideLocation.eulerAngles, Time.deltaTime * 10);
				this.transform.position = Vector3.Lerp(this.transform.position, this.SlideLocation.position, Time.deltaTime * 10);

				this.Corpse.GetComponent<RagdollScript>().Student.Hips.position = this.transform.position + new Vector3(0, 1, 0);

				if (Vector3.Distance(this.transform.position, this.SlideLocation.position) < .01f)
				{
					this.DragPrompts[0].enabled = false;
					this.DragPrompts[1].enabled = false;
					this.FallChecker.SetActive(false);
					this.Slide = false;
				}
			}
		}

		this.Hinge.localEulerAngles = new Vector3(this.Rotation, 0.0f, 0.0f);

		if (this.Fill)
		{
			this.GarbageDebris.localPosition = new Vector3(
				this.GarbageDebris.localPosition.x,
				Mathf.Lerp(this.GarbageDebris.localPosition.y, 1.0f, Time.deltaTime * 10.0f),
				this.GarbageDebris.localPosition.z);

			if (this.GarbageDebris.localPosition.y > 0.99f)
			{
				this.Prompt.Yandere.Police.SuicideScene = false;
				this.Prompt.Yandere.Police.Suicide = false;
				this.Prompt.Yandere.Police.HiddenCorpses--;
				this.Prompt.Yandere.Police.Corpses--;

				if (this.Corpse.GetComponent<RagdollScript>().AddingToCount)
				{
					this.Prompt.Yandere.NearBodies--;
				}

				this.GarbageDebris.localPosition = new Vector3(
					this.GarbageDebris.localPosition.x,
					1.0f,
					this.GarbageDebris.localPosition.z);

				this.StudentToGoMissing = this.Corpse.GetComponent<StudentScript>().StudentID;

				Destroy(this.Corpse);
				this.Fill = false;

				this.Prompt.Yandere.StudentManager.UpdateStudents();
			}
		}
	}

	public void SetVictimMissing()
	{
		StudentGlobals.SetStudentMissing(this.StudentToGoMissing, true);
	}
}