using UnityEngine;

public class ActivateOsuScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public OsuScript[] OsuScripts;
	public StudentScript Student;
	public ClockScript Clock;
	public GameObject Music;

	public Transform Mouse;
	public GameObject Osu;

	public int PlayerID = 0;

	public Vector3 OriginalMousePosition;
	public Vector3 OriginalMouseRotation;

	void Start()
	{
		this.OsuScripts = this.Osu.GetComponents<OsuScript>();

		this.OriginalMouseRotation = this.Mouse.transform.eulerAngles;
		this.OriginalMousePosition = this.Mouse.transform.position;
	}

	void Update()
	{
		if (this.Student == null)
		{
			this.Student = this.StudentManager.Students[this.PlayerID];
		}
		else
		{
			if (!this.Osu.activeInHierarchy)
			{
				if (Vector3.Distance(this.transform.position, this.Student.transform.position) < 0.10f)
				{
					if (this.Student.Routine)
					{
						if (this.Student.CurrentDestination == this.Student.Destinations[this.Student.Phase] &&
							this.Student.Actions[this.Student.Phase] == StudentActionType.Gaming)
						{
							this.ActivateOsu();
						}
					}
				}
			}
			else
			{
				this.Mouse.transform.eulerAngles = this.OriginalMouseRotation;

				// [af] Combined identical if statements.
				if (!this.Student.Routine ||
					this.Student.CurrentDestination != this.Student.Destinations[this.Student.Phase] ||
					(this.Student.Actions[this.Student.Phase] != StudentActionType.Gaming))
				{
					this.DeactivateOsu();
				}
			}
		}
	}

	void ActivateOsu()
	{
		this.Osu.transform.parent.gameObject.SetActive(true);
		this.Student.SmartPhone.SetActive(false);
		this.Music.SetActive(true);

		this.Mouse.parent = this.Student.RightHand;
		this.Mouse.transform.localPosition = Vector3.zero;
	}

	void DeactivateOsu()
	{
		this.Osu.transform.parent.gameObject.SetActive(false);
		this.Music.SetActive(false);
		
		// [af] Converted while loop to foreach loop.
		foreach (OsuScript osuScript in this.OsuScripts)
		{
			osuScript.Timer = 0.0f;
		}

		this.Mouse.parent = this.transform.parent;

		this.Mouse.transform.position = this.OriginalMousePosition;
	}
}
