using UnityEngine;

public class PanelScript : MonoBehaviour
{
	public UILabel BuildingLabel;
	public DoorBoxScript DoorBox;

	public Transform Player;

	public string Floor = string.Empty;

	public float PracticeBuildingZ = 0.0f;
	public float StairsZ = 0.0f;

	public float Floor1Height = 0.0f;
	public float Floor2Height = 0.0f;
	public float Floor3Height = 0.0f;

	void Update()
	{
		// [af] Looks like this could be replaced by defining bounding boxes
		// for each "zone".
		if ((this.Player.position.z > this.StairsZ) ||
			(this.Player.position.z < -this.StairsZ))
		{
			this.Floor = "Stairs";
		}
		else if (this.Player.position.y < this.Floor1Height)
		{
			this.Floor = "First Floor";
		}
		else if ((this.Player.position.y > this.Floor1Height) &&
			(this.Player.position.y < this.Floor2Height))
		{
			this.Floor = "Second Floor";
		}
		else if ((this.Player.position.y > this.Floor2Height) &&
			(this.Player.position.y < this.Floor3Height))
		{
			this.Floor = "Third Floor";
		}
		else
		{
			this.Floor = "Rooftop";
		}

		if (this.Player.position.z < this.PracticeBuildingZ)
		{
			this.BuildingLabel.text = "Practice Building, " + this.Floor;
		}
		else
		{
			this.BuildingLabel.text = "Classroom Building, " + this.Floor;
		}

		this.DoorBox.Show = false;
	}
}
