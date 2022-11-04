using UnityEngine;
using System.Collections.Generic;
using System.IO;

public static class PoseSerializer
{
    //Argument 0 is streaming assets path, argument 1 is filename
    public const string SavePath = "{0}/Poses/{1}";

	public static void SerializePose(CosmeticScript cosmeticScript, Transform root, string poseName)
	{
		SerializedPose pose;
		StudentCosmeticSheet myCosmeticSheet = cosmeticScript.CosmeticSheet();
		pose.CosmeticData = JsonUtility.ToJson(myCosmeticSheet);
		pose.BoneData = getBoneData(root);

		string serializedPose = JsonUtility.ToJson(pose);
		string saveFilePath = string.Format(SavePath, Application.streamingAssetsPath, poseName + ".txt");

		FileInfo fileInfo = new FileInfo(saveFilePath);
		fileInfo.Directory.Create();
		File.WriteAllText(saveFilePath, serializedPose);
	}

	private static BoneData[] getBoneData(Transform root)
	{
		List<BoneData> data = new List<BoneData>();
		Transform[] children = root.GetComponentsInChildren<Transform>();
		foreach(Transform child in children)
		{
			BoneData bone = new BoneData();
			bone.BoneName = child == root ? "StudentRoot" : child.name;
			bone.LocalPosition = child.localPosition;
			bone.LocalRotation = child.localRotation;
			bone.LocalScale = child.localScale;
			data.Add(bone);
		}
		return data.ToArray();
	}

	public static void DeserializePose(CosmeticScript cosmeticScript, Transform root, string poseName)
	{
		string filePath = string.Format(SavePath, Application.streamingAssetsPath, poseName+".txt");
		if (File.Exists(filePath))
		{
			string serializedData = File.ReadAllText(filePath);
			SerializedPose pose = JsonUtility.FromJson<SerializedPose>(serializedData);

			StudentCosmeticSheet newCosmeticSheet = JsonUtility.FromJson<StudentCosmeticSheet>(pose.CosmeticData);
			cosmeticScript.LoadCosmeticSheet(newCosmeticSheet);
			cosmeticScript.CharacterAnimation.Stop();

			bool isSameGender = cosmeticScript.Male == newCosmeticSheet.Male;

			Transform[] children = root.GetComponentsInChildren<Transform>();
			foreach(BoneData data in pose.BoneData)
			{
				foreach(Transform child in children)
				{
					if (child.name == data.BoneName && child != cosmeticScript.LeftEyeRenderer.transform &&
                        child != cosmeticScript.RightEyeRenderer.transform)
					{
						child.localRotation = data.LocalRotation;
						if (isSameGender)
						{
							child.localPosition = data.LocalPosition;
							child.localScale = data.LocalScale;
						}
					}
					else if (data.BoneName == "StudentRoot" && child == root)
					{
						child.localPosition = data.LocalPosition;
						child.localRotation = data.LocalRotation;
						child.localScale = data.LocalScale;
					}

				}
			}
		}
	}

    public static string[] GetSavedPoses()
    {
        string[] files = Directory.GetFiles(string.Format(SavePath, Application.streamingAssetsPath, ""));
        List<string> poseFiles = new List<string>();
        foreach(string file in files)
        {
            if (file.EndsWith(".txt")) poseFiles.Add(file);
        }
        return poseFiles.ToArray();
    }
}

public struct SerializedPose
{
    public string CosmeticData;
    public BoneData[] BoneData;
}

[System.Serializable]
public struct BoneData
{
    public string BoneName;
    public Quaternion LocalRotation;
    public Vector3 LocalPosition;
    public Vector3 LocalScale;
}
