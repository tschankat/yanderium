using UnityEngine;

public static class GameObjectUtils
{
	public static void SetLayerRecursively(GameObject obj, int newLayer)
	{
		obj.layer = newLayer;

		foreach (Transform child in obj.transform)
		{
			SetLayerRecursively(child.gameObject, newLayer);
		}
	}

	public static void SetTagRecursively(GameObject obj, string newTag)
	{
		obj.tag = newTag;

		foreach (Transform child in obj.transform)
		{
			SetTagRecursively(child.gameObject, newTag);
		}
	}
}
