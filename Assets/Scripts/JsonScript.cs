using JsonFx.Json;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

// [af] Just for reference, TFUtils is in Standard Assets.

public abstract class JsonData
{
	protected static string FolderPath
	{
		get { return Path.Combine(Application.streamingAssetsPath, "JSON"); }
	}

	protected static Dictionary<string, object>[] Deserialize(string filename)
	{
		string json = File.ReadAllText(filename);
		return JsonReader.Deserialize<Dictionary<string, object>[]>(json);
	}
}

[System.Serializable]
public class StudentJson : JsonData
{
	[SerializeField] string name;
    [SerializeField] string realname;
    [SerializeField] int gender;
	[SerializeField] int classID; // [af] Renamed to avoid "class" keyword.
	[SerializeField] int seat;
	[SerializeField] ClubType club;
	[SerializeField] PersonaType persona;
	[SerializeField] int crush;
	[SerializeField] float breastSize;
	[SerializeField] int strength;
	[SerializeField] string hairstyle;
	[SerializeField] string color;
	[SerializeField] string eyes;
	[SerializeField] string eyeType;
	[SerializeField] string stockings;
	[SerializeField] string accessory;
	[SerializeField] string info;
	[SerializeField] ScheduleBlock[] scheduleBlocks;
	[SerializeField] bool success;

	public static string FilePath
	{
		get { return Path.Combine(FolderPath, "Students.json"); }
	}

	public static StudentJson[] LoadFromJson(string path)
	{
		const int studentCount = 101;
		StudentJson[] studentObjects = new StudentJson[studentCount];

		for (int i = 0; i < studentObjects.Length; i++)
		{
			studentObjects[i] = new StudentJson();
		}

		foreach (Dictionary<string, object> dict in Deserialize(path))
		{
			int ID = TFUtils.LoadInt(dict, "ID");

			if (ID == 0)
			{
				break;
			}

			//Debug.Log("JSON is currently looking at the data for Student " + ID + ".");

			StudentJson student = studentObjects[ID];
			student.name = TFUtils.LoadString(dict, "Name");
            student.realname = TFUtils.LoadString(dict, "RealName");
            student.gender = TFUtils.LoadInt(dict, "Gender");
			student.classID = TFUtils.LoadInt(dict, "Class");
			student.seat = TFUtils.LoadInt(dict, "Seat");
			student.club = (ClubType)TFUtils.LoadInt(dict, "Club");
			student.persona = (PersonaType)TFUtils.LoadInt(dict, "Persona");
			student.crush = TFUtils.LoadInt(dict, "Crush");
			student.breastSize = TFUtils.LoadFloat(dict, "BreastSize");
			student.strength = TFUtils.LoadInt(dict, "Strength");
			student.hairstyle = TFUtils.LoadString(dict, "Hairstyle");
			student.color = TFUtils.LoadString(dict, "Color");
			student.eyes = TFUtils.LoadString(dict, "Eyes");
			student.eyeType = TFUtils.LoadString(dict, "EyeType");
			student.stockings = TFUtils.LoadString(dict, "Stockings");
			student.accessory = TFUtils.LoadString(dict, "Accessory");
			student.info = TFUtils.LoadString(dict, "Info");

			//No joke names in LoveSick mode.
			if (GameGlobals.LoveSick)
			{
                student.name = student.realname;
                student.realname = "";
			}

			// [af] Replace empty slots with random students.
			if (OptionGlobals.HighPopulation && (student.name == "Unknown"))
			{
				student.name = "Random";
			}

			float[] times = ConstructTempFloatArray(
				TFUtils.LoadString(dict, "ScheduleTime"));
			string[] destinations = ConstructTempStringArray(
				TFUtils.LoadString(dict, "ScheduleDestination"));
			string[] actions = ConstructTempStringArray(
				TFUtils.LoadString(dict, "ScheduleAction"));

			student.scheduleBlocks = new ScheduleBlock[times.Length];

			for (int i = 0; i < student.scheduleBlocks.Length; i++)
			{
				student.scheduleBlocks[i] = new ScheduleBlock(
					times[i], destinations[i], actions[i]);
			}

			//Anti-Osana Code
			#if !UNITY_EDITOR
			if (ID == 10 || ID == 11)
			{
				for (int i = 0; i < student.scheduleBlocks.Length; i++)
				{
					student.scheduleBlocks[i] = null;
				}
			}
			#endif

			student.success = true;
		}

		return studentObjects;
	}

	// [af] Some properties are accessors because they are modified elsewhere. For all 
	// intents and purposes, the accessors should be considered exceptions because the 
	// JSON data should remain immutable (it is meant to be a 1-to-1 representation of 
	// the .json file on the hard drive).
	public string Name { get { return this.name; } set { this.name = value; } }
    public string RealName { get { return this.realname; } set { this.realname = value; } }
    public int Gender { get { return this.gender; } }
	public int Class { get { return this.classID; } set { this.classID = value; } }
	public int Seat { get { return this.seat; } set { this.seat = value; } }
	public ClubType Club { get { return this.club; } }
	public PersonaType Persona { get { return this.persona; } set { this.persona = value; } }
	public int Crush { get { return this.crush; } }
	public float BreastSize { get { return this.breastSize; } set { this.breastSize = value; } }
	public int Strength { get { return this.strength; } set { this.strength = value; } }
	public string Hairstyle { get { return this.hairstyle; } set { this.hairstyle = value; } }
	public string Color { get { return this.color; } }
	public string Eyes { get { return this.eyes; } }
	public string EyeType { get { return this.eyeType; } }
	public string Stockings { get { return this.stockings; } }
	public string Accessory { get { return this.accessory; } set { this.accessory = value; } }
	public string Info { get { return this.info; } }
	public ScheduleBlock[] ScheduleBlocks { get { return this.scheduleBlocks; } }
	public bool Success { get { return this.success; } }

    // [af] Helper method for splitting a string into an array of floats.
    private static float[] ConstructTempFloatArray(string str)
    {
        string[] array = str.Split(new char[]
        {
            '_'
        });
        float[] array2 = new float[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            float result;
            if (float.TryParse(array[i], NumberStyles.Float, NumberFormatInfo.InvariantInfo, out result))
            {
                array2[i] = result;
            }
            else
            {
                //Debugger.Break();
            }
        }
        return array2;
    }

    // [af] Helper method for splitting a string into a list of strings.
    static string[] ConstructTempStringArray(string str)
	{
		return str.Split('_');
	}
}

[System.Serializable]
public class CreditJson : JsonData
{
	[SerializeField] string name;
	[SerializeField] int size;

	public static string FilePath
	{
		get { return Path.Combine(FolderPath, "Credits.json"); }
	}

	public static CreditJson[] LoadFromJson(string path)
	{
		List<CreditJson> creditObjects = new List<CreditJson>();

		foreach (Dictionary<string, object> dict in Deserialize(path))
		{
			CreditJson credit = new CreditJson();
			credit.name = TFUtils.LoadString(dict, "Name");
			credit.size = TFUtils.LoadInt(dict, "Size");

			creditObjects.Add(credit);
		}

		return creditObjects.ToArray();
	}

	public string Name
	{
		get { return this.name; }
	}

	public int Size
	{
		get { return this.size; }
	}
}

[System.Serializable]
public class TopicJson : JsonData
{
	[SerializeField] int[] topics;

	public static string FilePath
	{
		get { return Path.Combine(FolderPath, "Topics.json"); }
	}

	public static TopicJson[] LoadFromJson(string path)
	{
		const int studentCount = 101;
		TopicJson[] topicObjects = new TopicJson[studentCount];

		foreach (Dictionary<string, object> dict in Deserialize(path))
		{
			int ID = TFUtils.LoadInt(dict, "ID");

			if (ID == 0)
			{
				break;
			}

			topicObjects[ID] = new TopicJson();
			TopicJson topicObject = topicObjects[ID];

			const int topicCount = 26;
			topicObject.topics = new int[topicCount];

			// [af] Load all topics (1 through 25).
			for (int i = 1; i <= 25; i++)
			{
				topicObject.topics[i] = TFUtils.LoadInt(dict, i.ToString());
			}
		}

		return topicObjects;
	}

	public int[] Topics
	{
		get { return this.topics; }
	}
}

public class JsonScript : MonoBehaviour
{
	// [af] Array of student records for each student.
	[SerializeField] StudentJson[] students;

	// [af] Array of credit records for each line of text.
	[SerializeField] CreditJson[] credits;

	// [af] Array of topic records for each student.
	[SerializeField] TopicJson[] topics;

	void Start()
	{
		// [af] Always load student data.
		this.students = StudentJson.LoadFromJson(StudentJson.FilePath);

		// [af] Always load topics data.
		this.topics = TopicJson.LoadFromJson(TopicJson.FilePath);

		if (SceneManager.GetActiveScene().name == SceneNames.SchoolScene)
		{
			StudentManagerScript studentManager = FindObjectOfType<StudentManagerScript>();
			this.ReplaceDeadTeachers(studentManager.FirstNames, studentManager.LastNames);
		}
		else if (SceneManager.GetActiveScene().name == SceneNames.CreditsScene)
		{
			// [af] Load credits data when in the credits scene.
			this.credits = CreditJson.LoadFromJson(CreditJson.FilePath);
		}
	}

	public StudentJson[] Students
	{
		get { return this.students; }
	}

	public CreditJson[] Credits
	{
		get { return this.credits; }
	}

	public TopicJson[] Topics
	{
		get { return this.topics; }
	}

	void ReplaceDeadTeachers(string[] firstNames, string[] lastNames)
	{
		// [af] Converted while loop to for loop.
		for (int ID = 90; ID < 101; ID++)
		{
			if (StudentGlobals.GetStudentDead(ID))
			{
				StudentGlobals.SetStudentReplaced(ID, true);
				StudentGlobals.SetStudentDead(ID, false);

				string NewName = firstNames[Random.Range(0, firstNames.Length)] + " " +
					lastNames[Random.Range(0, lastNames.Length)];
				StudentGlobals.SetStudentName(ID, NewName);

				StudentGlobals.SetStudentBustSize(ID, Random.Range(1.0f, 1.50f));
				StudentGlobals.SetStudentHairstyle(ID, Random.Range(1, 8).ToString());

				float R = Random.Range(0.0f, 1.0f);
				float G = Random.Range(0.0f, 1.0f);
				float B = Random.Range(0.0f, 1.0f);
				StudentGlobals.SetStudentColor(ID, new Color(R, G, B));

				R = Random.Range(0.0f, 1.0f);
				G = Random.Range(0.0f, 1.0f);
				B = Random.Range(0.0f, 1.0f);
				StudentGlobals.SetStudentEyeColor(ID, new Color(R, G, B));

				StudentGlobals.SetStudentAccessory(ID, Random.Range(1, 7).ToString());
			}
		}

		// [af] Converted while loop to for loop.
		for (int ID = 90; ID < 101; ID++)
		{
			if (StudentGlobals.GetStudentReplaced(ID))
			{
				StudentJson student = this.students[ID];
				student.Name = StudentGlobals.GetStudentName(ID);
				student.BreastSize = StudentGlobals.GetStudentBustSize(ID);
				student.Hairstyle = StudentGlobals.GetStudentHairstyle(ID);
				student.Accessory = StudentGlobals.GetStudentAccessory(ID);

				// Give the gym teacher a whistle.
				if (ID == 97)
				{
					student.Accessory = "7";
				}

				// Give the nurse a nurse hat.
				if (ID == 90)
				{
					student.Accessory = "8";
				}
			}
		}
	}
}
