using System;
using System.Collections.Generic;
using UnityEngine;

// [af] PlayerPrefs only supports a small number of types (int, float, string),
// so we need wrappers here to increase its flexibility. Each pref category is sorted 
// alphabetically.

// *** Important ***
// When adding a new global to be saved in the save files, make sure to:
// - Add a call to KeysHelper.AddIfMissing() in a Set(...) method if it takes parameters.
// - Add a call to Globals.Delete() (or similar) in the class' DeleteAll() method.
// - Add a variable in the associated SaveData class for serialization.
// - Put code in the ReadFromGlobals() and WriteToGlobals() methods of the SaveData 
//   class for that particular variable.

// Helper methods (for types not directly supported by PlayerPrefs). We should avoid
// making helper methods for larger types because it would involve quite a lot of
// copying and string lookups, and it would ultimately be a drag on performance.
public static class GlobalsHelper
{
	public static bool GetBool(string key)
	{
		return PlayerPrefs.GetInt(key) == 1;
	}

	public static void SetBool(string key, bool value)
	{
		PlayerPrefs.SetInt(key, value ? 1 : 0);
	}

	public static T GetEnum<T>(string key) where T : struct, IConvertible
	{
		return (T)((object)PlayerPrefs.GetInt(key));
	}

	public static void SetEnum<T>(string key, T value) where T : struct, IConvertible
	{
		PlayerPrefs.SetInt(key, (int)((object)value));
	}

	public static Vector2 GetVector2(string key)
	{
		float x = PlayerPrefs.GetFloat(key + "_X");
		float y = PlayerPrefs.GetFloat(key + "_Y");
		return new Vector2(x, y);
	}

	public static void SetVector2(string key, Vector2 value)
	{
		PlayerPrefs.SetFloat(key + "_X", value.x);
		PlayerPrefs.SetFloat(key + "_Y", value.y);
	}

	public static void DeleteVector2(string key)
	{
		Globals.Delete(key + "_X");
		Globals.Delete(key + "_Y");
	}

	public static void DeleteVector2Collection(string key, int[] usedKeys)
	{
		foreach (int value in usedKeys)
		{
			DeleteVector2(key + value.ToString());
		}

		KeysHelper.Delete(key);
	}

	public static Vector3 GetVector3(string key)
	{
		float x = PlayerPrefs.GetFloat(key + "_X");
		float y = PlayerPrefs.GetFloat(key + "_Y");
		float z = PlayerPrefs.GetFloat(key + "_Z");
		return new Vector3(x, y, z);
	}

	public static void SetVector3(string key, Vector3 value)
	{
		PlayerPrefs.SetFloat(key + "_X", value.x);
		PlayerPrefs.SetFloat(key + "_Y", value.y);
		PlayerPrefs.SetFloat(key + "_Z", value.z);
	}

	public static void DeleteVector3(string key)
	{
		Globals.Delete(key + "_X");
		Globals.Delete(key + "_Y");
		Globals.Delete(key + "_Z");
	}

	public static void DeleteVector3Collection(string key, int[] usedKeys)
	{
		foreach (int value in usedKeys)
		{
			DeleteVector3(key + value.ToString());
		}

		KeysHelper.Delete(key);
	}

	public static Vector4 GetVector4(string key)
	{
		float w = PlayerPrefs.GetFloat(key + "_W");
		float x = PlayerPrefs.GetFloat(key + "_X");
		float y = PlayerPrefs.GetFloat(key + "_Y");
		float z = PlayerPrefs.GetFloat(key + "_Z");
		return new Vector4(w, x, y, z);
	}

	public static void SetVector4(string key, Vector4 value)
	{
		PlayerPrefs.SetFloat(key + "_W", value.w);
		PlayerPrefs.SetFloat(key + "_X", value.x);
		PlayerPrefs.SetFloat(key + "_Y", value.y);
		PlayerPrefs.SetFloat(key + "_Z", value.z);
	}

	public static void DeleteVector4(string key)
	{
		Globals.Delete(key + "_W");
		Globals.Delete(key + "_X");
		Globals.Delete(key + "_Y");
		Globals.Delete(key + "_Z");
	}

	public static Color GetColor(string key)
	{
		float r = PlayerPrefs.GetFloat(key + "_R");
		float g = PlayerPrefs.GetFloat(key + "_G");
		float b = PlayerPrefs.GetFloat(key + "_B");
		float a = PlayerPrefs.GetFloat(key + "_A");
		return new Color(r, g, b, a);
	}

	public static void SetColor(string key, Color value)
	{
		PlayerPrefs.SetFloat(key + "_R", value.r);
		PlayerPrefs.SetFloat(key + "_G", value.g);
		PlayerPrefs.SetFloat(key + "_B", value.b);
		PlayerPrefs.SetFloat(key + "_A", value.a);
	}

	public static void DeleteColor(string key)
	{
		Globals.Delete(key + "_R");
		Globals.Delete(key + "_G");
		Globals.Delete(key + "_B");
		Globals.Delete(key + "_A");
	}

	public static void DeleteColorCollection(string key, int[] usedKeys)
	{
		foreach (int value in usedKeys)
		{
			DeleteColor(key + value.ToString());
		}

		KeysHelper.Delete(key);
	}
}

// Key list functionality (for iterating over globals). Intended only for
// wrappers whose methods take one or more parameters.
public static class KeysHelper
{
	const string KeyListPrefix = "Keys";
	const char KeyListSeparator = '|';
	public const char PairSeparator = '^';

	// Gets all the keys (i.e., indices) in use by the given global key. Intended
	// for giving the save data a way to enumerate all indices in some global dataset.
	public static int[] GetIntegerKeys(string key)
	{
		string keyList = GetKeyList(GetKeyListKey(key));
		string[] keyListStrings = SplitList(keyList);
		int[] keys = Array.ConvertAll(keyListStrings, str => int.Parse(str));
		return keys;
	}

	// GetKeys method specialized for key lists composed of strings.
	public static string[] GetStringKeys(string key)
	{
		string keyList = GetKeyList(GetKeyListKey(key));
		string[] keyListStrings = SplitList(keyList);
		return keyListStrings;
	}

	// GetKeys method specialized for key lists composed of enums.
	public static T[] GetEnumKeys<T>(string key) where T : struct, IConvertible
	{
		string keyList = GetKeyList(GetKeyListKey(key));
		string[] keyListStrings = SplitList(keyList);
		T[] keys = Array.ConvertAll(keyListStrings, str => (T)Enum.Parse(typeof(T), str));
		return keys;
	}

	// GetKeys method specialized for key lists composed of value-type pairs.
	// @todo: Make specialized (non-generic) methods for each. Don't use generics.
	public static KeyValuePair<T, U>[] GetKeys<T, U>(string key) where T : struct
		where U : struct
	{
		string keyList = GetKeyList(GetKeyListKey(key));
		string[] keyListStrings = SplitList(keyList);
		KeyValuePair<T, U>[] keyPairs = new KeyValuePair<T, U>[keyListStrings.Length];

		for (int i = 0; i < keyListStrings.Length; i++)
		{
			string[] keyStringPair = keyListStrings[i].Split(PairSeparator);
			keyPairs[i] = new KeyValuePair<T, U>(
				(T)((object)int.Parse(keyStringPair[0])),
				(U)((object)int.Parse(keyStringPair[1])));
		}

		return keyPairs;
	}

	// Registers a new index into the given global key's key list.
	public static void AddIfMissing(string key, string id)
	{
		string keyListKey = GetKeyListKey(key);
		string keyList = GetKeyList(keyListKey);
		string[] keyListStrings = SplitList(keyList);

		if (!HasKey(keyListStrings, id))
		{
			AppendKey(keyListKey, keyList, id);
		}
	}

	// Deletes a key list, given some key.
	public static void Delete(string key)
	{
		string keyListKey = GetKeyListKey(key);
		Globals.Delete(keyListKey);
	}

	// Given a key for some global, this returns the key for accessing a key list.
	static string GetKeyListKey(string key)
	{
		return key + KeyListPrefix;
	}

	// Gets the key list associated with a key list's key.
	static string GetKeyList(string keyListKey)
	{
		return PlayerPrefs.GetString(keyListKey);
	}

	// Splits a key list on the character separator, so that individual elements can be 
	// read. If the key list is empty, then it returns an empty array (which is valid; 
	// we don't want the default .NET behavior of an array with one empty string).
	static string[] SplitList(string keyList)
	{
		return (keyList.Length > 0) ? keyList.Split(KeyListSeparator) : new string[0];
	}

	// Gets the index of a key string in the array of key list strings, or -1
	// if the key is not found.
	static int FindKey(string[] keyListStrings, string key)
	{
		return Array.IndexOf(keyListStrings, key);
	}

	// Returns whether the array of key list strings contains the given key.
	static bool HasKey(string[] keyListStrings, string key)
	{
		return FindKey(keyListStrings, key) > -1;
	}

	static void AppendKey(string keyListKey, string keyList, string key)
	{
		// Don't prepend a separator character on the first element so that
		// there is no empty string after splitting.
		string newKeyList = (keyList.Length == 0) ? (keyList + key) :
			(keyList + KeyListSeparator + key);

		PlayerPrefs.SetString(keyListKey, newKeyList);
	}
}

public static class Globals
{
	// --- Application-level methods ---

	public static bool KeyExists(string key)
	{
		return PlayerPrefs.HasKey(key);
	}

	public static void DeleteAll()
	{
		//Debug.Log("All globals (except OptionGlobals and SaveFileGlobals) have just been deleted.");

		int Profile = GameGlobals.Profile;

		ClassGlobals.DeleteAll();
		ClubGlobals.DeleteAll();
		CollectibleGlobals.DeleteAll();
		ConversationGlobals.DeleteAll();
		DateGlobals.DeleteAll();
		DatingGlobals.DeleteAll();
		EventGlobals.DeleteAll();
		GameGlobals.DeleteAll();
		HomeGlobals.DeleteAll();
		MissionModeGlobals.DeleteAll();
		//OptionGlobals.DeleteAll();
		PlayerGlobals.DeleteAll();
		PoseModeGlobals.DeleteAll();
		//SaveFileGlobals.DeleteAll();
		SchemeGlobals.DeleteAll();
		SchoolGlobals.DeleteAll();
		SenpaiGlobals.DeleteAll();
		StudentGlobals.DeleteAll();
		TaskGlobals.DeleteAll();
		YanvaniaGlobals.DeleteAll();
		WeaponGlobals.DeleteAll();
		TutorialGlobals.DeleteAll();
		CounselorGlobals.DeleteAll();
		YancordGlobals.DeleteAll();
		CorkboardGlobals.DeleteAll ();

		GameGlobals.Profile = Profile;

		DateGlobals.Week = 1;

		//PlayerPrefs.DeleteAll();
	}

	// [af] This should only be called by specialized Globals methods, such as those that 
	// have knowledge of key lists; i.e., something like ClubGlobals.DeleteAll(). Do not
	// call this method outside the context of one of the specialized Globals sub-classes.
	public static void Delete(string key)
	{
		PlayerPrefs.DeleteKey(key);
	}

	// [af] Similar to Delete(), but for a dictionary of values associated with the given 
	// key string. Also deletes the key list.
	public static void DeleteCollection(string key, int[] usedKeys)
	{
		foreach (int value in usedKeys)
		{
			PlayerPrefs.DeleteKey(key + value.ToString());
		}

		KeysHelper.Delete(key);
	}

	// [af] DeleteCollection() specialization for string IDs.
	public static void DeleteCollection(string key, string[] usedKeys)
	{
		foreach (string value in usedKeys)
		{
			PlayerPrefs.DeleteKey(key + value);
		}

		KeysHelper.Delete(key);
	}

	public static void Save()
	{
		PlayerPrefs.Save();
	}
}

public static class ApplicationGlobals
{
	const string Str_VersionNumber = "VersionNumber";

	public static float VersionNumber
	{
		get { return PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_" + Str_VersionNumber); }
		set { PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_" + Str_VersionNumber, value); }
	}

	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_VersionNumber);
	}
}

public static class ClassGlobals
{
	const string Str_Biology = "Biology";
	const string Str_BiologyBonus = "BiologyBonus";
	const string Str_BiologyGrade = "BiologyGrade";
	const string Str_Chemistry = "Chemistry";
	const string Str_ChemistryBonus = "ChemistryBonus";
	const string Str_ChemistryGrade = "ChemistryGrade";
	const string Str_Language = "Language";
	const string Str_LanguageBonus = "LanguageBonus";
	const string Str_LanguageGrade = "LanguageGrade";
	const string Str_Physical = "Physical";
	const string Str_PhysicalBonus = "PhysicalBonus";
	const string Str_PhysicalGrade = "PhysicalGrade";
	const string Str_Psychology = "Psychology";
	const string Str_PsychologyBonus = "PsychologyBonus";
	const string Str_PsychologyGrade = "PsychologyGrade";

	public static int Biology
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_Biology); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_Biology, value); }
	}

	public static int BiologyBonus
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_BiologyBonus); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_BiologyBonus, value); }
	}

	public static int BiologyGrade
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_BiologyGrade); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_BiologyGrade, value); }
	}

	public static int Chemistry
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_Chemistry); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_Chemistry, value); }
	}

	public static int ChemistryBonus
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_ChemistryBonus); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_ChemistryBonus, value); }
	}

	public static int ChemistryGrade
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_ChemistryGrade); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_ChemistryGrade, value); }
	}

	public static int Language
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_Language); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_Language, value); }
	}

	public static int LanguageBonus
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_LanguageBonus); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_LanguageBonus, value); }
	}

	public static int LanguageGrade
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_LanguageGrade); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_LanguageGrade, value); }
	}

	public static int Physical
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_Physical); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_Physical, value); }
	}

	public static int PhysicalBonus
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_PhysicalBonus); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_PhysicalBonus, value); }
	}

	public static int PhysicalGrade
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_PhysicalGrade); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_PhysicalGrade, value); }
	}

	public static int Psychology
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_Psychology); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_Psychology, value); }
	}

	public static int PsychologyBonus
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_PsychologyBonus); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_PsychologyBonus, value); }
	}

	public static int PsychologyGrade
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_PsychologyGrade); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_PsychologyGrade, value); }
	}

	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_Biology);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_BiologyBonus);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_BiologyGrade);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_Chemistry);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_ChemistryBonus);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_ChemistryGrade);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_Language);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_LanguageBonus);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_LanguageGrade);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_Physical);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_PhysicalBonus);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_PhysicalGrade);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_Psychology);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_PsychologyBonus);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_PsychologyGrade);
	}
}

public static class ClubGlobals
{
	const string Str_Club = "Club";
	const string Str_ClubClosed = "ClubClosed_"; // Replaces "Club_" + ID + "_Closed".
	const string Str_ClubKicked = "ClubKicked_"; // Replaces "Club_" + ID + "_Kicked".
	const string Str_QuitClub = "QuitClub_";

	public static ClubType Club
	{
		get { return GlobalsHelper.GetEnum<ClubType>("Profile_" + GameGlobals.Profile + "_" + Str_Club); }
		set { GlobalsHelper.SetEnum("Profile_" + GameGlobals.Profile + "_" + Str_Club, value); }
	}

	public static bool GetClubClosed(ClubType clubID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_ClubClosed + ((int)clubID).ToString());
	}

	public static void SetClubClosed(ClubType clubID, bool value)
	{
		string clubString = ((int)clubID).ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_ClubClosed, clubString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_ClubClosed + clubString, value);
	}

	public static ClubType[] KeysOfClubClosed()
	{
		return KeysHelper.GetEnumKeys<ClubType>("Profile_" + GameGlobals.Profile + "_" + Str_ClubClosed);
	}

	public static bool GetClubKicked(ClubType clubID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_ClubKicked + ((int)clubID).ToString());
	}

	public static void SetClubKicked(ClubType clubID, bool value)
	{
		string clubString = ((int)clubID).ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_ClubKicked, clubString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_ClubKicked + clubString, value);
	}

	public static ClubType[] KeysOfClubKicked()
	{
		return KeysHelper.GetEnumKeys<ClubType>("Profile_" + GameGlobals.Profile + "_" + Str_ClubKicked);
	}

	public static bool GetQuitClub(ClubType clubID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_QuitClub + ((int)clubID).ToString());
	}

	public static void SetQuitClub(ClubType clubID, bool value)
	{
		string clubString = ((int)clubID).ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_QuitClub, clubString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_QuitClub + clubString, value);
	}

	public static ClubType[] KeysOfQuitClub()
	{
		return KeysHelper.GetEnumKeys<ClubType>("Profile_" + GameGlobals.Profile + "_" + Str_QuitClub);
	}

	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_Club);

		foreach (ClubType value in KeysOfClubClosed())
		{
			Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_ClubClosed + ((int)value).ToString());
		}

		foreach (ClubType value in KeysOfClubKicked())
		{
			Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_ClubKicked + ((int)value).ToString());
		}

		foreach (ClubType value in KeysOfQuitClub())
		{
			Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_QuitClub + ((int)value).ToString());
		}

		KeysHelper.Delete("Profile_" + GameGlobals.Profile + "_" + Str_ClubClosed);
		KeysHelper.Delete("Profile_" + GameGlobals.Profile + "_" + Str_ClubKicked);
		KeysHelper.Delete("Profile_" + GameGlobals.Profile + "_" + Str_QuitClub);
	}
}

public static class CollectibleGlobals
{
	const string Str_HeadmasterTapeCollected = "HeadmasterTapeCollected_"; // Replaces "HeadmasterTape_" + ID + "_Collected".
	const string Str_HeadmasterTapeListened = "HeadmasterTapeListened_"; // Replaces "HeadmasterTape_" + ID + "_Listened".

	const string Str_BasementTapeCollected = "BasementTapeCollected_"; // Replaces "BasementTape_" + ID + "_Collected".
	const string Str_BasementTapeListened = "BasementTapeListened_"; // Replaces "BasementTape_" + ID + "_Listened".

	const string Str_MangaCollected = "MangaCollected_"; // Replaces "Manga_" + ID + "_Collected".

	const string Str_GiftPurchased = "GiftPurchased_";
	const string Str_GiftGiven = "GiftGiven_";

	const string Str_MatchmakingGifts = "MatchmakingGifts";
	const string Str_SenpaiGifts = "SenpaiGifts";

	const string Str_PantyPurchased = "PantyPurchased_";

	const string Str_TapeCollected = "TapeCollected_"; // Replaces "Tape_" + ID + "_Collected".
	const string Str_TapeListened = "TapeListened_"; // Replaces "Tape_" + ID + "_Listened".

	public static bool GetHeadmasterTapeCollected(int tapeID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_HeadmasterTapeCollected + tapeID.ToString());
	}

	public static void SetHeadmasterTapeCollected(int tapeID, bool value)
	{
		string tapeString = tapeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_HeadmasterTapeCollected, tapeString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_HeadmasterTapeCollected + tapeString, value);
	}

	public static bool GetHeadmasterTapeListened(int tapeID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_HeadmasterTapeListened + tapeID.ToString());
	}

	public static void SetHeadmasterTapeListened(int tapeID, bool value)
	{
		string tapeString = tapeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_HeadmasterTapeListened, tapeString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_HeadmasterTapeListened + tapeString, value);
	}

    public static int[] KeysOfHeadmasterTapeCollected()
    {
        return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_HeadmasterTapeCollected);
    }

    public static int[] KeysOfHeadmasterTapeListened()
    {
        return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_HeadmasterTapeListened);
    }

    public static bool GetBasementTapeCollected(int tapeID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_BasementTapeCollected + tapeID.ToString());
	}

	public static void SetBasementTapeCollected(int tapeID, bool value)
	{
		string tapeString = tapeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_BasementTapeCollected, tapeString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_BasementTapeCollected + tapeString, value);
	}

	public static int[] KeysOfBasementTapeCollected()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_BasementTapeCollected);
	}

	public static bool GetBasementTapeListened(int tapeID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_BasementTapeListened + tapeID.ToString());
	}

	public static void SetBasementTapeListened(int tapeID, bool value)
	{
		string tapeString = tapeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_BasementTapeListened, tapeString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_BasementTapeListened + tapeString, value);
	}

	public static int[] KeysOfBasementTapeListened()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_BasementTapeListened);
	}

	public static bool GetMangaCollected(int mangaID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_MangaCollected + mangaID.ToString());
	}

	public static void SetMangaCollected(int mangaID, bool value)
	{
		string mangaString = mangaID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_MangaCollected, mangaString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_MangaCollected + mangaString, value);
	}

	public static bool GetGiftPurchased(int giftID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_GiftPurchased + giftID.ToString());
	}

	public static void SetGiftPurchased(int giftID, bool value)
	{
		string giftString = giftID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_GiftPurchased, giftString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_GiftPurchased + giftString, value);
	}

	public static bool GetGiftGiven(int giftID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_GiftGiven + giftID.ToString());
	}

	public static void SetGiftGiven(int giftID, bool value)
	{
		string giftString = giftID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_GiftGiven, giftString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_GiftGiven + giftString, value);
	}
		
	public static int MatchmakingGifts
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_MatchmakingGifts); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_MatchmakingGifts, value); }
	}

	public static int SenpaiGifts
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiGifts); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiGifts, value); }
	}

	public static bool GetPantyPurchased(int giftID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_PantyPurchased + giftID.ToString());
	}

	public static void SetPantyPurchased(int pantyID, bool value)
	{
		string pantyString = pantyID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_PantyPurchased, pantyString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_PantyPurchased + pantyString, value);
	}

	public static int[] KeysOfMangaCollected()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_MangaCollected);
	}

	public static int[] KeysOfGiftPurchased()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_GiftPurchased);
	}

	public static int[] KeysOfGiftGiven()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_GiftGiven);
	}

	public static int[] KeysOfPantyPurchased()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_PantyPurchased);
	}

	public static bool GetTapeCollected(int tapeID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_TapeCollected + tapeID.ToString());
	}

	public static void SetTapeCollected(int tapeID, bool value)
	{
		string tapeString = tapeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_TapeCollected, tapeString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_TapeCollected + tapeString, value);
	}

	public static int[] KeysOfTapeCollected()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_TapeCollected);
	}

	public static bool GetTapeListened(int tapeID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_TapeListened + tapeID.ToString());
	}

	public static void SetTapeListened(int tapeID, bool value)
	{
		string tapeString = tapeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_TapeListened, tapeString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_TapeListened + tapeString, value);
	}

	public static int[] KeysOfTapeListened()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_TapeListened);
	}

	public static void DeleteAll()
	{
        Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_HeadmasterTapeCollected, KeysOfHeadmasterTapeCollected());
        Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_HeadmasterTapeListened, KeysOfHeadmasterTapeListened());

        Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_BasementTapeCollected, KeysOfBasementTapeCollected());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_BasementTapeListened, KeysOfBasementTapeListened());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_MangaCollected, KeysOfMangaCollected());

		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_PantyPurchased, KeysOfPantyPurchased());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_GiftPurchased, KeysOfGiftPurchased());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_GiftGiven, KeysOfGiftGiven());

		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_TapeCollected, KeysOfTapeCollected());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_TapeListened, KeysOfTapeListened());

		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_MatchmakingGifts);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiGifts);
	}
}

public static class ConversationGlobals
{
	const string Str_TopicDiscovered = "TopicDiscovered_"; // Replaces "Topic_" + ID + "_Discovered".
	const string Str_TopicLearnedByStudent = "TopicLearnedByStudent_"; // Replaces "Topic_" + ID + "_Student_" + StudentID + "_Learned".

	public static bool GetTopicDiscovered(int topicID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_TopicDiscovered + topicID.ToString());
	}

	public static void SetTopicDiscovered(int topicID, bool value)
	{
		string topicString = topicID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_TopicDiscovered, topicString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_TopicDiscovered + topicString, value);
	}

	public static int[] KeysOfTopicDiscovered()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_TopicDiscovered);
	}

	public static bool GetTopicLearnedByStudent(int topicID, int studentID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_TopicLearnedByStudent + topicID.ToString() +
			'_' + studentID.ToString());
	}

	public static void SetTopicLearnedByStudent(int topicID, int studentID, bool value)
	{
		string topicString = topicID.ToString();
		string studentString = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_TopicLearnedByStudent,
			topicString + KeysHelper.PairSeparator + studentString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_TopicLearnedByStudent + topicString +
			'_' + studentString, value);
	}

	public static IntAndIntPair[] KeysOfTopicLearnedByStudent()
	{
		KeyValuePair<int, int>[] pairs = KeysHelper.GetKeys<int, int>("Profile_" + GameGlobals.Profile + "_" + Str_TopicLearnedByStudent);
		IntAndIntPair[] newPairs = new IntAndIntPair[pairs.Length];

		for (int i = 0; i < pairs.Length; i++)
		{
			var pair = pairs[i];
			newPairs[i] = new IntAndIntPair(pair.Key, pair.Value);
		}

		return newPairs;
	}

	public static void DeleteAll()
	{
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_TopicDiscovered, KeysOfTopicDiscovered());

		foreach (var pair in KeysOfTopicLearnedByStudent())
		{
			Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_TopicLearnedByStudent + pair.first.ToString() +
				'_' + pair.second.ToString());
		}

		KeysHelper.Delete("Profile_" + GameGlobals.Profile + "_" + Str_TopicLearnedByStudent);
	}
}

public static class DateGlobals
{
	const string Str_Week = "Week";
	const string Str_Weekday = "Weekday";
	const string Str_PassDays = "PassDays";
	const string Str_DayPassed = "DayPassed";

	public static int Week
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_Week); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_Week, value); }
	}

	public static DayOfWeek Weekday
	{
		get { return GlobalsHelper.GetEnum<DayOfWeek>("Profile_" + GameGlobals.Profile + "_" + Str_Weekday); }
		set { GlobalsHelper.SetEnum("Profile_" + GameGlobals.Profile + "_" + Str_Weekday, value); }
	}

	public static int PassDays
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_PassDays); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_PassDays, value); }
	}

	public static bool DayPassed
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_DayPassed); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_DayPassed, value); }
	}

	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_Week);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_Weekday);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_PassDays);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_DayPassed);
	}
}

public static class DatingGlobals
{
	const string Str_Affection = "Affection";
	const string Str_AffectionLevel = "AffectionLevel";
	const string Str_ComplimentGiven = "ComplimentGiven_"; // Replaces "Compliment_" + ID + "_Given".
	const string Str_SuitorCheck = "SuitorCheck_"; // Replaces "SuitorCheck" + ID.
	const string Str_SuitorProgress = "SuitorProgress";
	const string Str_SuitorTrait = "SuitorTrait_"; // Replaces "SuitorTrait2".
	const string Str_TopicDiscussed = "TopicDiscussed_"; // Replaces "Topic_" + ID + "_Discussed".
	const string Str_TraitDemonstrated = "TraitDemonstrated_"; // Replaces "Trait_2_Demonstrated".
	const string Str_RivalSabotaged = "RivalSabotaged";

	public static float Affection
	{
		get { return PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_" + Str_Affection); }
		set { PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_" + Str_Affection, value); }
	}

	public static float AffectionLevel
	{
		get { return PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_" + Str_AffectionLevel); }
		set { PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_" + Str_AffectionLevel, value); }
	}

	public static bool GetComplimentGiven(int complimentID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_ComplimentGiven + complimentID.ToString());
	}

	public static void SetComplimentGiven(int complimentID, bool value)
	{
		string complimentString = complimentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_ComplimentGiven, complimentString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_ComplimentGiven + complimentString, value);
	}

	public static int[] KeysOfComplimentGiven()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_ComplimentGiven);
	}

	public static bool GetSuitorCheck(int checkID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_SuitorCheck + checkID.ToString());
	}

	public static void SetSuitorCheck(int checkID, bool value)
	{
		string checkString = checkID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_SuitorCheck, checkString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_SuitorCheck + checkString, value);
	}

	public static int[] KeysOfSuitorCheck()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_SuitorCheck);
	}

	public static int SuitorProgress
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_SuitorProgress); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_SuitorProgress, value); }
	}

	public static int GetSuitorTrait(int traitID)
	{
		return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_SuitorTrait + traitID.ToString());
	}

	public static void SetSuitorTrait(int traitID, int value)
	{
		string traitString = traitID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_SuitorTrait, traitString);
		PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_SuitorTrait + traitString, value);
	}

	public static int[] KeysOfSuitorTrait()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_SuitorTrait);
	}

	public static bool GetTopicDiscussed(int topicID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_TopicDiscussed + topicID.ToString());
	}

	public static void SetTopicDiscussed(int topicID, bool value)
	{
		string topicString = topicID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_TopicDiscussed, topicString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_TopicDiscussed + topicString, value);
	}

	public static int[] KeysOfTopicDiscussed()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_TopicDiscussed);
	}

	public static int GetTraitDemonstrated(int traitID)
	{
		return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_TraitDemonstrated + traitID.ToString());
	}

	public static void SetTraitDemonstrated(int traitID, int value)
	{
		string traitString = traitID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_TraitDemonstrated, traitString);
		PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_TraitDemonstrated + traitString, value);
	}

	public static int[] KeysOfTraitDemonstrated()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_TraitDemonstrated);
	}

	public static int RivalSabotaged
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_RivalSabotaged); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_RivalSabotaged, value); }
	}

	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_Affection);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_AffectionLevel);
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_ComplimentGiven, KeysOfComplimentGiven());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_SuitorCheck, KeysOfSuitorCheck());
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_SuitorProgress);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_RivalSabotaged);
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_SuitorTrait, KeysOfSuitorTrait());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_TopicDiscussed, KeysOfTopicDiscussed());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_TraitDemonstrated, KeysOfTraitDemonstrated());
	}
}

public static class EventGlobals
{
	const string Str_BefriendConversation = "BefriendConversation";
    const string Str_StalkerConversation = "StalkerConversation";
    const string Str_KidnapConversation = "KidnapConversation";
    const string Str_OsanaConversation = "OsanaConversation";
    const string Str_Event1 = "Event1";
	const string Str_Event2 = "Event2";
	const string Str_OsanaEvent1 = "OsanaEvent1";
	const string Str_OsanaEvent2 = "OsanaEvent2";
	const string Str_LivingRoom = "LivingRoom";

	public static bool BefriendConversation
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_BefriendConversation); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_BefriendConversation, value); }
	}

    public static bool StalkerConversation
    {
        get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_StalkerConversation); }
        set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_StalkerConversation, value); }
    }

    public static bool KidnapConversation
    {
        get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_KidnapConversation); }
        set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_KidnapConversation, value); }
    }

    public static bool OsanaConversation
    {
        get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_OsanaConversation); }
        set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_OsanaConversation, value); }
    }

    public static bool OsanaEvent1
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_OsanaEvent1); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_OsanaEvent1, value); }
	}

	public static bool OsanaEvent2
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_OsanaEvent2); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_OsanaEvent2, value); }
	}

	public static bool Event1
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_Event1); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_Event1, value); }
	}

	public static bool Event2
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_Event2); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_Event2, value); }
	}

    public static bool LivingRoom
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_LivingRoom); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_LivingRoom, value); }
	}

	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_BefriendConversation);
        Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_StalkerConversation);
        Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_KidnapConversation);
        Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_OsanaConversation);
        Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_OsanaEvent1);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_OsanaEvent2);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_Event1);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_Event2);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_LivingRoom);
	}
}

public static class GameGlobals
{
	const string Str_Profile = "Profile";
    const string Str_MostRecentSlot = "MostRecentSlot";

    const string Str_LoveSick = "LoveSick";
	const string Str_MasksBanned = "MasksBanned";
	const string Str_Paranormal = "Paranormal";
	const string Str_EasyMode = "EasyMode";
	const string Str_HardMode = "HardMode";
	const string Str_EmptyDemon = "EmptyDemon";
	const string Str_CensorBlood = "CensorBlood";
	const string Str_SpareUniform = "SpareUniform";
	const string Str_BlondeHair = "BlondeHair";
	const string Str_SenpaiMourning = "SenpaiMourning";
	const string Str_RivalEliminationID = "RivalEliminationID";
	const string Str_NonlethalElimination = "NonlethalElimination";
	const string Str_ReputationsInitialized = "ReputationsInitialized";
	const string Str_AnswerSheetUnavailable = "AnswerSheetUnavailable";
	const string Str_AlphabetMode = "AlphabetMode";
    const string Str_PoliceYesterday = "PoliceYesterday";
    const string Str_DarkEnding = "DarkEnding";

    public static int Profile
    {
        get { return PlayerPrefs.GetInt(Str_Profile); }
        set { PlayerPrefs.SetInt(Str_Profile, value); }
    }

    public static int MostRecentSlot
    {
        get { return PlayerPrefs.GetInt(Str_MostRecentSlot); }
        set { PlayerPrefs.SetInt(Str_MostRecentSlot, value); }
    }

    public static bool LoveSick
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_LoveSick); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_LoveSick, value); }
	}

	public static bool MasksBanned
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_MasksBanned); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_MasksBanned, value); }
	}

	public static bool Paranormal
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_Paranormal); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_Paranormal, value); }
	}

	public static bool EasyMode
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_EasyMode); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_EasyMode, value); }
	}

	public static bool HardMode
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_HardMode); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_HardMode, value); }
	}

	public static bool EmptyDemon
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_EmptyDemon); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_EmptyDemon, value); }
	}

	public static bool CensorBlood
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_CensorBlood); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_CensorBlood, value); }
	}

	public static bool SpareUniform
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_SpareUniform); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_SpareUniform, value); }
	}

	public static bool BlondeHair
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_BlondeHair); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_BlondeHair, value); }
	}

	public static bool SenpaiMourning
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiMourning); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiMourning, value); }
	}

	public static int RivalEliminationID
	{
		get { return PlayerPrefs.GetInt(Str_RivalEliminationID); }
		set { PlayerPrefs.SetInt(Str_RivalEliminationID, value); }
	}

	public static bool NonlethalElimination
	{
		get { return GlobalsHelper.GetBool(Str_NonlethalElimination); }
		set { GlobalsHelper.SetBool(Str_NonlethalElimination, value); }
	}

	public static bool ReputationsInitialized
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_ReputationsInitialized); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_ReputationsInitialized, value); }
	}

	public static bool AnswerSheetUnavailable
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_AnswerSheetUnavailable); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_AnswerSheetUnavailable, value); }
	}

	public static bool AlphabetMode
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_AlphabetMode); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_AlphabetMode, value); }
	}

    public static bool PoliceYesterday
    {
        get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_PoliceYesterday); }
        set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_PoliceYesterday, value); }
    }

    public static bool DarkEnding
    {
        get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_DarkEnding); }
        set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_DarkEnding, value); }
    }

    public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_LoveSick);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_MasksBanned);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_Paranormal);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_EasyMode);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_HardMode);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_EmptyDemon);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_CensorBlood);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_SpareUniform);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_BlondeHair);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiMourning);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_RivalEliminationID);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_NonlethalElimination);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_ReputationsInitialized);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_AnswerSheetUnavailable);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_AlphabetMode);
        Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_PoliceYesterday);
        Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_DarkEnding);
        Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_MostRecentSlot);
    }
}

public static class HomeGlobals
{
	const string Str_LateForSchool = "LateForSchool"; // Replaces "Late".
	const string Str_Night = "Night";
	const string Str_StartInBasement = "StartInBasement";
	const string Str_MiyukiDefeated = "MiyukiDefeated";

	public static bool LateForSchool
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_LateForSchool); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_LateForSchool, value); }
	}

	public static bool Night
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_Night); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_Night, value); }
	}

	public static bool StartInBasement
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_StartInBasement); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_StartInBasement, value); }
	}

	public static bool MiyukiDefeated
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_MiyukiDefeated); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_MiyukiDefeated, value); }
	}

	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_LateForSchool);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_Night);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_StartInBasement);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_MiyukiDefeated);
	}
}

public static class MissionModeGlobals
{
	const string Str_MissionCondition = "MissionCondition_";
	const string Str_MissionDifficulty = "MissionDifficulty";
	const string Str_MissionMode = "MissionMode";
	const string Str_MissionRequiredClothing = "MissionRequiredClothing";
	const string Str_MissionRequiredDisposal = "MissionRequiredDisposal";
	const string Str_MissionRequiredWeapon = "MissionRequiredWeapon";
	const string Str_MissionTarget = "MissionTarget";
	const string Str_MissionTargetName = "MissionTargetName";
	const string Str_NemesisDifficulty = "NemesisDifficulty";
	const string Str_NemesisAggression = "NemesisAggression";
	const string Str_MultiMission = "MultiMission";

	public static int GetMissionCondition(int id)
	{
		return PlayerPrefs.GetInt(Str_MissionCondition + id.ToString());
	}

	public static void SetMissionCondition(int id, int value)
	{
		string idString = id.ToString();
		KeysHelper.AddIfMissing(Str_MissionCondition, idString);
		PlayerPrefs.SetInt(Str_MissionCondition + idString, value);
	}

	public static int[] KeysOfMissionCondition()
	{
		return KeysHelper.GetIntegerKeys(Str_MissionCondition);
	}

	public static int MissionDifficulty
	{
		get { return PlayerPrefs.GetInt(Str_MissionDifficulty); }
		set { PlayerPrefs.SetInt(Str_MissionDifficulty, value); }
	}

	public static bool MissionMode
	{
		get { return GlobalsHelper.GetBool(Str_MissionMode); }
		set { GlobalsHelper.SetBool(Str_MissionMode, value); }
	}

	public static bool MultiMission
	{
		get { return GlobalsHelper.GetBool(Str_MultiMission); }
		set { GlobalsHelper.SetBool(Str_MultiMission, value); }
	}

	public static int MissionRequiredClothing
	{
		get { return PlayerPrefs.GetInt(Str_MissionRequiredClothing); }
		set { PlayerPrefs.SetInt(Str_MissionRequiredClothing, value); }
	}

	public static int MissionRequiredDisposal
	{
		get { return PlayerPrefs.GetInt(Str_MissionRequiredDisposal); }
		set { PlayerPrefs.SetInt(Str_MissionRequiredDisposal, value); }
	}

	public static int MissionRequiredWeapon
	{
		get { return PlayerPrefs.GetInt(Str_MissionRequiredWeapon); }
		set { PlayerPrefs.SetInt(Str_MissionRequiredWeapon, value); }
	}

	public static int MissionTarget
	{
		get { return PlayerPrefs.GetInt(Str_MissionTarget); }
		set { PlayerPrefs.SetInt(Str_MissionTarget, value); }
	}

	public static string MissionTargetName
	{
		get { return PlayerPrefs.GetString(Str_MissionTargetName); }
		set { PlayerPrefs.SetString(Str_MissionTargetName, value); }
	}

	public static int NemesisDifficulty
	{
		get { return PlayerPrefs.GetInt(Str_NemesisDifficulty); }
		set { PlayerPrefs.SetInt(Str_NemesisDifficulty, value); }
	}

	public static bool NemesisAggression
	{
		get { return GlobalsHelper.GetBool(Str_NemesisAggression); }
		set { GlobalsHelper.SetBool(Str_NemesisAggression, value); }
	}

	public static void DeleteAll()
	{
		Globals.DeleteCollection(Str_MissionCondition, KeysOfMissionCondition());
		Globals.Delete(Str_MissionDifficulty);
		Globals.Delete(Str_MissionMode);
		Globals.Delete(Str_MissionRequiredClothing);
		Globals.Delete(Str_MissionRequiredDisposal);
		Globals.Delete(Str_MissionRequiredWeapon);
		Globals.Delete(Str_MissionTarget);
		Globals.Delete(Str_MissionTargetName);
		Globals.Delete(Str_NemesisDifficulty);
		Globals.Delete(Str_NemesisAggression);
		Globals.Delete(Str_MultiMission);
	}
}

public static class OptionGlobals
{
	const string Str_DisableBloom = "DisableBloom";
	const string Str_DisableFarAnimations = "DisableFarAnimations";
	const string Str_DisableOutlines = "DisableOutlines";
	const string Str_DisablePostAliasing = "DisablePostAliasing";
	const string Str_EnableShadows = "EnableShadows";
	const string Str_DisableObscurance = "DisableObscurance";
	const string Str_DrawDistance = "DrawDistance";
	const string Str_DrawDistanceLimit = "DrawDistanceLimit";
	const string Str_Fog = "Fog";
	const string Str_FPSIndex = "FPSIndex";
	const string Str_HighPopulation = "HighPopulation";
	const string Str_LowDetailStudents = "LowDetailStudents";
	const string Str_ParticleCount = "ParticleCount";
	const string Str_RimLight = "RimLight";
	const string Str_DepthOfField = "DepthOfField";
	const string Str_Sensitivity = "Sensitivity";
	const string Str_InvertAxis = "InvertAxis";
	const string Str_TutorialsOff = "TutorialsOff";
	const string Str_ToggleRun = "ToggleRun";

	public static bool DisableBloom
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_DisableBloom); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_DisableBloom, value); }
	}

	public static int DisableFarAnimations
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_DisableFarAnimations); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_DisableFarAnimations, value); }
	}

	public static bool DisableOutlines
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_DisableOutlines); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_DisableOutlines, value); }
	}

	public static bool DisablePostAliasing
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_DisablePostAliasing); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_DisablePostAliasing, value); }
	}

	public static bool EnableShadows
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_EnableShadows); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_EnableShadows, value); }
	}

	public static bool DisableObscurance
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_DisableObscurance); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_DisableObscurance, value); }
	}

	public static int DrawDistance
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_DrawDistance); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_DrawDistance, value); }
	}

	public static int DrawDistanceLimit
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_DrawDistanceLimit); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_DrawDistanceLimit, value); }
	}

	public static bool Fog
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_Fog); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_Fog, value); }
	}

	public static int FPSIndex
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_FPSIndex); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_FPSIndex, value); }
	}

	public static bool HighPopulation
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_HighPopulation); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_HighPopulation, value); }
	}

	public static int LowDetailStudents
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_LowDetailStudents); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_LowDetailStudents, value); }
	}

	public static int ParticleCount
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_ParticleCount); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_ParticleCount, value); }
	}

	public static bool RimLight
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_RimLight); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_RimLight, value); }
	}

	public static bool DepthOfField
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_DepthOfField); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_DepthOfField, value); }
	}

	public static int Sensitivity
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_Sensitivity); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_Sensitivity, value); }
	}

	public static bool InvertAxis
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_InvertAxis); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_InvertAxis, value); }
	}

	public static bool TutorialsOff
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_TutorialsOff); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_TutorialsOff, value); }
	}

	public static bool ToggleRun
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_ToggleRun); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_ToggleRun, value); }
	}

	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_DisableBloom);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_DisableFarAnimations);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_DisableOutlines);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_DisablePostAliasing);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_EnableShadows);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_DisableObscurance);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_DrawDistance);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_DrawDistanceLimit);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_Fog);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_FPSIndex);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_HighPopulation);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_LowDetailStudents);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_ParticleCount);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_RimLight);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_DepthOfField);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_Sensitivity);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_InvertAxis);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_TutorialsOff);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_ToggleRun);
	}
}

public static class PlayerGlobals
{
	const string Str_Money = "Money";
	const string Str_Alerts = "Alerts";
	const string Str_BullyPhoto = "BullyPhoto_";
	const string Str_Enlightenment = "Enlightenment";
	const string Str_EnlightenmentBonus = "EnlightenmentBonus";
	const string Str_Friends = "Friends";
	const string Str_Headset = "Headset";
	const string Str_FakeID = "FakeID";
	const string Str_RaibaruLoner = "RaibaruLoner";
	const string Str_Kills = "Kills";
	const string Str_Numbness = "Numbness";
	const string Str_NumbnessBonus = "NumbnessBonus";
	const string Str_PantiesEquipped = "PantiesEquipped";
	const string Str_PantyShots = "PantyShots";
	const string Str_Photo = "Photo_";
	const string Str_PhotoOnCorkboard = "PhotoOnCorkboard_";
	const string Str_PhotoPosition = "PhotoPosition_";
	const string Str_PhotoRotation = "PhotoRotation_";
	const string Str_Reputation = "Reputation";
	const string Str_Seduction = "Seduction";
	const string Str_SeductionBonus = "SeductionBonus";
	const string Str_SenpaiPhoto = "SenpaiPhoto_";
	const string Str_SenpaiShots = "SenpaiShots";
	const string Str_SocialBonus = "SocialBonus";
	const string Str_SpeedBonus = "SpeedBonus";
	const string Str_StealthBonus = "StealthBonus";
	const string Str_StudentFriend = "StudentFriend_"; // Replaces StudentID + "_Friend".
	const string Str_StudentPantyShot = "StudentPantyShot_"; // Replaces StudentName + "PantyShot".
	const string Str_ShrineCollectible = "ShrineCollectible_";
	const string Str_UsingGamepad = "UsingGamepad";

	public static float Money
	{
		get { return PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_" + Str_Money); }
		set { PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_" + Str_Money, value); }
	}

	public static int Alerts
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_Alerts); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_Alerts, value); }
	}

	public static int Enlightenment
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_Enlightenment); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_Enlightenment, value); }
	}

	public static int EnlightenmentBonus
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_EnlightenmentBonus); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_EnlightenmentBonus, value); }
	}

	public static int Friends
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_Friends); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_Friends, value); }
	}

	public static bool Headset
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_Headset); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_Headset, value); }
	}

	public static bool FakeID
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_FakeID); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_FakeID, value); }
	}

	public static bool RaibaruLoner
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_RaibaruLoner); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_RaibaruLoner, value); }
	}

	public static int Kills
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_Kills); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_Kills, value); }
	}

	public static int Numbness
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_Numbness); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_Numbness, value); }
	}

	public static int NumbnessBonus
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_NumbnessBonus); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_NumbnessBonus, value); }
	}

	public static int PantiesEquipped
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_PantiesEquipped); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_PantiesEquipped, value); }
	}

	public static int PantyShots
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_PantyShots); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_PantyShots, value); }
	}

	public static bool GetPhoto(int photoID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_Photo + photoID.ToString());
	}

	public static void SetPhoto(int photoID, bool value)
	{
		string photoString = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_Photo, photoString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_Photo + photoString, value);
	}

	public static int[] KeysOfPhoto()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_Photo);
	}

	public static bool GetPhotoOnCorkboard(int photoID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_PhotoOnCorkboard + photoID.ToString());
	}

	public static void SetPhotoOnCorkboard(int photoID, bool value)
	{
		string photoString = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_PhotoOnCorkboard, photoString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_PhotoOnCorkboard + photoString, value);
	}

	public static int[] KeysOfPhotoOnCorkboard()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_PhotoOnCorkboard);
	}

	public static Vector2 GetPhotoPosition(int photoID)
	{
		return GlobalsHelper.GetVector2("Profile_" + GameGlobals.Profile + "_" + Str_PhotoPosition + photoID.ToString());
	}

	public static void SetPhotoPosition(int photoID, Vector2 value)
	{
		string photoString = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_PhotoPosition, photoString);
		GlobalsHelper.SetVector2("Profile_" + GameGlobals.Profile + "_" + Str_PhotoPosition + photoString, value);
	}

	public static int[] KeysOfPhotoPosition()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_PhotoPosition);
	}

	public static float GetPhotoRotation(int photoID)
	{
		return PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_" + Str_PhotoRotation + photoID.ToString());
	}

	public static void SetPhotoRotation(int photoID, float value)
	{
		string photoString = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_PhotoRotation, photoString);
		PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_" + Str_PhotoRotation + photoString, value);
	}

	public static int[] KeysOfPhotoRotation()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_PhotoRotation);
	}

	public static float Reputation
	{
		get { return PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_" + Str_Reputation); }
		set { PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_" + Str_Reputation, value); }
	}

	public static int Seduction
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_Seduction); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_Seduction, value); }
	}

	public static int SeductionBonus
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_SeductionBonus); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_SeductionBonus, value); }
	}

	public static bool GetSenpaiPhoto(int photoID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiPhoto + photoID.ToString());
	}

	public static void SetSenpaiPhoto(int photoID, bool value)
	{
		string photoString = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiPhoto, photoString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiPhoto + photoString, value);
	}

	public static int GetBullyPhoto(int photoID)
	{
		return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_BullyPhoto + photoID.ToString());
	}

	public static void SetBullyPhoto(int photoID, int value)
	{
		PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_BullyPhoto + photoID.ToString(), value);
	}

	public static int[] KeysOfSenpaiPhoto()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiPhoto);
	}

	public static int SenpaiShots
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiShots); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiShots, value); }
	}

	public static int SocialBonus
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_SocialBonus); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_SocialBonus, value); }
	}

	public static int SpeedBonus
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_SpeedBonus); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_SpeedBonus, value); }
	}

	public static int StealthBonus
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_StealthBonus); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_StealthBonus, value); }
	}

	public static bool GetStudentFriend(int studentID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_StudentFriend + studentID.ToString());
	}

	public static void SetStudentFriend(int studentID, bool value)
	{
		string studentString = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_StudentFriend, studentString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_StudentFriend + studentString, value);
	}

	public static int[] KeysOfStudentFriend()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_StudentFriend);
	}

	public static bool GetStudentPantyShot(string studentName)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_StudentPantyShot + studentName);
	}

	public static void SetStudentPantyShot(string studentName, bool value)
	{
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_StudentPantyShot, studentName);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_StudentPantyShot + studentName, value);
	}

	public static string[] KeysOfStudentPantyShot()
	{
		return KeysHelper.GetStringKeys("Profile_" + GameGlobals.Profile + "_" + Str_StudentPantyShot);
	}

	public static string[] KeysOfShrineCollectible()
	{
		return KeysHelper.GetStringKeys("Profile_" + GameGlobals.Profile + "_" + Str_ShrineCollectible);
	}

	public static bool GetShrineCollectible(int ID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_ShrineCollectible + ID.ToString());
	}

	public static void SetShrineCollectible(int ID, bool value)
	{
		string IDString = ID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_ShrineCollectible, IDString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_ShrineCollectible + IDString, value);
	}

	public static bool UsingGamepad
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_UsingGamepad); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_UsingGamepad, value); }
	}

    public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_Money);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_Alerts);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_Enlightenment);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_EnlightenmentBonus);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_Friends);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_Headset);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_FakeID);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_RaibaruLoner);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_Kills);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_Numbness);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_NumbnessBonus);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_PantiesEquipped);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_PantyShots);
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_Photo, KeysOfPhoto());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_PhotoOnCorkboard, KeysOfPhotoOnCorkboard());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_PhotoPosition, KeysOfPhotoPosition());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_PhotoRotation, KeysOfPhotoRotation());
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_Reputation);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_Seduction);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_SeductionBonus);
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiPhoto, KeysOfSenpaiPhoto());
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiShots);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_SocialBonus);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_SpeedBonus);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_StealthBonus);
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_StudentFriend, KeysOfStudentFriend());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_StudentPantyShot, KeysOfStudentPantyShot());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_ShrineCollectible, KeysOfShrineCollectible());
	}
}

public static class PoseModeGlobals
{
	const string Str_PosePosition = "PosePosition"; // Replaces "Position {X,Y,Z}".
	const string Str_PoseRotation = "PoseRotation"; // Replaces "Rotation {X,Y,Z}".
	const string Str_PoseScale = "PoseScale"; // Replaces "Scale {X,Y,Z}".

	public static Vector3 PosePosition
	{
		get { return GlobalsHelper.GetVector3("Profile_" + GameGlobals.Profile + "_" + Str_PosePosition); }
		set { GlobalsHelper.SetVector3("Profile_" + GameGlobals.Profile + "_" + Str_PosePosition, value); }
	}

	public static Vector3 PoseRotation
	{
		get { return GlobalsHelper.GetVector3("Profile_" + GameGlobals.Profile + "_" + Str_PoseRotation); }
		set { GlobalsHelper.SetVector3("Profile_" + GameGlobals.Profile + "_" + Str_PoseRotation, value); }
	}

	public static Vector3 PoseScale
	{
		get { return GlobalsHelper.GetVector3("Profile_" + GameGlobals.Profile + "_" + Str_PoseScale); }
		set { GlobalsHelper.SetVector3("Profile_" + GameGlobals.Profile + "_" + Str_PoseScale, value); }
	}

	public static void DeleteAll()
	{
		GlobalsHelper.DeleteVector3("Profile_" + GameGlobals.Profile + "_" + Str_PosePosition);
		GlobalsHelper.DeleteVector3("Profile_" + GameGlobals.Profile + "_" + Str_PoseRotation);
		GlobalsHelper.DeleteVector3("Profile_" + GameGlobals.Profile + "_" + Str_PoseScale);
	}
}

public static class SaveFileGlobals
{
	const string Str_CurrentSaveFile = "CurrentSaveFile";

	public static int CurrentSaveFile
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_CurrentSaveFile); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_CurrentSaveFile, value); }
	}

	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_CurrentSaveFile);
	}
}

public static class SchemeGlobals
{
	const string Str_CurrentScheme = "CurrentScheme";
	const string Str_DarkSecret = "DarkSecret";
    const string Str_HelpingKokona = "HelpingKokona";
    const string Str_SchemePreviousStage = "SchemePreviousStage_"; // Replaces "Scheme_" + ID + "_PreviousStage".
	const string Str_SchemeStage = "SchemeStage_"; // Replaces "Scheme_" + ID + "_Stage".
	const string Str_SchemeStatus = "SchemeStatus_"; // Replaces "Scheme_" + ID + "_Status".
	const string Str_SchemeUnlocked = "SchemeUnlocked_"; // Replaces "Scheme_" + ID + "_Unlocked".
	const string Str_ServicePurchased = "ServicePurchased_"; // Replaces "Service_" + ID + "_Purchased".

    public static int CurrentScheme
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_CurrentScheme); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_CurrentScheme, value); }
	}

	public static bool DarkSecret
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_DarkSecret); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_DarkSecret, value); }
	}

    public static bool HelpingKokona
    {
        get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_HelpingKokona); }
        set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_HelpingKokona, value); }
    }

    public static int GetSchemePreviousStage(int schemeID)
	{
		return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_SchemePreviousStage + schemeID.ToString());
	}

	public static void SetSchemePreviousStage(int schemeID, int value)
	{
		string schemeString = schemeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_SchemePreviousStage, schemeString);
		PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_SchemePreviousStage + schemeString, value);
	}

	public static int[] KeysOfSchemePreviousStage()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_SchemePreviousStage);
	}

	public static int GetSchemeStage(int schemeID)
	{
		return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_SchemeStage + schemeID.ToString());
	}

	public static void SetSchemeStage(int schemeID, int value)
	{
		string schemeString = schemeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_SchemeStage, schemeString);
		PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_SchemeStage + schemeString, value);
	}

	public static int[] KeysOfSchemeStage()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_SchemeStage);
	}

	public static bool GetSchemeStatus(int schemeID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_SchemeStatus + schemeID.ToString());
	}

	public static void SetSchemeStatus(int schemeID, bool value)
	{
		string schemeString = schemeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_SchemeStatus, schemeString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_SchemeStatus + schemeString, value);
	}

	public static int[] KeysOfSchemeStatus()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_SchemeStatus);
	}

	public static bool GetSchemeUnlocked(int schemeID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_SchemeUnlocked + schemeID.ToString());
	}

	public static void SetSchemeUnlocked(int schemeID, bool value)
	{
		string schemeString = schemeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_SchemeUnlocked, schemeString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_SchemeUnlocked + schemeString, value);
	}

	public static int[] KeysOfSchemeUnlocked()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_SchemeUnlocked);
	}

	public static bool GetServicePurchased(int serviceID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_ServicePurchased + serviceID.ToString());
	}

	public static void SetServicePurchased(int serviceID, bool value)
	{
		string serviceString = serviceID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_ServicePurchased, serviceString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_ServicePurchased + serviceString, value);
	}

	public static int[] KeysOfServicePurchased()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_ServicePurchased);
	}

	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_CurrentScheme);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_DarkSecret);
        Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_HelpingKokona);
        Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_SchemePreviousStage, KeysOfSchemePreviousStage());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_SchemeStage, KeysOfSchemeStage());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_SchemeStatus, KeysOfSchemeStatus());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_SchemeUnlocked, KeysOfSchemeUnlocked());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_ServicePurchased, KeysOfServicePurchased());
	}
}

public static class SchoolGlobals
{
	const string Str_DemonActive = "DemonActive_"; // Replaces "Demon_" + ID + "_Active".
	const string Str_GardenGraveOccupied = "GardenGraveOccupied_"; // Replaces "GardenGrave_" + ID + "_Occupied".
	const string Str_KidnapVictim = "KidnapVictim";
	const string Str_Population = "Population";
	const string Str_RoofFence = "RoofFence";
	const string Str_SchoolAtmosphere = "SchoolAtmosphere";
	const string Str_SchoolAtmosphereSet = "SchoolAtmosphereSet";
	const string Str_ReactedToGameLeader = "ReactedToGameLeader";
	const string Str_SCP = "SCP";
	const string Str_HighSecurity = "HighSecurity";

	public static bool GetDemonActive(int demonID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_DemonActive + demonID.ToString());
	}

	public static void SetDemonActive(int demonID, bool value)
	{
		string demonString = demonID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_DemonActive, demonString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_DemonActive + demonString, value);
	}

	public static int[] KeysOfDemonActive()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_DemonActive);
	}

	public static bool GetGardenGraveOccupied(int graveID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_GardenGraveOccupied + graveID.ToString());
	}

	public static void SetGardenGraveOccupied(int graveID, bool value)
	{
		string graveString = graveID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_GardenGraveOccupied, graveString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_GardenGraveOccupied + graveString, value);
	}

	public static int[] KeysOfGardenGraveOccupied()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_GardenGraveOccupied);
	}

	public static int KidnapVictim
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_KidnapVictim); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_KidnapVictim, value); }
	}

	public static int Population
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_Population); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_Population, value); }
	}

	public static bool RoofFence
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_RoofFence); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_RoofFence, value); }
	}

	public static float SchoolAtmosphere
	{
		get { return PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_" + Str_SchoolAtmosphere); }
		set { PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_" + Str_SchoolAtmosphere, value); }
	}

	public static bool SchoolAtmosphereSet
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_SchoolAtmosphereSet); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_SchoolAtmosphereSet, value); }
	}

	public static bool ReactedToGameLeader
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_ReactedToGameLeader); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_ReactedToGameLeader, value); }
	}

	public static bool HighSecurity
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_HighSecurity); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_HighSecurity, value); }
	}

	public static bool SCP
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_SCP); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_SCP, value); }
	}

	public static void DeleteAll()
	{
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_DemonActive, KeysOfDemonActive());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_GardenGraveOccupied, KeysOfGardenGraveOccupied());
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_KidnapVictim);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_Population);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_RoofFence);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_SchoolAtmosphere);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_SchoolAtmosphereSet);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_ReactedToGameLeader);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_HighSecurity);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_SCP);
	}
}

public static class SenpaiGlobals
{
	const string Str_CustomSenpai = "CustomSenpai";
	const string Str_SenpaiEyeColor = "SenpaiEyeColor";
	const string Str_SenpaiEyeWear = "SenpaiEyeWear";
	const string Str_SenpaiFacialHair = "SenpaiFacialHair";
	const string Str_SenpaiHairColor = "SenpaiHairColor";
	const string Str_SenpaiHairStyle = "SenpaiHairStyle";
	const string Str_SenpaiSkinColor = "SenpaiSkinColor";

	public static bool CustomSenpai
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_CustomSenpai); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_CustomSenpai, value); }
	}

	public static string SenpaiEyeColor
	{
		get { return PlayerPrefs.GetString("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiEyeColor); }
		set { PlayerPrefs.SetString("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiEyeColor, value); }
	}

	public static int SenpaiEyeWear
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiEyeWear); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiEyeWear, value); }
	}

	public static int SenpaiFacialHair
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiFacialHair); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiFacialHair, value); }
	}

	public static string SenpaiHairColor
	{
		get { return PlayerPrefs.GetString("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiHairColor); }
		set { PlayerPrefs.SetString("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiHairColor, value); }
	}

	public static int SenpaiHairStyle
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiHairStyle); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiHairStyle, value); }
	}

	public static int SenpaiSkinColor
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiSkinColor); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiSkinColor, value); }
	}

	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_CustomSenpai);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiEyeColor);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiEyeWear);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiFacialHair);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiHairColor);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiHairStyle);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_SenpaiSkinColor);
	}
}

public static class StudentGlobals
{
	const string Str_CustomSuitor = "CustomSuitor";
	const string Str_CustomSuitorAccessory = "CustomSuitorAccessory";
	const string Str_CustomSuitorBlonde = "CustomSuitorBlonde";
	const string Str_CustomSuitorBlack = "CustomSuitorBlack";
	const string Str_CustomSuitorEyewear = "CustomSuitorEyewear";
	const string Str_CustomSuitorHair = "CustomSuitorHair";
	const string Str_CustomSuitorJewelry = "CustomSuitorJewelry";
	const string Str_CustomSuitorTan = "CustomSuitorTan";
	const string Str_ExpelProgress = "ExpelProgress";
	const string Str_FemaleUniform = "FemaleUniform";
	const string Str_MaleUniform = "MaleUniform";
	const string Str_StudentAccessory = "StudentAccessory_"; // Replaces "Student_" + ID + "_Accessory".
	const string Str_StudentArrested = "StudentArrested_"; // Replaces "Student_" + ID + "_Arrested".
	const string Str_StudentBroken = "StudentBroken_"; // Replaces "Student_" + ID + "_Broken".
	const string Str_StudentBustSize = "StudentBustSize_"; // Replaces "Student_" + ID + "_BustSize".
	const string Str_StudentColor = "StudentColor_"; // Replaces "Student_" + ID + "_Color{R,G,B}".
	const string Str_StudentDead = "StudentDead_"; // Replaces "Student_" + ID + "_Dead".
	const string Str_StudentDying = "StudentDying_"; // Replaces "Student_" + ID + "_Dying".
	const string Str_StudentExpelled = "StudentExpelled_"; // Replaces "Student_" + ID + "_Expelled".
	const string Str_StudentExposed = "StudentExposed_"; // Replaces "Student_" + ID + "_Exposed".
	const string Str_StudentEyeColor = "StudentEyeColor_"; // Replaces "Student_" + ID + "_EyeColor{R,G,B}".
	const string Str_StudentGrudge = "StudentGrudge_"; // Replaces "Student_" + ID + "_Grudge".
	const string Str_StudentHairstyle = "StudentHairstyle_"; // Replaces "Student_" + ID + "_Hairstyle".
	const string Str_StudentKidnapped = "StudentKidnapped_"; // Replaces "Student_" + ID + "_Kidnapped".
	const string Str_StudentMissing = "StudentMissing_"; // Replaces "Student_" + ID + "_Missing".
	const string Str_StudentName = "StudentName_"; // Replaces "Student_" + ID + "_Name".
	const string Str_StudentPhotographed = "StudentPhotographed_"; // Replaces "Student_" + ID + "_Photographed".
	const string Str_StudentPhoneStolen = "StudentPhoneStolen_"; // Replaces "Student_" + ID + "_PhoneStolen".
	const string Str_StudentReplaced = "StudentReplaced_"; // Replaces "Student_" + ID + "_Replaced".
	const string Str_StudentReputation = "StudentReputation_"; // Replaces "Student_" + ID + "_Reputation".
	const string Str_StudentSanity = "StudentSanity_"; // Replaces "Student_" + ID + "_Sanity".
	const string Str_StudentSlave = "StudentSlave"; // Replaces "Student_" + ID + "_Slave".
	const string Str_FragileSlave = "FragileSlave";
	const string Str_FragileTarget = "FragileTarget";
	const string Str_ReputationTriangle = "ReputatonTriangle";

	const string Str_MemorialStudents = "MemorialStudents";
	const string Str_MemorialStudent1 = "MemorialStudent1";
	const string Str_MemorialStudent2 = "MemorialStudent2";
	const string Str_MemorialStudent3 = "MemorialStudent3";
	const string Str_MemorialStudent4 = "MemorialStudent4";
	const string Str_MemorialStudent5 = "MemorialStudent5";
	const string Str_MemorialStudent6 = "MemorialStudent6";
	const string Str_MemorialStudent7 = "MemorialStudent7";
	const string Str_MemorialStudent8 = "MemorialStudent8";
	const string Str_MemorialStudent9 = "MemorialStudent9";

	public static bool CustomSuitor
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_CustomSuitor); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_CustomSuitor, value); }
	}

	public static int CustomSuitorAccessory
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_CustomSuitorAccessory); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_CustomSuitorAccessory, value); }
	}

	public static bool CustomSuitorBlonde
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_CustomSuitorBlonde); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_CustomSuitorBlonde, value); }
	}

	public static bool CustomSuitorBlack
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_CustomSuitorBlack); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_CustomSuitorBlack, value); }
	}

	public static int CustomSuitorEyewear
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_CustomSuitorEyewear); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_CustomSuitorEyewear, value); }
	}

	public static int CustomSuitorHair
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_CustomSuitorHair); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_CustomSuitorHair, value); }
	}

	public static int CustomSuitorJewelry
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_CustomSuitorJewelry); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_CustomSuitorJewelry, value); }
	}

	public static bool CustomSuitorTan
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_CustomSuitorTan); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_CustomSuitorTan, value); }
	}

	public static int ExpelProgress
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_ExpelProgress); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_ExpelProgress, value); }
	}

	public static int FemaleUniform
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_FemaleUniform); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_FemaleUniform, value); }
	}

	public static int MaleUniform
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_MaleUniform); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_MaleUniform, value); }
	}

	public static int MemorialStudents
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_MemorialStudents); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_MemorialStudents, value); }
	}

	public static int MemorialStudent1
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_MemorialStudent1); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_MemorialStudent1, value); }
	}
	public static int MemorialStudent2
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_MemorialStudent2); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_MemorialStudent2, value); }
	}
	public static int MemorialStudent3
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_MemorialStudent3); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_MemorialStudent3, value); }
	}
	public static int MemorialStudent4
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_MemorialStudent4); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_MemorialStudent4, value); }
	}
	public static int MemorialStudent5
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_MemorialStudent5); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_MemorialStudent5, value); }
	}
	public static int MemorialStudent6
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_MemorialStudent6); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_MemorialStudent6, value); }
	}
	public static int MemorialStudent7
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_MemorialStudent7); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_MemorialStudent7, value); }
	}
	public static int MemorialStudent8
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_MemorialStudent8); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_MemorialStudent8, value); }
	}
	public static int MemorialStudent9
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_MemorialStudent9); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_MemorialStudent9, value); }
	}

	public static string GetStudentAccessory(int studentID)
	{
		return PlayerPrefs.GetString("Profile_" + GameGlobals.Profile + "_" + Str_StudentAccessory + studentID.ToString());
	}

	public static void SetStudentAccessory(int studentID, string value)
	{
		string studentString = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_StudentAccessory, studentString);
		PlayerPrefs.SetString("Profile_" + GameGlobals.Profile + "_" + Str_StudentAccessory + studentString, value);
	}

	public static int[] KeysOfStudentAccessory()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_StudentAccessory);
	}

	public static bool GetStudentArrested(int studentID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_StudentArrested + studentID.ToString());
	}

	public static void SetStudentArrested(int studentID, bool value)
	{
		string studentString = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_StudentArrested, studentString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_StudentArrested + studentString, value);
	}

	public static int[] KeysOfStudentArrested()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_StudentArrested);
	}

	public static bool GetStudentBroken(int studentID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_StudentBroken + studentID.ToString());
	}

	public static void SetStudentBroken(int studentID, bool value)
	{
		string studentString = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_StudentBroken, studentString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_StudentBroken + studentString, value);
	}

	public static int[] KeysOfStudentBroken()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_StudentBroken);
	}

	public static float GetStudentBustSize(int studentID)
	{
		return PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_" + Str_StudentBustSize + studentID.ToString());
	}

	public static void SetStudentBustSize(int studentID, float value)
	{
		string studentString = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_StudentBustSize, studentString);
		PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_" + Str_StudentBustSize + studentString, value);
	}

	public static int[] KeysOfStudentBustSize()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_StudentBustSize);
	}

	public static Color GetStudentColor(int studentID)
	{
		return GlobalsHelper.GetColor("Profile_" + GameGlobals.Profile + "_" + Str_StudentColor + studentID.ToString());
	}

	public static void SetStudentColor(int studentID, Color value)
	{
		string studentString = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_StudentColor, studentString);
		GlobalsHelper.SetColor("Profile_" + GameGlobals.Profile + "_" + Str_StudentColor + studentString, value);
	}

	public static int[] KeysOfStudentColor()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_StudentColor);
	}

	public static bool GetStudentDead(int studentID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_StudentDead + studentID.ToString());
	}

	public static void SetStudentDead(int studentID, bool value)
	{
		string studentString = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_StudentDead, studentString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_StudentDead + studentString, value);
	}

	public static int[] KeysOfStudentDead()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_StudentDead);
	}

	public static bool GetStudentDying(int studentID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_StudentDying + studentID.ToString());
	}

	public static void SetStudentDying(int studentID, bool value)
	{
		string studentString = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_StudentDying, studentString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_StudentDying + studentString, value);
	}

	public static int[] KeysOfStudentDying()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_StudentDying);
	}

	public static bool GetStudentExpelled(int studentID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_StudentExpelled + studentID.ToString());
	}

	public static void SetStudentExpelled(int studentID, bool value)
	{
		string studentString = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_StudentExpelled, studentString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_StudentExpelled + studentString, value);
	}

	public static int[] KeysOfStudentExpelled()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_StudentExpelled);
	}

	public static bool GetStudentExposed(int studentID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_StudentExposed + studentID.ToString());
	}

	public static void SetStudentExposed(int studentID, bool value)
	{
		string studentString = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_StudentExposed, studentString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_StudentExposed + studentString, value);
	}

	public static int[] KeysOfStudentExposed()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_StudentExposed);
	}

	public static Color GetStudentEyeColor(int studentID)
	{
		return GlobalsHelper.GetColor("Profile_" + GameGlobals.Profile + "_" + Str_StudentEyeColor + studentID.ToString());
	}

	public static void SetStudentEyeColor(int studentID, Color value)
	{
		string studentString = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_StudentEyeColor, studentString);
		GlobalsHelper.SetColor("Profile_" + GameGlobals.Profile + "_" + Str_StudentEyeColor + studentString, value);
	}

	public static int[] KeysOfStudentEyeColor()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_StudentEyeColor);
	}

	public static bool GetStudentGrudge(int studentID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_StudentGrudge + studentID.ToString());
	}

	public static void SetStudentGrudge(int studentID, bool value)
	{
		string studentString = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_StudentGrudge, studentString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_StudentGrudge + studentString, value);
	}

	public static int[] KeysOfStudentGrudge()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_StudentGrudge);
	}

	public static string GetStudentHairstyle(int studentID)
	{
		return PlayerPrefs.GetString("Profile_" + GameGlobals.Profile + "_" + Str_StudentHairstyle + studentID.ToString());
	}

	public static void SetStudentHairstyle(int studentID, string value)
	{
		string studentString = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_StudentHairstyle, studentString);
		PlayerPrefs.SetString("Profile_" + GameGlobals.Profile + "_" + Str_StudentHairstyle + studentString, value);
	}

	public static int[] KeysOfStudentHairstyle()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_StudentHairstyle);
	}

	public static bool GetStudentKidnapped(int studentID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_StudentKidnapped + studentID.ToString());
	}

	public static void SetStudentKidnapped(int studentID, bool value)
	{
		string studentString = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_StudentKidnapped, studentString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_StudentKidnapped + studentString, value);
	}

	public static int[] KeysOfStudentKidnapped()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_StudentKidnapped);
	}

	public static bool GetStudentMissing(int studentID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_StudentMissing + studentID.ToString());
	}

	public static void SetStudentMissing(int studentID, bool value)
	{
		string studentString = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_StudentMissing, studentString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_StudentMissing + studentString, value);
	}

	public static int[] KeysOfStudentMissing()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_StudentMissing);
	}

	public static string GetStudentName(int studentID)
	{
		return PlayerPrefs.GetString("Profile_" + GameGlobals.Profile + "_" + Str_StudentName + studentID.ToString());
	}

	public static void SetStudentName(int studentID, string value)
	{
		string studentString = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_StudentName, studentString);
		PlayerPrefs.SetString("Profile_" + GameGlobals.Profile + "_" + Str_StudentName + studentString, value);
	}

	public static int[] KeysOfStudentName()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_StudentName);
	}

	public static bool GetStudentPhotographed(int studentID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_StudentPhotographed + studentID.ToString());
	}

	public static void SetStudentPhotographed(int studentID, bool value)
	{
		string studentString = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_StudentPhotographed, studentString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_StudentPhotographed + studentString, value);
	}

	public static int[] KeysOfStudentPhotographed()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_StudentPhotographed);
	}

	public static bool GetStudentPhoneStolen(int studentID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_StudentPhoneStolen + studentID.ToString());
	}

	public static void SetStudentPhoneStolen(int studentID, bool value)
	{
		string studentString = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_StudentPhoneStolen, studentString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_StudentPhoneStolen + studentString, value);
	}

	public static int[] KeysOfStudentPhoneStolen()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_StudentPhoneStolen);
	}

	public static bool GetStudentReplaced(int studentID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_StudentReplaced + studentID.ToString());
	}

	public static void SetStudentReplaced(int studentID, bool value)
	{
		string studentString = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_StudentReplaced, studentString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_StudentReplaced + studentString, value);
	}

	public static int[] KeysOfStudentReplaced()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_StudentReplaced);
	}

	public static int GetStudentReputation(int studentID)
	{
		return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_StudentReputation + studentID.ToString());
	}

	public static void SetStudentReputation(int studentID, int value)
	{
		string studentString = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_StudentReputation, studentString);
		PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_StudentReputation + studentString, value);
	}

	public static int[] KeysOfStudentReputation()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_StudentReputation);
	}

	public static float GetStudentSanity(int studentID)
	{
		return PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_" + Str_StudentSanity + studentID.ToString());
	}

	public static void SetStudentSanity(int studentID, float value)
	{
		string studentString = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_StudentSanity, studentString);
		PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_" + Str_StudentSanity + studentString, value);
	}

	public static int[] KeysOfStudentSanity()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_StudentSanity);
	}

    public static int StudentSlave
    {
        get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_StudentSlave); }
        set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_StudentSlave, value); }
    }

    public static int FragileSlave
    {
        get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_FragileSlave); }
        set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_FragileSlave, value); }
    }

    public static int FragileTarget
    {
        get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_FragileTarget); }
        set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_FragileTarget, value); }
    }

	public static Vector3 GetReputationTriangle (int studentID)
	{
		return GlobalsHelper.GetVector3("Profile_" + GameGlobals.Profile + "_Student_" + studentID + "_" + Str_ReputationTriangle);
	}

	public static void SetReputationTriangle (int studentID, Vector3 triangle)
	{
		       GlobalsHelper.SetVector3("Profile_" + GameGlobals.Profile + "_Student_" + studentID + "_" + Str_ReputationTriangle, triangle);
	}

	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_CustomSuitor);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_CustomSuitorAccessory);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_CustomSuitorBlonde);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_CustomSuitorBlack);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_CustomSuitorEyewear);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_CustomSuitorHair);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_CustomSuitorJewelry);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_CustomSuitorTan);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_ExpelProgress);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_FemaleUniform);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_MaleUniform);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_StudentSlave);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_FragileSlave);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_FragileTarget);

		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_StudentAccessory, KeysOfStudentAccessory());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_StudentArrested, KeysOfStudentArrested());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_StudentBroken, KeysOfStudentBroken());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_StudentBustSize, KeysOfStudentBustSize());
		GlobalsHelper.DeleteColorCollection("Profile_" + GameGlobals.Profile + "_" + Str_StudentColor, KeysOfStudentColor());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_StudentDead, KeysOfStudentDead());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_StudentDying, KeysOfStudentDying());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_StudentExpelled, KeysOfStudentExpelled());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_StudentExposed, KeysOfStudentExposed());
		GlobalsHelper.DeleteColorCollection("Profile_" + GameGlobals.Profile + "_" + Str_StudentEyeColor, KeysOfStudentEyeColor());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_StudentGrudge, KeysOfStudentGrudge());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_StudentHairstyle, KeysOfStudentHairstyle());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_StudentKidnapped, KeysOfStudentKidnapped());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_StudentMissing, KeysOfStudentMissing());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_StudentName, KeysOfStudentName());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_StudentPhotographed, KeysOfStudentPhotographed());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_StudentPhoneStolen, KeysOfStudentPhoneStolen());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_StudentReplaced, KeysOfStudentReplaced());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_StudentReputation, KeysOfStudentReputation());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_StudentSanity, KeysOfStudentSanity());

		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_MemorialStudents);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_MemorialStudent1);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_MemorialStudent2);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_MemorialStudent3);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_MemorialStudent4);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_MemorialStudent5);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_MemorialStudent6);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_MemorialStudent7);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_MemorialStudent8);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_MemorialStudent9);
	}
}

public static class TaskGlobals
{
	const string Str_GuitarPhoto = "GuitarPhoto_";
	const string Str_KittenPhoto = "KittenPhoto_";
	const string Str_HorudaPhoto = "HorudaPhoto_";
	const string Str_TaskStatus = "TaskStatus_"; // Replaces "Task_" + ID + "_Status".

	public static bool GetGuitarPhoto(int photoID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_GuitarPhoto + photoID.ToString());
	}

	public static void SetGuitarPhoto(int photoID, bool value)
	{
		string photoString = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_GuitarPhoto, photoString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_GuitarPhoto + photoString, value);
	}

	public static int[] KeysOfGuitarPhoto()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_GuitarPhoto);
	}

	public static bool GetKittenPhoto(int photoID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_KittenPhoto + photoID.ToString());
	}

	public static void SetKittenPhoto(int photoID, bool value)
	{
		string photoString = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_KittenPhoto, photoString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_KittenPhoto + photoString, value);
	}

	public static int[] KeysOfKittenPhoto()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_KittenPhoto);
	}

	public static bool GetHorudaPhoto(int photoID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_HorudaPhoto + photoID.ToString());
	}

	public static void SetHorudaPhoto(int photoID, bool value)
	{
		string photoString = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_HorudaPhoto, photoString);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_HorudaPhoto + photoString, value);
	}

	public static int[] KeysOfHorudaPhoto()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_HorudaPhoto);
	}

	public static int GetTaskStatus(int taskID)
	{
		return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_TaskStatus + taskID.ToString());
	}

	public static void SetTaskStatus(int taskID, int value)
	{
		string taskString = taskID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_TaskStatus, taskString);
		PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_TaskStatus + taskString, value);
	}

	public static int[] KeysOfTaskStatus()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_TaskStatus);
	}

	public static void DeleteAll()
	{
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_GuitarPhoto, KeysOfGuitarPhoto());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_KittenPhoto, KeysOfKittenPhoto());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_HorudaPhoto, KeysOfHorudaPhoto());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_TaskStatus, KeysOfTaskStatus());
	}
}

public static class YanvaniaGlobals
{
	const string Str_DraculaDefeated = "DraculaDefeated";
	const string Str_MidoriEasterEgg = "MidoriEasterEgg";

	public static bool DraculaDefeated
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_DraculaDefeated); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_DraculaDefeated, value); }
	}

	public static bool MidoriEasterEgg
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_MidoriEasterEgg); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_MidoriEasterEgg, value); }
	}

	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_DraculaDefeated);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_MidoriEasterEgg);
	}
}

public static class WeaponGlobals
{
	const string Str_WeaponStatus = "WeaponStatus_";

	public static int GetWeaponStatus(int weaponID)
	{
		return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_WeaponStatus + weaponID.ToString());
	}

	public static void SetWeaponStatus(int weaponID, int value)
	{
		string weaponString = weaponID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_" + Str_WeaponStatus, weaponString);
		PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_WeaponStatus + weaponString, value);
	}

	public static int[] KeysOfWeaponStatus()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_" + Str_WeaponStatus);
	}

	public static void DeleteAll()
	{
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_" + Str_WeaponStatus, KeysOfWeaponStatus());
	}
}

public static class TutorialGlobals
{
	const string Str_IgnoreClothing = "IgnoreClothing";
	const string Str_IgnoreCouncil = "IgnoreCouncil";
	const string Str_IgnoreTeacher = "IgnoreTeacher";
	const string Str_IgnoreLocker = "IgnoreLocker";
	const string Str_IgnorePolice = "IgnorePolice";
	const string Str_IgnoreSanity = "IgnoreSanity";
	const string Str_IgnoreSenpai = "IgnoreSenpai";
	const string Str_IgnoreVision = "IgnoreVision";
	const string Str_IgnoreWeapon = "IgnoreWeapon";
	const string Str_IgnoreBlood = "IgnoreBlood";
	const string Str_IgnoreClass = "IgnoreClass";
    const string Str_IgnoreMoney = "IgnoreMoney";
    const string Str_IgnorePhoto = "IgnorePhoto";
	const string Str_IgnoreClub = "IgnoreClub";
	const string Str_IgnoreInfo = "IgnoreInfo";
	const string Str_IgnorePool = "IgnorePool";
	const string Str_IgnoreRep = "IgnoreClass";

	public static bool IgnoreClothing
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreClothing); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreClothing, value); }
	}

	public static bool IgnoreCouncil
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreCouncil); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreCouncil, value); }
	}

	public static bool IgnoreTeacher
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreTeacher); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreTeacher, value); }
	}

	public static bool IgnoreLocker
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreLocker); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreLocker, value); }
	}

	public static bool IgnorePolice
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnorePolice); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnorePolice, value); }
	}

	public static bool IgnoreSanity
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreSanity); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreSanity, value); }
	}

	public static bool IgnoreSenpai
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreSenpai); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreSenpai, value); }
	}

	public static bool IgnoreVision
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreVision); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreVision, value); }
	}

	public static bool IgnoreWeapon
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreWeapon); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreWeapon, value); }
	}

	public static bool IgnoreBlood
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreBlood); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreBlood, value); }
	}

	public static bool IgnoreClass
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreClass); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreClass, value); }
	}

    public static bool IgnoreMoney
    {
        get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreMoney); }
        set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreMoney, value); }
    }

    public static bool IgnorePhoto
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnorePhoto); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnorePhoto, value); }
	}

	public static bool IgnoreClub
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreClub); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreClub, value); }
	}

	public static bool IgnoreInfo
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreInfo); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreInfo, value); }
	}

	public static bool IgnorePool
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnorePool); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnorePool, value); }
	}

	public static bool IgnoreRep
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreRep); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreRep, value); }
	}

	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreClothing);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreCouncil);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreTeacher);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreLocker);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_IgnorePolice);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreSanity);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreSenpai);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreVision);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreWeapon);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreBlood);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreClass);
        Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreMoney);
        Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_IgnorePhoto);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreClub);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreInfo);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_IgnorePool);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_IgnoreRep);
	}
}

public static class CounselorGlobals
{
	const string Str_DelinquentPunishments = "DelinquentPunishments";
	const string Str_CounselorPunishments = "CounselorPunishments";
	const string Str_CounselorVisits = "CounselorVisits";
	const string Str_CounselorTape = "CounselorTape";
	const string Str_ApologiesUsed = "ApologiesUsed";
	const string Str_WeaponsBanned = "WeaponsBanned";

	const string Str_BloodVisits = "BloodVisits";
	const string Str_InsanityVisits = "InsanityVisits";
	const string Str_LewdVisits = "LewdVisits";
	const string Str_TheftVisits = "TheftVisits";
	const string Str_TrespassVisits = "TrespassVisits";
	const string Str_WeaponVisits = "WeaponVisits";

	const string Str_BloodExcuseUsed = "BloodExcuseUsed";
	const string Str_InsanityExcuseUsed = "InsanityExcuseUsed";
	const string Str_LewdExcuseUsed = "LewdExcuseUsed";
	const string Str_TheftExcuseUsed = "TheftExcuseUsed";
	const string Str_TrespassExcuseUsed = "TrespassExcuseUsed";
	const string Str_WeaponExcuseUsed = "WeaponExcuseUsed";

	const string Str_BloodBlameUsed = "BloodBlameUsed";
	const string Str_InsanityBlameUsed = "InsanityBlameUsed";
	const string Str_LewdBlameUsed = "LewdBlameUsed";
	const string Str_TheftBlameUsed = "TheftBlameUsed";
	const string Str_TrespassBlameUsed = "TrespassBlameUsed";
	const string Str_WeaponBlameUsed = "WeaponBlameUsed";

	public static int DelinquentPunishments
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_DelinquentPunishments); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_DelinquentPunishments, value); }
	}

	public static int CounselorPunishments
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_CounselorPunishments); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_CounselorPunishments, value); }
	}

	public static int CounselorVisits
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_CounselorVisits); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_CounselorVisits, value); }
	}

	public static int CounselorTape
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_CounselorTape); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_CounselorTape, value); }
	}

	public static int ApologiesUsed
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_ApologiesUsed); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_ApologiesUsed, value); }
	}

	public static int WeaponsBanned
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_WeaponsBanned); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_WeaponsBanned, value); }
	}

	//////////////////
	///// VISITS /////
	//////////////////

	public static int BloodVisits
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_BloodVisits); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_BloodVisits, value); }
	}

	public static int InsanityVisits
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_InsanityVisits); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_InsanityVisits, value); }
	}

	public static int LewdVisits
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_LewdVisits); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_LewdVisits, value); }
	}

	public static int TheftVisits
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_TheftVisits); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_TheftVisits, value); }
	}

	public static int TrespassVisits
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_TrespassVisits); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_TrespassVisits, value); }
	}

	public static int WeaponVisits
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_WeaponVisits); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_WeaponVisits, value); }
	}

	///////////////////
	///// EXCUSES /////
	///////////////////

	public static int BloodExcuseUsed
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_BloodExcuseUsed); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_BloodExcuseUsed, value); }
	}

	public static int InsanityExcuseUsed
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_InsanityExcuseUsed); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_InsanityExcuseUsed, value); }
	}

	public static int LewdExcuseUsed
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_LewdExcuseUsed); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_LewdExcuseUsed, value); }
	}

	public static int TheftExcuseUsed
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_TheftExcuseUsed); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_TheftExcuseUsed, value); }
	}

	public static int TrespassExcuseUsed
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_TrespassExcuseUsed); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_TrespassExcuseUsed, value); }
	}

	public static int WeaponExcuseUsed
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_WeaponExcuseUsed); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_WeaponExcuseUsed, value); }
	}

	//////////////////
	///// BLAMES /////
	//////////////////

	public static int BloodBlameUsed
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_BloodBlameUsed); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_BloodBlameUsed, value); }
	}

	public static int InsanityBlameUsed
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_InsanityBlameUsed); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_InsanityBlameUsed, value); }
	}

	public static int LewdBlameUsed
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_LewdBlameUsed); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_LewdBlameUsed, value); }
	}

	public static int TheftBlameUsed
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_TheftBlameUsed); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_TheftBlameUsed, value); }
	}

	public static int TrespassBlameUsed
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_TrespassBlameUsed); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_TrespassBlameUsed, value); }
	}

	public static int WeaponBlameUsed
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_WeaponBlameUsed); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_WeaponBlameUsed, value); }
	}

	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_DelinquentPunishments);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_CounselorPunishments);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_CounselorVisits);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_CounselorTape);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_ApologiesUsed);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_WeaponsBanned);

		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_BloodVisits);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_InsanityVisits);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_LewdVisits);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_TheftVisits);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_TrespassVisits);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_WeaponVisits);

		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_BloodExcuseUsed);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_InsanityExcuseUsed);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_LewdExcuseUsed);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_TheftExcuseUsed);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_TrespassExcuseUsed);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_WeaponExcuseUsed);

		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_BloodBlameUsed);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_InsanityBlameUsed);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_LewdBlameUsed);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_TheftBlameUsed);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_TrespassBlameUsed);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_WeaponBlameUsed);
	}
}

public static class YancordGlobals
{
	const string Str_JoinedYancord = "JoinedYancord";
	const string Str_CurrentConversation = "CurrentConversation";

	public static bool JoinedYancord
	{
		get { return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_" + Str_JoinedYancord); }
		set { GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_" + Str_JoinedYancord, value); }
	}

	public static int CurrentConversation
	{
		get { return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_" + Str_CurrentConversation); }
		set { PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_" + Str_CurrentConversation, value); }
	}

	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_JoinedYancord);
		Globals.Delete("Profile_" + GameGlobals.Profile + "_" + Str_CurrentConversation);
	}
}

public static class CorkboardGlobals
{
	public static void DeleteAll()
	{
		for (int ID = 0; ID < 100; ID++)
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_Exists", 0);
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_PhotoID", 0);

			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_PositionX", 0);
			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_PositionY", 0);
			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_PositionZ", 0);

			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_RotationX", 0);
			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_RotationY", 0);
			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_RotationZ", 0);

			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_ScaleX", 0);
			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_ScaleY", 0);
			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_CorkboardPhoto_" + ID + "_ScaleZ", 0);

			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CorkboardString_" + ID + "_Exists", 0);

			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_CorkboardString_" + ID + "_PositionX", 0);
			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_CorkboardString_" + ID + "_PositionY", 0);
			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_CorkboardString_" + ID + "_PositionZ", 0);

			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_CorkboardString2_" + ID + "_PositionX", 0);
			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_CorkboardString2_" + ID + "_PositionY", 0);
			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_CorkboardString2_" + ID + "_PositionZ", 0);
		}
	}
}