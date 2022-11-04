using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

// [af] The things we want to save go in this class. These should be accessible to the 
// rest of the program. Remember to add new SaveFileData members to SaveFile.ReadFromGlobals()
// and WriteToGlobals().
[Serializable]
[XmlRoot]
public class SaveFileData
{
	public ApplicationSaveData applicationData = new ApplicationSaveData();
	public ClassSaveData classData = new ClassSaveData();
	public ClubSaveData clubData = new ClubSaveData();
	public CollectibleSaveData collectibleData = new CollectibleSaveData();
	public ConversationSaveData conversationData = new ConversationSaveData();
	public DateSaveData dateData = new DateSaveData();
	public DatingSaveData datingData = new DatingSaveData();
	public EventSaveData eventData = new EventSaveData();
	public GameSaveData gameData = new GameSaveData();
	public HomeSaveData homeData = new HomeSaveData();
	public MissionModeSaveData missionModeData = new MissionModeSaveData();
	public OptionSaveData optionData = new OptionSaveData();
	public PlayerSaveData playerData = new PlayerSaveData();
	public PoseModeSaveData poseModeData = new PoseModeSaveData();
	public SaveFileSaveData saveFileData = new SaveFileSaveData();
	public SchemeSaveData schemeData = new SchemeSaveData();
	public SchoolSaveData schoolData = new SchoolSaveData();
	public SenpaiSaveData senpaiData = new SenpaiSaveData();
	public StudentSaveData studentData = new StudentSaveData();
	public TaskSaveData taskData = new TaskSaveData();
	public YanvaniaSaveData yanvaniaData = new YanvaniaSaveData();
}

// [af] This class is the interface between the program and the saves files on the
// hard drive.
[Serializable]
public class SaveFile
{
	[SerializeField] SaveFileData data;
	[SerializeField] int index;

	// Constructor for creating a new save file. Use this when the corresponding save
	// file index is not present on disk.
	public SaveFile(int index)
	{
		this.data = new SaveFileData();
		this.index = index;
	}

	// Internal constructor for Load().
	SaveFile(SaveFileData data, int index)
	{
		this.data = data;
		this.index = index;
	}

	public SaveFileData Data
	{
		get { return this.data; }
	}

	static readonly string SavesPath = Path.Combine(Application.persistentDataPath, "Saves");
	static readonly string SaveName = "Save.txt";

	// The indexed save folder path is public so that other parts of the code can save things 
	// (such as corkboard images) into it.
	public static string GetSaveFolderPath(int index)
	{
		return Path.Combine(SavesPath, "Save" + index.ToString());
	}

	// Gets the full path + filename of a save file given an index.
	static string GetFullSaveFileName(int index)
	{
		return Path.Combine(GetSaveFolderPath(index), SaveName);
	}

	// Returns whether the parent saves folder exists.
	static bool SavesFolderExists
	{
		get { return Directory.Exists(SavesPath); }
	}

	// Returns whether the indexed save folder exists on the file system. Any code
	// that writes files to this folder should call this function first.
	public static bool SaveFolderExists(int index)
	{
		return Directory.Exists(GetSaveFolderPath(index));
	}

	// Checks for the existence of a particular save file. The program can use this
	// to check if one exists, so it can say whether a save slot is empty or not.
	public static bool Exists(int index)
	{
		return File.Exists(GetFullSaveFileName(index));
	}

	// Loads a particular save file from the save folder. If the file cannot be
	// loaded for some reason, then null is returned and it should be considered
	// an error (i.e., the file exists but it's in a bad state).
	public static SaveFile Load(int index)
	{
		try
		{
			// Get all the text from the file.
			string xmlText = File.ReadAllText(GetFullSaveFileName(index));

			// Deserialize save file data.
			XmlSerializer xml = new XmlSerializer(typeof(SaveFileData));
			MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlText));
			SaveFileData saveFileData = (SaveFileData)xml.Deserialize(stream);

			return new SaveFile(saveFileData, index);
		}
		catch (Exception e)
		{
			// If something bad happens, just return null and the program can
			// decide what to do (like display "Cannot load save file").
			Debug.LogError("Loading save file " + index.ToString() + " failed (" +
				e.ToString() + ").");
			return null;
		}
	}

	// Deletes a save file.
	public static void Delete(int index)
	{
		try
		{
			string saveFileName = GetFullSaveFileName(index);
			File.Delete(saveFileName);
		}
		catch (Exception e)
		{
			Debug.LogError("Deleting save file " + index.ToString() + " failed (" +
				e.ToString() + ").");
		}
	}

	// Saves the save file, overwriting any existing one on disk with the same index.
	public void Save()
	{
		try
		{
			// Create the root saves folder if it doesn't exist.
			if (!SavesFolderExists)
			{
				Directory.CreateDirectory(SavesPath);
			}

			// Create the indexed folder associated with this save if it doesn't exist.
			if (!SaveFolderExists(this.index))
			{
				Directory.CreateDirectory(GetSaveFolderPath(this.index));
			}

			string saveFileName = GetFullSaveFileName(this.index);
			if (!Exists(this.index))
			{
				FileStream fileStream = File.Create(saveFileName);

				// Let go of the file handle immediately so the XML writer can use it.
				fileStream.Dispose();
			}

			XmlSerializer xml = new XmlSerializer(typeof(SaveFileData));
			XmlWriterSettings writerSettings = new XmlWriterSettings();
			writerSettings.Indent = true;
			writerSettings.IndentChars = "\t";

			using (XmlWriter writer = XmlWriter.Create(saveFileName, writerSettings))
			{
				xml.Serialize(writer, this.data);
			}
		}
		catch (Exception e)
		{
			Debug.LogError("Saving save file " + this.index.ToString() + " failed (" +
				e.ToString() + ").");
		}
	}

	// Reads all global variables that have a matching value in the save file.
	public void ReadFromGlobals()
	{
		this.data.applicationData = ApplicationSaveData.ReadFromGlobals();
		this.data.classData = ClassSaveData.ReadFromGlobals();
		this.data.clubData = ClubSaveData.ReadFromGlobals();
		this.data.collectibleData = CollectibleSaveData.ReadFromGlobals();
		this.data.conversationData = ConversationSaveData.ReadFromGlobals();
		this.data.dateData = DateSaveData.ReadFromGlobals();
		this.data.datingData = DatingSaveData.ReadFromGlobals();
		this.data.eventData = EventSaveData.ReadFromGlobals();
		this.data.gameData = GameSaveData.ReadFromGlobals();
		this.data.homeData = HomeSaveData.ReadFromGlobals();
		this.data.missionModeData = MissionModeSaveData.ReadFromGlobals();
		this.data.optionData = OptionSaveData.ReadFromGlobals();
		this.data.playerData = PlayerSaveData.ReadFromGlobals();
		this.data.poseModeData = PoseModeSaveData.ReadFromGlobals();
		this.data.saveFileData = SaveFileSaveData.ReadFromGlobals();
		this.data.schemeData = SchemeSaveData.ReadFromGlobals();
		this.data.schoolData = SchoolSaveData.ReadFromGlobals();
		this.data.senpaiData = SenpaiSaveData.ReadFromGlobals();
		this.data.studentData = StudentSaveData.ReadFromGlobals();
		this.data.taskData = TaskSaveData.ReadFromGlobals();
		this.data.yanvaniaData = YanvaniaSaveData.ReadFromGlobals();
	}

	// Assigns all save file variables to their matching global variables.
	public void WriteToGlobals()
	{
		ApplicationSaveData.WriteToGlobals(this.data.applicationData);
		ClassSaveData.WriteToGlobals(this.data.classData);
		ClubSaveData.WriteToGlobals(this.data.clubData);
		CollectibleSaveData.WriteToGlobals(this.data.collectibleData);
		ConversationSaveData.WriteToGlobals(this.data.conversationData);
		DateSaveData.WriteToGlobals(this.data.dateData);
		DatingSaveData.WriteToGlobals(this.data.datingData);
		EventSaveData.WriteToGlobals(this.data.eventData);
		GameSaveData.WriteToGlobals(this.data.gameData);
		HomeSaveData.WriteToGlobals(this.data.homeData);
		MissionModeSaveData.WriteToGlobals(this.data.missionModeData);
		OptionSaveData.WriteToGlobals(this.data.optionData);
		PlayerSaveData.WriteToGlobals(this.data.playerData);
		PoseModeSaveData.WriteToGlobals(this.data.poseModeData);
		SaveFileSaveData.WriteToGlobals(this.data.saveFileData);
		SchemeSaveData.WriteToGlobals(this.data.schemeData);
		SchoolSaveData.WriteToGlobals(this.data.schoolData);
		SenpaiSaveData.WriteToGlobals(this.data.senpaiData);
		StudentSaveData.WriteToGlobals(this.data.studentData);
		TaskSaveData.WriteToGlobals(this.data.taskData);
		YanvaniaSaveData.WriteToGlobals(this.data.yanvaniaData);
	}
}
