using System;
using UnityEngine;

// [af] Various data types used with the save files and global data. They are stored
// in the save file, and are loaded when the save file is deserialized.

[Serializable]
public class ApplicationSaveData
{
	public float versionNumber = 0.0f;

	public static ApplicationSaveData ReadFromGlobals()
	{
		ApplicationSaveData data = new ApplicationSaveData();
		data.versionNumber = ApplicationGlobals.VersionNumber;
		return data;
	}

	public static void WriteToGlobals(ApplicationSaveData data)
	{
		ApplicationGlobals.VersionNumber = data.versionNumber;
	}
}

[Serializable]
public class ClassSaveData
{
	public int biology = 0;
	public int biologyBonus = 0;
	public int biologyGrade = 0;
	public int chemistry = 0;
	public int chemistryBonus = 0;
	public int chemistryGrade = 0;
	public int language = 0;
	public int languageBonus = 0;
	public int languageGrade = 0;
	public int physical = 0;
	public int physicalBonus = 0;
	public int physicalGrade = 0;
	public int psychology = 0;
	public int psychologyBonus = 0;
	public int psychologyGrade = 0;

	public static ClassSaveData ReadFromGlobals()
	{
		ClassSaveData data = new ClassSaveData();
		data.biology = ClassGlobals.Biology;
		data.biologyBonus = ClassGlobals.BiologyBonus;
		data.biologyGrade = ClassGlobals.BiologyGrade;
		data.chemistry = ClassGlobals.Chemistry;
		data.chemistryBonus = ClassGlobals.ChemistryBonus;
		data.chemistryGrade = ClassGlobals.ChemistryGrade;
		data.language = ClassGlobals.Language;
		data.languageBonus = ClassGlobals.LanguageBonus;
		data.languageGrade = ClassGlobals.LanguageGrade;
		data.physical = ClassGlobals.Physical;
		data.physicalBonus = ClassGlobals.PhysicalBonus;
		data.physicalGrade = ClassGlobals.PhysicalGrade;
		data.psychology = ClassGlobals.Psychology;
		data.psychologyBonus = ClassGlobals.PsychologyBonus;
		data.psychologyGrade = ClassGlobals.PsychologyGrade;
		return data;
	}

	public static void WriteToGlobals(ClassSaveData data)
	{
		ClassGlobals.Biology = data.biology;
		ClassGlobals.BiologyBonus = data.biologyBonus;
		ClassGlobals.BiologyGrade = data.biologyGrade;
		ClassGlobals.Chemistry = data.chemistry;
		ClassGlobals.ChemistryBonus = data.chemistryBonus;
		ClassGlobals.ChemistryGrade = data.chemistryGrade;
		ClassGlobals.Language = data.language;
		ClassGlobals.LanguageBonus = data.languageBonus;
		ClassGlobals.LanguageGrade = data.languageGrade;
		ClassGlobals.Physical = data.physical;
		ClassGlobals.PhysicalBonus = data.physicalBonus;
		ClassGlobals.PhysicalGrade = data.physicalGrade;
		ClassGlobals.Psychology = data.psychology;
		ClassGlobals.PsychologyBonus = data.psychologyBonus;
		ClassGlobals.PsychologyGrade = data.psychologyGrade;
	}
}

[Serializable]
public class ClubSaveData
{
	public ClubType club = ClubType.None;
	public ClubTypeHashSet clubClosed = new ClubTypeHashSet();
	public ClubTypeHashSet clubKicked = new ClubTypeHashSet();
	public ClubTypeHashSet quitClub = new ClubTypeHashSet();

	public static ClubSaveData ReadFromGlobals()
	{
		ClubSaveData data = new ClubSaveData();
		data.club = ClubGlobals.Club;

		foreach (ClubType value in ClubGlobals.KeysOfClubClosed())
		{
			if (ClubGlobals.GetClubClosed(value))
			{
				data.clubClosed.Add(value);
			}
		}

		foreach (ClubType value in ClubGlobals.KeysOfClubKicked())
		{
			if (ClubGlobals.GetClubKicked(value))
			{
				data.clubKicked.Add(value);
			}
		}

		foreach (ClubType value in ClubGlobals.KeysOfQuitClub())
		{
			if (ClubGlobals.GetQuitClub(value))
			{
				data.quitClub.Add(value);
			}
		}

		return data;
	}

	public static void WriteToGlobals(ClubSaveData data)
	{
		ClubGlobals.Club = data.club;

		foreach (ClubType value in data.clubClosed)
		{
			ClubGlobals.SetClubClosed(value, true);
		}

		foreach (ClubType value in data.clubKicked)
		{
			ClubGlobals.SetClubKicked(value, true);
		}

		foreach (ClubType value in data.quitClub)
		{
			ClubGlobals.SetQuitClub(value, true);
		}
	}
}

[Serializable]
public class CollectibleSaveData
{
	public IntHashSet basementTapeCollected = new IntHashSet();
	public IntHashSet basementTapeListened = new IntHashSet();
	public IntHashSet mangaCollected = new IntHashSet();
	public IntHashSet tapeCollected = new IntHashSet();
	public IntHashSet tapeListened = new IntHashSet();

	public static CollectibleSaveData ReadFromGlobals()
	{
		CollectibleSaveData data = new CollectibleSaveData();

		foreach (int value in CollectibleGlobals.KeysOfBasementTapeCollected())
		{
			if (CollectibleGlobals.GetBasementTapeCollected(value))
			{
				data.basementTapeCollected.Add(value);
			}
		}

		foreach (int value in CollectibleGlobals.KeysOfBasementTapeListened())
		{
			if (CollectibleGlobals.GetBasementTapeListened(value))
			{
				data.basementTapeListened.Add(value);
			}
		}

		foreach (int value in CollectibleGlobals.KeysOfMangaCollected())
		{
			if (CollectibleGlobals.GetMangaCollected(value))
			{
				data.mangaCollected.Add(value);
			}
		}

		foreach (int value in CollectibleGlobals.KeysOfTapeCollected())
		{
			if (CollectibleGlobals.GetTapeCollected(value))
			{
				data.tapeCollected.Add(value);
			}
		}

		foreach (int value in CollectibleGlobals.KeysOfTapeListened())
		{
			if (CollectibleGlobals.GetTapeListened(value))
			{
				data.tapeListened.Add(value);
			}
		}

		return data;
	}

	public static void WriteToGlobals(CollectibleSaveData data)
	{
		foreach (int value in data.basementTapeCollected)
		{
			CollectibleGlobals.SetBasementTapeCollected(value, true);
		}

		foreach (int value in data.basementTapeListened)
		{
			CollectibleGlobals.SetBasementTapeListened(value, true);
		}

		foreach (int value in data.mangaCollected)
		{
			CollectibleGlobals.SetMangaCollected(value, true);
		}

		foreach (int value in data.tapeCollected)
		{
			CollectibleGlobals.SetTapeCollected(value, true);
		}

		foreach (int value in data.tapeListened)
		{
			CollectibleGlobals.SetTapeListened(value, true);
		}
	}
}

[Serializable]
public class ConversationSaveData
{
	public IntHashSet topicDiscovered = new IntHashSet();
	public IntAndIntPairHashSet topicLearnedByStudent = new IntAndIntPairHashSet();

	public static ConversationSaveData ReadFromGlobals()
	{
		ConversationSaveData data = new ConversationSaveData();

		foreach (int value in ConversationGlobals.KeysOfTopicDiscovered())
		{
			if (ConversationGlobals.GetTopicDiscovered(value))
			{
				data.topicDiscovered.Add(value);
			}
		}

		foreach (var pair in ConversationGlobals.KeysOfTopicLearnedByStudent())
		{
			if (ConversationGlobals.GetTopicLearnedByStudent(pair.first, pair.second))
			{
				data.topicLearnedByStudent.Add(pair);
			}
		}

		return data;
	}

	public static void WriteToGlobals(ConversationSaveData data)
	{
		foreach (int value in data.topicDiscovered)
		{
			ConversationGlobals.SetTopicDiscovered(value, true);
		}

		foreach (var pair in data.topicLearnedByStudent)
		{
			ConversationGlobals.SetTopicLearnedByStudent(pair.first, pair.second, true);
		}
	}
}

[Serializable]
public class DateSaveData
{
	public int week = 0;
	public DayOfWeek weekday = DayOfWeek.Sunday;

	public static DateSaveData ReadFromGlobals()
	{
		DateSaveData data = new DateSaveData();
		data.week = DateGlobals.Week;
		data.weekday = DateGlobals.Weekday;
		return data;
	}

	public static void WriteToGlobals(DateSaveData data)
	{
		DateGlobals.Week = data.week;
		DateGlobals.Weekday = data.weekday;
	}
}

[Serializable]
public class DatingSaveData
{
	public float affection = 0.0f;
	public float affectionLevel = 0.0f;
	public IntHashSet complimentGiven = new IntHashSet();
	public IntHashSet suitorCheck = new IntHashSet();
	public int suitorProgress = 0;
	public IntAndIntDictionary suitorTrait = new IntAndIntDictionary();
	public IntHashSet topicDiscussed = new IntHashSet();
	public IntAndIntDictionary traitDemonstrated = new IntAndIntDictionary();

	public static DatingSaveData ReadFromGlobals()
	{
		DatingSaveData data = new DatingSaveData();
		data.affection = DatingGlobals.Affection;
		data.affectionLevel = DatingGlobals.AffectionLevel;

		foreach (int value in DatingGlobals.KeysOfComplimentGiven())
		{
			if (DatingGlobals.GetComplimentGiven(value))
			{
				data.complimentGiven.Add(value);
			}
		}

		foreach (int value in DatingGlobals.KeysOfSuitorCheck())
		{
			if (DatingGlobals.GetSuitorCheck(value))
			{
				data.suitorCheck.Add(value);
			}
		}

		data.suitorProgress = DatingGlobals.SuitorProgress;

		foreach (int value in DatingGlobals.KeysOfSuitorTrait())
		{
			data.suitorTrait.Add(value, DatingGlobals.GetSuitorTrait(value));
		}

		foreach (int value in DatingGlobals.KeysOfTopicDiscussed())
		{
			if (DatingGlobals.GetTopicDiscussed(value))
			{
				data.topicDiscussed.Add(value);
			}
		}

		foreach (int value in DatingGlobals.KeysOfTraitDemonstrated())
		{
			data.traitDemonstrated.Add(value, DatingGlobals.GetTraitDemonstrated(value));
		}

		return data;
	}

	public static void WriteToGlobals(DatingSaveData data)
	{
		DatingGlobals.Affection = data.affection;
		DatingGlobals.AffectionLevel = data.affectionLevel;

		foreach (int value in data.complimentGiven)
		{
			DatingGlobals.SetComplimentGiven(value, true);
		}

		foreach (int value in data.suitorCheck)
		{
			DatingGlobals.SetSuitorCheck(value, true);
		}

		DatingGlobals.SuitorProgress = data.suitorProgress;

		foreach (var pair in data.suitorTrait)
		{
			DatingGlobals.SetSuitorTrait(pair.Key, pair.Value);
		}

		foreach (int value in data.topicDiscussed)
		{
			DatingGlobals.SetTopicDiscussed(value, true);
		}

		foreach (var pair in data.traitDemonstrated)
		{
			DatingGlobals.SetTraitDemonstrated(pair.Key, pair.Value);
		}
	}
}

[Serializable]
public class EventSaveData
{
	public bool befriendConversation = false;
	public bool event1 = false;
	public bool event2 = false;
	public bool kidnapConversation = false;
	public bool livingRoom = false;

	public static EventSaveData ReadFromGlobals()
	{
		EventSaveData data = new EventSaveData();
		data.befriendConversation = EventGlobals.BefriendConversation;
		data.event1 = EventGlobals.Event1;
		data.event2 = EventGlobals.Event2;
		data.kidnapConversation = EventGlobals.KidnapConversation;
		data.livingRoom = EventGlobals.LivingRoom;
		return data;
	}

	public static void WriteToGlobals(EventSaveData data)
	{
		EventGlobals.BefriendConversation = data.befriendConversation;
		EventGlobals.Event1 = data.event1;
		EventGlobals.Event2 = data.event2;
		EventGlobals.KidnapConversation = data.kidnapConversation;
		EventGlobals.LivingRoom = data.livingRoom;
	}
}

[Serializable]
public class GameSaveData
{
	public bool loveSick = false;
	public bool masksBanned = false;
	public bool paranormal = false;

	public static GameSaveData ReadFromGlobals()
	{
		GameSaveData data = new GameSaveData();
		data.loveSick = GameGlobals.LoveSick;
		data.masksBanned = GameGlobals.MasksBanned;
		data.paranormal = GameGlobals.Paranormal;
		return data;
	}

	public static void WriteToGlobals(GameSaveData data)
	{
		GameGlobals.LoveSick = data.loveSick;
		GameGlobals.MasksBanned = data.masksBanned;
		GameGlobals.Paranormal = data.paranormal;
	}
}

[Serializable]
public class HomeSaveData
{
	public bool lateForSchool = false;
	public bool night = false;
	public bool startInBasement = false;

	public static HomeSaveData ReadFromGlobals()
	{
		HomeSaveData data = new HomeSaveData();
		data.lateForSchool = HomeGlobals.LateForSchool;
		data.night = HomeGlobals.Night;
		data.startInBasement = HomeGlobals.StartInBasement;
		return data;
	}

	public static void WriteToGlobals(HomeSaveData data)
	{
		HomeGlobals.LateForSchool = data.lateForSchool;
		HomeGlobals.Night = data.night;
		HomeGlobals.StartInBasement = data.startInBasement;
	}
}

[Serializable]
public class MissionModeSaveData
{
	public IntAndIntDictionary missionCondition = new IntAndIntDictionary();
	public int missionDifficulty = 0;
	public bool missionMode = false;
	public int missionRequiredClothing = 0;
	public int missionRequiredDisposal = 0;
	public int missionRequiredWeapon = 0;
	public int missionTarget = 0;
	public string missionTargetName = string.Empty;
	public int nemesisDifficulty = 0;

	public static MissionModeSaveData ReadFromGlobals()
	{
		MissionModeSaveData data = new MissionModeSaveData();

		foreach (int value in MissionModeGlobals.KeysOfMissionCondition())
		{
			data.missionCondition.Add(value, MissionModeGlobals.GetMissionCondition(value));
		}

		data.missionDifficulty = MissionModeGlobals.MissionDifficulty;
		data.missionMode = MissionModeGlobals.MissionMode;
		data.missionRequiredClothing = MissionModeGlobals.MissionRequiredClothing;
		data.missionRequiredDisposal = MissionModeGlobals.MissionRequiredDisposal;
		data.missionRequiredWeapon = MissionModeGlobals.MissionRequiredWeapon;
		data.missionTarget = MissionModeGlobals.MissionTarget;
		data.missionTargetName = MissionModeGlobals.MissionTargetName;
		data.nemesisDifficulty = MissionModeGlobals.NemesisDifficulty;
		return data;
	}

	public static void WriteToGlobals(MissionModeSaveData data)
	{
		foreach (var pair in data.missionCondition)
		{
			MissionModeGlobals.SetMissionCondition(pair.Key, pair.Value);
		}

		MissionModeGlobals.MissionDifficulty = data.missionDifficulty;
		MissionModeGlobals.MissionMode = data.missionMode;
		MissionModeGlobals.MissionRequiredClothing = data.missionRequiredClothing;
		MissionModeGlobals.MissionRequiredDisposal = data.missionRequiredDisposal;
		MissionModeGlobals.MissionRequiredWeapon = data.missionRequiredWeapon;
		MissionModeGlobals.MissionTarget = data.missionTarget;
		MissionModeGlobals.MissionTargetName = data.missionTargetName;
		MissionModeGlobals.NemesisDifficulty = data.nemesisDifficulty;
	}
}

[Serializable]
public class OptionSaveData
{
	public bool disableBloom = false;
	public int disableFarAnimations = 5;
	public bool disableOutlines = false;
	public bool disablePostAliasing = false;
	public bool enableShadows = false;
	public int drawDistance = 0;
	public int drawDistanceLimit = 0;
	public bool fog = false;
	public int fpsIndex = 0;
	public bool highPopulation = false;
	public int lowDetailStudents = 0;
	public int particleCount = 0;

	public static OptionSaveData ReadFromGlobals()
	{
		OptionSaveData data = new OptionSaveData();
		data.disableBloom = OptionGlobals.DisableBloom;
		data.disableFarAnimations = OptionGlobals.DisableFarAnimations;
		data.disableOutlines = OptionGlobals.DisableOutlines;
		data.disablePostAliasing = OptionGlobals.DisablePostAliasing;
		data.enableShadows = OptionGlobals.EnableShadows;
		data.drawDistance = OptionGlobals.DrawDistance;
		data.drawDistanceLimit = OptionGlobals.DrawDistanceLimit;
		data.fog = OptionGlobals.Fog;
		data.fpsIndex = OptionGlobals.FPSIndex;
		data.highPopulation = OptionGlobals.HighPopulation;
		data.lowDetailStudents = OptionGlobals.LowDetailStudents;
		data.particleCount = OptionGlobals.ParticleCount;
		return data;
	}

	public static void WriteToGlobals(OptionSaveData data)
	{
		OptionGlobals.DisableBloom = data.disableBloom;
		OptionGlobals.DisableFarAnimations = data.disableFarAnimations;
		OptionGlobals.DisableOutlines = data.disableOutlines;
		OptionGlobals.DisablePostAliasing = data.disablePostAliasing;
		OptionGlobals.EnableShadows = data.enableShadows;
		OptionGlobals.DrawDistance = data.drawDistance;
		OptionGlobals.DrawDistanceLimit = data.drawDistanceLimit;
		OptionGlobals.Fog = data.fog;
		OptionGlobals.FPSIndex = data.fpsIndex;
		OptionGlobals.HighPopulation = data.highPopulation;
		OptionGlobals.LowDetailStudents = data.lowDetailStudents;
		OptionGlobals.ParticleCount = data.particleCount;
	}
}

[Serializable]
public class PlayerSaveData
{
	public int alerts = 0;
	public int enlightenment = 0;
	public int enlightenmentBonus = 0;
	public bool headset = false;
	public int kills = 0;
	public int numbness = 0;
	public int numbnessBonus = 0;
	public int pantiesEquipped = 0;
	public int pantyShots = 0;
	public IntHashSet photo = new IntHashSet();
	public IntHashSet photoOnCorkboard = new IntHashSet();
	public IntAndVector2Dictionary photoPosition = new IntAndVector2Dictionary();
	public IntAndFloatDictionary photoRotation = new IntAndFloatDictionary();
	public float reputation = 0.0f;
	public int seduction = 0;
	public int seductionBonus = 0;
	public IntHashSet senpaiPhoto = new IntHashSet();
	public int senpaiShots = 0;
	public int socialBonus = 0;
	public int speedBonus = 0;
	public int stealthBonus = 0;
	public IntHashSet studentFriend = new IntHashSet();
	public StringHashSet studentPantyShot = new StringHashSet();

	public static PlayerSaveData ReadFromGlobals()
	{
		PlayerSaveData data = new PlayerSaveData();
		data.alerts = PlayerGlobals.Alerts;
		data.enlightenment = PlayerGlobals.Enlightenment;
		data.enlightenmentBonus = PlayerGlobals.EnlightenmentBonus;
		data.headset = PlayerGlobals.Headset;
		data.kills = PlayerGlobals.Kills;
		data.numbness = PlayerGlobals.Numbness;
		data.numbnessBonus = PlayerGlobals.NumbnessBonus;
		data.pantiesEquipped = PlayerGlobals.PantiesEquipped;
		data.pantyShots = PlayerGlobals.PantyShots;

		foreach (int value in PlayerGlobals.KeysOfPhoto())
		{
			if (PlayerGlobals.GetPhoto(value))
			{
				data.photo.Add(value);
			}
		}

		foreach (int value in PlayerGlobals.KeysOfPhotoOnCorkboard())
		{
			if (PlayerGlobals.GetPhotoOnCorkboard(value))
			{
				data.photoOnCorkboard.Add(value);
			}
		}

		foreach (int value in PlayerGlobals.KeysOfPhotoPosition())
		{
			data.photoPosition.Add(value, PlayerGlobals.GetPhotoPosition(value));
		}

		foreach (int value in PlayerGlobals.KeysOfPhotoRotation())
		{
			data.photoRotation.Add(value, PlayerGlobals.GetPhotoRotation(value));
		}

		data.reputation = PlayerGlobals.Reputation;
		data.seduction = PlayerGlobals.Seduction;
		data.seductionBonus = PlayerGlobals.SeductionBonus;

		foreach (int value in PlayerGlobals.KeysOfSenpaiPhoto())
		{
			if (PlayerGlobals.GetSenpaiPhoto(value))
			{
				data.senpaiPhoto.Add(value);
			}
		}

		data.senpaiShots = PlayerGlobals.SenpaiShots;
		data.socialBonus = PlayerGlobals.SocialBonus;
		data.speedBonus = PlayerGlobals.SpeedBonus;
		data.stealthBonus = PlayerGlobals.StealthBonus;

		foreach (int value in PlayerGlobals.KeysOfStudentFriend())
		{
			if (PlayerGlobals.GetStudentFriend(value))
			{
				data.studentFriend.Add(value);
			}
		}

		foreach (string value in PlayerGlobals.KeysOfStudentPantyShot())
		{
			if (PlayerGlobals.GetStudentPantyShot(value))
			{
				data.studentPantyShot.Add(value);
			}
		}

		return data;
	}

	public static void WriteToGlobals(PlayerSaveData data)
	{
		PlayerGlobals.Alerts = data.alerts;
		PlayerGlobals.Enlightenment = data.enlightenment;
		PlayerGlobals.EnlightenmentBonus = data.enlightenmentBonus;
		PlayerGlobals.Headset = data.headset;
		PlayerGlobals.Kills = data.kills;
		PlayerGlobals.Numbness = data.numbness;
		PlayerGlobals.NumbnessBonus = data.numbnessBonus;
		PlayerGlobals.PantiesEquipped = data.pantiesEquipped;
		PlayerGlobals.PantyShots = data.pantyShots;

		Debug.Log("Is this being called anywhere?");

		foreach (int value in data.photo)
		{
			PlayerGlobals.SetPhoto(value, true);
		}

		foreach (int value in data.photoOnCorkboard)
		{
			PlayerGlobals.SetPhotoOnCorkboard(value, true);
		}

		foreach (var pair in data.photoPosition)
		{
			PlayerGlobals.SetPhotoPosition(pair.Key, pair.Value);
		}

		foreach (var pair in data.photoRotation)
		{
			PlayerGlobals.SetPhotoRotation(pair.Key, pair.Value);
		}

		PlayerGlobals.Reputation = data.reputation;
		PlayerGlobals.Seduction = data.seduction;
		PlayerGlobals.SeductionBonus = data.seductionBonus;

		foreach (int value in data.senpaiPhoto)
		{
			PlayerGlobals.SetSenpaiPhoto(value, true);
		}

		PlayerGlobals.SenpaiShots = data.senpaiShots;
		PlayerGlobals.SocialBonus = data.socialBonus;
		PlayerGlobals.SpeedBonus = data.speedBonus;
		PlayerGlobals.StealthBonus = data.stealthBonus;

		foreach (int value in data.studentFriend)
		{
			PlayerGlobals.SetStudentFriend(value, true);
		}

		foreach (string value in data.studentPantyShot)
		{
			PlayerGlobals.SetStudentPantyShot(value, true);
		}
	}
}

[Serializable]
public class PoseModeSaveData
{
	public Vector3 posePosition = new Vector3();
	public Vector3 poseRotation = new Vector3();
	public Vector3 poseScale = new Vector3();

	public static PoseModeSaveData ReadFromGlobals()
	{
		PoseModeSaveData data = new PoseModeSaveData();
		data.posePosition = PoseModeGlobals.PosePosition;
		data.poseRotation = PoseModeGlobals.PoseRotation;
		data.poseScale = PoseModeGlobals.PoseScale;
		return data;
	}

	public static void WriteToGlobals(PoseModeSaveData data)
	{
		PoseModeGlobals.PosePosition = data.posePosition;
		PoseModeGlobals.PoseRotation = data.poseRotation;
		PoseModeGlobals.PoseScale = data.poseScale;
	}
}

[Serializable]
public class SaveFileSaveData
{
	public int currentSaveFile = 0;

	public static SaveFileSaveData ReadFromGlobals()
	{
		SaveFileSaveData data = new SaveFileSaveData();
		data.currentSaveFile = SaveFileGlobals.CurrentSaveFile;
		return data;
	}

	public static void WriteToGlobals(SaveFileSaveData data)
	{
		SaveFileGlobals.CurrentSaveFile = data.currentSaveFile;
	}
}

[Serializable]
public class SchemeSaveData
{
	public int currentScheme = 0;
	public bool darkSecret = false;
	public IntAndIntDictionary schemePreviousStage = new IntAndIntDictionary();
	public IntAndIntDictionary schemeStage = new IntAndIntDictionary();
	public IntHashSet schemeStatus = new IntHashSet();
	public IntHashSet schemeUnlocked = new IntHashSet();
	public IntHashSet servicePurchased = new IntHashSet();

	public static SchemeSaveData ReadFromGlobals()
	{
		SchemeSaveData data = new SchemeSaveData();
		data.currentScheme = SchemeGlobals.CurrentScheme;
		data.darkSecret = SchemeGlobals.DarkSecret;

		foreach (int value in SchemeGlobals.KeysOfSchemePreviousStage())
		{
			data.schemePreviousStage.Add(value, SchemeGlobals.GetSchemePreviousStage(value));
		}

		foreach (int value in SchemeGlobals.KeysOfSchemeStage())
		{
			data.schemeStage.Add(value, SchemeGlobals.GetSchemeStage(value));
		}

		foreach (int value in SchemeGlobals.KeysOfSchemeStatus())
		{
			if (SchemeGlobals.GetSchemeStatus(value))
			{
				data.schemeStatus.Add(value);
			}
		}

		foreach (int value in SchemeGlobals.KeysOfSchemeUnlocked())
		{
			if (SchemeGlobals.GetSchemeUnlocked(value))
			{
				data.schemeUnlocked.Add(value);
			}
		}

		foreach (int value in SchemeGlobals.KeysOfServicePurchased())
		{
			if (SchemeGlobals.GetServicePurchased(value))
			{
				data.servicePurchased.Add(value);
			}
		}

		return data;
	}

	public static void WriteToGlobals(SchemeSaveData data)
	{
		SchemeGlobals.CurrentScheme = data.currentScheme;
		SchemeGlobals.DarkSecret = data.darkSecret;

		foreach (var pair in data.schemePreviousStage)
		{
			SchemeGlobals.SetSchemePreviousStage(pair.Key, pair.Value);
		}

		foreach (var pair in data.schemeStage)
		{
			SchemeGlobals.SetSchemeStage(pair.Key, pair.Value);
		}

		foreach (int value in data.schemeStatus)
		{
			SchemeGlobals.SetSchemeStatus(value, true);
		}

		foreach (int value in data.schemeUnlocked)
		{
			SchemeGlobals.SetSchemeUnlocked(value, true);
		}

		foreach (int value in data.servicePurchased)
		{
			SchemeGlobals.SetServicePurchased(value, true);
		}
	}
}

[Serializable]
public class SchoolSaveData
{
	public IntHashSet demonActive = new IntHashSet();
	public IntHashSet gardenGraveOccupied = new IntHashSet();
	public int kidnapVictim = 0;
	public int population = 0;
	public bool roofFence = false;
	public float schoolAtmosphere = 0.0f;
	public bool schoolAtmosphereSet = false;
	public bool scp = false;

	public static SchoolSaveData ReadFromGlobals()
	{
		SchoolSaveData data = new SchoolSaveData();

		foreach (int value in SchoolGlobals.KeysOfDemonActive())
		{
			if (SchoolGlobals.GetDemonActive(value))
			{
				data.demonActive.Add(value);
			}
		}

		foreach (int value in SchoolGlobals.KeysOfGardenGraveOccupied())
		{
			if (SchoolGlobals.GetGardenGraveOccupied(value))
			{
				data.gardenGraveOccupied.Add(value);
			}
		}

		data.kidnapVictim = SchoolGlobals.KidnapVictim;
		data.population = SchoolGlobals.Population;
		data.roofFence = SchoolGlobals.RoofFence;
		data.schoolAtmosphere = SchoolGlobals.SchoolAtmosphere;
		data.schoolAtmosphereSet = SchoolGlobals.SchoolAtmosphereSet;
		data.scp = SchoolGlobals.SCP;
		return data;
	}

	public static void WriteToGlobals(SchoolSaveData data)
	{
		foreach (int value in data.demonActive)
		{
			SchoolGlobals.SetDemonActive(value, true);
		}

		foreach (int value in data.gardenGraveOccupied)
		{
			SchoolGlobals.SetGardenGraveOccupied(value, true);
		}

		SchoolGlobals.KidnapVictim = data.kidnapVictim;
		SchoolGlobals.Population = data.population;
		SchoolGlobals.RoofFence = data.roofFence;
		SchoolGlobals.SchoolAtmosphere = data.schoolAtmosphere;
		SchoolGlobals.SchoolAtmosphereSet = data.schoolAtmosphereSet;
		SchoolGlobals.SCP = data.scp;
	}
}

[Serializable]
public class SenpaiSaveData
{
	public bool customSenpai = false;
	public string senpaiEyeColor = string.Empty;
	public int senpaiEyeWear = 0;
	public int senpaiFacialHair = 0;
	public string senpaiHairColor = string.Empty;
	public int senpaiHairStyle = 0;
	public int senpaiSkinColor = 0;

	public static SenpaiSaveData ReadFromGlobals()
	{
		SenpaiSaveData data = new SenpaiSaveData();
		data.customSenpai = SenpaiGlobals.CustomSenpai;
		data.senpaiEyeColor = SenpaiGlobals.SenpaiEyeColor;
		data.senpaiEyeWear = SenpaiGlobals.SenpaiEyeWear;
		data.senpaiFacialHair = SenpaiGlobals.SenpaiFacialHair;
		data.senpaiHairColor = SenpaiGlobals.SenpaiHairColor;
		data.senpaiHairStyle = SenpaiGlobals.SenpaiHairStyle;
		data.senpaiSkinColor = SenpaiGlobals.SenpaiSkinColor;
		return data;
	}

	public static void WriteToGlobals(SenpaiSaveData data)
	{
		SenpaiGlobals.CustomSenpai = data.customSenpai;
		SenpaiGlobals.SenpaiEyeColor = data.senpaiEyeColor;
		SenpaiGlobals.SenpaiEyeWear = data.senpaiEyeWear;
		SenpaiGlobals.SenpaiFacialHair = data.senpaiFacialHair;
		SenpaiGlobals.SenpaiHairColor = data.senpaiHairColor;
		SenpaiGlobals.SenpaiHairStyle = data.senpaiHairStyle;
		SenpaiGlobals.SenpaiSkinColor = data.senpaiSkinColor;
	}
}

[Serializable]
public class StudentSaveData
{
	public bool customSuitor = false;
	public int customSuitorAccessory = 0;
	public bool customSuitorBlonde = false;
	public int customSuitorEyewear = 0;
	public int customSuitorHair = 0;
	public int customSuitorJewelry = 0;
	public bool customSuitorTan = false;
	public int expelProgress = 0;
	public int femaleUniform = 0;
	public int maleUniform = 0;
	public IntAndStringDictionary studentAccessory = new IntAndStringDictionary();
	public IntHashSet studentArrested = new IntHashSet();
	public IntHashSet studentBroken = new IntHashSet();
	public IntAndFloatDictionary studentBustSize = new IntAndFloatDictionary();
	public IntAndColorDictionary studentColor = new IntAndColorDictionary();
	public IntHashSet studentDead = new IntHashSet();
	public IntHashSet studentDying = new IntHashSet();
	public IntHashSet studentExpelled = new IntHashSet();
	public IntHashSet studentExposed = new IntHashSet();
	public IntAndColorDictionary studentEyeColor = new IntAndColorDictionary();
	public IntHashSet studentGrudge = new IntHashSet();
	public IntAndStringDictionary studentHairstyle = new IntAndStringDictionary();
	public IntHashSet studentKidnapped = new IntHashSet();
	public IntHashSet studentMissing = new IntHashSet();
	public IntAndStringDictionary studentName = new IntAndStringDictionary();
	public IntHashSet studentPhotographed = new IntHashSet();
	public IntHashSet studentReplaced = new IntHashSet();
	public IntAndIntDictionary studentReputation = new IntAndIntDictionary();
	public IntAndFloatDictionary studentSanity = new IntAndFloatDictionary();
	public IntHashSet studentSlave = new IntHashSet();

	public static StudentSaveData ReadFromGlobals()
	{
		StudentSaveData data = new StudentSaveData();
		data.customSuitor = StudentGlobals.CustomSuitor;
		data.customSuitorAccessory = StudentGlobals.CustomSuitorAccessory;
		data.customSuitorBlonde = StudentGlobals.CustomSuitorBlonde;
		data.customSuitorEyewear = StudentGlobals.CustomSuitorEyewear;
		data.customSuitorHair = StudentGlobals.CustomSuitorHair;
		data.customSuitorJewelry = StudentGlobals.CustomSuitorJewelry;
		data.customSuitorTan = StudentGlobals.CustomSuitorTan;
		data.expelProgress = StudentGlobals.ExpelProgress;
		data.femaleUniform = StudentGlobals.FemaleUniform;
		data.maleUniform = StudentGlobals.MaleUniform;

		foreach (int value in StudentGlobals.KeysOfStudentAccessory())
		{
			data.studentAccessory.Add(value, StudentGlobals.GetStudentAccessory(value));
		}

		foreach (int value in StudentGlobals.KeysOfStudentArrested())
		{
			if (StudentGlobals.GetStudentArrested(value))
			{
				data.studentArrested.Add(value);
			}
		}

		foreach (int value in StudentGlobals.KeysOfStudentBroken())
		{
			if (StudentGlobals.GetStudentBroken(value))
			{
				data.studentBroken.Add(value);
			}
		}

		foreach (int value in StudentGlobals.KeysOfStudentBustSize())
		{
			data.studentBustSize.Add(value, StudentGlobals.GetStudentBustSize(value));
		}

		foreach (int value in StudentGlobals.KeysOfStudentColor())
		{
			data.studentColor.Add(value, StudentGlobals.GetStudentColor(value));
		}

		foreach (int value in StudentGlobals.KeysOfStudentDead())
		{
			if (StudentGlobals.GetStudentDead(value))
			{
				data.studentDead.Add(value);
			}
		}

		foreach (int value in StudentGlobals.KeysOfStudentDying())
		{
			if (StudentGlobals.GetStudentDying(value))
			{
				data.studentDying.Add(value);
			}
		}

		foreach (int value in StudentGlobals.KeysOfStudentExpelled())
		{
			if (StudentGlobals.GetStudentExpelled(value))
			{
				data.studentExpelled.Add(value);
			}
		}

		foreach (int value in StudentGlobals.KeysOfStudentExposed())
		{
			if (StudentGlobals.GetStudentExposed(value))
			{
				data.studentExposed.Add(value);
			}
		}

		foreach (int value in StudentGlobals.KeysOfStudentEyeColor())
		{
			data.studentEyeColor.Add(value, StudentGlobals.GetStudentEyeColor(value));
		}

		foreach (int value in StudentGlobals.KeysOfStudentGrudge())
		{
			if (StudentGlobals.GetStudentGrudge(value))
			{
				data.studentGrudge.Add(value);
			}
		}

		foreach (int value in StudentGlobals.KeysOfStudentHairstyle())
		{
			data.studentHairstyle.Add(value, StudentGlobals.GetStudentHairstyle(value));
		}

		foreach (int value in StudentGlobals.KeysOfStudentKidnapped())
		{
			if (StudentGlobals.GetStudentKidnapped(value))
			{
				data.studentKidnapped.Add(value);
			}
		}

		foreach (int value in StudentGlobals.KeysOfStudentMissing())
		{
			if (StudentGlobals.GetStudentMissing(value))
			{
				data.studentMissing.Add(value);
			}
		}

		foreach (int value in StudentGlobals.KeysOfStudentName())
		{
			data.studentName.Add(value, StudentGlobals.GetStudentName(value));
		}

		foreach (int value in StudentGlobals.KeysOfStudentPhotographed())
		{
			if (StudentGlobals.GetStudentPhotographed(value))
			{
				data.studentPhotographed.Add(value);
			}
		}

		foreach (int value in StudentGlobals.KeysOfStudentReplaced())
		{
			if (StudentGlobals.GetStudentReplaced(value))
			{
				data.studentReplaced.Add(value);
			}
		}

		foreach (int value in StudentGlobals.KeysOfStudentReputation())
		{
			data.studentReputation.Add(value, StudentGlobals.GetStudentReputation(value));
		}

		foreach (int value in StudentGlobals.KeysOfStudentSanity())
		{
			data.studentSanity.Add(value, StudentGlobals.GetStudentSanity(value));
		}

		/*
		foreach (int value in StudentGlobals.KeysOfStudentSlave())
		{
			if (StudentGlobals.GetStudentSlave(value))
			{
				data.studentSlave.Add(value);
			}
		}
		*/

		return data;
	}

	public static void WriteToGlobals(StudentSaveData data)
	{
		StudentGlobals.CustomSuitor = data.customSuitor;
		StudentGlobals.CustomSuitorAccessory = data.customSuitorAccessory;
		StudentGlobals.CustomSuitorBlonde = data.customSuitorBlonde;
		StudentGlobals.CustomSuitorEyewear = data.customSuitorEyewear;
		StudentGlobals.CustomSuitorHair = data.customSuitorHair;
		StudentGlobals.CustomSuitorJewelry = data.customSuitorJewelry;
		StudentGlobals.CustomSuitorTan = data.customSuitorTan;
		StudentGlobals.ExpelProgress = data.expelProgress;
		StudentGlobals.FemaleUniform = data.femaleUniform;
		StudentGlobals.MaleUniform = data.maleUniform;

		foreach (var pair in data.studentAccessory)
		{
			StudentGlobals.SetStudentAccessory(pair.Key, pair.Value);
		}

		foreach (int value in data.studentArrested)
		{
			StudentGlobals.SetStudentArrested(value, true);
		}

		foreach (int value in data.studentBroken)
		{
			StudentGlobals.SetStudentBroken(value, true);
		}

		foreach (var pair in data.studentBustSize)
		{
			StudentGlobals.SetStudentBustSize(pair.Key, pair.Value);
		}

		foreach (var pair in data.studentColor)
		{
			StudentGlobals.SetStudentColor(pair.Key, pair.Value);
		}

		foreach (int value in data.studentDead)
		{
			StudentGlobals.SetStudentDead(value, true);
		}

		foreach (int value in data.studentDying)
		{
			StudentGlobals.SetStudentDying(value, true);
		}

		foreach (int value in data.studentExpelled)
		{
			StudentGlobals.SetStudentExpelled(value, true);
		}

		foreach (int value in data.studentExposed)
		{
			StudentGlobals.SetStudentExposed(value, true);
		}

		foreach (var pair in data.studentEyeColor)
		{
			StudentGlobals.SetStudentEyeColor(pair.Key, pair.Value);
		}

		foreach (int value in data.studentGrudge)
		{
			StudentGlobals.SetStudentGrudge(value, true);
		}

		foreach (var pair in data.studentHairstyle)
		{
			StudentGlobals.SetStudentHairstyle(pair.Key, pair.Value);
		}

		foreach (int value in data.studentKidnapped)
		{
			StudentGlobals.SetStudentKidnapped(value, true);
		}

		foreach (int value in data.studentMissing)
		{
			StudentGlobals.SetStudentMissing(value, true);
		}

		foreach (var pair in data.studentName)
		{
			StudentGlobals.SetStudentName(pair.Key, pair.Value);
		}

		foreach (int value in data.studentPhotographed)
		{
			StudentGlobals.SetStudentPhotographed(value, true);
		}

		foreach (int value in data.studentReplaced)
		{
			StudentGlobals.SetStudentReplaced(value, true);
		}

		foreach (var pair in data.studentReputation)
		{
			StudentGlobals.SetStudentReputation(pair.Key, pair.Value);
		}

		foreach (var pair in data.studentSanity)
		{
			StudentGlobals.SetStudentSanity(pair.Key, pair.Value);
		}

		/*
		foreach (int value in data.studentSlave)
		{
			StudentGlobals.SetStudentSlave(value, true);
		}
		*/
	}
}

[Serializable]
public class TaskSaveData
{
	public IntHashSet guitarPhoto = new IntHashSet();
	public IntHashSet kittenPhoto = new IntHashSet();
	public IntHashSet horudaPhoto = new IntHashSet();
	public IntAndIntDictionary taskStatus = new IntAndIntDictionary();

	public static TaskSaveData ReadFromGlobals()
	{
		TaskSaveData data = new TaskSaveData();

		foreach (int value in TaskGlobals.KeysOfGuitarPhoto())
		{
			if (TaskGlobals.GetGuitarPhoto(value))
			{
				data.guitarPhoto.Add(value);
			}
		}

		foreach (int value in TaskGlobals.KeysOfKittenPhoto())
		{
			if (TaskGlobals.GetKittenPhoto(value))
			{
				data.kittenPhoto.Add(value);
			}
		}

		foreach (int value in TaskGlobals.KeysOfHorudaPhoto())
		{
			if (TaskGlobals.GetHorudaPhoto(value))
			{
				data.horudaPhoto.Add(value);
			}
		}

		foreach (int value in TaskGlobals.KeysOfTaskStatus())
		{
			data.taskStatus.Add(value, TaskGlobals.GetTaskStatus(value));
		}

		return data;
	}

	public static void WriteToGlobals(TaskSaveData data)
	{
		foreach (int value in data.kittenPhoto)
		{
			TaskGlobals.SetKittenPhoto(value, true);
		}

		foreach (int value in data.guitarPhoto)
		{
			TaskGlobals.SetGuitarPhoto(value, true);
		}

		foreach (var pair in data.taskStatus)
		{
			TaskGlobals.SetTaskStatus(pair.Key, pair.Value);
		}
	}
}

[Serializable]
public class YanvaniaSaveData
{
	public bool draculaDefeated = false;
	public bool midoriEasterEgg = false;

	public static YanvaniaSaveData ReadFromGlobals()
	{
		YanvaniaSaveData data = new YanvaniaSaveData();
		data.draculaDefeated = YanvaniaGlobals.DraculaDefeated;
		data.midoriEasterEgg = YanvaniaGlobals.MidoriEasterEgg;
		return data;
	}

	public static void WriteToGlobals(YanvaniaSaveData data)
	{
		YanvaniaGlobals.DraculaDefeated = data.draculaDefeated;
		YanvaniaGlobals.MidoriEasterEgg = data.midoriEasterEgg;
	}
}
