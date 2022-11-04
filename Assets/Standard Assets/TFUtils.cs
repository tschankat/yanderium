#define ASSERTS_ON
#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define DEBUG
#endif

using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Text;
//using Ionic.Zlib;      
//using Debug ; //= UnityMock.Debug;


public class TFUtils
{
    public static DateTime EPOCH = new DateTime(1970, 1, 1).ToUniversalTime();

    public static string ApplicationDataPath;
    public static string ApplicationPersistentDataPath;
    public static string DeviceID;
    public static string DeviceName;


    public static void Init()
    {
        // cache this in a static constructor so this path can be accessed from threads other than the main thread.
        ApplicationDataPath = Application.dataPath;
        ApplicationPersistentDataPath = Application.persistentDataPath;
        DeviceID = SystemInfo.deviceUniqueIdentifier;
        DeviceName = SystemInfo.deviceName;
        TFUtils.DebugLog("This device is:" + DeviceID);
    }


    public static int EpochTime()
    {
        return EpochTime(DateTime.UtcNow);
    }

    public static int EpochTime(DateTime dt)
    {
        TimeSpan t = (dt - EPOCH);
        return (int) t.TotalSeconds;
    }

    public static DateTime EpochToDateTime(int seconds)
    {
        return DateTime.SpecifyKind(EPOCH.AddSeconds(seconds), 
                                    DateTimeKind.Utc);
    }

    public static string DurationToString(int duration) {
        if (duration < 60) {
            return string.Format("{0}s", duration);
        }

        var seconds = duration % 60;
        duration -= seconds;
        var minutes = duration / 60;

        if (minutes < 60) {
            if (seconds == 0) {
                return string.Format("{0}m", minutes);
            }
            else {
                return string.Format("{0}m {1}s", minutes, seconds);
            }
        }

        var hours = minutes / 60;
        minutes %= 60;

        if (hours < 24) {
            if (minutes == 0) {
                return string.Format("{0}h", hours);
            }
            else {
                return string.Format("{0}h {1}m", hours, minutes);
            }
        }

        var days = hours / 24;
        hours %= 24;

        if (hours == 0) {
            return string.Format("{0}d", days);
        }
        else {
            return string.Format("{0}d {1}h", days, hours);
        }
    }

    /// <summary>
    /// Clones the dictionary.  This is needed because the copy constructor for dictionaries
    /// is busted on Mono.
    /// </summary>
    /// <returns>
    /// A copy of the dictionary.
    /// </returns>
    /// <param name='source'>
    /// The dictionary to clone.
    /// </param>
    public static Dictionary<KeyType, ValueType> CloneDictionary<KeyType, ValueType>(Dictionary<KeyType, ValueType> source)
    {
        Dictionary<KeyType, ValueType> copy = new Dictionary<KeyType, ValueType>();
        foreach (KeyType key in source.Keys)
        {
            copy[key] = source[key];
        }
        return copy;
    }

    public static void CloneDictionaryInPlace<KeyType, ValueType>(Dictionary<KeyType, ValueType> source, Dictionary<KeyType, ValueType> dest) {
        dest.Clear();

        foreach (var kvp in source) {
            dest.Add(kvp.Key, kvp.Value);
        }
    }

    public static Dictionary<KeyType, ValueType> ConcatenateDictionaryInPlace<KeyType, ValueType>(Dictionary<KeyType, ValueType> dest, Dictionary<KeyType, ValueType> source)
    {
        foreach (KeyType key in source.Keys)
        {
            if (dest.ContainsKey(key))
            {
                throw new System.ArgumentException("Destination dictionary already contains key " + key.ToString());
            }
            else
            {
                dest[key] = source[key];
            }
        }
        return dest;
    }

    public static List<To> CloneAndCastList<From,To>(List<From> list)
        where From : To
    {
        List<To> rv = new List<To>(list.Count);
        foreach(From element in list)
        {
            rv.Add((To)element);
        }
        return rv;
    }

    private static T AssertCast<T>(Dictionary<string,object> dict, string key)
    {
        AssertKeyExists(dict, key);
        Assert(
            (dict[key] is T),
            String.Format("Could not cast the key({0}) with value({1}) to type({2}) in dictionary{3}",
                key, dict[key], typeof(T).ToString(), DebugDictToString(dict)));

        return (T)dict[key];
    }


    //[Conditional("DEBUG")]
    public static void AssertKeyExists(Dictionary<string, object> dict, string key)
    {
        Assert(
            dict != null,
            String.Format("Can't search for the key '{0}' in a null dictionary", key));
        Assert(
            dict.ContainsKey(key),
            String.Format("Could not find the key '{0}' in the given dictionary:\n{1}", key, DebugDictToString(dict)));
    }

    public static System.Nullable<bool> LoadNullableBool(Dictionary<string, object> d, string key)
    {
        AssertKeyExists(d, key);

        object o = d[key];
        if (o == null)
        {
            return (System.Nullable<bool>)o;
        }
        else
        {
            return (System.Nullable<bool>)d[key];
        }
    }

    public static List<T> TryLoadList<T>(Dictionary<string,object> data, string key)
    {
        if (!data.ContainsKey(key))
            return null;

        return LoadList<T>(data, key);
    }
    
    public static List<T> LoadList<T>(Dictionary<string,object> data, string key)
    {
        AssertKeyExists(data, key);
        if ((data[key] as List<T>) != null)
        return (List<T>)data[key];
        else
        {
            List<object> objList = (List<object>)data[key];
            List<T> retval = new List<T>(data.Count);
            objList.ForEach( obj => { retval.Add((T)Convert.ChangeType(obj, typeof(T))); } );
            return retval;
        }
    }


    public static Dictionary<string,object> LoadDict(Dictionary<string,object> data, string key)
    {
        AssertKeyExists(data, key);
        return (Dictionary<string,object>)data[key];
    }


    public static Dictionary<string,object> TryLoadDict(Dictionary<string,object> data, string key)
    {
        if (!data.ContainsKey(key))
            return null;
        else
            return (Dictionary<string,object>)data[key];
    }


    public static string LoadString(Dictionary<string,object> data, string key)
    {
        AssertKeyExists(data, key);
        AssertCast<string>(data, key);

        return (string)data[key];
    }


    public static string TryLoadString(Dictionary<string,object> data, string key)
    {
        if (data.ContainsKey(key))
            return AssertCast<string>(data, key);
        else
            return null;
    }


    public static string LoadNullableString(Dictionary<string,object> data, string key)
    {
        AssertKeyExists(data, key);
        return (string)data[key];
    }


    public static System.Nullable<int> LoadNullableInt(Dictionary<string, object> d, string key)
    {
        AssertKeyExists(d, key);

        object o = d[key];
        if (o == null)
        {
            return (System.Nullable<int>)o;
        }
        else
        {
            return (System.Nullable<int>)LoadInt(d, key);
        }
    }

    public static System.Nullable<uint> LoadNullableUInt(Dictionary<string, object> d, string key)
    {
        AssertKeyExists(d, key);

        object o = d[key];
        if (o == null)
        {
            return (System.Nullable<uint>)o;
        }
        else
        {
            return (System.Nullable<uint>)LoadUint(d, key);
        }
    }

    public static System.Nullable<uint> TryLoadNullableUInt(Dictionary<string, object> d, string key)
    {
        if (d.ContainsKey(key))
        {
            return LoadNullableUInt(d, key);
        }
        else
        {
            return (System.Nullable<uint>)null;
        }
    }

    public static object NullableToObject<T>(System.Nullable<T> nullable) where T : struct
    {
        return nullable.HasValue ? (object)nullable.Value : null;
    }

    public static int? TryLoadInt(Dictionary<string,object> data, string key)
    {
        if (data.ContainsKey(key))
            return LoadIntHelper(data, key);
        else
            return null;
    }

    public static bool LoadBoolAsInt(Dictionary<string, object> d, string key)
    {
        int intValue = LoadInt(d, key);
        return intValue == 0 ? false : true;
    }

    public static int LoadInt(Dictionary<string, object> d, string key)
    {
        AssertKeyExists(d, key);
        return LoadIntHelper(d, key);
    }

    private static int LoadIntHelper(Dictionary<string, object> d, string key)
    {
#if DEBUG
        try
        {
#endif
            return Convert.ToInt32(d[key]);
#if DEBUG
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
            Debug.LogError(
                String.Format(
                    "Could not convert value to Int32!\nKey={0}, Value={1}, Dictionary={2}",
                    key, d[key], DebugDictToString(d)));

            return 0;
        }
#endif
    }

    public static uint LoadUint(Dictionary<string, object> data, string key)
    {
        AssertKeyExists(data, key);
        return LoadUintHelper(data, key);
    }

    public static uint? TryLoadUint(Dictionary<string, object> data, string key)
    {
        if (!data.ContainsKey(key))
            return null;
        return LoadUintHelper(data, key);
    }

    private static uint LoadUintHelper(Dictionary<string, object> data, string key)
    {
#if DEBUG
        try
        {
#endif
            return Convert.ToUInt32(data[key]);
#if DEBUG
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
            Debug.LogError(
                String.Format(
                    "Could not convert value to Uint32!\nKey={0}, Value={1}, Dictionary={2}",
                    key, data[key], DebugDictToString(data)));

            return 0;
        }
#endif
    }

    public static float? TryLoadFloat(Dictionary<string,object> data, string key)
    {
        if (data.ContainsKey(key))
            return (float)AssertCast<double>(data, key);
        else
            return null;
    }

    public static float LoadFloat(Dictionary<string, object> d, string key)
    {
        TFUtils.AssertKeyExists(d, key);
        float result;
        if (float.TryParse((string)d[key], NumberStyles.Float, NumberFormatInfo.InvariantInfo, out result))
        {
            return result;
        }
        return 0f;
    }

    public static void LoadVector3(out Vector3 v3, Dictionary<string, object> d, float defaultValue)
    {
        v3.x = (d.ContainsKey("x"))? TFUtils.LoadFloat(d, "x"):defaultValue;
        v3.y = (d.ContainsKey("y"))? TFUtils.LoadFloat(d, "y"):defaultValue;
        v3.z = (d.ContainsKey("z"))? TFUtils.LoadFloat(d, "z"):defaultValue;
    }
    
    // \todo saving/loading vector as a dictionary seem to be expensive? Each vector has exactly x, y, z.
    public static void SaveVector3(Vector3 v3, string name, Dictionary<string, object> d )
    {
        d[name] = new Dictionary<string, object> 
            {
                { "x", v3.x },
                { "y", v3.y },
                { "z", v3.z },            
            };
    }
    
    public static void LoadVector2(out Vector2 v2, Dictionary<string, object> d, float defaultValue)
    {
        Assert(!d.ContainsKey("z"), "Don't call LoadVector2 on something that has a z value! (do you want to use LoadVector3?)");

        v2.x = (d.ContainsKey("x"))? TFUtils.LoadFloat(d, "x"):defaultValue;
        v2.y = (d.ContainsKey("y"))? TFUtils.LoadFloat(d, "y"):defaultValue;
    }

    public static void LoadVector3(out Vector3 v3, Dictionary<string, object> d)
    {
        LoadVector3(out v3, d, 0F);
    }

    public static void LoadVector2(out Vector2 v2, Dictionary<string, object> d)
    {
        LoadVector2(out v2, d, 0F);
    }

    public static Vector3 ExpandVector(Vector2 vector)
    {
        return ExpandVector(vector, 0);
    }

    public static Vector3 ExpandVector(Vector2 vector, float z)
    {
        return new Vector3(vector.x, vector.y, z);
    }

    public static Vector2 TruncateVector(Vector3 vector)
    {
        return new Vector2(vector.x, vector.y);
    }

    public static void TruncateFile(string filePath)
    {
        DeleteFile(filePath);
        using (FileStream fs = File.Create(filePath)) {
            fs.Close();
        }
    }

    public static void DeleteFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    
    public static string GetPersistentAssetsPath()
    {
    	return Path.Combine(ApplicationPersistentDataPath, "Contents");
    }
    public static string GetStreamingAssetsPath()
    {
#if UNITY_EDITOR
        return  Path.Combine(ApplicationDataPath, "StreamingAssets");
#elif UNITY_IPHONE
        return Path.Combine(ApplicationDataPath, "Raw");
#elif UNITY_ANDROID
        return "jar:file://" + ApplicationDataPath + Path.DirectorySeparatorChar + "!/assets";
#else
        return Path.Combine(ApplicationDataPath, "StreamingAssets");
#endif
    }

    public static string GetStreamingAssetsSubfolder(string path)
    {
        return GetStreamingAssetsPath() + Path.DirectorySeparatorChar + path;
    }

    public static string GetStreamingAssetsFileInDirectory(string path, string filename)
    {
        return GetStreamingAssetsFile(path + Path.DirectorySeparatorChar + filename);
    }
    
    
    public static string GetStreamingAssetsFile(string fileName)
    {
    	// if it exists on the filesystem, get it there.
    	string persistentPath = GetPersistentAssetsPath() + Path.DirectorySeparatorChar + fileName;
    	if (File.Exists(persistentPath))
    	{
    		return persistentPath;
    	}
    	// otherwise, return the bundled version of the doc.
        return GetStreamingAssetsPath() + Path.DirectorySeparatorChar + fileName;
    }

    public static string[] GetFilesInPath(string path, string searchPattern)
    {
    	// we create a table of all of the json files currently bundled with the app. 
    	Dictionary<string, string> relativeToAbsolutePaths = new Dictionary<string, string>();	
    	
		// [af] Commented out unused code.
    	/*string bundledSearchPath = GetStreamingAssetsSubfolder(path);
    	string bundledBasePath = GetStreamingAssetsPath();*/
    	
        //foreach (string filePath in Directory.GetFiles(bundledSearchPath, searchPattern, SearchOption.AllDirectories))
        //{
        //    string relativePath = filePath.Substring(bundledBasePath.Length);
        //    relativeToAbsolutePaths[relativePath] = filePath;
        //}
        
		// [af] Commented out unused code.
        //string persistentBasePath = GetPersistentAssetsPath();

        string persistentSearchPath = GetPersistentAssetsPath() + Path.DirectorySeparatorChar +path;
        // we replace those files with any newer versions that might be patched from the file system
        if (Directory.Exists(persistentSearchPath))
        {
	       // foreach (string filePath in Directory.GetFiles(persistentSearchPath, searchPattern, SearchOption.AllDirectories))
           // {
	       //     string relativePath = filePath.Substring(persistentBasePath.Length);
	       //     relativeToAbsolutePaths[relativePath] = filePath;
           // }
        }
        
        string[] retVals = new string[relativeToAbsolutePaths.Count];
        relativeToAbsolutePaths.Values.CopyTo(retVals, 0);
        return retVals;
    }

    /// <summary>
    /// This is slow. Use it for debugging only!
    /// </summary>
    //[Conditional("DEBUG")]
    public static void DebugDict(Dictionary<string, object> d)
    {
        Debug.Log(DebugDictToString(d));
    }

    /// <summary>
    /// This is slow. Use it for debugging only!
    /// </summary>
    public static string DebugDictToString(Dictionary<string, object> d)
    {
        return "[Dictionary Debug View]\n" + PrintDict(d, "");
    }

    /// <summary>
    /// This is slow. Use it for debugging only!
    /// </summary>
    public static string DebugListToString(List<object> l)
    {
        return "[List Debug View]\n" + PrintList(l, "");
    }
    public static string DebugListToString(List<Vector3> list)
    {
        return DebugListToString(list.ConvertAll<object>(v => "\t("+v.x+",\t"+v.y+",\t"+v.z+")"));
    }
    public static string DebugListToString(List<Vector2> list)
    {
        return DebugListToString(list.ConvertAll<Vector3>(v => ExpandVector(v)));
    }

    private static string PrintDict(Dictionary<string, object> d, string lead)
    {
        if (d == null)
            return "null";

        string retStr = "{\n";
        foreach (string k in d.Keys)
        {
            if (d[k] as object != null)
                retStr += lead + k + ":" + PrintGenericValue(d[k], lead+" ") + ",\n";
            else
                retStr += lead + k + ":" + d[k] + ",\n";
        }
        return retStr + lead + "}";
    }

    private static string PrintList(List<object> l, string lead)
    {
        if (l == null)
            return "null";

        string retStr = "[\n";
        for (int i = 0; i < l.Count; i++)
        {
            retStr += lead + i + ":" + PrintGenericValue(l[i], lead+" ") + ",\n";
        }
        return retStr + lead + "]";
    }

    private static string PrintGenericValue(object v, string lead)
    {
        if (v is Dictionary<string, object>)
        {
            return PrintDict(v as Dictionary<string, object>, lead + " ");
        }
        else if (v is List<object>)
        {
            return PrintList(v as List<object>, lead+" ");
        }
        else if (v == null)
        {
            return "null\n";
        }
        else
        {
            return v.ToString();
        }
    }
#if !UNITY_EDITOR && UNITY_IPHONE
        [DllImport("__Internal")]
        private static extern void NSLog_Log(string msg);
#endif

    public static void DebugLog(object message)
    {
#if !UNITY_EDITOR && UNITY_IPHONE
        if (message != null)
        {
            string messageStr= message.ToString();
            if (messageStr != null)
            {
                NSLog_Log(messageStr);
            }
        }
#else
        Debug.Log(message);
#endif
    }

//    [Conditional("DEBUG")]
    public static void ErrorLog(object message)
    {
#if !UNITY_EDITOR && UNITY_IPHONE
        // should there be a call to analytics here so we can track errors on the device?   
        // seems so.
        // TODO -  Call to Analytics
        if (message != null)
        {
            string messageStr= message.ToString();
            if (messageStr != null)
            {
                NSLog_Log(messageStr);
            }
        }
#else
        Debug.LogError(message);
#endif
    }

   /// [Conditional("DEBUG")]
    public static void LogFormat(string format, params object[] args)
    {
        Debug.Log(string.Format(format, args));
    }

 //   [Conditional("DEBUG")]
    public static void UnexpectedEntry()
    {
        throw new Exception("Unexpected path of code execution! You should not be here!");
    }

   // [Conditional("ASSERTS_ON")]
    public static void Assert(bool condition, string message)
    {
        if (!condition)
        {
            throw new Exception(message);
        }
    }

    // Traverse DOWN the hierarchy to look for a child game object with "name"
    public static GameObject FindGameObjectInHierarchy(GameObject root, string name)
    {
        if (root.name.Equals(name))
        {
            return root;
        }
        else
        {
            GameObject result = null;
            int count = root.transform.childCount;
            for (int i=0; i<count; i++)
            {
                result = FindGameObjectInHierarchy(root.transform.GetChild(i).gameObject, name);
                if (result != null)
                {
                    break;
                }
            }
            return result;
        }
    }

    // Traverse UP the hierarchy to look for a parent game object with "name"
    public static GameObject FindParentGameObjectInHierarchy(GameObject root, string name)
    {
        Transform currentTransform = root.transform;
        while( currentTransform.parent != null )
        {
            if( currentTransform.gameObject.name.Equals(name) )
                return currentTransform.gameObject;

            currentTransform = currentTransform.parent;
        }

        return null;
    }


    // This call is blocking, in iOS, Unity stops updating while movie plays and resumes from point of call
    // movie file must be placed in StreamingAssets folder.
    public static void PlayMovie(string movie)
    {
#if !UNITY_EDITOR && UNITY_IPHONE
        Handheld.PlayFullScreenMovie(
            movie,
            Color.black,FullScreenMovieControlMode.CancelOnInput);
#endif            
    }
    
	/*
	 * 
    static public byte[] Zip(String str) 
    {
        byte[] bytedata =  Encoding.UTF8.GetBytes(str);
        return Zip(bytedata);
    }

    static public byte[] Zip(byte[] bytedata) 
    {
        using (MemoryStream ms = new MemoryStream())
        {
            using (GZipStream gzip = new GZipStream(ms, CompressionMode.Compress, true))
            {
                gzip.Write(bytedata, 0, bytedata.Length);
                gzip.Close();
            }
            return ms.ToArray();
         }
    }
    
    static public byte[] UnzipToBytes(byte[] input)
    {
        MemoryStream inputStream = new MemoryStream(input);
        MemoryStream cms = new MemoryStream ();
        using (GZipStream gz = new GZipStream (inputStream, CompressionMode.Decompress))
        {
            byte[] buf = new byte[1024];
            int byteCount = 0;
            while ((byteCount = gz.Read (buf, 0, buf.Length)) > 0) 
            {
                cms.Write (buf, 0, byteCount);
            }
        }
        return cms.ToArray();
    }

    static public String Unzip(byte[] input)
    {
        return Encoding.UTF8.GetString(UnzipToBytes(input));
    }
	*/
    
    static public int BoolToInt (bool myBool)
    {
        if (myBool == true)
            return 1;
        else
            return 0;
    }
    
    static public int KontagentCurrencyLevelIndex (int kRange)
    {
        if (kRange > 0 && kRange < 10)
            return 1;
        else if (kRange > 10 && kRange < 100)
            return 2;
        else if (kRange > 100 && kRange < 1000)
            return 3;
        else if (kRange > 1000 && kRange < 10000)
            return 4;
        else if (kRange > 10000 && kRange < 100000)
            return 5;
        else if (kRange > 100000)
            return 6;
        else
            return 0; // something is terribly wrong!
    }
    
    static public string GetiOSDeviceTypeString()
    {
#if UNITY_IPHONE
        switch (iPhone.generation) 
        {
            case iPhoneGeneration.iPhone3GS:
                return "iPhone3GS";
            case iPhoneGeneration.iPad1Gen:
                return "iPad1Gen";
            case iPhoneGeneration.iPodTouch4Gen:
                return "iPodTouch4Gen";
            case iPhoneGeneration.iPhone4:
                return "iPhone4";
            case iPhoneGeneration.iPhone4S:
                return "iPhone4S";
            case iPhoneGeneration.iPad2Gen:
                return "iPad2Gen";
            case iPhoneGeneration.iPad3Gen:
                return "iPad3Gen";
            case iPhoneGeneration.iPodTouch5Gen:
                return "iPodTouch5Gen";
            case iPhoneGeneration.iPhone5:
                return "iPhone5";
            case iPhoneGeneration.iPadUnknown:
                return "iPadUnknown";
            case iPhoneGeneration.iPhoneUnknown:
                return "iPhoneUnknown";
            case iPhoneGeneration.iPodTouchUnknown:
                return "iPodTouchUnkown";
            case iPhoneGeneration.Unknown:
                return "Unknown";            
            default:
            	break;
        }
#endif    
        return "Unknown";
    }
    
}
