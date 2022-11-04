using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// [af] To be used for editing Students.json.
public class StudentEditorScript : MonoBehaviour
{
	// [af] I think each deserialization method should stay here by the Json code, but each 
	// class definition could be in another file sometime (once it's in use by the program).

	class StudentAttendanceInfo
	{
		public int classNumber;
		public int seatNumber;
		public int club;

		public static StudentAttendanceInfo Deserialize(Dictionary<string, object> dict)
		{
			StudentAttendanceInfo attendanceInfo = new StudentAttendanceInfo();
			attendanceInfo.classNumber = TFUtils.LoadInt(dict, "Class");
			attendanceInfo.seatNumber = TFUtils.LoadInt(dict, "Seat");
			attendanceInfo.club = TFUtils.LoadInt(dict, "Club");
			return attendanceInfo;
		}
	}

	class StudentPersonality
	{
		public PersonaType persona;
		public int crush;

		public static StudentPersonality Deserialize(Dictionary<string, object> dict)
		{
			StudentPersonality personality = new StudentPersonality();
			personality.persona = (PersonaType)TFUtils.LoadInt(dict, "Persona");
			personality.crush = TFUtils.LoadInt(dict, "Crush");
			return personality;
		}
	}

	class StudentStats
	{
		public int strength;

		public static StudentStats Deserialize(Dictionary<string, object> dict)
		{
			StudentStats stats = new StudentStats();
			stats.strength = TFUtils.LoadInt(dict, "Strength");
			return stats;
		}
	}

	class StudentCosmetics
	{
		public float breastSize;
		public string hairstyle;
		public string color;
		public string eyes;
		public string stockings;
		public string accessory;

		public static StudentCosmetics Deserialize(Dictionary<string, object> dict)
		{
			StudentCosmetics cosmetics = new StudentCosmetics();
			cosmetics.breastSize = TFUtils.LoadFloat(dict, "BreastSize");
			cosmetics.hairstyle = TFUtils.LoadString(dict, "Hairstyle");
			cosmetics.color = TFUtils.LoadString(dict, "Color");
			cosmetics.eyes = TFUtils.LoadString(dict, "Eyes");
			cosmetics.stockings = TFUtils.LoadString(dict, "Stockings");
			cosmetics.accessory = TFUtils.LoadString(dict, "Accessory");
			return cosmetics;
		}
	}

	class StudentData
	{
		public int id;
		public string name;
		public bool isMale;
		public StudentAttendanceInfo attendanceInfo;
		public StudentPersonality personality;
		public StudentStats stats;
		public StudentCosmetics cosmetics;
		public ScheduleBlock[] scheduleBlocks;
		public string info;

		public static StudentData Deserialize(Dictionary<string, object> dict)
		{
			StudentData data = new StudentData();
			data.id = TFUtils.LoadInt(dict, "ID");
			data.name = TFUtils.LoadString(dict, "Name");
			data.isMale = TFUtils.LoadInt(dict, "Gender") == 1;
			data.attendanceInfo = StudentAttendanceInfo.Deserialize(dict);
			data.personality = StudentPersonality.Deserialize(dict);
			data.stats = StudentStats.Deserialize(dict);
			data.cosmetics = StudentCosmetics.Deserialize(dict);
			data.scheduleBlocks = DeserializeScheduleBlocks(dict);
			data.info = TFUtils.LoadString(dict, "Info");
			return data;
		}
	}

	[SerializeField] UIPanel mainPanel;
	[SerializeField] UIPanel studentPanel;
	[SerializeField] UILabel bodyLabel;
	[SerializeField] Transform listLabelsOrigin;
	[SerializeField] UILabel studentLabelTemplate;

	[SerializeField] PromptBarScript promptBar;

	StudentData[] students;
	int studentIndex;

	InputManagerScript inputManager;

	void Awake()
	{
		// [af] Deserialize Students.json.
		Dictionary<string, object>[] studentsJson =
			EditorManagerScript.DeserializeJson("Students.json");

		// Initialize students array.
		this.students = new StudentData[studentsJson.Length];

		// Deserialize each student from the JSON.
		for (int i = 0; i < this.students.Length; i++)
		{
			this.students[i] = StudentData.Deserialize(studentsJson[i]);
		}

		// Sort students by ID, low to high.
		Array.Sort(this.students, (a, b) => a.id - b.id);

		// Populate the labels list.
		// @todo: Maybe hide each game object when it's out of the panel's range?
		// @todo: Maybe have a "visible labels" List<> that pushes and pops label references
		// when moving up and down the list?
		for (int i = 0; i < this.students.Length; i++)
		{
			StudentData student = this.students[i];

			UILabel studentLabel = Instantiate(this.studentLabelTemplate, this.listLabelsOrigin);
			studentLabel.text = "(" + student.id.ToString() + ") " + student.name;

			Transform labelTransform = studentLabel.transform;
			labelTransform.localPosition = new Vector3(
				labelTransform.localPosition.x + (studentLabel.width / 2),
				labelTransform.localPosition.y - (i * studentLabel.height),
				labelTransform.localPosition.z);

			studentLabel.gameObject.SetActive(true);
		}

		// Select the first student in the list.
		this.studentIndex = 0;
		this.bodyLabel.text = GetStudentText(this.students[this.studentIndex]);

		this.inputManager = FindObjectOfType<InputManagerScript>();
	}

	void OnEnable()
	{
		this.promptBar.Label[0].text = string.Empty;
		this.promptBar.Label[1].text = "Back";
		this.promptBar.UpdateButtons();
	}

	// [af] This should be replaced with DeserializeScheduleBlock() once the .json is
	// redesigned in a newer format, or maybe it should remain and call that function 
	// internally.
	static ScheduleBlock[] DeserializeScheduleBlocks(Dictionary<string, object> dict)
	{
		string[] times = TFUtils.LoadString(dict, "ScheduleTime").Split('_');
		string[] destinations = TFUtils.LoadString(dict, "ScheduleDestination").Split('_');
		string[] actions = TFUtils.LoadString(dict, "ScheduleAction").Split('_');

		Debug.Assert(times.Length == destinations.Length);
		Debug.Assert(times.Length == actions.Length);

		ScheduleBlock[] blocks = new ScheduleBlock[times.Length];

		for (int i = 0; i < blocks.Length; i++)
		{
			blocks[i] = new ScheduleBlock(float.Parse(times[i]), destinations[i], actions[i]);
		}

		return blocks;
	}

	// [af] Convert a student's data into readable text.
	static string GetStudentText(StudentData data)
	{
		StringBuilder sb = new StringBuilder();
		sb.Append(data.name + " (" + data.id + "):\n");
		sb.Append("- Gender: " + (data.isMale ? "Male" : "Female") + "\n");
		sb.Append("- Class: " + data.attendanceInfo.classNumber + "\n");
		sb.Append("- Seat: " + data.attendanceInfo.seatNumber + "\n");
		sb.Append("- Club: " + data.attendanceInfo.club + "\n");
		sb.Append("- Persona: " + data.personality.persona + "\n");
		sb.Append("- Crush: " + data.personality.crush + "\n");
		sb.Append("- Breast size: " + data.cosmetics.breastSize + "\n");
		sb.Append("- Strength: " + data.stats.strength + "\n");
		sb.Append("- Hairstyle: " + data.cosmetics.hairstyle + "\n");
		sb.Append("- Color: " + data.cosmetics.color + "\n");
		sb.Append("- Eyes: " + data.cosmetics.eyes + "\n");
		sb.Append("- Stockings: " + data.cosmetics.stockings + "\n");
		sb.Append("- Accessory: " + data.cosmetics.accessory + "\n");
		sb.Append("- Schedule blocks: ");

		foreach (ScheduleBlock block in data.scheduleBlocks)
		{
			sb.Append("[" + block.time + ", " + block.destination + ", " + block.action + "]");
		}

		sb.Append("\n");

		sb.Append("- Info: \"" + data.info + "\"\n");
		return sb.ToString();
	}

	void HandleInput()
	{
		bool back = Input.GetButtonDown(InputNames.Xbox_B);

		if (back)
		{
			this.mainPanel.gameObject.SetActive(true);
			this.studentPanel.gameObject.SetActive(false);
		}

		int minIndex = 0;
		int maxIndex = this.students.Length - 1;

		bool moveUp = this.inputManager.TappedUp;
		bool moveDown = this.inputManager.TappedDown;

		if (moveUp)
		{
			this.studentIndex = (this.studentIndex > minIndex) ? (this.studentIndex - 1) : maxIndex;
		}
		else if (moveDown)
		{
			this.studentIndex = (this.studentIndex < maxIndex) ? (this.studentIndex + 1) : minIndex;
		}

		bool selectionChanged = moveUp || moveDown;

		if (selectionChanged)
		{
			this.bodyLabel.text = GetStudentText(this.students[this.studentIndex]);
		}
	}

	void Update()
	{
		this.HandleInput();
	}
}
